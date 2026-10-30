# Gacha export regression (#36)

Run `pwsh -File ui/tests/GachaRecordStoreTests/run.ps1` using the workspace .NET SDK and Obfuscar installation. Pass `-WorkspaceConfig` to select an explicit workspace configuration.

The harness links the production models and record store, builds Release without debug symbols, and runs both the original assembly and the assembly transformed by `scripts/ObfuscateNext.ps1`. The obfuscated run also verifies that the original anonymous payload reproduces the missing-constructor-parameter exception.

Checks cover export schema and timestamp, character/weapon/pool field preservation, export/import round trips, empty data, old exports, optional pool metadata, automatic cache persistence, and a filesystem failure preserving an earlier export. All fixtures live in a unique temporary directory; real caches and Documents exports are untouched.
