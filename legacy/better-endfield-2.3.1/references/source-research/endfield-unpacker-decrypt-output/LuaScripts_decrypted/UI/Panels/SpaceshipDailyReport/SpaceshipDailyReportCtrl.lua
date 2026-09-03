local MAX_REPORT_COUNT = 3

local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceshipDailyReport
local PHASE_ID = PhaseId.SpaceshipDailyReport
SpaceshipDailyReportCtrl = HL.Class('SpaceshipDailyReportCtrl', uiCtrl.UICtrl)






SpaceshipDailyReportCtrl.s_messages = HL.StaticField(HL.Table) << {

}

SpaceshipDailyReportCtrl.m_reportInfos = HL.Field(HL.Table)

SpaceshipDailyReportCtrl.m_reportBackBindingIds = HL.Field(HL.Table)

SpaceshipDailyReportCtrl.m_isScrollInit = HL.Field(HL.Boolean) << false

SpaceshipDailyReportCtrl.m_curIndex = HL.Field(HL.Number) << -1

SpaceshipDailyReportCtrl.m_dayTabCache = HL.Field(HL.Forward('UIListCache'))

SpaceshipDailyReportCtrl.m_focusBindingIds = HL.Field(HL.Table)

SpaceshipDailyReportCtrl.m_getReportCell = HL.Field(HL.Function)




SpaceshipDailyReportCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.view.btnLeft.onClick:AddListener(function()
        self:_ChangeToDay(self.m_curIndex + 1)
    end)
    self.view.btnRight.onClick:AddListener(function()
        self:_ChangeToDay(self.m_curIndex - 1)
    end)

    self.m_dayTabCache = UIUtils.genCellCache(self.view.dayTabCell)
    self.m_getReportCell = UIUtils.genCachedCellFunction(self.view.scrollViewScrollList)

    self:_InitData()
    local recoverState = arg and arg.recoverState or nil
    if recoverState then
        self:_ChangeToDay(self:_GetRecoverDayIndex(recoverState))
        arg.recoverState = nil
    end
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

SpaceshipDailyReportCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    return {
        recoverState = {
            dayIndex = self.m_curIndex,
        }
    }
end

SpaceshipDailyReportCtrl._GetRecoverDayIndex = HL.Method(HL.Table).Return(HL.Number) << function(self, recoverState)
    local dayCount = #self.m_reportInfos
    local recoverDayIndex = recoverState.dayIndex
    if type(recoverDayIndex) ~= "number" or dayCount <= 0 then
        return 1
    end
    if recoverDayIndex < 1 then
        return 1
    end
    if recoverDayIndex > dayCount then
        return dayCount
    end
    return recoverDayIndex
end

SpaceshipDailyReportCtrl._InitData = HL.Method() << function(self)
    self.m_reportInfos = {}
    self.m_reportBackBindingIds = {}
    self.m_focusBindingIds = {}
    local curDayStartTimestamp = DateTimeUtils.GetServerCurBelongedDayStartTimestamp()
    local secondsPerDay = 24 * 3600
    local dayStartTimestamps = {}
    local roomReportInfoByTs = {}
    for k = 1, MAX_REPORT_COUNT do
        local ts = curDayStartTimestamp - (k - 1) * secondsPerDay
        table.insert(dayStartTimestamps, ts)
        roomReportInfoByTs[ts] = {}
    end
    local rooms = GameInstance.player.spaceship:GetRoomsWithSort()
    for i = CSIndex(1),  CSIndex(rooms.Count) do
        local data = rooms[i]
        local roomId = rooms[i].id
        if data.roomType == GEnums.SpaceshipRoomType.GuestRoom then
            goto continue
        end
        local haveTodayReport = false
        local isCC = data.roomType == GEnums.SpaceshipRoomType.ControlCenter
        for ts, report in pairs(data.reports) do
            if roomReportInfoByTs[ts] then
                if ts == curDayStartTimestamp then
                    haveTodayReport = true
                end
                table.insert(roomReportInfoByTs[ts], {
                    id = roomId,
                    isCC = isCC,
                    sortId = i,
                    data = data,
                    report = report,
                })
            end
        end

        if not haveTodayReport then
            table.insert(roomReportInfoByTs[curDayStartTimestamp], {
                id = roomId,
                isCC = isCC,
                sortId = i,
                data = data,
            })
        end
        ::continue::
    end

    for k, ts in ipairs(dayStartTimestamps) do
        local roomReports = roomReportInfoByTs[ts]
        if next(roomReports) then
            table.sort(roomReports, Utils.genSortFunction({ "sortId"}, true))
            table.insert(self.m_reportInfos, {
                ts = ts,
                roomReports = roomReports,
            })
        end
    end

    local dayCount = #self.m_reportInfos
    self.m_dayTabCache:Refresh(dayCount)

    self:_ChangeToDay(1)
end

SpaceshipDailyReportCtrl._ChangeToDay = HL.Method(HL.Number) << function(self, index)
    if self.m_curIndex == index and not self.m_isScrollInit then
        return
    end
    self.m_isScrollInit = true
    self.m_curIndex = index
    self.m_dayTabCache:Get(index).toggle.isOn = true
    self:_RefreshRoomCells()
    self:_RefreshBottom()
end

SpaceshipDailyReportCtrl._RefreshRoomCells = HL.Method() << function(self)
    local info = self.m_reportInfos[self.m_curIndex]
    self.view.scrollViewScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getReportCell(obj)
        self:_OnUpdateRoomCell(cell, LuaIndex(csIndex))
    end)
    self.view.scrollViewScrollList:UpdateCount(#info.roomReports, true)
end

SpaceshipDailyReportCtrl._RefreshBottom = HL.Method() << function(self)
    local info = self.m_reportInfos[self.m_curIndex]
    local isToday = self.m_curIndex == 1

    self.view.todayHint.gameObject:SetActive(isToday)
    self.view.btnLeft.interactable = self.m_curIndex < #self.m_reportInfos
    self.view.btnRight.interactable = self.m_curIndex > 1

    local dateNode = self.view.dateNode
    dateNode.animationWrapper:PlayInAnimation()
    dateNode.simpleStateController:SetState(isToday and "Today" or "NotToday")

    local offsetSeconds = Utils.getServerTimeZoneOffsetSeconds()
    dateNode.dateTxt.text = os.date("!%m.%d", info.ts + offsetSeconds)
    dateNode.todayImg.gameObject:SetActive(isToday)
    if isToday then
        dateNode.startTimeTxt.text = string.format("%02d:00", DateTimeUtils.GAME_DAY_DIVISION_HOUR)
        local curTs = DateTimeUtils.GetCurrentTimestampBySeconds()
        local cutTxt = os.date("!%H:%M", curTs + Utils.getServerTimeZoneOffsetSeconds())
        if curTs - info.ts >= (24 - DateTimeUtils.GAME_DAY_DIVISION_HOUR) * 3600 then
            cutTxt = cutTxt .. "(+1)"
        end
        dateNode.curTimeTxt.text = cutTxt
    end
end

SpaceshipDailyReportCtrl._OnUpdateRoomCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local info = self.m_reportInfos[self.m_curIndex].roomReports[index]
    local roomInfo = info.data
    local roomTypeData = Tables.spaceshipRoomTypeTable[info.data.roomType]
    cell.icon:LoadSprite(UIConst.UI_SPRITE_SPACESHIP_ROOM, roomTypeData.icon)
    cell.nameTxt.text = SpaceshipUtils.getFormatCabinSerialNum(roomInfo.id, roomInfo.serialNum)
    cell.iconBg.color = UIUtils.getColorByString(roomTypeData.color)
    cell.gameObject.name = index

    local typeTxtStr = SpaceshipConst.TYPE_TXT_MAP[roomInfo.type]
    if typeTxtStr then
        cell.typeNode.gameObject:SetActive(true)
        cell.typeTxt.text = typeTxtStr
    else
        cell.typeNode.gameObject:SetActive(false)
    end

    local isToday = self.m_curIndex == 1
    if not cell.m_charCells then
        cell.m_charCells = UIUtils.genCellCache(cell.charCell)
    end

    local chars = {}
    local stationedChars = {}
    local charDivideLineIndex
    local maxCount = roomInfo.maxLvStationCount
    local curMaxCount = roomInfo.maxStationCharNum
    local curCount = roomInfo.stationedCharList.Count
    if isToday then
        for k = 1, maxCount do
            local cInfo = {}
            if k <= curCount then
                cInfo.charId = roomInfo.stationedCharList[CSIndex(k)]
                stationedChars[cInfo.charId] = true
            elseif k <= curMaxCount then
                cInfo.isEmpty = true
            else
                cInfo.isLocked = true
            end
            table.insert(chars, cInfo)
        end
        charDivideLineIndex = maxCount
        if info.report then
            for _, charId in pairs(info.report.charWorkRecord) do
                if not stationedChars[charId] then
                    table.insert(chars, { charId = charId })
                end
            end
        end
    else
        local showCount = math.max(info.report and info.report.charWorkRecord.Count or 0, 1)
        for k = 1, showCount do
            local cInfo = {}
            local charId = info.report and info.report.charWorkRecord.Count >= k and info.report.charWorkRecord[CSIndex(k)]
            if info.report and charId then
                cInfo.charId = charId
            else
                cInfo.isEmpty = true
            end
            table.insert(chars, cInfo)
        end
    end

    self.m_focusBindingIds[index] = self:BindInputPlayerAction("ss_focus_item", function()
        Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
            panelId = PANEL_ID,
            isGroup = true,
            id = cell.targetInputBindingGroupMonoTarget.groupId,
            noHighlight = false,
            useNormalFrame = true,
            rectTransform = cell.focusNode,
        })
        cell.targetSelectableNaviGroup:NaviToThisGroup(true)
        InputManagerInst:ToggleBinding(self.m_reportBackBindingIds[index], true)
    end, cell.inputBindingGroupMonoTarget.groupId)

    self.m_reportBackBindingIds[index] = self:BindInputPlayerAction("common_back", function()
        Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, cell.targetInputBindingGroupMonoTarget.groupId)
        InputManagerInst:ToggleBinding(self.m_reportBackBindingIds[index], false)
        self:SetNaviTarget(cell.inputBindingGroupNaviDecorator)
        Notify(MessageConst.HIDE_ITEM_TIPS)
        Notify(MessageConst.HIDE_SPACESHIP_CHAR_TIPS)
    end, cell.targetInputBindingGroupMonoTarget.groupId)

    InputManagerInst:ToggleBinding(self.m_reportBackBindingIds[index], false)
    InputManagerInst:ToggleBinding(self.m_focusBindingIds[index], false)

    cell.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:RemoveAllListeners()
    cell.inputBindingGroupNaviDecorator.onGroupSetAsNaviTarget:AddListener(function(select)
        local haveReport = false
        if info.report and info.report.charWorkRecord and info.report.outputs then
            haveReport = info.report.charWorkRecord.Count > 0 or info.report.outputs.Count > 0
        end
        InputManagerInst:ToggleBinding(self.m_focusBindingIds[index], select and haveReport)
        cell.charNode.controllerScrollEnabled = select
    end)

    cell.charNode.content.pivot = #chars > 10 and Vector2(0,0.5) or Vector2(0.5,0.5)
    cell.m_charCells:Refresh(#chars, function(charCell, charIndex)
        self:_OnUpdateCharCell(charCell, chars[charIndex], info)
        charCell.transform:SetSiblingIndex(CSIndex(charIndex))
    end)
    if charDivideLineIndex and #chars > charDivideLineIndex then
        cell.charDivideLine.gameObject:SetActive(true)
        cell.charDivideLine.transform:SetSiblingIndex(charDivideLineIndex)
    else
        cell.charDivideLine.gameObject:SetActive(false)
    end
    local items = {}
    if info.isCC then
        cell.emptyHint.gameObject:SetActive(false)
        cell.itemNode.gameObject:SetActive(false)
    else
        if info.report then
            for itemId, count in pairs(info.report.outputs) do
                local itemData = Tables.itemTable[itemId]
                table.insert(items, {
                    id = itemId,
                    count = count,
                    sortId1 = itemData.sortId1,
                    sortId2 = itemData.sortId2,
                    rarity = itemData.rarity,
                })
            end
            table.sort(items, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))
        end
        if not cell.m_itemCells then
            cell.m_itemCells = UIUtils.genCellCache(cell.item)
        end
        if next(items) then
            cell.emptyHint.gameObject:SetActive(false)
            cell.itemNode.gameObject:SetActive(true)
            cell.m_itemCells:Refresh(#items, function(itemCell, itemIndex)
                itemCell:InitItem(items[itemIndex], function()
                    itemCell:ShowTips()
                    Notify(MessageConst.HIDE_SPACESHIP_CHAR_TIPS)
                end)
                if DeviceInfo.usingController then
                    itemCell:SetEnableHoverTips(false)
                    itemCell:SetExtraInfo({
                        isSideTips = true,
                    })
                end
            end)

            if DeviceInfo.usingController then
                cell.targetSelectableNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
                    if not isFocused then
                        Notify(MessageConst.HIDE_ITEM_TIPS)
                        Notify(MessageConst.HIDE_SPACESHIP_CHAR_TIPS)
                    end
                end)
            end
        else
            cell.emptyHint.gameObject:SetActive(true)
            cell.itemNode.gameObject:SetActive(false)
        end
    end
    LayoutRebuilder.ForceRebuildLayoutImmediate(cell.charNode.content)
    if #chars > 0 then
        cell.charNode:ScrollToNaviTarget(cell.m_charCells:GetItem(1).charHead.view.button)
    end

    cell.targetSelectableNaviGroup.getDefaultSelectableFunc = function()
        for i, v in ipairs(chars) do
            if v.charId then
                return cell.m_charCells:GetItem(i).charHead.view.button
            end
        end
        for i, v in ipairs(items) do
            if v.id then
                return cell.m_itemCells:GetItem(i).view.button
            end
        end
    end
    if index == 1 and self.m_isScrollInit then
        self:SetNaviTarget(cell.inputBindingGroupNaviDecorator)
        cell.charNode.controllerScrollEnabled = true
        self.m_isScrollInit = false
    else
        cell.charNode.controllerScrollEnabled = false
    end
end

SpaceshipDailyReportCtrl._OnUpdateCharCell = HL.Method(HL.Table, HL.Table, HL.Table) << function(self, charCell, info, roomReport)
    charCell.friendshipChangeNode.gameObject:SetActive(false)

    charCell.charHead.view.emptyState.onGroupSetAsNaviTarget:RemoveAllListeners()
    charCell.charHead.view.emptyState.onGroupSetAsNaviTarget:AddListener(function(select)
        if select then
            Notify(MessageConst.HIDE_ITEM_TIPS)
            Notify(MessageConst.HIDE_SPACESHIP_CHAR_TIPS)
        end
    end)
    if info.isLocked then
        charCell.charHead.view.simpleStateController:SetState("Locked")
        if roomReport.isCC then
            charCell.friendshipChangeNode.gameObject:SetActive(true)
            charCell.friendshipChangeNode:SetState("Empty")
        end
        return
    elseif info.isEmpty then
        charCell.charHead.view.simpleStateController:SetState("Empty")
        if roomReport.isCC then
            charCell.friendshipChangeNode.gameObject:SetActive(true)
            charCell.friendshipChangeNode:SetState("Empty")
        end
        return
    end
    charCell.charHead.view.simpleStateController:SetState("Normal")

    local charId = info.charId
    local isSideTips = DeviceInfo.usingController
    charCell.charHead:InitSSCharHeadCell({
        charId = charId,
        targetRoomId = roomReport.id,
        onClick = function()
            charCell.charHead.view.selectNode.gameObject:SetActive(true)
            Notify(MessageConst.HIDE_ITEM_TIPS)
            Notify(MessageConst.SHOW_SPACESHIP_CHAR_TIPS, {
                key = charCell.transform,
                charId = charId,
                transform = charCell.tipsTarget.transform,
                onClose = function()
                    if not IsNull(charCell.charHead.view.selectNode)then
                        charCell.charHead.view.selectNode.gameObject:SetActive(false)
                    end
                end,
                isSideTips = isSideTips,
                posType = UIConst.UI_TIPS_POS_TYPE.LeftDown,
                ignoreWorkNode = true,
            })
        end,
        hideStaminaNode = true,
    })

    if not roomReport.isCC then
        return
    end
    charCell.friendshipChangeNode.gameObject:SetActive(true)
    charCell.friendshipChangeNode:SetState("Num")

    local curFriendship = GameInstance.player.spaceship.characters:get_Item(charId).friendship
    for k = 1, self.m_curIndex - 1 do
        local otherDayTs = self.m_reportInfos[k].ts
        local succ, r = roomReport.data.reports:TryGetValue(otherDayTs)
        if succ then
            local succ2, addedValue = r.outputs:TryGetValue(charId)
            if succ2 then
                curFriendship = curFriendship - addedValue
            end
        end
    end

    local finalPercent = math.floor(CSPlayerDataUtil.GetFriendshipPercent(curFriendship) * 100)
    local addedValue
    if roomReport.report then
        _, addedValue = roomReport.report.outputs:TryGetValue(charId)
    end
    local startPercent = math.floor(CSPlayerDataUtil.GetFriendshipPercent(curFriendship - (addedValue or 0)) * 100)
    charCell.friendshipTxt.text = string.format(Language.LUA_SPACESHIP_CHAR_FRIENDSHIP_FORMAT, finalPercent)
    charCell.addedFriendshipTxt.text = string.format(Language.LUA_SPACESHIP_CHAR_FRIENDSHIP_FORMAT, startPercent)
end

SpaceshipDailyReportCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    if DeviceInfo.usingController then
        self.view.keyHintLeft.gameObject:SetActive(active)
        self.view.keyHintRight.gameObject:SetActive(active)
    end
end

HL.Commit(SpaceshipDailyReportCtrl)
