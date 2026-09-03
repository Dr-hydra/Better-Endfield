local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceshipSalesRecords
local PHASE_ID = PhaseId.SpaceshipSalesRecords
SpaceshipSalesRecordsCtrl = HL.Class('SpaceshipSalesRecordsCtrl', uiCtrl.UICtrl)






SpaceshipSalesRecordsCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SPACESHIP_RECV_QUERY_VISIT_INFO] = 'OnRecvQueryVisitInfo',
}

SpaceshipSalesRecordsCtrl.m_getScrollListCell = HL.Field(HL.Function)

SpaceshipSalesRecordsCtrl.m_showCellInfo = HL.Field(HL.Table)

SpaceshipSalesRecordsCtrl.m_spaceship = HL.Field(HL.Any)

SpaceshipSalesRecordsCtrl.m_visitRecord = HL.Field(HL.Any)

SpaceshipSalesRecordsCtrl.m_queryVisitInfo = HL.Field(HL.Boolean) << false

SpaceshipSalesRecordsCtrl.m_haveFriendInfo = HL.Field(HL.Boolean) << false

SpaceshipSalesRecordsCtrl.m_bindFriendHeadAction = HL.Field(HL.Table)

SpaceshipSalesRecordsCtrl.m_bindExitFocusAction = HL.Field(HL.Table)

SpaceshipSalesRecordsCtrl.m_bindContentJumpInAction = HL.Field(HL.Number) << -1

SpaceshipSalesRecordsCtrl.m_csIndex2Cell = HL.Field(HL.Table)

SpaceshipSalesRecordsCtrl.m_readTickHandle = HL.Field(HL.Number) << -1

SpaceshipSalesRecordsCtrl.m_contentJumpIn = HL.Field(HL.Boolean) << false

SpaceshipSalesRecordsCtrl.autoSelectNaviCell = HL.Field(HL.Boolean) << false

SpaceshipSalesRecordsCtrl.m_requestHandle = HL.Field(HL.Number) << -1

SpaceshipSalesRecordsCtrl.m_requestTime = HL.Field(HL.Number) << 1

SpaceshipSalesRecordsCtrl.m_requestCount = HL.Field(HL.Number) << 0

SpaceshipSalesRecordsCtrl.m_requestTodayIds = HL.Field(HL.Table)

SpaceshipSalesRecordsCtrl.m_requestTodayIndex = HL.Field(HL.Number) << 1

SpaceshipSalesRecordsCtrl.m_requestYesterdayIds = HL.Field(HL.Table)

SpaceshipSalesRecordsCtrl.m_requestYesterdayIndex = HL.Field(HL.Number) << 1

local RequestBatchNum = 10


SpaceshipSalesRecordsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.m_bindFriendHeadAction = {}
    self.m_bindExitFocusAction = {}
    self.m_csIndex2Cell = {}
    if arg ~= nil and arg.fromCreditShop then
        self.view.backBtn.gameObject:SetActive(true)
        self.view.closeButton.gameObject:SetActive(false)
        self.view.backBtn.onClick:AddListener(function()
            PhaseManager:PopPhase(PHASE_ID)
        end)
    else
        self.view.backBtn.gameObject:SetActive(false)
        self.view.closeButton.gameObject:SetActive(true)
        self.view.closeButton.onClick:AddListener(function()
            PhaseManager:PopPhase(PHASE_ID)
        end)
    end

    self.m_getScrollListCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getScrollListCell(obj), csIndex)
    end)

    self.m_spaceship = GameInstance.player.spaceship
    self.m_spaceship:QueryVisitInfo()

    self.m_bindContentJumpInAction = self:BindInputPlayerAction("sale_role_content_jump_in", function()
        self.m_contentJumpIn = true
        InputManagerInst:ToggleBinding(self.m_bindContentJumpInAction, false)
        self.view.containerNaviGroup:ManuallyFocus()

        self.autoSelectNaviCell = true
    end)
    InputManagerInst:SetBindingText(self.m_bindContentJumpInAction, Language.LUA_SPACESHIP_SALES_JUMP_IN)
    InputManagerInst:ToggleBinding(self.m_bindContentJumpInAction, false)


    self.m_readTickHandle = LuaUpdate:Add("Tick", function(deltaTime)
        self:_ReadTick(deltaTime)
    end)

end

SpaceshipSalesRecordsCtrl.OnRecvQueryVisitInfo = HL.Method() << function(self)
    self.m_queryVisitInfo = true
    self.m_visitRecord = self.m_spaceship:GetRoomVisitRecord()
    self:_HandleFriendInfo()
end

SpaceshipSalesRecordsCtrl._HandleFriendInfo = HL.Method() << function(self)
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
            self:UpdateFriendCells()
        end)
    else
        self:UpdateFriendCells()
    end

end

SpaceshipSalesRecordsCtrl._RequestTick = HL.Method(HL.Number) << function(self, deltaTime)
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

SpaceshipSalesRecordsCtrl._GetNextPageNotInitIds = HL.Method().Return(HL.Table) << function(self)
    self.m_requestCount = self.m_requestCount + 1
    local ids = {}
    for i = 1, RequestBatchNum do
        self:AddTodayYesterdayId(ids)
    end

    return ids
end

SpaceshipSalesRecordsCtrl.AddTodayYesterdayId = HL.Method(HL.Table) << function(self, ids)
    if self.m_requestTodayIndex <= #self.m_requestTodayIds then
        table.insert(ids, self.m_requestTodayIds[self.m_requestTodayIndex])
        self.m_requestTodayIndex = self.m_requestTodayIndex + 1
    elseif self.m_requestYesterdayIndex <= #self.m_requestYesterdayIds then
        table.insert(ids, self.m_requestYesterdayIds[self.m_requestYesterdayIndex])
        self.m_requestYesterdayIndex = self.m_requestYesterdayIndex + 1
    end
end

SpaceshipSalesRecordsCtrl.UpdateFriendCells = HL.Method() << function(self)
    self.m_haveFriendInfo = true
    self.m_showCellInfo = {}
    local desInfo = {
        cellType = "description"
    }
    table.insert(self.m_showCellInfo, desInfo)

    local todayTimeInfo = {
        cellType = "todayTime",
        recvedCreditCnt = self.m_visitRecord.today.recvedCreditCnt,
        totalCreditCnt = self.m_visitRecord.today.totalCreditCnt,
    }
    table.insert(self.m_showCellInfo, todayTimeInfo)
    local todayShowCount = 0
    local todayCellInfo = {}
    if self.m_visitRecord.today.haveData and self.m_visitRecord.today.opDatas.Count > 0 then
        for _, opData in pairs(self.m_visitRecord.today.opDatas) do
            local haveData = false
            local success, info = GameInstance.player.friendSystem:TryGetFriendInfo(opData.roleId)
            if success and info.init then
                haveData = true
            end

            if haveData and opData.moneyIdToSoldPrice.Count > 0 then

                local showInfo = {
                    cellType = "todayNormalInfo",
                    opData = opData,
                }
                table.insert(todayCellInfo, showInfo)
                todayShowCount = todayShowCount + 1
            end
        end
    end

    local todayIndex = 0
    for _, showData in pairs(todayCellInfo) do
        todayIndex = todayIndex + 1
        if #todayCellInfo == 1 then
            showData.bgType = "normal"
        else
            if todayIndex == 1 then
                showData.bgType = "up"
            elseif todayIndex == #todayCellInfo then
                showData.bgType = "down"
            else
                showData.bgType = "empty"
            end
        end
        table.insert(self.m_showCellInfo, showData)
    end

    if todayShowCount == 0 then
        local todayEmptyInfo = {
            cellType = "todayEmptyInfo",
        }
        table.insert(self.m_showCellInfo, todayEmptyInfo)
    end

    local yesterdayTimeInfo = {
        cellType = "yesterdayTime",
        recvedCreditCnt = self.m_visitRecord.yesterday.recvedCreditCnt,
        totalCreditCnt = self.m_visitRecord.yesterday.totalCreditCnt,
    }
    table.insert(self.m_showCellInfo, yesterdayTimeInfo)

    local yesterdayShowCount = 0
    local yesterdayCellInfo = {}
    if self.m_visitRecord.yesterday.haveData and self.m_visitRecord.yesterday.opDatas.Count > 0 then
        for _, opData in pairs(self.m_visitRecord.yesterday.opDatas) do
            local haveData = false
            local success, info = GameInstance.player.friendSystem:TryGetFriendInfo(opData.roleId)
            if success and info.init then
                haveData = true
            end

            if haveData and opData.moneyIdToSoldPrice.Count > 0 then

                local showInfo = {
                    cellType = "yesterdayNormalInfo",
                    opData = opData,
                }
                table.insert(yesterdayCellInfo, showInfo)
                yesterdayShowCount = yesterdayShowCount + 1
            end
        end
    end

    local yesterdayIndex = 0
    for _, showData in pairs(yesterdayCellInfo) do
        yesterdayIndex = yesterdayIndex + 1
        if #todayCellInfo == 1 then
            showData.bgType = "normal"
        else
            if yesterdayIndex == 1 then
                showData.bgType = "up"
            elseif yesterdayIndex == #yesterdayCellInfo then
                showData.bgType = "down"
            else
                showData.bgType = "empty"
            end
        end
        table.insert(self.m_showCellInfo, showData)
    end

    if yesterdayShowCount == 0 then
        local yesterdayEmptyInfo = {
            cellType = "yesterdayEmptyInfo",
        }
        table.insert(self.m_showCellInfo, yesterdayEmptyInfo)
    end

    self.m_csIndex2Cell = {}
    self.view.scrollList:UpdateCount(#self.m_showCellInfo)
end

SpaceshipSalesRecordsCtrl._OnUpdateCell = HL.Method(HL.Table, HL.Number) << function(self, cell, csIndex)
    self.m_csIndex2Cell[csIndex] = cell

    local showCellInfo = self.m_showCellInfo[csIndex + 1]
    cell.descNode.gameObject:SetActive(false)
    cell.titleNode.gameObject:SetActive(false)
    cell.salesNode.gameObject:SetActive(false)
    cell.isNaviContent = false

    local curRealTime = DateTimeUtils.GetCurrentTimestampBySeconds() + Utils.getServerTimeZoneOffsetSeconds()
    local curShowTime = 0
    local curDate = os.date("!*t", curRealTime)
    if curDate.hour < UIConst.COMMON_SERVER_UPDATE_TIME then
        curShowTime = curRealTime - 86400
    else
        curShowTime = curRealTime
    end
    local nextRefreshTime = Utils.getNextCommonServerRefreshTime() + Utils.getServerTimeZoneOffsetSeconds()

    if showCellInfo.cellType == "description" then
        cell.descNode.gameObject:SetActive(true)
    elseif showCellInfo.cellType == "todayTime" then
        cell.titleNode.gameObject:SetActive(true)
        cell.titleNodeStateController:SetState("Today")
        cell.dateTxt.text = os.date("!" .. Language.LUA_CHAT_DATE_ONLY_MONTH_DAY, curShowTime)
        cell.timeTxt.gameObject:SetActive(true)
        cell.timeTxt.gameObject:SetActive(false)
        cell.currentText.gameObject:SetActive(true)

        local expireTime = nextRefreshTime - curRealTime + 86400
        self:UpdateTitleTimeInfo(cell, showCellInfo, expireTime)
    elseif showCellInfo.cellType == "todayNormalInfo" then
        cell.salesNode.gameObject:SetActive(true)
        self:UpdateNormalInfo(cell, showCellInfo)
    elseif showCellInfo.cellType == "todayEmptyInfo" then
        cell.salesNode.gameObject:SetActive(true)
        self:UpdateEmptyInfo(cell)
    elseif showCellInfo.cellType == "yesterdayTime" then
        cell.titleNode.gameObject:SetActive(true)
        cell.titleNodeStateController:SetState("Yesterday")
        local expireTime = nextRefreshTime - curRealTime
        cell.dateTxt.text = os.date("!" .. Language.LUA_CHAT_DATE_ONLY_MONTH_DAY, curShowTime - 86400)
        cell.timeTxt.gameObject:SetActive(false)
        cell.currentText.gameObject:SetActive(false)
        self:UpdateTitleTimeInfo(cell, showCellInfo, expireTime)
    elseif showCellInfo.cellType == "yesterdayNormalInfo" then
        cell.salesNode.gameObject:SetActive(true)
        self:UpdateNormalInfo(cell, showCellInfo)
    elseif showCellInfo.cellType == "yesterdayEmptyInfo" then
        cell.salesNode.gameObject:SetActive(true)
        self:UpdateEmptyInfo(cell)
    end

end

SpaceshipSalesRecordsCtrl.UpdateEmptyInfo = HL.Method(HL.Any) << function(self, cell)
    cell.salesRecordsCell:InitSpaceshipSalesRecordsCell(true, 0, nil)
    local groupId = cell.salesRecordsCell.view.normalInputGroup.groupId
    local headBindingId = self.m_bindFriendHeadAction[groupId]
    if headBindingId ~= nil then
        InputManagerInst:DeleteBinding(headBindingId)
        self.m_bindFriendHeadAction[groupId] = nil
    end
end

SpaceshipSalesRecordsCtrl.UpdateNormalInfo = HL.Method(HL.Any, HL.Any) << function(self, cell, showCellInfo)
    cell.isNaviContent = true
    local opData = showCellInfo.opData
    local bgType = showCellInfo.bgType

    if bgType == "normal" then
        cell.bgState:SetState("All")
    elseif bgType == "up" then
        cell.bgState:SetState("Up")
    elseif bgType == "down" then
        cell.bgState:SetState("Down")
    elseif bgType == "empty" then
        cell.bgState:SetState("Empty")
    end

    cell.salesRecordsCell:InitSpaceshipSalesRecordsCell(false, opData.roleId, opData.moneyIdToSoldPrice)
    local groupId = cell.salesRecordsCell.view.normalInputGroup.groupId

    local exitBindingId = self.m_bindExitFocusAction[groupId]
    if exitBindingId ~= nil then
        InputManagerInst:DeleteBinding(exitBindingId)
        self.m_bindExitFocusAction[groupId] = nil
    end

    self.m_bindExitFocusAction[groupId] = InputManagerInst:CreateBindingByActionId("sale_role_content_jump_out", function()
        self.m_contentJumpIn = false
        self.view.containerNaviGroup:ManuallyStopFocus()
        InputManagerInst:ToggleBinding(self.m_bindContentJumpInAction, true)
    end, groupId)

    local exitNewBindingId = self.m_bindExitFocusAction[groupId]
    if exitNewBindingId then
        InputManagerInst:SetBindingText(exitNewBindingId, Language.LUA_SPACESHIP_SALES_JUMP_OUT)
    end

    cell.salesRecordsCell.view.normalNaviDeco.onIsNaviTargetChanged = function(active)
        cell.salesRecordsCell.view.keyHint.gameObject:SetActive(active)
    end

end

SpaceshipSalesRecordsCtrl.UpdateTitleTimeInfo = HL.Method(HL.Any, HL.Any, HL.Any) << function(self, cell, showCellInfo, expireTime)
    local allCreditCnt = 0
    for _, creditData in pairs(Tables.SpaceshipCreditTable) do
        if creditData.creditRewardCnt > allCreditCnt then
            allCreditCnt = creditData.creditRewardCnt
        end
    end

    if showCellInfo.totalCreditCnt == 0 then
        cell.titleNodeStateController:SetState("NotObtained")
    else
        if showCellInfo.recvedCreditCnt >= showCellInfo.totalCreditCnt then
            cell.titleNodeStateController:SetState("Obtained")
        else
            cell.titleNodeStateController:SetState("Normal")
            cell.waitTime1Txt.text = string.format(Language.LUA_MAIL_CELL_EXPIRED_TIME_FORMAT, UIUtils.getShortLeftTime(expireTime))
            cell.waitNumTxt.text = showCellInfo.totalCreditCnt
        end
    end

    cell.recvedCreditTxt.text = showCellInfo.recvedCreditCnt
    cell.totalCreditTxt.text = allCreditCnt
end

SpaceshipSalesRecordsCtrl._ReadTick = HL.Method(HL.Number) << function(self, deltaTime)
    if not self.m_haveFriendInfo then
        return
    end

    local res = self.view.scrollList:GetShowRange()
    local naviCell = nil
    if res.x >= 0 and res.y >= 0 then
        local checkNum = 2
        if res.x <= 2 then
            checkNum = 1
        end
        for csIndex = res.x, res.y do
            local cell = self.m_csIndex2Cell[csIndex]
            if cell and cell.isNaviContent then
                checkNum = checkNum - 1
                if checkNum == 0 then
                    naviCell = cell
                    break
                end
            end
        end
    end

    if not self.m_contentJumpIn then
        if naviCell then
            InputManagerInst:ToggleBinding(self.m_bindContentJumpInAction, true)
        else
            InputManagerInst:ToggleBinding(self.m_bindContentJumpInAction, false)
        end
    end

    if naviCell and self.autoSelectNaviCell then
        self.autoSelectNaviCell = false
        InputManagerInst:ToggleBinding(self.m_bindContentJumpInAction, false)
        self:SetNaviTarget(naviCell.salesRecordsCell.view.normalNaviDeco)
    end
end

SpaceshipSalesRecordsCtrl.OnClose = HL.Override() << function(self)

    if self.m_readTickHandle > 0 then
        self.m_readTickHandle = LuaUpdate:Remove(self.m_readTickHandle)
    end

    GameInstance.player.friendSystem:ClearSyncCallback()
end

HL.Commit(SpaceshipSalesRecordsCtrl)
