local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local ChatType = CS.Beyond.Gameplay.SNSFriendChatSystem.ChatType

SNSFriendDialogContentCell = HL.Class('SNSFriendDialogContentCell', UIWidgetBase)

SNSFriendDialogContentCell.m_curActiveGo = HL.Field(GameObject)

SNSFriendDialogContentCell.m_curActiveWeight = HL.Field(HL.Any)

SNSFriendDialogContentCell.m_widgetTextLeft = HL.Field(HL.Any)

SNSFriendDialogContentCell.m_widgetTextRight = HL.Field(HL.Any)

SNSFriendDialogContentCell.m_widgetEmotionLeft = HL.Field(HL.Any)

SNSFriendDialogContentCell.m_widgetEmotionRight = HL.Field(HL.Any)

SNSFriendDialogContentCell.m_widgetBlueprintLeft = HL.Field(HL.Any)

SNSFriendDialogContentCell.m_widgetBlueprintRight = HL.Field(HL.Any)

SNSFriendDialogContentCell.m_widgetSocialBuildingLeft = HL.Field(HL.Any)

SNSFriendDialogContentCell.m_widgetSocialBuildingRight = HL.Field(HL.Any)

SNSFriendDialogContentCell.m_showLeft = HL.Field(HL.Boolean) << false

SNSFriendDialogContentCell.m_message = HL.Field(HL.Any)

SNSFriendDialogContentCell.m_msgIndex = HL.Field(HL.Number) << -1

SNSFriendDialogContentCell.m_inAnimationType = HL.Field(HL.Any) << ""

SNSFriendDialogContentCell.InitSNSFriendDialogContentCell = HL.Method(HL.Number, HL.Any, HL.Any, HL.Function) << function(
    self, showRoleId, message, dialogContentNaviGroup, callback)
    if message.showTimeStamp then
        self.view.timeNode.gameObject:SetActive(true)
        self.view.rightIsFirstPlaceholder.gameObject:SetActive(true)
        self.view.leftIsFirstPlaceholder.gameObject:SetActive(true)
        self.view.rightPlayerHead.gameObject:SetActive(true)
        self.view.leftPlayerHead.gameObject:SetActive(true)
        self.view.timeNode.gameObject:SetActive(true)

        local showTimeStamp = message.showUseTimeStamp + Utils.getServerTimeZoneOffsetSeconds()
        if message.showUseTimeStamp < Utils.getTimestampNowYear1M1Day() then
            self.view.timeTxt.text = os.date("!" .. Language.LUA_CHAT_DATE_YEAR_MONTH_DAY_HOUR_MIN, showTimeStamp)
        elseif message.showUseTimeStamp < Utils.getTimestampToday0AM() then
            self.view.timeTxt.text = os.date("!" .. Language.LUA_CHAT_DATE_MONTH_DAY_HOUR_MIN, showTimeStamp)
        else
            self.view.timeTxt.text = os.date("!" .. Language.LUA_CHAT_DATE_HOUR_MIN, showTimeStamp)
        end
    else
        self.view.timeNode.gameObject:SetActive(false)
        self.view.rightIsFirstPlaceholder.gameObject:SetActive(false)
        self.view.leftIsFirstPlaceholder.gameObject:SetActive(false)
        self.view.rightPlayerHead.gameObject:SetActive(false)
        self.view.leftPlayerHead.gameObject:SetActive(false)
    end

    self:_CloseAllChildWidget()

    self.view.contentMyselfNode.gameObject:SetActive(false)
    self.view.contentOtherNode.gameObject:SetActive(false)
    self.m_message = message
    self.m_msgIndex = message.msgIndex
    if message.ownerId == GameInstance.player.roleId then

        self.m_showLeft = false
        self.view.contentMyselfNode.gameObject:SetActive(true)
        self.view.rightPlayerHead:UpdateHideLevelTxt(true)
        self.view.rightPlayerHead:UpdateHideSignature(true)
        self.view.rightPlayerHead:InitCommonPlayerHeadByRoleId(GameInstance.player.roleId, false)


        if message.type == ChatType.Text then
            if not self.m_widgetTextRight then
                self.m_widgetTextRight = self:_CreateWidget("SNSFriendChatTextRight", self.view.rightContent)
            end
            local success, info = GameInstance.player.friendChatSystem.m_uniq2chatText:TryGetValue(message.textId)
            local showText = ""
            if success then
                local haveText, tableData = Tables.friendChatTextTable:TryGetValue(info.textName)
                if haveText then
                    showText = tableData.messageText
                else
                    showText = Language.LUA_FRIEND_CHAT_INVALID_TEXT
                end
            else
                showText = Language.LUA_FRIEND_CHAT_INVALID_TEXT
            end

            self.m_widgetTextRight:InitSNSFriendChatTextRight(showText)
            self.m_curActiveGo = self.m_widgetTextRight.gameObject
            self.m_curActiveWeight = self.m_widgetTextRight
            self.m_inAnimationType = "normal"

        elseif message.type == ChatType.Emotion then
            if not self.m_widgetEmotionRight then
                self.m_widgetEmotionRight = self:_CreateWidget("SNSFriendChatEmotionRight", self.view.rightContent)
            end
            local success, info = GameInstance.player.friendChatSystem.m_uniq2chatEmotion:TryGetValue(message.emojiId)
            local showImg = ""
            if success then
                showImg = info.emotionImgPath
            end
            self.m_widgetEmotionRight:InitSNSFriendChatEmotionRight(showImg)
            self.m_curActiveGo = self.m_widgetEmotionRight.gameObject
            self.m_curActiveWeight = self.m_widgetEmotionRight
            self.m_inAnimationType = "normal"
        elseif message.type == ChatType.Blueprint then
            if not self.m_widgetBlueprintRight then
                self.m_widgetBlueprintRight = self:_CreateWidget("SNSFriendBlueprintItem", self.view.rightContent)
            end
            self.m_widgetBlueprintRight:InitSNSFriendBlueprintItem(message, dialogContentNaviGroup)
            self.m_curActiveGo = self.m_widgetBlueprintRight.gameObject
            self.m_curActiveWeight = self.m_widgetBlueprintRight
            self.m_inAnimationType = "sns_friendblueprintitem_leftin"
        elseif message.type == ChatType.SocialBuilding then
            if not self.m_widgetSocialBuildingRight then
                self.m_widgetSocialBuildingRight = self:_CreateWidget("SNSFriendSocialBuilding", self.view.rightContent)
            end
            self.m_widgetSocialBuildingRight:InitSNSFriendSocialBuilding(message, dialogContentNaviGroup)
            self.m_curActiveGo = self.m_widgetSocialBuildingRight.gameObject
            self.m_curActiveWeight = self.m_widgetSocialBuildingRight
            self.m_inAnimationType = "sns_friendsociabuilding_leftin"
        end
    else
        self.m_showLeft = true
        self.view.contentOtherNode.gameObject:SetActive(true)
        self.view.leftPlayerHead:UpdateHideLevelTxt(true)
        self.view.leftPlayerHead:UpdateHideSignature(true)
        self.view.leftPlayerHead:InitCommonPlayerHeadByRoleId(showRoleId, function()
            FriendUtils.FRIEND_CELL_HEAD_FUNC.BUSINESS_CARD_PHASE(message.ownerId).action()
        end)



        if message.type == ChatType.Text then
            if not self.m_widgetTextLeft then
                self.m_widgetTextLeft = self:_CreateWidget("SNSFriendChatTextLeft", self.view.leftContent)
            end
            local success, info = GameInstance.player.friendChatSystem.m_uniq2chatText:TryGetValue(message.textId)
            local showText = ""
            if success then
                local haveText, tableData = Tables.friendChatTextTable:TryGetValue(info.textName)
                if haveText then
                    showText = tableData.messageText
                else
                    showText = Language.LUA_FRIEND_CHAT_INVALID_TEXT
                end
            else
                showText = Language.LUA_FRIEND_CHAT_INVALID_TEXT
            end

            self.m_widgetTextLeft:InitSNSFriendChatTextLeft(showText)
            self.m_curActiveGo = self.m_widgetTextLeft.gameObject
            self.m_curActiveWeight = self.m_widgetTextLeft
            self.m_inAnimationType = "normal"
        elseif message.type == ChatType.Emotion then
            if not self.m_widgetEmotionLeft then
                self.m_widgetEmotionLeft = self:_CreateWidget("SNSFriendChatEmotionLeft", self.view.leftContent)
            end
            local success, info = GameInstance.player.friendChatSystem.m_uniq2chatEmotion:TryGetValue(message.emojiId)
            local showImg = ""
            if success then
                showImg = info.emotionImgPath
            end
            self.m_widgetEmotionLeft:InitSNSFriendChatEmotionLeft(showImg)
            self.m_curActiveGo = self.m_widgetEmotionLeft.gameObject
            self.m_curActiveWeight = self.m_widgetEmotionLeft
            self.m_inAnimationType = "normal"
        elseif message.type == ChatType.Blueprint then
            if not self.m_widgetBlueprintLeft then
                self.m_widgetBlueprintLeft = self:_CreateWidget("SNSFriendBlueprintItem", self.view.leftContent)
            end
            self.m_widgetBlueprintLeft:InitSNSFriendBlueprintItem(message, dialogContentNaviGroup)
            self.m_curActiveGo = self.m_widgetBlueprintLeft.gameObject
            self.m_curActiveWeight = self.m_widgetBlueprintLeft
            self.m_inAnimationType = "sns_friendblueprintitem_in"
        elseif message.type == ChatType.SocialBuilding then
            if not self.m_widgetSocialBuildingLeft then
                self.m_widgetSocialBuildingLeft = self:_CreateWidget("SNSFriendSocialBuilding", self.view.leftContent)
            end
            self.m_widgetSocialBuildingLeft:InitSNSFriendSocialBuilding(message, dialogContentNaviGroup)
            self.m_curActiveGo = self.m_widgetSocialBuildingLeft.gameObject
            self.m_curActiveWeight = self.m_widgetSocialBuildingLeft
            self.m_inAnimationType = "sns_friendsociabuilding_in"
        end
    end

    if self.m_curActiveGo then
        self.m_curActiveGo:SetActiveIfNecessary(true)
    end
    if callback then
        callback()
    end
end


SNSFriendDialogContentCell.UpdateInfoFromServer = HL.Method(HL.Any)<< function(self, showRoleId)
    if showRoleId == nil then
        return
    end

    local msg = self.m_message
    if msg.type == ChatType.Blueprint or msg.type == ChatType.SocialBuilding then
        GameInstance.player.friendChatSystem:QueryAdd(showRoleId, msg.msgIndex, msg.type, msg.bpSharedCode, msg.sbCreatorId)
    end
end


SNSFriendDialogContentCell.CheckCanJumpIn = HL.Method().Return(HL.Boolean)<< function(self)
    if not self.m_curActiveWeight then
        return false
    end
    if self.m_message.type == ChatType.Blueprint then
        return self.m_curActiveWeight:CheckCanJumpIn()
    elseif self.m_message.type == ChatType.SocialBuilding then
        return self.m_curActiveWeight:CheckCanJumpIn()
    end
    return false
end


SNSFriendDialogContentCell.SetTargetNode = HL.Method()<< function(self)
    if not self.m_curActiveWeight then
        return
    end
    self.m_curActiveWeight:SetTargetNode()
end

SNSFriendDialogContentCell.PlayInAnimation = HL.Method()<< function(self)
    if not self.m_curActiveWeight then
        return
    end
    if self.m_curActiveWeight.view.animationWrapper then
        if self.m_inAnimationType == "normal" then
            self.m_curActiveWeight.view.animationWrapper:PlayInAnimation()
        else
            self.m_curActiveWeight.view.animationWrapper:PlayWithTween(self.m_inAnimationType)
        end
    end
end

SNSFriendDialogContentCell.UpdateDataShowInfo = HL.Method()<< function(self)
    if not self.m_curActiveWeight then
        return
    end
    if self.m_message.type == ChatType.Blueprint then
        self.m_curActiveWeight:UpdateBluePrintShow(self.m_message)
    elseif self.m_message.type == ChatType.SocialBuilding then
        self.m_curActiveWeight:UpdateSocialBuildingShow(self.m_message)
    end
end


SNSFriendDialogContentCell._CloseAllChildWidget = HL.Method()<< function(self)
    self:_CloseOneChildWidget(self.m_curActiveGo, true)
    self:_CloseOneChildWidget(self.m_widgetTextLeft, false)
    self:_CloseOneChildWidget(self.m_widgetTextRight, false)
    self:_CloseOneChildWidget(self.m_widgetEmotionLeft, false)
    self:_CloseOneChildWidget(self.m_widgetEmotionRight, false)
    self:_CloseOneChildWidget(self.m_widgetBlueprintLeft, false)
    self:_CloseOneChildWidget(self.m_widgetBlueprintRight, false)
    self:_CloseOneChildWidget(self.m_widgetSocialBuildingLeft, false)
    self:_CloseOneChildWidget(self.m_widgetSocialBuildingRight, false)
end


SNSFriendDialogContentCell._CloseOneChildWidget = HL.Method(HL.Any, HL.Boolean)<< function(self, widget, isObj)
    if widget then
        if isObj then
            widget:SetActiveIfNecessary(false)
        else
            widget.gameObject:SetActiveIfNecessary(false)
        end
    end
end


SNSFriendDialogContentCell._CreateWidget = HL.Method(HL.String, HL.Any).Return(HL.Any)
    << function(self, widgetName, parentNode)
    local go = self:_CreateGameObject(widgetName, parentNode)
    return Utils.wrapLuaNode(go)
end

SNSFriendDialogContentCell._CreateGameObject = HL.Method(HL.String, HL.Any).Return(GameObject)
    << function(self, widgetName, parentNode)
    local path = string.format(UIConst.UI_SNS_FRIEND_CHAT_WIDGETS_PATH, widgetName)
    local goAsset = self:LoadGameObject(path)
    local go = CSUtils.CreateObject(goAsset, parentNode)
    go.transform.localScale = Vector3.one
    go.transform.localPosition = Vector3.zero
    return go
end


HL.Commit(SNSFriendDialogContentCell)
return SNSFriendDialogContentCell

