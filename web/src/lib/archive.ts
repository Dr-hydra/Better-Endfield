import { deflateSync, inflateSync, strFromU8, strToU8 } from "fflate";
import { getCloudStorage, removeCloudStorage, setCloudStorage } from "./toy";

const OWNER_KEY = "be8:owner";
const INDEX_KEY = "be8:index";
const INDEX_PAGE_PREFIX = "be8:index:";
const META_PREFIX = "be8:m:";
const PART_PREFIX = "be8:p:";
const PART_SIZE = 960;
const MAX_KEYS = 128;
const IDS_PER_PAGE = 48;

export interface ArchiveMeta {
  id: string;
  title: string;
  createdAt: string;
  dungeonId: string;
  durationSeconds: number;
  dps: number;
  parts: number;
  bytes: number;
  digest: string;
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

export function encodeArchiveText(text: string): string {
  return toBase64Url(deflateSync(strToU8(text), { level: 9 }));
}

export function decodeArchiveText(value: string): string {
  return strFromU8(inflateSync(fromBase64Url(value)));
}

async function digest(value: Uint8Array): Promise<string> {
  const stable = new Uint8Array(value).buffer;
  const hash = await crypto.subtle.digest("SHA-256", stable);
  return [...new Uint8Array(hash)].map((byte) => byte.toString(16).padStart(2, "0")).join("");
}

function randomToken(): string {
  return toBase64Url(crypto.getRandomValues(new Uint8Array(32)));
}

export async function getOwnerToken(): Promise<string> {
  const existing = await getCloudStorage([OWNER_KEY]);
  if (existing[OWNER_KEY]) return existing[OWNER_KEY];
  const token = randomToken();
  await setCloudStorage({ [OWNER_KEY]: token });
  return token;
}

async function writeIndex(ids: string[], current: Record<string, string>) {
  const pages: string[][] = [];
  for (let index = 0; index < ids.length; index += IDS_PER_PAGE) {
    pages.push(ids.slice(index, index + IDS_PER_PAGE));
  }
  const writes: Record<string, string> = {
    [INDEX_KEY]: JSON.stringify({ version: 1, pages: pages.length }),
  };
  pages.forEach((page, index) => {
    writes[`${INDEX_PAGE_PREFIX}${index}`] = JSON.stringify(page);
  });
  await setCloudStorage(writes);
  const stale = Object.keys(current).filter((key) => {
    if (!key.startsWith(INDEX_PAGE_PREFIX)) return false;
    const index = Number(key.slice(INDEX_PAGE_PREFIX.length));
    return Number.isInteger(index) && index >= pages.length;
  });
  if (stale.length) await removeCloudStorage(stale);
}

export async function listArchives(): Promise<ArchiveMeta[]> {
  const all = await getCloudStorage();
  return Object.entries(all)
    .filter(([key]) => key.startsWith(META_PREFIX))
    .flatMap(([, value]) => {
      try {
        return [JSON.parse(value) as ArchiveMeta];
      } catch {
        return [];
      }
    })
    .sort((left, right) => right.createdAt.localeCompare(left.createdAt));
}

export async function saveArchive(
  text: string,
  summary: Pick<ArchiveMeta, "title" | "dungeonId" | "durationSeconds" | "dps">,
): Promise<ArchiveMeta> {
  const source = strToU8(text);
  const encoded = encodeArchiveText(text);
  const parts = Array.from(
    { length: Math.ceil(encoded.length / PART_SIZE) },
    (_, index) => encoded.slice(index * PART_SIZE, (index + 1) * PART_SIZE),
  );
  const all = await getCloudStorage();
  const id = `${Date.now().toString(36)}${crypto.getRandomValues(new Uint16Array(1))[0].toString(36)}`;
  const meta: ArchiveMeta = {
    id,
    ...summary,
    createdAt: new Date().toISOString(),
    parts: parts.length,
    bytes: source.byteLength,
    digest: await digest(source),
  };
  const currentIds = Object.keys(all)
    .filter((key) => key.startsWith(META_PREFIX))
    .map((key) => key.slice(META_PREFIX.length));
  const pageCount = Math.ceil((currentIds.length + 1) / IDS_PER_PAGE);
  const requiredKeys = parts.length + 1 + pageCount + 1;
  const availableKeys = MAX_KEYS - Object.keys(all).length;
  if (requiredKeys > availableKeys) {
    throw new Error(`Toy 云存储空间不足：本记录需要 ${requiredKeys} 个键，当前仅剩 ${availableKeys} 个。请先手动删除旧记录。`);
  }
  const writes: Record<string, string> = {};
  parts.forEach((part, index) => {
    writes[`${PART_PREFIX}${id}:${index}`] = part;
  });
  await setCloudStorage(writes);
  const verification = await getCloudStorage(Object.keys(writes));
  if (parts.some((part, index) => verification[`${PART_PREFIX}${id}:${index}`] !== part)) {
    await removeCloudStorage(Object.keys(writes));
    throw new Error("云存档分片校验失败，未提交索引");
  }
  await setCloudStorage({ [`${META_PREFIX}${id}`]: JSON.stringify(meta) });
  await writeIndex([id, ...currentIds], { ...all, ...writes });
  return meta;
}

export async function loadArchive(id: string): Promise<string> {
  const metaValue = await getCloudStorage([`${META_PREFIX}${id}`]);
  if (!metaValue[`${META_PREFIX}${id}`]) throw new Error("云存档不存在或已被删除");
  const meta = JSON.parse(metaValue[`${META_PREFIX}${id}`]) as ArchiveMeta;
  const keys = Array.from({ length: meta.parts }, (_, index) => `${PART_PREFIX}${id}:${index}`);
  const parts = await getCloudStorage(keys);
  const encoded = keys.map((key) => parts[key] ?? "").join("");
  if (!encoded || keys.some((key) => !parts[key])) throw new Error("云存档分片不完整");
  const text = decodeArchiveText(encoded);
  if (await digest(strToU8(text)) !== meta.digest) throw new Error("云存档摘要校验失败");
  return text;
}

export async function deleteArchive(id: string): Promise<void> {
  const all = await getCloudStorage();
  let parts = 0;
  try {
    parts = (JSON.parse(all[`${META_PREFIX}${id}`]) as ArchiveMeta).parts;
  } catch {
    // A damaged metadata record is still removable.
  }
  const keys = [`${META_PREFIX}${id}`];
  for (let index = 0; index < parts; index += 1) keys.push(`${PART_PREFIX}${id}:${index}`);
  await removeCloudStorage(keys);
  const remaining = Object.keys(all)
    .filter((key) => key.startsWith(META_PREFIX) && key !== `${META_PREFIX}${id}`)
    .map((key) => key.slice(META_PREFIX.length));
  await writeIndex(remaining, all);
}
