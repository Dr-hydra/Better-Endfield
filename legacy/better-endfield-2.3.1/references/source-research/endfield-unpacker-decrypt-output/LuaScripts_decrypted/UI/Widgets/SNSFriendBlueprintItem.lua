local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SNSFriendBlueprintItem = HL.Class('SNSFriendBlueprintItem', UIWidgetBase)

SNSFriendBlueprintItem.m_getTagCellFunc = HL.Field(HL.Function)

SNSFriendBlueprintItem.m_bpSharedCode = HL.Field(HL.String) << ""

SNSFriendBlueprintItem.m_message = HL.Field(HL.Any) << nil

SNSFriendBlueprintItem.curState = HL.Field(HL.Any) << nil

SNSFriendBlueprintItem.m_isQueryingBP = HL.Field(HL.Boolean) << false

local BlueprintShowState = {
    Normal = "Normal",
    Saved = "Saved",
    Loading = "Loading",
    InValid = "InValid",
}
local ICON_PATH = "ItemIcon/"

SNSFriendBlueprintItem.InitSNSFriendBlueprintItem = HL.Method(HL.Any, HL.Any) << function(self, message, dialogContentNaviGroup)
    self.curState = nil
    self.view.normalButton.onClick:RemoveAllListeners()
    self.view.normalButton.onClick:AddListener(function()
        if not GameInstance.player.systemUnlockManager:IsSystemUnlockByType(GEnums.UnlockSystemType.FacBlueprint) then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAT_JUMP_UNLOCK_BLUEPRINT_TOAST)
            return
        end
        if self.m_bpSharedCode ~= nil and not string.isEmpty(self.m_bpSharedCode) then
            self.m_isQueryingBP = true
            GameInstance.player.remoteFactory.blueprint:SendQuerySharedBlueprint(self.m_bpSharedCode)
        end
    end)

    self:RegisterMessage(MessageConst.FAC_ON_QUERY_SHARED_BLUEPRINT,function(arg)
        if self.m_isQueryingBP then
            self.m_isQueryingBP = false
            Notify(MessageConst.FAC_ON_UPDATE_FRIEND_CHAT_STATE, true)
            UIManager:Open(PanelId.FacSaveBlueprint, {
                bpInst = unpack(arg),
                isSharing = false,
                isEditing = false,
                isImporting = true,
                id = -1,
                shareCode = self.m_bpSharedCode,
                fromFriend = true,
            })
        end
    end)

    self.view.emptyButton.onClick:RemoveAllListeners()
    self.view.emptyButton.onClick:AddListener(function()
        if self.curState == BlueprintShowState.InValid then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_CHAT_BLUEPRINT_INVALID)
        elseif self.curState == BlueprintShowState.Loading then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_CHAT_BLUEPRINT_LOADING)
        end
    end)

    self:UpdateBluePrintShow(message)
end


SNSFriendBlueprintItem.UpdateBluePrintShow = HL.Method(HL.Any) << function(self, message)
    self.m_message = message
    if self.m_message.bpName == nil then
        self:SetState(BlueprintShowState.Loading)
    else
        if self.m_message.bpStatus == nil then
            self:SetState(BlueprintShowState.Loading)
        elseif self.m_message.bpStatus == CS.Proto.BRIEF_BP_STATUS.None then
            self:SetState(BlueprintShowState.Normal)
        elseif self.m_message.bpStatus == CS.Proto.BRIEF_BP_STATUS.Exist then
            self:SetState(BlueprintShowState.Saved)
        elseif self.m_message.bpStatus == CS.Proto.BRIEF_BP_STATUS.Invalid then
            self:SetState(BlueprintShowState.InValid)
        end
    end

    if self.m_message.bpStatus == CS.Proto.BRIEF_BP_STATUS.Invalid then
        return
    end

    self.m_bpSharedCode = self.m_message.bpSharedCode
    if self.m_message.bpIcon ~= nil then
        if #self.m_message.bpIcon > 0 then
            self.view.blueprintIcon:InitBlueprintIcon(self.m_message.bpIcon, self.m_message.bpBaseColor)
        end
    end

    if self.m_message.bpName ~= nil then
        if self.m_message.shareIdx == 0 then
            self.view.titleTxt.text = GameInstance.player.friendChatSystem:GetBluePrintNameByUid(self.m_message.bpUid)
        else
            self.view.titleTxt.text = self.m_message.bpName
        end
    end

    self.m_getTagCellFunc = UIUtils.genCachedCellFunction(self.view.tagScrollList)
    self.view.tagScrollList.onUpdateCell:AddListener(function(gameObject, csIndex)
        local cell = self.m_getTagCellFunc(gameObject)
        if self.m_message.bpTags ~= nil then
            local tagId = self.m_message.bpTags[csIndex]
            local hasTag, tagData = Tables.factoryBlueprintTagTable:TryGetValue(tagId)
            if hasTag then
                cell.tagTxt.text = tagData.name
            else
                cell.tagTxt.text = Language.LUA_FRIEND_SHARE_PANEL_LOADING_NAME
            end
        else
            cell.tagTxt.text = Language.LUA_FRIEND_SHARE_PANEL_LOADING_NAME
        end
    end)

    if self.m_message.bpTags == nil then
        self.view.tagScrollList:UpdateCount(0)
    else
        self.view.tagScrollList:UpdateCount(self.m_message.bpTags.Count)
    end
end


SNSFriendBlueprintItem.CheckCanJumpIn = HL.Method().Return(HL.Boolean)<< function(self)
    if not self.m_message then
        return false
    end

    if self.curState == BlueprintShowState.Normal
        or self.curState == BlueprintShowState.Saved
        or self.curState == BlueprintShowState.InValid then
        return true
    end

    return false
end

SNSFriendBlueprintItem.SetTargetNode = HL.Method() << function(self)
    self:SetNaviTarget(self.view.nodeNaviDeco)
end

SNSFriendBlueprintItem.SetState = HL.Method(HL.String) << function(self, state)
    self.curState = state
    self.view.stateController:SetState(state)
end

HL.Commit(SNSFriendBlueprintItem)
return SNSFriendBlueprintItem

