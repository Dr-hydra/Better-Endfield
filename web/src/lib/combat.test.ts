import { describe, expect, it } from "vitest";
import {
  CombatSchemaError,
  buildSkillIndex,
  isMobilityClip,
  parseCombatRecord,
  skillDisplay,
  skillName,
} from "./combat";
import { contributionIndex } from "./categories";
import type { CombatDictionary } from "../types";

function validRecord() {
  return {
    schemaVersion: 15,
    battle: {
      sessionId: 1,
      startedUnixSeconds: 1,
      dungeonId: "dungeon_test",
      durationSeconds: 10,
    },
    dictionary: {
      schemaVersion: 1,
      gameVersion: "dev",
      hotfixVersion: "",
      sourceSha256: "test",
      zones: [{ index: 0, name: "NormalCalcZone", multiply: false, mergeSides: false, damageTypeZone: true, roles: ["damageType"] }],
    },
    summary: { totalDamage: 1000, dps: 100, rdps: 100, hitCount: 1, criticalCount: 0 },
    squad: [{
      charId: "chr_test",
      charInstId: "1",
      level: 90,
      breakStage: 5,
      potential: 2,
      skillGroups: [],
      weapon: null,
      equipments: [],
      equipSuits: [],
    }, {
      charId: "chr_support",
      charInstId: "2",
      level: 90,
      breakStage: 5,
      potential: 2,
      skillGroups: [],
      weapon: null,
      equipments: [],
      equipSuits: [],
    }],
    actions: [{
      id: 1,
      start: 1,
      end: 1.5,
      actorId: "chr_test",
      type: "skillCast",
      skillId: "chr_test_attack1",
      result: "completed",
      finishType: "completed",
      interruptReason: "enterFreeState",
    }],
    effects: [{
      id: 1,
      actionId: 1,
      time: 1.2,
      type: "damage",
      sourceId: "chr_test",
      sourceEntityId: "chr_test_1001",
      creditedActorId: "chr_test",
      actorAttribution: "runtimeExact",
      targetId: "enemy",
      skillId: "chr_test_attack1",
      value: 1000,
      rawValue: 1000,
      hpDelta: -1000,
      damageType: 2,
      decorateMask: 4,
      critical: false,
      rdpsShares: [
        { sourceId: "chr_support", fraction: 0.2, contributionType: 1, contributionKind: "attack" },
      ],
      ledger: {
        originSkillId: "chr_test_attack1",
        calcResult: 900,
        atkScale: 1.5,
        attackerZones: [1.1],
        defenderZones: [1],
        factors: {
          valid: true,
          attack: 600,
          attackLinked: true,
          zoneProduct: 1.1,
          defense: 100,
          defResistance: 0.5,
          typeResistance: 0.9,
          critical: false,
          critRate: 0.05,
          critDamage: 0.5,
          weakness: 1,
          shelter: 0,
          ignite: 1,
          igniteApplies: false,
        },
        externalFraction: 0.2,
        selfFraction: 0,
        zoneWrites: [],
        zoneWritesDropped: 0,
        attributeGroups: [],
        attributeWrites: [],
        contributors: [{
          statusInstanceId: "77",
          statusId: "buff_support_atkup",
          sourceId: "chr_support",
          ownerId: "chr_test",
          mechanism: "attribute",
          contributionType: 1,
          contributionKind: "attack",
          semantic: "verified",
          side: 0,
          attributeType: 2,
          zoneIndex: -1,
          factor: 0.8,
          fraction: 0.2,
          external: true,
          excluded: false,
        }],
        staleMapBuffs: 0,
      },
    }, {
      id: 2,
      time: 0.5,
      type: "statusApply",
      sourceId: "chr_support",
      creditedActorId: "chr_support",
      targetId: "chr_test",
      statusId: "buff_support_atkup",
      statusInstanceId: "77",
      statusKind: "buff",
      contributionType: 1,
      contributionKind: "attack",
      sourceKind: "characterSkill",
      sourceTemplateId: "chr_support",
      sourceSkillId: "chr_support_normal_skill",
      stacking: {
        type: 2, typeId: "stack", maxStack: 2, enhanceCount: 0, keyed: false, key: "",
      },
    }, {
      id: 3,
      time: 4,
      type: "statusRemove",
      sourceId: "chr_support",
      targetId: "chr_test",
      statusId: "buff_support_atkup",
      statusInstanceId: "77",
      statusKind: "buff",
    }],
  };
}

describe("schema 15 parser", () => {
  it("accepts a complete latest record", () => {
    const record = parseCombatRecord(JSON.stringify(validRecord()));
    expect(record.schemaVersion).toBe(15);
    expect(record.totalDamage).toBe(1000);
    expect(record.events[0].actionId).toBe(1);
    expect(record.events[0].decorateMask).toBe(4);
    expect(record.events[0].transferred).toBeCloseTo(0.2);
    expect(record.zones[0].name).toBe("NormalCalcZone");
  });

  it("rejects older schemas", () => {
    for (const schemaVersion of [12, 13]) {
      const value = { ...validRecord(), schemaVersion };
      expect(() => parseCombatRecord(JSON.stringify(value))).toThrow(CombatSchemaError);
    }
  });

  it("builds action clips carrying their hits", () => {
    const record = parseCombatRecord(JSON.stringify(validRecord()));
    expect(record.clips).toHaveLength(1);
    expect(record.clips[0].hits).toHaveLength(1);
    expect(record.clips[0].damage).toBe(1000);
    expect(record.clips[0].result).toBe("completed");
  });

  it("closes buff intervals and credits them from the ledger", () => {
    const record = parseCombatRecord(JSON.stringify(validRecord()));
    expect(record.buffIntervals).toHaveLength(1);
    const [interval] = record.buffIntervals;
    expect(interval.start).toBe(0.5);
    expect(interval.end).toBe(4);
    expect(interval.open).toBe(false);
    expect(interval.external).toBe(true);
    expect(interval.credited).toBeCloseTo(200);
  });

  it("closes still-open buffs at the end of the battle", () => {
    const value = validRecord();
    value.effects = value.effects.filter((effect) => effect.type !== "statusRemove");
    const record = parseCombatRecord(JSON.stringify(value));
    expect(record.buffIntervals[0].open).toBe(true);
    expect(record.buffIntervals[0].end).toBe(10);
  });

  it("summarises contributors with a damage-weighted multiplier", () => {
    const record = parseCombatRecord(JSON.stringify(validRecord()));
    expect(record.contributors).toHaveLength(1);
    const [contributor] = record.contributors;
    expect(contributor.buffId).toBe("buff_support_atkup");
    expect(contributor.meanMultiplier).toBeCloseTo(1.25);
    expect(contributor.credited).toBeCloseTo(200);
    expect(contributor.sourceKind).toBe("characterSkill");
  });

  it("splits rDPS between the attacker and the support", () => {
    const record = parseCombatRecord(JSON.stringify(validRecord()));
    expect(record.rdpsCharacters.chr_test.damage).toBeCloseTo(800);
    expect(record.rdpsCharacters.chr_support.damage).toBeCloseTo(200);
    expect(record.rdpsCharacters.chr_support.rdpsByContribution[contributionIndex("attack")]).toBeCloseTo(200);
  });

  it("rejects missing result events", () => {
    const value = validRecord() as Record<string, unknown>;
    delete value.effects;
    expect(() => parseCombatRecord(JSON.stringify(value))).toThrow(/effects/);
  });

  it("rejects effects that reference an unknown action", () => {
    const value = validRecord();
    value.effects[0].actionId = 999;
    expect(() => parseCombatRecord(JSON.stringify(value))).toThrow(/不存在的操作/);
  });

  it("rejects incomplete squad snapshots", () => {
    const value = validRecord();
    delete (value.squad[0] as Partial<typeof value.squad[0]>).equipments;
    expect(() => parseCombatRecord(JSON.stringify(value))).toThrow(/equipments/);
  });
});

describe("stacking groups", () => {
  function stackedRecord(typeId: string, type: number, maxStack: number, starts: number[]) {
    const value = validRecord() as Record<string, unknown>;
    const effects = (value.effects as Record<string, unknown>[]).filter(
      (effect) => effect.type === "damage",
    );
    starts.forEach((start, index) => {
      effects.push({
        id: 100 + index,
        time: start,
        type: "statusApply",
        sourceId: "chr_support",
        creditedActorId: "chr_support",
        targetId: "chr_test",
        targetEntityId: "chr_test_1001",
        statusId: "buff_layered",
        statusInstanceId: String(200 + index),
        statusKind: "buff",
        contributionType: 1,
        contributionKind: "attack",
        stacking: { type, typeId, maxStack, enhanceCount: 0, keyed: false, key: "" },
      });
    });
    value.effects = effects;
    return parseCombatRecord(JSON.stringify(value));
  }

  function stacked(typeId: string, type: number, maxStack: number, starts: number[]) {
    return stackedRecord(typeId, type, maxStack, starts).buffIntervals
      .filter((interval) => interval.buffId === "buff_layered");
  }

  it("evicts the oldest layer once a capped group is full", () => {
    const intervals = stacked("stack", 2, 2, [1, 2, 3]);
    expect(intervals.map((interval) => [interval.start, interval.end])).toEqual([
      [1, 3], [2, 10], [3, 10],
    ]);
    expect(intervals[0].displaced).toBe(true);
    expect(intervals[2].stackIndex).toBe(2);
  });

  it("keeps a single instance for replace-style stacking", () => {
    const intervals = stacked("refresh", 4, 0, [1, 2, 3]);
    expect(intervals.map((interval) => [interval.start, interval.end])).toEqual([
      [1, 2], [2, 3], [3, 10],
    ]);
    expect(intervals.every((interval) => interval.stackConcurrent === 1)).toBe(true);
  });

  it("leaves unlimited stacking untouched", () => {
    const intervals = stacked("unlimited", 0, 0, [1, 2, 3]);
    expect(intervals.every((interval) => interval.end === 10)).toBe(true);
    expect(intervals[2].stackConcurrent).toBe(3);
  });
});

describe("mobility actions", () => {
  it("matches dodges and dashes but not skill casts", () => {
    expect(isMobilityClip({ type: "dodge", skillId: "" })).toBe(true);
    expect(isMobilityClip({ type: "skillCast", skillId: "common_character_perfect_dodge" })).toBe(true);
    expect(isMobilityClip({ type: "skillCast", skillId: "common_character_ai_dash" })).toBe(true);
    expect(isMobilityClip({ type: "skillCast", skillId: "chr_0032_lizhiyan_attack1" })).toBe(false);
  });
});

describe("skill naming", () => {
  const dict = {
    schemaVersion: 1, exportedAt: "", c: {}, t: {}, w: {}, su: {}, d: {}, ds: {}, zones: [],
    sk: {
      chr_test_NormalAttack: {
        n: "普通攻击", c: "attack", i: "icon_attack_test",
        ids: ["chr_test_attack1", "chr_test_attack2"], ch: "chr_test",
      },
    },
  } as unknown as CombatDictionary;

  it("names shared character actions that are absent from the table", () => {
    expect(skillName(dict, "common_character_perfect_dodge")).toBe("完美闪避");
    expect(skillName(dict, "common_character_ai_dash")).toBe("冲刺");
  });

  it("falls back to the humanised id for unknown skills", () => {
    expect(skillName(dict, "chr_0032_lizhiyan_attack4")).toBe("attack4");
  });

  it("maps runtime skill ids to their group icon and squad level", () => {
    const index = buildSkillIndex(
      [{ charId: "chr_test", skillGroups: [{ skillGroupId: "chr_test_NormalAttack", level: 9, maxLevel: 12 }] }],
      dict,
    );
    const display = skillDisplay(index, dict, "chr_test_attack2");
    expect(display.name).toBe("普通攻击");
    expect(display.icon).toBe("./icons/skills/icon_attack_test.png");
    expect(display.level).toBe(9);
    expect(display.maxLevel).toBe(12);
  });
});
