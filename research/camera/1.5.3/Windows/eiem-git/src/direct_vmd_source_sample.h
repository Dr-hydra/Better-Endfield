#pragma once
#include "direct_vmd_source.h"
#include "direct_vmd_knee.h"

namespace eiem_source {
struct Evaluated {
  VmdVec3 position{};
  VmdQuaternion rotation{0, 0, 0, 1};
};
struct ClipBinding {
  VmdFile canonical;
  std::vector<const VmdBoneTimeline *> tracks;
  std::vector<std::unique_ptr<VmdBoneTimeline>> fixedAxisTracks;
  mutable std::vector<Evaluated> world, local,
      append;
  std::string ikNames[2];
  std::string report;
  bool legacy = false;
};
inline VmdQuaternion FixedAxisKeyRotation(VmdQuaternion q, VmdVec3 axis) {
  q = DirectVmdNormalizeQuaternion(q);
  if (q.w < 0)
    q = {-q.x, -q.y, -q.z, -q.w};
  const float sine = std::sqrt(q.x * q.x + q.y * q.y + q.z * q.z);
  if (sine < 1e-8f)
    return {0, 0, 0, 1};
  const float dot = q.x * axis.x + q.y * axis.y + q.z * axis.z;
  const float signedSine = dot < 0 ? -sine : sine;
  return {axis.x * signedSine, axis.y * signedSine,
          axis.z * signedSine, q.w};
}
inline ClipBinding BindClip(const Prepared &p, const VmdFile &clip) {
  ClipBinding b;
  b.legacy = !p.pmx && p.config.tracks.empty() && p.config.semantics.empty();
  if (b.legacy)
    return b;
  if (p.pmx) {
    const auto count = p.skeleton.bones.size();
    b.tracks.resize(count);
    b.world.resize(count);
    b.local.resize(count);
    b.append.resize(count);
  } else {
    b.canonical.loaded = clip.loaded;
    b.canonical.totalFrames = clip.totalFrames;
  }
  size_t ignored = 0, unmapped = 0;
  std::string unmappedNames;
  std::set<std::string> used;
  auto sourceName = [&](const std::string &track) {
    const auto rule = p.config.tracks.find(track);
    return rule == p.config.tracks.end() ? track : rule->second;
  };
  for (const auto &t : clip.boneTimelines) {
    std::string dest = sourceName(t.first);
    if (dest == "-") {
      ++ignored;
      continue;
    }
    auto n = p.names.find(dest);
    if (p.pmx) {
      if (n == p.names.end()) {
        ++unmapped;
        if (unmapped <= 12)
          unmappedNames += (unmappedNames.empty() ? "" : ", ") + t.first;
        continue;
      }
      Require(n->second >= 0, "Ambiguous source alias: " + dest);
      Require(!b.tracks[n->second],
              "Multiple VMD tracks drive source bone: " + dest);
      b.tracks[n->second] = &t.second;
      const auto &bone = p.skeleton.bones[n->second];
      if (p.needed[n->second] && (bone.flags & 0x400)) {
        auto track = std::make_unique<VmdBoneTimeline>(t.second);
        for (auto &key : track->keys) {
          const auto q = FixedAxisKeyRotation(
              {key.rot[0], key.rot[1], key.rot[2], key.rot[3]}, bone.fixedAxis);
          key.rot[0] = q.x;
          key.rot[1] = q.y;
          key.rot[2] = q.z;
          key.rot[3] = q.w;
        }
        b.tracks[n->second] = track.get();
        b.fixedAxisTracks.push_back(std::move(track));
      }
    } else {
      if (n != p.names.end())
        dest = kDirectVmdBoneSpecs[n->second].name;
      for (const auto &s : kDirectVmdSemiStandardSpecs)
        if (dest == s.name || (s.alternateName && dest == s.alternateName)) {
          dest = s.name;
          break;
        }
      Require(used.insert(dest).second,
              "Multiple VMD aliases drive source bone: " + dest);
      b.canonical.boneTimelines.emplace(dest, t.second);
    }
  }
  if (p.pmx) {
    const auto count = p.skeleton.bones.size();
    std::vector<double> localBound(count), appendBound(count),
        worldBound(count);
    for (size_t i = 0; i < count; ++i)
      if (p.needed[i] && b.tracks[i]) {
        double axisMax[3] = {};
        for (const auto &key : b.tracks[i]->keys) {
          for (int axis = 0; axis < 3; ++axis) {
            const double value = std::fabs(double(key.pos[axis]));
            Require(std::isfinite(value), "Non-finite VMD source translation");
            axisMax[axis] = (std::max)(axisMax[axis], value);
          }
        }
        localBound[i] = axisMax[0] + axisMax[1] + axisMax[2];
      }
    for (int i : p.skeleton.order)
      if (p.needed[i]) {
        const auto &bone = p.skeleton.bones[i];
        if (bone.append >= 0 && (bone.flags & 0x200)) {
          const bool own =
              (bone.flags & 0x80) || p.skeleton.bones[bone.append].append < 0;
          appendBound[i] =
              std::fabs(double(bone.weight)) *
              (own ? localBound[bone.append] : appendBound[bone.append]);
        }
        const auto parentBind = bone.parent < 0
                                    ? VmdVec3{}
                                    : p.skeleton.bones[bone.parent].position;
        worldBound[i] =
            (bone.parent < 0 ? 0 : worldBound[bone.parent]) +
            DirectVmdLength(DirectVmdSub(bone.position, parentBind)) +
            localBound[i] + appendBound[i];
        Require(std::isfinite(worldBound[i]) && worldBound[i] < 1e8,
                "PMX/VMD translation dependency budget exceeded: " + bone.name);
      }
  }
  for (const auto &t : clip.ikTimelines) {
    const auto dest = sourceName(t.first);
    if (dest == "-")
      continue;
    if (!p.pmx) {
      int semantic = Semantic(dest);
      const std::string key =
          semantic >= 0 ? kDirectVmdBoneSpecs[semantic].name : dest;
      Require(b.canonical.ikTimelines.emplace(key, t.second).second,
              "Multiple IK aliases: " + key);
    } else {
      auto found = p.names.find(dest);
      if (found == p.names.end())
        continue;
      Require(found->second >= 0, "Ambiguous IK alias: " + dest);
      for (int side = 0; side < 2; ++side) {
        const int semantic = int(side ? DirectVmdBoneId::RightFootIk
                                      : DirectVmdBoneId::LeftFootIk);
        if (found->second == p.semantic[semantic]) {
          Require(b.ikNames[side].empty(),
                  "Multiple VMD IK switches drive one foot");
          b.ikNames[side] = t.first;
        }
      }
    }
  }
  b.report = "Ignored tracks=" + std::to_string(ignored) +
             "; unmapped tracks=" + std::to_string(unmapped) +
             (unmappedNames.empty() ? "" : " [" + unmappedNames + "]") +
             "; fixed-axis tracks=" + std::to_string(b.fixedAxisTracks.size());
  return b;
}
inline VmdQuaternion Power(VmdQuaternion q, float weight) {
  q = DirectVmdNormalizeQuaternion(q);
  if (q.w < 0) {
    q.x = -q.x;
    q.y = -q.y;
    q.z = -q.z;
    q.w = -q.w;
  }
  const float len = std::sqrt(q.x * q.x + q.y * q.y + q.z * q.z);
  if (len < 1e-8f)
    return {0, 0, 0, 1};
  const float angle = std::atan2(len, q.w) * weight, s = std::sin(angle) / len;
  return {q.x * s, q.y * s, q.z * s, std::cos(angle)};
}
inline void Sample(const Prepared &p, const ClipBinding &binding,
                   const VmdFile &clip, double time,
                   DirectVmdSampleFrame *out) {
  out->knees[0] = out->knees[1] = {};
  DirectVmdSampleFrame sampled;
  if (!p.pmx) {
    const VmdFile &source = binding.legacy ? clip : binding.canonical;
    DirectVmdSampleClip(source, time, out->sequence, out->rigGeneration,
                        out->clipGeneration, out->ownerCharacter, out->playback,
                        &sampled, true, false);
    if (binding.legacy) {
      for (uint32_t i = 0; i < DIRECT_VMD_BONE_COUNT; ++i)
        out->bones[i] = sampled.bones[i];
    } else {
      VmdQuaternion world[DIRECT_VMD_BONE_COUNT],
          destination[DIRECT_VMD_BONE_COUNT];
      for (uint32_t i = 0; i < DIRECT_VMD_BONE_COUNT; ++i) {
        const int parent = kDirectVmdBoneSpecs[i].parent;
        const auto local = sampled.bones[i].hasTrack
                               ? sampled.bones[i].rotation
                               : VmdQuaternion{0, 0, 0, 1};
        world[i] = parent < 0
                       ? local
                       : DirectVmdQuaternionMultiply(world[parent], local);
      }
      for (uint32_t i = 0; i < DIRECT_VMD_BONE_COUNT; ++i) {
        const int parent = kDirectVmdBoneSpecs[i].parent, src = p.semantic[i];
        destination[i] = src >= 0 ? world[src]
                                  : (parent < 0 ? VmdQuaternion{0, 0, 0, 1}
                                                : destination[parent]);
        out->bones[i] =
            src >= 0 ? sampled.bones[src] : DirectVmdBoneSamplePod{};
        out->bones[i].rotation =
            parent < 0 ? destination[i]
                       : DirectVmdQuaternionMultiply(
                             DirectVmdQuaternionInverse(destination[parent]),
                             destination[i]);
        out->bones[i].hasTrack = src >= 0;
      }
    }
    out->leftFootIkEnabled = sampled.leftFootIkEnabled;
    out->rightFootIkEnabled = sampled.rightFootIkEnabled;
    VmdQuaternion sourceWorld[DIRECT_VMD_BONE_COUNT];
    for (uint32_t i = 0; i < DIRECT_VMD_BONE_COUNT; ++i) {
      const int parent = kDirectVmdBoneSpecs[i].parent;
      const auto local = sampled.bones[i].hasTrack ? sampled.bones[i].rotation
                                                  : VmdQuaternion{0,0,0,1};
      sourceWorld[i] = parent < 0 ? local
          : DirectVmdQuaternionMultiply(sourceWorld[parent], local);
    }
    for (int side = 0; side < 2; ++side) {
      const int thigh = p.semantic[int(kDirectVmdPhase5LegFkBones[side][0])];
      const int knee = p.semantic[int(kDirectVmdPhase5LegFkBones[side][1])];
      const int ankle = p.semantic[int(kDirectVmdPhase5LegFkBones[side][2])];
      if (thigh < 0 || knee < 0 || ankle < 0 ||
          kDirectVmdBoneSpecs[knee].parent != thigh ||
          kDirectVmdBoneSpecs[ankle].parent != knee) continue;
      VmdVec3 upperAxis{}, lowerAxis{};
      if (!DirectVmdTryNormalizeVector(
              p.reference.direction[int(kDirectVmdPhase5LegFkBones[side][0])], &upperAxis))
        upperAxis = {0,-1,0};
      if (!DirectVmdTryNormalizeVector(
              p.reference.direction[int(kDirectVmdPhase5LegFkBones[side][1])], &lowerAxis))
        lowerAxis = {0,-1,0};
      const auto kneeRotation = sampled.bones[knee].rotation;
      out->knees[side] = DirectVmdSourceKneeReference(
          DirectVmdRotateVector(sourceWorld[thigh], upperAxis),
          DirectVmdRotateVector(sourceWorld[knee], lowerAxis),
          sampled.bones[knee].hasTrack != 0 &&
          kneeRotation.x*kneeRotation.x + kneeRotation.y*kneeRotation.y +
              kneeRotation.z*kneeRotation.z > 1e-8f);
    }
  } else {
    const auto &s = p.skeleton;
    auto &world = binding.world;
    auto &local = binding.local;
    auto &append = binding.append;
    for (int i : s.order) {
      if (!p.needed[i])
        continue;
      append[i] = Evaluated{};
      const auto &bone = s.bones[i];
      VmdBoneSample key;
      if (binding.tracks[i])
        SampleVmdBone(*binding.tracks[i], time, &key);
      local[i].position = key.position;
      local[i].rotation = key.rotation;
      if (bone.append >= 0) {
        const auto &dep = s.bones[bone.append];
        const bool own = (bone.flags & 0x80) || dep.append < 0;
        const auto &a = own ? local[bone.append] : append[bone.append];
        if (bone.flags & 0x100)
          append[i].rotation = Power(a.rotation, bone.weight);
        if (bone.flags & 0x200)
          append[i].position = DirectVmdScale(a.position, bone.weight);
      }
      const auto rotation =
          DirectVmdQuaternionMultiply(local[i].rotation, append[i].rotation);
      const auto delta = DirectVmdAdd(local[i].position, append[i].position);
      const auto parent = bone.parent >= 0 ? world[bone.parent] : Evaluated{};
      const auto bindParent =
          bone.parent >= 0 ? s.bones[bone.parent].position : VmdVec3{};
      world[i].rotation =
          DirectVmdQuaternionMultiply(parent.rotation, rotation);
      world[i].position = DirectVmdAdd(
          parent.position,
          DirectVmdRotateVector(
              parent.rotation,
              DirectVmdAdd(DirectVmdSub(bone.position, bindParent), delta)));
      Require(Finite(world[i].position) &&
                  DirectVmdLength(world[i].position) < 1e8f,
              "Source evaluation exceeded finite/position budget");
    }
    for (int side = 0; side < 2; ++side) {
      const int thigh = p.semantic[int(kDirectVmdPhase5LegFkBones[side][0])];
      const int knee = p.semantic[int(kDirectVmdPhase5LegFkBones[side][1])];
      const int ankle = p.semantic[int(kDirectVmdPhase5LegFkBones[side][2])];
      if (thigh < 0 || knee < 0 || ankle < 0) continue;
      const auto q = DirectVmdQuaternionMultiply(local[knee].rotation, append[knee].rotation);
      const bool effective = s.bones[knee].parent == thigh &&
          s.bones[ankle].parent == knee &&
          q.x*q.x + q.y*q.y + q.z*q.z > 1e-8f;
      out->knees[side] = DirectVmdSourceKneeReference(
          DirectVmdSub(world[knee].position, world[thigh].position),
          DirectVmdSub(world[ankle].position, world[knee].position), effective);
    }
    Evaluated semantic[DIRECT_VMD_BONE_COUNT];
    VmdVec3 bind[DIRECT_VMD_BONE_COUNT]{};
    for (uint32_t i = 0; i < DIRECT_VMD_BONE_COUNT; ++i) {
      const int src = p.semantic[i], parent = kDirectVmdBoneSpecs[i].parent;
      const auto pw = parent < 0 ? Evaluated{} : semantic[parent];
      const auto pb = parent < 0 ? VmdVec3{} : bind[parent];
      semantic[i] = src >= 0 ? world[src] : pw;
      bind[i] = src >= 0 ? s.bones[src].position : pb;
      const auto inv = DirectVmdQuaternionInverse(pw.rotation);
      out->bones[i].rotation =
          DirectVmdQuaternionMultiply(inv, semantic[i].rotation);
      out->bones[i].position = DirectVmdSub(
          DirectVmdRotateVector(
              inv, DirectVmdSub(semantic[i].position, pw.position)),
          DirectVmdSub(bind[i], pb));
      if (parent < 0 && src >= 0) {
        out->bones[i].position = DirectVmdSub(
            semantic[i].position,
            DirectVmdRotateVector(semantic[i].rotation, bind[i]));
      }
      out->bones[i].hasTrack = src >= 0;
    }
    out->leftFootIkEnabled =
        binding.ikNames[0].empty()
            ? 1
            : SampleVmdIkEnabled(clip, binding.ikNames[0], time);
    out->rightFootIkEnabled =
        binding.ikNames[1].empty()
            ? 1
            : SampleVmdIkEnabled(clip, binding.ikNames[1], time);
  }
  for (uint32_t i = 0; i < DIRECT_VMD_BONE_COUNT; ++i)
    if (!p.reference.controlled[i])
      out->bones[i] = DirectVmdBoneSamplePod{};
  for (int side = 0; side < 2; ++side) {
    bool controlled = true;
    for (auto id : kDirectVmdPhase5LegFkBones[side])
      controlled = controlled && p.reference.controlled[DirectVmdBoneIndex(id)];
    controlled =
        controlled &&
        p.reference.controlled[int(side ? DirectVmdBoneId::RightFootIk
                                        : DirectVmdBoneId::LeftFootIk)];
    if (!controlled)
      (side ? out->rightFootIkEnabled : out->leftFootIkEnabled) = 0;
    if (!(side ? out->rightFootIkEnabled : out->leftFootIkEnabled))
      out->knees[side] = {};
  }
  out->sourceRevision = p.revision;
}
}
