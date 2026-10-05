# First Person

Enjoy a smooth first-person view that follows where you look.

## Video demo

<p align="left">
  <a href="https://youtu.be/B66x3Gc5Vbw"><img src="https://raw.githubusercontent.com/landoria-gaming/Landoria.FirstPerson/main/assets/first-person.png" alt="First Person video demo" width="300"></a>
</p>

## Highlights

- Enable first-person zoom with the default F6 key.
- F6 can be changed to another key or mouse button in the BepInEx config file.
- Use Left Alt + F6 to toggle third person while attacking, blocking, chopping, mining, or building; returns to first person after 3 seconds by default.
- Customize your field of view (FOV) in-game from 65 to 120.
- Add a separate first-person FOV bonus, set to 15 by default.
- Apply configuration changes without restarting the game.
- Client-side mod; no server installation needed.

## Controls

| Control | Action |
|---|---|
| `F6` | Enable or disable first-person zoom without changing the current distance |
| `Left Alt + F6` | Enable or disable automatic third person during actions |

## Commands

| Command | Action |
|---|---|
| `fov <degrees>` | Set the saved FOV, up to 120 (default 65) |
| `fov` | Show the current FOV |
| `fov reset` | Restore the default FOV of 65 |

## BepInEx Configuration

The file `Landoria.FirstPerson.cfg` is created automatically in the config folder of the current BepInEx profile. Changes to this file are applied without restarting the game.

| Setting | Default | Description |
|---|---|---|
| `ToggleShortcut` | `F6` | First-person toggle shortcut. May be changed to `Mouse2` or `Mouse3` for example |
| `HeadBobStrength` | `2` | First-person head bob strength; `0` disables it |
| `FirstPersonEnabled` | `false` | Saved first-person state. Normally changed using F6. |
| `FieldOfView` | `65` | Saved camera FOV. Normally changed using the `fov` command in-game |
| `FirstPersonFieldOfViewBonus` | `15` | Additional FOV applied only in first person; accepts values from 0 to 50 |
| `ThirdPersonAuto` | `false` | Use third person during actions such as attacking, blocking, chopping, mining, or building |
| `ToggleThirdPersonAutoShortcut` | `Left Alt + F6` | Toggle automatic third person during actions |
| `ThirdPersonAutoDistance` | `3` | Camera distance during an action; accepts 1 to 10 meters |
| `AutomaticReturnDelay` | `3` | ThirdPersonAuto return delay from 1 to 10 seconds; zoom in returns immediately, zoom out cancels it |

## Contact

Report bugs through [GitHub Issues](https://github.com/landoria-gaming/Landoria.FirstPerson/issues).
