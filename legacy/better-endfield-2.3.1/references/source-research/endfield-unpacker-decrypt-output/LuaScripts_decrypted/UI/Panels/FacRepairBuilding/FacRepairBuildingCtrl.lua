local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacRepairBuilding
FacRepairBuildingCtrl = HL.Class('FacRepairBuildingCtrl', uiCtrl.UICtrl)





FacRepairBuildingCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.FAC_ON_BUILDING_REPAIRED] = 'OnBuildingRepaired',
}

FacRepairBuildingCtrl.m_nodeId = HL.Field(HL.Any)

FacRepairBuildingCtrl.m_repairId = HL.Field(HL.String) << ""

FacRepairBuildingCtrl.m_repairNeedItem = HL.Field(HL.Any) << ""

FacRepairBuildingCtrl.m_costItemCache = HL.Field(HL.Forward("UIListCache"))



FacRepairBuildingCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local nodeId = arg.nodeId
    self.m_nodeId = nodeId

    local buildingNode = FactoryUtils.getBuildingNodeHandler(nodeId)
    self.m_repairId = buildingNode.instKey
    self.m_repairNeedItem = arg.needRepairItems
    local buildingId = buildingNode.templateId
    local tableData = Tables.factoryBuildingTable:GetValue(buildingId)
    local buildingData = {
        nodeId = nodeId,
        itemId = FactoryUtils.getBuildingItemId(buildingId)
    }
    setmetatable(buildingData, { __index = tableData })
    self.view.buildingCommon.view.controllerSideMenuBtn.gameObject:SetActive(false)
    self.view.buildingCommon:InitBuildingCommon(nil, { data = buildingData })

    self.view.repairBtn.onClick:AddListener(function()
        self:_OnClickRepair()
    end)

    self:UpdateCount(true)
    self:_StartCoroutine(function()

        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_LARGER_UPDATE_INTERVAL)
            self:UpdateCount(false)
        end
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

FacRepairBuildingCtrl._OnClickRepair = HL.Method() << function(self)
    local chapterId = Utils.getCurrentChapterId()
    GameInstance.player.remoteFactory.core:Message_OpRepairNode(chapterId, self.m_nodeId, function(op, ret)
        self:OnBuildingRepaired()
    end)
end

FacRepairBuildingCtrl.UpdateCount = HL.Method(HL.Boolean) << function(self, isInit)
    local repairItems = self.m_repairNeedItem
    if isInit then
        self.m_costItemCache = UIUtils.genCellCache(self.view.costItem)
        self.m_costItemCache:Refresh(repairItems.Count, function(cell, index)
            local bundle = repairItems[CSIndex(index)]
            local info = {
                id = bundle.id,
                count = bundle.count,
            }
            cell.item:InitItem(info, true)
            if DeviceInfo.usingController then
                cell.item:SetExtraInfo({
                    tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
                    tipsPosTransform = self.view.costItemList.transform,
                    isSideTips = true,
                })
            end
        end)
    end
    local isEnough = true
    local isEmpty = repairItems.Count == 0
    self.view.costContent.gameObject:SetActive(not isEmpty)
    self.view.emptyCost.gameObject:SetActive(isEmpty)
    self.m_costItemCache:Update(function(cell, index)
        local bundle = repairItems[CSIndex(index)]
        local count = Utils.getBagItemCount(bundle.id)
        local isLack = count < bundle.count
        cell.item:UpdateCountSimple(bundle.count, isLack)
        cell.storageNode:InitStorageNode(count, bundle.count, nil, true)
        if isLack then
            isEnough = false
        end
    end)
    self.view.repairBtn.gameObject:SetActive(isEnough)
    self.view.notEnoughHint.gameObject:SetActive(not isEnough)
end

FacRepairBuildingCtrl.OnBuildingRepaired = HL.Method() << function(self)
    self:_ShowRepairEffect()
end

FacRepairBuildingCtrl.m_inRepairEffect = HL.Field(HL.Boolean) << false

FacRepairBuildingCtrl._ShowRepairEffect = HL.Method() << function(self)
    AudioAdapter.PostEvent("au_ui_fac_repair_complete")


    GameInstance.remoteFactoryManager:RepairBuilding(self.m_nodeId, 0)
    CoroutineManager:StartCoroutine(function()
        coroutine.wait(0.5)
        GameInstance.remoteFactoryManager:RepairBuilding(self.m_nodeId, 1)
    end)

    self:_CloseRepairPanel()
end

FacRepairBuildingCtrl._CloseRepairPanel = HL.Method() << function(self)

    if not PhaseManager:IsOpenAndValid(PhaseId.FacMachine) then
        return
    end

    if PhaseManager:GetTopPhaseId() ~= PhaseId.FacMachine then
        PhaseManager:ExitPhaseFast(PhaseId.FacMachine)
    else
        if not self:IsPlayingAnimationOut() then
            PhaseManager:PopPhase(PhaseId.FacMachine)
        end
    end
end

HL.Commit(FacRepairBuildingCtrl)
