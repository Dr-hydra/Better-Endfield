// Shipped resource tables: the id space BEC snapshots index into, and the
// grouped stage list the leaderboard navigates by. Both are produced by
// tools/CombatDataExporter and versioned with the game data.

import dictionaryJson from "../data/combat-dict.min.json";
import stagesJson from "../data/combat-stages.min.json";
import { createIdSpace, type IdSpace } from "./bec";
import type { CombatDictionary } from "../types";

export const dictionary = dictionaryJson as unknown as CombatDictionary;

export const ID_SPACE_VERSION = (dictionaryJson as unknown as { idv?: number }).idv ?? 0;

/**
 * Permanent numeric ids. The registry is append-only, so an index recorded in
 * an old snapshot still resolves after the game data is re-exported.
 *
 * The table is ~150 KB of strings and only matters when a snapshot is encoded
 * or decoded, so it loads on demand instead of riding the main bundle.
 */
let cachedSpace: IdSpace | null = null;
let pending: Promise<IdSpace> | null = null;

export async function ensureIdSpace(): Promise<IdSpace> {
  if (cachedSpace) return cachedSpace;
  pending ??= import("../data/combat-ids.min.json").then((module) => {
    const table = (module.default ?? module) as { version: number; ids: string[] };
    cachedSpace = createIdSpace(table.ids, table.version);
    return cachedSpace;
  });
  return pending;
}

/** The table once loaded, for render paths that cannot await. */
export function loadedIdSpace(): IdSpace | null {
  return cachedSpace;
}

export interface StageEntry {
  /** Stage name. */
  n: string;
  /** Sub-name, usually empty. */
  sn: string;
  /** Series id, e.g. 危境再现·罗丹. */
  g: string;
  /** In-game category id, e.g. dungeon_seasontower. */
  c: string;
  /** Difficulty rank inside its family; only ordinal within one family. */
  d: number;
}

export interface StageCategory {
  id: string;
  n: string;
  /** Hardest difficulty of every stage in the category, in in-game order. */
  s: string[];
}

interface StageTable {
  version: number;
  cats: StageCategory[];
  series: Record<string, string>;
  stages: Record<string, StageEntry>;
}

export const stageTable = stagesJson as unknown as StageTable;

/** 战争回响 — the tab people actually compete on, so the board opens here. */
export const DEFAULT_CATEGORY = "dungeon_seasontower";

export const stageCategories = stageTable.cats;

export function stageName(dungeonId: string): string {
  return stageTable.stages[dungeonId]?.n || dungeonId || "未知关卡";
}

export function stageSubName(dungeonId: string): string {
  return stageTable.stages[dungeonId]?.sn || "";
}

export function stageGroupName(dungeonId: string): string {
  return stageTable.series[stageTable.stages[dungeonId]?.g ?? ""] || "";
}

export function stageCategory(dungeonId: string): StageCategory | undefined {
  const categoryId = stageTable.stages[dungeonId]?.c;
  return stageTable.cats.find((category) => category.id === categoryId);
}

export function findCategory(categoryId: string): StageCategory | undefined {
  return stageTable.cats.find((category) => category.id === categoryId);
}
