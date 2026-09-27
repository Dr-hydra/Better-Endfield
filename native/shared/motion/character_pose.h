#pragma once
#include "vmd.h"
#include <memory>
#include <set>
#include <unordered_set>

namespace BetterEndfield::CharacterPose {
using Vmd::Vec3;
using Vmd::Quaternion;
inline Quaternion Inverse(Quaternion q) { q = Vmd::Normalize(q); return {-q.x,-q.y,-q.z,q.w}; }
inline Quaternion Multiply(Quaternion a, Quaternion b) {
    return Vmd::Normalize({a.w*b.x+a.x*b.w+a.y*b.z-a.z*b.y,
        a.w*b.y-a.x*b.z+a.y*b.w+a.z*b.x, a.w*b.z+a.x*b.y-a.y*b.x+a.z*b.w,
        a.w*b.w-a.x*b.x-a.y*b.y-a.z*b.z});
}
inline bool Finite(Quaternion q) {
    const double n = double(q.x)*q.x+double(q.y)*q.y+double(q.z)*q.z+double(q.w)*q.w;
    return std::isfinite(n) && n > 0.25 && n < 4;
}
inline bool Same(Quaternion a, Quaternion b) {
    if (!Finite(a) || !Finite(b)) return false;
    a=Vmd::Normalize(a); b=Vmd::Normalize(b);
    const double dot = double(a.x)*b.x+double(a.y)*b.y+double(a.z)*b.z+double(a.w)*b.w;
    return Finite(a) && Finite(b) && std::abs(dot) > 0.999999;
}
inline bool Same(float a, float b) { return std::isfinite(a) && std::isfinite(b) && std::abs(a-b) <= .001f; }

// The captured reference pose is NOT a bind pose. This is a relative FK preview:
// source frame zero is calibrated to the pose captured on the owning game thread.
// Per-bone basis conjugation avoids assuming the target's local axes are MMD axes.
inline Quaternion RelativeRotation(Quaternion reference, Quaternion basis,
    Quaternion source_zero, Quaternion source_now, float weight) {
    Quaternion delta = Multiply(Inverse(source_zero), source_now);
    delta = Multiply(Multiply(basis, delta), Inverse(basis));
    return Multiply(reference, Vmd::Slerp({}, delta, std::clamp(weight,0.0f,1.0f)));
}
struct Bone {
    uintptr_t target = 0;
    std::vector<std::string> sources; // CP932, ordered (e.g. both-eyes then left-eye)
    Quaternion basis;
    Quaternion reference, restore, last, source_zero;
    bool wrote = false;
};
struct Morph {
    uintptr_t target = 0;
    int index = -1;
    std::vector<std::string> sources; // several blink contributors combine by max
    float restore = 0, last = 0;
    bool wrote = false;
};
// Opaque handles must remain pinned throughout a session. Every method runs on
// the game-thread caller; this portable layer never follows an IL2CPP pointer.
struct Backend {
    virtual ~Backend() = default;
    virtual bool ValidBone(uintptr_t target) = 0;
    virtual bool ReadRotation(uintptr_t target, Quaternion& value) = 0;
    virtual bool WriteRotation(uintptr_t target, Quaternion value) = 0;
    virtual bool ValidMorph(uintptr_t target, int index) = 0;
    virtual bool ReadMorph(uintptr_t target, int index, float& value) = 0;
    virtual bool WriteMorph(uintptr_t target, int index, float value) = 0;
};
class Session final {
public:
    bool Start(std::shared_ptr<const Vmd::Motion> motion, std::vector<Bone> bones,
        std::vector<Morph> morphs, Backend& backend, std::string& error) {
        if (motion_ || !motion || (bones.empty() && morphs.empty()) || bones.size()>128 || morphs.size()>256) {
            error = "empty, busy or oversized pose session"; return false;
        }
        std::unordered_set<uintptr_t> seen;
        for (auto& bone : bones) {
            if (!bone.target || !seen.insert(bone.target).second || !Finite(bone.basis) ||
                !SourcesExist(motion->bones,bone.sources) || !backend.ValidBone(bone.target) ||
                !backend.ReadRotation(bone.target,bone.reference) || !Finite(bone.reference)) {
                error = "invalid/duplicate bone binding or reference pose"; return false;
            }
            bone.reference = Vmd::Normalize(bone.reference); bone.restore=bone.reference;
            bone.source_zero=SampleRotation(*motion,bone.sources,0); bone.wrote=false;
        }
        std::set<std::pair<uintptr_t,int>> morph_seen;
        for (auto& morph : morphs) {
            if (!morph.target || !morph_seen.emplace(morph.target,morph.index).second ||
                !SourcesExist(motion->morphs,morph.sources) || !backend.ValidMorph(morph.target,morph.index) ||
                !backend.ReadMorph(morph.target,morph.index,morph.restore) || !std::isfinite(morph.restore)) {
                error = "invalid/duplicate morph binding"; return false;
            }
            morph.wrote=false;
        }
        rotations_.resize(bones.size()); weights_.resize(morphs.size());
        motion_=std::move(motion); bones_=std::move(bones); morphs_=std::move(morphs);
        error.clear(); return true;
    }
    // Validate/read the entire pose first. On a write failure restore only values
    // still equal to this session's last write; never undo another writer's value.
    bool Apply(double frame, float weight, Backend& backend, std::string& error) {
        if (!motion_ || !std::isfinite(frame) || frame<0 || !std::isfinite(weight) || weight<0 || weight>1) {
            error="invalid pose time/weight or inactive session"; return false;
        }
        size_t bone_index=0,morph_index=0;
        for (auto& bone : bones_) {
            Quaternion current;
            if (!backend.ValidBone(bone.target) || !backend.ReadRotation(bone.target,current) || !Finite(current))
                return Fail(backend,error,"bone was destroyed, reparented or unreadable");
            if (!bone.wrote || !Same(current,bone.last)) bone.restore=current;
            auto q=RelativeRotation(bone.reference,bone.basis,bone.source_zero,
                SampleRotation(*motion_,bone.sources,frame),1);
            q=Vmd::Slerp(bone.restore,q,weight);
            if (!Finite(q)) return Fail(backend,error,"non-finite sampled pose");
            rotations_[bone_index++]=q;
        }
        for (auto& morph : morphs_) {
            float current=0;
            if (!backend.ValidMorph(morph.target,morph.index) || !backend.ReadMorph(morph.target,morph.index,current) || !std::isfinite(current))
                return Fail(backend,error,"morph renderer/mesh changed or is unreadable");
            if (!morph.wrote || !Same(current,morph.last)) morph.restore=current;
            float sample=0;
            for (const auto& name:morph.sources) {
                float value=Vmd::SampleMorph(motion_->morphs.at(name),frame);
                if(!std::isfinite(value)) return Fail(backend,error,"non-finite morph sample");
                sample=std::max(sample,value);
            }
            sample=std::clamp(sample,0.0f,1.0f)*100;
            weights_[morph_index++]=morph.restore+(sample-morph.restore)*weight;
        }
        for (size_t i=0;i<bones_.size();++i) {
            auto& bone=bones_[i];
            if (!backend.WriteRotation(bone.target,rotations_[i])) return Fail(backend,error,"bone write failed");
            bone.last=rotations_[i]; bone.wrote=true;
        }
        for (size_t i=0;i<morphs_.size();++i) {
            auto& morph=morphs_[i];
            if (!backend.WriteMorph(morph.target,morph.index,weights_[i])) return Fail(backend,error,"morph write failed");
            morph.last=weights_[i]; morph.wrote=true;
        }
        error.clear(); return true;
    }
    void Stop(Backend& backend, bool restore=true) {
        if (restore) {
            for (auto& bone:bones_) {
                Quaternion current;
                if (bone.wrote && backend.ValidBone(bone.target) && backend.ReadRotation(bone.target,current) && Same(current,bone.last))
                    backend.WriteRotation(bone.target,bone.restore);
            }
            for (auto& morph:morphs_) {
                float current=0;
                if (morph.wrote && backend.ValidMorph(morph.target,morph.index) && backend.ReadMorph(morph.target,morph.index,current) && Same(current,morph.last))
                    backend.WriteMorph(morph.target,morph.index,morph.restore);
            }
        }
        bones_.clear(); morphs_.clear(); motion_.reset();
    }
    bool Active() const { return bool(motion_); }
private:
    template<class Tracks> static bool SourcesExist(const Tracks& tracks,const std::vector<std::string>& names) {
        if(names.empty() || names.size()>4) return false;
        for(const auto& name:names) {auto it=tracks.find(name); if(it==tracks.end() || it->second.empty()) return false;}
        return true;
    }
    static Quaternion SampleRotation(const Vmd::Motion& motion,const std::vector<std::string>& sources,double frame) {
        Quaternion q;
        for(const auto& name:sources) { Vmd::BoneSample value; Vmd::SampleBone(motion.bones.at(name),frame,value); q=Multiply(q,value.rotation); }
        return q;
    }
    bool Fail(Backend& backend,std::string& error,const char* message) {Stop(backend);error=message;return false;}
    std::shared_ptr<const Vmd::Motion> motion_;
    std::vector<Bone> bones_;
    std::vector<Morph> morphs_;
    std::vector<Quaternion> rotations_;
    std::vector<float> weights_;
};

// Time is monotonic seconds supplied by caller, independent of game timeScale.
// The last pose is displayed once before the next tick requests restoration.
class Clock {
public:
    void Start(double now,double duration,bool loop) {last_=now;time_=0;duration_=std::max(0.0,duration);loop_=loop;paused_=false;finished_=false;}
    void Pause(bool value,double now) {paused_=value;last_=now;}
    bool Advance(double now,double& seconds) {
        if(!std::isfinite(now) || now<last_ || finished_) return false;
        if(!paused_) time_+=now-last_;
        last_=now;
        if(loop_ && duration_>0) time_=std::fmod(time_,duration_);
        else if(time_>=duration_) {time_=duration_;finished_=true;}
        seconds=time_;return true;
    }
    bool Paused()const{return paused_;}
private:
    double last_=0,time_=0,duration_=0;
    bool loop_=false,paused_=false,finished_=false;
};
} // namespace BetterEndfield::CharacterPose
