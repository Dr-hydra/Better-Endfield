/// <reference lib="webworker" />

import { parseCombatRecord } from "../lib/combat";
import type { CombatDictionary } from "../types";

self.onmessage = (event: MessageEvent<{ text: string; dictionary: CombatDictionary }>) => {
  try {
    const record = parseCombatRecord(event.data.text, event.data.dictionary);
    self.postMessage({ ok: true, record });
  } catch (error) {
    self.postMessage({
      ok: false,
      error: error instanceof Error ? error.message : "解析失败",
    });
  }
};

export {};
