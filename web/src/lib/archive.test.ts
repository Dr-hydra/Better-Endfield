import { beforeEach, describe, expect, it, vi } from "vitest";

// A stand-in for Toy cloud storage that enforces the platform's real limits:
// 128 keys, 1024 *bytes* per value, restricted key charset.
const store = new Map<string, string>();
const KEY_PATTERN = /^[A-Za-z0-9_-]+$/;

vi.mock("./toy", () => ({
  getCloudStorage: async (keys?: string[]) => {
    const out: Record<string, string> = {};
    for (const [key, value] of store) if (!keys?.length || keys.includes(key)) out[key] = value;
    return out;
  },
  setCloudStorage: async (items: Record<string, string>) => {
    for (const [key, value] of Object.entries(items)) {
      if (!KEY_PATTERN.test(key)) throw new Error(`非法 key: ${key}`);
      if (new TextEncoder().encode(value).length > 1024) throw new Error(`value 超过 1024 字节: ${key}`);
    }
    if (new Set([...store.keys(), ...Object.keys(items)]).size > 128) throw new Error("超过 128 个 key");
    for (const [key, value] of Object.entries(items)) store.set(key, value);
  },
  removeCloudStorage: async (keys: string[]) => { for (const key of keys) store.delete(key); },
}));

const { GACHA_RESERVE, archiveQuota, deleteArchive, estimateArchive, getOwnerToken, listArchives, loadArchive, partsFor, saveArchive } = await import("./archive");
const { createIdSpace, decodeSnapshot } = await import("./bec");
const { parseCombatRecord } = await import("./combat");
import type { CombatDictionary } from "../types";

const dictionary = { c: {}, sk: {}, t: {}, w: {}, eq: {}, su: {}, d: {}, ds: {}, zones: [] } as unknown as CombatDictionary;
const space = createIdSpace(["", "chr_a", "dung_ss02", "chr_a_attack"], 4);

function record(dps = 100) {
  return parseCombatRecord(JSON.stringify({
    schemaVersion: 15,
    battle: { sessionId: 7, startedUnixSeconds: 1757000000, dungeonId: "dung_ss02", durationSeconds: 10 },
    dictionary: { schemaVersion: 1, gameVersion: "dev", hotfixVersion: "", sourceSha256: "t", zones: [] },
    summary: { totalDamage: dps * 10, dps, rdps: dps, hitCount: 1, criticalCount: 0 },
    squad: [{ charId: "chr_a", charInstId: "1", level: 90, breakStage: 5, potential: 0, skillGroups: [], weapon: null, equipments: [], equipSuits: [] }],
    actions: [{ id: 1, start: 1, end: 2, actorId: "chr_a", type: "skillCast", skillId: "chr_a_attack", result: "completed" }],
    effects: [{
      id: 1, actionId: 1, time: 1.5, type: "damage", sourceId: "chr_a", creditedActorId: "chr_a",
      targetId: "enemy", skillId: "chr_a_attack", value: dps * 10, critical: false, damageType: 0, rdpsShares: [],
    }],
  }), dictionary);
}

beforeEach(() => store.clear());

describe("Toy archive", () => {
  it("uses only key names Toy accepts", async () => {
    await getOwnerToken();
    await saveArchive(record(), space, { title: "第一场" });
    expect([...store.keys()].every((key) => KEY_PATTERN.test(key))).toBe(true);
    expect([...store.keys()].some((key) => key.includes(":"))).toBe(false);
  });

  it("round trips a snapshot through cloud storage", async () => {
    const source = record(321);
    const meta = await saveArchive(source, space, { tier: "full", title: "第一场" });
    const decoded = decodeSnapshot(await loadArchive(meta.id), space, source.dictionary);
    expect(decoded.dungeonId).toBe("dung_ss02");
    expect(decoded.dps).toBeCloseTo(source.dps, 1);
    expect(decoded.clips).toHaveLength(1);
    expect(decoded.provenance).toBe("snapshot");
  });

  it("lists newest first and deletes every part", async () => {
    const first = await saveArchive(record(100), space, { title: "旧" });
    await new Promise((resolve) => setTimeout(resolve, 2));
    const second = await saveArchive(record(200), space, { title: "新" });
    const listed = await listArchives();
    expect(listed.map((item) => item.id)).toEqual([second.id, first.id]);

    await deleteArchive(first.id);
    expect((await listArchives()).map((item) => item.id)).toEqual([second.id]);
    expect([...store.keys()].some((key) => key.includes(first.id))).toBe(false);
  });

  it("holds back the gacha reserve", async () => {
    const quota = await archiveQuota();
    expect(quota.total).toBe(128);
    expect(quota.free).toBe(128 - GACHA_RESERVE);

    // Gacha writing its own keys does not shrink the pool twice.
    await import("./toy").then((toy) => toy.setCloudStorage(
      Object.fromEntries(Array.from({ length: 10 }, (_, index) => [`gacha_v1_state_${index}`, "x"])),
    ));
    expect((await archiveQuota()).free).toBe(128 - GACHA_RESERVE);
  });

  it("refuses to save when the reserve would be broken", async () => {
    await import("./toy").then((toy) => toy.setCloudStorage(
      Object.fromEntries(Array.from({ length: 126 }, (_, index) => [`gacha_v1_state_${index}`, "x"])),
    ));
    expect((await archiveQuota()).free).toBe(2);
    await expect(saveArchive(record(), space)).rejects.toThrow(/空间不足/);
  });

  it("estimates the same key count it later consumes", async () => {
    const source = record();
    const estimate = estimateArchive(source, space, { tier: "complete" });
    expect(estimate.parts).toBe(partsFor(estimate.bytes));
    const meta = await saveArchive(source, space, { tier: "complete" });
    expect(meta.parts).toBe(estimate.parts);
    expect([...store.keys()].filter((key) => key.startsWith(`be8_rec_${meta.id}_`))).toHaveLength(meta.parts);
  });
});
