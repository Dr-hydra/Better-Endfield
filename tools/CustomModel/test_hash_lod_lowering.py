import json
from pathlib import Path
import struct
import tempfile
import subprocess
import sys
import unittest

from convert_efmi_poc import Source
from hash_lod_lowering import condition, convert, selected_draws, skeleton_ranges
from efmi_source import sections


def fixture(root):
    """Synthetic two-source skeleton, two draw materials, 16-bit input IDs."""
    (root / "Meshes").mkdir(parents=True, exist_ok=True)
    (root / "Meshes/p.buf").write_bytes(bytes(3 * 16))
    (root / "Meshes/t.buf").write_bytes(bytes(3 * 12))
    skin = struct.pack("<8H", 32768, 32767, 0, 0, 0, 256, 65535, 65535)
    (root / "Meshes/b.buf").write_bytes(skin * 3)
    (root / "Meshes/i.buf").write_bytes(struct.pack("<6I", 0, 1, 2, 2, 1, 0))
    for name, marker in (("base", 1), ("alt", 2)):
        dds = bytearray(128); dds[:4] = b"DDS "
        struct.pack_into("<7I", dds, 4, 124, 0, 4, 4, 8, 0, 1)
        dds[84:88] = b"DXT1"
        (root / f"{name}.dds").write_bytes(dds + bytes([marker]) * 8)
    ini = """[Constants]
global persist $swap = 0
[CommandListInitializeMergedSkeleton]
$component_id = 0
$Pool_MergedSkeleton_Component_VertexGroupOffsets[$component_id] = 0
$Pool_MergedSkeleton_Component_VertexGroupCounts[$component_id] = 1
$component_id = 1
$Pool_MergedSkeleton_Component_VertexGroupOffsets[$component_id] = 256
$Pool_MergedSkeleton_Component_VertexGroupCounts[$component_id] = 1
[TextureOverride_EntryPoint_LOD0.12345678_6_0]
hash = 12345678
match_index_count = 6
match_first_index = 0
CommandList\\EFMIv1\\Callback_Component_DrawCustom = ref CommandListDraw
[CommandListDraw]
run = CommandList\\EFMIv1\\OverrideTextures
vb0 = ResourceP
vb1 = ResourceT
vb2 = ResourceB
ib = ResourceI
ps-t0 = ResourceBase
if $swap == 0
drawindexedinstanced = 3, INSTANCE_COUNT, 0, 0, FIRST_INSTANCE
else
drawindexedinstanced = 3, INSTANCE_COUNT, 3, 0, FIRST_INSTANCE
endif
ps-t0 = ResourceAlt
drawindexedinstanced = 3, INSTANCE_COUNT, 3, 0, FIRST_INSTANCE
[ResourceP]
stride = 16
filename = Meshes/p.buf
[ResourceT]
stride = 12
filename = Meshes/t.buf
[ResourceB]
stride = 16
filename = Meshes/b.buf
[ResourceI]
format = DXGI_FORMAT_R32_UINT
filename = Meshes/i.buf
[ResourceBase]
filename = base.dds
[ResourceAlt]
filename = alt.dds
"""
    (root / "mod.ini").write_text(ini, encoding="utf-8")
    profile = {"schema": 2, "verified": True, "evidence": "synthetic test fixture only",
               "components": {
                   "0": {"mesh_name": "mesh0", "original_index_count": 6, "strides": [16, 12, 12],
                         "bone_names": ["bone0"], "materials": ["material0"], "evidence": "fixture"},
                   "1": {"mesh_name": "mesh1", "original_index_count": 3, "strides": [16, 12, 12],
                         "bone_names": ["bone1"], "materials": ["material1"], "evidence": "fixture", "unmodified": True}},
               "skeleton_groups": {"0": {"component": 0, "indices": [0], "evidence": "fixture"},
                                   "1": {"component": 1, "indices": [0], "evidence": "fixture"}},
               "entries": {"12345678": {"component": 0, "skin": "unorm16x4_uint16x4", "input_strides": [16, 12, 16],
                           "material_rules": [{"bindings": {"ps-t0": "ResourceBase"}, "component": 0, "slot": 0, "evidence": "fixture",
                                               "textures": [{"resource": "ResourceBase", "original_texture": "original"}]},
                                              {"bindings": {"ps-t0": "ResourceAlt"}, "component": 1, "slot": 0, "evidence": "fixture",
                                               "textures": [{"resource": "ResourceAlt", "original_texture": "original"}]}]}}}
    for native in profile["components"].values():
        native["attributes"] = [[0, 0, 3, 0], [1, 3, 4, 0], [4, 0, 2, 1],
                                [6, 3, 4, 1], [12, 4, 4, 2], [13, 6, 4, 2]]
    return profile


class LoweringTests(unittest.TestCase):
    def setUp(self):
        self.temp = tempfile.TemporaryDirectory()
        self.addCleanup(self.temp.cleanup)
        self.root = Path(self.temp.name)
        self.profile = fixture(self.root)
        self.src = Source(self.root)
        self.addCleanup(self.src.close)

    def test_emits_compact_palette_and_two_material_draws(self):
        data, report = convert(self.src, "mod.ini", self.profile)
        self.assertEqual(data[:8], b"BEMPC25\0")
        self.assertEqual(report["textures"], 2)
        self.assertEqual(report["components"], [{"component": 0, "vertices": 3, "indices": 6, "bones": 2, "draws": 2}])
        skin_offset = 28 + 52 + 3 * (16 + 12)
        self.assertEqual(struct.unpack_from("<4H4B", data, skin_offset), (32768, 32767, 0, 0, 0, 1, 0, 0))
        draw_offset = skin_offset + 3 * 12 + 6 * 2 + 4 + 2 * 12
        self.assertEqual(struct.unpack_from("<6I", data, draw_offset)[2], 0)
        self.assertEqual(struct.unpack_from("<6I", data, draw_offset + 24)[2], 1)

    def test_native_texture_name_pin_keeps_high_bit(self):
        for rule in self.profile['entries']['12345678']['material_rules']:
            rule['textures'][0]['original_texture']='T_actor_zhuangfy_body_01_N'
        data, _=convert(self.src,'mod.ini',self.profile)
        texture_offset=28+52+3*40+6*2+4+2*12+2*24+52
        self.assertEqual(struct.unpack_from('<I',data,texture_offset+36)[0],0x87218325)

    def test_unmapped_bone_is_not_subtracted_or_truncated(self):
        del self.profile["skeleton_groups"]["1"]
        with self.assertRaisesRegex(ValueError, "unmapped source bone 256"):
            convert(self.src, "mod.ini", self.profile)

    def test_local_palette_uses_explicit_donor_order(self):
        p = self.root / 'mod.ini'
        ini = p.read_text()
        begin, end = ini.index('[CommandListInitializeMergedSkeleton]'), ini.index('[TextureOverride_EntryPoint')
        p.write_text(ini[:begin]+ini[end:], encoding='utf-8')
        self.profile['skeleton_groups'] = {}
        self.profile['components']['1']['bone_names'] = ['donor_a', 'donor_b']
        self.profile['entries']['12345678']['local_bones'] = {
            'component': 1, 'indices': [1, 0], 'evidence': 'synthetic explicit local order'}
        (self.root/'Meshes/b.buf').write_bytes(struct.pack('<8H',32768,32767,0,0,0,1,65535,65535)*3)
        data, report = convert(self.src, 'mod.ini', self.profile)
        offset = 28+52+3*40+6*2+4
        self.assertEqual(struct.unpack_from('<2I',data,offset), (1,1))
        self.assertEqual(struct.unpack_from('<2I',data,offset+12), (1,0))
        self.assertEqual(report['components'][0]['bones'],2)
        self.profile['entries']['12345678']['local_bones']['indices'] = [2]
        with self.assertRaisesRegex(ValueError,'local bone identity'):
            convert(self.src,'mod.ini',self.profile)

    def test_mixed_local_and_merged_palettes_are_rejected(self):
        self.profile['entries']['12345678']['local_bones'] = {
            'component':0,'indices':[0],'evidence':'synthetic'}
        with self.assertRaisesRegex(ValueError,'cannot be combined'):
            convert(self.src,'mod.ini',self.profile)

    def test_large_mesh_requires_opt_in_and_preserves_uint32_indices(self):
        count=65538
        p=self.root/'mod.ini'
        p.write_text(p.read_text().replace('3, INSTANCE_COUNT, 0,','65538, INSTANCE_COUNT, 0,'),encoding='utf-8')
        for name,stride in (('p',16),('t',12)):
            (self.root/f'Meshes/{name}.buf').write_bytes(bytes(count*stride))
        (self.root/'Meshes/b.buf').write_bytes(struct.pack('<8H',32768,32767,0,0,0,256,0,0)*count)
        (self.root/'Meshes/i.buf').write_bytes(struct.pack(f'<{count}I',*range(count)))
        with self.assertRaisesRegex(ValueError,'16-bit index limit'):
            convert(self.src,'mod.ini',self.profile)
        self.profile['allow_uint32_indices']=True
        data,report=convert(self.src,'mod.ini',self.profile)
        self.assertEqual(struct.unpack_from('<I',data,28+36)[0],4)
        self.assertEqual(struct.unpack_from('<I',data,28+52+count*40+(count-1)*4)[0],65537)
        self.assertEqual(report['components'][0]['vertices'],count)

    def test_unverified_profile_and_layout_mismatch(self):
        self.profile["verified"] = False
        with self.assertRaisesRegex(ValueError, "verified"):
            convert(self.src, "mod.ini", self.profile)
        self.profile["verified"] = True
        self.profile["components"]["0"]["strides"][1] = 20
        with self.assertRaisesRegex(ValueError, "stride mismatch"):
            convert(self.src, "mod.ini", self.profile)

    def test_default_conditions_and_unknown_pass(self):
        self.assertTrue(condition("$x == 0 && !($y == 2)", {"$x": 0, "$y": 1}))
        for expression in ("vs == 200", "__import__('os')", "$unknown == 0"):
            with self.assertRaises(ValueError): condition(expression, {})
        sec = sections(self.src.read_exact("mod.ini").decode())
        draws = selected_draws(sec, "CommandListDraw")
        self.assertEqual([d["start"] for d in draws], [0, 3])
        sec["CommandListDraw"] += "\nrun = CustomShaderUnsafe\n"
        with self.assertRaisesRegex(ValueError, "unsupported"):
            selected_draws(sec, "CommandListDraw")

    def test_material_state_requires_exact_rule(self):
        self.profile["entries"]["12345678"]["material_rules"].pop()
        with self.assertRaisesRegex(ValueError, "unresolved material"):
            convert(self.src, "mod.ini", self.profile)

    def test_explicit_texture_cannot_be_silently_dropped(self):
        self.profile["entries"]["12345678"]["material_rules"][0]["textures"] = []
        with self.assertRaisesRegex(ValueError, "silently drops"):
            convert(self.src, "mod.ini", self.profile)

    def test_global_texture_override_cannot_be_silently_dropped(self):
        with (self.root / 'mod.ini').open('a') as stream:
            stream.write('\n[TextureOverrideGlobal]\nhash = abcdef01\nthis = ResourceBase\n')
        with self.assertRaisesRegex(ValueError, 'unmapped global texture override'):
            convert(self.src, 'mod.ini', self.profile)
        mapping = {'resource': 'ResourceBase', 'evidence': 'synthetic texture identity',
                   'disposition': 'material_rules', 'original_texture': 'wrong'}
        self.profile['texture_overrides'] = {'abcdef01': mapping}
        with self.assertRaisesRegex(ValueError, 'not delivered'):
            convert(self.src, 'mod.ini', self.profile)
        mapping['original_texture'] = 'original'
        self.assertEqual(convert(self.src, 'mod.ini', self.profile)[1]['textures'], 2)
        mapping['disposition'] = 'preserve_native'
        with self.assertRaisesRegex(ValueError, 'requires a reason'):
            convert(self.src, 'mod.ini', self.profile)

    def test_conditional_global_texture_override_rejected(self):
        with (self.root / 'mod.ini').open('a') as stream:
            stream.write('\n[TextureOverrideGlobal]\nhash = abcdef01\nif $swap == 0\nthis = ResourceBase\nendif\n')
        with self.assertRaisesRegex(ValueError, 'unsupported texture override statement'):
            convert(self.src, 'mod.ini', self.profile)

    def test_global_texture_must_reach_selected_draws(self):
        path = self.root / 'mod.ini'
        path.write_text(path.read_text().replace(
            'ps-t0 = ResourceAlt\ndrawindexedinstanced = 3, INSTANCE_COUNT, 3, 0, FIRST_INSTANCE\n', '') +
            '\n[TextureOverrideGlobal]\nhash = abcdef01\nthis = ResourceAlt\n', encoding='utf-8')
        self.profile['texture_overrides'] = {'abcdef01': {
            'resource': 'ResourceAlt', 'evidence': 'fixture', 'disposition': 'material_rules',
            'original_texture': 'original'}}
        with self.assertRaisesRegex(ValueError, 'unselected material state'):
            convert(self.src, 'mod.ini', self.profile)

    def test_single_submesh_keeps_additional_material_draw(self):
        path = self.root / 'mod.ini'
        path.write_text(path.read_text().replace(
            'ps-t0 = ResourceAlt\ndrawindexedinstanced = 3, INSTANCE_COUNT, 3, 0, FIRST_INSTANCE\n', ''),
            encoding='utf-8')
        self.profile['components']['0']['materials'].append('material_overlay')
        rule = self.profile['entries']['12345678']['material_rules'][0]
        rule['repeat_material_slots'] = [1]
        data, report = convert(self.src, 'mod.ini', self.profile)
        self.assertEqual(report['components'][0], {'component': 0, 'vertices': 3, 'indices': 6, 'bones': 2, 'draws': 2})
        ib_offset = 28 + 52 + 3 * 40
        self.assertEqual(struct.unpack_from('<6H', data, ib_offset), (0, 1, 2, 0, 1, 2))
        draws_offset = ib_offset + 12 + 4 + 24
        self.assertEqual(struct.unpack_from('<6I', data, draws_offset)[:4], (0, 3, 0, 0))
        self.assertEqual(struct.unpack_from('<6I', data, draws_offset + 24)[:4], (3, 3, 0, 1))
        rule['repeat_material_slots'] = [0]
        with self.assertRaisesRegex(ValueError, 'invalid repeated material slot'):
            convert(self.src, 'mod.ini', self.profile)

    def test_multiple_source_draws_do_not_guess_extra_material_order(self):
        self.profile['components']['0']['materials'].append('overlay')
        self.profile['entries']['12345678']['material_rules'][0]['repeat_material_slots'] = [1]
        with self.assertRaisesRegex(ValueError, 'preserve draw order'):
            convert(self.src, 'mod.ini', self.profile)

    def test_merged_ranges_must_not_overlap(self):
        sec = sections(self.src.read_exact("mod.ini").decode())
        sec["CommandListInitializeMergedSkeleton"] = sec["CommandListInitializeMergedSkeleton"].replace("= 256", "= 0")
        with self.assertRaisesRegex(ValueError, "overlapping"):
            skeleton_ranges(sec)

    def test_bad_weight_sum_and_rigid_layout_rejected(self):
        self.profile["components"]["0"]["strides"][2] = 4
        self.profile["components"]["0"]["attributes"].pop(-2)
        with self.assertRaisesRegex(ValueError, "rigid"):
            convert(self.src, "mod.ini", self.profile)
        self.profile["components"]["0"]["strides"][2] = 12
        self.profile["components"]["0"]["attributes"].insert(-1, [12, 4, 4, 2])
        (self.root / "Meshes/b.buf").write_bytes(struct.pack("<8H", 1, 0, 0, 0, 0, 0, 0, 0) * 3)
        with self.assertRaisesRegex(ValueError, "weight sum"):
            convert(self.src, "mod.ini", self.profile)

    def test_large_source_indices_compact_without_truncation(self):
        count = 65539
        for name, stride in (("p", 16), ("t", 12)):
            (self.root / f"Meshes/{name}.buf").write_bytes(bytes(count * stride))
        skin = struct.pack("<8H", 32768, 32767, 0, 0, 0, 256, 0, 0)
        (self.root / "Meshes/b.buf").write_bytes(skin * count)
        (self.root / "Meshes/i.buf").write_bytes(struct.pack("<6I", 65536, 65537, 65538, 65538, 65537, 65536))
        _, report = convert(self.src, "mod.ini", self.profile)
        self.assertEqual(report["components"][0]["vertices"], 3)

    def test_palette_overflow_is_explicit(self):
        count = 258
        p = self.root / "mod.ini"
        p.write_text(p.read_text().replace("GroupCounts[$component_id] = 1", "GroupCounts[$component_id] = 258", 1)
                     .replace("Offsets[$component_id] = 256", "Offsets[$component_id] = 512")
                     .replace("3, INSTANCE_COUNT, 0,", "258, INSTANCE_COUNT, 0,"), encoding="utf-8")
        self.profile["components"]["0"]["bone_names"] = [f"bone{i}" for i in range(count)]
        self.profile["skeleton_groups"]["0"]["indices"] = list(range(count))
        for name, stride in (("p", 16), ("t", 12)):
            (self.root / f"Meshes/{name}.buf").write_bytes(bytes(count * stride))
        (self.root / "Meshes/b.buf").write_bytes(b"".join(struct.pack("<8H", 65535, 0, 0, 0, i, 0, 0, 0) for i in range(count)))
        (self.root / "Meshes/i.buf").write_bytes(struct.pack(f"<{count}I", *range(count)))
        with self.assertRaisesRegex(ValueError, "more than 256"):
            convert(self.src, "mod.ini", self.profile)

    def test_cli_refusal_does_not_overwrite_existing_package(self):
        profile = self.root / "profile.json"
        self.profile["verified"] = False
        profile.write_text(json.dumps(self.profile), encoding="utf-8")
        output = self.root / "old.bempoc"; output.write_bytes(b"keep existing")
        result = subprocess.run([sys.executable, str(Path(__file__).with_name("convert_hash_lod.py")),
                                 str(self.root), "--profile", str(profile), "--output", str(output),
                                 "--report", str(self.root / "report.json")], capture_output=True)
        self.assertEqual(result.returncode, 2)
        self.assertEqual(output.read_bytes(), b"keep existing")


if __name__ == "__main__":
    unittest.main()
