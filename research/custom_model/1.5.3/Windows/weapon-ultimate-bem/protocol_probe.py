"""Probe existing BEM 1.3 validation using synthetic, non-playable data."""
import copy
import json
import sys
from pathlib import Path

HERE = Path(__file__).resolve().parent
REPO = next(p for p in HERE.parents if (p / "tools/CustomModel/test_bem_v13.py").is_file())
sys.path.insert(0, str(REPO / "tools/CustomModel"))
import bem_v1
import bem_v11
from bem_tool import check_geometry
from test_bem_v13 import fixture


def main():
    out = HERE / "protocol-fixtures"
    out.mkdir(exist_ok=True)
    builder = fixture()
    builder.m["target"].update(
        character_id="chr_0030_zhuangfy",
        world_resource="chr_0030_zhuangfy_ult_postmodel",
        ui_resource="abilityentity_chr_0030_zhuangfy_ult_postmodel")
    path = out / "synthetic-two-ultimate-roots.bem"
    builder.write(path)
    manifest, payloads = bem_v1.read_package(path)
    check_geometry(manifest, payloads, 3)
    assert bem_v1.package_minor(path) == 3
    result = {"scope": "Python container/manifest/geometry validation only; not a playable Mod",
              "minor": 3, "alternate_two_root_names": "accepted", "negative_cases": {}}
    for label in ("two_stream_static_mesh", "empty_bone_palette", "missing_ui_resource"):
        candidate = copy.deepcopy(manifest)
        if label == "two_stream_static_mesh":
            candidate["meshes"][0]["streams"] = candidate["meshes"][0]["streams"][:2]
            candidate["meshes"][0]["attributes"] = [a for a in candidate["meshes"][0]["attributes"] if a[3] != 2]
        elif label == "empty_bone_palette":
            candidate["meshes"][0]["bones"] = []
        else:
            del candidate["target"]["ui_resource"]
        try:
            bem_v11.validate_manifest(candidate, len(payloads), minor=3)
        except (ValueError, KeyError) as error:
            result["negative_cases"][label] = str(error)
        else:
            raise AssertionError("Unexpected acceptance: " + label)
    (HERE / "protocol-probe.json").write_text(json.dumps(result, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    print(json.dumps(result, ensure_ascii=False))


if __name__ == "__main__":
    main()
