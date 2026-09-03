#pragma once

// ABI subset derived from OmniPcmShared's MIT-licensed public header.
// Better Endfield loads these functions dynamically and does not link against
// a particular OmniMix build.

#include <cstddef>
#include <cstdint>

namespace BetterEndfield::Music::Omni {

constexpr uint32_t kVersion2 = 2u;
constexpr uint32_t kAbiMajor = 2u;
constexpr uint32_t kFloat32Interleaved = 1u;
constexpr size_t kUuidBytes = 64;

enum StreamFlags : uint32_t {
    FormatReady = 1u << 0,
    DecoderEof = 1u << 1,
    StreamError = 1u << 2,
    SeekPending = 1u << 3,
    Discontinuity = 1u << 4,
    ClientDrained = 1u << 5,
    SyntheticEof = 1u << 6,
};

enum StreamState : int32_t {
    Stopped = 0,
    Preparing = 1,
    Playing = 2,
    Paused = 3,
    Draining = 4,
    Ended = 5,
    Error = 6,
};

enum InstanceKind : int32_t {
    GameMod = 1,
};

enum CapabilityFlags : uint32_t {
    ServerControlledPlayback = 1u << 0,
    QueueManagement = 1u << 2,
    Seek = 1u << 6,
    AudioPlayback = 1u << 13,
};

struct SnapshotV2 {
    uint32_t size;
    uint32_t abi_version;
    uint32_t shared_protocol_version;
    int32_t sample_format;
    int32_t sample_rate;
    int32_t channels;
    int32_t bytes_per_frame;
    int32_t buffer_frames;
    int32_t legacy_play_state;
    uint32_t flags;
    int64_t write_cursor;
    int64_t read_cursor;
    int64_t stream_id;
    int32_t state;
    int32_t error_code;
    int64_t total_frames_hint;
    int64_t decoded_total_frames;
    int64_t final_write_cursor;
    int64_t audible_cursor;
    int64_t seek_frame;
    int64_t seek_generation;
    int64_t heartbeat_monotonic_ms;
    int32_t format_generation;
    int32_t reserved;
    char current_uuid[kUuidBytes];
};

struct AbiInfo {
    uint32_t size;
    uint32_t abi_version;
    uint32_t abi_major;
    uint32_t abi_minor;
    uint32_t min_shared_protocol;
    uint32_t max_shared_protocol;
    uint32_t sample_format_mask;
    uint32_t reserved;
};

struct ClientConfig {
    const char* host;
    int32_t port;
    int32_t timeout_ms;
};

struct ConnectOptions {
    const char* client_id;
    const char* mod_id;
    const char* game_name;
    const char* display_name;
    int32_t kind;
    uint32_t capability_flags;
    int32_t no_instance;
    int32_t max_imported_playlists;
    int32_t max_tags;
    int32_t max_playlist_entries;
};

struct ConnectionInfo {
    char instance_id[128];
    int32_t is_new;
    int32_t no_instance;
};

using Handle = void*;
using ClientHandle = void*;

using GetAbiVersionFn = uint32_t(__cdecl*)();
using GetAbiInfoFn = int(__cdecl*)(AbiInfo*);
using OpenInstanceUtf8Fn = Handle(__cdecl*)(const char*);
using CloseFn = void(__cdecl*)(Handle);
using IsOpenFn = int(__cdecl*)(Handle);
using GetVersionFn = uint32_t(__cdecl*)(Handle);
using GetLastErrorFn = const char*(__cdecl*)(Handle);
using GetSnapshotV2Fn = int(__cdecl*)(Handle, SnapshotV2*);
using BindCurrentStreamFn = int(__cdecl*)(Handle);
using ReadFramesFn = int64_t(__cdecl*)(Handle, float*, int32_t);
using SetAudibleCursorFn = int(__cdecl*)(Handle, int64_t, int);

using ClientCreateFn = ClientHandle(__cdecl*)(const ClientConfig*);
using ClientDestroyFn = void(__cdecl*)(ClientHandle);
using ClientGetLastErrorFn = const char*(__cdecl*)(ClientHandle);
using ClientConnectInstanceFn = int(__cdecl*)(
    ClientHandle, const ConnectOptions*, ConnectionInfo*);
using ClientHeartbeatFn = int(__cdecl*)(ClientHandle, const char*, int*);
using ClientDisconnectInstanceFn = int(__cdecl*)(ClientHandle, const char*);
using ClientSetTargetLatencyFn = int(__cdecl*)(ClientHandle, const char*, float);

struct Api {
    GetAbiVersionFn get_abi_version = nullptr;
    GetAbiInfoFn get_abi_info = nullptr;
    OpenInstanceUtf8Fn open_instance_utf8 = nullptr;
    CloseFn close = nullptr;
    IsOpenFn is_open = nullptr;
    GetVersionFn get_version = nullptr;
    GetLastErrorFn get_last_error = nullptr;
    GetSnapshotV2Fn get_snapshot_v2 = nullptr;
    BindCurrentStreamFn bind_current_stream = nullptr;
    ReadFramesFn read_frames = nullptr;
    SetAudibleCursorFn set_audible_cursor = nullptr;
    ClientCreateFn client_create = nullptr;
    ClientDestroyFn client_destroy = nullptr;
    ClientGetLastErrorFn client_get_last_error = nullptr;
    ClientConnectInstanceFn client_connect_instance = nullptr;
    ClientHeartbeatFn client_heartbeat = nullptr;
    ClientDisconnectInstanceFn client_disconnect_instance = nullptr;
    ClientSetTargetLatencyFn client_set_target_latency = nullptr;
};

static_assert(sizeof(SnapshotV2) == 200);
static_assert(sizeof(AbiInfo) == 32);
static_assert(sizeof(ClientConfig) == 16);
static_assert(sizeof(ConnectOptions) == 56);
static_assert(sizeof(ConnectionInfo) == 136);

} // namespace BetterEndfield::Music::Omni
