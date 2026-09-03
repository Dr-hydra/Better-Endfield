
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacPowerDiffuser
FacPowerDiffuserCtrl = HL.Class('FacPowerDiffuserCtrl', uiCtrl.UICtrl)

local FAC_NOT_SHOW_TEMPLATE_ID_MINER = "miner_1"
local TECH_DIFFUSER_CONNECTABLE_ID = "tech_tundra_2_diffuser_connectable_1"
local ADVANCED_BUILDING_ID = "power_diffuser_2"







FacPowerDiffuserCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacPowerDiffuserCtrl.m_nodeId = HL.Field(HL.Any)

FacPowerDiffuserCtrl.m_uiInfo = HL.Field(HL.Userdata)

FacPowerDiffuserCtrl.m_powerInfo = HL.Field(HL.Userdata)

FacPowerDiffuserCtrl.m_diffuserInfos = HL.Field(HL.Table)

FacPowerDiffuserCtrl.m_poleInfos = HL.Field(HL.Table)

FacPowerDiffuserCtrl.m_diffuserCacheList = HL.Field(HL.Forward('UIListCache'))

FacPowerDiffuserCtrl.m_poleCacheList = HL.Field(HL.Forward('UIListCache'))

FacPowerDiffuserCtrl.m_isAdvancedBuilding = HL.Field(HL.Boolean) << false



FacPowerDiffuserCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_uiInfo = arg.uiInfo
    local nodeId = self.m_uiInfo.nodeId
    self.m_nodeId = nodeId

    self.view.buildingCommon:InitBuildingCommon(self.m_uiInfo, {
        onPowerChanged = function()
            self:_Refresh()
        end,
    })
    self.view.buildingCommon.view.powerToggle.gameObject:SetActiveIfNecessary(false)

    self:_InitPowerInfo()

    self.m_diffuserCacheList = UIUtils.genCellCache(self.view.diffuserCell)
    self.m_isAdvancedBuilding = self.m_uiInfo.buildingId == ADVANCED_BUILDING_ID or not GameInstance.player.facTechTreeSystem:NodeIsLocked(TECH_DIFFUSER_CONNECTABLE_ID)
    if self.m_isAdvancedBuilding then
        self.m_poleCacheList = UIUtils.genCellCache(self.view.poleCell)
    end
    self:_Refresh()

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.contentRect)
    local scrollRectYEnable = self.view.scrollRect.rect.size.y < self.view.contentRect.rect.size.y
    self.view.controllerHint.gameObject:SetActiveIfNecessary(scrollRectYEnable)

    self.view.buildingCommon.view.buttonsNaviGroup:NaviToThisGroup()

    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self:_RefreshPowerInfo()
            self:_RefreshAllDriversPower()
        end
    end)
end

FacPowerDiffuserCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, isActive)
    self.view.controllerHint.enabled = isActive
    if self.m_diffuserInfos == nil or #self.m_diffuserInfos == 0 then
        InputManagerInst:ToggleBinding(self.view.naviGroup.FocusBindingId, false)
    end
end

FacPowerDiffuserCtrl._Refresh = HL.Method() << function(self)
    local infos = {}
    local linkedNodes = self.m_uiInfo.powerPole.coveredNodeIds
    local nodeCount = 0
    for _, nodeId in pairs(linkedNodes) do
        local nodeHandler = FactoryUtils.getBuildingNodeHandler(nodeId)
        local success, buildingData = Tables.factoryBuildingTable:TryGetValue(nodeHandler.templateId)
        if nodeHandler ~= nil and success then
            if not FacConst.NOT_SHOW_IN_POWER_POLE_FC_NODE_TYPES[nodeHandler.nodeType] and nodeHandler.templateId ~= FAC_NOT_SHOW_TEMPLATE_ID_MINER and buildingData.needPower then

                table.insert(infos, {
                    nodeId = nodeId,
                    nodeHandler = nodeHandler,
                })
                nodeCount = nodeCount + 1
            end
        end
    end
    self.m_diffuserInfos = infos
    self.view.diffuserEmpty.gameObject:SetActive(nodeCount <= 0)
    self.view.diffuserContent.gameObject:SetActive(nodeCount > 0)
    InputManagerInst:ToggleBinding(self.view.naviGroup.FocusBindingId, nodeCount > 0)
    self.m_diffuserCacheList:Refresh(nodeCount, function(cell, index)
        self:_OnUpdateDiffuserCell(cell, index)
    end)

    self.view.machineController:SetState(self.m_isAdvancedBuilding and "Unlock" or "Lock")
    if self.m_isAdvancedBuilding then
        infos = {}
        local node = self.m_uiInfo.nodeHandler
        linkedNodes = CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryUtil.GetAllConnectedNodes(node)
        for _, linkNode in pairs(linkedNodes) do
            local linkNodeEntry
            for _, v in pairs(infos) do
                if v.templateId == linkNode.templateId then
                    linkNodeEntry = v
                    break
                end
            end
            if not linkNodeEntry then
                linkNodeEntry = {
                    templateId = linkNode.templateId,
                    nodeCount = 0
                }
                table.insert(infos, linkNodeEntry)
            end
            linkNodeEntry.nodeCount = linkNodeEntry.nodeCount + 1
        end

        nodeCount = #infos
        self.m_poleInfos = infos
        self.view.poleEmpty.gameObject:SetActive(nodeCount <= 0)
        self.m_poleCacheList:Refresh(nodeCount, function(cell, index)
            self:_OnUpdatePoleCell(cell, index)
        end)
    end
end

FacPowerDiffuserCtrl._InitPowerInfo = HL.Method() << function(self)
    self.m_powerInfo = FactoryUtils.getCurRegionPowerInfo()
    local powerStorageCapacity = self.m_powerInfo.powerSaveMax
    self.view.maxRestPowerText.text = string.format("/%s", UIUtils.getNumString(powerStorageCapacity))
    self:_RefreshPowerInfo()
end

FacPowerDiffuserCtrl._RefreshPowerInfo = HL.Method() << function(self)
    local powerInfo = self.m_powerInfo

    self.view.providePowerText.text = string.format("/%s", UIUtils.getNumString(powerInfo.powerGen))
    self.view.currentPowerText.text = UIUtils.getNumString(powerInfo.powerCost)

    local restPower = powerInfo.powerSaveCurrent

    self.view.restPowerText.text = UIUtils.getNumString(restPower)
end

FacPowerDiffuserCtrl._OnUpdateDiffuserCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local info = self.m_diffuserInfos[index]
    local nodeMsg = info.nodeHandler
    local data = Tables.factoryBuildingTable:GetValue(nodeMsg.templateId)

    cell.name.text = data.name
    cell.icon:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_PANEL_ICON, data.iconOnPanel)
    cell.toggle:InitCommonToggle(function(isOn)
        self:_OnToggleMachine(cell, info, isOn)
        self:_RefreshMachineCellToggleState(cell)
    end, not nodeMsg.isDeactive, true)
    if DeviceInfo.usingController then
        cell.toggle.view.keyHint:SetBindingId(cell.toggle.toggle.toggleBindingId)
    end

    cell.bgBtn.onClick:RemoveAllListeners()
    cell.bgBtn.onClick:AddListener(function()
        cell.toggle:Toggle()
        self:_RefreshMachineCellToggleState(cell)
    end)

    cell.bgOff.gameObject:SetActiveIfNecessary(index % 2 == 1)
    self:_RefreshMachineCellToggleState(cell)
    self:_RefreshDriverPower(cell, info)
end

FacPowerDiffuserCtrl._OnUpdatePoleCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local info = self.m_poleInfos[index]
    local data = Tables.factoryBuildingTable:GetValue(info.templateId)
    local isOdd = index % 2 > 0

    cell.name.text = data.name
    cell.icon:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_PANEL_ICON, data.iconOnPanel)
    cell.bgOdd.gameObject:SetActive(isOdd)
    cell.bgEven.gameObject:SetActive(not isOdd)
    cell.power.text = tostring(info.nodeCount)
end

FacPowerDiffuserCtrl._RefreshMachineCellToggleState = HL.Method(HL.Any) << function(self, cell)
    if cell == nil then
        return
    end

    local isOn = cell.toggle.toggle.isOn
    cell.machineInfoNode.color = isOn and self.view.config.COLOR_MACHINE_OPENED or self.view.config.COLOR_MACHINE_CLOSED
end

FacPowerDiffuserCtrl._RefreshAllDriversPower = HL.Method() << function(self)


    for index = 1, self.m_diffuserCacheList:GetCount() do
        local cell = self.m_diffuserCacheList:GetItem(index)
        local info = self.m_diffuserInfos[index]
        if cell ~= nil and info ~= nil then
            self:_RefreshDriverPower(cell, info)
        end
    end
end

FacPowerDiffuserCtrl._RefreshDriverPower = HL.Method(HL.Table, HL.Table) << function(self, cell, info)
    cell.power.text = FactoryUtils.getCurBuildingConsumePower(info.nodeId)
    cell.stateIcon:LoadSprite(FactoryUtils.getBuildingStateIconName(info.nodeId))
end

FacPowerDiffuserCtrl._OnToggleMachine = HL.Method(HL.Table, HL.Table, HL.Boolean) << function(self, cell, info, isOn)
    GameInstance.player.remoteFactory.core:Message_OpEnableNode(Utils.getCurrentChapterId(), info.nodeId, isOn)
end

HL.Commit(FacPowerDiffuserCtrl)
