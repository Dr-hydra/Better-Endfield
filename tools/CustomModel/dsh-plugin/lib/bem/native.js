/** Read only the selected character. Missing resources never hide package meshes. */
export function completeNativeScene(pkg, result, resolution) {
  const { scene, blobs } = result
  const repository = resolution.repository
  const report = scene.nativeCompletion = { status: 'unavailable', reason: resolution.reason || 'not_installed', resourceVersion: repository?.manifest.version ?? null, meshes: 0, textures: 0, message: resolution.message ?? null }
  scene.nativeMeshes = []
  if (!repository) return result
  const target = pkg.manifest.target || {}
  const character = target.character_id || (target.kind === 'character' ? target.id : null)
  const covered = repository.index.characters.filter((entry) => entry.character === character)
  if (!covered.length) { report.reason = 'character_not_covered'; return result }
  const packs = new Map(), textureIds = new Map()
  let sequence = 0, matched = 0, failures = 0, loadedBytes = 0
  const addBlob = (kind, data, get) => {
    const id = `native${sequence++}`
    blobs.set(id, get ? { kind, get } : { kind, data })
    return id
  }
  const getPack = (entry) => {
    if (packs.has(entry.path)) return packs.get(entry.path)
    const prefix = entry.path + '/'
    const data = JSON.parse(repository.read(prefix + 'pack.json').toString('utf8'))
    if (data.schema !== 1 || data.kind !== 'native-head-preview-prototype' || data.character !== character) throw new Error('Invalid character preview data')
    const read = (name, size) => {
      const bytes = repository.read(prefix + name, size)
      loadedBytes += bytes.length
      if (loadedBytes > 256 * 1024 * 1024) throw new Error('Character geometry exceeds preview memory budget')
      return bytes
    }
    const pack = { data, read, entry, prefix }
    packs.set(entry.path, pack)
    return pack
  }
  const baseTexture = (renderer, slot) => renderer.materials.find((material) => material.slot === slot)?.textures?.find((texture) => texture.property === '_BaseMap' && texture.id != null)
  const textureFor = (pack, binding) => {
    if (!binding) return null
    const key = `${pack.prefix}:${binding.id}`
    if (textureIds.has(key)) return textureIds.get(key)
    const texture = pack.data.textures.find((row) => row.id === binding.id)
    if (!texture) return null
    const { width, height, mips } = texture
    if (![width, height, mips].every(Number.isSafeInteger) || width < 1 || height < 1 || width > 4096 || height > 4096 || mips < 1 || mips > 13) throw new Error('Invalid preview texture dimensions')
    const family = { BC7: 'bc7', RGBA32: 'rgba32', ASTC_6x6: 'astc' }[texture.format]
    if (!family || (family === 'astc' && repository.manifest.platform !== 'android') || (family === 'bc7' && repository.manifest.platform !== 'windows')) throw new Error('Wrong preview texture format')
    const block = family === 'astc' ? 6 : 4
    const bytesFor = (w, h) => family === 'rgba32' ? w * h * 4 : Math.ceil(w / block) * Math.ceil(h / block) * 16
    let total = 0, w = width, h = height
    for (let level = 0; level < mips; level++) { total += bytesFor(w, h); w = Math.max(1, Math.floor(w / 2)); h = Math.max(1, Math.floor(h / 2)) }
    const file = pack.prefix + texture.payload
    if (repository.manifest.files[file] !== total) throw new Error('Invalid texture mip inventory')
    repository.check(file, total)
    const index = scene.textures.length
    scene.textures.push({ index, name: texture.name, width, height, mips, format: family === 'astc' ? 50 : family === 'bc7' ? 25 : 4, family, block: family === 'rgba32' ? null : block, srgb: texture.srgb !== false, native: true, blob: addBlob('texture', null, () => repository.read(file, total).subarray(0, bytesFor(width, height))), skipped: null })
    textureIds.set(key, index)
    report.textures++
    return index
  }
  const uvFor = (binding) => {
    const values = [binding?.scale?.X ?? 1, binding?.scale?.Y ?? 1, binding?.offset?.X ?? 0, binding?.offset?.Y ?? 0]
    if (!values.every(Number.isFinite)) throw new Error('Invalid native UV transform')
    return values
  }
  for (const [componentIndex, component] of (target.components || []).entries()) {
    try {
      const resource = (target.resources || []).find((row) => row.id === component.resource)
      // The selected pack platform describes texture encoding, not the BEM
      // target. Full prefab/renderer/material identities decide compatibility.
      if (target.resources?.length && !resource) continue
      const legacyRoot = target.world_resource || `${character}_postmodel`
      const entries = covered.filter((entry) => resource ? entry.assetPaths?.includes(resource.asset_path) : entry.resourceRoots?.includes(legacyRoot))
      if (entries.length !== 1) { if (entries.length > 1) failures++; continue }
      const pack = getPack(entries[0])
      const renderers = pack.data.renderers.filter((row) => {
        if (row.name !== component.mesh_name) return false
        if (resource) {
          const rel = row.path.startsWith(row.resource_root + '/') ? row.path.slice(row.resource_root.length + 1) : row.path
          if (rel !== component.renderer_path || !row.resource_asset_paths?.includes(resource.asset_path)) return false
        } else if (row.resource_root !== legacyRoot) return false
        return Array.isArray(component.materials) && row.materials.length === component.materials.length && row.materials.every((material, slot) => material.slot === slot && material.name === component.materials[slot])
      })
      if (renderers.length !== 1) { if (renderers.length > 1) failures++; continue }
      const renderer = renderers[0]
      matched++
      // Projected shadow geometry is a game shader effect, not character skin.
      const projected = renderer.materials.some((material) => material.shader?.asset_paths?.some((path) => path.includes('overlayshadow.shader')))
      const mesh = pack.data.meshes[renderer.previewMesh]
      const rule = (pkg.manifest.component_rules || []).find((row) => row.target === componentIndex)
      const keepChoices = rule ? rule.candidates.filter((row) => row.operation === 'keep').map((row) => ({ when: row.when, overrides: row.material_overrides }))
        : (pkg.manifest.appearances?.length ? pkg.manifest.appearances.flatMap((appearance) => {
          const value = appearance.components?.find((row) => row.target === componentIndex)
          return !value || value.operation === 'keep' ? [{ appearance: appearance.id, overrides: value?.material_overrides }] : []
        }) : [{}])
      if (!projected && mesh && keepChoices.length) {
        const count = mesh.vertex_count
        if (!Number.isSafeInteger(count) || count < 1 || count > 1048576) throw new Error('Invalid native vertex count')
        const positions = pack.read(mesh.payloads.positions, count * 12)
        for (let offset = 0; offset < positions.length; offset += 4) if (!Number.isFinite(positions.readFloatLE(offset))) throw new Error('Nonfinite native position')
        const uvs = pack.read(mesh.payloads.uvs, count * 8)
        for (let offset = 0; offset < uvs.length; offset += 4) if (!Number.isFinite(uvs.readFloatLE(offset))) throw new Error('Nonfinite native UV')
        const indices = pack.read(mesh.payloads.indices)
        if (indices.length % 4) throw new Error('Invalid native index buffer')
        const draws = []
        // Exported indices are concatenated uint32 triangle lists with baseVertex
        // already applied. Source first_byte/base_vertex belong to the game buffer.
        let first = 0
        for (const [slot, sub] of mesh.submeshes.entries()) {
          const size = sub.index_count
          if (!Number.isSafeInteger(size) || size < 1 || size % 3 || (first + size) * 4 > indices.length || (sub.topology && sub.topology !== 'Triangles')) throw new Error('Invalid native submesh')
          const data = Buffer.alloc(size * 4)
          for (let i = 0; i < size; i++) {
            const vertex = indices.readUInt32LE((first + i) * 4)
            if (vertex < 0 || vertex >= count) throw new Error('Native index outside vertex buffer')
            data.writeUInt32LE(vertex, i * 4)
          }
          first += size
          const binding = baseTexture(renderer, slot), texture = textureFor(pack, binding)
          const indicesBlob = addBlob('indices', data)
          for (const choice of keepChoices) {
            const override = choice.overrides?.find((row) => row.material_slot === slot && row.material_name === renderer.materials[slot]?.name)
            const authored = override?.textures?.filter(Number.isInteger).find((index) => scene.textures[index]?.name === binding?.name)
              ?? override?.textures?.filter(Number.isInteger).find((index) => /(?:_D|_BaseMap)$/i.test(scene.textures[index]?.name || ''))
            const selected = authored ?? texture
            draws.push({ first: 0, count: size, indicesBlob, textures: selected === null ? [] : [selected], material: renderer.materials[slot]?.name || null, when: choice.when || null, appearance: choice.appearance || null, uvTransform: uvFor(binding) })
          }
        }
        if (first * 4 !== indices.length) throw new Error('Native submesh inventory does not cover indices')
        const index = scene.meshes.length
        scene.meshes.push({ index, name: component.mesh_name, native: true, component: componentIndex, vertexCount: count, indexSize: 4, positionsBlob: addBlob('positions', positions), uvsBlob: addBlob('uvs', uvs), draws })
        scene.nativeMeshes.push({ index, target: componentIndex })
        report.meshes++
      }
      // Fill only an absent albedo. An authored package texture takes priority.
      const replacements = new Set()
      for (const rule of pkg.manifest.component_rules || []) if (rule.target === componentIndex) for (const candidate of rule.candidates || []) if (candidate.operation === 'replace') replacements.add(candidate.mesh)
      for (const appearance of pkg.manifest.appearances || []) for (const value of appearance.components || []) if (value.target === componentIndex && value.operation === 'replace') replacements.add(value.mesh)
      for (const meshIndex of replacements) {
        const replacement = scene.meshes[meshIndex]
        if (!replacement || replacement.native) continue
        for (const draw of replacement.draws) {
          if (draw.textures.length) continue
          const slot = renderer.materials.findIndex((material) => material.name === draw.material)
          if (slot < 0) continue
          const binding = baseTexture(renderer, slot), texture = textureFor(pack, binding)
          if (texture !== null) { draw.textures = [texture]; draw.uvTransform = uvFor(binding) }
        }
      }
    } catch (error) { failures++; report.message = error.message }
  }
  report.status = matched ? (failures ? 'partial' : 'ready') : (failures ? 'invalid' : 'unmatched')
  report.reason = failures ? 'invalid_character' : matched ? 'ready' : 'identity_mismatch'
  scene.stats = { ...scene.stats, meshes: scene.meshes.length, vertices: scene.meshes.reduce((sum, mesh) => sum + mesh.vertexCount, 0), draws: scene.meshes.reduce((sum, mesh) => sum + mesh.draws.length, 0), textures: scene.textures.length, previewableTextures: scene.textures.filter((texture) => texture.blob).length }
  return result
}
