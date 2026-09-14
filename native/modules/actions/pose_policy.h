#pragma once
#include <algorithm>
#include <cmath>
#include <cstdint>
#include <cstring>
#include <istream>
#include <string>
#include <unordered_set>
#include <vector>

namespace BetterEndfield::Actions {
struct PoseVector { float x=0,y=0,z=0; };
struct PoseQuaternion { float x=0,y=0,z=0,w=1; };
struct BonePose { PoseQuaternion rotation; PoseVector position; };
static_assert(sizeof(BonePose)==28);
inline float PoseDot(PoseQuaternion a,PoseQuaternion b){return a.x*b.x+a.y*b.y+a.z*b.z+a.w*b.w;}
inline PoseQuaternion PoseNormalize(PoseQuaternion q){float n=std::sqrt(PoseDot(q,q));return {q.x/n,q.y/n,q.z/n,q.w/n};}
inline PoseQuaternion PoseSlerp(PoseQuaternion a,PoseQuaternion b,float t){
    float dot=PoseDot(a,b);if(dot<0){b={-b.x,-b.y,-b.z,-b.w};dot=-dot;}
    float x=1-t,y=t;
    if(dot<.9995f){float angle=std::acos(std::clamp(dot,-1.f,1.f)),s=std::sin(angle);x=std::sin((1-t)*angle)/s;y=std::sin(t*angle)/s;}
    return PoseNormalize({x*a.x+y*b.x,x*a.y+y*b.y,x*a.z+y*b.z,x*a.w+y*b.w});
}
inline BonePose PoseBlend(const BonePose& a,const BonePose& b,float t){
    return {PoseSlerp(a.rotation,b.rotation,t),{a.position.x+(b.position.x-a.position.x)*t,a.position.y+(b.position.y-a.position.y)*t,a.position.z+(b.position.z-a.position.z)*t}};
}
inline bool PoseSame(PoseVector a,PoseVector b){return std::fabs(a.x-b.x)<1e-6f&&std::fabs(a.y-b.y)<1e-6f&&std::fabs(a.z-b.z)<1e-6f;}
inline bool PoseSame(PoseQuaternion a,PoseQuaternion b){
    if(PoseDot(a,b)<0)b={-b.x,-b.y,-b.z,-b.w};
    return std::fabs(a.x-b.x)<1e-6f&&std::fabs(a.y-b.y)<1e-6f&&std::fabs(a.z-b.z)<1e-6f&&std::fabs(a.w-b.w)<1e-6f;
}
// Native animation may not write constant channels every frame. Strip only our
// unchanged previous output, preserving any fresh native edits independently.
inline BonePose PoseNativeBaseline(BonePose current,const BonePose& last,const BonePose& baseline,bool wrote){
    if(wrote){if(PoseSame(current.position,last.position))current.position=baseline.position;if(PoseSame(current.rotation,last.rotation))current.rotation=baseline.rotation;}
    return current;
}
struct PoseBone {std::string path;bool required=false;};
struct PoseBank {
    std::vector<PoseBone> bones;
    std::vector<BonePose> samples[2];
    uint32_t frames=0,period=0;
    float fps=0,phase=0,entry=0;
    template<class T> static bool Read(std::istream& in,T& v){return bool(in.read(reinterpret_cast<char*>(&v),sizeof(v)));}
    bool Load(std::istream& in,std::string& error){
        PoseBank next;char magic[8]{};uint32_t version=0,count=0;
        auto fail=[&](const char* s){error=s;return false;};
        if(!in.read(magic,8)||std::memcmp(magic,"BEPOSE12",8)||!Read(in,version)||version!=1||!Read(in,count)||
           !Read(in,next.frames)||!Read(in,next.fps)||!Read(in,next.period)||!Read(in,next.phase)||!Read(in,next.entry))return fail("invalid pose header");
        if(count<20||count>512||next.frames<2||next.frames>600||next.period+1!=next.frames||
           !std::isfinite(next.fps)||next.fps<10||next.fps>240||!std::isfinite(next.phase)||next.phase<0||next.phase>=next.period||
           !std::isfinite(next.entry)||next.entry<0||next.entry>.8f)return fail("invalid pose dimensions");
        std::unordered_set<std::string> names;unsigned required=0;
        for(uint32_t i=0;i<count;i++){
            uint32_t length=0,flags=0;
            if(!Read(in,length)||length<1||length>2048||!Read(in,flags)||flags>1)return fail("invalid bone record");
            PoseBone bone;bone.path.resize(length);bone.required=flags==1;
            if(!in.read(bone.path.data(),length)||bone.path[0]=='/'||bone.path.find("..")!=std::string::npos||
               bone.path.find_first_of(":\\")!=std::string::npos||bone.path.find('\0')!=std::string::npos||!names.insert(bone.path).second)return fail("invalid bone path");
            required+=flags;next.bones.push_back(std::move(bone));
        }
        if(required<10)return fail("missing required skeleton contract");
        for(auto& side:next.samples){
            side.resize(size_t(count)*next.frames);
            if(!in.read(reinterpret_cast<char*>(side.data()),side.size()*sizeof(BonePose)))return fail("truncated pose samples");
            for(auto& p:side){
                const float v[]={p.rotation.x,p.rotation.y,p.rotation.z,p.rotation.w,p.position.x,p.position.y,p.position.z};
                for(float f:v)if(!std::isfinite(f)||std::fabs(f)>50)return fail("invalid pose value");
                float n=PoseDot(p.rotation,p.rotation);if(n<.99f||n>1.01f)return fail("invalid pose quaternion");
                p.rotation=PoseNormalize(p.rotation);
            }
            for(size_t i=0;i<count;i++)if(!PoseSame(side[i].position,side[size_t(next.period)*count+i].position)||
                !PoseSame(side[i].rotation,side[size_t(next.period)*count+i].rotation))return fail("pose endpoint is not closed");
        }
        if(in.peek()!=std::char_traits<char>::eof())return fail("unexpected trailing pose data");
        *this=std::move(next);return true;
    }
    BonePose Sample(int side,size_t bone,double seconds)const{
        double frame=std::fmod(double(phase)+seconds*fps,double(period));if(frame<0)frame+=period;
        const auto a=static_cast<uint32_t>(frame);return PoseBlend(samples[side][size_t(a)*bones.size()+bone],samples[side][size_t(a+1)*bones.size()+bone],float(frame-a));
    }
};
struct PoseClock {
    enum class State {Waiting,Playing,Fading,Done};
    State state=State::Waiting;double seconds=0;float weight=0;int last_frame=-1;int side=-1;
    bool Tick(int frame,float delta,bool allowed,int requested_side,float normalized,float length,float entry){
        if(frame==last_frame)return state==State::Playing||state==State::Fading;
        last_frame=frame;
        if(!std::isfinite(delta)||delta<0||!std::isfinite(normalized)||!std::isfinite(length)||length<=0){state=State::Done;weight=0;return false;}
        if(state==State::Waiting){
            if(!allowed)return false;
            if(requested_side<0||requested_side>1||normalized<entry)return false;
            side=requested_side;seconds=std::max(0.,double(normalized-entry)*length);state=State::Playing;
        }else if(state==State::Playing&&(!allowed||requested_side!=side))End();
        else if(state==State::Playing)seconds+=delta;
        if(state==State::Fading){seconds+=delta;weight=std::max(0.f,weight-delta/.12f);if(weight<=0)state=State::Done;}
        else if(state==State::Playing)weight=std::min(1.f,weight+delta/.12f);
        return weight>0;
    }
    void End(){if(state==State::Waiting){state=State::Done;weight=0;}else if(state==State::Playing)state=State::Fading;}
};
}
