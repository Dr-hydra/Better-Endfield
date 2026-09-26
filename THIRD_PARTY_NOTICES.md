# Third-Party Notices

## EIEM (Importing Endfield MMD)

- Source: https://github.com/Sasye/EIEM
- License: GNU Affero General Public License v3.0 (the same license as this project)
- Used in: `native/shared/include/BetterEndfield/vmd_parser.h` and
  `native/modules/camera/free_camera_runtime.inc`. The parser keeps EIEM's VMD
  record layout, while camera sampling follows EIEM's `camera_player.h`
  (Bezier evaluation, Euler signs, 180 degree basis, 0.07 scale and 5 degree
  FOV defaults).
