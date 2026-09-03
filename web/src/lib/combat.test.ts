import { describe, expect, it } from "vitest";
import { CombatSchemaError, parseCombatRecord } from "./combat";

function validRecord() {
  return {
    schemaVersion: 11,
    battle: {
      sessionId: 1,
      startedUnixSeconds: 1,
      dungeonId: "dungeon_test",
      durationSeconds: 10,
    },
    dictionary: { schemaVersion: 1, gameVersion: "dev", hotfixVersion: "", sourceSha256: "test" },
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
    }],
    actions: [{ id: 1, start: 1, end: 1.5, actorId: "chr_test", type: "basicAttack", skillId: "chr_test_attack1", result: "completed" }],
    effects: [{
      id: 1,
      actionId: 1,
      parentEffectId: 7,
      time: 1.2,
      type: "damage",
      sourceId: "chr_test",
      sourceEntityId: "chr_test_1001",
      creditedActorId: "chr_test",
      actorAttribution: "runtimeExact",
      targetId: "enemy",
      skillId: "chr_test_attack1",
      value: 1000,
      rawValue: 1200,
      hpDelta: -1000,
      damageType: 2,
      decorateMask: 4,
      critical: true,
      rdpsShares: [],
    }],
  };
}

describe("schema 11 parser", () => {
  it("accepts a complete latest record", () => {
    const record = parseCombatRecord(JSON.stringify(validRecord()));
    expect(record.schemaVersion).toBe(11);
    expect(record.totalDamage).toBe(1000);
    expect(record.events[0].actionId).toBe(1);
    expect(record.events[0].parentEffectId).toBe(7);
    expect(record.events[0].rawDamage).toBe(1200);
    expect(record.events[0].sourceEntityId).toBe("chr_test_1001");
    expect(record.events[0].decorateMask).toBe(4);
  });

  it("rejects old schemas", () => {
    const value = { ...validRecord(), schemaVersion: 10 };
    expect(() => parseCombatRecord(JSON.stringify(value))).toThrow(CombatSchemaError);
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
