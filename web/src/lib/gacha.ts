import { inflateSync, strFromU8 } from "fflate";
import type { GachaWebSnapshot } from "../types";

const PREFIX = "gacha:v";
const CURRENT_SCHEMA_VERSION = 1;
const SNAPSHOT_KIND = "betterendfield.gacha";
const MAX_FRAGMENT_CHARS = 64 * 1024;

function fromBase64Url(value: string): Uint8Array {
  if (!value || value.length > MAX_FRAGMENT_CHARS) throw new Error("寻访快照长度无效");
  const padded = value.replaceAll("-", "+").replaceAll("_", "/") + "=".repeat((4 - value.length % 4) % 4);
  try {
    const binary = atob(padded);
    return Uint8Array.from(binary, (char) => char.charCodeAt(0));
  } catch {
    throw new Error("寻访快照编码无效");
  }
}

function isObject(value: unknown): value is Record<string, unknown> {
  return typeof value === "object" && value !== null;
}

export function decodeGachaSnapshot(fragment: string): GachaWebSnapshot {
  const value = fragment.trim().replace(/^#/, "");
  if (!value.startsWith(PREFIX)) throw new Error("不是寻访快照链接");
  const colon = value.indexOf(":", PREFIX.length);
  if (colon < 0) throw new Error("寻访快照版本格式无效");
  const version = Number(value.slice(PREFIX.length, colon));
  if (!Number.isInteger(version) || version !== CURRENT_SCHEMA_VERSION) {
    throw new Error(`不支持的寻访快照版本：${value.slice(PREFIX.length, colon)}`);
  }
  const json = strFromU8(inflateSync(fromBase64Url(value.slice(colon + 1))));
  let parsed: unknown;
  try { parsed = JSON.parse(json); } catch { throw new Error("寻访快照内容不是有效 JSON"); }
  if (!isObject(parsed) || parsed.schemaVersion !== version || parsed.kind !== SNAPSHOT_KIND ||
      !Array.isArray(parsed.categories) || !Array.isArray(parsed.pools)) {
    throw new Error("寻访快照内容无效");
  }
  return parsed as unknown as GachaWebSnapshot;
}

export function isGachaSnapshotFragment(fragment: string): boolean {
  return fragment.trim().replace(/^#/, "").startsWith(PREFIX);
}
