
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.AdventureBook
local PHASE_ID = PhaseId.AdventureBook
AdventureBookCtrl = HL.Class('AdventureBookCtrl', uiCtrl.UICtrl)

local DUNGEON_TAB_INDEX = 3

AdventureBookCtrl.m_genTabCells = HL.Field(HL.Forward("UIListCache"))

AdventureBookCtrl.m_tabInfos = HL.Field(HL.Table)

AdventureBookCtrl.m_curTabIndex = HL.Field(HL.Number) << -1

AdventureBookCtrl.m_createArg = HL.Field(HL.Table)

AdventureBookCtrl.m_haveInitWalletBar = HL.Field(HL.Boolean) << false





AdventureBookCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CHANGE_ADVENTURE_BOOK_TAB] = 'ChangeTab',
    [MessageConst.ADVENTURE_BOOK_SELECT_TAB] = '_OnReceiveSelectTab',
}


AdventureBookCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_genTabCells = UIUtils.genCellCache(self.view.tabs.tabCell)

    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)


    self:BindInputPlayerAction("common_open_adventure_book", function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.m_createArg = arg
    self:_InitTabs()

    if self.m_createArg and self.m_createArg.panelId then
        self:ChangeTab(self.m_createArg)
        self.m_createArg = nil
    else
        self:_OnTabClick(self.m_curTabIndex, true)
    end
end

AdventureBookCtrl.OnAnimationInFinished = HL.Override() << function(self)
    if self.view.walletBarPlaceholder.gameObject.activeSelf then
        self.view.walletBarPlaceholder.gameObject:SetActive(false)
        self.view.walletBarPlaceholder.gameObject:SetActive(true)
    end
end

AdventureBookCtrl.ChangeTab = HL.Method(HL.Any) << function(self, arg)
    local panelId = PanelId[arg.panelId]
    self.m_curTabIndex = self:_GetCurTabIndexByPanelId(panelId)
    local cell = self.m_genTabCells:Get(self.m_curTabIndex)
    cell.toggle.isOn = true
    self:_OnTabClick(self.m_curTabIndex, true)
    if arg.dungeonTab and panelId == PanelId.AdventureDungeon then
        if self.m_phase.m_panel2Item[PanelId.AdventureDungeon] ~= nil then
            Notify(MessageConst.ON_CHANGE_ADVENTURE_DUNGEON_TAB, {
                dungeonTab = arg.dungeonTab,
                filterIndex = arg.filterIndex,
                reopenGemTermOverviewGameGroupId = arg.reopenGemTermOverviewGameGroupId,
            })
        else

            self.m_phase.m_dungeonTab = arg.dungeonTab
            self.m_phase.m_reopenGemTermOverviewGameGroupId = arg.reopenGemTermOverviewGameGroupId or ""
        end

        arg.dungeonTab = nil
        arg.filterIndex = nil
        arg.reopenGemTermOverviewGameGroupId = nil
    end
end

AdventureBookCtrl._OnReceiveSelectTab = HL.Method(HL.Any) << function(self, arg)
    local tabId = unpack(arg)
    self.m_curTabIndex = self:_GetCurTabIndexByTabId(tabId)
    local cell = self.m_genTabCells:Get(self.m_curTabIndex)
    cell.toggle.isOn = true
    self:_OnTabClick(self.m_curTabIndex, true)
    if arg.dungeonTab and panelId == PanelId.AdventureDungeon then
        Notify(MessageConst.ON_CHANGE_ADVENTURE_DUNGEON_TAB, { dungeonTab = arg.dungeonTab })
    end
end

AdventureBookCtrl._InitTabs = HL.Method() << function(self)
    self:_InitTabInfos()
    self:_InitTabIndex()

    self.m_genTabCells:Refresh(#self.m_tabInfos, function(cell, luaIndex)
        local info = self.m_tabInfos[luaIndex]
        cell.gameObject.name = "AdventureBookTab_"..luaIndex
        UIUtils.setTabIcons(cell,UIConst.UI_SPRITE_ADVENTURE,info.icon)
        cell.selectedNameTxt.text = info.tabName
        cell.defaultNameTxt.text = info.tabName
        if not string.isEmpty(info.redDot) then
            if info.redDotArg then
                cell.redDot:InitRedDot(info.redDot, info.redDotArg)
            else
                cell.redDot:InitRedDot(info.redDot)
            end
        end

        if luaIndex == self.m_curTabIndex then
            cell.stateController:SetState("Select")
        else
            cell.stateController:SetState("Unselected")
        end

        cell.toggle.isOn = luaIndex == self.m_curTabIndex
        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.onValueChanged:AddListener(function(isOn)
            local cell = self.m_genTabCells:Get(luaIndex)
            local stateCtrl = cell.stateController
            if isOn then
                stateCtrl:SetState("Select")
                self:_OnTabClick(luaIndex)
            else
                stateCtrl:SetState("Unselected")
            end
        end)
    end)
end

AdventureBookCtrl._InitTabInfos = HL.Method() << function(self)
    self.m_tabInfos = {

        {
            id = "AdventureStage",
            icon = "icon_adventure_book",
            tabName = Language.ui_AdventurePanel_title_adventurebook,
            panelId = PanelId.AdventureStage,
            redDot = "AdventureBookTabStage",
            checkRedDot = AdventureBookUtils.CheckRedDotAdventureBookTabStage
        },

        {
            id = "AdventureDaily",
            icon = "icon_adventure_daily",
            tabName = Language.ui_AdventurePanel_title_daily,
            panelId = PanelId.AdventureDaily,
            redDot = "AdventureBookTabDaily",
            checkRedDot = AdventureBookUtils.CheckRedDotAdventureBookTabDaily
        },
    }

    if(Utils.isSystemUnlocked(GEnums.UnlockSystemType.Dungeon) and AdventureBookUtils.HaveDungeon()) then
        table.insert(self.m_tabInfos, {
            id = "AdventureDungeon",
            icon = "icon_adventure_dungeon",
            tabName = Language.ui_AdventurePanel_title_dungeon,
            panelId = PanelId.AdventureDungeon,
            redDot = "AdventureBookTabDungeon",
            checkRedDot = AdventureBookUtils.CheckRedDotAdventureBookTabDungeon
        })
    end

    if(Utils.isSystemUnlocked(GEnums.UnlockSystemType.BattleTraining)) then
        table.insert(self.m_tabInfos, {
            id = "AdventureTraining",
            icon = "icon_adventure_training",
            tabName = Language.ui_AdventurePanel_title_training,
            panelId = PanelId.AdventureTraining,
            redDot = "AdventureBookTabTrain",
            checkRedDot = AdventureBookUtils.CheckRedDotAdventureBookTabTrain
        })
    end

    if(Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacTechTree)) then
        local canShow = false
        for _, cfg in pairs(Tables.domainDataTable) do
            if not GameInstance.player.facTechTreeSystem:PackageIsLocked(cfg.facTechPackageId) and
                not GameInstance.player.facTechTreeSystem:PackageIsHidden(cfg.facTechPackageId)
            then
                canShow = true
                break
            end
        end
        if canShow then
            table.insert(self.m_tabInfos, {
                id = "AdventureBlackbox",
                icon = "icon_adventure_blackbox",
                tabName = Language.ui_AdventurePanel_title_blackbox,
                panelId = PanelId.AdventureBlackbox,
                redDot = "AdventureBookTabBlackbox",
                checkRedDot = AdventureBookUtils.CheckRedDotAdventureBookTabBlackbox
            })
        end
    end

    if AdventureBookUtils.HaveActivityTab() then
        table.insert(self.m_tabInfos, {
            id = "AdventureActivity",
            icon = "icon_adventure_racingdungeon",
            tabName = Language.ui_AdventurePanel_title_activity,
            panelId = PanelId.AdventureActivity,
            redDot = "AdventureBookTabActivity",
            checkRedDot = AdventureBookUtils.CheckRedDotAdventureBookTabActivity,
        })
    end
end

AdventureBookCtrl._InitTabIndex = HL.Method() << function(self)

    for i, v in ipairs(self.m_tabInfos) do
        if v.redDot and v.checkRedDot then
            local showRedDot = v.checkRedDot()
            if showRedDot then
                self.m_curTabIndex = i
                return
            end
        end
    end

    for i, v in ipairs(self.m_tabInfos) do
        if v.panelId == PanelId.AdventureDungeon then
            self.m_curTabIndex = i
            return
        end
    end

    self.m_curTabIndex = 1
end

AdventureBookCtrl._GetCurTabIndexByPanelId = HL.Method(HL.Number).Return(HL.Number) << function(self, panelId)
    local index = 1
    for _, info in pairs(self.m_tabInfos) do
        if info.panelId == panelId then
            return index
        end
        index = index + 1
    end
    return 1
end

AdventureBookCtrl._GetCurTabIndexByTabId = HL.Method(HL.String).Return(HL.Number) << function(self, tabId)
    local index = 1
    for _, info in pairs(self.m_tabInfos) do
        if info.id == tabId then
            return index
        end
        index = index + 1
    end
    return 1
end

AdventureBookCtrl._OnTabClick = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, luaIndex, isInit)
    if self.m_curTabIndex == luaIndex and not isInit then
        return
    end
    local changeToLeft = luaIndex < self.m_curTabIndex
    local prevTabInfo = self.m_tabInfos[self.m_curTabIndex]
    if prevTabInfo.panelId == PanelId.AdventureDaily then

        self:Notify(MessageConst.P_ON_ADVENTURE_DAILY_CLOSE_REWARD_TIPS)

        Notify(MessageConst.HIDE_COMMON_TITLE_TIPS)
    end
    self.m_curTabIndex = luaIndex
    local curTabInfo = self.m_tabInfos[luaIndex]
    if curTabInfo.panelId == PanelId.AdventureDungeon then
        self.view.walletBarPlaceholder.gameObject:SetActive(true)
        self.view.walletBarPlaceholder:InitWalletBarPlaceholder(
            { "item_bp_double_reward", Tables.globalConst.apItemId })
        self.m_haveInitWalletBar = true
    else
        if self.m_haveInitWalletBar then
            self.view.walletBarPlaceholder.gameObject:SetActive(false)
        end
    end

    self.m_phase:OnTabChange({
        panelId = curTabInfo.panelId,
        changeToLeft = changeToLeft
    })
end

HL.Commit(AdventureBookCtrl)
