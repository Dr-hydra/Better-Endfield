// The shipped buff mechanics table, produced by tools/CombatDataExporter.
//
// The client has no buff names, but BuffIconConfig does carry a real sprite
// path, so a status can at least show its own icon instead of borrowing the
// face of whoever applied it. That is what this table is mostly for; the
// mechanics columns are secondary now that the recorder measures the resolved
// magnitudes per instance (statusApply.nominalEffects).
//
// Rows are keyed by id-registry index rather than by buff id, because the
// registry already carries all 2,872 ids for snapshots to reference and
// repeating the strings would cost about 100 KB of bundle. That makes the id
// space a hard dependency, so both load together, off the main bundle.
//
// Two tables, following the exporter's split. The main one holds every buff
// that touches damage, healing, poise or shields (686 rows, 57 sprites); the
// full one holds the markers, VFX carriers and AI states (2,186 rows, 82
// sprites) and only loads when a record actually contains one.

import { ensureIdSpace } from "./dict";

export interface BuffRow {
  /** Sprite leaf name under icons/buffs. */
  ic?: string;
  /** The game shows this status on the HUD. */
  hi?: number;
  /** Configured duration in seconds, or a blackboard key with no static value. */
  d?: number | { k: string };
  /** Buff.LifeType ordinal. */
  lt?: number;
  /** GameplayTag names. */
  tg?: string[];
}

interface BuffTableFile {
  tableVersion: number;
  idv: number;
  b: Record<string, BuffRow>;
}

let main: Map<string, BuffRow> | null = null;
let full: Map<string, BuffRow> | null = null;
let version = 0;
const listeners = new Set<() => void>();

function publish(): void {
  version += 1;
  for (const listener of listeners) listener();
}

export function buffTableVersion(): number {
  return version;
}

export function subscribeBuffTable(listener: () => void): () => void {
  listeners.add(listener);
  return () => listeners.delete(listener);
}

async function loadTable(
  file: Promise<{ default: unknown }>,
): Promise<Map<string, BuffRow>> {
  const [space, module] = await Promise.all([ensureIdSpace(), file]);
  const table = ((module as { default?: unknown }).default ?? module) as BuffTableFile;
  const rows = new Map<string, BuffRow>();
  // The registry is append-only, so an index is stable; walking it backwards
  // from the id is the only lookup direction the id space offers.
  for (const [index, row] of Object.entries(table.b ?? {})) {
    const buffId = space.id(Number(index));
    if (buffId) rows.set(buffId, row);
  }
  return rows;
}

let mainPending: Promise<void> | null = null;
let fullPending: Promise<void> | null = null;

export function ensureBuffTable(): Promise<void> {
  if (main) return Promise.resolve();
  mainPending ??= loadTable(import("../data/combat-buffs.min.json"))
    .then((rows) => { main = rows; publish(); })
    .catch(() => { mainPending = null; });
  return mainPending;
}

export function ensureFullBuffTable(): Promise<void> {
  if (full) return Promise.resolve();
  fullPending ??= loadTable(import("../data/combat-buffs-full.min.json"))
    .then((rows) => { full = rows; publish(); })
    .catch(() => { fullPending = null; });
  return fullPending;
}

/** Whether the main half is in memory, so callers can tell absent from unloaded. */
export function buffTableReady(): boolean {
  return main !== null;
}

export function buffRow(buffId: string): BuffRow | undefined {
  if (!buffId) return undefined;
  return main?.get(buffId) ?? full?.get(buffId);
}

/**
 * True once the main table is loaded and the id is in neither half, which is
 * the signal to pull the full one in.
 */
export function missingFromLoadedTables(buffId: string): boolean {
  return main !== null && full === null && !main.has(buffId);
}

export function buffIconPath(buffId: string): string {
  const sprite = buffRow(buffId)?.ic;
  return sprite ? `./icons/buffs/${sprite}.png` : "";
}

/** Configured duration in seconds, or null when it is resolved at runtime. */
export function buffConfiguredDuration(buffId: string): number | null {
  const duration = buffRow(buffId)?.d;
  return typeof duration === "number" && duration > 0 ? duration : null;
}
