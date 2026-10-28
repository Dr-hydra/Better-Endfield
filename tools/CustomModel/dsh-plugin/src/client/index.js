/**
 * bem-preview — browser half.
 *
 * Registers one entry in the conversation view ring ("预览") and renders the
 * last package the `bem` tool registered. The scene comes from the host as
 * JSON plus binary blobs; textures are decoded off-thread by the BCn worker.
 *
 * Written without JSX so the file ships as-is: the build step only wraps it in
 * the harness module-loader envelope and inlines the vendored decoder.
 */
const React = require('react')
const h = React.createElement

const NS = 'bem-preview'
/**
 * Absolute, because the host registers this prefix on the webserver and dsh
 * serves the web app at the origin root — so this resolves to the same place as
 * the document-relative form the built-in clients use, without depending on
 * where the document happens to sit.
 */
const API = '/dsh-bem/'

const zh = {
  resources: "预览资源",
  resourceReady: "已启用",
  resourceMissing: "未启用可用的预览资源",
  resourceCharacters: "个角色 / 变体",
  astcAvailable: "当前浏览器支持 ASTC，可选择 ASTC 以减少解码开销，也可使用 BC7。",
  astcUnavailable: "当前浏览器不支持 ASTC，建议选择 BC7。由你选择，不按服务器系统决定。",
  resourceFormat: "资源包格式",
  resourcePath: "DSH 运行端的本地目录",
  resourcePathHint: "resources 根目录或含 manifest.json 的包目录",
  resourceApply: "应用并刷新预览",
  resourceLocalOnly: "仅读取已解压的本地资源，不自动下载。路径设置仅用于当前 DSH 会话；长期使用可设置 BEM_PREVIEW_RESOURCES 和 BEM_PREVIEW_PLATFORM。",
  resourceRelease: "打开资源发布页",
  nativeCompletion: "原生补全",
  native_ready: "已就绪",
  native_not_installed: "未安装预览资源",
  native_character_not_covered: "资源未覆盖此角色",
  native_identity_mismatch: "角色部件身份不匹配",
  native_invalid_package: "资源包无效",
  native_invalid_character: "部分角色资源无效",
  native_pinned_version_missing: "固定的资源版本不存在",

  'view.preview': '预览',
  empty: '还没有可预览的包',
  emptyHint: '让 agent 调用 bem 工具：{ action: "preview", path: "…/xxx.bem" }，这里会立刻出现模型。',
  unreachable: '预览服务连不上',
  unreachableHint: '宿主侧的 /dsh-bem/ 路由没有响应。让 agent 重跑一次 bem preview；如果还是这样，把上面这行原文发给它。',
  loading: '读取场景…',
  failed: '场景读取失败',
  expired: '预览已过期，重新让 agent 运行一次 preview',
  appearance: '外观',
  options: '选项',
  parts: '部件',
  parameters: '形态参数',
  textures: '贴图',
  stats: '统计',
  vertices: '顶点',
  triangles: '三角面',
  draws: '绘制',
  texturesCount: '贴图',
  decoded: '贴图',
  decoding: '解码贴图',
  decodeFailed: '张解码失败',
  clay: '素模',
  noTexture: '无贴图',
  astc: 'ASTC 贴图需要浏览器 GPU 支持，不支持时可选 BC7',
  astcCount: '{n} 张 ASTC 贴图需要浏览器 GPU 支持，不支持时可选 BC7',
  reset: '重置视角',
  controlsMouse: '左键旋转 · 右键/中键或 Shift+拖动平移 · 滚轮缩放',
  controlsTouch: '单指旋转 · 双指平移/缩放',
  showAll: '全部显示',
  hideNone: '全部隐藏',
  note: '显示包内网格；本地资源可按精确身份补全保留的原生部件。',
}

const en = {
  resources: "Preview resources",
  resourceReady: "Active",
  resourceMissing: "No usable preview resources selected",
  resourceCharacters: "characters / variants",
  astcAvailable: "This browser supports ASTC. Select ASTC to avoid BC decoding, or use BC7.",
  astcUnavailable: "This browser lacks ASTC support; BC7 is recommended. The choice is yours, independent of the server OS.",
  resourceFormat: "Pack format",
  resourcePath: "Local directory on the DSH host",
  resourcePathHint: "resources root or pack directory containing manifest.json",
  resourceApply: "Apply and refresh preview",
  resourceLocalOnly: "Reads extracted local files only; never downloads automatically. Selection lasts for this DSH session; use BEM_PREVIEW_RESOURCES and BEM_PREVIEW_PLATFORM for persistent configuration.",
  resourceRelease: "Open resource release",
  nativeCompletion: "Native completion",
  native_ready: "Ready",
  native_not_installed: "Preview resources not installed",
  native_character_not_covered: "Character not covered",
  native_identity_mismatch: "Component identities do not match",
  native_invalid_package: "Invalid resource pack",
  native_invalid_character: "Some character resources are invalid",
  native_pinned_version_missing: "Pinned resource version is missing",

  'view.preview': 'Preview',
  empty: 'No preview yet',
  emptyHint: 'Ask the agent to call the bem tool: { action: "preview", path: "…/xxx.bem" }.',
  unreachable: 'Preview service unreachable',
  unreachableHint: 'The host did not answer on /dsh-bem/. Run bem preview again; if it persists, send the line above to the agent.',
  loading: 'Loading scene…',
  failed: 'Failed to load the scene',
  expired: 'preview expired — run preview again',
  appearance: 'Appearance',
  options: 'Options',
  parts: 'Parts',
  parameters: 'Shape',
  textures: 'Textures',
  stats: 'Stats',
  vertices: 'vertices',
  triangles: 'triangles',
  draws: 'draws',
  texturesCount: 'textures',
  decoded: 'textures',
  decoding: 'decoding textures',
  decodeFailed: 'failed to decode',
  clay: 'Clay',
  noTexture: 'No texture',
  astc: 'ASTC requires GPU support; select BC7 if unavailable',
  astcCount: '{n} ASTC requires GPU support; select BC7 if unavailable',
  reset: 'Reset view',
  controlsMouse: 'Left drag: orbit · Right/middle or Shift+drag: pan · Wheel: zoom',
  controlsTouch: 'One finger: orbit · Two fingers: pan/pinch',
  showAll: 'Show all',
  hideNone: 'Hide all',
  note: 'Shows package meshes and, when local resources match, untouched original components.',
}

/* ------------------------------------------------------------------ */
/* condition grammar (mirrors lib/bem/scene.js)                        */
/* ------------------------------------------------------------------ */

function evalWhen(when, state) {
  if (!when || typeof when !== 'object') return true
  if (Array.isArray(when.eq)) return state[when.eq[0]] === when.eq[1]
  if (Array.isArray(when.all)) return when.all.every((node) => evalWhen(node, state))
  if (Array.isArray(when.any)) return when.any.some((node) => evalWhen(node, state))
  if (when.not !== undefined) return !evalWhen(when.not, state)
  return true
}

function defaultOptions(groups) {
  const state = {}
  for (const group of groups || []) state[group.id] = group.default ?? group.choices?.[0]?.id ?? null
  return state
}

function meshesForRules(rules, state) {
  const keep = new Set()
  for (const rule of rules || []) {
    const chosen = (rule.candidates || []).find((candidate) => evalWhen(candidate.when, state)) || null
    if (chosen && chosen.operation === 'replace' && Number.isInteger(chosen.mesh)) keep.add(chosen.mesh)
  }
  return keep
}

function meshesForAppearance(appearance) {
  const keep = new Set()
  for (const component of appearance?.components || []) {
    if (component.operation === 'replace' && Number.isInteger(component.mesh)) keep.add(component.mesh)
  }
  return keep
}

/* ------------------------------------------------------------------ */
/* math                                                                */
/* ------------------------------------------------------------------ */

function perspective(out, fovy, aspect, near, far) {
  const f = 1 / Math.tan(fovy / 2)
  out.fill(0)
  out[0] = f / aspect
  out[5] = f
  out[10] = (far + near) / (near - far)
  out[11] = -1
  out[14] = (2 * far * near) / (near - far)
  return out
}

function lookAt(out, eye, center, up) {
  let zx = eye[0] - center[0]
  let zy = eye[1] - center[1]
  let zz = eye[2] - center[2]
  let len = Math.hypot(zx, zy, zz) || 1
  zx /= len; zy /= len; zz /= len
  let xx = up[1] * zz - up[2] * zy
  let xy = up[2] * zx - up[0] * zz
  let xz = up[0] * zy - up[1] * zx
  len = Math.hypot(xx, xy, xz) || 1
  xx /= len; xy /= len; xz /= len
  const yx = zy * xz - zz * xy
  const yy = zz * xx - zx * xz
  const yz = zx * xy - zy * xx
  out[0] = xx; out[1] = yx; out[2] = zx; out[3] = 0
  out[4] = xy; out[5] = yy; out[6] = zy; out[7] = 0
  out[8] = xz; out[9] = yz; out[10] = zz; out[11] = 0
  out[12] = -(xx * eye[0] + xy * eye[1] + xz * eye[2])
  out[13] = -(yx * eye[0] + yy * eye[1] + yz * eye[2])
  out[14] = -(zx * eye[0] + zy * eye[1] + zz * eye[2])
  out[15] = 1
  return out
}

/** Area-weighted vertex normals: the package stores packed normals we do not decode. */
function computeNormals(positions, indexArrays) {
  const normals = new Float32Array(positions.length)
  for (const indices of indexArrays) {
    for (let i = 0; i < indices.length; i += 3) {
      const a = indices[i] * 3
      const b = indices[i + 1] * 3
      const c = indices[i + 2] * 3
      const abx = positions[b] - positions[a]
      const aby = positions[b + 1] - positions[a + 1]
      const abz = positions[b + 2] - positions[a + 2]
      const acx = positions[c] - positions[a]
      const acy = positions[c + 1] - positions[a + 1]
      const acz = positions[c + 2] - positions[a + 2]
      const nx = aby * acz - abz * acy
      const ny = abz * acx - abx * acz
      const nz = abx * acy - aby * acx
      if (a + 2 < normals.length) { normals[a] += nx; normals[a + 1] += ny; normals[a + 2] += nz }
      if (b + 2 < normals.length) { normals[b] += nx; normals[b + 1] += ny; normals[b + 2] += nz }
      if (c + 2 < normals.length) { normals[c] += nx; normals[c + 1] += ny; normals[c + 2] += nz }
    }
  }
  for (let i = 0; i < normals.length; i += 3) {
    const len = Math.hypot(normals[i], normals[i + 1], normals[i + 2]) || 1
    normals[i] /= len; normals[i + 1] /= len; normals[i + 2] /= len
  }
  return normals
}

function boundsOf(positions) {
  const min = [Infinity, Infinity, Infinity]
  const max = [-Infinity, -Infinity, -Infinity]
  for (let i = 0; i < positions.length; i += 3) {
    for (let c = 0; c < 3; c += 1) {
      const value = positions[i + c]
      if (value < min[c]) min[c] = value
      if (value > max[c]) max[c] = value
    }
  }
  if (!Number.isFinite(min[0])) return { min: [0, 0, 0], max: [0, 0, 0], center: [0, 0, 0], radius: 1 }
  const center = [(min[0] + max[0]) / 2, (min[1] + max[1]) / 2, (min[2] + max[2]) / 2]
  const radius = Math.max(
    0.001,
    0.5 * Math.hypot(max[0] - min[0], max[1] - min[1], max[2] - min[2]),
  )
  return { min, max, center, radius }
}

/* ------------------------------------------------------------------ */
/* stage                                                               */
/* ------------------------------------------------------------------ */

const VERTEX_SHADER = `#version 300 es
in vec3 aPosition;
in vec3 aNormal;
in vec2 aUv;
uniform mat4 uProjection;
uniform mat4 uView;
uniform vec4 uUvTransform;
out vec3 vNormal;
out vec2 vUv;
out vec3 vWorld;
void main() {
  vec4 world = vec4(aPosition, 1.0);
  vWorld = world.xyz;
  vNormal = aNormal;
  vUv = aUv * uUvTransform.xy + uUvTransform.zw;
  gl_Position = uProjection * uView * world;
}`

/**
 * Lit in linear space, encoded once at the end.
 *
 * Two mistakes used to make this look unlit: the albedo is sampled through
 * `SRGB8_ALPHA8`, so it arrives linear, and the drawing buffer is *not* sRGB —
 * writing linear values made every mid-tone about a stop dark, and the visible
 * side of a model was left with nothing but the 0.42 ambient term. The encode
 * at the bottom of `main` is what restores the albedo the artist authored.
 *
 * Normals face the viewer instead of trusting the index order. Measured on the
 * packages in this repo, only ~64% of triangles wind outward by the centroid
 * test and the meshes are split along seams, so `gl_FrontFacing` is not a
 * reliable "this side is the outside" signal — while a normal pointing away
 * from the camera always is. Culling is off, so nothing else depends on it.
 */
const FRAGMENT_SHADER = `#version 300 es
precision highp float;
in vec3 vNormal;
in vec2 vUv;
in vec3 vWorld;
uniform sampler2D uAlbedo;
uniform int uHasAlbedo;
uniform vec3 uClay;
uniform vec3 uCamera;
out vec4 outColor;

// normalize(vec3(0.40, 0.72, 0.57)) and normalize(vec3(-0.62, 0.28, 0.38)).
const vec3 KEY_DIR = vec3(0.3994, 0.7189, 0.5691);
const vec3 FILL_DIR = vec3(-0.7957, 0.3593, 0.4877);

void main() {
  vec3 base = uHasAlbedo == 1 ? texture(uAlbedo, vUv).rgb : uClay;
  vec3 view = normalize(uCamera - vWorld);
  vec3 n = dot(vNormal, vNormal) > 1e-12 ? normalize(vNormal) : view;
  // Double-sided and seam-split geometry: light whichever side faces the viewer.
  if (dot(n, view) < 0.0) n = -n;
  float key = max(dot(n, KEY_DIR), 0.0);
  float fill = max(dot(n, FILL_DIR), 0.0);
  // Hemisphere term, keyed off world up (the host already rotated to Y-up).
  float sky = 0.5 + 0.5 * n.y;
  // Ambient is kept low on purpose: with 0.42 + 0.58 * key the whole model sat
  // within one shade of itself, which is what read as "no lighting" even after
  // the gamma fix. The key has to carry the form.
  vec3 color = base * (0.20 + 0.80 * key + 0.14 * fill + 0.10 * sky * sky);
  float rim = pow(1.0 - max(dot(n, view), 0.0), 3.0);
  color += vec3(0.10, 0.12, 0.16) * rim;
  color = clamp(color, 0.0, 1.0);
  outColor = vec4(pow(color, vec3(1.0 / 2.2)), 1.0);
}`

function compile(gl, type, source) {
  const shader = gl.createShader(type)
  gl.shaderSource(shader, source)
  gl.compileShader(shader)
  if (!gl.getShaderParameter(shader, gl.COMPILE_STATUS)) {
    throw new Error(`shader: ${gl.getShaderInfoLog(shader)}`)
  }
  return shader
}

/**
 * Decode pool.
 *
 * One 4096x2048 BC7 base level takes ~4.7 s single threaded, and a textured
 * character carries five of them, so a single worker would leave the stage
 * clay-coloured for twenty seconds. Workers are capped at four: past that the
 * decode is memory-bandwidth bound and the extra workers only compete.
 *
 * Decoded pixels are dropped once uploaded — five base levels as RGBA is 160 MB
 * of CPU copies on top of the GPU textures, which is what pushes a phone
 * WebView into an out-of-memory kill.
 */
class Decoder {
  constructor() {
    this.workers = []
    this.cursor = 0
    this.pending = new Map()
    this.cache = new Map()
    this.sequence = 0
    this.failed = new Set()
  }

  ensureWorkers() {
    if (this.workers.length > 0) return this.workers
    const count = Math.max(1, Math.min(4, (navigator.hardwareConcurrency || 4) - 1))
    for (let i = 0; i < count; i += 1) {
      const worker = new Worker(`${API}assets/decode-worker.js`, { type: 'module' })
      worker.onmessage = (event) => this.settle(event.data || {})
      this.workers.push(worker)
    }
    return this.workers
  }

  settle({ id, ok, data, width, height, error }) {
    const entry = this.pending.get(id)
    if (!entry) return
    this.pending.delete(id)
    if (ok) {
      this.cache.set(entry.key, { width, height, data: new Uint8Array(data) })
      entry.resolve(this.cache.get(entry.key))
    } else {
      this.failed.add(entry.key)
      entry.reject(new Error(error || 'decode failed'))
    }
  }

  request(sceneId, texture) {
    const key = `${sceneId}/${texture.index}`
    if (this.cache.has(key)) return Promise.resolve(this.cache.get(key))
    if (this.failed.has(key)) return Promise.reject(new Error('decode failed earlier'))
    if (!texture.blob) return Promise.reject(new Error(texture.skipped || 'no payload'))
    const workers = this.ensureWorkers()
    const worker = workers[this.cursor % workers.length]
    this.cursor += 1
    const id = `d${this.sequence++}`
    return new Promise((resolve, reject) => {
      this.pending.set(id, { key, resolve, reject })
      worker.postMessage({
        id,
        url: `${API}api/blob/${sceneId}/${texture.blob}`,
        family: texture.family,
        width: texture.width,
        height: texture.height,
      })
    })
  }

  forget(sceneId, textureIndex) {
    this.cache.delete(`${sceneId}/${textureIndex}`)
  }

  dispose() {
    for (const worker of this.workers) worker.terminate()
    this.workers = []
  }
}

/** Vertical field of view. Also sets the framing distance in `resetView`. */
const FOV_Y = (38 * Math.PI) / 180

/**
 * Clay colour in linear space: the shader lights linearly and encodes at the
 * end, so passing the authored sRGB value straight in would render it pale.
 */
const CLAY = [0.62, 0.6, 0.58].map((value) => (value <= 0.04045
  ? value / 12.92
  : ((value + 0.055) / 1.055) ** 2.4))

class Stage {
  constructor(canvas, decoder) {
    this.canvas = canvas
    this.decoder = decoder
    const gl = canvas.getContext('webgl2', { antialias: true, alpha: false })
    if (!gl) throw new Error('WebGL2 is unavailable in this browser')
    this.gl = gl

    const program = gl.createProgram()
    gl.attachShader(program, compile(gl, gl.VERTEX_SHADER, VERTEX_SHADER))
    gl.attachShader(program, compile(gl, gl.FRAGMENT_SHADER, FRAGMENT_SHADER))
    gl.linkProgram(program)
    if (!gl.getProgramParameter(program, gl.LINK_STATUS)) {
      throw new Error(`program: ${gl.getProgramInfoLog(program)}`)
    }
    this.program = program
    this.loc = {
      projection: gl.getUniformLocation(program, 'uProjection'),
      view: gl.getUniformLocation(program, 'uView'),
      albedo: gl.getUniformLocation(program, 'uAlbedo'),
      hasAlbedo: gl.getUniformLocation(program, 'uHasAlbedo'),
      clay: gl.getUniformLocation(program, 'uClay'),
      camera: gl.getUniformLocation(program, 'uCamera'),
      uvTransform: gl.getUniformLocation(program, 'uUvTransform'),
    }

    this.meshes = []
    this.scene = null
    this.sceneId = null
    this.camera = { yaw: 0.45, pitch: 0.12, distance: 3.2, target: [0, 0, 0] }
    this.projection = new Float32Array(16)
    this.view = new Float32Array(16)
    this.running = true
    this.onStatus = null
    this.pendingDecodes = 0
    this.frame = 0

    gl.enable(gl.DEPTH_TEST)
    // Culling stays off: mod meshes are re-indexed by the converter and a
    // flipped winding would render as an empty stage rather than a wrong one.
    gl.disable(gl.CULL_FACE)

    this.bindEvents()
    const loop = () => {
      if (!this.running) return
      this.render()
      this.frame = requestAnimationFrame(loop)
    }
    this.frame = requestAnimationFrame(loop)
  }

  pan(dx, dy) {
    // Move along the camera's screen axes, at the current zoom level.
    const { yaw, pitch, distance, target } = this.camera
    const scale = 2 * distance * Math.tan(FOV_Y / 2) / Math.max(1, this.canvas.clientHeight)
    const right = [Math.cos(yaw), 0, -Math.sin(yaw)]
    const up = [-Math.sin(pitch) * Math.sin(yaw), Math.cos(pitch), -Math.sin(pitch) * Math.cos(yaw)]
    for (let axis = 0; axis < 3; axis++) target[axis] += (-dx * right[axis] + dy * up[axis]) * scale
  }

  bindEvents() {
    const canvas = this.canvas
    const pointers = new Map()
    let gesture = null
    const zoom = (distance) => { this.camera.distance = Math.max(0.05, Math.min(400, distance)) }
    const measure = () => {
      if (pointers.size < 2) return null
      const [a, b] = [...pointers.values()]
      return { x: (a.x + b.x) / 2, y: (a.y + b.y) / 2, distance: Math.hypot(a.x - b.x, a.y - b.y) }
    }
    const isPan = (event) => (event.buttons & 6) !== 0 || event.shiftKey
    canvas.style.cursor = 'grab'
    canvas.addEventListener('contextmenu', (event) => event.preventDefault())
    canvas.addEventListener('pointerdown', (event) => {
      if (event.pointerType === 'mouse' && ![0, 1, 2].includes(event.button)) return
      event.preventDefault()
      pointers.set(event.pointerId, { x: event.clientX, y: event.clientY })
      canvas.setPointerCapture(event.pointerId)
      // Establish the baseline when fingers enter/leave; never reuse old coordinates.
      gesture = measure()
      canvas.style.cursor = isPan(event) ? 'move' : 'grabbing'
    })
    canvas.addEventListener('pointermove', (event) => {
      const previous = pointers.get(event.pointerId)
      if (!previous) return
      pointers.set(event.pointerId, { x: event.clientX, y: event.clientY })
      if (pointers.size >= 2) {
        const next = measure()
        if (gesture) {
          this.pan(next.x - gesture.x, next.y - gesture.y)
          if (gesture.distance > 1 && next.distance > 1) zoom(this.camera.distance * gesture.distance / next.distance)
        }
        gesture = next
        return
      }
      const dx = event.clientX - previous.x
      const dy = event.clientY - previous.y
      if (isPan(event)) {
        this.pan(dx, dy)
        canvas.style.cursor = 'move'
      } else {
        this.camera.yaw -= dx * 0.008
        this.camera.pitch = Math.max(-1.45, Math.min(1.45, this.camera.pitch + dy * 0.008))
        canvas.style.cursor = 'grabbing'
      }
    })
    const release = (event) => {
      pointers.delete(event.pointerId)
      gesture = measure()
      canvas.style.cursor = pointers.size ? 'grabbing' : 'grab'
    }
    canvas.addEventListener('pointerup', release)
    canvas.addEventListener('pointercancel', release)
    canvas.addEventListener('lostpointercapture', release)
    canvas.addEventListener('wheel', (event) => {
      event.preventDefault()
      zoom(this.camera.distance * Math.exp(event.deltaY * 0.0012))
    }, { passive: false })
  }

  dispose() {
    this.running = false
    cancelAnimationFrame(this.frame)
    const gl = this.gl
    for (const mesh of this.meshes) {
      gl.deleteVertexArray(mesh.vao)
      gl.deleteBuffer(mesh.positionBuffer)
      gl.deleteBuffer(mesh.normalBuffer)
      if (mesh.uvBuffer) gl.deleteBuffer(mesh.uvBuffer)
      for (const buffer of mesh.indexBuffers.values()) gl.deleteBuffer(buffer)
    }
    this.meshes = []
  }

  resetView() {
    const all = new Float32Array(this.meshes.reduce((sum, mesh) => sum + mesh.base.length, 0))
    let offset = 0
    for (const mesh of this.meshes) {
      all.set(mesh.base, offset)
      offset += mesh.base.length
    }
    const { center, radius } = boundsOf(all.length ? all : new Float32Array(3))
    this.camera.target = center
    // Distance that fits the bounding sphere in the vertical field of view —
    // `radius * 2.4` cropped the head and feet of a standing character.
    this.camera.distance = (radius / Math.sin(FOV_Y / 2)) * 1.06
    this.camera.yaw = 0.45
    this.camera.pitch = 0.12
  }

  async load(scene, sceneId) {
    const gl = this.gl
    for (const mesh of this.meshes) {
      gl.deleteVertexArray(mesh.vao)
      gl.deleteBuffer(mesh.positionBuffer)
      gl.deleteBuffer(mesh.normalBuffer)
      if (mesh.uvBuffer) gl.deleteBuffer(mesh.uvBuffer)
      for (const buffer of mesh.indexBuffers.values()) gl.deleteBuffer(buffer)
    }
    this.meshes = []
    this.scene = scene
    this.sceneId = sceneId
    if (this.textureUnits) for (const handle of this.textureUnits.values()) gl.deleteTexture(handle)
    this.textureUnits = new Map()
    this.textureErrors = new Map()

    const buffers = new Map()
    const fetchBlob = async (blobId) => {
      if (buffers.has(blobId)) return buffers.get(blobId)
      const response = await fetch(`${API}api/blob/${sceneId}/${blobId}`)
      if (!response.ok) throw new Error(`blob ${blobId}: ${response.status}`)
      const data = new Uint8Array(await response.arrayBuffer())
      buffers.set(blobId, data)
      return data
    }

    for (const descriptor of scene.meshes) {
      if (!descriptor.positionsBlob) continue
      const positions = await fetchBlob(descriptor.positionsBlob)
      const base = new Float32Array(positions.buffer, positions.byteOffset, positions.byteLength / 4).slice()
      let uvs = null
      if (descriptor.uvsBlob) {
        const raw = await fetchBlob(descriptor.uvsBlob)
        uvs = new Float32Array(raw.buffer, raw.byteOffset, raw.byteLength / 4).slice()
      }
      const draws = []
      const indexArrays = new Map()
      const byteSize = descriptor.indexSize === 4 ? 4 : 2
      for (const draw of descriptor.draws) {
        let typed = indexArrays.get(draw.indicesBlob)
        if (!typed) {
          const raw = await fetchBlob(draw.indicesBlob)
          typed = descriptor.indexSize === 4
            ? new Uint32Array(raw.buffer, raw.byteOffset, raw.byteLength / 4).slice()
            : new Uint16Array(raw.buffer, raw.byteOffset, raw.byteLength / 2).slice()
          indexArrays.set(draw.indicesBlob, typed)
        }
        draws.push({
          blob: draw.indicesBlob,
          type: descriptor.indexSize === 4 ? gl.UNSIGNED_INT : gl.UNSIGNED_SHORT,
          count: draw.count,
          offset: (draw.first || 0) * byteSize,
          texture: draw.textures.length > 0 ? draw.textures[0] : null,
          when: draw.when,
          material: draw.material,
          uvTransform: draw.uvTransform || [1, 1, 0, 0],
          appearance: draw.appearance || null,
        })
      }
      const normals = computeNormals(base, [...indexArrays.values()])

      const vao = gl.createVertexArray()
      gl.bindVertexArray(vao)
      const positionBuffer = gl.createBuffer()
      gl.bindBuffer(gl.ARRAY_BUFFER, positionBuffer)
      gl.bufferData(gl.ARRAY_BUFFER, base, gl.DYNAMIC_DRAW)
      const positionLoc = gl.getAttribLocation(this.program, 'aPosition')
      gl.enableVertexAttribArray(positionLoc)
      gl.vertexAttribPointer(positionLoc, 3, gl.FLOAT, false, 0, 0)

      const normalBuffer = gl.createBuffer()
      gl.bindBuffer(gl.ARRAY_BUFFER, normalBuffer)
      gl.bufferData(gl.ARRAY_BUFFER, normals, gl.STATIC_DRAW)
      const normalLoc = gl.getAttribLocation(this.program, 'aNormal')
      gl.enableVertexAttribArray(normalLoc)
      gl.vertexAttribPointer(normalLoc, 3, gl.FLOAT, false, 0, 0)

      let uvBuffer = null
      const uvLoc = gl.getAttribLocation(this.program, 'aUv')
      if (uvs && uvs.length >= (base.length / 3) * 2) {
        uvBuffer = gl.createBuffer()
        gl.bindBuffer(gl.ARRAY_BUFFER, uvBuffer)
        gl.bufferData(gl.ARRAY_BUFFER, uvs, gl.STATIC_DRAW)
        gl.enableVertexAttribArray(uvLoc)
        gl.vertexAttribPointer(uvLoc, 2, gl.FLOAT, false, 0, 0)
      } else if (uvLoc >= 0) {
        gl.disableVertexAttribArray(uvLoc)
        gl.vertexAttrib2f(uvLoc, 0, 0)
      }

      const indexBuffers = new Map()
      for (const [blobId, typed] of indexArrays) {
        const buffer = gl.createBuffer()
        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, buffer)
        gl.bufferData(gl.ELEMENT_ARRAY_BUFFER, typed, gl.STATIC_DRAW)
        indexBuffers.set(blobId, buffer)
      }
      gl.bindVertexArray(null)

      this.meshes.push({
        index: descriptor.index,
        name: descriptor.name,
        base,
        live: base.slice(),
        normals,
        draws,
        indexBuffers,
        vao,
        positionBuffer,
        normalBuffer,
        uvBuffer,
        texture: null,
        textureState: 'none',
      })
    }

    this.resetView()
    this.visible = new Set(this.meshes.map((mesh) => mesh.index))
    this.applyParameters({})
    this.ensureTextures()
  }

  setVisibility(meshSet, optionState) {
    this.visible = meshSet
    this.optionState = optionState || {}
  }

  /** Blend the sparse deformation frames for the current parameter state. */
  applyParameters(state) {
    const gl = this.gl
    const scene = this.scene
    if (!scene) return
    const channels = new Map()
    for (const channel of scene.channels) {
      const value = state[channel.parameter]
      if (value === undefined) continue
      if (!channels.has(channel.mesh)) channels.set(channel.mesh, [])
      channels.get(channel.mesh).push({ channel, value })
    }
    for (const mesh of this.meshes) {
      const entries = channels.get(mesh.index) || []
      const target = mesh.live
      if (entries.length === 0) {
        target.set(mesh.base)
      } else {
        target.set(mesh.base)
        for (const { channel, value } of entries) {
          const frames = channel.frames
          let lower = frames[0]
          let upper = frames[frames.length - 1]
          for (let i = 0; i < frames.length - 1; i += 1) {
            if (value >= frames[i].value && value <= frames[i + 1].value) {
              lower = frames[i]
              upper = frames[i + 1]
              break
            }
          }
          if (value <= frames[0].value) { lower = frames[0]; upper = frames[0] }
          if (value >= frames[frames.length - 1].value) { lower = upper = frames[frames.length - 1] }
          const span = upper.value - lower.value
          const weightLower = span === 0 ? 1 : (upper.value - value) / span
          const weightUpper = span === 0 ? 0 : (value - lower.value) / span
          this.applyFrame(target, lower, weightLower)
          if (upper !== lower) this.applyFrame(target, upper, weightUpper)
        }
      }
      gl.bindBuffer(gl.ARRAY_BUFFER, mesh.positionBuffer)
      gl.bufferSubData(gl.ARRAY_BUFFER, 0, target)
    }
  }

  applyFrame(target, frame, weight) {
    if (!frame || frame.neutral || weight === 0 || !frame.data) return
    const { indices, deltas } = frame.data
    for (let i = 0; i < indices.length; i += 1) {
      const vertex = indices[i] * 3
      target[vertex] += deltas[i * 3] * weight
      target[vertex + 1] += deltas[i * 3 + 1] * weight
      target[vertex + 2] += deltas[i * 3 + 2] * weight
    }
  }

  /** Deformation frame buffers arrive as blobs like everything else. */
  async loadDeformations() {
    const scene = this.scene
    if (!scene || scene.channels.length === 0) return
    const fetchBlob = async (blobId) => {
      const response = await fetch(`${API}api/blob/${this.sceneId}/${blobId}`)
      if (!response.ok) throw new Error(`deformation blob ${blobId}: ${response.status}`)
      return response.arrayBuffer()
    }
    // Sequential per channel, parallel across channels: a channel's frames are
    // small (a few thousand vertices), so the limit is request count, not size.
    await Promise.all(scene.channels.map(async (channel) => {
      for (const frame of channel.frames) {
        if (frame.neutral || !frame.indicesBlob) continue
        const [indices, deltas] = await Promise.all([fetchBlob(frame.indicesBlob), fetchBlob(frame.deltasBlob)])
        frame.data = { indices: new Uint32Array(indices), deltas: new Float32Array(deltas) }
      }
    }))
  }

  async ensureTextures() {
    const scene = this.scene
    if (!scene) return
    const wanted = new Set()
    for (const mesh of this.meshes) {
      for (const draw of mesh.draws) {
        if (draw.texture !== null && draw.texture !== undefined) wanted.add(draw.texture)
      }
    }
    if (wanted.size === 0) return
    // Keep unsupported ASTC out of both fetch and BC-worker queues. The panel
    // and overlay explain the capability; unsupported formats are not failures.
    const astcAvailable = Boolean(this.gl.getExtension('WEBGL_compressed_texture_astc'))
    const requestable = [...wanted].filter((index) => scene.textures[index]?.blob && (scene.textures[index].family !== 'astc' || astcAvailable))
    this.pendingDecodes = requestable.length
    this.reportStatus()
    await Promise.all(requestable.map(async (index) => {
      const texture = scene.textures[index]
      try {
        if (texture.family === 'rgba32' || texture.family === 'astc') {
          const response = await fetch(`${API}api/blob/${this.sceneId}/${texture.blob}`)
          if (!response.ok) throw new Error(`texture HTTP ${response.status}`)
          const data = new Uint8Array(await response.arrayBuffer())
          this.uploadTexture(index, texture, data)
        } else {
          const decoded = await this.decoder.request(this.sceneId, texture)
          this.uploadTexture(index, texture, decoded.data)
          this.decoder.forget(this.sceneId, index)
        }
      } catch (error) {
        this.textureErrors = this.textureErrors || new Map()
        this.textureErrors.set(index, String((error && error.message) || error))
      } finally {
        this.pendingDecodes -= 1
        this.reportStatus()
      }
    }))
  }

  uploadTexture(index, texture, data) {
    const gl = this.gl
    const handle = gl.createTexture()
    gl.bindTexture(gl.TEXTURE_2D, handle)
    gl.pixelStorei(gl.UNPACK_ALIGNMENT, 1)
    if (texture.family === 'astc') {
      const extension = gl.getExtension('WEBGL_compressed_texture_astc')
      const key = `COMPRESSED_${texture.srgb ? 'SRGB8_ALPHA8' : 'RGBA'}_ASTC_${texture.block}x${texture.block}_KHR`
      const internal = extension?.[key]
      if (internal === undefined) { gl.deleteTexture(handle); throw new Error('ASTC texture format is not supported by this browser') }
      gl.compressedTexImage2D(gl.TEXTURE_2D, 0, internal, texture.width, texture.height, 0, data)
      if (gl.getError() !== gl.NO_ERROR) { gl.deleteTexture(handle); throw new Error('ASTC GPU upload failed; select BC7') }
      gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR)
    } else {
      const internal = texture.srgb ? gl.SRGB8_ALPHA8 : gl.RGBA8
      gl.texImage2D(gl.TEXTURE_2D, 0, internal, texture.width, texture.height, 0, gl.RGBA, gl.UNSIGNED_BYTE, data)
      gl.generateMipmap(gl.TEXTURE_2D)
      gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR_MIPMAP_LINEAR)
    }
    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MAG_FILTER, gl.LINEAR)
    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_S, gl.REPEAT)
    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_WRAP_T, gl.REPEAT)
    this.textureUnits.set(index, handle)
  }

  reportStatus() {
    if (this.onStatus) {
      this.onStatus({
        pending: this.pendingDecodes,
        decoded: this.textureUnits ? this.textureUnits.size : 0,
        errors: this.textureErrors || new Map(),
      })
    }
  }

  setTextureMode(enabled) {
    this.texturesEnabled = enabled
  }

  render() {
    const gl = this.gl
    const canvas = this.canvas
    const ratio = Math.min(window.devicePixelRatio || 1, 2)
    const width = Math.max(1, Math.floor(canvas.clientWidth * ratio))
    const height = Math.max(1, Math.floor(canvas.clientHeight * ratio))
    if (canvas.width !== width || canvas.height !== height) {
      canvas.width = width
      canvas.height = height
    }
    gl.viewport(0, 0, width, height)
    gl.clearColor(0.09, 0.095, 0.11, 1)
    gl.clear(gl.COLOR_BUFFER_BIT | gl.DEPTH_BUFFER_BIT)
    if (this.meshes.length === 0) return

    const { yaw, pitch, distance, target } = this.camera
    const eye = [
      target[0] + distance * Math.cos(pitch) * Math.sin(yaw),
      target[1] + distance * Math.sin(pitch),
      target[2] + distance * Math.cos(pitch) * Math.cos(yaw),
    ]
    perspective(this.projection, FOV_Y, width / height, distance / 400, distance * 40)
    lookAt(this.view, eye, target, [0, 1, 0])

    gl.useProgram(this.program)
    gl.uniformMatrix4fv(this.loc.projection, false, this.projection)
    gl.uniformMatrix4fv(this.loc.view, false, this.view)
    gl.uniform3fv(this.loc.camera, eye)
    gl.uniform3fv(this.loc.clay, CLAY)
    gl.activeTexture(gl.TEXTURE0)
    gl.uniform1i(this.loc.albedo, 0)

    const texturesOn = this.texturesEnabled !== false
    const state = this.optionState || {}
    for (const mesh of this.meshes) {
      if (this.visible && !this.visible.has(mesh.index)) continue
      gl.bindVertexArray(mesh.vao)
      for (const draw of mesh.draws) {
        if (draw.when && !evalWhen(draw.when, state)) continue
        if (draw.appearance && draw.appearance !== this.appearance) continue
        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, mesh.indexBuffers.get(draw.blob))
        gl.uniform4fv(this.loc.uvTransform, draw.uvTransform)
        const handle = texturesOn && draw.texture !== null ? this.textureUnits.get(draw.texture) : null
        if (handle) {
          gl.bindTexture(gl.TEXTURE_2D, handle)
          gl.uniform1i(this.loc.hasAlbedo, 1)
        } else {
          gl.uniform1i(this.loc.hasAlbedo, 0)
        }
        gl.drawElements(gl.TRIANGLES, draw.count, draw.type, draw.offset)
      }
    }
    gl.bindVertexArray(null)
  }
}

/* ------------------------------------------------------------------ */
/* view                                                                */
/* ------------------------------------------------------------------ */

const styles = {
  root: {
    display: 'flex',
    flexDirection: 'column',
    height: '100%',
    minHeight: 0,
    font: '13px/1.5 var(--font-sans, system-ui, sans-serif)',
    color: 'var(--color-text-primary, #e6e6e6)',
  },
  bar: {
    display: 'flex',
    alignItems: 'center',
    gap: 10,
    padding: '8px 12px',
    borderBottom: '1px solid var(--color-border-tertiary, rgba(255,255,255,0.12))',
    flexWrap: 'wrap',
  },
  title: { fontWeight: 500, fontSize: 13 },
  muted: { color: 'var(--color-text-secondary, #9aa0a6)', fontSize: 12 },
  body: { display: 'flex', flex: 1, minHeight: 0 },
  canvasWrap: { position: 'relative', flex: 1, minWidth: 0, background: '#171a20' },
  canvas: { width: '100%', height: '100%', display: 'block', touchAction: 'none', cursor: 'grab' },
  panel: {
    width: 244,
    flexShrink: 0,
    borderLeft: '1px solid var(--color-border-tertiary, rgba(255,255,255,0.12))',
    overflowY: 'auto',
    padding: '10px 12px 24px',
  },
  section: { marginBottom: 14 },
  sectionTitle: {
    fontSize: 11,
    letterSpacing: '0.04em',
    textTransform: 'uppercase',
    color: 'var(--color-text-secondary, #9aa0a6)',
    marginBottom: 6,
  },
  chipRow: { display: 'flex', flexWrap: 'wrap', gap: 6 },
  chip: {
    padding: '3px 9px',
    borderRadius: 999,
    border: '1px solid var(--color-border-secondary, rgba(255,255,255,0.22))',
    background: 'transparent',
    color: 'inherit',
    cursor: 'pointer',
    font: 'inherit',
    fontSize: 12,
  },
  chipActive: {
    background: 'var(--color-text-primary, #e6e6e6)',
    color: 'var(--color-background-primary, #111)',
    borderColor: 'transparent',
  },
  row: { display: 'flex', alignItems: 'center', gap: 8, marginBottom: 4 },
  rowLabel: { flex: 1, overflow: 'hidden', textOverflow: 'ellipsis', whiteSpace: 'nowrap', fontSize: 12 },
  range: { width: '100%', accentColor: 'var(--color-text-primary, #e6e6e6)' },
  overlay: {
    position: 'absolute',
    left: 12,
    bottom: 12,
    padding: '4px 8px',
    borderRadius: 6,
    background: 'rgba(0,0,0,0.45)',
    color: '#dfe3e8',
    fontSize: 11,
    pointerEvents: 'none',
  },
  empty: {
    flex: 1,
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
    flexDirection: 'column',
    gap: 8,
    padding: 32,
    textAlign: 'center',
    color: 'var(--color-text-secondary, #9aa0a6)',
  },
}

function ResourcePanel({ t, gl }) {
  const [open, setOpen] = React.useState(false)
  const [info, setInfo] = React.useState(null)
  const [path, setPath] = React.useState('')
  const [platform, setPlatform] = React.useState('windows')
  const [busy, setBusy] = React.useState(false)
  const [error, setError] = React.useState(null)
  const [astc, setAstc] = React.useState(null)
  React.useEffect(() => {
    if (!open) return undefined
    let cancelled = false
    const context = gl || document.createElement('canvas').getContext('webgl2')
    setAstc(Boolean(context?.getExtension('WEBGL_compressed_texture_astc')))
    if (!gl) context?.getExtension('WEBGL_lose_context')?.loseContext()
    setBusy(true)
    fetch(`${API}api/resources`, { cache: 'no-store' }).then(async (response) => {
      if (!response.ok) throw new Error(`resources HTTP ${response.status}`)
      const data = await response.json()
      if (!cancelled) { setInfo(data); setPath(data.path || ''); setPlatform(data.selectedPlatform || 'windows'); setError(null) }
    }).catch((failure) => { if (!cancelled) setError(failure.message) }).finally(() => { if (!cancelled) setBusy(false) })
    return () => { cancelled = true }
  }, [open])
  const apply = async () => {
    setBusy(true); setError(null)
    try {
      const response = await fetch(`${API}api/resources`, { method: 'POST', headers: { 'Content-Type': 'application/json' }, body: JSON.stringify({ path, platform }) })
      const data = await response.json()
      if (!response.ok || !data.ok) throw new Error(data.error || data.message || data.reason || `HTTP ${response.status}`)
      setInfo(data)
    } catch (failure) { setError(failure.message) } finally { setBusy(false) }
  }
  return h('details', { style: { ...styles.section, maxWidth: '100%' }, onToggle: (event) => setOpen(event.currentTarget.open) },
    h('summary', { style: { ...styles.sectionTitle, cursor: 'pointer' } }, t('resources')),
    open ? h('div', { style: { display: 'grid', gap: 8, paddingTop: 8 } },
      h('div', { style: styles.muted }, info?.status === 'ready' ? `${t('resourceReady')} · ${info.version} · ${info.characters} ${t('resourceCharacters')}` : t('resourceMissing')),
      h('div', { style: styles.muted }, t(astc ? 'astcAvailable' : 'astcUnavailable')),
      h('label', null, t('resourceFormat'), h('select', { value: platform, disabled: busy, onChange: (event) => setPlatform(event.target.value), style: { width: '100%', marginTop: 4 } },
        h('option', { value: 'windows' }, 'BC7 (Windows)'), h('option', { value: 'android' }, 'ASTC (Android)'))),
      h('label', null, t('resourcePath'), h('input', { value: path, disabled: busy, onChange: (event) => setPath(event.target.value), style: { width: '100%', boxSizing: 'border-box', marginTop: 4 }, placeholder: t('resourcePathHint') })),
      h('button', { style: styles.chip, disabled: busy || !path.trim(), onClick: apply }, t(busy ? 'loading' : 'resourceApply')),
      error ? h('div', { role: 'alert', style: { color: '#d66', overflowWrap: 'anywhere' } }, error) : null,
      h('div', { style: styles.muted }, t('resourceLocalOnly')),
      h('a', { href: 'https://github.com/Dr-hydra/Better-Endfield/releases/tag/game-resources', target: '_blank', rel: 'noreferrer' }, t('resourceRelease'))) : null)
}

function PreviewView() {
  const t = React.useMemo(() => (key) => {
    const dict = (typeof navigator !== 'undefined' && navigator.language || '').startsWith('zh') ? zh : en
    return dict[key] || zh[key] || key
  }, [])

  const [latest, setLatest] = React.useState(null)
  const [scene, setScene] = React.useState(null)
  const [error, setError] = React.useState(null)
  // Set only after the poll has failed several times in a row, so a reload does
  // not flash an error while the host is still coming up.
  const [unreachable, setUnreachable] = React.useState(null)
  const [status, setStatus] = React.useState({ pending: 0, decoded: 0, errors: new Map() })
  const [optionState, setOptionState] = React.useState({})
  const [appearance, setAppearance] = React.useState(null)
  const [hidden, setHidden] = React.useState({})
  const [paramState, setParamState] = React.useState({})
  const [texturesOn, setTexturesOn] = React.useState(true)
  const [frame, setFrame] = React.useState(0)

  const canvasRef = React.useRef(null)
  const stageRef = React.useRef(null)
  const decoderRef = React.useRef(null)
  const latestRef = React.useRef(null)

  // The canvas only exists once a scene is known, so the renderer cannot be
  // built on mount — it is created by the effect below, which runs after the
  // canvas has actually been committed to the DOM.
  React.useEffect(() => () => {
    if (stageRef.current) stageRef.current.dispose()
    if (decoderRef.current) decoderRef.current.dispose()
    stageRef.current = null
    decoderRef.current = null
  }, [])

  React.useEffect(() => {
    if (!scene || stageRef.current || !canvasRef.current) return
    if (!decoderRef.current) decoderRef.current = new Decoder()
    try {
      const stage = new Stage(canvasRef.current, decoderRef.current)
      stage.onStatus = (value) => setStatus({ pending: value.pending, decoded: value.decoded, errors: new Map(value.errors) })
      stageRef.current = stage
    } catch (failure) {
      setError(String(failure.message || failure))
    }
  }, [scene])

  React.useEffect(() => {
    let cancelled = false
    let failures = 0
    const tick = async () => {
      try {
        const response = await fetch(`${API}api/latest`, { cache: 'no-store' })
        // A bodyless 404 from the SPA fallback used to reach `response.json()`,
        // throw inside it, and get swallowed by the catch below — the tab then
        // sat on "waiting" forever with nothing in the console. Say what broke.
        if (!response.ok) throw new Error(`HTTP ${response.status}${response.statusText ? ` ${response.statusText}` : ''}`)
        const text = await response.text()
        if (!text) throw new Error('empty response body')
        const data = JSON.parse(text)
        if (cancelled) return
        failures = 0
        setUnreachable(null)
        if (data.id && data.id !== latestRef.current) {
          latestRef.current = data.id
          setLatest(data)
        } else if (!data.id && !latestRef.current) {
          setLatest({ id: null })
        }
      } catch (failure) {
        if (cancelled) return
        failures += 1
        // Three ticks ≈ 4.5 s: long enough that a reload does not flash an error,
        // short enough that a broken route is visible before the user gives up.
        if (failures >= 3) setUnreachable(`${API}api/latest → ${String(failure?.message || failure)}`)
      }
    }
    tick()
    const timer = setInterval(tick, 1500)
    return () => { cancelled = true; clearInterval(timer) }
  }, [])

  React.useEffect(() => {
    if (!latest?.id) return undefined
    let cancelled = false
    setError(null)
    setScene(null)
    fetch(`${API}api/scene/${latest.id}`, { cache: 'no-store' })
      .then((response) => {
        if (!response.ok) throw new Error(response.status === 410 ? t('expired') : `scene: ${response.status}`)
        return response.json()
      })
      .then((loaded) => {
        if (cancelled) return
        setOptionState(defaultOptions(loaded.optionGroups))
        setParamState(Object.fromEntries(loaded.parameters.map((parameter) => [parameter.id, parameter.default])))
        setAppearance(loaded.selection?.appearance ?? loaded.defaultAppearance ?? null)
        setHidden({})
        setScene({ ...loaded, previewId: latest.id })
      })
      .catch((failure) => { if (!cancelled) setError(String(failure.message || failure)) })
    return () => { cancelled = true }
  }, [latest?.id])

  /**
   * Upload one scene into the renderer.
   *
   * Declared after the stage effect so that within a single commit the canvas
   * is committed, the stage is created, and only then does geometry upload —
   * all three depend on `scene`, which is why they cannot be one effect.
   */
  React.useEffect(() => {
    const stage = stageRef.current
    // `latest` and `scene` change in separate commits, so the id has to match:
    // otherwise a new preview uploads the previous scene's geometry.
    if (!stage || !scene || !latest?.id || scene.previewId !== latest.id) return undefined
    let cancelled = false
    const defaults = Object.fromEntries(scene.parameters.map((parameter) => [parameter.id, parameter.default]))
    stage.load(scene, latest.id)
      .then(() => (cancelled ? null : stage.loadDeformations()))
      .then(() => {
        if (cancelled) return
        stage.applyParameters(defaults)
        setFrame((value) => value + 1)
      })
      .catch((failure) => { if (!cancelled) setError(String(failure.message || failure)) })
    return () => { cancelled = true }
  }, [scene, latest?.id])

  const activeMeshes = React.useMemo(() => {
    if (!scene) return new Set()
    let base
    if (scene.componentRules.length > 0) base = meshesForRules(scene.componentRules, optionState)
    else if (scene.appearances.length > 0) {
      const chosen = scene.appearances.find((item) => item.id === appearance) || scene.appearances[0]
      base = meshesForAppearance(chosen)
    } else base = new Set(scene.meshes.map((mesh) => mesh.index))
    // An all-hide choice is deliberately empty; never re-enable every mesh.
    const chosen = scene.appearances.find((item) => item.id === appearance) || scene.appearances[0]
    for (const item of scene.nativeMeshes || []) {
      const rule = scene.componentRules.find((rule) => rule.target === item.target)
      const operation = scene.componentRules.length
        ? rule?.candidates.find((candidate) => evalWhen(candidate.when, optionState))?.operation
        : chosen?.components.find((component) => component.target === item.target)?.operation
      if (!operation || operation === 'keep') base.add(item.index)
      else base.delete(item.index)
    }
    const result = new Set()
    for (const index of base) if (!hidden[index]) result.add(index)
    return result
  }, [scene, optionState, appearance, hidden])

  /**
   * A parameter can be scoped to an option choice (`available_when`), so the
   * slider list is filtered the same way draw visibility is. The value itself
   * is kept, so re-selecting the option restores the pose the user left.
   */
  const visibleParameters = React.useMemo(
    () => (scene?.parameters || []).filter((parameter) => evalWhen(parameter.availableWhen, optionState)),
    [scene, optionState],
  )

  React.useEffect(() => {
    const stage = stageRef.current
    if (!stage || !scene) return
    stage.setVisibility(activeMeshes, optionState)
    stage.appearance = appearance || scene.defaultAppearance || scene.appearances[0]?.id
  }, [activeMeshes, optionState, appearance, scene])

  React.useEffect(() => {
    const stage = stageRef.current
    if (!stage || !scene) return
    // Only parameters that are currently applicable contribute: a hidden slider
    // whose option is off must not keep deforming the mesh.
    const effective = Object.fromEntries(
      visibleParameters.map((parameter) => [parameter.id, paramState[parameter.id] ?? parameter.default]),
    )
    stage.applyParameters(effective)
  }, [paramState, visibleParameters, scene, frame])

  React.useEffect(() => {
    const stage = stageRef.current
    if (stage) stage.setTextureMode(texturesOn)
  }, [texturesOn])

  const stats = scene?.stats
  const decodeNote = status.pending > 0 ? `${t('decoding')}…` : null
  // Counted from what the host said it could not serve, not from a failed
  // request: an ASTC package reports 22 skipped textures and zero errors.
  const astcSupported = Boolean(stageRef.current?.gl.getExtension('WEBGL_compressed_texture_astc'))
  const astcCount = (scene?.textures || []).filter((texture) => texture.family === 'astc' && (!texture.blob || !astcSupported)).length

  // Before `error`: a service that has stopped answering invalidates whatever the
  // last scene load reported, and this is the state that would otherwise look
  // exactly like an empty stage.
  if (unreachable && !scene) {
    return h('div', { style: styles.empty },
      h('div', { style: styles.title }, t('unreachable')),
      h('div', { style: { maxWidth: 480, marginTop: 6, font: '12px/1.5 ui-monospace, SFMono-Regular, Menlo, monospace', opacity: 0.8 } }, unreachable),
      h('div', { style: { maxWidth: 480, marginTop: 8 } }, t('unreachableHint')))
  }

  if (error) {
    return h('div', { style: styles.empty },
      h('div', { style: styles.title }, t('failed')),
      h('div', null, error))
  }

  if (!latest?.id) {
    return h('div', { style: styles.empty },
      h('div', { style: styles.title }, t('empty')),
      h('div', { style: { maxWidth: 420 } }, t('emptyHint')),
      h(ResourcePanel, { t }))
  }

  if (!scene) {
    return h('div', { style: styles.empty }, h('div', null, t('loading')))
  }

  return h('div', { style: styles.root },
    h('div', { style: styles.bar },
      h('span', { style: styles.title }, scene.package.name || latest.name),
      h('span', { style: styles.muted }, `BEM ${scene.package.formatVersion}`),
      scene.package.version ? h('span', { style: styles.muted }, `v${scene.package.version}`) : null,
      h('span', { style: styles.muted },
        `${stats.vertices} ${t('vertices')} · ${Math.round(stats.draws)} ${t('draws')} · ${stats.textures} ${t('texturesCount')}`),
      h('span', { style: { flex: 1 } }),
      h('button', {
        style: { ...styles.chip, ...(texturesOn ? styles.chipActive : {}) },
        onClick: () => setTexturesOn((value) => !value),
      }, texturesOn ? t('textures') : t('clay')),
      h('button', { style: styles.chip, onClick: () => stageRef.current?.resetView() }, t('reset')),
    ),
    h('div', { style: styles.body },
      h('div', { style: styles.canvasWrap },
        h('canvas', { ref: canvasRef, style: styles.canvas, title: t((window.matchMedia?.('(pointer: coarse)')?.matches) ? 'controlsTouch' : 'controlsMouse') }),
        h('div', { style: styles.overlay },
          decodeNote || `${t('decoded')} ${status.decoded}/${stats.previewableTextures}`,
          astcCount > 0 ? ` · ${t('astcCount').replace('{n}', String(astcCount))}` : '',
          status.errors.size > 0 ? ` · ${status.errors.size} ${t('decodeFailed')}` : ''),
      ),
      h('div', { style: styles.panel },
        h(ResourcePanel, { t, gl: stageRef.current?.gl }),
        scene.nativeCompletion ? h('div', { style: { ...styles.muted, padding: '4px 0 12px', overflowWrap: 'anywhere' }, title: scene.nativeCompletion.message || scene.nativeCompletion.reason },
          `${t('nativeCompletion')}: ${t('native_' + scene.nativeCompletion.reason)} · ${scene.nativeCompletion.meshes} ${t('parts')}`) : null,
        scene.appearances.length > 0
          ? h('div', { style: styles.section },
            h('div', { style: styles.sectionTitle }, t('appearance')),
            h('div', { style: styles.chipRow },
              scene.appearances.map((item) => h('button', {
                key: item.id,
                style: { ...styles.chip, ...(appearance === item.id ? styles.chipActive : {}) },
                onClick: () => setAppearance(item.id),
              }, item.name))))
          : null,

        scene.optionGroups.length > 0
          ? h('div', { style: styles.section },
            h('div', { style: styles.sectionTitle }, t('options')),
            scene.optionGroups.map((group) => h('div', { key: group.id, style: { marginBottom: 8 } },
              h('div', { style: styles.rowLabel, title: group.name }, group.name),
              h('div', { style: styles.chipRow },
                group.choices.map((choice) => h('button', {
                  key: choice.id,
                  style: {
                    ...styles.chip,
                    fontSize: 11,
                    padding: '2px 7px',
                    ...(optionState[group.id] === choice.id ? styles.chipActive : {}),
                  },
                  onClick: () => setOptionState((current) => ({ ...current, [group.id]: choice.id })),
                }, choice.name))))))
          : null,

        visibleParameters.length > 0
          ? h('div', { style: styles.section },
            h('div', { style: styles.sectionTitle }, t('parameters')),
            visibleParameters.map((parameter) => h('div', { key: parameter.id, style: { marginBottom: 8 } },
              h('div', { style: styles.row },
                h('span', { style: styles.rowLabel, title: parameter.name }, parameter.name),
                h('span', { style: styles.muted }, String(paramState[parameter.id] ?? parameter.default))),
              h('input', {
                type: 'range',
                min: parameter.min,
                max: parameter.max,
                step: parameter.step,
                value: paramState[parameter.id] ?? parameter.default,
                style: styles.range,
                onChange: (event) => {
                  const value = Number(event.target.value)
                  setParamState((current) => ({ ...current, [parameter.id]: value }))
                },
              }))))
          : null,

        h('div', { style: styles.section },
          h('div', { style: styles.sectionTitle }, t('parts')),
          h('div', { style: { ...styles.chipRow, marginBottom: 6 } },
            h('button', {
              style: { ...styles.chip, fontSize: 11, padding: '2px 7px' },
              onClick: () => setHidden({}),
            }, t('showAll')),
            h('button', {
              style: { ...styles.chip, fontSize: 11, padding: '2px 7px' },
              onClick: () => setHidden(Object.fromEntries(scene.meshes.map((mesh) => [mesh.index, true]))),
            }, t('hideNone'))),
          scene.meshes.map((mesh) => h('label', { key: mesh.index, style: styles.row },
            h('input', {
              type: 'checkbox',
              checked: !hidden[mesh.index],
              onChange: (event) => setHidden((current) => ({ ...current, [mesh.index]: !event.target.checked })),
            }),
            h('span', { style: styles.rowLabel, title: mesh.name }, mesh.name),
            h('span', { style: styles.muted }, String(mesh.vertexCount))))),

        h('div', { style: { ...styles.muted, marginTop: 12, lineHeight: 1.45 } }, t('note')),
      )),
  )
}

/* ------------------------------------------------------------------ */
/* plugin                                                              */
/* ------------------------------------------------------------------ */

export const inject = ['slots', 'locale']

export function apply(ctx) {
  if (ctx.locale && typeof ctx.locale.register === 'function') {
    ctx.effect(() => ctx.locale.register(NS, { zh, en }), 'bem-preview: dictionaries')
  }
  const translate = ctx.locale && typeof ctx.locale.bind === 'function'
    ? ctx.locale.bind(NS)
    : (key) => zh[key] || key

  if (!ctx.slots || typeof ctx.slots.register !== 'function') {
    ctx.logger?.warn?.('bem-preview: no slot service; the preview tab was not registered')
    return
  }
  ctx.slots.inject('conversation.view', () => ctx.slots.register({
    name: 'conversation.view',
    id: 'bem-preview',
    order: 30,
    locale: NS,
    label: () => translate('view.preview'),
    inject: () => ({}),
  }, PreviewView))
}
