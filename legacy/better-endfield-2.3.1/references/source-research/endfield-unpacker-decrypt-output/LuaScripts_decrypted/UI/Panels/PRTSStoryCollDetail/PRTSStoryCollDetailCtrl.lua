local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.PRTSStoryCollDetail
local PHASE_ID = PhaseId.PRTSStoryCollDetail
PRTSStoryCollDetailCtrl = HL.Class('PRTSStoryCollDetailCtrl', uiCtrl.UICtrl)






PRTSStoryCollDetailCtrl.s_messages = HL.StaticField(HL.Table) << {

}



local ContentTypeEnum = {
    RichContent = 0,
    Radio = 1,
}

PRTSStoryCollDetailCtrl.m_isFirstLvId = HL.Field(HL.Boolean) << false

PRTSStoryCollDetailCtrl.m_showGotoBtn = HL.Field(HL.Boolean) << false

PRTSStoryCollDetailCtrl.m_idList = HL.Field(HL.Table)

PRTSStoryCollDetailCtrl.m_curPageIndex = HL.Field(HL.Number) << 1

PRTSStoryCollDetailCtrl.m_curItemIndex = HL.Field(HL.Number) << 1

PRTSStoryCollDetailCtrl.m_genIndexPointCells = HL.Field(HL.Forward("UIListCache"))

PRTSStoryCollDetailCtrl.m_info = HL.Field(HL.Table)

PRTSStoryCollDetailCtrl.m_curItemInfo = HL.Field(HL.Table)

PRTSStoryCollDetailCtrl.m_args = HL.Field(HL.Table)

PRTSStoryCollDetailCtrl.m_logTimeTemp = HL.Field(HL.Number) << -1




PRTSStoryCollDetailCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)








    self.m_args = arg
    self:_InitUI()
    self:_InitData(arg)
    self:_ApplyResumeState(arg and arg.resumeState or nil)
    self:_UpdateData()
    self:_RefreshAllUI()
    if self.m_args then
        self.m_args.resumeState = nil
    end

    self:_SendEventLog(true)
end

PRTSStoryCollDetailCtrl.OnClose = HL.Override() << function(self)
    self:_SendEventLog(false)
end

PRTSStoryCollDetailCtrl._SendEventLog = HL.Method(HL.Boolean) << function(self, isEnter)
    if not isEnter and self.m_logTimeTemp < 0 then
        logger.warn("PRTSStoryCollDetailCtrl._SendEventLog 调用不对称，not isEnter但缺少m_logTimeTemp数据")
        return
    elseif isEnter and self.m_logTimeTemp >= 0 then
        logger.warn("PRTSStoryCollDetailCtrl._SendEventLog 调用不对称，isEnter但已有m_logTimeTemp数据")
        return
    end

    local stayTime = 0
    if isEnter then
        self.m_logTimeTemp = DateTimeUtils.GetCurrentTimestampBySeconds()
    else
        stayTime = DateTimeUtils.GetCurrentTimestampBySeconds() - self.m_logTimeTemp
        self.m_logTimeTemp = -1
    end

    local firstLvId = ""
    local prtsId = ""
    if self.m_isFirstLvId then
        local pageInfo = self.m_info.pageInfos[self.m_curPageIndex]
        firstLvId = pageInfo.firstLvId
        prtsId = pageInfo.itemInfos[self.m_curItemIndex].itemId
    else
        prtsId = self.m_info.pageInfos[self.m_curPageIndex].itemId
    end
    if string.isEmpty(self.m_args.researchId) then
        EventLogManagerInst:GameEvent_PRTSArchiveView(isEnter, self.m_args.pageType, self.m_args.categoryId, firstLvId, prtsId, stayTime)
    else
        EventLogManagerInst:GameEvent_PRTSResearchArchiveView(isEnter, self.m_args.researchId, false, prtsId, stayTime)
    end
end

PRTSStoryCollDetailCtrl.OnPhaseRefresh = HL.Override(HL.Any) << function(self, arg)
    self:_InitData(arg)
    self:_UpdateData()
    self:_RefreshAllUI()
end

PRTSStoryCollDetailCtrl.ShowSelf = HL.StaticMethod(HL.Any) << function(args)
    local id, isFirstLv = unpack(args)
    PhaseManager:OpenPhase(PHASE_ID, {
        isFirstLvId = isFirstLv,
        id = id,
    })
end

PRTSStoryCollDetailCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.m_args and lume.deepCopy(self.m_args) or {}
    arg.resumeState = self:_CollectResumeState()
    return arg
end



PRTSStoryCollDetailCtrl._CollectResumeState = HL.Method().Return(HL.Table) << function(self)
    return {
        curPageIndex = self.m_curPageIndex,
        curItemIndex = self.m_curItemIndex,
    }
end

PRTSStoryCollDetailCtrl._InitData = HL.Method(HL.Table) << function(self, arg)
    if not arg.idList and arg.id then
        arg.idList = { arg.id }
    end
    self.m_idList = arg.idList
    self.m_isFirstLvId = arg.isFirstLvId and true or false
    self.m_showGotoBtn = arg.showGotoBtn and true or false
    self.m_curPageIndex = arg.initShowIndex or 1
    self.m_curPageIndex = lume.clamp(self.m_curPageIndex, 1, #self.m_idList)

    if self.m_isFirstLvId then
        self.m_info = {
            pageInfos = {},
        }
        for _, firstLvId in pairs(self.m_idList) do
            if GameInstance.player.prts:IsFirstLvUnlock(firstLvId) then
                local firstLvCfg = Utils.tryGetTableCfg(Tables.prtsFirstLv, firstLvId)
                if firstLvCfg then
                    local itemInfos = self:_CreateItemInfos(firstLvCfg.itemIds)
                    if #itemInfos > 0 then
                        local newPageInfo = {
                            firstLvId = firstLvId,
                            itemInfos = itemInfos,
                        }
                        table.insert(self.m_info.pageInfos, newPageInfo)
                    end
                end
            end
        end
    else
        self.m_info = {
            pageInfos = self:_CreateItemInfos(self.m_idList),
        }
    end
    self.m_curPageIndex = lume.clamp(self.m_curPageIndex, 1, math.max(#self.m_info.pageInfos, 1))
    if self.m_isFirstLvId then
        local curPageInfo = self.m_info.pageInfos[self.m_curPageIndex]
        local itemCount = curPageInfo and #curPageInfo.itemInfos or 1
        self.m_curItemIndex = lume.clamp(self.m_curItemIndex, 1, math.max(itemCount, 1))
    else
        self.m_curItemIndex = 1
    end
end

PRTSStoryCollDetailCtrl._ApplyResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    if not resumeState then
        return
    end
    if resumeState.curPageIndex ~= nil then
        self.m_curPageIndex = resumeState.curPageIndex
    end
    if resumeState.curItemIndex ~= nil then
        self.m_curItemIndex = resumeState.curItemIndex
    end
    self.m_curPageIndex = lume.clamp(self.m_curPageIndex, 1, math.max(#self.m_info.pageInfos, 1))
    if self.m_isFirstLvId then
        local curPageInfo = self.m_info.pageInfos[self.m_curPageIndex]
        local itemCount = curPageInfo and #curPageInfo.itemInfos or 1
        self.m_curItemIndex = lume.clamp(self.m_curItemIndex, 1, math.max(itemCount, 1))
    else
        self.m_curItemIndex = 1
    end
end

PRTSStoryCollDetailCtrl._CreateItemInfos = HL.Method(HL.Any).Return(HL.Table) << function(self, itemIds)
    local itemInfos = {}
    for _, itemId in pairs(itemIds) do
        if GameInstance.player.prts:IsPrtsUnlocked(itemId) then
            local itemCfg = Utils.tryGetTableCfg(Tables.prtsAllItem, itemId)
            if itemCfg then
                local belongsInvestIds = GameInstance.player.prts:GetBelongsInvestIds(itemId)
                local belongsInvestNameList
                if belongsInvestIds then
                    belongsInvestNameList = {}
                    for _, id in pairs(belongsInvestIds) do
                        local cfg = Utils.tryGetTableCfg(Tables.prtsInvestigate, id)
                        table.insert(belongsInvestNameList, cfg.name)
                    end
                end
                local newItemInfo = {
                    itemId = itemId,
                    belongsInvestIds = belongsInvestIds,
                    belongsInvestNameList = belongsInvestNameList,
                    contentId = itemCfg.contentId,

                    contentType = itemCfg.type == "multi_media" and ContentTypeEnum.Radio or ContentTypeEnum.RichContent,
                    index = itemCfg.order,
                }
                table.insert(itemInfos, newItemInfo)
            end
        end
    end
    if self.m_isFirstLvId then
        table.sort(itemInfos, Utils.genSortFunction({ "index" }, true))
    end
    return itemInfos
end

PRTSStoryCollDetailCtrl._UpdateData = HL.Method() << function(self)
    if self.m_isFirstLvId then
        local pageInfo = self.m_info.pageInfos[self.m_curPageIndex]
        self.m_curItemInfo = pageInfo.itemInfos[self.m_curItemIndex]
    else
        self.m_curItemInfo = self.m_info.pageInfos[self.m_curPageIndex]
    end
end






PRTSStoryCollDetailCtrl._InitUI = HL.Method() << function(self)

    local viewRef = self.view
    viewRef.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.PRTSStoryCollDetail)
    end)

    viewRef.pageUpBtn.onClick:AddListener(function()
        self:_OnClickPageUpOrDownBtn(false)
    end)

    viewRef.pageDownBtn.onClick:AddListener(function()
        self:_OnClickPageUpOrDownBtn(true)
    end)

    viewRef.itemPreBtn.onClick:AddListener(function()
        self:_OnClickItemPreOrNextBtn(false)
    end)

    viewRef.itemNextBtn.onClick:AddListener(function()
        self:_OnClickItemPreOrNextBtn(true)
    end)

    self.m_genIndexPointCells = UIUtils.genCellCache(viewRef.indexPointCell)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

PRTSStoryCollDetailCtrl._RefreshAllUI = HL.Method() << function(self)
    self:_RefreshContent()
    self:_RefreshIndexPoint()
    self:_RefreshPageSwitchBtn()
    self:_RefreshItemSwitchBtn()
end

PRTSStoryCollDetailCtrl._RefreshContent = HL.Method() << function(self)
    local info = self.m_curItemInfo
    if self.m_curItemInfo.contentType == ContentTypeEnum.RichContent then
        self.view.prtsRichContent.gameObject:SetActiveIfNecessary(true)
        self.view.prtsRadio.gameObject:SetActiveIfNecessary(false)

        self.view.prtsRichContent:InitPRTSRichContent(info.contentId)
        if self.m_showGotoBtn and info.belongsInvestIds then

            self.view.prtsRichContent:SetGotoBtn(info.belongsInvestNameList, function(luaIndex)
                self:_OnClickGotoBtn(luaIndex)
            end)
        end
    else
        self.view.prtsRadio.gameObject:SetActiveIfNecessary(true)
        self.view.prtsRichContent.gameObject:SetActiveIfNecessary(false)

        local itemCfg = Utils.tryGetTableCfg(Tables.prtsAllItem, info.itemId)
        self.view.prtsRadio:InitPRTSRadio(info.contentId, itemCfg.name, itemCfg.overrideRadioId)
        self.view.prtsRadio:SetPlayRadio(true)
        if self.m_showGotoBtn and info.belongsInvestIds then

            self.view.prtsRadio:SetGotoBtn(info.belongsInvestNameList, function(luaIndex)
                self:_OnClickGotoBtn(luaIndex)
            end)
        end
    end

    GameInstance.player.prts:MarkRead(info.itemId)
end

PRTSStoryCollDetailCtrl._RefreshIndexPoint = HL.Method() << function(self)
    local pointCount = 0
    if self.m_isFirstLvId then
        local pageInfo = self.m_info.pageInfos[self.m_curPageIndex]
        pointCount = #pageInfo.itemInfos
    else
        pointCount = #self.m_info.pageInfos
    end
    if pointCount == 1 then
        pointCount = 0
    end
    self.m_genIndexPointCells:Refresh(pointCount, function(cell, luaIndex)
        self:_OnRefreshIndexPointCell(cell, luaIndex)
    end)
end

PRTSStoryCollDetailCtrl._OnRefreshIndexPointCell = HL.Method(HL.Table, HL.Number) << function(self, cell, luaIndex)
    if luaIndex == self.m_curItemIndex then
        cell.indexPointState:SetState("Select")
    else
        cell.indexPointState:SetState("Unselect")
    end
end

PRTSStoryCollDetailCtrl._RefreshPageSwitchBtn = HL.Method() << function(self)
    local totalCount = #self.m_info.pageInfos
    local viewRef = self.view
    local showUpBtn = self.m_curPageIndex > 1 and true or false
    local showDownBtn = self.m_curPageIndex < totalCount and true or false
    viewRef.pageUpBtn.gameObject:SetActiveIfNecessary(showUpBtn)
    viewRef.pageDownBtn.gameObject:SetActiveIfNecessary(showDownBtn)
end

PRTSStoryCollDetailCtrl._RefreshItemSwitchBtn = HL.Method() << function(self)
    local viewRef = self.view
    if not self.m_isFirstLvId then

        viewRef.indexPointList.gameObject:SetActiveIfNecessary(false)
        viewRef.itemPreBtn.gameObject:SetActiveIfNecessary(false)
        viewRef.itemNextBtn.gameObject:SetActiveIfNecessary(false)
        return
    end

    viewRef.indexPointList.gameObject:SetActiveIfNecessary(true)
    local curPageInfo = self.m_info.pageInfos[self.m_curPageIndex]
    local totalCount = #curPageInfo.itemInfos
    local showPreBtn = self.m_curItemIndex > 1 and true or false
    local showNextBtn = self.m_curItemIndex < totalCount and true or false
    viewRef.itemPreBtn.gameObject:SetActiveIfNecessary(showPreBtn)
    viewRef.itemNextBtn.gameObject:SetActiveIfNecessary(showNextBtn)
end

PRTSStoryCollDetailCtrl._OnClickPageUpOrDownBtn = HL.Method(HL.Boolean) << function(self, isDown)
    if isDown then
        self.m_curPageIndex = self.m_curPageIndex + 1
    else
        self.m_curPageIndex = self.m_curPageIndex - 1
    end
    self.m_curPageIndex = lume.clamp(self.m_curPageIndex, 1, #self.m_info.pageInfos)
    self.m_curItemIndex = 1

    self:_SendEventLog(false)
    self:_UpdateData()
    self:_RefreshAllUI()
    self:_SendEventLog(true)
    local aniWrapper = self.animationWrapper
    aniWrapper:PlayWithTween("prtsstorycolldetail_change")
end

PRTSStoryCollDetailCtrl._OnClickItemPreOrNextBtn = HL.Method(HL.Boolean) << function(self, isNext)
    if not self.m_isFirstLvId then

        return
    end

    if isNext then
        self.m_curItemIndex = self.m_curItemIndex + 1
    else
        self.m_curItemIndex = self.m_curItemIndex - 1
    end
    local curPageInfo = self.m_info.pageInfos[self.m_curPageIndex]
    self.m_curItemIndex = lume.clamp(self.m_curItemIndex, 1, #curPageInfo.itemInfos)

    self:_SendEventLog(false)
    self:_UpdateData()
    self:_RefreshAllUI()
    self:_SendEventLog(true)
    local aniWrapper = self.animationWrapper
    aniWrapper:PlayWithTween("prtsstorycolldetail_change")
end

PRTSStoryCollDetailCtrl._OnClickGotoBtn = HL.Method(HL.Number) << function(self, luaIndex)
    local investId = self.m_curItemInfo.belongsInvestIds[CSIndex(luaIndex)]
    PhaseManager:OpenPhase(PhaseId.PRTSInvestigateDetail, { id = investId })
end



HL.Commit(PRTSStoryCollDetailCtrl)
