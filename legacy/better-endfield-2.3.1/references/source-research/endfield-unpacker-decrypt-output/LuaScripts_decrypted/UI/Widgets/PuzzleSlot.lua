local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local PUZZLE_TWEEN_TIME = 0.2
local PUZZLE_RESET_TO_PLACEHOLDER_TIME = 0.3

local PuzzleState = {
    Normal = 3,
    Float = 2,
    Drag = 1,
}

PuzzleSlot = HL.Class('PuzzleSlot', UIWidgetBase)

PuzzleSlot.m_puzzleGame = HL.Field(HL.Userdata)

PuzzleSlot.m_instId = HL.Field(HL.String) << ""

PuzzleSlot.m_puzzleData = HL.Field(HL.Table)

PuzzleSlot.m_puzzleCells = HL.Field(HL.Forward("UIListCache"))

PuzzleSlot.m_puzzleCellSize = HL.Field(HL.Number) << -1

PuzzleSlot.m_currentState = HL.Field(HL.Number) << PuzzleState.Normal

PuzzleSlot.m_cells = HL.Field(HL.Table)

PuzzleSlot.m_puzzleCtrl = HL.Field(HL.Forward("PuzzleCtrl"))

PuzzleSlot.m_luaIndex = HL.Field(HL.Number) << -1

PuzzleSlot.m_rawSizeDelta = HL.Field(Vector2)

PuzzleSlot.m_dragPivot = HL.Field(Vector2)

PuzzleSlot.m_rawPivot = HL.Field(Vector2)

PuzzleSlot.m_tweenCore = HL.Field(HL.Any)

PuzzleSlot.m_placeholderCell = HL.Field(HL.Any)

PuzzleSlot.m_canDrag = HL.Field(HL.Boolean) << false

PuzzleSlot._OnDestroy = HL.Override() << function(self)
    if self.m_tweenCore then
        self.m_tweenCore:Kill()
    end
end


PuzzleSlot._OnFirstTimeInit = HL.Override() << function(self)
    self.m_puzzleCells = UIUtils.genCellCache(self.view.puzzleCell)
    self.m_puzzleGame = GameInstance.player.miniGame.puzzleGame
    self.config = self.view.config
    self.m_rawSizeDelta = self.view.viewRect.sizeDelta
    self.m_rawPivot = self.view.viewRect.pivot
end

PuzzleSlot.InitPuzzleSlot = HL.Method(HL.Table, HL.Number, HL.Number, HL.Forward("PuzzleCtrl"), HL.Any, HL.Boolean)
        << function(self, data, size, luaIndex, puzzleCtrl, placeholderCell, chessboardLock)
    self:_FirstTimeInit()

    self.view.puzzleDrag.onBeginDragEvent:RemoveAllListeners()
    self.view.puzzleDrag.onBeginDragEvent:AddListener(function(eventData)
        self:_OnBeginDrag(eventData)
    end)

    self.view.puzzleDrag.onDragEvent:RemoveAllListeners()
    self.view.puzzleDrag.onDragEvent:AddListener(function(eventData)
        self:_OnDrag(eventData)
    end)

    self.view.puzzleDrag.onEndDragEvent:RemoveAllListeners()
    self.view.puzzleDrag.onEndDragEvent:AddListener(function(eventData)
        self:_OnEndDrag(eventData)
    end)

    self.view.puzzleDrag.onPointerDownEvent:RemoveAllListeners()
    self.view.puzzleDrag.onPointerDownEvent:AddListener(function(eventData)
        self:_OnPointerDown(eventData)
    end)

    self.view.puzzleDrag.onPointerUpEvent:RemoveAllListeners()
    self.view.puzzleDrag.onPointerUpEvent:AddListener(function(eventData)
        self:_OnPointerUp(eventData)
    end)

    self.view.puzzleDrag.onPointerClickEvent:RemoveAllListeners()
    self.view.puzzleDrag.onPointerClickEvent:AddListener(function(eventData)
        self:_OnPointerClick(eventData)
    end)

    self.view.puzzleDrag.onPointerEnterEvent:RemoveAllListeners()
    self.view.puzzleDrag.onPointerEnterEvent:AddListener(function(eventData)
        self:_OnPointerEnter(eventData)
    end)
    self.view.puzzleDrag.onPointerExitEvent:RemoveAllListeners()
    self.view.puzzleDrag.onPointerExitEvent:AddListener(function(eventData)
        self:_OnPointerExit(eventData)
    end)

    self.view.puzzleDrag:SetIndex(luaIndex)

    self.m_cells = {}
    self.m_puzzleData = data
    self.m_instId = data.id
    self.m_puzzleCellSize = size
    self.m_luaIndex = luaIndex
    self.m_puzzleCtrl = puzzleCtrl
    self.m_placeholderCell = placeholderCell

    self.m_dragPivot = UIUtils.calcPivotVecByData(data, self.config.PUZZLE_CELL_SIZE, self.config.PUZZLE_CELL_PADDING)

    self.gameObject.transform:SetParent(placeholderCell.rectTransform)
    self.transform.localPosition = Vector3.zero
    self.transform.localScale = Vector3.one
    self.transform.localRotation = Quaternion.identity
    self.view.viewRect.localRotation = Quaternion.identity

    self.view.viewImage:LoadSprite(UIConst.UI_SPRITE_MINIGAME_BLOCK, data.resPath)
    self.view.viewSelected:LoadSprite(UIConst.UI_SPRITE_MINIGAME_BLOCK, data.resPath)
    self.view.viewSelected.gameObject:SetActiveIfNecessary(false)
    self.view.controllerHover:LoadSprite(UIConst.UI_SPRITE_MINIGAME_BLOCK, data.resPath)
    self:ToggleHover(false)
    self:ToggleCanDrag(true)

    local color = UIUtils.getPuzzleColorByColorType(data.color)
    self.view.viewImage.color = color
    self.view.viewSelected.color = color
    self.view.controllerHover.color = color

    self.view.canvasGroup.blocksRaycasts = not chessboardLock

    self.view.viewImage.preserveAspect = true
    self.view.canvasGroup.alpha = self.config.PUZZLE_COLOR_NORMAL_ALPHA
    self.view.viewRect.pivot = self.m_rawPivot
    self.view.viewRect.sizeDelta = self.m_rawSizeDelta

    for _ = 1, data.rawRotationCount do
        self.view.viewRect:Rotate(0, 0, -90)
    end


    self.view.viewPuzzleCellDrag:Init(self.view.puzzleDrag)

    self.view.puzzleCell.rectTransform.sizeDelta = Vector2(size, size)
    self.m_puzzleCells:Refresh(data.originBlocks.Count, function(cell, index)
        self:_UpdateCells(cell, index)
    end)
    self:_RebuildDragCell()
    self:_ToggleCellsOrViewRaycast(false)
end

PuzzleSlot._UpdateCells = HL.Method(HL.Any, HL.Number, HL.Opt(HL.Boolean)) << function(self, cell, index, lock)
    table.insert(self.m_cells, cell.rectTransform)
    cell.puzzleCellDrag:Init(self.view.puzzleDrag)
end

PuzzleSlot._OnBeginDrag = HL.Method(HL.Userdata) << function(self, eventData)
    if not self.m_canDrag then
        return
    end

    self.m_puzzleGame:TakeBlockFromChessboard(self.m_puzzleData.id)
    self.m_puzzleCtrl:SetCurActionBlock(self.m_puzzleData.id, self)

    self.m_puzzleCtrl:SetBlockOutScrollRect(self.transform)
    self.m_puzzleCtrl:ToggleRotateBtnState(true)
    self.m_puzzleCtrl:DisableComponentsInteractable()
    self.m_puzzleCtrl:SetOtherBlocksFading(self.m_puzzleData.id, true)
    self.m_puzzleCtrl:UpdateNoActionNoticeTimer()
    self.m_puzzleCtrl:ResetCachedGridData()
    self.m_puzzleCtrl:PuzzleSlotInMovement()


    self.transform:SetAsLastSibling()

    self.m_currentState = PuzzleState.Drag
    self.view.canvasGroup.alpha = self.config.PUZZLE_COLOR_NORMAL_ALPHA
    self.view.viewSelected.gameObject:SetActiveIfNecessary(true)
    self.view.viewRect.pivot = self.m_dragPivot
    self.view.viewRect.localPosition = Vector3.zero
    self.view.viewImage:SetNativeSize()

    AudioAdapter.PostEvent("Au_UI_Event_Piece_Drag")
end

PuzzleSlot._OnDrag = HL.Method(HL.Userdata) << function(self, eventData)

    if self.m_currentState ~= PuzzleState.Drag then
        return
    end

    local panel = self:GetLuaPanel()
    local dataPosition = eventData.position
    local pos = Vector3(dataPosition.x, dataPosition.y, panel.planeDistance)
    self.view.transform.position = panel.uiCamera:ScreenToWorldPoint(pos)
end

PuzzleSlot._OnEndDrag = HL.Method(HL.Userdata, HL.Opt(HL.Boolean)) << function(self, eventData, overrideResetToPlaceholder)
    if self.m_currentState ~= PuzzleState.Drag then
        return
    end

    self.m_puzzleCtrl:ClearCurActionBlock()
    self.m_puzzleCtrl:PuzzleSlotOutMovement()

    local resetToPlaceholder
    if overrideResetToPlaceholder ~= nil then
        resetToPlaceholder = overrideResetToPlaceholder
    else
        resetToPlaceholder = true
        if eventData.pointerEnter and
                eventData.pointerEnter:GetComponent(typeof(CS.Beyond.UI.ChessboardDrop)) then
            resetToPlaceholder = false
        end
    end

    if resetToPlaceholder then

        self:ResetToPlaceholder(false)
    else

        self:_SlotMoveToGridPos()
    end
end

PuzzleSlot._SlotMoveToGridPos = HL.Method() << function(self)
    self.m_puzzleCtrl:PuzzleSlotInMovement()
    local blockData = self.m_puzzleGame.currentChessboard.blocks:get_Item(self.m_puzzleData.id)

    self.m_tweenCore = self.transform:DOMove(blockData.forceLocation, PUZZLE_TWEEN_TIME):OnComplete(function()
        AudioAdapter.PostEvent("Au_UI_Event_Piece_Put")
        if not DeviceInfo.usingController then
            self.m_puzzleCtrl:PuzzleSlotOutMovement()
        end
    end)





    if DeviceInfo.usingController then
        self.m_puzzleCtrl:PuzzleSlotOutMovement()
    end

    if blockData.isIllegalLocate then

        self:_SlotIllegalLocate()
    else

        self:_SlotLegalLocate()
    end
    self:_ToggleCellsOrViewRaycast(true)

    self.m_puzzleCtrl:ResetCachedGridData()
    self.m_puzzleCtrl:RecoverComponentsInteractable()
    self.m_puzzleCtrl:UpdateResetBtn()
    self.m_puzzleCtrl:SetShadowCellVisibleById(self.m_puzzleData.id, false)
end

PuzzleSlot._SlotIllegalLocate = HL.Method() << function(self)
    self.m_currentState = PuzzleState.Float

    self.m_puzzleCtrl:SetCurActionBlock(self.m_puzzleData.id, self)
    self.m_puzzleCtrl:SetOtherBlocksFading(self.m_puzzleData.id, true)
end

PuzzleSlot._SlotLegalLocate = HL.Method() << function(self)
    self.m_currentState = PuzzleState.Normal
    self.view.canvasGroup.alpha = self.config.PUZZLE_COLOR_NORMAL_ALPHA
    self.view.viewSelected.gameObject:SetActiveIfNecessary(false)

    self.m_puzzleCtrl:ToggleRotateBtnState(false)
    self.m_puzzleCtrl:SetOtherBlocksFading(self.m_puzzleData.id, false)
    GameInstance.mobileMotionManager:PostEventCommonShort()
end

PuzzleSlot._OnPointerDown = HL.Method(HL.Userdata) << function(self, eventData)
    self.m_puzzleCtrl:SetOtherGraphicRaycasts(self.m_puzzleData.id, false)
end

PuzzleSlot._OnPointerUp = HL.Method(HL.Userdata) << function(self, eventData)
    self.m_puzzleCtrl:SetOtherGraphicRaycasts(self.m_puzzleData.id, true)
end

PuzzleSlot._OnPointerClick = HL.Method(HL.Userdata) << function(self, eventData)
    local blockId = self.m_puzzleData.id
    local succ, block = self.m_puzzleGame.currentChessboard.blocks:TryGetValue(blockId)
    if not succ or not block.locationOnChessboard then
        return
    end

    if self.m_currentState == PuzzleState.Normal then
        self:_TryToFloatSlot(blockId)
    elseif self.m_currentState == PuzzleState.Float then
        self:_RotateAndPutOnChessboard(blockId)
    end
end

PuzzleSlot._OnPointerEnter = HL.Method(HL.Userdata) << function(self, eventData)
    local blockId = self.m_puzzleData.id
    local succ, block = self.m_puzzleGame.currentChessboard.blocks:TryGetValue(blockId)
    if not succ or block.locationOnChessboard then
        return
    end

    self.view.viewSelected.gameObject:SetActiveIfNecessary(true)
end

PuzzleSlot._OnPointerExit = HL.Method(HL.Userdata) << function(self, eventData)
    local blockId = self.m_puzzleData.id
    local succ, block = self.m_puzzleGame.currentChessboard.blocks:TryGetValue(blockId)




    if not succ or block.locationOnChessboard or not DeviceInfo.usingController and eventData.dragging then
        return
    end

    self.view.viewSelected.gameObject:SetActiveIfNecessary(false)
end

PuzzleSlot._TryToFloatSlot = HL.Method(HL.String) << function(self, blockId)
    self.m_currentState = PuzzleState.Float
    self.view.canvasGroup.alpha = self.config.PUZZLE_COLOR_NORMAL_ALPHA
    self.view.viewSelected.gameObject:SetActiveIfNecessary(true)
    self.transform:SetAsLastSibling()

    self.m_puzzleGame:TakeBlockFromChessboard(blockId)

    self.m_puzzleCtrl:SetCurActionBlock(self.m_puzzleData.id, self)
    self.m_puzzleCtrl:ToggleRotateBtnState(true)
    self.m_puzzleCtrl:ResetCachedGridData()
    self.m_puzzleCtrl:SetOtherBlocksFading(self.m_puzzleData.id, true)
    self.m_puzzleCtrl:UpdateNoActionNoticeTimer()
end

PuzzleSlot._Rotate = HL.Method(HL.String) << function(self, blockId)
    local puzzleGame = self.m_puzzleGame
    local block = puzzleGame.currentChessboard.blocks:get_Item(blockId)
    puzzleGame:TakeBlockFromChessboard(blockId)
    puzzleGame:RotateBlock(blockId)
    self.m_tweenCore = self.view.viewRect:DORotate(Vector3(0, 0, -90 * (block.rotateCount % 4)), PUZZLE_TWEEN_TIME)
    self:_RebuildDragCell()
    self.m_puzzleCtrl:RotateShadowById(blockId)

    AudioAdapter.PostEvent("Au_UI_Event_Piece_Rotation")
end

PuzzleSlot._RotateAndPutOnChessboard = HL.Method(HL.String) << function(self, blockId)
    self:_Rotate(blockId)

    local puzzleGame = self.m_puzzleGame
    local puzzleCtr = self.m_puzzleCtrl
    local block = puzzleGame.currentChessboard.blocks:get_Item(blockId)
    local succ, complete = puzzleCtr:PutBlockOnChessboard(blockId,
                                                          block.location.x,
                                                          block.location.y,
                                                          block.forceLocation)
    if succ then
        self.view.viewRect.pivot = self.m_dragPivot
        self.transform.position = block.forceLocation
    end

    if complete then
        self.m_currentState = PuzzleState.Normal
        self.view.canvasGroup.alpha = self.config.PUZZLE_COLOR_NORMAL_ALPHA
        self.view.viewSelected.gameObject:SetActiveIfNecessary(false)
        self.m_puzzleCtrl:SetOtherBlocksFading("", false)
        self.m_puzzleCtrl:ClearCurActionBlock()
    end
end

PuzzleSlot._RebuildDragCell = HL.Method(HL.Opt(HL.Number)) << function(self, factor)
    local _, block = self.m_puzzleGame.currentChessboard.blocks:TryGetValue(self.m_puzzleData.id)
    for i = 1, #self.m_cells do
        self.m_cells[i].localScale = Vector3.one
        self.m_cells[i].anchoredPosition = block.rotateBlocks[CSIndex(i)] * self.m_puzzleCellSize
    end
end

PuzzleSlot._ToggleCellsOrViewRaycast = HL.Method(HL.Boolean) << function(self, isCells)
    self.view.cells.gameObject:SetActiveIfNecessary(isCells)
    self.view.viewPuzzleCellDrag.gameObject:SetActiveIfNecessary(not isCells)
end

PuzzleSlot.Rotate = HL.Method(HL.String) << function(self, blockId)
    if self.m_currentState == PuzzleState.Normal then
        return
    end

    if self.m_currentState == PuzzleState.Drag then
        self:_Rotate(blockId)
    elseif self.m_currentState == PuzzleState.Float then
        self:_RotateAndPutOnChessboard(blockId)
    end
end

PuzzleSlot.ResetState = HL.Method() << function(self)
    self.m_currentState = PuzzleState.Normal
end

PuzzleSlot.ResetToPlaceholder = HL.Method(HL.Boolean) << function(self, byChangeCurActionBlock)
    self.m_currentState = PuzzleState.Normal

    self.m_puzzleCtrl:SetOtherBlocksFading(self.m_puzzleData.id, false)
    self.m_puzzleCtrl:RecoverComponentsInteractable()
    self.m_puzzleCtrl:PuzzleSlotInMovement()

    self.m_puzzleCtrl:ResetCachedGridData()
    self.m_puzzleCtrl:UpdateResetBtn()
    self.m_puzzleCtrl:SetShadowCellVisibleById(self.m_puzzleData.id, false)

    if not byChangeCurActionBlock then
        self.m_puzzleCtrl:ClearCurActionBlock()
        self.m_puzzleCtrl:ToggleRotateBtnState(false)
    end



    self.m_puzzleGame:ResetBlockData(self.m_puzzleData.id, false)

    self.m_tweenCore = self.transform:DOMove(self.m_placeholderCell.transform.position, PUZZLE_RESET_TO_PLACEHOLDER_TIME):OnComplete(function()
        self.transform:SetParent(self.m_placeholderCell.transform)
        self.m_placeholderCell.remindNode:Play("puzzleplaceholderitem_remind_in")

        AudioAdapter.PostEvent("Au_UI_Event_Piece_Put")


        self:_ToggleCellsOrViewRaycast(false)

        self.m_puzzleCtrl:PuzzleSlotOutMovement()
    end)


    self.view.viewPuzzleCellDrag.gameObject:SetActiveIfNecessary(false)

    self.view.viewImage.preserveAspect = true
    self.view.canvasGroup.alpha = self.config.PUZZLE_COLOR_NORMAL_ALPHA
    self.view.viewSelected.gameObject:SetActiveIfNecessary(false)
    self.view.viewRect.pivot = self.m_rawPivot
    self.view.viewRect.sizeDelta = self.m_rawSizeDelta
    self.view.viewRect.localPosition = Vector3.zero
end

PuzzleSlot.SetBlockFading = HL.Method(HL.String, HL.Boolean) << function(self, blockId, isOn)
    if self.m_puzzleData.id == blockId then
        return
    end

    self.view.canvasGroup.alpha = isOn and self.config.PUZZLE_COLOR_FADING_ALPHA or self.config.PUZZLE_COLOR_NORMAL_ALPHA
    self.view.viewSelected.gameObject:SetActiveIfNecessary(false)
end

PuzzleSlot.IsStateDragging = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_currentState == PuzzleState.Drag
end

PuzzleSlot.GetInstId = HL.Method().Return(HL.String) << function(self)
    return self.m_instId
end

PuzzleSlot.ToggleInteractable = HL.Method(HL.Boolean) << function(self, isOn)
    self.view.canvasGroup.blocksRaycasts = isOn
    self.view.canvasGroup.interactable = isOn
end

PuzzleSlot.ToggleHover = HL.Method(HL.Boolean) << function(self, isOn)
    self.view.controllerHover.gameObject:SetActiveIfNecessary(isOn)
end

PuzzleSlot.ToggleCanDrag = HL.Method(HL.Boolean) << function(self, isOn)
    self.m_canDrag = isOn
end

PuzzleSlot.RecoverToChessboard = HL.Method(Transform) << function(self, gridCellTransform)
    local _, block = self.m_puzzleGame.currentChessboard.blocks:TryGetValue(self.m_puzzleData.id)

    self.m_puzzleCtrl:SetBlockOutScrollRect(self.transform)

    self.view.viewRect.pivot = self.m_dragPivot
    self.view.viewRect.localPosition = Vector3.zero
    self.view.viewImage:SetNativeSize()
    self.view.viewRect.localRotation = Quaternion.Euler(0, 0, -90 * (block.rotateCount % 4))

    self.transform.position = gridCellTransform.position
    block.forceLocation = gridCellTransform.position

    self.m_currentState = PuzzleState.Normal
    self.view.canvasGroup.alpha = self.config.PUZZLE_COLOR_NORMAL_ALPHA
    self.view.viewSelected.gameObject:SetActiveIfNecessary(false)
    self:_ToggleCellsOrViewRaycast(true)
    self:_RebuildDragCell()
end

PuzzleSlot.RecoverToChessboardAsFloat = HL.Method(Transform) << function(self, gridCellTransform)
    local _, block = self.m_puzzleGame.currentChessboard.blocks:TryGetValue(self.m_puzzleData.id)

    self.m_puzzleCtrl:SetBlockOutScrollRect(self.transform)

    self.view.viewRect.pivot = self.m_dragPivot
    self.view.viewRect.localPosition = Vector3.zero
    self.view.viewImage:SetNativeSize()
    self.view.viewRect.localRotation = Quaternion.Euler(0, 0, -90 * (block.rotateCount % 4))

    self.transform.position = gridCellTransform.position
    block.forceLocation = gridCellTransform.position

    self.m_currentState = PuzzleState.Float
    self.view.canvasGroup.alpha = self.config.PUZZLE_COLOR_NORMAL_ALPHA
    self.view.viewSelected.gameObject:SetActiveIfNecessary(true)
    self.transform:SetAsLastSibling()
    self:_ToggleCellsOrViewRaycast(true)
    self:_RebuildDragCell()
end

PuzzleSlot.RecoverRotation = HL.Method(HL.Number) << function(self, rotateCount)
    self.view.viewRect.localRotation = Quaternion.Euler(0, 0, -90 * (rotateCount % 4))
end

HL.Commit(PuzzleSlot)
return PuzzleSlot

