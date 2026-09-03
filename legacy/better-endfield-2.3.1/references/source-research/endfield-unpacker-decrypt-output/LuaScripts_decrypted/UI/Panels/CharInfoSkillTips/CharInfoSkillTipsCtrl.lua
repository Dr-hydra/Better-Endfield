
local charFormationSkillTipsCtrl = require_ex('UI/Panels/CharFormationSkillTips/CharFormationSkillTipsCtrl')
local PANEL_ID = PanelId.CharInfoSkillTips
CharInfoSkillTipsCtrl = HL.Class('CharInfoSkillTipsCtrl', charFormationSkillTipsCtrl.CharFormationSkillTipsCtrl)

CharInfoSkillTipsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    CharInfoSkillTipsCtrl.Super.OnCreate(self, arg)
    self.m_labelText = Language["key_hint_char_change_default_skill"]
end




HL.Commit(CharInfoSkillTipsCtrl)
