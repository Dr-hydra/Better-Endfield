local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.RaidMain

RaidMainCtrl = HL.Class('RaidMainCtrl', uiCtrl.UICtrl)






RaidMainCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_WEEK_RAID_MISSION_REFRESH] = "_UpdateCommissionView",
    [MessageConst.ON_WEEK_RAID_BATTLE_PASS_UPDATE] = "_UpdateBattlePassView",
}


RaidMainCtrl.s_needReOpen = HL.StaticField(HL.Boolean) << false

RaidMainCtrl.OnWeeklyRaidQuit = HL.StaticMethod() << function()

    if string.isEmpty(GameInstance.player.weekRaidSystem.currentMainMissionId) and string.isEmpty(GameInstance.player.weekRaidSystem.guideGameId) then
        RaidMainCtrl.s_needReOpen = true
    end
end

RaidMainCtrl.OnPhaseLevelOnTop = HL.StaticMethod() << function()
    if RaidMainCtrl.s_needReOpen then
        PhaseManager:OpenPhase(PhaseId.DungeonWeeklyRaid, nil, nil, true)
        RaidMainCtrl.s_needReOpen = false
    end
end


RaidMainCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self.m_phase:TryCloseTopPanel()
    end)

    self.view.btnIntro.onClick:AddListener(function()
        Notify(MessageConst.SHOW_INTRO, "week_raid")
    end)

    local commissionNode = self.view.commissionNode
    commissionNode.button.onClick:AddListener(function()
        self.m_phase:TryOpenPanel(PanelId.DungeonWeeklyRaid)
    end)
    commissionNode.redDot:InitRedDot("WeekRaidDelegate")

    local battlePassNode = self.view.battlePassNode
    battlePassNode.button.onClick:AddListener(function()
        self.m_phase:TryOpenPanel(PanelId.RaidReward)
    end)
    battlePassNode.redDot:InitRedDot("WeekRaidBattlePass")

    self.view.btnExplore.onClick:AddListener(function()
        local args = { weekRaidArg = {} }
        if PhaseManager:CheckCanOpenPhaseAndToast(PhaseId.CharFormation, args) then
            self:PlayAnimation("raid_main_slc_out", function()
                PhaseManager:OpenPhase(PhaseId.CharFormation, args)
            end)
        end
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

RaidMainCtrl.OnShow = HL.Override() << function(self)
    self:_UpdateView()
end

RaidMainCtrl._UpdateView = HL.Method() << function(self)
    self:_UpdateCommissionView()
    self:_UpdateBattlePassView()
end

RaidMainCtrl._UpdateCommissionView = HL.Method() << function(self)

    local system = GameInstance.player.weekRaidSystem

    local showCommissionTips = not system.IsCurrentMainMissionDependentCompleted
    if showCommissionTips then
        local dependentMissionId = system.currentMainMissionDependentId
        local missionInfo = GameInstance.player.mission:GetMissionInfo(dependentMissionId);
        if missionInfo then
            local missionName = missionInfo.missionName:GetText()
            self.view.commissionTipsText.text = string.format(Language.LUA_WEEK_RAID_MAIN_COMMISSION_TIPS, missionName)
        else
            logger.error("[WeekRaid] Dependent mission not found, id: " .. tostring(dependentMissionId))
            showCommissionTips = false
        end
    end
    self.view.commissionTips.gameObject:SetActive(showCommissionTips)
end

RaidMainCtrl._UpdateBattlePassView = HL.Method() << function(self)

    local system = GameInstance.player.weekRaidSystem
    local battlePassNode = self.view.battlePassNode

    local currentTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local refreshTime = Utils.getNextWeeklyServerRefreshTime()
    local deltaSeconds = refreshTime - currentTime
    local leftTimeText = UIUtils.getLeftTime(deltaSeconds)
    battlePassNode.refreshTipsText.text = string.format(Language["LUA_WEEK_RAID_MAIN_BATTLE_PASS_REFRESH_TIPS"], leftTimeText)

    local score, maxScore = system.battlePassScore, system.battlePassMaxScore
    battlePassNode.scoreText.text = string.format(Language["LUA_WEEK_RAID_MAIN_BATTLE_PASS_SCORE"], score, maxScore)
    battlePassNode.progressImg.fillAmount = maxScore > 0 and (score / maxScore) or 1
end

HL.Commit(RaidMainCtrl)
