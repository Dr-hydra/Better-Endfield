local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SettlementDefenseTransit

SettlementDefenseTransitCtrl = HL.Class('SettlementDefenseTransitCtrl', uiCtrl.UICtrl)

SettlementDefenseTransitCtrl.m_time = HL.Field(HL.Number) << 0

SettlementDefenseTransitCtrl.m_timeUpdateTick = HL.Field(HL.Number) << -1

SettlementDefenseTransitCtrl.m_playFinished = HL.Field(HL.Boolean) << false

SettlementDefenseTransitCtrl.m_defendingReady = HL.Field(HL.Boolean) << false

SettlementDefenseTransitCtrl.m_isNormal = HL.Field(HL.Boolean) << true

SettlementDefenseTransitCtrl.m_progressText = HL.Field(HL.Userdata)





SettlementDefenseTransitCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_TOWER_DEFENSE_DEFENDING_READY] = '_OnTowerDefenseDefendingReady',
}


SettlementDefenseTransitCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_time = 0
    self.m_timeUpdateTick = LuaUpdate:Add("Tick", function(deltaTime)
        self.m_time = self.m_time + deltaTime
        local progress = math.min(self.m_time / self.view.config.MIN_PLAY_DURATION, 1)
        self.m_progressText.text = string.format("%02d%%", math.floor(progress * 100))
        if self.m_time >= self.view.config.MIN_PLAY_DURATION then
            self.m_playFinished = true

            self.m_defendingReady = GameInstance.player.towerDefenseSystem.towerDefenseGame.defendingReady
            self.m_timeUpdateTick = LuaUpdate:Remove(self.m_timeUpdateTick)
            self:_TryPopPhase()
        end
    end)
    local _, cfg = Tables.towerDefenseTable:TryGetValue(GameInstance.player.towerDefenseSystem.activeTdId)
    if cfg then
        self.m_isNormal = cfg.tdType == GEnums.TowerDefenseLevelType.Normal
        self.view.stateController:SetState(self.m_isNormal and "normal" or "auto")
    end
    self.animationWrapper:Play(self.m_isNormal and "defensetransit_in_manualdefense" or "defensetransit_in_defense")
    AudioAdapter.PostEvent("Au_UI_Menu_SettlementDefenseTransitPanel_Open")
    self.m_progressText = self.m_isNormal and self.view.manualDefenseNode.numTxt or self.view.defenseNode.numTxt
    AudioManager.PostAudioCue("au_cue_music_base_mode_defense_loading")
end

SettlementDefenseTransitCtrl.OnEnterTowerDefenseDefendingPhase = HL.StaticMethod() << function()
    PhaseManager:OpenPhaseFast(PhaseId.SettlementDefenseTransit)
end

SettlementDefenseTransitCtrl._OnTowerDefenseDefendingReady = HL.Method() << function(self)
    self.m_defendingReady = true
    self:_TryPopPhase()
end

SettlementDefenseTransitCtrl._TryPopPhase = HL.Method() << function(self)
    if not self.m_playFinished then
        return
    end

    if not self.m_defendingReady then
        return
    end

    local isOpen, phaseLevel = PhaseManager:IsOpen(PhaseId.Level)
    if isOpen then
        phaseLevel:PlayTowerDefenseMainCharEffect()
    end

    self:PlayAnimation(self.m_isNormal and "defensetransit_out_manualdefense" or "defensetransit_out_defense", function()
        PhaseManager:PopPhase(PhaseId.SettlementDefenseTransit, function()
            Notify(MessageConst.ON_TOWER_DEFENSE_TRANSIT_FINISHED)
        end)
    end)
end


HL.Commit(SettlementDefenseTransitCtrl)
