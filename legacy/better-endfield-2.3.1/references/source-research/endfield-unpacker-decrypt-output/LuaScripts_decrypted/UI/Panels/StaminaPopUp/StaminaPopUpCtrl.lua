local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local CommonPopUpCtrl = require_ex('UI/Panels/CommonPopUp/CommonPopUpCtrl')
local PANEL_ID = PanelId.StaminaPopUp
local PHASE_ID = PhaseId.StaminaPopUp

StaminaPopUpCtrl = HL.Class('StaminaPopUpCtrl', uiCtrl.UICtrl)







StaminaPopUpCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_STAMINA_CHANGED] = '_OnStaminaChanged',
    [MessageConst.ON_ITEM_COUNT_CHANGED] = '_OnItemCountChangedImm',
    [MessageConst.ON_WALLET_CHANGED] = '_OnWalletChanged',
    [MessageConst.ON_RECOVER_AP_BY_MONEY_COUNT_RESTORED] = '_OnRecoverApByMoneyCountRestored',
}

local ExchangeStateEnum = {
    QuickExchange = 0,
    ExchangeOfItem = 1,
    ExchangeOfOriginium = 2,
}

StaminaPopUpCtrl.m_arg = HL.Field(HL.Table)


StaminaPopUpCtrl.m_exchangeState = HL.Field(HL.Number) << 0

StaminaPopUpCtrl.m_coroutineRecover = HL.Field(HL.Thread)

StaminaPopUpCtrl.m_totalExchangeStamina = HL.Field(HL.Number) << 0

StaminaPopUpCtrl.m_genItemCells = HL.Field(HL.Forward("UIListCache"))

StaminaPopUpCtrl.m_allItemTableInfoList = HL.Field(HL.Table)

StaminaPopUpCtrl.m_invItemInfoList = HL.Field(HL.Table)

StaminaPopUpCtrl.m_quickExchangeItemInfo = HL.Field(HL.Table)

StaminaPopUpCtrl.m_staminaCloseFun = HL.Field(HL.Function)

StaminaPopUpCtrl.m_curSelItemIndex = HL.Field(HL.Number) << 1

StaminaPopUpCtrl.m_isClosing = HL.Field(HL.Boolean) << false




StaminaPopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    if not arg then
        self.m_arg = {}
    else
        self.m_arg = arg
    end
    self.m_staminaCloseFun = self.m_arg.closeFun

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self.m_isClosing = false
    local itemTog = self.view.exchangeNode.costItemTabTog
    local originiumTog = self.view.exchangeNode.costOriginiumTabTog
    itemTog.onValueChanged:RemoveAllListeners()
    itemTog.onValueChanged:AddListener(function(isOn)
        if isOn then
            self:_RefreshUIExchangeCostItem()

            self.view.exchangeNode.costItemAnim:Play("staminapopup_costltemlistscrollview_in")
        end
    end)

    originiumTog.onValueChanged:RemoveAllListeners()
    originiumTog.onValueChanged:AddListener(function(isOn)
        if isOn then
            self:_RefreshUIExchangeCostOriginium()
        end
    end)

    self.view.exchangeNode.costOriginiumNode.tipsBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            itemId = Tables.dungeonConst.recoverApMoneyId,
            transform = self.view.exchangeNode.costOriginiumNode.imgRectTransform,
            posType = UIConst.UI_TIPS_POS_TYPE.RightTop,
        })
    end)

    self.view.fullScreenCloseBtn.onClick:AddListener(function()
        self:_DoClose()
    end)
    self.view.closeBtn.onClick:AddListener(function()
        self:_DoClose()
    end)
    self.view.confirmBtn.onClick:AddListener(function()
        self:_OnConfirm()
    end)

    self.m_genItemCells = UIUtils.genCellCache(self.view.exchangeNode.costItemCell)
    self:_InitBasicUI()

    if self.m_arg.isQuickExchange then
        self:_InitQuickExchange(self.m_arg)
    else
        self:_InitNormalExchange()
    end

    self:_RefreshTickRecoverTxt()
    self:_TryStartTickRecover()


    self.view.exchangeNode.costItemScrollRect.horizontalNormalizedPosition = 0;

    AudioManager.PostEvent("au_ui_menu_side_open")
end

StaminaPopUpCtrl.OnClose = HL.Override() << function(self)
    self:_StopTickRecover()
    self.view.quickExchangeNode.ltItemMark:EndTickLimitTime()
end


StaminaPopUpCtrl.SetStaminaCloseFun = HL.Method(HL.Function) << function(self, staminaCloseFun)
    self.m_staminaCloseFun = staminaCloseFun
end


StaminaPopUpCtrl._OnItemCountChangedImm = HL.Method(HL.Table) << function(self, eventData)
    if not eventData or self.m_isClosing then
        return
    end

    local itemId2DiffCount = unpack(eventData)
    if self.m_exchangeState == ExchangeStateEnum.ExchangeOfItem then

        if self.m_allItemTableInfoList then
            for _, tableInfo in pairs(self.m_allItemTableInfoList) do
                if itemId2DiffCount:ContainsKey(tableInfo.itemId) then
                    self:_UpdateItemData()
                    self:_RefreshUIExchangeCostItem()
                    break
                end
            end
        end
    elseif self.m_exchangeState == ExchangeStateEnum.QuickExchange then

        if itemId2DiffCount:ContainsKey(self.m_quickExchangeItemInfo.itemId) then
            self:_UpdateQuickExchangeItemData()
            self:_RefreshUIQuickExchange()
            if self.m_quickExchangeItemInfo.count <= 0 then
                self:_DoClose()
            end
        end
    end


end

StaminaPopUpCtrl._OnWalletChanged = HL.Method(HL.Table) << function(self, eventData)
    if not eventData then
        return
    end

    local data = unpack(eventData)
    if self.m_exchangeState == ExchangeStateEnum.ExchangeOfOriginium
        and data == Tables.dungeonConst.recoverApMoneyId then
        self:_RefreshUIExchangeCostOriginium()
    end
end

StaminaPopUpCtrl._OnStaminaChanged = HL.Method() << function(self)
    self:_TryStartTickRecover()
    self:_RefreshUICurrentAndTargetStamina()
end

StaminaPopUpCtrl._OnRecoverApByMoneyCountRestored = HL.Method() << function(self)

    if self.m_exchangeState == ExchangeStateEnum.ExchangeOfOriginium then

        Notify(MessageConst.SHOW_POP_UP, {
            hideCancel = true,
            content = Language.LUA_ORIGINIUM_EXCHANGE_STAMINA_REFRESH,
            onConfirm = function()
            end,
        })

        self:_RefreshUIExchangeCostOriginium()
    end
end

StaminaPopUpCtrl._OnConfirm = HL.Method() << function(self)
    AudioAdapter.PostEvent("au_ui_item_ap_supply_use")

    local targetStamina = self.m_totalExchangeStamina + GameInstance.player.inventory.curStamina
    local staminaLimit = Tables.dungeonConst.staminaCapacity
    if targetStamina > staminaLimit then
        local originiumItemCfg = Utils.tryGetTableCfg(Tables.itemTable, Tables.dungeonConst.recoverApMoneyId)
        local originiumName = originiumItemCfg.name
        local tipStr = string.format(Language.LUA_STAMINA_POPUP_EXCEED_STAMINA_LIMIT_TIP, originiumName, staminaLimit)
        Notify(MessageConst.SHOW_TOAST, tipStr)
        return
    end

    if self.m_exchangeState == ExchangeStateEnum.ExchangeOfItem then


        local msg = CS.Proto.CS_DUNGEON_RECOVER_AP()
        msg.UseMoney = false

        for i, info in ipairs(self.m_invItemInfoList) do
            if info.selectCount > info.invCount then
                Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_STAMINA_LACK_ITEM_TOAST, info.itemTableData.name))
                return
            end
            if info.selectCount > 0 then
                if info.isLTItem then

                    local bundle = CS.Proto.INST_ITEM_BUNDLE()
                    bundle.Id = info.itemId
                    bundle.InstId = info.instId
                    bundle.Count = info.selectCount
                    msg.InstItems:Add(bundle)
                else

                    local bundle = CS.Proto.ITEM_BUNDLE()
                    bundle.Id = info.itemId
                    bundle.Count = info.selectCount
                    msg.Items:Add(bundle)
                end
            end
        end
        GameInstance.player.inventory:SendUIMsg(msg)
    elseif self.m_exchangeState == ExchangeStateEnum.ExchangeOfOriginium then



        local hasValue
        local usedExchangeCount
        hasValue, usedExchangeCount = GameInstance.player.globalVar:TryGetServerVar(GEnums.ServerGameVarEnum.RecoverApByMoneyCount)
        if not hasValue then
            usedExchangeCount = 0
        end


        local nextBuyCount = usedExchangeCount + 1
        local requiredOriginiumNum = 1
        if Tables.originiumStaminaCost:ContainsKey(nextBuyCount) then

            requiredOriginiumNum = Tables.originiumStaminaCost:GetValue(nextBuyCount)
        else

            local maxBuyCount = 0
            for buyCount, _ in pairs(Tables.originiumStaminaCost) do
                if buyCount > maxBuyCount then
                    maxBuyCount = buyCount
                end
            end
            if maxBuyCount > 0 then
                requiredOriginiumNum = Tables.originiumStaminaCost:GetValue(maxBuyCount)
            end
        end


        local curOriginiumNum = GameInstance.player.inventory:GetItemCount(
            Utils.getCurrentScope(),
            Utils.getCurrentChapterId(),
            Tables.dungeonConst.recoverApMoneyId
        )


        if curOriginiumNum < requiredOriginiumNum then

            local originiumItemCfg = Utils.tryGetTableCfg(Tables.itemTable, Tables.dungeonConst.recoverApMoneyId)
            local originiumName = originiumItemCfg.name
            Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_STAMINA_LACK_ITEM_TOAST, originiumName))
            return
        end


        local originiumItemCfg = Utils.tryGetTableCfg(Tables.itemTable, Tables.dungeonConst.recoverApMoneyId)
        local originiumName = originiumItemCfg.name
        local recoverStaminaNum = Tables.dungeonConst.apRecoverValueByMoney


        local warningText = string.format(
            Language.LUA_ORIGINIUM_EXCHANGE_STAMINA_WARNING,
            requiredOriginiumNum,
            recoverStaminaNum
        )


        local serializedHintKeyHide = "ORIGINIUM_EXCHANGE_STAMINA_HIDE_TODAY"
        local succ, hideToday = ClientDataManagerInst:GetBool(serializedHintKeyHide, false, false, "StaminaPopUp")


        local serializedHintKeyExchange = "ORIGINIUM_EXCHANGE_STAMINA_LAST_EXCHANGE_PRICE"

        if hideToday then

            local msg = CS.Proto.CS_DUNGEON_RECOVER_AP()
            msg.UseMoney = true
            msg.ExpectMoneyBuyCount = nextBuyCount
            GameInstance.player.inventory:SendUIMsg(msg)

            ClientDataManagerInst:SetInt(serializedHintKeyExchange, requiredOriginiumNum, false, "StaminaPopUp", EClientDataTimeValidType.Permanent)

            self:_DoClose()
            return
        else

            local hideTodayToggle = false
            Notify(MessageConst.SHOW_POP_UP, {
                content = warningText,
                toggle = {
                    isOn = false,
                    onValueChanged = function(isOn)
                        hideTodayToggle = isOn
                    end,
                    toggleText = Language.LUA_ORIGINIUM_EXCHANGE_STAMINA_HIDE_TODAY,
                    styleType = CommonPopUpCtrl.EToggleStyle.Square,
                },
                onConfirm = function()

                    ClientDataManagerInst:SetBool(serializedHintKeyHide, hideTodayToggle, false, "StaminaPopUp", EClientDataTimeValidType.CurrentDay)

                    local msg = CS.Proto.CS_DUNGEON_RECOVER_AP()
                    msg.UseMoney = true
                    msg.ExpectMoneyBuyCount = nextBuyCount
                    GameInstance.player.inventory:SendUIMsg(msg)

                    ClientDataManagerInst:SetInt(serializedHintKeyExchange, requiredOriginiumNum, false, "StaminaPopUp", EClientDataTimeValidType.Permanent)

                    self:_DoClose()
                end,
                onCancel = function()


                end
            })
        end
    else

        local quickExchangeInfo = self.m_quickExchangeItemInfo
        if quickExchangeInfo.selectCount <= 0 then
            return
        end
        if quickExchangeInfo.selectCount > quickExchangeInfo.count then
            Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_STAMINA_LACK_ITEM_TOAST, quickExchangeInfo.itemTableData.name))
            return
        end


        local msg = CS.Proto.CS_DUNGEON_RECOVER_AP()
        msg.UseMoney = false
        if quickExchangeInfo.instId > 0 then


            local bundle = CS.Proto.INST_ITEM_BUNDLE()
            bundle.Id = quickExchangeInfo.itemId
            bundle.InstId = quickExchangeInfo.instId
            bundle.Count = quickExchangeInfo.selectCount
            msg.InstItems:Add(bundle)
        else


            local bundle = CS.Proto.ITEM_BUNDLE()
            bundle.Id = quickExchangeInfo.itemId
            bundle.Count = quickExchangeInfo.selectCount
            msg.Items:Add(bundle)
        end
        GameInstance.player.inventory:SendUIMsg(msg)
    end


    if self.m_exchangeState ~= ExchangeStateEnum.ExchangeOfOriginium then
        self:_DoClose()
    end
end

StaminaPopUpCtrl._DoClose = HL.Method() << function(self)
    if self.m_isClosing then
        return
    end
    Notify(MessageConst.HIDE_COMMON_HOVER_TIP, { noAnimation = true })
    self.m_isClosing = PhaseManager:PopPhase(PHASE_ID, function()


        local cell = self.m_genItemCells:Get(self.m_curSelItemIndex)
        if cell and not IsNull(cell.view.item.view.button) then
            cell.view.item.view.button.interactable = false
        end

        if self.m_staminaCloseFun ~= nil then
            self.m_staminaCloseFun()
        end
        self.m_isClosing = false
    end)

    AudioManager.PostEvent("au_ui_menu_side_close")
end

StaminaPopUpCtrl._InitQuickExchange = HL.Method(HL.Any) << function(self, arg)
    self.m_quickExchangeItemInfo = {
        itemId = arg.itemId,
        instId = arg.instId,
    }
    self.m_exchangeState = ExchangeStateEnum.QuickExchange
    self.view.exchangeState:SetState("QuickExchangeState")
    self.view.buttonOperateState:SetState("OnlyConfirmState")

    self.view.confirmBtn.onClick:ChangeBindingPlayerAction("add_stamina_fast_confirm")

    self:_UpdateQuickExchangeItemData()
    self:_RefreshUIQuickExchange()
end

StaminaPopUpCtrl._InitNormalExchange = HL.Method() << function(self)
    self.view.exchangeState:SetState("ExchangeState")
    self:_UpdateItemData()

    local isExchangeItem = true
    if self.m_arg.exchangeState then
        isExchangeItem = self.m_arg.exchangeState == ExchangeStateEnum.ExchangeOfItem
        self.m_arg.exchangeState = nil
    else

        isExchangeItem = self:_HasItemForExchange()
    end

    if isExchangeItem then
        self.view.exchangeNode.costItemTabTog.isOn = true
        self:_RefreshUIExchangeCostItem()
    else
        self.view.exchangeNode.costOriginiumTabTog.isOn = true
        self:_RefreshUIExchangeCostOriginium()
    end
end

StaminaPopUpCtrl._InitBasicUI = HL.Method() << function(self)

    local originiumItemCfg = Utils.tryGetTableCfg(Tables.itemTable, Tables.dungeonConst.recoverApMoneyId)
    local originiumName = originiumItemCfg.name
    local staminaItemCfg = Utils.tryGetTableCfg(Tables.itemTable, Tables.globalConst.apItemId)
    local staminaName = staminaItemCfg.name
    local tabName = string.format(Language.LUA_STAMINA_POPUP_EXCHANGE_COST_ORIGINIUM, originiumName)
    self.view.exchangeNode.costOriginiumNode.staminaNameTxt.text = staminaName
    self.view.insufficientOriginiumTxt.text = string.format(Language.LUA_STAMINA_POPUP_INSUFFICIENT_TXT, originiumName)
    self.view.exchangeNode.costOriginiumTxt.text = tabName
    self.view.exchangeNode.costOriginiumTxt2.text = tabName
end

StaminaPopUpCtrl._InitItemTableData = HL.Method() << function(self)
    self.m_allItemTableInfoList = {}
    for id, cfg in pairs(Tables.recoverApItemTable) do
        self.m_allItemTableInfoList[id] = {
            itemId = id,
            recoverValue = cfg.apRecoverValue,
            itemTableData = Tables.itemTable:GetValue(id),
        }
    end
end

StaminaPopUpCtrl._UpdateItemData = HL.Method() << function(self)

    if not self.m_allItemTableInfoList then
        self:_InitItemTableData()
    end

    self.m_invItemInfoList = {}
    local valuableDepotType = GEnums.ItemValuableDepotType.CommercialItem;
    local inventory = GameInstance.player.inventory
    local contains = inventory.valuableDepots:ContainsKey(valuableDepotType)

    local depot
    if contains then
        depot = inventory.valuableDepots[valuableDepotType]:GetOrFallback(CS.Beyond.Gameplay.Scope.Create(GEnums.ScopeName.Main))
    end
    if not depot then
        return
    end

    for _, tableInfo in pairs(self.m_allItemTableInfoList) do

        local isNormalItem, itemData = depot.normalItems:TryGetValue(tableInfo.itemId)
        if isNormalItem and itemData.count > 0 then

            if itemData.count > 0 then
                local selCount = 0
                if self.m_arg.selItemCountMap and self.m_arg.selItemCountMap[tableInfo.itemId] then
                    selCount = self.m_arg.selItemCountMap[tableInfo.itemId]
                end
                table.insert(self.m_invItemInfoList, {
                    itemId = tableInfo.itemId,
                    instId = 0,
                    recoverValue = tableInfo.recoverValue,
                    itemTableData = tableInfo.itemTableData,
                    invCount = itemData.count,
                    selectCount = selCount,
                    expireTs = 0,

                    isLTItem = false,
                })
            end
        end
    end

    for instId, instItemBundle in pairs(depot.instItems) do
        local tableInfo = self.m_allItemTableInfoList[instItemBundle.id]
        if tableInfo then
            if instItemBundle.count > 0 then
                local selCount = 0
                if self.m_arg.selInstItemCountMap and self.m_arg.selInstItemCountMap[instId] then
                    selCount = self.m_arg.selInstItemCountMap[instId]
                end
                table.insert(self.m_invItemInfoList, {
                    itemId = tableInfo.itemId,
                    instId = instId,
                    recoverValue = tableInfo.recoverValue,
                    itemTableData = tableInfo.itemTableData,
                    invCount = instItemBundle.count,
                    selectCount = selCount,
                    expireTs = instItemBundle.instData.expireTs,
                    isLTItem = true,
                })
            end
        end
    end
    self.m_arg.selItemCountMap = nil
    self.m_arg.selInstItemCountMap = nil







    table.sort(self.m_invItemInfoList, function(a, b)
        if a.isLTItem ~= b.isLTItem then
            return a.isLTItem
        end
        if a.expireTs ~= b.expireTs then
            return a.expireTs < b.expireTs
        end
        if a.invCount ~= b.invCount then
            return a.invCount < b.invCount
        end
        if a.itemId ~= b.itemId then
            return a.itemId < b.itemId
        end
        if a.instId ~= b.instId then
            return a.instId < b.instId
        end
    end)
end

StaminaPopUpCtrl._UpdateQuickExchangeItemData = HL.Method() << function(self)

    local info = self.m_quickExchangeItemInfo
    if not self.m_allItemTableInfoList then
        self:_InitItemTableData()
    end
    local tableInfo = self.m_allItemTableInfoList[info.itemId]

    local count = 0
    if info.instId > 0 then
        local valuableDepotType = GEnums.ItemValuableDepotType.CommercialItem;
        local inventory = GameInstance.player.inventory
        local contains = inventory.valuableDepots:ContainsKey(valuableDepotType)
        if contains then

            local depot = inventory.valuableDepots[valuableDepotType]:GetOrFallback(CS.Beyond.Gameplay.Scope.Create(GEnums.ScopeName.Main))
            if depot then
                count = depot.instItems[info.instId].count
            end
        end

        self.view.quickExchangeNode.ltItemMark.gameObject:SetActive(true)
        local limitTimeInfo = Utils.getLTItemExpireInfo(info.itemId, info.instId)
        self.view.quickExchangeNode.ltItemMark:StartTickLimitTime(limitTimeInfo.expireTime, limitTimeInfo.almostExpireTime)
    else
        count = GameInstance.player.inventory:GetItemCount(
            Utils.getCurrentScope(),
            Utils.getCurrentChapterId(),
            self.m_quickExchangeItemInfo.itemId
        )

        self.view.quickExchangeNode.ltItemMark.gameObject:SetActive(false)
    end

    info.name = tableInfo.itemTableData.name
    info.desc = tableInfo.itemTableData.desc
    info.decoDesc = tableInfo.itemTableData.decoDesc
    info.imgPath = tableInfo.itemTableData.iconId
    info.recoverValue = tableInfo.recoverValue
    info.count = count
    info.selectCount = self.m_arg.quickExchangeSelectCount or 1
    self.m_arg.quickExchangeSelectCount = nil
end

StaminaPopUpCtrl._HasItemForExchange = HL.Method().Return(HL.Boolean) << function(self)
    if not self.m_invItemInfoList or (#self.m_invItemInfoList <= 0) then
        return false
    end

    return true
end

StaminaPopUpCtrl._RefreshUIExchangeCostItem = HL.Method() << function(self)
    self.view.exchangeNode.keyHintDown.gameObject:SetActive(true)
    self.view.exchangeNode.keyHintUp.gameObject:SetActive(false)


    self.m_exchangeState = ExchangeStateEnum.ExchangeOfItem

    if not self:_HasItemForExchange() then
        self.view.exchangeNode.contentState:SetState("ItemInsufficientState")
        self.view.buttonOperateState:SetState("NotSelectItemState")
        self.m_totalExchangeStamina = 0
        self:_RefreshUICurrentAndTargetStamina()
        return
    end

    self.view.exchangeNode.contentState:SetState("ItemEnoughState")
    self:_CalculateExchangeStaminaOfItemList()


    if DeviceInfo.usingController then
        self:_InitItemScrollController()
    end

    if #self.m_invItemInfoList ~= 0 then
        self:ClearNaviTarget()
    end
    self.m_genItemCells:Refresh(#self.m_invItemInfoList, function(cell, luaIndex)
        local info = self.m_invItemInfoList[luaIndex]
        local args = {
            itemBundle = { id = info.itemId, count = info.invCount, instId = info.instId },
            curNum = info.selectCount,
            tryChangeNum = nil,
            bindInputChangeNum = true,
            onNumChanged = function(curNum)
                self.m_invItemInfoList[luaIndex].selectCount = curNum
                cell:_UpdateCount(curNum)
                self:_CalculateExchangeStaminaOfItemList()
                self:_RefreshUIExchangeCostItemStaminaState()
                self:_RefreshUICurrentAndTargetStamina()
            end,
        }
        cell:InitItemCellForSelect(args)

        if luaIndex == self.m_curSelItemIndex then
            self:SetNaviTarget(cell.view.item.view.button)
        end

        local btn = cell.view.item.view.button
        btn.onIsNaviTargetChanged = function(isTarget)
            if isTarget then
                self.m_curSelItemIndex = luaIndex
            end
        end
    end)
    local scrollRect = self.view.exchangeNode.costItemScrollRect
    if scrollRect and scrollRect.content and self.m_curSelItemIndex == 1 then



        CS.UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect.content)
        scrollRect.horizontalNormalizedPosition = 0
    end

    self:_RefreshUIExchangeCostItemStaminaState()
    self:_RefreshUICurrentAndTargetStamina()
end

StaminaPopUpCtrl._RefreshUIExchangeCostItemStaminaState = HL.Method() << function(self)
    if self.m_totalExchangeStamina <= 0 then
        self.view.buttonOperateState:SetState("NotSelectItemState")
    else
        self.view.buttonOperateState:SetState("CostItemState")
        local formatString = Language.LUA_STAMINA_POPUP_EXCHANGE_COST_ITEM_TIP
        self.view.costItemTipTxt:SetAndResolveTextStyle(string.format(formatString, self.m_totalExchangeStamina))
    end
end

StaminaPopUpCtrl._RefreshUIExchangeCostOriginium = HL.Method() << function(self)
    self.view.exchangeNode.keyHintDown.gameObject:SetActive(false)
    self.view.exchangeNode.keyHintUp.gameObject:SetActive(true)

    self.m_exchangeState = ExchangeStateEnum.ExchangeOfOriginium
    self.view.exchangeNode.contentState:SetState("OriginiumState")

    local recoverStaminaNum = Tables.dungeonConst.apRecoverValueByMoney
    local curOriginiumNum = GameInstance.player.inventory:GetItemCount(
        Utils.getCurrentScope(),
        Utils.getCurrentChapterId(),
        Tables.dungeonConst.recoverApMoneyId
    )
    local originiumItemCfg = Tables.itemTable[Tables.dungeonConst.recoverApMoneyId]
    local name = ""
    local imgPath = ""
    if originiumItemCfg then
        name = originiumItemCfg.name
        imgPath = originiumItemCfg.iconId
    end



    local hasValue
    local usedExchangeCount
    hasValue, usedExchangeCount = GameInstance.player.globalVar:TryGetServerVar(GEnums.ServerGameVarEnum.RecoverApByMoneyCount)
    if not hasValue then
        usedExchangeCount = 0
    end



    local nextBuyCount = usedExchangeCount + 1
    local onceCostOriginiumNum = 1
    if Tables.originiumStaminaCost:ContainsKey(nextBuyCount) then

        onceCostOriginiumNum = Tables.originiumStaminaCost:GetValue(nextBuyCount)
    else

        local maxBuyCount = 0
        for buyCount, _ in pairs(Tables.originiumStaminaCost) do
            if buyCount > maxBuyCount then
                maxBuyCount = buyCount
            end
        end
        if maxBuyCount > 0 then
            onceCostOriginiumNum = Tables.originiumStaminaCost:GetValue(maxBuyCount)
        end
    end



    local maxExchangeCount = 0
    for buyCount, _ in pairs(Tables.originiumStaminaCost) do
        if buyCount > maxExchangeCount then
            maxExchangeCount = buyCount
        end
    end

    local remainExchangeCount = maxExchangeCount - usedExchangeCount

    self.m_totalExchangeStamina = recoverStaminaNum

    local viewNode = self.view.exchangeNode.costOriginiumNode
    viewNode.costNumTxt.text = onceCostOriginiumNum
    viewNode.recoverNumTxt.text = recoverStaminaNum
    if curOriginiumNum <= 0 then
        viewNode.totalNumTxt.text = curOriginiumNum
        viewNode.totalNumTxt.color = self.view.config.NUM_TEXT_COLOR_INSUFFICIENT
    else
        viewNode.totalNumTxt.text = curOriginiumNum
        viewNode.totalNumTxt.color = self.view.config.NUM_TEXT_COLOR_NORMAL_BLACK
    end
    viewNode.img:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, imgPath)
    viewNode.originiumNameTxt.text = name


    if maxExchangeCount <= 0 then

        if curOriginiumNum < onceCostOriginiumNum then
            self.view.buttonOperateState:SetState("InsufficientOriginiumState")
        else
            self.view.buttonOperateState:SetState("CostOriginiumState")
            self.view.costOriginiumTipTxt.text = string.format(
                Language.LUA_STAMINA_POPUP_EXCHANGE_COST_ORIGINIUM_TIP_NO_LIMIT,
                onceCostOriginiumNum,
                name,
                recoverStaminaNum
            )
        end
    else

        if curOriginiumNum < onceCostOriginiumNum then
            self.view.buttonOperateState:SetState("InsufficientOriginiumState")
        elseif remainExchangeCount <= 0 then
            self.view.buttonOperateState:SetState("InsufficientTimesState")
        else
            self.view.buttonOperateState:SetState("CostOriginiumState")

            self.view.costOriginiumTipTxt.text = string.format(
                Language.LUA_ORIGINIUM_EXCHANGE_STAMINA_TIP,
                remainExchangeCount,
                onceCostOriginiumNum,
                recoverStaminaNum
            )
        end
    end
    self:_RefreshUICurrentAndTargetStamina()


    local serializedHintKeyExchange = "ORIGINIUM_EXCHANGE_STAMINA_LAST_EXCHANGE_PRICE"

    local succ, lastExchangePrice = ClientDataManagerInst:GetInt(serializedHintKeyExchange, false, 1, "StaminaPopUp")


    if lastExchangePrice ~= onceCostOriginiumNum then

        if onceCostOriginiumNum > lastExchangePrice then
            self.view.exchangeNode.costOriginiumNode.costChangeAnim:Play("staminapopup_costoriginium_digitaljump")
        end

        ClientDataManagerInst:SetInt(serializedHintKeyExchange, onceCostOriginiumNum, false, "StaminaPopUp", EClientDataTimeValidType.Permanent)
    end
end

StaminaPopUpCtrl._RefreshUIQuickExchange = HL.Method() << function(self)
    local node = self.view.quickExchangeNode
    local info = self.m_quickExchangeItemInfo

    node.itemImg:LoadSprite(UIConst.UI_SPRITE_ITEM, info.imgPath)
    node.itemNameTxt.text = info.name
    node.descTxt.text = info.desc
    node.decoDescTxt.text = info.decoDesc
    node.itemStorage:InitStorageNode(info.count, 0, true)
    node.itemCountSelector:InitNumberSelector(info.selectCount, 1, info.count, function(curNum, _)
        self.m_quickExchangeItemInfo.selectCount = curNum
        self.m_totalExchangeStamina = self.m_quickExchangeItemInfo.recoverValue * curNum
        self:_RefreshUICurrentAndTargetStamina()
    end)

    self:_RefreshUICurrentAndTargetStamina()
end

StaminaPopUpCtrl._CalculateExchangeStaminaOfItemList = HL.Method() << function(self)
    self.m_totalExchangeStamina = 0
    if not self.m_invItemInfoList then
        return
    end

    for _, v in ipairs(self.m_invItemInfoList) do
        self.m_totalExchangeStamina = self.m_totalExchangeStamina + v.recoverValue * v.selectCount
    end
end

StaminaPopUpCtrl._RefreshUICurrentAndTargetStamina = HL.Method() << function(self)
    local cur = GameInstance.player.inventory.curStamina
    local target = self.m_totalExchangeStamina + cur
    local max = GameInstance.player.inventory.maxStamina
    self.view.curStaminaTxt.text = cur
    self.view.curMaxStaminaTxt.text = max
    self.view.targetMaxStaminaTxt.text = max
    if self.m_totalExchangeStamina <= 0 then
        self.view.targetStaminaTxt.text = target
        self.view.targetStaminaTxt.color = self.view.config.NUM_TEXT_COLOR_NORMAL
    else
        self.view.targetStaminaTxt.text = target
        self.view.targetStaminaTxt.color = self.view.config.NUM_TEXT_COLOR_CHANGE
    end
end

StaminaPopUpCtrl._TryStartTickRecover = HL.Method() << function(self)
    if self.m_coroutineRecover then
        return
    end
    if StaminaPopUpCtrl._IsStaminaMax() then
        self:_StopTickRecover()
        return
    end

    self.view.recoverTimeNode.gameObject:SetActive(true)
    self.view.fullRecoverTimeNode.gameObject:SetActive(true)
    self:_RefreshTickRecoverTxt()
    self.m_coroutineRecover = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.COMMON_UI_TIME_UPDATE_INTERVAL)
            self:_RefreshTickRecoverTxt()
            self:_RefreshUICurrentAndTargetStamina()
            if StaminaPopUpCtrl._IsStaminaMax() then
                self:_StopTickRecover()
            end
        end
    end)
end

StaminaPopUpCtrl._StopTickRecover = HL.Method() << function(self)
    self.view.recoverTimeNode.gameObject:SetActive(false)
    self.view.fullRecoverTimeNode.gameObject:SetActive(false)
    if self.m_coroutineRecover then
        self:_ClearCoroutine(self.m_coroutineRecover)
        self.m_coroutineRecover = nil
    end
end

StaminaPopUpCtrl._RefreshTickRecoverTxt = HL.Method() << function(self)
    local nextLeftTime = Utils.nextStaminaRecoverLeftTime()
    local fullLeftTime = Utils.fullStaminaRecoverLeftTime()
    self.view.nextRecoverTimeTxt.text = UIUtils.getLeftTimeToSecond(nextLeftTime)
    self.view.fullRecoverTimeTxt.text = UIUtils.getLeftTimeToSecond(fullLeftTime)
end

StaminaPopUpCtrl._IsStaminaMax = HL.StaticMethod().Return(HL.Boolean) << function()
    local cur = GameInstance.player.inventory.curStamina
    local max = GameInstance.player.inventory.maxStamina
    return cur >= max
end

StaminaPopUpCtrl._InitItemScrollController = HL.Method() << function(self)

    local scrollRect = self.view.exchangeNode.costItemScrollRect




    if scrollRect.naviGroup then
        scrollRect.naviGroup.onSetLayerSelectedTarget:RemoveAllListeners()
        scrollRect.naviGroup.onSetLayerSelectedTarget:AddListener(function(target)
            if target and target.transform and DeviceInfo.usingController then
                scrollRect:AutoScrollToRectTransform(target.transform, false)
            end
        end)
    end
end

StaminaPopUpCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    self.m_arg.exchangeState = self.m_exchangeState
    if self.m_quickExchangeItemInfo then
        self.m_arg.quickExchangeSelectCount = self.m_quickExchangeItemInfo.selectCount
    else
        if self.m_invItemInfoList then
            self.m_arg.selItemCountMap = {}
            self.m_arg.selInstItemCountMap = {}
            for i, itemInfo in pairs(self.m_invItemInfoList) do
                if itemInfo.isLTItem then
                    self.m_arg.selInstItemCountMap[itemInfo.instId] = itemInfo.selectCount
                else
                    self.m_arg.selItemCountMap[itemInfo.itemId] = itemInfo.selectCount
                end
            end
        end
    end
    return self.m_arg
end

HL.Commit(StaminaPopUpCtrl)
