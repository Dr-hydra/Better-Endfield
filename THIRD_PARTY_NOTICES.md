# Third-Party Notices

## EIEM (Importing Endfield MMD)

- Source: https://github.com/Sasye/EIEM
- License: GNU Affero General Public License v3.0 (the same license as this project)
- Used in: `native/modules/camera/free_camera_runtime.inc`, where the VMD camera
  sampling follows EIEM's `camera_player.h` (Bezier evaluation, Euler signs,
  180 degree basis, 0.07 scale and 5 degree FOV defaults) and `vmd_parser.h`
  (camera record layout).
