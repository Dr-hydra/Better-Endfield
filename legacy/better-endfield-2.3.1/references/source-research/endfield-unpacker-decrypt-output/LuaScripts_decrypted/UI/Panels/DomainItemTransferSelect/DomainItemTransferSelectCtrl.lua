local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DomainItemTransferSelect
local RouteStatus = GEnums.DomainTransportRouteStatusType
local INIT_NUMBER_SELECTOR_CUR_VALUE = math.maxinteger
local SEC_PER_HOUR = 3600
local SEC_PER_MIN = 60
local MIN_PER_HOUR = 60
local TRANSMISSION_MIN_VALUE = 1

local LOSSLESS_TRANSMISSION_INSTRUCTION_ID = "lossless_transmission_instruction"
local LOSSLESS_VALUE_INSTRUCTION_ID = "lossless_value_instruction"

local NORMAL_TRANSMISSION_STATE = "WarehouseTransfer"
local LOSSLESS_TRANSMISSION_STATE = "NoConsumptionTransfer"

local DEPOT_SLOT_NORMAL_STYLE = "ShowStorageTag"
local DEPOT_SLOT_TRANSMISSION_STYLE = "HideStorageTag"

DomainItemTransferSelectCtrl = HL.Class('DomainItemTransferSelectCtrl', uiCtrl.UICtrl)






DomainItemTransferSelectCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_FAC_TRANS_ROUTE_CHANGE] = '_OnNotifyRouteInfoChange',
}


DomainItemTransferSelectCtrl.m_targetDomain = HL.Field(HL.String) << ""

DomainItemTransferSelectCtrl.m_chosenItemId = HL.Field(HL.String) << ""

DomainItemTransferSelectCtrl.m_chosenItemCount = HL.Field(HL.Number) << 0

DomainItemTransferSelectCtrl.m_chosenItemCell = HL.Field(HL.Any)

DomainItemTransferSelectCtrl.m_waitingToClose = HL.Field(HL.Boolean) << false

DomainItemTransferSelectCtrl.m_info = HL.Field(HL.Any)

DomainItemTransferSelectCtrl.m_losslessTransmission = HL.Field(HL.Boolean) << false

DomainItemTransferSelectCtrl.m_maxTransmissionValue = HL.Field(HL.Number) << 0

DomainItemTransferSelectCtrl.m_losslessTransmissionUnlocked = HL.Field(HL.Boolean) << false

DomainItemTransferSelectCtrl.m_allowSend = HL.Field(HL.Boolean) << false

DomainItemTransferSelectCtrl.m_destinationCellCache = HL.Field(HL.Forward("UIListCache"))


DomainItemTransferSelectCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_info = args.info
    local recoverState = self:_ProcessRecoverStateArg(args and args.recoverState)

    self.m_destinationCellCache = UIUtils.genCellCache(self.view.selectEndPointRoot.siteCell)

    self.view.btnBack.onClick:AddListener(function()
        self:_Close()
    end)

    self.view.detailsBtn.onClick:AddListener(function()
        self:_OnClickLosslessTransmissionInfoBtn()
    end)

    self.view.totalValueDetailsBtn.onClick:AddListener(function()
        self:_OnClickLosslessValueInstructionBtn()
    end)

    self.view.transferTabNode.onValueChanged:AddListener(function(isOn)
        self:_OnLosslessTransmissionToggleChanged(isOn)
    end)

    self.view.transferTabNode.checkIsValueValid = function(isOn)
        return self:_CheckIsValueValidLosslessTransmissionToggle(isOn)
    end

    if DeviceInfo.usingController then
        self:_InitControllerAbility()
    end

    self:_InitBtn()

    self:_InitFromDomainInfo()
    self:_ApplyRecoverState(recoverState)
    self:_InitLeftSidePlatformInfo(self.m_targetDomain)
    self:_InitNumberSelector()
    self:_InitTransmissionMode()

    if self:_IsCurrentTransmitting() then
        self:_OpenDepot(recoverState)
    elseif recoverState ~= nil and recoverState.isSelectingTarget == false and not string.isEmpty(self.m_targetDomain) then
        self:_OpenDepot(recoverState)
    else
        self:_OpenSelectTargetRoot()
    end
    self:_TryRecoverInstructionBook(recoverState)
    self:_TryRecoverCommonPopUp(recoverState)

    self.view.depotExtraRoot.timeRemainingTxt.text = self:_GetTimeText()
    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.FAC_COMMON_UI_UPDATE_INTERVAL)
            self.view.depotExtraRoot.timeRemainingTxt.text = self:_GetTimeText()
        end
    end)
end

DomainItemTransferSelectCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    self.view.depotExtraRoot.numberSelector:UpdateKeyHintVisible(active)
end

DomainItemTransferSelectCtrl.OnHide = HL.Override() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self:_ManuallyStopFocusLeftNode()
end

DomainItemTransferSelectCtrl.OnClose = HL.Override() << function(self)
    if DeviceInfo.usingController then
        Notify(MessageConst.HIDE_COMMON_HOVER_TIP)

        local isOpen, ctr = UIManager:IsOpen(PanelId.DomainItemTransfer)
        if isOpen then
            ctr:_SetFocusTargetByIndex(ctr.m_curFocusCellLuaIndex)
        end
    end
end

DomainItemTransferSelectCtrl.GetRecoverStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local recoverState = {
        routeFromDomain = self.m_info.fromDomain,
        routeIndex = self.m_info.index,
        isSelectingTarget = self.view.selectEndPointRoot.gameObject.activeInHierarchy,
        targetDomain = self.m_targetDomain,
        chosenItemId = self.m_chosenItemId,
        chosenItemCount = self.m_chosenItemCount,
        transferTabNodeIsOn = self.view.transferTabNode.isOn,
        depotState = self.view.depot:GetRecoverStateArg(),
        numberSelectorCount = self.view.depotExtraRoot.numberSelector.curNumber,
        commonPopUpArg = self:_GetCommonPopUpRecoverState(),
    }
    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and instructionCtrl:IsShow() and PhaseManager:GetTopPhaseId() == PhaseId.DomainItemTransfer and
            (instructionCtrl.id == LOSSLESS_TRANSMISSION_INSTRUCTION_ID or instructionCtrl.id == LOSSLESS_VALUE_INSTRUCTION_ID) then
        recoverState.instructionBookArg = {
            id = instructionCtrl.id,
        }
    end
    return recoverState
end

DomainItemTransferSelectCtrl._OpenDepot = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    AudioAdapter.PostEvent("Au_UI_Popup_DetailsPanel_Open")

    self:_ToggleLosslessTabOrTag(true)
    self:_SetDepotSlotStyleByLossless()

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.forceRebuildTarget)
    self.view.leftItemSlotRoot.gameObject:SetActive(true)
    self.view.changeTransNode.gameObject:SetActive(true)
    self.view.selectEndPointRoot.gameObject:SetActive(false)
    self.view.selectingTargetMask.gameObject:SetActive(false)
    self.view.leftLineWithEffect.gameObject:SetActive(true)
    self.view.leftLineWithoutEffect.gameObject:SetActive(false)

    self.view.sliderImg.gameObject:SetActive(self:_IsCurrentTransmitting())


    if self.m_losslessTransmission then
        self.view.leftLineWithEffect:SetState(LOSSLESS_TRANSMISSION_STATE)
    else
        self.view.leftLineWithEffect:SetState(NORMAL_TRANSMISSION_STATE)
    end

    local depotArgs = {
        domainId = self.m_info.fromDomain,
        recoverState = recoverState and recoverState.depotState or nil,

        customOnUpdateCell = function(cell, info, luaIndex)
            local itemInfoPack = {
                id = info.id
            }
            cell.item:InitItem(itemInfoPack, function()
                self:_OnClickItem(cell, info.id)
                self.view.depotExtraRoot.timeRemainingTxt.text = self:_GetTimeText()
            end)

            cell.item.view.button.onLongPress:RemoveAllListeners()
            cell.item.view.button.onLongPress:AddListener(function()
                Notify(MessageConst.SHOW_ITEM_TIPS, {
                    itemId = info.id,
                    transform = cell.item.gameObject.transform,
                    onClose = function()
                        cell.item.view.selectedBG.gameObject:SetActive(false)
                    end,
                    posType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
                })
                cell.item.view.selectedBG.gameObject:SetActive(true)
            end)

            cell.item.view.selectMarkNode.gameObject:SetActive(self.m_chosenItemId == info.id)
            local itemCount = self:_GetItemCount(info.id)
            cell.item.view.storageNumberText.text = UIUtils.getNumString(itemCount)

            local itemValue = self:_GetItemValue(info.id)
            cell.item.view.transferValueTxt.text = itemValue


            cell.item.view.storageImage.gameObject:SetActive(not self.m_losslessTransmission)

            if self.m_chosenItemId == info.id then
                self.m_chosenItemCell = cell
            end

            if DeviceInfo.usingController then
                cell.item.view.button.onIsNaviTargetChanged = function(isTarget, isGroupChanged, naviTargetEnabledAgain)
                    if isTarget then
                        self:_OnFocusTarget(cell, info, luaIndex)
                    end
                end
            end
        end,

        customItemInfoListPostProcess = function(allItemInfoList)
            if allItemInfoList == nil or next(allItemInfoList) == nil then
                return {}
            end

            local fromDomainId = self.m_info.fromDomain
            local result = {}
            for _, info in ipairs(allItemInfoList) do
                local id = info.id
                if FactoryUtils.isFactoryItemNormal(id) then
                    local facSuccess, facItemData = Tables.factoryItemTable:TryGetValue(id)
                    if self.m_losslessTransmission then

                        for i = 0, facItemData.losslessDomainIds.Count - 1 do
                            if facItemData.losslessDomainIds[i] == fromDomainId then
                                table.insert(result, info)
                            end
                        end
                    else
                        for i = 0, facItemData.transferDomainIds.Count - 1 do
                            if facItemData.transferDomainIds[i] == fromDomainId then
                                table.insert(result, info)
                            end
                        end
                    end
                end
            end
            return result
        end,

        onChangeTypeFunction = function()
            self:_ClearSelectedItem()
            self:_UpdateNumber()
        end,

        showHistory = true,
        disableDrag = true,
    }

    self.view.depot:InitDepot(GEnums.ItemValuableDepotType.Factory,
                              function(itemId, cell)
                                  self:_OnClickItem(cell, itemId)
                              end,
                              depotArgs)

    self:_RefreshLeftSideItem()
    self:_RefreshBtnAndText()
    self:_UpdateNumber()
    self:_TryRecoverDepotState(recoverState)

    if DeviceInfo.usingController then
        self:Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.leftNode.groupId)
        InputManagerInst:ToggleGroup(self.view.leftNode.groupId, false)
        InputManagerInst:ToggleBinding(self.m_unFocusLeftBindId, false)

        self.view.controllerFocusHintNode.gameObject:SetActive(true)

        self.view.leftItemSlotRootInputBindingGroupMonoTarget.enabled = false
        self.view.transferTabNodeInputBindingGroupMonoTarget.enabled = false
        self.view.noConsumptionTransferDetails.enabled = false
        self.view.selectEndPointRootInputBindingGroupMonoTarget.enabled = false
        self.view.changeTransNode.enabled = false

        InputManagerInst:ToggleBinding(self.m_focusLeftBindId, true)
    end
end

DomainItemTransferSelectCtrl._TryGetCell = HL.Method(HL.String).Return(HL.Any) << function(self, itemId)
    local depot = self.view.depot
    local depotContent = depot.view.depotContent
    local depotCellIndex = depotContent:GetItemIndex(itemId)
    local cell = depotContent:GetCell(depotCellIndex)
    return cell
end

DomainItemTransferSelectCtrl._RefreshLeftSideItem = HL.Method() << function(self)
    local view = self.view.leftItemSlotRoot




    if not self:_IsCurrentTransmitting() and string.isEmpty(self.m_chosenItemId) then
        view.itemSlotCenterTriangle.gameObject:SetActive(false)
        view.itemCanceled.gameObject:SetActive(false)
        view.itemTarget.gameObject:SetActive(false)
        view.itemEmpty.gameObject:SetActive(true)
        return

    elseif not self:_IsCurrentTransmitting() and not string.isEmpty(self.m_chosenItemId) then
        view.itemSlotCenterTriangle.gameObject:SetActive(true)
        view.itemCanceled.gameObject:SetActive(false)
        view.itemTarget.gameObject:SetActive(true)
        view.itemEmpty.gameObject:SetActive(false)
        local itemDataPack = {
            id = self.m_chosenItemId,
            count = self.m_chosenItemCount,
        }
        view.itemTarget:InitItem(itemDataPack, true)
        view.itemTarget:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        return

    elseif self:_IsCurrentTransmitting() and not self:_IsItemModified() then
        view.itemSlotCenterTriangle.gameObject:SetActive(false)
        view.itemCanceled.gameObject:SetActive(false)
        view.itemTarget.gameObject:SetActive(true)
        view.itemEmpty.gameObject:SetActive(false)
        local itemDataPack = {
            id = self.m_info.itemId,
            count = self.m_info.itemNumMax,
        }
        view.itemTarget:InitItem(itemDataPack, true)
        view.itemTarget:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        return

    elseif self:_IsCurrentTransmitting() and self:_IsItemModified() then
        view.itemSlotCenterTriangle.gameObject:SetActive(true)
        view.itemCanceled.gameObject:SetActive(true)
        view.itemTarget.gameObject:SetActive(true)
        view.itemEmpty.gameObject:SetActive(false)
        local itemCanceledPack = {
            id = self.m_info.itemId,
            count = self.m_info.itemNumMax,
        }
        view.itemCanceled:InitItem(itemCanceledPack, true)
        view.itemCanceled:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        local itemTargetPack = {
            id = self.m_chosenItemId,
            count = self.m_chosenItemCount,
        }
        view.itemTarget:InitItem(itemTargetPack, true)
        view.animationNode:Play("domainItemtransferselect_leftitemslot_changein")
        return
    end
end

DomainItemTransferSelectCtrl._ClearSelectedItem = HL.Method() << function(self)
    self:_OnClickItem(nil, "")
end

DomainItemTransferSelectCtrl._OnClickItem = HL.Method(HL.Any, HL.String) << function(self, itemCell, itemId)

    if self:_IsCurrentTransmitting() and itemId == self.m_chosenItemId then
        return
    end


    if self.m_chosenItemCell ~= nil then
        local view = self.m_chosenItemCell.view
        if view ~= nil then
            local toggle = view.item.view.selectMarkNode
            if toggle ~= nil then
                toggle.gameObject:SetActive(false)
            end
        end
        self.m_chosenItemCell = nil
    end


    if string.isEmpty(itemId) or not self:_IsCurrentTransmitting() and itemId == self.m_chosenItemId then
        self:_ClearChosenItem()
        return
    end

    if itemId ~= self.m_chosenItemId and not string.isEmpty(itemId) then
        self.m_chosenItemCell = itemCell
        self.m_chosenItemId = itemId
        local targetCount = self:_GetCurrentNeedAssignCount()
        self:_ChangeCount(targetCount)
        self.m_chosenItemCell.view.item.view.selectMarkNode.gameObject:SetActive(true)
    end

    self:_RefreshBtnAndText()
    self:_RefreshLeftSideItem()
    self:_UpdateNumber()

    self:_UpdateNumberSelector()
    self:_UpdateTransmissionValue()
end

DomainItemTransferSelectCtrl._ClearChosenItem = HL.Method() << function(self)
    self.m_chosenItemId = self.m_info.itemId
    self:_ChangeCount(self.m_info.itemNumMax)
    if self:_IsCurrentTransmitting() then
        local toggleCell = self:_TryGetCell(self.m_info.itemId)
        self.m_chosenItemCell = toggleCell
        if self.m_chosenItemCell ~= nil then
            self.m_chosenItemCell.view.item.view.selectMarkNode.gameObject:SetActive(true)
        end
    end
    self:_RefreshBtnAndText()
    self:_RefreshLeftSideItem()
    self:_UpdateNumber()

    self:_UpdateTransmissionValue()
end

DomainItemTransferSelectCtrl._GetCurrentNeedAssignCount = HL.Method().Return(HL.Number) << function(self)
    if self:_IsCurrentTransmitting() then
        return self.view.depotExtraRoot.numberSelector.curNumber
    end

    if self.m_chosenItemCount ~= 0 then
        return self.m_chosenItemCount
    end

    local factoryItemCfg = Tables.factoryItemTable[self.m_chosenItemId]
    local value = math.floor(self.m_maxTransmissionValue / factoryItemCfg.value)
    return value
end

DomainItemTransferSelectCtrl._RefreshBtnAndText = HL.Method() << function(self)
    local itemModified = self:_IsItemModified()
    local blockOrRetry = self.m_info.status == RouteStatus.blocked or self.m_info.status == RouteStatus.retry
    local isFocusItemEqualChosen = self:_IsFocusItemEqualChosen()

    self.view.startTransBtn.gameObject:SetActive(self.m_allowSend and itemModified and isFocusItemEqualChosen)
    self.view.transmittingFakeBtn.gameObject:SetActive(self.m_allowSend and self:_IsCurrentTransmitting() and not blockOrRetry and
                                                               (not itemModified or not isFocusItemEqualChosen))
    self.view.transPausedFakeBtn.gameObject:SetActive(self.m_allowSend and blockOrRetry and
                                                              (not itemModified or not isFocusItemEqualChosen))
    self.view.cancelSelectBtn.gameObject:SetActive(self.m_allowSend and self:_IsCurrentTransmitting() and itemModified)
    self.view.transLocked.gameObject:SetActive(not self.m_allowSend)

    local depotExtra = self.view.depotExtraRoot
    local hasNoChosenItem = string.isEmpty(self.m_chosenItemId)
    self.view.depot.view.bottomNode.gameObject:SetActive(hasNoChosenItem or not isFocusItemEqualChosen)
    self.view.depot.view.sortNode.gameObject:SetActive(hasNoChosenItem or not isFocusItemEqualChosen)
    depotExtra.selectItemInDepotRoot.gameObject:SetActive(hasNoChosenItem or not isFocusItemEqualChosen)
    if hasNoChosenItem or not isFocusItemEqualChosen then
        depotExtra.timeRemainingRoot.gameObject:SetActive(false)
    else
        depotExtra.timeRemainingRoot.gameObject:SetActive(not blockOrRetry or itemModified)
    end

    depotExtra.numberSelector.gameObject:SetActive(not hasNoChosenItem and isFocusItemEqualChosen)
    self.view.depot.view.sortNode.gameObject:SetActive(hasNoChosenItem or not isFocusItemEqualChosen)
end

DomainItemTransferSelectCtrl._Close = HL.Method() << function(self)
    self:PlayAnimationOutAndClose()

    if DeviceInfo.usingController then
        self:Notify(MessageConst.HIDE_COMMON_HOVER_TIP, {noAnimation = true})
        self:Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.leftNode.groupId)
        InputManagerInst:ToggleGroup(self.view.leftNode.groupId, false)
        InputManagerInst:ToggleBinding(self.m_unFocusLeftBindId, false)
    end
end

DomainItemTransferSelectCtrl._InitBtn = HL.Method() << function(self)
    self.view.changeTargetBtn.onClick:AddListener(function()
        self:_OpenSelectTargetRoot(true)
    end)

    self.view.stopTransBtn.onClick:AddListener(function()
        self:Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_FAC_TRANS_CONFIRM_RESET,
            onConfirm = function()
                self:_DoResetRoute()
            end,
            onCancel = function()
                self:_ClearSelectedItem()
                self:_UpdateNumber()
            end
        })
    end)

    self.view.changeTargetBtn.gameObject:SetActive(not self:_IsCurrentTransmitting())
    self.view.stopTransBtn.gameObject:SetActive(self:_IsCurrentTransmitting())

    self.view.cancelSelectBtn.onClick:AddListener(function()
        self:_ClearSelectedItem()
    end)

    if self:_IsCurrentTransmitting() then
        self.view.startTransBtnText.text = Language.LUA_FAC_TRANS_MODIFY_BTN_TEXT
        self.view.startTransBtn.onClick:AddListener(function()
            self:Notify(MessageConst.SHOW_POP_UP, {
                content = self.m_info.lossless and Language.LUA_FAC_TRANS_CONFIRM_MODIFY_LOSSLESS or Language.LUA_FAC_TRANS_CONFIRM_MODIFY,
                onConfirm = function()
                    self:_DoChangeRoute()
                end,
                onCancel = function()
                    self:_ClearSelectedItem()
                    self:_UpdateNumber()
                end
            })
        end)
    else
        self.view.startTransBtnText.text = Language.LUA_FAC_TRANS_START_BTN_TEXT
        self.view.startTransBtn.onClick:AddListener(function()
            self:_DoChangeRoute()
        end)
    end
end

DomainItemTransferSelectCtrl._OpenSelectTargetRoot = HL.Method(HL.Opt(HL.Boolean)) << function(self, clickBtn)
    self:_ToggleLosslessTabOrTag(false)

    self.view.selectingTargetMask.gameObject:SetActive(true)
    self.view.leftItemSlotRoot.gameObject:SetActive(false)
    self.view.changeTransNode.gameObject:SetActive(false)
    self.view.selectEndPointRoot.gameObject:SetActive(true)
    self.view.leftLineWithEffect.gameObject:SetActive(false)
    self.view.leftLineWithoutEffect.gameObject:SetActive(true)
    self:_InitLeftSidePlatformInfo("")

    local domainList = {}
    for _, domainInfo in pairs(Tables.domainDataTable) do
        local domainId = domainInfo.domainId
        local notSelf = domainId ~= self.m_info.fromDomain
        local valid = Tables.factoryDomainItemTransmissionTable:ContainsKey(domainId)
        if notSelf and valid then
            table.insert(domainList, domainInfo)
        end
    end
    table.sort(domainList, Utils.genSortFunction({ "sortId" }, true))
    self.m_destinationCellCache:Refresh(#domainList, function(cell, index)
        local domainData = domainList[index]
        local domainName = domainData.domainName
        local domainId = domainData.domainId
        cell.text.text = domainName
        cell.icon:LoadSprite(UIConst.UI_SPRITE_FAC_TRANS, UIConst.FAC_TRANS_DOMAIN_ICONS[domainId])
        cell.button.onClick:AddListener(function()
            self:_OnSelectTargetDomain(domainId)
        end)
    end)

    if clickBtn == true then
        AudioAdapter.PostEvent("Au_UI_Popup_DetailsPanel_Close")
        self.animationWrapper:Play("domainItemtransferselect_panel_select_in")
    end

    if DeviceInfo.usingController then
        self.view.controllerFocusHintNode.gameObject:SetActive(false)

        self:Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.leftNode.groupId)
        InputManagerInst:ToggleGroup(self.view.leftNode.groupId, true)
        InputManagerInst:ToggleBinding(self.m_unFocusLeftBindId, false)

        self.view.transferTabNodeInputBindingGroupMonoTarget.enabled = true
        self.view.noConsumptionTransferDetails.enabled = true
        self.view.selectEndPointRootInputBindingGroupMonoTarget.enabled = true
        self.view.changeTransNode.enabled = true

        InputManagerInst:ToggleBinding(self.m_focusLeftBindId, false)

        self:_SetFocusTargetByIndex(1)
    end
end

DomainItemTransferSelectCtrl._UpdateNumber = HL.Method() << function(self)
    local view = self.view.leftItemSlotRoot
    local isFocusItemEqualChosen = self:_IsFocusItemEqualChosen()
    if not self:_IsItemModified() then
        local showNotEnoughItem = self.m_info.itemNum ~= self.m_info.itemNumMax
        local subCondition = not self.m_losslessTransmission and isFocusItemEqualChosen
        self.view.notEnoughItemText.text = tostring(self.m_info.itemNum)
        self.view.notEnoughItemRoot.gameObject:SetActive(showNotEnoughItem and subCondition)
        if self:_IsCurrentTransmitting() then
            local itemTargetPack = {
                id = self.m_chosenItemId,
                count = self.m_chosenItemCount,
            }
            view.itemTarget:InitItem(itemTargetPack, true)
            view.itemTarget:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        end
    elseif string.isEmpty(self.m_chosenItemId) then
        self.view.notEnoughItemRoot.gameObject:SetActive(false)
    else
        local itemTargetPack = {
            id = self.m_chosenItemId,
            count = self.m_chosenItemCount,
        }
        view.itemTarget:InitItem(itemTargetPack, true)
        view.itemTarget:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        local depotCount = self:_GetItemCount(self.m_chosenItemId)
        local subCondition = not self.m_losslessTransmission and isFocusItemEqualChosen
        self.view.notEnoughItemRoot.gameObject:SetActive(depotCount < self.m_chosenItemCount and subCondition)
        self.view.notEnoughItemText.text = tostring(depotCount)
    end
end

DomainItemTransferSelectCtrl._GetItemCount = HL.Method(HL.String).Return(HL.Number) << function(self, itemId)
    local factoryDepot = GameInstance.player.inventory.factoryDepot
    local depotInChapter = factoryDepot:GetOrFallback(Utils.getCurrentScope())
    local actualDepot = depotInChapter[ScopeUtil.ChapterIdStr2Int(self.m_info.fromDomain)]
    local count = actualDepot:GetCount(itemId)
    return count
end

DomainItemTransferSelectCtrl._GetItemValue = HL.Method(HL.String).Return(HL.Number) << function(self, itemId)
    local factoryItemCfg = Tables.factoryItemTable[itemId]
    return factoryItemCfg.value
end

DomainItemTransferSelectCtrl._InitLeftSidePlatformInfo = HL.Method(HL.String) << function(self, targetDomainId)
    local fromDomainId = self.m_info.fromDomain
    local domainInfo = Tables.domainDataTable[fromDomainId]
    self.view.platformFrom.destinationTxt.text = domainInfo.domainName
    self.view.platformFrom.domainLevelTxt.text = GameInstance.player.domainDevelopmentSystem:GetDomainDevelopmentLv(fromDomainId)
    self.view.platformFrom.domainIconImg:LoadSprite(UIConst.UI_SPRITE_FAC_TRANS,
                                                    UIConst.FAC_TRANS_DOMAIN_ICONS[fromDomainId])

    local toDomainId
    toDomainId = string.isEmpty(targetDomainId) and self.m_info.toDomain or targetDomainId
    local hasToDomain = not string.isEmpty(toDomainId)
    self.view.platformTo.domainLevelTxt.gameObject:SetActive(hasToDomain)
    self.view.platformTo.destinationTxt.gameObject:SetActive(hasToDomain)
    self.view.platformTo.domainLevelNode.gameObject:SetActive(hasToDomain)
    self.view.platformTo.domainIconImg.gameObject:SetActive(hasToDomain)
    if hasToDomain then
        self.view.platformTo.destinationTxt.text = Tables.domainDataTable[toDomainId].domainName
        self.view.platformTo.domainLevelTxt.text = GameInstance.player.domainDevelopmentSystem:GetDomainDevelopmentLv(toDomainId)
        self.view.platformTo.domainIconImg:LoadSprite(UIConst.UI_SPRITE_FAC_TRANS,
                                                      UIConst.FAC_TRANS_DOMAIN_ICONS[toDomainId])
    end
end

DomainItemTransferSelectCtrl._IsCurrentTransmitting = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_info.status ~= RouteStatus.idle
end

DomainItemTransferSelectCtrl._GetDefaultNumber = HL.Method().Return(HL.Number) << function(self)
    if self:_IsCurrentTransmitting() then
        return self.m_info.itemNumMax
    end
    return INIT_NUMBER_SELECTOR_CUR_VALUE
end

DomainItemTransferSelectCtrl._IsItemModified = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_chosenItemId ~= self.m_info.itemId or self.m_chosenItemCount ~= self.m_info.itemNumMax
end

DomainItemTransferSelectCtrl._GiveUpItemSelect = HL.Method() << function(self)
    if self:_IsCurrentTransmitting() then
        self.m_chosenItemId = self.m_info.itemId
        self:_ChangeCount(self.m_info.itemNumMax)
        self.m_chosenItemCount = 0
        return
    end

    self.m_chosenItemId = ""
    self.m_chosenItemCount = 0
end

DomainItemTransferSelectCtrl._DoChangeRoute = HL.Method() << function(self)
    local fromDomain = self.m_info.fromDomain
    local toDomain = self.m_targetDomain
    local index = self.m_info.index
    local lossless = self.m_losslessTransmission
    local itemId = self.m_chosenItemId
    local itemCount = self.m_chosenItemCount
    GameInstance.player.remoteFactory:SendReqSetHubTransRoute(fromDomain, toDomain, index, lossless, itemId, itemCount)
    self.m_waitingToClose = true
end

DomainItemTransferSelectCtrl._ChangeCount = HL.Method(HL.Number) << function(self, count)
    self.m_chosenItemCount = count
    self.view.depotExtraRoot.numberSelector:_Refresh(count)
end

DomainItemTransferSelectCtrl._DoResetRoute = HL.Method() << function(self)
    local routeInfo = self.m_info
    GameInstance.player.remoteFactory:SendReqResetHubTransRoute(routeInfo.fromDomain, routeInfo.index)
    self.m_waitingToClose = true
end

DomainItemTransferSelectCtrl._OnSelectTargetDomain = HL.Method(HL.String) << function(self, domainId)


    local hasChangedMode = self.m_losslessTransmission ~= self.view.transferTabNode.isOn
    if hasChangedMode then
        self:_ClearChosenItem()



        self.view.depot.view.depotContent.view.itemList:UpdateCount(0)
    end
    self.m_losslessTransmission = self.view.transferTabNode.isOn

    self:_UpdateTransmissionTag()

    self.animationWrapper:Play("domainItemtransferselect_panel_select_out", function()

    end)
    self:_InitLeftSidePlatformInfo(domainId)
    self.m_targetDomain = domainId
    self:_OpenDepot()
end

DomainItemTransferSelectCtrl._OnNotifyRouteInfoChange = HL.Method() << function(self)
    if self.m_waitingToClose then
        self:_Close()
    end
end

DomainItemTransferSelectCtrl._GetTimeText = HL.Method().Return(HL.String) << function(self)
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local lastTryTime = self.m_info.timeStamp - self.m_info.progress
    local curProgress = curTime - lastTryTime
    local needTime = Tables.factoryConst.domainTransportIntervalTime
    local curNeedTimeSec = needTime - curProgress
    if not self:_IsCurrentTransmitting() or self:_IsItemModified() then
        curNeedTimeSec = needTime
    end

    while curNeedTimeSec < 0 do
        local reverse = -curNeedTimeSec
        local times = reverse // needTime
        if reverse % needTime > 0 then
            times = times + 1
        end
        curNeedTimeSec = curNeedTimeSec + needTime * times
    end
    local curNeedHour = curNeedTimeSec // SEC_PER_HOUR
    local restSec = curNeedTimeSec % SEC_PER_HOUR
    local curNeedMin = restSec // SEC_PER_MIN
    restSec = restSec % SEC_PER_MIN
    if restSec % SEC_PER_MIN > 0 then
        curNeedMin = curNeedMin + 1
    end
    if curNeedMin >= MIN_PER_HOUR then
        curNeedMin = curNeedMin - 60
        curNeedHour = curNeedHour + 1
    end
    local hourText = ""
    if curNeedHour > 0 then
        hourText = string.format(Language.LUA_TIME_HOUR, curNeedHour)
    end
    local minuteText = ""
    if curNeedMin > 0 then
        minuteText = string.format(Language.LUA_TIME_MIN, curNeedMin)
    end
    local text = hourText .. minuteText
    return text
end

DomainItemTransferSelectCtrl._OnClickLosslessTransmissionInfoBtn = HL.Method() << function(self)
    UIManager:Open(PanelId.InstructionBook, LOSSLESS_TRANSMISSION_INSTRUCTION_ID)
end

DomainItemTransferSelectCtrl._OnClickLosslessValueInstructionBtn = HL.Method() << function(self)
    UIManager:Open(PanelId.InstructionBook, LOSSLESS_VALUE_INSTRUCTION_ID)
end

DomainItemTransferSelectCtrl._CheckIsValueValidLosslessTransmissionToggle = HL.Method(HL.Boolean).Return(HL.Boolean)
        << function(self, isOn)
    if not self.m_losslessTransmissionUnlocked then
        local fromDomainId = self.m_info.fromDomain
        local domainCfg = Tables.domainDataTable[fromDomainId]
        local domainTransmissionCfg = Tables.factoryDomainItemTransmissionTable[fromDomainId]
        local unlockLosslessLevel = domainTransmissionCfg.unlockLosslessLevel

        self:Notify(MessageConst.SHOW_TOAST,
                    string.format(Language.LUA_DOMAIN_ITEM_TRANSMISSION_CANT_SWITCH_MODE, domainCfg.domainName,
                                  unlockLosslessLevel))
    end
    return self.m_losslessTransmissionUnlocked
end


DomainItemTransferSelectCtrl._ToggleLosslessTabOrTag = HL.Method(HL.Boolean) << function(self, isTag)

    self.view.transferTabNode.gameObject:SetActive(not isTag)
    self.view.transferTagNode.gameObject:SetActive(isTag)
end

DomainItemTransferSelectCtrl._OnLosslessTransmissionToggleChanged = HL.Method(HL.Boolean) << function(self, isOn)
    self:_UpdateLosslessTabView(isOn)
end

DomainItemTransferSelectCtrl._SetDepotSlotStyleByLossless = HL.Method() << function(self)

    local state = self.m_losslessTransmission and DEPOT_SLOT_TRANSMISSION_STYLE or DEPOT_SLOT_NORMAL_STYLE
    self.view.depotNode:SetState(state)
end

DomainItemTransferSelectCtrl._InitNumberSelector = HL.Method() << function(self)
    local maxValue = INIT_NUMBER_SELECTOR_CUR_VALUE
    if not string.isEmpty(self.m_chosenItemId) then
        local factoryItemCfg = Tables.factoryItemTable[self.m_chosenItemId]
        maxValue = math.floor(self.m_maxTransmissionValue / factoryItemCfg.value)
    end

    self.view.depotExtraRoot.numberSelector:InitNumberSelector(self:_GetDefaultNumber(), TRANSMISSION_MIN_VALUE,
                                                               maxValue, function()
                if not string.isEmpty(self.m_chosenItemId) then
                    self.m_chosenItemCount = self.view.depotExtraRoot.numberSelector.curNumber
                else
                    self.m_chosenItemCount = 0
                end
                self:_UpdateNumber()
                self:_RefreshBtnAndText()
                self:_UpdateTransmissionValue()
            end)
end

DomainItemTransferSelectCtrl._UpdateNumberSelector = HL.Method() << function(self)
    if string.isEmpty(self.m_chosenItemId) then
        return
    end

    local factoryItemCfg = Tables.factoryItemTable[self.m_chosenItemId]

    local minValue = TRANSMISSION_MIN_VALUE
    local maxValue = math.floor(self.m_maxTransmissionValue / factoryItemCfg.value)

    local curValue = math.max(self.view.depotExtraRoot.numberSelector.curNumber, maxValue)

    self.view.depotExtraRoot.numberSelector:RefreshNumber(curValue, minValue, maxValue)
end

DomainItemTransferSelectCtrl._UpdateTransmissionValue = HL.Method() << function(self)
    local curTransmissionValue = 0
    if not string.isEmpty(self.m_chosenItemId) and self:_IsFocusItemEqualChosen() then
        local selectItemFacCfg = Tables.factoryItemTable[self.m_chosenItemId]
        curTransmissionValue = selectItemFacCfg.value * self.m_chosenItemCount
    end

    self.view.totalValueTxt.text = string.format("%d/%d", curTransmissionValue, self.m_maxTransmissionValue)
end

DomainItemTransferSelectCtrl._InitFromDomainInfo = HL.Method() << function(self)
    local fromDomainId = self.m_info.fromDomain
    local allowSend = GameInstance.player.remoteFactory:FacDomainTransAllowSend(fromDomainId)
    local allowLosslessSend = GameInstance.player.remoteFactory:FacDomainTransAllowLosslessSend(fromDomainId)
    local fromDomainLv = GameInstance.player.domainDevelopmentSystem:GetDomainDevelopmentLv(fromDomainId)
    local fromDomainTransCfg = Tables.factoryDomainItemTransmissionTable[fromDomainId]

    local refLevel
    if allowSend then

        if not fromDomainTransCfg.levelToCapacity:ContainsKey(fromDomainLv) then

            logger.error("当前达到的地区发展等级与HUB跨区域传输升级表中无法匹配，请检查")
            refLevel = #fromDomainTransCfg.levelToCapacity
        else
            refLevel = fromDomainLv
        end
    else
        refLevel = fromDomainTransCfg.unlockLevel
    end

    self.m_losslessTransmissionUnlocked = allowLosslessSend
    self.m_allowSend = allowSend

    if self.m_info.status ~= RouteStatus.idle then
        self.m_chosenItemId = self.m_info.itemId
        self.m_chosenItemCount = self.m_info.itemNumMax
        self.m_targetDomain = self.m_info.toDomain
        self.m_losslessTransmission = self.m_info.lossless
    end


    if not self.m_allowSend then
        local domainCfg = Tables.domainDataTable[fromDomainId]
        local versionSupport = fromDomainTransCfg.unlockLevel <= domainCfg.domainDevelopmentLevel.Count
        self.view.promptTxt.text = versionSupport and
                string.format(Language.LUA_DOMAIN_ITEM_TRANSMISSION_CANT_SEND, domainCfg.domainName,
                              fromDomainTransCfg.unlockLevel) or
                string.format(Language.LUA_FAC_TRANS_CUR_VERSION_NOT_ALLOW_SEND, domainCfg.domainName)
    end

    local succ, capacity = fromDomainTransCfg.levelToCapacity:TryGetValue(refLevel)
    self.m_maxTransmissionValue = succ and capacity or 0
    if not succ then
        logger.error("请检查配置：获取跨地区传输最大传输容量失败，出发地：", fromDomainId, "，等级：", refLevel)
    end
end

DomainItemTransferSelectCtrl._ProcessRecoverStateArg = HL.Method(HL.Opt(HL.Any)).Return(HL.Opt(HL.Any)) << function(self, arg)
    if arg == nil then
        return nil
    end
    return arg
end

DomainItemTransferSelectCtrl._ApplyRecoverState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    if not string.isEmpty(recoverState.targetDomain) then
        self.m_targetDomain = recoverState.targetDomain
    end
    if recoverState.transferTabNodeIsOn ~= nil then
        self.m_losslessTransmission = recoverState.transferTabNodeIsOn
    end
    if recoverState.chosenItemId ~= nil then
        self.m_chosenItemId = recoverState.chosenItemId
    end
    if recoverState.chosenItemCount ~= nil then
        self.m_chosenItemCount = recoverState.chosenItemCount
    end
end

DomainItemTransferSelectCtrl._InitTransmissionMode = HL.Method() << function(self)

    self.view.transferTabNode:SetIsOnWithoutNotify(self.m_losslessTransmission)
    self.view.transferTabNode.interactable = self.m_losslessTransmissionUnlocked

    self:_UpdateLosslessTabView(self.m_losslessTransmission)
    self:_UpdateTransmissionTag()
end

DomainItemTransferSelectCtrl._UpdateLosslessTabView = HL.Method(HL.Boolean) << function(self, losslessTransmission)
    if losslessTransmission then
        self.view.noConsumptionTransferTab:SetState("SelectState")
        self.view.warehouseTransferTab:SetState("UnselectState")
    else
        self.view.warehouseTransferTab:SetState("SelectState")
        self.view.noConsumptionTransferTab:SetState(self.m_losslessTransmissionUnlocked and "UnlockState" or "LockState")
    end
end

DomainItemTransferSelectCtrl._UpdateTransmissionTag = HL.Method() << function(self)

    self.view.warehouseTransferNode.gameObject:SetActive(not self.m_losslessTransmission)
    self.view.noConsumptionTransferNode.gameObject:SetActive(self.m_losslessTransmission)
end

DomainItemTransferSelectCtrl._GetCommonPopUpRecoverState = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    if PhaseManager:GetTopPhaseId() ~= PhaseId.DomainItemTransfer then
        return
    end
    local isOpen, commonPopUpCtrl = UIManager:IsOpen(PanelId.CommonPopUp)
    if not isOpen or not commonPopUpCtrl:IsShow() then
        return
    end
    local recoverArg = commonPopUpCtrl:GetCurPhaseStateArg()
    if not self:_IsDomainItemTransferSelectCommonPopUpArg(recoverArg) then
        return
    end
    return recoverArg
end

DomainItemTransferSelectCtrl._IsDomainItemTransferSelectCommonPopUpArg = HL.Method(HL.Opt(HL.Any)).Return(HL.Boolean) << function(self, arg)
    if arg == nil or type(arg) ~= "table" or string.isEmpty(arg.content) then
        return false
    end
    return arg.content == Language.LUA_FAC_TRANS_CONFIRM_RESET
        or arg.content == Language.LUA_FAC_TRANS_CONFIRM_MODIFY
        or arg.content == Language.LUA_FAC_TRANS_CONFIRM_MODIFY_LOSSLESS
end

DomainItemTransferSelectCtrl._TryRecoverInstructionBook = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil or recoverState.instructionBookArg == nil then
        return
    end
    if PhaseManager:GetTopPhaseId() ~= PhaseId.DomainItemTransfer then
        return
    end
    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and instructionCtrl:IsShow() then
        return
    end
    UIManager:Open(PanelId.InstructionBook, recoverState.instructionBookArg)
    recoverState.instructionBookArg = nil
end

DomainItemTransferSelectCtrl._TryRecoverCommonPopUp = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil or recoverState.commonPopUpArg == nil then
        return
    end
    if PhaseManager:GetTopPhaseId() ~= PhaseId.DomainItemTransfer then
        return
    end
    if not self:_IsDomainItemTransferSelectCommonPopUpArg(recoverState.commonPopUpArg) then
        return
    end
    local isOpen, commonPopUpCtrl = UIManager:IsOpen(PanelId.CommonPopUp)
    if isOpen and commonPopUpCtrl:IsShow() then
        return
    end
    local commonPopUpArg = recoverState.commonPopUpArg
    commonPopUpArg.onConfirm = function()
        if commonPopUpArg.content == Language.LUA_FAC_TRANS_CONFIRM_RESET then
            self:_DoResetRoute()
        else
            self:_DoChangeRoute()
        end
    end
    commonPopUpArg.onCancel = function()
        self:_ClearSelectedItem()
        self:_UpdateNumber()
    end
    self:Notify(MessageConst.SHOW_POP_UP, commonPopUpArg)
    recoverState.commonPopUpArg = nil
end

DomainItemTransferSelectCtrl._TryRecoverDepotState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    local chosenItemId = self.m_chosenItemId
    if not string.isEmpty(chosenItemId) then
        local depotContent = self.view.depot.depotContent
        local itemIndex = depotContent:GetItemIndex(chosenItemId)
        if itemIndex ~= nil then
            depotContent.view.itemList:ScrollToIndex(CSIndex(itemIndex), true)
        end
    end

    local numberSelectorCount = self:_GetRecoverNumberSelectorCount(recoverState)
    if numberSelectorCount > 0 and not string.isEmpty(chosenItemId) then
        local numberSelector = self.view.depotExtraRoot.numberSelector
        numberSelector:RefreshNumber(numberSelectorCount, numberSelector.m_min, numberSelector.m_max)
        self.m_chosenItemCount = numberSelector.curNumber
    end

    self:_RefreshBtnAndText()
    self:_RefreshLeftSideItem()
    self:_UpdateNumber()
    self:_UpdateTransmissionValue()
end

DomainItemTransferSelectCtrl._GetRecoverNumberSelectorCount = HL.Method(HL.Any).Return(HL.Number) << function(self, recoverState)
    local numberSelectorCount = recoverState.numberSelectorCount
    if type(numberSelectorCount) ~= "number" then
        return 0
    end
    local numberSelector = self.view.depotExtraRoot.numberSelector
    local minNumber = numberSelector.m_min
    local maxNumber = numberSelector.m_max
    if type(minNumber) ~= "number" or type(maxNumber) ~= "number" or maxNumber < minNumber then
        return 0
    end
    return math.min(math.max(math.floor(numberSelectorCount), minNumber), maxNumber)
end



DomainItemTransferSelectCtrl.m_focusLeftBindId = HL.Field(HL.Number) << -1

DomainItemTransferSelectCtrl.m_unFocusLeftBindId = HL.Field(HL.Number) << -1

DomainItemTransferSelectCtrl.m_destinationFocusLuaIndex = HL.Field(HL.Number) << 1

DomainItemTransferSelectCtrl.m_focusItemId = HL.Field(HL.String) << ""

DomainItemTransferSelectCtrl._InitControllerAbility = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })


    self.m_focusLeftBindId = InputManagerInst:CreateBindingByActionId("domain_trans_select_focus_left", function()

        self:Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
            panelId = PANEL_ID,
            isGroup = true,
            id = self.view.leftNode.groupId,
            hintPlaceholder = self.view.controllerHintPlaceholder,
            rectTransform = self.view.controllerFocusHintNode,
            useNormalFrame = true,
            noHighlight = true,
        })
        InputManagerInst:ToggleGroup(self.view.leftNode.groupId, true)
        InputManagerInst:ToggleBinding(self.m_unFocusLeftBindId, true)
        self.view.controllerFocusHintNode.gameObject:SetActive(false)

        local hasItemFocus = self.view.leftItemSlotRoot.itemCanceled.gameObject.activeInHierarchy or
                self.view.leftItemSlotRoot.itemTarget.gameObject.activeInHierarchy
        self.view.leftItemSlotRootInputBindingGroupMonoTarget.enabled = hasItemFocus
        self.view.transferTabNodeInputBindingGroupMonoTarget.enabled = true
        self.view.noConsumptionTransferDetails.enabled = true
        self.view.selectEndPointRootInputBindingGroupMonoTarget.enabled = true
        self.view.changeTransNode.enabled = true
        Notify(MessageConst.HIDE_COMMON_HOVER_TIP)

    end, self.view.inputGroup.groupId)


    self.m_unFocusLeftBindId = InputManagerInst:CreateBindingByActionId("common_back", function()
        self:_ManuallyStopFocusLeftNode()
    end, self.view.leftNode.groupId)

    local depotItemList = self.view.depot.view.depotContent.view.itemList
    depotItemList.onGraduallyShowFinish:AddListener(function()
        self:_InitDepotItemFocus()
    end)

    depotItemList.onSelectedCell:AddListener(function(go, csIndex)
        self:_OnSelectedCell(go, csIndex)
    end)
end

DomainItemTransferSelectCtrl._ManuallyStopFocusLeftNode = HL.Method() << function(self)
    self:Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.leftNode.groupId)
    InputManagerInst:ToggleGroup(self.view.leftNode.groupId, false)
    InputManagerInst:ToggleBinding(self.m_unFocusLeftBindId, false)

    self.view.controllerFocusHintNode.gameObject:SetActive(true)

    self.view.leftItemSlotRootInputBindingGroupMonoTarget.enabled = false
    self.view.transferTabNodeInputBindingGroupMonoTarget.enabled = false
    self.view.noConsumptionTransferDetails.enabled = false
    self.view.selectEndPointRootInputBindingGroupMonoTarget.enabled = false
    self.view.changeTransNode.enabled = false
end

DomainItemTransferSelectCtrl._SetFocusTargetByIndex = HL.Method(HL.Number) << function(self, index)
    self.m_destinationFocusLuaIndex = index

    local firstCell = self.m_destinationCellCache:Get(index)
    self:SetNaviTarget(firstCell.siteCell)
end

DomainItemTransferSelectCtrl._InitDepotItemFocus = HL.Method() << function(self)
    local focusLuaIndexIndex = 1
    local itemList = self.view.depot.view.depotContent.m_itemInfoList
    for luaIndex, item in ipairs(itemList) do
        if item.id == self.m_chosenItemId then
            focusLuaIndexIndex = luaIndex
        end
    end


    self.view.depot.view.depotContent.view.itemList:ScrollToIndex(CSIndex(focusLuaIndexIndex), true)

    local itemSlot = self.view.depot.view.depotContent.m_getCell(focusLuaIndexIndex)
    if itemSlot then
        self:SetNaviTarget(itemSlot.view.item.view.button)
        self.m_focusItemId = itemList[focusLuaIndexIndex].id
    else
        self:ClearNaviTarget()
        self.m_focusItemId = ""
    end

    self:_UpdateNumber()
    self:_RefreshBtnAndText()
    self:_UpdateTransmissionValue()
end

DomainItemTransferSelectCtrl._OnFocusTarget = HL.Method(HL.Forward("ItemSlot"), HL.Table, HL.Number)
        << function(self, cell, info, luaIndex)
    self.m_focusItemId = info.id
    InputManagerInst:SetBindingText(cell.item.view.button.hoverConfirmBindingId, Language["key_hint_domain_trans_select_item"])

    if string.isEmpty(self.m_chosenItemId) then
        return
    end

    self:_UpdateNumber()
    self:_RefreshBtnAndText()
    self:_UpdateTransmissionValue()
end

DomainItemTransferSelectCtrl._IsFocusItemEqualChosen = HL.Method().Return(HL.Boolean) << function(self)
    if DeviceInfo.usingController then
        return self.m_focusItemId == self.m_chosenItemId
    else
        return true
    end
end



HL.Commit(DomainItemTransferSelectCtrl)
