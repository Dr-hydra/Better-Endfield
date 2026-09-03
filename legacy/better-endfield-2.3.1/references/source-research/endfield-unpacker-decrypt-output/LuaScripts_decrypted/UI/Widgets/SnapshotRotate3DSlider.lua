local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SnapshotRotate3DSlider = HL.Class('SnapshotRotate3DSlider', UIWidgetBase)

local RotateBtnAniState = {
    Default = 1,
    Hover = 2,
}


SnapshotRotate3DSlider.m_followSlotIndex = HL.Field(HL.Number) << -1
SnapshotRotate3DSlider.m_lateTickKey = HL.Field(HL.Number) << -1
SnapshotRotate3DSlider.m_isRotating = HL.Field(HL.Boolean) << false
SnapshotRotate3DSlider.m_rotateBtnAniState = HL.Field(HL.Number) << 0
SnapshotRotate3DSlider.m_sliderTailStartPos = HL.Field(HL.Any)
SnapshotRotate3DSlider.m_sliderTailRadius = HL.Field(HL.Number) << 0
SnapshotRotate3DSlider.m_rotateDragDirection = HL.Field(HL.Number) << 0
SnapshotRotate3DSlider.m_rotateDragLastDir = HL.Field(HL.Any)
SnapshotRotate3DSlider.m_rotateDragLastAngle = HL.Field(HL.Number) << 0
SnapshotRotate3DSlider.m_rootTransform = HL.Field(CS.UnityEngine.Transform)
SnapshotRotate3DSlider.m_rotateBtnIconWorldCorners = HL.Field(HL.Any)




SnapshotRotate3DSlider._OnFirstTimeInit = HL.Override() << function(self)
    self:_InitUI()
end

SnapshotRotate3DSlider.InitSnapshotRotate3DSlider = HL.Method(CS.UnityEngine.Transform) << function(self, rootTransform)
    self.m_rootTransform = rootTransform
    self:_FirstTimeInit()
    self:_RefreshAllUI()
end

SnapshotRotate3DSlider._OnDisable = HL.Override() << function(self)
    self:EndRotateDrag()
    self:_StopFollowTick()
    self:_SetRotateButtonAniState(RotateBtnAniState.Default)
end

SnapshotRotate3DSlider._OnDestroy = HL.Override() << function(self)
    self:EndRotateDrag()
    self:_StopFollowTick()
end



SnapshotRotate3DSlider._UpdateData = HL.Method() << function(self)
end



SnapshotRotate3DSlider._InitUI = HL.Method() << function(self)

    local tailRect = self.view.rotateSliderNode.sliderTail.rectTransform
    self.m_sliderTailStartPos = tailRect.anchoredPosition
    self.m_sliderTailRadius = self.m_sliderTailStartPos.magnitude
    self.m_rotateBtnIconWorldCorners = CS.System.Array.CreateInstance(typeof(Vector3), 4)
    self:EndRotateDrag()
    self.view.rotateBtnNode.gameObject:SetActiveIfNecessary(true)
    self.view.rotateSliderNode.gameObject:SetActiveIfNecessary(false)
end

SnapshotRotate3DSlider._RefreshAllUI = HL.Method() << function(self)
    self:EndRotateDrag()
end

SnapshotRotate3DSlider.PlayRotateButtonHoverAni = HL.Method() << function(self)
    self:_SetRotateButtonAniState(RotateBtnAniState.Hover)
end

SnapshotRotate3DSlider.CancelRotateButtonHoverAni = HL.Method() << function(self)
    self:_SetRotateButtonAniState(RotateBtnAniState.Default)
end

SnapshotRotate3DSlider._SetRotateButtonAniState = HL.Method(HL.Number) << function(self, state)
    if self.m_rotateBtnAniState == state then
        return
    end
    self.m_rotateBtnAniState = state
    local aniWrapper = self.view.rotateBtnNode.rotateBtnIconAniWrapper
    if IsNull(aniWrapper) then
        return
    end
    aniWrapper:ClearTween(false)
    aniWrapper:Play(state == RotateBtnAniState.Hover and "rotate3d_hover" or "rotate3d_default")
end




SnapshotRotate3DSlider.GetRotateButtonHitScreenData = HL.Method().Return(HL.Boolean, Vector2, Vector2, HL.Number) << function(self)
    if not self.gameObject.activeInHierarchy then
        return false, Vector2.zero, Vector2.zero, 0
    end
    if not self.view.rotateBtnNode.gameObject.activeInHierarchy then
        return false, Vector2.zero, Vector2.zero, 0
    end

    local iconRect = self.view.rotateBtnNode.rotateBtnIcon.rectTransform
    local camera = CameraManager.mainCamera

    local iconScreenPoint = camera:WorldToScreenPoint(iconRect.position)
    if iconScreenPoint.z < 0 then
        return false, Vector2.zero, Vector2.zero, 0
    end

    local worldCorners = self.m_rotateBtnIconWorldCorners
    iconRect:GetWorldCorners(worldCorners)
    local minX = math.huge
    local minY = math.huge
    local maxX = -math.huge
    local maxY = -math.huge
    for i = 0, 3 do
        local screenPoint = camera:WorldToScreenPoint(worldCorners[i])
        minX = math.min(minX, screenPoint.x)
        minY = math.min(minY, screenPoint.y)
        maxX = math.max(maxX, screenPoint.x)
        maxY = math.max(maxY, screenPoint.y)
    end
    local size = Vector2(maxX - minX, maxY - minY)
    if size.x <= 0 or size.y <= 0 then
        return false, Vector2.zero, Vector2.zero, 0
    end

    local center = Vector2((minX + maxX) * 0.5, (minY + maxY) * 0.5)
    return true, center, size, iconScreenPoint.z
end


SnapshotRotate3DSlider.IsRotateButtonScreenPosHit = HL.Method(Vector2).Return(HL.Boolean) << function(self, screenPos)
    local isValid, center, size = self:GetRotateButtonHitScreenData()
    if not isValid then
        return false
    end

    local halfSize = size * 0.5
    return math.abs(screenPos.x - center.x) <= halfSize.x
        and math.abs(screenPos.y - center.y) <= halfSize.y
end


SnapshotRotate3DSlider.GetRotateCenterScreenPos = HL.Method().Return(HL.Opt(Vector2)) << function(self)
    local camera = CameraManager.mainCamera
    if IsNull(camera) then
        return nil
    end

    local screenPos3 = camera:WorldToScreenPoint(self.view.rotateSliderNode.rectTransform.position)
    if screenPos3.z < 0 then
        return nil
    end
    return Vector2(screenPos3.x, screenPos3.y)
end


SnapshotRotate3DSlider.CalcDragDeltaAngle = HL.Method(Vector2, Vector2, Vector2).Return(HL.Number) << function(self, centerScreenPos, beginDir, curScreenPos)
    local curDir = curScreenPos - centerScreenPos
    if curDir.sqrMagnitude <= 0.0001 then
        return 0
    end
    curDir = curDir.normalized



    local beginAngle = self:_CalcScreenDirAngle(beginDir)
    local curAngle = self:_CalcScreenDirAngle(curDir)
    local clockwiseAngle = (beginAngle - curAngle + 360) % 360
    local counterClockwiseAngle = (curAngle - beginAngle + 360) % 360


    local moveDir = 0
    if self.m_rotateDragLastDir ~= nil then
        moveDir = self:_CalcFrameMoveDirection(self.m_rotateDragLastDir, curDir)
    end

    self:_RefreshRotateDragDirection(clockwiseAngle, counterClockwiseAngle, moveDir)
    local signedAngle = self:_GetSignedRotateDragAngle(clockwiseAngle, counterClockwiseAngle)
    self.m_rotateDragLastAngle = math.abs(signedAngle)
    self.m_rotateDragLastDir = curDir
    return signedAngle
end


SnapshotRotate3DSlider._CalcScreenDirAngle = HL.Method(Vector2).Return(HL.Number) << function(self, dir)
    local atan2 = math.atan2 or math.atan
    local angle = atan2(dir.y, dir.x) * 180 / math.pi
    if angle < 0 then
        angle = angle + 360
    end
    return angle
end


SnapshotRotate3DSlider._CalcFrameMoveDirection = HL.Method(Vector2, Vector2).Return(HL.Number) << function(self, lastDir, curDir)
    local frameDelta = -Vector2.SignedAngle(lastDir, curDir)
    if frameDelta > 0 then
        return 1
    elseif frameDelta < 0 then
        return -1
    end
    return 0
end


SnapshotRotate3DSlider._RefreshRotateDragDirection = HL.Method(HL.Number, HL.Number, HL.Number) << function(self, clockwiseAngle, counterClockwiseAngle, moveDir)

    if self.m_rotateDragDirection == 0 then
        self.m_rotateDragDirection = clockwiseAngle <= counterClockwiseAngle and 1 or -1
        return
    end

    if moveDir == 0 then
        return
    end

    local curAngle = self.m_rotateDragDirection > 0 and clockwiseAngle or counterClockwiseAngle
    if moveDir == self.m_rotateDragDirection then


        if curAngle < self.m_rotateDragLastAngle then
            self.m_rotateDragDirection = -self.m_rotateDragDirection
        end
        return
    end


    if curAngle > self.m_rotateDragLastAngle then
        self.m_rotateDragDirection = moveDir
    end
end


SnapshotRotate3DSlider._GetSignedRotateDragAngle = HL.Method(HL.Number, HL.Number).Return(HL.Number) << function(self, clockwiseAngle, counterClockwiseAngle)
    if self.m_rotateDragDirection >= 0 then
        return clockwiseAngle
    end
    return -counterClockwiseAngle
end


SnapshotRotate3DSlider._ResetRotateDragDirection = HL.Method() << function(self)
    self.m_rotateDragDirection = 0
    self.m_rotateDragLastDir = nil
    self.m_rotateDragLastAngle = 0
end


SnapshotRotate3DSlider.BeginRotateDrag = HL.Method() << function(self)
    self.m_isRotating = true
    self.view.rotateBtnNode.gameObject:SetActiveIfNecessary(false)
    self.view.rotateSliderNode.gameObject:SetActiveIfNecessary(true)
    self:ResetRotateDragProgress()
end


SnapshotRotate3DSlider.RefreshRotateDelta = HL.Method(HL.Number) << function(self, deltaAngle)
    local absAngle = math.abs(deltaAngle)
    local fillAmount = absAngle / 360


    self.view.rotateSliderNode.transform.localRotation =
        deltaAngle < 0 and Quaternion.Euler(0, 180, 0) or Quaternion.identity
    self.view.rotateSliderNode.sliderBar.fillAmount = fillAmount
    self:_RefreshSliderTail(fillAmount)
end


SnapshotRotate3DSlider.ResetRotateDragProgress = HL.Method() << function(self)
    self:_ResetRotateDragDirection()
    self.view.rotateSliderNode.sliderBar.fillAmount = 0
    self.view.rotateSliderNode.transform.localRotation = Quaternion.identity
    self:_RefreshSliderTail(0)
end


SnapshotRotate3DSlider.EndRotateDrag = HL.Method() << function(self)
    self.m_isRotating = false
    self:ResetRotateDragProgress()
    self.view.rotateSliderNode.gameObject:SetActiveIfNecessary(false)
    self.view.rotateBtnNode.gameObject:SetActiveIfNecessary(true)
end


SnapshotRotate3DSlider._RefreshSliderTail = HL.Method(HL.Number) << function(self, fillAmount)
    local tailRect = self.view.rotateSliderNode.sliderTail.rectTransform
    local startPos = self.m_sliderTailStartPos
    local radius = self.m_sliderTailRadius
    if startPos == nil or radius <= 0 then
        return
    end


    local atan2 = math.atan2 or math.atan
    local startAngle = atan2(startPos.y, startPos.x)
    local angle = startAngle - fillAmount * math.pi * 2
    local pos = Vector2(math.cos(angle), math.sin(angle)) * radius
    tailRect.anchoredPosition = pos



    local zAngle = atan2(-pos.y, -pos.x) * 180 / math.pi
    tailRect.localRotation = Quaternion.Euler(0, 0, zAngle + -90)
end



SnapshotRotate3DSlider.SetFollowTarget = HL.Method(HL.Number) << function(self, slotIndex)
    self.m_followSlotIndex = slotIndex
    self.gameObject:SetActiveIfNecessary(true)
    self:_StartFollowTick()
    self:_LateTick(0)
end

SnapshotRotate3DSlider.ClearFollowTarget = HL.Method() << function(self)
    self:EndRotateDrag()
    self.m_followSlotIndex = -1
    self:_StopFollowTick()
end

SnapshotRotate3DSlider._StartFollowTick = HL.Method() << function(self)
    if self.m_lateTickKey > 0 then
        return
    end
    self.m_lateTickKey = LuaUpdate:Add("LateTick", function(deltaTime)
        self:_LateTick(deltaTime)
    end)
end

SnapshotRotate3DSlider._StopFollowTick = HL.Method() << function(self)
    self.m_lateTickKey = LuaUpdate:Remove(self.m_lateTickKey)
end

SnapshotRotate3DSlider._LateTick = HL.Method(HL.Number) << function(self, _)
    if self.m_followSlotIndex < 0 then
        return
    end

    local entity = self:_GetFollowEntity()
    if entity == nil then
        self:ClearFollowTarget()
        self.gameObject:SetActiveIfNecessary(false)
        return
    end

    local targetTrans = entity.rootCom.transform
    if IsNull(targetTrans) then
        self:ClearFollowTarget()
        self.gameObject:SetActiveIfNecessary(false)
        return
    end

    local camera = CameraManager.mainCamera
    if IsNull(camera) then
        return
    end

    self.m_rootTransform.position = targetTrans.position + self.view.config.WORLD_OFFSET
    self:_RefreshScale(targetTrans.position, camera)
    self:_FaceCamera(camera)
    self:_FaceRotateButtonIconToCamera(camera)
end

SnapshotRotate3DSlider._RefreshScale = HL.Method(Vector3, CS.UnityEngine.Camera) << function(self, targetPos, camera)
    local curCamDis = (camera.transform.position - targetPos).magnitude
    if curCamDis <= 0 then
        return
    end
    local scale = (curCamDis * self.view.config.SCALE_STANDARD_FOCAL_LENGTH)
        / (camera.focalLength * self.view.config.SCALE_STANDARD_CAM_DISTANCE)
    scale = lume.clamp(scale, self.view.config.SCALE_MIN, self.view.config.SCALE_MAX)
    self.m_rootTransform.localScale = Vector3.one * scale
end

SnapshotRotate3DSlider._FaceCamera = HL.Method(CS.UnityEngine.Camera) << function(self, camera)
    local lookDir = self.m_rootTransform.position - camera.transform.position
    if lookDir.sqrMagnitude <= 0 then
        return
    end
    local lookEulerAngles = Quaternion.LookRotation(lookDir, Vector3.up).eulerAngles
    local xAngle = lookEulerAngles.x
    if xAngle > 180 then
        xAngle = xAngle - 360
    end
    xAngle = lume.clamp(xAngle, self.view.config.FACE_CAMERA_MIN_X_ANGLE, self.view.config.FACE_CAMERA_MAX_X_ANGLE)
    self.m_rootTransform.rotation = Quaternion.Euler(xAngle, lookEulerAngles.y, lookEulerAngles.z)
end

SnapshotRotate3DSlider._FaceRotateButtonIconToCamera = HL.Method(CS.UnityEngine.Camera) << function(self, camera)
    if not self.view.rotateBtnNode.gameObject.activeInHierarchy then
        return
    end

    local iconTrans = self.view.rotateBtnNode.rotateBtnIcon.transform
    local lookDir = iconTrans.position - camera.transform.position
    if lookDir.sqrMagnitude <= 0 then
        return
    end


    local lookEulerAngles = Quaternion.LookRotation(lookDir, camera.transform.up).eulerAngles
    local curEulerAngles = iconTrans.rotation.eulerAngles
    iconTrans.rotation = Quaternion.Euler(lookEulerAngles.x, curEulerAngles.y, curEulerAngles.z)
end

SnapshotRotate3DSlider._GetFollowEntity = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    if self.m_followSlotIndex < 0 then
        return nil
    end

    local entity = GameInstance.player.squadManager:GetMemberBySlot(self.m_followSlotIndex)
    if entity == nil or not entity.alive or entity.rootCom == nil then
        return nil
    end
    return entity
end



HL.Commit(SnapshotRotate3DSlider)
return SnapshotRotate3DSlider

