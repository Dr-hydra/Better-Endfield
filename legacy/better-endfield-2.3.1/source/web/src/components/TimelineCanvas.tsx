import { useEffect, useMemo, useRef, useState } from "preact/hooks";
import { characterName, skillName } from "../lib/combat";
import type { BuffInterval, CombatDictionary, CombatRecordV11, DamageEvent } from "../types";

interface TimelineCanvasProps {
  record: CombatRecordV11;
  dictionary: CombatDictionary;
}

interface HoveredItem {
  x: number;
  y: number;
  title: string;
  detail: string;
}

const ROW_HEIGHT = 42;
const LABEL_WIDTH = 150;

export function TimelineCanvas({ record, dictionary }: TimelineCanvasProps) {
  const canvasRef = useRef<HTMLCanvasElement>(null);
  const viewportRef = useRef<HTMLDivElement>(null);
  const [zoom, setZoom] = useState(1);
  const [hover, setHover] = useState<HoveredItem | null>(null);
  const actorIds = useMemo(() => [...new Set(
    record.squad.map((member) => member.charId).filter(Boolean),
  )], [record.squad]);
  const [visibleCharacters, setVisibleCharacters] = useState<Set<string>>(
    new Set(actorIds),
  );

  useEffect(() => setVisibleCharacters(new Set(actorIds)), [record.sessionId]);

  const rows = useMemo(() => {
    const characterRows = actorIds
      .filter((id) => visibleCharacters.has(id))
      .flatMap((id) => {
        const skillIds = [...new Set(record.actions
          .filter((action) => action.actorId === id && action.skillId)
          .map((action) => action.skillId!))];
        return [
          { kind: "character" as const, id, ownerId: id, label: `${characterName(dictionary, id)} / 操作` },
          ...skillIds.map((skillId) => ({
            kind: "skill" as const,
            id: skillId,
            ownerId: id,
            label: `↳ ${skillName(dictionary, skillId)}`,
          })),
        ];
      });
    return [
      ...characterRows,
      { kind: "buff" as const, id: "buff", label: "增益状态" },
      { kind: "debuff" as const, id: "debuff", label: "敌方减益" },
      { kind: "stagger" as const, id: "stagger", label: "失衡窗口" },
    ];
  }, [actorIds, dictionary, record.actions, visibleCharacters]);

  const width = Math.max(1100, Math.ceil(record.durationSeconds * 20 * zoom) + LABEL_WIDTH);
  const height = rows.length * ROW_HEIGHT + 34;

  useEffect(() => {
    const canvas = canvasRef.current;
    const viewport = viewportRef.current;
    if (!canvas || !viewport) return;
    const ratio = window.devicePixelRatio || 1;
    canvas.width = width * ratio;
    canvas.height = height * ratio;
    canvas.style.width = `${width}px`;
    canvas.style.height = `${height}px`;
    const context = canvas.getContext("2d");
    if (!context) return;
    context.scale(ratio, ratio);
    const css = getComputedStyle(document.documentElement);
    const ink = css.getPropertyValue("--ink").trim() || "#172126";
    const muted = css.getPropertyValue("--muted").trim() || "#738087";
    const line = css.getPropertyValue("--line").trim() || "#d8dddf";
    const panel = css.getPropertyValue("--panel").trim() || "#fff";
    const yellow = css.getPropertyValue("--yellow").trim() || "#ebc900";
    context.fillStyle = panel;
    context.fillRect(0, 0, width, height);
    context.font = "12px 'IBM Plex Mono', Consolas, monospace";
    context.textBaseline = "middle";
    const plotWidth = width - LABEL_WIDTH;
    const secondWidth = plotWidth / record.durationSeconds;
    const majorTick = record.durationSeconds > 180 ? 30 : record.durationSeconds > 60 ? 10 : 5;
    for (let second = 0; second <= record.durationSeconds; second += majorTick) {
      const x = LABEL_WIDTH + second * secondWidth;
      context.strokeStyle = line;
      context.beginPath();
      context.moveTo(x, 27);
      context.lineTo(x, height);
      context.stroke();
      context.fillStyle = muted;
      context.fillText(`${second}s`, x + 4, 14);
    }
    rows.forEach((row, rowIndex) => {
      const top = 28 + rowIndex * ROW_HEIGHT;
      context.fillStyle = rowIndex % 2 ? "rgba(127,136,140,.045)" : "transparent";
      context.fillRect(0, top, width, ROW_HEIGHT);
      context.strokeStyle = line;
      context.beginPath();
      context.moveTo(0, top + ROW_HEIGHT);
      context.lineTo(width, top + ROW_HEIGHT);
      context.stroke();
      context.fillStyle = ink;
      context.font = "600 12px 'IBM Plex Mono', Consolas, monospace";
      context.fillText(row.label, 14, top + ROW_HEIGHT / 2);
      if (row.kind === "character" || row.kind === "skill") {
        const actions = record.actions.filter((action) =>
          action.actorId === row.ownerId && (row.kind === "character" || action.skillId === row.id));
        for (const action of actions) {
          const x = LABEL_WIDTH + action.start * secondWidth;
          const actionEnd = action.end ?? action.observedUntil ?? action.start;
          const actionWidth = Math.max(3, (Math.max(actionEnd, action.start) - action.start) * secondWidth);
          context.fillStyle = action.result === "cancelled" ? "#d35d6e" : yellow;
          context.fillRect(x, top + 5, actionWidth, 10);
        }
        const events = record.events.filter((event) =>
          event.attackerId === row.ownerId && (row.kind === "character" || event.originSkillId === row.id));
        for (const event of events) {
          const x = LABEL_WIDTH + event.time * secondWidth;
          const markerWidth = Math.max(3, Math.min(14, Math.log10(Math.abs(event.damage) + 10) * 2.5));
          context.fillStyle = event.critical ? "#ff7a45" : row.kind === "skill" ? "#e6a800" : yellow;
          context.fillRect(x, top + 21, markerWidth, ROW_HEIGHT - 26);
        }
      }
      if (row.kind === "buff" || row.kind === "debuff") {
        const ownIds = new Set(record.squad.map((member) => member.charId));
        const intervals = record.buffIntervals.filter((item) => {
          const isFriendlyTarget = Boolean(item.targetId && ownIds.has(item.targetId));
          return row.kind === "buff" ? isFriendlyTarget : !isFriendlyTarget;
        });
        intervals.forEach((item, intervalIndex) => {
          const x = LABEL_WIDTH + item.start * secondWidth;
          const barWidth = Math.max(2, (item.end - item.start) * secondWidth);
          context.fillStyle = row.kind === "buff" ? "#47a879" : "#d35d6e";
          context.globalAlpha = 0.78;
          context.fillRect(x, top + 7 + (intervalIndex % 2) * 10, barWidth, 9);
          context.globalAlpha = 1;
        });
      }
      if (row.kind === "stagger") {
        record.staggerIntervals.forEach((item) => {
          const x = LABEL_WIDTH + item.start * secondWidth;
          context.fillStyle = "#8c6dd7";
          context.fillRect(x, top + 9, Math.max(2, (item.end - item.start) * secondWidth), 22);
        });
      }
    });
    context.fillStyle = line;
    context.fillRect(LABEL_WIDTH - 1, 0, 1, height);
  }, [dictionary, height, record, rows, width]);

  function pointItem(clientX: number, clientY: number) {
    const canvas = canvasRef.current;
    if (!canvas) return setHover(null);
    const rect = canvas.getBoundingClientRect();
    const x = clientX - rect.left;
    const y = clientY - rect.top;
    if (x < LABEL_WIDTH || y < 28) return setHover(null);
    const rowIndex = Math.floor((y - 28) / ROW_HEIGHT);
    const row = rows[rowIndex];
    if (!row) return setHover(null);
    const time = ((x - LABEL_WIDTH) / (width - LABEL_WIDTH)) * record.durationSeconds;
    if (row.kind === "character" || row.kind === "skill") {
      const action = record.actions.find((item) =>
        item.actorId === row.ownerId &&
        (row.kind === "character" || item.skillId === row.id) &&
        time >= item.start - .04 &&
        time <= Math.max(item.end ?? item.observedUntil ?? item.start, item.start) + .08);
      if (action) {
        const actionEnd = action.end ?? action.observedUntil ?? action.start;
        return setHover({
          x: clientX,
          y: clientY,
          title: action.skillId ? skillName(dictionary, action.skillId) : action.type,
          detail: `${characterName(dictionary, action.actorId)} · ${action.start.toFixed(3)}–${actionEnd.toFixed(3)}s · ${action.result}`,
        });
      }
      const event = record.events
        .filter((item) => item.attackerId === row.ownerId && (row.kind === "character" || item.originSkillId === row.id))
        .reduce<DamageEvent | null>((nearest, item) =>
          Math.abs(item.time - time) < Math.abs((nearest?.time ?? Number.POSITIVE_INFINITY) - time) ? item : nearest,
        null);
      if (event && Math.abs(event.time - time) < Math.max(0.4, 12 / zoom)) {
        const rdps = (event.rdpsShares ?? [])
          .filter((share) => share.fraction > 0)
          .map((share) => {
            const sourceId = share.sourceId || share.source || "";
            const source = sourceId ? characterName(dictionary, sourceId) : "未知来源";
            const kind = share.contributionKind || share.kind || "贡献";
            return `${source} ${kind} ${(share.fraction * 100).toFixed(1)}%`;
          })
          .join("；");
        return setHover({
          x: clientX,
          y: clientY,
          title: skillName(dictionary, event.originSkillId),
          detail: `${characterName(dictionary, event.attackerId)} → 敌方 · ${event.time.toFixed(3)}s · ${Math.round(event.damage).toLocaleString()} 伤害${event.critical ? " · 暴击" : ""}${rdps ? ` · rDPS：${rdps}` : ""}`,
        });
      }
    } else if (row.kind === "buff" || row.kind === "debuff") {
      const ownIds = new Set(record.squad.map((member) => member.charId));
      const interval = record.buffIntervals.find((item: BuffInterval) =>
        time >= item.start && time <= item.end &&
        (row.kind === "buff" ? ownIds.has(item.targetId || "") : !ownIds.has(item.targetId || "")));
      if (interval) {
        const actor = interval.actorAttribution === "runtimeExact" && interval.creditedActorId
          ? characterName(dictionary, interval.creditedActorId)
          : "未知角色";
        const source = interval.sourceAttribution === "configurationVerified"
          ? `${interval.sourceKind}:${interval.sourceTemplateId}`
          : "未知来源";
        return setHover({
          x: clientX,
          y: clientY,
          title: interval.buffId,
          detail: `${actor} · ${source} · ${interval.start.toFixed(2)}–${interval.end.toFixed(2)}s · 目标：${interval.targetId && record.squad.some((member) => member.charId === interval.targetId) ? characterName(dictionary, interval.targetId) : "敌方"}`,
        });
      }
    }
    setHover(null);
  }

  return (
    <section class="panel timeline-panel" aria-labelledby="timeline-title">
      <div class="section-heading timeline-heading">
        <div>
          <span class="eyebrow">SEQUENCE / 事件序列</span>
          <h2 id="timeline-title">战斗时间轴</h2>
        </div>
        <div class="timeline-tools">
          <button class="icon-button" onClick={() => setZoom((value) => Math.max(.5, value - .25))} aria-label="缩小时间轴">−</button>
          <output>{Math.round(zoom * 100)}%</output>
          <button class="icon-button" onClick={() => setZoom((value) => Math.min(4, value + .25))} aria-label="放大时间轴">＋</button>
        </div>
      </div>
      <div class="character-filters" aria-label="角色轨道筛选">
        {actorIds.map((id) => (
          <label class="filter-chip" key={id}>
            <input
              type="checkbox"
              checked={visibleCharacters.has(id)}
              onChange={() => setVisibleCharacters((current) => {
                const next = new Set(current);
                next.has(id) ? next.delete(id) : next.add(id);
                return next;
              })}
            />
            {characterName(dictionary, id)}
          </label>
        ))}
      </div>
      <div class="timeline-viewport" ref={viewportRef}>
        <canvas
          ref={canvasRef}
          onPointerMove={(event) => pointItem(event.clientX, event.clientY)}
          onPointerLeave={() => setHover(null)}
          aria-label="战斗事件分轨时间轴"
        />
      </div>
      {hover && (
        <div class="timeline-tooltip" style={{ left: `${Math.min(hover.x + 12, window.innerWidth - 300)}px`, top: `${hover.y + 12}px` }}>
          <strong>{hover.title}</strong>
          <span>{hover.detail}</span>
        </div>
      )}
    </section>
  );
}
