// Controller aliases from upstream/smc_face.h (EIEM; see LICENSE.EIEM).
struct AndroidMorphRule { const char* source; const char* targets[4]; };
static constexpr AndroidMorphRule kAndroidMorphMap[] = {
  {"\xe3\x81\xbe\xe3\x81\xb0\xe3\x81\x9f\xe3\x81\x8d", {"eye_thinkcloseeyes_a_R_ctrl", "eye_thinkcloseeyes_a_L_ctrl"}},
  {"\xe7\xac\x91\xe3\x81\x84", {"eye_relax_a_R_ctrl", "eye_relax_a_L_ctrl"}},
  {"\xe3\x82\xa6\xe3\x82\xa3\xe3\x83\xb3\xe3\x82\xaf", {"eye_thinkcloseeyes_a_L_ctrl"}},
  {"\xe3\x82\xa6\xe3\x82\xa3\xe3\x83\xb3\xe3\x82\xaf\xe5\x8f\xb3", {"eye_thinkcloseeyes_a_R_ctrl"}},
  {"\xe3\x81\xaa\xe3\x81\x94\xe3\x81\xbf", {"eye_relax_a_R_ctrl", "eye_relax_a_L_ctrl"}},
  {"\xe3\x81\xb3\xe3\x81\xa3\xe3\x81\x8f\xe3\x82\x8a", {"eye_relax_a_R_ctrl", "eye_relax_a_L_ctrl"}},
  {"\xe4\xb8\x8a", {"brow_offset_u_R_ctrl", "brow_offset_u_L_ctrl"}},
  {"\xe4\xb8\x8b", {"brow_offset_d_R_ctrl", "brow_offset_d_L_ctrl"}},
  {"\xe6\x80\x92\xe3\x82\x8a", {"brow_attack_a_R_ctrl", "brow_attack_a_L_ctrl"}},
  {"\xe5\x9b\xb0\xe3\x82\x8b", {"brow_relax_a_R_ctrl", "brow_relax_a_L_ctrl"}},
  {"\xe3\x81\xab\xe3\x81\x93\xe3\x82\x8a", {"brow_relax_a_R_ctrl", "brow_relax_a_L_ctrl"}},
  {"\xe3\x82\xa6\xe3\x82\xa3\xe3\x83\xb3\xe3\x82\xaf\xef\xbc\x92", {"eye_thinkcloseeyes_a_L_ctrl"}},
  {"\xe3\x82\xa6\xe3\x82\xa3\xe3\x83\xb3\xe3\x82\xaf\xef\xbc\x92\xe5\x8f\xb3", {"eye_thinkcloseeyes_a_R_ctrl"}},
  {"\xef\xbd\xb3\xef\xbd\xa8\xef\xbe\x9d\xef\xbd\xb8\xef\xbc\x92\xe5\x8f\xb3", {"eye_thinkcloseeyes_a_R_ctrl"}},
  {"\xe6\x82\xb2\xe3\x81\x97\xe3\x81\x84", {"eye_relax_a_R_ctrl", "eye_relax_a_L_ctrl"}},
  {"\xe7\x9c\x9f\xe9\x9d\xa2\xe7\x9b\xae", {"brow_attack_a_R_ctrl", "brow_attack_a_L_ctrl"}},
  {"\xe5\x89\x8d", {"brow_offset_d_R_ctrl", "brow_offset_d_L_ctrl"}},
  {"\xe3\x81\x98\xe3\x83\xbc\xe3\x81\xa3", {"eye_attack_a_R_ctrl", "eye_attack_a_L_ctrl"}},
  {"\xe3\x81\xaf\xe3\x81\x85", {"eye_thinkcloseeyes_a_R_ctrl", "eye_thinkcloseeyes_a_L_ctrl"}},
  {"\xe3\x81\xab\xe3\x82\x84\xe3\x82\x8a", {"mouth_happy_s_ctrl"}},
  {"\xe3\x81\xab\xe3\x82\x84\xe3\x82\x8a\xef\xbc\x92", {"mouth_happy_m_ctrl"}},
};

struct DirectVmdMouthAlias { const char* name; int shape; };
static constexpr DirectVmdMouthAlias kDirectVmdMouthAliases[] = {{u8"ワ",0},{u8"口横広げ",1}};
