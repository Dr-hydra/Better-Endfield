"use strict";

const cloud = require("@cloudbase/node-sdk");
const crypto = require("node:crypto");
const zlib = require("node:zlib");

const app = cloud.init({ env: cloud.SYMBOL_CURRENT_ENV });
const db = app.database();
const command = db.command;

const RECORDS = "combat_records";
const PARTS = "combat_record_parts";
const SNAPSHOTS = "combat_board_snapshots";
const UPLOADS = "combat_uploads";
const TOP_LIMIT = 50;
const PART_CHARS = 480_000;
const INLINE_CHARS = 3_000_000;
const MAX_RAW_BYTES = 64 * 1024 * 1024;
const OFF_BOARD_MS = 7 * 24 * 60 * 60 * 1000;

function json(statusCode, body, extraHeaders = {}) {
  return {
    statusCode,
    headers: {
      "content-type": "application/json; charset=utf-8",
      "access-control-allow-origin": "https://www.bilibili.com",
      "access-control-allow-headers": "content-type,x-owner-token",
      "access-control-allow-methods": "GET,POST,PATCH,DELETE,OPTIONS",
      ...extraHeaders,
    },
    body: JSON.stringify(body),
  };
}

function requestBody(event) {
  if (!event.body) return {};
  if (typeof event.body === "object") return event.body;
  try {
    const source = event.isBase64Encoded
      ? Buffer.from(event.body, "base64").toString("utf8")
      : event.body;
    return JSON.parse(source);
  } catch {
    throw new Error("请求体不是有效 JSON");
  }
}

function pathOf(event) {
  const path = event.path || event.requestContext?.path || "/";
  return path.replace(/^\/combat-api/, "").replace(/\/$/, "") || "/";
}

function methodOf(event) {
  return String(event.httpMethod || event.requestContext?.httpMethod || "GET").toUpperCase();
}

function queryOf(event) {
  return event.queryStringParameters || {};
}

function sha256(value) {
  return crypto.createHash("sha256").update(value).digest("hex");
}

function shortId() {
  const alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
  const bytes = crypto.randomBytes(9);
  let result = "";
  for (const byte of bytes) result += alphabet[byte % alphabet.length];
  return result;
}

function normalizeBvid(value) {
  if (!value) return "";
  const match = String(value).match(/(?:video\/)?(BV[0-9A-Za-z]{10})/i);
  if (!match) throw new Error("BVID 格式无效");
  return match[1];
}

function finite(value, name, min = 0, max = Number.MAX_SAFE_INTEGER) {
  if (typeof value !== "number" || !Number.isFinite(value) || value < min || value > max) {
    throw new Error(`${name} 超出允许范围`);
  }
  return value;
}

function unsignedIntegerString(value, name) {
  if (typeof value !== "string" || !/^\d+$/.test(value)) {
    throw new Error(`${name} 不是有效的无符号整数字符串`);
  }
  return value;
}

function validateRecord(text) {
  const bytes = Buffer.byteLength(text, "utf8");
  if (bytes > MAX_RAW_BYTES) throw new Error("战斗记录超过 64 MiB");
  let record;
  try { record = JSON.parse(text); } catch { throw new Error("战斗记录不是有效 JSON"); }
  if (record?.schemaVersion !== 11) throw new Error("仅接受最新版 schema 11");
  for (const key of ["squad", "actions", "effects"]) {
    if (!Array.isArray(record[key])) throw new Error(`缺少 ${key}`);
  }
  if (!record.battle || typeof record.battle !== "object") throw new Error("缺少 battle");
  if (!record.summary || typeof record.summary !== "object") throw new Error("缺少 summary");
  if (!record.squad.length || record.squad.length > 16) throw new Error("队伍人数无效");
  for (const member of record.squad) {
    if (!member?.charId || !Array.isArray(member.skillGroups) || !Array.isArray(member.equipments) || !Array.isArray(member.equipSuits)) {
      throw new Error("队伍配置快照不完整");
    }
    unsignedIntegerString(member.charInstId, "member.charInstId");
  }
  if (typeof record.battle.dungeonId !== "string" || !record.battle.dungeonId) throw new Error("缺少 battle.dungeonId");
  finite(record.battle.durationSeconds, "battle.durationSeconds", 0.001, 24 * 60 * 60);
  finite(record.summary.totalDamage, "summary.totalDamage", 0, 1e18);
  finite(record.summary.dps, "summary.dps", 0, 1e15);
  finite(record.summary.rdps, "summary.rdps", 0, 1e15);
  const actionIds = new Set();
  for (const action of record.actions) {
    actionIds.add(finite(action?.id, "action.id", 1));
    finite(action?.start, "action.start", 0, record.battle.durationSeconds + 60);
    if (action?.end === null) {
      finite(action?.observedUntil, "action.observedUntil", action.start,
        record.battle.durationSeconds + 60);
    } else {
      finite(action?.end, "action.end", action.start,
        record.battle.durationSeconds + 60);
    }
  }
  for (const effect of record.effects) {
    finite(effect?.id, "effect.id", 1);
    if (effect?.actionId !== undefined &&
        !actionIds.has(finite(effect.actionId, "effect.actionId", 1))) {
      throw new Error("结果引用了不存在的操作");
    }
    finite(effect?.time, "effect.time", 0, record.battle.durationSeconds + 60);
    if (["statusApply", "statusRefresh", "statusRemove"].includes(effect?.type)) {
      unsignedIntegerString(effect.statusInstanceId, "effect.statusInstanceId");
    }
    if (effect?.sourceAttribution === "configurationVerified" &&
        (!effect.sourceTemplateId || !effect.sourceKind ||
         effect.sourceKind === "unknown")) {
      throw new Error("已验证来源缺少来源模板");
    }
  }
  return { record, bytes, rdps: record.summary.rdps };
}

function publicEntry(item, rank = 0) {
  return {
    shortId: item._id,
    rank,
    nickname: item.nickname,
    avatar: item.avatar || "",
    durationSeconds: item.durationSeconds,
    dps: item.dps,
    rdps: item.rdps,
    uploadedAt: item.uploadedAt,
    dungeonId: item.dungeonId,
    squad: item.squad,
  };
}

function personalBest(items, metric) {
  const best = new Map();
  for (const item of items) {
    const current = best.get(item.ownerHash);
    const better = !current || (metric === "dps"
      ? item.dps > current.dps || (item.dps === current.dps && item.uploadedAt < current.uploadedAt)
      : item.durationSeconds < current.durationSeconds || (item.durationSeconds === current.durationSeconds && item.uploadedAt < current.uploadedAt));
    if (better) best.set(item.ownerHash, item);
  }
  return [...best.values()].sort((left, right) => metric === "dps"
    ? right.dps - left.dps || left.uploadedAt.localeCompare(right.uploadedAt)
    : left.durationSeconds - right.durationSeconds || left.uploadedAt.localeCompare(right.uploadedAt));
}

async function rebuildBoard(dungeonId) {
  const result = await db.collection(RECORDS)
    .where({ dungeonId })
    .field({ payload: false })
    .limit(1000)
    .get();
  const items = result.data || [];
  const dps = personalBest(items, "dps").slice(0, TOP_LIMIT);
  const time = personalBest(items, "time").slice(0, TOP_LIMIT);
  const rankedIds = new Set([...dps, ...time].map((item) => item._id));
  const now = new Date();
  await Promise.all(items.map((item) => {
    const ranked = rankedIds.has(item._id);
    const update = ranked
      ? { ranked: true, expireAt: command.remove() }
      : { ranked: false, expireAt: item.expireAt || new Date(now.getTime() + OFF_BOARD_MS) };
    return db.collection(RECORDS).doc(item._id).update(update).catch(() => undefined);
  }));
  const snapshot = {
    dungeonId,
    dps: dps.map((item, index) => publicEntry(item, index + 1)),
    time: time.map((item, index) => publicEntry(item, index + 1)),
    updatedAt: now.toISOString(),
  };
  await db.collection(SNAPSHOTS).doc(sha256(dungeonId).slice(0, 24)).set(snapshot);
  return snapshot;
}

async function storeCompressed(shortIdValue, compressed) {
  if (compressed.length <= INLINE_CHARS) return { payload: compressed, partCount: 0 };
  const chunks = [];
  for (let index = 0; index < compressed.length; index += PART_CHARS) chunks.push(compressed.slice(index, index + PART_CHARS));
  await Promise.all(chunks.map((value, index) => db.collection(PARTS).doc(`${shortIdValue}_${index}`).set({ recordId: shortIdValue, index, value })));
  return { payload: "", partCount: chunks.length };
}

async function ingest(body) {
  if (typeof body.text !== "string" || typeof body.ownerToken !== "string" || body.ownerToken.length < 32) {
    throw new Error("记录或 owner token 缺失");
  }
  const { record, bytes, rdps } = validateRecord(body.text);
  const id = shortId();
  const compressed = zlib.deflateRawSync(Buffer.from(body.text, "utf8"), { level: 9 }).toString("base64url");
  const payload = await storeCompressed(id, compressed);
  const uploadedAt = new Date().toISOString();
  const entry = {
    ownerHash: sha256(body.ownerToken),
    nickname: String(body.nickname || "匿名玩家").slice(0, 32),
    avatar: String(body.avatar || "").slice(0, 1024),
    bvid: normalizeBvid(body.bvid),
    dungeonId: record.battle.dungeonId,
    modeId: String(record.battle.modeId || ""),
    durationSeconds: record.battle.durationSeconds,
    totalDamage: record.summary.totalDamage,
    dps: record.summary.dps,
    rdps,
    uploadedAt,
    ranked: false,
    expireAt: new Date(Date.now() + OFF_BOARD_MS),
    squad: record.squad.slice(0, 8).map((member) => ({
      charId: member.charId,
      level: Number(member.level) || 0,
      potential: Number(member.potential) || 0,
    })),
    payload: payload.payload,
    partCount: payload.partCount,
    rawBytes: bytes,
    digest: sha256(body.text),
  };
  await db.collection(RECORDS).doc(id).set(entry);
  await rebuildBoard(record.dungeonId);
  return { shortId: id, entry: publicEntry({ ...entry, _id: id }) };
}

async function loadRecord(id) {
  const result = await db.collection(RECORDS).doc(id).get();
  const item = result.data?.[0];
  if (!item) return null;
  let compressed = item.payload || "";
  if (item.partCount) {
    const parts = await db.collection(PARTS).where({ recordId: id }).orderBy("index", "asc").limit(item.partCount).get();
    compressed = (parts.data || []).map((part) => part.value).join("");
  }
  const text = zlib.inflateRawSync(Buffer.from(compressed, "base64url")).toString("utf8");
  if (sha256(text) !== item.digest) throw new Error("记录摘要校验失败");
  return { item, record: JSON.parse(text) };
}

async function deleteRecord(id, ownerToken) {
  const found = await db.collection(RECORDS).doc(id).get();
  const item = found.data?.[0];
  if (!item) throw new Error("记录不存在");
  if (item.ownerHash !== sha256(ownerToken || "")) throw new Error("无权操作此记录");
  await db.collection(RECORDS).doc(id).remove();
  if (item.partCount) {
    const parts = await db.collection(PARTS).where({ recordId: id }).get();
    await Promise.all((parts.data || []).map((part) => db.collection(PARTS).doc(part._id).remove()));
  }
  await rebuildBoard(item.dungeonId);
}

exports.main = async (event) => {
  const method = methodOf(event);
  const path = pathOf(event);
  if (method === "OPTIONS") return json(204, {});
  try {
    if (method === "GET" && path === "/home") {
      const dungeonId = String(queryOf(event).dungeonId || "");
      if (!dungeonId) return json(400, { message: "缺少 dungeonId" });
      const key = sha256(dungeonId).slice(0, 24);
      const result = await db.collection(SNAPSHOTS).doc(key).get();
      const payload = result.data?.[0] || await rebuildBoard(dungeonId);
      const etag = `\"${sha256(JSON.stringify(payload))}\"`;
      return json(200, payload, { "cache-control": "public,max-age=60,stale-while-revalidate=300", etag });
    }
    const recordMatch = path.match(/^\/records\/([0-9A-Za-z]+)$/);
    if (method === "GET" && recordMatch) {
      const loaded = await loadRecord(recordMatch[1]);
      if (!loaded) return json(404, { message: "记录不存在或已过期" });
      return json(200, { record: loaded.record, bvid: loaded.item.bvid || "" }, { "cache-control": "public,max-age=300" });
    }
    if (method === "POST" && path === "/records") return json(201, await ingest(requestBody(event)));
    if (method === "POST" && path === "/records/multipart/init") {
      const body = requestBody(event);
      const uploadId = shortId();
      await db.collection(UPLOADS).doc(uploadId).set({
        ownerHash: sha256(String(body.ownerToken || "")),
        parts: finite(body.parts, "parts", 1, 128),
        createdAt: new Date(),
      });
      return json(201, { uploadId });
    }
    const partMatch = path.match(/^\/records\/multipart\/([0-9A-Za-z]+)\/(\d+)$/);
    if (method === "POST" && partMatch) {
      const body = requestBody(event);
      if (typeof body.value !== "string" || body.value.length > 1_000_000) throw new Error("分片无效");
      const manifest = await db.collection(UPLOADS).doc(partMatch[1]).get();
      const upload = manifest.data?.[0];
      const index = Number(partMatch[2]);
      if (!upload || upload.ownerHash !== sha256(String(body.ownerToken || "")) || index >= upload.parts) {
        throw new Error("上传会话无效");
      }
      await db.collection(UPLOADS).doc(`${partMatch[1]}_${partMatch[2]}`).set({
        uploadId: partMatch[1], index, value: body.value, createdAt: new Date(),
      });
      return json(201, { ok: true });
    }
    const completeMatch = path.match(/^\/records\/multipart\/([0-9A-Za-z]+)\/complete$/);
    if (method === "POST" && completeMatch) {
      const body = requestBody(event);
      const manifest = await db.collection(UPLOADS).doc(completeMatch[1]).get();
      const upload = manifest.data?.[0];
      if (!upload || upload.ownerHash !== sha256(String(body.ownerToken || ""))) throw new Error("上传会话无效");
      const result = await db.collection(UPLOADS).where({ uploadId: completeMatch[1] }).orderBy("index", "asc").limit(upload.parts).get();
      const pieces = result.data || [];
      if (pieces.length !== upload.parts) throw new Error("上传分片不完整");
      const response = await ingest({ ...body, text: pieces.map((item) => item.value).join("") });
      await Promise.all([db.collection(UPLOADS).doc(completeMatch[1]).remove(), ...pieces.map((item) => db.collection(UPLOADS).doc(item._id).remove())]);
      return json(201, response);
    }
    if (method === "GET" && path === "/me/records") {
      const ownerToken = event.headers?.["x-owner-token"] || event.headers?.["X-Owner-Token"] || "";
      const result = await db.collection(RECORDS).where({ ownerHash: sha256(ownerToken) }).field({ payload: false }).orderBy("uploadedAt", "desc").limit(100).get();
      return json(200, { records: (result.data || []).map((item) => publicEntry(item)) });
    }
    const videoMatch = path.match(/^\/records\/([0-9A-Za-z]+)\/video$/);
    if (method === "PATCH" && videoMatch) {
      const body = requestBody(event);
      const found = await db.collection(RECORDS).doc(videoMatch[1]).get();
      const item = found.data?.[0];
      if (!item || item.ownerHash !== sha256(String(body.ownerToken || ""))) throw new Error("无权操作此记录");
      await db.collection(RECORDS).doc(videoMatch[1]).update({ bvid: normalizeBvid(body.bvid) });
      return json(200, { ok: true });
    }
    if (method === "DELETE" && recordMatch) {
      const body = requestBody(event);
      await deleteRecord(recordMatch[1], body.ownerToken);
      return json(200, { ok: true });
    }
    return json(404, { message: "接口不存在" });
  } catch (error) {
    console.error(error);
    return json(400, { message: error instanceof Error ? error.message : "请求失败" });
  }
};

exports.rebuildBoard = rebuildBoard;
exports.validateRecord = validateRecord;
