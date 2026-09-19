from pathlib import Path
import struct
import tempfile
import unittest
import zipfile

from convert_efmi_poc import Source
from convert_hash_lod import select_ini
from efmi_lod_source import inspect_lod_source, binding_paths
from efmi_source import sections


class HashLodTests(unittest.TestCase):
    def source(self, *, vertices=3, indices=(0, 1, 2), draw="3, INSTANCE_COUNT, 0, 0, FIRST_INSTANCE",
               missing_callback=False, filename="Meshes/p.buf", second_ini=False):
        entry = "TextureOverride_EntryPoint_LOD0.12345678_6_0"
        ini = (f"[{entry}]\nhash = 12345678\nmatch_first_index = 0\nmatch_index_count = 6\n"
               "CommandList\\EFMIv1\\Callback_Component_DrawCustom = ref CommandListDraw\n")
        body = "ib = ResourceI\nvb0 = ResourceP\nvb1 = ResourceT\nvb2 = ResourceB\n"
        body += f"drawindexedinstanced = {draw}\n"
        if not missing_callback:
            ini += "[CommandListDraw]\n" + body
        ini += (f"[ResourceP]\nstride = 16\nfilename = {filename}\n"
                "[ResourceT]\nstride = 12\nfilename = Meshes/t.buf\n"
                "[ResourceB]\nstride = 16\nfilename = Meshes/b.buf\n"
                "[ResourceI]\nformat = DXGI_FORMAT_R32_UINT\nfilename = Meshes/i.buf\n")
        tmp = tempfile.TemporaryDirectory()
        self.addCleanup(tmp.cleanup)
        path = Path(tmp.name) / "sample.zip"
        with zipfile.ZipFile(path, "w") as archive:
            archive.writestr("pack/character.ini", ini)
            archive.writestr("pack/DISABLED_old.ini", ini)
            archive.writestr("pack/Backups/old.ini", ini)
            archive.writestr("pack/panel.ini", "[KeyToggle]\nkey = F1\n")
            if second_ini:
                archive.writestr("other/character.ini", ini)
            for name, stride in (("p", 16), ("t", 12), ("b", 16)):
                archive.writestr(f"pack/Meshes/{name}.buf", bytes(vertices * stride))
            archive.writestr("pack/Meshes/i.buf", struct.pack(f"<{len(indices)}I", *indices))
        src = Source(path)
        self.addCleanup(src.close)
        return src

    def report(self, **kwargs):
        src = self.source(**kwargs)
        ini = select_ini(src)
        return inspect_lod_source(src, ini, sections(src.read_exact(ini).decode()))

    def test_select_and_validate_callback(self):
        report = self.report()
        self.assertEqual(report["ini"], "pack/character.ini")
        self.assertEqual(report["structural_errors"], [])
        self.assertEqual(report["components"][0]["draws"][0]["count"], 3)
        self.assertFalse(report["conversion_ready"])

    def test_multiple_entry_inis_require_selection(self):
        src = self.source(second_ini=True)
        with self.assertRaisesRegex(ValueError, "found 2"):
            select_ini(src)
        self.assertEqual(select_ini(src, "pack/character.ini"), "pack/character.ini")

    def test_large_indices_cannot_be_narrowed(self):
        report = self.report(vertices=65537, indices=(0, 1, 65536))
        self.assertEqual(report["structural_errors"], [])
        self.assertIn("uint32_indices_or_mesh_partition", report["blockers"])

    def test_invalid_index_and_draw_range(self):
        for kwargs in ({"indices": (0, 1, 3)}, {"draw": "6, INSTANCE_COUNT, 0, 0, FIRST_INSTANCE"},
                       {"draw": "3, INSTANCE_COUNT, 0, -1, FIRST_INSTANCE"}):
            with self.subTest(kwargs=kwargs):
                self.assertTrue(self.report(**kwargs)["structural_errors"])

    def test_missing_callback_and_unsafe_filename(self):
        self.assertTrue(self.report(missing_callback=True)["structural_errors"])
        self.assertTrue(any("source-relative" in e for e in
                            self.report(filename="../p.buf")["structural_errors"]))

    def test_geometry_only_does_not_bypass_skeleton_gate(self):
        src = self.source()
        ini = select_ini(src)
        sec = sections(src.read_exact(ini).decode())
        sec["CommandListBones"] = ("run = CommandList\\EFMIv1\\MergedSkeleton_AttachComponent\n"
                                    "vb2->ElementFormat(BLENDINDICES, 0) = R16G16B16A16_UINT\n")
        report = inspect_lod_source(src, ini, sec, geometry_only=True)
        self.assertIn("merged_skeleton_remap", report["blockers"])
        self.assertIn("uint16_blend_indices", report["blockers"])
        self.assertFalse(report["conversion_ready"])

    def test_branch_bindings_restore_alternatives(self):
        draws = list(binding_paths([
            "vb0 = P", "vb1 = T", "vb2 = B", "ib = I", "if $x == 1",
            "ib = J", "drawindexed = 3, 0, 0", "else", "ib = K",
            "drawindexed = 3, 0, 0", "endif", "drawindexed = 3, 0, 0"]))
        self.assertEqual({s[3] for s in draws[0][1]}, {"J"})
        self.assertEqual({s[3] for s in draws[1][1]}, {"K"})
        self.assertEqual({s[3] for s in draws[2][1]}, {"J", "K"})
        with self.assertRaisesRegex(ValueError, "unclosed"):
            list(binding_paths(["if $x == 1"]))

    def test_cross_buffer_draw_uses_rebound_ib(self):
        src = self.source()
        ini = select_ini(src)
        sec = sections(src.read_exact(ini).decode())
        sec["CommandListDraw"] += ("if $x == 1\nib = ResourceOther\n"
                                    "drawindexed = 3, 0, 0\nendif\n")
        sec["ResourceOther"] = sec["ResourceI"]
        report = inspect_lod_source(src, ini, sec)
        self.assertEqual(report["structural_errors"], [])
        self.assertTrue(report["components"][0]["cross_buffer_draws"])
        self.assertIn("cross_buffer_draw_mapping", report["blockers"])


if __name__ == "__main__":
    unittest.main()
