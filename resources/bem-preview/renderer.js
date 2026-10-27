// Extracted from the AGPL-3.0-only DSH BEM preview; see THIRD_PARTY.md.
function evalWhen(value) { return value !== false }
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
out vec3 vNormal;
out vec2 vUv;
out vec3 vWorld;
void main() {
  vec4 world = vec4(aPosition, 1.0);
  vWorld = world.xyz;
  vNormal = aNormal;
  vUv = aUv;
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
uniform vec4 uUvTransform;
uniform int uHasAlbedo;
uniform vec3 uClay;
uniform vec3 uCamera;
out vec4 outColor;

// normalize(vec3(0.40, 0.72, 0.57)) and normalize(vec3(-0.62, 0.28, 0.38)).
const vec3 KEY_DIR = vec3(0.3994, 0.7189, 0.5691);
const vec3 FILL_DIR = vec3(-0.7957, 0.3593, 0.4877);

void main() {
  vec3 base = uHasAlbedo == 1 ? texture(uAlbedo, vUv * uUvTransform.xy + uUvTransform.zw).rgb : uClay;
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
      const worker = new Worker(`/decode-worker.js`, { type: 'module' })
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
        url: `/data/${sceneId}/${texture.blob}`,
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
    const vertexShader=compile(gl, gl.VERTEX_SHADER, VERTEX_SHADER); gl.attachShader(program, vertexShader)
    const fragmentShader=compile(gl, gl.FRAGMENT_SHADER, FRAGMENT_SHADER); gl.attachShader(program, fragmentShader)
    gl.linkProgram(program)
    gl.deleteShader(vertexShader);gl.deleteShader(fragmentShader)
    if (!gl.getProgramParameter(program, gl.LINK_STATUS)) {
      throw new Error(`program: ${gl.getProgramInfoLog(program)}`)
    }
    this.program = program
    this.loc = {
      projection: gl.getUniformLocation(program, 'uProjection'),
      view: gl.getUniformLocation(program, 'uView'),
      albedo: gl.getUniformLocation(program, 'uAlbedo'),
      uvTransform: gl.getUniformLocation(program, 'uUvTransform'),
      hasAlbedo: gl.getUniformLocation(program, 'uHasAlbedo'),
      clay: gl.getUniformLocation(program, 'uClay'),
      camera: gl.getUniformLocation(program, 'uCamera'),
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
      if (!this.paused && !document.hidden) this.render()
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
    let bodyTap = null
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
      if (event.pointerType === 'touch' && this.bodyEditDraws?.size) {
        bodyTap = pointers.size ? null : { id: event.pointerId, x: event.clientX, y: event.clientY }
      } else bodyTap = null
      if (event.button === 0 && event.ctrlKey && this.bodyEditDraws?.size) {
        event.preventDefault(); event.stopPropagation();
        const hit = this.pickBodyTriangle(event.clientX, event.clientY);
        if (hit && this.onBodyTrianglePicked) this.onBodyTrianglePicked(hit);
        return;
      }
      if (event.pointerType === 'mouse' && ![0, 1, 2].includes(event.button)) return
      event.preventDefault()
      pointers.set(event.pointerId, { x: event.clientX, y: event.clientY })
      canvas.setPointerCapture(event.pointerId)
      // Establish the baseline when fingers enter/leave; never reuse old coordinates.
      gesture = measure()
      canvas.style.cursor = isPan(event) ? 'move' : 'grabbing'
    })
    canvas.addEventListener('pointermove', (event) => {
      if (bodyTap && Math.hypot(event.clientX-bodyTap.x,event.clientY-bodyTap.y)>6) bodyTap=null
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
      if (event.type === 'pointerup' && bodyTap?.id === event.pointerId && pointers.size === 1) {
        const hit=this.pickBodyTriangle(event.clientX,event.clientY)
        if(hit && this.onBodyTrianglePicked)this.onBodyTrianglePicked(hit)
      }
      bodyTap=null
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

  dispose(releaseContext = true) {
    this.running = false; this.scene = null
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
    if (this.textureUnits) for (const handle of this.textureUnits.values()) gl.deleteTexture(handle)
    gl.deleteProgram(this.program)
    if (releaseContext) gl.getExtension('WEBGL_lose_context')?.loseContext()
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
    const previousTextures = new Map()
    if (this.textureUnits) for (const [index, handle] of this.textureUnits) {
      const key = this.scene?.textures[index]?.cacheKey
      if (key) previousTextures.set(key, handle)
      else gl.deleteTexture(handle)
    }
    this.scene = scene
    this.sceneId = sceneId
    this.textureUnits = new Map()
    const retained = new Set()
    for (const texture of scene.textures) {
      const handle = previousTextures.get(texture.cacheKey)
      if (handle) { this.textureUnits.set(texture.index, handle); retained.add(handle) }
    }
    for (const handle of previousTextures.values()) if (!retained.has(handle)) gl.deleteTexture(handle)
    this.textureErrors = new Map()
    this.pendingDecodes = 0

    const buffers = new Map()
    const fetchBlob = async (blobId) => {
      if (buffers.has(blobId)) return buffers.get(blobId)
      const response = await fetch(`/data/${sceneId}/${blobId}`)
      if (!response.ok) throw new Error(`blob ${blobId}: ${response.status}`)
      const data = new Uint8Array(await response.arrayBuffer())
      buffers.set(blobId, data)
      return data
    }

    for (const descriptor of scene.meshes) {
      if (!descriptor.positionsBlob) continue
      const positions = await fetchBlob(descriptor.positionsBlob)
      const base = new Float32Array(positions.buffer, positions.byteOffset, positions.byteLength / 4).slice()
      if (scene.orientation?.rotated) for(let i=0;i<base.length;i+=3) {const y=base[i+1];base[i+1]=base[i+2];base[i+2]=-y}
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
          uvTransform: draw.uvTransform || [1, 1, 0, 0],
          when: draw.when,
          material: draw.material,
          segment: draw.segment,
          bodyDraw: draw.bodyDraw, bodyRegion: draw.bodyRegion, bodyTriangles: draw.bodyTriangles, indices: draw.bodyDraw ? typed : null, first: draw.first || 0,
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
      const response = await fetch(`/data/${this.sceneId}/${blobId}`)
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
    // A texture the host refused to serve is not a decode failure. ASTC has no
    // browser decoder, so the host marks it `blob: null` with a reason; asking
    // the worker anyway turned one explained decision into a wall of errors.
    const requestable = [...wanted].filter((index) => scene.textures[index]?.blob && !this.textureUnits.has(index))
    this.pendingDecodes = requestable.length
    this.reportStatus()
    await Promise.all(requestable.map(async (index) => {
      const texture = scene.textures[index]
      try {
        if (texture.gpuCompressed) {
          const response = await fetch('/data/'+this.sceneId+'/'+texture.blob)
          if(!response.ok)throw new Error('Compressed texture fetch failed: '+response.status)
          const data=new Uint8Array(await response.arrayBuffer())
          if(this.running && this.scene===scene)this.uploadCompressedTexture(index,texture,data)
        } else if (texture.family === 'rgba32') {
          const response = await fetch(`/data/${this.sceneId}/${texture.blob}`)
          const data = new Uint8Array(await response.arrayBuffer())
          if (this.running && this.scene === scene) this.uploadTexture(index, texture, data)
        } else {
          const decoded = await this.decoder.request(this.sceneId, texture)
          if (this.running && this.scene === scene) this.uploadTexture(index, texture, decoded.data)
          this.decoder.forget(this.sceneId, index)
        }
      } catch (error) {
        if (!this.running || this.scene !== scene) return
        this.textureErrors = this.textureErrors || new Map()
        this.textureErrors.set(index, String((error && error.message) || error))
        if(texture.gpuCompressed && this.running && this.scene===scene)this.onCompressedFailure?.()
      } finally {
        if (this.running && this.scene === scene) { this.pendingDecodes -= 1; this.reportStatus() }
      }
    }))
  }

  uploadCompressedTexture(index,texture,data) {
    const gl=this.gl,isAstc=texture.family==='astc'
    const ext=gl.getExtension(isAstc?'WEBGL_compressed_texture_astc':'EXT_texture_compression_bptc')
    if(!ext || (!isAstc && texture.family!=='bc7'))throw new Error('GPU compressed format is unavailable')
    const block=isAstc?texture.block:4
    if(![4,5,6].includes(block))throw new Error('Invalid compressed block size')
    const internal=isAstc?ext[(texture.srgb?'COMPRESSED_SRGB8_ALPHA8_ASTC_':'COMPRESSED_RGBA_ASTC_')+block+'x'+block+'_KHR']:
      texture.srgb?ext.COMPRESSED_SRGB_ALPHA_BPTC_UNORM_EXT:ext.COMPRESSED_RGBA_BPTC_UNORM_EXT
    if(!internal)throw new Error('GPU compressed format is unavailable')
    if(texture.width>gl.getParameter(gl.MAX_TEXTURE_SIZE)||texture.height>gl.getParameter(gl.MAX_TEXTURE_SIZE))throw new Error('Compressed texture exceeds GPU texture size')
    if(data.byteLength!==Math.ceil(texture.width/block)*Math.ceil(texture.height/block)*16)throw new Error('Invalid compressed block extent')
    const handle=gl.createTexture()
    if(!handle)throw new Error('Cannot allocate compressed texture')
    try {
      gl.bindTexture(gl.TEXTURE_2D,handle)
      gl.getError()
      gl.compressedTexImage2D(gl.TEXTURE_2D,0,internal,texture.width,texture.height,0,data)
      gl.texParameteri(gl.TEXTURE_2D,gl.TEXTURE_MIN_FILTER,gl.LINEAR)
      gl.texParameteri(gl.TEXTURE_2D,gl.TEXTURE_MAG_FILTER,gl.LINEAR)
      gl.texParameteri(gl.TEXTURE_2D,gl.TEXTURE_WRAP_S,gl.REPEAT)
      gl.texParameteri(gl.TEXTURE_2D,gl.TEXTURE_WRAP_T,gl.REPEAT)
      const error=gl.getError()
      if(error!==gl.NO_ERROR)throw new Error('GPU compressed upload failed: '+error)
      this.textureUnits.set(index,handle)
    }catch(error){gl.deleteTexture(handle);throw error}
  }

  uploadTexture(index, texture, data) {
    const gl = this.gl
    const handle = gl.createTexture()
    gl.bindTexture(gl.TEXTURE_2D, handle)
    gl.pixelStorei(gl.UNPACK_ALIGNMENT, 1)
    const internal = texture.srgb ? gl.SRGB8_ALPHA8 : gl.RGBA8
    gl.texImage2D(gl.TEXTURE_2D, 0, internal, texture.width, texture.height, 0, gl.RGBA, gl.UNSIGNED_BYTE, data)
    gl.generateMipmap(gl.TEXTURE_2D)
    gl.texParameteri(gl.TEXTURE_2D, gl.TEXTURE_MIN_FILTER, gl.LINEAR_MIPMAP_LINEAR)
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

  pickBodyTriangle(clientX, clientY) {
    const rect = this.canvas.getBoundingClientRect(); if (!rect.width || !rect.height) return null
    const { yaw, pitch, distance, target } = this.camera
    const sin = Math.sin, cos = Math.cos
    const eye = [target[0] + distance * cos(pitch) * sin(yaw), target[1] + distance * sin(pitch), target[2] + distance * cos(pitch) * cos(yaw)]
    const forward = [-cos(pitch) * sin(yaw), -sin(pitch), -cos(pitch) * cos(yaw)]
    const right = [cos(yaw), 0, -sin(yaw)], up = [-sin(pitch) * sin(yaw), cos(pitch), -sin(pitch) * cos(yaw)]
    const x = ((clientX - rect.left) / rect.width * 2 - 1) * Math.tan(FOV_Y / 2) * rect.width / rect.height
    const y = (1 - (clientY - rect.top) / rect.height * 2) * Math.tan(FOV_Y / 2)
    const ray = forward.map((v,i) => v + right[i]*x + up[i]*y)
    const length = Math.hypot(...ray); for(let i=0;i<3;i++) ray[i]/=length
    let best = Infinity, hit = null
    for (const mesh of this.meshes) {
      if (this.visible && !this.visible.has(mesh.index)) continue
      for (const draw of mesh.draws) {
        if (!draw.bodyDraw || !this.bodyEditDraws?.has(draw.bodyDraw) || (draw.when && !evalWhen(draw.when, this.optionState || {}))) continue
        const positions = mesh.live, indices = draw.indices
        for (let t=0;t<draw.count/3;t++) {
          const a=indices[draw.first+t*3]*3,b=indices[draw.first+t*3+1]*3,c=indices[draw.first+t*3+2]*3
          const e1x=positions[b]-positions[a],e1y=positions[b+1]-positions[a+1],e1z=positions[b+2]-positions[a+2]
          const e2x=positions[c]-positions[a],e2y=positions[c+1]-positions[a+1],e2z=positions[c+2]-positions[a+2]
          const px=ray[1]*e2z-ray[2]*e2y,py=ray[2]*e2x-ray[0]*e2z,pz=ray[0]*e2y-ray[1]*e2x
          const det=e1x*px+e1y*py+e1z*pz;if(Math.abs(det)<1e-10)continue
          const vx=eye[0]-positions[a],vy=eye[1]-positions[a+1],vz=eye[2]-positions[a+2]
          const u=(vx*px+vy*py+vz*pz)/det;if(u<0||u>1)continue
          const qx=vy*e1z-vz*e1y,qy=vz*e1x-vx*e1z,qz=vx*e1y-vy*e1x
          const w=(ray[0]*qx+ray[1]*qy+ray[2]*qz)/det;if(w<0||u+w>1)continue
          const distance=(e2x*qx+e2y*qy+e2z*qz)/det
          if(distance>0&&distance<best){best=distance;hit={bodyDraw:draw.bodyDraw,triangle:draw.bodyTriangles?.[t] ?? t}}
        }
      }
    }
    return hit
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
        if (this.bodyEditDraws?.size && !this.bodyEditDraws.has(draw.bodyDraw)) continue
        gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, mesh.indexBuffers.get(draw.blob))
        const highlighted = (this.highlightSegment && draw.segment === this.highlightSegment) || this.highlightSegments?.has(draw.segment)
        const bodyColors = {neck:[0.95,0.65,0.25],head:[0.95,0.4,0.55],torso:[0.3,0.7,0.95],pelvis:[0.65,0.4,0.9],left_arm:[0.3,0.85,0.5],right_arm:[0.15,0.7,0.65],left_leg:[0.9,0.75,0.3],right_leg:[0.8,0.45,0.2],unclassified:[0.6,0.6,0.6]}
        const bodyColor = this.bodyEditDraws?.size && bodyColors[draw.bodyRegion]
        gl.uniform3fv(this.loc.clay, highlighted ? [1.0, 0.72, 0.08] : bodyColor || CLAY)
        const handle = !highlighted && !bodyColor && texturesOn && draw.texture !== null ? this.textureUnits.get(draw.texture) : null
        if (handle) {
          gl.bindTexture(gl.TEXTURE_2D, handle)
          gl.uniform1i(this.loc.hasAlbedo, 1)
        } else {
          gl.uniform1i(this.loc.hasAlbedo, 0)
        }
        gl.uniform4fv(this.loc.uvTransform, draw.uvTransform)
        gl.drawElements(gl.TRIANGLES, draw.count, draw.type, draw.offset)
      }
    }
    gl.bindVertexArray(null)
  }
}

/* ------------------------------------------------------------------ */
/* view                                                                */
/* ------------------------------------------------------------------ */


function capabilities() {
  const canvas=document.createElement('canvas'),gl=canvas.getContext('webgl2')
  if(!gl)return {bc7:false,astc:false,maxTextureSize:0}
  const astc=gl.getExtension('WEBGL_compressed_texture_astc')
  const result={bc7:!!gl.getExtension('EXT_texture_compression_bptc'),astc:!!astc && astc.getSupportedProfiles().includes('ldr'),maxTextureSize:gl.getParameter(gl.MAX_TEXTURE_SIZE)}
  gl.getExtension('WEBGL_lose_context')?.loseContext()
  return result
}
window.BemRenderer = { Stage, Decoder, capabilities };
