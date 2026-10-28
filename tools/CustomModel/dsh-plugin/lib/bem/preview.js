/**
 * Preview registry: one built scene per package, plus the read-only summary
 * helpers the `bem` tool uses for `inspect` and `validate`.
 */
import { statSync } from 'node:fs'
import { basename, resolve } from 'node:path'
import { readPackage } from './container.js'
import { buildScene, evalWhen, meshesForRules, meshesForAppearance, defaultOptions } from './scene.js'
import { resolvePreviewResources } from './resources.js'
import { completeNativeScene } from './native.js'

/** Bounded so a long session that previews twenty packages cannot grow without limit. */
const MAX_SCENES = 6

const scenes = new Map()
let latestId = null
let sequence = 0

function sceneIdFor(path) {
  const stats = statSync(path)
  return `${stats.size.toString(36)}-${Math.round(stats.mtimeMs).toString(36)}`
}

export function getScene(id) {
  return scenes.get(id) || null
}

export function latestScene() {
  return latestId ? scenes.get(latestId) || null : null
}

export function latestSceneId() {
  return latestId
}

/**
 * Read a package and build its scene. Re-previewing an unchanged file reuses
 * the previous build, which is what makes the tab's refresh cheap.
 */
export function preview(path, options = {}) {
  const absolute = resolve(path)
  const resources = resolvePreviewResources(options.resourcesPath, options.platform)
  const key = JSON.stringify([absolute, sceneIdFor(absolute), resources.path, resources.repository?.signature, resources.reason, options.platform])
  const cached = [...scenes.values()].find((entry) => entry.key === key)
  if (cached) {
    latestId = cached.id
    return cached
  }
  const id = `${sceneIdFor(absolute)}-${sequence.toString(36)}`
  const pkg = readPackage(absolute)
  const { scene, blobs } = completeNativeScene(pkg, buildScene(pkg, { path: absolute }), resources)
  const entry = {
    id,
    key,
    path: absolute,
    name: basename(absolute),
    scene,
    blobs,
    builtAt: Date.now(),
    seq: sequence,
  }
  sequence += 1
  scenes.set(id, entry)
  latestId = id
  while (scenes.size > MAX_SCENES) {
    const oldest = [...scenes.values()].sort((a, b) => a.seq - b.seq)[0]
    if (!oldest || oldest.id === id) break
    scenes.delete(oldest.id)
  }
  return { id, ...entry }
}

/** Manifest-only summary: no payload is decoded. */
export function inspect(path) {
  const absolute = resolve(path)
  const pkg = readPackage(absolute)
  const m = pkg.manifest
  const meshes = m.meshes || []
  const textures = m.textures || []
  let vertices = 0
  let indices = 0
  for (const mesh of meshes) {
    vertices += Number(mesh.vertex_count) || 0
    if (Array.isArray(mesh.draws)) {
      for (const draw of mesh.draws) indices += Number(draw.count) || 0
    } else {
      indices += Number(mesh.index_count) || 0
    }
  }
  return {
    path: absolute,
    format: `BEMv${pkg.formatVersion}`,
    headerMinor: pkg.minor,
    package: {
      id: m.package_id || '',
      name: m.name || '',
      author: m.author || '',
      version: m.version || '',
      description: m.description || '',
    },
    target: {
      character: m.target?.character_id || '',
      platform: m.target?.platform || '',
      profile: m.target?.profile_id || '',
    },
    capabilities: m.required_capabilities || [],
    counts: {
      meshes: meshes.length,
      vertices,
      indices,
      textures: textures.length,
      payloads: pkg.payloadCount,
      appearances: (m.appearances || []).length,
      optionGroups: (m.option_groups || []).length,
      parameters: (m.parameters || []).length,
      channels: (m.mesh_deformations || []).length,
    },
    size: pkg.size,
    previewable: meshes.length > 0 && meshes.every((mesh) => Array.isArray(mesh.attributes)),
  }
}

/**
 * Preview-side integrity checks. These are the failures that make a render
 * wrong rather than merely unusual — an out-of-range index draws garbage, a
 * short payload reads past its buffer. Anything about authoring intent belongs
 * to the Python validator, which is the authority.
 */
export function validate(path) {
  const absolute = resolve(path)
  const pkg = readPackage(absolute)
  const m = pkg.manifest
  const issues = []

  const meshes = m.meshes || []
  meshes.forEach((mesh, index) => {
    const vertexCount = Number(mesh.vertex_count) || 0
    if (vertexCount === 0) issues.push({ code: 'EMPTY_MESH', mesh: index, message: '网格没有顶点' })
    for (const [streamIndex, stream] of (mesh.streams || []).entries()) {
      let data
      try {
        data = pkg.payload(stream.payload)
      } catch {
        issues.push({ code: 'STREAM_PAYLOAD_MISSING', mesh: index, stream: streamIndex, message: '顶点流载荷缺失' })
        continue
      }
      const need = vertexCount * stream.stride
      if (data.length < need) {
        issues.push({ code: 'STREAM_SHORT', mesh: index, stream: streamIndex, message: `顶点流 ${data.length} 字节，需要 ${need}` })
      }
    }
    const indexSize = Number(mesh.index_size) || 2
    // Same two layouts as the scene builder: 1.3+ draws own an index payload,
    // earlier draws are `start`/`count` slices of the mesh's single payload.
    const drawSpecs = Array.isArray(mesh.draws) && mesh.draws.length > 0
      ? mesh.draws.map((draw) => ({
        count: Number(draw.count) || 0,
        payload: draw.indices !== undefined ? draw.indices : mesh.indices,
        start: Number(draw.start) || 0,
      }))
      : [{ count: Number(mesh.index_count) || 0, payload: mesh.indices, start: 0 }]
    for (const draw of drawSpecs) {
      if (!draw.count) continue
      let data
      try {
        data = pkg.payload(draw.payload)
      } catch {
        issues.push({ code: 'INDEX_PAYLOAD_MISSING', mesh: index, message: '索引载荷缺失' })
        continue
      }
      if (data.length < (draw.start + draw.count) * indexSize) {
        issues.push({ code: 'INDEX_SHORT', mesh: index, message: '索引载荷短于声明条数' })
        continue
      }
      for (let i = 0; i < draw.count; i += 1) {
        const at = (draw.start + i) * indexSize
        const value = indexSize === 4 ? data.readUInt32LE(at) : data.readUInt16LE(at)
        if (value >= vertexCount) {
          issues.push({ code: 'INDEX_RANGE', mesh: index, message: `索引 ${value} 超出顶点数 ${vertexCount}` })
          break
        }
      }
    }
  })

  ;(m.textures || []).forEach((texture, index) => {
    let data = null
    try {
      data = pkg.payload(texture.payload)
    } catch {
      issues.push({ code: 'TEXTURE_PAYLOAD_MISSING', texture: index, message: '贴图载荷缺失' })
      return
    }
    if (data.length === 0) issues.push({ code: 'TEXTURE_EMPTY', texture: index, message: '贴图载荷为空' })
  })

  const meshCount = meshes.length
  const checkMeshRef = (value, where) => {
    if (Number.isInteger(value) && (value < 0 || value >= meshCount)) {
      issues.push({ code: 'MESH_REF_RANGE', message: `${where} 引用了不存在的网格 ${value}` })
    }
  }
  for (const appearance of m.appearances || []) {
    for (const component of appearance.components || []) checkMeshRef(component.mesh, `appearance ${appearance.id}`)
  }
  for (const rule of m.component_rules || []) {
    for (const candidate of rule.candidates || []) checkMeshRef(candidate.mesh, `component_rules[${rule.target}]`)
  }
  for (const channel of m.mesh_deformations || []) checkMeshRef(channel.mesh, `mesh_deformations ${channel.parameter}`)

  const byMesh = new Map((m.mesh_deformations || []).map((channel) => [`${channel.mesh}/${channel.parameter}`, channel]))
  const parameterIds = new Set((m.parameters || []).map((parameter) => parameter.id))
  for (const key of byMesh.keys()) {
    const parameter = key.split('/')[1]
    if (!parameterIds.has(parameter)) {
      issues.push({ code: 'CHANNEL_PARAMETER_UNKNOWN', message: `形变通道引用了未声明的参数 ${parameter}` })
    }
  }

  return {
    path: absolute,
    format: `BEMv${pkg.formatVersion}`,
    ok: issues.length === 0,
    issues,
    note: '预览侧检查；完整校验以 BEM Tools CLI 的 validate 为准。',
  }
}

/**
 * Resolve the geometry an initial render should show, so the tab can open on a
 * sensible state instead of an empty stage.
 */
export function nativeMeshesForSelection(scene, options, appearanceId) {
  const appearance = scene.appearances.find((item) => item.id === appearanceId) || scene.appearances[0]
  return (scene.nativeMeshes || []).filter(({ target }) => {
    const rule = scene.componentRules.find((item) => item.target === target)
    const operation = scene.componentRules.length
      ? rule?.candidates.find((candidate) => evalWhen(candidate.when, options))?.operation
      : appearance?.components.find((item) => item.target === target)?.operation
    return !operation || operation === 'keep'
  }).map((item) => item.index)
}

export function initialSelection(scene) {
  if (scene.componentRules.length > 0) {
    const state = defaultOptions(scene.optionGroups)
    return { mode: 'rules', options: state, meshes: [...meshesForRules(scene.componentRules, state), ...nativeMeshesForSelection(scene, state)] }
  }
  const appearance = scene.appearances.find((a) => a.id === scene.defaultAppearance) || scene.appearances[0]
  if (appearance) return { mode: 'appearance', appearance: appearance.id, meshes: [...appearance.meshes, ...nativeMeshesForSelection(scene, {}, appearance.id)] }
  return { mode: 'all', meshes: scene.meshes.map((mesh) => mesh.index) }
}

export { evalWhen, defaultOptions, meshesForRules, meshesForAppearance }
