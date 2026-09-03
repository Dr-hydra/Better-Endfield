
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.AdventureStage

AdventureStageCtrl = HL.Class('AdventureStageCtrl', uiCtrl.UICtrl)


AdventureStageCtrl.m_curAdventureStage = HL.Field(HL.Number) << -1

AdventureStageCtrl.m_adventureMaxStage = HL.Field(HL.Number) << -1

AdventureStageCtrl.m_curAdventureStageTaskInfos = HL.Field(HL.Table)

AdventureStageCtrl.m_adventureStageRewardCellCache = HL.Field(HL.Forward("UIListCache"))

AdventureStageCtrl.m_taskCellList = HL.Field(HL.Table)

AdventureStageCtrl.m_selectedGroupType = HL.Field(HL.Number) << 0

AdventureStageCtrl.m_canSwitchDomain = HL.Field(HL.Boolean) << false

AdventureStageCtrl.m_domainDropDownInfo = HL.Field(HL.Table)

AdventureStageCtrl.m_groupType2DomainInfo = HL.Field(HL.Table)

AdventureStageCtrl.m_slotInfosMap = HL.Field(HL.Table)

AdventureStageCtrl.m_stageTempDisplayOverride = HL.Field(HL.Table)





AdventureStageCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ADVENTURE_TASK_MODIFY] = 'OnAdventureTaskModify',
    [MessageConst.ON_ADVENTURE_BOOK_STAGE_MODIFY] = 'OnAdventureBookStageModify',
    [MessageConst.ON_ADVENTURE_BOOK_SWITCH_SAME_TAB] = 'OnAdventureTabChangedSame',
}


AdventureStageCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    arg = arg or {}
    local adventure = self.view
    adventure.incBtn.onClick:AddListener(function()
        self:_OnIncBtnClick()
    end)
    adventure.decBtn.onClick:AddListener(function()
        self:_OnDecBtnClick()
    end)

    adventure.getStageReward.onClick:AddListener(function()
        self:_OnGetStageRewardBtnClick()
    end)

    self.view.rewardListNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
        adventure.controllerFocusHintNode.gameObject:SetActive(not isFocused)
    end)

    self.m_adventureStageRewardCellCache = UIUtils.genCellCache(adventure.itemReward)


    self.m_taskCellList = {}
    for i = 1, 6 do
        local cell = self.view.taskGroup["adventureTaskCell"..i]
        table.insert(self.m_taskCellList, cell)
    end

    self.m_selectedGroupType = arg.selectedGroupType or GEnums.AdventureTaskGroupType.Common:GetHashCode()
    self.m_domainDropDownInfo = {}
    self.m_groupType2DomainInfo = {}
    self.m_slotInfosMap = {}
    self.m_stageTempDisplayOverride = {}
    self:_InitData()
    self:_InitDomainDropdown()
    self:_ResetAdventureStage()
    if arg.adventureStageViewIndex ~= nil then
        local v = math.max(1, math.min(arg.adventureStageViewIndex, self.m_adventureMaxStage))
        self.m_curAdventureStage = v
        self:_SetStageText(self.m_curAdventureStage)
        arg.adventureStageViewIndex = nil
    end
    self:_RefreshAdventurePage(true)
end

AdventureStageCtrl.OnShow = HL.Override() << function(self)
    Notify(MessageConst.HIDE_ITEM_TIPS)
    if next(self.m_stageTempDisplayOverride) ~= nil then
        self.m_stageTempDisplayOverride = {}
        self:_RefreshAdventurePage(false)
    end
    for luaIndex, cell in pairs(self.m_taskCellList) do
        cell:PlayInAniAndDelayTime(self.view.config.TASK_CELL_SHOW_DELAY_TIME * CSIndex(luaIndex))
    end
end

AdventureStageCtrl._OnIncBtnClick = HL.Method() << function(self)
    self.m_curAdventureStage = self.m_curAdventureStage + 1
    self:_SetStageText(self.m_curAdventureStage)

    self.m_stageTempDisplayOverride = {}
    self:_RefreshAdventurePage(false)
end

AdventureStageCtrl._OnDecBtnClick = HL.Method() << function(self)
    self.m_curAdventureStage = self.m_curAdventureStage - 1
    self:_SetStageText(self.m_curAdventureStage)

    self.m_stageTempDisplayOverride = {}
    self:_RefreshAdventurePage(false)
end

AdventureStageCtrl._OnGetStageRewardBtnClick = HL.Method() << function(self)
    GameInstance.player.adventure:TakeAdventureBookStageReward(self.m_curAdventureStage)
end

AdventureStageCtrl._ResetAdventureStage = HL.Method() << function(self)
    local adventureBookData = GameInstance.player.adventure.adventureBookData
    self.m_curAdventureStage = adventureBookData.adventureBookStage
    self.m_adventureMaxStage = adventureBookData.adventureBookStage
    self:_SetStageText(self.m_curAdventureStage)
end

AdventureStageCtrl._InitData = HL.Method() << function(self)
    self.m_groupType2DomainInfo = {}
    self.m_slotInfosMap = {}
    for groupType, groupData in pairs(Tables.adventureTaskGroupTable) do
        if groupType ~= GEnums.AdventureTaskGroupType.Common and not string.isEmpty(groupData.domainId) then
            local hasDomain, domainData = Tables.domainDataTable:TryGetValue(groupData.domainId)
            if hasDomain then
                self.m_groupType2DomainInfo[groupType] = {
                    groupType = groupType,
                    domainId = groupData.domainId,
                    domainName = domainData.domainName,
                    domainIcon = domainData.domainIcon,
                    domainColor = UIUtils.getColorByString(domainData.blackBoxNumColor),
                    badgeIcon = groupData.badgeIcon,
                    sortId = domainData.sortId,
                }
            end
        end
    end
end

AdventureStageCtrl._InitDomainDropdown = HL.Method() << function(self)
    local adventure = GameInstance.player.adventure
    local curDomainId = Utils.getCurDomainId()
    local curDomainGroupType = GEnums.AdventureTaskGroupType.Common:GetHashCode()
    self.m_domainDropDownInfo = {}
    for groupType, domainInfo in pairs(self.m_groupType2DomainInfo) do
        if domainInfo.domainId == curDomainId then
            curDomainGroupType = groupType
        end
        if adventure:IsAdventureGroupUnlocked(groupType) then
            table.insert(self.m_domainDropDownInfo, domainInfo)
        end
    end
    table.sort(self.m_domainDropDownInfo, function(a, b)
        return a.sortId < b.sortId
    end)
    if self.m_selectedGroupType ~= GEnums.AdventureTaskGroupType.Common:GetHashCode() and adventure:IsAdventureGroupUnlocked(self.m_selectedGroupType) then

    elseif adventure:IsAdventureGroupUnlocked(curDomainGroupType) then
        self.m_selectedGroupType = curDomainGroupType
    elseif #self.m_domainDropDownInfo > 0 then
        self.m_selectedGroupType = self.m_domainDropDownInfo[1].groupType
    end
    self.m_canSwitchDomain = #self.m_domainDropDownInfo >= 2
    self.view.taskGroup.domainDropdownNode.gameObject:SetActiveIfNecessary(self.m_canSwitchDomain)
    if not self.m_canSwitchDomain then
        return
    end

    local selectedIndex = 1
    for index, info in ipairs(self.m_domainDropDownInfo) do
        if info.groupType == self.m_selectedGroupType then
            selectedIndex = index
            break
        end
    end

    local inited = false
    self.view.taskGroup.domainDropdown:Init(function(index, option, isSelected)
        local info = self.m_domainDropDownInfo[LuaIndex(index)]
        option:SetText(info.domainName)
        option.icon:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT, info.domainIcon)

        local optionTrans = option.notSelectedNode.transform
        local notSelectedIconTrans = optionTrans:Find("IconImg")
        if notSelectedIconTrans then
            local notSelectedIcon = notSelectedIconTrans:GetComponent(typeof(CS.Beyond.UI.UIImage))
            if notSelectedIcon then
                notSelectedIcon:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT, info.domainIcon)
            end
        end
    end, function(index)
        if inited then
            self:_OnDomainChanged(self.m_domainDropDownInfo[LuaIndex(index)].groupType)
        end
    end)
    self.view.taskGroup.domainDropdown:Refresh(#self.m_domainDropDownInfo, CSIndex(selectedIndex), false)

    local selectedInfo = self.m_domainDropDownInfo[selectedIndex]
    if selectedInfo then
        self.view.taskGroup.domainDropdown.captionIcon:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT, selectedInfo.domainIcon)
    end
    inited = true
    self:_RefreshDomainDropdownBg()
end

AdventureStageCtrl._RefreshDomainDropdownBg = HL.Method() << function(self)
    if not self.m_canSwitchDomain then
        return
    end
    local domainInfo = self.m_groupType2DomainInfo[self.m_selectedGroupType]
    if domainInfo ~= nil then
        self.view.taskGroup.domainDropdownBg.color = domainInfo.domainColor
    end
end

AdventureStageCtrl._OnDomainChanged = HL.Method(HL.Number) << function(self, groupType)
    if self.m_selectedGroupType == groupType then
        return
    end
    self.m_selectedGroupType = groupType
    self:_RefreshDomainDropdownBg()
    self.m_stageTempDisplayOverride = {}
    self:_RefreshAdventureStageTask(false)
end

AdventureStageCtrl._InitStageSlotInfos = HL.Method(HL.Number).Return(HL.Table) << function(self, stage)
    local stageRewardData = Tables.adventureBookStageRewardTable[stage]
    local stageSlotInfos = {
        slotInfosMap = {},
        taskInfos = {},
    }
    for _, taskId in pairs(stageRewardData.taskIds) do
        local _, taskData = Tables.adventureTaskTable:TryGetValue(taskId)
        if taskData ~= nil then
            local belongingGroup = taskData.belongingGroup:GetHashCode()
            local slotId = taskData.stageSlotId
            local slotInfo = stageSlotInfos.slotInfosMap[slotId]
            if slotInfo == nil then
                slotInfo = {
                    stageSlotId = slotId,
                    taskList = {},
                    taskMap = {},
                    displayInfo = {
                        stageSlotId = slotId,
                        redDotArg = {
                            stage = stage,
                            slotId = slotId,
                        },
                    },
                }
                stageSlotInfos.slotInfosMap[slotId] = slotInfo
                table.insert(stageSlotInfos.taskInfos, slotInfo.displayInfo)
            end
            local domainInfo = self.m_groupType2DomainInfo[belongingGroup]
            local taskInfo = {
                taskId = taskId,
                stageSlotId = slotId,
                belongingGroup = belongingGroup,
                domainId = domainInfo and domainInfo.domainId or "",
                domainName = domainInfo and domainInfo.domainName or "",
                badgeIcon = domainInfo and domainInfo.badgeIcon or "",
                domainSortId = domainInfo and domainInfo.sortId or math.huge,
                sortId = taskData.sortId,
                curProgress = 0,
                maxProgress = AdventureBookUtils.GetTaskMaxProgress(taskData),
                isComplete = false,
                isRewarded = false,
            }
            table.insert(slotInfo.taskList, taskInfo)
            slotInfo.taskMap[belongingGroup] = taskInfo
        end
    end
    for _, slotInfo in pairs(stageSlotInfos.slotInfosMap) do
        table.sort(slotInfo.taskList, Utils.genSortFunction({"domainSortId", "sortId", "taskId"}, true))
    end
    self.m_slotInfosMap[stage] = stageSlotInfos
    return stageSlotInfos
end

AdventureStageCtrl._RefreshStageSlotInfos = HL.Method(HL.Table) << function(self, stageSlotInfos)
    local adventure = GameInstance.player.adventure
    for _, slotInfo in pairs(stageSlotInfos.slotInfosMap) do
        for _, taskInfo in ipairs(slotInfo.taskList) do
            local taskData = Tables.adventureTaskTable[taskInfo.taskId]
            local curProgress = AdventureBookUtils.GetTaskCurrProgress(taskData)
            local isComplete = adventure:IsTaskComplete(taskInfo.taskId)
            if not isComplete then
                isComplete = curProgress >= taskInfo.maxProgress
            end
            taskInfo.curProgress = curProgress
            taskInfo.isComplete = isComplete
            taskInfo.isRewarded = adventure:IsTaskRewarded(taskInfo.taskId)
        end
    end
end

AdventureStageCtrl._BuildTempDisplayOverrideInfo = HL.Method(HL.Table).Return(HL.Table) << function(self, taskInfo)

    return {
        taskId = taskInfo.taskId,
        belongingGroup = taskInfo.belongingGroup,
        state = AdventureBookUtils.StageTaskDisplayState.Rewarded,
        domainId = taskInfo.domainId,
        badgeIcon = taskInfo.badgeIcon,
        showDomainBadge = self.m_canSwitchDomain and taskInfo.belongingGroup ~= GEnums.AdventureTaskGroupType.Common:GetHashCode()
            and not string.isEmpty(taskInfo.domainId),
        otherDomainName = nil,
        sortId = taskInfo.sortId,
        completeSortId = 1,
        rewardSortId = 1,
    }
end

AdventureStageCtrl._RefreshStageTaskDisplayInfos = HL.Method(HL.Table) << function(self, stageSlotInfos)
    for stageSlotId, slotInfo in pairs(stageSlotInfos.slotInfosMap) do
        local displayTask = slotInfo.taskMap[self.m_selectedGroupType] or slotInfo.taskList[1]
        local selectedRewardedTask = nil
        local rewardedTask = nil
        local selectedCompleteTask = nil
        local completeTask = nil
        local rewardedCount = 0
        for _, taskInfo in ipairs(slotInfo.taskList) do
            if taskInfo.isRewarded then
                rewardedCount = rewardedCount + 1
                rewardedTask = rewardedTask or taskInfo
                if taskInfo.belongingGroup == self.m_selectedGroupType then
                    selectedRewardedTask = taskInfo
                end
            elseif taskInfo.isComplete then
                completeTask = completeTask or taskInfo
                if taskInfo.belongingGroup == self.m_selectedGroupType then
                    selectedCompleteTask = taskInfo
                end
            end
        end
        rewardedTask = selectedRewardedTask or rewardedTask
        completeTask = selectedCompleteTask or completeTask

        local state = AdventureBookUtils.StageTaskDisplayState.InProgress
        local otherDomainName = nil
        if rewardedTask ~= nil then
            if rewardedCount > 1 then
                logger.error("[AdventureBook] duplicated rewarded slot, slotId = " .. stageSlotId)
            end
            if not self.m_canSwitchDomain or
                rewardedTask.belongingGroup == GEnums.AdventureTaskGroupType.Common:GetHashCode() or
                rewardedTask.belongingGroup == self.m_selectedGroupType
            then
                displayTask = rewardedTask
                state = AdventureBookUtils.StageTaskDisplayState.Rewarded
            else
                state = AdventureBookUtils.StageTaskDisplayState.OtherDomainRewarded
                otherDomainName = rewardedTask.domainName
            end
        elseif completeTask ~= nil then
            displayTask = completeTask
            state = AdventureBookUtils.StageTaskDisplayState.Complete
        end

        if displayTask ~= nil then
            local displayInfo = slotInfo.displayInfo
            displayInfo.taskId = displayTask.taskId
            displayInfo.stageSlotId = stageSlotId
            displayInfo.belongingGroup = displayTask.belongingGroup
            displayInfo.state = state
            displayInfo.domainId = displayTask.domainId
            displayInfo.badgeIcon = displayTask.badgeIcon
            displayInfo.showDomainBadge = self.m_canSwitchDomain and displayTask.belongingGroup ~= GEnums.AdventureTaskGroupType.Common
                and not string.isEmpty(displayTask.domainId)
            displayInfo.otherDomainName = otherDomainName
            displayInfo.sortId = displayTask.sortId
            displayInfo.completeSortId = state == AdventureBookUtils.StageTaskDisplayState.Complete and 0 or 1
            displayInfo.rewardSortId = (state == AdventureBookUtils.StageTaskDisplayState.Rewarded or
                state == AdventureBookUtils.StageTaskDisplayState.OtherDomainRewarded) and 1 or 0


            local tempOverrideInfo = self.m_stageTempDisplayOverride[stageSlotId]
            if tempOverrideInfo ~= nil then
                displayInfo.taskId = tempOverrideInfo.taskId
                displayInfo.belongingGroup = tempOverrideInfo.belongingGroup
                displayInfo.state = tempOverrideInfo.state
                displayInfo.domainId = tempOverrideInfo.domainId
                displayInfo.badgeIcon = tempOverrideInfo.badgeIcon
                displayInfo.showDomainBadge = tempOverrideInfo.showDomainBadge
                displayInfo.otherDomainName = tempOverrideInfo.otherDomainName
                displayInfo.sortId = tempOverrideInfo.sortId
                displayInfo.completeSortId = tempOverrideInfo.completeSortId
                displayInfo.rewardSortId = tempOverrideInfo.rewardSortId
            end
        end
    end
    table.sort(stageSlotInfos.taskInfos, Utils.genSortFunction({"rewardSortId", "completeSortId", "sortId", "stageSlotId"}, true))
end

AdventureStageCtrl._BuildStageTaskInfos = HL.Method(HL.Number).Return(HL.Table) << function(self, stage)
    local stageSlotInfos = self.m_slotInfosMap[stage]
    if stageSlotInfos == nil then
        stageSlotInfos = self:_InitStageSlotInfos(stage)
    end
    self:_RefreshStageSlotInfos(stageSlotInfos)
    self:_RefreshStageTaskDisplayInfos(stageSlotInfos)
    return stageSlotInfos.taskInfos
end

AdventureStageCtrl._RefreshAdventurePage = HL.Method(HL.Boolean) << function(self, isInit)
    self:_RefreshBtnState()
    self:_RefreshAdventureStageOverview()
    self:_RefreshAdventureStageTask(isInit)
end

AdventureStageCtrl._RefreshBtnState = HL.Method() << function(self)
    local adventure = self.view
    adventure.incBtn.gameObject:SetActive(self.m_curAdventureStage < self.m_adventureMaxStage)
    adventure.decBtn.gameObject:SetActive(self.m_curAdventureStage > 1)
end

AdventureStageCtrl._RefreshAdventureStageOverview = HL.Method(HL.Opt(HL.Boolean)) << function(self, isTaskChanged)
    local rewardId = Tables.adventureBookStageRewardTable[self.m_curAdventureStage].rewardId
    local rewardData = Tables.rewardTable[rewardId]
    local adventureBookData = GameInstance.player.adventure.adventureBookData
    local isActualStage = self.m_curAdventureStage == adventureBookData.actualBookStage
    local isComplete = adventureBookData.isCurAdventureBookStateComplete

    local rewards = {}
    for _, itemBundle in pairs(rewardData.itemBundles) do
        local cfg = Utils.tryGetTableCfg(Tables.itemTable, itemBundle.id)
        if cfg then
            table.insert(rewards, {
                id = itemBundle.id,
                count = itemBundle.count,
                forceHidePotentialStar = true,

                rarity = -cfg.rarity,
                sortId1 = cfg.sortId1,
                sortId2 = cfg.sortId2,
            })
        end
    end

    table.sort(rewards, Utils.genSortFunction({"rarity", "sortId1", "sortId2", "id"}, true))

    self.m_adventureStageRewardCellCache:Refresh(#rewards, function(cell, luaIndex)
        cell.view.rewardedCover.gameObject:SetActiveIfNecessary(not isActualStage)
        cell:InitItem(rewards[luaIndex], function()
            UIUtils.showItemSideTips(cell)
        end)
        cell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
    end)

    local adventure = self.view
    adventure.stageComplete.gameObject:SetActiveIfNecessary(isActualStage and isComplete)
    adventure.stageCompleteBg.gameObject:SetActiveIfNecessary(isActualStage and isComplete)
    adventure.stageRewarded.gameObject:SetActiveIfNecessary(not isActualStage)


    if not isActualStage then

        adventure.overviewState:SetState("Received")
        adventure.ani:SampleToInAnimationBegin()
    else
        if isComplete then

            adventure.overviewState:SetState("Receive")
            if isTaskChanged then
                adventure.ani:Play("adventurestagepanel_receive")
            end
        else

            adventure.overviewState:SetState("NotReceived")
            adventure.ani:SampleToInAnimationBegin()
        end
    end
end

AdventureStageCtrl._RefreshAdventureStageTask = HL.Method(HL.Boolean) << function(self, isInit)
    self.m_curAdventureStageTaskInfos = self:_BuildStageTaskInfos(self.m_curAdventureStage)

    for luaIndex, cell in pairs(self.m_taskCellList) do
        local info = self.m_curAdventureStageTaskInfos[luaIndex]
        if isInit then
            cell:InitAdventureTaskCell(info)
        else
            cell:InitAdventureTaskCell(info, self.view.config.TASK_CELL_SHOW_DELAY_TIME * CSIndex(luaIndex))
        end
    end
end

AdventureStageCtrl.OnAdventureTaskModify = HL.Method(HL.Any) << function(self, args)
    local rewardedTaskIds = unpack(args)
    local rewardedIds = {}
    for _, rewardedTaskId in pairs(rewardedTaskIds) do
        local taskData = Tables.AdventureTaskTable[rewardedTaskId]
        if taskData ~= nil and taskData.taskType == GEnums.AdventureTaskType.AdventureBook then
            table.insert(rewardedIds, taskData.rewardId)

            if taskData.adventureBookStage == self.m_curAdventureStage then
                local belongingGroup = taskData.belongingGroup:GetHashCode()
                if belongingGroup ~= self.m_selectedGroupType then
                    local stageSlotInfos = self.m_slotInfosMap[self.m_curAdventureStage]
                    if stageSlotInfos ~= nil then
                        local slotInfo = stageSlotInfos.slotInfosMap[taskData.stageSlotId]
                        if slotInfo ~= nil and slotInfo.taskMap[belongingGroup] ~= nil then
                            if self.m_stageTempDisplayOverride[taskData.stageSlotId] then
                                local errorMsg = string.format("冒险阶段同时完成了同一slot不同地区的任务！！slot id[%s]，任务id1[%s]，任务id2[%s]",
                                    taskData.stageSlotId,
                                    self.m_stageTempDisplayOverride[taskData.stageSlotId].taskId,
                                    rewardedTaskId
                                )
                                logger.error(errorMsg)
                            else
                                self.m_stageTempDisplayOverride[taskData.stageSlotId] =
                                self:_BuildTempDisplayOverrideInfo(slotInfo.taskMap[belongingGroup])
                            end
                        end
                    end
                end
            end
        end
    end
    self:_ShowRewardPopup(Language.LUA_ADVENTURE_BOOK_TASK_REWARD_TITLE_DESC, rewardedIds)

    self:_RefreshAdventureStageOverview(true)
    self:_RefreshAdventureStageTask(false)
end

AdventureStageCtrl.OnAdventureBookStageModify = HL.Method(HL.Any) << function(self, args)
    local preBookStage = unpack(args)
    local bookStageData = Tables.adventureBookStageRewardTable[preBookStage]
    self:_ShowRewardPopup(Language.LUA_ADVENTURE_BOOK_STAGE_REWARD_TITLE_DESC, {bookStageData.rewardId})

    local adventureBookData = GameInstance.player.adventure.adventureBookData
    self.m_curAdventureStage = adventureBookData.adventureBookStage
    self.m_adventureMaxStage = adventureBookData.adventureBookStage
    self:_SetStageText(self.m_curAdventureStage)
    self.m_stageTempDisplayOverride = {}
    self:_RefreshAdventurePage(false)
end

AdventureStageCtrl.OnAdventureTabChangedSame = HL.Method(HL.Number) << function(self, panelId)
    if panelId == PANEL_ID then
        self.view.taskGroup.selectableNaviGroup:NaviToThisGroup()
    end
end

AdventureStageCtrl._ShowRewardPopup = HL.Method(HL.String, HL.Table) << function(self, title, rewardedIds)
    if #rewardedIds < 1 then
        return
    end


    local rewardData = Tables.RewardTable[rewardedIds[1]]
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        title = title,
        items = rewardData.itemBundles,
    })
end

AdventureStageCtrl._SetStageText = HL.Method(HL.Number) << function(self, number)
    if number < 10 then
        self.view.stageTxtLeft.text = "0"
        self.view.stageTxtLeftBg.text = "0"
        self.view.stageTxtRight.text = number
        self.view.stageTxtRightBg.text = number
    else
        local left = number // 10
        local right = number % 10
        self.view.stageTxtLeft.text = left
        self.view.stageTxtLeftBg.text = left
        self.view.stageTxtRight.text = right
        self.view.stageTxtRightBg.text = right
    end
end

HL.Commit(AdventureStageCtrl)
