local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityFoodSubmit
local PHASE_ID = PhaseId.ActivityFoodSubmit
ActivityFoodSubmitCtrl = HL.Class('ActivityFoodSubmitCtrl', uiCtrl.UICtrl)






ActivityFoodSubmitCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_SUBMIT_ITEM] = 'OnSubmitItem',
    [MessageConst.ON_UPDATE_ACTIVITY_FOOD_SUBMIT_NODE_RED_DOT] = 'OnUpdateNodeRedDot',
    [MessageConst.ON_ACTIVITY_FOOD_SUBMIT_FORCE_UPDATE_PANEL] = '_ForceUpdatePanel',
    [MessageConst.ON_MANUAL_CRAFT_PANEL_CLOSE] = 'OnManualCraftPanelClose',
    [MessageConst.ON_MANUAL_CRAFT_POPUP_PANEL_CLOSE] = 'OnManualCraftPopupPanelClose',
    [MessageConst.ON_ACTIVITY_UPDATED] = '_OnActivityUpdated',
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE] = '_OnMultiStageUpdate',
}

ActivityFoodSubmitCtrl.m_getScrollListCell = HL.Field(HL.Function)

ActivityFoodSubmitCtrl.m_getHaveListCell = HL.Field(HL.Function)

ActivityFoodSubmitCtrl.m_getRewardListCell = HL.Field(HL.Function)

ActivityFoodSubmitCtrl.m_luaIndex2Cell = HL.Field(HL.Table)

ActivityFoodSubmitCtrl.m_luaIndex2StageId = HL.Field(HL.Table)

ActivityFoodSubmitCtrl.m_luaIndex2ShowState = HL.Field(HL.Table)

ActivityFoodSubmitCtrl.m_selectedLuaIndex = HL.Field(HL.Number) << -1

ActivityFoodSubmitCtrl.m_controllerCurLuaIndex = HL.Field(HL.Number) << -1

ActivityFoodSubmitCtrl.m_selectedCell = HL.Field(HL.Any) << nil

ActivityFoodSubmitCtrl.m_submitItemId = HL.Field(HL.String) << ""

ActivityFoodSubmitCtrl.m_submitNeedNum = HL.Field(HL.Number) << 0

ActivityFoodSubmitCtrl.m_submitHaveNum = HL.Field(HL.Number) << 0

ActivityFoodSubmitCtrl.m_submitHaveCell = HL.Field(HL.Any) << nil

ActivityFoodSubmitCtrl.m_activityId = HL.Field(HL.String) << ""

ActivityFoodSubmitCtrl.m_rewardItems = HL.Field(HL.Table)

ActivityFoodSubmitCtrl.m_luaIndex2RewardCell = HL.Field(HL.Table)

ActivityFoodSubmitCtrl.m_inMainPanel = HL.Field(HL.Boolean) << true

ActivityFoodSubmitCtrl.m_inSubmitting = HL.Field(HL.Boolean) << false

ActivityFoodSubmitCtrl.m_haveSucceedSubmit = HL.Field(HL.Boolean) << false

ActivityFoodSubmitCtrl.m_fromDialog = HL.Field(HL.Boolean) << false

ActivityFoodSubmitCtrl.m_foodDetailsState = HL.Field(HL.String) << ""

ActivityFoodSubmitCtrl.m_exitFoodFocusFlag = HL.Field(HL.Boolean) << false

local ShowStatus = {
    Locked = 0,
    Unlocked = 1,
    Completed = 2,
    Rewarded = 3,
}



ActivityFoodSubmitCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_fromDialog = args and args.fromDialog or false
    if self.m_fromDialog then
        self.m_activityId = args.activityId
    else
        self.m_activityId = "activity_submit_food_1"
    end

    self:UpdateAllData()
end

ActivityFoodSubmitCtrl.UpdateAllData = HL.Method() << function(self)
    if not self:CheckHaveActivityData() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_FORBIDDEN)
        self:_OnClickCloseBtn()
        return
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self:_UpdateStageId(self.m_activityId)
    self.view.closeBtn.onClick:RemoveAllListeners()
    self.view.closeBtn.onClick:AddListener(function()
        self:_OnClickCloseBtn()
    end)

    self.view.backBtn.onClick:RemoveAllListeners()
    self.view.backBtn.onClick:AddListener(function()
        self.m_inMainPanel = true
        self.view.foodDetailsInputBindingGroupMonoTarget.internalEnabled = not self.m_inMainPanel
        self.view.foodDetails.animationWrapper:PlayOutAnimation(function()
            self:_BackToMainPanel()
        end)
    end)

    self.view.helpBtn.onClick:RemoveAllListeners()
    self.view.helpBtn.onClick:AddListener(function()
        local _, activityData = Tables.activityTable:TryGetValue(self.m_activityId)
        local instructionId = activityData.instructionId
        UIManager:Open(PanelId.InstructionBook, instructionId)
    end)

    self.view.notesBtn.onClick:RemoveAllListeners()
    self.view.notesBtn.onClick:AddListener(function()
        local selectedLuaIndex = -1
        if not self.m_inMainPanel then
            selectedLuaIndex = self.m_selectedLuaIndex
        end

        PhaseManager:OpenPhase(PhaseId.ActivityFoodSubmitNotes, {
            mainCtrl = self,
            activityId = self.m_activityId,
            selectedLuaIndex = selectedLuaIndex,
        })
    end)

    if DeviceInfo.usingTouch then
        self.view.notesNode:SetState("Mobile")
    else
        self.view.notesNode:SetState("Standalone")
    end

    self.m_getScrollListCell = UIUtils.genCachedCellFunction(self.view.foodList.foodItemScrollList)
    self.view.foodList.foodItemScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateMainCell(self.m_getScrollListCell(obj), csIndex)
    end)

    self.m_getHaveListCell = UIUtils.genCachedCellFunction(self.view.foodDetails.haveScrollList)
    self.view.foodDetails.haveScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateHaveCell(self.m_getHaveListCell(obj), csIndex)
    end)
    self.m_getRewardListCell = UIUtils.genCachedCellFunction(self.view.foodDetails.rewardScrollList)
    self.view.foodDetails.rewardScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateRewardCell(self.m_getRewardListCell(obj), csIndex)
    end)

    self.view.main:SetState("FoodList")
    self.m_inMainPanel = true
    self.view.foodDetailsInputBindingGroupMonoTarget.internalEnabled = not self.m_inMainPanel
    self.m_luaIndex2Cell = {}
    local fastRunLuaIndex = 1
    local controllerFocusLuaIndex = 1
    for i = 1, #self.m_luaIndex2StageId do
        local stageId = self.m_luaIndex2StageId[i]
        local showStatus = self:GetStageShowState(stageId)
        if showStatus ~= ShowStatus.Locked then
            local status = ActivityUtils.GetFoodSubmitStageState(self.m_activityId, stageId)
            if status ~= GEnums.ActivityConditionalStageState.Rewarded then
                fastRunLuaIndex = i - 1
                controllerFocusLuaIndex = i
                break
            end
        end
    end
    if fastRunLuaIndex < 1 then
        fastRunLuaIndex = 1
    end
    self.view.foodList.foodItemScrollList:UpdateCount(#self.m_luaIndex2StageId, fastRunLuaIndex - 1)

    self.view.foodList.foodItemScrollList.onGraduallyShowFinish:RemoveAllListeners()
    self.view.foodList.foodItemScrollList.onGraduallyShowFinish:AddListener(function()
        if DeviceInfo.usingController then
            if self.m_luaIndex2Cell[controllerFocusLuaIndex] ~= nil then
                self:SetNaviTarget(self.m_luaIndex2Cell[controllerFocusLuaIndex].clickBtn)
            end
        end
    end)


    local timeVal = ActivityUtils.GetFoodSubmitCurGoToRedDot()
    GameInstance.player.activitySystem:SetFoodSubmitGoToRedDotRecord(self.m_activityId, timeVal)
    Notify(MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE)
    self:_UpdateNotesBtnRedDot()
end


ActivityFoodSubmitCtrl._OnClickCloseBtn = HL.Method() << function(self)
    if self.m_fromDialog then
        local res = 0
        if self.m_haveSucceedSubmit then
            res = 1
        end
        Notify(MessageConst.DIALOG_CHANGE_NEXT_INDEX, { phaseId = PHASE_ID, nextIndex = res, })
    end
    AudioAdapter.PostEvent("Au_UI_Menu_Common_Large_Close")
    PhaseManager:PopPhase(PHASE_ID)
end


ActivityFoodSubmitCtrl._BackToMainPanel = HL.Method() << function(self)
    self.view.main:SetState("FoodList")
    self.m_inMainPanel = true
end

ActivityFoodSubmitCtrl._UpdateStageId = HL.Method(HL.String) << function(self, activityId)
    local stageTable = {}
    for key, value in pairs(Tables.FoodSubmitStageIdTable) do
        if value.activityId == activityId then
            local info = {
                stageId = key,
                sortId = value.sortId,
            }
            local showStage = false
            local stageState = self:GetStageShowState(info.stageId)

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
                if stageState ~= ShowStatus.Locked then
                    local stageRecord = GameInstance.player.activitySystem:GetFoodSubmitTabStageRedDotRecord(activityId, info.stageId)
                    if not stageRecord then
                        GameInstance.player.activitySystem:SetFoodSubmitTabStageRedDotRecord(activityId, info.stageId)
                    end
                end
            end
        end
    end

    table.sort(stageTable, Utils.genSortFunction({ "sortId" }, true))

    self.m_luaIndex2StageId = {}
    self.m_luaIndex2ShowState = {}
    for i = 1, #stageTable do
        self.m_luaIndex2StageId[i] = stageTable[i].stageId
    end
end

ActivityFoodSubmitCtrl._OnMultiStageUpdate = HL.Method(HL.Any) << function(self, arg)
    local id = unpack(arg)
    if id ~= self.m_activityId then
        return
    end

    if self:CheckHaveActivityData() then
        self:_ForceUpdatePanel()
    end
end


ActivityFoodSubmitCtrl._OnActivityUpdated = HL.Method(HL.Any) << function(self, arg)
    local id = unpack(arg)
    if id ~= self.m_activityId then
        return
    end

    if not self:CheckHaveActivityData() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_FORBIDDEN)
        local topPhaseId = PhaseManager:GetTopPhaseId()
        if topPhaseId == PhaseId.ActivityFoodSubmitNotes then
            PhaseManager:PopPhase(PhaseId.ActivityFoodSubmitNotes, function()
                self:_OnClickCloseBtn()
            end)
        elseif topPhaseId == PHASE_ID then
            self:_OnClickCloseBtn()
        end
    end
end

ActivityFoodSubmitCtrl._ForceUpdatePanel = HL.Method() << function(self)
    local topPhaseId = PhaseManager:GetTopPhaseId()
    if topPhaseId == PhaseId.ActivityFoodSubmitNotes then
        PhaseManager:PopPhase(PhaseId.ActivityFoodSubmitNotes, function()
            self.view.animationWrapper:PlayOutAnimation(function()
                self:UpdateAllData()
                self.view.animationWrapper:PlayInAnimation()
                self.view.foodList.animationWrapper:PlayInAnimation()
            end)
        end)
    elseif topPhaseId == PHASE_ID then
        self.view.animationWrapper:PlayOutAnimation(function()
            self:UpdateAllData()
            self.view.animationWrapper:PlayInAnimation()
            self.view.foodList.animationWrapper:PlayInAnimation()
        end)
    end
end

ActivityFoodSubmitCtrl._UpdateNotesBtnRedDot = HL.Method() << function(self)
    if self.m_luaIndex2StageId == nil then
        return
    end

    local unReadNum = 0
    for i = 1, #self.m_luaIndex2StageId do
        local stageId = self.m_luaIndex2StageId[i]
        local showStatus = self:GetStageShowState(stageId)
        if showStatus ~= ShowStatus.Locked then
            local record = GameInstance.player.activitySystem:GetFoodSubmitNoteRedDotRecord(self.m_activityId, stageId)
            local status = ActivityUtils.GetFoodSubmitStageState(self.m_activityId, stageId)
            if status == GEnums.ActivityConditionalStageState.Rewarded and not record then
                unReadNum = unReadNum + 1
            end
        end
    end

    self.view.notesBtnRedDot.gameObject:SetActive(unReadNum > 0)
end

ActivityFoodSubmitCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    if not self:CheckHaveActivityData() then
        return
    end
    self:_UpdateNotesBtnRedDot()
end

ActivityFoodSubmitCtrl.OnUpdateNodeRedDot = HL.Method() << function(self)
    if not self:CheckHaveActivityData() then
        return
    end
    self:_UpdateNotesBtnRedDot()
end

ActivityFoodSubmitCtrl._OnUpdateMainCell = HL.Method(HL.Any, HL.Number) << function(self, cell, csIndex)
    local luaIndex = LuaIndex(csIndex)
    self.m_luaIndex2Cell[luaIndex] = cell
    local stageId = self.m_luaIndex2StageId[luaIndex]
    local showStatus = self:_OnUpdateMainCellNodeState(cell, luaIndex)


    cell.numTxt.text = string.format(Language.LUA_ACTIVITY_SUBMIT_FOOD_MAIN_NUM_PREFIX, luaIndex)
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
        cell.lockedText.text = textCfg.foodLockedText
    end

    cell.clickBtn.onClick:RemoveAllListeners()
    cell.clickBtn.onClick:AddListener(function()
        if showStatus == ShowStatus.Locked then
            if hasText then
                Notify(MessageConst.SHOW_TOAST, textCfg.foodLockedToast)
            end
            return
        end
        self.m_selectedLuaIndex = luaIndex

        self.view.main:SetState("FoodDetails")
        self.m_inMainPanel = false
        self.view.foodDetailsInputBindingGroupMonoTarget.internalEnabled = not self.m_inMainPanel
        self:initFoodDetails()
    end)

    cell.clickBtn.onIsNaviTargetChanged = function(active)
        if active then
            self.m_controllerCurLuaIndex = luaIndex
        end
    end
end

ActivityFoodSubmitCtrl._OnUpdateMainCellNodeState = HL.Method(HL.Any, HL.Number).Return(HL.Any) << function(self, cell, luaIndex)
    local stageId = self.m_luaIndex2StageId[luaIndex]
    local showStatus = self:GetStageShowState(stageId)
    if showStatus == ShowStatus.Locked then
        cell.nodeState:SetState("Locked")
        local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
        if activity ~= nil then
            local _,stageData = activity.stageDataDict:TryGetValue(stageId)
            local stageStartTime = stageData.OpenTimeTs + Utils.getServerTimeZoneOffsetSeconds()
            local curTime = DateTimeUtils.GetCurrentTimestampBySeconds() + Utils.getServerTimeZoneOffsetSeconds()
            local disTime = stageStartTime - curTime
            cell.lockedTimeTxt.text = UIUtils.getLeftTime(disTime)
        else
            cell.lockedTimeTxt.text = ""
        end
    elseif showStatus == ShowStatus.Unlocked then
        cell.nodeState:SetState("Unlock")
    elseif showStatus == ShowStatus.Completed then
        cell.nodeState:SetState("Finish")
    elseif showStatus == ShowStatus.Rewarded then
        cell.nodeState:SetState("Finish")
    end
    self.m_luaIndex2ShowState[luaIndex] = showStatus
    return showStatus
end

ActivityFoodSubmitCtrl.GetStageShowState = HL.Method(HL.Any).Return(HL.Any) << function(self, stageId)
    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local stageData = activityData:GetStageData(stageId)
    local status = GEnums.ActivityConditionalStageState.Locked
    if stageData ~= nil then
        status = GEnums.ActivityConditionalStageState.__CastFrom(stageData.Status)
    end

    local showStatus = ShowStatus.Locked
    if status == GEnums.ActivityConditionalStageState.Locked then
        showStatus = ShowStatus.Locked
    elseif status == GEnums.ActivityConditionalStageState.Unlocked then
        showStatus = ShowStatus.Unlocked
    elseif status == GEnums.ActivityConditionalStageState.Completed then
        showStatus = ShowStatus.Completed
    elseif status == GEnums.ActivityConditionalStageState.Rewarded then
        showStatus = ShowStatus.Rewarded
    end

    return showStatus
end

ActivityFoodSubmitCtrl.initFoodDetails = HL.Method() << function(self)
    local stageId = self.m_luaIndex2StageId[self.m_selectedLuaIndex]

    local hasUiCfg, uiCfg = Tables.ActivitySubmitFoodTable:TryGetValue(stageId)
    if hasUiCfg then

        self.view.foodDetails.descTxt.text = uiCfg.submitDesc
        self.m_submitItemId = uiCfg.submitItemId
    else
        self.view.foodDetails.titleTxt.text = ""
        self.view.foodDetails.descTxt.text = ""
        self.m_submitItemId = ""
    end

    local hasCfg, cfg = Tables.SubmitItem:TryGetValue(self.m_submitItemId)
    if hasCfg then
        local itemId = cfg.paramData[0].paramList[0].valueStringList[0]
        local itemCfg = Tables.itemTable[itemId]
        self.view.foodDetails.titleTxt.text = itemCfg.name
    end

    self.m_rewardItems = {}
    self.m_luaIndex2RewardCell = {}
    local hasCfg, cfg = Tables.FoodSubmitStageIdTable:TryGetValue(stageId)
    if hasCfg then
        local hasReward, rewardData = Tables.rewardTable:TryGetValue(cfg.rewardId)
        if hasReward then
            for _, v in pairs(rewardData.itemBundles) do
                table.insert(self.m_rewardItems, v)
            end
        end
    end

    self.view.foodDetails.rewardScrollList:UpdateCount(#self.m_rewardItems)

    self.view.foodDetails.submitBtn.onClick:RemoveAllListeners()
    self.view.foodDetails.submitBtn.onClick:AddListener(function()
        if self.m_inSubmitting then
            return
        end
        self.m_inSubmitting = true
        GameInstance.player.activitySystem:SendActivitySubmitFood(self.m_activityId, stageId, self.m_submitItemId)
    end)
    self.view.foodDetails.submittedBtn.interactable = false

    self:UpdateSubmitNum()
    self:UpdateFoodDetailsState()

    self.view.foodDetails.haveScrollList:UpdateCount(1)
end

ActivityFoodSubmitCtrl.UpdateSubmitNum = HL.Method() << function(self)
    local hasItemCfg, itemCfg = Tables.SubmitItem:TryGetValue(self.m_submitItemId)
    if hasItemCfg then
        self.m_submitNeedNum = itemCfg.paramData[0].paramList[1].valueIntList[0]

        local itemId = itemCfg.paramData[0].paramList[0].valueStringList[0]
        local itemBag = GameInstance.player.inventory.itemBag:GetOrFallback(Utils.getCurrentScope())
        self.m_submitHaveNum = itemBag:GetCount(itemId)
    end
end

ActivityFoodSubmitCtrl.CheckHaveActivityData = HL.Method().Return(HL.Boolean) << function(self)
    local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    if activity then
        return true
    else
        return false
    end
end

ActivityFoodSubmitCtrl.UpdateFoodDetailsState = HL.Method() << function(self)
    local stageId = self.m_luaIndex2StageId[self.m_selectedLuaIndex]
    local showStatus = ShowStatus.Locked
    if stageId ~= nil then
        showStatus = self:GetStageShowState(stageId)
    end

    self.m_foodDetailsState = ""
    if showStatus == ShowStatus.Locked then
        self:SetFoodDetailsState("Dissatisfied")
    elseif showStatus == ShowStatus.Unlocked then
        if self.m_submitHaveNum >= self.m_submitNeedNum then
            self:SetFoodDetailsState("Satisfy")
        else
            self:SetFoodDetailsState("Dissatisfied")
        end
    elseif showStatus == ShowStatus.Completed then
        self:SetFoodDetailsState("Finish")
    elseif showStatus == ShowStatus.Rewarded then
        self:SetFoodDetailsState("Finish")
    end
end

ActivityFoodSubmitCtrl.SetFoodDetailsState = HL.Method(HL.Any) << function(self, state)
    self.view.foodDetails.foodState:SetState(state)
    self.m_foodDetailsState = state
end

ActivityFoodSubmitCtrl.OnSubmitItem = HL.Method(HL.Any) << function(self, submitId)
    if (submitId[1] == self.m_submitItemId) then
        self.m_haveSucceedSubmit = true
        self.m_inSubmitting = false
        local cell = self.m_luaIndex2Cell[self.m_selectedLuaIndex]
        if cell ~= nil then
            local showStatus = self:_OnUpdateMainCellNodeState(cell, self.m_selectedLuaIndex)
            if not self.m_inMainPanel then
                self:initFoodDetails()
            end
        end
        self:_UpdateNotesBtnRedDot()
        self:_BackToMainPanel()
    end
end

ActivityFoodSubmitCtrl.OnManualCraftPanelClose = HL.Method() << function(self)
    if not self:CheckHaveActivityData() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_FORBIDDEN)
        self:_StartTimer(1.5, function()
            PhaseManager:PopPhase(PHASE_ID, function()
                GameWorld.dialogManager:SkipCurrentDialog(true)
            end)
        end)
        return
    end

    if self.m_inMainPanel then
        return
    end
    if DeviceInfo.usingController then
        self.m_exitFoodFocusFlag = true
    end
    self:UpdateSubmitNum()
    self:UpdateFoodDetailsState()
    if self.m_submitHaveCell ~= nil then
        self:_OnUpdateHaveNumCell(self.m_submitHaveCell, self.m_submitHaveNum)
    end
end

ActivityFoodSubmitCtrl.OnManualCraftPopupPanelClose = HL.Method() << function(self)
    if not self:CheckHaveActivityData() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_FORBIDDEN)
        self:_StartTimer(1.5, function()
            PhaseManager:PopPhase(PHASE_ID, function()
                GameWorld.dialogManager:SkipCurrentDialog(true)
            end)
        end)
        return
    end
    if DeviceInfo.usingController then
        self.m_exitFoodFocusFlag = true
    end
end

ActivityFoodSubmitCtrl._OnUpdateHaveCell = HL.Method(HL.Any, HL.Number) << function(self, cell, csIndex)

    local luaIndex = LuaIndex(csIndex)
    local stageId = self.m_luaIndex2StageId[self.m_selectedLuaIndex]
    local hasUiCfg, uiCfg = Tables.ActivitySubmitFoodTable:TryGetValue(stageId)
    if hasUiCfg then
        self.m_submitItemId = uiCfg.submitItemId
    else
        self.m_submitItemId = ""
    end
    self.m_submitHaveCell = cell
    local hasCfg, cfg = Tables.SubmitItem:TryGetValue(self.m_submitItemId)
    if hasCfg then
        local itemId = cfg.paramData[0].paramList[0].valueStringList[0]

        cell.itemBlack:InitItem({ id = itemId, count = self.m_submitNeedNum, gained = false }, function()
            if self.m_exitFoodFocusFlag then
                self.m_exitFoodFocusFlag = false
                self.view.foodDetails.haveScrollListSelectableNaviGroup:ManuallyStopFocus()
                self.view.foodDetails.rewardScrollListSelectableNaviGroup:ManuallyStopFocus()
            else
                cell.itemBlack:ShowTips()
            end
        end)

        cell.itemBlack:SetExtraInfo({
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
            tipsPosTransform = self.view.foodDetails.topNode,
            isSideTips = true,
        })

        self:_OnUpdateHaveNumCell(cell, self.m_submitHaveNum)
    end
end

ActivityFoodSubmitCtrl._OnUpdateHaveNumCell = HL.Method(HL.Any, HL.Number) << function(self, cell, submitHaveNum)
    cell.numTxt.text = self.m_submitHaveNum
    if self.m_foodDetailsState == "Dissatisfied" then
        cell.numLayout.gameObject:SetActive(true)
        cell.numTxt.color = self.view.config.DISSATISFIED_COLOR
    elseif self.m_foodDetailsState == "Satisfy" then
        cell.numLayout.gameObject:SetActive(true)
        cell.numTxt.color = self.view.config.SATISFIED_COLOR
    elseif self.m_foodDetailsState == "Finish" then
        cell.numLayout.gameObject:SetActive(false)
    end
end

ActivityFoodSubmitCtrl._OnUpdateRewardCell = HL.Method(HL.Any, HL.Number) << function(self, cell, csIndex)
    local luaIndex = LuaIndex(csIndex)
    self.m_luaIndex2RewardCell[luaIndex] = cell
    cell:InitItem(self.m_rewardItems[luaIndex], function()
        if self.m_exitFoodFocusFlag then
            self.m_exitFoodFocusFlag = false
            self.view.foodDetails.haveScrollListSelectableNaviGroup:ManuallyStopFocus()
            self.view.foodDetails.rewardScrollListSelectableNaviGroup:ManuallyStopFocus()
        else
            cell:ShowTips()
        end
    end)

    cell:SetExtraInfo({
        tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
        tipsPosTransform = self.view.foodDetails.topNode,
        isSideTips = true,
    })

end

HL.Commit(ActivityFoodSubmitCtrl)
