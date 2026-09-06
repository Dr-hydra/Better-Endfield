import { CONTRIBUTION_CATEGORIES, CONTRIBUTION_COUNT, SKILL_CATEGORIES, contributionIndex } from "./categories";
import { localizeIdTail } from "./buffTerms";
import { buffIconPath } from "./buffTable";
import type {
  ActionClip,
  Aggregate,
  BuffInterval,
  CharacterAggregate,
  CombatAction,
  CombatDictionary,
  CombatEffect,
  CombatRecord,
  CombatRecordRaw,
  ContributorSummary,
  DamageEvent,
  FieldSegment,
  NominalEffect,
  SkillAggregate,
  SkillLevel,
  SquadMember,
  StaggerInterval,
  StatusStacking,
} from "../types";

export class CombatSchemaError extends Error {}

export const SUPPORTED_SCHEMA = 15;

function isObject(value: unknown): value is Record<string, unknown> {
  return typeof value === "object" && value !== null && !Array.isArray(value);
}

function finiteNumber(value: unknown, field: string): number {
  if (typeof value !== "number" || !Number.isFinite(value)) {
    throw new CombatSchemaError(`${field} 必须是有限数字`);
  }
  return value;
}

function unsignedIntegerString(value: unknown, field: string): string {
  if (typeof value !== "string" || !/^\d+$/.test(value)) {
    throw new CombatSchemaError(`${field} 必须是无符号整数字符串`);
  }
  return value;
}

function validateMember(member: unknown, index: number): asserts member is SquadMember {
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
    damageByCategory: Array(SKILL_CATEGORIES.length).fill(0),
    rdpsByContribution: Array(CONTRIBUTION_COUNT).fill(0),
  };
}

const SKILL_CATEGORY_INDEX: Record<string, number> = Object.fromEntries(
  SKILL_CATEGORIES.map((category, index) => [category.id, index]),
);

export function skillCategoryFromId(skillId: string): number {
  if (/_attack\d*$/.test(skillId) || /_attack\d*_/.test(skillId)) return SKILL_CATEGORY_INDEX.attack;
  if (skillId.includes("_normal_skill")) return SKILL_CATEGORY_INDEX.skill;
  if (skillId.includes("_ultimate")) return SKILL_CATEGORY_INDEX.ultimate;
  if (skillId.includes("_combo_skill")) return SKILL_CATEGORY_INDEX.combo;
  if (skillId.includes("_talent") || skillId.includes("passive")) return SKILL_CATEGORY_INDEX.passive;
  return SKILL_CATEGORY_INDEX.other;
}

function skillMetadata(dictionary?: CombatDictionary) {
  const categoryBySkill = new Map<string, number>();
  const ownerBySkill = new Map<string, string>();
  const conflictingOwners = new Set<string>();
  if (!dictionary) return { categoryBySkill, ownerBySkill };
  for (const [groupId, value] of Object.entries(dictionary.sk)) {
    if (typeof value === "string") continue;
    const category = SKILL_CATEGORY_INDEX[value.c];
    const ids = [groupId, ...(value.ids ?? [])];
    for (const skillId of ids) {
      if (category !== undefined) categoryBySkill.set(skillId, category);
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

/**
 * How many instances of one stacking group can be live at once.
 *
 * `stack` and `highPriorityWithMaxStack` are the only types that keep several
 * Buff objects alive; the enhance family folds its layers into a single
 * instance's `enhanceCount`, and the rest simply replace the incumbent.
 * `unlimited` has no cap at all.
 */
export function stackInstanceLimit(stacking: StatusStacking | undefined): number {
  if (!stacking) return Number.POSITIVE_INFINITY;
  if (stacking.typeId === "unlimited") return Number.POSITIVE_INFINITY;
  if (stacking.typeId === "stack" || stacking.typeId === "highPriorityWithMaxStack") {
    return stacking.maxStack > 0 ? stacking.maxStack : Number.POSITIVE_INFINITY;
  }
  return 1;
}

export function stackLayerCount(interval: BuffInterval): number {
  const enhance = interval.stacking?.enhanceCount ?? 0;
  return enhance > 0 ? enhance : interval.stackConcurrent;
}

export function stackLayerLimit(stacking: StatusStacking | undefined): number {
  if (!stacking || stacking.maxStack <= 0) return 0;
  return stacking.maxStack;
}

/**
 * The recorder learns about a buff ending from Buff.Finish, which the game
 * skips when a full stacking group silently evicts its oldest layer. Replaying
 * the group with the runtime's own cap recovers those ends, and fills in each
 * instance's layer position for the timeline.
 */
/**
 * Everything the game would put in one BuffStackingGroup: the same owner, and
 * either a shared stacking key or the same buff id.
 */
export function stackGroupKey(interval: BuffInterval): string {
  const stacking = interval.stacking;
  const key = stacking?.keyed && stacking.key ? `k:${stacking.key}` : `b:${interval.buffId}`;
  return `${interval.ownerEntityId || interval.ownerId}|${key}`;
}

function groupByStack(intervals: BuffInterval[]): Map<string, BuffInterval[]> {
  const groups = new Map<string, BuffInterval[]>();
  for (const interval of intervals) {
    const groupKey = stackGroupKey(interval);
    const bucket = groups.get(groupKey);
    if (bucket) bucket.push(interval);
    else groups.set(groupKey, [interval]);
  }
  return groups;
}

function resolveStackGroups(intervals: BuffInterval[]): void {
  for (const bucket of groupByStack(intervals).values()) {
    const limit = stackInstanceLimit(bucket[0].stacking);
    const live: BuffInterval[] = [];
    for (const interval of bucket) {
      // Drop everything the group had already finished before this one landed.
      for (let index = live.length - 1; index >= 0; index -= 1) {
        if (live[index].end <= interval.start) live.splice(index, 1);
      }
      while (live.length >= limit) {
        const evicted = live.shift();
        if (!evicted) break;
        evicted.end = Math.max(evicted.start, Math.min(evicted.end, interval.start));
        evicted.open = false;
        evicted.displaced = true;
      }
      live.push(interval);
      for (const member of live) {
        member.stackConcurrent = Math.max(member.stackConcurrent, live.length);
      }
      interval.stackIndex = live.length;
    }
  }
}

function analyze(raw: CombatRecordRaw, dictionary?: CombatDictionary): CombatRecord {
  const durationSeconds = raw.battle.durationSeconds;
  const squadIds = new Set(raw.squad.map((member) => member.charId));
  const { categoryBySkill, ownerBySkill } = skillMetadata(dictionary);
  const categoryOf = (skillId: string) => categoryBySkill.get(skillId) ?? skillCategoryFromId(skillId);

  const characters: Record<string, CharacterAggregate> = {};
  const rdpsCharacters: Record<string, CharacterAggregate> = {};
  const skills: Record<string, SkillAggregate> = {};
  const events: DamageEvent[] = [];
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

  // Damage credited per buff instance, accumulated while walking the ledgers.
  const creditedByInstance = new Map<string, { credited: number; hits: number }>();
  const contributorMap = new Map<string, Omit<ContributorSummary, "owners"> & {
    logSum: number;
    damageSum: number;
    owners: Set<string>;
  }>();

  for (const effect of raw.effects) {
    if (effect.type !== "damage") continue;
    const amount = Math.abs(Number(effect.value) || 0);
    const skillId = effect.skillId || effect.ledger?.originSkillId || "<unknown>";
    const runtimeActor = effect.creditedActorId || effect.sourceId;
    const dictionaryActor = ownerBySkill.get(skillId);
    const attackerId = runtimeActor && squadIds.has(runtimeActor)
      ? runtimeActor
      : dictionaryActor && squadIds.has(dictionaryActor) ? dictionaryActor : "<unknown>";
    const critical = Boolean(effect.critical);
    const category = categoryOf(skillId);
    totalDamage += amount;
    hitCount += 1;
    if (critical) criticalCount += 1;

    const direct = getCharacter(characters, attackerId);
    add(direct, amount, critical);
    direct.damageByCategory[category] += amount;
    const skill = skills[skillId] ??= { ...aggregate(), actorId: attackerId, category };
    add(skill, amount, critical);

    const shares = new Map<string, { fraction: number; kind: number }>();
    for (const share of effect.rdpsShares ?? []) {
      const sourceId = share.sourceId || "";
      const fraction = Number(share.fraction);
      if (!sourceId || sourceId === attackerId || !Number.isFinite(fraction) || fraction <= 0) continue;
      const kind = contributionIndex(share.contributionType ?? share.contributionKind);
      const key = `${sourceId}\0${kind}`;
      const current = shares.get(key);
      shares.set(key, { fraction: (current?.fraction ?? 0) + fraction, kind });
    }
    const rawTransferred = [...shares.values()].reduce((sum, item) => sum + item.fraction, 0);
    const transferred = Math.min(0.999999, rawTransferred);
    const scale = rawTransferred > 0.999999 ? 0.999999 / rawTransferred : 1;
    const retained = amount * (1 - transferred);
    const rdpsAttacker = getCharacter(rdpsCharacters, attackerId);
    add(rdpsAttacker, retained, critical);
    rdpsAttacker.damageByCategory[category] += retained;
    rdpsAttacker.rdpsByContribution[0] += retained;
    for (const [key, share] of shares) {
      const sourceId = key.split("\0", 1)[0];
      const credited = amount * share.fraction * scale;
      const support = getCharacter(rdpsCharacters, sourceId);
      support.damage += credited;
      support.damageByCategory[category] += credited;
      support.rdpsByContribution[share.kind] += credited;
    }

    for (const contributor of effect.ledger?.contributors ?? []) {
      const credited = amount * Math.max(0, contributor.fraction) * scale;
      if (contributor.statusInstanceId) {
        const entry = creditedByInstance.get(contributor.statusInstanceId) ?? { credited: 0, hits: 0 };
        entry.credited += credited;
        entry.hits += 1;
        creditedByInstance.set(contributor.statusInstanceId, entry);
      }
      const kind = contributionIndex(contributor.contributionType ?? contributor.contributionKind);
      const key = `${contributor.statusId}\0${contributor.sourceId}\0${kind}\0${contributor.external ? 1 : 0}`;
      const factor = contributor.factor > 0 && Number.isFinite(contributor.factor) ? contributor.factor : 1;
      const multiplier = 1 / factor;
      let summary = contributorMap.get(key);
      if (!summary) {
        summary = {
          key,
          buffId: contributor.statusId,
          sourceId: contributor.sourceId,
          contributionType: kind,
          contributionKind: contributor.contributionKind,
          mechanism: contributor.mechanism,
          semantic: contributor.semantic,
          external: contributor.external,
          excluded: contributor.excluded,
          hits: 0,
          credited: 0,
          meanMultiplier: 1,
          minMultiplier: multiplier,
          maxMultiplier: multiplier,
          owners: new Set<string>(),
          sourceKind: "",
          sourceTemplateId: "",
          sourceSkillId: "",
          peakLayers: 1,
          logSum: 0,
          damageSum: 0,
        };
        contributorMap.set(key, summary);
      }
      summary.hits += 1;
      summary.credited += credited;
      summary.logSum += Math.log(multiplier) * amount;
      summary.damageSum += amount;
      summary.minMultiplier = Math.min(summary.minMultiplier, multiplier);
      summary.maxMultiplier = Math.max(summary.maxMultiplier, multiplier);
      if (contributor.ownerId) summary.owners.add(contributor.ownerId);
    }

    events.push({
      id: effect.id,
      actionId: effect.actionId,
      time: effect.time,
      damage: amount,
      hpDelta: effect.hpDelta,
      damageType: effect.damageType,
      decorateMask: effect.decorateMask,
      critical,
      attackerId,
      targetId: effect.targetId || "enemy",
      skillId,
      skillCategory: category,
      rdpsShares: effect.rdpsShares ?? [],
      ledger: effect.ledger,
      transferred,
    });
  }
  events.sort((a, b) => a.time - b.time || a.id - b.id);

  // Action clips with their hits attached.
  const eventsByAction = new Map<number, DamageEvent[]>();
  for (const event of events) {
    if (event.actionId === undefined || event.actionId <= 0) continue;
    const list = eventsByAction.get(event.actionId) ?? [];
    list.push(event);
    eventsByAction.set(event.actionId, list);
  }
  const clips: ActionClip[] = raw.actions
    .filter((action) => squadIds.has(action.actorId))
    .map((action: CombatAction) => {
      const hits = eventsByAction.get(action.id) ?? [];
      const skillId = action.skillId || "";
      const category = skillId ? categoryOf(skillId) : SKILL_CATEGORY_INDEX.other;
      const end = action.end ?? action.observedUntil ?? action.start;
      return {
        id: action.id,
        actorId: action.actorId,
        skillId,
        type: action.type,
        category,
        start: action.start,
        end: Math.max(action.start, end),
        open: action.end === null,
        result: action.result,
        finishType: action.finishType,
        interruptReason: action.interruptReason,
        hits,
        damage: hits.reduce((sum, hit) => sum + hit.damage, 0),
      };
    })
    .sort((a, b) => a.start - b.start || a.id - b.id);

  // Buff / stagger intervals from status apply/remove pairs.
  const openStatuses = new Map<string, CombatEffect>();
  const buffIntervals: BuffInterval[] = [];
  const staggerIntervals: StaggerInterval[] = [];
  const closeStatus = (start: CombatEffect, end: number, open: boolean) => {
    const targetId = start.targetId || "";
    if (start.statusKind === "stagger") {
      staggerIntervals.push({ targetId, buffId: start.statusId || "", start: start.time, end });
      return;
    }
    const friendlyTarget = squadIds.has(targetId);
    const credit = creditedByInstance.get(start.statusInstanceId || "");
    buffIntervals.push({
      instanceId: start.statusInstanceId || "",
      buffId: start.statusId || "",
      sourceId: start.creditedActorId || start.sourceId || "",
      ownerId: targetId,
      ownerEntityId: start.targetEntityId || "",
      friendlyTarget,
      external: friendlyTarget && Boolean(start.sourceId) && start.sourceId !== targetId,
      start: start.time,
      end,
      open,
      contributionType: contributionIndex(start.contributionType ?? start.contributionKind),
      contributionKind: start.contributionKind || "other",
      sourceKind: start.sourceKind || "unknown",
      sourceTemplateId: start.sourceTemplateId || "",
      sourceSkillId: start.sourceSkillId || "",
      originSkillId: start.skillId || "",
      credited: credit?.credited ?? 0,
      hitsCredited: credit?.hits ?? 0,
      stacking: start.stacking,
      nominalEffects: start.nominalEffects,
      stackIndex: 0,
      stackConcurrent: 1,
    });
  };
  for (const effect of raw.effects) {
    const instanceId = effect.statusInstanceId;
    if (!instanceId) continue;
    if (effect.type === "statusApply" || effect.type === "statusRefresh") {
      if (!openStatuses.has(instanceId)) openStatuses.set(instanceId, effect);
    } else if (effect.type === "statusRemove") {
      const start = openStatuses.get(instanceId);
      if (!start) continue;
      openStatuses.delete(instanceId);
      closeStatus(start, effect.time, false);
    }
  }
  for (const start of openStatuses.values()) closeStatus(start, durationSeconds, true);
  buffIntervals.sort((a, b) => a.start - b.start || a.end - b.end);
  resolveStackGroups(buffIntervals);
  staggerIntervals.sort((a, b) => a.start - b.start);

  // Who is on the field: the actor of the most recent squad action.
  const fieldSegments: FieldSegment[] = [];
  for (const clip of clips) {
    const last = fieldSegments[fieldSegments.length - 1];
    if (last && last.actorId === clip.actorId) continue;
    if (last) last.end = clip.start;
    fieldSegments.push({ actorId: clip.actorId, start: last ? clip.start : 0, end: durationSeconds });
  }

  // Source metadata for contributor rows comes from the buff intervals.
  const sourceByBuff = new Map<string, BuffInterval>();
  const peakLayersByBuff = new Map<string, number>();
  // Nominal ranges merge across instances rather than taking the first, because
  // a buff re-applied later in the fight can carry a different magnitude --
  // more layers, a bigger affix roll, more enemies caught by the same hit.
  const nominalByBuff = new Map<string, Map<number, NominalEffect>>();
  for (const interval of buffIntervals) {
    if (!sourceByBuff.has(interval.buffId)) sourceByBuff.set(interval.buffId, interval);
    const layers = stackLayerCount(interval);
    if (layers > (peakLayersByBuff.get(interval.buffId) ?? 0)) {
      peakLayersByBuff.set(interval.buffId, layers);
    }
    for (const effect of interval.nominalEffects ?? []) {
      let slots = nominalByBuff.get(interval.buffId);
      if (!slots) nominalByBuff.set(interval.buffId, slots = new Map());
      const slotKey = effect.attributeType * 8 + effect.slot;
      const merged = slots.get(slotKey);
      if (!merged) slots.set(slotKey, { ...effect });
      else {
        merged.min = Math.min(merged.min, effect.min);
        merged.max = Math.max(merged.max, effect.max);
      }
    }
  }
  const contributors: ContributorSummary[] = [...contributorMap.values()].map((summary) => {
    const meta = sourceByBuff.get(summary.buffId);
    const nominal = [...(nominalByBuff.get(summary.buffId)?.values() ?? [])];
    return {
      key: summary.key,
      buffId: summary.buffId,
      sourceId: summary.sourceId,
      contributionType: summary.contributionType,
      contributionKind: summary.contributionKind,
      mechanism: summary.mechanism,
      semantic: summary.semantic,
      external: summary.external,
      excluded: summary.excluded,
      hits: summary.hits,
      credited: summary.credited,
      meanMultiplier: Math.exp(summary.logSum / Math.max(1e-9, summary.damageSum)),
      minMultiplier: summary.minMultiplier,
      maxMultiplier: summary.maxMultiplier,
      owners: [...summary.owners],
      sourceKind: meta?.sourceKind ?? "unknown",
      sourceTemplateId: meta?.sourceTemplateId ?? "",
      sourceSkillId: meta?.sourceSkillId ?? "",
      stacking: meta?.stacking,
      peakLayers: peakLayersByBuff.get(summary.buffId) ?? 1,
      nominalEffects: nominal?.length ? nominal : undefined,
    };
  }).sort((a, b) => b.credited - a.credited);

  return {
    ...raw,
    provenance: "events",
    sessionId: raw.battle.sessionId,
    startedUnixSeconds: raw.battle.startedUnixSeconds,
    durationSeconds,
    totalDamage,
    dps: totalDamage / Math.max(durationSeconds, 0.001),
    hitCount,
    criticalCount,
    dungeonId: raw.battle.dungeonId,
    modeId: raw.battle.modeId,
    characters,
    rdpsCharacters,
    skills,
    events,
    clips,
    buffIntervals,
    staggerIntervals,
    fieldSegments,
    contributors,
    zones: raw.dictionary.zones ?? [],
  };
}

export function parseCombatRecordObject(raw: unknown, dictionary?: CombatDictionary): CombatRecord {
  if (!isObject(raw)) throw new CombatSchemaError("战斗记录根节点无效");
  if (raw.schemaVersion !== SUPPORTED_SCHEMA) {
    throw new CombatSchemaError(
      `仅支持 schema ${SUPPORTED_SCHEMA}，当前文件为 schema ${String(raw.schemaVersion ?? "未知")}；请用最新版 Better Endfield 重新记录`,
    );
  }
  for (const field of ["squad", "actions", "effects"] as const) {
    if (!Array.isArray(raw[field])) throw new CombatSchemaError(`schema ${SUPPORTED_SCHEMA} 必须包含 ${field}`);
  }
  for (const field of ["battle", "dictionary", "summary"] as const) {
    if (!isObject(raw[field])) throw new CombatSchemaError(`schema ${SUPPORTED_SCHEMA} 必须包含 ${field}`);
  }
  const battle = raw.battle as Record<string, unknown>;
  const summary = raw.summary as Record<string, unknown>;
  if (finiteNumber(battle.durationSeconds, "battle.durationSeconds") <= 0) {
    throw new CombatSchemaError("战斗时长必须大于 0");
  }
  if (typeof battle.dungeonId !== "string" || !battle.dungeonId) {
    throw new CombatSchemaError("battle.dungeonId 缺失");
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
    if (effect.actionId !== undefined && effect.actionId !== 0) {
      const actionId = finiteNumber(effect.actionId, `effects[${index}].actionId`);
      if (!actionIds.has(actionId)) {
        throw new CombatSchemaError(`effects[${index}] 引用了不存在的操作`);
      }
    }
    if (typeof effect.type !== "string" || typeof effect.sourceId !== "string") {
      throw new CombatSchemaError(`effects[${index}] 缺少结果类型或来源`);
    }
    if (effect.type === "statusApply" || effect.type === "statusRefresh" || effect.type === "statusRemove") {
      unsignedIntegerString(effect.statusInstanceId, `effects[${index}].statusInstanceId`);
    }
  }
  return analyze(raw as unknown as CombatRecordRaw, dictionary);
}

export function parseCombatRecord(text: string, dictionary?: CombatDictionary): CombatRecord {
  let raw: unknown;
  try {
    raw = JSON.parse(text);
  } catch {
    throw new CombatSchemaError("文件不是有效的 JSON");
  }
  return parseCombatRecordObject(raw, dictionary);
}

export function totalRdps(record: CombatRecord): number {
  return record.summary.rdps;
}

export function formatNumber(value: number, digits = 0): string {
  return new Intl.NumberFormat("zh-CN", {
    maximumFractionDigits: digits,
    minimumFractionDigits: digits,
  }).format(Number.isFinite(value) ? value : 0);
}

export function formatCompact(value: number): string {
  const abs = Math.abs(value);
  if (abs >= 1e8) return `${(value / 1e8).toFixed(2)}亿`;
  if (abs >= 1e4) return `${(value / 1e4).toFixed(abs >= 1e6 ? 0 : 1)}万`;
  return formatNumber(value);
}

export function formatDuration(seconds: number): string {
  const minutes = Math.floor(seconds / 60);
  const rest = seconds - minutes * 60;
  return `${String(minutes).padStart(2, "0")}:${rest.toFixed(2).padStart(5, "0")}`;
}

export function formatClock(seconds: number, digits = 2): string {
  const minutes = Math.floor(seconds / 60);
  const rest = seconds - minutes * 60;
  return `${minutes}:${rest.toFixed(digits).padStart(digits ? 3 + digits : 2, "0")}`;
}

export function characterName(dict: CombatDictionary, id: string): string {
  const value = dict.c[id];
  if (typeof value === "string") return value;
  if (value?.n) return value.n;
  if (id === "<unknown>") return "未知角色";
  return id;
}

export function characterIcon(dict: CombatDictionary, id: string): string {
  const value = dict.c[id];
  return typeof value === "string" ? "" : value?.i ? `./${value.i}` : "";
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

// Shared character actions have no entry in the exported skill table.
/** Dodges, dashes and the other movement actions every character shares. */
const MOBILITY_SKILL_IDS = new Set([
  "common_character_dodge",
  "common_character_perfect_dodge",
  "common_character_dash",
  "common_character_ai_dash",
  "common_character_jump",
  "common_character_sprint",
]);

export function isMobilityClip(clip: Pick<ActionClip, "type" | "skillId">): boolean {
  return clip.type === "dodge" || MOBILITY_SKILL_IDS.has(clip.skillId);
}

const COMMON_SKILL_NAMES: Record<string, string> = {
  common_character_perfect_dodge: "完美闪避",
  common_character_dodge: "闪避",
  common_character_dash: "冲刺",
  common_character_ai_dash: "冲刺",
  common_character_jump: "跳跃",
  common_character_sprint: "疾跑",
};

export function skillName(dict: CombatDictionary, id: string): string {
  const value = skillEntry(dict, id);
  if (typeof value === "string") return value;
  if (value?.n) return value.n;
  if (!id) return "未知技能";
  if (COMMON_SKILL_NAMES[id]) return COMMON_SKILL_NAMES[id];
  // Fallback: humanise the runtime skill id ("chr_0032_lizhiyan_attack4" → "attack4").
  const tail = id.replace(/^chr_\d+_[a-z0-9]+_/, "").replace(/^common_character_/, "");
  return tail || id;
}

export interface SkillDisplay {
  name: string;
  icon: string;
  groupId: string;
  level: number;
  maxLevel: number;
  category: string;
}

/**
 * Runtime skill id → its skill group's name, icon and the level this squad had.
 * The exported table keys groups (`chr_0032_lizhiyan_NormalAttack`) and lists
 * the runtime ids each group covers in `ids`.
 */
export function buildSkillIndex(
  squad: { charId: string; skillGroups: SkillLevel[] }[],
  dict: CombatDictionary,
): Map<string, SkillDisplay> {
  const levelByGroup = new Map<string, SkillLevel>();
  for (const member of squad) {
    for (const skill of member.skillGroups) {
      const key = skill.skillGroupId || skill.skillId;
      if (key) levelByGroup.set(key, skill);
    }
  }
  const index = new Map<string, SkillDisplay>();
  for (const [groupId, value] of Object.entries(dict.sk)) {
    if (typeof value === "string") continue;
    const level = levelByGroup.get(groupId);
    const display: SkillDisplay = {
      name: value.n || groupId,
      icon: value.i ? iconPath("skills", value.i) : "",
      groupId,
      level: level?.level ?? 0,
      maxLevel: level?.maxLevel ?? 0,
      category: value.c || "",
    };
    for (const id of [groupId, ...(value.ids ?? [])]) index.set(id, display);
  }
  return index;
}

export function skillDisplay(
  index: Map<string, SkillDisplay>,
  dict: CombatDictionary,
  id: string,
): SkillDisplay {
  const found = index.get(id);
  if (found) return found;
  return { name: skillName(dict, id), icon: "", groupId: "", level: 0, maxLevel: 0, category: "" };
}

export function weaponName(dict: CombatDictionary, id: string): string {
  const value = dict.w[id];
  return typeof value === "string" ? value : value?.n || id;
}

export function suitName(dict: CombatDictionary, id: string): string {
  const value = dict.su[id];
  return typeof value === "string" ? value : value?.n || id;
}

export function talentName(dict: CombatDictionary, id: string): string {
  if (dict.t[id]) return dict.t[id];
  // Squad snapshots key talent groups as `chr_0032_lizhiyan_talent1`, while the
  // exported table keys the individual ranks as `..._talent_1_1`.
  const group = /^(chr_\d+_[a-z0-9]+)_talent(\d+)$/.exec(id);
  if (group) return dict.t[`${group[1]}_talent_${group[2]}_1`] || "";
  return "";
}

/** Human-readable source of a buff: "武器 · 长枪名" / "战技 · 技能名" / "装备套装 · 套装名". */
export function buffSourceLabel(
  dict: CombatDictionary,
  sourceKind: string,
  sourceTemplateId: string,
  sourceSkillId: string,
): string {
  switch (sourceKind) {
    case "weapon": return `武器 · ${weaponName(dict, sourceTemplateId)}`;
    case "equipSuit": return `套装 · ${suitName(dict, sourceTemplateId)}`;
    case "characterSkill": return `技能 · ${skillName(dict, sourceSkillId || sourceTemplateId)}`;
    case "characterPassive": {
      const talent = talentName(dict, sourceSkillId) || talentName(dict, sourceTemplateId);
      return talent ? `天赋 · ${talent}` : `天赋 · ${characterName(dict, sourceTemplateId)}`;
    }
    case "character": return `角色 · ${characterName(dict, sourceTemplateId)}`;
    case "system": return "系统";
    default: return "";
  }
}

export interface BuffDisplay {
  /** Best available human name, e.g. "曜夜的首演 · damageup". */
  name: string;
  icon: string;
  /** Where it comes from, e.g. "武器" / "套装" / "天赋". */
  kind: string;
}

const ELEMENT_NAMES: Record<string, string> = {
  physical: "物理", fire: "灼热", pulse: "电磁", cryst: "晶体", crystal: "晶体",
  natural: "自然", spell: "法术", ether: "以太",
};

// Suits are keyed by id but buffs reference their passive skill, so the lookup
// needs the reverse direction. Cached per dictionary object.
const suitByPassiveCache = new WeakMap<CombatDictionary, Map<string, string>>();

function suitByPassive(dict: CombatDictionary): Map<string, string> {
  const cached = suitByPassiveCache.get(dict);
  if (cached) return cached;
  const index = new Map<string, string>();
  for (const [suitId, value] of Object.entries(dict.su)) {
    if (typeof value === "string") continue;
    if (value.sid) index.set(value.sid, suitId);
    for (const bonus of value.b ?? []) {
      if (bonus.skillId) index.set(bonus.skillId, suitId);
    }
  }
  suitByPassiveCache.set(dict, index);
  return index;
}

/**
 * Qualifies a source name so two statuses from the same weapon/suit/skill stay
 * apart. The client ships no buff names, but the runtime measured what each one
 * actually does, so the contribution category is used as the qualifier and the
 * raw id fragment is only the fallback for statuses that never took effect.
 */
function withQualifier(base: string, tail: string, contributionType?: number): string {
  if (contributionType !== undefined) {
    const category = CONTRIBUTION_CATEGORIES[contributionType];
    if (category && category.id !== "other" && category.id !== "direct") {
      return `${base} · ${category.name}`;
    }
  }
  const cleaned = localizeIdTail(tail);
  return cleaned ? `${base} · ${cleaned}` : base;
}

export interface BuffDisplayInput {
  buffId: string;
  sourceKind?: string;
  sourceTemplateId?: string;
  sourceSkillId?: string;
  sourceId?: string;
  contributionType?: number;
}

/**
 * Name and icon for a status.
 *
 * The name is derived from the source, because the client ships none — see
 * buffDisplayFromSource. The icon is not: BuffIconConfig._spritePath is the
 * status's own glyph, and the shipped buff table has it for 473 of them. It
 * wins whenever it is loaded, since every fallback below is really a portrait
 * or skill icon belonging to whoever applied the buff, not to the buff.
 */
export function buffDisplay(dict: CombatDictionary, buff: BuffDisplayInput): BuffDisplay {
  const display = buffDisplayFromSource(dict, buff);
  const sprite = buffIconPath(buff.buffId ?? "");
  return sprite ? { ...display, icon: sprite } : display;
}

/**
 * The exported tables carry no buff names, so the display is built from
 * whatever produced it — weapon, suit passive, skill, talent or character —
 * with the remaining id fragment kept as a suffix so two buffs from the same
 * source stay distinguishable.
 */
function buffDisplayFromSource(dict: CombatDictionary, buff: BuffDisplayInput): BuffDisplay {
  const id = buff.buffId ?? "";
  const qualify = (base: string, tail: string) => withQualifier(base, tail, buff.contributionType);
  const weapon = (templateId: string, tail: string): BuffDisplay | null => {
    const entry = dict.w[templateId];
    if (!entry) return null;
    const value = typeof entry === "string" ? { n: entry, sn: "", i: "" } : entry;
    return { name: qualify(value.sn || value.n || templateId, tail), icon: iconPath("items", value.i), kind: "武器" };
  };
  const suit = (suitId: string, tail: string): BuffDisplay | null => {
    const entry = dict.su[suitId];
    if (!entry) return null;
    const value = typeof entry === "string" ? { n: entry, sn: "", i: "" } : entry;
    return { name: qualify(value.n || suitId, tail), icon: iconPath("suits", value.i), kind: "套装" };
  };
  const character = (charId: string, tail: string): BuffDisplay | null => {
    if (!dict.c[charId]) return null;
    return { name: qualify(characterName(dict, charId), tail), icon: characterIcon(dict, charId), kind: "角色" };
  };
  const skill = (skillId: string, tail: string): BuffDisplay | null => {
    const entry = skillEntry(dict, skillId);
    if (!entry || typeof entry === "string") return null;
    return { name: qualify(entry.n || skillId, tail), icon: iconPath("skills", entry.i), kind: "技能" };
  };

  // 1. Runtime told us exactly where it came from.
  switch (buff.sourceKind) {
    case "weapon": {
      const found = weapon(buff.sourceTemplateId ?? "", id.replace(/^buff_wpn_[a-z]+_\d+/, ""));
      if (found) return found;
      break;
    }
    case "equipSuit": {
      const found = suit(buff.sourceTemplateId ?? "", id.replace(/^buff_equipsuit_[a-z0-9]+_\d+/, ""));
      if (found) return found;
      break;
    }
    case "characterSkill": {
      const found = skill(buff.sourceSkillId || buff.sourceTemplateId || "", "");
      if (found) return found;
      break;
    }
    case "characterPassive": {
      const talent = talentName(dict, buff.sourceSkillId ?? "");
      const owner = buff.sourceTemplateId ?? "";
      if (talent) return { name: talent, icon: characterIcon(dict, owner), kind: "天赋" };
      const found = character(owner, id.replace(/^buff_chr_\d+_[a-z0-9]+/, ""));
      if (found) return { ...found, kind: "天赋" };
      break;
    }
    case "character": {
      const found = character(buff.sourceTemplateId ?? "", "");
      if (found) return found;
      break;
    }
    default:
      break;
  }

  // 2. Derive it from the id, which encodes the source for most statuses.
  const weaponMatch = /^buff_wpn_([a-z]+_\d+)(.*)$/.exec(id);
  if (weaponMatch) {
    const found = weapon(`wpn_${weaponMatch[1]}`, weaponMatch[2]);
    if (found) return found;
  }
  const suitMatch = /^buff_equipsuit_([a-z0-9]+_\d+)(.*)$/.exec(id);
  if (suitMatch) {
    const suitId = suitByPassive(dict).get(`passive_equipsuit_${suitMatch[1]}`);
    const found = suitId ? suit(suitId, suitMatch[2]) : null;
    if (found) return found;
  }
  const affixMatch = /^buff_common_affixes_(enhance|vulnerable)_([a-z]+)/.exec(id);
  if (affixMatch) {
    const element = ELEMENT_NAMES[affixMatch[2]] ?? affixMatch[2];
    return {
      name: `${element}${affixMatch[1] === "enhance" ? "增幅" : "脆弱"}`,
      icon: buff.sourceId ? characterIcon(dict, buff.sourceId) : "",
      kind: "词条",
    };
  }
  const charMatch = /^buff_chr_(\d+_[a-z0-9]+)(.*)$/.exec(id);
  if (charMatch) {
    const found = character(`chr_${charMatch[1]}`, charMatch[2]);
    if (found) return found;
  }
  // Elemental reactions and other shared statuses: keep the element in Chinese
  // and the mechanic token as-is, since the client ships no name for them.
  const commonMatch = /^buff_common_([a-z]+)_(.+)$/.exec(id);
  if (commonMatch && ELEMENT_NAMES[commonMatch[1]]) {
    return {
      name: qualify(ELEMENT_NAMES[commonMatch[1]], commonMatch[2]),
      icon: buff.sourceId ? characterIcon(dict, buff.sourceId) : "",
      kind: "反应",
    };
  }

  // 3. Nothing resolved: keep the trimmed id, and borrow the provider's face.
  return {
    name: buffShortName(id),
    icon: buff.sourceId ? characterIcon(dict, buff.sourceId) : "",
    kind: "",
  };
}

/**
 * Short label for a buff id: strips the shared prefixes so the track fits, then
 * runs the remaining tokens through the hand-written vocabulary. The client
 * ships no buff names, so this is the last line of defence -- without it the
 * label is a raw English id like `combo_skill_seal`.
 */
export function buffShortName(buffId: string): string {
  // The character segment goes too: whoever owns the buff is already the main
  // label, so repeating `lizhiyan` here only adds romanisation.
  const stripped = buffId
    .replace(/^buff_/, "")
    .replace(/^chr_\d+_[a-z0-9]+_?/, "")
    .replace(/^common_/, "")
    .replace(/^wpn_/, "武器·")
    .replace(/^equipsuit_/, "套装·")
    .replace(/^eny_\d+_/, "敌·");
  const separator = stripped.lastIndexOf("·");
  const prefix = separator < 0 ? "" : stripped.slice(0, separator + 1);
  const rest = separator < 0 ? stripped : stripped.slice(separator + 1);
  return prefix + (localizeIdTail(rest) || rest);
}

export function iconPath(kind: "skills" | "items" | "suits", iconId?: string): string {
  if (!iconId) return "";
  return `./icons/${kind}/${iconId}.png`;
}

export const ACTION_RESULT_LABELS: Record<string, string> = {
  completed: "完整释放",
  cancelled: "取消",
  interrupted: "被打断",
  superseded: "被后续动作接替",
  openAtSessionEnd: "战斗结束时仍在进行",
};

export const INTERRUPT_REASON_LABELS: Record<string, string> = {
  default: "默认",
  enterFreeState: "回到自由状态",
  aiManual: "AI/手动切换",
  mud: "被淤泥打断",
  detachSkill: "技能脱离",
  interruptAction: "被动作打断",
  dash: "闪避取消",
  castNextSkill: "接续下一技能",
  levelScript: "关卡脚本",
  narrative: "剧情",
};
