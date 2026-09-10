// Temporary research split keeps the fast-moving CustomModel PoC reviewable
// while the runtime contract is still being validated in-game. The .inc files
// are textual continuations of one translation unit and will be consolidated
// once the BEM v1 runtime shape is frozen.
#include "module_poc2_part_00.inc"
#include "module_poc2_part_01.inc"
#include "module_poc2_part_02.inc"
#include "module_poc3_part_03b.inc"
#include "module_poc2_part_04.inc"

// This runs during DLL static initialization, before Host calls Initialize(),
// and redirects stripped Mesh contracts to methods proven present in the
// Endfield UnityEngine.CoreModule metadata dump.
#include "module_poc3_contract_patch.inc"
