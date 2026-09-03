
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FriendTab

FriendTabCtrl = HL.Class('FriendTabCtrl', uiCtrl.UICtrl)

FriendTabCtrl.m_genTabCells = HL.Field(HL.Forward("UIListCache"))

FriendTabCtrl.m_tabInfos = HL.Field(HL.Table)

FriendTabCtrl.m_curTabIndex = HL.Field(HL.Number) << -1

FriendTabCtrl.m_createArg = HL.Field(HL.Table)





FriendTabCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CHANGE_FRIEND_TAB] = 'ChangeTab',
}


FriendTabCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_genTabCells = UIUtils.genCellCache(self.view.tabs.tabCell)
    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.Friend)
    end)

    self.m_createArg = arg
    self:_InitTabs()
end

FriendTabCtrl._InitTabs = HL.Method() << function(self)
    self:_InitTabInfos()

    self.m_genTabCells:Refresh(#self.m_tabInfos, function(cell, luaIndex)
        local info = self.m_tabInfos[luaIndex]
        cell.gameObject.name = "FriendTab_"..luaIndex
        UIUtils.setTabIcons(cell,UIConst.UI_SPRITE_INVENTORY,info.icon)
        if not string.isEmpty(info.redDot) then
            if info.redDotArg then
                cell.redDot:InitRedDot(info.redDot, info.redDotArg)
            else
                cell.redDot:InitRedDot(info.redDot)
            end
        end

        cell.toggle.isOn = luaIndex == self.m_curTabIndex
        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self:_OnTabClick(luaIndex)
            end
        end)
    end)
end

FriendTabCtrl.ChangeTab = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    if arg and arg.panelId then

        self.m_curTabIndex = self:_GetCurTabIndexByPanelId(arg.panelId)
    else
        self.m_curTabIndex = 1
    end
    local cell = self.m_genTabCells:Get(self.m_curTabIndex)
    cell.toggle.isOn = true
    self:_OnTabClick(self.m_curTabIndex, true , arg)
end

FriendTabCtrl._InitTabInfos = HL.Method() << function(self)
    self.m_tabInfos = {
        {
            icon = "friend_tab_personal_info_icon",
            panelId = PanelId.FriendBusinessCardRoot,
            redDot = "BusinessCard",
            text = Language.LUA_FRIEND_TITLE_BUSINESS_CARD
        },
        {
            icon = "friend_tab_friend_list_icon",
            panelId = PanelId.FriendList,
            redDot = "NewFriendRequest",
            text = Language.LUA_FRIEND_TITLE_FRIEND_LIST
        },
        {
            icon = "friend_tab_friend_add_icon",
            panelId = PanelId.StrangerList,
            text = Language.LUA_FRIEND_TITLE_FRIEND_ADD
        },
    }
end

FriendTabCtrl._OnTabClick = HL.Method(HL.Number, HL.Opt(HL.Boolean, HL.Table)) << function(self, luaIndex, isInit, arg)
    if self.m_curTabIndex == luaIndex and not isInit then
        return
    end
    self.m_curTabIndex = luaIndex
    local curTabInfo = self.m_tabInfos[luaIndex]
    self.m_phase:OnTabChange(curTabInfo.panelId ,arg)
    self.view.tabText.text = curTabInfo.text

    UIManager:SetTopOrder(PANEL_ID)
end

FriendTabCtrl._GetCurTabIndexByPanelId = HL.Method(HL.Number).Return(HL.Number) << function(self, panelId)
    local index = 1
    for _, info in pairs(self.m_tabInfos) do
        if info.panelId == panelId then
            return index
        end
        index = index + 1
    end
    return 1
end











HL.Commit(FriendTabCtrl)
