"""Read only BEM headers/manifests; compare Aurora package targets to the catalog."""
from pathlib import Path
import json
import sys

ROOT = Path(__file__).resolve().parents[5]
sys.path.insert(0, str(ROOT / "tools/CustomModel"))
import bem_v1 as bem

source = Path("G:/zmd_bem/成品/昼雪")
catalog = json.loads((ROOT / "tools/CustomModel/catalog/chr_0014_aurora.json").read_text(encoding="utf-8"))
fields = ("mesh_name", "original_index_count", "bone_names", "materials")
packages = []
for path in sorted(source.glob("*.bem")):
    manifest, _ = bem.read_package(path, decode=False)
    target = manifest["target"]
    differences = []
    for component in target["components"]:
        baseline = catalog["components"].get(str(component["id"]))
        if baseline is None:
            differences.append({"component": component["id"], "field": "missing_catalog_component"})
            continue
        for field in fields:
            if component.get(field) != baseline.get(field):
                differences.append({"component": component["id"], "field": field,
                                    "package": component.get(field), "catalog": baseline.get(field)})
    packages.append({"file": str(path), "name": manifest.get("name"), "format": f"1.{bem.package_minor(path)}",
                     "character_id": target.get("character_id"), "platform": target.get("platform"),
                     "revision": target.get("revision"), "world_resource": target.get("world_resource"),
                     "ui_resource": target.get("ui_resource"), "components": target["components"],
                     "catalog_differences": differences})

report = {"kind": "aurora-package-target-audit", "payloads_decoded": False,
          "catalog_revision": catalog["revision"], "package_count": len(packages), "packages": packages}
destination = Path(__file__).with_name("package-targets.json")
destination.write_text(json.dumps(report, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
print(json.dumps({"packages": len(packages), "catalog_revision": catalog["revision"],
                  "component_counts": [len(p["components"]) for p in packages],
                  "difference_counts": [len(p["catalog_differences"]) for p in packages],
                  "formats": sorted({p["format"] for p in packages}), "report": str(destination)}, ensure_ascii=True))
