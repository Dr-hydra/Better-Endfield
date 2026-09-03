local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

BusinessCardRegionalNode = HL.Class('RegionalNode', UIWidgetBase)


BusinessCardRegionalNode._OnFirstTimeInit = HL.Override() << function(self)

end

BusinessCardRegionalNode.InitBusinessCardRegionalNodeByRoleId = HL.Method(HL.Number) << function(self, roleId)
    self:_FirstTimeInit()
    local _, friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)

    self:InitBusinessCardRegionalNode(friendInfo.domainInfos)
end

BusinessCardRegionalNode.InitBusinessCardRegionalNode = HL.Method(HL.Userdata) << function(self, domainInfos)
    self:_FirstTimeInit()

    if domainInfos == nil then
        return
    end

    if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.DomainDevelopment) then
        for i = 1, self.config.DOMAIN_COUNT do
            self.view['domain_' .. i].gameObject:SetActiveIfNecessary(false)
        end
        return
    end



    for i = 1, self.config.DOMAIN_COUNT do
        self.view['domain_' .. i].gameObject:SetActiveIfNecessary(i <= domainInfos.Count)
    end

    for i = 1, domainInfos.Count do
        local domainInfo = domainInfos[i - 1]
        self.view[domainInfo.DomainId..'_text'].text = domainInfo.Level
        local domainCfg = Tables.domainDataTable:GetValue(domainInfo.DomainId)
        self.view[domainInfo.DomainId..'_nameText'].text = domainCfg.domainName
        self.view[domainInfo.DomainId..'_color'].color = UIUtils.getColorByString(domainCfg.domainColor)
    end

end

HL.Commit(BusinessCardRegionalNode)
return BusinessCardRegionalNode

