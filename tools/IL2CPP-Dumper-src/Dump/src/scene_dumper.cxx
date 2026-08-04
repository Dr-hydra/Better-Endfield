#define _CRT_SECURE_NO_WARNINGS

#include "../include/scene_dumper.hxx"
#include "../include/il2cpp_api.hxx"
#include "../include/utils.hxx"

#include <cstdio>
#include <cstring>
#include <fstream>
#include <sstream>
#include <string>
#include <unordered_set>
#include <vector>
#include <windows.h>

namespace {

    bool g_ready = false;
    bool g_deepFieldDump = false;
    FILE * g_tracker = nullptr;

    enum class HwTier { LOW, MID, HIGH };
    HwTier g_hwTier = HwTier::MID;
    int g_yieldEveryN = 100;
    int g_yieldMs = 1;
    int g_maxGOs = 0; // 0 = no cap

    static void DetectHardwareTier( ) {
        SYSTEM_INFO si = { 0 };
        GetSystemInfo( &si );
        DWORD cores = si.dwNumberOfProcessors;

        MEMORYSTATUSEX ms = { 0 };
        ms.dwLength = sizeof( ms );
        GlobalMemoryStatusEx( &ms );
        DWORDLONG ramGb = ms.ullTotalPhys / ( 1024ULL * 1024ULL * 1024ULL );

        if ( cores <= 4 || ramGb <= 8 ) {
            g_hwTier = HwTier::LOW;
            g_yieldEveryN = 50;
            g_yieldMs = 2;
            g_maxGOs = 30000;
        }
        else if ( cores <= 8 || ramGb <= 16 ) {
            g_hwTier = HwTier::MID;
            g_yieldEveryN = 100;
            g_yieldMs = 1;
            g_maxGOs = 0;
        }
        else {
            g_hwTier = HwTier::HIGH;
            g_yieldEveryN = 250;
            g_yieldMs = 1;
            g_maxGOs = 0;
        }

        char buf [ 192 ];
        const char * tier = ( g_hwTier == HwTier::LOW ) ? "LOW"
            : ( g_hwTier == HwTier::MID ) ? "MID"
            : "HIGH";
        sprintf_s( buf, "hardware: %s  cores=%lu  ram=%lluGB  yield=%dms/%d", tier,
            cores, ramGb, g_yieldMs, g_yieldEveryN );
        Log( buf );
    }

    static bool MemoryHealthy( ) {
        MEMORYSTATUSEX ms = { 0 };
        ms.dwLength = sizeof( ms );
        if ( !GlobalMemoryStatusEx( &ms ) )
            return true;
        return ms.ullAvailPhys > ( 512ULL * 1024ULL * 1024ULL );
    }

    struct GcSuspendScope {
        int saved;
        GcSuspendScope( ) : saved( -1 ) {
            if ( api::gc_dont_gc_ptr ) {
                __try {
                    saved = *api::gc_dont_gc_ptr;
                    *api::gc_dont_gc_ptr = saved + 1;
                }
                __except ( EXCEPTION_EXECUTE_HANDLER ) {
                    saved = -1;
                }
            }
        }
        ~GcSuspendScope( ) {
            if ( saved >= 0 && api::gc_dont_gc_ptr ) {
                __try {
                    *api::gc_dont_gc_ptr = saved;
                }
                __except ( EXCEPTION_EXECUTE_HANDLER ) {
                }
            }
        }
    };

    // Cached classes (UnityEngine namespace).
    void * clsObject = nullptr;
    void * clsResources = nullptr;
    void * clsGameObject = nullptr;
    void * clsTransform = nullptr;
    void * clsComponent = nullptr;
    void * clsMonoBehaviour = nullptr;
    void * clsRenderer = nullptr;
    void * clsMaterial = nullptr;
    void * clsTexture = nullptr;
    void * clsCamera = nullptr;

    // Cached methods.
    void * mFindObjectsOfTypeAll = nullptr;
    void * mGetName = nullptr;
    void * mGetTransform = nullptr;
    void * mGetParent = nullptr;
    void * mGetPosition = nullptr;
    void * mGetRotation = nullptr;
    void * mGetLossyScale = nullptr;
    void * mGetActive = nullptr;
    void * mGetLayer = nullptr;
    void * mGetTag = nullptr;
    void * mGetComponents = nullptr;
    void * mGetInstanceID = nullptr;
    void * mRendererGetMaterials = nullptr;
    void * mGetMainTexture = nullptr;

    // Camera
    void * mCameraMain = nullptr;
    void * mCameraGetFov = nullptr;
    void * mCameraGetAspect = nullptr;
    void * mCameraGetNear = nullptr;
    void * mCameraGetFar = nullptr;
    void * mTransformGetForward = nullptr;
    void * mTransformGetRight = nullptr;
    void * mTransformGetUp = nullptr;

    void * typeObjGameObject = nullptr;
    void * typeObjComponent = nullptr;

    struct Vector3 {
        float x, y, z;
    };
    struct Quaternion {
        float x, y, z, w;
    };

    static std::string ReadStringObj( void * str ) {
        if ( !str || !api::string_length_fn || !api::string_chars )
            return "";
        int len = 0;
        wchar_t * chars = nullptr;
        __try {
            len = api::string_length_fn( str );
            chars = api::string_chars( str );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return "";
        }
        if ( len <= 0 || !chars )
            return "";

        int sz =
            WideCharToMultiByte( CP_UTF8, 0, chars, len, nullptr, 0, nullptr, nullptr );
        if ( sz <= 0 || sz > 1 << 20 )
            return "";
        std::string out( static_cast< size_t >( sz ), '\0' );
        WideCharToMultiByte( CP_UTF8, 0, chars, len, &out [ 0 ], sz, nullptr, nullptr );
        return out;
    }

    static void * Invoke( void * method, void * obj, void ** params = nullptr ) {
        if ( !method || !api::runtime_invoke )
            return nullptr;
        void * exc = nullptr;
        __try {
            return api::runtime_invoke( method, obj, params, &exc );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return nullptr;
        }
    }

    template <typename T> static bool UnboxAs( void * boxed, T & out ) {
        if ( !boxed || !api::object_unbox )
            return false;
        __try {
            T * p = reinterpret_cast< T * >( api::object_unbox( boxed ) );
            if ( !p )
                return false;
            out = *p;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return false;
        }
        return true;
    }

    static void * FindImageByName( const char * imageName ) {
        if ( !api::get_domain || !api::get_assemblies || !api::assembly_get_image ||
            !api::image_get_name )
            return nullptr;
        void * domain = api::get_domain( );
        if ( !domain )
            return nullptr;
        size_t count = 0;
        void ** assemblies = api::get_assemblies( domain, &count );
        if ( !assemblies )
            return nullptr;
        for ( size_t i = 0; i < count; ++i ) {
            void * ass = assemblies [ i ];
            if ( !ass )
                continue;
            void * img = api::assembly_get_image( ass );
            if ( !img )
                continue;
            const char * n = api::image_get_name( img );
            if ( n && strcmp( n, imageName ) == 0 )
                return img;
        }
        return nullptr;
    }

    static void * ResolveUnityClass( const char * ns, const char * name ) {
        if ( !api::class_from_name )
            return nullptr;
        const char * candidates[ ] = {
            "UnityEngine.CoreModule.dll",       "UnityEngine.dll",
            "UnityEngine.GameObjectModule.dll", "UnityEngine.PhysicsModule.dll",
            "UnityEngine.UIModule.dll",         nullptr };
        for ( int i = 0; candidates [ i ]; ++i ) {
            void * img = FindImageByName( candidates [ i ] );
            if ( !img )
                continue;
            void * k = api::class_from_name( img, ns, name );
            if ( k )
                return k;
        }
        return nullptr;
    }

    static int32_t ArrayLengthSafe( void * arr ) {
        if ( !arr || !api::array_length )
            return 0;
        __try {
            return api::array_length( arr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return 0;
        }
    }

    static void * ArrayElement( void * arr, int32_t i ) {
        if ( !arr )
            return nullptr;
        __try {
            void ** vec = reinterpret_cast< void ** >( reinterpret_cast< char * >( arr ) + 32 );
            return vec [ i ];
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return nullptr;
        }
    }

    static std::string ObjectClassFq( void * obj ) {
        if ( !obj )
            return "";
        __try {
            void * k = *reinterpret_cast< void ** >( obj );
            if ( !k )
                return "";
            const char * n = api::class_get_name ? api::class_get_name( k ) : nullptr;
            const char * ns =
                api::class_get_namespace ? api::class_get_namespace( k ) : nullptr;
            if ( !n )
                return "";
            return ( ns && *ns ) ? std::string( ns ) + "." + n : std::string( n );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return "";
        }
    }

    static bool IsSubclassOf( void * klass, void * target ) {
        if ( !klass || !target )
            return false;
        __try {
            void * k = klass;
            for ( int i = 0; i < 32 && k; ++i ) {
                if ( k == target )
                    return true;
                k = api::class_get_parent ? api::class_get_parent( k ) : nullptr;
            }
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
        }
        return false;
    }

    static std::string EscapeStr( const std::string & s ) {
        std::string r;
        r.reserve( s.size( ) + 2 );
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
                    char b [ 8 ];
                    sprintf_s( b, "\\x%02X", static_cast< unsigned char >( c ) );
                    r += b;
                }
                else
                    r += c;
            }
        }
        return r;
    }

    static int32_t GetInstanceId( void * obj ) {
        if ( !obj || !mGetInstanceID )
            return 0;
        void * boxed = Invoke( mGetInstanceID, obj );
        int32_t v = 0;
        UnboxAs( boxed, v );
        return v;
    }

    static std::string GetName( void * obj ) {
        if ( !obj || !mGetName )
            return "";
        void * s = Invoke( mGetName, obj );
        return ReadStringObj( s );
    }

    static void DumpRendererMaterials( std::ofstream & out, void * comp ) {
        if ( !mRendererGetMaterials || !mGetName )
            return;
        void * matsArr = Invoke( mRendererGetMaterials, comp );
        if ( !matsArr )
            return;

        int32_t mc = ArrayLengthSafe( matsArr );
        for ( int32_t j = 0; j < mc; ++j ) {
            void * mat = ArrayElement( matsArr, j );
            if ( !mat )
                continue;

            std::string matName = GetName( mat );
            out << "    material[" << j << "] name=\"" << EscapeStr( matName ) << "\"";

            if ( mGetMainTexture ) {
                void * tex = Invoke( mGetMainTexture, mat );
                if ( tex ) {
                    std::string texName = GetName( tex );
                    out << " main_tex=\"" << EscapeStr( texName ) << "\"";
                }
            }
            out << "\n";
        }
    }

    DWORD g_deepStartMs = 0;
    int g_deepBudgetMs = 8000;
    int g_deepMbsDumped = 0;
    int g_deepMbsBudget = 2000;

    static void ResetDeepBudget( ) {
        g_deepStartMs = GetTickCount( );
        g_deepMbsDumped = 0;
    }
    static bool DeepBudgetOk( ) {
        if ( g_deepMbsDumped >= g_deepMbsBudget )
            return false;
        if ( ( int ) ( GetTickCount( ) - g_deepStartMs ) >= g_deepBudgetMs )
            return false;
        return true;
    }

    static bool TypeIsRiskyForRead( const std::string & t ) {
        if ( t.find( '<' ) != std::string::npos )
            return true; // generic instance
        if ( t.find( '`' ) != std::string::npos )
            return true; // generic definition
        if ( t == "System.IntPtr" || t == "IntPtr" )
            return true;
        if ( t == "System.UIntPtr" || t == "UIntPtr" )
            return true;
        if ( t.find( "System.Span" ) != std::string::npos )
            return true;
        if ( t.find( "System.ReadOnlySpan" ) != std::string::npos )
            return true;
        return false;
    }

    static bool IsFrameworkClass( const std::string & fq ) {
        if ( fq.rfind( "UnityEngine.", 0 ) == 0 )
            return true;
        if ( fq.rfind( "System.", 0 ) == 0 )
            return true;
        if ( fq.rfind( "TMPro.", 0 ) == 0 )
            return true;
        if ( fq.rfind( "Cinemachine.", 0 ) == 0 )
            return true;
        if ( fq.rfind( "DOTween", 0 ) == 0 )
            return true;
        if ( fq.rfind( "ParadoxNotion.", 0 ) == 0 )
            return true;
        if ( fq.rfind( "Sirenix.", 0 ) == 0 )
            return true;
        if ( fq.rfind( "MagicaCloth.", 0 ) == 0 )
            return true;
        if ( fq.rfind( "RootMotion.", 0 ) == 0 )
            return true;
        if ( fq.rfind( "Slate.", 0 ) == 0 )
            return true;
        if ( fq.rfind( "Rewired", 0 ) == 0 )
            return true;
        if ( fq.rfind( "XLua.", 0 ) == 0 )
            return true;
        if ( fq.rfind( "Beyond.UI.", 0 ) == 0 )
            return true; // packed bool fields crash
        return false;
    }

    static void DumpMonoBehaviourFields( std::ofstream & out, void * comp,
        void * compKlass ) {
        if ( !api::class_get_fields || !api::field_get_value )
            return;
        if ( !comp || !compKlass )
            return;
        if ( !DeepBudgetOk( ) )
            return;

        std::string compFq = ObjectClassFq( comp );
        if ( IsFrameworkClass( compFq ) )
            return;

        if ( g_tracker ) {
            fseek( g_tracker, 0, SEEK_SET );
            fprintf( g_tracker, "deep MB=%s  comp=%p                              \n",
                compFq.c_str( ), comp );
            fflush( g_tracker );
        }

        ++g_deepMbsDumped;

        constexpr int kMaxDepth = 3;
        constexpr int kMaxFieldsPerCls = 64;

        int depth = 0;
        for ( void * k = compKlass;
            k && k != clsMonoBehaviour && k != clsObject && depth < kMaxDepth;
            ++depth ) {

            void * iter = nullptr;
            int fieldsSeen = 0;

            // SEH around the iter to swallow torn metadata.
            __try {
                while ( void * f = api::class_get_fields( k, &iter ) ) {
                    if ( !f )
                        break;
                    if ( ++fieldsSeen > kMaxFieldsPerCls )
                        break;

                    const char * fname =
                        api::field_get_name ? api::field_get_name( f ) : nullptr;
                    if ( !fname || !*fname )
                        continue;

                    uint32_t flags = api::field_get_flags ? api::field_get_flags( f ) : 0;
                    if ( flags & 0x0050 )
                        continue; // STATIC | LITERAL

                    void * ftype = api::field_get_type ? api::field_get_type( f ) : nullptr;
                    const char * tname =
                        ftype && api::type_get_name ? api::type_get_name( ftype ) : "object";
                    std::string typeName = tname ? tname : "object";

                    if ( TypeIsRiskyForRead( typeName ) ) {
                        out << "    field " << typeName << " " << fname
                            << " = <skipped: risky type>\n";
                        continue;
                    }

                    int32_t off = api::field_get_offset ? api::field_get_offset( f ) : -1;
                    if ( off <= 0 || off > 65536 ) {
                        out << "    field " << typeName << " " << fname
                            << " = <bad offset>\n";
                        continue;
                    }
                    const unsigned char * fp =
                        reinterpret_cast< const unsigned char * >( comp ) + off;

                    __try {
                        out << "    field " << typeName << " " << fname << " = ";

                        if ( typeName == "System.Boolean" || typeName == "bool" ) {
                            out << ( fp [ 0 ] ? "true" : "false" );
                        }
                        else if ( typeName == "System.Byte" || typeName == "byte" ) {
                            out << static_cast< int >( *reinterpret_cast< const uint8_t * >( fp ) );
                        }
                        else if ( typeName == "System.SByte" || typeName == "sbyte" ) {
                            out << static_cast< int >( *reinterpret_cast< const int8_t * >( fp ) );
                        }
                        else if ( typeName == "System.Int16" || typeName == "short" ) {
                            out << *reinterpret_cast< const int16_t * >( fp );
                        }
                        else if ( typeName == "System.UInt16" || typeName == "ushort" ) {
                            out << *reinterpret_cast< const uint16_t * >( fp );
                        }
                        else if ( typeName == "System.Int32" || typeName == "int" ) {
                            out << *reinterpret_cast< const int32_t * >( fp );
                        }
                        else if ( typeName == "System.UInt32" || typeName == "uint" ) {
                            out << *reinterpret_cast< const uint32_t * >( fp );
                        }
                        else if ( typeName == "System.Int64" || typeName == "long" ) {
                            out << *reinterpret_cast< const int64_t * >( fp );
                        }
                        else if ( typeName == "System.UInt64" || typeName == "ulong" ) {
                            out << *reinterpret_cast< const uint64_t * >( fp );
                        }
                        else if ( typeName == "System.Single" || typeName == "float" ) {
                            char nb [ 32 ];
                            sprintf_s( nb, "%.4f", *reinterpret_cast< const float * >( fp ) );
                            out << nb;
                        }
                        else if ( typeName == "System.Double" || typeName == "double" ) {
                            char nb [ 32 ];
                            sprintf_s( nb, "%.4f", *reinterpret_cast< const double * >( fp ) );
                            out << nb;
                        }
                        else if ( typeName == "UnityEngine.Vector3" ) {
                            const Vector3 * v = reinterpret_cast< const Vector3 * >( fp );
                            char nb [ 64 ];
                            sprintf_s( nb, "(%.3f, %.3f, %.3f)", v->x, v->y, v->z );
                            out << nb;
                        }
                        else if ( typeName == "UnityEngine.Vector2" ) {
                            const float * v = reinterpret_cast< const float * >( fp );
                            char nb [ 48 ];
                            sprintf_s( nb, "(%.3f, %.3f)", v [ 0 ], v [ 1 ] );
                            out << nb;
                        }
                        else if ( typeName == "UnityEngine.Quaternion" ) {
                            const Quaternion * q = reinterpret_cast< const Quaternion * >( fp );
                            char nb [ 80 ];
                            sprintf_s( nb, "(%.3f, %.3f, %.3f, %.3f)", q->x, q->y, q->z, q->w );
                            out << nb;
                        }
                        else {
                            void * raw = *reinterpret_cast< void * const * >( fp );
                            if ( !raw ) {
                                out << "null";
                            }
                            else {
                                char ab [ 48 ];
                                sprintf_s( ab, "<obj 0x%llX>",
                                    reinterpret_cast< unsigned long long >( raw ) );
                                out << ab;
                            }
                        }
                        out << "\n";
                    }
                    __except ( EXCEPTION_EXECUTE_HANDLER ) {
                        out << "<read fault>\n";
                    }
                }
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
            }

            void * parentK = nullptr;
            __try {
                parentK = api::class_get_parent ? api::class_get_parent( k ) : nullptr;
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
                parentK = nullptr;
            }
            k = parentK;
        }
    }

    static void DumpComponents( std::ofstream & out, void * go ) {
        if ( !mGetComponents || !typeObjComponent )
            return;
        void * args[ ] = { typeObjComponent };
        void * arr = Invoke( mGetComponents, go, args );
        if ( !arr )
            return;

        int32_t cnt = ArrayLengthSafe( arr );
        for ( int32_t i = 0; i < cnt; ++i ) {
            void * comp = ArrayElement( arr, i );
            if ( !comp )
                continue;

            std::string fq = ObjectClassFq( comp );
            if ( fq.empty( ) )
                continue;
            out << "  comp \"" << fq << "\"\n";

            void * compKlass = nullptr;
            __try {
                compKlass = *reinterpret_cast< void ** >( comp );
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
                compKlass = nullptr;
            }

            if ( clsRenderer && IsSubclassOf( compKlass, clsRenderer ) ) {
                DumpRendererMaterials( out, comp );
            }

            if ( g_deepFieldDump && clsMonoBehaviour &&
                IsSubclassOf( compKlass, clsMonoBehaviour ) ) {
                DumpMonoBehaviourFields( out, comp, compKlass ); // tracker logs from inside
            }
        }
    }

} // namespace

bool SceneDumper::Initialize( ) {
    if ( g_ready )
        return true;

    if ( !api::class_get_method_from_name || !api::runtime_invoke ||
        !api::class_get_type || !api::type_get_object || !api::array_length ||
        !api::object_unbox ) {
        Log( "scene dumper: missing runtime API, disabled" );
        return false;
    }

    clsObject = ResolveUnityClass( "UnityEngine", "Object" );
    clsResources = ResolveUnityClass( "UnityEngine", "Resources" );
    clsGameObject = ResolveUnityClass( "UnityEngine", "GameObject" );
    clsTransform = ResolveUnityClass( "UnityEngine", "Transform" );
    clsComponent = ResolveUnityClass( "UnityEngine", "Component" );
    clsMonoBehaviour = ResolveUnityClass( "UnityEngine", "MonoBehaviour" );
    clsRenderer = ResolveUnityClass( "UnityEngine", "Renderer" );
    clsMaterial = ResolveUnityClass( "UnityEngine", "Material" );
    clsTexture = ResolveUnityClass( "UnityEngine", "Texture" );

    if ( !clsGameObject || !clsTransform || !clsResources || !clsObject ||
        !clsComponent ) {
        Log( "scene dumper: GameObject/Transform/Resources not in image" );
        return false;
    }

    mFindObjectsOfTypeAll =
        api::class_get_method_from_name( clsResources, "FindObjectsOfTypeAll", 1 );
    mGetName = api::class_get_method_from_name( clsObject, "get_name", 0 );
    mGetInstanceID =
        api::class_get_method_from_name( clsObject, "GetInstanceID", 0 );
    mGetTransform =
        api::class_get_method_from_name( clsGameObject, "get_transform", 0 );
    mGetActive =
        api::class_get_method_from_name( clsGameObject, "get_activeSelf", 0 );
    mGetLayer = api::class_get_method_from_name( clsGameObject, "get_layer", 0 );
    mGetTag = api::class_get_method_from_name( clsGameObject, "get_tag", 0 );
    mGetComponents =
        api::class_get_method_from_name( clsGameObject, "GetComponents", 1 );
    mGetParent = api::class_get_method_from_name( clsTransform, "get_parent", 0 );
    mGetPosition =
        api::class_get_method_from_name( clsTransform, "get_position", 0 );
    mGetRotation =
        api::class_get_method_from_name( clsTransform, "get_rotation", 0 );
    mGetLossyScale =
        api::class_get_method_from_name( clsTransform, "get_lossyScale", 0 );

    if ( clsRenderer ) {
        mRendererGetMaterials =
            api::class_get_method_from_name( clsRenderer, "get_sharedMaterials", 0 );
        if ( !mRendererGetMaterials )
            mRendererGetMaterials =
            api::class_get_method_from_name( clsRenderer, "get_materials", 0 );
    }
    if ( clsMaterial ) {
        mGetMainTexture =
            api::class_get_method_from_name( clsMaterial, "get_mainTexture", 0 );
    }

    // Camera + transform direction accessors
    clsCamera = ResolveUnityClass( "UnityEngine", "Camera" );
    if ( clsCamera ) {
        mCameraMain = api::class_get_method_from_name( clsCamera, "get_main", 0 );
        mCameraGetFov =
            api::class_get_method_from_name( clsCamera, "get_fieldOfView", 0 );
        mCameraGetAspect =
            api::class_get_method_from_name( clsCamera, "get_aspect", 0 );
        mCameraGetNear =
            api::class_get_method_from_name( clsCamera, "get_nearClipPlane", 0 );
        mCameraGetFar =
            api::class_get_method_from_name( clsCamera, "get_farClipPlane", 0 );
    }
    mTransformGetForward =
        api::class_get_method_from_name( clsTransform, "get_forward", 0 );
    mTransformGetRight =
        api::class_get_method_from_name( clsTransform, "get_right", 0 );
    mTransformGetUp = api::class_get_method_from_name( clsTransform, "get_up", 0 );

    if ( !mFindObjectsOfTypeAll || !mGetTransform || !mGetPosition ||
        !mGetComponents ) {
        Log( "scene dumper: required Unity methods missing" );
        return false;
    }

    void * tGo = api::class_get_type( clsGameObject );
    if ( tGo )
        typeObjGameObject = api::type_get_object( tGo );

    void * tComp = api::class_get_type( clsComponent );
    if ( tComp )
        typeObjComponent = api::type_get_object( tComp );

    if ( !typeObjGameObject || !typeObjComponent ) {
        Log( "scene dumper: System.Type build failed" );
        return false;
    }

    DetectHardwareTier( );

    g_ready = true;
    Log( "scene dumper ready" );
    return true;
}

void SceneDumper::SetDeepFieldDump( bool on ) {
    g_deepFieldDump = on;
    Log( on ? "[scene] deep MonoBehaviour field dump ON"
        : "[scene] deep MonoBehaviour field dump OFF" );
}

bool SceneDumper::Ready( ) { return g_ready; }

void SceneDumper::Dump( ) {
    if ( !g_ready ) {
        Log( "scene dumper not ready" );
        return;
    }

    GcSuspendScope gcGuard;
    ResetDeepBudget( );

    Log( g_deepFieldDump ? "scene snapshot: deep mode (MB fields)"
        : "scene snapshot: light mode" );

    SYSTEMTIME st;
    GetLocalTime( &st );
    char ts [ 64 ];
    sprintf_s( ts, "%04d%02d%02d_%02d%02d%02d", st.wYear, st.wMonth, st.wDay,
        st.wHour, st.wMinute, st.wSecond );

    std::string base =
        g_outputDir.empty( ) ? std::string( "C:\\" ) : g_outputDir + "\\";
    std::string path = base + "IL2CPP_World_Dump_" + ts + ".txt";

    std::ofstream out( path, std::ios::out | std::ios::trunc );
    if ( !out.is_open( ) ) {
        Log( "cannot open output: " + path );
        return;
    }

    out << "# Live IL2CPP world snapshot\n";
    out << "# captured " << ts << "\n";
    out << "# format: line-tagged. tags:  @go  pos  rot  scale  parent_id  comp  "
        "material\n\n";

    void * args[ ] = { typeObjGameObject };
    void * arr = Invoke( mFindObjectsOfTypeAll, nullptr, args );
    if ( !arr ) {
        Log( "FindObjectsOfTypeAll returned null" );
        out.close( );
        return;
    }

    int32_t cnt = ArrayLengthSafe( arr );
    Log( std::to_string( cnt ) + " GameObjects" );

    int32_t walkLimit = cnt;
    if ( g_maxGOs > 0 && walkLimit > g_maxGOs ) {
        walkLimit = g_maxGOs;
        Log( "LOW-tier cap, walking first " + std::to_string( walkLimit ) );
    }

    out << "# total game objects: " << cnt << "  walked: " << walkLimit << "\n\n";

    DWORD startMs = GetTickCount( );
    int32_t dumped = 0, skipped = 0;
    int reportEvery = g_deepFieldDump ? 50 : ( walkLimit > 5000 ? 1000 : 250 );

    std::string trackerPath = base + "IL2CPP_DumpTracker.txt";
    fopen_s( &g_tracker, trackerPath.c_str( ), "w" );
    if ( g_tracker ) {
        fprintf( g_tracker, "started\n" );
        fflush( g_tracker );
    }

    for ( int32_t i = 0; i < walkLimit; ++i ) {
        if ( g_yieldEveryN > 0 && ( i % g_yieldEveryN ) == 0 && i > 0 ) {
            Sleep( g_yieldMs );
        }

        if ( i > 0 && ( i % reportEvery ) == 0 ) {
            DWORD elapsed = GetTickCount( ) - startMs;
            char pb [ 96 ];
            sprintf_s( pb, "%d/%d  %lu ms", i, walkLimit, elapsed );
            Log( pb );
            if ( !MemoryHealthy( ) ) {
                Log( "low free RAM, aborting dump" );
                break;
            }
        }

        void * go = ArrayElement( arr, i );
        if ( !go ) {
            ++skipped;
            continue;
        }

        if ( g_tracker ) {
            std::string preCls = ObjectClassFq( go );
            fseek( g_tracker, 0, SEEK_SET );
            fprintf( g_tracker, "i=%d  go=%p  class=%s              \n", i, go,
                preCls.empty( ) ? "?" : preCls.c_str( ) );
            fflush( g_tracker );
        }

        __try {
            std::string name = GetName( go );
            int32_t id = GetInstanceId( go );

            bool active = true;
            int32_t layer = -1;
            std::string tag;

            if ( mGetActive ) {
                void * b = Invoke( mGetActive, go );
                UnboxAs( b, active );
            }
            if ( mGetLayer ) {
                void * b = Invoke( mGetLayer, go );
                UnboxAs( b, layer );
            }
            if ( mGetTag ) {
                void * b = Invoke( mGetTag, go );
                tag = ReadStringObj( b );
            }

            Vector3 pos = { 0, 0, 0 };
            Quaternion rot = { 0, 0, 0, 1 };
            Vector3 scale = { 1, 1, 1 };
            int32_t parentId = 0;

            void * trans = Invoke( mGetTransform, go );
            if ( trans ) {
                UnboxAs( Invoke( mGetPosition, trans ), pos );
                if ( mGetRotation )
                    UnboxAs( Invoke( mGetRotation, trans ), rot );
                if ( mGetLossyScale )
                    UnboxAs( Invoke( mGetLossyScale, trans ), scale );
                if ( mGetParent ) {
                    void * pTrans = Invoke( mGetParent, trans );
                    if ( pTrans )
                        parentId = GetInstanceId( pTrans );
                }
            }

            out << "@go id=" << id << " name=\"" << EscapeStr( name ) << "\""
                << " active=" << ( active ? "true" : "false" ) << " layer=" << layer
                << " tag=\"" << EscapeStr( tag ) << "\""
                << "\n";

            char buf [ 160 ];
            sprintf_s( buf, "  pos=(%.3f, %.3f, %.3f)", pos.x, pos.y, pos.z );
            out << buf << "\n";
            sprintf_s( buf, "  rot=(%.4f, %.4f, %.4f, %.4f)", rot.x, rot.y, rot.z,
                rot.w );
            out << buf << "\n";
            sprintf_s( buf, "  scale=(%.3f, %.3f, %.3f)", scale.x, scale.y, scale.z );
            out << buf << "\n";
            if ( parentId )
                out << "  parent_id=" << parentId << "\n";

            DumpComponents( out, go );
            out << "\n";
            ++dumped;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            ++skipped;
        }

        if ( g_deepFieldDump && ( i % 100 ) == 0 ) {
            out.flush( );
        }
    }

    out.close( );
    if ( g_tracker ) {
        fseek( g_tracker, 0, SEEK_SET );
        fprintf(
            g_tracker,
            "completed                                                       \n" );
        fclose( g_tracker );
        g_tracker = nullptr;
    }
    DWORD totalMs = GetTickCount( ) - startMs;
    char fb [ 256 ];
    sprintf_s( fb, "dumped %d / %d  (%d skipped, %lu ms) -> %s", dumped, walkLimit,
        skipped, totalMs, path.c_str( ) );
    Log( fb );
}

// Main camera state.
void SceneDumper::DumpCamera( ) {
    if ( !g_ready )
        return;
    if ( !clsCamera || !mCameraMain ) {
        Log( "Camera class not loaded" );
        return;
    }

    GcSuspendScope gcGuard;

    void * mainCam = Invoke( mCameraMain, nullptr );
    if ( !mainCam ) {
        Log( "Camera.main is null - no main camera tagged" );
        return;
    }

    SYSTEMTIME st;
    GetLocalTime( &st );
    char ts [ 64 ];
    sprintf_s( ts, "%04d%02d%02d_%02d%02d%02d", st.wYear, st.wMonth, st.wDay,
        st.wHour, st.wMinute, st.wSecond );

    std::string base =
        g_outputDir.empty( ) ? std::string( "C:\\" ) : g_outputDir + "\\";
    std::string path = base + "IL2CPP_Camera_" + ts + ".txt";
    std::ofstream out( path, std::ios::out | std::ios::trunc );
    if ( !out.is_open( ) ) {
        Log( "cannot open: " + path );
        return;
    }

    void * trans = Invoke( mGetTransform, mainCam );
    Vector3 pos = { 0, 0, 0 }, fwd = { 0, 0, 1 }, right = { 1, 0, 0 }, up = { 0, 1, 0 };
    if ( trans ) {
        UnboxAs( Invoke( mGetPosition, trans ), pos );
        if ( mTransformGetForward )
            UnboxAs( Invoke( mTransformGetForward, trans ), fwd );
        if ( mTransformGetRight )
            UnboxAs( Invoke( mTransformGetRight, trans ), right );
        if ( mTransformGetUp )
            UnboxAs( Invoke( mTransformGetUp, trans ), up );
    }

    float fov = 60.0f, aspect = 16.0f / 9.0f, nearC = 0.3f, farC = 1000.0f;
    if ( mCameraGetFov )
        UnboxAs( Invoke( mCameraGetFov, mainCam ), fov );
    if ( mCameraGetAspect )
        UnboxAs( Invoke( mCameraGetAspect, mainCam ), aspect );
    if ( mCameraGetNear )
        UnboxAs( Invoke( mCameraGetNear, mainCam ), nearC );
    if ( mCameraGetFar )
        UnboxAs( Invoke( mCameraGetFar, mainCam ), farC );

    out << "# Main camera state captured " << ts << "\n";
    out << "# all vectors are world-space; fov in degrees\n\n";
    char b [ 256 ];
    sprintf_s( b, "pos     = (%.4f, %.4f, %.4f)", pos.x, pos.y, pos.z );
    out << b << "\n";
    sprintf_s( b, "forward = (%.4f, %.4f, %.4f)", fwd.x, fwd.y, fwd.z );
    out << b << "\n";
    sprintf_s( b, "right   = (%.4f, %.4f, %.4f)", right.x, right.y, right.z );
    out << b << "\n";
    sprintf_s( b, "up      = (%.4f, %.4f, %.4f)", up.x, up.y, up.z );
    out << b << "\n";
    sprintf_s( b, "fov_deg = %.4f", fov );
    out << b << "\n";
    sprintf_s( b, "aspect  = %.4f", aspect );
    out << b << "\n";
    sprintf_s( b, "near    = %.4f", nearC );
    out << b << "\n";
    sprintf_s( b, "far     = %.4f", farC );
    out << b << "\n";
    out.close( );
    Log( "camera -> " + path );
}

void SceneDumper::DumpMeshColliders( ) {
    if ( !g_ready )
        return;

    void * clsMeshCol = ResolveUnityClass( "UnityEngine", "MeshCollider" );
    void * clsMesh = ResolveUnityClass( "UnityEngine", "Mesh" );
    if ( !clsMeshCol || !clsMesh ) {
        Log( "MeshCollider/Mesh classes not found" );
        return;
    }

    GcSuspendScope gcGuard;

    void * mGetSharedMesh =
        api::class_get_method_from_name( clsMeshCol, "get_sharedMesh", 0 );
    void * mGetVertices =
        api::class_get_method_from_name( clsMesh, "get_vertices", 0 );
    void * mGetTriangles =
        api::class_get_method_from_name( clsMesh, "get_triangles", 0 );
    if ( !mGetSharedMesh || !mGetVertices || !mGetTriangles ) {
        Log( "MeshCollider/Mesh getters missing" );
        return;
    }

    void * tType = api::class_get_type( clsMeshCol );
    if ( !tType )
        return;
    void * tObj = api::type_get_object( tType );
    if ( !tObj )
        return;

    void * args[ ] = { tObj };
    void * arr = Invoke( mFindObjectsOfTypeAll, nullptr, args );
    int32_t cnt = ArrayLengthSafe( arr );
    Log( std::to_string( cnt ) + " MeshColliders" );
    if ( cnt == 0 )
        return;

    SYSTEMTIME st;
    GetLocalTime( &st );
    char ts [ 64 ];
    sprintf_s( ts, "%04d%02d%02d_%02d%02d%02d", st.wYear, st.wMonth, st.wDay,
        st.wHour, st.wMinute, st.wSecond );

    std::string base = g_outputDir.empty( ) ? "C:\\" : g_outputDir + "\\";
    std::string idxPath = base + "IL2CPP_MeshColliders_" + ts + ".txt";
    std::string binPath = base + "IL2CPP_MeshColliders_" + ts + ".bin";

    std::ofstream idx( idxPath, std::ios::out | std::ios::trunc );
    idx << "# MeshCollider dump  " << ts << "\n";
    idx << "# bin layout per collider:\n";
    idx << "#   uint32 instance_id, float pos[3] (collider GO position)\n";
    idx << "#   uint32 vertex_count, Vector3[vc] verts (local-space)\n";
    idx << "#   uint32 index_count,  int32[ic] tris\n\n";

    FILE * bf = nullptr;
    fopen_s( &bf, binPath.c_str( ), "wb" );
    if ( !bf ) {
        Log( "cannot open: " + binPath );
        idx.close( );
        return;
    }

    int written = 0, skipped = 0;
    for ( int32_t i = 0; i < cnt; ++i ) {
        if ( ( i % 100 ) == 0 && i > 0 )
            Sleep( g_yieldMs );

        void * mc = ArrayElement( arr, i );
        if ( !mc ) {
            ++skipped;
            continue;
        }

        __try {
            void * mesh = Invoke( mGetSharedMesh, mc );
            if ( !mesh ) {
                ++skipped;
                continue;
            }

            void * verts = Invoke( mGetVertices, mesh );
            void * tris = Invoke( mGetTriangles, mesh );
            if ( !verts || !tris ) {
                ++skipped;
                continue;
            }

            int32_t vc = ArrayLengthSafe( verts );
            int32_t ic = ArrayLengthSafe( tris );
            if ( vc <= 0 || ic <= 0 ) {
                ++skipped;
                continue;
            }

            if ( vc > 200000 || ic > 600000 ) {
                ++skipped;
                continue;
            }

            int32_t id = GetInstanceId( mc );

            Vector3 colPos = { 0, 0, 0 };
            void * trans = Invoke( mGetTransform, mc );
            if ( trans )
                UnboxAs( Invoke( mGetPosition, trans ), colPos );

            uint32_t u_id = ( uint32_t ) id;
            uint32_t u_vc = ( uint32_t ) vc;
            uint32_t u_ic = ( uint32_t ) ic;

            fwrite( &u_id, sizeof( uint32_t ), 1, bf );
            fwrite( &colPos, sizeof( Vector3 ), 1, bf );
            fwrite( &u_vc, sizeof( uint32_t ), 1, bf );
            __try {
                Vector3 * vp =
                    reinterpret_cast< Vector3 * >( reinterpret_cast< char * >( verts ) + 32 );
                fwrite( vp, sizeof( Vector3 ), vc, bf );
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
            }
            fwrite( &u_ic, sizeof( uint32_t ), 1, bf );
            __try {
                int32_t * ip =
                    reinterpret_cast< int32_t * >( reinterpret_cast< char * >( tris ) + 32 );
                fwrite( ip, sizeof( int32_t ), ic, bf );
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
            }

            ++written;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            ++skipped;
        }
    }

    fclose( bf );
    idx << "colliders_written = " << written << "\n";
    idx << "colliders_skipped = " << skipped << "\n";
    idx << "binary = IL2CPP_MeshColliders_" << ts << ".bin\n";
    idx.close( );
    Log( "mesh: " + std::to_string( written ) + " written, " +
        std::to_string( skipped ) + " skipped -> " + binPath );
}

void SceneDumper::DumpClassesByList( ) {
    if ( !g_ready )
        return;

    std::string base = g_outputDir.empty( ) ? "C:\\" : g_outputDir + "\\";
    std::string listPath = base + "IL2CPP_ScanList.txt";

    std::ifstream listIn( listPath );
    if ( !listIn.is_open( ) ) {
        Log( "no IL2CPP_ScanList.txt - drop one next to the launcher with FQ class "
            "names" );
        return;
    }

    std::vector<std::string> classes;
    std::string line;
    while ( std::getline( listIn, line ) ) {

        size_t a = line.find_first_not_of( " \t\r\n" );
        size_t b = line.find_last_not_of( " \t\r\n" );

        if ( a == std::string::npos )
            continue;

        std::string c = line.substr( a, b - a + 1 );

        if ( c.empty( ) || c [ 0 ] == '#' )
            continue;
        classes.push_back( c );
    }
    listIn.close( );
    if ( classes.empty( ) ) {
        Log( "scan list is empty" );
        return;
    }

    GcSuspendScope gcGuard;

    SYSTEMTIME st;
    GetLocalTime( &st );
    char ts [ 64 ];
    sprintf_s( ts, "%04d%02d%02d_%02d%02d%02d", st.wYear, st.wMonth, st.wDay,
        st.wHour, st.wMinute, st.wSecond );

    std::string outPath = base + "IL2CPP_Scan_" + ts + ".txt";
    std::ofstream out( outPath, std::ios::out | std::ios::trunc );
    out << "# Targeted class scan  " << ts << "\n";

    int totalInstances = 0;
    for ( const auto & fq : classes ) {

        size_t dot = fq.find_last_of( '.' );
        std::string ns = ( dot == std::string::npos ) ? "" : fq.substr( 0, dot );
        std::string nm = ( dot == std::string::npos ) ? fq : fq.substr( dot + 1 );

        void * klass = nullptr;
        if ( api::class_from_name && api::get_domain && api::get_assemblies &&
            api::assembly_get_image ) {
            void * domain = api::get_domain( );
            size_t cnt = 0;
            void ** asses = api::get_assemblies( domain, &cnt );
            for ( size_t i = 0; i < cnt && !klass; ++i ) {
                void * img = api::assembly_get_image( asses [ i ] );
                if ( !img )
                    continue;
                klass = api::class_from_name( img, ns.c_str( ), nm.c_str( ) );
            }
        }

        out << "\n@class " << fq << "\n";
        if ( !klass ) {
            out << "  (not found)\n";
            Log( fq + ": not found" );
            continue;
        }

        void * tType = api::class_get_type( klass );
        void * tObj = tType ? api::type_get_object( tType ) : nullptr;
        if ( !tObj ) {
            out << "  (no type object)\n";
            continue;
        }

        void * args[ ] = { tObj };
        void * arr = Invoke( mFindObjectsOfTypeAll, nullptr, args );
        int32_t inst = ArrayLengthSafe( arr );
        out << "  instances=" << inst << "\n";

        Log( fq + ": " + std::to_string( inst ) + " instances" );

        for ( int32_t i = 0; i < inst; ++i ) {
            void * obj = ArrayElement( arr, i );
            if ( !obj )
                continue;
            int32_t id = GetInstanceId( obj );
            std::string name = GetName( obj );
            out << "  inst id=" << id << " name=\"" << EscapeStr( name ) << "\"\n";

            if ( clsComponent && IsSubclassOf( klass, clsComponent ) ) {
                void * trans = Invoke( mGetTransform, obj );
                if ( trans ) {
                    Vector3 p = { 0, 0, 0 };
                    UnboxAs( Invoke( mGetPosition, trans ), p );
                    char b [ 96 ];
                    sprintf_s( b, "    pos=(%.3f, %.3f, %.3f)", p.x, p.y, p.z );
                    out << b << "\n";
                }
            }

            DumpMonoBehaviourFields( out, obj, klass );
            ++totalInstances;
        }
    }

    out.close( );
    Log( "scan: " + std::to_string( totalInstances ) + " instances -> " + outPath );
}

void SceneDumper::DumpAll( ) {
    DumpCamera( );
    Dump( );
    DumpMeshColliders( );
}
