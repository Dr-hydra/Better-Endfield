local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local AutoTriggerOnClick = CS.Beyond.Input.ActionOnSetNaviTarget.AutoTriggerOnClick

MoneyCell = HL.Class('MoneyCell', UIWidgetBase)

local TIPS_SHOW_PADDING_TOP = 100

MoneyCell.m_itemId = HL.Field(HL.String) << ""

MoneyCell.m_itemInstId = HL.Field(HL.Number) << 0

MoneyCell.m_isMoneyType = HL.Field(HL.Boolean) << false

MoneyCell.m_coroutine = HL.Field(HL.Thread)

MoneyCell.m_lastStamina = HL.Field(HL.Number) << 0

MoneyCell.m_controllerBindingId = HL.Field(HL.Number) << -1

MoneyCell.m_useItemIcon = HL.Field(HL.Boolean) << false

MoneyCell.m_needNumberLimit = HL.Field(HL.Boolean) << true

MoneyCell.m_customLimitNumber = HL.Field(HL.Number) << -1

MoneyCell.m_staminaCloseFun = HL.Field(HL.Function)

MoneyCell.m_staminaClickFun = HL.Field(HL.Function)

MoneyCell.m_rawPreferredWidth = HL.Field(HL.Number) << -1

MoneyCell.m_cellPreferredWidth = HL.Field(HL.Number) << -1

MoneyCell.m_staminaShowItemTips = HL.Field(HL.Boolean) << false

MoneyCell.m_isItemTipsShowing = HL.Field(HL.Boolean) << false


local HOT_AREA_ADJUST_POSITION_X = 10


MoneyCell._OnFirstTimeInit = HL.Override() << function(self)
    self:_RegisterMessages()
    local autoCloseArea = self.view.autoCloseArea
    autoCloseArea.tmpSafeArea = self.view.tip.transform
    autoCloseArea.onTriggerAutoClose:RemoveAllListeners()
    autoCloseArea.onTriggerAutoClose:AddListener(function()
        local active = self.view.tip.gameObject.activeSelf
        if active then
            self.view.tip.gameObject:SetActive(false)
        end
    end)

    self.view.button.onClick:AddListener(function()
        self:_OnClickItem()
    end)
    self.view.addBtn.onClick:AddListener(function()
        self:_OnClickAddItem()
    end)
    self.view.button.onIsNaviTargetChanged = function(isTarget, isGroupChanged, isOnNaviTargetEnabledAgain)
        local actionId = (isTarget and DeviceInfo.usingController) and "money_cell_add" or ""
        self.view.addBtn.onClick:ChangeBindingPlayerAction(actionId)
    end

    self.m_rawPreferredWidth = self.view.contentNode.preferredWidth
end

MoneyCell._OnDestroy = HL.Override() << function(self)
    self:_StopTick()
end

MoneyCell._OnEnable = HL.Override() << function(self)
    if self:IsStamina() then
        self:_StartTick()
    end
end

MoneyCell._OnDisable = HL.Override() << function(self)
    self:_StopTick()
    if self:IsStamina() then
        self.view.tip.gameObject:SetActive(false)
    end
end

MoneyCell.InitMoneyCell = HL.Method(HL.String, HL.Opt(HL.Boolean, HL.Boolean, HL.Boolean, HL.Number, HL.Number))
        << function(self, itemId, useAction, useItemIcon, needNumberLimit, limitNumber, cellPreferredWidth)
    self:_FirstTimeInit()

    self.m_itemId = itemId
    local itemData = Tables.itemTable:GetValue(self.m_itemId)
    self.m_isMoneyType = GameInstance.player.inventory:IsMoneyType(itemData.type)

    self.m_useItemIcon = useItemIcon == true
    self.m_needNumberLimit = needNumberLimit == true
    self.m_customLimitNumber = limitNumber or -1
    self.m_cellPreferredWidth = cellPreferredWidth or self.m_rawPreferredWidth
    self:_RefreshUI()
    if self:IsStamina() then
        self:_StartTick()
    end

    self:_ClearControllerBinding()
    if useAction then
        self.m_controllerBindingId = InputManagerInst:CreateBindingByActionId("inv_money_add", function()
            self:_OnClickAddItem()
        end, self.view.addBtn.groupId)
        self.view.keyHint.gameObject:SetActiveIfNecessary(useAction)
    else
        self.view.keyHint.gameObject:SetActiveIfNecessary(false)
    end
end


MoneyCell.SetStaminaCloseFun = HL.Method(HL.Function) << function(self, staminaCloseFun)
    self.m_staminaCloseFun = staminaCloseFun
end

MoneyCell.SetStaminaClickFun = HL.Method(HL.Function) << function(self, staminaClickFun)
    self.m_staminaClickFun = staminaClickFun
end

MoneyCell.SetStaminaShowItemTips = HL.Method(HL.Boolean) << function(self, staminaShowItemTips)
    self.m_staminaShowItemTips = staminaShowItemTips
end

MoneyCell.SetItemInstId = HL.Method(HL.Number) << function(self, instId)
    self.m_itemInstId = instId
end

MoneyCell.SetAddBtnKeyHintText = HL.Method(HL.String) << function(self, keyHintText)
    self.view.addBtn.customBindingViewLabelText = keyHintText
end

MoneyCell._RegisterMessages = HL.Method() << function(self)
    self:RegisterMessage(MessageConst.ON_STAMINA_CHANGED, function()
        self:_OnStaminaChanged()
    end)

    self:RegisterMessage(MessageConst.ON_WALLET_CHANGED, function(evtData)
        self:_OnWalletChanged(evtData)
    end)
    self:RegisterMessage(MessageConst.ON_ITEM_COUNT_CHANGED, function(evtData)
        if not self.m_isMoneyType then

            self:_OnItemCountChanged(evtData)
        end
    end)
end

MoneyCell._RefreshUI = HL.Method() << function(self)
    local itemData = Tables.itemTable:GetValue(self.m_itemId)
    self.view.icon:LoadSprite(self.m_useItemIcon and UIConst.UI_SPRITE_ITEM or UIConst.UI_SPRITE_WALLET, itemData.iconId)

    local showAddBtn = self:_ShouldShowAddButton()
    self.view.addBtn.gameObject:SetActive(showAddBtn)
    if showAddBtn then
        self.view.tip:InitStaminaTips()
        self:_StartCoroutine(function()

            coroutine.step()
            coroutine.step()
            self.view.hotArea.sizeDelta = Vector2( -(self.view.addBtn.transform.rect.width + HOT_AREA_ADJUST_POSITION_X), self.view.hotArea.sizeDelta.y)
        end)
    end
    if self.m_cellPreferredWidth > 0 then
        self.view.contentNode.preferredWidth = self.m_cellPreferredWidth
    end
    self:_UpdateCount()
end

MoneyCell._UpdateCount = HL.Method() << function(self)
    if self:IsStamina() then
        local curStamina = GameInstance.player.inventory.curStamina
        local maxStamina = GameInstance.player.inventory.maxStamina
        self.view.text.text = string.format(Language.LUA_FORWARD_SLASH, curStamina, maxStamina)
    elseif self.m_needNumberLimit then
        local numberLimit = nil
        if self.m_customLimitNumber > 0 then
            numberLimit = self.m_customLimitNumber
        else
            local succ, cfg = Tables.MoneyConfigTable:TryGetValue(self.m_itemId)
            if succ then
                numberLimit = cfg.MoneyClearLimit
            end
        end
        if numberLimit ~= nil then
            local itemCountStr = UIUtils.getNumString(GameInstance.player.inventory:GetItemCount(Utils.getCurrentScope(), Utils.getCurrentChapterId(), self.m_itemId))
            local numberLimitStr = UIUtils.getNumString(numberLimit)
            self.view.text.text = string.format("%s<color=#A7A7A7>/%s</color>", itemCountStr, numberLimitStr)
        else
            self.view.text.text = tonumber(GameInstance.player.inventory:GetItemCount(Utils.getCurrentScope(), Utils.getCurrentChapterId(), self.m_itemId))
        end
    else
        self.view.text.text = tonumber(GameInstance.player.inventory:GetItemCount(Utils.getCurrentScope(), Utils.getCurrentChapterId(), self.m_itemId))
    end
end

MoneyCell._ShouldShowAddButton = HL.Method().Return(HL.Boolean) << function(self)
    return self:IsStamina() or self:_IsDiamond() or self:_IsOriginium() or self:_IsWeaponGacha()
end

MoneyCell._OnClickItem = HL.Method() << function(self)
    if self:IsStamina() then
        if self.m_staminaShowItemTips then
            self:_OnClickItem2ShowItemTips()
        elseif DeviceInfo.usingController then
            PhaseManager:OpenPhase(PhaseId.StaminaPopUp, { closeFun = self.m_staminaCloseFun })
            if self.m_staminaClickFun then
                self.m_staminaClickFun()
            end
        else
            local curStamina = GameInstance.player.inventory.curStamina
            local maxStamina = GameInstance.player.inventory.maxStamina
            if curStamina >= maxStamina then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_STAMINA_FULL_HINT)
            else
                local active = self.view.tip.gameObject.activeSelf
                self.view.tip.gameObject:SetActive(not active)
            end
        end
        AudioAdapter.PostEvent("au_ui_btn_ap_info")
    else
        if not DeviceInfo.usingController and self.view.selected.gameObject.activeInHierarchy then
            Notify(MessageConst.HIDE_ITEM_TIPS)
            return
        end
        self:_OnClickItem2ShowItemTips()
    end
end

MoneyCell._OnClickItem2ShowItemTips = HL.Method() << function(self)
    if UIManager:IsShow(PanelId.RewardsPopUpForSystem) then


        return
    end
    if DeviceInfo.usingController and self.m_isItemTipsShowing then
        return
    end
    self.m_isItemTipsShowing = true
    local usingSideTips = DeviceInfo.usingController and self.view.button.actionOnSetNaviTarget == AutoTriggerOnClick
    Notify(MessageConst.SHOW_ITEM_TIPS, {
        transform = self.view.transform,
        safeArea = self.view.transform,
        posType = UIConst.UI_TIPS_POS_TYPE.MidBottom,
        itemId = self.m_itemId,
        instId = self.m_itemInstId,
        isSideTips = usingSideTips,
        keyHintGroupIds = { self.view.inputBindingGroupMonoTarget.groupId },
        padding = { top = TIPS_SHOW_PADDING_TOP },
        onClose = function()

            self.m_isItemTipsShowing = false
            if NotNull(self.view.selected) then
                self.view.selected.gameObject:SetActive(false)
            end
        end
    })
    self.view.selected.gameObject:SetActive(true)
end

MoneyCell._OnClickAddItem = HL.Method() << function(self)
    if self:IsStamina() then
        PhaseManager:OpenPhase(PhaseId.StaminaPopUp)
        if self.m_staminaClickFun then
            self.m_staminaClickFun()
        end
    elseif self:_IsDiamond() then
        PhaseManager:OpenPhase(PhaseId.CommonMoneyExchange, {
            sourceId = Tables.globalConst.originiumItemId,
            targetId = Tables.globalConst.diamondItemId,
            onClose = function()
                local ctrl = self:GetUICtrl()
                if DeviceInfo.usingController and not ctrl.m_isClosed and ctrl.view.contentNaviGroup then
                    ctrl.view.contentNaviGroup:ManuallyFocus()
                end
            end,})
    elseif self:_IsWeaponGacha() then
        PhaseManager:OpenPhase(PhaseId.CommonMoneyExchange, {
            sourceId = Tables.globalConst.originiumItemId,
            targetId = Tables.globalConst.gachaWeaponItemId,
            onClose = function()
                if DeviceInfo.usingController then
                    local naviGroup = self:GetUICtrl().view.contentNaviGroup
                    if naviGroup then
                        naviGroup:ManuallyFocus()
                    end
                end
            end,})
    elseif self:_IsOriginium() then
        CashShopUtils.GotoCashShopRechargeTab()
    end
    Notify(MessageConst.HIDE_ITEM_TIPS)
end

MoneyCell._OnStaminaChanged = HL.Method() << function(self)
    if self:IsStamina() then
        self:_UpdateCount()
        local curStamina = GameInstance.player.inventory.curStamina
        local maxStamina = GameInstance.player.inventory.maxStamina
        if curStamina >= maxStamina then
            self.view.tip.gameObject:SetActiveIfNecessary(false)
        end
    end
end

MoneyCell._OnWalletChanged = HL.Method(HL.Table) << function(self, args)
    if self:IsStamina() then
        return
    end

    local id, amount, opAmount = unpack(args)
    if id == self.m_itemId then
        self:_UpdateCount()
    end
end

MoneyCell._OnItemCountChanged = HL.Method(HL.Table) << function(self, arg)
    if string.isEmpty(self.m_itemId) then
        return
    end
    local itemId2DiffCount = unpack(arg)
    if itemId2DiffCount:ContainsKey(self.m_itemId) then
        self:_UpdateCount()
    end
end

MoneyCell.IsStamina = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_itemId == Tables.globalConst.apItemId
end

MoneyCell._IsDiamond = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_itemId == Tables.globalConst.diamondItemId
end

MoneyCell._IsWeaponGacha = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_itemId == Tables.globalConst.gachaWeaponItemId
end

MoneyCell._IsOriginium = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_itemId == Tables.globalConst.originiumItemId and not UIManager:IsShow(PanelId.ShopRecharge)
end

MoneyCell.IsOriginium = HL.Method().Return(HL.Boolean) << function(self)
    return self:_IsOriginium()
end

MoneyCell._StartTick = HL.Method() << function(self)
    if self.m_coroutine then
        self.m_coroutine = self:_ClearCoroutine(self.m_coroutine)
    end
    self:_UpdateCount()
    self.m_lastStamina = GameInstance.player.inventory.curStamina
    self.m_coroutine = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.COMMON_UI_TIME_UPDATE_INTERVAL)
            self:_UpdateStamina()
        end
    end)
end

MoneyCell._StopTick = HL.Method() << function(self)
    if self.m_coroutine then
        self.m_coroutine = self:_ClearCoroutine(self.m_coroutine)
    end
end

MoneyCell._UpdateStamina = HL.Method() << function(self)
    local curStamina = GameInstance.player.inventory.curStamina
    if curStamina ~= self.m_lastStamina then
        self:_UpdateCount()
        self.m_lastStamina = curStamina
    end
end

MoneyCell._ClearControllerBinding = HL.Method() << function(self)
    if self.m_controllerBindingId == -1 then
        return
    end

    InputManagerInst:DeleteBinding(self.m_controllerBindingId)
    self.m_controllerBindingId = -1
end

HL.Commit(MoneyCell)
return MoneyCell
