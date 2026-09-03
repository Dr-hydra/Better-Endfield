local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

BusinessCardPersonalCollectionNode = HL.Class('BusinessCardPersonalCollectionNode', UIWidgetBase)


BusinessCardPersonalCollectionNode._OnFirstTimeInit = HL.Override() << function(self)
end






BusinessCardPersonalCollectionNode.InitBusinessCardPersonalCollectionNode = HL.Method(HL.Number) << function(self, roleId)
    self:_FirstTimeInit()

    local _ ,friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)
    local info = friendInfo.statisticInfo
    if info == nil then
        logger.error("BusinessCardPersonalCollectionNode.InitBusinessCardPersonalCollectionNode 失败！因为 statisticInfo 为空！")
        return
    end
    self.view.agentNumTxt.text = info.CharNum < 10 and string.format("0%d", info.CharNum) or tostring(info.CharNum)
    self.view.armsNumTxt.text = info.WeaponNum < 10 and string.format("0%d", info.WeaponNum) or tostring(info.WeaponNum)
    self.view.filesNumTxt.text = info.DocNum < 10 and string.format("0%d", info.DocNum) or tostring(info.DocNum)
end

HL.Commit(BusinessCardPersonalCollectionNode)
return BusinessCardPersonalCollectionNode

