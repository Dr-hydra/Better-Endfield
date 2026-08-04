#include "../include/il2cpp_api.hxx"
#include "../include/utils.hxx"
#include <windows.h>

namespace api {

    bool initialized = false;
    uintptr_t module_base = 0;
    size_t module_size = 0;

    // function pointers
    get_domain_t get_domain = nullptr;
    get_assemblies_t get_assemblies = nullptr;
    assembly_get_image_t assembly_get_image = nullptr;
    image_get_name_t image_get_name = nullptr;

    image_get_class_count_t image_get_class_count = nullptr;
    image_get_class_t image_get_class = nullptr;

    class_get_name_t class_get_name = nullptr;
    class_get_namespace_t class_get_namespace = nullptr;
    class_get_flags_t class_get_flags = nullptr;
    class_get_parent_t class_get_parent = nullptr;
    class_is_valuetype_t class_is_valuetype = nullptr;
    class_is_interface_t class_is_interface = nullptr;
    class_get_interfaces_t class_get_interfaces = nullptr;

    class_num_fields_t class_num_fields = nullptr;
    class_get_fields_t class_get_fields = nullptr;
    field_get_name_t field_get_name = nullptr;
    field_get_type_t field_get_type = nullptr;
    field_get_flags_t field_get_flags = nullptr;
    field_get_offset_t field_get_offset = nullptr;

    class_get_methods_t class_get_methods = nullptr;
    method_get_name_t method_get_name = nullptr;
    method_get_flags_t method_get_flags = nullptr;
    method_get_param_count_t method_get_param_count = nullptr;
    method_get_param_t method_get_param = nullptr;
    method_get_param_name_t method_get_param_name = nullptr;
    method_get_return_type_t method_get_return_type = nullptr;
    method_get_pointer_t method_get_pointer = nullptr;

    type_get_name_t type_get_name = nullptr;
    class_get_type_token_t class_get_type_token = nullptr;

    thread_attach_t thread_attach = nullptr;
    thread_detach_t thread_detach = nullptr;
    thread_current_t thread_current = nullptr;

    custom_attrs_from_class_t custom_attrs_from_class = nullptr;
    custom_attrs_from_method_t custom_attrs_from_method = nullptr;
    custom_attrs_from_field_t custom_attrs_from_field = nullptr;
    custom_attrs_free_t custom_attrs_free = nullptr;

    field_get_token_t field_get_token = nullptr;
    field_static_get_value_t field_static_get_value = nullptr;
    field_get_default_value_t field_get_default_value = nullptr;

    method_get_token_t method_get_token = nullptr;
    method_is_generic_t method_is_generic = nullptr;
    method_is_inflated_t method_is_inflated = nullptr;

    class_from_name_t class_from_name = nullptr;
    class_instance_size_t class_instance_size = nullptr;
    class_value_size_t class_value_size = nullptr;

    class_get_properties_t class_get_properties = nullptr;
    class_get_events_t class_get_events = nullptr;
    property_get_name_t property_get_name = nullptr;
    property_get_get_method_t property_get_get_method = nullptr;
    property_get_set_method_t property_get_set_method = nullptr;
    property_get_flags_t property_get_flags = nullptr;
    event_get_name_t event_get_name = nullptr;
    event_get_add_method_t event_get_add_method = nullptr;
    event_get_remove_method_t event_get_remove_method = nullptr;
    event_get_raise_method_t event_get_raise_method = nullptr;

    class_get_nested_types_t class_get_nested_types = nullptr;

    class_get_method_from_name_t class_get_method_from_name = nullptr;
    runtime_invoke_t runtime_invoke = nullptr;
    type_get_object_t type_get_object = nullptr;
    class_get_type_t class_get_type = nullptr;
    array_length_t array_length = nullptr;
    string_length_t string_length_fn = nullptr;
    string_chars_t string_chars = nullptr;
    object_unbox_t object_unbox = nullptr;
    object_new_t object_new = nullptr;
    object_get_class_t object_get_class = nullptr;
    string_new_t string_new = nullptr;
    array_new_t array_new = nullptr;
    gc_wbarrier_set_field_t gc_wbarrier_set_field = nullptr;
    gchandle_new_t gchandle_new = nullptr;
    gchandle_free_t gchandle_free = nullptr;
    field_get_value_t field_get_value = nullptr;
    field_set_value_t field_set_value = nullptr;
    class_get_field_from_name_t class_get_field_from_name = nullptr;

    gc_register_my_thread_t gc_register_my_thread = nullptr;
    gc_unregister_my_thread_t gc_unregister_my_thread = nullptr;
    gc_disable_t gc_disable = nullptr;
    gc_enable_t gc_enable = nullptr;
    gc_is_disabled_t gc_is_disabled = nullptr;
    gc_disable_boehm_t gc_disable_boehm = nullptr;
    gc_enable_boehm_t gc_enable_boehm = nullptr;
    int * gc_dont_gc_ptr = nullptr;

    void init( ) {
        if ( initialized ) {
            return;
        }

        HMODULE gameAsm = GetModuleHandleA( "GameAssembly.dll" );
        if ( !gameAsm ) {
            Log( "[ERROR] GameAssembly.dll not found" );
            return;
        }

        module_base = reinterpret_cast< uintptr_t >( gameAsm );

        // module size from PE headers (for RVA range validation)
        auto dos = reinterpret_cast< IMAGE_DOS_HEADER * >( gameAsm );
        if ( dos->e_magic == IMAGE_DOS_SIGNATURE ) {
            auto nt = reinterpret_cast< IMAGE_NT_HEADERS * >( module_base + dos->e_lfanew );
            if ( nt->Signature == IMAGE_NT_SIGNATURE ) {
                module_size = nt->OptionalHeader.SizeOfImage;
            }
        }

        // Core functions - fail early if missing
        get_domain = ( get_domain_t ) GetProcAddress( gameAsm, "il2cpp_domain_get" );
        get_assemblies =
            ( get_assemblies_t ) GetProcAddress( gameAsm, "il2cpp_domain_get_assemblies" );
        assembly_get_image = ( assembly_get_image_t ) GetProcAddress(
            gameAsm, "il2cpp_assembly_get_image" );
        image_get_name =
            ( image_get_name_t ) GetProcAddress( gameAsm, "il2cpp_image_get_name" );

        // class / image enumeration
        image_get_class_count = ( image_get_class_count_t ) GetProcAddress(
            gameAsm, "il2cpp_image_get_class_count" );
        image_get_class =
            ( image_get_class_t ) GetProcAddress( gameAsm, "il2cpp_image_get_class" );

        // class reflection
        class_get_name =
            ( class_get_name_t ) GetProcAddress( gameAsm, "il2cpp_class_get_name" );
        class_get_namespace = ( class_get_namespace_t ) GetProcAddress(
            gameAsm, "il2cpp_class_get_namespace" );
        class_get_flags =
            ( class_get_flags_t ) GetProcAddress( gameAsm, "il2cpp_class_get_flags" );
        class_get_parent =
            ( class_get_parent_t ) GetProcAddress( gameAsm, "il2cpp_class_get_parent" );

        class_is_valuetype = ( class_is_valuetype_t ) GetProcAddress(
            gameAsm, "il2cpp_class_is_valuetype" );
        class_is_interface = ( class_is_interface_t ) GetProcAddress(
            gameAsm, "il2cpp_class_is_interface" );
        class_get_interfaces = ( class_get_interfaces_t ) GetProcAddress(
            gameAsm, "il2cpp_class_get_interfaces" );

        // fields
        class_num_fields =
            ( class_num_fields_t ) GetProcAddress( gameAsm, "il2cpp_class_num_fields" );
        class_get_fields =
            ( class_get_fields_t ) GetProcAddress( gameAsm, "il2cpp_class_get_fields" );
        field_get_name =
            ( field_get_name_t ) GetProcAddress( gameAsm, "il2cpp_field_get_name" );
        field_get_type =
            ( field_get_type_t ) GetProcAddress( gameAsm, "il2cpp_field_get_type" );
        field_get_flags =
            ( field_get_flags_t ) GetProcAddress( gameAsm, "il2cpp_field_get_flags" );
        field_get_offset =
            ( field_get_offset_t ) GetProcAddress( gameAsm, "il2cpp_field_get_offset" );

        // methods
        class_get_methods =
            ( class_get_methods_t ) GetProcAddress( gameAsm, "il2cpp_class_get_methods" );
        method_get_name =
            ( method_get_name_t ) GetProcAddress( gameAsm, "il2cpp_method_get_name" );
        method_get_flags =
            ( method_get_flags_t ) GetProcAddress( gameAsm, "il2cpp_method_get_flags" );
        method_get_param_count = ( method_get_param_count_t ) GetProcAddress(
            gameAsm, "il2cpp_method_get_param_count" );
        method_get_param =
            ( method_get_param_t ) GetProcAddress( gameAsm, "il2cpp_method_get_param" );
        method_get_param_name = ( method_get_param_name_t ) GetProcAddress(
            gameAsm, "il2cpp_method_get_param_name" );
        method_get_return_type = ( method_get_return_type_t ) GetProcAddress(
            gameAsm, "il2cpp_method_get_return_type" );
        method_get_pointer = ( method_get_pointer_t ) GetProcAddress(
            gameAsm, "il2cpp_method_get_pointer" );

        type_get_name =
            ( type_get_name_t ) GetProcAddress( gameAsm, "il2cpp_type_get_name" );
        class_get_type_token = ( class_get_type_token_t ) GetProcAddress(
            gameAsm, "il2cpp_class_get_type_token" );

        thread_attach =
            ( thread_attach_t ) GetProcAddress( gameAsm, "il2cpp_thread_attach" );
        thread_detach =
            ( thread_detach_t ) GetProcAddress( gameAsm, "il2cpp_thread_detach" );
        thread_current =
            ( thread_current_t ) GetProcAddress( gameAsm, "il2cpp_thread_current" );

        custom_attrs_from_class = ( custom_attrs_from_class_t ) GetProcAddress(
            gameAsm, "il2cpp_custom_attrs_from_class" );
        custom_attrs_from_method = ( custom_attrs_from_method_t ) GetProcAddress(
            gameAsm, "il2cpp_custom_attrs_from_method" );
        custom_attrs_from_field = ( custom_attrs_from_field_t ) GetProcAddress(
            gameAsm, "il2cpp_custom_attrs_from_field" );
        custom_attrs_free =
            ( custom_attrs_free_t ) GetProcAddress( gameAsm, "il2cpp_custom_attrs_free" );

        field_get_token =
            ( field_get_token_t ) GetProcAddress( gameAsm, "il2cpp_field_get_token" );
        field_static_get_value = ( field_static_get_value_t ) GetProcAddress(
            gameAsm, "il2cpp_field_static_get_value" );
        field_get_default_value = ( field_get_default_value_t ) GetProcAddress(
            gameAsm, "il2cpp_field_get_default_value" );

        method_get_token =
            ( method_get_token_t ) GetProcAddress( gameAsm, "il2cpp_method_get_token" );
        method_is_generic =
            ( method_is_generic_t ) GetProcAddress( gameAsm, "il2cpp_method_is_generic" );
        method_is_inflated = ( method_is_inflated_t ) GetProcAddress(
            gameAsm, "il2cpp_method_is_inflated" );

        class_from_name =
            ( class_from_name_t ) GetProcAddress( gameAsm, "il2cpp_class_from_name" );
        class_instance_size = ( class_instance_size_t ) GetProcAddress(
            gameAsm, "il2cpp_class_instance_size" );
        class_value_size =
            ( class_value_size_t ) GetProcAddress( gameAsm, "il2cpp_class_value_size" );

        class_get_properties = ( class_get_properties_t ) GetProcAddress(
            gameAsm, "il2cpp_class_get_properties" );
        class_get_events =
            ( class_get_events_t ) GetProcAddress( gameAsm, "il2cpp_class_get_events" );
        class_get_nested_types = ( class_get_nested_types_t ) GetProcAddress(
            gameAsm, "il2cpp_class_get_nested_types" );

        property_get_name =
            ( property_get_name_t ) GetProcAddress( gameAsm, "il2cpp_property_get_name" );
        property_get_get_method = ( property_get_get_method_t ) GetProcAddress(
            gameAsm, "il2cpp_property_get_get_method" );
        property_get_set_method = ( property_get_set_method_t ) GetProcAddress(
            gameAsm, "il2cpp_property_get_set_method" );
        property_get_flags = ( property_get_flags_t ) GetProcAddress(
            gameAsm, "il2cpp_property_get_flags" );

        event_get_name =
            ( event_get_name_t ) GetProcAddress( gameAsm, "il2cpp_event_get_name" );
        event_get_add_method = ( event_get_add_method_t ) GetProcAddress(
            gameAsm, "il2cpp_event_get_add_method" );
        event_get_remove_method = ( event_get_remove_method_t ) GetProcAddress(
            gameAsm, "il2cpp_event_get_remove_method" );
        event_get_raise_method = ( event_get_raise_method_t ) GetProcAddress(
            gameAsm, "il2cpp_event_get_raise_method" );

        // Runtime invocation suite (for live scene dumper)
        class_get_method_from_name = ( class_get_method_from_name_t ) GetProcAddress(
            gameAsm, "il2cpp_class_get_method_from_name" );
        runtime_invoke =
            ( runtime_invoke_t ) GetProcAddress( gameAsm, "il2cpp_runtime_invoke" );
        type_get_object =
            ( type_get_object_t ) GetProcAddress( gameAsm, "il2cpp_type_get_object" );
        class_get_type =
            ( class_get_type_t ) GetProcAddress( gameAsm, "il2cpp_class_get_type" );
        array_length = ( array_length_t ) GetProcAddress( gameAsm, "il2cpp_array_length" );
        string_length_fn =
            ( string_length_t ) GetProcAddress( gameAsm, "il2cpp_string_length" );
        string_chars = ( string_chars_t ) GetProcAddress( gameAsm, "il2cpp_string_chars" );
        object_unbox = ( object_unbox_t ) GetProcAddress( gameAsm, "il2cpp_object_unbox" );
        object_new = ( object_new_t ) GetProcAddress( gameAsm, "il2cpp_object_new" );
        object_get_class = ( object_get_class_t ) GetProcAddress(
            gameAsm, "il2cpp_object_get_class" );
        string_new = ( string_new_t ) GetProcAddress( gameAsm, "il2cpp_string_new" );
        array_new = ( array_new_t ) GetProcAddress( gameAsm, "il2cpp_array_new" );
        gc_wbarrier_set_field = ( gc_wbarrier_set_field_t ) GetProcAddress(
            gameAsm, "il2cpp_gc_wbarrier_set_field" );
        gchandle_new = ( gchandle_new_t ) GetProcAddress(
            gameAsm, "il2cpp_gchandle_new" );
        gchandle_free = ( gchandle_free_t ) GetProcAddress(
            gameAsm, "il2cpp_gchandle_free" );
        field_get_value =
            ( field_get_value_t ) GetProcAddress( gameAsm, "il2cpp_field_get_value" );
        field_set_value =
            ( field_set_value_t ) GetProcAddress( gameAsm, "il2cpp_field_set_value" );
        class_get_field_from_name = ( class_get_field_from_name_t ) GetProcAddress(
            gameAsm, "il2cpp_class_get_field_from_name" );

        gc_register_my_thread =
            ( gc_register_my_thread_t ) GetProcAddress( gameAsm, "GC_register_my_thread" );
        gc_unregister_my_thread = ( gc_unregister_my_thread_t ) GetProcAddress(
            gameAsm, "GC_unregister_my_thread" );
        gc_disable = ( gc_disable_t ) GetProcAddress( gameAsm, "il2cpp_gc_disable" );
        gc_enable = ( gc_enable_t ) GetProcAddress( gameAsm, "il2cpp_gc_enable" );
        gc_is_disabled =
            ( gc_is_disabled_t ) GetProcAddress( gameAsm, "il2cpp_gc_is_disabled" );
        gc_disable_boehm = ( gc_disable_boehm_t ) GetProcAddress( gameAsm, "GC_disable" );
        gc_enable_boehm = ( gc_enable_boehm_t ) GetProcAddress( gameAsm, "GC_enable" );
        gc_dont_gc_ptr = ( int * ) GetProcAddress( gameAsm, "GC_dont_gc" );

        if ( !get_domain || !get_assemblies || !assembly_get_image ||
            !image_get_name ) {
            Log( "[ERROR] Failed to resolve core IL2CPP functions" );
            return;
        }

        initialized = true;

        char buf [ 96 ];
        sprintf_s( buf, "[OK] IL2CPP API ready  base=0x%llX  size=0x%zX",
            ( unsigned long long )module_base, module_size );
        Log( buf );

        char diag [ 640 ];
        sprintf_s(
            diag,
            "[diag] gc_register=%s gc_disable=%s gc_enable=%s "
            "GC_disable=%s GC_enable=%s GC_dont_gc=%s "
            "props=%s events=%s field_default=%s method_token=%s "
            "runtime_invoke=%s class_from_name=%s object_new=%s string_new=%s array_new=%s",
            gc_register_my_thread ? "yes" : "NO", gc_disable ? "yes" : "NO",
            gc_enable ? "yes" : "NO", gc_disable_boehm ? "yes" : "NO",
            gc_enable_boehm ? "yes" : "NO", gc_dont_gc_ptr ? "yes" : "NO",
            class_get_properties ? "yes" : "NO", class_get_events ? "yes" : "NO",
            field_get_default_value ? "yes" : "NO", method_get_token ? "yes" : "NO",
            runtime_invoke ? "yes" : "NO", class_from_name ? "yes" : "NO",
            object_new ? "yes" : "NO", string_new ? "yes" : "NO",
            array_new ? "yes" : "NO" );
        Log( diag );
    }

} // namespace api
