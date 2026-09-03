local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

Tabs = HL.Class('Tabs', UIWidgetBase)


Tabs.m_tabCells = HL.Field(HL.Forward('UIListCache'))

Tabs.m_tabList = HL.Field(HL.Table)


Tabs._OnFirstTimeInit = HL.Override() << function(self)
    self.m_tabCells = UIUtils.genCellCache(self.view.tabCell)
end

Tabs.InitTabs = HL.Method(HL.Table) << function(self, tabList)
    self:_FirstTimeInit()

    self.m_tabList = tabList
    self.m_tabCells:Refresh(#tabList, function(cell, index)
        self:_Refresh(cell,index)
    end)
end

Tabs._Refresh = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local tabInfo = self.m_tabList[index]
    local name = tabInfo.name
    local sprite = tabInfo.sprite

    cell.normal.gameObject:SetActive(true)
    cell.highLight.gameObject:SetActive(false)

    if index == 1 then
        cell.high.enabled = true
    else
        cell.high.enabled = false
    end

    if name then
        cell.name.text = name
        cell.nameHigh.text = name
    end
    if sprite then
        cell.icon.gameObject:SetActive(true)
        cell.iconHigh.gameObject:SetActive(true)
        cell.icon.sprite = sprite
        cell.iconHigh.sprite = sprite
    else
        cell.icon.gameObject:SetActive(false)
        cell.iconHigh.gameObject:SetActive(false)
    end
    cell.normal.onClick:RemoveAllListeners()
    cell.normal.onClick:AddListener(function()
        self:ClickTab(index)
    end)
end

Tabs.ClickTab = HL.Method(HL.Number) << function(self, index)
    local tabInfo = self.m_tabList[index]
    local click = tabInfo.click
    local cell = self.m_tabCells:GetItem(index)
    if self.view.lastCell then
        self.view.lastCell.normal.gameObject:SetActive(true)
        self.view.lastCell.highLight.gameObject:SetActive(false)
    end
    self.view.lastCell = cell
    cell.normal.gameObject:SetActive(false)
    cell.highLight.gameObject:SetActive(true)
    if click then
        click(index)
    end
end


HL.Commit(Tabs)
return Tabs
