local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FilterBtn = HL.Class('FilterBtn', UIWidgetBase)


FilterBtn._OnFirstTimeInit = HL.Override() << function(self)
    self.view.normalBtn.onClick:AddListener(function()
        self:_OpenFilterPanel()
    end)
    self.view.selectedBtn.onClick:AddListener(function()
        self:_OpenFilterPanel()
    end)
end

FilterBtn.m_args = HL.Field(HL.Table)










FilterBtn.InitFilterBtn = HL.Method(HL.Table) << function(self, args)
    self:_FirstTimeInit()

    self.m_args = args
    local oriOnConfirm = args.onConfirm
    args.onConfirm = function(selectedTags)
        self:_UpdateState(selectedTags)
        oriOnConfirm(selectedTags)
    end

    self:_UpdateState(args.selectedTags)
end

FilterBtn.GetSelectedTags = HL.Method().Return(HL.Table) << function(self)
    if not self.m_args or not self.m_args.selectedTags then
        return {}
    end
    return self.m_args.selectedTags
end

FilterBtn._UpdateState = HL.Method(HL.Table) << function(self, selectedTags)
    self.m_args.selectedTags = selectedTags
    local isSelected = (selectedTags and next(selectedTags)) ~= nil
    self.view.normalBtn.gameObject:SetActive(not isSelected)
    self.view.selectedBtn.gameObject:SetActive(isSelected)
    if self.view.txtFilterTags and isSelected then
        local selectedTagNames = {}
        for _, selectedTag in ipairs(selectedTags) do
            table.insert(selectedTagNames, selectedTag.name)
        end
        self.view.txtFilterTags.text = table.concat(selectedTagNames, "/")
    end
    if isSelected then
        if self.view.txtCount then
            self.view.txtCount.text = tostring(#selectedTags)
        end
    end
end

FilterBtn._OpenFilterPanel = HL.Method() << function(self)
    Notify(MessageConst.SHOW_COMMON_FILTER, self.m_args)
end

HL.Commit(FilterBtn)
return FilterBtn
