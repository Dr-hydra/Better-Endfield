"use strict";

// Board derivation. Rows in, ranked boards out — no storage, no I/O, so the
// rules that used to live in a SQL view are testable on their own.
//
// A row is the compact record summary kept in the private category file:
//
//   i  short id          n  nickname        du duration seconds   ly layer mask
//   d  dungeon id        a  avatar url      dp dps                iv id space
//   o  owner id (16 hex) b  bilibili bvid   rd rdps               sq squad
//   x  board opt-in      t  uploaded at ms  dm total damage       hc hit count
//   f  off-board since ms (0 while ranked)

const BOARD_SIZE = 20;
const PREVIEW_SIZE = 3;

/** Fields the browser gets. `o`, `x` and `f` are bookkeeping and stay private. */
function publicRow(row) {
  return {
    i: row.i, d: row.d, n: row.n, a: row.a || "", b: row.b || "",
    du: row.du, dp: row.dp, rd: row.rd, dm: row.dm, hc: row.hc,
    ly: row.ly, iv: row.iv, sq: row.sq || [], t: row.t,
  };
}

/** Ties go to whoever uploaded first, so a board never reshuffles on a redraw. */
function compare(metric) {
  return metric === "dps"
    ? (left, right) => right.dp - left.dp || left.t - right.t
    : (left, right) => left.du - right.du || left.t - right.t;
}

/**
 * One entry per player per stage, best first, capped at BOARD_SIZE.
 * Opted-out rows are invisible to every board but still exist as shares.
 */
function rankStage(rows, metric) {
  const order = compare(metric);
  const best = new Map();
  for (const row of rows) {
    if (row.x === false) continue;
    const current = best.get(row.o);
    if (!current || order(row, current) < 0) best.set(row.o, row);
  }
  return [...best.values()].sort(order);
}

function groupByStage(rows) {
  const stages = new Map();
  for (const row of rows) {
    if (!stages.has(row.d)) stages.set(row.d, []);
    stages.get(row.d).push(row);
  }
  return stages;
}

/** Full board for one stage: the top twenty on both metrics. */
function stageBoard(dungeonId, rows) {
  const dps = rankStage(rows, "dps");
  const time = rankStage(rows, "time");
  return {
    d: dungeonId,
    e: dps.length,
    t: Date.now(),
    dps: dps.slice(0, BOARD_SIZE).map(publicRow),
    time: time.slice(0, BOARD_SIZE).map(publicRow),
  };
}

/** Category overview: three names per stage, which is all the cards show. */
function categoryBoard(categoryId, rows) {
  const stages = {};
  for (const [dungeonId, stageRows] of groupByStage(rows)) {
    const dps = rankStage(stageRows, "dps");
    if (!dps.length) continue;
    stages[dungeonId] = {
      e: dps.length,
      dps: dps.slice(0, PREVIEW_SIZE).map(publicRow),
      time: rankStage(stageRows, "time").slice(0, PREVIEW_SIZE).map(publicRow),
    };
  }
  return { c: categoryId, t: Date.now(), s: stages };
}

/** Ids currently held by a board, so pruning knows what may not be deleted. */
function rankedIds(rows) {
  const held = new Set();
  for (const [, stageRows] of groupByStage(rows)) {
    for (const metric of ["dps", "time"]) {
      for (const row of rankStage(stageRows, metric).slice(0, BOARD_SIZE)) held.add(row.i);
    }
  }
  return held;
}

module.exports = { BOARD_SIZE, PREVIEW_SIZE, publicRow, rankStage, groupByStage, stageBoard, categoryBoard, rankedIds };
