local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SocializeFriendName = HL.Class('SocializeFriendName', UIWidgetBase)

SocializeFriendName.m_friendRoleId = HL.Field(HL.Number) << 0


SocializeFriendName._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_FRIEND_CELL_INFO_CHANGE, function()
        self:InitSocializeFriendName(self.m_friendRoleId)
    end)
end



SocializeFriendName.InitSocializeFriendName = HL.Method(HL.Number) << function(self, friendRoleId)
    self:_FirstTimeInit()
    local success, playerInfo = GameInstance.player.friendSystem:TryGetFriendInfo(friendRoleId)
    if not success then
        return
    end
    self.m_friendRoleId = friendRoleId
    local nameStr, avatarPath, avatarFramePath = FriendUtils.getFriendInfoByRoleId(friendRoleId,nil, true)
    self.view.nameTxt.text = nameStr
    self.view.friendInfoBtn.onClick:RemoveAllListeners()
    self.view.friendInfoBtn.onClick:AddListener(function()
        Notify(MessageConst.ON_OPEN_BUSINESS_CARD_PREVIEW, { roleId = friendRoleId, isPhase = true })
    end)
    self.view.playHeadImage:LoadSprite(avatarPath)
end

HL.Commit(SocializeFriendName)
return SocializeFriendName

