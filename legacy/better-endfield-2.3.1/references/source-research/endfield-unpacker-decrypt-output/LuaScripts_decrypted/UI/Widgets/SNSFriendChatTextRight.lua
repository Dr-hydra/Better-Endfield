local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SNSFriendChatTextRight = HL.Class('SNSFriendChatTextRight', UIWidgetBase)


SNSFriendChatTextRight.InitSNSFriendChatTextRight = HL.Method(HL.String) << function(self, text)
    self.view.mainText.text = text
end

HL.Commit(SNSFriendChatTextRight)
return SNSFriendChatTextRight

