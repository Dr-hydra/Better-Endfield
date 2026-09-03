local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GeneralTracker

local SIGNAL_1 = 1


local LuaNodeCache = require_ex('Common/Utils/LuaNodeCache')


GeneralTrackerCtrl = HL.Class('GeneralTrackerCtrl', uiCtrl.UICtrl)







GeneralTrackerCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.REFRESH_MISSION_TRACK_ALPHA] = '_RefreshMissionTrackAlpha',
    [MessageConst.ON_TRACK_MISSION_CHANGE] = '_OnTrackMissionChange',
    [MessageConst.ON_MISSION_STATE_CHANGE] = '_OnMissionStateChange',
    [MessageConst.ON_QUEST_STATE_CHANGE] = '_OnQuestStateChange',
    [MessageConst.ON_QUEST_OBJECTIVE_UPDATE] = '_OnQuestObjectiveUpdate',
}

GeneralTrackerCtrl.s_missionShowTime = HL.StaticField(HL.Number) << 0

GeneralTrackerCtrl.s_missionTrackerSignal = HL.StaticField(HL.Number) << 0

GeneralTrackerCtrl.s_onShowSignal = HL.Field(HL.Number) << 0

GeneralTrackerCtrl.OnShowMissionTracker = HL.StaticMethod() << function()
    GeneralTrackerCtrl.s_missionTrackerSignal = SIGNAL_1
    GeneralTrackerCtrl.s_missionShowTime = 0
end

GeneralTrackerCtrl.m_rootTransform = HL.Field(HL.Userdata)

GeneralTrackerCtrl.m_missionTrackerTickHandler = HL.Field(HL.Any)


GeneralTrackerCtrl.m_commonTrackerData = HL.Field(HL.Table)

GeneralTrackerCtrl.m_commonTrackers = HL.Field(HL.Table)

GeneralTrackerCtrl.m_commonTrackerTickHandler = HL.Field(HL.Number) << -1

GeneralTrackerCtrl.m_commonTrackerNodeCache = HL.Field(LuaNodeCache)



GeneralTrackerCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_rootTransform = self.view.gameObject:GetComponent(typeof(RectTransform))
    GameInstance.player.commonTrackingSystem.rootTransform = self.m_rootTransform
    GameInstance.player.commonTrackingSystem:UpdateRadiusRate(self.view.config.ELLIPSE_X_RADIUS, self.view.config.ELLIPSE_Y_RADIUS)

    self.m_commonTrackers = {}
    self.m_commonTrackerData = {}
    self.m_commonTrackerNodeCache = LuaNodeCache(self.view.commonTrackerNode, self.view.mainRectTransForm)
    self.view.missionTrackerComp.ALPHA_IN_FIGHT = self.view.config.ALPHA_IN_FIGHT
    self.view.missionTrackerComp.ALPHA_TIME_OUT = self.view.config.ALPHA_TIME_OUT
    self.view.missionTrackerComp.MISSION_TRACKER_TIME_OUT_VALUE = self.view.config.MISSION_TRACKER_TIME_OUT_VALUE
    self.view.missionTrackerComp.rootTransform = self.m_rootTransform
    self.view.missionTrackerComp.missionTrackerGo = self.view.missionTracker.gameObject
    self.view.missionTrackerComp.missionTrackerParentTransform = self.view.missionTrackerParent

    self.view.commonTrackerUpdate.rootTransform = self.m_rootTransform
    self.view.commonTrackerUpdate.templateTrackerGo = self.view.commonTrackerNode.gameObject
    self.view.commonTrackerUpdate.trackerParentTransform = self.m_rootTransform

    self.view.missionTracker.gameObject:SetActive(false)
    self.view.commonTrackerNode.gameObject:SetActive(false)
end

GeneralTrackerCtrl.OnShow = HL.Override() << function(self)
    GeneralTrackerCtrl.s_missionShowTime = 0
    self.s_onShowSignal = 1
    self:_TickCommonTrackers()
    self:_TickMissionTrackers()
    self.m_missionTrackerTickHandler = LuaUpdate:Add("TailTick", function()
        self:_TickCommonTrackers()
        self:_TickMissionTrackers()
    end)
end

GeneralTrackerCtrl.OnHide = HL.Override() << function(self)
    LuaUpdate:Remove(self.m_missionTrackerTickHandler)
    self.m_missionTrackerTickHandler = nil
end

GeneralTrackerCtrl.OnClose = HL.Override() << function(self)
    GameInstance.player.commonTrackingSystem.rootTransform = nil
    LuaUpdate:Remove(self.m_missionTrackerTickHandler)
    self.m_missionTrackerTickHandler = nil
end

GeneralTrackerCtrl._ResetMissionShowTime = HL.StaticMethod() << function()
    GeneralTrackerCtrl.s_missionShowTime = 0
end

GeneralTrackerCtrl._OnTrackMissionChange = HL.Method() << function(self)
    GeneralTrackerCtrl._ResetMissionShowTime()
end

GeneralTrackerCtrl._RefreshMissionTrackAlpha = HL.Method() << function(self)
    GeneralTrackerCtrl._ResetMissionShowTime()
end

GeneralTrackerCtrl._OnMissionStateChange = HL.Method(HL.Any) << function(self, arg)
    local missionId, missionState = unpack(arg)
    GeneralTrackerCtrl._ResetMissionShowTime()
end

GeneralTrackerCtrl._OnQuestStateChange = HL.Method(HL.Any) << function(self, arg)
    local questId, questState = unpack(arg)
    GeneralTrackerCtrl._ResetMissionShowTime()
end

GeneralTrackerCtrl._OnQuestObjectiveUpdate = HL.Method(HL.Any) << function(self, arg)
    local questId = unpack(arg)
    GeneralTrackerCtrl._ResetMissionShowTime()
end

GeneralTrackerCtrl._TickMissionTrackers = HL.Method() << function(self)
    local missionHudOpen, _ = UIManager:IsOpen(PanelId.MissionHud) or UIManager:IsOpen(PanelId.WeeklyRaidTaskTrackHud)
    GeneralTrackerCtrl.s_missionShowTime = GeneralTrackerCtrl.s_missionShowTime + Time.deltaTime

    local trackDataChange = self.view.missionTrackerComp:UpdateMissionTrackers(
        missionHudOpen,
        GeneralTrackerCtrl.s_missionShowTime,
        GeneralTrackerCtrl.s_missionTrackerSignal
    )
    if trackDataChange then
        GeneralTrackerCtrl._ResetMissionShowTime()
    end

    GeneralTrackerCtrl.s_missionTrackerSignal = 0
end



GeneralTrackerCtrl._TickCommonTrackers = HL.Method() << function(self)
    self.view.commonTrackerUpdate:UpdateCommonTrackers(true, self.s_onShowSignal)
    self.s_onShowSignal = 0
end

GeneralTrackerCtrl.PlayAnimationOutWithCallback = HL.Override(HL.Opt(HL.Function)) << function(self, action)
    self.view.commonTrackerUpdate:AllTrackersPlayOutAnimation()
    GeneralTrackerCtrl.Super.PlayAnimationOutWithCallback(self, action)
end




HL.Commit(GeneralTrackerCtrl)
