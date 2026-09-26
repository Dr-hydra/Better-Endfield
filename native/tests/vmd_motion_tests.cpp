#include "BetterEndfield/vmd_motion.h"

#include <cmath>
#include <iostream>
#include <stdexcept>

using BetterEndfield::Vmd::BoneKey;
using BetterEndfield::Vmd::BuildMotion;
using BetterEndfield::Vmd::Document;
using BetterEndfield::Vmd::MorphKey;
using BetterEndfield::Vmd::SampleBone;
using BetterEndfield::Vmd::SampleMorph;

namespace {

void Check(bool condition, const char* message) {
    if (!condition) throw std::runtime_error(message);
}

} // namespace

int main() {
    try {
        Document document;
        BoneKey first;
        first.name = "センター";
        first.frame = 0;
        first.rotation = {0.0f, 0.0f, 0.0f, 1.0f};
        BoneKey second = first;
        second.frame = 30;
        second.position = {3.0f, 6.0f, 9.0f};
        second.rotation = {0.0f, 0.0f, 1.0f, 0.0f};
        second.interpolation.fill(64);
        document.bones = {first, second};

        MorphKey morph_first;
        morph_first.name = "笑い";
        morph_first.frame = 0;
        morph_first.weight = 0.0f;
        MorphKey morph_second = morph_first;
        morph_second.frame = 30;
        morph_second.weight = 100.0f;
        document.morphs = {morph_first, morph_second};

        const auto motion = BuildMotion(document);
        BetterEndfield::Vmd::BoneSample bone;
        Check(SampleBone(motion, "センター", 15.0, bone), "bone sample failed");
        Check(std::fabs(bone.position.x - 1.5f) < 1.0e-3f &&
            std::fabs(bone.position.y - 3.0f) < 1.0e-3f,
            "bone interpolation failed");
        Check(std::fabs(bone.rotation.w - 0.7071067f) < 1.0e-3f,
            "quaternion interpolation failed");

        BetterEndfield::Vmd::MorphSample sampled_morph;
        Check(SampleMorph(motion, "笑い", 15.0, sampled_morph), "morph sample failed");
        Check(std::fabs(sampled_morph.weight - 50.0f) < 1.0e-3f,
            "morph interpolation failed");
        Check(!SampleMorph(motion, "missing", 15.0, sampled_morph),
            "missing morph was reported as present");
        std::cout << "vmd_motion: grouped timelines, Bezier sampling, slerp and morph sampling passed\n";
    } catch (const std::exception& exception) {
        std::cerr << exception.what() << '\n';
        return 1;
    }
}
