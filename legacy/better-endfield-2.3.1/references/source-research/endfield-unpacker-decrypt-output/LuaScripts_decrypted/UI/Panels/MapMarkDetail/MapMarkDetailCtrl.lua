local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetail
local PHASE_ID = PhaseId.MapMarkDetail
local MarkType = GEnums.MarkType
MapMarkDetailCtrl = HL.Class('MapMarkDetailCtrl', uiCtrl.UICtrl)

MapMarkDetailCtrl.m_markInstId = HL.Field(HL.String) << ""





MapMarkDetailCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_TRACKING_MAP_MARK] = '_OnMarkTrackingStateChanged' ,
}


MapMarkDetailCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    local markInstId = args.markInstId

    self.view.closeBtn.onClick:AddListener(function()
        self:_CloseDetail(false)
    end)

    self.view.fullScreenBtn.onClick:AddListener(function()
        self:_CloseDetail(true)
    end)

    self:_SimpleRefreshDetailContent(markInstId)
end

MapMarkDetailCtrl.OnShowLevelMapMarkDetail = HL.StaticMethod(HL.Any) << function(args)
    if PhaseManager:IsPhaseRepeated(PHASE_ID) then
        local ctrl = UIManager:AutoOpen(PANEL_ID)
        ctrl:RefreshDetail(args.markInstId)
    else
        PhaseManager:OpenPhase(PHASE_ID, args)
    end
end

MapMarkDetailCtrl.RefreshDetail = HL.Method(HL.String) << function(self, markInstId)
    self:_SimpleRefreshDetailContent(markInstId)
end

MapMarkDetailCtrl._CloseDetail = HL.Method(HL.Boolean) << function(self, fastMode)
    Notify(MessageConst.HIDE_LEVEL_MAP_MARK_DETAIL)
end

MapMarkDetailCtrl._OnMarkTrackingStateChanged = HL.Method(HL.Any) << function(self, args)
    self:_RefreshTrackButtonText()
end

MapMarkDetailCtrl._SimpleRefreshDetailContent = HL.Method(HL.String) << function(self, markInstId)

    local markSuccess, markData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    if not markSuccess then
        return
    end

    local templateId = markData.templateId
    local tempSuccess, markTemplateData = Tables.mapMarkTempTable:TryGetValue(templateId)
    if not tempSuccess then
        return
    end

    self.m_markInstId = markInstId

    if markData.missionInfo ~= nil then
        self.view.teleportBtn.gameObject:SetActive(false)
        self.view.trackBtn.gameObject:SetActive(false)
    else
        local tpValid = (markTemplateData.markType == MarkType.CampFire or
            markTemplateData.markType == MarkType.HUB)
            and markData.isActive
        if tpValid then
            self.view.teleportBtn.gameObject:SetActive(true)
            self.view.trackBtn.gameObject:SetActive(false)
            self.view.teleportBtn.onClick:AddListener(function()
                GameWorld.dialogManager:SkipCurrentDialog(true)
                if markTemplateData.markType == MarkType.CampFire then

                    Utils.teleportToPosition(markData.levelId, markData.position + Vector3(2, 0 , 0))
                else
                    Utils.teleportToPosition(markData.levelId, markData:GetTeleportPosition())
                end
            end)
        else
            self.view.teleportBtn.gameObject:SetActive(false)
            self.view.trackBtn.gameObject:SetActive(true)
            self.view.trackBtn.onClick:AddListener(function()
                GameInstance.player.mapManager:TrackMark(markInstId, markInstId ~= GameInstance.player.mapManager.trackingMarkInstId)
            end)
            self:_RefreshTrackButtonText()
        end
    end

    local commonNode = self.view.common
    commonNode.title.text.text = markTemplateData.name
    commonNode.desc.text = markTemplateData.desc

    local levelSuccess, levelDesc = Tables.levelDescTable:TryGetValue(markData.levelId)
    if levelSuccess then
        commonNode.subTitle.text.text = levelDesc.showName
    end
end

MapMarkDetailCtrl._RefreshTrackButtonText = HL.Method() << function(self)
    local isTracking = self.m_markInstId == GameInstance.player.mapManager.trackingMarkInstId
    self.view.trackText.text = isTracking and Language["ui_map_common_tracer_cancel"] or Language["ui_map_common_tracer"]
end

HL.Commit(MapMarkDetailCtrl)