
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.RewardsPopUpForSystem
RewardsPopUpForSystemCtrl = HL.Class('RewardsPopUpForSystemCtrl', uiCtrl.UICtrl)





RewardsPopUpForSystemCtrl.s_messages = HL.StaticField(HL.Table) << {
}


RewardsPopUpForSystemCtrl.m_args = HL.Field(HL.Table)

RewardsPopUpForSystemCtrl.m_items = HL.Field(HL.Table)

RewardsPopUpForSystemCtrl.m_extraItemInfos = HL.Field(HL.Table)

RewardsPopUpForSystemCtrl.m_extraItemCellCache = HL.Field(HL.Forward("UIListCache"))

RewardsPopUpForSystemCtrl.m_extraItemTitleCellCache = HL.Field(HL.Forward("UIListCache"))



RewardsPopUpForSystemCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeBtn.onClick:AddListener(function()
        if UIManager:IsShow(PanelId.ItemTips) then
            return
        end
        self:_OnClickClose()
    end)
    self.view.fullMask.onClick:AddListener(function()
        if UIManager:IsShow(PanelId.ItemTips) then
            return
        end
        self:_OnClickClose()
    end)
    self.view.skipBtn.onClick:AddListener(function()
        self:_OnClickSkip()
    end)

    if self.view.extraRewardNode then
        self.m_extraItemCellCache = UIUtils.genCellCache(self.view.extraRewardNode.itemCell)
        self.m_extraItemTitleCellCache = UIUtils.genCellCache(self.view.extraRewardNode.titleTxtCell)
    end

    local getItemCells = UIUtils.genCachedCellFunction(self.view.rewardsScrollList)
    self.view.rewardsScrollList.onUpdateCell:AddListener(function(object, csIndex)
        local cell = getItemCells(object)
        self:_OnUpdateCell(cell, LuaIndex(csIndex))
    end)
    self.view.rewardsScrollList.onGraduallyShowFinish:AddListener(function()
        self:_OnGraduallyShowFinish()
    end)

    self.view.naviGroup.getDefaultSelectableFunc = function()
        local cell = getItemCells(1)
        return cell and cell.view.button or nil
    end
    self.view.naviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})



    if NotNull(self.view.emptyNaviDecorator) then
        self:SetNaviTarget(self.view.emptyNaviDecorator)
        self.view.emptyNaviDecorator.hideNaviHint = true
    end
end

RewardsPopUpForSystemCtrl.OnShow = HL.Override() << function(self)

    Notify(MessageConst.TOGGLE_IN_MAIN_HUD_STATE, { key = "systemRewards", isInMainHud = false })
end

RewardsPopUpForSystemCtrl.OnHide = HL.Override() << function(self)
    Notify(MessageConst.TOGGLE_IN_MAIN_HUD_STATE, { key = "systemRewards", isInMainHud = true })
end

RewardsPopUpForSystemCtrl.OnClose = HL.Override() << function(self)
    Notify(MessageConst.TOGGLE_IN_MAIN_HUD_STATE, { key = "systemRewards", isInMainHud = true })
end
















































RewardsPopUpForSystemCtrl.ShowSystemRewards = HL.StaticMethod(HL.Table) << function(args)
    if RewardsPopUpForSystemCtrl._TryShowCharRewards(args) then
        return
    end
    if RewardsPopUpForSystemCtrl._TryShowWeaponRewards(args) then
        return
    end
    Notify(MessageConst.HIDE_ITEM_TIPS)
    if UIManager:IsOpen(PANEL_ID) then
        UIManager:SetTopOrder(PANEL_ID)
    end

    local self = UIManager:AutoOpen(PANEL_ID, nil, false)
    self:_ShowRewards(args)
end

RewardsPopUpForSystemCtrl.CSShowSystemRewards = HL.StaticMethod(HL.Table) << function(args)
    local title, items, inputChars, onComplete = unpack(args)
    local newArgs = {
        title = title,
        items = items,
        chars = inputChars,
        onComplete = onComplete
    }
    RewardsPopUpForSystemCtrl.ShowSystemRewards(newArgs)
end

RewardsPopUpForSystemCtrl._TryShowCharRewards = HL.StaticMethod(HL.Any).Return(HL.Boolean) << function(args)
    if not args.chars then
        return false
    end

    local chars
    local extraRewardFirstGetChar
    if type(args.chars) == "table" then
        chars = args.chars
    else

        chars = {}
        for _, v in pairs(args.chars) do

            local msg = v
            local info = {
                charId = msg.CharTemplateId,
                isNew = not msg.IsConverted,
            }
            info.rarity = Tables.characterTable[info.charId].rarity
            info.items = {}
            if not string.isEmpty(msg.ConvertedItemId) then
                table.insert(info.items, { id = msg.ConvertedItemId, count = 1 })
            end
            if not string.isEmpty(msg.ConvertedRewardId) then
                UIUtils.getRewardItems(msg.ConvertedRewardId, info.items)
            end
            table.insert(chars, info)

            if not string.isEmpty(msg.FirstGotRewardId) then
                local itemBundle = UIUtils.getRewardFirstItem(msg.FirstGotRewardId)
                if not extraRewardFirstGetChar then
                    extraRewardFirstGetChar = {
                        id = itemBundle.id,
                        count = itemBundle.count,
                    }
                else
                    extraRewardFirstGetChar.count = extraRewardFirstGetChar.count + itemBundle.count
                end
            end
        end
    end








    local items = RewardsPopUpForSystemCtrl._TryConvertCSItemsLuaToTable(args.items)

    local initWeaponIds = {}
    for _, charInfo in pairs(chars) do
        local _, charCfg = Tables.characterTable:TryGetValue(charInfo.charId)
        if charCfg then
            table.insert(initWeaponIds, charCfg.defaultWeaponId)
        end
    end
    local itemCount = #items
    for i = itemCount, 1, -1 do
        local itemInfo = items[i]
        if lume.find(initWeaponIds, itemInfo.id) then

            table.remove(items, i)
        elseif extraRewardFirstGetChar and extraRewardFirstGetChar.id == itemInfo.id then

            table.remove(items, i)
        end
    end

    args.chars = nil
    args.items = items
    if extraRewardFirstGetChar then
        args.extraItems = args.extraItems or {}
        table.insert(args.extraItems, {
            extraTitle = Language.LUA_GACHA_FIRST_GET_CHAR_REWARD_TOAST_TITLE,
            titleColorState = "Yellow",
            item = extraRewardFirstGetChar,
        })
    end



    PhaseManager:OpenPhaseFast(PhaseId.GachaChar, {
        chars = chars,
        seamlessExit = true,
        onComplete = function()
            RewardsPopUpForSystemCtrl.ShowSystemRewards(args)
        end
    })
    return true
end

RewardsPopUpForSystemCtrl._TryShowWeaponRewards = HL.StaticMethod(HL.Any).Return(HL.Boolean) << function(args)
    if args._weaponDisplayShown or args.notShowGachaWeaponDisplay then
        return false
    end
    local items = RewardsPopUpForSystemCtrl._TryConvertCSItemsLuaToTable(args.items)
    local weapons = {}
    for _, itemInfo in ipairs(items) do
        local itemCfg = Tables.itemTable[itemInfo.id]
        if itemCfg.type == GEnums.ItemType.Weapon and itemCfg.rarity >= 6 then
            table.insert(weapons, {
                weaponId = itemInfo.id,
                rarity = itemCfg.rarity,
                items = {},
                isNew = Utils.getItemCount(itemInfo.id) <= 1,
            })
        end
    end
    if #weapons == 0 then
        return false
    end
    local origOnComplete = args.onComplete
    args.onComplete = nil
    args._weaponDisplayShown = true
    PhaseManager:OpenPhaseFast(PhaseId.GachaWeapon, {
        weapons = weapons,
        onComplete = function()
            args.onComplete = origOnComplete
            RewardsPopUpForSystemCtrl.ShowSystemRewards(args)
        end,
    })
    return true
end

RewardsPopUpForSystemCtrl._TryConvertCSItemsLuaToTable = HL.StaticMethod(HL.Any).Return(HL.Table) << function(csItems)
    if type(csItems) == "table" then
        return csItems
    else


        local items = {}
        for _, v in pairs(csItems) do
            local ok, instId = pcall(function()
                return v.instId
                end)
            table.insert(items, { id = v.id, count = v.count, instId = ok and instId or 0 })
        end
        return items
    end
end

RewardsPopUpForSystemCtrl._ShowRewards = HL.Method(HL.Table) << function(self, args)
    self.m_args = args

    if not string.isEmpty(args.title) then
        self.view.titleTxt.text = args.title
    else
        self.view.titleTxt.text = Language.LUA_DEFAULT_SYSTEM_REWARD_POP_UP_TITLE
    end
    if args.subTitle then
        self.view.subTitleTxt.text = args.subTitle
        self.view.subTitleTxt.gameObject:SetActive(true)
    else
        self.view.subTitleTxt.gameObject:SetActive(false)
    end

    if args.icon then
        self.view.rewardsTypeIcon:LoadSprite(UIConst.UI_SPRITE_REWARDS, args.icon)
    else
        self.view.rewardsTypeIcon:LoadSprite(UIConst.UI_SPRITE_REWARDS, "icon_common_rewards")
    end

    if self.view.moreIndicatorImg then
        if args.hideDecoArrow then
            self.view.moreIndicatorImg.gameObject:SetActive(false)
        else
            self.view.moreIndicatorImg.gameObject:SetActive(true)
        end
    end

    local items = RewardsPopUpForSystemCtrl._TryConvertCSItemsLuaToTable(args.items)
    local count = #items

    for k = 1, count do
        local v = items[k]
        if type(v) ~= "table" then
            v = { id = v.id, count = v.count }
            items[k] = v
        end
        local iData = Tables.itemTable[v.id]
        v.sortId1 = iData.sortId1
        v.sortId2 = iData.sortId2
        v.rarity = iData.rarity
    end
    table.sort(items, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))

    for _, v in ipairs(items) do
        local itemCfg = Tables.itemTable[v.id]
        if itemCfg.type == GEnums.ItemType.Equip then
            local hasFormula, formulaId = Tables.equipFormulaReverseTable:TryGetValue(v.id)
            if hasFormula and not Utils.isSystemUnlocked(GEnums.UnlockSystemType.Equip) then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_REWARD_EQUIP_NOT_UNLOCKED_TOAST)
                break
            end
        end
    end

    self.m_items = items

    self.view.rewardsScrollList.gameObject:SetActive(false)
    self.view.rewardsScrollList:UpdateCount(count, true)

    self.view.skipBtn.gameObject:SetActive(true)

    self:_TryProcessInterruptMessage(true)


    if self.view.extraRewardNode then
        self.m_extraItemInfos = args.extraItems
        if self.m_extraItemInfos then
            self.view.extraRewardNode.gameObject:SetActive(true)
            local itemCount = #self.m_extraItemInfos
            self.m_extraItemCellCache:Refresh(itemCount, function(cell, luaIndex)
                local info = self.m_extraItemInfos[luaIndex]
                cell:InitItem(info.item, true)
                cell:SetExtraInfo({
                    isSideTips = DeviceInfo.usingController,
                })
            end)
            self.m_extraItemTitleCellCache:Refresh(itemCount, function(cell, luaIndex)
                local info = self.m_extraItemInfos[luaIndex]
                cell.rewardTitleTxt.text = info.extraTitle
                cell.stateController:SetState(string.isEmpty(info.titleColorState) and "Yellow" or info.titleColorState)
            end)
        else
            self.view.extraRewardNode.gameObject:SetActive(false)
        end
    end



    if self.view.extraScoreNode then
        local showExtraScore = args.extraScore and args.extraScore > 0
        self.view.extraScoreNode.gameObject:SetActive(showExtraScore)
        if showExtraScore then
            self.view.extraScoreNode.scoreTxt.text = string.format("+%d", args.extraScore)
        end
    end



    if self.view.hintNode then
        local showHint = args.showHint or false
        self.view.hintNode.gameObject:SetActive(showHint)
    end

end

RewardsPopUpForSystemCtrl._OnClickSkip = HL.Method() << function(self)
    self.view.luaPanel.animationWrapper:SkipInAnimation()
    self.view.rewardsScrollList:SkipGraduallyShow()
end

RewardsPopUpForSystemCtrl._OnClickClose = HL.Method() << function(self)
    if self.m_args.closeFast then
        local onComplete = self.m_args.onComplete
        self:_ClearData()
        self:Hide()
        if onComplete then
            onComplete()
        end
    else
        self:PlayAnimationOutWithCallback(function()
            local onComplete = self.m_args.onComplete
            self:_ClearData()
            self:Hide()
            if onComplete then
                onComplete()
            end
        end)
    end
end

RewardsPopUpForSystemCtrl._ClearData = HL.Method() << function(self)
    self:_TryProcessInterruptMessage(false)
    self.m_args.onComplete = nil
    self.m_args = nil
    self.m_items = nil
    self.m_extraItemInfos = nil
end

RewardsPopUpForSystemCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local itemBundle = self.m_items[index]
    cell:InitItem(itemBundle, true)
    cell:SetExtraInfo({
        isSideTips = DeviceInfo.usingController,
    })

    if DeviceInfo.usingController then
        cell:SetEnableHoverTips(false)
    end
    if cell.view.extraCornerNode ~= nil then
        cell.view.extraCornerNode.gameObject:SetActive(itemBundle.isExtra and true or false)
    end
    if cell.view.friendBoostTips ~= nil then
        cell.view.friendBoostTips.gameObject:SetActive(itemBundle.needShowHelp and true or false)
    end
    if cell.view.doubleNode ~= nil then
        cell.view.doubleNode.gameObject:SetActive(itemBundle.isDouble == true)
    end
    cell:ShowGemPerfectIcon(itemBundle.showGemPerfectIcon == true)
    UIUtils.setRewardItemRarityGlow(cell, UIUtils.getItemRarity(itemBundle.id))
    local isFullBottle, bottleData = Tables.fullBottleTable:TryGetValue(itemBundle.id)
    if isFullBottle then
        cell.view.name.text = string.format(Language.LUA_REWARD_FULL_BOTTLE_FORMAT, Tables.itemTable[bottleData.emptyBottleId].name, Tables.itemTable[bottleData.liquidId].name)
    end
    cell.gameObject.name = itemBundle.id
end

RewardsPopUpForSystemCtrl._TryProcessInterruptMessage = HL.Method(HL.Boolean) << function(self, register)
    local interrupt = self.m_args and self.m_args.interrupt
    if not interrupt then
        return
    end

    local groupKey = "RewardsPopUpForSystemInterruptMessage"
    for _, message in ipairs(interrupt.interruptMessage) do
        if register then
            MessageManager:Register(message, function()

                if interrupt.onInterrupt then
                    interrupt.onInterrupt()
                end
                self:Hide()
            end, groupKey)
        else
            MessageManager:UnregisterAll(groupKey)
        end
    end
end

RewardsPopUpForSystemCtrl._OnGraduallyShowFinish = HL.Method() << function(self)
    self.view.skipBtn.gameObject:SetActive(false)
    self.view.controllerHintPlaceholder.gameObject:SetActive(true)
    local firstItemGo = self.view.rewardsScrollList:Get(0)
    if firstItemGo then
        self.view.focusItemKeyHint.gameObject:SetActive(true)
        self.view.focusItemKeyHint.transform.position = firstItemGo.transform.position
        local keyHintPos = self.view.focusItemKeyHint.transform.localPosition
        keyHintPos.x = keyHintPos.x - 50
        keyHintPos.y = keyHintPos.y - 90
        self.view.focusItemKeyHint.transform.localPosition = keyHintPos
    end

    if self.m_args.onGraduallyShowFinishItemList then
        self.m_args.onGraduallyShowFinishItemList()
    end
end

RewardsPopUpForSystemCtrl.OnShow = HL.Override() << function(self)
    Notify(MessageConst.ON_ENTER_BLOCKED_REWARD_POP_UP_PANEL)

    self.view.focusItemKeyHint.gameObject:SetActive(false)
    self.view.controllerHintPlaceholder.gameObject:SetActive(false)
end
RewardsPopUpForSystemCtrl.OnHide = HL.Override() << function(self)
    Notify(MessageConst.ON_EXIT_BLOCKED_REWARD_POP_UP_PANEL)
end
RewardsPopUpForSystemCtrl.OnClose = HL.Override() << function(self)
    Notify(MessageConst.ON_EXIT_BLOCKED_REWARD_POP_UP_PANEL)
end

RewardsPopUpForSystemCtrl.OnSortingOrderChange = HL.Override(HL.Number, HL.Boolean) << function(self, order, isInit)
    RewardsPopUpForSystemCtrl.Super.OnSortingOrderChange(self, order, isInit)
    Notify(MessageConst.REFRESH_CONTROLLER_HINT_ORDER)
end

HL.Commit(RewardsPopUpForSystemCtrl)
