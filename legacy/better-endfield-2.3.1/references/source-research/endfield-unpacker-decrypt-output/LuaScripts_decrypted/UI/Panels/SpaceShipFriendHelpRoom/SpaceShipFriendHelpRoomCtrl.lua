
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceShipFriendHelpRoom

SpaceShipFriendHelpRoomCtrl = HL.Class('SpaceShipFriendHelpRoomCtrl', uiCtrl.UICtrl)






SpaceShipFriendHelpRoomCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SPACESHIP_USE_HELP_CREDIT] = 'OnUseHelpCredit',
    [MessageConst.ON_SPACESHIP_ASSIST_DATA_MODIFY] = 'OnDataModify',
}

SpaceShipFriendHelpRoomCtrl.m_roomId = HL.Field(HL.String) << ""

SpaceShipFriendHelpRoomCtrl.m_formulaId = HL.Field(HL.String) << ""

SpaceShipFriendHelpRoomCtrl.m_tickCoroutine = HL.Field(HL.Thread)

SpaceShipFriendHelpRoomCtrl.m_useCount = HL.Field(HL.Number) << 1

SpaceShipFriendHelpRoomCtrl.m_useCountMax = HL.Field(HL.Number) << 1


SpaceShipFriendHelpRoomCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitViews()
    if arg and arg.roomId and arg.formulaId then
        self.m_roomId = arg.roomId
        self.m_formulaId = arg.formulaId
    else
        logger.error("[SpaceShipFriendHelpRoom]: arg no roomId or no valid formula")
    end
    self:_RenderViews()
end





SpaceShipFriendHelpRoomCtrl.OnClose = HL.Override() << function(self)
    self:_StopTickIfNecessary()
end

SpaceShipFriendHelpRoomCtrl._InitViews = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.view.btnBackLv01.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            self:Close()
        end)
    end)

    self.view.btnCommonYellow.onClick:AddListener(function()
        self:_OnConfirm()
    end)
end

SpaceShipFriendHelpRoomCtrl._RenderViews = HL.Method() << function(self)
    local leftSec, produceRate = self:_GetFormulaLeftSec()
    local helpProgress = self:_GetHelpProgress()
    local helpSec = produceRate > 0 and helpProgress / produceRate or 0
    local useCountMax = 0
    if helpSec > 0 then
        useCountMax = (leftSec // helpSec) + 1
    else
        useCountMax = 0
    end
    self:_InitNumberSelector(useCountMax)
    self:_StopTickIfNecessary()
    self:_RenderFormulaInfo()
    self.view.terminalNode:InitSpaceShipFriendHelpTerminalNode(self.m_roomId, self)
    self.m_tickCoroutine = self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.COMMON_UI_TIME_UPDATE_INTERVAL)
            self:_TickFormulaPanel()
        end
    end)
end

SpaceShipFriendHelpRoomCtrl._InitNumberSelector = HL.Method(HL.Number) << function(self, useCountMax)
    local helpLimit = SpaceshipUtils.getRoomHelpLimit(self.m_roomId)
    local leftHelpCount, beHelpedCount = GameInstance.player.spaceship:GetCabinAssistedTime(self.m_roomId)
    local helpMaxLimit = math.min(useCountMax, helpLimit)
    local helpMax = math.min(helpMaxLimit, leftHelpCount)
    local helpMin = math.min(1, helpMaxLimit)
    self.m_useCount = math.min(self.m_useCount, helpMax)
    self.view.numberSelector:InitNumberSelector(self.m_useCount, helpMin, helpMax, function(curNumber, isChangeByBtn)
        self:_OnNumberSelectorChange(curNumber, isChangeByBtn)
    end)
end

SpaceShipFriendHelpRoomCtrl._OnNumberSelectorChange = HL.Method(HL.Number, HL.Boolean)
    << function(self, curNumber, isChangeByBtn)
    self.m_useCount = curNumber
    local spaceship = GameInstance.player.spaceship
    local isProducing = spaceship:IsManufacturingStateProducing(self.m_roomId)
    if not isProducing then
        self:_RenderFormulaInfo()
    end
    local leftSec, produceRate = self:_GetFormulaLeftSec()
    self:_RenderFastTimeInfo(leftSec, produceRate)
end

SpaceShipFriendHelpRoomCtrl._StopTickIfNecessary = HL.Method() << function(self)
    if self.m_tickCoroutine ~= nil then
        self:_ClearCoroutine(self.m_tickCoroutine)
        self.m_tickCoroutine = nil
    end
end

SpaceShipFriendHelpRoomCtrl._TickFormulaPanel = HL.Method() << function(self)
    local spaceship = GameInstance.player.spaceship
    local isProducing = spaceship:IsManufacturingStateProducing(self.m_roomId)
    if not isProducing then
        self:_RenderFormulaInfo()
        return
    end
    self:_RenderFormulaInfo()
end

SpaceShipFriendHelpRoomCtrl._GetFormulaLeftSec = HL.Method().Return(HL.Number, HL.Number) << function(self)
    local spaceship = GameInstance.player.spaceship
    local remainFormulaId = spaceship:GetManufacturingStationRemainFormulaId(self.m_roomId)
    local hasRemainFormula = not string.isEmpty(remainFormulaId)
    if hasRemainFormula then
        local formulaData = Tables.spaceshipManufactureFormulaTable[remainFormulaId]
        local lastSyncTime = spaceship:GetManufacturingStationLastSyncTime(self.m_roomId)
        local curProgress = spaceship:GetManufacturingStationCurProgress(self.m_roomId)
        local remainProduceCount = spaceship:GetManufacturingStationRemainProduceCount(self.m_roomId)
        local roomProduceRate = spaceship:GetRoomProduceRate(self.m_roomId, formulaData.roomAttrType)
        local isProducing = spaceship:IsManufacturingStateProducing(self.m_roomId)

        local diffProgress = isProducing and (DateTimeUtils.GetCurrentTimestampBySeconds() - lastSyncTime) * roomProduceRate or 0
        local realProgress = curProgress + diffProgress
        local curLeftProgress = math.max(0, formulaData.totalProgress - realProgress)
        local leftSec = (remainProduceCount > 0 and roomProduceRate > 0) and (curLeftProgress + formulaData.totalProgress * math.max(0, remainProduceCount - 1)) / roomProduceRate or 0
        return leftSec, roomProduceRate
    else
        return 0, 0
    end
end

SpaceShipFriendHelpRoomCtrl._GetHelpProgress = HL.Method().Return(HL.Number) << function(self)
    local spaceship = GameInstance.player.spaceship
    local helpProgress = 0
    local succ, room = spaceship:TryGetRoom(self.m_roomId)
    if succ and room ~= nil then
        if room.type == GEnums.SpaceshipRoomType.ManufacturingStation then
            helpProgress = Tables.spaceshipConst.manufacturingStationBeHelpedRewardProgress
        end
    end
    return helpProgress
end

SpaceShipFriendHelpRoomCtrl._RenderFormulaInfo = HL.Method() << function(self)
    local spaceship = GameInstance.player.spaceship
    local hasRemainFormula = not string.isEmpty(self.m_formulaId)
    local leftSec, produceRate = self:_GetFormulaLeftSec()
    local formulaData = Tables.spaceshipManufactureFormulaTable[self.m_formulaId]
    local outcomeItemId = formulaData.outcomeItemId
    if hasRemainFormula then
        local remainProduceCount = spaceship:GetManufacturingStationRemainProduceCount(self.m_roomId)
        self.view.itemBig:InitItem({ id = outcomeItemId, count = remainProduceCount}, true)
    else
        self.view.itemBig:InitItem({ id = outcomeItemId, count = 0}, true)
    end
    self.view.timeTxt.text = UIUtils.getLeftTimeToSecond(leftSec)
    self:_RenderFastTimeInfo(leftSec, produceRate)
end

SpaceShipFriendHelpRoomCtrl._RenderFastTimeInfo = HL.Method(HL.Number, HL.Number) << function(self, leftSec, produceRate)
    local hasRemainFormula = not string.isEmpty(self.m_formulaId)
    local helpProgress = self:_GetHelpProgress()
    local finalHelpProgress = helpProgress * self.m_useCount
    local finalHelpTime = finalHelpProgress / produceRate
    local helpTimeStr = UIUtils.getLeftTime(finalHelpTime)
    if hasRemainFormula then
        self.view.fastTimeTxt.text = finalHelpTime > leftSec and
            string.format(Language.LUA_SPACESHIP_HELP_ROOM_HELP_SEC_WASTE, helpTimeStr) or
            string.format(Language.LUA_SPACESHIP_HELP_ROOM_HELP_SEC, helpTimeStr)
    end
    self.view.fastTimeTxt.gameObject:SetActive(hasRemainFormula)
    local helpSec = produceRate > 0 and helpProgress / produceRate or 0
    local useCountMax = 0
    if helpSec > 0 then
        useCountMax = (leftSec // helpSec) + 1
    else
        useCountMax = 0
    end
    if useCountMax ~= self.m_useCountMax then
        self.m_useCountMax = useCountMax
        self:_InitNumberSelector(useCountMax)
    end
end

SpaceShipFriendHelpRoomCtrl._OnConfirm = HL.Method() << function(self)
    local leftSec, produceRate = self:_GetFormulaLeftSec()
    if leftSec <= 0 or produceRate == 0 then
        Notify(MessageConst.SHOW_TOAST, I18nUtils.GetText("ui_spaceship_friendhelproom_no_formula_toast"))
        return
    end
    local helpProgress = self:_GetHelpProgress()
    local helpSec = produceRate > 0 and helpProgress / produceRate or 0
    local useCountMax = 0
    if helpProgress > 0 then
        useCountMax = (leftSec // helpSec) + 1
    else
        useCountMax = 0
    end
    local helpLimit = SpaceshipUtils.getRoomHelpLimit(self.m_roomId)
    local leftHelpCount, beHelpedCount = GameInstance.player.spaceship:GetCabinAssistedTime(self.m_roomId)
    local helpMaxLimit = math.min(useCountMax, helpLimit)
    local helpMax = math.min(helpMaxLimit, leftHelpCount)
    if self.m_useCount > helpMax or self.m_useCount <= 0 then
        return
    end
    GameInstance.player.spaceship:SpaceshipUseHelpRoomCreditManufacturingStation(self.m_roomId, self.m_useCount)
end

SpaceShipFriendHelpRoomCtrl.OnUseHelpCredit = HL.Method(HL.Opt(HL.Table)) << function(self, arg)
    Notify(MessageConst.SHOW_TOAST, Language.LUA_SS_USE_HELP_MANUFACTURING_STATION_TOAST)
    self:Close()
end

SpaceShipFriendHelpRoomCtrl.OnDataModify = HL.Method() << function(self)
    local leftHelpCount, beHelpedCount = GameInstance.player.spaceship:GetCabinAssistedTime(self.m_roomId)
    if leftHelpCount == 0 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SS_USE_HELP_MANUFACTURING_STATION_TOAST)
        self:Close()
    end
end

SpaceShipFriendHelpRoomCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    if DeviceInfo.usingController then
        self.view.numberSelector.view.keyHintReduce.gameObject:SetActive(active)
        self.view.numberSelector.view.keyHintAdd.gameObject:SetActive(active)
    end
end
HL.Commit(SpaceShipFriendHelpRoomCtrl)
