
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharJoinToast
CharJoinToastCtrl = HL.Class('CharJoinToastCtrl', uiCtrl.UICtrl)







CharJoinToastCtrl.s_messages = HL.StaticField(HL.Table) << {
}


CharJoinToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeBtn.onClick:AddListener(function()
        self:_OnCloseBtnClick()
    end)

    self:RefreshCharJoinToastInfo(arg.charId)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    GameInstance.player.guide:OnCharJoinByMainline()
end








CharJoinToastCtrl.ShowCharJoinToast = HL.StaticMethod(HL.Any) << function(arg)

    local msg = unpack(arg)
    local charId = msg.CharTemplateId
    PhaseManager:GoToPhase(PhaseId.CharJoinToast, {
        charId = charId,
    })
end

CharJoinToastCtrl.RefreshCharJoinToastInfo = HL.Method(HL.String) << function(self, charId)
    if string.isEmpty(charId) then
        return
    end

    local charInfo = Tables.characterTable[charId]
    if charInfo == nil then
        return
    end
    local charName = charInfo.name
    self.view.charName.text = charName

    self.view.charImage:LoadSprite(UIConst.UI_SPRITE_CHAR_IMAGE_510, charId)

    AudioManager.PostEvent("au_ui_popup_new_char")
    Utils.triggerVoice("chrbark_Introduce", charId)
end

CharJoinToastCtrl._OnCloseBtnClick = HL.Method() << function(self)
    if self:IsPlayingAnimationIn() then
        return
    end

    PhaseManager:PopPhase(PhaseId.CharJoinToast)
end

HL.Commit(CharJoinToastCtrl)
