local SNSContentBase = require_ex('UI/Widgets/SNSContentBase')

SNSContentSystemMsg = HL.Class('SNSContentSystemMsg', SNSContentBase)

SNSContentSystemMsg._OnSNSContentInit = HL.Override() << function(self)
    if self.m_contentInfo.forceSystemMsg then
        return
    end
    self.view.contentTxt:SetAndResolveTextStyle(SNSUtils.resolveTextPlayerName(self.m_contentCfg.content))
end

SNSContentSystemMsg.ManuallyUpdateSNSContentSystemMsg = HL.Method(HL.String) << function(self, content)
    self.view.contentTxt:SetAndResolveTextStyle(SNSUtils.resolveTextPlayerName(content))
end

HL.Commit(SNSContentSystemMsg)
return SNSContentSystemMsg

