
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BalloonMiniGameRotatePlate
local MOVE_POS = 16

BalloonMiniGameRotatePlateCtrl = HL.Class('BalloonMiniGameRotatePlateCtrl', uiCtrl.UICtrl)

BalloonMiniGameRotatePlateCtrl.m_gridCell = HL.Field(HL.Forward("UIListCache"))

BalloonMiniGameRotatePlateCtrl.m_gridSuccessCell = HL.Field(HL.Forward("UIListCache"))

BalloonMiniGameRotatePlateCtrl.m_chessboardTween = HL.Field(HL.Userdata)

BalloonMiniGameRotatePlateCtrl.m_successCellAnimCoroutine = HL.Field(HL.Thread)

BalloonMiniGameRotatePlateCtrl.m_verSliderTween = HL.Field(HL.Userdata)

BalloonMiniGameRotatePlateCtrl.m_horSliderTween = HL.Field(HL.Userdata)

BalloonMiniGameRotatePlateCtrl.m_curGame = HL.Field(HL.Userdata)

BalloonMiniGameRotatePlateCtrl.m_curLevel = HL.Field(HL.Userdata)

BalloonMiniGameRotatePlateCtrl.m_gridItems = HL.Field(HL.Table)

BalloonMiniGameRotatePlateCtrl.m_needLift = HL.Field(HL.Number) << 0

BalloonMiniGameRotatePlateCtrl.m_curLift = HL.Field(HL.Number) << 0

BalloonMiniGameRotatePlateCtrl.m_needShowAllAnswer = HL.Field(HL.Boolean) << false

BalloonMiniGameRotatePlateCtrl.m_showAllAnswerMiniGameId = HL.Field(HL.String) << ""

BalloonMiniGameRotatePlateCtrl.m_lastBalanceState = HL.Field(HL.Boolean) << false

BalloonMiniGameRotatePlateCtrl.m_hasPlayedSuccessAnim = HL.Field(HL.Boolean) << false

BalloonMiniGameRotatePlateCtrl.m_hasPlayedVerBalanceAnim = HL.Field(HL.Boolean) << false

BalloonMiniGameRotatePlateCtrl.m_hasPlayedHorBalanceAnim = HL.Field(HL.Boolean) << false

BalloonMiniGameRotatePlateCtrl.m_lastDraggingAudioState = HL.Field(HL.String) << ""

BalloonMiniGameRotatePlateCtrl.m_baseBalloonMat = HL.Field(HL.Userdata)

BalloonMiniGameRotatePlateCtrl.m_balloonTextures = HL.Field(HL.Table)





BalloonMiniGameRotatePlateCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_MINIGAME_BALLOON_BLOCK_CHANGE] = 'OnBalloonCellDrop',
}


BalloonMiniGameRotatePlateCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_gridCell = UIUtils.genCellCache(self.view.balloonMinigameGridPlate.seedNodeTemplate)
    self.m_gridSuccessCell = UIUtils.genCellCache(self.view.balloonMinigameGridEndPlate.succNodeTemplate)
    self.m_curGame = GameInstance.player.miniGame.balloonGame
    self.view.completeNode.button.onClick:AddListener(function()
        self.m_phase:OnClickNextBtn()
        self.view.completeNode.button.interactable = true
    end)

    self.m_baseBalloonMat = self.loader:LoadMaterial(MiniGameBalloonUtils.FAKE_3D_BALLOON_MAT_PATH)
    self.m_balloonTextures = {}
    for _, liftNum in ipairs({1, 2, 3, 6}) do
        local tex = self.loader:LoadTexture(string.format(MiniGameBalloonUtils.BALLOON_TYPE_TEX_PATH, liftNum))
        self.m_balloonTextures[liftNum] = tex
    end
    local sortingOrder = self:GetSortingOrder()
    self.view.balloonMinigameGridPlate.canvas.sortingOrder = sortingOrder - 1
    self.view.dragTarget.sortingOrder = sortingOrder + 2
    self.view.mainCanvas.sortingOrder = sortingOrder + 2

end

BalloonMiniGameRotatePlateCtrl.OnShow = HL.Override() << function(self)

end
BalloonMiniGameRotatePlateCtrl.OnHide = HL.Override() << function(self)

end
BalloonMiniGameRotatePlateCtrl.OnClose = HL.Override() << function(self)
    if self.m_successCellAnimCoroutine then
        self.m_successCellAnimCoroutine = self:_ClearCoroutine(self.m_successCellAnimCoroutine)
    end
    if self.m_gridItems then
        for _, col in pairs(self.m_gridItems) do
            for _, itemData in pairs(col) do
                itemData.cell:DestroyBalloonMaterial()
            end
        end
    end
end


BalloonMiniGameRotatePlateCtrl.UpdateAllBalloonCells = HL.Method(HL.Userdata) << function(self, currentRot)
    if not self.m_gridItems then return end
    local invRot = Quaternion.Inverse(currentRot)
    local floatZ = -self.view.config.BALLOON_FLOAT_Z or -80
    local balloonOffset = invRot * Vector3(0, 0, floatZ)
    for cx, col in pairs(self.m_gridItems) do
        for cy, itemData in pairs(col) do
            local cell = itemData.cell
            if cell.m_curLiftNum and cell.m_curLiftNum > 0 and not cell.m_isRising then
                cell:UpdateBalloonTransform(invRot, balloonOffset)
            end
        end
    end
end


BalloonMiniGameRotatePlateCtrl.SwitchState = HL.Method(HL.String) << function(self, state)
    self.view.mainState:SetState(state)
    if state == MiniGameBalloonUtils.GAME_STATE.PLAY then
        self.m_lastBalanceState = false
        self.m_hasPlayedSuccessAnim = false
        self.m_hasPlayedVerBalanceAnim = false
        self.m_hasPlayedHorBalanceAnim = false
        self:UpdateNewLevel()
    elseif state == MiniGameBalloonUtils.GAME_STATE.COMPLETE then
        self.m_curLevel = self.m_curLevel or self.m_phase.curLevel
        if DeviceInfo.usingController then
            self.m_phase.m_balloonMainPanel:ToggleChessboardSelectBindingId(false)
            self.m_phase.m_balloonMainPanel:ToggleChessboardRecycleBindingId(false)
            self:ClearNaviTarget()
        end
    end
end

BalloonMiniGameRotatePlateCtrl.UpdateNewLevel = HL.Method() << function(self)
    if not self.m_phase then
        logger.error("phase: [Balloon] is not init")
        return
    end
    self.m_curLevel = self.m_phase.curLevel
    local levelRawData = self.m_curLevel.rawData
    local minBoard = self.view.config.MIN_CHESSBOARD
    local maxBoard = self.view.config.MAX_CHESSBOARD
    if levelRawData.boardSizeX < minBoard or levelRawData.boardSizeX > maxBoard then
        logger.critical("BalloonMiniGame:boardSizeX(%d) out of range [%d, %d]", levelRawData.boardSizeX, minBoard, maxBoard)
    end
    local gridPlate = self.view.balloonMinigameGridPlate
    self.view.balloonMinigameGridEndPlate.gameObject:SetActiveIfNecessary(false)
    gridPlate.gridLayout.constraintCount = levelRawData.boardSizeX
    self.m_gridItems = {}
    self.m_gridCell:Refresh(levelRawData.boardSizeX * levelRawData.boardSizeY, function(cell, index)
        self:UpdateGridCell(cell, index)
    end)
    self:RefreshLiftCache()
    local borderSize = levelRawData.boardSizeX * gridPlate.gridLayout.cellSize.x + (levelRawData.boardSizeX - 1) * gridPlate.gridLayout.spacing.x
    self.view.balloonMinigameGridPlate.mask.transform.sizeDelta = Vector2(borderSize, borderSize)
    self:SetAllPlateState()
    self:ToggleAllAnswer(false)
end

BalloonMiniGameRotatePlateCtrl.UpdateGridCell = HL.Method(HL.Userdata, HL.Number) << function(self, cell, index)
    cell:InitBalloonSeedNode(self, index)
    local cx, cy = cell.m_cx, cell.m_cy
    self.m_gridItems[cx] = self.m_gridItems[cx] or {}
    self.m_gridItems[cx][cy] = {
        index = index,
        cell = cell
    }
end

BalloonMiniGameRotatePlateCtrl.IsBalloonState = HL.Method(CS.Beyond.Gameplay.BalloonBlockState).Return(HL.Boolean) << function(self, state)
    local BLOCK_STATE = MiniGameBalloonUtils.BalloonBlockState
    return state ~= BLOCK_STATE.CANT_PLACE and state ~= BLOCK_STATE.PLACE_ABLE
end

BalloonMiniGameRotatePlateCtrl.GetStateLift = HL.Method(CS.Beyond.Gameplay.BalloonBlockState).Return(HL.Number) << function(self, state)
    return self:IsBalloonState(state) and state:GetHashCode() or 0
end

BalloonMiniGameRotatePlateCtrl.GetBalloonConfigCount = HL.Method(CS.Beyond.Gameplay.BalloonBlockState).Return(HL.Number) << function(self, state)
    return self.m_curLevel.rawData.balloonConfig:GetCountByState(state)
end

BalloonMiniGameRotatePlateCtrl.RefreshLiftCache = HL.Method() << function(self)
    self.m_needLift = 0
    for _, state in pairs(MiniGameBalloonUtils.BLOCK_STATE_SORT) do
        self.m_needLift = self.m_needLift + self:GetBalloonConfigCount(state) * self:GetStateLift(state)
    end

    self.m_curLift = 0
    if not self.m_gridItems then
        return
    end
    for cx, col in pairs(self.m_gridItems) do
        for cy, _ in pairs(col) do
            self.m_curLift = self.m_curLift + self:GetStateLift(self.m_curLevel:GetNowPositionState(cx, cy))
        end
    end
end

BalloonMiniGameRotatePlateCtrl.ApplyLiftCacheChange = HL.Method(CS.Beyond.Gameplay.BalloonBlockState, CS.Beyond.Gameplay.BalloonBlockState)
    << function(self, oldState, newState)
    self.m_curLift = self.m_curLift - self:GetStateLift(oldState) + self:GetStateLift(newState)
end

BalloonMiniGameRotatePlateCtrl._TryToggleChessboardSelectBalloon = HL.Method(HL.Number, HL.Number) << function(self, cx, cy)
    if self.m_phase.isDragIngCell then
        return
    end
    local nowState = self.m_curLevel:GetNowPositionState(cx, cy)
    local BLOCK_STATE = MiniGameBalloonUtils.BalloonBlockState
    local hasBalloon = nowState ~= BLOCK_STATE.CANT_PLACE and nowState ~= BLOCK_STATE.PLACE_ABLE
    self.m_phase.m_balloonMainPanel:ToggleChessboardSelectBindingId(hasBalloon)
    self.m_phase.m_balloonMainPanel:ToggleChessboardRecycleBindingId(hasBalloon)
end

BalloonMiniGameRotatePlateCtrl.OnGroupSetAsNaviTarget = HL.Method(HL.Userdata, HL.Number, HL.Number, HL.Boolean)
    << function(self, cell, cx, cy, select)
    if not DeviceInfo.usingController then
        return
    end
    local phase = self.m_phase
    if select then
        local previewLiftNum
        if phase.isDragIngCell and (phase.nowDragTabItemData or phase.nowDragChessboardItemData) then
            phase:OnDraggingStart()
            phase:OnDragging(cell.view.dropItem, cx, cy)
            local itemData = phase.nowDragTabItemData or phase.nowDragChessboardItemData
            if itemData and itemData.liftNum then
                previewLiftNum = itemData.liftNum
                phase.m_balloonMainPanel:ToggleChessboardBindingId(true)
                local targetState = self.m_curLevel:GetNowPositionState(cx, cy)
                local isCantPlace = targetState == MiniGameBalloonUtils.BalloonBlockState.CANT_PLACE
                InputManagerInst:ForceBindingKeyhintToGray(phase.m_balloonMainPanel.m_confirmChessboardBindingId, isCantPlace)
            else
                phase.m_balloonMainPanel:ToggleChessboardBindingId(false)
            end
        else
            phase.m_balloonMainPanel:ToggleChessboardBindingId(false)
        end
        phase.nowNaviChessboardItemData = {
            cx = cx,
            cy = cy,
        }
        cell:OnNaviSelected(previewLiftNum)
        self:_TryToggleChessboardSelectBalloon(cx, cy)
    else
        cell:OnNaviDeselected()
        local hi = phase.nowDragHighLightItemData
        if hi and hi.cx == cx and hi.cy == cy then
            phase.nowDragHighLightItemData = nil
            self:SetDraggingPlateState()
        end
        phase:ToggleHighlightEnd()
    end
end

BalloonMiniGameRotatePlateCtrl.OnTriggerConfirmChessboard = HL.Method() << function(self)
    local dragTabItemData = self.m_phase.nowDragTabItemData
    local nowDragChessboardItemData = self.m_phase.nowDragChessboardItemData
    local nowNaviChessboardItemData = self.m_phase.nowNaviChessboardItemData
    local liftNum = (dragTabItemData and dragTabItemData.liftNum) or (nowDragChessboardItemData and nowDragChessboardItemData.liftNum)
    if liftNum then
        local BLOCK_STATE = MiniGameBalloonUtils.BalloonBlockState
        local targetState = self.m_curLevel:GetNowPositionState(nowNaviChessboardItemData.cx, nowNaviChessboardItemData.cy)
        if targetState == BLOCK_STATE.CANT_PLACE then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_BALLOON_CAN_NOT_SET_TOAST)
            return
        end
        self.m_phase:OnEndDrag()
        if dragTabItemData and nowNaviChessboardItemData.cx and nowNaviChessboardItemData.cy then
            self:SetBlockStateByPosition(nowNaviChessboardItemData.cx, nowNaviChessboardItemData.cy, MiniGameBalloonUtils.BLOCK_STATE[liftNum])
        else
            self.m_phase.m_balloonMainPanel:SetSuppressTabCellCountAnim(true)
            if targetState ~= BLOCK_STATE.PLACE_ABLE then

                self:SwapBlocksByPosition(nowDragChessboardItemData.cx, nowDragChessboardItemData.cy,
                    nowNaviChessboardItemData.cx, nowNaviChessboardItemData.cy)
            else

                self:SetBlockStateByPosition(nowDragChessboardItemData.cx, nowDragChessboardItemData.cy, BLOCK_STATE.PLACE_ABLE)
                self:SetBlockStateByPosition(nowNaviChessboardItemData.cx, nowNaviChessboardItemData.cy, MiniGameBalloonUtils.BLOCK_STATE[liftNum])
            end
            self.m_phase.m_balloonMainPanel:SetSuppressTabCellCountAnim(false)
        end
        self:SetAllPlateState()
        local cx = nowNaviChessboardItemData.cx
        local cy = nowNaviChessboardItemData.cy
        local cellItem = self.m_gridItems[cx][cy]

        self:OnGroupSetAsNaviTarget(cellItem.cell, cx, cy, true)
        self:_TryToggleChessboardSelectBalloon(cx, cy)
    end

    if dragTabItemData then

        self.m_phase.m_balloonMainPanel:TrySwitchToFirstListCell()
    elseif nowDragChessboardItemData and nowNaviChessboardItemData then

        local cx = nowNaviChessboardItemData.cx
        local cy = nowNaviChessboardItemData.cy
        local cellItem = self.m_gridItems[cx] and self.m_gridItems[cx][cy]
        if cellItem then
            self:SetNaviTarget(cellItem.cell.view.inputBindingGroupNaviDecorator)
        end
    else
        self.m_phase.m_balloonMainPanel:TrySwitchToFirstListCell()
    end
end

BalloonMiniGameRotatePlateCtrl.OnTriggerSelectBalloon = HL.Method() << function(self)
    local nowNaviChessboardItemData = self.m_phase.nowNaviChessboardItemData
    if nowNaviChessboardItemData then
        local cx = nowNaviChessboardItemData.cx
        local cy = nowNaviChessboardItemData.cy
        local cellItem = self.m_gridItems[cx][cy]
        self:OnGroupSetAsNaviTarget(cellItem.cell, cx, cy, true)
        local dragData = self.m_phase.nowDragChessboardItemData
        if dragData then
            self:UpdateGridCellByPosition(dragData.cx, dragData.cy)
            self:SetDraggingPlateState()
        end
        self:_TryToggleChessboardSelectBalloon(cx, cy)
    end
end


BalloonMiniGameRotatePlateCtrl.NaviToNearestPlaceableNode = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end
    if not self.m_phase or not self.m_curLevel or not self.m_gridItems then
        return
    end

    local BLOCK_STATE = MiniGameBalloonUtils.BalloonBlockState
    local bestCx, bestCy, bestDecorator
    local bestDist2
    for cx, col in pairs(self.m_gridItems) do
        for cy, itemData in pairs(col) do
            local nowState = self.m_curLevel:GetNowPositionState(cx, cy)
            if nowState ~= BLOCK_STATE.CANT_PLACE then
                local isPlaceable = nowState == BLOCK_STATE.PLACE_ABLE or self.m_curLevel:CheckPositionNoPlace(cx, cy)
                if isPlaceable and itemData and itemData.cell and itemData.cell.view and itemData.cell.view.inputBindingGroupNaviDecorator then
                    local dist2 = cx * cx + cy * cy
                    if not bestDist2 or dist2 < bestDist2 then
                        bestDist2 = dist2
                        bestCx, bestCy = cx, cy
                        bestDecorator = itemData.cell.view.inputBindingGroupNaviDecorator
                    end
                end
            end
        end
    end

    if bestDecorator then
        self.m_phase.nowDragHighLightItemData = {
            cx = bestCx,
            cy = bestCy
        }
        self:SetNaviTarget(bestDecorator)
        self:SetDraggingPlateState()
    end
end

BalloonMiniGameRotatePlateCtrl.NaviToCenterGridNode = HL.Method() << function(self)
    if not self.m_gridItems then
        return
    end
    local cx = 0
    local cy = 0
    local centerItem = self.m_gridItems[cx][cy]
    local decorator = centerItem.cell.view.inputBindingGroupNaviDecorator
    if decorator then
        self.m_phase.nowDragHighLightItemData = {
            cx = cx,
            cy = cy
        }
        self:SetNaviTarget(decorator)
        self:SetDraggingPlateState()
    end
end

BalloonMiniGameRotatePlateCtrl.SetDraggingPlateState = HL.Method() << function(self)
    local phase = self.m_phase
    local dragChessboardItemData = phase.nowDragChessboardItemData
    local dragTabItemData = phase.nowDragTabItemData

    local highlightChessboardItemData = phase.nowDragHighLightItemData

    local BLOCK_STATE = MiniGameBalloonUtils.BalloonBlockState

    local draggingTempPositions
    local draggingTempLifts


    if dragChessboardItemData then
        local tempX = dragChessboardItemData.cx
        local tempY = dragChessboardItemData.cy
        local tempPositions
        local tempLifts
        local tempLiftsForRot
        if highlightChessboardItemData then
            local highLightNowState = self.m_curLevel:GetNowPositionState(highlightChessboardItemData.cx, highlightChessboardItemData.cy)
            if highLightNowState == BLOCK_STATE.CANT_PLACE then
                logger.error(string.format("CANT_PLACE 的cell 不应高亮：%d _ %d",highlightChessboardItemData.cx, highlightChessboardItemData.cy))
            else
                local tempHighLightX = highlightChessboardItemData.cx
                local tempHighLightY = highlightChessboardItemData.cy
                local highLiftNum = highLightNowState:GetHashCode()
                tempPositions = {tempX, tempY, tempHighLightX, tempHighLightY}
                tempLifts = {highLiftNum > 0 and highLiftNum or 0, dragChessboardItemData.liftNum}
                local isDragCell = tempHighLightX == tempX and tempHighLightY == tempY
                tempLiftsForRot = {0, isDragCell and 0 or highLightNowState:GetHashCode()}
                self:SetRotation(tempPositions, tempLiftsForRot)
                self:SetPlateLine(tempPositions, tempLifts)
                self:SetSliderState(tempPositions, tempLifts)
                self:OnTempBalloonChange(tempPositions, tempLiftsForRot)
                draggingTempPositions = tempPositions
                draggingTempLifts = tempLifts
            end
        else
            tempPositions = {tempX, tempY}
            tempLifts = {0}
            self:SetRotation(tempPositions, tempLifts)
            self:SetPlateLine(tempPositions, tempLifts)
            self:SetSliderState(tempPositions, tempLifts)
            self:OnTempBalloonChange(tempPositions, tempLifts)
        end


    elseif dragTabItemData and highlightChessboardItemData then
        local tempX = highlightChessboardItemData.cx
        local tempY = highlightChessboardItemData.cy
        local highLightNowState = self.m_curLevel:GetNowPositionState(highlightChessboardItemData.cx, highlightChessboardItemData.cy)
        if highLightNowState == BLOCK_STATE.CANT_PLACE then
            logger.error(string.format("CANT_PLACE 的cell 不应高亮：%d _ %d",highlightChessboardItemData.cx, highlightChessboardItemData.cy))
        elseif highLightNowState == BLOCK_STATE.PLACE_ABLE then
            local tempPositions = {tempX, tempY}
            local tempLifts = {dragTabItemData.liftNum}
            local tempLiftsForRot = {0}
            self:SetRotation()
            self:SetPlateLine(tempPositions, tempLifts)
            self:SetSliderState(tempPositions, tempLifts)
            self:OnTempBalloonChange(tempPositions, tempLiftsForRot)
            draggingTempPositions = tempPositions
            draggingTempLifts = tempLifts
        else
            local tempPositions = {tempX, tempY}
            local tempLifts = {dragTabItemData.liftNum}
            self:SetAllPlateState(tempPositions, tempLifts)
            draggingTempPositions = tempPositions
            draggingTempLifts = tempLifts
        end
    else
        self:SetAllPlateState()
        self.m_lastDraggingAudioState = ""
        return
    end

    self:_PlayDraggingHoverAudio(draggingTempPositions, draggingTempLifts)
end

BalloonMiniGameRotatePlateCtrl._GetWorstGravityColor = HL.Method(HL.Opt(HL.Table, HL.Table)).Return(HL.String)
    << function(self, tempPositions, tempLifts)
    local vector2Normalize = self.m_curLevel:GetNowCenterOfGravityNormalize(tempPositions, tempLifts)
    local dirty = self.m_curGame:IsCurLevelDirty(tempPositions, tempLifts)
    local maxDeviation = self.m_curLevel.rawData.gravityConfig.maxDeviation

    local function getAxisColor(axisValue)
        if not dirty then
            return "None"
        end
        if math.abs(axisValue) <= maxDeviation then
            return "Green"
        elseif math.abs(axisValue) > 0.5 then
            return "Red"
        else
            return "Yellow"
        end
    end

    local horColor = getAxisColor(vector2Normalize.x)
    local verColor = getAxisColor(vector2Normalize.y)

    local COLOR_SEVERITY = { None = 0, Green = 1, Yellow = 2, Red = 3 }
    return (COLOR_SEVERITY[horColor] or 0) >= (COLOR_SEVERITY[verColor] or 0) and horColor or verColor
end

BalloonMiniGameRotatePlateCtrl._PlayDraggingHoverAudio = HL.Method(HL.Opt(HL.Table, HL.Table))
    << function(self, tempPositions, tempLifts)
    if not tempPositions or not tempLifts then
        self.m_lastDraggingAudioState = ""
        return
    end

    local worstColor = self:_GetWorstGravityColor(tempPositions, tempLifts)

    if worstColor == self.m_lastDraggingAudioState or worstColor == "None" then
        return
    end
    self.m_lastDraggingAudioState = worstColor

    if worstColor == "Red" then
        AudioAdapter.PostEvent("Au_UI_Hover_BalloonRecycle_Red")
    elseif worstColor == "Yellow" then
        AudioAdapter.PostEvent("Au_UI_Hover_BalloonRecycle_Yellow")
    elseif worstColor == "Green" then
        AudioAdapter.PostEvent("Au_UI_Hover_BalloonRecycle_Green")
    end
end

BalloonMiniGameRotatePlateCtrl.SetAllPlateState = HL.Method(HL.Opt(HL.Table, HL.Table))
    << function(self, tempPositions, tempLifts)
    self:SetRotation(tempPositions, tempLifts)
    self:SetPlateLine(tempPositions, tempLifts)
    self:SetSliderState(tempPositions, tempLifts)
    self:OnTempBalloonChange(tempPositions, tempLifts)
end

BalloonMiniGameRotatePlateCtrl.SetSliderState = HL.Method(HL.Opt(HL.Table, HL.Table))
    << function(self, tempPositions, tempLifts)
    local vector2Normalize = self.m_curLevel:GetNowCenterOfGravityNormalize(tempPositions, tempLifts)
    local verSlider = self.view.centerNode.balloonMinigameSliderVer
    local horSlider = self.view.centerNode.balloonMinigameSliderHor
    local vec2 = self.m_curLevel:GetTotalLift(tempPositions, tempLifts)
    local dirty = self.m_curGame:IsCurLevelDirty(tempPositions, tempLifts)
    local maxDeviation = self.m_curLevel.rawData.gravityConfig.maxDeviation

    if self.m_verSliderTween then
        self.m_verSliderTween:Kill()
        self.m_verSliderTween = nil
    end
    if self.m_horSliderTween then
        self.m_horSliderTween:Kill()
        self.m_horSliderTween = nil
    end

    local function setSliderStateFunc(slider, axisValue, liftValue, isVertical)
        local isBalance = dirty and math.abs(axisValue) <= maxDeviation
        local hasPlayedFlag = self.m_hasPlayedHorBalanceAnim
        if isVertical then
            hasPlayedFlag = self.m_hasPlayedVerBalanceAnim
        end
        local shouldSetBalance = isBalance and not hasPlayedFlag
        if not dirty then
            slider.stateController:SetState("Empty")
            if isVertical then
                self.m_hasPlayedVerBalanceAnim = false
            else
                self.m_hasPlayedHorBalanceAnim = false
            end
        else
            if not isBalance then
                if isVertical then
                    self.m_hasPlayedVerBalanceAnim = false
                else
                    self.m_hasPlayedHorBalanceAnim = false
                end

                local showBalloon = false
                if self.m_phase and self.m_phase.isDragIngCell then
                    local hi = self.m_phase.nowDragHighLightItemData
                    if hi then
                        local hiState = self.m_curLevel:GetNowPositionState(hi.cx, hi.cy)
                        showBalloon = hiState ~= MiniGameBalloonUtils.BalloonBlockState.CANT_PLACE
                    end
                end
                if showBalloon then
                    slider.stateController:SetState("Balloon")
                else
                    slider.stateController:SetState("LiftNum")
                    slider.forceTxt.text = math.abs(lume.round(liftValue))
                end
                local directionState
                if isVertical then
                    directionState = liftValue > 0 and "Top" or "Bottom"
                else
                    directionState = liftValue > 0 and "Right" or "Left"
                end
                slider.stateController:SetState(directionState)
            end
            hasPlayedFlag = self.m_hasPlayedHorBalanceAnim
            if isVertical then
                hasPlayedFlag = self.m_hasPlayedVerBalanceAnim
            end
            if not hasPlayedFlag then
                slider.stateController:SetState("Slider")
            end
            if not isBalance then
                slider.stateController:SetState(math.abs(axisValue) > 0.5 and "Red" or "Yellow")
            else
                slider.stateController:SetState("Green")
            end
        end

        local baseValue = 0.5
        local offset = math.abs(axisValue) / 2

        local nowValue = slider.slider.value
        local directedLift = isVertical and -liftValue or liftValue
        local targetValue = directedLift > 0 and (baseValue + offset) or (baseValue - offset)
        if nowValue ~= targetValue then
            local tween = CSUtils.TweenTo(0, 1, self.view.config.BALLOON_RISE_TIME, function(x)
                if NotNull(slider.gameObject) then
                    slider.slider.value = lume.lerp(nowValue, targetValue, x)
                    if x == 1 and shouldSetBalance then
                        slider.stateController:SetState("Balance")
                        if isVertical then
                            self.m_hasPlayedVerBalanceAnim = true
                        else
                            self.m_hasPlayedHorBalanceAnim = true
                        end
                    end
                end
            end)
            if isVertical then
                if self.m_verSliderTween then self.m_verSliderTween:Kill() end
                self.m_verSliderTween = tween
                self.m_verSliderTween:Play()
            else
                if self.m_horSliderTween then self.m_horSliderTween:Kill() end
                self.m_horSliderTween = tween
                self.m_horSliderTween:Play()
            end
        elseif shouldSetBalance then
            slider.stateController:SetState("Balance")
            if isVertical then
                self.m_hasPlayedVerBalanceAnim = true
            else
                self.m_hasPlayedHorBalanceAnim = true
            end
        end
    end

    setSliderStateFunc(verSlider, vector2Normalize.y, vec2.y, true)
    setSliderStateFunc(horSlider, vector2Normalize.x, vec2.x, false)
    self:RefreshLiftNumNode(tempPositions, tempLifts)
end

BalloonMiniGameRotatePlateCtrl.RefreshLiftNumNode = HL.Method(HL.Opt(HL.Table, HL.Table)) << function(self, tempPositions, tempLifts)
    local curLift = self.m_curLift

    if tempPositions and tempLifts then
        local previewLiftByPos = {}
        for index, lift in ipairs(tempLifts) do
            local cx = tempPositions[index * 2 - 1]
            local cy = tempPositions[index * 2]
            if cx and cy then
                previewLiftByPos[cx] = previewLiftByPos[cx] or {}
                previewLiftByPos[cx][cy] = lift
            end
        end
        for cx, col in pairs(previewLiftByPos) do
            for cy, lift in pairs(col) do
                local oldState = self.m_curLevel:GetNowPositionState(cx, cy)
                curLift = curLift - self:GetStateLift(oldState) + math.max(lift, 0)
            end
        end
    end

    local isBalance = curLift >= self.m_needLift
    self.view.centerNode.liftNumNode:SetState(isBalance and "Balance" or "NoBalance")
    self.view.centerNode.liftTxt.text = string.format("%d/%d", curLift, self.m_needLift)
end


BalloonMiniGameRotatePlateCtrl.SetBlockStateByPosition = HL.Method(HL.Number, HL.Number, CS.Beyond.Gameplay.BalloonBlockState)
    << function(self, cx, cy, state)
    if not self.m_gridItems then
        return
    end
    self.m_gridItems[cx] = self.m_gridItems[cx] or {}
    local cellItem = self.m_gridItems[cx][cy]
    if cellItem then
        local oldState = self.m_curLevel:GetNowPositionState(cx, cy)
        self.m_curLevel:SetBalloonBlock(cx, cy, state)
        local newState = self.m_curLevel:GetNowPositionState(cx, cy)
        if oldState ~= newState then
            self:ApplyLiftCacheChange(oldState, newState)
        end
        self:UpdateGridCell(cellItem.cell, cellItem.index)
        if state ~= MiniGameBalloonUtils.BalloonBlockState.PLACE_ABLE and state ~= MiniGameBalloonUtils.BalloonBlockState.CANT_PLACE then
            cellItem.cell:PlayGridOnAnimation()
            AudioAdapter.PostEvent("Au_UI_Event_BalloonRecycle_Put_B")
            AudioAdapter.PostEvent("Au_UI_Event_BalloonRecycle_Float")
            self:_PlayPlacementColorAudio()
        end
    end
end

BalloonMiniGameRotatePlateCtrl._PlayPlacementColorAudio = HL.Method() << function(self)
    local worstColor = self:_GetWorstGravityColor()

    if worstColor == "Red" then
        AudioAdapter.PostEvent("Au_UI_Event_BalloonRecycle_FloatRed")
    elseif worstColor == "Yellow" then
        AudioAdapter.PostEvent("Au_UI_Event_BalloonRecycle_FloatYellow")
    elseif worstColor == "Green" then
        AudioAdapter.PostEvent("Au_UI_Event_BalloonRecycle_FloatGreen")
    end
end

BalloonMiniGameRotatePlateCtrl.SwapBlocksByPosition = HL.Method(HL.Number, HL.Number, HL.Number, HL.Number)
    << function(self, cx1, cy1, cx2, cy2)
    if not self.m_gridItems then
        return
    end
    self.m_curLevel:SwapBalloonBlocks(cx1, cy1, cx2, cy2)
    local cellItem1 = self.m_gridItems[cx1] and self.m_gridItems[cx1][cy1]
    local cellItem2 = self.m_gridItems[cx2] and self.m_gridItems[cx2][cy2]
    if cellItem1 then
        self:UpdateGridCell(cellItem1.cell, cellItem1.index)
    end
    if cellItem2 then
        self:UpdateGridCell(cellItem2.cell, cellItem2.index)
    end
end

BalloonMiniGameRotatePlateCtrl.UpdateGridCellByPosition = HL.Method(HL.Number, HL.Number) << function(self, cx, cy)
    if not self.m_gridItems then
        return
    end
    self.m_gridItems[cx] = self.m_gridItems[cx] or {}
    local cellItem = self.m_gridItems[cx][cy]
    if cellItem then
        self:UpdateGridCell(cellItem.cell, cellItem.index)
    end
end

BalloonMiniGameRotatePlateCtrl.GetNeedResetAnswerState = HL.Method().Return(HL.Boolean) << function(self)
    if not self.m_curLevel or self.m_showAllAnswerMiniGameId == "" then
        return true
    end
    if not self.m_needShowAllAnswer then
        return true
    end
    return self.m_showAllAnswerMiniGameId ~= self.m_curLevel.rawData.levelId
end

BalloonMiniGameRotatePlateCtrl.ToggleAllAnswer = HL.Method(HL.Boolean) << function(self, show)
    if not self.m_gridItems then
        return
    end
    if not show and self.m_showAllAnswerMiniGameId == self.m_curLevel.rawData.levelId then
        return
    end
    self.m_needShowAllAnswer = show
    self.m_showAllAnswerMiniGameId = self.m_curLevel.rawData.levelId
    for cx, v in pairs(self.m_gridItems) do
        for cy, itemData in pairs(v) do
            itemData.cell:ResetState(true)
        end
    end
end

BalloonMiniGameRotatePlateCtrl.SetAllLineImageInterval = HL.Method(HL.Opt(HL.Table, HL.Table))
    << function(self, tempPositions, tempLifts)
    if not self.m_gridItems then
        return
    end
    local vec2 = self.m_curLevel:GetNowCenterOfGravityNormalize(tempPositions, tempLifts)
    local BLOCK_STATE = MiniGameBalloonUtils.BalloonBlockState
    for cx, v in pairs(self.m_gridItems) do
        for cy, itemData in pairs(v) do
            local curBlockState = self.m_curLevel:GetNowPositionState(cx, cy)
            if curBlockState ~= BLOCK_STATE.CANT_PLACE and curBlockState ~= BLOCK_STATE.PLACE_ABLE then
                itemData.cell:SetLineInterval(vec2)
            end
        end
    end
end

BalloonMiniGameRotatePlateCtrl.SetRotation = HL.Method(HL.Opt(HL.Table, HL.Table))
    << function(self, tempPositions, tempLifts)
    local vec2 = self.m_curLevel:GetNowCenterOfGravityNormalize(tempPositions, tempLifts)
    local minAngle = self.view.config.MAX_ROTATE_ANGLE_MIN_CHESSBOARD
    local maxAngle = self.view.config.MAX_ROTATE_ANGLE_MAX_CHESSBOARD
    local minBoard = self.view.config.MIN_CHESSBOARD
    local maxBoard = self.view.config.MAX_CHESSBOARD
    local boardSizeX = self.m_curLevel.rawData.boardSizeX
    local t = 0
    if maxBoard > minBoard then
        t = math.max(0, math.min(1, (maxBoard - boardSizeX) / (maxBoard - minBoard)))
    end
    local angle = maxAngle + (minAngle - maxAngle) * t
    local targetEuler = Vector3(
        -vec2.y * angle,
        vec2.x * angle,
        0
    )
    local startRot = self.view.rotateTarget.localEulerAngles
    local normalizedStart = Vector3(
        math.fmod(startRot.x + 180, 360) - 180,
        math.fmod(startRot.y + 180, 360) - 180,
        0
    )
    local startQuat = Quaternion.Euler(normalizedStart.x, normalizedStart.y, 0)
    local targetQuat = Quaternion.Euler(targetEuler.x, targetEuler.y, 0)
    self:UpdateAllBalloonCells(startQuat)

    local tween = CSUtils.TweenTo(0, 1, self.view.config.BALLOON_RISE_TIME, function(x)
        if NotNull(self.view.rotateTarget) then
            local slerpQuat = Quaternion.Slerp(startQuat, targetQuat, x)
            self.view.rotateTarget.rotation = slerpQuat
            self:UpdateAllBalloonCells(slerpQuat)
        end
    end)
    if self.m_chessboardTween then
        self.m_chessboardTween:Kill()
    end
    self.m_chessboardTween = tween
    self.m_chessboardTween:Play()
end

BalloonMiniGameRotatePlateCtrl.SetSuccessState = HL.Method() << function(self)
    if not self.m_phase then
        logger.error("phase: [Balloon] is not init")
        return
    end

    local levelRawData = self.m_curLevel.rawData
    local maxDisX = math.floor((levelRawData.boardSizeX + 1) / 2)
    local maxDisY = math.floor((levelRawData.boardSizeY + 1) / 2)
    local successCells = {}
    self.view.balloonMinigameGridEndPlate.gridLayout.constraintCount = levelRawData.boardSizeX
    self.m_gridSuccessCell:Refresh(levelRawData.boardSizeX * levelRawData.boardSizeY, function(cell, index)
        local sizeX = levelRawData.boardSizeX
        local sizeY = levelRawData.boardSizeY
        local x = (index - 1) % sizeX
        local y = math.floor((index - 1) / sizeY)
        local distBottom = levelRawData.boardSizeY - 1 - x
        local distRight = levelRawData.boardSizeY - 1 - y
        local dis = math.min(x, distBottom, y, distRight)
        cell.gameObject.name = index
        local maxDis = math.abs(x) > math.abs(y) and maxDisX or maxDisY
        cell.stateController:SetState("SuccForce" .. maxDis - dis)
        cell.canvasGroup.alpha = 0
        table.insert(successCells, cell)
    end)

    if self.m_successCellAnimCoroutine then
        self.m_successCellAnimCoroutine = self:_ClearCoroutine(self.m_successCellAnimCoroutine)
    end

    for i = #successCells, 2, -1 do
        local randomIndex = math.random(i)
        successCells[i], successCells[randomIndex] = successCells[randomIndex], successCells[i]
    end

    self.m_successCellAnimCoroutine = self:_StartCoroutine(function()
        local interval = self.view.config.END_FIX_ANIM_INTERVAL_TIME or 0
        for index, cell in ipairs(successCells) do
            cell.canvasGroup.alpha = 1
            cell.animationWrapper:PlayInAnimation()
            if index < #successCells then
                coroutine.wait(interval)
            end
        end
        self.m_successCellAnimCoroutine = nil
    end)
end

BalloonMiniGameRotatePlateCtrl.SetPlateLine = HL.Method(HL.Opt(HL.Table, HL.Table))
    << function(self, tempPositions, tempLifts)
    local plateLineNodeName = "balloonPlateLineNode"
    local levelRawData = self.m_curLevel.rawData
    local boardSizeX = levelRawData.boardSizeX
    local plateLineNode = self.view.balloonMinigameGridPlate.lineNode
    local index = math.floor((boardSizeX + 1) / 2)
    local MAX_NUM = 4
    local vector2Normalize = self.m_curLevel:GetNowCenterOfGravityNormalize(tempPositions, tempLifts)
    local normalizeAbs = Vector2(math.abs(vector2Normalize.x), math.abs(vector2Normalize.y))

    local startIndex = lume.round((index - 2) * math.max(normalizeAbs.x, normalizeAbs.y) - 0.01) + 1
    for i = 1, MAX_NUM do
        local lineNodeState = plateLineNode[plateLineNodeName .. i]
        local lineNodeTransform = lineNodeState.gameObject.transform
        local lineRectMask = plateLineNode[plateLineNodeName .. i .. "RectMask2D"]
        local needShow = i <= index and not self.m_curLevel:CheckChessboardIsBalance(tempPositions, tempLifts) and (i == startIndex or i == startIndex +1)
        lineNodeTransform.gameObject:SetActiveIfNecessary(needShow)
        if needShow then
            local width = lineNodeTransform.rect.width
            local height = lineNodeTransform.rect.height
            local vec4 = Vector4(0, 0, 0, 0)
            if vector2Normalize.y > 0 then
                vec4.y = height
            elseif vector2Normalize.y < 0 then
                vec4.w = height
            end
            if vector2Normalize.x > 0 then
                vec4.x = width
            elseif vector2Normalize.x < 0 then
                vec4.z = width
            end
            lineRectMask.hgSoftness = vec4
            lineNodeState:SetState(math.max(normalizeAbs.x, normalizeAbs.y) > 0.5 and "Red" or "Yellow")
        end
    end
end

BalloonMiniGameRotatePlateCtrl.OnBalloonCellDrop = HL.Method(HL.Table) << function(self, args)
    self.m_hasPlayedSuccessAnim = false
    local isEmptyAndBalance = self.m_curLevel:CheckChessboardIsBalance() and self.m_curLevel:CheckAllBalloonIsEmpty()
    if self.m_lastBalanceState ~= isEmptyAndBalance then
        if isEmptyAndBalance then
            self.view.mainState:SetState(MiniGameBalloonUtils.GAME_STATE.SUCCESS)
        else
            self.view.mainState:SetState(MiniGameBalloonUtils.GAME_STATE.PLAY)
        end
        self.m_lastBalanceState = isEmptyAndBalance
    end

    self:SetAllLineImageInterval()
end

BalloonMiniGameRotatePlateCtrl.OnTempBalloonChange = HL.Method(HL.Opt(HL.Table, HL.Table)) << function(self, tempPositions, tempLifts)
    local isEmptyAndBalance = self.m_curLevel:CheckChessboardIsBalance(tempPositions, tempLifts) and self.m_curLevel:CheckAllBalloonIsEmpty(tempPositions, tempLifts)
    if isEmptyAndBalance and not self.m_hasPlayedSuccessAnim then
        self.view.animationWrapper:PlayWithTween("balloonminigame_plate_succse")
        self.m_hasPlayedSuccessAnim = true
    elseif not isEmptyAndBalance and not tempPositions then
        self.m_hasPlayedSuccessAnim = false
    end
    if self.m_lastBalanceState ~= isEmptyAndBalance then
        self.view.mainState:SetState(isEmptyAndBalance and MiniGameBalloonUtils.GAME_STATE.SUCCESS or MiniGameBalloonUtils.GAME_STATE.PLAY)
        self.m_phase.m_balloonMainPanel:SetConfirmBtn(isEmptyAndBalance)
        self.m_lastBalanceState = isEmptyAndBalance
        self:SetAllLineImageInterval(tempPositions, tempLifts)
    end
end

HL.Commit(BalloonMiniGameRotatePlateCtrl)