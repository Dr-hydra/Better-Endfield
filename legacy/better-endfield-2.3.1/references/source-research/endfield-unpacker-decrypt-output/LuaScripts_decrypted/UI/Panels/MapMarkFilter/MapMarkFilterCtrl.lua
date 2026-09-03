local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkFilter

MapMarkFilterCtrl = HL.Class('MapMarkFilterCtrl', uiCtrl.UICtrl)

MapMarkFilterCtrl.m_categoryCells = HL.Field(HL.Forward("UIListCache"))





MapMarkFilterCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkFilterCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.resetBtn.onClick:AddListener(function()
        GameInstance.player.mapManager:ResetFilterState()
    end)

    self.view.fillBtn.onClick:AddListener(function()
        GameInstance.player.mapManager:FillFilterState()
    end)

    self.view.fullScreenBtn.onClick:AddListener(function()
        if arg.onCloseCallback ~= nil then
            arg.onCloseCallback()
        end
        Notify(MessageConst.HIDE_LEVEL_MAP_FILTER)
    end)

    self:_InitFilterCategoryCells()

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

MapMarkFilterCtrl._InitFilterCategoryCells = HL.Method() << function(self)
    self.m_categoryCells = UIUtils.genCellCache(self.view.markCategoryCell)
    local categoryDataList = {}
    for category, categoryTableData in pairs(Tables.mapMarkCategoryTable) do
        table.insert(categoryDataList, {
            category = category,
            sortId = categoryTableData.sortId,
        })
    end
    table.sort(categoryDataList, Utils.genSortFunction({ "sortId" }, true))

    local count = #categoryDataList
    self.m_categoryCells:Refresh(count, function(cell, index)
        local categoryData = categoryDataList[index]
        cell:InitFilterMarkCategoryCell(categoryData.category)
    end)

    self.view.selectableNaviGroup:NaviToThisGroup()
end

HL.Commit(MapMarkFilterCtrl)
