local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.Snapshot
local PHASE_ID = PhaseId.Snapshot
SnapshotCtrl = HL.Class('SnapshotCtrl', uiCtrl.UICtrl)







SnapshotCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SQUAD_INFIGHT_CHANGED] = 'OnSquadInFightChanged',
    [MessageConst.FORBID_SYSTEM_CHANGED] = 'OnForbidSystemChanged',
    [MessageConst.SNAPSHOT_LISTENER_IDENTIFY_CHANGED] = 'OnListenerIdentifyChanged',
    [MessageConst.ON_FIRST_GOT_ITEM] = 'OnFirstGotItem',

    [MessageConst.ON_TELEPORT_SQUAD] = 'AutoCloseSelfOnInterrupt',
    [MessageConst.DEAD_ZONE_ROLLBACK] = 'AutoCloseSelfOnInterrupt',
    [MessageConst.ALL_CHARACTER_DEAD] = 'AutoCloseSelfOnInterrupt',
    [MessageConst.ON_CINEMATIC_TO_QUEUE] = 'OnCinematicToQueue',
    [MessageConst.CLOSE_SNAPSHOT] = '_OnMsgCloseSnapshot',
    [MessageConst.SNAPSHOT_ACTION_FORCE_RESET] = '_OnActionForceReset',
    [MessageConst.ON_BATTLE_SQUAD_CHANGED] = '_OnBattleSquadChanged',
    [MessageConst.ON_CHARACTER_DEAD] = '_OnCharacterDead',
    [MessageConst.ON_SCREEN_SIZE_CHANGED] = '_OnScreenSizeChanged',
}


local snapshotSystem = GameInstance.player.snapshotSystem

local formationManager = GameWorld.aiManager.characterPhotoSystem

local inventorySystem = GameInstance.player.inventory

local DATA_KEY_MOVE_MODE = "SNAPSHOT_IS_CAMERA_MOVE_MODE"

local FIRST_PERSON_FORBID_KEY = "FirstPerson"



local TEAM_FORMATION_INDEX_NONE = -1
local TEAM_FORMATION_INDEX_CUSTOM = -2
local FORMATION_DROPDOWN_INDEX_NONE = 0
local FORMATION_DROPDOWN_INDEX_CUSTOM = 1
local FORMATION_DROPDOWN_PRESET_OFFSET = 2

local MOVE_SELECT_ARROW_HEAD_WORLD_OFFSET = Vector3(0, 0.35, 0)

local MOVE_CHAR_OUTLINE_ASSET = "P_fxgp_common_char_outline_01_nofade_asset"
local MOVE_CHAR_ROTATE_3D_SLIDER_PREFAB_PATH =
    "Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Snapshot/Widget/SnapshotRotate3DSlider.prefab"

local showCharConfig = {
    {
        nameLuaKey = "LUA_SNAPSHOT_SHOW_CHAR_ALL",
        showLeader = true,
        showTeamMate = true,
    },
    {
        nameLuaKey = "LUA_SNAPSHOT_SHOW_CHAR_LEADER_ONLY",
        showLeader = true,
        showTeamMate = false,
    },
    {
        nameLuaKey = "LUA_SNAPSHOT_SHOW_CHAR_HIDE_ALL",
        showLeader = false,
        showTeamMate = false,
    },
}

local environmentOptionConfig = {
    {
        key = "npc",
        nameLuaKey = "LUA_SNAPSHOT_ENV_NPC",
    },
    {
        key = "dropItem",
        nameLuaKey = "LUA_SNAPSHOT_ENV_DROP_ITEM",
    },
    {
        key = "decorationBuilding",
        nameLuaKey = "LUA_SNAPSHOT_ENV_DECORATION_BUILDING",
    },
    {
        key = "otherBuilding",
        nameLuaKey = "LUA_SNAPSHOT_ENV_OTHER_BUILDING",
    },
}


local controllerExitHideUIAnyKeyDown = {
    [CS.Beyond.Input.GamepadKeyCode.RightStickBtn] = true,
    [CS.Beyond.Input.GamepadKeyCode.ArrowUp] = true,
    [CS.Beyond.Input.GamepadKeyCode.ArrowDown] = true,
    [CS.Beyond.Input.GamepadKeyCode.ArrowLeft] = true,
    [CS.Beyond.Input.GamepadKeyCode.ArrowRight] = true,

    [CS.Beyond.Input.GamepadKeyCode.A] = true,
    [CS.Beyond.Input.GamepadKeyCode.B] = true,
    [CS.Beyond.Input.GamepadKeyCode.X] = true,
    [CS.Beyond.Input.GamepadKeyCode.Y] = true,

    [CS.Beyond.Input.GamepadKeyCode.LB] = true,
    [CS.Beyond.Input.GamepadKeyCode.LT] = true,
    [CS.Beyond.Input.GamepadKeyCode.RB] = true,
    [CS.Beyond.Input.GamepadKeyCode.RT] = true,

    [CS.Beyond.Input.GamepadKeyCode.LeftMenuBtn] = true,
    [CS.Beyond.Input.GamepadKeyCode.RightMenuBtn] = true,
    [CS.Beyond.Input.GamepadKeyCode.Home] = true,
    [CS.Beyond.Input.GamepadKeyCode.TouchPanel] = true,
}

local CharFormationCameraNames = {
    "TeamCamera",
    "vcam_formation",
    "TeamExtraCamera",
    "MultiCamera"
}




SnapshotCtrl.m_arg = HL.Field(HL.Table)

SnapshotCtrl.m_cameraCtrl = HL.Field(HL.Forward("SnapshotCameraCtrl"))

SnapshotCtrl.m_isInCapture = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_captureTexture = HL.Field(HL.Any)

SnapshotCtrl.m_tipTimerKey = HL.Field(HL.Number) << -1

SnapshotCtrl.m_nextAutoFocusTime = HL.Field(HL.Number) << -1

SnapshotCtrl.m_defaultFocus = HL.Field(HL.Number) << -1

SnapshotCtrl.m_isInCloseProcess = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_cinematicInQueueWaitCloseSnapshot = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_autoFocusDistanceTime = HL.Field(HL.Number) << 0


SnapshotCtrl.m_isManualFocus = HL.Field(HL.Boolean) << true


SnapshotCtrl.m_focusCharSlotIndex = HL.Field(HL.Number) << -1


SnapshotCtrl.m_addManualFocusCoroutine = HL.Field(HL.Thread)


SnapshotCtrl.m_minusManualFocusCoroutine = HL.Field(HL.Thread)


SnapshotCtrl.m_addYAxisRotCoroutine = HL.Field(HL.Thread)


SnapshotCtrl.m_minusYAxisRotCoroutine = HL.Field(HL.Thread)

SnapshotCtrl.m_keepCamPosWhenClose = HL.Field(HL.Boolean) << true


SnapshotCtrl.m_onZoom = HL.Field(HL.Function)

SnapshotCtrl.m_onDrag = HL.Field(HL.Function)

SnapshotCtrl.m_onDragBegin = HL.Field(HL.Function)

SnapshotCtrl.m_onDragEnd = HL.Field(HL.Function)

SnapshotCtrl.m_onClickTouchPlate = HL.Field(HL.Function)

SnapshotCtrl.m_isShowSnapshotUI = HL.Field(HL.Boolean) << true

SnapshotCtrl.m_addFocalLengthCoroutine = HL.Field(HL.Thread)

SnapshotCtrl.m_minusFocalLengthCoroutine = HL.Field(HL.Thread)

SnapshotCtrl.m_isMenuNodeFocused = HL.Field(HL.Boolean) << false





SnapshotCtrl.m_curSelectMenuIndex = HL.Field(HL.Number) << 0

SnapshotCtrl.m_menuTabCellList = HL.Field(HL.Table)

SnapshotCtrl.m_menuContentCellList = HL.Field(HL.Table)

SnapshotCtrl.m_onChangeContentFuncList = HL.Field(HL.Table)

SnapshotCtrl.m_isMenuExpand = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_isBasicSliderControllerInputInited = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_isQuickMoveCharControllerInputInited = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_resumeMenuControllerFocusType = HL.Field(HL.Any)

SnapshotCtrl.m_resumeMenuControllerFocusIndex = HL.Field(HL.Number) << 0



SnapshotCtrl.m_environmentSelection = HL.Field(HL.Table)

SnapshotCtrl.m_isShowGridLines = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_addApertureCoroutine = HL.Field(HL.Thread)

SnapshotCtrl.m_minusApertureCoroutine = HL.Field(HL.Thread)



SnapshotCtrl.m_curTeamFormationIndex = HL.Field(HL.Number) << -1

SnapshotCtrl.m_formationAvatarCellCache = HL.Field(HL.Forward("UIListCache"))

SnapshotCtrl.m_curShowCharIndex = HL.Field(HL.Number) << 1

SnapshotCtrl.m_gamepadMoveRotateEntrySlot = HL.Field(HL.Number) << -1

SnapshotCtrl.m_gamepadMoveRotateEntryFromMenu = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_needIgnoreMenuFormationNavi = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_quickAvatarCellCache = HL.Field(HL.Forward("UIListCache"))

SnapshotCtrl.m_dragStartScreenPos = HL.Field(HL.Any)

SnapshotCtrl.m_dragHitMoveChar = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_dragHitRotateChar = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_isKeyboardQuickRotateCharMode = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_rotateDragBeginScreenPos = HL.Field(HL.Any)

SnapshotCtrl.m_rotateDragBeginDir = HL.Field(HL.Any)

SnapshotCtrl.m_rotateDragCenterScreenPos = HL.Field(HL.Any)

SnapshotCtrl.m_moveCharRotate3DSliderGo = HL.Field(CS.UnityEngine.GameObject)

SnapshotCtrl.m_moveCharRotate3DSlider = HL.Field(HL.Forward("SnapshotRotate3DSlider"))

SnapshotCtrl.m_squadChangeFinishHandle = HL.Field(HL.Any)

SnapshotCtrl.m_isSquadDirty = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_skipThisTimeQuickMoveCharChangePreAvatar = HL.Field(HL.Boolean) << false



SnapshotCtrl.m_filterInfos = HL.Field(HL.Table)

SnapshotCtrl.m_getFilterCellFunc = HL.Field(HL.Function)

SnapshotCtrl.m_curUsedFilterIndex = HL.Field(HL.Number) << 0

SnapshotCtrl.m_curSelectFilterIndex = HL.Field(HL.Number) << 0



SnapshotCtrl.m_stickerInfos = HL.Field(HL.Table)

SnapshotCtrl.m_isInitRefreshStickerUI = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_getStickerCellFunc = HL.Field(HL.Function)

SnapshotCtrl.m_curUsedStickerIndex = HL.Field(HL.Number) << 0

SnapshotCtrl.m_curSelectStickerIndex = HL.Field(HL.Number) << 0

SnapshotCtrl.m_inStickerEditMode = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_hideSnapshotUIBySticker = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_editStickerCtrl = HL.Field(HL.Any)



SnapshotCtrl.m_squadCharList = HL.Field(HL.Table)

SnapshotCtrl.m_curSelectAvatarIndex = HL.Field(HL.Number) << 0

SnapshotCtrl.m_actionAvatarCellCache = HL.Field(HL.Forward("UIListCache"))

SnapshotCtrl.m_actionInfos = HL.Field(HL.Table)

SnapshotCtrl.m_curSelectActionIndex = HL.Field(HL.Number) << 0

SnapshotCtrl.m_actionViewedNewSet = HL.Field(HL.Table)

SnapshotCtrl.m_actionProgressCoroutine = HL.Field(HL.Thread)

SnapshotCtrl.m_videoProgressCoroutine = HL.Field(HL.Thread)

SnapshotCtrl.m_curActionVideoPath = HL.Field(HL.String) << ""

SnapshotCtrl.m_isInitRefreshActionUI = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_getActionCellFunc = HL.Field(HL.Function)

SnapshotCtrl.m_actionPreAvatarBindingId = HL.Field(HL.Number) << -1

SnapshotCtrl.m_actionNextAvatarBindingId = HL.Field(HL.Number) << -1






SnapshotCtrl.m_lastSuccess = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_updateKey = HL.Field(HL.Number) << -1

SnapshotCtrl.m_lateTickKey = HL.Field(HL.Number) << -1

SnapshotCtrl.m_hasTraceIdentify = HL.Field(HL.Boolean) << false

SnapshotCtrl.m_traceIdentifyGroupId = HL.Field(HL.String) << ""

SnapshotCtrl.m_unTraceIdentifyGroupIds = HL.Field(HL.Table)

SnapshotCtrl.m_overrideTraceIdentifyGroupId = HL.Field(HL.String) << ""

SnapshotCtrl.m_identifyInfos = HL.Field(HL.Table)

SnapshotCtrl.m_indicatorCellCache = HL.Field(HL.Forward("UIListCache"))

SnapshotCtrl.m_identifyGoalCellCache = HL.Field(HL.Forward("UIListCache"))



SnapshotCtrl.m_forbidRecords = HL.Field(HL.Table)



SnapshotCtrl.m_eventLogInfo = HL.Field(HL.Table)








SnapshotCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg
    snapshotSystem:OpenSnapshotMode(self:_BuildMoveRotateCharConfig(), true)
    self:_InitUI()
    self:_InitData(arg)
    self:_RefreshAllUI()
    self:_ResumePanel()
    self:_AddRegisters()


    if not PhaseManager.isRecovering then
        EventLogManagerInst:GameEvent_Snapshot(1)
        if self.m_eventLogInfo.isFromActivity then
            EventLogManagerInst:GameEvent_SnapshotActivityStart(
                self.m_eventLogInfo.activityId,
                self.m_eventLogInfo.stageId,
                self.m_eventLogInfo.isFromInteractive and 1 or 2
            )
        end
    end

end

SnapshotCtrl.OnShow = HL.Override() << function(self)
    self:_SwitchAllWorldUIActive(false)
    self.m_updateKey = LuaUpdate:Add("TailTick", function()
        self:_OnUpdate()
    end)
end

SnapshotCtrl.OnAnimationInFinished = HL.Override() << function(self)
    self.m_lateTickKey = LuaUpdate:Add("LateTick", function()
        self:_OnLateTick()
    end)
end

SnapshotCtrl.OnHide = HL.Override() << function(self)
    self.m_updateKey = LuaUpdate:Remove(self.m_updateKey)
    self:_DeselectMoveChar()
end

SnapshotCtrl.OnClose = HL.Override() << function(self)

    GameInstance.player.forbidSystem:SetPhaseForbid("CharInfo", "SNAPSHOT", false, nil)
    UIManager:ToggleBlockObtainWaysJump("SNAPSHOT", false)
    self:_ClearRegisters()
    self:_CloseActionVideo()
    self:_DeselectMoveChar()

    if self.m_editStickerCtrl then
        self.m_editStickerCtrl:Close()
        self.m_editStickerCtrl = nil
    end



    local isHotSwitch = InputManagerInst.inChangingInputDevice
    if not isHotSwitch then

        snapshotSystem:SwitchMoveMode(false)
        self:_SwitchAllWorldUIActive(true)
        self:_SetEnvironmentSelection({
            npc = true,
            dropItem = true,
            decorationBuilding = true,
            otherBuilding = true,
        })
        self:_ApplyEnvironmentVisibility()
        self:_SwitchShowGridLines(false)
        self:_ChangeCharShowMode(1)
        self:_ChangeTeamFormation(TEAM_FORMATION_INDEX_NONE, true)
        self:_ChangeFilter(1)
        self:_ClearFilter()
        snapshotSystem.camController:ResetToInitialParam()
        self:_SwitchPersonPerspectiveMode(false, false)

        snapshotSystem:ResetAllCustomActions()
        self.view.quickMoveRotateCharNode.stateController:SetState("CloseQuickMoveRotate")
        snapshotSystem:CloseSnapshotMode(self.m_keepCamPosWhenClose)
    end

end

SnapshotCtrl.ShowSnapshot = HL.StaticMethod(HL.Opt(HL.Any)) << function(args)

    local isForbidden, forbidParams = Utils.isForbiddenWithReason(ForbidType.ForbidGeneralAbility)
    if isForbidden then
        if forbidParams and forbidParams:IsStyleForbidden(CS.Beyond.Gameplay.GeneralAbilityForbidParams.ForbidStyle.Snapshot) then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_SNAPSHOT_FORBID_SNAPSHOT)
            logger.info("拍照模式当前被禁用")
            return
        end
    end
    if LuaSystemManager.mainHudActionQueue:HasRequest(Const.CinematicQueueType) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SNAPSHOT_FORBID_SNAPSHOT)
        logger.info("拍照模式当前被禁用，原因是mainHud队列里有演出")
        return
    end

    snapshotSystem:SetForcePlayRadio(true)
    if args then
        local param = unpack(args)
        PhaseManager:OpenPhase(PhaseId.Snapshot, param)
    else
        PhaseManager:OpenPhase(PhaseId.Snapshot)
    end
end

SnapshotCtrl._AddRegisters = HL.Method() << function(self)
    self.view.touchPlate.onZoom:RemoveListener(self.m_onZoom)
    self.view.touchPlate.onDragBegin:RemoveListener(self.m_onDragBegin)
    self.view.touchPlate.onDrag:RemoveListener(self.m_onDrag)
    self.view.touchPlate.onDragEnd:RemoveListener(self.m_onDragEnd)
    self.view.touchPlate.onClick:RemoveListener(self.m_onClickTouchPlate)
    self.view.touchPlate.onZoom:AddListener(self.m_onZoom)
    self.view.touchPlate.onDragBegin:AddListener(self.m_onDragBegin)
    self.view.touchPlate.onDrag:AddListener(self.m_onDrag)
    self.view.touchPlate.onDragEnd:AddListener(self.m_onDragEnd)
    self.view.touchPlate.onClick:AddListener(self.m_onClickTouchPlate)

    if self.m_squadChangeFinishHandle ~= nil then
        self.m_squadChangeFinishHandle:Clear()
    end
    self.m_squadChangeFinishHandle = GameWorld.eventManager:RegisterLevelEventAction(
        GameLevelEvent.ON_SQUAD_CHANGE_FINISH,
        function(_)
            self:_ReapplyTeamFormationAfterSquadChanged()
        end
    )
end

SnapshotCtrl._ClearRegisters = HL.Method() << function(self)
    if self.m_onZoom then
        self.view.touchPlate.onZoom:RemoveListener(self.m_onZoom)
        self.m_onZoom = nil
    end
    if self.m_onDragBegin then
        self.view.touchPlate.onDragBegin:RemoveListener(self.m_onDragBegin)
        self.m_onDragBegin = nil
    end
    if self.m_onDrag then
        self.view.touchPlate.onDrag:RemoveListener(self.m_onDrag)
        self.m_onDrag = nil
    end
    if self.m_onDragEnd then
        self.view.touchPlate.onDragEnd:RemoveListener(self.m_onDragEnd)
        self.m_onDragEnd = nil
    end
    if self.m_onClickTouchPlate then
        self.view.touchPlate.onClick:RemoveListener(self.m_onClickTouchPlate)
        self.m_onClickTouchPlate = nil
    end
    if self.m_dragHitRotateChar then
        self:_EndDragRotateChar()
    end
    if self.m_dragHitMoveChar then
        snapshotSystem:EndMoveChar()
    end
    self:_ClearMoveRotateDragState()
    self.m_updateKey = LuaUpdate:Remove(self.m_updateKey)
    self.m_lateTickKey = LuaUpdate:Remove(self.m_lateTickKey)


    self.m_addFocalLengthCoroutine = self:_ClearCoroutine(self.m_addFocalLengthCoroutine)
    self.m_minusFocalLengthCoroutine = self:_ClearCoroutine(self.m_minusFocalLengthCoroutine)

    self.m_addApertureCoroutine = self:_ClearCoroutine(self.m_addApertureCoroutine)
    self.m_minusApertureCoroutine = self:_ClearCoroutine(self.m_minusApertureCoroutine)

    self.m_addManualFocusCoroutine = self:_ClearCoroutine(self.m_addManualFocusCoroutine)
    self.m_minusManualFocusCoroutine = self:_ClearCoroutine(self.m_minusManualFocusCoroutine)

    self.m_addYAxisRotCoroutine = self:_ClearCoroutine(self.m_addYAxisRotCoroutine)
    self.m_minusYAxisRotCoroutine = self:_ClearCoroutine(self.m_minusYAxisRotCoroutine)

    self.m_actionProgressCoroutine = self:_ClearCoroutine(self.m_actionProgressCoroutine)
    self.m_videoProgressCoroutine = self:_ClearCoroutine(self.m_videoProgressCoroutine)

    local basicNode = self.view.menuContentNode.menuBasicNode
    basicNode.showCharDropDown.onIsNaviTargetChanged = nil
    basicNode.environmentNode.environmentDropDown.onIsNaviTargetChanged = nil
    basicNode.apertureSlider.onIsNaviTargetChanged = nil
    basicNode.manualFocusDropDownNode.manualFocusDropDown.onIsNaviTargetChanged = nil
    basicNode.manualFocusSliderNode.manualFocusSlider.onIsNaviTargetChanged = nil
    basicNode.yAxisRotSliderNode.yAxisRotSlider.onIsNaviTargetChanged = nil
    self.view.menuContentNode.menuFormationNode.formationDropDown.onIsNaviTargetChanged = nil

    if self.m_squadChangeFinishHandle ~= nil then
        self.m_squadChangeFinishHandle:Clear()
        self.m_squadChangeFinishHandle = nil
    end
end

SnapshotCtrl._OnUpdate = HL.Method() << function(self)

    local traceGroupInfo = self.m_identifyInfos.traceIdentifyGroupInfo
    if traceGroupInfo and #traceGroupInfo.identifyIds > 0 then
        local successIds, targetPosList = snapshotSystem:ExecuteIdentify(traceGroupInfo.identifyIds)

        local matchCount = 0
        for index, id in pairs(traceGroupInfo.identifyIds) do
            local info = traceGroupInfo.identifyInfos[id]
            local curMatched = lume.find(successIds, id)
            if curMatched ~= info.matched then
                local cell = self.m_identifyGoalCellCache:Get(index)
                if cell then
                    cell.animation:ClearTween(false)
                    if curMatched then
                        cell.animation:SampleClipAtPercent("tasktrackhud_celldefault", 1)
                        cell.animation:Play("tasktrackhud_cellfinish")
                    else
                        cell.animation:SampleClipAtPercent("tasktrackhud_cellfinish", 0)
                        cell.animation:Play("tasktrackhud_celldefault")
                    end
                    if curMatched then
                        AudioAdapter.PostEvent("Au_UI_Mission_Step_Complete")
                    end
                end
            end
            info.matched = curMatched
            if curMatched then
                matchCount = matchCount + 1
            end
        end
        local success = matchCount == #traceGroupInfo.identifyIds

        if self.m_lastSuccess ~= success then
            CS.Beyond.Gameplay.Conditions.CheckSnapShotTrace.Trigger(self.m_traceIdentifyGroupId, success)
            self.m_lastSuccess = success


            self.view.shutterBtnHighLight.gameObject:SetActive(success)
            self.view.shutterBtnAnimationWrapper:ClearTween(false)
            self.view.shutterBtnAnimationWrapper:SampleClipAtPercent("shutterbtn_in", 0)
            if success then
                AudioAdapter.PostEvent("Au_UI_Mission_PhotoComplete")
                self.view.shutterBtnAnimationWrapper:Play("shutterbtn_in", function()
                    self.view.shutterBtnAnimationWrapper:Play("shutterbtn_loop")
                end)
            end
        end

        self.m_indicatorCellCache:Refresh(targetPosList.Count, function(cell, luaIndex)

            local pos = targetPosList[CSIndex(luaIndex)]
            local screenPos = CameraManager.mainCamera:WorldToScreenPoint(pos)
            local uiPos = UIUtils.screenPointToUI(Vector2(screenPos.x, screenPos.y), self.uiCamera, self.view.transform)
            cell.transform.anchoredPosition = uiPos

            local curCamDis = (snapshotSystem.camController.cameraTrans.position - pos)
            curCamDis = curCamDis.magnitude
            local curFocal = CameraManager.mainCamera.focalLength
            local scale = (curFocal * self.view.config.INDICATOR_STANDARD_CAM_DISTANCE) / (curCamDis * self.view.config.INDICATOR_STANDARD_FOCAL_LENGTH)
            local finalLength = math.max(scale * self.view.config.INDICATOR_STANDARD_LENGTH, self.view.config.INDICATOR_MIN_LENGTH)
            cell.transform:SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, finalLength)
            cell.transform:SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, finalLength)
        end)
    end


    if self.m_isManualFocus or snapshotSystem.isFirstPersonMode
        or self.m_nextAutoFocusTime <= 0 or Time.time < self.m_nextAutoFocusTime then

    else
        self.m_nextAutoFocusTime = Time.time + self.m_autoFocusDistanceTime
        snapshotSystem:AutoFocusOnSquadMember(self.m_focusCharSlotIndex)
    end
    self:_RefreshMoveSelectArrow()
    self:_RefreshMoveCharRotateButtonHover()

    if DeviceInfo.usingController then
        if self.m_inStickerEditMode then


            local screenWidth = UIManager.uiCanvasRect.rect.size.x
            local delta = screenWidth * self.view.config.CONTROLLER_MOVE_STICKER_SPEED * Time.deltaTime
            local moveDelta = InputManagerInst:GetGamepadStickValue(true) * delta
            local newPos = self.view.stickerImg.rectTransform.anchoredPosition + moveDelta
            self:_SetStickerNewPos(newPos)
        elseif not self.m_isShowSnapshotUI then

            for keyCode, _ in pairs(controllerExitHideUIAnyKeyDown) do
                if InputManagerInst:GetKeyDown(keyCode) then
                    self:_SwitchSnapshotUIVisible(true)
                    break
                end
            end
        end
    end

    if BEYOND_DEBUG then
        snapshotSystem:DrawMoveCharPickGizmos()
        self:_DrawMoveCharRotateButtonHitGizmos()
    end
end

SnapshotCtrl._OnLateTick = HL.Method() << function(self)

    if IsNull(CameraManager.curActiveController) then
        if IsNull(CameraManager.cinemachineBrainCpt) then
            self:_CloseSelf(false, true)
        end
        local topCam = CameraManager.cinemachineBrainCpt:TopCameraFromPriorityQueue()
        if IsNull(topCam) then
            self:_CloseSelf(false, true)
        end
        if not lume.find(CharFormationCameraNames, topCam.name) and topCam.name ~= "SnapshotCamera" then
            self:_CloseSelf(false, true)
        end
    elseif CameraManager.curActiveController.name ~= "SnapshotCamera" then
        self:_CloseSelf(false, true)
    end

end





SnapshotCtrl._BuildMoveRotateCharConfig = HL.Method().Return(CS.Beyond.Gameplay.SnapshotSystem.SnapshotMoveRotateCharConfig) << function(self)
    local viewConfig = self.view.config
    local config = CS.Beyond.Gameplay.SnapshotSystem.SnapshotMoveRotateCharConfig()
    config.outlineAsset = MOVE_CHAR_OUTLINE_ASSET
    config.rotate3DSliderPrefabPath = MOVE_CHAR_ROTATE_3D_SLIDER_PREFAB_PATH
    config.pickRadiusRatio = viewConfig.MOVE_CHAR_PICK_RADIUS_RATIO
    config.pickMinRadiusPx = viewConfig.MOVE_CHAR_PICK_MIN_RADIUS_PX
    config.pickMaxRadiusPx = viewConfig.MOVE_CHAR_PICK_MAX_RADIUS_PX
    return config
end

SnapshotCtrl._InitData = HL.Method(HL.Any) << function(self, arg)

    self.m_cameraCtrl = self.m_phase.snapshotCameraPanel.uiCtrl
    self.m_autoFocusDistanceTime = self.view.config.AUTO_FOCUS_DISTANCE_TIME
    self.m_keepCamPosWhenClose = true

    self.m_onZoom = function(delta)
        self.m_cameraCtrl:ZoomCamera(delta)
    end
    self.m_onDragBegin = function(screenPos)
        if InputManager.GetMouseButton(1) then
            return
        end
        self.m_dragStartScreenPos = screenPos
        if self.m_isKeyboardQuickRotateCharMode then
            self:_TryBeginDragRotateChar(screenPos, false)
            return
        end
        if self:_TryBeginDragRotateChar(screenPos, true) then
            return
        end
        self:_UpdateDragHitMoveChar(screenPos)
    end
    self.m_onDrag = function(eventData)
        if eventData.button == CS.UnityEngine.EventSystems.PointerEventData.InputButton.Right then
            return
        end
        if self.m_inStickerEditMode then
            self:_EnableStickerEditMode(false)
        end

        if self.m_isKeyboardQuickRotateCharMode and not self.m_dragHitRotateChar then
            if not self:_TryBeginDragRotateChar(eventData.position, false) then

                return
            end

        end

        if self.m_dragHitRotateChar then
            self:_OnDragRotateChar(eventData)
        elseif self.m_dragHitMoveChar then
            self:_OnDragMoveChar(eventData)
        else
            self:_MoveCamera(eventData)
        end
    end
    self.m_onDragEnd = function(_)
        if self.m_dragHitRotateChar then
            if self.m_isKeyboardQuickRotateCharMode then
                snapshotSystem:EndRotateChar()
                if self.m_moveCharRotate3DSlider ~= nil then
                    self.m_moveCharRotate3DSlider:ResetRotateDragProgress()
                end
            else
                self:_EndDragRotateChar()
            end
            AudioAdapter.PostEvent("Au_UI_Event_PhotoCharRotate_End")
        elseif self.m_dragHitMoveChar then
            snapshotSystem:EndMoveChar()
            AudioAdapter.PostEvent("Au_UI_Event_PhotoCharDrag_End")
        end
        self:_ClearMoveRotateDragState()
    end
    self.m_onClickTouchPlate = function(eventData)
        local sucControlMoveChar = self:_OnClickTouchPlateForMoveChar(eventData)
        if not self.m_isShowSnapshotUI and not sucControlMoveChar then
            self:_SwitchSnapshotUIVisible(true)
        end
        if self.m_inStickerEditMode then
            self:_EnableStickerEditMode(false)
        end
    end


    self:_InitForbidRecords()



    local _, isCameraMoveMode, _ = ClientDataManagerInst:GetBool(DATA_KEY_MOVE_MODE, true, false)
    isCameraMoveMode = isCameraMoveMode and not GameWorld.battle.isSquadInFight and not DeviceInfo.usingController
    self:_SwitchMoveMode(isCameraMoveMode, false)

    if not self.m_arg.resumeState then
        self:_SwitchSnapshotUIVisible(true, true)
    end
    if Utils.isForbidden(ForbidType.ForbidMove) then
        self:_SetForbid(self.m_forbidRecords.playerMoveMode, true, "ForbidSystem")
        self:_SetForbid(self.m_forbidRecords.action, true, "ForbidSystem")
    end

    if arg.focus then
        self.m_defaultFocus = arg.focus
    else
        self.m_defaultFocus = self.view.config.DEFAULT_FOCAL_LENGTH
    end




    self.m_curSelectMenuIndex = 1



    self:_SetEnvironmentSelection({
        npc = true,
        dropItem = false,
        decorationBuilding = true,
        otherBuilding = true,
    })
    self:_ApplyEnvironmentVisibility()
    self:_SwitchShowGridLines(false)



    self.m_gamepadMoveRotateEntrySlot = -1
    self.m_gamepadMoveRotateEntryFromMenu = false



    self.m_curSelectFilterIndex = 1
    self.m_filterInfos = {}
    table.insert(self.m_filterInfos, {
        isEmpty = true,
        isUnlock = true,
        sortId = math.mininteger,
    })
    for id, filterCfg in pairs(Tables.snapshotFilterTable) do
        local itemId = filterCfg.itemId
        local _, itemCfg = Tables.itemTable:TryGetValue(itemId)
        if not itemCfg then
            logger.error("滤镜对应的itemId表配置不存在！滤镜id：" .. id)
        else
            local isUnlock = filterCfg.isDefaultUnlock or inventorySystem:IsItemGot(itemId)
            local canShow = isUnlock or Utils.isNotObtainCanShow(itemCfg.notObtainShow, itemCfg.notObtainShowTimeId)
            if canShow then
                local info = {
                    id = id,
                    itemId = itemId,
                    name = filterCfg.name,
                    icon = filterCfg.icon,
                    filterPath = filterCfg.filterPath,
                    effectPath = filterCfg.filterEffectPath,
                    desc = filterCfg.desc,
                    sourceText = filterCfg.sourceText,
                    jumpId = filterCfg.jumpId,
                    rewardTaskId = filterCfg.rewardTaskId,

                    isEmpty = false,
                    isUnlock = isUnlock,
                    filterObj = nil,
                    effectInst = nil,

                    sortId = filterCfg.sortId,
                }
                table.insert(self.m_filterInfos, info)
            end
        end
    end
    table.sort(self.m_filterInfos, function(a, b)
        if a.isUnlock ~= b.isUnlock then
            return a.isUnlock
        end
        return a.sortId < b.sortId
    end)



    self.m_curSelectStickerIndex = 1
    self.m_stickerInfos = {}
    table.insert(self.m_stickerInfos, {
        isEmpty = true,
        isUnlock = true,
        sortId = math.mininteger,
    })
    for id, stickerCfg in pairs(Tables.snapshotStickerTable) do
        local itemId = stickerCfg.itemId
        local _, itemCfg = Tables.itemTable:TryGetValue(itemId)
        if not itemCfg then
            logger.error("贴纸对应的itemId表配置不存在！贴纸id：" .. id)
        else
            local isUnlock = stickerCfg.isDefaultUnlock or inventorySystem:IsItemGot(itemId)
            local canShow = isUnlock or Utils.isNotObtainCanShow(itemCfg.notObtainShow, itemCfg.notObtainShowTimeId)
            if canShow then
                local info = {
                    id = id,
                    itemId = itemId,
                    name = stickerCfg.name,
                    icon = stickerCfg.icon,
                    desc = stickerCfg.desc,
                    sourceText = stickerCfg.sourceText,
                    jumpId = stickerCfg.jumpId,
                    rewardTaskId = stickerCfg.rewardTaskId,

                    isEmpty = false,
                    isUnlock = isUnlock,

                    sortId = stickerCfg.sortId,
                }
                table.insert(self.m_stickerInfos, info)
            end
        end
    end
    table.sort(self.m_stickerInfos, function(a, b)
        if a.isUnlock ~= b.isUnlock then
            return a.isUnlock
        end
        return a.sortId < b.sortId
    end)
    self:_ChangeSticker(1, true)



    self.m_squadCharList = {}
    self.m_actionInfos = {}
    self.m_actionViewedNewSet = {}
    self.m_isInitRefreshActionUI = false



    self.m_dragStartScreenPos = nil
    self.m_dragHitMoveChar = false
    self.m_dragHitRotateChar = false
    self.m_rotateDragBeginScreenPos = nil
    self.m_rotateDragBeginDir = nil
    self.m_rotateDragCenterScreenPos = nil
    self.m_curShowCharIndex = 1
    self:_SetMoveSelectUIVisible(false, false)





    self.m_eventLogInfo = {
        isFromInteractive = arg.isFromInteractive,
        isFromActivity = false,
        activityId = "",
        stageId = "",

        identifyGroupId = "",
        traceIdentifyProgress = {},
        traceIdentifySuccess = false,
    }



    self.m_overrideTraceIdentifyGroupId = ""
    if not string.isEmpty(arg.identifyGroupId) then
        self.m_overrideTraceIdentifyGroupId = arg.identifyGroupId
    else

        local trackIdentifyGroupId = snapshotSystem:GetCurTrackIdentifyGroupId()
        if not string.isEmpty(trackIdentifyGroupId) then
            self.m_overrideTraceIdentifyGroupId = trackIdentifyGroupId
        end
    end
    self:_UpdateIdentifyInfo()

end

SnapshotCtrl._InitForbidRecords = HL.Method() << function(self)
    self.m_forbidRecords = {

        switchMoveMode = {
            forbidKeys = {},
            forbidFuncName = "_ForbidSwitchMoveMode",
        },

        hideChar = {
            forbidKeys = {},
            forbidFuncName = "_ForbidHideChar",
        },

        aperture = {
            forbidKeys = {},
            forbidFuncName = "_ForbidAperture",
        },

        manualFocus = {
            forbidKeys = {},
            forbidFuncName = "_ForbidManualFocus",
        },

        switchFormation = {
            forbidKeys = {},
            forbidFuncName = "_ForbidSwitchFormation",
        },

        action = {
            forbidKeys = {},
            forbidFuncName = "_ForbidAction",
        },

        firstPersonPerspective = {
            forbidKeys = {},
            forbidFuncName = "_ForbidFirstPersonPerspective",
        },

        playerMoveMode = {
            forbidKeys = {},
            forbidFuncName = "_ForbidPlayerMove",
        },

        controlCam = {
            forbidKeys = {},
            forbidFuncName = "_ForbidMoveOrRotateCam",
        },
    }
end

SnapshotCtrl._UpdateIdentifyInfo = HL.Method() << function(self)
    self.m_identifyInfos = {
        traceIdentifyGroupInfo = nil,
        allIdentifyGroupIds = {},
        allIdentifyGroupInfos = {},
        allIdentifyIds = {},
    }
    local allIdentifyGroupIds = snapshotSystem:GetCurListenerIdentifyGroupIds()
    allIdentifyGroupIds:Sort()
    for _, groupId in cs_pairs(allIdentifyGroupIds) do
        table.insert(self.m_identifyInfos.allIdentifyGroupIds, groupId)
    end
    allIdentifyGroupIds = self.m_identifyInfos.allIdentifyGroupIds
    if not string.isEmpty(self.m_overrideTraceIdentifyGroupId) then
        self.m_traceIdentifyGroupId = self.m_overrideTraceIdentifyGroupId
        if not lume.find(allIdentifyGroupIds, self.m_traceIdentifyGroupId) then
            table.insert(allIdentifyGroupIds, self.m_traceIdentifyGroupId)
        end
    end
    self.m_hasTraceIdentify = not string.isEmpty(self.m_traceIdentifyGroupId)


    self.m_eventLogInfo.isFromActivity = false
    self.m_eventLogInfo.identifyGroupId = self.m_traceIdentifyGroupId
    local hasCfg, cfg = Tables.identifyGroupId2ActivitySnapshotStageTable:TryGetValue(self.m_traceIdentifyGroupId)
    if hasCfg then
        self.m_eventLogInfo.isFromActivity = true
        self.m_eventLogInfo.activityId = cfg.activityId
        self.m_eventLogInfo.stageId = cfg.stageId
    end

    if self.m_hasTraceIdentify then
        self.m_identifyInfos.traceIdentifyGroupInfo = SnapshotCtrl._WrapIdentifyGroupInfo(self.m_traceIdentifyGroupId, true)
    end
    for _, groupId in pairs(self.m_identifyInfos.allIdentifyGroupIds) do
        local groupInfo
        if groupId == self.m_traceIdentifyGroupId then
            groupInfo = SnapshotCtrl._WrapIdentifyGroupInfo(self.m_traceIdentifyGroupId, true)
            self.m_identifyInfos.traceIdentifyGroupInfo = groupInfo
            CS.Beyond.Gameplay.SnapshotSystem.curRealTraceIdentifyIds:Clear()
            for _, id in pairs(groupInfo.identifyIds) do
                CS.Beyond.Gameplay.SnapshotSystem.curRealTraceIdentifyIds:Add(id)
            end
        else
            groupInfo = SnapshotCtrl._WrapIdentifyGroupInfo(groupId, false)
        end
        if groupInfo then
            self.m_identifyInfos.allIdentifyGroupInfos[groupId] = groupInfo
            for _, identifyId in pairs(groupInfo.identifyIds) do
                table.insert(self.m_identifyInfos.allIdentifyIds, identifyId)
            end
        end
    end
end

SnapshotCtrl._WrapIdentifyGroupInfo = HL.StaticMethod(HL.String, HL.Boolean).Return(HL.Table) << function(identifyGroupId, needDesc)
    local hasCfg, cfg = Tables.snapshotIdentifyGroupTable:TryGetValue(identifyGroupId)
    if not hasCfg then
        return nil
    end

    local groupInfo = {
        identifyGroupId = identifyGroupId,
        identifyIds = {},
        identifyInfos = {},
    }
    for _, identifyId in pairs(cfg.identifyIds) do
        table.insert(groupInfo.identifyIds, identifyId)
        local identifyInfo = {
            id = identifyId,
            matched = false,
        }
        if needDesc then
            identifyInfo.desc = snapshotSystem:GetIdentifyDesc(identifyId)
        end
        groupInfo.identifyInfos[identifyId] = identifyInfo
    end

    return groupInfo
end




SnapshotCtrl._InitUI = HL.Method() << function(self)
    self.m_isBasicSliderControllerInputInited = false

    self.view.closeBtn.onClick:AddListener(function(eventData)
        if DeviceInfo.usingKeyboard and eventData == nil and snapshotSystem:GetMoveSelectedSlotIndex() >= 0 then
            self:_DeselectMoveChar()
            return
        end
        self:_CloseSelf()
    end)
    self.view.shutterBtn.onClick:AddListener(function()
        self:_ClickShutter()
    end)
    self.view.personPerspectiveBtn.onClick:AddListener(function()
        local isFirstPersonMode = snapshotSystem.isFirstPersonMode
        if not isFirstPersonMode and self:_IsForbid(self.m_forbidRecords.firstPersonPerspective) then
            self:_ShowForbidToast(self.m_forbidRecords.firstPersonPerspective)
            return
        end
        self:_SwitchPersonPerspectiveMode(not isFirstPersonMode, true)
    end)
    self.view.resetPerspectiveBtn.onClick:AddListener(function()
        self:_ResetPerspective()
    end)
    self.view.openCharFormationBtn.onClick:AddListener(function()
        self:_OpenCharFormation()
    end)
    self.view.uiVisibleBtn.onClick:AddListener(function()
        self:_SwitchSnapshotUIVisible(false)
    end)
    self.view.quickMoveRotateCharBtn.onClick:AddListener(function()
        self:_OnClickQuickMoveRotateCharBtn()
    end)
    self.view.switchMoveModeTog.onValueChanged:AddListener(function(isOn)
        if self:_IsForbid(self.m_forbidRecords.switchMoveMode) then
            self:_ShowForbidToast(self.m_forbidRecords.switchMoveMode)
            return
        end
        self:_SwitchMoveMode(isOn, true)
        self.view.switchMoveModeAniWrapper:Play(isOn and "switchmove_to_left" or "switchmove_to_right")
    end)

    local focalLengthNode = self.view.focalLengthNode
    focalLengthNode.focalLengthSlider.minValue = self.view.config.MIN_FOCAL_LENGTH
    focalLengthNode.focalLengthSlider.maxValue = self.view.config.MAX_FOCAL_LENGTH
    focalLengthNode.focalLengthSlider.onValueChanged:AddListener(function(newValue)
        self:_ChangeFocalLength(newValue)
    end)


    focalLengthNode.addBtn.onPressStart:AddListener(function()

        if InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.Mouse1) then
            return
        end
        self.m_addFocalLengthCoroutine = self:_ClearCoroutine(self.m_addFocalLengthCoroutine)
        self.m_addFocalLengthCoroutine = self:_StartSliderChangeCoroutine(
            focalLengthNode.focalLengthSlider, 1, self.view.config.PRESS_CHANGE_FOCAL_LENGTH_SPEED)
    end)
    focalLengthNode.addBtn.onPressEnd:AddListener(function()
        self.m_addFocalLengthCoroutine = self:_ClearCoroutine(self.m_addFocalLengthCoroutine)
    end)

    focalLengthNode.minusBtn.onPressStart:AddListener(function()
        if InputManagerInst:GetKey(CS.Beyond.Input.KeyboardKeyCode.Mouse1) then
            return
        end
        self.m_minusFocalLengthCoroutine = self:_ClearCoroutine(self.m_minusFocalLengthCoroutine)
        self.m_minusFocalLengthCoroutine = self:_StartSliderChangeCoroutine(
            focalLengthNode.focalLengthSlider, -1, -self.view.config.PRESS_CHANGE_FOCAL_LENGTH_SPEED)
    end)
    focalLengthNode.minusBtn.onPressEnd:AddListener(function()
        self.m_minusFocalLengthCoroutine = self:_ClearCoroutine(self.m_minusFocalLengthCoroutine)
    end)

    if DeviceInfo.usingController then
        self:BindInputPlayerAction("snapshot_controller_cam_zoom_in", function()
            self.m_cameraCtrl:ZoomCamera(4)
            if self:IsInGamepadMoveRotateMode() then
                self.m_skipThisTimeQuickMoveCharChangePreAvatar = true
            end
        end)
        self:BindInputPlayerAction("snapshot_controller_cam_zoom_out", function()
            self.m_cameraCtrl:ZoomCamera(-4)
            if self:IsInGamepadMoveRotateMode() then
                self.m_skipThisTimeQuickMoveCharChangePreAvatar = true
            end
        end)
    end



    local quickNode = self.view.quickMoveRotateCharNode
    quickNode.exitBtn.onClick:AddListener(function()
        self:_ExitGamepadMoveRotateCharMode()
    end)
    self.m_quickAvatarCellCache = UIUtils.genCellCache(quickNode.avatarCell)
    quickNode.stateController:SetState("CloseQuickMoveRotate")


    if DeviceInfo.usingKeyboard then
        UIUtils.bindInputPlayerAction("snapshot_keyboard_start_rotate_char", function()
            self:_EnterKeyboardQuickRotateCharMode()
        end, self.view.inputGroup.groupId)
        UIUtils.bindInputPlayerAction("snapshot_keyboard_end_rotate_char", function()
            self:_ExitKeyboardQuickRotateCharMode()
        end, self.view.inputGroup.groupId)
    end




    self.view.menuFoldBtn.onClick:AddListener(function()
        self:_SwitchMenuContentExpand(not self.m_isMenuExpand)
    end)
    self.view.menuNodeNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        self.m_isMenuNodeFocused = isFocused
        if isFocused then
            self:_ChangeMenuTab(self.m_curSelectMenuIndex, true)

            if DeviceInfo.usingController then
                self.m_addFocalLengthCoroutine = self:_ClearCoroutine(self.m_addFocalLengthCoroutine)
                self.m_minusFocalLengthCoroutine = self:_ClearCoroutine(self.m_minusFocalLengthCoroutine)
                self.m_addApertureCoroutine = self:_ClearCoroutine(self.m_addApertureCoroutine)
                self.m_minusApertureCoroutine = self:_ClearCoroutine(self.m_minusApertureCoroutine)
            end
        else
            self:_SwitchMenuContentExpand(false)
        end
    end)

    local menuTabNode = self.view.menuTabNode
    self.m_menuTabCellList = {
        menuTabNode.menuTabBasicBtn,
        menuTabNode.menuTabFormationBtn,
        menuTabNode.menuTabActionBtn,
        menuTabNode.menuTabFilterBtn,
        menuTabNode.menuTabStickerBtn,
    }
    for i, tabBtnCell in pairs(self.m_menuTabCellList) do
        tabBtnCell.btn.onClick:AddListener(function()
            if self.m_isMenuExpand and self.m_curSelectMenuIndex == i then
                self:_SwitchMenuContentExpand(false)
            else
                self:_ChangeMenuTab(i, true)
            end
        end)
    end

    menuTabNode.menuTabActionBtn.redDot:InitRedDot("SnapshotActionTab")

    local preTabActionId = menuTabNode.keyHintPreTab.actionId
    local nextTabActionId = menuTabNode.keyHintNextTab.actionId
    UIUtils.bindInputPlayerAction(preTabActionId, function()
        local count = #self.m_menuTabCellList
        local newIndex = (self.m_curSelectMenuIndex + count - 2) % count + 1
        if newIndex ~= self.m_curSelectMenuIndex then
            AudioAdapter.PostEvent("Au_UI_Toggle_Tab_On")
            self:_ChangeMenuTab(newIndex, true)
        end
    end, self.view.menuInputGroup.groupId)
    UIUtils.bindInputPlayerAction(nextTabActionId, function()
        local count = #self.m_menuTabCellList
        local newIndex = self.m_curSelectMenuIndex % count + 1
        if newIndex ~= self.m_curSelectMenuIndex then
            AudioAdapter.PostEvent("Au_UI_Toggle_Tab_On")
            self:_ChangeMenuTab(newIndex, true)
        end
    end, self.view.menuInputGroup.groupId)
    if DeviceInfo.usingController then
        menuTabNode.keyHintPreTabRoot.gameObject:SetActive(false)
        menuTabNode.keyHintNextTabRoot.gameObject:SetActive(false)
    end



    local menuContentNode = self.view.menuContentNode
    self.m_menuContentCellList = {
        menuContentNode.menuBasicNode,
        menuContentNode.menuFormationNode,
        menuContentNode.menuActionNode,
        menuContentNode.menuFilterNode,
        menuContentNode.menuStickerNode,
    }
    self:_InitUIMenuContentChangeFunc()


    local basicNode = menuContentNode.menuBasicNode
    basicNode.showCharDropDown:ClearComponent()
    basicNode.showCharDropDown:Init(
        function(csIndex, option, _)

            option:SetText(Language[showCharConfig[LuaIndex(csIndex)].nameLuaKey])
        end,
        function(csIndex)

            self:_ChangeCharShowMode(LuaIndex(csIndex))
        end
    )
    basicNode.showCharDropDown.onIsNaviTargetChanged = function(isTarget)
        InputManagerInst:ToggleGroup(basicNode.showCharDropDown.groupId, isTarget)
    end
    basicNode.showCharDropDownForbidToastBtn.onClick:AddListener(function()
        self:_ShowForbidToast(self.m_forbidRecords.hideChar)
    end)
    local environmentDropDown = basicNode.environmentNode.environmentDropDown
    environmentDropDown:ClearComponent()
    environmentDropDown:Init(
        function(csIndex, option, isSelected)
            self:_RefreshEnvironmentOptionCell(csIndex, option, isSelected)
        end,
        function(csIndex, isOn)
            self:_OnEnvironmentDropDownValueChanged(csIndex, isOn)
        end
    )
    environmentDropDown.onIsNaviTargetChanged = function(isTarget)
        InputManagerInst:ToggleGroup(environmentDropDown.groupId, isTarget)
    end
    basicNode.gridLinesTog.onValueChanged:AddListener(function(isOn)
        self:_SwitchShowGridLines(isOn)
    end)


    basicNode.apertureSlider.minValue = self.view.config.MIN_APERTURE
    basicNode.apertureSlider.maxValue = self.view.config.MAX_APERTURE
    basicNode.apertureSlider.onValueChanged:AddListener(function(newValue)
        self:_ChangeAperture(newValue)
    end)
    basicNode.apertureSlider.onEndDragSlider:AddListener(function()
        if self:_IsForbid(self.m_forbidRecords.aperture) then
            self:_ShowForbidToast(self.m_forbidRecords.aperture)
        end
    end)

    basicNode.apertureSlider.onIsNaviTargetChanged = function(isTarget)
        self.m_addApertureCoroutine = self:_ClearCoroutine(self.m_addApertureCoroutine)
        self.m_minusApertureCoroutine = self:_ClearCoroutine(self.m_minusApertureCoroutine)
        InputManagerInst:ToggleGroup(basicNode.apertureSlider.groupId, isTarget)
        basicNode.keyHintApertureSliderLeft.gameObject:SetActive(isTarget)
        basicNode.keyHintApertureSliderRight.gameObject:SetActive(isTarget)
    end
    basicNode.apertureSliderForbidToastBtn.onClick:AddListener(function()
        self:_ShowForbidToast(self.m_forbidRecords.aperture)
    end)



    local manualFocusDropDownNode = basicNode.manualFocusDropDownNode
    local manualFocusSliderNode = basicNode.manualFocusSliderNode

    manualFocusDropDownNode.manualFocusDropDown:ClearComponent()
    manualFocusDropDownNode.manualFocusDropDown:Init(
        function(csIndex, option, _)

            if csIndex == 0 then
                option:SetText(Language.LUA_SNAPSHOT_MANUAL_FOCUS)
            else
                local slot = GameInstance.player.squadManager.curSquad.slots[csIndex - 1]
                if slot then
                    local charName = Tables.characterTable[slot.charId].name
                    option:SetText(charName)
                end
            end
        end,
        function(csIndex)
            self:_OnManualFocusDropDownChanged(LuaIndex(csIndex))
        end
    )
    manualFocusDropDownNode.manualFocusDropDown.onIsNaviTargetChanged = function(isTarget)
        InputManagerInst:ToggleGroup(manualFocusDropDownNode.manualFocusDropDown.groupId, isTarget)
    end


    manualFocusSliderNode.manualFocusSlider.minValue = 0.1
    manualFocusSliderNode.manualFocusSlider.maxValue = self.view.config.DEFAULT_AUTO_FOCUS_DISTANCE
    manualFocusSliderNode.manualFocusSlider.onValueChanged:AddListener(function(newValue)
        self:_OnManualFocusSliderChanged(newValue)
    end)
    manualFocusSliderNode.manualFocusSlider.onEndDragSlider:AddListener(function()
        if self:_IsForbid(self.m_forbidRecords.manualFocus) then
            self:_ShowForbidToast(self.m_forbidRecords.manualFocus)
        end
    end)
    manualFocusSliderNode.manualFocusSliderForbidToastBtn.onClick:AddListener(function()
        self:_ShowForbidToast(self.m_forbidRecords.manualFocus)
    end)

    manualFocusSliderNode.manualFocusSlider.onIsNaviTargetChanged = function(isTarget)
        self.m_addManualFocusCoroutine = self:_ClearCoroutine(self.m_addManualFocusCoroutine)
        self.m_minusManualFocusCoroutine = self:_ClearCoroutine(self.m_minusManualFocusCoroutine)
        InputManagerInst:ToggleGroup(manualFocusSliderNode.manualFocusSlider.groupId, isTarget)
        manualFocusSliderNode.keyHintManualFocusSliderLeft.gameObject:SetActive(isTarget)
        manualFocusSliderNode.keyHintManualFocusSliderRight.gameObject:SetActive(isTarget)
    end




    local yAxisRotSliderNode = basicNode.yAxisRotSliderNode

    yAxisRotSliderNode.yAxisRotSlider.minValue = 0
    yAxisRotSliderNode.yAxisRotSlider.maxValue = 360

    yAxisRotSliderNode.yAxisRotSlider.onValueChanged:AddListener(function(newValue)
        self:_OnYAxisRotSliderChanged(newValue)
    end)

    yAxisRotSliderNode.yAxisRotSlider.onIsNaviTargetChanged = function(isTarget)
        self.m_addYAxisRotCoroutine = self:_ClearCoroutine(self.m_addYAxisRotCoroutine)
        self.m_minusYAxisRotCoroutine = self:_ClearCoroutine(self.m_minusYAxisRotCoroutine)
        InputManagerInst:ToggleGroup(yAxisRotSliderNode.yAxisRotSlider.groupId, isTarget)
        yAxisRotSliderNode.keyHintYAxisRotSliderLeft.gameObject:SetActive(isTarget)
        yAxisRotSliderNode.keyHintYAxisRotSliderRight.gameObject:SetActive(isTarget)
    end





    local formationNode = menuContentNode.menuFormationNode
    formationNode.gameObject:SetActive(false)
    formationNode.formationDropDown.interactable = true
    formationNode.formationDropDownForbidToastBtn.gameObject:SetActive(false)
    formationNode.formationDropDownForbidToastBtn.onClick:AddListener(function()
        self:_ShowForbidToast(self.m_forbidRecords.switchFormation)
    end)
    formationNode.formationDropDown:ClearComponent()
    formationNode.formationDropDown:Init(
        function(csIndex, option, _)
            local name
            if csIndex == FORMATION_DROPDOWN_INDEX_NONE then
                name = Language.LUA_SNAPSHOT_FORMATION_NONE
            elseif csIndex == FORMATION_DROPDOWN_INDEX_CUSTOM then
                name = Language.LUA_SNAPSHOT_FORMATION_CUSTOM
            else
                local textId = formationManager.formationUIData[csIndex - FORMATION_DROPDOWN_PRESET_OFFSET].Item2
                local _, result = I18nUtils.TryGetText(textId)
                name = result
            end
            option:SetText(name)
        end,
        function(csIndex)
            self:_ChangeTeamFormation(self:_FormationDropdownIndexToTeamFormationIndex(csIndex))
        end
    )
    formationNode.formationDropDown.onIsNaviTargetChanged = function(isTarget)
        InputManagerInst:ToggleGroup(formationNode.formationDropDown.groupId, isTarget)
    end
    self.m_formationAvatarCellCache = UIUtils.genCellCache(formationNode.avatarCell)
    formationNode.formationDropDown.customNaviTargetInDirFunc = function(dir)
        if dir ~= Unity.UI.NaviDirection.Down then
            return nil
        end
        local squadManager = GameInstance.player.squadManager
        local slot = squadManager:GetMemberIndex(GameInstance.playerController.mainCharacter)
        if slot < 0 or not self:_IsFormationAvatarSelectable(slot) then
            slot = self:_GetFirstSelectableSlot()
        end
        if slot >= 0 then
            local cell = self.m_formationAvatarCellCache:Get(LuaIndex(slot))
            if cell ~= nil then
                return cell.btn
            end
        end
        return nil
    end



    local actionNode = menuContentNode.menuActionNode
    actionNode.gameObject:SetActive(false)
    self.m_getActionCellFunc = UIUtils.genCachedCellFunction(actionNode.actionList)
    actionNode.actionList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getActionCellFunc(obj)
        self:_RefreshActionCell(cell, LuaIndex(csIndex))
    end)
    actionNode.redDotScrollRect.getRedDotStateAt = function(csIndex)
        return self:_GetActionRedDotStateAt(csIndex)
    end
    self.m_actionAvatarCellCache = UIUtils.genCellCache(actionNode.avatarList.avatarCell)

    actionNode.playBtnNode.playBtn.onClick:AddListener(function()
        self:_OnClickActionPlayBtn()
    end)

    actionNode.jumpBtnNode.jumpBtn.onClick:AddListener(function()
        self:_OnClickActionJumpBtn()
    end)

    self.view.videoNode.closeBtn.onClick:AddListener(function()
        self:_CloseActionVideo()
    end)

    actionNode.controllerPlayVideoBtn.onClick:AddListener(function()
        self:onClickControllerPlayVideoBtn()
    end)

    local avatarListNode = actionNode.avatarList
    local preAvatarActionId = avatarListNode.preAvatarKeyHint.actionId
    local nextAvatarActionId = avatarListNode.nextAvatarKeyHint.actionId
    self.m_actionPreAvatarBindingId = UIUtils.bindInputPlayerAction(preAvatarActionId, function()
        local count = #self.m_squadCharList
        if count <= 0 then
            return
        end
        local newIndex = (self.m_curSelectAvatarIndex + count - 2) % count + 1
        self:_OnSelectAvatarChar(newIndex)
        self.view.menuContentNode.menuActionNode.actionList:ScrollToIndex(CSIndex(self.m_curSelectActionIndex), true)
        local cell = self.m_getActionCellFunc(self.m_curSelectActionIndex)
        if cell then
            self:SetNaviTarget(cell.actionBtn)
        end
    end, self.view.menuInputGroup.groupId)
    self.m_actionNextAvatarBindingId = UIUtils.bindInputPlayerAction(nextAvatarActionId, function()
        local count = #self.m_squadCharList
        if count <= 0 then
            return
        end
        local newIndex = self.m_curSelectAvatarIndex % count + 1
        self:_OnSelectAvatarChar(newIndex)
        self.view.menuContentNode.menuActionNode.actionList:ScrollToIndex(CSIndex(self.m_curSelectActionIndex), true)
        local cell = self.m_getActionCellFunc(self.m_curSelectActionIndex)
        if cell then
            self:SetNaviTarget(cell.actionBtn)
        end
    end, self.view.menuInputGroup.groupId)
    self:_ToggleActionAvatarBindings(false)



    local filterNode = menuContentNode.menuFilterNode
    filterNode.gameObject:SetActive(false)
    self.m_getFilterCellFunc = UIUtils.genCachedCellFunction(filterNode.menuFilterList)
    filterNode.menuFilterList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getFilterCellFunc(obj)
        self:_RefreshFilterCell(cell, LuaIndex(csIndex))
    end)
    filterNode.menuDownInfoNode.jumpBtnNode.jumpBtn.onClick:AddListener(function()
        self:_OnClickFilterJumpBtn()
    end)



    local stickerNode = menuContentNode.menuStickerNode
    stickerNode.gameObject:SetActive(false)
    self.m_getStickerCellFunc = UIUtils.genCachedCellFunction(stickerNode.stickerList)
    stickerNode.stickerList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getStickerCellFunc(obj)
        self:_RefreshStickerCell(cell, LuaIndex(csIndex))
    end)
    stickerNode.menuDownInfoNode.jumpBtnNode.jumpBtn.onClick:AddListener(function()
        self:_OnClickStickerJumpBtn()
    end)

    local stickerTouchPlate = self.view.stickerTouchPlate
    stickerTouchPlate.onDrag:AddListener(function(eventData)
        self:_OnDragSticker(eventData)
    end)
    stickerTouchPlate.onDragBegin:AddListener(function(_)
        if not self.m_inStickerEditMode then
            return
        end
        if self.m_isShowSnapshotUI then
            self.m_hideSnapshotUIBySticker = true
            self:_SwitchSnapshotUIVisible(false)
        end
    end)
    stickerTouchPlate.onDragEnd:AddListener(function(_)
        if not self.m_inStickerEditMode then
            return
        end
        if self.m_hideSnapshotUIBySticker then
            self.m_hideSnapshotUIBySticker = false
            self:_SwitchSnapshotUIVisible(true)
        end
    end)

    local stickerTouchPlateNoEdit = self.view.stickerTouchPlateNoEdit
    stickerTouchPlateNoEdit.onClick:AddListener(function()
        if not self.m_inStickerEditMode then
            self:_EnableStickerEditMode(true)
        end
    end)
    stickerTouchPlateNoEdit.onDrag:AddListener(function(eventData)
        self.m_onDrag(eventData)
    end)






    self.m_indicatorCellCache = UIUtils.genCellCache(self.view.indicatorCell)
    self.m_identifyGoalCellCache = UIUtils.genCellCache(self.view.identifyTaskNode.identifyGoalCell)



    self.view.tipNode.gameObject:SetActive(false)

end

SnapshotCtrl._InitUIMenuContentChangeFunc = HL.Method() << function(self)
    self.m_onChangeContentFuncList = {
        {

            entryFunc = function()
                self.view.menuContentNode.menuBasicNode.apertureSlider.interactable = false
                self.view.menuContentNode.menuBasicNode.manualFocusSliderNode.manualFocusSlider.interactable = false

                self.view.menuContentNode.menuBasicNode.yAxisRotSliderNode.yAxisRotSlider.interactable = false
                self:_SetBasicSliderKeyHintsActive(false)
            end,
            naviFunc = function()
                self:SetNaviTarget(self.view.menuContentNode.menuBasicNode.showCharDropDown)

                if not self:_IsForbid(self.m_forbidRecords.aperture) then
                    self.view.menuContentNode.menuBasicNode.apertureSlider.interactable = true
                end

                if not self:_IsForbid(self.m_forbidRecords.manualFocus) and self.m_isManualFocus then
                    self.view.menuContentNode.menuBasicNode.manualFocusSliderNode.manualFocusSlider.interactable = true
                else
                    self.view.menuContentNode.menuBasicNode.manualFocusSliderNode.manualFocusSlider.interactable = false
                end
                InputManagerInst:ToggleGroup(self.view.menuContentNode.menuBasicNode.apertureSlider.groupId, false)
                InputManagerInst:ToggleGroup(self.view.menuContentNode.menuBasicNode.manualFocusSliderNode.manualFocusSlider.groupId, false)

                self.view.menuContentNode.menuBasicNode.yAxisRotSliderNode.yAxisRotSlider.interactable = true
                InputManagerInst:ToggleGroup(
                    self.view.menuContentNode.menuBasicNode.yAxisRotSliderNode.yAxisRotSlider.groupId, false)
                self:_SetBasicSliderKeyHintsActive(false)
            end,
            leaveFunc = function()
                self:_CloseEnvironmentDropDown()
            end,
        },
        {

            entryFunc = function()
                if not self.m_squadCharList or #self.m_squadCharList == 0 then
                    self:_UpdateSquadCharList()
                end
                self:_RefreshFormationAvatarList()
            end,
            naviFunc = function()
                if self.m_needIgnoreMenuFormationNavi then
                    self.m_needIgnoreMenuFormationNavi = false
                else
                    self:SetNaviTarget(self.view.menuContentNode.menuFormationNode.formationDropDown)
                end
            end,
            leaveFunc = nil,
        },
        {

            entryFunc = function()
                self:_ToggleActionAvatarBindings(true)
                if not self.m_isInitRefreshActionUI then
                    self.m_isInitRefreshActionUI = true
                    self:_UpdateSquadCharList()
                    self:_RefreshActionAvatarList()
                    local defaultIndex = self:_GetMoveSelectedSquadAvatarIndex()
                    if defaultIndex <= 0 then
                        defaultIndex = self:_GetMainControlSquadAvatarIndex()
                    end
                    self:_OnSelectAvatarChar(defaultIndex)
                end
            end,
            naviFunc = function()
                local focusIndex = self.m_curSelectActionIndex
                if focusIndex > 0 then
                    local actionNode = self.view.menuContentNode.menuActionNode
                    local obj = actionNode.actionList:Get(CSIndex(focusIndex))
                    local cell = self.m_getActionCellFunc(obj)
                    if cell then
                        self:SetNaviTarget(cell.actionBtn)
                    end
                end
            end,
            leaveFunc = function()
                self:_ToggleActionAvatarBindings(false)
                self:_OnLeaveActionTab()
            end,
        },
        {

            entryFunc = function()
                self:_RefreshFilterInfoArea()
            end,
            naviFunc = function()
                local focusIndex = self.m_curSelectFilterIndex > 0 and self.m_curSelectFilterIndex or self.m_curUsedFilterIndex
                if self.m_resumeMenuControllerFocusType == "filter" and self.m_resumeMenuControllerFocusIndex > 0 then
                    focusIndex = self.m_resumeMenuControllerFocusIndex
                    self.m_resumeMenuControllerFocusType = nil
                    self.m_resumeMenuControllerFocusIndex = 0
                end
                local obj = self.view.menuContentNode.menuFilterNode.menuFilterList:Get(CSIndex(focusIndex))
                local cell = self.m_getFilterCellFunc(obj)
                if cell then
                    self:SetNaviTarget(cell.btn)
                end
            end,
            leaveFunc = function()
            end,
        },
        {

            entryFunc = function()
                if not self.m_isInitRefreshStickerUI then
                    self.m_isInitRefreshStickerUI = true
                    local stickerNode = self.view.menuContentNode.menuStickerNode
                    stickerNode.stickerList:UpdateCount(#self.m_stickerInfos, true)
                end
                self:_RefreshStickerInfoArea()
            end,
            naviFunc = function()
                local focusIndex = self.m_curSelectStickerIndex > 0 and self.m_curSelectStickerIndex or self.m_curUsedStickerIndex
                if self.m_resumeMenuControllerFocusType == "sticker" and self.m_resumeMenuControllerFocusIndex > 0 then
                    focusIndex = self.m_resumeMenuControllerFocusIndex
                    self.m_resumeMenuControllerFocusType = nil
                    self.m_resumeMenuControllerFocusIndex = 0
                end
                local obj = self.view.menuContentNode.menuStickerNode.stickerList:Get(CSIndex(focusIndex))
                local cell = self.m_getStickerCellFunc(obj)
                if cell then
                    self:SetNaviTarget(cell.btn)
                end
            end,
            leaveFunc = function()
                if self.m_inStickerEditMode then
                    self:_EnableStickerEditMode(false)
                end
            end,
        },
    }
end

SnapshotCtrl._RefreshAllUI = HL.Method() << function(self)

    self.view.switchMoveModeTog:SetIsOnWithoutNotify(snapshotSystem.isCameraMoveMode)
    self.view.shutterBtnHighLight.gameObject:SetActive(false)

    local menuContentNode = self.view.menuContentNode
    self.m_curSelectMenuIndex = 1

    local basicNode = menuContentNode.menuBasicNode
    self:_RefreshEnvironmentDropDown()
    basicNode.gridLinesTog:SetIsOnWithoutNotify(self.m_isShowGridLines)
    basicNode.showCharDropDown:Refresh(#showCharConfig, 0, false)
    basicNode.apertureSlider:SetValueWithoutNotify(self.view.config.DEFAULT_APERTURE)
    self:_ChangeAperture(self.view.config.DEFAULT_APERTURE)

    basicNode.yAxisRotSliderNode.yAxisRotSlider:SetValueWithoutNotify(0)
    basicNode.yAxisRotSliderNode.yAxisRotTxt.text = string.format(Language.LUA_SNAPSHOT_Y_AXIS_ROT_FORMAT, 0)

    self:_RefreshManualFocusDropDown()

    local squadManager = GameInstance.player.squadManager
    local mainCharSlotIndex = squadManager:GetMemberIndex(GameInstance.playerController.mainCharacter)
    local defaultFocusSelectedIndex = mainCharSlotIndex + 1
    if defaultFocusSelectedIndex < 0 then
        defaultFocusSelectedIndex = 0
    end
    basicNode.manualFocusDropDownNode.manualFocusDropDown:SetSelected(defaultFocusSelectedIndex)
    basicNode.manualFocusSliderNode.manualFocusSlider:SetValueWithoutNotify(
        self.view.config.DEFAULT_AUTO_FOCUS_DISTANCE)



    self:_OnManualFocusDropDownChanged(LuaIndex(defaultFocusSelectedIndex))

    local formationNode = menuContentNode.menuFormationNode
    formationNode.formationDropDown:Refresh(formationManager.formationUIData.Count + FORMATION_DROPDOWN_PRESET_OFFSET,
        FORMATION_DROPDOWN_INDEX_NONE, false)

    local filterNode = menuContentNode.menuFilterNode
    filterNode.menuFilterList:UpdateCount(#self.m_filterInfos, true)


    self:_ChangeMenuTab(1, false, true)
    self:_SwitchMenuContentExpand(false, true)


    self:_RefreshIdentifyTask()

    self.m_indicatorCellCache:Refresh(0)
end

SnapshotCtrl._RefreshFilterCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local info = self.m_filterInfos[luaIndex]
    cell.useStateCtrl:SetState(self.m_curUsedFilterIndex == luaIndex and "Used" or "Unused")
    cell.selectStateCtrl:SetState(self.m_curSelectFilterIndex == luaIndex and "Select" or "Unselect")
    cell.editorStateCtrl:SetState("NoEditor")
    cell.emptyStateCtrl:SetState(info.isEmpty and "Empty" or "Normal")
    cell.btn.onClick:RemoveAllListeners()
    cell.btn.onLongPress:RemoveAllListeners()
    cell.showTipsBtn.onClick:RemoveAllListeners()
    if info.isUnlock then
        cell.lockStateCtrl:SetState("Unlock")
    else
        cell.lockStateCtrl:SetState("Lock")
    end
    cell.btn.onClick:AddListener(function()
        local filterInfo = self.m_filterInfos[luaIndex]
        self:_SelectFilterCell(luaIndex, cell)
        if not filterInfo.isUnlock then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_SNAPSHOT_FILTER_UNLOCK_TOAST)
            return
        end

        local oldIndex = self.m_curUsedFilterIndex
        if oldIndex == luaIndex then
            return
        end
        if oldIndex > 0 then
            local oldObj = self.view.menuContentNode.menuFilterNode.menuFilterList:Get(CSIndex(oldIndex))
            local oldCell = self.m_getFilterCellFunc(oldObj)
            if oldCell then
                oldCell.useStateCtrl:SetState("Unused")
            end
        end
        cell.useStateCtrl:SetState("Used")
        self:_ChangeFilter(luaIndex)
    end)
    if not string.isEmpty(info.itemId) then
        cell.btn.onLongPress:AddListener(function()
            Notify(MessageConst.SHOW_ITEM_TIPS, {
                transform = cell.transform,
                safeArea = cell.transform,
                posType = UIConst.UI_TIPS_POS_TYPE.RightDown,
                itemId = info.itemId,
            })
        end)
        cell.showTipsBtn.gameObject:SetActive(false)
        if DeviceInfo.usingController then
            cell.showTipsBtn.onClick:AddListener(function()
                Notify(MessageConst.SHOW_ITEM_TIPS, {
                    transform = cell.transform,
                    posType = UIConst.UI_TIPS_POS_TYPE.RightDown,
                    itemId = info.itemId,
                })
            end)
            cell.btn.onIsNaviTargetChanged = function(isTarget)
                cell.showTipsBtn.gameObject:SetActive(isTarget)
                if isTarget then
                    self:_SelectFilterCell(luaIndex, cell)
                end
            end
        end
    end
    if info.isEmpty and DeviceInfo.usingController then
        cell.btn.onIsNaviTargetChanged = function(isTarget)
            if isTarget then
                self.m_curSelectFilterIndex = 1
                self:_RefreshFilterInfoArea()
            end
        end
    end

    if not info.isEmpty then
        cell.iconImg:LoadSprite(UIConst.UI_SPRITE_SNAPSHOT_FILTER, info.icon)
    end
end

SnapshotCtrl._RefreshFilterCellSelectState = HL.Method(HL.Number, HL.Opt(HL.Any)) << function(self, luaIndex, cell)
    if luaIndex <= 0 then
        return
    end
    if not cell and not self.m_getFilterCellFunc then
        return
    end
    if not cell then
        local obj = self.view.menuContentNode.menuFilterNode.menuFilterList:Get(CSIndex(luaIndex))
        if obj then
            cell = self.m_getFilterCellFunc(obj)
        end
    end
    if cell then
        cell.selectStateCtrl:SetState(self.m_curSelectFilterIndex == luaIndex and "Select" or "Unselect")
    end
end

SnapshotCtrl._SelectFilterCell = HL.Method(HL.Number, HL.Opt(HL.Any)) << function(self, luaIndex, cell)
    local oldIndex = self.m_curSelectFilterIndex
    self.m_curSelectFilterIndex = luaIndex
    if oldIndex ~= luaIndex then
        self:_RefreshFilterCellSelectState(oldIndex)
    end
    self:_RefreshFilterCellSelectState(luaIndex, cell)
    self:_RefreshFilterInfoArea()
end

SnapshotCtrl._RefreshStickerCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local info = self.m_stickerInfos[luaIndex]
    local isUsed = self.m_curUsedStickerIndex == luaIndex
    cell.useStateCtrl:SetState(isUsed and "Used" or "Unused")
    cell.selectStateCtrl:SetState(self.m_curSelectStickerIndex == luaIndex and "Select" or "Unselect")
    local isEditor = (not info.isEmpty) and isUsed
    cell.editorStateCtrl:SetState(isEditor and "Editor" or "NoEditor")
    cell.emptyStateCtrl:SetState(info.isEmpty and "Empty" or "Normal")
    cell.btn.onClick:RemoveAllListeners()
    cell.btn.onLongPress:RemoveAllListeners()
    cell.showTipsBtn.onClick:RemoveAllListeners()
    if info.isUnlock then
        cell.lockStateCtrl:SetState("Unlock")
        InputManagerInst:SetBindingText(cell.btn.hoverConfirmBindingId, Language.LUA_SNAPSHOT_STICKER_SELECT_HINT_TEXT)
    else
        cell.lockStateCtrl:SetState("Lock")
    end
    cell.btn.onClick:AddListener(function()
        self:_SelectStickerCell(luaIndex, cell)
        if info.isUnlock then
            self:_OnClickMenuStickerCell(luaIndex, cell)
        else
            Notify(MessageConst.SHOW_TOAST, Language.LUA_SNAPSHOT_STICKER_UNLOCK_TOAST)
        end
    end)
    if not string.isEmpty(info.itemId) then
        cell.btn.onLongPress:AddListener(function()
            Notify(MessageConst.SHOW_ITEM_TIPS, {
                transform = cell.transform,
                safeArea = cell.transform,
                posType = UIConst.UI_TIPS_POS_TYPE.RightDown,
                itemId = info.itemId,
            })
        end)
        cell.showTipsBtn.gameObject:SetActive(false)
        if DeviceInfo.usingController then
            cell.showTipsBtn.onClick:AddListener(function()
                Notify(MessageConst.SHOW_ITEM_TIPS, {
                    transform = cell.transform,
                    posType = UIConst.UI_TIPS_POS_TYPE.RightDown,
                    itemId = info.itemId,
                })
            end)
            cell.btn.onIsNaviTargetChanged = function(isTarget)
                cell.showTipsBtn.gameObject:SetActive(isTarget)
                if isTarget then
                    self:_SelectStickerCell(luaIndex, cell)
                end
            end
        end
    end
    if info.isEmpty and DeviceInfo.usingController then
        cell.btn.onIsNaviTargetChanged = function(isTarget)
            if isTarget then
                self.m_curSelectStickerIndex = 1
                self:_RefreshStickerInfoArea()
            end
        end
    end

    if not info.isEmpty then
        cell.iconImg:LoadSprite(UIConst.UI_SPRITE_SNAPSHOT_STICKER, info.icon)
    end
end

SnapshotCtrl._RefreshStickerCellSelectState = HL.Method(HL.Number, HL.Opt(HL.Any)) << function(self, luaIndex, cell)
    if luaIndex <= 0 then
        return
    end
    if not cell and not self.m_getStickerCellFunc then
        return
    end
    if not cell then
        local obj = self.view.menuContentNode.menuStickerNode.stickerList:Get(CSIndex(luaIndex))
        if obj then
            cell = self.m_getStickerCellFunc(obj)
        end
    end
    if cell then
        cell.selectStateCtrl:SetState(self.m_curSelectStickerIndex == luaIndex and "Select" or "Unselect")
    end
end

SnapshotCtrl._SelectStickerCell = HL.Method(HL.Number, HL.Opt(HL.Any)) << function(self, luaIndex, cell)
    local oldIndex = self.m_curSelectStickerIndex
    self.m_curSelectStickerIndex = luaIndex
    if oldIndex ~= luaIndex then
        self:_RefreshStickerCellSelectState(oldIndex)
    end
    self:_RefreshStickerCellSelectState(luaIndex, cell)
    self:_RefreshStickerInfoArea()
end

SnapshotCtrl._RefreshIdentifyTask = HL.Method() << function(self)
    local count = 0
    if self.m_identifyInfos.traceIdentifyGroupInfo then
        count = #self.m_identifyInfos.traceIdentifyGroupInfo.identifyIds
    end
    self.m_identifyGoalCellCache:Refresh(
        count,
        function(cell, luaIndex)
            local id = self.m_identifyInfos.traceIdentifyGroupInfo.identifyIds[luaIndex]
            local info = self.m_identifyInfos.traceIdentifyGroupInfo.identifyInfos[id]
            cell.goalTxt.text = info.desc
            cell.animation:Play("tasktrackhud_celldefault")
        end
    )
end

SnapshotCtrl._RefreshTaskNodeVisible = HL.Method() << function(self)
    self.view.identifyTaskNode.gameObject:SetActive(self.m_hasTraceIdentify and not self.m_isMenuExpand)
end

SnapshotCtrl._SetBasicSliderKeyHintsActive = HL.Method(HL.Boolean) << function(self, active)
    local basicNode = self.view.menuContentNode.menuBasicNode
    basicNode.keyHintApertureSliderLeft.gameObject:SetActive(active)
    basicNode.keyHintApertureSliderRight.gameObject:SetActive(active)
    basicNode.manualFocusSliderNode.keyHintManualFocusSliderLeft.gameObject:SetActive(active)
    basicNode.manualFocusSliderNode.keyHintManualFocusSliderRight.gameObject:SetActive(active)
    basicNode.yAxisRotSliderNode.keyHintYAxisRotSliderLeft.gameObject:SetActive(active)
    basicNode.yAxisRotSliderNode.keyHintYAxisRotSliderRight.gameObject:SetActive(active)
end






SnapshotCtrl._CloseSelf = HL.Method(HL.Opt(HL.Boolean, HL.Boolean)) << function(self, filterCommonShare, closeFast)
    if self.m_isInCloseProcess or not PhaseManager:IsOpenAndValid(PhaseId.Snapshot) or not PhaseManager:CanPopPhase(PhaseId.Snapshot) then
        return
    end
    self.m_isInCloseProcess = true
    self:_CloseActionVideo()
    if not filterCommonShare then
        UIManager:Close(PanelId.CommonShare)
    end
    UIManager:Close(PanelId.CommonPopUp)
    if PhaseManager:GetTopPhaseId() == PHASE_ID and not closeFast then
        PhaseManager:PopPhase(PHASE_ID, function()
            snapshotSystem:SetForcePlayRadio(false)
        end)
    else
        PhaseManager:ExitPhaseFast(PHASE_ID)
        snapshotSystem:SetForcePlayRadio(false)
    end
    AudioAdapter.PostEvent("Au_UI_Popup_PhotoPanel_Close")
end

SnapshotCtrl._ClickShutter = HL.Method() << function(self)
    EventLogManagerInst:GameEvent_Snapshot(2)
    if self.m_inStickerEditMode then
        self:_EnableStickerEditMode(false)
    end
    self.m_isInCapture = true
    self.view.captureInvisibleRoot.gameObject:SetActive(false)
    self.view.touchPlate.gameObject:SetActive(false)
    local atLeastOneSuccess = false


    if #self.m_identifyInfos.allIdentifyIds > 0 then
        local successIds = snapshotSystem:ExecuteIdentify(self.m_identifyInfos.allIdentifyIds)

        local tempSet = {}
        for _, identifyId in cs_pairs(successIds) do
            tempSet[identifyId] = true
        end
        for _, groupInfo in pairs(self.m_identifyInfos.allIdentifyGroupInfos) do
            local curMatchCount = 0
            for _, identifyId in pairs(groupInfo.identifyIds) do
                if tempSet[identifyId] then
                    groupInfo.identifyInfos[identifyId].matched = true
                    curMatchCount = curMatchCount + 1
                end
                if groupInfo.identifyGroupId == self.m_eventLogInfo.identifyGroupId then
                    self.m_eventLogInfo.traceIdentifyProgress[identifyId] = not not tempSet[identifyId]
                end
            end
            local isAllComplete = curMatchCount == #groupInfo.identifyIds
            if isAllComplete then

                snapshotSystem:NotifyIdentifyGroupSuccess(groupInfo.identifyGroupId)
                atLeastOneSuccess = true
            end
            if groupInfo.identifyGroupId == self.m_eventLogInfo.identifyGroupId then
                self.m_eventLogInfo.traceIdentifySuccess = isAllComplete
            end
        end
    end

    if self.m_eventLogInfo.isFromActivity then
        EventLogManagerInst:GameEvent_SnapshotActivityShot(
            self.m_eventLogInfo.activityId,
            self.m_eventLogInfo.stageId,
            self.m_eventLogInfo.isFromInteractive and 1 or 2,
            self.m_eventLogInfo.traceIdentifyProgress,
            self.m_eventLogInfo.traceIdentifySuccess and 1 or 0
        )
    end

    local curShowSticker = self.view.stickerImg.gameObject.activeSelf
    self.m_phase:SetForbidJoystick(true, "Shutter")
    Notify(MessageConst.HIDE_ITEM_TIPS)
    self:_SetMoveSelectUIVisible(false, false)

    local needCloseSelfFast = atLeastOneSuccess or self.m_cinematicInQueueWaitCloseSnapshot
    Notify(MessageConst.SHOW_COMMON_SHARE_PANEL, self:_BuildCommonShareArg({
        type = "PhotoShot",
        aperture = self.view.menuContentNode.menuBasicNode.apertureValueTxt.text,
        focus = self.view.focalLengthNode.sliderValueTxt.text,
        showPlayerInfo = true,
        success = atLeastOneSuccess,
        isCloseFast = needCloseSelfFast,
    }, curShowSticker, needCloseSelfFast))
    GameInstance.player.statisticValueSystem:IncrementClientStatisticValueByType(GEnums.StatType.CltTakePhoto)
end

SnapshotCtrl._BuildCommonShareArg = HL.Method(HL.Table, HL.Opt(HL.Boolean, HL.Boolean)).Return(HL.Table) << function(self, commonShareArg, curShowSticker, needCloseSelfFast)
    commonShareArg.snapshotCurShowSticker = curShowSticker ~= nil and curShowSticker or commonShareArg.snapshotCurShowSticker == true
    commonShareArg.snapshotNeedCloseSelfFast = needCloseSelfFast ~= nil and needCloseSelfFast or commonShareArg.snapshotNeedCloseSelfFast == true
    local shouldRestoreSticker = commonShareArg.snapshotCurShowSticker == true
    local shouldCloseSelfFast = commonShareArg.snapshotNeedCloseSelfFast == true
    commonShareArg.onCaptureEnd = function()
        if not UIManager:IsOpen(PANEL_ID) or not PhaseManager:IsOpenAndValid(PHASE_ID) then
            return
        end
        if shouldRestoreSticker and not IsNull(self.view.stickerImg) then
            self.view.stickerImg.gameObject:SetActive(false)
        end
    end
    commonShareArg.onClose = function()
        if not UIManager:IsOpen(PANEL_ID) or not PhaseManager:IsOpenAndValid(PHASE_ID) then
            return
        end
        self.m_isInCapture = false
        if shouldRestoreSticker and not IsNull(self.view.stickerImg) then
            self.view.stickerImg.gameObject:SetActive(true)
        end
        if not IsNull(self.view.touchPlate) then
            self.view.touchPlate.gameObject:SetActive(true)
        end
        if not IsNull(self.view.captureInvisibleRoot) then
            self.view.captureInvisibleRoot.gameObject:SetActive(true)
        end
        self.m_phase:SetForbidJoystick(false, "Shutter")
        if shouldCloseSelfFast then
            self:_CloseSelf(true, true)
            return
        end
        if snapshotSystem:GetMoveSelectedSlotIndex() >= 0 then
            self:_SetMoveSelectUIVisible(true, false)
        end
    end
    return commonShareArg
end

SnapshotCtrl._SwitchPersonPerspectiveMode = HL.Method(HL.Boolean, HL.Boolean) << function(self, isFirstPersonMode, showTip)
    if showTip then
        Notify(MessageConst.SHOW_TOAST, isFirstPersonMode
            and Language.LUA_SNAPSHOT_SWITCH_FIRST_PERSON_MODE
            or Language.LUA_SNAPSHOT_SWITCH_THIRD_PERSON_MODE
        )
    end
    snapshotSystem:SetFirstPersonMode(isFirstPersonMode)
    if isFirstPersonMode then
        GameInstance.playerController:UpdateMoveCommand(Vector2.zero)
    end

    self:_SetForbid(self.m_forbidRecords.switchMoveMode, isFirstPersonMode, FIRST_PERSON_FORBID_KEY)
    self.view.hintNode.moveNode.gameObject:SetActive(not isFirstPersonMode)
    self.view.hintNode.moveRoleNode.gameObject:SetActive(not isFirstPersonMode)
    self.view.hintNode.moveCamNode.gameObject:SetActive(not isFirstPersonMode)
    self.view.hintNode.pcZoomNode.gameObject:SetActive(not isFirstPersonMode)
    self.view.hintNode.gamepadZoomNode.gameObject:SetActive(not isFirstPersonMode)



    self:_ResetYAxisRotSlider()
    self:_OnFirstPersonChangedForMoveChar()
end

SnapshotCtrl._ResetPerspective = HL.Method() << function(self)
    self:Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_SNAPSHOT_RESET_PERSPECTIVE_SECOND_CONFIRM,
        onConfirm = function()
            snapshotSystem.camController:ResetToInitialParam()


            self:_ResetYAxisRotSlider()
        end,
    })
end

SnapshotCtrl._ChangeFocalLength = HL.Method(HL.Number) << function(self, newValue)
    self.view.focalLengthNode.sliderValueTxt.text = string.format("%.0f mm", newValue)
    self.m_cameraCtrl:SetFocalLenCamera(newValue)
end

SnapshotCtrl._SwitchMoveMode = HL.Method(HL.Boolean, HL.Boolean) << function(self, isCameraMoveMode, showTip)
    if DeviceInfo.usingController and isCameraMoveMode then
        return
    end
    if showTip then
        local msg = isCameraMoveMode
            and Language.LUA_SNAPSHOT_SWITCH_MOVE_MODE_CAMERA
            or Language.LUA_SNAPSHOT_SWITCH_MOVE_MODE_PLAYER
        Notify(MessageConst.SHOW_TOAST, msg)
    end
    snapshotSystem:SwitchMoveMode(isCameraMoveMode)
    if isCameraMoveMode then
        self.view.hintNode.pcMoveNodeStateCtrl:SetState("CameraMove")
        Notify(MessageConst.SNAPSHOT_CAMERA_MOVE_MODE)
    else
        self.view.hintNode.pcMoveNodeStateCtrl:SetState("PlayerMove")
        Notify(MessageConst.SNAPSHOT_PLAYER_MOVE_MODE)
    end
    if not DeviceInfo.usingController then
        ClientDataManagerInst:SetBool(DATA_KEY_MOVE_MODE, isCameraMoveMode, true)
    end
end

SnapshotCtrl._SwitchSnapshotUIVisible = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, isShow, isInit)
    self.m_isShowSnapshotUI = isShow
    self.view.captureInvisibleRoot.gameObject:SetActive(isShow)
    if DeviceInfo.usingController then
        self.view.inputGroup.enabled = isShow
    elseif DeviceInfo.usingTouch then
        self.m_phase:SetForbidJoystick(not isShow, "SnapshotUIVisibleUsingTouch")
    end
    if not isInit then
        AudioAdapter.PostEvent(isShow and "Au_UI_Popup_Common_Large_Open" or "Au_UI_Popup_Common_Large_Close")
    end
    if not isShow then
        Notify(MessageConst.HIDE_ITEM_TIPS)
    end
end





SnapshotCtrl._ChangeMenuTab = HL.Method(HL.Number, HL.Boolean, HL.Opt(HL.Boolean)) << function(self, luaIndex, autoExpand, isInit)
    local oldIndex = self.m_curSelectMenuIndex
    self.m_curSelectMenuIndex = luaIndex
    if oldIndex ~= luaIndex then
        if self.m_isMenuExpand then
            self.m_menuTabCellList[luaIndex].tabAniWrapper:ClearTween(false)
            self.m_menuTabCellList[oldIndex].tabAniWrapper:Play("menutabcell_selectout")
        end
        self.m_menuContentCellList[oldIndex].gameObject:SetActive(false)
        if self.m_onChangeContentFuncList[oldIndex].leaveFunc then
            self.m_onChangeContentFuncList[oldIndex].leaveFunc()
        end
    end
    if isInit then
        self.m_menuTabCellList[luaIndex].tabAniWrapper:ClearTween(false)
        self.m_menuTabCellList[oldIndex].tabAniWrapper:SampleClipAtPercent(self.m_isMenuExpand and "menutabcell_select" or "menutabcell_selectout", 1)
    else
        self.m_menuTabCellList[luaIndex].tabAniWrapper:ClearTween(false)
        self.m_menuTabCellList[luaIndex].tabAniWrapper:Play("menutabcell_select")
        if self.m_onChangeContentFuncList[luaIndex].entryFunc then
            self.m_onChangeContentFuncList[luaIndex].entryFunc()
        end
    end
    self.m_menuContentCellList[luaIndex].gameObject:SetActive(true)

    if autoExpand and not self.m_isMenuExpand then
        self:_SwitchMenuContentExpand(true)
    elseif self.m_isMenuExpand then

        self.m_onChangeContentFuncList[luaIndex].naviFunc()
    end
end

SnapshotCtrl._SwitchMenuContentExpand = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, isExpand, isInit)
    self.m_isMenuExpand = isExpand
    if isExpand then
        self:_InitBasicSliderControllerInputs()
    end
    if isInit then
        self.m_menuTabCellList[self.m_curSelectMenuIndex].selectStateCtrl:SetState(isExpand and "Select" or "Unselect")
        self.view.menuNodeAniWrapper:SampleClipAtPercent(isExpand and "menunode_in" or "menunode_out", 1)
        self.m_menuTabCellList[self.m_curSelectMenuIndex].tabAniWrapper:SampleClipAtPercent(isExpand and "menutabcell_select" or "menutabcell_selectout", 1)
    else
        self.m_menuTabCellList[self.m_curSelectMenuIndex].tabAniWrapper:Play(isExpand and "menutabcell_select" or "menutabcell_selectout")
        if isExpand then
            self.view.menuNodeAniWrapper:ClearTween(false)
            self:ClearNaviTarget()
            self.view.menuNodeAniWrapper:Play("menunode_in", function()
                self.m_onChangeContentFuncList[self.m_curSelectMenuIndex].naviFunc()
            end)
        else
            self.view.menuNodeAniWrapper:ClearTween(false)
            self:_CloseEnvironmentDropDown()
            self:_CloseActionVideo()
            self.view.menuNodeAniWrapper:Play("menunode_out")
        end
        AudioManager.PostEvent(isExpand and "Au_UI_Popup_Photo_Small_Open" or "Au_UI_Popup_Photo_Small_Close")
    end
    if DeviceInfo.usingController then
        self.view.menuTabNode.keyHintPreTabRoot.gameObject:SetActive(isExpand)
        self.view.menuTabNode.keyHintNextTabRoot.gameObject:SetActive(isExpand)
        self.m_phase:SetForbidJoystick(isExpand, "focusMenu")
        InputManagerInst:ToggleGroup(self.m_phase.snapshotCameraPanel.uiCtrl.view.inputGroup.groupId, not isExpand)
        self.view.hintNode.gameObject:SetActive(not isExpand)
        self.view.menuTabNode.focusMenuKeyHintNode.gameObject:SetActive(not isExpand)
    end
    if DeviceInfo.usingTouch then
        if self.m_phase.snapshotJoystickPanel then
            local joystickCtrl = self.m_phase.snapshotJoystickPanel.uiCtrl
            joystickCtrl.view.joystick.gameObject:SetActive(not isExpand)
        end
    end

    self:_RefreshTaskNodeVisible()
end

SnapshotCtrl._InitBasicSliderControllerInputs = HL.Method() << function(self)
    if self.m_isBasicSliderControllerInputInited then
        return
    end
    self.m_isBasicSliderControllerInputInited = true

    local basicNode = self.view.menuContentNode.menuBasicNode
    local apertureGroupId = basicNode.apertureSlider.groupId
    local manualFocusSliderNode = basicNode.manualFocusSliderNode
    local manualFocusGroupId = manualFocusSliderNode.manualFocusSlider.groupId
    local yAxisRotSliderNode = basicNode.yAxisRotSliderNode
    local yAxisRotGroupId = yAxisRotSliderNode.yAxisRotSlider.groupId
    if apertureGroupId <= 0 or manualFocusGroupId <= 0 or yAxisRotGroupId <= 0 then
        logger.error("[snapshot error] _InitBasicSliderControllerInputs: groupId <= 0")
        return
    end


    UIUtils.bindInputPlayerAction(basicNode.keyHintApertureSliderLeft.actionId, function()
        self.m_minusApertureCoroutine = self:_ClearCoroutine(self.m_minusApertureCoroutine)
        self.m_minusApertureCoroutine = self:_StartSliderChangeCoroutine(
            basicNode.apertureSlider, -0.1, -self.view.config.CONTROLLER_CHANGE_APERTURE_SPEED)
    end, apertureGroupId)
    UIUtils.bindInputPlayerAction("snapshot_controller_aperture_minus_end", function()
        self.m_minusApertureCoroutine = self:_ClearCoroutine(self.m_minusApertureCoroutine)
    end, apertureGroupId)
    UIUtils.bindInputPlayerAction(basicNode.keyHintApertureSliderRight.actionId, function()
        self.m_addApertureCoroutine = self:_ClearCoroutine(self.m_addApertureCoroutine)
        self.m_addApertureCoroutine = self:_StartSliderChangeCoroutine(
            basicNode.apertureSlider, 0.1, self.view.config.CONTROLLER_CHANGE_APERTURE_SPEED)
    end, apertureGroupId)
    UIUtils.bindInputPlayerAction("snapshot_controller_aperture_add_end", function()
        self.m_addApertureCoroutine = self:_ClearCoroutine(self.m_addApertureCoroutine)
    end, apertureGroupId)


    UIUtils.bindInputPlayerAction(manualFocusSliderNode.keyHintManualFocusSliderRight.actionId, function()
        self.m_addManualFocusCoroutine = self:_ClearCoroutine(self.m_addManualFocusCoroutine)
        self.m_addManualFocusCoroutine = self:_StartSliderChangeCoroutine(
            manualFocusSliderNode.manualFocusSlider, 0.1, self.view.config.CONTROLLER_CHANGE_FOCUS_SPEED)
    end, manualFocusGroupId)
    UIUtils.bindInputPlayerAction("snapshot_controller_focus_add_end", function()
        self.m_addManualFocusCoroutine = self:_ClearCoroutine(self.m_addManualFocusCoroutine)
    end, manualFocusGroupId)
    UIUtils.bindInputPlayerAction(manualFocusSliderNode.keyHintManualFocusSliderLeft.actionId, function()
        self.m_minusManualFocusCoroutine = self:_ClearCoroutine(self.m_minusManualFocusCoroutine)
        self.m_minusManualFocusCoroutine = self:_StartSliderChangeCoroutine(
            manualFocusSliderNode.manualFocusSlider, -0.1, -self.view.config.CONTROLLER_CHANGE_FOCUS_SPEED)
    end, manualFocusGroupId)
    UIUtils.bindInputPlayerAction("snapshot_controller_focus_minus_end", function()
        self.m_minusManualFocusCoroutine = self:_ClearCoroutine(self.m_minusManualFocusCoroutine)
    end, manualFocusGroupId)


    UIUtils.bindInputPlayerAction(yAxisRotSliderNode.keyHintYAxisRotSliderRight.actionId, function()
        self.m_addYAxisRotCoroutine = self:_ClearCoroutine(self.m_addYAxisRotCoroutine)
        self.m_addYAxisRotCoroutine = self:_StartSliderChangeCoroutine(
            yAxisRotSliderNode.yAxisRotSlider, 1, self.view.config.CONTROLLER_CHANGE_ROLL_ANGLE_SPEED)
    end, yAxisRotGroupId)
    UIUtils.bindInputPlayerAction("snapshot_controller_yaxisrot_add_end", function()
        self.m_addYAxisRotCoroutine = self:_ClearCoroutine(self.m_addYAxisRotCoroutine)
    end, yAxisRotGroupId)
    UIUtils.bindInputPlayerAction(yAxisRotSliderNode.keyHintYAxisRotSliderLeft.actionId, function()
        self.m_minusYAxisRotCoroutine = self:_ClearCoroutine(self.m_minusYAxisRotCoroutine)
        self.m_minusYAxisRotCoroutine = self:_StartSliderChangeCoroutine(
            yAxisRotSliderNode.yAxisRotSlider, -1, -self.view.config.CONTROLLER_CHANGE_ROLL_ANGLE_SPEED)
    end, yAxisRotGroupId)
    UIUtils.bindInputPlayerAction("snapshot_controller_yaxisrot_minus_end", function()
        self.m_minusYAxisRotCoroutine = self:_ClearCoroutine(self.m_minusYAxisRotCoroutine)
    end, yAxisRotGroupId)

    InputManagerInst:ToggleGroup(apertureGroupId, false)
    InputManagerInst:ToggleGroup(manualFocusGroupId, false)
    InputManagerInst:ToggleGroup(yAxisRotGroupId, false)
    self:_SetBasicSliderKeyHintsActive(false)
end

SnapshotCtrl._InitQuickMoveCharControllerInputs = HL.Method() << function(self)
    if self.m_isQuickMoveCharControllerInputInited then
        return
    end
    self.m_isQuickMoveCharControllerInputInited = true

    local quickNode = self.view.quickMoveRotateCharNode
    local quickNodeGroupId = quickNode.inputGroup.groupId
    if quickNodeGroupId <= 0 then
        logger.error("[snapshot error] _InitQuickMoveCharControllerInputs: quickNodeGroupId <= 0")
        return
    end

    if DeviceInfo.usingController then
        local preActionId = quickNode.preAvatarKeyHint.actionId
        local nextActionId = quickNode.nextAvatarKeyHint.actionId
        UIUtils.bindInputPlayerAction(preActionId, function()
            if self.m_skipThisTimeQuickMoveCharChangePreAvatar then
                self.m_skipThisTimeQuickMoveCharChangePreAvatar = false
                return
            end
            if not self:IsInGamepadMoveRotateMode() then
                return
            end
            self:_StepQuickAvatar(-1)
        end, quickNodeGroupId)
        UIUtils.bindInputPlayerAction(nextActionId, function()
            if not self:IsInGamepadMoveRotateMode() then
                return
            end
            self:_StepQuickAvatar(1)
        end, quickNodeGroupId)
    end
end



SnapshotCtrl._ChangeCharShowMode = HL.Method(HL.Number) << function(self, luaIndex)
    self.m_curShowCharIndex = luaIndex
    local squadManager = GameInstance.player.squadManager
    local count = squadManager.curSquad.slots.Count
    local mainCharacter = GameInstance.playerController.mainCharacter

    local info = showCharConfig[luaIndex]
    local showLeader = info.showLeader
    local showTeamMate = info.showTeamMate

    for i = 0, count - 1 do
        local entity = squadManager:GetMemberBySlot(i)
        if entity ~= nil then
            if entity ~= mainCharacter then
                CS.Beyond.Gameplay.View.ViewUtils.SetEntityActive(entity, showTeamMate, CS.Beyond.Gameplay.View.ModelVisibleType.Snapshot)
            else
                CS.Beyond.Gameplay.View.ViewUtils.SetEntityActive(entity, showLeader, CS.Beyond.Gameplay.View.ModelVisibleType.Snapshot)
            end
        end
    end
    self:_OnChangeCharShowModeForMoveChar()
end

SnapshotCtrl._SwitchShowNpc = HL.Method(HL.Boolean) << function(self, isShow)
    if isShow then
        GameWorld.npcManager:DisableNpcVisibleRule()
    else
        GameWorld.npcManager:EnableNpcVisibleRule(CS.Beyond.Gameplay.NpcEnums.NpcVisibleRuleType.WhiteList, nil)
    end
end

SnapshotCtrl._SwitchShowDropItem = HL.Method(HL.Boolean) << function(self, isShow)
    GameWorld.gameMechManager.itemDropBrain:SetDropItemVisible(isShow)
end


SnapshotCtrl._SetEnvironmentSelection = HL.Method(HL.Table) << function(self, selection)
    self.m_environmentSelection = {
        npc = selection.npc == true,
        dropItem = selection.dropItem == true,
        decorationBuilding = selection.decorationBuilding == true,
        otherBuilding = selection.otherBuilding == true,
    }
end


SnapshotCtrl._GetEnvironmentOptionKey = HL.Method(HL.Number).Return(HL.Opt(HL.String)) << function(self, csIndex)
    local config = environmentOptionConfig[LuaIndex(csIndex)]
    return config and config.key or nil
end


SnapshotCtrl._RefreshEnvironmentOptionCell = HL.Method(HL.Number, HL.Any, HL.Boolean) << function(self, csIndex, option, _)
    local config = environmentOptionConfig[LuaIndex(csIndex)]
    if config then
        option:SetText(Language[config.nameLuaKey])
    end
end


SnapshotCtrl._RefreshEnvironmentDropDownCaption = HL.Method(HL.Any) << function(_, dropDown)
    local selectedCount = dropDown.selectedCount
    if selectedCount == #environmentOptionConfig then
        dropDown.captionText.text = Language.LUA_SNAPSHOT_ENV_CAPTION_ALL
    elseif selectedCount == 0 then
        dropDown.captionText.text = Language.LUA_SNAPSHOT_ENV_CAPTION_NONE
    else
        dropDown.captionText.text = Language.LUA_SNAPSHOT_ENV_CAPTION_PARTIAL
    end
end


SnapshotCtrl._RefreshEnvironmentDropDown = HL.Method() << function(self)
    local dropDown = self.view.menuContentNode.menuBasicNode.environmentNode.environmentDropDown
    if not self.m_environmentSelection then
        self:_SetEnvironmentSelection({
            npc = true,
            dropItem = false,
            decorationBuilding = true,
            otherBuilding = true,
        })
    end
    dropDown:Refresh(#environmentOptionConfig)
    for i, config in ipairs(environmentOptionConfig) do
        dropDown:SetSelected(CSIndex(i), self.m_environmentSelection[config.key] == true, true, false, false)
    end
    self:_RefreshEnvironmentDropDownCaption(dropDown)
    dropDown:ToggleOptions(false)
end


SnapshotCtrl._CloseEnvironmentDropDown = HL.Method() << function(self)
    self.view.menuContentNode.menuBasicNode.environmentNode.environmentDropDown:ToggleOptions(false)
end


SnapshotCtrl._OnEnvironmentDropDownValueChanged = HL.Method(HL.Number, HL.Boolean) << function(self, csIndex, isOn)
    local optionKey = self:_GetEnvironmentOptionKey(csIndex)
    if optionKey then
        self:_OnEnvironmentOptionChanged(optionKey, isOn)
        self:_RefreshEnvironmentDropDownCaption(
            self.view.menuContentNode.menuBasicNode.environmentNode.environmentDropDown)
    end
end


SnapshotCtrl._OnEnvironmentOptionChanged = HL.Method(HL.String, HL.Boolean) << function(self, optionKey, isOn)
    if self.m_environmentSelection[optionKey] == isOn then
        return
    end
    self.m_environmentSelection[optionKey] = isOn
    if optionKey == "npc" then
        self:_SwitchShowNpc(isOn)
    elseif optionKey == "dropItem" then
        self:_SwitchShowDropItem(isOn)
    elseif optionKey == "decorationBuilding" then
        self:_SwitchDecorationBuildingVisible(isOn)
    elseif optionKey == "otherBuilding" then
        self:_SwitchOtherBuildingVisible(isOn)
    end
end


SnapshotCtrl._ApplyEnvironmentVisibility = HL.Method() << function(self)
    self:_SwitchShowNpc(self.m_environmentSelection.npc == true)
    self:_SwitchShowDropItem(self.m_environmentSelection.dropItem == true)
    self:_SwitchDecorationBuildingVisible(self.m_environmentSelection.decorationBuilding == true)
    self:_SwitchOtherBuildingVisible(self.m_environmentSelection.otherBuilding == true)
end


SnapshotCtrl._SwitchDecorationBuildingVisible = HL.Method(HL.Boolean) << function(_, isShow)
    snapshotSystem:SwitchDecorationBuildingVisible(isShow)
end


SnapshotCtrl._SwitchOtherBuildingVisible = HL.Method(HL.Boolean) << function(_, isShow)
    snapshotSystem:SwitchOtherBuildingVisible(isShow)
end

SnapshotCtrl._SwitchShowGridLines = HL.Method(HL.Boolean) << function(self, isShow)
    self.m_isShowGridLines = isShow
    self.view.gridLines.gameObject:SetActive(isShow)
end

SnapshotCtrl._ChangeAperture = HL.Method(HL.Number) << function(self, value)
    self.m_cameraCtrl:SetApertureCamera(value)
    self.view.menuContentNode.menuBasicNode.apertureValueTxt.text = self:_GetApertureShowString(value)
end

SnapshotCtrl._GetApertureShowString = HL.Method(HL.Number).Return(HL.String) << function(self, value)
    local result = value
    if (lume.round(value, 0.1) * 10 % 10) < 1 then
        result = string.format("(f/%.0f)", value)
    else
        result = string.format("(f/%.1f)", value)
    end
    return result
end





SnapshotCtrl._StartSliderChangeCoroutine = HL.Method(HL.Userdata, HL.Number, HL.Number).Return(HL.Thread) << function(self, slider, initialDelta, speed)
    return self:_StartCoroutine(function()

        slider.value = slider.value + initialDelta
        AudioManager.PostEvent("Au_UI_Slider_Common")

        coroutine.wait(UIConst.NUMBER_SELECTOR_COUNT_REFRESH_INTERVAL)
        local holdElapsed = 0
        while true do
            holdElapsed = holdElapsed + Time.deltaTime

            slider.value = slider.value + Time.deltaTime * speed * (holdElapsed >= 1.5 and 2 or 1)
            AudioManager.PostEvent("Au_UI_Slider_Common")
            coroutine.step()
        end
    end)
end



SnapshotCtrl._OnManualFocusDropDownChanged = HL.Method(HL.Number) << function(self, luaIndex)
    local basicNode = self.view.menuContentNode.menuBasicNode
    local sliderNode = basicNode.manualFocusSliderNode
    if luaIndex == 1 then

        self.m_isManualFocus = true
        self.m_focusCharSlotIndex = -1
        sliderNode.manualFocusSlider.interactable = true
        self.m_nextAutoFocusTime = -1

        self:_OnManualFocusSliderChanged(sliderNode.manualFocusSlider.value)
    else

        self.m_isManualFocus = false
        self.m_focusCharSlotIndex = luaIndex - 2

        sliderNode.manualFocusSlider.interactable = false

        snapshotSystem:AutoFocusOnSquadMember(self.m_focusCharSlotIndex)
        self.m_nextAutoFocusTime = Time.time + self.m_autoFocusDistanceTime
        self.view.menuContentNode.menuBasicNode.manualFocusSliderNode.manualFocusTxt.text = ""
    end
end



SnapshotCtrl._OnManualFocusSliderChanged = HL.Method(HL.Number) << function(self, value)

    if not self.m_isManualFocus then
        return
    end

    snapshotSystem.camController:SetFocusDistance(value)

    self.view.menuContentNode.menuBasicNode.manualFocusSliderNode.manualFocusTxt.text = string.format("(%.1fm)", value)
end



SnapshotCtrl._OnYAxisRotSliderChanged = HL.Method(HL.Number) << function(self, value)

    self.m_cameraCtrl:SetCameraRoll(value)

    self.view.menuContentNode.menuBasicNode.yAxisRotSliderNode.yAxisRotTxt.text =
    string.format(Language.LUA_SNAPSHOT_Y_AXIS_ROT_FORMAT, math.floor(value))
end






SnapshotCtrl._ResetYAxisRotSlider = HL.Method() << function(self)
    local sliderNode = self.view.menuContentNode.menuBasicNode.yAxisRotSliderNode

    sliderNode.yAxisRotSlider:SetValueWithoutNotify(0)

    self.m_cameraCtrl:SetCameraRoll(0)
    sliderNode.yAxisRotTxt.text = string.format(Language.LUA_SNAPSHOT_Y_AXIS_ROT_FORMAT, 0)
end



SnapshotCtrl._RefreshManualFocusDropDown = HL.Method() << function(self)

    local count = 1 + GameInstance.player.squadManager.curSquad.slots.Count
    self.view.menuContentNode.menuBasicNode.manualFocusDropDownNode.manualFocusDropDown:Refresh(count, 0, false)
end





SnapshotCtrl._OpenCharFormation = HL.Method() << function(self)
    if snapshotSystem.camController then

        snapshotSystem.camController:ResetSnapshotDofSettings()
    end
    GameInstance.player.forbidSystem:SetPhaseForbid("CharInfo", "SNAPSHOT", true, nil)
    UIManager:ToggleBlockObtainWaysJump("SNAPSHOT", true)
    PhaseManager:OpenPhase(PhaseId.CharFormation, {
        onCloseCallback = function()
            GameInstance.player.forbidSystem:SetPhaseForbid("CharInfo", "SNAPSHOT", false, nil)
            UIManager:ToggleBlockObtainWaysJump("SNAPSHOT", false)

            if self.m_isClosed or InputManagerInst.inChangingInputDevice then
                return
            end
            self:_RestoreSnapshotDofAfterCharFormation()
        end,
    })
end

SnapshotCtrl._RestoreSnapshotDofAfterCharFormation = HL.Method() << function(self)
    if not snapshotSystem.camController then
        return
    end
    snapshotSystem.camController:ApplySnapshotDofSettings()



    local focalLengthNode = self.view.focalLengthNode
    self:_ChangeFocalLength(focalLengthNode.focalLengthSlider.value)

    local basicNode = self.view.menuContentNode.menuBasicNode
    self:_ChangeAperture(basicNode.apertureSlider.value)
    if self.m_isManualFocus then
        self:_OnManualFocusSliderChanged(basicNode.manualFocusSliderNode.manualFocusSlider.value)
    end
end

SnapshotCtrl._FormationDropdownIndexToTeamFormationIndex = HL.Method(HL.Number).Return(HL.Number) << function(self, csIndex)

    if csIndex == FORMATION_DROPDOWN_INDEX_CUSTOM then
        return TEAM_FORMATION_INDEX_CUSTOM
    elseif csIndex <= FORMATION_DROPDOWN_INDEX_NONE then
        return TEAM_FORMATION_INDEX_NONE
    end
    return csIndex - FORMATION_DROPDOWN_PRESET_OFFSET
end

SnapshotCtrl._TeamFormationIndexToDropdownIndex = HL.Method(HL.Number).Return(HL.Number) << function(self, teamFormationIndex)

    if teamFormationIndex == TEAM_FORMATION_INDEX_CUSTOM then
        return FORMATION_DROPDOWN_INDEX_CUSTOM
    elseif teamFormationIndex < 0 then
        return FORMATION_DROPDOWN_INDEX_NONE
    end
    return lume.clamp(teamFormationIndex + FORMATION_DROPDOWN_PRESET_OFFSET,
        FORMATION_DROPDOWN_INDEX_NONE, formationManager.formationUIData.Count + FORMATION_DROPDOWN_PRESET_OFFSET - 1)
end

SnapshotCtrl._ResetFormationDropDownNextFrame = HL.Method(HL.Number) << function(self, teamFormationIndex)
    self:_StartCoroutine(function()
        coroutine.step()

        self.view.menuContentNode.menuFormationNode.formationDropDown:SetSelected(
            self:_TeamFormationIndexToDropdownIndex(teamFormationIndex), false, false)
    end)
end

SnapshotCtrl._HasCustomFormationEdits = HL.Method().Return(HL.Boolean) << function(self)
    return snapshotSystem:HasAnyCustomAction() or snapshotSystem:HasMoveCharCustomPositionEdits()
end

SnapshotCtrl._ShowChangeCustomFormationToNoneConfirm = HL.Method() << function(self)

    local oldIndex = self.m_curTeamFormationIndex
    self:Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_SNAPSHOT_FORMATION_CUSTOM_RESET_CONFIRM,
        onConfirm = function()
            self:_ChangeTeamFormation(TEAM_FORMATION_INDEX_NONE, true)
        end,
        onCancel = function()
            self.view.menuContentNode.menuFormationNode.formationDropDown:SetSelected(
                self:_TeamFormationIndexToDropdownIndex(oldIndex), false, false)
        end,
    })
end

SnapshotCtrl._ResetAllCustomActionsAndRefreshActionUI = HL.Method() << function(self)
    snapshotSystem:ResetAllCustomActions()
    if self.m_isInitRefreshActionUI then
        self:_OnActionForceReset()
    end
end

SnapshotCtrl._TryEnterCustomTeamFormation = HL.Method(HL.Boolean).Return(HL.Boolean) << function(self, showToast)
    if self.m_curTeamFormationIndex >= 0 then

        formationManager:ExitPhotoFormation()
    end
    local ret = snapshotSystem:SetAllTeamDummy(true)
    if ret ~= 0 then
        return false
    end
    self.m_curTeamFormationIndex = TEAM_FORMATION_INDEX_CUSTOM
    if showToast then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SNAPSHOT_FORMATION_CUSTOM_TOAST)
    end
    return true
end

SnapshotCtrl._EnsureCustomTeamFormation = HL.Method().Return(HL.Boolean) << function(self)
    if self.m_curTeamFormationIndex == TEAM_FORMATION_INDEX_CUSTOM then
        return true
    end

    local oldIndex = self.m_curTeamFormationIndex

    if not self:_TryEnterCustomTeamFormation(true) then
        if oldIndex >= 0 then

            self.m_curTeamFormationIndex = TEAM_FORMATION_INDEX_NONE
            self.view.menuContentNode.menuFormationNode.formationDropDown:SetSelected(
                FORMATION_DROPDOWN_INDEX_NONE, false, false)
        end
        return false
    end
    self.view.menuContentNode.menuFormationNode.formationDropDown:SetSelected(FORMATION_DROPDOWN_INDEX_CUSTOM, false, false)
    return true
end

SnapshotCtrl._ResetTeamFormationToNone = HL.Method() << function(self)
    self.m_curTeamFormationIndex = TEAM_FORMATION_INDEX_NONE
    formationManager:ExitPhotoFormation()
    self:_ResetFormationDropDownNextFrame(TEAM_FORMATION_INDEX_NONE)
end

SnapshotCtrl._ReapplyTeamFormationAfterSquadChanged = HL.Method() << function(self)
    if not self.m_isSquadDirty then
        return
    end
    self.m_isSquadDirty = false

    local formationIndex = self.m_curTeamFormationIndex
    if formationIndex == TEAM_FORMATION_INDEX_NONE then
        return
    end


    self:_ChangeTeamFormation(TEAM_FORMATION_INDEX_NONE, true)
    self:_ResetFormationDropDownNextFrame(TEAM_FORMATION_INDEX_NONE)
end

SnapshotCtrl._ChangeTeamFormation = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, csIndex, skipConfirm)
    local oldIndex = self.m_curTeamFormationIndex
    if csIndex == TEAM_FORMATION_INDEX_CUSTOM then
        if not self:_TryEnterCustomTeamFormation(true) then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_SNAPSHOT_FORBID_COMMON_TOAST)
            self:_ResetTeamFormationToNone()
            return
        end
        return
    elseif csIndex == TEAM_FORMATION_INDEX_NONE then


        if oldIndex ~= TEAM_FORMATION_INDEX_NONE and self:_HasCustomFormationEdits() and not skipConfirm then
            self:_ShowChangeCustomFormationToNoneConfirm()
            return
        elseif oldIndex ~= TEAM_FORMATION_INDEX_NONE and self:_HasCustomFormationEdits() then
            self:_ResetAllCustomActionsAndRefreshActionUI()
            snapshotSystem:ClearMoveCharCustomPositionEdits()
        end
        if oldIndex == TEAM_FORMATION_INDEX_CUSTOM then
            snapshotSystem:SetAllTeamDummy(false)
        end
        self.m_curTeamFormationIndex = TEAM_FORMATION_INDEX_NONE
        formationManager:ExitPhotoFormation()
        return
    end

    local resultId = formationManager:EnterPhotoFormation(formationManager.formationUIData[csIndex].Item1);
    if resultId < 0 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SNAPSHOT_FORBID_COMMON_TOAST)
        self:_ResetTeamFormationToNone()
        return
    end
    self.m_curTeamFormationIndex = csIndex
end


SnapshotCtrl._OnClickTouchPlateForMoveChar = HL.Method(HL.Opt(HL.Any)).Return(HL.Boolean) << function(self, eventData)
    if not eventData then
        return false
    end
    local hitSlot = snapshotSystem:PickSquadMemberAtScreen(eventData.position)
    if hitSlot >= 0 then
        self:_TrySelectMoveChar(hitSlot)
        return true
    elseif snapshotSystem:GetMoveSelectedSlotIndex() >= 0 then
        self:_DeselectMoveChar()
        return true
    end
    return false
end


SnapshotCtrl.IsInGamepadMoveRotateMode = HL.Method().Return(HL.Boolean) << function(self)
    return DeviceInfo.usingController and snapshotSystem:GetMoveSelectedSlotIndex() >= 0
end


SnapshotCtrl._OnClickQuickMoveRotateCharBtn = HL.Method() << function(self)
    if not DeviceInfo.usingController or self:IsInGamepadMoveRotateMode() then
        return
    end

    if not self.m_squadCharList or #self.m_squadCharList == 0 then
        self:_UpdateSquadCharList()
    end
    local squadManager = GameInstance.player.squadManager
    local slot = squadManager:GetMemberIndex(GameInstance.playerController.mainCharacter)
    if slot < 0 or not self:_IsFormationAvatarSelectable(slot) then
        slot = self:_GetFirstSelectableSlot()
    end
    if slot < 0 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SNAPSHOT_FORBID_PLAYER_MOVE)
        return
    end
    self:_EnterGamepadMoveRotateCharMode(slot, false)
end


SnapshotCtrl._EnterGamepadMoveRotateCharMode = HL.Method(HL.Number, HL.Boolean) << function(self, slotIndex, fromMenu)
    if self:IsInGamepadMoveRotateMode() then
        return
    end

    if not self:_TrySelectMoveChar(slotIndex) then
        self.m_gamepadMoveRotateEntrySlot = -1
        self.m_gamepadMoveRotateEntryFromMenu = false
        return
    end
    self.m_gamepadMoveRotateEntrySlot = slotIndex
    self.m_gamepadMoveRotateEntryFromMenu = fromMenu

    if fromMenu then
        self.view.menuNodeNaviGroup:ManuallyStopFocus()
    end
    self:_SyncQuickMoveRotateNodeState()
    self:_RefreshQuickMoveRotateAvatarList()
    self.view.quickMoveRotateCharBtn.gameObject:SetActive(false)
    self:_InitQuickMoveCharControllerInputs()
end


SnapshotCtrl._ExitGamepadMoveRotateCharMode = HL.Method() << function(self)
    if not self:IsInGamepadMoveRotateMode() then
        self.m_gamepadMoveRotateEntrySlot = -1
        self.m_gamepadMoveRotateEntryFromMenu = false
        self:_SyncQuickMoveRotateNodeState()
        return
    end

    local entrySlot = self.m_gamepadMoveRotateEntrySlot
    local entryFromMenu = self.m_gamepadMoveRotateEntryFromMenu
    self:_DeselectMoveChar()
    self:_SyncQuickMoveRotateNodeState()
    if entryFromMenu then
        if not self.m_isMenuExpand then
            self:_SwitchMenuContentExpand(true)
        end
        self.view.menuNodeNaviGroup:ManuallyFocus()
        self:_NaviToFormationAvatarBySlot(entrySlot)
        self.m_needIgnoreMenuFormationNavi = true
    end
    self.m_gamepadMoveRotateEntrySlot = -1
    self.m_gamepadMoveRotateEntryFromMenu = false
    self.view.quickMoveRotateCharBtn.gameObject:SetActive(true)
end


SnapshotCtrl._NaviToFormationAvatarBySlot = HL.Method(HL.Number) << function(self, slotIndex)
    if slotIndex < 0 or self.m_formationAvatarCellCache == nil then
        return
    end
    local cell = self.m_formationAvatarCellCache:Get(LuaIndex(slotIndex))
    if cell ~= nil then
        self:SetNaviTarget(cell.btn)
    end
end


SnapshotCtrl._GetMoveSelectedSquadAvatarIndex = HL.Method().Return(HL.Number) << function(self)
    local selectedSlot = snapshotSystem:GetMoveSelectedSlotIndex()
    if selectedSlot < 0 then
        return 0
    end
    return LuaIndex(selectedSlot)
end


SnapshotCtrl._StepQuickAvatar = HL.Method(HL.Number) << function(self, step)
    if not self:IsInGamepadMoveRotateMode() then
        return
    end
    local count = self.m_squadCharList and #self.m_squadCharList or 0
    if count <= 0 then
        return
    end

    local curIndex = LuaIndex(snapshotSystem:GetMoveSelectedSlotIndex())
    for i = 1, count do
        local index = ((curIndex - 1 + step * i) % count + count) % count + 1
        local info = self.m_squadCharList[index]
        if info and self:_IsFormationAvatarSelectable(info.slotIndex) then
            self:_TrySelectMoveChar(info.slotIndex)
            return
        end
    end
end


SnapshotCtrl._GetFirstSelectableSlot = HL.Method().Return(HL.Number) << function(self)
    if not self.m_squadCharList or #self.m_squadCharList == 0 then
        self:_UpdateSquadCharList()
    end
    for _, info in ipairs(self.m_squadCharList) do
        if self:_IsFormationAvatarSelectable(info.slotIndex) then
            return info.slotIndex
        end
    end
    return -1
end


SnapshotCtrl._OnClickFormationAvatarCell = HL.Method(HL.Number) << function(self, luaIndex)
    local charInfo = self.m_squadCharList[luaIndex]
    if not charInfo then
        return
    end
    if DeviceInfo.usingController then
        self:_EnterGamepadMoveRotateCharMode(charInfo.slotIndex, true)
        return
    end
    self:_TrySelectMoveChar(charInfo.slotIndex)
end


SnapshotCtrl._TrySelectMoveChar = HL.Method(HL.Number).Return(HL.Boolean) << function(self, slotIndex)
    local oldSlot = snapshotSystem:GetMoveSelectedSlotIndex()
    if oldSlot == slotIndex then
        return false
    end

    local reasonKey = self:_GetFormationUnselectableReasonKey(slotIndex)
    if reasonKey then
        Notify(MessageConst.SHOW_TOAST, Language[reasonKey])
        return false
    end

    if self.m_curTeamFormationIndex ~= TEAM_FORMATION_INDEX_CUSTOM then
        if not self:_EnsureCustomTeamFormation() then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_SNAPSHOT_FORBID_COMMON_TOAST)
            return false
        end
    end

    if not snapshotSystem:SelectMoveChar(slotIndex) then
        return false
    end

    local entity = GameInstance.player.squadManager:GetMemberBySlot(slotIndex)
    local characterId = entity and entity.templateData.id or ""
    local _, charCfg = Tables.characterTable:TryGetValue(characterId)
    local charName = charCfg and charCfg.name or characterId
    local toastKey = DeviceInfo.usingController and "LUA_SNAPSHOT_MOVE_CHAR_SELECTED_GAMEPAD" or "LUA_SNAPSHOT_MOVE_CHAR_SELECTED"
    Notify(MessageConst.SHOW_TOAST, string.format(Language[toastKey], charName))

    self:_RefreshMoveAvatarCellsBySlot(oldSlot)
    self:_RefreshMoveAvatarCellsBySlot(slotIndex)
    self:_SyncActionMenuAvatarToMoveChar()
    self:_SetMoveSelectUIVisible(true, true)
    return true
end


SnapshotCtrl._DeselectMoveChar = HL.Method() << function(self)
    local joystickCtrl = self.m_phase.snapshotJoystickPanel and self.m_phase.snapshotJoystickPanel.uiCtrl
    if joystickCtrl ~= nil then
        joystickCtrl.isInGamepadRotateSubMode = false
    end
    if snapshotSystem:GetMoveSelectedSlotIndex() < 0 then
        return
    end
    local oldSlot = snapshotSystem:GetMoveSelectedSlotIndex()
    self:_SetMoveSelectUIVisible(false, true)
    snapshotSystem:DeselectMoveChar()
    self:_RefreshMoveAvatarCellsBySlot(oldSlot)
end



SnapshotCtrl._RefreshMoveSelectArrow = HL.Method() << function(self)
    local selectedSlot = snapshotSystem:GetMoveSelectedSlotIndex()
    if selectedSlot < 0 or self.m_isInCapture then
        return
    end

    local entity = GameInstance.player.squadManager:GetMemberBySlot(selectedSlot)
    local camera = CameraManager.mainCamera
    if entity == nil or entity.rootCom == nil or IsNull(camera) then
        return
    end

    local arrowImg = self.view.moveSelectArrowImg
    local arrowRect = arrowImg.rectTransform

    local headTrans = entity.rootCom:GetNodeTransform(CS.Beyond.Gameplay.MountPoint.Head)
    local worldPos = (IsNull(headTrans) and entity.position or headTrans.position) + MOVE_SELECT_ARROW_HEAD_WORLD_OFFSET
    local uiPos = UIUtils.objectPosToUI(worldPos, self.uiCamera)
    local panelRect = self.view.rectTransform.rect
    local xRadius = self.view.config.ELLIPSE_X_RADIUS / CS.Beyond.UI.UIConst.STANDARD_HORIZONTAL_RESOLUTION * panelRect.width
    local yRadius = self.view.config.ELLIPSE_Y_RADIUS / CS.Beyond.UI.UIConst.STANDARD_VERTICAL_RESOLUTION * panelRect.height
    local finalPos, angle, isOutBound = UIUtils.mapScreenPosToEllipseEdge(
        uiPos, xRadius, yRadius)

    arrowRect.anchoredPosition = finalPos

    arrowRect.localRotation = isOutBound
        and Quaternion.Euler(0, 0, angle + 90)
        or Quaternion.Euler(0, 0, -self.m_cameraCtrl:GetCameraRoll())
end


SnapshotCtrl._SetMoveSelectArrowVisible = HL.Method(HL.Boolean) << function(self, visible)
    local arrowImg = self.view.moveSelectArrowImg
    if IsNull(arrowImg) then
        return
    end
    arrowImg.gameObject:SetActiveIfNecessary(visible)
end

SnapshotCtrl._SetMoveSelectUIVisible = HL.Method(HL.Boolean, HL.Boolean) << function(self, visible, playAni)
    snapshotSystem:ToggleSelectedCharOutline(visible)
    self:_SetMoveSelectArrowVisible(visible)
    self.view.hintNode.keyboardQuickRotateCharNode.gameObject:SetActive(visible)
    local showRotateSlider = visible and not self:IsInGamepadMoveRotateMode()
    if not DeviceInfo.usingController then
        self:_SetMoveCharRotate3DSliderVisible(showRotateSlider, playAni)
    end
end

SnapshotCtrl._EnsureMoveCharRotate3DSlider = HL.Method().Return(HL.Boolean) << function(self)
    if self.m_moveCharRotate3DSlider ~= nil and not self.m_moveCharRotate3DSlider.m_isDestroyed then
        return true
    end

    local sliderGo = self.m_moveCharRotate3DSliderGo
    if IsNull(sliderGo) then
        sliderGo = snapshotSystem:GetMoveCharRotate3DSliderGameObject()
        self.m_moveCharRotate3DSliderGo = sliderGo
        self.m_moveCharRotate3DSlider = nil
        if IsNull(sliderGo) then
            return false
        end
    end

    if self.m_moveCharRotate3DSlider == nil or self.m_moveCharRotate3DSlider.m_isDestroyed then
        local sliderTransform = sliderGo.transform
        local sliderWidgetTransform = sliderTransform:Find("Widget")
        if IsNull(sliderWidgetTransform) then
            return false
        end
        self.m_moveCharRotate3DSlider = Utils.wrapLuaNode(sliderWidgetTransform)
        if self.m_moveCharRotate3DSlider ~= nil then
            self.m_moveCharRotate3DSlider:InitSnapshotRotate3DSlider(sliderTransform)
        end
    end
    return self.m_moveCharRotate3DSlider ~= nil
end

SnapshotCtrl._SetMoveCharRotate3DSliderFollow = HL.Method(HL.Boolean) << function(self, isFollow)
    if not self:_EnsureMoveCharRotate3DSlider() then
        return
    end

    if not isFollow then
        self.m_moveCharRotate3DSlider:ClearFollowTarget()
        return
    end

    local selectedSlot = snapshotSystem:GetMoveSelectedSlotIndex()
    if selectedSlot >= 0 then
        self.m_moveCharRotate3DSlider:SetFollowTarget(selectedSlot)
    else
        self.m_moveCharRotate3DSlider:ClearFollowTarget()
    end
end

SnapshotCtrl._SetMoveCharRotate3DSliderVisible = HL.Method(HL.Boolean, HL.Boolean) << function(self, visible, playAni)
    if not visible then
        self.m_isKeyboardQuickRotateCharMode = false
    end
    if not self:_EnsureMoveCharRotate3DSlider() then
        return
    end

    if not visible then
        self.m_moveCharRotate3DSlider:EndRotateDrag()
    end
    self.m_moveCharRotate3DSlider.view.animationWrapper:ClearTween(false)
    if playAni then
        if visible then
            self.m_moveCharRotate3DSliderGo:SetActiveIfNecessary(true)
            self:_SetMoveCharRotate3DSliderFollow(true)
            self.m_moveCharRotate3DSlider.view.animationWrapper:PlayInAnimation()
        else
            self.m_moveCharRotate3DSlider.view.animationWrapper:PlayOutAnimation(function()
                self.m_moveCharRotate3DSliderGo:SetActiveIfNecessary(false)
                self:_SetMoveCharRotate3DSliderFollow(false)
            end)
        end
    else
        self.m_moveCharRotate3DSliderGo:SetActiveIfNecessary(visible)
        self:_SetMoveCharRotate3DSliderFollow(visible)
    end
end


SnapshotCtrl._UpdateDragHitMoveChar = HL.Method(HL.Any) << function(self, screenPos)
    self.m_dragHitMoveChar = false
    local selectedSlot = snapshotSystem:GetMoveSelectedSlotIndex()
    if selectedSlot < 0 then
        return
    end
    if snapshotSystem:IsScreenPosOnSquadMember(screenPos, selectedSlot) then
        self.m_dragHitMoveChar = true
        GameInstance.playerController:UpdateMoveCommand(Vector2.zero)
        GameInstance.playerController:OnSprintReleased()
        GameInstance.playerController:OnJoystickSprint(false)
        snapshotSystem:BeginMoveChar(screenPos)
        AudioAdapter.PostEvent("Au_UI_Event_PhotoCharDrag_Start")
    end
end


SnapshotCtrl._TryBeginDragRotateChar = HL.Method(Vector2, HL.Boolean).Return(HL.Boolean) << function(self, screenPos, requireButtonHit)
    self.m_dragHitRotateChar = false
    if snapshotSystem:GetMoveSelectedSlotIndex() < 0 then
        return false
    end
    if not self:_EnsureMoveCharRotate3DSlider() then
        return false
    end
    if requireButtonHit and not self.m_moveCharRotate3DSlider:IsRotateButtonScreenPosHit(screenPos) then
        return false
    end

    local centerScreenPos = self.m_moveCharRotate3DSlider:GetRotateCenterScreenPos()
    if centerScreenPos == nil then
        return false
    end

    local beginDir = screenPos - centerScreenPos
    if beginDir.sqrMagnitude <= 0.0001 then
        return false
    end



    self.m_dragHitMoveChar = false
    self.m_dragHitRotateChar = true
    self.m_rotateDragBeginScreenPos = screenPos
    self.m_rotateDragCenterScreenPos = centerScreenPos
    self.m_rotateDragBeginDir = beginDir.normalized

    snapshotSystem:BeginRotateChar()
    self.m_moveCharRotate3DSlider:BeginRotateDrag()
    AudioAdapter.PostEvent("Au_UI_Event_PhotoCharRotate_Start")
    return true
end


SnapshotCtrl._EnterKeyboardQuickRotateCharMode = HL.Method() << function(self)
    if not DeviceInfo.usingKeyboard or snapshotSystem:GetMoveSelectedSlotIndex() < 0 then
        return
    end

    if self.m_dragHitRotateChar then
        self.m_isKeyboardQuickRotateCharMode = true
        return
    end
    if self.m_dragHitMoveChar then
        snapshotSystem:EndMoveChar()
        self.m_dragHitMoveChar = false
    end
    if not self:_EnsureMoveCharRotate3DSlider() then
        return
    end

    self.m_isKeyboardQuickRotateCharMode = true
    self.m_moveCharRotate3DSlider:BeginRotateDrag()
    if self.m_dragStartScreenPos ~= nil then
        local mousePos = InputManager.mousePosition
        self:_TryBeginDragRotateChar(Vector2(mousePos.x, mousePos.y), false)
        return
    end
end


SnapshotCtrl._ExitKeyboardQuickRotateCharMode = HL.Method() << function(self)
    self.m_isKeyboardQuickRotateCharMode = false
    if self.m_dragHitRotateChar then
        return
    end
    if self.m_moveCharRotate3DSlider ~= nil then
        self.m_moveCharRotate3DSlider:EndRotateDrag()
    end
end


SnapshotCtrl._OnDragRotateChar = HL.Method(HL.Any) << function(self, eventData)
    if not self.m_dragHitRotateChar or self.m_moveCharRotate3DSlider == nil then
        return
    end
    if self.m_rotateDragCenterScreenPos == nil or self.m_rotateDragBeginDir == nil then
        return
    end

    local deltaAngle = self.m_moveCharRotate3DSlider:CalcDragDeltaAngle(
        self.m_rotateDragCenterScreenPos,
        self.m_rotateDragBeginDir,
        eventData.position)

    snapshotSystem:RotateChar(deltaAngle)
    self.m_moveCharRotate3DSlider:RefreshRotateDelta(deltaAngle)
end


SnapshotCtrl._EndDragRotateChar = HL.Method() << function(self)
    snapshotSystem:EndRotateChar()
    if self.m_moveCharRotate3DSlider ~= nil then
        self.m_moveCharRotate3DSlider:EndRotateDrag()
    end
end


SnapshotCtrl._ClearMoveRotateDragState = HL.Method() << function(self)
    self.m_dragStartScreenPos = nil
    self.m_dragHitMoveChar = false
    self.m_dragHitRotateChar = false
    self.m_rotateDragBeginScreenPos = nil
    self.m_rotateDragBeginDir = nil
    self.m_rotateDragCenterScreenPos = nil
end


SnapshotCtrl._OnDragMoveChar = HL.Method(HL.Any) << function(self, eventData)
    snapshotSystem:UpdateMoveChar(eventData.position)
end

SnapshotCtrl._RefreshMoveCharRotateButtonHover = HL.Method() << function(self)
    if DeviceInfo.usingController then
        return
    end

    if self.m_isKeyboardQuickRotateCharMode or self.m_moveCharRotate3DSlider == nil
        or self.m_moveCharRotate3DSlider.m_isDestroyed
        or self.m_moveCharRotate3DSlider.m_isRotating then
        return
    end

    local mousePos = InputManager.mousePosition
    if self.m_moveCharRotate3DSlider:IsRotateButtonScreenPosHit(Vector2(mousePos.x, mousePos.y)) then
        self.m_moveCharRotate3DSlider:PlayRotateButtonHoverAni()
    else
        self.m_moveCharRotate3DSlider:CancelRotateButtonHoverAni()
    end
end


SnapshotCtrl._OnFirstPersonChangedForMoveChar = HL.Method() << function(self)
    self:_RefreshAvatarSelectableChanged()
end


SnapshotCtrl._OnChangeCharShowModeForMoveChar = HL.Method() << function(self)
    self:_RefreshAvatarSelectableChanged()
end


SnapshotCtrl._RefreshAfterMoveSelectableChanged = HL.Method() << function(self)
    local curSlot = snapshotSystem:GetMoveSelectedSlotIndex()
    if curSlot >= 0 and not self:_IsFormationAvatarSelectable(curSlot) then
        if DeviceInfo.usingController then
            local nextSlot = self:_GetFirstSelectableSlot()
            if nextSlot >= 0 then
                self:_TrySelectMoveChar(nextSlot)
            else
                self:_ExitGamepadMoveRotateCharMode()
            end
        else
            self:_DeselectMoveChar()
        end
    end

    self:_RefreshFormationAvatarList()
    self:_RefreshQuickMoveRotateAvatarList()
end


SnapshotCtrl._RefreshFormationAvatarList = HL.Method() << function(self)
    if not self.m_formationAvatarCellCache then
        return
    end
    local count = self.m_squadCharList and #self.m_squadCharList or 0
    self.m_formationAvatarCellCache:Refresh(count, function(cell, luaIndex)
        cell.btn.onClick:RemoveAllListeners()
        cell.btn.onClick:AddListener(function()
            self:_OnClickFormationAvatarCell(luaIndex)
        end)
        self:_RefreshFormationAvatarCell(cell, luaIndex)
    end)
end


SnapshotCtrl._RefreshQuickMoveRotateAvatarList = HL.Method() << function(self)
    if not DeviceInfo.usingController or not self.m_quickAvatarCellCache then
        return
    end

    local count = self.m_squadCharList and #self.m_squadCharList or 0
    self.m_quickAvatarCellCache:Refresh(count, function(cell, luaIndex)
        cell.btn.onClick:RemoveAllListeners()
        cell.btn.onClick:AddListener(function()
            local info = self.m_squadCharList[luaIndex]
            if info then
                self:_TrySelectMoveChar(info.slotIndex)
            end
        end)
        self:_RefreshQuickMoveRotateAvatarCell(cell, luaIndex)
    end)
end


SnapshotCtrl._RefreshMoveAvatarCellsBySlot = HL.Method(HL.Number) << function(self, slotIndex)
    if slotIndex < 0 then
        return
    end
    local luaIndex = LuaIndex(slotIndex)
    self:_RefreshFormationAvatarCellByIndex(luaIndex)
    self:_RefreshQuickMoveRotateAvatarCellByIndex(luaIndex)
end


SnapshotCtrl._RefreshFormationAvatarCellByIndex = HL.Method(HL.Number) << function(self, luaIndex)
    if self.m_formationAvatarCellCache == nil then
        return
    end
    local cell = self.m_formationAvatarCellCache:Get(luaIndex)
    if cell ~= nil then
        self:_RefreshFormationAvatarCell(cell, luaIndex)
    end
end


SnapshotCtrl._RefreshQuickMoveRotateAvatarCellByIndex = HL.Method(HL.Number) << function(self, luaIndex)
    if self.m_quickAvatarCellCache == nil then
        return
    end
    local cell = self.m_quickAvatarCellCache:Get(luaIndex)
    if cell ~= nil then
        self:_RefreshQuickMoveRotateAvatarCell(cell, luaIndex)
    end
end


SnapshotCtrl._RefreshFormationAvatarCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local charInfo = self.m_squadCharList[luaIndex]
    if not charInfo then
        return
    end

    cell.avatarImg:LoadSprite(UIConst.UI_SPRITE_ROUND_CHAR_HEAD, UIConst.UI_ROUND_CHAR_HEAD_PREFIX .. charInfo.characterId)

    local selectable = self:_IsFormationAvatarSelectable(charInfo.slotIndex)
    cell.selectState:SetState(selectable and "Enable" or "Disable")

    local selectedSlot = snapshotSystem:GetMoveSelectedSlotIndex()
    if DeviceInfo.usingController then
        cell.selectState:SetState("Unselect")
    else
        cell.selectState:SetState(selectedSlot == charInfo.slotIndex and "Select" or "Unselect")
    end
end


SnapshotCtrl._RefreshQuickMoveRotateAvatarCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local charInfo = self.m_squadCharList[luaIndex]
    if not charInfo then
        return
    end

    cell.avatarImg:LoadSprite(UIConst.UI_SPRITE_ROUND_CHAR_HEAD, UIConst.UI_ROUND_CHAR_HEAD_PREFIX .. charInfo.characterId)

    local selectable = self:_IsFormationAvatarSelectable(charInfo.slotIndex)
    cell.selectState:SetState(selectable and "Enable" or "Disable")

    local selectedSlot = snapshotSystem:GetMoveSelectedSlotIndex()
    cell.selectState:SetState(selectedSlot == charInfo.slotIndex and "Select" or "Unselect")
end


SnapshotCtrl._SyncQuickMoveRotateNodeState = HL.Method() << function(self)
    local stateCtrl = self.view.quickMoveRotateCharNode.stateController
    if self:IsInGamepadMoveRotateMode() then
        stateCtrl:SetState("OpenQuickMoveRotate")
    else
        stateCtrl:SetState("CloseQuickMoveRotate")
    end
end






SnapshotCtrl._UpdateSquadCharList = HL.Method() << function(self)
    self.m_squadCharList = {}
    local slots = GameInstance.player.squadManager.curSquad.slots
    local count = slots.Count
    for i = 0, count - 1 do
        local slot = slots[i]
        table.insert(self.m_squadCharList, {
            slotIndex = i,
            characterId = slot.charId,
        })
    end
end


SnapshotCtrl._GetMainControlSquadAvatarIndex = HL.Method().Return(HL.Number) << function(self)
    local squadManager = GameInstance.player.squadManager
    local mainCharSlotIndex = squadManager:GetMemberIndex(GameInstance.playerController.mainCharacter)
    if mainCharSlotIndex >= 0 then
        return LuaIndex(mainCharSlotIndex)
    end
    return 1
end


SnapshotCtrl._SyncActionMenuAvatarToMoveChar = HL.Method() << function(self)
    if not self.m_isInitRefreshActionUI then
        return
    end
    local selectedSlot = snapshotSystem:GetMoveSelectedSlotIndex()
    if selectedSlot < 0 then
        return
    end
    for i, charInfo in ipairs(self.m_squadCharList) do
        if charInfo.slotIndex == selectedSlot then
            if self.m_curSelectAvatarIndex ~= i then
                self:_OnSelectAvatarChar(i)
            end
            break
        end
    end
end


SnapshotCtrl._UpdateActionListForChar = HL.Method(HL.Number) << function(self, avatarIndex)
    local charInfo = self.m_squadCharList[avatarIndex]
    if not charInfo then
        self.m_actionInfos = {}
        return
    end

    self.m_actionInfos = {}

    table.insert(self.m_actionInfos, {
        isEmpty = true,
        isUnlock = true,
        isNew = false,
        isStatic = true,
        sortId = math.mininteger,
    })

    local entries = snapshotSystem:GetActionsForCharacter(charInfo.characterId)
    if entries then
        for i = 0, entries.Count - 1 do
            local entry = entries[i]
            local actionId = entry.actionId
            local templateActionId = entry.templateActionId
            local _, actionCfg = Tables.snapshotActionTable:TryGetValue(templateActionId)
            if actionCfg then
                local itemId = actionCfg.itemId
                local _, itemCfg = Tables.itemTable:TryGetValue(itemId)
                local isUnlock = actionCfg.isDefaultUnlock or inventorySystem:IsItemGot(itemId)
                local canShow = isUnlock
                if not isUnlock and itemCfg then
                    canShow = Utils.isNotObtainCanShow(itemCfg.notObtainShow, itemCfg.notObtainShowTimeId)
                end
                if canShow then
                    local isNew = false
                    if isUnlock and not string.isEmpty(itemId) then
                        isNew = inventorySystem:IsNewItem(itemId)
                    end
                    local info = {
                        actionId = actionId,
                        name = actionCfg.name,
                        icon = actionCfg.icon,
                        desc = actionCfg.desc,
                        sourceText = actionCfg.sourceText,
                        jumpId = actionCfg.jumpId,
                        videoPath = actionCfg.videoPath,
                        rewardTaskId = actionCfg.rewardTaskId,
                        itemId = itemId,
                        isStatic = entry.isStatic,
                        characterId = entry.characterId,

                        isEmpty = false,
                        isUnlock = isUnlock,
                        isCanUnlock = false,
                        isNew = isNew,
                        sortId = actionCfg.sortId,
                    }
                    table.insert(self.m_actionInfos, info)
                end
            end
        end
    end


    table.sort(self.m_actionInfos, function(a, b)
        if a.isEmpty ~= b.isEmpty then
            return a.isEmpty
        end
        if a.isUnlock ~= b.isUnlock then
            return a.isUnlock
        end
        return a.sortId < b.sortId
    end)
end


SnapshotCtrl._OnSelectAvatarChar = HL.Method(HL.Number) << function(self, avatarIndex)
    local charInfo = self.m_squadCharList[avatarIndex]
    if not charInfo then
        return
    end

    self.m_curSelectAvatarIndex = avatarIndex
    self:_RefreshActionCellSelect(self.m_curSelectActionIndex, false)
    self:_RefreshActionAvatarList()
    self:_UpdateActionListForChar(avatarIndex)


    local defaultIndex = 1
    local playingId = snapshotSystem:GetPlayingActionId(charInfo.slotIndex)
    if playingId then
        for i, info in ipairs(self.m_actionInfos) do
            if info.actionId == playingId then
                defaultIndex = i
                break
            end
        end
    end
    self.m_curSelectActionIndex = defaultIndex
    self:_RefreshActionCellSelect(defaultIndex, true)
    self:_RefreshActionList()
    self:_RefreshActionInfoArea()
    self:_RefreshControllerPlayVideoBtn()


    if charInfo and not self.m_actionInfos[defaultIndex].isEmpty
        and self.m_actionInfos[defaultIndex].isUnlock
        and not self.m_actionInfos[defaultIndex].isStatic then
        if snapshotSystem:IsPlayingAction(charInfo.slotIndex) then
            self:_StartActionProgressUpdate()
        end
    else
        self:_StopActionProgressUpdate()
    end
end


SnapshotCtrl._OnSelectAction = HL.Method(HL.Number) << function(self, luaIndex)
    local oldIndex = self.m_curSelectActionIndex
    local info = self.m_actionInfos[luaIndex]
    if not info then
        return
    end


    if info.isNew then
        info.isNew = false
        if not string.isEmpty(info.itemId) then
            inventorySystem:ReadNewItem(info.itemId)
        end
        self.m_actionViewedNewSet[info.actionId] = nil
    end

    local keepCurActionVideo = oldIndex == luaIndex
        and self.view.videoNode.gameObject.activeSelf
        and self.m_curActionVideoPath == info.videoPath

    local charInfo = self.m_squadCharList[self.m_curSelectAvatarIndex]
    if not charInfo then
        return
    end
    local slotIndex = charInfo.slotIndex
    self.m_curSelectActionIndex = luaIndex


    if not keepCurActionVideo then
        self:_CloseActionVideo()
    end

    if info.isEmpty then
        snapshotSystem:StopAction(slotIndex)
    elseif info.isUnlock then

        local reasonKey = self:_GetActionUnselectableReasonKey(slotIndex)
        if reasonKey ~= nil then
            Notify(MessageConst.SHOW_TOAST, Language[reasonKey])
        else

            if not self:_EnsureCustomTeamFormation() then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_SNAPSHOT_ACTION_CANNOT_SET)
                self:_OnSelectAction(1)
                return
            end
            local playResult = snapshotSystem:PlayAction(slotIndex, info.actionId)
            self:_StopActionProgressUpdate()
            if playResult == 0 and not info.isStatic then
                self:_StartActionProgressUpdate()
            end
        end
    else

        if not keepCurActionVideo and not string.isEmpty(info.videoPath) then
            self:_ShowActionVideo(info.videoPath)
        end
    end


    self:_RefreshActionCellSelect(oldIndex, false)
    self:_RefreshActionCellSelect(luaIndex, true)


    self:_RefreshActionCellUseState()

    self:_RefreshActionInfoArea()
end


SnapshotCtrl._OnClickActionPlayBtn = HL.Method() << function(self)
    local charInfo = self.m_squadCharList[self.m_curSelectAvatarIndex]
    if not charInfo then
        return
    end
    local slotIndex = charInfo.slotIndex
    local info = self.m_actionInfos[self.m_curSelectActionIndex]
    if not info or info.isEmpty then
        return
    end

    local isPlayingSelectedAction = snapshotSystem:GetPlayingActionId(slotIndex) == info.actionId
    if not isPlayingSelectedAction then
        self:_OnSelectAction(self.m_curSelectActionIndex)
    elseif snapshotSystem:IsActionPaused(slotIndex) then
        snapshotSystem:ResumeAction(slotIndex)
        self:_RefreshActionPlayButton()
    else
        snapshotSystem:PauseAction(slotIndex)
        self:_RefreshActionPlayButton()
    end
end


SnapshotCtrl._OnClickActionJumpBtn = HL.Method() << function(self)
    local info = self.m_actionInfos[self.m_curSelectActionIndex]
    if not info then
        return
    end

    if not string.isEmpty(info.rewardTaskId) then
        UIManager:Open(PanelId.SnapshotRewardTask, {
            rewardTaskId = info.rewardTaskId,
            actionIsStatic = info.isStatic,
            characterId = info.characterId,
            titleLanguageKey = "LUA_SNAPSHOT_REWARD_TASK_TITLE_ACTION",
        })
        return
    end
    if not string.isEmpty(info.jumpId) then
        Utils.jumpToSystemWithItem(info.jumpId, nil, info.itemId)
    end
end


SnapshotCtrl._OnLeaveActionTab = HL.Method() << function(self)

    local readItemIds = {}
    for actionId, _ in pairs(self.m_actionViewedNewSet) do
        for _, info in pairs(self.m_actionInfos) do
            if info.actionId == actionId and not string.isEmpty(info.itemId) then
                table.insert(readItemIds, info.itemId)
                info.isNew = false
                break
            end
        end
    end
    if #readItemIds > 0 then
        inventorySystem:ReadNewItems(readItemIds)
    end
    self.m_actionViewedNewSet = {}
    self:_CloseActionVideo()
end


SnapshotCtrl._ToggleActionAvatarBindings = HL.Method(HL.Boolean) << function(self, enable)
    if self.m_actionPreAvatarBindingId > 0 then
        InputManagerInst:ToggleBinding(self.m_actionPreAvatarBindingId, enable)
    end
    if self.m_actionNextAvatarBindingId > 0 then
        InputManagerInst:ToggleBinding(self.m_actionNextAvatarBindingId, enable)
    end
end


SnapshotCtrl._StartActionProgressUpdate = HL.Method() << function(self)
    self:_StopActionProgressUpdate()
    local actionNode = self.view.menuContentNode.menuActionNode
    self.m_actionProgressCoroutine = self:_StartCoroutine(function()
        while true do
            local charInfo = self.m_squadCharList[self.m_curSelectAvatarIndex]
            if charInfo then
                local progress = snapshotSystem:GetActionProgress(charInfo.slotIndex)
                actionNode.playBtnNode.playProgressBar.fillAmount = progress
            end
            coroutine.step()
        end
    end)
end


SnapshotCtrl._StopActionProgressUpdate = HL.Method() << function(self)
    self.m_actionProgressCoroutine = self:_ClearCoroutine(self.m_actionProgressCoroutine)
    self.view.menuContentNode.menuActionNode.playBtnNode.playProgressBar.fillAmount = 0
end


SnapshotCtrl.onClickControllerPlayVideoBtn = HL.Method() << function(self)
    local isPlayingVideo = self.view.videoNode.gameObject.activeSelf and not string.isEmpty(self.m_curActionVideoPath)
    if isPlayingVideo then
        self:_CloseActionVideo()
        self:_RefreshControllerPlayVideoBtn()
        return
    end

    local info = self.m_actionInfos[self.m_curSelectActionIndex]
    if not info or info.isEmpty or string.isEmpty(info.videoPath) then
        return
    end
    self:_ShowActionVideo(info.videoPath)
    self:_RefreshControllerPlayVideoBtn()
end


SnapshotCtrl._OnActionCellNaviTargetChanged = HL.Method(HL.Number, HL.Boolean) << function(self, luaIndex, isTarget)
    if not isTarget then
        return
    end
    local oldIndex = self.m_curSelectActionIndex
    if oldIndex ~= luaIndex then
        self.m_curSelectActionIndex = luaIndex
        self:_RefreshActionCellSelect(oldIndex, false)
        self:_RefreshActionCellSelect(luaIndex, true)
    end
    self:_RefreshActionInfoArea()
    self:_RefreshActionProgressForSelectedAction()
    self:_RefreshControllerPlayVideoBtn()
end


SnapshotCtrl._RefreshActionProgressForSelectedAction = HL.Method() << function(self)
    self:_StopActionProgressUpdate()
    local actionNode = self.view.menuContentNode.menuActionNode
    actionNode.playBtnNode.playProgressBar.fillAmount = 0
    local charInfo = self.m_squadCharList[self.m_curSelectAvatarIndex]
    local info = self.m_actionInfos[self.m_curSelectActionIndex]
    if not charInfo or not info or info.isEmpty or not info.isUnlock or info.isStatic then
        return
    end
    if snapshotSystem:IsPlayingAction(charInfo.slotIndex)
        and snapshotSystem:GetPlayingActionId(charInfo.slotIndex) == info.actionId then
        self:_StartActionProgressUpdate()
    end
end


SnapshotCtrl._RefreshControllerPlayVideoBtn = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end
    local actionNode = self.view.menuContentNode.menuActionNode
    local info = self.m_actionInfos[self.m_curSelectActionIndex]
    local hasVideo = info and not info.isEmpty and not string.isEmpty(info.videoPath)
    local isPlayingVideo = self.view.videoNode.gameObject.activeSelf and not string.isEmpty(self.m_curActionVideoPath)
    actionNode.controllerPlayVideoBtn.gameObject:SetActive(hasVideo or isPlayingVideo)
    if not hasVideo and not isPlayingVideo then
        return
    end
    local bindingId = actionNode.controllerPlayVideoBtn.hoverConfirmBindingId
    if bindingId <= 0 then
        bindingId = actionNode.controllerPlayVideoBtn.onClick.bindingId
    end
    if bindingId <= 0 then
        return
    end
    InputManagerInst:SetBindingText(
        bindingId,
        isPlayingVideo and Language.LUA_SNAPSHOT_ACTION_CLOSE_VIDEO_HINT_TEXT
            or Language.LUA_SNAPSHOT_ACTION_OPEN_VIDEO_HINT_TEXT
    )
end


SnapshotCtrl._ShowActionVideo = HL.Method(HL.String) << function(self, videoPath)
    local videoExist, playPath = UIUtils.getUIVideoFullPath(UIConst.UI_VIDEO_SNAPSHOT .. videoPath)
    if not videoExist then
        logger.error("拍照动作视频不存在！videoPath：", videoPath)
        return
    end
    local videoNode = self.view.videoNode
    self.m_curActionVideoPath = videoPath
    videoNode.animationWrapper:ClearTween(false)
    videoNode.gameObject:SetActive(true)
    self:_PlayActionVideo(playPath)
    self:_StartVideoProgressUpdate()
    self:_RefreshControllerPlayVideoBtn()
end

SnapshotCtrl._PlayActionVideo = HL.Method(HL.String) << function(self, playVideoPath)

    self.view.videoNode.videoPlayer:PlayVideo(playVideoPath)
    self.view.videoNode.videoPlayer:SetLoop(true)
end


SnapshotCtrl._CloseActionVideo = HL.Method() << function(self)
    local videoNode = self.view.videoNode
    videoNode.videoPlayer:StopVideo()
    if videoNode.gameObject.activeSelf then
        videoNode.animationWrapper:ClearTween(false)
        videoNode.animationWrapper:PlayOutAnimation(function()
            videoNode.gameObject:SetActive(false)
        end)
    end
    self.m_curActionVideoPath = ""
    self:_StopVideoProgressUpdate()
    self:_RefreshControllerPlayVideoBtn()
end


SnapshotCtrl._StartVideoProgressUpdate = HL.Method() << function(self)
    self:_StopVideoProgressUpdate()
    local videoNode = self.view.videoNode
    videoNode.progressBar.interactable = false
    self.m_videoProgressCoroutine = self:_StartCoroutine(function()
        while true do
            local totalTime = videoNode.videoPlayer:GetVideoTotalTime()
            if totalTime > 0 then
                videoNode.progressBar.value = videoNode.videoPlayer:GetTime() % totalTime / totalTime
            end
            coroutine.step()
        end
    end)
end


SnapshotCtrl._StopVideoProgressUpdate = HL.Method() << function(self)
    self.m_videoProgressCoroutine = self:_ClearCoroutine(self.m_videoProgressCoroutine)
end


SnapshotCtrl._OnActionForceReset = HL.Method() << function(self)
    self.m_curSelectActionIndex = 1
    self:_StopActionProgressUpdate()
    self:_CloseActionVideo()
    self:_RefreshActionList()
    self:_RefreshActionInfoArea()
end


SnapshotCtrl._OnMainControlActionInterrupted = HL.Method(HL.Number) << function(self, slotIndex)
    if not self.m_isInitRefreshActionUI then
        return
    end

    local interruptedAvatarIndex = 0
    for avatarIndex, charInfo in ipairs(self.m_squadCharList) do
        if charInfo.slotIndex == slotIndex then
            interruptedAvatarIndex = avatarIndex
            break
        end
    end
    if interruptedAvatarIndex <= 0 or interruptedAvatarIndex ~= self.m_curSelectAvatarIndex then
        return
    end


    self:_StopActionProgressUpdate()
    self:_CloseActionVideo()
    self:_RefreshActionCellSelect(self.m_curSelectActionIndex, false)
    self.m_curSelectActionIndex = 1
    self:_RefreshActionCellSelect(self.m_curSelectActionIndex, true)
    self:_RefreshActionCellUseState()
    self:_RefreshActionInfoArea()
end


SnapshotCtrl._RefreshActionAvatarList = HL.Method() << function(self)
    local count = #self.m_squadCharList
    self.m_actionAvatarCellCache:Refresh(count, function(cell, luaIndex)
        cell.btn.onClick:RemoveAllListeners()
        cell.btn.onClick:AddListener(function()
            self:_OnSelectAvatarChar(luaIndex)
        end)
        local charInfo = self.m_squadCharList[luaIndex]
        if charInfo then
            cell.avatarImg:LoadSprite(UIConst.UI_SPRITE_ROUND_CHAR_HEAD, UIConst.UI_ROUND_CHAR_HEAD_PREFIX .. charInfo.characterId)
        end
        cell.selectState:SetState("Enable")
        cell.selectState:SetState(luaIndex == self.m_curSelectAvatarIndex and "Select" or "Unselect")
    end)
end


SnapshotCtrl._RefreshActionCellSelect = HL.Method(HL.Number, HL.Boolean) << function(self, luaIndex, isSelect)
    if luaIndex <= 0 then
        return
    end
    local actionNode = self.view.menuContentNode.menuActionNode
    local obj = actionNode.actionList:Get(CSIndex(luaIndex))
    if not obj then
        return
    end
    local cell = self.m_getActionCellFunc(obj)
    if not cell then
        return
    end
    local info = self.m_actionInfos[luaIndex]
    if not info then
        return
    end
    if isSelect and not DeviceInfo.usingController then
        cell.actionState:SetState("Select")
    else
        cell.actionState:SetState("UnSelect")
    end
end


SnapshotCtrl._RefreshActionCellUseState = HL.Method() << function(self)
    local charInfo = self.m_squadCharList[self.m_curSelectAvatarIndex]
    if not charInfo then
        return
    end
    local slotIndex = charInfo.slotIndex
    local playingId = snapshotSystem:GetPlayingActionId(slotIndex)
    local actionNode = self.view.menuContentNode.menuActionNode
    for i, info in ipairs(self.m_actionInfos) do
        local obj = actionNode.actionList:Get(CSIndex(i))
        if obj then
            local cell = self.m_getActionCellFunc(obj)
            if cell then
                if info.isEmpty then
                    cell.useNode.gameObject:SetActive(not snapshotSystem:IsPlayingAction(slotIndex))
                else
                    cell.useNode.gameObject:SetActive(playingId == info.actionId)
                end
            end
        end
    end
end


SnapshotCtrl._RefreshActionList = HL.Method() << function(self)
    local actionNode = self.view.menuContentNode.menuActionNode
    actionNode.actionList:UpdateCount(#self.m_actionInfos, true)
end

SnapshotCtrl._GetActionRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, csIndex)
    local info = self.m_actionInfos[LuaIndex(csIndex)]
    if not info or string.isEmpty(info.itemId) then
        return 0
    end

    local hasRedDot, redDotType = RedDotManager:GetRedDotState("SnapshotActionItem", info.itemId)
    if hasRedDot then
        return redDotType
    end
    return 0
end


SnapshotCtrl._OpenSnapshotInfoJump = HL.Method(HL.Any, HL.Boolean) << function(self, info, isFilter)
    if not info then
        return
    end
    if not string.isEmpty(info.rewardTaskId) then
        UIManager:Open(PanelId.SnapshotRewardTask, {
            rewardTaskId = info.rewardTaskId,
            titleLanguageKey = isFilter and "LUA_SNAPSHOT_REWARD_TASK_TITLE_FILTER" or "LUA_SNAPSHOT_REWARD_TASK_TITLE_STICKER",
        })
        return
    end
    if not string.isEmpty(info.jumpId) then
        Utils.jumpToSystemWithItem(info.jumpId, nil, info.itemId)
    end
end


SnapshotCtrl._OnClickFilterJumpBtn = HL.Method() << function(self)
    local index = self.m_curSelectFilterIndex > 0 and self.m_curSelectFilterIndex or self.m_curUsedFilterIndex
    local info = self.m_filterInfos[index]
    self:_OpenSnapshotInfoJump(info, true)
end


SnapshotCtrl._OnClickStickerJumpBtn = HL.Method() << function(self)
    local index = self.m_curSelectStickerIndex > 0 and self.m_curSelectStickerIndex or self.m_curUsedStickerIndex
    local info = self.m_stickerInfos[index]
    self:_OpenSnapshotInfoJump(info, false)
end


SnapshotCtrl._RefreshFilterInfoArea = HL.Method() << function(self)
    local filterNode = self.view.menuContentNode.menuFilterNode
    local index = self.m_curSelectFilterIndex > 0 and self.m_curSelectFilterIndex or self.m_curUsedFilterIndex
    local info = self.m_filterInfos[index]
    self:_RefreshSnapshotMenuInfoArea(
        filterNode.menuDownInfoNode,
        info,
        Language.LUA_SNAPSHOT_FILTER_EMPTY_NAME,
        Language.LUA_SNAPSHOT_FILTER_EMPTY_DESC)
end


SnapshotCtrl._RefreshStickerInfoArea = HL.Method() << function(self)
    local stickerNode = self.view.menuContentNode.menuStickerNode
    local index = self.m_curSelectStickerIndex > 0 and self.m_curSelectStickerIndex or self.m_curUsedStickerIndex
    local info = self.m_stickerInfos[index]
    self:_RefreshSnapshotMenuInfoArea(
        stickerNode.menuDownInfoNode,
        info,
        Language.LUA_SNAPSHOT_STICKER_EMPTY_NAME,
        Language.LUA_SNAPSHOT_STICKER_EMPTY_DESC)
end


SnapshotCtrl._RefreshSnapshotMenuInfoArea = HL.Method(HL.Any, HL.Any, HL.String, HL.String) << function(self, infoNode, info, emptyName, emptyDesc)
    if not info then
        infoNode.nameTxt.gameObject:SetActive(false)
        infoNode.descTxt.gameObject:SetActive(false)
        infoNode.jumpBtnNode.gameObject:SetActive(false)
        return
    end

    if info.isEmpty then
        infoNode.nameTxt.gameObject:SetActive(true)
        infoNode.nameTxt.text = emptyName
        infoNode.descTxt.gameObject:SetActive(true)
        infoNode.descTxt.text = emptyDesc
        infoNode.jumpBtnNode.gameObject:SetActive(false)
        return
    end

    infoNode.nameTxt.gameObject:SetActive(true)
    infoNode.nameTxt.text = info.name or ""

    if info.isUnlock then
        infoNode.descTxt.gameObject:SetActive(not string.isEmpty(info.desc))
        infoNode.descTxt.text = info.desc or ""
        infoNode.jumpBtnNode.gameObject:SetActive(false)
        return
    end

    if not string.isEmpty(info.rewardTaskId) or not string.isEmpty(info.jumpId) then
        infoNode.descTxt.gameObject:SetActive(false)
        infoNode.jumpBtnNode.gameObject:SetActive(true)
        infoNode.jumpBtnNode.jumpBtnTxt.text = info.sourceText or ""
    else
        infoNode.descTxt.gameObject:SetActive(not string.isEmpty(info.sourceText))
        infoNode.descTxt.text = info.sourceText or ""
        infoNode.jumpBtnNode.gameObject:SetActive(false)
    end
end


SnapshotCtrl._RefreshActionInfoArea = HL.Method() << function(self)
    local actionNode = self.view.menuContentNode.menuActionNode
    local info = self.m_actionInfos[self.m_curSelectActionIndex]

    if not info then
        actionNode.actionNameTxt.gameObject:SetActive(false)
        actionNode.actionDescTxt.gameObject:SetActive(false)
        actionNode.jumpBtnNode.gameObject:SetActive(false)
        actionNode.playBtnNode.gameObject:SetActive(false)
        return
    end

    if info.isEmpty then
        actionNode.actionNameTxt.gameObject:SetActive(true)
        actionNode.actionNameTxt.text = Language.LUA_SNAPSHOT_ACTION_EMPTY_NAME
        actionNode.actionDescTxt.gameObject:SetActive(true)
        actionNode.actionDescTxt.text = Language.LUA_SNAPSHOT_ACTION_EMPTY_DESC
        actionNode.jumpBtnNode.gameObject:SetActive(false)
        actionNode.playBtnNode.gameObject:SetActive(false)
        return
    end

    actionNode.actionNameTxt.gameObject:SetActive(true)
    actionNode.actionNameTxt.text = info.name

    if info.isUnlock then
        actionNode.actionDescTxt.gameObject:SetActive(info.desc and info.desc ~= "")
        actionNode.actionDescTxt.text = info.desc or ""
        actionNode.jumpBtnNode.gameObject:SetActive(false)
        if info.isStatic then
            actionNode.playBtnNode.gameObject:SetActive(false)
        else
            actionNode.playBtnNode.gameObject:SetActive(true)
            self:_RefreshActionPlayButton()
        end
    else
        if not string.isEmpty(info.rewardTaskId) or not string.isEmpty(info.jumpId) then
            actionNode.actionDescTxt.gameObject:SetActive(false)
            actionNode.jumpBtnNode.gameObject:SetActive(true)
            actionNode.jumpBtnNode.jumpBtnTxt.text = info.sourceText or ""
        else
            actionNode.actionDescTxt.gameObject:SetActive(info.sourceText and info.sourceText ~= "")
            actionNode.actionDescTxt.text = info.sourceText or ""
            actionNode.jumpBtnNode.gameObject:SetActive(false)
        end
        actionNode.playBtnNode.gameObject:SetActive(false)
    end
end


SnapshotCtrl._RefreshActionPlayButton = HL.Method() << function(self)
    local actionNode = self.view.menuContentNode.menuActionNode
    local charInfo = self.m_squadCharList[self.m_curSelectAvatarIndex]
    local info = self.m_actionInfos[self.m_curSelectActionIndex]
    local isPlayingSelectedAction = false
    if charInfo and info and not info.isEmpty then
        isPlayingSelectedAction = snapshotSystem:GetPlayingActionId(charInfo.slotIndex) == info.actionId
    end
    local playState = (isPlayingSelectedAction and not snapshotSystem:IsActionPaused(charInfo.slotIndex)) and "Play" or "Pause"
    actionNode.playBtnNode.stateController:SetState(playState)
    Notify(MessageConst.REFRESH_CONTROLLER_HINT)
end


SnapshotCtrl._RefreshActionCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local info = self.m_actionInfos[luaIndex]
    if not info then
        return
    end

    local charInfo = self.m_squadCharList[self.m_curSelectAvatarIndex]
    local slotIndex = charInfo and charInfo.slotIndex or 0


    cell.actionBtn.onLongPress:RemoveAllListeners()
    cell.showTipsBtn.onClick:RemoveAllListeners()
    cell.actionBtn.onIsNaviTargetChanged = nil

    cell.actionState:SetState(luaIndex == self.m_curSelectActionIndex and not DeviceInfo.usingController and "Select" or "UnSelect")
    if info.isEmpty then
        cell.actionState:SetState("Empty")
        cell.iconImg.gameObject:SetActive(false)
        cell.nameTxt.text = Language.LUA_SNAPSHOT_ACTION_EMPTY_NAME
        cell.lockState:SetState("Unlock")
        cell.redDot:Stop()
        cell.useNode.gameObject:SetActive(not snapshotSystem:IsPlayingAction(slotIndex))
        cell.notStaticNode.gameObject:SetActive(false)
        cell.showTipsBtn.gameObject:SetActive(false)
    else
        cell.actionState:SetState("Normal")
        cell.iconImg.gameObject:SetActive(true)
        cell.iconImg:LoadSprite(UIConst.UI_SPRITE_SNAPSHOT_ACTION, info.icon)
        cell.nameTxt.text = info.name


        if info.isUnlock then
            cell.lockState:SetState("Unlock")
        elseif info.isCanUnlock then
            cell.lockState:SetState("LockButCanUnlock")
        else
            cell.lockState:SetState("Lock")
        end


        if not string.isEmpty(info.itemId) then
            cell.redDot:InitRedDot("SnapshotActionItem", info.itemId, nil, self.view.menuContentNode.menuActionNode.redDotScrollRect)
        else
            cell.redDot:Stop()
        end

        if info.isNew then
            self.m_actionViewedNewSet[info.actionId] = true
        end


        local playingId = snapshotSystem:GetPlayingActionId(slotIndex)
        cell.useNode.gameObject:SetActive(playingId == info.actionId)


        cell.notStaticNode.gameObject:SetActive(not info.isStatic)
    end


    if not info.isEmpty and not string.isEmpty(info.itemId) then
        cell.actionBtn.onLongPress:AddListener(function()
            Notify(MessageConst.SHOW_ITEM_TIPS, {
                transform = cell.transform,
                safeArea = cell.transform,
                posType = UIConst.UI_TIPS_POS_TYPE.RightDown,
                itemId = info.itemId,
            })
        end)
        cell.showTipsBtn.gameObject:SetActive(false)
        if DeviceInfo.usingController then
            cell.showTipsBtn.onClick:AddListener(function()
                Notify(MessageConst.SHOW_ITEM_TIPS, {
                    transform = cell.transform,
                    posType = UIConst.UI_TIPS_POS_TYPE.RightDown,
                    itemId = info.itemId,
                })
            end)
        end
    else
        cell.showTipsBtn.gameObject:SetActive(false)
    end
    if DeviceInfo.usingController then
        cell.actionBtn.onIsNaviTargetChanged = function(isTarget)
            if not info.isEmpty and not string.isEmpty(info.itemId) then
                cell.showTipsBtn.gameObject:SetActive(isTarget)
            end
            self:_OnActionCellNaviTargetChanged(luaIndex, isTarget)
        end
    end

    cell.actionBtn.onClick:RemoveAllListeners()
    cell.actionBtn.onClick:AddListener(function()
        self:_OnSelectAction(luaIndex)
    end)
end


SnapshotCtrl._RefreshAfterActionSelectableChanged = HL.Method(HL.Opt(HL.Boolean)) << function(self, refreshActionList)
    if not self.m_isInitRefreshActionUI then
        return
    end

    if refreshActionList then
        self:_StopActionProgressUpdate()
        self:_CloseActionVideo()
        local count = #self.m_squadCharList
        if count <= 0 then
            self.m_curSelectAvatarIndex = 0
            self.m_curSelectActionIndex = 0
            self.m_actionInfos = {}
            self:_RefreshActionAvatarList()
            self:_RefreshActionList()
            self:_RefreshActionInfoArea()
            self:_RefreshControllerPlayVideoBtn()
            return
        end

        local avatarIndex = lume.clamp(math.max(self.m_curSelectAvatarIndex, 1), 1, count)
        self:_OnSelectAvatarChar(avatarIndex)
        return
    end

    self:_RefreshActionAvatarList()
end



SnapshotCtrl._ChangeFilter = HL.Method(HL.Number) << function(self, luaIndex)
    local oldIndex = self.m_curUsedFilterIndex
    local oldInfo = self.m_filterInfos[oldIndex]
    local info = self.m_filterInfos[luaIndex]
    self.m_curUsedFilterIndex = luaIndex
    if self.m_curSelectFilterIndex ~= luaIndex then
        self:_SelectFilterCell(luaIndex)
    end
    local filterContainer = self.view.filterContainer

    if oldIndex > 0 and not oldInfo.isEmpty then

        if oldInfo.filterObj ~= nil then
            oldInfo.filterObj:SetActive(false)
        end

        if oldInfo.effectInst ~= nil and oldInfo.effectInst:Lock() ~= nil then
            oldInfo.effectInst:Lock():SetVisible(false)
        end
    end

    if not info.isEmpty then

        if info.filterObj ~= nil then
            info.filterObj:SetActive(true)
        else
            if not string.isEmpty(info.filterPath) then
                local path = string.format(UIConst.SNAPSHOT_FILTER_VOLUME_PATH, info.filterPath)
                local prefab = self.loader:LoadGameObject(path)
                info.filterObj = CSUtils.CreateObject(prefab, filterContainer.filterVolume)
                info.filterObj:SetActive(true)
            end
        end

        if info.effectInst ~= nil and info.effectInst:Lock() ~= nil then
            info.effectInst:Lock():SetVisible(true)
        else
            if not string.isEmpty(info.effectPath) then

                local transform = snapshotSystem.camController.cameraTrans
                info.effectInst = GameInstance.effectManager:CreateStationaryEffect(
                    info.effectPath,
                    transform.position,
                    transform.rotation
                )
            end
        end
    end
end



SnapshotCtrl._ChangeSticker = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, luaIndex, isInit)
    self.m_curUsedStickerIndex = luaIndex
    if self.m_curSelectStickerIndex ~= luaIndex then
        self:_SelectStickerCell(luaIndex)
    end
    local info = self.m_stickerInfos[luaIndex]
    local stickerImg = self.view.stickerImg
    if info.isEmpty then
        self.view.stickerTouchPlate.transform.anchoredPosition = Vector2.zero
        self.view.stickerImg.rectTransform.anchoredPosition = Vector2.zero
        self.view.stickerTouchPlate.gameObject:SetActive(false)
        stickerImg.gameObject:SetActive(false)
        self:_EnableStickerEditMode(false, isInit)
    else
        self.view.stickerTouchPlate.gameObject:SetActive(true)
        if not isInit then
            self:_EnableStickerEditMode(true, false)
        end
        if not DeviceInfo.usingController then
            self.view.stickerMoveHint.gameObject:SetActive(false)
        end
        stickerImg.gameObject:SetActive(true)
        self.view.stickerImgAnimationWrapper:Play("stickertouchimg_in")
        stickerImg:LoadSprite(UIConst.UI_SPRITE_SNAPSHOT_STICKER, info.icon)
        local size = stickerImg.sprite.rect.size * UIManager.uiCanvasRect.rect.size.y / 1080
        stickerImg.rectTransform:SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x)
        stickerImg.rectTransform:SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y)
        self.view.stickerTouchPlate.transform:SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x)
        self.view.stickerTouchPlate.transform:SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y)
    end
end

SnapshotCtrl._EnableStickerEditMode = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, enable, isInit)
    self:_SwitchSnapshotUIVisible(not enable, isInit)
    self.m_phase:SetForbidJoystick(enable, "stickerMode")
    if enable then
        if self.m_editStickerCtrl == nil then
            self.m_editStickerCtrl = UIManager:Open(PanelId.SnapshotControllerEditSticker, function()
                self:_EnableStickerEditMode(false)
                self.m_editStickerCtrl:Hide()
            end)
        else
            self.m_editStickerCtrl:Show()
        end
    else
        if self.m_editStickerCtrl ~= nil then
            self.m_editStickerCtrl:Hide()
        end
    end
    self.m_inStickerEditMode = enable
    self.view.stickerSurroundBoxImg.enabled = enable
    self.view.stickerTouchPlate.gameObject:SetActive(enable)
    self.view.stickerTouchPlateNoEdit.enabled = not enable
    self.view.stickerMoveHint.gameObject:SetActive(DeviceInfo.usingController and enable)
end

SnapshotCtrl._OnDragSticker = HL.Method(CS.UnityEngine.EventSystems.PointerEventData) << function(self, eventData)
    local newScreenPos = UIUtils.screenPointToUI(eventData.position, self.uiCamera, self.view.rectTransform)
    self:_SetStickerNewPos(newScreenPos)
end

SnapshotCtrl._SetStickerNewPos = HL.Method(Vector2) << function(self, newPos)

    local touchPlate = self.view.stickerTouchPlate.transform

    local stickerHalfSize = touchPlate.rect.size / 2
    local minPos = UIManager.uiCanvasRect.offsetMin + stickerHalfSize
    local maxPos = UIManager.uiCanvasRect.offsetMax - stickerHalfSize
    local finalPos = Vector2(lume.clamp(newPos.x, minPos.x, maxPos.x), lume.clamp(newPos.y, minPos.y, maxPos.y))
    touchPlate.anchoredPosition = finalPos
    self.view.stickerImg.rectTransform.anchoredPosition = finalPos
end

SnapshotCtrl._OnClickMenuStickerCell = HL.Method(HL.Number, HL.Any) << function(self, newIndex, newCell)
    local info = self.m_stickerInfos[newIndex]
    local oldIndex = self.m_curUsedStickerIndex

    if oldIndex == newIndex and not info.isEmpty then
        if not self.m_inStickerEditMode then
            self:_EnableStickerEditMode(true)
        end
        return
    end

    if oldIndex > 0 then
        local oldObj = self.view.menuContentNode.menuStickerNode.stickerList:Get(CSIndex(oldIndex))
        local oldCell = self.m_getStickerCellFunc(oldObj)
        if oldCell then
            oldCell.useStateCtrl:SetState("Unused")
            oldCell.editorStateCtrl:SetState("NoEditor")
            InputManagerInst:SetBindingText(oldCell.btn.hoverConfirmBindingId, Language.LUA_SNAPSHOT_STICKER_SELECT_HINT_TEXT)
        end
    end

    newCell.useStateCtrl:SetState("Used")
    if not info.isEmpty then
        newCell.editorStateCtrl:SetState("Editor")
        InputManagerInst:SetBindingText(newCell.btn.hoverConfirmBindingId, Language.LUA_SNAPSHOT_STICKER_MOVE_HINT_TEXT)
    end

    self:_ChangeSticker(newIndex)
end







SnapshotCtrl._SwitchAllWorldUIActive = HL.Method(HL.Boolean) << function(self, isActive)
    UIManager.worldObjectRoot.gameObject:SetActive(isActive)
end

SnapshotCtrl._ShowTip = HL.Method(HL.Opt(HL.String)) << function(self, content)
    if not content then
        self.view.tipNodeAnimationWrapper:Play("tipnode_out", function()
            self.view.tipNode.gameObject:SetActive(false)
        end)
        return
    end
    self.view.tipNodeAnimationWrapper:ClearTween(false)
    self.view.tipNode.gameObject:SetActive(true)
    self.view.tipNodeAnimationWrapper:Play("tipnode_in")
    self.view.tipTxt.text = content
end

SnapshotCtrl._MoveCamera = HL.Method(CS.UnityEngine.EventSystems.PointerEventData) << function(self, eventData)
    local delta = eventData.delta
    if UNITY_EDITOR and DeviceInfo.usingTouch then


        if math.abs(delta.x) > 500 or math.abs(delta.y) > 300 then
            return
        end
    end

    if DeviceInfo.usingKeyboard then



        if delta.x == 0 then
            delta.x = InputManagerInst:GetAxis("Mouse X") * 0.9
        end
        if delta.y == 0 then
            delta.y = InputManagerInst:GetAxis("Mouse Y") * 0.63
        end
    end

    local deltaX = UIUtils.getNormalizedScreenX(delta.x)
    local deltaY = UIUtils.getNormalizedScreenY(delta.y)
    self.m_cameraCtrl:SurroundMoveCamera(deltaX, deltaY)
end

SnapshotCtrl._ClearFilter = HL.Method() << function(self)
    for _, info in pairs(self.m_filterInfos) do
        if info.filterObj ~= nil then
            GameObject.Destroy(info.filterObj)
            info.filterObj = nil
        end
        if info.effectInst ~= nil and info.effectInst:Lock() ~= nil then
            info.effectInst:Lock():Finish()
            info.effectInst = nil
        end
    end
end


SnapshotCtrl._OnBattleSquadChanged = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    self.m_isSquadDirty = true

    self:_RefreshManualFocusDropDown()
    local newSquadCount = GameInstance.player.squadManager.curSquad.slots.Count
    local dropDown = self.view.menuContentNode.menuBasicNode.manualFocusDropDownNode.manualFocusDropDown
    if self.m_isManualFocus then
        dropDown:SetSelected(0)
    elseif self.m_focusCharSlotIndex >= 0 and self.m_focusCharSlotIndex < newSquadCount then
        dropDown:SetSelected(self.m_focusCharSlotIndex + 1)
    else
        self:_OnManualFocusDropDownChanged(1)
    end

    self:_UpdateSquadCharList()

    snapshotSystem:ResetAllCustomActions()

    self:_DeselectMoveChar()
    self:_RefreshAvatarSelectableChanged(true)
end


SnapshotCtrl._IsCharHiddenByShowMode = HL.Method(HL.Number).Return(HL.Boolean) << function(self, slotIndex)
    local cfg = showCharConfig[self.m_curShowCharIndex or 1]
    if not cfg then
        return false
    end
    local mainCharSlot = GameInstance.player.squadManager:GetMemberIndex(GameInstance.playerController.mainCharacter)
    if slotIndex == mainCharSlot then
        return not cfg.showLeader
    end
    return not cfg.showTeamMate
end


SnapshotCtrl._GetFormationUnselectableReasonKey = HL.Method(HL.Number).Return(HL.Opt(HL.String)) << function(self, slotIndex)
    if Utils.isInFight() or self:_IsForbid(self.m_forbidRecords.switchFormation) then
        return "LUA_SNAPSHOT_FORBID_COMMON_TOAST"
    end
    if Utils.isForbidden(ForbidType.ForbidMove) then
        return "LUA_SNAPSHOT_FORBID_PLAYER_MOVE"
    end
    if self:_IsMoveCharDead(slotIndex) then
        return "LUA_SNAPSHOT_MOVE_CHAR_DEAD"
    end
    local mainCharSlot = GameInstance.player.squadManager:GetMemberIndex(GameInstance.playerController.mainCharacter)
    if slotIndex == mainCharSlot and Utils.isForbidden(ForbidType.ForbidSnapshotMainCharDragMoveRotate) then
        return "LUA_SNAPSHOT_FORBID_PLAYER_MOVE"
    end
    if snapshotSystem.isFirstPersonMode and slotIndex == mainCharSlot then
        return "LUA_SNAPSHOT_MOVE_CHAR_FIRST_PERSON_FORBID"
    end
    if self:_IsCharHiddenByShowMode(slotIndex) then
        return "LUA_SNAPSHOT_MOVE_CHAR_HIDDEN"
    end
    return nil
end


SnapshotCtrl._IsMoveCharDead = HL.Method(HL.Number).Return(HL.Boolean) << function(self, slotIndex)
    local entity = GameInstance.player.squadManager:GetMemberBySlot(slotIndex)
    return entity == nil or entity.abilityCom == nil or entity.abilityCom.alive == false
end

SnapshotCtrl._IsFormationAvatarSelectable = HL.Method(HL.Number).Return(HL.Boolean) << function(self, slotIndex)
    return self:_GetFormationUnselectableReasonKey(slotIndex) == nil
end


SnapshotCtrl._GetActionUnselectableReasonKey = HL.Method(HL.Number).Return(HL.Opt(HL.String)) << function(self, slotIndex)
    if Utils.isInFight() or self:_IsForbid(self.m_forbidRecords.action) or Utils.isForbidden(ForbidType.ForbidMove) then
        return "LUA_SNAPSHOT_FORBID_COMMON_TOAST"
    end
    if self:_IsMoveCharDead(slotIndex) then
        return "LUA_SNAPSHOT_ACTION_CANNOT_SET"
    end
    return nil
end

SnapshotCtrl._IsActionAvatarSelectable = HL.Method(HL.Number).Return(HL.Boolean) << function(self, slotIndex)
    return self:_GetActionUnselectableReasonKey(slotIndex) == nil
end

SnapshotCtrl._DrawMoveCharRotateButtonHitGizmos = HL.Method() << function(self)
    local slider = self.m_moveCharRotate3DSlider
    if slider == nil or slider.m_isDestroyed then
        return
    end

    local isValid, center, size, screenDepth = slider:GetRotateButtonHitScreenData()
    if not isValid then
        return
    end

    snapshotSystem:DrawMoveCharRotateButtonHitGizmos(center, size, screenDepth)
end


SnapshotCtrl._RefreshAvatarSelectableChanged = HL.Method(HL.Opt(HL.Boolean)) << function(self, refreshActionList)
    self:_RefreshAfterMoveSelectableChanged()
    self:_RefreshAfterActionSelectableChanged(refreshActionList)
end


SnapshotCtrl._SetForbid = HL.Method(HL.Table, HL.Boolean, HL.String) << function(self, forbidRecord, isForbid, key)
    local preForbid = next(forbidRecord.forbidKeys) ~= nil
    if isForbid then
        forbidRecord.forbidKeys[key] = true
    else
        forbidRecord.forbidKeys[key] = nil
    end
    local nowForbid = next(forbidRecord.forbidKeys) ~= nil
    if nowForbid ~= preForbid then
        self[forbidRecord.forbidFuncName](self, nowForbid)
    end
end

SnapshotCtrl._IsForbid = HL.Method(HL.Table).Return(HL.Boolean) << function(self, forbidRecord)
    return next(forbidRecord.forbidKeys) ~= nil
end

SnapshotCtrl._ShowForbidToast = HL.Method(HL.Table) << function(self, forbidRecord)
    if forbidRecord.forbidKeys[FIRST_PERSON_FORBID_KEY] then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SNAPSHOT_FIRST_PERSON_MODE_FORBID_TOAST)
    else
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SNAPSHOT_FORBID_COMMON_TOAST)
    end
end

SnapshotCtrl._ForbidSwitchMoveMode = HL.Method(HL.Boolean) << function(self, isForbid)
    self.view.switchMoveModeTog.interactable = not isForbid
    self.view.hintNode.switchMoveModeNode.gameObject:SetActive(not isForbid)
end

SnapshotCtrl._ForbidHideChar = HL.Method(HL.Boolean) << function(self, isForbid)
    local basicNode = self.view.menuContentNode.menuBasicNode
    if isForbid then
        basicNode.showCharDropDown:SetSelected(0)
    end
    basicNode.showCharDropDown.interactable = not isForbid
    basicNode.showCharDropDownForbidToastBtn.gameObject:SetActive(isForbid)
end

SnapshotCtrl._ForbidAperture = HL.Method(HL.Boolean) << function(self, isForbid)
    local basicNode = self.view.menuContentNode.menuBasicNode
    if isForbid then
        basicNode.apertureSlider.value = self.view.config.MAX_APERTURE
        basicNode.apertureSlider.interactable = false
    else
        basicNode.apertureSlider.interactable = true
    end
    basicNode.apertureSliderForbidToastBtn.gameObject:SetActive(isForbid)
end

SnapshotCtrl._ForbidManualFocus = HL.Method(HL.Boolean) << function(self, isForbid)
    local manualFocusSliderNode = self.view.menuContentNode.menuBasicNode.manualFocusSliderNode
    if isForbid then
        manualFocusSliderNode.manualFocusSlider.value = self.view.config.DEFAULT_AUTO_FOCUS_DISTANCE
        manualFocusSliderNode.manualFocusSlider.interactable = false
    else
        manualFocusSliderNode.manualFocusSlider.interactable = true
    end
    manualFocusSliderNode.keyHintManualFocusSliderRight.gameObject:SetActive(isForbid)
end

SnapshotCtrl._ForbidSwitchFormation = HL.Method(HL.Boolean) << function(self, isForbid)
    local formationNode = self.view.menuContentNode.menuFormationNode
    if isForbid then

        if self:IsInGamepadMoveRotateMode() then
            self.m_gamepadMoveRotateEntrySlot = -1
            self.m_gamepadMoveRotateEntryFromMenu = false
            self:_ExitGamepadMoveRotateCharMode()
        else
            self:_DeselectMoveChar()
        end
        self:_ChangeTeamFormation(TEAM_FORMATION_INDEX_NONE, true)
        formationNode.formationDropDown:SetSelected(FORMATION_DROPDOWN_INDEX_NONE, false, false)
        formationNode.formationDropDown.interactable = false
    else
        formationNode.formationDropDown.interactable = true
    end
    formationNode.formationDropDownForbidToastBtn.gameObject:SetActive(isForbid)
    self:_RefreshAfterMoveSelectableChanged()
end

SnapshotCtrl._ForbidFirstPersonPerspective = HL.Method(HL.Boolean) << function(self, isForbid)
    if isForbid then
        self.view.personPerspectiveBtn.interactable = false
        self:_SwitchPersonPerspectiveMode(false, true)
    else
        self.view.personPerspectiveBtn.interactable = true
    end
end

SnapshotCtrl._ForbidMoveOrRotateCam = HL.Method(HL.Boolean) << function(self, isForbid)
    self.m_cameraCtrl:SetForbidMoveOrRotate(isForbid)
end

SnapshotCtrl._ForbidPlayerMove = HL.Method(HL.Boolean) << function(self, isForbid)
    self:Notify(MessageConst.SNAPSHOT_INNER_FORBID_PLAYER_MOVE, isForbid)
end

SnapshotCtrl._ForbidAction = HL.Method(HL.Boolean) << function(self, isForbid)
    if isForbid then
        self:_ResetAllCustomActionsAndRefreshActionUI()
        return
    end
end



SnapshotCtrl.OnSquadInFightChanged = HL.Method(HL.Opt(HL.Any)) << function(self)
    local inFight = Utils.isInFight()
    self:_SetForbid(self.m_forbidRecords.switchFormation, inFight, "Fight")
    self:_SetForbid(self.m_forbidRecords.action, inFight, "Fight")
    self:_SetForbid(self.m_forbidRecords.firstPersonPerspective, inFight, "Fight")
    self:_SetForbid(self.m_forbidRecords.hideChar, inFight, "Fight")
end

SnapshotCtrl.OnForbidSystemChanged = HL.Method(HL.Any) << function(self, args)
    local forbidType, isForbid = unpack(args)
    if forbidType == ForbidType.ForbidMove then
        if self.m_forbidRecords == nil then
            self:_InitForbidRecords()
        end
        if isForbid then
            if not snapshotSystem.isCameraMoveMode then
                self.view.switchMoveModeTog.isOn = true
            end
            self:_SetForbid(self.m_forbidRecords.playerMoveMode, true, "ForbidSystem")
            self:_SetForbid(self.m_forbidRecords.action, true, "ForbidSystem")
        else
            self:_SetForbid(self.m_forbidRecords.playerMoveMode, false, "ForbidSystem")
            self:_SetForbid(self.m_forbidRecords.action, false, "ForbidSystem")
        end
        self:_RefreshAvatarSelectableChanged()
    elseif forbidType == ForbidType.ForbidSnapshotMainCharDragMoveRotate then
        self:_RefreshAvatarSelectableChanged()
    elseif forbidType == ForbidType.ForbidGeneralAbility then

        local forbidParams = Utils.getForbiddenReason(ForbidType.ForbidGeneralAbility)
        if forbidParams:IsStyleForbidden(CS.Beyond.Gameplay.GeneralAbilityForbidParams.ForbidStyle.Snapshot) then
            self:_CloseSelf()
            Notify(MessageConst.SHOW_TOAST, Language.LUA_SNAPSHOT_FORBID_SNAPSHOT)
            logger.info("拍照模式当前被禁用")
        end
    end
end

SnapshotCtrl.OnListenerIdentifyChanged = HL.Method() << function(self)
    self:_UpdateIdentifyInfo()
    self:_RefreshIdentifyTask()
    self:_RefreshTaskNodeVisible()
end

SnapshotCtrl.AutoCloseSelfOnInterrupt = HL.Method(HL.Opt(HL.Any)) << function(self)
    self:_CloseSelf()
end

SnapshotCtrl.OnCinematicToQueue = HL.Method() << function(self)
    if UIManager:IsOpen(PanelId.CommonShare) then
        self.m_cinematicInQueueWaitCloseSnapshot = true
    else
        self:_CloseSelf()
    end
end

SnapshotCtrl._OnMsgCloseSnapshot = HL.Method(HL.Any) << function(self, arg)
    self.m_keepCamPosWhenClose = unpack(arg)
    self:_CloseSelf()
end

SnapshotCtrl.OnFirstGotItem = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    logger.info("拍照：OnFirstGotItem")
    local itemId = unpack(args)

    for index, info in pairs(self.m_filterInfos) do
        if info.itemId == itemId then
            info.isUnlock = true
            local cell = self.m_getFilterCellFunc(index)
            if cell then
                cell.lockStateCtrl:SetState("Unlock")
            end
            local infoAreaIndex = self.m_curSelectFilterIndex > 0 and self.m_curSelectFilterIndex or self.m_curUsedFilterIndex
            if infoAreaIndex == index then
                self:_RefreshFilterInfoArea()
            end
        end
    end

    for index, info in pairs(self.m_stickerInfos) do
        if info.itemId == itemId then
            info.isUnlock = true
            local cell = self.m_getStickerCellFunc(index)
            if cell then
                cell.lockStateCtrl:SetState("Unlock")
                InputManagerInst:SetBindingText(cell.btn.hoverConfirmBindingId, Language.LUA_SNAPSHOT_STICKER_SELECT_HINT_TEXT)
            end
            local infoAreaIndex = self.m_curSelectStickerIndex > 0 and self.m_curSelectStickerIndex or self.m_curUsedStickerIndex
            if infoAreaIndex == index then
                self:_RefreshStickerInfoArea()
            end
        end
    end

    for index, info in pairs(self.m_actionInfos) do
        if info.itemId == itemId then
            info.isUnlock = true
            info.isNew = true
            local obj = self.view.menuContentNode.menuActionNode.actionList:Get(CSIndex(index))
            if obj then
                local cell = self.m_getActionCellFunc(obj)
                if cell then
                    cell.lockState:SetState("Unlock")
                end
            end
            if self.m_curSelectActionIndex == index then
                self:_RefreshActionInfoArea()
                self.view.menuContentNode.menuActionNode.playBtnNode.playProgressBar.fillAmount = 0
            end
        end
    end
end

SnapshotCtrl._OnCharacterDead = HL.Method(HL.Table) << function(self, args)
    self:_RefreshAvatarSelectableChanged()
end

SnapshotCtrl._OnScreenSizeChanged = HL.Method() << function(self)
    snapshotSystem:UpdateSensorSize()
end




SnapshotCtrl._CollectResumeState = HL.Method().Return(HL.Table) << function(self)
    local filterInfo = self.m_filterInfos[self.m_curUsedFilterIndex]
    local stickerInfo = self.m_stickerInfos[self.m_curUsedStickerIndex]
    local controllerFocusType, controllerFocusIndex = self:_GetMenuControllerFocusKey()
    local resumeState = {
        ui = {
            isShowSnapshotUI = self.m_isShowSnapshotUI,
        },
        camera = {
            isFirstPerson = snapshotSystem.isFirstPersonMode,
            focalLength = self.m_cameraCtrl:GetFocalLen(),
            aperture = self.m_cameraCtrl:GetAperture(),
            thirdPersonParamFullSnapshot = snapshotSystem.isFirstPersonMode and nil or self.m_cameraCtrl:GetCameraParamFullSnapshot(),
            rotation = self.m_cameraCtrl:GetCameraRotationState(),
            zoomScale = self.m_cameraCtrl:GetZoomScale(),
            offset = self.m_cameraCtrl:GetCameraOffset(),
        },
        menu = {
            curSelectMenuIndex = self.m_curSelectMenuIndex,
            isMenuExpand = self.m_isMenuExpand and snapshotSystem:GetMoveSelectedSlotIndex() < 0,
            controllerFocusType = controllerFocusType,
            controllerFocusIndex = controllerFocusIndex,
            basic = {
                showCharMode = self:_GetCurrentShowCharMode(),
                isShowNpc = self.m_environmentSelection.npc,
                isShowDropItem = self.m_environmentSelection.dropItem,
                isShowDecorationBuilding = self.m_environmentSelection.decorationBuilding,
                isShowOtherBuilding = self.m_environmentSelection.otherBuilding,
                isShowGridLines = self.m_isShowGridLines,
                aperture = self.m_cameraCtrl:GetAperture(),

                isManualFocus = self.m_isManualFocus,
                focusCharSlotIndex = self.m_focusCharSlotIndex,
                manualFocusSliderValue = self.view.menuContentNode.menuBasicNode.manualFocusSliderNode.manualFocusSlider.value,

                yAxisRotValue = self.view.menuContentNode.menuBasicNode.yAxisRotSliderNode.yAxisRotSlider.value,
            },
            formation = {
                formationIndex = self.m_curTeamFormationIndex,
            },
            filter = {
                usedId = filterInfo and (not filterInfo.isEmpty) and filterInfo.id or nil,
            },
            sticker = {
                usedId = stickerInfo and (not stickerInfo.isEmpty) and stickerInfo.id or nil,
                anchoredPos = self.view.stickerImg.rectTransform.anchoredPosition,
                isEditMode = self.m_inStickerEditMode,
            },
        },
    }
    resumeState.menu.move = {
        selectedSlotIndex = snapshotSystem:GetMoveSelectedSlotIndex(),
    }

    local actionResumeState = {
        curSelectAvatarIndex = self.m_curSelectAvatarIndex,
        curSelectActionIndex = self.m_curSelectActionIndex,
        slotActionMap = {},
    }
    for _, charInfo in pairs(self.m_squadCharList) do
        local playingId = snapshotSystem:GetPlayingActionId(charInfo.slotIndex)
        if playingId then
            actionResumeState.slotActionMap[charInfo.slotIndex] = playingId
        end
    end
    resumeState.menu.action = actionResumeState

    local isOpen, ctrl = UIManager:IsOpen(PanelId.CommonShare)
    if isOpen and UIManager:IsShow(PanelId.CommonShare) and PhaseManager:GetTopPhaseId() == PHASE_ID then
        local commonShareArg = ctrl:GetCurPhaseStateArg()
        if commonShareArg then
            resumeState.commonShareArg = commonShareArg
        end
    end
    return resumeState
end

SnapshotCtrl._ApplyInitialArgState = HL.Method() << function(self)

    self:_SwitchPersonPerspectiveMode(self.m_arg.thirdPerson == false, false)
    local value = math.max(math.min(self.view.config.MAX_FOCAL_LENGTH, self.m_defaultFocus), self.view.config.MIN_FOCAL_LENGTH)
    self:_ChangeFocalLength(value)
    self.view.focalLengthNode.focalLengthSlider:SetValueWithoutNotify(value)
    if self.m_arg.camInitRotate then
        self.m_cameraCtrl:SetCameraRotationState(self.m_arg.camInitRotate, false)
    end
end

SnapshotCtrl._ApplyResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    if not resumeState then
        return
    end
    self:_ApplyResumeCameraState(resumeState.camera)
    self:_ApplyResumeMenuState(resumeState.menu)
    self:_ApplyResumeUIViewState(resumeState.ui, resumeState.menu and resumeState.menu.sticker or nil)
    if resumeState.commonShareArg then
        UIManager:Open(PanelId.CommonShare, self:_BuildCommonShareArg(resumeState.commonShareArg))
        resumeState.commonShareArg = nil
    end
end

SnapshotCtrl._ApplyResumeCameraState = HL.Method(HL.Opt(HL.Any)) << function(self, cameraState)
    if not cameraState then
        return
    end
    local isFirstPerson = cameraState.isFirstPerson == true
    self:_SwitchPersonPerspectiveMode(isFirstPerson, false)
    if not isFirstPerson and cameraState.thirdPersonParamFullSnapshot then
        self.m_cameraCtrl:RestoreCameraParamFullSnapshot(cameraState.thirdPersonParamFullSnapshot)
    end
    if cameraState.focalLength then
        local focalLength = math.max(math.min(self.view.config.MAX_FOCAL_LENGTH, cameraState.focalLength), self.view.config.MIN_FOCAL_LENGTH)
        self:_ChangeFocalLength(focalLength)
        self.view.focalLengthNode.focalLengthSlider:SetValueWithoutNotify(focalLength)
    end





    if isFirstPerson and cameraState.zoomScale then
        self.m_cameraCtrl:SetZoomScale(cameraState.zoomScale)
    end
    if cameraState.offset then
        self.m_cameraCtrl:SetCameraOffset(cameraState.offset)
    end
    if isFirstPerson and cameraState.rotation then
        self.m_cameraCtrl:SetCameraRotationState(cameraState.rotation, not isFirstPerson)
    end
end

SnapshotCtrl._ApplyResumeMenuState = HL.Method(HL.Opt(HL.Any)) << function(self, menuState)
    if not menuState then
        return
    end
    local targetMenuIndex = lume.clamp(menuState.curSelectMenuIndex or 1, 1, #self.m_menuTabCellList)
    local menuContentNode = self.view.menuContentNode
    local basicState = menuState.basic
    if basicState then
        local basicNode = menuContentNode.menuBasicNode
        local showCharMode = lume.clamp(basicState.showCharMode or 1, 1, #showCharConfig)
        basicNode.showCharDropDown:SetSelected(showCharMode - 1)
        local showDecorationBuilding = basicState.isShowDecorationBuilding
        if showDecorationBuilding == nil then
            showDecorationBuilding = basicState.isShowFactoryBuilding
        end
        local showOtherBuilding = basicState.isShowOtherBuilding
        if showOtherBuilding == nil then
            showOtherBuilding = basicState.isShowFactoryBuilding
        end
        self:_SetEnvironmentSelection({
            npc = basicState.isShowNpc == true,
            dropItem = basicState.isShowDropItem == true,
            decorationBuilding = showDecorationBuilding ~= false,
            otherBuilding = showOtherBuilding ~= false,
        })
        self:_RefreshEnvironmentDropDown()
        self:_ApplyEnvironmentVisibility()
        basicNode.gridLinesTog:SetIsOnWithoutNotify(basicState.isShowGridLines == true)
        self:_SwitchShowGridLines(basicState.isShowGridLines == true)
        if basicState.aperture then
            basicNode.apertureSlider:SetValueWithoutNotify(basicState.aperture)
            self:_ChangeAperture(basicState.aperture)
        end

        if basicState.yAxisRotValue then
            local sliderNode = basicNode.yAxisRotSliderNode
            sliderNode.yAxisRotSlider:SetValueWithoutNotify(basicState.yAxisRotValue)
            self:_OnYAxisRotSliderChanged(basicState.yAxisRotValue)
        end

        if basicState.manualFocusSliderValue then
            local sliderNode = basicNode.manualFocusSliderNode
            sliderNode.manualFocusSlider:SetValueWithoutNotify(basicState.manualFocusSliderValue)

            if basicState.isManualFocus ~= false then
                self:_OnManualFocusSliderChanged(basicState.manualFocusSliderValue)
            end
        end

        local focusDropDownIndex = 0
        if basicState.isManualFocus == false and basicState.focusCharSlotIndex
            and basicState.focusCharSlotIndex >= 0 then
            focusDropDownIndex = basicState.focusCharSlotIndex + 1
        end
        basicNode.manualFocusDropDownNode.manualFocusDropDown:SetSelected(focusDropDownIndex)
    end

    self:_UpdateSquadCharList()
    local formationState = menuState.formation
    if formationState then
        local formationNode = menuContentNode.menuFormationNode
        local formationIndex = formationState.formationIndex or TEAM_FORMATION_INDEX_NONE
        local dropDownIndex = self:_TeamFormationIndexToDropdownIndex(formationIndex)
        formationNode.formationDropDown:SetSelected(dropDownIndex, false, false)
        self.m_curTeamFormationIndex = formationIndex
        if not DeviceInfo.usingController then

            self:_RefreshFormationAvatarList()
        end
    end

    local filterState = menuState.filter
    if filterState then
        local filterIndex = self:_FindFilterIndexById(filterState.usedId)
        self:_ChangeFilter(filterIndex)
        menuContentNode.menuFilterNode.menuFilterList:UpdateCount(#self.m_filterInfos, true)
    end

    local stickerState = menuState.sticker
    if stickerState then


        self.m_curUsedStickerIndex = self:_FindStickerIndexById(stickerState.usedId)
    end


    local actionState = menuState.action
    if actionState then
        self.m_isInitRefreshActionUI = true
        self:_RefreshActionAvatarList()

        local avatarIndex = lume.clamp(actionState.curSelectAvatarIndex or 1, 1, math.max(#self.m_squadCharList, 1))
        self:_OnSelectAvatarChar(avatarIndex)
    end

    local moveState = menuState.move
    if moveState and moveState.selectedSlotIndex and moveState.selectedSlotIndex >= 0 then
        if not self.m_squadCharList or #self.m_squadCharList == 0 then
            self:_UpdateSquadCharList()
        end
        self.m_gamepadMoveRotateEntrySlot = -1
        self.m_gamepadMoveRotateEntryFromMenu = false
        if DeviceInfo.usingController then
            self:_EnterGamepadMoveRotateCharMode(moveState.selectedSlotIndex, false)
        else
            self:_TrySelectMoveChar(moveState.selectedSlotIndex)
        end
    end

    self.m_resumeMenuControllerFocusType = menuState.controllerFocusType
    self.m_resumeMenuControllerFocusIndex = menuState.controllerFocusIndex or 0
    self:_ChangeMenuTab(targetMenuIndex, false, true)
    self:_SwitchMenuContentExpand(menuState.isMenuExpand == true, true)
    if menuState.isMenuExpand == true then
        if DeviceInfo.usingController then
            self.view.menuNodeNaviGroup:ManuallyFocus()
        elseif self.m_onChangeContentFuncList[targetMenuIndex]
            and self.m_onChangeContentFuncList[targetMenuIndex].entryFunc then
            self.m_onChangeContentFuncList[targetMenuIndex].entryFunc()
        end
    else
        self.m_resumeMenuControllerFocusType = nil
        self.m_resumeMenuControllerFocusIndex = 0
    end

    local stickerState = menuState.sticker
    if stickerState then
        local stickerIndex = self:_FindStickerIndexById(stickerState.usedId)
        self:_ChangeSticker(stickerIndex, true)
        if stickerState.anchoredPos and stickerIndex > 1 then
            self:_SetStickerNewPos(stickerState.anchoredPos)
        end
        self:_EnableStickerEditMode(stickerState.isEditMode == true, true)
    end
end

SnapshotCtrl._ApplyResumeUIViewState = HL.Method(HL.Opt(HL.Any, HL.Any)) << function(self, uiState, stickerState)
    if stickerState and stickerState.isEditMode then
        return
    end
    if uiState and uiState.isShowSnapshotUI == false then
        self:_SwitchSnapshotUIVisible(false, true)
    end
end

SnapshotCtrl._ApplyForbidState = HL.Method() << function(self)
    self:OnSquadInFightChanged()
    if self.m_arg.forbidFirstPerson then
        self:_SetForbid(self.m_forbidRecords.firstPersonPerspective, true, "InitArg")
    end
    if self.m_arg.forbidMoveOrRotateCam then
        self:_SetForbid(self.m_forbidRecords.controlCam, true, "InitArg")
    end
end

SnapshotCtrl._GetMenuControllerFocusKey = HL.Method().Return(HL.Any, HL.Number) << function(self)
    if self.m_curSelectMenuIndex == 4 then
        return "filter", 1
    elseif self.m_curSelectMenuIndex == 5 then
        return "sticker", 1
    end
    return "", 0
end

SnapshotCtrl._GetCurrentShowCharMode = HL.Method().Return(HL.Number) << function(self)
    local selectedIndex = self.view.menuContentNode.menuBasicNode.showCharDropDown.selectedIndex
    local luaIndex = LuaIndex(selectedIndex)
    if luaIndex <= 0 then
        return 1
    end
    return luaIndex
end

SnapshotCtrl._FindFilterIndexById = HL.Method(HL.Opt(HL.Any)).Return(HL.Number) << function(self, filterId)
    if filterId == nil or filterId == "" then
        return 1
    end
    for luaIndex, info in ipairs(self.m_filterInfos) do
        if info.id == filterId then
            return luaIndex
        end
    end
    return 1
end

SnapshotCtrl._FindStickerIndexById = HL.Method(HL.Opt(HL.Any)).Return(HL.Number) << function(self, stickerId)
    if stickerId == nil or stickerId == "" then
        return 1
    end
    for luaIndex, info in ipairs(self.m_stickerInfos) do
        if info.id == stickerId then
            return luaIndex
        end
    end
    return 1
end

SnapshotCtrl._ResumePanel = HL.Method() << function(self)
    self:_ApplyInitialArgState()
    self:_ApplyResumeState(self.m_arg.resumeState)


    self:_ApplyForbidState()


    if self.m_arg.onOpenCallBack then
        self.m_arg.onOpenCallBack()
    end

    self.m_arg.resumeState = nil
end




HL.Commit(SnapshotCtrl)
