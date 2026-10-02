"""Regression checks for character IDs that differ from their art codename."""

import contextlib
import io
import json
from pathlib import Path
import sys
import tempfile
import unittest
from unittest.mock import patch


ROOT = Path(__file__).resolve().parents[2]
sys.path.insert(0, str(ROOT / "scripts"))
import GenerateModCharacterPresets as presets
import RefreshEndfieldResourceInputs as refresh
import ScanCharacterAssets as scan


class CharacterPresetTests(unittest.TestCase):
    def test_character_without_corresponding_id_requires_own_model_and_rig(self):
        prefab = {
            "partNameIdList": ["chr_0038_purrche_postmodel"],
            "cpuAnimationTempletName": "NPC/AnimationConfig/Humanoid/Panda/purrchena",
        }
        self.assertTrue(refresh.is_character_prefab(prefab, "npc_chr_0038_purrche.json"))
        self.assertFalse(refresh.is_character_prefab(prefab, "npc_chr_0035_liino.json"))
        self.assertFalse(refresh.is_character_prefab(prefab, "npc_vendor.json"))
        self.assertFalse(refresh.is_character_prefab({**prefab, "partNameIdList": []}, "npc_chr_0038_purrche.json"))
        self.assertFalse(refresh.is_character_prefab({**prefab, "cpuAnimationTempletName": ""}, "npc_chr_0038_purrche.json"))
        self.assertTrue(refresh.is_character_prefab({"correspondingCharId": "chr_0004_pelica"}, "npc_chr_0004_pelica.json"))

    def test_codename_comes_from_animation_config_with_platform_bundle(self):
        for bundle_hash in (101, 202):
            with self.subTest(bundle_hash=bundle_hash), tempfile.TemporaryDirectory() as temporary:
                root = Path(temporary)
                prefab_dir = root / "prefabs"
                prefab_dir.mkdir()
                (prefab_dir / "npc_chr_0038_purrche.json").write_text(json.dumps({
                    "partNameIdList": ["chr_0038_purrche_postmodel"],
                    "cpuAnimationTempletName": "NPC/AnimationConfig/Humanoid/Panda/purrchena",
                }), encoding="utf-8")
                model_path = "assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/chr_0038_purrche_postmodel.prefab"
                assets = [{"path": model_path, "pathHashHead": 1, "bundleIndex": 0}]
                for index, suffix in enumerate(("sit_loop", "sit_sp", "sit_end", "sit_start"), 2):
                    clip = f"a_actor_purrchena_interact_{suffix}"
                    assets.append({"path": f"assets/beyond/arts/entity/actor/panda/purrchena/animations/interact/{clip}.fbx##{clip}",
                                   "pathHashHead": index, "bundleIndex": 0})
                manifest = root / "manifest.json"
                manifest.write_text(json.dumps({"Version": "fixture", "Hash": "fixture",
                    "Bundles": [{"bundleIndex": 0, "name": "fixture", "hashName": bundle_hash}], "Assets": assets}), encoding="utf-8")
                catalog_dir = root / "catalog"
                with patch.object(sys, "argv", ["scan", "--manifest", str(manifest), "--prefab-info", str(prefab_dir),
                                                "--clip-json", str(root / "no-metadata.json"), "--out", str(catalog_dir)]), contextlib.redirect_stdout(io.StringIO()):
                    self.assertEqual(scan.main(), 0)
                output = root / "presets.json"
                with patch.object(sys, "argv", ["generate", "--manifest", str(manifest), "--catalog", str(catalog_dir / "characters.json"),
                                                "--output", str(output)]), contextlib.redirect_stdout(io.StringIO()):
                    self.assertEqual(presets.main(), 0)
                character = json.loads(output.read_text())["characters"][0]
                self.assertEqual(character["id"], "chr_0038_purrche")
                self.assertEqual(character["model"]["bundleHash"], presets.hex_u64(bundle_hash))
                self.assertEqual(character["defaultActionId"], "a_actor_purrchena_interact_sit_loop")
                self.assertEqual(len(character["actions"]), 4)
                self.assertEqual(character["sitToWalk"]["pathHash"], presets.hex_u64(4))


if __name__ == "__main__":
    unittest.main()
