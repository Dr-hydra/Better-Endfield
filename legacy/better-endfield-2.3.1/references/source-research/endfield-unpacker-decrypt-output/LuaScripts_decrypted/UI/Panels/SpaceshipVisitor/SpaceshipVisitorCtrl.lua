local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceshipVisitor

SpaceshipVisitorCtrl = HL.Class('SpaceshipVisitorCtrl', uiCtrl.UICtrl)






SpaceshipVisitorCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SPACESHIP_RECV_QUERY_VISIT_INFO] = 'OnRecvQueryVisitInfo',
}

SpaceshipVisitorCtrl.m_getScrollListCell = HL.Field(HL.Function)

SpaceshipVisitorCtrl.m_showInfo = HL.Field(HL.Table)

SpaceshipVisitorCtrl.m_bindSwitchAction = HL.Field(HL.Table)

SpaceshipVisitorCtrl.m_bindFriendHeadAction = HL.Field(HL.Table)

SpaceshipVisitorCtrl.m_spaceship = HL.Field(HL.Any)

SpaceshipVisitorCtrl.m_visitRecord = HL.Field(HL.Any)

SpaceshipVisitorCtrl.m_queryVisitInfo = HL.Field(HL.Boolean) << false

SpaceshipVisitorCtrl.m_haveFriendInfo = HL.Field(HL.Boolean) << false

SpaceshipVisitorCtrl.m_requestHandle = HL.Field(HL.Number) << -1

SpaceshipVisitorCtrl.m_requestTime = HL.Field(HL.Number) << 1

SpaceshipVisitorCtrl.m_requestCount = HL.Field(HL.Number) << 0

SpaceshipVisitorCtrl.m_requestTodayIds = HL.Field(HL.Table)

SpaceshipVisitorCtrl.m_requestTodayIndex = HL.Field(HL.Number) << 1

SpaceshipVisitorCtrl.m_requestYesterdayIds = HL.Field(HL.Table)

SpaceshipVisitorCtrl.m_requestYesterdayIndex = HL.Field(HL.Number) << 1

SpaceshipVisitorCtrl.m_todayRotationInfo = HL.Field(HL.Table)

SpaceshipVisitorCtrl.m_yesterdayRotationInfo = HL.Field(HL.Table)

SpaceshipVisitorCtrl.m_csIndex2Cell = HL.Field(HL.Table)

SpaceshipVisitorCtrl.m_pendingFocusCsIndex = HL.Field(HL.Number) << -1

local RequestBatchNum = 10

local SubCellState = {
    NoSell = "NoSell",
    IsSell = "IsSell",
}

local VISITOR_ICON_FOLDER = "Spaceship/SpaceshipReception"
local VISITOR_NO_OP_ICON = "reception_visitor_detailicon04"
local JOINED_INFO_EXCHANGE_TYPE_ICON = "reception_visitor_detailicon03"
local JOINED_INFO_EXCHANGE_ROOM_ICON = "reception_visitor_areaicon03"

local SOLD_PRICE_TYPE_ICON = "reception_visitor_detailicon01"

local PROD_SUPPORT_TYPE_ICON = "reception_visitor_detailicon02"

local CONTROL_CENTER_TYPE = 0


SpaceshipVisitorCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.m_bindSwitchAction = {}
    self.m_bindFriendHeadAction = {}
    self.m_showInfo = {}
    self.m_todayRotationInfo = {}
    self.m_yesterdayRotationInfo = {}

    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.SpaceshipVisitor)
    end)

    self.m_getScrollListCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getScrollListCell(obj), csIndex)
    end)

    self.view.todayCellButton.onClick:AddListener(function()
        self.view.toggleTodayCell.isOn = true
        self.view.toggleYesterdayCell.isOn = false
    end)
    self.view.yesterdayCellButton.onClick:AddListener(function()
        self.view.toggleTodayCell.isOn = false
        self.view.toggleYesterdayCell.isOn = true
    end)

    self.view.toggleTodayCell.onValueChanged:AddListener(function(isOn)
        if isOn then
            self:UpdateTodayInfo()
        end
    end)

    self.view.toggleYesterdayCell.onValueChanged:AddListener(function(isOn)
        if isOn then
            self:UpdateYesterdayInfo()
        end
    end)

    self.view.toggleTodayCell.isOn = true
    self.m_spaceship = GameInstance.player.spaceship

    local recoverState = arg and arg.recoverState or nil
    if recoverState then
        self:_TryRecoverState(recoverState)
        arg.recoverState = nil
    end

    self.m_spaceship:QueryVisitInfo()
    self.view.helpCellNumTxt.text = Language.LUA_SPACESHIP_VISITOR_LOADING_TEXT
    self.view.visitCellNumTxt.text = Language.LUA_SPACESHIP_VISITOR_LOADING_TEXT
end

SpaceshipVisitorCtrl.OnRecvQueryVisitInfo = HL.Method() << function(self)
    self.m_queryVisitInfo = true
    self.m_visitRecord = self.m_spaceship:GetRoomVisitRecord()
    self:_HandleFriendInfo()
end

SpaceshipVisitorCtrl._HandleFriendInfo = HL.Method() << function(self)
    self.m_requestTodayIds = {}
    self.m_requestYesterdayIds = {}

    local temp = {}
    for _, opData in pairs(self.m_visitRecord.today.opDatas) do
        temp[opData.roleId] = true
    end
    for roleId, _ in pairs(temp) do
        local success , friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)
        if not success or not friendInfo.init then
            table.insert(self.m_requestTodayIds, roleId)
        end
    end

    temp = {}
    for _, opData in pairs(self.m_visitRecord.yesterday.opDatas) do
        temp[opData.roleId] = true
    end

    for roleId, _ in pairs(temp) do
        local success , friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(roleId)
        if not success or not friendInfo.init then
            table.insert(self.m_requestYesterdayIds, roleId)
        end
    end

    local ids = self:_GetNextPageNotInitIds()
    if #ids > 0 then
        GameInstance.player.friendSystem:SyncSocialFriendInfo(ids, function()
            self.m_requestHandle = LuaUpdate:Add("Tick", function(deltaTime)
                self:_RequestTick(deltaTime)
            end)
            self:_RealUpdateInfo()
        end)
    else
        self:_RealUpdateInfo()
    end
end

SpaceshipVisitorCtrl._RealUpdateInfo = HL.Method() << function(self)
    self.m_haveFriendInfo = true
    self.view.helpCellNumTxt.text = self.m_visitRecord.weeklyBeSupportedCnt
    self.view.visitCellNumTxt.text = self.m_visitRecord.weeklyVisitRoleCnt

    local isBuild = GameInstance.player.spaceship:IsRoomBuild(Tables.spaceshipConst.guestRoomClueExtensionId)
    if isBuild then
        self.view.communicationCell.gameObject:SetActive(true)
        self.view.communicationCellNumTxt.text = self.m_visitRecord.weeklyJoinInfoExchangeCnt
    else
        self.view.communicationCell.gameObject:SetActive(false)
    end

    if self.view.toggleTodayCell.isOn then
        self:UpdateTodayInfo()
    else
        self:UpdateYesterdayInfo()
    end
end

SpaceshipVisitorCtrl._RequestTick = HL.Method(HL.Number) << function(self, deltaTime)
    self.m_requestTime = self.m_requestTime + deltaTime
    if self.m_requestTime < 1 then
        return
    end
    self.m_requestTime = 0

    local ids = self:_GetNextPageNotInitIds()
    if #ids == 0 then
        if self.m_requestHandle > 0 then
            self.m_requestHandle = LuaUpdate:Remove(self.m_requestHandle)
        end
        return
    end
    GameInstance.player.friendSystem:SyncSocialFriendInfo(ids)
end

SpaceshipVisitorCtrl._GetNextPageNotInitIds = HL.Method().Return(HL.Table) << function(self)
    self.m_requestCount = self.m_requestCount + 1
    local ids = {}
    if self.m_requestCount == 1 then
        for i = 1, RequestBatchNum do
            self:AddTodayId(ids)
        end
    elseif self.m_requestCount == 2 then
        for i = 1, RequestBatchNum do
            self:AddYesterdayId(ids)
        end
    else
        for i = 1, RequestBatchNum do
            if i % 2 == 0 then
                self:AddTodayId(ids)
            else
                self:AddYesterdayId(ids)
            end
        end
    end

    return ids
end

SpaceshipVisitorCtrl.AddTodayId = HL.Method(HL.Table) << function(self, ids)
    if self.m_requestTodayIndex <= #self.m_requestTodayIds then
        table.insert(ids, self.m_requestTodayIds[self.m_requestTodayIndex])
        self.m_requestTodayIndex = self.m_requestTodayIndex + 1
    elseif self.m_requestYesterdayIndex <= #self.m_requestYesterdayIds then
        table.insert(ids, self.m_requestYesterdayIds[self.m_requestYesterdayIndex])
        self.m_requestYesterdayIndex = self.m_requestYesterdayIndex + 1
    end
end

SpaceshipVisitorCtrl.AddYesterdayId = HL.Method(HL.Table) << function(self, ids)
    if self.m_requestYesterdayIndex <= #self.m_requestYesterdayIds then
        table.insert(ids, self.m_requestYesterdayIds[self.m_requestYesterdayIndex])
        self.m_requestYesterdayIndex = self.m_requestYesterdayIndex + 1
    elseif self.m_requestTodayIndex <= #self.m_requestTodayIds then
        table.insert(ids, self.m_requestTodayIds[self.m_requestTodayIndex])
        self.m_requestTodayIndex = self.m_requestTodayIndex + 1
    end
end

SpaceshipVisitorCtrl.UpdateTodayInfo = HL.Method() << function(self)
    self.m_showInfo = {}
    if not self.m_haveFriendInfo then
        self.m_showInfo.showNum = 0
    else
        self.m_showInfo.recordData = self.m_visitRecord.today
        self.m_showInfo.showNum = self.m_visitRecord.today.opDatas.Count

    end
    self:UpdateFriendCells()
end

SpaceshipVisitorCtrl.UpdateYesterdayInfo = HL.Method() << function(self)
    self.m_showInfo = {}
    if not self.m_haveFriendInfo then
        self.m_showInfo.showNum = 0
    else
        self.m_showInfo.recordData = self.m_visitRecord.yesterday
        self.m_showInfo.showNum = self.m_visitRecord.yesterday.opDatas.Count
    end
    self:UpdateFriendCells()
end

SpaceshipVisitorCtrl.UpdateFriendCells = HL.Method() << function(self)
    if self.m_showInfo.showNum > 0 then
        self.view.scrollList.gameObject:SetActive(true)
        self.view.visitorNull.gameObject:SetActive(false)
        self.m_csIndex2Cell = {}
        self.view.scrollList:UpdateCount(self.m_showInfo.showNum, 0)

        if self.m_pendingFocusCsIndex >= 0 then
            local rotInfo = self.view.toggleTodayCell.isOn and self.m_todayRotationInfo or self.m_yesterdayRotationInfo
            for csIndex, cell in pairs(self.m_csIndex2Cell) do
                if rotInfo[csIndex] == true then
                    LayoutRebuilder.ForceRebuildLayoutImmediate(cell.rectTransform)
                    self.view.scrollList:NotifyCellSizeChange(csIndex, cell.rectTransform.sizeDelta.y)
                end
            end
            local targetCsIndex = math.min(self.m_pendingFocusCsIndex, self.m_showInfo.showNum - 1)
            self.view.scrollList:ScrollToIndex(targetCsIndex, true)
            if self.m_csIndex2Cell[targetCsIndex] ~= nil then
                self:SetNaviTarget(self.m_csIndex2Cell[targetCsIndex].friendListCell.view.inputNaviDecorator)
            end
            self.m_pendingFocusCsIndex = -1
        elseif self.m_csIndex2Cell[0] ~= nil then
            self:SetNaviTarget(self.m_csIndex2Cell[0].friendListCell.view.inputNaviDecorator)
        end

        self.view.scrollListAnimationWrapper:PlayInAnimation()
    else
        self.view.scrollList.gameObject:SetActive(false)
        self.view.visitorNull.gameObject:SetActive(true)
    end
end

SpaceshipVisitorCtrl._OnUpdateCell = HL.Method(HL.Table, HL.Number) << function(self, cell, csIndex)
    self.m_csIndex2Cell[csIndex] = cell
    cell.cellMain.gameObject:SetActive(true)
    cell.loadingState.gameObject:SetActive(false)
    cell.subList.gameObject:SetActive(false)

    cell.subCellCache = cell.subCellCache or UIUtils.genCellCache(cell.subCell)

    local opData = self.m_showInfo.recordData.opDatas[csIndex]

    local groupId = cell.friendListCell.view.inputGroupTarget.groupId
    local switchBindingId = self.m_bindSwitchAction[groupId]
    if switchBindingId ~= nil then
        InputManagerInst:DeleteBinding(switchBindingId)
        self.m_bindSwitchAction[groupId] = nil
    end
    local haveData = false

    local success, info = GameInstance.player.friendSystem:TryGetFriendInfo(opData.roleId)
    if success and info.init then
        haveData = true
    end

    if haveData then
        self.m_bindSwitchAction[groupId] = InputManagerInst:CreateBindingByActionId("visitor_info_switch_detail", function()
            AudioAdapter.PostEvent("Au_UI_Button_DropDown")
            self:RotationBtnRect(cell, opData, csIndex, false)
            local lastZ = cell.friendListCell.view.spreadIconRect.localEulerAngles.z
            local switchDetailBindingId = self.m_bindSwitchAction[groupId]
            if switchDetailBindingId then
                if lastZ > 0 then
                    InputManagerInst:SetBindingText(switchDetailBindingId, Language.LUA_SPACESHIP_VISITOR_FOLD_HEAD)
                else
                    InputManagerInst:SetBindingText(switchDetailBindingId, Language.LUA_SPACESHIP_VISITOR_EXPAND_DETAIL)
                end
            end
        end, groupId)

        local lastZ = cell.friendListCell.view.spreadIconRect.localEulerAngles.z
        local switchDetailBindingId = self.m_bindSwitchAction[groupId]
        if switchDetailBindingId then
            if lastZ > 0 then
                InputManagerInst:SetBindingText(switchDetailBindingId, Language.LUA_SPACESHIP_VISITOR_FOLD_HEAD)
            else
                InputManagerInst:SetBindingText(switchDetailBindingId, Language.LUA_SPACESHIP_VISITOR_EXPAND_DETAIL)
            end
        end
        self:RotationBtnRect(cell, opData, csIndex, true)
    end

    local headBindingId = self.m_bindFriendHeadAction[groupId]
    if headBindingId ~= nil then
        InputManagerInst:DeleteBinding(headBindingId)
        self.m_bindFriendHeadAction[groupId] = nil
    end

    if haveData then
        self.m_bindFriendHeadAction[groupId] = InputManagerInst:CreateBindingByActionId("visitor_friend_info_detail", function()
            FriendUtils.FRIEND_CELL_HEAD_FUNC.BUSINESS_CARD_PHASE(opData.roleId).action()
        end, groupId)

        local headNewBindingId = self.m_bindFriendHeadAction[groupId]
        if headNewBindingId then
            InputManagerInst:SetBindingText(headNewBindingId, Language.LUA_SPACESHIP_VISITOR_TIP_HEAD)
        end
    end

    local leftSec = DateTimeUtils.GetCurrentTimestampBySeconds() - opData.lastTs
    local showTimeText = UIUtils.getShortLeftTime(leftSec)

    cell.friendListCell:RefreshFriendListCell(opData.roleId, {
        stateName = "SpaceshipVisitor",
        showVisitorTimeText = string.format(Language.LUA_SPACESHIP_LAST_VISIT_TIME_TEXT, showTimeText),
        hideSignature = true,
        onPlayerClick = function(headRectTransform, roleId)
            FriendUtils.FRIEND_CELL_HEAD_FUNC.BUSINESS_CARD_PHASE(roleId).action()
        end,
        onSpaceshipVisitorClick = function(id, iconRect)
            self:RotationBtnRect(cell, opData, csIndex, false)
        end
    }, "")

end

SpaceshipVisitorCtrl.RotationBtnRect = HL.Method(HL.Any, HL.Any, HL.Number, HL.Boolean) << function(self, cell, opData, csIndex, isInit)
    if isInit then
        if self.view.toggleTodayCell.isOn then
            if self.m_todayRotationInfo[csIndex] == true then
                cell.friendListCell.view.spreadIconRect.localEulerAngles = Vector3(0, 0, 180)
            else
                cell.friendListCell.view.spreadIconRect.localEulerAngles = Vector3(0, 0, 0)
            end
        else
            if self.m_yesterdayRotationInfo[csIndex] == true then
                cell.friendListCell.view.spreadIconRect.localEulerAngles = Vector3(0, 0, 180)
            else
                cell.friendListCell.view.spreadIconRect.localEulerAngles = Vector3(0, 0, 0)
            end
        end

        local lastZ = cell.friendListCell.view.spreadIconRect.localEulerAngles.z
        if lastZ > 0 then
            cell.subList.gameObject:SetActive(true)
            self:UpdateSubCellList(cell, opData)
        else
            cell.subList.gameObject:SetActive(false)
        end
    else
        local rect = cell.friendListCell.view.spreadIconRect
        local lastZ = 180 - rect.localEulerAngles.z
        rect.localEulerAngles = Vector3(0, 0, lastZ)
        if lastZ > 0 then
            cell.subList.gameObject:SetActive(true)
            self:UpdateSubCellList(cell, opData)
            if self.view.toggleTodayCell.isOn then
                self.m_todayRotationInfo[csIndex] = true
            else
                self.m_yesterdayRotationInfo[csIndex] = true
            end
        else
            cell.subList.gameObject:SetActive(false)
            if self.view.toggleTodayCell.isOn then
                self.m_todayRotationInfo[csIndex] = false
            else
                self.m_yesterdayRotationInfo[csIndex] = false
            end
        end
    end

    if not isInit then
        LayoutRebuilder.ForceRebuildLayoutImmediate(cell.rectTransform)
        self.view.scrollList:NotifyCellSizeChange(csIndex, cell.rectTransform.sizeDelta.y)
    end

    if DeviceInfo.usingController and not isInit then
        self.view.scrollRect:ScrollToNaviTarget(cell.friendListCell.view.inputNaviDecorator)
    end
end

SpaceshipVisitorCtrl.UpdateSubCellList = HL.Method(HL.Any, HL.Any) << function(self, cell, opData)
    if not opData.joinedInfoExchange and opData.prodSupportList.Count == 0 and opData.moneyIdToSoldPrice.Count == 0 then
        cell.subCellCache:Refresh(1, function(subCell, subLuaIndex)
            subCell.subIcon:LoadSprite(VISITOR_ICON_FOLDER, VISITOR_NO_OP_ICON)
            subCell.subDetailNode:SetState(SubCellState.NoSell)
            subCell.subDetailIcon.gameObject:SetActive(false)
            subCell.subDetailTxt.text = Language.LUA_SPACESHIP_NO_OP_DATA_TEXT
        end)
        return
    end

    local showInfoList = {}

    if opData.prodSupportList then
        for _, roomType in pairs(opData.prodSupportList) do
            local succ, roomData = Tables.spaceshipRoomTypeTable:TryGetValue(roomType)
            if succ then
                local nums = opData.prodSupportDict[roomType]
                local roomText = ""
                if nums.Count == 1 then
                    if roomType == CONTROL_CENTER_TYPE then
                        roomText = string.format(" %s ", roomData.name)
                    else
                        roomText = SpaceshipUtils.getFormatCabinSerialNumByName(roomData.name, nums[0])
                        roomText = string.format(" %s ", roomText)
                    end
                else
                    for i = 0, nums.Count - 2 do
                        if SpaceshipUtils.getRoomSerialNum(nums[i]) == "" then
                            roomText = roomText..string.format("%s、", roomData.name)
                        else
                            roomText = roomText..roomText..string.format("%s、", SpaceshipUtils.getFormatCabinSerialNumByName(roomData.name, nums[i]))
                        end
                    end
                    if SpaceshipUtils.getRoomSerialNum(nums[nums.Count-1]) == "" then
                        roomText = roomText..string.format("%s ", roomData.name)
                    else
                        roomText = roomText..string.format("%s ", SpaceshipUtils.getFormatCabinSerialNumByName(roomData.name, nums[nums.Count-1]))
                    end
                end

                local showInfo = {
                    showType = "prodSupport",
                    typeIcon = PROD_SUPPORT_TYPE_ICON,
                    roomIcon = roomData.visitorIcon,
                    sortId = roomData.sortId,
                    text = string.format(Language.LUA_SPACESHIP_OP_SUPPORT_TEXT, roomText),
                }
                table.insert(showInfoList, showInfo)
            end
        end

        table.sort(showInfoList, Utils.genSortFunction({ "sortId" }, false))
    end

    if opData.joinedInfoExchange then
        local showInfo = {
            showType = "JoinedInfoExchange",
            typeIcon = JOINED_INFO_EXCHANGE_TYPE_ICON,
            roomIcon = JOINED_INFO_EXCHANGE_ROOM_ICON,
            text = Language.LUA_SPACESHIP_OP_JOIN_EXCHANGE_TEXT,
        }
        table.insert(showInfoList, showInfo)
    end

    if opData.moneyIdToSoldPrice.Count > 0 then
        local showInfo = {
            showType = "moneyIdToSold",
            typeIcon = SOLD_PRICE_TYPE_ICON,
            moneyIdToSoldPrice = opData.moneyIdToSoldPrice
        }
        table.insert(showInfoList, showInfo)
    end

    cell.subCellCache:Refresh(#showInfoList, function(subCell, subLuaIndex)
        local showInfo = showInfoList[subLuaIndex]
        subCell.subIcon:LoadSprite(VISITOR_ICON_FOLDER, showInfo.typeIcon)
        if showInfo.showType == "moneyIdToSold" then
            subCell.subDetailNode:SetState(SubCellState.IsSell)
            subCell.subDetailTxt.text = Language.LUA_SPACESHIP_OP_SOLD_TEXT
            subCell.currencyCellCache = subCell.currencyCellCache or UIUtils.genCellCache(subCell.currencyCell)
            subCell.currencyCellCache:Refresh(showInfo.moneyIdToSoldPrice.Count, function(currencyCell, currencyLuaIndex)
                local soldPrice = showInfo.moneyIdToSoldPrice[currencyLuaIndex - 1]
                local succ, itemData = Tables.itemTable:TryGetValue(soldPrice.moneyIdStr)
                if succ then
                    currencyCell.currencyIcon:LoadSprite(UIConst.UI_SPRITE_WALLET, itemData.iconId)
                end
                currencyCell.currencyNumberTxt.text = soldPrice.price
            end)
        else
            subCell.subDetailNode:SetState(SubCellState.NoSell)
            subCell.subDetailIcon:LoadSprite(VISITOR_ICON_FOLDER, showInfo.roomIcon)
            subCell.subDetailTxt.text = showInfo.text
        end
    end)
end


SpaceshipVisitorCtrl.ShowSpaceshipVisitor = HL.StaticMethod(HL.Opt(HL.Table)) << function(args)
    PhaseManager:OpenPhase(PhaseId.SpaceshipVisitor)
end


SpaceshipVisitorCtrl.OnClose = HL.Override() << function(self)
    GameInstance.player.friendSystem:ClearSyncCallback()
    if self.m_requestHandle > 0 then
        self.m_requestHandle = LuaUpdate:Remove(self.m_requestHandle)
    end
end


SpaceshipVisitorCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = {}
    local recoverState = {
        isYesterdayTab = not self.view.toggleTodayCell.isOn,
    }

    local todayRotCopy = {}
    for k, v in pairs(self.m_todayRotationInfo) do
        todayRotCopy[k] = v
    end
    recoverState.todayRotationInfo = todayRotCopy

    local yesterdayRotCopy = {}
    for k, v in pairs(self.m_yesterdayRotationInfo) do
        yesterdayRotCopy[k] = v
    end
    recoverState.yesterdayRotationInfo = yesterdayRotCopy

    if DeviceInfo.usingController and self.m_csIndex2Cell then
        local curTarget = InputManagerInst.controllerNaviManager.curTarget
        if curTarget then
            for csIndex, cell in pairs(self.m_csIndex2Cell) do
                if cell.friendListCell.view.inputNaviDecorator == curTarget then
                    recoverState.focusedCsIndex = csIndex
                    break
                end
            end
        end
    end

    arg.recoverState = recoverState
    return arg
end

SpaceshipVisitorCtrl._TryRecoverState = HL.Method(HL.Table) << function(self, recoverState)
    if recoverState.todayRotationInfo then
        self.m_todayRotationInfo = recoverState.todayRotationInfo
    end
    if recoverState.yesterdayRotationInfo then
        self.m_yesterdayRotationInfo = recoverState.yesterdayRotationInfo
    end

    if recoverState.focusedCsIndex then
        self.m_pendingFocusCsIndex = recoverState.focusedCsIndex
    end

    if recoverState.isYesterdayTab then
        self.view.toggleTodayCell.isOn = false
        self.view.toggleYesterdayCell.isOn = true
    end
end

HL.Commit(SpaceshipVisitorCtrl)
