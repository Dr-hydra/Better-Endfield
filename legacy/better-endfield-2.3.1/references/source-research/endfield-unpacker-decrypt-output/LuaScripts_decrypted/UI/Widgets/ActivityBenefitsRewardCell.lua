local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ActivityBenefitsRewardCell = HL.Class('ActivityBenefitsRewardCell', UIWidgetBase)

ActivityBenefitsRewardCell.InitActivityBenefitsRewardCell = HL.Method(HL.Table) << function(self, rewardInfo)

    local fromMain = rewardInfo.fromMain
    local itemId = rewardInfo.itemId
    local obtainNum = rewardInfo.obtain
    local totalNum = rewardInfo.total
    local itemExtraInfo = rewardInfo.itemExtraInfo
    local CompleteAll = rewardInfo.CompleteAll or false


    local item = fromMain and self.view.itemSmall or self.view.itemBig
    item.gameObject:SetActive(true)
    if totalNum > 1 or Tables.itemTable[itemId].type == GEnums.ItemType.Weapon then
        item:InitItem({ id = itemId, count = totalNum }, true)
    else
        item:InitItem({ id = itemId }, true)
    end
    item:SetExtraInfo(itemExtraInfo)


    local isComplete = obtainNum == totalNum
    local pre = isComplete and "<@activitybenefits.grey>" or "<@activitybenefits.blue>"
    local post = isComplete and "<@activitybenefits.grey>" or "<@activitybenefits.black>"
    self.view.numText:SetAndResolveTextStyle(pre .. tostring(obtainNum) .. "</>" .. post .. "/" .. tostring(totalNum) .. "</>")
    self.view.stateController:SetState(CompleteAll and "Gray" or "Normal")
end

HL.Commit(ActivityBenefitsRewardCell)
return ActivityBenefitsRewardCell

