// Toy cloud-storage archive for combat snapshots.
//
// Toy gives a Toy 128 key/value pairs per logged-in user, each value capped at
// 1024 *bytes*. Values are strings, so a binary snapshot rides base64url and
// costs 4/3 of its size — 720 payload bytes per key at PART_CHARS.
//
// The gacha archive shares that same 128-key pool, so this module works against
// a declared reserve rather than assuming it owns everything, and exposes the
// ledger so the UI can tell the user what is left before a save fails.

import { getCloudStorage, removeCloudStorage, setCloudStorage } from "./toy";
import { TIER, encodeSnapshot, type IdSpace, type LaneFilter, type TierName } from "./bec";
import type { CombatRecord } from "../types.combat";

// Toy key charset is [A-Za-z0-9_-]; colons are rejected by the platform.
const OWNER_KEY = "be8_own";
const INDEX_KEY = "be8_idx";
const INDEX_PAGE_PREFIX = "be8_idx_";
const PART_PREFIX = "be8_rec_";

/** base64url characters per value: 960 chars -> 720 payload bytes. */
const PART_CHARS = 960;
const TOTAL_KEYS = 128;
/** Keys held back for the gacha archive (`gacha_v1_*`). */
export const GACHA_RESERVE = 28;
const METAS_PER_PAGE = 8;

export interface ArchiveMeta {
  id: string;
  title: string;
  createdAt: string;
  dungeonId: string;
  durationSeconds: number;
  dps: number;
  tier: TierName;
  parts: number;
  bytes: number;
}

export interface ArchiveQuota {
  total: number;
  /** Keys currently held by the gacha archive and anything else. */
  foreign: number;
  /** Keys held by this module. */
  used: number;
  /** Keys a new combat archive may still claim. */
  free: number;
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
  const binary = atob(padded);
  return Uint8Array.from(binary, (char) => char.charCodeAt(0));
}

export function encodeArchiveBytes(bytes: Uint8Array): string {
  return toBase64Url(bytes);
}

export function decodeArchiveBytes(value: string): Uint8Array {
  return fromBase64Url(value);
}

/** Keys a payload of this size will occupy, excluding the shared index. */
export function partsFor(bytes: number): number {
  return Math.ceil(toBase64Url(new Uint8Array(bytes)).length / PART_CHARS);
}

function randomToken(): string {
  return toBase64Url(crypto.getRandomValues(new Uint8Array(32)));
}

function isOwnKey(key: string): boolean {
  return key === OWNER_KEY || key === INDEX_KEY
    || key.startsWith(INDEX_PAGE_PREFIX) || key.startsWith(PART_PREFIX);
}

function quotaFrom(all: Record<string, string>): ArchiveQuota {
  const keys = Object.keys(all);
  const used = keys.filter(isOwnKey).length;
  const foreign = keys.length - used;
  // The gacha reserve is honoured even before gacha has written anything, so a
  // combat archive can never squeeze it out.
  const free = Math.max(0, TOTAL_KEYS - used - Math.max(foreign, GACHA_RESERVE));
  return { total: TOTAL_KEYS, foreign, used, free };
}

export async function archiveQuota(): Promise<ArchiveQuota> {
  return quotaFrom(await getCloudStorage());
}

export async function getOwnerToken(): Promise<string> {
  const existing = await getCloudStorage([OWNER_KEY]);
  if (existing[OWNER_KEY]) return existing[OWNER_KEY];
  const token = randomToken();
  await setCloudStorage({ [OWNER_KEY]: token });
  return token;
}

function readMetas(all: Record<string, string>): ArchiveMeta[] {
  const pages = Object.keys(all)
    .filter((key) => key.startsWith(INDEX_PAGE_PREFIX))
    .sort((left, right) => Number(left.slice(INDEX_PAGE_PREFIX.length)) - Number(right.slice(INDEX_PAGE_PREFIX.length)));
  const out: ArchiveMeta[] = [];
  for (const key of pages) {
    try {
      const parsed = JSON.parse(all[key]) as ArchiveMeta[];
      if (Array.isArray(parsed)) out.push(...parsed);
    } catch {
      // A damaged page loses its entries but must not hide the rest.
    }
  }
  return out;
}

async function writeIndex(metas: ArchiveMeta[], all: Record<string, string>) {
  const pages: ArchiveMeta[][] = [];
  for (let index = 0; index < metas.length; index += METAS_PER_PAGE) {
    pages.push(metas.slice(index, index + METAS_PER_PAGE));
  }
  const writes: Record<string, string> = {
    [INDEX_KEY]: JSON.stringify({ version: 2, pages: pages.length }),
  };
  pages.forEach((page, index) => { writes[`${INDEX_PAGE_PREFIX}${index}`] = JSON.stringify(page); });
  const oversize = Object.entries(writes).find(([, value]) => new TextEncoder().encode(value).length > 1024);
  if (oversize) throw new Error(`云存档索引页 ${oversize[0]} 超过 1024 字节，请减少存档数量`);
  await setCloudStorage(writes);
  const stale = Object.keys(all).filter((key) => {
    if (!key.startsWith(INDEX_PAGE_PREFIX)) return false;
    const index = Number(key.slice(INDEX_PAGE_PREFIX.length));
    return Number.isInteger(index) && index >= pages.length;
  });
  if (stale.length) await removeCloudStorage(stale);
}

export async function listArchives(): Promise<ArchiveMeta[]> {
  const all = await getCloudStorage();
  return readMetas(all).sort((left, right) => right.createdAt.localeCompare(left.createdAt));
}

export interface SaveOptions {
  tier?: TierName;
  lanes?: LaneFilter;
  title?: string;
}

/** Bytes and keys a record would cost, so the UI can ask before it writes. */
export function estimateArchive(record: CombatRecord, space: IdSpace, options: SaveOptions = {}) {
  const bytes = encodeSnapshot(record, space, { tier: options.tier ?? "full", lanes: options.lanes }).byteLength;
  return { bytes, parts: partsFor(bytes) };
}

export async function saveArchive(
  record: CombatRecord,
  space: IdSpace,
  options: SaveOptions = {},
): Promise<ArchiveMeta> {
  const tier = options.tier ?? "full";
  const snapshot = encodeSnapshot(record, space, { tier, lanes: options.lanes });
  const encoded = encodeArchiveBytes(snapshot);
  const parts = Array.from(
    { length: Math.ceil(encoded.length / PART_CHARS) },
    (_, index) => encoded.slice(index * PART_CHARS, (index + 1) * PART_CHARS),
  );

  const all = await getCloudStorage();
  const quota = quotaFrom(all);
  const metas = readMetas(all);
  const pagesBefore = Math.ceil(metas.length / METAS_PER_PAGE);
  const pagesAfter = Math.ceil((metas.length + 1) / METAS_PER_PAGE);
  const required = parts.length + (pagesAfter - pagesBefore) + (all[INDEX_KEY] ? 0 : 1) + (all[OWNER_KEY] ? 0 : 1);
  if (required > quota.free) {
    throw new Error(
      `Toy 云存储空间不足：本次需要 ${required} 片，当前可用 ${quota.free} 片`
      + `（共 ${TOTAL_KEYS} 片，寻访记录预留 ${GACHA_RESERVE} 片）。请先删除旧存档或改存较小的档位。`,
    );
  }

  const id = `${Date.now().toString(36)}${crypto.getRandomValues(new Uint16Array(1))[0].toString(36)}`;
  const writes: Record<string, string> = {};
  parts.forEach((part, index) => { writes[`${PART_PREFIX}${id}_${index}`] = part; });
  await setCloudStorage(writes);
  // Toy writes can partially succeed; verify before the index points at them.
  const verification = await getCloudStorage(Object.keys(writes));
  if (parts.some((part, index) => verification[`${PART_PREFIX}${id}_${index}`] !== part)) {
    await removeCloudStorage(Object.keys(writes));
    throw new Error("云存档分片校验失败，未提交索引");
  }

  const meta: ArchiveMeta = {
    id,
    title: options.title || record.dungeonId || "战斗记录",
    createdAt: new Date().toISOString(),
    dungeonId: record.dungeonId,
    durationSeconds: Math.round(record.durationSeconds * 10) / 10,
    dps: Math.round(record.dps),
    tier,
    parts: parts.length,
    bytes: snapshot.byteLength,
  };
  await writeIndex([meta, ...metas], all);
  return meta;
}

export async function loadArchive(id: string): Promise<Uint8Array> {
  const all = await getCloudStorage();
  const meta = readMetas(all).find((item) => item.id === id);
  if (!meta) throw new Error("云存档不存在或已被删除");
  const keys = Array.from({ length: meta.parts }, (_, index) => `${PART_PREFIX}${id}_${index}`);
  const missing = keys.filter((key) => !all[key]);
  if (missing.length) throw new Error(`云存档分片不完整，缺少 ${missing.length} 片`);
  return decodeArchiveBytes(keys.map((key) => all[key]).join(""));
}

export async function deleteArchive(id: string): Promise<void> {
  const all = await getCloudStorage();
  const metas = readMetas(all);
  const meta = metas.find((item) => item.id === id);
  // Sweep by prefix rather than by the recorded count, so a half-written
  // archive with a stale part count still cleans up completely.
  const keys = Object.keys(all).filter((key) => key.startsWith(`${PART_PREFIX}${id}_`));
  if (meta) {
    for (let index = 0; index < meta.parts; index += 1) {
      const key = `${PART_PREFIX}${id}_${index}`;
      if (!keys.includes(key)) keys.push(key);
    }
  }
  if (keys.length) await removeCloudStorage(keys);
  await writeIndex(metas.filter((item) => item.id !== id), all);
}

export const ARCHIVE_TIERS: Array<{ id: TierName; name: string; hint: string }> = [
  { id: "card", name: "名片", hint: "队伍、伤害构成、增益归因与 DPS 曲线" },
  { id: "summary", name: "摘要", hint: "名片加技能统计" },
  { id: "full", name: "完整", hint: "加动作轨道与增益轨道，不含逐次命中" },
  { id: "complete", name: "全量", hint: "再加每一次命中" },
];

export { TIER };
