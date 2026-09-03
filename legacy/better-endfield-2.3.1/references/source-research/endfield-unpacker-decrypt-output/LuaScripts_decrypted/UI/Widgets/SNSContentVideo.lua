local SNSContentBase = require_ex('UI/Widgets/SNSContentBase')

SNSContentVideo = HL.Class('SNSContentVideo', SNSContentBase)

SNSContentVideo._OnSNSContentInit = HL.Override() << function(self)
    self.view.image:LoadSprite(UIConst.UI_SPRITE_SNS_VIDEO_PREVIEW, self.m_contentCfg.contentParam[0])
    self.view.image.preserveAspect = true

    local videoName = self.m_contentCfg.contentParam[1]
    self.view.playBtn.onClick:RemoveAllListeners()
    self.view.playBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_COMMON_VIDEO, videoName)
    end)

    local time = CS.Beyond.Gameplay.VideoDataTable.instance:GetVideoLength(videoName)
    self.view.timeTxt.text = UIUtils.getLeftTimeToSecond(time)
end



SNSContentVideo.CanSetTarget = HL.Override().Return(HL.Boolean) << function(self)
    return true
end

SNSContentVideo.GetNaviTarget = HL.Override().Return(HL.Any) << function(self)
    return self.view.playBtn
end



HL.Commit(SNSContentVideo)
return SNSContentVideo

