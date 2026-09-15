import json
from pathlib import Path
import struct
import tempfile
import unittest
import zipfile

from convert_efmi_poc import Source, parse_original_index_counts
from efmi_source import analyze_source, adapt_stream
from build_source_profile import source_streams


class ImportTests(unittest.TestCase):
    def archive(self, files):
        folder = tempfile.TemporaryDirectory()
        self.addCleanup(folder.cleanup)
        path = Path(folder.name) / "mod.zip"
        with zipfile.ZipFile(path, "w") as archive:
            for name, data in files.items():
                archive.writestr(name, data)
        src = Source(path)
        self.addCleanup(src.close)
        return src

    def sample(self, draw=3, bad_index=False, stride=12, original_count=True):
        count_line = "match_index_count = 6\n" if original_count else ""
        ini = "; EFMI ALPHA-4 INI\n[TextureOverride_Component0]\n" + count_line
        ini += f"[CommandList_Draw_Component0]\ndrawindexedinstanced = {draw}, INSTANCE_COUNT, 0, 0, FIRST_INSTANCE\n"
        streams = [
            {"semantics": [{"name": "POSITION", "index": 0, "format": "R32G32B32_FLOAT", "stride": 12}]},
            {"semantics": [{"name": "TEXCOORD", "index": 0, "format": "R32G32_FLOAT", "stride": 8}]},
            {"semantics": [{"name": "BLENDINDICES", "index": 0, "format": "R8G8B8A8_UINT", "stride": 4}]},
        ]
        profile = {"schema": 1, "character_id": "test", "components": {"0": {
            "mesh_name": "test_mesh", "bone_count": 1, "original_index_count": 6, "streams": streams}}}
        files = {}
        for index, size in enumerate([stride, 8, 4]):
            ini += f"[Resource_Component0_VB{index}]\nstride = {size}\n"
            files[f"Meshes/Component0_VB{index}.buf"] = bytes(3*size)
        files["Meshes/Component0_IB.buf"] = struct.pack("<3H", 0, 1, 3 if bad_index else 2)
        files["mod.ini"] = ini
        return self.archive(files), profile

    def test_selects_basename_not_disabled_suffix(self):
        src = self.archive({"pack/mod.ini": "active", "pack/DISABLED_mod.ini": "old"})
        self.assertEqual(src.read("mod.ini"), b"active")

    def test_multiple_mods_remain_ambiguous(self):
        src = self.archive({"a/mod.ini": "a", "b/mod.ini": "b"})
        with self.assertRaisesRegex(ValueError, "ambiguous"):
            src.find("mod.ini")

    def test_both_generations_ignore_lod_suffix(self):
        for name in ("TextureOverride_Component0", "TextureOverride_EntryPoint_Component0"):
            self.assertEqual(parse_original_index_counts(
                f"[{name}]\nmatch_index_count = 6\n[{name}_LOD0]\nmatch_index_count = 3\n"), {0: 6})

    def test_profile_supplies_missing_count(self):
        src, profile = self.sample(original_count=False)
        report = analyze_source(src, "mod.ini", profile)
        self.assertEqual(report["errors"], [])
        self.assertEqual(report["components"][0]["original_index_count"], 6)

    def test_rejects_oversized_draw_and_bad_index(self):
        src, profile = self.sample(draw=9, bad_index=True)
        errors = analyze_source(src, "mod.ini", profile)["errors"]
        self.assertTrue(any("IB range" in e for e in errors))
        self.assertTrue(any("index exceeds" in e for e in errors))

    def test_rejects_mod_source_stride_difference(self):
        src, profile = self.sample(stride=16)
        self.assertTrue(any("stride 16" in e for e in analyze_source(src, "mod.ini", profile)["errors"]))

    def test_rejects_unknown_semantics(self):
        src, _ = self.sample()
        self.assertFalse(analyze_source(src, "mod.ini")["conversion_ready"])

    def test_rejects_out_of_range_bone(self):
        src, profile = self.sample()
        profile["components"]["0"]["bone_count"] = 0
        self.assertTrue(any("bone slot" in e for e in analyze_source(src, "mod.ini", profile)["errors"]))

    def test_unknown_source_channels_not_guessed(self):
        with self.assertRaisesRegex(ValueError, "unsupported source channel"):
            source_streams("VertexData m_VertexData\nUInt8 stream = 0\nUInt8 offset = 0\nUInt8 format = 99\nUInt8 dimension = 3\nm_DataSize")

    def test_uv_adaptation_keeps_each_vertex_uv_exact(self):
        rule = {"operation": "retain_uv0", "input_stride": 12, "evidence": "fixture"}
        raw = struct.pack("<2f4s2f4s", .25, .75, b"tail", .5, 1., b"diff")
        self.assertEqual(adapt_stream(raw, rule, 8), struct.pack("<4f", .25, .75, .5, 1.))
        with self.assertRaises(ValueError):
            adapt_stream(raw, rule, 12)

    def test_alternate_lod_reference_is_warning(self):
        src, profile = self.sample()
        files = {n: src.read_exact(n) for n in src.names}
        files["mod.ini"] += b"\n[TextureOverride_Component0_LOD0]\nx = ref ResourceMissing\n"
        report = analyze_source(self.archive(files), "mod.ini", profile)
        self.assertEqual(report["errors"], [])
        self.assertTrue(any("ResourceMissing" in w for w in report["warnings"]))

    def test_explicit_draw_correction_and_material_gate(self):
        src, profile = self.sample(draw=9)
        profile["components"]["0"]["draw_correction"] = {
            "declared_count": 9, "actual_count": 3, "evidence": "complete fixture IB"}
        profile["requires_geometry_only"] = True
        self.assertFalse(analyze_source(src, "mod.ini", profile)["conversion_ready"])
        self.assertTrue(analyze_source(src, "mod.ini", profile, geometry_only=True)["conversion_ready"])


if __name__ == "__main__":
    unittest.main()
