local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ReflowFormalDialogue

ReflowFormalDialogueCtrl = HL.Class('ReflowFormalDialogueCtrl', uiCtrl.UICtrl)






ReflowFormalDialogueCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ReflowFormalDialogueCtrl.m_activityId = HL.Field(HL.String) << ""

ReflowFormalDialogueCtrl.m_closeCallback = HL.Field(HL.Function)


ReflowFormalDialogueCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_activityId = arg.activityId
    self.m_closeCallback = arg.closeCallback
    local _, table = Tables.activityReflowTable:TryGetValue(self.m_activityId)
    local cfg = table.reflowCfg

    self.view.roleImg:LoadSprite(string.format("%s/%s", UIConst.UI_SPRITE_CHAR_REMOTE_ICON_700, cfg.popupRoleIcon))
    self.view.nameTxt.text = cfg.popupRoleName
    self.view.descTxt.text = cfg.popupDesc

    self.view.bgMask.onClick:AddListener(function()
        if self.m_closeCallback then
            self.m_closeCallback()
        else
            self:PlayAnimationOutAndClose()
        end

        ActivityUtils.GameEventLogActivityDialogEnd(self.m_activityId)
    end)

    if DeviceInfo.usingController then
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    end

    ActivityUtils.GameEventLogActivityDialogStart(self.m_activityId)
end

HL.Commit(ReflowFormalDialogueCtrl)
