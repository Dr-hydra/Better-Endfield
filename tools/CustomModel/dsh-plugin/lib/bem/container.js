/**
 * Read-only BEM container reader.
 *
 * Layout (little endian):
 *   header   <8s H H I Q Q I I  = 40 bytes
 *            magic, major, minor, headerSize, fileSize, manifestSize,
 *            payloadCount, flags
 *   manifest manifestSize bytes of UTF-8 JSON
 *   table    32 bytes per payload: codec, reserved, offset, stored, decoded
 *   blocks   concatenated payload blocks
 *
 * This module only reads. Every write path, every cross-field rule and every
 * version-specific limit lives in the Python toolchain; duplicating them here
 * would create a second authority for what a legal package is. What this reader
 * does enforce is the part it physically depends on: magic, header arithmetic,
 * directory bounds and payload length, because a wrong offset would silently
 * render garbage instead of failing.
 */
import { readFileSync, statSync } from 'node:fs'
import { zstdDecompressSync } from 'node:zlib'

export const MAGIC = Buffer.from([0x42, 0x45, 0x4d, 0x00, 0x50, 0x4b, 0x47, 0x00])
export const HEADER_SIZE = 40
export const ENTRY_SIZE = 32
export const MAX_HEADER_MINOR = 5

/** Raised when a file is not a BEM container this reader understands. */
export class ContainerError extends Error {
  constructor(code, message, detail) {
    super(message)
    this.name = 'ContainerError'
    this.code = code
    if (detail) this.detail = detail
  }
}

const FORMAT_VERSION = { 0: '1.0', 1: '1.1', 2: '1.2', 3: '1.3', 4: '1.4', 5: '1.5' }

/**
 * Open a package and return its manifest plus a lazy payload accessor.
 *
 * Payloads are decoded on demand: a 10 MB package holds ~30 MB of decoded
 * texture blocks, and a preview never touches every texture.
 */
export function readPackage(path) {
  const stats = statSync(path)
  const buf = readFileSync(path)
  if (buf.length < HEADER_SIZE) {
    throw new ContainerError('TRUNCATED', `${path}: file is shorter than a BEM header`)
  }
  if (!buf.subarray(0, 8).equals(MAGIC)) {
    const head = buf.subarray(0, 4).toString('latin1')
    if (head === 'PK\u0003\u0004') {
      throw new ContainerError('ZIP_CONTAINER',
        `${path}: this is a ZIP container; preview needs the binary .bem form`,
        { hint: 'pack the package with pack-zip only for distribution, preview the binary package' })
    }
    throw new ContainerError('NOT_BEM', `${path}: not a BEM container (magic mismatch)`)
  }

  const major = buf.readUInt16LE(8)
  const minor = buf.readUInt16LE(10)
  const headerSize = buf.readUInt32LE(12)
  const fileSize = Number(buf.readBigUInt64LE(16))
  const manifestSize = Number(buf.readBigUInt64LE(24))
  const count = buf.readUInt32LE(32)
  const flags = buf.readUInt32LE(36)

  if (major !== 1 || minor > MAX_HEADER_MINOR) {
    throw new ContainerError('UNSUPPORTED_VERSION',
      `${path}: unsupported BEM header ${major}.${minor}`)
  }
  if (headerSize !== HEADER_SIZE || flags !== 0) {
    throw new ContainerError('BAD_HEADER', `${path}: unexpected header shape`)
  }
  if (fileSize !== buf.length || !(manifestSize > 0) || manifestSize > 4 * 1024 * 1024) {
    throw new ContainerError('BAD_SIZES', `${path}: header sizes disagree with the file`)
  }
  const manifestEnd = HEADER_SIZE + manifestSize
  const tableEnd = manifestEnd + count * ENTRY_SIZE
  if (tableEnd > buf.length) {
    throw new ContainerError('BAD_DIRECTORY', `${path}: payload directory runs past EOF`)
  }

  let manifest
  try {
    manifest = JSON.parse(buf.subarray(HEADER_SIZE, manifestEnd).toString('utf8'))
  } catch (error) {
    throw new ContainerError('BAD_MANIFEST', `${path}: manifest is not valid JSON`, { cause: String(error) })
  }

  const entries = []
  let expect = tableEnd
  for (let i = 0; i < count; i += 1) {
    const base = manifestEnd + i * ENTRY_SIZE
    const codec = buf.readUInt32LE(base)
    const reserved = buf.readUInt32LE(base + 4)
    const offset = Number(buf.readBigUInt64LE(base + 8))
    const stored = Number(buf.readBigUInt64LE(base + 16))
    const decoded = Number(buf.readBigUInt64LE(base + 24))
    if (codec > 1 || reserved !== 0 || offset !== expect || stored < 1 || decoded < 1 || offset + stored > buf.length) {
      throw new ContainerError('BAD_DIRECTORY', `${path}: payload entry ${i} is inconsistent`)
    }
    expect = offset + stored
    entries.push({ codec, offset, stored, decoded })
  }
  if (expect !== buf.length) {
    throw new ContainerError('TRAILING_BYTES', `${path}: ${buf.length - expect} trailing bytes after the last payload`)
  }

  const cache = new Map()
  function payload(index) {
    if (!Number.isInteger(index) || index < 0 || index >= entries.length) {
      throw new ContainerError('PAYLOAD_RANGE', `payload ${index} is outside 0..${entries.length - 1}`)
    }
    const hit = cache.get(index)
    if (hit) return hit
    const entry = entries[index]
    const raw = buf.subarray(entry.offset, entry.offset + entry.stored)
    let data
    if (entry.codec === 1) {
      try {
        data = zstdDecompressSync(raw)
      } catch (error) {
        throw new ContainerError('ZSTD_FAILED', `payload ${index} failed to decompress`, { cause: String(error) })
      }
    } else {
      data = Buffer.from(raw)
    }
    if (data.length !== entry.decoded) {
      throw new ContainerError('PAYLOAD_LENGTH',
        `payload ${index} decoded to ${data.length} bytes, the directory says ${entry.decoded}`)
    }
    cache.set(index, data)
    return data
  }

  return {
    path,
    size: stats.size,
    minor,
    formatVersion: FORMAT_VERSION[minor] || `1.${minor}`,
    manifest,
    payloadCount: count,
    payload,
    entry(i) { return entries[i] },
  }
}

export { FORMAT_VERSION }
