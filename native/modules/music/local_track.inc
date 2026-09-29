// Local file playback for BetterEndfield/LocalMusic.h. One Media Foundation
// decoder thread converts the file to the Wwise output rate and feeds the same
// Audio Input ring as OmniMix; PumpLocalMusicPolicy() on the game's main thread
// owns every Wwise call. Included inside the module's anonymous namespace.

namespace LocalTrack {

constexpr uint32_t kPushFloorFrames = 16384u;
constexpr double kMaximumSeekSeconds = 24.0 * 3600.0;

struct Command {
    enum class Kind { Open, Play, Pause, Seek, Close, Gain } kind = Kind::Close;
    uint64_t token = 0;
    double position = -1.0;
    float gain = 1.0f;
    std::wstring path;
    std::string owner;
};

std::mutex mutex;
std::condition_variable wake;
std::deque<Command> queue;      // guarded by mutex
bool stop_requested = false;    // guarded by mutex
std::thread worker;             // guarded by mutex
std::atomic<bool> enabled{true};
std::atomic<uint64_t> next_token{0};
std::atomic<uint64_t> current_token{0};
std::atomic<int32_t> state{BE_LocalMusic_Idle};
std::atomic<double> base_seconds{0.0};
std::atomic<double> duration_seconds{0.0};
std::atomic<uint32_t> base_rate{48000};
std::mutex error_mutex;
std::string error; // guarded by error_mutex

void SetError(const std::string& message) {
    {
        std::scoped_lock lock(error_mutex);
        error = message;
    }
    state.store(BE_LocalMusic_Error, std::memory_order_release);
    playing.store(false, std::memory_order_release);
    Log("[music.local] " + message);
}

void TakeRing() {
    std::scoped_lock lock(g_ring_producer_mutex);
    g_local_owns_ring.store(true, std::memory_order_release);
    g_ring.Clear();
}

void ReleaseRing() {
    std::scoped_lock lock(g_ring_producer_mutex);
    g_ring.Clear();
    g_local_owns_ring.store(false, std::memory_order_release);
}

bool AllowedExtension(const std::filesystem::path& path) {
    std::wstring extension = path.extension().wstring();
    for (wchar_t& character : extension) {
        character = static_cast<wchar_t>(towlower(character));
    }
    return extension == L".wav" || extension == L".mp3" || extension == L".m4a" ||
        extension == L".aac" || extension == L".flac" || extension == L".wma";
}

class Decoder {
public:
    ~Decoder() { Close(); }

    bool Open(const std::wstring& path, std::string& message) {
        Close();
        std::error_code status;
        const std::filesystem::path file(path);
        if (!std::filesystem::is_regular_file(file, status)) {
            message = "music file not found";
            return false;
        }
        if (!AllowedExtension(file)) {
            message = "music must be WAV, MP3, M4A, AAC, FLAC or WMA";
            return false;
        }
        HRESULT result = MFCreateSourceReaderFromURL(path.c_str(), nullptr, &reader_);
        if (FAILED(result) || !reader_) {
            message = "Media Foundation cannot open the music file (hr=" +
                std::to_string(static_cast<long>(result)) + ")";
            Close();
            return false;
        }
        reader_->SetStreamSelection(
            static_cast<DWORD>(MF_SOURCE_READER_ALL_STREAMS), FALSE);
        reader_->SetStreamSelection(
            static_cast<DWORD>(MF_SOURCE_READER_FIRST_AUDIO_STREAM), TRUE);
        IMFMediaType* requested = nullptr;
        result = MFCreateMediaType(&requested);
        if (SUCCEEDED(result)) result = requested->SetGUID(MF_MT_MAJOR_TYPE, MFMediaType_Audio);
        if (SUCCEEDED(result)) result = requested->SetGUID(MF_MT_SUBTYPE, MFAudioFormat_Float);
        if (SUCCEEDED(result)) {
            result = reader_->SetCurrentMediaType(
                static_cast<DWORD>(MF_SOURCE_READER_FIRST_AUDIO_STREAM), nullptr, requested);
        }
        if (requested) requested->Release();
        if (FAILED(result) || !ReadFormat()) {
            message = "the music file has no decodable audio stream";
            Close();
            return false;
        }
        PROPVARIANT value;
        PropVariantInit(&value);
        duration_ = 0.0;
        if (SUCCEEDED(reader_->GetPresentationAttribute(
                static_cast<DWORD>(MF_SOURCE_READER_MEDIASOURCE), MF_PD_DURATION, &value)) &&
            value.vt == VT_UI8) {
            duration_ = static_cast<double>(value.uhVal.QuadPart) / 1.0e7;
        }
        PropVariantClear(&value);
        return true;
    }

    void Close() {
        if (reader_) reader_->Release();
        reader_ = nullptr;
        pending_.clear();
        eof_ = false;
    }

    bool IsOpen() const { return reader_ != nullptr; }
    bool Drained() const { return eof_ && pending_.empty(); }
    double Duration() const { return duration_; }
    uint32_t OutputRate() const { return output_rate_; }
    void SetGain(float gain) { gain_ = std::clamp(gain, 0.0f, 2.0f); }

    bool Seek(double seconds, uint32_t output_rate) {
        if (!reader_) return false;
        seconds = std::clamp(seconds, 0.0, kMaximumSeekSeconds);
        PROPVARIANT value;
        PropVariantInit(&value);
        value.vt = VT_I8;
        value.hVal.QuadPart = static_cast<LONGLONG>(seconds * 1.0e7);
        const HRESULT result = reader_->SetCurrentPosition(GUID_NULL, value);
        PropVariantClear(&value);
        if (FAILED(result)) return false;
        output_rate_ = std::max(output_rate, 1u);
        resampler_.Configure(rate_, channels_, output_rate_);
        pending_.clear();
        eof_ = false;
        {
            std::scoped_lock lock(g_ring_producer_mutex);
            g_ring.Clear();
            base_seconds.store(seconds, std::memory_order_release);
            base_rate.store(output_rate_, std::memory_order_release);
            consumed_frames.store(0, std::memory_order_release);
        }
        return true;
    }

    // Decodes one Media Foundation sample when the ring has room.
    bool Pump(std::string& message) {
        if (!reader_) return true;
        if (!pending_.empty()) {
            PushPending();
            return true;
        }
        if (eof_ || g_ring.AvailableWrite() < kPushFloorFrames) return true;
        DWORD flags = 0;
        LONGLONG timestamp = 0;
        IMFSample* sample = nullptr;
        const HRESULT result = reader_->ReadSample(
            static_cast<DWORD>(MF_SOURCE_READER_FIRST_AUDIO_STREAM), 0, nullptr,
            &flags, &timestamp, &sample);
        if (FAILED(result)) {
            if (sample) sample->Release();
            message = "music decode failed (hr=" + std::to_string(static_cast<long>(result)) + ")";
            return false;
        }
        if (flags & MF_SOURCE_READERF_CURRENTMEDIATYPECHANGED) {
            if (!ReadFormat()) {
                if (sample) sample->Release();
                message = "music stream changed to an unsupported format";
                return false;
            }
            resampler_.Configure(rate_, channels_, output_rate_);
        }
        if (flags & MF_SOURCE_READERF_ENDOFSTREAM) eof_ = true;
        if (!sample) return true;
        IMFMediaBuffer* buffer = nullptr;
        bool ok = SUCCEEDED(sample->ConvertToContiguousBuffer(&buffer)) && buffer;
        BYTE* data = nullptr;
        DWORD length = 0;
        if (ok) ok = SUCCEEDED(buffer->Lock(&data, nullptr, &length));
        if (ok) {
            const uint32_t frames = static_cast<uint32_t>(
                length / (sizeof(float) * static_cast<uint32_t>(channels_)));
            resampler_.Convert(reinterpret_cast<const float*>(data), frames, pending_);
            buffer->Unlock();
            if (gain_ != 1.0f) {
                for (float& value : pending_) value *= gain_;
            }
        }
        if (buffer) buffer->Release();
        sample->Release();
        PushPending();
        return true;
    }

private:
    bool ReadFormat() {
        IMFMediaType* current = nullptr;
        if (FAILED(reader_->GetCurrentMediaType(
                static_cast<DWORD>(MF_SOURCE_READER_FIRST_AUDIO_STREAM), &current)) || !current) {
            return false;
        }
        GUID subtype{};
        current->GetGUID(MF_MT_SUBTYPE, &subtype);
        const UINT32 rate = MFGetAttributeUINT32(current, MF_MT_AUDIO_SAMPLES_PER_SECOND, 0);
        const UINT32 channels = MFGetAttributeUINT32(current, MF_MT_AUDIO_NUM_CHANNELS, 0);
        const UINT32 bits = MFGetAttributeUINT32(current, MF_MT_AUDIO_BITS_PER_SAMPLE, 0);
        current->Release();
        if (subtype != MFAudioFormat_Float || bits != 32 || rate == 0 || rate > 384000 ||
            channels == 0 || channels > 8) {
            return false;
        }
        rate_ = static_cast<int32_t>(rate);
        channels_ = static_cast<int32_t>(channels);
        return true;
    }

    void PushPending() {
        if (pending_.empty()) return;
        const uint32_t frames = static_cast<uint32_t>(pending_.size() / 2u);
        uint32_t pushed = 0;
        {
            std::scoped_lock lock(g_ring_producer_mutex);
            if (g_local_owns_ring.load(std::memory_order_acquire)) {
                pushed = g_ring.Push(pending_.data(), frames);
            }
        }
        pending_.erase(pending_.begin(), pending_.begin() + static_cast<ptrdiff_t>(pushed) * 2);
    }

    IMFSourceReader* reader_ = nullptr;
    LinearStereoResampler resampler_;
    std::vector<float> pending_;
    int32_t rate_ = 48000;
    int32_t channels_ = 2;
    uint32_t output_rate_ = 48000;
    double duration_ = 0.0;
    float gain_ = 1.0f;
    bool eof_ = false;
};

double Position() {
    const double rate = std::max<uint32_t>(base_rate.load(std::memory_order_acquire), 1u);
    return base_seconds.load(std::memory_order_acquire) +
        static_cast<double>(consumed_frames.load(std::memory_order_acquire)) / rate;
}

void CloseCurrent(Decoder& decoder, const char* reason) {
    const bool had_track = decoder.IsOpen() || g_local_owns_ring.load(std::memory_order_acquire);
    decoder.Close();
    playing.store(false, std::memory_order_release);
    ReleaseRing();
    duration_seconds.store(0.0, std::memory_order_release);
    if (had_track) Log(std::string("[music.local] track closed: ") + reason);
}

void Process(Decoder& decoder, const Command& command) {
    const uint64_t current = current_token.load(std::memory_order_acquire);
    if (command.token != current) return; // superseded or already closed
    const uint32_t output_rate = std::max(g_output_sample_rate.load(std::memory_order_acquire), 1u);
    switch (command.kind) {
    case Command::Kind::Open: {
        CloseCurrent(decoder, "replaced");
        std::string message;
        if (!decoder.Open(command.path, message)) {
            SetError(message);
            return;
        }
        TakeRing();
        decoder.Seek(0.0, output_rate);
        duration_seconds.store(decoder.Duration(), std::memory_order_release);
        state.store(BE_LocalMusic_Ready, std::memory_order_release);
        Log("[music.local] track opened owner=" + command.owner + " duration=" +
            std::to_string(decoder.Duration()) + "s");
        return;
    }
    case Command::Kind::Play:
        if (!decoder.IsOpen()) return;
        if (command.position >= 0.0 && !decoder.Seek(command.position, output_rate)) {
            SetError("music seek failed");
            return;
        }
        playing.store(true, std::memory_order_release);
        state.store(BE_LocalMusic_Playing, std::memory_order_release);
        return;
    case Command::Kind::Pause:
        if (!decoder.IsOpen()) return;
        playing.store(false, std::memory_order_release);
        state.store(BE_LocalMusic_Paused, std::memory_order_release);
        return;
    case Command::Kind::Seek:
        if (decoder.IsOpen() && !decoder.Seek(command.position, output_rate)) {
            SetError("music seek failed");
        } else if (state.load(std::memory_order_acquire) == BE_LocalMusic_Ended) {
            state.store(BE_LocalMusic_Paused, std::memory_order_release);
        }
        return;
    case Command::Kind::Gain:
        decoder.SetGain(command.gain);
        return;
    case Command::Kind::Close: {
        CloseCurrent(decoder, "closed by owner");
        uint64_t expected = command.token;
        if (current_token.compare_exchange_strong(expected, 0, std::memory_order_acq_rel)) {
            state.store(BE_LocalMusic_Idle, std::memory_order_release);
        }
        return;
    }
    }
}

void WorkerMain() {
    const HRESULT com = CoInitializeEx(nullptr, COINIT_MULTITHREADED);
    const HRESULT media = MFStartup(MF_VERSION, MFSTARTUP_LITE);
    if (FAILED(media)) Log("[music.local] MFStartup failed; local playback unavailable");
    Decoder decoder;
    for (;;) {
        std::deque<Command> commands;
        {
            std::unique_lock lock(mutex);
            wake.wait_for(lock, std::chrono::milliseconds(decoder.IsOpen() ? 5 : 100),
                [] { return stop_requested || !queue.empty(); });
            if (stop_requested) break;
            commands.swap(queue);
        }
        for (const Command& command : commands) {
            if (FAILED(media)) {
                if (command.kind == Command::Kind::Open &&
                    command.token == current_token.load(std::memory_order_acquire)) {
                    SetError("Media Foundation is unavailable");
                }
                continue;
            }
            Process(decoder, command);
        }
        if (!decoder.IsOpen()) continue;
        const uint32_t output_rate = std::max(g_output_sample_rate.load(std::memory_order_acquire), 1u);
        if (output_rate != decoder.OutputRate() && !decoder.Seek(Position(), output_rate)) {
            SetError("music seek failed after the output rate changed");
            CloseCurrent(decoder, "error");
            continue;
        }
        std::string message;
        for (int step = 0; step < 8; ++step) {
            if (!decoder.Pump(message)) break;
        }
        if (!message.empty()) {
            SetError(message);
            CloseCurrent(decoder, "error");
            continue;
        }
        if (decoder.Drained() && g_ring.AvailableRead() == 0 &&
            state.load(std::memory_order_acquire) == BE_LocalMusic_Playing) {
            playing.store(false, std::memory_order_release);
            state.store(BE_LocalMusic_Ended, std::memory_order_release);
        }
    }
    CloseCurrent(decoder, "shutdown");
    if (SUCCEEDED(media)) MFShutdown();
    if (SUCCEEDED(com)) CoUninitialize();
}

bool Enqueue(Command command) {
    std::scoped_lock lock(mutex);
    if (stop_requested) return false;
    if (!worker.joinable()) worker = std::thread(&WorkerMain);
    queue.push_back(std::move(command));
    wake.notify_one();
    return true;
}

void Stop() {
    {
        std::scoped_lock lock(mutex);
        stop_requested = true;
        queue.clear();
    }
    wake.notify_all();
    if (worker.joinable()) worker.join();
    current_token.store(0, std::memory_order_release);
    state.store(BE_LocalMusic_Idle, std::memory_order_release);
}

uint64_t BE_CALL ApiOpen(const char* utf8_path, const char* owner) {
    try {
        if (!utf8_path || !*utf8_path || !enabled.load(std::memory_order_acquire)) return 0;
        std::wstring path = Utf8ToWide(utf8_path);
        if (path.empty()) return 0;
        const uint64_t token = next_token.fetch_add(1, std::memory_order_acq_rel) + 1;
        {
            std::scoped_lock lock(error_mutex);
            error.clear();
        }
        current_token.store(token, std::memory_order_release);
        state.store(BE_LocalMusic_Loading, std::memory_order_release);
        Command command;
        command.kind = Command::Kind::Open;
        command.token = token;
        command.path = std::move(path);
        command.owner = owner ? owner : "";
        if (!Enqueue(std::move(command))) {
            current_token.store(0, std::memory_order_release);
            state.store(BE_LocalMusic_Idle, std::memory_order_release);
            return 0;
        }
        return token;
    } catch (...) {
        return 0;
    }
}

int Submit(Command::Kind kind, uint64_t token, double position = -1.0, float gain = 1.0f) {
    try {
        if (!token || token != current_token.load(std::memory_order_acquire)) return 0;
        Command command;
        command.kind = kind;
        command.token = token;
        command.position = std::isfinite(position) ? position : -1.0;
        command.gain = std::isfinite(gain) ? gain : 1.0f;
        return Enqueue(std::move(command)) ? 1 : 0;
    } catch (...) {
        return 0;
    }
}

int BE_CALL ApiPlay(uint64_t token, double position) {
    return Submit(Command::Kind::Play, token, position);
}
int BE_CALL ApiPause(uint64_t token) { return Submit(Command::Kind::Pause, token); }
int BE_CALL ApiSeek(uint64_t token, double position) {
    return position >= 0.0 ? Submit(Command::Kind::Seek, token, position) : 0;
}
void BE_CALL ApiClose(uint64_t token) { Submit(Command::Kind::Close, token); }
int BE_CALL ApiSetGain(uint64_t token, float gain) {
    return Submit(Command::Kind::Gain, token, -1.0, gain);
}

int BE_CALL ApiStatus(uint64_t token, BE_LocalMusicStatusV1* out) {
    if (!out || out->size < sizeof(BE_LocalMusicStatusV1)) return 0;
    const uint32_t size = out->size;
    *out = {};
    out->size = size;
    if (!token || token != current_token.load(std::memory_order_acquire)) {
        out->state = BE_LocalMusic_Idle;
        return 1;
    }
    out->state = state.load(std::memory_order_acquire);
    out->position_seconds = Position();
    out->duration_seconds = duration_seconds.load(std::memory_order_acquire);
    out->output_active = output_active.load(std::memory_order_acquire) ? 1u : 0u;
    if (out->state == BE_LocalMusic_Error) {
        std::scoped_lock lock(error_mutex);
        strncpy_s(out->error, error.c_str(), _TRUNCATE);
    }
    return 1;
}

const BE_LocalMusicApiV1 kApi{1, sizeof(BE_LocalMusicApiV1), &ApiOpen, &ApiPlay,
    &ApiPause, &ApiSeek, &ApiClose, &ApiStatus, &ApiSetGain};

} // namespace LocalTrack
