local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local LIFT_TO_TIPS = {
    [1] = "Tips1",
    [2] = "Tips2",
    [3] = "Tips3",
    [6] = "Tips4",
    [16] = "Tips0",
}

local MOVE_POS = 16

BalloonSeedNode = HL.Class('BalloonSeedNode', UIWidgetBase)

BalloonSeedNode.m_ctrl = HL.Field(HL.Any)

BalloonSeedNode.m_cx = HL.Field(HL.Number) << 0

BalloonSeedNode.m_cy = HL.Field(HL.Number) << 0

BalloonSeedNode.m_rawX = HL.Field(HL.Number) << 0

BalloonSeedNode.m_rawY = HL.Field(HL.Number) << 0

BalloonSeedNode.m_index = HL.Field(HL.Number) << 0

BalloonSeedNode.m_curLiftNum = HL.Field(HL.Number) << 0

BalloonSeedNode.m_ring = HL.Field(HL.Number) << 0

BalloonSeedNode.m_isSetUnPlace = HL.Field(HL.Boolean) << false

BalloonSeedNode.m_isRising = HL.Field(HL.Boolean) << false

BalloonSeedNode.m_isNaviFlat = HL.Field(HL.Boolean) << false

BalloonSeedNode.m_balloonMat = HL.Field(HL.Userdata)

BalloonSeedNode.m_balloonNormalMat = HL.Field(HL.Userdata)

BalloonSeedNode.m_balloonRiseTween = HL.Field(HL.Userdata)

BalloonSeedNode.m_targetDisturbU = HL.Field(HL.Number) << 0

BalloonSeedNode.m_targetDisturbV = HL.Field(HL.Number) << 0

BalloonSeedNode._OnFirstTimeInit = HL.Override() << function(self)
    self.view.balloonImgDragItem.onBeginDragEvent:AddListener(function(eventData)
        self:_OnBeginDrag(eventData)
    end)
    self.view.balloonImgDragItem.onDragEvent:AddListener(function(eventData)
        self:_OnDrag(eventData)
    end)
    self.view.balloonImgDragItem.onEndDragEvent:AddListener(function(eventData)
        self:_OnEndDrag(eventData)
    end)
    self.view.dropItem.onDropEvent:AddListener(function(eventData)
        self:_OnDrop(eventData)
    end)
    self.view.dropItem.onToggleHighlight:AddListener(function(active)
        self:_OnToggleHighlight(active)
    end)
    self.view.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:AddListener(function(select)
        self:_OnGroupSetAsNaviTarget(select)
    end)
    self.view.balloonImgDragItem.onUpdateDragObject:AddListener(function(dragObj, srcObj)
        if self.m_curLiftNum and self.m_curLiftNum > 0 then
            local dragImg = dragObj:GetComponent(typeof(CS.Beyond.UI.UIImage))
            if dragImg then
                dragImg.material = nil
                dragImg:LoadSprite(UIConst.UI_SPRITE_MINIGAME_BALLOON, "icon_balloon_type_" .. self.m_curLiftNum)
            end
            dragObj.transform.rotation = Quaternion.identity
        end
    end)
end

BalloonSeedNode.InitBalloonSeedNode = HL.Method(HL.Any, HL.Number) << function(self, ctrl, index)
    self:_FirstTimeInit()
    if self.m_ctrl == ctrl and self.m_index == index then
        self:ResetState()
        return
    end
    self.m_ctrl = ctrl
    self.m_index = index

    local levelRawData = ctrl.m_curLevel.rawData
    local sizeX = levelRawData.boardSizeX
    local sizeY = levelRawData.boardSizeY
    local x = (index - 1) % sizeX
    local y = math.floor((index - 1) / sizeY)
    local centerX = math.floor(sizeX / 2)
    local centerY = math.floor(sizeY / 2)
    self.m_rawX = x
    self.m_rawY = y
    self.m_cx = x - centerX
    self.m_cy = centerY - y
    self.m_ring = math.min(3, math.max(math.abs(self.m_cx), math.abs(self.m_cy)))

    if not self.m_balloonMat then
        self.m_balloonMat = Unity.Material(ctrl.m_baseBalloonMat)
        self.m_balloonNormalMat = Unity.Material(ctrl.m_baseBalloonMat)
    end
    local halfSize = math.max(1, math.floor(sizeX / 2))
    local dist = math.sqrt(self.m_cx * self.m_cx + self.m_cy * self.m_cy)
    local distFactor = math.min(1, dist / halfSize)
    local DISTURB_FACTOR = self.m_ctrl.view.config.DISTURB_FACTOR or 0.1
    self.m_targetDisturbU = self.m_cx < 0 and DISTURB_FACTOR * distFactor or (self.m_cx > 0 and -DISTURB_FACTOR * distFactor or 0)
    self.m_targetDisturbV = self.m_cy < 0 and DISTURB_FACTOR * distFactor or (self.m_cy > 0 and -DISTURB_FACTOR * distFactor or 0)
    local sortingOrder = ctrl:GetSortingOrder()
    self.view.balloonImgCanvas.sortingOrder = sortingOrder
    self.view.preCrosshairCanvas.sortingOrder = sortingOrder - 1
    self.view.hintRect.canvas.sortingOrder = sortingOrder
    self.gameObject.name = string.format("gridCell_%s_%s", self.m_cx, self.m_cy)
    self:ResetState()
end

BalloonSeedNode.ResetState = HL.Method(HL.Opt(HL.Boolean)) << function(self, doNotTween)
    local ctrl = self.m_ctrl
    local cx, cy = self.m_cx, self.m_cy
    local BLOCK_STATE = MiniGameBalloonUtils.BalloonBlockState

    local curBlockState = ctrl.m_curLevel:GetNowPositionState(cx, cy)
    local liftNum = curBlockState:GetHashCode()
    self.m_curLiftNum = liftNum > 0 and liftNum or 0
    self.m_isSetUnPlace = false
    if self.m_curLiftNum <= 0 then
        self.m_isNaviFlat = false
    end

    local cellState
    local lineState = "Line"
    if curBlockState == BLOCK_STATE.CANT_PLACE then
        cellState = "ForbidPlaced"
    elseif ctrl.m_curLevel:CheckPositionNoPlace(cx, cy) then
        cellState = "CanPlaced"
    else
        cellState = "AlreadyPlaced"
        if DeviceInfo.usingController then
            local dragData = ctrl.m_phase.nowDragChessboardItemData
            if dragData and dragData.cx == cx and dragData.cy == cy then
                cellState = "CanPlaced"
            end
        end
        if self.m_balloonMat and ctrl.m_balloonTextures[liftNum] then
            self.m_balloonMat:SetTexture("_VFXMainTex", ctrl.m_balloonTextures[liftNum])
        end
        self.view.balloonImg.material = self.m_balloonMat
        if not doNotTween then
            self:PlayBalloonRiseTween()
        end
        lineState = "LineLight"
    end
    if ctrl.m_needShowAllAnswer and cellState ~= "AlreadyPlaced" then
        local answerState = ctrl.m_curLevel:CheckPositionCorrectAnswerState(cx, cy)
        local tipsState = LIFT_TO_TIPS[answerState:GetHashCode()]
        if tipsState then
            self.view.stateController:SetState("TipsPlaced")
            cellState = tipsState
        end
    end
    self.view.stateController:SetState(lineState)
    self.view.stateController:SetState(cellState)
    self.view.stateController:SetState("Bg" .. self.m_ring)


    self.view.balloonImgDragItem.enabled = self.m_curLiftNum > 0
    self.view.balloonImgDragItem.luaTable = {
        dropItem = self.view.dropItem,
        dragItem = self.view.balloonImgDragItem,
        liftNum = self.m_curLiftNum,
        cx = cx,
        cy = cy,
    }
end

BalloonSeedNode._OnBeginDrag = HL.Method(HL.Any) << function(self, eventData)
    local ctrl = self.m_ctrl
    if ctrl.m_phase then
        ctrl.m_phase.nowDragChessboardItemData = {
            cx = self.m_cx,
            cy = self.m_cy,
            liftNum = self.m_curLiftNum,
        }
        self.view.balloonImgButton.enabled = false
        ctrl.m_phase:OnBeginDragCell(eventData)
    end
end

BalloonSeedNode._OnDrag = HL.Method(HL.Any) << function(self, eventData)
    local ctrl = self.m_ctrl
    if ctrl.m_phase then
        if not self.m_isSetUnPlace then
            self.view.stateController:SetState("CanclePlaced")
        end
        ctrl.m_phase:OnDraggingCell(eventData)
    end
end

BalloonSeedNode._OnEndDrag = HL.Method(HL.Any) << function(self, eventData)
    if InputManagerInst.inChangingInputDevice then
        return
    end
    local ctrl = self.m_ctrl
    if ctrl.m_phase then
        ctrl.m_phase:OnEndDragCell(eventData)
    end
    self.view.balloonImgButton.enabled = true
    ctrl.m_phase.nowDragChessboardItemData = nil
    self:ResetState()
    self.m_isSetUnPlace = false
end

BalloonSeedNode._OnDrop = HL.Method(HL.Any) << function(self, eventData)
    if IsNull(eventData.pointerDrag) then
        return
    end
    local ctrl = self.m_ctrl
    local cx, cy = self.m_cx, self.m_cy
    local BLOCK_STATE = MiniGameBalloonUtils.BalloonBlockState
    local dragItem = eventData.pointerDrag:GetComponent(typeof(CS.Beyond.UI.UIDragItem))
    if dragItem and dragItem.inDragging and dragItem.luaTable then
        local itemData = dragItem.luaTable
        local liftNum = MiniGameBalloonUtils.BLOCK_STATE[itemData.liftNum]
        if not liftNum then
            return
        end
        if itemData.cx and itemData.cy then
            local targetState = ctrl.m_curLevel:GetNowPositionState(cx, cy)
            if targetState == BLOCK_STATE.CANT_PLACE then
                return
            end
            ctrl.m_phase.m_balloonMainPanel:SetSuppressTabCellCountAnim(true)
            if targetState ~= BLOCK_STATE.PLACE_ABLE then
                ctrl:SwapBlocksByPosition(itemData.cx, itemData.cy, cx, cy)

                ctrl.m_phase.dropHandled = true
                ctrl.m_phase.m_balloonMainPanel:SetSuppressTabCellCountAnim(false)
                ctrl:SetAllPlateState()
                return
            end
            ctrl:SetBlockStateByPosition(itemData.cx, itemData.cy, BLOCK_STATE.PLACE_ABLE)
        end
        ctrl:SetBlockStateByPosition(cx, cy, liftNum)
        if itemData.cx and itemData.cy then
            ctrl.m_phase.m_balloonMainPanel:SetSuppressTabCellCountAnim(false)
        end
        ctrl:SetAllPlateState()
    end
end

BalloonSeedNode._OnToggleHighlight = HL.Method(HL.Boolean) << function(self, active)
    local ctrl = self.m_ctrl
    local crossHair = ctrl.view.balloonMinigameGridPlate.preCrosshairTemplate
    if not ctrl.m_phase or not active then
        crossHair.gameObject:SetActiveIfNecessary(false)
        ctrl.m_phase.nowDragHighLightItemData = nil
        if not ctrl.m_phase.dragEnterMask then
            ctrl:SetDraggingPlateState()
        else
            ctrl:SetSliderState()
        end
    elseif active then
        crossHair.transform:SetParent(self.view.preCrosshairCanvas.transform, false)
        crossHair.gameObject:SetActiveIfNecessary(true)
        ctrl:SetDraggingPlateState()
    end
end

BalloonSeedNode.PlayBalloonRiseTween = HL.Method() << function(self)
    if self.m_curLiftNum <= 0 then
        return
    end
    local ctrl = self.m_ctrl
    local riseTime = ctrl.view.config.BALLOON_RISE_TIME or 0.5
    local curFloatZ = -ctrl.view.config.BALLOON_FLOAT_Z or -80
    self.m_isRising = true
    self.m_isNaviFlat = false
    local balloonTransform = self.view.balloonImg.transform
    local mat = self.m_balloonMat
    if self.m_balloonRiseTween then
        self.m_balloonRiseTween:Kill()
        self.m_balloonRiseTween = nil
    end
    balloonTransform.localPosition = Vector3.zero
    balloonTransform.localRotation = Quaternion.identity
    if mat then
        mat:SetFloat("_DisturbUIntensity1", 0)
        mat:SetFloat("_DisturbVIntensity1", 0)
    end

    local floatVec = Vector3(0, 0, curFloatZ)
    self.m_balloonRiseTween = CSUtils.TweenTo(0, 1, riseTime, function(t)
        if NotNull(balloonTransform) then
            if t >= 1 then
                self.m_isRising = false
                self.m_balloonRiseTween = nil
            end
            local curRot = ctrl.view.rotateTarget.rotation
            local curInvRot = Quaternion.Inverse(curRot)
            balloonTransform.localPosition = Vector3.Lerp(Vector3.zero, curInvRot * floatVec, t)
            balloonTransform.localRotation = Quaternion.Slerp(Quaternion.identity, curInvRot, t)
            if mat then
                mat:SetFloat("_DisturbUIntensity1", self.m_targetDisturbU * t)
                mat:SetFloat("_DisturbVIntensity1", self.m_targetDisturbV * t)
            end
        end
    end)
    self.m_balloonRiseTween:Play()
end

BalloonSeedNode.PlayGridOnAnimation = HL.Method() << function(self)
    if not self.m_ctrl then
        return
    end
    local curBlockState = self.m_ctrl.m_curLevel:GetNowPositionState(self.m_cx, self.m_cy)
    local BLOCK_STATE = MiniGameBalloonUtils.BalloonBlockState
    if curBlockState == BLOCK_STATE.CANT_PLACE then
        return
    end
    self.view.animationWrapper:PlayWithTween("balloon_grid_on")
end

BalloonSeedNode.OnNaviSelected = HL.Method(HL.Opt(HL.Number)) << function(self, previewLiftNum)
    local ctrl = self.m_ctrl
    if previewLiftNum and previewLiftNum > 0 then
        if self.m_balloonNormalMat and ctrl.m_balloonTextures[previewLiftNum] then
            self.m_balloonNormalMat:SetTexture("_VFXMainTex", ctrl.m_balloonTextures[previewLiftNum])
        end
        self.view.balloonImgNormal.material = self.m_balloonNormalMat
        self.view.balloonImgNormal.gameObject:SetActiveIfNecessary(true)
        self.view.hintRect.transform.anchoredPosition = Vector2(MOVE_POS, -MOVE_POS)
    else
        self.view.balloonImgNormal.gameObject:SetActiveIfNecessary(false)
        self.view.hintRect.transform.anchoredPosition = Vector2.zero
    end
    self:SetBalloonFlatForNavi()
    self.view.hintRect.gameObject:SetActiveIfNecessary(true)
end

BalloonSeedNode.OnNaviDeselected = HL.Method() << function(self)
    self:ReplayBalloonRiseTweenForNavi()
    self.view.balloonImgNormal.gameObject:SetActiveIfNecessary(false)
    self.view.hintRect.gameObject:SetActiveIfNecessary(false)
    self.view.hintRect.transform.anchoredPosition = Vector2.zero
end

BalloonSeedNode.SetBalloonFlatForNavi = HL.Method() << function(self)
    if self.m_curLiftNum <= 0 then
        return
    end
    if self.m_balloonRiseTween then
        self.m_balloonRiseTween:Kill()
        self.m_balloonRiseTween = nil
    end
    self.m_isRising = false
    self.m_isNaviFlat = true
    local balloonTransform = self.view.balloonImg.transform
    balloonTransform.localPosition = Vector3.zero
    balloonTransform.localRotation = Quaternion.identity
end

BalloonSeedNode.ReplayBalloonRiseTweenForNavi = HL.Method() << function(self)
    if self.m_curLiftNum <= 0 or not self.m_isNaviFlat then
        return
    end
    self:PlayBalloonRiseTween()
end

BalloonSeedNode.UpdateBalloonTransform = HL.Method(HL.Userdata, HL.Userdata) << function(self, invRot, balloonOffset)
    if self.m_isNaviFlat then
        return
    end
    self.view.balloonImg.transform.localRotation = invRot
    self.view.balloonImg.transform.localPosition = balloonOffset
end

BalloonSeedNode.DestroyBalloonMaterial = HL.Method() << function(self)
    if self.m_balloonRiseTween then
        self.m_balloonRiseTween:Kill()
        self.m_balloonRiseTween = nil
    end
    if self.m_balloonMat then
        Unity.Object.Destroy(self.m_balloonMat)
        self.m_balloonMat = nil
    end

    if self.m_balloonNormalMat then
        Unity.Object.Destroy(self.m_balloonNormalMat)
        self.m_balloonNormalMat = nil
    end
end

BalloonSeedNode.SetLineInterval = HL.Method(HL.Any) << function(self, vector2Normalize)
    local config = self.m_ctrl.view.config
    local maxInterval = config.MAX_POS_INTERVAL
    local minInterval = config.MIN_POS_INTERVAL
    local maxVec2 = math.max(math.abs(vector2Normalize.x), math.abs(vector2Normalize.y))
    local interval = lume.lerp(minInterval, maxInterval, maxVec2)
    local MAX_NUM = 5
    for i = 1, MAX_NUM do
        local item = self.view["lineImage0" .. i]
        item.gameObject.transform.localPosition = Vector3(0, 0, interval * i)
    end
end

BalloonSeedNode._OnGroupSetAsNaviTarget = HL.Method(HL.Boolean) << function(self, select)
    self.m_ctrl:OnGroupSetAsNaviTarget(self, self.m_cx, self.m_cy, select)
end

HL.Commit(BalloonSeedNode)
return BalloonSeedNode
