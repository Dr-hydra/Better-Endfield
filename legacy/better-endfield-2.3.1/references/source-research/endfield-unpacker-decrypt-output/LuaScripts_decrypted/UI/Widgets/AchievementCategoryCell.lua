local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

AchievementCategoryCell = HL.Class('AchievementCategoryCell', UIWidgetBase)

AchievementCategoryCell.m_cacheCell = HL.Field(HL.Forward("UIListCache"))

AchievementCategoryCell.m_isSelected = HL.Field(HL.Boolean) << false

AchievementCategoryCell.m_haveSub = HL.Field(HL.Boolean) << false


AchievementCategoryCell._OnFirstTimeInit = HL.Override() << function(self)
    self.m_cacheCell = UIUtils.genCellCache(self.view.cell)
end

AchievementCategoryCell.InitAchievementCategoryCell = HL.Method(HL.Any, HL.Number, HL.Any) << function(self, categoryInfo, categoryIndex, options)










    self:_FirstTimeInit()

    local needSetNavi = options.needSetNavi == true
    local groupCount = #categoryInfo.filteredGroups
    self.m_isSelected = categoryIndex == options.selectCategoryIndex
    self.m_haveSub = categoryInfo.haveSub
    local needExpand = (self.m_isSelected and self.m_haveSub) or DeviceInfo.usingController
    local iconPath = UIConst.UI_SPRITE_ACHIEVEMENT
    local name = categoryInfo.data.categoryName
    if options.isSearchMode then
        local achievementCount = 0
        for _, groupInfo in pairs(categoryInfo.filteredGroups) do
            achievementCount = achievementCount + #groupInfo.filteredInfos
        end
        local nameFormat = selected and Language.LUA_ACHIEVEMENT_CATEGORY_SELECT_COUNT_FORMAT or Language.LUA_ACHIEVEMENT_CATEGORY_UNSELECT_COUNT_FORMAT
        name = name .. string.format(nameFormat, achievementCount)
    end
    self.view.button.enabled = not DeviceInfo.usingController or not self.m_haveSub
    self.view.name.text = name
    self.view.stateCtrl:SetState(self.m_haveSub and "HaveSub" or "NoSub")
    self.view.stateCtrl:SetState(self.m_isSelected and "Select" or "Normal")
    self.view.icon:LoadSprite(iconPath, categoryInfo.data.categoryId)
    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        options.onCategoryClick(categoryIndex)
    end)
    self:UpdateArrow(options.isFold)
    self.m_cacheCell:Refresh(needExpand and groupCount or 0, function(groupCell, groupIndex)
        local groupInfo = categoryInfo.filteredGroups[groupIndex]
        if groupInfo == nil then
            return
        end
        local groupSelected = categoryIndex == options.selectCategoryIndex and groupIndex == options.selectGroupIndex
        local groupName = groupInfo.data.groupName
        if options.isSearchMode then
            local nameFormat = selected and Language.LUA_ACHIEVEMENT_CATEGORY_SELECT_COUNT_FORMAT or Language.LUA_ACHIEVEMENT_CATEGORY_UNSELECT_COUNT_FORMAT
            groupName = groupName .. string.format(nameFormat, #groupInfo.filteredInfos)
        end
        groupCell.name.text = groupName
        groupCell.stateCtrl:SetState(groupSelected and "Select" or "Normal")
        groupCell.button.onClick:RemoveAllListeners()
        groupCell.button.onClick:AddListener(function()
            options.onGroupClick(categoryIndex, groupIndex)
        end)
        if options.onGroupCellRender ~= nil then
            options.onGroupCellRender(groupCell, groupIndex)
        end
        if DeviceInfo.usingController and needSetNavi and groupSelected and self.m_isSelected then
            self:SetNaviTarget(groupCell.button)
        end
    end)
    if DeviceInfo.usingController and needSetNavi and self.m_isSelected and not self.m_haveSub then
        self:SetNaviTarget(self.view.button)
    end
end

AchievementCategoryCell.UpdateArrow = HL.Method(HL.Boolean) << function(self, isFold)
    local needExpand = (self.m_isSelected and self.m_haveSub) or DeviceInfo.usingController
    self.view.arrow.localScale = Vector3(1, (needExpand and not isFold) and -1 or 1, 1)
end

HL.Commit(AchievementCategoryCell)
return AchievementCategoryCell

