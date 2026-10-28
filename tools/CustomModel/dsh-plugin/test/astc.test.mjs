import assert from 'node:assert/strict'
import { test } from 'node:test'
import { readFileSync } from 'node:fs'
import vm from 'node:vm'

const source = readFileSync(new URL('../src/client/index.js', import.meta.url), 'utf8')
  .replace(/^export const /gm, 'const ').replace(/^export function /gm, 'function ')
const requests = []
const context = vm.createContext({
  require: () => ({ createElement: () => null }),
  fetch: async (url) => { requests.push(url); return { ok: true, arrayBuffer: async () => new ArrayBuffer(16) } },
})
vm.runInContext(source + '\n;globalThis.StageForTest = Stage;', context)
const Stage = context.StageForTest

function stage(supported = true, gpuError = false) {
  const calls = [], uploads = []
  const constants = { NO_ERROR: 0, TEXTURE_2D: 3553, UNPACK_ALIGNMENT: 3317, LINEAR: 9729, TEXTURE_MIN_FILTER: 10241, TEXTURE_MAG_FILTER: 10240, TEXTURE_WRAP_S: 10242, TEXTURE_WRAP_T: 10243, REPEAT: 10497 }
  const extension = { COMPRESSED_SRGB8_ALPHA8_ASTC_6x6_KHR: 37844, COMPRESSED_RGBA_ASTC_6x6_KHR: 37812 }
  const gl = { ...constants, getExtension: () => supported ? extension : null, getError: () => gpuError ? 1280 : 0, createTexture: () => ({ texture: true }), compressedTexImage2D: (...args) => uploads.push(args) }
  for (const name of ['bindTexture', 'pixelStorei', 'texParameteri', 'deleteTexture', 'generateMipmap', 'texImage2D']) gl[name] = (...args) => calls.push({ name, args })
  const texture = { index: 0, family: 'astc', block: 6, width: 6, height: 6, srgb: true, blob: 'texture' }
  const value = { gl, textureUnits: new Map(), sceneId: 'local', scene: { textures: [texture] }, meshes: [{ draws: [{ texture: 0 }] }], reportStatus() {}, uploadTexture: Stage.prototype.uploadTexture, decoder: { request() { throw new Error('ASTC must not reach a BC worker') } } }
  return { value, texture, calls, uploads }
}

test('ASTC uses compressed GPU upload with the correct sRGB format', async () => {
  requests.length = 0
  const { value, uploads, calls } = stage()
  await Stage.prototype.ensureTextures.call(value)
  assert.equal(requests.length, 1)
  assert.equal(uploads.length, 1)
  assert.equal(uploads[0][2], 37844)
  assert.equal(value.textureUnits.size, 1)
  assert.equal(calls.some((call) => call.name === 'texImage2D' || call.name === 'generateMipmap'), false, 'compressed ASTC is never decoded or mip-generated')
})

test('ASTC linear textures use the non-sRGB GPU format', () => {
  const { value, texture, uploads } = stage(); texture.srgb = false
  Stage.prototype.uploadTexture.call(value, 0, texture, new Uint8Array(16))
  assert.equal(uploads[0][2], 37812)
})

test('unsupported ASTC is reported by capability without fetching or decode errors', async () => {
  requests.length = 0
  const { value, uploads } = stage(false)
  await Stage.prototype.ensureTextures.call(value)
  assert.equal(requests.length, 0)
  assert.equal(uploads.length, 0)
  assert.equal(value.textureUnits.size, 0)
  assert.equal(value.textureErrors?.size || 0, 0)
})

test('failed ASTC GPU upload does not register an invalid texture', async () => {
  const { value, calls } = stage(true, true)
  await Stage.prototype.ensureTextures.call(value)
  assert.equal(value.textureUnits.size, 0)
  assert.equal(value.textureErrors.size, 1)
  assert.equal(calls.some((call) => call.name === 'deleteTexture'), true)
})
