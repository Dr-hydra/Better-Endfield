local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WorldLevelUp


local CONDITION_INDEX_TEXT_KEYS = {
    "LUA_WORLD_LEVEL_UP_CONDITION_INDEX_1",
    "LUA_WORLD_LEVEL_UP_CONDITION_INDEX_2",
}



local MAX_FIT_HEIGHT = 689


local MISSION_TYPE = CS.Beyond.Gameplay.MissionSystem.MissionType
local MISSION_STATE = CS.Beyond.Gameplay.MissionSystem.MissionState

WorldLevelUpCtrl = HL.Class('WorldLevelUpCtrl', uiCtrl.UICtrl)

WorldLevelUpCtrl.m_targetWorldLevel = HL.Field(HL.Number) << 0
WorldLevelUpCtrl.m_isExpanded = HL.Field(HL.Boolean) << false
WorldLevelUpCtrl.m_groupCache = HL.Field(HL.Forward("UIListCache"))
WorldLevelUpCtrl.m_requiredConditionInfo = HL.Field(HL.Table)
WorldLevelUpCtrl.m_optionalSchemeInfos = HL.Field(HL.Table)
WorldLevelUpCtrl.m_defaultNaviCell = HL.Field(HL.Userdata)
WorldLevelUpCtrl.m_missionSystem = HL.Field(HL.Userdata)







WorldLevelUpCtrl.s_messages = HL.StaticField(HL.Table) << {

}


WorldLevelUpCtrl._GetSingleChapterBitNumber = HL.Method(HL.Number).Return(HL.Number) << function(self, bitmask)
    if bitmask <= 0 then
        return 0
    end
    local bitPosition = 0
    local n = bitmask
    while n > 0 and n % 2 == 0 do
        n = n / 2
        bitPosition = bitPosition + 1
    end
    if n == 1 then
        return bitPosition + 1
    end
    return 0
end


WorldLevelUpCtrl._ResolveMissionPhaseSelectChapter = HL.Method(HL.String).Return(HL.Opt(HL.String)) << function(self, missionId)
    if self.m_missionSystem == nil or string.isEmpty(missionId) then
        return nil
    end
    local missionInfo = self.m_missionSystem:GetMissionInfo(missionId)
    if missionInfo == nil then
        return nil
    end
    local missionChapter = missionInfo.missionChapterBitmask
    if missionChapter == nil then
        return nil
    end
    if self:_GetSingleChapterBitNumber(missionChapter:ToInt()) <= 0 then
        return nil
    end
    local success, data = Tables.missionChapterSelectTable:TryGetValue(missionChapter)
    if success then
        return data.selectChapter
    end
    return nil
end

WorldLevelUpCtrl._HasChapterBitmaskIntersect = HL.Method(HL.Number, HL.Number).Return(HL.Boolean) << function(self, a, b)
    if a <= 0 or b <= 0 then
        return false
    end
    local v = 1
    while v <= a and v <= b do
        if math.floor(a / v) % 2 == 1 and math.floor(b / v) % 2 == 1 then
            return true
        end
        v = v * 2
    end
    return false
end

WorldLevelUpCtrl._GetMissionDisplayName = HL.Method(HL.String).Return(HL.String) << function(self, missionId)

    if string.isEmpty(missionId) then
        return ""
    end

    local missionInfo = self.m_missionSystem:GetMissionInfo(missionId)
    if missionInfo and missionInfo.missionName then
        return missionInfo.missionName:GetText()
    end
    return missionId
end

WorldLevelUpCtrl._GetMissionMainProgressText = HL.Method(HL.Any).Return(HL.String) << function(self, missionId)
    if string.isEmpty(missionId) then
        return ""
    end

    local missionSystem = self.m_missionSystem
    local missionName = self:_GetMissionDisplayName(missionId)
    local chapterId = missionSystem:GetChapterIdByMissionId(missionId)
    local chapterInfo = missionSystem:GetChapterInfo(chapterId)

    if chapterInfo == nil and string.isEmpty(missionName) then
        return ""
    end

    local progressParts = {}
    if chapterInfo and chapterInfo.chapterNum then
        local text = chapterInfo.chapterNum:GetText()
        if not string.isEmpty(text) then
            table.insert(progressParts, text)
        end
    end
    if chapterInfo and chapterInfo.episodeNum then
        local text = chapterInfo.episodeNum:GetText()
        if not string.isEmpty(text) then
            table.insert(progressParts, text)
        end
    end
    if not string.isEmpty(missionName) then
        table.insert(progressParts, missionName)
    end
    return table.concat(progressParts, " - ")
end

WorldLevelUpCtrl._BuildMissionChapterResolutionHints = HL.Method(HL.Table).Return(HL.Table) << function(self, missionIds)



    local maskSet = {}
    local majorChapterNumKeys = {}
    local missionSystem = self.m_missionSystem
    for _, missionId in ipairs(missionIds) do
        if not string.isEmpty(missionId) then
            local missionInfo = missionSystem:GetMissionInfo(missionId)
            local mask = missionInfo and missionInfo.missionChapterBitmask and missionInfo.missionChapterBitmask:ToInt() or 0
            if mask > 0 then
                maskSet[mask] = true
            else
                local chapterId = missionSystem:GetChapterIdByMissionId(missionId)
                local chapterInfo = chapterId ~= nil and missionSystem:GetChapterInfo(chapterId) or nil
                if chapterInfo ~= nil and chapterInfo.chapterNum ~= nil then
                    local key = chapterInfo.chapterNum:GetText()
                    if not string.isEmpty(key) then
                        majorChapterNumKeys[key] = true
                    end
                end
            end
        end
    end
    return {
        maskSet = maskSet,
        majorChapterNumKeys = majorChapterNumKeys,
    }
end

WorldLevelUpCtrl._ResolveCurrentMainMissionId = HL.Method(HL.Table, HL.Table).Return(HL.String) << function(self, chapterMaskSet, majorChapterNumKeys)
    majorChapterNumKeys = majorChapterNumKeys or {}








    local missionSystem = self.m_missionSystem
    local trackMissionId = missionSystem.trackMissionId or ""

    local function _IsBetter(idA, idB)
        local trackA = idA == trackMissionId
        local trackB = idB == trackMissionId
        if trackA ~= trackB then return trackA end

        local procA = missionSystem:GetMissionState(idA) == MISSION_STATE.Processing
        local procB = missionSystem:GetMissionState(idB) == MISSION_STATE.Processing
        if procA ~= procB then return procA end

        local chapterIdA = missionSystem:GetChapterIdByMissionId(idA)
        local chapterIdB = missionSystem:GetChapterIdByMissionId(idB)
        local chapterInfoA = chapterIdA ~= nil and missionSystem:GetChapterInfo(chapterIdA) or nil
        local chapterInfoB = chapterIdB ~= nil and missionSystem:GetChapterInfo(chapterIdB) or nil
        local prioA = chapterInfoA and chapterInfoA.priority or math.maxinteger
        local prioB = chapterInfoB and chapterInfoB.priority or math.maxinteger
        if prioA ~= prioB then return prioA < prioB end

        return idA < idB
    end

    local function _PickBestMain(filter)
        local bestId = ""
        for missionId, _ in pairs(missionSystem.missions) do
            local missionInfo = missionSystem:GetMissionInfo(missionId)
            if missionInfo ~= nil and missionInfo.missionType == MISSION_TYPE.Main and filter(missionId, missionInfo) then
                if string.isEmpty(bestId) or _IsBetter(missionId, bestId) then
                    bestId = missionId
                end
            end
        end
        return bestId
    end

    if next(chapterMaskSet) ~= nil then
        local result = _PickBestMain(function(_, missionInfo)
            local mask = missionInfo.missionChapterBitmask and missionInfo.missionChapterBitmask:ToInt() or 0
            for targetMask, _ in pairs(chapterMaskSet) do
                if self:_HasChapterBitmaskIntersect(mask, targetMask) then
                    return true
                end
            end
            return false
        end)
        if not string.isEmpty(result) then
            return result
        end
    end

    if next(majorChapterNumKeys) ~= nil then
        local result = _PickBestMain(function(missionId)
            local chapterId = missionSystem:GetChapterIdByMissionId(missionId)
            local chapterInfo = chapterId ~= nil and missionSystem:GetChapterInfo(chapterId) or nil
            if chapterInfo == nil or chapterInfo.chapterNum == nil then
                return false
            end
            local key = chapterInfo.chapterNum:GetText()
            if string.isEmpty(key) then
                return false
            end
            return majorChapterNumKeys[key] == true
        end)
        if not string.isEmpty(result) then
            return result
        end
    end

    return _PickBestMain(function()
        return true
    end)
end

WorldLevelUpCtrl._IsSchemeInCurrentChapter = HL.Method(HL.Table).Return(HL.Boolean) << function(self, taskInfos)
    local currentChapterId = Utils.getCurrentChapterId()
    if currentChapterId == nil or currentChapterId <= 0 then
        return false
    end
    local success, currentChapterData = Tables.chapterMissionChapterTable:TryGetValue(currentChapterId)
    if not success or currentChapterData == nil or currentChapterData.missionChapter == nil then
        return false
    end
    local currentMissionChapter = currentChapterData.missionChapter:ToInt()

    local missionSystem = self.m_missionSystem
    for _, taskInfo in ipairs(taskInfos) do
        if taskInfo ~= nil and not string.isEmpty(taskInfo.missionId) then
            local missionInfo = missionSystem:GetMissionInfo(taskInfo.missionId)
            local missionChapterBitmask = missionInfo ~= nil and missionInfo.missionChapterBitmask and missionInfo.missionChapterBitmask:ToInt() or 0
            if self:_HasChapterBitmaskIntersect(missionChapterBitmask, currentMissionChapter) then
                return true
            end
        end
    end
    return false
end

WorldLevelUpCtrl._BuildGroupSummaryText = HL.Method(HL.String, HL.Number, HL.Boolean).Return(HL.String) << function(self, baseText, targetWorldLevel, needSuffix)
    if needSuffix then
        return string.format(Language.LUA_WORLD_LEVEL_UP_GROUP_WITH_TARGET_LEVEL, baseText, targetWorldLevel)
    end
    return baseText
end

WorldLevelUpCtrl._BuildMissionConditionTexts = HL.Method(HL.Table, HL.Table).Return(HL.String, HL.String) << function(self, missionNames, missionIds)

    local missionText = table.concat(missionNames, Language.LUA_WORLD_LEVEL_UP_MISSION_NAME_CONJUNCTION)
    local titleText = string.format(Language.LUA_WORLD_LEVEL_UP_LEVEL1_MISSION_TITLE, missionText)


    local hints = self:_BuildMissionChapterResolutionHints(missionIds)
    local currentMissionId = self:_ResolveCurrentMainMissionId(hints.maskSet, hints.majorChapterNumKeys)
    local progressText = self:_GetMissionMainProgressText(currentMissionId)
    local contentText = string.isEmpty(progressText) and "" or string.format(Language.LUA_WORLD_LEVEL_UP_LEVEL1_MISSION_DESC, progressText)
    return titleText, contentText
end

WorldLevelUpCtrl._GetConditionTitleText = HL.Method(HL.Number, HL.Number).Return(HL.String) << function(self, index, totalCount)
    if totalCount <= 1 then
        return Language.LUA_WORLD_LEVEL_UP_CONDITION_TITLE
    end

    local indexTextKey = CONDITION_INDEX_TEXT_KEYS[index]
    if indexTextKey == nil then
        logger.error("WorldLevelUpCtrl._GetConditionTitleText: unsupported condition index " .. tostring(index))
        return Language.LUA_WORLD_LEVEL_UP_CONDITION_TITLE
    end
    return string.format(Language.LUA_WORLD_LEVEL_UP_CONDITION_TITLE_FORMAT, Language[indexTextKey])
end

WorldLevelUpCtrl._CanShowExpend = HL.Method(HL.Table).Return(HL.Boolean) << function(self, optionalSchemeInfos)
    if #optionalSchemeInfos <= 1 then
        return false
    end
    local missionSystem = self.m_missionSystem
    return (missionSystem.earlyAcceptChapterMask:GetHashCode() or 0) > 0
end


WorldLevelUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    arg = arg or {}
    self.m_isExpanded = false
    self.m_requiredConditionInfo = nil
    self.m_optionalSchemeInfos = {}
    self.m_groupCache = nil
    self.m_defaultNaviCell = nil
    self.m_missionSystem = GameInstance.player.mission

    self.m_groupCache = UIUtils.genCellCache(self.view.upgradeWaysNode.upgradeGroupNode)
    self.view.btnClose.onClick:RemoveAllListeners()
    self.view.btnClose.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            self.m_phase:BackToTips()
        end)
    end)

    self.m_targetWorldLevel = self:_ResolveTargetWorldLevel(arg)
    local success, targetCfg = Tables.adventureWorldLevelTable:TryGetValue(self.m_targetWorldLevel)
    if not success or targetCfg == nil then
        logger.error("WorldLevelUpCtrl.OnCreate: invalid target world level " .. tostring(self.m_targetWorldLevel))
        self:PlayAnimationOutAndClose()
        return
    end

    local currentWorldLevel = GameInstance.player.adventure.currentWorldLevel
    local currentAdventureLevel = GameInstance.player.adventure.adventureLevelData.lv


    self:_RefreshLeftInfo(currentWorldLevel, targetCfg)
    self.m_requiredConditionInfo = self:_BuildRequiredConditionInfo(targetCfg, currentAdventureLevel)
    self.m_optionalSchemeInfos = self:_BuildOptionalSchemeInfos(targetCfg)

    self.view.btnExpend.onClick:RemoveAllListeners()
    self.view.btnExpend.onClick:AddListener(function()
        self:_ToggleExpend()
    end)

    self:_RefreshExpendState()
    self:_RefreshConditionList()
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end


WorldLevelUpCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    return {
        targetWorldLevel = self.m_targetWorldLevel,
        isExpanded = self.m_isExpanded,
    }
end

WorldLevelUpCtrl._ResolveTargetWorldLevel = HL.Method(HL.Any).Return(HL.Number) << function(self, arg)

    local currentMaxWorldLevel = GameInstance.player.adventure.currentMaxWorldLevel

    local maxConfigWorldLevel = math.max(1, currentMaxWorldLevel)
    while true do
        local success = Tables.adventureWorldLevelTable:TryGetValue(maxConfigWorldLevel + 1)
        if not success then
            break
        end
        maxConfigWorldLevel = maxConfigWorldLevel + 1
    end
    local defaultTargetWorldLevel = math.min(currentMaxWorldLevel + 1, maxConfigWorldLevel)
    local targetWorldLevel
    if type(arg) == "table" then
        targetWorldLevel = tonumber(arg.targetWorldLevel)
    else
        targetWorldLevel = tonumber(arg)
    end
    targetWorldLevel = targetWorldLevel or defaultTargetWorldLevel
    return math.max(1, math.min(targetWorldLevel, maxConfigWorldLevel))
end

WorldLevelUpCtrl._RefreshLeftInfo = HL.Method(HL.Number, HL.Userdata) << function(self, currentWorldLevel, targetCfg)

    self.view.levelBeforeTxt.text = string.format("%02d", currentWorldLevel)
    self.view.levelAfterTxt.text = string.format("%02d", targetCfg.level)
    self.view.upgradewaysTxt.text = self:_GetMissionDisplayName(targetCfg.missionId)
    self.view.upgradeTxt.text = string.isEmpty(targetCfg.missionId)
        and Language.LUA_WORLD_LEVEL_UP_LEFT_UPGRADE_TEXT
        or Language.LUA_WORLD_LEVEL_UP_LEFT_MISSION_TEXT
end

WorldLevelUpCtrl._BuildRequiredConditionInfo = HL.Method(HL.Userdata, HL.Number).Return(HL.Table) << function(self, targetCfg, currentAdventureLevel)

    if not targetCfg.needAdventureLv or targetCfg.needAdventureLv <= 0 then
        return nil
    end

    local isComplete = currentAdventureLevel >= targetCfg.needAdventureLv
    return {
        conditionText = string.format(Language.LUA_WORLD_LEVEL_UP_ADVENTURE_LEVEL_TITLE, targetCfg.needAdventureLv),
        progressText = string.format(Language.LUA_WORLD_LEVEL_UP_ADVENTURE_LEVEL_PROGRESS, currentAdventureLevel),
        isComplete = isComplete,
        onClick = isComplete and nil or function()
            PhaseManager:ExitPhaseFast(PhaseId.WorldLevelPopup)
            PhaseManager:GoToPhase(PhaseId.AdventureBook, {
                panelId = "AdventureStage",
            })
        end,
    }
end

WorldLevelUpCtrl._BuildOptionalSchemeInfos = HL.Method(HL.Userdata).Return(HL.Table) << function(self, targetCfg)

    local optionalSchemeInfos = {}
    if string.isEmpty(targetCfg.taskUnlockGroupId) then
        return optionalSchemeInfos
    end

    local success, groupData = Tables.adventureWorldLevelUnlockTaskGroupTable:TryGetValue(targetCfg.taskUnlockGroupId)
    if not success or groupData == nil or groupData.unlockTaskInfos == nil then
        return optionalSchemeInfos
    end

    local missionSystem = self.m_missionSystem
    local groupedInfos = {}
    local taskInfos = groupData.unlockTaskInfos
    for luaIndex = 1, taskInfos.Count do
        local taskInfo = taskInfos[CSIndex(luaIndex)]
        if taskInfo == nil then
            logger.error("WorldLevelUpCtrl._BuildOptionalSchemeInfos: unlockTaskInfos item is nil, groupId = " .. tostring(targetCfg.taskUnlockGroupId) .. ", index = " .. tostring(luaIndex))
        else
            local optionId = taskInfo.optionId
            if groupedInfos[optionId] == nil then
                groupedInfos[optionId] = {
                    optionId = optionId,
                    taskInfos = {},
                }
            end
            table.insert(groupedInfos[optionId].taskInfos, taskInfo)
        end
    end

    for _, schemeInfo in pairs(groupedInfos) do


        local missionTexts = {}
        local missionIds = {}
        local isComplete = #schemeInfo.taskInfos > 0
        local tagSpriteName = nil
        local tagOk = true
        for _, taskInfo in ipairs(schemeInfo.taskInfos) do
            local missionId = taskInfo.missionId
            if not string.isEmpty(missionId) then
                table.insert(missionIds, missionId)
            end

            local missionText = self:_GetMissionMainProgressText(missionId)
            if not string.isEmpty(missionText) then
                table.insert(missionTexts, missionText)
            end

            if isComplete then
                local currentState = missionSystem:GetMissionState(missionId)
                if currentState:ToInt() ~= taskInfo.missionState then
                    isComplete = false
                end
            end

            if tagOk then
                local taskTag = nil
                local missionInfo = missionSystem:GetMissionInfo(missionId)
                if missionInfo ~= nil and missionInfo.missionType == MISSION_TYPE.Main then


                    local bitmask = missionInfo.missionChapterBitmask and missionInfo.missionChapterBitmask:ToInt() or 0
                    local chapterNumber = self:_GetSingleChapterBitNumber(bitmask)
                    if chapterNumber > 0 then
                        taskTag = string.format(UIConst.WORLD_LEVEL_CONDITION_TAG_ICON_FORMAT, chapterNumber)
                    end
                end

                if string.isEmpty(taskTag) or (tagSpriteName ~= nil and tagSpriteName ~= taskTag) then
                    tagSpriteName = nil
                    tagOk = false
                else
                    tagSpriteName = taskTag
                end
            end
        end

        local conditionText = table.concat(missionTexts, "\n")
        if not string.isEmpty(conditionText) then
            local displayTitleText, displayContentText = self:_BuildMissionConditionTexts(missionTexts, missionIds)
            table.insert(optionalSchemeInfos, {
                optionId = schemeInfo.optionId,
                conditionText = conditionText,
                displayTitleText = displayTitleText,
                displayContentText = displayContentText,
                progressText = isComplete and "1/1" or "0/1",
                isComplete = isComplete,
                isCurrentChapter = self:_IsSchemeInCurrentChapter(schemeInfo.taskInfos),
                tagSpriteName = tagSpriteName,
                onClick = isComplete and nil or function()
                    local firstTaskInfo = schemeInfo.taskInfos[1]
                    if firstTaskInfo == nil or string.isEmpty(firstTaskInfo.missionId) then
                        return
                    end


                    PhaseManager:ExitPhaseFast(PhaseId.WorldLevelPopup)
                    PhaseManager:OpenPhase(PhaseId.Mission, {
                        selectChapter = self:_ResolveMissionPhaseSelectChapter(firstTaskInfo.missionId),
                        useBlackMask = true,
                    })
                end,
            })
        end
    end

    table.sort(optionalSchemeInfos, function(a, b)
        if a.isComplete ~= b.isComplete then
            return a.isComplete
        end
        if a.isCurrentChapter ~= b.isCurrentChapter then
            return a.isCurrentChapter
        end
        return a.optionId > b.optionId
    end)

    return optionalSchemeInfos
end

WorldLevelUpCtrl._ToggleExpend = HL.Method() << function(self)

    if not self:_CanShowExpend(self.m_optionalSchemeInfos) then
        return
    end
    self.m_isExpanded = not self.m_isExpanded
    self:_RefreshExpendState()
    self:_RefreshConditionList()
    Notify(MessageConst.REFRESH_CONTROLLER_HINT)
end

WorldLevelUpCtrl._RefreshExpendState = HL.Method() << function(self)

    local showExpend = self:_CanShowExpend(self.m_optionalSchemeInfos)
    if not showExpend then
        self.m_isExpanded = false
    end
    self.view.btnExpend.gameObject:SetActiveIfNecessary(showExpend)
    self.view.otherWays.gameObject:SetActiveIfNecessary(showExpend)
    if showExpend then
        self.view.otherWays:SetState(self.m_isExpanded and "CollapseState" or "ExpendState")
    end
    self.view.stateText.gameObject:SetActiveIfNecessary(showExpend)
end

WorldLevelUpCtrl._RefreshConditionList = HL.Method() << function(self)
    local groupInfos = {}
    local optionalCount = #self.m_optionalSchemeInfos
    local hasRequired = self.m_requiredConditionInfo ~= nil

    if hasRequired then
        table.insert(groupInfos, {
            summaryText = self:_BuildGroupSummaryText(
                Language.LUA_WORLD_LEVEL_UP_REQUIRED_CONDITION,
                self.m_targetWorldLevel,
                optionalCount <= 0
            ),
            progressText = self.m_requiredConditionInfo.isComplete and "1/1" or "0/1",
            isComplete = self.m_requiredConditionInfo.isComplete,
            ways = {
                {
                    displayTitleText = self.m_requiredConditionInfo.conditionText,
                    displayContentText = self.m_requiredConditionInfo.progressText,
                    progressText = self.m_requiredConditionInfo.isComplete and "1/1" or "0/1",
                    isComplete = self.m_requiredConditionInfo.isComplete,
                    onClick = self.m_requiredConditionInfo.onClick,
                },
            },
        })
    end

    if optionalCount > 0 then
        local anyComplete = false
        for _, info in ipairs(self.m_optionalSchemeInfos) do
            if info.isComplete then
                anyComplete = true
                break
            end
        end
        local optionalWays = {}
        table.insert(optionalWays, self.m_optionalSchemeInfos[1])
        if self.m_isExpanded then
            for index = 2, optionalCount do
                table.insert(optionalWays, self.m_optionalSchemeInfos[index])
            end
        end
        table.insert(groupInfos, {
            summaryText = self:_BuildGroupSummaryText(
                Language.LUA_WORLD_LEVEL_UP_ANY_CONDITION,
                self.m_targetWorldLevel,
                not hasRequired
            ),
            progressText = anyComplete and "1/1" or "0/1",
            isComplete = anyComplete,
            ways = optionalWays,
        })
    end

    self:_RefreshConditionGroups(groupInfos)
end

WorldLevelUpCtrl._RefreshConditionGroups = HL.Method(HL.Table) << function(self, groupInfos)
    if #groupInfos == 0 then
        logger.error("WorldLevelUpCtrl._RefreshConditionList: no condition data for target world level " .. tostring(self.m_targetWorldLevel))
        return
    end
    for index, info in ipairs(groupInfos) do
        info.titleText = self:_GetConditionTitleText(index, #groupInfos)
    end

    self.m_defaultNaviCell = nil
    self.m_groupCache:Refresh(#groupInfos, function(group, index)
        local info = groupInfos[index]
        self:_RefreshGroupTitle(group.upgradeTitle, info)
        group.wayCellCache = group.wayCellCache or UIUtils.genCellCache(group.upgradeWaysCell)
        group.wayCellCache:Refresh(#info.ways, function(cell, wayIndex)
            self:_RefreshWayCell(cell, info.ways[wayIndex])
        end)
    end)

    if self.m_defaultNaviCell then
        self:SetNaviTarget(self.m_defaultNaviCell)
    end


    self:_UpdateScrollLayout()
end

WorldLevelUpCtrl._UpdateScrollLayout = HL.Method() << function(self)


    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.scrollContent)
    local contentHeight = self.view.scrollContent.rect.height
    local canScroll = contentHeight > MAX_FIT_HEIGHT
    self.view.scrollRectLayoutElement.preferredHeight = canScroll and MAX_FIT_HEIGHT or contentHeight
    self.view.scrollRect.enabled = canScroll
    if canScroll then

        self.view.scrollRect:ScrollTo(Vector2(0, 1), true)
    end

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.scrollRect.transform.parent)


    self.view.scrollbarVertical.gameObject:SetActiveIfNecessary(canScroll)
end

WorldLevelUpCtrl._RefreshGroupTitle = HL.Method(HL.Any, HL.Table) << function(self, cell, info)
    cell.upgradeTitleTxt.text = info.titleText or Language.LUA_WORLD_LEVEL_UP_CONDITION_TITLE
    cell.conditionTxt.text = info.summaryText or ""
    cell.conditionNumTxt.text = info.progressText or ""
    cell.processingNode.gameObject:SetActiveIfNecessary(not info.isComplete)
    cell.completeIcon.gameObject:SetActiveIfNecessary(info.isComplete)
end

WorldLevelUpCtrl._RefreshWayCell = HL.Method(HL.Any, HL.Table) << function(self, cell, info)
    cell.titleTxt.text = info.displayTitleText or info.conditionText or ""
    cell.contentTxt.text = info.displayContentText or info.progressText or ""

    local showTag = not string.isEmpty(info.tagSpriteName)
    cell.tagImg.gameObject:SetActiveIfNecessary(showTag)
    if showTag then
        cell.tagImg:LoadSprite(UIConst.WORLD_LEVEL_CONDITION_TAG_ICON_FOLDER, info.tagSpriteName)
    end

    cell.completedNode.gameObject:SetActiveIfNecessary(info.isComplete)
    cell.btnGoto.gameObject:SetActiveIfNecessary(not info.isComplete)
    cell.btnGoto.onClick:RemoveAllListeners()
    self.m_defaultNaviCell = self.m_defaultNaviCell or cell.inputBindingGroupNaviDecorator
    if info.onClick ~= nil and not info.isComplete then
        cell.btnGoto.onClick:AddListener(info.onClick)
    end
end

HL.Commit(WorldLevelUpCtrl)