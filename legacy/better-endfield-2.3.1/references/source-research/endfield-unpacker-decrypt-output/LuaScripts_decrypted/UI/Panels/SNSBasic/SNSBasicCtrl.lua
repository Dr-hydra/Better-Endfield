
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SNSBasic
local PHASE_ID = PhaseId.SNS

local TabConfigs = {
    {
        icon = "sns_icon_chat",
        titleName = Language.LUA_SNS_BARKER_TITLE,
        panelId = PanelId.SNSBarker,
        redDot = "SNSBarkerTabCell",
        forceHide = function()
            return GameInstance.player.spaceship.isViewingFriend
        end
    },
    {
        icon = "sns_icon_task",
        titleName = Language.LUA_SNS_MISSION_TITLE,
        panelId = PanelId.SNSMission,
        redDot = "SNSMissionTabCell",
        forceHide = function()
            return GameInstance.player.spaceship.isViewingFriend
        end
    },
    {
        icon = "sns_icon_friend",
        titleName = Language.LUA_SNS_CHAT_TITLE,
        panelId = PanelId.SNSFriend,
        redDot = "FriendChatUnRead",
        separation = false,
    },
}

SNSBasicCtrl = HL.Class('SNSBasicCtrl', uiCtrl.UICtrl)

SNSBasicCtrl.m_genTabCells = HL.Field(HL.Forward("UIListCache"))

SNSBasicCtrl.m_curTabIndex = HL.Field(HL.Number) << -1

SNSBasicCtrl.m_tabInfos = HL.Field(HL.Table)

SNSBasicCtrl.m_args = HL.Field(HL.Table)





SNSBasicCtrl.s_messages = HL.StaticField(HL.Table) << {

}


SNSBasicCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_args = arg
    self.m_genTabCells = UIUtils.genCellCache(self.view.tabs.tabCell)

    self:BindInputPlayerAction("close_sns", function()
        self:_OnClickBtnClose()
    end)

    self.view.btnClose.onClick:AddListener(function()
        self:_OnClickBtnClose()
    end)

    self:_InitTabInfos()
    self:_InitTabs()
end








SNSBasicCtrl._OnClickBtnClose = HL.Method() << function(self)


    PhaseManager:PopPhase(PHASE_ID)
end

SNSBasicCtrl._InitTabInfos = HL.Method() << function(self)
    self.m_tabInfos = {}
    for _, tabConfig in ipairs(TabConfigs) do
        if not tabConfig.forceHide or not tabConfig.forceHide() then
            table.insert(self.m_tabInfos, tabConfig)
        end
    end
end

SNSBasicCtrl._InitTabs = HL.Method() << function(self)
    local defaultPanelId = self.m_args and (self.m_args.defaultPanelId or unpack(self.m_args))
    local recoverTabIndex = self.m_args and self.m_args.tabIndex or nil
    if type(recoverTabIndex) == "number" and self.m_tabInfos[recoverTabIndex] then
        self.m_curTabIndex = recoverTabIndex
    else
        for luaIndex, tabInfo in ipairs(self.m_tabInfos) do
            if defaultPanelId == tabInfo.panelId then
                self.m_curTabIndex = luaIndex
                break
            end
        end
    end

    if self.m_curTabIndex < 1 then
        self.m_curTabIndex = 1
    end

    self.m_genTabCells:Refresh(#self.m_tabInfos, function(cell, luaIndex)
        self:_OnUpdateTabCell(cell, luaIndex)
    end)

    self.view.title.text = self.m_tabInfos[self.m_curTabIndex].titleName
    self.view.tabs.inputBindingGroupMonoTarget.enabled = #self.m_tabInfos > 1
end

SNSBasicCtrl._OnUpdateTabCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local info = self.m_tabInfos[luaIndex]
    cell.gameObject.name = "SNSTab_" .. luaIndex
    UIUtils.setTabIcons(cell, UIConst.UI_SPRITE_SNS, info.icon)

    cell.toggle.isOn = luaIndex == self.m_curTabIndex
    cell.toggle.onValueChanged:RemoveAllListeners()
    cell.toggle.onValueChanged:AddListener(function(isOn)
        if isOn then
            self:_OnTabClick(luaIndex)
        end
    end)

    cell.redDot:InitRedDot(info.redDot)

    if cell.lineImg then
        cell.lineImg.gameObject:SetActive(info.separation == true)
    end
end

SNSBasicCtrl._OnTabClick = HL.Method(HL.Number) << function(self, luaIndex)
    if self.m_curTabIndex == luaIndex then
        return
    end
    self.m_curTabIndex = luaIndex
    local curTabInfo = self.m_tabInfos[luaIndex]
    self.view.title.text = curTabInfo.titleName


    local phase = self.m_phase
    phase:OnTabChange({ panelId = curTabInfo.panelId })
end

SNSBasicCtrl.GetRecoverStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local curTabInfo = self.m_tabInfos and self.m_tabInfos[self.m_curTabIndex] or nil
    if not curTabInfo then
        return
    end

    return {
        tabIndex = self.m_curTabIndex,
        panelId = curTabInfo.panelId,
    }
end




SNSBasicCtrl.ToggleTitleBindGroup = HL.Method(HL.Boolean) << function(self, isOn)
    self.view.content.enabled = isOn
end

SNSBasicCtrl.ToggleCloseBtn = HL.Method(HL.Boolean) << function(self, isOn)
    self.view.btnClose.enabled = isOn
end



HL.Commit(SNSBasicCtrl)
