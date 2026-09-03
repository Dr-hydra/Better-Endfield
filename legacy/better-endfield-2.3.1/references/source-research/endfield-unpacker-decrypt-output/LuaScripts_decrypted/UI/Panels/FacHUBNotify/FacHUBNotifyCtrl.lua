
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacHUBNotify

FacHUBNotifyCtrl = HL.Class('FacHUBNotifyCtrl', uiCtrl.UICtrl)






FacHUBNotifyCtrl.s_messages = HL.StaticField(HL.Table) << {

}



FacHUBNotifyCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.view.closeBtn.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            self:Close()
            CS.Beyond.Gameplay.Conditions.OnOpenFacHubPanelWithoutNotify.Trigger()
        end)
    end)
    local info = GameInstance.player.facSpMachineSystem.offlineInfo
    local time = info.endOfflineCalcTimestamp
    if BEYOND_DEBUG_COMMAND then
        if arg then
            time = arg
        end
    end
    self.view.stopTimeTxt.text = os.date("!" .. Language.LUA_FAC_OFFLINE_OS_DATE_FORMAT, time + Utils.getServerTimeZoneOffsetSeconds())
    self.view.btnNode.gameObject:SetActive(not DeviceInfo.usingController)
end


HL.Commit(FacHUBNotifyCtrl)
