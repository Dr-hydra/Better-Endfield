local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
ActivityVersionGuide = HL.Class('ActivityVersionGuide', UIWidgetBase)

ActivityVersionGuide.m_activityId = HL.Field(HL.String) << ''

ActivityVersionGuide.m_guideDataList = HL.Field(HL.Table)

ActivityVersionGuide.m_getCell = HL.Field(HL.Function)

ActivityVersionGuide.m_listCells = HL.Field(HL.Table)

ActivityVersionGuide.m_focusIndex = HL.Field(HL.Number) << 1

ActivityVersionGuide.m_normalizedPosition = HL.Field(HL.Number) << 1

ActivityVersionGuide.m_genCellFunc = HL.Field(HL.Function)

ActivityVersionGuide.m_taskCells = HL.Field(HL.Any)

ActivityVersionGuide.InitVersionGuide = HL.Method(HL.Any) << function(self, args)
    self:RegisterMessage(MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE, function(args)
        self:_OnMultiStageUpdate(args)
    end)
    self:RegisterMessage(MessageConst.ON_ACTIVITY_UPDATED, function(args)
        self:_OnActivityUpdate(args)
    end)
    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    local success, activityData = Tables.activityTable:TryGetValue(self.m_activityId)
    if success then
        self.view.descText.text = activityData.desc
    end
    self.m_taskCells = UIUtils.genCellCache(self.view.missionCell)


    self.view.redDotScrollRect.getRedDotStateAt = function(csIndex)
        return self:GetRedDotStateAt(csIndex)
    end

    local status = GameInstance.player.activitySystem:GetActivityStatus(self.m_activityId)
    if status == GEnums.ActivityStatus.InProgress or status == GEnums.ActivityStatus.Completed then

        self.view.mainStateCtrl:SetState("MissionMode")
        self:_RefreshMissionMode()
    else
        self.view.mainStateCtrl:SetState("PreMode")
    end


    if DeviceInfo.usingController then

        self.view.rightNaviGroup.onIsTopLayerChanged:RemoveAllListeners()
        self.view.rightNaviGroup.onIsTopLayerChanged:AddListener(function(isTopLayer)
            if isTopLayer then
                self:_SetAsNaviTarget(self.m_focusIndex)
            end
        end)
        if ActivityUtils.isActivityUnlocked(self.m_activityId) then
            local viewBindingId = self:BindInputPlayerAction("common_view_item", function()
                self:_SetAsNaviTarget(self.m_focusIndex)
            end)
            self.view.rightNaviGroup.onIsTopLayerChanged:AddListener(function(active)
                InputManagerInst:ToggleBinding(viewBindingId, not active)
                InputManagerInst:ToggleGroup(self.view.enterNode.groupId, not active)
            end)
        end
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    end
end

ActivityVersionGuide._RefreshMissionMode = HL.Method() << function(self)

    self.m_listCells = {}


    self:_CollectGuideData()


    self:_RefreshGuides()


    self:_RefreshTaskCells()
end

ActivityVersionGuide._CollectGuideData = HL.Method() << function(self)
    self.m_guideDataList = {}

    local activityData = Tables.activityVersionGuideStageTable[self.m_activityId]


    local stageListCount = activityData.stageList.Count
    for i = 1, stageListCount do
        local stageData = activityData.stageList[CSIndex(i)]
        local dataRef = {
            stageId = stageData.stageId,
            stageConfig = stageData,
            index = i,
            status = nil,
            sortId = stageData.sortId
        }
        table.insert(self.m_guideDataList, dataRef)
    end
end

ActivityVersionGuide._SortGuideDataList = HL.Method() << function(self)






    for _, guideInfo in ipairs(self.m_guideDataList) do
        local status = guideInfo.status
        if status == "Available" then
            guideInfo._sortPriority = 1
        elseif status == "Goto" then
            guideInfo._sortPriority = 2
        elseif status == "Complete" then
            guideInfo._sortPriority = 3
        else
            guideInfo._sortPriority = 2
        end
    end


    table.sort(self.m_guideDataList, Utils.genSortFunction({ "_sortPriority", "sortId" }, true))
end

ActivityVersionGuide._RefreshTaskCells = HL.Method() << function(self)

    self.m_taskCells:Refresh(#self.m_guideDataList, function(cell, index)
        self:_OnUpdateCell(cell, index)
    end)
end

ActivityVersionGuide._SetAsNaviTarget = HL.Method(HL.Number) << function(self, index)
    if index <= 0 or not DeviceInfo.usingController then
        return
    end
    local cell = self.m_taskCells:Get(index)
    if cell then
        self:SetNaviTarget(cell.naviDecorator)
    end
end

ActivityVersionGuide.OnShow = HL.Method() << function(self)
    self.view.scrollRect.verticalNormalizedPosition = self.m_normalizedPosition
end

ActivityVersionGuide.OnHide = HL.Method() << function(self)
    self.m_normalizedPosition = self.view.scrollRect.verticalNormalizedPosition
end

ActivityVersionGuide.OnActivityCenterNaviFailed = HL.Method() << function(self)
    self.view.scrollRect.verticalNormalizedPosition = 1
    self.m_focusIndex = 1
    self:_SetAsNaviTarget(self.m_focusIndex)
end

ActivityVersionGuide._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local guideInfo = self.m_guideDataList[index]
    local stageConfig = guideInfo.stageConfig




    local stageData = self:_GetStageData(guideInfo.stageId)


    local state = self:_GetStageStateString(stageData.Status)
    cell.stateController:SetState(state)

    if state == "Available" then

        local playAfterInAnim = function()
            local success, isPlayed = ClientDataManagerInst:GetBool("activity_guide_wuling_stage_complete_anim_played" .. guideInfo.stageId, false)
            if not success or not isPlayed then

                cell.animationWrapper:Play("activityguidewulingcell_change", function()
                    cell.animationWrapper:Play("activityguidewulingcellavi_loop")
                end)
                ClientDataManagerInst:SetBool("activity_guide_wuling_stage_complete_anim_played" .. guideInfo.stageId, true, false, EClientDataTimeValidType.Permanent)
            else

                cell.animationWrapper:Play("activityguidewulingcellavi_loop")
            end
        end


        if cell.animationWrapper.curState == CS.Beyond.UI.UIConst.AnimationState.In then

            cell.animationWrapper:PlayInAnimation(playAfterInAnim)
        else

            playAfterInAnim()
        end
    end

    local conditionId = stageConfig.conditions.conditionId
    local progressToCompare = stageConfig.conditions.progressToCompare
    local progressCurrent
    if state == "Complete" or state == "Available" then
        progressCurrent = progressToCompare
    else
        local success, progress = stageData.Conditions.Values:TryGetValue(conditionId)
        progressCurrent = progress
    end

    cell.missionTxt.text = stageConfig.desc


    local normalizedValue = math.min(progressCurrent / progressToCompare, 1.0)
    cell.scrollbar.size = normalizedValue
    cell.scrollbar.value = 0
    cell.numberTxt.text = string.format("%d/%d", progressCurrent, progressToCompare)


    cell.clickBtn.onClick:RemoveAllListeners()
    if state == "Available" then
        cell.clickBtn.onClick:AddListener(function()
            self:_OnReceiveReward(stageConfig)
        end)
    end


    cell.btnGoto.onClick:RemoveAllListeners()
    if state == "Goto" then
        cell.btnGoto.onClick:AddListener(function()
            self:_OnJumpClick(stageConfig, index)
        end)
    end

    cell.redDot:InitRedDot("ActivityGuideWulingStage", { self.m_activityId, stageConfig.stageId }, nil, self.view.redDotScrollRect)


    local rewardId = stageConfig.rewardId
    local rewardBundles = UIUtils.getRewardItems(rewardId)
    cell.cache = cell.cache or UIUtils.genCellCache(cell.reward)
    cell.cache:Refresh(#rewardBundles, function(innerCell, innerIndex)
        innerCell:InitItem(rewardBundles[innerIndex], function()
            innerCell:ShowTips()
        end)
        innerCell:SetExtraInfo({
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
            tipsPosTransform = self.view.controllerHintRect or cell.transform,
            isSideTips = true,
        })
        innerCell.view.getNode.gameObject:SetActive(state == "Complete")
    end)


    if DeviceInfo.usingController then
        cell.focusRect.onClick:RemoveAllListeners()
        if state == "Available" then
            InputManagerInst:ToggleBinding(cell.focusRect.onClick.bindingId, true)
            cell.focusRect.onClick:AddListener(function()
                self:_OnReceiveReward(stageConfig)
            end)
        elseif state == "Goto" then
            InputManagerInst:ToggleBinding(cell.focusRect.onClick.bindingId, true)
            cell.focusRect.onClick:AddListener(function()
                self:_OnJumpClick(stageConfig)
            end)
        else


            InputManagerInst:ToggleBinding(cell.focusRect.onClick.bindingId, false)
        end


        InputManagerInst:ToggleGroup(cell.reward.groupId,false)

        cell.keyHintRewards.gameObject:SetActive(false)
        cell.naviDecorator.onIsNaviTargetChanged = function(isTarget)
            InputManagerInst:ToggleGroup(cell.reward.groupId,isTarget)
            cell.keyHintRewards.gameObject:SetActive(isTarget)

            if isTarget then
                ActivityUtils.setFalseNewActivityConditionalStage(guideInfo.stageConfig.stageId)
            end
        end

        cell.rewardLayout.onIsFocusedChange:AddListener(function(isFocused)
            if isFocused then
                self.m_focusIndex = index
            else
                self:_SetAsNaviTarget(self.m_focusIndex)
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
        end)
    end
end

ActivityVersionGuide._RefreshGuides = HL.Method() << function(self)

    for index, guideInfo in ipairs(self.m_guideDataList) do
        local stageData = self:_GetStageData(guideInfo.stageConfig.stageId)
        guideInfo.status = self:_GetStageStateString(stageData.Status)
    end
    self:_SortGuideDataList()

    if self.m_taskCells then
        self.m_taskCells:Refresh(#self.m_guideDataList, function(cell, index)
            self:_OnUpdateCell(cell, index)
        end)
    end
end

ActivityVersionGuide._OnMultiStageUpdate = HL.Method(HL.Table) << function(self, args)
    local id = unpack(args)
    if id == self.m_activityId and ActivityUtils.isActivityUnlocked(id) then
        self:_RefreshGuides()
    end
end

ActivityVersionGuide._OnActivityUpdate = HL.Method(HL.Table) << function(self, args)
    local id = unpack(args)
    if id == self.m_activityId and ActivityUtils.isActivityUnlocked(id) then


        self:_RefreshMissionMode()
    end
end

ActivityVersionGuide._OnJumpClick = HL.Method(HL.Any, HL.Number) << function(self, guideData, index)
    local jumpId = guideData.jumpId
    local succ, cfg = Tables.systemJumpTable:TryGetValue(jumpId)
    ActivityUtils.setFalseNewActivityConditionalStage(guideData.stageId)
    if succ then
        Utils.jumpToSystem(jumpId)

        self.m_focusIndex = index
    else
        logger.error("ActivityGuideWuling: Invalid jumpId", jumpId)
    end
end


ActivityVersionGuide._GetStageData = HL.Method(HL.String).Return(HL.Any) << function(self, stageId)
    local activityCS = GameInstance.player.activitySystem:GetActivity(self.m_activityId)

    local success, stageData = activityCS.stageDataDict:TryGetValue(stageId)
    return stageData
end

ActivityVersionGuide._GetStageStateString = HL.Method(HL.Number).Return(HL.String) << function(self, stageStatus)
    if stageStatus == 0 or stageStatus == 1 then
        return "Goto"
    elseif stageStatus == 2 then
        return "Available"
    elseif stageStatus == 3 then
        return "Complete"
    else
        return "Goto"
    end
end


ActivityVersionGuide._OnReceiveReward = HL.Method(HL.Any) << function(self, guideData)
    ActivityUtils.setFalseNewActivityConditionalStage(guideData.stageId)
    GameInstance.player.activitySystem:SendReceiveRewardConditionMultiStage(self.m_activityId, self:GetAllCanReceiveStageIds())

    self.view.scrollRect.verticalNormalizedPosition = 1
    self.m_focusIndex = 1
    self:_SetAsNaviTarget(self.m_focusIndex)
end

ActivityVersionGuide.GetAllCanReceiveStageIds = HL.Method().Return(HL.Table) << function(self)
    local stageIds = {}
    for _, stage in ipairs(self.m_guideDataList) do
        if stage.status == "Available" then
            table.insert(stageIds, stage.stageId)
        end
    end
    return stageIds
end

ActivityVersionGuide.GetRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
    local luaIndex = LuaIndex(index)
    if luaIndex < 1 or luaIndex > #self.m_guideDataList then
        return 0
    end

    local guideInfo = self.m_guideDataList[luaIndex]
    if not guideInfo then
        return 0
    end

    local stageId = guideInfo.stageConfig.stageId
    local hasRedDot, redDotType = RedDotManager:GetRedDotState("ActivityGuideWulingStage", { self.m_activityId, stageId })
    if hasRedDot then
        return redDotType or UIConst.RED_DOT_TYPE.Normal
    else
        return 0
    end
end

ActivityVersionGuide._OnDestroy = HL.Override() << function(self)
    local status = GameInstance.player.activitySystem:GetActivityStatus(self.m_activityId)
    if status == GEnums.ActivityStatus.InProgress or status == GEnums.ActivityStatus.Completed then

        for _, guideInfo in ipairs(self.m_guideDataList) do
            ActivityUtils.setFalseNewActivityConditionalStage(guideInfo.stageConfig.stageId)
        end
    end
end

HL.Commit(ActivityVersionGuide)
return ActivityVersionGuide
