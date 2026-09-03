# Endfield character model resource findings

Date: 2026-08-03

## Result

Playable character models are not stored as loose FBX files. Each character is
described by a JSON prefab-info record and assembled at runtime from logical
resource keys.

The recurring fields are:

- `avatarTempletName`: actor/avatar hierarchy template.
- `avatarMeshName`: skinned mesh resource description.
- `partNameIdList`: character-specific post-model part set.
- `cpuAnimationTempletName`: animation/skeleton family.
- `facialMorphAvatarName`: facial morph definition.
- `lodNum`: number of LOD levels, normally 4.

Example mappings:

| Character | Part | Avatar mesh | Avatar template | Animation family |
| --- | --- | --- | --- | --- |
| `chr_0002_endminm` | `chr_0002_endminm_postmodel` | `NPC/AvatarMesh/Actor/endminm` | `NPC/AvatarTemplet/Entity/Actor/endminm` | `NPC/AnimationConfig/Humanoid/Boy/endminm` |
| `chr_0003_endminf` | `chr_0003_endminf_postmodel` | `NPC/AvatarMesh/Actor/endminf` | `NPC/AvatarTemplet/Entity/Actor/endminf` | `NPC/AnimationConfig/Humanoid/Girl/endminf` |
| `chr_0004_pelica` | `chr_0004_pelica_postmodel` | `NPC/AvatarMesh/Actor/pelica` | `NPC/AvatarTemplet/Entity/Actor/pelica` | `NPC/AnimationConfig/Humanoid/Girl/pelica` |
| `chr_0017_yvonne` | `chr_0017_yvonne_postmodel` | `NPC/AvatarMesh/Actor/yvonne` | `NPC/AvatarTemplet/Entity/Actor/yvonne` | `NPC/AnimationConfig/Humanoid/Girl/yvonne` |
| `chr_0025_ardelia` | `chr_0025_ardelia_postmodel` | `NPC/AvatarMesh/Actor/ardelia` | `NPC/AvatarTemplet/Entity/Actor/ardelia` | `NPC/AnimationConfig/Humanoid/Girl/ardelia` |
| `chr_0033_camille` | `chr_0033_camille_postmodel` | `NPC/AvatarMesh/Actor/camille` | `NPC/AvatarTemplet/Entity/Actor/camille` | `NPC/AnimationConfig/Humanoid/Boy/camille` |

The extracted main-character records currently cover 32 character IDs, from
`chr_0002_endminm` through `chr_0036_jsspsi` with gaps in the numeric sequence.

## Storage format

The physical storage stack is:

1. `StreamingAssets/VFS/<block>/<block>.blc`: encrypted block index.
2. `StreamingAssets/VFS/<block>/<hash>.chk`: chunk data containing many logical files.
3. `Bundles/Windows/.../<hash>.ab`: hash-named Endfield AssetBundle files within the VFS.
4. Unity serialized objects inside each bundle: `GameObject`, `Mesh`,
   `SkinnedMeshRenderer`, `Avatar`, `Material`, `Texture2D`, and related objects.

The public EndfieldStudio implementation describes the inner bundle as an
obfuscated UnityFS stream using VFSAES and LZ4Inv. Its current mesh exporter is
not implemented, although it can parse bundle objects and textures.

The VFS lists 236,746 main Bundle files and 1,048 InitialBundle files. Bundle
names are hashes, so character names cannot be inferred from the `.ab` filename.
The path-to-bundle mapping is stored in `Bundles/Windows/manifest.hgmmap`.

## Mod implication

For an in-game character replacement, the useful stable input is the logical
resource key such as `NPC/AvatarMesh/Actor/pelica`, not an FBX path or a hashed
`.ab` filename. The preferred implementation is to call or intercept the game's
existing resource loader with the target character's avatar/template keys, then
instantiate or bind the returned Unity objects.

Male and female Endministrator records use different animation families and
different `sizeType` values:

- Male: `Humanoid/Boy/endminm`, `sizeType = 3`.
- Female: `Humanoid/Girl/endminf`, `sizeType = 4`.

They therefore should not be treated as proven identical skeletons. Loading the
complete target avatar template is safer than cross-assigning only the mesh.

## Evidence

- `research/table-dump/Table/CharacterTable.json`
- `tools/EndfieldUnpacker/DecryptOutput/Json_decrypted/NPC/PrefabInfo/manifest.json`
- `tools/EndfieldUnpacker/DecryptOutput/Json_decrypted/NPC/PrefabInfo/npc_chr_0002_endminm.json`
- `tools/EndfieldUnpacker/DecryptOutput/Json_decrypted/NPC/PrefabInfo/npc_chr_0003_endminf.json`
- `tools/EndfieldUnpacker/DecryptOutput/Json_decrypted/NPC/PrefabInfo/npc_chr_0004_pelica.json`
- `tools/EndfieldUnpacker/DecryptOutput/Json_decrypted/NPC/PrefabInfo/npc_chr_0017_yvonne.json`
- `tools/EndfieldUnpacker/DecryptOutput/Json_decrypted/NPC/PrefabInfo/npc_chr_0025_ardelia.json`
- `tools/EndfieldUnpacker/DecryptOutput/Json_decrypted/NPC/PrefabInfo/npc_chr_0033_camille.json`

## Tool versions

- EndfieldUnpacker: `3017a799d2920a279c2175e9db80033883391f0c`
- EndfieldStudio: `87b017727bf1301a5813b5d4ac28d278027bf756`

