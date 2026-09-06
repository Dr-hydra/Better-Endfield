"use strict";

// Object storage as the whole database.
//
// There is no PostgreSQL here on purpose. In this environment the PG instance
// bills core-seconds from the moment anything wakes it, so a public leaderboard
// — where every visitor is a read — burns the monthly quota on idle time rather
// than on work. Object storage bills per request and per GB, and the objects
// the browser reads are served by the CDN in front of the bucket.
//
// Two shapes live in the bucket:
//
//   public   `<bucket>/…​.js`      JSONP the browser loads with a <script> tag
//   private  `<bucket>/<key>/….json`  gzip JSON only this function reads
//
// JSONP because the object URL sends no `access-control-allow-origin`, so a
// browser `fetch` is blocked, while a classic script load sends no `Origin` at
// all and goes through. The private half is behind an unguessable path segment;
// it holds owner hashes and rows that are not on any board.
//
// Reads happen server-side over plain HTTP against the bucket's public domain,
// which is what makes the missing CORS headers irrelevant here.

const zlib = require("node:zlib");
const cloud = require("@cloudbase/node-sdk");

const app = cloud.init({ env: cloud.SYMBOL_CURRENT_ENV });

const BASE = String(process.env.COMBAT_STORAGE_BASE || "").replace(/\/$/, "");
const BUCKET = String(process.env.COMBAT_BUCKET || "combat");
/** Path segment guarding the private half; set from an env variable. */
const DATA_KEY = String(process.env.COMBAT_DATA_KEY || "data");

const publicPath = (key) => `${BUCKET}/${key}`;
const privatePath = (key) => `${BUCKET}/${DATA_KEY}/${key}`;

/**
 * `deleteFile` wants a fileID, not a path, and the SDK offers no way to build
 * one. `getUploadMetadata` returns the fileID for any path without uploading,
 * so one call on a cold container gives us the prefix for all of them.
 */
let idPrefix = null;
async function fileId(path) {
  if (idPrefix === null) {
    const probe = publicPath(".probe");
    const meta = await app.getUploadMetadata({ cloudPath: probe });
    const id = String(meta?.data?.fileId || "");
    if (!id.endsWith(probe)) throw new Error("无法解析存储 fileID 前缀");
    idPrefix = id.slice(0, id.length - probe.length);
  }
  return idPrefix + path;
}

/**
 * There is a CDN in front of the bucket, and it will happily serve a copy of
 * an object from seconds ago even though the object says `no-cache`. That is
 * fine for the browser and fatal here: this function read-modify-writes its own
 * state, and one stale read silently drops whatever the previous write added.
 * A unique query makes it a different cache key, so the read reaches the origin.
 */
async function readPath(path, { fresh = false } = {}) {
  if (!BASE) throw new Error("未配置 COMBAT_STORAGE_BASE");
  const url = fresh ? `${BASE}/${path}?t=${Date.now()}-${Math.random().toString(36).slice(2)}` : `${BASE}/${path}`;
  const response = await fetch(url, { cache: "no-store" });
  if (response.status === 404 || response.status === 403) return null;
  if (!response.ok) throw new Error(`存储读取失败（${response.status}）`);
  return Buffer.from(await response.arrayBuffer());
}

async function writePath(path, body) {
  await app.uploadFile({ cloudPath: path, fileContent: body });
}

/** Private JSON, gzip on the wire and at rest. Missing objects read as null. */
async function readPrivate(key) {
  const raw = await readPath(privatePath(key), { fresh: true });
  if (!raw) return null;
  try {
    return JSON.parse(zlib.gunzipSync(raw).toString("utf8"));
  } catch {
    // Not gzip: an object written before compression was turned on.
    return JSON.parse(raw.toString("utf8"));
  }
}

async function writePrivate(key, value) {
  await writePath(privatePath(key), zlib.gzipSync(Buffer.from(JSON.stringify(value), "utf8"), { level: 6 }));
}

/**
 * Public JSONP. The body is `__be("<key>", <json>)` — one global callback for
 * every object, keyed by what was asked for, so the client can have several
 * loads in flight without a callback-name scheme.
 *
 * The `.js` extension is what makes the bucket answer with a JavaScript
 * content type; an object served as `application/json` is dropped by Chrome's
 * opaque-response blocking before it ever executes.
 */
async function writeScript(key, value) {
  const body = `__be(${JSON.stringify(key)},${JSON.stringify(value)});\n`;
  await writePath(publicPath(`${key}.js`), Buffer.from(body, "utf8"));
}

async function removeObjects(paths) {
  if (!paths.length) return;
  const fileList = await Promise.all(paths.map(fileId));
  await app.deleteFile({ fileList });
}

const removeScript = (key) => removeObjects([publicPath(`${key}.js`)]);
const removePrivate = (key) => removeObjects([privatePath(key)]);

/** Reads a published JSONP object back. The body is `__be("<key>",<json>);`. */
async function readScript(key) {
  const raw = await readPath(publicPath(`${key}.js`), { fresh: true });
  if (!raw) return null;
  const text = raw.toString("utf8");
  const start = text.indexOf(",");
  const end = text.lastIndexOf(")");
  if (start < 0 || end < start) return null;
  try { return JSON.parse(text.slice(start + 1, end)); } catch { return null; }
}

module.exports = {
  app, BUCKET, BASE,
  publicPath, privatePath,
  readPrivate, writePrivate,
  writeScript, readScript, removeScript, removePrivate, removeObjects,
};
