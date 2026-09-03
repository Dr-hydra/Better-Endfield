local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ReliabilityCell = HL.Class('ReliabilityCell', UIWidgetBase)

ReliabilityCell.m_charId = HL.Field(HL.String) << ""

ReliabilityCell.m_deltaCor = HL.Field(HL.Thread)

ReliabilityCell.m_sendTipCellCache = HL.Field(HL.Forward("UIListCache"))

ReliabilityCell.m_successCor = HL.Field(HL.Thread)


ReliabilityCell._OnFirstTimeInit = HL.Override() << function(self)
    self.m_sendTipCellCache = UIUtils.genCellCache(self.view.sendTips)
    self.view.friendshipRoot.circleCur.fillAmount = 0
end

ReliabilityCell.InitReliabilityCell = HL.Method(HL.String) << function(self, charId)
    self:_FirstTimeInit()
    self.m_charId = charId
    self:RefreshAll()
end

ReliabilityCell.RefreshAll = HL.Method() << function(self)
    self:_RefreshFriendship(true)
    self:_RefreshText()
    self:RefreshTmpFriendship(0)
    self:RefreshSSCharStamina()
end

ReliabilityCell.RefreshSSCharStamina = HL.Method() << function(self)
    SpaceshipUtils.updateSSCharStamina(self.view.detailsNode, self.m_charId)
end


ReliabilityCell._StartDeltaCor = HL.Method(HL.Number) << function(self, deltaFav)
    self:_ClearDeltaCor()
    local curLevel = CSPlayerDataUtil.GetFriendshipLevelByChar(self.m_charId)
    local lastFav = CSPlayerDataUtil.GetCharFriendship(self.m_charId) - deltaFav
    local lastLevel = CSPlayerDataUtil.GetFriendshipLevel(lastFav)
    local tips = self.view.tips
    tips.gameObject:SetActive(true)
    if curLevel ~= lastLevel then
        tips.textTip.gameObject:SetActive(true)
        tips.numTip.gameObject:SetActive(false)
    else
        tips.textTip.gameObject:SetActive(false)
        tips.numTip.gameObject:SetActive(true)
        tips.text.text = string.format("+ %d", deltaFav)
    end

    self.m_deltaCor = self:_StartCoroutine(function()
        coroutine.wait(1)
        tips.gameObject:SetActive(false)
        self:_ClearDeltaCor()
    end)
end

ReliabilityCell._ClearDeltaCor = HL.Method() << function(self)
    if self.m_deltaCor ~= nil then
        self:_ClearCoroutine(self.m_deltaCor)
    end
    self.m_deltaCor = nil
end


ReliabilityCell.RefreshDeltaFav = HL.Method(HL.Number) << function(self, deltaFav)


    self:_RefreshFriendship()
end

ReliabilityCell._RefreshFriendship = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    isInit = isInit or false
    local percent, realPercent = self:_GetCurFriendshipPercent()
    local level = CSPlayerDataUtil.GetFriendshipLevelByChar(self.m_charId)
    local maxLevel = CSPlayerDataUtil.maxFriendshipLevel

    local friendshipRoot = self.view.friendshipRoot
    local config = self.view.config
    local isMax = level == maxLevel

    friendshipRoot.circleCur:DOFillAmount(isMax and 1.0 or realPercent / 100, 0.5)

    friendshipRoot.friendShipHale.gameObject:SetActive(isMax)
    friendshipRoot.circleHalo.gameObject:SetActive(isMax)

    friendshipRoot.bgBlur.color = config[string.format("BG_BLUR_COLOR_%d", level)]
    friendshipRoot.friendShipIcon.color = config[string.format("FRIENDSHIP_ICON_COLOR_%d", level)]
    friendshipRoot.circleCur.color = config[string.format("CIRCLE_CUR_COLOR_%d", level)]
    friendshipRoot.circleTmpStart.color = config[string.format("CIRCLE_TMP_START_COLOR_%d", level)]
    friendshipRoot.decoTexture2.gameObject:SetActive(level == 2)
    friendshipRoot.decoTexture3.gameObject:SetActive(level == 3)
end

ReliabilityCell.RefreshTmpFriendship = HL.Method(HL.Number) << function(self, deltaTmpNum)
    if deltaTmpNum <= 0 then
        self.view.friendshipRoot.circleTmpStart.gameObject:SetActive(false)
        self.view.friendshipRoot.circleTmpEnd.gameObject:SetActive(false)
    else
        self.view.friendshipRoot.circleTmpStart.gameObject:SetActive(true)
        self.view.friendshipRoot.circleTmpEnd.gameObject:SetActive(true)
        local percent, realPercent = self:_GetCurFriendshipPercent()
        realPercent = realPercent / 100

        local curLevel = CSPlayerDataUtil.GetFriendshipLevelByChar(self.m_charId)
        local needFriendShip = CSPlayerDataUtil.favoriteLevelMap[curLevel]
        local tmpPercent = deltaTmpNum / needFriendShip

        if (realPercent + tmpPercent > 1) then
            tmpPercent = 1 - realPercent
        end

        self.view.friendshipRoot.circleTmpStart.fillAmount = tmpPercent

        local startRot = Vector3.zero
        startRot.z = -realPercent * 360
        self.view.friendshipRoot.circleTmpStart.transform.localEulerAngles = startRot

        local endRot = Vector3.zero
        endRot.z = -(realPercent + tmpPercent) * 360
        self.view.friendshipRoot.circleTmpEnd.transform.localEulerAngles = endRot
    end
end

ReliabilityCell._RefreshText = HL.Method() << function(self)
    local characterData = CharInfoUtils.getCharTableData(self.m_charId)
    self.view.textRoot.textName.text = string.format(Language.LUA_SPACESHIP_CHAR_FRIENDSHIP_NAME_FORMAT, characterData.name)
    local friendshipValue = CSPlayerDataUtil.GetCharFriendship(self.m_charId)
    self.view.textRoot.textCur.text = string.format("%.0f%%", CharInfoUtils.getCharRelationShowValue(friendshipValue))
    self.view.textRoot.textMax.text = "/100%"
end

ReliabilityCell._GetCurFriendshipPercent = HL.Method().Return(HL.Number, HL.Number) << function(self)
    local curLevel = CSPlayerDataUtil.GetFriendshipLevelByChar(self.m_charId)
    local maxLevel = CSPlayerDataUtil.maxFriendshipLevel

    local realPercent = 0
    local percent = (curLevel - 1) * 100

    if curLevel ~= maxLevel then
        local lastFriendShip

        if curLevel == 1 then
            lastFriendShip = 0
        else
            lastFriendShip = CSPlayerDataUtil.favoriteLevelMap[curLevel - 1]
        end
        local curFriendShip = CSPlayerDataUtil.GetCharFriendship(self.m_charId)
        local needFriendShip = CSPlayerDataUtil.favoriteLevelMap[curLevel]

        realPercent =  lume.round((curFriendShip - lastFriendShip) * 100 / needFriendShip)
        percent = percent + realPercent
    end

    return percent, realPercent
end

ReliabilityCell.ShowPresentSuccessTips = HL.Method(HL.Boolean, HL.Number, HL.Table) << function(self, levelChanged, deltaFav, selectedItems)
    self:_ClearSuccessCor()
    self:_StartSuccessCor(deltaFav, selectedItems)
    if levelChanged then
       self.view.friendshipRoot.animationWrapper:PlayWithTween("friendshipsend_up")
    else
        self.view.friendshipRoot.animationWrapper:PlayWithTween("friendshipsend_feedback")
    end
end


ReliabilityCell._RefreshSingleSuccessTips = HL.Method(HL.Table, HL.Opt(HL.String, HL.Number)) << function(self, cell, itemId, num)
    local itemData = Tables.itemTable:GetValue(itemId)
    local iconId = itemData.iconId
    cell.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, iconId)

    cell.textName.text = itemData.name
    cell.textNum.text = string.format("x %d", num)
    cell.gameObject:SetActive(false)
end


ReliabilityCell._StartSuccessCor = HL.Method(HL.Number, HL.Table) << function(self, deltaFav, selectedItems)
    local items = {}
    for itemId, num in pairs(selectedItems) do
        if num > 0 then
            table.insert(items, {
                itemId = itemId,
                num = num,
            })
        end
    end

    self.m_sendTipCellCache:Refresh(#items, function(cell, index)
        local data = items[index]
        self:_RefreshSingleSuccessTips(cell, data.itemId, data.num)
    end)

    self.m_successCor = self:_StartCoroutine(function()
        self:RefreshDeltaFav(deltaFav)
        for index = 1, #items do
            local cell = self.m_sendTipCellCache:Get(index)
            cell.gameObject:SetActive(true)
            cell.animationWrapper:PlayInAnimation(function()
                cell.animationWrapper:PlayOutAnimation()
            end)
            coroutine.wait(0.6)
        end
    end)
end

ReliabilityCell._ClearSuccessCor = HL.Method() << function(self)
    if self.m_successCor ~= nil then
        self:_ClearCoroutine(self.m_successCor)
    end
    self.m_successCor = nil
    self.m_sendTipCellCache:Refresh(0)
end

HL.Commit(ReliabilityCell)
return ReliabilityCell

