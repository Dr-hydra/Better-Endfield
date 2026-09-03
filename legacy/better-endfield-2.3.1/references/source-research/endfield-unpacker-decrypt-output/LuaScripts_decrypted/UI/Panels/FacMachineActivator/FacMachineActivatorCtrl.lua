
local facMachineCrafterCtrl = require_ex('UI/Panels/FacMachineCrafter/FacMachineCrafterCtrl')
local PANEL_ID = PanelId.FacMachineActivator

FacMachineActivatorCtrl = HL.Class('FacMachineActivatorCtrl', facMachineCrafterCtrl.FacMachineCrafterCtrl)





FacMachineActivatorCtrl.s_overrideMessages = HL.StaticField(HL.Table) << {
}





local DECO_STATE = {
    NotActivated = "NotActivated",
    Available = "Available",
    Closed = "Closed",
    NoElectricity = "NoElectricity",
    NoNetWork = "NoNetWork",
}

local function _GetCurrentConsumeInfo(uiInfo)
    local curConsumeCount = 0
    local inActive = false
    if uiInfo.activatorCost then
        curConsumeCount = uiInfo.activatorCost.currentBufCnt * 6
        inActive = uiInfo.activatorCost.inActive
    end
    return curConsumeCount, inActive
end

FacMachineActivatorCtrl.m_activatorNeedCount = HL.Field(HL.Number) << 0

FacMachineActivatorCtrl.m_consumeItemName = HL.Field(HL.String) << ""

FacMachineActivatorCtrl._OnExpendTypeMachineInit = HL.Override() << function(self)
    local activatorCfg = Tables.factoryTransmuterTable:GetValue(self.m_uiInfo.nodeHandler.templateId)

    local needCount, maxCount = activatorCfg.consumeRate, activatorCfg.consumeRateUpperLimit
    self.m_activatorNeedCount = needCount
    self.view.activatorNodes.consumeBar.fillAmount = needCount / maxCount

    local consumeItemCfg = Tables.itemTable:GetValue(activatorCfg.consumeItem)
    self.view.activatorNodes.consumeItemName.text = string.format(Language.LUA_FACTORY_ACTIVATOR_CONSUME_ITEM_TIPS, consumeItemCfg.name)
    self.view.activatorNodes.consumeMax.text = maxCount
    self.m_consumeItemName = consumeItemCfg.name

    self.view.activatorNodes.item:InitItem({ id = activatorCfg.consumeItem }, function()
        if DeviceInfo.usingController then
            self.view.activatorNodes.item:ShowActionMenu()
            return
        end
        self.view.activatorNodes.item:SetSelected(true)
        self.view.activatorNodes.item:ShowTips(nil, function()
            self.view.activatorNodes.item:SetSelected(false)
        end)
    end)
    self.view.activatorNodes.item.actionMenuArgs = {}
    self.view.activatorNodes.item.view.button.onIsNaviTargetChanged = function(active)
        if active then
            self.view.contentBindingGroup.enabled = true
        end
    end
    InputManagerInst:SetBindingText(self.view.activatorNodes.item.view.button.hoverConfirmBindingId, Language["key_hint_item_open_action_menu"])
    self.view.activatorNodes.needNum.text = needCount

    local curConsumeCount = _GetCurrentConsumeInfo(self.m_uiInfo)
    self.view.activatorNodes.activatorPointer:InitActivatorPointer(maxCount, curConsumeCount)
end

FacMachineActivatorCtrl._OnExpendTypeMachineUpdate = HL.Override() << function(self)
    local curConsumeCount = _GetCurrentConsumeInfo(self.m_uiInfo)

    self.view.activatorNodes.averageNum.text = curConsumeCount
    self.view.activatorNodes.averageNum.color = curConsumeCount >= self.m_activatorNeedCount and self.view.config.COLOR_GREEN or self.view.config.COLOR_RED
    self.view.activatorNodes.activatorPointer:RefreshConsumePointer(curConsumeCount)
end

FacMachineActivatorCtrl._OnExpendTypeStateUpdate = HL.Override(HL.Userdata) << function(self, state)
    if state == GEnums.FacBuildingState.NoPower then
        self.view.activatorNodes.consumeDecoState:SetState(DECO_STATE.NoElectricity)
    elseif state == GEnums.FacBuildingState.NotInPowerNet then
        self.view.activatorNodes.consumeDecoState:SetState(DECO_STATE.NoNetWork)
    elseif state == GEnums.FacBuildingState.Closed then
        self.view.activatorNodes.consumeDecoState:SetState(DECO_STATE.Closed)
    elseif state == GEnums.FacBuildingState.InActive then
        self.view.activatorNodes.consumeDecoState:SetState(DECO_STATE.NotActivated)
    else
        self.view.activatorNodes.consumeDecoState:SetState(DECO_STATE.Available)
    end

    local postProcessText
    if state == GEnums.FacBuildingState.InActive then
        postProcessText = string.format(Language.LUA_FACTORY_ACTIVATOR_CONSUME_ITEM_LACK_TIPS, self.m_consumeItemName)
    end

    self.view.activatorNodes.activeAnim.gameObject:SetActiveIfNecessary(state == GEnums.FacBuildingState.Idle or state == GEnums.FacBuildingState.Normal)

    local useStateText = FactoryUtils.refreshStateNodeByState(self.view.facStateNode, self.view.facProgressNode, state, postProcessText)
    self.view.cacheArea:RefreshAreaBlockState(state == GEnums.FacBuildingState.Blocked)
    if not useStateText then
        self.view.facProgressNode:SwitchAudioPlayingState(state == GEnums.FacBuildingState.Normal)
    end
end



HL.Commit(FacMachineActivatorCtrl)
