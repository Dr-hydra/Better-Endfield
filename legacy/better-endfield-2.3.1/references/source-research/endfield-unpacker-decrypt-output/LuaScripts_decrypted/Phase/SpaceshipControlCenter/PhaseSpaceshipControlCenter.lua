
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.SpaceshipControlCenter
PhaseSpaceshipControlCenter = HL.Class('PhaseSpaceshipControlCenter', phaseBase.PhaseBase)





PhaseSpaceshipControlCenter.s_messages = HL.StaticField(HL.Table) << {

}

PhaseSpaceshipControlCenter._OnActivated = HL.Override() << function(self)
    local controllerPanel = self.m_panel2Item[PanelId.SpaceshipControlCenter].uiCtrl
    controllerPanel.view.ssBacklogNode:InitSSBacklogNode()
    if not InputManagerInst.controllerNaviManager.curTarget then
        controllerPanel:SetDefaultNaviTarget()
    end
end

PhaseSpaceshipControlCenter.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}
    local isOpen, popupCtrl = UIManager:IsOpen(PanelId.SpaceShipFriendHelpList)
    if isOpen and popupCtrl then
        arg.friendHelpPopupState = {
            roomId = popupCtrl.m_roomId,
        }
    else
        arg.friendHelpPopupState = nil
    end
    return arg
end


HL.Commit(PhaseSpaceshipControlCenter)
