# BEM matching review (2026-10-01)

## Confirmed corrections

- When several properties in one material reference the same original Texture
  object, replace all of them with one replacement object. Distinct objects which
  happen to have the same name remain ambiguous. The Typhoea campus package
  exposed this case; the PC runtime log showed `sameTextureObject=true` for the
  clear-coat and fresnel properties. The user confirmed the corrected package
  now loads in game.
- A supported capability declaration may include features the package does not
  use. Apply this to `keep-material-textures`, `texture-slots`, and
  `resource-bone-aliases`. Using these features still requires their declaration;
  unknown capabilities are rejected. Python writers keep a BEM 1.2 header when
  a 1.2 capability is declared, even if it is unused.

The rabbit package had a separate metadata error: its `cloth_03` material actually
references `cloth_01_D/N/P`. A separate test package changes only the three target
texture names. Meshes, bones, component rules and all 84 binary payloads are
unchanged. The user confirmed this package also works in game.

## Other checks reviewed

Normal mode keeps exact renderer identities, source mesh index counts, bone identities and
declared aliases, material donor slots, and missing/ambiguous texture target
checks. There is currently no evidence these checks reject a valid package;
loosening them could bind a package to an unintended resource. Bone aliases from
BEM 1.2 are already supported by the shared parser and binding implementation.

Normal mode keeps container bounds, payload lengths, memory budgets, vertex declarations,
index ranges, skin palette ranges and finite normalized weights. They are
structural requirements of the current upload implementation.

## Developer option on both platforms

The third-party model page now has `开发者：关闭模型校验`, off by default.
Windows persists `skip_validation` in the `[CustomModel]` section. Android
publishes `bem_skip_validation` with its framework preferences and forwards it
through import inspection and the game module configuration to the same parser.

With this option on, the parser bypasses compatibility checks and policy limits
for capacities, required capabilities, source identities, vertex/index ranges,
palette size and weight validity. Binding bypasses name/space/layout checks and
uses the package's declaration. Ambiguous texture names replace matching slots;
missing names retain the original textures. Android skips post-submit readback
validation in this mode. The payload cache distinguishes normal and developer
loads. Windows imports BEM and ZIP files without running the creator validator.

Required parsing and representation checks remain: container/payload extents,
decoding, referenced table bounds, supported stream/texture encodings, arithmetic
representation, and deterministic option plans. In particular, texture binding
masks still represent at most 64 selected textures, and vertex declarations at
most 16 attributes. This option does not implement new file encodings. Engine
API failures still stop publication. Crashes or incorrect rendering are expected
possible outcomes of developer tests, as the interface explicitly states.

## Android external import

The external entry starts importing immediately on Activity creation. Framework
service binding arrives asynchronously through a provider callback, but the
installer previously made one publication attempt and deleted its staged result
on failure. Any publication failure also produced the same disconnected-service
message, including file and Binder errors after connection.

The worker now waits up to 10 seconds for a connection before publication, checks
cancellation at least every 250 ms, and wakes on the bind callback. A connection
timeout, a disconnected service, and a failed file publication have separate
messages. Index changes still happen only after successful publication. No
recent import failure was available in the device's log buffer; the connection
timing case still needs external-open testing on a device with the next APK.

## Validation

- 36 Python BEM 1.0/1.1/1.2 tests, with the production native validator enabled,
  including palettes above 256, 257 active draws, invalid indices, unknown
  capabilities and truncated payloads with the developer option on/off.
- Native custom-model binding tests, including shared-object texture slots,
  distinct same-name objects, missing targets and duplicate assignments.
- Host Java tests for already-connected service, timeout, delayed bind callback,
  spurious wake, cancellation and interruption.
- Windows package-manager tests cover option persistence, developer BEM/ZIP
  imports and normal-mode restoration.
- Windows UI publish, Windows native build, Android release Java compilation
  and Android ARM64 native build. No APK was rebuilt for these latest changes.
