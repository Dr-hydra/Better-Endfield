# Third-Party Notices

## EIEM (Importing Endfield MMD)

- Source: https://github.com/Sasye/EIEM
- License: GNU Affero General Public License v3.0 (the same license as this project)
- Used in: `native/modules/camera/free_camera_runtime.inc`, where the VMD camera
  sampling follows EIEM's `camera_player.h` (Bezier evaluation, Euler signs,
  180 degree basis, 0.07 scale and 5 degree FOV defaults) and `vmd_parser.h`
  (camera record layout).

- Additional use (2026-09-27): `native/shared/motion/vmd.h` follows the
  section layouts and timeline conventions in EIEM `src/vmd_parser.h` and
  `src/camera_player.h`, reviewed at commit
  `447600b9de959d8b709ad84b19e605eeb8a2a0a9`.
  The bounded portable parser, transactional file handling and camera path format
  are adapted/implemented for Better Endfield. No EIEM binaries or assets are included.

### Character semantic mapping reference

`native/shared/motion/character_mapping.h` contains semantic bone/morph-name mappings
reviewed against Sasye/EIEM `src/bone_map.h` and `src/smc_face.h` at
`8b46b76b33e3f61825b6b8c55d983bcf2c1bc94c` (AGPL-3.0).
No upstream executable, private SMC memory layout, hash constant, or game asset is included.
