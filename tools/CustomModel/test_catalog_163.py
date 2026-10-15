"""Regression checks for newly shipped 1.6.3 source identities and portable targets."""
import json
from pathlib import Path
import tempfile
import unittest

import bem_v1 as bem
import component_auto


ROOT = Path(__file__).resolve().parent
PROFILES = ROOT / "profiles/endfield-1.6.3"


def load(path):
    return json.loads(path.read_text(encoding="utf-8"))


class Catalog163Tests(unittest.TestCase):
    def test_jsspsi_source_identity_selects_real_catalog_and_rejects_changed_counts(self):
        catalog = load(ROOT / "catalog/chr_0036_jsspsi.json")
        entries = {
            "TextureOverride_EntryPoint_Component" + str(index):
                f"hash = {identity}\nmatch_index_count = {catalog['components'][str(cid)]['original_index_count']}\n"
            for index, (identity, cid) in enumerate(catalog["entries"].items())
        }
        selected, resolved = component_auto.match_catalog(entries, component_auto.catalogs())
        self.assertEqual(selected["character_id"], "chr_0036_jsspsi")
        self.assertEqual(len(resolved), 14)
        first = next(iter(entries))
        entries[first] += "match_index_count = 1\n"
        # Replace, rather than duplicate, the count so INI first-key behavior
        # cannot accidentally hide a contract mismatch.
        entries[first] = entries[first].split("match_index_count", 1)[0] + "match_index_count = 1\n"
        with self.assertRaisesRegex(ValueError, "CHARACTER_REVISION"):
            component_auto.match_catalog(entries, component_auto.catalogs())

    def test_partial_source_layouts_and_packaged_evidence_remain_honest(self):
        catalog_path = ROOT / "catalog/chr_0036_jsspsi.json"
        catalog = load(catalog_path)
        self.assertEqual(catalog["mapping_status"], "offline-resource-identities-partial")
        self.assertEqual(sum(c["source_layout_supported"] for c in catalog["components"].values()), 8)
        self.assertEqual(len(catalog["textures"]), 58)
        self.assertEqual(len(catalog["identity_issues"]), 1)
        for reference in catalog["evidence"]:
            self.assertTrue((catalog_path.parent / reference).is_file(), reference)
        for component in catalog["components"].values():
            for observation in component["evidence"]["observations"].values():
                self.assertTrue((catalog_path.parent / observation["file"]).is_file())

    def test_explicit_targets_keep_real_owner_platform_and_pack_without_research_paths(self):
        index = load(PROFILES / "index.json")
        self.assertEqual(len(index["targets"]), 5)
        with tempfile.TemporaryDirectory() as temporary:
            for item in index["targets"]:
                with self.subTest(profile=item["profile"]):
                    profile = load(PROFILES / item["profile"])
                    project = load(PROFILES / item["project"])
                    self.assertFalse(profile["runtime_verified"])
                    self.assertFalse(profile["conversion_ready"])
                    self.assertFalse(profile["render_verified"])
                    self.assertTrue(all(e["runtime_layout_observed"] for e in profile["evidence"]))
                    self.assertTrue((PROFILES / profile["evidence_file"]).is_file())
                    self.assertTrue(all(r["platforms"] == ["windows-x64"] for r in profile["target"]["resources"]))
                    if "minghe" in item["profile"]:
                        self.assertEqual(profile["target"]["id"], "chr_0037_minghe")
                        self.assertEqual(len(profile["target"]["resources"]), 1)
                    self.assertEqual(project["payload_files"], [])
                    self.assertTrue(all(c["candidates"] == [{"operation": "keep"}] for c in project["manifest"]["component_rules"]))
                    bem.validate_manifest(project["manifest"], 0)
                    # Complete project pack/read exercises actual BEM 1.4 schema.
                    from bem_projects import pack_project
                    output = Path(temporary) / (Path(item["project"]).stem + ".bem")
                    pack_project(PROFILES / item["project"], output)
                    manifest, _ = bem.read_package(output)
                    self.assertEqual(manifest["target"], project["manifest"]["target"])


if __name__ == "__main__":
    unittest.main()
