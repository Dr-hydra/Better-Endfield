#include "BetterEndfield/vmd_parser.h"

#include <cmath>
#include <cstdint>
#include <cstring>
#include <iostream>
#include <stdexcept>
#include <string>
#include <vector>

using BetterEndfield::Vmd::Document;
using BetterEndfield::Vmd::Limits;

namespace {

void Check(bool condition, const char* message) {
    if (!condition) throw std::runtime_error(message);
}

template <class T>
void Append(std::vector<uint8_t>& bytes, const T& value) {
    const auto* data = reinterpret_cast<const uint8_t*>(&value);
    bytes.insert(bytes.end(), data, data + sizeof(value));
}

void AppendString(std::vector<uint8_t>& bytes, const std::string& value, size_t width) {
    Check(value.size() <= width, "fixture string is too long");
    bytes.insert(bytes.end(), value.begin(), value.end());
    bytes.insert(bytes.end(), width - value.size(), 0);
}

void AppendCamera(std::vector<uint8_t>& bytes, uint32_t frame, float distance,
    uint32_t fov, uint8_t perspective = 1) {
    Append(bytes, frame);
    Append(bytes, distance);
    const float interest[3]{1.0f, 2.0f, 3.0f};
    const float euler[3]{0.1f, 0.2f, 0.3f};
    for (float value : interest) Append(bytes, value);
    for (float value : euler) Append(bytes, value);
    for (int i = 0; i < 24; ++i) bytes.push_back(static_cast<uint8_t>(i));
    Append(bytes, fov);
    bytes.push_back(perspective);
}

std::vector<uint8_t> MakeFixture(bool optional_sections) {
    std::vector<uint8_t> bytes;
    AppendString(bytes, "Vocaloid Motion Data 0002", 30);
    AppendString(bytes, "parser-test", 20);

    uint32_t count = 1;
    Append(bytes, count);
    AppendString(bytes, "center", 15);
    uint32_t frame = 3;
    Append(bytes, frame);
    const float position[3]{1.0f, 2.0f, 3.0f};
    const float rotation[4]{0.0f, 0.0f, 0.0f, 1.0f};
    for (float value : position) Append(bytes, value);
    for (float value : rotation) Append(bytes, value);
    bytes.insert(bytes.end(), 64, 0x7f);

    Append(bytes, count);
    AppendString(bytes, "smile", 15);
    Append(bytes, frame);
    const float weight = 0.5f;
    Append(bytes, weight);

    count = 4;
    Append(bytes, count);
    AppendCamera(bytes, 20, 20.0f, 60);
    AppendCamera(bytes, 10, 10.0f, 45);
    AppendCamera(bytes, 10, 11.0f, 50);
    AppendCamera(bytes, 30, 30.0f, 0); // Invalid camera is ignored.

    if (!optional_sections) return bytes;
    count = 1;
    Append(bytes, count);
    Append(bytes, frame);
    const float color[3]{0.1f, 0.2f, 0.3f};
    const float light_position[3]{4.0f, 5.0f, 6.0f};
    for (float value : color) Append(bytes, value);
    for (float value : light_position) Append(bytes, value);

    Append(bytes, count);
    Append(bytes, frame);
    bytes.push_back(2);
    const float shadow_distance = 0.25f;
    Append(bytes, shadow_distance);

    Append(bytes, count);
    Append(bytes, frame);
    bytes.push_back(1);
    uint32_t toggle_count = 1;
    Append(bytes, toggle_count);
    AppendString(bytes, "センター", 20);
    bytes.push_back(0);
    return bytes;
}

} // namespace

int main() {
    try {
        Document document;
        std::string error;
        auto minimal = MakeFixture(false);
        Check(BetterEndfield::Vmd::Parse(minimal, document, error), "minimal VMD rejected");
        Check(document.version_2 && document.model_name == "parser-test", "header was not parsed");
        Check(document.bones.size() == 1 && document.morphs.size() == 1, "bone/morph records missing");
        Check(document.bones[0].name == "center" && document.morphs[0].name == "smile",
            "bone/morph names were not preserved");
        Check(document.cameras.size() == 2, "invalid camera or duplicate was not filtered");
        Check(document.cameras[0].frame == 10 && std::fabs(document.cameras[0].distance - 11.0f) < 1e-6f,
            "duplicate camera frame did not keep the last record");
        Check(document.cameras[1].frame == 20, "camera frames not sorted");

        auto body_only = minimal;
        const size_t camera_count_offset = 50 + 4 + 111 + 4 + 23;
        uint32_t no_cameras = 0;
        std::memcpy(body_only.data() + camera_count_offset, &no_cameras, sizeof(no_cameras));
        body_only.resize(camera_count_offset + sizeof(no_cameras));
        Check(BetterEndfield::Vmd::Parse(body_only, document, error), "body-only VMD rejected");
        Check(document.cameras.empty() && document.bones.size() == 1, "body-only records were lost");

        Check(BetterEndfield::Vmd::Parse(MakeFixture(true), document, error), "optional VMD sections rejected");
        Check(document.lights.size() == 1 && document.self_shadows.size() == 1 && document.ik.size() == 1,
            "optional VMD sections missing");
        Check(document.ik[0].toggles.size() == 1 && !document.ik[0].toggles[0].enabled,
            "IK toggle was not parsed");

        auto truncated = minimal;
        truncated.pop_back();
        Check(!BetterEndfield::Vmd::Parse(truncated, document, error), "truncated VMD accepted");

        Limits limits;
        limits.max_camera_keys = 1;
        Check(!BetterEndfield::Vmd::Parse(minimal, document, error, limits), "camera count limit ignored");
        std::cout << "vmd_parser: header, records, optional sections, deduplication and limits passed\n";
    } catch (const std::exception& exception) {
        std::cerr << exception.what() << '\n';
        return 1;
    }
}
