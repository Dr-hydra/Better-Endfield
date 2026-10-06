"""Author a Windows LOD0 package for the production first-enable regression."""
from __future__ import annotations

import argparse
import importlib.util
from pathlib import Path


def main() -> None:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("output", type=Path)
    args = parser.parse_args()
    source = Path(__file__).resolve().parents[2] / "tools/CustomModel/examples/multi-resource/create_project.py"
    spec = importlib.util.spec_from_file_location("first_enable_resource_fixture", source)
    assert spec is not None and spec.loader is not None
    module = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(module)
    builder = module.fixture()
    # The general cross-platform fixture deliberately uses weapon LOD1. This
    # Windows receiver regression exercises the supported LOD0 contract.
    for resource in builder.m["target"]["resources"]:
        resource["lod"] = 0
    args.output.parent.mkdir(parents=True, exist_ok=True)
    builder.write(args.output)
    print(args.output)


if __name__ == "__main__":
    main()
