#pragma once
// Pure data side of the BoneCloth skirt binding (EIEM-style visible physics):
// new joints are created under the live pelvis with identity local rotation,
// the receiver mesh gains one bindpose per joint, and selected vertices are
// re-skinned onto those joints. Native BoneCloth then moves the Transforms and
// ordinary skinning displays the result. No vertex positions are written.
#include "cloth_display_binding.h"
#include <algorithm>
#include <array>
#include <cstdint>
#include <cstring>
#include <stdexcept>
#include <string>
#include <utility>
#include <vector>
#include <optional>

namespace BetterEndfieldNext::CustomModel::ClothBone {
namespace CD = ClothDisplay;

struct Joint {
    std::string name;
    int parent = -1;  // -1: pelvis
    CD::V mesh_position;  // BEM mesh-local bind space
    std::optional<CD::M> mesh_frame; // source-authored orientation and bind origin
};
struct Placement {
    std::vector<CD::V> local_positions;  // relative to the parent Transform
    std::vector<CD::M> bindposes;        // mesh -> joint local
    std::vector<CD::M> local_rotations;
};

inline CD::M Translation(CD::V t) {
    return {1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, t.x, t.y, t.z, 1};
}

// Joints carry identity rotation relative to the pelvis, so every joint frame
// is the pelvis frame translated by its pelvis-local bind position.
inline Placement Place(const std::vector<Joint>& joints, const CD::M& pelvis_bindpose) {
    Placement out;
    std::vector<CD::M> pelvis_frames;
    for (size_t i = 0; i < joints.size(); ++i) {
        const auto& j = joints[i];
        if (j.parent >= static_cast<int>(i)) throw std::runtime_error("joint parent must precede child");
        const CD::V p = CD::Point(pelvis_bindpose, j.mesh_position);
        for (double v : {p.x, p.y, p.z})
            if (!std::isfinite(v) || std::abs(v) > 10) throw std::runtime_error("joint position out of range");
        auto frame=j.mesh_frame?CD::Multiply(pelvis_bindpose,*j.mesh_frame):Translation(p);
        if(j.mesh_frame) {
            const CD::V origin{(*j.mesh_frame)[12],(*j.mesh_frame)[13],(*j.mesh_frame)[14]};
            if(CD::Dot(origin-j.mesh_position,origin-j.mesh_position)>1e-8)throw std::runtime_error("joint frame origin differs from bind position");
        }
        auto local=j.parent<0?frame:CD::Multiply(CD::Inverse(pelvis_frames[size_t(j.parent)]),frame);
        out.local_positions.push_back({local[12],local[13],local[14]});
        local[12]=local[13]=local[14]=0;
        for(int axis=0;axis<3;++axis) {
            CD::V a{local[axis*4],local[axis*4+1],local[axis*4+2]};
            if(std::abs(CD::Dot(a,a)-1)>1e-4)throw std::runtime_error("joint rotation frame is not unit scale");
            for(int previous=0;previous<axis;++previous) {
                CD::V b{local[previous*4],local[previous*4+1],local[previous*4+2]};
                if(std::abs(CD::Dot(a,b))>1e-4)throw std::runtime_error("joint rotation frame is not orthogonal");
            }
        }
        CD::V x{local[0],local[1],local[2]},y{local[4],local[5],local[6]},z{local[8],local[9],local[10]};
        if(CD::Dot(CD::Cross(x,y),z)<0.9999)throw std::runtime_error("joint rotation frame reflected");
        out.local_rotations.push_back(local);pelvis_frames.push_back(frame);
        out.bindposes.push_back(CD::Multiply(CD::Inverse(frame),pelvis_bindpose));
    }
    return out;
}
inline std::array<double,4> Quaternion(const CD::M& m) {
    const double trace=m[0]+m[5]+m[10];std::array<double,4> q{};
    if(trace>0) {
        const double s=2*std::sqrt(trace+1);q={(m[6]-m[9])/s,(m[8]-m[2])/s,(m[1]-m[4])/s,s/4};
    } else if(m[0]>m[5]&&m[0]>m[10]) {
        const double s=2*std::sqrt(1+m[0]-m[5]-m[10]);q={s/4,(m[4]+m[1])/s,(m[8]+m[2])/s,(m[6]-m[9])/s};
    } else if(m[5]>m[10]) {
        const double s=2*std::sqrt(1+m[5]-m[0]-m[10]);q={(m[4]+m[1])/s,s/4,(m[9]+m[6])/s,(m[8]-m[2])/s};
    } else {
        const double s=2*std::sqrt(1+m[10]-m[0]-m[5]);q={(m[8]+m[2])/s,(m[9]+m[6])/s,s/4,(m[1]-m[4])/s};
    }
    const double n=std::sqrt(q[0]*q[0]+q[1]*q[1]+q[2]*q[2]+q[3]*q[3]);
    for(auto& v:q){v/=n;if(!std::isfinite(v))throw std::runtime_error("joint quaternion invalid");}
    return q;
}

struct Influence {
    int palette = -1;
    double weight = 0;
};

// stream 2 layout of the receiver: 4 x UNorm16 weights, 4 x UInt8 indices.
inline void EncodeSkin(uint8_t* vertex, std::vector<Influence> influences) {
    std::sort(influences.begin(), influences.end(), [](const auto& a, const auto& b) { return a.weight > b.weight; });
    if (influences.size() > 4) influences.resize(4);
    double total = 0;
    for (const auto& i : influences) {
        if (i.palette < 0 || i.palette > 255 || !std::isfinite(i.weight) || i.weight < 0)
            throw std::runtime_error("skin influence invalid");
        total += i.weight;
    }
    if (total <= 1e-6) throw std::runtime_error("skin influence total invalid");
    std::array<uint16_t, 4> weights{};
    std::array<uint8_t, 4> bones{};
    int32_t sum = 0;
    for (size_t k = 0; k < influences.size(); ++k) {
        weights[k] = static_cast<uint16_t>(std::lround(influences[k].weight / total * 65535.0));
        bones[k] = static_cast<uint8_t>(influences[k].palette);
        sum += weights[k];
    }
    weights[0] = static_cast<uint16_t>(weights[0] + (65535 - sum));
    std::memcpy(vertex, weights.data(), 8);
    std::memcpy(vertex + 8, bones.data(), 4);
}
}  // namespace BetterEndfieldNext::CustomModel::ClothBone
