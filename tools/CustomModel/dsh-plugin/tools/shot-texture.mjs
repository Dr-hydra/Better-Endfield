#!/usr/bin/env node
/**
 * Decode one texture from a package and write a PNG.
 *
 * This exists to verify the BC7 path end to end against real game data: the
 * decoder is the only part of the preview that cannot be checked by reading
 * numbers, and a wrong partition table shows up immediately as noise.
 *
 * Usage: node tools/shot-texture.mjs <package.bem> [textureIndex] [maxWidth]
 */
import { writeFileSync, mkdirSync } from 'node:fs'
import { dirname } from 'node:path'
import { deflateSync } from 'node:zlib'
import { readPackage } from '../lib/bem/container.js'
import { buildScene, textureFamily } from '../lib/bem/scene.js'
import { decodeBC1, decodeBC3, decodeBC4, decodeBC5, decodeBC7 } from '../lib/vendor/bcn.js'

const [, , packagePath, textureArg, widthArg] = process.argv
if (!packagePath) {
  console.error('usage: node tools/shot-texture.mjs <package.bem> [textureIndex] [maxWidth]')
  process.exit(2)
}

const DECODERS = { bc1: decodeBC1, bc3: decodeBC3, bc4: decodeBC4, bc5: decodeBC5, bc7: decodeBC7 }

function pngEncode(width, height, rgba) {
  const raw = Buffer.alloc((width * 4 + 1) * height)
  for (let y = 0; y < height; y += 1) {
    raw[y * (width * 4 + 1)] = 0
    rgba.copy
      ? rgba.copy(raw, y * (width * 4 + 1) + 1, y * width * 4, (y + 1) * width * 4)
      : raw.set(rgba.subarray(y * width * 4, (y + 1) * width * 4), y * (width * 4 + 1) + 1)
  }
  const chunks = []
  const chunk = (type, data) => {
    const length = Buffer.alloc(4)
    length.writeUInt32BE(data.length)
    const body = Buffer.concat([Buffer.from(type, 'latin1'), data])
    const crcTable = pngEncode.crcTable || (pngEncode.crcTable = (() => {
      const table = new Uint32Array(256)
      for (let n = 0; n < 256; n += 1) {
        let c = n
        for (let k = 0; k < 8; k += 1) c = c & 1 ? 0xedb88320 ^ (c >>> 1) : c >>> 1
        table[n] = c >>> 0
      }
      return table
    })())
    let crc = 0xffffffff
    for (const byte of body) crc = crcTable[(crc ^ byte) & 0xff] ^ (crc >>> 8)
    const crcBuffer = Buffer.alloc(4)
    crcBuffer.writeUInt32BE((crc ^ 0xffffffff) >>> 0)
    chunks.push(length, body, crcBuffer)
  }
  const header = Buffer.alloc(13)
  header.writeUInt32BE(width, 0)
  header.writeUInt32BE(height, 4)
  header[8] = 8
  header[9] = 6
  chunk('IHDR', header)
  chunk('IDAT', deflateSync(raw, { level: 6 }))
  chunk('IEND', Buffer.alloc(0))
  return Buffer.concat([Buffer.from([0x89, 0x50, 0x4e, 0x47, 0x0d, 0x0a, 0x1a, 0x0a]), ...chunks])
}

function boxDownsample(width, height, rgba, maxWidth) {
  if (width <= maxWidth) return { width, height, data: rgba }
  const scale = width / maxWidth
  const outWidth = maxWidth
  const outHeight = Math.max(1, Math.round(height / scale))
  const out = Buffer.alloc(outWidth * outHeight * 4)
  for (let y = 0; y < outHeight; y += 1) {
    for (let x = 0; x < outWidth; x += 1) {
      const x0 = Math.floor(x * scale)
      const x1 = Math.min(width, Math.floor((x + 1) * scale))
      const y0 = Math.floor(y * scale)
      const y1 = Math.min(height, Math.floor((y + 1) * scale))
      let r = 0; let g = 0; let b = 0; let a = 0; let n = 0
      for (let sy = y0; sy < y1; sy += 1) {
        for (let sx = x0; sx < x1; sx += 1) {
          const at = (sy * width + sx) * 4
          r += rgba[at]; g += rgba[at + 1]; b += rgba[at + 2]; a += rgba[at + 3]
          n += 1
        }
      }
      const at = (y * outWidth + x) * 4
      out[at] = r / n; out[at + 1] = g / n; out[at + 2] = b / n; out[at + 3] = a / n
    }
  }
  return { width: outWidth, height: outHeight, data: out }
}

const pkg = readPackage(packagePath)
const { scene } = buildScene(pkg, { path: packagePath })
const index = textureArg === undefined ? 0 : Number(textureArg)
const texture = scene.textures[index]
if (!texture) {
  console.error(`no texture ${index}; the package has ${scene.textures.length}`)
  process.exit(2)
}
const decoder = DECODERS[texture.family]
if (!decoder) {
  console.error(`texture ${index} is ${texture.family}; no decoder here`)
  process.exit(2)
}

const info = textureFamily(texture.format)
const blockBytes = Math.ceil(texture.width / info.block) * Math.ceil(texture.height / info.block) * info.bytesPerBlock
const entry = pkg.manifest.textures[index]
const payload = pkg.payload(entry.payload).subarray(0, blockBytes)

const started = Date.now()
const rgba = decoder(new DataView(payload.buffer, payload.byteOffset, payload.byteLength), texture.width, texture.height)
const elapsed = Date.now() - started

const maxWidth = Number(widthArg || 640)
const scaled = boxDownsample(texture.width, texture.height, Buffer.from(rgba.buffer), maxWidth)
const outPath = `${dirname(packagePath)}/preview-texture-${index}.png`.replace(/\\/g, '/')
mkdirSync(dirname(outPath), { recursive: true })
writeFileSync(outPath, pngEncode(scaled.width, scaled.height, scaled.data))

// A flat or single-colour result means the decode produced nothing usable.
let min = 255
let max = 0
let sum = 0
for (let i = 0; i < rgba.length; i += 4) {
  const value = rgba[i]
  if (value < min) min = value
  if (value > max) max = value
  sum += value
}
console.log(`texture ${index}: ${texture.name} ${texture.width}x${texture.height} ${texture.family}`)
console.log(`decoded ${rgba.length / 1048576} Mpx-bytes in ${elapsed} ms`)
console.log(`red channel min=${min} max=${max} mean=${(sum / (rgba.length / 4)).toFixed(1)}`)
console.log(`wrote ${outPath} (${scaled.width}x${scaled.height})`)
