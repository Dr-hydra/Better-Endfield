/**
 * BC1/BC2/BC3/BC4/BC5/BC7 block-compression decoders.
 *
 * Vendored verbatim from @bis-toolkit/bcn 1.0.2 (dist/index.js), which is a
 * direct port of BCnEncoder.NET (https://github.com/Nominom/BCnEncoder.NET).
 * License: GPL-3.0-or-later. This bundle is AGPL-3.0-only, which section 13 of
 * the AGPL permits combining with GPLv3-or-later code.
 *
 * No local modifications; re-vendor by replacing everything below the marker.
 */
// --- vendored source below ---
// src/utils.ts
var ColorRgb565 = class {
  constructor(r, g, b) {
    if (r !== void 0 && g !== void 0 && b !== void 0) {
      const r5 = r >> 3 & 31;
      const g6 = g >> 2 & 63;
      const b5 = b >> 3 & 31;
      this.data = r5 << 11 | g6 << 5 | b5;
    } else {
      this.data = 0;
    }
  }
  toColorRgb24() {
    const r5 = this.data >> 11 & 31;
    const g6 = this.data >> 5 & 63;
    const b5 = this.data & 31;
    const r = r5 << 3 | r5 >> 2;
    const g = g6 << 2 | g6 >> 4;
    const b = b5 << 3 | b5 >> 2;
    return { r, g, b };
  }
};
function interpolateHalf(c0, c1) {
  return {
    r: (c0.r + c1.r) / 2 | 0,
    g: (c0.g + c1.g) / 2 | 0,
    b: (c0.b + c1.b) / 2 | 0
  };
}
function interpolateThird(c0, c1, step) {
  if (step === 1) {
    return {
      r: (2 * c0.r + c1.r) / 3 | 0,
      g: (2 * c0.g + c1.g) / 3 | 0,
      b: (2 * c0.b + c1.b) / 3 | 0
    };
  } else {
    return {
      r: (c0.r + 2 * c1.r) / 3 | 0,
      g: (c0.g + 2 * c1.g) / 3 | 0,
      b: (c0.b + 2 * c1.b) / 3 | 0
    };
  }
}
function interpolateByteFifth(e0, e1, step) {
  if (step === 1) return (4 * e0 + e1) / 5 | 0;
  if (step === 2) return (3 * e0 + 2 * e1) / 5 | 0;
  if (step === 3) return (2 * e0 + 3 * e1) / 5 | 0;
  return (e0 + 4 * e1) / 5 | 0;
}
function interpolateByteSeventh(e0, e1, step) {
  if (step === 1) return (6 * e0 + e1) / 7 | 0;
  if (step === 2) return (5 * e0 + 2 * e1) / 7 | 0;
  if (step === 3) return (4 * e0 + 3 * e1) / 7 | 0;
  if (step === 4) return (3 * e0 + 4 * e1) / 7 | 0;
  if (step === 5) return (2 * e0 + 5 * e1) / 7 | 0;
  return (e0 + 6 * e1) / 7 | 0;
}

// src/bc1.ts
function decodeBC1(data, width, height, useAlpha = false) {
  const rgba = new Uint8Array(width * height * 4);
  const blocksX = Math.ceil(width / 4);
  const blocksY = Math.ceil(height / 4);
  let offset = 0;
  for (let by = 0; by < blocksY; by++) {
    for (let bx = 0; bx < blocksX; bx++) {
      const color0Data = data.getUint16(offset, true);
      const color1Data = data.getUint16(offset + 2, true);
      const indices = data.getUint32(offset + 4, true);
      const color0 = new ColorRgb565();
      color0.data = color0Data;
      const color1 = new ColorRgb565();
      color1.data = color1Data;
      const c0 = color0.toColorRgb24();
      const c1 = color1.toColorRgb24();
      const hasAlphaOrBlack = color0Data <= color1Data;
      const actualUseAlpha = useAlpha && hasAlphaOrBlack;
      const colors = hasAlphaOrBlack ? [
        c0,
        c1,
        interpolateHalf(c0, c1),
        { r: 0, g: 0, b: 0 }
      ] : [
        c0,
        c1,
        interpolateThird(c0, c1, 1),
        interpolateThird(c0, c1, 2)
      ];
      for (let y = 0; y < 4; y++) {
        for (let x = 0; x < 4; x++) {
          const px = bx * 4 + x;
          const py = by * 4 + y;
          if (px < width && py < height) {
            const i = y * 4 + x;
            const colorIndex = indices >> i * 2 & 3;
            const color = colors[colorIndex];
            const dstIdx = (py * width + px) * 4;
            if (actualUseAlpha && colorIndex === 3) {
              rgba[dstIdx] = 0;
              rgba[dstIdx + 1] = 0;
              rgba[dstIdx + 2] = 0;
              rgba[dstIdx + 3] = 0;
            } else {
              rgba[dstIdx] = color.r;
              rgba[dstIdx + 1] = color.g;
              rgba[dstIdx + 2] = color.b;
              rgba[dstIdx + 3] = 255;
            }
          }
        }
      }
      offset += 8;
    }
  }
  return rgba;
}

// src/bc2.ts
function decodeBC2(data, width, height) {
  const rgba = new Uint8Array(width * height * 4);
  const blocksX = Math.ceil(width / 4);
  const blocksY = Math.ceil(height / 4);
  let offset = 0;
  for (let by = 0; by < blocksY; by++) {
    for (let bx = 0; bx < blocksX; bx++) {
      const alphaLow = data.getUint32(offset, true);
      const alphaHigh = data.getUint32(offset + 4, true);
      const color0Data = data.getUint16(offset + 8, true);
      const color1Data = data.getUint16(offset + 10, true);
      const indices = data.getUint32(offset + 12, true);
      const color0 = new ColorRgb565();
      color0.data = color0Data;
      const color1 = new ColorRgb565();
      color1.data = color1Data;
      const c0 = color0.toColorRgb24();
      const c1 = color1.toColorRgb24();
      const colors = [
        c0,
        c1,
        interpolateThird(c0, c1, 1),
        interpolateThird(c0, c1, 2)
      ];
      for (let y = 0; y < 4; y++) {
        for (let x = 0; x < 4; x++) {
          const px = bx * 4 + x;
          const py = by * 4 + y;
          if (px < width && py < height) {
            const i = y * 4 + x;
            const colorIndex = indices >> i * 2 & 3;
            const color = colors[colorIndex];
            const alphaIndex = i * 4;
            let alpha;
            if (alphaIndex < 32) {
              alpha = alphaLow >> alphaIndex & 15;
            } else {
              alpha = alphaHigh >> alphaIndex - 32 & 15;
            }
            alpha = alpha << 4 | alpha;
            const dstIdx = (py * width + px) * 4;
            rgba[dstIdx] = color.r;
            rgba[dstIdx + 1] = color.g;
            rgba[dstIdx + 2] = color.b;
            rgba[dstIdx + 3] = alpha;
          }
        }
      }
      offset += 16;
    }
  }
  return rgba;
}

// src/bc3.ts
function decodeAlphaBlock(alphaData) {
  const alpha = new Array(16);
  const alpha0 = Number(alphaData & 0xFFn);
  const alpha1 = Number(alphaData >> 8n & 0xFFn);
  const alphas = alpha0 > alpha1 ? [
    alpha0,
    alpha1,
    interpolateByteSeventh(alpha0, alpha1, 1),
    interpolateByteSeventh(alpha0, alpha1, 2),
    interpolateByteSeventh(alpha0, alpha1, 3),
    interpolateByteSeventh(alpha0, alpha1, 4),
    interpolateByteSeventh(alpha0, alpha1, 5),
    interpolateByteSeventh(alpha0, alpha1, 6)
  ] : [
    alpha0,
    alpha1,
    interpolateByteFifth(alpha0, alpha1, 1),
    interpolateByteFifth(alpha0, alpha1, 2),
    interpolateByteFifth(alpha0, alpha1, 3),
    interpolateByteFifth(alpha0, alpha1, 4),
    0,
    255
  ];
  for (let i = 0; i < 16; i++) {
    const bitOffset = 16 + i * 3;
    const index = Number(alphaData >> BigInt(bitOffset) & 0x7n);
    alpha[i] = alphas[index];
  }
  return alpha;
}
function decodeBC3(data, width, height) {
  const rgba = new Uint8Array(width * height * 4);
  const blocksX = Math.ceil(width / 4);
  const blocksY = Math.ceil(height / 4);
  let offset = 0;
  for (let by = 0; by < blocksY; by++) {
    for (let bx = 0; bx < blocksX; bx++) {
      const alphaBlock = data.getBigUint64(offset, true);
      const alphas = decodeAlphaBlock(alphaBlock);
      const color0Data = data.getUint16(offset + 8, true);
      const color1Data = data.getUint16(offset + 10, true);
      const indices = data.getUint32(offset + 12, true);
      const color0 = new ColorRgb565();
      color0.data = color0Data;
      const color1 = new ColorRgb565();
      color1.data = color1Data;
      const c0 = color0.toColorRgb24();
      const c1 = color1.toColorRgb24();
      const colors = [
        c0,
        c1,
        interpolateThird(c0, c1, 1),
        interpolateThird(c0, c1, 2)
      ];
      for (let y = 0; y < 4; y++) {
        for (let x = 0; x < 4; x++) {
          const px = bx * 4 + x;
          const py = by * 4 + y;
          if (px < width && py < height) {
            const i = y * 4 + x;
            const colorIndex = indices >> i * 2 & 3;
            const color = colors[colorIndex];
            const dstIdx = (py * width + px) * 4;
            rgba[dstIdx] = color.r;
            rgba[dstIdx + 1] = color.g;
            rgba[dstIdx + 2] = color.b;
            rgba[dstIdx + 3] = alphas[i];
          }
        }
      }
      offset += 16;
    }
  }
  return rgba;
}

// src/bc4.ts
function decodeComponentBlock(componentData) {
  const output = new Array(16);
  const c0 = Number(componentData & 0xFFn);
  const c1 = Number(componentData >> 8n & 0xFFn);
  const components = c0 > c1 ? [
    c0,
    c1,
    interpolateByteSeventh(c0, c1, 1),
    interpolateByteSeventh(c0, c1, 2),
    interpolateByteSeventh(c0, c1, 3),
    interpolateByteSeventh(c0, c1, 4),
    interpolateByteSeventh(c0, c1, 5),
    interpolateByteSeventh(c0, c1, 6)
  ] : [
    c0,
    c1,
    interpolateByteFifth(c0, c1, 1),
    interpolateByteFifth(c0, c1, 2),
    interpolateByteFifth(c0, c1, 3),
    interpolateByteFifth(c0, c1, 4),
    0,
    255
  ];
  for (let i = 0; i < 16; i++) {
    const bitOffset = 16 + i * 3;
    const index = Number(componentData >> BigInt(bitOffset) & 0x7n);
    output[i] = components[index];
  }
  return output;
}
function decodeBC4(data, width, height, channel = "r") {
  const rgba = new Uint8Array(width * height * 4);
  const blocksX = Math.ceil(width / 4);
  const blocksY = Math.ceil(height / 4);
  const channelMap = { r: 0, g: 1, b: 2, a: 3 };
  const outputChannel = channelMap[channel];
  let offset = 0;
  for (let by = 0; by < blocksY; by++) {
    for (let bx = 0; bx < blocksX; bx++) {
      const componentBlock = data.getBigUint64(offset, true);
      const components = decodeComponentBlock(componentBlock);
      for (let y = 0; y < 4; y++) {
        for (let x = 0; x < 4; x++) {
          const px = bx * 4 + x;
          const py = by * 4 + y;
          if (px < width && py < height) {
            const i = y * 4 + x;
            const dstIdx = (py * width + px) * 4;
            rgba[dstIdx] = 0;
            rgba[dstIdx + 1] = 0;
            rgba[dstIdx + 2] = 0;
            rgba[dstIdx + 3] = 255;
            rgba[dstIdx + outputChannel] = components[i];
          }
        }
      }
      offset += 8;
    }
  }
  return rgba;
}

// src/bc5.ts
function decodeComponentBlock2(componentData) {
  const output = new Array(16);
  const c0 = Number(componentData & 0xFFn);
  const c1 = Number(componentData >> 8n & 0xFFn);
  const components = c0 > c1 ? [
    c0,
    c1,
    interpolateByteSeventh(c0, c1, 1),
    interpolateByteSeventh(c0, c1, 2),
    interpolateByteSeventh(c0, c1, 3),
    interpolateByteSeventh(c0, c1, 4),
    interpolateByteSeventh(c0, c1, 5),
    interpolateByteSeventh(c0, c1, 6)
  ] : [
    c0,
    c1,
    interpolateByteFifth(c0, c1, 1),
    interpolateByteFifth(c0, c1, 2),
    interpolateByteFifth(c0, c1, 3),
    interpolateByteFifth(c0, c1, 4),
    0,
    255
  ];
  for (let i = 0; i < 16; i++) {
    const bitOffset = 16 + i * 3;
    const index = Number(componentData >> BigInt(bitOffset) & 0x7n);
    output[i] = components[index];
  }
  return output;
}
function decodeBC5(data, width, height, channel1 = "r", channel2 = "g") {
  const rgba = new Uint8Array(width * height * 4);
  const blocksX = Math.ceil(width / 4);
  const blocksY = Math.ceil(height / 4);
  const channelMap = { r: 0, g: 1, b: 2, a: 3 };
  const outputChannel1 = channelMap[channel1];
  const outputChannel2 = channelMap[channel2];
  let offset = 0;
  for (let by = 0; by < blocksY; by++) {
    for (let bx = 0; bx < blocksX; bx++) {
      const redBlock = data.getBigUint64(offset, true);
      const reds = decodeComponentBlock2(redBlock);
      const greenBlock = data.getBigUint64(offset + 8, true);
      const greens = decodeComponentBlock2(greenBlock);
      for (let y = 0; y < 4; y++) {
        for (let x = 0; x < 4; x++) {
          const px = bx * 4 + x;
          const py = by * 4 + y;
          if (px < width && py < height) {
            const i = y * 4 + x;
            const dstIdx = (py * width + px) * 4;
            rgba[dstIdx] = 0;
            rgba[dstIdx + 1] = 0;
            rgba[dstIdx + 2] = 0;
            rgba[dstIdx + 3] = 255;
            rgba[dstIdx + outputChannel1] = reds[i];
            rgba[dstIdx + outputChannel2] = greens[i];
          }
        }
      }
      offset += 16;
    }
  }
  return rgba;
}

// src/bc7.ts
var ByteHelper = class _ByteHelper {
  static extract(source, index, bitCount) {
    const mask = (1n << BigInt(bitCount)) - 1n;
    return Number(source >> BigInt(index) & mask);
  }
  static extractFrom128(low, high, index, bitCount) {
    if (index + bitCount <= 64) {
      return _ByteHelper.extract(low, index, bitCount);
    }
    if (index >= 64) {
      return _ByteHelper.extract(high, index - 64, bitCount);
    }
    const lowBitCount = 64 - index;
    const highBitCount = bitCount - lowBitCount;
    const lowValue = _ByteHelper.extract(low, index, lowBitCount);
    const highValue = _ByteHelper.extract(high, 0, highBitCount);
    return lowValue | highValue << lowBitCount;
  }
  static extract1(source, index) {
    return Number(source >> BigInt(index) & 1n);
  }
  static extract2(source, index) {
    return Number(source >> BigInt(index) & 3n);
  }
  static extract4(source, index) {
    return Number(source >> BigInt(index) & 15n);
  }
  static extract6(source, index) {
    return Number(source >> BigInt(index) & 63n);
  }
};
var COLOR_WEIGHTS_2 = [0, 21, 43, 64];
var COLOR_WEIGHTS_3 = [0, 9, 18, 27, 37, 46, 55, 64];
var COLOR_WEIGHTS_4 = [0, 4, 9, 13, 17, 21, 26, 30, 34, 38, 43, 47, 51, 55, 60, 64];
function interpolateByte(e0, e1, index, indexPrecision) {
  if (indexPrecision === 0) return e0;
  const weights = indexPrecision === 2 ? COLOR_WEIGHTS_2 : indexPrecision === 3 ? COLOR_WEIGHTS_3 : COLOR_WEIGHTS_4;
  const w = weights[index];
  return (64 - w) * e0 + w * e1 + 32 >> 6;
}
var SUBSETS_2_PARTITION_TABLE = [
  [0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1],
  [0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1],
  [0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1],
  [0, 0, 0, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 1, 1, 1],
  [0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 1, 1],
  [0, 0, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1],
  [0, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1],
  [0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 1],
  [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1],
  [0, 0, 1, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
  [0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1],
  [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 1],
  [0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
  [0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1],
  [0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1],
  [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1],
  [0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1, 1],
  [0, 1, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0],
  [0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0],
  [0, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0],
  [0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0],
  [0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0],
  [0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0],
  [0, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 1],
  [0, 0, 1, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0],
  [0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 1, 0, 0],
  [0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0],
  [0, 0, 1, 1, 0, 1, 1, 0, 0, 1, 1, 0, 1, 1, 0, 0],
  [0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0],
  [0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0],
  [0, 1, 1, 1, 0, 0, 0, 1, 1, 0, 0, 0, 1, 1, 1, 0],
  [0, 0, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 0],
  [0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1],
  [0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1],
  [0, 1, 0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0],
  [0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0],
  [0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0],
  [0, 1, 0, 1, 0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0],
  [0, 1, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, 1, 0, 0, 1],
  [0, 1, 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 0, 1, 0, 1],
  [0, 1, 1, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0],
  [0, 0, 0, 1, 0, 0, 1, 1, 1, 1, 0, 0, 1, 0, 0, 0],
  [0, 0, 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 1, 0, 0],
  [0, 0, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 0, 0],
  [0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1, 0, 1, 1, 0],
  [0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 1, 1],
  [0, 1, 1, 0, 0, 1, 1, 0, 1, 0, 0, 1, 1, 0, 0, 1],
  [0, 0, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0],
  [0, 1, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0],
  [0, 0, 1, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0, 0, 0, 0],
  [0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0, 0, 1, 0],
  [0, 0, 0, 0, 0, 1, 0, 0, 1, 1, 1, 0, 0, 1, 0, 0],
  [0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 1],
  [0, 0, 1, 1, 0, 1, 1, 0, 1, 1, 0, 0, 1, 0, 0, 1],
  [0, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0],
  [0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 0, 1, 1, 0],
  [0, 1, 1, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 1],
  [0, 1, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 1, 0, 0, 1],
  [0, 1, 1, 1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1],
  [0, 0, 0, 1, 1, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1],
  [0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1],
  [0, 0, 1, 1, 0, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0],
  [0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 0, 1, 1, 1, 0],
  [0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 0, 1, 1, 1]
];
var SUBSETS_3_PARTITION_TABLE = [
  [0, 0, 1, 1, 0, 0, 1, 1, 0, 2, 2, 1, 2, 2, 2, 2],
  [0, 0, 0, 1, 0, 0, 1, 1, 2, 2, 1, 1, 2, 2, 2, 1],
  [0, 0, 0, 0, 2, 0, 0, 1, 2, 2, 1, 1, 2, 2, 1, 1],
  [0, 2, 2, 2, 0, 0, 2, 2, 0, 0, 1, 1, 0, 1, 1, 1],
  [0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 2, 2, 1, 1, 2, 2],
  [0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 2, 2, 0, 0, 2, 2],
  [0, 0, 2, 2, 0, 0, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1],
  [0, 0, 1, 1, 0, 0, 1, 1, 2, 2, 1, 1, 2, 2, 1, 1],
  [0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2],
  [0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2],
  [0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2],
  [0, 0, 1, 2, 0, 0, 1, 2, 0, 0, 1, 2, 0, 0, 1, 2],
  [0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2, 0, 1, 1, 2],
  [0, 1, 2, 2, 0, 1, 2, 2, 0, 1, 2, 2, 0, 1, 2, 2],
  [0, 0, 1, 1, 0, 1, 1, 2, 1, 1, 2, 2, 1, 2, 2, 2],
  [0, 0, 1, 1, 2, 0, 0, 1, 2, 2, 0, 0, 2, 2, 2, 0],
  [0, 0, 0, 1, 0, 0, 1, 1, 0, 1, 1, 2, 1, 1, 2, 2],
  [0, 1, 1, 1, 0, 0, 1, 1, 2, 0, 0, 1, 2, 2, 0, 0],
  [0, 0, 0, 0, 1, 1, 2, 2, 1, 1, 2, 2, 1, 1, 2, 2],
  [0, 0, 2, 2, 0, 0, 2, 2, 0, 0, 2, 2, 1, 1, 1, 1],
  [0, 1, 1, 1, 0, 1, 1, 1, 0, 2, 2, 2, 0, 2, 2, 2],
  [0, 0, 0, 1, 0, 0, 0, 1, 2, 2, 2, 1, 2, 2, 2, 1],
  [0, 0, 0, 0, 0, 0, 1, 1, 0, 1, 2, 2, 0, 1, 2, 2],
  [0, 0, 0, 0, 1, 1, 0, 0, 2, 2, 1, 0, 2, 2, 1, 0],
  [0, 1, 2, 2, 0, 1, 2, 2, 0, 0, 1, 1, 0, 0, 0, 0],
  [0, 0, 1, 2, 0, 0, 1, 2, 1, 1, 2, 2, 2, 2, 2, 2],
  [0, 1, 1, 0, 1, 2, 2, 1, 1, 2, 2, 1, 0, 1, 1, 0],
  [0, 0, 0, 0, 0, 1, 1, 0, 1, 2, 2, 1, 1, 2, 2, 1],
  [0, 0, 2, 2, 1, 1, 0, 2, 1, 1, 0, 2, 0, 0, 2, 2],
  [0, 1, 1, 0, 0, 1, 1, 0, 2, 0, 0, 2, 2, 2, 2, 2],
  [0, 0, 1, 1, 0, 1, 2, 2, 0, 1, 2, 2, 0, 0, 1, 1],
  [0, 0, 0, 0, 2, 0, 0, 0, 2, 2, 1, 1, 2, 2, 2, 1],
  [0, 0, 0, 0, 0, 0, 0, 2, 1, 1, 2, 2, 1, 2, 2, 2],
  [0, 2, 2, 2, 0, 0, 2, 2, 0, 0, 1, 2, 0, 0, 1, 1],
  [0, 0, 1, 1, 0, 0, 1, 2, 0, 0, 2, 2, 0, 2, 2, 2],
  [0, 1, 2, 0, 0, 1, 2, 0, 0, 1, 2, 0, 0, 1, 2, 0],
  [0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 0, 0, 0, 0],
  [0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0, 1, 2, 0],
  [0, 1, 2, 0, 2, 0, 1, 2, 1, 2, 0, 1, 0, 1, 2, 0],
  [0, 0, 1, 1, 2, 2, 0, 0, 1, 1, 2, 2, 0, 0, 1, 1],
  [0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 0, 0, 0, 0, 1, 1],
  [0, 1, 0, 1, 0, 1, 0, 1, 2, 2, 2, 2, 2, 2, 2, 2],
  [0, 0, 0, 0, 0, 0, 0, 0, 2, 1, 2, 1, 2, 1, 2, 1],
  [0, 0, 2, 2, 1, 1, 2, 2, 0, 0, 2, 2, 1, 1, 2, 2],
  [0, 0, 2, 2, 0, 0, 1, 1, 0, 0, 2, 2, 0, 0, 1, 1],
  [0, 2, 2, 0, 1, 2, 2, 1, 0, 2, 2, 0, 1, 2, 2, 1],
  [0, 1, 0, 1, 2, 2, 2, 2, 2, 2, 2, 2, 0, 1, 0, 1],
  [0, 0, 0, 0, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1],
  [0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 2, 2, 2, 2],
  [0, 2, 2, 2, 0, 1, 1, 1, 0, 2, 2, 2, 0, 1, 1, 1],
  [0, 0, 0, 2, 1, 1, 1, 2, 0, 0, 0, 2, 1, 1, 1, 2],
  [0, 0, 0, 0, 2, 1, 1, 2, 2, 1, 1, 2, 2, 1, 1, 2],
  [0, 2, 2, 2, 0, 1, 1, 1, 0, 1, 1, 1, 0, 2, 2, 2],
  [0, 0, 0, 2, 1, 1, 1, 2, 1, 1, 1, 2, 0, 0, 0, 2],
  [0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 1, 0, 2, 2, 2, 2],
  [0, 0, 0, 0, 0, 0, 0, 0, 2, 1, 1, 2, 2, 1, 1, 2],
  [0, 1, 1, 0, 0, 1, 1, 0, 2, 2, 2, 2, 2, 2, 2, 2],
  [0, 0, 2, 2, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 2, 2],
  [0, 0, 2, 2, 1, 1, 2, 2, 1, 1, 2, 2, 0, 0, 2, 2],
  [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 1, 1, 2],
  [0, 0, 0, 2, 0, 0, 0, 1, 0, 0, 0, 2, 0, 0, 0, 1],
  [0, 2, 2, 2, 1, 2, 2, 2, 0, 2, 2, 2, 1, 2, 2, 2],
  [0, 1, 0, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2],
  [0, 1, 1, 1, 2, 0, 1, 1, 2, 2, 0, 1, 2, 2, 2, 0]
];
var SUBSETS_2_ANCHOR_INDICES = [
  15,
  15,
  15,
  15,
  15,
  15,
  15,
  15,
  15,
  15,
  15,
  15,
  15,
  15,
  15,
  15,
  15,
  2,
  8,
  2,
  2,
  8,
  8,
  15,
  2,
  8,
  2,
  2,
  8,
  8,
  2,
  2,
  15,
  15,
  6,
  8,
  2,
  8,
  15,
  15,
  2,
  8,
  2,
  2,
  2,
  15,
  15,
  6,
  6,
  2,
  6,
  8,
  15,
  15,
  2,
  2,
  15,
  15,
  15,
  15,
  15,
  2,
  2,
  15
];
var SUBSETS_3_ANCHOR_INDICES_2 = [
  3,
  3,
  15,
  15,
  8,
  3,
  15,
  15,
  8,
  8,
  6,
  6,
  6,
  5,
  3,
  3,
  3,
  3,
  8,
  15,
  3,
  3,
  6,
  10,
  5,
  8,
  8,
  6,
  8,
  5,
  15,
  15,
  8,
  15,
  3,
  5,
  6,
  10,
  8,
  15,
  15,
  3,
  15,
  5,
  15,
  15,
  15,
  15,
  3,
  15,
  5,
  5,
  5,
  8,
  5,
  10,
  5,
  10,
  8,
  13,
  15,
  12,
  3,
  3
];
var SUBSETS_3_ANCHOR_INDICES_3 = [
  15,
  8,
  8,
  3,
  15,
  15,
  3,
  8,
  15,
  15,
  15,
  15,
  15,
  15,
  15,
  8,
  15,
  8,
  15,
  3,
  15,
  8,
  15,
  8,
  3,
  15,
  6,
  10,
  15,
  15,
  10,
  8,
  15,
  3,
  15,
  10,
  10,
  8,
  9,
  10,
  6,
  15,
  8,
  15,
  3,
  6,
  6,
  8,
  15,
  3,
  15,
  15,
  15,
  15,
  15,
  15,
  15,
  15,
  15,
  15,
  3,
  15,
  15,
  8
];
var Bc7Block = class {
  constructor(data) {
    const view = new DataView(data.buffer, data.byteOffset, 16);
    this.lowBits = view.getBigUint64(0, true);
    this.highBits = view.getBigUint64(8, true);
  }
  getType() {
    for (let i = 0; i < 8; i++) {
      const mask = 1n << BigInt(i);
      if ((this.lowBits & mask) === mask) {
        return i;
      }
    }
    return 8;
  }
  getNumSubsets(type) {
    if (type === 0 || type === 2) return 3;
    if (type === 1 || type === 3 || type === 7) return 2;
    return 1;
  }
  getPartitionSetId(type) {
    switch (type) {
      case 0:
        return ByteHelper.extract4(this.lowBits, 1);
      case 1:
        return ByteHelper.extract6(this.lowBits, 2);
      case 2:
        return ByteHelper.extract6(this.lowBits, 3);
      case 3:
        return ByteHelper.extract6(this.lowBits, 4);
      case 7:
        return ByteHelper.extract6(this.lowBits, 8);
      default:
        return 0;
    }
  }
  getRotationBits(type) {
    if (type === 4) return ByteHelper.extract2(this.lowBits, 5);
    if (type === 5) return ByteHelper.extract2(this.lowBits, 6);
    return 0;
  }
  getColorComponentPrecision(type) {
    const precisions = [5, 7, 5, 8, 5, 7, 8, 6];
    return precisions[type] || 0;
  }
  getAlphaComponentPrecision(type) {
    if (type === 4) return 6;
    if (type === 5 || type === 6) return 8;
    if (type === 7) return 6;
    return 0;
  }
  getType4IndexMode() {
    return ByteHelper.extract1(this.lowBits, 7);
  }
  getColorIndexBitCount(type) {
    if (type === 0 || type === 1) return 3;
    if (type === 2 || type === 3 || type === 5 || type === 7) return 2;
    if (type === 4) {
      const indexMode = this.getType4IndexMode();
      return indexMode === 0 ? 2 : 3;
    }
    if (type === 6) return 4;
    return 0;
  }
  getAlphaIndexBitCount(type) {
    if (type === 4) {
      const indexMode = this.getType4IndexMode();
      return indexMode === 0 ? 3 : 2;
    }
    if (type === 5 || type === 7) return 2;
    if (type === 6) return 4;
    return 0;
  }
  extractRawEndpoints(type, numSubsets) {
    const endpoints = Array(numSubsets * 2).fill(null).map(() => ({ r: 0, g: 0, b: 0, a: 0 }));
    switch (type) {
      case 0:
        for (let i = 0; i < 6; i++) {
          endpoints[i].r = ByteHelper.extractFrom128(this.lowBits, this.highBits, 5 + i * 4, 4);
          endpoints[i].g = ByteHelper.extractFrom128(this.lowBits, this.highBits, 29 + i * 4, 4);
          endpoints[i].b = ByteHelper.extractFrom128(this.lowBits, this.highBits, 53 + i * 4, 4);
        }
        break;
      case 1:
        for (let i = 0; i < 4; i++) {
          endpoints[i].r = ByteHelper.extractFrom128(this.lowBits, this.highBits, 8 + i * 6, 6);
          endpoints[i].g = ByteHelper.extractFrom128(this.lowBits, this.highBits, 32 + i * 6, 6);
          endpoints[i].b = ByteHelper.extractFrom128(this.lowBits, this.highBits, 56 + i * 6, 6);
        }
        break;
      case 2:
        for (let i = 0; i < 6; i++) {
          endpoints[i].r = ByteHelper.extractFrom128(this.lowBits, this.highBits, 9 + i * 5, 5);
          endpoints[i].g = ByteHelper.extractFrom128(this.lowBits, this.highBits, 39 + i * 5, 5);
          endpoints[i].b = ByteHelper.extractFrom128(this.lowBits, this.highBits, 69 + i * 5, 5);
        }
        break;
      case 3:
        for (let i = 0; i < 4; i++) {
          endpoints[i].r = ByteHelper.extractFrom128(this.lowBits, this.highBits, 10 + i * 7, 7);
          endpoints[i].g = ByteHelper.extractFrom128(this.lowBits, this.highBits, 38 + i * 7, 7);
          endpoints[i].b = ByteHelper.extractFrom128(this.lowBits, this.highBits, 66 + i * 7, 7);
        }
        break;
      case 4:
        endpoints[0].r = ByteHelper.extractFrom128(this.lowBits, this.highBits, 8, 5);
        endpoints[1].r = ByteHelper.extractFrom128(this.lowBits, this.highBits, 13, 5);
        endpoints[0].g = ByteHelper.extractFrom128(this.lowBits, this.highBits, 18, 5);
        endpoints[1].g = ByteHelper.extractFrom128(this.lowBits, this.highBits, 23, 5);
        endpoints[0].b = ByteHelper.extractFrom128(this.lowBits, this.highBits, 28, 5);
        endpoints[1].b = ByteHelper.extractFrom128(this.lowBits, this.highBits, 33, 5);
        endpoints[0].a = ByteHelper.extractFrom128(this.lowBits, this.highBits, 38, 6);
        endpoints[1].a = ByteHelper.extractFrom128(this.lowBits, this.highBits, 44, 6);
        break;
      case 5:
        endpoints[0].r = ByteHelper.extractFrom128(this.lowBits, this.highBits, 8, 7);
        endpoints[1].r = ByteHelper.extractFrom128(this.lowBits, this.highBits, 15, 7);
        endpoints[0].g = ByteHelper.extractFrom128(this.lowBits, this.highBits, 22, 7);
        endpoints[1].g = ByteHelper.extractFrom128(this.lowBits, this.highBits, 29, 7);
        endpoints[0].b = ByteHelper.extractFrom128(this.lowBits, this.highBits, 36, 7);
        endpoints[1].b = ByteHelper.extractFrom128(this.lowBits, this.highBits, 43, 7);
        endpoints[0].a = ByteHelper.extractFrom128(this.lowBits, this.highBits, 50, 8);
        endpoints[1].a = ByteHelper.extractFrom128(this.lowBits, this.highBits, 58, 8);
        break;
      case 6:
        endpoints[0].r = ByteHelper.extractFrom128(this.lowBits, this.highBits, 7, 7);
        endpoints[1].r = ByteHelper.extractFrom128(this.lowBits, this.highBits, 14, 7);
        endpoints[0].g = ByteHelper.extractFrom128(this.lowBits, this.highBits, 21, 7);
        endpoints[1].g = ByteHelper.extractFrom128(this.lowBits, this.highBits, 28, 7);
        endpoints[0].b = ByteHelper.extractFrom128(this.lowBits, this.highBits, 35, 7);
        endpoints[1].b = ByteHelper.extractFrom128(this.lowBits, this.highBits, 42, 7);
        endpoints[0].a = ByteHelper.extractFrom128(this.lowBits, this.highBits, 49, 7);
        endpoints[1].a = ByteHelper.extractFrom128(this.lowBits, this.highBits, 56, 7);
        break;
      case 7:
        for (let i = 0; i < 4; i++) {
          endpoints[i].r = ByteHelper.extractFrom128(this.lowBits, this.highBits, 14 + i * 5, 5);
          endpoints[i].g = ByteHelper.extractFrom128(this.lowBits, this.highBits, 34 + i * 5, 5);
          endpoints[i].b = ByteHelper.extractFrom128(this.lowBits, this.highBits, 54 + i * 5, 5);
          endpoints[i].a = ByteHelper.extractFrom128(this.lowBits, this.highBits, 74 + i * 5, 5);
        }
        break;
    }
    return endpoints;
  }
  extractPBits(type, _numSubsets) {
    switch (type) {
      case 0:
        return [
          ByteHelper.extract1(this.highBits, 77 - 64),
          ByteHelper.extract1(this.highBits, 78 - 64),
          ByteHelper.extract1(this.highBits, 79 - 64),
          ByteHelper.extract1(this.highBits, 80 - 64),
          ByteHelper.extract1(this.highBits, 81 - 64),
          ByteHelper.extract1(this.highBits, 82 - 64)
        ];
      case 1:
        return [
          ByteHelper.extract1(this.highBits, 80 - 64),
          ByteHelper.extract1(this.highBits, 81 - 64)
        ];
      case 3:
        return [
          ByteHelper.extract1(this.highBits, 94 - 64),
          ByteHelper.extract1(this.highBits, 95 - 64),
          ByteHelper.extract1(this.highBits, 96 - 64),
          ByteHelper.extract1(this.highBits, 97 - 64)
        ];
      case 6:
        return [
          ByteHelper.extract1(this.lowBits, 63),
          ByteHelper.extract1(this.highBits, 0)
        ];
      case 7:
        return [
          ByteHelper.extract1(this.highBits, 94 - 64),
          ByteHelper.extract1(this.highBits, 95 - 64),
          ByteHelper.extract1(this.highBits, 96 - 64),
          ByteHelper.extract1(this.highBits, 97 - 64)
        ];
      default:
        return [];
    }
  }
  hasPBits(type) {
    return type === 0 || type === 1 || type === 3 || type === 6 || type === 7;
  }
  hasAlpha(type) {
    return type === 4 || type === 5 || type === 6 || type === 7;
  }
  finalizeEndpoints(endpoints, type) {
    const hasPBits = this.hasPBits(type);
    if (hasPBits) {
      const pBits = this.extractPBits(type, endpoints.length);
      for (const ep of endpoints) {
        ep.r <<= 1;
        ep.g <<= 1;
        ep.b <<= 1;
        ep.a <<= 1;
      }
      if (type === 1) {
        endpoints[0].r |= pBits[0];
        endpoints[0].g |= pBits[0];
        endpoints[0].b |= pBits[0];
        endpoints[1].r |= pBits[0];
        endpoints[1].g |= pBits[0];
        endpoints[1].b |= pBits[0];
        endpoints[2].r |= pBits[1];
        endpoints[2].g |= pBits[1];
        endpoints[2].b |= pBits[1];
        endpoints[3].r |= pBits[1];
        endpoints[3].g |= pBits[1];
        endpoints[3].b |= pBits[1];
      } else {
        for (let i = 0; i < endpoints.length; i++) {
          endpoints[i].r |= pBits[i];
          endpoints[i].g |= pBits[i];
          endpoints[i].b |= pBits[i];
          if (this.hasAlpha(type)) {
            endpoints[i].a |= pBits[i];
          }
        }
      }
    }
    const colorPrec = this.getColorComponentPrecision(type);
    const alphaPrec = this.getAlphaComponentPrecision(type);
    for (const ep of endpoints) {
      ep.r = ep.r << 8 - colorPrec | ep.r >> colorPrec - (8 - colorPrec);
      ep.g = ep.g << 8 - colorPrec | ep.g >> colorPrec - (8 - colorPrec);
      ep.b = ep.b << 8 - colorPrec | ep.b >> colorPrec - (8 - colorPrec);
      ep.a = alphaPrec > 0 ? ep.a << 8 - alphaPrec | ep.a >> alphaPrec - (8 - alphaPrec) : 255;
    }
    if (!this.hasAlpha(type)) {
      for (const ep of endpoints) {
        ep.a = 255;
      }
    }
  }
  getPartitionIndex(numSubsets, partitionSetId, pixelIndex) {
    if (numSubsets === 1) return 0;
    if (numSubsets === 2) return SUBSETS_2_PARTITION_TABLE[partitionSetId][pixelIndex];
    return SUBSETS_3_PARTITION_TABLE[partitionSetId][pixelIndex];
  }
  getIndexBegin(type, bitCount, isAlpha) {
    switch (type) {
      case 0:
        return 83;
      case 1:
        return 82;
      case 2:
        return 99;
      case 3:
        return 98;
      case 4:
        return bitCount === 2 ? 50 : 81;
      case 5:
        return isAlpha ? 97 : 66;
      case 6:
        return 65;
      case 7:
        return 98;
      default:
        return 0;
    }
  }
  getIndexBitCount(numSubsets, partitionIndex, bitCount, pixelIndex) {
    if (pixelIndex === 0) return bitCount - 1;
    if (numSubsets === 2) {
      const anchorIndex = SUBSETS_2_ANCHOR_INDICES[partitionIndex];
      if (pixelIndex === anchorIndex) return bitCount - 1;
    } else if (numSubsets === 3) {
      const anchor2 = SUBSETS_3_ANCHOR_INDICES_2[partitionIndex];
      const anchor3 = SUBSETS_3_ANCHOR_INDICES_3[partitionIndex];
      if (pixelIndex === anchor2 || pixelIndex === anchor3) return bitCount - 1;
    }
    return bitCount;
  }
  getIndexOffset(type, numSubsets, partitionIndex, bitCount, pixelIndex) {
    if (pixelIndex === 0) return 0;
    if (numSubsets === 1) {
      return bitCount * pixelIndex - 1;
    }
    if (numSubsets === 2) {
      const anchorIndex = SUBSETS_2_ANCHOR_INDICES[partitionIndex];
      if (pixelIndex <= anchorIndex) {
        return bitCount * pixelIndex - 1;
      } else {
        return bitCount * pixelIndex - 2;
      }
    }
    if (numSubsets === 3) {
      const anchor2 = SUBSETS_3_ANCHOR_INDICES_2[partitionIndex];
      const anchor3 = SUBSETS_3_ANCHOR_INDICES_3[partitionIndex];
      if (pixelIndex <= anchor2 && pixelIndex <= anchor3) {
        return bitCount * pixelIndex - 1;
      } else if (pixelIndex > anchor2 && pixelIndex > anchor3) {
        return bitCount * pixelIndex - 3;
      } else {
        return bitCount * pixelIndex - 2;
      }
    }
    return 0;
  }
  getColorIndex(type, numSubsets, partitionIndex, bitCount, pixelIndex) {
    const indexOffset = this.getIndexOffset(type, numSubsets, partitionIndex, bitCount, pixelIndex);
    const indexBitCount = this.getIndexBitCount(numSubsets, partitionIndex, bitCount, pixelIndex);
    const indexBegin = this.getIndexBegin(type, bitCount, false);
    return ByteHelper.extractFrom128(this.lowBits, this.highBits, indexBegin + indexOffset, indexBitCount);
  }
  getAlphaIndex(type, numSubsets, partitionIndex, bitCount, pixelIndex) {
    if (bitCount === 0) return 0;
    const indexOffset = this.getIndexOffset(type, numSubsets, partitionIndex, bitCount, pixelIndex);
    const indexBitCount = this.getIndexBitCount(numSubsets, partitionIndex, bitCount, pixelIndex);
    const indexBegin = this.getIndexBegin(type, bitCount, true);
    return ByteHelper.extractFrom128(this.lowBits, this.highBits, indexBegin + indexOffset, indexBitCount);
  }
  swapChannels(color, rotation) {
    switch (rotation) {
      case 0:
        return color;
      case 1:
        return { r: color.a, g: color.g, b: color.b, a: color.r };
      case 2:
        return { r: color.r, g: color.a, b: color.b, a: color.g };
      case 3:
        return { r: color.r, g: color.g, b: color.a, a: color.b };
      default:
        return color;
    }
  }
  decode() {
    const output = new Uint8Array(16 * 4);
    const type = this.getType();
    if (type === 8) {
      for (let i = 0; i < 16; i++) {
        output[i * 4] = 255;
        output[i * 4 + 1] = 0;
        output[i * 4 + 2] = 255;
        output[i * 4 + 3] = 255;
      }
      return output;
    }
    const numSubsets = this.getNumSubsets(type);
    const partitionSetId = this.getPartitionSetId(type);
    const rotation = this.getRotationBits(type);
    const endpoints = this.extractRawEndpoints(type, numSubsets);
    this.finalizeEndpoints(endpoints, type);
    const colorBitCount = this.getColorIndexBitCount(type);
    const alphaBitCount = this.getAlphaIndexBitCount(type);
    for (let i = 0; i < 16; i++) {
      const subsetIndex = this.getPartitionIndex(numSubsets, partitionSetId, i);
      const ep0 = endpoints[2 * subsetIndex];
      const ep1 = endpoints[2 * subsetIndex + 1];
      const colorIndex = this.getColorIndex(type, numSubsets, partitionSetId, colorBitCount, i);
      const alphaIndex = this.getAlphaIndex(type, numSubsets, partitionSetId, alphaBitCount, i);
      let color = {
        r: interpolateByte(ep0.r, ep1.r, colorIndex, colorBitCount),
        g: interpolateByte(ep0.g, ep1.g, colorIndex, colorBitCount),
        b: interpolateByte(ep0.b, ep1.b, colorIndex, colorBitCount),
        a: interpolateByte(ep0.a, ep1.a, alphaIndex, alphaBitCount || colorBitCount)
      };
      if (rotation > 0) {
        color = this.swapChannels(color, rotation);
      }
      output[i * 4] = color.r;
      output[i * 4 + 1] = color.g;
      output[i * 4 + 2] = color.b;
      output[i * 4 + 3] = color.a;
    }
    return output;
  }
};
function decodeBC7(imageData, width, height) {
  const rgba = new Uint8Array(width * height * 4);
  const blocksX = Math.ceil(width / 4);
  const blocksY = Math.ceil(height / 4);
  let offset = 0;
  for (let by = 0; by < blocksY; by++) {
    for (let bx = 0; bx < blocksX; bx++) {
      const blockData = new Uint8Array(16);
      for (let i = 0; i < 16; i++) {
        blockData[i] = imageData.getUint8(offset + i);
      }
      const block = new Bc7Block(blockData);
      const decodedBlock = block.decode();
      for (let y = 0; y < 4; y++) {
        for (let x = 0; x < 4; x++) {
          const px = bx * 4 + x;
          const py = by * 4 + y;
          if (px < width && py < height) {
            const srcIdx = (y * 4 + x) * 4;
            const dstIdx = (py * width + px) * 4;
            rgba[dstIdx] = decodedBlock[srcIdx];
            rgba[dstIdx + 1] = decodedBlock[srcIdx + 1];
            rgba[dstIdx + 2] = decodedBlock[srcIdx + 2];
            rgba[dstIdx + 3] = decodedBlock[srcIdx + 3];
          }
        }
      }
      offset += 16;
    }
  }
  return rgba;
}
export {
  decodeBC1,
  decodeBC2,
  decodeBC3,
  decodeBC4,
  decodeBC5,
  decodeBC7
};
//# sourceMappingURL=index.js.map
