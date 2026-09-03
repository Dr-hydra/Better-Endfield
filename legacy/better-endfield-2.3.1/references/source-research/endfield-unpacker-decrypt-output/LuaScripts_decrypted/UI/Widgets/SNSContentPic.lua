local SNSContentWithEmojiComp = require_ex('UI/Widgets/SNSContentWithEmojiComp')

SNSContentPic = HL.Class('SNSContentPic', SNSContentWithEmojiComp)

SNSContentPic._OnSNSContentInit = HL.Override() << function(self)
    SNSContentPic.Super._OnSNSContentInit(self)

    local image = SNSUtils.getDiffPicNameByGender(self.m_contentCfg.contentParam)
    local picSprite = self:LoadSprite(UIConst.UI_SPRITE_SNS_PICTURE, image)
    self.view.picImage.sprite = picSprite
    self.view.picRect.sizeDelta = SNSUtils.regulatePicSizeDelta(picSprite)
    self.view.picButton.onClick:RemoveAllListeners()
    self.view.picButton.onClick:AddListener(function()
        Notify(MessageConst.SHOW_COMMON_PICTURE, image)
    end)
end



SNSContentPic.CanSetTarget = HL.Override().Return(HL.Boolean) << function(self)
    return true
end

SNSContentPic.GetNaviTarget = HL.Override().Return(HL.Any) << function(self)
    local target = SNSContentPic.Super.GetNaviTarget(self)
    if target then
        return target
    end
    return self.view.picButton
end



HL.Commit(SNSContentPic)
return SNSContentPic

