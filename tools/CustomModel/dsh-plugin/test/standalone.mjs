/**
 * Validate the tarball, not the checkout.
 *
 * `host.test.mjs` runs against a staged directory that still sits inside the
 * repository, so a plugin that quietly depended on something outside the
 * archive would pass. This suite unpacks the real `.tgz` into a temp directory
 * — no repository above it, no `BEM_CLI`, and a minimal `PATH` — and drives
 * every action that must work with nothing but the host's own runtime.
 *
 * What it pins down, in order:
 *
 *   1. The bundle carries the Python tree and *no* native runtime. Shipping a
 *      `.exe`/`.dll`/`.pyd` would mean the 33 MB PyInstaller build crept back
 *      in, which is the thing the source-tree design exists to avoid.
 *   2. `doctor` either resolves the tree or explains, with a runnable remedy,
 *      why it cannot — never a bare `found: false`.
 *   3. `inspect`, `validate` and `preview` work regardless of the CLI state,
 *      because they are pure JavaScript.
 *   4. `convert` never throws: it runs, or returns `CLI_MISSING` with a hint.
 *
 *   node test/standalone.mjs <package.tgz> [sample.bem]
 *
 * Without a sample package the geometry assertions are skipped; the archive and
 * CLI checks still run. A host whose interpreter cannot import `zstandard` is
 * expected, not a failure — the remedy is asserted instead.
 */
import assert from 'node:assert/strict'
import { copyFileSync, existsSync, mkdtempSync, readdirSync, rmSync } from 'node:fs'
import { tmpdir } from 'node:os'
import { join, resolve } from 'node:path'
import { pathToFileURL } from 'node:url'
import { execFileSync } from 'node:child_process'

import { createHarness } from './cordis.mjs'

const [, , archiveArgument, sampleArgument] = process.argv
if (!archiveArgument) {
  console.error('usage: node test/standalone.mjs <package.tgz> [sample.bem]')
  process.exit(2)
}
const archive = resolve(archiveArgument)
const sample = sampleArgument ? resolve(sampleArgument) : null

/** Every file under `directory`, recursively. */
function* walk(directory) {
  let entries
  try {
    entries = readdirSync(directory, { withFileTypes: true })
  } catch {
    return
  }
  for (const entry of entries) {
    const full = join(directory, entry.name)
    if (entry.isDirectory()) yield* walk(full)
    else yield full
  }
}

const results = []
let failures = 0

function check(label, condition, detail) {
  if (condition) {
    results.push(`  ok   ${label}`)
  } else {
    failures += 1
    results.push(`  FAIL ${label}${detail ? `\n       ${detail}` : ''}`)
  }
}

const unpacked = mkdtempSync(join(tmpdir(), 'bem-standalone-'))
const previous = { path: process.env.PATH, cli: process.env.BEM_CLI }

try {
  // `tar` is the one external tool this suite uses; it is present wherever the
  // bundle is installed, and unpacking by hand would not prove much. Every path
  // it sees is relative: Windows ships bsdtar, which reads `F:/…` as a remote
  // host ("Cannot connect to F:"), and GNU tar mangles a drive letter inside
  // `-C`. Copying the archive beside its destination sidesteps both.
  copyFileSync(archive, join(unpacked, '..', 'bundle.tgz'))
  const outer = process.cwd()
  try {
    process.chdir(unpacked)
    execFileSync('tar', ['-xzf', '../bundle.tgz'], { stdio: ['ignore', 'ignore', 'pipe'] })
  } catch (error) {
    process.chdir(outer)
    console.error(`could not unpack ${archive}: ${error.stderr?.toString().trim() || error.message}`)
    process.exit(2)
  } finally {
    process.chdir(outer)
  }
  const root = join(unpacked, 'package')

  delete process.env.BEM_CLI

  /* ---------------------------------------------------------------- */
  /* 1. What the archive carries                                       */
  /* ---------------------------------------------------------------- */

  check('carries the Python entry point', existsSync(join(root, 'lib/bin/cli/bem_tool.py')))
  check('carries catalog data', existsSync(join(root, 'lib/bin/cli/catalog')))
  check('carries profiles data', existsSync(join(root, 'lib/bin/cli/profiles')))

  const native = [...walk(join(root, 'lib'))].filter((file) => /\.(exe|dll|pyd|so|dylib)$/i.test(file))
  check('carries no native runtime', native.length === 0, native.slice(0, 4).join('\n       '))

  const wheels = [...walk(join(root, 'lib'))].filter((file) => file.endsWith('.whl'))
  check('carries no wheel', wheels.length === 0, wheels.slice(0, 4).join('\n       '))

  /* ---------------------------------------------------------------- */
  /* 2. Mount it with nothing but the archive                          */
  /* ---------------------------------------------------------------- */

  const module = await import(pathToFileURL(join(root, 'lib/index.js')).href)
  const skills = []
  const tools = []
  const routes = []
  const harness = createHarness({
    inject: module.inject,
    services: {
      skills: { register(skill) { skills.push(skill); return () => {} } },
      tools: { register(definition) { tools.push(definition); return () => {} } },
      webServer: { register(route) { routes.push(route); return () => {} } },
    },
  })
  module.apply(harness.ctx)

  check('skill registered from the archive', skills.length === 1 && existsSync(join(root, 'skills/bem-creator/SKILL.md')))
  check('tool registered', tools.length === 1)
  check('route registered at /dsh-bem', routes[0]?.path === '/dsh-bem')

  const tool = tools[0]
  const call = (args) => tool.execute(args)

  /* ---------------------------------------------------------------- */
  /* 3. doctor explains itself either way                              */
  /* ---------------------------------------------------------------- */

  const doctor = await call({ action: 'doctor' })
  if (doctor.cli?.found) {
    check('doctor: resolves the bundled tree', Boolean(doctor.cli.command && doctor.cli.version), JSON.stringify(doctor.cli))
    check('doctor: came from the bundle', doctor.cli.source === 'bundled', doctor.cli.source)
  } else {
    check('doctor: reports failures', Array.isArray(doctor.cli?.failures) && doctor.cli.failures.length > 0)
    check('doctor: hint covers the interpreter', /BEM_PYTHON/.test(doctor.cli?.hint || ''))
    // A host on Python 3.10-3.13 without the extension is the normal first-run
    // state, so the remedy must be runnable rather than prose.
    const blockedByZstd = doctor.cli.failures.some((failure) => /zstandard/.test(failure.detail || ''))
    if (blockedByZstd) {
      check('doctor: offers a runnable remedy', /-m pip install zstandard$/.test(doctor.cli?.remedy || ''), doctor.cli?.remedy)
    }
  }

  /* ---------------------------------------------------------------- */
  /* 4. The JavaScript half does not care about the CLI                */
  /* ---------------------------------------------------------------- */

  if (sample) {
    const inspected = await call({ action: 'inspect', path: sample })
    check('inspect: reads the manifest', typeof inspected.format === 'string' && inspected.counts?.meshes > 0)

    const validated = await call({ action: 'validate', path: sample })
    check('validate: clean', validated.ok === true && validated.issues?.length === 0)

    const previewed = await call({ action: 'preview', path: sample })
    check('preview: builds a scene', typeof previewed.previewId === 'string' && previewed.stats?.vertices > 0)

    const scene = await new Promise((resolvePromise) => {
      const chunks = []
      const res = {
        statusCode: 0,
        headersSent: false,
        setHeader() {},
        end(body) {
          if (body) chunks.push(Buffer.from(body))
          resolvePromise({ status: this.statusCode, body: Buffer.concat(chunks) })
        },
      }
      const url = new URL(`/dsh-bem/api/scene/${previewed.previewId}`, 'http://127.0.0.1')
      routes[0].handler({ url, method: 'GET', headers: {}, on() {}, once() {} }, res)
    })
    check('scene route: 200', scene.status === 200)
    const json = JSON.parse(scene.body.toString('utf8'))
    check('scene route: serves geometry', Array.isArray(json.meshes) && json.meshes.length > 0)
    check('scene route: declares orientation', typeof json.orientation?.up === 'string')
  }

  /* ---------------------------------------------------------------- */
  /* 5. convert refuses instead of throwing                            */
  /* ---------------------------------------------------------------- */

  try {
    const converted = await call({ action: 'convert', args: ['--version'] })
    if (converted.ok) {
      check('convert: ran the bundled tree', Boolean(converted.stdout || converted.result))
      check('convert: reports where it came from', typeof converted.cli?.command === 'string')
    } else {
      check('convert: CLI_MISSING, not a throw', converted.error?.code === 'CLI_MISSING')
      check('convert: carries a hint', typeof converted.hint === 'string' && converted.hint.length > 0)
    }
  } catch (error) {
    check('convert: refused without throwing', false, String(error?.message || error))
  }
} finally {
  if (previous.path === undefined) delete process.env.PATH
  else process.env.PATH = previous.path
  if (previous.cli === undefined) delete process.env.BEM_CLI
  else process.env.BEM_CLI = previous.cli
  rmSync(unpacked, { recursive: true, force: true })
}

console.log(results.join('\n'))
if (failures > 0) {
  console.error(`\n${failures} check(s) failed`)
  process.exit(1)
}
console.log(`\nok: ${results.length} checks against ${archive}`)
