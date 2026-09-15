"""Build a compact source profile from AnimeStudio Mesh JSON and type-tree dumps.

The caller supplies an explicit component-to-mesh mapping. Matching an EFMI
draw to a Unity mesh is a separate, evidence-based step; names or counts alone
must not silently select between ambiguous candidates.
"""
import argparse
import json
from pathlib import Path
import re
import copy


def apply_runtime_streams(component: dict, runtime: dict) -> None:
    """Runtime observations override serialized layouts, preserving both as evidence."""
    if not runtime.get("evidence") or not runtime.get("streams"):
        raise ValueError("runtime streams require explicit observations and evidence")
    component["serialized_streams"] = copy.deepcopy(component["streams"])
    for key, stream in runtime["streams"].items():
        if key != "1" or stream.get("semantics") != [
            {"name": "TEXCOORD", "index": 0, "format": "R32G32_FLOAT", "stride": 8},
            {"name": "TEXCOORD", "index": 2, "format": "R8G8B8A8_SNORM", "stride": 4},
        ]:
            raise ValueError("unsupported runtime stream observation")
        component["streams"][int(key)] = copy.deepcopy(stream)
    component["runtime_layout_evidence"] = runtime["evidence"]


def source_streams(text: str) -> list[dict]:
    start = text.index("VertexData m_VertexData")
    end = text.index("m_DataSize", start)
    channels = re.findall(
        r"UInt8 stream = (\d+)\s+UInt8 offset = (\d+)\s+"
        r"UInt8 format = (\d+)\s+UInt8 dimension = (\d+)", text[start:end])
    # HG stores flags in dimension's high nibble. Preserve raw descriptors in
    # the report and interpret only known packed layouts (same convention as
    # the existing EFMI raw-stream adapter).
    known = {
        (0, 0, 3): ("POSITION", "R32G32B32_FLOAT", 12),
        (1, 0, 49): ("ENCODEDDATA", "R32_UINT", 4),
        (4, 0, 2): ("TEXCOORD", "R32G32_FLOAT", 8),
        (6, 3, 36): ("COLOR", "R8G8B8A8_SNORM", 4),
        (12, 4, 4): ("BLENDWEIGHTS", "R16G16B16A16_UNORM", 8),
        (13, 6, 4): ("BLENDINDICES", "R8G8B8A8_UINT", 4),
        (13, 6, 20): ("BLENDINDICES", "R8G8B8A8_UINT", 4),
    }
    streams = [{"semantics": [], "source_channels": []} for _ in range(3)]
    for attr, values in enumerate(channels):
        stream, offset, fmt, dimension = map(int, values)
        if dimension == 0:
            continue
        if stream >= 3 or (attr, fmt, dimension) not in known:
            raise ValueError(f"unsupported source channel {attr}: {values}")
        name, format_name, size = known[attr, fmt, dimension]
        current = streams[stream]
        if offset != sum(s["stride"] for s in current["semantics"]):
            raise ValueError(f"channel {attr}: unsupported gap/overlap at {offset}")
        current["semantics"].append({"name": name, "index": 0, "format": format_name, "stride": size})
        current["source_channels"].append({"attribute": attr, "offset": offset,
                                           "format": fmt, "dimension_raw": dimension})
    if any(not stream["semantics"] for stream in streams):
        raise ValueError("expected three populated source streams")
    return streams


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--mapping", required=True, type=Path)
    parser.add_argument("--json-dir", required=True, type=Path)
    parser.add_argument("--dump-dir", required=True, type=Path)
    parser.add_argument("--output", required=True, type=Path)
    args = parser.parse_args()
    mapping = json.loads(args.mapping.read_text(encoding="utf-8-sig"))
    result = {"schema": 1, "character_id": mapping["character_id"],
              "evidence": mapping["evidence"], "components": {}}
    if mapping.get("requires_geometry_only"):
        result["requires_geometry_only"] = True
    if mapping.get("requires_material_profile"):
        result["requires_material_profile"] = True
    for key, mapped in mapping["components"].items():
        name = mapped["mesh_name"]
        if Path(name).name != name or "/" in name or "\\" in name:
            raise ValueError("mesh_name must be a basename")
        mesh = json.loads((args.json_dir / (name + ".json")).read_text(encoding="utf-8-sig"))
        if mesh["m_Name"] != name:
            raise ValueError(f"mesh identity mismatch: {name}")
        if any(s["topology"] != "Triangles" for s in mesh["m_SubMeshes"]):
            raise ValueError(f"unsupported source topology: {name}")
        result["components"][key] = {
            "mesh_name": name, "mapping_evidence": mapped["evidence"],
            "original_index_count": sum(s["indexCount"] for s in mesh["m_SubMeshes"]),
            "bone_count": len(mesh["m_BindPose"]),
            "streams": source_streams((args.dump_dir / (name + ".txt")).read_text(encoding="utf-8-sig")),
        }
        for option in ("stream_adaptations", "draw_correction"):
            if option in mapped:
                result["components"][key][option] = mapped[option]
        if "runtime_layout" in mapped:
            if "stream_adaptations" in mapped:
                raise ValueError("runtime layout and stream adaptations cannot be combined")
            apply_runtime_streams(result["components"][key], mapped["runtime_layout"])
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(json.dumps(result, ensure_ascii=False, indent=2) + "\n", encoding="utf-8")
    print(f"wrote {args.output}: {len(result['components'])} source components")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
