import { useEffect, useMemo, useState } from "preact/hooks";
import type { GachaSnapshotCategory, GachaSnapshotPool, GachaSnapshotStar, GachaWebSnapshot } from "../types";

const CATEGORY_ORDER = ["limited", "weapon", "festival", "standard", "beginner", "other"];
const CATEGORY_LABELS: Record<string, string> = {
  limited: "限定池",
  weapon: "武器池",
  festival: "庆典池",
  standard: "常驻池",
  beginner: "新手池",
  other: "其他",
};

function categoryLabel(category: GachaSnapshotCategory): string {
  return category.name || CATEGORY_LABELS[category.id] || "其他";
}

function categoryOrder(id: string): number {
  const index = CATEGORY_ORDER.indexOf(id);
  return index < 0 ? CATEGORY_ORDER.length : index;
}

function iconPath(itemId: string | null): string {
  if (!itemId) return "";
  if (itemId.startsWith("chr_")) return `./icons/characters/${itemId}.png`;
  if (itemId.startsWith("wpn_")) return `./icons/items/${itemId}.png`;
  return "";
}

function imageFallback(event: Event) {
  (event.currentTarget as HTMLImageElement).style.display = "none";
}

function barWidth(total: number): number {
  return Math.max(4, Math.min(100, total / 120 * 100));
}

function tone(total: number): "green" | "yellow" | "red" {
  if (total <= 30) return "green";
  if (total <= 60) return "yellow";
  return "red";
}

function formatAverage(value: number | null): string {
  return value == null ? "-" : value.toFixed(1);
}

function formatDate(value: string): string {
  const date = new Date(value);
  return Number.isNaN(date.valueOf()) ? "未知时间" : date.toLocaleString("zh-CN");
}

function ResultBar({ star }: { star: GachaSnapshotStar }) {
  const total = Math.max(1, star.pity);
  const width = barWidth(total);
  const carry = Math.min(100, Math.max(0, star.carryPity / total * 100));
  const detail = star.carryPity > 0
    ? `${Math.max(0, star.pity - star.carryPity)} 抽 + 垫 ${star.carryPity} 抽 = ${star.pity} 抽`
    : `第 ${star.pity} 抽`;
  return <div class="gacha-result-row">
    <div class="gacha-item-icon">{iconPath(star.itemId) && <img src={iconPath(star.itemId)} alt="" onError={imageFallback} />}</div>
    <div class="gacha-result-copy">
      <div class="gacha-result-title"><strong>{star.name || "未记录"}</strong><span class={`gacha-badge ${star.isUp ? "up" : "off"}`}>{star.isUp ? "UP" : "歪"}</span></div>
      <div class="gacha-bar-line"><div class="gacha-bar" style={{ width: `${width}%` }}><i class="carry" style={{ width: `${carry}%` }} /><i class={`paid ${tone(total)}`} /></div><b>{star.pity} 抽</b></div>
      <small>{detail}</small>
    </div>
  </div>;
}

function CurrentPity({ pool }: { pool: GachaSnapshotPool }) {
  if (pool.currentPity <= 0) return null;
  // currentPity already includes startingPity in the desktop calculator.
  const total = Math.max(1, pool.currentPity);
  const width = barWidth(total);
  const carry = Math.min(100, Math.max(0, pool.startingPity / total * 100));
  const localPulls = Math.max(0, pool.currentPity - pool.startingPity);
  return <div class="gacha-current-pity">
    <span>当前垫数</span>
    <div class="gacha-bar-line"><div class="gacha-bar" style={{ width: `${width}%` }}><i class="carry" style={{ width: `${carry}%` }} /><i class={`paid ${tone(total)}`} /></div><b>{pool.currentPity} 抽</b></div>
    <small>{pool.startingPity > 0 ? `本池 ${localPulls} 抽 + 垫 ${pool.startingPity} 抽 = ${pool.currentPity} 抽` : "本池累计抽数"}</small>
  </div>;
}

function FreePulls({ pool }: { pool: GachaSnapshotPool }) {
  if (pool.freePulls <= 0) return null;
  const stars = pool.freeSixStars ?? [];
  return <section class="gacha-free-section">
    <div class="gacha-subheading"><span>免费十连</span><b>{pool.freePulls} 抽</b></div>
    {stars.length ? stars.map((star) => <ResultBar star={star} key={star.id} />) : <div class="gacha-free-empty"><span>未出六星</span><b>{pool.freePulls} 抽</b></div>}
  </section>;
}

function CloudNotice({ onLogin, busy }: { onLogin?: () => void; busy: boolean }) {
  return <div class="gacha-cloud-notice"><div><strong>云存档未登录</strong><span>当前显示本地寻访数据；登录 Toy 后可自动读取、合并并保存。</span></div>{onLogin && <button class="button primary" onClick={onLogin} disabled={busy}>{busy ? "登录中…" : "登录 Toy"}</button>}</div>;
}

function PoolCard({ pool, latest }: { pool: GachaSnapshotPool; latest: boolean }) {
  const stars = pool.sixStars ?? [];
  return <article class="gacha-pool-card">
    <header class="gacha-pool-header">
      <div><span class="eyebrow">{pool.category}</span><h3>{pool.poolName || "未知卡池"}</h3></div>
      <div class="gacha-pool-total"><strong>合计 {pool.totalPulls} 抽</strong><span>六星 {pool.sixStarCount} · 出 {pool.upCount} · 歪 {pool.offRateCount}</span></div>
    </header>
    {latest && <CurrentPity pool={pool} />}
    <div class="gacha-results">{stars.length ? stars.map((star) => <ResultBar star={star} key={star.id} />) : <p class="gacha-empty-inline">该池未记录六星</p>}</div>
    <FreePulls pool={pool} />
  </article>;
}

export function GachaPage({ snapshot, error, onLoadCloud, onSaveCloud, profileReady = false, onLogin, busy = false }: {
  snapshot: GachaWebSnapshot | null;
  error?: string;
  onLoadCloud?: () => void;
  onSaveCloud?: () => void;
  onLogin?: () => void;
  profileReady?: boolean;
  busy?: boolean;
}) {
  const categories = useMemo(() => [...(snapshot?.categories ?? [])]
    .sort((left, right) => categoryOrder(left.id) - categoryOrder(right.id) || left.name.localeCompare(right.name)), [snapshot]);
  const [selectedCategory, setSelectedCategory] = useState("");

  useEffect(() => {
    if (!categories.some((category) => category.id === selectedCategory)) setSelectedCategory(categories[0]?.id ?? "");
  }, [categories, selectedCategory]);

  const selected = categories.find((category) => category.id === selectedCategory);
  const pools = useMemo(() => (snapshot?.pools ?? []).filter((pool) => pool.categoryId === selectedCategory), [snapshot, selectedCategory]);

  if (!snapshot) return <main class="gacha-page">{!profileReady && <CloudNotice onLogin={onLogin} busy={busy} /> }<section class="gacha-empty-page panel"><span class="eyebrow">GACHA INTELLIGENCE / 寻访统计</span><h1>{error || "等待寻访快照"}</h1><p>{error ? "链接中的数据无法读取，请从桌面端重新打开网页。" : "请从 Better Endfield 寻访页面点击“打开网页”，或从 Toy 云存档读取。"}</p><div class="gacha-cloud-actions">{profileReady && onLoadCloud && <button class="button secondary" onClick={onLoadCloud} disabled={busy}>{busy ? "读取中…" : "读取云端寻访"}</button>}</div></section></main>;

  return <main class="gacha-page">
    {!profileReady && <CloudNotice onLogin={onLogin} busy={busy} />}
    <section class="gacha-summary-grid">
      {categories.map((category) => <article class={`gacha-summary-card ${category.id === selectedCategory ? "active" : ""}`} key={category.id}>
        <span>{categoryLabel(category)}</span><strong>{category.totalPulls}</strong><small>抽</small>
        <div><span>六星 <b>{category.sixStarCount}</b></span><span class="up-stat">出 <b>{category.upCount}</b></span><span class="off-stat">歪 <b>{category.offRateCount}</b></span></div>
        <small>UP 平均 {formatAverage(category.upAveragePaidPulls)}</small>
      </article>)}
    </section>
    <section class="gacha-browser panel">
      <div class="gacha-tabs" role="tablist" aria-label="寻访类型">
        {categories.map((category) => <button class={category.id === selectedCategory ? "active" : ""} role="tab" aria-selected={category.id === selectedCategory} onClick={() => setSelectedCategory(category.id)} key={category.id}>{categoryLabel(category)}</button>)}
      </div>
      <div class="gacha-browser-heading"><div><span class="eyebrow">POOL HISTORY / 卡池记录</span><h2>{selected ? categoryLabel(selected) : "寻访记录"}</h2></div><div class="gacha-browser-actions"><span class="refresh-state">{pools.length} POOLS · {formatDate(snapshot.createdAt)}</span>{profileReady && onLoadCloud && <button class="button secondary" onClick={onLoadCloud} disabled={busy}>{busy ? "同步中…" : "读取并合并云端"}</button>}{profileReady && onSaveCloud && <button class="button primary" onClick={onSaveCloud} disabled={busy}>{busy ? "上传中…" : "增量上传"}</button>}</div></div>
      <div class="gacha-pool-list">{pools.length ? pools.map((pool, index) => <PoolCard pool={pool} latest={index === 0} key={`${pool.poolId ?? "unknown"}-${pool.poolType}`} />) : <div class="gacha-empty-inline">该分类暂无记录</div>}</div>
    </section>
  </main>;
}
