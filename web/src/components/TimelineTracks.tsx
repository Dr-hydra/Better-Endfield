import { useEffect, useMemo, useRef, useState } from "preact/hooks";
import {
  ACTION_RESULT_LABELS,
  INTERRUPT_REASON_LABELS,
  buffDisplay,
  buffShortName,
  buffSourceLabel,
  characterIcon,
  characterName,
  isMobilityClip,
  stackGroupKey,
  formatClock,
  formatNumber,
  skillDisplay,
  stackLayerLimit,
  skillName,
  type BuffDisplay,
  type SkillDisplay,
} from "../lib/combat";
import {
  CONTRIBUTION_CATEGORIES,
  SKILL_CATEGORIES,
  contributionCategory,
  damageTypeName,
  stackingType,
  stackingTypeName,
} from "../lib/categories";
import { describeNominalEffects, nominalAmount, primaryNominalEffect } from "../lib/buffTerms";
import { buffConfiguredDuration } from "../lib/buffTable";
import type {
  ActionClip,
  BuffInterval,
  CombatDictionary,
  CombatRecord,
  DamageEvent,
  StaggerInterval,
} from "../types";

type TimelineItem =
  | { kind: "clip"; value: ActionClip }
  | { kind: "hit"; value: DamageEvent }
  | { kind: "buff"; value: BuffInterval }
  | { kind: "stagger"; value: StaggerInterval };

interface HitBox {
  x: number;
  y: number;
  width: number;
  height: number;
  item: TimelineItem;
}

interface Row {
  id: string;
  kind: "clips" | "hits" | "buffs" | "debuffs" | "stagger";
  label: string;
  sublabel: string;
  actorId?: string;
  height: number;
  clips?: ActionClip[];
  hits?: DamageEvent[];
  buffs?: Array<BuffInterval & { lane: number; groupKey: string }>;
  staggers?: StaggerInterval[];
}

interface ViewRange {
  start: number;
  end: number;
}

const LANE_HEIGHT = 22;
const CLIP_ROW_HEIGHT = 60;
const HIT_ROW_HEIGHT = 44;
const RULER_HEIGHT = 30;
const OVERVIEW_HEIGHT = 46;
/** Shortest window the user can zoom into, in seconds. */
const MIN_SPAN = 0.35;
const BASIC_ATTACK_CATEGORY = SKILL_CATEGORIES.findIndex((category) => category.id === "attack");

function clampRange(range: ViewRange, duration: number): ViewRange {
  const span = Math.min(Math.max(range.end - range.start, MIN_SPAN), duration);
  let start = Math.max(0, Math.min(range.start, duration - span));
  if (!Number.isFinite(start)) start = 0;
  return { start, end: start + span };
}

/**
 * Every instance keeps its own start and end — layers of one buff rarely line
 * up. What is grouped is the layout: a stacking group owns a contiguous band of
 * lanes, strongest buff first, and its layers pack into that band. Every bar in
 * a lane is the same buff, so the drawing pass labels each lane exactly once.
 */
function packBuffLanes(intervals: BuffInterval[], minWidthSeconds: number) {
  const groups = new Map<string, BuffInterval[]>();
  for (const interval of intervals) {
    const key = stackGroupKey(interval);
    const bucket = groups.get(key);
    if (bucket) bucket.push(interval);
    else groups.set(key, [interval]);
  }
  const ranked = [...groups.entries()].sort((a, b) => {
    const credited = (items: BuffInterval[]) => items.reduce((sum, item) => sum + item.credited, 0);
    return credited(b[1]) - credited(a[1]) ||
      Math.min(...a[1].map((item) => item.start)) - Math.min(...b[1].map((item) => item.start)) ||
      a[0].localeCompare(b[0]);
  });

  const packed: Array<BuffInterval & { lane: number; groupKey: string }> = [];
  let base = 0;
  for (const [groupKey, bucket] of ranked) {
    const ordered = [...bucket].sort((a, b) => a.start - b.start || a.end - b.end);
    const laneEnds: number[] = [];
    for (const interval of ordered) {
      const end = Math.max(interval.end, interval.start + minWidthSeconds);
      // Snapshot timestamps are quantised to 10 ms, so back-to-back layers can
      // overlap by a hair. Without the tolerance they would claim a lane each.
      let lane = laneEnds.findIndex((laneEnd) => laneEnd <= interval.start + 0.006);
      if (lane < 0) {
        lane = laneEnds.length;
        laneEnds.push(end);
      } else {
        laneEnds[lane] = end;
      }
      packed.push({ ...interval, lane: base + lane, groupKey });
    }
    base += Math.max(1, laneEnds.length);
  }
  return { packed, lanes: Math.max(1, base) };
}

function niceTickStep(pxPerSecond: number): number {
  const candidates = [0.05, 0.1, 0.25, 0.5, 1, 2, 5, 10, 15, 30, 60, 120, 300];
  return candidates.find((step) => step * pxPerSecond >= 72) ?? 600;
}

/** Dodges and other non-skill actions have no skill id to name them by. */
function clipLabel(dictionary: CombatDictionary, clip: ActionClip): string {
  if (clip.skillId) return skillName(dictionary, clip.skillId);
  return clip.type === "dodge" ? "闪避" : clip.type;
}

export function TimelineTracks({
  record,
  dictionary,
  skillIndex,
  buffTable = 0,
}: {
  record: CombatRecord;
  dictionary: CombatDictionary;
  skillIndex: Map<string, SkillDisplay>;
  /** Bumps when the shipped buff table loads; only a memo invalidation key. */
  buffTable?: number;
}) {
  const canvasRef = useRef<HTMLCanvasElement>(null);
  const plotRef = useRef<HTMLDivElement>(null);
  const overviewRef = useRef<HTMLCanvasElement>(null);
  const boxesRef = useRef<HitBox[]>([]);
  const dragRef = useRef<
    { mode: "pan" | "moveWindow" | "resizeStart" | "resizeEnd"; x: number; range: ViewRange; moved: boolean } | null
  >(null);

  // Canvas cannot use <img>, so icons are loaded once and the draw is retriggered
  // as they arrive.
  const imageCacheRef = useRef(new Map<string, HTMLImageElement>());
  const [imageTick, setImageTick] = useState(0);
  const getImage = (url: string): HTMLImageElement | null => {
    if (!url) return null;
    const cache = imageCacheRef.current;
    const cached = cache.get(url);
    if (cached) return cached.complete && cached.naturalWidth > 0 ? cached : null;
    const image = new Image();
    image.onload = () => setImageTick((tick) => tick + 1);
    image.src = url;
    cache.set(url, image);
    return null;
  };

  const [plotWidth, setPlotWidth] = useState(900);
  const [view, setView] = useState<ViewRange>({ start: 0, end: record.durationSeconds });
  const [hover, setHover] = useState<{ x: number; y: number; item: TimelineItem } | null>(null);
  const [selected, setSelected] = useState<TimelineItem | null>(null);
  const [cursor, setCursor] = useState<number | null>(null);
  const [showBuffs, setShowBuffs] = useState(true);
  const [showEnemy, setShowEnemy] = useState(true);
  const [onlyCredited, setOnlyCredited] = useState(true);
  const [hideBasicAttack, setHideBasicAttack] = useState(false);
  const [hideMobility, setHideMobility] = useState(false);
  const [panning, setPanning] = useState(false);

  const actorIds = useMemo(
    () => [...new Set(record.squad.map((member) => member.charId).filter(Boolean))],
    [record.squad],
  );
  const [visible, setVisible] = useState<Set<string>>(() => new Set(actorIds));
  useEffect(() => {
    setVisible(new Set(actorIds));
    setView({ start: 0, end: record.durationSeconds });
    setSelected(null);
  }, [record.sessionId]);

  useEffect(() => {
    const element = plotRef.current;
    if (!element) return;
    const observer = new ResizeObserver(([entry]) => {
      setPlotWidth(Math.max(240, Math.floor(entry.contentRect.width)));
    });
    observer.observe(element);
    setPlotWidth(Math.max(240, Math.floor(element.clientWidth)));
    return () => observer.disconnect();
  }, []);

  const span = view.end - view.start;
  const pxPerSecond = plotWidth / span;

  const clipDisplays = useMemo(() => {
    const map = new Map<number, SkillDisplay>();
    for (const clip of record.clips) map.set(clip.id, skillDisplay(skillIndex, dictionary, clip.skillId));
    return map;
  }, [dictionary, record.clips, skillIndex]);
  const buffDisplays = useMemo(() => {
    const map = new Map<string, BuffDisplay>();
    for (const interval of record.buffIntervals) {
      const key = stackGroupKey(interval);
      if (!map.has(key)) map.set(key, buffDisplay(dictionary, interval));
    }
    return map;
  }, [dictionary, record.buffIntervals, buffTable]);

  // The magnitude the buff is configured for, appended to its label. Only the
  // headline slot: a track is a few dozen pixels wide, and the attribute name
  // is spelled out in the detail panel anyway. Records older than schema 15
  // carry nothing here and the label stays as it was.
  const buffAmounts = useMemo(() => {
    const map = new Map<string, string>();
    for (const interval of record.buffIntervals) {
      const key = stackGroupKey(interval);
      if (map.has(key)) continue;
      const primary = primaryNominalEffect(interval.nominalEffects);
      if (primary) map.set(key, nominalAmount(primary));
    }
    return map;
  }, [record.buffIntervals]);

  const enemyTags = useMemo(() => {
    const map = new Map<string, string>();
    for (const interval of record.buffIntervals) {
      if (interval.friendlyTarget) continue;
      const id = interval.ownerEntityId || interval.ownerId;
      if (!map.has(id)) map.set(id, `敌${map.size + 1}`);
    }
    return map;
  }, [record.buffIntervals]);

  const rows = useMemo<Row[]>(() => {
    const result: Row[] = [];
    const minWidth = 6 / pxPerSecond;
    for (const actorId of actorIds) {
      if (!visible.has(actorId)) continue;
      const clips = record.clips.filter((clip) =>
        clip.actorId === actorId &&
        !(hideBasicAttack && clip.category === BASIC_ATTACK_CATEGORY) &&
        !(hideMobility && isMobilityClip(clip)));
      const hits = record.events.filter((event) =>
        event.attackerId === actorId && !(hideBasicAttack && event.skillCategory === BASIC_ATTACK_CATEGORY));
      result.push({
        id: `${actorId}:clips`,
        kind: "clips",
        label: characterName(dictionary, actorId),
        sublabel: "操作",
        actorId,
        height: CLIP_ROW_HEIGHT,
        clips,
      });
      result.push({
        id: `${actorId}:hits`,
        kind: "hits",
        label: "",
        sublabel: `伤害 · ${formatNumber(hits.reduce((sum, hit) => sum + hit.damage, 0))}`,
        actorId,
        height: HIT_ROW_HEIGHT,
        hits,
      });
      if (!showBuffs) continue;
      const owned = record.buffIntervals.filter((buff) =>
        buff.friendlyTarget && buff.ownerId === actorId && (!onlyCredited || buff.credited > 0));
      if (!owned.length) continue;
      const { packed, lanes } = packBuffLanes(owned, minWidth);
      result.push({
        id: `${actorId}:buffs`,
        kind: "buffs",
        label: "",
        sublabel: `增益 ${lanes}`,
        actorId,
        height: lanes * LANE_HEIGHT + 6,
        buffs: packed,
      });
    }
    if (showEnemy) {
      const debuffs = record.buffIntervals.filter((buff) =>
        !buff.friendlyTarget && (!onlyCredited || buff.credited > 0));
      if (debuffs.length) {
        const { packed, lanes } = packBuffLanes(debuffs, minWidth);
        result.push({
          id: "enemy:debuffs",
          kind: "debuffs",
          label: "敌方",
          sublabel: `减益 ${lanes}`,
          height: lanes * LANE_HEIGHT + 6,
          buffs: packed,
        });
      }
      if (record.staggerIntervals.length) {
        result.push({
          id: "enemy:stagger",
          kind: "stagger",
          label: "",
          sublabel: "失衡",
          height: 22,
          staggers: record.staggerIntervals,
        });
      }
    }
    return result;
  }, [actorIds, dictionary, hideBasicAttack, hideMobility, onlyCredited, pxPerSecond, record, showBuffs, showEnemy, visible]);

  const height = RULER_HEIGHT + rows.reduce((sum, row) => sum + row.height, 0);
  const maxHitDamage = useMemo(
    () => record.events.reduce((max, event) => Math.max(max, event.damage), 1),
    [record.events],
  );

  // ---- main track canvas -------------------------------------------------
  useEffect(() => {
    const canvas = canvasRef.current;
    if (!canvas) return;
    const ratio = window.devicePixelRatio || 1;
    canvas.width = Math.ceil(plotWidth * ratio);
    canvas.height = Math.ceil(height * ratio);
    canvas.style.width = `${plotWidth}px`;
    canvas.style.height = `${height}px`;
    const context = canvas.getContext("2d");
    if (!context) return;
    context.setTransform(ratio, 0, 0, ratio, 0, 0);
    const css = getComputedStyle(document.documentElement);
    const ink = css.getPropertyValue("--ink").trim() || "#172126";
    const muted = css.getPropertyValue("--muted").trim() || "#738087";
    const line = css.getPropertyValue("--line").trim() || "#d8dddf";
    const panel = css.getPropertyValue("--panel").trim() || "#fff";
    const boxes: HitBox[] = [];
    const timeToX = (seconds: number) => (seconds - view.start) * pxPerSecond;
    const inView = (start: number, end: number) => end >= view.start && start <= view.end;

    context.fillStyle = panel;
    context.fillRect(0, 0, plotWidth, height);

    const step = niceTickStep(pxPerSecond);
    const digits = step < 1 ? 2 : 0;
    context.font = "12px 'IBM Plex Mono', Consolas, monospace";
    context.textBaseline = "middle";
    const first = Math.floor(view.start / step) * step;
    for (let second = first; second <= view.end + step; second += step) {
      if (second < -1e-6) continue;
      const x = Math.round(timeToX(second)) + 0.5;
      context.strokeStyle = line;
      context.beginPath();
      context.moveTo(x, 0);
      context.lineTo(x, height);
      context.stroke();
      context.fillStyle = muted;
      context.fillText(formatClock(second, digits), x + 4, RULER_HEIGHT / 2);
    }
    const minorStep = step / 5;
    if (minorStep * pxPerSecond > 9) {
      context.strokeStyle = line;
      context.globalAlpha = 0.45;
      for (let second = first; second <= view.end + step; second += minorStep) {
        if (second < -1e-6) continue;
        const x = Math.round(timeToX(second)) + 0.5;
        context.beginPath();
        context.moveTo(x, RULER_HEIGHT);
        context.lineTo(x, height);
        context.stroke();
      }
      context.globalAlpha = 1;
    }

    let top = RULER_HEIGHT;
    rows.forEach((row, index) => {
      context.fillStyle = index % 2 ? "rgba(127,136,140,.05)" : "transparent";
      context.fillRect(0, top, plotWidth, row.height);
      context.strokeStyle = line;
      context.beginPath();
      context.moveTo(0, top + row.height - 0.5);
      context.lineTo(plotWidth, top + row.height - 0.5);
      context.stroke();

      if (row.kind === "clips") {
        for (const clip of row.clips ?? []) {
          if (!inView(clip.start, clip.end)) continue;
          const x = timeToX(clip.start);
          const clipWidth = Math.max(3, timeToX(clip.end) - x);
          const y = top + 7;
          const barHeight = row.height - 16;
          context.fillStyle = SKILL_CATEGORIES[clip.category]?.color ?? "#8F98AA";
          context.globalAlpha = clip.result === "cancelled" ? 0.4 : 0.92;
          context.fillRect(x, y, clipWidth, barHeight);
          context.globalAlpha = 1;
          if (clip.result === "interrupted" || clip.result === "cancelled") {
            context.fillStyle = "#d14e61";
            context.fillRect(x + clipWidth - 2, y, 2, barHeight);
          }
          if (clipWidth > 16) {
            const display = clipDisplays.get(clip.id);
            context.save();
            context.beginPath();
            context.rect(x + 2, y, clipWidth - 4, barHeight);
            context.clip();
            let textX = Math.max(x + 5, 5);
            const iconSize = barHeight - 8;
            const icon = display?.icon && clipWidth >= iconSize + 8 ? getImage(display.icon) : null;
            if (icon) {
              context.drawImage(icon, textX, y + 4, iconSize, iconSize);
              textX += iconSize + 6;
            }
            // Below this the label would be a single cropped glyph, so the icon
            // alone carries the bar.
            if (x + clipWidth - textX > 30) {
              context.fillStyle = ink;
              context.font = "600 13px 'Noto Sans SC', 'Microsoft YaHei', sans-serif";
              context.fillText(clipLabel(dictionary, clip), textX, y + barHeight / 2 - 7);
              if (display?.maxLevel) {
                context.fillStyle = "rgba(20,26,29,.66)";
                context.font = "11px Consolas, monospace";
                context.fillText(`LV.${display.level}/${display.maxLevel}`, textX, y + barHeight / 2 + 8);
              }
            }
            context.restore();
          }
          boxes.push({ x, y, width: clipWidth, height: barHeight, item: { kind: "clip", value: clip } });
        }
      } else if (row.kind === "hits") {
        const base = top + row.height - 5;
        const usable = row.height - 12;
        for (const hit of row.hits ?? []) {
          if (!inView(hit.time, hit.time)) continue;
          const x = timeToX(hit.time);
          const scale = Math.log10(hit.damage + 10) / Math.log10(maxHitDamage + 10);
          const barHeight = Math.max(3, usable * Math.min(1, Math.max(0.08, scale)));
          const barWidth = 3;
          const external = Math.min(0.95, hit.transferred);
          context.fillStyle = hit.critical ? "#ff7a45" : "#e0a700";
          context.fillRect(x, base - barHeight, barWidth, barHeight * (1 - external));
          if (external > 0) {
            context.fillStyle = hit.critical ? "rgba(255,122,69,.45)" : "rgba(224,167,0,.42)";
            context.fillRect(x, base - barHeight, barWidth, barHeight * external);
          }
          // The hit box spans the whole row so a thin marker is still easy to
          // hover; itemAt prefers the narrowest match, so it wins over the clip.
          boxes.push({
            x: x - 2,
            y: top + 2,
            width: barWidth + 4,
            height: row.height - 4,
            item: { kind: "hit", value: hit },
          });
        }
      } else if (row.kind === "buffs" || row.kind === "debuffs") {
        // One label per lane, on the leftmost bar wide enough to carry it, so the
        // name sits at the start of the row the way a single-instance buff does
        // instead of drifting to whichever bar happens to be longest.
        const labelled = new Map<number, unknown>();
        for (const buff of [...(row.buffs ?? [])].sort((a, b) => a.start - b.start)) {
          if (labelled.has(buff.lane)) continue;
          if (!inView(buff.start, buff.end)) continue;
          const left = Math.max(timeToX(buff.start), 0);
          const right = Math.min(timeToX(buff.end), plotWidth);
          if (right - left <= 12) continue;
          labelled.set(buff.lane, buff);
        }
        for (const buff of row.buffs ?? []) {
          if (!inView(buff.start, buff.end)) continue;
          const x = timeToX(buff.start);
          const barWidth = Math.max(2, timeToX(buff.end) - x);
          const y = top + 3 + buff.lane * LANE_HEIGHT;
          const barHeight = LANE_HEIGHT - 3;
          context.fillStyle = CONTRIBUTION_CATEGORIES[buff.contributionType]?.color ?? "#8F98AA";
          context.globalAlpha = buff.credited > 0 ? 0.92 : 0.34;
          context.fillRect(x, y, barWidth, barHeight);
          context.globalAlpha = 1;
          if (buff.open) {
            context.fillStyle = panel;
            for (let dash = x + barWidth - 4; dash > x && dash > x + barWidth - 14; dash -= 4) {
              context.fillRect(dash, y, 2, barHeight);
            }
          }
          if (labelled.get(buff.lane) === buff && barWidth > 12) {
            const display = buffDisplays.get(buff.groupKey);
            context.save();
            context.beginPath();
            context.rect(x + 2, y, barWidth - 4, barHeight);
            context.clip();
            let textX = Math.max(x + 3, 3);
            const iconSize = barHeight - 4;
            const icon = display?.icon && barWidth >= iconSize + 6 ? getImage(display.icon) : null;
            if (icon) {
              context.drawImage(icon, textX, y + 2, iconSize, iconSize);
              textX += iconSize + 5;
            }
            if (x + barWidth - textX > 26) {
              context.fillStyle = "rgba(18,24,27,.88)";
              context.font = "12px 'Noto Sans SC', 'Microsoft YaHei', sans-serif";
              const tag = row.kind === "debuffs" && enemyTags.size > 1
                ? `${enemyTags.get(buff.ownerEntityId || buff.ownerId) ?? ""} ` : "";
              const name = tag + (display?.name ?? buffShortName(buff.buffId));
              const amount = buffAmounts.get(buff.groupKey);
              // The amount is dropped rather than clipped when the bar is too
              // short: half a percentage is worse than none.
              const label = amount &&
                context.measureText(`${name} ${amount}`).width <= x + barWidth - textX
                ? `${name} ${amount}` : name;
              context.fillText(label, textX, y + barHeight / 2);
            }
            context.restore();
          }
          boxes.push({ x, y, width: barWidth, height: barHeight, item: { kind: "buff", value: buff } });
        }
      } else if (row.kind === "stagger") {
        for (const stagger of row.staggers ?? []) {
          if (!inView(stagger.start, stagger.end)) continue;
          const x = timeToX(stagger.start);
          const barWidth = Math.max(2, timeToX(stagger.end) - x);
          context.fillStyle = "#8c6dd7";
          context.fillRect(x, top + 5, barWidth, row.height - 11);
          boxes.push({ x, y: top + 5, width: barWidth, height: row.height - 11, item: { kind: "stagger", value: stagger } });
        }
      }
      top += row.height;
    });

    if (cursor !== null && cursor >= view.start && cursor <= view.end) {
      const x = Math.round(timeToX(cursor)) + 0.5;
      context.strokeStyle = "#d14e61";
      context.beginPath();
      context.moveTo(x, 0);
      context.lineTo(x, height);
      context.stroke();
    }
    boxesRef.current = boxes;
  }, [buffAmounts, buffDisplays, clipDisplays, cursor, dictionary, enemyTags, height, imageTick, maxHitDamage, plotWidth, pxPerSecond, record, rows, view]);

  // ---- overview strip / range brush --------------------------------------
  useEffect(() => {
    const canvas = overviewRef.current;
    if (!canvas) return;
    const ratio = window.devicePixelRatio || 1;
    canvas.width = Math.ceil(plotWidth * ratio);
    canvas.height = Math.ceil(OVERVIEW_HEIGHT * ratio);
    canvas.style.width = `${plotWidth}px`;
    canvas.style.height = `${OVERVIEW_HEIGHT}px`;
    const context = canvas.getContext("2d");
    if (!context) return;
    context.setTransform(ratio, 0, 0, ratio, 0, 0);
    const css = getComputedStyle(document.documentElement);
    const line = css.getPropertyValue("--line").trim() || "#d8dddf";
    const panel2 = css.getPropertyValue("--panel-2").trim() || "#f0efe9";
    context.fillStyle = panel2;
    context.fillRect(0, 0, plotWidth, OVERVIEW_HEIGHT);

    // Damage per bucket, so the shape of the fight stays visible while scrubbing.
    const buckets = Math.max(1, Math.floor(plotWidth / 3));
    const totals = new Float64Array(buckets);
    for (const event of record.events) {
      const index = Math.min(buckets - 1, Math.floor(event.time / record.durationSeconds * buckets));
      totals[index] += event.damage;
    }
    let peak = 1;
    for (const value of totals) peak = Math.max(peak, value);
    context.fillStyle = "#e0a700";
    for (let index = 0; index < buckets; index += 1) {
      if (!totals[index]) continue;
      const barHeight = Math.max(1, totals[index] / peak * (OVERVIEW_HEIGHT - 8));
      context.fillRect(index * 3, OVERVIEW_HEIGHT - 4 - barHeight, 2, barHeight);
    }
    for (const stagger of record.staggerIntervals) {
      const x = stagger.start / record.durationSeconds * plotWidth;
      const barWidth = Math.max(1, (stagger.end - stagger.start) / record.durationSeconds * plotWidth);
      context.fillStyle = "rgba(140,109,215,.5)";
      context.fillRect(x, 0, barWidth, OVERVIEW_HEIGHT);
    }

    const windowStart = view.start / record.durationSeconds * plotWidth;
    const windowEnd = view.end / record.durationSeconds * plotWidth;
    context.fillStyle = "rgba(23,33,38,.26)";
    context.fillRect(0, 0, windowStart, OVERVIEW_HEIGHT);
    context.fillRect(windowEnd, 0, plotWidth - windowEnd, OVERVIEW_HEIGHT);
    context.strokeStyle = line;
    context.strokeRect(0.5, 0.5, plotWidth - 1, OVERVIEW_HEIGHT - 1);
    context.fillStyle = "#d14e61";
    context.fillRect(windowStart - 2, 0, 4, OVERVIEW_HEIGHT);
    context.fillRect(windowEnd - 2, 0, 4, OVERVIEW_HEIGHT);
  }, [plotWidth, record, view]);

  // Wheel needs a non-passive listener to take over horizontal navigation.
  useEffect(() => {
    const element = plotRef.current;
    if (!element) return;
    const onWheel = (event: WheelEvent) => {
      event.preventDefault();
      const rect = element.getBoundingClientRect();
      setView((current) => {
        const currentSpan = current.end - current.start;
        if (event.ctrlKey || event.altKey || event.metaKey) {
          const anchor = current.start + (event.clientX - rect.left) / rect.width * currentSpan;
          const factor = event.deltaY < 0 ? 1 / 1.25 : 1.25;
          const nextSpan = Math.min(Math.max(currentSpan * factor, MIN_SPAN), record.durationSeconds);
          const ratio = (anchor - current.start) / currentSpan;
          const start = anchor - ratio * nextSpan;
          return clampRange({ start, end: start + nextSpan }, record.durationSeconds);
        }
        // A plain wheel scrolls the window sideways.
        const delta = Math.abs(event.deltaX) > Math.abs(event.deltaY) ? event.deltaX : event.deltaY;
        const shift = delta / rect.width * currentSpan;
        return clampRange({ start: current.start + shift, end: current.end + shift }, record.durationSeconds);
      });
    };
    element.addEventListener("wheel", onWheel, { passive: false });
    return () => element.removeEventListener("wheel", onWheel);
  }, [record.durationSeconds]);

  function itemAt(offsetX: number, offsetY: number): TimelineItem | null {
    let best: HitBox | null = null;
    for (const box of boxesRef.current) {
      if (offsetX < box.x - 2 || offsetX > box.x + box.width + 2) continue;
      if (offsetY < box.y - 2 || offsetY > box.y + box.height + 2) continue;
      if (!best || box.width < best.width) best = box;
    }
    return best?.item ?? null;
  }

  function onCanvasPointerDown(event: PointerEvent) {
    (event.currentTarget as HTMLCanvasElement).setPointerCapture(event.pointerId);
    dragRef.current = { mode: "pan", x: event.clientX, range: view, moved: false };
    setPanning(true);
  }

  function onCanvasPointerMove(event: PointerEvent) {
    const canvas = canvasRef.current;
    if (!canvas) return;
    const rect = canvas.getBoundingClientRect();
    const drag = dragRef.current;
    if (drag?.mode === "pan") {
      const delta = event.clientX - drag.x;
      if (Math.abs(delta) > 2) drag.moved = true;
      const shift = -delta / rect.width * (drag.range.end - drag.range.start);
      setView(clampRange({ start: drag.range.start + shift, end: drag.range.end + shift }, record.durationSeconds));
      setHover(null);
      return;
    }
    const offsetX = event.clientX - rect.left;
    const offsetY = event.clientY - rect.top;
    setCursor(view.start + offsetX / pxPerSecond);
    const item = itemAt(offsetX, offsetY);
    setHover(item ? { x: event.clientX, y: event.clientY, item } : null);
  }

  /**
   * The page won the gesture — a vertical swipe turned into a scroll, so the
   * browser took the pointer back. Without this the half-finished pan would sit
   * in the ref and the next move would jump the view.
   */
  function onDragCancel() {
    dragRef.current = null;
    setPanning(false);
    setHover(null);
  }

  function onCanvasPointerUp(event: PointerEvent) {
    const drag = dragRef.current;
    dragRef.current = null;
    setPanning(false);
    if (!drag || drag.moved) return;
    const canvas = canvasRef.current;
    if (!canvas) return;
    const rect = canvas.getBoundingClientRect();
    setSelected(itemAt(event.clientX - rect.left, event.clientY - rect.top));
  }

  function onOverviewPointerDown(event: PointerEvent) {
    const canvas = overviewRef.current;
    if (!canvas) return;
    canvas.setPointerCapture(event.pointerId);
    const rect = canvas.getBoundingClientRect();
    const offsetX = event.clientX - rect.left;
    const startX = view.start / record.durationSeconds * rect.width;
    const endX = view.end / record.durationSeconds * rect.width;
    if (Math.abs(offsetX - startX) <= 6) {
      dragRef.current = { mode: "resizeStart", x: event.clientX, range: view, moved: false };
      return;
    }
    if (Math.abs(offsetX - endX) <= 6) {
      dragRef.current = { mode: "resizeEnd", x: event.clientX, range: view, moved: false };
      return;
    }
    if (offsetX < startX || offsetX > endX) {
      // Clicking outside the window recentres it on that point.
      const centre = offsetX / rect.width * record.durationSeconds;
      const next = clampRange({ start: centre - span / 2, end: centre + span / 2 }, record.durationSeconds);
      setView(next);
      dragRef.current = { mode: "moveWindow", x: event.clientX, range: next, moved: true };
      return;
    }
    dragRef.current = { mode: "moveWindow", x: event.clientX, range: view, moved: false };
  }

  function onOverviewPointerMove(event: PointerEvent) {
    const drag = dragRef.current;
    const canvas = overviewRef.current;
    if (!drag || !canvas || drag.mode === "pan") return;
    const rect = canvas.getBoundingClientRect();
    const deltaSeconds = (event.clientX - drag.x) / rect.width * record.durationSeconds;
    if (drag.mode === "moveWindow") {
      setView(clampRange({ start: drag.range.start + deltaSeconds, end: drag.range.end + deltaSeconds }, record.durationSeconds));
    } else if (drag.mode === "resizeStart") {
      const start = Math.min(drag.range.start + deltaSeconds, drag.range.end - MIN_SPAN);
      setView(clampRange({ start: Math.max(0, start), end: drag.range.end }, record.durationSeconds));
    } else {
      const end = Math.max(drag.range.end + deltaSeconds, drag.range.start + MIN_SPAN);
      setView(clampRange({ start: drag.range.start, end: Math.min(record.durationSeconds, end) }, record.durationSeconds));
    }
  }

  function zoomBy(factor: number) {
    const centre = (view.start + view.end) / 2;
    const nextSpan = Math.min(Math.max(span * factor, MIN_SPAN), record.durationSeconds);
    setView(clampRange({ start: centre - nextSpan / 2, end: centre + nextSpan / 2 }, record.durationSeconds));
  }

  function setSpanSeconds(nextSpan: number) {
    const centre = (view.start + view.end) / 2;
    const clamped = Math.min(Math.max(nextSpan, MIN_SPAN), record.durationSeconds);
    setView(clampRange({ start: centre - clamped / 2, end: centre + clamped / 2 }, record.durationSeconds));
  }

  const usedBuffCategories = useMemo(() => {
    const present = new Set<number>();
    for (const buff of record.buffIntervals) present.add(buff.contributionType);
    return CONTRIBUTION_CATEGORIES.filter((_, index) => present.has(index));
  }, [record.buffIntervals]);

  return (
    <section class="panel track-panel" aria-labelledby="timeline-title">
      <div class="section-heading track-heading">
        <div>
          <span class="eyebrow">SEQUENCE / 战斗轨道</span>
          <h2 id="timeline-title">时间轴</h2>
        </div>
        <div class="track-tools">
          <label class="filter-chip"><input type="checkbox" checked={showBuffs} onChange={() => setShowBuffs((value) => !value)} />增益轨道</label>
          <label class="filter-chip"><input type="checkbox" checked={showEnemy} onChange={() => setShowEnemy((value) => !value)} />敌方轨道</label>
          <label class="filter-chip"><input type="checkbox" checked={onlyCredited} onChange={() => setOnlyCredited((value) => !value)} />仅显示计入伤害的状态</label>
          <label class="filter-chip"><input type="checkbox" checked={hideBasicAttack} onChange={() => setHideBasicAttack((value) => !value)} />隐藏普攻</label>
          <label class="filter-chip"><input type="checkbox" checked={hideMobility} onChange={() => setHideMobility((value) => !value)} />隐藏闪避冲刺</label>
        </div>
      </div>

      <div class="track-range">
        <div class="range-readout">
          <span class="eyebrow">窗口</span>
          <b>{formatClock(view.start, 2)} – {formatClock(view.end, 2)}</b>
          <small>{span.toFixed(2)}s · {pxPerSecond.toFixed(0)} px/s</small>
        </div>
        <label class="range-slider">
          <span class="eyebrow">细度</span>
          <input
            type="range"
            min={Math.log10(MIN_SPAN)}
            max={Math.log10(Math.max(record.durationSeconds, MIN_SPAN * 2))}
            step={0.01}
            value={Math.log10(span)}
            onInput={(event) => setSpanSeconds(10 ** Number((event.currentTarget as HTMLInputElement).value))}
          />
        </label>
        <div class="zoom-group">
          <button class="icon-button" onClick={() => zoomBy(1.6)} aria-label="缩小">−</button>
          <button class="icon-button" onClick={() => zoomBy(1 / 1.6)} aria-label="放大">＋</button>
          <button class="icon-button wide" onClick={() => setView({ start: 0, end: record.durationSeconds })}>全程</button>
        </div>
      </div>

      <div class="character-filters">
        {actorIds.map((id) => (
          <label class="filter-chip" key={id}>
            <input
              type="checkbox"
              checked={visible.has(id)}
              onChange={() => setVisible((current) => {
                const next = new Set(current);
                if (next.has(id)) next.delete(id); else next.add(id);
                return next;
              })}
            />
            {characterName(dictionary, id)}
          </label>
        ))}
      </div>

      <div class="track-body">
        <div class="track-labels" style={{ paddingTop: `${RULER_HEIGHT}px` }}>
          {rows.map((row) => (
            <div class={`track-label ${row.kind}`} key={row.id} style={{ height: `${row.height}px` }}>
              {row.kind === "clips" && row.actorId
                ? (
                  <span class="track-avatar" title={row.label}>
                    <img
                      src={characterIcon(dictionary, row.actorId)}
                      alt={row.label}
                      onError={(event) => { (event.currentTarget as HTMLImageElement).style.visibility = "hidden"; }}
                    />
                  </span>
                )
                : row.label ? <b>{row.label}</b> : null}
              <span class="track-label-copy">{row.sublabel}</span>
            </div>
          ))}
        </div>
        <div class="track-plot" ref={plotRef}>
          <canvas
            ref={canvasRef}
            class={panning ? "panning" : ""}
            onPointerDown={onCanvasPointerDown}
            onPointerMove={onCanvasPointerMove}
            onPointerUp={onCanvasPointerUp}
            onPointerCancel={onDragCancel}
            onPointerLeave={() => { setHover(null); setCursor(null); }}
          />
        </div>
      </div>

      <div class="track-overview">
        <span class="eyebrow">全程</span>
        <canvas
          ref={overviewRef}
          onPointerDown={onOverviewPointerDown}
          onPointerMove={onOverviewPointerMove}
          onPointerUp={() => { dragRef.current = null; }}
          onPointerCancel={onDragCancel}
        />
      </div>

      <TrackLegend buffCategories={usedBuffCategories} hasStagger={record.staggerIntervals.length > 0} />

      {hover && <TimelineTooltip hover={hover} record={record} dictionary={dictionary} skillIndex={skillIndex} />}
      {selected && (
        <TimelineInspector
          item={selected}
          record={record}
          dictionary={dictionary}
          skillIndex={skillIndex}
          onClose={() => setSelected(null)}
        />
      )}
    </section>
  );
}

function TrackLegend({
  buffCategories,
  hasStagger,
}: {
  buffCategories: readonly { id: string; name: string; color: string; hint: string }[];
  hasStagger: boolean;
}) {
  return (
    <div class="track-legend">
      <div>
        <span class="eyebrow">操作</span>
        {SKILL_CATEGORIES.map((category) => (
          <b key={category.id}><i style={{ background: category.color }} />{category.name}</b>
        ))}
        <b><i class="mark-interrupted" />被打断</b>
      </div>
      <div>
        <span class="eyebrow">伤害</span>
        <b><i style={{ background: "#e0a700" }} />普通命中</b>
        <b><i style={{ background: "#ff7a45" }} />暴击</b>
        <b><i style={{ background: "rgba(224,167,0,.42)" }} />归给他人的部分</b>
        <b class="legend-note">柱高按伤害对数</b>
      </div>
      <div>
        <span class="eyebrow">状态</span>
        {buffCategories.map((category) => (
          <b key={category.id} title={category.hint}><i style={{ background: category.color }} />{category.name}</b>
        ))}
        <b><i class="mark-uncredited" />未计入伤害</b>
        {hasStagger && <b><i style={{ background: "#8c6dd7" }} />失衡</b>}
      </div>
    </div>
  );
}

function itemTitle(item: TimelineItem, dictionary: CombatDictionary): string {
  switch (item.kind) {
    case "clip": return clipLabel(dictionary, item.value);
    case "hit": return skillName(dictionary, item.value.skillId);
    case "buff": return buffDisplay(dictionary, item.value).name;
    case "stagger": return "失衡窗口";
  }
}

function EntityHeading({ icon, title, subtitle }: { icon: string; title: string; subtitle: string }) {
  return (
    <div class="skill-heading">
      {icon
        ? (
          <span class="skill-heading-icon">
            <img src={icon} alt="" onError={(event) => { (event.currentTarget as HTMLImageElement).style.display = "none"; }} />
          </span>
        )
        : <span class="skill-heading-icon empty" />}
      <span>
        <strong>{title}</strong>
        <small>{subtitle}</small>
      </span>
    </div>
  );
}

function ItemHeading({
  item,
  dictionary,
  skillIndex,
}: {
  item: TimelineItem;
  dictionary: CombatDictionary;
  skillIndex: Map<string, SkillDisplay>;
}) {
  if (item.kind === "clip" || item.kind === "hit") {
    const display = skillDisplay(skillIndex, dictionary, item.value.skillId);
    return (
      <EntityHeading
        icon={display.icon}
        title={itemTitle(item, dictionary)}
        subtitle={display.maxLevel ? `LV.${display.level} / ${display.maxLevel}` : "无等级数据"}
      />
    );
  }
  if (item.kind === "buff") {
    const display = buffDisplay(dictionary, item.value);
    return (
      <EntityHeading
        icon={display.icon}
        title={display.name}
        subtitle={[display.kind, buffShortName(item.value.buffId)].filter(Boolean).join(" · ")}
      />
    );
  }
  return <strong>{itemTitle(item, dictionary)}</strong>;
}

function TimelineTooltip({
  hover,
  record,
  dictionary,
  skillIndex,
}: {
  hover: { x: number; y: number; item: TimelineItem };
  record: CombatRecord;
  dictionary: CombatDictionary;
  skillIndex: Map<string, SkillDisplay>;
}) {
  const { item } = hover;
  const style = {
    left: `${Math.min(hover.x + 16, window.innerWidth - 380)}px`,
    top: `${Math.min(hover.y + 16, window.innerHeight - 300)}px`,
  };
  return (
    <div class="track-tooltip" style={style}>
      <ItemHeading item={item} dictionary={dictionary} skillIndex={skillIndex} />
      {item.kind === "clip" && <ClipBody clip={item.value} dictionary={dictionary} />}
      {item.kind === "hit" && <HitBody hit={item.value} record={record} dictionary={dictionary} compact />}
      {item.kind === "buff" && <BuffBody buff={item.value} dictionary={dictionary} />}
      {item.kind === "stagger" && (
        <dl>
          <div><dt>区间</dt><dd>{item.value.start.toFixed(2)} – {item.value.end.toFixed(2)}s</dd></div>
          <div><dt>持续</dt><dd>{(item.value.end - item.value.start).toFixed(2)}s</dd></div>
          <div><dt>状态</dt><dd>{item.value.buffId || "—"}</dd></div>
        </dl>
      )}
    </div>
  );
}

function ClipBody({ clip, dictionary }: { clip: ActionClip; dictionary: CombatDictionary }) {
  return (
    <dl>
      <div><dt>角色</dt><dd>{characterName(dictionary, clip.actorId)}</dd></div>
      <div><dt>区间</dt><dd>{clip.start.toFixed(3)} – {clip.end.toFixed(3)}s（{(clip.end - clip.start).toFixed(3)}s）</dd></div>
      <div><dt>结束</dt><dd>{ACTION_RESULT_LABELS[clip.result] ?? clip.result}{clip.interruptReason ? ` · ${INTERRUPT_REASON_LABELS[clip.interruptReason] ?? clip.interruptReason}` : ""}</dd></div>
      <div><dt>命中</dt><dd>{clip.hits.length} 次 · {formatNumber(clip.damage)}</dd></div>
      <div><dt>技能 ID</dt><dd class="mono">{clip.skillId || clip.type}</dd></div>
    </dl>
  );
}

// BuffData.stackingSettings as the game resolved it, so a shared stacking key
// and a layer cap read the same way they do in the client.
function stackingLabel(buff: BuffInterval): string {
  const stacking = buff.stacking;
  if (!stacking) return "未知";
  const type = stackingType(stacking.type);
  const name = stackingTypeName(stacking.type);
  if (!type?.layered) return `${name}（不叠加）`;
  const limit = stackLayerLimit(stacking);
  const cap = limit > 0 ? `上限 ${limit} 层` : "无上限";
  const keyed = stacking.keyed && stacking.key ? `，共享层数「${stacking.key}」` : "";
  return `${name} · ${cap}（本层 ${buff.stackIndex}/${buff.stackConcurrent}）${keyed}`;
}

// Enemy entity ids carry the template plus a runtime instance suffix.
function enemyLabel(buff: BuffInterval): string {
  const id = buff.ownerEntityId || buff.ownerId;
  const template = id.replace(/_\d{6,}$/, "");
  return template ? `敌方 · ${template}` : "敌方";
}

function BuffBody({ buff, dictionary }: { buff: BuffInterval; dictionary: CombatDictionary }) {
  const category = CONTRIBUTION_CATEGORIES[buff.contributionType];
  const source = buffSourceLabel(dictionary, buff.sourceKind, buff.sourceTemplateId, buff.sourceSkillId);
  const configuredDuration = buffConfiguredDuration(buff.buffId);
  return (
    <dl>
      <div><dt>类别</dt><dd><i class="swatch" style={{ background: category?.color }} />{category?.name ?? buff.contributionKind}</dd></div>
      <div><dt>提供者</dt><dd>{buff.sourceId ? characterName(dictionary, buff.sourceId) : "—"}{buff.external ? "（外部）" : ""}</dd></div>
      <div><dt>承载者</dt><dd>{buff.friendlyTarget ? characterName(dictionary, buff.ownerId) : enemyLabel(buff)}</dd></div>
      {source && <div><dt>来源</dt><dd>{source}</dd></div>}
      {!!buff.nominalEffects?.length && (
        <div><dt>效果</dt><dd>{describeNominalEffects(buff.nominalEffects, 4)}</dd></div>
      )}
      <div>
        <dt>区间</dt>
        <dd>
          {buff.start.toFixed(2)} – {buff.end.toFixed(2)}s{buff.open ? "（未结束）" : ""}
          {/* Only shown when BuffData holds a literal: 81% of configured
              durations are blackboard keys with no static value. */}
          {configuredDuration !== null && <small> · 配置 {configuredDuration}s</small>}
        </dd>
      </div>
      <div><dt>归功伤害</dt><dd>{buff.credited > 0 ? `${formatNumber(buff.credited)} · ${buff.hitsCredited} 次命中` : "未计入"}</dd></div>
      <div><dt>叠加</dt><dd>{stackingLabel(buff)}</dd></div>
      <div><dt>状态 ID</dt><dd class="mono">{buff.buffId}</dd></div>
    </dl>
  );
}

function HitBody({
  hit,
  record,
  dictionary,
  compact,
}: {
  hit: DamageEvent;
  record: CombatRecord;
  dictionary: CombatDictionary;
  compact?: boolean;
}) {
  const factors = hit.ledger?.factors;
  const contributors = [...(hit.ledger?.contributors ?? [])]
    .filter((contributor) => contributor.fraction > 0)
    .sort((a, b) => b.fraction - a.fraction);
  const shown = compact ? contributors.slice(0, 5) : contributors;
  return (
    <>
      <dl>
        <div><dt>角色</dt><dd>{characterName(dictionary, hit.attackerId)}</dd></div>
        <div><dt>时间</dt><dd>{hit.time.toFixed(3)}s</dd></div>
        <div><dt>伤害</dt><dd>{formatNumber(hit.damage)}{hit.critical ? " · 暴击" : ""} · {damageTypeName(hit.damageType)}</dd></div>
        {factors?.valid && (
          <>
            <div><dt>攻击力</dt><dd>{formatNumber(factors.attack)}{factors.attackLinked ? "" : "（本次不随攻击力缩放）"}</dd></div>
            <div><dt>乘区合计</dt><dd>×{factors.zoneProduct.toFixed(4)}</dd></div>
            {factors.defResistance !== null && (
              <div><dt>防御减免</dt><dd>×{factors.defResistance.toFixed(4)}（DEF {formatNumber(factors.defense ?? 0)}）</dd></div>
            )}
            {factors.typeResistance !== null && (
              <div><dt>抗性/承伤</dt><dd>×{factors.typeResistance.toFixed(4)}</dd></div>
            )}
            {hit.critical && <div><dt>暴击伤害</dt><dd>×{(1 + factors.critDamage).toFixed(3)}</dd></div>}
            {factors.igniteApplies && factors.ignite !== null && (
              <div><dt>点燃系数</dt><dd>×{factors.ignite.toFixed(3)}</dd></div>
            )}
          </>
        )}
        <div><dt>外部贡献</dt><dd>{(hit.transferred * 100).toFixed(1)}%</dd></div>
      </dl>
      {shown.length > 0 && (
        <div class="tooltip-contributors">
          {shown.map((contributor) => {
            const category = contributionCategory(contributor.contributionType);
            return (
              <div key={`${contributor.statusInstanceId}:${contributor.mechanism}:${contributor.attributeType}:${contributor.zoneIndex}`}>
                <i class="swatch" style={{ background: category.color }} />
                <b>{buffShortName(contributor.statusId)}</b>
                <span>{contributor.sourceId ? characterName(dictionary, contributor.sourceId) : "—"}</span>
                <em>×{(1 / Math.max(1e-9, contributor.factor)).toFixed(3)}</em>
                <strong>{(contributor.fraction * 100).toFixed(1)}%</strong>
              </div>
            );
          })}
          {compact && contributors.length > shown.length && (
            <div class="tooltip-more">还有 {contributors.length - shown.length} 项，点击查看全部</div>
          )}
        </div>
      )}
      {!compact && (
        <p class="tooltip-note">
          技能倍率 ×{hit.ledger?.atkScale.toFixed(4)} · 计算基数 {formatNumber(hit.ledger?.calcResult ?? 0)} ·
          乘区 [{(hit.ledger?.attackerZones ?? []).map((zone, index) => `${record.zones[index]?.name ?? index}:${zone.toFixed(3)}`).join(" ")}]
        </p>
      )}
    </>
  );
}

function TimelineInspector({
  item,
  record,
  dictionary,
  skillIndex,
  onClose,
}: {
  item: TimelineItem;
  record: CombatRecord;
  dictionary: CombatDictionary;
  skillIndex: Map<string, SkillDisplay>;
  onClose: () => void;
}) {
  return (
    <div class="track-inspector">
      <header>
        <span class="eyebrow">INSPECTOR / 详情</span>
        <ItemHeading item={item} dictionary={dictionary} skillIndex={skillIndex} />
        <button class="icon-button" onClick={onClose} aria-label="关闭详情">×</button>
      </header>
      <div class="track-inspector-body">
        {item.kind === "clip" && (
          <>
            <ClipBody clip={item.value} dictionary={dictionary} />
            <div class="inspector-hits">
              {item.value.hits.map((hit) => (
                <div class="inspector-hit" key={hit.id}>
                  <span class="mono">{hit.time.toFixed(3)}s</span>
                  <b>{formatNumber(hit.damage)}</b>
                  <span>{hit.critical ? "暴击" : ""}</span>
                  <em>{(hit.transferred * 100).toFixed(0)}% 外部</em>
                </div>
              ))}
              {!item.value.hits.length && <p class="empty-inline">该动作没有产生伤害</p>}
            </div>
          </>
        )}
        {item.kind === "hit" && <HitBody hit={item.value} record={record} dictionary={dictionary} />}
        {item.kind === "buff" && <BuffBody buff={item.value} dictionary={dictionary} />}
        {item.kind === "stagger" && (
          <dl>
            <div><dt>区间</dt><dd>{item.value.start.toFixed(2)} – {item.value.end.toFixed(2)}s</dd></div>
            <div><dt>状态</dt><dd class="mono">{item.value.buffId || "—"}</dd></div>
          </dl>
        )}
      </div>
    </div>
  );
}
