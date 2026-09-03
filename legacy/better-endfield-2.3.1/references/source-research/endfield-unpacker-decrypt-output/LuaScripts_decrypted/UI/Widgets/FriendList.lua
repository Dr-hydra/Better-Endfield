local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')




FriendList = HL.Class('FriendList', UIWidgetBase)

FriendList.m_friendCellList = HL.Field(HL.Table)

FriendList.m_friendList = HL.Field(HL.Table)

FriendList.m_sortOptions = HL.Field(HL.Table)

FriendList.GetCell = HL.Field(HL.Function)

FriendList.m_searchKey = HL.Field(HL.String) << ""

FriendList.SearchSort = HL.Field(HL.Function)

FriendList.UpdateFunc = HL.Field(HL.Function)

FriendList.SearchFunc = HL.Field(HL.Function)

FriendList.m_isPsnTab = HL.Field(HL.Boolean) << false

FriendList.m_arg = HL.Field(HL.Table)

FriendList.m_endIndex = HL.Field(HL.Number) << -1

FriendList.m_needLoadingItem = HL.Field(HL.Boolean) << false

FriendList.m_clueGiftNaviModel = HL.Field(HL.Boolean) << false

FriendList.m_clueGiftBindCache = HL.Field(HL.Table)

FriendList.m_blueprintID = HL.Field(HL.Any)

FriendList.m_noInfoTip = HL.Field(HL.Any)

FriendList.m_rawIdList = HL.Field(HL.Table)


FriendList.InitFriendListCtrl = HL.Method(HL.Any) << function(self, arg)
    self.m_arg = arg;

    if self.m_arg ~= nil and self.m_arg.clueGiftNaviModel == true then
        self.m_clueGiftNaviModel = true
        self.m_clueGiftBindCache = {}
    end

    if self.m_isPsnTab then
        self.view.operationTabAnim:Play("common_sonytabtoggle_to_right")
        self.view.psnFriendTab.isOn = true
    else
        self.view.operationTabAnim:Play("common_sonytabtoggle_to_left")
        self.view.gameFriendTab.isOn = true
    end

    self.view.sonyTabNode.gameObject:SetActiveIfNecessary(FriendUtils.isPsnPlatform() and self.m_arg.onSonyTabChange ~= nil)
    self.UpdateFunc = function(index, forceLoading)
        local cell = self:_GetCellByIndex(index)

        if self.m_arg.stateName then
            cell.view.stateController:SetState(self.m_arg.stateName)
        end

        if self.m_needLoadingItem and (forceLoading or (index == self.view.scrollList.count and index > self.m_endIndex)) then
            cell:SetLoadingState()
            return
        end

        if index <= #self.m_friendList then
            cell.view.gameObject:SetActiveIfNecessary(true)
            if self.m_isPsnTab then
                local id = self.m_friendList[index].accountId
                cell:RefreshFriendListCellByPsnId(id, arg, self.m_searchKey)
            else
                local id = self.m_friendList[index].roleId
                cell:RefreshFriendListCell(id, arg, self.m_searchKey)
            end
        else
            cell.view.gameObject:SetActiveIfNecessary(false)
        end

        cell.view.shareBtn.onClick:RemoveAllListeners()
        cell.view.shareBtn.onClick:AddListener(function()
            if self.m_arg.onShareClick then
                self.m_arg.onShareClick(self.m_friendList[index].roleId)
            end
        end)

        if self.m_clueGiftNaviModel then
            local groupId = cell.view.friendListCellGroupMonoTarget.groupId
            local alreadyBindingId = self.m_clueGiftBindCache[groupId]
            if alreadyBindingId ~= nil then
                InputManagerInst:DeleteBinding(alreadyBindingId)
                self.m_clueGiftBindCache[groupId] = nil
            end
            self.m_clueGiftBindCache[groupId] = InputManagerInst:CreateBindingByActionId("spaceship_clue_send_friend_list_unfocus", function()
                self:clueGiftExitNavi()
            end, groupId)
            self.m_clueGiftBindCache[groupId] = InputManagerInst:CreateBindingByActionId("friend_chat_send_area_use_arrow_jump_out", function()
                self:clueGiftExitNavi()
            end, groupId)
        end
    end
    if arg.onSearchChange then
        self.SearchFunc = arg.onSearchChange
    end

    self:_FirstTimeInit()
end

FriendList.clueGiftExitNavi = HL.Method() << function(self)
    self.view.scrollListSelectableNaviGroup:ManuallyStopFocus()
    self.view.selectableNaviGroup:ManuallyStopFocus()
    if self.m_arg ~= nil and self.m_arg.clueGiftNaviFun ~= nil then
        self.m_arg.clueGiftNaviFun()
    end
end



FriendList._OnFirstTimeInit = HL.Override() << function(self)
    self.view.operationTabAnim:Play("common_sonytabtoggle_to_left")
    if FriendUtils.isPsnPlatform() and self.m_arg.onSonyTabChange then
        self.view.gameFriendTab.onValueChanged:RemoveAllListeners()
        self.view.gameFriendTab.onValueChanged:AddListener(function(isOn)
            if isOn and self.m_isPsnTab == true then
                self.m_isPsnTab = false
                self.m_arg.onSonyTabChange(false)
                self.view.inputNode.gameObject:SetActiveIfNecessary(true)
                self.view.operationTabAnim:Play("common_sonytabtoggle_to_left")
            end
        end)
        self.view.psnFriendTab.onValueChanged:RemoveAllListeners()
        self.view.psnFriendTab.onValueChanged:AddListener(function(isOn)
            if isOn and self.m_isPsnTab == false then
                self.m_isPsnTab = true
                self.m_arg.onSonyTabChange(true)
                self.view.inputNode.gameObject:SetActiveIfNecessary(false)
                self.view.operationTabAnim:Play("common_sonytabtoggle_to_right")
            end
        end)
    end

    self.SearchSort = function(info)
        if self.m_searchKey and info.name then
            if self.view.sortNode.isIncremental then
                return string.find(info.name, self.m_searchKey)
            else
                local value = string.find(info.name, self.m_searchKey)
                if value == nil then
                    return nil
                end
                return -value
            end
        else
            return 0
        end
    end

    if self.view.sortNode then
        self.m_sortOptions = self.m_arg.sortOptions

        self.view.sortNode:InitSortNode(self.m_sortOptions, function(optData, isIncremental)
            self:_OnSortChanged(optData, isIncremental, self.m_needLoadingItem, self.m_noInfoTip)
        end, nil, false)
    end

    if self.view.inputField then
        self.view.inputField.onValueChanged:AddListener(function(str)
            self:OnChangeInputField(str)
        end)
    end

    self.GetCell = UIUtils.genCachedCellFunction(self.view.scrollList)

    self.view.scrollList.onUpdateCell:RemoveAllListeners()
    self.view.scrollList.onUpdateCell:AddListener(function(object, index)
        object.name = "FriendListCell_" .. index
        self:_OnUpdateCell(object, LuaIndex(index))
    end)
end

FriendList.RefreshInfo = HL.Method(HL.Table, HL.Opt(HL.Boolean, HL.String, HL.Boolean, HL.Boolean)) << function(self, info, needLoadingItem, noInfoTip, loading, needUpdateSearchCache)
    UIManager:Close(PanelId.NaviTargetActionMenu)
    if loading == true and (info == nil or #info == 0) then
        self.view.scrollList:UpdateCount(0)
        self.view.loading.gameObject:SetActiveIfNecessary(true)
        self.view.noUsersText.transform.parent.gameObject:SetActiveIfNecessary(false)
        return
    end
    self.view.loading.gameObject:SetActiveIfNecessary(false)


    if self.m_friendList == nil or self.m_friendList ~= info then
        self.m_friendList = info
        if self.view.sortNode.gameObject.activeInHierarchy then
            if not self.m_arg.customSortFun then
                self:_OnSortChanged(self.view.sortNode:GetCurSortData(), self.view.sortNode.isIncremental, needLoadingItem, noInfoTip)
            end
            return
        end
    else
        self.m_friendList = info
    end
    self.m_needLoadingItem = needLoadingItem == true
    self.m_noInfoTip = noInfoTip

    if self.m_arg.isFilter and needUpdateSearchCache ~= false then
        self.m_rawIdList = {}
        for i = 1, #self.m_friendList do
            table.insert(self.m_rawIdList, self.m_friendList[i].roleId)
        end
    end

    if #self.m_friendList == 0 then
        self.view.scrollList:UpdateCount(0)
        self.view.noUsersText.transform.parent.gameObject:SetActiveIfNecessary(true)
        if noInfoTip == nil then
            noInfoTip = Language.LUA_FRIEND_NO_FRIEND
        end
        self.view.noUsersText.text = noInfoTip
        return
    end

    self.view.noUsersText.transform.parent.gameObject:SetActiveIfNecessary(false)


    local ids, endIndex = self:_GetNextPageNotInitIds(1)
    self.m_endIndex = endIndex
    if needLoadingItem and endIndex + 1 <= #self.m_friendList then
        endIndex = endIndex + 1
    end


    if #ids > 0 then
        if self.m_isPsnTab then
            GameInstance.player.friendSystem:SyncFriendInfoForPsn(self.m_arg.infoDicIndex, ids, function(delArray)
                if IsNull(self.view.gameObject) then
                    return
                end
                local del = 0
                if delArray ~= nil then
                    del = delArray.Count
                    for _, roleId in cs_pairs(delArray) do
                        for i = 1, #self.m_friendList do
                            if self.m_friendList[i].roleId == roleId then
                                table.remove(self.m_friendList, i)
                                break
                            end
                        end
                    end
                end
                self.view.scrollList:UpdateCount(endIndex - del, true)


                if self:_CanRecordNaviTarget() then
                    self:NaviToFirstCell()
                end
            end)
        else
            GameInstance.player.friendSystem:SyncFriendInfo(self.m_arg.infoDicIndex, ids, function(delArray)
                if IsNull(self.view.gameObject) then
                    return
                end
                local del = 0
                if delArray ~= nil then
                    del = delArray.Count
                    for _, roleId in cs_pairs(delArray) do
                        for i = 1, #self.m_friendList do
                            if self.m_friendList[i].roleId == roleId then
                                table.remove(self.m_friendList, i)
                                break
                            end
                        end
                    end
                end
                self.view.scrollList:UpdateCount(endIndex - del, true)


                if self:_CanRecordNaviTarget() then
                    self:NaviToFirstCell()
                end
            end)
        end
    else
        self.view.scrollList:UpdateCount(endIndex, true)
        if self:_CanRecordNaviTarget() then
            self:NaviToFirstCell()
        end
    end
end

FriendList.RefreshInfoStayPos = HL.Method(HL.Table) << function(self, info)
    UIManager:Close(PanelId.NaviTargetActionMenu)
    self.view.loading.gameObject:SetActiveIfNecessary(false)

    local wasEmpty = (self.m_friendList == nil or #self.m_friendList == 0) and (#info > 0)

    local needNaviToLast = false
    if DeviceInfo.usingController and self.m_friendList ~= nil then
        local lastCell = self.view.scrollList:Get(#self.m_friendList - 1)
        if lastCell and InputManagerInst.controllerNaviManager.curTarget == lastCell:GetComponent("InputBindingGroupNaviDecorator") then
            needNaviToLast = true
        end
    end
    if self.m_friendList == nil or self.m_friendList ~= info then
        self.m_friendList = info
        if self.view.sortNode.gameObject.activeInHierarchy then
            if self.m_arg.customSortFun then
                if self.m_friendList then
                    self.m_friendList = self.m_arg.customSortFun(self.m_friendList)
                end
            else
                table.sort(self.m_friendList, Utils.genSortFunctionWithIgnore(self.view.sortNode:GetCurSortData().keys, self.view.sortNode.isIncremental, { "searchSort" }))
            end
        end
    else
        self.m_friendList = info
    end

    if #self.m_friendList == 0 then
        self.view.scrollList:UpdateCount(0)
        self.view.noUsersText.transform.parent.gameObject:SetActiveIfNecessary(true)
        local noInfoTip = self.m_noInfoTip
        if noInfoTip == nil then
            noInfoTip = Language.LUA_FRIEND_NO_FRIEND
        end
        self.view.noUsersText.text = noInfoTip
        return
    end
    self.view.noUsersText.transform.parent.gameObject:SetActiveIfNecessary(false)

    self.view.scrollList:UpdateCount(#self.m_friendList, false, false, false, true)
    if self:_CanRecordNaviTarget() then
        if wasEmpty or InputManagerInst.controllerNaviManager.curTarget == nil then
            self:NaviToFirstCell()
        elseif needNaviToLast then
            local lastCell = self.view.scrollList:Get(CSIndex(#self.m_friendList))
            if IsNull(lastCell) then
                return
            end

            if not self.m_clueGiftNaviModel then
                self:_SetCellAsNaviTarget(lastCell)
            end
        end
    end
end

FriendList.NaviToFirstCell = HL.Method() << function(self)
    local go = self.view.scrollList:Get(0)
    self:_SetCellAsNaviTarget(go)
end

FriendList._CanRecordNaviTarget = HL.Method().Return(HL.Boolean) << function(self)
    if not DeviceInfo.usingController then
        return false
    end
    local ctrl = self:GetUICtrl()
    if ctrl == nil or ctrl.view == nil or IsNull(ctrl.view.gameObject) or not ctrl.view.gameObject.activeInHierarchy then
        return false
    end
    if ctrl.view.inputGroup.groupEnabled then
        return true
    end
    return ctrl.m_phase ~= nil
end

FriendList._SetCellAsNaviTarget = HL.Method(HL.Opt(HL.Userdata)) << function(self, go)
    if IsNull(go) then
        return
    end

    if not self.m_clueGiftNaviModel then
        local target = go:GetComponent("InputBindingGroupNaviDecorator")
        if IsNull(target) then
            return
        end

        local ctrl = self:GetUICtrl()
        local isForeground = ctrl ~= nil and ctrl.view ~= nil
            and ctrl.view.inputGroup ~= nil and ctrl.view.inputGroup.groupEnabled
        if ctrl ~= nil then
            ctrl:SetNaviTarget(target)
        else
            self:SetNaviTarget(target)
        end
    end
end

FriendList.OnChangeInputField = HL.Method(HL.String) << function(self, str)
    if self.SearchFunc then
        self.SearchFunc(str)
    end
    if self.m_arg.isFilter then
        self.m_searchKey = str
        self:_OnInputFieldChange(str)
    end
end

FriendList._GetNextPageNotInitIds = HL.Method(HL.Number).Return(HL.Table, HL.Number) << function(self, startLuaIndex)
    local ids = {}
    ids["isPsnTab"] = self.m_isPsnTab
    if startLuaIndex > #self.m_friendList then
        return ids, 0
    end
    local endIndex = startLuaIndex + self.m_arg.maxLen - 1
    for i = startLuaIndex, endIndex do
        if i <= #self.m_friendList then
            local success = false
            local info = nil
            if self.m_isPsnTab then
                success, info = GameInstance.player.friendSystem:GetPsnDictByIndex(self.m_arg.infoDicIndex):TryGetValue(self.m_friendList[i].accountId)
            else
                success, info = GameInstance.player.friendSystem:GetDictInfo(self.m_arg.infoDicIndex):TryGetValue(self.m_friendList[i].roleId)
            end
            if not success then

                if GameInstance.player.spaceship.isViewingFriend then
                    goto continue
                end
                logger.error(CS.Beyond.ELogChannel.Friend, "未找到好友数据 " .. self.m_friendList[i].roleId .. "isPsnTab:" .. tostring(self.m_isPsnTab))
                return ids, i
            end

            if self.m_isPsnTab then
                if not info.psInit then
                    table.insert(ids, self.m_friendList[i].accountId)
                end
            else
                if not info.init then
                    table.insert(ids, self.m_friendList[i].roleId)
                end
            end
            :: continue ::
        else
            return ids, i - 1
        end
    end
    return ids, endIndex
end

FriendList._SearchSort = HL.Method(HL.Table).Return(HL.Number) << function(self, info)
    return string.find(info.name, self.m_searchKey)
end

FriendList.GetClueGiftNaviToFirstCell = HL.Method().Return(HL.Any) << function(self)
    local go = self.view.scrollList:Get(0)
    if IsNull(go) then
        return nil
    end

    return go:GetComponent("InputBindingGroupNaviDecorator")
end


FriendList._GetCellByIndex = HL.Method(HL.Number).Return(HL.Forward("FriendListCell")) << function(self, cellIndex)
    local go = self.view.scrollList:Get(CSIndex(cellIndex))
    local cell = nil
    if go then
        cell = self.GetCell(go)
    end

    return cell
end

FriendList._OnUpdateCell = HL.Method(HL.Userdata, HL.Number, HL.Opt(HL.Function)) << function(self, object, index)
    local needLoadingItem = false
    if self.m_needLoadingItem and index <= #self.m_friendList then
        local success = false
        local info = nil
        if self.m_isPsnTab then
            success, info = GameInstance.player.friendSystem:GetPsnDictByIndex(self.m_arg.infoDicIndex):TryGetValue(self.m_friendList[index].accountId)
        else
            success, info = GameInstance.player.friendSystem:GetDictInfo(self.m_arg.infoDicIndex):TryGetValue(self.m_friendList[index].roleId)
        end



        if GameInstance.player.spaceship.isViewingFriend and self.m_friendList[index].roleId == GameInstance.player.spaceship:GetFriendRoleInfo().roleId then
            self.UpdateFunc(index, false)
            return
        end

        needLoadingItem = not success or not info.init
    end

    if self.m_needLoadingItem and (needLoadingItem or (index == self.view.scrollList.count and index > self.m_endIndex)) then

        local ids, endIndex = self:_GetNextPageNotInitIds(index)
        self.UpdateFunc(index, needLoadingItem)
        self.m_endIndex = endIndex
        if endIndex + 1 <= #self.m_friendList then
            endIndex = endIndex + 1
        end
        if #ids > 0 then
            if self.m_isPsnTab then
                GameInstance.player.friendSystem:SyncFriendInfoForPsn(self.m_arg.infoDicIndex, ids, function(delArray)
                    local del = 0
                    if delArray ~= nil then
                        del = delArray.Count
                        for _, roleId in cs_pairs(delArray) do
                            for i = 1, #self.m_friendList do
                                if self.m_friendList[i].roleId == roleId then
                                    table.remove(self.m_friendList, i)
                                    break
                                end
                            end
                        end
                    end
                    self.view.scrollList:UpdateCount(endIndex - del, false, false, false, true)
                end)
            else
                GameInstance.player.friendSystem:SyncFriendInfo(self.m_arg.infoDicIndex, ids, function(delArray)
                    local del = 0
                    if delArray ~= nil then
                        del = delArray.Count
                        for _, roleId in cs_pairs(delArray) do
                            for i = 1, #self.m_friendList do
                                if self.m_friendList[i].roleId == roleId then
                                    table.remove(self.m_friendList, i)
                                    break
                                end
                            end
                        end
                    end
                    self.view.scrollList:UpdateCount(endIndex - del, false, false, false, true)
                end)
            end
        else
            self.view.scrollList:UpdateCount(endIndex, false, false, false, true)
        end
        return
    end
    self.UpdateFunc(index, false)
end

FriendList._OnSortChanged = HL.Method(HL.Table, HL.Boolean, HL.Opt(HL.Boolean, HL.String, HL.Boolean)) << function(self, optData, isIncremental, needLoadingItem, noInfoTip, needUpdateSearchCache)
    if self.m_arg.customSortFun then
        if self.m_friendList then
            self.m_friendList = self.m_arg.customSortFun(self.m_friendList)
            self:RefreshInfo(self.m_friendList, needLoadingItem, noInfoTip, false, needUpdateSearchCache)
        end
    else
        if self.m_friendList then
            table.sort(self.m_friendList, Utils.genSortFunctionWithIgnore(optData.keys, isIncremental, { "searchSort" }))
            self:RefreshInfo(self.m_friendList, needLoadingItem, noInfoTip, false, needUpdateSearchCache)
        end
    end
end

FriendList._OnInputFieldChange = HL.Method(HL.String) << function(self, searchKey)
    self.m_friendList = {}

    local friendSystem = GameInstance.player.friendSystem
    local searchFriends = friendSystem:SearchFriend(searchKey, self.m_rawIdList)
    for i = 1, searchFriends.Count do
        self.m_friendList[i] = FriendUtils.friendInfo2SortInfo(searchFriends[CSIndex(i)])
        self.m_friendList[i].searchSort = self.SearchSort
    end

    self:_OnSortChanged(self.view.sortNode:GetCurSortData(), self.view.sortNode.isIncremental, self.m_needLoadingItem, self.m_noInfoTip, false)
    self.view.searchResultTxt.text = #self.m_friendList
    self.view.scrollList:UpdateCount(#self.m_friendList, true)
end

FriendList._OnDisable = HL.Override() << function(self)
end

HL.Commit(FriendList)
return FriendList

