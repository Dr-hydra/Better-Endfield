local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.PRTSInvestigateGallery
local PHASE_ID = PhaseId.PRTSInvestigateGallery

PRTSInvestigateGalleryCtrl = HL.Class('PRTSInvestigateGalleryCtrl', uiCtrl.UICtrl)






PRTSInvestigateGalleryCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_UNLOCK_PRTS] = '_OnUnlockStoryColl',
    [MessageConst.ON_INVESTIGATE_FINISHED] = '_OnInvestigateFinished',
    [MessageConst.ON_READ_PRTS_NOTE_BATCH] = '_OnNoteStateChange',
}



local InvestStateEnum = {
    IsLock = 0,
    IsFinished = 1,
    IsOngoing = 2,
    IsCanReward = 3,
}

PRTSInvestigateGalleryCtrl.m_ongoingCount = HL.Field(HL.Number) << 0

PRTSInvestigateGalleryCtrl.m_finishedCount = HL.Field(HL.Number) << 0

PRTSInvestigateGalleryCtrl.m_investInfos = HL.Field(HL.Table)

PRTSInvestigateGalleryCtrl.m_getInvestCellFunc = HL.Field(HL.Function)

PRTSInvestigateGalleryCtrl.m_indexTextFormat = HL.Field(HL.String) << ""



PRTSInvestigateGalleryCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self:_UpdateData()
    self:_RefreshAllUI()

    EventLogManagerInst:GameEvent_PRTSArchiveVisit(true, "research")
end

PRTSInvestigateGalleryCtrl.OnClose = HL.Override() << function(self)
    EventLogManagerInst:GameEvent_PRTSArchiveVisit(false, "research")
end

PRTSInvestigateGalleryCtrl.OnAnimationInFinished = HL.Override() << function(self)
    local firstObj = self.view.investList:Get(0)
    local firstCell = self.m_getInvestCellFunc(firstObj)
    if firstCell then
        self:SetNaviTarget(firstCell.gotoBtn)
    end
end

PRTSInvestigateGalleryCtrl._OnUnlockStoryColl = HL.Method(HL.Table) << function(self, args)
    self:_UpdateData()
    self:_RefreshAllUI()
end

PRTSInvestigateGalleryCtrl._OnInvestigateFinished = HL.Method(HL.Table) << function(self, args)
    self:_UpdateData()
    self:_RefreshAllUI()
end

PRTSInvestigateGalleryCtrl._OnNoteStateChange = HL.Method() << function(self)


end



PRTSInvestigateGalleryCtrl._UpdateData = HL.Method() << function(self)
    self.m_investInfos = {}
    self.m_ongoingCount = 0
    self.m_finishedCount = 0

    for id, investCfg in pairs(Tables.prtsInvestigate) do

        local targetCount = #investCfg.collectionIdList
        local curCount = GameInstance.player.prts:GetStoryCollUnlockCount(id)
        local stateOrder = 0
        local infoState
        if curCount <= 0 then
            infoState = InvestStateEnum.IsLock
            stateOrder = 3
        elseif GameInstance.player.prts:IsInvestigateFinished(id) then
            infoState = InvestStateEnum.IsFinished
            stateOrder = 2
            self.m_finishedCount = self.m_finishedCount + 1
        elseif curCount < targetCount then
            infoState = InvestStateEnum.IsOngoing
            stateOrder = 1
            self.m_ongoingCount = self.m_ongoingCount + 1
        else
            infoState = InvestStateEnum.IsCanReward
            stateOrder = 0
            self.m_ongoingCount = self.m_ongoingCount + 1
        end

        local iconPath = ""
        if infoState == InvestStateEnum.IsOngoing or infoState == InvestStateEnum.IsCanReward then

            if investCfg.type == GEnums.PrtsInvestigateType.Level0 then
                iconPath = self.view.config.ICON_PATH_ONGOING_LEVEL_0
            else
                iconPath = self.view.config.ICON_PATH_ONGOING_LEVEL_1
            end
        else

            if investCfg.type == GEnums.PrtsInvestigateType.Level0 then
                iconPath = self.view.config.ICON_PATH_FINISHED_LEVEL_0
            else
                iconPath = self.view.config.ICON_PATH_FINISHED_LEVEL_1
            end
        end

        local info = {
            id = id,
            title = investCfg.name,
            type = investCfg.type,
            iconPath = iconPath,
            index = investCfg.index,

            curCount = curCount,
            targetCount = targetCount,
            state = infoState,
            stateOrder = stateOrder,
        }
        table.insert(self.m_investInfos, info)
    end

    table.sort(self.m_investInfos, Utils.genSortFunction({ "stateOrder", "index" }, true))
end






PRTSInvestigateGalleryCtrl._InitUI = HL.Method() << function(self)

    self.m_getInvestCellFunc = UIUtils.genCachedCellFunction(self.view.investList)
    self.view.investList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getInvestCellFunc(obj)
        self:_OnRefreshInvestCell(cell, LuaIndex(csIndex))
    end)

    self.view.closeBtn.onClick:RemoveAllListeners()
    self.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.PRTSInvestigateGallery)
    end)

    self.m_indexTextFormat = "%0" .. self.view.config.TITLE_INDEX_DIGITS .. "d"
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

PRTSInvestigateGalleryCtrl._RefreshAllUI = HL.Method() << function(self)

    self.view.ongoingCountTxt.text = self.m_ongoingCount
    self.view.ongoingCountShadowTxt.text = self.m_ongoingCount
    if self.m_ongoingCount > 0 then
        self.view.ongoingCountTxt.color = self.view.config.NUM_COLOR_ONGOING
        self.view.ongoingCountShadowTxt.color = self.view.config.NUM_COLOR_ONGOING_SHADOW
    else
        self.view.ongoingCountTxt.color = self.view.config.NUM_COLOR_ZERO
        self.view.ongoingCountShadowTxt.color = self.view.config.NUM_COLOR_ZERO_SHADOW
    end

    self.view.finishedCountTxt.text = self.m_finishedCount
    self.view.finishedCountShadowTxt.text = self.m_finishedCount
    if self.m_finishedCount > 0 then
        self.view.finishedCountTxt.color = self.view.config.NUM_COLOR_FINISHED
        self.view.finishedCountShadowTxt.color = self.view.config.NUM_COLOR_FINISHED_SHADOW
    else
        self.view.finishedCountTxt.color = self.view.config.NUM_COLOR_ZERO
        self.view.finishedCountShadowTxt.color = self.view.config.NUM_COLOR_ZERO_SHADOW
    end

    self.view.investList:UpdateCount(#self.m_investInfos)
end

PRTSInvestigateGalleryCtrl._OnRefreshInvestCell = HL.Method(HL.Table, HL.Number) << function(self, cell, luaIndex)
    local info = self.m_investInfos[luaIndex]

    cell.indexTxt.text = string.format(self.m_indexTextFormat, luaIndex)
    cell.titleTxt.text = info.title
    cell.gameObject.name = info.id
    cell.gotoBtn.onClick:RemoveAllListeners()
    cell.redDot:Stop()

    if info.state == InvestStateEnum.IsLock then
        cell.lockStateAniWrapper:Play("prtsinvestigategallerycell_lock")
        cell.gotoBtn.interactable = false
        return
    end

    cell.lockStateAniWrapper:Play("prtsinvestigategallerycell_unlock")
    cell.gotoBtn.interactable = true

    local realIconPath = Utils.getImgGenderDiffPath(info.iconPath)
    cell.iconImg:LoadSprite(UIConst.UI_SPRITE_PRTS, realIconPath)

    if info.state == InvestStateEnum.IsFinished then
        cell.progressState:SetState("Finished")
    elseif info.state == InvestStateEnum.IsOngoing then
        cell.progressState:SetState("Ongoing")
        cell.curCountTxt.text = info.curCount
        cell.targetCountTxt.text = info.targetCount
    else
        cell.progressState:SetState("CanReward")
    end

    cell.gotoBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.PRTSInvestigateDetail, { id = info.id })
    end)

    cell.redDot:InitRedDot("PRTSInvestigate", info.id)
end



HL.Commit(PRTSInvestigateGalleryCtrl)
