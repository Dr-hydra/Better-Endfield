import { useEffect, useState } from "preact/hooks";
import {
  ARCHIVE_TIERS,
  archiveQuota,
  estimateArchive,
  saveArchive,
  type ArchiveMeta,
  type ArchiveQuota,
} from "../lib/archive";
import type { IdSpace, TierName } from "../lib/bec";
import { ensureIdSpace, stageName } from "../lib/dict";
import type { CombatRecord } from "../types.combat";

/**
 * Tier picker for a Toy cloud save. The quota is shared with the gacha archive
 * and a save that would break it fails loudly, so the ledger is on screen
 * before the choice is made rather than after.
 */
export function ArchiveSaveDialog({
  record,
  onClose,
  onSaved,
}: {
  record: CombatRecord;
  onClose: () => void;
  onSaved: (meta: ArchiveMeta) => void;
}) {
  const [space, setSpace] = useState<IdSpace | null>(null);
  const [quota, setQuota] = useState<ArchiveQuota | null>(null);
  const [tier, setTier] = useState<TierName>("full");
  const [busy, setBusy] = useState(false);
  const [error, setError] = useState("");

  useEffect(() => {
    // The id table is a separate chunk; nothing here can encode until it lands.
    ensureIdSpace().then(setSpace).catch(() => setError("资源表加载失败"));
    archiveQuota().then(setQuota).catch((reason) => {
      setError(reason instanceof Error ? reason.message : "配额读取失败");
    });
  }, []);

  const estimate = space ? estimateArchive(record, space, { tier }) : null;
  // Each save also costs one index shard when it spills onto a new page.
  const tooLarge = Boolean(quota && estimate && estimate.parts + 1 > quota.free);

  async function save() {
    if (!space) return;
    setBusy(true);
    setError("");
    try {
      onSaved(await saveArchive(record, space, { tier, title: stageName(record.dungeonId) }));
    } catch (reason) {
      setError(reason instanceof Error ? reason.message : "保存失败");
      setBusy(false);
    }
  }

  return (
    <div class="modal-backdrop" onClick={onClose}>
      <div class="archive-dialog panel" onClick={(event) => event.stopPropagation()}>
        <div class="section-heading">
          <div>
            <span class="eyebrow">CLOUD SAVE / 上传云存档</span>
            <h2>{stageName(record.dungeonId)}</h2>
          </div>
          <button class="button ghost" onClick={onClose}>×</button>
        </div>

        {quota && (
          <p class="data-note">
            剩余 {quota.free} 片 · 战斗存档已用 {quota.used} 片 · 寻访记录 {quota.foreign} 片
          </p>
        )}

        <div class="tier-picker">
          {ARCHIVE_TIERS.map((item) => {
            const cost = space ? estimateArchive(record, space, { tier: item.id }) : null;
            return (
              <label key={item.id} class={`tier-option ${tier === item.id ? "active" : ""}`}>
                <input type="radio" name="tier" checked={tier === item.id} onChange={() => setTier(item.id)} />
                <b>{item.name}</b>
                <small>{item.hint}</small>
                <i>{cost ? `${cost.parts} 片 · ${(cost.bytes / 1024).toFixed(1)} KB` : "计算中…"}</i>
              </label>
            );
          })}
        </div>

        {error && <p class="empty-inline error">{error}</p>}
        {tooLarge && quota && estimate && (
          <p class="empty-inline error">
            剩余 {quota.free} 片不足以存放 {estimate.parts} 片，请先删除旧存档或换较小档位。
          </p>
        )}

        <div class="archive-actions">
          <button class="button secondary" onClick={onClose}>取消</button>
          <button class="button primary" onClick={() => void save()} disabled={busy || !space || tooLarge}>
            {busy ? "保存中…" : "保存到云端"}
          </button>
        </div>
      </div>
    </div>
  );
}
