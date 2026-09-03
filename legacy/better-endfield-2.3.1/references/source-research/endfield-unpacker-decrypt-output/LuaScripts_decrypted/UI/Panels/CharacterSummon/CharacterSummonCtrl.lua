local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharacterSummon
CharacterSummonCtrl = HL.Class('CharacterSummonCtrl', uiCtrl.UICtrl)

CharacterSummonCtrl.m_headCellFunc = HL.Field(HL.Function)

CharacterSummonCtrl.m_csIndex2HeadCell = HL.Field(HL.Table)

CharacterSummonCtrl.m_charId2HeadCell = HL.Field(HL.Table)

CharacterSummonCtrl.m_sortMode = HL.Field(HL.Number) << 1

CharacterSummonCtrl.m_sortIncremental = HL.Field(HL.Boolean) << true

CharacterSummonCtrl.m_sortKeys = HL.Field(HL.Table)

CharacterSummonCtrl.m_selectedNum = HL.Field(HL.Number) << 0

CharacterSummonCtrl.m_selectedNum2CharId = HL.Field(HL.Table)

CharacterSummonCtrl.m_charId2ChooseState = HL.Field(HL.Table)

CharacterSummonCtrl.m_cacheCharInfos = HL.Field(HL.Table)

CharacterSummonCtrl.m_charId2Infos = HL.Field(HL.Table)

CharacterSummonCtrl.m_allCharInfos = HL.Field(HL.Table)

CharacterSummonCtrl.m_charId2show = HL.Field(HL.Table)

CharacterSummonCtrl.m_allCharInfoReverseMap = HL.Field(HL.Table)

CharacterSummonCtrl.m_selectedTags = HL.Field(HL.Table)

CharacterSummonCtrl.m_initSetTarget = HL.Field(HL.Boolean) << false


local FriendStageSetting = {
    [1] = {minVal = 0, maxVal = 99},
    [2] = {minVal = 100, maxVal = 199},
    [3] = {minVal = 200, maxVal = 9999},
}


local MAX_SELECT_NUM = 3

local SummonBtnState = {
    NormalState = "NormalState",
    DisableState = "DisableState",
}





CharacterSummonCtrl.s_messages = HL.StaticField(HL.Table) << {
}




CharacterSummonCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.view.closeButton.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.CharacterSummon)
    end)

    self.view.summonBtn.onClick:AddListener(function()
        self:SummonChar()
    end)

    self.m_selectedTags = {}
    self:_InitSortNode()
    self:_InitFilterNode()

    self:_InitSelectInfos()

    self.m_csIndex2HeadCell = {}
    self.m_charId2HeadCell = {}
    self.m_headCellFunc = UIUtils.genCachedCellFunction(self.view.headScrollList)

    self.view.headScrollList.onUpdateCell:AddListener(function(gameObject, csIndex)
        self:_UpdateHeadCell(gameObject, LuaIndex(csIndex))
    end)

    self:_InitCharInfos()
    if arg and arg.recoverState then
        self:_TryRecoverState(arg.recoverState)
    end
end

CharacterSummonCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local recoverState = {
        selectedCharIdList = lume.clone(self.m_selectedNum2CharId),
        sortSelectedIndex = self.view.sortNodeUp:GetCurSelectedIndex(),
        sortIsIncremental = self.view.sortNodeUp.isIncremental,
    }
    if self.view.filterBtn.m_args then
        recoverState.filterTags = lume.deepCopy(self.view.filterBtn.m_args.selectedTags)
    end
    return {
        recoverState = recoverState,
    }
end

CharacterSummonCtrl._InitSelectInfos = HL.Method() << function(self)
    self:_SetSelectedNum(0)
    self.m_selectedNum2CharId = {}
    self.m_charId2ChooseState = {}
end

CharacterSummonCtrl._UpdateSelectInfos = HL.Method() << function(self)
    if not self.m_allCharInfos then
        return
    end
    for i, item in ipairs(self.m_allCharInfos) do
        local selectedNum = 0
        local selected = false
        for num, searchCharId in ipairs(self.m_selectedNum2CharId) do
            if searchCharId == item.templateId then
                selected = true
                selectedNum = self.m_charId2ChooseState[item.templateId]
            end
        end
        if selected then
            item.downAllStageSort =  8 - selectedNum
            item.upStageSort = selectedNum - 8
        else
            self:_UpdateAllStageSort(item)
        end
    end
end

CharacterSummonCtrl._SetSelectedNum = HL.Method(HL.Number) << function(self, num)
    self.m_selectedNum = num
    if self.m_selectedNum == 0 then
        self.view.summonBtn.interactable = false
        self.view.summonBtnRoot:SetState(SummonBtnState.DisableState)
    else
        self.view.summonBtn.interactable = true
        self.view.summonBtnRoot:SetState(SummonBtnState.NormalState)
    end
end


CharacterSummonCtrl._InitCharInfos = HL.Method() << function(self)
    self.m_cacheCharInfos = {}
    self.m_charId2Infos = {}
    self.m_allCharInfos = {}

    local spaceship = GameInstance.player.spaceship
    local index = 1

    local charTempDict = {}
    for id, charInfo in pairs(CharInfoUtils.getCharInfoList()) do
        charTempDict[charInfo.templateId] = charInfo
    end

    for id, charInfo in pairs(spaceship.characters) do
        if charTempDict[charInfo.id] ~= nil then
            local isShow = true
            local succ, level = GameUtil.SpaceshipUtils.TryGetSpaceshipLevel()
            if succ then
                isShow = not level:CheckCharIsOccupation(charInfo.id)
            end

            local tempInfo = charTempDict[charInfo.id]
            local item = {
                charInfo = charInfo,
                instId = tempInfo.instId,
                templateId = tempInfo.templateId,
                level = tempInfo.level,
                ownTime = tempInfo.ownTime,
                rarity = tempInfo.rarity,
                slotIndex = tempInfo.slotIndex,
                slotReverseIndex = tempInfo.slotReverseIndex,
                sortOrder = tempInfo.sortOrder,
                isShow = isShow,

            }

            local hasGift = spaceship:GetCharHasGiftToPlayer(item.templateId)
            if hasGift then
                item.hasGift = 1
            else
                item.hasGift = 0
            end

            item.friendLevel = CSPlayerDataUtil.GetFriendshipLevelByChar(item.templateId)
            local favorabilityValue = CSPlayerDataUtil.GetCharFriendship(item.templateId)
            local favorability = CSPlayerDataUtil.GetFriendshipPercent(favorabilityValue) * 100


            item.favorability = favorability
            local stageNum = 0
            for checkLevel, stage in ipairs(FriendStageSetting) do
                if favorability >= stage.minVal then
                    stageNum = checkLevel
                end
            end
            item.friendStageUpSort = stageNum
            item.friendStageDownSort = 1000 - stageNum

            item.friendStageSpecialUpSort = stageNum
            if stageNum == 3 then
                item.friendStageSpecialDownSort = 0
            else
                item.friendStageSpecialDownSort = stageNum
            end
            item.friendValueSort = favorability

            self:_UpdateAllStageSort(item)
            self.m_cacheCharInfos[index] = item
            self.m_charId2Infos[charInfo.id] = item
            index = index + 1
        end
    end

    self.m_sortIncremental = false
    self.m_sortKeys = UIConst.CharacterSummonSortOptions[1].downKeys
    table.sort(self.m_cacheCharInfos, Utils.genSortFunction(self.m_sortKeys, self.m_sortIncremental))

    self.m_allCharInfos = self.m_cacheCharInfos
    self:_UpdateCharShowList()
    self.m_csIndex2HeadCell = {}
    self.m_charId2HeadCell = {}
    if #self.m_allCharInfos > 0 then
        self.view.rightNodeController:SetState("NormalState")
    else
        self.view.rightNodeController:SetState("EmptyState")
    end
    self.view.headScrollList:UpdateCount(#self.m_allCharInfos)
end

CharacterSummonCtrl._UpdateCharShowList = HL.Method() << function(self)
    self.m_allCharInfoReverseMap = {}
    self.m_charId2show = {}

    for i, charInfo in ipairs(self.m_allCharInfos) do
        if charInfo.isShow then
            self.m_charId2show[charInfo.templateId] = true
        else
            self.m_charId2show[charInfo.templateId] = false
        end
    end


    for i, charInfo in ipairs(self.m_allCharInfos) do
        self.m_allCharInfoReverseMap[charInfo.templateId] = i
    end
end

CharacterSummonCtrl._UpdateAllStageSort = HL.Method(HL.Table) << function(self, item)
    local downAllStageSort = 2
    local upStageSort = 2
    if item.hasGift > 0 then
        downAllStageSort = 3
        upStageSort = 1
    end
    if not item.isShow then
        downAllStageSort = 1
        upStageSort = 3
    end
    item.downAllStageSort = downAllStageSort
    item.upStageSort = upStageSort
end

CharacterSummonCtrl._InitSortNode = HL.Method() << function(self)
    self.view.sortNodeUp:InitSortNode(UIConst.CharacterSummonSortOptions, function(optData, isIncremental)
        self:_ApplySort(optData, isIncremental)
    end, nil, false)
end

CharacterSummonCtrl._ApplySort = HL.Method(HL.Table, HL.Boolean) << function(self, optData, isIncremental)
    self:_UpdateSelectInfos()
    self.m_sortIncremental = isIncremental
    if isIncremental then
        self.m_sortKeys = optData.upKeys
    else
        self.m_sortKeys = optData.downKeys
    end

    if self.m_allCharInfos then
        table.sort(self.m_allCharInfos, Utils.genSortFunction(self.m_sortKeys, isIncremental))
    end

    if self.m_allCharInfos then
        self.m_csIndex2HeadCell = {}
        self.m_charId2HeadCell = {}
        self:_UpdateCharShowList()
        if #self.m_allCharInfos > 0 then
            self.view.rightNodeController:SetState("NormalState")
        else
            self.view.rightNodeController:SetState("EmptyState")
        end
        self.view.headScrollList:UpdateCount(#self.m_allCharInfos)
    end
end

CharacterSummonCtrl._ApplyFilter = HL.Method(HL.Table) << function(self, selectedTags)
    local itemInfoList = self.m_cacheCharInfos
    local filteredList = {}
    local tempDict = {}
    for _, itemInfo in pairs(itemInfoList) do
        if FilterUtils.checkIfPassFilter(itemInfo, selectedTags) then
            table.insert(filteredList, itemInfo)
            tempDict[itemInfo.templateId] = true
        end
    end

    for num, searchCharId in ipairs(self.m_selectedNum2CharId) do
        if tempDict[searchCharId] ~= true then
            for i, itemInfo in ipairs(self.m_cacheCharInfos) do
                if searchCharId == itemInfo.templateId then
                    table.insert(filteredList, itemInfo)
                    break
                end
            end
        end
    end

    self.m_allCharInfos = filteredList
end

CharacterSummonCtrl._InitFilterNode = HL.Method() << function(self)
    local filterArgs = {
        tagGroups = FilterUtils.generateConfigCharSummon(),

        onConfirm = function(tags)
            self:_OnFilterConfirm(tags)
        end,
        getResultCount = function(tags)
            return self:_FilterBtnGetResCount(tags)
        end,
        sortNodeWidget = self.view.sortNodeUp,
    }
    self.view.filterBtn:InitFilterBtn(filterArgs)
end

CharacterSummonCtrl._OnFilterConfirm = HL.Method(HL.Any) << function(self, tags)
    self.m_selectedTags = tags or {}
    self:_ApplyFilter(self.m_selectedTags)
    self:_ApplySort(self.view.sortNodeUp:GetCurSortData(), self.view.sortNodeUp.isIncremental)
end

CharacterSummonCtrl._FilterBtnGetResCount = HL.Method(HL.Table).Return(HL.Number) << function(self, tags)
    local resultCount = 0
    for _, itemInfo in pairs(self.m_cacheCharInfos) do
        if FilterUtils.checkIfPassFilter(itemInfo, tags) then
            resultCount = resultCount + 1
        end
    end
    return resultCount
end

CharacterSummonCtrl._TryRecoverState = HL.Method(HL.Table) << function(self, recoverState)
    local validSelectedCharIdList = {}
    if recoverState.selectedCharIdList then
        for _, charId in ipairs(recoverState.selectedCharIdList) do
            if self.m_charId2Infos[charId] then
                table.insert(validSelectedCharIdList, charId)
            end
        end
    end
    self.m_selectedNum2CharId = {}
    self.m_charId2ChooseState = {}
    for index, charId in ipairs(validSelectedCharIdList) do
        self.m_selectedNum2CharId[index] = charId
        self.m_charId2ChooseState[charId] = index
    end
    self:_SetSelectedNum(#validSelectedCharIdList)

    local sortNode = self.view.sortNodeUp
    local sortSelectedIndex = recoverState.sortSelectedIndex
    if sortSelectedIndex and sortNode and sortNode.m_sortOptions and sortNode.view and sortNode.view.mobilePCNode and sortNode.view.mobilePCNode.dropDown then
        local optionCount = #sortNode.m_sortOptions
        if optionCount > 0 then
            sortSelectedIndex = math.max(1, math.min(sortSelectedIndex, optionCount))
            sortNode.isIncremental = recoverState.sortIsIncremental == true
            sortNode:RefreshIncremental()
            sortNode.view.mobilePCNode.dropDown:SetSelected(CSIndex(sortSelectedIndex), true, false)
        end
    end

    local filterTags = recoverState.filterTags and lume.deepCopy(recoverState.filterTags) or {}
    self.m_selectedTags = filterTags
    if self.view.filterBtn.m_args then
        self.view.filterBtn.m_args.selectedTags = lume.deepCopy(filterTags)
    end
    if self.view.filterBtn.m_args and self.view.filterBtn.m_args.onConfirm then
        self.view.filterBtn.m_args.onConfirm(filterTags)
    else
        sortNode:OnSortChanged()
    end
    sortNode:UpdateDeviceState()
end

CharacterSummonCtrl._UpdateHeadCell = HL.Method(GameObject, HL.Number) << function(self, gameObject, luaIndex)
    local cell = self.m_headCellFunc(gameObject)
    self.m_csIndex2HeadCell[CSIndex(luaIndex)] = cell

    local charId = self.m_allCharInfos[luaIndex].templateId
    self.m_charId2HeadCell[charId] = cell

    cell.ssCharHeadCell:InitSSCharHeadCell({
        charId = charId,
        disableFunc = function()
            return self:_CheckCharDisableByCharIndex(luaIndex)
        end,
        onClick = function()
            self:_OnClickCell(charId)
        end,
        showGiftInfo = self.m_allCharInfos[luaIndex].hasGift,
        targetRoomId = "",
        hideStaminaNode = true,
    })

    if self.m_charId2ChooseState[charId] ~= nil then
        local chooseState = self.m_charId2ChooseState[charId]
        cell.ssCharHeadCell:SetChooseState(chooseState)
    else
        cell.ssCharHeadCell:SetChooseState(false)
    end

    if self.m_charId2ChooseState[charId] == nil or self.m_charId2ChooseState[charId] == false then
        cell.ssCharHeadCell.view.controllerTip.text = Language.LUA_SPACESHIP_SUMMON_FOCUS_NO_SELECT_TIP
    else
        cell.ssCharHeadCell.view.controllerTip.text = Language.LUA_SPACESHIP_SUMMON_FOCUS_SELECT_TIP
    end

    if luaIndex == 1 and not self.m_initSetTarget then
        self.m_initSetTarget = true
        self:SetNaviTarget(cell.ssCharHeadCell.view.groupNaviDecorator)
    end
end

CharacterSummonCtrl._CheckCharDisableByCharIndex = HL.Method(HL.Number).Return(HL.Boolean) << function(self, luaIndex)
    local charId = self.m_allCharInfos[luaIndex].templateId
    if self.m_charId2show[charId] ~= nil then
        return not self.m_charId2show[charId]
    end
    return true
end

CharacterSummonCtrl._OnClickCell = HL.Method(HL.String) << function(self, charId)
    if not self.m_charId2show[charId] then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SPACESHIP_SUMMON_FORBID_TOAST)
        return
    end

    local selected = false
    for num, searchCharId in ipairs(self.m_selectedNum2CharId) do
        if searchCharId == charId then
            selected = true
        end
    end
    if selected then
        self:_DeleteSelect(charId)
    else
        self:_AddSelect(charId)
    end

    local cell = self.m_charId2HeadCell[charId]
    if cell ~= nil then
        if self.m_charId2ChooseState[charId] == nil or self.m_charId2ChooseState[charId] == false then
            cell.ssCharHeadCell.view.controllerTip.text = Language.LUA_SPACESHIP_SUMMON_FOCUS_NO_SELECT_TIP
        else
            cell.ssCharHeadCell.view.controllerTip.text = Language.LUA_SPACESHIP_SUMMON_FOCUS_SELECT_TIP
        end
    end
    Notify(MessageConst.REFRESH_CONTROLLER_HINT)
end

CharacterSummonCtrl._AddSelect = HL.Method(HL.String) << function(self, charId)
    if self.m_selectedNum == MAX_SELECT_NUM then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SPACESHIP_SUMMON_MAX_SELECT_TOAST)
        return
    end
    self:_SetSelectedNum(self.m_selectedNum + 1)
    local cell = self.m_charId2HeadCell[charId]
    if cell then
        cell.ssCharHeadCell:SetChooseState(self.m_selectedNum)
        self.m_charId2ChooseState[charId] = self.m_selectedNum
    end

    self.m_selectedNum2CharId[self.m_selectedNum] = charId

end


CharacterSummonCtrl.ShowCharacterSummon = HL.StaticMethod(HL.Opt(HL.Table)) << function(args)
    PhaseManager:OpenPhase(PhaseId.CharacterSummon)
end


CharacterSummonCtrl._DeleteSelect = HL.Method(HL.String) << function(self, charId)
    if self.m_selectedNum == 0 then
        return
    end
    local cell = self.m_charId2HeadCell[charId]
    if cell then
        cell.ssCharHeadCell:SetChooseState(false)
        self.m_charId2ChooseState[charId] = false
    end

    local cancelIndex = -1
    for num, searchCharId in ipairs(self.m_selectedNum2CharId) do
        if searchCharId == charId then
            cancelIndex = num
        end
    end

    if cancelIndex ~= -1 then
        for i = cancelIndex, self.m_selectedNum - 1 do
            self.m_selectedNum2CharId[i] = self.m_selectedNum2CharId[i + 1]
            local searchCharId = self.m_selectedNum2CharId[i]
            local tempCell = self.m_charId2HeadCell[searchCharId]
            if tempCell then
                tempCell.ssCharHeadCell:SetChooseState(i)
                self.m_charId2ChooseState[searchCharId] = i
            end
        end
    end

    table.remove(self.m_selectedNum2CharId, self.m_selectedNum)
    self:_SetSelectedNum(self.m_selectedNum - 1)
end

CharacterSummonCtrl.SummonChar = HL.Method() << function(self)
    if self.m_selectedNum == 0 then
        return
    end

    local gameEventLog = {}

    local fadeIn = UIConst.SPACESHIP_SUMMON_MASK_FADE_IN
    local fadeOut = UIConst.SPACESHIP_SUMMON_MASK_FADE_OUT
    local fadeWait = UIConst.SPACESHIP_SUMMON_MASK_FADE_WAIT
    local dynamicMaskData = UIUtils.genDynamicBlackScreenMaskDataWithWaitTime("SummonChar", fadeIn, fadeOut, fadeWait, function()
        local succ, level = GameUtil.SpaceshipUtils.TryGetSpaceshipLevel()
        if succ then
            level:ClearCurSummonCharInfo()
            for id, charId in pairs(self.m_selectedNum2CharId) do
                if charId ~= nil then
                    level:AddSummonChar(charId)
                    if self.m_charId2Infos[charId] ~= nil then
                        gameEventLog[charId] = self.m_charId2Infos[charId].favorability
                    end
                end
            end
            level:SummonChar()
            EventLogManagerInst:GameEvent_operator_contact(gameEventLog)
        end
        PhaseManager:PopPhase(PhaseId.CharacterSummon)
    end)
    GameAction.ShowBlackScreen(dynamicMaskData)
end

HL.Commit(CharacterSummonCtrl)
