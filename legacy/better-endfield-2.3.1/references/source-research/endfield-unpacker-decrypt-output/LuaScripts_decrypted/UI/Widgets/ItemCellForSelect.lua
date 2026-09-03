local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ItemCellForSelect = HL.Class('ItemCellForSelect', UIWidgetBase)


ItemCellForSelect.m_pressBtnCoroutine = HL.Field(HL.Thread)

ItemCellForSelect.curNum = HL.Field(HL.Number) << 1

ItemCellForSelect.m_max = HL.Field(HL.Number) << 1

ItemCellForSelect.m_onNumChanged = HL.Field(HL.Function)

ItemCellForSelect.m_tryChangeNum = HL.Field(HL.Function)

ItemCellForSelect.m_bindInputChangeNum = HL.Field(HL.Boolean) << false

ItemCellForSelect.m_addNumPressBindingId = HL.Field(HL.Number) << -1

ItemCellForSelect.m_addNumReleaseBindingId = HL.Field(HL.Number) << -1

ItemCellForSelect.m_minusNumPressBindingId = HL.Field(HL.Number) << -1

ItemCellForSelect.m_minusNumReleaseBindingId = HL.Field(HL.Number) << -1


ItemCellForSelect._OnFirstTimeInit = HL.Override() << function(self)
    local addBtn = self.view.item.view.button
    addBtn.onPressStart:AddListener(function()
        self:_OnPressStart(true)
    end)
    addBtn.onPressEnd:AddListener(function()
        self:_OnPressEnd(true)
    end)

    self.view.btnMinus.onPressStart:AddListener(function()
        self:_OnPressStart(false)
    end)
    self.view.btnMinus.onPressEnd:AddListener(function()
        self:_OnPressEnd(false)
    end)
end











ItemCellForSelect.InitItemCellForSelect = HL.Method(HL.Table) << function(self, args)
    self:_FirstTimeInit()

    self.view.item:InitItem(args.itemBundle)
    self.view.item.view.button.enabled = true
    self.curNum = args.curNum
    self.m_max = args.itemBundle.count
    self.m_onNumChanged = args.onNumChanged
    self.m_tryChangeNum = args.tryChangeNum
    self.m_bindInputChangeNum = args.bindInputChangeNum == true
    self:_InitInputBinding()
    self:_UpdateInputBindings()
    self:_UpdateCountShow()
end

ItemCellForSelect._InitInputBinding = HL.Method() << function(self)
    if self.m_bindInputChangeNum then
        local item = self.view.item
        self.m_addNumPressBindingId = item:AddHoverBinding("item_increase_count_press", function()
            AudioAdapter.PostEvent("Au_UI_Button_Item")
            self:_OnPressStart(true)
        end)
        self.m_addNumReleaseBindingId = item:AddHoverBinding("item_increase_count_release", function()
            self:_OnPressEnd(true)
        end)
        self.m_minusNumPressBindingId = item:AddHoverBinding("item_decrease_count_press", function()
            AudioAdapter.PostEvent("Au_UI_Button_Minus")
            self:_OnPressStart(false)
        end)
        self.m_minusNumReleaseBindingId = item:AddHoverBinding("item_decrease_count_release", function()
            self:_OnPressEnd(false)
        end)
        item.view.button.onHoverChange:AddListener(function(isHover)
            if not isHover then
                self:_OnPressEnd(true)
                self:_OnPressEnd(false)
            end
        end)
    end
end

ItemCellForSelect.m_needTriggerOnClick = HL.Field(HL.Boolean) << false

ItemCellForSelect.m_startPressMousePos = HL.Field(Vector3)

local DRAG_MIN_DIST = 10

ItemCellForSelect._OnPressStart = HL.Method(HL.Boolean) << function(self, isAdd)
    if InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.Mouse1) then
        return
    end
    local diff = isAdd and 1 or -1
    self.m_needTriggerOnClick = true
    self.m_pressBtnCoroutine = self:_ClearCoroutine(self.m_pressBtnCoroutine)
    self.m_startPressMousePos = InputManager.mousePosition
    self.m_pressBtnCoroutine = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.NUMBER_SELECTOR_COUNT_REFRESH_INTERVAL)
            self.m_needTriggerOnClick = false
            if Vector3.Distance(self.m_startPressMousePos - InputManager.mousePosition) >= DRAG_MIN_DIST then
                self:_OnPressEnd(isAdd)
                return
            end
            local nextNumber = (math.floor(self.curNum / UIConst.NUMBER_SELECTOR_COUNT_REFRESH_AMOUNT) + diff) * UIConst.NUMBER_SELECTOR_COUNT_REFRESH_AMOUNT
            local audioEventName = isAdd and "Au_UI_Button_Add" or "Au_UI_Button_Minus"
            self:_UpdateCount(nextNumber, audioEventName)
            if not isAdd and self.curNum == 0 then
                self:_OnPressEnd(isAdd)
                return
            end
        end
    end)
end

ItemCellForSelect._OnPressEnd = HL.Method(HL.Boolean) << function(self, isAdd)
    self.m_pressBtnCoroutine = self:_ClearCoroutine(self.m_pressBtnCoroutine)
    if self.m_needTriggerOnClick then
        self.m_needTriggerOnClick = false
        if self.m_startPressMousePos and Vector3.Distance(self.m_startPressMousePos - InputManager.mousePosition) < DRAG_MIN_DIST then
            local diff = isAdd and 1 or -1
            self:_UpdateCount(self.curNum + diff)
        end
    end
end

ItemCellForSelect._OnDisable = HL.Override() << function(self)
    self:_OnPressEnd(true)
    self:_OnPressEnd(false)
end

ItemCellForSelect._UpdateCount = HL.Method(HL.Number, HL.Opt(HL.String)) << function(self, curNum, audioEventName)
    curNum = lume.clamp(curNum, 0, self.m_max)
    if curNum == self.curNum then
        return
    end

    if self.m_tryChangeNum then
        local valid, newNum = self.m_tryChangeNum(curNum)
        if not valid then
            return
        end
        if newNum then

            curNum = newNum
        end
    end
    self.curNum = curNum
    self:_UpdateCountShow()
    self:_UpdateInputBindings()
    if not string.isEmpty(audioEventName) then
        AudioAdapter.PostEvent(audioEventName)
    end
    if self.m_onNumChanged then
        self.m_onNumChanged(curNum)
    end
end

ItemCellForSelect._UpdateCountShow = HL.Method() << function(self)
    local isSelected = self.curNum > 0
    self.view.selectNode.gameObject:SetActive(isSelected)
    if isSelected then
        self.view.selectCount.text = self.curNum
    end
end

ItemCellForSelect._UpdateInputBindings = HL.Method() << function(self)
    local enableMinusNumBinding = self.curNum > 0
    InputManagerInst:ToggleBinding(self.m_minusNumPressBindingId, enableMinusNumBinding)
    InputManagerInst:ToggleBinding(self.m_minusNumReleaseBindingId, enableMinusNumBinding)
end

HL.Commit(ItemCellForSelect)
return ItemCellForSelect
