"""Minimal Blender -> BEM editable-project exporter.

The add-on intentionally exports a BEM project, not a final container.  The
portable BEM CLI remains responsible for validation, compression, version
selection and final packaging.  Objects named BEM_C<number> (or carrying the
same ``bem_component_id`` custom property) are exported as replacements.
"""
bl_info = {
    "name": "Better Endfield BEM Exporter",
    "author": "Better Endfield",
    "version": (0, 1, 0),
    "blender": (3, 6, 0),
    "location": "View3D > Sidebar > BEM",
    "category": "Import-Export",
}

import json
import math
import os
import re
import struct
import uuid
from pathlib import Path

try:
    import bpy
    from bpy.props import BoolProperty, EnumProperty, StringProperty
except ImportError:  # Allows syntax checking outside Blender.
    bpy = None


_FORMAT_BYTES = (4, 2, 1, 1, 2, 2, 1, 1, 2, 2, 4, 4)


def _fail(message):
    raise ValueError(message)


def _unit(v):
    length = math.sqrt(sum(x * x for x in v))
    return tuple(x / length for x in v) if length > 1e-12 else (0.0, 0.0, 1.0)


def _dot(a, b):
    return sum(x * y for x, y in zip(a, b))


def _cross(a, b):
    return (a[1] * b[2] - a[2] * b[1], a[2] * b[0] - a[0] * b[2], a[0] * b[1] - a[1] * b[0])


def _oct(n):
    n = _unit(n)
    scale = 1.0 / (abs(n[0]) + abs(n[1]) + abs(n[2]))
    x, y, z = n[0] * scale, n[1] * scale, n[2] * scale
    if z < 0.0:
        x, y = (1.0 - abs(y)) * (1.0 if x >= 0 else -1.0), (1.0 - abs(x)) * (1.0 if y >= 0 else -1.0)
    return x, y


def _encode_tangent(tangent, normal):
    n = _unit(normal)
    r = (n[1] - n[2], n[2] - n[0], n[0] - n[1])
    if math.sqrt(_dot(r, r)) < 1e-6:
        helper = (1.0, 0.0, 0.0) if abs(n[0]) >= 0.9 else (0.0, 1.0, 0.0)
        r = _cross(n, helper)
    r = _unit(r)
    b = _cross(r, n)
    cos_theta = max(-1.0, min(1.0, _dot(tangent, r)))
    sin_theta = max(-1.0, min(1.0, _dot(tangent, b)))
    denom = abs(cos_theta) + abs(sin_theta)
    u = cos_theta / denom if denom > 1e-12 else 1.0
    value = 1.0 - (1.0 - u) / 2.0
    return math.copysign(value, 1.0 if sin_theta == 0.0 else sin_theta)


def _pack_tbn(normal, tangent, bitangent_sign):
    ox, oy = _oct(normal)
    oz = _encode_tangent(tangent, normal)
    def signed10(value):
        return max(-511, min(511, int(round(value * 511.0)))) & 0x3FF
    x, y, z = signed10(ox), signed10(oy), signed10(oz)
    sign = 1 if bitangent_sign >= 0.0 else 0
    return x | (y << 10) | (z << 20) | (1 << 30) | (sign << 31)


def _color_byte(value):
    return max(-128, min(127, int(round((float(value) * 2.0 - 1.0) * 127.0))))


def _catalog(scene):
    path = Path(scene.bem_catalog_path)
    if not path.is_file(): _fail("BEM catalog file does not exist: " + str(path))
    data = json.loads(path.read_text(encoding="utf-8"))
    if data.get("kind") != "bem-character-catalog": _fail("Selected file is not a BEM character catalog")
    return data


def _components(catalog):
    return [(int(k), v) for k, v in sorted(catalog.get("components", {}).items(), key=lambda x: int(x[0]))]


def _attr_layout(component):
    strides = component.get("strides", [])
    attributes = component.get("attributes", [])
    if strides != [16, 12, 12]:
        _fail("Only the verified 16/12/12 skin layout is supported by this exporter")
    offsets = [0, 0, 0]
    result = []
    for attr in attributes:
        if len(attr) < 4 or attr[1] >= len(_FORMAT_BYTES): _fail("Invalid catalog vertex declaration")
        semantic, fmt, dimension, stream = attr[:4]
        result.append([semantic, fmt, dimension, stream, offsets[stream]])
        offsets[stream] += _FORMAT_BYTES[fmt] * dimension
    if offsets != strides: _fail("Catalog vertex declaration does not match its strides")
    return result


def _bone_weights(obj, vertex, bone_names):
    values = []
    for group in vertex.groups:
        if group.group >= len(obj.vertex_groups): continue
        name = obj.vertex_groups[group.group].name
        if name in bone_names and group.weight > 0.0: values.append((group.weight, bone_names.index(name)))
    if not values: _fail("Vertex has no weight for a target bone: " + obj.name)
    values.sort(reverse=True)
    values = values[:4]
    total = sum(v for v, _ in values)
    values = [(v / total, i) for v, i in values]
    weights = [0, 0, 0, 0]; indices = [0, 0, 0, 0]
    for i, (weight, bone) in enumerate(values): weights[i] = weight; indices[i] = bone
    encoded = [int(round(w * 65535.0)) for w in weights]
    encoded[0] += 65535 - sum(encoded)
    if max(indices) > 255: _fail("Target palette contains a bone index above 255")
    return encoded, indices


def _export_mesh(obj, component):
    if len(obj.material_slots) > 1: _fail("Each exported BEM object currently supports one material slot: " + obj.name)
    mesh = obj.data.copy()
    try:
        mesh.calc_loop_triangles()
        mesh.calc_tangents()
        uv_layer = mesh.uv_layers.active
        if uv_layer is None: _fail("Mesh has no active UV layer: " + obj.name)
        colors = getattr(mesh.color_attributes, "active_color", None)
        bone_names = component.get("bone_names", [])
        vertices0, vertices1, vertices2, indices = bytearray(), bytearray(), bytearray(), []
        for triangle in mesh.loop_triangles:
            for loop_index in triangle.loops:
                loop = mesh.loops[loop_index]
                vertex = mesh.vertices[loop.vertex_index]
                position = vertex.co
                normal = tuple(loop.normal)
                tangent = tuple(loop.tangent)
                packed = _pack_tbn(normal, tangent, loop.bitangent_sign)
                uv = uv_layer.data[loop_index].uv
                color = colors.data[loop_index].color if colors else (0.5, 0.5, 0.5, 0.5)
                weights, bones = _bone_weights(obj, vertex, bone_names)
                vertices0 += struct.pack("<3fI", position.x, position.y, position.z, packed)
                vertices1 += struct.pack("<2f4b", uv.x, uv.y, *[_color_byte(x) for x in color])
                vertices2 += struct.pack("<4H4B", *weights, *bones)
                indices.append(len(indices))
        index_size = 2 if len(indices) <= 65535 else 4
        index_bytes = struct.pack("<" + ("H" if index_size == 2 else "I") * len(indices), *indices)
        return dict(vertex_count=len(indices), index_count=len(indices), index_size=index_size,
                    streams=[vertices0, vertices1, vertices2], indices=index_bytes,
                    attributes=_attr_layout(component), bone_names=bone_names)
    finally:
        bpy.data.meshes.remove(mesh)


def _image_payloads(obj, component, payloads, textures, texture_by_name):
    # Texture replacement is explicit: set image["bem_original_name"] in Blender.
    names = set(component.get("material_textures", [[]])[0] if component.get("material_textures") else [])
    result = []
    for slot in obj.material_slots:
        material = slot.material
        if not material or not material.use_nodes: continue
        for node in material.node_tree.nodes:
            image = getattr(node, "image", None)
            original = image.get("bem_original_name") if image else None
            if not image or not original or original not in names: continue
            if original in texture_by_name:
                result.append(texture_by_name[original]); continue
            width, height = image.size
            if width <= 0 or height <= 0: _fail("Image has no pixel data: " + image.name)
            pixels = list(image.pixels[:])
            raw = bytearray()
            for i in range(0, len(pixels), 4): raw += bytes(max(0, min(255, int(round(x * 255.0)))) for x in pixels[i:i + 4])
            payload = len(payloads); payloads.append(bytes(raw))
            texture = dict(width=width, height=height, mips=1, format=4,
                           srgb=bool(image.get("bem_srgb", True)), original_name=original, payload=payload)
            if image.get("bem_semantic") == "normal":
                texture["semantic"] = "normal"
                texture["normal_encoding"] = image.get("bem_normal_encoding", "xyz-unorm")
            texture_by_name[original] = len(textures); textures.append(texture); result.append(texture_by_name[original])
    return result


def export_scene(scene):
    catalog = _catalog(scene)
    character = scene.bem_character_id or catalog.get("character_id")
    if character != catalog.get("character_id"): _fail("Scene character does not match catalog")
    out = Path(scene.bem_output_directory).resolve()
    project_root = out / (scene.bem_project_name or (character + "-bem"))
    project_root.mkdir(parents=True, exist_ok=True)
    payload_root = project_root / "payloads"; payload_root.mkdir(exist_ok=True)
    payloads, meshes, textures, replacements = [], [], [], {}
    texture_by_name = {}
    by_id = dict(_components(catalog))
    for obj in scene.objects:
        component_id = obj.get("bem_component_id")
        match = re.fullmatch(r"BEM_C(\d+)(?:_.*)?", obj.name)
        if component_id is None and match: component_id = int(match.group(1))
        if component_id is None: continue
        component_id = int(component_id)
        if component_id not in by_id: _fail("Unknown BEM component: " + str(component_id))
        component = by_id[component_id]
        data = _export_mesh(obj, component)
        texture_ids = _image_payloads(obj, component, payloads, textures, texture_by_name)
        mesh_id = len(meshes)
        stream_ids = []
        for raw in data["streams"]:
            stream_ids.append(len(payloads)); payloads.append(raw)
        index_id = len(payloads); payloads.append(data["indices"])
        mesh = dict(vertex_count=data["vertex_count"], index_count=data["index_count"], index_size=data["index_size"],
                    streams=[dict(stride=s, payload=p) for s, p in zip(data["strides"] if "strides" in data else [16, 12, 12], stream_ids)],
                    indices=index_id, attributes=data["attributes"],
                    bones=[dict(component=component_id, index=i, name=name) for i, name in enumerate(data["bone_names"])],
                    draws=[dict(start=0, count=data["index_count"], material_component=component_id, material_slot=0,
                                material_name=(component.get("materials") or [""])[0], textures=texture_ids)])
        meshes.append(mesh); replacements[component_id] = dict(target=component_id, operation="replace", mesh=mesh_id)
    if not replacements: _fail("No BEM_C<number> objects were found")
    target_components = []
    for component_id, component in _components(catalog):
        target_components.append(dict(id=component_id, mesh_name=component["mesh_name"],
                                      original_index_count=component["original_index_count"],
                                      bone_names=component.get("bone_names", []), materials=component.get("materials", [])))
    manifest = dict(schema=1, package_id=scene.bem_package_id or "creator." + uuid.uuid4().hex,
                    name=scene.bem_package_name or project_root.name, author=scene.bem_package_author or "未填写",
                    version=scene.bem_package_version or "1.0.0",
                    required_capabilities=["native-materials", "palette-u8", "indices-u32", "fixed-appearances"],
                    target=dict(character_id=character, platform="windows-x64", profile_id=catalog.get("profile_id", ""),
                                revision=catalog.get("revision", ""), snapshot=catalog.get("source_snapshot", {}).get("manifest_version", ""),
                                world_resource=catalog["world_resource"], ui_resource=catalog["ui_resource"], components=target_components),
                    meshes=meshes, textures=textures, default_appearance_id="default",
                    appearances=[dict(id="default", name="Default", components=[replacements.get(i, dict(target=i, operation="keep"))
                                                                                     for i, _ in _components(catalog)])])
    options_text = scene.bem_option_groups_json.strip()
    rules_text = scene.bem_component_rules_json.strip()
    if options_text:
        options = json.loads(options_text)
        if not isinstance(options, list): _fail("Option groups JSON must be an array")
        if rules_text:
            rules = json.loads(rules_text)
        else:
            rules = []
            for i, _ in _components(catalog):
                candidate = dict(replacements.get(i, dict(target=i, operation="keep")))
                candidate.pop("target", None)
                rules.append(dict(target=i, candidates=[candidate]))
        if not isinstance(rules, list): _fail("Component rules JSON must be an array")
        manifest.pop("default_appearance_id", None); manifest.pop("appearances", None)
        manifest["option_groups"] = options; manifest["component_rules"] = rules
        if scene.bem_selection_constraints_json.strip():
            constraints = json.loads(scene.bem_selection_constraints_json)
            if not isinstance(constraints, list): _fail("Selection constraints JSON must be an array")
            manifest["selection_constraints"] = constraints
    names = []
    for index, raw in enumerate(payloads):
        name = f"payloads/{index:04d}.bin"; (project_root / name).write_bytes(raw); names.append(name)
    (project_root / "project.json").write_text(json.dumps(dict(manifest=manifest, payload_files=names), ensure_ascii=False, indent=2), encoding="utf-8")
    deformation_path = ""
    if scene.bem_deformations_path:
        source_deformation = Path(scene.bem_deformations_path).resolve()
        if not source_deformation.is_file(): _fail("BEM deformation file does not exist: " + str(source_deformation))
        target_deformation = project_root / source_deformation.name
        target_deformation.write_bytes(source_deformation.read_bytes())
        deformation_path = target_deformation.name
    task = dict(schema=1, kind="bem-export-task", mode="pack", source="project.json", output="dist/" + project_root.name + ".bem",
                report="reports/build.json", package=dict(id=manifest["package_id"], name=manifest["name"], author=manifest["author"], version=manifest["version"]))
    if deformation_path: task["deformations"] = deformation_path
    (project_root / "export.bemproj.json").write_text(json.dumps(task, ensure_ascii=False, indent=2), encoding="utf-8")
    return str(project_root / "export.bemproj.json")


class BEM_OT_export_project(bpy.types.Operator):
    bl_idname = "bem.export_project"
    bl_label = "Export BEM project"
    bl_options = {"REGISTER", "UNDO"}
    def execute(self, context):
        try:
            path = export_scene(context.scene)
            self.report({"INFO"}, "BEM project exported: " + path)
            return {"FINISHED"}
        except Exception as error:
            self.report({"ERROR"}, str(error)); return {"CANCELLED"}


class BEM_PT_exporter(bpy.types.Panel):
    bl_label = "BEM Exporter"
    bl_idname = "BEM_PT_exporter"
    bl_space_type = "VIEW_3D"
    bl_region_type = "UI"
    bl_category = "BEM"
    def draw(self, context):
        layout = self.layout; scene = context.scene
        layout.prop(scene, "bem_catalog_path"); layout.prop(scene, "bem_character_id")
        layout.prop(scene, "bem_output_directory"); layout.prop(scene, "bem_project_name")
        layout.prop(scene, "bem_package_id"); layout.prop(scene, "bem_package_name")
        layout.prop(scene, "bem_package_author"); layout.prop(scene, "bem_package_version")
        layout.prop(scene, "bem_option_groups_json"); layout.prop(scene, "bem_component_rules_json")
        layout.prop(scene, "bem_selection_constraints_json"); layout.prop(scene, "bem_deformations_path")
        layout.operator(BEM_OT_export_project.bl_idname)


_CLASSES = (BEM_OT_export_project, BEM_PT_exporter)


def register():
    if bpy is None: return
    for cls in _CLASSES: bpy.utils.register_class(cls)
    bpy.types.Scene.bem_catalog_path = StringProperty(name="Catalog JSON", subtype="FILE_PATH")
    bpy.types.Scene.bem_character_id = StringProperty(name="Character ID")
    bpy.types.Scene.bem_output_directory = StringProperty(name="Output directory", subtype="DIR_PATH")
    bpy.types.Scene.bem_project_name = StringProperty(name="Project name", default="bem-project")
    bpy.types.Scene.bem_package_id = StringProperty(name="Package ID")
    bpy.types.Scene.bem_package_name = StringProperty(name="Package name", default="Blender Outfit")
    bpy.types.Scene.bem_package_author = StringProperty(name="Author", default="Author")
    bpy.types.Scene.bem_package_version = StringProperty(name="Version", default="1.0.0")
    bpy.types.Scene.bem_option_groups_json = StringProperty(name="Option groups JSON", default="")
    bpy.types.Scene.bem_component_rules_json = StringProperty(name="Component rules JSON", default="")
    bpy.types.Scene.bem_selection_constraints_json = StringProperty(name="Selection constraints JSON", default="")
    bpy.types.Scene.bem_deformations_path = StringProperty(name="BEM 1.3 deformation JSON", subtype="FILE_PATH")


def unregister():
    if bpy is None: return
    for name in ("bem_catalog_path", "bem_character_id", "bem_output_directory", "bem_project_name",
                 "bem_package_id", "bem_package_name", "bem_package_author", "bem_package_version",
                 "bem_option_groups_json", "bem_component_rules_json", "bem_selection_constraints_json",
                 "bem_deformations_path"):
        if hasattr(bpy.types.Scene, name): delattr(bpy.types.Scene, name)
    for cls in reversed(_CLASSES): bpy.utils.unregister_class(cls)


if __name__ == "__main__" and bpy is not None:
    register()
