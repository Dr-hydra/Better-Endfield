
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ShopTokenExchangePopUp

ShopTokenExchangePopUpCtrl = HL.Class('ShopTokenExchangePopUpCtrl', uiCtrl.UICtrl)

ShopTokenExchangePopUpCtrl.m_redundantItemInfo = HL.Field(HL.Table)

ShopTokenExchangePopUpCtrl.m_exchangeDatas = HL.Field(HL.Table)

ShopTokenExchangePopUpCtrl.m_getCellFunc = HL.Field(HL.Function)

ShopTokenExchangePopUpCtrl.m_exchangeCells = HL.Field(HL.Any)


ShopTokenExchangePopUpCtrl.m_currLeftNaviIndex = HL.Field(HL.Number) << 1


ShopTokenExchangePopUpCtrl.m_currRightNaviIndex = HL.Field(HL.Number) << 1


ShopTokenExchangePopUpCtrl.m_currNaviIsLeft = HL.Field(HL.Boolean) << false


ShopTokenExchangePopUpCtrl.m_currNaviIsRight = HL.Field(HL.Boolean) << false

ShopTokenExchangePopUpCtrl.m_showItems = HL.Field(HL.Table)

ShopTokenExchangePopUpCtrl.m_focusGroupId = HL.Field(HL.Number) << 0





ShopTokenExchangePopUpCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_SC_SHOP_SWAP_CHAR_POTENTIAL_UP] = '_OnReceiveServer',
    [MessageConst.ON_CASH_SHOP_OPEN_CATEGORY] = '_OnCashShopOpenCategory',
    [MessageConst.ON_SHOP_SHOW_REWARD] = '_OnReceiveReward',
}


ShopTokenExchangePopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeButton.onClick:AddListener(function()
        self:PlayAnimationOut()
    end)

    self.view.maskBg.onClick:AddListener(function()
        self:PlayAnimationOut()
    end)

    self.view.btnCommon.onClick:AddListener(function()
        self:PlayAnimationOut()
    end)

    self.view.btnCommonYellow.onClick:AddListener(function()
        self:_OnClickBtnConfirm()
    end)

    self.m_showItems = {}

    self:_InitShortCut()

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, index)
        local cell = self.m_getCellFunc(obj)
        local itemInfo = self.m_redundantItemInfo[LuaIndex(index)]
        cell:InitItem({ id = itemInfo.itemId, count = itemInfo.count }, true)
        cell:SetExtraInfo({
            isSideTips = DeviceInfo.usingController,
        })
    end)
    if DeviceInfo.usingController then
        self.view.scrollListSelectableNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
            self.m_currNaviIsLeft = isFocused
            if not isFocused then
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
        end)
    end


    self.m_exchangeCells = UIUtils.genCellCache(self.view.exchangeNode)

    self.m_redundantItemInfo = arg.redundantItemInfo
    self:_InitExchangeData()
    self:_RefreshUI()
end

ShopTokenExchangePopUpCtrl.OnShow = HL.Override() << function(self)
    if not DeviceInfo.usingController then
        return
    end


    self.m_currNaviIsLeft = false
    self.m_currNaviIsRight = false
    InputManagerInst:ToggleGroup(self.m_focusGroupId, true)
    InputManagerInst:ToggleGroup(self.view.rightNodeInputBindingGroupMonoTarget.groupId, false)
    InputManagerInst:ToggleGroup(self.view.buttonLayoutMonoTarget.groupId, true)
end









ShopTokenExchangePopUpCtrl._InitData = HL.Method() << function(self)
    self.m_redundantItemInfo = {}

    local charList = GameInstance.player.charBag.charList
    for _, charInfo in pairs(charList) do
        local charInstId = charInfo.instId
        local templateId = charInfo.templateId
        local currentPotentialLevel = charInfo.potentialLevel
        local succ, characterPotentialList = Tables.characterPotentialTable:TryGetValue(templateId)

        local maxPotentialLevel = characterPotentialList.potentialUnlockBundle.Count;

        local unlockData = characterPotentialList.potentialUnlockBundle[0]
        local materialId = unlockData.itemIds[0]
        local getCount = Utils.getItemCount(materialId)

        local redundant = currentPotentialLevel + getCount - maxPotentialLevel
        if redundant > 0 then
            logger.info(string.format("%s已满潜,itemID:%s,多出来%s个",
                templateId, materialId, redundant))
            local getItemDataSucc, itemData = Tables.itemTable:TryGetValue(materialId)
            if getItemDataSucc then
                table.insert(self.m_redundantItemInfo, {
                    itemId = materialId,
                    count = redundant,
                    rarity = itemData.rarity,
                    itemData = itemData,
                })
            else
                logger.error("缺少数据 " .. materialId .. " 注意拉新。")
            end
        end
    end
end

ShopTokenExchangePopUpCtrl._InitExchangeData = HL.Method() << function(self)
    local rewardItemDict = {}
    self.m_exchangeDatas = {}
    for _, itemInfo in ipairs(self.m_redundantItemInfo) do
        local rarity = itemInfo.rarity
        local rewardId = nil
        if rarity == 4 then
            rewardId = Tables.CashShopConst.star4PotentialupSwapRewardId
        end
        if rarity == 5 then
            rewardId = Tables.CashShopConst.star5PotentialupSwapRewardId
        end
        if rarity == 6 then
            rewardId = Tables.CashShopConst.star6PotentialupSwapRewardId
        end

        local succ, rewardsCfg = Tables.rewardTable:TryGetValue(rewardId)
        if succ then
            for _, itemBundle in pairs(rewardsCfg.itemBundles) do
                local itemId = itemBundle.id
                local count = itemBundle.count * itemInfo.count
                if rewardItemDict[itemId] == nil then
                    rewardItemDict[itemId] = {
                        count = count,
                        rarity = rarity,
                    }
                else
                    rewardItemDict[itemId].count = rewardItemDict[itemId].count + count
                end
            end
        end
    end
    for itemId, rewardItem in pairs(rewardItemDict) do
        table.insert(self.m_exchangeDatas, {
            itemId = itemId,
            count = rewardItem.count,
            rarity = rewardItem.rarity,
        })
    end
    table.sort(self.m_exchangeDatas, Utils.genSortFunction({ "rarity" }, true))
end

ShopTokenExchangePopUpCtrl._RefreshUI = HL.Method() << function(self)
    self.view.scrollList:UpdateCount(#self.m_redundantItemInfo)
    self.m_exchangeCells:Refresh(#self.m_exchangeDatas, function(cell, index)
        local exchangeData = self.m_exchangeDatas[index]
        local itemData = Tables.itemTable[exchangeData.itemId]
        cell.iconImg:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemData.iconId)
        local prevText = cell.costNumTxt
        local afterText = cell.exchangeNumTxt
        local currHave = Utils.getItemCount(exchangeData.itemId)
        prevText.text = currHave
        afterText.text = currHave + exchangeData.count
        cell.tipsBtn.onClick:AddListener(function()
            Notify(MessageConst.SHOW_ITEM_TIPS, {
                transform = cell.transform,
                posType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
                itemId = exchangeData.itemId,
                isSideTips = DeviceInfo.usingController,
            })
        end)
        cell.keyHint.gameObject:SetActive(index == 1)
    end)
end

ShopTokenExchangePopUpCtrl._OnClickBtnConfirm = HL.Method() << function(self)
    local arg1 = {}
    local arg2 = {}
    for _, itemInfo in ipairs(self.m_redundantItemInfo) do
        table.insert(arg1, itemInfo.itemId)
        table.insert(arg2, itemInfo.count)
    end
    GameInstance.player.cashShopSystem:SendPotentialMaterialExchange(arg1, arg2)
end

ShopTokenExchangePopUpCtrl._OnReceiveServer = HL.Method(HL.Table) << function(self, args)
    logger.info("ShopTokenExchangePopUpCtrl._OnReceiveServer 显示reward")

    table.sort(self.m_showItems, Utils.genSortFunction({"rarity", "type", "id"}, false))

    local rewardPanelArgs = {}
    rewardPanelArgs.items = self.m_showItems
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, rewardPanelArgs)

    self:Close()
end

ShopTokenExchangePopUpCtrl._OnReceiveReward = HL.Method(HL.Any) << function(self, args)
    logger.info("ShopTokenExchangePopUpCtrl._OnReceiveReward: 暂存reward")


    local itemBundleList = unpack(args)

    for _, itemBundle in pairs(itemBundleList) do
        local _, itemData = Tables.itemTable:TryGetValue(itemBundle.id)
        if itemData then
            local putInside = false
            for i = 1, #self.m_showItems do
                if self.m_showItems[i].id == itemData.id and itemBundle.instId == 0 then
                    self.m_showItems[i].count = self.m_showItems[i].count + itemBundle.count
                    putInside = true
                    break
                end
            end

            if not putInside then
                table.insert(self.m_showItems, {id = itemBundle.id,
                                     count = itemBundle.count,
                                     instData = itemBundle.instData,
                                     instId = itemBundle.instId,
                                     rarity = itemData.rarity,
                                     type = itemData.type:ToInt()})
            end
        end
    end
end

ShopTokenExchangePopUpCtrl._OnCashShopOpenCategory = HL.Method() << function(self)
    self:Close()
end

ShopTokenExchangePopUpCtrl._InitShortCut = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    local focusGroupId = InputManagerInst:CreateGroup(self.view.inputGroup.groupId)
    self.m_focusGroupId = focusGroupId

    InputManagerInst:ToggleGroup(self.m_focusGroupId, true)
    InputManagerInst:ToggleGroup(self.view.rightNodeInputBindingGroupMonoTarget.groupId, false)
    InputManagerInst:ToggleGroup(self.view.buttonLayoutMonoTarget.groupId, true)


    self:BindInputPlayerAction("cashshop_token_exchange_focus_left", function()
        logger.info("ShopTokenExchangePopUpCtrl:cashshop_token_exchange_focus_left")
        InputManagerInst:ToggleGroup(focusGroupId, false)
        InputManagerInst:ToggleGroup(self.view.rightNodeInputBindingGroupMonoTarget.groupId, true)
        InputManagerInst:ToggleGroup(self.view.buttonLayoutMonoTarget.groupId, false)
        self.m_currNaviIsLeft = true
        self.m_currNaviIsRight = false
        self:_LeftNaviAddCol(0)
    end, focusGroupId)


    self:BindInputPlayerAction("cashshop_token_exchange_focus_right", function()
        logger.info("ShopTokenExchangePopUpCtrl:cashshop_token_exchange_focus_right")
        InputManagerInst:ToggleGroup(focusGroupId, false)
        InputManagerInst:ToggleGroup(self.view.rightNodeInputBindingGroupMonoTarget.groupId, true)
        InputManagerInst:ToggleGroup(self.view.buttonLayoutMonoTarget.groupId, false)
        self.m_currNaviIsLeft = false
        self.m_currNaviIsRight = true
        self.m_currRightNaviIndex = 1
        self:_RightNaviAddValue(0)
    end, focusGroupId)

    self:BindInputPlayerAction("common_cancel_no_hint", function()
        self.m_currNaviIsLeft = false
        self.m_currNaviIsRight = false
        Notify(MessageConst.HIDE_ITEM_TIPS)
        self.view.scrollListSelectableNaviGroup:ManuallyStopFocus()
        self.view.rightNode:ManuallyStopFocus()
        InputManagerInst:ToggleGroup(self.view.rightNodeInputBindingGroupMonoTarget.groupId, false)
        InputManagerInst:ToggleGroup(focusGroupId, true)
        InputManagerInst:ToggleGroup(self.view.buttonLayoutMonoTarget.groupId, true)
    end, self.view.rightNodeInputBindingGroupMonoTarget.groupId)

    self:BindInputPlayerAction("cashshop_navigation_4_dir_left", function()
        self:_OnGoLeft()
    end, self.view.rightNodeInputBindingGroupMonoTarget.groupId)

    self:BindInputPlayerAction("cashshop_navigation_4_dir_up", function()
        self:_OnGoUp()
    end, self.view.rightNodeInputBindingGroupMonoTarget.groupId)

    self:BindInputPlayerAction("cashshop_navigation_4_dir_right", function()
        self:_OnGoRight()
    end, self.view.rightNodeInputBindingGroupMonoTarget.groupId)

    self:BindInputPlayerAction("cashshop_navigation_4_dir_down", function()
        self:_OnGoDown()
    end, self.view.rightNodeInputBindingGroupMonoTarget.groupId)
end

ShopTokenExchangePopUpCtrl._OnGoLeft = HL.Method() << function(self)
    logger.info("ShopTokenExchangePopUpCtrl._OnGoLeft")

    if self.m_currNaviIsLeft then
        self:_LeftNaviAddCol(-1)
    elseif self.m_currNaviIsRight then
        self.m_currNaviIsLeft = true
        self.m_currNaviIsRight = false
        Notify(MessageConst.HIDE_ITEM_TIPS)
        self:_LeftNaviAddCol(0)
    end
end

ShopTokenExchangePopUpCtrl._OnGoUp = HL.Method() << function(self)
    logger.info("ShopTokenExchangePopUpCtrl._OnGoUp")

    if self.m_currNaviIsLeft then
        self:_LeftNaviAddRow(-1)
    elseif self.m_currNaviIsRight then
        self:_RightNaviAddValue(-1)
    end
end

ShopTokenExchangePopUpCtrl._OnGoRight = HL.Method() << function(self)
    logger.info("ShopTokenExchangePopUpCtrl._OnGoRight")

    if self.m_currNaviIsLeft then
        local lineCount = self.view.scrollList.countPerLine
        if self.m_currLeftNaviIndex % lineCount == 0 or
            self.m_currLeftNaviIndex == #self.m_redundantItemInfo then
            self.m_currNaviIsLeft = false
            self.m_currNaviIsRight = true
            Notify(MessageConst.HIDE_ITEM_TIPS)
            self:_RightNaviAddValue(0)
        else
            self:_LeftNaviAddCol(1)
        end
    elseif self.m_currNaviIsRight then

    end
end

ShopTokenExchangePopUpCtrl._OnGoDown = HL.Method() << function(self)
    logger.info("ShopTokenExchangePopUpCtrl._OnGoDown")

    if self.m_currNaviIsLeft then
        self:_LeftNaviAddRow(1)
    elseif self.m_currNaviIsRight then
        self:_RightNaviAddValue(1)
    end
end

ShopTokenExchangePopUpCtrl._LeftNaviAddCol = HL.Method(HL.Number) << function(self, value)
    local curr = self.m_currLeftNaviIndex
    local new = curr + value
    if new <= 0 or new > #self.m_redundantItemInfo then
        return
    end

    local targetCell = self.m_getCellFunc(self.view.scrollList:Get(CSIndex(new)))
    self:SetNaviTarget(targetCell.view.button)

    self.m_currLeftNaviIndex = new
end

ShopTokenExchangePopUpCtrl._LeftNaviAddRow = HL.Method(HL.Number) << function(self, value)
    local curr = self.m_currLeftNaviIndex
    local lineCount = self.view.scrollList.countPerLine
    local new = curr + (value * lineCount)
    if new <= 0 or new > #self.m_redundantItemInfo then
        return
    end

    local targetCell = self.m_getCellFunc(self.view.scrollList:Get(CSIndex(new)))
    self:SetNaviTarget(targetCell.view.button)

    self.m_currLeftNaviIndex = new
end

ShopTokenExchangePopUpCtrl._RightNaviAddValue = HL.Method(HL.Number) << function(self, value)
    local curr = self.m_currRightNaviIndex
    local new = curr + value
    if new <= 0 or new > #self.m_exchangeDatas then
        return
    end

    InputManagerInst:ToggleGroup(self.view.rightNodeInputBindingGroupMonoTarget.groupId, true)
    local targetCell = self.m_getCellFunc(self.m_exchangeCells:Get(new))
    self:SetNaviTarget(targetCell.inputBindingGroupNaviDecorator)

    self.m_currRightNaviIndex = new

    local cell = self.m_exchangeCells:Get(new)
    local exchangeData = self.m_exchangeDatas[new]
    Notify(MessageConst.SHOW_ITEM_TIPS, {
        transform = cell.transform,
        posType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
        itemId = exchangeData.itemId,
        isSideTips = true,
    })
end

HL.Commit(ShopTokenExchangePopUpCtrl)
