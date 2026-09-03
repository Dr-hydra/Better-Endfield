
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MissionHudMini
local ANIMATION_IN_STRETCH = "missionhudmini_in_stretch"
local ANIMATION_OUT_EXTEND = "missionhudmini_out_extend"

MissionHudMiniCtrl = HL.Class('MissionHudMiniCtrl', uiCtrl.UICtrl)






MissionHudMiniCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_TRACK_MISSION_CHANGE] = '_OnTrackMissionChange',
}


MissionHudMiniCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeBtn.onClick:AddListener(function()
        self:_ShrinkPanel()
    end)

    self:_OnTrackMissionChange()

    self.view.openMissionUIBtn.onClick:AddListener(function()
        local trackMission = GameInstance.player.mission.trackMissionId
        if trackMission ~= nil and trackMission ~= "" then
            PhaseManager:OpenPhase(PhaseId.Mission, {autoSelect = trackMission, useBlackMask = true})
        else
            PhaseManager:OpenPhase(PhaseId.Mission, {useBlackMask = true})
        end
    end)

    if arg ~= nil and arg.needAnimationIn then
        local clipName = ANIMATION_IN_STRETCH
        self.view.animationWrapper:PlayWithTween(clipName)
    end
end

MissionHudMiniCtrl._OnTrackMissionChange = HL.Method() << function(self)
    local trackMission = GameInstance.player.mission.trackMissionId
    local trackingValid = (trackMission ~= nil and trackMission ~= "")
    self.view.miniPanel.gameObject:SetActive(trackingValid)
end

MissionHudMiniCtrl._ShrinkPanel = HL.Method() << function(self)
    self.view.animationWrapper:SetAnimationOutClip(ANIMATION_OUT_EXTEND)

    self.view.animationWrapper:PlayOutAnimation(function()
        UIManager:Close(PANEL_ID)
        UIManager:ShowWithKey(PanelId.MissionHud, "MiniPanel")
        local open, missionHud = UIManager:IsOpen(PanelId.MissionHud)
        if not open or missionHud == nil then
            return
        end
        missionHud:SkipMissionBtnAnimIn()
    end)
end

MissionHudMiniCtrl.OnFacTopViewChanged = HL.StaticMethod(HL.Boolean) << function(uselessParam)
    if not DeviceInfo.usingTouch then
        return
    end

    local isTopView = LuaSystemManager.factory.inTopView


    if not isTopView then
        local open, self = UIManager:IsOpen(PanelId.MissionHudMini)
        if not open or self == nil then
            return
        end

        self:_ShrinkPanel()
        return
    else
        local open, missionHud = UIManager:IsOpen(PanelId.MissionHud)
        if not open or missionHud == nil then
            return
        end

        if false then
            return
        end
        UIManager:AutoOpen(PANEL_ID)
        UIManager:HideWithKey(PanelId.MissionHud, "MiniPanel")
    end
end














HL.Commit(MissionHudMiniCtrl)
