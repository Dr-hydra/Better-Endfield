local MotionType = {
    SlightShort = 1,
    Strong = 2
}
local PowerPoleFastTravelKey = "PowerPoleFastTravelKey"
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.PowerPoleFastTravel

local ConflictName = "PowerPoleFastTravelCtrl"

PowerPoleFastTravelCtrl = HL.Class('PowerPoleFastTravelCtrl', uiCtrl.UICtrl)







PowerPoleFastTravelCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.TRAVEL_POLE_TRAVEL_ON_ENTER] = 'OnEnterFinish',
    [MessageConst.TRAVEL_POLE_TRAVEL_ON_REACH] = 'OnReach',
    [MessageConst.TRAVEL_POLE_TRAVEL_ON_REACH_REFRESH] = 'OnReachRefresh',
    [MessageConst.TRAVEL_POLE_FAST_EXIT] = 'OnTravelPoleExitFast',
    [MessageConst.TRAVEL_POLE_SHOW_QTE] = 'ShowQte',
    [MessageConst.TRAVEL_POLE_HIDE_QTE] = 'HideQte',
    [MessageConst.TRAVEL_POLE_UPDATE_QTE_COUNTDOWN] = 'UpdateQteCountdown',
    [MessageConst.TRAVEL_POLE_TRIGGER_DEFAULT_NEXT] = 'TriggerDefaultNext',
    [MessageConst.TRAVEL_POLE_TRIGGER_CLOSE_PANEL] = 'TriggerClosePanel',
    [MessageConst.ON_COMMON_BACK_CLICKED] = '_OnButtonLeave',

}

PowerPoleFastTravelCtrl.m_linkInfoList = HL.Field(HL.Userdata)

PowerPoleFastTravelCtrl.m_trackers = HL.Field(HL.Table)

PowerPoleFastTravelCtrl.m_trackersCache = HL.Field(HL.Table)

PowerPoleFastTravelCtrl.m_currentLogicId = HL.Field(HL.Any) << 0

PowerPoleFastTravelCtrl.m_currentIsUpgraded = HL.Field(HL.Boolean) << false

PowerPoleFastTravelCtrl.m_currentAimingLogicId = HL.Field(HL.Any) << 0

PowerPoleFastTravelCtrl.m_lateTickKey = HL.Field(HL.Number) << -1

PowerPoleFastTravelCtrl.m_isMoving = HL.Field(HL.Boolean) << false

PowerPoleFastTravelCtrl.m_nextDestinationLogicId = HL.Field(HL.Any) << 0

PowerPoleFastTravelCtrl.m_buttonConfirmTimer = HL.Field(HL.Number) << -1

PowerPoleFastTravelCtrl.m_qteToggled = HL.Field(HL.Boolean) << false

PowerPoleFastTravelCtrl.m_allowLeave = HL.Field(HL.Boolean) << false

PowerPoleFastTravelCtrl.m_enterFinished = HL.Field(HL.Boolean) << false

PowerPoleFastTravelCtrl.m_confirmEnabled = HL.Field(HL.Boolean) << true

PowerPoleFastTravelCtrl.m_waitHideQte = HL.Field(HL.Boolean) << false

PowerPoleFastTravelCtrl.m_isQteClickAnimPlaying = HL.Field(HL.Boolean) << false

PowerPoleFastTravelCtrl.m_onClickLeave = HL.Field(HL.Boolean) << false

PowerPoleFastTravelCtrl.m_args = HL.Field(HL.Any)

PowerPoleFastTravelCtrl.m_onClickScreen = HL.Field(HL.Function)


PowerPoleFastTravelCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_args = arg
    self.view.nodeConfirm.gameObject:SetActive(false)
    self:_SetButtonState(self.view.buttonConfirm, false)
    self.view.buttonConfirm.onClick:AddListener(function()
        AudioAdapter.PostEvent("Au_UI_Button_PoleForwardDirectly")
        self:_OnConfirmButton()
    end)

    if DeviceInfo.usingTouch then
        self.view.buttonConfirm.onPressStart:AddListener(function()
            self.view.mobile.buttonConfirmAnim:Play("mobile_fasttravelbtn_pressed")
        end)

        self.view.buttonConfirm.onPressEnd:AddListener(function()
            self.view.mobile.buttonConfirmAnim:Play("mobile_fasttravelbtn_release")
        end)
    end

    self.view.buttonLink.onClick:AddListener(function()
        self:_OnButtonLink()
    end)

    self.view.buttonLeave.onClick:AddListener(function()
        self:_OnButtonLeave()
    end)

    self.view.buttonQte.onClick:AddListener(function()
        AudioAdapter.PostEvent("Au_UI_Button_PoleForwardDirectly")
        self:_OnButtonQte()
    end)

    self.view.buttonQte.onPressStart:AddListener(function()
        self.view.qteAnimationWrapper:Play("powerpolefasttravel_qte_pressd")
    end)

    self.view.buttonQte.onPressEnd:AddListener(function()
        self.view.qteAnimationWrapper:Play("powerpolefasttravel_qte_release")
    end)

    self.view.hintBar.gameObject:SetActive(false)
    self.view.qteNode.gameObject:SetActive(false)
    self.view.nodeSetDefaultNext.gameObject:SetActive(false)
    self.view.nodeLeave.gameObject:SetActive(false)
    self:_SetButtonState(self.view.buttonLeave, false)

    self.m_enterFinished = false
    self.m_trackers = {}
    self.m_trackersCache = {}
    self.m_linkInfoList = nil

    self.view.tracker.gameObject:SetActive(false)
    self.m_lateTickKey = LuaUpdate:Add("LateTick", function(deltaTime)
        self:_UpdateTrackers()
    end)


    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId},{"fac_fast_travel_cam_orbit"})

    self:_InitFastTravelPanel(self.m_args)

    if DeviceInfo.isPC then
        self.m_onClickScreen = function(eventData)
            self:_OnConfirmButton(true)
        end
        UIManager.commonTouchPanel.onClick:AddListener(self.m_onClickScreen)
    end

    if DeviceInfo.usingController then
        self:_ToggleControllerTriggerSetting(true, false)
        self:_StartCoroutine(function()
            while true do
                coroutine.step()
                self:_ControllerMoveTick()
            end
        end)
    end
end

PowerPoleFastTravelCtrl.OnShow = HL.Override() << function(self)
end

PowerPoleFastTravelCtrl.OnClose = HL.Override() << function(self)
    if DeviceInfo.isPC then
        UIManager.commonTouchPanel.onClick:RemoveListener(self.m_onClickScreen)
    end
    GameInstance.player.systemActionConflictManager:OnSystemActionEnd(ConflictName)
    self:_ToggleControllerTriggerSetting(false)
    LuaUpdate:Remove(self.m_lateTickKey)
    self:Notify(MessageConst.ON_EXIT_TRAVEL_MODE)
end

PowerPoleFastTravelCtrl._SetButtonState = HL.Method(HL.Userdata, HL.Boolean) << function(self, button, enabled)
    if DeviceInfo.usingController then
        button.gameObject:SetActive(enabled)
    else
        button.interactable = enabled
    end
end

PowerPoleFastTravelCtrl._GetButtonState = HL.Method(HL.Userdata).Return(HL.Boolean) << function(self, button)
    if DeviceInfo.usingController then
        return button.gameObject.activeSelf
    else
        return button.interactable
    end
end

PowerPoleFastTravelCtrl.OnEnterFinish = HL.Method() << function(self)
    self.m_enterFinished = true
    self.view.hintBar.gameObject:SetActive(true)
    self.view.nodeConfirm.gameObject:SetActive(true)
    self.m_allowLeave = GameWorld.gameMechManager.travelPoleBrain:GetTravelPoleAllowLeave(self.m_currentLogicId)
    self.view.nodeLeave.gameObject:SetActive(self.m_allowLeave)
    self:_OnTravelPoleIdle()
end

PowerPoleFastTravelCtrl.OnReach = HL.Method(HL.Any) << function(self, args)
    local qteTriggered = unpack(args)

    AudioAdapter.PostEvent("au_ui_travel_pole_pause")
    self.m_isMoving = false
    self.m_currentLogicId = self.m_nextDestinationLogicId
    self.m_currentIsUpgraded = GameWorld.gameMechManager.travelPoleBrain:CheckTravelPoleIsUpgradedByLid(self.m_currentLogicId)

    self.m_nextDestinationLogicId = 0

    self.m_allowLeave = GameWorld.gameMechManager.travelPoleBrain:GetTravelPoleAllowLeave(self.m_currentLogicId)
    self.view.nodeLeave.gameObject:SetActive(self.m_allowLeave)

    self.m_linkInfoList = GameWorld.gameMechManager.travelPoleBrain:GetLinkedTravelPoleInfoList(self.m_currentLogicId)

    self:_RefreshQteToggled(qteTriggered)
    GameInstance.player.systemActionConflictManager:OnSystemActionEnd(ConflictName)

    self:_OnTravelPoleIdle()
end

PowerPoleFastTravelCtrl._RefreshQteToggled = HL.Method(HL.Boolean) << function(self, qteTriggered)
    self.m_qteToggled = qteTriggered
    local isFinalPole = GameWorld.gameMechManager.travelPoleBrain:IsFinalTravelPole(self.m_currentLogicId)
    local confirmEnabled = not qteTriggered and not isFinalPole
    self.m_confirmEnabled = confirmEnabled
    self.view.hintBar.gameObject:SetActive(confirmEnabled)
    self.view.nodeConfirm.gameObject:SetActive(confirmEnabled)
    if not self.m_qteToggled then
        UIManager:ShowWithKey(PanelId.MiniMap, PowerPoleFastTravelKey)
    end
end

PowerPoleFastTravelCtrl.OnReachRefresh = HL.Method() << function(self)
    self.m_linkInfoList = GameWorld.gameMechManager.travelPoleBrain:GetLinkedTravelPoleInfoList(self.m_currentLogicId)
end

PowerPoleFastTravelCtrl.OnTravelPoleExitFast = HL.Method() << function(self)
    AudioAdapter.PostEvent("au_ui_travel_pole_stop")
    if PhaseManager:IsOpen(PhaseId.PowerPoleFastTravel) and PhaseManager:GetTopPhaseId() == PhaseId.PowerPoleFastTravel then
        PhaseManager:PopPhase(PhaseId.PowerPoleFastTravel)
    end
end

PowerPoleFastTravelCtrl.ShowQte = HL.Method() << function(self)
    self.view.qteNode.gameObject:SetActive(true)
    AudioAdapter.PostEvent("Au_UI_Popup_PoleButtonAppear")
    UIUtils.PlayAnimationAndToggleActive(self.view.qteAnimationWrapper, true)
    self.m_isQteClickAnimPlaying = false
    self.m_waitHideQte = false
end

PowerPoleFastTravelCtrl.HideQte = HL.Method() << function(self)
    if self.m_isQteClickAnimPlaying then
        self.m_waitHideQte = true
    else
        AudioAdapter.PostEvent("Au_UI_Popup_PoleButtonDisappear")
        UIUtils.PlayAnimationAndToggleActive(self.view.qteAnimationWrapper, false)
    end
end

PowerPoleFastTravelCtrl.UpdateQteCountdown = HL.Method(HL.Table) << function(self, args)
    local value = unpack(args)
    self.view.qteNode.qteCountdown.fillAmount = value
end

PowerPoleFastTravelCtrl.TriggerDefaultNext = HL.Method(HL.Table) << function(self, args)
    local defaultNextLid = unpack(args)
    self:_BeginTravel(defaultNextLid)
end

PowerPoleFastTravelCtrl._InitFastTravelPanel = HL.Method(HL.Table) << function(self, args)
    local poleLogicId = unpack(args)

    self.m_isMoving = false
    self.m_currentLogicId = poleLogicId
    self.m_currentIsUpgraded = GameWorld.gameMechManager.travelPoleBrain:CheckTravelPoleIsUpgradedByLid(self.m_currentLogicId)


    AudioAdapter.PostEvent("au_ui_travel_pole_transfer")


    self.m_linkInfoList = GameWorld.gameMechManager.travelPoleBrain:GetLinkedTravelPoleInfoList(self.m_currentLogicId)
end

PowerPoleFastTravelCtrl._UpdateTrackers = HL.Method() << function(self)
    if self.m_linkInfoList == nil then
        return
    end

    if not GameWorld.gameMechManager.travelPoleBrain:CheckCurrentTravelPoleValid() then
        return
    end

    local targetScrPosDict = {}
    local targetScrPosIsForwardDict = {}
    local targetDistanceDict = {}
    local targetIconStatusDict = {}
    local targetStatusDict = {}
    local targetIsOtherSocialTravelDict = {}
    local targetIsHighlightedDict = {}
    local targetLineDict = {}
    local logicIdList = {}
    local mainCharacterPos = GameWorld.gameMechManager.travelPoleBrain.handRailPos
    local mainCharacterForwardVector = GameWorld.gameMechManager.travelPoleBrain.handRailForwardVector

    for _, linkInfo in pairs(self.m_linkInfoList) do
        if linkInfo.targetEntity.isValid then
            local screenPos, isInside = UIUtils.objectPosToUI(linkInfo.targetPos, self.uiCamera)
            table.insert(targetScrPosDict, screenPos)
            local targetVector = linkInfo.targetPos - mainCharacterPos
            table.insert(targetScrPosIsForwardDict, Vector3.Dot(mainCharacterForwardVector, targetVector) > 0)
            table.insert(targetDistanceDict, targetVector)
            table.insert(targetIconStatusDict, GameWorld.gameMechManager.travelPoleBrain:GetTravelPoleIcon(self.m_currentLogicId, linkInfo.logicId))
            table.insert(targetStatusDict, GameWorld.gameMechManager.travelPoleBrain:GetTravelPoleStatus(self.m_currentLogicId, linkInfo.logicId))
            table.insert(targetIsHighlightedDict, false)
            linkInfo:RefreshLine()
            if linkInfo.line == nil or linkInfo.line.markInvalid then
                table.insert(targetLineDict, 0)
            else
                table.insert(targetLineDict, linkInfo.line)
            end
            table.insert(logicIdList, linkInfo.logicId)
            table.insert(targetIsOtherSocialTravelDict, FactoryUtils.isOthersSocialBuilding(CSFactoryUtil.GetNodeIdByLogicId(linkInfo.logicId)))
        end
    end

    if #self.m_trackers > #targetScrPosDict then
        for i = #self.m_trackers, #targetScrPosDict+1, -1 do
            self.m_trackers[i].obj:SetActive(false)
            table.insert(self.m_trackersCache, self.m_trackers[i])
            table.remove(self.m_trackers, i)
        end
    end

    if #self.m_trackers < #targetScrPosDict then
        for i = #self.m_trackers + 1, #targetScrPosDict do
            table.insert(self.m_trackers, self:_CreateNewTracker())
        end
    end

    local nearestIndex = -1
    local nearestDistance = 99999999

    for i = 1, #targetScrPosDict do
        if self.m_trackers[i].tracker.allowToHighlight and targetScrPosIsForwardDict[i] then
            local screenPos = targetScrPosDict[i]
            local distToCenter = screenPos.x * screenPos.x + screenPos.y * screenPos.y
            if distToCenter < nearestDistance then
                nearestIndex = i
                nearestDistance = distToCenter
            end
        end
    end

    local newFocus = false
    if self.m_isMoving then
        self.view.nodeConfirm.gameObject:SetActive(false)
        local lastIsEnabled = self:_GetButtonState(self.view.buttonConfirm)
        self:_SetButtonState(self.view.buttonConfirm, false)
        if lastIsEnabled then
            AudioAdapter.PostEvent("Au_UI_Popup_PoleButtonDisappear")
        end
        self.m_currentAimingLogicId = 0
    else
        if self.m_enterFinished then
            self.view.nodeConfirm.gameObject:SetActive(self.m_confirmEnabled)
            local lastIsDisabled = not self:_GetButtonState(self.view.buttonConfirm)
            self:_SetButtonState(self.view.buttonConfirm, true)
            if lastIsDisabled then
                AudioAdapter.PostEvent("Au_UI_Popup_PoleButtonAppear")
            end
        end
        if nearestIndex > 0 and nearestDistance < 600000 then
            targetIsHighlightedDict[nearestIndex] = true
            local newAniming = logicIdList[nearestIndex]
            if newAniming ~= self.m_currentAimingLogicId then
                newFocus = true
                AudioAdapter.PostEvent("au_ui_travel_pole_correct")
            end
            self.m_currentAimingLogicId = newAniming
        else
            self.m_currentAimingLogicId = 0
        end
    end
    self:_OnConfirmBtnStateChange()
    self:_OnLinkHintStateChange()

    local hasHighlighted = false
    for i = 1, #targetScrPosDict do
        local item = self.m_trackers[i]
        if item then
            local uiPos, uiAngle, isOutBound = UIUtils.mapScreenPosToEllipseEdge(targetScrPosDict[i], self.view.config.ELLIPSE_X_RADIUS, self.view.config.ELLIPSE_Y_RADIUS)


            local isHighlighted = targetIsHighlightedDict[i]
            if self.m_isMoving then
                if self.m_nextDestinationLogicId == logicIdList[i] then
                    isHighlighted = true
                else
                    isHighlighted = false
                end
            end
            item.tracker:UpdateIsOtherSocial(targetIsOtherSocialTravelDict[i])
            item.tracker:UpdatePosition(uiPos, uiAngle, isOutBound)
            item.tracker:UpdateDistance(targetDistanceDict[i])
            item.tracker:UpdateStatus(targetStatusDict[i])
            item.tracker:UpdateIsHighlighted(isHighlighted)

            if targetLineDict[i] ~= 0 and not targetLineDict[i].markInvalid then
                targetLineDict[i]:SetStatus(targetStatusDict[i])
                targetLineDict[i]:SetIsHighlighted(isHighlighted)
            end
            item.tracker:UpdateIconStatus(targetIconStatusDict[i])

            if isHighlighted and newFocus then
                item.tracker:PlayFocus()
            end

            hasHighlighted = hasHighlighted or isHighlighted
        end
    end


    if GameWorld.gameMechManager.travelPoleBrain.isFactoryTravelPole then
        local aimingIsUpgraded = false
        if self.m_currentAimingLogicId ~= 0 then
            aimingIsUpgraded = GameWorld.gameMechManager.travelPoleBrain:CheckTravelPoleIsUpgradedByLid(self.m_currentAimingLogicId)
        end

        local showDefaultNext = self:_GetButtonState(self.view.buttonConfirm) and self.m_currentIsUpgraded and aimingIsUpgraded and not self.m_qteToggled
        if not self.m_onClickLeave then
            self.view.nodeSetDefaultNext.gameObject:SetActive(showDefaultNext)
        end
    end

    if self.m_isMoving or self.m_qteToggled then
        self.view.nodeConfirm.gameObject:SetActive(false)
        self:_SetButtonState(self.view.buttonConfirm, false)
        self.view.nodeLeave.gameObject:SetActive(false)
        self:_SetButtonState(self.view.buttonLeave, false)
    else
        if self.m_enterFinished and not self.m_onClickLeave then
            self.view.nodeLeave.gameObject:SetActive(self.m_allowLeave)
            self:_SetButtonState(self.view.buttonLeave, true)
        end
    end
end

PowerPoleFastTravelCtrl._CreateNewTracker = HL.Method().Return(HL.Table) << function(self)
    local cacheCount = #self.m_trackersCache
    if cacheCount > 0 then
        local cacheObj = self.m_trackersCache[cacheCount]
        cacheObj.obj:SetActive(true)
        table.remove(self.m_trackersCache, cacheCount)
        return cacheObj
    end
    local obj = CSUtils.CreateObject(self.view.tracker.gameObject, self.view.trackerParent)
    obj:SetActive(true)
    local item = {}
    item.obj = obj
    item.tracker = obj:GetComponent(typeof(CS.Beyond.UI.UIPowerPoleFastTravelTracker))
    return item
end

PowerPoleFastTravelCtrl._OnConfirmButton = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipCursorVisibleCheck)

    if skipCursorVisibleCheck ~= true and InputManager.cursorVisible and DeviceInfo.isPC then
       return
    end


    if not GameWorld.gameMechManager.travelPoleBrain.allowButtonBeginTravel then
        return
    end
    if self.m_isMoving then
        return
    end

    if DeviceInfo.usingTouch then
        self.view.mobile.buttonConfirmAnim:Play("mobile_fasttravelbtn_release", function()
            self:_BeginTravel(self.m_currentAimingLogicId)
        end)
    elseif DeviceInfo.usingController then


        return
    else
        self:_BeginTravel(self.m_currentAimingLogicId)
    end
end

PowerPoleFastTravelCtrl._BeginTravel = HL.Method(HL.Any) << function(self, nextLogicId)
    if not GameWorld.gameMechManager.travelPoleBrain.allowBeginTravel then
        return
    end

    if not nextLogicId or nextLogicId <= 0 then
        if GameWorld.gameMechManager.travelPoleBrain.isFactoryTravelPole then
            self:Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_FAST_TRAVEL_NO_TARGET_TOAST)
        end
        return
    end

    if not GameWorld.gameMechManager.travelPoleBrain:BeginTravelToStuckTest(self.m_currentLogicId, nextLogicId) then
        self:_RefreshQteToggled(false)
        if self.m_isQteClickAnimPlaying then
            self.m_isQteClickAnimPlaying = false
            if self.m_waitHideQte then
                UIUtils.PlayAnimationAndToggleActive(self.view.qteAnimationWrapper, false)
                self.m_waitHideQte = false
            end
        end
        return
    end

    if not GameInstance.player.systemActionConflictManager:TryStartSystemAction(ConflictName) then
        logger.error("PowerPoleFastTravelCtrl._OnConfirmButton FAIL 正在进行其他互斥的行为 ", GameInstance.player.systemActionConflictManager:GetCurProcessingSystemActionInfo())
        return
    end

    if nextLogicId ~= 0 then
        for iCs, linkInfo in pairs(self.m_linkInfoList) do
            local i = iCs + 1
            local item = self.m_trackers[i]
            if item ~= nil then
                if linkInfo.targetEntity.isValid then
                    if linkInfo.logicId ~= nextLogicId then
                        item.tracker:UpdateIsHighlighted(false)
                        if linkInfo.line ~= nil and not linkInfo.line.markInvalid then
                            linkInfo.line:SetIsHighlighted(false)
                        end
                    else
                        item.tracker:UpdateIsHighlighted(true)
                        if linkInfo.line ~= nil and not linkInfo.line.markInvalid then
                            linkInfo.line:SetIsHighlighted(true)
                        end
                    end
                end
            end
        end

        AudioAdapter.PostEvent("au_ui_travel_pole_confirm_start")
        self.view.hintBar.gameObject:SetActive(false)

        GameWorld.gameMechManager.travelPoleBrain:BeginTravelTo(nextLogicId)
        self.m_linkInfoList = GameWorld.gameMechManager.travelPoleBrain:GetLinkedTravelPoleInfoList(self.m_currentLogicId)
        self.m_nextDestinationLogicId = nextLogicId
        self.m_isMoving = true
        UIManager:HideWithKey(PanelId.MiniMap, PowerPoleFastTravelKey)
        self.m_qteToggled = false

        self:_TryMotionOnMobileDevice(MotionType.Strong)
        if DeviceInfo.usingController then
            self:_ToggleControllerTriggerSetting(true, true)
        end
    end
end

PowerPoleFastTravelCtrl._OnButtonLink = HL.Method() << function(self)
    if self.m_isMoving ~= true and self.m_currentAimingLogicId ~= nil then
        local result = GameWorld.gameMechManager.travelPoleBrain:SetDefaultNext(self.m_currentAimingLogicId)

        if result == 3 then
            self:Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_TRAVEL_POLE_REPEAT_LINK)
        elseif result == 2 then
            self:Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_TRAVEL_POLE_RECURSIVE_LINK)
        elseif result == 1 then
            self:_TryMotionOnMobileDevice(MotionType.SlightShort)
        end
    end
end

PowerPoleFastTravelCtrl._OnButtonQte = HL.Method() << function(self)
    if not GameWorld.gameMechManager.travelPoleBrain.allowButtonQte then
        return
    end

    GameWorld.gameMechManager.travelPoleBrain:TriggerQte(self.m_nextDestinationLogicId)
    self.m_isQteClickAnimPlaying = true
    AudioAdapter.PostEvent("Au_UI_Popup_PoleButtonAppear")
    self.view.qteAnimationWrapper:PlayWithTween("polefasttravelbuttonqte_in", function()
        self.m_isQteClickAnimPlaying = false
        if self.m_waitHideQte then
            UIUtils.PlayAnimationAndToggleActive(self.view.qteAnimationWrapper, false)
            AudioAdapter.PostEvent("Au_UI_Popup_PoleButtonDisappear")
            self.m_waitHideQte = false
        end
    end)
    self.m_qteToggled = true

    self:_TryMotionOnMobileDevice(MotionType.SlightShort)
end

PowerPoleFastTravelCtrl._OnButtonLeave = HL.Method() << function(self)
    if not GameWorld.gameMechManager.travelPoleBrain.allowButtonLeave then
        return
    end

    if self.m_currentAimingLogicId ~= 0 and self.m_linkInfoList ~= nil then
        for iCs, linkInfo in pairs(self.m_linkInfoList) do
            if linkInfo.targetEntity.isValid then
                local item = self.m_trackers[iCs + 1]
                item.tracker:UpdateIsHighlighted(false)
                if linkInfo.line ~= nil and not linkInfo.line.markInvalid then
                    linkInfo.line:SetIsHighlighted(false)
                end
            end
        end
    end

    AudioAdapter.PostEvent("au_ui_travel_pole_end")
    GameWorld.gameMechManager.travelPoleBrain:ExitTravelMode(self.m_currentLogicId)

    self.m_onClickLeave = true
    self.m_confirmEnabled = false

    self:_SetButtonState(self.view.buttonLink, false)
    self:_SetButtonState(self.view.buttonLeave, false)
    self.view.hintBar.gameObject:SetActive(false)
    if DeviceInfo.usingTouch then
        local mobileNode = self.view.mobile
        UIUtils.PlayAnimationAndToggleActive(mobileNode.buttonConfirmAnim, false)
        UIUtils.PlayAnimationAndToggleActive(mobileNode.buttonQteAnim, false)
        UIUtils.PlayAnimationAndToggleActive(mobileNode.buttonLeaveAnim, false)
        UIUtils.PlayAnimationAndToggleActive(mobileNode.typeNodeAnim, false)
    end
end

PowerPoleFastTravelCtrl.TriggerClosePanel = HL.Method() << function(self)
    UIManager:ShowWithKey(PanelId.MiniMap, PowerPoleFastTravelKey)
    GameInstance.player.systemActionConflictManager:OnSystemActionEnd(ConflictName)

    if PhaseManager:IsOpen(PhaseId.PowerPoleFastTravel) and PhaseManager:GetTopPhaseId() == PhaseId.PowerPoleFastTravel then
        GameWorld.gameMechManager.travelPoleBrain:OnClosePanelNormal()
        PhaseManager:PopPhase(PhaseId.PowerPoleFastTravel, function()
            GameWorld.gameMechManager.travelPoleBrain:ClearExtraGlobalTags()
        end)
    end
end

PowerPoleFastTravelCtrl._OnTravelPoleIdle = HL.Method() << function(self)
    if self.m_isMoving then
        return
    end
    local nodeId = CSFactoryUtil.GetNodeIdByLogicId(self.m_currentLogicId)
    if nodeId == 0 or not FactoryUtils.isOthersSocialBuilding(nodeId) or not FactoryUtils.canLikeSocialBuilding(nodeId) then
        return
    end
    FactoryUtils.likeSocialBuilding(nodeId, function()
        if self.m_isMoving then
            return
        end
        if not FactoryUtils.canLikeSocialBuilding(nodeId) then
            UIUtils.PlayAnimationAndToggleActive(self.view.autoLikeNode, true, function()
                self.view.autoLikeNode.gameObject:SetActive(false)
            end)
        end
    end)
end



PowerPoleFastTravelCtrl._OnConfirmBtnStateChange = HL.Method() << function(self)
    if not DeviceInfo.usingTouch then
        return
    end

    local isOn = self.m_currentAimingLogicId > 0 and
            GameWorld.gameMechManager.travelPoleBrain.allowButtonBeginTravel
    self.view.mobile.normalNode.gameObject:SetActive(isOn)
    self.view.mobile.disableNode.gameObject:SetActive(not isOn)
end

PowerPoleFastTravelCtrl._OnLinkHintStateChange = HL.Method() << function(self)
    if not DeviceInfo.usingTouch then
        return
    end

    if self.m_currentAimingLogicId <= 0 then
        return
    end

    local brain = GameWorld.gameMechManager.travelPoleBrain
    local currentSucc, currentPoleDefaultNextLid = brain:TryGetTravelPoleDefaultNextByLid(self.m_currentLogicId)
    local aimSucc, aimPoleDefaultNextLid = brain:TryGetTravelPoleDefaultNextByLid(self.m_currentAimingLogicId)
    if currentSucc and currentPoleDefaultNextLid == self.m_currentAimingLogicId then
        self.view.mobile.linkStateController:SetState("aim_is_cur_default")
    elseif aimSucc and aimPoleDefaultNextLid == self.m_currentLogicId then
        self.view.mobile.linkStateController:SetState("aim_default_is_cur")
    else
        self.view.mobile.linkStateController:SetState("aim_can_set_default")
    end
end

PowerPoleFastTravelCtrl._TryMotionOnMobileDevice = HL.Method(HL.Number) << function(self, motionType)
    if not DeviceInfo.usingTouch then
        return
    end


    if motionType == MotionType.SlightShort then
        GameInstance.mobileMotionManager:PostEventCommonShort()
    elseif motionType == MotionType.Strong then
        GameInstance.mobileMotionManager:PostEventCommonOperateSuccess()
    end
end






PowerPoleFastTravelCtrl.m_controllerTriggerSettingHandlerId = HL.Field(HL.Number) << -1

PowerPoleFastTravelCtrl.m_isControllerTriggerUsingVibration = HL.Field(HL.Boolean) << false

PowerPoleFastTravelCtrl.m_controllerTriggerNeedRelease = HL.Field(HL.Boolean) << false

PowerPoleFastTravelCtrl._ToggleControllerTriggerSetting = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, active, useVibration)
    local oldHandlerId = self.m_controllerTriggerSettingHandlerId
    self.m_isControllerTriggerUsingVibration = active and useVibration
    if active then
        local cmd = CS.Plugins.LibScePad.TriggerEffectCommandUnion()
        cmd.mask = CS.Plugins.LibScePad.TriggerMask.ALL

        if useVibration then
            cmd.mode = CS.Plugins.LibScePad.ScePadTriggerEffectMode.SCE_PAD_TRIGGER_EFFECT_MODE_MULTIPLE_POSITION_VIBRATION
            local vibration = CS.Plugins.LibScePad.TriggerMultiPositionVibrationEffect()
            vibration.amplitude0 = 8
            vibration.amplitude1 = 6
            vibration.amplitude2 = 5
            vibration.amplitude3 = 3
            vibration.amplitude4 = 6
            vibration.amplitude5 = 3
            vibration.amplitude6 = 8
            vibration.amplitude7 = 2
            vibration.amplitude8 = 0
            vibration.amplitude9 = 5
            vibration.frequency = 150
            cmd.multiPositionVibration = vibration
        else
            cmd.mode = CS.Plugins.LibScePad.ScePadTriggerEffectMode.SCE_PAD_TRIGGER_EFFECT_MODE_SLOPE_FEEDBACK
            local effect = CS.Plugins.LibScePad.TriggerSlopeFeedbackEffect()
            effect.startPosition = 0
            effect.endPosition = 8
            effect.startStrength = 8
            effect.endStrength = 7
            cmd.slopeFeedback = effect
        end
        self.m_controllerTriggerSettingHandlerId = GameInstance.audioManager.gamePad.scePad:SetTriggerEffect(cmd)
    else
        self.m_controllerTriggerSettingHandlerId = -1
    end

    if oldHandlerId >= 0 then
        GameInstance.audioManager.gamePad.scePad:EndTriggerEffect(oldHandlerId)
    end
end

PowerPoleFastTravelCtrl._ControllerMoveTick = HL.Method() << function(self)
    if self.m_isMoving then
        if not self.m_isControllerTriggerUsingVibration or self.m_controllerTriggerSettingHandlerId < 0 then
            self:_ToggleControllerTriggerSetting(true, true)
        end
        return
    else
        if self.m_isControllerTriggerUsingVibration then
            self.m_controllerTriggerNeedRelease = true
        end
        if self.m_controllerTriggerNeedRelease then
            if InputManagerInst:GetGamepadTriggerValue(true) <= 0.1 and InputManagerInst:GetGamepadTriggerValue(false) <= 0.1 then
                self.m_controllerTriggerNeedRelease = false
            end
        end

        local hasTarget = self.m_currentAimingLogicId > 0
        if hasTarget then
            if self.m_isControllerTriggerUsingVibration or self.m_controllerTriggerSettingHandlerId < 0 then
                self:_ToggleControllerTriggerSetting(true, false)
            end
        else
            if self.m_controllerTriggerSettingHandlerId >= 0 then
                self:_ToggleControllerTriggerSetting(false, false)
            end
        end

        if self.m_controllerTriggerNeedRelease then
            return
        end
    end
    if not GameWorld.gameMechManager.travelPoleBrain.allowButtonBeginTravel then
        return
    end
    if not self.view.buttonConfirm.groupEnabled then
        return
    end

    local ltValue = InputManagerInst:GetGamepadTriggerValue(true)
    local rtValue = InputManagerInst:GetGamepadTriggerValue(false)
    if ltValue >= 0.9 and rtValue >= 0.9 then
        self:_BeginTravel(self.m_currentAimingLogicId)
        self.m_controllerTriggerNeedRelease = true
    end
end





HL.Commit(PowerPoleFastTravelCtrl)
