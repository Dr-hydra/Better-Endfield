import { test } from 'node:test';
import assert from 'node:assert/strict';
import { createApp, makeSession, validateDraft } from './server.mjs';

const minimum = { name: '测试模型', type: 'bem', url: 'https://example.com/model.zip' };
async function fixture(extra = {}) {
  const app = createApp({ databasePath: ':memory:', publicUrl: 'https://mods.example/endfield/', ...extra });
  app.db.prepare('INSERT INTO users VALUES (?,?,?,?,?)').run('author', '123', 'creator', '', 'https://github.com/creator');
  app.db.prepare('INSERT INTO users VALUES (?,?,?,?,?)').run('other', '456', 'other', '', 'https://github.com/other');
  const author = makeSession(app.db, 'author'), other = makeSession(app.db, 'other');
  await new Promise(resolve => app.server.listen(0, '127.0.0.1', resolve));
  const root = `http://127.0.0.1:${app.server.address().port}/endfield/`;
  const call = async (route, { method = 'GET', body, who = author, headers = {}, anonymous = false } = {}) => {
    const response = await fetch(root + route, { method, redirect: 'manual', headers: {
      ...(anonymous ? {} : { Cookie: `be_session=${who.value}`, 'X-CSRF-Token': who.csrf }),
      Origin: 'https://mods.example', ...(body === undefined ? {} : { 'Content-Type': 'application/json' }), ...headers,
    }, ...(body === undefined ? {} : { body: JSON.stringify(body) }) });
    return { status: response.status, headers: response.headers, data: response.headers.get('content-type')?.includes('json') ? await response.json() : await response.text() };
  };
  return { ...app, call, author, other };
}

test('only name, type and link are required; dangerous URLs and unknown types are rejected', () => {
  assert.deepEqual(validateDraft(minimum), { ...minimum, version: '', cover: '', description: '', notes: '' });
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
test('pagination is bounded and private source files are not served', async () => {
  const app = await fixture();
  try {
    for (let n = 0; n < 25; n++) assert.equal((await app.call('api/resources', { method: 'POST', body: { ...minimum, name: `作品 ${n}` } })).status, 201);
    const page = await app.call('api/resources?page=2', { anonymous: true });
    assert.equal(page.data.items.length, 1); assert.equal(page.data.pages, 2);
    for (const route of ['.env', 'server/server.mjs', '%2e%2e/server/server.mjs']) assert.equal((await app.call(route, { anonymous: true })).status, 404);
  } finally { await app.close(); }
});
