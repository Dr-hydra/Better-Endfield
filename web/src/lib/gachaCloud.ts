import { deflateSync, inflateSync, strFromU8, strToU8 } from "fflate";
import type { GachaSnapshotCategory, GachaSnapshotPool, GachaSnapshotStar, GachaWebSnapshot } from "../types";
import { getCloudStorage, hasToyAbility, removeCloudStorage, setCloudStorage } from "./toy";

const MANIFEST_KEY = "gacha_v1_manifest";
const PART_PREFIX = "gacha_v1_state_";
const PART_SIZE = 960;
const MAX_KEYS = 128;
const VERSION = 1;

interface GachaCloudManifest {
  version: number;
  parts: number;
  updatedAt: string;
}

export interface GachaCloudSaveResult {
  snapshot: GachaWebSnapshot;
  changed: boolean;
  parts: number;
}

function toBase64Url(bytes: Uint8Array): string {
  let binary = "";
  for (let index = 0; index < bytes.length; index += 0x8000) {
    binary += String.fromCharCode(...bytes.subarray(index, index + 0x8000));
  }
  return btoa(binary).replaceAll("+", "-").replaceAll("/", "_").replace(/=+$/, "");
}

function fromBase64Url(value: string): Uint8Array {
  const padded = value.replaceAll("-", "+").replaceAll("_", "/") + "=".repeat((4 - value.length % 4) % 4);
  return Uint8Array.from(atob(padded), (char) => char.charCodeAt(0));
}

function encodeSnapshot(snapshot: GachaWebSnapshot): string {
  return toBase64Url(deflateSync(strToU8(JSON.stringify(snapshot)), { level: 9 }));
}

function decodeSnapshot(value: string): GachaWebSnapshot {
  const parsed = JSON.parse(strFromU8(inflateSync(fromBase64Url(value)))) as GachaWebSnapshot;
  if (parsed.schemaVersion !== VERSION || parsed.kind !== "betterendfield.gacha" ||
      !Array.isArray(parsed.categories) || !Array.isArray(parsed.pools)) {
    throw new Error("寻访云存档版本不受支持");
  }
  return parsed;
}

function poolKey(pool: GachaSnapshotPool): string {
  return pool.poolId || `${pool.poolType}|${pool.categoryId}|${pool.poolName}`;
}

function newer(left: GachaWebSnapshot, right: GachaWebSnapshot): GachaWebSnapshot {
  return Date.parse(left.createdAt) >= Date.parse(right.createdAt) ? left : right;
}

function uniqueStars(values: GachaSnapshotStar[]): GachaSnapshotStar[] {
  const map = new Map<string, GachaSnapshotStar>();
  for (const value of values) {
    if (!value?.id) continue;
    const previous = map.get(value.id);
    if (!previous || (value.name && !previous.name)) map.set(value.id, value);
  }
  return [...map.values()];
}

function mergePool(oldPool: GachaSnapshotPool | undefined, incoming: GachaSnapshotPool, useIncomingMeta: boolean): GachaSnapshotPool {
  const sixStars = uniqueStars([...(oldPool?.sixStars ?? []), ...(incoming.sixStars ?? [])]);
  const freeSixStars = uniqueStars([...(oldPool?.freeSixStars ?? []), ...(incoming.freeSixStars ?? [])]);
  const meta = useIncomingMeta || !oldPool ? incoming : oldPool;
  const sixStarCount = Math.max(meta.sixStarCount, sixStars.length + freeSixStars.length);
  const upCount = Math.max(meta.upCount, sixStars.filter((star) => star.isUp).length + freeSixStars.filter((star) => star.isUp).length);
  return {
    ...meta,
    totalPulls: Math.max(oldPool?.totalPulls ?? 0, incoming.totalPulls),
    freePulls: Math.max(oldPool?.freePulls ?? 0, incoming.freePulls),
    sixStarCount,
    upCount,
    offRateCount: Math.max(0, sixStarCount - upCount),
    sixStars,
    freeSixStars,
  };
}

function rebuildCategories(pools: GachaSnapshotPool[], oldCategories: GachaSnapshotCategory[], incomingCategories: GachaSnapshotCategory[]): GachaSnapshotCategory[] {
  const metadata = new Map<string, GachaSnapshotCategory>();
  for (const category of [...oldCategories, ...incomingCategories]) metadata.set(category.id, category);
  const grouped = new Map<string, GachaSnapshotPool[]>();
  for (const pool of pools) grouped.set(pool.categoryId, [...(grouped.get(pool.categoryId) ?? []), pool]);
  return [...grouped.entries()].map(([id, values]) => {
    const fallback = metadata.get(id);
    const totalPulls = values.reduce((sum, pool) => sum + pool.totalPulls, 0);
    const sixStarCount = values.reduce((sum, pool) => sum + pool.sixStarCount, 0);
    const upCount = values.reduce((sum, pool) => sum + pool.upCount, 0);
    return {
      id,
      name: values.find((pool) => pool.category)?.category || fallback?.name || "其他",
      totalPulls,
      sixStarCount,
      upCount,
      offRateCount: Math.max(0, sixStarCount - upCount),
      upAveragePaidPulls: upCount > 0 ? totalPulls / upCount : null,
    };
  });
}

/** Merge a newer local snapshot into a cloud snapshot without double-counting records. */
export function mergeGachaSnapshots(cloud: GachaWebSnapshot | null, incoming: GachaWebSnapshot): GachaWebSnapshot {
  if (!cloud) return incoming;
  const latest = newer(cloud, incoming);
  const oldByKey = new Map(cloud.pools.map((pool) => [poolKey(pool), pool]));
  const pools: GachaSnapshotPool[] = [];
  const seen = new Set<string>();
  for (const pool of [...incoming.pools, ...cloud.pools]) {
    const key = poolKey(pool);
    if (seen.has(key)) continue;
    seen.add(key);
    const isIncoming = incoming.pools.some((candidate) => poolKey(candidate) === key);
    pools.push(mergePool(oldByKey.get(key), isIncoming ? incoming.pools.find((candidate) => poolKey(candidate) === key)! : pool, isIncoming && latest === incoming));
  }
  return {
    ...latest,
    pools,
    categories: rebuildCategories(pools, cloud.categories, incoming.categories),
  };
}

export async function loadGachaCloudSnapshot(): Promise<GachaWebSnapshot | null> {
  if (!hasToyAbility("getCloudStorage")) throw new Error("当前环境不支持 Toy 云存档");
  const values = await getCloudStorage([MANIFEST_KEY]);
  if (!values[MANIFEST_KEY]) return null;
  let manifest: GachaCloudManifest;
  try { manifest = JSON.parse(values[MANIFEST_KEY]) as GachaCloudManifest; } catch { throw new Error("寻访云存档索引损坏"); }
  if (manifest.version !== VERSION || !Number.isInteger(manifest.parts) || manifest.parts < 1 || manifest.parts > MAX_KEYS) {
    throw new Error("寻访云存档索引版本无效");
  }
  const keys = Array.from({ length: manifest.parts }, (_, index) => `${PART_PREFIX}${index}`);
  const parts = await getCloudStorage(keys);
  if (keys.some((key) => !parts[key])) throw new Error("寻访云存档分片不完整");
  try { return decodeSnapshot(keys.map((key) => parts[key]).join("")); } catch { throw new Error("寻访云存档内容损坏"); }
}

export async function saveGachaCloudSnapshot(incoming: GachaWebSnapshot): Promise<GachaCloudSaveResult> {
  if (!hasToyAbility("setCloudStorage")) throw new Error("当前环境不支持 Toy 云存档");
  const current = await loadGachaCloudSnapshot();
  const merged = mergeGachaSnapshots(current, incoming);
  const encoded = encodeSnapshot(merged);
  const changed = !current || encodeSnapshot(current) !== encoded;
  if (current && !changed) return { snapshot: current, changed: false, parts: Math.ceil(encoded.length / PART_SIZE) };
  const parts = Array.from({ length: Math.ceil(encoded.length / PART_SIZE) }, (_, index) => encoded.slice(index * PART_SIZE, (index + 1) * PART_SIZE));
  const all = await getCloudStorage();
  const oldPartKeys = Object.keys(all).filter((key) => key.startsWith(PART_PREFIX));
  const otherKeyCount = Object.keys(all).filter((key) => key !== MANIFEST_KEY && !key.startsWith(PART_PREFIX)).length;
  if (otherKeyCount + 1 + parts.length > MAX_KEYS) throw new Error(`Toy 云存储空间不足：寻访存档需要 ${parts.length + 1} 个键，当前最多可用 ${MAX_KEYS - otherKeyCount} 个`);
  const writes: Record<string, string> = {};
  parts.forEach((part, index) => { writes[`${PART_PREFIX}${index}`] = part; });
  await setCloudStorage(writes);
  const verification = await getCloudStorage(Object.keys(writes));
  if (parts.some((part, index) => verification[`${PART_PREFIX}${index}`] !== part)) throw new Error("寻访云存档分片校验失败");
  await setCloudStorage({ [MANIFEST_KEY]: JSON.stringify({ version: VERSION, parts: parts.length, updatedAt: merged.createdAt } satisfies GachaCloudManifest) });
  const stale = oldPartKeys.filter((key) => !writes[key]);
  if (stale.length) await removeCloudStorage(stale);
  return { snapshot: merged, changed, parts: parts.length };
}
