local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacPowerTerminal
FacPowerTerminalCtrl = HL.Class('FacPowerTerminalCtrl', uiCtrl.UICtrl)







FacPowerTerminalCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacPowerTerminalCtrl.m_powerInfo = HL.Field(CS.Proto.SCD_FACTORY_SYNC_BLACKBOARD_POWER)


FacPowerTerminalCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local uiInfo = arg.uiInfo

    self.view.buildingCommon:InitBuildingCommon(uiInfo)

    self:_RefreshPowerInfo()
    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_RefreshPowerInfo()
        end
    end)

    self:_RefreshPowerTerminal(uiInfo.nodeHandler.instKey)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

FacPowerTerminalCtrl._RefreshPowerTerminal = HL.Method(HL.String) << function(self, instKey)
    local success, powerTerminalCfg = Tables.factorySpecialPowerPoleTable:TryGetValue(instKey)
    if not success then
        logger.error("FacPowerTerminalCtrl->Can't find cfg in specialPowerPole, instKey: ", instKey)
        return
    end

    self.view.terminalDescText.text = powerTerminalCfg.buildingDesc
    self.view.buildingCommon.view.machineName.text = powerTerminalCfg.buildingName
end

FacPowerTerminalCtrl._RefreshPowerInfo = HL.Method() << function(self)
    local powerInfo = FactoryUtils.getCurRegionPowerInfo()
    local powerStorageCapacity = powerInfo.powerSaveMax
    local restPower = powerInfo.powerSaveCurrent

    self.view.maxRestPowerText.text = string.format("/%s", UIUtils.getNumString(powerStorageCapacity))
    self.view.providePowerText.text = string.format("/%s", UIUtils.getNumString(powerInfo.powerGen))
    self.view.currentPowerText.text = UIUtils.getNumString(powerInfo.powerCost)
    self.view.restPowerText.text = UIUtils.getNumString(restPower)
end

HL.Commit(FacPowerTerminalCtrl)
