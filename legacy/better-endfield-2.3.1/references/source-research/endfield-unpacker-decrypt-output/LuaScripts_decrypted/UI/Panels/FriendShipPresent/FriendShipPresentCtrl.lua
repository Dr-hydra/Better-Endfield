
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PHASE_ID = PhaseId.FriendShipPresent
local INSTRUCTION_ID = "spaceship_gift_send"

FriendShipPresentCtrl = HL.Class('FriendShipPresentCtrl', uiCtrl.UICtrl)

FriendShipPresentCtrl.m_charId = HL.Field(HL.String) << ""

FriendShipPresentCtrl.m_level = HL.Field(HL.Number) << 1

FriendShipPresentCtrl.m_charTagData = HL.Field(HL.Userdata)

FriendShipPresentCtrl.m_gifts = HL.Field(HL.Table)

FriendShipPresentCtrl.m_filteredGifts = HL.Field(HL.Table)

FriendShipPresentCtrl.m_sentGifts = HL.Field(HL.Table)

FriendShipPresentCtrl.m_selected = HL.Field(HL.Table)

FriendShipPresentCtrl.m_curSelectedNum = HL.Field(HL.Number) << 0

FriendShipPresentCtrl.m_curSelectedIncrease = HL.Field(HL.Number) << 0

FriendShipPresentCtrl.m_curIncreaseInfo = HL.Field(HL.Table)

FriendShipPresentCtrl.m_getGiftItemCell = HL.Field(HL.Function)

FriendShipPresentCtrl.m_getSentGiftItemCell = HL.Field(HL.Function)

FriendShipPresentCtrl.m_itemTagCellCache = HL.Field(HL.Forward("UIListCache"))

FriendShipPresentCtrl.m_charTagCellCache = HL.Field(HL.Forward("UIListCache"))

FriendShipPresentCtrl.m_successCor = HL.Field(HL.Thread)

FriendShipPresentCtrl.s_sortInfo = HL.StaticField(HL.Table) << {}

FriendShipPresentCtrl.m_selectedFilterTags = HL.Field(HL.Table)



FriendShipPresentCtrl.m_decreaseButtonBindingId = HL.Field(HL.Number) << -1

FriendShipPresentCtrl.m_decreaseButtonLongPressBindingId = HL.Field(HL.Number) << -1

FriendShipPresentCtrl.m_decreaseButtonReleaseBindingId = HL.Field(HL.Number) << -1

FriendShipPresentCtrl.m_increaseButtonLongPressBindingId = HL.Field(HL.Number) << -1

FriendShipPresentCtrl.m_increaseButtonReleaseBindingId = HL.Field(HL.Number) << -1

FriendShipPresentCtrl.m_focusItemId = HL.Field(HL.String) << ""

FriendShipPresentCtrl.m_focusLuaIndex = HL.Field(HL.Number) << -1

FriendShipPresentCtrl.m_tipsItemId = HL.Field(HL.String) << ""

FriendShipPresentCtrl.m_tipsLogicVisible = HL.Field(HL.Boolean) << false

FriendShipPresentCtrl.m_itemFillCor = HL.Field(HL.Thread)






FriendShipPresentCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_VALUABLE_DEPOT_CHANGED] = '_OnItemRefresh',
    [MessageConst.ON_CHAR_FRIENDSHIP_CHANGED] = '_OnCharPresentRefresh',
    [MessageConst.ON_CHAR_FRIENDSHIP_SEND_CHAR_GIFT_SUCCESS] = '_OnCharSendPresentSuccess',
}

FriendShipPresentCtrl._OnItemRefresh = HL.Method(HL.Table) << function(self, _)

    self:_RefreshGiftList()
end

FriendShipPresentCtrl._OnCharPresentRefresh = HL.Method() << function(self)
    self:_RefreshFriendshipInfo(true)
end

FriendShipPresentCtrl._OnCharSendPresentSuccess = HL.Method(HL.Table) << function(self, data)
    local level = CSPlayerDataUtil.GetFriendshipLevelByChar(self.m_charId)
    local deltaFav = unpack(data)
    self:Notify(MessageConst.DIALOG_SEND_PRESENT_END, {
        success = true,
        nextIndex = 0,
        deltaFav = deltaFav,
        selectedItems = self.m_selected,
        levelChanged = level ~= self.m_level
    })
    self.m_level = level
end


FriendShipPresentCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_charId = arg.charId
    self.m_level = CSPlayerDataUtil.GetFriendshipLevelByChar(self.m_charId)
    self.m_charTagData = Tables.characterTagTable:GetValue(self.m_charId)
    self.m_gifts = {}
    self.m_filteredGifts = {}
    self.m_sentGifts = {}
    self.m_selected = {}
    self.m_curIncreaseInfo = {}


    self.view.titleTxt.text = Language.LUA_GIFT_SEND_TITLE

    self.view.helpBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, INSTRUCTION_ID)
    end)


    self:InitFriendshipNode()


    self.m_getGiftItemCell = UIUtils.genCachedCellFunction(self.view.bottomList.scrollList)
    self.view.bottomList.scrollList.onUpdateCell:AddListener(function(object, index)
        self:_RefreshGiftItemCell(object, LuaIndex(index))
    end)

    self.m_getSentGiftItemCell = UIUtils.genCachedCellFunction(self.view.bottomList.sentGiftScrollList)
    self.view.bottomList.sentGiftScrollList.onUpdateCell:AddListener(function(object, index)
        self:_RefreshSentGiftItemCell(object, LuaIndex(index))
    end)


    self.view.closeBtn.onClick:AddListener(function()
        self:Notify(MessageConst.DIALOG_SEND_PRESENT_END, {
            success = false,
            nextIndex = 1,
        })
    end)


    self.view.main.onClick:AddListener(function()
        self:_RefreshTips(false)
    end)

    self.view.bottomList.buttonClear.onClick:AddListener(function()
        self:_ClearSelected()
    end)


    self.view.bottomList.buttonSend.onClick:AddListener(function()
        self:_SendPresentToChar()
    end)


    self:InitFilterButton()

    local FRIENDSHIP_PRESENT_GIFT_SORT_OPTION = {
        {

            name = Language.LUA_GIFT_SORT_DEFAULT,
            keys = { "isLike", "isAvailable", "isPreferTagMatch", "isPopular", "count", "sortId1Reverse" },
            reverseKeys = {"isLikeReverse", "isAvailableReverse", "isPreferTagMatch", "isPopular", "count", "sortId1Reverse" },
        },
        {

            name = Language.LUA_GIFT_SORT_PREFER_TAG_MATCH,
            keys = { "isLike", "isPreferTagMatch", "isPopular", "isAvailable", "count", "sortId1Reverse" },
            reverseKeys = { "isLikeReverse", "isPreferTagMatch", "isPopular", "isAvailableReverse", "count", "sortId1Reverse" },
        },
        {

            name = Language.LUA_GIFT_SORT_COUNT,
            keys = { "isLike", "count", "isAvailable", "isPreferTagMatch", "isPopular", "sortId1Reverse" },
            reverseKeys = { "isLikeReverse", "count", "isAvailableReverse", "isPreferTagMatch", "isPopular", "sortId1Reverse" },
        },
    }


    self.view.sortNode:InitSortNode(FRIENDSHIP_PRESENT_GIFT_SORT_OPTION, function(optData, isIncremental)
        self:_OnSortChanged(optData, isIncremental)
    end, 0, false, false, self.view.filterBtn)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

FriendShipPresentCtrl.OnClose = HL.Override() << function(self)
    self:_StopFillTimer()
end



FriendShipPresentCtrl.InitFilterButton = HL.Method() << function(self)
    local hobbyFilterTag = {
        name = Language.LUA_GIFT_HOBBY_TAG_MATCH,
        filterWithHobbyTag = true,
    }

    local preferFilterTag = {
        name = Language.LUA_GIFT_PREFER_TAG_MATCH,
        filterWithPreferTag = true,
    }

    local defaultSelectedTags = {}
    self.m_selectedFilterTags = defaultSelectedTags

    local filterConfig = {
        tagGroups = {
            {tags = {hobbyFilterTag, preferFilterTag},}
        },
        selectedTags = defaultSelectedTags,
        onConfirm = function(selectedTags) self:OnFilterChanged(selectedTags) end,
        getResultCount = function(selectedTags) return self:GetFilterResultCount(selectedTags) end,
        sortNodeWidget = self.view.sortNode,
    }

    self.view.filterBtn:InitFilterBtn(filterConfig)
end

FriendShipPresentCtrl.OnFilterChanged = HL.Method(HL.Table) << function(self, selectedTags)
    self.m_selectedFilterTags = selectedTags
    local filteredGifts = self:GetFilterResult(self.m_selectedFilterTags, true)


    local isChanged = false
    if #filteredGifts ~= #self.m_filteredGifts then
        isChanged = true
    else
        for i, v in ipairs(filteredGifts) do
            if v ~= self.m_filteredGifts[i] then
                isChanged = true
                break
            end
        end
    end

    if isChanged then
        self.m_filteredGifts = filteredGifts
        self.view.sortNode:SortCurData()
    end
end

FriendShipPresentCtrl.GetFilterResultCount = HL.Method(HL.Table).Return(HL.Number) << function(self, selectedTags)
    return #self:GetFilterResult(selectedTags)
end

FriendShipPresentCtrl.GetFilterResult = HL.Method(HL.Table, HL.Opt(HL.Boolean)).Return(HL.Table) << function(self, selectedTags, withSelected)
    if selectedTags == nil or #selectedTags == 0 then

        return self.m_gifts
    end

    local filterResult = {}
    for _, gift in ipairs(self.m_gifts) do
        if withSelected and self.m_selected[gift.itemId] and self.m_selected[gift.itemId] > 0 then
            table.insert(filterResult, gift)
            goto continue
        end

        local isLike = gift.isLike == 1
        local isPreferTagMatch = gift.isPreferTagMatch == 1
        local isMatched = true
        for _, tag in ipairs(selectedTags) do
            if tag.filterWithHobbyTag and not isLike then
                isMatched = false
                break
            end

            if tag.filterWithPreferTag and not isPreferTagMatch then
                isMatched = false
                break
            end
        end

        if isMatched then
            table.insert(filterResult, gift)
        end

        ::continue::
    end
    return filterResult
end




FriendShipPresentCtrl.OnShow = HL.Override() << function(self)
    self:_RefreshCharInfo()
    self:_RefreshFriendshipInfo(true)
    self:_RefreshGiftList()
    self:_RefreshButtonState()
end

FriendShipPresentCtrl._RefreshCharInfo = HL.Method() << function(self)
    local charId = self.m_charId
    if charId == nil or charId == "" then
        return
    end

    local charInfoNode = self.view.rightTrustNode.charInfoNode
    if self.m_charTagCellCache == nil then
        self.m_charTagCellCache = UIUtils.genCellCache(charInfoNode.tagNode.tagCell)
    end

    local charName = Tables.characterTable[charId].name
    local charHobbyTags = self.m_charTagData.hobbyTagIds

    local charGiftPreferTag = self.m_charTagData.giftPreferTagId

    charInfoNode.name.text = charName
    charInfoNode.headIcon:LoadSprite(UIConst.UI_SPRITE_CHAR_REMOTE_ICON, UIConst.UI_CHAR_REMOTE_ICON_PREFIX .. charId)

    self.m_charTagCellCache:Refresh(charHobbyTags.Count, function(cell, index)
        local tagId = charHobbyTags[CSIndex(index)]
        local tagName = Tables.tagDataTable[tagId].tagName
        cell.nameTxt.text = tagName;
        cell.stateController:SetState("Normal")
    end)

    if charGiftPreferTag.Count > 0 then

        local preferTagId = charGiftPreferTag[CSIndex(1)]
        local preferTagName = Tables.tagDataTable[preferTagId].tagName
        charInfoNode.areaName.text = preferTagName

        local preferTagIcon = self:_GetPreferTagIcon(preferTagId)
        if preferTagIcon and preferTagIcon ~= "" then
            charInfoNode.areaIcon:LoadSprite(UIConst.UI_SPRITE_SHIP, preferTagIcon)
        end
    end
end

FriendShipPresentCtrl.InitFriendshipNode = HL.Method() << function(self)
    local rightTrustNode = self.view.rightTrustNode
    local giftGainRatioLevels = Tables.spaceshipCharGiftGainRatio

    rightTrustNode.bigSmiley:InitCircularProgressBar(giftGainRatioLevels[1].maxLimit)
    rightTrustNode.midSmiley:InitCircularProgressBar(giftGainRatioLevels[2].maxLimit - giftGainRatioLevels[1].maxLimit)
    rightTrustNode.smallSmiley:InitCircularProgressBar(giftGainRatioLevels[3].maxLimit - giftGainRatioLevels[2].maxLimit)
end

FriendShipPresentCtrl._RefreshFriendshipInfo = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    isInit = isInit or false
    if self:_IsMax() then
        self.view.mainStateController:SetState("Max")
        self.view.reliabilityCell:InitReliabilityCell(self.m_charId)
        self.view.reliabilityCell:RefreshTmpFriendship(0)
        self:_RefreshSentGifts()
        return
    end

    self.view.mainStateController:SetState("Normal")
    local rightTrustNode = self.view.rightTrustNode
    local giftGainRatioLevels = Tables.spaceshipCharGiftGainRatio

    local percent, showPercent = self:_GetCurFriendshipPercent()
    local charTrustNode = rightTrustNode.charInfoNode.charTrustNode

    if self.m_curSelectedIncrease > 0 then
        local tmpPercent, tmpShowPercent = self:_GetCurFriendshipPercent(self.m_curSelectedIncrease)
        charTrustNode.circleTmpStart.fillAmount = lume.clamp(tmpShowPercent / 100, 0, 1);
        charTrustNode.circleCur.fillAmount = lume.clamp(showPercent / 100, 0, 1);
        charTrustNode.trustNumTxt.text = string.format("%d%%", tmpPercent)
        if charTrustNode.stateController.currentStateName ~= "High" then
            charTrustNode.stateController:SetState("High")
            charTrustNode.animationWrapper:PlayInAnimation()
        end
    else
        charTrustNode.circleCur.fillAmount = lume.clamp(showPercent / 100, 0, 1);
        charTrustNode.circleTmpStart.fillAmount = 0;
        charTrustNode.trustNumTxt.text = string.format("%d%%", percent)
        if charTrustNode.stateController.currentStateName ~= "Normal" then
            charTrustNode.stateController:SetState("Normal")
            charTrustNode.animationWrapper:PlayOutAnimation()
        end
    end

    local dailyFriendshipIncrease = CSPlayerDataUtil.GetCharDailyFriendshipIncrease(self.m_charId)
    local dailyFriendshipGainRatioLevel = CSPlayerDataUtil.GetGiftGainRatioLevelByChar(self.m_charId, self.m_curSelectedIncrease)

    local sendTips = Language.LUA_GIFT_SEND_TIPS_LIMIT_LV1
    if self:_IsTodayMax() then
        rightTrustNode.stateController:SetState("SentOut")

        rightTrustNode.bigSmiley:SetCurrentValue(dailyFriendshipIncrease)
        sendTips = Language.LUA_GIFT_SEND_TIPS_LIMIT_MAX
    else
        if dailyFriendshipGainRatioLevel == 1 then
            rightTrustNode.stateController:SetState("HighTrust", false)
            sendTips = Language.LUA_GIFT_SEND_TIPS_LIMIT_LV1
        elseif dailyFriendshipGainRatioLevel == 2 then
            rightTrustNode.stateController:SetState("MiddleTrust", false)
            sendTips = Language.LUA_GIFT_SEND_TIPS_LIMIT_LV2
        elseif dailyFriendshipGainRatioLevel == 3 then
            rightTrustNode.stateController:SetState("LowTrust", false)
            sendTips = Language.LUA_GIFT_SEND_TIPS_LIMIT_LV3
        end

        if isInit then


            rightTrustNode.bigSmiley:SetCurrentValue(dailyFriendshipIncrease)
            rightTrustNode.midSmiley:SetCurrentValue(dailyFriendshipIncrease - giftGainRatioLevels[1].maxLimit)
            rightTrustNode.smallSmiley:SetCurrentValue(dailyFriendshipIncrease - giftGainRatioLevels[2].maxLimit)
        end


        rightTrustNode.bigSmiley:SetTempValue(self.m_curIncreaseInfo[1] or 0, dailyFriendshipGainRatioLevel >= 1)
        rightTrustNode.midSmiley:SetTempValue(self.m_curIncreaseInfo[2] or 0, dailyFriendshipGainRatioLevel >= 2)
        rightTrustNode.smallSmiley:SetTempValue(self.m_curIncreaseInfo[3] or 0, dailyFriendshipGainRatioLevel >= 3)
    end

    rightTrustNode.sendText.text = sendTips
end

FriendShipPresentCtrl._GetCharLike = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    local giftData = Tables.giftItemTable:GetValue(itemId)
    local tagList = giftData.tagList
    local charTagData = self.m_charTagData
    local hobbyTagIds = charTagData.hobbyTagIds
    local like = false
    for _, tag in pairs(tagList) do
        if lume.find(hobbyTagIds, tag) then
            like = true
            break
        end
    end
    return like
end

FriendShipPresentCtrl._GetIsPopularGift = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    local giftData = Tables.giftItemTable:GetValue(itemId)
    if not giftData.isPopular then
        return false
    end

    return Utils.isCurTimeInTimeIdRange(giftData.finishPopularTimeId)
end

FriendShipPresentCtrl._GetIsShowPopularLeftTime = HL.Method(HL.String).Return(HL.Boolean, HL.Number) << function(self, itemId)
    local giftData = Tables.giftItemTable:GetValue(itemId)
    if not giftData.isShowPopularFinishTime then
        return false, -1
    end

    if string.isEmpty(giftData.finishPopularTimeId) then
        return false, -1
    end

    local hasCfg, timeCfg = Tables.timeRangeTable:TryGetValue(giftData.finishPopularTimeId)
    if not hasCfg then
        return false, -1
    end

    local serverAreaTypeInt = Utils.getServerAreaType():GetHashCode()
    local timeRange = timeCfg.timeRangeList[CSIndex(serverAreaTypeInt)]
    local timeZoneSeconds = Utils.getServerTimeZoneOffsetSeconds()
    local closeTs = Utils.timeStr2TimeStamp(timeRange.closeTime, timeZoneSeconds)
    return true, closeTs
end

FriendShipPresentCtrl._GetCharPreferTagMatch = HL.Method(HL.String).Return(HL.Boolean) << function(self, itemId)
    local giftData = Tables.giftItemTable:GetValue(itemId)
    local giftPreferTag = giftData.giftPreferTag
    local charTagData = self.m_charTagData
    local charPreferTags = charTagData.giftPreferTagId
    return lume.find(charPreferTags, giftPreferTag) ~= nil
end

FriendShipPresentCtrl._RefreshSentGifts = HL.Method() << function(self)
    self.m_sentGifts = {}
    local sentGifts = CSPlayerDataUtil.GetCharSentGifts(self.m_charId)
    local list = Tables.itemListByTypeTable:GetValue(GEnums.ItemType.Gift).list
    for _, itemId in pairs(list) do
        local success, giftData = Tables.giftItemTable:TryGetValue(itemId)
        if not success or giftData.giftPreferTag == nil or giftData.giftPreferTag == "" then

            goto continue
        end

        local isLike = self:_GetCharLike(itemId)
        if not isLike then
            goto continue
        end

        local res, sentCount = sentGifts:TryGetValue(itemId)
        if not res then
            sentCount = 0
        end

        local isPreferTagMatch = self:_GetCharPreferTagMatch(itemId)
        local data = {
            itemId = itemId,
            isPreferTagMatch = isPreferTagMatch and 1 or 0,
            sentCount = sentCount,
        }

        table.insert(self.m_sentGifts, data)
        ::continue::
    end

    table.sort(self.m_sentGifts, Utils.genSortFunction({"sentCount", "isPreferTagMatch"}, false))

    self.view.bottomList.sentGiftScrollList:UpdateCount(#self.m_sentGifts)
    self.view.sentGiftNaviGroup:NaviToThisGroup(true)
end

FriendShipPresentCtrl._RefreshGiftList = HL.Method() << function(self)
    if self:_IsMax() then
        self.view.giftRecordTitle.text = Language.LUA_GIFT_RECORD_TITLE

        return
    end

    self.m_gifts = {}
    local list = Tables.itemListByTypeTable:GetValue(GEnums.ItemType.Gift).list
    for _, itemId in pairs(list) do
        local success, giftData = Tables.giftItemTable:TryGetValue(itemId)
        if not success or giftData.giftPreferTag == nil or giftData.giftPreferTag == "" then

            goto continue
        end

        local count = Utils.getItemCount(itemId)
        local favorablePoint = giftData.favorablePoint
        local isLike = self:_GetCharLike(itemId)
        local isPopular = self:_GetIsPopularGift(itemId)
        local isPreferTagMatch = self:_GetCharPreferTagMatch(itemId)
        local itemData = Tables.itemTable:GetValue(itemId)
        local isAvailable = count > 0 and isLike
        local data = {
            itemId = itemId,
            count = count,
            countReverse = -count,
            isLike = isLike and 1 or 0,
            isLikeReverse = isLike and 0 or 1,
            isAvailable = isAvailable and 1 or 0,
            isAvailableReverse = isAvailable and 0 or 1,
            isPreferTagMatch = isPreferTagMatch and 1 or 0,
            isPopular = isPopular and 1 or 0,
            favorablePoint = favorablePoint,
            rarity = itemData.rarity,
            sortId1Reverse = -itemData.sortId1,
        }
        table.insert(self.m_gifts, data)
        ::continue::
    end

    self:OnFilterChanged(self.m_selectedFilterTags)
end


FriendShipPresentCtrl._GetCurFriendshipPercent = HL.Method(HL.Opt(HL.Number)).Return(HL.Number, HL.Number) << function(self, tmpValue)
    tmpValue = tmpValue or 0
    local curLevel = CSPlayerDataUtil.GetFriendshipLevelByChar(self.m_charId)
    local maxLevel = CSPlayerDataUtil.maxFriendshipLevel

    local showPercent = 0
    local percent = 0

    if curLevel ~= maxLevel then
        local lastFriendShip

        if curLevel == 1 then
            lastFriendShip = 0
        else
            lastFriendShip = CSPlayerDataUtil.favoriteLevelMap[curLevel - 1]
        end
        local curFriendShip = CSPlayerDataUtil.GetCharFriendship(self.m_charId) + tmpValue
        local needFriendShip = CSPlayerDataUtil.favoriteLevelMap[curLevel]

        showPercent = math.floor((curFriendShip - lastFriendShip) * 100 / needFriendShip)
        percent = CharInfoUtils.getCharRelationShowValue(curFriendShip)
    end

    return percent, showPercent
end


FriendShipPresentCtrl._IsMax = HL.Method(HL.Opt(HL.Number)).Return(HL.Boolean) << function(self, tmpNum)
    local isMax
    if tmpNum then
        local friendshipValue = CSPlayerDataUtil.GetCharFriendship(self.m_charId)
        local tmpValue = friendshipValue + tmpNum
        isMax = tmpValue >= CSPlayerDataUtil.maxFriendship
    else
        local level = CSPlayerDataUtil.GetFriendshipLevelByChar(self.m_charId)
        local maxLevel = CSPlayerDataUtil.maxFriendshipLevel
        isMax = level == maxLevel
    end

    return isMax
end

FriendShipPresentCtrl._IsTodayMax = HL.Method().Return(HL.Boolean) << function(self)
    local friendShipIncreaseRemain = CSPlayerDataUtil.GetCharDailyRemainFriendshipIncrease(self.m_charId)
    return friendShipIncreaseRemain <= 0
end


FriendShipPresentCtrl._OnSortChanged = HL.Method(HL.Table, HL.Boolean) << function(self, optData, isIncremental)
    local keys =  isIncremental and optData.reverseKeys or optData.keys
    table.sort(self.m_filteredGifts, Utils.genSortFunction(keys, isIncremental))
    self.view.bottomList.scrollList:UpdateCount(#self.m_filteredGifts)
    self.view.normalNaviGroup:NaviToThisGroup(true)
end

FriendShipPresentCtrl._GetPreferTagIcon = HL.Method(HL.String).Return(HL.String) << function(self, preferTagId)
    local success, data = Tables.giftPreferTagConfigTable:TryGetValue(preferTagId)
    if success then
        return data.iconName
    else
        return ""
    end
end

FriendShipPresentCtrl._RefreshGiftItemCell = HL.Method(HL.Userdata, HL.Number) << function(self, object, luaIndex)
    local itemId = self.m_filteredGifts[luaIndex].itemId
    local giftData = Tables.giftItemTable:GetValue(itemId)
    local count = Utils.getItemCount(itemId)
    local cell = self.m_getGiftItemCell(object)
    local data = {
        id = itemId,
        count = count
    }

    local itemCell = cell.listCellFriendshipUpgrade

    itemCell.item:SetEnableHoverTips(false)
    itemCell.item:InitItem(data, function()
        self:_OnItemSelectClicked(itemId, luaIndex, 1)
    end)

    itemCell.item.view.button.onLongPress:RemoveAllListeners()
    itemCell.item.view.button.onLongPress:AddListener(function()
        self:_StartFillTimer(itemId, luaIndex, 10)
    end)

    itemCell.item.view.button.onPressEnd:RemoveAllListeners()
    itemCell.item.view.button.onPressEnd:AddListener(function()
        self:_StopFillTimer()
    end)

    itemCell.item.view.button.onIsNaviTargetChanged = function(isNaviTarget)
        if isNaviTarget then
           self:_OnItemFocused(itemId, luaIndex)
        end
    end

    local preferTagId = giftData.giftPreferTag
    local iconName = self:_GetPreferTagIcon(preferTagId)
    if iconName and iconName ~= "" then
        itemCell.areaIcon.gameObject:SetActive(true)
        itemCell.areaIcon:LoadSprite(UIConst.UI_SPRITE_SHIP, iconName)
    end

    local isPopular = self:_GetIsPopularGift(itemId)
    itemCell.hotIcon.gameObject:SetActive(isPopular)


    local isLike = self:_GetCharLike(itemId)
    itemCell.unableGift.gameObject:SetActive(not isLike)


    self:_RefreshSingleItemSelect(itemCell)

    itemCell.btnMinus.onClick:RemoveAllListeners()
    itemCell.btnMinus.onClick:AddListener(function()
        self:_OnItemMinusClicked(itemId, luaIndex, 1)
    end)

    itemCell.btnMinus.onLongPress:RemoveAllListeners()
    itemCell.btnMinus.onLongPress:AddListener(function()
        self:_StartFillTimer(itemId, luaIndex, -10)
    end)

    itemCell.btnMinus.onPressEnd:RemoveAllListeners()
    itemCell.btnMinus.onPressEnd:AddListener(function()
        self:_StopFillTimer()
    end)
end

FriendShipPresentCtrl._RefreshSentGiftItemCell = HL.Method(HL.Userdata, HL.Number) << function(self, object, luaIndex)
    local itemId = self.m_sentGifts[luaIndex].itemId
    local sentCount = self.m_sentGifts[luaIndex].sentCount
    local giftData = Tables.giftItemTable:GetValue(itemId)
    local cell = self.m_getGiftItemCell(object)
    local data = {
        id = itemId,
    }

    local itemCell = cell.listCellFriendshipUpgrade

    itemCell.item:SetEnableHoverTips(false)
    itemCell.item:InitItem(data, function()
        self:_RefreshTips(true, itemId)
    end)

    local preferTagId = giftData.giftPreferTag
    local iconName = self:_GetPreferTagIcon(preferTagId)
    if iconName and iconName ~= "" then
        itemCell.areaIcon.gameObject:SetActive(true)
        itemCell.areaIcon:LoadSprite(UIConst.UI_SPRITE_SHIP, iconName)
    end

    local isPopular = self:_GetIsPopularGift(itemId)
    itemCell.hotIcon.gameObject:SetActive(isPopular)


    if sentCount == 0 then
        itemCell.item:SetIconTransparent(UIConst.ITEM_MISSING_TRANSPARENCY)
    else
        itemCell.item:SetIconTransparent(UIConst.ITEM_EXIST_TRANSPARENCY)
    end

    if sentCount > 0 then
        itemCell.sentCountTxt.gameObject:SetActive(true)
        itemCell.sentCountTxt.text = string.format(Language.LUA_GIFT_SENT_COUNT, sentCount)
    else
        itemCell.sentCountTxt.gameObject:SetActive(false)
    end
end


FriendShipPresentCtrl._OnItemFocused = HL.Method(HL.String, HL.Number) << function(self, itemId, luaIndex)
    if not DeviceInfo.usingController then
        return
    end

    if self:_IsMax() then
        self:_RefreshTips(true, itemId)
        return
    end

    if self.m_focusItemId ~= itemId or self.m_focusLuaIndex ~= luaIndex then
        self:_StopFillTimer()
    end

    self.m_focusItemId = itemId
    self.m_focusLuaIndex = luaIndex

    self:_RefreshInputKeyHint(itemId, luaIndex)
    self:_RefreshTips(true, itemId)
end

FriendShipPresentCtrl._RefreshInputKeyHint = HL.Method(HL.String, HL.Number) << function(self, itemId, luaIndex)
    if not DeviceInfo.usingController then
        return
    end

    if itemId == "" or luaIndex < 0 then
        return
    end

    local selectNum = self.m_selected[itemId] or 0
    local cell = self:_GetCellByIndex(luaIndex)
    if not cell then
        return
    end

    if self.m_decreaseButtonBindingId < 0 then
        self.m_decreaseButtonBindingId = self:BindInputPlayerAction("gift_send_decrease_item_count", function()
            self:_OnItemMinusClicked(self.m_focusItemId, self.m_focusLuaIndex, 1)
        end)

        InputManagerInst:SetBindingText(self.m_decreaseButtonBindingId, Language.LUA_GIFT_COUNT_DECREASE)
    end

    if self.m_decreaseButtonLongPressBindingId < 0 then
        self.m_decreaseButtonLongPressBindingId = self:BindInputPlayerAction("gift_send_decrease_item_count_long_press", function()
            self:_StartFillTimer(self.m_focusItemId, self.m_focusLuaIndex, -10)
        end)
    end

    if self.m_decreaseButtonReleaseBindingId < 0 then
        self.m_decreaseButtonReleaseBindingId = self:BindInputPlayerAction("gift_send_decrease_item_count_release", function()
            self:_StopFillTimer()
        end)
    end

    if self.m_increaseButtonLongPressBindingId < 0 then
        self.m_increaseButtonLongPressBindingId = self:BindInputPlayerAction("gift_send_increase_item_count_long_press", function()
            self:_StartFillTimer(self.m_focusItemId, self.m_focusLuaIndex, 10)
        end)
    end

    if self.m_increaseButtonReleaseBindingId < 0 then
        self.m_increaseButtonReleaseBindingId = self:BindInputPlayerAction("gift_send_increase_item_count_release", function()
            self:_StopFillTimer()
        end)
    end

    local itemCell = cell.listCellFriendshipUpgrade
    if selectNum > 0 then
        InputManagerInst:SetBindingText(itemCell.item.view.button.hoverConfirmBindingId, Language.LUA_GIFT_COUNT_ADD)
        InputManagerInst:ToggleBinding(self.m_decreaseButtonBindingId, true)
        InputManagerInst:ToggleBinding(self.m_decreaseButtonLongPressBindingId, true)
    else
        InputManagerInst:SetBindingText(itemCell.item.view.button.hoverConfirmBindingId, Language.LUA_GIFT_SELECT_ITEM)
        InputManagerInst:ToggleBinding(self.m_decreaseButtonBindingId, false)
        InputManagerInst:ToggleBinding(self.m_decreaseButtonLongPressBindingId, false)
    end
end

FriendShipPresentCtrl._OnItemSelectClicked = HL.Method(HL.String, HL.Number, HL.Number) << function(self, itemId, luaIndex, addCount)
    local count = Utils.getItemCount(itemId)
    local selectNum = self.m_selected[itemId] or 0
    local tmpSelectNum = selectNum + addCount

    if not DeviceInfo.usingController then
        self:_RefreshTips(true, itemId)
    end

    local isMax = self:_IsMax()
    if isMax then
        self:_SelectOnlyOneCell(luaIndex)
        return
    end


    local friendShipIncreaseRemain = CSPlayerDataUtil.GetCharDailyRemainFriendshipIncrease(self.m_charId)
    if friendShipIncreaseRemain <= 0 then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_GIFT_DAILY_FRIENDSHIP_INCREASE_LIMITED)
        self:_SelectOnlyOneCell(luaIndex)
        return
    end


    local isLike = self:_GetCharLike(itemId)
    if not isLike then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_GIFT_HOBBY_TAG_NOT_MATCH)
        return
    end


    if count == 0 then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_GIFT_ITEM_NOT_ENOUGH)
        return
    end


    if count < tmpSelectNum then
        if count > selectNum then
            tmpSelectNum = count
        else
            self:Notify(MessageConst.SHOW_TOAST, Language.LUA_SPACESHIP_GIFT_EMPTY)
            return
        end
    end

    if friendShipIncreaseRemain == 0 or self.m_curSelectedIncrease >= friendShipIncreaseRemain then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_GIFT_DAILY_FRIENDSHIP_INCREASE_LIMITED)
        return
    end

    local realAddCount = tmpSelectNum - selectNum
    if realAddCount > 1 then
        local maxMoreSendCount = self:_GetGiftMaxMoreSendCount(itemId)
        if realAddCount > maxMoreSendCount then
            tmpSelectNum = selectNum + maxMoreSendCount
            self:Notify(MessageConst.SHOW_TOAST, Language.LUA_GIFT_DAILY_FRIENDSHIP_INCREASE_LIMITED)
        end
    end

    if self:_IsMax(self.m_curSelectedIncrease) then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_SPACESHIP_CHAR_FRIENDSHIP_FULL)
        return
    end

    self.m_selected[itemId] = tmpSelectNum

    local cell = self:_GetCellByIndex(luaIndex)
    if cell then
        self:_RefreshSingleItemSelect(cell.listCellFriendshipUpgrade)
    end

    self:_RefreshSelectedFriendshipIncrease()
    self:_RefreshButtonState()
end

FriendShipPresentCtrl._StartFillTimer = HL.Method(HL.String, HL.Number, HL.Number) << function(self, itemId, luaIndex, count)
    self.m_itemFillCor = self:_ClearCoroutine(self.m_itemFillCor)
    self.m_itemFillCor = self:_StartCoroutine(function()
        while true do
            if count > 0 then
                self:_OnItemSelectClicked(itemId, luaIndex, count)
            else
                self:_OnItemMinusClicked(itemId, luaIndex, -count)
            end
            coroutine.wait(0.35)
        end
    end)
end

FriendShipPresentCtrl._StopFillTimer = HL.Method() << function(self)
    self.m_itemFillCor = self:_ClearCoroutine(self.m_itemFillCor)
end


FriendShipPresentCtrl._RefreshSelectedFriendshipIncrease = HL.Method() << function(self)
    local realFriendshipIncrease = 0
    local friendshipIncreaseInfo = {}
    local totalSelectCount = 0

    local sortedGifts = {}
    local keys = lume.keys(self.m_selected)
    table.sort(keys)

    for _, giftId in ipairs(keys) do
        local num = self.m_selected[giftId]
        if num > 0 then
            totalSelectCount = totalSelectCount + num
            local giftData = Tables.giftItemTable:GetValue(giftId)
            local isPreferTagMatch = self:_GetCharPreferTagMatch(giftId)
            local isPopular = self:_GetIsPopularGift(giftId)
            local ratio = (isPreferTagMatch or isPopular) and 1 or Tables.spaceshipConst.notMatchDomainGiftRatio
            table.insert(sortedGifts, {
                itemId = giftId,
                num = self.m_selected[giftId],
                point = lume.round(giftData.favorablePoint * ratio),
            })
        end
    end

    local dailyFriendshipIncrease = CSPlayerDataUtil.GetCharDailyFriendshipIncrease(self.m_charId)
    local giftGainRatioLevel = CSPlayerDataUtil.GetGiftGainRatioLevelByChar(self.m_charId)

    local dailyFriendshipIncreaseMaxLevel = CSPlayerDataUtil.dailyFriendshipIncreaseMaxLevel
    if giftGainRatioLevel > 0 and giftGainRatioLevel <= dailyFriendshipIncreaseMaxLevel then

        local giftLeftPoint = 0
        for i = giftGainRatioLevel, dailyFriendshipIncreaseMaxLevel do
            if #sortedGifts <= 0 and giftLeftPoint == 0 then
                break
            end

            local levelData = Tables.spaceshipCharGiftGainRatio[i]
            local levelRealRemain = levelData.maxLimit - dailyFriendshipIncrease
            local levelRealIncrease = 0
            local levelTotalRemain = levelRealRemain / levelData.gainRatio

            while((#sortedGifts > 0 or giftLeftPoint > 0) and levelTotalRemain > 0) do
                local point = giftLeftPoint

                if #sortedGifts > 0 then
                    local gift = sortedGifts[1]
                    gift.num = gift.num - 1
                    if gift.num <= 0 then
                        table.remove(sortedGifts, 1)
                    end
                    point = point + gift.point
                end

                giftLeftPoint = 0

                if point >= levelTotalRemain then
                    levelRealIncrease = levelRealIncrease + levelRealRemain
                    giftLeftPoint = point - levelTotalRemain
                    levelRealRemain = 0
                    levelTotalRemain = 0
                else
                    local giftRealIncrease =  lume.round(point * levelData.gainRatio)
                    levelRealIncrease = levelRealIncrease + giftRealIncrease
                    levelRealRemain = levelRealRemain - giftRealIncrease
                    levelTotalRemain = levelRealRemain / levelData.gainRatio
                end
            end

            realFriendshipIncrease = realFriendshipIncrease + levelRealIncrease
            dailyFriendshipIncrease = dailyFriendshipIncrease + levelRealIncrease
            friendshipIncreaseInfo[i] = levelRealIncrease
        end
    end

    self.m_curSelectedIncrease = realFriendshipIncrease
    self.m_curIncreaseInfo = friendshipIncreaseInfo
    self.m_curSelectedNum = totalSelectCount

    self:_RefreshFriendshipInfo()
end

FriendShipPresentCtrl._GetGiftMaxMoreSendCount = HL.Method(HL.String).Return(HL.Number) << function(self, giftId)
    local giftData = Tables.giftItemTable:GetValue(giftId)
    local isPreferTagMatch = self:_GetCharPreferTagMatch(giftId)
    local isPopular = self:_GetIsPopularGift(giftId)
    local ratio = (isPreferTagMatch or isPopular) and 1 or Tables.spaceshipConst.notMatchDomainGiftRatio
    local giftPoint = lume.round(giftData.favorablePoint * ratio)

    local dailyFriendshipIncrease = CSPlayerDataUtil.GetCharDailyFriendshipIncrease(self.m_charId) + self.m_curSelectedIncrease
    local giftGainRatioLevel = CSPlayerDataUtil.GetGiftGainRatioLevelByChar(self.m_charId, self.m_curSelectedIncrease)

    local validSendNum = 0
    local dailyFriendshipIncreaseMaxLevel = CSPlayerDataUtil.dailyFriendshipIncreaseMaxLevel
    if giftGainRatioLevel > 0 and giftGainRatioLevel <= dailyFriendshipIncreaseMaxLevel then

        local giftLeftPoint = 0
        for i = giftGainRatioLevel, dailyFriendshipIncreaseMaxLevel do
            local levelData = Tables.spaceshipCharGiftGainRatio[i]
            local levelRealRemain = levelData.maxLimit - dailyFriendshipIncrease
            local levelRealIncrease = 0
            local levelTotalRemain = levelRealRemain / levelData.gainRatio

            while levelTotalRemain > 0 do
                local point = giftLeftPoint
                point = point + giftPoint
                validSendNum = validSendNum + 1

                giftLeftPoint = 0

                if point >= levelTotalRemain then
                    levelRealIncrease = levelRealIncrease + levelRealRemain
                    giftLeftPoint = point - levelTotalRemain
                    levelRealRemain = 0
                    levelTotalRemain = 0
                else
                    local giftRealIncrease =  lume.round(point * levelData.gainRatio)
                    levelRealIncrease = levelRealIncrease + giftRealIncrease
                    levelRealRemain = levelRealRemain - giftRealIncrease
                    levelTotalRemain = levelRealRemain / levelData.gainRatio
                end
            end
            dailyFriendshipIncrease = dailyFriendshipIncrease + levelRealIncrease
        end
    end

    return validSendNum
end

FriendShipPresentCtrl._OnItemMinusClicked = HL.Method(HL.String, HL.Number, HL.Number) << function(self, itemId, luaIndex, count)
    local isMax = self:_IsMax()
    if isMax then
        return
    end

    local selectNum = self.m_selected[itemId]
    if selectNum and selectNum > 0 then
        self.m_selected[itemId] = math.max(self.m_selected[itemId] - count, 0)
        self.m_curSelectedNum = math.max(self.m_curSelectedNum - count, 0)

        if self.m_selected[itemId] == 0 then
            self:OnFilterChanged(self.m_selectedFilterTags)
        end
    end

    local cell = self:_GetCellByIndex(luaIndex)
    if cell then
        self:_RefreshSingleItemSelect(cell.listCellFriendshipUpgrade)
    end

    self:_RefreshSelectedFriendshipIncrease()
    self:_RefreshButtonState()
end

FriendShipPresentCtrl._SelectOnlyOneCell = HL.Method(HL.Number) << function(self, luaIndex)
    local isMax = self:_IsMax()
    if isMax then
        for index = 1, self.view.bottomList.scrollList.count do
            local cell = self:_GetCellByIndex(index)
            if cell then
                cell.listCellFriendshipUpgrade.selectNode.gameObject:SetActive(index == luaIndex)
            end
        end
    end
end

FriendShipPresentCtrl._GetCellByIndex = HL.Method(HL.Number).Return(HL.Any) << function(self, luaIndex)
    local cell
    if luaIndex > 0 then
        local object = self.view.bottomList.scrollList:Get(CSIndex(luaIndex))
        if object then
            cell = self.m_getGiftItemCell(object)
        end
    end
    return cell
end


FriendShipPresentCtrl._RefreshSingleItemSelect = HL.Method(HL.Table) << function(self, itemCell)
    local itemId = itemCell.item.id
    local selectNum = self.m_selected[itemId] or 0
    itemCell.multiSelectNode.gameObject:SetActive(selectNum > 0)
    itemCell.selectCount.text = tostring(selectNum)
    itemCell.btnMinus.gameObject:SetActive(selectNum > 0)
    itemCell.selectNode.gameObject:SetActive(selectNum > 0)
    itemCell.hotIcon.gameObject:SetActive(self:_GetIsPopularGift(itemId))
end

FriendShipPresentCtrl._RefreshTips = HL.Method(HL.Boolean, HL.Opt(HL.String)) << function(self, visible, itemId)
    local describeTips = self.view.bottomList.describeTips

    if visible ~= self.m_tipsLogicVisible then

        self.m_tipsLogicVisible = visible
        if visible then
            describeTips.animationWrapper:ClearTween(true)
            describeTips.gameObject:SetActive(visible)
            self.m_tipsItemId = itemId
        else
            describeTips.animationWrapper:PlayOutAnimation(function()
                describeTips.gameObject:SetActive(visible)
            end)
            self.m_tipsItemId = ""
        end
    else

        if visible and itemId ~= self.m_tipsItemId then
            describeTips.animationWrapper:Play("describetips_change")
            self.m_tipsItemId = itemId
        end
    end

    if self.m_itemTagCellCache == nil then
        self.m_itemTagCellCache = UIUtils.genCellCache(describeTips.collectionTagNode.tagCell)
    end

    if visible then
        local itemData = Tables.itemTable:GetValue(itemId)
        local giftData = Tables.giftItemTable:GetValue(itemId)

        local preferTagId = giftData.giftPreferTag
        local preferTagName = Tables.tagDataTable[preferTagId].tagName
        describeTips.areaTxt.text = preferTagName

        local areaIcon = self:_GetPreferTagIcon(preferTagId)
        if areaIcon and areaIcon ~= "" then
            describeTips.areaIcon:LoadSprite(UIConst.UI_SPRITE_SHIP, areaIcon)
        end

        local isPreferTagMatch = self:_GetCharPreferTagMatch(itemId)
        if isPreferTagMatch then

            describeTips.loveNode.gameObject:SetActive(true)
            describeTips.loveBg.gameObject:SetActive(true)
        else

            describeTips.loveNode.gameObject:SetActive(false)
            describeTips.loveBg.gameObject:SetActive(false)
        end

        local isPopular = self:_GetIsPopularGift(itemId)
        if isPopular then
            AudioManager.PostEvent("Au_UI_Toast_Fashion")
            describeTips.hotNode.gameObject:SetActive(true)
        else
            describeTips.hotNode.gameObject:SetActive(false)
        end

        local isShowLeftTime, expireTime = self:_GetIsShowPopularLeftTime(itemId)
        if isPopular and isShowLeftTime then
            describeTips.hotLeftTimeNode.gameObject:SetActive(true)
            describeTips.hotLeftTimeNode:StartTickLimitTime(expireTime, Const.POPULAR_EXPIRE_WARNING_TIME)
        else
            describeTips.hotLeftTimeNode.gameObject:SetActive(false)
        end

        local tagList = giftData.tagList
        self.m_itemTagCellCache:Refresh(#tagList, function(cell, index)
            local tagId = tagList[CSIndex(index)]
            local tagName = Tables.tagDataTable[tagId].tagName
            cell.nameTxt.text = tagName;
            if lume.find(self.m_charTagData.hobbyTagIds, tagId) ~= nil then
                cell.stateController:SetState("Conform")
            else
                cell.stateController:SetState("NotCompliant")
            end
        end)


        local iconId = itemData.iconId
        describeTips.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, iconId)

        describeTips.textName.text = itemData.name
        describeTips.textDesc.text = itemData.desc

        describeTips.buttonTips.onClick:RemoveAllListeners()
        describeTips.buttonTips.onClick:AddListener(function()
            self:Notify(MessageConst.SHOW_ITEM_TIPS, {
                transform = describeTips.buttonTips.transform,
                itemId = itemId,
            })
        end)
    end
end


FriendShipPresentCtrl._ClearSelected = HL.Method() << function(self)
    self.m_selected = {}
    self.m_curSelectedNum = 0
    self.m_curSelectedIncrease = 0
    self.m_curIncreaseInfo = {}


    for index = 1, self.view.bottomList.scrollList.count do
        local cell = self:_GetCellByIndex(index)
        if cell then
            self:_RefreshSingleItemSelect(cell.listCellFriendshipUpgrade)
        end
    end

    self:_RefreshSelectedFriendshipIncrease()
    self:_RefreshButtonState()
end


FriendShipPresentCtrl._RefreshButtonState = HL.Method() << function(self)
    if self:_IsMax() then
        return
    end

    local buttonSendStateController = self.view.bottomList.buttonSendState
    if self:_IsTodayMax() then
        buttonSendStateController:SetState("DisableState")
        self.view.bottomList.nodeClear.gameObject:SetActive(false)
        self.view.bottomList.emptyNode.gameObject:SetActive(false)
        return
    end

    if self.m_curSelectedNum <= 0 then
        self.view.bottomList.buttonSend.gameObject:SetActive(false)
        self.view.bottomList.nodeClear.gameObject:SetActive(false)

        self.view.bottomList.emptyNode.gameObject:SetActive(true)
    else
        self.view.bottomList.emptyNode.gameObject:SetActive(false)

        self.view.bottomList.buttonSend.gameObject:SetActive(true)
        self.view.bottomList.nodeClear.gameObject:SetActive(true)
        buttonSendStateController:SetState("NormalState")
    end

    if DeviceInfo.usingController then
        self:_RefreshInputKeyHint(self.m_focusItemId, self.m_focusLuaIndex)
    end
end

FriendShipPresentCtrl._SendPresentToChar = HL.Method() << function(self)
    if self.m_curSelectedNum <= 0 then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_SPACESHIP_GIFT_COUNT_NONE)
        return
    end

    local giftIds = {}
    local nums = {}
    local keys = lume.keys(self.m_selected)
    table.sort(keys)

    for _, giftId in ipairs(keys) do
        table.insert(giftIds, giftId)
        table.insert(nums, self.m_selected[giftId])
    end

    self.view.bottomList.buttonSendState:SetState("ActiveState")
    GameInstance.player.spaceship:SendGiftToChar(self.m_charId, giftIds, nums);
end

HL.Commit(FriendShipPresentCtrl)
