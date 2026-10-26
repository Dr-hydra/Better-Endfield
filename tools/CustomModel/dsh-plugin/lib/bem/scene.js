/**
 * Turn a decoded package into a render-ready scene.
 *
 * The scene is deliberately dumb: a list of meshes with already-extracted
 * position/UV/index buffers, the texture blocks they bind to, the option or
 * appearance rules that decide visibility, and the shape-key channels. All of
 * it is plain JSON plus binary blobs, so the browser half never needs to know
 * what a BEM container is.
 */

/** Bytes per element for Unity's `VertexAttributeFormat` values 0..11. */
const ELEMENT_BYTES = [4, 2, 1, 1, 2, 2, 1, 1, 2, 2, 4, 4]

/** Semantic ids: 0 position, 4 texcoord0, 6 texcoord2, 12/13 skin. */
const SEMANTIC_POSITION = 0
const SEMANTIC_TEXCOORD0 = 4

/**
 * Texture format -> decoder family. Mirrors the Python table.
 *
 * `decodable` is a promise the browser build can actually keep. ASTC needs the
 * GPU's ASTC extension, which WebGL2 does not expose, so those blocks never get
 * a blob: handing the client a payload no decoder can read produced one failed
 * request per texture ("no decoder for astc", six of them on one package)
 * instead of the one sentence that explains it.
 */
const TEXTURE_FAMILY = {
  4: { family: 'rgba32', block: null, bytesPerBlock: null, decodable: true },
  10: { family: 'bc1', block: 4, bytesPerBlock: 8, decodable: true },
  12: { family: 'bc3', block: 4, bytesPerBlock: 16, decodable: true },
  25: { family: 'bc7', block: 4, bytesPerBlock: 16, decodable: true },
  26: { family: 'bc4', block: 4, bytesPerBlock: 8, decodable: true },
  27: { family: 'bc5', block: 4, bytesPerBlock: 16, decodable: true },
  48: { family: 'astc', block: 4, bytesPerBlock: 16, decodable: false },
  49: { family: 'astc', block: 5, bytesPerBlock: 16, decodable: false },
  50: { family: 'astc', block: 6, bytesPerBlock: 16, decodable: false },
  63: { family: 'unknown', block: null, bytesPerBlock: null, decodable: false },
}

export function textureFamily(format) {
  return TEXTURE_FAMILY[format] || { family: 'unknown', block: null, bytesPerBlock: null, decodable: false }
}

/** Why a texture carries no blob. The client prints this verbatim. */
export function skipReason(info, texture) {
  if (info.family === 'astc') return 'ASTC blocks are not decoded in the browser build'
  if (info.family === 'unknown') return `texture format ${texture.format} is not previewable`
  return 'payload unavailable'
}

/** Slice the base mip out of a block-compressed payload. */
function baseLevelBytes(width, height, info) {
  if (info.family === 'rgba32') return width * height * 4
  if (!info.block) return null
  const bw = Math.ceil(width / info.block)
  const bh = Math.ceil(height / info.block)
  return bw * bh * info.bytesPerBlock
}

/**
 * The packages store Z-up geometry: a 1.67 m character measures 1.668 units
 * along Z but only 0.58 along Y, so a Y-up camera laid it on its back. The
 * rotate happens once, here — positions and deformation deltas together, since
 * the deltas are added to those same vertices — and nothing downstream has to
 * know which axis a package was authored on.
 */
const UP_AXIS_SLACK = 1.05

/** Decide from the union bounds whether the package is Z-up. */
function decideOrientation(geometry) {
  const min = [Infinity, Infinity, Infinity]
  const max = [-Infinity, -Infinity, -Infinity]
  for (const record of geometry) {
    const positions = record.positions
    if (!positions) continue
    for (let i = 0; i < positions.length; i += 3) {
      for (let c = 0; c < 3; c += 1) {
        const value = positions[i + c]
        if (value < min[c]) min[c] = value
        if (value > max[c]) max[c] = value
      }
    }
  }
  if (!Number.isFinite(min[0])) return { up: 'Y', sourceUp: 'Y', rotated: false }
  // A character is taller than it is deep; the slack keeps a symmetric prop
  // (a ball, a cube) from being flipped for no reason.
  const rotated = (max[2] - min[2]) > (max[1] - min[1]) * UP_AXIS_SLACK
  return { up: 'Y', sourceUp: rotated ? 'Z' : 'Y', rotated }
}

/** (x, y, z) -> (x, z, -y): a -90° turn about X, which is a proper rotation. */
function rotateZUpToYUp(typed) {
  if (!typed) return
  for (let i = 0; i < typed.length; i += 3) {
    const y = typed[i + 1]
    typed[i + 1] = typed[i + 2]
    typed[i + 2] = -y
  }
}

/**
 * Resolve a mesh attribute into a typed-array view over its stream.
 *
 * Streams are interleaved: attribute offset + index * stride. Only the float32
 * layouts in use today are converted; anything else is reported as absent so a
 * preview shows a mesh without UVs rather than misreading floats as positions.
 */
function readAttribute(mesh, semantics, components, payloadOf) {
  const attributes = Array.isArray(mesh.attributes) ? mesh.attributes : []
  const spec = attributes.find((a) => a[0] === semantics && a[2] === components && a[1] === 0)
  if (!spec) return null
  const [, , dim, streamIndex, offset] = spec
  const stream = mesh.streams?.[streamIndex]
  if (!stream) return null
  const data = payloadOf(stream.payload)
  const stride = stream.stride
  const elementBytes = ELEMENT_BYTES[0] * dim
  if (!data || stride < offset + elementBytes) return null
  const count = mesh.vertex_count
  if (data.length < count * stride) return null
  const out = new Float32Array(count * dim)
  for (let i = 0; i < count; i += 1) {
    const base = i * stride + offset
    for (let c = 0; c < dim; c += 1) out[i * dim + c] = data.readFloatLE(base + c * 4)
  }
  return out
}

function readIndices(payload, count, indexSize) {
  const out = indexSize === 4 ? new Uint32Array(count) : new Uint16Array(count)
  for (let i = 0; i < count; i += 1) {
    out[i] = indexSize === 4 ? payload.readUInt32LE(i * 4) : payload.readUInt16LE(i * 2)
  }
  return out
}

/** Split `S_actor_aglina_body_01_lod0` into comparable lower-case tokens. */
function nameTokens(name) {
  return String(name || '')
    .toLowerCase()
    .replace(/\.(png|tga|dds)$/, '')
    .split(/[_\s]+/)
    .filter(Boolean)
}

/**
 * Guess a texture for a mesh by name when the package predates per-draw
 * material bindings. `T_actor_aglina_body_01_D` against
 * `S_actor_aglina_body_01_lod0` shares four tokens, which is what makes the
 * 1.0–1.2 packages renderable at all; when nothing shares a prefix the mesh
 * stays untextured rather than picking a random texture.
 */
function matchTexture(meshName, textures, wantSuffix) {
  const meshTokens = nameTokens(meshName)
  if (meshTokens.length === 0) return null
  let best = null
  let bestScore = 0
  for (let i = 0; i < textures.length; i += 1) {
    const texture = textures[i]
    const tokens = nameTokens(texture.name)
    if (tokens.length === 0) continue
    const suffix = tokens[tokens.length - 1]
    if (wantSuffix && suffix !== wantSuffix) continue
    let score = 0
    while (score < meshTokens.length && score < tokens.length && meshTokens[score] === tokens[score]) score += 1
    if (score === 0) continue
    if (score > bestScore) {
      bestScore = score
      best = i
    }
  }
  return best
}

/**
 * Evaluate a `when` expression against option state.
 *
 * Grammar, as written by the compiler: `{eq:[groupId, choiceId]}`, `{all:[…]}`,
 * `{any:[…]}`, `{not: expr}`. An expression this evaluator does not recognise
 * evaluates to `true` — a preview that hides geometry on a parse failure is
 * worse than one that shows too much.
 */
export function evalWhen(when, state) {
  if (!when || typeof when !== 'object') return true
  if (Array.isArray(when.eq)) {
    const [group, choice] = when.eq
    return state[group] === choice
  }
  if (Array.isArray(when.all)) return when.all.every((node) => evalWhen(node, state))
  if (Array.isArray(when.any)) return when.any.some((node) => evalWhen(node, state))
  if (when.not !== undefined) return !evalWhen(when.not, state)
  return true
}

/** Default option state from `option_groups`. */
export function defaultOptions(groups) {
  const state = {}
  for (const group of groups || []) {
    const fallback = group.choices?.[0]?.id
    state[group.id] = group.default ?? fallback ?? null
  }
  return state
}

/** Resolve which meshes an option state selects, for 1.5 `component_rules`. */
export function meshesForRules(rules, state) {
  const keep = new Set()
  for (const rule of rules || []) {
    const candidates = rule.candidates || []
    const chosen = candidates.find((c) => evalWhen(c.when, state)) || null
    if (chosen && chosen.operation === 'replace' && Number.isInteger(chosen.mesh)) keep.add(chosen.mesh)
  }
  return keep
}

/** Resolve which meshes an appearance selects, for 1.0–1.4 `appearances`. */
export function meshesForAppearance(appearance) {
  const keep = new Set()
  for (const component of appearance?.components || []) {
    if (component.operation === 'replace' && Number.isInteger(component.mesh)) keep.add(component.mesh)
  }
  return keep
}

/**
 * Parse one deformation frame payload.
 *
 *   u32 J, J bytes of JSON, then N records of (u32 vertex, f32 x, f32 y, f32 z).
 */
function readDeformationFrame(data, vertexCount) {
  const headerLength = data.readUInt32LE(0)
  const json = JSON.parse(data.subarray(4, 4 + headerLength).toString('utf8'))
  const count = Number(json.count) || 0
  if (data.length !== 4 + headerLength + count * 16) {
    throw new Error(`deformation frame length ${data.length} does not match count ${count}`)
  }
  const indices = new Uint32Array(count)
  const deltas = new Float32Array(count * 3)
  for (let i = 0; i < count; i += 1) {
    const base = 4 + headerLength + i * 16
    const vertex = data.readUInt32LE(base)
    if (vertex >= vertexCount) throw new Error(`deformation references vertex ${vertex} of ${vertexCount}`)
    indices[i] = vertex
    deltas[i * 3] = data.readFloatLE(base + 4)
    deltas[i * 3 + 1] = data.readFloatLE(base + 8)
    deltas[i * 3 + 2] = data.readFloatLE(base + 12)
  }
  return { indices, deltas }
}

/**
 * Build the scene.
 *
 * `blobs` is a mutable map the caller serves over HTTP; ids are stable for the
 * life of one scene, so a client can cache a decoded texture by blob id.
 */
export function buildScene(pkg, { path } = {}) {
  const m = pkg.manifest
  const blobs = new Map()
  let blobSeq = 0
  function addBlob(kind, buffer) {
    const id = `b${blobSeq}`
    blobSeq += 1
    blobs.set(id, { kind, data: buffer })
    return id
  }

  const payloadOf = (index) => {
    try {
      return pkg.payload(index)
    } catch {
      return null
    }
  }

  const targetComponents = m.target?.components || []

  // Texture blocks stay compressed and unread until a client asks for the blob:
  // a package holds ~30 MB of BC7 that most previews never decode, so paying
  // for it during `preview` would cost seconds before the tab can open.
  const textures = (m.textures || []).map((texture, index) => {
    const info = textureFamily(texture.format)
    const levelBytes = info.decodable ? baseLevelBytes(texture.width, texture.height, info) : null
    let blob = null
    if (levelBytes) {
      const id = `b${blobSeq}`
      blobSeq += 1
      blobs.set(id, {
        kind: 'texture',
        get() {
          const raw = payloadOf(texture.payload)
          if (!raw || raw.length < levelBytes) return null
          return raw.subarray(0, levelBytes)
        },
      })
      blob = id
    }
    return {
      index,
      name: texture.original_name || `texture ${index}`,
      width: texture.width,
      height: texture.height,
      mips: texture.mips,
      format: texture.format,
      srgb: Boolean(texture.srgb),
      family: info.family,
      block: info.block,
      blob,
      skipped: blob ? null : skipReason(info, texture),
    }
  })

  // A mesh name is only recoverable through the component it replaces: the
  // package stores meshes positionally and names components, not meshes.
  const meshNames = new Map()
  const rememberName = (meshIndex, name) => {
    if (!Number.isInteger(meshIndex) || !name) return
    if (!meshNames.has(meshIndex)) meshNames.set(meshIndex, name)
  }
  for (const appearance of m.appearances || []) {
    for (const component of appearance.components || []) {
      rememberName(component.mesh, targetComponents[component.target]?.mesh_name)
    }
  }
  for (const rule of m.component_rules || []) {
    for (const candidate of rule.candidates || []) {
      if (candidate.mesh !== undefined) {
        rememberName(candidate.mesh, targetComponents[rule.target]?.mesh_name)
      }
    }
  }
  // Fall back to matching by original index count when no rule mentions it.
  for (let i = 0; i < (m.meshes || []).length; i += 1) {
    if (meshNames.has(i)) continue
    const mesh = m.meshes[i]
    const draws = Array.isArray(mesh.draws) ? mesh.draws : null
    const indexCount = draws
      ? draws.reduce((sum, draw) => sum + (Number(draw.count) || 0), 0)
      : Number(mesh.index_count) || 0
    const guess = targetComponents.find((c) => c.original_index_count === indexCount)
    if (guess) meshNames.set(i, guess.mesh_name)
  }

  // Every vertex stream is read once up front: the rotation is decided from the
  // union bounds, so all positions have to exist before any of them is served.
  const geometry = (m.meshes || []).map((mesh) => ({
    positions: readAttribute(mesh, SEMANTIC_POSITION, 3, payloadOf),
    uvs: readAttribute(mesh, SEMANTIC_TEXCOORD0, 2, payloadOf),
  }))
  const orientation = decideOrientation(geometry)
  if (orientation.rotated) for (const record of geometry) rotateZUpToYUp(record.positions)

  const meshes = (m.meshes || []).map((mesh, index) => {
    const name = meshNames.get(index) || `mesh ${index}`
    const { positions, uvs } = geometry[index]
    const vertexCount = Number(mesh.vertex_count) || 0
    const indexSize = Number(mesh.index_size) || 2

    const draws = []
    // Two layouts exist. Before 1.3 a mesh owns one index payload and every
    // draw is a `start`/`count` slice of it; from 1.3 each draw carries its own
    // index payload, because a draw is only referenced when its `when`
    // condition holds. Both end up as `{ first, count }` into one buffer.
    const sharedIndices = payloadOf(mesh.indices)
    const sharedCount = Number(mesh.index_count) || 0
    let sharedBlob = null
    if (sharedIndices && sharedCount > 0 && sharedIndices.length >= sharedCount * indexSize) {
      sharedBlob = addBlob('indices', Buffer.from(readIndices(sharedIndices, sharedCount, indexSize).buffer))
    }

    if (Array.isArray(mesh.draws) && mesh.draws.length > 0) {
      for (const draw of mesh.draws) {
        const count = Number(draw.count) || 0
        if (count === 0) continue
        if (draw.indices !== undefined) {
          const data = payloadOf(draw.indices)
          if (!data || data.length < count * indexSize) continue
          draws.push({
            first: 0,
            count,
            indicesBlob: addBlob('indices', Buffer.from(readIndices(data, count, indexSize).buffer)),
            textures: Array.isArray(draw.textures) ? draw.textures.slice() : [],
            material: draw.material_name || null,
            when: draw.when || null,
            overrides: draw.material_overrides || null,
          })
          continue
        }
        if (!sharedBlob) continue
        const start = Number(draw.start) || 0
        if (start + count > sharedCount) continue
        draws.push({
          first: start,
          count,
          indicesBlob: sharedBlob,
          textures: Array.isArray(draw.textures) ? draw.textures.slice() : [],
          material: draw.material_name || null,
          when: draw.when || null,
          overrides: draw.material_overrides || null,
        })
      }
    } else if (sharedBlob) {
      draws.push({
        first: 0,
        count: sharedCount,
        indicesBlob: sharedBlob,
        textures: [],
        material: null,
        when: null,
        overrides: null,
      })
    }

    // Untextured packages: bind by name so a 1.0 package still renders.
    if (draws.length > 0 && draws.every((draw) => draw.textures.length === 0)) {
      const albedo = matchTexture(name, textures, 'd')
      const normal = matchTexture(name, textures, 'n')
      for (const draw of draws) {
        if (albedo !== null) draw.textures = [albedo]
        if (normal !== null) draw.autoNormal = normal
      }
    }
    for (const draw of draws) {
      if (draw.textures.length > 1) {
        const candidate = textures[draw.textures[1]]
        if (candidate && !/_N$/i.test(candidate.name)) delete draw.textures[1]
      }
    }

    return {
      index,
      name,
      vertexCount,
      indexSize,
      positionsBlob: positions ? addBlob('positions', Buffer.from(positions.buffer)) : null,
      uvsBlob: uvs ? addBlob('uvs', Buffer.from(uvs.buffer)) : null,
      draws,
    }
  })

  const channels = []
  for (const channel of m.mesh_deformations || []) {
    const mesh = meshes[channel.mesh]
    if (!mesh) continue
    const frames = []
    for (const frame of channel.frames || []) {
      if (frame.neutral) {
        frames.push({ value: frame.value, neutral: true, count: 0, indicesBlob: null, deltasBlob: null })
        continue
      }
      const data = payloadOf(frame.payload)
      if (!data) continue
      let parsed
      try {
        parsed = readDeformationFrame(data, mesh.vertexCount)
      } catch {
        continue
      }
      // Same space as the positions, so the same rotation applies.
      if (orientation.rotated) rotateZUpToYUp(parsed.deltas)
      frames.push({
        value: frame.value,
        neutral: false,
        count: parsed.indices.length,
        indicesBlob: addBlob('deform-indices', Buffer.from(parsed.indices.buffer)),
        deltasBlob: addBlob('deform-deltas', Buffer.from(parsed.deltas.buffer)),
      })
    }
    frames.sort((a, b) => a.value - b.value)
    if (frames.some((frame) => !frame.neutral)) {
      channels.push({ mesh: channel.mesh, parameter: channel.parameter, frames })
    }
  }

  const scene = {
    schema: 'bem-preview/1',
    package: {
      path: path || pkg.path,
      id: m.package_id || '',
      name: m.name || '',
      author: m.author || '',
      version: m.version || '',
      description: m.description || '',
      minor: pkg.minor,
      formatVersion: pkg.formatVersion,
      payloadCount: pkg.payloadCount,
      size: pkg.size,
    },
    target: {
      character: m.target?.character_id || '',
      platform: m.target?.platform || '',
      profile: m.target?.profile_id || '',
      components: targetComponents.map((component, index) => ({
        index,
        name: component.mesh_name || `component ${index}`,
        originalIndexCount: component.original_index_count ?? null,
      })),
    },
    meshes,
    textures,
    orientation,
    appearances: (m.appearances || []).map((appearance, index) => ({
      index,
      id: appearance.id || `appearance-${index}`,
      name: appearance.name || appearance.id || `外观 ${index + 1}`,
      meshes: [...meshesForAppearance(appearance)],
      components: (appearance.components || []).map((c) => ({
        target: c.target,
        operation: c.operation,
        mesh: c.mesh ?? null,
        targetName: targetComponents[c.target]?.mesh_name || null,
      })),
    })),
    defaultAppearance: m.default_appearance_id || m.appearances?.[0]?.id || null,
    optionGroups: (m.option_groups || []).map((group) => ({
      id: group.id,
      name: group.name || group.id,
      default: group.default ?? group.choices?.[0]?.id ?? null,
      choices: (group.choices || []).map((choice) => ({ id: choice.id, name: choice.name || choice.id })),
    })),
    componentRules: (m.component_rules || []).map((rule) => ({
      target: rule.target,
      targetName: targetComponents[rule.target]?.mesh_name || null,
      candidates: (rule.candidates || []).map((candidate) => ({
        when: candidate.when || null,
        operation: candidate.operation,
        mesh: candidate.mesh ?? null,
      })),
    })),
    parameters: (m.parameters || []).map((parameter) => ({
      id: parameter.id,
      name: parameter.name || parameter.id,
      min: parameter.min,
      max: parameter.max,
      neutral: parameter.neutral,
      default: parameter.default ?? parameter.neutral ?? parameter.min,
      step: parameter.step || 1,
      availableWhen: parameter.available_when || null,
    })),
    channels,
    stats: {
      meshes: meshes.length,
      vertices: meshes.reduce((sum, mesh) => sum + mesh.vertexCount, 0),
      draws: meshes.reduce((sum, mesh) => sum + mesh.draws.length, 0),
      textures: textures.length,
      previewableTextures: textures.filter((texture) => texture.blob).length,
      parameters: (m.parameters || []).length,
      channels: channels.length,
    },
  }

  return { scene, blobs }
}
