#include "../include/dumper.hxx"
#include "../include/il2cpp_api.hxx"
#include "../include/utils.hxx"
#include <algorithm>
#include <fstream>
#include <iomanip>
#include <sstream>
#include <unordered_map>
#include <unordered_set>

Dumper::Dumper( ) {
    api::init( );
    Sleep( 500 );
    if ( !api::initialized ) {
        return;
    }

    void * domain = api::get_domain( );
    if ( !domain ) {
        Log( "[ERROR] No domain" );
        return;
    }

    size_t count = 0;
    void ** assemblies = api::get_assemblies( domain, &count );
    if ( !assemblies || count == 0 ) {
        Log( "[ERROR] No assemblies" );
        return;
    }

    for ( size_t i = 0; i < count; ++i ) {
        void * ass = assemblies [ i ];
        if ( !ass )
            continue;

        void * img = api::assembly_get_image( ass );
        if ( !img )
            continue;

        const char * name = api::image_get_name( img );
        if ( !name || !*name )
            continue;

        images.emplace_back( img );
    }
}

std::map<std::string, std::vector<CachedClass>>
Dumper::CacheImage( const Il2CppImage & img ) const {
    std::map<std::string, std::vector<CachedClass>> ns_classes;

    const size_t total = img.GetClassCount( );
    for ( size_t i = 0; i < total; ++i ) {
        auto cls = img.GetClassByIndex( i );
        if ( !cls.klass )
            continue;

        const char * rawName = cls.GetName( );
        if ( !rawName || !*rawName )
            continue;

        std::string name = rawName;

        CachedClass cc;
        const char * nsRaw = cls.GetNamespace( );
        cc.typeToken = cls.GetTypeToken( );
        cc.flags = cls.GetFlags( );
        cc.instanceSize = cls.InstanceSize( );
        cc.name = std::move( name );
        cc.ns = nsRaw ? nsRaw : "";
        cc.isInterface = cls.IsInterface( );
        cc.isStruct = cls.IsValueType( ) && !cc.isInterface;

        auto fqName = [ ] ( const Il2CppClass & c ) -> std::string {
            const char * n = c.GetName( );
            if ( !n || !*n )
                return "";
            const char * ns = c.GetNamespace( );
            return ( ns && *ns ) ? std::string( ns ) + "." + n : std::string( n );
            };

        auto parent = cls.GetParent( );
        if ( parent.klass )
            cc.parentName = fqName( parent );

        for ( const auto & iface : cls.GetInterfaces( ) ) {
            std::string fq = fqName( iface );
            if ( !fq.empty( ) )
                cc.interfaces.emplace_back( std::move( fq ) );
        }

        cc.attributes = cls.GetAttributes( );
        cc.fields = cls.GetFields( );
        cc.methods = cls.GetMethods( );
        cc.properties = cls.GetProperties( );
        cc.events = cls.GetEvents( );

        ns_classes [ cc.ns ].emplace_back( std::move( cc ) );
    }

    return ns_classes;
}

// TypeAttributes (ECMA-335 II.23.1.15)
constexpr uint32_t TA_ABSTRACT = 0x00000080;
constexpr uint32_t TA_SEALED = 0x00000100;
constexpr uint32_t TA_INTERFACE = 0x00000020;
constexpr uint32_t TA_VISIBILITY_MASK = 0x00000007;
// MethodAttributes (ECMA-335 II.23.1.10)
constexpr uint32_t MA_ABSTRACT = 0x0400;
constexpr uint32_t MA_PINVOKEIMPL = 0x2000;
// MethodImplAttributes (ECMA-335 II.23.1.11)
constexpr uint32_t MIA_CODETYPE_MASK = 0x0003;
constexpr uint32_t MIA_NATIVE = 0x0001;
constexpr uint32_t MIA_RUNTIME = 0x0003;
constexpr uint32_t MIA_INTERNALCALL = 0x1000;
// FieldAttributes (ECMA-335 II.23.1.5)
constexpr uint32_t FA_STATIC = 0x0010;
constexpr uint32_t FA_INITONLY = 0x0020;
constexpr uint32_t FA_LITERAL = 0x0040;
constexpr uint32_t FA_HASDEFAULT = 0x8000;

static std::string MethodNoCodeReason( const MethodData & m ) {
    if ( m.flags & MA_ABSTRACT )
        return "abstract";
    if ( m.flags & MA_PINVOKEIMPL )
        return "extern (P/Invoke)";
    if ( m.iflags & MIA_INTERNALCALL )
        return "extern (icall)";
    if ( ( m.iflags & MIA_CODETYPE_MASK ) == MIA_NATIVE )
        return "native";
    if ( ( m.iflags & MIA_CODETYPE_MASK ) == MIA_RUNTIME )
        return "runtime";
    if ( m.isGeneric )
        return "generic def";
    return "not resolved";
}

static std::string FormatFieldOffset( const FieldData & f );
static bool ReadLiteralInt( const FieldData & f, int64_t & out );

// abstract+sealed = static class. Interfaces are abstract by definition.
static std::string ClassTypeModifiers( const CachedClass & cls ) {
    if ( cls.isInterface )
        return ""; // interfaces are inherently abstract
    bool isAbstract = ( cls.flags & TA_ABSTRACT ) != 0;
    bool isSealed = ( cls.flags & TA_SEALED ) != 0;
    if ( isAbstract && isSealed )
        return "static ";
    if ( isAbstract )
        return "abstract ";
    if ( isSealed )
        return "sealed ";
    return "";
}

static std::unordered_set<std::string>
ConsumedMethodNames( const CachedClass & cls ) {
    std::unordered_set<std::string> s;
    for ( const auto & p : cls.properties ) {
        if ( !p.getterName.empty( ) )
            s.insert( p.getterName );
        if ( !p.setterName.empty( ) )
            s.insert( p.setterName );
    }
    for ( const auto & e : cls.events ) {
        if ( !e.addName.empty( ) )
            s.insert( e.addName );
        if ( !e.removeName.empty( ) )
            s.insert( e.removeName );
        if ( !e.raiseName.empty( ) )
            s.insert( e.raiseName );
    }
    return s;
}

// O(1) lookup for property/event accessors.
using MethodIndex = std::unordered_map<std::string, const MethodData *>;

static MethodIndex BuildMethodIndex( const CachedClass & cls ) {
    MethodIndex idx;
    idx.reserve( cls.methods.size( ) * 2 );
    for ( const auto & m : cls.methods )
        idx.emplace( m.name, &m );
    return idx;
}

static const MethodData * FindMethod( const MethodIndex & idx,
    const std::string & n ) {
    if ( n.empty( ) )
        return nullptr;
    auto it = idx.find( n );
    return it != idx.end( ) ? it->second : nullptr;
}

static std::string FormatRva( const MethodData & m ) {
    std::ostringstream os;
    if ( m.rva ) {
        os << "0x" << std::hex << std::uppercase << std::setw( 8 )
            << std::setfill( '0' ) << m.rva;
    }
    else {
        os << "-1  // " << MethodNoCodeReason( m );
    }
    return os.str( );
}

void Dumper::DumpAssemblyNormal(
    const std::string & asmName, std::size_t classCount,
    const std::map<std::string, std::vector<CachedClass>> & nsClasses ) {
    std::string filename = GetDumpDirNormal( ) + asmName + ".cs";
    std::ofstream out( filename, std::ios::out );
    if ( !out.is_open( ) ) {
        Log( "Failed to open: " + filename );
        return;
    }

    out << "// ========================================================\n";
    out << "// Dumped by @desirepro\n";
    out << "// Assembly: " << asmName << "\n";
    out << "// Classes:  " << classCount << "\n";
    out << "// Module:   GameAssembly.dll  base=0x" << std::hex << std::uppercase
        << api::module_base << std::dec << "\n";
    out << "// Date:     " << __DATE__ << " " << __TIME__ << "\n";
    out << "// ========================================================\n\n";
    out << "using System;\n";
    out << "using System.Collections.Generic;\n\n";

    for ( const auto & [ns, classes] : nsClasses ) {
        if ( !ns.empty( ) ) {
            out << "namespace " << ns << "\n{\n\n";
        }

        for ( const auto & cls : classes ) {
            std::string type =
                cls.isInterface ? "interface" : ( cls.isStruct ? "struct" : "class" );
            MethodIndex methodIdx = BuildMethodIndex( cls );

            for ( const auto & a : cls.attributes ) {
                out << "    [" << a << "]\n";
            }
            out << "    // TypeToken: 0x" << std::hex << std::uppercase
                << cls.typeToken << std::dec;
            if ( cls.instanceSize > 0 )
                out << "  // size: 0x" << std::hex << cls.instanceSize;
            out << std::dec << "\n";
            out << "    public " << ClassTypeModifiers( cls ) << type << " "
                << cls.name;

            bool wroteColon = false;
            if ( !cls.parentName.empty( ) && cls.parentName != "System.Object" &&
                cls.parentName != "System.ValueType" &&
                cls.parentName != "System.Enum" ) {
                out << " : " << cls.parentName;
                wroteColon = true;
            }
            for ( size_t j = 0; j < cls.interfaces.size( ); ++j ) {
                out << ( ( j == 0 && !wroteColon ) ? " : " : ", " ) << cls.interfaces [ j ];
                wroteColon = true;
            }
            out << "\n    {\n";

            if ( !cls.fields.empty( ) ) {
                out << "        // Fields\n";
                for ( const auto & f : cls.fields ) {
                    for ( const auto & a : f.attributes ) {
                        out << "        [" << a << "]\n";
                    }
                    std::string acc = GetAccessModifier( f.flags );
                    std::string mods = ( f.flags & 0x0010 ) ? "static " : "";
                    if ( f.flags & 0x0020 )
                        mods += "readonly ";

                    out << "        " << acc << " " << mods << f.type << " " << f.name
                        << ";"
                        << "  // " << FormatFieldOffset( f ) << "\n";
                }
                out << "\n";
            }

            if ( !cls.properties.empty( ) ) {
                out << "        // Properties\n";
                for ( const auto & p : cls.properties ) {
                    const MethodData * gm = FindMethod( methodIdx, p.getterName );
                    const MethodData * sm = FindMethod( methodIdx, p.setterName );
                    out << "        "
                        << ( gm ? gm->returnType
                            : ( sm && !sm->params.empty( ) ? sm->params [ 0 ].type
                                : std::string( "object" ) ) )
                        << " " << p.name << " { ";
                    if ( gm )
                        out << "get; /* RVA: " << FormatRva( *gm ) << " */ ";
                    if ( sm )
                        out << "set; /* RVA: " << FormatRva( *sm ) << " */ ";
                    out << "}\n";
                }
                out << "\n";
            }

            if ( !cls.events.empty( ) ) {
                out << "        // Events\n";
                for ( const auto & e : cls.events ) {
                    const MethodData * am = FindMethod( methodIdx, e.addName );
                    out << "        event "
                        << ( am && !am->params.empty( ) ? am->params [ 0 ].type
                            : std::string( "object" ) )
                        << " " << e.name << ";\n";
                }
                out << "\n";
            }

            if ( !cls.methods.empty( ) ) {
                auto consumed = ConsumedMethodNames( cls );
                bool wroteHeader = false;
                for ( const auto & m : cls.methods ) {
                    if ( consumed.count( m.name ) )
                        continue; // already shown via property/event

                    if ( !wroteHeader ) {
                        out << "        // Methods\n";
                        wroteHeader = true;
                    }

                    std::string acc = GetAccessModifier( m.flags );
                    std::string mods = ( m.flags & 0x0010 ) ? "static " : "";
                    if ( m.flags & 0x0040 )
                        mods += "virtual ";

                    out << "        // RVA: " << FormatRva( m );
                    if ( m.token )
                        out << "  token: 0x" << std::hex << std::uppercase << m.token
                        << std::dec;
                    out << "\n";
                    for ( const auto & a : m.attributes ) {
                        out << "        [" << a << "]\n";
                    }
                    out << "        " << acc << " " << mods << m.returnType << " "
                        << m.name << "(";

                    for ( size_t j = 0; j < m.params.size( ); ++j ) {
                        if ( j > 0 )
                            out << ", ";
                        out << m.params [ j ].type << " " << m.params [ j ].name;
                    }
                    out << ") { }\n";
                }
                if ( wroteHeader )
                    out << "\n";
            }

            out << "    }\n\n";
        }

        if ( !ns.empty( ) ) {
            out << "}\n\n";
        }
    }

    out.close( );
    Log( "Saved -> " + filename + "  [C# mode]" );
}

void Dumper::DumpAssemblyAi(
    const std::string & asmName, std::size_t classCount,
    const std::map<std::string, std::vector<CachedClass>> & nsClasses ) {
    std::string filename = GetDumpDirAi( ) + asmName + ".cs";
    std::ofstream out( filename, std::ios::out );
    if ( !out.is_open( ) ) {
        Log( "Failed to open: " + filename );
        return;
    }

    out << "// ========================================================\n";
    out << "// Dumped by @desirepro\n";
    out << "// Assembly: " << asmName << "\n";
    out << "// Classes:  " << classCount << "\n";
    out << "// Module:   GameAssembly.dll  base=0x" << std::hex << std::uppercase
        << api::module_base << std::dec << "\n";
    out << "// Date:     " << __DATE__ << " " << __TIME__ << "\n";
    out << "// ========================================================\n\n";
    out << "# AI-FRIENDLY STRUCTURED DUMP\n";
    out << "# Optimized for LLM parsing / code generation\n\n";

    for ( const auto & [ns, classes] : nsClasses ) {
        for ( const auto & cls : classes ) {
            std::string type =
                cls.isInterface ? "interface" : ( cls.isStruct ? "struct" : "class" );
            MethodIndex methodIdx = BuildMethodIndex( cls );

            out << "CLASS: " << ( ns.empty( ) ? "" : ns + "." ) << cls.name << "\n";
            out << "TYPE:  " << ClassTypeModifiers( cls ) << type << "\n";
            out << "TOKEN: 0x" << std::hex << std::uppercase << cls.typeToken
                << std::dec << "\n";
            if ( cls.instanceSize > 0 )
                out << "SIZE:  0x" << std::hex << cls.instanceSize << std::dec << "\n";

            if ( !cls.attributes.empty( ) ) {
                out << "ATTRS:";
                for ( const auto & a : cls.attributes )
                    out << " [" << a << "]";
                out << "\n";
            }
            if ( !cls.parentName.empty( ) && cls.parentName != "System.Object" &&
                cls.parentName != "System.ValueType" &&
                cls.parentName != "System.Enum" ) {
                out << "EXTENDS: " << cls.parentName << "\n";
            }
            if ( !cls.interfaces.empty( ) ) {
                out << "IMPLEMENTS:";
                for ( const auto & i : cls.interfaces )
                    out << " " << i;
                out << "\n";
            }

            out << "FIELDS:\n";
            for ( const auto & f : cls.fields ) {
                std::string acc = GetAccessModifier( f.flags );
                std::string mods = ( f.flags & 0x0010 ) ? "static " : "";
                if ( f.flags & 0x0020 )
                    mods += "readonly ";

                out << "  " << std::left << std::setw( 10 ) << acc << std::setw( 8 ) << mods
                    << std::setw( 32 ) << f.type << f.name << "  // "
                    << FormatFieldOffset( f );
                if ( !f.attributes.empty( ) ) {
                    out << "  attrs=";
                    for ( size_t k = 0; k < f.attributes.size( ); ++k ) {
                        if ( k > 0 )
                            out << ",";
                        out << "[" << f.attributes [ k ] << "]";
                    }
                }
                out << "\n";
            }

            if ( !cls.properties.empty( ) ) {
                out << "PROPERTIES:\n";
                for ( const auto & p : cls.properties ) {
                    const MethodData * gm = FindMethod( methodIdx, p.getterName );
                    const MethodData * sm = FindMethod( methodIdx, p.setterName );
                    out << "  " << p.name;
                    if ( gm )
                        out << "  get=" << FormatRva( *gm );
                    if ( sm )
                        out << "  set=" << FormatRva( *sm );
                    out << "\n";
                }
            }

            if ( !cls.events.empty( ) ) {
                out << "EVENTS:\n";
                for ( const auto & e : cls.events ) {
                    out << "  " << e.name;
                    if ( !e.addName.empty( ) )
                        out << "  add=" << e.addName;
                    if ( !e.removeName.empty( ) )
                        out << "  remove=" << e.removeName;
                    if ( !e.raiseName.empty( ) )
                        out << "  raise=" << e.raiseName;
                    out << "\n";
                }
            }

            auto consumed = ConsumedMethodNames( cls );
            out << "METHODS:\n";
            for ( const auto & m : cls.methods ) {
                if ( consumed.count( m.name ) )
                    continue;
                out << "  RVA=" << FormatRva( m );
                if ( m.token )
                    out << "  token=0x" << std::hex << std::uppercase << m.token
                    << std::dec;
                out << "  " << m.returnType << " " << m.name << "(";
                for ( size_t j = 0; j < m.params.size( ); ++j ) {
                    if ( j > 0 )
                        out << ", ";
                    out << m.params [ j ].type << " " << m.params [ j ].name;
                }
                out << ")";
                if ( !m.attributes.empty( ) ) {
                    out << "  attrs=";
                    for ( size_t k = 0; k < m.attributes.size( ); ++k ) {
                        if ( k > 0 )
                            out << ",";
                        out << "[" << m.attributes [ k ] << "]";
                    }
                }
                out << "\n";
            }

            out << "END_CLASS\n\n";
        }
    }

    out.close( );
    Log( "Saved -> " + filename + "  [AI mode]" );
}

static bool ReadLiteralInt( const FieldData & f, int64_t & out ) {
    if ( !( f.flags & FA_LITERAL ) )
        return false;
    if ( !api::field_get_default_value || !f.raw )
        return false;

    void * data = nullptr;
    __try {
        api::field_get_default_value( f.raw, &data );
    }
    __except ( EXCEPTION_EXECUTE_HANDLER ) {
        return false;
    }
    if ( !data )
        return false;

    __try {
        if ( f.type == "System.Int32" || f.type == "int" )
            out = *reinterpret_cast< int32_t * >( data );
        else if ( f.type == "System.UInt32" || f.type == "uint" )
            out = *reinterpret_cast< uint32_t * >( data );
        else if ( f.type == "System.Int64" || f.type == "long" )
            out = *reinterpret_cast< int64_t * >( data );
        else if ( f.type == "System.UInt64" || f.type == "ulong" )
            out = static_cast< int64_t >( *reinterpret_cast< uint64_t * >( data ) );
        else if ( f.type == "System.Int16" || f.type == "short" )
            out = *reinterpret_cast< int16_t * >( data );
        else if ( f.type == "System.UInt16" || f.type == "ushort" )
            out = *reinterpret_cast< uint16_t * >( data );
        else if ( f.type == "System.SByte" || f.type == "sbyte" )
            out = *reinterpret_cast< int8_t * >( data );
        else if ( f.type == "System.Byte" || f.type == "byte" )
            out = *reinterpret_cast< uint8_t * >( data );
        else if ( f.type == "System.Boolean" || f.type == "bool" )
            out = *reinterpret_cast< uint8_t * >( data );
        else
            return false;
    }
    __except ( EXCEPTION_EXECUTE_HANDLER ) {
        return false;
    }

    return true;
}

static std::string FormatFieldOffset( const FieldData & f ) {
    std::ostringstream os;
    if ( f.flags & FA_LITERAL ) {
        int64_t v = 0;
        if ( ReadLiteralInt( f, v ) ) {
            os << "const = " << v;
        }
        else {
            os << "const";
        }
    }
    else if ( f.flags & FA_STATIC ) {
        os << "static @ 0x" << std::hex << f.offset << std::dec;
    }
    else {
        os << "0x" << std::hex << f.offset << std::dec;
    }
    if ( f.token )
        os << "  token: 0x" << std::hex << std::uppercase << f.token << std::dec;
    return os.str( );
}

// Il2CppString: [obj header 16][int32 len @ 16][char16 chars[] @ 20]
static bool ReadIl2CppString( void * strPtr, std::string & out ) {
    if ( !strPtr )
        return false;
    __try {
        int32_t len =
            *reinterpret_cast< int32_t * >( reinterpret_cast< char * >( strPtr ) + 16 );
        if ( len < 0 || len > 65536 )
            return false;
        wchar_t * chars =
            reinterpret_cast< wchar_t * >( reinterpret_cast< char * >( strPtr ) + 20 );

        int sz = WideCharToMultiByte( CP_UTF8, 0, chars, len, nullptr, 0, nullptr,
            nullptr );
        if ( sz <= 0 || sz > 1 << 20 )
            return false;
        out.assign( static_cast< size_t >( sz ), '\0' );
        WideCharToMultiByte( CP_UTF8, 0, chars, len, &out [ 0 ], sz, nullptr, nullptr );
    }
    __except ( EXCEPTION_EXECUTE_HANDLER ) {
        return false;
    }
    return true;
}

static std::string EscapeStr( const std::string & s ) {
    std::string r;
    r.reserve( s.size( ) + 4 );
    for ( char c : s ) {
        switch ( c ) {
        case '\\':
            r += "\\\\";
            break;
        case '"':
            r += "\\\"";
            break;
        case '\n':
            r += "\\n";
            break;
        case '\r':
            r += "\\r";
            break;
        case '\t':
            r += "\\t";
            break;
        default:
            if ( static_cast< unsigned char >( c ) < 0x20 ) {
                char buf [ 8 ];
                sprintf_s( buf, "\\x%02X", static_cast< unsigned char >( c ) );
                r += buf;
            }
            else {
                r += c;
            }
        }
    }
    return r;
}

void Dumper::DumpStringLiterals(
    const std::string & asmName,
    const std::map<std::string, std::vector<CachedClass>> & nsClasses,
    std::ofstream & out, std::size_t & counter ) {
    if ( !api::field_static_get_value )
        return;

    constexpr uint32_t FA_STATIC = 0x0010;
    constexpr uint32_t FA_LITERAL = 0x0040;

    bool wroteHeader = false;

    for ( const auto & [ns, classes] : nsClasses ) {
        for ( const auto & cls : classes ) {
            for ( const auto & f : cls.fields ) {
                if ( ( f.flags & ( FA_STATIC | FA_LITERAL ) ) != ( FA_STATIC | FA_LITERAL ) )
                    continue;
                if ( f.type != "System.String" && f.type != "string" )
                    continue;
                if ( !f.raw )
                    continue;

                void * value = nullptr;
                __try {
                    api::field_static_get_value( f.raw, &value );
                }
                __except ( EXCEPTION_EXECUTE_HANDLER ) {
                    value = nullptr;
                }
                if ( !value )
                    continue;

                std::string utf8;
                if ( !ReadIl2CppString( value, utf8 ) )
                    continue;

                if ( !wroteHeader ) {
                    out << "\n# === " << asmName << " ===\n";
                    wroteHeader = true;
                }
                std::string fq = ns.empty( ) ? cls.name : ns + "." + cls.name;
                out << fq << "::" << f.name << " = \"" << EscapeStr( utf8 ) << "\"\n";
                ++counter;
            }
        }
    }
}

static bool DirHasDumpFiles( const std::string & dir ) {
    WIN32_FIND_DATAA fd;
    HANDLE h = FindFirstFileA( ( dir + "*.cs" ).c_str( ), &fd );
    if ( h == INVALID_HANDLE_VALUE )
        return false;
    FindClose( h );
    return true;
}

void Dumper::DumpAllToFiles( ) {
    std::string dirNormal = GetDumpDirNormal( );
    std::string dirAi = GetDumpDirAi( );

    if ( DirHasDumpFiles( dirNormal ) && DirHasDumpFiles( dirAi ) ) {
        Log( "Static dump already exists, skipping. Delete folders to re-dump." );
        Log( "  " + dirNormal );
        Log( "  " + dirAi );
        Log( "" );
        return;
    }

    EnsureDirectory( dirNormal );
    EnsureDirectory( dirAi );

    // one shared file for all literals across assemblies
    std::string stringsPath =
        ( g_outputDir.empty( ) ? std::string( "C:\\" ) : g_outputDir + "\\" ) +
        "IL2CPP_Dump_Strings.txt";
    std::ofstream stringsOut( stringsPath, std::ios::out | std::ios::trunc );
    stringsOut << "# Static const string literals\n";
    stringsOut << "# format: <Namespace.Class>::<field> = \"<utf8 value>\"\n";
    std::size_t stringsCount = 0;

    for ( const auto & img : images ) {
        std::string asmName = img.GetName( );
        Log( "Dumping assembly: " + asmName + " ..." );

        auto nsClasses = CacheImage( img );

        size_t classCount = 0;
        for ( const auto & [_, v] : nsClasses )
            classCount += v.size( );

        DumpAssemblyNormal( asmName, classCount, nsClasses );
        DumpAssemblyAi( asmName, classCount, nsClasses );
        DumpStringLiterals( asmName, nsClasses, stringsOut, stringsCount );
    }

    stringsOut.close( );

    Log( "" );
    Log( "Done!" );
    Log( "  Normal (C#):     " + dirNormal );
    Log( "  AI-friendly:     " + dirAi );
    Log( "  String literals: " + stringsPath + "  (" +
        std::to_string( stringsCount ) + " entries)" );
    Log( "  RVA resolved:    " + std::to_string( g_rvaResolved ) + " / " +
        std::to_string( g_rvaTotal ) + " methods" );
    Log( "" );
}
