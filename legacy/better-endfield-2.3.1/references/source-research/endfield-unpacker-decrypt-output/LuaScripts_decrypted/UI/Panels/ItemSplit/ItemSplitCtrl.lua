
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ItemSplit
ItemSplitCtrl = HL.Class('ItemSplitCtrl', uiCtrl.UICtrl)







ItemSplitCtrl.s_messages = HL.StaticField(HL.Table) << {

}

ItemSplitCtrl.m_args = HL.Field(HL.Table)

ItemSplitCtrl.m_slotIndex = HL.Field(HL.Number) << -1

ItemSplitCtrl.m_itemId = HL.Field(HL.String) << ''

ItemSplitCtrl.m_count = HL.Field(HL.Number) << 1

ItemSplitCtrl.m_curCount = HL.Field(HL.Number) << 1

ItemSplitCtrl.m_onComplete = HL.Field(HL.Function)

ItemSplitCtrl.m_addBtnPressCoroutine = HL.Field(HL.Thread)

ItemSplitCtrl.m_reduceBtnPressCoroutine = HL.Field(HL.Thread)


ItemSplitCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.view.btnCancel.onClick:AddListener(function()
        self:PlayAnimationOut(UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Close)
    end)
    self.view.mask.onClick:AddListener(function()
        self:PlayAnimationOut(UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Close)
    end)
    self.view.btnConfirm.onClick:AddListener(function()
        self:_OnClickConfirm()
    end)


    self.view.addBtn.onPressStart:AddListener(function()
        if InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.Mouse1) then
            return
        end

        self:_ChangeNum(self.m_curCount + 1, true)
        if not self.view.addBtn.interactable then
            return
        end

        self.m_addBtnPressCoroutine = self:_ClearCoroutine(self.m_addBtnPressCoroutine)
        self.m_addBtnPressCoroutine = self:_StartCoroutine(function()
            while true do
                coroutine.wait(UIConst.NUMBER_SELECTOR_COUNT_REFRESH_INTERVAL)
                local nextNumber = math.min((
                    math.floor(self.m_curCount / UIConst.NUMBER_SELECTOR_COUNT_REFRESH_AMOUNT) + 1) * UIConst.NUMBER_SELECTOR_COUNT_REFRESH_AMOUNT,
                    self.m_count)
                self:_ChangeNum(nextNumber, true)
            end
        end)
    end)
    self.view.addBtn.onPressEnd:AddListener(function()
        self.m_addBtnPressCoroutine = self:_ClearCoroutine(self.m_addBtnPressCoroutine)
    end)
    self.view.reduceBtn.onPressStart:AddListener(function()
        if InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.Mouse1) then
            return
        end

        self:_ChangeNum(self.m_curCount - 1, true)
        if not self.view.reduceBtn.interactable then
            return
        end

        self.m_reduceBtnPressCoroutine = self:_ClearCoroutine(self.m_reduceBtnPressCoroutine)
        self.m_reduceBtnPressCoroutine = self:_StartCoroutine(function()
            while true do
                coroutine.wait(UIConst.NUMBER_SELECTOR_COUNT_REFRESH_INTERVAL)
                local nextNumber = math.max((
                    math.ceil(self.m_curCount / UIConst.NUMBER_SELECTOR_COUNT_REFRESH_AMOUNT) - 1) * UIConst.NUMBER_SELECTOR_COUNT_REFRESH_AMOUNT,
                    1)
                self:_ChangeNum(nextNumber, true)
            end
        end)
    end)
    self.view.reduceBtn.onPressEnd:AddListener(function()
        self.m_reduceBtnPressCoroutine = self:_ClearCoroutine(self.m_reduceBtnPressCoroutine)
    end)


    self.view.halfButton.onClick:AddListener(function()
        self:_ChangeNum(math.floor(self.m_count / 2), true)
    end)


    self.view.numSlider.minValue = 1
    self.view.numSlider.onValueChanged:AddListener(function(newNum)
        self:_OnNumChanged(newNum)
    end)

    local slotIndex = args.slotIndex
    self.m_onComplete = args.onComplete
    self.m_slotIndex = slotIndex
    local bundle = GameInstance.player.inventory.itemBag:GetOrFallback(Utils.getCurrentScope()).slots[slotIndex]
    self.m_itemId = bundle.id
    self.m_count = bundle.count
    self.m_curCount = 1
    self.view.numSlider.maxValue = math.max(1, self.m_count - 1)
    self:_ChangeNum(self.m_curCount, true)
    self:_OnNumChanged(self.m_curCount)


    local fullBottleSuccess, fullBottleData = Tables.fullBottleTable:TryGetValue(self.m_itemId)
    if fullBottleSuccess then
        local liquidSuccess, liquidData = Tables.itemTable:TryGetValue(fullBottleData.liquidId)
        if liquidSuccess then
            self.view.liquidIcon.gameObject:SetActive(true)
            self.view.liquidIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, liquidData.iconId)
        else
            self.view.liquidIcon.gameObject:SetActive(false)
        end
    else
        self.view.liquidIcon.gameObject:SetActive(false)
    end

    UIUtils.displayItemBasicInfos(self.view, self.loader, bundle.id)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

ItemSplitCtrl.OnClose = HL.Override() << function(self)
    if self.m_onComplete then
        self.m_onComplete()
    end
end

ItemSplitCtrl._ChangeNum = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, num, isFinalNum)
    local newNum = isFinalNum and num or (self.m_curCount + num)
    self.view.numSlider.value = newNum
end

ItemSplitCtrl._OnNumChanged = HL.Method(HL.Number) << function(self, num)
    self.view.addBtn.interactable = num < self.m_count - 1

    self.view.reduceBtn.interactable = num > 1
    self.m_curCount = num
    self.view.splitNumTxt.text = math.floor(num)
    self.view.leftNumTxt.text = math.floor(self.m_count - num)

    if not self.view.addBtn.interactable then
        self.m_addBtnPressCoroutine = self:_ClearCoroutine(self.m_addBtnPressCoroutine)
    end
    if not self.view.reduceBtn.interactable then
        self.m_reduceBtnPressCoroutine = self:_ClearCoroutine(self.m_reduceBtnPressCoroutine)
    end
end

ItemSplitCtrl._OnClickConfirm = HL.Method() << function(self)
    local toSlot = GameInstance.player.inventory.itemBag:GetOrFallback(Utils.getCurrentScope()):GetFirstEmptySlotIndex()
    if toSlot < 0 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ITEM_TIPS_TOAST_1)
        return
    end

    GameInstance.player.inventory:SplitInItemBag(Utils.getCurrentScope(), self.m_slotIndex, toSlot, self.m_curCount)
    self:PlayAnimationOut(UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Close)
end

HL.Commit(ItemSplitCtrl)
