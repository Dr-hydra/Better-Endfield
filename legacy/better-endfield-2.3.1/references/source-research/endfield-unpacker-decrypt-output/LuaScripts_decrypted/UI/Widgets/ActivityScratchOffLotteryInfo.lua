local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ActivityScratchOffLotteryInfo = HL.Class('ActivityScratchOffLotteryInfo', UIWidgetBase)







ActivityScratchOffLotteryInfo.m_activityId = HL.Field(HL.String) << ""

ActivityScratchOffLotteryInfo.m_activity = HL.Field(HL.Userdata)

ActivityScratchOffLotteryInfo.m_lotteryInstanceId = HL.Field(HL.Number) << 0

ActivityScratchOffLotteryInfo.m_isScratchCompletedLocally = HL.Field(HL.Boolean) << false

ActivityScratchOffLotteryInfo.m_canScratch = HL.Field(HL.Boolean) << false

ActivityScratchOffLotteryInfo.m_onScratchCompletedLocally = HL.Field(HL.Function)

ActivityScratchOffLotteryInfo.m_onScratchCompleted = HL.Field(HL.Function)

ActivityScratchOffLotteryInfo.m_cellListCache = HL.Field(HL.Forward("UIListCache"))

ActivityScratchOffLotteryInfo.m_isScratchAudioPlaying = HL.Field(HL.Boolean) << false

ActivityScratchOffLotteryInfo.m_scratchAudioTickId = HL.Field(HL.Number) << -1

ActivityScratchOffLotteryInfo.m_lastScratchTime = HL.Field(HL.Number) << 0

ActivityScratchOffLotteryInfo.m_lastScratchLocalPosition = HL.Field(HL.Userdata)

ActivityScratchOffLotteryInfo.m_lastScratchVelocity = HL.Field(HL.Userdata)


ActivityScratchOffLotteryInfo._OnFirstTimeInit = HL.Override() << function(self)
    self.view.scratchAreaDragHandler.onBeginDrag:AddListener(function(eventData)
        self:_OnScratchAreaBeginDrag(eventData)
    end)
    self.view.scratchAreaDragHandler.onDrag:AddListener(function(eventData)
        self:_OnScratchAreaDrag(eventData)
    end)
    self.view.scratchAreaDragHandler.onEndDrag:AddListener(function(eventData)
        self:_OnScratchAreaEndDrag(eventData)
    end)

    self.m_cellListCache = UIUtils.genCellCache(self.view.cell)
end

ActivityScratchOffLotteryInfo.InitActivityScratchOffLotteryInfo = HL.Method(HL.Table) << function(self, args)
    self:_FirstTimeInit()

    local activitySystem = GameInstance.player.activitySystem

    self.m_activityId = args.activityId
    self.m_activity = activitySystem:GetActivity(args.activityId)
    self.m_lotteryInstanceId = self.m_activity.lotteryInstanceId

    self.m_canScratch = args.canScratch == true
    self.m_onScratchCompletedLocally = args.onScratchCompletedLocally
    self.m_onScratchCompleted = args.onScratchCompleted
end

ActivityScratchOffLotteryInfo.Refresh = HL.Method(HL.Opt(HL.Boolean, HL.Function)) << function(self, playAnimation, onAnimationFinished)
    playAnimation = playAnimation == true

    local activity = self.m_activity
    local isScratchCompleted = self:_IsScratchCompleted()
    local isAllRewardStamp = isScratchCompleted and activity:IsAllRewardStamp()


    if playAnimation then

        if isAllRewardStamp then
            self:GetUICtrl().animationWrapper:Play("activity_lottery_popup_all")
        end

        if activity:HasAnyDoubleRewardNode() then
            AudioAdapter.PostEvent("Au_UI_Event_ScratchOff_Double")
        end
    end


    self.view.stampStateCtrl:SetState(isAllRewardStamp and "All" or "Normal")


    local stampIcon
    if isAllRewardStamp then
        stampIcon = Tables.activityConst.ActivityScratchOffLotteryAllRewardStampIcon
    else
        local success, stampData = Tables.activityScratchOffLotteryStampTable:TryGetValue(activity.doubleRewardStampId)
        if success then
            stampIcon = stampData.stampIconPath
        else
            logger.error(ELogChannel.Activity, "[ScratchOffLottery] Invalid stamp id: " .. tostring(activity.doubleRewardStampId))
        end
    end
    if not string.isEmpty(stampIcon) then
        self.view.stampImg.gameObject:SetActive(true)
        self.view.stampImg:LoadSprite(UIConst.UI_SPRITE_ACTIVITY, stampIcon)
    else
        self.view.stampImg.gameObject:SetActive(false)
    end


    local isNewActivityDay = ActivityUtils.isNewActivityDayUnread(activity.id)
    if isNewActivityDay then

        self.view.stampCoatingAnimationWrapper.gameObject:SetActive(true)
        ActivityUtils.setActivityDayAsRead(activity.id)
    else

        self.view.stampCoatingAnimationWrapper.gameObject:SetActive(false)
    end


    local endTime = activity.endTime
    self.view.countDownText:InitCountDownText(endTime, nil, function(leftTime)
        return string.format(Language.LUA_ACTIVITY_SCRATCH_OFF_LOTTERY_COUNT_DOWN, UIUtils.getLeftTime(leftTime))
    end)


    local nodeCount = activity.nodeCount
    local onNodeAnimationFinished
    if onAnimationFinished then
        local nodeAnimationCount = 0
        onNodeAnimationFinished = function()
            nodeAnimationCount = nodeAnimationCount + 1
            if nodeAnimationCount == nodeCount then
                onAnimationFinished()
            end
        end
    end
    self.m_cellListCache:Refresh(nodeCount, function(cell, index)
        self:_OnUpdateCell(cell, index, playAnimation, onNodeAnimationFinished)
    end)
    local enableFocus = not self.m_canScratch and isScratchCompleted
    InputManagerInst:ToggleBinding(self.view.cellNaviGroup.FocusBindingId, enableFocus)
    self.view.scratchArea.gameObject:SetActive(not isScratchCompleted)
    self.view.scratchAreaDragHandler.enabled = self.m_canScratch
end

ActivityScratchOffLotteryInfo.PlayStampAnimationIn = HL.Method() << function(self)
    if not self.view.stampCoatingAnimationWrapper.gameObject.activeInHierarchy then
        return
    end
    self.view.stampCoatingAnimationWrapper:Play("activity_lottery_first_in")
end

ActivityScratchOffLotteryInfo.BeginScratch = HL.Method(HL.Userdata) << function(self, screenPosition)
    self.view.scratchCoating:BeginScratch(screenPosition)
    self:StartScratchAudio(screenPosition)
end

ActivityScratchOffLotteryInfo.ApplyScratch = HL.Method(HL.Userdata) << function(self, screenPosition)
    self.view.scratchCoating:ApplyScratch(screenPosition)
    self:_UpdateScratchAudioParams(screenPosition)

    local complete = self:_CheckScratch()
    if complete then
        self:_CompleteScratch()
    end
end

ActivityScratchOffLotteryInfo.EndScratch = HL.Method(HL.Userdata) << function(self, screenPosition)
    self.view.scratchCoating:EndScratch(screenPosition)
    self:StopScratchAudio()
end

ActivityScratchOffLotteryInfo.QuickScratch = HL.Method() << function(self)
    local ctrl = self:GetUICtrl()
    ctrl.view.luaPanel:BlockAllInput()
    self.view.scratchAnimationWrapper:Play("activity_lottery_onekey_clear", function()
        ctrl.view.luaPanel:RecoverAllInput()
        self:_CompleteScratch()
    end)
end

ActivityScratchOffLotteryInfo.StartScratchAudio = HL.Method(HL.Userdata) << function(self, screenPosition)
    if self.m_isScratchAudioPlaying then
        return
    end

    self.m_isScratchAudioPlaying = true

    local scratchArea = self.view.scratchArea
    local uiCamera = self:GetUICtrl().uiCamera
    local _, localPosition = Unity.RectTransformUtility.ScreenPointToLocalPointInRectangle(scratchArea, screenPosition, uiCamera)
    self.m_lastScratchTime = Time.unscaledTime
    self.m_lastScratchLocalPosition = localPosition

    self.m_scratchAudioTickId = LuaUpdate:Add("Tick", function()
        if Time.unscaledTime - self.m_lastScratchTime > 0.05 then
            AudioAdapter.SetRtpc("au_ui_mouse_speed", 0)
        end
    end)

    AudioAdapter.PostEvent("Au_UI_Event_ScratchOff_Start")
end

ActivityScratchOffLotteryInfo.StopScratchAudio = HL.Method() << function(self)
    if not self.m_isScratchAudioPlaying then
        return
    end

    self.m_isScratchAudioPlaying = false

    self.m_scratchAudioTickId = LuaUpdate:Remove(self.m_scratchAudioTickId)
    self.m_lastScratchTime = 0
    self.m_lastScratchLocalPosition = nil
    self.m_lastScratchVelocity = nil

    AudioAdapter.PostEvent("Au_UI_Event_ScratchOff_Finish")
    AudioAdapter.SetRtpc("au_ui_mouse_speed", 0)
end

ActivityScratchOffLotteryInfo._UpdateScratchAudioParams = HL.Method(HL.Userdata) << function(self, screenPosition)

    local scratchArea = self.view.scratchArea
    local uiCamera = self:GetUICtrl().uiCamera
    local _, localPosition = Unity.RectTransformUtility.ScreenPointToLocalPointInRectangle(scratchArea, screenPosition, uiCamera)
    local lastLocalPosition = self.m_lastScratchLocalPosition


    local currentTime = Time.unscaledTime
    local velocity = Vector2.zero
    local deltaTime = currentTime - self.m_lastScratchTime
    if deltaTime > 0 then
        velocity = (localPosition - self.m_lastScratchLocalPosition) / deltaTime
    end
    self.m_lastScratchTime = currentTime
    self.m_lastScratchLocalPosition = localPosition


    local scratchRect = scratchArea.rect
    local tolerance = 0.1
    local toleranceRect = Unity.Rect(scratchRect.xMin - tolerance, scratchRect.yMin - tolerance,
        scratchRect.width + tolerance * 2, scratchRect.height + tolerance * 2)
    local insideScratchArea = toleranceRect:Contains(lastLocalPosition)
        or toleranceRect:Contains(localPosition)


    local speed = velocity.magnitude
    local speedRtpc = insideScratchArea and speed or 0
    AudioAdapter.SetRtpc("au_ui_mouse_speed", speedRtpc)


    local stressSpeedThreshold = self.view.config.STRESS_SPEED_THRESHOLD
    if insideScratchArea and speed > stressSpeedThreshold and self.m_lastScratchVelocity then
        local lastSpeed = self.m_lastScratchVelocity.magnitude
        if lastSpeed > stressSpeedThreshold then
            local dot = Vector2.Dot(self.m_lastScratchVelocity / lastSpeed, velocity / speed)
            local dotThreshold = math.cos(math.rad(self.view.config.STRESS_ANGLE_THRESHOLD))
            if dot < dotThreshold then
                AudioAdapter.PostEvent("Au_UI_Event_ScratchOff_Stress")
            end
        end
    end
    self.m_lastScratchVelocity = velocity


    local normalizedX = math.max(0, math.min(1, (localPosition.x - scratchRect.xMin) / scratchRect.width))
    AudioAdapter.SetRtpc("au_ui_mouse_x_position", normalizedX)
end

ActivityScratchOffLotteryInfo._CheckScratch = HL.Method().Return(HL.Boolean) << function(self)
    if self:_IsScratchCompleted() then
        return true
    end

    local progress = self.view.scratchCoating:OutputCompletePercent()
    if progress < Tables.activityConst.ActivityScratchOffLotteryCompletionThreshold then
        return false
    end
    if not self.view.scratchCoating:FlagCompleteMarkPoint() then
        return false
    end
    return true
end

ActivityScratchOffLotteryInfo._IsScratchCompleted = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_activity.isScratchCompleted or self.m_isScratchCompletedLocally
end

ActivityScratchOffLotteryInfo._CompleteScratch = HL.Method() << function(self)
    if self:_IsScratchCompleted() then
        return
    end

    self.m_isScratchCompletedLocally = true
    if self.m_onScratchCompletedLocally then
        self.m_onScratchCompletedLocally()
    end

    self:StopScratchAudio()
    self:GetUICtrl().view.luaPanel:BlockAllInput()


    self:Refresh(true, function()

        self.m_activity:CompleteScratch(self.m_lotteryInstanceId, function()
            self:GetUICtrl().view.luaPanel:RecoverAllInput()
            if self.m_onScratchCompleted then
                self.m_onScratchCompleted()
            end
        end)
    end)
end

ActivityScratchOffLotteryInfo._OnUpdateCell = HL.Method(HL.Table, HL.Number, HL.Boolean, HL.Function) << function(self, cell, index, playAnimation, onAnimationFinished)
    local activity = self.m_activity
    local node = activity:GetNode(CSIndex(index))
    local isScratchCompleted = self:_IsScratchCompleted()
    local isAllRewardStamp = isScratchCompleted and activity:IsAllRewardStamp()
    local isDoubleRewardStamp = activity:IsDoubleRewardStamp(node.stampId)


    local stateName
    local animationName
    if isScratchCompleted then

        if isAllRewardStamp then
            stateName = "All"
            animationName = "activity_lottery_allin"
        elseif isDoubleRewardStamp then
            stateName = "Double"
            animationName = "activity_lottery_doublein"
        else
            stateName = "Normal"
            animationName = "activity_lottery_normal_in"
        end
    else

        stateName = "Normal"
    end
    cell.stateCtrl:SetState(stateName)


    if playAnimation then
        if animationName then
            cell.animationWrapper:Play(animationName, onAnimationFinished)
        elseif onAnimationFinished then
            onAnimationFinished()
        end
    end


    cell.numText.text = string.format("%02d", index)


    local itemCount = node.itemCount
    if not isScratchCompleted and (isAllRewardStamp or isDoubleRewardStamp) then
        itemCount = math.floor(node.itemCount / 2)
    end
    local itemBundle = { id = node.itemId, count = itemCount }
    cell.item:InitItem(itemBundle, true)
    cell.item:SetExtraInfo({ isSideTips = true })


    local stampIcon
    if isAllRewardStamp then
        stampIcon = Tables.activityConst.ActivityScratchOffLotteryAllRewardStampIcon
    else
        local success, stampData = Tables.activityScratchOffLotteryStampTable:TryGetValue(node.stampId)
        if success then
            stampIcon = stampData.stampIconPath
        else
            logger.error(ELogChannel.Activity, "[ScratchOffLottery] Invalid stamp id: " .. tostring(node.stampId))
        end
    end
    if not string.isEmpty(stampIcon) then
        cell.stampImg.gameObject:SetActive(true)
        cell.stampImg:LoadSprite(UIConst.UI_SPRITE_ACTIVITY, stampIcon)
    else
        cell.stampImg.gameObject:SetActive(false)
    end
    if isAllRewardStamp or isDoubleRewardStamp then
        cell.stampImg.color = self.view.stampImg.color
    end


    cell.coatingImg.gameObject:SetActive(not self.m_canScratch and not isScratchCompleted)
end

ActivityScratchOffLotteryInfo._OnScratchAreaBeginDrag = HL.Method(HL.Userdata) << function(self, eventData)
    if not self.m_canScratch then
        return
    end
    if self:_IsScratchCompleted() then
        return
    end

    self:BeginScratch(eventData.position)
end

ActivityScratchOffLotteryInfo._OnScratchAreaDrag = HL.Method(HL.Userdata) << function(self, eventData)
    if not self.m_canScratch then
        return
    end
    if self:_IsScratchCompleted() then
        return
    end

    self:ApplyScratch(eventData.position)
end

ActivityScratchOffLotteryInfo._OnScratchAreaEndDrag = HL.Method(HL.Userdata) << function(self, eventData)
    if not self.m_canScratch then
        return
    end
    if self:_IsScratchCompleted() then
        return
    end

    self:EndScratch(eventData.position)
end

HL.Commit(ActivityScratchOffLotteryInfo)
return ActivityScratchOffLotteryInfo
