import { describe, expect, it } from "vitest";
import { qrMatrix, qrSvgPath } from "./qr";

/**
 * Decodes a matrix the way a reader would: undo the mask, walk the placement
 * order back out, de-interleave, and check the Reed-Solomon syndromes are all
 * zero. That verifies the whole pipeline, not just that it produced a picture.
 */
function readBack(matrix: boolean[][]): { text: string; version: number } {
  const size = matrix.length;
  const version = (size - 17) / 4;
  const FORMAT_M = [0x5412, 0x5125, 0x5e7c, 0x5b4b, 0x45f9, 0x40ce, 0x4f97, 0x4aa0];
  let format = 0;
  for (let i = 0; i <= 5; i += 1) format |= (matrix[8][i] ? 1 : 0) << i;
  format |= (matrix[8][7] ? 1 : 0) << 6;
  format |= (matrix[8][8] ? 1 : 0) << 7;
  format |= (matrix[7][8] ? 1 : 0) << 8;
  for (let i = 9; i <= 14; i += 1) format |= (matrix[14 - i][8] ? 1 : 0) << i;
  const mask = FORMAT_M.indexOf(format);
  expect(mask, "format information must name a level-M mask").toBeGreaterThanOrEqual(0);

  // Rebuild the reserved map the same way the encoder does.
  const fixed: boolean[][] = Array.from({ length: size }, () => new Array(size).fill(false));
  const reserve = (r: number, c: number) => { if (r >= 0 && c >= 0 && r < size && c < size) fixed[r][c] = true; };
  for (const [top, left] of [[0, 0], [0, size - 7], [size - 7, 0]]) {
    for (let r = -1; r <= 7; r += 1) for (let c = -1; c <= 7; c += 1) reserve(top + r, left + c);
  }
  for (let i = 0; i < size; i += 1) { reserve(6, i); reserve(i, 6); }
  const ALIGNMENT = [[], [6, 18], [6, 22], [6, 26], [6, 30], [6, 34], [6, 22, 38], [6, 24, 42], [6, 26, 46], [6, 28, 50]][version - 1] as number[];
  for (const row of ALIGNMENT) for (const col of ALIGNMENT) {
    if ((row <= 8 && col <= 8) || (row <= 8 && col >= size - 9) || (row >= size - 9 && col <= 8)) continue;
    for (let r = -2; r <= 2; r += 1) for (let c = -2; c <= 2; c += 1) reserve(row + r, col + c);
  }
  for (let i = 0; i <= 8; i += 1) { reserve(8, i); reserve(i, 8); }
  for (let i = 0; i <= 7; i += 1) { reserve(8, size - 1 - i); reserve(size - 1 - i, 8); }
  if (version >= 7) for (let i = 0; i < 18; i += 1) {
    reserve(size - 11 + (i % 3), Math.floor(i / 3));
    reserve(Math.floor(i / 3), size - 11 + (i % 3));
  }

  const MASKS = [
    (r: number, c: number) => (r + c) % 2 === 0,
    (r: number) => r % 2 === 0,
    (_r: number, c: number) => c % 3 === 0,
    (r: number, c: number) => (r + c) % 3 === 0,
    (r: number, c: number) => (Math.floor(r / 2) + Math.floor(c / 3)) % 2 === 0,
    (r: number, c: number) => ((r * c) % 2) + ((r * c) % 3) === 0,
    (r: number, c: number) => (((r * c) % 2) + ((r * c) % 3)) % 2 === 0,
    (r: number, c: number) => (((r + c) % 2) + ((r * c) % 3)) % 2 === 0,
  ];

  const bits: number[] = [];
  for (let right = size - 1; right >= 1; right -= 2) {
    if (right === 6) right = 5;
    for (let step = 0; step < size; step += 1) {
      for (let half = 0; half < 2; half += 1) {
        const col = right - half;
        const upward = ((right + 1) & 2) === 0;
        const row = upward ? size - 1 - step : step;
        if (fixed[row][col]) continue;
        bits.push((matrix[row][col] !== MASKS[mask](row, col)) ? 1 : 0);
      }
    }
  }
  // Independent of how the encoder reserves: the free-module count is fixed by
  // the standard, so an over- or under-reservation is caught here rather than
  // being mirrored by this decoder and slipping through.
  const TOTAL_CODEWORDS = [26, 44, 70, 100, 134, 172, 196, 242, 292, 346][version - 1];
  const REMAINDER = version === 1 || version >= 7 ? 0 : 7;
  expect(bits.length, "data module count").toBe(TOTAL_CODEWORDS * 8 + REMAINDER);

  const stream = new Uint8Array(bits.length >> 3);
  for (let i = 0; i < stream.length * 8; i += 1) if (bits[i]) stream[i >> 3] |= 0x80 >>> (i & 7);

  const SPECS = [
    { ecc: 10, groups: [[1, 16]] }, { ecc: 16, groups: [[1, 28]] }, { ecc: 26, groups: [[1, 44]] },
    { ecc: 18, groups: [[2, 32]] }, { ecc: 24, groups: [[2, 43]] }, { ecc: 16, groups: [[4, 27]] },
    { ecc: 18, groups: [[4, 31]] }, { ecc: 22, groups: [[2, 38], [2, 39]] },
    { ecc: 22, groups: [[3, 36], [2, 37]] }, { ecc: 26, groups: [[4, 43], [1, 44]] },
  ][version - 1] as { ecc: number; groups: number[][] };

  const sizes: number[] = [];
  for (const [count, len] of spec_groups(SPECS)) for (let i = 0; i < count; i += 1) sizes.push(len);
  const blocks = sizes.map((len) => new Uint8Array(len));
  const checks = sizes.map(() => new Uint8Array(SPECS.ecc));
  let cursor = 0;
  for (let i = 0; i < Math.max(...sizes); i += 1) {
    for (let b = 0; b < blocks.length; b += 1) if (i < sizes[b]) blocks[b][i] = stream[cursor++];
  }
  for (let i = 0; i < SPECS.ecc; i += 1) {
    for (let b = 0; b < blocks.length; b += 1) checks[b][i] = stream[cursor++];
  }

  // Syndromes: a correct codeword evaluates to zero at every root of the generator.
  const EXP = new Uint8Array(512); const LOG = new Uint8Array(256);
  for (let i = 0, x = 1; i < 255; i += 1) { EXP[i] = x; LOG[x] = i; x <<= 1; if (x & 0x100) x ^= 0x11d; }
  for (let i = 255; i < 512; i += 1) EXP[i] = EXP[i - 255];
  const mul = (a: number, b: number) => (a && b ? EXP[LOG[a] + LOG[b]] : 0);
  blocks.forEach((block, index) => {
    const full = new Uint8Array([...block, ...checks[index]]);
    for (let s = 0; s < SPECS.ecc; s += 1) {
      let value = 0;
      for (const byte of full) value = mul(value, EXP[s]) ^ byte;
      expect(value, `block ${index} syndrome ${s}`).toBe(0);
    }
  });

  const data = new Uint8Array(blocks.reduce<number[]>((all, b) => [...all, ...b], []));
  expect(data[0] >> 4).toBe(0b0100);
  const countBits = version < 10 ? 8 : 16;
  let at = 4;
  const take = (width: number) => {
    let value = 0;
    for (let i = 0; i < width; i += 1, at += 1) value = (value << 1) | ((data[at >> 3] >>> (7 - (at & 7))) & 1);
    return value;
  };
  const length = take(countBits);
  const bytes = new Uint8Array(length);
  for (let i = 0; i < length; i += 1) bytes[i] = take(8);
  return { text: new TextDecoder().decode(bytes), version };
}

function spec_groups(spec: { groups: number[][] }): number[][] { return spec.groups; }

describe("qrMatrix", () => {
  const url = "https://www.bilibili.com/toy/endfield/index.html?r=Ab3xY9_kQm2P";

  it("round-trips a record link", () => {
    expect(readBack(qrMatrix(url)).text).toBe(url);
  });

  // v1..v10 including the two-group layouts (v8-v10) and version info (v7+).
  for (const length of [1, 14, 26, 42, 62, 84, 106, 122, 152, 180, 213]) {
    it(`round-trips ${length} bytes`, () => {
      const text = "A".repeat(length);
      expect(readBack(qrMatrix(text)).text).toBe(text);
    });
  }

  it("handles multi-byte utf-8", () => {
    const text = "泥岩 · 荒芜拉黎 4.2万 DPS";
    expect(readBack(qrMatrix(text)).text).toBe(text);
  });

  it("refuses content past version 10", () => {
    expect(() => qrMatrix("A".repeat(400))).toThrow();
  });

  it("emits a path covering exactly the dark modules", () => {
    const matrix = qrMatrix(url);
    const runs = qrSvgPath(matrix).match(/h(\d+)v1/g) ?? [];
    const covered = runs.reduce((sum, run) => sum + Number(run.slice(1, -2)), 0);
    const dark = matrix.flat().filter(Boolean).length;
    expect(covered).toBe(dark);
  });
});
