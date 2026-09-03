local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.PRTSMain
local PHASE_ID = PhaseId.PRTS
PRTSMainCtrl = HL.Class('PRTSMainCtrl', uiCtrl.UICtrl)







PRTSMainCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_UNLOCK_PRTS] = '_OnUnlockStoryColl',
    [MessageConst.ON_INVESTIGATE_FINISHED] = '_OnInvestigateFinished',
}



PRTSMainCtrl.m_info = HL.Field(HL.Table)

PRTSMainCtrl.m_textPageType = HL.Field(HL.String) << ""

PRTSMainCtrl.m_documentPageType = HL.Field(HL.String) << ""

PRTSMainCtrl.m_multimediaPageType = HL.Field(HL.String) << ""





PRTSMainCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self:_InitData()
    self:_UpdateData()
    self:_RefreshAllUI()
    self:SetNaviTarget(self.view.investigateTab.gotoBtn)
end

PRTSMainCtrl._OnUnlockStoryColl = HL.Method(HL.Table) << function(self, args)
    self:_UpdateData()
    self:_RefreshAllUI()
end

PRTSMainCtrl._OnInvestigateFinished = HL.Method(HL.Table) << function(self, args)
    self:_UpdateData()
    self:_RefreshAllUI()
end



PRTSMainCtrl._InitData = HL.Method() << function(self)
    self.m_textPageType = "text"
    self.m_documentPageType = "document"
    self.m_multimediaPageType = "multi_media"
end

PRTSMainCtrl._UpdateData = HL.Method() << function(self)
    self.m_info = {
        textCount = GameInstance.player.prts:GetUnlockCountByPageType(self.m_textPageType),
        documentCount = GameInstance.player.prts:GetUnlockCountByPageType(self.m_documentPageType),
        multimediaCount = GameInstance.player.prts:GetUnlockCountByPageType(self.m_multimediaPageType),
        investFinishedCount = 0,
        investOnGoingCount = 0,
    }
    self:_UpdateInvestigateInfo()
end

PRTSMainCtrl._UpdateInvestigateInfo = HL.Method() << function(self)
    local count = 0
    local ongoingCount = 0
    for id, _ in pairs(Tables.prtsInvestigate) do
        if (GameInstance.player.prts:IsInvestigateFinished(id)) then
            count = count + 1
        else
            local curCount = GameInstance.player.prts:GetStoryCollUnlockCount(id)
            if curCount > 0 then
                ongoingCount = ongoingCount + 1
            end
        end
    end
    self.m_info.investFinishedCount = count
    self.m_info.investOnGoingCount = ongoingCount
end






PRTSMainCtrl._InitUI = HL.Method() << function(self)
    self.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.PRTS)
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.view.textTab.redDot:InitRedDot("PRTSText")
    self.view.textTab.gotoBtn.onClick:AddListener(function()
        self:_OnClickStoryCollTab(self.m_textPageType, self.m_info.textCount)
    end)

    self.view.documentTab.redDot:InitRedDot("PRTSDocument")
    self.view.documentTab.gotoBtn.onClick:AddListener(function()
        self:_OnClickStoryCollTab(self.m_documentPageType, self.m_info.documentCount)
    end)

    self.view.multimediaTab.redDot:InitRedDot("PRTSMultimedia")
    self.view.multimediaTab.gotoBtn.onClick:AddListener(function()
        self:_OnClickStoryCollTab(self.m_multimediaPageType, self.m_info.multimediaCount)
    end)

    self.view.investigateTab.redDot:InitRedDot("PRTSInvestigateTab")
    self.view.investigateTab.gotoBtn.onClick:AddListener(function()
        local totalCount = self.m_info.investFinishedCount + self.m_info.investOnGoingCount
        if totalCount > 0 then
            PhaseManager:OpenPhase(PhaseId.PRTSInvestigateGallery)
        else
            self:Notify(MessageConst.SHOW_TOAST, Language.LUA_PRTS_INVESTIGATE_NOT_FOUND_COLL_TOAST)
        end
    end)
end

PRTSMainCtrl._OnClickStoryCollTab = HL.Method(HL.String, HL.Number) << function(self, pageType, count)
    if count > 0 then
        PhaseManager:OpenPhase(PhaseId.PRTSStoryCollGallery, { pageType = pageType })
    else
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_PRTS_INVESTIGATE_NOT_FOUND_COLL_TOAST)
    end
end

PRTSMainCtrl._RefreshAllUI = HL.Method() << function(self)
    local viewRef = self.view

    self:_OnRefreshStoryCollTab(viewRef.textTab, self.m_info.textCount)
    self:_OnRefreshStoryCollTab(viewRef.documentTab, self.m_info.documentCount)
    self:_OnRefreshStoryCollTab(viewRef.multimediaTab, self.m_info.multimediaCount)

    local count = self.m_info.investFinishedCount
    local ongoingCount = self.m_info.investOnGoingCount
    local hasOngoing = ongoingCount > 0
    if hasOngoing then

        viewRef.ongoingCountTxt.text = ongoingCount
        viewRef.ongoingCountShadowTxt.text = ongoingCount
        viewRef.investCountState:SetState("Ongoing")
    elseif count > 0 then

        viewRef.finishedCountTxt.text = count
        viewRef.finishedCountShadowTxt.text = count
        viewRef.investCountState:SetState("Finished")
    else

        viewRef.finishedCountTxt.text = count
        viewRef.finishedCountShadowTxt.text = count
        viewRef.investCountState:SetState("Finished")
    end
end

PRTSMainCtrl._OnRefreshStoryCollTab = HL.Method(HL.Any, HL.Number) << function(self, uiRef, count)
    local color = count > 0 and self.view.config.NUM_COLOR_UNLOCK or self.view.config.NUM_COLOR_LOCK
    uiRef.countTxt.text = count
    uiRef.countTxt.color = color
end

PRTSMainCtrl._CloseWithAnimation = HL.Method() << function(self)
    PhaseManager:PopPhase(PHASE_ID)
end



HL.Commit(PRTSMainCtrl)
