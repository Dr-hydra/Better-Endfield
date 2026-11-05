// Compatibility repair records. Included at translation-unit scope by
// module.cpp; every row here ships with the module and therefore also repairs
// packages that were published before the record existed.
//
// Keep this table hand written while it is this small. Every row states one
// confirmed rename of a mesh identity inside a single resource, with the
// geometry count that must still hold. Rows that stop proving themselves go
// quiet instead of guessing, so a stale row degrades to the previous behaviour.

namespace BetterEndfieldNext::CustomModel::GenericMatching {

// chr_0036_jsspsi / 1.6.5 dedup: the two vfxpart slots in both the world and UI
// prefabs now share the mesh object of cloth_06 / cloth_08 (verified on Windows
// and Android: same mesh id, same vertex/index counts, same bind poses). The
// receiver paths and every other component identity are unchanged, so the only
// thing a package has to be told is which mesh that slot holds now.
//
// `component` stays -1 on purpose: the local component index depends on the
// order of the resource selection inside the package, while the mesh identity
// is unique per resource. Keying on the identity cannot fire on the wrong slot.
inline constexpr DataRepairRecord kCompatDataRecords[] = {
    {"jsspsi-vfxpart01-cloth06-world",
        "chr_0036_jsspsi","chr_0036_jsspsi_postmodel",-1,
        "S_actor_jsspsi_vfxpart_01_lod0",4680,true,
        "S_actor_jsspsi_cloth_06_lod0",0,false,
        "2954fa80-23c1-1579-2b22-4ecfd6d70418"},
    {"jsspsi-vfxpart04-cloth08-world",
        "chr_0036_jsspsi","chr_0036_jsspsi_postmodel",-1,
        "S_actor_jsspsi_vfxpart_04_lod0",1752,true,
        "S_actor_jsspsi_cloth_08_lod0",0,false,
        "2954fa80-23c1-1579-2b22-4ecfd6d70418"},
    {"jsspsi-vfxpart01-cloth06-ui",
        "chr_0036_jsspsi","chr_0036_jsspsi_uimodel",-1,
        "S_actor_jsspsi_vfxpart_01_lod0",4680,true,
        "S_actor_jsspsi_cloth_06_lod0",0,false,
        "2954fa80-23c1-1579-2b22-4ecfd6d70418"},
    {"jsspsi-vfxpart04-cloth08-ui",
        "chr_0036_jsspsi","chr_0036_jsspsi_uimodel",-1,
        "S_actor_jsspsi_vfxpart_04_lod0",1752,true,
        "S_actor_jsspsi_cloth_08_lod0",0,false,
        "2954fa80-23c1-1579-2b22-4ecfd6d70418"},
};

// Hand written repairs live in compat_handlers.inc, which needs the runtime
// validators and is therefore included later in the translation unit.
std::span<const DataRepairRecord> CompatDataRepairRecords() { return kCompatDataRecords; }

}
