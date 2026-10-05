import struct
import sys


IMAGE_BASE = 0x180000000
CODE_REGISTRATION_RVA = 0x0B9217C0


class PeImage:
    def __init__(self, path):
        self.data = open(path, "rb").read()
        pe = struct.unpack_from("<I", self.data, 0x3C)[0]
        section_count = struct.unpack_from("<H", self.data, pe + 6)[0]
        optional_size = struct.unpack_from("<H", self.data, pe + 20)[0]
        section_table = pe + 24 + optional_size
        self.sections = []
        for index in range(section_count):
            offset = section_table + index * 40
            name = self.data[offset:offset + 8].rstrip(b"\0").decode("ascii", "replace")
            virtual_size, virtual_address, raw_size, raw_address = struct.unpack_from(
                "<IIII", self.data, offset + 8)
            self.sections.append((name, virtual_address, virtual_size, raw_address, raw_size))

    def offset(self, address):
        rva = address - IMAGE_BASE if address >= IMAGE_BASE else address
        for _name, va, vs, raw, rs in self.sections:
            if va <= rva < va + max(vs, rs):
                delta = rva - va
                if delta >= rs:
                    raise ValueError("address is in virtual-only section tail: 0x%X" % address)
                return raw + delta
        raise ValueError("unmapped address: 0x%X" % address)

    def qword(self, address):
        return struct.unpack_from("<Q", self.data, self.offset(address))[0]

    def qwords(self, address, count):
        return struct.unpack_from("<" + "Q" * count, self.data, self.offset(address))

    def cstring(self, address, limit=512):
        offset = self.offset(address)
        end = self.data.find(b"\0", offset, offset + limit)
        if end < 0:
            end = offset + limit
        return self.data[offset:end].decode("utf-8", "replace")

    def va(self, offset):
        for _name, virtual_address, _virtual_size, raw_address, raw_size in self.sections:
            if raw_address <= offset < raw_address + raw_size:
                return IMAGE_BASE + virtual_address + offset - raw_address
        raise ValueError("unmapped file offset: 0x%X" % offset)


def scan_metadata_registration(image, type_definition_count):
    """Find the v29 Il2CppMetadataRegistration by its two type-count fields."""
    marker = struct.pack("<Q", type_definition_count)
    start = 0
    candidates = []
    while True:
        offset = image.data.find(marker, start)
        if offset < 0:
            break
        start = offset + 1
        # fieldOffsetsCount, fieldOffsets, typeDefinitionsSizesCount, pointer
        if offset + 32 > len(image.data):
            continue
        if struct.unpack_from("<Q", image.data, offset + 16)[0] != type_definition_count:
            continue
        registration_offset = offset - 80
        if registration_offset < 0:
            continue
        values = struct.unpack_from("<" + "Q" * 16, image.data, registration_offset)
        try:
            registration_va = image.va(registration_offset)
        except ValueError:
            continue
        candidates.append((registration_va, values))
    print("metadata registration candidates")
    labels = [
        "generic_classes_count", "generic_classes", "generic_insts_count", "generic_insts",
        "generic_method_table_count", "generic_method_table", "types_count", "types",
        "method_specs_count", "method_specs", "field_offsets_count", "field_offsets",
        "type_sizes_count", "type_sizes", "metadata_usages_count", "metadata_usages",
    ]
    for registration_va, values in candidates:
        print("  candidate=0x%X" % registration_va)
        for label, value in zip(labels, values):
            print("    %-28s 0x%X (%d)" % (label, value, value))
    return candidates


def inspect_usage_targets(image, metadata, registration_values):
    generic_insts_count, generic_insts = registration_values[2], registration_values[3]
    types_count, types_pointer = registration_values[6], registration_values[7]
    method_specs_count, method_specs_pointer = registration_values[8], registration_values[9]

    def metadata_u32(offset):
        return struct.unpack_from("<I", metadata, offset)[0]

    string_offset = metadata_u32(24)
    methods_offset = metadata_u32(48)
    type_definitions_offset = metadata_u32(160)
    type_record_size = 92

    def metadata_string(index):
        start = string_offset + index
        end = metadata.find(b"\0", start)
        return metadata[start:end].decode("utf-8", "replace")

    def type_definition_name(index):
        if index < 0 or type_definitions_offset + index * type_record_size + 8 > len(metadata):
            return "<type-def:%d>" % index
        offset = type_definitions_offset + index * type_record_size
        name_index, namespace_index = struct.unpack_from("<II", metadata, offset)
        name = metadata_string(name_index)
        namespace = metadata_string(namespace_index)
        return (namespace + "." if namespace else "") + name

    first_type_index = struct.unpack_from("<i", metadata, type_definitions_offset + 8)[0]

    def type_pointer(index):
        if not 0 <= index < types_count:
            raise IndexError("type index %d outside %d" % (index, types_count))
        return image.qword(types_pointer + index * 8)

    first_pointer = type_pointer(first_type_index)
    first_data, first_bits = image.qwords(first_pointer, 2)
    metadata_image_base = first_data - type_definitions_offset
    print("type resolver: first_index=%d first_ptr=0x%X first_data=0x%X bits=0x%X metadata_base=0x%X" %
          (first_type_index, first_pointer, first_data, first_bits, metadata_image_base))

    primitive_names = {
        0x01: "System.Void", 0x02: "System.Boolean", 0x03: "System.Char",
        0x04: "System.SByte", 0x05: "System.Byte", 0x06: "System.Int16",
        0x07: "System.UInt16", 0x08: "System.Int32", 0x09: "System.UInt32",
        0x0A: "System.Int64", 0x0B: "System.UInt64", 0x0C: "System.Single",
        0x0D: "System.Double", 0x0E: "System.String", 0x18: "System.IntPtr",
        0x19: "System.UIntPtr", 0x1C: "System.Object",
    }

    def resolve_type_at(pointer, depth=0):
        if depth > 8:
            return "<recursive>"
        data, bits = image.qwords(pointer, 2)
        kind = (bits >> 16) & 0xFF
        if kind in primitive_names:
            return primitive_names[kind]
        if kind in (0x11, 0x12):
            if data < 63987:
                return type_definition_name(data)
            delta = data - metadata_image_base - type_definitions_offset
            if delta % type_record_size == 0:
                return type_definition_name(delta // type_record_size)
            return "<class-handle:0x%X>" % data
        if kind in (0x0F, 0x1D):
            suffix = "*" if kind == 0x0F else "[]"
            return resolve_type_at(data, depth + 1) + suffix
        if kind == 0x15:
            generic_type, class_inst, _method_inst, _cached = image.qwords(data, 4)
            base_name = resolve_type_at(generic_type, depth + 1)
            return base_name + resolve_generic_inst_pointer(class_inst, depth + 1)
        if kind == 0x13:
            return "!%d" % data
        if kind == 0x1E:
            return "!!%d" % data
        if kind == 0xFF:
            return resolve_type_index(data, depth + 1)
        return "<type kind=0x%X data=0x%X bits=0x%X>" % (kind, data, bits)

    def resolve_generic_inst_pointer(pointer, depth=0):
        if pointer == 0:
            return ""
        argc, argv = image.qwords(pointer, 2)
        if argc > 64:
            return "<bad-inst:0x%X>" % pointer
        args = [resolve_type_at(image.qword(argv + i * 8), depth + 1) for i in range(argc)]
        return "<" + ", ".join(args) + ">"

    def resolve_generic_inst_index(index):
        if index < 0:
            return ""
        if index >= generic_insts_count:
            return "<generic-inst-index:%d>" % index
        return resolve_generic_inst_pointer(image.qword(generic_insts + index * 8))

    def resolve_type_index(index, depth=0):
        return resolve_type_at(type_pointer(index), depth)

    type_indices = [140018, 134412, 134330, 134339]
    print("decoded TypeInfo entries")
    for index in type_indices:
        pointer = type_pointer(index)
        raw = image.qwords(pointer, 2)
        print("  type[%d] ptr=0x%X data=0x%X bits=0x%X => %s" %
              (index, pointer, raw[0], raw[1], resolve_type_index(index)))

    fields_offset = metadata_u32(96)
    print("EventManager field types")
    for field_index in range(184839, 184843):
        field_offset = fields_offset + field_index * 12
        name_index, field_type_index = struct.unpack_from("<Ii", metadata, field_offset)
        print("  field[%d] %s typeIndex=%d => %s" %
              (field_index, metadata_string(name_index), field_type_index,
               resolve_type_index(field_type_index)))

    method_spec_indices = [595791, 513422, 595759, 167306]
    print("decoded MethodRef entries")
    for index in method_spec_indices:
        if index >= method_specs_count:
            print("  methodSpec[%d] outside %d" % (index, method_specs_count))
            continue
        offset = image.offset(method_specs_pointer) + index * 12
        method_definition_index, class_index, method_index = struct.unpack_from("<iii", image.data, offset)
        method_offset = methods_offset + method_definition_index * 32
        name_index, declaring_type = struct.unpack_from("<Ii", metadata, method_offset)
        method_name = metadata_string(name_index)
        owner = type_definition_name(declaring_type) + resolve_generic_inst_index(class_index)
        method_name += resolve_generic_inst_index(method_index)
        print("  methodSpec[%d] def=%d classInst=%d methodInst=%d => %s.%s" %
              (index, method_definition_index, class_index, method_index, owner, method_name))

    parameters_offset = metadata_u32(88)
    print("CameraManager target signatures")
    for method_definition_index in [47290, 47291, 47364]:
        method_offset = methods_offset + method_definition_index * 32
        name_index, declaring_type, return_type, parameter_start = struct.unpack_from(
            "<Iiii", metadata, method_offset)
        parameter_count = struct.unpack_from("<H", metadata, method_offset + 30)[0]
        parameter_types = []
        for parameter_index in range(parameter_start, parameter_start + parameter_count):
            parameter_offset = parameters_offset + parameter_index * 12
            parameter_type = struct.unpack_from("<i", metadata, parameter_offset + 8)[0]
            parameter_types.append(resolve_type_index(parameter_type))
        print("  def[%d] %s.%s(%s) => %s" % (
            method_definition_index, type_definition_name(declaring_type),
            metadata_string(name_index), ", ".join(parameter_types),
            resolve_type_index(return_type)))


def main():
    image = PeImage(sys.argv[1])
    metadata = open(sys.argv[2], "rb").read() if len(sys.argv) > 2 else None
    code_reg = IMAGE_BASE + CODE_REGISTRATION_RVA
    metadata_registrations = scan_metadata_registration(image, 63987)
    if metadata and metadata_registrations:
        inspect_usage_targets(image, metadata, metadata_registrations[0][1])
    values = image.qwords(code_reg, 17)
    labels = [
        "reverse_count", "reverse_ptr", "generic_count", "generic_ptr",
        "generic_adjustor_ptr", "invoker_count", "invoker_ptr",
        "unresolved_count", "unresolved_ptr", "unresolved_instance_ptr",
        "unresolved_static_ptr", "interop_count", "interop_ptr",
        "winrt_count", "winrt_ptr", "module_count", "modules_ptr",
    ]
    for label, value in zip(labels, values):
        print("%-28s 0x%X" % (label, value))
    module_count = values[15]
    module_array = values[16]
    print("modules")
    targets = {"Gameplay.Beyond.dll", "Lua.Beyond.dll", "UI.Beyond.dll"}
    reverse_targets = {
        IMAGE_BASE + rva: label for rva, label in (
            (0x03F0CCE0, "ToggleUI_Core"),
            (0x01852060, "ToggleUI_HotfixPath"),
            (0x036D0BB0, "DisableHudFade_Core"),
            (0x06375088, "DisableHudFade_HotfixPath"),
            (0x03CD8810, "Dramatic_SetCategory"),
            (0x032F1D80, "GetUIController"),
            (0x03B9D810, "ToggleUI_Core_Call1"),
            (0x03BE2A40, "ToggleUI_Core_Call2"),
            (0x03192670, "ToggleUI_Core_Call3"),
            (0x03195AF0, "ToggleUI_Core_Call4"),
            (0x000995D0, "ToggleUI_Core_Call5"),
            (0x03194A80, "DisableHudFade_Core_Final"),
            (0x03194580, "ToggleClearScreen_Dispatch"),
            (0x03194740, "ToggleClearScreenButRadio_Dispatch"),
            (0x03BE3DB0, "Action_GetBoolParameter"),
            (0x04111710, "DisableHudFade_Add"),
            (0x03314E70, "DisableHudFade_Remove"),
        )
    }
    matches = []
    for index in range(module_count):
        module = image.qword(module_array + index * 8)
        module_values = image.qwords(module, 16)
        name = image.cstring(module_values[0])
        if name not in targets:
            # Still scan every module below for reverse-map targets.
            pass
        else:
            print("  %s struct=0x%X methods=%d method_ptrs=0x%X" %
                  (name, module, module_values[1], module_values[2]))
        if name == "Gameplay.Beyond.dll":
            for label, token in (
                ("DisableHudFade", 0x060080A3),
                ("DramaticPerformanceForbidLevelUI", 0x060080A4),
                ("ToggleUI", 0x060080A9),
                ("ShowHideEntireHUD.Execute", 0x060090E3),
                ("ToggleClearScreen.Execute", 0x06009106),
                ("ToggleClearScreenButRadio.Execute", 0x06009109),
                ("CameraManager.AddUICamCullingMaskConfig", 0x0600B8BB),
                ("CameraManager.RemoveUICamCullingMaskConfig", 0x0600B8BC),
                ("CameraManager._UpdateUICamCullingMask", 0x0600B8BD),
                ("CameraUtils.get_cameraManager", 0x0600B905),
            ):
                rid = token & 0x00FFFFFF
                pointer = image.qword(module_values[2] + (rid - 1) * 8)
                print("    %s token=0x%08X va=0x%X rva=0x%X" %
                      (label, token, pointer, pointer - IMAGE_BASE))
        if name == "Lua.Beyond.dll":
            for label, token in (
                ("LuaEventSystem.DispatchEvent(string)", 0x06000009),
                ("LuaManager.get_instance", 0x06000051),
            ):
                rid = token & 0x00FFFFFF
                pointer = image.qword(module_values[2] + (rid - 1) * 8)
                print("    %s token=0x%08X va=0x%X rva=0x%X" %
                      (label, token, pointer, pointer - IMAGE_BASE))
        for method_index in range(module_values[1]):
            pointer = image.qword(module_values[2] + method_index * 8)
            if pointer in reverse_targets:
                token = 0x06000000 | (method_index + 1)
                matches.append((reverse_targets[pointer], name, token, pointer))

    print("reverse method map")
    method_names = {}
    if metadata:
        string_offset = struct.unpack_from("<I", metadata, 24)[0]
        methods_offset, methods_size = struct.unpack_from("<II", metadata, 48)
        for offset in range(methods_offset, methods_offset + methods_size, 32):
            name_index = struct.unpack_from("<I", metadata, offset)[0]
            token = struct.unpack_from("<I", metadata, offset + 20)[0]
            start = string_offset + name_index
            end = metadata.find(b"\0", start)
            method_names[token] = metadata[start:end].decode("utf-8", "replace")
    for label, module_name, token, pointer in matches:
        print("  %s -> %s token=0x%08X method=%s va=0x%X" %
              (label, module_name, token, method_names.get(token, "?"), pointer))


if __name__ == "__main__":
    main()
