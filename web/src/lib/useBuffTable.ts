import { useEffect, useState } from "preact/hooks";
import {
  buffTableReady,
  buffTableVersion,
  ensureBuffTable,
  ensureFullBuffTable,
  missingFromLoadedTables,
  subscribeBuffTable,
} from "./buffTable";
import type { CombatRecord } from "../types";

/**
 * Loads the buff table for a record and returns a version that changes when it
 * arrives, so memoised displays recompute.
 *
 * The full half is pulled only when the record actually contains a status the
 * main half does not describe. Snapshots normally do not: the `semantic` lane
 * filter drops the marker statuses before they are ever encoded.
 */
export function useBuffTable(record: CombatRecord | null): number {
  const [version, setVersion] = useState(buffTableVersion);

  useEffect(() => subscribeBuffTable(() => setVersion(buffTableVersion())), []);
  useEffect(() => { void ensureBuffTable(); }, []);
  useEffect(() => {
    if (!record || !buffTableReady()) return;
    if (record.buffIntervals.some((interval) => missingFromLoadedTables(interval.buffId))) {
      void ensureFullBuffTable();
    }
  }, [record, version]);

  return version;
}
