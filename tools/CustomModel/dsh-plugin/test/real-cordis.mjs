/**
 * Run the bundle against the Cordis that ships inside an installed dsh.
 *
 * The stub in `test/cordis.mjs` models one behaviour — a context proxy throwing
 * on an undeclared service read — but it cannot prove that `ctx.inject` starts a
 * child fiber whose inject set actually contains the name, nor that a profile
 * missing the web surface still reaches an active state. Both were wrong once
 * and both cost a restart cycle to discover, so this file checks them on the
 * real thing.
 *
 * It needs the Cordis module inside an asar, which only Electron's Node can read:
 *
 *   ELECTRON_RUN_AS_NODE=1 "<path to DeepSeek Harness.exe>" test/real-cordis.mjs
 *
 * or point `DSH_CORDIS` at `.../node_modules/@deepseek-ai/cordis/lib/index.js`.
 * Without a readable Cordis the file exits 0 with a skip notice, so it is safe to
 * invoke from anywhere.
 */
import assert from 'node:assert/strict'
import { existsSync } from 'node:fs'
import { dirname, join, resolve } from 'node:path'
import { pathToFileURL } from 'node:url'
import { fileURLToPath } from 'node:url'

import { stagePlugin } from './stage.mjs'

const here = dirname(fileURLToPath(import.meta.url))

const candidates = [
  process.env.DSH_CORDIS,
  'D:/work/DSH/resources/app.asar/dsh/node_modules/@deepseek-ai/cordis/lib/index.js',
  join(process.env.LOCALAPPDATA || '', 'Programs/DeepSeek Harness/resources/app.asar/dsh/node_modules/@deepseek-ai/cordis/lib/index.js'),
  '/Applications/DeepSeek Harness.app/Contents/Resources/app.asar/dsh/node_modules/@deepseek-ai/cordis/lib/index.js',
].filter(Boolean)

const cordisPath = candidates.find((candidate) => existsSync(candidate))
if (!cordisPath) {
  console.log('skip: no readable Cordis; set DSH_CORDIS=<...>/cordis/lib/index.js and run under ELECTRON_RUN_AS_NODE')
  process.exit(0)
}

const { Context } = await import(pathToFileURL(cordisPath).href)
const { root } = stagePlugin()
const { apply, inject, name } = await import(pathToFileURL(join(root, 'lib/index.js')).href)

function services(record) {
  return {
    skills: { register(skill) { record.skills.push(skill); return () => {} } },
    tools: { register(definition) { record.tools.push(definition); return () => {} } },
    webServer: { register(route) { record.routes.push(route); return () => {} } },
  }
}

async function settle(fiber) {
  await fiber
  await new Promise((next) => setTimeout(next, 10))
}

const results = []
let failures = 0

async function check(label, body) {
  try {
    await body()
    results.push(`  ok   ${label}`)
  } catch (error) {
    failures += 1
    results.push(`  FAIL ${label}\n       ${String(error?.message || error).split('\n').join('\n       ')}`)
  }
}

/* The behaviour `mountService` is built around, asserted directly. */
await check('a fiber cannot read an undeclared service, but ctx.get can', async () => {
  const ctx = new Context()
  ctx.provide('skills', services({ skills: [], tools: [], routes: [] }).skills)
  let thrown = null
  let seen = 'unset'
  ctx.plugin({
    name: 'probe',
    inject: ['skills'],
    apply(inner) {
      try { void inner.webServer } catch (error) { thrown = error }
      seen = inner.get('webServer')
    },
  })
  await new Promise((next) => setTimeout(next, 20))
  assert.ok(thrown, 'reading an undeclared service inside a fiber must throw')
  assert.match(thrown.message, /cannot get property "webServer" without inject/)
  assert.equal(seen, undefined, 'get() is the non-strict read and must not throw')
})

await check('desktop profile: skill, routes and tool all mount, fiber stays active', async () => {
  const record = { skills: [], tools: [], routes: [] }
  const all = services(record)
  const ctx = new Context()
  ctx.provide('skills', all.skills)
  ctx.provide('webServer', all.webServer)
  ctx.provide('tools', all.tools)

  const fiber = ctx.plugin({ name, inject, apply })
  await settle(fiber)

  assert.equal(fiber.state, 2, 'the bundle fiber must reach active, not failed')
  assert.equal(record.skills.length, 1)
  assert.equal(record.routes.length, 1)
  assert.equal(record.tools.length, 1)
  assert.equal(record.routes[0].path, '/dsh-bem', 'the prefix is registered without a trailing slash')

  const doctor = await record.tools[0].execute({ action: 'doctor' })
  assert.deepEqual(doctor.surface, { routes: true, tool: true, notes: [] })

  // Cross-check the route against the webserver's *own* matcher rather than a
  // transcription of it. `match` only reads `exact` and `prefixes`, so calling it
  // on a duck-typed sink exercises the real dispatch without binding a port.
  const webserver = await import(pathToFileURL(join(dirname(cordisPath), '../../dsh-host-webserver/lib/index.js')).href)
    .catch((error) => {
      console.log(`  note the dsh-host-webserver module was not readable (${error.message}); matcher cross-check skipped`)
      return null
    })
  if (webserver?.WebServer) {
    const route = record.routes[0]
    const sink = { exact: new Map(), prefixes: new Map([[route.path, route]]) }
    for (const pathname of ['/dsh-bem', '/dsh-bem/', '/dsh-bem/api/latest', '/dsh-bem/assets/bcn.js']) {
      assert.equal(
        webserver.WebServer.prototype.match.call(sink, pathname),
        route,
        `${pathname} must be claimed by the preview route, not the SPA fallback`,
      )
    }
    assert.equal(webserver.WebServer.prototype.match.call(sink, '/dsh-bemx/api'), undefined)
    results.push('  ok   the real webserver matcher claims /dsh-bem and everything below it')
  } else {
    results.push('  skip the dsh-host-webserver module was not readable; matcher cross-check skipped')
  }
})

await check('headless profile: skill mounts, absent surfaces do not block the fiber', async () => {
  const record = { skills: [], tools: [], routes: [] }
  const all = services(record)
  const ctx = new Context()
  ctx.provide('skills', all.skills)

  const fiber = ctx.plugin({ name, inject, apply })
  await settle(fiber)

  assert.equal(fiber.state, 2, 'a missing web surface must not leave the fiber pending or failed')
  assert.equal(record.skills.length, 1)
  assert.equal(record.routes.length, 0)
  assert.equal(record.tools.length, 0)
})

await check('late services attach exactly once', async () => {
  const record = { skills: [], tools: [], routes: [] }
  const all = services(record)
  const ctx = new Context()
  ctx.provide('skills', all.skills)

  const fiber = ctx.plugin({ name, inject, apply })
  await settle(fiber)
  assert.equal(record.routes.length, 0)

  ctx.provide('webServer', all.webServer)
  ctx.provide('tools', all.tools)
  await new Promise((next) => setTimeout(next, 30))

  assert.equal(record.routes.length, 1, 'the route must attach when the web surface appears')
  assert.equal(record.tools.length, 1, 'the tool must attach when the tool host appears')
  assert.equal(fiber.state, 2)
})

console.log(results.join('\n'))
if (failures) {
  console.log(`\n${failures} check(s) failed against ${cordisPath}`)
  process.exit(1)
}
console.log(`\nok: ${name} behaves on the real Cordis (${resolve(cordisPath).replace(/^.*node_modules/, 'node_modules')})`)
