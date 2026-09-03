#define _CRT_SECURE_NO_WARNINGS

#include "../include/model_replacer.hxx"
#include "../include/il2cpp_api.hxx"
#include "../include/utils.hxx"
#include "../../third_party/minhook/include/MinHook.h"

#include <algorithm>
#include <atomic>
#include <cstdint>
#include <cstring>
#include <string>
#include <unordered_map>
#include <unordered_set>
#include <vector>

namespace {

    constexpr uintptr_t RVA_CACHED_LOAD_GAME_OBJECT = 0x04701AF0;
    constexpr uintptr_t RVA_I18N_LOAD_GAME_OBJECT = 0x0304D350;
    constexpr uintptr_t RVA_I18N_RESOLVE_PATH_HASH = 0x0304DB50;
    constexpr uintptr_t RVA_INIT_INITIAL_PATH_HASH = 0x0311B400;
    constexpr uintptr_t RVA_INIT_MAIN_PATH_HASH = 0x04994340;
    constexpr uintptr_t RVA_RESOURCE_MANAGER_IS_INITIALIZED = 0x036D47E0;
    constexpr uintptr_t RVA_RESOURCE_MANAGER_IS_ASYNC_INITING = 0x04CDB190;
    constexpr uintptr_t RVA_RESOURCE_MANAGER_USING_BUNDLE = 0x03C8F590;
    constexpr uintptr_t RVA_RESOURCE_MANAGER_CHECK_STRING = 0x0304B570;
    constexpr uintptr_t RVA_RESOURCE_MANAGER_CHECK_HASH = 0x030492B0;
    constexpr uintptr_t RVA_RESOURCE_MANAGER_RAW_CHECK_HASH = 0x0304A850;
    constexpr uintptr_t RVA_BUNDLE_LOAD_UNTRACKED_HASH = 0x03393F90;
    constexpr uintptr_t RVA_LOAD_ASSET_INTERNAL_HASH = 0x02FCE2B0;
    constexpr uintptr_t RVA_BUNDLE_LOADER_LOAD_ASSET = 0x02FCEE80;
    constexpr uintptr_t RVA_TRY_GET_ASSET_INFO_HASH = 0x02FCE680;
    constexpr uintptr_t RVA_UNTRACKED_HANDLE_GATE = 0x02EFB620;
    constexpr uintptr_t RVA_UNTRACKED_HANDLE_IS_VALID = 0x068AF788;
    constexpr uintptr_t RVA_ASSET_PROXY_HAS_ERROR = 0x02FCDBE0;
    constexpr uintptr_t RVA_ASSET_PROXY_FINISH_ERROR = 0x068C2D48;
    constexpr uintptr_t RVA_BUNDLE_PROXY_LOAD_SYNC = 0x02F67FE0;
    constexpr uintptr_t RVA_BUNDLE_PROXY_GET_FILE_PATH = 0x067DD024;
    constexpr uintptr_t RVA_BUNDLE_PROXY_LOAD_FILE = 0x067DD138;
    constexpr uintptr_t RVA_LOGIN_SCENE_ROOT_ON_BIND = 0x0380F010;
    constexpr uintptr_t RVA_ASSET_PROXY_HANDLE_GET = 0x0360A4D0;
    constexpr uintptr_t RVA_INSTANTIATE_GAME_OBJECT_PARENT = 0x03E03C80;
    constexpr uintptr_t RVA_GAME_OBJECT_SET_ACTIVE = 0x0339E900;
    constexpr uintptr_t RVA_TRANSFORM_SET_PARENT = 0x03339FA0;
    constexpr uintptr_t RVA_GAME_OBJECT_GET_COMPONENTS_IN_CHILDREN = 0x04753620;
    constexpr uintptr_t RVA_RENDERER_SET_ENABLED = 0x0339A890;
    constexpr uintptr_t RVA_SKINNED_MESH_RENDERER_SET_BONES = 0x0B3CBFAC;
    constexpr uintptr_t RVA_SKINNED_MESH_RENDERER_SET_ROOT_BONE = 0x039C9B00;
    constexpr double MIN_SKELETON_MAPPING_COVERAGE = 0.50;
    constexpr size_t MIN_UNIQUE_MAPPED_BONES = 20;
    constexpr uint64_t PELICA_POSTMODEL_PATH_HASH = 0x06F2CE7B3D05C9CCULL;
    constexpr uint64_t PELICA_POSTMODEL_BUNDLE_HASH = 0x0AF1ED0668AAAB45ULL;
    constexpr uint8_t ROOT_CATEGORY_MAIN = 0;
    constexpr uint8_t ROOT_CATEGORY_INITIAL = 1;
    constexpr uintptr_t TLS_FLAG_TARGET_INTERNAL = 1ULL << 16;
    constexpr uintptr_t TLS_FLAG_EXPECT_HANDLE_GATE = 1ULL << 17;
    constexpr uintptr_t TLS_FLAG_CAPTURE_LOGIN_LOAD = 1ULL << 18;
    constexpr uintptr_t TLS_FLAG_LOGIN_BIND_SCOPE = 1ULL << 19;
    constexpr const char * LOGIN_SK_ACTOR_F =
        "Assets/Beyond/InitialAssets/Intro/Prefab/SK_actor_female.prefab";
    constexpr const char * PELICA_POSTMODEL_PATH =
        "Assets/Beyond/DynamicAssets/Gameplay/Actors/PostModels/Characters/"
        "chr_0004_pelica_postmodel.prefab";
    constexpr const char * TARGET_MODEL_PATH =
        "Assets/Beyond/DynamicAssets/Gameplay/Actors/PostModels/Characters/"
        "chr_0004_pelica_postmodel.prefab";

    std::atomic_bool g_replacementActive = false;
    std::atomic_bool g_redirectEnabled = true;
    std::atomic_bool g_redirectHit = false;
    std::atomic_bool g_mainCategoryHit = false;
    std::atomic_bool g_mainLoadReturnedHandle = false;
    std::atomic_bool g_initialPathHashEntered = false;
    std::atomic_bool g_initialPathHashCompleted = false;
    std::atomic_bool g_mainPathHashEntered = false;
    std::atomic_bool g_mainPathHashCompleted = false;
    std::atomic_bool g_mainDiagnosticsStarted = false;
    std::atomic_bool g_mainDiagnosticsCompleted = false;
    std::atomic_bool g_resourceManagerInitialized = false;
    std::atomic_bool g_resourceManagerAsyncIniting = false;
    std::atomic_bool g_resourceManagerUsingBundle = false;
    std::atomic_bool g_stringCheckExists = false;
    std::atomic_bool g_hashCheckExists = false;
    std::atomic_bool g_rawHashCheckExists = false;
    std::atomic_uint64_t g_initialPathHashCompletedTick = 0;
    std::atomic_uint64_t g_mainPathHashCompletedTick = 0;
    std::atomic_uint64_t g_targetResolvedHash = 0;
    std::atomic_bool g_loadAssetInternalReturnedProxy = false;
    std::atomic_bool g_bundleLoaderReturnedProxy = false;
    std::atomic_bool g_bundleLoaderProxyHasError = false;
    std::atomic_bool g_loadUntrackedReturnedHandle = false;
    std::atomic_bool g_loadUntrackedDirectIsValid = false;
    std::atomic_bool g_untrackedHandleGatePassed = false;
    std::atomic_bool g_tryGetAssetInfoLoadObserved = false;
    std::atomic_bool g_loadAssetInternalObserved = false;
    std::atomic_bool g_bundleLoaderObserved = false;
    std::atomic_bool g_loadUntrackedObserved = false;
    std::atomic_bool g_untrackedHandleGateObserved = false;
    std::atomic_bool g_assetProxyFinishErrorObserved = false;
    std::atomic_bool g_mainBundlePathObserved = false;
    std::atomic_uint32_t g_bundleFileLoadCount = 0;
    std::atomic_uint32_t g_bundleFileFailureCount = 0;
    std::atomic_uint32_t g_bundleSyncCount = 0;
    std::atomic_uint32_t g_bundleSyncFailureCount = 0;
    std::atomic_bool g_deferredLoadPending = false;
    std::atomic_bool g_deferredLoadAttempted = false;
    std::atomic_bool g_loginLoadContextCaptured = false;
    std::atomic< void * > g_i18nLoadGameObjectMethod = nullptr;
    std::atomic_uint32_t g_loginLoadThreadId = 0;
    std::atomic_uint64_t g_deferredHandleRaw0 = 0;
    std::atomic_uint64_t g_deferredHandleRaw1 = 0;
    std::atomic_uint32_t g_deferredHandleRaw2 = 0;
    std::atomic_bool g_loginBindObserved = false;
    std::atomic_bool g_loginInstantiateCaptured = false;
    std::atomic_bool g_pelicaPrefabResolved = false;
    std::atomic_bool g_pelicaInstantiateAttempted = false;
    std::atomic_bool g_pelicaInstantiateSucceeded = false;
    std::atomic_bool g_originalLoginActorDisabled = false;
    std::atomic_bool g_skeletonBindAttempted = false;
    std::atomic_bool g_skeletonBindSucceeded = false;
    std::atomic_bool g_originalRenderersDisabled = false;
    std::atomic< void * > g_loginActorPrefab = nullptr;
    std::atomic< void * > g_loginActorParent = nullptr;
    std::atomic< void * > g_loginActorInstance = nullptr;
    std::atomic< void * > g_loginInstantiateMethod = nullptr;
    std::atomic< void * > g_pelicaPrefab = nullptr;
    std::atomic< void * > g_pelicaInstance = nullptr;
    uint32_t g_pelicaPrefabGcHandle = 0;

    using CachedLoadGameObjectFn = void * ( __fastcall * )(
        void * result, void * loader, void * path, void * method );
    using I18NLoadGameObjectFn = void * ( __fastcall * )(
        void * result, uint64_t pathHash, uint8_t category, void * method );
    using ResolveI18NPathHashFn = uint64_t( __fastcall * )(
        uint64_t pathHash, uint8_t category, void * method );
    using InitPathHashFn = void( __fastcall * )( void * method );
    using StaticBoolFn = bool( __fastcall * )( void * method );
    using CheckStringFn = bool( __fastcall * )(
        void * path, uint8_t category, void * method );
    using CheckHashFn = bool( __fastcall * )(
        uint64_t pathHash, uint8_t category, void * method );
    using LoadUntrackedHashFn = void * ( __fastcall * )(
        void * result, void * manager, uint64_t pathHash, void * type,
        uint8_t category, void * method );
    using LoadAssetInternalHashFn = void * ( __fastcall * )(
        void * manager, uint64_t pathHash, void * type, uint8_t category,
        bool isAsync, int priority, void * method );
    using BundleLoaderLoadAssetFn = void * ( __fastcall * )(
        void * manager, void * assetInfo, void * type, bool isAsync,
        int priority, void * method );
    using TryGetAssetInfoHashFn = bool( __fastcall * )(
        void * manifest, uint64_t pathHash, void * assetInfo, void * method );
    using HandleGateFn = bool( __fastcall * )( void * handle, void * method );
    using AssetProxyHasErrorFn = bool( __fastcall * )(
        void * assetProxy, void * method );
    using AssetProxyFinishErrorFn = void( __fastcall * )(
        void * assetProxy, void * message, void * method );
    using BundleProxyLoadSyncFn = bool( __fastcall * )(
        void * bundleProxy, void * method );
    using BundleProxyGetFilePathFn = void * ( __fastcall * )(
        void * bundleProxy, void * bundleName, void * method );
    using BundleProxyLoadFileFn = void * ( __fastcall * )(
        void * filePath, void * method );
    using Il2CppThreadCurrentFn = void * ( __fastcall * )( );
    using Il2CppStringNewFn = void * ( __fastcall * )( const char * value );
    using LoginSceneRootOnBindFn = void( __fastcall * )(
        void * instance, void * method );
    using AssetProxyHandleGetFn = void * ( __fastcall * )(
        void * handle, void * method );
    using InstantiateGameObjectParentFn = void * ( __fastcall * )(
        void * original, void * parent, bool worldPositionStays, void * method );
    using GameObjectSetActiveFn = void( __fastcall * )(
        void * gameObject, bool active, void * method );
    using TransformSetParentFn = void( __fastcall * )(
        void * transform, void * parent, bool worldPositionStays, void * method );
    using GameObjectGetComponentsInChildrenFn = void * ( __fastcall * )(
        void * gameObject, void * type, bool includeInactive, void * method );
    using RendererSetEnabledFn = void( __fastcall * )(
        void * renderer, bool enabled, void * method );
    using SkinnedMeshRendererSetBonesFn = void( __fastcall * )(
        void * renderer, void * bones, void * method );
    using SkinnedMeshRendererSetRootBoneFn = void( __fastcall * )(
        void * renderer, void * rootBone, void * method );
    CachedLoadGameObjectFn g_originalCachedLoadGameObject = nullptr;
    I18NLoadGameObjectFn g_originalI18NLoadGameObject = nullptr;
    ResolveI18NPathHashFn g_originalResolveI18NPathHash = nullptr;
    InitPathHashFn g_originalInitInitialPathHash = nullptr;
    InitPathHashFn g_originalInitMainPathHash = nullptr;
    StaticBoolFn g_resourceManagerIsInitialized = nullptr;
    StaticBoolFn g_resourceManagerIsAsyncIniting = nullptr;
    StaticBoolFn g_resourceManagerGetUsingBundle = nullptr;
    CheckStringFn g_resourceManagerCheckString = nullptr;
    CheckHashFn g_resourceManagerCheckHash = nullptr;
    CheckHashFn g_resourceManagerRawCheckHash = nullptr;
    LoadUntrackedHashFn g_originalLoadUntrackedHash = nullptr;
    LoadAssetInternalHashFn g_originalLoadAssetInternalHash = nullptr;
    BundleLoaderLoadAssetFn g_originalBundleLoaderLoadAsset = nullptr;
    TryGetAssetInfoHashFn g_originalTryGetAssetInfoHash = nullptr;
    HandleGateFn g_originalUntrackedHandleGate = nullptr;
    HandleGateFn g_untrackedHandleIsValid = nullptr;
    AssetProxyHasErrorFn g_assetProxyHasError = nullptr;
    AssetProxyFinishErrorFn g_originalAssetProxyFinishError = nullptr;
    BundleProxyLoadSyncFn g_originalBundleProxyLoadSync = nullptr;
    BundleProxyGetFilePathFn g_originalBundleProxyGetFilePath = nullptr;
    BundleProxyLoadFileFn g_originalBundleProxyLoadFile = nullptr;
    Il2CppThreadCurrentFn g_il2cppThreadCurrent = nullptr;
    Il2CppStringNewFn g_il2cppStringNew = nullptr;
    LoginSceneRootOnBindFn g_originalLoginSceneRootOnBind = nullptr;
    AssetProxyHandleGetFn g_assetProxyHandleGet = nullptr;
    InstantiateGameObjectParentFn g_originalInstantiateGameObjectParent = nullptr;
    GameObjectSetActiveFn g_gameObjectSetActive = nullptr;
    TransformSetParentFn g_transformSetParent = nullptr;
    GameObjectGetComponentsInChildrenFn g_gameObjectGetComponentsInChildren = nullptr;
    RendererSetEnabledFn g_rendererSetEnabled = nullptr;
    SkinnedMeshRendererSetBonesFn g_skinnedMeshRendererSetBones = nullptr;
    SkinnedMeshRendererSetRootBoneFn g_skinnedMeshRendererSetRootBone = nullptr;
    void * g_cachedLoadGameObjectTarget = nullptr;
    void * g_i18nLoadGameObjectTarget = nullptr;
    void * g_resolveI18NPathHashTarget = nullptr;
    void * g_initInitialPathHashTarget = nullptr;
    void * g_initMainPathHashTarget = nullptr;
    void * g_loadUntrackedHashTarget = nullptr;
    void * g_loadAssetInternalHashTarget = nullptr;
    void * g_bundleLoaderLoadAssetTarget = nullptr;
    void * g_tryGetAssetInfoHashTarget = nullptr;
    void * g_untrackedHandleGateTarget = nullptr;
    void * g_assetProxyFinishErrorTarget = nullptr;
    void * g_bundleProxyLoadSyncTarget = nullptr;
    void * g_bundleProxyGetFilePathTarget = nullptr;
    void * g_bundleProxyLoadFileTarget = nullptr;
    void * g_loginSceneRootOnBindTarget = nullptr;
    void * g_instantiateGameObjectParentTarget = nullptr;
    DWORD g_redirectTlsIndex = TLS_OUT_OF_INDEXES;
    uintptr_t g_gameAssemblyBase = 0;
    bool g_minHookInitialized = false;

    void * g_loader = nullptr;
    uint32_t g_loaderGcHandle = 0;
    void * g_targetRoot = nullptr;
    void * g_replacementRoot = nullptr;
    std::vector< void * > g_disabledRenderers;

    void * clsObject = nullptr;
    void * clsResources = nullptr;
    void * clsGameObject = nullptr;
    void * clsComponent = nullptr;
    void * clsTransform = nullptr;
    void * clsRenderer = nullptr;
    void * clsSkinnedMeshRenderer = nullptr;
    void * clsResourceManager = nullptr;
    void * clsSimpleAssetLoader = nullptr;
    void * clsStringPathHash = nullptr;
    void * clsAssetLoaderHandle = nullptr;

    void * mFindObjectsOfTypeAll = nullptr;
    void * mObjectGetName = nullptr;
    void * mObjectDestroy = nullptr;
    void * mGameObjectFind = nullptr;
    void * mGameObjectGetTransform = nullptr;
    void * mGameObjectGetLayer = nullptr;
    void * mGameObjectSetLayer = nullptr;
    void * mGameObjectSetActive = nullptr;
    void * mComponentGetGameObject = nullptr;
    void * mGetComponentsInChildren = nullptr;
    void * mTransformGetParent = nullptr;
    void * mTransformSetParent = nullptr;
    void * mGetLocalPosition = nullptr;
    void * mSetLocalPosition = nullptr;
    void * mGetLocalRotation = nullptr;
    void * mSetLocalRotation = nullptr;
    void * mGetLocalScale = nullptr;
    void * mSetLocalScale = nullptr;
    void * mRendererSetEnabled = nullptr;
    void * mSkinnedGetBones = nullptr;
    void * mSkinnedSetBones = nullptr;
    void * mSkinnedGetRootBone = nullptr;
    void * mSkinnedSetRootBone = nullptr;
    void * mResourceCheckExists = nullptr;
    void * mLoaderCtor = nullptr;
    void * mCreateStringPathHash = nullptr;
    void * mInstantiatePrefab = nullptr;
    void * mHandleLoadImmediate = nullptr;
    void * mHandleGet = nullptr;
    void * mHandleGetHasError = nullptr;
    void * mHandleGetErrorCode = nullptr;

    struct Vector3 {
        float x, y, z;
    };

    struct Quaternion {
        float x, y, z, w;
    };

    struct RawIl2CppString {
        void * klass;
        void * monitor;
        int32_t length;
        wchar_t chars [ 1 ];
    };

    struct RendererBinding {
        void * renderer = nullptr;
        std::vector< void * > bones;
        void * rootBone = nullptr;
        int mapped = 0;
    };

    static bool RawStringEqualsAscii( void * value, const char * expected ) {
        if ( !value || !expected )
            return false;

        __try {
            auto * str = reinterpret_cast< RawIl2CppString * >( value );
            size_t expectedLength = strlen( expected );
            if ( str->length < 0 ||
                static_cast< size_t >( str->length ) != expectedLength )
                return false;
            for ( int32_t i = 0; i < str->length; ++i ) {
                if ( str->chars [ i ] !=
                    static_cast< wchar_t >( static_cast< unsigned char >(
                        expected [ i ] ) ) )
                    return false;
            }
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return false;
        }
    }

    static uintptr_t GetRedirectScopeDepth( ) {
        if ( g_redirectTlsIndex == TLS_OUT_OF_INDEXES )
            return 0;
        return reinterpret_cast< uintptr_t >(
            TlsGetValue( g_redirectTlsIndex ) );
    }

    static bool SetRedirectScopeDepth( uintptr_t depth ) {
        return g_redirectTlsIndex != TLS_OUT_OF_INDEXES &&
            TlsSetValue( g_redirectTlsIndex,
                reinterpret_cast< void * >( depth ) ) != FALSE;
    }

    static const char * BoolResult( bool called, bool value ) {
        return called ? ( value ? "true" : "false" ) : "fault";
    }

    static bool CheckPrologue( const char * label, const void * target,
        const uint8_t * expected, size_t length ) {
        constexpr size_t MAX_PROLOGUE_LENGTH = 16;
        uint8_t actual [ MAX_PROLOGUE_LENGTH ] = { 0 };
        bool readable = false;
        bool matches = false;
        if ( target && expected && length > 0 &&
            length <= MAX_PROLOGUE_LENGTH ) {
            __try {
                memcpy( actual, target, length );
                readable = true;
                matches = memcmp( actual, expected, length ) == 0;
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
                readable = false;
                matches = false;
            }
        }

        if ( matches )
            return true;

        char expectedHex [ MAX_PROLOGUE_LENGTH * 3 + 1 ] = { 0 };
        char actualHex [ MAX_PROLOGUE_LENGTH * 3 + 1 ] = { 0 };
        for ( size_t i = 0; i < length && i < MAX_PROLOGUE_LENGTH; ++i ) {
            snprintf( expectedHex + i * 3,
                sizeof( expectedHex ) - i * 3, "%02X ", expected [ i ] );
            if ( readable )
                snprintf( actualHex + i * 3,
                    sizeof( actualHex ) - i * 3, "%02X ", actual [ i ] );
        }
        char output [ 320 ] = { 0 };
        snprintf( output, sizeof( output ),
            "[hook-diag] signature mismatch %s target=%p expected=%sactual=%s",
            label ? label : "<unknown>", target, expectedHex,
            readable ? actualHex : "<unreadable>" );
        Log( output );
        return false;
    }

    static std::string ReadString( void * str );

    static const char * BundleLoadStatusName( int status ) {
        switch ( status ) {
        case 0: return "Wait";
        case 1: return "Loading";
        case 2: return "SuccessToLoad";
        case 3: return "FailedToLoad";
        case 4: return "Unloaded";
        default: return "Unknown";
        }
    }

    static bool SafeReadBundleProxyState( void * proxy, uint64_t & hashName,
        int & status, void *& assetBundle, void *& errorCode ) {
        if ( !proxy )
            return false;
        __try {
            hashName = *reinterpret_cast< const uint64_t * >(
                reinterpret_cast< const uint8_t * >( proxy ) + 0x30 );
            assetBundle = *reinterpret_cast< void * const * >(
                reinterpret_cast< const uint8_t * >( proxy ) + 0x38 );
            status = *reinterpret_cast< const int * >(
                reinterpret_cast< const uint8_t * >( proxy ) + 0x40 );
            errorCode = *reinterpret_cast< void * const * >(
                reinterpret_cast< const uint8_t * >( proxy ) + 0x68 );
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            hashName = 0;
            status = -1;
            assetBundle = nullptr;
            errorCode = nullptr;
            return false;
        }
    }

    static bool SafeReadAssetProxyState( void * proxy, int & status,
        void *& bundleProxy, void *& errorCode ) {
        if ( !proxy )
            return false;
        __try {
            bundleProxy = *reinterpret_cast< void * const * >(
                reinterpret_cast< const uint8_t * >( proxy ) + 0x10 );
            status = *reinterpret_cast< const int * >(
                reinterpret_cast< const uint8_t * >( proxy ) + 0x38 );
            errorCode = *reinterpret_cast< void * const * >(
                reinterpret_cast< const uint8_t * >( proxy ) + 0x108 );
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            status = -1;
            bundleProxy = nullptr;
            errorCode = nullptr;
            return false;
        }
    }

    static bool SafeCallStaticBool( StaticBoolFn function, bool & value ) {
        if ( !function )
            return false;
        __try {
            value = function( nullptr );
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            value = false;
            return false;
        }
    }

    static bool SafeCheckString( void * path, uint8_t category, bool & value ) {
        if ( !g_resourceManagerCheckString || !path )
            return false;
        __try {
            value = g_resourceManagerCheckString( path, category, nullptr );
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            value = false;
            return false;
        }
    }

    static bool SafeCheckHash( CheckHashFn function, uint64_t pathHash,
        uint8_t category, bool & value ) {
        if ( !function )
            return false;
        __try {
            value = function( pathHash, category, nullptr );
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            value = false;
            return false;
        }
    }

    static void RunMainResourceDiagnostics( ) {
        if ( g_mainDiagnosticsStarted.exchange( true ) )
            return;

        void * currentThread = nullptr;
        void * targetPath = nullptr;
        __try {
            currentThread = g_il2cppThreadCurrent
                ? g_il2cppThreadCurrent( ) : nullptr;
            targetPath = currentThread && g_il2cppStringNew
                ? g_il2cppStringNew( PELICA_POSTMODEL_PATH ) : nullptr;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            currentThread = nullptr;
            targetPath = nullptr;
        }

        if ( !currentThread || !targetPath ) {
            Log( "[main-diag] target callback was not IL2CPP-attached or path allocation failed" );
            return;
        }

        bool initialized = false;
        bool asyncIniting = false;
        bool usingBundle = false;
        bool stringExists = false;
        bool hashExists = false;
        bool rawHashExists = false;

        bool initializedCalled = SafeCallStaticBool(
            g_resourceManagerIsInitialized, initialized );
        bool asyncInitingCalled = SafeCallStaticBool(
            g_resourceManagerIsAsyncIniting, asyncIniting );
        bool usingBundleCalled = SafeCallStaticBool(
            g_resourceManagerGetUsingBundle, usingBundle );
        bool stringCalled = SafeCheckString( targetPath, ROOT_CATEGORY_MAIN,
            stringExists );
        bool hashCalled = SafeCheckHash( g_resourceManagerCheckHash,
            PELICA_POSTMODEL_PATH_HASH, ROOT_CATEGORY_MAIN, hashExists );
        bool rawHashCalled = SafeCheckHash( g_resourceManagerRawCheckHash,
            PELICA_POSTMODEL_PATH_HASH, ROOT_CATEGORY_MAIN, rawHashExists );

        if ( initializedCalled )
            g_resourceManagerInitialized = initialized;
        if ( asyncInitingCalled )
            g_resourceManagerAsyncIniting = asyncIniting;
        if ( usingBundleCalled )
            g_resourceManagerUsingBundle = usingBundle;
        if ( stringCalled )
            g_stringCheckExists = stringExists;
        if ( hashCalled )
            g_hashCheckExists = hashExists;
        if ( rawHashCalled )
            g_rawHashCheckExists = rawHashExists;

        char stateMessage [ 320 ] = { 0 };
        snprintf( stateMessage, sizeof( stateMessage ),
            "[main-diag] ResourceManager initialized=%s asyncIniting=%s usingBundle=%s; "
            "InitPathHash entered/completed=%s/%s MainPathHash entered/completed=%s/%s",
            BoolResult( initializedCalled, initialized ),
            BoolResult( asyncInitingCalled, asyncIniting ),
            BoolResult( usingBundleCalled, usingBundle ),
            g_initialPathHashEntered.load( ) ? "true" : "false",
            g_initialPathHashCompleted.load( ) ? "true" : "false",
            g_mainPathHashEntered.load( ) ? "true" : "false",
            g_mainPathHashCompleted.load( ) ? "true" : "false" );
        Log( stateMessage );

        char checkMessage [ 256 ] = { 0 };
        snprintf( checkMessage, sizeof( checkMessage ),
            "[main-diag] Pelica Main CheckExists string=%s hash=%s rawHash=%s",
            BoolResult( stringCalled, stringExists ),
            BoolResult( hashCalled, hashExists ),
            BoolResult( rawHashCalled, rawHashExists ) );
        Log( checkMessage );

        g_mainDiagnosticsCompleted = true;
    }

    static void TryDeferredMainLoad( );
    static void TryInstantiateLoadedPelica( );
    static bool ResolveSkeletonBindingApis( );
    static bool BindReplacement( void * targetRoot, void * replacementRoot );

    static void __fastcall LoginSceneRootOnBindHook( void * instance,
        void * method ) {
        g_loginBindObserved = true;
        uintptr_t previousState = GetRedirectScopeDepth( );
        bool scopeSet = SetRedirectScopeDepth(
            previousState | TLS_FLAG_LOGIN_BIND_SCOPE );
        if ( !scopeSet )
            Log( "[login-capture] TLS login scope failed; actor Instantiate will not be captured" );

        if ( g_originalLoginSceneRootOnBind )
            g_originalLoginSceneRootOnBind( instance, method );

        if ( scopeSet )
            SetRedirectScopeDepth( previousState );
    }

    static void * __fastcall InstantiateGameObjectParentHook( void * original,
        void * parent, bool worldPositionStays, void * method ) {
        void * instance = g_originalInstantiateGameObjectParent
            ? g_originalInstantiateGameObjectParent(
                original, parent, worldPositionStays, method )
            : nullptr;

        uintptr_t scopeState = GetRedirectScopeDepth( );
        bool loginScope = ( scopeState & TLS_FLAG_LOGIN_BIND_SCOPE ) != 0;
        bool actorLoadObserved = g_redirectHit.load( );
        if ( !g_redirectEnabled.load( ) || !loginScope ||
            !actorLoadObserved || worldPositionStays || !original ||
            !instance || !method || g_loginInstantiateCaptured.load( ) )
            return instance;

        g_loginActorPrefab = original;
        g_loginActorParent = parent;
        g_loginActorInstance = instance;
        g_loginInstantiateMethod = method;
        g_loginInstantiateCaptured = true;

        char message [ 384 ] = { 0 };
        snprintf( message, sizeof( message ),
            "[login-capture] captured actor Instantiate original=%p parent=%p "
            "instance=%p method=%p worldPositionStays=false thread=%lu",
            original, parent, instance, method,
            static_cast< unsigned long >( GetCurrentThreadId( ) ) );
        Log( message );
        TryInstantiateLoadedPelica( );
        return instance;
    }

    static void ReleasePelicaPrefabRoot( ) {
        if ( g_pelicaPrefabGcHandle != 0 && api::gchandle_free ) {
            api::gchandle_free( g_pelicaPrefabGcHandle );
            g_pelicaPrefabGcHandle = 0;
        }
    }

    static void TryInstantiateLoadedPelica( ) {
        if ( !g_redirectEnabled.load( ) ||
            !g_loginInstantiateCaptured.load( ) )
            return;

        void * prefab = g_pelicaPrefab.load( );
        void * parent = g_loginActorParent.load( );
        void * originalInstance = g_loginActorInstance.load( );
        void * instantiateMethod = g_loginInstantiateMethod.load( );
        if ( !prefab || !originalInstance || !instantiateMethod ||
            !g_originalInstantiateGameObjectParent || !g_gameObjectSetActive )
            return;

        DWORD currentThreadId = GetCurrentThreadId( );
        DWORD loginThreadId = g_loginLoadThreadId.load( );
        if ( loginThreadId == 0 || currentThreadId != loginThreadId ) {
            char message [ 224 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[replace-live] skipped: callback thread changed login=%lu current=%lu",
                static_cast< unsigned long >( loginThreadId ),
                static_cast< unsigned long >( currentThreadId ) );
            Log( message );
            return;
        }
        if ( g_pelicaInstantiateAttempted.exchange( true ) )
            return;

        if ( !ResolveSkeletonBindingApis( ) ) {
            Log( "[skeleton-bind] required Unity/IL2CPP APIs could not be resolved; original actor retained" );
            ReleasePelicaPrefabRoot( );
            return;
        }

        Log( "[replace-live] login context and Pelica Prefab are ready; instantiating with captured parent" );
        void * replacement = nullptr;
        bool instantiateCalled = false;
        __try {
            replacement = g_originalInstantiateGameObjectParent(
                prefab, parent, false, instantiateMethod );
            instantiateCalled = true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            instantiateCalled = false;
            replacement = nullptr;
        }

        if ( !instantiateCalled || !replacement ) {
            char message [ 320 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[replace-live] Pelica Instantiate failed called=%s prefab=%p parent=%p method=%p",
                instantiateCalled ? "true" : "false", prefab, parent,
                instantiateMethod );
            Log( message );
            ReleasePelicaPrefabRoot( );
            return;
        }

        g_pelicaInstance = replacement;
        g_pelicaInstantiateSucceeded = true;
        g_skeletonBindAttempted = true;
        bool bound = false;
        __try {
            bound = BindReplacement( originalInstance, replacement );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            bound = false;
        }

        g_skeletonBindSucceeded = bound;
        if ( !bound ) {
            bool hidden = false;
            __try {
                g_gameObjectSetActive( replacement, false, nullptr );
                hidden = true;
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
                hidden = false;
            }
            char failure [ 320 ] = { 0 };
            snprintf( failure, sizeof( failure ),
                "[skeleton-bind] binding failed; original actor retained, "
                "replacement SetActive(false)=%s",
                hidden ? "called" : "fault" );
            Log( failure );
            ReleasePelicaPrefabRoot( );
            return;
        }

        g_originalLoginActorDisabled = false;
        g_originalRenderersDisabled = true;
        g_replacementRoot = replacement;
        g_replacementActive = true;

        char message [ 448 ] = { 0 };
        snprintf( message, sizeof( message ),
            "[skeleton-bind] Pelica renderers bound to original skeleton "
            "prefab=%p parent=%p replacement=%p original=%p; "
            "original root remains active",
            prefab, parent, replacement, originalInstance );
        Log( message );
        ReleasePelicaPrefabRoot( );
    }

    static void __fastcall InitInitialPathHashHook( void * method ) {
        g_initialPathHashEntered = true;
        Log( "[main-diag] HashStringPathProcessor.InitInitPathHash entered" );
        if ( g_originalInitInitialPathHash )
            g_originalInitInitialPathHash( method );
        g_initialPathHashCompletedTick = GetTickCount64( );
        g_initialPathHashCompleted = true;
        Log( "[main-diag] HashStringPathProcessor.InitInitPathHash completed" );
        if ( g_mainPathHashCompleted.load( ) )
            TryDeferredMainLoad( );
    }

    static void __fastcall InitMainPathHashHook( void * method ) {
        g_mainPathHashEntered = true;
        Log( "[main-diag] HashStringPathProcessor.InitMainPathHash entered" );
        if ( g_originalInitMainPathHash )
            g_originalInitMainPathHash( method );
        g_mainPathHashCompletedTick = GetTickCount64( );
        g_mainPathHashCompleted = true;
        Log( "[main-diag] HashStringPathProcessor.InitMainPathHash completed" );
    }

    static uint64_t __fastcall ResolveI18NPathHashHook( uint64_t pathHash,
        uint8_t category, void * method ) {
        uint64_t resolvedHash = g_originalResolveI18NPathHash
            ? g_originalResolveI18NPathHash( pathHash, category, method )
            : pathHash;

        if ( pathHash == PELICA_POSTMODEL_PATH_HASH ) {
            g_targetResolvedHash = resolvedHash;
            char message [ 224 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[main-diag] I18N resolve category=%s(%u) input=%016llX output=%016llX%s",
                category == ROOT_CATEGORY_MAIN ? "Main" :
                    ( category == ROOT_CATEGORY_INITIAL ? "Initial" : "Other" ),
                static_cast< unsigned int >( category ),
                static_cast< unsigned long long >( pathHash ),
                static_cast< unsigned long long >( resolvedHash ),
                resolvedHash == pathHash ? " (unchanged)" :
                    ( resolvedHash == 0 ? " (zero)" : "" ) );
            Log( message );
        }
        return resolvedHash;
    }

    static bool ReadRawHandle( void * handle, uint64_t & raw0,
        uint64_t & raw1, uint32_t & raw2 ) {
        if ( !handle )
            return false;
        __try {
            memcpy( &raw0, handle, sizeof( raw0 ) );
            memcpy( &raw1, reinterpret_cast< const uint8_t * >( handle ) + 8,
                sizeof( raw1 ) );
            memcpy( &raw2, reinterpret_cast< const uint8_t * >( handle ) + 16,
                sizeof( raw2 ) );
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            raw0 = 0;
            raw1 = 0;
            raw2 = 0;
            return false;
        }
    }

    static void TryDeferredMainLoad( ) {
        if ( !g_redirectEnabled.load( ) ||
            !g_deferredLoadPending.load( ) ||
            g_deferredLoadAttempted.exchange( true ) )
            return;

        g_deferredLoadPending = false;
        void * method = g_i18nLoadGameObjectMethod.load( );
        DWORD currentThreadId = GetCurrentThreadId( );
        DWORD loginThreadId = g_loginLoadThreadId.load( );
        if ( !g_originalI18NLoadGameObject || !method ) {
            Log( "[deferred-load] skipped: captured I18N Load<GameObject> method is unavailable" );
            return;
        }
        if ( loginThreadId == 0 || currentThreadId != loginThreadId ) {
            char message [ 224 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[deferred-load] skipped: callback thread changed login=%lu current=%lu",
                static_cast< unsigned long >( loginThreadId ),
                static_cast< unsigned long >( currentThreadId ) );
            Log( message );
            return;
        }

        RunMainResourceDiagnostics( );
        g_mainCategoryHit = true;
        Log( "[deferred-load] Main hashes ready; loading Pelica for the first time on the captured game thread" );

        alignas( 16 ) uint8_t handleBuffer [ 24 ] = { 0 };
        uintptr_t previousState = GetRedirectScopeDepth( );
        bool scopeSet = SetRedirectScopeDepth(
            previousState | TLS_FLAG_TARGET_INTERNAL );
        bool called = false;
        __try {
            g_originalI18NLoadGameObject( handleBuffer,
                PELICA_POSTMODEL_PATH_HASH, ROOT_CATEGORY_MAIN, method );
            called = true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            called = false;
        }
        if ( scopeSet )
            SetRedirectScopeDepth( previousState );

        uint64_t raw0 = 0;
        uint64_t raw1 = 0;
        uint32_t raw2 = 0;
        bool readable = called && ReadRawHandle( handleBuffer, raw0, raw1, raw2 );
        bool nonZero = readable && ( raw0 != 0 || raw1 != 0 || raw2 != 0 );
        g_deferredHandleRaw0 = raw0;
        g_deferredHandleRaw1 = raw1;
        g_deferredHandleRaw2 = raw2;
        g_mainLoadReturnedHandle = nonZero;

        char message [ 288 ] = { 0 };
        snprintf( message, sizeof( message ),
            "[deferred-load] Pelica Main load called=%s handle=%s raw=%016llX:%016llX:%08X",
            called ? "true" : "false", nonZero ? "non-zero" : "zero",
            static_cast< unsigned long long >( raw0 ),
            static_cast< unsigned long long >( raw1 ), raw2 );
        Log( message );

        if ( !nonZero || !g_assetProxyHandleGet ) {
            if ( nonZero )
                Log( "[replace-live] FAssetProxyHandle.Get is unavailable" );
            return;
        }

        void * prefab = nullptr;
        bool getCalled = false;
        __try {
            prefab = g_assetProxyHandleGet( handleBuffer, nullptr );
            getCalled = true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            getCalled = false;
            prefab = nullptr;
        }
        if ( !getCalled || !prefab ) {
            char getMessage [ 224 ] = { 0 };
            snprintf( getMessage, sizeof( getMessage ),
                "[replace-live] FAssetProxyHandle.Get failed called=%s prefab=%p",
                getCalled ? "true" : "false", prefab );
            Log( getMessage );
            return;
        }

        g_pelicaPrefab = prefab;
        g_pelicaPrefabResolved = true;
        if ( api::gchandle_new )
            g_pelicaPrefabGcHandle = api::gchandle_new( prefab, false );

        char getMessage [ 256 ] = { 0 };
        snprintf( getMessage, sizeof( getMessage ),
            "[replace-live] FAssetProxyHandle.Get prefab=%p gcHandle=%u",
            prefab, g_pelicaPrefabGcHandle );
        Log( getMessage );
        TryInstantiateLoadedPelica( );
    }

    static bool SafeAssetProxyHasError( void * proxy, bool & hasError ) {
        if ( !proxy || !g_assetProxyHasError )
            return false;
        __try {
            hasError = g_assetProxyHasError( proxy, nullptr );
            return true;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            hasError = false;
            return false;
        }
    }

    static void __fastcall AssetProxyFinishErrorHook( void * proxy,
        void * message, void * method ) {
        bool targetLoad =
            ( GetRedirectScopeDepth( ) & TLS_FLAG_TARGET_INTERNAL ) != 0;
        std::string finishMessage = targetLoad ? ReadString( message ) : "";

        if ( g_originalAssetProxyFinishError )
            g_originalAssetProxyFinishError( proxy, message, method );

        if ( !targetLoad )
            return;

        g_assetProxyFinishErrorObserved = true;
        int assetStatus = -1;
        void * bundleProxy = nullptr;
        void * assetErrorCode = nullptr;
        bool assetReadable = SafeReadAssetProxyState( proxy, assetStatus,
            bundleProxy, assetErrorCode );

        uint64_t bundleHash = 0;
        int bundleStatus = -1;
        void * assetBundle = nullptr;
        void * bundleErrorCode = nullptr;
        bool bundleReadable = SafeReadBundleProxyState( bundleProxy, bundleHash,
            bundleStatus, assetBundle, bundleErrorCode );

        std::string assetError = ReadString( assetErrorCode );
        std::string bundleError = ReadString( bundleErrorCode );
        char output [ 896 ] = { 0 };
        snprintf( output, sizeof( output ),
            "[bundle-diag] AssetProxy._FinishWithError message=\"%.260s\" "
            "assetStatus=%s(%d) assetError=\"%.180s\" bundle=%p "
            "bundleHash=%016llX bundleStatus=%s(%d) bundleAsset=%p "
            "bundleError=\"%.180s\" stateReadable=%s/%s",
            finishMessage.c_str( ), BundleLoadStatusName( assetStatus ),
            assetStatus, assetError.c_str( ), bundleProxy,
            static_cast< unsigned long long >( bundleHash ),
            BundleLoadStatusName( bundleStatus ), bundleStatus, assetBundle,
            bundleError.c_str( ), assetReadable ? "true" : "false",
            bundleReadable ? "true" : "false" );
        Log( output );
    }

    static bool __fastcall BundleProxyLoadSyncHook( void * proxy,
        void * method ) {
        bool targetLoad =
            ( GetRedirectScopeDepth( ) & TLS_FLAG_TARGET_INTERNAL ) != 0;
        bool loaded = g_originalBundleProxyLoadSync &&
            g_originalBundleProxyLoadSync( proxy, method );

        if ( !targetLoad )
            return loaded;

        ++g_bundleSyncCount;
        uint64_t hashName = 0;
        int status = -1;
        void * assetBundle = nullptr;
        void * errorCode = nullptr;
        bool readable = SafeReadBundleProxyState( proxy, hashName, status,
            assetBundle, errorCode );
        bool failed = !loaded || !readable || status == 3;
        if ( failed )
            ++g_bundleSyncFailureCount;

        if ( failed || hashName == PELICA_POSTMODEL_BUNDLE_HASH ) {
            std::string error = ReadString( errorCode );
            char output [ 512 ] = { 0 };
            snprintf( output, sizeof( output ),
                "[bundle-diag] BundleProxy.LoadSync kind=%s hash=%016llX "
                "result=%s status=%s(%d) assetBundle=%p error=\"%.240s\" "
                "stateReadable=%s",
                hashName == PELICA_POSTMODEL_BUNDLE_HASH ? "target" : "dependency",
                static_cast< unsigned long long >( hashName ),
                loaded ? "true" : "false", BundleLoadStatusName( status ),
                status, assetBundle, error.c_str( ), readable ? "true" : "false" );
            Log( output );
        }
        return loaded;
    }

    static void * __fastcall BundleProxyGetFilePathHook( void * proxy,
        void * bundleName, void * method ) {
        void * fullPath = g_originalBundleProxyGetFilePath
            ? g_originalBundleProxyGetFilePath( proxy, bundleName, method )
            : nullptr;
        bool targetLoad =
            ( GetRedirectScopeDepth( ) & TLS_FLAG_TARGET_INTERNAL ) != 0;
        if ( !targetLoad )
            return fullPath;

        uint64_t hashName = 0;
        int status = -1;
        void * assetBundle = nullptr;
        void * errorCode = nullptr;
        bool readable = SafeReadBundleProxyState( proxy, hashName, status,
            assetBundle, errorCode );
        if ( readable && hashName == PELICA_POSTMODEL_BUNDLE_HASH ) {
            g_mainBundlePathObserved = true;
            std::string name = ReadString( bundleName );
            std::string path = ReadString( fullPath );
            char output [ 768 ] = { 0 };
            snprintf( output, sizeof( output ),
                "[bundle-diag] target bundle path name=\"%.260s\" fullPath=\"%.420s\"",
                name.c_str( ), path.c_str( ) );
            Log( output );
        }
        return fullPath;
    }

    static void * __fastcall BundleProxyLoadFileHook( void * filePath,
        void * method ) {
        bool targetLoad =
            ( GetRedirectScopeDepth( ) & TLS_FLAG_TARGET_INTERNAL ) != 0;
        std::string path = targetLoad ? ReadString( filePath ) : "";
        void * assetBundle = g_originalBundleProxyLoadFile
            ? g_originalBundleProxyLoadFile( filePath, method )
            : nullptr;

        if ( !targetLoad )
            return assetBundle;

        ++g_bundleFileLoadCount;
        bool failed = assetBundle == nullptr;
        if ( failed )
            ++g_bundleFileFailureCount;
        bool mainBundle = path.find( "ca788b482b3c7b99d02c5057.ab" ) !=
            std::string::npos;
        if ( failed || mainBundle ) {
            char output [ 768 ] = { 0 };
            snprintf( output, sizeof( output ),
                "[bundle-diag] AssetBundle.LoadFromFile kind=%s result=%p path=\"%.560s\"",
                mainBundle ? "target" : "dependency", assetBundle,
                path.c_str( ) );
            Log( output );
        }
        return assetBundle;
    }

    static bool __fastcall TryGetAssetInfoHashHook( void * manifest,
        uint64_t pathHash, void * assetInfo, void * method ) {
        bool found = g_originalTryGetAssetInfoHash &&
            g_originalTryGetAssetInfoHash( manifest, pathHash, assetInfo, method );
        if ( pathHash == PELICA_POSTMODEL_PATH_HASH ) {
            bool loadStage =
                ( GetRedirectScopeDepth( ) & TLS_FLAG_TARGET_INTERNAL ) != 0;
            if ( loadStage )
                g_tryGetAssetInfoLoadObserved = true;
            Log( std::string( "[load-diag] TryGetAssetInfoFromPath source=" ) +
                ( loadStage ? "load" : "probe" ) +
                " result=" + ( found ? "true" : "false" ) );
        }
        return found;
    }

    static void * __fastcall BundleLoaderLoadAssetHook( void * manager,
        void * assetInfo, void * type, bool isAsync, int priority,
        void * method ) {
        bool targetLoad =
            ( GetRedirectScopeDepth( ) & TLS_FLAG_TARGET_INTERNAL ) != 0;
        void * proxy = g_originalBundleLoaderLoadAsset
            ? g_originalBundleLoaderLoadAsset( manager, assetInfo, type, isAsync,
                priority, method )
            : nullptr;

        if ( targetLoad ) {
            g_bundleLoaderObserved = true;
            bool hasError = false;
            bool errorCalled = SafeAssetProxyHasError( proxy, hasError );
            g_bundleLoaderReturnedProxy = proxy != nullptr;
            if ( errorCalled )
                g_bundleLoaderProxyHasError = hasError;
            char message [ 224 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[load-diag] BundleLoader.Manager.LoadAsset proxy=%p hasError=%s "
                "isAsync=%s priority=%d",
                proxy, BoolResult( errorCalled, hasError ),
                isAsync ? "true" : "false", priority );
            Log( message );
        }
        return proxy;
    }

    static void * __fastcall LoadAssetInternalHashHook( void * manager,
        uint64_t pathHash, void * type, uint8_t category, bool isAsync,
        int priority, void * method ) {
        bool targetLoad = pathHash == PELICA_POSTMODEL_PATH_HASH &&
            category == ROOT_CATEGORY_MAIN;
        uintptr_t previousState = GetRedirectScopeDepth( );
        if ( targetLoad )
            SetRedirectScopeDepth( previousState | TLS_FLAG_TARGET_INTERNAL );

        void * proxy = g_originalLoadAssetInternalHash
            ? g_originalLoadAssetInternalHash( manager, pathHash, type, category,
                isAsync, priority, method )
            : nullptr;

        if ( targetLoad ) {
            g_loadAssetInternalObserved = true;
            g_loadAssetInternalReturnedProxy = proxy != nullptr;
            bool hasError = false;
            bool errorCalled = SafeAssetProxyHasError( proxy, hasError );
            char message [ 224 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[load-diag] BundleResourceManager._LoadAssetInternal proxy=%p "
                "hasError=%s isAsync=%s priority=%d",
                proxy, BoolResult( errorCalled, hasError ),
                isAsync ? "true" : "false", priority );
            Log( message );
            SetRedirectScopeDepth( previousState );
        }
        return proxy;
    }

    static void * __fastcall LoadUntrackedHashHook( void * result,
        void * manager, uint64_t pathHash, void * type, uint8_t category,
        void * method ) {
        void * returned = g_originalLoadUntrackedHash
            ? g_originalLoadUntrackedHash( result, manager, pathHash, type,
                category, method )
            : result;

        if ( pathHash == PELICA_POSTMODEL_PATH_HASH &&
            category == ROOT_CATEGORY_MAIN ) {
            g_loadUntrackedObserved = true;
            uint64_t raw0 = 0;
            uint64_t raw1 = 0;
            uint32_t raw2 = 0;
            bool readable = ReadRawHandle( result, raw0, raw1, raw2 );
            bool nonZero = readable && ( raw0 != 0 || raw1 != 0 || raw2 != 0 );
            bool directValid = false;
            bool directValidCalled = false;
            if ( readable && g_untrackedHandleIsValid ) {
                __try {
                    directValid = g_untrackedHandleIsValid( result, nullptr );
                    directValidCalled = true;
                }
                __except ( EXCEPTION_EXECUTE_HANDLER ) {
                    directValid = false;
                    directValidCalled = false;
                }
            }

            g_loadUntrackedReturnedHandle = nonZero;
            if ( directValidCalled )
                g_loadUntrackedDirectIsValid = directValid;

            char message [ 288 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[load-diag] LoadUntracked raw=%016llX:%016llX:%08X "
                "nonZero=%s directIsValid=%s",
                static_cast< unsigned long long >( raw0 ),
                static_cast< unsigned long long >( raw1 ), raw2,
                nonZero ? "true" : "false",
                BoolResult( directValidCalled, directValid ) );
            Log( message );

            uintptr_t state = GetRedirectScopeDepth( );
            SetRedirectScopeDepth( state | TLS_FLAG_EXPECT_HANDLE_GATE );
        }
        return returned;
    }

    static bool __fastcall UntrackedHandleGateHook( void * handle,
        void * method ) {
        uintptr_t state = GetRedirectScopeDepth( );
        bool expected = ( state & TLS_FLAG_EXPECT_HANDLE_GATE ) != 0;
        bool valid = g_originalUntrackedHandleGate &&
            g_originalUntrackedHandleGate( handle, method );
        if ( expected ) {
            g_untrackedHandleGateObserved = true;
            g_untrackedHandleGatePassed = valid;
            Log( valid
                ? "[load-diag] ResourceManager untracked-handle gate=true"
                : "[load-diag] ResourceManager untracked-handle gate=false (outer Load will zero the handle)" );
            SetRedirectScopeDepth( state & ~TLS_FLAG_EXPECT_HANDLE_GATE );
        }
        return valid;
    }

    static void * __fastcall I18NLoadGameObjectHook( void * result,
        uint64_t pathHash, uint8_t category, void * method ) {
        uint8_t effectiveCategory = category;
        uintptr_t scopeState = GetRedirectScopeDepth( );
        bool redirectScope = ( scopeState & 1U ) != 0;
        bool captureLoginLoad =
            ( scopeState & TLS_FLAG_CAPTURE_LOGIN_LOAD ) != 0;
        bool targetHash = pathHash == PELICA_POSTMODEL_PATH_HASH;
        bool categoryBridged = false;

        if ( captureLoginLoad && category == ROOT_CATEGORY_INITIAL ) {
            g_i18nLoadGameObjectMethod = method;
            g_loginLoadThreadId = GetCurrentThreadId( );
            if ( !g_loginLoadContextCaptured.exchange( true ) ) {
                char message [ 224 ] = { 0 };
                snprintf( message, sizeof( message ),
                    "[deferred-load] captured login Load<GameObject> context method=%p thread=%lu",
                    method, static_cast< unsigned long >(
                        GetCurrentThreadId( ) ) );
                Log( message );
            }
        }

        if ( g_redirectEnabled.load( ) &&
            category == ROOT_CATEGORY_INITIAL &&
            ( redirectScope || targetHash ) ) {
            effectiveCategory = ROOT_CATEGORY_MAIN;
            categoryBridged = true;
            g_mainCategoryHit = true;
            if ( redirectScope )
                SetRedirectScopeDepth( scopeState + 1 );

            char message [ 192 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[redirect] I18N Load<GameObject> category Initial(1) -> Main(0), pathHash=%016llX%s",
                static_cast< unsigned long long >( pathHash ),
                targetHash ? "" : " (redirect scope)" );
            Log( message );
        }

        if ( categoryBridged && targetHash )
            RunMainResourceDiagnostics( );

        if ( !g_originalI18NLoadGameObject )
            return result;

        void * returned = g_originalI18NLoadGameObject( result, pathHash,
            effectiveCategory, method );

        if ( categoryBridged ) {
            uint64_t raw0 = 0;
            uint64_t raw1 = 0;
            uint32_t raw2 = 0;
            bool readable = false;
            __try {
                memcpy( &raw0, result, sizeof( raw0 ) );
                memcpy( &raw1, reinterpret_cast< const uint8_t * >( result ) + 8,
                    sizeof( raw1 ) );
                memcpy( &raw2, reinterpret_cast< const uint8_t * >( result ) + 16,
                    sizeof( raw2 ) );
                readable = true;
            }
            __except ( EXCEPTION_EXECUTE_HANDLER ) {
                readable = false;
            }

            if ( readable ) {
                bool nonZero = raw0 != 0 || raw1 != 0 || raw2 != 0;
                g_mainLoadReturnedHandle = nonZero;
                char message [ 224 ] = { 0 };
                snprintf( message, sizeof( message ),
                    "[redirect] Main load returned %s handle raw=%016llX:%016llX:%08X",
                    nonZero ? "non-zero" : "zero",
                    static_cast< unsigned long long >( raw0 ),
                    static_cast< unsigned long long >( raw1 ), raw2 );
                Log( message );
            }
            else {
                Log( "[redirect] Main load handle buffer was unreadable" );
            }
        }
        return returned;
    }

    static void * __fastcall CachedLoadGameObjectHook( void * result,
        void * loader, void * path, void * method ) {
        if ( !g_originalCachedLoadGameObject )
            return result;

        bool captureLoginLoad = g_redirectEnabled.load( ) &&
            RawStringEqualsAscii( path, LOGIN_SK_ACTOR_F );
        uintptr_t previousState = GetRedirectScopeDepth( );
        bool scopeSet = !captureLoginLoad || SetRedirectScopeDepth(
            previousState | TLS_FLAG_CAPTURE_LOGIN_LOAD );
        if ( captureLoginLoad ) {
            g_redirectHit = true;
            g_deferredLoadPending = true;
            Log( "[deferred-load] female login actor preserved; Pelica load queued until Main initialization" );
            if ( !scopeSet )
                Log( "[deferred-load] TLS context capture failed; original female actor will still load" );
        }

        void * returned = g_originalCachedLoadGameObject(
            result, loader, path, method );

        if ( captureLoginLoad && scopeSet )
            SetRedirectScopeDepth( previousState );
        return returned;
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
            void * image = assemblies [ i ] ? api::assembly_get_image( assemblies [ i ] ) : nullptr;
            const char * name = image ? api::image_get_name( image ) : nullptr;
            if ( name && strcmp( name, imageName ) == 0 )
                return image;
        }
        return nullptr;
    }

    static void * FindClass( const char * namespaze, const char * name ) {
        if ( !api::get_domain || !api::get_assemblies || !api::class_from_name )
            return nullptr;

        if ( namespaze && strncmp( namespaze, "UnityEngine", 11 ) == 0 ) {
            const char * unityImages [ ] = {
                "UnityEngine.CoreModule.dll",
                "UnityEngine.AnimationModule.dll",
                "UnityEngine.GameObjectModule.dll",
                "UnityEngine.dll",
                nullptr
            };
            for ( int i = 0; unityImages [ i ]; ++i ) {
                void * image = FindImageByName( unityImages [ i ] );
                if ( !image )
                    continue;
                void * klass = api::class_from_name( image, namespaze, name );
                if ( klass ) {
                    Log( std::string( "[replace] resolved " ) + namespaze + "." +
                        name + " from " + unityImages [ i ] );
                    return klass;
                }
            }
        }

        void * domain = api::get_domain( );
        size_t count = 0;
        void ** assemblies = domain ? api::get_assemblies( domain, &count ) : nullptr;
        if ( !assemblies )
            return nullptr;

        for ( size_t i = 0; i < count; ++i ) {
            void * image = assemblies [ i ] ? api::assembly_get_image( assemblies [ i ] ) : nullptr;
            if ( !image )
                continue;
            void * klass = api::class_from_name( image, namespaze, name );
            if ( klass )
                return klass;
        }
        return nullptr;
    }

    static bool Contains( const char * text, const char * expected ) {
        return !expected || ( text && strstr( text, expected ) != nullptr );
    }

    static void * FindMethod( void * klass, const char * name, int argc,
        const char * firstParamContains = nullptr,
        const char * returnContains = nullptr,
        bool requireNonGeneric = false ) {
        if ( !klass || !api::class_get_methods || !api::method_get_name ||
            !api::method_get_param_count )
            return nullptr;

        void * iter = nullptr;
        while ( void * method = api::class_get_methods( klass, &iter ) ) {
            const char * methodName = api::method_get_name( method );
            if ( !methodName || strcmp( methodName, name ) != 0 )
                continue;
            if ( static_cast< int >( api::method_get_param_count( method ) ) != argc )
                continue;
            if ( requireNonGeneric && api::method_is_generic && api::method_is_generic( method ) )
                continue;

            if ( firstParamContains ) {
                const void * param = argc > 0 && api::method_get_param
                    ? api::method_get_param( method, 0 ) : nullptr;
                const char * paramName = param && api::type_get_name
                    ? api::type_get_name( const_cast< void * >( param ) ) : nullptr;
                if ( !Contains( paramName, firstParamContains ) )
                    continue;
            }

            if ( returnContains ) {
                void * ret = api::method_get_return_type
                    ? api::method_get_return_type( method ) : nullptr;
                const char * retName = ret && api::type_get_name
                    ? api::type_get_name( ret ) : nullptr;
                if ( !Contains( retName, returnContains ) )
                    continue;
            }
            return method;
        }
        return nullptr;
    }

    static void * Invoke( void * method, void * obj, void ** params,
        const char * label ) {
        if ( !method || !api::runtime_invoke ) {
            Log( std::string( "[replace] unresolved method: " ) + label );
            return nullptr;
        }

        void * exception = nullptr;
        void * result = nullptr;
        __try {
            result = api::runtime_invoke( method, obj, params, &exception );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            Log( std::string( "[replace] native exception in " ) + label );
            return nullptr;
        }
        if ( exception ) {
            Log( std::string( "[replace] managed exception in " ) + label );
            return nullptr;
        }
        return result;
    }

    static std::string ReadString( void * str ) {
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

        int size = WideCharToMultiByte( CP_UTF8, 0, chars, len, nullptr, 0,
            nullptr, nullptr );
        if ( size <= 0 || size > ( 1 << 20 ) )
            return "";
        std::string out( static_cast< size_t >( size ), '\0' );
        WideCharToMultiByte( CP_UTF8, 0, chars, len, out.data( ), size,
            nullptr, nullptr );
        return out;
    }

    static std::string ObjectName( void * obj ) {
        return ReadString( Invoke( mObjectGetName, obj, nullptr, "Object.get_name" ) );
    }

    static std::string ObjectClassName( void * obj ) {
        if ( !obj || !api::object_get_class || !api::class_get_name )
            return "";
        void * klass = api::object_get_class( obj );
        const char * name = klass ? api::class_get_name( klass ) : nullptr;
        return name ? name : "";
    }

    template < typename T > static bool Unbox( void * boxed, T & value ) {
        if ( !boxed || !api::object_unbox )
            return false;
        __try {
            T * ptr = reinterpret_cast< T * >( api::object_unbox( boxed ) );
            if ( !ptr )
                return false;
            value = *ptr;
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return false;
        }
        return true;
    }

    static int ArrayLength( void * array ) {
        if ( !array || !api::array_length )
            return 0;
        __try {
            return api::array_length( array );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return 0;
        }
    }

    static void * ArrayElement( void * array, int index ) {
        if ( !array )
            return nullptr;
        __try {
            void ** vector = reinterpret_cast< void ** >(
                reinterpret_cast< char * >( array ) + 32 );
            return vector [ index ];
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            return nullptr;
        }
    }

    static void SetArrayElement( void * array, int index, void * value ) {
        void ** vector = reinterpret_cast< void ** >(
            reinterpret_cast< char * >( array ) + 32 );
        void ** slot = &vector [ index ];
        if ( api::gc_wbarrier_set_field )
            api::gc_wbarrier_set_field( array, slot, value );
        else
            *slot = value;
    }

    static void * TypeObject( void * klass ) {
        if ( !klass || !api::class_get_type || !api::type_get_object )
            return nullptr;
        void * type = api::class_get_type( klass );
        return type ? api::type_get_object( type ) : nullptr;
    }

    static void * GetComponentsInChildren( void * root, void * componentClass ) {
        void * typeObject = TypeObject( componentClass );
        if ( !root || !typeObject || !g_gameObjectGetComponentsInChildren )
            return nullptr;

        void * components = nullptr;
        __try {
            components = g_gameObjectGetComponentsInChildren(
                root, typeObject, true, nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            Log( "[replace] native exception in direct "
                "GameObject.GetComponentsInChildren" );
            return nullptr;
        }
        if ( !components )
            Log( "[replace] direct GameObject.GetComponentsInChildren returned null" );
        return components;
    }

    static void * GetTransform( void * gameObject ) {
        return Invoke( mGameObjectGetTransform, gameObject, nullptr,
            "GameObject.get_transform" );
    }

    static void * GetParent( void * transform ) {
        return Invoke( mTransformGetParent, transform, nullptr,
            "Transform.get_parent" );
    }

    static std::string RelativePath( void * transform, void * root ) {
        if ( !transform )
            return "";
        if ( transform == root )
            return "";

        std::vector< std::string > parts;
        void * current = transform;
        for ( int depth = 0; current && depth < 256; ++depth ) {
            if ( current == root )
                break;
            parts.push_back( ObjectName( current ) );
            current = GetParent( current );
        }
        if ( current != root )
            return ObjectName( transform );

        std::reverse( parts.begin( ), parts.end( ) );
        std::string result;
        for ( size_t i = 0; i < parts.size( ); ++i ) {
            if ( i )
                result += '/';
            result += parts [ i ];
        }
        return result;
    }

    static bool EndsWith( const std::string & value, const std::string & suffix ) {
        return suffix.size( ) <= value.size( ) &&
            value.compare( value.size( ) - suffix.size( ), suffix.size( ), suffix ) == 0;
    }

    static bool SetEnabled( void * renderer, bool enabled ) {
        if ( !renderer || !g_rendererSetEnabled )
            return false;
        __try {
            g_rendererSetEnabled( renderer, enabled, nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            Log( "[replace] native exception in direct Renderer.set_enabled" );
            return false;
        }
        return true;
    }

    static bool SetBones( void * renderer, void * bones ) {
        if ( !renderer || !bones || !g_skinnedMeshRendererSetBones )
            return false;
        __try {
            g_skinnedMeshRendererSetBones( renderer, bones, nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            Log( "[replace] native exception in direct "
                "SkinnedMeshRenderer.set_bones" );
            return false;
        }
        return true;
    }

    static bool SetRootBone( void * renderer, void * rootBone ) {
        if ( !renderer || !g_skinnedMeshRendererSetRootBone )
            return false;
        __try {
            g_skinnedMeshRendererSetRootBone( renderer, rootBone, nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            Log( "[replace] native exception in direct "
                "SkinnedMeshRenderer.set_rootBone" );
            return false;
        }
        return true;
    }

    static bool SetActive( void * gameObject, bool active ) {
        if ( !gameObject || !g_gameObjectSetActive )
            return false;
        __try {
            g_gameObjectSetActive( gameObject, active, nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            Log( "[replace] native exception in direct GameObject.SetActive" );
            return false;
        }
        return true;
    }

    static bool CopyLayerToHierarchy( void * targetRoot, void * replacementRoot ) {
        int layer = 0;
        if ( !Unbox( Invoke( mGameObjectGetLayer, targetRoot, nullptr,
            "GameObject.get_layer" ), layer ) )
            return false;

        void * transforms = GetComponentsInChildren( replacementRoot, clsTransform );
        int count = ArrayLength( transforms );
        for ( int i = 0; i < count; ++i ) {
            void * transform = ArrayElement( transforms, i );
            void * gameObject = Invoke( mComponentGetGameObject, transform, nullptr,
                "Component.get_gameObject" );
            if ( !gameObject )
                continue;
            void * params [ 1 ] = { &layer };
            Invoke( mGameObjectSetLayer, gameObject, params, "GameObject.set_layer" );
        }

        Log( "[replace] copied silhouette layer " + std::to_string( layer ) +
            " to " + std::to_string( count ) + " replacement objects" );
        return true;
    }

    static void DestroyObject( void * obj ) {
        if ( !obj )
            return;
        void * params [ 1 ] = { &obj };
        Invoke( mObjectDestroy, nullptr, params, "Object.Destroy" );
    }

    static bool CopyLocalTransform( void * source, void * target ) {
        if ( !source || !target )
            return false;

        Vector3 position { };
        Vector3 scale { 1.0f, 1.0f, 1.0f };
        Quaternion rotation { 0.0f, 0.0f, 0.0f, 1.0f };
        if ( !Unbox( Invoke( mGetLocalPosition, target, nullptr,
            "Transform.get_localPosition" ), position ) )
            return false;
        if ( !Unbox( Invoke( mGetLocalRotation, target, nullptr,
            "Transform.get_localRotation" ), rotation ) )
            return false;
        if ( !Unbox( Invoke( mGetLocalScale, target, nullptr,
            "Transform.get_localScale" ), scale ) )
            return false;

        void * parent = GetParent( target );
        if ( !g_transformSetParent )
            return false;
        __try {
            g_transformSetParent( source, parent, false, nullptr );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            Log( "[replace] native exception in direct Transform.SetParent" );
            return false;
        }

        void * positionParams [ 1 ] = { &position };
        void * rotationParams [ 1 ] = { &rotation };
        void * scaleParams [ 1 ] = { &scale };
        Invoke( mSetLocalPosition, source, positionParams, "Transform.set_localPosition" );
        Invoke( mSetLocalRotation, source, rotationParams, "Transform.set_localRotation" );
        Invoke( mSetLocalScale, source, scaleParams, "Transform.set_localScale" );
        return true;
    }

    static void * FindTargetRoot( ) {
        if ( mGameObjectFind && api::string_new ) {
            void * targetName = api::string_new( "SK_actor_female(Clone)" );
            void * findParams [ 1 ] = { &targetName };
            void * direct = Invoke( mGameObjectFind, nullptr, findParams,
                "GameObject.Find" );
            if ( direct ) {
                Log( "[replace] target found through GameObject.Find" );
                return direct;
            }
        }

        Log( "[replace] direct target lookup missed; trying global enumeration" );
        void * typeObject = TypeObject( clsGameObject );
        void * params [ 1 ] = { &typeObject };
        void * objects = Invoke( mFindObjectsOfTypeAll, nullptr, params,
            "Resources.FindObjectsOfTypeAll" );
        int count = ArrayLength( objects );
        for ( int i = 0; i < count; ++i ) {
            void * obj = ArrayElement( objects, i );
            std::string name = ObjectName( obj );
            if ( name == "SK_actor_female(Clone)" )
                return obj;
        }
        return nullptr;
    }

    static bool CheckResourceExists( const char * path ) {
        if ( !mResourceCheckExists || !api::string_new )
            return true;
        void * pathString = api::string_new( path );
        uint8_t categoryMain = 0;
        void * params [ 2 ] = { &pathString, &categoryMain };
        bool exists = false;
        return Unbox( Invoke( mResourceCheckExists, nullptr, params,
            "ResourceManager.CheckExists" ), exists ) && exists;
    }

    static bool EnsureLoader( ) {
        if ( g_loader )
            return true;
        if ( !api::object_new )
            return false;

        g_loader = api::object_new( clsSimpleAssetLoader );
        if ( !g_loader )
            return false;

        uint8_t channelRes = 1;
        uint8_t categoryMain = 0;
        uint32_t priorityDefault = 2;
        void * params [ 3 ] = { &channelRes, &categoryMain, &priorityDefault };
        Invoke( mLoaderCtor, g_loader, params, "SimpleAssetLoader.ctor" );
        if ( api::gchandle_new )
            g_loaderGcHandle = api::gchandle_new( g_loader, false );
        return true;
    }

    static void * InstantiateTargetModel( ) {
        if ( !EnsureLoader( ) || !api::string_new )
            return nullptr;

        void * pathString = api::string_new( TARGET_MODEL_PATH );
        void * hashParams [ 1 ] = { &pathString };
        void * hashBox = Invoke( mCreateStringPathHash, nullptr, hashParams,
            "StringPathHash.CreateStringPathHash" );
        int64_t pathHash = 0;
        if ( !Unbox( hashBox, pathHash ) ) {
            Log( "[replace] failed to create StringPathHash" );
            return nullptr;
        }

        bool useBeyondInstantiate = true;
        void * instantiateParams [ 2 ] = { &pathHash, &useBeyondInstantiate };
        void * handleBox = Invoke( mInstantiatePrefab, g_loader, instantiateParams,
            "SimpleAssetLoader.InstantiatePrefab" );
        if ( !handleBox )
            return nullptr;

        Invoke( mHandleLoadImmediate, handleBox, nullptr,
            "FAssetProxyLoaderHandle.LoadImmediate" );

        bool hasError = false;
        if ( Unbox( Invoke( mHandleGetHasError, handleBox, nullptr,
            "FAssetProxyLoaderHandle.get_hasError" ), hasError ) && hasError ) {
            std::string error = ReadString( Invoke( mHandleGetErrorCode, handleBox,
                nullptr, "FAssetProxyLoaderHandle.get_errorCode" ) );
            Log( "[replace] asset load error: " + error );
            return nullptr;
        }

        return Invoke( mHandleGet, handleBox, nullptr,
            "FAssetProxyLoaderHandle.Get" );
    }

    static void * MapBone( void * sourceBone, void * sourceRoot,
        const std::unordered_map< std::string, void * > & targetByPath,
        const std::unordered_map< std::string, std::vector< void * > > & targetByName ) {
        if ( !sourceBone )
            return nullptr;

        std::string sourcePath = RelativePath( sourceBone, sourceRoot );
        auto exact = targetByPath.find( sourcePath );
        if ( exact != targetByPath.end( ) )
            return exact->second;

        std::string sourceName = ObjectName( sourceBone );
        auto byName = targetByName.find( sourceName );
        if ( byName != targetByName.end( ) && byName->second.size( ) == 1 )
            return byName->second [ 0 ];

        for ( const auto & entry : targetByPath ) {
            if ( EndsWith( entry.first, "/" + sourcePath ) ||
                EndsWith( sourcePath, "/" + entry.first ) )
                return entry.second;
        }
        return nullptr;
    }

    static bool BindReplacement( void * targetRoot, void * replacementRoot ) {
        void * targetTransform = GetTransform( targetRoot );
        void * replacementTransform = GetTransform( replacementRoot );
        if ( !targetTransform || !replacementTransform )
            return false;

        if ( !CopyLocalTransform( replacementTransform, targetTransform ) )
            return false;
        if ( !CopyLayerToHierarchy( targetRoot, replacementRoot ) )
            Log( "[replace] warning: failed to copy silhouette layer" );

        void * targetTransforms = GetComponentsInChildren( targetRoot, clsTransform );
        void * sourceTransforms = GetComponentsInChildren( replacementRoot, clsTransform );
        void * sourceRenderers = GetComponentsInChildren(
            replacementRoot, clsSkinnedMeshRenderer );
        if ( !targetTransforms || !sourceTransforms || !sourceRenderers )
            return false;

        std::unordered_map< std::string, void * > targetByPath;
        std::unordered_map< std::string, std::vector< void * > > targetByName;
        int targetTransformCount = ArrayLength( targetTransforms );
        for ( int i = 0; i < targetTransformCount; ++i ) {
            void * transform = ArrayElement( targetTransforms, i );
            if ( !transform )
                continue;
            targetByPath [ RelativePath( transform, targetTransform ) ] = transform;
            targetByName [ ObjectName( transform ) ].push_back( transform );
        }

        std::vector< RendererBinding > bindings;
        std::unordered_set< void * > mappedSourceBones;
        std::unordered_set< void * > mappedTargetBones;
        int totalBones = 0;
        int mappedBones = 0;
        int fullyMappedRenderers = 0;
        int partiallyMappedRenderers = 0;
        int unmappedRenderers = 0;
        int mappedRootBones = 0;
        int rendererCount = ArrayLength( sourceRenderers );
        for ( int i = 0; i < rendererCount; ++i ) {
            void * renderer = ArrayElement( sourceRenderers, i );
            void * bonesArray = Invoke( mSkinnedGetBones, renderer, nullptr,
                "SkinnedMeshRenderer.get_bones" );
            int boneCount = ArrayLength( bonesArray );
            if ( boneCount <= 0 )
                continue;

            RendererBinding binding;
            binding.renderer = renderer;
            binding.bones.reserve( static_cast< size_t >( boneCount ) );
            for ( int boneIndex = 0; boneIndex < boneCount; ++boneIndex ) {
                void * sourceBone = ArrayElement( bonesArray, boneIndex );
                void * mapped = MapBone( sourceBone, replacementTransform,
                    targetByPath, targetByName );
                if ( mapped ) {
                    ++binding.mapped;
                    ++mappedBones;
                    mappedSourceBones.insert( sourceBone );
                    mappedTargetBones.insert( mapped );
                    binding.bones.push_back( mapped );
                }
                else {
                    binding.bones.push_back( sourceBone );
                }
                ++totalBones;
            }

            void * sourceRootBone = Invoke( mSkinnedGetRootBone, renderer, nullptr,
                "SkinnedMeshRenderer.get_rootBone" );
            binding.rootBone = MapBone( sourceRootBone, replacementTransform,
                targetByPath, targetByName );
            if ( binding.rootBone )
                ++mappedRootBones;
            else
                binding.rootBone = sourceRootBone;

            if ( binding.mapped == boneCount )
                ++fullyMappedRenderers;
            else if ( binding.mapped > 0 )
                ++partiallyMappedRenderers;
            else
                ++unmappedRenderers;
            bindings.push_back( std::move( binding ) );
        }

        double coverage = totalBones > 0
            ? static_cast< double >( mappedBones ) / totalBones : 0.0;
        char coverageLog [ 192 ];
        sprintf_s( coverageLog,
            "[replace] skeleton mapping: %d/%d bones (%.1f%%), %zu renderers",
            mappedBones, totalBones, coverage * 100.0, bindings.size( ) );
        Log( coverageLog );

        char rendererLog [ 256 ];
        sprintf_s( rendererLog,
            "[replace] renderer mapping: full=%d partial=%d zero=%d, "
            "rootBones=%d/%zu, uniqueBones=%zu source -> %zu target",
            fullyMappedRenderers, partiallyMappedRenderers, unmappedRenderers,
            mappedRootBones, bindings.size( ), mappedSourceBones.size( ),
            mappedTargetBones.size( ) );
        Log( rendererLog );

        if ( mappedTargetBones.size( ) < MIN_UNIQUE_MAPPED_BONES ||
            coverage < MIN_SKELETON_MAPPING_COVERAGE ) {
            char rejection [ 256 ];
            sprintf_s( rejection,
                "[replace] mapping coverage too low (requires %.0f%% and %zu "
                "unique target bones); original model retained",
                MIN_SKELETON_MAPPING_COVERAGE * 100.0,
                MIN_UNIQUE_MAPPED_BONES );
            Log( rejection );
            return false;
        }

        for ( auto & binding : bindings ) {
            void * newBones = api::array_new( clsTransform, binding.bones.size( ) );
            if ( !newBones )
                return false;
            for ( int i = 0; i < static_cast< int >( binding.bones.size( ) ); ++i )
                SetArrayElement( newBones, i, binding.bones [ i ] );

            if ( !SetBones( binding.renderer, newBones ) )
                return false;

            if ( !SetRootBone( binding.renderer, binding.rootBone ) ||
                !SetEnabled( binding.renderer, true ) )
                return false;
        }

        void * targetRenderers = GetComponentsInChildren(
            targetRoot, clsSkinnedMeshRenderer );
        int targetRendererCount = ArrayLength( targetRenderers );
        g_disabledRenderers.clear( );
        for ( int i = 0; i < targetRendererCount; ++i ) {
            void * renderer = ArrayElement( targetRenderers, i );
            if ( !renderer )
                continue;
            if ( !SetEnabled( renderer, false ) ) {
                for ( void * disabled : g_disabledRenderers )
                    SetEnabled( disabled, true );
                g_disabledRenderers.clear( );
                return false;
            }
            g_disabledRenderers.push_back( renderer );
        }

        if ( !SetActive( replacementRoot, true ) ) {
            for ( void * disabled : g_disabledRenderers )
                SetEnabled( disabled, true );
            g_disabledRenderers.clear( );
            return false;
        }
        return true;
    }

    static bool ResolveSkeletonBindingApis( ) {
        if ( !api::initialized )
            api::init( );
        if ( !api::initialized )
            return false;

        clsObject = FindClass( "UnityEngine", "Object" );
        clsGameObject = FindClass( "UnityEngine", "GameObject" );
        clsComponent = FindClass( "UnityEngine", "Component" );
        clsTransform = FindClass( "UnityEngine", "Transform" );
        clsRenderer = FindClass( "UnityEngine", "Renderer" );
        clsSkinnedMeshRenderer = FindClass( "UnityEngine", "SkinnedMeshRenderer" );
        if ( !clsObject || !clsGameObject || !clsComponent || !clsTransform ||
            !clsRenderer || !clsSkinnedMeshRenderer )
            return false;

        mObjectGetName = FindMethod( clsObject, "get_name", 0 );
        mGameObjectGetTransform = FindMethod( clsGameObject, "get_transform", 0 );
        mGameObjectGetLayer = FindMethod( clsGameObject, "get_layer", 0 );
        mGameObjectSetLayer = FindMethod( clsGameObject, "set_layer", 1 );
        mGameObjectSetActive = FindMethod( clsGameObject, "SetActive", 1 );
        mComponentGetGameObject = FindMethod( clsComponent, "get_gameObject", 0 );
        mGetComponentsInChildren = FindMethod( clsGameObject,
            "GetComponentsInChildren", 2, "System.Type" );
        mTransformGetParent = FindMethod( clsTransform, "get_parent", 0 );
        mTransformSetParent = FindMethod( clsTransform, "SetParent", 2 );
        mGetLocalPosition = FindMethod( clsTransform, "get_localPosition", 0 );
        mSetLocalPosition = FindMethod( clsTransform, "set_localPosition", 1 );
        mGetLocalRotation = FindMethod( clsTransform, "get_localRotation", 0 );
        mSetLocalRotation = FindMethod( clsTransform, "set_localRotation", 1 );
        mGetLocalScale = FindMethod( clsTransform, "get_localScale", 0 );
        mSetLocalScale = FindMethod( clsTransform, "set_localScale", 1 );
        mRendererSetEnabled = FindMethod( clsRenderer, "set_enabled", 1 );
        mSkinnedGetBones = FindMethod( clsSkinnedMeshRenderer, "get_bones", 0 );
        mSkinnedSetBones = FindMethod( clsSkinnedMeshRenderer, "set_bones", 1 );
        mSkinnedGetRootBone = FindMethod( clsSkinnedMeshRenderer, "get_rootBone", 0 );
        mSkinnedSetRootBone = FindMethod( clsSkinnedMeshRenderer, "set_rootBone", 1 );

        bool ready = mObjectGetName && mGameObjectGetTransform &&
            mGameObjectGetLayer && mGameObjectSetLayer && mGameObjectSetActive &&
            mComponentGetGameObject && mGetComponentsInChildren &&
            mTransformGetParent && mTransformSetParent && mGetLocalPosition &&
            mSetLocalPosition && mGetLocalRotation && mSetLocalRotation &&
            mGetLocalScale && mSetLocalScale && mRendererSetEnabled &&
            mSkinnedGetBones && mSkinnedSetBones && mSkinnedGetRootBone &&
            mSkinnedSetRootBone && api::runtime_invoke && api::class_get_type &&
            api::type_get_object && api::object_unbox && api::array_length &&
            api::array_new && api::string_length_fn && api::string_chars;
        Log( ready
            ? "[skeleton-bind] Unity component APIs resolved on the captured game thread; hierarchy and renderer writes use guarded direct RVAs"
            : "[skeleton-bind] one or more Unity component APIs are unavailable" );
        return ready;
    }

    static bool ResolveApis( ) {
        clsObject = FindClass( "UnityEngine", "Object" );
        clsResources = FindClass( "UnityEngine", "Resources" );
        clsGameObject = FindClass( "UnityEngine", "GameObject" );
        clsComponent = FindClass( "UnityEngine", "Component" );
        clsTransform = FindClass( "UnityEngine", "Transform" );
        clsRenderer = FindClass( "UnityEngine", "Renderer" );
        clsSkinnedMeshRenderer = FindClass( "UnityEngine", "SkinnedMeshRenderer" );
        clsResourceManager = FindClass( "Beyond.Resource", "ResourceManager" );
        clsSimpleAssetLoader = FindClass( "Beyond.Resource", "SimpleAssetLoader" );
        clsStringPathHash = FindClass( "Beyond.Resource", "StringPathHash" );
        clsAssetLoaderHandle = FindClass( "Beyond.Resource", "FAssetProxyLoaderHandle" );

        if ( !clsObject || !clsResources || !clsGameObject || !clsComponent ||
            !clsTransform ||
            !clsRenderer || !clsSkinnedMeshRenderer || !clsResourceManager ||
            !clsSimpleAssetLoader || !clsStringPathHash || !clsAssetLoaderHandle )
            return false;

        mFindObjectsOfTypeAll = FindMethod( clsResources,
            "FindObjectsOfTypeAll", 1, "System.Type" );
        mObjectGetName = FindMethod( clsObject, "get_name", 0 );
        mObjectDestroy = FindMethod( clsObject, "Destroy", 1, "UnityEngine.Object" );
        mGameObjectFind = FindMethod( clsGameObject, "Find", 1, "System.String" );
        mGameObjectGetTransform = FindMethod( clsGameObject, "get_transform", 0 );
        mGameObjectGetLayer = FindMethod( clsGameObject, "get_layer", 0 );
        mGameObjectSetLayer = FindMethod( clsGameObject, "set_layer", 1 );
        mGameObjectSetActive = FindMethod( clsGameObject, "SetActive", 1 );
        mComponentGetGameObject = FindMethod( clsComponent, "get_gameObject", 0 );
        mGetComponentsInChildren = FindMethod( clsGameObject,
            "GetComponentsInChildren", 2, "System.Type" );
        mTransformGetParent = FindMethod( clsTransform, "get_parent", 0 );
        mTransformSetParent = FindMethod( clsTransform, "SetParent", 2 );
        mGetLocalPosition = FindMethod( clsTransform, "get_localPosition", 0 );
        mSetLocalPosition = FindMethod( clsTransform, "set_localPosition", 1 );
        mGetLocalRotation = FindMethod( clsTransform, "get_localRotation", 0 );
        mSetLocalRotation = FindMethod( clsTransform, "set_localRotation", 1 );
        mGetLocalScale = FindMethod( clsTransform, "get_localScale", 0 );
        mSetLocalScale = FindMethod( clsTransform, "set_localScale", 1 );
        mRendererSetEnabled = FindMethod( clsRenderer, "set_enabled", 1 );
        mSkinnedGetBones = FindMethod( clsSkinnedMeshRenderer, "get_bones", 0 );
        mSkinnedSetBones = FindMethod( clsSkinnedMeshRenderer, "set_bones", 1 );
        mSkinnedGetRootBone = FindMethod( clsSkinnedMeshRenderer, "get_rootBone", 0 );
        mSkinnedSetRootBone = FindMethod( clsSkinnedMeshRenderer, "set_rootBone", 1 );
        mResourceCheckExists = FindMethod( clsResourceManager, "CheckExists", 2,
            "System.String" );
        mLoaderCtor = FindMethod( clsSimpleAssetLoader, ".ctor", 3 );
        mCreateStringPathHash = FindMethod( clsStringPathHash,
            "CreateStringPathHash", 1, "System.String" );
        mInstantiatePrefab = FindMethod( clsSimpleAssetLoader,
            "InstantiatePrefab", 2, "Beyond.Resource.StringPathHash" );
        mHandleLoadImmediate = FindMethod( clsAssetLoaderHandle,
            "LoadImmediate", 0 );
        mHandleGet = FindMethod( clsAssetLoaderHandle, "Get", 0, nullptr,
            "UnityEngine.Object", true );
        mHandleGetHasError = FindMethod( clsAssetLoaderHandle, "get_hasError", 0 );
        mHandleGetErrorCode = FindMethod( clsAssetLoaderHandle, "get_errorCode", 0 );

        return mFindObjectsOfTypeAll && mObjectGetName && mObjectDestroy &&
            mGameObjectFind &&
            mGameObjectGetTransform && mGameObjectGetLayer &&
            mGameObjectSetLayer && mGameObjectSetActive &&
            mComponentGetGameObject &&
            mGetComponentsInChildren && mTransformGetParent && mTransformSetParent &&
            mGetLocalPosition && mSetLocalPosition && mGetLocalRotation &&
            mSetLocalRotation && mGetLocalScale && mSetLocalScale &&
            mRendererSetEnabled && mSkinnedGetBones && mSkinnedSetBones &&
            mSkinnedGetRootBone && mSkinnedSetRootBone && mLoaderCtor &&
            mCreateStringPathHash && mInstantiatePrefab && mHandleLoadImmediate &&
            mHandleGet && api::object_new && api::string_new && api::array_new;
    }

    static void ReplaceNow( ) {
        if ( g_replacementRoot ) {
            Log( "[replace] replacement is already active; press F10 to restore" );
            return;
        }

        Log( "[replace] starting Pelica replacement on Unity main thread" );
        if ( !ResolveApis( ) ) {
            Log( "[replace] required classes or methods were not resolved" );
            return;
        }

        void * targetRoot = FindTargetRoot( );
        if ( !targetRoot ) {
            Log( "[replace] SK_actor_female(Clone) not found; trigger F9 during silhouette" );
            return;
        }
        Log( "[replace] target=" + ObjectName( targetRoot ) );

        if ( !CheckResourceExists( TARGET_MODEL_PATH ) ) {
            Log( std::string( "[replace] resource does not exist: " ) +
                TARGET_MODEL_PATH );
            return;
        }
        Log( std::string( "[replace] loading " ) + TARGET_MODEL_PATH );

        void * replacementRoot = InstantiateTargetModel( );
        if ( !replacementRoot ) {
            Log( "[replace] prefab instantiate returned null" );
            return;
        }

        Log( "[replace] loaded object class=" + ObjectClassName( replacementRoot ) +
            " name=" + ObjectName( replacementRoot ) );
        SetActive( replacementRoot, false );

        if ( !BindReplacement( targetRoot, replacementRoot ) ) {
            DestroyObject( replacementRoot );
            Log( "[replace] replacement aborted and temporary object destroyed" );
            return;
        }

        g_targetRoot = targetRoot;
        g_replacementRoot = replacementRoot;
        g_replacementActive = true;
        Log( "[replace] Pelica renderers are active; original animation remains in control" );
    }

    static void RestoreNow( ) {
        if ( !g_replacementRoot ) {
            Log( "[replace] no active replacement" );
            return;
        }

        for ( void * renderer : g_disabledRenderers )
            SetEnabled( renderer, true );
        g_disabledRenderers.clear( );

        SetActive( g_replacementRoot, false );
        DestroyObject( g_replacementRoot );
        g_replacementRoot = nullptr;
        g_targetRoot = nullptr;
        g_replacementActive = false;
        Log( "[replace] original silhouette model restored" );
    }

} // namespace

namespace ModelReplacer {

    bool Initialize( HMODULE module ) {
        ( void ) module;
        if ( g_cachedLoadGameObjectTarget && g_i18nLoadGameObjectTarget &&
            g_resolveI18NPathHashTarget && g_initInitialPathHashTarget &&
            g_initMainPathHashTarget && g_loadUntrackedHashTarget &&
            g_loadAssetInternalHashTarget && g_bundleLoaderLoadAssetTarget &&
             g_tryGetAssetInfoHashTarget && g_untrackedHandleGateTarget &&
             g_assetProxyFinishErrorTarget && g_bundleProxyLoadSyncTarget &&
             g_bundleProxyGetFilePathTarget && g_bundleProxyLoadFileTarget &&
             g_loginSceneRootOnBindTarget &&
             g_instantiateGameObjectParentTarget )
            return true;

        HMODULE gameAssembly = GetModuleHandleA( "GameAssembly.dll" );
        if ( !gameAssembly ) {
            Log( "[redirect] GameAssembly.dll is not loaded" );
            return false;
        }
        g_gameAssemblyBase = reinterpret_cast< uintptr_t >( gameAssembly );
        g_redirectHit = false;
        g_mainCategoryHit = false;
        g_mainLoadReturnedHandle = false;
        g_initialPathHashEntered = false;
        g_initialPathHashCompleted = false;
        g_mainPathHashEntered = false;
        g_mainPathHashCompleted = false;
        g_mainDiagnosticsStarted = false;
        g_mainDiagnosticsCompleted = false;
        g_resourceManagerInitialized = false;
        g_resourceManagerAsyncIniting = false;
        g_resourceManagerUsingBundle = false;
        g_stringCheckExists = false;
        g_hashCheckExists = false;
        g_rawHashCheckExists = false;
        g_initialPathHashCompletedTick = 0;
        g_mainPathHashCompletedTick = 0;
        g_targetResolvedHash = 0;
        g_loadAssetInternalReturnedProxy = false;
        g_bundleLoaderReturnedProxy = false;
        g_bundleLoaderProxyHasError = false;
        g_loadUntrackedReturnedHandle = false;
        g_loadUntrackedDirectIsValid = false;
        g_untrackedHandleGatePassed = false;
        g_tryGetAssetInfoLoadObserved = false;
        g_loadAssetInternalObserved = false;
        g_bundleLoaderObserved = false;
        g_loadUntrackedObserved = false;
        g_untrackedHandleGateObserved = false;
        g_assetProxyFinishErrorObserved = false;
        g_mainBundlePathObserved = false;
        g_bundleFileLoadCount = 0;
        g_bundleFileFailureCount = 0;
        g_bundleSyncCount = 0;
        g_bundleSyncFailureCount = 0;
        g_deferredLoadPending = false;
        g_deferredLoadAttempted = false;
        g_loginLoadContextCaptured = false;
        g_i18nLoadGameObjectMethod = nullptr;
        g_loginLoadThreadId = 0;
        g_deferredHandleRaw0 = 0;
        g_deferredHandleRaw1 = 0;
        g_deferredHandleRaw2 = 0;
        g_loginBindObserved = false;
        g_loginInstantiateCaptured = false;
        g_pelicaPrefabResolved = false;
        g_pelicaInstantiateAttempted = false;
        g_pelicaInstantiateSucceeded = false;
        g_originalLoginActorDisabled = false;
        g_skeletonBindAttempted = false;
        g_skeletonBindSucceeded = false;
        g_originalRenderersDisabled = false;
        g_loginActorPrefab = nullptr;
        g_loginActorParent = nullptr;
        g_loginActorInstance = nullptr;
        g_loginInstantiateMethod = nullptr;
        g_pelicaPrefab = nullptr;
        g_pelicaInstance = nullptr;
        g_pelicaPrefabGcHandle = 0;
        g_redirectEnabled = true;
        g_il2cppThreadCurrent = reinterpret_cast< Il2CppThreadCurrentFn >(
            GetProcAddress( gameAssembly, "il2cpp_thread_current" ) );
        g_il2cppStringNew = reinterpret_cast< Il2CppStringNewFn >(
            GetProcAddress( gameAssembly, "il2cpp_string_new" ) );
        if ( !g_il2cppThreadCurrent || !g_il2cppStringNew ) {
            Log( "[redirect] required guarded IL2CPP exports are unavailable" );
            g_gameAssemblyBase = 0;
            return false;
        }
        g_cachedLoadGameObjectTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_CACHED_LOAD_GAME_OBJECT );
        g_i18nLoadGameObjectTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_I18N_LOAD_GAME_OBJECT );
        g_resolveI18NPathHashTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_I18N_RESOLVE_PATH_HASH );
        g_initInitialPathHashTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_INIT_INITIAL_PATH_HASH );
        g_initMainPathHashTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_INIT_MAIN_PATH_HASH );
        g_resourceManagerIsInitialized = reinterpret_cast< StaticBoolFn >(
            g_gameAssemblyBase + RVA_RESOURCE_MANAGER_IS_INITIALIZED );
        g_resourceManagerIsAsyncIniting = reinterpret_cast< StaticBoolFn >(
            g_gameAssemblyBase + RVA_RESOURCE_MANAGER_IS_ASYNC_INITING );
        g_resourceManagerGetUsingBundle = reinterpret_cast< StaticBoolFn >(
            g_gameAssemblyBase + RVA_RESOURCE_MANAGER_USING_BUNDLE );
        g_resourceManagerCheckString = reinterpret_cast< CheckStringFn >(
            g_gameAssemblyBase + RVA_RESOURCE_MANAGER_CHECK_STRING );
        g_resourceManagerCheckHash = reinterpret_cast< CheckHashFn >(
            g_gameAssemblyBase + RVA_RESOURCE_MANAGER_CHECK_HASH );
        g_resourceManagerRawCheckHash = reinterpret_cast< CheckHashFn >(
            g_gameAssemblyBase + RVA_RESOURCE_MANAGER_RAW_CHECK_HASH );
        g_loadUntrackedHashTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_BUNDLE_LOAD_UNTRACKED_HASH );
        g_loadAssetInternalHashTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_LOAD_ASSET_INTERNAL_HASH );
        g_bundleLoaderLoadAssetTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_BUNDLE_LOADER_LOAD_ASSET );
        g_tryGetAssetInfoHashTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_TRY_GET_ASSET_INFO_HASH );
        g_untrackedHandleGateTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_UNTRACKED_HANDLE_GATE );
        g_assetProxyFinishErrorTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_ASSET_PROXY_FINISH_ERROR );
        g_bundleProxyLoadSyncTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_BUNDLE_PROXY_LOAD_SYNC );
        g_bundleProxyGetFilePathTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_BUNDLE_PROXY_GET_FILE_PATH );
        g_bundleProxyLoadFileTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_BUNDLE_PROXY_LOAD_FILE );
        g_loginSceneRootOnBindTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_LOGIN_SCENE_ROOT_ON_BIND );
        g_instantiateGameObjectParentTarget = reinterpret_cast< void * >(
            g_gameAssemblyBase + RVA_INSTANTIATE_GAME_OBJECT_PARENT );
        g_assetProxyHandleGet = reinterpret_cast< AssetProxyHandleGetFn >(
            g_gameAssemblyBase + RVA_ASSET_PROXY_HANDLE_GET );
        g_gameObjectSetActive = reinterpret_cast< GameObjectSetActiveFn >(
            g_gameAssemblyBase + RVA_GAME_OBJECT_SET_ACTIVE );
        g_transformSetParent = reinterpret_cast< TransformSetParentFn >(
            g_gameAssemblyBase + RVA_TRANSFORM_SET_PARENT );
        g_gameObjectGetComponentsInChildren =
            reinterpret_cast< GameObjectGetComponentsInChildrenFn >(
                g_gameAssemblyBase + RVA_GAME_OBJECT_GET_COMPONENTS_IN_CHILDREN );
        g_rendererSetEnabled = reinterpret_cast< RendererSetEnabledFn >(
            g_gameAssemblyBase + RVA_RENDERER_SET_ENABLED );
        g_skinnedMeshRendererSetBones =
            reinterpret_cast< SkinnedMeshRendererSetBonesFn >(
                g_gameAssemblyBase + RVA_SKINNED_MESH_RENDERER_SET_BONES );
        g_skinnedMeshRendererSetRootBone =
            reinterpret_cast< SkinnedMeshRendererSetRootBoneFn >(
                g_gameAssemblyBase + RVA_SKINNED_MESH_RENDERER_SET_ROOT_BONE );
        g_untrackedHandleIsValid = reinterpret_cast< HandleGateFn >(
            g_gameAssemblyBase + RVA_UNTRACKED_HANDLE_IS_VALID );
        g_assetProxyHasError = reinterpret_cast< AssetProxyHasErrorFn >(
            g_gameAssemblyBase + RVA_ASSET_PROXY_HAS_ERROR );
        if ( g_redirectTlsIndex == TLS_OUT_OF_INDEXES )
            g_redirectTlsIndex = TlsAlloc( );
        if ( g_redirectTlsIndex == TLS_OUT_OF_INDEXES )
            Log( "[redirect] TlsAlloc failed; target path hash fallback remains active" );

        const uint8_t expectedPrologue [ ] = {
            0x48, 0x89, 0x5C, 0x24, 0x08,
            0x48, 0x89, 0x6C, 0x24, 0x10,
            0x48, 0x89, 0x74, 0x24, 0x18,
            0x57
        };
        bool cachedPrologueMatches = false;
        bool i18nPrologueMatches = false;
        bool resolvePrologueMatches = false;
        bool initialHashPrologueMatches = false;
        bool mainHashPrologueMatches = false;
        bool deepLoadProloguesMatch = false;
        bool loginAttachProloguesMatch = false;
        __try {
            cachedPrologueMatches = CheckPrologue( "CachedLoad<GameObject>",
                g_cachedLoadGameObjectTarget, expectedPrologue,
                sizeof( expectedPrologue ) );
            i18nPrologueMatches = CheckPrologue( "I18N Load<GameObject>",
                g_i18nLoadGameObjectTarget, expectedPrologue,
                sizeof( expectedPrologue ) );
            const uint8_t resolvePrologue [ ] = {
                0x48, 0x89, 0x4C, 0x24, 0x08, 0x53, 0x56
            };
            const uint8_t initPrologue [ ] = { 0x48, 0x83, 0xEC, 0x28 };
            resolvePrologueMatches = CheckPrologue( "I18N resolve path hash",
                g_resolveI18NPathHashTarget, resolvePrologue,
                sizeof( resolvePrologue ) );
            initialHashPrologueMatches = CheckPrologue( "InitInitPathHash",
                g_initInitialPathHashTarget, initPrologue,
                sizeof( initPrologue ) );
            mainHashPrologueMatches = CheckPrologue( "InitMainPathHash",
                g_initMainPathHashTarget, initPrologue,
                sizeof( initPrologue ) );
            const uint8_t loadUntrackedPrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x10, 0x4C, 0x89, 0x44
            };
            const uint8_t loadInternalPrologue [ ] = {
                0x48, 0x8B, 0xC4, 0x48, 0x89, 0x58, 0x08
            };
            const uint8_t bundleLoadPrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x18, 0x55, 0x57
            };
            const uint8_t assetInfoPrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x20, 0x48, 0x89, 0x4C
            };
            const uint8_t handleGatePrologue [ ] = {
                0x48, 0x89, 0x54, 0x24, 0x10, 0x48, 0x89, 0x4C
            };
            const uint8_t finishErrorPrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x08, 0x57
            };
            const uint8_t bundleSyncOrFilePrologue [ ] = {
                0x40, 0x53, 0x48, 0x83, 0xEC, 0x20
            };
            const uint8_t bundlePathPrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x08, 0x57
            };
            deepLoadProloguesMatch =
                CheckPrologue( "BundleResourceManager.LoadUntracked(hash)",
                    g_loadUntrackedHashTarget, loadUntrackedPrologue,
                    sizeof( loadUntrackedPrologue ) ) &
                CheckPrologue( "BundleResourceManager._LoadAssetInternal(hash)",
                    g_loadAssetInternalHashTarget, loadInternalPrologue,
                    sizeof( loadInternalPrologue ) ) &
                CheckPrologue( "BundleLoader.Manager.LoadAsset",
                    g_bundleLoaderLoadAssetTarget, bundleLoadPrologue,
                    sizeof( bundleLoadPrologue ) ) &
                CheckPrologue( "TryGetAssetInfoFromPath(hash)",
                    g_tryGetAssetInfoHashTarget, assetInfoPrologue,
                    sizeof( assetInfoPrologue ) ) &
                CheckPrologue( "FAssetProxyUntrackedHandle gate",
                    g_untrackedHandleGateTarget, handleGatePrologue,
                    sizeof( handleGatePrologue ) ) &
                CheckPrologue( "AssetProxy._FinishWithError",
                    g_assetProxyFinishErrorTarget, finishErrorPrologue,
                    sizeof( finishErrorPrologue ) ) &
                CheckPrologue( "BundleProxy.LoadSync",
                    g_bundleProxyLoadSyncTarget, bundleSyncOrFilePrologue,
                    sizeof( bundleSyncOrFilePrologue ) ) &
                CheckPrologue( "BundleProxy._GetBundleFileFullPath",
                    g_bundleProxyGetFilePathTarget, bundlePathPrologue,
                    sizeof( bundlePathPrologue ) ) &
                CheckPrologue( "BundleProxy._LoadAssetBundle",
                    g_bundleProxyLoadFileTarget, bundleSyncOrFilePrologue,
                    sizeof( bundleSyncOrFilePrologue ) );
            const uint8_t loginOnBindPrologue [ ] = {
                0x40, 0x57, 0x48, 0x83, 0xEC, 0x50
            };
            const uint8_t handleGetPrologue [ ] = {
                0x40, 0x53, 0x48, 0x83, 0xEC, 0x20
            };
            const uint8_t setActivePrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x08, 0x57
            };
            const uint8_t transformSetParentPrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x08,
                0x48, 0x89, 0x74, 0x24, 0x10, 0x57
            };
            const uint8_t getComponentsInChildrenPrologue [ ] = {
                0x48, 0x83, 0xEC, 0x48, 0x33, 0xC0, 0x41, 0xB1, 0x01
            };
            const uint8_t rendererSetterPrologue [ ] = {
                0x48, 0x89, 0x5C, 0x24, 0x08,
                0x57, 0x48, 0x83, 0xEC, 0x20
            };
            loginAttachProloguesMatch =
                CheckPrologue( "LoginSceneRoot.OnBindToManager",
                    g_loginSceneRootOnBindTarget, loginOnBindPrologue,
                    sizeof( loginOnBindPrologue ) ) &
                CheckPrologue( "Instantiate<GameObject>(original,parent,false)",
                    g_instantiateGameObjectParentTarget, expectedPrologue,
                    sizeof( expectedPrologue ) ) &
                CheckPrologue( "FAssetProxyHandle.Get",
                    reinterpret_cast< void * >( g_assetProxyHandleGet ),
                    handleGetPrologue, sizeof( handleGetPrologue ) ) &
                CheckPrologue( "GameObject.SetActive",
                    reinterpret_cast< void * >( g_gameObjectSetActive ),
                    setActivePrologue, sizeof( setActivePrologue ) ) &
                CheckPrologue( "Transform.SetParent(Transform,bool)",
                    reinterpret_cast< void * >( g_transformSetParent ),
                    transformSetParentPrologue,
                    sizeof( transformSetParentPrologue ) ) &
                CheckPrologue( "GameObject.GetComponentsInChildren(Type,bool)",
                    reinterpret_cast< void * >(
                        g_gameObjectGetComponentsInChildren ),
                    getComponentsInChildrenPrologue,
                    sizeof( getComponentsInChildrenPrologue ) ) &
                CheckPrologue( "Renderer.set_enabled",
                    reinterpret_cast< void * >( g_rendererSetEnabled ),
                    rendererSetterPrologue,
                    sizeof( rendererSetterPrologue ) ) &
                CheckPrologue( "SkinnedMeshRenderer.set_bones",
                    reinterpret_cast< void * >(
                        g_skinnedMeshRendererSetBones ),
                    rendererSetterPrologue,
                    sizeof( rendererSetterPrologue ) ) &
                CheckPrologue( "SkinnedMeshRenderer.set_rootBone",
                    reinterpret_cast< void * >(
                        g_skinnedMeshRendererSetRootBone ),
                    rendererSetterPrologue,
                    sizeof( rendererSetterPrologue ) );
        }
        __except ( EXCEPTION_EXECUTE_HANDLER ) {
            cachedPrologueMatches = false;
            i18nPrologueMatches = false;
            resolvePrologueMatches = false;
            initialHashPrologueMatches = false;
            mainHashPrologueMatches = false;
            deepLoadProloguesMatch = false;
            loginAttachProloguesMatch = false;
        }
        if ( !cachedPrologueMatches || !i18nPrologueMatches ||
             !resolvePrologueMatches || !initialHashPrologueMatches ||
             !mainHashPrologueMatches || !deepLoadProloguesMatch ||
             !loginAttachProloguesMatch ) {
            Log( "[redirect] login/Main diagnostic signature mismatch; refusing hook" );
            g_cachedLoadGameObjectTarget = nullptr;
            g_i18nLoadGameObjectTarget = nullptr;
            g_resolveI18NPathHashTarget = nullptr;
            g_initInitialPathHashTarget = nullptr;
            g_initMainPathHashTarget = nullptr;
            g_loadUntrackedHashTarget = nullptr;
            g_loadAssetInternalHashTarget = nullptr;
            g_bundleLoaderLoadAssetTarget = nullptr;
            g_tryGetAssetInfoHashTarget = nullptr;
            g_untrackedHandleGateTarget = nullptr;
            g_assetProxyFinishErrorTarget = nullptr;
            g_bundleProxyLoadSyncTarget = nullptr;
            g_bundleProxyGetFilePathTarget = nullptr;
            g_bundleProxyLoadFileTarget = nullptr;
            g_loginSceneRootOnBindTarget = nullptr;
            g_instantiateGameObjectParentTarget = nullptr;
            g_assetProxyHandleGet = nullptr;
            g_gameObjectSetActive = nullptr;
            g_transformSetParent = nullptr;
            g_gameObjectGetComponentsInChildren = nullptr;
            g_rendererSetEnabled = nullptr;
            g_skinnedMeshRendererSetBones = nullptr;
            g_skinnedMeshRendererSetRootBone = nullptr;
            if ( g_redirectTlsIndex != TLS_OUT_OF_INDEXES ) {
                TlsFree( g_redirectTlsIndex );
                g_redirectTlsIndex = TLS_OUT_OF_INDEXES;
            }
            g_gameAssemblyBase = 0;
            return false;
        }

        MH_STATUS status = MH_Initialize( );
        if ( status != MH_OK && status != MH_ERROR_ALREADY_INITIALIZED ) {
            Log( "[redirect] MH_Initialize failed: " +
                std::to_string( static_cast< int >( status ) ) );
            g_cachedLoadGameObjectTarget = nullptr;
            g_i18nLoadGameObjectTarget = nullptr;
            g_resolveI18NPathHashTarget = nullptr;
            g_initInitialPathHashTarget = nullptr;
            g_initMainPathHashTarget = nullptr;
            g_loadUntrackedHashTarget = nullptr;
            g_loadAssetInternalHashTarget = nullptr;
            g_bundleLoaderLoadAssetTarget = nullptr;
            g_tryGetAssetInfoHashTarget = nullptr;
            g_untrackedHandleGateTarget = nullptr;
            g_assetProxyFinishErrorTarget = nullptr;
            g_bundleProxyLoadSyncTarget = nullptr;
            g_bundleProxyGetFilePathTarget = nullptr;
            g_bundleProxyLoadFileTarget = nullptr;
            g_loginSceneRootOnBindTarget = nullptr;
            g_instantiateGameObjectParentTarget = nullptr;
            g_assetProxyHandleGet = nullptr;
            g_gameObjectSetActive = nullptr;
            g_transformSetParent = nullptr;
            g_gameObjectGetComponentsInChildren = nullptr;
            g_rendererSetEnabled = nullptr;
            g_skinnedMeshRendererSetBones = nullptr;
            g_skinnedMeshRendererSetRootBone = nullptr;
            if ( g_redirectTlsIndex != TLS_OUT_OF_INDEXES ) {
                TlsFree( g_redirectTlsIndex );
                g_redirectTlsIndex = TLS_OUT_OF_INDEXES;
            }
            return false;
        }
        g_minHookInitialized = true;

        status = MH_CreateHook( g_instantiateGameObjectParentTarget,
            reinterpret_cast< void * >( &InstantiateGameObjectParentHook ),
            reinterpret_cast< void ** >(
                &g_originalInstantiateGameObjectParent ) );
        if ( status != MH_OK ) {
            Log( "[login-capture] MH_CreateHook(Instantiate<GameObject>) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_loginSceneRootOnBindTarget,
            reinterpret_cast< void * >( &LoginSceneRootOnBindHook ),
            reinterpret_cast< void ** >( &g_originalLoginSceneRootOnBind ) );
        if ( status != MH_OK ) {
            Log( "[login-capture] MH_CreateHook(LoginSceneRoot.OnBindToManager) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_initInitialPathHashTarget,
            reinterpret_cast< void * >( &InitInitialPathHashHook ),
            reinterpret_cast< void ** >( &g_originalInitInitialPathHash ) );
        if ( status != MH_OK ) {
            Log( "[main-diag] MH_CreateHook(InitInitPathHash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_initMainPathHashTarget,
            reinterpret_cast< void * >( &InitMainPathHashHook ),
            reinterpret_cast< void ** >( &g_originalInitMainPathHash ) );
        if ( status != MH_OK ) {
            Log( "[main-diag] MH_CreateHook(InitMainPathHash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_resolveI18NPathHashTarget,
            reinterpret_cast< void * >( &ResolveI18NPathHashHook ),
            reinterpret_cast< void ** >( &g_originalResolveI18NPathHash ) );
        if ( status != MH_OK ) {
            Log( "[main-diag] MH_CreateHook(I18N resolve path hash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_tryGetAssetInfoHashTarget,
            reinterpret_cast< void * >( &TryGetAssetInfoHashHook ),
            reinterpret_cast< void ** >( &g_originalTryGetAssetInfoHash ) );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_CreateHook(TryGetAssetInfoFromPath) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_assetProxyFinishErrorTarget,
            reinterpret_cast< void * >( &AssetProxyFinishErrorHook ),
            reinterpret_cast< void ** >( &g_originalAssetProxyFinishError ) );
        if ( status != MH_OK ) {
            Log( "[bundle-diag] MH_CreateHook(AssetProxy._FinishWithError) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_bundleProxyLoadSyncTarget,
            reinterpret_cast< void * >( &BundleProxyLoadSyncHook ),
            reinterpret_cast< void ** >( &g_originalBundleProxyLoadSync ) );
        if ( status != MH_OK ) {
            Log( "[bundle-diag] MH_CreateHook(BundleProxy.LoadSync) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_bundleProxyGetFilePathTarget,
            reinterpret_cast< void * >( &BundleProxyGetFilePathHook ),
            reinterpret_cast< void ** >( &g_originalBundleProxyGetFilePath ) );
        if ( status != MH_OK ) {
            Log( "[bundle-diag] MH_CreateHook(BundleProxy._GetBundleFileFullPath) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_bundleProxyLoadFileTarget,
            reinterpret_cast< void * >( &BundleProxyLoadFileHook ),
            reinterpret_cast< void ** >( &g_originalBundleProxyLoadFile ) );
        if ( status != MH_OK ) {
            Log( "[bundle-diag] MH_CreateHook(BundleProxy._LoadAssetBundle) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_bundleLoaderLoadAssetTarget,
            reinterpret_cast< void * >( &BundleLoaderLoadAssetHook ),
            reinterpret_cast< void ** >( &g_originalBundleLoaderLoadAsset ) );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_CreateHook(BundleLoader.LoadAsset) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_loadAssetInternalHashTarget,
            reinterpret_cast< void * >( &LoadAssetInternalHashHook ),
            reinterpret_cast< void ** >( &g_originalLoadAssetInternalHash ) );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_CreateHook(_LoadAssetInternal hash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_loadUntrackedHashTarget,
            reinterpret_cast< void * >( &LoadUntrackedHashHook ),
            reinterpret_cast< void ** >( &g_originalLoadUntrackedHash ) );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_CreateHook(LoadUntracked hash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_untrackedHandleGateTarget,
            reinterpret_cast< void * >( &UntrackedHandleGateHook ),
            reinterpret_cast< void ** >( &g_originalUntrackedHandleGate ) );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_CreateHook(untracked-handle gate) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_i18nLoadGameObjectTarget,
            reinterpret_cast< void * >( &I18NLoadGameObjectHook ),
            reinterpret_cast< void ** >( &g_originalI18NLoadGameObject ) );
        if ( status != MH_OK ) {
            Log( "[redirect] MH_CreateHook(I18N Load<GameObject>) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_CreateHook( g_cachedLoadGameObjectTarget,
            reinterpret_cast< void * >( &CachedLoadGameObjectHook ),
            reinterpret_cast< void ** >( &g_originalCachedLoadGameObject ) );
        if ( status != MH_OK ) {
            Log( "[redirect] MH_CreateHook(CachedLoad<GameObject>) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        status = MH_EnableHook( g_instantiateGameObjectParentTarget );
        if ( status != MH_OK ) {
            Log( "[login-capture] MH_EnableHook(Instantiate<GameObject>) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_loginSceneRootOnBindTarget );
        if ( status != MH_OK ) {
            Log( "[login-capture] MH_EnableHook(LoginSceneRoot.OnBindToManager) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_initInitialPathHashTarget );
        if ( status != MH_OK ) {
            Log( "[main-diag] MH_EnableHook(InitInitPathHash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_initMainPathHashTarget );
        if ( status != MH_OK ) {
            Log( "[main-diag] MH_EnableHook(InitMainPathHash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_resolveI18NPathHashTarget );
        if ( status != MH_OK ) {
            Log( "[main-diag] MH_EnableHook(I18N resolve path hash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_tryGetAssetInfoHashTarget );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_EnableHook(TryGetAssetInfoFromPath) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_assetProxyFinishErrorTarget );
        if ( status != MH_OK ) {
            Log( "[bundle-diag] MH_EnableHook(AssetProxy._FinishWithError) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_bundleProxyLoadSyncTarget );
        if ( status != MH_OK ) {
            Log( "[bundle-diag] MH_EnableHook(BundleProxy.LoadSync) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_bundleProxyGetFilePathTarget );
        if ( status != MH_OK ) {
            Log( "[bundle-diag] MH_EnableHook(BundleProxy._GetBundleFileFullPath) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_bundleProxyLoadFileTarget );
        if ( status != MH_OK ) {
            Log( "[bundle-diag] MH_EnableHook(BundleProxy._LoadAssetBundle) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_bundleLoaderLoadAssetTarget );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_EnableHook(BundleLoader.LoadAsset) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_loadAssetInternalHashTarget );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_EnableHook(_LoadAssetInternal hash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_loadUntrackedHashTarget );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_EnableHook(LoadUntracked hash) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_untrackedHandleGateTarget );
        if ( status != MH_OK ) {
            Log( "[load-diag] MH_EnableHook(untracked-handle gate) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_i18nLoadGameObjectTarget );
        if ( status != MH_OK ) {
            Log( "[redirect] MH_EnableHook(I18N Load<GameObject>) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }
        status = MH_EnableHook( g_cachedLoadGameObjectTarget );
        if ( status != MH_OK ) {
            Log( "[redirect] MH_EnableHook(CachedLoad<GameObject>) failed: " +
                std::to_string( static_cast< int >( status ) ) );
            Shutdown( );
            return false;
        }

        Log( "[deferred-load] login actor capture armed; Pelica will attach after Main initialization" );
        return true;
    }

    void QueueReplace( ) {
        if ( !g_cachedLoadGameObjectTarget ) {
            Log( "[redirect] CachedLoad<GameObject> hook is not ready" );
            return;
        }
        Log( g_redirectHit.load( )
            ? "[deferred-load] status: female login actor was preserved and observed"
            : "[deferred-load] status: armed, female login actor load has not been observed" );
        Log( g_mainCategoryHit.load( )
            ? "[deferred-load] status: deferred Pelica Main load was issued"
            : "[deferred-load] status: deferred Pelica Main load was not issued" );
        char deferredMessage [ 384 ] = { 0 };
        snprintf( deferredMessage, sizeof( deferredMessage ),
            "[deferred-load] status: contextCaptured=%s pending=%s attempted=%s "
            "handle=%s raw=%016llX:%016llX:%08X",
            g_loginLoadContextCaptured.load( ) ? "true" : "false",
            g_deferredLoadPending.load( ) ? "true" : "false",
            g_deferredLoadAttempted.load( ) ? "true" : "false",
            g_mainLoadReturnedHandle.load( ) ? "non-zero" : "zero",
            static_cast< unsigned long long >(
                g_deferredHandleRaw0.load( ) ),
            static_cast< unsigned long long >(
                g_deferredHandleRaw1.load( ) ),
            g_deferredHandleRaw2.load( ) );
        Log( deferredMessage );
        char attachMessage [ 640 ] = { 0 };
        snprintf( attachMessage, sizeof( attachMessage ),
            "[replace-live] status: bind=%s instantiateCapture=%s prefab=%s "
            "instantiateAttempted=%s instantiateSucceeded=%s "
            "skeletonAttempted=%s skeletonSucceeded=%s "
            "originalRootDisabled=%s originalRenderersDisabled=%s "
            "originalPrefab=%p parent=%p originalInstance=%p pelicaPrefab=%p pelicaInstance=%p",
            g_loginBindObserved.load( ) ? "observed" : "not-observed",
            g_loginInstantiateCaptured.load( ) ? "captured" : "not-captured",
            g_pelicaPrefabResolved.load( ) ? "resolved" : "not-resolved",
            g_pelicaInstantiateAttempted.load( ) ? "true" : "false",
            g_pelicaInstantiateSucceeded.load( ) ? "true" : "false",
            g_skeletonBindAttempted.load( ) ? "true" : "false",
            g_skeletonBindSucceeded.load( ) ? "true" : "false",
            g_originalLoginActorDisabled.load( ) ? "true" : "false",
            g_originalRenderersDisabled.load( ) ? "true" : "false",
            g_loginActorPrefab.load( ), g_loginActorParent.load( ),
            g_loginActorInstance.load( ), g_pelicaPrefab.load( ),
            g_pelicaInstance.load( ) );
        Log( attachMessage );
        Log( g_initialPathHashCompleted.load( )
            ? "[main-diag] status: Initial path hash initialization completed"
            : "[main-diag] status: Initial path hash initialization was not observed" );
        Log( g_mainPathHashCompleted.load( )
            ? "[main-diag] status: Main path hash initialization completed"
            : "[main-diag] status: Main path hash initialization was not observed" );
        if ( g_mainDiagnosticsCompleted.load( ) ) {
            char message [ 320 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[main-diag] status: manager initialized=%s asyncIniting=%s usingBundle=%s; "
                "CheckExists string/hash/rawHash=%s/%s/%s; resolvedHash=%016llX",
                g_resourceManagerInitialized.load( ) ? "true" : "false",
                g_resourceManagerAsyncIniting.load( ) ? "true" : "false",
                g_resourceManagerUsingBundle.load( ) ? "true" : "false",
                g_stringCheckExists.load( ) ? "true" : "false",
                g_hashCheckExists.load( ) ? "true" : "false",
                g_rawHashCheckExists.load( ) ? "true" : "false",
                static_cast< unsigned long long >(
                    g_targetResolvedHash.load( ) ) );
            Log( message );
        }
        else {
            Log( "[main-diag] status: target Main diagnostic has not completed" );
        }
        if ( g_mainCategoryHit.load( ) ) {
            char message [ 384 ] = { 0 };
            snprintf( message, sizeof( message ),
                "[load-diag] status: assetInfo(load)=%s internalProxy=%s "
                "bundleProxy=%s bundleProxyError=%s untrackedHandle=%s "
                "directIsValid=%s outerGate=%s",
                g_tryGetAssetInfoLoadObserved.load( ) ? "observed" : "not-observed",
                BoolResult( g_loadAssetInternalObserved.load( ),
                    g_loadAssetInternalReturnedProxy.load( ) ),
                BoolResult( g_bundleLoaderObserved.load( ),
                    g_bundleLoaderReturnedProxy.load( ) ),
                BoolResult( g_bundleLoaderObserved.load( ),
                    g_bundleLoaderProxyHasError.load( ) ),
                BoolResult( g_loadUntrackedObserved.load( ),
                    g_loadUntrackedReturnedHandle.load( ) ),
                BoolResult( g_loadUntrackedObserved.load( ),
                    g_loadUntrackedDirectIsValid.load( ) ),
                BoolResult( g_untrackedHandleGateObserved.load( ),
                    g_untrackedHandleGatePassed.load( ) ) );
            Log( message );

            char bundleMessage [ 384 ] = { 0 };
            snprintf( bundleMessage, sizeof( bundleMessage ),
                "[bundle-diag] status: finishError=%s targetPath=%s "
                "fileLoads=%u fileFailures=%u syncLoads=%u syncFailures=%u",
                g_assetProxyFinishErrorObserved.load( ) ? "observed" : "not-observed",
                g_mainBundlePathObserved.load( ) ? "observed" : "not-observed",
                g_bundleFileLoadCount.load( ), g_bundleFileFailureCount.load( ),
                g_bundleSyncCount.load( ), g_bundleSyncFailureCount.load( ) );
            Log( bundleMessage );
        }
    }

    void QueueRestore( ) {
        g_redirectEnabled = false;
        g_deferredLoadPending = false;
        Log( "[deferred-load] disabled for future loads; current scene objects were not changed by F10" );
    }

    void Shutdown( ) {
        g_redirectEnabled = false;
        if ( g_loginSceneRootOnBindTarget ) {
            MH_DisableHook( g_loginSceneRootOnBindTarget );
            MH_RemoveHook( g_loginSceneRootOnBindTarget );
            g_loginSceneRootOnBindTarget = nullptr;
            g_originalLoginSceneRootOnBind = nullptr;
        }
        if ( g_instantiateGameObjectParentTarget ) {
            MH_DisableHook( g_instantiateGameObjectParentTarget );
            MH_RemoveHook( g_instantiateGameObjectParentTarget );
            g_instantiateGameObjectParentTarget = nullptr;
            g_originalInstantiateGameObjectParent = nullptr;
        }
        if ( g_cachedLoadGameObjectTarget ) {
            MH_DisableHook( g_cachedLoadGameObjectTarget );
            MH_RemoveHook( g_cachedLoadGameObjectTarget );
            g_cachedLoadGameObjectTarget = nullptr;
            g_originalCachedLoadGameObject = nullptr;
        }
        if ( g_i18nLoadGameObjectTarget ) {
            MH_DisableHook( g_i18nLoadGameObjectTarget );
            MH_RemoveHook( g_i18nLoadGameObjectTarget );
            g_i18nLoadGameObjectTarget = nullptr;
            g_originalI18NLoadGameObject = nullptr;
        }
        if ( g_bundleProxyLoadFileTarget ) {
            MH_DisableHook( g_bundleProxyLoadFileTarget );
            MH_RemoveHook( g_bundleProxyLoadFileTarget );
            g_bundleProxyLoadFileTarget = nullptr;
            g_originalBundleProxyLoadFile = nullptr;
        }
        if ( g_bundleProxyGetFilePathTarget ) {
            MH_DisableHook( g_bundleProxyGetFilePathTarget );
            MH_RemoveHook( g_bundleProxyGetFilePathTarget );
            g_bundleProxyGetFilePathTarget = nullptr;
            g_originalBundleProxyGetFilePath = nullptr;
        }
        if ( g_bundleProxyLoadSyncTarget ) {
            MH_DisableHook( g_bundleProxyLoadSyncTarget );
            MH_RemoveHook( g_bundleProxyLoadSyncTarget );
            g_bundleProxyLoadSyncTarget = nullptr;
            g_originalBundleProxyLoadSync = nullptr;
        }
        if ( g_assetProxyFinishErrorTarget ) {
            MH_DisableHook( g_assetProxyFinishErrorTarget );
            MH_RemoveHook( g_assetProxyFinishErrorTarget );
            g_assetProxyFinishErrorTarget = nullptr;
            g_originalAssetProxyFinishError = nullptr;
        }
        if ( g_untrackedHandleGateTarget ) {
            MH_DisableHook( g_untrackedHandleGateTarget );
            MH_RemoveHook( g_untrackedHandleGateTarget );
            g_untrackedHandleGateTarget = nullptr;
            g_originalUntrackedHandleGate = nullptr;
        }
        if ( g_loadUntrackedHashTarget ) {
            MH_DisableHook( g_loadUntrackedHashTarget );
            MH_RemoveHook( g_loadUntrackedHashTarget );
            g_loadUntrackedHashTarget = nullptr;
            g_originalLoadUntrackedHash = nullptr;
        }
        if ( g_loadAssetInternalHashTarget ) {
            MH_DisableHook( g_loadAssetInternalHashTarget );
            MH_RemoveHook( g_loadAssetInternalHashTarget );
            g_loadAssetInternalHashTarget = nullptr;
            g_originalLoadAssetInternalHash = nullptr;
        }
        if ( g_bundleLoaderLoadAssetTarget ) {
            MH_DisableHook( g_bundleLoaderLoadAssetTarget );
            MH_RemoveHook( g_bundleLoaderLoadAssetTarget );
            g_bundleLoaderLoadAssetTarget = nullptr;
            g_originalBundleLoaderLoadAsset = nullptr;
        }
        if ( g_tryGetAssetInfoHashTarget ) {
            MH_DisableHook( g_tryGetAssetInfoHashTarget );
            MH_RemoveHook( g_tryGetAssetInfoHashTarget );
            g_tryGetAssetInfoHashTarget = nullptr;
            g_originalTryGetAssetInfoHash = nullptr;
        }
        if ( g_resolveI18NPathHashTarget ) {
            MH_DisableHook( g_resolveI18NPathHashTarget );
            MH_RemoveHook( g_resolveI18NPathHashTarget );
            g_resolveI18NPathHashTarget = nullptr;
            g_originalResolveI18NPathHash = nullptr;
        }
        if ( g_initMainPathHashTarget ) {
            MH_DisableHook( g_initMainPathHashTarget );
            MH_RemoveHook( g_initMainPathHashTarget );
            g_initMainPathHashTarget = nullptr;
            g_originalInitMainPathHash = nullptr;
        }
        if ( g_initInitialPathHashTarget ) {
            MH_DisableHook( g_initInitialPathHashTarget );
            MH_RemoveHook( g_initInitialPathHashTarget );
            g_initInitialPathHashTarget = nullptr;
            g_originalInitInitialPathHash = nullptr;
        }
        if ( g_minHookInitialized ) {
            MH_Uninitialize( );
            g_minHookInitialized = false;
        }
        if ( g_redirectTlsIndex != TLS_OUT_OF_INDEXES ) {
            TlsFree( g_redirectTlsIndex );
            g_redirectTlsIndex = TLS_OUT_OF_INDEXES;
        }
        g_gameAssemblyBase = 0;
        g_il2cppThreadCurrent = nullptr;
        g_il2cppStringNew = nullptr;
        g_resourceManagerIsInitialized = nullptr;
        g_resourceManagerIsAsyncIniting = nullptr;
        g_resourceManagerGetUsingBundle = nullptr;
        g_resourceManagerCheckString = nullptr;
        g_resourceManagerCheckHash = nullptr;
        g_resourceManagerRawCheckHash = nullptr;
        g_untrackedHandleIsValid = nullptr;
        g_assetProxyHasError = nullptr;
        g_assetProxyHandleGet = nullptr;
        g_gameObjectSetActive = nullptr;
        g_transformSetParent = nullptr;
        g_gameObjectGetComponentsInChildren = nullptr;
        g_rendererSetEnabled = nullptr;
        g_skinnedMeshRendererSetBones = nullptr;
        g_skinnedMeshRendererSetRootBone = nullptr;
        g_pelicaPrefabGcHandle = 0;
    }

} // namespace ModelReplacer
