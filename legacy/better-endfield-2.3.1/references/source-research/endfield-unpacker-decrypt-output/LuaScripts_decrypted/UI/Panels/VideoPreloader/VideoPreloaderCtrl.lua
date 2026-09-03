
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.VideoPreloader

VideoPreloaderCtrl = HL.Class('VideoPreloaderCtrl', uiCtrl.UICtrl)

local EmptyStayTime<const> = 5
local LoadedVideoTimeout<const> = 10 * 60

VideoPreloaderCtrl.m_preloadVideoNode = HL.Field(HL.Table)

VideoPreloaderCtrl.m_leakWatcher = HL.Field(HL.Thread)

VideoPreloaderCtrl.m_emptyTime = HL.Field(HL.Number) << -1





VideoPreloaderCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.RELEASE_PRELOADED_FMV] = "OnReleasePreloadedVideo",
}

VideoPreloaderCtrl.OnPreloadVideo = HL.StaticMethod(HL.Table) << function(arg)
    local isOpen, ctrl = UIManager:IsOpen(PANEL_ID)
    if not isOpen then
        ctrl = UIManager:Open(PANEL_ID, nil)
    end
    local fmvId, path, readyCallback, keepForever = unpack(arg)
    ctrl:PreloadVideo(fmvId, path, readyCallback, keepForever == true)
end

VideoPreloaderCtrl.OnReleasePreloadedVideo = HL.Method(HL.Table) << function(self, arg)
    local fmvId = unpack(arg)
    self:ReleasePreloadedVideo(fmvId)
end


VideoPreloaderCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)





    self.m_leakWatcher = self:_StartCoroutine(function() return self:_LeakWatcher() end)
    self.m_preloadVideoNode = {}
end

VideoPreloaderCtrl._LeakWatcher = HL.Method() << function(self)
    while true do
        local currentTime = Time.unscaledTime

        lume.each(lume.keys(self.m_preloadVideoNode), function(fmvId)
            local node = self.m_preloadVideoNode[fmvId]

            if not node.preloadKeepForever and currentTime - node.preloadStartTime > LoadedVideoTimeout then
                node:StopVideo(true)
                GameObject.Destroy(node.gameObject)
                self.m_preloadVideoNode[fmvId] = nil
            end
        end)

        if next(self.m_preloadVideoNode) == nil then
            if self.m_emptyTime > 0 then
                if currentTime - self.m_emptyTime > EmptyStayTime then
                    self:Close()
                end
            else
                self.m_emptyTime = currentTime
            end
        else
            self.m_emptyTime = -1
        end

        coroutine.wait(2.0)
    end
end

VideoPreloaderCtrl.PreloadVideo = HL.Method(HL.String, HL.String, HL.Any, HL.Boolean) << function(self, fmvId, path, readyCallback, keepForever)
    if self.m_preloadVideoNode[fmvId] then
        return
    end

    local fmvNode = self:GenVideoNode(fmvId)
    fmvNode.preloadKeepForever = keepForever
    fmvNode:PreloadVideo(path,
        function(videoImage)
            if readyCallback then
                readyCallback(videoImage)
            end
        end,
        function(state)
            logger.error("VideoPreloader preload video error", fmvId, path, state)
            self:ReleasePreloadedVideo(fmvId)
            if readyCallback then
                readyCallback(nil)
            end
        end
    )
end

VideoPreloaderCtrl.ReleasePreloadedVideo = HL.Method(HL.String) << function(self, fmvId)
    local node = self.m_preloadVideoNode[fmvId]
    if node then
        node:StopVideo(true)
        GameObject.Destroy(node.gameObject)
        self.m_preloadVideoNode[fmvId] = nil
    end
end

VideoPreloaderCtrl.MovePreloadedVideoNode = HL.Method(HL.String, HL.Any).Return(HL.Any) << function(self, fmvId, newParent)
    local node = self.m_preloadVideoNode[fmvId]
    if not node then
        return nil
    end

    node.transform:SetParent(newParent, false)
    self.m_preloadVideoNode[fmvId] = nil

    return node
end

VideoPreloaderCtrl.GenVideoNode = HL.Method(HL.String, HL.Opt(HL.Boolean)).Return(HL.Any) << function(self, fmvId)
    if self.m_preloadVideoNode[fmvId] then
        return self.m_preloadVideoNode[fmvId]
    end

    local node = UIUtils.addChild(self.view.gameObject, self.view.videoPlayerTemplate)
    self.m_preloadVideoNode[fmvId] = Utils.wrapLuaNode(node)
    return self.m_preloadVideoNode[fmvId]
end

VideoPreloaderCtrl.OnClose = HL.Override() << function(self)
    lume.each(self.m_preloadVideoNode, function(node) node:StopVideo(true) GameObject.Destroy(node.gameObject) end)
    self.m_preloadVideoNode = {}

    if self.m_leakWatcher then
        self:_ClearCoroutine(self.m_leakWatcher)
    end
end


HL.Commit(VideoPreloaderCtrl)
