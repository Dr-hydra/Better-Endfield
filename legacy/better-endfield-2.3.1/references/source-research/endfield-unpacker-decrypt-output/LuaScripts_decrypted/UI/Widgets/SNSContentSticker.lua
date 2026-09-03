local SNSContentWithEmojiComp = require_ex('UI/Widgets/SNSContentWithEmojiComp')

SNSContentSticker = HL.Class('SNSContentSticker', SNSContentWithEmojiComp)

SNSContentSticker._OnSNSContentInit = HL.Override() << function(self)
    SNSContentSticker.Super._OnSNSContentInit(self)

    self.view.stickerImage:LoadSprite(UIConst.UI_SPRITE_SNS_STICKER, self.m_contentCfg.contentParam[0])
end

HL.Commit(SNSContentSticker)
return SNSContentSticker

