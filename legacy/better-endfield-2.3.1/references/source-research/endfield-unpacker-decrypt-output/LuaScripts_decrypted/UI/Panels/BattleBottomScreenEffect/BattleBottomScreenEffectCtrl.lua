
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BattleBottomScreenEffect
BattleBottomScreenEffectCtrl = HL.Class('BattleBottomScreenEffectCtrl', uiCtrl.UICtrl)





BattleBottomScreenEffectCtrl.s_messages = HL.StaticField(HL.Table) << {

}


BattleBottomScreenEffectCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    GameInstance.effectManager:SetBottomScreenEffectRoot(self.view.battleBottomScreenEffectPanel)
end

BattleBottomScreenEffectCtrl.OnClose = HL.Override() << function(self)
    GameInstance.effectManager:SetBottomScreenEffectRoot(nil)
end









HL.Commit(BattleBottomScreenEffectCtrl)
