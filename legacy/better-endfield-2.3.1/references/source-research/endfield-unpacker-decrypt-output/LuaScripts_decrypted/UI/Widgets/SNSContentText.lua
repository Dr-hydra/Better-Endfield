local SNSContentWithEmojiComp = require_ex('UI/Widgets/SNSContentWithEmojiComp')

SNSContentText = HL.Class('SNSContentText', SNSContentWithEmojiComp)

SNSContentText._OnSNSContentInit = HL.Override() << function(self)
    SNSContentText.Super._OnSNSContentInit(self)

    self.view.textMaxWidth.preferredWidth = self.m_contentInfo.maxTextWidth
    self.view.contentTxt:SetAndResolveTextStyle(SNSUtils.resolveTextStyleWithPlayerName(self.m_contentCfg.content))
end

HL.Commit(SNSContentText)
return SNSContentText

