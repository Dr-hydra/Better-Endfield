// Offline sampling of extracted ACL 2.1 tracks; no game memory or code changes.
#include <acl/decompression/decompress.h>
#include <acl/core/track_writer.h>
#include <rtm/quatf.h>
#include <rtm/vector4f.h>
#include <rtm/scalarf.h>
#include <fstream>
#include <iostream>
#include <vector>
#include <limits>
#include <malloc.h>

struct Writer : acl::track_writer {
    std::vector<float> values;
    uint32_t stride;
    Writer(uint32_t tracks, uint32_t width) : values(tracks * width), stride(width) {}
    void RTM_SIMD_CALL write_float1(uint32_t i, rtm::scalarf_arg0 v) { values[i] = rtm::scalar_cast(v); }
    void RTM_SIMD_CALL write_rotation(uint32_t i, rtm::quatf_arg0 v) { rtm::quat_store(v, values.data() + i * stride); }
    void RTM_SIMD_CALL write_translation(uint32_t i, rtm::vector4f_arg0 v) { rtm::vector_store3(v, values.data() + i * stride + 4); }
    void RTM_SIMD_CALL write_scale(uint32_t i, rtm::vector4f_arg0 v) { rtm::vector_store3(v, values.data() + i * stride + 7); }
};
int main(int argc, char** argv) {
    if (argc != 3) return 2;
    std::ifstream input(argv[1], std::ios::binary | std::ios::ate);
    if (!input) return 3;
    auto size = input.tellg();
    if (size <= 0 || size > 100000000) return 4;
    void* buffer = _aligned_malloc(static_cast<size_t>(size) + 64, 64);
    input.seekg(0); input.read(static_cast<char*>(buffer), size);
    acl::error_result error;
    const auto* tracks = acl::make_compressed_tracks(buffer, &error);
    if (!tracks || !error.empty()) { std::cerr << error.c_str(); return 5; }
    error = tracks->is_valid(false); // Do not compute an artifact/content hash.
    if (!error.empty()) { std::cerr << error.c_str(); return 6; }
    uint32_t n = tracks->get_num_tracks(), frames = tracks->get_num_samples_per_track();
    float rate = tracks->get_sample_rate();
    auto type = tracks->get_track_type();
    if (type != acl::track_type8::qvvf && type != acl::track_type8::float1f) return 7;
    uint32_t width = type == acl::track_type8::qvvf ? 10 : 1;
    if (!n || n > 10000 || frames > 100000) return 8;
    acl::decompression_context<acl::decompression_settings> context;
    if (!context.initialize(*tracks)) return 9;
    Writer writer(n, width);
    std::ofstream output(argv[2], std::ios::binary);
    for (uint32_t i = 0; i < frames; ++i) {
        context.seek(float(i) / rate, acl::sample_rounding_policy::nearest);
        context.decompress_tracks(writer);
        output.write(reinterpret_cast<const char*>(writer.values.data()), writer.values.size() * sizeof(float));
    }
    std::cout << "{\"tracks\":" << n << ",\"frames\":" << frames << ",\"width\":" << width
        << ",\"rate\":" << rate << ",\"version\":" << int(tracks->get_version()) << "}\n";
    _aligned_free(buffer);
}
