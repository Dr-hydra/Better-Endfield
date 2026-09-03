local dialogCtrlBase = require_ex('UI/Panels/Dialog/DialogCtrlBase')
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DialogTimeline

DialogTimelineCtrl = HL.Class('DialogTimelineCtrl', dialogCtrlBase.DialogCtrlBase)


local DIALOG_TEXT_HIDE_DELAY_TIME<const> = 0.2
local DIALOG_BOTTOM_IN_ANIMATION<const> = "dialog_bottom_in"
local DIALOG_BOTTOM_OUT_ANIMATION<const> = "dialog_bottom_out"






DialogTimelineCtrl.s_overrideMessages = HL.StaticField(HL.Table) << {
    [MessageConst.UI_DIALOG_TEXT_STOPPED] = 'OnDialogTextStopped',
    [MessageConst.ON_DIALOG_TIMELINE_START_TRUNK] = 'OnDialogTimelineStartTrunk',
    [MessageConst.ON_DIALOG_TIMELINE_STOP_TRUNK] = 'OnDialogTimelineStopTrunk',
    [MessageConst.ON_DIALOG_TIMELINE_START_LEFT_SUBTITLE] = 'OnDialogTimelineStartLeftSubTitle',
    [MessageConst.ON_LOAD_NEW_DLG_TIMELINE] = 'OnLoadNewDialogTimeline',
    [MessageConst.REFRESH_DIALOG_TIMELINE_CAN_CLICK] = 'RefreshCanClick',
    [MessageConst.SWITCH_DIALOG_CAN_SKIP] = 'OnSwitchDialogCanSkip',
    [MessageConst.DIALOG_SHOW_DEV_WATER_MARK] = 'ShowDevWaterMark',
    [MessageConst.DIALOG_TIMELINE_REFRESH_AUTO_MODE] = 'OnRefreshAutoMode',
    [MessageConst.DIALOG_TIMELINE_SHOW_VIDEO_BORDER] = 'OnShowVideoBorder',
    [MessageConst.DIALOG_TIMELINE_HIDE_VIDEO_BORDER] = 'OnHideVideoBorder',
}

DialogTimelineCtrl.m_timelineHandle = HL.Field(HL.Userdata)

DialogTimelineCtrl.m_dialogTextStopped = HL.Field(HL.Boolean) << true

DialogTimelineCtrl.m_hasShowedDialogText = HL.Field(HL.Boolean) << false

DialogTimelineCtrl.m_isDialogTextShowing = HL.Field(HL.Boolean) << false

DialogTimelineCtrl.m_canSkip = HL.Field(HL.Boolean) << true

DialogTimelineCtrl.m_showingTrunkId = HL.Field(HL.String) << ""

DialogTimelineCtrl.m_dialogTextHideTimer = HL.Field(HL.Number) << -1

DialogTimelineCtrl.m_fmvNodeMap = HL.Field(HL.Table)

DialogTimelineCtrl.m_timelineHasBound = HL.Field(HL.Boolean) << false

DialogTimelineCtrl.m_videoBorderAspectRatio = HL.Field(HL.Number) << 0

DialogTimelineCtrl.m_videoBorderNoSafeZone = HL.Field(HL.Boolean) << false

DialogTimelineCtrl.m_videoBorderCanvasChangedClosure = HL.Field(HL.Function)

DialogTimelineCtrl.m_videoBorderHideTween = HL.Field(HL.Userdata)

DialogTimelineCtrl.OnPreloadDialogTimelinePanel = HL.StaticMethod(HL.Opt(HL.Table)) << function(arg)
    local preloadFinishCallback = arg and unpack(arg)

    UIManager:PreloadPanelAsset(PANEL_ID, PhaseId.Dialog, function()
        if preloadFinishCallback ~= nil then
            preloadFinishCallback()
        end
    end)
end

DialogTimelineCtrl.OnCreated = HL.Override(HL.Any) << function(self, arg)
    self.m_fmvNodeMap = {}
    self.m_dialogTextStopped = true
    self.m_timelineHandle = unpack(arg)
    self.m_timelineHasBound = false
    GameWorld.dialogTimelineManager:BindUIDialogTimelineText(self.m_timelineHandle, self.view.dialogTimelineText)
    GameWorld.dialogTimelineManager:BindSubtitle(self.m_timelineHandle, self.view.subtitlePanel)
    self.view.videoBorder.gameObject:SetActive(false)
end

DialogTimelineCtrl.OnShow = HL.Override() << function(self)
    self:OnDialogShow()
    self.view.debugNode.gameObject:SetActive(false)
    if NarrativeUtils.ShouldShowNarrativeDebugNode() then
        self.view.debugNode.gameObject:SetActive(true)
        GameWorld.dialogTimelineManager:BindFrameDebugTextMarker(self.view.frameDebugText)
        local desc = ""
        if GameWorld.dialogManager.dialogTree then
            desc = "\nT2/T3"
            if GameWorld.dialogManager.dialogTree.dialogTreeData.qualityLevel == CS.Beyond.Gameplay.DialogEnums.DialogQualityLevel.High then
                desc = "\nTO/T1"
            end
        end
        self.view.textDialogId.text = self:GetCurDialogId() .. desc
    end
end

DialogTimelineCtrl.GetCurDialogId = HL.Override().Return(HL.String) << function(self)
    return GameWorld.dialogTimelineManager.dialogId or ""
end

DialogTimelineCtrl.OnDialogTimelineStartLeftSubTitle = HL.Method() << function(self)
end

DialogTimelineCtrl.OnDialogTimelineStartTrunk = HL.Method(HL.Table) << function(self, arg)
    if not self.m_hasShowedDialogText then
        self.m_hasShowedDialogText = true
        self.view.dialogTimelineText:UpdateAlpha(1)

        self.view.buttonLog.gameObject:SetActive(true)
        self.view.buttonAuto.gameObject:SetActive(true)

        self:_RefreshAutoMode(GameWorld.dialogTimelineManager.autoMode)


        self.view.bottomMask.gameObject:SetActive(true)
    end
    self.m_dialogTextStopped = false

    local trunkId, actorName, entryLinks = unpack(arg)
    self:SetCurEntryLinks(entryLinks)
    self.m_showingTrunkId = trunkId
    self.view.actorNameLabel.gameObject:SetActive(not string.isEmpty(actorName))

    self:_TryShowDialogTextWithAnimation()
    self:_TrySetWaitNode(false)
    self:_RefreshCanSkip()
end

DialogTimelineCtrl._TryShowDialogTextWithAnimation = HL.Method() << function(self)
    if self.m_dialogTextHideTimer > 0 then
        self.m_dialogTextHideTimer = self:_ClearTimer(self.m_dialogTextHideTimer)
    end

    if not self.m_isDialogTextShowing then
        self.m_isDialogTextShowing = true
        self:PlayAnimation(DIALOG_BOTTOM_IN_ANIMATION)

        self.view.dialogTimelineText.gameObject:SetActive(true)
        self.view.bottomMask.gameObject:SetActive(true)
        self:SetGlossaryPopUpEnable(true)
    end
end

DialogTimelineCtrl.OnDialogTimelineStopTrunk = HL.Method(HL.Table) << function(self, arg)
    local trunkId = unpack(arg)
    if self.m_showingTrunkId ~= trunkId then
        return
    end

    self.m_showingTrunkId = ""
    self:_TryHideDialogTextWithAnimation()
end

DialogTimelineCtrl.OnSwitchDialogCanSkip = HL.Method(HL.Table) << function(self, arg)
    self:_RefreshCanSkip()
end

DialogTimelineCtrl._RefreshCanSkip = HL.Override() << function(self)
    self.m_canSkip = GameWorld.dialogManager.canSkip
    self.view.buttonSkip.gameObject:SetActive(self.m_canSkip)
end


DialogTimelineCtrl._TryHideDialogTextWithAnimation = HL.Method() << function(self)

    if self.m_dialogTextHideTimer < 0 then
        self.m_dialogTextHideTimer = self:_StartTimer(DIALOG_TEXT_HIDE_DELAY_TIME, function()
            if not self:IsPlayingAnimationOut() then
                self.m_isDialogTextShowing = false
                self:PlayAnimation(DIALOG_BOTTOM_OUT_ANIMATION, function()
                    if self.m_isClosed then
                        return
                    end
                    self.view.dialogTimelineText.gameObject:SetActive(false)
                    self.view.bottomMask.gameObject:SetActive(false)
                    self:SetGlossaryPopUpEnable(false)
                end)
            end
        end)
    end
end

DialogTimelineCtrl.OnLoadNewDialogTimeline = HL.Method(HL.Any) << function(self, arg)
    self.m_timelineHandle = unpack(arg)
end

DialogTimelineCtrl.OnDialogShow = HL.Override() << function(self)
    DialogTimelineCtrl.Super.OnDialogShow(self)
    self:_RefreshCanSkip()

    if self.m_timelineHasBound then
        return
    end


    local dialogTimelineManager = GameWorld.dialogTimelineManager
    local hasMask = dialogTimelineManager:BindDialogMask(self.view.mask)
    self.view.mask.gameObject:SetActive(hasMask)
    dialogTimelineManager:BindLeftSubtitle(self.m_timelineHandle, self.view.leftSubtitlePanel)
    dialogTimelineManager:BindPostProcessEffect(self.m_timelineHandle, self.view.postProcessEffect)

    for fmvId, fmvPath in pairs(self.m_timelineHandle.loadedPanelFmv) do
        local node = self:GetLoadedFMVNode(fmvId, fmvPath)
        node:StartAutoKeepAspectRatio()
        node:SetUserTimeCorrectionThreshold(0)
        dialogTimelineManager:BindPanelFMVNode(self.m_timelineHandle, fmvId, node.view.movieController)
    end

    if self.m_timelineHandle.loadedPanelFmv.Count > 0 then
        self.view.fmvGroup.gameObject:SetActive(true)
    else
        self.view.fmvGroup.gameObject:SetActive(false)
    end


    self.view.dialogTimelineText:UpdateAlpha(0)


    if GameWorld.dialogManager.records.Count == 0 then
        self.view.buttonLog.gameObject:SetActive(false)
        self.view.buttonAuto.gameObject:SetActive(false)

        self.view.textAuto.gameObject:SetActive(false)
    end


    self.view.bottomMask.gameObject:SetActive(false)
    self.m_timelineHasBound = true
end



DialogTimelineCtrl.OnBtnNextClick = HL.Override() << function(self)
    if GameWorld.dialogTimelineManager.canClick then
        if self:CheckTextPlaying() then
            self.view.textTalk:SeekToEnd()
            self:_TrySetWaitNode(true)
        else
            GameWorld.dialogTimelineManager:Next()
        end
    end
end

DialogTimelineCtrl.OnBtnAutoClick = HL.Override() << function(self)
    local auto = not GameWorld.dialogTimelineManager.autoMode
    GameWorld.dialogTimelineManager:SetAutoMode(auto)
end

DialogTimelineCtrl._RefreshAutoMode = HL.Override(HL.Boolean) << function(self, autoMode)
    if not self.m_hasShowedDialogText then
        return
    end

    DialogTimelineCtrl.Super._RefreshAutoMode(self, autoMode)
end


DialogTimelineCtrl.OnBtnLogClick = HL.Override() << function(self)
    self:Notify(MessageConst.OPEN_DIALOG_TIMELINE_RECORD)
end

DialogTimelineCtrl._GetCurrentAutoMode = HL.Override().Return(HL.Boolean) << function(self)
    return GameWorld.dialogTimelineManager.autoMode
end

DialogTimelineCtrl.OnOptionClick = HL.Override(HL.Number, HL.Any) << function(self, index, data)
    GameWorld.dialogTimelineManager:SelectIndex(CSIndex(index))
end

DialogTimelineCtrl.OnBtnSkipClick = HL.Override() << function(self)
    self:Notify(MessageConst.OPEN_DIALOG_TIMELINE_SKIP_POP_UP)
end


DialogTimelineCtrl.OnDialogTextStopped = HL.Override() << function(self)
    self.m_dialogTextStopped = true
    self:_TrySetWaitNode(GameWorld.dialogTimelineManager.canClick)
end

DialogTimelineCtrl.RefreshCanClick = HL.Method(HL.Table) << function(self, _)
    self:_TrySetWaitNode(self.m_dialogTextStopped)
end

DialogTimelineCtrl._TrySetWaitNode = HL.Override(HL.Boolean) << function(self, active)
    self.view.waitNode.gameObject:SetActive(active)
    self.view.centerWaitNode.gameObject:SetActive(active)
end

DialogTimelineCtrl.OnClose = HL.Override() << function(self)
    if self.m_dialogTextHideTimer > 0 then
        self.m_dialogTextHideTimer = self:_ClearTimer(self.m_dialogTextHideTimer)
    end
    self:_KillVideoBorderHideTween()
    self:_StopVideoBorderCanvasListener()
    self:ClearFMV()

    DialogTimelineCtrl.Super.OnClose(self)
end



DialogTimelineCtrl.OnShowVideoBorder = HL.Method(HL.Table) << function(self, arg)
    local fmvAspectRatio, noSafeZone = unpack(arg)
    if noSafeZone == nil then
        noSafeZone = false
    end

    self:_KillVideoBorderHideTween()

    self.m_videoBorderAspectRatio = fmvAspectRatio
    self.m_videoBorderNoSafeZone = noSafeZone

    self.view.videoBorder.upper.transform.localScale = Vector3.one
    self.view.videoBorder.down.transform.localScale = Vector3.one
    self.view.videoBorder.left.transform.localScale = Vector3.one
    self.view.videoBorder.right.transform.localScale = Vector3.one

    self:_RefreshVideoBorder()
    UIUtils.PlayAnimationAndToggleActive(self.view.videoBorder.animationWrapper, true)

    if not self.m_videoBorderCanvasChangedClosure then
        self.m_videoBorderCanvasChangedClosure = function() self:_RefreshVideoBorder() end
        UIManager.m_uiCanvasScaleHelper.onCanvasChanged:AddListener(self.m_videoBorderCanvasChangedClosure)
    end
end

DialogTimelineCtrl.OnHideVideoBorder = HL.Method(HL.Opt(HL.Table)) << function(self, arg)
    local tweenDuration = arg and unpack(arg) or 0
    self:_StopVideoBorderCanvasListener()
    self:_KillVideoBorderHideTween()

    if tweenDuration <= 0 then
        self.view.videoBorder.gameObject:SetActive(false)
        self.m_videoBorderAspectRatio = 0
        self.m_videoBorderNoSafeZone = false
        return
    end

    local currentScale = 1
    self.m_videoBorderHideTween = DOTween.To(
        function() return currentScale end,
        function(value)
            currentScale = value
            self.view.videoBorder.upper.transform.localScale = Vector3(1, value, 1)
            self.view.videoBorder.down.transform.localScale = Vector3(1, value, 1)
            self.view.videoBorder.left.transform.localScale = Vector3(value, 1, 1)
            self.view.videoBorder.right.transform.localScale = Vector3(value, 1, 1)
        end,
        0,
        tweenDuration
    )
    self.m_videoBorderHideTween:SetEase(CS.DG.Tweening.Ease.Linear)
    self.m_videoBorderHideTween:OnComplete(function()
        self.view.videoBorder.gameObject:SetActive(false)
        self.view.videoBorder.upper.transform.localScale = Vector3.one
        self.view.videoBorder.down.transform.localScale = Vector3.one
        self.view.videoBorder.left.transform.localScale = Vector3.one
        self.view.videoBorder.right.transform.localScale = Vector3.one
        self.m_videoBorderAspectRatio = 0
        self.m_videoBorderNoSafeZone = false
        self.m_videoBorderHideTween = nil
    end)
end

DialogTimelineCtrl._RefreshVideoBorder = HL.Method() << function(self)
    local parentWidth = self.view.videoBorder.transform.rect.width
    local parentHeight = self.view.videoBorder.transform.rect.height

    local offsetMin, offsetMax = FMVUtils.GetSuitableFMVImageOffset(
        parentWidth, parentHeight,
        self.m_videoBorderAspectRatio, 1,
        self.m_videoBorderNoSafeZone)

    local barW = math.max(0, offsetMin.x)
    local barH = math.max(0, offsetMin.y)

    self.view.videoBorder.upper.transform.sizeDelta = Vector2(0, barH)
    self.view.videoBorder.down.transform.sizeDelta = Vector2(0, barH)
    self.view.videoBorder.left.transform.sizeDelta = Vector2(barW, 0)
    self.view.videoBorder.right.transform.sizeDelta = Vector2(barW, 0)
end

DialogTimelineCtrl._KillVideoBorderHideTween = HL.Method() << function(self)
    if self.m_videoBorderHideTween then
        self.m_videoBorderHideTween:Kill()
        self.m_videoBorderHideTween = nil
    end
end

DialogTimelineCtrl._StopVideoBorderCanvasListener = HL.Method() << function(self)
    if self.m_videoBorderCanvasChangedClosure then
        UIManager.m_uiCanvasScaleHelper.onCanvasChanged:RemoveListener(self.m_videoBorderCanvasChangedClosure)
        self.m_videoBorderCanvasChangedClosure = nil
    end
end





DialogTimelineCtrl.StopFMV = HL.Method(HL.String) << function(self, fmvId)
    local fmvNode = self.m_fmvNodeMap[fmvId]
    if not fmvNode then
        return
    end

    fmvNode:StopVideo(true)

    fmvNode.gameObject:SetActive(false)
    self.m_fmvNodeMap[fmvId] = nil
    if next(self.m_fmvNodeMap) == nil then
        self.view.fmvGroup.gameObject:SetActive(false)
    end
end

DialogTimelineCtrl.OnStopFMV = HL.Method(HL.Table) << function(self, arg)
    local fmvId = unpack(arg)
    self:StopFMV(fmvId)
end

DialogTimelineCtrl.ClearFMV = HL.Method() << function(self)
    lume.each(lume.keys(self.m_fmvNodeMap), function(fmvId) self:StopFMV(fmvId) end)
end

DialogTimelineCtrl.GetLoadedFMVNode = HL.Method(HL.String, HL.String).Return(HL.Any) << function(self, fmvId, fmvPath)
    if self.m_fmvNodeMap[fmvId] then
        return self.m_fmvNodeMap[fmvId]
    end

    local isOpen, preloader = UIManager:IsOpen(PanelId.VideoPreloader)

    local node = nil
    if isOpen then
        node = preloader:MovePreloadedVideoNode(fmvId, self.view.fmvGroup)
    end

    if node == nil then
        local rawNode = UIUtils.addChild(self.view.fmvGroup, self.view.fmvTemplate)
        node = Utils.wrapLuaNode(rawNode)
        node.gameObject:SetActive(true)
        node:PreloadVideo(fmvPath)
        logger.error("FMV node not preloaded!!! gen new node for fmvId: ", fmvId)
    end

    self.m_fmvNodeMap[fmvId] = node
    return node
end


HL.Commit(DialogTimelineCtrl)
