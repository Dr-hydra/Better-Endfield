#include "../include/il2cpp_class.hxx"
#include "../include/il2cpp_api.hxx"
#include "../include/utils.hxx"
#include <algorithm>
#include <excpt.h>
#include <windows.h>

static std::vector<std::string> ParseAttrCache( void * cache ) {
    std::vector<std::string> result;
    if ( !cache )
        return result;

    __try {
        struct CACache {
            int32_t count;
            int32_t pad;
            void ** attrs;
        };
        auto * c = reinterpret_cast< CACache * >( cache );

        if ( c->count <= 0 || c->count > 256 || !c->attrs )
            return result;

        for ( int i = 0; i < c->count; ++i ) {
            void * attr = c->attrs [ i ];
            if ( !attr )
                continue;

            void * attrKlass = *reinterpret_cast< void ** >( attr );
            if ( !attrKlass )
                continue;

            const char * n =
                api::class_get_name ? api::class_get_name( attrKlass ) : nullptr;
            const char * ns = api::class_get_namespace
                ? api::class_get_namespace( attrKlass )
                : nullptr;
            if ( !n || !*n )
                continue;

            std::string fq = ( ns && *ns ) ? std::string( ns ) + "." + n : std::string( n );

            const std::string suffix = "Attribute";

            if ( fq.size( ) > suffix.size( ) &&
                fq.compare( fq.size( ) - suffix.size( ), suffix.size( ), suffix ) == 0 ) {
                fq.resize( fq.size( ) - suffix.size( ) );
            }
            result.push_back( std::move( fq ) );
        }
    }
    __except ( EXCEPTION_EXECUTE_HANDLER ) {
        result.clear( );
    }

    return result;
}

int32_t Il2CppClass::InstanceSize( ) const {
    return ( api::class_instance_size && klass ) ? api::class_instance_size( klass )
        : 0;
}

static std::string MethodName( void * m ) {
    if ( !m || !api::method_get_name )
        return "";
    const char * n = api::method_get_name( m );
    return n ? n : "";
}

std::vector<PropertyData> Il2CppClass::GetProperties( ) const {
    std::vector<PropertyData> result;
    if ( !klass || !api::class_get_properties || !api::property_get_name )
        return result;

    void * iter = nullptr;
    while ( void * p = api::class_get_properties( klass, &iter ) ) {
        if ( !p )
            break;
        const char * pn = api::property_get_name( p );
        if ( !pn || !*pn )
            continue;

        PropertyData pd;
        pd.name = pn;
        pd.flags = api::property_get_flags ? api::property_get_flags( p ) : 0;
        if ( api::property_get_get_method )
            pd.getterName = MethodName( api::property_get_get_method( p ) );
        if ( api::property_get_set_method )
            pd.setterName = MethodName( api::property_get_set_method( p ) );

        result.push_back( std::move( pd ) );
    }

    return result;
}

std::vector<EventData> Il2CppClass::GetEvents( ) const {
    std::vector<EventData> result;
    if ( !klass )
        return result;

    if ( api::class_get_events && api::event_get_name ) {
        void * iter = nullptr;
        while ( void * e = api::class_get_events( klass, &iter ) ) {
            if ( !e )
                break;
            const char * en = api::event_get_name( e );
            if ( !en || !*en )
                continue;

            EventData ed;
            ed.name = en;
            if ( api::event_get_add_method )
                ed.addName = MethodName( api::event_get_add_method( e ) );
            if ( api::event_get_remove_method )
                ed.removeName = MethodName( api::event_get_remove_method( e ) );
            if ( api::event_get_raise_method )
                ed.raiseName = MethodName( api::event_get_raise_method( e ) );

            result.push_back( std::move( ed ) );
        }
    }

    if ( !result.empty( ) )
        return result;

    if ( !api::class_get_methods || !api::method_get_name )
        return result;

    std::vector<std::string> adds, removes, raises;
    void * iter = nullptr;
    while ( auto m = api::class_get_methods( klass, &iter ) ) {
        if ( !m )
            break;
        const char * n = api::method_get_name( m );
        if ( !n )
            continue;
        std::string s = n;
        if ( s.rfind( "add_", 0 ) == 0 )
            adds.push_back( s.substr( 4 ) );
        else if ( s.rfind( "remove_", 0 ) == 0 )
            removes.push_back( s.substr( 7 ) );
        else if ( s.rfind( "raise_", 0 ) == 0 )
            raises.push_back( s.substr( 6 ) );
    }

    for ( const auto & a : adds ) {
        bool hasRemove =
            std::find( removes.begin( ), removes.end( ), a ) != removes.end( );
        if ( !hasRemove )
            continue;

        EventData ed;
        ed.name = a;
        ed.addName = "add_" + a;
        ed.removeName = "remove_" + a;
        if ( std::find( raises.begin( ), raises.end( ), a ) != raises.end( ) )
            ed.raiseName = "raise_" + a;
        result.push_back( std::move( ed ) );
    }

    return result;
}

std::vector<std::string> Il2CppClass::GetAttributes( ) const {
    if ( !klass || !api::custom_attrs_from_class )
        return {};
    void * cache = nullptr;
    __try {
        cache = api::custom_attrs_from_class( klass );
    }
    __except ( EXCEPTION_EXECUTE_HANDLER ) {
        return {};
    }
    return ParseAttrCache( cache );
}

const char * Il2CppClass::GetName( ) const {
    return api::class_get_name && klass ? api::class_get_name( klass ) : "Unknown";
}

const char * Il2CppClass::GetNamespace( ) const {
    return api::class_get_namespace && klass ? api::class_get_namespace( klass )
        : "";
}

bool Il2CppClass::IsValueType( ) const {
    return api::class_is_valuetype && klass ? api::class_is_valuetype( klass )
        : false;
}

bool Il2CppClass::IsInterface( ) const {
    return api::class_is_interface && klass ? api::class_is_interface( klass )
        : false;
}

uint32_t Il2CppClass::GetFlags( ) const {
    return api::class_get_flags && klass ? api::class_get_flags( klass ) : 0;
}

uint32_t Il2CppClass::GetTypeToken( ) const {
    return api::class_get_type_token && klass ? api::class_get_type_token( klass )
        : 0;
}

Il2CppClass Il2CppClass::GetParent( ) const {
    return Il2CppClass(
        api::class_get_parent && klass ? api::class_get_parent( klass ) : nullptr );
}

std::vector<Il2CppClass> Il2CppClass::GetInterfaces( ) const {
    std::vector<Il2CppClass> ifaces;

    if ( !klass || !api::class_get_interfaces ) {
        return ifaces;
    }

    void * iter = nullptr;
    while ( auto iface = api::class_get_interfaces( klass, &iter ) ) {
        if ( !iface )
            break;
        ifaces.emplace_back( iface );
    }

    return ifaces;
}

std::vector<FieldData> Il2CppClass::GetFields( ) const {
    std::vector<FieldData> fields;

    if ( !klass || !api::class_get_fields ) {
        return fields;
    }

    void * iter = nullptr;
    while ( auto field = api::class_get_fields( klass, &iter ) ) {
        if ( !field )
            break;

        const char * name =
            api::field_get_name ? api::field_get_name( field ) : nullptr;
        if ( !name || !*name )
            continue;

        FieldData fd;
        fd.flags = api::field_get_flags ? api::field_get_flags( field ) : 0;
        fd.offset = api::field_get_offset ? api::field_get_offset( field ) : -1;
        fd.name = name;
        fd.token = api::field_get_token ? api::field_get_token( field ) : 0;
        fd.raw = field;

        void * ftype = api::field_get_type ? api::field_get_type( field ) : nullptr;
        const char * tname =
            ftype && api::type_get_name ? api::type_get_name( ftype ) : nullptr;
        fd.type = tname ? tname : "object";

        if ( api::custom_attrs_from_field ) {
            void * cache = nullptr;
            __try {
                cache = api::custom_attrs_from_field( klass, field );
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
                cache = nullptr;
            }
            fd.attributes = ParseAttrCache( cache );
        }

        fields.emplace_back( std::move( fd ) );
    }

    return fields;
}

std::vector<MethodData> Il2CppClass::GetMethods( ) const {
    std::vector<MethodData> methods;

    if ( !klass || !api::class_get_methods ) {
        return methods;
    }

    void * iter = nullptr;
    while ( auto method = api::class_get_methods( klass, &iter ) ) {
        if ( !method )
            break;

        const char * mname =
            api::method_get_name ? api::method_get_name( method ) : nullptr;
        if ( !mname || !*mname )
            continue;

        MethodData md;
        md.name = mname;
        md.iflags = 0;
        md.token = api::method_get_token ? api::method_get_token( method ) : 0;
        md.isGeneric =
            api::method_is_generic ? api::method_is_generic( method ) : false;

        md.flags =
            api::method_get_flags ? api::method_get_flags( method, &md.iflags ) : 0;

        void * rtype = api::method_get_return_type
            ? api::method_get_return_type( method )
            : nullptr;
        const char * rtname =
            rtype && api::type_get_name ? api::type_get_name( rtype ) : nullptr;
        md.returnType = rtname ? rtname : "void";

        uint32_t pcount =
            api::method_get_param_count ? api::method_get_param_count( method ) : 0;
        md.params.reserve( pcount );

        for ( uint32_t i = 0; i < pcount; ++i ) {
            const void * ptype =
                api::method_get_param ? api::method_get_param( method, i ) : nullptr;
            const char * ptname = ptype && api::type_get_name
                ? api::type_get_name( const_cast< void * >( ptype ) )
                : nullptr;

            const char * pname = api::method_get_param_name
                ? api::method_get_param_name( method, i )
                : nullptr;

            ParamData pd;
            pd.type = ptname ? ptname : "object";
            pd.name = pname ? pname : ( "arg" + std::to_string( i ) );
            md.params.emplace_back( std::move( pd ) );
        }

        md.address = 0;
        md.rva = 0;

        auto tryAddr = [ & ] ( uintptr_t addr ) -> bool {
            if ( !addr || !api::module_base )
                return false;
            if ( addr < api::module_base )
                return false;
            if ( api::module_size && addr >= api::module_base + api::module_size )
                return false;
            md.address = addr;
            md.rva = static_cast< uint64_t >( addr - api::module_base );
            return true;
            };

        if ( api::method_get_pointer ) {
            uintptr_t a =
                reinterpret_cast< uintptr_t >( api::method_get_pointer( method ) );
            tryAddr( a );
        }

        if ( !md.address ) {
            __try {
                uintptr_t a = *reinterpret_cast< uintptr_t * >( method );
                tryAddr( a );
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
            }
        }

        ++g_rvaTotal;
        if ( md.rva )
            ++g_rvaResolved;

        if ( api::custom_attrs_from_method ) {
            void * cache = nullptr;
            __try {
                cache = api::custom_attrs_from_method( method );
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
                cache = nullptr;
            }
            md.attributes = ParseAttrCache( cache );
        }

        methods.emplace_back( std::move( md ) );
    }

    return methods;
}
