local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local ActionOnSetNaviTarget = CS.Beyond.Input.ActionOnSetNaviTarget

BottomCustomizeItemList = HL.Class('BottomCustomizeItemList', UIWidgetBase)

BottomCustomizeItemList.m_panelId = HL.Field(HL.Number) << 0

BottomCustomizeItemList.m_controllerHintPlaceholder = HL.Field(HL.Userdata)

BottomCustomizeItemList.m_getItemCell = HL.Field(HL.Function)

BottomCustomizeItemList.m_selectedIndex = HL.Field(HL.Number) << 1

BottomCustomizeItemList.m_itemInfos = HL.Field(HL.Table)

BottomCustomizeItemList.m_tryChangeBagItemNumCallback = HL.Field(HL.Function)

BottomCustomizeItemList.m_onBagItemNumChangedCallback = HL.Field(HL.Function)

BottomCustomizeItemList.m_onConfirmCallback = HL.Field(HL.Function)


BottomCustomizeItemList._OnFirstTimeInit = HL.Override() << function(self)
    self.view.itemScrollRect.OnScrollStart:AddListener(function()
        self:_TryHideItemTips()
    end)
    self.view.itemScrollRect.onValueChanged:AddListener(function()
        if self.view.itemScrollRect.dragging then
            self:_TryHideItemTips()
        end
    end)
end

BottomCustomizeItemList.InitBottomCustomizeItemList = HL.Method() << function(self)
    self:_FirstTimeInit()
end

BottomCustomizeItemList.Init = HL.Method(HL.Number, HL.Table, HL.Userdata, HL.Function, HL.Function, HL.Function) <<
    function(self, panelId, itemInfoList, controllerHintPlaceholder, tryChangeNumCallback, onNumChangedCallback,
    onConfirmCallback)
    self:InitBottomCustomizeItemList()
    self.m_panelId = panelId
    self.m_itemInfos = itemInfoList
    self.m_controllerHintPlaceholder = controllerHintPlaceholder
    self.m_tryChangeBagItemNumCallback = tryChangeNumCallback
    self.m_onBagItemNumChangedCallback = onNumChangedCallback
    self.m_onConfirmCallback = onConfirmCallback
end

BottomCustomizeItemList.SetActive = HL.Method(HL.Boolean) << function(self, active)
    if self.gameObject.activeSelf == active then
        return
    end

    if not active then
        self:ClearNaviTarget()
        self.view.animationWrapper:PlayOutAnimation(function()
            self.gameObject:SetActiveIfNecessary(false)
            Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.inputGroup.groupId)
        end)
        return
    end

    self.gameObject:SetActiveIfNecessary(true)
    Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
        panelId = self.m_panelId,
        isGroup = true,
        id = self.view.inputGroup.groupId,
        hintPlaceholder = self.m_controllerHintPlaceholder,
        rectTransform = self.rectTransform,
        noHighlight = true,
    })

    if not self.m_getItemCell then
        self.m_getItemCell = UIUtils.genCachedCellFunction(self.view.itemScrollList)
        self.view.itemScrollList.onUpdateCell:AddListener(function(obj, csIndex)
            self:_OnUpdateItemCell(self.m_getItemCell(obj), LuaIndex(csIndex))
        end)
        self.view.closeBtn.onClick:AddListener(function()
            if self:_TryHideItemTips() then
                return
            end
            self:SetActive(false)
        end)
        self.view.confirmBtn.onClick:AddListener(function()
            self.m_onConfirmCallback()
            self:SetActive(false)
        end)
    end

    local count = #self.m_itemInfos
    self.view.itemScrollList:ScrollToIndex(CSIndex(self.m_selectedIndex), true)
    self.view.itemScrollList:UpdateCount(count)
    self.view.emptyTarget.gameObject:SetActiveIfNecessary(count == 0)
end

BottomCustomizeItemList.ResetData = HL.Method(HL.Table) << function(self, itemInfoList)
    self.m_itemInfos = itemInfoList
end

BottomCustomizeItemList.OnPanelClose = HL.Method() << function(self)
    if self.gameObject.activeSelf then
        Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.inputGroup.groupId)
    end
end

BottomCustomizeItemList._OnUpdateItemCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local info = self.m_itemInfos[index]
    cell:InitItemCellForSelect({
        itemBundle = info,
        curNum = info.selectedCount,
        tryChangeNum = function(newNum)
            return self.m_tryChangeBagItemNumCallback(index, newNum)
        end,
        onNumChanged = function(newNum)
            self.m_onBagItemNumChangedCallback(index, newNum)
        end,
        bindInputChangeNum = true,
    })

    cell.view.item.view.button.onClick:AddListener(function()
        if not cell.view.item.showingTips then
            cell.view.item:ShowTips({
                tipsPosTransform = cell.view.tipsPos,
                tipsPosType = UIConst.UI_TIPS_POS_TYPE.RightDown,
                safeArea = cell.transform,
                isSideTips = true,
            })
        end
    end)
    cell.view.item.view.button:ChangeActionOnSetNaviTarget(ActionOnSetNaviTarget.None)
    cell.view.item.view.button.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self.m_selectedIndex = index
        end
    end
    cell.view.gameObject.name = info.id
    if index == self.m_selectedIndex then
        self:SetNaviTarget(cell.view.item.view.button)
    end
end

BottomCustomizeItemList._TryHideItemTips = HL.Method().Return(HL.Boolean) << function(self)
    local isShow = UIManager:IsShow(PanelId.ItemTips)
    if isShow then
        Notify(MessageConst.HIDE_ITEM_TIPS)
    end
    return isShow
end

BottomCustomizeItemList._OnSelectNumChanged = HL.Method(HL.Number, HL.Number) << function(self, index, newNum)
    local info = self.m_bagNodeItemInfos[index]
    info.selectedCount = newNum
    if not self.m_isFilling then
        if newNum == 0 then
            if string.isEmpty(self.m_csInfo.itemId) then
                for k, v in ipairs(self.m_bagNodeItemInfos) do
                    if v.selectedCount > 0 then

                        return
                    end
                end

                self.m_bagNodeSelectTargetLiquidId = nil
                self:_UpdateBagNodeAllItemValidStateOnSelect()
            end
        elseif not self.m_bagNodeSelectTargetLiquidId then
            self.m_bagNodeSelectTargetLiquidId = info.liquidId
            self:_UpdateBagNodeAllItemValidStateOnSelect()
        end
    end
end

HL.Commit(BottomCustomizeItemList)
return BottomCustomizeItemList

