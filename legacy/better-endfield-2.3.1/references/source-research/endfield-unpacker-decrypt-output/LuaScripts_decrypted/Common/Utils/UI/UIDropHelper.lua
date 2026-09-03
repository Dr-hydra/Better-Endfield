UIDropHelper = HL.Class('UIDropHelper')


UIDropHelper.info = HL.Field(HL.Table)

UIDropHelper.uiDropItem = HL.Field(CS.Beyond.UI.UIDropItem)

UIDropHelper.acceptTypes = HL.Field(HL.Table)

UIDropHelper.isDropArea = HL.Field(HL.Boolean) << false

UIDropHelper.dropPriority = HL.Field(HL.Number) << 0

UIDropHelper.s_dropAreas = HL.StaticField(HL.Table) << {}














UIDropHelper.UIDropHelper = HL.Constructor(CS.Beyond.UI.UIDropItem, HL.Table) << function(self, uiDropItem, info)
    self.uiDropItem = uiDropItem
    uiDropItem.luaTable = {self}
    uiDropItem.onDestroy = function()
        self:_OnDestroy()
    end

    self:RefreshInfo(info)
end

UIDropHelper.RefreshInfo = HL.Method(HL.Table) << function(self, info)
    self.info = info
    self.acceptTypes = info.acceptTypes
    self.isDropArea = info.isDropArea == true
    self.dropPriority = info.dropPriority or 0


    self.uiDropItem.onDropEvent:AddListener(function(eventData)
        self:OnDropItem(eventData)
    end)
    self.uiDropItem.onToggleHighlight:AddListener(function(active)
        self:OnToggleHighlight(active)
    end)

    MessageManager:UnregisterAll(self)
    if self.isDropArea then
        UIDropHelper.s_dropAreas[self] = true
    else
        UIDropHelper.s_dropAreas[self] = nil
    end
end

UIDropHelper.OnDropItem = HL.Method(CS.UnityEngine.EventSystems.PointerEventData) << function(self, eventData)

    if IsNull(eventData.pointerDrag) then
        return
    end
    local dragItem = eventData.pointerDrag:GetComponent(typeof(CS.Beyond.UI.UIDragItem))
    if dragItem and dragItem.inDragging and dragItem.luaTable then
        local drag = dragItem.luaTable[1]
        if drag and self:Accept(drag) then
            if self.info.onDropItem then
                self.info.onDropItem(eventData, drag)
            end
        end
    end
end

UIDropHelper.OnToggleHighlight = HL.Method(HL.Boolean) << function(self, active)
    if self.info.onToggleHighlight then
        self.info.onToggleHighlight(active)
    end
end

UIDropHelper.IsTypeValid = HL.Method(HL.Forward('UIDragHelper')).Return(HL.Boolean) << function(self, dragHelper)
    return UIUtils.isTypeDropValid(dragHelper, self.acceptTypes)
end

UIDropHelper.Accept = HL.Method(HL.Forward('UIDragHelper')).Return(HL.Boolean) << function(self, dragHelper)
    if self:IsTypeValid(dragHelper) then
        if self.info.checkAccept then
            return self.info.checkAccept(dragHelper)
        end
        return true
    end
    return false
end

UIDropHelper.RegisterMessage = HL.Method(HL.Number, HL.Function) << function(self, msg, action)
    MessageManager:Register(msg, function(msgArg)
        action(msgArg)
    end, self)
end

UIDropHelper._OnDestroy = HL.Method() << function(self)
    UIDropHelper.s_dropAreas[self] = nil
    MessageManager:UnregisterAll(self)
    self.uiDropItem.luaTable = nil
    self.uiDropItem.onDestroy = nil
end


HL.Commit(UIDropHelper)
return UIDropHelper
