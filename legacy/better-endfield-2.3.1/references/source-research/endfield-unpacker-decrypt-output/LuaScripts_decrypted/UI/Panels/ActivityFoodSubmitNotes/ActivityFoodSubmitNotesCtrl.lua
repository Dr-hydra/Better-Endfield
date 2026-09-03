local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityFoodSubmitNotes
local PHASE_ID = PhaseId.ActivityFoodSubmitNotes
ActivityFoodSubmitNotesCtrl = HL.Class('ActivityFoodSubmitNotesCtrl', uiCtrl.UICtrl)






ActivityFoodSubmitNotesCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ActivityFoodSubmitNotesCtrl.m_getScrollListCell = HL.Field(HL.Function)

ActivityFoodSubmitNotesCtrl.m_luaIndex2Cell = HL.Field(HL.Table)

ActivityFoodSubmitNotesCtrl.m_luaIndex2StageId = HL.Field(HL.Table)

ActivityFoodSubmitNotesCtrl.m_selectedLuaIndex = HL.Field(HL.Number) << -1

ActivityFoodSubmitNotesCtrl.m_activityId = HL.Field(HL.String) << ""

ActivityFoodSubmitNotesCtrl.m_mainCtrl = HL.Field(HL.Any) << nil


local ShowStatus = {
    Locked = 0,
    Unlocked = 1,
    Completed = 2,
    Rewarded = 3,
}


ActivityFoodSubmitNotesCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_mainCtrl = arg.mainCtrl
    self.m_activityId = arg.activityId
    local selectedLuaIndex = arg.selectedLuaIndex
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self:_UpdateStageId(self.m_activityId)

    self.view.closeButton.onClick:RemoveAllListeners()
    self.view.closeButton.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.m_getScrollListCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getScrollListCell(obj), csIndex)
    end)

    local showNum = #self.m_luaIndex2StageId
    self.m_luaIndex2Cell = {}

    self.view.redDotScrollRect.getRedDotStateAt = function(csIndex)
        return self:GetRedDotStateAt(csIndex)
    end

    if showNum > 0 then
        if selectedLuaIndex < 0 then
            for i = 1, #self.m_luaIndex2StageId do
                local stageId = self.m_luaIndex2StageId[i]
                local showStatus = self.m_mainCtrl:GetStageShowState(stageId)
                if showStatus ~= ShowStatus.Locked then
                    local record = GameInstance.player.activitySystem:GetFoodSubmitNoteRedDotRecord(self.m_activityId, stageId)
                    local status = ActivityUtils.GetFoodSubmitStageState(self.m_activityId, stageId)
                    if status == GEnums.ActivityConditionalStageState.Rewarded and not record then
                        selectedLuaIndex = i
                        break
                    end
                end
            end
            if selectedLuaIndex < 0 then
                selectedLuaIndex = 1
            end
        end
        self.view.scrollList:UpdateCount(showNum, selectedLuaIndex - 1)

        self.view.rightNode:SetState("Empty")
        self.view.scrollList.onGraduallyShowFinish:RemoveAllListeners()
        self.view.scrollList.onGraduallyShowFinish:AddListener(function()
            self:_SelectCell(selectedLuaIndex)
        end)
    end

end

ActivityFoodSubmitNotesCtrl.GetRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, csIndex)
    local luaIndex = LuaIndex(csIndex)
    local stageId = self.m_luaIndex2StageId[luaIndex]
    local showState = self.m_mainCtrl:GetStageShowState(stageId)

    if showState == ShowStatus.Locked then
        return 0
    else
        local record = GameInstance.player.activitySystem:GetFoodSubmitNoteRedDotRecord(self.m_activityId, stageId)
        local status = ActivityUtils.GetFoodSubmitStageState(self.m_activityId, stageId)
        if status == GEnums.ActivityConditionalStageState.Rewarded then
            if record then
                return 0
            else
                return 1
            end
        else
            return 0
        end
    end
end


ActivityFoodSubmitNotesCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, csIndex)
    local luaIndex = LuaIndex(csIndex)

    self.m_luaIndex2Cell[luaIndex] = cell
    local stageId = self.m_luaIndex2StageId[luaIndex]

    local hasCfg, cfg = Tables.FoodSubmitStageIdTable:TryGetValue(stageId)
    if hasCfg then
        cell.nameTxt.text = cfg.name
    else
        cell.nameTxt.text = ""
    end

    local hasUiCfg, uiCfg = Tables.ActivitySubmitFoodTable:TryGetValue(stageId)
    if hasUiCfg then
        cell.foodImg:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, uiCfg.showImg)
    end

    local hasText, textCfg = Tables.ActivitySubmitTextTable:TryGetValue(self.m_activityId)
    if hasText then
        cell.lockedText.text = textCfg.foodNoteLockedtText
    end

    cell.showState = self.m_mainCtrl:GetStageShowState(stageId)

    if self.m_selectedLuaIndex == luaIndex then
        self:_UpdateCellSelectedState(cell)
    else
        self:_UpdateCellNormalState(cell)
    end

    self:_UpdateRedDot(cell, stageId, "Normal")

    cell.clickBtn.onClick:RemoveAllListeners()
    cell.clickBtn.onClick:AddListener(function()
        if cell.showState == ShowStatus.Locked then

            if hasText then
                Notify(MessageConst.SHOW_TOAST, textCfg.foodNoteLockedtToast)
            end

            if DeviceInfo.usingController then
                local preCell = self.m_luaIndex2Cell[luaIndex-1]
                if preCell ~= nil and preCell.showState ~= ShowStatus.Locked then
                    self:SetNaviTarget(preCell.clickBtn)
                else
                    local nextId = luaIndex + 1
                    if nextId > #self.m_luaIndex2StageId then
                        nextId = 1
                    end
                    local nextCell = self.m_luaIndex2Cell[nextId]
                    if nextCell ~= nil and nextCell.showState ~= ShowStatus.Locked then
                        self:SetNaviTarget(nextCell.clickBtn)
                    end
                end
            end
            return
        end

        self:_SelectCell(luaIndex)
    end)
end

ActivityFoodSubmitNotesCtrl._UpdateRedDot = HL.Method(HL.Any, HL.Any, HL.Any) << function(self, cell, stageId, mode)
    if cell.showState == ShowStatus.Locked then
        cell.redDot.gameObject:SetActive(false)
    else
        local record = GameInstance.player.activitySystem:GetFoodSubmitNoteRedDotRecord(self.m_activityId, stageId)
        local status = ActivityUtils.GetFoodSubmitStageState(self.m_activityId, stageId)
        if status == GEnums.ActivityConditionalStageState.Rewarded then
            cell.redDot.gameObject:SetActive(not record)
            if mode == "SelectCell" and not record then
                AudioManager.PostEvent("Au_UI_Event_DataLoading")
                self.view.animationWrapper:PlayWithTween("activity_foodsubmit_notes_refresh")
            end
        else
            cell.redDot.gameObject:SetActive(false)
        end
    end
end

ActivityFoodSubmitNotesCtrl._UpdateStageId = HL.Method(HL.String) << function(self, activityId)
    local stageTable = {}
    for key, value in pairs(Tables.FoodSubmitStageIdTable) do
        if value.activityId == activityId then
            local info = {
                stageId = key,
                sortId = value.sortId,
            }

            local showStage = false
            local stageState = self.m_mainCtrl:GetStageShowState(info.stageId)

            if stageState ~= ShowStatus.Locked then
                showStage = true
            end
            local hasUiCfg, uiCfg = Tables.ActivitySubmitFoodTable:TryGetValue(info.stageId)
            if hasUiCfg then
                if uiCfg.unlockShow then
                    showStage = true
                end
            else
                showStage = false
            end

            if showStage then
                table.insert(stageTable, info)
            end
        end
    end

    table.sort(stageTable, Utils.genSortFunction({ "sortId" }, true))

    self.m_luaIndex2StageId = {}
    for i = 1, #stageTable do
        self.m_luaIndex2StageId[i] = stageTable[i].stageId
    end
end


ActivityFoodSubmitNotesCtrl.GetNoteShowState = HL.Method(HL.Any).Return(HL.Any) << function(self, stageId)
    local status = ActivityUtils.GetFoodSubmitStageState(self.m_activityId, stageId)
    local showNote = ShowStatus.Locked
    if status == GEnums.ActivityConditionalStageState.Locked then
        showNote = false
    elseif status == GEnums.ActivityConditionalStageState.Unlocked then
        showNote = false
    elseif status == GEnums.ActivityConditionalStageState.Completed then
        showNote = true
    elseif status == GEnums.ActivityConditionalStageState.Rewarded then
        showNote = true
    end

    return showNote
end


ActivityFoodSubmitNotesCtrl._SelectCell = HL.Method(HL.Number) << function(self, luaIndex)
    if self.m_selectedLuaIndex == luaIndex then
        return
    end

    local selectedCell = self.m_luaIndex2Cell[self.m_selectedLuaIndex]
    if selectedCell ~= nil then
        self:_UpdateCellNormalState(selectedCell)
    end

    self.m_selectedLuaIndex = luaIndex
    local cell = self.m_luaIndex2Cell[luaIndex]
    if cell ~= nil then
        self:_UpdateCellSelectedState(cell)
        cell.selectedNodeAnim:PlayInAnimation()
        self:SetNaviTarget(cell.clickBtn)
    end

    local stageId = self.m_luaIndex2StageId[luaIndex]

    if self:GetNoteShowState(stageId) then
        local hasUiCfg, uiCfg = Tables.ActivitySubmitFoodTable:TryGetValue(stageId)
        if hasUiCfg then
            local status = ActivityUtils.GetFoodSubmitStageState(self.m_activityId, stageId)
            if status == GEnums.ActivityConditionalStageState.Rewarded then
                self:_UpdateRedDot(cell, stageId, "SelectCell")
                GameInstance.player.activitySystem:SetFoodSubmitNoteRedDotRecord(self.m_activityId, stageId)
                cell.redDot.gameObject:SetActive(false)
                Notify(MessageConst.ON_UPDATE_ACTIVITY_FOOD_SUBMIT_NODE_RED_DOT)
            end
            self.view.rightNode:SetState("Normal")
            self.view.detailsTitleTxt.text = uiCfg.name
            self.view.detailsDescTxt.text = uiCfg.noteDesc

        else
            self.view.rightNode:SetState("Empty")
        end
    else
        self.view.rightNode:SetState("Empty")
    end
end

ActivityFoodSubmitNotesCtrl._UpdateCellSelectedState = HL.Method(HL.Any) << function(self, cell)
    if cell == nil then
        return
    end
    if cell.showState == ShowStatus.Locked then
        self:_SetCellState(cell, "SelectedLocked")
    elseif cell.showState == ShowStatus.Unlocked then
        self:_SetCellState(cell, "SelectedUnlocked")
    elseif cell.showState == ShowStatus.Completed then
        self:_SetCellState(cell, "SelectedFinish")
    elseif cell.showState == ShowStatus.Rewarded then
        self:_SetCellState(cell, "SelectedFinish")
    end
end

ActivityFoodSubmitNotesCtrl._UpdateCellNormalState = HL.Method(HL.Any) << function(self, cell)
    if cell == nil then
        return
    end

    if cell.showState == ShowStatus.Locked then
        self:_SetCellState(cell, "Locked")
    elseif cell.showState == ShowStatus.Unlocked then
        self:_SetCellState(cell, "Unlocked")
    elseif cell.showState == ShowStatus.Completed then
        self:_SetCellState(cell, "Finish")
    elseif cell.showState == ShowStatus.Rewarded then
        self:_SetCellState(cell, "Finish")
    end
end

ActivityFoodSubmitNotesCtrl._SetCellState = HL.Method(HL.Any, HL.String) << function(self, cell, state)
    cell.nodeState:SetState(state)
end

HL.Commit(ActivityFoodSubmitNotesCtrl)
