// A QR encoder, byte mode, error level M, versions 1-10.
//
// The Toy SDK has a getQrCode, but on web its share/clipboard neighbours turn
// out to be either unavailable or blocked by the iframe's permission policy,
// and a share dialog that sometimes has no code in it is not worth shipping.
// The payload here is one short https URL, so ten versions (213 bytes at level
// M) is far more headroom than the feature can ever use.

const EXP = new Uint8Array(512);
const LOG = new Uint8Array(256);
for (let i = 0, x = 1; i < 255; i += 1) {
  EXP[i] = x;
  LOG[x] = i;
  x <<= 1;
  if (x & 0x100) x ^= 0x11d;             // GF(256) with the QR primitive polynomial
}
for (let i = 255; i < 512; i += 1) EXP[i] = EXP[i - 255];

const mul = (a: number, b: number) => (a && b ? EXP[LOG[a] + LOG[b]] : 0);

interface VersionSpec {
  /** EC codewords per block. */
  ecc: number;
  /** [block count, data codewords per block] for each of the two groups. */
  groups: Array<[number, number]>;
}

/** Level M only. Index is version - 1. */
const SPECS: VersionSpec[] = [
  { ecc: 10, groups: [[1, 16]] },
  { ecc: 16, groups: [[1, 28]] },
  { ecc: 26, groups: [[1, 44]] },
  { ecc: 18, groups: [[2, 32]] },
  { ecc: 24, groups: [[2, 43]] },
  { ecc: 16, groups: [[4, 27]] },
  { ecc: 18, groups: [[4, 31]] },
  { ecc: 22, groups: [[2, 38], [2, 39]] },
  { ecc: 22, groups: [[3, 36], [2, 37]] },
  { ecc: 26, groups: [[4, 43], [1, 44]] },
];

/** Alignment pattern centre coordinates, index is version - 1. */
const ALIGNMENT: number[][] = [
  [], [6, 18], [6, 22], [6, 26], [6, 30],
  [6, 34], [6, 22, 38], [6, 24, 42], [6, 26, 46], [6, 28, 50],
];

/** 18-bit version information, only written from version 7 up. */
const VERSION_INFO: Record<number, number> = {
  7: 0x07c94, 8: 0x085bc, 9: 0x09a99, 10: 0x0a4d3,
};

/** 15-bit format information for level M, indexed by mask. */
const FORMAT_M = [0x5412, 0x5125, 0x5e7c, 0x5b4b, 0x45f9, 0x40ce, 0x4f97, 0x4aa0];

const MASKS: Array<(row: number, col: number) => boolean> = [
  (r, c) => (r + c) % 2 === 0,
  (r) => r % 2 === 0,
  (_r, c) => c % 3 === 0,
  (r, c) => (r + c) % 3 === 0,
  (r, c) => (Math.floor(r / 2) + Math.floor(c / 3)) % 2 === 0,
  (r, c) => ((r * c) % 2) + ((r * c) % 3) === 0,
  (r, c) => (((r * c) % 2) + ((r * c) % 3)) % 2 === 0,
  (r, c) => (((r + c) % 2) + ((r * c) % 3)) % 2 === 0,
];

function generatorPoly(degree: number): Uint8Array {
  let poly = new Uint8Array([1]);
  for (let i = 0; i < degree; i += 1) {
    const next = new Uint8Array(poly.length + 1);
    for (let j = 0; j < poly.length; j += 1) next[j] ^= poly[j];
    for (let j = 0; j < poly.length; j += 1) next[j + 1] ^= mul(poly[j], EXP[i]);
    poly = next;
  }
  return poly;
}

/** Reed-Solomon remainder: polynomial division, leading coefficient is 1. */
function remainder(data: Uint8Array, ecLength: number): Uint8Array {
  const generator = generatorPoly(ecLength);
  const buffer = new Uint8Array(data.length + ecLength);
  buffer.set(data);
  for (let i = 0; i < data.length; i += 1) {
    const factor = buffer[i];
    if (!factor) continue;
    for (let j = 0; j < generator.length; j += 1) buffer[i + j] ^= mul(generator[j], factor);
  }
  return buffer.slice(data.length);
}

const dataCapacity = (spec: VersionSpec) =>
  spec.groups.reduce((total, [count, size]) => total + count * size, 0);

/** Mode indicator, length, payload, terminator and the standard pad bytes. */
function encodeData(bytes: Uint8Array, version: number, capacity: number): Uint8Array {
  const countBits = version < 10 ? 8 : 16;
  const out = new Uint8Array(capacity);
  let cursor = 0;
  const push = (value: number, width: number) => {
    for (let i = width - 1; i >= 0; i -= 1) {
      if ((value >>> i) & 1) out[cursor >>> 3] |= 0x80 >>> (cursor & 7);
      cursor += 1;
    }
  };
  push(0b0100, 4);                        // byte mode
  push(bytes.length, countBits);
  for (const byte of bytes) push(byte, 8);
  push(0, Math.min(4, capacity * 8 - cursor));
  cursor = (cursor + 7) & ~7;             // to the byte boundary
  for (let i = cursor >>> 3, pad = 0; i < capacity; i += 1, pad += 1) {
    out[i] = pad % 2 === 0 ? 0xec : 0x11;
  }
  return out;
}

/** Blocks are encoded separately then interleaved, per the spec's layout. */
function interleave(data: Uint8Array, spec: VersionSpec): Uint8Array {
  const blocks: Uint8Array[] = [];
  const checks: Uint8Array[] = [];
  let offset = 0;
  for (const [count, size] of spec.groups) {
    for (let i = 0; i < count; i += 1) {
      const block = data.slice(offset, offset + size);
      offset += size;
      blocks.push(block);
      checks.push(remainder(block, spec.ecc));
    }
  }
  const out: number[] = [];
  const longest = Math.max(...blocks.map((block) => block.length));
  for (let i = 0; i < longest; i += 1) {
    for (const block of blocks) if (i < block.length) out.push(block[i]);
  }
  for (let i = 0; i < spec.ecc; i += 1) for (const check of checks) out.push(check[i]);
  return new Uint8Array(out);
}

class Canvas {
  readonly size: number;
  readonly dark: Uint8Array;
  /** Function patterns and reserved areas, which the mask must not touch. */
  readonly fixed: Uint8Array;

  constructor(version: number) {
    this.size = version * 4 + 17;
    this.dark = new Uint8Array(this.size * this.size);
    this.fixed = new Uint8Array(this.size * this.size);
  }

  set(row: number, col: number, dark: boolean, fixed = true) {
    if (row < 0 || col < 0 || row >= this.size || col >= this.size) return;
    this.dark[row * this.size + col] = dark ? 1 : 0;
    if (fixed) this.fixed[row * this.size + col] = 1;
  }

  isDark(row: number, col: number) {
    return this.dark[row * this.size + col] === 1;
  }
}

function drawFunctionPatterns(canvas: Canvas, version: number) {
  const { size } = canvas;
  const finder = (top: number, left: number) => {
    for (let r = -1; r <= 7; r += 1) {
      for (let c = -1; c <= 7; c += 1) {
        const edge = Math.max(Math.abs(r - 3), Math.abs(c - 3));
        canvas.set(top + r, left + c, edge !== 2 && edge <= 3);
      }
    }
  };
  finder(0, 0);
  finder(0, size - 7);
  finder(size - 7, 0);

  for (let i = 8; i < size - 8; i += 1) {
    canvas.set(6, i, i % 2 === 0);
    canvas.set(i, 6, i % 2 === 0);
  }

  const centres = ALIGNMENT[version - 1];
  for (const row of centres) {
    for (const col of centres) {
      // The three corners already carry finders.
      const atFinder = (row <= 8 && col <= 8) ||
        (row <= 8 && col >= size - 9) || (row >= size - 9 && col <= 8);
      if (atFinder) continue;
      for (let r = -2; r <= 2; r += 1) {
        for (let c = -2; c <= 2; c += 1) {
          canvas.set(row + r, col + c, Math.max(Math.abs(r), Math.abs(c)) !== 1);
        }
      }
    }
  }

  // Reserve the format areas so data placement skips them. The copy beside the
  // top-left finder is 9 modules per arm; the two secondary strips are 8 — a
  // ninth would eat a data module, which only shows up on the versions that
  // have no remainder bits to spare.
  for (let i = 0; i <= 8; i += 1) {
    canvas.set(8, i, false);
    canvas.set(i, 8, false);
  }
  for (let i = 0; i <= 7; i += 1) {
    canvas.set(8, size - 1 - i, false);
    canvas.set(size - 1 - i, 8, false);
  }
  canvas.set(size - 8, 8, true);          // the always-dark module

  const info = VERSION_INFO[version];
  if (info === undefined) return;
  for (let i = 0; i < 18; i += 1) {
    const bit = ((info >>> i) & 1) === 1;
    const far = size - 11 + (i % 3);
    const near = Math.floor(i / 3);
    canvas.set(far, near, bit);
    canvas.set(near, far, bit);
  }
}

function placeData(canvas: Canvas, codewords: Uint8Array) {
  const { size } = canvas;
  let bit = 0;
  for (let right = size - 1; right >= 1; right -= 2) {
    if (right === 6) right = 5;           // the vertical timing column is not data
    for (let step = 0; step < size; step += 1) {
      for (let half = 0; half < 2; half += 1) {
        const col = right - half;
        const upward = ((right + 1) & 2) === 0;
        const row = upward ? size - 1 - step : step;
        if (canvas.fixed[row * size + col]) continue;
        let dark = false;
        if (bit < codewords.length * 8) {
          dark = ((codewords[bit >>> 3] >>> (7 - (bit & 7))) & 1) === 1;
          bit += 1;
        }
        canvas.set(row, col, dark, false);
      }
    }
  }
}

/** The four penalty rules; the mask with the lowest total wins. */
function penalty(canvas: Canvas): number {
  const { size } = canvas;
  let score = 0;

  const runScore = (run: number) => (run >= 5 ? run - 2 : 0);
  for (let a = 0; a < size; a += 1) {
    let rowRun = 1;
    let colRun = 1;
    for (let b = 1; b < size; b += 1) {
      if (canvas.isDark(a, b) === canvas.isDark(a, b - 1)) rowRun += 1;
      else { score += runScore(rowRun); rowRun = 1; }
      if (canvas.isDark(b, a) === canvas.isDark(b - 1, a)) colRun += 1;
      else { score += runScore(colRun); colRun = 1; }
    }
    score += runScore(rowRun) + runScore(colRun);
  }

  for (let r = 0; r < size - 1; r += 1) {
    for (let c = 0; c < size - 1; c += 1) {
      const first = canvas.isDark(r, c);
      if (first === canvas.isDark(r, c + 1) && first === canvas.isDark(r + 1, c) &&
        first === canvas.isDark(r + 1, c + 1)) score += 3;
    }
  }

  // 1:1:3:1:1 finder-lookalikes, with four light modules on either side.
  const pattern = [true, false, true, true, true, false, true];
  const matches = (get: (i: number) => boolean, start: number) => {
    for (let i = 0; i < 7; i += 1) if (get(start + i) !== pattern[i]) return false;
    const clear = (from: number) => {
      for (let i = 0; i < 4; i += 1) {
        const at = from + i;
        if (at >= 0 && at < size && get(at)) return false;
      }
      return true;
    };
    return clear(start - 4) || clear(start + 7);
  };
  for (let a = 0; a < size; a += 1) {
    for (let b = 0; b + 7 <= size; b += 1) {
      if (matches((i) => canvas.isDark(a, i), b)) score += 40;
      if (matches((i) => canvas.isDark(i, a), b)) score += 40;
    }
  }

  let dark = 0;
  for (let i = 0; i < canvas.dark.length; i += 1) dark += canvas.dark[i];
  const percent = (dark * 100) / canvas.dark.length;
  score += Math.floor(Math.abs(percent - 50) / 5) * 10;
  return score;
}

function applyFormat(canvas: Canvas, mask: number) {
  const { size } = canvas;
  const bits = FORMAT_M[mask];
  const at = (i: number) => ((bits >>> i) & 1) === 1;
  for (let i = 0; i <= 5; i += 1) canvas.set(8, i, at(i));
  canvas.set(8, 7, at(6));
  canvas.set(8, 8, at(7));
  canvas.set(7, 8, at(8));
  for (let i = 9; i <= 14; i += 1) canvas.set(14 - i, 8, at(i));
  for (let i = 0; i <= 7; i += 1) canvas.set(size - 1 - i, 8, at(i));
  for (let i = 8; i <= 14; i += 1) canvas.set(8, size - 15 + i, at(i));
  canvas.set(size - 8, 8, true);
}

/**
 * Encodes `text` and returns the module grid, row-major, true where dark.
 * The quiet zone is not included; renderers add their own margin.
 */
export function qrMatrix(text: string): boolean[][] {
  const bytes = new TextEncoder().encode(text);
  const version = SPECS.findIndex((spec, index) => {
    const countBits = index + 1 < 10 ? 8 : 16;
    return 4 + countBits + bytes.length * 8 <= dataCapacity(spec) * 8;
  }) + 1;
  if (version === 0) throw new Error("内容过长，无法生成二维码");

  const spec = SPECS[version - 1];
  const codewords = interleave(encodeData(bytes, version, dataCapacity(spec)), spec);

  let best: Canvas | null = null;
  let bestScore = Infinity;
  for (let mask = 0; mask < 8; mask += 1) {
    const canvas = new Canvas(version);
    drawFunctionPatterns(canvas, version);
    placeData(canvas, codewords);
    for (let row = 0; row < canvas.size; row += 1) {
      for (let col = 0; col < canvas.size; col += 1) {
        if (canvas.fixed[row * canvas.size + col]) continue;
        if (MASKS[mask](row, col)) canvas.dark[row * canvas.size + col] ^= 1;
      }
    }
    applyFormat(canvas, mask);
    const score = penalty(canvas);
    if (score < bestScore) { bestScore = score; best = canvas; }
  }

  const chosen = best!;
  return Array.from({ length: chosen.size }, (_unused, row) =>
    Array.from({ length: chosen.size }, (_ignored, col) => chosen.isDark(row, col)));
}

/**
 * An SVG path covering every dark module, as horizontal runs so the document
 * stays small. One module is one user unit; the viewBox includes a 4-module
 * quiet zone, which is what the spec asks for.
 */
export function qrSvgPath(matrix: boolean[][]): string {
  const parts: string[] = [];
  for (let row = 0; row < matrix.length; row += 1) {
    let start = -1;
    for (let col = 0; col <= matrix.length; col += 1) {
      const dark = col < matrix.length && matrix[row][col];
      if (dark && start < 0) start = col;
      else if (!dark && start >= 0) {
        parts.push(`M${start + 4} ${row + 4}h${col - start}v1h-${col - start}z`);
        start = -1;
      }
    }
  }
  return parts.join("");
}
