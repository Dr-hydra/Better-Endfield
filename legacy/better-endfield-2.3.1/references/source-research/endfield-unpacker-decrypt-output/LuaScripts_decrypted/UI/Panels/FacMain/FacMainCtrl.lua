local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacMain
FacMainCtrl = HL.Class('FacMainCtrl', uiCtrl.UICtrl)

local FORMULA_PIN_TOAST_TEXT_ID = "LUA_FORMULA_PIN_TOAST"
local FORMULA_CANCEL_PIN_TOAST_TEXT_ID = "LUA_FORMULA_CANCEL_PIN_TOAST"

FacMainCtrl.m_pinFormulaInCells = HL.Field(HL.Forward('UIListCache'))

FacMainCtrl.m_pinFormulaOutCells = HL.Field(HL.Forward('UIListCache'))

FacMainCtrl.m_pinFormulaDataSeq = HL.Field(HL.Table)

FacMainCtrl.m_isPinFormula = HL.Field(HL.Boolean) << false

FacMainCtrl.m_focusPinInfo = HL.Field(HL.Table) << nil

FacMainCtrl.m_cancelPinTimer = HL.Field(HL.Number) << 0

FacMainCtrl.m_isCancelPinLock = HL.Field(HL.Boolean) << false

FacMainCtrl.m_closeBindingId = HL.Field(HL.Number) << -1







FacMainCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_PIN_STATE_CHANGED] = '_OnPinStateChanged',
    [MessageConst.ON_SYSTEM_UNLOCK_CHANGED] = '_OnPinSystemLockedStateChanged',
    [MessageConst.ON_ENTER_FAC_MAIN_REGION] = '_OnEnterFacMainRegion',
    [MessageConst.ON_EXIT_FAC_MAIN_REGION] = '_OnExitFacMainRegion',

    [MessageConst.ON_ENTITY_ALL_CPT_START_DONE] = '_OnFacBuildingNodeStateChanged' ,
    [MessageConst.ON_REMOTE_FACTORY_ENTITY_REMOVED] = '_OnFacBuildingNodeStateChanged' ,


    [MessageConst.ON_TOGGLE_FAC_TOP_VIEW] = 'OnToggleFacTopView',
    [MessageConst.ON_FAC_TOP_VIEW_HIDE_UI_MODE_CHANGE] = 'OnFacTopViewHideUIModeChange',
    [MessageConst.FAC_ON_DEL_TIME_LIMITED_FORMULA] = "_OnFormulaDelete",
}

FacMainCtrl.m_needUpdatePin = HL.Field(HL.Boolean) << false


FacMainCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.facQuickBarPlaceHolder:InitFacQuickBarPlaceHolder()

    self:_InitPinNode()
    self:_UpdateAndRefreshPinFormula()
end

FacMainCtrl.OnShow = HL.Override() << function(self)
    self.view.facQuickBarPlaceHolder.gameObject:SetActive(not LuaSystemManager.factory.inTopView)
    if self.view.pinFormulaNode.gameObject.activeInHierarchy then
        self.view.pinFormulaNode.animationWrapper:PlayInAnimation()
    end
end

FacMainCtrl.OnHide = HL.Override() << function(self)
    self.view.facQuickBarPlaceHolder.gameObject:SetActive(false)
    self.view.pinFormulaNode.formulaDetailNodeSelectableNaviGroup:ManuallyStopFocus()
end


FacMainCtrl._OnPlayAnimationOut = HL.Override() << function(self)
    if self.view.pinFormulaNode.gameObject.activeInHierarchy then
        self.view.pinFormulaNode.animationWrapper:PlayOutAnimation()
    end
end

FacMainCtrl._OnEnterFacMainRegion = HL.Method(HL.Opt(HL.Any)) << function(self)
    self:_RefreshPinFormula()
end

FacMainCtrl._OnExitFacMainRegion = HL.Method() << function(self)
    self:_RefreshPinFormula()
end



FacMainCtrl._InitPinNode = HL.Method() << function(self)
    local node = self.view.pinFormulaNode
    InputManagerInst:ChangeParent(true, node.craftNode.groupId, node.formulaDetailNodeInputBindingGroupMonoTarget.groupId)
    InputManagerInst:ChangeParent(true, node.sideNodeInputBindingGroupMonoTarget.groupId, node.formulaDetailNodeInputBindingGroupMonoTarget.groupId)
    self.m_closeBindingId = self:BindInputPlayerAction("fac_main_hud_pin_close", function()
        if #self.m_pinFormulaDataSeq > 1 then
            self:_OnPreviousCraftClick()
        else
            self:_OnPinFormulaCloseBtnClick()
        end
    end, node.inputBindingGroupMonoTarget.groupId)
    node.controllerHintBarCell:InitControllerHintBarCell({
        groupIds = {
            node.inputBindingGroupMonoTarget.groupId,
            node.formulaDetailNodeInputBindingGroupMonoTarget.groupId
        },
    }, true)
    node.closeButton.onClick:RemoveAllListeners()
    node.closeButton.onClick:AddListener(function()
        self:_OnPinFormulaCloseBtnClick()
    end)
    node.previousButton.onClick:RemoveAllListeners()
    node.previousButton.onClick:AddListener(function()
        self:_OnPreviousCraftClick()
    end)

    node.formulaDetailNodeSelectableNaviGroup.getDefaultSelectableFunc = function()
        return self.m_pinFormulaInCells:Get(1).incomeItem
    end
    node.formulaDetailNodeSelectableNaviGroup.onIsTopLayerChanged:AddListener(function(isTopLayer)
        self:_OnIsTopLayerChanged(isTopLayer)
    end)
    node.formulaDetailNodeSelectableNaviGroup.focusPanelSortingOrder = UIManager:GetBaseOrder(Types.EPanelOrderTypes.PopUp) - 1

    self.m_pinFormulaInCells = UIUtils.genCellCache(node.incomeCell)
    self.m_pinFormulaOutCells = UIUtils.genCellCache(node.outcomeCell)
    self:_UpdateAndRefreshPinFormula(true)
    self:OnFacMainRightActiveChange(false)
end

FacMainCtrl._OnIsTopLayerChanged = HL.Method(HL.Boolean) << function(self, isTopLayer)

    if isTopLayer then
        UIManager:HideWithKey(PanelId.GeneralAbility, "FacMainPanel")
    else
        UIManager:ShowWithKey(PanelId.GeneralAbility, "FacMainPanel")
    end

    self:OnFacMainRightActiveChange(isTopLayer)

    Notify(MessageConst.TOGGLE_HIDE_INTERACT_OPTION_LIST, { "FacMain", isTopLayer })
    Notify(MessageConst.TOGGLE_HIDE_FAC_TOP_VIEW_RIGHT_SIDE_UI, isTopLayer)
end

FacMainCtrl.OnFacMainRightActiveChange = HL.Method(HL.Boolean) << function(self, isActive)
    local node = self.view.pinFormulaNode
    node.activeKeyHint.gameObject:SetActive(DeviceInfo.usingController and not isActive)
    node.inputBindingGroupMonoTarget.internalEnabled = not DeviceInfo.usingController or isActive
    node.controllerHintBarCell.gameObject:SetActive(isActive)
    if isActive then
        node.controllerHintBarCell:RefreshAll(false)
        CS.Beyond.Gameplay.Conditions.OnFacMainPinHintShow.Trigger()
    end
end

FacMainCtrl._OnFormulaDelete = HL.Method(HL.Any) << function(self, args)
    local chapterInfo = FactoryUtils.getCurChapterInfo()
    if chapterInfo == nil then
        return
    end
    local pinId = CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryUtil.GetPinBoardStrId(chapterInfo.pinBoard, GEnums.FCPinPosition.Formula:GetHashCode())
    if string.isEmpty(pinId) then
        return
    end
    if FactoryUtils.isExpiredTimeLimitedFormula(pinId) then
        local curScopeIndex = ScopeUtil.GetCurrentScope():GetHashCode()
        if curScopeIndex ~= 0 then



            GameInstance.player.remoteFactory.core:Message_PinSet(curScopeIndex, GEnums.FCPinPosition.Formula:GetHashCode(), "", 0, true)
        end
    end
end

FacMainCtrl._OnPinStateChanged = HL.Method(HL.Table) << function(self, pinStateInfo)
    local pinId, pinType, chapterId = unpack(pinStateInfo)
    if chapterId ~= Utils.getCurrentChapterId() then
        return
    end

    local lastPinFormulaId = self:_GetPinFormulaIdFromFormulaData()
    local lastFocusItem = self.m_focusPinInfo ~= nil and self.m_focusPinInfo.itemBundle or nil
    local isPinFormula = pinType == GEnums.FCPinPosition.Formula:GetHashCode()
    if isPinFormula then
        self:_UpdateAndRefreshPinFormula()
    end
    local currPinFormulaId = self:_GetPinFormulaIdFromFormulaData()
    if isPinFormula and not string.isEmpty(currPinFormulaId) then
        local hasFocus = self:_RefocusPinItem(lastFocusItem, true)
        if not hasFocus then
            self:_RefocusPinItem(lastFocusItem, false)
        end
    end

    self:_ShowPinFormulaChangedToast(lastPinFormulaId, currPinFormulaId)
end

FacMainCtrl._OnPinSystemLockedStateChanged = HL.Method(HL.Any) << function(self, arg)
    local systemIndex = unpack(arg)
    if systemIndex == GEnums.UnlockSystemType.FacCraftPin:GetHashCode() then
        self:_UpdateAndRefreshPinFormula()
    end
end

FacMainCtrl._OnFacBuildingNodeStateChanged = HL.Method(HL.Any) << function(self, arg)
    self.m_needUpdatePin = true
end

FacMainCtrl._ShowPinFormulaChangedToast = HL.Method(HL.String, HL.String) << function(self, lastId, currId)



    if lastId == currId then
        return
    end

    local isCurrEmpty = string.isEmpty(currId)
    local textId = isCurrEmpty and FORMULA_CANCEL_PIN_TOAST_TEXT_ID or FORMULA_PIN_TOAST_TEXT_ID
    local formulaDesc = ""
    if isCurrEmpty then
        local success, formulaTableData = Tables.factoryMachineCraftTable:TryGetValue(lastId)
        if success then
            formulaDesc = formulaTableData.formulaDesc
        end
    else
        local success, formulaTableData = Tables.factoryMachineCraftTable:TryGetValue(currId)
        if success then
            formulaDesc = formulaTableData.formulaDesc
        end
    end

    Notify(MessageConst.SHOW_TOAST, string.format(Language[textId], formulaDesc))
end

FacMainCtrl._GetPinFormulaIdFromFormulaData = HL.Method().Return(HL.String) << function(self)
    local formulaData = self:_GetCurPinFormulaData()
    return formulaData == nil and "" or formulaData.craftId
end

FacMainCtrl._TryFocusPinItem = HL.Method(HL.Any, HL.Table, HL.Boolean) << function(self, cell, itemBundle, isIn)
    local craftData = isIn and FactoryUtils.getItemCraft(itemBundle.id) or nil
    self.m_focusPinInfo = {
        itemBundle = itemBundle,
        isIn = isIn,
        craftData = craftData,
        focusCell = cell,
    }
    cell:SetSelected(true)
    self:_RefreshPinFocusNode()
end

FacMainCtrl._UnFocusPinItem = HL.Method() << function(self)
    self:_UnSelectItemsIfNecessary()
    self.m_focusPinInfo = nil
    self:_RefreshPinFocusNode()
end

FacMainCtrl._RefreshPinFocusNode = HL.Method() << function(self)
    local pinFormulaNode = self.view.pinFormulaNode
    local isFocus = self.m_focusPinInfo ~= nil
    if not isFocus then
        pinFormulaNode.focusNode:SetState("Empty")
        return
    end
    local hasCraft = self.m_focusPinInfo.isIn and self.m_focusPinInfo.craftData ~= nil
    pinFormulaNode.focusNode:SetState(hasCraft and "Craft" or "Empty")
    if not hasCraft then
        return
    end
    pinFormulaNode.focusItem:SetEnableHoverTips(not DeviceInfo.usingController)
    pinFormulaNode.focusItem:InitItem(self.m_focusPinInfo.itemBundle)
    pinFormulaNode.focusItem:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
    pinFormulaNode.focusItem:SetSelected(true)
    pinFormulaNode.focusButton.onClick:RemoveAllListeners()
    pinFormulaNode.focusButton.onClick:AddListener(function()
        self:_OnApplyCraftClick(self.m_focusPinInfo.craftData, self.m_focusPinInfo.itemBundle)
    end)

    local cellRectTrans = self.m_focusPinInfo.focusCell.transform
    local bound = CSUtils.CalcBoundOfRectTransform(cellRectTrans, pinFormulaNode.focusNode.transform)
    pinFormulaNode.focusContent.anchoredPosition = Vector2(bound.center.x, bound.center.y)
end

FacMainCtrl._UpdateAndRefreshPinFormula = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    self:_UnlockCancelButton()
    self:_UpdatePinFormulaData()
    self:_RefreshPinFormula(isInit)
end

FacMainCtrl._UpdatePinFormulaData = HL.Method() << function(self)
    self.m_pinFormulaDataSeq = {}
    self.m_focusPinInfo = nil

    local chapterInfo = FactoryUtils.getCurChapterInfo()
    if chapterInfo == nil then
        return
    end

    local isFormulaPinUnlocked = Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacCraftPin)
    if not isFormulaPinUnlocked then
        return
    end

    local pinId = CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryUtil.GetPinBoardStrId(chapterInfo.pinBoard, GEnums.FCPinPosition.Formula:GetHashCode())
    if string.isEmpty(pinId) then
        return
    end

    local formulaId = pinId
    local formulaData = self:_LoadPinFormulaData(formulaId)
    if formulaData ~= nil then
        table.insert(self.m_pinFormulaDataSeq, {
            data = formulaData,
            isRoot = true,
        })
    end
end

FacMainCtrl._LoadPinFormulaData = HL.Method(HL.String).Return(HL.Table) << function(self, formulaId)
    if Tables.factoryMachineCraftTable:ContainsKey(formulaId) then
        return FactoryUtils.parseMachineCraftData(formulaId)
    elseif Tables.factoryHubCraftTable:ContainsKey(formulaId) then
        return FactoryUtils.parseHubCraftData(formulaId)
    elseif Tables.factoryManualCraftTable:ContainsKey(formulaId) then
        return FactoryUtils.parseManualCraftData(formulaId)
    end
    return nil
end

FacMainCtrl._GetCurPinFormulaData = HL.Method().Return(HL.Table) << function(self)
    if self.m_pinFormulaDataSeq == nil or #self.m_pinFormulaDataSeq <= 0 then
        return nil
    end
    return self.m_pinFormulaDataSeq[#self.m_pinFormulaDataSeq].data
end

FacMainCtrl._RefreshPinFormula = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    local pinFormulaNode = self.view.pinFormulaNode
    local isInFacMainRegion = Utils.isInFacMainRegion()

    local formulaData = self:_GetCurPinFormulaData()
    if formulaData ~= nil and pinFormulaNode ~= nil and isInFacMainRegion then
        if not self.view.pinFormulaAnim.gameObject.activeSelf then
            UIUtils.PlayAnimationAndToggleActive(self.view.pinFormulaAnim, true)
        end
        self:_RefreshPinFormulaNode()
        self.m_isPinFormula = true
    else
        if isInit then
            self.view.pinFormulaAnim.gameObject:SetActive(false)
        else
            if self.view.pinFormulaAnim.gameObject.activeSelf then
                UIUtils.PlayAnimationAndToggleActive(self.view.pinFormulaAnim, false)
            end
        end

        local isFrontPanel = self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder()
        if isFrontPanel then
            self:_UnFocusItemAndTips()
        end
        self.m_isPinFormula = false
    end
end

FacMainCtrl._RefreshPinFormulaNode = HL.Method() << function(self)
    local pinFormulaNode = self.view.pinFormulaNode
    if pinFormulaNode == nil then
        return
    end

    local formulaData = self:_GetCurPinFormulaData()
    if formulaData == nil then
        return
    end
    if pinFormulaNode.earlyAccessNode ~= nil then
        local isEarlyAccessBuilding = not string.isEmpty(formulaData.buildingId) and
            FactoryUtils.isSkipUnlockedBuilding(formulaData.buildingId) and
            not Utils.isInBlackbox()
        pinFormulaNode.earlyAccessNode.gameObject:SetActiveIfNecessary(isEarlyAccessBuilding)
    end

    if formulaData.formulaMode == nil or formulaData.formulaMode == FacConst.FAC_FORMULA_MODE_MAP.NORMAL then
        pinFormulaNode.iconMode.gameObject:SetActive(false)
    else
        local hasMode, modeData = Tables.factoryMachineCraftModeTable:TryGetValue(formulaData.formulaMode)
        pinFormulaNode.iconMode.gameObject:SetActive(hasMode)
        if hasMode then
            pinFormulaNode.iconMode:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_COMMON, modeData.iconId)
        end
    end


    if pinFormulaNode.limitedTimeEventNode then
        if formulaData.craftId ~= nil then
            pinFormulaNode.limitedTimeEventNode.gameObject:SetActiveIfNecessary(FactoryUtils.isTimeLimitedFormula(formulaData.craftId))
            FactoryUtils.setTimeLimitedFormulaTagColor(pinFormulaNode.timeLimitedColorTag1, formulaData.craftId)
            FactoryUtils.setTimeLimitedFormulaTagColor(pinFormulaNode.timeLimitedColorTag2, formulaData.craftId)
        else
            pinFormulaNode.limitedTimeEventNode.gameObject:SetActiveIfNecessary(false)
        end
    end

    local curSeq = self.m_pinFormulaDataSeq[#self.m_pinFormulaDataSeq]
    pinFormulaNode.sideNode:SetState(curSeq.isRoot and "Close" or "Previous")
    if curSeq.isRoot ~= true and #self.m_pinFormulaDataSeq > 0 then
        local rootOutItem = self.m_pinFormulaDataSeq[1].data.outcomes[1]
        if rootOutItem ~= nil then
            pinFormulaNode.previousItemIcon:InitItemIcon(rootOutItem.id)
        end
    end
    if self.m_closeBindingId > 0 then
        InputManagerInst:SetBindingText(self.m_closeBindingId, curSeq.isRoot and Language["key_hint_fac_main_hud_pin_close"] or Language["key_hint_fac_main_hud_pin_previous"])
        pinFormulaNode.controllerHintBarCell:RefreshContentOnly()
    end

    local incomes = formulaData.incomes
    self.m_pinFormulaInCells:Refresh(#incomes, function(cell, index)
        local incomeItem = incomes[index]
        cell.gameObject.name = "income" .. index
        cell.incomeItem:SetEnableHoverTips(not DeviceInfo.usingController)
        cell.incomeItem:InitItem(incomes[index], function(itemBundle)
            self:_OnPinItemClick(cell.incomeItem, itemBundle, true)
        end)
        cell.incomeItem:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        local incomeCraft = FactoryUtils.getItemCraft(incomeItem.id)
        cell.bgImg.gameObject:SetActive(incomeCraft ~= nil)
    end)

    local outcomes = formulaData.outcomes
    self.m_pinFormulaOutCells:Refresh(#outcomes, function(cell, index)
        cell.gameObject.name = "outcome" .. index
        cell.outcomeItem:SetEnableHoverTips(not DeviceInfo.usingController)
        cell.outcomeItem:InitItem(outcomes[index], function(itemBundle)
            self:_OnPinItemClick(cell.outcomeItem, itemBundle, false)
        end)
        cell.outcomeItem:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
    end)


    FactoryUtils.refreshEnvIcon(formulaData.env, pinFormulaNode.environmentIconController)

    LayoutRebuilder.ForceRebuildLayoutImmediate(pinFormulaNode.animationNode.transform)

    self:_RefreshPinFormulaNodeText()
    self:_RefreshPinFocusNode()
end

FacMainCtrl._RefreshPinFormulaNodeText = HL.Method() << function(self)
    local pinFormulaNode = self.view.pinFormulaNode
    if pinFormulaNode == nil then
        return
    end
    local formulaData = self:_GetCurPinFormulaData()
    if formulaData == nil then
        return
    end

    pinFormulaNode.formulaTime.text = string.format(Language["LUA_CRAFT_CELL_STANDARD_TIME"], FactoryUtils.getCraftTimeStr(formulaData.time))

    local buildingData = Tables.factoryBuildingTable:GetValue(formulaData.buildingId)
    if buildingData == nil then
        pinFormulaNode.craftMachineTxt.text = ''
    else
        pinFormulaNode.craftMachineTxt.text = string.format(Language.LUA_FAC_MAIN_PIN_CRAFT_MACHINE_FORMAT, UIConst.UI_SPRITE_FAC_BUILDING_PANEL_ICON, buildingData.iconOnPanel, buildingData.name)
    end
end

FacMainCtrl._UnSelectItemsIfNecessary = HL.Method() << function(self)
    local closeFuc = function(isIn)
        local cells = isIn and self.m_pinFormulaInCells:GetItems() or self.m_pinFormulaOutCells:GetItems()
        for _, cell in pairs(cells) do
            local item = isIn and cell.incomeItem or cell.outcomeItem
            item:SetSelected(false)
        end
    end

    closeFuc(true)
    closeFuc(false)
end

FacMainCtrl._FocusItemWithTips = HL.Method(HL.Any, HL.Table, HL.Boolean, HL.Opt(HL.Boolean)) << function(self, cell, itemBundle, isIn, isRefocus)
    local pinFormulaNode = self.view.pinFormulaNode
    Notify(MessageConst.SHOW_ITEM_TIPS, {
        transform = pinFormulaNode.animationNode.transform,
        posType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
        safeArea = pinFormulaNode.animationNode.transform,
        isSideTips = true,

        itemId = itemBundle.id,
        itemCount = itemBundle.count,

        onClose = function()
            self:_UnFocusPinItem()
        end
    })
    self:_TryFocusPinItem(cell, itemBundle, isIn)
end

FacMainCtrl._UnFocusItemAndTips = HL.Method() << function(self)
    Notify(MessageConst.HIDE_ITEM_TIPS)
    self:_UnFocusPinItem()
end

FacMainCtrl._LockCancelButton = HL.Method() << function(self)
    local pinFormulaNode = self.view.pinFormulaNode
    pinFormulaNode.cancelProtectNode.gameObject:SetActive(true)
    self.m_isCancelPinLock = true
    self.m_cancelPinTimer = TimerManager:StartTimer(self.view.config.CANCEL_PIN_PROTECT_DURATION, function()
        self:_UnlockCancelButton(true)
    end)
end

FacMainCtrl._UnlockCancelButton = HL.Method(HL.Opt(HL.Boolean)) << function(self, fromTimer)
    if fromTimer ~= true then
        if self.m_cancelPinTimer > 0 then
            TimerManager:ClearTimer(self.m_cancelPinTimer)
        end
    end
    self.m_cancelPinTimer = 0
    self.m_isCancelPinLock = false
    local pinFormulaNode = self.view.pinFormulaNode
    pinFormulaNode.cancelProtectNode.gameObject:SetActive(false)
end

FacMainCtrl._ApplyPinFormulaCraft = HL.Method(HL.Table, HL.Table) << function(self, craftData, itemBundle)
    table.insert(self.m_pinFormulaDataSeq, {
        data = craftData,
        fromItem = itemBundle,
    })
    self:_RefreshPinFormulaNode()
    if self.m_focusPinInfo ~= nil then
        self:_RefocusPinItem(itemBundle, false)
    end
end

FacMainCtrl._PreviousPinFormulaCraft = HL.Method(HL.Table) << function(self, fromItem)
    table.remove(self.m_pinFormulaDataSeq)
    self:_RefreshPinFormulaNode()
    if self.m_focusPinInfo ~= nil then
        self:_RefocusPinItem(fromItem, true)
    end
end

FacMainCtrl._RefocusPinItem = HL.Method(HL.Table, HL.Boolean).Return(HL.Boolean) << function(self, fromItem, isIn)
    if fromItem == nil then
        return false
    end
    local cells = isIn and self.m_pinFormulaInCells:GetItems() or self.m_pinFormulaOutCells:GetItems()
    local hasFocus = false
    for _, cell in pairs(cells) do
        local item = isIn and cell.incomeItem or cell.outcomeItem
        if item.id == fromItem.id then
            if DeviceInfo.usingController then
                self:SetNaviTarget(item.view.button)
            end
            self:_FocusItemWithTips(item, fromItem, isIn, true)
            hasFocus = true
        end
    end
    return hasFocus
end

FacMainCtrl._OnPinFormulaCloseBtnClick = HL.Method() << function(self)
    if self.m_isCancelPinLock == true then
        return
    end
    local curScopeIndex = ScopeUtil.GetCurrentScope():GetHashCode()
    if curScopeIndex ~= 0 then

        EventLogManagerInst:GameEvent_FactoryPinSetting(false, self:_GetPinFormulaIdFromFormulaData(), 'way_1')

        GameInstance.player.remoteFactory.core:Message_PinSet(curScopeIndex, GEnums.FCPinPosition.Formula:GetHashCode(), "", 0, true)
    end
end

FacMainCtrl._OnPinItemClick = HL.Method(HL.Any, HL.Table, HL.Boolean) << function(self, cell, itemBundle, isIn)
    if self.m_focusPinInfo ~= nil and self.m_focusPinInfo.focusCell == cell and not DeviceInfo.usingController then
        self:_UnFocusItemAndTips()
        return
    end
    self:_FocusItemWithTips(cell, itemBundle, isIn)
end

FacMainCtrl._OnApplyCraftClick = HL.Method(HL.Table, HL.Table) << function(self, craftData, itemBundle)
    local targetIndex = -1
    for i = #self.m_pinFormulaDataSeq, 1, -1 do
        local seq = self.m_pinFormulaDataSeq[i]
        if seq.fromItem ~= nil and seq.fromItem.id == itemBundle.id then
            targetIndex = i
            break
        end
    end
    if targetIndex < 0 then
        self:_ApplyPinFormulaCraft(craftData, itemBundle)
        return
    end

    for i = #self.m_pinFormulaDataSeq, targetIndex + 1, -1 do
        table.remove(self.m_pinFormulaDataSeq)
    end
    self:_RefreshPinFormulaNode()
    if self.m_focusPinInfo ~= nil then
        self:_RefocusPinItem(itemBundle, false)
    end
end

FacMainCtrl._OnPreviousCraftClick = HL.Method() << function(self)
    local dataSeq = self.m_pinFormulaDataSeq[#self.m_pinFormulaDataSeq]
    if dataSeq.isRoot == true then
        return
    end
    self:_PreviousPinFormulaCraft(dataSeq.fromItem)
    local afterDataSeq = self.m_pinFormulaDataSeq[#self.m_pinFormulaDataSeq]
    if afterDataSeq.isRoot == true then
        self:_LockCancelButton()
    end
end


FacMainCtrl.OnToggleFacTopView = HL.Method(HL.Boolean) << function(self, active)

    self.view.facQuickBarPlaceHolder.gameObject:SetActive(not active and self:IsShow())
end

FacMainCtrl.OnFacTopViewHideUIModeChange = HL.Method(HL.Boolean) << function(self, isTopViewHideUIMode)
    self.view.rightNode.gameObject:SetActive(not isTopViewHideUIMode)
end

HL.Commit(FacMainCtrl)
