"""Independent safety/evidence regressions for generated FP profile inputs."""
import collections
import importlib.util
import json
import pathlib
import unittest

ROOT = pathlib.Path(__file__).resolve().parents[3]
GENERATOR = ROOT / "tools/FirstPersonProfiles/generate_profiles.py"
spec = importlib.util.spec_from_file_location("fp_profile_generator", GENERATOR)
gen = importlib.util.module_from_spec(spec)
spec.loader.exec_module(gen)


class ProfilesEvidenceTests(unittest.TestCase):
    @classmethod
    def setUpClass(cls):
        cls.data = gen.read_json(GENERATOR.with_name("profiles.generated.json"))
        cls.profiles = {profile["model_id"]: profile for profile in cls.data["profiles"]}

    def test_own_primary_skeleton_and_source_coverage(self):
        self.assertEqual(self.data["native_graph_roles"], 32)
        self.assertEqual(self.data["supplemental_scope_roles"], 1)
        self.assertEqual(len(self.profiles), 33)
        self.assertEqual(self.profiles["chr_0023_antal"]["excluded_dependency_roots"],
                         ["chr_0020_meurs_uimodel", "chr_0024_deepfin_uimodel"])
        for profile in self.profiles.values():
            for source in profile["sources"]:
                self.assertTrue(source["model_root"].startswith(profile["model_id"] + "_"))
                self.assertEqual(source["platform"], "windows-x64")
                self.assertTrue(source["revision"])
                if source["graph_verified"]:
                    self.assertEqual(source["head_path"], gen.HEAD)
                    self.assertEqual(source["neck_path"], gen.NECK)
                    self.assertTrue(all("Soldier_Root" not in rule["path"] for rule in source["bone_rules"]))
                else:
                    self.assertEqual(profile["model_id"], "chr_0038_purrche")
                    self.assertFalse(source["head_path"])
                    self.assertFalse(source["bone_rules"])

    def test_spine_and_neck_never_globally_hidden(self):
        roles = set()
        for profile in self.profiles.values():
            for source in profile["sources"]:
                for rule in source["bone_rules"]:
                    if rule["region"] == "HeadAccessory":
                        roles.add(profile["model_id"])
                        self.assertEqual(rule["evidence"], "NamePathInferred")
                        self.assertNotEqual(rule["path"], gen.SPINE2)
                        self.assertTrue(rule["path"].startswith(gen.SPINE2 + "/"))
                    if rule["path"] == gen.NECK:
                        self.assertEqual(rule["semantic"], "Neck")
                        self.assertFalse(rule["subtree"])
        self.assertEqual(roles, {"chr_0002_endminm", "chr_0003_endminf", "chr_0004_pelica",
                                "chr_0007_ikut", "chr_0027_tangtang", "chr_0032_lizhiyan"})

    def test_actual_lod_and_renderer_scope_not_mesh_name(self):
        purrche = self.profiles["chr_0038_purrche"]
        for source in purrche["sources"]:
            fur = next(rule for rule in source["local_rules"] if "fur_03" in rule["original_mesh_name"])
            self.assertEqual(fur["renderer_path"], "Mesh_all/lod0/S_actor_purrchena_fur_03_lod0")
            self.assertEqual(fur["original_mesh_name"], "S_actor_purrchena_fur_03_lod0_20")
            self.assertEqual(fur["evidence"], "DrawWeightsVerified")
        for profile in self.profiles.values():
            for source in profile["sources"]:
                for rule in source["local_rules"]:
                    self.assertTrue(rule["renderer_path"].startswith("Mesh_all/"))
                    self.assertTrue(source["lod_mask"] & (1 << rule["lod"]))

    def test_real_draw_regression_and_body_preservation(self):
        report = gen.read_json(GENERATOR.with_name("geometry_regression.generated.json"))
        self.assertTrue(report["cpp_helper_verified"])
        self.assertFalse(report["android_geometry_verified"])
        self.assertFalse(report["in_game_verified"])
        samples = report["samples"]
        self.assertEqual(len(samples), 25)
        self.assertTrue(all(sample["status"] == "checked" for sample in samples))
        added_hat = collections.Counter()
        for sample in samples:
            added_hat[sample["model_id"]] += sample.get("added_accessory_triangles", 0)
            accounted = sum(sample.get(key, 0) for key in
                            ("generic_head_tail_triangles", "added_accessory_triangles",
                             "added_local_boundary_triangles", "retained_triangles", "degenerate_draw_triangles"))
            self.assertEqual(accounted, sample["draw_triangles"])
            if "cloth_" in sample["mesh"]:
                self.assertGreater(sample.get("retained_triangles", 0), 0)
        self.assertEqual({role for role, count in added_hat.items() if count},
                         {"chr_0002_endminm", "chr_0003_endminf", "chr_0004_pelica",
                          "chr_0007_ikut", "chr_0027_tangtang", "chr_0032_lizhiyan"})
        typhoea = [sample for sample in samples if sample["model_id"] == "chr_0034_typhoea"]
        self.assertEqual(sum(sample.get("added_local_boundary_triangles", 0) for sample in typhoea), 242)

    def test_corrupt_graph_cycle_and_missing_parent_rejected(self):
        objects = [dict(id="go", type="GameObject", name="Root"),
                   dict(id="tr", type="Transform", game_object=dict(id="go"),
                        parent=dict(id="tr", path_id="1"))]
        with self.assertRaisesRegex(ValueError, "cyclic"):
            gen.graph_paths(dict(objects=objects))
        objects[1]["parent"] = dict(id="missing", path_id="2")
        with self.assertRaisesRegex(ValueError, "unresolved"):
            gen.graph_paths(dict(objects=objects))
        objects[1]["parent"] = dict(path_id="0")
        objects[0]["name"] = "../Root"
        with self.assertRaisesRegex(ValueError, "unsafe"):
            gen.graph_paths(dict(objects=objects))


if __name__ == "__main__":
    unittest.main()
