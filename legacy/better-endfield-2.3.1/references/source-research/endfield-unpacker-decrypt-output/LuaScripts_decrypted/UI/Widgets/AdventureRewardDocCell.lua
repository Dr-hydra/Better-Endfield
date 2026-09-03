local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

AdventureRewardDocCell = HL.Class('AdventureRewardDocCell', UIWidgetBase)


AdventureRewardDocCell._OnFirstTimeInit = HL.Override() << function(self)
end

AdventureRewardDocCell.m_index = HL.Field(HL.Number) << -1

AdventureRewardDocCell.m_extraSideDocs = HL.Field(HL.Number) << -1

AdventureRewardDocCell.m_rewardInfo = HL.Field(HL.Any) << nil

local REWARD_TYPE_STATE_NAME = {
    [GEnums.AdventureRewardShowType.None] = "Gray",
    [GEnums.AdventureRewardShowType.Blue] = "Blue",
    [GEnums.AdventureRewardShowType.Purple] = "Purple",
    [GEnums.AdventureRewardShowType.Yellow] = "Yellow",
}

AdventureRewardDocCell.InitAdventureRewardDocCell = HL.Method(HL.Any, HL.Number, HL.Number, HL.Number, HL.Number)
    << function(self, levelRewardInfo, docIndex, currIndex, drawOutPercent, extraSideDocs)
    self:_FirstTimeInit()
    self:UpdateDrawOut(currIndex, drawOutPercent)
    self:UpdateCellExpand(currIndex, drawOutPercent)
    self.m_rewardInfo = levelRewardInfo
    self.m_index = docIndex
    self.m_extraSideDocs = extraSideDocs
    local isEmpty = levelRewardInfo == nil
    self.view.viewRoot.gameObject:SetActive(not isEmpty)
    if not isEmpty then
        self.view.docState:SetState(REWARD_TYPE_STATE_NAME[levelRewardInfo.rewardType])
    end
end


AdventureRewardDocCell.UpdateDrawOut = HL.Method(HL.Number, HL.Number) << function(self, currIndex, drawOutPercent)
    local foldHeight = self.view.config.LEVEL_DOC_FOLD_HEIGHT
    local expandHeight = self.view.config.LEVEL_DOC_EXPAND_HEIGHT
    local dist = math.abs(currIndex - self.m_index)
    local distPercent = 1 - math.max(math.min(dist, 1), 0)
    local samplePercent = distPercent * drawOutPercent
    local floorIndex = math.floor(currIndex + 0.5)
    local expandPercent = 1 - math.max(math.min(dist, 1), 0)
    self.view.layoutElement.preferredHeight = foldHeight + expandPercent * drawOutPercent * (expandHeight - foldHeight)
    self.view.docState:SetState(floorIndex == self.m_index and "Select" or "Unselect")
    self.view.animationWrapper:SampleClipAtPercent("adv_reward_document_select", samplePercent)
end

AdventureRewardDocCell.UpdateCellExpand = HL.Method(HL.Number, HL.Number) << function(self, currIndex, drawOutPercent)
    local allIndex = self.m_index + 1 - currIndex + self.m_extraSideDocs
    local allLength = self.m_extraSideDocs * 2 + 1
    local allPercent = 0
    if allLength ~= 0 then
        allPercent = allIndex / allLength
    end
    allPercent = math.max(math.min(allPercent, 1), 0)
    self.view.animationWrapper:SampleClipAtPercent("adv_reward_document_rotation", allPercent)
end

HL.Commit(AdventureRewardDocCell)
return AdventureRewardDocCell