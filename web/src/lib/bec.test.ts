import { existsSync, readFileSync } from "node:fs";
import { resolve } from "node:path";
import { describe, expect, it } from "vitest";
import { LAYER, TIER, createIdSpace, decodeSnapshot, encodeSnapshot, readSnapshotMeta } from "./bec";
import { SUPPORTED_SCHEMA, parseCombatRecord } from "./combat";
import type { CombatDictionary } from "../types";
import type { CombatRecord } from "../types.combat";

const dictionary = { c: {}, sk: {}, t: {}, w: {}, eq: {}, su: {}, d: {}, ds: {}, zones: [] } as unknown as CombatDictionary;

function record(): CombatRecord {
  return parseCombatRecord(JSON.stringify({
    schemaVersion: 15,
    battle: { sessionId: 29517680673600, startedUnixSeconds: 1757000000, dungeonId: "dung_ss02", durationSeconds: 12.5 },
    dictionary: { schemaVersion: 1, gameVersion: "dev", hotfixVersion: "", sourceSha256: "test", zones: [] },
    summary: { totalDamage: 3000, dps: 240, rdps: 240, hitCount: 2, criticalCount: 1 },
    squad: [
      {
        charId: "chr_test", charInstId: "2305843009213694000", level: 90, breakStage: 5, potential: 2,
        skillGroups: [{ skillId: "chr_test_attack", level: 9, maxLevel: 12 }],
        weapon: { templateId: "wpn_lance_0014", level: 80, refineLevel: 3, breakthroughLevel: 4, skills: [] },
        equipments: [{ slot: 0, templateId: "item_equip_t4_a", enhanceLevels: { "1": 3, "2": 2 } }],
        equipSuits: [{ suitId: "suit_atk01", count: 3, skills: [] }],
      },
      {
        charId: "chr_support", charInstId: "2305843009213694001", level: 90, breakStage: 5, potential: 0,
        skillGroups: [], weapon: null, equipments: [], equipSuits: [],
      },
    ],
    actions: [
      { id: 1, start: 1, end: 1.5, actorId: "chr_test", type: "skillCast", skillId: "chr_test_attack", result: "completed" },
      { id: 2, start: 4, end: 4.4, actorId: "chr_test", type: "dodge", result: "completed" },
    ],
    effects: [
      {
        id: 1, actionId: 1, time: 1.2, type: "damage", sourceId: "chr_test",
        creditedActorId: "chr_test", targetId: "enemy", skillId: "chr_test_attack",
        value: 1000, critical: false, damageType: 2, rdpsShares: [],
        ledger: {
          originSkillId: "chr_test_attack", calcResult: 900, atkScale: 1.5,
          attackerZones: [1.1], defenderZones: [1],
          factors: {
            valid: true, attack: 600, attackLinked: true, zoneProduct: 1.1, defense: 100,
            defResistance: 0.5, typeResistance: 0.9, critical: false, critRate: 0.05,
            critDamage: 0.5, weakness: 1, shelter: 0, ignite: 1, igniteApplies: false,
          },
          externalFraction: 0.2, selfFraction: 0, zoneWrites: [], zoneWritesDropped: 0,
          attributeGroups: [], attributeWrites: [], staleMapBuffs: 0,
          contributors: [{
            statusInstanceId: "77", statusId: "buff_support_atkup", sourceId: "chr_support",
            ownerId: "chr_test", mechanism: "attribute", contributionType: 1,
            contributionKind: "attack", semantic: "verified", side: 0, attributeType: 2,
            zoneIndex: -1, factor: 0.8, fraction: 0.2, external: true, excluded: false,
          }],
        },
      },
      {
        id: 2, time: 6.5, type: "damage", sourceId: "chr_test", creditedActorId: "chr_test",
        targetId: "enemy", skillId: "chr_test_attack", value: 2000, critical: true,
        damageType: 2, rdpsShares: [],
      },
      {
        id: 3, time: 0.5, type: "statusApply", sourceId: "chr_support", creditedActorId: "chr_support",
        targetId: "chr_test", targetEntityId: "chr_test_4635227125512945664",
        statusId: "buff_support_atkup", statusInstanceId: "77", statusKind: "buff",
        contributionType: 1, contributionKind: "attack", sourceKind: "characterSkill",
        sourceTemplateId: "chr_support", sourceSkillId: "chr_support_skill",
        stacking: { type: 2, typeId: "stack", maxStack: 4, enhanceCount: 0, keyed: false, key: "" },
        nominalEffects: [
          { attributeType: 2, slot: 6, min: 180, max: 240 },
          { attributeType: 3, slot: 5, min: -0.25, max: -0.25 },
        ],
      },
      { id: 4, time: 8, type: "statusRemove", sourceId: "chr_support", targetId: "chr_test", statusId: "buff_support_atkup", statusInstanceId: "77" },
    ],
  }), dictionary);
}

const space = createIdSpace([
  "", "chr_test", "chr_support", "dung_ss02", "chr_test_attack", "chr_support_skill",
  "wpn_lance_0014", "item_equip_t4_a", "suit_atk01", "buff_support_atkup",
], 10);

describe("BEC snapshot round trip", () => {
  it("keeps the head and squad through a full-tier round trip", () => {
    const source = record();
    const decoded = decodeSnapshot(encodeSnapshot(source, space, { tier: "full" }), space, source.dictionary);

    expect(decoded.provenance).toBe("snapshot");
    expect(decoded.sessionId).toBe(source.sessionId);
    expect(decoded.dungeonId).toBe("dung_ss02");
    expect(decoded.durationSeconds).toBeCloseTo(source.durationSeconds, 2);
    // Damage rides a varint, so it lands on the nearest integer.
    expect(decoded.totalDamage).toBe(Math.round(source.totalDamage));
    expect(decoded.hitCount).toBe(source.hitCount);
    expect(decoded.squad).toHaveLength(2);
    expect(decoded.squad[0].charId).toBe("chr_test");
    expect(decoded.squad[0].charInstId).toBe("2305843009213694000");
    expect(decoded.squad[0].weapon?.templateId).toBe("wpn_lance_0014");
    expect(decoded.squad[0].weapon?.refineLevel).toBe(3);
    expect(decoded.squad[0].equipments[0].enhanceLevels).toEqual({ "1": 3, "2": 2 });
    expect(decoded.squad[0].equipSuits[0]).toMatchObject({ suitId: "suit_atk01", count: 3 });
  });

  it("keeps clips, lanes and per-character aggregates", () => {
    const source = record();
    const decoded = decodeSnapshot(encodeSnapshot(source, space, { tier: "full" }), space, source.dictionary);

    expect(decoded.clips.map((clip) => clip.skillId)).toEqual(source.clips.map((clip) => clip.skillId));
    expect(decoded.clips[0].start).toBeCloseTo(source.clips[0].start, 2);
    expect(decoded.clips[0].end).toBeCloseTo(source.clips[0].end, 2);
    expect(decoded.clips[1].type).toBe("dodge");

    expect(decoded.buffIntervals).toHaveLength(source.buffIntervals.length);
    const lane = decoded.buffIntervals[0];
    const original = source.buffIntervals[0];
    expect(lane.buffId).toBe(original.buffId);
    expect(lane.start).toBeCloseTo(original.start, 2);
    expect(lane.end).toBeCloseTo(original.end, 2);
    expect(lane.credited).toBe(Math.round(original.credited));
    expect(lane.sourceKind).toBe("characterSkill");
    expect(lane.stacking?.type).toBe(2);
    expect(lane.stacking?.maxStack).toBe(4);
    // Nominal magnitudes ride BUFFDEF at 1/10000, and the second one is
    // negative -- a plain fixed would have clamped it to zero.
    expect(lane.nominalEffects).toEqual([
      { attributeType: 2, slot: 6, min: 180, max: 240 },
      { attributeType: 3, slot: 5, min: -0.25, max: -0.25 },
    ]);

    expect(Object.keys(decoded.characters)).toEqual(Object.keys(source.characters));
    expect(decoded.characters.chr_test.damage).toBe(source.characters.chr_test.damage);
    expect(Object.keys(decoded.rdpsCharacters)).toEqual(Object.keys(source.rdpsCharacters));
    for (const [charId, value] of Object.entries(source.rdpsCharacters)) {
      expect(decoded.rdpsCharacters[charId].rdpsByContribution)
        .toEqual(value.rdpsByContribution.map(Math.round));
    }
  });

  it("keeps the contributor table the attribution panel renders", () => {
    const source = record();
    const decoded = decodeSnapshot(encodeSnapshot(source, space, { tier: "full" }), space, source.dictionary);

    expect(decoded.contributors).toHaveLength(source.contributors.length);
    const row = decoded.contributors[0];
    const original = source.contributors[0];
    expect(row.buffId).toBe(original.buffId);
    expect(row.sourceId).toBe(original.sourceId);
    expect(row.contributionType).toBe(original.contributionType);
    expect(row.contributionKind).toBe(original.contributionKind);
    expect(row.mechanism).toBe(original.mechanism);
    expect(row.credited).toBe(Math.round(original.credited));
    expect(row.meanMultiplier).toBeCloseTo(original.meanMultiplier, 3);
    expect(row.owners).toEqual(original.owners);
  });

  it("carries every hit at the complete tier and none below it", () => {
    const source = record();
    const complete = decodeSnapshot(encodeSnapshot(source, space, { tier: "complete" }), space, source.dictionary);
    expect(complete.events).toHaveLength(source.events.length);
    expect(complete.events[0].damage).toBe(source.events[0].damage);
    expect(complete.events[1].critical).toBe(true);
    // Clips reclaim their hits by time span so the inspector still lists them.
    expect(complete.clips[0].hits).toHaveLength(1);

    const full = decodeSnapshot(encodeSnapshot(source, space, { tier: "full" }), space, source.dictionary);
    expect(full.events).toHaveLength(0);
  });

  it("reports its layers so the UI can hide what is missing", () => {
    const source = record();
    const card = encodeSnapshot(source, space, { tier: "card" });
    const meta = readSnapshotMeta(card);
    expect(meta.idSpaceVersion).toBe(10);
    expect(meta.layers & LAYER.SPARK).toBeTruthy();
    expect(meta.layers & LAYER.HITS).toBeFalsy();

    const decoded = decodeSnapshot(card, space, source.dictionary);
    expect(decoded.spark?.length).toBeGreaterThan(0);
    expect(decoded.spark?.[0].values).toHaveLength(96);
    expect(decoded.clips).toHaveLength(0);
    expect(decoded.contributors.length).toBe(source.contributors.length);
  });

  it("gets smaller as the tier drops", () => {
    const source = record();
    const sizes = (["card", "summary", "full", "complete"] as const)
      .map((tier) => encodeSnapshot(source, space, { tier }).byteLength);
    expect(sizes[0]).toBeLessThan(sizes[2]);
    expect(sizes[2]).toBeLessThan(sizes[3]);
    expect(TIER.complete & LAYER.HITS).toBeTruthy();
  });
});

// The synthetic fixture cannot cover attribution at scale; when the scratch
// sample is present, round trip the real thing.
//
// Every schema bump so far has been additive, so a sample recorded against an
// older one still exercises the codec at full size -- it just carries none of
// the newer fields. The version is rewritten rather than the sample re-recorded
// because re-recording means playing the game.
const samplePaths = [15, 14].map((schema) =>
  resolve(__dirname, `../../../tmp_analysis/sample-${schema}.json`));
const samplePath = samplePaths.find((path) => existsSync(path));
describe.skipIf(!samplePath)("BEC against the real record", () => {
  it("reproduces the analysed model within quantisation error", () => {
    const raw = readFileSync(samplePath!, "utf8")
      .replace(/"schemaVersion":\s*\d+/, `"schemaVersion":${SUPPORTED_SCHEMA}`);
    const source = parseCombatRecord(raw, dictionary);
    const ids = new Set<string>([""]);
    for (const member of source.squad) {
      ids.add(member.charId);
      if (member.weapon) ids.add(member.weapon.templateId);
      for (const item of member.equipments) ids.add(item.templateId);
      for (const suit of member.equipSuits) ids.add(suit.suitId);
      for (const skill of member.skillGroups) ids.add(skill.skillId ?? skill.skillGroupId ?? "");
    }
    ids.add(source.dungeonId);
    for (const clip of source.clips) ids.add(clip.skillId);
    for (const event of source.events) ids.add(event.skillId);
    for (const lane of source.buffIntervals) {
      ids.add(lane.buffId); ids.add(lane.sourceId); ids.add(lane.ownerId);
      ids.add(lane.sourceTemplateId); ids.add(lane.sourceSkillId); ids.add(lane.originSkillId);
    }
    for (const row of source.contributors) {
      ids.add(row.buffId); ids.add(row.sourceId); ids.add(row.sourceTemplateId); ids.add(row.sourceSkillId);
      for (const owner of row.owners) ids.add(owner);
    }
    for (const item of source.staggerIntervals) { ids.add(item.buffId); ids.add(item.targetId); }
    for (const skillId of Object.keys(source.skills)) ids.add(skillId);
    const realSpace = createIdSpace([...ids], 1);

    // `all` keeps every lane, so the round trip is exact.
    const encoded = encodeSnapshot(source, realSpace, { tier: "complete", lanes: "all" });
    const decoded = decodeSnapshot(encoded, realSpace, source.dictionary);

    expect(decoded.clips).toHaveLength(source.clips.length);
    expect(decoded.events).toHaveLength(source.events.length);
    expect(decoded.buffIntervals).toHaveLength(source.buffIntervals.length);
    expect(decoded.contributors).toHaveLength(source.contributors.length);
    // Damage rides a varint, so it lands on the nearest integer.
    expect(decoded.totalDamage).toBe(Math.round(source.totalDamage));
    expect(decoded.dps).toBeCloseTo(source.dps, 1);

    // Every lane keeps its buff, owner and window.
    const sortedSource = [...source.buffIntervals].sort((a, b) => a.start - b.start);
    decoded.buffIntervals.forEach((lane, index) => {
      expect(lane.buffId).toBe(sortedSource[index].buffId);
      expect(lane.ownerId).toBe(sortedSource[index].ownerId);
      expect(lane.start).toBeCloseTo(sortedSource[index].start, 1);
    });
    // Distinct enemies stay distinct after entity handles are renumbered.
    const sourceEntities = new Set(source.buffIntervals.map((lane) => lane.ownerEntityId || lane.ownerId));
    const decodedEntities = new Set(decoded.buffIntervals.map((lane) => lane.ownerEntityId));
    expect(decodedEntities.size).toBe(sourceEntities.size);

    // The default filter drops the VFX/state markers the timeline hides anyway.
    const semantic = decodeSnapshot(
      encodeSnapshot(source, realSpace, { tier: "full", lanes: "semantic" }), realSpace, source.dictionary);
    const kept = source.buffIntervals.filter((lane) => lane.contributionKind !== "other");
    expect(semantic.buffIntervals).toHaveLength(kept.length);
    expect(kept.length).toBeLessThan(source.buffIntervals.length);
    expect(semantic.buffIntervals.every((lane) => lane.contributionKind !== "other")).toBe(true);

    const full = encodeSnapshot(source, realSpace, { tier: "full" });
    const card = encodeSnapshot(source, realSpace, { tier: "card" });
    console.log(`BEC sizes: card ${card.byteLength} B, full ${full.byteLength} B, complete(all lanes) ${encoded.byteLength} B`);
    expect(card.byteLength).toBeLessThan(2048);
  });
});
