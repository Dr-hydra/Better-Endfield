import json
import tempfile
import unittest
from pathlib import Path

from generate_android_lod_relations import build_rows, render_header, strip_variant_suffix

POLICY = {"schema_version": 1, "platform": "Android", "snapshot": "SNP", "overrides": {}}


def prefab_index(*renderers):
    """Index entry mapping ``Mesh_all/lod1`` renderer paths to mesh identities."""
    return {"renderers": {path: {"mesh": mesh, "vertices": 10, "indices": 30}
                          for path, mesh in renderers}}


def component(mesh, path, resource="chr_0001_test_uimodel"):
    return {"mesh_name": mesh, "evidence": {"observations": {resource: {"path": path}}}}


class BuildRowsTests(unittest.TestCase):
    def setUp(self):
        self.tmp = tempfile.TemporaryDirectory()
        self.dir = Path(self.tmp.name)

    def tearDown(self):
        self.tmp.cleanup()

    def write_catalog(self, character, components, ui=None, world=None):
        payload = {"character_id": character, "ui_resource": ui or f"{character}_uimodel",
                   "world_resource": world or f"{character}_postmodel", "components": components}
        (self.dir / f"{character}.json").write_text(json.dumps(payload), encoding="utf-8")

    def build(self, android=None, indexed=None, emit=False, policy=POLICY):
        return build_rows(policy, self.dir, android or {}, indexed or {}, emit)

    def test_prefab_evidence_overrides_the_archived_catalog(self):
        self.write_catalog("chr_0038_purrche", {
            "15": component("S_actor_purrchena_fur_01_lod0_20",
                            "chr_0038_purrche_uimodel/Mesh_all/lod0/S_actor_purrchena_fur_01_lod0")})
        android = {"chr_0038_purrche": {"modelAssets": [
            {"path": "assets/x/s_actor_purrchena_fur_01_lod1.asset"}]}}
        indexed = {"chr_0038_purrche": prefab_index(
            ("Mesh_all/lod1/S_actor_purrchena_fur_01_lod1", "S_actor_purrchena_fur_01_lod1_11"))}
        rows, dropped = self.build(android, indexed)
        self.assertEqual([], dropped)
        self.assertEqual(1, len(rows))
        self.assertEqual("Mesh_all/lod1/S_actor_purrchena_fur_01_lod1", rows[0]["target_path"])
        self.assertEqual("S_actor_purrchena_fur_01_lod1_11", rows[0]["target_mesh"])
        self.assertEqual("prefab", rows[0]["evidence"])

    def test_missing_renderer_is_dropped_instead_of_falling_back(self):
        # The catalog still carries an (unreliable) LOD1 asset for this character.
        # A character with prefab evidence must not mix the two sources: a renderer
        # the prefab does not expose is dropped and reported, never guessed.
        self.write_catalog("chr_0038_purrche", {
            "15": component("S_actor_purrchena_fur_01_lod0_20",
                            "chr_0038_purrche_uimodel/Mesh_all/lod0/S_actor_purrchena_fur_01_lod0")})
        android = {"chr_0038_purrche": {"modelAssets": [
            {"path": "assets/x/s_actor_purrchena_fur_01_lod1.asset"}]}}
        indexed = {"chr_0038_purrche": prefab_index(
            ("Mesh_all/lod1/S_actor_purrchena_body_01_lod1", "S_actor_purrchena_body_01_lod1"))}
        rows, dropped = self.build(android, indexed)
        self.assertEqual([], rows)
        self.assertEqual(1, len(dropped))
        self.assertIn("no LOD1 renderer in prefab", dropped[0])

    def test_character_without_any_evidence_emits_no_row(self):
        self.write_catalog("chr_9999_ghost", {
            "0": component("S_actor_ghost_body_01_lod0", "chr_9999_ghost_uimodel/Mesh_all/lod0/S_actor_ghost_body_01_lod0")})
        rows, dropped = self.build()
        self.assertEqual([], rows)
        self.assertEqual([], dropped)

    def test_catalog_variant_suffix_is_preserved(self):
        self.write_catalog("chr_0014_aurora", {
            "0": component("S_actor_aurora_fur_01_lod0_20",
                           "chr_0014_aurora_uimodel/Mesh_all/lod0/S_actor_aurora_fur_01_lod0")})
        android = {"chr_0014_aurora": {"modelAssets": [
            {"path": "assets/x/s_actor_aurora_fur_01_lod1_8.asset"}]}}
        rows, _ = self.build(android)
        self.assertEqual(1, len(rows))
        self.assertEqual("S_actor_aurora_fur_01_lod1_8", rows[0]["target_mesh"])
        self.assertEqual("Mesh_all/lod1/S_actor_aurora_fur_01_lod1", rows[0]["target_path"])
        self.assertEqual("catalog", rows[0]["evidence"])

    def test_override_wins_over_both_sources(self):
        policy = json.loads(json.dumps(POLICY))
        policy["overrides"] = {"chr_0038_purrche": {"components": {"15": {
            "target_path": "Mesh_all/lod1/S_manual", "target_mesh": "S_manual"}}}}
        self.write_catalog("chr_0038_purrche", {
            "15": component("S_actor_purrchena_fur_01_lod0_20",
                            "chr_0038_purrche_uimodel/Mesh_all/lod0/S_actor_purrchena_fur_01_lod0")})
        indexed = {"chr_0038_purrche": prefab_index(
            ("Mesh_all/lod1/S_actor_purrchena_fur_01_lod1", "S_actor_purrchena_fur_01_lod1_11"))}
        rows, _ = self.build(None, indexed, policy=policy)
        self.assertEqual("override", rows[0]["evidence"])
        self.assertEqual("S_manual", rows[0]["target_mesh"])

    def test_index_counts_stay_unknown_unless_requested(self):
        self.write_catalog("chr_0038_purrche", {
            "15": component("S_actor_purrchena_fur_01_lod0_20",
                            "chr_0038_purrche_uimodel/Mesh_all/lod0/S_actor_purrchena_fur_01_lod0")})
        indexed = {"chr_0038_purrche": prefab_index(
            ("Mesh_all/lod1/S_actor_purrchena_fur_01_lod1", "S_actor_purrchena_fur_01_lod1_11"))}
        rows, _ = self.build(indexed=indexed)
        self.assertEqual(0, rows[0]["indices"])
        self.assertFalse(rows[0]["indices_known"])
        rows, _ = self.build(indexed=indexed, emit=True)
        self.assertEqual(30, rows[0]["indices"])
        self.assertTrue(rows[0]["indices_known"])

    def test_rendered_row_keeps_the_field_order_the_runtime_expects(self):
        self.write_catalog("chr_0038_purrche", {
            "15": component("S_actor_purrchena_fur_01_lod0_20",
                            "chr_0038_purrche_uimodel/Mesh_all/lod0/S_actor_purrchena_fur_01_lod0")})
        indexed = {"chr_0038_purrche": prefab_index(
            ("Mesh_all/lod1/S_actor_purrchena_fur_01_lod1", "S_actor_purrchena_fur_01_lod1_11"))}
        rows, _ = self.build(indexed=indexed, emit=True)
        header = render_header(rows, POLICY)
        self.assertIn('{{"Android","SNP"},"chr_0038_purrche","chr_0038_purrche_uimodel",'
                      '"chr_0038_purrche_postmodel","Mesh_all/lod0/S_actor_purrchena_fur_01_lod0",'
                      '"S_actor_purrchena_fur_01_lod0_20","Mesh_all/lod1/S_actor_purrchena_fur_01_lod1",'
                      '"S_actor_purrchena_fur_01_lod1_11",true,true,30,true},', header)
        self.assertIn('return {"Android","SNP"};', header)


class SuffixTests(unittest.TestCase):
    def test_variant_suffixes_are_stripped(self):
        self.assertEqual("S_actor_x_lod0", strip_variant_suffix("S_actor_x_lod0_20"))
        self.assertEqual("S_actor_x_lod1", strip_variant_suffix("S_actor_x_lod1_8"))
        self.assertEqual("S_actor_x_lod1_11", strip_variant_suffix("S_actor_x_lod1_11"))


if __name__ == "__main__":
    unittest.main()
