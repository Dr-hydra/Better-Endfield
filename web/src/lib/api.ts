// Leaderboard backend client.
//
// Reads and writes take different roads. Boards and records are static objects
// in a public bucket, loaded straight from the CDN with a <script> tag (see
// jsonp.ts) — no cloud function, no gateway, no database on that path. Only
// uploading, deleting and the two toggles go through the HTTP function, and
// those are rare.
//
// The wire rows are the backend's compact shape; they are widened here so the
// rest of the app keeps seeing LeaderboardEntry.

import { decodeSnapshot, encodeSnapshot, type TierName } from "./bec";
import { dictionary, ensureIdSpace } from "./dict";
import { loadJsonp } from "./jsonp";
import type { CombatRecord } from "../types.combat";
import type { BoardPayload, CategoryBoard, LeaderboardEntry, PublicRecord } from "../types";

const API_BASE = (import.meta.env.VITE_CLOUDBASE_API as string | undefined)?.replace(/\/$/, "") ?? "";
const STORAGE_BASE = (import.meta.env.VITE_COMBAT_STORAGE as string | undefined)?.replace(/\/$/, "") ?? "";

/** Boards can be read without an upload endpoint; uploading needs both. */
export const boardConfigured = Boolean(STORAGE_BASE);
export const apiConfigured = Boolean(API_BASE && STORAGE_BASE);

/** Shares per user; the backend enforces the same number. */
export const MAX_SHARES = 20;

function endpoint(path: string): string {
  if (!API_BASE) throw new Error("尚未配置 CloudBase API 地址");
  return `${API_BASE}${path}`;
}

async function responseJson<T>(response: Response): Promise<T> {
  const payload = (await response.json().catch(() => ({}))) as { message?: string } & T;
  if (!response.ok) throw new Error(payload.message || `请求失败（${response.status}）`);
  return payload;
}

function fromBase64Url(value: string): Uint8Array {
  const padded = value.replaceAll("-", "+").replaceAll("_", "/") + "=".repeat((4 - value.length % 4) % 4);
  return Uint8Array.from(atob(padded), (char) => char.charCodeAt(0));
}

function toBase64Url(bytes: Uint8Array): string {
  let binary = "";
  for (let index = 0; index < bytes.length; index += 0x8000) {
    binary += String.fromCharCode(...bytes.subarray(index, index + 0x8000));
  }
  return btoa(binary).replaceAll("+", "-").replaceAll("/", "_").replace(/=+$/, "");
}

// ---------------------------------------------------------------------------
// wire shapes

interface WireRow {
  i: string; d: string; n: string; a: string; b: string;
  du: number; dp: number; rd: number; dm: number; hc: number;
  ly: number; iv: number; sq: Array<[string, number, number]>; t: number;
  /** Owner-only: board opt-in and the moment it fell off every board. */
  x?: boolean; f?: number; c?: string;
}

function widen(row: WireRow, rank: number): LeaderboardEntry {
  return {
    shortId: row.i,
    rank,
    nickname: row.n,
    avatar: row.a || "",
    bvid: row.b || "",
    durationSeconds: row.du,
    dps: row.dp,
    rdps: row.rd,
    totalDamage: row.dm,
    uploadedAt: new Date(row.t).toISOString(),
    dungeonId: row.d,
    layers: row.ly,
    idSpaceVersion: row.iv,
    squad: (row.sq ?? []).map(([charId, level, potential]) => ({ charId, level, potential })),
    optIn: row.x !== false,
    ranked: !row.f,
    expireAt: row.f ? new Date(row.f).toISOString() : null,
  };
}

const widenAll = (rows: WireRow[] = []) => rows.map((row, index) => widen(row, index + 1));

// ---------------------------------------------------------------------------
// reads

/** Per-category record counts; tells the nav which tabs have anything in them. */
export async function getIndex(): Promise<Record<string, number>> {
  const payload = await loadJsonp<{ c?: Record<string, number> }>(STORAGE_BASE, "index", { freshness: 60_000 });
  return payload?.c ?? {};
}

/** Category overview: every stage with records, three names each. */
export async function getCategoryBoard(categoryId: string): Promise<CategoryBoard> {
  const payload = await loadJsonp<{
    t?: number;
    s?: Record<string, { e: number; dps: WireRow[]; time: WireRow[] }>;
  }>(STORAGE_BASE, `cat/${categoryId}`);
  const stages: CategoryBoard["stages"] = {};
  for (const [dungeonId, stage] of Object.entries(payload?.s ?? {})) {
    stages[dungeonId] = { entries: stage.e, dps: widenAll(stage.dps), time: widenAll(stage.time) };
  }
  return { categoryId, updatedAt: payload?.t ?? 0, stages };
}

/** One stage, the full twenty on both metrics. */
export async function getBoard(dungeonId: string): Promise<BoardPayload | null> {
  const payload = await loadJsonp<{ e: number; t: number; dps: WireRow[]; time: WireRow[] }>(
    STORAGE_BASE, `stage/${dungeonId}`,
  );
  if (!payload) return null;
  return {
    dungeonId,
    entries: payload.e,
    updatedAt: new Date(payload.t).toISOString(),
    dps: widenAll(payload.dps),
    time: widenAll(payload.time),
  };
}

/**
 * Public records decode with the *shipped* id space. A snapshot written against
 * a newer export still decodes — ids past the local table resolve to "" rather
 * than to the wrong entity, because the registry only ever appends.
 */
export async function getPublicRecord(shortId: string): Promise<{ record: CombatRecord; meta: PublicRecord }> {
  const payload = await loadJsonp<{
    i: string; d: string; n: string; a: string; b: string; ly: number; iv: number; t: number; p: string;
  }>(STORAGE_BASE, `rec/${shortId}`, { freshness: 300_000 });
  if (!payload) throw new Error("记录不存在或已过期");
  const record = decodeSnapshot(fromBase64Url(payload.p), await ensureIdSpace(), {
    schemaVersion: 1, gameVersion: "", hotfixVersion: "", sourceSha256: "",
  }, dictionary.zones as never);
  return {
    record,
    meta: {
      shortId: payload.i,
      dungeonId: payload.d,
      nickname: payload.n,
      avatar: payload.a || "",
      bvid: payload.b || "",
      layers: payload.ly,
      idSpaceVersion: payload.iv,
      uploadedAt: new Date(payload.t).toISOString(),
      ranked: true,
    },
  };
}

// ---------------------------------------------------------------------------
// writes

export interface PublishArgs {
  record: CombatRecord;
  ownerToken: string;
  nickname: string;
  avatar?: string;
  toyOpenId?: string;
  bvid?: string;
  tier?: TierName;
  /** Sharing joins the board unless this is explicitly false. */
  optIn?: boolean;
}

export async function publishRecord(args: PublishArgs): Promise<{ shortId: string; categoryId: string }> {
  const snapshot = encodeSnapshot(args.record, await ensureIdSpace(), { tier: args.tier ?? "complete" });
  const response = await fetch(endpoint("/records"), {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({
      payload: toBase64Url(snapshot),
      ownerToken: args.ownerToken,
      nickname: args.nickname,
      avatar: args.avatar,
      bvid: args.bvid,
      optIn: args.optIn !== false,
      dungeonId: args.record.dungeonId,
      squad: args.record.squad.map((member) => ({
        charId: member.charId, level: member.level, potential: member.potential,
      })),
    }),
  });
  return responseJson(response);
}

export async function myRecords(ownerToken: string): Promise<LeaderboardEntry[]> {
  const response = await fetch(endpoint(`/me/records?ownerToken=${encodeURIComponent(ownerToken)}`));
  const payload = await responseJson<{ records: WireRow[] }>(response);
  return payload.records.map((row, index) => widen(row, index + 1));
}

export async function updateRecordVideo(shortId: string, ownerToken: string, bvid: string) {
  const response = await fetch(endpoint(`/records/${encodeURIComponent(shortId)}/video`), {
    method: "PATCH",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ ownerToken, bvid }),
  });
  return responseJson<{ ok: true }>(response);
}

/** Leave or rejoin the board without deleting the share link. */
export async function setRecordRanked(shortId: string, ownerToken: string, optIn: boolean) {
  const response = await fetch(endpoint(`/records/${encodeURIComponent(shortId)}/ranked`), {
    method: "PATCH",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ ownerToken, optIn }),
  });
  return responseJson<{ ok: true }>(response);
}

export async function deletePublicRecord(shortId: string, ownerToken: string) {
  const response = await fetch(endpoint(`/records/${encodeURIComponent(shortId)}`), {
    method: "DELETE",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ ownerToken }),
  });
  return responseJson<{ ok: true }>(response);
}
