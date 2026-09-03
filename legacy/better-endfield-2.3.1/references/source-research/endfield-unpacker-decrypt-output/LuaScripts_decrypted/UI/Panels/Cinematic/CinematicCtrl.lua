local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.Cinematic
CinematicCtrl = HL.Class('CinematicCtrl', uiCtrl.UICtrl)







CinematicCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_LOAD_NEW_CUTSCENE] = 'OnLoadNewCinematic',
}

CinematicCtrl.m_timelineHandle = HL.Field(HL.Userdata)

CinematicCtrl.m_debugSkipCounter = HL.Field(HL.Number) << 0

CinematicCtrl.m_fmvNodeMap = HL.Field(HL.Table)

CinematicCtrl.OnLoadNewCinematic = HL.Method(HL.Any) << function(self, arg)
    if arg == nil then
        logger.error("CinematicCtrl.OnLoadNewCinematic handle is nil")
        return
    end

    self.m_timelineHandle = unpack(arg)
    self:OnShow()
end


CinematicCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local handle = unpack(arg)
    self.m_timelineHandle = handle
    self.m_fmvNodeMap = {}


    self.view.exUINode.view.button.onClick:RemoveAllListeners()
    self.view.exUINode.view.button.onClick:AddListener(function()
        self:OnBtnSkipClick()
    end)

    self:_InitBorderMask()
end

CinematicCtrl._InitBorderMask = HL.Method() << function(self, arg)
    local screenWidth = Screen.width
    local screenHeight = Screen.height

    local maxScreenWidth = FMVUtils.MAX_FMV_ASPECT_RATIO * screenHeight
    local borderSize = (screenWidth - maxScreenWidth) / 2
    local ratio = self.view.transform.rect.width / Screen.width

    self.view.leftBorder.transform.sizeDelta = Vector2(borderSize, screenHeight) * ratio
    self.view.rightBorder.transform.sizeDelta = Vector2(borderSize, screenHeight) * ratio
end

CinematicCtrl.OnShow = HL.Override() << function(self)
    self:_ShowCinematic()

    self.view.debugNode.gameObject:SetActive(false)
    if NarrativeUtils.ShouldShowNarrativeDebugNode() then
        local curCutsceneData = GameWorld.cutsceneManager.curMainTimelineData
        self.view.debugNode.gameObject:SetActive(true)
        self.view.textCutsceneId.text = curCutsceneData.cutsceneName
        GameWorld.cutsceneManager:BindFrameDebugTextMarker(self.view.frameDebugText)
    end

    if UNITY_EDITOR and BEYOND_DEBUG then
        GameWorld.cutsceneManager:BindDebugFramingInfo(self.view.textCutsceneFrame)
    end

    local canSkip = Utils.checkCinematicCanSkip(self.m_timelineHandle.data)
    self.view.exUINode.view.button.gameObject:SetActive(canSkip)
    if canSkip then
        self.view.exUINode:InitCinematicExUI()
    end
end

CinematicCtrl.OnHide = HL.Override() << function(self)
    self.view.exUINode:Clear()
end

CinematicCtrl.OnClose = HL.Override() << function(self)
    self.view.exUINode.view.button.onClick:RemoveAllListeners()
    self.view.exUINode:Clear()
    self:ClearFMV()
end

CinematicCtrl._ShowCinematic = HL.Method() << function(self)
    local cinematicMgr = GameWorld.cutsceneManager
    local hasSubtitle = cinematicMgr:BindSubtitle(self.m_timelineHandle, self.view.subtitlePanel)

    for fmvId, fmvPath in pairs(self.m_timelineHandle.loadedPanelFmv) do
        local node = self:GetLoadedFMVNode(fmvId, fmvPath)
        node:SetUserTimeCorrectionThreshold(0)
        node:StartAutoKeepAspectRatio()
        cinematicMgr:BindPanelFMVNode(self.m_timelineHandle, fmvId, node.view.movieController)
    end

    if self.m_timelineHandle.loadedPanelFmv.Count > 0 then
        self.view.fmvGroup.gameObject:SetActive(true)
    else
        self.view.fmvGroup.gameObject:SetActive(false)
    end

    self.view.subtitlePanel.gameObject:SetActive(hasSubtitle)

    cinematicMgr:BindLeftSubtitle(self.m_timelineHandle, self.view.leftSubtitlePanel)

    local hasMask = cinematicMgr:BindMask(self.m_timelineHandle, self.view.mask)
    self.view.mask.gameObject:SetActive(hasMask)
end

CinematicCtrl.OnBtnSkipClick = HL.Method() << function(self)
    local cinematicMgr = GameWorld.cutsceneManager

    self.view.exUINode:SetPause(true)
    cinematicMgr:PauseTimelineByUI(true)
    cinematicMgr:PauseTimeByTimeline(true)
    self:Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_CONFIRM_SKIP_DIALOG,
        onConfirm = function()
            cinematicMgr:PauseTimelineByUI(false)
            cinematicMgr:PauseTimeByTimeline(false)
            cinematicMgr:SkipTimeline(self.m_timelineHandle)
        end,
        onCancel = function()
            self.view.exUINode:SetPause(true)
            cinematicMgr:PauseTimelineByUI(false)
            cinematicMgr:PauseTimeByTimeline(false)
        end
    })
end


CinematicCtrl.StopFMV = HL.Method(HL.String) << function(self, fmvId)
    local fmvNode = self.m_fmvNodeMap[fmvId]
    if not fmvNode then
        return
    end

    fmvNode:StopVideo(true)

    fmvNode.gameObject:SetActive(false)
    self.m_fmvNodeMap[fmvId] = nil
    if next(self.m_fmvNodeMap) == nil then
         self.view.fmvGroup.gameObject:SetActive(false)
    end
end

CinematicCtrl.ClearFMV = HL.Method() << function(self)
    lume.each(lume.keys(self.m_fmvNodeMap), function(fmvId) self:StopFMV(fmvId) end)
end

CinematicCtrl.GetLoadedFMVNode = HL.Method(HL.String, HL.String).Return(HL.Any) << function(self, fmvId, fmvPath)
    if self.m_fmvNodeMap[fmvId] then
        return self.m_fmvNodeMap[fmvId]
    end

    local isOpen, preloader = UIManager:IsOpen(PanelId.VideoPreloader)

    local node = nil
    if isOpen then
        node = preloader:MovePreloadedVideoNode(fmvId, self.view.fmvGroup)
    end

    if node == nil then
        local rawNode = UIUtils.addChild(self.view.fmvGroup, self.view.fmvTemplate)
        node = Utils.wrapLuaNode(rawNode)
        node.gameObject:SetActive(true)
        node:PreloadVideo(fmvPath)
        logger.error("FMV node not preloaded!!! gen new node for fmvId: ", fmvId)
    end

    self.m_fmvNodeMap[fmvId] = node
    return node
end



HL.Commit(CinematicCtrl)
