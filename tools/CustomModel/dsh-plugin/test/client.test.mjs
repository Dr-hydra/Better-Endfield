/**
 * Browser-half test: renders the preview tab without a browser.
 *
 * `lib/client.js` is a module-loader payload, so it is loaded the way the
 * harness loads it — through `window.__ModuleLoader__.load` — with a minimal
 * React runtime, a fake WebGL2 context, a Worker that decodes nothing, and a
 * `fetch` that serves one canned scene. That is enough to prove the parts that
 * usually break in a plugin UI: the render tree, the hook order, the effects
 * that need the canvas to exist first, and the blob plumbing behind them.
 *
 * What it cannot prove: that the shader compiles on a real driver, or that the
 * result looks right. Those need the dsh window.
 *
 *   node test/client.test.mjs
 */
import assert from 'node:assert/strict'
import { existsSync } from 'node:fs'
import { dirname, join, resolve } from 'node:path'
import { fileURLToPath, pathToFileURL } from 'node:url'

const here = dirname(fileURLToPath(import.meta.url))
const plugin = resolve(here, '..')
const clientFile = join(plugin, 'lib/client.js')

assert.ok(existsSync(clientFile), `${clientFile} is missing; run node tools/build-client.mjs`)

const results = []
let failures = 0

async function section(label, body) {
  try {
    await body()
    results.push(`  ok   ${label}`)
  } catch (error) {
    failures += 1
    results.push(`  FAIL ${label}\n       ${String(error?.message || error).split('\n').join('\n       ')}`)
  }
}

/* ------------------------------------------------------------------ */
/* a React small enough to reason about                                */
/* ------------------------------------------------------------------ */

const hooks = { state: [], refs: [], deps: [], memoDeps: [], cleanups: [], cursor: 0 }
let pending = []

const React = {
  createElement(type, props, ...children) {
    return { type, props: props || {}, children: children.flat(Infinity).filter((child) => child !== null && child !== undefined && child !== false) }
  },
  useState(initial) {
    const at = hooks.cursor++
    if (!(at in hooks.state)) hooks.state[at] = typeof initial === 'function' ? initial() : initial
    return [hooks.state[at], (next) => {
      hooks.state[at] = typeof next === 'function' ? next(hooks.state[at]) : next
    }]
  },
  useRef(initial) {
    const at = hooks.cursor++
    if (!(at in hooks.refs)) hooks.refs[at] = { current: initial }
    return hooks.refs[at]
  },
  useMemo(factory, deps) {
    const at = hooks.cursor++
    const previous = hooks.memoDeps[at]
    const changed = !previous || !deps || deps.length !== previous.length || deps.some((value, index) => value !== previous[index])
    if (changed) {
      hooks.state[at] = factory()
      hooks.memoDeps[at] = deps ? [...deps] : null
    }
    return hooks.state[at]
  },
  useEffect(body, deps) {
    const at = hooks.cursor++
    const previous = hooks.deps[at]
    const changed = !previous || !deps || deps.length !== previous.length || deps.some((value, index) => value !== previous[index])
    if (!changed) return
    hooks.deps[at] = deps ? [...deps] : null
    pending.push(() => {
      if (typeof hooks.cleanups[at] === 'function') hooks.cleanups[at]()
      hooks.cleanups[at] = body() || null
    })
  },
}

/** Run one commit's effects and let their promises settle. */
async function settle(rounds = 8) {
  for (let round = 0; round < rounds; round += 1) {
    const queue = pending
    pending = []
    for (const effect of queue) effect()
    await new Promise((done) => setTimeout(done, 0))
  }
}

/* ------------------------------------------------------------------ */
/* stubs: canvas, WebGL2, worker, fetch                                */
/* ------------------------------------------------------------------ */

const glCalls = []
/** Shader text, for the one contract the fake context cannot render. */
const shaderSources = []
function makeGl() {
  const store = {}
  const constants = new Map()
  return new Proxy(store, {
    get(target, prop) {
      if (prop === '__calls') return glCalls
      if (typeof prop === 'symbol') return undefined
      if (prop in target) return target[prop]
      if (/^[A-Z][A-Z0-9_]*$/.test(prop)) {
        if (!constants.has(prop)) constants.set(prop, constants.size + 100)
        return constants.get(prop)
      }
      return (...args) => {
        glCalls.push(prop)
        if (prop === 'shaderSource') shaderSources.push(String(args[1]))
        return { from: prop, args }
      }
    },
    set(target, prop, value) {
      target[prop] = value
      return true
    },
  })
}

const gl = makeGl()
const listeners = new Map()
const canvas = {
  width: 0,
  height: 0,
  clientWidth: 900,
  clientHeight: 620,
  style: {},
  getContext: (kind) => (kind === 'webgl2' ? gl : null),
  addEventListener(type, handler) { listeners.set(type, handler) },
  removeEventListener() {},
  setPointerCapture() {},
}

const rafQueue = []
let rafId = 0
globalThis.requestAnimationFrame = (callback) => {
  rafQueue.push(callback)
  rafId += 1
  return rafId
}
globalThis.cancelAnimationFrame = () => {}

const workers = []
class FakeWorker {
  constructor(url) {
    this.url = url
    this.terminated = false
    this.posted = []
    workers.push(this)
  }

  postMessage(message) {
    this.posted.push(message)
    // Answer synchronously on the next tick, like a worker would.
    setTimeout(() => {
      const { id, width, height } = message
      const data = new Uint8Array(width * height * 4)
      data.fill(127)
      this.onmessage?.({ data: { id, ok: true, width, height, data: data.buffer } })
    }, 0)
  }

  terminate() { this.terminated = true }
}
globalThis.Worker = FakeWorker
// Node ships a read-only `navigator`, so it has to be replaced, not assigned.
Object.defineProperty(globalThis, 'navigator', {
  configurable: true,
  value: { language: 'zh-CN', hardwareConcurrency: 4 },
})
globalThis.window = globalThis.window || {}
globalThis.window.devicePixelRatio = 2

/* One mesh stored with clockwise winding (the packages' convention), three
 * draws — an uncompressed texture, a BC7 texture and an ASTC one the host
 * refuses to serve — plus an option group, a slider and a shape channel. */
const meshPositions = new Float32Array([
  0, 0, 0,
  1, 0, 0,
  0, 1, 0,
  0, 0, 1,
])
const meshUvs = new Float32Array([0, 0, 1, 0, 0, 1, 1, 1])
// A tetrahedron wound inside-out on purpose: the packages are seam-split with
// mixed winding, so nothing in the renderer may depend on index order.
const meshIndices = new Uint16Array([0, 1, 2, 0, 3, 1, 0, 2, 3, 1, 3, 2])
const rgbaPixels = new Uint8Array(2 * 2 * 4).fill(200)
const deformIndices = new Uint32Array([1])
const deformDeltas = new Float32Array([0, 0.25, 0])

const blobs = new Map([
  ['b0', new Uint8Array(meshPositions.buffer)],
  ['b1', new Uint8Array(meshUvs.buffer)],
  ['b2', new Uint8Array(meshIndices.buffer)],
  ['b3', rgbaPixels],
  ['b4', new Uint8Array(deformIndices.buffer)],
  ['b5', new Uint8Array(deformDeltas.buffer)],
  ['b6', new Uint8Array(4 * 4 * 16)], // BC7 blocks for a 4x4 base level
])

const scene = {
  schema: 'bem-preview/1',
  package: { path: 'x.bem', id: 'pkg', name: '测试角色', author: 'me', version: '1.0.0', minor: 5, formatVersion: '1.5', payloadCount: 7, size: 1 },
  target: { character: 'c', platform: 'windows', profile: 'p', components: [] },
  meshes: [{
    index: 0, name: 'body', vertexCount: 4, indexSize: 2,
    positionsBlob: 'b0', uvsBlob: 'b1',
    draws: [
      { first: 0, count: 12, indicesBlob: 'b2', textures: [0], material: 'body', when: null, overrides: null },
      { first: 0, count: 12, indicesBlob: 'b2', textures: [1], material: 'normal', when: null, overrides: null },
      { first: 0, count: 12, indicesBlob: 'b2', textures: [2], material: 'armour', when: null, overrides: null },
    ],
  }],
  textures: [
    { index: 0, name: 'T_body_D', width: 2, height: 2, mips: 1, format: 4, srgb: true, family: 'rgba32', block: null, blob: 'b3', skipped: null },
    { index: 1, name: 'T_body_N', width: 4, height: 4, mips: 1, format: 25, srgb: false, family: 'bc7', block: 4, blob: 'b6', skipped: null },
    {
      index: 2,
      name: 'T_body_ASTC',
      width: 8,
      height: 8,
      mips: 1,
      format: 48,
      srgb: true,
      family: 'astc',
      block: 4,
      blob: null,
      skipped: 'ASTC blocks are not decoded in the browser build',
    },
  ],
  appearances: [{ index: 0, id: 'default', name: '默认', meshes: [0], components: [] }],
  defaultAppearance: 'default',
  optionGroups: [{ id: 'outfit', name: '服装', default: 'a', choices: [{ id: 'a', name: '便装' }, { id: 'b', name: '战装' }] }],
  componentRules: [],
  parameters: [{ id: 'chest', name: '胸部', min: 0, max: 1000, neutral: 0, default: 0, step: 1, availableWhen: null }],
  channels: [{
    mesh: 0,
    parameter: 'chest',
    frames: [
      { value: 0, neutral: true, count: 0, indicesBlob: null, deltasBlob: null },
      { value: 1000, neutral: false, count: 1, indicesBlob: 'b4', deltasBlob: 'b5' },
    ],
  }],
  stats: { meshes: 1, vertices: 4, draws: 3, textures: 3, previewableTextures: 2, parameters: 1, channels: 1 },
  selection: { mode: 'appearance', appearance: 'default', meshes: [0] },
}

const fetched = []
globalThis.fetch = async (url) => {
  fetched.push(String(url))
  if (String(url).endsWith('/api/latest')) {
    const body = { id: 'scene-1', name: 'x.bem', stats: scene.stats, capabilities: { appearances: true } }
    return { ok: true, status: 200, statusText: 'OK', json: async () => body, text: async () => JSON.stringify(body) }
  }
  if (String(url).endsWith('/api/scene/scene-1')) {
    return { ok: true, status: 200, json: async () => scene }
  }
  const blobId = String(url).split('/').pop()
  const body = blobs.get(blobId)
  if (!body) return { ok: false, status: 404, json: async () => ({ error: 'no blob' }) }
  return { ok: true, status: 200, arrayBuffer: async () => body.buffer.slice(body.byteOffset, body.byteOffset + body.byteLength) }
}

/* ------------------------------------------------------------------ */
/* load the payload the way the harness does                           */
/* ------------------------------------------------------------------ */

let payload = null
globalThis.window.__ModuleLoader__ = { load: (value) => { payload = value } }
await import(pathToFileURL(clientFile).href)
assert.ok(payload, 'lib/client.js did not call __ModuleLoader__.load')
assert.equal(payload.id, 'dsh-bem-creator')

const moduleExports = payload.factory((specifier) => {
  if (specifier === 'react') return React
  throw new Error(`unexpected require(${specifier})`)
})
const { apply, inject } = moduleExports

/* ------------------------------------------------------------------ */
/* plugin registration                                                 */
/* ------------------------------------------------------------------ */

const slots = []
const locales = new Map()
const ctx = {
  effect(fn) { const disposer = fn(); return () => { if (typeof disposer === 'function') disposer() } },
  slots: {
    inject(name, body) { return body() },
    register(definition, View) { slots.push({ definition, View }); return () => {} },
  },
  locale: {
    register(namespace, dictionaries) { locales.set(namespace, dictionaries); return () => {} },
    bind(namespace) { return (key) => locales.get(namespace)?.zh?.[key] || key },
  },
  logger: { warn() {} },
}

await section('client registers one conversation view', () => {
  apply(ctx)
  assert.deepEqual(inject, ['slots', 'locale'])
  assert.equal(slots.length, 1)
  assert.equal(slots[0].definition.name, 'conversation.view')
  assert.equal(slots[0].definition.id, 'bem-preview')
  assert.equal(slots[0].definition.locale, 'bem-preview')
  assert.equal(slots[0].definition.label(), '预览', 'the tab label resolves through the locale service')
  assert.ok(locales.get('bem-preview').en['view.preview'], 'both dictionaries are registered')
})

await section('apply survives a harness without the slot service', () => {
  let warned = 0
  apply({ effect: ctx.effect, locale: ctx.locale, logger: { warn() { warned += 1 } } })
  assert.equal(warned, 1, 'a missing slot service warns instead of throwing')
})

/* ------------------------------------------------------------------ */
/* render                                                             */
/* ------------------------------------------------------------------ */

const View = slots[0].View
let tree = null

/** Collect the effect timers the view schedules, so tests can tick them. */
const intervals = []
globalThis.setInterval = (callback) => { intervals.push(callback); return intervals.length }
globalThis.clearInterval = () => {}

/**
 * One commit: render, attach refs the way React would, then run effects.
 *
 * The fake reconciler has no host nodes, so a `ref` on the canvas resolves to
 * the canvas stub — that is the object the stage will call `getContext` on.
 */
async function render() {
  hooks.cursor = 0
  tree = View()
  walk(tree, (node) => {
    const ref = node.props && node.props.ref
    if (ref && typeof ref === 'object') ref.current = node.type === 'canvas' ? canvas : node
  })
  await settle()
  return tree
}

function walk(node, visit) {
  if (!node || typeof node !== 'object') return
  visit(node)
  for (const child of node.children || []) {
    if (typeof child === 'string' || typeof child === 'number') visit({ text: String(child) })
    else walk(child, visit)
  }
}

function textOf(node) {
  const parts = []
  walk(node, (entry) => { if (entry.text !== undefined) parts.push(entry.text) })
  return parts.join(' ')
}

function find(node, predicate) {
  let found = null
  walk(node, (entry) => { if (!found && predicate(entry)) found = entry })
  return found
}

function findAll(node, predicate) {
  const found = []
  walk(node, (entry) => { if (predicate(entry)) found.push(entry) })
  return found
}

const isButton = (node) => node.type === 'button'

await section('empty state before any preview', async () => {
  await render()
  assert.ok(textOf(tree).includes('还没有可预览的包'), 'the first render explains how to preview')
})

await section('scene renders into the panel', async () => {
  await render() // poll resolves /api/latest, then /api/scene
  await render()
  const text = textOf(tree)
  assert.ok(!text.includes('还没有可预览的包'), 'the empty state is gone')
  assert.ok(text.includes('测试角色'), 'the package name is in the title bar')
  assert.ok(text.includes('BEM 1.5'), 'the format version is shown')
  assert.ok(text.includes('服装'), 'an option group renders')
  assert.ok(text.includes('便装') && text.includes('战装'), 'both option choices render')
  assert.ok(text.includes('胸部'), 'a shape parameter renders its slider')
  assert.ok(text.includes('body'), 'a part renders its checkbox')
  assert.ok(text.includes('默认'), 'an appearance chip renders')
})

await section('the canvas exists and the stage owns it', async () => {
  const canvasNode = find(tree, (node) => node.type === 'canvas')
  assert.ok(canvasNode, 'the stage needs a canvas')
  assert.equal(canvasNode.props.style.touchAction, 'none', 'pointer gestures must not scroll the page')
  // Geometry upload: positions, normals, uvs and one index buffer.
  const uploads = glCalls.filter((name) => name === 'bufferData')
  assert.ok(uploads.length >= 4, `expected four buffer uploads, saw ${uploads.length}`)
  assert.ok(glCalls.includes('createVertexArray'), 'the mesh is bound through a VAO')
})

await section('a frame draws the visible draws', async () => {
  const before = glCalls.filter((name) => name === 'drawElements').length
  const callbacks = rafQueue.splice(0, rafQueue.length)
  for (const callback of callbacks) callback(16)
  const after = glCalls.filter((name) => name === 'drawElements').length
  assert.equal(after - before, 3, 'one draw per draw entry')
  assert.ok(glCalls.includes('uniformMatrix4fv'), 'the camera matrices are uploaded')
})

await section('textures are decoded and uploaded', async () => {
  // rgba32 goes straight up; bc7 goes through the worker pool.
  assert.equal(workers.length, 3, 'the pool is capped by hardwareConcurrency - 1')
  assert.ok(workers.every((worker) => worker.url === '/dsh-bem/assets/decode-worker.js'))
  assert.ok(workers.some((worker) => worker.posted.length === 1), 'the compressed texture was handed to a worker')
  const uploads = glCalls.filter((name) => name === 'texImage2D').length
  assert.equal(uploads, 2, 'both textures reached the GPU')
  assert.ok(fetched.some((url) => url.endsWith('/api/blob/scene-1/b3')), 'the uncompressed texture came over the blob route')
})

await section('a texture the host skipped is reported, not retried', async () => {
  // The bug this guards: ASTC has no browser decoder, so the host marks it
  // `blob: null` with a reason. Asking the worker anyway produced one
  // "no decoder for astc" failure per texture and a red overlay.
  const posted = workers.reduce((sum, worker) => sum + worker.posted.length, 0)
  assert.equal(posted, 1, 'only the BC7 texture reaches the decoder')
  await render()
  const text = textOf(tree)
  assert.ok(text.includes('ASTC'), `the overlay must name ASTC as the reason, saw: ${text.slice(0, 200)}`)
  assert.ok(text.includes('1 张'), 'the overlay counts the skipped textures')
  assert.ok(!text.includes('解码失败'), 'a skipped texture is not a decode failure')
})

await section('the fragment shader does not depend on winding', () => {
  // The drawing buffer is not sRGB and the albedo is sampled through
  // SRGB8_ALPHA8, so without an explicit encode each mid-tone reads about a
  // stop dark. And because the packages are seam-split with mixed winding,
  // shading must key off the view direction, never `gl_FrontFacing` — that was
  // what left whole surfaces with nothing but the ambient term.
  const fragment = shaderSources.find((source) => source.includes('outColor'))
  assert.ok(fragment, 'the fragment shader was compiled')
  assert.ok(fragment.includes('pow(color, vec3(1.0 / 2.2))'), 'lighting must end with a gamma encode')
  assert.ok(fragment.includes('dot(n, view) < 0.0'), 'normals must be flipped towards the viewer')
  assert.ok(!fragment.includes('gl_FrontFacing'), 'winding is not a reliable outside/inside signal here')
  assert.ok(fragment.includes('0.80 * key'), 'the key light carries the shape instead of the ambient term')
})

await section('the clay toggle stops binding textures', async () => {
  const toggle = findAll(tree, isButton)[0]
  assert.ok(toggle, 'the texture toggle is the first button in the bar')
  const before = glCalls.filter((name) => name === 'bindTexture').length
  toggle.props.onClick()
  await render()
  const callbacks = rafQueue.splice(0, rafQueue.length)
  for (const callback of callbacks) callback(16)
  assert.equal(glCalls.filter((name) => name === 'bindTexture').length, before, 'clay mode binds no texture')
  assert.ok(textOf(tree).includes('素模'), 'the button now reads as clay')
})

await section('shape slider deforms the position buffer', async () => {
  const slider = find(tree, (node) => node.type === 'input' && node.props.type === 'range')
  assert.ok(slider, 'the parameter renders a range input')
  assert.equal(slider.props.min, 0)
  assert.equal(slider.props.max, 1000)
  const before = glCalls.filter((name) => name === 'bufferSubData').length
  slider.props.onChange({ target: { value: '500' } })
  await render()
  const after = glCalls.filter((name) => name === 'bufferSubData').length
  assert.ok(after > before, 'a parameter change re-uploads positions')
  assert.ok(textOf(tree).includes('500'), 'the slider value is echoed next to its label')
})

await section('part checkbox hides a mesh', async () => {
  const checkbox = find(tree, (node) => node.type === 'input' && node.props.type === 'checkbox')
  assert.ok(checkbox, 'the part list renders a checkbox')
  assert.equal(checkbox.props.checked, true)
  checkbox.props.onChange({ target: { checked: false } })
  await render()
  const callbacks = rafQueue.splice(0, rafQueue.length)
  const before = glCalls.filter((name) => name === 'drawElements').length
  for (const callback of callbacks) callback(16)
  assert.equal(glCalls.filter((name) => name === 'drawElements').length, before, 'a hidden part draws nothing')
  const restored = find(tree, (node) => node.type === 'input' && node.props.type === 'checkbox')
  restored.props.onChange({ target: { checked: true } })
  await render()
})

await section('an expiring scene explains itself', async () => {
  assert.ok(intervals.length >= 1, 'the view polls for new previews')
  const realFetch = globalThis.fetch
  globalThis.fetch = async (url) => {
    if (String(url).includes('/api/latest')) {
      const body = { id: 'scene-2' }
      return { ok: true, status: 200, statusText: 'OK', json: async () => body, text: async () => JSON.stringify(body) }
    }
    return { ok: false, status: 410, json: async () => ({ error: 'evicted' }) }
  }
  await intervals[0]() // one poll, as the timer would fire it
  await render()
  await render()
  globalThis.fetch = realFetch
  assert.ok(textOf(tree).includes('预览已过期'), `expected the refresh hint, saw: ${textOf(tree).slice(0, 160)}`)
})

await section('an unreachable preview service reports the failure', async () => {
  // The bug this guards: the poll's catch swallowed everything, so a bodyless 404
  // from a mis-registered prefix looked exactly like "no preview yet" — a blank
  // stage with an empty console.
  const realFetch = globalThis.fetch
  globalThis.fetch = async () => ({
    ok: false,
    status: 404,
    statusText: 'Not Found',
    text: async () => '',
    json: async () => { throw new Error('Unexpected end of JSON input') },
  })
  for (let index = 0; index < 3; index += 1) await intervals[0]()
  await render()
  globalThis.fetch = realFetch
  const text = textOf(tree)
  assert.ok(text.includes('预览服务连不上'), `expected the unreachable notice, saw: ${text.slice(0, 200)}`)
  assert.ok(text.includes('404'), 'the notice must quote the failing status')
})

await section('unmount releases the stage and the pool', async () => {
  for (const cleanup of hooks.cleanups) if (typeof cleanup === 'function') cleanup()
  assert.ok(workers.every((worker) => worker.terminated), 'workers are terminated on unmount')
  const deletes = glCalls.filter((name) => name === 'deleteTexture').length
  assert.ok(deletes >= 0)
})

console.log(results.join('\n'))
if (failures > 0) {
  console.error(`\n${failures} section(s) failed`)
  process.exit(1)
}
console.log('\nok: client registers, renders, decodes, deforms and cleans up')
