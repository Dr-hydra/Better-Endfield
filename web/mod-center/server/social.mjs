import { randomUUID } from 'node:crypto';

export function createSocial({ db, session, authorize, rate, json, text, bodyOf, canAdmin }) {
  db.exec(`CREATE TABLE IF NOT EXISTS likes (resource_id TEXT NOT NULL REFERENCES resources(id) ON DELETE CASCADE, user_id TEXT NOT NULL REFERENCES users(id) ON DELETE CASCADE, created_at INTEGER NOT NULL, PRIMARY KEY(resource_id,user_id));
    CREATE TABLE IF NOT EXISTS comments (id TEXT PRIMARY KEY, resource_id TEXT NOT NULL REFERENCES resources(id) ON DELETE CASCADE, user_id TEXT NOT NULL REFERENCES users(id) ON DELETE CASCADE, body TEXT NOT NULL, created_at INTEGER NOT NULL);
    CREATE INDEX IF NOT EXISTS comment_resource ON comments(resource_id,created_at DESC);`);
  const fail = (status, message) => { throw Object.assign(new Error(message), { status }); };
  const stats = (id, userId) => ({ likes: db.prepare('SELECT COUNT(*) AS n FROM likes WHERE resource_id=?').get(id).n,
    comments: db.prepare('SELECT COUNT(*) AS n FROM comments WHERE resource_id=?').get(id).n,
    liked: Boolean(userId && db.prepare('SELECT 1 FROM likes WHERE resource_id=? AND user_id=?').get(id, userId)) });
  async function handle(route, req, res, url) {
    const match = /^api\/resources\/([a-f0-9-]{36})\/(likes|comments)(?:\/([a-f0-9-]{36}))?$/.exec(route);
    if (!match) return false;
    const [, id, operation, commentId] = match;
    if (!db.prepare('SELECT 1 FROM resources WHERE id=?').get(id)) fail(404, '作品不存在或已被删除。');
    if (operation === 'likes' && !commentId && ['PUT', 'DELETE'].includes(req.method)) {
      const user = authorize(req); rate(`like:${user.id}`, 120, 3600000);
      if (req.method === 'PUT') db.prepare('INSERT OR IGNORE INTO likes VALUES (?,?,?)').run(id, user.id, Date.now());
      else db.prepare('DELETE FROM likes WHERE resource_id=? AND user_id=?').run(id, user.id);
      json(res, 200, stats(id, user.id)); return true;
    }
    if (operation === 'comments' && !commentId && req.method === 'GET') {
      const user = session(req);
      const page = Math.max(1, Math.min(10000, Number(url.searchParams.get('page')) || 1)) | 0;
      const total = stats(id).comments;
      const rows = db.prepare('SELECT c.*,u.login,u.avatar,u.profile_url FROM comments c JOIN users u ON u.id=c.user_id WHERE c.resource_id=? ORDER BY c.created_at DESC,c.id DESC LIMIT 20 OFFSET ?').all(id, (page - 1) * 20);
      json(res, 200, { items: rows.map(row => ({ id: row.id, body: row.body, created_at: row.created_at,
        author: { id: row.user_id, login: row.login, avatar: row.avatar, profile_url: row.profile_url },
        canDelete: Boolean(user && (user.id === row.user_id || canAdmin(user))) })), total, page, pages: Math.max(1, Math.ceil(total / 20)) }); return true;
    }
    if (operation === 'comments' && !commentId && req.method === 'POST') {
      const user = authorize(req); const body = text((await bodyOf(req)).body, 2000, '评论', true);
      rate(`comment:${user.id}`, 20, 3600000);
      const latest = db.prepare('SELECT created_at FROM comments WHERE user_id=? ORDER BY created_at DESC LIMIT 1').get(user.id);
      if (latest && Date.now() - latest.created_at < 15000) fail(429, '评论间隔至少 15 秒。');
      const createdId = randomUUID();
      db.prepare('INSERT INTO comments VALUES (?,?,?,?,?)').run(createdId, id, user.id, body, Date.now());
      json(res, 201, { id: createdId, ...stats(id, user.id) }); return true;
    }
    if (operation === 'comments' && commentId && req.method === 'DELETE') {
      const user = authorize(req);
      const comment = db.prepare('SELECT * FROM comments WHERE id=? AND resource_id=?').get(commentId, id);
      if (!comment) fail(404, '评论不存在或已被删除。');
      if (comment.user_id !== user.id && !canAdmin(user)) fail(403, '只有评论者或管理员可以删除评论。');
      db.prepare('DELETE FROM comments WHERE id=?').run(commentId);
      json(res, 200, stats(id, user.id)); return true;
    }
    json(res, 405, { error: '请求方法不支持。' }); return true;
  }
  return { handle, stats };
}
