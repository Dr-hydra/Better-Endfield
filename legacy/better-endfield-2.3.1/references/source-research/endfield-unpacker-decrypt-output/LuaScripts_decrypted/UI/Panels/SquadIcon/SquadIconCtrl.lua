local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SquadIcon

SquadIconCtrl = HL.Class('SquadIconCtrl', uiCtrl.UICtrl)







SquadIconCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_BATTLE_SQUAD_CHANGED] = '_OnTeamChange',
    [MessageConst.GAME_MODE_ENABLE] = '_OnTeamChange',
    [MessageConst.ON_CHAR_LEVEL_UP] = '_OnLevelChange',
    [MessageConst.ON_CHARACTER_DEAD] = '_OnCharacterDead',
    [MessageConst.ON_CONTROLLER_INDICATOR_CHANGE] = 'OnToggleControllerIndicator',
    [MessageConst.ON_SQUAD_TACTICAL_ITEM_CHANGE] = '_OnSquadTacticalItemChange',
    [MessageConst.ON_MANUAL_CRAFT_ITEM_LEVEL_UP] = "_OnManualCraftItemLevelUp",
    [MessageConst.ON_SYSTEM_UNLOCK_CHANGED] = 'OnSystemUnlock',
    [MessageConst.ON_TOGGLE_UI_ACTION] = 'OnToggleUiAction',
    [MessageConst.ON_SWITCH_CENTER_BTN_CLICK] = 'OnSwitchCenterBtnClick',
    [MessageConst.ON_SWITCH_MAIN_CHAR_ENABLE_CONFIG_CHANGED] = 'OnSwitchMainCharEnableConfigChanged',
    [MessageConst.ON_SQUAD_ICON_ACTIVE_CONFIG_CHANGED] = 'OnSquadIconActiveConfigChanged',
    [MessageConst.ON_SQUAD_DISABLE_TIPS_CHANGED] = 'OnDisableTipsChanged',
    [MessageConst.TOGGLE_DEBUG_SQUAD_ICON_BUFF_MODE] = 'OnToggleDebugSquadIconBuffMode',
}

SquadIconCtrl.m_listItems = HL.Field(HL.Table)

SquadIconCtrl.m_indicatorShowing = HL.Field(HL.Boolean) << false

SquadIconCtrl.m_teamSwitchUnlocked = HL.Field(HL.Boolean) << false

SquadIconCtrl.m_teamSwitchEnabled = HL.Field(HL.Boolean) << false

SquadIconCtrl.m_gpuiBuffEnabled = HL.Field(HL.Boolean) << true

SquadIconCtrl.m_disableTipList = HL.Field(HL.Forward("UIListCache"))


SquadIconCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_listItems = {}
    if self.isControllerPanel then
        for i = 1, Const.BATTLE_SQUAD_MAX_CHAR_NUM do
            self.m_listItems[i] = self.view["squadIcon" .. i]
            if self.view.buffIconSync ~= nil then
                self.m_listItems[i]:SetGPUIBuffSync(self.view.buffIconSync)
            end
            self.m_listItems[i]:SetGPUIBuffMode(self.m_gpuiBuffEnabled)
            self.m_listItems[i]:FirstTimeInit(CSIndex(i), self.isDefaultPanel, self.isControllerPanel)
        end
    else
        self.m_listItems[1] = self.view.squadIcon
        for i = 2, Const.BATTLE_SQUAD_MAX_CHAR_NUM do
            local obj = CSUtils.CreateObject(self.view.squadIcon.gameObject, self.view.squadIcon.gameObject.transform.parent)
            local cell = obj:GetComponent("SquadIcon")
            self.m_listItems[i] = cell
        end
        for i = 1, Const.BATTLE_SQUAD_MAX_CHAR_NUM do
            if self.view.buffIconSync ~= nil then
                self.m_listItems[i]:SetGPUIBuffSync(self.view.buffIconSync)
            end
            self.m_listItems[i]:SetGPUIBuffMode(self.m_gpuiBuffEnabled)
            self.m_listItems[i]:FirstTimeInit(CSIndex(i), self.isDefaultPanel, self.isControllerPanel)
        end
    end

    self.m_teamSwitchUnlocked = Utils.isSystemUnlocked(GEnums.UnlockSystemType.TeamSwitch)
    self.m_teamSwitchEnabled = GameWorld.battle.switchMainCharEnable

    self.view.nextBtn.onPressStart:AddListener(function(eventData)
        self:_OnNextBtnClick()
    end)
    self.view.nextBtn.gameObject:SetActive(self:_ShowNextBtn())

    self:OnToggleControllerIndicator(false)

    if self.isControllerPanel then
        self.view.activateHint.gameObject:SetActiveIfNecessary(true)
    else
        self.view.activateHint.gameObject:SetActiveIfNecessary(false)
    end

    self.view.main.gameObject:SetActive(GameWorld.battle.squadIconActive)

    self:OnDisableTipsChanged({GameWorld.battle.squadDisableTips})
end

SquadIconCtrl.OnShow = HL.Override() << function(self)
    for _, item in ipairs(self.m_listItems) do
        item.enabled = true
    end
    self:_OnTeamChange()
    if CS.Beyond.UI.BattleControllerInputController.instance:GetControllerIndicatorState() then
        self:OnToggleControllerIndicator(true)
    end
    if self.isControllerPanel then
        self.view.hudFadeController:OnShow()
    end
end

SquadIconCtrl.OnHide = HL.Override() << function(self)
    self:OnToggleControllerIndicator(false)
    for _, item in ipairs(self.m_listItems) do
        item.enabled = false
    end
end

SquadIconCtrl.OnToggleDebugSquadIconBuffMode = HL.Method() << function(self)
    self.m_gpuiBuffEnabled = not self.m_gpuiBuffEnabled
    GameAction.ShowUIToast("已切换到" .. (self.m_gpuiBuffEnabled and "GPUI Buff模式" or "普通Buff模式"))
    for _, item in ipairs(self.m_listItems) do
        item:SetGPUIBuffMode(self.m_gpuiBuffEnabled)
    end
end

SquadIconCtrl.OnClose = HL.Override() << function(self)
    for i, item in ipairs(self.m_listItems) do
        item:Close()
    end
end

SquadIconCtrl._GetCount = HL.Method().Return(HL.Number) << function(self)
    local count = 0
    for _, item in ipairs(self.m_listItems) do
        if not item.isEmpty then
            count = count + 1
        end
    end
    return count
end

SquadIconCtrl._GetItem = HL.Method(HL.Number).Return(HL.Any) << function(self, index)
    return self.m_listItems[index]
end

SquadIconCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    if not active and self:IsShow() then
        self:OnToggleControllerIndicator(false)
    end
    if active and self:IsShow() and CS.Beyond.UI.BattleControllerInputController.instance:GetControllerIndicatorState() then
        self:OnToggleControllerIndicator(true)
    end
end

SquadIconCtrl.OnToggleUiAction = HL.Method(HL.Table) << function(self, arg)
    self:_OnPanelInputBlocked(self.view.inputGroup.groupEnabled)
end


SquadIconCtrl._OnTeamChange = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    local squadSlots = GameInstance.player.squadManager.curSquad.slots


    local isFullLockedTeam, formationData = CharInfoUtils.IsFullLockedTeam()

    for i = 1, squadSlots.Count do

        local showFixed, showTrial = false, false
        if isFullLockedTeam then
            showFixed = true
            showTrial = false
        elseif formationData ~= nil and formationData.chars ~= nil then
            local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(squadSlots[CSIndex(i)].charInstId)
            if charInfo.charType == GEnums.CharType.Trial then
                for j = 1, #formationData.chars do
                    if formationData.chars[j].charId == charInfo.templateId then
                        showFixed, showTrial = CharInfoUtils.getLockedFormationCharTipsShow(formationData.chars[j])
                        break
                    end
                end
            end
        end

        local item = self.m_listItems[i]
        if item ~= nil then
            item:SetEmpty(false)
            item:InitSquadIcon(showFixed, showTrial)
        end
    end
    for i = squadSlots.Count + 1, Const.BATTLE_SQUAD_MAX_CHAR_NUM do
        local item = self.m_listItems[i]
        item:SetEmpty(true)
    end

    self.view.nextBtn.gameObject:SetActive(self:_ShowNextBtn())
end


SquadIconCtrl._OnLevelChange = HL.Method(HL.Table) << function(self, args)
    local instId, _ = unpack(args)
    local squadSlots = GameInstance.player.squadManager.curSquad.slots
    for i = 1, squadSlots.Count do
        local item = self.m_listItems[i]
        local csIndex = CSIndex(i)
        if squadSlots[csIndex].charInstId == instId then
            item:OnLevelChange()
        end
    end
end

SquadIconCtrl._OnCharacterDead = HL.Method(HL.Table) << function(self, args)
    local luaIndex = LuaIndex(unpack(args))
    local item = self:_GetItem(luaIndex)
    if item ~= nil then
        item:SetDeadState(true)
    end
end

SquadIconCtrl.OnToggleControllerIndicator = HL.Method(HL.Boolean) << function(self, active)
    if self.isControllerPanel then
        if active and not self:IsShow() then

            return
        end
        self.m_indicatorShowing = active
        self.view.animator:SetBool("IndicatorActive", active)
        local count = self:_GetCount()
        if count > 0 then
            for i = 1, count do
                local item = self:_GetItem(i)
                item:ToggleIndicator(active)
            end
        end
    end
end

SquadIconCtrl._OnSquadTacticalItemChange = HL.Method(HL.Table) << function(self, args)
    local index, itemId = unpack(args)
    local luaIndex = LuaIndex(index)
    local item = self:_GetItem(luaIndex)
    if item ~= nil then
        item:OnTacticalItemChange()
    end
end

SquadIconCtrl._OnManualCraftItemLevelUp = HL.Method(HL.Table) << function(self, args)
    local items = unpack(args)
    for i = 0, items.Count - 1 do
        local itemId = items[i]
        local squadSlots = GameInstance.player.squadManager.curSquad.slots
        for j = 1, squadSlots.Count do
            local squadSlot = squadSlots[CSIndex(j)]
            local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(squadSlot.charInstId)
            if charInfo.tacticalItemId == itemId then
                local squadIcon = self:_GetItem(j)
                if squadIcon ~= nil then
                    squadIcon:OnTacticalItemChange()
                end
            end
        end
    end
end

SquadIconCtrl.OnSystemUnlock = HL.Method(HL.Any) << function(self, arg)
    local systemIndex = unpack(arg)
    if systemIndex == GEnums.UnlockSystemType.TeamSwitch:GetHashCode() then
        for i = 1, Const.BATTLE_SQUAD_MAX_CHAR_NUM do
            local item = self:_GetItem(i)
            item:OnTeamSwitchUnlocked()
        end
        self.m_teamSwitchUnlocked = true
        self.view.nextBtn.gameObject:SetActive(self:_ShowNextBtn())
    end
end

SquadIconCtrl.OnSquadIconActiveConfigChanged = HL.Method(HL.Any) << function(self, arg)
    local active = unpack(arg)
    self.view.main.gameObject:SetActive(active)
end

SquadIconCtrl.OnSwitchMainCharEnableConfigChanged = HL.Method(HL.Any) << function(self, arg)
    local enable = unpack(arg)
    self.m_teamSwitchEnabled = enable
    self.view.nextBtn.gameObject:SetActive(self:_ShowNextBtn())
end


SquadIconCtrl._OnNextBtnClick = HL.Method() << function(self)
    local curIndex = LuaIndex(GameInstance.player.squadManager.leaderIndex)
    local count = self:_GetCount()
    for i = 1, count do
        local nextIndex = curIndex + i
        if nextIndex > count then
            nextIndex = nextIndex - count
        end
        local item = self:_GetItem(nextIndex)
        if item:CanSwitchToCenter(false) then
            item:OnPressCharIconStart(nil)
            break
        end
    end
    self:OnSwitchCenterBtnClick()
end

SquadIconCtrl.OnSwitchCenterBtnClick = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    local count = self:_GetCount()
    for i = 1, count do
        local item = self:_GetItem(i)
        item:InformKeyHint()
    end
end

SquadIconCtrl._ShowNextBtn = HL.Method().Return(HL.Boolean) << function(self)
    return self:_GetCount() > 1 and self.m_teamSwitchUnlocked and self.m_teamSwitchEnabled
end

SquadIconCtrl.OnDisableTipsChanged = HL.Method(HL.Table) << function(self, arg)
    if DeviceInfo.isMobile or self.view.disableTipTemp == nil then

        return
    end
    if self.m_disableTipList == nil then
        self.m_disableTipList = UIUtils.genCellCache(self.view.disableTipTemp)
    end

    local tipList = unpack(arg)
    self.m_disableTipList:Refresh(tipList.Count, function(cell, index)
        local tipTextId = tipList[CSIndex(index)]
        cell.disableTxt.text = I18nUtils.GetText(tipTextId)
        cell.transform:SetAsFirstSibling()
    end)
end

HL.Commit(SquadIconCtrl)
