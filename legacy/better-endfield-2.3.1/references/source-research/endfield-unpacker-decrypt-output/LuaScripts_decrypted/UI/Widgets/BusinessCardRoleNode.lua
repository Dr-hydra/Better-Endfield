local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

BusinessCardRoleNode = HL.Class('BusinessCardRoleNode', UIWidgetBase)

BusinessCardRoleNode.m_charInstanceIdList = HL.Field(HL.Table)

BusinessCardRoleNode.m_isPreview = HL.Field(HL.Boolean) << false

BusinessCardRoleNode.m_firstCharIsEmpty = HL.Field(HL.Boolean) << true

BusinessCardRoleNode.m_roleId = HL.Field(HL.Number) << 0


BusinessCardRoleNode._OnFirstTimeInit = HL.Override() << function(self)
    self.view.roleOnwardsBtn.onClick:RemoveAllListeners()
    self.view.roleOnwardsBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.FriendRoleDisplay)
    end)
end

BusinessCardRoleNode.InitBusinessCardRoleNode = HL.Method(HL.Number, HL.Boolean) << function(self, roleId, preview)
    self:_FirstTimeInit()

    self.m_charInstanceIdList = {}

    local roleCharCount = 4
    local list = GameInstance.player.friendSystem:GetCharInfoByRoleId(roleId)
    self.m_isPreview = preview or false
    self.m_roleId = roleId

    self.view.roleLayout.enabled = not (self.m_isPreview and self.m_roleId == GameInstance.player.friendSystem.SelfInfo.roleId)

    for i = 1, roleCharCount do
        self.view['friendBusinessCardRoleCell' .. i].aniRoot:SetState(CSIndex(i) < list.Count and "Role" or preview and "OtherEmpty" or "SelfEmpty")
        self.view['friendBusinessCardRoleCell' .. i].addBtn.onClick:RemoveAllListeners()
        self.view['friendBusinessCardRoleCell' .. i].addBtn.onClick:AddListener(function()
            if not self.m_isPreview then
                UIManager:Open(PanelId.FriendRoleDisplay)
            end
        end)




        self.view['friendBusinessCardRoleCell' .. i].addBtn.customBindingViewLabelText = preview and Language.LUA_FRIEND_BUSINESS_EDIT_CHAR_PREVIEW or Language.LUA_FRIEND_BUSINESS_EDIT_CHAR
        if CSIndex(i) < list.Count and list[CSIndex(i)] ~= nil then
            local charConfig = Tables.characterTable:GetValue(list[CSIndex(i)].templateId)
            local args = {
                templateId = list[CSIndex(i)].templateId,
                instId = list[CSIndex(i)].instId,
                level = list[CSIndex(i)].level,
                ownTime = 0,
                rarity = charConfig.rarity,
                potentialLevel = list[CSIndex(i)].potentialLevel,
                noHpBar = true,
                singleSelect = false,
                selectIndex = -1,
            }
            self.m_charInstanceIdList[i] = args


            self.view['friendBusinessCardRoleCell' .. i].charHeadCell:InitCharFormationHeadCell(args, function()

                if preview and CSIndex(i) < list.Count and roleId ~= GameInstance.player.friendSystem.SelfInfo.roleId then
                    local templateIdList = {}
                    for j = 0, list.Count - 1 do
                        table.insert(templateIdList, list[j].templateId)
                    end
                    FriendUtils.openFriendCharInfo(roleId, list[CSIndex(i)].templateId, templateIdList)
                    return
                end
                if not preview then
                    UIManager:Open(PanelId.FriendRoleDisplay)
                end
            end, true)

            self.view['friendBusinessCardRoleCell' .. i].charHeadCell.view.button.customBindingViewLabelText = preview and Language.LUA_FRIEND_BUSINESS_EDIT_CHAR_PREVIEW or Language.LUA_FRIEND_BUSINESS_EDIT_CHAR
            if i == 1 then
                self.m_firstCharIsEmpty = false
            end
        end
    end
end

BusinessCardRoleNode.NaviToFirstChar = HL.Method() << function(self)
    if self.view.roleLayout.groupEnabled and self.view.naviGroup.IsTopLayer == false then
        self:SetNaviTarget(self.m_firstCharIsEmpty and self.view['friendBusinessCardRoleCell1'].addBtn or self.view['friendBusinessCardRoleCell1'].charHeadCell.view.button)
    end
end

HL.Commit(BusinessCardRoleNode)
return BusinessCardRoleNode

