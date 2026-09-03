local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonHoverTip
CommonHoverTipCtrl = HL.Class('CommonHoverTipCtrl', uiCtrl.UICtrl)

CommonHoverTipCtrl.m_coroutine = HL.Field(HL.Thread)

CommonHoverTipCtrl.m_delayCoroutine = HL.Field(HL.Thread)

CommonHoverTipCtrl.m_isShown = HL.Field(HL.Boolean) << false






CommonHoverTipCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.HIDE_COMMON_HOVER_TIP] = '_OnHideTip',
}


CommonHoverTipCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.tipContent.gameObject:SetActive(false)
end
















CommonHoverTipCtrl._OnShowTip = HL.StaticMethod(HL.Table) << function(args)
    if DeviceInfo.usingTouch then
        return
    end

    local self = UIManager:AutoOpen(PANEL_ID)
    if self.m_delayCoroutine ~= nil then
        self:_ClearCoroutine(self.m_delayCoroutine)
        self.m_delayCoroutine = nil
    end

    if type(args.delay) == "number" and args.delay > 0 then
        self.m_delayCoroutine = self:_StartCoroutine(function()
            coroutine.wait(args.delay)
            self.m_delayCoroutine = nil
            self:_ShowTip(args)
        end)
        return
    end

    self:_ShowTip(args)
end

CommonHoverTipCtrl._ShowTip = HL.Method(HL.Table) << function(self, args)
    self.m_isShown = true

    self.view.tipContent:ClearTween(false)

    self.view.tipContent:PlayInAnimation()
    self.view.tipContent.gameObject:SetActive(true)

    local mainText = args.mainText
    local subText = args.subText

    local itemId = args.itemId or ""
    if not string.isEmpty(itemId) then

        local itemData = Tables.itemTable[itemId]

        local itemTypeData = Tables.itemTypeTable[itemData.type]
        mainText = itemData.name
        subText = UIUtils.getItemTypeName(itemId)

        UIUtils.setItemRarityImage(self.view.rarityLine, itemData.rarity)

        self.view.rarityLine.gameObject:SetActive(true)
    elseif args.rarity ~= nil then
        UIUtils.setItemRarityImage(self.view.rarityLine, args.rarity)
        self.view.rarityLine.gameObject:SetActive(true)
    else
        self.view.rarityLine.gameObject:SetActive(false)
    end

    self.view.mainText.text = mainText
    if subText ~= nil then
        self.view.subText.gameObject:SetActive(true)
        self.view.subText.text = subText
    else
        self.view.subText.gameObject:SetActive(false)
    end

    self:_RefreshLiquidInfo(itemId)
    self:_RefreshGasInfo(itemId)

    if self.m_coroutine ~= nil then
        self.m_coroutine = self:_ClearCoroutine(self.m_coroutine)
    end

    local posType = args.posType or UIConst.UI_TIPS_POS_TYPE.RightTop
    if InputManager.cursorVisible then
        self.m_coroutine = self:_StartCoroutine(function()
            while true do
                if InputManager.cursorVisible then
                    local mousePos = InputManager.mousePosition
                    local xRate = self.view.rectTransform.rect.width / Screen.width
                    local yRate = self.view.rectTransform.rect.height / Screen.height
                    local xOffset = CS.Beyond.UI.UIUtils.GetCursorTipOffsetX(self.view.config.DEFAULT_OFFSET_X)
                    self.view.cursorRect.anchoredPosition = Vector2(mousePos.x * xRate + xOffset, mousePos.y * yRate)
                    UIUtils.updateTipsPosition(self.view.tipContent.transform, self.view.cursorRect, self.view.rectTransform, self.uiCamera, posType, args.padding)
                end
                coroutine.step()
            end
        end)
    else
        self.m_coroutine = self:_StartCoroutine(function()
            while true do
                UIUtils.updateTipsPosition(self.view.tipContent.transform, args.targetRect, self.view.rectTransform, self.uiCamera, posType, args.padding)
                coroutine.step()
            end
        end)
    end
    self:ToTop()
end

CommonHoverTipCtrl._OnHideTip = HL.Method(HL.Opt(HL.Table)) << function(self, args)
    if self.m_delayCoroutine ~= nil then
        self:_ClearCoroutine(self.m_delayCoroutine)
        self.m_delayCoroutine = nil
    end

    if not self.m_isShown then
        return
    end
    self.m_isShown = false

    local noAnimation = args ~= nil and args.noAnimation == true
    if noAnimation then
        self:_HideTipContent()
    else
        self.view.tipContent:PlayOutAnimation(function()
            self:_HideTipContent()
        end)
    end
end

CommonHoverTipCtrl._HideTipContent = HL.Method() << function(self)
    self.view.tipContent.gameObject:SetActive(false)
    if self.m_coroutine ~= nil then
        self.m_coroutine = self:_ClearCoroutine(self.m_coroutine)
    end
end




CommonHoverTipCtrl._RefreshLiquidInfo = HL.Method(HL.String) << function(self, itemId)
    self.view.liquidInfo.gameObject:SetActive(false)

    if string.isEmpty(itemId) then
        return
    end

    local isFullBottle = FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
    if not isFullBottle then
        return
    end

    local fullBottleData = Tables.fullBottleTable[itemId]
    local liquidItemId = fullBottleData.liquidId
    local liquidSuccess, liquidItemData = Tables.itemTable:TryGetValue(liquidItemId)
    if not liquidSuccess then
        return
    end

    self.view.liquidInfo.liquidInfoTxt.text = string.format(Language["LUA_ITEM_HOVER_TIP_LIQUID_INFO"], liquidItemData.name)
    self.view.liquidInfo.gameObject:SetActive(true)
end



CommonHoverTipCtrl._RefreshGasInfo = HL.Method(HL.String) << function(self, itemId)
    if string.isEmpty(itemId) then
        return
    end

    local isFullBottle = FactoryUtils.isFullBottleOrJarItem(itemId, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)
    if not isFullBottle then
        return
    end

    local fullBottleData = Tables.fullGasJarTable[itemId]
    local gasItemId = fullBottleData.gasId
    local gasSuccess, gasItemData = Tables.itemTable:TryGetValue(gasItemId)
    if not gasSuccess then
        return
    end

    self.view.liquidInfo.liquidInfoTxt.text = string.format(Language["LUA_ITEM_HOVER_TIP_LIQUID_INFO"], gasItemData.name)
    self.view.liquidInfo.gameObject:SetActive(true)
end

HL.Commit(CommonHoverTipCtrl)
