local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SeasonTowerBuff

local SeasonTowerState = CS.Beyond.Gameplay.Core.BattleManager.SeasonTowerState

SeasonTowerBuffCtrl = HL.Class('SeasonTowerBuffCtrl', uiCtrl.UICtrl)

SeasonTowerBuffCtrl.m_updateKey = HL.Field(HL.Any)





SeasonTowerBuffCtrl.s_messages = HL.StaticField(HL.Table) << {

}

SeasonTowerBuffCtrl.OnShow = HL.Override() << function(self)
    self:_Refresh()
end

SeasonTowerBuffCtrl.OnClose = HL.Override() << function(self)
    self:_StopDrainTick()
end

SeasonTowerBuffCtrl.OnSeasonTowerBuffUpdate = HL.StaticMethod() << function()
    local state = GameWorld.battle.seasonTowerState
    if state == SeasonTowerState.None then
        local succ, ctrl = UIManager:IsOpen(PANEL_ID)
        if succ then
            ctrl:Close()
        end
        return
    end

    local ctrl = SeasonTowerBuffCtrl.AutoOpen(PANEL_ID)
    ctrl:_Refresh()
end

SeasonTowerBuffCtrl._Refresh = HL.Method() << function(self)
    local battle = GameWorld.battle
    local state = battle.seasonTowerState

    if state == SeasonTowerState.Draining then
        self.view.seasonTowerBuffNode:SetState("Effective")
        self:_StartDrainTick()
    else
        self.view.seasonTowerBuffNode:SetState("NoEffective")
        self:_StopDrainTick()
    end

    local maxEnergy = battle.maxSeasonTowerEnergy
    local ratio = 0
    if maxEnergy > 0 then
        ratio = battle.curSeasonTowerEnergy / maxEnergy
    end
    self.view.barImg.fillAmount = ratio
end

SeasonTowerBuffCtrl._StartDrainTick = HL.Method() << function(self)
    if self.m_updateKey then
        return
    end
    self.m_updateKey = self:_StartUpdate(function()
        self:_OnTick()
    end)
end

SeasonTowerBuffCtrl._StopDrainTick = HL.Method() << function(self)
    if not self.m_updateKey then
        return
    end
    self:_RemoveUpdate(self.m_updateKey)
    self.m_updateKey = nil
end

SeasonTowerBuffCtrl._OnTick = HL.Method() << function(self)
    local battle = GameWorld.battle
    local maxEnergy = battle.maxSeasonTowerEnergy
    if maxEnergy > 0 then
        self.view.barImg.fillAmount = battle.curSeasonTowerEnergy / maxEnergy
    end
end

HL.Commit(SeasonTowerBuffCtrl)
