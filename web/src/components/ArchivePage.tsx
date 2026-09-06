import { useEffect, useState } from "preact/hooks";
import {
  ARCHIVE_TIERS,
  GACHA_RESERVE,
  archiveQuota,
  deleteArchive,
  getOwnerToken,
  listArchives,
  loadArchive,
  type ArchiveMeta,
  type ArchiveQuota,
} from "../lib/archive";
import {
  MAX_SHARES,
  apiConfigured,
  deletePublicRecord,
  myRecords,
  setRecordRanked,
} from "../lib/api";
import { decodeSnapshot, type IdSpace } from "../lib/bec";
import { dictionary, ensureIdSpace, stageName } from "../lib/dict";
import { formatClock, formatNumber } from "../lib/combat";
import { shareRecord } from "../lib/toy";
import { ArchiveSaveDialog } from "./ArchiveSaveDialog";
import type { LeaderboardEntry } from "../types";
import type { CombatRecord } from "../types.combat";

/**
 * Toy cloud archive plus the shares published to the leaderboard backend. The
 * key ledger is shown up front because the quota is shared with the gacha
 * archive and a save that would break it fails loudly — the user needs to see
 * what is left before choosing a tier.
 */
export function ArchivePage({
  record,
  profileReady,
  onLogin,
  onOpen,
  onOpenRecord,
}: {
  record: CombatRecord | null;
  profileReady: boolean;
  onLogin: () => void;
  onOpen: (record: CombatRecord, label: string) => void;
  onOpenRecord: (shortId: string) => void;
}) {
  const [space, setSpace] = useState<IdSpace | null>(null);
  const [metas, setMetas] = useState<ArchiveMeta[] | null>(null);
  const [quota, setQuota] = useState<ArchiveQuota | null>(null);
  const [shares, setShares] = useState<LeaderboardEntry[] | null>(null);
  const [saving, setSaving] = useState(false);
  const [busy, setBusy] = useState(false);
  const [error, setError] = useState("");
  const [notice, setNotice] = useState("");

  async function refresh() {
    setError("");
    try {
      const [list, ledger] = await Promise.all([listArchives(), archiveQuota()]);
      setMetas(list);
      setQuota(ledger);
    } catch (reason) {
      setError(reason instanceof Error ? reason.message : "云存档读取失败");
      setMetas([]);
    }
  }

  async function refreshShares() {
    if (!apiConfigured) return;
    try {
      setShares(await myRecords(await getOwnerToken()));
    } catch (reason) {
      setShares([]);
      setError(reason instanceof Error ? reason.message : "分享列表读取失败");
    }
  }

  useEffect(() => {
    // The id table is a separate chunk; nothing here can decode until it lands.
    ensureIdSpace().then(setSpace).catch(() => setError("资源表加载失败"));
  }, []);

  useEffect(() => {
    if (!profileReady) return;
    void refresh();
    void refreshShares();
  }, [profileReady]);

  async function open(meta: ArchiveMeta) {
    if (!space) return;
    setBusy(true);
    setError("");
    try {
      const bytes = await loadArchive(meta.id);
      const decoded = decodeSnapshot(bytes, space, {
        schemaVersion: 1, gameVersion: "", hotfixVersion: "", sourceSha256: "",
      }, dictionary.zones as never);
      onOpen(decoded, `CLOUD / ${meta.title}`);
    } catch (reason) {
      setError(reason instanceof Error ? reason.message : "云存档读取失败");
    } finally {
      setBusy(false);
    }
  }

  async function remove(meta: ArchiveMeta) {
    setBusy(true);
    setError("");
    try {
      await deleteArchive(meta.id);
      setNotice(`已删除「${meta.title}」，释放 ${meta.parts} 片`);
      await refresh();
    } catch (reason) {
      setError(reason instanceof Error ? reason.message : "删除失败");
    } finally {
      setBusy(false);
    }
  }

  async function toggleRanked(entry: LeaderboardEntry) {
    setBusy(true);
    setError("");
    try {
      const optIn = entry.optIn === false;
      await setRecordRanked(entry.shortId, await getOwnerToken(), optIn);
      setNotice(optIn ? "已重新参与排行榜" : "已退出排行榜，分享链接仍然有效（7 天后过期）");
      await refreshShares();
    } catch (reason) {
      setError(reason instanceof Error ? reason.message : "修改失败");
    } finally {
      setBusy(false);
    }
  }

  async function removeShare(entry: LeaderboardEntry) {
    setBusy(true);
    setError("");
    try {
      await deletePublicRecord(entry.shortId, await getOwnerToken());
      setNotice(`已删除「${stageName(entry.dungeonId)}」的分享`);
      await refreshShares();
    } catch (reason) {
      setError(reason instanceof Error ? reason.message : "删除失败");
    } finally {
      setBusy(false);
    }
  }

  if (!profileReady) {
    return (
      <main class="archive-page">
        <section class="panel empty-state">
          <b>云存档需要先登录 Toy</b>
          <p>存档按「登录用户 + Toy」隔离，跨设备保留。</p>
          <button class="button primary" onClick={onLogin}>TOY 登录</button>
        </section>
      </main>
    );
  }

  return (
    <main class="archive-page">
      <section class="panel archive-quota">
        <div class="section-heading">
          <div>
            <span class="eyebrow">CLOUD SAVE / 云存档</span>
            <h2>存储配额</h2>
          </div>
          <div class="archive-actions">
            {record && <button class="button primary" onClick={() => setSaving(true)} disabled={busy}>保存当前记录</button>}
            <button class="button secondary" onClick={() => { void refresh(); void refreshShares(); }} disabled={busy}>刷新</button>
          </div>
        </div>
        {quota && (
          <>
            <div class="quota-bar" role="img" aria-label={`已用 ${quota.used} 片，剩余 ${quota.free} 片`}>
              <i class="used" style={{ width: `${quota.used / quota.total * 100}%` }} />
              <i class="foreign" style={{ width: `${Math.max(quota.foreign, GACHA_RESERVE) / quota.total * 100}%` }} />
            </div>
            <dl class="quota-legend">
              <div><dt>战斗存档</dt><dd>{quota.used} 片</dd></div>
              <div><dt>寻访记录</dt><dd>{quota.foreign} 片（预留 {GACHA_RESERVE}）</dd></div>
              <div><dt>剩余可用</dt><dd><b>{quota.free}</b> 片</dd></div>
              <div><dt>总容量</dt><dd>{quota.total} 片 × 1 KB</dd></div>
            </dl>
          </>
        )}
      </section>

      <section class="panel archive-list">
        <div class="section-heading">
          <div>
            <span class="eyebrow">ARCHIVE / 已存记录</span>
            <h2>{metas?.length ?? 0} 份</h2>
          </div>
        </div>
        {error && <p class="empty-inline error">{error}</p>}
        {notice && <p class="empty-inline">{notice}</p>}
        {metas === null && <p class="empty-inline">正在读取…</p>}
        {metas?.length === 0 && <p class="empty-inline">云端还没有战斗存档。</p>}
        {metas?.map((meta) => (
          <div class="archive-row" key={meta.id}>
            <span class="archive-title">
              <strong>{meta.title}</strong>
              <small>{new Date(meta.createdAt).toLocaleString("zh-CN")}</small>
            </span>
            <span class="archive-stat"><strong>{formatNumber(meta.dps)}</strong><small>DPS</small></span>
            <span class="archive-stat"><strong>{formatClock(meta.durationSeconds, 1)}</strong><small>用时</small></span>
            <span class="archive-stat">
              <strong>{ARCHIVE_TIERS.find((item) => item.id === meta.tier)?.name ?? meta.tier}</strong>
              <small>{meta.parts} 片</small>
            </span>
            <span class="archive-row-actions">
              <button class="button secondary" onClick={() => void open(meta)} disabled={busy}>解析</button>
              <button class="button ghost" onClick={() => void remove(meta)} disabled={busy}>删除</button>
            </span>
          </div>
        ))}
      </section>

      {apiConfigured && (
        <section class="panel archive-list">
          <div class="section-heading">
            <div>
              <span class="eyebrow">SHARES / 我的分享</span>
              <h2>{shares?.length ?? 0} / {MAX_SHARES} 条</h2>
            </div>
          </div>
          <p class="data-note">
            分享默认参与排行榜。退出排行榜后链接仍然可用，但掉出榜单满 7 天的记录会被清理。
          </p>
          {shares === null && <p class="empty-inline">正在读取…</p>}
          {shares?.length === 0 && <p class="empty-inline">还没有分享过战斗记录。</p>}
          {shares?.map((entry) => (
            <div class="archive-row" key={entry.shortId}>
              <span class="archive-title">
                <strong>{stageName(entry.dungeonId)}</strong>
                <small>
                  {new Date(entry.uploadedAt).toLocaleString("zh-CN")}
                  {entry.optIn === false ? " · 未参与排行榜" : entry.ranked ? " · 在榜" : " · 未上榜"}
                </small>
              </span>
              <span class="archive-stat"><strong>{formatNumber(entry.dps)}</strong><small>DPS</small></span>
              <span class="archive-stat"><strong>{formatClock(entry.durationSeconds, 1)}</strong><small>用时</small></span>
              <span class="archive-row-actions">
                <button class="button secondary" onClick={() => onOpenRecord(entry.shortId)} disabled={busy}>打开</button>
                <button class="button secondary" onClick={() => void shareRecord(entry.shortId)}>分享</button>
                <button class="button ghost" onClick={() => void toggleRanked(entry)} disabled={busy}>
                  {entry.optIn === false ? "参与排行榜" : "退出排行榜"}
                </button>
                <button class="button ghost" onClick={() => void removeShare(entry)} disabled={busy}>删除</button>
              </span>
            </div>
          ))}
        </section>
      )}

      {saving && record && (
        <ArchiveSaveDialog
          record={record}
          onClose={() => setSaving(false)}
          onSaved={(meta) => {
            setSaving(false);
            setNotice(`已保存「${meta.title}」，占用 ${meta.parts} 片`);
            void refresh();
          }}
        />
      )}
    </main>
  );
}
