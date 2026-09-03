
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacPendingBuilding

FacPendingBuildingCtrl = HL.Class('FacPendingBuildingCtrl', uiCtrl.UICtrl)






FacPendingBuildingCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.FAC_ON_PENDING_NODE_CHANGED] = 'FacOnPendingNodeChanged',
    [MessageConst.FAC_ON_PENDING_NODE_SLOT_SUBMIT_ITEM_CHANGED] = 'FacOnPendingNodeChanged',
}


FacPendingBuildingCtrl.m_slotId = HL.Field(HL.Number) << -1

FacPendingBuildingCtrl.m_getCell = HL.Field(HL.Function)

FacPendingBuildingCtrl.m_infos = HL.Field(HL.Table)




FacPendingBuildingCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeButton.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.FacMachine)
    end)

    self.m_slotId = arg.slotId

    self.view.cancelBtn.onClick:AddListener(function()
        self:_OnClickCancel()
    end)
    self.view.craftBtn.onClick:AddListener(function()
        self:_OnClickCraft()
    end)
    self.view.submitBtn.onClick:AddListener(function()
        self:_OnClickSubmit()
    end)

    self.m_getCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getCell(obj), LuaIndex(csIndex))
    end)

    self.view.name.text = FactoryUtils.getPendingSlotName(self.m_slotId)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.mainInputBindingGroupMonoTarget.groupId})
    self.view.decoTextImg:LoadSprite(UIConst.UI_SPRITE_BLUEPRINT, string.format("deco_facbuilding_text_0%d", self.m_slotId))

    self:RefreshList()

    if DeviceInfo.usingController then
        self.view.listNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
            if not isFocused then
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
        end)
    end
end




FacPendingBuildingCtrl.RefreshList = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipGradually)
    local slotInfo = GameInstance.remoteFactoryManager.currentChapterInfo:GetPendingPlaceSlot(self.m_slotId)
    local infoMap = {}

    local hasItemCanSubmit = false
    slotInfo:FillTotalCostItems({})
    for _, v in pairs(slotInfo.totalCostItems) do
        local id, count = v.Item1, v.Item2
        local data = Tables.itemTable[id]
        infoMap[id] = {
            id = id,
            needCount = count,
            submitCount = 0,
            sortId = 1,
            sortId1 = data.sortId1,
            sortId2 = data.sortId2,
        }
    end
    for _, v in pairs(slotInfo.submittedItems) do
        local id, count = v.Item1, v.Item2
        local info = infoMap[id]
        info.submitCount = count
        info.isCompleted = count >= info.needCount
        info.sortId = info.isCompleted and 0 or 1
    end
    self.m_infos = {}
    for _, v in pairs(infoMap) do
        table.insert(self.m_infos, v)
        if not v.isCompleted then
            if Utils.getItemCount(v.id, true, true) > 0 then
                hasItemCanSubmit = true
            end
        end
    end
    table.sort(self.m_infos, Utils.genSortFunction({ "sortId", "sortId1", "sortId2", "id" }))

    self.view.scrollList:UpdateCount(#self.m_infos, true, false, false, skipGradually == true)
    self.view.submitBtn.interactable = hasItemCanSubmit
end

FacPendingBuildingCtrl._OnUpdateCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local info = self.m_infos[index]
    cell.stateController:SetState(info.isCompleted and "Complete" or "Normal")
    cell.item:InitItem({ id = info.id }, true)
    if DeviceInfo.usingController then
        cell.item:SetExtraInfo({
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
            tipsPosTransform = cell.controllerHintNode,
            isSideTips = true,
        })
    end
    local ownCount = Utils.getItemCount(info.id, true, true)
    cell.ownCountTxt.text = UIUtils.setCountColor(ownCount, (info.needCount - info.submitCount) > ownCount)
    cell.submitCountTxt.text = string.format("%d/%d", info.submitCount, info.needCount)
    cell.progressBar.fillAmount = info.submitCount / info.needCount
end






FacPendingBuildingCtrl._OnClickCraft = HL.Method() << function(self)
    local deviceList = {}
    for _, info in ipairs(self.m_infos) do
        if not info.isCompleted then
            local buildingId = FactoryUtils.getItemBuildingId(info.id)
            local isEnough = info.needCount - info.submitCount <= 0 or Utils.getItemCount(info.id, true, true) >= info.needCount - info.submitCount
            if not isEnough then
                table.insert(deviceList, {
                    id = buildingId,
                    count = info.needCount - info.submitCount
                })
            end
        end
    end
    Notify(MessageConst.OPEN_FAC_BUILD_MODE_SELECT, { bluePrintData = deviceList })
end

FacPendingBuildingCtrl._OnClickSubmit = HL.Method() << function(self)
    if self:IsPlayingAnimationIn() then
        return
    end
    local items = {}
    for _, info in ipairs(self.m_infos) do
        if not info.isCompleted then
            local ownCount = Utils.getItemCount(info.id, true, true)
            local diff = info.needCount - info.submitCount
            items[info.id] = math.min(diff, ownCount)
        end
    end
    local sys = GameInstance.remoteFactoryManager.system
    sys:SubmitItemToPendingData(Utils.getCurrentChapterId(), self.m_slotId, items, function()
        self:FacOnPendingNodeChanged()
    end)
end

FacPendingBuildingCtrl._OnClickCancel = HL.Method() << function(self)
    local sys = GameInstance.remoteFactoryManager.system
    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_FAC_BLUEPRINT_CONFIRM_CANCEL_PENDING,
        warningContent = Language.LUA_FAC_BLUEPRINT_CONFIRM_CANCEL_PENDING_HINT,
        onConfirm = function()
            sys:CancelBatchPending(Utils.getCurrentChapterId(), self.m_slotId,
            function()
                AudioAdapter.PostEvent("Au_UI_Event_Blueprint_YellowShadow_Close")
                if PhaseManager:GetTopPhaseId() == PhaseId.FacMachine then
                    PhaseManager:PopPhase(PhaseId.FacMachine)
                end
            end)
        end
    })
end



FacPendingBuildingCtrl.m_isExiting = HL.Field(HL.Boolean) << false

FacPendingBuildingCtrl.FacOnPendingNodeChanged = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    local slotInfo = GameInstance.remoteFactoryManager.currentChapterInfo:GetPendingPlaceSlot(self.m_slotId)
    if slotInfo then

        self:RefreshList()
    else
        if not self.m_isExiting then
            self.m_isExiting = true

            if PhaseManager:GetTopPhaseId() == PhaseId.FacMachine then
                PhaseManager:PopPhase(PhaseId.FacMachine)
            else
                PhaseManager:ExitPhaseFast(PhaseId.FacMachine)
            end

            AudioAdapter.PostEvent("Au_UI_Event_Blueprint_BuildingBuild")
        end
    end
end



HL.Commit(FacPendingBuildingCtrl)
