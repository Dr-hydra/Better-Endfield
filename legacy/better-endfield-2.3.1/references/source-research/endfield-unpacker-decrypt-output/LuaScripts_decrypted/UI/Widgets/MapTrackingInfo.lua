local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

MapTrackingInfo = HL.Class('MapTrackingInfo', UIWidgetBase)

MapTrackingInfo.m_trackingListCache = HL.Field(HL.Forward("UIListCache"))

MapTrackingInfo.m_mapManager = HL.Field(HL.Userdata)








MapTrackingInfo.m_trackingInfoList = HL.Field(HL.Table)


MapTrackingInfo._OnFirstTimeInit = HL.Override() << function(self)
    self.m_trackingListCache = UIUtils.genCellCache(self.view.cellTracking)
end





MapTrackingInfo.InitMapTrackingInfo = HL.Method(HL.Table) << function(self, args)
    self:_FirstTimeInit()

    self.m_mapManager = GameInstance.player.mapManager
    local hasValue

    self.m_trackingInfoList = {}

    local checkIsShowTrackingData = nil
    if not string.isEmpty(args.domainId) then
        checkIsShowTrackingData = function(levelId)
            return true
        end
    elseif not string.isEmpty(args.levelId) then
        checkIsShowTrackingData = function(levelId)
            return levelId ~= args.levelId
        end
    end


    for index = 0, self.m_mapManager.trackingMissionMarkList.Count - 1 do
        local markInstId = self.m_mapManager.trackingMissionMarkList[index]
        local _, runtimeData = self.m_mapManager:GetMarkInstRuntimeData(markInstId)
        if checkIsShowTrackingData and checkIsShowTrackingData(runtimeData.levelId) then
            local _, markTempData = Tables.mapMarkTempTable:TryGetValue(runtimeData.templateId)
            if markTempData then

                local trackingInfo = {
                    name = runtimeData.missionInfo.missionName:GetText(),
                    instId = runtimeData.instId,
                    levelId = runtimeData.levelId,
                    icon = markTempData.activeIcon,
                    color = Color.white,
                }
                table.insert(self.m_trackingInfoList, trackingInfo)
            end
        end
    end


    if not string.isEmpty(self.m_mapManager.trackingMarkInstId) then

        local markData
        hasValue, markData = self.m_mapManager:GetMarkInstRuntimeData(self.m_mapManager.trackingMarkInstId)
        if hasValue then
            local levelId = self.m_mapManager:GetMarkInstRuntimeDataLevelId(markData.instId)
            if checkIsShowTrackingData and checkIsShowTrackingData(levelId) then

                local markTempData
                hasValue, markTempData = Tables.mapMarkTempTable:TryGetValue(markData.templateId)
                local showText = markTempData.name
                if markTempData.markType == GEnums.MarkType.CustomMark then
                    showText = markData.note
                elseif markTempData.markType == GEnums.MarkType.SnapshotActivity or markTempData.markType == GEnums.MarkType.SnapshotActivityNew then
                    showText = MapUtils.getActivitySnapShotMarkTitle(markData)
                end
                if hasValue then

                    local trackingInfo = {
                        name = showText,
                        icon = markTempData.activeIcon,
                        instId = markData.instId,
                        levelId = self.m_mapManager:GetMarkInstRuntimeDataLevelId(markData.instId),
                        color = Color.white,
                    }
                    table.insert(self.m_trackingInfoList, trackingInfo)
                end
            end
        end
    end

    local trackingCount = #self.m_trackingInfoList
    self.view.main.gameObject:SetActive(trackingCount > 0)
    if trackingCount == 0 then
        return
    end

    self.m_trackingListCache:Refresh(trackingCount, function(cell, index)
        local trackingInfo = self.m_trackingInfoList[index]
        cell.txtTitle.text = trackingInfo.name
        cell.imgTrackIcon:LoadSprite(UIConst.UI_SPRITE_MAP_MARK_ICON_SMALL, trackingInfo.icon)
        cell.imgTrackIcon.color = trackingInfo.color
        cell.btn.onClick:RemoveAllListeners()
        cell.btn.onClick:AddListener(function()
            if DeviceInfo.usingController then
                self.view.listNaviGroup:ManuallyStopFocus()
            end
            if PhaseManager:IsOpen(PhaseId.RegionMap) then
                MapUtils.switchFromRegionMapToLevelMap(trackingInfo.instId, trackingInfo.levelId)
            else
                PhaseManager:GoToPhase(PhaseId.Map, {
                    instId = trackingInfo.instId,
                    levelId = trackingInfo.levelId
                })
            end
        end)
        cell.btn.customBindingViewLabelText = Language.LUA_MAP_TRACKING_CELL_CONFIRM
    end)
end

HL.Commit(MapTrackingInfo)
return MapTrackingInfo

