
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.AdventureStage
PhaseAdventureBook = HL.Class('PhaseAdventureBook', phaseBase.PhaseBase)

PhaseAdventureBook.m_curPanelItem = HL.Field(HL.Forward("PhasePanelItem"))

PhaseAdventureBook.m_panelItemDic = HL.Field(HL.Table)

PhaseAdventureBook.m_bookPanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseAdventureBook.m_waitOpenCoroutine = HL.Field(HL.Thread)

PhaseAdventureBook.m_dungeonTab = HL.Field(HL.String) << ""

PhaseAdventureBook.m_reopenGemTermOverviewGameGroupId = HL.Field(HL.String) << ""





PhaseAdventureBook.s_messages = HL.StaticField(HL.Table) << {

}



PhaseAdventureBook._OnInit = HL.Override() << function(self)
   PhaseAdventureBook.Super._OnInit(self)
end




PhaseAdventureBook._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self.m_panelItemDic = {}
    local arg = self.arg or {}
    arg.phase = self
    self.m_bookPanel = self:CreatePhasePanelItem(PanelId.AdventureBook, arg)
    self:_BindControllerHintPlaceHolder()
end

PhaseAdventureBook._OnRefresh = HL.Override() << function(self)
    if not self.m_bookPanel then
        return
    end

    Notify(MessageConst.ON_CHANGE_ADVENTURE_BOOK_TAB, self.arg)
end

PhaseAdventureBook._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseAdventureBook._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    Notify(MessageConst.ON_PHASE_ADVENTURE_BOOK_BEHIND)
end

PhaseAdventureBook._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseAdventureBook._OnActivated = HL.Override() << function(self)
end

PhaseAdventureBook._OnDeActivated = HL.Override() << function(self)
end

PhaseAdventureBook._OnDestroy = HL.Override() << function(self)
    self:_ClearCoroutine(self.m_waitOpenCoroutine)
   PhaseAdventureBook.Super._OnDestroy(self)
end




PhaseAdventureBook.OnTabChange = HL.Method(HL.Table) << function(self, arg)
    if arg.panelId == nil then
       return
    end




    self:_ClearCoroutine(self.m_waitOpenCoroutine)
    if self.m_curPanelItem then
        local preUiCtrl = self.m_curPanelItem.uiCtrl
        local outAniName = arg.changeToLeft and preUiCtrl.view.config.ANI_OUT_RIGHT or preUiCtrl.view.config.ANI_OUT_LEFT
        if self.m_curPanelItem.uiCtrl.view.animationWrapper then
            self.m_curPanelItem.uiCtrl.view.animationWrapper:ClearTween(false)
        end
        if self.m_curPanelItem.uiCtrl.panelId == arg.panelId then
            Notify(MessageConst.ON_ADVENTURE_BOOK_SWITCH_SAME_TAB, arg.panelId)
        else
            self.m_curPanelItem:ClearNaviTarget()
        end
        logger.info("[PhaseAdventureBook] play out：", self.m_curPanelItem.uiCtrl)
        if self.m_curPanelItem.uiCtrl.view.animationWrapper and not string.isEmpty(outAniName) then
            self.m_curPanelItem.uiCtrl.view.animationWrapper:Play(outAniName, function()
                preUiCtrl:Hide()
            end)
        else
            logger.error(string.format("请检查%s的config配置", tostring(self.m_curPanelItem.uiCtrl)))
            self.m_curPanelItem.uiCtrl:Hide()
        end
        self.m_curPanelItem = nil
        self.m_waitOpenCoroutine = self:_StartCoroutine(function()
            coroutine.wait(self.m_bookPanel.uiCtrl.view.config.CHANGE_TAB_IN_ANI_DELAY_TIME)
            self:_OpenTab(arg.panelId, arg.changeToLeft)
            self.m_waitOpenCoroutine = nil
            self:_BindControllerHintPlaceHolder()
        end)
    else
        self:_OpenTab(arg.panelId, arg.changeToLeft)
        self:_BindControllerHintPlaceHolder()
    end
end

PhaseAdventureBook._OpenTab = HL.Method(HL.Number, HL.Boolean) << function(self, panelId, changeToLeft)
    local panelItem
    if self.m_panelItemDic[panelId] then
        panelItem = self.m_panelItemDic[panelId]
        panelItem.uiCtrl:Show()
    else
        local arg = self.arg or {}
        arg.phase = self
        panelItem = self:CreatePhasePanelItem(panelId, arg)
        self.m_panelItemDic[panelId] = panelItem
    end
    local inAniName = changeToLeft and panelItem.uiCtrl.view.config.ANI_IN_RIGHT or panelItem.uiCtrl.view.config.ANI_IN_LEFT
    logger.info("[PhaseAdventureBook] play in：", panelItem.uiCtrl)
    if panelItem.uiCtrl.view.animationWrapper and not string.isEmpty(inAniName) then
        panelItem.uiCtrl.view.animationWrapper:ClearTween(false)
        panelItem.uiCtrl.view.animationWrapper:Play(inAniName)
    else
        logger.error(string.format("请检查%s的config配置", tostring(panelItem.uiCtrl)))
    end
    self.m_curPanelItem = panelItem
end

PhaseAdventureBook._BindControllerHintPlaceHolder = HL.Method() << function(self)
    if not self.m_bookPanel or not self.m_curPanelItem then
        return
    end

    local bookCtrl = self.m_bookPanel.uiCtrl
    if bookCtrl then
        self.m_curPanelItem.uiCtrl.view.controllerHintPlaceholder:InitControllerHintPlaceholder({
            bookCtrl.view.inputGroup.groupId,
            self.m_curPanelItem.uiCtrl.view.inputGroup.groupId,
        })
    end
end

PhaseAdventureBook.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}
    arg.panelId = self.m_curPanelItem.uiCtrl.panelCfg.name
    if arg.panelId == "AdventureDungeon" then
        arg.dungeonTab = self.m_curPanelItem.uiCtrl:GetCurTabName()
        arg.filterIndex = self.m_curPanelItem.uiCtrl:GetCurEnemySpawnerFilterIndex()
        local isOpen, gemCtrl = UIManager:IsOpen(PanelId.GemTermOverviewPopup)
        if isOpen and gemCtrl and gemCtrl.m_info then
            arg.reopenGemTermOverviewGameGroupId = gemCtrl.m_info.gameGroupId
        end
    elseif arg.panelId == "AdventureStage" then
        arg.adventureStageViewIndex = self.m_curPanelItem.uiCtrl.m_curAdventureStage
        arg.selectedGroupType = self.m_curPanelItem.uiCtrl.m_selectedGroupType
    end
    arg.phase = nil
    return arg
end

HL.Commit(PhaseAdventureBook)
