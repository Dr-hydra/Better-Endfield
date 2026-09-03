local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FriendshipNode = HL.Class('FriendshipNode', UIWidgetBase)

FriendshipNode.m_charInstId = HL.Field(HL.Number) << 0

FriendshipNode.m_friendshipValue = HL.Field(HL.Number) << 0



FriendshipNode._OnFirstTimeInit = HL.Override() << function(self)
    self.view.reliabilityTips.gameObject:SetActive(false)
    self.view.button.onClick:AddListener(function()
        self.view.reliabilityTips.gameObject:SetActive(true)
    end)
    self.view.reliabilityTips.controllerBtn.onClick:AddListener(function()
        self.view.reliabilityTips.gameObject:SetActive(false)
    end)
    self:RegisterMessage(MessageConst.ON_CHAR_FRIENDSHIP_CHANGED, function()
        self:_RefreshFriendship()
    end)
    self:RegisterMessage(MessageConst.CHAR_INFO_SHOW_FRIENDSHIP_TIPS, function()
        self.view.reliabilityTips.gameObject:SetActive(true)
    end)
    if self.view.button.enableControllerNavi then
        self.view.button.onIsNaviTargetChanged = function(isNaviTarget)
            if not isNaviTarget then
                self.view.reliabilityTips.gameObject:SetActive(false)
            end
        end
    end
end

FriendshipNode.InitFriendshipNode = HL.Method(HL.Number) << function(self, charInstId)
    self.m_charInstId = charInstId
    self:_FirstTimeInit()
    self.view.reliabilityTips.gameObject:SetActive(false)

    local isCardInTrail = CharInfoUtils.checkIsCardInTrail(charInstId)
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local isEndmin = CharInfoUtils.isEndmin(charInst.templateId)

    local isShowEmpty = isCardInTrail or isEndmin
    self.view.empty.gameObject:SetActive(isShowEmpty)
    self.view.normal.gameObject:SetActive(not isShowEmpty)
    self.view.button.enabled = not isShowEmpty
    if isShowEmpty then
        return
    end

    self:_RefreshFriendship()
end

FriendshipNode.InitFriendshipNodeByFriendShipValue = HL.Method(HL.Number) << function(self, friendshipValue)
    self.m_friendshipValue = friendshipValue
    self:_FirstTimeInit()
    self.view.reliabilityTips.gameObject:SetActive(false)

    local isShowEmpty = false
    self.view.empty.gameObject:SetActive(isShowEmpty)
    self.view.normal.gameObject:SetActive(not isShowEmpty)
    self.view.button.enabled = not isShowEmpty
    if isShowEmpty then
        return
    end

    self:_RefreshFriendshipByValue()
end

FriendshipNode._RefreshFriendship = HL.Method() << function(self)
    if self.m_charInstId == 0 then
        return
    end
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(self.m_charInstId)
    local friendshipValue = CSPlayerDataUtil.GetCharFriendshipByInstId(charInst.instId)
    local maxFriendship = CSPlayerDataUtil.maxFriendship
    local rate = friendshipValue / maxFriendship

    self.view.percentText.text = string.format("%.0f%%", CharInfoUtils.getCharRelationShowValue(friendshipValue))
    self.view.fill.fillAmount = rate
end

FriendshipNode._RefreshFriendshipByValue = HL.Method() << function(self)
    local maxFriendship = CSPlayerDataUtil.maxFriendship
    local rate = self.m_friendshipValue / maxFriendship

    self.view.percentText.text = string.format("%.0f%%", CharInfoUtils.getCharRelationShowValue(self.m_friendshipValue))
    self.view.fill.fillAmount = rate
end

HL.Commit(FriendshipNode)
return FriendshipNode

