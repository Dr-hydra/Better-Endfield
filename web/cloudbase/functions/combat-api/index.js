"use strict";

// HTTP云函数：排行榜与分享记录的**写入**接口。
//
// 读取不经过这里。榜单、单关卡榜、记录本体都是云存储里的 JSONP 对象，浏览器用
// <script> 直接从 CDN 拉——那条路上没有云函数、没有网关、也没有数据库。这个函数
// 只在有人上传、删除、改视频号或改上榜意愿时被调用，然后就地重算受影响的对象。
//
// 之所以这么排：本环境的 PostgreSQL 按核秒计费，且是被唤醒就开始算，不是按查询
// 的 CPU 算。公开榜单每个访客都是一次读，靠数据库撑读路径等于花钱买实例常驻。
// 对象存储按请求数和流量计费，读多少花多少。
//
// 上传的 payload 是 BEC1 紧凑快照（base64url），不是 JSON 记录。榜单字段全部从
// 快照头部解析而来，而不是信任 body 里的数字，这样榜单上的 DPS 与点开详情后看到
// 的一定是同一份数据。注意快照用 fflate 的裸 DEFLATE（无 zlib 包头）。

const crypto = require("node:crypto");
const zlib = require("node:zlib");

const store = require("./store");
const boards = require("./board");
const STAGE_MAP = require("./stage-map.json");

/** BEC snapshots are kilobytes; anything larger is not one of ours. */
const MAX_PAYLOAD_CHARS = 512 * 1024;
const BEC_MAGIC = 0x42454331;
/**
 * Only HEAD is read here, and every version so far encodes it identically —
 * v2 only added fields to BUFFDEF. So the range widens with the writer instead
 * of pinning one version and rejecting every snapshot a newer page produces.
 */
const MIN_BEC_VERSION = 1;
const MAX_BEC_VERSION = 2;
const LAYER_SQUAD = 1 << 1;

/** Shares per user. */
const MAX_SHARES = 20;
/** Grace period for a record that has fallen off every board. */
const GRACE_MS = 7 * 24 * 3600 * 1000;

const OTHER_CATEGORY = "dungeon_other";

function json(statusCode, body) {
  return {
    statusCode,
    headers: {
      "Content-Type": "application/json; charset=utf-8",
      "Access-Control-Allow-Origin": "*",
      "Access-Control-Allow-Headers": "Content-Type",
      "Access-Control-Allow-Methods": "GET,POST,PATCH,DELETE,OPTIONS",
      "Access-Control-Max-Age": "86400",
    },
    body: JSON.stringify(body),
  };
}

function requestBody(event) {
  if (!event.body) return {};
  const text = event.isBase64Encoded
    ? Buffer.from(event.body, "base64").toString("utf8")
    : event.body;
  try { return JSON.parse(text); } catch { throw new Error("请求体不是有效 JSON"); }
}

const pathOf = (event) => String(event.path || event.rawPath || "/").replace(/\/+$/, "") || "/";
const methodOf = (event) => String(event.httpMethod || event.method || "GET").toUpperCase();
const queryOf = (event) => event.queryStringParameters || event.queryString || {};
const sha256 = (value) => crypto.createHash("sha256").update(String(value)).digest("hex");
const shortId = () => crypto.randomBytes(9).toString("base64url");
const categoryOf = (dungeonId) => STAGE_MAP[dungeonId] || OTHER_CATEGORY;

function normalizeBvid(value) {
  const text = String(value || "").trim();
  return /^BV[0-9A-Za-z]{10}$/.test(text) ? text : "";
}

// ---------------------------------------------------------------------------
// BEC head

class Cursor {
  constructor(bytes) { this.bytes = bytes; this.offset = 0; }
  u8() {
    if (this.offset >= this.bytes.length) throw new Error("快照头部不完整");
    return this.bytes[this.offset++];
  }
  varint() {
    let result = 0;
    let shift = 1;
    for (;;) {
      const byte = this.u8();
      result += (byte & 0x7f) * shift;
      if (!(byte & 0x80)) return result;
      shift *= 128;
      if (shift > 2 ** 56) throw new Error("快照 varint 过长");
    }
  }
}

/**
 * Reads the mandatory HEAD layer. Everything a board sorts on lives here, so
 * the board can never disagree with the record it links to.
 */
function readHead(payload) {
  const raw = Buffer.from(payload, "base64url");
  const cursor = new Cursor(zlib.inflateRawSync(raw));
  if (cursor.varint() !== BEC_MAGIC) throw new Error("不是 BEC 快照");
  const version = cursor.u8();
  if (version < MIN_BEC_VERSION || version > MAX_BEC_VERSION) {
    throw new Error(`不支持的 BEC 版本 ${version}`);
  }
  const idSpaceVersion = cursor.varint();
  const layers = cursor.varint();
  const sessionId = cursor.varint() + cursor.varint() * 0x100000000;
  cursor.varint();                                   // startedUnixSeconds
  const durationSeconds = cursor.varint() / 100;
  const dungeonIndex = cursor.varint();
  cursor.varint();                                   // modeIndex
  const totalDamage = cursor.varint();
  const dps = cursor.varint() / 100;
  const hitCount = cursor.varint();
  cursor.varint();                                   // criticalCount
  const rdps = cursor.varint() / 100;
  if (!(durationSeconds > 0) || durationSeconds > 24 * 3600) throw new Error("战斗时长无效");
  if (!(layers & LAYER_SQUAD)) throw new Error("快照缺少队伍层，无法上榜");
  return { idSpaceVersion, layers, sessionId, durationSeconds, dungeonIndex, totalDamage, dps, hitCount, rdps };
}

// ---------------------------------------------------------------------------
// storage shapes

const categoryKey = (categoryId) => `cat/${categoryId}.json`;
const ownerKey = (ownerHash) => `own/${ownerHash}.json`;

const readCategory = async (categoryId) => (await store.readPrivate(categoryKey(categoryId)))?.rows ?? [];
const readOwner = async (ownerHash) => (await store.readPrivate(ownerKey(ownerHash)))?.rows ?? [];

/**
 * Rewrite what one write actually changed: the private row file, the category
 * overview, and the board of every stage named in `touched`. Rewriting all of
 * a category's stages would be up to 49 objects for a one-row edit.
 *
 * A stage whose last row just went away gets its board object deleted rather
 * than left behind as a page that says nothing.
 */
async function republish(categoryId, rows, touched) {
  await store.writeScript(`cat/${categoryId}`, boards.categoryBoard(categoryId, rows));
  const grouped = boards.groupByStage(rows);
  await Promise.all([...new Set(touched)].map((dungeonId) => {
    const stageRows = grouped.get(dungeonId);
    return stageRows
      ? store.writeScript(`stage/${dungeonId}`, boards.stageBoard(dungeonId, stageRows))
      : store.removeScript(`stage/${dungeonId}`).catch(() => undefined);
  }));
  await refreshIndex(categoryId, rows.length);
}

/**
 * Read-modify-write of one category's rows, then republish what it feeds.
 *
 * Object storage has no compare-and-set, so two uploads landing on the same
 * category in the same instant can still lose one. Writing and then reading
 * back turns that from a silent loss into a retry: if our change is not in the
 * settled copy, someone else's write landed on top and we redo it against the
 * new state. `apply` therefore has to work from whatever it is handed.
 */
async function mutateCategory(categoryId, apply, verify, touched) {
  for (let attempt = 0; attempt < 3; attempt += 1) {
    const rows = apply(await readCategory(categoryId));
    await store.writePrivate(categoryKey(categoryId), { rows });
    const settled = await readCategory(categoryId);
    if (verify(settled)) {
      await republish(categoryId, settled, touched);
      return settled;
    }
  }
  throw new Error("存储写入冲突，请稍后重试");
}

/** Per-category totals, so the browser can tell which tabs have anything in them. */
async function refreshIndex(categoryId, count) {
  const index = (await store.readPrivate("index.json")) || { c: {} };
  if (count > 0) index.c[categoryId] = count;
  else delete index.c[categoryId];
  index.t = Date.now();
  await store.writePrivate("index.json", index);
  await store.writeScript("index", index);
}

// ---------------------------------------------------------------------------
// handlers

async function ingest(body) {
  const payload = String(body.payload || "");
  const ownerToken = String(body.ownerToken || "");
  if (ownerToken.length < 32) throw new Error("缺少 owner token");
  if (!payload) throw new Error("缺少快照数据");
  if (payload.length > MAX_PAYLOAD_CHARS) throw new Error("快照过大");
  if (!/^[A-Za-z0-9_-]+$/.test(payload)) throw new Error("快照不是 base64url");

  const dungeonId = String(body.dungeonId || "").slice(0, 64);
  if (!/^[A-Za-z0-9_]{1,64}$/.test(dungeonId)) throw new Error("关卡 id 无效");

  const head = readHead(payload);
  const ownerHash = sha256(ownerToken);
  const mine = await readOwner(ownerHash);
  if (mine.length >= MAX_SHARES) {
    throw new Error(`分享数量已达上限（${MAX_SHARES} 条），请先在云存档页删除旧分享`);
  }

  const categoryId = categoryOf(dungeonId);
  const row = {
    i: shortId(),
    d: dungeonId,
    // Full owner hash: it is what dedupes a board to one entry per player, and
    // what lets pruning find the owner file of a record it just deleted. It
    // only ever lives in the private half, and it is not a credential — writes
    // are authorised by the token, which this is the hash of.
    o: ownerHash,
    n: String(body.nickname || "匿名玩家").slice(0, 32),
    a: String(body.avatar || "").slice(0, 512),
    b: normalizeBvid(body.bvid),
    du: head.durationSeconds,
    dp: head.dps,
    rd: head.rdps,
    dm: head.totalDamage,
    hc: head.hitCount,
    ly: head.layers,
    iv: head.idSpaceVersion,
    sq: Array.isArray(body.squad)
      ? body.squad.slice(0, 8).map((member) => [
        String(member?.charId || "").slice(0, 64),
        Number(member?.level) || 0,
        Number(member?.potential) || 0,
      ])
      : [],
    t: Date.now(),
    // Sharing joins the board unless the uploader says otherwise; the toggle
    // afterwards lives in 我的分享管理.
    x: body.optIn !== false,
    f: 0,
  };

  // The record object first: a row pointing at a record that is not there yet
  // would be a broken link on the board for as long as the rest takes.
  await store.writeScript(`rec/${row.i}`, {
    i: row.i, d: dungeonId, n: row.n, a: row.a, b: row.b,
    ly: row.ly, iv: row.iv, t: row.t, p: payload,
  });

  await mutateCategory(
    categoryId,
    (rows) => [...rows.filter((item) => item.i !== row.i), row],
    (rows) => rows.some((item) => item.i === row.i),
    [dungeonId],
  );
  await store.writePrivate(ownerKey(ownerHash), { rows: [...mine, { ...row, c: categoryId }] });

  return { shortId: row.i, categoryId };
}

/** Finds one of the caller's own records; the owner file is the authority. */
async function ownedRow(shortIdValue, ownerToken) {
  if (String(ownerToken || "").length < 32) throw new Error("缺少 owner token");
  const ownerHash = sha256(ownerToken);
  const mine = await readOwner(ownerHash);
  const index = mine.findIndex((row) => row.i === shortIdValue);
  if (index < 0) throw new Error("记录不存在或无权修改");
  return { ownerHash, mine, index, row: mine[index] };
}

async function patchRow(shortIdValue, ownerToken, mutate) {
  const { ownerHash, mine, index, row } = await ownedRow(shortIdValue, ownerToken);
  mutate(row);
  await mutateCategory(
    row.c,
    (rows) => rows.map((item) => (item.i === shortIdValue ? { ...item, x: row.x, b: row.b, f: row.f } : item)),
    (rows) => rows.some((item) => item.i === shortIdValue && item.x === row.x && item.b === row.b),
    [row.d],
  );
  await store.writePrivate(ownerKey(ownerHash), { rows: mine.map((item, at) => (at === index ? row : item)) });
  return row;
}

async function removeRecord(shortIdValue, ownerToken) {
  const { ownerHash, mine, row } = await ownedRow(shortIdValue, ownerToken);
  await mutateCategory(
    row.c,
    (rows) => rows.filter((item) => item.i !== shortIdValue),
    (rows) => !rows.some((item) => item.i === shortIdValue),
    [row.d],
  );
  await store.writePrivate(ownerKey(ownerHash), { rows: mine.filter((item) => item.i !== shortIdValue) });
  await store.removeScript(`rec/${shortIdValue}`).catch(() => undefined);
}

/**
 * Marks rows that have fallen off every board and deletes the ones past the
 * grace period. Runs from a timer trigger on this same function — the logic
 * needs the storage layout, and a second function would be a second copy of it.
 *
 * The mark is only set once: a row that dropped off days ago keeps its original
 * timestamp, so the grace period counts from when it actually fell off. Getting
 * back onto a board clears it.
 */
async function prune() {
  const index = (await store.readPrivate("index.json")) || { c: {} };
  const now = Date.now();
  const report = { categories: 0, marked: 0, restored: 0, deleted: 0 };

  for (const categoryId of Object.keys(index.c || {})) {
    const rows = await readCategory(categoryId);
    if (!rows.length) continue;
    const held = boards.rankedIds(rows);
    const survivors = [];
    const expired = [];
    let changed = 0;
    for (const row of rows) {
      if (held.has(row.i)) {
        if (row.f) { row.f = 0; report.restored += 1; changed += 1; }
        survivors.push(row);
        continue;
      }
      if (!row.f) { row.f = now; report.marked += 1; changed += 1; }
      if (now - row.f > GRACE_MS) { expired.push(row); changed += 1; }
      else survivors.push(row);
    }
    report.categories += 1;
    report.deleted += expired.length;
    if (!changed) continue;
    await store.writePrivate(categoryKey(categoryId), { rows: survivors });
    await republish(categoryId, survivors, expired.map((row) => row.d));
    await Promise.all(expired.map((row) => store.removeScript(`rec/${row.i}`).catch(() => undefined)));
    await forgetFromOwners(expired);
  }
  return report;
}

/** Drops expired records from their owners' lists so 我的分享 has no ghosts. */
async function forgetFromOwners(expired) {
  const byOwner = new Map();
  for (const row of expired) {
    if (!byOwner.has(row.o)) byOwner.set(row.o, new Set());
    byOwner.get(row.o).add(row.i);
  }
  for (const [ownerHash, ids] of byOwner) {
    const mine = await readOwner(ownerHash);
    const kept = mine.filter((row) => !ids.has(row.i));
    if (kept.length !== mine.length) await store.writePrivate(ownerKey(ownerHash), { rows: kept });
  }
}

exports.main = async (event) => {
  // Timer triggers arrive as a bare object, not an HTTP request.
  if (event && (event.Type === "Timer" || event.TriggerName)) return prune();

  const method = methodOf(event);
  const path = pathOf(event);
  if (method === "OPTIONS") return json(204, {});
  try {
    const recordMatch = /^\/records\/([A-Za-z0-9_-]{6,32})$/.exec(path);
    const videoMatch = /^\/records\/([A-Za-z0-9_-]{6,32})\/video$/.exec(path);
    const rankedMatch = /^\/records\/([A-Za-z0-9_-]{6,32})\/ranked$/.exec(path);

    if (method === "GET" && path === "/health") {
      const index = (await store.readPrivate("index.json")) || { c: {} };
      return json(200, { ok: true, storage: store.BUCKET, categories: Object.keys(index.c || {}).length });
    }
    if (method === "POST" && path === "/records") {
      return json(201, await ingest(requestBody(event)));
    }
    if (method === "GET" && path === "/me/records") {
      const ownerToken = String(queryOf(event).ownerToken || "");
      if (ownerToken.length < 32) throw new Error("缺少 owner token");
      const mine = await readOwner(sha256(ownerToken));
      return json(200, { records: mine, limit: MAX_SHARES });
    }
    if (method === "PATCH" && videoMatch) {
      const body = requestBody(event);
      const bvid = normalizeBvid(body.bvid);
      const row = await patchRow(videoMatch[1], body.ownerToken, (item) => { item.b = bvid; });
      // The record object carries the video id too, so it has to be rewritten.
      const published = await store.readScript(`rec/${row.i}`);
      if (published) await store.writeScript(`rec/${row.i}`, { ...published, b: bvid });
      return json(200, { ok: true });
    }
    if (method === "PATCH" && rankedMatch) {
      const body = requestBody(event);
      const optIn = body.optIn !== false;
      await patchRow(rankedMatch[1], body.ownerToken, (item) => { item.x = optIn; item.f = 0; });
      return json(200, { ok: true });
    }
    if (method === "DELETE" && recordMatch) {
      await removeRecord(recordMatch[1], requestBody(event).ownerToken);
      return json(200, { ok: true });
    }
    return json(404, { message: "未知接口" });
  } catch (error) {
    return json(400, { message: error?.message || "请求失败" });
  }
};

