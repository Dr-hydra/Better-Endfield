import { useEffect, useMemo, useState } from "preact/hooks";
import { getBoard, getCategoryBoard, getIndex } from "../lib/api";
import { characterIcon, characterName, formatClock, formatNumber } from "../lib/combat";
import {
  DEFAULT_CATEGORY,
  dictionary,
  findCategory,
  stageCategories,
  stageCategory,
  stageGroupName,
  stageName,
} from "../lib/dict";
import type { BoardMetric, BoardPayload, CategoryBoard, LeaderboardEntry } from "../types";

const METRICS: Array<{ id: BoardMetric; name: string; hint: string }> = [
  { id: "dps", name: "伤害榜", hint: "按每秒有效伤害排名" },
  { id: "time", name: "速通榜", hint: "按战斗用时排名" },
];

function MetricPicker({ metric, onChange }: { metric: BoardMetric; onChange: (metric: BoardMetric) => void }) {
  return (
    <div class="board-metrics">
      {METRICS.map((item) => (
        <button
          key={item.id}
          class={`chip ${metric === item.id ? "active" : ""}`}
          title={item.hint}
          onClick={() => onChange(item.id)}
        >
          {item.name}
        </button>
      ))}
    </div>
  );
}

/**
 * Category overview: the in-game tab picks the stage list, every stage shows
 * its top three, and the full twenty live one click deeper.
 *
 * Only the hardest difficulty of each stage is listed — that is what the
 * shipped stage table exposes per category — because a board is per dungeon id
 * and ranking 普通 beside 残酷 buries the runs anyone wants to see.
 */
export function LeaderboardPage({
  categoryId,
  onSelectCategory,
  onSelectStage,
  onOpenRecord,
}: {
  categoryId?: string;
  onSelectCategory: (categoryId: string) => void;
  onSelectStage: (dungeonId: string) => void;
  onOpenRecord: (shortId: string) => void;
}) {
  const category = findCategory(categoryId || DEFAULT_CATEGORY) ?? stageCategories[0];
  const [board, setBoard] = useState<CategoryBoard | null>(null);
  const [counts, setCounts] = useState<Record<string, number>>({});
  const [metric, setMetric] = useState<BoardMetric>("dps");
  const [error, setError] = useState("");

  useEffect(() => {
    getIndex().then(setCounts).catch(() => undefined);
  }, []);

  useEffect(() => {
    let stale = false;
    setBoard(null);
    setError("");
    getCategoryBoard(category.id)
      .then((result) => { if (!stale) setBoard(result); })
      .catch((reason) => {
        if (stale) return;
        setError(reason instanceof Error ? reason.message : "榜单读取失败");
      });
    return () => { stale = true; };
  }, [category.id]);

  const [ranked, empty] = useMemo(() => {
    const withEntries = new Set(category.s.filter((stageId) => (board?.stages[stageId]?.entries ?? 0) > 0));
    return [category.s.filter((stageId) => withEntries.has(stageId)),
      category.s.filter((stageId) => !withEntries.has(stageId))];
  }, [category.id, board]);

  return (
    <main class="board-page">
      <section class="board-heading panel">
        <div>
          <span class="eyebrow">RANKING / 排行榜</span>
          <h1>{category.n}</h1>
          <p>每个关卡保留前 20 名，同一玩家每关只占一个名次；仅收录每个关卡的最高难度。</p>
        </div>
        <MetricPicker metric={metric} onChange={setMetric} />
      </section>

      <nav class="category-filter panel" aria-label="关卡分类">
        {stageCategories.map((item) => (
          <button
            key={item.id}
            class={`chip ${item.id === category.id ? "active" : ""}`}
            onClick={() => onSelectCategory(item.id)}
          >
            {item.n}<i>{counts[item.id] ?? 0}</i>
          </button>
        ))}
      </nav>

      <section class="stage-cards">
        {error && <p class="empty-inline error">{error}</p>}
        {board === null && <p class="empty-inline">正在读取榜单…</p>}
        {board !== null && !ranked.length && !error && (
          <p class="empty-inline">「{category.n}」下还没有任何上传记录，来做第一个吧。</p>
        )}
        {ranked.map((stageId) => (
          <StageCard
            key={stageId}
            dungeonId={stageId}
            stage={board?.stages[stageId]}
            metric={metric}
            onOpenStage={() => onSelectStage(stageId)}
            onOpenRecord={onOpenRecord}
          />
        ))}
        {board !== null && empty.length > 0 && (
          <details class="panel stage-empty">
            <summary>暂无记录的关卡（{empty.length}）</summary>
            <div>
              {empty.map((stageId) => (
                <button key={stageId} class="chip" onClick={() => onSelectStage(stageId)}>
                  {stageName(stageId)}
                </button>
              ))}
            </div>
          </details>
        )}
      </section>
    </main>
  );
}

function StageCard({
  dungeonId,
  stage,
  metric,
  onOpenStage,
  onOpenRecord,
}: {
  dungeonId: string;
  stage?: CategoryBoard["stages"][string];
  metric: BoardMetric;
  onOpenStage: () => void;
  onOpenRecord: (shortId: string) => void;
}) {
  const rows = stage?.[metric] ?? [];
  return (
    <article class="panel stage-card">
      <button class="stage-card-head" onClick={onOpenStage}>
        <span>
          <strong>{stageName(dungeonId)}</strong>
          <small>{[stageGroupName(dungeonId), `${stage?.entries ?? 0} 人上榜`].filter(Boolean).join(" · ")}</small>
        </span>
        <i>完整榜单 →</i>
      </button>
      <div class="board-list compact">
        {rows.map((entry) => (
          <BoardRow key={entry.shortId} entry={entry} metric={metric} onOpen={onOpenRecord} />
        ))}
      </div>
    </article>
  );
}

/** One stage, all twenty. */
export function StageBoardPage({
  dungeonId,
  onBack,
  onOpenRecord,
}: {
  dungeonId: string;
  onBack: (categoryId: string) => void;
  onOpenRecord: (shortId: string) => void;
}) {
  const [board, setBoard] = useState<BoardPayload | null | undefined>(undefined);
  const [metric, setMetric] = useState<BoardMetric>("dps");
  const [error, setError] = useState("");
  const category = stageCategory(dungeonId);

  useEffect(() => {
    let stale = false;
    setBoard(undefined);
    setError("");
    getBoard(dungeonId)
      .then((result) => { if (!stale) setBoard(result); })
      .catch((reason) => { if (!stale) setError(reason instanceof Error ? reason.message : "榜单读取失败"); });
    return () => { stale = true; };
  }, [dungeonId]);

  const rows = board?.[metric] ?? [];

  return (
    <main class="board-page">
      <section class="board-heading panel">
        <div>
          <button class="link-back" onClick={() => onBack(category?.id ?? DEFAULT_CATEGORY)}>
            ← {category?.n ?? "排行榜"}
          </button>
          <h1>{stageName(dungeonId)}</h1>
          <p>{stageGroupName(dungeonId) || "同一玩家每关只占一个名次，取其最优成绩。"}</p>
        </div>
        <MetricPicker metric={metric} onChange={setMetric} />
      </section>

      <section class="board-list panel">
        {error && <p class="empty-inline error">{error}</p>}
        {board === undefined && !error && <p class="empty-inline">正在读取榜单…</p>}
        {board !== undefined && !rows.length && !error && <p class="empty-inline">该关卡还没有上榜记录。</p>}
        {rows.map((entry) => (
          <BoardRow key={entry.shortId} entry={entry} metric={metric} onOpen={onOpenRecord} />
        ))}
        {board && rows.length > 0 && (
          <p class="board-footnote">
            共 {board.entries} 份记录 · 更新于 {new Date(board.updatedAt).toLocaleString("zh-CN")}
          </p>
        )}
      </section>
    </main>
  );
}

function BoardRow({
  entry,
  metric,
  onOpen,
}: {
  entry: LeaderboardEntry;
  metric: BoardMetric;
  onOpen: (shortId: string) => void;
}) {
  return (
    <button class="board-row" onClick={() => onOpen(entry.shortId)}>
      <b class={`board-rank rank-${Math.min(entry.rank, 4)}`}>{entry.rank}</b>
      <span class="board-player">
        {entry.avatar
          ? <img src={entry.avatar} alt="" onError={(event) => { (event.currentTarget as HTMLImageElement).style.visibility = "hidden"; }} />
          : <i class="avatar-blank" />}
        <span>
          <strong>{entry.nickname}</strong>
          <small>{new Date(entry.uploadedAt).toLocaleDateString("zh-CN")}{entry.bvid ? " · 有视频" : ""}</small>
        </span>
      </span>
      <span class="board-squad">
        {entry.squad.map((member) => (
          <img
            key={member.charId}
            src={characterIcon(dictionary, member.charId)}
            alt={characterName(dictionary, member.charId)}
            title={`${characterName(dictionary, member.charId)} · ${member.potential} 潜能`}
            onError={(event) => { (event.currentTarget as HTMLImageElement).style.visibility = "hidden"; }}
          />
        ))}
      </span>
      <span class="board-metric">
        <strong>{metric === "dps" ? formatNumber(entry.dps) : formatClock(entry.durationSeconds, 2)}</strong>
        <small>{metric === "dps" ? "DPS" : "用时"}</small>
      </span>
      <span class="board-alt">
        <strong>{metric === "dps" ? formatClock(entry.durationSeconds, 1) : formatNumber(entry.dps)}</strong>
        <small>{metric === "dps" ? "用时" : "DPS"}</small>
      </span>
    </button>
  );
}
