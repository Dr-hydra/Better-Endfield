
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivitySkipChapter1

ActivitySkipChapter1Ctrl = HL.Class('ActivitySkipChapter1Ctrl', uiCtrl.UICtrl)

ActivitySkipChapter1Ctrl.s_messages = HL.StaticField(HL.Table) << {

}

ActivitySkipChapter1Ctrl.m_activityId = HL.Field(HL.String) << ''

ActivitySkipChapter1Ctrl.m_info = HL.Field(HL.Table)

ActivitySkipChapter1Ctrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self:_InitVideoInfoAndLoad()

    self.animationWrapper.onAnimationInEasingFinished:AddListener(function()
        self:_StartVideos()
    end)

    self.view.activityCommonInfo:InitActivityCommonInfo(args)

    self.view.activityCommonInfoLuaReference.gotoNode.btnDetailRedDot:InitRedDot("ActivitySkipChapterDetailBtn", self.m_activityId)

    self.view.activityCommonInfoLuaReference.gotoNode.btnDetail.onClick:RemoveAllListeners()
    self.view.activityCommonInfoLuaReference.gotoNode.btnDetail.onClick:AddListener(function()
        if Utils.isInDungeon() then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_SKIP_CHAPTER_IN_CHALLENGE)
            return
        end
        ClientDataManagerInst:SetBool(self.m_activityId .. "_skip_chapter_detail_btn_clicked", true, false, EClientDataTimeValidType.Permanent)
        Notify(MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE)
        UIManager:Open(PanelId.ActivitySkipChapter1Confirm, {
            activityId = self.m_activityId,
        })
    end)
end

ActivitySkipChapter1Ctrl.OnClose = HL.Override() << function(self)
    self.animationWrapper.onAnimationInEasingFinished:RemoveAllListeners()
    self.view.videoPlayer:StopVideo(true)
end

ActivitySkipChapter1Ctrl._InitVideoInfoAndLoad = HL.Method() << function(self)
    local cfg = Tables.activitySkipChapterTable:GetValue(self.m_activityId)
    local videoExist, videoPath = UIUtils.getUIVideoFullPath(UIConst.UI_VIDEO_SKIP_CHAPTER .. cfg.videoPath)
    self.m_info = {
        videoExist = videoExist,
        videoPath = videoPath,
        videoAudioKey = cfg.videoAudioKey,
    }

    if self.m_info.videoExist then
        self.view.videoPlayer:PreloadVideo(self.m_info.videoPath)
    else
        logger.error("跳关活动视频不存在！activityId：", self.m_activityId)
    end

    if Utils.checkIsPSDevice() then

        self.view.videoPlayer.view.aspectRatioFitter.aspectRatio = 16 / 9
    end
end

ActivitySkipChapter1Ctrl._StartVideos = HL.Method() << function(self)
    if self.m_info.videoExist then
        self:_StartPlayVideo()
    end
end

ActivitySkipChapter1Ctrl._StartPlayVideo = HL.Method() << function(self)
    self.view.videoPlayer:PlayVideo(
        self.m_info.videoPath,
        function()
            self.view.videoPlayer:PlayAudio(self.m_info.videoAudioKey)
        end,
        function()
            self:_StartPlayVideo()
        end
    )
end


HL.Commit(ActivitySkipChapter1Ctrl)
