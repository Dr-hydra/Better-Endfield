local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
WikiDetailBaseCtrl = HL.Class('WikiDetailBaseCtrl', uiCtrl.UICtrl)






WikiDetailBaseCtrl.s_messages = HL.StaticField(HL.Table) << {

}


WikiDetailBaseCtrl.m_wikiEntryShowData = HL.Field(HL.Table)

WikiDetailBaseCtrl.m_arg = HL.Field(HL.Table)

WikiDetailBaseCtrl.m_wikiGroupShowDataList = HL.Field(HL.Table)

WikiDetailBaseCtrl.m_itemTipsPosInfo = HL.Field(HL.Table)

WikiDetailBaseCtrl.m_needHideModel = HL.Field(HL.Boolean) << true



WikiDetailBaseCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitController()
    self.m_arg = arg or {}


    local args = arg
    self.m_wikiEntryShowData = args.wikiEntryShowData
    self.m_wikiGroupShowDataList = args.wikiGroupShowDataList

    self.m_itemTipsPosInfo = {
        tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
        isSideTips = false,
    }
    if self.view.right and self.view.right.itemTipsNode then
        self.m_itemTipsPosInfo.tipsPosTransform = self.view.right.itemTipsNode
    end

    if self.view.prtsBtn then
        self.view.prtsBtn.btn.onClick:AddListener(function()
            PhaseManager:GoToPhase(PhaseId.PRTSStoryCollDetail, {
                id = self.m_wikiEntryShowData.wikiEntryData.prtsId,
                isFirstLvId = false,
            })
        end)
    end

    self:_RefreshLeft()
    self:_RefreshCenter()
    self:_RefreshRight()
    self:_ApplyResumeState(args and args.resumeState or nil)
    if self.m_arg then
        self.m_arg.resumeState = nil
    end

    self:_RefreshTop()

    EventLogManagerInst:GameEvent_WikiEntry(self.m_wikiEntryShowData.wikiCategoryType,
        self.m_wikiEntryShowData.wikiGroupData.groupId, self.m_wikiEntryShowData.wikiEntryData.id)
end

WikiDetailBaseCtrl.OnClose = HL.Override() << function(self)
    if self.m_phase then
        self.m_phase:DestroyModel()
    end
    self:GameEventLogExit()
end

WikiDetailBaseCtrl.OnShow = HL.Override() << function(self)
    if self.m_phase then
        self.m_phase:ActiveModelRotateRoot(true)
        if self.m_phase.m_currentWikiDetailArgs and
            self.m_phase.m_currentWikiDetailArgs.wikiEntryShowData.wikiEntryData.id ~= self.m_wikiEntryShowData.wikiEntryData.id then
            self:Refresh(self.m_phase.m_currentWikiDetailArgs)
        end
    end
    self.m_needHideModel = true
    self:GameEventLogEnter()
end

WikiDetailBaseCtrl.OnHide = HL.Override() << function(self)
    if self.m_needHideModel and self.m_phase then
        self.m_phase:ActiveModelRotateRoot(false)
    end
    self:GameEventLogExit()
end





WikiDetailBaseCtrl.GetPanelId = HL.Virtual().Return(HL.Number) << function(self)

end

WikiDetailBaseCtrl._RefreshTop = HL.Virtual() << function(self)
    if self.m_phase == nil then
        return
    end

    local wikiTopArgs = {
        phase = self.m_phase,
        panelId = self:GetPanelId(),
        categoryType = self.m_wikiEntryShowData.wikiCategoryType,
        wikiEntryShowData = self.m_wikiEntryShowData
    }
    self.view.top:InitWikiTop(wikiTopArgs)
end

WikiDetailBaseCtrl._RefreshCenter = HL.Virtual() << function(self)

    local args = {
        wikiEntryShowData = self.m_wikiEntryShowData,
        onDetailBtnClick = function()
            self:PlayAnimationOutWithCallback(function()
                self.m_needHideModel = false
                self.m_phase:CreatePhasePanelItem(PanelId.WikiModelShow, self.m_wikiEntryShowData)
            end)
        end
    }
    self.view.wikiItemInfo:InitWikiItemInfo(args)
    if self.view.prtsBtn then
        local prtsId = self.m_wikiEntryShowData.wikiEntryData.prtsId
        if not string.isEmpty(prtsId) then
            local _, prtsData = Tables.prtsAllItem:TryGetValue(prtsId)
            if prtsData then
                self.view.prtsBtn.titleTxt.text = prtsData.name
            end
        end
        self.view.prtsBtn.gameObject:SetActive(Utils.isSystemUnlocked(GEnums.UnlockSystemType.PRTS) and
            not string.isEmpty(prtsId) and GameInstance.player.prts:IsPrtsUnlocked(prtsId))
    end
end

WikiDetailBaseCtrl._RefreshLeft = HL.Virtual() << function(self)

    local wikiGroupItemListArgs = {
        isInitHidden = true,
        wikiGroupShowDataList = self.m_wikiGroupShowDataList,
        onItemClicked = function(wikiEntryShowData)
            self.m_wikiEntryShowData = wikiEntryShowData
            if self.m_phase then
                self.m_phase.m_currentWikiDetailArgs = {
                    categoryType = self.m_wikiEntryShowData.wikiCategoryType,
                    wikiEntryShowData = self.m_wikiEntryShowData,
                    wikiGroupShowDataList = self.m_wikiGroupShowDataList
                }
            end
            self:_RefreshTop()
            self:_RefreshCenter()
            self:_RefreshRight()
        end,
        onGetSelectedEntryShowData = function()
            return self.m_wikiEntryShowData
        end,
        btnExpandList = self.view.expandListBtn,
        btnClose = self.view.btnEmpty,
        wikiItemInfo = self.view.wikiItemInfo,
    }
    self.view.left:InitWikiGroupItemList(wikiGroupItemListArgs)
end

WikiDetailBaseCtrl._RefreshRight = HL.Virtual() << function(self)

end

WikiDetailBaseCtrl._CollectLocalResumeState = HL.Virtual().Return(HL.Table) << function(self)
    return {}
end

WikiDetailBaseCtrl._ApplyLocalResumeState = HL.Virtual(HL.Opt(HL.Any)) << function(self, resumeState)
end



WikiDetailBaseCtrl.Refresh = HL.Method(HL.Table) << function(self, args)
    self.m_arg = args or {}
    if self.view.right.naviGroup then
        self.view.right.naviGroup:ManuallyStopFocus()
        if self.view.left.gameObject.activeSelf then
            self.view.left:_OnCloseBtnClicked(true)
        end
    end
    self.m_wikiEntryShowData = args.wikiEntryShowData
    self.m_wikiGroupShowDataList = args.wikiGroupShowDataList

    self:_RefreshTop()
    self:_RefreshCenter()
    self:_RefreshRight()
    self:_ApplyResumeState(args and args.resumeState or nil)
    if self.m_arg then
        self.m_arg.resumeState = nil
    end
end

WikiDetailBaseCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.m_arg and lume.deepCopy(self.m_arg) or {}
    arg.categoryType = self.m_wikiEntryShowData and self.m_wikiEntryShowData.wikiCategoryType or arg.categoryType
    arg.wikiEntryShowData = self.m_wikiEntryShowData
    arg.wikiGroupShowDataList = self.m_wikiGroupShowDataList
    arg.resumeState = self:_CollectResumeState()
    return arg
end

WikiDetailBaseCtrl._CollectResumeState = HL.Method().Return(HL.Table) << function(self)
    return {
        groupItemList = self.view.left and self.view.left.GetResumeState and self.view.left:GetResumeState() or nil,
        localState = self:_CollectLocalResumeState(),
    }
end

WikiDetailBaseCtrl._ApplyResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    if not resumeState then
        return
    end
    if self.view.left and self.view.left.ApplyResumeState then
        self.view.left:ApplyResumeState(resumeState.groupItemList)
    end
    self:_ApplyLocalResumeState(resumeState.localState)
end



WikiDetailBaseCtrl.m_enterTime = HL.Field(HL.Number) << -1

WikiDetailBaseCtrl.GameEventLogEnter = HL.Method() << function(self)
    self.m_enterTime = Time.realtimeSinceStartup
    EventLogManagerInst:GameEvent_WikiCategory(true, self.m_wikiEntryShowData.wikiCategoryType, 0)
end

WikiDetailBaseCtrl.GameEventLogExit = HL.Method() << function(self)
    if self.m_enterTime < 0 then
        return
    end
    local stayTime = Time.realtimeSinceStartup - self.m_enterTime
    self.m_enterTime = -1
    EventLogManagerInst:GameEvent_WikiCategory(false, self.m_wikiEntryShowData.wikiCategoryType, stayTime)
end





WikiDetailBaseCtrl.m_closeItemTipsBindingId = HL.Field(HL.Number) << -1

WikiDetailBaseCtrl.m_currentNaviCraftCellView = HL.Field(HL.Table)

WikiDetailBaseCtrl._InitController = HL.Virtual() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    if self.view.right.naviGroup then
        self.view.right.naviGroup.onIsFocusedChange:AddListener(function(isFocused)
            self.view.right.controllerFocusHintNode.gameObject:SetActive(not isFocused)
            if not isFocused then
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
        end)
    end
    UIUtils.bindHyperlinkPopup(self, "wiki_detail_right", self.view.inputGroup.groupId)
    if self.view.selectableNaviGroup then
        self.view.selectableNaviGroup:NaviToThisGroup()
    end
    if self.view.right.inputGroup then
        self.m_closeItemTipsBindingId = self:BindInputPlayerAction("common_cancel_no_hint", function()
            Notify(MessageConst.HIDE_ITEM_TIPS)
            InputManagerInst:ToggleBinding(self.m_closeItemTipsBindingId, false)
        end, self.view.right.inputGroup.groupId)
        InputManagerInst:ToggleBinding(self.m_closeItemTipsBindingId, false)
    end
end

WikiDetailBaseCtrl._InitItemObtainWaysController = HL.Method(HL.Userdata).Return(CS.UnityEngine.UI.Selectable, CS.Beyond.UI.UISelectableNaviGroup) << function(self, itemObtainWaysForWiki)
    local lastCraftFirstSelectable = nil
    local lastNaviGroup = nil
    for i = 1, itemObtainWaysForWiki.m_obtainCells:GetCount() do
        local obtainCell = itemObtainWaysForWiki.m_obtainCells:GetItem(i)
        obtainCell.normalNode.button.onIsNaviTargetChanged = function(isTarget)
            if isTarget then
                self.view.right.scrollRect:ScrollToNaviTarget(obtainCell.normalNode.button)
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
        end
        obtainCell.normalNode.button.useExplicitNaviSelect = true
        if lastCraftFirstSelectable then
            obtainCell.normalNode.button.banExplicitOnUp = false
            obtainCell.normalNode.button:SetExplicitSelectOnUp(lastCraftFirstSelectable)
            lastCraftFirstSelectable.useExplicitNaviSelect = true
            lastCraftFirstSelectable.banExplicitOnLeft = true
            lastCraftFirstSelectable.banExplicitOnRight = true
            lastCraftFirstSelectable.banExplicitOnUp = true
            lastCraftFirstSelectable.banExplicitOnDown = false
            lastCraftFirstSelectable:SetExplicitSelectOnDown(obtainCell.normalNode.button)
        else
            obtainCell.normalNode.button.banExplicitOnUp = true
        end
        local firstItemSelectable
        for j = 1, obtainCell.craftCells:GetCount() do
            local craftCell = obtainCell.craftCells:GetItem(j)
            craftCell.pinKeyHint.gameObject:SetActive(false)
            if DeviceInfo.usingController then
                InputManagerInst:ToggleBinding(craftCell.pinBtn.view.pinToggle.toggleBindingId, false)
            end
            for k = 1, craftCell.itemCells:GetCount() do
                local itemCell = craftCell.itemCells:GetItem(k)
                local selectable = itemCell.view.button
                selectable.useExplicitNaviSelect = false
                selectable.onIsNaviTargetChanged = function(isTarget)
                    self:_OnRightItemIsNaviTargetChanged(isTarget, selectable, craftCell)
                end
                if firstItemSelectable == nil then
                    firstItemSelectable = selectable
                end
                if k == 1 then
                    lastCraftFirstSelectable = selectable
                end
            end
            craftCell.selectableNaviGroup.naviPartnerOnDown:Clear()
            lastNaviGroup = craftCell.selectableNaviGroup
        end
        if firstItemSelectable then
            obtainCell.normalNode.button.banExplicitOnDown = false
            obtainCell.normalNode.button:SetExplicitSelectOnDown(firstItemSelectable)
            firstItemSelectable.useExplicitNaviSelect = true
            firstItemSelectable.banExplicitOnLeft = true
            firstItemSelectable.banExplicitOnRight = true
            firstItemSelectable.banExplicitOnDown = true
            firstItemSelectable.banExplicitOnUp = false
            firstItemSelectable:SetExplicitSelectOnUp(obtainCell.normalNode.button)
        else
            obtainCell.normalNode.button.banExplicitOnDown = true
        end
    end
    itemObtainWaysForWiki.view.emptyNode.button.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self.view.right.scrollRect:ScrollToNaviTarget(itemObtainWaysForWiki.view.emptyNode.button)
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end
    itemObtainWaysForWiki.view.selectableNaviGroup.naviPartnerOnDown:Clear()
    if not lastNaviGroup then
        lastNaviGroup = itemObtainWaysForWiki.view.selectableNaviGroup
    end
    if itemObtainWaysForWiki.view.emptyNode.gameObject.activeSelf then
        return itemObtainWaysForWiki.view.emptyNode.button, nil
    end
    return lastCraftFirstSelectable, lastNaviGroup
end

WikiDetailBaseCtrl._OnRightItemIsNaviTargetChanged = HL.Method(HL.Boolean, HL.Userdata, HL.Opt(HL.Table)) << function(self, isTarget, selectable, craftCellView)
    if not DeviceInfo.usingController then
        return
    end
    local isItemTipsShown = UIManager:IsShow(PanelId.ItemTips)
    if craftCellView and not isItemTipsShown and craftCellView.pinBtn.gameObject.activeSelf then
        InputManagerInst:ToggleBinding(craftCellView.pinBtn.view.pinToggle.toggleBindingId, isTarget)
        craftCellView.pinKeyHint.gameObject:SetActive(isTarget)
    end
    if isTarget then
        if self.view.right.scrollRect then
            self.view.right.scrollRect:ScrollToNaviTarget(selectable)
        end
        if isItemTipsShown then
            selectable.onClick:Invoke(nil)
            InputManagerInst:ToggleBinding(selectable.hoverConfirmBindingId, false)
            InputManagerInst:ToggleBinding(self.view.right.naviGroup.StopFocusBindingId, false)
        end
    end
    self.m_itemTipsPosInfo.isSideTips = UIManager:IsShow(PanelId.ItemTips)
    self.m_currentNaviCraftCellView = isTarget and craftCellView or nil
end

WikiDetailBaseCtrl._OnClickRightItemCell = HL.Method(HL.Userdata, HL.Opt(HL.Table)) << function(self, cell, craftCellView)
    self.m_itemTipsPosInfo.isSideTips = true
    cell:ShowTips(nil, function()
        if not DeviceInfo.usingController or self.m_isClosed then
            return
        end
        if not UIManager:IsShow(PanelId.ItemTips) then
            if InputManagerInst.controllerNaviManager.curTarget == cell.view.button then
                InputManagerInst:ToggleBinding(cell.view.button.hoverConfirmBindingId, true)
            end
            InputManagerInst:ToggleBinding(self.view.right.naviGroup.StopFocusBindingId, true)
            InputManagerInst:ToggleBinding(self.m_closeItemTipsBindingId, false)
        end
        if self.m_currentNaviCraftCellView then
            if self.m_currentNaviCraftCellView.pinBtn.gameObject.activeSelf then
                InputManagerInst:ToggleBinding(self.m_currentNaviCraftCellView.pinBtn.view.pinToggle.toggleBindingId, true)
                self.m_currentNaviCraftCellView.pinKeyHint.gameObject:SetActive(true)
            end
        end
        self.m_itemTipsPosInfo.isSideTips = false
        local isItemTipsShown = UIManager:IsShow(PanelId.ItemTips)
        if not isItemTipsShown and InputManagerInst.controllerNaviManager.curTarget == cell.view.button then
            cell:_OnHoverChange(true)
        end
    end)
    if DeviceInfo.usingController then
        InputManagerInst:ToggleBinding(cell.view.button.hoverConfirmBindingId, false)
        InputManagerInst:ToggleBinding(self.m_closeItemTipsBindingId, true)
        InputManagerInst:ToggleBinding(self.view.right.naviGroup.StopFocusBindingId, false)
        if craftCellView then
            InputManagerInst:ToggleBinding(craftCellView.pinBtn.view.pinToggle.toggleBindingId, false)
            craftCellView.pinKeyHint.gameObject:SetActive(false)
        end
    end
end



HL.Commit(WikiDetailBaseCtrl)
