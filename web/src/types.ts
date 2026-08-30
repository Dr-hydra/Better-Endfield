export interface Aggregate {
  damage: number;
  hits: number;
  criticalHits: number;
}

export interface CharacterAggregate extends Aggregate {
  damageByCategory?: number[];
  rdpsByContribution?: number[];
}

export interface SkillLevel {
  skillId?: string;
  skillGroupId?: string;
  level: number;
  maxLevel: number;
}

export interface WeaponSnapshot {
  templateId: string;
  level: number;
  refineLevel: number;
  breakthroughLevel: number;
  skills: SkillLevel[];
}

export interface EquipmentSnapshot {
  slot: number;
  templateId: string;
  enhanceLevels: Record<string, number>;
}

export interface EquipSuitSnapshot {
  suitId: string;
  count: number;
  skills: SkillLevel[];
}

export interface SquadMemberV11 {
  charId: string;
  charInstId: string;
  level: number;
  breakStage: number;
  potential: number;
  skillGroups: SkillLevel[];
  weapon: WeaponSnapshot | null;
  equipments: EquipmentSnapshot[];
  equipSuits: EquipSuitSnapshot[];
}

export interface RdpsShare {
  source?: string;
  sourceId?: string;
  fraction: number;
  kind?: number | string;
  contributionType?: number;
  contributionKind?: string;
}

export interface DamageEvent {
  id?: number;
  actionId?: number;
  time: number;
  damage: number;
  hpDelta?: number;
  damageType?: number;
  decorateMask?: number;
  critical?: boolean;
  attackerId: string;
  sourceEntityId?: string;
  actorAttribution?: "runtimeExact" | "dictionaryExact" | "unknown";
  attributionReason?: string;
  originSkillId: string;
  rdpsShares?: RdpsShare[];
}

export type CombatActionType =
  | "basicAttack" | "skill" | "ultimate" | "combo"
  | "switch" | "dodge" | "passive" | "system" | "skillCast";

export interface CombatAction {
  id: number;
  start: number;
  end: number | null;
  observedUntil?: number;
  actorId: string;
  type: CombatActionType;
  skillId?: string;
  stage?: number;
  parentActionId?: number;
  result: "completed" | "cancelled" | "interrupted" |
    "superseded" | "openAtSessionEnd";
}

export interface CombatEffect {
  id: number;
  actionId?: number;
  parentEffectId?: number;
  time: number;
  type: "damage" | "heal" | "statusApply" | "statusRefresh" |
    "statusRemove" | "resource" | "stagger";
  sourceId: string;
  sourceEntityId?: string;
  creditedActorId?: string;
  actorAttribution?: "runtimeExact" | "unknown";
  sourceAttribution?: "configurationVerified" | "unknown";
  sourceKind?: "character" | "characterSkill" | "characterPassive" | "weapon" | "equipSuit" | "system" | "unknown";
  sourceTemplateId?: string;
  sourceSkillId?: string;
  attributionReason?: string;
  targetId?: string;
  targetEntityId?: string;
  skillId?: string;
  value?: number;
  hpDelta?: number;
  critical?: boolean;
  damageType?: number;
  decorateMask?: number;
  statusId?: string;
  statusInstanceId?: string;
  statusKind?: "buff" | "debuff" | "stagger";
  stack?: number;
  contributionType?: number;
  contributionKind?: string;
  effectKind?: number;
  effectMin?: number;
  effectMax?: number;
  rdpsShares?: RdpsShare[];
}

export interface BuffInterval {
  buffId: string;
  sourceId?: string;
  targetId?: string;
  targetEntityId?: string;
  skillId?: string;
  originSkillId?: string;
  start: number;
  end: number;
  kind?: number | string;
  contributionType?: number;
  contributionKind?: string;
  effectMin?: number;
  effectMax?: number;
  relevant?: boolean;
  sourceEntityId?: string;
  creditedActorId?: string;
  actorAttribution?: "runtimeExact" | "unknown";
  sourceAttribution?: "configurationVerified" | "unknown";
  sourceKind?: "character" | "characterSkill" | "characterPassive" | "weapon" | "equipSuit" | "system" | "unknown";
  sourceTemplateId?: string;
  sourceSkillId?: string;
  attributionReason?: string;
}

export interface StaggerInterval {
  targetId?: string;
  buffId?: string;
  start: number;
  end: number;
}

export interface CombatRecordV11Raw {
  schemaVersion: 11;
  battle: {
    sessionId: number;
    startedUnixSeconds: number;
    durationSeconds: number;
    dungeonId: string;
    modeId?: string;
  };
  dictionary: {
    schemaVersion: number;
    gameVersion: string;
    hotfixVersion: string;
    sourceSha256: string;
  };
  squad: SquadMemberV11[];
  actions: CombatAction[];
  effects: CombatEffect[];
  summary: {
    totalDamage: number;
    dps: number;
    rdps: number;
    hitCount: number;
    criticalCount: number;
  };
}

export interface CombatRecordV11 extends CombatRecordV11Raw {
  sessionId: number;
  startedUnixSeconds: number;
  durationSeconds: number;
  totalDamage: number;
  dps: number;
  hitCount: number;
  criticalCount: number;
  dungeonId: string;
  modeId?: string;
  squad: SquadMemberV11[];
  characters: Record<string, CharacterAggregate>;
  rdpsCharacters: Record<string, CharacterAggregate>;
  skills: Record<string, Aggregate>;
  damageCategories: Record<string, Aggregate>;
  timeline: unknown[];
  buffIntervals: BuffInterval[];
  staggerIntervals: StaggerInterval[];
  events: DamageEvent[];
}

export interface CharacterDictionaryEntry {
  n: string;
  r: number;
  p: number;
  wt: number;
  i: string;
}

export interface SkillDictionaryEntry {
  n: string;
  c: string;
  i: string;
  ids: string[];
  ch?: string;
}

export interface WeaponDictionaryEntry {
  n: string;
  r: number;
  t: number;
  i: string;
  sid: string;
  sn: string;
}

export interface EquipmentDictionaryEntry {
  n: string;
  r: number;
  i: string;
  p: number;
  su: string;
  lv: number;
  m: Array<{
    index: number;
    type: number;
    value: number;
    modifierType: number;
    enhancedValues?: number[];
    base: boolean;
  }>;
}

export interface SuitDictionaryEntry {
  n: string;
  i: string;
  sid: string;
  sn: string;
  b: Array<{ count: number; skillId: string; skillLevel: number }>;
}

export interface CombatDictionary {
  schemaVersion: number;
  exportedAt: string;
  c: Record<string, CharacterDictionaryEntry | string>;
  sk: Record<string, SkillDictionaryEntry | string>;
  t: Record<string, string>;
  w: Record<string, WeaponDictionaryEntry | string>;
  eq?: Record<string, EquipmentDictionaryEntry>;
  su: Record<string, SuitDictionaryEntry | string>;
  d: Record<string, string | { n: string; sn: string; s: string; m?: string }>;
  ds: Record<string, string>;
  zones: string[];
}

export interface LeaderboardEntry {
  shortId: string;
  rank: number;
  nickname: string;
  avatar?: string;
  durationSeconds: number;
  dps: number;
  rdps: number;
  uploadedAt: string;
  dungeonId: string;
  squad: Array<{ charId: string; level: number; potential: number }>;
  example?: boolean;
}

export interface HomePayload {
  dungeonId: string;
  dps: LeaderboardEntry[];
  time: LeaderboardEntry[];
}

export type Route =
  | { page: "home" }
  | { page: "analyze" }
  | { page: "record"; id?: string }
  | { page: "archive" }
  | { page: "download" };
