import copy
import unittest
from unittest.mock import patch
from efmi_materials import collect_mapped_textures, validate_material_profile
from build_source_profile import apply_runtime_streams


class MaterialTests(unittest.TestCase):
    def profile(self):
        return {"schema": 1, "character_id": "pelica", "evidence": "fixture", "textures": [
            {"source": "12345678", "components": [7, 9], "target_texture": "original_D", "evidence": "binding"}]}

    def test_effective_binding_overrides_filename_components(self):
        class Source:
            names = ["Textures/Components-2 t=12345678.dds"]
            def read_exact(self, name): return b"dds"
        with patch("convert_efmi_poc.parse_dds", return_value=(4, 4, 1, 25, True, b"x"*16)):
            result = collect_mapped_textures(Source(), self.profile(), "pelica")
        self.assertEqual(result[0].component_mask, (1 << 7) | (1 << 9))
        self.assertEqual(result[0].pin_kind, 2)
        self.assertNotEqual(result[0].explicit_slot, 0)

    def test_reject_overlap_and_wrong_character(self):
        p = self.profile()
        with self.assertRaisesRegex(ValueError, "character"):
            validate_material_profile(p, "another")
        p["textures"].append(copy.deepcopy(p["textures"][0]))
        with self.assertRaisesRegex(ValueError, "overlapping"):
            validate_material_profile(p)

    def test_runtime_evidence_preserves_serialized_layout(self):
        component = {"streams": [{}, {"semantics": [{"stride": 8}]}, {}]}
        observed = {"evidence": "live declaration", "streams": {"1": {"semantics": [
            {"name": "TEXCOORD", "index": 0, "format": "R32G32_FLOAT", "stride": 8},
            {"name": "TEXCOORD", "index": 2, "format": "R8G8B8A8_SNORM", "stride": 4}]}}}
        apply_runtime_streams(component, observed)
        self.assertEqual(sum(s["stride"] for s in component["streams"][1]["semantics"]), 12)
        self.assertEqual(component["serialized_streams"][1]["semantics"], [{"stride": 8}])
        with self.assertRaisesRegex(ValueError, "evidence"):
            apply_runtime_streams(component, {"streams": observed["streams"]})
