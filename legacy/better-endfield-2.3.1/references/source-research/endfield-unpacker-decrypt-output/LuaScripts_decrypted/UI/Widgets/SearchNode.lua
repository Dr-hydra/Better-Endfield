local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SearchNode = HL.Class('SearchNode', UIWidgetBase)













SearchNode.m_lastInput = HL.Field(HL.String) << ""

SearchNode.InitSearchNode = HL.Method(HL.Table) << function(self, initInfo)
    local inputField = self.view.inputField


    initInfo.clearBtn = self.view.clearBtn
    initInfo.searchBtn = self.view.searchBtn
    if initInfo.characterLimit then
        initInfo.characterLimit = initInfo.characterLimit
    end


    initInfo.onClearClick = function()
        self:Clear()
    end


    local originalOnEndEdit = initInfo.onEndEdit
    initInfo.onEndEdit = function(newText)
        if newText == self.m_lastInput then
            self:SetInputFieldActive(false)
        end
        self.m_lastInput = newText
        if originalOnEndEdit then
            originalOnEndEdit()
        end
    end


    initInfo.onSearchClick = function()

        if string.isEmpty(self.view.inputField.text) then
            return
        end


        self:SetInputFieldActive(false)


        if initInfo.searchFunc then
            initInfo.searchFunc()
        end
    end


    UIUtils.initSearchInput(inputField, initInfo)


    self.view.controllerInnerSearchBtn.onClick:RemoveAllListeners()
    self.view.controllerInnerSearchBtn.onClick:AddListener(function()
        initInfo.onSearchClick()
    end)

    self.view.inputField.onIsNaviTargetChanged = function(isTarget, isGroupChanged, isOnNaviTargetEnabledAgain)
        if DeviceInfo.usingController and isTarget and isOnNaviTargetEnabledAgain then

            self.view.inputField:ActivateInputField()
        end
    end


    self.view.navigroup.onIsFocusedChange:AddListener(function(active)
        if active then
            self.view.inputField:ActivateInputField()
        else
            self.view.inputField:DeactivateInputField(false)
        end
        self.view.clearBtnNode.gameObject:SetActive(not active)
        self.view.searchBtn.gameObject:SetActive(not active)
        self.view.controllerInnerSearchBtn.gameObject:SetActive(active)
    end)
end

SearchNode.SetInputFieldActive = HL.Method(HL.Boolean) << function(self, active)


    if DeviceInfo.usingController then
        if active then
            self.view.navigroup:ManuallyFocus()
        else
            self.view.navigroup:ManuallyStopFocus()
        end
    else
        if active then
            self.view.inputField:ActivateInputField()
        else
            self.view.inputField:DeactivateInputField(false)
        end
    end
end

SearchNode.Clear = HL.Method() << function(self)

    self.view.inputField.text = ""
    self:SetInputFieldActive(true)
end

HL.Commit(SearchNode)
return SearchNode
