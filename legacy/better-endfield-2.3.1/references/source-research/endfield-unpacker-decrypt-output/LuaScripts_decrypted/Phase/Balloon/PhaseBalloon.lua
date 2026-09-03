
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.Balloon
local FREEZE_WORLD_SOURCE = "PhaseBalloon"

PhaseBalloon = HL.Class('PhaseBalloon', phaseBase.PhaseBase)

PhaseBalloon.curLevel = HL.Field(HL.Userdata)

PhaseBalloon.curGame = HL.Field(HL.Userdata)

PhaseBalloon.m_nowState = HL.Field(HL.String) << MiniGameBalloonUtils.GAME_STATE.FIRST_IN

PhaseBalloon.m_rotatePlatePanel = HL.Field(HL.Userdata)

PhaseBalloon.m_balloonMainPanel = HL.Field(HL.Userdata)

PhaseBalloon.m_miniGameSystem = HL.Field(HL.Userdata)

PhaseBalloon.dragEnterDropItem = HL.Field(HL.Userdata)

PhaseBalloon.nowDragChessboardItemData = HL.Field(HL.Table)

PhaseBalloon.nowDragTabItemData = HL.Field(HL.Table)

PhaseBalloon.nowNaviChessboardItemData = HL.Field(HL.Table)

PhaseBalloon.nowDragHighLightItemData = HL.Field(HL.Table)

PhaseBalloon.dragEnterMask = HL.Field(HL.Boolean) << false

PhaseBalloon.isDragIngCell = HL.Field(HL.Boolean) << false

PhaseBalloon.isDraggingStart = HL.Field(HL.Boolean) << false

PhaseBalloon.dropHandled = HL.Field(HL.Boolean) << false

PhaseBalloon.m_isWorldFreeze = HL.Field(HL.Boolean) << false

PhaseBalloon.m_hasNotifiedPhaseClosed = HL.Field(HL.Boolean) << false

PhaseBalloon.m_balloonGameStartTs = HL.Field(HL.Number) << 0

PhaseBalloon.m_balloonGameEndTs = HL.Field(HL.Number) << 0


PhaseBalloon.m_curBalloonGameComplete = HL.Field(HL.Boolean) << false

PhaseBalloon.m_balloonGameSucc = HL.Field(HL.Boolean) << false

PhaseBalloon.m_hasHint = HL.Field(HL.Boolean) << false

PhaseBalloon.m_isHintUse = HL.Field(HL.Boolean) << false

PhaseBalloon.m_useHintTs = HL.Field(HL.Number) << 0





PhaseBalloon.s_messages = HL.StaticField(HL.Table) << {

}


PhaseBalloon._OnInit = HL.Override() << function(self)
    PhaseBalloon.Super._OnInit(self)
    self.m_miniGameSystem = GameInstance.player.miniGame
    self.curGame = self.m_miniGameSystem.balloonGame
    self.curLevel = self.m_miniGameSystem.balloonGame.curBalloonMiniGame
    self:SetFreezeWorld(true)
end




PhaseBalloon.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)

end

PhaseBalloon._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self:SetFreezeWorld(true)
    self.m_rotatePlatePanel = self.m_panel2Item[PanelId.BalloonMiniGameRotatePlate].uiCtrl
    self.m_balloonMainPanel = self.m_panel2Item[PanelId.BalloonMiniGame].uiCtrl
    self:SetNaviGroupNaviPartner()

    local isRecover = self.arg and self.arg.isFromChangeInputDevice
    if isRecover then
        local savedState = self.arg.nowState or MiniGameBalloonUtils.GAME_STATE.PLAY

        if self.arg.needShowAllAnswer then
            self.m_rotatePlatePanel.m_needShowAllAnswer = true
            self.m_rotatePlatePanel.m_showAllAnswerMiniGameId = self.arg.showAllAnswerMiniGameId or ""
        end

        self.curLevel = self.m_miniGameSystem.balloonGame.curBalloonMiniGame

        local GAME_STATE = MiniGameBalloonUtils.GAME_STATE
        if savedState == GAME_STATE.COMPLETE or savedState == GAME_STATE.END then
            self.m_rotatePlatePanel:SwitchState(GAME_STATE.PLAY)
            self.m_balloonMainPanel:SwitchState(GAME_STATE.PLAY)
            self.m_curBalloonGameComplete = true
            if savedState == GAME_STATE.END then
                self.m_nowState = GAME_STATE.PLAY
                self:SwitchState(GAME_STATE.END)
            else
                self.m_nowState = GAME_STATE.COMPLETE
                self.m_rotatePlatePanel:SwitchState(GAME_STATE.COMPLETE)
                self.m_balloonMainPanel:SwitchState(GAME_STATE.COMPLETE)
                self.m_balloonMainPanel.view.animationWrapper:PlayWithTween("balloonminigame_play_succse")
                self.m_rotatePlatePanel.view.animationWrapper:PlayWithTween("balloonminigame_plate_complete")
            end
        else
            self.m_nowState = savedState
            self.m_rotatePlatePanel:SwitchState(savedState)
            self.m_balloonMainPanel:SwitchState(savedState)
        end
        self.m_balloonMainPanel:RecoverNoticeTimerState(self.arg.noticeTimerState)
        if self.arg.hasHint ~= nil then
            self.m_hasHint = self.arg.hasHint
        end
        if self.arg.isHintUse ~= nil then
            self.m_isHintUse = self.arg.isHintUse
        end
        if self.arg.useHintTs then
            self.m_useHintTs = self.arg.useHintTs
        end
    else
        self.curGame:ResetLevel()
        local GAME_STATE = MiniGameBalloonUtils.GAME_STATE
        self:SwitchState(GAME_STATE.FIRST_IN)
    end
end

PhaseBalloon._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if InputManagerInst.inChangingInputDevice then
        return
    end
    if not self.m_curBalloonGameComplete then
        self.m_balloonGameEndTs = DateTimeUtils.GetCurrentTimestampBySeconds()
        self:_EventLogBalloonGameEnd()
    end
    self:SetFreezeWorld(false)
    self:_NotifyPhaseClosed()
    GameInstance.player.miniGame:FinishBalloonGame()
end

PhaseBalloon._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)

end

PhaseBalloon._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)

end






PhaseBalloon._OnActivated = HL.Override() << function(self)

end

PhaseBalloon._OnDeActivated = HL.Override() << function(self)

end

PhaseBalloon._OnDestroy = HL.Override() << function(self)
    if not InputManagerInst.inChangingInputDevice then
        self:SetFreezeWorld(false)
    end
    PhaseBalloon.Super._OnDestroy(self)
end



PhaseBalloon.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local nowState = self.m_nowState
    local GAME_STATE = MiniGameBalloonUtils.GAME_STATE
    if nowState == GAME_STATE.FIRST_IN then
        nowState = GAME_STATE.PLAY
    end
    return {
        isFromChangeInputDevice = true,
        nowState = nowState,
        needShowAllAnswer = self.m_rotatePlatePanel and self.m_rotatePlatePanel.m_needShowAllAnswer or false,
        showAllAnswerMiniGameId = self.m_rotatePlatePanel and self.m_rotatePlatePanel.m_showAllAnswerMiniGameId or "",
        noticeTimerState = self.m_balloonMainPanel and self.m_balloonMainPanel:GetNoticeTimerStateArg() or nil,
        hasHint = self.m_hasHint,
        isHintUse = self.m_isHintUse,
        useHintTs = self.m_useHintTs,
    }
end

PhaseBalloon.SetFreezeWorld = HL.Method(HL.Boolean) << function(self, isFreeze)
    if self.m_isWorldFreeze == isFreeze then
        return
    end
    self.m_isWorldFreeze = isFreeze
    if isFreeze then
        Notify(MessageConst.OPEN_FREEZE_WORLD_PANEL, FREEZE_WORLD_SOURCE)
    else
        Notify(MessageConst.CLOSE_FREEZE_WORLD_PANEL, FREEZE_WORLD_SOURCE)
    end
end

PhaseBalloon._NotifyPhaseClosed = HL.Method() << function(self)
    if self.m_hasNotifiedPhaseClosed then
        return
    end
    self.m_hasNotifiedPhaseClosed = true
    self.m_miniGameSystem:OnBalloonPhaseClosed()
end

PhaseBalloon.ExitBalloonPhase = HL.Method() << function(self)
    self:SetFreezeWorld(false)
    if PhaseManager:IsOpen(PhaseId.BalloonInteractive) then
        PhaseManager:ExitPhaseFast(PhaseId.BalloonInteractive)
    end
    PhaseManager:PopPhase(PhaseId.Balloon)
end

PhaseBalloon.SwitchState = HL.Method(HL.String) << function(self, state)

    if self.m_nowState == MiniGameBalloonUtils.GAME_STATE.END then
        return
    end
    self.m_nowState = state
    if state == MiniGameBalloonUtils.GAME_STATE.COMPLETE then
        self.curGame:CompleteCurrentLevel()
    elseif state == MiniGameBalloonUtils.GAME_STATE.PLAY then
        self.curLevel = self.m_miniGameSystem.balloonGame.curBalloonMiniGame
    end

    self.m_rotatePlatePanel:SwitchState(state)
    self.m_balloonMainPanel:SwitchState(state)

    if state == MiniGameBalloonUtils.GAME_STATE.PLAY then

        if self.m_balloonGameStartTs == 0 then
            self.m_balloonGameStartTs = DateTimeUtils.GetCurrentTimestampBySeconds()
            self.m_balloonGameEndTs = 0
            self.m_curBalloonGameComplete = false
            self.m_hasHint = false
            self.m_isHintUse = false
            self.m_useHintTs = 0
            self.m_rotatePlatePanel.view.animationWrapper:PlayWithTween("balloonminigame_plate_in")
            AudioAdapter.PostEvent("Au_UI_Menu_BalloonRecycle_Open")
        end
    elseif state == MiniGameBalloonUtils.GAME_STATE.COMPLETE then
        self.m_curBalloonGameComplete = true
        self.m_balloonGameEndTs = DateTimeUtils.GetCurrentTimestampBySeconds()
        if self.curGame:IsLevelComplete() then
            self.m_balloonGameSucc = true
        end
        self:_EventLogBalloonGameEnd()
        self.m_balloonGameEndTs = 0
        self.m_balloonGameStartTs = 0
        self.m_balloonMainPanel.view.animationWrapper:PlayWithTween("balloonminigame_play_succse")
        self.m_rotatePlatePanel.view.animationWrapper:PlayWithTween("balloonminigame_plate_complete")
    elseif state == MiniGameBalloonUtils.GAME_STATE.FIRST_IN then
        self.m_balloonMainPanel.view.animationWrapper:PlayInAnimation(function()
            self:SwitchState(MiniGameBalloonUtils.GAME_STATE.PLAY)
        end)
    elseif state == MiniGameBalloonUtils.GAME_STATE.SUCCESS then

    elseif state == MiniGameBalloonUtils.GAME_STATE.END then
        self.m_rotatePlatePanel.view.animationWrapper:PlayWithTween("balloonminigame_plate_end")
        AudioAdapter.PostEvent("Au_UI_Event_BalloonRecycle_Finish")
        self.m_rotatePlatePanel:SetSuccessState()
        self:SetFreezeWorld(false)
        self.m_rotatePlatePanel.view.endNode.gameObject:SetActive(true)
        self.m_rotatePlatePanel.view.endNode.animationWrapper:PlayWithTween("balloonminigame_plate_unlock", function()
            self:_StartCoroutine(function()
                coroutine.wait(self.m_rotatePlatePanel.view.config.END_FIX_ANIM_TIME)
                self:ExitBalloonPhase()
            end)
        end)
    end
end

PhaseBalloon.SetNaviGroupNaviPartner = HL.Method() << function(self)
    local balloonMiniGameScrollNaviGroup = self.m_balloonMainPanel.view.scrollListSelectableNaviGroup
    local balloonMiniGameNaviGroup = self.m_balloonMainPanel.view.selectableNaviGroup
    balloonMiniGameScrollNaviGroup:TryChangeNaviPartnerOnLeft(self.m_rotatePlatePanel.view.selectableNaviGroup, true)
    balloonMiniGameNaviGroup:TryChangeNaviPartnerOnLeft(self.m_rotatePlatePanel.view.selectableNaviGroup, true)
    self.m_rotatePlatePanel.view.selectableNaviGroup:TryChangeNaviPartnerOnRight(balloonMiniGameScrollNaviGroup, true)
    self.m_rotatePlatePanel.view.selectableNaviGroup:TryChangeNaviPartnerOnRight(balloonMiniGameNaviGroup, true)
end


PhaseBalloon.SetChessboardNaviLocked = HL.Method(HL.Boolean) << function(self, locked)



    local rotatePlate = self.m_rotatePlatePanel
    local mainPanel = self.m_balloonMainPanel
    if not rotatePlate or rotatePlate.m_isClosed or not mainPanel or mainPanel.m_isClosed then
        return
    end
    local rotatePlateNaviGroup = rotatePlate.view.selectableNaviGroup
    local scrollNaviGroup = mainPanel.view.scrollListSelectableNaviGroup
    local mainNaviGroup = mainPanel.view.selectableNaviGroup
    if IsNull(rotatePlateNaviGroup) or IsNull(scrollNaviGroup) or IsNull(mainNaviGroup) then
        return
    end
    local ok, err = pcall(function()
        rotatePlateNaviGroup:TryChangeNaviPartnerOnRight(scrollNaviGroup, not locked)
        rotatePlateNaviGroup:TryChangeNaviPartnerOnRight(mainNaviGroup, not locked)
    end)
    if not ok then
        logger.warning(ELogChannel.MiniGame, string.format("[PhaseBalloon] SetChessboardNaviLocked pcall caught: %s", tostring(err)))
    end
end


PhaseBalloon._ForEachDragItem = HL.Method(HL.Function) << function(self, fn)
    if self.m_balloonMainPanel and self.m_balloonMainPanel.m_balloonCells then
        for _, cell in pairs(self.m_balloonMainPanel.m_balloonCells) do
            if cell and NotNull(cell.view.balloonDragItem) then
                fn(cell.view.balloonDragItem)
            end
        end
    end
    if self.m_rotatePlatePanel and self.m_rotatePlatePanel.m_gridItems then
        for _, col in pairs(self.m_rotatePlatePanel.m_gridItems) do
            for _, item in pairs(col) do
                if item.cell and NotNull(item.cell.view.balloonImgDragItem) then
                    fn(item.cell.view.balloonImgDragItem)
                end
            end
        end
    end
end


PhaseBalloon.DisableComponentsInteractable = HL.Method(HL.Opt(CS.UnityEngine.EventSystems.PointerEventData)) << function(self, eventData)
    local activeDragItem
    if eventData and NotNull(eventData.pointerDrag) then
        activeDragItem = eventData.pointerDrag:GetComponent(typeof(CS.Beyond.UI.UIDragItem))
    end

    local mainPanel = self.m_balloonMainPanel
    if mainPanel and not mainPanel.m_isClosed then
        mainPanel.view.btnClose.enabled = false
        mainPanel.view.bottomNode.btnConfirm.enabled = false
        mainPanel.view.bottomNode.resetBtn.interactable = false
        mainPanel.view.scrollList.enabled = false
        mainPanel.view.balloonNoticeTips:ToggleNoticeBtnInteractable(false)
    end

    local rotatePlate = self.m_rotatePlatePanel
    if rotatePlate and not rotatePlate.m_isClosed then
        rotatePlate.view.completeNode.button.enabled = false
    end


    if rotatePlate and not rotatePlate.m_isClosed and rotatePlate.m_gridItems then
        for _, col in pairs(rotatePlate.m_gridItems) do
            for _, item in pairs(col) do
                if item.cell and NotNull(item.cell.gameObject) then
                    if item.cell.view.balloonImgDragItem ~= activeDragItem then
                        item.cell.view.balloonImg.raycastTarget = false
                    end
                end
            end
        end
    end


    if mainPanel and not mainPanel.m_isClosed and mainPanel.m_balloonCells then
        for _, cell in pairs(mainPanel.m_balloonCells) do
            if cell and NotNull(cell.view.balloonDragItem) and cell.view.balloonDragItem ~= activeDragItem then
                cell.view.balloonImg.raycastTarget = false
            end
        end
    end
end


PhaseBalloon.RecoverComponentsInteractable = HL.Method() << function(self)
    local mainPanel = self.m_balloonMainPanel
    if mainPanel and not mainPanel.m_isClosed then
        mainPanel.view.btnClose.enabled = true
        mainPanel.view.scrollList.enabled = true
        mainPanel.view.balloonNoticeTips:ToggleNoticeBtnInteractable(true)
        mainPanel:UpdateResetBtn()
        mainPanel:UpdateConfirmBtn()
    end

    local rotatePlate = self.m_rotatePlatePanel
    if rotatePlate and not rotatePlate.m_isClosed then
        rotatePlate.view.completeNode.button.enabled = true
    end


    if rotatePlate and not rotatePlate.m_isClosed and rotatePlate.m_gridItems then
        for _, col in pairs(rotatePlate.m_gridItems) do
            for _, item in pairs(col) do
                if item.cell and NotNull(item.cell.gameObject) then
                    item.cell.view.balloonImg.raycastTarget = true
                end
            end
        end
    end


    if mainPanel and not mainPanel.m_isClosed and mainPanel.m_balloonCells then
        for _, cell in pairs(mainPanel.m_balloonCells) do
            if cell and NotNull(cell.view.balloonDragItem) then
                cell.view.balloonImg.raycastTarget = true
            end
        end
    end
end


PhaseBalloon._ForceEndOrphanDrags = HL.Method(HL.Opt(CS.UnityEngine.EventSystems.PointerEventData)) << function(self, eventData)
    local activeDragItem
    if eventData and NotNull(eventData.pointerDrag) then
        activeDragItem = eventData.pointerDrag:GetComponent(typeof(CS.Beyond.UI.UIDragItem))
    end
    self:_ForEachDragItem(function(dragItem)
        if dragItem ~= activeDragItem and dragItem.inDragging then
            dragItem:OnEndDrag(nil)
        end
    end)
end

PhaseBalloon.OnBeginDragCell = HL.Method(HL.Opt(CS.UnityEngine.EventSystems.PointerEventData)) << function(self, eventData)
    local pendingTab = self.nowDragTabItemData
    local pendingChessboard = self.nowDragChessboardItemData
    self:_ForceEndOrphanDrags(eventData)
    self.nowDragTabItemData = pendingTab
    self.nowDragChessboardItemData = pendingChessboard

    self:DisableComponentsInteractable(eventData)
    self:OnBeginDrag()
end

PhaseBalloon.OnDraggingCell = HL.Method(HL.Opt(CS.UnityEngine.EventSystems.PointerEventData)) << function(self, eventData)

    self:OnDraggingStart()
    if (not eventData or not eventData.pointerEnter) and self.dragEnterDropItem then
        self:ToggleHighlightEnd()
        self.dragEnterMask = false
    end

    if eventData.pointerEnter then
        local luaReference = eventData.pointerEnter:GetComponent(typeof(CS.Beyond.Lua.LuaReference))

        if not luaReference then
            luaReference = eventData.pointerEnter.transform.parent:GetComponent(typeof(CS.Beyond.Lua.LuaReference))
        end
        if not luaReference then
            self.dragEnterMask = eventData.pointerEnter.name == "Mask"
            self:ToggleHighlightEnd()
            return
        end
        self.dragEnterMask = false
        local ref = {}
        luaReference:BindToLua(ref)
        local dropItem = ref.dropItem
        local dragItem = ref.balloonImgDragItem
        if not dropItem or not dragItem or self.dragEnterDropItem == dropItem then
            return
        end
        self:ToggleHighlightEnd()
        local cx, cy = dragItem.luaTable.cx, dragItem.luaTable.cy
        local curBlockState
        if cx and cy then
            curBlockState = self.curLevel:GetNowPositionState(cx, cy)
        end
        if curBlockState and curBlockState == MiniGameBalloonUtils.BalloonBlockState.CANT_PLACE then
            return
        end
        self:OnDragging(dropItem, cx, cy)
    end
end

PhaseBalloon.OnDraggingStart = HL.Method() << function(self)
    if self.isDraggingStart then
       return
    end
    self.isDraggingStart = true
    self.m_balloonMainPanel:RefreshAllTabCellView()
end


PhaseBalloon.OnDragging = HL.Method(HL.Userdata, HL.Number, HL.Number) << function(self, dropItem, cx, cy)
    self.dragEnterDropItem = dropItem
    if dropItem then
        self.nowDragHighLightItemData = {
            cx = cx,
            cy = cy
        }
        local nowState = self.curLevel:GetNowPositionState(cx, cy)
        if nowState ~= MiniGameBalloonUtils.BalloonBlockState.CANT_PLACE then
            dropItem:ToggleHighlight(true)
        end
    end
end

PhaseBalloon.OnBeginDrag = HL.Method() << function(self)
    self.isDragIngCell = true
    self.m_rotatePlatePanel:SetSliderState()
    self:SetChessboardNaviLocked(true)
end

PhaseBalloon.OnEndDrag = HL.Method(HL.Opt(HL.Boolean)) << function(self, doNotRefreshState)

    self.nowDragTabItemData = nil
    self.nowNaviChessboardItemData = nil
    self.nowDragChessboardItemData = nil
    self.isDragIngCell = false
    self.isDraggingStart = false
    self:SetChessboardNaviLocked(false)
    self:ToggleHighlightEnd()
    if self.m_balloonMainPanel and not self.m_balloonMainPanel.m_isClosed then
        self.m_balloonMainPanel:RefreshAllTabCellView()
    end
    if not doNotRefreshState and self.m_rotatePlatePanel and not self.m_rotatePlatePanel.m_isClosed then
        self.m_rotatePlatePanel:SetAllPlateState()
    end
end

PhaseBalloon.ToggleHighlightEnd = HL.Method() << function(self)
    if self.dragEnterDropItem then
        if NotNull(self.dragEnterDropItem) then
            self.dragEnterDropItem:ToggleHighlight(false, true)
        end
        self.dragEnterDropItem = nil
    end
end
PhaseBalloon.OnEndDragCell = HL.Method(HL.Opt(CS.UnityEngine.EventSystems.PointerEventData)) << function(self, eventData)
    self:RecoverComponentsInteractable()
    local dragData = self.nowDragChessboardItemData
    local handled = self.dropHandled
    self.dropHandled = false
    self:OnEndDrag(true)
    if not handled and dragData and self.m_rotatePlatePanel and not self.m_rotatePlatePanel.m_isClosed then
        self.m_rotatePlatePanel:SetBlockStateByPosition(dragData.cx, dragData.cy, MiniGameBalloonUtils.BalloonBlockState.PLACE_ABLE)
        self.m_rotatePlatePanel:SetAllPlateState()
    end
end

PhaseBalloon.OnClickNextBtn = HL.Method() << function(self)
    if self.curGame.currentIndex + 1 == self.curGame.levelNum then
        self:SwitchState(MiniGameBalloonUtils.GAME_STATE.END)
    else
        self.curGame:NextLevel()
        self.m_balloonMainPanel.m_isFirstInit = true
        self:SwitchState(MiniGameBalloonUtils.GAME_STATE.PLAY)
    end
end



PhaseBalloon._EventLogBalloonGameEnd = HL.Method() << function(self)
    local curIndex = LuaIndex(self.curGame.currentIndex)
    local balloonGameTime = math.max(self.m_balloonGameEndTs - self.m_balloonGameStartTs, 0)
    local hintUseTime = math.max(self.m_useHintTs - self.m_balloonGameStartTs, 0)
    EventLogManagerInst:GameEvent_PuzzleEnd(tostring(self.curGame.eventLogEntityLid),
                                            self.curGame.eventLogTemplateId,
                                            self.curGame.eventLogEntityPos,
                                            self.m_balloonGameSucc,
                                            curIndex,
                                            self.m_curBalloonGameComplete,
                                            self.curLevel.rawData.levelId,
                                            self.m_hasHint,
                                            self.m_isHintUse,
                                            hintUseTime,
                                            balloonGameTime)
end



HL.Commit(PhaseBalloon)

