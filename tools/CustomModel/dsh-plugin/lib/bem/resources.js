/** Local-only resource discovery. The selected texture pack is independent of the host OS. */
import { existsSync, readFileSync, readdirSync, realpathSync, statSync } from 'node:fs'
import { resolve, join, relative, isAbsolute } from 'node:path'

let configuredRoot = null
let configuredPlatform = null
const selectedPlatform = (value) => value || configuredPlatform || process.env.BEM_PREVIEW_PLATFORM || 'windows'
export const distributionIndexUrl = 'https://github.com/Dr-hydra/Better-Endfield/releases/download/game-resources/distribution-index.json'
const versionParts = (value) => /^\d+\.\d+\.\d+\.\d+$/.test(value) ? value.split('.').map(Number) : null
const compareVersions = (a, b) => {
  const left = versionParts(a), right = versionParts(b)
  for (let i = 0; i < 4; i++) if (left[i] !== right[i]) return right[i] - left[i]
  return 0
}

function localFile(root, name) {
  if (typeof name !== 'string' || !name || name.includes('\\') || name.includes(':') || name.split('/').some((part) => !part || part === '.' || part === '..')) throw new Error('Invalid resource path')
  const base = realpathSync(root), file = realpathSync(join(base, name))
  const rel = relative(base, file)
  if (!rel || rel === '..' || rel.startsWith('../') || rel.startsWith('..\\') || isAbsolute(rel)) throw new Error('Resource path escapes its directory')
  return file
}

export function readPreviewRepository(path, platform = 'windows') {
  const root = realpathSync(path)
  const manifestPath = localFile(root, 'manifest.json')
  if (statSync(manifestPath).size > 8 * 1024 * 1024) throw new Error('Resource manifest is too large')
  const manifest = JSON.parse(readFileSync(manifestPath, 'utf8'))
  if (manifest.schemaVersion !== 1 || manifest.kind !== 'betterendfield-resource-pack' || manifest.packageType !== 'preview') throw new Error('Not a supported preview resource pack')
  if (!['windows', 'android'].includes(platform) || manifest.platform !== platform) throw new Error('Preview pack does not match the selected BC7/windows or ASTC/android format')
  if (!versionParts(manifest.version) || manifest.gameVersion !== manifest.version.split('.').slice(0, 3).join('.') || !manifest.files || typeof manifest.files !== 'object' || Array.isArray(manifest.files)) throw new Error('Invalid resource version or inventory')
  const check = (name, expected = null) => {
    const file = localFile(root, name), size = statSync(file).size
    if (!Number.isSafeInteger(manifest.files[name]) || manifest.files[name] !== size || size > 64 * 1024 * 1024 || (expected !== null && size !== expected)) throw new Error('Unlisted or incomplete resource: ' + name)
    return file
  }
  const read = (name, expected = null) => readFileSync(check(name, expected))
  const index = JSON.parse(read('characters.json').toString('utf8'))
  if (index.schemaVersion !== 1 || !Array.isArray(index.characters)) throw new Error('Invalid preview character index')
  const signature = `${root}:${manifest.version}:${statSync(manifestPath).mtimeMs}:${statSync(localFile(root, 'characters.json')).mtimeMs}`
  return { root, manifest, index, check, read, signature }
}

export function resolvePreviewResources(explicitRoot, requestedPlatform) {
  const platform = selectedPlatform(requestedPlatform)
  const path = resolve(explicitRoot || configuredRoot || process.env.BEM_PREVIEW_RESOURCES || join(process.cwd(), 'resources'))
  try {
    if (existsSync(join(path, 'manifest.json'))) return { repository: readPreviewRepository(path, platform), path }
    if (!['windows', 'android'].includes(platform)) throw new Error('platform must be windows (BC7) or android (ASTC)')
    const folder = join(path, 'preview', platform)
    let pinned = null
    if (existsSync(join(path, 'state.json'))) {
      const state = JSON.parse(readFileSync(join(path, 'state.json'), 'utf8'))
      pinned = state[`preview.${platform}`] ?? null
      if (pinned !== null && !versionParts(pinned)) throw new Error('Invalid pinned preview version')
    }
    if (!existsSync(folder)) return { path, reason: 'not_installed', repository: null }
    const versions = readdirSync(folder, { withFileTypes: true }).filter((item) => item.isDirectory() && versionParts(item.name)).map((item) => item.name).sort(compareVersions)
    if (pinned !== null && !versions.includes(pinned)) return { path, reason: 'pinned_version_missing', repository: null }
    const version = pinned ?? versions[0]
    if (!version) return { path, reason: 'not_installed', repository: null }
    return { repository: readPreviewRepository(join(folder, version), platform), path }
  } catch (error) { return { path, reason: 'invalid_package', message: error.message, repository: null } }
}

export function previewResourcesStatus(path, platform) {
  const result = resolvePreviewResources(path, platform)
  return {
    status: result.repository ? 'ready' : 'unavailable', reason: result.repository ? 'ready' : result.reason,
    path: result.path, root: result.repository?.root ?? null, message: result.message ?? null,
    version: result.repository?.manifest.version ?? null, gameVersion: result.repository?.manifest.gameVersion ?? null,
    characters: result.repository?.index.characters.length ?? 0,
    host: { platform: process.platform, arch: process.arch },
    selectedPlatform: selectedPlatform(platform),
    choices: [{ platform: 'windows', texture: 'BC7', requirement: 'Browser BC decoder; works without an ASTC GPU extension.' }, { platform: 'android', texture: 'ASTC', requirement: 'Viewing browser must support WEBGL_compressed_texture_astc.' }],
    distributionIndexUrl,
    hint: 'Reuse an extracted local pack first. Set BEM_PREVIEW_RESOURCES or use bem action=resources path=<resources directory or pack directory>. No downloads are performed.',
  }
}

export function configurePreviewResources(path, platform) {
  const status = previewResourcesStatus(path, platform)
  if (status.status !== 'ready') return { ok: false, ...status }
  configuredRoot = resolve(path)
  configuredPlatform = selectedPlatform(platform)
  return { ok: true, ...status }
}
