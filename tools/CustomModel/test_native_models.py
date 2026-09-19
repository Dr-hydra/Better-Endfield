import copy
import unittest
from parse_native_models import parse, serialized_layout
from extract_native_bundles import select_bundles


def ref(key): return {"id": key, "resolved": key is not None}


def fixture():
    matrix = [float(i % 5 == 0) for i in range(16)]
    def obj(key, kind, name="", **fields): return {"id": key, "type": kind, "name": name, **fields}
    transform = {"position": {"X": 0, "Y": 0, "Z": 0}, "scale": {"X": 1, "Y": 1, "Z": 1},
                 "rotation": {"X": 0, "Y": 0, "Z": 0, "W": 1}, "children": []}
    channels = [{"attribute": 0, "stream": 0, "offset": 0, "format": 0, "dimension_raw": 3},
                {"attribute": 1, "stream": 0, "offset": 12, "format": 0, "dimension_raw": 49},
                {"attribute": 4, "stream": 1, "offset": 0, "format": 0, "dimension_raw": 2},
                {"attribute": 12, "stream": 2, "offset": 0, "format": 4, "dimension_raw": 4},
                {"attribute": 13, "stream": 2, "offset": 8, "format": 6, "dimension_raw": 4}]
    return {"schema": 1, "objects": [
        obj("scene:1", "GameObject", "character"), obj("scene:2", "GameObject", "mesh"),
        obj("scene:3", "GameObject", "bone"),
        obj("scene:4", "Transform", game_object=ref("scene:1"), parent=ref(None), **transform),
        obj("scene:5", "Transform", game_object=ref("scene:2"), parent=ref("scene:4"), **transform),
        obj("scene:6", "Transform", game_object=ref("scene:3"), parent=ref("scene:4"), **transform),
        obj("meshes:1", "Mesh", "mesh", vertex_count=3, bindposes=[matrix], serialized_channels=channels,
            submeshes=[{"topology": "Triangles", "index_count": 3}]),
        obj("scene:7", "SkinnedMeshRenderer", game_object=ref("scene:2"), mesh=ref("meshes:1"),
            bones=[ref("scene:6")], root_bone=ref("scene:6"), materials=[ref("materials:1")]),
        obj("materials:1", "Material", "material", shader=ref("shader:1"), textures=[
            {"property": "_BaseMap", "texture": ref("textures:1"), "scale": [1, 1], "offset": [0, 0]}]),
        obj("textures:1", "Texture2D", "texture", width=4, height=4, format="DXT1", mips=1),
        obj("shader:1", "Shader", "shader", code_parsed=False)]}


class NativeParserTests(unittest.TestCase):
    def test_resolves_file_and_path_identity_in_bone_order(self):
        result = parse(fixture())
        row = result["renderers"][0]
        self.assertTrue(row["offline_references_complete"])
        self.assertEqual(row["path"], "character/mesh")
        self.assertEqual(row["bones"][0]["name"], "bone")
        self.assertEqual(row["materials"][0]["textures"][0]["name"], "texture")
        self.assertEqual(row["materials"][0]["shader"]["name"], "shader")
        self.assertFalse(row["conversion_ready"])
        raw = fixture(); raw["snapshot"] = {"assets": [{"path": "characters/missing.prefab"}]}
        self.assertEqual(parse(raw)["missing_resources"], ["missing"])

    def test_same_names_and_same_path_id_in_other_file_do_not_alias(self):
        raw = fixture()
        other = copy.deepcopy(raw["objects"][6]); other["id"] = "other:1"
        other["vertex_count"] = 99; raw["objects"].append(other)
        result = parse(raw)
        self.assertEqual(len(result["meshes"]), 2)
        self.assertEqual(result["renderers"][0]["mesh_id"], "meshes:1")
        raw["objects"] = [o for o in raw["objects"] if o["id"] != "meshes:1"]
        self.assertFalse(parse(raw)["renderers"][0]["offline_references_complete"])

    def test_duplicate_ids_and_hierarchy_cycles_fail(self):
        raw = fixture(); raw["objects"].append(copy.deepcopy(raw["objects"][0]))
        with self.assertRaisesRegex(ValueError, "duplicate serialized"):
            parse(raw)
        raw = fixture(); raw["objects"][3]["parent"] = ref("scene:5")
        self.assertIn("cycle", parse(raw)["renderers"][0]["errors"][0])

    def test_missing_texture_does_not_drop_other_properties(self):
        raw = fixture(); material = raw["objects"][8]
        bad = copy.deepcopy(material["textures"][0]); bad["texture"] = ref("missing:1")
        material["textures"].insert(0, bad)
        row = parse(raw)["renderers"][0]
        self.assertFalse(row["offline_references_complete"])
        self.assertEqual(row["materials"][0]["textures"][1]["name"], "texture")

    def test_bone_count_and_nonfinite_bindpose_are_reported(self):
        raw = fixture(); raw["objects"][6]["bindposes"] = []
        self.assertIn("bones/bindposes count mismatch", parse(raw)["renderers"][0]["errors"])
        raw = fixture(); raw["objects"][6]["bindposes"][0][0] = float("nan")
        self.assertIn("invalid bindpose matrix array", parse(raw)["renderers"][0]["errors"])

    def test_raw_hg_flags_preserved_and_unknown_layout_not_guessed(self):
        raw = fixture(); mesh = raw["objects"][6]
        layout = serialized_layout(mesh["serialized_channels"])
        self.assertEqual(layout["channels"][1]["dimension_raw"], 49)
        self.assertTrue(any("HG flags" in x for x in layout["issues"]))
        mesh["serialized_channels"][1]["format"] = 99
        self.assertTrue(any("unknown" in x for x in serialized_layout(mesh["serialized_channels"])["issues"]))

    def test_runtime_overlay_retains_serialized_eight_byte_uv(self):
        obs = {"schema": 1, "evidence": "synthetic runtime observation", "renderers": [{
            "id": "scene:7", "resource_root": "character", "path": "character/mesh", "mesh_name": "mesh",
            "original_index_count": 3, "strides": [16, 12, 12],
            "attributes": [[0, 0, 3, 0], [1, 3, 4, 0], [4, 0, 2, 1], [6, 3, 4, 1], [12, 4, 4, 2], [13, 6, 4, 2]]}]}
        result = parse(fixture(), obs)
        self.assertEqual(result["meshes"]["meshes:1"]["serialized_layout"]["streams"][1]["minimum_stride"], 8)
        self.assertEqual(result["renderers"][0]["runtime_layout"]["strides"][1], 12)
        self.assertFalse(result["renderers"][0]["conversion_ready"])
        obs["renderers"][0]["original_index_count"] = 6
        with self.assertRaisesRegex(ValueError, "stale"):
            parse(fixture(), obs)
        obs["renderers"][0]["original_index_count"] = 3
        raw = fixture(); raw["snapshot"] = {"perforce_cl": 123, "manifest_version": 4}
        with self.assertRaisesRegex(ValueError, "snapshot"):
            parse(raw, obs)
        obs.update(perforce_cl=123, manifest_version=4)
        self.assertEqual(parse(raw, obs)["summary"]["runtime_layout_observed"], 1)

    def test_dependency_closure_does_not_select_same_named_npc(self):
        character = "chr_0001_test"
        world = f"assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/{character}_postmodel.prefab"
        ui = f"assets/beyond/dynamicassets/gameplay/prefabs/uimodels/{character}_uimodel.prefab"
        manifest = {"Assets": [{"path": world, "bundleIndex": 1}, {"path": ui, "bundleIndex": 2},
                               {"path": world.replace("/characters/", "/npc/"), "bundleIndex": 4}],
                    "Bundles": [{"bundleIndex": i, "name": f"main/{i}.ab", "dependencies": [3] if i < 3 else []}
                                for i in range(1, 5)]}
        assets, names = select_bundles(manifest, character)
        self.assertEqual(len(assets), 2)
        self.assertEqual(len(names), 3)
        self.assertFalse(any("4.ab" in n for n in names))
        manifest["Bundles"].pop(2)
        with self.assertRaisesRegex(ValueError, "missing dependency"):
            select_bundles(manifest, character)


if __name__ == "__main__": unittest.main()
