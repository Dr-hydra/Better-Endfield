local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GachaPoolVideo

GachaPoolVideoCtrl = HL.Class('GachaPoolVideoCtrl', uiCtrl.UICtrl)






GachaPoolVideoCtrl.s_messages = HL.StaticField(HL.Table) << {

}


GachaPoolVideoCtrl.m_info = HL.Field(HL.Table)

GachaPoolVideoCtrl.m_eventLogInfo = HL.Field(HL.Table)




GachaPoolVideoCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self:_InitData(arg)
    self:_RefreshAllUI()

    EventLogManagerInst:GameEvent_GachaVideoStart(
        self.m_eventLogInfo.videoId,
        self.m_eventLogInfo.charId
    )
end

GachaPoolVideoCtrl.OnClose = HL.Override() << function(self)
    self.view.videoPlayer:StopVideo(true)


    local endTs = DateTimeUtils.GetCurrentTimestampByMilliseconds()
    local stayTime = (endTs - self.m_eventLogInfo.startTs) / 1000
    EventLogManagerInst:GameEvent_GachaVideoEnd(
        self.m_eventLogInfo.videoId,
        self.m_eventLogInfo.charId,
        self.m_eventLogInfo.startTs,
        endTs,
        self.m_eventLogInfo.videoTime,
        self.m_eventLogInfo.isPlayFinish,
        stayTime
    )
end

GachaPoolVideoCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    return self.m_info and self.m_info.poolId or nil
end



GachaPoolVideoCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    local poolId = arg
    local _, cfg = Tables.gachaCharPoolTable:TryGetValue(poolId)
    local videoExist, videoPath = UIUtils.getUIVideoFullPath(UIConst.UI_VIDEO_GACHA .. cfg.videoPath)

    self.m_info = {
        poolId = poolId,
        videoExist = videoExist,
        videoPath = videoPath,
        videoAudioKey = cfg.videoAudioKey,
        videoDesc = cfg.videoDesc,
    }

    self.m_eventLogInfo = {
        videoId = videoPath,
        charId = cfg.upCharIds[0],

        startTs = DateTimeUtils.GetCurrentTimestampByMilliseconds(),
        videoTime = 0,
        isPlayFinish = false,
    }
end



GachaPoolVideoCtrl._InitUI = HL.Method() << function(self)
    self.view.closeBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

GachaPoolVideoCtrl._RefreshAllUI = HL.Method() << function(self)
    if self.m_info.videoExist then
        self.view.descTxt.text = self.m_info.videoDesc
        self.view.videoPlayer:PreloadVideo(self.m_info.videoPath, function()
            self.m_eventLogInfo.videoTime = self.view.videoPlayer:GetVideoTotalTime()
            self:_StartPlayVideo()
        end)
    else
        logger.error("卡池角色展示视频不存在！卡池id：", self.m_info.poolId)
    end
end



GachaPoolVideoCtrl._StartPlayVideo = HL.Method() << function(self)
    self.view.videoPlayer:PlayVideo(
        self.m_info.videoPath,
        function()
            self.view.videoPlayer:PlayAudio(self.m_info.videoAudioKey)
        end,
        function()
            self:_ReplayVideo()
        end
    )
end



GachaPoolVideoCtrl._ReplayVideo = HL.Method() << function(self)
    logger.info("[GachaPoolVideoTest] restart video")
    self.m_eventLogInfo.isPlayFinish = true
    self.view.videoPlayer:ReplayVideo(
        function()
            self.view.videoPlayer:PlayAudio(self.m_info.videoAudioKey)
        end,
        function()
            self:_ReplayVideo()
        end
    )
end


HL.Commit(GachaPoolVideoCtrl)
