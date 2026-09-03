local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SNSFriendChatEmotionRight = HL.Class('SNSFriendChatEmotionRight', UIWidgetBase)


SNSFriendChatEmotionRight.InitSNSFriendChatEmotionRight = HL.Method(HL.String) << function(self, imgPath)
    if not string.isEmpty(imgPath) then
        self.view.stickerImage:LoadSprite(imgPath)
    end
end

HL.Commit(SNSFriendChatEmotionRight)
return SNSFriendChatEmotionRight

