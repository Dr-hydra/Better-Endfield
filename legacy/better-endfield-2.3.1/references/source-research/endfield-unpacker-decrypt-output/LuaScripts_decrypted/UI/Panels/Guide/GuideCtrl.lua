local GuideStepType = CS.Beyond.Gameplay.GuideStepType
local HintType = CS.Beyond.Gameplay.GuideUIHighlightInfo.HintType
local TextStyle = CS.Beyond.Gameplay.GuideTextInfo.Style

local InfoAtFirstHighlightStyle = {
    Force = "Force",
    Weak = "Weak",
    FullScreenForce = "FullScreenForce",
}

local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.Guide
GuideCtrl = HL.Class('GuideCtrl', uiCtrl.UICtrl)





GuideCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.HIDE_GUIDE_STEP] = 'HideGuideStep',
    [MessageConst.CLEAR_GUIDE_STEP] = '_ClearStep',
    [MessageConst.ON_COMPLETE_GUIDE_GROUP] = 'OnCompleteGuide',
    [MessageConst.ON_INTERRUPT_GUIDE] = 'OnInterruptGuide',
    [MessageConst.ON_UI_PANEL_START_OPEN] = 'OnPanelOpened',
    [MessageConst.ON_UI_PANEL_SHOW] = 'OnPanelOpened',
    [MessageConst.ON_UI_PANEL_CLOSED] = 'OnPanelClosed',
    [MessageConst.ON_UI_PANEL_HIDE] = 'OnPanelClosed',
    [MessageConst.ON_UI_PHASE_OPENED] = 'OnPhaseOpened',
    [MessageConst.ON_UI_PHASE_EXITED] = 'OnPhaseExited',
    [MessageConst.ON_CONTROLLER_TYPE_CHANGED] = '_OnControllerTypeChanged',
}

local HIGHLIGHT_DIALOG_ARROW_MIDDLE_RANG = 0.4

local USE_LEFT_HAND_WIDTH_RATIO = 0.06

local RIGHT_ARROW_MODIFIED_OFFSET = -9

GuideCtrl.m_curStepInfo = HL.Field(CS.Beyond.Gameplay.GuideStepInfo)

GuideCtrl.m_isCurForceStep = HL.Field(HL.Boolean) << false

GuideCtrl.m_waitClickUIHighlight = HL.Field(HL.Boolean) << false

GuideCtrl.m_stepStartTime = HL.Field(HL.Number) << -1

GuideCtrl.m_curUIHighlightInfos = HL.Field(HL.Table)

GuideCtrl.m_uiHighlightCells = HL.Field(HL.Forward('UIListCache'))

GuideCtrl.m_firstHighlightUICell = HL.Field(HL.Table)

GuideCtrl.m_dialogNodeMap = HL.Field(HL.Table)

GuideCtrl.m_highlightDialogPadding = HL.Field(HL.Table)

GuideCtrl.m_hasFinishCondition = HL.Field(HL.Boolean) << false

GuideCtrl.m_guideStepInterval = HL.Field(HL.Number) << 0


GuideCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.fullScreenBtn.onClick:AddListener(function()
        self:_TryGotoNextStep(true)
    end)

    UIUtils.bindInputPlayerAction("guide_click", function()
        if not DeviceInfo.usingTouch then
            self:_TryGotoNextStep(true)
        end
    end, self.view.fullScreenBtn.groupId)

    self.m_uiHighlightCells = UIUtils.genCellCache(self.view.uiHighlightCell)
    self.m_dialogNodeMap = {
        [TextStyle.BigLeftTop] = self.view.bigLeftTopDialogNode,
        [TextStyle.BigLeftBottom] = self.view.bigLeftBottomDialogNode,
        [TextStyle.TitleMiddleTop] = self.view.titleMiddleTopNode,
        [TextStyle.InfoAtFirstUIHighlight] = self.view.infoAtFirstUIHighlightDialogNode,
    }

    self.m_highlightDialogPadding = {
        top = self.view.config.UI_HIGHLIGHT_DIALOG_PADDING_TOP,
        bottom = self.view.config.UI_HIGHLIGHT_DIALOG_PADDING_BOTTOM,
        left = self.view.config.UI_HIGHLIGHT_DIALOG_PADDING_LEFT,
        right = self.view.config.UI_HIGHLIGHT_DIALOG_PADDING_RIGHT,
    }

    self:_HideAllOnCreate()
    self:_InitDialogControllerHint()
end

GuideCtrl.OnShow = HL.Override() << function(self)
    self:_AddTick()
end
GuideCtrl.OnHide = HL.Override() << function(self)
    self:_Clear()
end
GuideCtrl.OnClose = HL.Override() << function(self)
    self:_Clear()
end

GuideCtrl._HideAllOnCreate = HL.Method() << function(self)
    self.view.helperNode.gameObject:SetActive(false)
    for _, node in pairs(self.m_dialogNodeMap) do
        node.gameObject:SetActive(false)
    end
end




GuideCtrl.m_lateTickKey = HL.Field(HL.Number) << -1

GuideCtrl._AddTick = HL.Method() << function(self)
    if self.m_lateTickKey > 0 then
        return
    end
    self.m_lateTickKey = LuaUpdate:Add("LateTick", function(deltaTime)
        self:_LateTick()
    end)
end

GuideCtrl._Clear = HL.Method() << function(self)
    LuaUpdate:Remove(self.m_lateTickKey)
    self.m_lateTickKey = -1
    self.m_curHideMediaTime = -1
    Notify(MessageConst.TOGGLE_LEVEL_CAMERA_MOVE, { "guide", true })
    CoroutineManager:ClearAllCoroutine(self)

    CS.Beyond.UI.UIActionKeyHint.s_stopCheckBindingEnabledForGuide = false
    CS.Beyond.UI.UIAutoCloseArea.s_stopCheckShouldClose = false
end

GuideCtrl._LateTick = HL.Method() << function(self)
    if self.m_curUIHighlightInfos then
        local isForce = self.m_curStepInfo.type ~= GuideStepType.Weak
        for _, v in ipairs(self.m_curUIHighlightInfos) do
            if IsNull(v.targetTrans) or v.targetTrans.gameObject.name ~= v.oriName then

                if NotNull(v.targetTrans) then
                    v.targetTrans = v.targetTrans.parent:Find(v.oriName)
                end
                if IsNull(v.targetTrans) then
                    if v.is3DUI then
                        v.targetTrans = GameObject.Find(v.path)
                    else
                        v.targetTrans = UIManager.uiRoot.transform:Find(v.path) or UIManager.worldUIRoot.transform:Find(v.path)
                    end
                end
                if NotNull(v.targetTrans) then
                    local button, toggle, dropdown, keyHint = self:_GetClickTarget(v.targetTrans)
                    v.button = button

                    v.cell.button:ClearTarget()
                    v.cell.button.targetButton = button
                    v.cell.button.targetToggle = toggle
                    v.cell.button.targetDropdown = dropdown
                    v.cell.button.targetKeyHintActionId = keyHint and keyHint:GetActionId() or nil
                    if isForce then
                        v.cell.button:CopyTargetBinding()
                    end
                end
            end

            if NotNull(v.targetTrans) then
                local rect = UIUtils.getUIRectOfRectTransform(v.targetTrans, v.is3DUI and CameraManager.mainCamera or self.uiCamera)
                self:_SyncUIHighlightTargetRect(v.cell, rect, v.needMask, v.isCircle)
                if not isForce then
                    local isActive = UIUtils.isUIGOActive(v.targetTrans.gameObject) and (not v.button or (v.ignoreBindingEnabled or v.button.groupEnabled))
                    if v.cell.gameObject.activeSelf ~= isActive then
                        v.cell.gameObject:SetActive(isActive)

                        if isActive then
                            v.cell.animationWrapper:Play(v.isCircle and "guideuihighlight_loop02" or "guideuihighlight_loop01")
                        end

                        if isActive then
                            GameInstance.player.guide:ResumeWeakGuideAutoFinishStepTimer()
                        else
                            GameInstance.player.guide:PauseWeakGuideAutoFinishStepTimer()
                        end
                    end
                end
            else

                if not isForce and v.cell.gameObject.activeSelf ~= false then
                    v.cell.gameObject:SetActive(false)
                    GameInstance.player.guide:ResumeWeakGuideAutoFinishStepTimer()
                end
            end
        end
        self:_SyncInfoAtFirstUIHighlight(self.m_curUIHighlightInfos[1])
    end

    if self.m_needRefreshTrackPoint then
        self:_RefreshHelperTrackPointDistance()
    end
end






GuideCtrl.ShowGuideStep = HL.StaticMethod(HL.Table) << function(args)

    local stepInfo, hasFinishCondition = unpack(args)

    local self = UIManager:AutoOpen(PANEL_ID)

    self:_ClearStep()
    if stepInfo.type == GuideStepType.Weak and self:_StopShowWeakGuideIfNeed(stepInfo.uiHighlightInfos) then
        return
    end

    self.m_curStepInfo = stepInfo
    self.m_stepStartTime = Time.unscaledTime
    self.m_hasFinishCondition = hasFinishCondition or false

    self.view.emptyMask.gameObject:SetActiveIfNecessary(false)
    self.view.main.gameObject:SetActiveIfNecessary(true)

    local isForce = stepInfo.type ~= GuideStepType.Weak
    self.m_isCurForceStep = isForce
    self:_RefreshUIHighlight(stepInfo.uiHighlightInfos, isForce)
    self:_RefreshDialog(stepInfo.textInfos)
    self:_RefreshDialogControllerHint(stepInfo, hasFinishCondition == true)
    local isMedia = self:_RefreshMedia(stepInfo.mediaInfos)
    local isHelper = self:_RefreshHelper()
    local needBlockInput = false
    local needFullScreenBtn = false
    local hasHighlightInfo = stepInfo.uiHighlightInfos.Count > 0
    if isForce then








        if hasHighlightInfo or not string.isEmpty(stepInfo.enableActionId) or not hasFinishCondition then
            needBlockInput = true
        end







        if not hasFinishCondition and not self.m_waitClickUIHighlight then
            needFullScreenBtn = true
        end
    end
    Notify(MessageConst.TOGGLE_LEVEL_CAMERA_MOVE, { "guide", not needBlockInput })
    local stopCheckBinding = needBlockInput and not isMedia and not isHelper
    CS.Beyond.UI.UIActionKeyHint.s_stopCheckBindingEnabledForGuide = stopCheckBinding
    CS.Beyond.UI.UIAutoCloseArea.s_stopCheckShouldClose = needBlockInput
    self:ChangeCurPanelBlockSetting(needBlockInput)
    local showFullScreenBtn = needFullScreenBtn and not isMedia
    self.view.fullScreenBtn.gameObject:SetActive(showFullScreenBtn)
    if showFullScreenBtn then
        self.m_guideStepInterval = stepInfo.stepInterval
    end

    if self.m_hasInfoAtUI then
        self:_RefreshInfoAtFirstUIHighlightStyle(needFullScreenBtn, isForce)
    end

    self:_RefreshDialogHintNode(needFullScreenBtn)


    self.view.maskImg.gameObject:SetActive(needFullScreenBtn and not self.view.uiHighlightNode.gameObject.activeSelf)

    self:_AddTick()

    if DeviceInfo.usingController then
        if stepInfo.hideControllerHintBar then
            UIManager:HideWithKey(PanelId.ControllerHint, "Guide")
        end
    end
end

GuideCtrl.HideGuideStep = HL.Method(HL.Opt(HL.Any)) << function(self, args)
    args = args or {}
    local groupInfo, isClientInterrupt = unpack(args)

    CS.Beyond.UI.UIActionKeyHint.s_stopCheckBindingEnabledForGuide = false
    CS.Beyond.UI.UIAutoCloseArea.s_stopCheckShouldClose = false
    Notify(MessageConst.HIDE_GUIDE_MEDIA)
    if not self:IsShow(true) then
        return
    end
    self:ChangeCurPanelBlockSetting(false)
    self:_Clear()

    local groupId = GameInstance.player.guide.curGuideGroupId
    if not isClientInterrupt and self.view.dialogNode.gameObject.activeInHierarchy then
        for _, node in pairs(self.m_dialogNodeMap) do
            if node.animationWrapper.curState == CS.Beyond.UI.UIConst.AnimationState.Out then
                return
            end
        end
        local isFirst = true
        local animPlay = false
        for t, dialogNode in pairs(self.m_dialogNodeMap) do
            if dialogNode.gameObject.activeInHierarchy then
                if t == TextStyle.InfoAtFirstUIHighlight then
                    local innerIsFirst = isFirst
                    animPlay = true
                    dialogNode.animationWrapper:Play(dialogNode.outAniName, function()
                        dialogNode.animationWrapper.curState = CS.Beyond.UI.UIConst.AnimationState.Stop
                        self:_TryHideGuidePanel(groupId)
                    end, CS.Beyond.UI.UIConst.AnimationState.Out)
                else
                    if isFirst then
                        animPlay = true
                        dialogNode.animationWrapper:PlayOutAnimation(function()
                            self:_TryHideGuidePanel(groupId)
                        end)
                    else
                        animPlay = true
                        dialogNode.animationWrapper:PlayOutAnimation()
                    end
                end
                isFirst = false
            end
        end
        if not animPlay then
            self:_TryHideGuidePanel(groupId)
        end
    else
        self:_TryHideGuidePanel(groupId)
    end
    self.view.uiHighlightNode.gameObject:SetActiveIfNecessary(false)
end

GuideCtrl.ShowGuideEmptyMask = HL.StaticMethod() << function()
    local self = UIManager:AutoOpen(PANEL_ID)
    self.view.main.gameObject:SetActiveIfNecessary(false)
    self:_RefreshEmptyMaskState(true)
end

GuideCtrl.OnPanelOpened = HL.Method(HL.String) << function(self, panelName)
    self:_RefreshHelperVisibleStateByPanelOrPhaseState(panelName, true, true)
end

GuideCtrl.OnPanelClosed = HL.Method(HL.String) << function(self, panelName)
    self:_RefreshHelperVisibleStateByPanelOrPhaseState(panelName, false, true)
end

GuideCtrl.OnPhaseOpened = HL.Method(HL.String) << function(self, phaseName)
    self:_RefreshHelperVisibleStateByPanelOrPhaseState(phaseName, true, false)
end

GuideCtrl.OnPhaseExited = HL.Method(HL.String) << function(self, phaseName)
    self:_RefreshHelperVisibleStateByPanelOrPhaseState(phaseName, false, false)
end

GuideCtrl.OnInterruptGuide = HL.Method() << function(self)
    self:Hide()
end

GuideCtrl.OnCompleteGuide = HL.Method(HL.Any) << function(self, args)




    local groupId, clearPendingGroup = unpack(args)
    if clearPendingGroup then
        self:_RefreshEmptyMaskState(false)
        self:Hide()
    end
end






GuideCtrl._RefreshEmptyMaskState = HL.Method(HL.Boolean) << function(self, needShow)
    self.view.emptyMask.gameObject:SetActiveIfNecessary(needShow)
    CS.Beyond.UI.UIActionKeyHint.s_stopCheckBindingEnabledForGuide = needShow
    CS.Beyond.UI.UIAutoCloseArea.s_stopCheckShouldClose = needShow
    self:ChangeCurPanelBlockSetting(needShow)
end

GuideCtrl._StopShowWeakGuideIfNeed = HL.Method(HL.Userdata).Return(HL.Boolean) << function(self, highlightInfos)
    if highlightInfos == nil or highlightInfos.Count == 0 then
        return false
    end
    for index = 0, highlightInfos.Count - 1 do
        local info = highlightInfos[index]
        local path = info.path
        local trans = self:_GetHighlightTarget(path)
        if not NotNull(trans) then
            GameInstance.player.guide:OnWeakGuideFindHighlightFailedOnInit()
            return true
        end
    end
    return false
end

GuideCtrl._OnClickFakeButton = HL.Method() << function(self)
    if self.m_waitClickUIHighlight and not self.m_hasFinishCondition then
        self.m_waitClickUIHighlight = false
        self:_TryGotoNextStep(false)
    end
end

GuideCtrl._TryGotoNextStep = HL.Method(HL.Boolean) << function(self, checkInterval)

    if not self.m_curStepInfo then
        return
    end

    if self.m_waitClickUIHighlight then
        return
    end
    if checkInterval and Time.unscaledTime < self.m_stepStartTime + self.m_guideStepInterval then
        return
    end

    GameInstance.player.guide:OnGuideStepUIClicked()
end

GuideCtrl._ClearStep = HL.Method() << function(self)
    self.m_waitClickUIHighlight = false
    self.m_curUIHighlightInfos = nil
    self.m_firstHighlightUICell = nil
    self.m_curStepInfo = nil
    self.m_guideStepInterval = UIConst.GUIDE_STEP_MIN_INTERVAL

    self.m_uiHighlightCells:Update(function(cell)
        cell.button:ClearTarget()
    end)

    UIManager:ShowWithKey(PanelId.ControllerHint, "Guide")
    Notify(MessageConst.REFRESH_CONTROLLER_HINT_CONTENT_IMMEDIATELY)
end






GuideCtrl.m_hasInfoAtUI = HL.Field(HL.Boolean) << false

GuideCtrl._RefreshDialog = HL.Method(HL.Userdata) << function(self, textInfos)

    if not textInfos or textInfos.Count == 0 then
        for _, node in pairs(self.m_dialogNodeMap) do
            node.animationWrapper:ClearTween()
        end
        self.view.dialogNode.gameObject:SetActiveIfNecessary(false)
        return
    end
    self.view.dialogNode.gameObject:SetActiveIfNecessary(true)

    self.view.infoAtFirstUIHighlightDialogNode.gameObject:SetActive(false)
    self.view.bigLeftTopDialogNode.gameObject:SetActive(false)
    self.view.bigLeftBottomDialogNode.gameObject:SetActive(false)
    self.view.titleMiddleTopNode.gameObject:SetActive(false)
    self.m_hasInfoAtUI = false
    for _, info in pairs(textInfos) do
        local node = self.m_dialogNodeMap[info.style]
        node.gameObject:SetActive(true)

        local text = Utils.getGuideText(info.txtId)
        local result = InputManager.ParseTextActionId(text)
        node.content:SetAndResolveTextStyle(result)
        LayoutRebuilder.ForceRebuildLayoutImmediate(node.rectTransform)
        if info.style == TextStyle.InfoAtFirstUIHighlight then
            self.m_hasInfoAtUI = true
            self:_SyncInfoAtFirstUIHighlight(self.m_curUIHighlightInfos[1])

        else
            node.animationWrapper:PlayInAnimation()
        end
    end
end

GuideCtrl._RefreshDialogHintNode = HL.Method(HL.Boolean) << function(self, needFullScreenBtn)
    for _, dialogNode in pairs(self.m_dialogNodeMap) do
        dialogNode.hintNode.gameObject:SetActiveIfNecessary(needFullScreenBtn and not DeviceInfo.usingController)
    end
end

GuideCtrl._SyncInfoAtFirstUIHighlight = HL.Method(HL.Table) << function(self, info)
    if not info or not self.m_hasInfoAtUI then
        return
    end


    if IsNull(info.targetTrans) then


        return
    end

    local node = self.view.infoAtFirstUIHighlightDialogNode
    local hasArrow = self.view.infoAtFirstUIHighlightDialogNode.forceArrow.gameObject.activeInHierarchy
    UIUtils.updateTipsPosition(node.rectTransform,
        info.targetTrans,
        self.view.rectTransform, self.uiCamera,
        UIConst.UI_TIPS_POS_TYPE.GuideTips,
        self.m_highlightDialogPadding,
        hasArrow and self.view.config.DIALOG_NODE_ARROW_OFFSET_X or 0
    )
    self:_SyncUIHighlightDialogArrowState(info.targetTrans)

    local isForce = self.m_curStepInfo.type ~= GuideStepType.Weak
    if not isForce then
        local isActive = UIUtils.isUIGOActive(info.targetTrans.gameObject) and (not info.button or (info.ignoreBindingEnabled or info.button.groupEnabled))
        if node.gameObject.activeSelf ~= isActive then
            node.gameObject:SetActive(isActive)
        end
    end
end

GuideCtrl._RefreshInfoAtFirstUIHighlightStyle = HL.Method(HL.Boolean, HL.Boolean) << function(self, needFullScreenBtn, isForce)


    local dialogNode = self.view.infoAtFirstUIHighlightDialogNode
    local state = ""
    if isForce then
        state = needFullScreenBtn and InfoAtFirstHighlightStyle.FullScreenForce or InfoAtFirstHighlightStyle.Force
    else
        state = InfoAtFirstHighlightStyle.Weak
    end
    dialogNode.stateController:SetState(state)

    local hasArrow = dialogNode.forceArrow.gameObject.activeSelf
    local isRight = dialogNode.right
    if not hasArrow then
        if isForce then
            dialogNode.animationWrapper:Play(isRight and "hightdialogcoverright_in" or "hightdialogcoverleft_in")
            dialogNode.outAniName = "hightdialogcover_out"
        else
            dialogNode.animationWrapper:Play(isRight and "hightdialognlowright_in" or "hightdialognlowleft_in")
            dialogNode.outAniName = "hightdialognlow_out"
        end
    else
        if isRight then
            dialogNode.animationWrapper:Play("hightdialogarrowright_in", function()
                dialogNode.animationWrapper:Play("hightdialogarrowright_loop")
            end)
            dialogNode.outAniName = "hightdialogarrowright_out"
        else
            dialogNode.animationWrapper:Play("hightdialogarrowleft_in", function()
                dialogNode.animationWrapper:Play("hightdialogarrowleft_loop")
            end)
            dialogNode.outAniName = "hightdialogarrowleft_out"
        end
    end
end

GuideCtrl._SyncUIHighlightDialogArrowState = HL.Method(HL.Userdata) << function(self, highlightRectTransform)
    if highlightRectTransform == nil then
        return
    end

    local dialogNode = self.view.infoAtFirstUIHighlightDialogNode

    local dialogScreenRect = UIUtils.getTransformScreenRect(dialogNode.rectTransform, self.uiCamera)
    local highlightScreenRect = UIUtils.getTransformScreenRect(highlightRectTransform, self.uiCamera)

    local dialogScreenRectHeight = highlightScreenRect.height
    local middleRangeOffset = dialogScreenRectHeight * HIGHLIGHT_DIALOG_ARROW_MIDDLE_RANG
    local right = highlightScreenRect.center.x > dialogScreenRect.center.x

    dialogNode.right = right

    if not dialogNode.forceArrow.gameObject.activeInHierarchy then
        return
    end

    local up = highlightScreenRect.center.y < dialogScreenRect.center.y
    local middle = highlightScreenRect.center.y >= dialogScreenRect.center.y - middleRangeOffset and
            highlightScreenRect.center.y <= dialogScreenRect.center.y + middleRangeOffset

    local arrowX, arrowY
    if middle then
        arrowY = 0
    else
        local halfHeight = dialogNode.rectTransform.rect.height / 2 + self.view.config.UI_HIGHLIGHT_DIALOG_ARROW_OFFSET_Y
        arrowY = up and halfHeight or -halfHeight
    end
    local halfWidth = dialogNode.rectTransform.rect.width / 2 + self.view.config.UI_HIGHLIGHT_DIALOG_ARROW_OFFSET_X
    arrowX = right and halfWidth or -halfWidth
    if right then
        arrowX = arrowX + RIGHT_ARROW_MODIFIED_OFFSET
    end

    dialogNode.forceArrow.anchoredPosition = Vector2(arrowX, arrowY)
    dialogNode.forceArrow.localScale = Vector3(right and -1 or 1, 1, 1)
end

GuideCtrl._TryHideGuidePanel = HL.Method(HL.String) << function(self, lastGroupId)
    local curGroupId = GameInstance.player.guide.curGuideGroupId
    local pendingGroupId = GameInstance.player.guide.pendingGuideGroupId
    local nextGroupId = string.isEmpty(curGroupId) and pendingGroupId or curGroupId
    if not string.isEmpty(nextGroupId) and lastGroupId ~= curGroupId then

        return
    end

    self:Hide()
end






GuideCtrl.m_curHideMediaTime = HL.Field(HL.Number) << -1

local FORCE_HIDE_MEDIA_TIMER_DURATION = 3

GuideCtrl._RefreshMedia = HL.Method(HL.Userdata).Return(HL.Boolean) << function(self, mediaInfos)
    local count = mediaInfos.Count
    if not mediaInfos or count == 0 then
        Notify(MessageConst.HIDE_GUIDE_MEDIA)
        return false
    end
    self.m_curHideMediaTime = -1
    Notify(MessageConst.SHOW_GUIDE_MEDIA, {
        mediaInfos = mediaInfos,
        onComplete = function()
            if self:_ForceHideMediaIfNeed() then
                return
            end
            self:_TryGotoNextStep(false)
        end
    })
    return true
end

GuideCtrl._ForceHideMediaIfNeed = HL.Method().Return(HL.Boolean) << function(self)

    if self.m_curStepInfo ~= nil then
        self.m_curHideMediaTime = Time.time
        return false
    end

    if self.m_curHideMediaTime > 0 and Time.time - self.m_curHideMediaTime > FORCE_HIDE_MEDIA_TIMER_DURATION then
        self:HideGuideStep()
        self.m_curHideMediaTime = -1
        logger.error(ELogChannel.Guide, "图文引导", GameInstance.player.guide.curGuideGroupId, "未正常关闭")
        return true
    end

    return false
end






GuideCtrl.m_needRefreshTrackPoint = HL.Field(HL.Boolean) << false

GuideCtrl.m_curTrackPointKey = HL.Field(HL.String) << ""

GuideCtrl.m_helperCoverVisible = HL.Field(HL.Boolean) << false

GuideCtrl._RefreshHelper = HL.Method().Return(HL.Boolean) << function(self)
    if self.m_curStepInfo.type ~= GuideStepType.ForceHelper then
        self.view.helperNode.animationWrapper:PlayOutAnimation(function()
            self.view.helperNode.gameObject:SetActiveIfNecessary(false)
        end)
        return false
    end

    local helperNode = self.view.helperNode
    local helperInfo = self.m_curStepInfo.helperInfo
    local contentText = Utils.getGuideText(helperInfo.contentTextId)
    helperNode.content:SetAndResolveTextStyle(InputManager.ParseTextActionId(contentText))
    local useCustomImage = not string.isEmpty(helperInfo.helperImagePath)
    if useCustomImage then
        helperNode.customHelperImage:LoadSprite(UIConst.UI_SPRITE_GUIDE, helperInfo.helperImagePath)
    end
    helperNode.defaultHelperImage.gameObject:SetActiveIfNecessary(not useCustomImage)
    helperNode.customHelperImage.gameObject:SetActiveIfNecessary(useCustomImage)

    self.m_needRefreshTrackPoint = not string.isEmpty(helperInfo.trackingPointKey)
    self.m_curTrackPointKey = helperInfo.trackingPointKey
    helperNode.targetNode.gameObject:SetActiveIfNecessary(self.m_needRefreshTrackPoint)

    helperNode.switchButton.onClick:RemoveAllListeners()
    helperNode.switchButton.onClick:AddListener(function()
        self:_RefreshHelperCoverVisibleState(not self.m_helperCoverVisible)
    end)
    self:_RefreshHelperCoverVisibleState(true)

    helperNode.controlNode.gameObject:SetActiveIfNecessary(true)
    helperNode.gameObject:SetActiveIfNecessary(true)
    helperNode.animationWrapper:PlayInAnimation()

    if self.m_needRefreshTrackPoint then
        self:_RefreshHelperTrackPointDistance()
    end

    return true
end

GuideCtrl._RefreshHelperTrackPointDistance = HL.Method() << function(self)
    if string.isEmpty(self.m_curTrackPointKey) then
        return
    end

    local distance = GameInstance.player.commonTrackingSystem:GetDistanceByTrackingPointKey(self.m_curTrackPointKey)
    if distance > 0 then
        self.view.helperNode.targetNode.distanceText.text = string.format("%d", math.floor(distance))
        self.view.helperNode.targetNode.gameObject:SetActive(true)
    else
        self.view.helperNode.targetNode.gameObject:SetActive(false)
    end
end

GuideCtrl._RefreshHelperCoverVisibleState = HL.Method(HL.Boolean) << function(self, isVisible)
    if isVisible then
        self.view.helperNode.cover:PlayInAnimation()
    else
        self.view.helperNode.cover:PlayOutAnimation()
    end
    self.view.helperNode.switchIcon.localScale = Vector3(isVisible and 1 or -1, 1, 1)
    self.m_helperCoverVisible = isVisible
end

GuideCtrl._RefreshHelperVisibleStateByPanelOrPhaseState = HL.Method(HL.String, HL.Boolean, HL.Boolean) << function(self, name, isOpened, isPanel)
    if not self:IsShow() or IsNull(self.view.helperNode.gameObject) or not self.view.helperNode.gameObject.activeSelf then
        return
    end

    local invisibleList = isPanel and
            Tables.globalConst.guideHelperInvisiblePanelList or
            Tables.globalConst.guideHelperInvisiblePhaseList
    local isInvisible = false
    for _, invisibleName in pairs(invisibleList) do
        if invisibleName == name then
            isInvisible = isOpened
            if not isInvisible then
                self.view.helperNode.controlNode.gameObject:SetActiveIfNecessary(true)
            else
                self.view.helperNode.controlNode.gameObject:SetActiveIfNecessary(false)
            end
            break
        end
    end
end






GuideCtrl._RefreshUIHighlight = HL.Method(HL.Userdata, HL.Boolean) << function(self, highlightInfos, needMask)

    if not highlightInfos or highlightInfos.Count == 0 then
        self.view.uiHighlightNode.gameObject:SetActiveIfNecessary(false)
        return
    end
    self.view.uiHighlightNode.gameObject:SetActiveIfNecessary(true)


    Notify(MessageConst.REFRESH_CONTROLLER_HINT_CONTENT_IMMEDIATELY)

    self.m_curUIHighlightInfos = {}
    local showDrag = false
    self.view.dragHint.hand:DOKill()
    self.m_uiHighlightCells:Refresh(highlightInfos.Count, function(cell, index)
        local info = highlightInfos[CSIndex(index)]
        self:_HighlightUITarget(cell, info, needMask)
        if index == 1 then
            self.m_firstHighlightUICell = cell
        end
        if info.hintType == HintType.DragFrom or info.hintType == HintType.DragTo then
            showDrag = true
        end
    end)
    self.view.dragHint.gameObject:SetActiveIfNecessary(showDrag)
    if showDrag then
        self.view.dragHint.hand.anchoredPosition = Vector2.zero
        self.view.dragHint.hand:DOMove(self.view.dragHint.toPosition, 1.3, false):SetEase(CS.DG.Tweening.Ease.OutFlash):SetLoops(-1, CS.DG.Tweening.LoopType.Restart)
    end
end

GuideCtrl._HighlightUITarget = HL.Method(HL.Table, HL.Userdata, HL.Boolean)
        << function(self, cell, info, needMask)
    local path = info.path
    local trans, is3DUI = self:_GetHighlightTarget(path)
    if not trans then
        logger.error(ELogChannel.Guide, "引导", GameInstance.player.guide.curGuideGroupId, "UI路径不对", path)
        if BEYOND_DEBUG then

            Notify(MessageConst.SHOW_TOAST, string.format("引导的UI路径不对 %s\n详情看Log", path))
            self:_ShowCompleteGuidePopup()
        end
        return
    end

    if string.startWith(path, "ItemTipsPanel") then

        Notify(MessageConst.TOGGLE_ITEM_TIPS_AUTO_CLOSE, false)
    end

    trans = trans:GetComponent("RectTransform")

    needMask = needMask and info.needBlackMask

    local isCircle = info.shape == CS.Beyond.Gameplay.GuideUIHighlightInfo.Shape.Circle
    if isCircle then
        cell.simpleStateController:SetState(needMask and "Circle" or "CircleNoMask")
    else
        cell.simpleStateController:SetState("Rect")
        cell.forceFrame.gameObject:SetActive(self.m_isCurForceStep)
        cell.weakFrame.gameObject:SetActive(not self.m_isCurForceStep)
    end

    cell.animationWrapper:PlayInAnimation(function()
        cell.animationWrapper:Play(isCircle and "guideuihighlight_loop02" or "guideuihighlight_loop01")
    end)

    local targetRect = UIUtils.getUIRectOfRectTransform(trans, is3DUI and CameraManager.mainCamera or self.uiCamera)
    self:_SyncUIHighlightTargetRect(cell, targetRect, needMask, isCircle)
    local uiInfo = {
        cell = cell,
        targetTrans = trans,
        oriName = trans.gameObject.name,
        is3DUI = is3DUI,
        path = info.path,
        isCircle = isCircle,
        needMask = needMask,
        ignoreBindingEnabled = info.ignoreBindingEnabled,
    }
    table.insert(self.m_curUIHighlightInfos, uiInfo)
    cell.gameObject:SetActive(true)

    local needClick, needClickHint = false, false
    local button, toggle, dropdown, keyHint = self:_GetClickTarget(trans)
    uiInfo.button = button


    if info.hintType == HintType.Click and info.useClickHint then
        needClickHint = button ~= nil or toggle ~= nil or dropdown ~= nil or keyHint ~= nil
    end
    cell.clickHint.gameObject:SetActive(needClickHint)


    if self.m_isCurForceStep then
        if info.hintType == HintType.Click then
            if button or toggle or dropdown or keyHint then
                needClick = true
                self.m_waitClickUIHighlight = true
                cell.button.targetButton = button
                cell.button.targetToggle = toggle
                cell.button.targetDropdown = dropdown
                cell.button.targetKeyHintActionId = keyHint and keyHint:GetActionId() or nil

                cell.button.onClick = function()
                    self:_OnClickFakeButton()
                end
                cell.button:CopyTargetBinding()
            else
                logger.error(ELogChannel.Guide, "引导", GameInstance.player.guide.curGuideGroupId, "该路径下没有可点击对象", info.path)
            end
        elseif info.hintType == HintType.DragFrom then
            self.view.dragHint.rectTransform.anchoredPosition = cell.rectTransform.anchoredPosition




            if button then
                cell.button.targetButton = button
                cell.button:ForceToggleTargetBinding(true, true)
            end
        elseif info.hintType == HintType.DragTo then
            self.view.dragHint.toPosition = cell.rectTransform.position
            local vector = cell.rectTransform.anchoredPosition - self.view.dragHint.rectTransform.anchoredPosition
            local line = self.view.dragHint.line
            line.sizeDelta = Vector2(line.sizeDelta.x, vector.magnitude)
            line.localEulerAngles = Vector3(0, 0, Vector2.SignedAngle(Vector2.down, vector))

            if button or toggle or dropdown then
                cell.button.targetButton = button
                cell.button.targetToggle = toggle
                cell.button.targetDropdown = dropdown
                cell.button:ForceToggleTargetBinding(true, true)
            end
        end
    end
    cell.button.gameObject:SetActive(needClick)

    if DeviceInfo.usingController and info.autoNaviToThisTarget then
        local selectable = button or toggle or dropdown
        if selectable then
            self:SetNaviTarget(selectable)
        else
            logger.error(ELogChannel.Guide, "引导", GameInstance.player.guide.curGuideGroupId, "找不到 Selectable，无法自动手柄Navi", path)
        end
    end
end

GuideCtrl._GetHighlightTarget = HL.Method(HL.String).Return(HL.Userdata, HL.Boolean) << function(self, path)
    local trans = UIManager.uiRoot.transform:Find(path)
    local is3DUI = false
    if not trans then
        trans = UIManager.worldUIRoot.transform:Find(path)
        if not trans then
            trans = GameObject.Find(path)
            is3DUI = true
        end
    end
    return trans, is3DUI
end

GuideCtrl._GetClickTarget = HL.Method(Transform).Return(HL.Opt(HL.Any, HL.Any, HL.Any, HL.Any)) << function(self, trans)
    local targets = {}
    local targetTypes = {
        typeof(CS.Beyond.UI.UIButton),
        typeof(CS.Beyond.UI.UIToggle),
        typeof(CS.Beyond.UI.UIDropdown),
        typeof(CS.Beyond.UI.UIActionKeyHint),
    }

    for k, t in ipairs(targetTypes) do
        local target = trans:GetComponent(t)
        if target then
            targets[k] = target
            return targets[1], targets[2], targets[3], targets[4]
        end
    end
    for k, t in ipairs(targetTypes) do
        local target = trans:GetComponentInChildren(t)
        if target then
            targets[k] = target
            return targets[1], targets[2], targets[3], targets[4]
        end
    end
    for k, t in ipairs(targetTypes) do
        local target = trans:GetComponentInParent(t)
        if target then
            targets[k] = target
            return targets[1], targets[2], targets[3], targets[4]
        end
    end
end

GuideCtrl._SyncUIHighlightTargetRect = HL.Method(HL.Table, Unity.Rect, HL.Boolean, HL.Boolean) << function(self, cell, targetRect, needMask, isCircle)
    if DeviceInfo.usingController then
        local extraSize = self.view.config.CONTROLLER_HIGHLIGHT_EXTRA_SIZE
        targetRect = UIUtils.addRectSizeKeepCenter(targetRect, extraSize.x, extraSize.y)
    end

    local rectTrans = cell.rectTransform
    rectTrans.anchoredPosition = Vector2(targetRect.center.x, -targetRect.center.y)
    if isCircle then
        local size = math.max(targetRect.size.x, targetRect.size.y)
        rectTrans.sizeDelta = Vector2(size, size)
    else
        rectTrans.sizeDelta = targetRect.size
    end

    if cell.clickHint.gameObject.activeSelf then
        local clickHintRect = UIUtils.getTransformScreenRect(rectTrans, self.uiCamera)
        local ratio = (Screen.width - clickHintRect.center.x) / Screen.width
        local handOutOfScreen = ratio <= USE_LEFT_HAND_WIDTH_RATIO
        cell.clickHintRightHand.gameObject:SetActive(not handOutOfScreen)
        cell.clickHintLeftHand.gameObject:SetActive(handOutOfScreen)
    end

    cell.maskNode.gameObject:SetActiveIfNecessary(needMask)
    if not needMask then
        return
    end


    local width = UIManager.uiCanvasRect.rect.size.x
    local height = UIManager.uiCanvasRect.rect.size.y
    local xOffset = width / 2 - targetRect.center.x
    cell.up.anchoredPosition = Vector2(xOffset, 0)
    cell.down.anchoredPosition = Vector2(xOffset, 0)
    cell.up.sizeDelta = Vector2(width, targetRect.y)
    cell.down.sizeDelta = Vector2(width, height - targetRect.yMax)
    cell.left.sizeDelta = Vector2(targetRect.x, targetRect.height)
    cell.right.sizeDelta = Vector2(width - targetRect.xMax, targetRect.height)

    local otherCell = self.m_firstHighlightUICell
    if otherCell and otherCell ~= cell then

        local selfRect = {
            xMin = rectTrans.anchoredPosition.x - rectTrans.sizeDelta.x / 2,
            xMax = rectTrans.anchoredPosition.x + rectTrans.sizeDelta.x / 2,
            yMin = rectTrans.anchoredPosition.y - rectTrans.sizeDelta.y / 2,
            yMax = rectTrans.anchoredPosition.y + rectTrans.sizeDelta.y / 2,
        }
        local otherRect = {
            xMin = otherCell.rectTransform.anchoredPosition.x - otherCell.rectTransform.sizeDelta.x / 2,
            xMax = otherCell.rectTransform.anchoredPosition.x + otherCell.rectTransform.sizeDelta.x / 2,
            yMin = otherCell.rectTransform.anchoredPosition.y - otherCell.rectTransform.sizeDelta.y / 2,
            yMax = otherCell.rectTransform.anchoredPosition.y + otherCell.rectTransform.sizeDelta.y / 2,
        }


        if otherRect.yMin >= selfRect.yMax then

            self:_AdjustVerticalTargetMasks(otherCell, cell, otherRect.yMin - selfRect.yMax)
        elseif selfRect.yMin > otherRect.yMax then
            self:_AdjustVerticalTargetMasks(cell, otherCell, selfRect.yMin - otherRect.yMax)
        elseif otherRect.xMin >= selfRect.xMax then

            self:_AdjustHorizontalTargetMasks(otherCell, cell, otherRect.xMin - selfRect.xMax)
        elseif selfRect.xMin > otherRect.xMax then
            self:_AdjustHorizontalTargetMasks(cell, otherCell, selfRect.xMin - otherRect.xMax)
        else


        end
    end
end

GuideCtrl._AdjustVerticalTargetMasks = HL.Method(HL.Table, HL.Table, HL.Number) << function(self, upCell, downCell, height)
    upCell.down.sizeDelta = Vector2(upCell.down.sizeDelta.x, height)
    downCell.up.sizeDelta = Vector2.zero
end

GuideCtrl._AdjustHorizontalTargetMasks = HL.Method(HL.Table, HL.Table, HL.Number)
        << function(self, rightCell, leftCell, uiWidth)
    local width = UIManager.uiCanvasRect.rect.size.x

    local halfRightItemWidth = rightCell.rectTransform.sizeDelta.x / 2
    local rightHorMaskWidth = width - rightCell.rectTransform.anchoredPosition.x + uiWidth + halfRightItemWidth
    local rightHorOffsetX = rightHorMaskWidth / 2 - uiWidth - halfRightItemWidth
    rightCell.up.sizeDelta = Vector2(rightHorMaskWidth, rightCell.up.sizeDelta.y)
    rightCell.up.anchoredPosition = Vector2(rightHorOffsetX, 0)
    rightCell.down.sizeDelta = Vector2(rightHorMaskWidth, rightCell.down.sizeDelta.y)
    rightCell.down.anchoredPosition = Vector2(rightHorOffsetX, 0)
    rightCell.left.sizeDelta = Vector2(uiWidth, rightCell.left.sizeDelta.y)

    local leftItemWidth = leftCell.rectTransform.sizeDelta.x
    local leftHorMaskWidth = leftCell.rectTransform.anchoredPosition.x + leftItemWidth
    local leftHorOffsetX = leftHorMaskWidth / 2 - leftCell.rectTransform.anchoredPosition.x - leftItemWidth / 2
    leftCell.up.sizeDelta = Vector2(leftHorMaskWidth, leftCell.up.sizeDelta.y)
    leftCell.up.anchoredPosition = Vector2(leftHorOffsetX, 0)
    leftCell.down.sizeDelta = Vector2(leftHorMaskWidth, leftCell.down.sizeDelta.y)
    leftCell.down.anchoredPosition = Vector2(leftHorOffsetX, 0)
    leftCell.right.sizeDelta = Vector2.zero
end

GuideCtrl._SetHighlightTargetMask = HL.Method(HL.Table, HL.Boolean) << function(self, cell, enable)
    cell.upImage.raycastTarget = enable
    cell.downImage.raycastTarget = enable
    cell.leftImage.raycastTarget = enable
    cell.rightImage.raycastTarget = enable
end






GuideCtrl.m_controllerHintTextList = HL.Field(HL.Table)

GuideCtrl.m_controllerHintNodeList = HL.Field(HL.Table)

GuideCtrl._ConvertTextInfosStyle = HL.Method(HL.Userdata, HL.Userdata) << function(self, textInfos, uiHighlightInfos)

    if not textInfos or textInfos.Count == 0 then
        return
    end

    if not uiHighlightInfos or uiHighlightInfos.Count == 0 then
        return
    end

    local firstHighlight = uiHighlightInfos[0]
    if firstHighlight == nil then
        return
    end

    local highlightPath = firstHighlight.path
    if string.isEmpty(highlightPath) then
        return
    end

    for _, info in pairs(textInfos) do
        info.style = TextStyle.InfoAtFirstUIHighlight
    end
end

GuideCtrl._InitDialogControllerHint = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.m_controllerHintTextList = {
        self.view.bigLeftBottomDialogNode.controllerHintText,
        self.view.bigLeftTopDialogNode.controllerHintText,
        self.view.titleMiddleTopNode.controllerHintText,
        self.view.infoAtFirstUIHighlightDialogNode.controllerHintText,
    }

    self.m_controllerHintNodeList = {
        self.view.bigLeftBottomDialogNode.controllerHintNode,
        self.view.bigLeftTopDialogNode.controllerHintNode,
        self.view.titleMiddleTopNode.controllerHintNode,
        self.view.infoAtFirstUIHighlightDialogNode.controllerHintNode,
    }

    self:_RefreshControllerHintText()

    self:_RefreshDialogControllerHintNodeList(false)
end

GuideCtrl._RefreshControllerHintText = HL.Method() << function(self)
    if self.m_controllerHintTextList == nil then
        return
    end

    for _, hintText in ipairs(self.m_controllerHintTextList) do
        if hintText ~= nil then
            local result = InputManager.ParseTextActionId(Language["ui_common_next_step"])
            hintText:SetAndResolveTextStyle(result)
        end
    end
end

GuideCtrl._OnControllerTypeChanged = HL.Method(HL.Any) << function(self, args)
    self:_RefreshControllerHintText()
end

GuideCtrl._RefreshDialogControllerHintNodeList = HL.Method(HL.Boolean) << function(self, active)
    if not DeviceInfo.usingController then
        return
    end

    if self.m_controllerHintNodeList == nil then
        return
    end

    for _, hintNode in ipairs(self.m_controllerHintNodeList) do
        if hintNode ~= nil then
            hintNode.gameObject:SetActiveIfNecessary(active)
        end
    end
end

GuideCtrl._RefreshDialogControllerHint = HL.Method(HL.Userdata, HL.Boolean) << function(self, stepInfo, hasFinishCondition)
    self:_RefreshDialogControllerHintNodeList(false)

    if not DeviceInfo.usingController then
        return
    end

    if stepInfo == nil or stepInfo.type == GuideStepType.Weak or not string.isEmpty(stepInfo.enableActionId) or hasFinishCondition then
        return
    end

    local highlightInfos = stepInfo.uiHighlightInfos
    if highlightInfos ~= nil and highlightInfos.Count > 0 then
        local firstHighlightHintInfo = highlightInfos[0]
        if firstHighlightHintInfo == nil or firstHighlightHintInfo.hintType ~= HintType.None then
            return
        end
    end

    self:_RefreshDialogControllerHintNodeList(true)
end






if BEYOND_DEBUG then
    GuideCtrl._ShowCompleteGuidePopup = HL.Method() << function(self)
        local offset = 5
        local guideSortingOrder = self:GetSortingOrder()
        local lastSortingOrder
        Notify(MessageConst.SHOW_POP_UP, {
            content = "引导状态异常，是否直接跳过该引导",
            freezeWorld = true,
            hideBlur = true,
            onConfirm = function()
                GameInstance.player.guide:CompleteCurGuideGroupForDebug()
                local _, popupCtrl = UIManager:IsOpen(PanelId.CommonPopUp)
                popupCtrl:SetSortingOrder(lastSortingOrder, false)
            end,
            onCancel = function()
                local _, popupCtrl = UIManager:IsOpen(PanelId.CommonPopUp)
                popupCtrl:SetSortingOrder(lastSortingOrder, false)
            end,
        })
        local _, popupCtrl = UIManager:IsOpen(PanelId.CommonPopUp)
        lastSortingOrder = popupCtrl:GetSortingOrder()
        popupCtrl:SetSortingOrder(guideSortingOrder + offset, false)
    end
end



HL.Commit(GuideCtrl)
