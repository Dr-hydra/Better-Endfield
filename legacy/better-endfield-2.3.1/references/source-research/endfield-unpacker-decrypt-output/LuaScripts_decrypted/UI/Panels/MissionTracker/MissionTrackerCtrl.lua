
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MissionTracker
MissionTrackerCtrl = HL.Class('MissionTrackerCtrl', uiCtrl.UICtrl)





MissionTrackerCtrl.s_messages = HL.StaticField(HL.Table) << {

}

MissionTrackerCtrl.m_trackerData = HL.Field(HL.Table)
MissionTrackerCtrl.m_trackers = HL.Field(HL.Table)
MissionTrackerCtrl.m_trackersCache = HL.Field(HL.Table)


MissionTrackerCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    if self.m_trackers == nil then
        self.m_trackers = {}
    end
    if self.m_trackersCache == nil then
        self.m_trackersCache = {}
    end
    self.view.tracker.gameObject:SetActive(false)
    self:_StartCoroutine(self:_Tick())
end

MissionTrackerCtrl._Tick = HL.Method().Return(HL.Function) << function(self)
    return function()
        while true do
            self:_UpdateMissionTrackers()
            coroutine.step()
        end
    end
end

MissionTrackerCtrl.UpdateEntityHeadPointDict = HL.Method(HL.Table) << function(self, arg)
    self.m_trackerData = arg
end

MissionTrackerCtrl._UpdateMissionTrackers = HL.Method() << function(self)
    local targetScrPosDict = {}
    local targetDistanceDict = {}
    local targetRadiusDict = {}
    local logicIdList = {}

    self:Notify(MessageConst.UPDATE_MISSION_TRACKER)

    for _, data in pairs(self.m_trackerData) do
        local screenPos, isInside = UIUtils.objectPosToUI(data.worldPos, self.uiCamera)
        table.insert(targetScrPosDict, screenPos)
        table.insert(targetDistanceDict, data.distance)
        table.insert(targetRadiusDict, data.radius)
    end

    if #self.m_trackers > #targetScrPosDict then
        for i = #self.m_trackers, #targetScrPosDict+1, -1 do
            self.m_trackers[i].obj:SetActive(false)
            table.insert(self.m_trackersCache, self.m_trackers[i])
            table.remove(self.m_trackers, i)
        end
    end

    if #self.m_trackers < #targetScrPosDict then
        for i = #self.m_trackers + 1, #targetScrPosDict do
            table.insert(self.m_trackers, self:_CreateNewTracker())
        end
    end

    for i = 1, #targetScrPosDict do
        local item = self.m_trackers[i]
        if item then
            local uiPos, uiAngle, isOutBound = UIUtils.mapScreenPosToEllipseEdge(targetScrPosDict[i], self.view.config.ELLIPSE_X_RADIUS, self.view.config.ELLIPSE_Y_RADIUS)
            item.tracker:UpdatePosition(uiPos, uiAngle, isOutBound, targetDistanceDict[i], targetRadiusDict[i])
        end
    end
end

MissionTrackerCtrl._CreateNewTracker = HL.Method().Return(HL.Table) << function(self)
    local cacheCount = #self.m_trackersCache
    if cacheCount > 0 then
        local cacheObj = self.m_trackersCache[cacheCount]
        cacheObj.obj:SetActive(true)
        table.remove(self.m_trackersCache, cacheCount)
        return cacheObj
    end
    local obj = CSUtils.CreateObject(self.view.tracker.gameObject, self.view.main.transform)
    obj:SetActive(true)
    local item = {}
    item.obj = obj
    item.tracker = obj:GetComponent(typeof(CS.Beyond.UI.UIMissionTracker))
    return item
end









HL.Commit(MissionTrackerCtrl)
