#pragma once
// Independent SMC caches for the editor and up to four squad actors. The pose
// mutex protects registration and callbacks; thread-local scope selection is
// constant-time and never copies the large expression arrays per frame.
#define SMC_MAX_SLIDERS (SMC_NUM_MOUTH + 32)
#define SMC_MAX_REJECTED 64
#define SMC_SNAPSHOT_MAX 4096
struct SMCMotionFrame {
  bool active=false; void* animator=nullptr; float weights[(SMC_NUM_MOUTH + 32)]={};
  bool gazeCamera=false;float gazeStrength=1;
  void* eyes[2]={}; Quat eyeRotation[2]; bool eyeDriven[2]={};
  uint64_t generation=0;
  face_mixing::Settings settings;
  std::shared_ptr<const character_face::Profile> profile;
  std::array<float,character_face::MaxMorphs> expressions{};
  float fallbackWeights[(SMC_NUM_MOUTH + 32)]={};
};
struct SMCActorState {
  poser_gaze::Context gaze;
  void *actor = nullptr, *root = nullptr;
  bool frozen = true;
  int revision = 0;
  std::vector<AllBone> bones;
  SMCAutomationPause automation;
  uint32_t retainedCore = 0;
  bool morphWeightProbed = false;
  bool fieldsDumped = false;
  bool allMorphsIsFloatArray = false;
  int allMorphsWeightOff = -1;
  char allMorphsClass[64] = "";
  void *smcCore = nullptr;
  void *confirmedSMC = nullptr;
  int frame = 0;
  volatile bool driving = false;
  bool smcOwnershipVerified = false;
  SMCMorphBoneEntry capturedExpression[SMC_MAX_BIGLIST]{};
  int capturedLen = 0;
  bool bigListCaptured = false;
  SMCFaceBone faceBones[SMC_MAX_FACE_BONES]{};
  SMCFaceBone faceRestPose[SMC_MAX_FACE_BONES]{};
  int faceBoneCount = 0;
  bool faceBonesCaptured = false;
  bool faceBoneTouched[SMC_MAX_FACE_BONES] = {};
  bool faceBoneEvalOk = false;
  std::vector<face_geometry::Bone> faceNodes{};
  std::shared_ptr<const character_face::Profile> characterProfile{};
  character_face::Binding characterBinding{};
  uint64_t characterBindingGeneration=0;
  std::string characterModel{};
  face_mixing::Hierarchy faceHierarchy{};
  std::array<int,SMC_MAX_FACE_BONES> faceRegions=[] {
  std::array<int,SMC_MAX_FACE_BONES> regions;regions.fill(-1);return regions;
}();
  uint64_t faceGeneration=1;
  int faceBindingRevision=-1;
  bool mmdFaceMode=false;
  mmd_face_controls::State manualFace{};
  void **faceBoneRefs = nullptr;
  bool captureNeutral = false;
  int neutralFrames = 0;
  SMCFaceBone driveBase[SMC_MAX_FACE_BONES]{};
  bool driveBaseReady = false;
  float fitW[SMC_NUM_MOUTH + 32] = {};
  SMCFaceBone lastFacePoseBuf[SMC_MAX_FACE_BONES]{};
  bool lastFacePoseValid = false;
  bool wasFrozen = false;
  bool holdApplied = false;
  int boneIDToIdx[SMC_BONE_MAP_SIZE]{};
  int boneIDMapCount = 0;
  bool boneMapReady = false;
  SMCMouthShape mouthShapes[SMC_NUM_MOUTH] = {
    {"A", 299073642, -1, -1, -1, -1, -1, false},
    {"I", 1271943943, -1, -1, -1, -1, -1, false},
    {"U", 1701661734, -1, -1, -1, -1, -1, false},
    {"E", -781522180, -1, -1, -1, -1, -1, false},
    {"O", -348812070, -1, -1, -1, -1, -1, false},
};
  float mouthWeights[SMC_NUM_MOUTH] = {};
  bool mouthResolved = false;
  SMCExtraMorph extraMorphs[19] = {
    {"\xe3\x81\xbe\xe3\x81\xb0\xe3\x81\x9f\xe3\x81\x8d", "blink",
     {{"eye_thinkcloseeyes_a_R_ctrl", 0, -1, -1, -1, -1, false},
      {"eye_thinkcloseeyes_a_L_ctrl", 0, -1, -1, -1, -1, false}},
     2, 0, 0},
    {"\xe7\xac\x91\xe3\x81\x84", "smile_eye",
     {{"eye_relax_a_R_ctrl", 0, -1, -1, -1, -1, false},
      {"eye_relax_a_L_ctrl", 0, -1, -1, -1, -1, false}},
     2, 0, 0},
    {"\xe3\x82\xa6\xe3\x82\xa3\xe3\x83\xb3\xe3\x82\xaf", "wink_L",
     {{"eye_thinkcloseeyes_a_L_ctrl", 0, -1, -1, -1, -1, false}},
     1, 0, 0},
    {"\xe3\x82\xa6\xe3\x82\xa3\xe3\x83\xb3\xe3\x82\xaf\xe5\x8f\xb3", "wink_R",
     {{"eye_thinkcloseeyes_a_R_ctrl", 0, -1, -1, -1, -1, false}},
     1, 0, 0},
    {"\xe3\x81\xaa\xe3\x81\x94\xe3\x81\xbf", "nagomi",
     {{"eye_relax_a_R_ctrl", 0, -1, -1, -1, -1, false},
      {"eye_relax_a_L_ctrl", 0, -1, -1, -1, -1, false}},
     2, 0, 0},
    {"\xe3\x81\xb3\xe3\x81\xa3\xe3\x81\x8f\xe3\x82\x8a", "surprise_eye",
     {{"eye_relax_a_R_ctrl", 0, -1, -1, -1, -1, false},
      {"eye_relax_a_L_ctrl", 0, -1, -1, -1, -1, false}},
     2, 0, 0},
    {"\xe4\xb8\x8a", "brow_up",
     {{"brow_offset_u_R_ctrl", 0, -1, -1, -1, -1, false},
      {"brow_offset_u_L_ctrl", 0, -1, -1, -1, -1, false}},
     2, 0, 0},
    {"\xe4\xb8\x8b", "brow_down",
     {{"brow_offset_d_R_ctrl", 0, -1, -1, -1, -1, false},
      {"brow_offset_d_L_ctrl", 0, -1, -1, -1, -1, false}},
     2, 0, 0},
    {"\xe6\x80\x92\xe3\x82\x8a", "brow_angry",
     {{"brow_attack_a_R_ctrl", 0, -1, -1, -1, -1, false},
      {"brow_attack_a_L_ctrl", 0, -1, -1, -1, -1, false}},
     2, 0, 0},
    {"\xe5\x9b\xb0\xe3\x82\x8b", "brow_sad",
     {{"brow_relax_a_R_ctrl", 0, -1, -1, -1, -1, false},
      {"brow_relax_a_L_ctrl", 0, -1, -1, -1, -1, false}},
     2, 0, 0},
    {"\xe3\x81\xab\xe3\x81\x93\xe3\x82\x8a", "brow_smile",
     {{"brow_relax_a_R_ctrl", 0, -1, -1, -1, -1, false},
      {"brow_relax_a_L_ctrl", 0, -1, -1, -1, -1, false}},
     2, 0, 0},
    {"\xe3\x82\xa6\xe3\x82\xa3\xe3\x83\xb3\xe3\x82\xaf\xef\xbc\x92", "wink2_L",
     {{"eye_thinkcloseeyes_a_L_ctrl", 0, -1, -1, -1, -1, false}},
     1, 0, 0},
    {"\xe3\x82\xa6\xe3\x82\xa3\xe3\x83\xb3\xe3\x82\xaf\xef\xbc\x92\xe5\x8f\xb3",
     "wink2_R",
     {{"eye_thinkcloseeyes_a_R_ctrl", 0, -1, -1, -1, -1, false}},
     1, 0, 0},
    {"\xef\xbd\xb3\xef\xbd\xa8\xef\xbe\x9d\xef\xbd\xb8\xef\xbc\x92\xe5\x8f\xb3",
     "wink2_R_half",
     {{"eye_thinkcloseeyes_a_R_ctrl", 0, -1, -1, -1, -1, false}},
     1, 0, 0},
    {"\xe6\x82\xb2\xe3\x81\x97\xe3\x81\x84", "sad_eye",
     {{"eye_relax_a_R_ctrl", 0, -1, -1, -1, -1, false},
      {"eye_relax_a_L_ctrl", 0, -1, -1, -1, -1, false}},
     2, 0, 0},
    {"\xe7\x9c\x9f\xe9\x9d\xa2\xe7\x9b\xae", "serious",
     {{"brow_attack_a_R_ctrl", 0, -1, -1, -1, -1, false},
      {"brow_attack_a_L_ctrl", 0, -1, -1, -1, -1, false}},
     2, 0, 0},
    {"\xe5\x89\x8d", "forward",
     {{"brow_offset_d_R_ctrl", 0, -1, -1, -1, -1, false},
      {"brow_offset_d_L_ctrl", 0, -1, -1, -1, -1, false}},
     2, 0, 0},
    {"\xe3\x81\x98\xe3\x83\xbc\xe3\x81\xa3", "stare",
     {{"eye_attack_a_R_ctrl", 0, -1, -1, -1, -1, false},
      {"eye_attack_a_L_ctrl", 0, -1, -1, -1, -1, false}},
     2, 0, 0},
    {"\xe3\x81\xaf\xe3\x81\x85", "hau",
     {{"eye_thinkcloseeyes_a_R_ctrl", 0, -1, -1, -1, -1, false},
      {"eye_thinkcloseeyes_a_L_ctrl", 0, -1, -1, -1, -1, false}},
     2, 0, 0},
};
  bool extraMorphsResolved = false;
  void *smcRejected[SMC_MAX_REJECTED] = {};
  int smcRejectedCount = 0;
  int smcRejectStrikes = 0;
  void *smcCheckedInstance = nullptr;
  void *charBoneXforms[SMC_SNAPSHOT_MAX] = {};
  int charBoneXformCount = 0;
  int charBoneXformRev = -1;
  SRWLOCK motionFaceLock = SRWLOCK_INIT;
  SMCMotionFrame motionFaceMailbox{};
  SMCMotionFrame motionFaceCurrent{};
  bool motionFaceSaved=false;
  bool motionSavedDriving=false;
  bool motionSavedBaseReady=false;
  void* motionSavedCore=nullptr;
  uint64_t motionSavedGeneration=0;
  float motionSavedWeights[SMC_MAX_SLIDERS]={};
  SMCFaceBone motionSavedBase[SMC_MAX_FACE_BONES]{};
  char smcReadStatus[128] = "";
};
static SMCActorState &s_editorSMC = *new SMCActorState{};
static thread_local SMCActorState *s_activeSMC = &s_editorSMC;
static std::array<SMCActorState *,4> s_squadSMC{};
static bool s_editorSquadFrozen=false;
struct SMCActorScope {
  SMCActorState *previous;
  explicit SMCActorScope(SMCActorState *next):previous(s_activeSMC) {
    s_activeSMC = next ? next : &s_editorSMC;
  }
  ~SMCActorScope() { s_activeSMC=previous; }
};
static void *SMCAnimator() {return s_activeSMC==&s_editorSMC?g_charAnimator:s_activeSMC->actor;}
static void *SMCRoot() {return s_activeSMC==&s_editorSMC?GetCharRootTransform():s_activeSMC->root;}
static bool SMCFrozen() {return s_activeSMC==&s_editorSMC?(g_frozen||s_editorSquadFrozen):s_activeSMC->frozen;}
static poser_gaze::Context &SMCGazeContext() {return s_activeSMC==&s_editorSMC?poser_gaze::editor:s_activeSMC->gaze;}
static int SMCRevision() {return s_activeSMC==&s_editorSMC?s_bonesRev:s_activeSMC->revision;}
static const std::vector<AllBone> &SMCBones() {return s_activeSMC==&s_editorSMC?s_allBones:s_activeSMC->bones;}
static SMCAutomationPause &SMCAutomation() {return s_activeSMC==&s_editorSMC?s_smcAutomation:s_activeSMC->automation;}
static SMCActorState *SMCFindActor(void *core);
#define s_morphWeightProbed (s_activeSMC->morphWeightProbed)
#define s_fieldsDumped (s_activeSMC->fieldsDumped)
#define s_allMorphsIsFloatArray (s_activeSMC->allMorphsIsFloatArray)
#define s_allMorphsWeightOff (s_activeSMC->allMorphsWeightOff)
#define s_allMorphsClass (s_activeSMC->allMorphsClass)
#define s_smcCore (s_activeSMC->smcCore)
#define s_confirmedSMC (s_activeSMC->confirmedSMC)
#define s_frame (s_activeSMC->frame)
#define s_driving (s_activeSMC->driving)
#define s_smcOwnershipVerified (s_activeSMC->smcOwnershipVerified)
#define s_capturedExpression (s_activeSMC->capturedExpression)
#define s_capturedLen (s_activeSMC->capturedLen)
#define s_bigListCaptured (s_activeSMC->bigListCaptured)
#define s_faceBones (s_activeSMC->faceBones)
#define s_faceRestPose (s_activeSMC->faceRestPose)
#define s_faceBoneCount (s_activeSMC->faceBoneCount)
#define s_faceBonesCaptured (s_activeSMC->faceBonesCaptured)
#define s_faceBoneTouched (s_activeSMC->faceBoneTouched)
#define s_faceBoneEvalOk (s_activeSMC->faceBoneEvalOk)
#define s_faceNodes (s_activeSMC->faceNodes)
#define s_characterProfile (s_activeSMC->characterProfile)
#define s_characterBinding (s_activeSMC->characterBinding)
#define s_characterBindingGeneration (s_activeSMC->characterBindingGeneration)
#define s_characterModel (s_activeSMC->characterModel)
#define s_faceHierarchy (s_activeSMC->faceHierarchy)
#define s_faceRegions (s_activeSMC->faceRegions)
#define s_faceGeneration (s_activeSMC->faceGeneration)
#define s_faceBindingRevision (s_activeSMC->faceBindingRevision)
#define s_mmdFaceMode (s_activeSMC->mmdFaceMode)
#define s_manualFace (s_activeSMC->manualFace)
#define s_faceBoneRefs (s_activeSMC->faceBoneRefs)
#define s_captureNeutral (s_activeSMC->captureNeutral)
#define s_neutralFrames (s_activeSMC->neutralFrames)
#define s_driveBase (s_activeSMC->driveBase)
#define s_driveBaseReady (s_activeSMC->driveBaseReady)
#define s_fitW (s_activeSMC->fitW)
#define s_lastFacePoseBuf (s_activeSMC->lastFacePoseBuf)
#define s_lastFacePoseValid (s_activeSMC->lastFacePoseValid)
#define s_wasFrozen (s_activeSMC->wasFrozen)
#define s_holdApplied (s_activeSMC->holdApplied)
#define s_boneIDToIdx (s_activeSMC->boneIDToIdx)
#define s_boneIDMapCount (s_activeSMC->boneIDMapCount)
#define s_boneMapReady (s_activeSMC->boneMapReady)
#define s_mouthShapes (s_activeSMC->mouthShapes)
#define s_mouthWeights (s_activeSMC->mouthWeights)
#define s_mouthResolved (s_activeSMC->mouthResolved)
#define s_extraMorphs (s_activeSMC->extraMorphs)
#define s_extraMorphsResolved (s_activeSMC->extraMorphsResolved)
#define s_smcRejected (s_activeSMC->smcRejected)
#define s_smcRejectedCount (s_activeSMC->smcRejectedCount)
#define s_smcRejectStrikes (s_activeSMC->smcRejectStrikes)
#define s_smcCheckedInstance (s_activeSMC->smcCheckedInstance)
#define s_charBoneXforms (s_activeSMC->charBoneXforms)
#define s_charBoneXformCount (s_activeSMC->charBoneXformCount)
#define s_charBoneXformRev (s_activeSMC->charBoneXformRev)
#define s_motionFaceLock (s_activeSMC->motionFaceLock)
#define s_motionFaceMailbox (s_activeSMC->motionFaceMailbox)
#define s_motionFaceCurrent (s_activeSMC->motionFaceCurrent)
#define s_motionFaceSaved (s_activeSMC->motionFaceSaved)
#define s_motionSavedDriving (s_activeSMC->motionSavedDriving)
#define s_motionSavedBaseReady (s_activeSMC->motionSavedBaseReady)
#define s_motionSavedCore (s_activeSMC->motionSavedCore)
#define s_motionSavedGeneration (s_activeSMC->motionSavedGeneration)
#define s_motionSavedWeights (s_activeSMC->motionSavedWeights)
#define s_motionSavedBase (s_activeSMC->motionSavedBase)
#define s_smcReadStatus (s_activeSMC->smcReadStatus)
