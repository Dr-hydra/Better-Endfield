local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

GiftFeatureTagsNode = HL.Class('GiftFeatureTagsNode', UIWidgetBase)


GiftFeatureTagsNode._OnFirstTimeInit = HL.Override() << function(self)
end

GiftFeatureTagsNode.InitGiftFeatureTagsNode = HL.Method(HL.Any).Return(HL.Boolean) << function(self, itemId)
    self:_FirstTimeInit()

    if string.isEmpty(itemId) then
        self.view.gameObject:SetActive(false)
        return false
    end
    local _, giftData = Tables.giftItemTable:TryGetValue(itemId)
    if not giftData then
        self.view.gameObject:SetActive(false)
        return false
    end
    self.view.gameObject:SetActive(true)

    local _, tagData = Tables.tagDataTable:TryGetValue(giftData.giftPreferTag)
    if tagData then
        self.view.areaTxt.text = tagData.tagName
    end
    local _, tagConfig = Tables.giftPreferTagConfigTable:TryGetValue(giftData.giftPreferTag)
    if tagConfig then
        self.view.areaImg:LoadSprite(UIConst.UI_SPRITE_SHIP, tagConfig.iconName)
    end

    local isPopular = giftData.isPopular and Utils.isCurTimeInTimeIdRange(giftData.finishPopularTimeId)
    self.view.hotNode.gameObject:SetActive(isPopular)
    if isPopular then
        local isShowLeftTime = giftData.isShowPopularFinishTime and not string.isEmpty(giftData.finishPopularTimeId)
        self.view.hotTimeNode.gameObject:SetActive(isShowLeftTime)
        if isShowLeftTime then
            local _, timeCfg = Tables.timeRangeTable:TryGetValue(giftData.finishPopularTimeId)
            if timeCfg then
                local serverAreaTypeInt = Utils.getServerAreaType():GetHashCode()
                local timeRange = timeCfg.timeRangeList[CSIndex(serverAreaTypeInt)]
                local timeZoneSeconds = Utils.getServerTimeZoneOffsetSeconds()
                local closeTs = Utils.timeStr2TimeStamp(timeRange.closeTime, timeZoneSeconds)
                self.view.hotTimeNode:StartTickLimitTime(closeTs, Const.POPULAR_EXPIRE_WARNING_TIME)
            else
                self.view.hotTimeNode.gameObject:SetActive(false)
            end
        end
    end
    return true
end

HL.Commit(GiftFeatureTagsNode)
return GiftFeatureTagsNode

