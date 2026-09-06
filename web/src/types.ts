export * from "./types.combat";

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
  bvid?: string;
  durationSeconds: number;
  dps: number;
  rdps: number;
  totalDamage: number;
  uploadedAt: string;
  dungeonId: string;
  /** BEC layer bitmask, so the row can say what the detail view will offer. */
  layers?: number;
  idSpaceVersion?: number;
  squad: Array<{ charId: string; level: number; potential: number }>;
  /** Present on /me/records: whether the record is currently held by a board. */
  ranked?: boolean;
  /** Present on /me/records: whether the uploader wants it on a board at all. */
  optIn?: boolean;
  expireAt?: string | null;
  example?: boolean;
}

export type BoardMetric = "dps" | "time";

export interface BoardPayload {
  dungeonId: string;
  dps: LeaderboardEntry[];
  time: LeaderboardEntry[];
  entries: number;
  updatedAt: string;
}

/** Category overview: the top few of every stage that has records. */
export interface CategoryBoard {
  categoryId: string;
  updatedAt: number;
  stages: Record<string, { entries: number; dps: LeaderboardEntry[]; time: LeaderboardEntry[] }>;
}

export interface PublicRecord {
  shortId: string;
  layers: number;
  idSpaceVersion: number;
  dungeonId: string;
  nickname: string;
  avatar?: string;
  bvid?: string;
  uploadedAt: string;
  ranked: boolean;
  expireAt?: string | null;
}

export interface GachaWebSnapshot {
  schemaVersion: number;
  kind: string;
  createdAt: string;
  categories: GachaSnapshotCategory[];
  pools: GachaSnapshotPool[];
}

export interface GachaSnapshotCategory {
  id: string;
  name: string;
  totalPulls: number;
  sixStarCount: number;
  upCount: number;
  offRateCount: number;
  upAveragePaidPulls: number | null;
}

export interface GachaSnapshotPool {
  poolId: string | null;
  poolType: string;
  categoryId: string;
  category: string;
  poolName: string;
  totalPulls: number;
  freePulls: number;
  sixStarCount: number;
  upCount: number;
  offRateCount: number;
  startingPity: number;
  currentPity: number;
  lastSeqId: string | null;
  sixStars: GachaSnapshotStar[];
  freeSixStars: GachaSnapshotStar[];
}

export interface GachaSnapshotStar {
  id: string;
  name: string;
  itemId: string | null;
  pity: number;
  carryPity: number;
  isUp: boolean;
  isFree: boolean;
  seqId: string;
  gachaTs: string | null;
}

export type Route =
  | { page: "home" }
  | { page: "board"; dungeonId?: string; categoryId?: string }
  | { page: "analyze" }
  | { page: "gacha" }
  | { page: "record"; id?: string }
  | { page: "archive" }
  /** Desktop handoff: fetch one record from the app's loopback port. */
  | { page: "import"; port: number; nonce: string }
  | { page: "download" };
