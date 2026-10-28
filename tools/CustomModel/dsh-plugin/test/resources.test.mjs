import assert from 'node:assert/strict'
import { test } from 'node:test'
import { mkdtempSync, mkdirSync, writeFileSync, readFileSync, rmSync } from 'node:fs'
import { tmpdir } from 'node:os'
import { join } from 'node:path'
import { readPreviewRepository, resolvePreviewResources, previewResourcesStatus, configurePreviewResources } from '../lib/bem/resources.js'
import { completeNativeScene } from '../lib/bem/native.js'
import { initialSelection, nativeMeshesForSelection } from '../lib/bem/preview.js'

function fixture(platform = 'windows', version = '1.6.3.2') {
  const temporary = mkdtempSync(join(tmpdir(), 'dsh-resources-'))
  const root = join(temporary, 'preview', platform, version)
  const prefix = 'characters/character/default/'
  const files = {}
  const put = (name, value) => {
    const bytes = Buffer.isBuffer(value) ? value : Buffer.from(JSON.stringify(value))
    mkdirSync(join(root, name, '..'), { recursive: true })
    writeFileSync(join(root, name), bytes); files[name] = bytes.length
  }
  const pack = {
    schema: 1, kind: 'native-head-preview-prototype', character: 'character',
    meshes: [{ vertex_count: 3, submeshes: [{ first_byte: 998, base_vertex: 999, index_count: 3, topology: 'Triangles' }], payloads: { positions: 'positions.bin', uvs: 'uvs.bin', indices: 'indices.bin' } }],
    textures: [{ id: 'base', name: 'head_D', width: 6, height: 6, mips: 1, format: platform === 'android' ? 'ASTC_6x6' : 'BC7', payload: 'texture.bin', srgb: true }],
    renderers: ['body', 'head'].map((name) => ({ name, path: 'character_postmodel/Mesh/' + name, resource_root: 'character_postmodel', resource_asset_paths: ['assets/character_postmodel.prefab'], previewMesh: 0, materials: [{ slot: 0, name: 'skin', shader: { asset_paths: [] }, textures: [{ property: '_BaseMap', id: 'base', name: 'head_D', scale: { X: 2, Y: 1 }, offset: { X: 0.25, Y: 0 } }] }] })),
  }
  put(prefix + 'pack.json', pack)
  put(prefix + 'positions.bin', Buffer.from(new Float32Array([0, 0, 0, 1, 0, 0, 0, 1, 0]).buffer))
  put(prefix + 'uvs.bin', Buffer.from(new Float32Array([0, 0, 1, 0, 0, 1]).buffer))
  put(prefix + 'indices.bin', Buffer.from(new Uint32Array([0, 1, 2]).buffer))
  put(prefix + 'texture.bin', Buffer.alloc(platform === 'android' ? 16 : 64))
  const entry = { character: 'character', path: prefix.slice(0, -1), resourceRoots: ['character_postmodel'], assetPaths: ['assets/character_postmodel.prefab'] }
  put('characters.json', { schemaVersion: 1, characters: [entry] })
  const save = () => { put(prefix + 'pack.json', pack); put('characters.json', { schemaVersion: 1, characters: [entry] }); writeFileSync(join(root, 'manifest.json'), JSON.stringify({ schemaVersion: 1, kind: 'betterendfield-resource-pack', packageType: 'preview', platform, version, gameVersion: version.split('.').slice(0, 3).join('.'), files })) }
  save()
  const manifest = {
    target: { character_id: 'character', world_resource: 'character_postmodel', components: ['body', 'head'].map((mesh_name) => ({ mesh_name, materials: ['skin'] })) },
    meshes: [{}],
    component_rules: [
      { target: 0, candidates: [{ operation: 'replace', mesh: 0, when: { not: { eq: ['mode', 'hide'] } } }, { operation: 'hide', when: { eq: ['mode', 'hide'] } }] },
      { target: 1, candidates: [{ operation: 'keep', when: { eq: ['mode', 'keep'] } }, { operation: 'hide', when: { eq: ['mode', 'hide'] } }, { operation: 'replace', mesh: 0, when: { eq: ['mode', 'replace'] } }] },
    ],
  }
  const result = () => ({ scene: { meshes: [{ index: 0, name: 'replacement', vertexCount: 3, draws: [{ count: 3, textures: [0], material: 'skin' }] }], textures: [{ index: 0, name: 'authored_D', blob: 'authored' }], optionGroups: [{ id: 'mode', default: 'keep', choices: [{ id: 'keep' }, { id: 'hide' }, { id: 'replace' }] }], componentRules: manifest.component_rules, appearances: [], stats: {} }, blobs: new Map() })
  const complete = () => completeNativeScene({ manifest }, result(), resolvePreviewResources(root, platform))
  return { temporary, root, prefix, platform, pack, entry, files, put, save, manifest, result, complete, cleanup: () => rmSync(temporary, { recursive: true, force: true }) }
}

function local(label, body, platform) { test(label, () => { const f = fixture(platform); try { body(f) } finally { f.cleanup() } }) }

local('BC7 pack is readable without a network request', (f) => {
  const original = globalThis.fetch
  globalThis.fetch = () => { throw new Error('Unexpected network access') }
  try { const status = previewResourcesStatus(f.root, 'windows'); assert.equal(status.status, 'ready'); assert.equal(status.choices.length, 2); assert.equal(status.characters, 1) } finally { globalThis.fetch = original }
})
local('ASTC can be explicitly selected even on a Windows DSH host', (f) => {
  assert.equal(configurePreviewResources(f.root, 'android').ok, true)
  const { scene, blobs } = f.complete()
  assert.equal(scene.nativeCompletion.status, 'ready')
  const texture = scene.textures.find((item) => item.native)
  assert.equal(texture.family, 'astc'); assert.equal(blobs.get(texture.blob).get().length, 16)
}, 'android')
local('selected format is validated rather than silently changed', (f) => {
  assert.equal(resolvePreviewResources(f.root, 'android').reason, 'invalid_package')
  assert.throws(() => readPreviewRepository(f.root, 'android'), /selected/)
})
local('shared resource root honors an exact version pin', (f) => {
  writeFileSync(join(f.temporary, 'state.json'), JSON.stringify({ 'preview.windows': '1.6.3.2' }))
  assert.equal(resolvePreviewResources(f.temporary, 'windows').repository.manifest.version, '1.6.3.2')
  writeFileSync(join(f.temporary, 'state.json'), JSON.stringify({ 'preview.windows': '1.6.3.0' }))
  assert.equal(resolvePreviewResources(f.temporary, 'windows').reason, 'pinned_version_missing')
})
local('only untouched keep geometry is completed and hidden/replaced states exclude it', (f) => {
  const { scene, blobs } = f.complete()
  assert.equal(scene.nativeCompletion.status, 'ready')
  assert.equal(scene.nativeCompletion.meshes, 1, 'always replaced body must not load native geometry')
  assert.deepEqual(initialSelection(scene).meshes, [0, 1])
  assert.deepEqual(nativeMeshesForSelection(scene, { mode: 'hide' }), [])
  assert.deepEqual(nativeMeshesForSelection(scene, { mode: 'replace' }), [])
  const native = scene.meshes[1]
  assert.deepEqual(native.draws[0].uvTransform, [2, 1, 0.25, 0])
  assert.deepEqual([...new Uint32Array(blobs.get(native.draws[0].indicesBlob).data.buffer.slice(blobs.get(native.draws[0].indicesBlob).data.byteOffset, blobs.get(native.draws[0].indicesBlob).data.byteOffset + 12))], [0, 1, 2], 'export already normalized source offsets')
})
local('appearance keep/hide/replace selects the correct original mesh', (f) => {
  f.manifest.component_rules = []
  f.manifest.appearances = [{ id: 'keep', components: [{ target: 0, operation: 'replace', mesh: 0 }, { target: 1, operation: 'keep' }] }, { id: 'hide', components: [{ target: 0, operation: 'hide' }, { target: 1, operation: 'hide' }] }]
  const result = f.result(); result.scene.componentRules = []; result.scene.appearances = f.manifest.appearances.map((row) => ({ ...row, meshes: row.id === 'keep' ? [0] : [] })); result.scene.defaultAppearance = 'keep'
  const { scene } = completeNativeScene({ manifest: f.manifest }, result, resolvePreviewResources(f.root, 'windows'))
  assert.deepEqual(initialSelection(scene).meshes, [0, 1])
  assert.deepEqual(nativeMeshesForSelection(scene, {}, 'hide'), [])
})
local('material slot names must match exactly', (f) => {
  f.manifest.target.components.forEach((row) => { row.materials = ['wrong'] })
  const { scene } = f.complete(); assert.equal(scene.nativeCompletion.reason, 'identity_mismatch'); assert.equal(scene.meshes.length, 1)
})
local('ambiguous character variants are not guessed', (f) => {
  f.put('characters.json', { schemaVersion: 1, characters: [f.entry, f.entry] })
  writeFileSync(join(f.root, 'manifest.json'), JSON.stringify({ ...JSON.parse(readFileSync(join(f.root, 'manifest.json'), 'utf8')), files: f.files }))
  const { scene } = f.complete(); assert.equal(scene.nativeCompletion.status, 'invalid'); assert.equal(scene.meshes.length, 1)
})
local('explicit targets require full asset and renderer-relative identities', (f) => {
  const target = f.manifest.target; delete target.character_id; target.kind = 'character'; target.id = 'character'
  target.resources = [{ id: 'world', asset_path: 'assets/character_postmodel.prefab', platforms: ['windows'] }]
  target.components.forEach((row) => { row.resource = 'world'; row.renderer_path = 'Mesh/' + row.mesh_name })
  assert.equal(f.complete().scene.nativeCompletion.status, 'ready')
  target.resources[0].asset_path = 'assets/wrong.prefab'; assert.equal(f.complete().scene.nativeCompletion.reason, 'identity_mismatch')
})
local('out-of-range native geometry degrades without losing authored geometry', (f) => {
  f.put(f.prefix + 'indices.bin', Buffer.from(new Uint32Array([0, 1, 99]).buffer)); f.save()
  const { scene } = f.complete(); assert.equal(scene.nativeCompletion.status, 'partial'); assert.equal(scene.meshes.length, 1)
})
local('unrelated character payloads are not loaded', (f) => {
  f.entry.character = 'unrelated'; f.entry.path = 'missing/character'; f.save()
  const { scene } = f.complete(); assert.equal(scene.nativeCompletion.reason, 'character_not_covered'); assert.equal(scene.meshes.length, 1)
})
local('resource paths cannot escape their directory', (f) => {
  const repository = readPreviewRepository(f.root)
  assert.throws(() => repository.read('../outside.json'), /Invalid/)
  assert.throws(() => repository.read('/outside.json'), /Invalid/)
})
local('ASTC resources may complete a Windows-target explicit package', (f) => {
  const target = f.manifest.target; delete target.character_id; target.kind = 'character'; target.id = 'character'
  target.resources = [{ id: 'world', asset_path: 'assets/character_postmodel.prefab', platforms: ['windows'] }]
  target.components.forEach((row) => { row.resource = 'world'; row.renderer_path = 'Mesh/' + row.mesh_name })
  assert.equal(f.complete().scene.nativeCompletion.status, 'ready')
}, 'android')
local('bad inventories are rejected without replacing active package meshes', (f) => {
  f.files['characters.json']++
  writeFileSync(join(f.root, 'manifest.json'), JSON.stringify({ ...JSON.parse(readFileSync(join(f.root, 'manifest.json'), 'utf8')), files: f.files }))
  const { scene } = f.complete(); assert.equal(scene.nativeCompletion.reason, 'invalid_package'); assert.equal(scene.meshes.length, 1)
})
local('keep material override albedo takes priority over original texture', (f) => {
  f.manifest.component_rules[1].candidates[0].material_overrides = [{ material_slot: 0, material_name: 'skin', textures: [0] }]
  const { scene } = f.complete(); assert.deepEqual(scene.meshes[1].draws[0].textures, [0])
})
