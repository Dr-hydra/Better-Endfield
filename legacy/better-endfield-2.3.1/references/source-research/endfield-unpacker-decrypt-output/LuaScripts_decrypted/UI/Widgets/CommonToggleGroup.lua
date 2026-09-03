local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CommonToggleGroup = HL.Class('CommonToggleGroup', UIWidgetBase)

CommonToggleGroup.m_toggleCache = HL.Field(HL.Forward("UIListCache"))

CommonToggleGroup.m_tweenSelectBg = HL.Field(HL.Userdata)

CommonToggleGroup.m_toggleIsOnAction = HL.Field(HL.Function)

CommonToggleGroup.m_selectBgSiblingIndex = HL.Field(HL.Number) << -1


CommonToggleGroup._OnFirstTimeInit = HL.Override() << function(self)
    self.m_toggleCache = UIUtils.genCellCache(self.view.toggle)
end










CommonToggleGroup.InitCommonToggleGroup = HL.Method(HL.Table) << function(self, arg)
    self:_FirstTimeInit()

    self.m_toggleIsOnAction = arg.onToggleIsOn
    self.m_selectBgSiblingIndex = -1
    local defaultIndex = arg.defaultIndex or 1
    self.m_toggleCache:Refresh(#arg.toggleDataList, function(toggleCell, index)
        toggleCell.toggle.onValueChanged:RemoveAllListeners()
        local toggleData = arg.toggleDataList[index]
        local isDefault = index == defaultIndex
        toggleCell.toggle.isOn = isDefault
        toggleCell.txtName.text = toggleData.name
        toggleCell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self:_OnToggleIsOn(index, toggleCell, true)
            end
        end)
        if isDefault then
            if arg.defaultNotCall ~= true then
                self:_OnToggleIsOn(index, toggleCell, false)
            end
            self.m_selectBgSiblingIndex = self.view.selectedBG.transform:GetSiblingIndex()
            self.view.selectedBG.transform:SetParent(toggleCell.transform, false)
            self.view.selectedBG.transform:SetSiblingIndex(0)
            self.view.selectedBG.transform.localPosition = Vector3.zero
        end
    end)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.transform)
end

CommonToggleGroup._OnDestroy = HL.Override() << function (self)
    if self.m_tweenSelectBg then
        self.m_tweenSelectBg:Kill()
        self.m_tweenSelectBg = nil
    end
end

CommonToggleGroup._OnToggleIsOn = HL.Method(HL.Number, HL.Table, HL.Boolean) << function(self, index, toggleCell, playAnim)
    if self.m_selectBgSiblingIndex > 0 then
        self.view.selectedBG.transform:SetParent(self.view.transform, false)
        self.view.selectedBG.transform:SetSiblingIndex(self.m_selectBgSiblingIndex)
        self.m_selectBgSiblingIndex = -1
    end
    if self.m_tweenSelectBg then
        self.m_tweenSelectBg:Kill()
    end
    if playAnim then
        self.m_tweenSelectBg = DOTween.To(function()
            return self.view.selectedBG.transform.position
        end, function(value)
            self.view.selectedBG.transform.position = value
        end, toggleCell.transform.position, self.config.TWEEN_BG_DURATION)
    else
        self.view.selectedBG.transform.position = toggleCell.transform.position
    end

    if self.m_toggleIsOnAction then
        self.m_toggleIsOnAction(index)
    end
end

HL.Commit(CommonToggleGroup)
return CommonToggleGroup

