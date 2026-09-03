
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.DungeonEntry

local Category2Panel = {

    [DungeonConst.DUNGEON_CATEGORY.SS] = PanelId.DungeonCommonEntry,
    [DungeonConst.DUNGEON_CATEGORY.BossRush] = PanelId.DungeonCommonEntry,
    [DungeonConst.DUNGEON_CATEGORY.CharTutorial] = PanelId.DungeonCommonEntry,
    [DungeonConst.DUNGEON_CATEGORY.Challenge] = PanelId.DungeonCommonEntry,
    [DungeonConst.DUNGEON_CATEGORY.Resource] = PanelId.DungeonCommonEntry,
    [DungeonConst.DUNGEON_CATEGORY.WorldLevel] = PanelId.DungeonCommonEntry,
    [DungeonConst.DUNGEON_CATEGORY.HighDifficulty] = PanelId.DungeonCommonEntry,
    [DungeonConst.DUNGEON_CATEGORY.Train] = PanelId.DungeonTrainEntry,

    [DungeonConst.DUNGEON_CATEGORY.ActMonster] = PanelId.DungeonActivityEntry,
    [DungeonConst.DUNGEON_CATEGORY.DoubleBattle] = PanelId.DungeonDoubleAssaultEntry,
}

PhaseDungeonEntry = HL.Class('PhaseDungeonEntry', phaseBase.PhaseBase)

PhaseDungeonEntry.m_currentPanelItem = HL.Field(HL.Forward("PhasePanelItem"))





PhaseDungeonEntry.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_OPEN_DUNGEON_ENTRY_PANEL] = { 'OnOpenDungeonEntryPanel', false },
    [MessageConst.ENTER_DUNGEON_SKIP_PANEL] = { 'EnterDungeonSkipPanel', false },
}

PhaseDungeonEntry.OnOpenDungeonEntryPanel = HL.StaticMethod(HL.Any) << function(args)
    local id, enterDungeonCallback = unpack(args)
    local isDungeonId = Tables.dungeonTable:ContainsKey(id)
    local isDungeonSeriesId = Tables.dungeonSeriesTable:ContainsKey(id)

    local dungeonId
    local dungeonSeriesId
    if isDungeonId then
        local dungeonCfg = Tables.dungeonTable[id]
        dungeonSeriesId = dungeonCfg.dungeonSeriesId

        dungeonId = id
    elseif isDungeonSeriesId then
        dungeonSeriesId = id
    else
        logger.error("打开PhaseDungeonEntry的参数既不是副本系列id也不是副本id")
        return
    end

    PhaseManager:GoToPhase(PHASE_ID, {
        dungeonSeriesId = dungeonSeriesId,
        dungeonId = dungeonId,
        enterDungeonCallback = enterDungeonCallback,
    })
end

PhaseDungeonEntry.EnterDungeonSkipPanel = HL.StaticMethod(HL.Any) << function(args)
    local dungeonId = unpack(args)
    local hasSubGameInstData, subGameData = DataManager.subGameInstDataTable:TryGetValue(dungeonId)
    if not hasSubGameInstData then
        logger.error("EnterDungeonSkipPanel失败,没有对应的subGameData,subGameId:", dungeonId)
        return
    end

    local teamId = subGameData.teamConfigId
    local charInfo = CharInfoUtils.getLockedFormationData(teamId, true)

    if charInfo then
        local charInfos = {}
        for _, char in ipairs(charInfo.chars) do
            local presetCharInfo = CharInfoUtils.getPlayerCharInfoByInstId(char.charInstId)
            table.insert(charInfos, presetCharInfo)
        end
        if GameInstance.dungeonManager:TryReqEnterDungeon(dungeonId, charInfos) then
            Utils.reportPlacementEvent(GEnums.ClientPlacementEventType.DungeonBattleFirst)
        end
        GameInstance.player.charBag:ClearAllClientCharAndItemData()
    else
        if GameInstance.dungeonManager:TryReqEnterDungeon(dungeonId) then
            Utils.reportPlacementEvent(GEnums.ClientPlacementEventType.DungeonBattleFirst)
        end
    end
end


PhaseDungeonEntry._OnInit = HL.Override() << function(self)
    PhaseDungeonEntry.Super._OnInit(self)
end



PhaseDungeonEntry.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseDungeonEntry._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    local openPanelArgs = self:_PrepareOpenPanelArgs()

    if self.arg.dungeonId then
        openPanelArgs.dungeonId = self.arg.dungeonId
        self.arg.dungeonId = nil
    end
    self.m_currentPanelItem = self:CreatePhasePanelItem(openPanelArgs.panelId, {
        dungeonSeriesId = openPanelArgs.dungeonSeriesId,
        dungeonId = openPanelArgs.dungeonId,
        fromDialog = self.arg.fromDialog,
        enterDungeonCallback = self.arg.enterDungeonCallback,
        activityId = self.arg.activityId
    })
    self:_TryRecoverPopupState()
end

PhaseDungeonEntry._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseDungeonEntry._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseDungeonEntry._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseDungeonEntry._OnActivated = HL.Override() << function(self)
end

PhaseDungeonEntry._OnDeActivated = HL.Override() << function(self)
end

PhaseDungeonEntry._OnDestroy = HL.Override() << function(self)
    PhaseDungeonEntry.Super._OnDestroy(self)
end



PhaseDungeonEntry._OnRefresh = HL.Override() << function(self)
    local openPanelArgs = self:_PrepareOpenPanelArgs()

    if self.m_currentPanelItem then
        self:RemovePhasePanelItem(self.m_currentPanelItem)
    end

    self.m_currentPanelItem = self:CreatePhasePanelItem(openPanelArgs.panelId, {
        dungeonSeriesId = openPanelArgs.dungeonSeriesId,
        dungeonId = openPanelArgs.dungeonId,
        fromDialog = self.arg.fromDialog,
        enterDungeonCallback = self.arg.enterDungeonCallback,
        activityId = self.arg.activityId,
    })
    self:_TryRecoverPopupState()
end

PhaseDungeonEntry._PrepareOpenPanelArgs = HL.Method().Return(HL.Table) << function(self)
    local dungeonId = self.arg.dungeonId
    local dungeonSeriesId = self.arg.dungeonSeriesId
    if string.isEmpty(dungeonSeriesId) then
        dungeonSeriesId = Tables.dungeonTable[dungeonId].dungeonSeriesId
    end
    local dungeonSeriesCfg = Tables.dungeonSeriesTable[dungeonSeriesId]

    local panelId = PanelId.DungeonCommonEntry
    if Category2Panel[dungeonSeriesCfg.gameCategory] then
        panelId = Category2Panel[dungeonSeriesCfg.gameCategory]
    end

    return {
        dungeonId = dungeonId,
        dungeonSeriesId = dungeonSeriesId,
        panelId = panelId,
    }
end

PhaseDungeonEntry.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}
    arg.popupState = nil
    local uiCtrl = self.m_currentPanelItem and self.m_currentPanelItem.uiCtrl
    if uiCtrl and HL.TryGet(uiCtrl, "GetCurSelectDungeonId") then


        arg.dungeonId = uiCtrl:GetCurSelectDungeonId()
    end
    if uiCtrl and PhaseManager:GetTopPhaseId() == PHASE_ID and HL.TryGet(uiCtrl, "GetRecoverPopupStateArg") then
        arg.popupState = uiCtrl:GetRecoverPopupStateArg()
    end
    return arg
end

PhaseDungeonEntry._TryRecoverPopupState = HL.Method() << function(self)
    if self.arg == nil or self.arg.popupState == nil then
        return
    end
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end
    local uiCtrl = self.m_currentPanelItem and self.m_currentPanelItem.uiCtrl
    if uiCtrl == nil or not HL.TryGet(uiCtrl, "TryRecoverPopupState") then
        return
    end

    uiCtrl:TryRecoverPopupState(self.arg.popupState)

    self.arg.popupState = nil
end


HL.Commit(PhaseDungeonEntry)

