
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BattleComboSkill
BattleComboSkillCtrl = HL.Class('BattleComboSkillCtrl', uiCtrl.UICtrl)







BattleComboSkillCtrl.s_messages = HL.StaticField(HL.Table) << {

}


BattleComboSkillCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.comboSkillPanel:OnCreate(self.isDefaultPanel)
end

BattleComboSkillCtrl.OnShow = HL.Override() << function(self)
    self.view.comboSkillPanel:OnShow()
end

HL.Commit(BattleComboSkillCtrl)
