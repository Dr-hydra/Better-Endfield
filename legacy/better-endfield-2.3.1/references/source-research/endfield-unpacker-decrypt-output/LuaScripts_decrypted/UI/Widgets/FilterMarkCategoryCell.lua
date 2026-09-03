local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local MarkInfoType = GEnums.MarkInfoType
FilterMarkCategoryCell = HL.Class('FilterMarkCategoryCell', UIWidgetBase)

local IGNORE_TYPE_ICON_COLOR_LIST = {
    [MarkInfoType.MainMission:GetHashCode()] = true,
    [MarkInfoType.SideMission:GetHashCode()] = true,
    [MarkInfoType.FactoryMission:GetHashCode()] = true,
    [MarkInfoType.WorldMission:GetHashCode()] = true,
    [MarkInfoType.BlocMission:GetHashCode()] = true,
}

FilterMarkCategoryCell.m_category = HL.Field(HL.Number) << -1

FilterMarkCategoryCell.m_typeDataList = HL.Field(HL.Table)

FilterMarkCategoryCell.m_typeCells = HL.Field(HL.Forward("UIListCache"))


FilterMarkCategoryCell._OnFirstTimeInit = HL.Override() << function(self)
    self.m_typeCells = UIUtils.genCellCache(self.view.filterMarkTypeCell)
    self:RegisterMessage(MessageConst.ON_MAP_FILTER_STATE_CHANGED, function(args)
        self:_RefreshCateGoryTypeCellsSelectState()
    end)
end

FilterMarkCategoryCell.InitFilterMarkCategoryCell = HL.Method(HL.Number) << function(self, category)
    self:_FirstTimeInit()

    self.m_category = category

    self:_InitCategoryBasicContent()
    self:_InitCategoryTypeDataList()
    self:_InitCategoryTypeCells()
end

FilterMarkCategoryCell._InitCategoryBasicContent = HL.Method() << function(self)
    local success, categoryData = Tables.mapMarkCategoryTable:TryGetValue(self.m_category)
    if not success then
        return
    end
    self.view.titleText.text = categoryData.name
end

FilterMarkCategoryCell._InitCategoryTypeDataList = HL.Method() << function(self)
    self.m_typeDataList = {}
    for filterType, filterData in pairs(Tables.mapMarkTypeTable) do
        if filterData.category:GetHashCode() == self.m_category then
            local unlockSystemType = filterData.unlockSystemType
            local filterValid = unlockSystemType == nil or unlockSystemType == GEnums.UnlockSystemType.None or Utils.isSystemUnlocked(unlockSystemType)
            if filterValid then
                table.insert(self.m_typeDataList, {
                    type = filterType,
                    name = filterData.name,
                    icon = filterData.icon,
                    sortId = filterData.sortId,
                })
            end
        end
    end
    table.sort(self.m_typeDataList, Utils.genSortFunction({ "sortId" }, true))
end

FilterMarkCategoryCell._InitCategoryTypeCells = HL.Method() << function(self)
    if self.m_typeDataList == nil or #self.m_typeDataList == 0 then
        self.view.gameObject:SetActive(false)
        return
    end

    self.m_typeCells:Refresh(#self.m_typeDataList, function(cell, index)
        local data = self.m_typeDataList[index]
        cell.selectNode.typeName.text = data.name
        cell.notSelectNode.typeName.text = data.name

        cell.typeIcon:LoadSprite(UIConst.UI_SPRITE_MAP_MARK_ICON_SMALL, data.icon)

        cell.button.onClick:RemoveAllListeners()
        cell.button.onClick:AddListener(function()
            local filterActive = GameInstance.player.mapManager:HasFilterFlag(data.type)
            GameInstance.player.mapManager:SetFilterTypeState(data.type, not filterActive)
        end)

        cell.gameObject.name = "MarkFilter"..data.type
    end)
    self.view.gameObject:SetActive(true)

    self:_RefreshCateGoryTypeCellsSelectState()
end

FilterMarkCategoryCell._RefreshCateGoryTypeCellsSelectState = HL.Method() << function(self)
    local count = self.m_typeCells:GetCount()
    for index = 1, count do
        local cell = self.m_typeCells:GetItem(index)
        local data = self.m_typeDataList[index]
        if cell ~= nil and data ~= nil then
            local filterActive = GameInstance.player.mapManager:HasFilterFlag(data.type)
            UIUtils.PlayAnimationAndToggleActive(cell.selectNode.animationWrapper, not filterActive)
            cell.notSelectNode.gameObject:SetActiveIfNecessary(filterActive)

            if IGNORE_TYPE_ICON_COLOR_LIST[data.type] then
                cell.typeIcon.color = Color.white
            else
                cell.typeIcon.color = filterActive and
                    self.view.config.NOT_SELECT_TYPE_CELL_ICON_COLOR or
                    self.view.config.SELECT_TYPE_CELL_ICON_COLOR
            end

            cell.button.customBindingViewLabelText = filterActive and Language.LUA_MAP_FILTER_CELL_SELECT or Language.LUA_MAP_FILTER_CELL_CANCEL_SELECT
        end
    end
end


HL.Commit(FilterMarkCategoryCell)
return FilterMarkCategoryCell

