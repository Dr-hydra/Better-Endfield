#pragma once
static bool ClothRibbonMotionRow(ClothInstance &i,void *simulation,
    const eiem_cloth::Owner &owner,int frame,std::ostringstream &out) {
  void *bbc=ClothTarget(i.ref),*process=nullptr,*data=nullptr,*roots=nullptr,*colliders=nullptr,*box=nullptr;
  int team=0;
  if(!bbc||!ClothOwns(owner)||!ClothField(bbc,"process","BeyondDynamicBone.ClothProcess",process)||!process||
      !ClothValue(SurfaceMethod(il2cpp_object_get_class(process),"get_TeamId","System.Int32"),process,team)||
      !ClothBoneContactTeam(team,process,box)||
      !ClothInvoke(SurfaceMethod(il2cpp_object_get_class(bbc),"get_SerializeData","BeyondDynamicBone.ClothSerializeData"),bbc,nullptr,data)||
      !data||!ClothField(data,"rootBones","System.Collections.Generic.List<UnityEngine.Transform>",roots)||
      !ClothField(process,"colliderList",CollisionListType,colliders))return false;
  const int rc=CollisionCount(roots),cc=CollisionCount(colliders);int relative=-1;ClothInputChunk pc{};
  ClothInputArray ids{},next{},display{};
  if(rc<1||rc>8||cc<0||cc>16||!ClothInputTeamField(box,"useRelativeTransform","System.Int32",relative)||relative!=0||
      !ClothInputArrayOpen(simulation,"teamIdArray","System.Int16",ids)||
      !ClothInputArrayOpen(simulation,"nextPosArray","Unity.Mathematics.double3",next)||
      !ClothInputArrayOpen(simulation,"dispPosArray","Unity.Mathematics.double3",display)||
      !ClothInputChunkRead(box,"particleChunk",ids.length,pc,64)||pc.count<1||
      pc.start>next.length||pc.count>next.length-pc.start||pc.start>display.length||pc.count>display.length-pc.start)return false;
  out<<"{\"component\":"<<CollisionJsonString(i.name)<<",\"instance\":"<<i.ref.id.instance
     <<",\"Process\":"<<uintptr_t(process)<<",\"data\":"<<uintptr_t(data)<<",\"team\":"<<team<<",\"roots\":[";
  for(int n=0;n<rc;++n) {
    auto root=CollisionItem(roots,n,"UnityEngine.Transform");auto parent=CollisionParent(root);char name[128]{},pn[128]{};
    Vector3 p{},lp{};Quaternion q{};Vector3 scale{};
    if(!root||!parent||!ClothAnchorUnderOwner(root)||!CollisionPosition(root,p)||!SurfaceVisiblePose(root,lp,q,scale))return false;
    CollisionName(root,name,sizeof(name));CollisionName(parent,pn,sizeof(pn));
    if(n)out<<',';out<<"{\"name\":"<<CollisionJsonString(name)<<",\"parent\":"<<CollisionJsonString(pn)
        <<",\"position\":["<<p.x<<','<<p.y<<','<<p.z<<"],\"local\":["<<lp.x<<','<<lp.y<<','<<lp.z<<"]}";
  }
  out<<"],\"colliders\":[";
  for(int n=0;n<cc;++n) {
    auto c=CollisionItem(colliders,n,"BeyondDynamicBone.ColliderComponent");void *t=nullptr;char name[128]{},parent[128]{};Vector3 p{};
    if(n)out<<',';if(!c){out<<"null";continue;}
    if(!ClothInvoke(s_clothUnity.getTransform,c,nullptr,t)||!t||!ClothAnchorUnderOwner(t)||!CollisionPosition(t,p))return false;
    CollisionName(c,name,sizeof(name));CollisionName(CollisionParent(t),parent,sizeof(parent));
    out<<"{\"name\":"<<CollisionJsonString(name)<<",\"parent\":"<<CollisionJsonString(parent)
       <<",\"position\":["<<p.x<<','<<p.y<<','<<p.z<<"]}";
  }
  out<<"],\"points\":[";
  for(int n=0;n<pc.count;++n) {
    int16_t id=0;double a[3]{},b[3]{};const int index=pc.start+n;
    if(!ClothInputArrayValue(ids,index,"System.Int16",&id,2)||id!=team||
        !ClothInputArrayValue(next,index,"Unity.Mathematics.double3",a,24)||
        !ClothInputArrayValue(display,index,"Unity.Mathematics.double3",b,24))return false;
    for(double v:{a[0],a[1],a[2],b[0],b[1],b[2]})if(!std::isfinite(v))return false;
    if(n)out<<',';out<<'['<<index<<','<<a[0]<<','<<a[1]<<','<<a[2]<<','<<b[0]<<','<<b[1]<<','<<b[2]<<']';
  }
  out<<"]}";void *after=nullptr;
  return frame==ClothFrame()&&ClothOwns(owner)&&ClothField(bbc,"process","BeyondDynamicBone.ClothProcess",after)&&after==process;
}
static void ClothRibbonMotionTraceImpl() {
  bool eligible=false;for(const auto &s:s_clothBoneSlots)eligible|=ClothBoneDisplayEligible(s);
  if(!eligible)return;
  static uint64_t next=0,session=0,generation=0;static unsigned pause=0;
  const auto owner=s_cloth.owner;const auto now=GetTickCount64();const auto request=s_clothBonePauseEvidence.load(std::memory_order_acquire);
  if(session==owner.session&&generation==owner.generation&&request==pause&&now<next)return;
  session=owner.session;generation=owner.generation;pause=request;next=now+500;
  LARGE_INTEGER begin{},end{},frequency{};QueryPerformanceCounter(&begin);QueryPerformanceFrequency(&frequency);
  void *simulation=nullptr;
  if(!ClothContactManager("get_Simulation","BeyondDynamicBone.SimulationManager",simulation))return;
  auto animator=ClothTarget(s_cloth.animator);
  auto bodyType=SurfaceClass("UnityEngine","HumanBodyBones");
  auto get=animator?ClothMethod(il2cpp_object_get_class(animator),"GetBoneTransform","UnityEngine.Transform","UnityEngine.HumanBodyBones"):nullptr;
  const int frame=ClothFrame();std::ostringstream out;out.precision(12);
  out<<"{\"session\":"<<session<<",\"generation\":"<<generation<<",\"frame\":"<<frame<<",\"pause\":"<<pause
      <<",\"playhead\":"<<CollisionNumber(s_clothBonePoseTicket.playhead)<<",\"hands\":[";
  for(int side=0;side<2;++side) {
    int id=0;void *hand=nullptr,*args[]{&id};Vector3 p{};
    if(!get||!CollisionEnumValue(bodyType,side?"RightHand":"LeftHand",id)||
        !ClothInvoke(get,animator,args,hand)||!hand||!ClothAnchorUnderOwner(hand)||!CollisionPosition(hand,p))return;
    if(side)out<<',';out<<'['<<p.x<<','<<p.y<<','<<p.z<<']';
  }
  out<<"],\"garments\":[";int rows=0;
  for(int n=0;n<s_cloth.count;++n) {
    auto &i=s_cloth.instances[n];
    if(strcmp(i.name,"MC_Seraph_Skirt_Ribbon")&&strcmp(i.name,"MC_Seraph_Ribbon")&&strcmp(i.name,"MC_Seraph_Arm_Ribbon"))continue;
    std::ostringstream row;row.precision(12);
    if(!ClothRibbonMotionRow(i,simulation,owner,frame,row))continue;
    if(rows++)out<<',';out<<row.str();
  }
  if(!rows||frame!=ClothFrame()||!ClothOwns(owner))return;
  QueryPerformanceCounter(&end);
  const double ms=frequency.QuadPart?double(end.QuadPart-begin.QuadPart)*1000/frequency.QuadPart:0;
  if(ms>3)next=now+2000;
  out<<"],\"readMs\":"<<ms<<",\"readOnly\":true,\"contactAttachmentUnconfirmed\":true}";
  Log("[CLOTH-RIBBON-MOTION] %s",out.str().c_str());
}
static void ClothRibbonMotionTrace() {
  __try {ClothRibbonMotionTraceImpl();}
  __except(EXCEPTION_EXECUTE_HANDLER) {}
}
