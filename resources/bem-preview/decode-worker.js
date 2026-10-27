/**
 * Texture decode worker.
 *
 * BC7 decoding of a 4096x2048 base level is ~8.4M pixels; doing it on the main
 * thread freezes the tab for seconds. The worker fetches the compressed block
 * blob from the same origin, decodes, and transfers the RGBA buffer back.
 *
 * Module worker: it imports the vendored decoder, which is served next to it.
 */
import { decodeBC1, decodeBC3, decodeBC4, decodeBC5, decodeBC7 } from './bcn.js'

const DECODERS = {
  bc1: decodeBC1,
  bc3: decodeBC3,
  bc4: decodeBC4,
  bc5: decodeBC5,
  bc7: decodeBC7,
}

self.onmessage = async (event) => {
  const { id, url, family, width, height } = event.data || {}
  try {
    const decoder = DECODERS[family]
    if (!decoder) throw new Error(`no decoder for ${family}`)
    const response = await fetch(url)
    if (!response.ok) throw new Error(`blob fetch failed: ${response.status}`)
    const buffer = await response.arrayBuffer()
    const rgba = decoder(new DataView(buffer), width, height)
    self.postMessage({ id, ok: true, width, height, data: rgba.buffer }, [rgba.buffer])
  } catch (error) {
    self.postMessage({ id, ok: false, error: String((error && error.message) || error) })
  }
}
