local EColor = CS.Beyond.Gameplay.EColor
local EPreBlockType = CS.Beyond.Gameplay.EPreBlockType

local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.Puzzle
PuzzleCtrl = HL.Class('PuzzleCtrl', uiCtrl.UICtrl)







PuzzleCtrl.m_cellSize = HL.Field(HL.Number) << -1

PuzzleCtrl.m_info = HL.Field(HL.Table)

PuzzleCtrl.m_spacing = HL.Field(HL.Number) << -1

PuzzleCtrl.m_puzzleGame = HL.Field(HL.Userdata)

PuzzleCtrl.m_chessboardWidthGridNum = HL.Field(HL.Number) << -1

PuzzleCtrl.m_chessboardHeightGridNum = HL.Field(HL.Number) << -1

PuzzleCtrl.m_chessboardGridCells = HL.Field(HL.Forward("UIListCache"))

PuzzleCtrl.m_chessboardRawData = HL.Field(HL.Userdata)

PuzzleCtrl.m_chessboardGridData = HL.Field(HL.Table)

PuzzleCtrl.m_rowConditions = HL.Field(HL.Table)

PuzzleCtrl.m_chessboardRowConditionItems = HL.Field(HL.Forward("UIListCache"))

PuzzleCtrl.m_columnConditions = HL.Field(HL.Table)

PuzzleCtrl.m_chessboardColumnConditionItems = HL.Field(HL.Forward("UIListCache"))

PuzzleCtrl.m_attachPuzzleData = HL.Field(HL.Table)

PuzzleCtrl.m_puzzleRootCells = HL.Field(HL.Forward("UIListCache"))

PuzzleCtrl.m_puzzleBlockShadowCells = HL.Field(HL.Forward("UIListCache"))

PuzzleCtrl.m_id2BlockShadowIndex = HL.Field(HL.Table)

PuzzleCtrl.m_chessboardLock = HL.Field(HL.Boolean) << false

PuzzleCtrl.m_totalPuzzlesNum = HL.Field(HL.Number) << -1

PuzzleCtrl.m_placeholderCellCache = HL.Field(HL.Forward("UIListCache"))

PuzzleCtrl.m_playerNotHoldBlocks = HL.Field(HL.Table)

PuzzleCtrl.m_curActionBlockId = HL.Field(HL.String) << ""

PuzzleCtrl.m_curActionBlockSlot = HL.Field(HL.Forward("PuzzleSlot"))

PuzzleCtrl.m_cachedGridVec = HL.Field(HL.Table)

PuzzleCtrl.m_cachedGridPos = HL.Field(Vector3)

PuzzleCtrl.m_refAnswerGridCellCache = HL.Field(HL.Forward("UIListCache"))

PuzzleCtrl.m_noActionNoticeThreshold = HL.Field(HL.Number) << -1

PuzzleCtrl.m_noActionNoticeTimerId = HL.Field(HL.Number) << -1

PuzzleCtrl.m_stayNoticeThreshold = HL.Field(HL.Number) << -1

PuzzleCtrl.m_stayNoticeTimerId = HL.Field(HL.Number) << -1

PuzzleCtrl.m_conditionStyleRectangle = HL.Field(HL.Boolean) << true

PuzzleCtrl.m_canScrollPuzzleList = HL.Field(HL.Boolean) << false


PuzzleCtrl.m_inMovementSlotCount = HL.Field(HL.Number) << 0

PuzzleCtrl.m_componentsInteractable = HL.Field(HL.Boolean) << false

PuzzleCtrl.m_succ = HL.Field(HL.Boolean) << false

PuzzleCtrl.m_isHotSwitch = HL.Field(HL.Boolean) << false

PuzzleCtrl.m_shouldCloseAfterRecover = HL.Field(HL.Boolean) << false

PuzzleCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.PUZZLE_UNIT_COMPLETE] = 'PuzzleUnitComplete',
    [MessageConst.ON_CHANGE_INPUT_DEVICE_TYPE_FINISHED] = '_OnChangeInputDeviceTypeFinished',
}

PuzzleCtrl.OpenPuzzlePanel = HL.StaticMethod(HL.Table) << function(args)
    local callback = unpack(args)
    local finalArgs = {}
    finalArgs.callback = callback
    PhaseManager:OpenPhase(PhaseId.Puzzle, finalArgs)
end


PuzzleCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_info = args
    self.view.btnClose.onClick:AddListener(function()
        self:_OnClickClose()
    end)

    self.view.resetBtn.onClick:AddListener(function()
        self:_OnClickReset()
    end)

    self.view.btnClick.onClick:AddListener(function()
        self:_OnClickNextBtn()
    end)

    self.view.rotateBtn.onClick:AddListener(function()
        self:RotateCurActionBlock(false)
    end)

    self.view.conditionStyleToggle:InitCommonToggle(function(isOn)
        self:_OnConditionStyleToggleChange(isOn)
    end, true, true)

    local seamlessBlendToPerf = args.seamlessBlendToPerf or false
    PuzzleCtrl.TogglePanelUseBlackOut(PANEL_ID, seamlessBlendToPerf)


    self.view.nameTxt.text = string.isEmpty(args.title) and Language["lang_fixable_props_mat_universal"] or args.title

    self.view.noticeNode:InitPuzzleNoticeTips(function()
        self:_OnClickNoticeBtn()
    end)

    self.m_chessboardGridCells = UIUtils.genCellCache(self.view.chessboardGrid)
    self.m_chessboardRowConditionItems = UIUtils.genCellCache(self.view.rowConditionItem)
    self.m_chessboardColumnConditionItems = UIUtils.genCellCache(self.view.columnConditionItem)
    self.m_placeholderCellCache = UIUtils.genCellCache(self.view.placeholderItem)
    self.m_puzzleRootCells = UIUtils.genCellCache(self.view.puzzleSlot)
    self.m_puzzleBlockShadowCells = UIUtils.genCellCache(self.view.puzzleBlockShadow)
    self.m_refAnswerGridCellCache = UIUtils.genCellCache(self.view.refAnswerGridCell)

    self.m_puzzleGame = GameInstance.player.miniGame.puzzleGame
    self.m_totalPuzzlesNum = self.m_puzzleGame.levelNum

    self.m_cellSize = self.view.gridLayout.cellSize.x
    self.m_spacing = self.view.gridLayout.spacing.x

    self.m_puzzleGame.onChessboardStateChange:AddListener(function()
        self:_OnChessboardStateChange()
    end)

    self.m_puzzleGame.onBlockStateChange:AddListener(function()
        self:UpdateResetBtn()
    end)

    self:BindInputPlayerAction("mini_game_block_rotate", function()
        self:RotateCurActionBlock(false)
    end)

    self:BindInputPlayerAction("mini_game_block_rotate_mouse", function()
        self:RotateCurActionBlock(true)
    end)

    self:_InitController()

    local recoverState = args and args.recoverState or nil
    if recoverState and recoverState.conditionStyleRectangle ~= nil then
        self.m_conditionStyleRectangle = recoverState.conditionStyleRectangle
    end

    self:_UpdatePuzzleProgress()

    if recoverState then
        self:_TryRecoverState(recoverState)
        args.recoverState = nil
    end

    self:_ResetControllerState()
end

PuzzleCtrl.OnClose = HL.Override() << function(self)
    self.m_puzzleGame.onChessboardStateChange:RemoveAllListeners()
    self.m_puzzleGame.onBlockStateChange:RemoveAllListeners()

    if not self.m_isHotSwitch then
        GameInstance.player.miniGame:FinishPuzzleGame()
        if self.m_info ~= nil and self.m_info.callback ~= nil then
            self.m_info.callback(self.m_succ)
        end
    end
    self.m_isHotSwitch = false
end

PuzzleCtrl._RefreshContent = HL.Method() << function(self)

    self.m_chessboardGridData = self:_ProcessChessboardGridsData()
    self.m_chessboardGridCells:Refresh(#self.m_chessboardGridData, function(cell, index)
        self:_UpdateChessboardGrid(cell, index)
    end)

    self.m_refAnswerGridCellCache:Refresh(#self.m_chessboardGridData, function(cell, index)
        self:_UpdateRefAnswerGridCell(cell, index)
    end)

    self.m_rowConditions = self:_ProcessChessboardCondition(true)
    self.m_chessboardRowConditionItems:Refresh(#self.m_rowConditions, function(cell, index)
        self:_UpdateChessboardRowCondition(cell, index)
    end)

    self.m_columnConditions = self:_ProcessChessboardCondition(false)
    self.m_chessboardColumnConditionItems:Refresh(#self.m_columnConditions, function(cell, index)
        self:_UpdateChessboardColumnCondition(cell, index)
    end)

    self.m_attachPuzzleData, self.m_playerNotHoldBlocks = self:_ProcessAttachBlockData()

    self.m_placeholderCellCache:Refresh(#self.m_attachPuzzleData, function(cell, index)
        self:_UpdatePlaceholderCell(cell, index)
    end)

    self.m_id2BlockShadowIndex = {}
    self.m_puzzleBlockShadowCells:Refresh(#self.m_attachPuzzleData, function(cell, index)
        self:_UpdatePuzzleBlockShadow(cell, index)
    end)

    self.m_puzzleRootCells:Refresh(#self.m_attachPuzzleData, function(cell, index)
        self:_UpdatePuzzleCell(cell, index)
    end)

    self.view.cantPlayTips.gameObject:SetActiveIfNecessary(self.m_chessboardLock)
    self.view.selectHighlight.gameObject:SetActiveIfNecessary(false)

    local contentRect = self.view.placeHolderScrollView.content
    local viewportRect = self.view.placeHolderScrollView.viewport
    self.m_canScrollPuzzleList = contentRect.rect.height > viewportRect.rect.height
    self.view.placeHolderScrollView.enabled = self.m_canScrollPuzzleList

    self:ToggleRotateBtnState(false, true)
end

PuzzleCtrl._UpdateNoticeTimer = HL.Method() << function(self)
    self.view.refAnswerGrid.gameObject:SetActiveIfNecessary(false)
    self.view.noticeNode.gameObject:SetActiveIfNecessary(false)
    self.view.noticeNode:Reset()

    if self.m_chessboardLock then
        return
    end

    self.m_stayNoticeTimerId = self:_ClearTimer(self.m_stayNoticeTimerId)
    self.m_stayNoticeTimerId = self:_StartTimer(self.m_stayNoticeThreshold, function()
        self:_ShowNoticeEntry()
    end)

    self:UpdateNoActionNoticeTimer()
end

PuzzleCtrl._ShowNoticeEntry = HL.Method() << function(self)
    self:_ClearNoticeTimer()

    self.view.noticeNode.gameObject:SetActiveIfNecessary(true)

    AudioAdapter.PostEvent("Au_UI_Event_Piece_Notice")

    self.m_hasHint = true
end

PuzzleCtrl._ClearNoticeTimer = HL.Method() << function(self)
    self.m_stayNoticeTimerId = self:_ClearTimer(self.m_stayNoticeTimerId)
    self.m_noActionNoticeTimerId = self:_ClearTimer(self.m_noActionNoticeTimerId)
end

PuzzleCtrl._OnClickReset = HL.Method() << function(self)
    self:Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_PUZZLE_RESET_CONFIRM_DESC,
        onConfirm = function()

            self.m_puzzleGame:ResetCurChessboard()

            self:_RefreshContent()

            self:UpdateResetBtn()

            self:_UpdateNoticeTimer()


            self:_ResetControllerState()
        end,
    })
    self:UpdateNoActionNoticeTimer()
end

PuzzleCtrl._OnClickNoticeBtn = HL.Method() << function(self)
    self.view.refAnswerGrid.gameObject:SetActiveIfNecessary(true)

    AudioAdapter.PostEvent("Au_UI_Event_Piece_Hint")

    self.m_isHintUse = true
    self.m_useHintTs = DateTimeUtils.GetCurrentTimestampBySeconds()
end

PuzzleCtrl._OnClickNextBtn = HL.Method() << function(self)
    local wrapper = self.view.chessboardAnimationWrapper
    wrapper:Play("puzzle_notice_completetips_out", function()
        if self.m_puzzleGame:IsPuzzleComplete() then
            AudioAdapter.PostEvent("Au_UI_Event_Piece_Finish")
            wrapper:Play("puzzle_unlock_complete", function()
                self:_OnClickGameSucc()
            end)
        else
            wrapper:Play("puzzle_chessboard_out", function()
                AudioAdapter.PostEvent("Au_UI_Event_Piece_Refresh")
                self.m_puzzleGame:NextPuzzle()
                self:_UpdatePuzzleProgress()

                self:_ResetControllerState()
                wrapper:Play("puzzle_chessboard_in")

                self:_ToggleComponentInput(true)
            end)
        end
    end)
end

PuzzleCtrl._OnClickClose = HL.Method() << function(self)
    if self.m_chessboardLock then
        self:_DoExitGame()
    else
        self:Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_PUZZLE_EXIT_CONFIRM_DESC,
            onConfirm = function()
                self:_DoExitGame()
            end
        })
    end
end

PuzzleCtrl._DoExitGame = HL.Method() << function(self)
    self.m_puzzleUnitEndTs = DateTimeUtils.GetCurrentTimestampBySeconds()
    self:_EventLogPuzzleUnitEnd()
    PhaseManager:PopPhase(PhaseId.Puzzle)
end

PuzzleCtrl._OnClickGameSucc = HL.Method() << function(self)
    PhaseManager:PopPhase(PhaseId.Puzzle)
end

PuzzleCtrl._UpdateChessboardGrid = HL.Method(HL.Forward("PuzzleChessboardGrid"), HL.Number)
        << function(self, cell, index)
    local gridData = self.m_chessboardGridData[index]
    cell:InitPuzzleChessboardGrid(gridData, function()

        self:PutBlockOnChessboard(self.m_curActionBlockId,
                                  CSIndex(gridData.x),
                                  CSIndex(gridData.y),
                                  cell.transform.position)
    end, function()

        self:_SetCachedGridInfo(cell, index)
        self:_OnSlotEnterChessboardGrid()
    end, function()

        self:ResetCachedGridData()
        self:_OnSlotOutChessboardGrid()
    end)

    if DeviceInfo.usingController then
        cell.view.controllerBtn.onClick:RemoveAllListeners()
        cell.view.controllerBtn.onClick:AddListener(function()
            self:_OnClickChessboardControllerBtn(cell, index)
        end)

        cell.view.controllerBtn.onIsNaviTargetChanged = function(isTarget, isGroupChanged)
            self:_OnIsNaviTargetChangedChessboardGrid(cell, index, isTarget, isGroupChanged)
        end
    end
end

PuzzleCtrl._SetCachedGridInfo = HL.Method(HL.Forward("PuzzleChessboardGrid"), HL.Number)
        << function(self, cell, luaIndex)
    local gridData = self.m_chessboardGridData[luaIndex]
    self.m_cachedGridVec = gridData
    self.m_cachedGridPos = cell.transform.position
end

PuzzleCtrl._UpdateRefAnswerGridCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local gridData = self.m_chessboardGridData[index]
    local refAnswerGrids = self.m_puzzleGame.currentChessboard.refAnswerGrids
    local refAnswerGrid = refAnswerGrids[CSIndex(gridData.x)][CSIndex(gridData.y)]
    local color = UIUtils.getPuzzleColorByColorType(refAnswerGrid.color)
    local colorTrans = Color(color.a, color.g, color.b, cell.grid.color.a)

    cell.grid.gameObject:SetActiveIfNecessary(refAnswerGrid.needNotice)
    cell.upLine.gameObject:SetActiveIfNecessary(refAnswerGrid.upLine)
    cell.downLine.gameObject:SetActiveIfNecessary(refAnswerGrid.downLine)
    cell.leftLine.gameObject:SetActiveIfNecessary(refAnswerGrid.leftLine)
    cell.rightLine.gameObject:SetActiveIfNecessary(refAnswerGrid.rightLine)
    cell.upRightDrop.gameObject:SetActiveIfNecessary(refAnswerGrid.upRightDrop)
    cell.upLeftDrop.gameObject:SetActiveIfNecessary(refAnswerGrid.upLeftDrop)
    cell.downRightDrop.gameObject:SetActiveIfNecessary(refAnswerGrid.downRightDrop)
    cell.downLeftDrop.gameObject:SetActiveIfNecessary(refAnswerGrid.downLeftDrop)

    if refAnswerGrid.needNotice then
        cell.lineNode.color = color
        cell.grid.color = colorTrans
    end
end

PuzzleCtrl._OnSlotEnterChessboardGrid = HL.Method() << function(self)
    if not self.m_cachedGridVec or not self.m_cachedGridPos then
        return
    end

    if not self.m_curActionBlockSlot or not self.m_curActionBlockSlot:IsStateDragging() then
        return
    end

    local index = self.m_id2BlockShadowIndex[self.m_curActionBlockId]
    local shadowCell = self.m_puzzleBlockShadowCells:GetItem(index)
    shadowCell:SetVisible(true)
    shadowCell:SetPosition(self.m_cachedGridPos)
    if self.m_puzzleGame:CheckBlockOnChessboardLegal(self.m_curActionBlockId,
                                                     CSIndex(self.m_cachedGridVec.x),
                                                     CSIndex(self.m_cachedGridVec.y))
    then

        shadowCell:SetLegal(true)
        AudioAdapter.PostEvent("Au_UI_Event_Piece_Correct")
    else

        shadowCell:SetLegal(false)
        AudioAdapter.PostEvent("Au_UI_Event_Piece_Warning")
    end
end

PuzzleCtrl._OnSlotOutChessboardGrid = HL.Method() << function(self)
    if not self.m_curActionBlockSlot or not self.m_curActionBlockSlot:IsStateDragging() then
        return
    end

    self:SetShadowCellVisibleById(self.m_curActionBlockId, false)
end

PuzzleCtrl._ProcessChessboardGridsData = HL.Method().Return(HL.Table) << function(self)
    local tbl = {}

    for j = 1, self.m_chessboardHeightGridNum do
        for i = 1, self.m_chessboardWidthGridNum do
            table.insert(tbl,{x = i, y = j, state = EColor.Clear})
        end
    end


    for i = 0, self.m_chessboardRawData.bannedGrids.Count - 1 do


        local vec = self.m_chessboardRawData.bannedGrids[i]
        local index = (LuaIndex(vec.y) - 1) * self.m_chessboardWidthGridNum + LuaIndex(vec.x)
        tbl[index].state = EColor.Banned
    end


    for k, v in pairs(self.m_chessboardRawData.preGrids) do
        for i = 0, v.Count - 1 do
            local vec = v[i]

            local index = (LuaIndex(vec.y) - 1) * self.m_chessboardWidthGridNum + LuaIndex(vec.x)
            tbl[index].state = k
        end
    end

    return tbl
end

PuzzleCtrl._ProcessChessboardCondition = HL.Method(HL.Boolean).Return(HL.Table) << function(self, isRow)
    local resultConditions = {}
    local rawConditionsDic = isRow and self.m_chessboardRawData.rowCondition or self.m_chessboardRawData.columnCondition
    for eColor, rawConditions in pairs(rawConditionsDic) do
        local _, state
        if isRow then
            _, state = self.m_puzzleGame.currentChessboard.rowState:TryGetValue(eColor)
        else
            _, state = self.m_puzzleGame.currentChessboard.columnState:TryGetValue(eColor)
        end


        for i = 0, rawConditions.Length - 1 do
            local luaIndex = LuaIndex(i)
            if not resultConditions[luaIndex] then
                resultConditions[luaIndex] = {}
            end

            if not resultConditions[luaIndex][eColor] then
                resultConditions[luaIndex][eColor] = {}
                resultConditions[luaIndex][eColor].conditions = {}
            end


            resultConditions[luaIndex][eColor].stateCount = state[i]
            resultConditions[luaIndex][eColor].rawCount = rawConditions[i]

            local overflow = state[i] < 0
            local diffCount = overflow and state[i] or 0



            for j = 1, rawConditions[i] - diffCount do
                if overflow then
                    resultConditions[luaIndex][eColor].overflow = true
                    table.insert(resultConditions[luaIndex][eColor].conditions,
                                 { overflow = j > rawConditions[i], done = j <= rawConditions[i]})
                else
                    table.insert(resultConditions[luaIndex][eColor].conditions,
                                 { done = rawConditions[i] - state[i] >= j , overflow = false})
                end
            end

        end
    end

    return resultConditions
end

PuzzleCtrl._UpdateChessboardRowCondition = HL.Method(HL.Forward("PuzzleChessboardConditionItem"), HL.Int) << function(self, cell, index)
    cell:InitPuzzleChessboardConditionItem(self.m_rowConditions[index],
                                           self.m_cellSize,
                                           self.m_spacing,
                                           self.m_chessboardWidthGridNum)


    cell:Toggle(self.m_conditionStyleRectangle)
end

PuzzleCtrl._UpdateChessboardColumnCondition = HL.Method(HL.Forward("PuzzleChessboardConditionItem"), HL.Int) << function(self, cell, index)
    cell:InitPuzzleChessboardConditionItem(self.m_columnConditions[index],
                                           self.m_cellSize,
                                           self.m_spacing,
                                           self.m_chessboardHeightGridNum)


    cell:Toggle(self.m_conditionStyleRectangle)
end

PuzzleCtrl._OnConditionStyleToggleChange = HL.Method(HL.Boolean) << function(self, rectangle)
    self.m_conditionStyleRectangle = rectangle

    self.m_chessboardRowConditionItems:Update(function(cell, index)
        cell:Toggle(rectangle)
    end)

    self.m_chessboardColumnConditionItems:Update(function(cell, index)
        cell:Toggle(rectangle)
    end)
end

PuzzleCtrl._ProcessAttachBlockData = HL.Method().Return(HL.Table, HL.Table) << function(self)
    local attachBlocks = {}
    local playerNotHoldBlocks = {}
    local blocks = self.m_puzzleGame.currentChessboard.blocks


    local playerNotHolderKeys = {}
    for k, v in pairs(blocks) do
        local rawData = v.rawData

        local playerHold = v.blockSource == EPreBlockType.SystemAssign
        if not playerHold then
            local hasConfig, _ = Tables.itemTable:TryGetValue(rawData.blockID)
            if not hasConfig then
                logger.error(ELogChannel.MiniGame, string.format("no itemCfg，blockID:%s", rawData.blockID))
                return attachBlocks, playerNotHoldBlocks
            end

            playerHold = v.blockSource == EPreBlockType.PlayerCollection and Utils.getItemCount(rawData.blockID) > 0
        end

        if playerHold or (not playerHold and not lume.find(playerNotHolderKeys, rawData.blockID)) then
            local blockUnit = {}
            blockUnit.id = k
            blockUnit.color = v.color
            blockUnit.sortId = v.sortId
            blockUnit.playerHold = playerHold
            blockUnit.playerHoldSortId = playerHold and 0 or 1
            blockUnit.resPath = rawData.resName
            blockUnit.originBlocks = rawData.originBlocks
            blockUnit.rawId = rawData.blockID
            blockUnit.rawRotationCount = v.rawRotationCount

            table.insert(attachBlocks, blockUnit)
            if not playerHold then
                self.m_chessboardLock = true
                table.insert(playerNotHolderKeys, rawData.blockID)
                table.insert(playerNotHoldBlocks, blockUnit)
            end
        end
    end

    table.sort(attachBlocks, Utils.genSortFunction({"playerHoldSortId", "sortId"}))

    return attachBlocks, playerNotHoldBlocks
end

PuzzleCtrl._UpdatePlaceholderCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local data = self.m_attachPuzzleData[index]
    if not data.playerHold then
        cell.iconGray:LoadSprite(UIConst.UI_SPRITE_MINIGAME_BLOCK, data.resPath..UIConst.UI_MINIGAME_PUZZLE_GREY_BLOCK_SUFFIX)
        cell.lockedBtn.onClick:RemoveAllListeners()
        cell.lockedBtn.onClick:AddListener(function()
            PhaseManager:OpenPhase(PhaseId.PuzzleTrackPopup, { blocks = self.m_playerNotHoldBlocks,
                                                               selectBlockId = data.rawId })
        end)
    end
    cell.locked.gameObject:SetActiveIfNecessary(not data.playerHold)

    if DeviceInfo.usingController then
        cell.controllerBtn.onClick:RemoveAllListeners()
        cell.controllerBtn.onClick:AddListener(function()
            if not data.playerHold then
                PhaseManager:OpenPhase(PhaseId.PuzzleTrackPopup, { blocks = self.m_playerNotHoldBlocks,
                                                                   selectBlockId = data.rawId })
            else
                self:_OnClickPlaceholderControllerBtn(index)
            end
        end)

        cell.controllerBtn.onIsNaviTargetChanged = function(isTarget, isGroupChanged)
            self:_OnIsNaviTargetChangedPlaceholder(index, isTarget, isGroupChanged)
        end

        cell.controllerBtn.enabled = self.m_chessboardLock and not data.playerHold or not self.m_chessboardLock
    end
end

PuzzleCtrl._UpdatePuzzleCell = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local data = self.m_attachPuzzleData[index]
    local placeholderCell = self.m_placeholderCellCache:GetItem(index)

    cell:InitPuzzleSlot(data, self.m_cellSize, index, self, placeholderCell, self.m_chessboardLock)
    cell.gameObject:SetActiveIfNecessary(data.playerHold)
end

PuzzleCtrl._UpdatePuzzleBlockShadow = HL.Method(HL.Any, HL.Number) << function(self, cell, index)
    local data = self.m_attachPuzzleData[index]
    cell:InitPuzzleBlockShadow(data)

    self.m_id2BlockShadowIndex[data.id] = index
end

PuzzleCtrl._OnChessboardStateChange = HL.Method() << function(self)
    self.m_rowConditions = self:_ProcessChessboardCondition(true)
    self.m_chessboardRowConditionItems:Update(function(cell, index)
        cell:UpdateContent(self.m_rowConditions[index], self.m_cellSize, self.m_spacing, self.m_chessboardWidthGridNum)
    end)

    self.m_columnConditions = self:_ProcessChessboardCondition(false)
    self.m_chessboardColumnConditionItems:Update(function(cell, index)
        cell:UpdateContent(self.m_columnConditions[index], self.m_cellSize, self.m_spacing, self.m_chessboardHeightGridNum)
    end)
end

PuzzleCtrl._UpdatePuzzleProgress = HL.Method() << function(self)
    self.m_chessboardGridData = {}
    self.m_rowConditions = {}
    self.m_columnConditions = {}
    self:ClearCurActionBlock()
    self:ResetCachedGridData()

    local chessboardData = self.m_puzzleGame.currentChessboard.rawData
    local width = (self.m_cellSize + self.m_spacing) * chessboardData.sizeX
    local height = (self.m_cellSize + self.m_spacing) * chessboardData.sizeY

    self.m_chessboardRawData = chessboardData
    self.m_chessboardWidthGridNum = chessboardData.sizeX
    self.m_chessboardHeightGridNum = chessboardData.sizeY
    self.m_noActionNoticeThreshold = chessboardData.noActionNoticeThreshold
    self.m_stayNoticeThreshold = chessboardData.stayNoticeThreshold

    self.view.chessboard.sizeDelta = Vector2(width, height)

    self.view.progressTxt.text = string.format(Language.LUA_PUZZLE_PANEL_PROGRESS_FORMAT,
                                               LuaIndex(self.m_puzzleGame.currentIndex), self.m_totalPuzzlesNum)
    self.view.cantPlayTips.gameObject:SetActiveIfNecessary(false)
    self.view.completeTips.gameObject:SetActiveIfNecessary(false)
    self.view.resetBtn.interactable = false

    self:_RefreshContent()
    self:_UpdateNoticeTimer()


    self.m_hasHint = false
    self.m_isHintUse = false
    self.m_useHintTs = 0
    self.m_puzzleUnitStartTs = DateTimeUtils.GetCurrentTimestampBySeconds()
    self.m_puzzleUnitEndTs = 0
    self.m_curPuzzleUnitComplete = false
end

PuzzleCtrl._ToggleComponentInput = HL.Method(HL.Boolean) << function(self, isOn)
    self.view.btnClose.interactable = isOn
    self.view.conditionStyleToggle.view.toggle.enabled = isOn
end

PuzzleCtrl.UpdateResetBtn = HL.Method() << function(self)
    local dirty = self.m_puzzleGame:IsPuzzleDirty()
    self.view.resetBtn.interactable = self.m_componentsInteractable and dirty
end

PuzzleCtrl.SetShadowCellVisibleById = HL.Method(HL.String, HL.Boolean) << function(self, blockId, visible)
    local index = self.m_id2BlockShadowIndex[blockId]
    local shadowCell = self.m_puzzleBlockShadowCells:GetItem(index)
    shadowCell:SetVisible(visible)
end

PuzzleCtrl.RotateShadowById = HL.Method(HL.String) << function(self, blockId)
    local index = self.m_id2BlockShadowIndex[blockId]
    local shadowCell = self.m_puzzleBlockShadowCells:GetItem(index)

    local block = self.m_puzzleGame.currentChessboard.blocks:get_Item(blockId)
    shadowCell:Rotate(block.rotateCount)
end

PuzzleCtrl.UpdateNoActionNoticeTimer = HL.Method() << function(self)
    self.m_noActionNoticeTimerId = self:_ClearTimer(self.m_noActionNoticeTimerId)
    self.m_noActionNoticeTimerId = self:_StartTimer(self.m_noActionNoticeThreshold, function()
        self:_ShowNoticeEntry()
    end)
end


PuzzleCtrl.PuzzleUnitComplete = HL.Method(HL.Any) << function(self)
    self.view.btnClick.interactable = false
    local wrapper = self.view.chessboardAnimationWrapper
    wrapper:Play("puzzle_notice_completetips_in", function()
        self.view.btnClick.interactable = true
    end)

    AudioAdapter.PostEvent("Au_UI_Event_Piece_Success")

    self.m_curPuzzleUnitComplete = true
    self.m_puzzleUnitEndTs = DateTimeUtils.GetCurrentTimestampBySeconds()
    if self.m_puzzleGame:IsPuzzleComplete() then
        self.m_succ = true
    end
    self:_EventLogPuzzleUnitEnd()
end

PuzzleCtrl.SetBlockOutScrollRect = HL.Method(Transform) << function(self, trans)
    trans:SetParent(self.view.outBlockArea)
end

PuzzleCtrl.SetOtherGraphicRaycasts = HL.Method(HL.String, HL.Boolean) << function(self, curBlockInstId, on)

    self.m_puzzleRootCells:Update(function(slot, _)
        local instId = slot:GetInstId()
        if curBlockInstId ~= instId then
            slot:ToggleInteractable(on)
        end
    end)
end

PuzzleCtrl.SetOtherBlocksFading = HL.Method(HL.String, HL.Boolean) << function(self, blockId, isOn)

    self.m_puzzleRootCells:Update(function(slot, _)
        slot:SetBlockFading(blockId, isOn)
    end)
end

PuzzleCtrl.SetCurActionBlock = HL.Method(HL.String, HL.Forward("PuzzleSlot")) << function(self, blockId, slot)
    if self.m_curActionBlockId == blockId then
        return
    end

    local preActionBlockId = self.m_curActionBlockId
    local preActionBlockSlot = self.m_curActionBlockSlot
    self.m_curActionBlockId = blockId
    self.m_curActionBlockSlot = slot

    self.view.selectHighlight.gameObject:SetActiveIfNecessary(true)






    if string.isEmpty(preActionBlockId) then
        return
    end

    preActionBlockSlot:ResetState()
    local getPreBlockSucc, preBlockData = self.m_puzzleGame.currentChessboard.blocks:TryGetValue(preActionBlockId)
    if getPreBlockSucc and preBlockData.locationOnChessboard and preBlockData.isIllegalLocate then

        local succ, _ = self:PutBlockOnChessboard(preActionBlockId,
                                                  preBlockData.location.x,
                                                  preBlockData.location.y,
                                                  preBlockData.forceLocation)
        if not succ then
            preActionBlockSlot:ResetToPlaceholder(true)
        end
    end
end

PuzzleCtrl.ClearCurActionBlock = HL.Method() << function(self)
    self.m_curActionBlockId = ""
    self.m_curActionBlockSlot = nil

    self.view.selectHighlight.gameObject:SetActiveIfNecessary(false)
end

PuzzleCtrl.RotateCurActionBlock = HL.Method(HL.Boolean) << function(self, isMouseClick)
    if self.m_curActionBlockSlot and
            (isMouseClick and self.m_curActionBlockSlot:IsStateDragging() or not isMouseClick) then
        self.m_curActionBlockSlot:Rotate(self.m_curActionBlockId)
        self:_OnSlotEnterChessboardGrid()
    end
end

PuzzleCtrl.ResetCachedGridData = HL.Method() << function(self)
    self.m_cachedGridVec = nil
    self.m_cachedGridPos = nil
end

PuzzleCtrl.PuzzleSlotInMovement = HL.Method() << function(self)
    self.m_inMovementSlotCount = self.m_inMovementSlotCount + 1
    self.view.placeHolderScrollView.enabled = self.m_canScrollPuzzleList and self.m_inMovementSlotCount == 0

    logger.info(ELogChannel.MiniGame, string.format("m_inMovementSlotCount == %s", self.m_inMovementSlotCount))

    self.m_puzzleRootCells:Update(function(slotCell, _)
        slotCell:ToggleInteractable(self.m_inMovementSlotCount == 0)
    end)
end

PuzzleCtrl.PuzzleSlotOutMovement = HL.Method() << function(self)
    self.m_inMovementSlotCount = self.m_inMovementSlotCount - 1
    self.view.placeHolderScrollView.enabled = self.m_canScrollPuzzleList and self.m_inMovementSlotCount == 0

    logger.info(ELogChannel.MiniGame, string.format("m_inMovementSlotCount == %s", self.m_inMovementSlotCount))

    self.m_puzzleRootCells:Update(function(slotCell, _)
        slotCell:ToggleInteractable(self.m_inMovementSlotCount == 0)
    end)
end

PuzzleCtrl.DisableComponentsInteractable = HL.Method() << function(self)
    self.m_componentsInteractable = false

    self.view.btnClose.enabled = false

    self.view.placeHolderScrollView.enabled = false


    self.view.conditionStyleToggle:ToggleInteractable(DeviceInfo.usingController)
    self.view.noticeNode:ToggleNoticeBtnInteractable(DeviceInfo.usingController)
    self:UpdateResetBtn()
end

PuzzleCtrl.RecoverComponentsInteractable = HL.Method() << function(self)
    self.m_componentsInteractable = true

    self.view.btnClose.enabled = true

    self.view.placeHolderScrollView.enabled = self.m_canScrollPuzzleList and self.m_inMovementSlotCount == 0


    self.view.conditionStyleToggle:ToggleInteractable(true)
    self.view.noticeNode:ToggleNoticeBtnInteractable(true)
    self:UpdateResetBtn()
end

PuzzleCtrl.PutBlockOnChessboard = HL.Method(HL.String, HL.Number, HL.Number, Vector3).Return(HL.Boolean, HL.Boolean)
        << function(self, blockId, x, y, targetPos)
    local succ, complete = self.m_puzzleGame:PutBlockOnChessboard(blockId, x, y, targetPos)
    if complete then
        self:PuzzleUnitCompleteClient()
    end

    return succ, complete
end

PuzzleCtrl.PuzzleUnitCompleteClient = HL.Method() << function(self)
    self.m_puzzleRootCells:Update(function(cell, luaIndex)
        cell:ToggleCanDrag(false)
    end)

    self:_ClearNoticeTimer()
    self:_ToggleComponentInput(false)
    if DeviceInfo.usingController then
        self:SetNaviTarget(nil)
        self:_ToggleAreaFocusActionId(false, false)
        InputManagerInst:ToggleGroup(self.view.leftNode.groupId, false)
        InputManagerInst:ToggleGroup(self.view.gridInputBindingGroupMonoTarget.groupId, false)
    end
end



PuzzleCtrl.ToggleRotateBtnState = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, isOn, ignoreAnim)
    if not DeviceInfo.usingTouch then
        return
    end

    if ignoreAnim then
        self.view.rotateCon.gameObject:SetActiveIfNecessary(isOn)
    else
        if isOn and not self.view.rotateCon.gameObject.activeInHierarchy then
            self.view.rotateCon.gameObject:SetActiveIfNecessary(true)
            self.view.rotateCon:ClearTween()
            self.view.rotateCon:PlayInAnimation()
        elseif not isOn and self.view.rotateCon.gameObject.activeInHierarchy then
            self.view.rotateCon:PlayOutAnimation(function()
                self.view.rotateCon.gameObject:SetActiveIfNecessary(false)
            end)
        end
    end
end





PuzzleCtrl.m_focusChessboardActionId = HL.Field(HL.Number) << -1

PuzzleCtrl.m_focusBlockListActionId = HL.Field(HL.Number) << -1

PuzzleCtrl.m_curCtrSlotResetToPlaceholderActionId = HL.Field(HL.Number) << -1

PuzzleCtrl.m_curControllerSlotLuaIndex = HL.Field(HL.Number) << -1

PuzzleCtrl.m_cachedReturnGridIndex = HL.Field(HL.Number) << -1

PuzzleCtrl.m_curCtrlSlotResetToPreGridActionId = HL.Field(HL.Number) << -1

PuzzleCtrl.m_curControllerHoverSlotLuaIndex = HL.Field(HL.Number) << -1

PuzzleCtrl._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    self.m_focusChessboardActionId = self:BindInputPlayerAction("puzzle_focus_chessboard", function()
        self:_FocusChessboard()
    end)

    self.m_focusBlockListActionId = self:BindInputPlayerAction("puzzle_focus_block_list", function()
        self:_FocusBlockList()
    end)

    self.m_curCtrSlotResetToPlaceholderActionId = self:BindInputPlayerAction("puzzle_cur_ctrl_slot_reset_to_placeholder", function()
        self:_CurCtrlSlotResetToPlaceholder()
    end)

    self.m_curCtrlSlotResetToPreGridActionId = self:BindInputPlayerAction("puzzle_cur_ctrl_slot_reset_to_pre_grid", function()
        self:_CurCtrlSlotResetToPreGrid()
    end)

    self.view.chessboardSelectableNaviGroup.onDefaultNaviFailed:AddListener(function(dir)
        self:_OnDefaultNaviFailedChessboard(dir)
    end)

    self.view.blockArea.onDefaultNaviFailed:AddListener(function(dir)
        self:_OnDefaultNaviFailedBlockArea(dir)
    end)

    self:_ToggleAreaFocusActionId(false, true)
    self:_ToggleChessboardNaviToBlockList(true)
end

PuzzleCtrl._GetGridLuaIndexByVec = HL.Method(HL.Number, HL.Number).Return(HL.Number) << function(self, x, y)
    return y * self.m_chessboardWidthGridNum + x + 1
end

PuzzleCtrl._OnClickPlaceholderControllerBtn = HL.Method(HL.Number) << function(self, luaIndex)
    self.m_curControllerSlotLuaIndex = luaIndex

    local slot = self.m_puzzleRootCells:Get(luaIndex)

    local blockId = self.m_attachPuzzleData[luaIndex].id
    local succ, block = self.m_puzzleGame.currentChessboard.blocks:TryGetValue(blockId)
    local gridIndex
    if succ and block.locationOnChessboard then

        local vec = block.location
        gridIndex = vec.y * self.m_chessboardWidthGridNum + vec.x + 1
        gridIndex = self:_GetGridLuaIndexByVec(vec.x, vec.y)
    else

        local count = self.m_chessboardGridCells:GetCount()
        gridIndex = count // 2 + 1
    end


    slot:_OnBeginDrag(nil)

    local chessboardCell = self.m_chessboardGridCells:Get(gridIndex)

    self:SetNaviTarget(chessboardCell.view.controllerBtn)





    self:_ToggleChessboardNaviToBlockList(false)
    self:_ToggleAreaFocusActionId(false, false)
end

PuzzleCtrl._OnClickChessboardControllerBtn = HL.Method(HL.Forward("PuzzleChessboardGrid"), HL.Number)
        << function(self, cell, luaIndex)

    if self.m_curControllerSlotLuaIndex > 0 then

        local gridData = self.m_chessboardGridData[luaIndex]
        self:PutBlockOnChessboard(self.m_curActionBlockId,
                                  CSIndex(gridData.x),
                                  CSIndex(gridData.y),
                                  cell.transform.position)
        self.m_curActionBlockSlot:_OnEndDrag(nil, false)

        local targetIndex

        for luaIndex, slotInfo in ipairs(self.m_attachPuzzleData) do
            local succ, block = self.m_puzzleGame.currentChessboard.blocks:TryGetValue(slotInfo.id)
            if succ and not block.locationOnChessboard then
                targetIndex = luaIndex
                break
            end
        end


        if targetIndex then
            local cell = self.m_placeholderCellCache:Get(targetIndex)
            self:SetNaviTarget(cell.controllerBtn)
        else
            self:_TryHoverPuzzleSlot(cell.rectTransform, self.m_curControllerSlotLuaIndex)
        end


        self.m_curControllerSlotLuaIndex = -1

        self:_ToggleChessboardNaviToBlockList(true)
        self:_ToggleAreaFocusActionId(targetIndex == nil, targetIndex ~= nil)

        self.m_cachedReturnGridIndex = -1
    else

        local slotLuaIndex = self.view.puzzleControllerHelper:TryGetPuzzleSlotIndexByPos(cell.rectTransform)
        if slotLuaIndex > 0 then
            self.m_curControllerSlotLuaIndex = slotLuaIndex

            local slot = self.m_puzzleRootCells:Get(slotLuaIndex)

            slot:_OnBeginDrag(nil)


            local slotInfo = self.m_attachPuzzleData[slotLuaIndex]
            local succ, block = self.m_puzzleGame.currentChessboard.blocks:TryGetValue(slotInfo.id)
            local curLocationGridIndex
            if succ and block.locationOnChessboard then
                curLocationGridIndex = self:_GetGridLuaIndexByVec(block.location.x, block.location.y)
                self.m_cachedReturnGridIndex = curLocationGridIndex
            else
                logger.error("[puzzle game] cache returnGridIndex fail")
            end

            if curLocationGridIndex then


                local grid = self.m_chessboardGridCells:Get(curLocationGridIndex)
                self:SetNaviTarget(nil)


                self:SetNaviTarget(grid.view.controllerBtn)
            end

            self:_ToggleChessboardNaviToBlockList(false)
            self:_ToggleAreaFocusActionId(false, false)
        end
    end
end

PuzzleCtrl._OnIsNaviTargetChangedChessboardGrid = HL.Method(HL.Forward("PuzzleChessboardGrid"), HL.Number, HL.Boolean, HL.Boolean)
        << function(self, cell, luaIndex, isTarget, isGroupChanged)

    if not isTarget then
        return
    end

    if isGroupChanged then
        self:_ToggleAreaFocusActionId(true, false)
    end

    self:_TryHoverPuzzleSlot(cell.rectTransform)


    if self.m_curControllerSlotLuaIndex < 0 then
        return
    end


    self.m_curActionBlockSlot.transform.position = cell.transform.position
    local rawAnchoredPosition = self.m_curActionBlockSlot.rectTransform.anchoredPosition
    self.m_curActionBlockSlot.rectTransform.anchoredPosition = rawAnchoredPosition + self.view.config.CONTROLLER_SLOT_OFFSET


    self:_SetCachedGridInfo(cell, luaIndex)
    self:_OnSlotEnterChessboardGrid()
end

PuzzleCtrl._TryHoverPuzzleSlot = HL.Method(Transform, HL.Opt(HL.Number)) << function(self, cellRectTransform, slotIndex)
    local slotLuaIndex = slotIndex or self.view.puzzleControllerHelper:TryGetPuzzleSlotIndexByPos(cellRectTransform)
    if self.m_curControllerHoverSlotLuaIndex == slotLuaIndex then
        return
    end

    if self.m_curControllerHoverSlotLuaIndex> -1 then

        local preHoverSlot = self.m_puzzleRootCells:Get(self.m_curControllerHoverSlotLuaIndex)
        preHoverSlot:ToggleHover(false)
    end

    if slotLuaIndex > -1 then

        local curHoverSlot = self.m_puzzleRootCells:Get(slotLuaIndex)
        curHoverSlot:ToggleHover(true)
    end

    self.m_curControllerHoverSlotLuaIndex = slotLuaIndex


end

PuzzleCtrl._OnIsNaviTargetChangedPlaceholder = HL.Method(HL.Number, HL.Boolean, HL.Boolean) << function(self, luaIndex, isTarget, isGroupChanged)
    if isTarget then
        if isGroupChanged then
            self:_ToggleAreaFocusActionId(false, true)
        end
    else

    end


    local puzzleSlot = self.m_puzzleRootCells:Get(luaIndex)





end

PuzzleCtrl._OnDefaultNaviFailedChessboard = HL.Method(CS.UnityEngine.UI.NaviDirection) << function(self, dir)
    if dir ~= Unity.UI.NaviDirection.Right then
        return
    end

    if self.m_curControllerSlotLuaIndex > 0 then
        return
    end

    self:_FocusBlockList()
end


PuzzleCtrl._OnDefaultNaviFailedBlockArea = HL.Method(CS.UnityEngine.UI.NaviDirection) << function(self, dir)
    if dir ~= Unity.UI.NaviDirection.Left then
        return
    end

    local chessboardCell = self.m_chessboardGridCells:Get(self.m_chessboardWidthGridNum)
    self:SetNaviTarget(chessboardCell.view.controllerBtn)
end

PuzzleCtrl._CurCtrlSlotResetToPlaceholder = HL.Method() << function(self)
    if self.m_curControllerSlotLuaIndex < 0 then
        logger.error("[puzzle game] _CurCtrlSlotResetToPlaceholder but no curCtrSlot")
        return
    end


    local puzzleSlot = self.m_puzzleRootCells:Get(self.m_curControllerSlotLuaIndex)
    puzzleSlot:_OnEndDrag(nil, true)


    self.m_curControllerSlotLuaIndex = -1


    self:_ToggleAreaFocusActionId(true, false)
    self:_ToggleChessboardNaviToBlockList(true)

    self.m_cachedReturnGridIndex = -1
end

PuzzleCtrl._FocusBlockList = HL.Method() << function(self)
    local placeholderCell = self.m_placeholderCellCache:Get(1)
    self:SetNaviTarget(placeholderCell.controllerBtn)

    self:_ToggleAreaFocusActionId(false, true)
end

PuzzleCtrl._FocusChessboard = HL.Method() << function(self)
    local gridIndex = self.m_chessboardGridCells:GetCount() // 2 + 1
    local chessboardCell = self.m_chessboardGridCells:Get(gridIndex)
    self:SetNaviTarget(chessboardCell.view.controllerBtn)

    self:_ToggleAreaFocusActionId(true, false)
end

PuzzleCtrl._ToggleAreaFocusActionId = HL.Method(HL.Boolean, HL.Boolean) << function(self, focusBlockListActionOn, focusChessboardActionOn)
    InputManagerInst:ToggleBinding(self.m_focusBlockListActionId, focusBlockListActionOn)
    InputManagerInst:ToggleBinding(self.m_focusChessboardActionId, focusChessboardActionOn)
end

PuzzleCtrl._ToggleChessboardNaviToBlockList = HL.Method(HL.Boolean) << function(self, isOn)
    InputManagerInst:ToggleBinding(self.m_curCtrSlotResetToPlaceholderActionId, not isOn)
    InputManagerInst:ToggleBinding(self.m_curCtrlSlotResetToPreGridActionId, not isOn)

    if isOn then
        UIManager:Show(PanelId.ControllerNaviTarget)
    else
        UIManager:Hide(PanelId.ControllerNaviTarget)
    end
end

PuzzleCtrl._CurCtrlSlotResetToPreGrid = HL.Method() << function(self)
    if self.m_curControllerSlotLuaIndex < 0 then
        return
    end

    if self.m_cachedReturnGridIndex < 0 then
        self:_CurCtrlSlotResetToPlaceholder()
    else

        local luaIndex = self.m_cachedReturnGridIndex
        local cell = self.m_chessboardGridCells:Get(luaIndex)
        local gridData = self.m_chessboardGridData[luaIndex]
        self:PutBlockOnChessboard(self.m_curActionBlockId,
                                  CSIndex(gridData.x),
                                  CSIndex(gridData.y),
                                  cell.transform.position)
        self.m_curActionBlockSlot:_OnEndDrag(nil, false)

        self:_TryHoverPuzzleSlot(cell.rectTransform, self.m_curControllerSlotLuaIndex)

        self.m_curControllerSlotLuaIndex = -1

        self.m_cachedReturnGridIndex = -1

        self:_ToggleAreaFocusActionId(true, false)
        self:_ToggleChessboardNaviToBlockList(true)


    end
end

PuzzleCtrl._ResetControllerState = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.m_curControllerSlotLuaIndex = -1
    self.m_cachedReturnGridIndex = -1

    local placeHolderCell = self.m_placeholderCellCache:Get(1)
    self:SetNaviTarget(placeHolderCell.controllerBtn)

    InputManagerInst:ToggleGroup(self.view.leftNode.groupId, true)
    InputManagerInst:ToggleGroup(self.view.gridInputBindingGroupMonoTarget.groupId, true)
    self:_ToggleAreaFocusActionId(false, true)
    self:_ToggleChessboardNaviToBlockList(true)
end





PuzzleCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    self.m_isHotSwitch = true

    local arg = self.m_info and lume.deepCopy(self.m_info) or {}
    arg.recoverState = {
        conditionStyleRectangle = self.m_conditionStyleRectangle,
        noticeNodeVisible = self.view.noticeNode.gameObject.activeSelf,
        noticeClicked = self.view.noticeNode.m_noticeClicked,
        refAnswerGridVisible = self.view.refAnswerGrid.gameObject.activeSelf,
        curPuzzleUnitComplete = self.m_curPuzzleUnitComplete,
        hasHint = self.m_hasHint,
        isHintUse = self.m_isHintUse,
        useHintTs = self.m_useHintTs,
        puzzleUnitStartTs = self.m_puzzleUnitStartTs,
    }
    return arg
end

PuzzleCtrl._TryRecoverState = HL.Method(HL.Table) << function(self, recoverState)
    if recoverState.conditionStyleRectangle ~= nil then
        self.view.conditionStyleToggle:SetValue(recoverState.conditionStyleRectangle, true)
    end

    if recoverState.noticeNodeVisible then
        self:_ClearNoticeTimer()
        self.view.noticeNode.gameObject:SetActiveIfNecessary(true)
    end

    if recoverState.noticeClicked then
        self.view.noticeNode.m_noticeClicked = true
        self.view.noticeNode:ShowTips()
    end

    if recoverState.refAnswerGridVisible then
        self.view.refAnswerGrid.gameObject:SetActiveIfNecessary(true)
    end

    if recoverState.hasHint ~= nil then
        self.m_hasHint = recoverState.hasHint
    end
    if recoverState.isHintUse ~= nil then
        self.m_isHintUse = recoverState.isHintUse
    end
    if recoverState.useHintTs then
        self.m_useHintTs = recoverState.useHintTs
    end
    if recoverState.puzzleUnitStartTs then
        self.m_puzzleUnitStartTs = recoverState.puzzleUnitStartTs
    end

    self:_RecoverBlockPositions()
    self:RecoverComponentsInteractable()

    if recoverState.curPuzzleUnitComplete then
        if self.m_puzzleGame:IsPuzzleComplete() then
            self.m_succ = true
            self.m_shouldCloseAfterRecover = true
        else
            self.m_puzzleGame:NextPuzzle()
            self:_UpdatePuzzleProgress()
            self:_ResetControllerState()
        end
    end
end

PuzzleCtrl._RecoverBlockPositions = HL.Method() << function(self)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.grid)

    local blocks = self.m_puzzleGame.currentChessboard.blocks

    for luaIndex, data in ipairs(self.m_attachPuzzleData) do
        local succ, block = blocks:TryGetValue(data.id)
        if succ then

            local slot = self.m_puzzleRootCells:Get(luaIndex)

            if block.locationOnChessboard and not block.isIllegalLocate then
                local gridLuaIndex = block.location.y * self.m_chessboardWidthGridNum + block.location.x + 1
                local gridCell = self.m_chessboardGridCells:Get(gridLuaIndex)
                slot:RecoverToChessboard(gridCell.transform)
            elseif block.locationOnChessboard and block.isIllegalLocate then
                local gridLuaIndex = block.location.y * self.m_chessboardWidthGridNum + block.location.x + 1
                local gridCell = self.m_chessboardGridCells:Get(gridLuaIndex)
                slot:RecoverToChessboardAsFloat(gridCell.transform)
                self:SetCurActionBlock(data.id, slot)
                self:SetOtherBlocksFading(data.id, true)
                self:ToggleRotateBtnState(true, true)
            else
                if block.rotateCount % 4 ~= data.rawRotationCount % 4 then
                    slot:RecoverRotation(block.rotateCount)
                end
            end

            if block.rotateCount % 4 ~= data.rawRotationCount % 4 then
                local shadowIndex = self.m_id2BlockShadowIndex[data.id]
                local shadowCell = self.m_puzzleBlockShadowCells:GetItem(shadowIndex)
                shadowCell:RecoverRotation(block.rotateCount)
            end
        end
    end
end

PuzzleCtrl._OnChangeInputDeviceTypeFinished = HL.Method(HL.Any) << function(self)
    if self.m_shouldCloseAfterRecover then
        self.m_shouldCloseAfterRecover = false
        PhaseManager:PopPhase(PhaseId.Puzzle)
    end
end





PuzzleCtrl.m_hasHint = HL.Field(HL.Boolean) << false

PuzzleCtrl.m_isHintUse = HL.Field(HL.Boolean) << false

PuzzleCtrl.m_useHintTs = HL.Field(HL.Number) << 0

PuzzleCtrl.m_puzzleUnitStartTs = HL.Field(HL.Number) << 0

PuzzleCtrl.m_puzzleUnitEndTs = HL.Field(HL.Number) << 0

PuzzleCtrl.m_curPuzzleUnitComplete = HL.Field(HL.Boolean) << false

PuzzleCtrl._EventLogPuzzleUnitEnd = HL.Method() << function(self)
    if not GameWorld.gameMechManager.mainCharFixBrain then
        logger.error("PuzzleCtrl._EventLogPuzzleUnitEnd fail, mainCharFixBrain is nil")
        return
    end

    local curIndex = LuaIndex(self.m_puzzleGame.currentIndex)
    local curChessboard = self.m_puzzleGame.currentChessboard
    local eventLogInfo = GameWorld.gameMechManager.mainCharFixBrain.eventLogInfo
    local hintUseTime = math.max(self.m_useHintTs - self.m_puzzleUnitStartTs, 0)
    local puzzleUnitTIme = math.max(self.m_puzzleUnitEndTs - self.m_puzzleUnitStartTs, 0)
    EventLogManagerInst:GameEvent_PuzzleEnd(tostring(eventLogInfo.lastFixLogicId),
                                            eventLogInfo.lastEntityTemplateId,
                                            eventLogInfo.lastEntityPos,
                                            self.m_succ,
                                            curIndex,
                                            self.m_curPuzzleUnitComplete,
                                            curChessboard.rawData.chessBoardID,
                                            self.m_hasHint,
                                            self.m_isHintUse,
                                            hintUseTime,
                                            puzzleUnitTIme)
end



HL.Commit(PuzzleCtrl)
