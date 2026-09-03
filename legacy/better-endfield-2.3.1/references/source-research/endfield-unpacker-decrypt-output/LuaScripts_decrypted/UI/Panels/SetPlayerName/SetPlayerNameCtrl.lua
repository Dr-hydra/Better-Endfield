
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SetPlayerName
SetPlayerNameCtrl = HL.Class('SetPlayerNameCtrl', uiCtrl.UICtrl)





SetPlayerNameCtrl.s_messages = HL.StaticField(HL.Table) << {



    [MessageConst.ON_SET_PLAYER_NAME] = 'OnRequestClose',
}






SetPlayerNameCtrl.OnRequestClose = HL.Method() << function(self)
    self:Close()










end


SetPlayerNameCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.confirmBtn.onClick:AddListener(function()
        self:_SetPlayerName()
    end)
end

SetPlayerNameCtrl._SetPlayerName = HL.Method() << function(self)
    local roleName = self.view.nameInput.text
    GameInstance.player.playerInfoSystem:SetPlayerName(roleName)
end

HL.Commit(SetPlayerNameCtrl)
