local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CommonBannerWidget = HL.Class('CommonBannerWidget', UIWidgetBase)



CommonBannerWidget.m_genPageTabCells = HL.Field(HL.Forward("UIListCache"))

CommonBannerWidget.m_getBannerCellFunc = HL.Field(HL.Function)

CommonBannerWidget.m_onUpdateCellFunc = HL.Field(HL.Function)

CommonBannerWidget.m_onPageChangeFunc = HL.Field(HL.Function)

CommonBannerWidget.m_curPageIndex = HL.Field(HL.Number) << 1

CommonBannerWidget.m_bannerCount = HL.Field(HL.Number) << 0

CommonBannerWidget.m_scrollHoldTime = HL.Field(HL.Number) << 0

CommonBannerWidget.m_isPause = HL.Field(HL.Boolean) << false

CommonBannerWidget.m_isWrappedLoop = HL.Field(HL.Boolean) << false




CommonBannerWidget._OnFirstTimeInit = HL.Override() << function(self)
    self.m_genPageTabCells = UIUtils.genCellCache(self.view.pageTabCell)
    self.m_getBannerCellFunc = UIUtils.genCachedCellFunction(self.view.bannerList)
    self.view.bannerList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getBannerCellFunc(obj)
        if self.m_onUpdateCellFunc ~= nil then
            self.m_onUpdateCellFunc(cell, LuaIndex(csIndex))
        end
    end)
    self.view.bannerList.onDrag:AddListener(function(_)
        self.m_scrollHoldTime = 0
    end)
    self.view.bannerList.onCenterIndexChanged:AddListener(function(oldIndex, newIndex)
        self:_OnManualScroll()
        if self.m_onPageChangeFunc ~= nil then
            self.m_onPageChangeFunc(LuaIndex(oldIndex), LuaIndex(newIndex))
        end
    end)
end

CommonBannerWidget.InitCommonBannerWidget = HL.Method(HL.Any) << function(self, options)







    self.m_bannerCount = 0
    self.m_onUpdateCellFunc = options.onUpdateCell
    self.m_onPageChangeFunc = options.onPageChange
    self.m_isWrappedLoop = options.isWrappedLoop == true
    self:_FirstTimeInit()

    self:_RefreshAllUI()
    self:_StartAutoScroll()
end

CommonBannerWidget.UpdateCount = HL.Method(HL.Number) << function(self, count)
    self.m_bannerCount = count
    self:_RefreshAllUI()
end

CommonBannerWidget.Refresh = HL.Method() << function(self)
    self:_RefreshAllUI(true)
end

CommonBannerWidget.ScrollToIndex = HL.Method(HL.Number) << function(self, luaIndex)
    self.view.bannerList:ScrollToIndex(CSIndex(luaIndex))
    self:_ScrollPageTabToIndex(luaIndex)
end

CommonBannerWidget.OnDestroy = HL.Method() << function(self)
    self:_StopAutoScroll()
end

CommonBannerWidget.SetPause = HL.Method(HL.Boolean) << function(self, isPause)
    self.m_isPause = isPause
end

CommonBannerWidget._OnDestroy = HL.Override() << function(self)
    self:_StopAutoScroll()
end

CommonBannerWidget._RefreshAllUI = HL.Method(HL.Opt(HL.Boolean)) << function(self, isRefresh)
    self.view.bannerList:UpdateCount(self.m_bannerCount, isRefresh ~= true)
    self.m_genPageTabCells:Refresh(self.m_bannerCount, function(cell, luaIndex)
        self:_OnRefreshPageTabCell(cell, luaIndex)
    end)
end

CommonBannerWidget._OnRefreshPageTabCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    cell.toggle.isOn = luaIndex == self.m_curPageIndex
end

CommonBannerWidget._OnManualScroll = HL.Method() << function(self)
    self.m_scrollHoldTime = 0
    local newLuaIndex = LuaIndex(self.view.bannerList.centerIndex)
    if self.m_curPageIndex ~= newLuaIndex then
        self.m_curPageIndex = newLuaIndex
        self:_ScrollPageTabToIndex(newLuaIndex)
    end
end

CommonBannerWidget._ScrollPageTabToIndex = HL.Method(HL.Number) << function(self, index)
    self.m_scrollHoldTime = 0
    for idx, cell in pairs(self.m_genPageTabCells:GetItems()) do
        cell.toggle.isOn = index == idx
    end
end



CommonBannerWidget.m_updateKey = HL.Field(HL.Number) << -1

CommonBannerWidget._StartAutoScroll = HL.Method() << function(self)
    if self.m_updateKey > 0 then
        return
    end

    self.m_scrollHoldTime = 0
    self.m_updateKey = LuaUpdate:Add("Tick", function(deltaTime)
        self:_UpdateAutoScroll(deltaTime)
    end)
end

CommonBannerWidget._UpdateAutoScroll = HL.Method(HL.Number) << function(self, deltaTime)
    if self.m_isPause or self.m_bannerCount == 0 then
        return
    end
    self.m_scrollHoldTime = self.m_scrollHoldTime + deltaTime
    if self.m_scrollHoldTime < self.view.config.AUTO_SCROLL_TIME then
        return
    end

    local nextToHead = self.m_curPageIndex == self.m_bannerCount
    self.m_scrollHoldTime = 0
    self.m_curPageIndex = self.m_curPageIndex % self.m_bannerCount + 1

    if self.m_isWrappedLoop and nextToHead then
        self.view.bannerList:ScrollToIndex(CSIndex(self.m_curPageIndex), true)
    end
    self.view.bannerList:ScrollToIndex(CSIndex(self.m_curPageIndex))
    self:_ScrollPageTabToIndex(self.m_curPageIndex)
end

CommonBannerWidget._StopAutoScroll = HL.Method() << function(self)
    if self.m_updateKey <= 0 then
        return
    end

    LuaUpdate:Remove(self.m_updateKey)
    self.m_updateKey = -1
end

CommonBannerWidget.PageUpOrDown = HL.Method(HL.Boolean) << function(self, pageDown)
    if self.m_isPause then
        return
    end
    if pageDown then
        self.m_curPageIndex = self.m_curPageIndex % self.m_bannerCount + 1
    else
        self.m_curPageIndex = self.m_curPageIndex == 1 and self.m_bannerCount or (self.m_curPageIndex - 1)
    end
    self.view.bannerList:ScrollToIndex(CSIndex(self.m_curPageIndex))
    self:_ScrollPageTabToIndex(self.m_curPageIndex)
end



HL.Commit(CommonBannerWidget)
return CommonBannerWidget

