import http from 'node:http';
import { DatabaseSync } from 'node:sqlite';
import { randomBytes, randomInt, randomUUID, createHash, timingSafeEqual } from 'node:crypto';
import { readFileSync, mkdirSync, createReadStream } from 'node:fs';
import { stat } from 'node:fs/promises';
import { fileURLToPath, pathToFileURL } from 'node:url';
import path from 'node:path';
import nodemailer from 'nodemailer';

const root = fileURLToPath(new URL('..', import.meta.url));
const types = JSON.parse(readFileSync(path.join(root, 'shared/types.json'), 'utf8'));
const typeIds = new Set(types.map(item => item.id));
const digest = value => createHash('sha256').update(value).digest('hex');
const token = () => randomBytes(32).toString('base64url');
const fail = (status, message) => { throw Object.assign(new Error(message), { status }); };
const text = (value, maximum, label, required = false) => {
  if (value === undefined || value === null) value = '';
  if (typeof value !== 'string') fail(400, `${label}格式不正确。`);
  value = value.trim();
  if ((required && !value) || value.length > maximum) fail(400, `${label}${required && !value ? '不能为空' : `不能超过 ${maximum} 个字符`}。`);
  return value;
};
const link = (value, label, required = false) => {
  value = text(value, 2048, label, required);
  if (!value) return '';
  try {
    const parsed = new URL(value);
    if (!['http:', 'https:'].includes(parsed.protocol) || parsed.username || parsed.password || !parsed.hostname) throw new Error();
    return parsed.href;
  } catch { fail(400, `${label}需要填写完整的 HTTP 或 HTTPS 地址。`); }
};
const emailAddress = value => {
  const email = text(value, 254, '邮箱', true).toLowerCase();
  if (!/^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/u.test(email)) fail(400, '邮箱地址格式不正确。');
  return email;
};
export function validateDraft(body) {
  if (!body || typeof body !== 'object' || Array.isArray(body)) fail(400, '表单格式不正确。');
  const category = text(body.type, 40, '类型', true);
  if (!typeIds.has(category)) fail(400, '请选择列表中的资源类型。');
  return {
    name: text(body.name, 120, '名字', true), type: category,
    url: link(body.url, '下载链接', true), version: text(body.version, 80, '版本'),
    cover: link(body.cover, '封面链接'), description: text(body.description, 16000, '作品介绍'),
    notes: text(body.notes, 4000, '更新说明'),
  };
}
export function makeSession(db, userId) {
  const value = token();
  const csrf = token();
  db.prepare('INSERT INTO sessions(token_hash,user_id,csrf,expires_at) VALUES (?,?,?,?)').run(digest(value), userId, csrf, Date.now() + 30 * 86400000);
  return { value, csrf };
}

export function createApp(options = {}) {
  const publicUrl = new URL(options.publicUrl || process.env.PUBLIC_URL || 'http://127.0.0.1:9017/endfield/');
  if (publicUrl.username || publicUrl.password || publicUrl.search || publicUrl.hash || !['http:', 'https:'].includes(publicUrl.protocol)) throw new Error('PUBLIC_URL must be a plain HTTP(S) site URL');
  const prefix = `${publicUrl.pathname.replace(/\/$/, '')}/`;
  const githubId = options.githubId ?? process.env.GITHUB_CLIENT_ID ?? '';
  const githubSecret = options.githubSecret ?? process.env.GITHUB_CLIENT_SECRET ?? '';
  const githubEnabled = Boolean(githubId && githubSecret);
  const smtpHost = options.smtpHost ?? process.env.SMTP_HOST ?? '';
  const smtpPort = Number(options.smtpPort ?? process.env.SMTP_PORT ?? 465);
  const smtpUser = options.smtpUser ?? process.env.SMTP_USER ?? '';
  const smtpPass = options.smtpPass ?? process.env.SMTP_PASS ?? '';
  const smtpFrom = options.smtpFrom ?? process.env.SMTP_FROM ?? smtpUser;
  const smtpSettings = { connectionTimeout: 5000, greetingTimeout: 5000, socketTimeout: 10000, requireTLS: true };
  const mailer = options.mailer || (smtpHost && smtpFrom ? nodemailer.createTransport({ ...smtpSettings, host: smtpHost, port: smtpPort, secure: smtpPort === 465, auth: smtpUser ? { user: smtpUser, pass: smtpPass } : undefined }) : null);
  const sendEmail = options.sendEmail || (mailer ? message => mailer.sendMail(message) : null);
  const emailEnabled = Boolean(sendEmail && smtpFrom);
  const databasePath = options.databasePath || process.env.DATABASE_PATH || path.join(root, 'data/catalog.sqlite');
  if (databasePath !== ':memory:') mkdirSync(path.dirname(databasePath), { recursive: true });
  const db = new DatabaseSync(databasePath);
  db.exec(`PRAGMA foreign_keys=ON; PRAGMA journal_mode=WAL; PRAGMA busy_timeout=5000;
    CREATE TABLE IF NOT EXISTS users (id TEXT PRIMARY KEY, github_id TEXT UNIQUE, login TEXT NOT NULL, avatar TEXT NOT NULL, profile_url TEXT NOT NULL, email TEXT);
    CREATE TABLE IF NOT EXISTS sessions (token_hash TEXT PRIMARY KEY, user_id TEXT NOT NULL REFERENCES users(id) ON DELETE CASCADE, csrf TEXT NOT NULL, expires_at INTEGER NOT NULL);
    CREATE TABLE IF NOT EXISTS oauth_states (state_hash TEXT PRIMARY KEY, verifier TEXT NOT NULL, expires_at INTEGER NOT NULL);
    CREATE TABLE IF NOT EXISTS email_codes (challenge_hash TEXT PRIMARY KEY, code_hash TEXT NOT NULL, email TEXT NOT NULL, user_id TEXT REFERENCES users(id) ON DELETE CASCADE, expires_at INTEGER NOT NULL, attempts INTEGER NOT NULL DEFAULT 0);
    CREATE TABLE IF NOT EXISTS resources (id TEXT PRIMARY KEY, name TEXT NOT NULL, type TEXT NOT NULL, description TEXT NOT NULL, cover TEXT NOT NULL, owner_id TEXT NOT NULL REFERENCES users(id), created_at INTEGER NOT NULL, updated_at INTEGER NOT NULL);
    CREATE TABLE IF NOT EXISTS releases (id TEXT PRIMARY KEY, resource_id TEXT NOT NULL REFERENCES resources(id) ON DELETE CASCADE, number INTEGER NOT NULL, url TEXT NOT NULL, version TEXT NOT NULL, notes TEXT NOT NULL, created_at INTEGER NOT NULL, UNIQUE(resource_id,number));
    CREATE UNIQUE INDEX IF NOT EXISTS release_version ON releases(resource_id,version) WHERE version <> '';
    CREATE INDEX IF NOT EXISTS resource_type ON resources(type,updated_at DESC);
    CREATE INDEX IF NOT EXISTS resource_owner ON resources(owner_id,updated_at DESC);
    CREATE INDEX IF NOT EXISTS release_resource ON releases(resource_id,number DESC);`);
  if (!db.prepare('PRAGMA table_info(users)').all().some(column => column.name === 'email')) db.exec('ALTER TABLE users ADD COLUMN email TEXT');
  if (db.prepare('PRAGMA table_info(users)').all().find(column => column.name === 'github_id').notnull) {
    // Rebuild only the account table; child tables keep their references to users.
    db.exec(`PRAGMA foreign_keys=OFF;
      BEGIN IMMEDIATE;
      CREATE TABLE users_email_migration (id TEXT PRIMARY KEY, github_id TEXT UNIQUE, login TEXT NOT NULL, avatar TEXT NOT NULL, profile_url TEXT NOT NULL, email TEXT);
      INSERT INTO users_email_migration SELECT id,github_id,login,avatar,profile_url,email FROM users;
      DROP TABLE users;
      ALTER TABLE users_email_migration RENAME TO users;
      COMMIT;
      PRAGMA foreign_keys=ON;`);
  }
  db.exec('CREATE UNIQUE INDEX IF NOT EXISTS users_email ON users(email) WHERE email IS NOT NULL');
  const secure = publicUrl.protocol === 'https:';
  const cookie = (name, value, age) => `${name}=${value}; Path=${prefix}; HttpOnly; SameSite=Lax; Max-Age=${age}${secure ? '; Secure' : ''}`;
  const cookies = req => Object.fromEntries(String(req.headers.cookie || '').split(';').map(part => part.trim().split('=')).filter(part => part.length === 2));
  const session = req => {
    const value = cookies(req).be_session;
    if (!value || value.length > 128) return null;
    return db.prepare(`SELECT s.csrf,u.* FROM sessions s JOIN users u ON u.id=s.user_id WHERE s.token_hash=? AND s.expires_at>?`).get(digest(value), Date.now()) || null;
  };
  const userView = row => ({ id: row.id, login: row.login, avatar: row.avatar, profile_url: row.profile_url });
  const authorize = req => {
    const user = session(req);
    if (!user) fail(401, '请先登录。');
    if (req.headers.origin !== publicUrl.origin) fail(403, '请求来源不正确，请刷新页面后重试。');
    const supplied = String(req.headers['x-csrf-token'] || '');
    const suppliedBytes = Buffer.from(supplied), expectedBytes = Buffer.from(user.csrf);
    if (suppliedBytes.length !== expectedBytes.length || !timingSafeEqual(suppliedBytes, expectedBytes)) fail(403, '登录状态已变化，请刷新页面后重试。');
    return user;
  };
  const rateBuckets = new Map();
  const rate = (key, maximum, interval) => {
    const now = Date.now();
    let bucket = rateBuckets.get(key);
    if (!bucket || bucket.until < now) { bucket = { count: 0, until: now + interval }; rateBuckets.set(key, bucket); }
    if (++bucket.count > maximum) fail(429, '操作太频繁，请稍后再试。');
    if (rateBuckets.size > 10000) for (const [k, b] of rateBuckets) if (b.until < now) rateBuckets.delete(k);
    if (rateBuckets.size > 10000) fail(503, '服务繁忙，请稍后再试。');
  };
  const json = (res, status, body) => { res.writeHead(status, { 'Content-Type': 'application/json; charset=utf-8', 'Cache-Control': 'no-store' }); res.end(JSON.stringify(body)); };
  const redirect = (res, url, headers = {}) => { res.writeHead(302, { Location: url, 'Cache-Control': 'no-store', ...headers }); res.end(); };
  const bodyOf = async req => {
    if (!String(req.headers['content-type'] || '').startsWith('application/json')) fail(415, '请求需要使用 JSON 格式。');
    if (Number(req.headers['content-length'] || 0) > 98304) fail(413, '表单内容过长。');
    const chunks = []; let size = 0;
    for await (const chunk of req) { size += chunk.length; if (size > 98304) fail(413, '表单内容过长。'); chunks.push(chunk); }
    try { return JSON.parse(Buffer.concat(chunks).toString('utf8')); } catch { fail(400, '表单格式不正确。'); }
  };
  const transaction = action => {
    db.exec('BEGIN IMMEDIATE');
    try { const result = action(); db.exec('COMMIT'); return result; } catch (error) { db.exec('ROLLBACK'); throw error; }
  };
  const releaseView = row => ({ id: row.id, number: row.number, url: row.url, version: row.version, notes: row.notes, created_at: row.created_at });
  const resourceView = (row, history = false) => {
    const author = db.prepare('SELECT * FROM users WHERE id=?').get(row.owner_id);
    const latest = db.prepare('SELECT * FROM releases WHERE resource_id=? ORDER BY number DESC LIMIT 1').get(row.id);
    return { id: row.id, name: row.name, type: row.type, description: row.description, cover: row.cover, created_at: row.created_at, updated_at: row.updated_at, author: userView(author), latest: releaseView(latest), ...(history ? { releases: db.prepare('SELECT * FROM releases WHERE resource_id=? ORDER BY number DESC').all(row.id).map(releaseView) } : {}) };
  };
  const owned = (id, user) => {
    const row = db.prepare('SELECT * FROM resources WHERE id=?').get(id);
    if (!row) fail(404, '作品不存在或已被删除。');
    if (row.owner_id !== user.id) fail(403, '只有发布者可以修改这份作品。');
    return row;
  };
  const addRelease = (id, draft) => {
    if (draft.version && db.prepare('SELECT 1 FROM releases WHERE resource_id=? AND version=?').get(id, draft.version)) fail(409, '这个版本已发布，请填写新版本名或留空。');
    const next = db.prepare('SELECT COALESCE(MAX(number),0)+1 AS number FROM releases WHERE resource_id=?').get(id).number;
    db.prepare('INSERT INTO releases VALUES (?,?,?,?,?,?,?)').run(randomUUID(), id, next, draft.url, draft.version, draft.notes, Date.now());
  };
  const assets = path.resolve(options.assetsDir || path.join(root, 'dist'));
  const oauthFetch = options.oauthFetch || fetch;
  const mime = { '.html': 'text/html; charset=utf-8', '.js': 'text/javascript; charset=utf-8', '.css': 'text/css; charset=utf-8', '.svg': 'image/svg+xml', '.png': 'image/png', '.ico': 'image/x-icon' };
  const server = http.createServer(async (req, res) => {
    res.setHeader('X-Content-Type-Options', 'nosniff');
    res.setHeader('Referrer-Policy', 'no-referrer');
    res.setHeader('X-Frame-Options', 'DENY');
    res.setHeader('Content-Security-Policy', "default-src 'self'; script-src 'self'; style-src 'self' 'unsafe-inline'; img-src 'self' https: http: data:; connect-src 'self'; object-src 'none'; base-uri 'self'; frame-ancestors 'none'; form-action 'self'");
    try {
      const url = new URL(req.url, publicUrl);
      if (url.pathname === prefix.slice(0, -1)) return redirect(res, prefix);
      if (!url.pathname.startsWith(prefix)) return json(res, 404, { error: '页面不存在。' });
      const route = url.pathname.slice(prefix.length);
      if (route === 'api/health' && req.method === 'GET') return json(res, 200, { ok: true });
      if (route === 'api/session' && req.method === 'GET') {
        const user = session(req); return json(res, 200, { user: user ? userView(user) : null, csrf: user?.csrf || '', githubEnabled, emailEnabled, email: user?.email || '' });
      }
      if (route === 'api/logout' && req.method === 'POST') {
        authorize(req); db.prepare('DELETE FROM sessions WHERE token_hash=?').run(digest(cookies(req).be_session));
        res.setHeader('Set-Cookie', cookie('be_session', '', 0)); return json(res, 200, { ok: true });
      }
      if (route === 'auth/github' && req.method === 'GET') {
        if (!githubEnabled) return redirect(res, `${prefix}#/publish?login=unavailable`);
        const clientIp = String(req.headers['x-forwarded-for'] || req.socket.remoteAddress).slice(0, 128);
        rate(`auth:${clientIp}`, 30, 900000);
        const state = token(), verifier = token();
        db.prepare('INSERT INTO oauth_states VALUES (?,?,?)').run(digest(state), verifier, Date.now() + 600000);
        const target = new URL('https://github.com/login/oauth/authorize');
        target.search = new URLSearchParams({ client_id: githubId, redirect_uri: new URL('auth/github/callback', publicUrl.href.endsWith('/') ? publicUrl : `${publicUrl}/`).href, state, code_challenge: createHash('sha256').update(verifier).digest('base64url'), code_challenge_method: 'S256' }).toString();
        return redirect(res, target.href, { 'Set-Cookie': cookie('be_oauth', state, 600) });
      }
      if (route === 'auth/github/callback' && req.method === 'GET') {
        if (!githubEnabled) return redirect(res, `${prefix}#/publish?login=unavailable`);
        const state = url.searchParams.get('state') || '';
        const record = state.length <= 128 && state === cookies(req).be_oauth ? db.prepare('SELECT * FROM oauth_states WHERE state_hash=? AND expires_at>?').get(digest(state), Date.now()) : null;
        if (!record) return redirect(res, `${prefix}#/publish?login=failed`, { 'Set-Cookie': cookie('be_oauth', '', 0) });
        db.prepare('DELETE FROM oauth_states WHERE state_hash=?').run(digest(state));
        const code = url.searchParams.get('code');
        if (!code || code.length > 256 || url.searchParams.has('error')) return redirect(res, `${prefix}#/publish?login=failed`, { 'Set-Cookie': cookie('be_oauth', '', 0) });
        try {
          const exchanged = await oauthFetch('https://github.com/login/oauth/access_token', { method: 'POST', signal: AbortSignal.timeout(15000), headers: { Accept: 'application/json', 'Content-Type': 'application/x-www-form-urlencoded' }, body: new URLSearchParams({ client_id: githubId, client_secret: githubSecret, code, code_verifier: record.verifier, redirect_uri: new URL('auth/github/callback', publicUrl.href.endsWith('/') ? publicUrl : `${publicUrl}/`).href }) });
          const access = await exchanged.json();
          if (!exchanged.ok || typeof access.access_token !== 'string') throw new Error('exchange');
          const response = await oauthFetch('https://api.github.com/user', { signal: AbortSignal.timeout(15000), headers: { Authorization: `Bearer ${access.access_token}`, Accept: 'application/vnd.github+json', 'User-Agent': 'BetterEndfieldNext-ModCenter' } });
          const profile = await response.json();
          if (!response.ok || !Number.isSafeInteger(profile.id) || profile.id <= 0 || typeof profile.login !== 'string') throw new Error('profile');
          const known = db.prepare('SELECT id FROM users WHERE github_id=?').get(String(profile.id));
          const id = known?.id || randomUUID();
          const login = text(profile.login, 100, 'GitHub 用户名', true);
          const avatar = /^https:\/\/avatars\.githubusercontent\.com\//.test(profile.avatar_url || '') ? profile.avatar_url : '';
          const profileUrl = `https://github.com/${encodeURIComponent(login)}`;
          db.prepare('INSERT INTO users (id,github_id,login,avatar,profile_url) VALUES (?,?,?,?,?) ON CONFLICT(github_id) DO UPDATE SET login=excluded.login,avatar=excluded.avatar,profile_url=excluded.profile_url').run(id, String(profile.id), login, avatar, profileUrl);
          const issued = makeSession(db, id);
          return redirect(res, `${prefix}#/publish`, { 'Set-Cookie': [cookie('be_session', issued.value, 30 * 86400), cookie('be_oauth', '', 0)] });
        } catch { return redirect(res, `${prefix}#/publish?login=failed`, { 'Set-Cookie': cookie('be_oauth', '', 0) }); }
      }
      if (route === 'auth/email/request' && req.method === 'POST') {
        if (!emailEnabled) fail(503, '邮箱登录暂未配置。');
        if (req.headers.origin !== publicUrl.origin) fail(403, '请求来源不正确，请刷新页面后重试。');
        const body = await bodyOf(req);
        const email = emailAddress(body.email);
        const binding = body.purpose === 'bind';
        const user = binding ? authorize(req) : null;
        if (user?.email && user.email !== email) fail(409, '账号已绑定邮箱。');
        const clientIp = String(req.headers['x-forwarded-for'] || req.socket.remoteAddress).slice(0, 128);
        rate(`email-ip:${clientIp}`, 5, 900000);
        rate(`email:${digest(email)}`, 3, 900000);
        rate('email-global', 100, 86400000);
        const value = token(), code = randomInt(100000, 1000000).toString();
        const previous = cookies(req).be_email;
        if (previous?.length <= 128) db.prepare('DELETE FROM email_codes WHERE challenge_hash=?').run(digest(previous));
        db.prepare('DELETE FROM email_codes WHERE expires_at<?').run(Date.now());
        db.prepare('INSERT INTO email_codes(challenge_hash,code_hash,email,user_id,expires_at) VALUES (?,?,?,?,?)').run(digest(value), digest(`${value}:${code}`), email, user?.id || null, Date.now() + 10 * 60 * 1000);
        const english = body.lang === 'en';
        try {
          await sendEmail({ from: smtpFrom, to: email,
            subject: english ? 'Better Endfield sign-in code' : 'Better Endfield 下载站验证码',
            text: english ? `Your ${binding ? 'email linking' : 'sign-in'} code is: ${code}\n\nEnter it in the browser where you requested it. It expires in 10 minutes. If you did not request this email, ignore it.` : `你的${binding ? '邮箱绑定' : '登录'}验证码为：${code}\n\n请回到申请验证码的网页填写，10 分钟内有效。如果不是你发起的请求，可以忽略此邮件。` });
        } catch {
          db.prepare('DELETE FROM email_codes WHERE challenge_hash=?').run(digest(value));
          fail(503, '登录邮件发送失败，请稍后重试。');
        }
        res.setHeader('Set-Cookie', cookie('be_email', value, 600));
        return json(res, 202, { ok: true });
      }
      if (route === 'auth/email/verify' && req.method === 'POST') {
        if (!emailEnabled) fail(503, '邮箱登录暂未配置。');
        if (req.headers.origin !== publicUrl.origin) fail(403, '请求来源不正确，请刷新页面后重试。');
        const body = await bodyOf(req);
        const code = text(body.code, 6, '验证码', true);
        if (!/^\d{6}$/.test(code)) fail(400, '请填写六位数字验证码。');
        const value = cookies(req).be_email || '';
        const challenge = value.length <= 128 ? db.prepare('SELECT * FROM email_codes WHERE challenge_hash=? AND expires_at>?').get(digest(value), Date.now()) : null;
        if (!challenge || challenge.attempts >= 5) fail(400, '验证码无效或已过期，请重新发送。');
        if (!timingSafeEqual(Buffer.from(challenge.code_hash), Buffer.from(digest(`${value}:${code}`)))) {
          db.prepare('UPDATE email_codes SET attempts=attempts+1 WHERE challenge_hash=?').run(digest(value));
          fail(400, '验证码不正确，请重试。');
        }
        const bindingUser = challenge.user_id ? authorize(req) : null;
        if (bindingUser && bindingUser.id !== challenge.user_id) fail(403, '登录状态已变化，请刷新页面后重试。');
        const id = transaction(() => {
          const existing = db.prepare('SELECT * FROM users WHERE email=?').get(challenge.email);
          db.prepare('DELETE FROM email_codes WHERE challenge_hash=?').run(digest(value));
          if (bindingUser) {
            if (existing && existing.id !== bindingUser.id) fail(409, '该邮箱已用于另一个账号，请使用其他邮箱。');
            if (bindingUser.email && bindingUser.email !== challenge.email) fail(409, '账号已绑定邮箱。');
            db.prepare('UPDATE users SET email=? WHERE id=?').run(challenge.email, bindingUser.id);
            return bindingUser.id;
          }
          if (existing) return existing.id;
          const userId = randomUUID();
          db.prepare('INSERT INTO users (id,github_id,login,avatar,profile_url,email) VALUES (?,?,?,?,?,?)').run(userId, null, `creator-${userId.slice(0, 8)}`, '', '', challenge.email);
          return userId;
        });
        const issued = makeSession(db, id);
        res.setHeader('Set-Cookie', [cookie('be_session', issued.value, 30 * 86400), cookie('be_email', '', 0)]);
        const user = db.prepare('SELECT * FROM users WHERE id=?').get(id);
        return json(res, 200, { user: userView(user), csrf: issued.csrf, githubEnabled, emailEnabled, email: user.email });
      }
      if (route === 'api/resources' && req.method === 'GET') {
        const category = url.searchParams.get('type') || '';
        if (category && !typeIds.has(category)) fail(400, '资源类型不正确。');
        const query = text(url.searchParams.get('q'), 120, '搜索内容');
        const page = Math.max(1, Math.min(100000, Number(url.searchParams.get('page')) || 1)) | 0;
        const mine = url.searchParams.get('mine') === '1';
        const user = mine ? session(req) : null;
        if (mine && !user) fail(401, '请先登录。');
        const where = [], params = [];
        if (category) { where.push('r.type=?'); params.push(category); }
        if (query) { where.push('(r.name LIKE ? OR r.description LIKE ? OR u.login LIKE ?)'); params.push(...Array(3).fill(`%${query}%`)); }
        if (mine) { where.push('r.owner_id=?'); params.push(user.id); }
        const clause = where.length ? `WHERE ${where.join(' AND ')}` : '';
        const total = db.prepare(`SELECT COUNT(*) AS total FROM resources r JOIN users u ON u.id=r.owner_id ${clause}`).get(...params).total;
        const sort = url.searchParams.get('sort') === 'newest' ? 'r.created_at' : 'r.updated_at';
        const items = db.prepare(`SELECT r.* FROM resources r JOIN users u ON u.id=r.owner_id ${clause} ORDER BY ${sort} DESC,r.id LIMIT 24 OFFSET ?`).all(...params, (page - 1) * 24).map(row => resourceView(row));
        const counts = Object.fromEntries(types.map(item => [item.id, 0]));
        const rows = mine ? db.prepare('SELECT type,COUNT(*) AS n FROM resources WHERE owner_id=? GROUP BY type').all(user.id) : db.prepare('SELECT type,COUNT(*) AS n FROM resources GROUP BY type').all();
        for (const row of rows) counts[row.type] = row.n;
        return json(res, 200, { items, counts, total, page, pages: Math.max(1, Math.ceil(total / 24)) });
      }
      if (route === 'api/resources' && req.method === 'POST') {
        const user = authorize(req); rate(`write:${user.id}`, 60, 3600000);
        const draft = validateDraft(await bodyOf(req));
        const id = randomUUID(), now = Date.now();
        transaction(() => { db.prepare('INSERT INTO resources VALUES (?,?,?,?,?,?,?,?)').run(id, draft.name, draft.type, draft.description, draft.cover, user.id, now, now); addRelease(id, draft); });
        return json(res, 201, resourceView(db.prepare('SELECT * FROM resources WHERE id=?').get(id), true));
      }
      const resourceRoute = /^api\/resources\/([a-f0-9-]{36})(\/releases)?$/.exec(route);
      if (resourceRoute) {
        const id = resourceRoute[1];
        if (!resourceRoute[2] && req.method === 'GET') {
          const row = db.prepare('SELECT * FROM resources WHERE id=?').get(id);
          if (!row) fail(404, '作品不存在或已被删除。');
          return json(res, 200, resourceView(row, true));
        }
        if (resourceRoute[2] && req.method === 'POST') {
          const user = authorize(req); rate(`write:${user.id}`, 60, 3600000); const row = owned(id, user);
          const body = await bodyOf(req);
          const draft = validateDraft({ ...body, name: row.name, type: row.type });
          transaction(() => { addRelease(id, draft); db.prepare('UPDATE resources SET updated_at=? WHERE id=?').run(Date.now(), id); });
          return json(res, 201, resourceView(db.prepare('SELECT * FROM resources WHERE id=?').get(id), true));
        }
        if (!resourceRoute[2] && req.method === 'PUT') {
          const user = authorize(req); rate(`write:${user.id}`, 60, 3600000); owned(id, user);
          const draft = validateDraft(await bodyOf(req));
          const latest = db.prepare('SELECT * FROM releases WHERE resource_id=? ORDER BY number DESC LIMIT 1').get(id);
          transaction(() => {
            if (draft.url !== latest.url || draft.version !== latest.version) addRelease(id, draft);
            db.prepare('UPDATE resources SET name=?,type=?,description=?,cover=?,updated_at=? WHERE id=?').run(draft.name, draft.type, draft.description, draft.cover, Date.now(), id);
          });
          return json(res, 200, resourceView(db.prepare('SELECT * FROM resources WHERE id=?').get(id), true));
        }
        if (!resourceRoute[2] && req.method === 'DELETE') {
          const user = authorize(req); owned(id, user); db.prepare('DELETE FROM resources WHERE id=?').run(id);
          return json(res, 200, { ok: true });
        }
      }
      if (route.startsWith('api/') || route.startsWith('auth/')) return json(res, 404, { error: '接口不存在。' });
      if (!['GET', 'HEAD'].includes(req.method)) return json(res, 405, { error: '请求方法不支持。' });
      let relative;
      try { relative = decodeURIComponent(route); } catch { fail(400, '地址格式不正确。'); }
      if (relative.includes('\\') || relative.includes('\0') || relative.split('/').some(part => part.startsWith('.'))) fail(404, '页面不存在。');
      const filename = path.resolve(assets, relative || 'index.html');
      if (!filename.startsWith(`${assets}${path.sep}`)) fail(404, '页面不存在。');
      let info; try { info = await stat(filename); } catch { fail(404, '页面不存在。'); }
      if (!info.isFile()) fail(404, '页面不存在。');
      res.writeHead(200, { 'Content-Type': mime[path.extname(filename)] || 'application/octet-stream', 'Content-Length': info.size, 'Cache-Control': relative.startsWith('assets/') ? 'public,max-age=31536000,immutable' : 'no-cache' });
      if (req.method === 'HEAD') return res.end();
      const stream = createReadStream(filename); stream.on('error', () => res.destroy()); stream.pipe(res);
    } catch (error) {
      if (res.headersSent) return res.destroy();
      const status = Number(error.status) || 500;
      if (status === 500) console.error('Request failed:', error.code || error.name);
      json(res, status, { error: status === 500 ? '服务暂时不可用，请稍后重试。' : error.message });
    }
  });
  server.requestTimeout = 20000; server.headersTimeout = 10000; server.maxHeadersCount = 48; server.maxConnections = 128;
  const cleanup = setInterval(() => { db.prepare('DELETE FROM sessions WHERE expires_at<?').run(Date.now()); db.prepare('DELETE FROM oauth_states WHERE expires_at<?').run(Date.now()); db.prepare('DELETE FROM email_codes WHERE expires_at<?').run(Date.now()); }, 600000).unref();
  return { server, db, close: () => new Promise(resolve => { clearInterval(cleanup); server.close(() => { mailer?.close(); db.close(); resolve(); }); server.closeIdleConnections(); }) };
}

if (process.argv[1] && import.meta.url === pathToFileURL(path.resolve(process.argv[1])).href) {
  const app = createApp();
  const port = Number(process.env.PORT || 9017);
  app.server.listen(port, '127.0.0.1', () => console.info(`Endfield resource center listening on 127.0.0.1:${port}`));
  process.on('SIGTERM', () => app.close().then(() => process.exit(0)));
}
