local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.WorldLevelPopup
local CHILD_PANEL_TYPE = {
    Tips = "Tips",
    Popup = "Popup",
    Up = "Up",
}

PhaseWorldLevelPopup = HL.Class('PhaseWorldLevelPopup', phaseBase.PhaseBase)





PhaseWorldLevelPopup.s_messages = HL.StaticField(HL.Table) << {

}

PhaseWorldLevelPopup.m_worldLevelPopup = HL.Field(HL.Forward("PhasePanelItem"))

PhaseWorldLevelPopup.m_worldLevelTipsPopup = HL.Field(HL.Forward("PhasePanelItem"))

PhaseWorldLevelPopup.m_worldLevelUp = HL.Field(HL.Forward("PhasePanelItem"))

PhaseWorldLevelPopup.m_worldLevelPreview = HL.Field(HL.Forward("PhasePanelItem"))


PhaseWorldLevelPopup._OnInit = HL.Override() << function(self)
    PhaseWorldLevelPopup.Super._OnInit(self)
end

PhaseWorldLevelPopup.OpenWorldLevelPopup = HL.Method() << function(self)
    self:_RemoveChildPanels()
    self.m_worldLevelPopup = self:CreatePhasePanelItem(PanelId.WorldLevelPopup, { isUp = not GameInstance.player.adventure.isCurWorldLvMax })
end

PhaseWorldLevelPopup.OpenWorldLevelUp = HL.Method(HL.Number) << function(self, targetWorldLevel)
    self:_RemoveChildPanels()
    self.m_worldLevelUp = self:CreatePhasePanelItem(PanelId.WorldLevelUp, { targetWorldLevel = targetWorldLevel })
end

PhaseWorldLevelPopup.BackToTips = HL.Method() << function(self)
    self:_RemoveChildPanels()
    local tipsArg = self.arg and lume.deepCopy(self.arg) or {}
    tipsArg.childPanelType = nil
    tipsArg.childPanelArg = nil
    self.arg = tipsArg
end

PhaseWorldLevelPopup.OpenWorldLevelPreview = HL.Method(HL.Boolean, HL.Number, HL.Number, HL.Any) << function(self, isUp, lastLevel, currentLevel, textKeyTable)
    self:RemovePhasePanelItemById(PanelId.WorldLevelPopup)
    self.m_worldLevelPopup = nil




end

PhaseWorldLevelPopup._RemoveChildPanels = HL.Method() << function(self)
    self:RemovePhasePanelItemById(PanelId.WorldLevelPopup)
    self:RemovePhasePanelItemById(PanelId.WorldLevelUp)
    self.m_worldLevelPopup = nil
    self.m_worldLevelUp = nil
end



PhaseWorldLevelPopup.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseWorldLevelPopup._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self.m_worldLevelTipsPopup = self:_GetPanelPhaseItem(PanelId.WorldLevelTipsPopup)
    local phaseArg = self.arg
    if phaseArg == nil then
        return
    end

    if phaseArg.childPanelType == CHILD_PANEL_TYPE.Popup and phaseArg.childPanelArg ~= nil then
        self:_RemoveChildPanels()
        self.m_worldLevelPopup = self:CreatePhasePanelItem(PanelId.WorldLevelPopup, phaseArg.childPanelArg)
    elseif phaseArg.childPanelType == CHILD_PANEL_TYPE.Up and phaseArg.childPanelArg ~= nil then
        self:_RemoveChildPanels()
        self.m_worldLevelUp = self:CreatePhasePanelItem(PanelId.WorldLevelUp, phaseArg.childPanelArg)
    else
        phaseArg.childPanelType = nil
        phaseArg.childPanelArg = nil
    end
end

PhaseWorldLevelPopup._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseWorldLevelPopup._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseWorldLevelPopup._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseWorldLevelPopup._OnActivated = HL.Override() << function(self)
end

PhaseWorldLevelPopup._OnDeActivated = HL.Override() << function(self)
end

PhaseWorldLevelPopup._OnDestroy = HL.Override() << function(self)
    PhaseWorldLevelPopup.Super._OnDestroy(self)
end



PhaseWorldLevelPopup.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}
    local popupItem = self:_GetPanelPhaseItem(PanelId.WorldLevelPopup)
    if popupItem and popupItem.uiCtrl then
        arg.childPanelType = CHILD_PANEL_TYPE.Popup
        arg.childPanelArg = popupItem.uiCtrl:GetPopupState()
        return arg
    end

    local worldLevelUpItem = self:_GetPanelPhaseItem(PanelId.WorldLevelUp)
    if worldLevelUpItem and worldLevelUpItem.uiCtrl then
        arg.childPanelType = CHILD_PANEL_TYPE.Up
        arg.childPanelArg = worldLevelUpItem.uiCtrl:GetCurPhaseStateArg()
        return arg
    end

    arg.childPanelType = CHILD_PANEL_TYPE.Tips
    arg.childPanelArg = nil
    return arg
end


HL.Commit(PhaseWorldLevelPopup)

