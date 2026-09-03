import type {
  Aggregate,
  CharacterAggregate,
  CombatDictionary,
  CombatEffect,
  CombatRecordV11,
  CombatRecordV11Raw,
  DamageEvent,
  SquadMemberV11,
} from "../types";

export class CombatSchemaError extends Error {
  constructor(message: string) {
    super(message);
    this.name = "CombatSchemaError";
  }
}

function isObject(value: unknown): value is Record<string, unknown> {
  return typeof value === "object" && value !== null && !Array.isArray(value);
}

function finiteNumber(value: unknown, field: string): number {
  if (typeof value !== "number" || !Number.isFinite(value)) {
    throw new CombatSchemaError(`${field} 不是有效数值`);
  }
  return value;
}

function unsignedIntegerString(value: unknown, field: string): string {
  if (typeof value !== "string" || !/^\d+$/.test(value)) {
    throw new CombatSchemaError(`${field} 不是有效的无符号整数字符串`);
  }
  return value;
}

function validateMember(member: unknown, index: number): asserts member is SquadMemberV11 {
  if (!isObject(member)) throw new CombatSchemaError(`squad[${index}] 无效`);
  if (typeof member.charId !== "string" || !member.charId) {
    throw new CombatSchemaError(`squad[${index}].charId 缺失`);
  }
  unsignedIntegerString(member.charInstId, `squad[${index}].charInstId`);
  for (const field of ["level", "breakStage", "potential"] as const) {
    finiteNumber(member[field], `squad[${index}].${field}`);
  }
  for (const field of ["skillGroups", "equipments", "equipSuits"] as const) {
    if (!Array.isArray(member[field])) {
      throw new CombatSchemaError(`squad[${index}].${field} 缺失`);
    }
  }
  if (!(member.weapon === null || isObject(member.weapon))) {
    throw new CombatSchemaError(`squad[${index}].weapon 无效`);
  }
}

function aggregate(): Aggregate {
  return { damage: 0, hits: 0, criticalHits: 0 };
}

function characterAggregate(): CharacterAggregate {
  return {
    ...aggregate(),
    damageByCategory: Array(16).fill(0),
    rdpsByContribution: Array(16).fill(0),
  };
}

function exactSkillMetadata(dictionary?: CombatDictionary) {
  const categoryBySkill = new Map<string, number>();
  const ownerBySkill = new Map<string, string>();
  const conflictingOwners = new Set<string>();
  if (!dictionary) return { categoryBySkill, ownerBySkill };
  const categories: Record<string, number> = {
    attack: 0,
    skill: 1,
    ultimate: 2,
    combo: 3,
    passive: 4,
  };
  for (const [groupId, value] of Object.entries(dictionary.sk)) {
    if (typeof value === "string") continue;
    const category = categories[value.c];
    if (category === undefined) continue;
    const ids = [groupId, ...(value.ids ?? [])];
    for (const skillId of ids) {
      categoryBySkill.set(skillId, category);
      if (!value.ch || conflictingOwners.has(skillId)) continue;
      const current = ownerBySkill.get(skillId);
      if (current && current !== value.ch) {
        ownerBySkill.delete(skillId);
        conflictingOwners.add(skillId);
      } else {
        ownerBySkill.set(skillId, value.ch);
      }
    }
  }
  return { categoryBySkill, ownerBySkill };
}

function analyze(raw: CombatRecordV11Raw, dictionary?: CombatDictionary): CombatRecordV11 {
  const characters: Record<string, CharacterAggregate> = {};
  const rdpsCharacters: Record<string, CharacterAggregate> = {};
  const skills: Record<string, Aggregate> = {};
  const damageCategories: Record<string, Aggregate> = {};
  const events: DamageEvent[] = [];
  const categoryIds = ["basic_attack", "skill", "ultimate", "combo", "passive", "other"];
  const { categoryBySkill, ownerBySkill } = exactSkillMetadata(dictionary);
  const squadIds = new Set(raw.squad.map((member) => member.charId));
  let totalDamage = 0;
  let hitCount = 0;
  let criticalCount = 0;

  const getCharacter = (map: Record<string, CharacterAggregate>, id: string) =>
    map[id] ??= characterAggregate();
  const add = (value: Aggregate, amount: number, critical: boolean) => {
    value.damage += amount;
    value.hits += 1;
    if (critical) value.criticalHits += 1;
  };

  for (const effect of raw.effects) {
    if (effect.type !== "damage") continue;
    const amount = Math.abs(Number(effect.value) || 0);
    const skillId = effect.skillId || "<unknown>";
    const runtimeActor = effect.creditedActorId || effect.sourceId;
    const dictionaryActor = ownerBySkill.get(skillId);
    const attackerId = runtimeActor && squadIds.has(runtimeActor)
      ? runtimeActor
      : dictionaryActor && squadIds.has(dictionaryActor)
        ? dictionaryActor
        : "<unknown>";
    const actorAttribution = attackerId === "<unknown>"
      ? "unknown"
      : runtimeActor === attackerId ? "runtimeExact" : "dictionaryExact";
    const critical = Boolean(effect.critical);
    const category = categoryBySkill.get(skillId) ?? 5;
    totalDamage += amount;
    hitCount += 1;
    if (critical) criticalCount += 1;

    const direct = getCharacter(characters, attackerId);
    add(direct, amount, critical);
    direct.damageByCategory![category] += amount;
    add(skills[skillId] ??= aggregate(), amount, critical);
    add(damageCategories[categoryIds[category]] ??= aggregate(), amount, critical);

    const shares = new Map<string, { fraction: number; kind: number }>();
    for (const share of effect.rdpsShares ?? []) {
      const sourceId = share.sourceId || share.source || "";
      const fraction = Number(share.fraction);
      if (!sourceId || sourceId === attackerId || !Number.isFinite(fraction) || fraction <= 0) continue;
      const kind = Math.max(0, Math.min(15, Number(share.contributionType) || 8));
      const key = `${sourceId}\0${kind}`;
      const current = shares.get(key);
      shares.set(key, { fraction: (current?.fraction ?? 0) + fraction, kind });
    }
    const rawTransferred = [...shares.values()].reduce((sum, item) => sum + item.fraction, 0);
    const transferred = Math.min(.999999, rawTransferred);
    const scale = rawTransferred > .999999 ? .999999 / rawTransferred : 1;
    const retained = amount * (1 - transferred);
    const rdpsAttacker = getCharacter(rdpsCharacters, attackerId);
    add(rdpsAttacker, retained, critical);
    rdpsAttacker.damageByCategory![category] += retained;
    rdpsAttacker.rdpsByContribution![0] += retained;
    for (const [key, share] of shares) {
      const sourceId = key.split("\0", 1)[0];
      const credited = amount * share.fraction * scale;
      const support = getCharacter(rdpsCharacters, sourceId);
      support.damage += credited;
      support.damageByCategory![category] += credited;
      support.rdpsByContribution![share.kind] += credited;
    }
    events.push({
      id: effect.id,
      actionId: effect.actionId,
      parentEffectId: effect.parentEffectId,
      time: effect.time,
      damage: amount,
      rawDamage: effect.rawValue == null ? undefined : Math.abs(Number(effect.rawValue) || 0),
      hpDelta: effect.hpDelta,
      damageType: effect.damageType,
      decorateMask: effect.decorateMask,
      critical,
      attackerId,
      sourceEntityId: effect.sourceEntityId,
      actorAttribution,
      attributionReason: actorAttribution === "unknown"
        ? "runtime_and_dictionary_actor_unresolved"
        : undefined,
      originSkillId: skillId,
      rdpsShares: effect.rdpsShares,
    });
  }

  const openStatuses = new Map<string, CombatEffect>();
  const buffIntervals = [] as CombatRecordV11["buffIntervals"];
  const staggerIntervals = [] as CombatRecordV11["staggerIntervals"];
  for (const effect of raw.effects) {
    const instanceId = effect.statusInstanceId;
    if (!instanceId) continue;
    if (effect.type === "statusApply" || effect.type === "statusRefresh") {
      openStatuses.set(instanceId, effect);
    } else if (effect.type === "statusRemove") {
      const start = openStatuses.get(instanceId);
      if (!start) continue;
      openStatuses.delete(instanceId);
      if (start.statusKind === "stagger") {
        staggerIntervals.push({
          start: start.time,
          end: effect.time,
          targetId: start.targetId,
          buffId: start.statusId,
        });
      } else {
        buffIntervals.push({
          start: start.time,
          end: effect.time,
          buffId: start.statusId || "",
          sourceId: start.sourceId,
          targetId: start.targetId,
          targetEntityId: start.targetEntityId,
          originSkillId: start.skillId,
          contributionType: start.contributionType,
          contributionKind: start.contributionKind,
          effectMin: start.effectMin,
          effectMax: start.effectMax,
          sourceEntityId: start.sourceEntityId,
          creditedActorId: start.creditedActorId,
          actorAttribution: start.actorAttribution,
          sourceAttribution: start.sourceAttribution,
          sourceKind: start.sourceKind,
          sourceTemplateId: start.sourceTemplateId,
          sourceSkillId: start.sourceSkillId,
          attributionReason: start.attributionReason,
        });
      }
    }
  }

  const durationSeconds = raw.battle.durationSeconds;
  return {
    ...raw,
    sessionId: raw.battle.sessionId,
    startedUnixSeconds: raw.battle.startedUnixSeconds,
    durationSeconds,
    totalDamage,
    dps: totalDamage / Math.max(durationSeconds, .001),
    hitCount,
    criticalCount,
    dungeonId: raw.battle.dungeonId,
    modeId: raw.battle.modeId,
    characters,
    rdpsCharacters,
    skills,
    damageCategories,
    timeline: [],
    buffIntervals,
    staggerIntervals,
    events,
  };
}

export function parseCombatRecordObject(
  raw: unknown,
  dictionary?: CombatDictionary,
): CombatRecordV11 {
  if (!isObject(raw)) throw new CombatSchemaError("战斗记录根节点无效");
  if (raw.schemaVersion !== 11) {
    throw new CombatSchemaError(
      `仅支持最新版 schema 11，当前文件为 schema ${String(raw.schemaVersion ?? "未知")}`,
    );
  }
  for (const field of ["squad", "actions", "effects"] as const) {
    if (!Array.isArray(raw[field])) throw new CombatSchemaError(`schema 11 必须包含 ${field}`);
  }
  for (const field of ["battle", "dictionary", "summary"] as const) {
    if (!isObject(raw[field])) throw new CombatSchemaError(`schema 11 必须包含 ${field}`);
  }
  const battle = raw.battle as Record<string, unknown>;
  const summary = raw.summary as Record<string, unknown>;
  if (finiteNumber(battle.durationSeconds, "battle.durationSeconds") <= 0) {
    throw new CombatSchemaError("战斗时长必须大于 0");
  }
  if (typeof battle.dungeonId !== "string" || !battle.dungeonId) {
    throw new CombatSchemaError("schema 11 必须包含 battle.dungeonId");
  }
  for (const field of ["totalDamage", "dps", "rdps", "hitCount", "criticalCount"] as const) {
    finiteNumber(summary[field], `summary.${field}`);
  }
  (raw.squad as unknown[]).forEach(validateMember);
  const actionIds = new Set<number>();
  for (const [index, action] of (raw.actions as unknown[]).entries()) {
    if (!isObject(action)) throw new CombatSchemaError(`actions[${index}] 无效`);
    const id = finiteNumber(action.id, `actions[${index}].id`);
    finiteNumber(action.start, `actions[${index}].start`);
    if (action.end === null) {
      finiteNumber(action.observedUntil, `actions[${index}].observedUntil`);
    } else {
      finiteNumber(action.end, `actions[${index}].end`);
    }
    if (typeof action.actorId !== "string" || typeof action.type !== "string") {
      throw new CombatSchemaError(`actions[${index}] 缺少操作主体或类型`);
    }
    actionIds.add(id);
  }
  for (const [index, effect] of (raw.effects as unknown[]).entries()) {
    if (!isObject(effect)) throw new CombatSchemaError(`effects[${index}] 无效`);
    finiteNumber(effect.id, `effects[${index}].id`);
    finiteNumber(effect.time, `effects[${index}].time`);
    if (effect.actionId !== undefined) {
      const actionId = finiteNumber(effect.actionId, `effects[${index}].actionId`);
      if (!actionIds.has(actionId)) {
        throw new CombatSchemaError(`effects[${index}] 引用了不存在的操作`);
      }
    }
    if (typeof effect.type !== "string" || typeof effect.sourceId !== "string") {
      throw new CombatSchemaError(`effects[${index}] 缺少结果类型或来源`);
    }
    if (effect.type === "statusApply" || effect.type === "statusRefresh" ||
        effect.type === "statusRemove") {
      unsignedIntegerString(effect.statusInstanceId,
        `effects[${index}].statusInstanceId`);
    }
  }
  return analyze(raw as unknown as CombatRecordV11Raw, dictionary);
}

export function parseCombatRecord(
  text: string,
  dictionary?: CombatDictionary,
): CombatRecordV11 {
  let raw: unknown;
  try {
    raw = JSON.parse(text);
  } catch {
    throw new CombatSchemaError("文件不是有效的 JSON");
  }
  return parseCombatRecordObject(raw, dictionary);
}

export function totalRdps(record: CombatRecordV11): number {
  return record.summary.rdps;
}

export function formatNumber(value: number, digits = 0): string {
  return new Intl.NumberFormat("zh-CN", {
    maximumFractionDigits: digits,
    minimumFractionDigits: digits,
  }).format(Number.isFinite(value) ? value : 0);
}

export function formatDuration(seconds: number): string {
  const minutes = Math.floor(seconds / 60);
  const rest = seconds - minutes * 60;
  return `${String(minutes).padStart(2, "0")}:${rest.toFixed(2).padStart(5, "0")}`;
}

export function characterName(dict: CombatDictionary, id: string): string {
  const value = dict.c[id];
  return typeof value === "string" ? value : value?.n || id;
}

export function dungeonName(dict: CombatDictionary, id: string): string {
  const value = dict.d[id];
  return typeof value === "string" ? value : value?.n || id;
}

export function skillEntry(dict: CombatDictionary, id: string) {
  const direct = dict.sk[id];
  if (direct) return direct;
  return Object.values(dict.sk).find(
    (value) => typeof value !== "string" && value.ids?.includes(id),
  );
}

export function skillName(dict: CombatDictionary, id: string): string {
  const value = skillEntry(dict, id);
  return typeof value === "string" ? value : value?.n || id || "未知技能";
}

export function iconPath(kind: "skills" | "items" | "suits", iconId?: string): string {
  if (!iconId) return "";
  return `./icons/${kind}/${iconId}.png`;
}

export function eventRows(record: CombatRecordV11): Map<string, DamageEvent[]> {
  const result = new Map<string, DamageEvent[]>();
  for (const event of record.events) {
    const key = event.attackerId || "unknown";
    const row = result.get(key) ?? [];
    row.push(event);
    result.set(key, row);
  }
  return result;
}
