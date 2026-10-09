// Offline owned-object host fixture; never attaches to or loads a game.
#include <cassert>
#include <deque>
#include <iostream>
#include <unordered_map>
#include "../../eiem_slot0.cpp"
using namespace eiem_slot_0;
namespace fixture {
struct Object;
struct Method { std::string name, result; std::vector<std::string> params; };
struct Class { std::string name; bool value = false; std::vector<Method*> methods; };
struct Object {
  Class* klass;
  std::array<unsigned char, 64> payload{};
  std::string text;
  std::vector<Object*> items;
  std::map<std::string,Object*> fields;
  Object* root = nullptr;
  Object* parent = nullptr;
  std::map<int,Object*> human;
  std::vector<Object*> children, components;
  VmdVec3 position{0,0,0};
  VmdQuaternion rotation{0,0,0,1};
  bool alive = true, enabled = true;
};
std::deque<Class> classes;
std::deque<Method> methodStore;
std::deque<Object> objects;
std::deque<std::string> fields;
std::unordered_map<void*,Object*> payloadOwners;
std::unordered_map<uint32_t,Object*> handles;
uint32_t nextHandle = 1;
int faceUpdates = 0, setPoseCalls = 0, poseWrites = 0, pinFailures = 0;
int originalBipedCalls = 0;
void* bipedDetour = nullptr;
void OriginalBiped(void*,void*) { ++originalBipedCalls; }
BE_Result HookFn(void*,const char*,void*,void* detour,void** original){bipedDetour=detour;*original=reinterpret_cast<void*>(&OriginalBiped);return BE_Result_Ok;}
std::string failMethod;
float floorBase=0,floorSlope=0;bool floorWalkable=true;int floorQueries=0;
Class* FindClass(const std::string& name, bool value = false) {
  for (auto& c:classes) if(c.name==name) return &c;
  classes.push_back({name,value,{}});return &classes.back();
}
Method* AddMethod(Class* c,const char* name,const char* result,std::vector<std::string> params={}) {
  methodStore.push_back({name,result,std::move(params)});auto* m=&methodStore.back();if(c)c->methods.push_back(m);return m;
}
Object* New(Class* c) {objects.push_back({});auto* o=&objects.back();o->klass=c;payloadOwners[o->payload.data()]=o;return o;}
Object* Text(const std::string& s){auto* o=New(FindClass("String"));o->text=s;return o;}
template<class T> Object* Box(T value){auto* o=New(FindClass("Box",true));memcpy(o->payload.data(),&value,sizeof(T));return o;}
template<class T> T Value(Object* o){T out{};memcpy(&out,o->payload.data(),sizeof(T));return out;}
Object* Obj(void* p){auto it=payloadOwners.find(p);return it!=payloadOwners.end()?it->second:static_cast<Object*>(p);}
void LogFn(void*,const char*,const char* message){std::cerr<<message<<'\n';}
BE_Result Resolve(void*,const BE_MethodDescriptorV1* d,BE_ResolvedMethodV1* out){
  if(failMethod==d->method_name)return BE_Result_NotFound;
  std::vector<std::string> params;std::string text=d->parameter_types?d->parameter_types:"";
  while(!text.empty()){auto i=text.find('|');params.push_back(text.substr(0,i));if(i==std::string::npos)break;text.erase(0,i+1);}
  if(params.size()!=d->parameter_count)return BE_Result_ContractMismatch;
  out->method_info=AddMethod(nullptr,d->method_name,d->return_type,std::move(params));out->method_pointer=d->method_name==std::string("UpdateSolver")?reinterpret_cast<void*>(&OriginalBiped):nullptr;return BE_Result_Ok;
}
BE_Result ResolveFieldFn(void*,const BE_FieldDescriptorV1* d,BE_ResolvedFieldV1* out){fields.emplace_back(d->field_name);out->field_info=&fields.back();out->offset=-1;return BE_Result_Ok;}
void* FieldFn(void*,const void* field,void* instance){const auto& name=*static_cast<const std::string*>(field);if(name=="DefaultEx2")return Box(2);auto* o=Obj(instance);return o&&o->fields.count(name)?o->fields.at(name):nullptr;}
BE_Result ResolveClassFn(void*,const char*,const char*,const char* name,BE_ResolvedClassV1* out){
  auto* c=FindClass(name,(std::string(name)=="MorphCtrlValue" || std::string(name)=="MovementComponent.FindFloorResult"));out->class_info=c;out->type_object=Text(name);return BE_Result_Ok;
}
void* ObjectNew(void*,const void* c){return New(const_cast<Class*>(static_cast<const Class*>(c)));}
void* StringNew(void*,const char* text){return Text(text);}
void* Unbox(void*,void* object){return Obj(object)->payload.data();}
uint32_t PinFn(void*,void* object,int pinned){assert(pinned==1);if(pinFailures){--pinFailures;return 0;}uint32_t h=nextHandle++;handles[h]=Obj(object);return h;}
void FreeFn(void*,uint32_t h){assert(handles.erase(h)==1);}
int CopyString(void*,const void* object,char* out,size_t size){const auto& text=static_cast<const Object*>(object)->text;if(text.size()>=size)return 0;memcpy(out,text.c_str(),text.size()+1);return int(text.size());}
void* InvokeFn(void*,const void* info,void* instance,void** args,void** exception){
  const auto* m=static_cast<const Method*>(info);auto* o=Obj(instance);const auto& n=m->name;
  if(n==failMethod){*exception=Text("fixture failure");return nullptr;}
  if(n=="Clear"&&o->klass->name=="MovementComponent.FindFloorResult"){o->fields["isHit"]=Box(false);o->fields["walkableFloor"]=Box(false);return nullptr;}
  if(n=="ComputeFloorDist"||n=="FindFloor"){
    const auto position=*static_cast<VmdVec3*>(args[0]);auto* floor=Obj(args[n=="ComputeFloorDist"?2:1]);++floorQueries;
    const float height=floorBase+floorSlope*position.x;const float sweep=*static_cast<float*>(args[n=="ComputeFloorDist"?1:2]);
    floor->fields["isHit"]=Box(position.y>=height&&position.y-height<=sweep);floor->fields["walkableFloor"]=Box(floorWalkable);
    floor->fields["floorDist"]=Box(position.y-height);auto* hit=New(FindClass("RaycastHit",true));hit->fields["m_Point"]=Box(VmdVec3{position.x,height,position.z});floor->fields["floorHit"]=hit;floor->fields["normal"]=Box(VmdVec3{-floorSlope,1,0});return nullptr;
  }
  if(n=="IsWalkableFloor")return o->fields.at("walkableFloor");
  if(n=="get_floorNormal")return o->fields.at("normal");
  if(n=="op_Implicit")return Box(args[0]&&Obj(args[0])->alive);
  if(n=="GetBoneTransform"){auto i=*static_cast<int*>(args[0]);return o->human.count(i)?o->human[i]:nullptr;}
  if(n=="get_transform")return o->root;
  if(n=="get_avatar")return o->fields.at("avatar");
  if(n=="get_humanDescription")return o->fields.at("description");
  if(n=="get_lossyScale")return Box(VmdVec3{1,1,1});
  if(n=="get_enabled")return Box(o->enabled);
  if(n=="set_enabled"){o->enabled=*static_cast<bool*>(args[0]);return nullptr;}
  if(n=="get_position"||n=="get_localPosition")return Box(o->position);
  if(n=="get_rotation"||n=="get_localRotation")return Box(o->rotation);
  if(n=="set_rotation"||n=="set_localRotation"){o->rotation=*static_cast<VmdQuaternion*>(args[0]);++poseWrites;return nullptr;}
  if(n=="set_position"||n=="set_localPosition"){o->position=*static_cast<VmdVec3*>(args[0]);++poseWrites;return nullptr;}
  if(n=="get_childCount")return Box(int(o->children.size()));
  if(n=="GetChild")return o->children.at(*static_cast<int*>(args[0]));
  if(n=="get_parent")return o->parent;
  if(n=="get_name")return Text(o->text);
  if(n=="get_gameObject")return o;
  if(n=="GetComponents"){auto* array=New(FindClass("Array"));array->items=o->components;return array;}
  if(n=="get_Length"||n=="get_Count")return Box(int(o->items.size()));
  if(n=="GetValue"||n=="get_Item")return o->items.at(*static_cast<int*>(args[0]));
  if(n=="set_Item"){auto* original=Obj(args[1]);auto* copy=New(original->klass);copy->payload=original->payload;o->items.at(*static_cast<int*>(args[0]))=copy;return nullptr;}
  if(n=="Equals")return Box(o==Obj(args[0]));
  if(n=="get_skMorphCom")return o->fields.at("skMorphCom");
  if(n=="get_IsInited")return Box(o->alive);
  if(n=="CompleteJob")return nullptr;
  if(n=="GetIsOverridedTracker"||n=="GetTrackerAnimationPause")return Box(false);
  if(n=="SetIsOverridedTracker"||n=="SetTrackerAnimationPause")return nullptr;
  if(n=="SetMorphEmotionPause"){o->fields["m_pauseEmotion"]=Box(*static_cast<bool*>(args[0]));return nullptr;}
  if(n=="Update"){++faceUpdates;return nullptr;}
  if(n=="SetPose"){++setPoseCalls;assert(args[0]&&*static_cast<float*>(args[2])==1);o->fields["test_pose"]=Obj(args[0]);return nullptr;}
  if(n==".ctor"){
    if(o->klass->name=="MorphCtrlValue"){struct Data{Object* text;float weight;};Data data{Obj(args[0]),*static_cast<float*>(args[1])};memcpy(o->payload.data(),&data,sizeof(data));}
    if(o->klass->name=="SkeletalMorphPose")for(const char* p:{"browValueL","browValueR","eyeValueL","eyeValueR","mouthValue","otherValue"})o->fields[p]=New(FindClass("List"));
    return nullptr;
  }
  if(n.rfind("get_",0)==0&&o->fields.count(n.substr(4)))return o->fields[n.substr(4)];
  if(n=="Clear"){o->items.clear();return nullptr;}
  if(n=="Add"){auto* original=Obj(args[0]);auto* copy=New(original->klass);copy->payload=original->payload;o->items.push_back(copy);return nullptr;}
  if(n=="get_SerializeData")return o->fields["serialize"];
  if(n=="SetClothSimulateWeight"||n=="SetAnimationPoseRatio"){
    const char* name=n=="SetClothSimulateWeight"?"clothSimulateWeight":"animationPoseRatio";
    auto* box=Box(*static_cast<float*>(args[0]));o->fields["serialize"]->fields[name]=box;o->fields[std::string(name)+"Property"]=box;return nullptr;
  }
  std::cerr<<"Unhandled fake method "<<n<<'\n';assert(false);return nullptr;
}
void SetFieldFn(void* object,void* field,void* value){Obj(object)->fields[*static_cast<std::string*>(field)]=Box(*static_cast<float*>(value));}
void* ObjectClass(void* object){return Obj(object)->klass;}
void* ClassMethods(void* klass,void** iterator){auto* c=static_cast<Class*>(klass);auto i=reinterpret_cast<uintptr_t>(*iterator);if(i==c->methods.size())return nullptr;*iterator=reinterpret_cast<void*>(i+1);return c->methods[i];}
const char* MethodName(void* m){return static_cast<Method*>(m)->name.c_str();}
uint32_t ParamCount(void* m){return uint32_t(static_cast<Method*>(m)->params.size());}
void* ParamType(void* m,uint32_t i){return &static_cast<Method*>(m)->params.at(i);}
void* ReturnType(void* m){return &static_cast<Method*>(m)->result;}
const char* TypeName(void* type){return _strdup(static_cast<std::string*>(type)->c_str());}
bool ValueType(void* c){return static_cast<Class*>(c)->value;}
void FreeMemory(void* p){free(p);}
Object* MakeArray(const std::string& element){
  std::string type="Unity.Collections.NativeArray<"+element+">";auto* c=FindClass(type,true);
  if(c->methods.empty()){AddMethod(c,"get_Length","System.Int32");AddMethod(c,"get_Item",element.c_str(),{"System.Int32"});AddMethod(c,"set_Item","System.Void",{"System.Int32",element});AddMethod(c,"Equals","System.Boolean",{type});}
  auto* o=New(c);auto* itemClass=FindClass(element,true);for(int i=0;i<4;++i){auto* item=New(itemClass);int v=100+i;memcpy(item->payload.data(),&v,4);o->items.push_back(item);}return o;
}
Object* MakeSmc(Object* owner){
  auto* component=New(FindClass("MorphComponent"));auto* core=New(FindClass("SMC"));component->fields["m_core"]=core;owner->fields["skMorphCom"]=component;core->fields["m_pauseEmotion"]=Box(false);
  const char* fields[]={"m_tracks","m_tracksDoubleBuffer","m_transitionTracks","m_transitionTracksDoubleBuffer","m_transitionCurrentTrackAnim","m_transitionCurrent","m_transitionNext"};
  const char* types[]={"FSkeletalMorphTracker","FSkeletalMorphTracker","FSkeletalMorphTracker","FSkeletalMorphTracker","FMorphAnimTransitionRuntime","FMorphCurrentEmotionTransitionRuntime","FMorphEmotionTransitionRuntime"};
  for(int i=0;i<7;++i)core->fields[fields[i]]=MakeArray(std::string("Beyond.Gameplay.Core.")+types[i]);
  auto* avatar=New(FindClass("Avatar"));auto* names=New(FindClass("Array"));for(const char* n:{"A","I","U","E","O","eye_thinkcloseeyes_a_R_ctrl","eye_thinkcloseeyes_a_L_ctrl"})names->items.push_back(Text(n));avatar->fields["morphMappingNames"]=names;core->fields["morphData"]=avatar;
  auto* list=FindClass("List");if(list->methods.empty()){AddMethod(list,"Clear","System.Void");AddMethod(list,"Add","System.Void",{kCtrlType});}
  return core;
}
Object* MakeRig(Object* anim){
  auto* root=New(FindClass("Transform"));root->text="Root";anim->root=root;
  for(int human=0;human<55;++human){auto* b=New(FindClass("Transform"));b->position={float(human%2)*.2f,1.5f+float(human)*.01f,0};b->parent=root;b->text="Human"+std::to_string(human);root->children.push_back(b);anim->human[human]=b;}
  anim->human[0]->position={0,1,0};
  for(int side=0;side<2;++side){int h=1+side;float x=side?.15f:-.15f;anim->human[h]->position={x,.9f,0};anim->human[h+2]->position={x,.5f,.03f};anim->human[h+4]->position={x,.1f,0};anim->human[19+side]->position={x,.1f,.12f};}
  const char* names[4][2]={{"Bip001_LUpArmTwist","Bip001_LUpArmTwist1"},{"Bip001_L_ForeTwist","Bip001_L_ForeTwist1"},{"Bip001_RUpArmTwist","Bip001_RUpArmTwist1"},{"Bip001_R_ForeTwist","Bip001_R_ForeTwist1"}};
  for(auto& channel:names)for(auto* name:channel){auto* b=New(FindClass("Transform"));b->text=name;b->parent=root;root->children.push_back(b);}
  auto* cloth=New(FindClass("Cloth"));auto* data=New(FindClass("Serialize"));data->fields["clothSimulateWeight"]=Box(.3f);data->fields["animationPoseRatio"]=Box(.7f);cloth->fields["serialize"]=data;cloth->fields["clothSimulateWeightProperty"]=Box(.4f);cloth->fields["animationPoseRatioProperty"]=Box(.6f);root->components.push_back(cloth);
  auto* anchor=New(FindClass("Transform"));anchor->text="TailAnchor";anchor->position={0,1.25f,0};anchor->parent=root;root->children.push_back(anchor);root->fields["test_anchor"]=anchor;
  auto* rootsClass=FindClass("TransformList");if(rootsClass->methods.empty()){AddMethod(rootsClass,"get_Count","System.Int32");AddMethod(rootsClass,"get_Item","UnityEngine.Transform",{"System.Int32"});}
  auto* roots=New(rootsClass);roots->items.push_back(anchor);data->fields["rootBones"]=roots;
  auto* avatar=New(FindClass("Avatar"));auto* description=New(FindClass("HumanDescription",true));auto* skeleton=New(FindClass("Array"));
  for(auto* node:root->children){auto* record=New(FindClass("SkeletonBone",true));record->fields["name"]=Text(node->text);record->fields["position"]=Box(node->position);record->fields["rotation"]=Box(node->rotation);record->fields["scale"]=Box(VmdVec3{1,1,1});skeleton->items.push_back(record);}
  description->fields["skeleton"]=skeleton;avatar->fields["description"]=description;anim->fields["avatar"]=avatar;anchor->position.y=2.5f;
  return root;
}
BE_HostApiV1 Host(){BE_HostApiV1 h{};h.abi_version=1;h.log=LogFn;h.create_hook=HookFn;h.resolve_method=Resolve;h.resolve_field=ResolveFieldFn;h.copy_managed_string=CopyString;h.resolve_class=ResolveClassFn;h.object_new=ObjectNew;h.string_new=StringNew;h.runtime_invoke=InvokeFn;h.object_unbox=Unbox;h.gchandle_new=PinFn;h.gchandle_free=FreeFn;h.field_get_value_object=FieldFn;return h;}
}
void* BeEiemOfflineMetadataExport(const char* name){
#define EXPORT(symbol,func) if(!strcmp(name,symbol))return reinterpret_cast<void*>(&fixture::func)
  EXPORT("il2cpp_object_get_class",ObjectClass);EXPORT("il2cpp_class_get_methods",ClassMethods);EXPORT("il2cpp_method_get_name",MethodName);EXPORT("il2cpp_method_get_param_count",ParamCount);EXPORT("il2cpp_method_get_param",ParamType);EXPORT("il2cpp_method_get_return_type",ReturnType);EXPORT("il2cpp_type_get_name",TypeName);EXPORT("il2cpp_class_is_valuetype",ValueType);EXPORT("il2cpp_free",FreeMemory);EXPORT("il2cpp_field_set_value",SetFieldFn);
#undef EXPORT
  return nullptr;
}

#if !defined(BE_EIEM_FIXTURE_LIBRARY)
int main(){
  using namespace fixture;
  auto h=Host();host=&h;mainThread=std::this_thread::get_id();assert(metadata.Load());
  for(auto& m:methods)assert(Resolve(nullptr,&m.descriptor,&m.resolved)==BE_Result_Ok);
  auto* owner=New(FindClass("Entity"));auto* anim=New(FindClass("Animator"));auto* root=MakeRig(anim);auto* core=MakeSmc(owner);
  entity=Pin(owner);animator=Pin(anim);options.face=true;options.cloth=BetterEndfieldNext::EiemBody::ClothMode::Stable;
  anim->human[7]->rotation=DirectVmdQuaternionFromAxisAngle({1,0,0},.7f);assert(Capture());assert(std::fabs(bones[size_t(Id::UpperBody)].natural.rotation.w-1)<1e-6f);assert(face.ready);assert(cloth.size()==1);assert(twists[0][0].transform&&twists[0][1].transform);
  DirectVmdSampleFrame frame{};frame.valid=1;frame.leftFootIkEnabled=1;frame.rightFootIkEnabled=1;
  for(auto& b:frame.bones)b.rotation={0,0,0,1};
  frame.bones[size_t(Id::LeftArmTwist)].rotation=DirectVmdQuaternionFromAxisAngle({.7941f,-.6076f,.012f},.6f);
  ApplyTwists(frame);assert(std::fabs(twists[0][0].transform.object?Obj(twists[0][0].transform.object)->rotation.w:1)<.999f);
  ApplyCloth();assert(root->fields["test_anchor"]->position.y==1.25f);auto* c=root->components[0];assert(Value<float>(c->fields["serialize"]->fields["clothSimulateWeight"])==1);assert(ClothRestore());assert(root->fields["test_anchor"]->position.y==2.5f);assert(Value<float>(c->fields["serialize"]->fields["clothSimulateWeight"])==.3f);assert(Value<float>(c->fields["clothSimulateWeightProperty"])==.4f);
  options.cloth=BetterEndfieldNext::EiemBody::ClothMode::Freeze;auto nodes=Tree();CaptureCloth(nodes);ApplyCloth();assert(!c->enabled);assert(ClothRestore());assert(c->enabled);
  frame.morphCount=2;strcpy(frame.morphs[0].name,u8"あ");frame.morphs[0].weight=.8f;strcpy(frame.morphs[1].name,u8"まばたき");frame.morphs[1].weight=.5f;
  ApplyFace(frame);assert(setPoseCalls==1&&faceUpdates==1);assert(Value<bool>(core->fields["m_pauseEmotion"]));auto* pose=core->fields["test_pose"];assert(pose->fields["mouthValue"]->items.size()==5);assert(pose->fields["eyeValueL"]->items.size()==1);assert(pose->fields["eyeValueR"]->items.size()==1);
  auto* snapshotArray=Obj(face.snapshots[0].array.object);snapshotArray->items[2]=Box(999);assert(FaceRestore());assert(Value<int>(snapshotArray->items[2])==102);assert(!Value<bool>(core->fields["m_pauseEmotion"]));
  // Independent leg solver conserves limb lengths and reaches a finite target.
  size_t thigh=size_t(Id::LeftLeg);poses[thigh]={{0,1,0},{0,0,0,1}};poses[thigh+1]={{0,.5f,.02f},{0,0,0,1}};poses[thigh+2]={{0,0,0},{0,0,0,1}};poses[size_t(Id::LeftFootIk)]={{.2f,.2f,.1f},{0,0,0,1}};
  frame.bones[size_t(Id::LeftFootIk)].hasTrack=1;frame.bones[size_t(Id::LeftToeIk)].hasTrack=1;poses[size_t(Id::LeftToeIk)]={{.2f,.2f,.25f},{0,0,0,1}};float before=DirectVmdLength(DirectVmdSub(poses[thigh+1].position,poses[thigh].position));SolveLeg(0,frame);assert(std::fabs(DirectVmdLength(DirectVmdSub(poses[thigh+1].position,poses[thigh].position))-before)<1e-5f);assert(DirectVmdLength(DirectVmdSub(poses[thigh+2].position,poses[size_t(Id::LeftFootIk)].position))<1e-5f);
  // Terrain owns a real fixture box and passes its unboxed payload as out-ref.
  movement=Pin(New(FindClass("MovementComponent")));options.terrain=true;
  assert(PrepareTerrain());assert(TerrainAvailable());
  DirectVmdTerrainProbeHit hit{};assert(FloorSample({0,.7f,0},1.5f,hit));assert(hit.hit&&hit.point.y==0&&hit.normal.y==1);
  floorWalkable=false;assert(!FloorSample({0,.7f,0},1.5f,hit));assert(terrain.available);floorWalkable=true;
  auto flatPoses=poses;flatPoses[size_t(Id::LeftFootIk)].position={.2f,.1f,0};flatPoses[size_t(Id::RightFootIk)].position={-.2f,.1f,0};
  frame.bones[size_t(Id::RightFootIk)].hasTrack=1;frame.playback=DirectVmdPlaybackState::Playing;frame.sourceFrame=0;frame.sequence=1;
  ResetTerrain();poses=flatPoses;DirectVmdWorldPosePod pelvis{{0,1,0},{0,0,0,1}};ApplyTerrain(frame,pelvis);
  floorBase=.3f;
  for(int tick=0;tick<90;++tick){poses=flatPoses;pelvis={{0,1,0},{0,0,0,1}};frame.sourceFrame+=.5;frame.sequence++;ApplyTerrain(frame,pelvis);}
  assert(terrain.rootOffset>.15f&&pelvis.position.y>1.15f&&poses[size_t(Id::LeftFootIk)].position.y>.2f);
  const float held=terrain.rootOffset;frame.playback=DirectVmdPlaybackState::Paused;floorBase=.6f;poses=flatPoses;pelvis={{0,1,0},{0,0,0,1}};ApplyTerrain(frame,pelvis);assert(std::fabs(terrain.rootOffset-held)<1e-6f);
  failMethod="ComputeFloorDist";assert(!FloorSample({0,1,0},1.5f,hit));assert(!TerrainAvailable());assert(std::string(TerrainReason()).find("exception")!=std::string::npos);failMethod.clear();
  terrain.result.Reset();movement.Reset();options.terrain=false;
  // Managed exception fails the feature; it does not become an API success.
  face.ready=true;failMethod="SetPose";ApplyFace(frame);assert(!face.ready);failMethod.clear();
  Stop("offline-stop");entity.Reset();animator.Reset();ownerRoot.Reset();nodes.clear();assert(handles.empty());
  auto cp=BetterEndfieldNext::EiemAndroid::DecodeCp932("\x83\x5a\x83\x93\x83\x5e\x81\x5b",8);assert(cp==u8"センター");assert(BetterEndfieldNext::EiemAndroid::DecodeCp932("\x81",1).empty());
  const uint8_t utf16[]={0x3d,0xd8,0x00,0xde};assert(BetterEndfieldNext::EiemAndroid::DecodeUtf16LE(utf16,4)=="\xf0\x9f\x98\x80");
  std::cout<<"PASS: Avatar natural bind (animated entry rejected as bind), cloth root anchors, managed SMC pose+snapshot restoration, twist, independent leg/toe IK, boxed terrain query+walkability+height response+pause+exception capability status, stable/freeze cloth restoration, exception gating, GC pins, CP932/UTF16\n";
}

#endif
