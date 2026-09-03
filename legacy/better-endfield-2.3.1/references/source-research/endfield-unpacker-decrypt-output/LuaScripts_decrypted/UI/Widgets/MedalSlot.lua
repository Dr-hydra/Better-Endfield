local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

MedalSlot = HL.Class('MedalSlot', UIWidgetBase)

MedalSlot.isEmpty = HL.Field(HL.Boolean) << false

MedalSlot.slotIndex = HL.Field(HL.Number) << -1

MedalSlot.m_dragEnterObj = HL.Field(GameObject)

MedalSlot.m_dragEnterDropItem = HL.Field(HL.Userdata)

MedalSlot.m_dragOptions = HL.Field(HL.Any)

MedalSlot.m_achievementId = HL.Field(HL.String) << ''

MedalSlot._OnFirstTimeInit = HL.Override() << function(self)
    if self.slotIndex == 1 then
        self:SetNaviTarget(self.view.button)
    end
    if self.view.button ~= nil then
        self.view.button.onClick:RemoveAllListeners()
        self.view.button.onClick:AddListener(function()
            self.m_dragOptions.onClick(self.slotIndex, self.m_achievementId)
        end)
    end
end

MedalSlot.InitMedalSlot = HL.Method(HL.Opt(HL.Any, HL.Any, HL.Number, HL.Boolean)) << function(self, medalBundle, dragOptions, slotIndex, isNaviDrag)
    if slotIndex ~= nil then
        self.slotIndex = slotIndex
    end
    self.m_dragOptions = dragOptions
    self:_FirstTimeInit()





    self.isEmpty = medalBundle == nil or string.isEmpty(medalBundle.achievementId)
    if self.view.button ~= nil and isNaviDrag ~= nil then
        local isNaviDrag = isNaviDrag == true
        self.view.button.customBindingViewLabelText = isNaviDrag and Language["key_hint_achievement_edit_putdown"] or Language["key_hint_achievement_edit_take"]
    end
    self.m_achievementId = (not self.isEmpty) and medalBundle.achievementId or ''
    self.view.gameObject.name = medalBundle == nil and "MedalSlot" or "MedalSlot_" .. medalBundle.achievementId
    self.view.medal:InitMedal(medalBundle)
    if not self.view.config.CAN_DRAG_DROP then
        if self.view.dragItem ~= nil then
            self.view.dragItem.enabled = false
        end
        if self.view.dropItem ~= nil then
            self.view.dropItem.enabled = false
        end
        return
    end
    if self.isEmpty then
        if self.view.dragItem ~= nil then
            self.view.dragItem.enabled = false
            self.view.dragItem:ClearEvents()
        end
        if self.view.dropItem ~= nil then
            self.view.dropItem:ClearEvents()
            self.view.dropItem.onDropEvent:AddListener(function(eventData)
                self:_OnDropMedal(eventData, dragOptions.onDropMedal)
            end)
        end
        if dragOptions ~= nil then
            self:_InitDragInfo('', dragOptions)
        end
        return
    end
    if self.view.dropItem ~= nil then
        self.view.dropItem:ClearEvents()
        self.view.dropItem.onDropEvent:AddListener(function(eventData)
            self:_OnDropMedal(eventData, dragOptions.onDropMedal)
        end)
    end
    if self.view.dragItem ~= nil then
        self.view.dragItem:ClearEvents()
        self.view.dragItem.enabled = true
        self.view.dragItem.onBeginDragEvent:AddListener(function(eventData)
            self:_OnBeginDrag(eventData, dragOptions.onBeginDrag)
        end)
        self.view.dragItem.onEndDragEvent:AddListener(function(eventData)
            self:_OnEndDrag(eventData, dragOptions.onEndDrag)
        end)
        self.view.dragItem.onDragEvent:AddListener(function(eventData)
            self:_OnDragMedal(eventData, dragOptions.onDragMedal)
        end)
        self.view.dragItem.onUpdateDragObject:AddListener(function(dragObj)
            local dragMedal = UIWidgetManager:Wrap(dragObj)
            dragMedal:InitMedal(medalBundle)
        end)
    end
    if dragOptions ~= nil then
        self:_InitDragInfo(medalBundle.achievementId, dragOptions)
    end
end

MedalSlot.SetDragState = HL.Method(HL.Boolean) << function(self, isDrag)
    if self.view.stateController ~= nil then
        self.view.stateController:SetState(isDrag and "Drag" or "Normal")
    end
end

MedalSlot._OnDestroy = HL.Override() << function(self)
    self:_ClearLastDropIfNeeded()
end

MedalSlot._InitDragInfo = HL.Method(HL.String, HL.Any) << function(self, achievementId, dragOptions)
    if self.view.dragItem ~= nil then
        if self.view.dragItem.luaTable == nil then
            self.view.dragItem.luaTable = {}
            self.view.dragItem.luaTable[1] = {}
        end
        self.view.dragItem.luaTable[1].achievementId = achievementId
        self.view.dragItem.luaTable[1].dragItem = self.view.dragItem
        self.view.dragItem.luaTable[1].slotType = dragOptions.slotType
        self.view.dragItem.luaTable[1].slotIndex = dragOptions.slotIndex
    end
    if self.view.dropItem ~= nil then
        if self.view.dropItem.luaTable == nil then
            self.view.dropItem.luaTable = {}
            self.view.dropItem.luaTable[1] = {}
        end
        self.view.dropItem.luaTable[1].achievementId = achievementId
        self.view.dropItem.luaTable[1].dropItem = self.view.dropItem
        self.view.dropItem.luaTable[1].slotType = dragOptions.slotType
        self.view.dropItem.luaTable[1].slotIndex = dragOptions.slotIndex
    end
end

MedalSlot._OnDragMedal = HL.Method(CS.UnityEngine.EventSystems.PointerEventData, HL.Function) << function(self, eventData, onDragMedal)
    local curDragObj = self.view.dragItem.curDragObj
    if IsNull(curDragObj) or not curDragObj.activeSelf then
        return
    end
    local enterObj = eventData.pointerEnter
    if enterObj ~= self.m_dragEnterObj then
        local dropItem = nil
        if NotNull(enterObj) then
            dropItem = enterObj:GetComponentInParent(typeof(CS.Beyond.UI.UIDropItem))
            if dropItem and dropItem.luaTable then
                if dropItem ~= self.m_dragEnterDropItem then
                    self:_ClearLastDropIfNeeded()
                end
            else
                self:_ClearLastDropIfNeeded()
            end
        else
            self:_ClearLastDropIfNeeded()
        end
        self.m_dragEnterObj = enterObj
        local dragInfo = self.view.dragItem.luaTable[1]
        local dropInfo = nil
        if dropItem and dropItem.luaTable then
            dropInfo = dropItem.luaTable[1]
            if dropItem ~= self.m_dragEnterDropItem then
                self.m_dragEnterDropItem = dropItem
                dropItem:ToggleHighlight(true)
            end
        end
        if dragInfo and onDragMedal then
            onDragMedal(dragInfo, dropInfo)
        end
    end
end

MedalSlot._OnBeginDrag = HL.Method(CS.UnityEngine.EventSystems.PointerEventData, HL.Function) << function(self, eventData, onBeginDrag)
    self:_ClearLastDropIfNeeded()
    if onBeginDrag then
        onBeginDrag(self.view.dragItem.luaTable[1])
    end
end

MedalSlot._OnEndDrag = HL.Method(CS.UnityEngine.EventSystems.PointerEventData, HL.Function) << function(self, eventData, onEndDrag)
    self:_ClearLastDropIfNeeded()
    if onEndDrag then
        onEndDrag(self.view.dragItem.luaTable[1])
    end
end

MedalSlot._OnDropMedal = HL.Method(CS.UnityEngine.EventSystems.PointerEventData, HL.Function) << function(self, eventData, onDropMedal)
    if IsNull(eventData.pointerDrag) then
        return
    end
    local dragItem = eventData.pointerDrag:GetComponent(typeof(CS.Beyond.UI.UIDragItem))
    if dragItem and dragItem.inDragging and dragItem.luaTable then
        local dragInfo = dragItem.luaTable[1]
        local dropInfo = self.view.dropItem.luaTable[1]
        self.view.dropItem:ToggleHighlight(false)
        if dragInfo and dropInfo and onDropMedal then
            onDropMedal(dragInfo, dropInfo)
        end
    end
end

MedalSlot._ClearLastDropIfNeeded = HL.Method() << function(self)
    if self.m_dragEnterDropItem ~= nil then
        self.m_dragEnterDropItem:ToggleHighlight(false)
        self.m_dragEnterDropItem = nil
    end
    self.m_dragEnterObj = nil
end

HL.Commit(MedalSlot)
return MedalSlot