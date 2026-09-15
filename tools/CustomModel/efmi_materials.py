"""Explicit native texture mappings; no implicit filename/component guessing."""
import re
import zlib


def validate_material_profile(profile, character_id=None):
    if profile.get("schema") != 1 or not profile.get("evidence"):
        raise ValueError("material profile requires schema=1 and evidence")
    if character_id and profile.get("character_id") != character_id:
        raise ValueError("material profile character differs from source profile")
    entries = profile.get("textures", [])
    if not entries or len(entries) > 32:
        raise ValueError("material profile needs 1..32 texture bindings")
    slots = set()
    for entry in entries:
        if not re.fullmatch(r"[0-9a-f]{8}", entry.get("source", "")):
            raise ValueError("material source must be an EFMI texture ID")
        target = entry.get("target_texture", "")
        components = entry.get("components", [])
        if (not target or not entry.get("evidence") or not components
                or len(set(components)) != len(components)
                or any(type(c) is not int or not 0 <= c < 32 for c in components)):
            raise ValueError("material binding requires target, unique components and evidence")
        if not zlib.crc32(target.encode("utf-8")):
            raise ValueError("material target name cannot encode as an empty pin")
        for c in components:
            if (c, target) in slots:
                raise ValueError("overlapping material bindings")
            slots.add((c, target))
    return entries


def collect_mapped_textures(src, profile, character_id=None):
    from convert_efmi_poc import Texture, TEXTURE_NAME, parse_dds, PIN_KIND_NAME
    entries = validate_material_profile(profile, character_id)
    files = {}
    for path in src.names:
        match = TEXTURE_NAME.search(path.replace("\\", "/"))
        if match:
            key = match[2].lower()
            if key in files:
                raise ValueError(f"ambiguous texture ID: {key}")
            files[key] = path
    result = []
    for entry in entries:
        key = entry["source"]
        if key not in files:
            raise ValueError(f"mapped texture is missing: {key}")
        texture = Texture()
        (texture.width, texture.height, texture.mip_count, texture.create_format,
         texture.create_srgb, texture.data) = parse_dds(key, src.read_exact(files[key]))
        texture.name = key
        texture.slot_format, texture.slot_srgb = texture.create_format, texture.create_srgb
        texture.component_mask = sum(1 << c for c in entry["components"])
        texture.explicit_slot = zlib.crc32(entry["target_texture"].encode("utf-8")) & 0xffffffff
        texture.pin_kind = PIN_KIND_NAME
        result.append(texture)
    return result
