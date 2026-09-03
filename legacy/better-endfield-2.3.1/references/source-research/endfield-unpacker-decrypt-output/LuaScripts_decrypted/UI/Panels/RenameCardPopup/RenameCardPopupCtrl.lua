
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.RenameCardPopup

RenameCardPopupCtrl = HL.Class('RenameCardPopupCtrl', uiCtrl.UICtrl)






RenameCardPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}


RenameCardPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.cancelButton.onClick:AddListener(function()
        self:Close()
    end)

    self.view.confirmButton.onClick:AddListener(function()
        self:Close()
        Notify(MessageConst.RESET_PLAYER_NAME_START, { nil, arg.itemId, arg.instId })
    end)

    local name = string.format(Language.LUA_FRIEND_NAME, Utils.getPlayerName(), GameInstance.player.playerInfoSystem.shortId)
    self.view.nameTxt.text = name

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end











HL.Commit(RenameCardPopupCtrl)
