/**
 * DeepSeek Harness bundle for the bem-creator skill and its preview surface.
 *
 * Three things live here:
 *
 *  1. The skill. The instruction body, its references and its scripts ship
 *     inside this package under `skills/bem-creator/`. Harness filesystem
 *     discovery scans a fixed set of roots one level deep and cannot see inside
 *     an installed package, so the skill is contributed to the registry as a
 *     runtime skill instead. `resourceBase` points at the packaged directory,
 *     which is what makes the skill's own relative references resolve.
 *
 *  2. The `bem` tool. Reads a package, validates it, and registers a preview
 *     scene the browser half can render. Geometry and texture blocks are served
 *     as blobs so a 10 MB package does not travel as base64 through JSON.
 *
 *  3. The routes under `/dsh-bem/` that serve those blobs plus the decoder the
 *     browser half runs. `/api/*` belongs to the connection plugin, so this
 *     bundle uses its own prefix and never competes for that table.
 *
 * No `@deepseek-ai/*` package is imported. `ctx.skills`, `ctx.tools` and
 * `ctx.webServer` are the only surfaces this bundle touches, each behind a
 * presence check, so a harness without the web surface (a headless profile)
 * still loads the skill and the tool.
 */
import { readFileSync, existsSync, statSync, lstatSync, openSync, readSync, closeSync } from 'node:fs'
import { fileURLToPath } from 'node:url'
import { spawn } from 'node:child_process'
import { resolve, join, delimiter } from 'node:path'

import { inspect, validate, preview, getScene, latestSceneId, initialSelection } from './bem/preview.js'

export const name = 'bem-creator'
export const inject = ['skills']

const skillDirectory = fileURLToPath(new URL('../skills/bem-creator/', import.meta.url))
const instructionFile = fileURLToPath(new URL('../skills/bem-creator/SKILL.md', import.meta.url))
const decoderFile = fileURLToPath(new URL('./vendor/bcn.js', import.meta.url))
const workerFile = fileURLToPath(new URL('./assets/decode-worker.js', import.meta.url))

const ROUTE_PREFIX = '/dsh-bem/'
/**
 * The prefix registered with the webserver, without the trailing slash.
 *
 * `dsh-host-webserver` matches a prefix route with
 *
 *   pathname !== prefix && !pathname.startsWith(`${prefix}/`)
 *
 * so it appends the separator itself. Registering `/dsh-bem/` therefore asks for
 * `/dsh-bem//…` and a normal `/dsh-bem/api/latest` never reaches the handler —
 * the route is active (its disposer is held, `doctor.surface.routes` is true) and
 * still invisible, which is the worst possible failure shape. Registering the
 * bare base matches `/dsh-bem`, `/dsh-bem/` and everything below it.
 */
const ROUTE_BASE = '/dsh-bem'

/**
 * What actually got mounted on this host.
 *
 * `apply` has no return channel, and both optional surfaces live on child fibers,
 * so mount state is recorded here and echoed by `doctor`. Without it a bundle
 * that registered only its skill is indistinguishable from one that mounted
 * everything — which is exactly the failure that costs an hour of debugging.
 */
const surface = { routes: false, tool: false, notes: [] }

/**
 * Split a leading `---` block into flat `key: value` fields.
 *
 * The skill frontmatter is deliberately flat: only `name`, `description` and
 * an optional `whenToUse` are read, all single-line.
 */
function splitFrontmatter(text, source) {
  const match = /^---\r?\n([\s\S]*?)\r?\n---[ \t]*(?:\r?\n|$)/.exec(text)
  if (!match) throw new Error(`${source}: SKILL.md has no frontmatter block`)
  const fields = new Map()
  for (const line of match[1].split(/\r?\n/)) {
    const pair = /^([A-Za-z0-9_-]+)[ \t]*:[ \t]*(.*)$/.exec(line)
    if (!pair) continue
    const quoted = /^(['"])([\s\S]*)\1$/.exec(pair[2].trim())
    fields.set(pair[1], quoted ? quoted[2] : pair[2].trim())
  }
  return { fields, body: text.slice(match[0].length) }
}

function registerSkill(ctx) {
  const { fields, body } = splitFrontmatter(readFileSync(instructionFile, 'utf8'), instructionFile)
  const skillName = fields.get('name')
  const description = fields.get('description')
  if (!skillName || !description) {
    throw new Error(`${instructionFile}: frontmatter needs both name and description`)
  }
  if (skillName !== name) {
    throw new Error(`${instructionFile}: frontmatter name "${skillName}" does not match the bundle name "${name}"`)
  }
  const skill = {
    name: skillName,
    description,
    path: instructionFile,
    content: body,
    source: 'bundled',
    provider: name,
    resourceBase: { kind: 'directory', path: skillDirectory },
    invocation: { modelInvocable: true, userInvocable: true },
  }
  const whenToUse = fields.get('whenToUse')
  if (whenToUse) skill.whenToUse = whenToUse
  // `register` returns the registry's disposer; `ctx.effect` ties it to this
  // plugin's fiber so unloading the bundle removes the skill.
  ctx.effect(() => ctx.skills.register(skill))
}

/* ------------------------------------------------------------------ */
/* routes                                                              */
/* ------------------------------------------------------------------ */

function send(res, status, body, contentType) {
  res.statusCode = status
  res.setHeader('Content-Type', contentType)
  res.setHeader('Cache-Control', 'no-store')
  if (body === null) {
    res.end()
    return
  }
  res.setHeader('Content-Length', String(body.length))
  res.end(body)
}

function sendJson(res, status, value) {
  send(res, status, Buffer.from(JSON.stringify(value), 'utf8'), 'application/json; charset=utf-8')
}

/**
 * Serve one path under the plugin prefix.
 *
 * `/api/blob/<scene>/<blob>` is the only route that can be large, and it is the
 * only one allowed to fail with 410: scenes are evicted after a few previews,
 * so a stale tab gets a clear "run preview again" instead of a blank stage.
 */
function handleRequest(req, res, url) {
  const path = url.pathname
  if (path !== ROUTE_BASE && !path.startsWith(ROUTE_PREFIX)) {
    sendJson(res, 404, { error: 'not a bem-preview route' })
    return
  }
  // Tolerate the base alone, a trailing slash, and the doubled slash that the
  // old registration produced — a stale client may still ask for any of them.
  const rest = path.slice(ROUTE_BASE.length).replace(/^\/+/, '')

  if (rest === 'api/latest') {
    const id = latestSceneId()
    const entry = id ? getScene(id) : null
    if (!entry) {
      sendJson(res, 200, { id: null })
      return
    }
    sendJson(res, 200, {
      id,
      name: entry.name,
      path: entry.path,
      builtAt: entry.builtAt,
      package: entry.scene.package,
      stats: entry.scene.stats,
      capabilities: {
        appearances: entry.scene.appearances.length > 0,
        options: entry.scene.optionGroups.length > 0,
        parameters: entry.scene.parameters.length > 0,
        textures: entry.scene.textures.some((texture) => texture.blob),
      },
    })
    return
  }

  if (rest.startsWith('api/scene/')) {
    const entry = getScene(rest.slice('api/scene/'.length))
    if (!entry) {
      sendJson(res, 410, { error: 'scene evicted; run bem preview again' })
      return
    }
    sendJson(res, 200, { ...entry.scene, selection: initialSelection(entry.scene) })
    return
  }

  if (rest.startsWith('api/blob/')) {
    const [sceneId, blobId] = rest.slice('api/blob/'.length).split('/')
    const entry = getScene(sceneId)
    if (!entry) {
      sendJson(res, 410, { error: 'scene evicted; run bem preview again' })
      return
    }
    const blob = entry.blobs.get(blobId)
    if (!blob) {
      sendJson(res, 404, { error: `unknown blob ${blobId}` })
      return
    }
    let data = null
    try {
      data = typeof blob.get === 'function' ? blob.get() : blob.data
    } catch (error) {
      sendJson(res, 500, { error: `blob ${blobId} failed: ${error.message}` })
      return
    }
    if (!data) {
      sendJson(res, 404, { error: `blob ${blobId} has no data` })
      return
    }
    send(res, 200, data, 'application/octet-stream')
    return
  }

  if (rest === 'assets/bcn.js') {
    send(res, 200, readFileSync(decoderFile), 'text/javascript; charset=utf-8')
    return
  }
  if (rest === 'assets/decode-worker.js') {
    send(res, 200, readFileSync(workerFile), 'text/javascript; charset=utf-8')
    return
  }

  sendJson(res, 404, { error: `unknown bem-preview route ${rest}` })
}

/**
 * Read a service without declaring it as a hard dependency.
 *
 * `ctx[name]` is not usable here: a Cordis context is a proxy over the service
 * table, and reading a name that this fiber did not declare throws
 * `cannot get property "name" without inject` instead of yielding `undefined`.
 * That is what killed the bundle on a real install — a `source?.webServer` probe
 * looked like a safe degradation but was a startup crash on every profile that
 * actually had a web server.
 *
 * `ctx.get(name)` is the documented non-strict read: it returns `undefined` when
 * the service is not provided or not active yet.
 */
function peek(ctx, name) {
  return typeof ctx.get === 'function' ? ctx.get(name) : undefined
}

/**
 * Run `mount` against a service that may not exist yet.
 *
 * Two paths on purpose. When the service is already up — the normal desktop
 * case — it is mounted synchronously on this fiber, with no extra fiber created.
 * Only when it is absent does `ctx.inject` start a child fiber that declares the
 * name, which is the only way the service becomes readable in that context; the
 * callback stays unexecuted until the service appears, so a headless profile
 * still loads with just its skill.
 *
 * Lifecycle is `effect`'s, not a hand-rolled flag: a restarted service reloads
 * the fiber, disposing the old registration before the new one is made.
 */
function mountService(ctx, serviceName, mount) {
  const ready = peek(ctx, serviceName)
  if (ready) {
    if (!mount(ctx, ready)) {
      surface.notes.push(`${serviceName} is present but does not look right; nothing mounted`)
    }
    return
  }
  if (typeof ctx.inject !== 'function') {
    surface.notes.push(`${serviceName} is absent and this context has no inject(); nothing mounted`)
    return
  }
  ctx.inject([serviceName], (scoped) => {
    const resolved = scoped[serviceName]
    if (!resolved) {
      surface.notes.push(`${serviceName} was injected but resolved empty; nothing mounted`)
      return
    }
    if (!mount(scoped, resolved)) {
      surface.notes.push(`${serviceName} arrived but does not look right; nothing mounted`)
    }
  })
}

/** Attach the preview routes. */
function registerRoutes(ctx) {
  mountService(ctx, 'webServer', (source, server) => {
    if (typeof server.register !== 'function') return false
    source.effect(() => server.register({
      kind: 'prefix',
      path: ROUTE_BASE,
      handler: (req, res) => {
        let url
        try {
          url = new URL(req.url || '/', 'http://127.0.0.1')
        } catch {
          sendJson(res, 400, { error: 'malformed request url' })
          return
        }
        try {
          handleRequest(req, res, url)
        } catch (error) {
          if (!res.headersSent) sendJson(res, 500, { error: String(error?.message || error) })
          else res.end()
        }
      },
    }))
    surface.routes = true
    return true
  })
}

/* ------------------------------------------------------------------ */
/* tool                                                                */
/* ------------------------------------------------------------------ */

const TOOL_DESCRIPTION = [
  'Read, validate and preview a Better Endfield BEM package.',
  'Actions:',
  '  inspect  - manifest summary (format, target, counts) without decoding payloads.',
  '  validate - preview-side integrity checks (index ranges, payload lengths, mesh refs).',
  '  preview  - build a render scene and open it in the conversation 预览 tab.',
  '  convert  - run the BEM Tools CLI, which this bundle does not ship; doctor',
  '             reports whether one is reachable and from where.',
  '  doctor   - report which parts of the surface are available on this host.',
  'Preview needs a binary .bem file; ZIP containers are refused with a hint.',
].join('\n')

/*
 * The bundle carries a Python source tree, not a binary: the 33 MB PyInstaller
 * build is almost entirely the CPython runtime, and a machine that runs a mod
 * toolchain already has one. One tree serves both platforms — `python` on
 * Windows, `python3` elsewhere — so there is no per-platform directory to keep
 * in sync.
 *
 * Finding the tree is only half the job: the interpreter has to exist and the
 * tree has to be able to import its dependencies. The probe therefore runs a
 * real `--version` on every candidate and keeps the first that answers, instead
 * of asserting that a file exists.
 */

/** Entry point of a tree; the rest of the modules are imported from it. */
const CLI_BASENAMES = [
  'bem_tool',                        // Python source tree (the one this bundle ships)
  'BetterEndfieldNext.BemConverter', // a PyInstaller build, if the user installed one
  'bem-tools',                       // POSIX wrapper from the Android tarball
]

/**
 * On Windows a PyInstaller build is an `.exe`, or a bare name inside a checkout.
 * `.cmd`/`.bat` are deliberately absent: Node refuses to spawn them without
 * `shell: true`, and a shell would turn caller-supplied arguments into a
 * quoting hazard for no gain — no release publishes the converter that way.
 */
const WINDOWS_SUFFIXES = ['.exe', '']

const CLI_HINT = [
  'the bundle carries the BEM Tools sources and needs a Python 3.10+ interpreter:',
  '  - Python 3.14+ works as-is (compression.zstd is in the standard library),',
  '  - Python 3.10-3.13 needs `pip install zstandard` once,',
  '  - set BEM_PYTHON if `python3` / `python` is not the interpreter you want,',
  '  - every python3.x on PATH is probed, not only the first `python` — one of',
  '    them usually already has what the tree needs,',
  '  - and BEM_CLI overrides discovery entirely (bem_tool.py, a checkout',
  '    directory, or a standalone converter build).',
].join('\n')

function isFile(path) {
  try {
    return statSync(path).isFile()
  } catch (error) {
    if (error.code !== 'EACCES' && error.code !== 'EPERM') return false
    /*
     * App Execution Aliases live under `%LOCALAPPDATA%\Microsoft\WindowsApps`.
     * They run normally but cannot be `stat`ed (EACCES), and `lstat` reports
     * them as neither file nor directory — just a 111-byte reparse point. A
     * store-installed Python is only reachable through one of these, so
     * existence is the most this can assert; the `--version` probe downstream
     * is what proves the thing actually runs.
     */
    try {
      lstatSync(path)
      return true
    } catch {
      return false
    }
  }
}

function isDirectory(path) {
  try {
    return statSync(path).isDirectory()
  } catch {
    return false
  }
}

/** Cheap `#!` sniff — the whole file is never read, it may be a 30 MB binary. */
function hasShebang(path) {
  let fd
  try {
    fd = openSync(path, 'r')
    const head = Buffer.alloc(2)
    return readSync(fd, head, 0, 2, 0) === 2 && head[0] === 0x23 && head[1] === 0x21
  } catch {
    return false
  } finally {
    if (fd !== undefined) {
      try { closeSync(fd) } catch { /* already closed */ }
    }
  }
}

/**
 * Interpreter names worth trying, newest first.
 *
 * The order matters: 3.14+ carries `compression.zstd` in the standard library,
 * so an older interpreter that cannot import `zstandard` must never shadow it.
 * Versioned names come before the bare ones for the same reason — a `3.14`
 * anywhere on PATH should win over a `python` that happens to sit earlier.
 */
const PYTHON_VERSIONED = ['python3.14', 'python3.13', 'python3.12', 'python3.11', 'python3.10']
const PYTHON_GENERIC = ['python3', 'python', 'py']

/** Bounded so a pathological PATH cannot turn a probe into a minute of spawns. */
const MAX_INTERPRETERS = 12

/**
 * Enumerate every interpreter on PATH, by absolute path.
 *
 * Resolving bare names would be wrong here: PATH can hold two different
 * `python3.exe` (a bundled runtime plus a store install), and the OS picks the
 * first. The first one is not necessarily the one that can import `zstandard`,
 * which is exactly the situation this function exists to survive.
 */
function pythonInterpreters() {
  if (process.env.BEM_PYTHON) return [process.env.BEM_PYTHON]
  const suffix = process.platform === 'win32' ? '.exe' : ''
  const directories = (process.env.PATH || '').split(delimiter).filter(Boolean)
  const seen = new Set()
  const out = []
  for (const base of [...PYTHON_VERSIONED, ...PYTHON_GENERIC]) {
    for (const directory of directories) {
      const path = join(directory, base + suffix)
      const key = process.platform === 'win32' ? path.toLowerCase() : path
      if (seen.has(key)) continue
      if (!isFile(path)) continue
      seen.add(key)
      out.push(path)
      if (out.length >= MAX_INTERPRETERS) return out
    }
  }
  return out
}

/**
 * A match expands to every way it could be started. The caller runs them in
 * order and keeps the first that answers, which is what lets a Windows box with
 * only the `py` launcher, or only `python`, work without configuration.
 */
function describe(source, path) {
  if (path.toLowerCase().endsWith('.py')) {
    return pythonInterpreters().map((command) => ({
      source,
      command,
      prefix: [path],
      needsInterpreter: true,
    }))
  }
  // The POSIX entry point is a `#!` script. Node refuses to exec one that
  // arrived without its exec bit, and that bit is exactly what an npm tarball
  // packed on Windows loses — so hand the script to `sh` instead of guessing.
  if (process.platform !== 'win32' && hasShebang(path)) {
    return [{ source, command: 'sh', prefix: [path], viaShell: true }]
  }
  return [{ source, command: path, prefix: [] }]
}

/** Look for an entry point inside one directory. */
function scanDirectory(source, directory) {
  for (const basename of CLI_BASENAMES) {
    if (process.platform === 'win32') {
      for (const suffix of WINDOWS_SUFFIXES) {
        const path = join(directory, basename + suffix)
        if (isFile(path)) return describe(source, path)
      }
    } else {
      const path = join(directory, basename)
      if (isFile(path)) return describe(source, path)
    }
    // The source tree keeps its entry point beside its modules.
    const script = join(directory, `${basename}.py`)
    if (isFile(script)) return describe(source, script)
  }
  return null
}

/** Walk PATH by hand: spawning a bare name would need a shell, and a shell needs quoting. */
function scanPath() {
  for (const directory of (process.env.PATH || '').split(delimiter).filter(Boolean)) {
    const hit = scanDirectory('PATH', directory)
    if (hit) return hit
  }
  return null
}

/** Where each platform conventionally installs to. */
function conventionalDirectories() {
  const env = process.env
  const home = env.HOME || env.USERPROFILE || ''
  if (process.platform === 'win32') {
    return [
      env.LOCALAPPDATA && join(env.LOCALAPPDATA, 'BEM Tools'),
      env.ProgramFiles && join(env.ProgramFiles, 'BEM Tools'),
      home && join(home, 'bem-tools'),
      home && join(home, '.bem-tools'),
    ].filter(Boolean)
  }
  return [
    // `install.sh` defaults here because /sdcard is mounted noexec.
    '/data/local/tmp/bem-cli',
    // Termux installs land on PATH, but nothing guarantees it was exported.
    env.PREFIX && join(env.PREFIX, 'bin'),
    home && join(home, 'bem-cli'),
    home && join(home, '.local/bin'),
    '/opt/bem-cli',
  ].filter(Boolean)
}

/**
 * Ordered probe. `BEM_CLI` wins because it is the only lever a user can pull
 * without moving files; the bundle's own tree comes next; PATH and the
 * conventional directories cover a user who installed a converter themselves.
 * `tried` is reported by `doctor` so "found but broken" cannot look like "not
 * installed".
 */
function cliCandidates() {
  const tried = []

  const explicit = process.env.BEM_CLI
  if (explicit) {
    tried.push('BEM_CLI')
    if (isDirectory(explicit)) {
      const hit = scanDirectory('BEM_CLI', explicit)
      if (hit) return { candidates: hit, tried }
    } else if (isFile(explicit)) {
      return { candidates: describe('BEM_CLI', explicit), tried }
    } else {
      // A bare command name: leave resolution to the OS.
      return { candidates: [{ source: 'BEM_CLI', command: explicit, prefix: [] }], tried }
    }
  }

  const here = fileURLToPath(new URL('.', import.meta.url))
  tried.push('bundled')
  const bundled = scanDirectory('bundled', join(here, 'bin', 'cli'))
    || scanDirectory('bundled', join(here, 'bin', process.platform === 'win32' ? 'win' : 'android'))
    || scanDirectory('bundled', join(here, 'bin'))
  if (bundled) return { candidates: bundled, tried }

  tried.push('PATH')
  const onPath = scanPath()
  if (onPath) return { candidates: onPath, tried }

  for (const directory of conventionalDirectories()) {
    tried.push(directory)
    const hit = scanDirectory('install', directory)
    if (hit) return { candidates: hit, tried }
  }
  return { candidates: [], tried }
}

function run(command, argv, { timeout = 0 } = {}) {
  return new Promise((resolvePromise) => {
    let child
    try {
      child = spawn(command, argv, { stdio: ['ignore', 'pipe', 'pipe'], windowsHide: true })
    } catch (error) {
      resolvePromise({ code: -1, stdout: '', stderr: String(error?.message || error) })
      return
    }
    const out = []
    const err = []
    let settled = false
    let timer = null
    const settle = (value) => {
      if (settled) return
      settled = true
      if (timer) clearTimeout(timer)
      resolvePromise(value)
    }
    if (timeout > 0) {
      timer = setTimeout(() => {
        try { child.kill() } catch { /* already gone */ }
        settle({
          code: -1,
          stdout: Buffer.concat(out).toString('utf8'),
          stderr: Buffer.concat(err).toString('utf8') || `timed out after ${timeout} ms`,
        })
      }, timeout)
      timer.unref?.()
    }
    child.stdout?.on('data', (chunk) => out.push(chunk))
    child.stderr?.on('data', (chunk) => err.push(chunk))
    child.on('error', (error) => settle({ code: -1, stdout: '', stderr: String(error?.message || error) }))
    child.on('close', (code) => settle({
      code,
      stdout: Buffer.concat(out).toString('utf8'),
      stderr: Buffer.concat(err).toString('utf8'),
    }))
  })
}

const firstLine = (text) => (text || '').trim().split(/\r?\n/).filter(Boolean)[0] || null

/**
 * The useful line of a Python traceback is the last one — the exception type and
 * message. `firstLine` would report "Traceback (most recent call last):" for
 * every failure, which is why a missing `zstandard` used to look like a missing
 * interpreter.
 */
function diagnose(stderr) {
  const lines = (stderr || '').split(/\r?\n/).map((line) => line.trim()).filter(Boolean)
  const exception = [...lines].reverse().find((line) => /^[A-Za-z_][\w.]*(Error|Exception|Exit|Interrupt)\b/.test(line))
  return exception || lines[lines.length - 1] || null
}

/*
 * Probing is not free — it spawns each candidate — and `convert` would pay for
 * it on every call. The answer is cached, keyed by the environment it came from
 * so a mid-session `BEM_CLI` change still takes effect.
 */
let cliCache = null

function cliCacheKey() {
  return [process.env.BEM_CLI, process.env.BEM_PYTHON, process.env.PATH].join('\u0000')
}

/**
 * Resolve once, then reuse. A candidate counts only if `--version` succeeds: a
 * missing interpreter, a tree that cannot import `zstandard`, and a stale
 * `BEM_CLI` all look identical to `existsSync` and all fail on the first
 * convert, so the probe pays for one round trip to tell them apart.
 */
async function resolveCli() {
  const key = cliCacheKey()
  if (cliCache && cliCache.key === key) return cliCache.value

  const { candidates, tried } = cliCandidates()
  const failures = []
  let value = null
  for (const candidate of candidates) {
    const probe = await run(candidate.command, [...candidate.prefix, '--version'], { timeout: 3000 })
    if (probe.code === 0) {
      value = {
        found: true,
        source: candidate.source,
        command: candidate.command,
        args: candidate.prefix.length ? candidate.prefix : undefined,
        interpreter: Boolean(candidate.needsInterpreter),
        version: firstLine(probe.stdout) || firstLine(probe.stderr),
        // Why the earlier candidates lost — the usual case is a second
        // interpreter on PATH that has the dependency the first one lacks.
        rejected: failures.length
          ? failures.map((failure) => ({ command: failure.command, detail: failure.detail }))
          : undefined,
      }
      break
    }
    const detail = diagnose(probe.stderr) || `exit ${probe.code}`
    failures.push({
      command: candidate.command,
      args: candidate.prefix,
      detail,
      stderr: (probe.stderr || '').trim().slice(0, 1200) || undefined,
    })
  }
  if (!value) {
    value = {
      found: false,
      tried,
      attempts: candidates.length,
      failures,
      remedy: remedyFor(failures),
      hint: CLI_HINT,
    }
  }
  cliCache = { key, value }
  return value
}

/**
 * The one failure worth acting on automatically: the tree is fine and the
 * interpreter is fine, but `zstandard` is not installed. Python 3.14+ does not
 * need it at all, which is why the bundle does not ship a wheel — wheels are
 * ABI-tagged, so covering 3.10 through 3.14 would mean five copies and would
 * still break on the next minor release. One `pip install` is cheaper.
 */
function remedyFor(failures) {
  const missing = failures.find((failure) => /No module named 'zstandard'/.test(failure.detail || ''))
  if (!missing) return null
  return `${missing.command} -m pip install zstandard`
}

const ACTIONS = ['inspect', 'validate', 'preview', 'convert', 'doctor']

/**
 * Drop every `undefined` before the value leaves the plugin.
 *
 * The tool host validates results as lossless JSON, where `undefined` has no
 * representation at all — not as `null`, not as an omitted key. A single
 * `stdout: undefined` therefore fails the whole call *after* the handler has
 * already succeeded, which reads as "the tool is broken" with nothing in the
 * plugin to point at. Optional fields are built conditionally elsewhere; this
 * is the belt to that pair of braces, and it also catches the nested cases
 * (`cli.args`, `rejected`) that are easy to miss.
 *
 * Only plain objects and arrays are rebuilt: typed arrays, buffers and dates
 * must pass through untouched.
 */
function plain(value) {
  if (Array.isArray(value)) return value.map(plain)
  if (value === null || typeof value !== 'object') return value
  const prototype = Object.getPrototypeOf(value)
  if (prototype !== Object.prototype && prototype !== null) return value
  const out = {}
  for (const [key, entry] of Object.entries(value)) {
    if (entry !== undefined) out[key] = plain(entry)
  }
  return out
}

async function runTool(args) {
  const action = args?.action
  if (!action) throw new Error(`action is required: ${ACTIONS.join(' | ')}`)
  // Reject an unknown action here rather than falling through to the path
  // check, which would report a confusing "path is required for action wat".
  if (!ACTIONS.includes(action)) throw new Error(`unknown action ${action}; expected one of ${ACTIONS.join(' | ')}`)

  if (action === 'doctor') {
    return {
      ok: true,
      node: process.version,
      platform: process.platform,
      arch: process.arch,
      routes: ROUTE_PREFIX,
      surface: { routes: surface.routes, tool: surface.tool, notes: [...surface.notes] },
      preview: {
        ready: true,
        decoders: ['bc1', 'bc3', 'bc4', 'bc5', 'bc7', 'rgba32'],
        astc: 'not decoded in the browser build',
      },
      cli: await resolveCli(),
    }
  }

  if (action === 'convert') {
    const cli = await resolveCli()
    if (!cli.found) {
      return {
        ok: false,
        error: {
          code: 'CLI_MISSING',
          message: 'the bundled BEM Tools sources could not be started',
        },
        hint: cli.hint,
        remedy: cli.remedy,
        searched: cli.tried,
        attempts: cli.attempts,
        failures: cli.failures,
      }
    }
    const argv = Array.isArray(args.args) ? args.args.map(String) : []
    const invoke = (extra) => run(cli.command, [...(cli.args || []), ...extra])
    const result = await invoke(argv)
    let parsed = null
    try {
      parsed = JSON.parse(result.stdout)
    } catch {
      parsed = null
    }
    // Optional keys are omitted rather than written as `undefined`: the result
    // is validated as lossless JSON, and `undefined` has no representation
    // there — one such field fails the call after the handler already ran.
    const payload = {
      ok: result.code === 0,
      exitCode: result.code,
      cli: { source: cli.source, command: cli.command, args: cli.args, version: cli.version },
      result: parsed,
    }
    if (!parsed) payload.stdout = result.stdout
    if (result.stderr) payload.stderr = result.stderr
    // A missing interpreter or a missing `zstandard` surfaces here with the
    // explanation, not as a silent exit 127.
    if (result.code !== 0) payload.hint = CLI_HINT
    return payload
  }

  const target = args?.path
  if (typeof target !== 'string' || target.trim() === '') {
    throw new Error(`path is required for action ${action}`)
  }
  const absolute = resolve(target)

  // A typo is the common case. Throwing would hand the harness a raw fs error
  // with an absolute temp path in it; a code is something the agent can act on.
  if (!existsSync(absolute)) {
    return {
      ok: false,
      error: { code: 'ENOENT', message: `no such file: ${absolute}` },
      path: absolute,
      hint: 'the path is resolved from the dsh process working directory; prefer an absolute path',
    }
  }

  if (action === 'inspect') return { ok: true, ...inspect(absolute) }
  if (action === 'validate') {
    const report = validate(absolute)
    return { ok: report.ok, ...report }
  }
  if (action === 'preview') {
    const result = preview(absolute)
    return {
      ok: true,
      previewId: result.id,
      name: result.name,
      path: result.path,
      stats: result.scene.stats,
      selection: initialSelection(result.scene),
      url: `${ROUTE_PREFIX}api/scene/${result.id}`,
      hint: 'open the 预览 tab in this conversation; the stage updates as soon as a preview is registered',
    }
  }
  throw new Error(`unknown action ${action}`)
}

/**
 * Register the tool.
 *
 * `defineTool` would validate arguments and project the output for us, but it
 * lives in `@deepseek-ai/dsh-tools`, which a bundle installed into a profile may
 * not resolve. The definition below is the same shape without that helper: raw
 * JSON Schema parameters, a canonical output declaration, and a body returning
 * plain JSON.
 */
function registerTool(ctx) {
  mountService(ctx, 'tools', (source, registry) => {
    if (typeof registry.register !== 'function') return false
    const disposer = registry.register({
      name: 'bem',
      description: TOOL_DESCRIPTION,
      parameters: {
        type: 'object',
        properties: {
          action: { type: 'string', enum: ACTIONS },
          path: { type: 'string', description: 'absolute path to a .bem file, or a source path for convert' },
          args: { type: 'array', items: { type: 'string' }, description: 'arguments for the CLI when action=convert' },
        },
        required: ['action'],
        additionalProperties: false,
      },
      output: {
        schema: { type: 'object', additionalProperties: true },
        render: (callArgs, value) => [{ type: 'text', text: JSON.stringify(value, null, 2) }],
      },
      async execute(callArgs) {
        return plain(await runTool(callArgs))
      },
    })
    // The tool host's register() may or may not hand back a disposer; tie it to
    // the fiber only when it did, so unloading takes the tool with it.
    if (typeof disposer === 'function') source.effect(() => disposer)
    surface.tool = true
    return true
  })
}

export function apply(ctx) {
  // A reloaded fiber re-runs apply, so the record starts clean each time.
  surface.routes = false
  surface.tool = false
  surface.notes = []

  registerSkill(ctx)
  registerRoutes(ctx)
  registerTool(ctx)

  const missing = [!surface.routes && 'routes', !surface.tool && 'tool'].filter(Boolean)
  if (missing.length) {
    // Degrading is expected on a headless profile; a diagnostic makes it visible
    // instead of looking like the bundle silently did nothing.
    ctx.logger?.info?.(`bem-creator: skill registered, ${missing.join(' and ')} not mounted yet`)
    for (const note of surface.notes) ctx.logger?.info?.(`bem-creator: ${note}`)
  }
}
