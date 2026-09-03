local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.DomainDepotPackage
local DOMAIN_DEPOT_BACKGROUND_STAGES = UIConst.DOMAIN_DEPOT_BACKGROUND_STAGES

PhaseDomainDepotPackage = HL.Class('PhaseDomainDepotPackage', phaseBase.PhaseBase)

local DOMAIN_MONEY_TITLE_NAVI_GROUP_ORDER_OFFSET = 5
local TOP_PANEL_ID_TO_TAB_INDEX = {
    [PanelId.DomainDepotInstList] = 1,
    [PanelId.DomainDepotDelivery] = 2,
    [PanelId.DomainDepotMyOrder] = 3,
}

PhaseDomainDepotPackage.m_popAsyncActionHelper = HL.Field(HL.Forward("AsyncActionHelper"))

PhaseDomainDepotPackage.m_curPanelItem = HL.Field(HL.Forward("PhasePanelItem"))

PhaseDomainDepotPackage.m_panelItemDic = HL.Field(HL.Table)

PhaseDomainDepotPackage.m_typePanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseDomainDepotPackage.m_itemPanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseDomainDepotPackage.m_backPanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseDomainDepotPackage.m_sellPanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseDomainDepotPackage.m_tabPanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseDomainDepotPackage.m_showSellAnimPanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseDomainDepotPackage.m_sellEndPanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseDomainDepotPackage.m_pendingPanelArgMap = HL.Field(HL.Table)

PhaseDomainDepotPackage.m_lastTabPanelId = HL.Field(HL.Number) << PanelId.DomainDepotInstList

PhaseDomainDepotPackage.m_lastTopPanelState = HL.Field(HL.Table)





PhaseDomainDepotPackage.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_OPEN_DOMAIN_DEPOT_TAB] = { 'OnOpenDomainDepotTab', false },
    [MessageConst.ON_CLOSE_DOMAIN_DEPOT_TAB] = { 'OnCloseDomainDepotTab', true },
    [MessageConst.ON_OPEN_DOMAIN_DEPOT_PACK_TYPE_SELECT_PANEL] = { 'OnOpenPackTypeSelectPanel', true },
    [MessageConst.ON_CLOSE_DOMAIN_DEPOT_PACK_TYPE_SELECT_PANEL] = { 'OnClosePackTypeSelectPanel', true },
    [MessageConst.ON_OPEN_DOMAIN_DEPOT_PACK_ITEM_SELECT_PANEL] = { 'OnOpenPackItemSelectPanel', true },
    [MessageConst.ON_DOMAIN_DEPOT_BACK_TO_PACK_TYPE_SELECT_PANEL] = { 'OnBackToPackTypeSelectPanel', true },
    [MessageConst.ON_CLOSE_DOMAIN_DEPOT_PACK_ITEM_SELECT_PANEL] = { 'OnClosePackItemSelectPanel', true },
    [MessageConst.ON_OPEN_DOMAIN_DEPOT_PACK_SELL_PANEL] = { 'OnOpenPackSellPanel', true },
    [MessageConst.ON_CLOSE_DOMAIN_DEPOT_PACK_SELL_PANEL] = { 'OnClosePackSellPanel', true },
    [MessageConst.ON_OPEN_SHOW_SELL_ANIM_PANEL] = { 'OnOpenSellAnimPanel', true },
    [MessageConst.ON_SELECT_BUYER_END] = { 'OnPackSellEndPanel', true },
    [MessageConst.ON_CLOSE_DOMAIN_DEPOT_PACK_SETTLE_PANEL] = { 'OnClosePackSellEndPanel', true },
}

PhaseDomainDepotPackage.OnOpenDomainDepotTab = HL.StaticMethod(HL.Table) << function(args)
    local domainId
    if string.isEmpty(args) then
        domainId = ScopeUtil.GetCurrentChapterIdAsStr()
    else
        local domainDepotId = unpack(args)
        local domainDepotCfg = Tables.domainDepotTable[domainDepotId]
        domainId = domainDepotCfg.domainId
    end
    PhaseManager:OpenPhase(PhaseId.DomainDepotPackage, { domainId = domainId })
end

PhaseDomainDepotPackage.OnCloseDomainDepotTab = HL.Method() << function(self)
    PhaseManager:PopPhase(PHASE_ID)
end

PhaseDomainDepotPackage.OnTabChange = HL.Method(HL.Number) << function(self, panelId)
    if panelId == nil then
        return
    end

    self.m_lastTabPanelId = panelId

    if self.m_curPanelItem and self.m_curPanelItem.uiCtrl then
        self.m_curPanelItem.uiCtrl:Hide()
    end

    local panelItem
    if self.m_panelItemDic[panelId] and self.m_panelItemDic[panelId].uiCtrl ~= nil then
        panelItem = self.m_panelItemDic[panelId]
    else
        local panelArg = self.m_pendingPanelArgMap and self.m_pendingPanelArgMap[panelId] or nil
        panelArg = lume.deepCopy(panelArg or {})
        panelArg.domainId = panelArg.domainId or self.arg.domainId
        panelItem = self:CreatePhasePanelItem(panelId, panelArg)
        self.m_panelItemDic[panelId] = panelItem
        if self.m_pendingPanelArgMap then
            self.m_pendingPanelArgMap[panelId] = nil
        end
    end
    panelItem.uiCtrl:Show()
    if HL.TryGet(panelItem.uiCtrl, "Sync") then
        panelItem.uiCtrl:Sync()
    end
    panelItem.uiCtrl.view.domainTopMoneyTitle.view.closeBtn.onClick:RemoveAllListeners()
    panelItem.uiCtrl.view.domainTopMoneyTitle.view.closeBtn.onClick:AddListener(function()
        Notify(MessageConst.ON_CLOSE_DOMAIN_DEPOT_TAB)
    end)

    self.m_curPanelItem = panelItem
    self:_BindPlaceHolder()
end




PhaseDomainDepotPackage.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseDomainDepotPackage._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if self.arg == nil then
        self.arg = {
            domainId = ScopeUtil.GetCurrentChapterIdAsStr()
        }
    end

    self.m_tabPanel = self:CreatePhasePanelItem(PanelId.DomainDepotTab, { domainId = self.arg.domainId })
    self.m_panelItemDic = {}
    self.m_pendingPanelArgMap = {}

    if self.arg.resumeState then
        self:_RestoreByResumeState(self.arg.resumeState)
    else
        Notify(MessageConst.ON_CHANGE_DOMAIN_DEPOT_TAB, self.arg)
    end


    if self.arg.resumeOpenPanel then
        for _, panelInfo in ipairs(self.arg.resumeOpenPanel) do
            UIManager:Open(panelInfo.panelId, panelInfo.arg)
        end
    end

    self.arg.resumeState = nil
    self.arg.resumeOpenPanel = nil
end

PhaseDomainDepotPackage._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseDomainDepotPackage._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseDomainDepotPackage._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end







PhaseDomainDepotPackage._OnInit = HL.Override() << function(self)
    PhaseDomainDepotPackage.Super._OnInit(self)

    self.m_popAsyncActionHelper = require_ex("Common/Utils/AsyncActionHelper")(true)
end

PhaseDomainDepotPackage._OnActivated = HL.Override() << function(self)
end

PhaseDomainDepotPackage._OnDeActivated = HL.Override() << function(self)
end

PhaseDomainDepotPackage._OnDestroy = HL.Override() << function(self)
    self.m_popAsyncActionHelper:Clear()
    self.m_popAsyncActionHelper = nil

    PhaseDomainDepotPackage.Super._OnDestroy(self)
end

PhaseDomainDepotPackage._OnRefresh = HL.Override() << function(self)
    local removeFunc = function(panel)
        if panel ~= nil and panel.uiCtrl ~= nil then
            self:RemovePhasePanelItem(panel)
        end
    end
    removeFunc(self.m_backPanel)
    self.m_backPanel = nil
    removeFunc(self.m_typePanel)
    self.m_typePanel = nil
    removeFunc(self.m_itemPanel)
    self.m_itemPanel = nil
    removeFunc(self.m_sellPanel)
    self.m_sellPanel = nil

    self.m_tabPanel.uiCtrl:ForceResetTab()
end

PhaseDomainDepotPackage._PlayAnimationOutAndRemovePhaseItem = HL.Method(HL.Forward("PhasePanelItem"), HL.Opt(HL.Function)) << function(self, phaseItem, onRemove)
    local removeCallback = function()
        if onRemove ~= nil then
            onRemove()
        end
    end

    if phaseItem and phaseItem.uiCtrl and not phaseItem.uiCtrl:IsPlayingAnimationOut() then
        phaseItem.uiCtrl:PlayAnimationOutWithCallback(function()
            self:RemovePhasePanelItem(phaseItem)
            removeCallback()
        end)
    else
        removeCallback()
    end
end

PhaseDomainDepotPackage._AsyncPlayAnimationOutAndRemovePhaseItem = HL.Method(HL.Forward("PhasePanelItem")) << function(self, phaseItem)
    self.m_popAsyncActionHelper:AddAction(function(onComplete)
        self:_PlayAnimationOutAndRemovePhaseItem(phaseItem, function()
            onComplete()
        end)
    end)
end

PhaseDomainDepotPackage._AsyncPlayBackAnimationOutByStageAndRemovePhaseItem = HL.Method(HL.Number, HL.Opt(HL.Function)) << function(self, stage)
    self.m_popAsyncActionHelper:AddAction(function(onComplete)

        if self.m_backPanel == nil or self.m_backPanel.uiCtrl == nil then
            onComplete()
            return
        end
        self.m_backPanel.uiCtrl:PlayAnimationByStage(stage, false, function()
            self:RemovePhasePanelItem(self.m_backPanel)
            self.m_backPanel = nil
            onComplete()
        end)

    end)
end

PhaseDomainDepotPackage._AsyncPlayBackAnimationOutByStage = HL.Method(HL.Number) << function(self, stage)
    self.m_popAsyncActionHelper:AddAction(function(onComplete)

        if self.m_backPanel == nil or self.m_backPanel.uiCtrl == nil then
            onComplete()
            return
        end
        self.m_backPanel.uiCtrl:PlayAnimationByStage(stage, false, function()
            onComplete()
        end)
    end)
end

PhaseDomainDepotPackage._BindPlaceHolder = HL.Method() << function(self)
    if not self.m_tabPanel then
        return
    end

    local tabCtrl = self.m_tabPanel.uiCtrl
    if tabCtrl then
        self.m_curPanelItem.uiCtrl.view.controllerHintPlaceholder:InitControllerHintPlaceholder({
            tabCtrl.view.inputGroup.groupId,
            self.m_curPanelItem.uiCtrl.view.inputGroup.groupId,
        })
    end
    self.m_curPanelItem.uiCtrl.view.domainTopMoneyTitle:InitDomainTopMoneyTitle(self.arg.domainId)
end




PhaseDomainDepotPackage.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = lume.deepCopy(self.arg or {})
    arg.resumeState = self:_CollectResumeState()
    local resumeOpenPanel = self:_CollectResumeOpenPanel()
    arg.resumeOpenPanel = #resumeOpenPanel > 0 and resumeOpenPanel or nil
    return arg
end


PhaseDomainDepotPackage._CollectResumeState = HL.Method().Return(HL.Table) << function(self)
    local topPanelItem = self.m_curPanelItem
    if topPanelItem == nil and self.m_panelItemDic ~= nil then
        topPanelItem = self.m_panelItemDic[self.m_lastTabPanelId]
    end
    local topPanelState = self:_CollectPanelState(topPanelItem) or lume.deepCopy(self.m_lastTopPanelState)
    return {
        selectedTabIndex = self:_GetTabIndexByPanelId(self.m_lastTabPanelId),
        topPanelState = topPanelState,
        panelStates = self:_CollectOpenPanelStates(),
    }
end


PhaseDomainDepotPackage._CollectPanelState = HL.Method(HL.Opt(HL.Forward("PhasePanelItem"))).Return(HL.Table) << function(self, panelItem)
    if panelItem == nil or panelItem.uiCtrl == nil then
        return nil
    end

    local uiCtrl = panelItem.uiCtrl
    if not uiCtrl then
        return nil
    end
    local arg
    if HL.TryGet(uiCtrl, "GetCurStateArg") then
        arg = uiCtrl:GetCurStateArg()
    elseif HL.TryGet(uiCtrl, "GetCurPhaseStateArg") then
        arg = uiCtrl:GetCurPhaseStateArg()
    end

    return {
        panelId = uiCtrl.panelId,
        arg = arg,
    }
end


PhaseDomainDepotPackage._CollectOpenPanelStates = HL.Method().Return(HL.Table) << function(self)
    local panelStates = {}
    local panelItems = {
        self.m_typePanel,
        self.m_itemPanel,
        self.m_showSellAnimPanel,
        self.m_sellPanel,
        self.m_sellEndPanel,
    }
    for _, panelItem in pairs(panelItems) do
        local panelState = self:_CollectPanelState(panelItem)
        if panelState ~= nil then
            table.insert(panelStates, panelState)
        end
    end
    return panelStates
end


PhaseDomainDepotPackage._CollectResumeOpenPanel = HL.Method().Return(HL.Table) << function(self)
    local resumeOpenPanel = {}
    local canResumeInstructionBook = self.m_lastTabPanelId == PanelId.DomainDepotDelivery or self.m_lastTabPanelId == PanelId.DomainDepotMyOrder
    if canResumeInstructionBook then
        local isOpen, instructionBookCtrl = UIManager:IsOpen(PanelId.InstructionBook)
        if isOpen and instructionBookCtrl then
            table.insert(resumeOpenPanel, {
                panelId = PanelId.InstructionBook,
                arg = { id = instructionBookCtrl.id }
            })
        end
    end
    return resumeOpenPanel
end


PhaseDomainDepotPackage._RestoreByResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    self:_RestoreTopPanelState(resumeState)
    local panelStates = resumeState and resumeState.panelStates or nil
    if panelStates == nil then
        return
    end
    for _, panelState in ipairs(panelStates) do
        self:_RestorePanelState(panelState)
    end
end


PhaseDomainDepotPackage._RestoreTopPanelState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    local topPanelState = resumeState and resumeState.topPanelState or nil
    local selectedTabIndex = resumeState and resumeState.selectedTabIndex or 1
    if topPanelState and topPanelState.panelId then
        self.m_lastTabPanelId = topPanelState.panelId
        self.m_pendingPanelArgMap[topPanelState.panelId] = topPanelState.arg
    end
    Notify(MessageConst.ON_CHANGE_DOMAIN_DEPOT_TAB, {
        domainId = self.arg.domainId,
        index = selectedTabIndex,
    })
end


PhaseDomainDepotPackage._RestorePanelState = HL.Method(HL.Table) << function(self, panelState)
    if panelState == nil or panelState.panelId == nil then
        return
    end

    local arg = lume.deepCopy(panelState.arg or {})
    if panelState.panelId == PanelId.DomainDepotGoodsType then
        self:OnOpenPackTypeSelectPanel(arg)
    elseif panelState.panelId == PanelId.DomainDepotGoodsPack then
        self:OnOpenPackItemSelectPanel(arg)
    elseif panelState.panelId == PanelId.DomainDepotPackBidPrice then
        self:OnOpenSellAnimPanel({ arg.domainDepotId })
    elseif panelState.panelId == PanelId.DomainDepotPackageSell then
        arg.simpleOpen = true
        self:OnOpenPackSellPanel(arg)
    elseif panelState.panelId == PanelId.DomainDepotGoodsSettle then
        self:OnPackSellEndPanel({ arg.deliverInstId, arg.stateName })
    end
end

PhaseDomainDepotPackage._GetTabIndexByPanelId = HL.Method(HL.Opt(HL.Number)).Return(HL.Number) << function(self, panelId)
    return TOP_PANEL_ID_TO_TAB_INDEX[panelId] or 1
end




PhaseDomainDepotPackage.OnOpenPackTypeSelectPanel = HL.Method(HL.Table) << function(self, args)
    self.m_backPanel = self:CreatePhasePanelItem(PanelId.DomainDepotPackBackGround)

    self.m_typePanel = self:CreatePhasePanelItem(PanelId.DomainDepotGoodsType, {
        depotId = args.depotId,
        pack = self.m_backPanel.uiCtrl.view.domainDepotPack,
        backPanel = self.m_backPanel.uiCtrl,
        resumeState = args.resumeState,
    })
    self.m_backPanel.uiCtrl:OnGoodsPack()
end

PhaseDomainDepotPackage.OnClosePackTypeSelectPanel = HL.Method() << function(self)
    self:_PlayAnimationOutAndRemovePhaseItem(self.m_backPanel)
    self:_PlayAnimationOutAndRemovePhaseItem(self.m_typePanel)
end

PhaseDomainDepotPackage.OnOpenPackItemSelectPanel = HL.Method(HL.Table) << function(self, args)
    if self.m_typePanel.uiCtrl:IsPlayingAnimationOut() then
        self.m_typePanel.uiCtrl:Hide()
    else
        self.m_typePanel.uiCtrl:PlayAnimationOutAndHide()
    end
    args.pack = self.m_backPanel.uiCtrl.view.domainDepotPack
    self.m_backPanel.uiCtrl:ChangePackItemType(GEnums.DomainDepotDeliverItemType.Misc)
    self.m_itemPanel = self:CreatePhasePanelItem(PanelId.DomainDepotGoodsPack, args)
end

PhaseDomainDepotPackage.OnClosePackItemSelectPanel = HL.Method() << function(self)
    self:_PlayAnimationOutAndRemovePhaseItem(self.m_typePanel)
    self:_PlayAnimationOutAndRemovePhaseItem(self.m_itemPanel)
    self:_PlayAnimationOutAndRemovePhaseItem(self.m_backPanel)
end

PhaseDomainDepotPackage.OnBackToPackTypeSelectPanel = HL.Method() << function(self)
    self:_PlayAnimationOutAndRemovePhaseItem(self.m_itemPanel)
    if self.m_typePanel.uiCtrl:IsPlayingAnimationOut() then
        self.m_typePanel.uiCtrl:Hide()
    end
    self.m_typePanel.uiCtrl:Show()
end

PhaseDomainDepotPackage.OnOpenSellAnimPanel = HL.Method(HL.Any) << function(self, args)
    if self.m_backPanel == nil or self.m_backPanel.uiCtrl == nil then
        self.m_backPanel = self:CreatePhasePanelItem(PanelId.DomainDepotPackBackGround)
    end

    local removeFinishAndOpen = function()
        local domainDepotId = unpack(args)
        self.m_showSellAnimPanel = self:CreatePhasePanelItem(PanelId.DomainDepotPackBidPrice, { domainDepotId = domainDepotId, pack = self.m_backPanel.uiCtrl.view.domainDepotPack })
        self.m_backPanel.uiCtrl:OnPackBackGround()
    end

    self.m_popAsyncActionHelper:Clear()
    self.m_popAsyncActionHelper:SetOnFinished(function()
        removeFinishAndOpen()
    end)

    self:_AsyncPlayBackAnimationOutByStage(DOMAIN_DEPOT_BACKGROUND_STAGES.Pack)
    self:_AsyncPlayAnimationOutAndRemovePhaseItem(self.m_itemPanel)
    self:_AsyncPlayAnimationOutAndRemovePhaseItem(self.m_typePanel)

    self.m_popAsyncActionHelper:Start()
end

PhaseDomainDepotPackage.OnOpenPackSellPanel = HL.Method(HL.Any) << function(self, args)
    if self.m_backPanel == nil or self.m_backPanel.uiCtrl == nil then
        self.m_backPanel = self:CreatePhasePanelItem(PanelId.DomainDepotPackBackGround)
    end

    local removeFinishAndOpen = function()
        self.m_sellPanel = self:CreatePhasePanelItem(PanelId.DomainDepotPackageSell, {
            domainDepotId = args.domainDepotId,
            resumeState = args.resumeState,
        })
        self.m_backPanel.uiCtrl:OnPackageSell()
    end

    if args.simpleOpen then
        removeFinishAndOpen()
        local deliverInfo = GameInstance.player.domainDepotSystem:GetDomainDepotDeliverInfoByDepotId(args.domainDepotId)
        self.m_backPanel.uiCtrl:InitPackageSellBgNode(deliverInfo)
    else
        self.m_popAsyncActionHelper:Clear()
        self.m_popAsyncActionHelper:SetOnFinished(function()
            removeFinishAndOpen()
        end)

        self:_AsyncPlayBackAnimationOutByStage(DOMAIN_DEPOT_BACKGROUND_STAGES.WaitSelectBuyer)
        self:_AsyncPlayAnimationOutAndRemovePhaseItem(self.m_showSellAnimPanel)

        self.m_popAsyncActionHelper:Start()
    end
end

PhaseDomainDepotPackage.OnClosePackSellPanel = HL.Method() << function(self)
    self:_PlayAnimationOutAndRemovePhaseItem(self.m_backPanel)
    self:_PlayAnimationOutAndRemovePhaseItem(self.m_showSellAnimPanel)
    self:_PlayAnimationOutAndRemovePhaseItem(self.m_sellPanel)
end

PhaseDomainDepotPackage.OnPackSellEndPanel = HL.Method(HL.Any) << function(self, args)
    if self.m_backPanel == nil or self.m_backPanel.uiCtrl == nil then
        self.m_backPanel = self:CreatePhasePanelItem(PanelId.DomainDepotPackBackGround)
    end

    self:_PlayAnimationOutAndRemovePhaseItem(self.m_sellPanel)
    self:_PlayAnimationOutAndRemovePhaseItem(self.m_showSellAnimPanel)

    local deliverInstId, stateName = unpack(args)
    local deliverInfo = GameInstance.player.domainDepotSystem:GetDomainDepotDeliverInfoByInstId(deliverInstId)
    self.m_backPanel.uiCtrl:OnGoodsSettle(deliverInstId)

    local panelArgs = { deliverInstId = deliverInstId, stateName = stateName }
    self.m_sellEndPanel = self:CreatePhasePanelItem(PanelId.DomainDepotGoodsSettle, panelArgs)
    self.m_lastTopPanelState = self:_CollectPanelState(self.m_curPanelItem)


    if self.m_tabPanel then
        self:RemovePhasePanelItem(self.m_tabPanel)
    end
    if self.m_curPanelItem then
        self:RemovePhasePanelItem(self.m_curPanelItem)
    end
end

PhaseDomainDepotPackage.OnClosePackSellEndPanel = HL.Method() << function(self)
    self.m_popAsyncActionHelper:Clear()
    self.m_popAsyncActionHelper:SetOnFinished(function()
        Notify(MessageConst.RECOVER_PHASE_LEVEL)
    end)


    self:_AsyncPlayBackAnimationOutByStageAndRemovePhaseItem(DOMAIN_DEPOT_BACKGROUND_STAGES.FinishSelectBuyer)
    self:_AsyncPlayAnimationOutAndRemovePhaseItem(self.m_sellEndPanel)

    self.m_popAsyncActionHelper:Start()
end




HL.Commit(PhaseDomainDepotPackage)

