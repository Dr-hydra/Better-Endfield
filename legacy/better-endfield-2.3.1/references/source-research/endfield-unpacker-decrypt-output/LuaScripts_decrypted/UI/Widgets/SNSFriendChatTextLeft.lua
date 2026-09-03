local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SNSFriendChatTextLeft = HL.Class('SNSFriendChatTextLeft', UIWidgetBase)

SNSFriendChatTextLeft.InitSNSFriendChatTextLeft = HL.Method(HL.String) << function(self, text)
    self.view.mainText.text = text
end

HL.Commit(SNSFriendChatTextLeft)
return SNSFriendChatTextLeft

