local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DungeonDoubleAssaultEntry
local dungeonActivityEntry = require_ex('UI/Panels/DungeonActivityEntry/DungeonActivityEntryCtrl')
local activityUtils = require_ex('Common/Utils/ActivityUtils')

DungeonDoubleAssaultEntryCtrl = HL.Class('DungeonDoubleAssaultEntryCtrl', dungeonActivityEntry.DungeonActivityEntryCtrl)


DungeonDoubleAssaultEntryCtrl.m_selectTeamIndexTable = HL.Field(HL.Table)

DungeonDoubleAssaultEntryCtrl.m_skipSameDungeonCellClick = HL.Field(HL.Boolean) << true




















DungeonDoubleAssaultEntryCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_selectTeamIndexTable = {}
    self.m_skipSameDungeonCellClick = false




    if arg and not string.isEmpty(arg.dungeonId) then
        local hasRaid, raidId = Tables.dungeonNormal2RaidTable:TryGetValue(arg.dungeonId)
        if hasRaid and not string.isEmpty(raidId) and DungeonUtils.isDungeonPassed(arg.dungeonId) then
            arg.dungeonId = raidId
        end
    end

    DungeonDoubleAssaultEntryCtrl.Super.OnCreate(self, arg)

    if DeviceInfo.usingController then
        self.view.dungeonCommonInfo.view.hardTog.customBindingViewLabelText = Language.LUA_DOUBLE_ASSAULT_SWITCH_FULL_POWER_MODE_HINT
        self.view.dungeonCommonInfo.view.fakeHardTogBtn.customBindingViewLabelText = Language.LUA_DOUBLE_ASSAULT_SWITCH_FULL_POWER_MODE_HINT
    end

    local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)

    if not activity then
        return
    end

    for i, id in ipairs(self.m_dungeons) do
        self:_InitSelectTeamData(id)
    end

    self.view.helpBtn.onClick:RemoveAllListeners()
    self.view.helpBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "activity_dungeon_actmonster")
    end)

    self.view.editCharBtn.onClick:RemoveAllListeners()
    self.view.editCharBtn.onClick:AddListener(function()
        local selectedTeamData = self:_EnsureSelectTeamData(self.m_curSelectedDungeonId)
        if selectedTeamData == nil then
            logger.error("双人突袭未初始化当前关卡配队数据，gameId:" .. self.m_curSelectedDungeonId)
            return
        end
        UIManager:Open(PanelId.DoubleAssaultCharSelPopup,
            { activityId = self.m_activityId,
              gameId = self.m_curSelectedDungeonId,
              teamIndex = selectedTeamData[1],
              onSelect = function(selectedTeamIndex, teamIndex)
                  self.m_selectTeamIndexTable[self.m_curSelectedDungeonId] = { selectedTeamIndex, teamIndex }
                  self:_RefreshCharBtnState()
              end })
    end)

    self.view.systemCharBtn.onClick:RemoveAllListeners()
    self.view.systemCharBtn.onClick:AddListener(function()
        local selectedTeamData = self:_EnsureSelectTeamData(self.m_curSelectedDungeonId)
        if selectedTeamData == nil then
            logger.error("双人突袭未初始化当前关卡配队数据，gameId:" .. self.m_curSelectedDungeonId)
            return
        end
        UIManager:Open(PanelId.DoubleAssaultCharSelPopup,
            { activityId = self.m_activityId,
              gameId = self.m_curSelectedDungeonId,
              teamIndex = selectedTeamData[1],
              onSelect = function(selectedTeamIndex, teamIndex)
                  self.m_selectTeamIndexTable[self.m_curSelectedDungeonId] = { selectedTeamIndex, teamIndex }
                  self:_RefreshCharBtnState()
              end })
    end)

    self.view.dungeonCommonInfo:SetOpenCharFormationCallback(function(gameId)
        self:_OpenDoubleAssaultCharFormation(gameId)
    end)

    self.view.dungeonCommonInfo.view.hardTog.onValueChanged:AddListener(function(isOn)
        if isOn then
            self.m_curSelectedDungeonId = Tables.dungeonNormal2RaidTable[self.m_curSelectedDungeonId]
        else
            self.m_curSelectedDungeonId = Tables.dungeonRaid2NormalTable[self.m_curSelectedDungeonId]
        end
        self:_EnsureSelectTeamData(self.m_curSelectedDungeonId)
        self:UpdateInfo()
        self:_RefreshCharBtnState()
        if isOn then
            AudioAdapter.PostEvent("Au_UI_Toast_HighDifficultyHint")
        end
    end)

    for i, id in ipairs(self.m_dungeons) do
        if self:_IsSameDungeon(id) then
            self:_OnDungeonCellClick(self.m_genCells:Get(i), i, id)
            break
        end
    end
    self.m_skipSameDungeonCellClick = true

    local currentDungeonId = self.m_curSelectedDungeonId
    if Tables.dungeonRaid2NormalTable:TryGetValue(currentDungeonId) then
        self.view.dungeonCommonInfo.view.hardTog:SetIsOnWithoutNotify(true)
    end
end

DungeonDoubleAssaultEntryCtrl._IsSameDungeon = HL.Override(HL.String).Return(HL.Boolean) << function(self, dungeonId)
    if dungeonId == self.m_curSelectedDungeonId then
        return true
    end
    local hasRaid, raidDungeonId = Tables.dungeonNormal2RaidTable:TryGetValue(dungeonId)
    return hasRaid and raidDungeonId == self.m_curSelectedDungeonId
end

DungeonDoubleAssaultEntryCtrl._InitSelectTeamData = HL.Method(HL.String) << function(self, dungeonId)
    local firstTeamId = activityUtils.getDoubleAssaultDefaultTeamId(dungeonId)
    self.m_selectTeamIndexTable[dungeonId] = { 1, firstTeamId }
end








DungeonDoubleAssaultEntryCtrl.GetDefaultSelectedDungeonId = HL.Override().Return(HL.String) << function(self)
    local success, dungeonSeriesData = Tables.dungeonSeriesTable:TryGetValue(self.m_dungeonSeriesId)
    if not success then
        return ""
    end

    local normalDungeons = {}
    for i = 0, dungeonSeriesData.includeDungeonIds.Count - 1 do
        local dungeonId = dungeonSeriesData.includeDungeonIds[i]
        if Tables.dungeonNormal2RaidTable:TryGetValue(dungeonId) then
            table.insert(normalDungeons, dungeonId)
        end
    end
    table.sort(normalDungeons, function(a, b)
        local _, cfgA = Tables.dungeonTable:TryGetValue(a)
        local _, cfgB = Tables.dungeonTable:TryGetValue(b)
        return cfgA.sortId < cfgB.sortId
    end)


    for _, normalId in ipairs(normalDungeons) do
        if DungeonUtils.isDungeonUnlock(normalId) and not DungeonUtils.isDungeonPassed(normalId) then
            return normalId
        end
    end


    for _, normalId in ipairs(normalDungeons) do
        local raidId = Tables.dungeonNormal2RaidTable[normalId]
        if not string.isEmpty(raidId)
            and DungeonUtils.isDungeonUnlock(raidId)
            and not DungeonUtils.isDungeonPassed(raidId)
        then
            return raidId
        end
    end


    for i = #normalDungeons, 1, -1 do
        local normalId = normalDungeons[i]
        local raidId = Tables.dungeonNormal2RaidTable[normalId]
        if not string.isEmpty(raidId) and DungeonUtils.isDungeonUnlock(raidId) then
            return raidId
        end
        if DungeonUtils.isDungeonUnlock(normalId) then
            return normalId
        end
    end

    return ""
end

DungeonDoubleAssaultEntryCtrl._EnsureSelectTeamData = HL.Method(HL.String).Return(HL.Table) << function(self, dungeonId)
    if string.isEmpty(dungeonId) then
        return nil
    end

    local selectedTeamData = self.m_selectTeamIndexTable[dungeonId]
    if selectedTeamData == nil then
        self:_InitSelectTeamData(dungeonId)
        selectedTeamData = self.m_selectTeamIndexTable[dungeonId]
    end
    return selectedTeamData
end

DungeonDoubleAssaultEntryCtrl._OpenDoubleAssaultCharFormation = HL.Method(HL.String) << function(self, dungeonId)
    local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    if not activity then
        return
    end
    local selectedTeamData = self:_EnsureSelectTeamData(self.m_curSelectedDungeonId)
    if selectedTeamData == nil then
        logger.error("双人突袭未初始化当前关卡配队数据，gameId:" .. self.m_curSelectedDungeonId)
        return
    end

    local selectedTeamIndex = selectedTeamData[1]
    local teamId = selectedTeamData[2]
    if string.isEmpty(teamId) then
        logger.error("双人突袭当前选择配队为空，gameId:" .. self.m_curSelectedDungeonId .. "，selectedTeamIndex:" .. tostring(selectedTeamIndex))
        return
    end

    LuaSystemManager.uiRestoreSystem:AddRequest(dungeonId)
    if selectedTeamIndex == 1 then
        PhaseManager:GoToPhase(PhaseId.CharFormation, {
            dungeonId = self.m_curSelectedDungeonId,
            presetTeamId = teamId,
            initCharInstIdList = activity.charTeam,
            customSetTeamCallback = function(charInstIdList)
                GameInstance.player.activitySystem:SendActivitySetTeam(self.m_activityId, charInstIdList)
            end,
        })
        return
    end

    PhaseManager:GoToPhase(PhaseId.CharFormation, {
        dungeonId = dungeonId,
        presetTeamId = teamId,
    })
end

DungeonDoubleAssaultEntryCtrl._OnDungeonCellClick = HL.Override(HL.Any, HL.Number, HL.String) << function(self, cell, index, dungeonId)
    if self.m_skipSameDungeonCellClick and self:_IsSameDungeon(dungeonId) then
        return
    end

    local dungeonCommonInfoView = self.view.dungeonCommonInfo.view
    local raidDungeonId = Tables.dungeonNormal2RaidTable[dungeonId]
    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local isHardModeUnlocked = false
    if not string.isEmpty(raidDungeonId) and activityData then
        isHardModeUnlocked = DungeonUtils.isDungeonPassed(dungeonId)
    end
    if isHardModeUnlocked then
        dungeonCommonInfoView.hardModeNode:SetState("Enabled")
    else
        dungeonCommonInfoView.hardModeNode:SetState("Disabled")
        dungeonCommonInfoView.fakeHardTogBtn.onClick:RemoveAllListeners()
        dungeonCommonInfoView.fakeHardTogBtn.onClick:AddListener(function()
            Notify(MessageConst.SHOW_TOAST,Language.LUA_DUNGEON_HARD_MODE_CANT_TOG)
        end)
    end



    local autoPickHard = isHardModeUnlocked
    local finalDungeonId = autoPickHard and raidDungeonId or dungeonId
    dungeonCommonInfoView.hardTog:SetIsOnWithoutNotify(autoPickHard)
    dungeonCommonInfoView.hardTogStateController:SetState(autoPickHard and "On" or "Off")

    local prevSelectedDungeonId = self.m_curSelectedDungeonId
    DungeonDoubleAssaultEntryCtrl.Super._OnDungeonCellClick(self, cell, index, finalDungeonId)
    if autoPickHard then
        self:_EnsureSelectTeamData(finalDungeonId)
        if self.m_curSelectedDungeonId ~= prevSelectedDungeonId then
            AudioAdapter.PostEvent("Au_UI_Toast_HighDifficultyHint")
        end
    end
    self:_RefreshCharBtnState()
end

DungeonDoubleAssaultEntryCtrl._RefreshCharBtnState = HL.Method() << function(self)
    local selectedTeamData = self:_EnsureSelectTeamData(self.m_curSelectedDungeonId)
    if selectedTeamData == nil then
        return
    end
    local selectedTeamIndex = selectedTeamData[1]
    local teamId = selectedTeamData[2]
    local isSystemTeam = selectedTeamIndex ~= 1

    if self.view == nil or IsNull(self.view.editCharBtn) or IsNull(self.view.systemCharBtn) then
        return
    end
    self.view.editCharBtn.gameObject:SetActive(not isSystemTeam)
    self.view.systemCharBtn.gameObject:SetActive(isSystemTeam)

    if isSystemTeam and not string.isEmpty(teamId) then
        if IsNull(self.view.systemTxt) or IsNull(self.view.charImage1) or IsNull(self.view.charImage2) then
            return
        end
        self.view.systemTxt.text = string.format(Language.LUA_ACT_DOUBLE_ASSAULT_CHAR_SEL_TEAM_NAME, string.char(string.byte('A') + selectedTeamIndex - 2))
        local _, teamCfg = Tables.charTeamTable:TryGetValue(teamId)
        if teamCfg and teamCfg.presetCharList.Count >= 2 then
            local presetCharId1 = teamCfg.presetCharList[0]
            local charPresetData1 = Tables.charPresetTable:GetValue(presetCharId1)
            local templateId1 = CS.Beyond.Gameplay.CharUtils.GetCharTemplateId(charPresetData1.charId)
            self.view.charImage1:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD, UIConst.UI_CHAR_HEAD_PREFIX .. templateId1)

            local presetCharId2 = teamCfg.presetCharList[1]
            local charPresetData2 = Tables.charPresetTable:GetValue(presetCharId2)
            local templateId2 = CS.Beyond.Gameplay.CharUtils.GetCharTemplateId(charPresetData2.charId)
            self.view.charImage2:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD, UIConst.UI_CHAR_HEAD_PREFIX .. templateId2)
        end
    end
end






DungeonDoubleAssaultEntryCtrl._GetCellRefreshContext = HL.Override().Return(HL.Table) << function(self)
    local context = DungeonDoubleAssaultEntryCtrl.Super._GetCellRefreshContext(self)
    context.cellRedDotName = "ActivityDoubleAssaultDungeon"
    context.cellRedDotArgsBuilder = function(dungeonId, _activityDungeonStateCfg)
        return {
            activityId = self.m_activityId,
            dungeonId = dungeonId,
        }
    end
    context.useDungeonPassedForCompleteState = true
    return context
end





DungeonDoubleAssaultEntryCtrl._MarkCurrentSelectedAsViewed = HL.Override(HL.Any) << function(self, _)
    local curId = self.m_curSelectedDungeonId
    if string.isEmpty(curId) then
        return
    end
    local hasRaid = Tables.dungeonNormal2RaidTable:ContainsKey(curId)
    local normalDungeonId = hasRaid and curId or Tables.dungeonRaid2NormalTable[curId]
    if string.isEmpty(normalDungeonId) then
        return
    end
    ActivityUtils.setFalseNewDoubleAssaultDungeon(normalDungeonId)
end

DungeonDoubleAssaultEntryCtrl.GetRecoverPopupStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local isOpen = UIManager:IsOpen(PanelId.DoubleAssaultCharSelPopup)
    if isOpen then
        return { popupType = "CharSel" }
    end
    return DungeonDoubleAssaultEntryCtrl.Super.GetRecoverPopupStateArg(self)
end

DungeonDoubleAssaultEntryCtrl.TryRecoverPopupState = HL.Override(HL.Any) << function(self, popupState)
    if popupState ~= nil and popupState.popupType == "CharSel" then
        local selectedTeamData = self:_EnsureSelectTeamData(self.m_curSelectedDungeonId)
        if selectedTeamData then
            UIManager:Open(PanelId.DoubleAssaultCharSelPopup, {
                activityId = self.m_activityId,
                gameId = self.m_curSelectedDungeonId,
                teamIndex = selectedTeamData[1],
                onSelect = function(selectedTeamIndex, teamIndex)
                    self.m_selectTeamIndexTable[self.m_curSelectedDungeonId] = { selectedTeamIndex, teamIndex }
                    self:_RefreshCharBtnState()
                end
            })
        end
        return
    end
    DungeonDoubleAssaultEntryCtrl.Super.TryRecoverPopupState(self, popupState)
end











HL.Commit(DungeonDoubleAssaultEntryCtrl)
