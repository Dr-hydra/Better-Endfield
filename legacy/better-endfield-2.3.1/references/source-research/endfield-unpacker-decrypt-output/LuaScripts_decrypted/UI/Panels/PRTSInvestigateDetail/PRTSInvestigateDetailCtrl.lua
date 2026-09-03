local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.PRTSInvestigateDetail
local PHASE_ID = PhaseId.PRTSInvestigateDetail
PRTSInvestigateDetailCtrl = HL.Class('PRTSInvestigateDetailCtrl', uiCtrl.UICtrl)






PRTSInvestigateDetailCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_INVESTIGATE_FINISHED] = '_OnInvestigateFinished',
    [MessageConst.ON_UNLOCK_PRTS] = '_OnUnlockStoryColl',

    [MessageConst.PRTS_CHANGE_INVESTIGATE_GALLERY_NOTE_VISIBLE] = '_OnChangeShownState',

    [MessageConst.ON_READ_PRTS_NOTE_BATCH] = '_OnNoteStateChange',
    [MessageConst.ON_UNREAD_PRTS_NOTE_BATCH] = '_OnNoteStateChange',
}




PRTSInvestigateDetailCtrl.m_getCategoryCellFunc = HL.Field(HL.Function)

PRTSInvestigateDetailCtrl.m_genRewardCells = HL.Field(HL.Forward("UIListCache"))

PRTSInvestigateDetailCtrl.m_getNoteCellFunc = HL.Field(HL.Function)

PRTSInvestigateDetailCtrl.m_investId = HL.Field(HL.String) << ""

PRTSInvestigateDetailCtrl.m_arg = HL.Field(HL.Table)

PRTSInvestigateDetailCtrl.m_info = HL.Field(HL.Table)

PRTSInvestigateDetailCtrl.m_isNoteShown = HL.Field(HL.Boolean) << false

PRTSInvestigateDetailCtrl.m_selectedCollId = HL.Field(HL.String) << ""

PRTSInvestigateDetailCtrl.m_scrollToCategoryCor = HL.Field(HL.Thread)

PRTSInvestigateDetailCtrl.m_logNoteTimeTemp = HL.Field(HL.Number) << -1

PRTSInvestigateDetailCtrl.m_currentNoteCategoryIndex = HL.Field(HL.Number) << -1

PRTSInvestigateDetailCtrl.m_progressCells = HL.Field(HL.Table)


PRTSInvestigateDetailCtrl.m_mapInfoCells = HL.Field(HL.Table)


PRTSInvestigateDetailCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg
    self:_InitUI()
    self:_InitData(arg)
    self:_UpdateData()
    self:_ApplyResumeState(arg and arg.resumeState or nil)
    self:_RefreshAllUI()
    if self.m_isNoteShown then
        self:_RefreshUINoteShowState(true)
    else
        self:_RefreshUINoteShowState(false)
    end
    if self.m_arg then
        self.m_arg.resumeState = nil
    end
end

PRTSInvestigateDetailCtrl.OnClose = HL.Override() << function(self)

    self:_ClearCoroutine(self.m_scrollToCategoryCor)
end

PRTSInvestigateDetailCtrl.OnHide = HL.Override() << function(self)
    self.view.rewardListNaviGroup:ManuallyStopFocus()
end

PRTSInvestigateDetailCtrl.OnShow = HL.Override() << function(self)
    self:_TryRestoreCollSelection()
end

PRTSInvestigateDetailCtrl._OnChangeShownState = HL.Method(HL.Any) << function(self, arg)
    if arg.isShow then
        local cell = self.m_getCategoryCellFunc(self.m_currentNoteCategoryIndex)
        if cell then
            cell:RefreshUINoteShowState(false, false)
            self:_MarkNoteRead(self.m_currentNoteCategoryIndex)
        end
        self.m_currentNoteCategoryIndex = arg.index
    end
    self:_RefreshUINoteShowState(arg.isShow)

    self.m_scrollToCategoryCor = self:_StartCoroutine(function()

        coroutine.wait(0.1)
        self.view.categoryList:ScrollToIndex(CSIndex(arg.index))
    end)
end

PRTSInvestigateDetailCtrl._OnUnlockStoryColl = HL.Method(HL.Table) << function(self, args)
    local collId = unpack(args)
    local investId = self.m_investId
    local hasCfg, investCfg = Tables.prtsInvestigate:TryGetValue(investId)
    if not hasCfg then
        logger.error("[prts Investigate Table] missing cfg, id = " .. investId)
        return
    end
    if lume.find(investCfg.collectionIdList, collId) then
        self:_UpdateData()
        self:_RefreshAllUI()
    end
end

PRTSInvestigateDetailCtrl._OnNoteStateChange = HL.Method() << function(self)
    local bundleCount = #self.m_info.categoryInfoBundles
    for i = 1, bundleCount do
        local categoryObj = self.view.categoryList:Get(CSIndex(i))
        local categoryCell = self.m_getCategoryCellFunc(categoryObj)
        if categoryCell then
            categoryCell:ForceRefreshNoteCell(self.m_info.categoryInfoBundles[i].noteInfos)
        end
    end
end

PRTSInvestigateDetailCtrl._OnInvestigateFinished = HL.Method(HL.Table) << function(self, args)
    local investId = unpack(args)
    if self.m_investId == investId then
        self:_UpdateData()
        self:_RefreshAllUI()
        PhaseManager:OpenPhase(PhaseId.PRTSInvestigateReport, {
            investId = self.m_info.investId,
            storyCollId = self.m_info.unlockPrts,
            isNewReport = true,
        })
    end
end

PRTSInvestigateDetailCtrl.ShowSelf = HL.StaticMethod(HL.Any) << function(args)
    local id = unpack(args)
    PhaseManager:OpenPhase(PhaseId.PRTSInvestigateDetail, { id = id })
end

PRTSInvestigateDetailCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.m_arg and lume.deepCopy(self.m_arg) or {}
    arg.id = self.m_investId
    arg.resumeState = self:_CollectResumeState()
    return arg
end



PRTSInvestigateDetailCtrl._CollectResumeState = HL.Method().Return(HL.Table) << function(self)
    return {
        isNoteShown = self.m_isNoteShown,
        selectedCollId = self.m_selectedCollId,
        currentNoteCategoryIndex = self.m_currentNoteCategoryIndex,
    }
end

PRTSInvestigateDetailCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    if (arg == nil or string.isEmpty(arg.id)) then
        logger.error("[PRTSInvestigateDetailCtrl:_InitData] arg or arg.id is nil!")
        return
    end

    self.m_investId = arg.id
    self.m_isNoteShown = false
    self.m_selectedCollId = ""
    self.m_currentNoteCategoryIndex = -1
end

PRTSInvestigateDetailCtrl._UpdateData = HL.Method() << function(self)
    local investId = self.m_investId
    local hasCfg, investCfg = Tables.prtsInvestigate:TryGetValue(investId)
    if not hasCfg then
        logger.error("[prts Investigate Table] missing cfg, id = " .. investId)
        return
    end

    self.m_info = {
        investId = investId,
        unlockPrts = investCfg.unlockPrts,
        title = investCfg.name,
        desc = investCfg.desc,
        investigateAreaDesc = investCfg.investigateAreaDesc,
        curCount = GameInstance.player.prts:GetStoryCollUnlockCount(investId),
        targetCount = #investCfg.collectionIdList,
        isRewarded = GameInstance.player.prts:IsInvestigateFinished(investId),
        rewardList = UIUtils.getRewardItems(investCfg.rewardId),
        categoryInfoBundles = PRTSInvestigateDetailCtrl._GetCategoryInfoBundles(investCfg.categoryDataList),
        mapInfo = PRTSInvestigateDetailCtrl._GetMapList(investCfg.categoryDataList),
    }
end

PRTSInvestigateDetailCtrl._ApplyResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    if not resumeState then
        return
    end
    self.m_isNoteShown = resumeState.isNoteShown == true
    self.m_selectedCollId = resumeState.selectedCollId or ""
    self.m_currentNoteCategoryIndex = resumeState.currentNoteCategoryIndex or -1
    for _, infoBundle in pairs(self.m_info.categoryInfoBundles) do
        if infoBundle.index == self.m_currentNoteCategoryIndex then
            infoBundle.showNote = self.m_isNoteShown
            break
        end
    end
end

PRTSInvestigateDetailCtrl._GetCollLocationById = HL.Method(HL.String).Return(HL.Number, HL.Number) << function(self, collId)
    if string.isEmpty(collId) then
        return -1, -1
    end
    for categoryIndex, infoBundle in ipairs(self.m_info.categoryInfoBundles) do
        for collIndex, collInfo in ipairs(infoBundle.collInfos) do
            if collInfo.collId == collId then
                return categoryIndex, collIndex
            end
        end
    end
    return -1, -1
end

PRTSInvestigateDetailCtrl._OnCollFocusChanged = HL.Method(HL.String) << function(self, collId)
    self.m_selectedCollId = collId
end

PRTSInvestigateDetailCtrl._GetMapList = HL.StaticMethod(HL.Any).Return(HL.Table) << function(categoryDataList)
    local map = {}
    local prtsSystem = GameInstance.player.prts
    for _, data in pairs(categoryDataList) do
        for _, id in pairs(data.collectionIdList) do
            local collCfg = Utils.tryGetTableCfg(Tables.prtsAllItem, id)
            local isUnlock = GameInstance.player.prts:IsPrtsUnlocked(id)
            if collCfg then
                local success, levelId = prtsSystem:TryGetLevelIdByPrtsId(id)
                if success then
                    local hasMap = false
                    for i, info in ipairs(map) do
                        if info.levelId == levelId then
                            if info.isUnlock ~= isUnlock then
                                map[i].isUnlock = false
                            end
                            hasMap = true
                            break
                        end
                    end
                    if not hasMap then
                        local _, levelCfg = Tables.levelDescTable:TryGetValue(levelId)
                        local mapInfo = {
                            levelId = levelId,
                            name = GameInstance.player.mapManager:IsLevelUnlocked(levelId) and levelCfg.showName or Language.LUA_PRTS_UNLOCK_LEVEL,
                            isUnlock = isUnlock,
                            sortId = isUnlock and 1 or 0,
                            level = levelId
                        }
                        table.insert(map, mapInfo)
                    end
                end
            end
        end
    end

    table.sort(map, Utils.genSortFunction({"sortId", "level"}, true))
    return map
end

PRTSInvestigateDetailCtrl._GetCategoryInfoBundles = HL.StaticMethod(HL.Any).Return(HL.Table) << function(categoryDataList)
    local infoBundleList = {}
    for _, data in pairs(categoryDataList) do

        local infoBundle = {
            title = data.name,
            index = data.index,
            collInfos = PRTSInvestigateDetailCtrl._GetCategoryCollInfos(data.collectionIdList),
            noteInfos = PRTSInvestigateDetailCtrl._GetCategoryNoteInfos(data.noteIdList),

            showNote = false,
            isPlayNoteAni = false,
        }
        if #infoBundle.collInfos > 0 then
            table.insert(infoBundleList, infoBundle)
        end
    end
    table.sort(infoBundleList, Utils.genSortFunction({ "index" }, true))
    return infoBundleList
end

PRTSInvestigateDetailCtrl._GetCategoryCollInfos = HL.StaticMethod(HL.Any).Return(HL.Table) << function(collIds)
    local infos = {}
    for _, id in pairs(collIds) do
        local collCfg = Utils.tryGetTableCfg(Tables.prtsAllItem, id)
        local isUnlock = GameInstance.player.prts:IsPrtsUnlocked(id)
        if collCfg and isUnlock then
            local firstLvCfg = Utils.tryGetTableCfg(Tables.prtsFirstLv, collCfg.firstLvId)
            if firstLvCfg then
                local info = {
                    collId = id,
                    name = collCfg.name,
                    imgPath = firstLvCfg.icon,
                }
                table.insert(infos, info)
            end
        end
    end
    return infos
end

PRTSInvestigateDetailCtrl._GetCategoryNoteInfos = HL.StaticMethod(HL.Any).Return(HL.Table) << function(noteIds)
    local infos = {}
    local lockCount = 0
    local lockId = -1
    local lockIndex = -1
    for index, id in pairs(noteIds) do
        local noteCfg = Utils.tryGetTableCfg(Tables.prtsNote, id)
        local isNoteUnlock = GameInstance.player.prts:IsNoteUnlock(id)
        if noteCfg and isNoteUnlock then
            local info = {
                noteId = id,
                index = index,
                desc = noteCfg.desc,
                isLastNote = false
            }
            local sourceText

            for index, collectionId in pairs(noteCfg.collectionIdList) do
                local collectionCfg = Utils.tryGetTableCfg(Tables.prtsAllItem, collectionId)
                if index == 0 then
                    sourceText = collectionCfg.name
                else
                    sourceText = string.format(Language.LUA_PRTS_NOTE_SOURCE_FORMAT, sourceText, collectionCfg.name)
                end
            end
            info.sourceText = string.format(Language.LUA_PRTS_NOTE_SOURCE, sourceText)
            table.insert(infos, info)
        else
            lockCount = lockCount + 1
            lockId = id
            lockIndex = index
        end
    end

    if lockCount == 1 and lockIndex > 0 then
        local noteCfg = Utils.tryGetTableCfg(Tables.prtsNote, lockId)
        local info = {
            noteId = lockId,
            index = lockIndex,
            desc = noteCfg.desc,
            isLastNote = true
        }
        table.insert(infos, info)
    end
    return infos
end






PRTSInvestigateDetailCtrl._InitUI = HL.Method() << function(self)

    local viewRef = self.view

    viewRef.closeNoteBtn.onClick:AddListener(function()
        self:_MarkNoteRead(self.m_currentNoteCategoryIndex)
        self:_SetNoteHasRead()
        self:_RefreshUINoteShowState(false)
    end)

    viewRef.closeBtn.onClick:AddListener(function()
        self:_SetCollHasRead()
        PhaseManager:PopPhase(PhaseId.PRTSInvestigateDetail)
    end)

    viewRef.getRewardBtn.onClick:AddListener(function()
        GameInstance.player.prts:SendFinishInvestigate(self.m_info.investId)
    end)

    viewRef.gotoReportBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.PRTSInvestigateReport, {
            investId = self.m_info.investId,
            storyCollId = self.m_info.unlockPrts,
        })
    end)


    self.m_genRewardCells = UIUtils.genCellCache(self.view.rewardCell)

    self.m_getCategoryCellFunc = UIUtils.genCachedCellFunction(self.view.categoryList)
    viewRef.categoryList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getCategoryCellFunc(obj)
        self:_OnRefreshCategoryCell(cell, LuaIndex(csIndex))
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.view.rewardListNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
end

PRTSInvestigateDetailCtrl._RefreshAllUI = HL.Method() << function(self)
    local info = self.m_info
    if not info then
        return
    end
    local viewRef = self.view
    local isComplete = (not info.isRewarded) and (info.curCount >= info.targetCount)

    viewRef.titleTxt.text = info.title
    viewRef.descTxt.text = info.desc
    viewRef.progressTxt.text = info.curCount .. '/' .. info.targetCount
    self:_RefreshProgress(info.curCount, info.targetCount)
    viewRef.investigateAreaDesc.text = info.investigateAreaDesc
    self:_RefreshUnlockMap()

    local rewardCount = #info.rewardList
    self.view.rewardListNaviGroup.enabled = rewardCount > 0
    self.m_genRewardCells:Refresh(rewardCount, function(cell, luaIndex)
        self:_OnRefreshRewardCell(cell, luaIndex)
    end)

    if info.isRewarded then
        viewRef.investState:SetState("Rewarded")
    elseif isComplete then
        viewRef.investState:SetState("Complete")
    else
        viewRef.investState:SetState("Normal")
    end

    viewRef.categoryList:UpdateCount(#self.m_info.categoryInfoBundles, true)
end

PRTSInvestigateDetailCtrl._TryRestoreCollSelection = HL.Method().Return(HL.Boolean) << function(self)
    if not self.m_info or #self.m_info.categoryInfoBundles <= 0 then
        return false
    end
    local categoryIndex, collIndex = self:_GetCollLocationById(self.m_selectedCollId)
    if categoryIndex < 1 or collIndex < 1 then
        categoryIndex = 1
        collIndex = 1
    end
    local categoryInfo = self.m_info.categoryInfoBundles[categoryIndex]
    local collInfo = categoryInfo and categoryInfo.collInfos[collIndex] or nil
    if not collInfo then
        return false
    end
    self.m_selectedCollId = collInfo.collId
    self.view.categoryList:ScrollToIndex(CSIndex(categoryIndex), true)
    local categoryObj = self.view.categoryList:Get(CSIndex(categoryIndex))
    local categoryCell = self.m_getCategoryCellFunc(categoryObj)
    local collCell = categoryCell and categoryCell.m_genCollCell:Get(collIndex) or nil
    if not collCell then
        return false
    end
    self:SetNaviTarget(collCell.gotoBtn)
    return true
end

PRTSInvestigateDetailCtrl._OnRefreshRewardCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local info = self.m_info.rewardList[luaIndex]
    cell:InitItem(info, function()
        UIUtils.showItemSideTips(cell)
    end)
    cell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
    cell.view.rewardedCover.gameObject:SetActiveIfNecessary(self.m_info.isRewarded)
end

PRTSInvestigateDetailCtrl._OnRefreshCategoryCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    cell.gameObject.name = "CategoryCell_" .. luaIndex
    local infoBundle = self.m_info.categoryInfoBundles[luaIndex]
    cell:InitPRTSInvestigateCategoryCell(infoBundle, self.m_investId, function(collId)
        self:_OnCollFocusChanged(collId)
    end)
    infoBundle.isPlayNoteAni = false
end

PRTSInvestigateDetailCtrl._RefreshUINoteShowState = HL.Method(HL.Boolean) << function(self, isShow)
    self.m_isNoteShown = isShow
    if self.m_isNoteShown then
        self.view.noteState:SetState("ShowNoteState")
        self:_RefreshUINote(true)
        self.view.gotoReportBtn.gameObject:SetActive(false)
        self.view.getRewardBtn.gameObject:SetActive(false)
        self.view.noteNode.animationWrapper:PlayInAnimation()
    else
        self.view.noteState:SetState("HideNoteState")
        local info = self.m_info
        local isComplete = (not info.isRewarded) and (info.curCount >= info.targetCount)
        if info.isRewarded then
            self.view.gotoReportBtn.gameObject:SetActive(true)
        elseif isComplete then
            self.view.getRewardBtn.gameObject:SetActive(true)
        end
    end

    local cell = self.m_getCategoryCellFunc(self.m_currentNoteCategoryIndex)
    if cell then
        cell:RefreshUINoteShowState(isShow, false)
    end








    self:_SendEventLogNote(isShow)
end

PRTSInvestigateDetailCtrl._RefreshUINote = HL.Method(HL.Boolean) << function(self, isShow)
    if isShow then
        local noteList = self.view.noteNode.noteList
        if not self.m_getNoteCellFunc then
            self.m_getNoteCellFunc = UIUtils.genCachedCellFunction(noteList)
            noteList.onUpdateCell:AddListener(function(obj, csIndex)
                self:_OnUpdateNoteCell(self.m_getNoteCellFunc(obj), LuaIndex(csIndex))
            end)
        end

        local count = 0
        for _, infoBundle in pairs(self.m_info.categoryInfoBundles) do
            if infoBundle.index == self.m_currentNoteCategoryIndex then
                count = #infoBundle.noteInfos
                break
            end
        end
        noteList:UpdateCount(count)
    end
end

PRTSInvestigateDetailCtrl._MarkNoteRead = HL.Method(HL.Number) << function(self, index)
    for _, infoBundle in pairs(self.m_info.categoryInfoBundles) do
        if infoBundle.index == self.m_currentNoteCategoryIndex then
            for _, noteInfo in pairs(infoBundle.noteInfos) do
                noteInfo.hasRead = true
            end
            break
        end
    end
end

PRTSInvestigateDetailCtrl._OnUpdateNoteCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local noteInfos = nil
    for _, infoBundle in pairs(self.m_info.categoryInfoBundles) do
        if infoBundle.index == self.m_currentNoteCategoryIndex then
            noteInfos = infoBundle.noteInfos
            break
        end
    end

    if not noteInfos then
        return
    end

    cell.gameObject.name = "NoteCell" .. index
    local info = noteInfos[index]
    cell.indexTxt.text = string.format(Language.LUA_PRTS_NOTE_INDEX_FORMAT, info.index + 1)
    cell.descTxt:SetAndResolveTextStyle(UIUtils.resolveTextCinematic(info.desc))
    cell.redDot:InitRedDot("PRTSNote", info.noteId)

    if info.hasRead then
        cell.redDot.gameObject:SetActiveIfNecessary(false)
    end

    if info.isLastNote then
        cell.stateController:SetState("Complete")
    else
        cell.sourceTxt:SetAndResolveTextStyle(UIUtils.resolveTextCinematic(info.sourceText or ""))
        cell.stateController:SetState("Normal")
    end

end

PRTSInvestigateDetailCtrl._SetNoteHasRead = HL.Method() << function(self)
    local readNoteList = {}
    for _, infoBundle in pairs(self.m_info.categoryInfoBundles) do
        for _, noteInfo in pairs(infoBundle.noteInfos) do
            if noteInfo.hasRead then
                table.insert(readNoteList, noteInfo.noteId)
            end
        end
    end
    GameInstance.player.prts:SendReadNoteList(readNoteList)
end

PRTSInvestigateDetailCtrl._SetCollHasRead = HL.Method() << function(self)
    for _, infoBundle in pairs(self.m_info.categoryInfoBundles) do
        for _, collInfo in pairs(infoBundle.collInfos) do
            if collInfo.hasRead then
                GameInstance.player.prts:MarkRead(collInfo.collId)
            end
        end
    end
end


PRTSInvestigateDetailCtrl._SendEventLogNote = HL.Method(HL.Boolean) << function(self, isEnter)
    if not isEnter and self.m_logNoteTimeTemp < 0 then
        logger.warn("PRTSInvestigateDetailCtrl._SendEventLogNote 调用不对称，not isEnter但缺少m_logTimeTemp数据")
        return
    elseif isEnter and self.m_logNoteTimeTemp >= 0 then
        logger.warn("PRTSInvestigateDetailCtrl._SendEventLogNote 调用不对称，isEnter但已有m_logTimeTemp数据")
        return
    end

    local stayTime = 0
    if isEnter then
        self.m_logNoteTimeTemp = DateTimeUtils.GetCurrentTimestampBySeconds()
    else
        stayTime = DateTimeUtils.GetCurrentTimestampBySeconds() - self.m_logNoteTimeTemp
        self.m_logNoteTimeTemp = -1
    end

    EventLogManagerInst:GameEvent_PRTSResearchArchiveView(isEnter, self.m_investId, true, "", stayTime)
end

PRTSInvestigateDetailCtrl._RefreshProgress = HL.Method(HL.Number, HL.Number) << function(self, currentNum, totalNum)
    if self.m_progressCells == nil then
        self.m_progressCells = {}
    end

    for i = 1, totalNum do
        local cell
        if i <= #self.m_progressCells then
            cell = self.m_progressCells[i]
        else
            local go = GameObject.Instantiate(self.view.progressNode.cell.gameObject, self.view.progressNode.transform)
            cell = Utils.wrapLuaNode(go)
            table.insert(self.m_progressCells, cell)
        end
        cell.fill.gameObject:SetActiveIfNecessary(i <= currentNum)
        cell.gameObject:SetActiveIfNecessary(true)
    end

    for i = totalNum + 1, #self.m_progressCells do
        self.m_progressCells[i].gameObject:SetActiveIfNecessary(false)
    end
end

PRTSInvestigateDetailCtrl._RefreshUnlockMap = HL.Method() << function(self)
    if self.m_mapInfoCells == nil then
        self.m_mapInfoCells = {}
    end

    local mapInfo = self.m_info.mapInfo
    local count = #mapInfo
    for i = 1, count do
        local cell
        if i <= #self.m_mapInfoCells then
            cell = self.m_mapInfoCells[i]
        else
            local go = GameObject.Instantiate(self.view.mapNameCell.gameObject, self.view.potentialSurveyAreasNode)
            cell = Utils.wrapLuaNode(go)
            table.insert(self.m_mapInfoCells, cell)
        end

        local info = mapInfo[i]
        cell.completedImg.gameObject:SetActiveIfNecessary(info.isUnlock)
        cell.text.text = info.name
        cell.gameObject:SetActiveIfNecessary(true)
    end

    for i = count + 1, #self.m_mapInfoCells do
        self.m_mapInfoCells[i].gameObject:SetActiveIfNecessary(false)
    end
end


HL.Commit(PRTSInvestigateDetailCtrl)
