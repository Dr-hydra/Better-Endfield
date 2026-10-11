import { mkdir, open, unlink, stat } from 'node:fs/promises';
import { createReadStream } from 'node:fs';
import { randomUUID } from 'node:crypto';
import path from 'node:path';

export const uploadLimits = { imageBytes: 5 * 1024 ** 2, fileBytes: 200 * 1024 ** 2, imagesPerWork: 6 };
const fail = (status, message) => { throw Object.assign(new Error(message), { status }); };
function imageMime(bytes) {
  if (bytes.length >= 24 && bytes.subarray(0, 8).equals(Buffer.from([137,80,78,71,13,10,26,10])) && bytes.toString('ascii', 12, 16) === 'IHDR') return 'image/png';
  if (bytes.length >= 4 && bytes[0] === 255 && bytes[1] === 216 && bytes[2] === 255) return 'image/jpeg';
  if (bytes.length >= 16 && bytes.toString('ascii', 0, 4) === 'RIFF' && bytes.toString('ascii', 8, 12) === 'WEBP' && ['VP8 ', 'VP8L', 'VP8X'].includes(bytes.toString('ascii', 12, 16))) return 'image/webp';
  fail(415, '仅支持 JPG、PNG 和 WebP 图片。');
}

export function createUploads({ db, directory, publicUrl, prefix, canUploadFiles, limits = uploadLimits }) {
  db.exec(`CREATE TABLE IF NOT EXISTS uploads (id TEXT PRIMARY KEY, owner_id TEXT NOT NULL REFERENCES users(id), kind TEXT NOT NULL, filename TEXT NOT NULL, mime TEXT NOT NULL, size INTEGER NOT NULL, created_at INTEGER NOT NULL);
    CREATE TABLE IF NOT EXISTS resource_images (resource_id TEXT NOT NULL REFERENCES resources(id) ON DELETE CASCADE, upload_id TEXT NOT NULL REFERENCES uploads(id), position INTEGER NOT NULL, PRIMARY KEY(resource_id,upload_id));
    CREATE INDEX IF NOT EXISTS upload_owner ON uploads(owner_id,created_at);`);
  const filenameOf = id => path.join(directory, id);
  const view = row => ({ id: row.id, filename: row.filename, size: row.size, url: new URL(`media/${row.id}`, publicUrl).href });
  const referenced = id => Boolean(db.prepare('SELECT 1 FROM resource_images WHERE upload_id=? UNION ALL SELECT 1 FROM releases WHERE file_id=? LIMIT 1').get(id, id));
  const getOwned = (id, user, kind) => {
    const row = db.prepare('SELECT * FROM uploads WHERE id=?').get(id);
    if (!row || row.owner_id !== user.id || row.kind !== kind) fail(403, '上传文件不存在或不属于当前账号。');
    return row;
  };
  let active = 0, reserved = 0;
  const activeOwners = new Set();
  async function receive(req, user, kind) {
    if (!['image', 'file'].includes(kind)) fail(400, '上传类型不正确。');
    if (kind === 'file' && !canUploadFiles(user)) fail(403, '当前账号没有作品文件上传权限。');
    if (active >= 2 || activeOwners.has(user.id)) fail(429, '操作太频繁，请稍后再试。');
    const maximum = kind === 'image' ? limits.imageBytes : limits.fileBytes;
    const declared = Number(req.headers['content-length']);
    if (declared > maximum) fail(413, kind === 'image' ? '图片不能超过 5 MB。' : '作品文件不能超过 200 MB。');
    let filename;
    try { filename = decodeURIComponent(String(req.headers['x-upload-name'] || '')); } catch { fail(400, '文件名格式不正确。'); }
    filename = Array.from(filename.replace(/[\\/\u0000-\u001f\u007f]/g, '_').trim()).slice(0, 180).join('');
    if (!filename) fail(400, '文件名不能为空。');
    const used = db.prepare('SELECT COALESCE(SUM(size),0) AS total FROM uploads').get().total;
    if (used + reserved + maximum > 12 * 1024 ** 3) fail(507, '存储空间不足，请稍后重试。');
    const account = db.prepare("SELECT COALESCE(SUM(size),0) AS total,COUNT(*) AS n FROM uploads WHERE owner_id=? AND kind='image'").get(user.id);
    if (kind === 'image' && account.total + maximum > 100 * 1024 ** 2) fail(413, '账号图片存储额度已用完。');
    const pending = db.prepare('SELECT COUNT(*) AS n FROM uploads u WHERE owner_id=? AND NOT EXISTS(SELECT 1 FROM resource_images WHERE upload_id=u.id) AND NOT EXISTS(SELECT 1 FROM releases WHERE file_id=u.id)').get(user.id).n;
    if (pending >= 12) fail(429, '未发布的上传文件过多，请先发布作品或删除文件。');
    active++; reserved += maximum; activeOwners.add(user.id);
    const id = randomUUID(), destination = filenameOf(id);
    let handle, size = 0, first = Buffer.alloc(0), mime = 'application/octet-stream';
    try {
      await mkdir(directory, { recursive: true, mode: 0o700 });
      handle = await open(destination, 'wx', 0o600);
      for await (const chunk of req.iterator({ destroyOnReturn: false })) {
        size += chunk.length;
        if (size > maximum) fail(413, kind === 'image' ? '图片不能超过 5 MB。' : '作品文件不能超过 200 MB。');
        if (first.length < 32) first = Buffer.concat([first, chunk.subarray(0, 32 - first.length)]);
        let offset = 0;
        while (offset < chunk.length) offset += (await handle.write(chunk, offset)).bytesWritten;
      }
      if (!size || (Number.isFinite(declared) && size !== declared)) fail(400, '上传文件为空或传输未完成。');
      if (kind === 'image') mime = imageMime(first);
      await handle.close(); handle = null;
      db.prepare('INSERT INTO uploads VALUES (?,?,?,?,?,?,?)').run(id, user.id, kind, filename, mime, size, Date.now());
      return view(db.prepare('SELECT * FROM uploads WHERE id=?').get(id));
    } catch (error) {
      await handle?.close().catch(() => {});
      await unlink(destination).catch(() => {});
      throw error;
    } finally { active--; reserved -= maximum; activeOwners.delete(user.id); }
  }
  async function serve(req, res, id, user) {
    const row = db.prepare('SELECT * FROM uploads WHERE id=?').get(id);
    if (!row || (!referenced(id) && row.owner_id !== user?.id)) fail(404, '文件不存在或已被删除。');
    const file = filenameOf(row.id);
    const info = await stat(file).catch(() => null);
    if (!info) fail(404, '文件不存在或已被删除。');
    const headers = { 'Content-Type': row.mime, 'Content-Length': info.size, 'Accept-Ranges': 'bytes', 'Cache-Control': 'private,no-cache', 'Content-Disposition': `${row.kind === 'image' ? 'inline' : 'attachment'}; filename="download"; filename*=UTF-8''${encodeURIComponent(row.filename)}` };
    if (row.kind === 'file') headers['Content-Security-Policy'] = "default-src 'none'; sandbox";
    let start = 0, end = info.size - 1, status = 200;
    if (req.headers.range) {
      const range = /^bytes=(\d*)-(\d*)$/.exec(req.headers.range);
      if (!range || (!range[1] && !range[2])) { res.writeHead(416, { 'Content-Range': `bytes */${info.size}` }); return res.end(); }
      start = range[1] ? Number(range[1]) : Math.max(0, info.size - Number(range[2]));
      end = range[1] && range[2] ? Math.min(Number(range[2]), end) : end;
      if (start > end || start >= info.size) { res.writeHead(416, { 'Content-Range': `bytes */${info.size}` }); return res.end(); }
      status = 206; headers['Content-Length'] = end - start + 1; headers['Content-Range'] = `bytes ${start}-${end}/${info.size}`;
    }
    res.writeHead(status, headers);
    if (req.method === 'HEAD') return res.end();
    const stream = createReadStream(file, { start, end });
    stream.on('error', () => res.destroy()); res.on('close', () => stream.destroy()); stream.pipe(res);
  }
  async function remove(id, user) {
    const row = db.prepare('SELECT * FROM uploads WHERE id=?').get(id);
    if (!row || row.owner_id !== user.id) fail(403, '上传文件不存在或不属于当前账号。');
    if (referenced(id)) fail(409, '文件仍被作品或历史版本使用。');
    db.prepare('DELETE FROM uploads WHERE id=?').run(id);
    await unlink(filenameOf(id)).catch(() => {});
  }
  async function cleanup() {
    const rows = db.prepare('SELECT id FROM uploads u WHERE created_at<? AND NOT EXISTS(SELECT 1 FROM resource_images WHERE upload_id=u.id) AND NOT EXISTS(SELECT 1 FROM releases WHERE file_id=u.id)').all(Date.now() - 86400000);
    for (const row of rows) { if (!referenced(row.id)) { db.prepare('DELETE FROM uploads WHERE id=?').run(row.id); await unlink(filenameOf(row.id)).catch(() => {}); } }
  }
  return { receive, serve, remove, cleanup, view, getOwned, limits,
    gallery: id => db.prepare('SELECT u.* FROM resource_images i JOIN uploads u ON i.upload_id=u.id WHERE i.resource_id=? ORDER BY i.position').all(id).map(view),
    file: id => id ? view(db.prepare('SELECT * FROM uploads WHERE id=?').get(id)) : null,
    setGallery: (id, imageIds) => { db.prepare('DELETE FROM resource_images WHERE resource_id=?').run(id); imageIds.forEach((uploadId, index) => db.prepare('INSERT INTO resource_images VALUES (?,?,?)').run(id, uploadId, index)); },
  };
}
