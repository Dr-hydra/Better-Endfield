
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FriendBusinessCardRoot

FriendBusinessCardRootCtrl = HL.Class('FriendBusinessCardRootCtrl', uiCtrl.UICtrl)






FriendBusinessCardRootCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_FRIEND_BUSINESS_INFO_CHANGE] = 'OnFriendBusinessInfoChange',
    [MessageConst.ON_BIRTHDAY_VISIBILITY_REFRESH] = 'OnBirthdayRefresh',
    [MessageConst.ON_BIRTHDAY_SET] = 'OnBirthdayRefresh'
}

FriendBusinessCardRootCtrl.m_roleId = HL.Field(HL.Number) << 0

FriendBusinessCardRootCtrl.m_panel = HL.Field(HL.Userdata)

FriendBusinessCardRootCtrl.m_businessCard = HL.Field(HL.Forward('FriendBusinessCard'))

FriendBusinessCardRootCtrl.m_businessCardId = HL.Field(HL.String) << ''

FriendBusinessCardRootCtrl.m_isPlayInAnimationInFrame = HL.Field(HL.Boolean) << false



FriendBusinessCardRootCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    if arg and arg.roleId then
        self.m_roleId = arg.roleId
    else
        self.m_roleId = GameInstance.player.roleId
    end

    self:_UpdateCardInfo()

end

FriendBusinessCardRootCtrl.OnPhaseRefresh = HL.Override(HL.Any) << function(self, args)

end

FriendBusinessCardRootCtrl._UpdateCardInfo = HL.Method() << function(self)
    local success , friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(self.m_roleId)
    if not success then
        logger.error('FriendBusinessCardRootCtrl.OnCreate: Failed to get friend info for roleId: ' .. self.m_roleId)

        friendInfo = GameInstance.player.friendSystem.SelfInfo
    end


    local businessCardId = friendInfo.businessCardTopicId

    if self.m_businessCardId == businessCardId then

        self.m_businessCard:InitFriendBusinessCard(self.m_roleId)
        self:_TrySetDefaultNaviTarget()
        return
    end

    if self.m_panel then
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({
        })
        CSUtils.ClearUIComponents(self.m_panel)
        GameObject.DestroyImmediate(self.m_panel)
        self.m_phase:SetTabBlockState(false)
    end
    self.m_businessCardId = businessCardId

    local success, cfg = Tables.businessCardTopicTable:TryGetValue(businessCardId)

    if not success then
        logger.error('FriendBusinessCardRootCtrl.OnCreate: Failed to get business card config for id: ' .. businessCardId)
        return
    end

    local path = string.format(UIConst.UI_BUSINESS_CARD_PREFAB_PATH , cfg.panelPrefab)
    local prefab = self:LoadGameObject(path)

    self.m_panel = CSUtils.CreateObject(prefab, self.view.cardRoot)

    self.m_businessCard = Utils.wrapLuaNode(self.m_panel)
    self.m_businessCard:InitFriendBusinessCard(self.m_roleId)
    self.m_businessCard.view.rightBottomBtn.onIsFocusedChange:RemoveAllListeners()
    self.m_businessCard.view.rightBottomBtn.onIsFocusedChange:AddListener(function(isFocused)
        self.m_phase:SetTabBlockState(isFocused)
    end)
    self.animationWrapper = self.m_businessCard.view.animationWrapper
    self.m_businessCard.view.animationWrapper:PlayInAnimation()
    self.m_isPlayInAnimationInFrame = true
    CoroutineManager:StartCoroutine(function()
        coroutine.step()
        self.m_isPlayInAnimationInFrame = false
    end)
    if self.m_phase then
        self.m_phase:_BindControllerHintPlaceHolder()
    end
    self:_TrySetDefaultNaviTarget()
end

FriendBusinessCardRootCtrl._TrySetDefaultNaviTarget = HL.Method() << function(self)
    if self.m_businessCard == nil or self.m_businessCard.view == nil then
        return
    end
    if self.view == nil or self.view.inputGroup == nil then
        return
    end
    if not self.view.inputGroup.groupEnabled then
        return
    end

    if self.m_businessCard.m_isExpanded then
        self.m_businessCard.view.businessCardRoleNode:NaviToFirstChar()
    end
end

FriendBusinessCardRootCtrl.RefreshTabBlockState = HL.Method() << function(self)
    if self.m_phase == nil then
        return
    end

    local shouldBlock = false
    if self.m_businessCard ~= nil and self.m_businessCard.view ~= nil and self.m_businessCard.view.rightBottomBtn ~= nil then
        shouldBlock = InputManagerInst.controllerNaviManager:IsTopLayer(self.m_businessCard.view.rightBottomBtn)
    end
    self.m_phase:SetTabBlockState(shouldBlock)
end

FriendBusinessCardRootCtrl.OnFriendBusinessInfoChange = HL.Method() << function(self)
    self:_UpdateCardInfo()
end

FriendBusinessCardRootCtrl.OnShow = HL.Override() << function(self)
    self.m_businessCard:InitFriendBusinessCard(self.m_roleId)
    self:_TrySetDefaultNaviTarget()
    self:RefreshTabBlockState()
    if not self.m_isPlayInAnimationInFrame then
        self.m_businessCard.view.animationWrapper:PlayInAnimation()
        self.m_isPlayInAnimationInFrame = true
        CoroutineManager:StartCoroutine(function()
            coroutine.step()
            self.m_isPlayInAnimationInFrame = false
        end)
    end
end

FriendBusinessCardRootCtrl.OnBirthdayRefresh = HL.Method() << function(self)
    if self.m_roleId ~= GameInstance.player.roleId then
        return
    end
    self.m_businessCard:RefreshBirthdayInfo()
end









HL.Commit(FriendBusinessCardRootCtrl)
