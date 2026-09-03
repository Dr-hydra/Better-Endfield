local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WikiFilterNode = HL.Class('WikiFilterNode', UIWidgetBase)

WikiFilterNode.m_filterGroupCells = HL.Field(HL.Userdata)
WikiFilterNode.m_model = HL.Field(HL.Table)
WikiFilterNode.m_toggles = HL.Field(HL.Table)


WikiFilterNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_filterGroupCells = UIUtils.genCellCache(self.view.filterGroupView)
end

WikiFilterNode.InitWikiFilterNode = HL.Method(HL.Table, HL.Function) << function(self, model, applyCallback)
    self:_FirstTimeInit()

    self.m_model = model
    self.m_toggles = {}
    self.m_filterGroupCells:Refresh(#model, function(cell, index)
        self:_UpdateFilterGroup(cell, self.m_model[index])
    end)


    self.view.btnApply.onClick:RemoveAllListeners()
    self.view.btnApply.onClick:AddListener(function()
        if applyCallback then
            applyCallback()
        end
    end)
    self.view.btnReset.onClick:RemoveAllListeners()
    self.view.btnReset.onClick:AddListener(function()
        for i, v in ipairs(self.m_toggles) do
            v.isOn = false
        end
    end)
end

WikiFilterNode._UpdateFilterGroup = HL.Method(HL.Any, HL.Table) << function(self, view, model)
    view.text.text = model.groupName
    local filterCells = UIUtils.genCellCache(view.cell)
    local filters = model.filters
    filterCells:Refresh(#model.filters, function(cell, index)
        self:_UpdateFilter(cell, filters[index])
    end)

    view.root.sizeDelta = Vector2(view.root.sizeDelta.x, 55 + math.ceil(#model.filters / 2) * 77)
    view.allBtn.onClick:RemoveAllListeners()
    view.allBtn.onClick:AddListener(function()
        for i, v in ipairs(model.filters) do
            filterCells:Get(i).toggle.isOn = true
        end
    end)
end

WikiFilterNode._UpdateFilter = HL.Method(HL.Any, HL.Table) << function(self, view, model)
    view.label.text = model.name
    table.insert(self.m_toggles, view.toggle)
    view.toggle.onValueChanged:RemoveAllListeners()
    view.toggle.onValueChanged:AddListener(function(val)
        model.checked = val
    end)
    view.toggle.isOn = model.checked or false
end

HL.Commit(WikiFilterNode)
return WikiFilterNode

