import { deflateSync, strToU8 } from "fflate";
import { describe, expect, it } from "vitest";
import { decodeGachaSnapshot } from "./gacha";
import { mergeGachaSnapshots } from "./gachaCloud";
import type { GachaWebSnapshot } from "../types";

function base64Url(value: Uint8Array): string {
  let binary = "";
  for (let index = 0; index < value.length; index += 0x8000) binary += String.fromCharCode(...value.subarray(index, index + 0x8000));
  return btoa(binary).replaceAll("+", "-").replaceAll("/", "_").replace(/=+$/, "");
}

describe("gacha snapshot protocol", () => {
  it("decodes a v1 compressed snapshot", () => {
    const snapshot = { schemaVersion: 1, kind: "betterendfield.gacha", createdAt: "2026-09-02T00:00:00Z", categories: [], pools: [] };
    const encoded = `#gacha:v1:${base64Url(deflateSync(strToU8(JSON.stringify(snapshot))))}`;
    expect(decodeGachaSnapshot(encoded)).toEqual(snapshot);
  });

  it("merges cloud and local snapshots by pool and six-star id", () => {
    const base = {
      schemaVersion: 1,
      kind: "betterendfield.gacha",
      createdAt: "2026-09-01T00:00:00Z",
      categories: [],
      pools: [{
        poolId: "p1", poolType: "character", categoryId: "limited", category: "限定", poolName: "旧池",
        totalPulls: 40, freePulls: 0, sixStarCount: 1, upCount: 1, offRateCount: 0,
        startingPity: 0, currentPity: 10, lastSeqId: "10", sixStars: [{ id: "s1", name: "A", itemId: "chr_a", pity: 30, carryPity: 0, isUp: true, isFree: false, seqId: "10", gachaTs: "2026-08-01" }], freeSixStars: [],
      }],
    } as const;
    const local = {
      ...base,
      createdAt: "2026-09-02T00:00:00Z",
      pools: [{ ...base.pools[0], totalPulls: 55, currentPity: 15, lastSeqId: "20", sixStars: [...base.pools[0].sixStars, { id: "s2", name: "B", itemId: "chr_b", pity: 45, carryPity: 0, isUp: false, isFree: false, seqId: "20", gachaTs: "2026-09-02" }], sixStarCount: 2, upCount: 1, offRateCount: 1 }],
    } as const;
    const merged = mergeGachaSnapshots(base as unknown as GachaWebSnapshot, local as unknown as GachaWebSnapshot);
    expect(merged.pools[0].totalPulls).toBe(55);
    expect(merged.pools[0].currentPity).toBe(15);
    expect(merged.pools[0].sixStars).toHaveLength(2);
    expect(merged.categories[0]).toMatchObject({ totalPulls: 55, sixStarCount: 2, upCount: 1, offRateCount: 1 });
  });

  it("rejects an unsupported version before decoding", () => {
    expect(() => decodeGachaSnapshot("#gacha:v2:AAAA")).toThrow("不支持的寻访快照版本");
  });
});
