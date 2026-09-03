local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

MedalGroup = HL.Class('MedalGroup', UIWidgetBase)

MedalGroup.m_getMedalSlotCell = HL.Field(HL.Function)

MedalGroup.m_medalMap = HL.Field(HL.Any)

MedalGroup.m_medalSlotList = HL.Field(HL.Any)

MedalGroup.m_maxSlot = HL.Field(HL.Number) << 10

MedalGroup.m_minU = HL.Field(HL.Number) << 0

MedalGroup.m_maxU = HL.Field(HL.Number) << 0

MedalGroup.m_minV = HL.Field(HL.Number) << 0

MedalGroup.m_maxV = HL.Field(HL.Number) << 0

MedalGroup.m_cellCount = HL.Field(HL.Number) << 0

MedalGroup.m_dragOptions = HL.Field(HL.Any) << nil

MedalGroup.m_dragMedalSlot = HL.Field(HL.Number) << -1

MedalGroup.m_inNaviDrag = HL.Field(HL.Boolean) << false


local MEDAL_SLOT_CONFIG = {
    [1] = { posU = 0, posV = 0 },
    [2] = { posU = 0, posV = 1 },
    [3] = { posU = 1, posV = 0 },
    [4] = { posU = 1, posV = 1 },
    [5] = { posU = 2, posV = 0 },
    [6] = { posU = 2, posV = 1 },
    [7] = { posU = 3, posV = 0 },
    [8] = { posU = 3, posV = 1 },
    [9] = { posU = 4, posV = 0 },
    [10] = { posU = 4, posV = 1 },


















}

MedalGroup._OnFirstTimeInit = HL.Override() << function(self)
    self.m_getMedalSlotCell = UIUtils.genCachedCellFunction(self.view.slotList)
    self.view.slotList.onUpdateCell:AddListener(function(object, csIndex)
        local cell = self.m_getMedalSlotCell(object)
        self:_OnUpdateCell(cell, csIndex)
    end)
end

MedalGroup._GetMedalSlotByIndex = HL.Method(HL.Number).Return(HL.Number) << function(self, csIndex)
    local rowCount = self.m_maxV - self.m_minV + 1
    local posU = 0
    local posV = 0
    if rowCount ~= 0 then
        local colIndex = csIndex // rowCount
        local rowIndex = csIndex % rowCount
        posU = colIndex + self.m_minU
        posV = rowIndex + self.m_minV
    end
    local slotIndex = -1
    for slot, pos in pairs(MEDAL_SLOT_CONFIG) do
        if pos.posU == posU and pos.posV == posV then
            slotIndex = slot
            break
        end
    end
    return slotIndex
end

MedalGroup._GetMedalPosBySlot = HL.Method(HL.Number).Return(HL.Number, HL.Number) << function(self, slotIndex)
    local slotConfig = MEDAL_SLOT_CONFIG[slotIndex]
    if slotConfig ~= nil then
        return slotConfig.posU, slotConfig.posV
    end
    return nil, nil
end

MedalGroup._GetMedalIndexByPos = HL.Method(HL.Number, HL.Number).Return(HL.Number) << function(self, posU, posV)
    local rowCount = self.m_maxV - self.m_minV + 1
    local colIndex = posU - self.m_minU
    local rowIndex = posV - self.m_minV
    return rowIndex + colIndex * rowCount
end

MedalGroup._GetMedalIndexBySlot = HL.Method(HL.Number).Return(HL.Number) << function(self, slotIndex)
    local posU, posV = self:_GetMedalPosBySlot(slotIndex)
    if posU == nil or posV == nil then
        return -1
    end
    return self:_GetMedalIndexByPos(posU, posV)
end

MedalGroup._InitSlotCells = HL.Method(HL.Number) << function(self, maxSlot)

    self.m_minU = 0
    self.m_minV = 0
    self.m_maxU = 0
    self.m_maxV = 0
    self.m_cellCount = 0
    for i = 1, maxSlot do
        local posU, posV = self:_GetMedalPosBySlot(i)
        if posU ~= nil and posV ~= nil then
            self.m_minU = math.min(posU, self.m_minU)
            self.m_minV = math.min(posV, self.m_minV)
            self.m_maxU = math.max(posU, self.m_maxU)
            self.m_maxV = math.max(posV, self.m_maxV)
        end
    end
    local colCount = self.m_maxU - self.m_minU + 1
    local rowCount = self.m_maxV - self.m_minV + 1
    self:_InitGroupSize(rowCount)
    self.m_cellCount = colCount * rowCount
    local cellList = {}

    for row = self.m_minV, self.m_maxV do
        for col = self.m_minU, self.m_maxU do
            local rowIndex = row - self.m_minV
            local colIndex = col - self.m_minU
            cellList[rowIndex + colIndex * rowCount] = {}
            local cell = cellList[rowIndex + colIndex * rowCount]
            cell.pos = {
                posU = col,
                posV = row,
            }
            cell.isValid = false
            cell.medalSlot = nil
            cell.medalBundle = nil
        end
    end
    for i = 1, maxSlot do
        local posU, posV = self:_GetMedalPosBySlot(i)
        if posU ~= nil and posV ~= nil then
            local cellIndex = self:_GetMedalIndexByPos(posU, posV)
            local cell = cellList[cellIndex]
            cell.isValid = true
            cell.medalSlot = i
        end
    end
    self.m_medalSlotList = cellList
end

MedalGroup._InitGroupSize = HL.Method(HL.Number) << function(self, rowCount)
    local groupHeight = rowCount * self.config.MEDAL_GROUP_CELL_HEIGHT + (rowCount + 1) * self.config.MEDAL_GROUP_CELL_SPACEY
    UIUtils.setSizeDeltaY(self.view.transform, groupHeight)
end

MedalGroup._UpdateSlotCells = HL.Method(HL.Opt(HL.Any)) << function(self, medalMap)
    if medalMap == nil then
        return
    end
    for slotIndex, medalBundle in pairs(medalMap) do
        local posU, posV = self:_GetMedalPosBySlot(slotIndex)
        if posU ~= nil and posV ~= nil then
            local cellIndex = self:_GetMedalIndexByPos(posU, posV)
            local cell = self.m_medalSlotList[cellIndex]
            if cell ~= nil then
                cell.medalBundle = medalBundle
            end
        end
    end
end

MedalGroup.InitMedalGroup = HL.Method(HL.Opt(HL.Any, HL.Number, HL.Any)) << function(self, medalMap, maxSlot, dragOptions)




    self.m_dragOptions = dragOptions
    self.m_maxSlot = maxSlot
    self.m_medalMap = medalMap
    self:_FirstTimeInit()
    self:_InitSlotCells(maxSlot)
    self:_UpdateSlotCells(medalMap)
    self.view.slotList:UpdateCount(self.m_cellCount, false, false, false, true)
end

MedalGroup.GetFirstEmptySlot = HL.Method().Return(HL.Number, HL.Number) << function(self)
    for i = 1, self.m_maxSlot do
        local slotCellIndex = self:_GetMedalIndexBySlot(i)
        if slotCellIndex >= 0 then
            local slotCell = self.m_medalSlotList[slotCellIndex]
            if slotCell ~= nil and slotCell.isValid and slotCell.medalBundle == nil then
                return i, slotCellIndex
            end
        end
    end
    return -1, -1
end

MedalGroup._RefreshMedalGroup = HL.Method() << function(self)
    self.view.slotList:UpdateShowingCells(function(csIndex, obj)
        local cell = self.m_getMedalSlotCell(obj)
        self:_OnUpdateCell(cell, csIndex)
    end)
end

MedalGroup._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, cell, csIndex)
    local medalSlotList = self.m_medalSlotList
    local slot = csIndex < self.m_cellCount and medalSlotList[csIndex] or nil
    if slot ~= nil then
        local dragOptions = nil
        if self.m_dragOptions ~= nil then
            dragOptions = {
                slotType = self.m_dragOptions.slotType,
                slotIndex = slot.medalSlot,
                onBeginDrag = self.m_dragOptions.onBeginDrag,
                onEndDrag = self.m_dragOptions.onEndDrag,
                onDragMedal = self.m_dragOptions.onDragMedal,
                onDropMedal = self.m_dragOptions.onDropMedal,
                onClick = self.m_dragOptions.onClick,
            }
        end
        cell:InitMedalSlotHolder(slot.medalBundle, slot.pos, slot.isValid, self.view.config.USE_SLOT, dragOptions, slot.medalSlot, self.m_inNaviDrag)
        cell:SetDragState(slot.medalSlot == self.m_dragMedalSlot)
    end
end

MedalGroup.OnDragMedal = HL.Method(HL.Number) << function(self, slotIndex)
    self.m_dragMedalSlot = slotIndex
    self:_RefreshMedalGroup()
end

MedalGroup.CancelDragMedal = HL.Method() << function(self)
    self.m_dragMedalSlot = -1
    self:_RefreshMedalGroup()
end


MedalGroup.InitNaviDragMedal = HL.Method(HL.Opt(HL.Number)) << function(self, slotIndex)
    self.m_inNaviDrag = true
    local posU, posV = self:_GetMedalPosBySlot(1)
    if slotIndex ~= nil then
        posU, posV = self:_GetMedalPosBySlot(slotIndex)
    else
        local slotIndex, slotCellIndex = self:GetFirstEmptySlot()
        if slotCellIndex > 0 and self.m_medalSlotList[slotCellIndex] ~= nil then
            local slotCell = self.m_medalSlotList[slotCellIndex]
            posU = slotCell.pos.posU
            posV = slotCell.pos.posV
        end
    end
    local cellIndex = -1
    if posU ~= nil and posV ~= nil then
        cellIndex = self:_GetMedalIndexByPos(posU, posV)
        self.view.slotList:ScrollToIndex(cellIndex, true)
    end
    self.view.slotList:UpdateShowingCells(function(csIndex, obj)
        local cell = self.m_getMedalSlotCell(obj)
        self:_OnUpdateCell(cell, csIndex)
        if csIndex == cellIndex then
            self:SetNaviTarget(cell.view.medalSlot.view.button)
        end
    end)
    self:_UpdateNaviDragView()
end

MedalGroup.ClearNaviDragMedal = HL.Method(HL.Opt(HL.Number)) << function(self, slotIndex)
    self.m_inNaviDrag = false
    local cellIndex = -1
    if slotIndex ~= nil then
        cellIndex = self:_GetMedalIndexBySlot(slotIndex)
    end
    if cellIndex > 0 then
        self.view.slotList:ScrollToIndex(cellIndex, true)
    end
    self.view.slotList:UpdateShowingCells(function(csIndex, obj)
        local cell = self.m_getMedalSlotCell(obj)
        self:_OnUpdateCell(cell, csIndex)
        if csIndex == cellIndex then
            self:SetNaviTarget(cell.view.medalSlot.view.button)
        end
    end)
    self:_UpdateNaviDragView()
end

MedalGroup._UpdateNaviDragView = HL.Method() << function(self)
    if self.view.stateController ~= nil then
        self.view.stateController:SetState(self.m_inNaviDrag and "Drag" or "Normal")
    end
end

MedalGroup.OnNavigate = HL.Method(CS.UnityEngine.UI.NaviDirection, HL.Number) << function(self, direction, slotIndex)
    local posU, posV = self:_GetMedalPosBySlot(slotIndex)
    if posU == nil or posV == nil then
        return
    end
    if direction == CS.UnityEngine.UI.NaviDirection.Up then
        posV = posV - 1
        posU = posU + 1
    elseif direction == CS.UnityEngine.UI.NaviDirection.Down then
        posV = posV + 1
    elseif direction == CS.UnityEngine.UI.NaviDirection.Left then
        posU = posU - 1
    elseif direction == CS.UnityEngine.UI.NaviDirection.Right then
        posU = posU + 1
    end
    if posU <= self.m_maxU and posV <= self.m_maxV and posU >= self.m_minU and posV >= self.m_minV then
        local cellIndex = self:_GetMedalIndexByPos(posU, posV)
        local cell = self.view.slotList:Get(cellIndex)
        if cell ~= nil then
            local medalSlotHolder = Utils.wrapLuaNode(cell)
            self:SetNaviTarget(medalSlotHolder.view.medalSlot.view.button)
        end
    elseif not self.m_inNaviDrag then
        InputManagerInst.controllerNaviManager:Navigate(direction)
    end
end


HL.Commit(MedalGroup)
return MedalGroup