
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BattlePassSeasonDisplay

BattlePassSeasonDisplayCtrl = HL.Class('BattlePassSeasonDisplayCtrl', uiCtrl.UICtrl)






BattlePassSeasonDisplayCtrl.s_messages = HL.StaticField(HL.Table) << {

}

BattlePassSeasonDisplayCtrl.m_isVideoReady = HL.Field(HL.Boolean) << false

BattlePassSeasonDisplayCtrl.m_videoDelayTimer = HL.Field(HL.Number) << 0

BattlePassSeasonDisplayCtrl.m_preloadVideoTime = HL.Field(HL.Number) << -1


BattlePassSeasonDisplayCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local videoAspectRatio
    if Utils.checkIsPSDevice() then
        videoAspectRatio = self.view.config.VIDEO_ASPECT_RATIO_PS5
    elseif DeviceInfo.isMobile then
        videoAspectRatio = self.view.config.VIDEO_ASPECT_RATIO_MOBILE
    else
        videoAspectRatio = self.view.config.VIDEO_ASPECT_RATIO_PC
    end
    if videoAspectRatio then
        self.view.videoPlayer.view.aspectRatioFitter.aspectRatio = videoAspectRatio
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    local curServerTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local leftSec = GameInstance.player.battlePassSystem.seasonData.closeTime - curServerTime
    local videoExist, videoPath = BattlePassUtils.GetBattlePassIntroVideoPath()
    leftSec = math.max(leftSec, 0)
    self.view.seasonEndReminderText.text = string.format(Language.LUA_BATTLEPASS_NEW_SEASON_PANEL_SEASON_END_TIME, UIUtils.getShortLeftTime(leftSec))
    self.view.maskBtn.onClick:AddListener(function()
        if not videoExist then
            logger.error(ELogChannel.UI, "BP赛季武器视频不存在")
            self:Close()
            if arg ~= nil and arg.onClose ~= nil then
                arg.onClose()
            end
            return
        end
        if not self.m_isVideoReady then

            local currTime = Time.unscaledTime
            if currTime - self.m_preloadVideoTime >= 3.0 then
                logger.error(ELogChannel.UI, "超过3s没有preload成功, 进入保底流程")
                self:Close()
                if arg ~= nil and arg.onClose ~= nil then
                    arg.onClose()
                end
                return
            end
            return
        end
        self.m_videoDelayTimer = TimerManager:StartTimer(self.view.config.VIDEO_PLAY_DELAY, function()
            self.view.videoPlayer:PlayVideo(videoPath)
            self.view.videoPlayer.view.canvasGroup.alpha = 1.0
            AudioAdapter.PostEvent("au_music_cs_battlepass")
            TimerManager:ClearTimer(self.m_videoDelayTimer)
            self.m_videoDelayTimer = 0
        end)
        self:PlayAnimationOutWithCallback(function()
            self:Close()
            if arg ~= nil and arg.onClose ~= nil then
                arg.onClose()
            end
        end)
    end)
    self.view.agreementTxt.text = BattlePassUtils.GetSeasonData().name

    self.m_isVideoReady = false
    self.view.videoPlayer:PreloadVideo(videoPath, function()
        self.m_isVideoReady = true
    end)

    self.m_preloadVideoTime = Time.unscaledTime
end






BattlePassSeasonDisplayCtrl.OnClose = HL.Override() << function(self)
    TimerManager:ClearTimer(self.m_videoDelayTimer)
    self.m_videoDelayTimer = 0
    self.view.videoPlayer:StopVideo(true)
end




HL.Commit(BattlePassSeasonDisplayCtrl)
