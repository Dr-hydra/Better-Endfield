"""Enable an unchanged legacy Aurora BEM in the isolated capture installation."""
from pathlib import Path
import argparse
import json
import shutil
import sys
from prepare_capture import ROOT, LIVE, write_settings

sys.path.insert(0, str(ROOT / "tools/CustomModel"))
import bem_v1 as bem

parser = argparse.ArgumentParser()
parser.add_argument("target", choices=["world", "ui"])
args = parser.parse_args()
catalog = LIVE / "local/BetterEndfieldNext/catalog/custom-model"
if (catalog / "native-probe.request").exists():
    raise RuntimeError("Stop the metadata capture before enabling a package.")
source = min(Path("G:/zmd_bem/成品/昼雪").glob("*.bem"), key=lambda p: p.stat().st_size)
manifest, _ = bem.read_package(source, decode=False)
if manifest["target"]["character_id"] != "chr_0014_aurora":
    raise RuntimeError("Unexpected character target")
package = catalog / "packages/aurora-legacy.bem"
package.parent.mkdir(parents=True, exist_ok=True)
shutil.copy2(source, package)
if source.read_bytes() != package.read_bytes():
    raise RuntimeError("Package copy differs")
dll = ROOT / "build/aurora-compat-native/stage/Release/modules/BetterEndfieldNext.CustomModel.dll"
shutil.copy2(dll, LIVE / "runtime/modules/BetterEndfieldNext.CustomModel.dll")
text = ("[CustomModel]\nstandalone_lod=false\nskip_validation=false\nhot_switch=true\n"
        "clone_support=false\nfast_loading=false\noverlay_enabled=true\noverlay_visible=false\n\n"
        f"[Mod.{manifest['package_id']}]\nenabled=true\npackage=packages/aurora-legacy.bem\noptions=\nparameters=\n")
(catalog / "runtime.ini").write_text(text, encoding="utf-8")
write_settings(args.target)
result = {"original_package": str(source), "staged_package": str(package), "package_id": manifest["package_id"],
          "package_format": f"1.{bem.package_minor(source)}", "original_bytes_preserved": True,
          "skip_validation": False, "custom_model_binary": str(dll), "target": args.target}
(LIVE / "validation-setup.json").write_text(json.dumps(result, ensure_ascii=False, indent=2), encoding="utf-8")
print(json.dumps(result, ensure_ascii=True))
