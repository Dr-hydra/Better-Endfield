local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ContingencySelectTag
local PHASE_ID = PhaseId.ContingencySelectTag
ContingencySelectTagCtrl = HL.Class('ContingencySelectTagCtrl', uiCtrl.UICtrl)






ContingencySelectTagCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CONTINGENCY_CONTRACT_SET_TAG] = '_OnSetTagSuccess',
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE] = '_OnMultiStageUpdate',
}



local ccSystem = GameInstance.player.contingencyContractSystem

local MAX_LOCK_COUNT = 2
local SORT_TYPE_ENUM = {
    JoinSeq = 1,
    Level = 2,
}
local LockDir = {
    None = 0,
    Left = 1,
    Right = 2,
}
local CheckOutsideLockDotInterval = 0.1
local RomanNumber1to5 = { "I", "II", "III", "IV", "V" }
local COMMON_INTRO_ID = "contingency_contract"


ContingencySelectTagCtrl.m_gameId = HL.Field(HL.String) << ""

ContingencySelectTagCtrl.m_activityId = HL.Field(HL.String) << ""

ContingencySelectTagCtrl.m_basicInfo = HL.Field(HL.Table)

ContingencySelectTagCtrl.m_tagInfos = HL.Field(HL.Table)

ContingencySelectTagCtrl.m_allTagInfosMap = HL.Field(HL.Table)

ContingencySelectTagCtrl.m_tagKeyInfos = HL.Field(HL.Table)

ContingencySelectTagCtrl.m_tagConflictInfos = HL.Field(HL.Table)

ContingencySelectTagCtrl.m_unlockScoreInfos = HL.Field(HL.Table)

ContingencySelectTagCtrl.m_conflictArrowInfos = HL.Field(HL.Table)

ContingencySelectTagCtrl.m_joinedTagInfos = HL.Field(HL.Table)

ContingencySelectTagCtrl.m_joinedTagInfosMap = HL.Field(HL.Table)

ContingencySelectTagCtrl.m_joinedSeqGlobal = HL.Field(HL.Number) << 1

ContingencySelectTagCtrl.m_newJoinedKeyInfo = HL.Field(HL.Table)

ContingencySelectTagCtrl.m_outsideLockDotInfo = HL.Field(HL.Table)

ContingencySelectTagCtrl.m_sortSetting = HL.Field(HL.Table)



ContingencySelectTagCtrl.m_tagCellCache = HL.Field(HL.Table)

ContingencySelectTagCtrl.m_getTagEffectCellFunc = HL.Field(HL.Function)

ContingencySelectTagCtrl.m_arrowCellCache = HL.Field(HL.Forward("UIListCache"))

ContingencySelectTagCtrl.m_lockAreaCellCache = HL.Field(HL.Forward("UIListCache"))

ContingencySelectTagCtrl.m_tipsKeyCellCacheList = HL.Field(HL.Table)

ContingencySelectTagCtrl.m_curSelectTagCellIndex = HL.Field(HL.Number) << -1

ContingencySelectTagCtrl.m_waitEnterDungeon = HL.Field(HL.Boolean) << false

ContingencySelectTagCtrl.m_checkOutsideLockDotTickTime = HL.Field(HL.Number) << 0

ContingencySelectTagCtrl.m_curOpenTipsTagIndex = HL.Field(HL.Number) << -1

ContingencySelectTagCtrl.m_tipsTagUnlockTimeCor = HL.Field(HL.Thread)

ContingencySelectTagCtrl.m_lockAreaAniCor = HL.Field(HL.Thread)

ContingencySelectTagCtrl.m_tagEffectCellInAniUpdateKey = HL.Field(HL.Number) << -1




ContingencySelectTagCtrl.m_isNaviTagCell = HL.Field(HL.Boolean) << false

ContingencySelectTagCtrl.m_curNaviTagCellIndex = HL.Field(HL.Number) << -1

ContingencySelectTagCtrl.m_curNaviTagEffectCellIndex = HL.Field(HL.Number) << -1

ContingencySelectTagCtrl.m_isEnableAreaOperate = HL.Field(HL.Boolean) << true






ContingencySelectTagCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local recoverState = arg and arg.recoverState or nil
    self:_InitUI(recoverState)
    self:_InitData(arg)
    if not next(self.m_tagInfos) then
        logger.error("危机ContingencySelectTagCtrl词条选择界面：tag数据为空！gameId：" .. self.m_gameId)
        return
    end
    self:_UpdateData()
    self:_RefreshAllUI()
    local recoverJoinedTagIds = recoverState and recoverState.joinedTagIds
    local needAutoScroll = arg.tagIds ~= nil or not string.isEmpty(arg.shareCode)
    self:_ApplyShareTag(recoverJoinedTagIds or self.m_basicInfo.defaultTagIds, recoverJoinedTagIds ~= nil, needAutoScroll)
    self:_RefreshCurScoreUI()
    self:_TryPlayTagEffectCellInAni()

    local sucOpenPopup = self:TryRecoverPopupState(recoverState and recoverState.popupState)
    if sucOpenPopup then
        self:_ActiveSwitchAreaAndTagEffectControllerScroll(self.view.inputGroup.internalEnabled)
    end
    if arg then
        arg.recoverState = nil
    end
end

ContingencySelectTagCtrl.OnShow = HL.Override() << function(self)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.topRightNode)
    self.view.commonTopTitleNode.walletBarPlaceholder:InitWalletBarPlaceholder({ self.m_basicInfo.moneyId })
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    local ccData = ccSystem:GetCcGameData(self.m_gameId)
    if not ccData then
        self:_SetNaviTagCell(false)
        return
    end

    local preBestScore = ccData.preHistoryBestScore
    local curBestScore = ccData.historyBestScore
    if preBestScore == curBestScore then
        self:_SetNaviTagCell(false)
        return
    end

    ccData.preHistoryBestScore = curBestScore
    for index, info in pairs(self.m_basicInfo.needLockAreaInfos) do
        if preBestScore < info.score and info.score <= curBestScore then
            self.m_basicInfo.scoreLockAniData.playTailIndex = index
            if self.m_basicInfo.scoreLockAniData.playHeadIndex < 1 then
                self.m_basicInfo.scoreLockAniData.playHeadIndex = index
            end
            self.view.inputGroup.enabled = false
            self.view.forbidInteractMask.gameObject:SetActive(true)
        end
    end

end

ContingencySelectTagCtrl.OnAnimationInFinished = HL.Override() << function(self)
    if self.m_basicInfo.scoreLockAniData.playTailIndex > 0 then

        local headIndex = self.m_basicInfo.scoreLockAniData.playHeadIndex
        local cell = self.m_lockAreaCellCache:Get(headIndex)
        for i = headIndex, self.m_basicInfo.scoreLockAniData.playTailIndex do
            local lockCell = self.m_lockAreaCellCache:Get(i)
            lockCell.gameObject:SetActive(true)
        end
        self.view.tagList:AutoScrollToRectTransform(cell.transform)

        self.m_lockAreaAniCor = self:_StartCoroutine(function()
            while true do
                coroutine.step()
                if not self.view.tagList.inScrollTween then

                    local headCell = self.m_lockAreaCellCache:Get(headIndex)
                    AudioAdapter.PostEvent("Au_UI_Event_CCReignite_NodeStarUnlock")
                    headCell.animationWrapper:Play("selecttag_lockedareacell_out", function()
                        self.view.inputGroup.enabled = true
                        self.view.forbidInteractMask.gameObject:SetActive(false)
                        headCell.gameObject:SetActive(false)
                        self.m_curNaviTagCellIndex = -1
                        self:_SetNaviTagCell(false)
                    end)
                    for i = headIndex + 1, self.m_basicInfo.scoreLockAniData.playTailIndex do
                        local curCell = self.m_lockAreaCellCache:Get(i)
                        AudioAdapter.PostEvent("selecttag_lockedareacell_out")
                        curCell.animationWrapper:Play("selecttag_lockedareacell_out", function()
                            curCell.gameObject:SetActive(false)
                        end)
                    end
                    self.m_lockAreaAniCor = nil
                    self.m_basicInfo.scoreLockAniData.playTailIndex = 0
                    break
                end
            end
        end)
    end
    self.m_basicInfo.scoreLockAniData.preHistoryMaxScore = self.m_basicInfo.historyMaxScore
end

ContingencySelectTagCtrl.OnClose = HL.Override() << function(self)
    LuaUpdate:Remove(self.m_tagEffectCellInAniUpdateKey)
    self.m_tagEffectCellInAniUpdateKey = -1
    self:_SetTagEffectControllerScrollEnabled(false)

    self.m_lockAreaAniCor = self:_ClearCoroutine(self.m_lockAreaAniCor)
    self.m_tipsTagUnlockTimeCor = self:_ClearCoroutine(self.m_tipsTagUnlockTimeCor)
end

ContingencySelectTagCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    self:_ActiveSwitchAreaAndTagEffectControllerScroll(active)
end

ContingencySelectTagCtrl._ActiveSwitchAreaAndTagEffectControllerScroll = HL.Method(HL.Boolean) << function(self, active)
    if not DeviceInfo.usingController then
        return
    end
    if active then
        local canSwitchArea = #self.m_joinedTagInfos > 0
        self.view.switchAreaKeyHint.gameObject:SetActive(canSwitchArea)
        self:_SetTagEffectControllerScrollEnabled(self.m_isEnableAreaOperate)
    else
        self.view.switchAreaKeyHint.gameObject:SetActive(false)
        self:_SetTagEffectControllerScrollEnabled(false)
    end
end



ContingencySelectTagCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    self.m_gameId = arg.gameId
    local defaultTagIds = arg.tagIds
    local defaultShareTag = arg.shareCode
    local defaultTagIdsValid = true
    if not string.isEmpty(defaultShareTag) then
        defaultTagIdsValid, defaultTagIds = ContingencyContractUtils.AnalyzeShareCode(self.m_gameId, string.upper(defaultShareTag))
    elseif defaultTagIds then
        defaultTagIdsValid = ContingencyContractUtils.CheckJoinedTagListValid(self.m_gameId, defaultTagIds)
    end
    if not defaultTagIdsValid then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CONTINGENCY_CONTRACT_SELECT_TAG_NOT_VALID_TOAST)
        defaultTagIds = nil
    end
    local ccData = ccSystem:GetCcGameData(self.m_gameId)
    if ccData then
        if defaultTagIds == nil or #defaultTagIds <= 0 then
            defaultTagIds = {}
            if defaultTagIdsValid then

                for _, tagId in pairs(ccData.curSelectTagList) do
                    table.insert(defaultTagIds, tagId)
                end
            end
        end
    end
    self.m_basicInfo = {
        curScore = 0,
        preScore = 0,
        historyMaxScore = 0,
        defaultTagIds = defaultTagIds,
        needLockAreaInfos = {},

        moneyId = "",
        warningScore1 = 5,
        warningScore2 = 10,

        scoreLockAniData = {
            preHistoryMaxScore = 0,
            playHeadIndex = 0,
            playTailIndex = 0,
        },
    }
    self.m_sortSetting = {
        sortType = SORT_TYPE_ENUM.JoinSeq,
        isIncremental = false,
    }
    self.m_joinedTagInfosMap = {}

    local infosTable = ContingencyContractUtils.GetTagParseInfos(self.m_gameId)
    self.m_tagInfos = infosTable.tagInfos
    self.m_allTagInfosMap = infosTable.allTagInfosMap
    self.m_tagKeyInfos = infosTable.tagKeyInfos
    self.m_tagConflictInfos = infosTable.tagConflictInfos

    if UNITY_EDITOR and ContingencyContractUtils.IsRemoveCCTagLimit then
        self.m_unlockScoreInfos = {}
        for _, tagInfo in pairs(self.m_allTagInfosMap) do
            tagInfo.unlockScore = 0
        end
    else
        self.m_unlockScoreInfos = infosTable.unlockScoreInfos
    end

    local needLockAreaInfos = {}
    for score, colInfo in pairs(self.m_unlockScoreInfos) do
        table.insert(needLockAreaInfos, colInfo)
    end
    table.sort(needLockAreaInfos, function(a, b)
        return a.score < b.score
    end)
    self.m_basicInfo.needLockAreaInfos = needLockAreaInfos

    self:_InitConflictArrowData()
    local hasCfg, ccCfg = Tables.contingencyContractTable:TryGetValue(self.m_gameId)
    if not hasCfg then
        return false
    end
    self.m_activityId = ccCfg.activityId
    local _, ccActivityCfg = Tables.activityContingencyContractTable:TryGetValue(self.m_activityId)
    local moneyId = Tables.activityShopAdditionalTable[ccActivityCfg.shopGroupId].activityMoneyId
    self.m_basicInfo.moneyId = moneyId
    local scoreBandCount = ccActivityCfg.scoreBand.Count
    if scoreBandCount < 2 then
        logger.error(string.format("危机合约活动表，[Id:%s]活动配置scoreBand数量少于2！无法正确显示分数难度提示功能"))
    else
        self.m_basicInfo.warningScore1 = ccActivityCfg.scoreBand[0]
        self.m_basicInfo.warningScore2 = ccActivityCfg.scoreBand[1]
    end
end

ContingencySelectTagCtrl._InitConflictArrowData = HL.Method() << function(self)

    self.m_conflictArrowInfos = {}
    for conflictId, conflictInfo in pairs(self.m_tagConflictInfos) do
        local conflictTagCount = #conflictInfo.conflictTagsList
        local limit = conflictTagCount - 1

        for i = 1, limit do
            local tagInfoI = conflictInfo.conflictTagsList[i]
            local arrowInfoI = self.m_conflictArrowInfos[tagInfoI.cellIndex]
            if arrowInfoI == nil then
                arrowInfoI = {
                    recentIndexL = math.maxinteger,
                    recentIndexU = math.maxinteger,
                    recentIndexR = math.maxinteger,
                    recentIndexD = math.maxinteger,
                    tagCellIndex = tagInfoI.cellIndex
                }
                self.m_conflictArrowInfos[tagInfoI.cellIndex] = arrowInfoI
            end

            for j = i + 1, conflictTagCount do
                local tagInfoJ = conflictInfo.conflictTagsList[j]
                local arrowInfoJ = self.m_conflictArrowInfos[tagInfoJ.cellIndex]
                if arrowInfoJ == nil then
                    arrowInfoJ = {
                        recentIndexL = math.maxinteger,
                        recentIndexU = math.maxinteger,
                        recentIndexR = math.maxinteger,
                        recentIndexD = math.maxinteger,
                        tagCellIndex = tagInfoJ.cellIndex
                    }
                    self.m_conflictArrowInfos[tagInfoJ.cellIndex] = arrowInfoJ
                end

                if tagInfoI.row == tagInfoJ.row then
                    local indexDiff = math.abs(tagInfoI.column - tagInfoJ.column)
                    if tagInfoI.column > tagInfoJ.column then

                        arrowInfoI.recentIndexL = math.min(indexDiff, arrowInfoI.recentIndexL)
                        arrowInfoJ.recentIndexR = math.min(indexDiff, arrowInfoJ.recentIndexR)
                    else

                        arrowInfoI.recentIndexR = math.min(indexDiff, arrowInfoI.recentIndexR)
                        arrowInfoJ.recentIndexL = math.min(indexDiff, arrowInfoJ.recentIndexL)
                    end
                end

                if tagInfoI.column == tagInfoJ.column then
                    local indexDiff = math.abs(tagInfoI.row - tagInfoJ.row)
                    if tagInfoI.row > tagInfoJ.row then

                        arrowInfoI.recentIndexU = math.min(indexDiff, arrowInfoI.recentIndexU)
                        arrowInfoJ.recentIndexD = math.min(indexDiff, arrowInfoJ.recentIndexD)
                    else

                        arrowInfoI.recentIndexD = math.min(indexDiff, arrowInfoI.recentIndexD)
                        arrowInfoJ.recentIndexU = math.min(indexDiff, arrowInfoJ.recentIndexU)
                    end
                end
            end
        end
    end

    local tempList = {}
    for cellIndex, arrowInfo in pairs(self.m_conflictArrowInfos) do
        table.insert(tempList, arrowInfo)
    end
    self.m_conflictArrowInfos = tempList
end

ContingencySelectTagCtrl._UpdateData = HL.Method() << function(self)


    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    if activityData then
        for _, tagInfo in pairs(self.m_allTagInfosMap) do
            local isUnlock = true
            if not string.isEmpty(tagInfo.unlockStage) then

                local hasData, stageDataMsg = activityData.stageDataDict:TryGetValue(tagInfo.unlockStage)
                if hasData then
                    tagInfo.openTime = stageDataMsg.OpenTimeTs
                end
                isUnlock = ActivityUtils.isStageUnlockMultiConditionStageActivity(activityData, tagInfo.unlockStage)
            end
            tagInfo.isUnlockByStage = isUnlock
        end
    else
        for _, tagInfo in pairs(self.m_allTagInfosMap) do
            local isUnlock = string.isEmpty(tagInfo.unlockStage)
            tagInfo.isUnlockByStage = isUnlock
        end
    end


    local ccData = ccSystem:GetCcGameData(self.m_gameId)
    if ccData then
        self.m_basicInfo.historyMaxScore = ccData.historyBestScore
        self.m_basicInfo.scoreLockAniData.preHistoryBestScore = ccData.preHistoryBestScore
        for _, tagId in pairs(ccData.receivedRewardTagList) do
            self.m_allTagInfosMap[tagId].hasFirstPassReward = false
        end
    end
end

ContingencySelectTagCtrl._UpdateOutsideDotInfo = HL.Method() << function(self)
    if not self.m_newJoinedKeyInfo then
        return
    end

    if self.m_outsideLockDotInfo and self.m_newJoinedKeyInfo.keyId == self.m_outsideLockDotInfo.keyId then
        self.m_newJoinedKeyInfo = nil
        return
    end

    local outsideLockInfos = {}
    for tagId, tagInfo in pairs(self.m_newJoinedKeyInfo.lockTags) do
        local isUnlock = tagInfo.isUnlockByStage and (tagInfo.unlockScore <= 0 or tagInfo.unlockScore <= self.m_basicInfo.historyMaxScore)
        if isUnlock then
            local cellIndex = tagInfo.cellIndex
            local tagCell = self:_GetTagCell(cellIndex)
            if tagCell and not self:_isTagCellCanView(tagCell.transform) then
                table.insert(outsideLockInfos, {
                    isNotView = true,
                    cellIndex = cellIndex,
                })
            end
        end
    end
    local hasOutsideLock = #outsideLockInfos > 0
    if hasOutsideLock then
        self.view.outsideDotNode:SetState("LockDot")
        self.view.outsideLockDot.color = self.m_newJoinedKeyInfo.color
        self.m_outsideLockDotInfo = {
            keyId = self.m_newJoinedKeyInfo.keyId,
            outsideLockInfos = outsideLockInfos,
        }
        self.m_newJoinedKeyInfo = nil
        self.m_checkOutsideLockDotTickTime = Time.time + CheckOutsideLockDotInterval
    end
end



ContingencySelectTagCtrl._InitUI = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    self.view.commonTopTitleNode.btnClose.onClick:AddListener(function()
        self:_SendSetTagList()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self.view.commonTopTitleNode.helpBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_INTRO, COMMON_INTRO_ID)
    end)

    self.view.dungeonInfoBtn.onClick:AddListener(function()
        self:_OpenDetailsPopup()
    end)

    self.view.shareTagBtn.onClick:AddListener(function()
        self:_OpenImportSharePopup()
    end)
    self.view.entryBtn.onClick:AddListener(function()
        self:_SendSetTagList()
        self.m_waitEnterDungeon = true
        if UNITY_EDITOR and ContingencyContractUtils.IsRemoveCCTagLimit then
            self:_OnSetTagSuccess({ self.m_gameId })
        end
    end)
    self.view.clearAllTagBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_CONTINGENCY_CONTRACT_SELECT_CONFIRM_CLEAR_ALL_TAG,
            warningContent = Language.LUA_CONTINGENCY_CONTRACT_SELECT_CONFIRM_CLEAR_ALL_TAG_WARNING,
            onConfirm = function()
                self:_ClearAllTag(true, true)
            end
        })
    end)

    self.view.rewardInstructionBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.ContingencyContractInstructionBook, self.m_gameId)
    end)

    self.view.tagList.onValueChanged:AddListener(function(pos)
        self:_RefreshOutsideLockDot()
    end)
    self.view.tagListDragHandler.onBeginDrag:AddListener(function()
        if self.view.tipsNodeRoot.gameObject.activeSelf then
            self.view.tagTipsNode.autoCloseArea:TryCloseSelf()
        end
    end)
    self.view.tagListBtn.onClick:AddListener(function()
        if self.view.tipsNodeRoot.gameObject.activeSelf then
            self.view.tagTipsNode.autoCloseArea:TryCloseSelf()
        end
    end)
    self.view.selectTagArrow.gameObject:SetActive(false)

    local SORT_OPTIONS = {
        {
            name = Language.LUA_CONTINGENCY_CONTRACT_SELECT_SORT_NAME_BY_TIME,
            keys = { "joinedSeq", "cellIndex" }
        },
        {
            name = Language.LUA_CONTINGENCY_CONTRACT_SELECT_SORT_NAME_BY_LEVEL,
            keys = { "level", "joinedSeq", "cellIndex" }
        },
    }

    local sortSelectedIndex = recoverState and recoverState.sortSelectedIndex or 1
    sortSelectedIndex = lume.clamp(sortSelectedIndex, 1, #SORT_OPTIONS)
    local sortIsIncremental = recoverState and recoverState.sortIsIncremental
    if sortIsIncremental == nil then
        sortIsIncremental = false
    end
    self.view.sortNode:InitSortNode(SORT_OPTIONS, function(data, isIncremental)
        self:_SortAndRefreshTagEffectList(data, isIncremental)
        self.view.tagEffectList:ScrollToIndex(0, true, CS.Beyond.UI.UIScrollList.ScrollAlignType.Top)
        if DeviceInfo.usingController then
            if not self.m_isNaviTagCell then
                self:_SetNaviTagEffectCell()
            end
        end
    end, CSIndex(sortSelectedIndex), sortIsIncremental, true)

    self.view.tagCell.gameObject:SetActive(false)
    self.view.emptyTagCell.gameObject:SetActive(false)

    self.m_tagCellCache = {
        cellMap = {},
        usingTagCells = {},
        usingEmptyCells = {},
        recycledTagCells = {},
        recycledEmptyCells = {},
    }
    self.m_getTagEffectCellFunc = UIUtils.genCachedCellFunction(self.view.tagEffectList)
    self.view.tagEffectList.onUpdateCell:RemoveAllListeners()
    self.view.tagEffectList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_RefreshTagEffectCell(self.m_getTagEffectCellFunc(obj), LuaIndex(csIndex))
    end)
    self.m_arrowCellCache = UIUtils.genCellCache(self.view.arrowCell)
    self.m_lockAreaCellCache = UIUtils.genCellCache(self.view.lockedAreaCell)
    self.m_tipsKeyCellCacheList = {}
    for i = 1, MAX_LOCK_COUNT do
        local cellCache = UIUtils.genCellCache(self.view.tagTipsNode["keyLockInfoCell" .. i].keyInfoCell)
        table.insert(self.m_tipsKeyCellCacheList, cellCache)
    end


    UIUtils.bindInputPlayerAction("contingency_select_tag_change_area_left", function()
        if not self.m_isNaviTagCell then
            self:_SetNaviTagCell(true)
        end
    end, self.view.inputGroup.groupId)
    UIUtils.bindInputPlayerAction("contingency_select_tag_change_area_right", function()
        if self.m_isNaviTagCell then
            if #self.m_joinedTagInfos > 0 then
                self:_SetNaviTagEffectCell()
            end
        end
    end, self.view.inputGroup.groupId)


    self.view.outsideRedDot.getRedDotStateAt = function(csIndex)
        return self:_GetRedDotStateAt(csIndex)
    end


    AudioManager.PostEvent("au_music_meta_ui_cc_v1d3_preparing_enter")
end

ContingencySelectTagCtrl._GetJoinedTagIds = HL.Method().Return(HL.Table) << function(self)
    local joinedTagInfos = {}
    for _, tagInfo in pairs(self.m_joinedTagInfosMap) do
        table.insert(joinedTagInfos, tagInfo)
    end

    table.sort(joinedTagInfos, function(a, b)
        return a.joinedSeq < b.joinedSeq
    end)
    local joinedTagIds = {}
    for _, tagInfo in ipairs(joinedTagInfos) do
        table.insert(joinedTagIds, tagInfo.tagId)
    end
    return joinedTagIds
end

ContingencySelectTagCtrl._OpenDetailsPopup = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    PhaseManager:OpenPhase(PhaseId.ContingencyContractDetailsPopup, {
        gameId = self.m_gameId,
        tagIds = self:_GetJoinedTagIds(),
        recoverState = recoverState,
    })
end

ContingencySelectTagCtrl._OpenImportSharePopup = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    UIManager:Open(PanelId.ContingencyContractImportShare, {
        gameId = self.m_gameId,
        joinedTagIds = self:_GetJoinedTagIds(),
        totalScore = self.m_basicInfo.curScore,
        recoverState = recoverState,
        importCallback = function(shareTagIds)
            self:_ApplyShareTag(shareTagIds, false, true)
        end,
    })
end

ContingencySelectTagCtrl.GetRecoverPopupStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local isOpen, importShareCtrl = UIManager:IsOpen(PanelId.ContingencyContractImportShare)
    if isOpen then
        local popupState = {
            popupType = "ImportShare",
        }
        if HL.TryGet(importShareCtrl, "GetRecoverStateArg") then
            popupState.recoverState = importShareCtrl:GetRecoverStateArg()
        end
        return popupState
    end

    local isInstructionBookOpen = UIManager:IsOpen(PanelId.ContingencyContractInstructionBook)
    if isInstructionBookOpen then
        return {
            popupType = "ContingencyContractInstructionBook",
        }
    end

    local isCommonIntroOpen, commonIntroCtrl = UIManager:IsOpen(PanelId.CommonIntro)
    if isCommonIntroOpen and commonIntroCtrl then
        local introState = commonIntroCtrl:GetRecoverStateArg()

        if introState ~= nil and introState.introId == COMMON_INTRO_ID then
            return {
                popupType = "CommonIntro",
                introState = introState,
            }
        end
    end
end

ContingencySelectTagCtrl.TryRecoverPopupState = HL.Method(HL.Any).Return(HL.Boolean) << function(self, popupState)
    if popupState == nil or string.isEmpty(popupState.popupType) then
        return false
    end
    if popupState.popupType == "ImportShare" then
        local isOpen = UIManager:IsOpen(PanelId.ContingencyContractImportShare)
        if isOpen then
            return true
        end

        self:_OpenImportSharePopup(popupState.recoverState)
        isOpen = UIManager:IsOpen(PanelId.ContingencyContractImportShare)
        return isOpen
    end

    if popupState.popupType == "ContingencyContractInstructionBook" then
        local isOpen = UIManager:IsOpen(PanelId.ContingencyContractInstructionBook)
        if isOpen then
            return true
        end
        UIManager:Open(PanelId.ContingencyContractInstructionBook, self.m_gameId)
        isOpen = UIManager:IsOpen(PanelId.ContingencyContractInstructionBook)
        return isOpen
    end

    if popupState.popupType == "CommonIntro" then
        local isOpen = UIManager:IsOpen(PanelId.CommonIntro)
        if isOpen then
            return true
        end
        local introState = popupState.introState
        if introState ~= nil and introState.introId == COMMON_INTRO_ID then
            UIManager:Open(PanelId.CommonIntro, introState)
        else
            Notify(MessageConst.SHOW_INTRO, COMMON_INTRO_ID)
        end
        isOpen = UIManager:IsOpen(PanelId.CommonIntro)
        return isOpen
    end
    return false
end

ContingencySelectTagCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local joinedTagIds = self:_GetJoinedTagIds()
    return {
        gameId = self.m_gameId,

        tagIds = joinedTagIds,
        recoverState = {
            joinedTagIds = joinedTagIds,
            sortSelectedIndex = self.view.sortNode:GetCurSelectedIndex(),
            sortIsIncremental = self.view.sortNode.isIncremental,
            popupState = self:GetRecoverPopupStateArg(),
        },
    }
end

ContingencySelectTagCtrl._ClearTagContentCells = HL.Method() << function(self)
    local tagCellCache = self.m_tagCellCache
    tagCellCache.cellMap = {}


    for _, cell in pairs(tagCellCache.usingTagCells) do
        cell.gameObject:SetActive(false)
        table.insert(tagCellCache.recycledTagCells, cell)
    end
    for _, cell in pairs(tagCellCache.usingEmptyCells) do
        cell.gameObject:SetActive(false)
        table.insert(tagCellCache.recycledEmptyCells, cell)
    end

    tagCellCache.usingTagCells = {}
    tagCellCache.usingEmptyCells = {}
end


ContingencySelectTagCtrl._GetOrCreateTagContentCell = HL.Method(GameObject, HL.Table).Return(HL.Table) << function(self, templateObj, recycledCells)
    local cell = table.remove(recycledCells)
    if not cell then
        local child = UIUtils.addChild(self.view.tagListContent.gameObject, templateObj, true)
        cell = Utils.wrapLuaNode(child)
    end
    cell.gameObject:SetActive(true)
    return cell
end


ContingencySelectTagCtrl._GenTagContentCell = HL.Method(HL.Boolean, HL.Number).Return(HL.Table) << function(self, isEmpty, luaIndex)
    local tagCellCache = self.m_tagCellCache
    local templateObj = isEmpty and self.view.emptyTagCell.gameObject or self.view.tagCell.gameObject
    local recycledCells = isEmpty and tagCellCache.recycledEmptyCells or tagCellCache.recycledTagCells
    local usingCells = isEmpty and tagCellCache.usingEmptyCells or tagCellCache.usingTagCells

    local cell = self:_GetOrCreateTagContentCell(templateObj, recycledCells)
    cell.transform:SetSiblingIndex(luaIndex)
    cell.gameObject.name = (isEmpty and "emptyTagCell_" or "tagCell_") .. luaIndex

    table.insert(usingCells, cell)
    tagCellCache.cellMap[luaIndex] = cell
    return cell
end


ContingencySelectTagCtrl._GetTagCell = HL.Method(HL.Number).Return(HL.Opt(HL.Table)) << function(self, cellIndex)
    local tagCellCache = self.m_tagCellCache
    if not tagCellCache then
        return nil
    end
    return tagCellCache.cellMap[cellIndex]
end


ContingencySelectTagCtrl._RefreshTagCellList = HL.Method() << function(self)

    self.view.tagCell.stateController:SetState("Open")
    self.view.tagCell.stateController:SetState("NotConflict")
    self.view.tagCell.stateController:SetState("NotJoin")
    self.view.tagCell.stateController:SetState("NotKey")
    self.view.tagCell.lockNode.stateController:SetState("NoLock")

    self:_ClearTagContentCells()
    self.view.tagCell.gameObject:SetActive(true)
    local count = #self.m_tagInfos * ContingencyContractUtils.MAX_ROW_COUNT
    for luaIndex = 1, count do
        local column, row = ContingencyContractUtils.GetColumnRow(luaIndex)
        local tagInfo = self.m_tagInfos[column][row]
        tagInfo.tagEffectIndex = -1


        local cell = self:_GenTagContentCell(tagInfo.isEmpty, luaIndex)
        if not tagInfo.isEmpty then
            self:_InitTagCell(cell, luaIndex)
        end
    end
    self.view.tagCell.gameObject:SetActive(false)
end

ContingencySelectTagCtrl._RefreshAllUI = HL.Method() << function(self)
    self:_RefreshTagCellList()
    self:_InitLockScoreArea()
    self:_UpdateLockScoreArea()

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.tagListContent)
    self.m_arrowCellCache:Refresh(#self.m_conflictArrowInfos, function(cell, luaIndex)
        self:_InitConflictArrowCell(cell, luaIndex)
    end)
    self.view.arrowRoot:SetSiblingIndex(1)
    self.view.selectArrowRoot:SetAsLastSibling()
    self.view.lockedAreaRoot:SetAsLastSibling()

    self:_SortAndRefreshJoinedTagInfos()
    self:_HideTagTips(true)
    self.view.historyMaxScoreTxt.text = self.m_basicInfo.historyMaxScore







    self.view.outsideDotNode:SetState("RedDot")
end

ContingencySelectTagCtrl._InitTagCell = HL.Method(HL.Any, HL.Number) << function(self, inCell, luaIndex)

    local cell = inCell
    local column, row = ContingencyContractUtils.GetColumnRow(luaIndex)
    local tagInfo = self.m_tagInfos[column][row]
    if tagInfo.isEmpty then
        return
    end

    cell.gameObject.name = "tagCell_" .. tagInfo.tagId
    tagInfo.tagEffectIndex = -1
    local isOpen = tagInfo.isUnlockByStage
    cell.stateController:SetState("NotConflict")
    cell.stateController:SetState((tagInfo.hasFirstPassReward and isOpen) and "HasFirstPassReward" or "NoFirstPassReward")
    cell.tagIcon:LoadSprite(UIConst.UI_SPRITE_CONTINGENCY_CONTRACT_BUFF, tagInfo.icon)

    local btn
    if isOpen then
        cell.stateController:SetState("Open")
        cell.stateController:SetState("NotJoin")

        if not string.isEmpty(tagInfo.keyId) then
            local keyInfo = self.m_tagKeyInfos[tagInfo.keyId]
            cell.stateController:SetState("IsKey")
            keyInfo.color.a = cell.keyIcon.color.a
            cell.keyIcon.color = keyInfo.color
            keyInfo.color.a = cell.keyLightColorImg.color.a
            cell.keyLightColorImg.color = keyInfo.color
            keyInfo.color.a = 1
            cell.keyNode:LoadSprite(UIConst.UI_SPRITE_CONTINGENCY_CONTRACT, keyInfo.keyDecoImg)
        else
            cell.stateController:SetState("NotKey")
        end

        local lockNode = cell.lockNode
        if self:_IsTagLockByTag(tagInfo) then
            local lockCount = #tagInfo.lockIds
            local lockId = tagInfo.lockIds[1]
            local keyInfo = self.m_tagKeyInfos[lockId]
            lockNode.lock1.color = keyInfo.color
            lockNode.lock1Unlock.color = keyInfo.color
            lockNode.loopEffectLineImg.color = keyInfo.color
            if lockCount == 1 then
                lockNode.stateController:SetState("SingleLock")
            else
                lockNode.stateController:SetState("DoubleLock1")
                lockNode.stateController:SetState("DoubleLock2")
                lockId = tagInfo.lockIds[2]
                keyInfo = self.m_tagKeyInfos[lockId]
                lockNode.lock2.color = keyInfo.color
                lockNode.lock2Unlock.color = keyInfo.color
            end
        else
            lockNode.stateController:SetState("NoLock")
        end

        btn = cell.openedBtn
        cell.openedBtn.onClick:RemoveAllListeners()
        cell.openedBtn.onClick:AddListener(function()
            self:_OnClickOpenedTag(tagInfo, false)
        end)
        if DeviceInfo.usingController then

            cell.controllerDeleteBtn.onClick:RemoveAllListeners()
            cell.controllerDeleteBtn.onClick:AddListener(function()
                self:_OnClickOpenedTag(tagInfo, true)
            end)
            cell.controllerDeleteBtn.enabled = false
        end
        self:_RefreshTagCellOpenBtnHoverText(tagInfo)
    else
        local canPreview = tagInfo.canPreview
        cell.stateController:SetState(canPreview and "NotOpenButPreview" or "NotOpen")
        btn = cell.notOpenBtn

        cell.notOpenBtn.onClick:RemoveAllListeners()
        cell.notOpenBtn.onClick:AddListener(function()
            self:_OnCLickNotOpenTag(tagInfo)
        end)
        InputManagerInst:SetBindingText(btn.hoverConfirmBindingId, Language.LUA_CONTINGENCY_CONTRACT_SELECT_TAG_NAVI_DETAIL)
    end

    btn.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self.m_curNaviTagCellIndex = luaIndex
            self.m_curNaviTagEffectCellIndex = -1
            self.m_isNaviTagCell = true
            self:_ChangeAreaOperateEnable(true)
            local curIsJoined = self.m_joinedTagInfosMap[tagInfo.tagId] ~= nil
            cell.controllerDeleteBtn.enabled = curIsJoined
            self:_RefreshTagCellOpenBtnHoverText(tagInfo)
        else
            cell.controllerDeleteBtn.enabled = false
            self:_ChangeTagSelect(tagInfo, false, false)
        end
    end

    cell.redDot.gameObject:SetActive(true)
    cell.redDot:InitRedDot("ContingencyContractTag", {
        activityId = self.m_activityId,
        stageId = tagInfo.unlockStage,
        tagId = tagInfo.tagId
    }, nil, self.view.outsideRedDot)

end

ContingencySelectTagCtrl._RefreshTagCellOpenBtnHoverText = HL.Method(HL.Table) << function(self, tagInfo)
    local tagCell = self:_GetTagCell(tagInfo.cellIndex)
    if not tagCell then
        return
    end

    local curIsJoined = self.m_joinedTagInfosMap[tagInfo.tagId] ~= nil
    local preIsSelect = tagInfo.cellIndex == self.m_curSelectTagCellIndex
    if curIsJoined then
        if preIsSelect then
            InputManagerInst:SetBindingText(tagCell.openedBtn.hoverConfirmBindingId, Language.LUA_CONTINGENCY_CONTRACT_SELECT_TAG_NAVI_CANCEL)
        else
            InputManagerInst:SetBindingText(tagCell.openedBtn.hoverConfirmBindingId, Language.LUA_CONTINGENCY_CONTRACT_SELECT_TAG_NAVI_DETAIL)
        end
    else
        InputManagerInst:SetBindingText(tagCell.openedBtn.hoverConfirmBindingId, Language.LUA_CONTINGENCY_CONTRACT_SELECT_TAG_NAVI_SET_JOIN)
    end
end

ContingencySelectTagCtrl._InitConflictArrowCell = HL.Method(HL.Any, HL.Number) << function(self, inCell, luaIndex)
    local cell = inCell
    local arrowInfo = self.m_conflictArrowInfos[luaIndex]

    local tagCell = self:_GetTagCell(arrowInfo.tagCellIndex)
    if not tagCell then
        return
    end
    cell.rectTransform.anchoredPosition = tagCell.rectTransform.anchoredPosition
    tagCell.arrowNode = cell

    cell.upArrow.gameObject:SetActive(arrowInfo.recentIndexU < math.maxinteger)
    cell.rightArrow.gameObject:SetActive(arrowInfo.recentIndexR < math.maxinteger)

    local tagCellSize = self.view.tagListContentGridLayoutGroup.cellSize
    local tagCellSpacing = self.view.tagListContentGridLayoutGroup.spacing
    if arrowInfo.recentIndexU < math.maxinteger and arrowInfo.recentIndexU > 1 then
        local length = tagCellSize.y * (arrowInfo.recentIndexU - 1) + tagCellSpacing.y * arrowInfo.recentIndexU
        local offset = (tagCellSize.y / 2) - math.abs(cell.upArrow.transform.anchoredPosition.y)
        cell.upArrow.transform:SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, length + offset)
    end
    if arrowInfo.recentIndexR < math.maxinteger and arrowInfo.recentIndexR > 1 then
        local length = tagCellSize.x * (arrowInfo.recentIndexR - 1) + tagCellSpacing.x * arrowInfo.recentIndexR
        local offset = (tagCellSize.x / 2) - math.abs(cell.rightArrow.transform.anchoredPosition.x)
        cell.rightArrow.transform:SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, length + offset)
    end
end

ContingencySelectTagCtrl._InitLockScoreArea = HL.Method() << function(self)
    self.m_lockAreaCellCache:Refresh(#self.m_basicInfo.needLockAreaInfos, function(cell, luaIndex)
        self:_RefreshLockAreaCell(cell, luaIndex, self.m_basicInfo.needLockAreaInfos[luaIndex])
    end)
end

ContingencySelectTagCtrl._UpdateLockScoreArea = HL.Method() << function(self)

    for index, areaInfo in pairs(self.m_basicInfo.needLockAreaInfos) do
        local cell = self.m_lockAreaCellCache:Get(index)
        if cell then

            if self.m_basicInfo.scoreLockAniData.preHistoryBestScore >= areaInfo.score then
                cell.gameObject:SetActive(false)
            else
                cell.gameObject:SetActive(true)
            end
        end
    end

    local tagColCount = #self.m_tagInfos
    for col = 1, tagColCount do
        for row = 1, ContingencyContractUtils.MAX_ROW_COUNT do
            local info = self.m_tagInfos[col][row]
            if not info.isEmpty and info.unlockScore > 0 then
                local isUnlock = info.unlockScore <= self.m_basicInfo.historyMaxScore

                local cell = self:_GetTagCell(info.cellIndex)
                if cell then
                    cell.openedBtn.enabled = isUnlock
                    cell.notOpenBtn.enabled = isUnlock
                end
            end
        end
    end
end

ContingencySelectTagCtrl._RefreshLockAreaCell = HL.Method(HL.Any, HL.Number, HL.Table) << function(self, inCell, luaIndex, info)

    local cell = inCell
    cell.scoreTxt.text = info.score
    cell.titleTxt.text = string.format(Language.LUA_CONTINGENCY_CONTRACT_SELECT_LOCK_AREA_TITLE, RomanNumber1to5[luaIndex])
    cell.naviDeco.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self.m_curNaviTagEffectCellIndex = -1
            if not self.m_isNaviTagCell then
                self.m_isNaviTagCell = true
                self:_ChangeAreaOperateEnable(true)
            end
        end
    end

    local colCount = info.maxCol - info.minCol + 1
    local tagCellSizeX = self.view.tagListContentGridLayoutGroup.cellSize.x
    local tagCellSpacingX = self.view.tagListContentGridLayoutGroup.spacing.x
    local length = colCount * (tagCellSpacingX + tagCellSizeX)
    local paddingLeft = self.view.tagListContentGridLayoutGroup.padding.left - tagCellSpacingX / 2
    local preTagCellLength = (info.minCol - 1) * (tagCellSpacingX + tagCellSizeX)

    cell.rectTransform.anchoredPosition = Vector2(paddingLeft + preTagCellLength, 0)
    cell.rectTransform:SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, length)
end

ContingencySelectTagCtrl._RefreshTagEffectCell = HL.Method(HL.Any, HL.Number) << function(self, inCell, luaIndex)

    local cell = inCell
    local tagInfo = self.m_joinedTagInfos[luaIndex]
    cell.gameObject.name = "tagEffectCell_" .. tagInfo.tagId

    cell.animationWrapper:SampleClipAtPercent("tageffectcell_in", 1)

    cell.levelTxt.text = tagInfo.level
    cell.tagNameTxt.text = tagInfo.tagFullName
    cell.descTxt.text = tagInfo.desc
    cell.stateController:SetState("Level" .. tagInfo.level)
    cell.rewardIcon.gameObject:SetActive(tagInfo.hasFirstPassReward)
    cell.tagIcon:LoadSprite(UIConst.UI_SPRITE_CONTINGENCY_CONTRACT_BUFF, tagInfo.icon)
    local isCurSelect = self.m_curSelectTagCellIndex == tagInfo.cellIndex
    cell.stateController:SetState(isCurSelect and "Select" or "Normal")

    local keyInfo = self.m_tagKeyInfos[tagInfo.keyId]
    if keyInfo then
        cell.keyNode.gameObject:SetActive(true)
        cell.unlockLockNode.gameObject:SetActive(true)
        cell.keyImg.color = keyInfo.color
        cell.unlockLockColorDeco.color = keyInfo.color
        cell.lockDescTxt.color = keyInfo.color
        cell.lockDescTxt.text = string.format(Language.LUA_CONTINGENCY_CONTRACT_SELECT_UNLOCK_KEY, keyInfo.lockName)
    else
        cell.keyNode.gameObject:SetActive(false)
        cell.unlockLockNode.gameObject:SetActive(false)
    end

    local lockCount = #tagInfo.lockIds
    if lockCount > 0 then
        cell.lockNode1.gameObject:SetActive(true)
        if lockCount == 1 then
            cell.lockNode2.gameObject:SetActive(false)
            local lockInfo = self.m_tagKeyInfos[tagInfo.lockIds[1]]
            cell.lockImg1.color = lockInfo.color
        else
            cell.lockNode2.gameObject:SetActive(true)
            local lockInfo = self.m_tagKeyInfos[tagInfo.lockIds[1]]
            cell.lockImg1.color = lockInfo.color
            lockInfo = self.m_tagKeyInfos[tagInfo.lockIds[2]]
            cell.lockImg2.color = lockInfo.color
        end
    else
        cell.lockNode1.gameObject:SetActive(false)
        cell.lockNode2.gameObject:SetActive(false)
    end

    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        self:_ChangeTagSelect(tagInfo, true, false)

        local tagCell = self:_GetTagCell(tagInfo.cellIndex)
        if tagCell then
            self.view.tagList:AutoScrollToRectTransform(tagCell.transform)
        end
        cell.stateController:SetState("Select")
    end)
    cell.deleteCellBtn.onClick:RemoveAllListeners()
    cell.deleteCellBtn.onClick:AddListener(function()

        if self:_IsLastKeyLockUp(tagInfo.keyId, tagInfo.tagId, true) then
            Notify(MessageConst.SHOW_POP_UP, {
                content = string.format(Language.LUA_CONTINGENCY_CONTRACT_SELECT_CONFIRM_UNJOINE_KEY, keyInfo.keyName),
                warningContent = Language.LUA_CONTINGENCY_CONTRACT_SELECT_CONFIRM_UNJOINE_KEY_WARNING,
                onConfirm = function()
                    self:_SetTagJoinAndRefreshUI(tagInfo, false)
                    local listCount = #self.m_joinedTagInfos
                    if listCount <= 0 then
                        Notify(MessageConst.SHOW_TOAST, Language.LUA_CONTINGENCY_CONTRACT_SELECT_CLEAR_ALL_TOAST)
                    end
                end
            })
        else
            self:_SetTagJoinAndRefreshUI(tagInfo, false)
        end
    end)
    if DeviceInfo.usingController then
        cell.naviDeco.onIsNaviTargetChanged = function(isTarget)
            if isTarget then
                self.m_curNaviTagEffectCellIndex = luaIndex
                if self.m_isNaviTagCell then
                    self.m_isNaviTagCell = false
                    self:_ChangeAreaOperateEnable(false)
                end
            end
        end
    end
end

ContingencySelectTagCtrl._GetTagEffectCell = HL.Method(HL.Number).Return(HL.Opt(HL.Any)) << function(self, luaIndex)
    if luaIndex <= 0 or not self.m_getTagEffectCellFunc then
        return nil
    end
    local obj = self.view.tagEffectList:Get(CSIndex(luaIndex))
    if not obj then
        return nil
    end
    return self.m_getTagEffectCellFunc(obj)
end

ContingencySelectTagCtrl._ScrollTagEffectListToIndex = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, luaIndex, fastMode)
    if luaIndex <= 0 or luaIndex > #self.m_joinedTagInfos then
        return
    end
    self.view.tagEffectList:ScrollToIndex(CSIndex(luaIndex), fastMode == true, CS.Beyond.UI.UIScrollList.ScrollAlignType.Top)
end

ContingencySelectTagCtrl._SetTagEffectControllerScrollEnabled = HL.Method(HL.Boolean) << function(self, enabled)
    if self.view.tagEffectListRect then
        self.view.tagEffectListRect.controllerScrollEnabled = enabled and self.view.inputGroup.internalEnabled
    end
end



ContingencySelectTagCtrl._RefreshCurScoreUI = HL.Method() << function(self)
    local preScore = self.m_basicInfo.preScore
    local curScore = self.m_basicInfo.curScore
    self.m_basicInfo.preScore = curScore
    if curScore == 0 then
        self.view.curScore.text = "--"
    else
        self.view.curScore.text = curScore
    end

    if curScore < self.m_basicInfo.warningScore1 then
        self.view.entryNode:SetState("Normal")
        if preScore >= self.m_basicInfo.warningScore1 then
            self.view.warningNode:ClearTween()
            self.view.entryNodeAnimationWrapper:SampleClipAtPercent("contingencyselecttag_entryloop", 1)
        end
    elseif curScore < self.m_basicInfo.warningScore2 then
        self.view.entryNode:SetState("Warning1")
        if preScore < self.m_basicInfo.warningScore1 then
            AudioAdapter.PostEvent("Au_UI_Event_CCReignite_WarningA")
            self.view.entryNodeAnimationWrapper:Play("contingencyselecttag_entryloop")
        end
    else
        self.view.entryNode:SetState("Warning2")
        if preScore < self.m_basicInfo.warningScore1 then
            self.view.entryNodeAnimationWrapper:Play("contingencyselecttag_entryloop")
        end
        if preScore < self.m_basicInfo.warningScore2 then
            AudioAdapter.PostEvent("Au_UI_Event_CCReignite_WarningB")
        end
    end
end

ContingencySelectTagCtrl._ChangeTagSelect = HL.Method(HL.Table, HL.Boolean, HL.Boolean) << function(self, tagInfo, isSelect, isFromTagClick)
    local luaIndex = tagInfo.cellIndex
    if luaIndex == self.m_curSelectTagCellIndex and isSelect then
        return
    end

    if self.m_curSelectTagCellIndex > 0 then

        self.view.selectTagArrow.gameObject:SetActive(false)

        local column, row = ContingencyContractUtils.GetColumnRow(self.m_curSelectTagCellIndex)
        local oldTagInfo = self.m_tagInfos[column][row]
        local tagEffectIndex = oldTagInfo.tagEffectIndex

        local cell = self:_GetTagCell(self.m_curSelectTagCellIndex)
        if cell and tagEffectIndex > 0 then

            local tagEffectCell = self:_GetTagEffectCell(tagEffectIndex)
            if tagEffectCell and isSelect then

                tagEffectCell.animationWrapper:ClearTween()
                tagEffectCell.animationWrapper:SampleClipAtPercent("tageffectcell_slc", 1)
            end
            if tagEffectCell then
                tagEffectCell.stateController:SetState("Normal")
            end
            cell.animationWrapper:Play("tageffectcell_slc_leftout")
        end
        self.m_curSelectTagCellIndex = -1
    end
    if isSelect then

        local cell = self:_GetTagCell(luaIndex)
        if not cell then
            return
        end
        self.view.selectTagArrow.gameObject:SetActive(true)


        local arrowTrans = self.view.selectTagArrow.transform
        local targetPos = cell.rectTransform.anchoredPosition + self.view.config.SELECT_ARROW_POST_OFFSET
        arrowTrans.anchoredPosition = targetPos

        if isFromTagClick then
            local tagEffectIndex = tagInfo.tagEffectIndex
            if tagEffectIndex > 0 then
                self:_ScrollTagEffectListToIndex(tagEffectIndex, true)
                local tagEffectCell = self:_GetTagEffectCell(tagEffectIndex)
                if tagEffectCell then
                    tagEffectCell.animationWrapper:ClearTween()
                    tagEffectCell.animationWrapper:Play("tageffectcell_slc")
                    tagEffectCell.stateController:SetState("Select")
                end
            end
        end

        self.m_curSelectTagCellIndex = luaIndex
        self.m_curNaviTagCellIndex = luaIndex
        self:_UpdateTagSelectRemoveNode(tagInfo)
    end
end

ContingencySelectTagCtrl._ClearTagSelect = HL.Method() << function(self)
    if self.m_curSelectTagCellIndex > 0 then
        local cell = self:_GetTagCell(self.m_curSelectTagCellIndex)
        if cell and cell.selectRemoveNode.gameObject.activeSelf then
            cell.animationWrapper:Play("tageffectcell_slc_leftout")
        end
    end
    self.view.selectTagArrow.gameObject:SetActive(false)
    self.m_curSelectTagCellIndex = -1
end

ContingencySelectTagCtrl._ChangeTagJoin = HL.Method(HL.Table, HL.Boolean) << function(self, tagInfo, isJoin)
    local cell = self:_GetTagCell(tagInfo.cellIndex)
    if not cell then
        return
    end
    local curIsJoined = self.m_joinedTagInfosMap[tagInfo.tagId] ~= nil
    if curIsJoined == isJoin then
        return
    end

    cell.stateController:SetState(isJoin and "Joined" or "NotJoin")
    if self.m_isNaviTagCell and self.m_curNaviTagCellIndex == tagInfo.cellIndex then
        cell.controllerDeleteBtn.enabled = isJoin
    end
    if isJoin then
        self.m_joinedTagInfosMap[tagInfo.tagId] = tagInfo
        tagInfo.joinedSeq = self.m_joinedSeqGlobal
        self.m_joinedSeqGlobal = self.m_joinedSeqGlobal + 1
        self.m_basicInfo.curScore = self.m_basicInfo.curScore + tagInfo.level
    else
        self.m_joinedTagInfosMap[tagInfo.tagId] = nil
        tagInfo.joinedSeq = 0
        tagInfo.tagEffectIndex = -1
        self.m_basicInfo.curScore = self.m_basicInfo.curScore - tagInfo.level
        if tagInfo.cellIndex == self.m_curSelectTagCellIndex then
            self:_ClearTagSelect()
        end
    end
    if not string.isEmpty(tagInfo.keyId) then
        cell.keyLightColorImg.gameObject:SetActive(not isJoin)
    end
    self:_UpdateTagSelectRemoveNode(tagInfo)
end

ContingencySelectTagCtrl._ChangeTagLock = HL.Method(HL.Table, HL.String, HL.Boolean) << function(self, tagInfo, keyId, isLock)
    local lockIndex
    for i, lockId in pairs(tagInfo.lockIds) do
        if lockId == keyId then
            lockIndex = i
            break
        end
    end
    if not lockIndex then
        logger.error("ContingencySelectTagCtrl._ChangeTagLock：没找到对应keyId")
        return
    end


    local cell = self:_GetTagCell(tagInfo.cellIndex)
    if not cell then
        return
    end
    local lockCount = #tagInfo.lockIds
    local lockNode = cell.lockNode
    local targetLock = lockNode["lock" .. lockIndex]

    if isLock then
        if not targetLock.gameObject.activeSelf then
            tagInfo.useKeyCount = tagInfo.useKeyCount - 1
            if tagInfo.useKeyCount == 0 then
                lockNode.loopEffectLine.gameObject:SetActive(false)
            end
            if lockCount == 1 then
                lockNode.stateController:SetState("SingleLock")
            else
                lockNode.stateController:SetState("DoubleLock" .. lockIndex)
            end
        end
    else
        if targetLock.gameObject.activeSelf then
            tagInfo.useKeyCount = tagInfo.useKeyCount + 1
            lockNode.loopEffectLine.gameObject:SetActive(true)
            if lockCount == 1 then
                lockNode.stateController:SetState("SingleLockUnlock")
                cell.animationWrapper:Play("contingencyselecttag_tagcell_locktounlock_all")
            else
                lockNode.stateController:SetState("DoubleLockUnlock" .. lockIndex)

                if tagInfo.useKeyCount == lockCount then
                    lockNode.stateController:SetState("AllUnlock")
                    cell.animationWrapper:Play("contingencyselecttag_tagcell_locktounlock_all")
                else
                    cell.animationWrapper:Play("contingencyselecttag_tagcell_locktounlock")
                end
            end
        end
    end
end

ContingencySelectTagCtrl._ChangeTagConflict = HL.Method(HL.Table, HL.Boolean) << function(self, tagInfo, isConflict)
    local cell = self:_GetTagCell(tagInfo.cellIndex)
    if not cell then
        return
    end
    cell.stateController:SetState(isConflict and "Conflict" or "NotConflict")
    if cell.arrowNode then
        local conflictInfo = self.m_tagConflictInfos[tagInfo.conflictId]
        local isJoined = conflictInfo.curJoinedTag == tagInfo
        cell.arrowNode.stateController:SetState(isJoined and "Joined" or "NotJoined")
    end
end

ContingencySelectTagCtrl._RefreshTipsLockCell = HL.Method(HL.Table, HL.Any, HL.Forward("UIListCache")) << function(self, keyInfo, inCell, keyCellCache)

    local lockCell = inCell
    lockCell.lockInfoNode.color = keyInfo.color

    local tipsKeyInfos = {}
    local tempTipsKeyInfoMap = {}
    for i, tagInfo in pairs(keyInfo.keyTagsList) do
        if tagInfo.isUnlockByStage then
            if string.isEmpty(tagInfo.romanNumSuffix) then
                table.insert(tipsKeyInfos, {
                    groupId = tagInfo.groupId,
                    tagInfo = tagInfo,
                })
            else
                local tipsKeyInfo = tempTipsKeyInfoMap[tagInfo.groupId]
                if not tipsKeyInfo then
                    tipsKeyInfo = {
                        groupId = tagInfo.groupId,
                        groupTagInfos = {}
                    }
                    tempTipsKeyInfoMap[tagInfo.groupId] = tipsKeyInfo
                end
                table.insert(tipsKeyInfo.groupTagInfos, tagInfo)
            end
        end
    end
    for groupId, groupInfo in pairs(tempTipsKeyInfoMap) do
        table.sort(groupInfo.groupTagInfos, function(a, b)
            return a.level < b.level
        end)
        table.insert(tipsKeyInfos, groupInfo)
    end
    table.sort(tipsKeyInfos, function(a, b)
        return a.groupId < b.groupId
    end)

    local joinedKeyCount = 0
    keyCellCache:Refresh(#tipsKeyInfos, function(cell, luaIndex)


        local keyCell = cell
        keyCell.keyIcon.color = keyInfo.color
        local tipsKeyInfo = tipsKeyInfos[luaIndex]
        if tipsKeyInfo.tagInfo then

            local keyTagInfo = tipsKeyInfo.tagInfo
            if keyInfo.curJoinedKeyTags[keyTagInfo.tagId] ~= nil then
                joinedKeyCount = joinedKeyCount + 1
                keyCell.keyNameTxt.text = string.format(Language.LUA_CONTINGENCY_CONTRACT_SELECT_HIGH_LIGHT_TEXT_COLOR, keyTagInfo.tagFullName)
            else
                keyCell.keyNameTxt.text = keyTagInfo.tagFullName
            end
        else

            local keyNameSuffix = ""
            local hasKeyJoined = false
            for i, keyTagInfo in pairs(tipsKeyInfo.groupTagInfos) do
                if i ~= 1 then
                    keyNameSuffix = keyNameSuffix .. '/'
                end
                if keyInfo.curJoinedKeyTags[keyTagInfo.tagId] ~= nil then
                    hasKeyJoined = true
                    joinedKeyCount = joinedKeyCount + 1
                    keyNameSuffix = keyNameSuffix .. string.format(Language.LUA_CONTINGENCY_CONTRACT_SELECT_HIGH_LIGHT_TEXT_COLOR, keyTagInfo.romanNumSuffix)
                else
                    keyNameSuffix = keyNameSuffix .. keyTagInfo.romanNumSuffix
                end
            end
            local keyTagName = hasKeyJoined and
                string.format(Language.LUA_CONTINGENCY_CONTRACT_SELECT_HIGH_LIGHT_TEXT_COLOR, tipsKeyInfo.groupTagInfos[1].tagName) or
                tipsKeyInfo.groupTagInfos[1].tagName
            keyCell.keyNameTxt.text = string.format(Language.LUA_CONTINGENCY_CONTRACT_TAG_NAME_FORMAT, keyTagName, keyNameSuffix)
        end
    end)
    local needUseKeyCount = 1
    joinedKeyCount = math.min(joinedKeyCount, needUseKeyCount)
    lockCell.keyJoinedCountTxt.text = string.format("%d/%d", joinedKeyCount, needUseKeyCount)
    lockCell.stateController:SetState(joinedKeyCount >= needUseKeyCount and "Unlock" or "Lock")
end

ContingencySelectTagCtrl._RefreshUIWhenTagJoinChange = HL.Method() << function(self)
    self:_SortAndRefreshJoinedTagInfos()
    self:_RefreshCurScoreUI()
end

ContingencySelectTagCtrl._TryPlayTagEffectCellInAni = HL.Method() << function(self)
    local listCount = #self.m_joinedTagInfos
    if listCount <= 0 then
        return
    end
    LuaUpdate:Remove(self.m_tagEffectCellInAniUpdateKey)

    local visibleIndices = {}
    local showRange = self.view.tagEffectList:GetShowRange()
    if showRange.x < 0 or showRange.y < showRange.x then
        return
    end
    for csIndex = showRange.x, showRange.y do
        local luaIndex = LuaIndex(csIndex)
        if luaIndex >= 1 and luaIndex <= listCount then
            table.insert(visibleIndices, luaIndex)
        end
    end
    local count = #visibleIndices
    if count <= 0 then
        return
    end
    local curIndex = 1
    local intervalTime = self.view.config.TAG_EFFECT_IN_ANI_INTERVAL
    for i = 1, count do

        local cell = self:_GetTagEffectCell(visibleIndices[i])
        if cell then
            cell.animationWrapper:SampleToInAnimationBegin()
        end
    end
    self.m_tagEffectCellInAniUpdateKey = LuaUpdate:Add("Tick", function(deltaTime)
        intervalTime = intervalTime + deltaTime
        if intervalTime >= self.view.config.TAG_EFFECT_IN_ANI_INTERVAL then

            local cell = self:_GetTagEffectCell(visibleIndices[curIndex])
            if cell then
                cell.animationWrapper:PlayInAnimation()
            end

            curIndex = curIndex + 1
            if curIndex > count then
                LuaUpdate:Remove(self.m_tagEffectCellInAniUpdateKey)
                self.m_tagEffectCellInAniUpdateKey = -1
            end
        end
    end)
end

ContingencySelectTagCtrl._TryStopTagEffectCellInAni = HL.Method() << function(self)
    if self.m_tagEffectCellInAniUpdateKey <= 0 then
        return
    end
    LuaUpdate:Remove(self.m_tagEffectCellInAniUpdateKey)
    local showRange = self.view.tagEffectList:GetShowRange()
    if showRange.x < 0 or showRange.y < showRange.x then
        self.m_tagEffectCellInAniUpdateKey = -1
        return
    end
    for csIndex = showRange.x, showRange.y do
        local luaIndex = LuaIndex(csIndex)

        local cell = self:_GetTagEffectCell(luaIndex)
        if cell then
            cell.animationWrapper:SampleToInAnimationEnd()
        end
    end
    self.m_tagEffectCellInAniUpdateKey = -1
end



ContingencySelectTagCtrl._SetTagJoinAndRefreshUI = HL.Method(HL.Table, HL.Boolean) << function(self, tagInfo, setJoin)
    self:_SetTagJoin(tagInfo, setJoin, false)
    self:_RefreshUIWhenTagJoinChange()
    if not setJoin then
        self:_TryPlayTagEffectCellInAni()
    end
end

ContingencySelectTagCtrl._QuickUnlockTag = HL.Method(HL.Table) << function(self, tagInfo)
    local hasScoreNotValidTag = false
    for index, lockId in ipairs(tagInfo.lockIds) do
        local keyInfo = self.m_tagKeyInfos[lockId]
        if lume.count(keyInfo.curJoinedKeyTags) <= 0 then
            local curMinLevel = 4
            local curMinLevelIndex = 0
            for i, keyTagInfo in ipairs(keyInfo.keyTagsList) do
                local checkKeyTagLevel = keyTagInfo.level
                local keyIsOpen = tagInfo.isUnlockByStage
                if keyIsOpen and checkKeyTagLevel < curMinLevel then
                    curMinLevel = checkKeyTagLevel
                    curMinLevelIndex = i
                    if checkKeyTagLevel == 1 then
                        break
                    end
                end
            end
            if curMinLevelIndex > 0 then
                local keyTagInfo = keyInfo.keyTagsList[curMinLevelIndex]
                if keyTagInfo.unlockScore > 0 and keyTagInfo.unlockScore <= self.m_basicInfo.historyMaxScore then
                    self:_SetTagJoin(keyTagInfo, true, false)
                else
                    hasScoreNotValidTag = true
                end
            end
        end
    end
    if hasScoreNotValidTag then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CONTINGENCY_CONTRACT_SELECT_TAG_NOT_VALID_TOAST)
    end

    self:_RefreshUIWhenTagJoinChange()
end

ContingencySelectTagCtrl._SetTagJoin = HL.Method(HL.Table, HL.Boolean, HL.Boolean) << function(self, tagInfo, setJoin, isInit)
    logger.info(string.format("ContingencySelectTagCtrl：SetJoinTag [%s] [id:%d] [c%d_r%d]", setJoin, tagInfo.tagId, tagInfo.column, tagInfo.row))

    local keyId = tagInfo.keyId
    local keyInfo = self.m_tagKeyInfos[keyId]

    if setJoin then

        local conflictId = tagInfo.conflictId
        local conflictInfo = self.m_tagConflictInfos[conflictId]
        local conflictTagKeyId
        if conflictInfo then

            if conflictInfo.curJoinedTag ~= nil then
                local curConflictTagInfo = conflictInfo.curJoinedTag
                conflictTagKeyId = curConflictTagInfo.keyId
                local conflictKeyInfo = self.m_tagKeyInfos[conflictTagKeyId]
                if conflictKeyInfo then

                    if keyId ~= conflictTagKeyId then
                        self:_TryChangeKeyJoin(conflictKeyInfo, curConflictTagInfo, false, not isInit)
                    else
                        conflictKeyInfo.curJoinedKeyTags[curConflictTagInfo.tagId] = nil
                    end
                end

                self:_ChangeTagJoin(curConflictTagInfo, false)

                for i, lockId in ipairs(curConflictTagInfo.lockIds) do
                    local lockInfo = self.m_tagKeyInfos[lockId]
                    lockInfo.curJoinedLockTags[curConflictTagInfo.tagId] = nil
                end
            end
            self:_TryChangeConflictJoin(conflictInfo, tagInfo, true)
        end

        self:_TryChangeLockJoin(tagInfo, true)

        if keyInfo then
            local needHint = not isInit and (not conflictTagKeyId or keyId ~= conflictTagKeyId)
            self:_TryChangeKeyJoin(keyInfo, tagInfo, true, needHint)
        end

        self:_ChangeTagJoin(tagInfo, true)
    else

        local conflictId = tagInfo.conflictId
        local conflictInfo = self.m_tagConflictInfos[conflictId]
        if conflictInfo then
            self:_TryChangeConflictJoin(conflictInfo, tagInfo, false)
        end

        self:_TryChangeLockJoin(tagInfo, false)

        if keyInfo then
            self:_TryChangeKeyJoin(keyInfo, tagInfo, false, not isInit)
        end

        self:_ChangeTagJoin(tagInfo, false)
    end
end

ContingencySelectTagCtrl._OnClickOpenedTag = HL.Method(HL.Table, HL.Boolean) << function(self, tagInfo, controllerQuickUnjoin)
    logger.info("ContingencySelectTagCtrl：点击openedBtn")
    ActivityUtils.setCcTagRead(tagInfo.tagId)

    local isTagLockByTag = self:_IsTagLockByTag(tagInfo)
    if not isTagLockByTag then

        if self.view.tipsNodeRoot.gameObject.activeSelf then
            self.view.tagTipsNode.autoCloseArea:TryCloseSelf()
        end
    end

    local preIsSelect = tagInfo.cellIndex == self.m_curSelectTagCellIndex
    if not preIsSelect and not controllerQuickUnjoin then
        self:_ChangeTagSelect(tagInfo, true, true)
    end

    if isTagLockByTag then

        logger.info("ContingencySelectTagCtrl：词条锁着呢")
        AudioAdapter.PostEvent("Au_UI_Button_CCReigniteNodeLocked")
        self:_ShowTagTips(tagInfo)
        return
    end

    local tagId = tagInfo.tagId
    local keyId = tagInfo.keyId

    local tagIsJoined = self.m_joinedTagInfosMap[tagInfo.tagId] ~= nil
    if tagIsJoined then

        if not preIsSelect and not controllerQuickUnjoin then

            local tagEffectIndex = tagInfo.tagEffectIndex
            if tagEffectIndex > 0 then
                self:_ScrollTagEffectListToIndex(tagEffectIndex, true)
            end
            self:_RefreshTagCellOpenBtnHoverText(tagInfo)
            return
        end

        if self:_IsLastKeyLockUp(keyId, tagId, true) then
            local keyInfo = self.m_tagKeyInfos[keyId]
            Notify(MessageConst.SHOW_POP_UP, {
                content = string.format(Language.LUA_CONTINGENCY_CONTRACT_SELECT_CONFIRM_UNJOINE_KEY, keyInfo.keyName),
                warningContent = Language.LUA_CONTINGENCY_CONTRACT_SELECT_CONFIRM_UNJOINE_KEY_WARNING,
                onConfirm = function()
                    self:_SetTagJoinAndRefreshUI(tagInfo, false)
                end
            })
        else
            AudioAdapter.PostEvent("Au_UI_Button_CCReigniteNode_cancel")
            self:_SetTagJoinAndRefreshUI(tagInfo, false)
        end
    else


        local conflictId = tagInfo.conflictId
        local conflictInfo = self.m_tagConflictInfos[conflictId]
        local curJoinedConflictTag
        if conflictInfo and conflictInfo.curJoinedTag ~= nil then
            curJoinedConflictTag = conflictInfo.curJoinedTag
        end

        if curJoinedConflictTag and keyId ~= curJoinedConflictTag.keyId and
            self:_IsLastKeyLockUp(curJoinedConflictTag.keyId, curJoinedConflictTag.tagId, true)
        then
            local keyInfo = self.m_tagKeyInfos[keyId]
            Notify(MessageConst.SHOW_POP_UP, {
                content = string.format(Language.LUA_CONTINGENCY_CONTRACT_SELECT_CONFIRM_UNJOINE_KEY, keyInfo.keyName),
                warningContent = Language.LUA_CONTINGENCY_CONTRACT_SELECT_CONFIRM_UNJOINE_KEY_WARNING,
                onConfirm = function()
                    self:_SetTagJoinAndRefreshUI(tagInfo, true)
                end
            })
        else
            if not string.isEmpty(keyId) or #tagInfo.lockIds > 0 then

                AudioAdapter.PostEvent("Au_UI_Button_CCReigniteNode")
            end
            self:_SetTagJoinAndRefreshUI(tagInfo, true)
        end
    end
    self:_RefreshTagCellOpenBtnHoverText(tagInfo)
end

ContingencySelectTagCtrl._OnCLickNotOpenTag = HL.Method(HL.Table) << function(self, tagInfo)
    logger.info("ContingencySelectTagCtrl：点击notOpenBtn")
    self:_ChangeTagSelect(tagInfo, true, true)
    self:_ShowTagTips(tagInfo)
end

ContingencySelectTagCtrl._TryChangeKeyJoin = HL.Method(HL.Table, HL.Table, HL.Boolean, HL.Boolean) << function(self, keyInfo, tagInfo, isJoin, needHint)
    local tagId = tagInfo.tagId
    local keyId = keyInfo.keyId
    if isJoin then
        if keyInfo.curJoinedKeyTags[tagId] ~= nil then
            return
        end

        for lockTagId, lockTagInfo in pairs(keyInfo.lockTags) do
            self:_ChangeTagLock(lockTagInfo, keyId, false)
        end
        keyInfo.curJoinedKeyTags[tagId] = tagInfo

        if needHint and lume.count(keyInfo.curJoinedKeyTags) == 1 then
            self.m_newJoinedKeyInfo = keyInfo
            self:_UpdateOutsideDotInfo()
            AudioAdapter.PostEvent("Au_UI_Event_CCReigniteNodeUnlock")
            Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_CONTINGENCY_CONTRACT_SELECT_UNLOCK_KEY, keyInfo.lockName))
        end
    else
        if keyInfo.curJoinedKeyTags[tagId] == nil then
            return
        end

        if self:_IsLastKeyLockUp(keyId, tagId, false) then
            for lockTagId, lockTagInfo in pairs(keyInfo.lockTags) do
                self:_ChangeTagJoin(lockTagInfo, false)
                self:_TryChangeLockJoin(lockTagInfo, false)
                self:_ChangeTagLock(lockTagInfo, keyId, true)
                local conflictInfo = self.m_tagConflictInfos[lockTagInfo.conflictId]
                if conflictInfo then
                    self:_TryChangeConflictJoin(conflictInfo, lockTagInfo, false)
                end
            end
            keyInfo.curJoinedLockTags = {}
        end
        keyInfo.curJoinedKeyTags[tagId] = nil

        if needHint and lume.count(keyInfo.curJoinedKeyTags) <= 0 then
            if self.m_outsideLockDotInfo ~= nil and self.m_outsideLockDotInfo.keyId == keyInfo.keyId then
                self.m_outsideLockDotInfo = nil
                self.view.outsideDotNode:SetState("RedDot")
            end
        end
    end
end

ContingencySelectTagCtrl._TryChangeConflictJoin = HL.Method(HL.Table, HL.Table, HL.Boolean) << function(self, conflictInfo, tagInfo, isJoin)
    local tagId = tagInfo.tagId
    if isJoin then
        if conflictInfo.curJoinedTag == tagInfo then
            return
        end

        conflictInfo.curJoinedTag = tagInfo
        for i, conflictTagInfo in pairs(conflictInfo.conflictTagsList) do
            if conflictTagInfo.tagId ~= tagId then
                self:_ChangeTagConflict(conflictTagInfo, true)
            else
                self:_ChangeTagConflict(conflictTagInfo, false)
            end
        end
    else
        if conflictInfo.curJoinedTag ~= tagInfo then
            return
        end

        conflictInfo.curJoinedTag = nil
        for i, conflictTagInfo in pairs(conflictInfo.conflictTagsList) do
            self:_ChangeTagConflict(conflictTagInfo, false)
        end
    end
end

ContingencySelectTagCtrl._TryChangeLockJoin = HL.Method(HL.Table, HL.Boolean) << function(self, tagInfo, isJoin)
    local tagId = tagInfo.tagId
    if isJoin then
        if #tagInfo.lockIds > 0 then
            for i, lockId in pairs(tagInfo.lockIds) do
                local lockInfo = self.m_tagKeyInfos[lockId]
                local isJoined = lockInfo.curJoinedLockTags[tagId] ~= nil
                if not isJoined then
                    lockInfo.curJoinedLockTags[tagId] = tagInfo
                end
            end
        end
    else
        if #tagInfo.lockIds > 0 then
            for i, lockId in pairs(tagInfo.lockIds) do
                local lockInfo = self.m_tagKeyInfos[lockId]
                local isJoined = lockInfo.curJoinedLockTags[tagId] ~= nil
                if isJoined then
                    lockInfo.curJoinedLockTags[tagId] = nil
                end
            end
        end
    end
end

ContingencySelectTagCtrl._IsTagLockByTag = HL.Method(HL.Table).Return(HL.Boolean) << function(self, tagInfo)
    for i, lockId in pairs(tagInfo.lockIds) do
        local lockInfo = self.m_tagKeyInfos[lockId]
        if lume.count(lockInfo.curJoinedKeyTags) <= 0 then
            return true
        end
    end
    return false
end

ContingencySelectTagCtrl._IsLastKeyLockUp = HL.Method(HL.String, HL.Number, HL.Boolean).Return(HL.Boolean)
    << function(self, keyId, tagId, checkDependLock)
    local keyInfo = self.m_tagKeyInfos[keyId]
    if keyInfo and keyInfo.curJoinedKeyTags[tagId] ~= nil and lume.count(keyInfo.curJoinedKeyTags) == 1 then
        local hasDependLock = lume.count(keyInfo.curJoinedLockTags) >= 1
        if not checkDependLock or hasDependLock then
            return true
        end
    end
    return false
end

ContingencySelectTagCtrl._SortAndRefreshJoinedTagInfos = HL.Method() << function(self)
    self.m_joinedTagInfos = {}
    for i, tagInfo in pairs(self.m_joinedTagInfosMap) do
        table.insert(self.m_joinedTagInfos, tagInfo)
    end
    self:_SortAndRefreshTagEffectList()
end

ContingencySelectTagCtrl._RefreshTagEffectIndices = HL.Method() << function(self)
    for luaIndex, tagInfo in ipairs(self.m_joinedTagInfos) do
        tagInfo.tagEffectIndex = luaIndex
    end
end

ContingencySelectTagCtrl._SortAndRefreshTagEffectList = HL.Method(HL.Opt(HL.Table, HL.Boolean)) << function(self, sortData, isIncremental)
    local isFromTagJoinChange = sortData == nil
    sortData = sortData or self.view.sortNode:GetCurSortData()
    if isIncremental == nil then
        isIncremental = self.view.sortNode.isIncremental
    end


    table.sort(self.m_joinedTagInfos, Utils.genSortFunction(sortData.keys, isIncremental))
    self:_RefreshTagEffectIndices()
    local listCount = #self.m_joinedTagInfos
    self.view.rightTagEffectListNode:SetState(listCount <= 0 and "Empty" or "NotEmpty")
    self.view.clearAllTagBtn.gameObject:SetActive(listCount > 0)
    self:_TryStopTagEffectCellInAni()
    self.view.tagEffectList:UpdateCount(listCount)


    if self.m_curSelectTagCellIndex > 0 then
        local column, row = ContingencyContractUtils.GetColumnRow(self.m_curSelectTagCellIndex)
        local curSelTagInfo = self.m_tagInfos[column][row]
        local tagEffectIndex = curSelTagInfo.tagEffectIndex
        if tagEffectIndex > 0 then

            self:_ScrollTagEffectListToIndex(tagEffectIndex, true)
            local tagEffectCell = self:_GetTagEffectCell(tagEffectIndex)
            if tagEffectCell and isFromTagJoinChange then
                tagEffectCell.animationWrapper:Play("tageffectcell_slc")

            end
        end
    end


    if DeviceInfo.usingController then
        local canSwitchArea = listCount > 0
        if not canSwitchArea then
            if not self.m_isNaviTagCell then
                self:_SetNaviTagCell(true)
            end
        else
            if not self.m_isNaviTagCell then
                self:_SetNaviTagEffectCell()
            end
        end
        self.view.switchAreaKeyHint.gameObject:SetActive(canSwitchArea)
    end
end

ContingencySelectTagCtrl._ClearAllTag = HL.Method(HL.Boolean, HL.Boolean) << function(self, refreshUI, showToast)
    local tempTagInfoMap = self.m_joinedTagInfosMap
    for tagId, tagInfo in pairs(self.m_joinedTagInfosMap) do
        tempTagInfoMap[tagId] = tagInfo
    end

    for tagId, tagInfo in pairs(tempTagInfoMap) do
        self:_ChangeTagJoin(tagInfo, false)
    end
    self:_ClearTagSelect()

    for keyId, keyInfo in pairs(self.m_tagKeyInfos) do
        keyInfo.curJoinedKeyTags = {}
        keyInfo.curJoinedLockTags = {}
        for lockTagId, lockTagInfo in pairs(keyInfo.lockTags) do
            self:_ChangeTagLock(lockTagInfo, keyId, true)
        end
    end
    for conflictId, conflictInfo in pairs(self.m_tagConflictInfos) do
        if conflictInfo.curJoinedTag ~= nil then
            conflictInfo.curJoinedTag = nil
            for i, conflictTagInfo in pairs(conflictInfo.conflictTagsList) do
                self:_ChangeTagConflict(conflictTagInfo, false)
            end
        end
    end
    if self.m_curSelectTagCellIndex > 0 then
        local column, row = ContingencyContractUtils.GetColumnRow(self.m_curSelectTagCellIndex)
        local tagInfo = self.m_tagInfos[column][row]
        self:_ChangeTagSelect(tagInfo, false, false)
    end

    self.m_outsideLockDotInfo = nil
    self.view.outsideDotNode:SetState("RedDot")
    if refreshUI then
        if showToast then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_CONTINGENCY_CONTRACT_SELECT_CLEAR_ALL_TOAST)
        end
        self:_RefreshUIWhenTagJoinChange()
        if DeviceInfo.usingController then
            if not self.m_isNaviTagCell then
                self:_SetNaviTagCell(true)
            end
        end

        if self.m_isNaviTagCell and self.m_curNaviTagCellIndex > 0 then
            local col, row = ContingencyContractUtils.GetColumnRow(self.m_curNaviTagCellIndex)
            local tagInfo = self.m_tagInfos[col][row]
            self:_RefreshTagCellOpenBtnHoverText(tagInfo)
        end
    end
end

ContingencySelectTagCtrl._ApplyShareTag = HL.Method(HL.Table, HL.Boolean, HL.Boolean) << function(self, tagIds, keepJoinOrder, autoScroll)
    if tagIds == nil or #tagIds <= 0 then
        self:_ClearAllTag(true, false)
        return
    end
    self:_ClearAllTag(false, false)
    local applyTagIds = {}
    for _, tagId in ipairs(tagIds) do
        table.insert(applyTagIds, tagId)
    end
    if not keepJoinOrder then

        table.sort(applyTagIds, function(tagIdA, tagIdB)
            local tagInfoA = self.m_allTagInfosMap[tagIdA]
            local tagInfoB = self.m_allTagInfosMap[tagIdB]
            return tagInfoA.cellIndex > tagInfoB.cellIndex
        end)
    end
    for _, tagId in ipairs(applyTagIds) do
        self:_SetTagJoin(self.m_allTagInfosMap[tagId], true, true)
    end

    self:_SortAndRefreshJoinedTagInfos()
    self:_RefreshCurScoreUI()
    if self.m_isNaviTagCell and self.m_curNaviTagCellIndex > 0 then
        local col, row = ContingencyContractUtils.GetColumnRow(self.m_curNaviTagCellIndex)
        local tagInfo = self.m_tagInfos[col][row]
        self:_RefreshTagCellOpenBtnHoverText(tagInfo)
    end

    if autoScroll then
        if #self.m_joinedTagInfos > 0 then
            local tagInfo = self.m_joinedTagInfos[1]
            local needNaviCellIndex = tagInfo.cellIndex
            local tagCell = self:_GetTagCell(needNaviCellIndex)
            if tagCell then
                self.view.tagList:AutoScrollToRectTransform(tagCell.transform, true)
                if DeviceInfo.usingController then
                    self.m_curNaviTagCellIndex = needNaviCellIndex
                    self:_SetNaviTagCell(false)
                end
            end
        end
    end
end

ContingencySelectTagCtrl._ShowTagTips = HL.Method(HL.Table) << function(self, tagInfo)
    if self.m_curOpenTipsTagIndex == tagInfo.cellIndex then
        self.view.tagTipsNode.autoCloseArea:CloseSelf()
        self:_HideTagTips()
        return
    end
    self.m_curOpenTipsTagIndex = tagInfo.cellIndex


    local tagCell = self:_GetTagCell(tagInfo.cellIndex)
    if not tagCell then
        return
    end
    local tipsNode = self.view.tagTipsNode
    local preIsActive = self.view.tipsNodeRoot.gameObject.activeSelf
    tipsNode.autoCloseArea:OpenSelf()
    if preIsActive then
        self.view.tipsNodeRoot:SampleToInAnimationEnd()
    else
        self.view.tipsNodeRoot:PlayInAnimation()
    end


    local isOpen = tagInfo.isUnlockByStage
    local canPreview = tagInfo.canPreview

    tipsNode.tagIcon:LoadSprite(UIConst.UI_SPRITE_CONTINGENCY_CONTRACT_BUFF, tagInfo.icon)
    tipsNode.tagIconShadow:LoadSprite(UIConst.UI_SPRITE_CONTINGENCY_CONTRACT_BUFF, tagInfo.icon)
    tipsNode.levelTxt.text = tagInfo.level
    tipsNode.stateController:SetState("Level" .. tagInfo.level)
    tipsNode.rewardIcon.gameObject:SetActive(tagInfo.hasFirstPassReward and isOpen)
    if isOpen or canPreview then
        tipsNode.tagNameTxt.text = tagInfo.tagFullName
        tipsNode.descTxt.text = tagInfo.desc
    else
        tipsNode.tagNameTxt.text = Language.LUA_CONTINGENCY_CONTRACT_SELECT_HIDE_TAG_NAME
        tipsNode.descTxt.text = Language.LUA_CONTINGENCY_CONTRACT_SELECT_HIDE_TAG_DESC
    end
    if not isOpen then

        self.m_tipsTagUnlockTimeCor = self:_ClearCoroutine(self.m_tipsTagUnlockTimeCor)
        local leftTime = tagInfo.openTime - DateTimeUtils.GetCurrentTimestampBySeconds()
        tipsNode.unlockTimeTxt.text = UIUtils.getLeftTime(leftTime)
        self.m_tipsTagUnlockTimeCor = self:_StartCoroutine(function()
            while true do
                coroutine.wait(1)
                local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
                local leftTime = tagInfo.openTime - curTime
                tipsNode.unlockTimeTxt.text = UIUtils.getLeftTime(leftTime)
                if leftTime <= 0 then
                    self.m_tipsTagUnlockTimeCor = self:_ClearCoroutine(self.m_tipsTagUnlockTimeCor)
                    break
                end
            end
        end)
    end

    if isOpen then
        tipsNode.stateController:SetState("Open")
    elseif canPreview then
        tipsNode.stateController:SetState("NotOpenButPreview")
    else
        tipsNode.stateController:SetState("NotOpen")
    end

    local lockCount = #tagInfo.lockIds
    if lockCount > 0 and isOpen then
        tipsNode.keyLockInfoRoot.gameObject:SetActive(true)
        tipsNode.keyLockInfoCell1.gameObject:SetActive(true)
        tipsNode.keyLockInfoCell2.gameObject:SetActive(false)
        local lockInfo = self.m_tagKeyInfos[tagInfo.lockIds[1]]
        self:_RefreshTipsLockCell(lockInfo, tipsNode.keyLockInfoCell1, self.m_tipsKeyCellCacheList[1])
        if lockCount > 1 then
            tipsNode.keyLockInfoCell2.gameObject:SetActive(true)
            lockInfo = self.m_tagKeyInfos[tagInfo.lockIds[2]]
            self:_RefreshTipsLockCell(lockInfo, tipsNode.keyLockInfoCell2, self.m_tipsKeyCellCacheList[2])
        end

        tipsNode.quickUnlockBtn.onClick:RemoveAllListeners()
        tipsNode.quickUnlockBtn.onClick:AddListener(function()
            self:_HideTagTips(true)
            self:_QuickUnlockTag(tagInfo)
        end)
    else
        tipsNode.keyLockInfoRoot.gameObject:SetActive(false)
    end



    self.view.tagList:AutoScrollToRectTransform(tagCell.transform, true)
    UIUtils.updateTipsPosition(tipsNode.rectTransform, tagCell.transform, self.view.rectTransform, self.uiCamera, UIConst.UI_TIPS_POS_TYPE.AdaptiveRightTop)
    tipsNode.autoCloseArea.tmpSafeArea = tagCell.transform
    tipsNode.autoCloseArea.onTriggerAutoClose:RemoveAllListeners()
    tipsNode.autoCloseArea.onTriggerAutoClose:AddListener(function()
        self:_HideTagTips()
    end)


    if DeviceInfo.usingController then
        self.view.switchAreaKeyHint.gameObject:SetActive(false)
        Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
            panelId = PANEL_ID,
            isGroup = true,
            id = self.view.tagTipsNode.inputGroup.groupId,
            hintPlaceholder = self.view.tagTipsNode.controllerHintPlaceholder,
            rectTransform = self.view.tagTipsNode.transform,
            noHighlight = true,
        })
    end
end

ContingencySelectTagCtrl._HideTagTips = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    if DeviceInfo.usingController then
        Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.tagTipsNode.inputGroup.groupId)
    end
    if isInit then
        self.view.tipsNodeRoot.gameObject:SetActive(false)
    end
    self.m_curOpenTipsTagIndex = -1
    self.m_tipsTagUnlockTimeCor = self:_ClearCoroutine(self.m_tipsTagUnlockTimeCor)
    self:_ClearTagSelect()
    local canSwitchArea = #self.m_joinedTagInfos > 0
    self.view.switchAreaKeyHint.gameObject:SetActive(canSwitchArea)
end

ContingencySelectTagCtrl._OnSetTagSuccess = HL.Method(HL.Any) << function(self, arg)
    local gameId = unpack(arg)
    if gameId ~= self.m_gameId then
        return
    end

    if self.m_waitEnterDungeon then
        PhaseManager:GoToPhase(PhaseId.CharFormation, {
            dungeonId = gameId,
            enterDungeonCallback = function(enterDungeonId)
                LuaSystemManager.uiRestoreSystem:AddRequest(enterDungeonId)
            end,
            startBtnCallback = function()
                AudioManager.PostEvent("au_music_meta_ui_cc_v1d3_preparing_finished")
            end
        })
        self.m_waitEnterDungeon = false
    end
end

ContingencySelectTagCtrl._RefreshOutsideLockDot = HL.Method() << function(self)
    if self.m_outsideLockDotInfo == nil then
        return
    end

    if Time.time < self.m_checkOutsideLockDotTickTime then
        return
    end
    self.m_checkOutsideLockDotTickTime = Time.time + CheckOutsideLockDotInterval


    local notViewCount = 0
    for i, lockInfo in pairs(self.m_outsideLockDotInfo.outsideLockInfos) do
        if lockInfo.isNotView then
            local tagCell = self:_GetTagCell(lockInfo.cellIndex)
            if tagCell and self:_isTagCellCanView(tagCell.transform) then
                lockInfo.isNotView = false
            else
                notViewCount = notViewCount + 1
            end
        end
    end
    if notViewCount <= 0 then
        self.m_outsideLockDotInfo = nil
        self.view.outsideDotNode:SetState("RedDot")
    end
end

ContingencySelectTagCtrl._isTagCellCanView = HL.Method(RectTransform).Return(HL.Boolean, HL.Number) << function(self, tagCellTransform)
    local viewport = self.view.tagList and self.view.tagList.viewport
    if not viewport or not tagCellTransform then
        return false, LockDir.None
    end
    local cam = self.uiCamera
    local viewRect = CSUtils.RectTransformToScreenRect(viewport, cam)
    local cellRect = CSUtils.RectTransformToScreenRect(tagCellTransform, cam)

    local overlapX = cellRect.xMax <= viewRect.xMax and cellRect.xMin >= viewRect.xMin
    local visible = overlapX
    if visible then
        return true, LockDir.None
    end

    local side
    if cellRect.xMax <= viewRect.xMin then
        side = LockDir.Left
    elseif cellRect.xMin >= viewRect.xMax then
        side = LockDir.Right
    else
        side = LockDir.None
    end
    return false, side
end

ContingencySelectTagCtrl._isTagEffectCellCanView = HL.Method(RectTransform).Return(HL.Boolean) << function(self, tagEffectCellTransform)
    local viewport = self.view.tagEffectListRect and self.view.tagEffectListRect.viewport
    if not viewport or not tagEffectCellTransform then
        return false
    end
    local cam = self.uiCamera
    local viewRect = CSUtils.RectTransformToScreenRect(viewport, cam)
    local cellRect = CSUtils.RectTransformToScreenRect(tagEffectCellTransform, cam)
    local overlapY = cellRect.yMax <= viewRect.yMax and cellRect.yMin >= viewRect.yMin
    return overlapY
end

ContingencySelectTagCtrl._GetRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, csIndex)
    local luaIndex = LuaIndex(csIndex)
    local col, row = ContingencyContractUtils.GetColumnRow(luaIndex)
    local tagInfo = self.m_tagInfos[col][row]
    if not tagInfo then
        return 0
    end

    local hasRedDot, redDotType = RedDotManager:GetRedDotState("ContingencyContractTag", {
        activityId = self.m_activityId,
        stageId = tagInfo.unlockStage,
        tagId = tagInfo.tagId
    })
    if hasRedDot then
        return redDotType
    else
        return 0
    end
end

ContingencySelectTagCtrl._GetFirstNotEmptyTagInfo = HL.Method().Return(HL.Opt(HL.Table)) << function(self)

    local maxCol = #self.m_tagInfos
    for col = 1, maxCol do
        for row = 1, ContingencyContractUtils.MAX_ROW_COUNT do
            local tagInfo = self.m_tagInfos[col][row]
            if tagInfo and not tagInfo.isEmpty then
                return tagInfo
            end
        end
    end
end

ContingencySelectTagCtrl._SetNaviTagCell = HL.Method(HL.Boolean) << function(self, isForceNavi)
    local tagInfo

    if self.m_curNaviTagCellIndex > 0 then
        local col, row = ContingencyContractUtils.GetColumnRow(self.m_curNaviTagCellIndex)
        tagInfo = self.m_tagInfos[col][row]
    elseif self.m_curSelectTagCellIndex > 0 then
        local col, row = ContingencyContractUtils.GetColumnRow(self.m_curSelectTagCellIndex)
        tagInfo = self.m_tagInfos[col][row]
    else

        local maxCol = #self.m_tagInfos
        local isFound = false
        for col = 1, maxCol do
            for row = 1, ContingencyContractUtils.MAX_ROW_COUNT do
                local checkTagInfo = self.m_tagInfos[col][row]
                if not checkTagInfo.isEmpty then
                    local tagCell = self:_GetTagCell(checkTagInfo.cellIndex)
                    if tagCell and self:_isTagCellCanView(tagCell.transform) then
                        tagInfo = checkTagInfo
                        isFound = true
                        break
                    end
                end
            end
            if isFound then
                break
            end
        end
    end
    if not tagInfo or tagInfo.isEmpty then
        tagInfo = self:_GetFirstNotEmptyTagInfo()
    end
    if not tagInfo then
        return
    end

    local tagCell = self:_GetTagCell(tagInfo.cellIndex)
    if tagCell then
        if isForceNavi then
            if tagInfo.isUnlockByStage then
                self:SetNaviTarget(tagCell.openedBtn)
            else
                self:SetNaviTarget(tagCell.notOpenBtn)
            end
        else
            if tagInfo.isUnlockByStage then
                self:SetNaviTarget(tagCell.openedBtn)
            else
                self:SetNaviTarget(tagCell.notOpenBtn)
            end
        end
        self.m_isNaviTagCell = true
        self.m_curNaviTagEffectCellIndex = -1

        self:_ChangeAreaOperateEnable(true)
    end
end

ContingencySelectTagCtrl._SetNaviTagEffectCell = HL.Method() << function(self)

    local tagEffectCell
    local tagEffectCellIndex = -1
    local listCount = #self.m_joinedTagInfos
    if self.m_curNaviTagEffectCellIndex > 0 then

        self.m_curNaviTagEffectCellIndex = math.min(self.m_curNaviTagEffectCellIndex, listCount)
        local effectCell = self:_GetTagEffectCell(self.m_curNaviTagEffectCellIndex)
        if effectCell and self:_isTagEffectCellCanView(effectCell.transform) then
            tagEffectCell = effectCell
            tagEffectCellIndex = self.m_curNaviTagEffectCellIndex
        end
    end
    if not tagEffectCell and self.m_curSelectTagCellIndex > 0 then

        local col, row = ContingencyContractUtils.GetColumnRow(self.m_curSelectTagCellIndex)
        local tagInfo = self.m_tagInfos[col][row]
        if tagInfo.tagEffectIndex > 0 then
            local effectCell = self:_GetTagEffectCell(tagInfo.tagEffectIndex)
            if effectCell and self:_isTagEffectCellCanView(effectCell.transform) then
                tagEffectCell = effectCell
                tagEffectCellIndex = tagInfo.tagEffectIndex
            end
        end
    end
    if not tagEffectCell then

        local showRange = self.view.tagEffectList:GetShowRange()
        local startIndex, endIndex = 1, 0
        if showRange.x >= 0 and showRange.y >= showRange.x then
            startIndex = math.max(1, LuaIndex(showRange.x))
            endIndex = math.min(listCount, LuaIndex(showRange.y))
        end
        for i = startIndex, endIndex do
            local effectCell = self:_GetTagEffectCell(i)
            if effectCell and self:_isTagEffectCellCanView(effectCell.transform) then
                tagEffectCell = effectCell
                tagEffectCellIndex = i
                break
            end
        end

        if not tagEffectCell and listCount > 0 then
            tagEffectCellIndex = 1
            self:_ScrollTagEffectListToIndex(tagEffectCellIndex, true)
            tagEffectCell = self:_GetTagEffectCell(tagEffectCellIndex)
        end
    end


    if tagEffectCell then
        self:SetNaviTarget(tagEffectCell.naviDeco)
        if self.view.tagEffectListRect then
            self.view.tagEffectListRect:AutoScrollToRectTransform(tagEffectCell.transform)
        elseif tagEffectCellIndex > 0 then
            self:_ScrollTagEffectListToIndex(tagEffectCellIndex, false)
        end
        self.m_isNaviTagCell = false

        self:_ChangeAreaOperateEnable(false)
    end
end

ContingencySelectTagCtrl._ChangeAreaOperateEnable = HL.Method(HL.Boolean) << function(self, isEnable)
    self.m_isEnableAreaOperate = isEnable
    self.view.rewardInstructionBtnInputGroup.enabled = isEnable
    self.view.dungeonInfoBtnInputGroup.enabled = isEnable
    self.view.shareTagBtnInputGroup.enabled = isEnable
    self:_SetTagEffectControllerScrollEnabled(isEnable)
end

ContingencySelectTagCtrl._UpdateTagSelectRemoveNode = HL.Method(HL.Table) << function(self, tagInfo)
    local cellIndex = tagInfo.cellIndex
    local cell = self:_GetTagCell(cellIndex)
    if not cell then
        return
    end

    if cellIndex ~= self.m_curSelectTagCellIndex then
        if cell.selectRemoveNode.gameObject.activeSelf then
            cell.animationWrapper:Play("tageffectcell_slc_leftout")
        end
        return
    end

    local isJoined = self.m_joinedTagInfosMap[tagInfo.tagId] ~= nil
    if cell.selectRemoveNode.gameObject.activeSelf ~= isJoined then
        cell.animationWrapper:Play(isJoined and "tageffectcell_slc_left" or "tageffectcell_slc_leftout")
    end
end



ContingencySelectTagCtrl._OnMultiStageUpdate = HL.Method(HL.Any) << function(self, arg)
    local activityId = unpack(arg)
    if self.m_activityId ~= activityId then
        return
    end

    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local isGameplayEnd = activityData.gameplayEndTime - DateTimeUtils.GetCurrentTimestampBySeconds() <= 0
    if isGameplayEnd then
        GameInstance.player.guide:OnActivityDisabled()
        UIManager:Close(PanelId.CommonIntro)
        UIManager:Close(PanelId.ContingencyContractDetailsPopup)
        if UIManager:IsOpen(PanelId.ContingencyContractDetailsPopup) then
            PhaseManager:PopPhase(PhaseId.ContingencyContractDetailsPopup)
        end
        UIManager:Close(PanelId.ContingencyContractImportShare)
        UIManager:Close(PanelId.ContingencyContractInstructionBook)
        ActivityUtils.backToMainHud()
    else
        self:_TryRefreshNewUnlockedTags()
    end
end

ContingencySelectTagCtrl._TryRefreshNewUnlockedTags = HL.Method() << function(self)
    local preLockedTagInfos = {}
    for _, tagInfo in pairs(self.m_allTagInfosMap) do
        if not tagInfo.isUnlockByStage then
            table.insert(preLockedTagInfos, tagInfo)
        end
    end

    self:_UpdateData()
    local refreshCount = 0
    local needRefreshNaviTagCell = false
    for _, tagInfo in ipairs(preLockedTagInfos) do
        if tagInfo.isUnlockByStage then
            if DeviceInfo.usingController and self.m_isNaviTagCell and self.m_curNaviTagCellIndex == tagInfo.cellIndex then
                needRefreshNaviTagCell = true
            end
            local cell = self:_GetTagCell(tagInfo.cellIndex)
            if cell then
                self:_InitTagCell(cell, tagInfo.cellIndex)
                self:_RefreshTagConflictAndLock(tagInfo)
                refreshCount = refreshCount + 1
            end
        end
    end
    if refreshCount > 0 and self.view.tipsNodeRoot.gameObject.activeSelf then
        self:_HideTagTips(true)
    end
    if needRefreshNaviTagCell then
        self:_SetNaviTagCell(false)
    end
end

ContingencySelectTagCtrl._RefreshTagConflictAndLock = HL.Method(HL.Table) << function(self, tagInfo)
    local conflictInfo = self.m_tagConflictInfos[tagInfo.conflictId]
    if conflictInfo then
        self:_ChangeTagConflict(tagInfo, conflictInfo.curJoinedTag ~= nil and conflictInfo.curJoinedTag ~= tagInfo)
    end

    for _, lockId in ipairs(tagInfo.lockIds) do
        local lockInfo = self.m_tagKeyInfos[lockId]
        self:_ChangeTagLock(tagInfo, lockId, lume.count(lockInfo.curJoinedKeyTags) <= 0)
    end
end

ContingencySelectTagCtrl._SendSetTagList = HL.Method() << function(self)
    local joinedTagIds = {}
    for tagId, _ in pairs(self.m_joinedTagInfosMap) do
        table.insert(joinedTagIds, tagId)
    end
    if UNITY_EDITOR and ContingencyContractUtils.IsRemoveCCTagLimit then
        ccSystem:SendSetTagByGM(self.m_gameId, joinedTagIds)
    else
        ccSystem:SendSetTag(self.m_gameId, joinedTagIds)
    end
end


HL.Commit(ContingencySelectTagCtrl)
