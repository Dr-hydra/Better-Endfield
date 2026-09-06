// Schema 13 combat record types (raw file shape) and the analysed model the
// UI renders. Mirrors the writer in native/modules/combat_stats/module.cpp.

export interface Aggregate {
  damage: number;
  hits: number;
  criticalHits: number;
}

export interface CharacterAggregate extends Aggregate {
  damageByCategory: number[];
  rdpsByContribution: number[];
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

export interface SquadMember {
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
  sourceId: string;
  fraction: number;
  contributionType: number;
  contributionKind: string;
}

export type ActionResult = "completed" | "cancelled" | "interrupted" | "superseded" | "openAtSessionEnd";

export interface CombatAction {
  id: number;
  start: number;
  end: number | null;
  observedUntil?: number;
  actorId: string;
  type: "skillCast" | "dodge" | string;
  skillId?: string;
  parentActionId?: number;
  result: ActionResult;
  finishType?: "completed" | "interrupted";
  interruptReason?: string;
}

export interface HitFactors {
  valid: boolean;
  attack: number;
  attackLinked: boolean;
  zoneProduct: number;
  defense: number | null;
  defResistance: number | null;
  typeResistance: number | null;
  critical: boolean;
  critRate: number;
  critDamage: number;
  weakness: number | null;
  shelter: number;
  ignite: number | null;
  igniteApplies: boolean;
}

export interface LedgerZoneWrite {
  statusInstanceId: string;
  statusId: string;
  sourceId: string;
  ownerId: string;
  side: number;
  zoneIndex: number;
  zoneName: string;
  before: number;
  after: number;
  external: boolean;
  semantic: string;
}

export interface LedgerAttributeGroup {
  side: number;
  attributeType: number;
  values: number[];
  min: number | null;
  max: number | null;
  fullValue: number;
  packValue: number | null;
  liveEnumerated: boolean;
  liveBuffs: number;
  otherModifiers: number;
}

export interface LedgerContributor {
  statusInstanceId: string;
  statusId: string;
  sourceId: string;
  ownerId: string;
  mechanism: "zoneWrite" | "attribute" | "instantModifier" | "artsStrength" | string;
  contributionType: number;
  contributionKind: string;
  semantic: string;
  side: number;
  attributeType: number;
  zoneIndex: number;
  factor: number;
  fraction: number;
  external: boolean;
  excluded: boolean;
}

export interface HitLedger {
  originSkillId: string;
  calcResult: number;
  atkScale: number;
  attackerZones: number[];
  defenderZones: number[];
  factors: HitFactors;
  externalFraction: number;
  selfFraction: number;
  zoneWrites: LedgerZoneWrite[];
  zoneWritesDropped: number;
  attributeGroups: LedgerAttributeGroup[];
  attributeWrites: unknown[];
  contributors: LedgerContributor[];
  staleMapBuffs: number;
}

export type EffectType = "damage" | "heal" | "statusApply" | "statusRefresh" | "statusRemove" | "resource" | "stagger";

/**
 * BuffData.stackingSettings, as the game resolved it for this instance.
 * `typeId` decides whether two live instances of the same buff id add up
 * (`stack`), fold their layers into one instance (`enhance*`), or simply
 * replace each other (`refresh` / `highPriority` / `unique` / ...).
 */
export interface StatusStacking {
  type: number;
  typeId: string;
  maxStack: number;
  /** Peak layer count held by this single instance, for enhance-type buffs. */
  enhanceCount: number;
  /** true when the group is keyed by `key` instead of by buff id. */
  keyed: boolean;
  key: string;
}

/**
 * What a buff configures for one slot of one attribute, as the game resolved
 * it -- the nominal "+25%" behind a measured multiplier.
 *
 * A range, not a number, because the magnitude moves: enhance layers and
 * blackboard writes rewrite it mid-interval, and a modifier keyed on the damage
 * pack (enemies hit, stacks on the target) resolves anew on every hit. `min ===
 * max` is the common case and means the buff held one value throughout.
 *
 * `slot` is a Beyond.GEnums.ModifierType ordinal; see MODIFIER_SLOT_NAMES in
 * lib/buffTerms.ts for the formula it lands in.
 */
export interface NominalEffect {
  attributeType: number;
  slot: number;
  min: number;
  max: number;
}

export interface CombatEffect {
  id: number;
  actionId?: number;
  parentEffectId?: number;
  time: number;
  type: EffectType;
  sourceId: string;
  sourceEntityId?: string;
  creditedActorId?: string;
  actorAttribution?: "runtimeExact" | "unknown";
  sourceAttribution?: "configurationVerified" | "unknown";
  sourceKind?: string;
  sourceTemplateId?: string;
  sourceSkillId?: string;
  attributionReason?: string;
  targetId?: string;
  targetEntityId?: string;
  skillId?: string;
  value?: number;
  rawValue?: number;
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
  stacking?: StatusStacking;
  /** Schema 15 and later; absent on older records. */
  nominalEffects?: NominalEffect[];
  rdpsShares?: RdpsShare[];
  ledger?: HitLedger;
}

export interface ZoneInfo {
  index: number;
  name: string;
  multiply: boolean;
  mergeSides: boolean;
  damageTypeZone: boolean;
  roles: string[];
}

export interface AttributionDiagnostics {
  hits: number;
  hitsWithExternal: number;
  liveSetFailures: number;
  groupSumMismatch: number;
  zoneResidualUnexplained: number;
  lateBuffs: number;
  staleMapBuffs: number;
  attackUnlinked: number;
}

export interface CombatRecordRaw {
  schemaVersion: 14;
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
    zoneNames?: Record<string, Record<string, string>>;
    zones?: ZoneInfo[];
  };
  squad: SquadMember[];
  actions: CombatAction[];
  effects: CombatEffect[];
  diagnostics?: {
    attribution?: AttributionDiagnostics;
    [key: string]: unknown;
  };
  summary: {
    totalDamage: number;
    dps: number;
    rdps: number;
    hitCount: number;
    criticalCount: number;
  };
}

// ---------------------------------------------------------------------------
// Analysed model

export interface DamageEvent {
  id: number;
  actionId?: number;
  time: number;
  damage: number;
  hpDelta?: number;
  damageType?: number;
  decorateMask?: number;
  critical: boolean;
  attackerId: string;
  targetId: string;
  skillId: string;
  skillCategory: number;
  rdpsShares: RdpsShare[];
  ledger?: HitLedger;
  /** Fraction of this hit credited to other characters. */
  transferred: number;
}

export interface ActionClip {
  id: number;
  actorId: string;
  skillId: string;
  type: string;
  category: number;
  start: number;
  end: number;
  open: boolean;
  result: ActionResult;
  finishType?: string;
  interruptReason?: string;
  hits: DamageEvent[];
  damage: number;
}

export interface BuffInterval {
  instanceId: string;
  buffId: string;
  sourceId: string;
  ownerId: string;
  ownerEntityId: string;
  friendlyTarget: boolean;
  external: boolean;
  start: number;
  end: number;
  open: boolean;
  contributionType: number;
  contributionKind: string;
  sourceKind: string;
  sourceTemplateId: string;
  sourceSkillId: string;
  originSkillId: string;
  /** Damage credited to this instance across every hit ledger. */
  credited: number;
  hitsCredited: number;
  stacking?: StatusStacking;
  nominalEffects?: NominalEffect[];
  /**
   * Layer index of this instance within its stacking group, and how many
   * layers the group held while it was live. Derived, not recorded.
   */
  stackIndex: number;
  stackConcurrent: number;
  /** Evicted by a later application because its stacking group was full. */
  displaced?: boolean;
}

export interface StaggerInterval {
  targetId: string;
  buffId: string;
  start: number;
  end: number;
}

export interface FieldSegment {
  actorId: string;
  start: number;
  end: number;
}

export interface ContributorSummary {
  key: string;
  buffId: string;
  sourceId: string;
  contributionType: number;
  contributionKind: string;
  mechanism: string;
  semantic: string;
  external: boolean;
  excluded: boolean;
  hits: number;
  credited: number;
  /** Geometric mean of damage_with / damage_without over credited hits. */
  meanMultiplier: number;
  minMultiplier: number;
  maxMultiplier: number;
  owners: string[];
  sourceKind: string;
  sourceTemplateId: string;
  sourceSkillId: string;
  stacking?: StatusStacking;
  /** Highest layer count this buff was ever seen holding on one target. */
  peakLayers: number;
  /**
   * What this buff is configured to do, merged across every instance of it in
   * the fight. Read next to `meanMultiplier`: nominal is what the buff offers,
   * meanMultiplier is what the fight actually got out of it.
   */
  nominalEffects?: NominalEffect[];
}

export interface SkillAggregate extends Aggregate {
  actorId: string;
  category: number;
}

/** One fixed-point damage curve per actor, carried by snapshots for card previews. */
export interface SparkSeries {
  actorId: string;
  values: number[];
}

export interface CombatRecord extends CombatRecordRaw {
  /**
   * `events` means the analysed fields were derived here from a raw record;
   * `snapshot` means they were decoded already-computed, so per-hit ledger
   * drill-downs are unavailable and the UI must not offer them.
   */
  provenance?: "events" | "snapshot";
  /** Snapshots only: the BEC layer bitmask and id-space version they came with. */
  layers?: number;
  idSpaceVersion?: number;
  spark?: SparkSeries[];
  sessionId: number;
  startedUnixSeconds: number;
  durationSeconds: number;
  totalDamage: number;
  dps: number;
  hitCount: number;
  criticalCount: number;
  dungeonId: string;
  modeId?: string;
  characters: Record<string, CharacterAggregate>;
  rdpsCharacters: Record<string, CharacterAggregate>;
  skills: Record<string, SkillAggregate>;
  events: DamageEvent[];
  clips: ActionClip[];
  buffIntervals: BuffInterval[];
  staggerIntervals: StaggerInterval[];
  fieldSegments: FieldSegment[];
  contributors: ContributorSummary[];
  zones: ZoneInfo[];
}
