
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BalloonMiniGame

BalloonMiniGameCtrl = HL.Class('BalloonMiniGameCtrl', uiCtrl.UICtrl)

BalloonMiniGameCtrl.m_noActionNoticeThreshold = HL.Field(HL.Number) << -1

BalloonMiniGameCtrl.m_noActionNoticeTimerId = HL.Field(HL.Number) << -1

BalloonMiniGameCtrl.m_stayNoticeThreshold = HL.Field(HL.Number) << -1

BalloonMiniGameCtrl.m_stayNoticeTimerId = HL.Field(HL.Number) << -1

BalloonMiniGameCtrl.m_chessboardLock = HL.Field(HL.Boolean) << false

BalloonMiniGameCtrl.m_balloonCells = HL.Field(HL.Table)

BalloonMiniGameCtrl.m_balloonTypes = HL.Field(HL.Table)

BalloonMiniGameCtrl.m_getCell = HL.Field(HL.Function)

BalloonMiniGameCtrl.m_isFirstInit = HL.Field(HL.Boolean) << true

BalloonMiniGameCtrl.m_tabEquipBalloonBindingId = HL.Field(HL.Number) << -1

BalloonMiniGameCtrl.m_recycleBalloonGridBindingId = HL.Field(HL.Number) << -1

BalloonMiniGameCtrl.m_confirmChessboardBindingId = HL.Field(HL.Number) << -1

BalloonMiniGameCtrl.m_selectBalloonBindingId = HL.Field(HL.Number) << -1

BalloonMiniGameCtrl.m_switchToChessboardBindingId = HL.Field(HL.Number) << -1

BalloonMiniGameCtrl.m_switchToListBindingId = HL.Field(HL.Number) << -1

BalloonMiniGameCtrl.m_isInList = HL.Field(HL.Boolean) << true

BalloonMiniGameCtrl.m_suppressTabCellCountAnim = HL.Field(HL.Boolean) << false

BalloonMiniGameCtrl.m_cancelSelectBindingId = HL.Field(HL.Number) << -1

BalloonMiniGameCtrl.m_nextLevelBindingId = HL.Field(HL.Number) << -1

BalloonMiniGameCtrl.balloonLiftNum = HL.Field(HL.Number) << -1




BalloonMiniGameCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_MINIGAME_BALLOON_BLOCK_CHANGE] = 'OnBalloonCellDrop',
}


BalloonMiniGameCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_BALLOON_CLOSE_CONFIRM_SUB_CONTENT,
            onConfirm = function()
                self.m_phase:ExitBalloonPhase()
            end,
        })
    end)

    self.view.balloonNoticeTips:InitPuzzleNoticeTips(function()
        self:_OnClickNoticeBtn()
    end, {
        content = Language.LUA_BALLOON_SHOW_ANSWER_CONFIRM_CONTENT,
    })

    self.view.bottomNode.btnConfirm.onClick:AddListener(function()
        self:_OnClickConfirmBtn()
    end)

    self.view.bottomNode.resetBtn.onClick:AddListener(function()
        self:_OnClickReset()
    end)

    self.m_getCell = UIUtils.genCachedCellFunction(self.view.scrollList)

    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getCell(obj), LuaIndex(csIndex))
    end)


    self.m_tabEquipBalloonBindingId = self:BindInputPlayerAction("mini_game_balloon_equip_tab_balloon", function()
        self:OnEquipTabBalloon()
    end)


    self.m_confirmChessboardBindingId = self:BindInputPlayerAction("mini_game_balloon_confirm_chessboard", function()
        if not self.m_phase or not DeviceInfo.usingController then
            return
        end
        self.m_phase.m_rotatePlatePanel:OnTriggerConfirmChessboard()
    end)


    self.m_recycleBalloonGridBindingId = self:BindInputPlayerAction("mini_game_balloon_recycle_balloon", function()
        self:OnTriggerRecycleBalloon()
    end)


    self.m_selectBalloonBindingId = self:BindInputPlayerAction("mini_game_balloon_select_balloon", function()
        self:OnTriggerSelectBalloon()
    end)


    self.m_switchToChessboardBindingId = self:BindInputPlayerAction("mini_game_balloon_switch_to_chessboard", function()
        self:OnTriggerSwitchToChessboard()
    end)


    self.m_switchToListBindingId = self:BindInputPlayerAction("mini_game_balloon_switch_to_list", function()
        self:OnTriggerSwitchToList()
    end)


    self.m_cancelSelectBindingId = self:BindInputPlayerAction("mini_game_balloon_cancel_select", function()
        self:OnCancelSelect()
    end)


    self.m_nextLevelBindingId = self:BindInputPlayerAction("mini_game_balloon_next_level", function()
        self.m_phase:OnClickNextBtn()
    end)

    InputManagerInst:ToggleBinding(self.m_tabEquipBalloonBindingId, false)
    InputManagerInst:ToggleBinding(self.m_selectBalloonBindingId, false)
    self:ToggleChessboardSelectBindingId(false)
    self:ToggleChessboardBindingId(false)

    self:InitData()
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

BalloonMiniGameCtrl.OnShow = HL.Override() << function(self)

end

BalloonMiniGameCtrl.OnHide = HL.Override() << function(self)

end

BalloonMiniGameCtrl.OnClose = HL.Override() << function(self)
    if self.m_phase.curGame then
        self.m_phase.curGame.onChessboardStateChange:RemoveAllListeners()
    end
end

BalloonMiniGameCtrl._OnClickNoticeBtn = HL.Method() << function(self)
    self.m_phase.m_isHintUse = true
    self.m_phase.m_useHintTs = DateTimeUtils.GetCurrentTimestampBySeconds()
    self.m_phase.m_rotatePlatePanel:ToggleAllAnswer(true)
    self:_ResetLevel()
end

BalloonMiniGameCtrl._OnClickReset = HL.Method() << function(self)
    self:Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_BALLOON_RESET_CONFIRM_SUB_CONTENT,
        onConfirm = function()
            self:_ResetLevel()
        end,
    })
    self:UpdateNoActionNoticeTimer()
end

BalloonMiniGameCtrl._ResetLevel = HL.Method() << function(self)
    self.m_phase.curGame:ResetLevel()
    self.m_phase:SwitchState(MiniGameBalloonUtils.GAME_STATE.PLAY)
    self:UpdateResetBtn()
    self:_UpdateNoticeTimer()
end

BalloonMiniGameCtrl.SwitchState = HL.Method(HL.String) << function(self, state)
    self.view.main:SetState(state)
    InputManagerInst:ToggleBinding(self.m_nextLevelBindingId, state == MiniGameBalloonUtils.GAME_STATE.COMPLETE)
    if state == MiniGameBalloonUtils.GAME_STATE.PLAY then
        local isFirst = self.m_isFirstInit
        self.m_isFirstInit = true
        self:UpdateNewLevel()

        self:_UpdateNoticeTimer(isFirst)
    elseif state == MiniGameBalloonUtils.GAME_STATE.COMPLETE then
        self:_StopNoticeTimer()
    elseif state == MiniGameBalloonUtils.GAME_STATE.FIRST_IN then
        self:_StopNoticeTimer()
    elseif state == MiniGameBalloonUtils.GAME_STATE.END then
        self:_StopNoticeTimer()
    end
end

BalloonMiniGameCtrl._OnUpdateCell = HL.Method(HL.Userdata, HL.Number) << function(self, cell, index)
    self.m_balloonCells[index] = cell
    local blockTypeEnum = self.m_balloonTypes[index]
    local liftNum = blockTypeEnum:GetHashCode()
    cell:InitBalloonTabCell(self, index, liftNum)
    if self.m_phase and self.m_phase.m_rotatePlatePanel then
        cell.view.balloonDragItem.dragObjectParent = self.m_phase.m_rotatePlatePanel.view.dragTarget.transform
    end
    if index == 1 and self.m_isFirstInit then
        self.m_isFirstInit = false
        self:SetNaviTarget(cell.view.inputBindingGroupNaviDecorator)
    end
end

BalloonMiniGameCtrl._OnRefreshCellView = HL.Method(HL.Number) << function(self, index)
    if not self.m_balloonCells or not self.m_balloonCells[index] then
        return
    end
    local blockTypeEnum = self.m_balloonTypes[index]
    local cell = self.m_balloonCells[index]
    if not blockTypeEnum or not cell then
        return
    end
    cell:RefreshCellView()
end

BalloonMiniGameCtrl.InitData = HL.Method() << function(self)
    self.view.titleTxt.text = Language.LUA_BALLOON_TITLE_NAME
end

BalloonMiniGameCtrl.UpdateNewLevel = HL.Method() << function(self)
    if not self.m_phase then
        logger.error("phase: [Balloon] is not init")
        return
    end
    local curGame = self.m_phase.curGame
    local curLevel = self.m_phase.curLevel
    local levelRawData = curLevel.rawData

    local resetFunction = function()
        self:UpdateResetBtn()
        self:UpdateConfirmBtn()
    end
    resetFunction()
    curGame.onChessboardStateChange:RemoveAllListeners()
    curGame.onChessboardStateChange:AddListener(function()
        resetFunction()
    end)
    self.m_noActionNoticeThreshold = self.view.config.NOTICE_WAIT_TIME
    self.m_stayNoticeThreshold = self.view.config.NOTICE_WAIT_TIME
    self.view.numTxt.text = string.format("%d/%d", curGame.currentIndex + 1, curGame.levelNum)
    self.view.leftNode.miniGameNameTxt.text = Language[levelRawData.levelTextId]
    self.m_balloonCells = {}
    self.m_balloonTypes = {}
    local balloonConfig = levelRawData.balloonConfig
    for i, blockTypeEnum in ipairs(MiniGameBalloonUtils.BLOCK_STATE_SORT) do
        local configCount = balloonConfig:GetCountByState(blockTypeEnum)
        if configCount > 0 then
            table.insert(self.m_balloonTypes, blockTypeEnum)
        end
    end
    self.view.scrollList:UpdateCount(#self.m_balloonTypes)
    self:RefreshBottomNode()
end

BalloonMiniGameCtrl._StopNoticeTimer = HL.Method() << function(self)
    self.view.balloonNoticeTips.gameObject:SetActiveIfNecessary(false)
    self.view.balloonNoticeTips:Reset()
    self.m_stayNoticeTimerId = self:_ClearTimer(self.m_stayNoticeTimerId)
    self.m_noActionNoticeTimerId = self:_ClearTimer(self.m_noActionNoticeTimerId)
    if self.m_phase and self.m_phase.m_rotatePlatePanel then
        self.m_phase.m_rotatePlatePanel:ToggleAllAnswer(false)
    end
end


BalloonMiniGameCtrl._UpdateNoticeTimer = HL.Method(HL.Opt(HL.Boolean)) << function(self, force)
    if not self.m_phase then
        return
    end
    if (not self.m_phase.m_rotatePlatePanel:GetNeedResetAnswerState() or
        self.view.balloonNoticeTips.gameObject.activeSelf) and not force then
        return
    end
    self.view.balloonNoticeTips.gameObject:SetActiveIfNecessary(false)
    self.view.balloonNoticeTips:Reset()
    self.m_phase.m_rotatePlatePanel:ToggleAllAnswer(false)
    if self.m_chessboardLock or not force then
        return
    end

    self.m_stayNoticeTimerId = self:_ClearTimer(self.m_stayNoticeTimerId)
    self.m_stayNoticeTimerId = self:_StartTimer(self.m_stayNoticeThreshold, function()
        self:_ShowNoticeEntry()
    end)

    self:UpdateNoActionNoticeTimer()
end

BalloonMiniGameCtrl._OnClickConfirmBtn = HL.Method() << function(self)
    local curLevel = self.m_phase.curLevel
    local isEmptyAndBalance = curLevel:CheckChessboardIsBalance() and curLevel:CheckAllBalloonIsEmpty()
    if not isEmptyAndBalance then
        return
    end
    self.m_phase:SwitchState(MiniGameBalloonUtils.GAME_STATE.COMPLETE)
    AudioAdapter.PostEvent("Au_UI_Popup_BalloonRecycle_ConditionsMet")
end

BalloonMiniGameCtrl.UpdateNoActionNoticeTimer = HL.Method() << function(self)
    self.m_noActionNoticeTimerId = self:_ClearTimer(self.m_noActionNoticeTimerId)
    self.m_noActionNoticeTimerId = self:_StartTimer(self.m_noActionNoticeThreshold, function()
        self:_ShowNoticeEntry()
    end)
end

BalloonMiniGameCtrl._ShowNoticeEntry = HL.Method() << function(self)
    self:_ClearNoticeTimer()
    self.m_phase.m_hasHint = true
    self.view.balloonNoticeTips.gameObject:SetActiveIfNecessary(true)
    AudioAdapter.PostEvent("Au_UI_Event_Piece_Notice")
end


BalloonMiniGameCtrl._ClearNoticeTimer = HL.Method() << function(self)
    self.m_stayNoticeTimerId = self:_ClearTimer(self.m_stayNoticeTimerId)
    self.m_noActionNoticeTimerId = self:_ClearTimer(self.m_noActionNoticeTimerId)
end

BalloonMiniGameCtrl.GetNoticeTimerStateArg = HL.Method().Return(HL.Table) << function(self)
    local hasStayNoticeTimer = false
    local stayNoticeRemainTime = 0
    if self.m_stayNoticeTimerId > 0 then
        local triggerTime = TimerManager:GetTimerTriggerTime(self.m_stayNoticeTimerId)
        if triggerTime > 0 then
            hasStayNoticeTimer = true
            stayNoticeRemainTime = math.max(triggerTime - Time.time, 0)
        end
    end

    local hasNoActionNoticeTimer = false
    local noActionNoticeRemainTime = 0
    if self.m_noActionNoticeTimerId > 0 then
        local triggerTime = TimerManager:GetTimerTriggerTime(self.m_noActionNoticeTimerId)
        if triggerTime > 0 then
            hasNoActionNoticeTimer = true
            noActionNoticeRemainTime = math.max(triggerTime - Time.time, 0)
        end
    end

    return {
        noticeTipsVisible = NotNull(self.view.balloonNoticeTips.gameObject) and self.view.balloonNoticeTips.gameObject.activeSelf or false,
        noticeTipsResultVisible = NotNull(self.view.balloonNoticeTips.gameObject) and self.view.balloonNoticeTips.m_noticeClicked,
        hasStayNoticeTimer = hasStayNoticeTimer,
        stayNoticeRemainTime = stayNoticeRemainTime,
        hasNoActionNoticeTimer = hasNoActionNoticeTimer,
        noActionNoticeRemainTime = noActionNoticeRemainTime,
    }
end

BalloonMiniGameCtrl.RecoverNoticeTimerState = HL.Method(HL.Table) << function(self, arg)
    self:_ClearNoticeTimer()
    self.view.balloonNoticeTips.gameObject:SetActiveIfNecessary(false)
    self.view.balloonNoticeTips:Reset()
    if not arg then
        return
    end
    if arg.noticeTipsVisible then
        self:_ClearNoticeTimer()
        self.view.balloonNoticeTips.gameObject:SetActiveIfNecessary(true)
        if arg.noticeTipsResultVisible then
            self.view.balloonNoticeTips.m_noticeClicked = true
            self.view.balloonNoticeTips:ShowTips()
        end
        return
    end
    if self.m_phase and not self.m_phase.m_rotatePlatePanel:GetNeedResetAnswerState() then
        return
    end
    if self.m_chessboardLock then
        return
    end
    if arg.hasStayNoticeTimer then
        self.m_stayNoticeTimerId = self:_StartTimer(arg.stayNoticeRemainTime, function()
            self:_ShowNoticeEntry()
        end)
    end
    if arg.hasNoActionNoticeTimer then
        self.m_noActionNoticeTimerId = self:_StartTimer(arg.noActionNoticeRemainTime, function()
            self:_ShowNoticeEntry()
        end)
    end
end

BalloonMiniGameCtrl.UpdateResetBtn = HL.Method() << function(self)
    local dirty = self.m_phase.curGame:IsCurLevelDirty()
    self.view.bottomNode.resetBtn.interactable = dirty
    self.view.bottomNode.resetBtnRoot:SetState(dirty and "Normal" or "Disable")
end

BalloonMiniGameCtrl.UpdateConfirmBtn = HL.Method() << function(self)
    local curLevel = self.m_phase.curLevel
    local balloonIsEmpty = curLevel:CheckAllBalloonIsEmpty()
    local isBalance = curLevel:CheckChessboardIsBalance()
    self.view.bottomNode.hintNodeNotEnough.gameObject:SetActive(not balloonIsEmpty)
    self.view.bottomNode.hintNodeNotBalance.gameObject:SetActive(not isBalance and balloonIsEmpty)
    self:SetConfirmBtn(balloonIsEmpty and isBalance)
end

BalloonMiniGameCtrl.SetConfirmBtn = HL.Method(HL.Boolean) << function(self, needShow)
    if not self.view.bottomNode.btnConfirm.enabled and needShow then
        AudioAdapter.PostEvent("Au_UI_Toast_BalloonRecycle_Complete")
    end
    self.view.bottomNode.btnConfirm.enabled = needShow
    local isLastGame = self.m_phase.curGame.currentIndex + 1 == self.m_phase.curGame.levelNum
    local normalStateName = isLastGame and "EndState" or "NormalState"
    local disableStateName = isLastGame and "EndDisableState" or "DisableState"
    self.view.bottomNode.btnConfirmRoot:SetState(needShow and normalStateName or disableStateName)
end

BalloonMiniGameCtrl.OnBalloonCellDrop = HL.Method(HL.Table) << function(self, args)
    if not self.m_balloonTypes or #self.m_balloonTypes <= 0 then
        return
    end
    self:RefreshAllTabCellView()
    self:UpdateResetBtn()
    self:UpdateConfirmBtn()
end

BalloonMiniGameCtrl.RefreshAllTabCellView = HL.Method() << function(self)
    if not self.m_balloonCells then
        return
    end
    for i, v in pairs(self.m_balloonCells) do
        self:_OnRefreshCellView(i)
    end
end


BalloonMiniGameCtrl.SetSuppressTabCellCountAnim = HL.Method(HL.Boolean) << function(self, suppress)
    self.m_suppressTabCellCountAnim = suppress
end

BalloonMiniGameCtrl.IsSuppressTabCellCountAnim = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_suppressTabCellCountAnim
end


BalloonMiniGameCtrl.RefreshBottomNode = HL.Method() << function(self)
    local nodeName = "componentsNode0"
    local levelRawData = self.m_phase.curLevel.rawData
    local boardSizeX = levelRawData.boardSizeX
    local boardSizeY = levelRawData.boardSizeY
    local maxBoardSize = math.max(boardSizeX, boardSizeY)
    local index = math.floor((maxBoardSize + 1) / 2)
    local MAX_NUM = 4
    for i = 1, MAX_NUM do
        self.view.bottomNode[nodeName .. i].gameObject:SetActiveIfNecessary(i <= index)
    end
end

BalloonMiniGameCtrl.OnTriggerSwitchToChessboard = HL.Method() << function(self)
    if not self.m_isInList then
        return
    end
    self.m_phase.m_rotatePlatePanel:NaviToCenterGridNode()
end

BalloonMiniGameCtrl.OnTriggerSwitchToList = HL.Method(HL.Opt(HL.Number)) << function(self, index)
    if self.m_isInList then
        return
    end
    index = index or 1
    local cell = self.m_balloonCells[index]
    if not cell then
        return
    end
    local dragChessboardData
    if self.m_phase and self.m_phase.isDragIngCell and self.m_phase.nowDragChessboardItemData then
        dragChessboardData = self.m_phase.nowDragChessboardItemData
    end
    self:SetNaviTarget(cell.view.inputBindingGroupNaviDecorator)

    if dragChessboardData then
        self.m_phase.m_rotatePlatePanel:UpdateGridCellByPosition(dragChessboardData.cx, dragChessboardData.cy)
    end
    self:ToggleChessboardRecycleBindingId(false)
end

BalloonMiniGameCtrl.TrySwitchToFirstListCell = HL.Method().Return(HL.Boolean) << function(self)
    self:ToggleChessboardBindingId(false)
    for i, blockTypeEnum in ipairs(self.m_balloonTypes) do
        local leftCount = self.m_phase.curLevel:GetLeftCountByType(blockTypeEnum)
        if leftCount > 0 then
            self:OnTriggerSwitchToList(i)
            return true
        end
    end
    return false
end


BalloonMiniGameCtrl.OnTriggerSelectBalloon = HL.Method() << function(self)
    if not self.m_phase or not DeviceInfo.usingController then
        return
    end
    local phase = self.m_phase
    local curLevel = self.m_phase.curLevel
    if phase and phase.nowNaviChessboardItemData then
        local cx = phase.nowNaviChessboardItemData.cx
        local cy = phase.nowNaviChessboardItemData.cy
        local curBlockState = curLevel:GetNowPositionState(cx, cy)
        local liftNum = curBlockState:GetHashCode()
        self.m_phase.nowDragChessboardItemData = {
            cx = cx,
            cy = cy,
            liftNum = liftNum > 0 and liftNum or 0,
        }
        self.m_phase:OnBeginDrag()

        self.m_phase.m_rotatePlatePanel:OnTriggerSelectBalloon()
    end
end

BalloonMiniGameCtrl.OnEquipTabBalloon = HL.Method() << function(self)
    if self.balloonLiftNum == -1 or not self.m_phase or not DeviceInfo.usingController then
        return
    end
    self.m_phase.nowDragTabItemData = {
        liftNum = self.balloonLiftNum
    }
    self.m_phase:OnBeginDrag()
    self:RefreshAllTabCellView()
    self.m_phase.m_rotatePlatePanel:NaviToNearestPlaceableNode()
end


BalloonMiniGameCtrl.OnTriggerRecycleBalloon = HL.Method() << function(self)
    if not self.m_phase or not DeviceInfo.usingController then
        return
    end


    local targetItem = self.m_phase.nowDragChessboardItemData or self.m_phase.nowNaviChessboardItemData
    local liftNum = 0
    local recycledFromChessboard = false
    local recycledCx, recycledCy
    if self.m_phase.nowDragTabItemData and self.m_phase.nowDragTabItemData.liftNum then

        liftNum = self.m_phase.nowDragTabItemData.liftNum
    elseif targetItem then

        recycledCx, recycledCy = targetItem.cx, targetItem.cy
        liftNum = self.m_phase.curLevel:GetNowPositionState(recycledCx, recycledCy):GetHashCode()
        self.m_phase.m_rotatePlatePanel:SetBlockStateByPosition(recycledCx, recycledCy, MiniGameBalloonUtils.BalloonBlockState.PLACE_ABLE)
        recycledFromChessboard = true
    end


    local stayOnChessboard = false
    if recycledFromChessboard then
        local BLOCK_STATE = MiniGameBalloonUtils.BalloonBlockState
        for gcx, col in pairs(self.m_phase.m_rotatePlatePanel.m_gridItems) do
            for gcy, _ in pairs(col) do
                local st = self.m_phase.curLevel:GetNowPositionState(gcx, gcy)
                if st ~= BLOCK_STATE.CANT_PLACE and st ~= BLOCK_STATE.PLACE_ABLE then
                    stayOnChessboard = true
                    break
                end
            end
            if stayOnChessboard then break end
        end
    end

    if stayOnChessboard then


        local naviData = self.m_phase.nowNaviChessboardItemData
        if self.m_phase.isDragIngCell then
            self.m_phase:OnEndDrag()
        end


        local stayCx, stayCy = recycledCx, recycledCy
        if naviData then
            stayCx, stayCy = naviData.cx, naviData.cy
        end


        local rotatePlate = self.m_phase.m_rotatePlatePanel
        if stayCx ~= recycledCx or stayCy ~= recycledCy then
            rotatePlate:UpdateGridCellByPosition(stayCx, stayCy)
        end


        local stayItem = rotatePlate.m_gridItems[stayCx] and rotatePlate.m_gridItems[stayCx][stayCy]
        if stayItem then
            rotatePlate:OnGroupSetAsNaviTarget(stayItem.cell, stayCx, stayCy, true)
            self:SetNaviTarget(stayItem.cell.view.inputBindingGroupNaviDecorator)
        end
        self:RefreshAllTabCellView()
        rotatePlate:SetAllPlateState()
    else

        if liftNum == 0 then
            local cell = self.m_balloonCells[1]
            self:SetNaviTarget(cell.view.inputBindingGroupNaviDecorator)
        else
            for i, v in ipairs(self.m_balloonTypes) do
                if v:GetHashCode() == liftNum then
                    local cell = self.m_balloonCells[i]
                    self:SetNaviTarget(cell.view.inputBindingGroupNaviDecorator)
                    break
                end
            end
        end
        self.m_phase.m_rotatePlatePanel:SetAllPlateState()
        self:ToggleChessboardBindingId(false)
    end
end

BalloonMiniGameCtrl.OnCancelSelect = HL.Method() << function(self)
    if not self.m_phase or not DeviceInfo.usingController then
        return
    end
    if not self.m_phase.isDragIngCell then
        return
    end

    local dragChessboardData = self.m_phase.nowDragChessboardItemData
    local nowNaviData = self.m_phase.nowNaviChessboardItemData
    local rotatePlate = self.m_phase.m_rotatePlatePanel
    self.m_phase:OnEndDrag()

    if dragChessboardData then
        AudioAdapter.PostEvent("Au_UI_Event_BalloonRecycle_Put_B")

        local gridItems = rotatePlate.m_gridItems


        local srcItem = gridItems[dragChessboardData.cx] and gridItems[dragChessboardData.cx][dragChessboardData.cy]
        if srcItem then
            rotatePlate:UpdateGridCellByPosition(dragChessboardData.cx, dragChessboardData.cy)
            srcItem.cell:SetBalloonFlatForNavi()
        end

        if nowNaviData then

            local naviItem = gridItems[nowNaviData.cx] and gridItems[nowNaviData.cx][nowNaviData.cy]
            if naviItem then

                if nowNaviData.cx ~= dragChessboardData.cx or nowNaviData.cy ~= dragChessboardData.cy then
                    rotatePlate:UpdateGridCellByPosition(nowNaviData.cx, nowNaviData.cy)
                end

                rotatePlate:OnGroupSetAsNaviTarget(naviItem.cell, nowNaviData.cx, nowNaviData.cy, true)
                self:SetNaviTarget(naviItem.cell.view.inputBindingGroupNaviDecorator)
            end
        elseif srcItem then

            rotatePlate:OnGroupSetAsNaviTarget(srcItem.cell, dragChessboardData.cx, dragChessboardData.cy, true)
            self:SetNaviTarget(srcItem.cell.view.inputBindingGroupNaviDecorator)
        end
    else

        self:TrySwitchToFirstListCell()
    end

    rotatePlate:SetAllPlateState()
end

BalloonMiniGameCtrl.ToggleChessboardSelectBindingId = HL.Method(HL.Boolean) << function(self, active)
    InputManagerInst:ToggleBinding(self.m_selectBalloonBindingId, active)
end

BalloonMiniGameCtrl.ToggleChessboardRecycleBindingId = HL.Method(HL.Boolean) << function(self, active)
    InputManagerInst:ToggleBinding(self.m_recycleBalloonGridBindingId, active)
end

BalloonMiniGameCtrl.ToggleSwitch2ChessboardOrListBindingId = HL.Method(HL.Boolean) << function(self, toList)
    self.m_isInList = toList
    self.view.leftNode.keyHintSwitchLC:SetState(toList and "Select" or "Unselect")
    self.view.leftNode.keyHintSwitchRC:SetState(not toList and "Select" or "Unselect")
end

BalloonMiniGameCtrl.ToggleChessboardBindingId = HL.Method(HL.Boolean) << function(self, active)
    InputManagerInst:ToggleBinding(self.m_confirmChessboardBindingId, active)
    InputManagerInst:ToggleBinding(self.m_cancelSelectBindingId, active)
    self:ToggleChessboardRecycleBindingId(active)
end

HL.Commit(BalloonMiniGameCtrl)
