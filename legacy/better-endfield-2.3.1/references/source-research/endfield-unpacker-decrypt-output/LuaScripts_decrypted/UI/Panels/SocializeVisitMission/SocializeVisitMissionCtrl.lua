
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SocializeVisitMission
local PhaseLevelConfig = require_ex("Phase/Level/PhaseLevelConfig")

SocializeVisitMissionCtrl = HL.Class('SocializeVisitMissionCtrl', uiCtrl.UICtrl)

SocializeVisitMissionCtrl.m_listCells = HL.Field(HL.Forward("UIListCache"))






SocializeVisitMissionCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SPACESHIP_ASSIST_DATA_MODIFY] = '_UpdateQuestCell',
    [MessageConst.ON_SPACESHIP_JOIN_FRIEND_INFO_EXCHANGE] = '_UpdateQuestCell',
    [MessageConst.ON_SPACESHIP_CLUE_INFO_SYNC] = '_UpdateQuestCell',
    [MessageConst.ON_LOADING_PANEL_CLOSED] = '_OnSpaceshipVisitFriend',
    [MessageConst.ON_SPACESHIP_VISIT_FRIEND] = '_OnSpaceshipVisitFriend',
}


SocializeVisitMissionCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.exitBtn.onClick:AddListener(function()
        self:_Exit()
    end)

    self.view.visitorBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.SpaceshipControlCenter)
    end)

    self:_UpdateQuestCell()
end

SocializeVisitMissionCtrl.OnShow = HL.Override() << function(self)

end
SocializeVisitMissionCtrl.OnHide = HL.Override() << function(self)

end
SocializeVisitMissionCtrl.OnClose = HL.Override() << function(self)

end

SocializeVisitMissionCtrl._Exit = HL.Method() << function(self)
    Notify(MessageConst.ON_OPEN_VISIT_FRIEND_LIST)
end

SocializeVisitMissionCtrl._UpdateQuestCell = HL.Method(HL.Opt(HL.Table)) << function(self, args)
    if not self.m_listCells then
        self.m_listCells = UIUtils.genCellCache(self.view.questCell)
    end

    local realMissionTable = {}
    for i, missionData in ipairs(SpaceshipUtils.getFriendMissionTable()) do
        if not missionData:finish() then
            table.insert(realMissionTable, missionData)
        end
    end
    if #realMissionTable == 0 then
        table.insert(realMissionTable, {
            showText = Language.LUA_SPACESHIP_VISIT_FRIEND_FINISH_MISSION
        })
    end
    self.m_listCells:Refresh(#realMissionTable, function(cell, index)
        local data = realMissionTable[index]
        cell.objectiveCell.desc.text = data.showText
    end)
end


SocializeVisitMissionCtrl._OnSpaceshipVisitFriend = HL.Method(HL.Opt(HL.Any)) << function(args)
    if not GameInstance.player.spaceship.isViewingFriend then
        return
    end
    GameInstance.player.mapManager:TryAutoClearTrackingMarkOnLevelStart(Tables.spaceshipConst.visitSceneName)
    if PhaseManager:GetTopPhaseId() ~= PhaseId.Level then
        PhaseManager:ExitPhaseFastTo(PhaseId.Level)
    end
    Notify(MessageConst.HIDE_ITEM_TIPS)
    local visitorConfig = PhaseLevelConfig.Config[PhaseLevelConfig.ConditionType.SPACESHIP_VISITOR]
    local keepSet = PhaseLevelConfig.GetWhitelistPanelSet(visitorConfig)

    keepSet[PanelId.LevelCamera] = true
    keepSet[PanelId.HeadLabel] = true
    local toClose = {}
    for panelId, ctrl in pairs(UIManager.m_openedPanels) do
        if not keepSet[panelId] and ctrl:GetCurPanelCfg("closeWhenChangeScene") then
            toClose[#toClose + 1] = panelId
        end
    end
    for _, panelId in ipairs(toClose) do
        local cfg = UIManager.m_panelConfigs[panelId]
        logger.info("PhaseLevel._OnSpaceshipVisitFriend close panel:", cfg and cfg.name or panelId)
        UIManager:Close(panelId)
    end


    UIManager:AutoOpen(PanelId.SpaceShipCharPoster)
end





HL.Commit(SocializeVisitMissionCtrl)
