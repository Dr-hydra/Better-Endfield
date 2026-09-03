local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SNSFriendChatEmotionLeft = HL.Class('SNSFriendChatEmotionLeft', UIWidgetBase)

SNSFriendChatEmotionLeft.InitSNSFriendChatEmotionLeft = HL.Method(HL.String) << function(self, imgPath)
    if not string.isEmpty(imgPath) then
        self.view.stickerImage:LoadSprite(imgPath)
    end
end

HL.Commit(SNSFriendChatEmotionLeft)
return SNSFriendChatEmotionLeft

