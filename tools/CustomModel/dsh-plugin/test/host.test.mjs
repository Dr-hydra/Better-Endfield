/**
 * Host-half integration test: no harness, no browser, no display.
 *
 * The plugin is applied to a stub context that records what it registers, then
 * the route handler is driven with fake request/response objects. That covers
 * the whole path the browser half depends on — skill and tool registration,
 * scene building, scene JSON, binary blobs, decoder assets — without installing
 * dsh or opening a window.
 *
 * The plugin resolves `skills/bem-creator` relative to `lib/`, which only exists
 * after the packager stages it. Run from the repository, this file stages `lib/`
 * and the skill into a temp directory first so the real path resolution is
 * exercised instead of mocked.
 *
 *   node test/host.test.mjs
 *
 * Two real packages are needed for the geometry side. Their locations come from
 * BEM_TEST_PACKAGE_10 / BEM_TEST_PACKAGE_15, with the repository's own build
 * outputs as the default. A missing file skips that section with a notice rather
 * than failing, so the suite still guards the host surface on a fresh clone.
 */
import assert from 'node:assert/strict'
import { tmpdir } from 'node:os'
import { existsSync, renameSync, rmSync, mkdtempSync, mkdirSync, writeFileSync } from 'node:fs'
import { delimiter, join, resolve } from 'node:path'
import { pathToFileURL } from 'node:url'

import { pluginDirectory as plugin, stagePlugin } from './stage.mjs'
import { createHarness } from './cordis.mjs'

const repo = resolve(plugin, '../../..')

const results = []
let failures = 0

async function section(label, body) {
  const started = Date.now()
  try {
    await body()
    results.push(`  ok   ${label} (${Date.now() - started} ms)`)
  } catch (error) {
    failures += 1
    results.push(`  FAIL ${label}\n       ${String(error?.message || error).split('\n').join('\n       ')}`)
  }
}

function notice(label) {
  results.push(`  skip ${label}`)
}

const { root, temporary } = stagePlugin()

try {
  const module = await import(pathToFileURL(join(root, 'lib/index.js')).href)
  const { apply, inject, name } = module

  const skills = []
  const tools = []
  const routes = []
  const harness = createHarness({
    inject,
    services: {
      skills: { register(skill) { skills.push(skill); return () => {} } },
      tools: { register(definition) { tools.push(definition); return () => {} } },
      webServer: { register(route) { routes.push(route); return () => {} } },
    },
  })
  const ctx = harness.ctx

  apply(ctx)
  const tool = tools[0]

  /** Drive the registered prefix handler with a minimal response recorder. */
  /**
   * The prefix matcher of `dsh-host-webserver` 0.2.1-rc.3, transcribed from
   * `lib/index.js:495-504` (`match`), exact table included.
   *
   * This exists because calling `routes[0].handler` directly hid a whole class of
   * bug: the route was registered, alive, and unreachable, because the matcher
   * appends the separator itself — `startsWith(`${prefix}/`)` — so a path
   * registered with a trailing slash only matches a doubled one. Every request
   * went to the SPA fallback while `doctor.surface.routes` truthfully said `true`.
   */
  function matchRoute(pathname) {
    const exact = new Map()
    const hit = exact.get(pathname)
    if (hit !== undefined) return hit
    let best
    for (const route of routes) {
      const prefix = route.path
      if (pathname !== prefix && !pathname.startsWith(`${prefix}/`)) continue
      if (best === undefined || prefix.length > best.path.length) best = route
    }
    return best
  }

  function request(path, options = {}) {
    return new Promise((resolvePromise, rejectPromise) => {
      const headers = {}
      const chunks = []
      const res = {
        statusCode: 0,
        headersSent: false,
        setHeader(key, value) { headers[key.toLowerCase()] = value },
        end(body) {
          this.headersSent = true
          if (body) chunks.push(Buffer.from(body))
          resolvePromise({ status: this.statusCode, headers, body: Buffer.concat(chunks) })
        },
      }
      const route = matchRoute(new URL(path, 'http://127.0.0.1').pathname)
      if (!route) {
        // The webserver hands unmatched requests to the SPA fallback; from the
        // plugin's point of view that is an empty 404.
        res.statusCode = 404
        res.end()
        return
      }
      try {
        route.handler({ url: path, method: options.method || 'GET', headers: options.headers || {}, async *[Symbol.asyncIterator]() { if (options.body) yield Buffer.from(options.body) } }, res)
      } catch (error) {
        rejectPromise(error)
      }
    })
  }

  const sceneOf = async (id) => JSON.parse((await request(`/dsh-bem/api/scene/${id}`)).body.toString('utf8'))

  await section('a real context rejects undeclared service reads', () => {
    // Guards the fidelity of `test/cordis.mjs`. The bundle failed in a real
    // install precisely because a plain object stub returned `undefined` here;
    // if this assertion ever starts passing for the wrong reason, the suite has
    // stopped being able to catch that bug.
    assert.throws(() => ctx.webServer, /cannot get property "webServer" without inject/)
    assert.throws(() => ctx.tools, /cannot get property "tools" without inject/)
    assert.equal(ctx.get('webServer').register instanceof Function, true, 'get() stays non-strict')
  })

  await section('bundle identity and registration', () => {
    assert.equal(name, 'bem-creator')
    assert.ok(inject.includes('skills'), 'the bundle declares the skills dependency')
    assert.equal(skills.length, 1, 'the skill must register')
    assert.equal(tools.length, 1, 'the bem tool must register')
    assert.equal(routes.length, 1, 'one preview route must register')
    assert.equal(routes[0].kind, 'prefix')
    assert.equal(routes[0].path, '/dsh-bem', 'the prefix is registered without a trailing slash')
    assert.equal(skills[0].name, 'bem-creator')
    assert.equal(skills[0].source, 'bundled')
    assert.ok(skills[0].content.length > 500, 'the instruction body must arrive with the registration')
    assert.ok(existsSync(join(skills[0].resourceBase.path, 'references')), 'resourceBase must point at the packaged skill')
  })

  await section('the registered prefix is reachable through the real matcher', () => {
    // Guards the bug that survived a full restart: the route was registered,
    // alive, and invisible, because `/dsh-bem/` plus the matcher's own separator
    // asked for `/dsh-bem//…` while every client asked for `/dsh-bem/…`.
    for (const pathname of [
      '/dsh-bem',
      '/dsh-bem/',
      '/dsh-bem/api/latest',
      '/dsh-bem/api/scene/abc',
      '/dsh-bem/api/blob/abc/b1',
      '/dsh-bem/assets/bcn.js',
    ]) {
      assert.equal(matchRoute(pathname)?.path, '/dsh-bem', `${pathname} must reach the handler`)
    }
    assert.equal(matchRoute('/dsh-bemx/api'), undefined, 'a sibling prefix must not match')
    assert.equal(matchRoute('/api/latest'), undefined, 'the connection plugin keeps /api/*')
    assert.equal(matchRoute('/other'), undefined)
  })

  await section('optional surfaces attach when they arrive, never before', () => {
    // A headless-first profile starts this fiber before the web surface and the
    // tool host exist. Nothing may throw, the skill must land immediately, and
    // each surface must attach once when it shows up.
    const late = { skills: [], tools: [], routes: [] }
    const lateHarness = createHarness({
      inject,
      services: { skills: { register(skill) { late.skills.push(skill); return () => {} } } },
    })
    apply(lateHarness.ctx)
    assert.equal(late.skills.length, 1, 'the skill does not wait for an optional surface')
    assert.equal(late.tools.length, 0, 'nothing registers before its service exists')
    assert.equal(late.routes.length, 0)

    lateHarness.provide('webServer', { register(route) { late.routes.push(route); return () => {} } })
    lateHarness.provide('tools', { register(definition) { late.tools.push(definition); return () => {} } })
    assert.equal(late.routes.length, 1, 'exactly one route after the web surface arrives')
    assert.equal(late.tools.length, 1, 'exactly one tool after the tool host arrives')
    lateHarness.dispose()
  })

  await section('tool declaration', () => {
    assert.equal(tool.name, 'bem')
    assert.ok(tool.description.includes('preview'), 'the description must advertise preview')
    assert.deepEqual(tool.parameters.required, ['action'])
    assert.deepEqual(tool.parameters.properties.action.enum, ['inspect', 'validate', 'preview', 'resources', 'convert', 'doctor'])
    assert.equal(typeof tool.output.render, 'function', 'the output declaration needs a renderer')
    const rendered = tool.output.render({ action: 'doctor' }, { ok: true })
    assert.equal(rendered[0].type, 'text')
    assert.ok(rendered[0].text.includes('"ok"'))
  })

  await section('resource status exposes both user-selected formats without downloading', async () => {
    const status = await tool.execute({ action: 'resources' })
    assert.equal(status.ok, true)
    assert.deepEqual(status.choices.map((item) => item.platform), ['windows', 'android'])
    const response = await request('/dsh-bem/api/resources')
    assert.equal(response.status, 200)
    assert.equal(JSON.parse(response.body).choices.length, 2)
  })

  await section('resource settings route validates and applies a local pack', async () => {
    const temporary = mkdtempSync(join(tmpdir(), 'dsh-resource-route-'))
    try {
      const index = Buffer.from(JSON.stringify({ schemaVersion: 1, characters: [] }))
      writeFileSync(join(temporary, 'characters.json'), index)
      writeFileSync(join(temporary, 'manifest.json'), JSON.stringify({ schemaVersion: 1, kind: 'betterendfield-resource-pack', packageType: 'preview', platform: 'android', version: '1.6.4.2', gameVersion: '1.6.4', files: { 'characters.json': index.length } }))
      let response = await request('/dsh-bem/api/resources', { method: 'POST', body: '{}' })
      assert.equal(response.status, 400)
      response = await request('/dsh-bem/api/resources', { method: 'POST', body: JSON.stringify({ path: temporary, platform: 'android' }) })
      assert.equal(response.status, 200)
      assert.equal(JSON.parse(response.body).selectedPlatform, 'android')
      response = await request('/dsh-bem/api/resources', { method: 'POST', headers: { origin: 'https://untrusted.invalid', host: '127.0.0.1' }, body: '{}' })
      assert.equal(response.status, 403)
      response = await request('/dsh-bem/api/resources', { method: 'DELETE' })
      assert.equal(response.status, 405)
    } finally { rmSync(temporary, { recursive: true, force: true }) }
  })

  await section('doctor', async () => {
    const doctor = await tool.execute({ action: 'doctor' })
    assert.equal(doctor.ok, true)
    assert.equal(doctor.routes, '/dsh-bem/')
    assert.equal(doctor.surface.routes, true, 'doctor must admit whether the routes are actually mounted')
    assert.equal(doctor.surface.tool, true)
    assert.equal(doctor.preview.ready, true)
    assert.ok(doctor.preview.decoders.includes('bc7'))
    assert.equal(typeof doctor.cli.found, 'boolean', 'doctor must always report a CLI verdict')
  })

  await section('unknown action is rejected', async () => {
    await assert.rejects(() => tool.execute({ action: 'wat' }), /unknown action/)
    await assert.rejects(() => tool.execute({ action: 'inspect' }), /path is required/)
  })

  /* ---------------------------------------------------------------- */
  /* asset routes need no package                                    */
  /* ---------------------------------------------------------------- */

  await section('decoder and worker assets', async () => {
    const decoder = await request('/dsh-bem/assets/bcn.js')
    assert.equal(decoder.status, 200)
    assert.ok(decoder.headers['content-type'].startsWith('text/javascript'))
    const source = decoder.body.toString('utf8')
    for (const symbol of ['decodeBC1', 'decodeBC3', 'decodeBC4', 'decodeBC5', 'decodeBC7']) {
      assert.ok(source.includes(symbol), `decoder asset must expose ${symbol}`)
    }
    const worker = await request('/dsh-bem/assets/decode-worker.js')
    assert.equal(worker.status, 200)
    assert.ok(worker.body.toString('utf8').includes('decodeBC7'))
  })

  await section('route guards', async () => {
    const missing = await request('/dsh-bem/api/scene/nope')
    assert.equal(missing.status, 410, 'an evicted scene must say so rather than 404')
    const bogus = await request('/dsh-bem/api/not-a-route')
    assert.equal(bogus.status, 404)
    const owned = await request('/dsh-bem/')
    assert.equal(owned.status, 404, 'the prefix root is not a route')
  })

  /* ---------------------------------------------------------------- */
  /* 1.0 package: shared index payload, draws as start/count slices   */
  /* ---------------------------------------------------------------- */

  const package10 = process.env.BEM_TEST_PACKAGE_10
    || join(repo, 'artifacts/native-bem-tohsaka-aglina-20261008/dist/TohsakaRin-aglina-by-Dr-hydra.bem')

  if (!existsSync(package10)) {
    notice(`1.0 package (${package10} missing)`)
  } else {
    let previewId = null

    await section('1.0 inspect', async () => {
      const inspected = await tool.execute({ action: 'inspect', path: package10 })
      assert.equal(inspected.ok, true)
      assert.equal(inspected.format, 'BEMv1.0')
      assert.equal(inspected.counts.meshes, 4)
      assert.equal(inspected.counts.textures, 15)
      assert.equal(inspected.previewable, true)
    })

    await section('1.0 preview builds a scene', async () => {
      const previewed = await tool.execute({ action: 'preview', path: package10 })
      assert.equal(previewed.ok, true)
      assert.ok(previewed.previewId, 'preview must return a scene id')
      assert.equal(previewed.stats.meshes, 4)
      assert.equal(previewed.stats.draws, 16, 'every 1.0 draw slice must survive')
      assert.deepEqual(previewed.selection.meshes, [0, 1, 2], 'the default appearance selects three meshes')
      previewId = previewed.previewId
    })

    await section('1.0 validate is clean', async () => {
      const validated = await tool.execute({ action: 'validate', path: package10 })
      assert.equal(validated.ok, true, `validate must be clean: ${JSON.stringify(validated.issues)}`)
    })

    if (previewId) {
      await section('1.0 latest pointer', async () => {
        const latest = await request('/dsh-bem/api/latest')
        assert.equal(latest.status, 200)
        const json = JSON.parse(latest.body.toString('utf8'))
        assert.equal(json.id, previewId)
        assert.equal(json.stats.meshes, 4)
        assert.equal(json.capabilities.appearances, true)
        assert.equal(json.capabilities.textures, true)
      })

      await section('1.0 scene JSON', async () => {
        const scene = await sceneOf(previewId)
        assert.equal(scene.schema, 'bem-preview/1')
        assert.equal(scene.meshes.length, 4)
        assert.equal(scene.textures.length, 15)
        assert.ok(scene.meshes[0].positionsBlob, 'meshes need a position blob')
        assert.equal(scene.meshes[0].draws.length, 6)
        assert.ok(scene.meshes[0].draws[1].first > 0, 'draw slices must keep their start offset')
        const shared = new Set(scene.meshes[0].draws.map((draw) => draw.indicesBlob))
        assert.equal(shared.size, 1, '1.0 draws share one index blob')
        assert.equal(scene.selection.meshes.length, 3)
      })

      await section('1.0 position blob', async () => {
        const scene = await sceneOf(previewId)
        const first = scene.meshes[0]
        const positions = await request(`/dsh-bem/api/blob/${previewId}/${first.positionsBlob}`)
        assert.equal(positions.status, 200)
        assert.equal(positions.headers['content-type'], 'application/octet-stream')
        assert.equal(positions.body.length, first.vertexCount * 3 * 4, 'position blob is float32 xyz')
        // Every vertex must be finite and the mesh must have a non-degenerate extent.
        let min = Infinity
        let max = -Infinity
        for (let i = 0; i < positions.body.length; i += 4) {
          const value = positions.body.readFloatLE(i)
          assert.ok(Number.isFinite(value), `position ${i / 4} is not finite`)
          if (value < min) min = value
          if (value > max) max = value
        }
        assert.ok(max - min > 0.01, 'the mesh has a non-degenerate extent')
      })

      await section('1.0 index blob', async () => {
        const scene = await sceneOf(previewId)
        const mesh = scene.meshes[0]
        const blob = await request(`/dsh-bem/api/blob/${previewId}/${mesh.draws[0].indicesBlob}`)
        assert.equal(blob.status, 200)
        const indicesInBlob = blob.body.length / mesh.indexSize
        assert.ok(indicesInBlob >= mesh.draws[0].count, 'the index blob covers the first draw')
        for (let i = 0; i < indicesInBlob; i += 1) {
          const at = i * mesh.indexSize
          const value = mesh.indexSize === 4 ? blob.body.readUInt32LE(at) : blob.body.readUInt16LE(at)
          assert.ok(value < mesh.vertexCount, `index ${value} out of range for ${mesh.vertexCount} vertices`)
        }
      })

      await section('1.0 texture blob is the base level', async () => {
        const scene = await sceneOf(previewId)
        const texture = scene.textures.find((entry) => entry.blob)
        assert.ok(texture, 'at least one texture must be servable')
        const blob = await request(`/dsh-bem/api/blob/${previewId}/${texture.blob}`)
        assert.equal(blob.status, 200)
        // Family -> bytes per block; rgba32 is the only uncompressed one.
        const bytesPerBlock = { rgba32: 4, bc1: 8, bc4: 8, bc3: 16, bc5: 16, bc7: 16, astc: 16 }[texture.family]
        assert.ok(bytesPerBlock, `unexpected texture family ${texture.family}`)
        const block = texture.block || 1
        const expected = Math.ceil(texture.width / block) * Math.ceil(texture.height / block) * bytesPerBlock
        assert.equal(blob.body.length, expected, `${texture.name}: base mip block size`)
        assert.ok(texture.mips >= 1, 'the mip count is described for the client')
        const astc = scene.textures.filter((entry) => entry.family === 'astc')
        assert.ok(astc.every((entry) => entry.blob === null && entry.skipped), 'ASTC must be reported as skipped, not served wrong')
      })

      await section('1.0 geometry is served Y-up', async () => {
        // 1.0 packages are authored Z-up — a 1.67 m character measures 1.668
        // along Z and only 0.58 along Y — so a Y-up camera laid it on its back.
        const scene = await sceneOf(previewId)
        assert.equal(scene.orientation.up, 'Y')
        assert.equal(scene.orientation.sourceUp, 'Z')
        assert.equal(scene.orientation.rotated, true)
        const mesh = scene.meshes[0]
        const positions = await request(`/dsh-bem/api/blob/${previewId}/${mesh.positionsBlob}`)
        const min = [Infinity, Infinity, Infinity]
        const max = [-Infinity, -Infinity, -Infinity]
        for (let i = 0; i < positions.body.length; i += 4) {
          const axis = (i / 4) % 3
          const value = positions.body.readFloatLE(i)
          if (value < min[axis]) min[axis] = value
          if (value > max[axis]) max[axis] = value
        }
        const size = [max[0] - min[0], max[1] - min[1], max[2] - min[2]]
        const shown = size.map((value) => value.toFixed(2)).join(' x ')
        assert.ok(size[1] > size[2] * 1.5, `height must run along Y, got ${shown}`)
        assert.ok(size[1] > size[0], `a character is taller than it is wide, got ${shown}`)
      })

      await section('1.0 unknown blob is 404', async () => {
        const blob = await request(`/dsh-bem/api/blob/${previewId}/nope`)
        assert.equal(blob.status, 404)
      })
    }
  }

  /* ---------------------------------------------------------------- */
  /* ASTC package: geometry yes, textures no — and said once           */
  /* ---------------------------------------------------------------- */

  const astcPackage = process.env.BEM_TEST_PACKAGE_ASTC
    || join(repo, 'artifacts/native-bem-tohsaka-aglina-20261008/dist/TohsakaRin-aglina-native-test-0.3.2-android-astc.bem')

  if (!existsSync(astcPackage)) {
    notice(`ASTC package (${astcPackage} missing)`)
  } else {
    await section('ASTC package serves base blocks for browser GPU upload', async () => {
      const previewed = await tool.execute({ action: 'preview', path: astcPackage })
      assert.equal(previewed.ok, true)
      const scene = await sceneOf(previewed.previewId)
      const astc = scene.textures.filter((texture) => texture.family === 'astc')
      assert.ok(astc.length >= 6)
      for (const texture of astc) {
        assert.ok(texture.blob, 'ASTC blocks are served for GPU-capable browsers')
        assert.equal(texture.skipped, null)
        const response = await request('/dsh-bem/api/blob/' + previewed.previewId + '/' + texture.blob)
        assert.equal(response.status, 200)
        assert.equal(response.body.length, Math.ceil(texture.width / texture.block) * Math.ceil(texture.height / texture.block) * 16)
      }
      assert.equal(scene.stats.previewableTextures, scene.textures.filter((texture) => texture.blob).length)
      assert.ok(scene.meshes.every((mesh) => mesh.positionsBlob))
    })
  }

  /* ---------------------------------------------------------------- */
  /* 1.5 package: option groups, component rules, shape channels       */
  /* ---------------------------------------------------------------- */

  const package15 = process.env.BEM_TEST_PACKAGE_15 || join(repo, 'temp/v3-check.bem')

  if (!existsSync(package15)) {
    notice(`1.5 package (${package15} missing)`)
  } else {
    let modernId = null

    await section('1.5 preview builds a scene', async () => {
      const modern = await tool.execute({ action: 'preview', path: package15 })
      assert.equal(modern.ok, true)
      assert.equal(modern.selection.mode, 'rules')
      assert.ok(modern.selection.meshes.length >= 5, 'default options must select the base outfit')
      modernId = modern.previewId
    })

    if (modernId) {
      await section('1.5 scene surface', async () => {
        const scene = await sceneOf(modernId)
        assert.equal(scene.optionGroups.length, 17)
        // Every real package measured so far is Z-up, including this 1.5 target:
        // 1.00 wide, 0.58 deep, 1.67 tall.
        assert.equal(scene.orientation.up, 'Y')
        assert.equal(scene.orientation.sourceUp, 'Z')
        assert.equal(scene.orientation.rotated, true)
        assert.equal(scene.componentRules.length, 16)
        assert.equal(scene.parameters.length, 4)
        assert.equal(scene.channels.length, 4)
        for (const group of scene.optionGroups) {
          assert.ok(group.choices.length > 1, `${group.id} offers a choice`)
          assert.ok(group.choices.some((choice) => choice.id === group.default), `${group.id} default is one of its choices`)
        }
        const conditional = scene.meshes.flatMap((mesh) => mesh.draws).filter((draw) => draw.when).length
        assert.ok(conditional > 0, '1.5 draws carry visibility conditions')
        for (const channel of scene.channels) {
          // A channel is neutral plus one or more keyed frames on the 0..1000
          // scale; a single key at 1000 is the common "grow from rest" morph.
          const animated = channel.frames.filter((frame) => !frame.neutral)
          assert.ok(animated.length >= 1, `${channel.parameter} needs a keyed frame to interpolate towards`)
          assert.ok(scene.parameters.some((parameter) => parameter.id === channel.parameter))
          const values = channel.frames.map((frame) => frame.value)
          assert.deepEqual(values, [...values].sort((a, b) => a - b), `${channel.parameter} frames are ordered by value`)
          const rest = channel.frames.findIndex((frame) => frame.neutral)
          if (rest !== -1) assert.equal(rest, 0, `${channel.parameter} lists its rest pose first`)
          for (const frame of animated) {
            assert.ok(frame.indicesBlob && frame.deltasBlob, 'a keyed frame ships indices and deltas')
            assert.ok(frame.count > 0, 'a keyed frame moves at least one vertex')
          }
        }
      })

      await section('1.5 deformation frame payloads', async () => {
        const scene = await sceneOf(modernId)
        const channel = scene.channels.find((entry) => entry.frames.some((frame) => !frame.neutral))
        assert.ok(channel, 'at least one channel is keyed')
        const frame = channel.frames.find((entry) => !entry.neutral)
        const indices = await request(`/dsh-bem/api/blob/${modernId}/${frame.indicesBlob}`)
        const deltas = await request(`/dsh-bem/api/blob/${modernId}/${frame.deltasBlob}`)
        assert.equal(indices.status, 200)
        assert.equal(deltas.status, 200)
        assert.equal(indices.body.length, frame.count * 4, 'one uint32 vertex id per moved vertex')
        assert.equal(deltas.body.length, frame.count * 3 * 4, 'one float32 xyz delta per moved vertex')
        const mesh = scene.meshes[channel.mesh]
        for (let i = 0; i < frame.count; i += 1) {
          assert.ok(indices.body.readUInt32LE(i * 4) < mesh.vertexCount, 'deformation targets a real vertex')
        }
      })

      await section('1.5 validate is clean', async () => {
        const modernValidated = await tool.execute({ action: 'validate', path: package15 })
        assert.equal(modernValidated.ok, true, `validate must be clean: ${JSON.stringify(modernValidated.issues)}`)
      })
    }
  }

  /* ---------------------------------------------------------------- */
  /* CLI bridge                                                        */
  /* ---------------------------------------------------------------- */

  /*
   * CLI discovery.
   *
   * The bundle ships no converter, so `convert` lives or dies on finding one
   * that the *user* installed. The probe used to look at exactly one path
   * (`lib/bin/<platform>`), which meant a CLI installed the documented way —
   * `/data/local/tmp/bem-cli/bem-tools` on Android, an unzipped folder on
   * Windows — was invisible unless BEM_CLI was set by hand. These cases pin the
   * chain down: explicit override, a directory holding the entry point, and a
   * clean "nothing found" that still explains itself.
   */
  await section('CLI discovery: BEM_CLI directory and script forms', async () => {
    const { spawnSync } = await import('node:child_process')
    const { writeFileSync, mkdirSync, mkdtempSync, rmSync: drop } = await import('node:fs')
    const { tmpdir } = await import('node:os')

    const python = [process.env.BEM_PYTHON, 'python3', 'python']
      .filter(Boolean)
      .find((candidate) => {
        try { return spawnSync(candidate, ['-c', 'print(1)'], { stdio: 'ignore' }).status === 0 } catch { return false }
      })
    if (!python) {
      notice('CLI discovery (no python interpreter to stand in for the CLI)')
      return
    }

    const sandbox = mkdtempSync(join(tmpdir(), 'bem-cli-'))
    const script = join(sandbox, 'bem_tool.py')
    writeFileSync(script, 'print("bem-tools 9.9.9-test")\n', 'utf8')

    const before = { path: process.env.PATH, cli: process.env.BEM_CLI, python: process.env.BEM_PYTHON }
    try {
      process.env.BEM_PYTHON = python

      // 1. A directory: the probe has to find bem_tool.py inside it and put the
      //    interpreter in front of it.
      process.env.BEM_CLI = sandbox
      const byDirectory = await tool.execute({ action: 'doctor' })
      assert.equal(byDirectory.cli.found, true, `a directory holding bem_tool.py must be accepted: ${byDirectory.cli.failures}`)
      assert.equal(byDirectory.cli.source, 'BEM_CLI')
      assert.equal(byDirectory.cli.command, python)
      assert.deepEqual(byDirectory.cli.args, [script])
      assert.match(String(byDirectory.cli.version), /9\.9\.9-test/)

      // 2. The script itself, not its directory.
      process.env.BEM_CLI = script
      const byScript = await tool.execute({ action: 'doctor' })
      assert.equal(byScript.cli.found, true, 'a .py path must be accepted directly')
      assert.deepEqual(byScript.cli.args, [script])

      // Cases 3 and 4 both need the bundle's own tree out of the way: it sits
      // ahead of PATH in the probe order, so leaving it in place would make the
      // PATH branch unreachable and the "nothing found" case unreachable too.
      delete process.env.BEM_CLI
      const bundled = join(root, 'lib/bin/cli')
      const hidden = `${bundled}.hidden`
      const restoring = existsSync(bundled)
      if (restoring) renameSync(bundled, hidden)
      try {
        // 3. On PATH, with no override at all — how an agent-installed CLI is
        //    expected to be picked up. No BEM_PYTHON either, so the interpreter
        //    has to come from the enumeration rather than from a shortcut.
        delete process.env.BEM_PYTHON
        process.env.PATH = [sandbox, before.path].filter(Boolean).join(delimiter)
        const byPath = await tool.execute({ action: 'doctor' })
        assert.equal(byPath.cli.found, true, `a CLI on PATH must be found without BEM_CLI: ${byPath.cli.failures}`)
        assert.equal(byPath.cli.source, 'PATH')
        assert.deepEqual(byPath.cli.args, [script])

        // 4. Two interpreters share a name on PATH and the first one cannot run.
        //    A bundled 3.13 without `zstandard` sitting ahead of a store 3.13
        //    that has it is exactly this shape in the wild. Resolving the bare
        //    name once would pick the broken one and call the tree unusable.
        const shadow = join(sandbox, 'shadow')
        mkdirSync(shadow, { recursive: true })
        const decoy = join(shadow, process.platform === 'win32' ? 'python3.14.exe' : 'python3.14')
        writeFileSync(decoy, '')  // empty: not executable on any platform
        process.env.PATH = [shadow, sandbox, ...(before.path || '').split(delimiter).filter(Boolean)].join(delimiter)
        const skipped = await tool.execute({ action: 'doctor' })
        assert.equal(skipped.cli.found, true, `a broken interpreter must not shadow a working one: ${JSON.stringify(skipped.cli.failures)}`)
        assert.notEqual(skipped.cli.command, decoy, 'the broken interpreter must lose')
        assert.ok(
          (skipped.cli.rejected || []).some((entry) => entry.command === decoy),
          'doctor must report which candidate it rejected',
        )

        // 5. Nothing anywhere: PATH emptied so the answer cannot depend on the
        //    machine the suite happens to run on.
        process.env.PATH = ''
        const missing = await tool.execute({ action: 'doctor' })
        assert.equal(missing.cli.found, false)
        assert.ok(Array.isArray(missing.cli.tried) && missing.cli.tried.includes('PATH'), 'doctor must say where it looked')
        assert.match(missing.cli.hint, /BEM_CLI/)

        // convert has to refuse with the same explanation instead of throwing.
        const refused = await tool.execute({ action: 'convert', args: ['--version'] })
        assert.equal(refused.ok, false)
        assert.equal(refused.error.code, 'CLI_MISSING')
        assert.ok(refused.searched.includes('PATH'))
      } finally {
        if (restoring) renameSync(hidden, bundled)
      }
    } finally {
      if (before.path === undefined) delete process.env.PATH
      else process.env.PATH = before.path
      if (before.cli === undefined) delete process.env.BEM_CLI
      else process.env.BEM_CLI = before.cli
      if (before.python === undefined) delete process.env.BEM_PYTHON
      else process.env.BEM_PYTHON = before.python
      drop(sandbox, { recursive: true, force: true })
    }
  })

  await section('tool results are lossless JSON', async () => {
    // The host validates every tool result as lossless JSON, where `undefined`
    // has no representation — not as null, not as a dropped key. A single
    // optional field written as `stdout: undefined` fails the call *after* the
    // handler has already run, which looks like the tool itself is broken and
    // leaves nothing in the plugin to point at. Every action is checked because
    // the leak is always in the branch that was not exercised.
    const leak = (value, path = '$') => {
      if (value === undefined) return path
      if (Array.isArray(value)) {
        for (let index = 0; index < value.length; index += 1) {
          const hit = leak(value[index], `${path}[${index}]`)
          if (hit) return hit
        }
        return null
      }
      if (value && typeof value === 'object' && !ArrayBuffer.isView(value)) {
        for (const [key, entry] of Object.entries(value)) {
          const hit = leak(entry, `${path}.${key}`)
          if (hit) return hit
        }
      }
      return null
    }

    const package10 = process.env.BEM_TEST_PACKAGE_10
      || join(repo, 'artifacts/native-bem-tohsaka-aglina-20261008/dist/TohsakaRin-aglina-by-Dr-hydra.bem')
    const calls = [
      { action: 'doctor' },
      { action: 'convert', args: ['--version'] },
      { action: 'convert' },
      { action: 'inspect', path: join(repo, 'no-such-package.bem') },
    ]
    if (existsSync(package10)) {
      calls.push(
        { action: 'inspect', path: package10 },
        { action: 'validate', path: package10 },
        { action: 'preview', path: package10 },
      )
    }
    for (const call of calls) {
      const result = await tool.execute(call)
      const hit = leak(result)
      assert.equal(hit, null, `${call.action}${call.path ? ` ${call.path}` : ''} leaks undefined at ${hit}`)
    }
  })

  await section('convert without a CLI explains itself', async () => {
    const result = await tool.execute({ action: 'convert', args: ['--version'] })
    if (result.ok) {
      // A CLI is installed beside the bundle; then the call must have produced output.
      assert.equal(result.exitCode, 0)
      assert.ok(result.result || result.stdout, 'a working CLI must produce output')
    } else {
      assert.equal(result.error.code, 'CLI_MISSING')
      assert.ok(result.hint.includes('BEM_CLI'), 'the refusal must name the override')
    }
  })
} finally {
  if (temporary) rmSync(temporary, { recursive: true, force: true })
}

console.log(results.join('\n'))
if (failures > 0) {
  console.error(`\n${failures} section(s) failed`)
  process.exit(1)
}
console.log('\nok: tool, routes, scene JSON, blobs, 1.0 and 1.5 surfaces')
