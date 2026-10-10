# Desktop launch regression

Run `pwsh -File ui/tests/DesktopLaunch/run.ps1` with the workspace .NET SDK and Obfuscar tool installed. An explicit local configuration can be supplied with `-WorkspaceConfig`.

The harness links production voice-catalog and XInput deployment services, supplies a tiny synthetic PCK/index, and executes both the ordinary Release assembly and the same assembly transformed by the production obfuscation script. It creates only synthetic game/install/settings directories under the build output; no real game or injector is launched.

Checks cover voice preparation and cache reuse before the loader stage, proxy installation and immediate inspection, persisted JSON ownership, repeated launcher installation, recovery of a same-hash proxy without its manifest, old-version updates, uninstall status and unknown-loader protection. Debug symbols are disabled to reproduce the published Release metadata behavior.
