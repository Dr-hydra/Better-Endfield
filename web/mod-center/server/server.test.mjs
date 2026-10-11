import { test } from 'node:test';
import assert from 'node:assert/strict';
import { mkdtemp, rm } from 'node:fs/promises';
import { tmpdir } from 'node:os';
import path from 'node:path';
import { Readable } from 'node:stream';
import { createApp, makeSession, validateDraft } from './server.mjs';

const minimum = { name: '测试模型', type: 'bem', url: 'https://example.com/model.zip' };
const png = Buffer.from('iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAQAAAC1HAwCAAAAC0lEQVR42mP8/x8AAwMCAO+jfuQAAAAASUVORK5CYII=', 'base64');
const uploadHeaders = name => ({ 'Content-Type': 'application/octet-stream', 'X-Upload-Name': encodeURIComponent(name) });
async function fixture(extra = {}) {
  const uploadDirectory = await mkdtemp(path.join(tmpdir(), 'endfield-upload-test-'));
  const app = createApp({ databasePath: ':memory:', publicUrl: 'https://mods.example/endfield/', uploadDirectory, ...extra });
  app.db.prepare('INSERT INTO users (id,github_id,login,avatar,profile_url) VALUES (?,?,?,?,?)').run('author', '123', 'creator', '', 'https://github.com/creator');
  app.db.prepare('INSERT INTO users (id,github_id,login,avatar,profile_url) VALUES (?,?,?,?,?)').run('other', '456', 'other', '', 'https://github.com/other');
  const author = makeSession(app.db, 'author'), other = makeSession(app.db, 'other');
  await new Promise(resolve => app.server.listen(0, '127.0.0.1', resolve));
  const root = `http://127.0.0.1:${app.server.address().port}/endfield/`;
  const call = async (route, { method = 'GET', body, raw, who = author, headers = {}, anonymous = false } = {}) => {
    const response = await fetch(root + route, { method, redirect: 'manual', headers: {
      ...(anonymous ? {} : { Cookie: `be_session=${who.value}`, 'X-CSRF-Token': who.csrf }),
      Origin: 'https://mods.example', ...(body === undefined ? {} : { 'Content-Type': 'application/json' }), ...headers,
    }, ...(raw !== undefined ? { body: raw, duplex: 'half' } : body === undefined ? {} : { body: JSON.stringify(body) }) });
    return { status: response.status, headers: response.headers, data: response.headers.get('content-type')?.includes('json') ? await response.json() : await response.text() };
  };
  return { ...app, call, author, other, close: async () => { await app.close(); await rm(uploadDirectory, { recursive: true, force: true }); } };
}

test('only name, type and link are required; dangerous URLs and unknown types are rejected', () => {
  assert.deepEqual(validateDraft(minimum), { ...minimum, version: '', cover: '', description: '', notes: '', file_id: '', image_ids: undefined });
  for (const key of ['name', 'type', 'url']) assert.throws(() => validateDraft({ ...minimum, [key]: '' }), { status: 400 });
  for (const url of ['javascript:alert(1)', 'data:text/html,hello', 'file:///etc/passwd', 'https://user:pass@example.com/']) assert.throws(() => validateDraft({ ...minimum, url }), { status: 400 });
  assert.throws(() => validateDraft({ ...minimum, type: 'invalid' }), { status: 400 });
});
test('anonymous writes are denied even when OAuth is not configured', async () => {
  const app = await fixture();
  try {
    assert.equal((await app.call('api/resources', { method: 'POST', body: minimum, anonymous: true })).status, 401);
    const session = await app.call('api/session', { anonymous: true });
    assert.equal(session.data.githubEnabled, false); assert.equal(session.data.user, null);
    assert.equal((await app.call('auth/github', { anonymous: true })).headers.get('location'), '/endfield/#/publish?login=unavailable');
  } finally { await app.close(); }
});
test('publish minimal resource, filter by type and search author without login', async () => {
  const app = await fixture();
  try {
    const created = await app.call('api/resources', { method: 'POST', body: minimum });
    assert.equal(created.status, 201); assert.equal(created.data.latest.number, 1); assert.equal(created.data.latest.version, '');
    const list = await app.call('api/resources?type=bem&q=creator', { anonymous: true });
    assert.equal(list.data.total, 1); assert.equal(list.data.counts.bem, 1); assert.equal(list.data.items[0].author.id, 'author');
    assert.equal((await app.call('api/resources?type=skin', { anonymous: true })).data.total, 0);
    assert.equal((await app.call('api/resources?type=unknown', { anonymous: true })).status, 400);
    assert.equal((await app.call('api/resources?mine=1', { anonymous: true })).status, 401);
  } finally { await app.close(); }
});
test('ownership, origin and CSRF are enforced', async () => {
  const app = await fixture();
  try {
    const created = await app.call('api/resources', { method: 'POST', body: minimum });
    for (const method of ['PUT', 'DELETE']) assert.equal((await app.call(`api/resources/${created.data.id}`, { method, body: minimum, who: app.other })).status, 403);
    assert.equal((await app.call('api/resources', { method: 'POST', body: minimum, headers: { Origin: 'https://attacker.example' } })).status, 403);
    assert.equal((await app.call('api/resources', { method: 'POST', body: minimum, headers: { 'X-CSRF-Token': 'invalid' } })).status, 403);
    assert.equal((await app.call('api/resources', { method: 'POST', body: minimum, headers: { 'Content-Type': 'text/plain' } })).status, 415);
  } finally { await app.close(); }
});
test('editing preserves old links and releases; duplicate version rolls back metadata changes', async () => {
  const app = await fixture();
  try {
    const first = await app.call('api/resources', { method: 'POST', body: { ...minimum, version: '1.0' } });
    const id = first.data.id;
    const edit = await app.call(`api/resources/${id}`, { method: 'PUT', body: { ...minimum, version: '1.0', description: '<script>alert(1)</script>\n安装说明' } });
    assert.equal(edit.status, 200); assert.equal(edit.data.releases.length, 1);
    const update = await app.call(`api/resources/${id}/releases`, { method: 'POST', body: { url: 'https://example.com/new.zip', notes: '更新模型' } });
    assert.equal(update.status, 201); assert.equal(update.data.releases.length, 2); assert.equal(update.data.releases[1].url, minimum.url);
    const duplicate = await app.call(`api/resources/${id}`, { method: 'PUT', body: { ...minimum, name: '不应该被保存', url: 'https://example.com/third.zip', version: '1.0' } });
    assert.equal(duplicate.status, 409);
    const current = await app.call(`api/resources/${id}`);
    assert.equal(current.data.name, minimum.name); assert.equal(current.data.releases.length, 2);
  } finally { await app.close(); }
});
test('deleting your resource removes its history; logout revokes the session', async () => {
  const app = await fixture();
  try {
    const created = await app.call('api/resources', { method: 'POST', body: minimum });
    assert.equal((await app.call(`api/resources/${created.data.id}`, { method: 'DELETE' })).status, 200);
    assert.equal((await app.call(`api/resources/${created.data.id}`)).status, 404);
    assert.equal(app.db.prepare('SELECT COUNT(*) AS n FROM releases').get().n, 0);
    assert.equal((await app.call('api/logout', { method: 'POST', body: {} })).status, 200);
    assert.equal((await app.call('api/resources', { method: 'POST', body: minimum })).status, 401);
  } finally { await app.close(); }
});
test('OAuth state is browser-bound, single-use, and uses the stable GitHub account ID', async () => {
  let login = 'original-name'; let exchanges = 0;
  const app = await fixture({ githubId: 'test-client', githubSecret: 'test-secret', oauthFetch: async (url, options) => {
    if (url.includes('access_token')) { exchanges++; assert.ok(options.body.get('code_verifier')); return Response.json({ access_token: 'test-access-token' }); }
    return Response.json({ id: 999, login, avatar_url: 'https://avatars.githubusercontent.com/u/999' });
  } });
  try {
    async function authenticate() {
      const begin = await app.call('auth/github', { anonymous: true });
      const target = new URL(begin.headers.get('location'));
      assert.equal(target.hostname, 'github.com'); assert.equal(target.searchParams.get('code_challenge_method'), 'S256');
      assert.equal(target.searchParams.get('redirect_uri'), 'https://mods.example/endfield/auth/github/callback');
      const cookie = begin.headers.get('set-cookie').split(';')[0];
      assert.match(begin.headers.get('set-cookie'), /HttpOnly/); assert.match(begin.headers.get('set-cookie'), /Secure/);
      const callback = `auth/github/callback?code=test-code&state=${target.searchParams.get('state')}`;
      assert.match((await app.call(callback, { anonymous: true })).headers.get('location'), /login=failed/);
      const success = await app.call(callback, { anonymous: true, headers: { Cookie: cookie } });
      assert.equal(success.headers.get('location'), '/endfield/#/publish');
      const sessionCookie = success.headers.getSetCookie().find(value => value.startsWith('be_session=')).split(';')[0];
      assert.match((await app.call(callback, { anonymous: true, headers: { Cookie: cookie } })).headers.get('location'), /login=failed/);
      return (await app.call('api/session', { anonymous: true, headers: { Cookie: sessionCookie } })).data.user;
    }
    const original = await authenticate(); login = 'renamed-account'; const renamed = await authenticate();
    assert.equal(original.id, renamed.id); assert.equal(renamed.login, login); assert.equal(exchanges, 2);
  } finally { await app.close(); }
});

test('likes require login and CSRF, are unique per account, and can be cancelled', async () => {
  const app = await fixture();
  try {
    const resource = (await app.call('api/resources', { method: 'POST', body: minimum })).data;
    const endpoint = `api/resources/${resource.id}/likes`;
    assert.equal((await app.call(endpoint, { method: 'PUT', anonymous: true })).status, 401);
    assert.equal((await app.call(endpoint, { method: 'PUT', headers: { 'X-CSRF-Token': 'wrong' } })).status, 403);
    assert.equal((await app.call(endpoint, { method: 'PUT' })).data.likes, 1);
    assert.equal((await app.call(endpoint, { method: 'PUT' })).data.likes, 1);
    assert.equal((await app.call(endpoint, { method: 'PUT', who: app.other })).data.likes, 2);
    const anonymous = (await app.call(`api/resources/${resource.id}`, { anonymous: true })).data;
    assert.equal(anonymous.likes, 2); assert.equal(anonymous.liked, false);
    assert.equal((await app.call(endpoint, { method: 'DELETE' })).data.likes, 1);
    assert.equal((await app.call(endpoint, { method: 'DELETE' })).data.liked, false);
    assert.equal((await app.call('api/resources')).data.items[0].likes, 1);
  } finally { await app.close(); }
});

test('comments remain plain text, enforce limits, pagination, and self/admin deletion', async () => {
  const app = await fixture({ fileUploadGithubIds: '123' });
  try {
    const resource = (await app.call('api/resources', { method: 'POST', body: minimum })).data;
    const endpoint = `api/resources/${resource.id}/comments`;
    assert.equal((await app.call(endpoint, { method: 'POST', anonymous: true, body: { body: 'hi' } })).status, 401);
    assert.equal((await app.call(endpoint, { method: 'POST', body: { body: ' ' } })).status, 400);
    assert.equal((await app.call(endpoint, { method: 'POST', body: { body: 'x'.repeat(2001) } })).status, 400);
    const posted = await app.call(endpoint, { method: 'POST', body: { body: '<script>alert(1)</script>\n评论' } });
    assert.equal(posted.status, 201);
    assert.equal((await app.call(endpoint, { method: 'POST', body: { body: 'again' } })).status, 429);
    const list = await app.call(endpoint, { anonymous: true });
    assert.equal(list.data.items[0].body, '<script>alert(1)</script>\n评论');
    assert.equal(list.data.items[0].canDelete, false);
    assert.equal((await app.call(`${endpoint}/${posted.data.id}`, { method: 'DELETE', who: app.other })).status, 403);
    assert.equal((await app.call(`${endpoint}/${posted.data.id}`, { method: 'DELETE' })).status, 200);
    const other = await app.call(endpoint, { method: 'POST', who: app.other, body: { body: 'other comment' } });
    assert.equal((await app.call(endpoint)).data.items[0].canDelete, true);
    assert.equal((await app.call(`${endpoint}/${other.data.id}`, { method: 'DELETE' })).status, 200);
    for (let index = 0; index < 25; index++) app.db.prepare('INSERT INTO comments VALUES (?,?,?,?,?)').run(`comment-${index}`, resource.id, 'other', 'comment', index);
    assert.equal((await app.call(endpoint)).data.items.length, 20);
    assert.equal((await app.call(`${endpoint}?page=2`)).data.items.length, 5);
    await app.call(`api/resources/${resource.id}`, { method: 'DELETE' });
    assert.equal(app.db.prepare('SELECT COUNT(*) AS n FROM comments').get().n, 0);
  } finally { await app.close(); }
});

test('file upload rights use stable GitHub IDs; hosted files preserve old releases and support ranges', async () => {
  const app = await fixture({ fileUploadGithubIds: '123' });
  try {
    app.db.prepare('UPDATE users SET login=? WHERE id=?').run('Dr-hydra', 'other');
    assert.equal((await app.call('api/session')).data.canUploadFiles, true);
    assert.equal((await app.call('api/session', { who: app.other })).data.canUploadFiles, false);
    assert.equal((await app.call('api/uploads?kind=file', { method: 'POST', who: app.other, raw: Buffer.from('file'), headers: uploadHeaders('mod.zip') })).status, 403);
    const first = await app.call('api/uploads?kind=file', { method: 'POST', raw: Buffer.from('old-file'), headers: uploadHeaders('模型.zip') });
    assert.equal(first.status, 201);
    const media = `media/${first.data.id}`;
    assert.equal((await app.call(media, { anonymous: true })).status, 404);
    assert.equal((await app.call('api/resources', { method: 'POST', who: app.other, body: { ...minimum, file_id: first.data.id } })).status, 403);
    const created = await app.call('api/resources', { method: 'POST', body: { ...minimum, url: '', file_id: first.data.id } });
    assert.equal(created.status, 201); assert.equal(created.data.latest.file.filename, '模型.zip');
    const downloaded = await app.call(media, { anonymous: true });
    assert.equal(downloaded.data, 'old-file'); assert.match(downloaded.headers.get('content-disposition'), /attachment/);
    const range = await app.call(media, { anonymous: true, headers: { Range: 'bytes=0-2' } });
    assert.equal(range.status, 206); assert.equal(range.data, 'old');
    assert.equal((await app.call(media, { headers: { Range: 'bytes=999-1000' } })).status, 416);
    assert.equal((await app.call(media, { method: 'HEAD', anonymous: true })).headers.get('content-length'), '8');
    assert.equal((await app.call(`api/uploads/${first.data.id}`, { method: 'DELETE' })).status, 409);
    const second = await app.call('api/uploads?kind=file', { method: 'POST', raw: Buffer.from('new-file'), headers: uploadHeaders('new.zip') });
    const release = await app.call(`api/resources/${created.data.id}/releases`, { method: 'POST', body: { file_id: second.data.id } });
    assert.equal(release.data.releases.length, 2); assert.equal(release.data.releases[1].file.id, first.data.id);
    await app.call(`api/resources/${created.data.id}`, { method: 'DELETE' });
    assert.equal((await app.call(media, { anonymous: true })).status, 404);
  } finally { await app.close(); }
});

test('image uploads enforce contents, ownership and six-image gallery limit', async () => {
  const app = await fixture();
  try {
    assert.equal((await app.call('api/uploads?kind=image', { method: 'POST', anonymous: true, raw: png, headers: uploadHeaders('a.png') })).status, 401);
    assert.equal((await app.call('api/uploads?kind=image', { method: 'POST', raw: Buffer.from('<svg onload="alert(1)"></svg>'), headers: uploadHeaders('fake.png') })).status, 415);
    const ids = [];
    for (let index = 0; index < 7; index++) { const uploaded = await app.call('api/uploads?kind=image', { method: 'POST', raw: png, headers: uploadHeaders(`${index}.png`) }); assert.equal(uploaded.status, 201); ids.push(uploaded.data.id); }
    assert.equal((await app.call('api/resources', { method: 'POST', body: { ...minimum, image_ids: ids } })).status, 400);
    assert.equal((await app.call('api/resources', { method: 'POST', who: app.other, body: { ...minimum, image_ids: [ids[0]] } })).status, 403);
    const created = await app.call('api/resources', { method: 'POST', body: { ...minimum, image_ids: ids.slice(0, 6) } });
    assert.equal(created.data.images.length, 6); assert.match(created.data.cover, new RegExp(ids[0]));
    assert.equal((await app.call(`media/${ids[0]}`, { anonymous: true })).status, 200);
    assert.equal((await app.call(`media/${ids[6]}`, { anonymous: true })).status, 404);
    const edited = await app.call(`api/resources/${created.data.id}`, { method: 'PUT', body: { ...minimum, image_ids: [ids[1]] } });
    assert.equal(edited.data.images.length, 1); assert.match(edited.data.cover, new RegExp(ids[1]));
    assert.equal((await app.call(`api/uploads/${ids[6]}`, { method: 'DELETE' })).status, 200);
    assert.equal((await app.call(`api/uploads/${ids[1]}`, { method: 'DELETE', who: app.other })).status, 403);
  } finally { await app.close(); }
});

test('upload bounds reject oversized declared and chunked bodies without retaining files', async () => {
  const app = await fixture({ fileUploadGithubIds: '123', uploadLimits: { imageBytes: 32, fileBytes: 8, imagesPerWork: 6 } });
  try {
    assert.equal((await app.call('api/uploads?kind=image', { method: 'POST', raw: png, headers: uploadHeaders('a.png') })).status, 413);
    assert.equal((await app.call('api/uploads?kind=file', { method: 'POST', raw: Buffer.from('123456789'), headers: uploadHeaders('a.zip') })).status, 413);
    assert.equal((await app.call('api/uploads?kind=file', { method: 'POST', raw: Readable.from([Buffer.from('1234'), Buffer.from('56789')]), headers: uploadHeaders('a.zip') })).status, 413);
    assert.equal(app.db.prepare('SELECT COUNT(*) AS n FROM uploads').get().n, 0);
    assert.equal((await app.call('api/uploads?kind=file', { method: 'POST', raw: Buffer.alloc(0), headers: uploadHeaders('empty.zip') })).status, 400);
  } finally { await app.close(); }
});
test('email codes create a session and can only be used once', async () => {
  const sent = [];
  const app = await fixture({ smtpFrom: 'no-reply@example.com', sendEmail: async message => { sent.push(message); } });
  try {
    const status = await app.call('api/session', { anonymous: true });
    assert.equal(status.data.emailEnabled, true);
    const requested = await app.call('auth/email/request', { method: 'POST', body: { email: 'Author@example.com' }, anonymous: true });
    assert.equal(requested.status, 202); assert.equal(sent.length, 1); assert.match(sent[0].to, /^author@example\.com$/);
    assert.match(sent[0].text, /\b\d{6}\b/);
    const code = sent[0].text.match(/\b(\d{6})\b/)[1];
    const emailCookie = requested.headers.getSetCookie().find(value => value.startsWith('be_email=')).split(';')[0];
    const verified = await app.call('auth/email/verify', { method: 'POST', body: { code }, anonymous: true, headers: { Cookie: emailCookie } });
    assert.equal(verified.status, 200);
    const sessionCookie = verified.headers.getSetCookie().find(value => value.startsWith('be_session=')).split(';')[0];
    const session = await app.call('api/session', { anonymous: true, headers: { Cookie: sessionCookie } });
    assert.match(session.data.user.login, /^creator-[a-f0-9]{8}$/);
    assert.equal(app.db.prepare('SELECT email FROM users WHERE id=?').get(session.data.user.id).email, 'author@example.com');
    const reused = await app.call('auth/email/verify', { method: 'POST', body: { code }, anonymous: true, headers: { Cookie: emailCookie } });
    assert.equal(reused.status, 400);
  } finally { await app.close(); }
});
test('pagination is bounded and private source files are not served', async () => {
  const app = await fixture();
  try {
    for (let n = 0; n < 25; n++) assert.equal((await app.call('api/resources', { method: 'POST', body: { ...minimum, name: `作品 ${n}` } })).status, 201);
    const page = await app.call('api/resources?page=2', { anonymous: true });
    assert.equal(page.data.items.length, 1); assert.equal(page.data.pages, 2);
    for (const route of ['.env', 'server/server.mjs', '%2e%2e/server/server.mjs']) assert.equal((await app.call(route, { anonymous: true })).status, 404);
  } finally { await app.close(); }
});
