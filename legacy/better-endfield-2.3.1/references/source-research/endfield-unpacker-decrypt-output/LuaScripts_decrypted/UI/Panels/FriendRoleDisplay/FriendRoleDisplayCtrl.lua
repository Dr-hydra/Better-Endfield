local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FriendRoleDisplay


local charCount = 4

FriendRoleDisplayCtrl = HL.Class('FriendRoleDisplayCtrl', uiCtrl.UICtrl)

FriendRoleDisplayCtrl.m_genDisplayCells = HL.Field(HL.Forward("UIListCache"))

FriendRoleDisplayCtrl.m_charInfo = HL.Field(HL.Table)

FriendRoleDisplayCtrl.m_index = HL.Field(HL.Number) << 1

FriendRoleDisplayCtrl.m_arg = HL.Field(HL.Table)

FriendRoleDisplayCtrl.m_selectCharInsIdList = HL.Field(HL.Table)

FriendRoleDisplayCtrl.m_isClosing = HL.Field(HL.Boolean) << false





FriendRoleDisplayCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_FRIEND_BUSINESS_INFO_CHANGE] = 'OnChange',
}

FriendRoleDisplayCtrl.OnChange = HL.Method() << function(self)
    self:_PlayAnimationOutAndCloseOnce()
end


FriendRoleDisplayCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg or {}
    self.m_isClosing = false

    self.view.btnBack.onClick:RemoveAllListeners()
    self.view.btnBack.onClick:AddListener(function()
        self:_PlayAnimationOutAndCloseOnce()
    end)

    self.view.applyBtn.onClick:RemoveAllListeners()
    self.view.applyBtn.onClick:AddListener(function()
        local array = {}
        for _,insId in ipairs(self.m_selectCharInsIdList) do
            table.insert(array, insId.instId)
        end
        GameInstance.player.friendSystem:DisplayCharModify(array)
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })


    local info = {
        selectNum = charCount,
    }

    self.m_selectCharInsIdList = {}
    local recoverArg = self.m_arg.roleDisplayState or {}
    self.view.charList:InitCharFormationList(info, nil , true)
    self.view.charList:SetUpdateCellFunc(nil, function(select, cellIndex, charItem, charItemList, charInfoList)
        self:_CharListChangeSelectIndex(select, cellIndex, charItem, charItemList, charInfoList)
    end)

    if recoverArg.selectCharInstIds and #recoverArg.selectCharInstIds > 0 then
        for _, instId in ipairs(recoverArg.selectCharInstIds) do
            table.insert(self.m_selectCharInsIdList, { instId = instId })
        end
    else
        for i = 0, GameInstance.player.friendSystem.SelfInfo.charInfos.Count - 1 do
            local charInfo = GameInstance.player.friendSystem.SelfInfo.charInfos[i]
            table.insert(self.m_selectCharInsIdList, { instId = charInfo.instId })
        end
    end
    self.view.charList:UpdateCharItems(CharInfoUtils.getAllCharInfoList())
    self:_ApplyRoleDisplaySortFilterState(recoverArg)
    self.view.charList:ShowSelectChars(self.m_selectCharInsIdList)

    self.m_genDisplayCells = UIUtils.genCellCache(self.view.charHeadCell)
    self.m_genDisplayCells:Refresh(charCount, function(cell, luaIndex)
        self:_RefreshDisplayCells(cell, luaIndex)
    end)
end

FriendRoleDisplayCtrl._PlayAnimationOutAndCloseOnce = HL.Method() << function(self)
    if self.m_isClosing then
        return
    end

    self.m_isClosing = true
    local isOpen, commonFilterCtrl = UIManager:IsOpen(PanelId.CommonFilter)
    if isOpen and commonFilterCtrl then
        commonFilterCtrl:_CloseSelf()
    end
    self:PlayAnimationOutAndClose()
    Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
end

FriendRoleDisplayCtrl._ApplyRoleDisplaySortFilterState = HL.Method(HL.Table) << function(self, recoverArg)
    local charListView = self.view and self.view.charList and self.view.charList.view or nil
    local sortNode = charListView and charListView.sortNode or nil
    if sortNode == nil then
        return
    end

    local sortState = recoverArg and recoverArg.sortState
    if sortState and sortNode.m_sortOptions and sortNode.view and sortNode.view.mobilePCNode and sortNode.view.mobilePCNode.dropDown then
        local optionCount = #sortNode.m_sortOptions
        if optionCount > 0 then
            local optionIndex = math.max(1, math.min(sortState.selectedIndex or 1, optionCount))
            sortNode.isIncremental = sortState.isIncremental == true
            sortNode:RefreshIncremental()
            sortNode.view.mobilePCNode.dropDown:SetSelected(CSIndex(optionIndex), true, false)
        end
    end

    local filterTags = recoverArg and recoverArg.filterState and lume.deepCopy(recoverArg.filterState) or {}
    if sortNode.m_filterBtn and sortNode.m_filterBtn.m_args then
        sortNode.m_filterBtn.m_args.selectedTags = lume.deepCopy(filterTags)
        if sortNode.m_filterBtn.m_args.onConfirm then
            sortNode.m_filterBtn.m_args.onConfirm(filterTags)
        else
            sortNode:OnSortChanged()
        end
    else
        sortNode:OnSortChanged()
    end
    sortNode:UpdateDeviceState()
end

FriendRoleDisplayCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.m_arg and lume.deepCopy(self.m_arg) or {}
    local selectCharInstIds = {}
    for _, info in ipairs(self.m_selectCharInsIdList or {}) do
        table.insert(selectCharInstIds, info.instId)
    end
    local sortNode = self.view and self.view.charList and self.view.charList.view and self.view.charList.view.sortNode
    local roleDisplayState = {
        selectCharInstIds = selectCharInstIds,
    }
    if sortNode then
        roleDisplayState.sortState = {
            selectedIndex = sortNode:GetCurSelectedIndex(),
            isIncremental = sortNode.isIncremental,
        }
        if sortNode.m_filterBtn and sortNode.m_filterBtn.m_args then
            roleDisplayState.filterState = lume.deepCopy(sortNode.m_filterBtn.m_args.selectedTags)
        end
    end
    arg.roleDisplayState = roleDisplayState
    return arg
end

FriendRoleDisplayCtrl._RefreshDisplayCells = HL.Method(HL.Table, HL.Number) << function(self, cell, luaIndex)
    cell.roleState:SetState(luaIndex <= #self.m_selectCharInsIdList and 'role' or 'add')

    cell.roleNunTxt.text = string.format('%02d', luaIndex)
    if luaIndex <= #self.m_selectCharInsIdList then
        local instId = self.m_selectCharInsIdList[luaIndex].instId
        local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(instId)
        local charData = CharInfoUtils.getCharTableData(charInfo.templateId)
        local item = {
            instId = instId,
            templateId = charInfo.templateId,
            level = charInfo.level,
            ownTime = charInfo.ownTime,
            rarity = charData.rarity,
            slotIndex = Const.BATTLE_SQUAD_MAX_CHAR_NUM + 1,
            slotReverseIndex = -1,
        }
        cell.charHeadCell:InitCharFormationHeadCell(item, nil, true)
    end
end

FriendRoleDisplayCtrl._CharListChangeSelectIndex = HL.Method(HL.Boolean, HL.Number, HL.Table, HL.Table, HL.Table) << function(self, select, cellIndex, charItem, charItemList, charInfoList)
    self.m_selectCharInsIdList = {}
    for index, item in ipairs(charItemList) do
        table.insert(self.m_selectCharInsIdList, { instId = item.instId })
    end
    self.m_genDisplayCells:Refresh(charCount, function(cell, luaIndex)
        self:_RefreshDisplayCells(cell, luaIndex)
    end)
end











HL.Commit(FriendRoleDisplayCtrl)
