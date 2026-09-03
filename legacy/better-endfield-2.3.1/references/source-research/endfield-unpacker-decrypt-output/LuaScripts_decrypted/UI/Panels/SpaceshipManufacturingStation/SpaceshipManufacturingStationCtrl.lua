local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceshipManufacturingStation
local PHASE_ID = PhaseId.SpaceshipManufacturingStation

local RoomStateEnum = SpaceshipUtils.RoomStateEnum

SpaceshipManufacturingStationCtrl = HL.Class('SpaceshipManufacturingStationCtrl', uiCtrl.UICtrl)

SpaceshipManufacturingStationCtrl.m_roomId = HL.Field(HL.String) << ""

SpaceshipManufacturingStationCtrl.m_showingFormulaList = HL.Field(HL.Boolean) << false

SpaceshipManufacturingStationCtrl.m_curSelectFormulaId = HL.Field(HL.String) << ""

SpaceshipManufacturingStationCtrl.m_curSelectNumber = HL.Field(HL.Number) << -1

SpaceshipManufacturingStationCtrl.m_formulaList = HL.Field(HL.Table)

SpaceshipManufacturingStationCtrl.m_diffBetweenSelectAndRemain = HL.Field(HL.Number) << 0

SpaceshipManufacturingStationCtrl.m_diffBetweenSelectAndRemainDirty = HL.Field(HL.Boolean) << false

SpaceshipManufacturingStationCtrl.m_animationWrapper = HL.Field(HL.Userdata)

SpaceshipManufacturingStationCtrl.m_moveCam = HL.Field(HL.Boolean) << false

SpaceshipManufacturingStationCtrl.m_recoverState = HL.Field(HL.Table)
SpaceshipManufacturingStationCtrl.m_isRemote = HL.Field(HL.Boolean) << false

SpaceshipManufacturingStationCtrl.m_clearScreenKey = HL.Field(HL.Number) << -1







SpaceshipManufacturingStationCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.SPACESHIP_ON_SYNC_ROOM_STATION] = "OnSpaceshipRoomStationSync",
    [MessageConst.ON_SPACESHIP_MANUFACTURING_STATION_START] = "OnSpaceshipManufacturingStationStart",
    [MessageConst.ON_SPACESHIP_MANUFACTURING_STATION_SYNC] = "OnSpaceshipManufacturingStationSync",
    [MessageConst.ON_SPACESHIP_MANUFACTURING_STATION_COLLECT] = "OnSpaceshipManufacturingStationCollect",
    [MessageConst.ON_SPACESHIP_MANUFACTURING_STATION_CANCEL] = "OnSpaceshipManufacturingStationCancel",
}

SpaceshipManufacturingStationCtrl.s_cachedSortOptCsIndex = HL.StaticField(HL.Number) << 0

SpaceshipManufacturingStationCtrl.s_cachedSortIncremental = HL.StaticField(HL.Boolean) << false


SpaceshipManufacturingStationCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    self.m_roomId = arg.roomId
    self.m_moveCam = arg.moveCam == true
    self.m_isRemote = arg.isRemoteCamera == true
    self.m_clearScreenKey = arg.clearScreenKey or -1
    local spaceship = GameInstance.player.spaceship

    self.m_animationWrapper = self.animationWrapper

    self.view.enterFormulaSelectBtn.onClick:AddListener(function()

        self:_OpenFormulaList()
    end)

    self.view.formulaSelectConfirmBtn.onClick:AddListener(function()

        self:_OnFormulaSelectConfirmBtnClick()
    end)

    self.view.formulaSelectCancelBtn.onClick:AddListener(function()

        self:_OnFormulaSelectCancelBtnClick()
    end)

    self.view.collectBtn.onClick:AddListener(function()

        spaceship:ManufacturingStationCollect(self.m_roomId)
    end)

    self.view.changeFormulaBtn.onClick:AddListener(function()

        self:_OpenFormulaList()
    end)

    self.view.stopBtn.onClick:AddListener(function()

        self:_ShowCancelFormulaToast()
    end)
    self.view.moreInfoBtn.onClick:AddListener(function()
        self:_OnMoreInfoBtnClick()
    end)

    self:_InitBG()
    self:_InitRoomInfo()
    local recoverState = arg and arg.recoverState or nil
    self.m_recoverState = recoverState
    self:_InitFormulaPanel()

    if recoverState then
        self:_TryRecoverState(recoverState)
        arg.recoverState = nil
        self.m_recoverState = nil
    end



    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.COMMON_UI_TIME_UPDATE_INTERVAL)
            self:_TickFormulaPanel()
        end
    end)

    self:PlayAnimationIn()
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    self:_TryRecoverFriendHelpPopup(arg and arg.friendHelpPopupState or nil)
end

SpaceshipManufacturingStationCtrl._InitBG = HL.Method() << function(self)
    self.view.spaceshipRoomCommonBg:InitSpaceshipRoomCommonBg(self.m_roomId, function()
        self:_CloseFormulaList()
    end, function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
end

SpaceshipManufacturingStationCtrl._InitRoomInfo = HL.Method() << function(self)
    self.view.roomCommonInfo:InitSpaceshipRoomCommonInfo(self.m_roomId, self.m_moveCam, self.m_isRemote)
end

SpaceshipManufacturingStationCtrl._InitFormulaList = HL.Method() << function(self)
    local formulaData = {}
    formulaData.formulas = self:_ProcessFormulaTabListData()

    formulaData.sortOptions = {
        {
            name = Language.LUA_FAC_CRAFT_SORT_1,
            keys = { "isWorkingSortId", "isUnlockSortId",  "sortId" }
        },
        {
            name = Language.LUA_FAC_CRAFT_SORT_2,
            keys = { "isWorkingSortId", "rarity", "sortId" }
        },
    }

    formulaData.onCellClick = function(info)
        self.m_curSelectFormulaId = info.formulaId
        self.m_diffBetweenSelectAndRemainDirty = false
        self.m_diffBetweenSelectAndRemain = 0

        self:_RefreshNumberSelector()
        self:_OnFormulaChange()
        self:_RefreshPanelTopNode()
    end

    formulaData.selectedSortOptionCsIndex = SpaceshipManufacturingStationCtrl.s_cachedSortOptCsIndex
    formulaData.isIncremental = SpaceshipManufacturingStationCtrl.s_cachedSortIncremental

    formulaData.onSortChanged = function(csIndex, isIncremental)
        SpaceshipManufacturingStationCtrl.s_cachedSortOptCsIndex = csIndex
        SpaceshipManufacturingStationCtrl.s_cachedSortIncremental = isIncremental
    end

    if self.m_recoverState then
        formulaData.defaultSelectFormulaId = self.m_recoverState.curSelectFormulaId
        formulaData.defaultTabIndex = self.m_recoverState.formulaListTabIndex
    end
    self.view.formulaList:InitSpaceshipRoomFormulaList(formulaData)
end

SpaceshipManufacturingStationCtrl._ProcessFormulaTabListData = HL.Method().Return(HL.Table) << function(self)
    local spaceship = GameInstance.player.spaceship

    local formulaTabListData = {}
    local formulaShowType2Data = {}
    local allFormulaList = {}

    for _, formulaData in pairs(Tables.spaceshipManufactureFormulaTable) do
        if not formulaShowType2Data[formulaData.showingType] then
            formulaShowType2Data[formulaData.showingType] = {}
        end

        local outcomeItemData = Tables.itemTable[formulaData.outcomeItemId]
        local isUnlock = spaceship:IsManufactureFormulaUnlocked(self.m_roomId, formulaData.id)
        local isWorking = spaceship:GetManufacturingStationRemainFormulaId(self.m_roomId) == formulaData.id
        local formulaDataUnit = {
            formulaId = formulaData.id,
            itemId = outcomeItemData.id,
            roomId = self.m_roomId,
            isWorking = isWorking,
            isUnlock = isUnlock,
            roomAttrType = formulaData.roomAttrType,
            rarity = formulaData.rarity,
            sortId = formulaData.sortId,
            isWorkingSortId = isWorking and 0 or 1,
            inUnlockSortId = isUnlock and 1 or 0,
        }
        formulaDataUnit.isMfg = true

        table.insert(allFormulaList, formulaDataUnit)
        table.insert(formulaShowType2Data[formulaData.showingType], formulaDataUnit)
    end


    for showType, formulaDataList in pairs(formulaShowType2Data) do
        local showingTypeCfgData = Tables.factoryCraftShowingTypeTable[showType]
        table.insert(formulaTabListData, {
            tabName = showingTypeCfgData.name,
            iconName = showingTypeCfgData.icon,
            sortId = showType:GetHashCode(),
            list = formulaDataList,
        })
    end

    table.insert(formulaTabListData, {
        tabName = Language.LUA_SPACESHIP_ROOM_FORMULA_LIST_TAB_ALL_DESC,
        iconName = "icon_type_all",
        sortId = 0,
        list = allFormulaList,
    })
    table.sort(formulaTabListData, Utils.genSortFunction({ "sortId" }, true))

    return formulaTabListData
end

SpaceshipManufacturingStationCtrl._InitFormulaPanel = HL.Method() << function(self)

    self.view.numberSelector:InitNumberSelector(0, 0, 0, function(curNumber, isChangeByBtn)
        self:_OnNumberSelectorChange(curNumber, isChangeByBtn)
    end)

    local remainFormulaId = GameInstance.player.spaceship:GetManufacturingStationRemainFormulaId(self.m_roomId)
    if not string.isEmpty(remainFormulaId) then
        self.m_curSelectFormulaId = remainFormulaId
    end

    self:_RefreshPanelTopNode()
    self:_RefreshNumberSelector()
    self:_RefreshFormulaInfoByRemain()
    self:_RefreshPanelBottomNode()

    self:_ToggleFormulaListAndRoomInfo(false)
end

SpaceshipManufacturingStationCtrl._TickFormulaPanel = HL.Method() << function(self)
    local spaceship = GameInstance.player.spaceship
    local remainFormulaId = spaceship:GetManufacturingStationRemainFormulaId(self.m_roomId)
    local isProducing = spaceship:IsManufacturingStateProducing(self.m_roomId)

    if string.isEmpty(remainFormulaId) or not isProducing
        or self.m_showingFormulaList and self.m_curSelectFormulaId ~= remainFormulaId then
        return
    end

    self:_RefreshNumberSelector()
    self:_RefreshFormulaTimeInfoByRemain()
end

SpaceshipManufacturingStationCtrl._RefreshFormulaInfoByRemain = HL.Method() << function(self)
    self:_RefreshFormulaOutcomeInfo()
    self:_RefreshFormulaTimeInfoByRemain()
    self:_RefreshFormulaBottomState()
end

SpaceshipManufacturingStationCtrl._RefreshFormulaInfoBySelected = HL.Method() << function(self)
    self:_RefreshFormulaOutcomeInfo()
    self:_RefreshFormulaTimeInfoBySelected()
    self:_RefreshFormulaBottomState()
end

SpaceshipManufacturingStationCtrl._RefreshFormulaOutcomeInfo = HL.Method() << function(self)
    local spaceship = GameInstance.player.spaceship
    local formulaId = self.m_showingFormulaList and self.m_curSelectFormulaId
        or spaceship:GetManufacturingStationRemainFormulaId(self.m_roomId)

    local hasFormulaId = not string.isEmpty(formulaId)
    self.view.formulaInfo.gameObject:SetActiveIfNecessary(hasFormulaId)
    self.view.noFormula.gameObject:SetActiveIfNecessary(not hasFormulaId)
    self.view.formulaStateTxt.text = Language.LUA_SPACESHIP_MANUFACTURING_STATION_NO_FORMULA

    if hasFormulaId then
        local formulaCfg = Tables.spaceshipManufactureFormulaTable[formulaId]
        local itemId = formulaCfg.outcomeItemId
        local itemCfg = Tables.itemTable[itemId]
        local ownCount = Utils.getItemCount(itemId)
        self.view.formulaNameTxt.text = itemCfg.name
        self.view.formulaStateTxt.text = string.format(Language.LUA_SPACESHIP_MANUFACTURING_STATION_OWN_OUTCOME_FORMAT,
            ownCount)
        self.view.itemBig:InitItem({ id = itemId }, true)
    end
end

SpaceshipManufacturingStationCtrl._RefreshNumberSelector = HL.Method() << function(self)
    if string.isEmpty(self.m_curSelectFormulaId) then
        return
    end
    local spaceship = GameInstance.player.spaceship
    local remainFormulaId = spaceship:GetManufacturingStationRemainFormulaId(self.m_roomId)
    local machineCapacity = spaceship:GetManufacturingStationCapacity(self.m_roomId)

    local formulaData = Tables.spaceshipManufactureFormulaTable[self.m_curSelectFormulaId]
    local remainProduceCount = spaceship:GetManufacturingStationRemainProduceCount(self.m_roomId)
    local selectUnlock = spaceship:IsManufactureFormulaUnlocked(self.m_roomId, self.m_curSelectFormulaId)
    local hasRemainFormula = not string.isEmpty(remainFormulaId)
    local isRemainFormulaSelected = remainFormulaId == self.m_curSelectFormulaId
    local maxNum = math.floor(machineCapacity / formulaData.perCapacity)
    local defaultNum
    if selectUnlock then
        local remainFormulaSelectedShowCount = remainProduceCount > 0
            and math.max(remainProduceCount + self.m_diffBetweenSelectAndRemain, 1) or remainProduceCount
        defaultNum = (isRemainFormulaSelected or not self.m_showingFormulaList) and remainFormulaSelectedShowCount or 1
    else
        defaultNum = 0
    end
    self.view.numberSelector:RefreshNumber(defaultNum, math.min(defaultNum, 1), maxNum)
    self.view.numberSelector.gameObject:SetActiveIfNecessary(self.m_showingFormulaList and selectUnlock
        or not self.m_showingFormulaList and hasRemainFormula)
end

SpaceshipManufacturingStationCtrl._RefreshFormulaTimeInfoBySelected = HL.Method() << function(self)
    if string.isEmpty(self.m_curSelectFormulaId) then
        return
    end
    local spaceship = GameInstance.player.spaceship
    local machineCapacity = spaceship:GetManufacturingStationCapacity(self.m_roomId)

    local formulaData = Tables.spaceshipManufactureFormulaTable[self.m_curSelectFormulaId]
    local roomProduceRate = spaceship:GetRoomProduceRate(self.m_roomId, formulaData.roomAttrType)
    local selectUnlock = spaceship:IsManufactureFormulaUnlocked(self.m_roomId, self.m_curSelectFormulaId)

    self.view.schedule.fillAmount = 0
    self.view.countdownTxt.text = UIUtils.getLeftTimeToSecond(selectUnlock and (formulaData.totalProgress / roomProduceRate) or 0)
    self.view.capacityTxt.text = string.format(Language.LUA_SPACESHIP_MANUFACTURING_STATION_CAPACITY_FORMAT,
        self.m_curSelectNumber * formulaData.perCapacity, machineCapacity)
    self.view.timeTxt.text = UIUtils.getLeftTimeToSecond(self.m_curSelectNumber * formulaData.totalProgress / roomProduceRate)

    self.view.suspendImg.gameObject:SetActiveIfNecessary(false)
end

SpaceshipManufacturingStationCtrl._RefreshFormulaTimeInfoByRemain = HL.Method() << function(self)
    local spaceship = GameInstance.player.spaceship
    local remainFormulaId = spaceship:GetManufacturingStationRemainFormulaId(self.m_roomId)
    local machineCapacity = spaceship:GetManufacturingStationCapacity(self.m_roomId)

    local hasRemainFormula = not string.isEmpty(remainFormulaId)
    if hasRemainFormula then
        local formulaData = Tables.spaceshipManufactureFormulaTable[remainFormulaId]
        local lastSyncTime = spaceship:GetManufacturingStationLastSyncTime(self.m_roomId)
        local curProgress = spaceship:GetManufacturingStationCurProgress(self.m_roomId)
        local remainProduceCount = spaceship:GetManufacturingStationRemainProduceCount(self.m_roomId)
        local roomProduceRate = spaceship:GetRoomProduceRate(self.m_roomId, formulaData.roomAttrType)
        local isProducing = spaceship:IsManufacturingStateProducing(self.m_roomId)
        local isShutDown = spaceship:IsManufacturingStateShutDown(self.m_roomId)


        local panelRemainCount = self.m_curSelectNumber
        local diffProgress = isProducing and (DateTimeUtils.GetCurrentTimestampBySeconds() - lastSyncTime) * roomProduceRate or 0
        local realProgress = curProgress + diffProgress
        local curLeftProgress = math.max(0, formulaData.totalProgress - realProgress)
        self.view.formulaStateTxt.text = string.format(Language.LUA_SPACESHIP_MANUFACTURING_STATION_OWN_OUTCOME_FORMAT,
            Utils.getItemCount(formulaData.outcomeItemId))
        self.view.capacityTxt.text = string.format(Language.LUA_SPACESHIP_MANUFACTURING_STATION_CAPACITY_FORMAT,
            panelRemainCount * formulaData.perCapacity, machineCapacity)
        self.view.schedule.fillAmount = remainProduceCount > 0 and realProgress / formulaData.totalProgress or 0
        self.view.countdownTxt.text = UIUtils.getLeftTimeToSecond(panelRemainCount > 0 and curLeftProgress / roomProduceRate or 0)
        self.view.timeTxt.text = UIUtils.getLeftTimeToSecond(panelRemainCount > 0 and (curLeftProgress + formulaData.totalProgress * math.max(0, panelRemainCount - 1)) / roomProduceRate or 0)
        self.view.suspendImg.gameObject:SetActiveIfNecessary(isShutDown)
    else
        self.view.schedule.fillAmount = 0
        self.view.timeTxt.text = UIUtils.getLeftTimeToSecond(0)
        self.view.countdownTxt.text = UIUtils.getLeftTimeToSecond(0)
        self.view.capacityTxt.text = string.format(Language.LUA_SPACESHIP_MANUFACTURING_STATION_CAPACITY_FORMAT, 0,
            machineCapacity)
        self.view.overCapacityTag.gameObject:SetActiveIfNecessary(false)
        self.view.suspendImg.gameObject:SetActiveIfNecessary(false)
    end
end

SpaceshipManufacturingStationCtrl._RefreshFormulaBottomState = HL.Method() << function(self)



    self.view.changeFormulaNode.gameObject:SetActiveIfNecessary(false)
    self.view.enterFormulaSelectBtn.gameObject:SetActiveIfNecessary(false)
    self.view.formulaState.gameObject:SetActiveIfNecessary(false)
    self.view.collect.gameObject:SetActiveIfNecessary(false)

    local spaceship = GameInstance.player.spaceship
    local remainFormulaId = spaceship:GetManufacturingStationRemainFormulaId(self.m_roomId)
    local hasRemainFormula = not string.isEmpty(remainFormulaId)
    local isRemainFormulaSelected = remainFormulaId == self.m_curSelectFormulaId
    local selectUnlock = spaceship:IsManufactureFormulaUnlocked(self.m_roomId, self.m_curSelectFormulaId)

    local itemId, count = spaceship:GetManufacturingStationProduct(self.m_roomId)
    local isProducing = spaceship:IsManufacturingStateProducing(self.m_roomId)
    local hasProduct = not string.isEmpty(itemId)

    if not self.m_showingFormulaList and not hasRemainFormula then
        self.view.enterFormulaSelectBtn.gameObject:SetActiveIfNecessary(true)
    elseif self.m_showingFormulaList and selectUnlock or self.m_diffBetweenSelectAndRemainDirty then
        self.view.changeFormulaNode.gameObject:SetActiveIfNecessary(true)
        local showHint = spaceship:GetManufacturingStationRemainProduceCount(self.m_roomId) > 0 and
            not isRemainFormulaSelected and (isProducing or hasProduct)
        self.view.confirmHintNode.gameObject:SetActiveIfNecessary(showHint)
    elseif not self.m_showingFormulaList and hasProduct then
        AudioManager.PostEvent("Au_UI_Event_Manufacturing_Finish")
        self.view.collect.gameObject:SetActiveIfNecessary(true)
        self.view.itemSmall:InitItem({ id = itemId }, true)
        self.view.countTxt.text = 'x' .. count
    else
        self.view.formulaState.gameObject:SetActiveIfNecessary(true)
        if self.m_showingFormulaList then
            local formulaCfg = Tables.spaceshipManufactureFormulaTable[self.m_curSelectFormulaId]
            self.view.stateTxt.text = string.format(
                Language.LUA_SPACESHIP_MANUFACTURING_STATION_FORMULA_UNLOCK_CONDITION_FORMAT, formulaCfg.level)
        else
            local remainCount = spaceship:GetManufacturingStationRemainProduceCount(self.m_roomId)
            local lang
            if isProducing then
                lang = Language.LUA_SPACESHIP_MANUFACTURING_STATION_IS_PRODUCING
            elseif remainCount > 0 then
                lang = Language.LUA_SPACESHIP_MANUFACTURING_STATION_IS_PAUSING
            else
                lang = Language.LUA_SPACESHIP_MANUFACTURING_STATION_FINISH
            end
            self.view.stateTxt.text = lang
        end
    end

    self.view.outcomeDeco.gameObject:SetActiveIfNecessary(isRemainFormulaSelected and (isProducing or hasProduct))

    self.view.stopBtn.gameObject:SetActiveIfNecessary(isRemainFormulaSelected and self.m_showingFormulaList)
    self.view.changeFormulaBtn.gameObject:SetActiveIfNecessary(hasRemainFormula and not self.m_showingFormulaList)
end

SpaceshipManufacturingStationCtrl._OnNumberSelectorChange = HL.Method(HL.Number, HL.Boolean)
    << function(self, curNumber, isChangeByBtn)
    self.m_curSelectNumber = curNumber

    local spaceship = GameInstance.player.spaceship
    local remainFormulaId = spaceship:GetManufacturingStationRemainFormulaId(self.m_roomId)
    if (not self.m_showingFormulaList or self.m_showingFormulaList and self.m_curSelectFormulaId == remainFormulaId) and
        isChangeByBtn then
        local remainProduceCount = spaceship:GetManufacturingStationRemainProduceCount(self.m_roomId)
        self.m_diffBetweenSelectAndRemain = self.m_curSelectNumber - remainProduceCount
        self.m_diffBetweenSelectAndRemainDirty = true
        self:_RefreshFormulaTimeInfoByRemain()
        self:_RefreshOverCapacity(remainFormulaId)
    else
        self:_RefreshFormulaTimeInfoBySelected()
        self:_RefreshOverCapacity(self.m_curSelectFormulaId)
    end
    self:_RefreshFormulaBottomState()

    local cantStart = curNumber ~= 0
    self.view.formulaSelectConfirmBtn.interactable = cantStart
end

SpaceshipManufacturingStationCtrl._RefreshOverCapacity = HL.Method(HL.String) << function(self, formulaId)
    if string.isEmpty(formulaId) then
        self.view.overCapacityTag.gameObject:SetActiveIfNecessary(false)
        return
    end

    local spaceship = GameInstance.player.spaceship
    local machineCapacity = spaceship:GetManufacturingStationCapacity(self.m_roomId)
    local formulaData = Tables.spaceshipManufactureFormulaTable[formulaId]

    local maxNum = math.floor(machineCapacity / formulaData.perCapacity)
    local overCapacity = self.m_curSelectNumber == maxNum
    if overCapacity then
        local reachMachineCapacity = formulaData.perCapacity * self.m_curSelectNumber == machineCapacity
        self.view.overCapacityTxt.text = reachMachineCapacity and Language.LUA_SPACESHIP_MANUFACTURING_STATION_OVER_CAPACITY
            or Language.LUA_SPACESHIP_MANUFACTURING_STATION_NEARLY_OVER_CAPACITY
        self.view.overCapacityTag:ClearTween()
        self.view.overCapacityTag.gameObject:SetActiveIfNecessary(true)
    else
        self.view.overCapacityTag:PlayOutAnimation(function()
            self.view.overCapacityTag.gameObject:SetActiveIfNecessary(false)
        end)
    end
end

SpaceshipManufacturingStationCtrl._RefreshPanelTopNode = HL.Method() << function(self)

    self.view.spaceshipRoomCommonBg:SetTopInfoNodeState(self.m_showingFormulaList)

    if self.m_showingFormulaList then
        local spaceship = GameInstance.player.spaceship
        local formulaData = Tables.spaceshipManufactureFormulaTable[self.m_curSelectFormulaId]
        local rate = spaceship:GetRoomProduceRate(self.m_roomId, formulaData.roomAttrType)
        local txt = string.format(Language.LUA_SPACESHIP_MANUFACTURING_STATION_PRODUCE_RATE_FORMAT, rate)
        local _ ,info = Tables.spaceshipRoomAttrTypeTable:TryGetValue(formulaData.roomAttrType)
        self.view.spaceshipRoomCommonBg:RefreshTopInfoNode(info.name, txt)
    end
end

SpaceshipManufacturingStationCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    if DeviceInfo.usingController then
        self.view.numberSelector.view.keyHintAdd.gameObject:SetActive(active)
        self.view.numberSelector.view.keyHintReduce.gameObject:SetActive(active)
    end
end

SpaceshipManufacturingStationCtrl._RefreshPanelBottomNode = HL.Method() << function(self)

    local spaceship = GameInstance.player.spaceship
    local isProducing = spaceship:IsManufacturingStateProducing(self.m_roomId)
    local isShutDown = spaceship:IsManufacturingStateShutDown(self.m_roomId)

    if isProducing then
        self.view.spaceshipRoomCommonBg:SetState(RoomStateEnum.Producing)
    elseif isShutDown then
        self.view.spaceshipRoomCommonBg:SetState(RoomStateEnum.ShutDown)
    else
        self.view.spaceshipRoomCommonBg:SetState(RoomStateEnum.Idle)
    end

    self.view.suspendImg.gameObject:SetActiveIfNecessary(isShutDown)
end

SpaceshipManufacturingStationCtrl._OnFormulaChange = HL.Method() << function(self)
    if GameInstance.player.spaceship:GetManufacturingStationRemainFormulaId(self.m_roomId)
        == self.m_curSelectFormulaId then
        self:_RefreshFormulaInfoByRemain()
    else
        self:_RefreshFormulaInfoBySelected()
    end
end

SpaceshipManufacturingStationCtrl._OpenFormulaList = HL.Method() << function(self)
    self.m_animationWrapper:Play("spaceshipmanufacturingstation_change")
    AudioManager.PostEvent("Au_UI_Popup_DetailsPanel_Open")

    self:_ToggleFormulaListAndRoomInfo(true)
    self:_InitFormulaList()
    self:_RefreshPanelTopNode()
    self:_RefreshNumberSelector()
    self:_OnFormulaChange()
end

SpaceshipManufacturingStationCtrl._CloseFormulaList = HL.Method() << function(self)
    local remainFormulaId = GameInstance.player.spaceship:GetManufacturingStationRemainFormulaId(self.m_roomId)
    if not string.isEmpty(remainFormulaId) then
        self.m_curSelectFormulaId = remainFormulaId
    end

    self.m_animationWrapper:Play("spaceshipmanufacturingstation_return")
    AudioManager.PostEvent("Au_UI_Popup_DetailsPanel_Close")

    self:_ToggleFormulaListAndRoomInfo(false)
    self:_RefreshPanelTopNode()
    self:_RefreshNumberSelector()
    self:_RefreshFormulaInfoByRemain()
end

SpaceshipManufacturingStationCtrl._OnFormulaSelectConfirmBtnClick = HL.Method() << function(self)

    if string.isEmpty(self.m_curSelectFormulaId) then
        return
    end

    if not GameInstance.player.spaceship:IsManufactureFormulaUnlocked(self.m_roomId, self.m_curSelectFormulaId) then
        self:Notify(MessageConst.SHOW_TOAST, Language["ui_spaceship_manufacturingstation_formulalist_Lock"])
        return
    end


    local spaceship = GameInstance.player.spaceship
    local curFormula = spaceship:GetManufacturingStationRemainFormulaId(self.m_roomId)
    local _, count = spaceship:GetManufacturingStationProduct(self.m_roomId)
    local isProducing = spaceship:IsManufacturingStateProducing(self.m_roomId)
    if not string.isEmpty(curFormula) and curFormula ~= self.m_curSelectFormulaId then
        if (isProducing or count > 0) and spaceship:GetManufacturingStationRemainProduceCount(self.m_roomId) > 0 then
            self:_ShowExchangeFormulaToast()
        else
            spaceship:ManufacturingStationChangeFormula(self.m_roomId, self.m_curSelectFormulaId, self.m_curSelectNumber)
        end
    else
        spaceship:ManufacturingStationStart(self.m_roomId, self.m_curSelectFormulaId, self.m_curSelectNumber)
    end
end

SpaceshipManufacturingStationCtrl._OnFormulaSelectCancelBtnClick = HL.Method() << function(self)
    if self.m_showingFormulaList then
        self:_CloseFormulaList()
    else
        self.m_diffBetweenSelectAndRemain = 0
        self.m_diffBetweenSelectAndRemainDirty = false
        self:_RefreshNumberSelector()
        self:_RefreshFormulaInfoByRemain()
    end
end

SpaceshipManufacturingStationCtrl._ToggleFormulaListAndRoomInfo = HL.Method(HL.Boolean) << function(self, showFormula)
    self.m_showingFormulaList = showFormula
    self.view.spaceshipRoomCommonBg:SetFriendAssistNode(not showFormula)
    self.view.formulaList.gameObject:SetActiveIfNecessary(self.m_showingFormulaList)
    if not self.m_showingFormulaList then
        self.view.formulaList.view.formulaScrollListSelectableNaviGroup:ManuallyStopFocus()
    end
    self.view.roomCommonInfo.gameObject:SetActiveIfNecessary(not self.m_showingFormulaList)

    self.view.spaceshipRoomCommonBg:ToggleReturnBtnOn(showFormula)
end


SpaceshipManufacturingStationCtrl._ShowCancelFormulaToast = HL.Method() << function(self)
    local spaceship = GameInstance.player.spaceship
    local _, count = spaceship:GetManufacturingStationProduct(self.m_roomId)
    local content = count > 0 and Language.LUA_SPACESHIP_MANUFACTURING_STATION_CANCEL_FORMULA_COMMON_WITH_PRODUCT_DESC
        or Language.LUA_SPACESHIP_MANUFACTURING_STATION_CANCEL_FORMULA_COMMON_WITHOUT_PRODUCT_DESC
    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_SPACESHIP_MANUFACTURING_STATION_CANCEL_FORMULA_SUB_DESC .. "\n" .. content,
        onConfirm = function()
            spaceship:ManufacturingStationCollect(self.m_roomId)
            spaceship:ManufacturingStationCancel(self.m_roomId)
        end,
        onCancel = function()
        end
    })
end

SpaceshipManufacturingStationCtrl._ShowExchangeFormulaToast = HL.Method() << function(self)
    local spaceship = GameInstance.player.spaceship
    local _, count = spaceship:GetManufacturingStationProduct(self.m_roomId)
    local content = count > 0 and Language.LUA_SPACESHIP_MANUFACTURING_STATION_CANCEL_FORMULA_COMMON_WITH_PRODUCT_DESC
        or Language.LUA_SPACESHIP_MANUFACTURING_STATION_CANCEL_FORMULA_COMMON_WITHOUT_PRODUCT_DESC
    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_SPACESHIP_MANUFACTURING_STATION_EXCHANGE_FORMULA_SUB_DESC .. "\n" .. content,
        onConfirm = function()
            GameInstance.player.spaceship:ManufacturingStationChangeFormula(self.m_roomId, self.m_curSelectFormulaId,
                self.m_curSelectNumber)
        end,
        onCancel = function()
        end
    })
end

SpaceshipManufacturingStationCtrl._OnMoreInfoBtnClick = HL.Method() << function(self)
    local type = SpaceshipUtils.getRoomTypeByRoomId(self.m_roomId)
    local roomTypeData = Tables.spaceshipRoomTypeTable[type]

    Notify(MessageConst.SHOW_COMMON_TIPS, {
        text = roomTypeData.extraDesc1,
        transform = self.view.moreInfoBtn.transform,
        posType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
    })
end

SpaceshipManufacturingStationCtrl.OnSpaceshipManufacturingStationStart = HL.Method(HL.Any) << function(self, arg)
    self.m_diffBetweenSelectAndRemain = 0
    self.m_diffBetweenSelectAndRemainDirty = false

    if self.m_showingFormulaList then
        self:_CloseFormulaList()
    end

    self:_RefreshFormulaInfoByRemain()
    self:_RefreshPanelBottomNode()
end

SpaceshipManufacturingStationCtrl.OnSpaceshipManufacturingStationSync = HL.Method(HL.Any) << function(self, arg)
    self:_RefreshFormulaBottomState()
    self:_RefreshPanelBottomNode()

    local spaceship = GameInstance.player.spaceship
    local remainFormulaId = spaceship:GetManufacturingStationRemainFormulaId(self.m_roomId)
    if self.m_curSelectFormulaId == remainFormulaId and self.m_showingFormulaList or not self.m_showingFormulaList then
        self:_RefreshNumberSelector()
        self:_RefreshFormulaInfoByRemain()
        local _, productAdded = unpack(arg)
        if productAdded then
            self.view.formulaNode:Play("spaceshipmanufacturingstationformu_done")
        end
    end
end

SpaceshipManufacturingStationCtrl.OnSpaceshipManufacturingStationCollect = HL.Method(HL.Any) << function(self, args)
    local _, itemId, count = unpack(args)
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        title = Language.LUA_SPACESHIP_MANUFACTURING_STATION_COLLECT_OUTCOME,
        items = {
            { id = itemId, count = count },
        }
    })

    self:_RefreshFormulaInfoByRemain()
end

SpaceshipManufacturingStationCtrl.OnSpaceshipManufacturingStationCancel = HL.Method(HL.Any) << function(self, arg)
    self.m_diffBetweenSelectAndRemain = 0
    self.m_diffBetweenSelectAndRemainDirty = false

    self:_CloseFormulaList()
    self:_RefreshPanelBottomNode()
end

SpaceshipManufacturingStationCtrl.OnSpaceshipRoomStationSync = HL.Method() << function(self)
    self:_RefreshFormulaBottomState()
    self:_RefreshPanelBottomNode()
end

SpaceshipManufacturingStationCtrl.OnClose = HL.Override() << function(self)
    local clearScreenKey
    if self.m_moveCam then
        clearScreenKey = GameInstance.player.spaceship:UndoMoveCamToSpaceshipRoom(self.m_roomId)
        if clearScreenKey and clearScreenKey ~= -1 then
            UIManager:RecoverScreen(clearScreenKey)
        end
    end
    if self.m_clearScreenKey ~= -1 then
        if self.m_clearScreenKey ~= clearScreenKey then
            UIManager:RecoverScreen(self.m_clearScreenKey)
        end
        self.m_clearScreenKey = -1
    end
end

SpaceshipManufacturingStationCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = {
        roomId = self.m_roomId,
        moveCam = self.m_moveCam,
        clearScreenKey = self.m_clearScreenKey,
        recoverState = {
            showingFormulaList = self.m_showingFormulaList,
            formulaListTabIndex = self.m_showingFormulaList and self.view.formulaList.m_curTabIndex or nil,
            curSelectFormulaId = self.m_curSelectFormulaId,
            curSelectNumber = self.m_curSelectNumber,
        }
    }
    local isOpen, popupCtrl = UIManager:IsOpen(PanelId.SpaceShipFriendHelpList)
    if isOpen and popupCtrl and popupCtrl.m_roomId == self.m_roomId then
        arg.friendHelpPopupState = {
            roomId = popupCtrl.m_roomId,
        }
    end
    return arg
end

SpaceshipManufacturingStationCtrl._TryRecoverState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    local savedFormulaId = recoverState.curSelectFormulaId
    if recoverState.showingFormulaList == true then
        self:_OpenFormulaList()
        self:_RecoverNumberSelectorState(recoverState.curSelectNumber)
        self:_RefreshPanelTopNode()
    else
        if not string.isEmpty(savedFormulaId) then
            self.m_curSelectFormulaId = savedFormulaId
        end
        self:_RefreshNumberSelector()
        self:_RefreshFormulaInfoByRemain()
        self:_RecoverNumberSelectorState(recoverState.curSelectNumber)
    end
end

SpaceshipManufacturingStationCtrl._RecoverNumberSelectorState = HL.Method(HL.Opt(HL.Number)) << function(self, savedNumber)
    local numberSelector = self.view and self.view.numberSelector
    if not numberSelector or not savedNumber or savedNumber < 0 then
        return
    end
    self.m_curSelectNumber = math.min(math.max(savedNumber, numberSelector.m_min), numberSelector.m_max)
    numberSelector:RefreshNumber(self.m_curSelectNumber, numberSelector.m_min, numberSelector.m_max)
    self:_RefreshFormulaBottomState()
end

SpaceshipManufacturingStationCtrl._TryRecoverFriendHelpPopup = HL.Method(HL.Opt(HL.Any)) << function(self, popupState)
    if popupState == nil or string.isEmpty(popupState.roomId) or popupState.roomId ~= self.m_roomId then
        return
    end
    local recoverPopup = function()
        local isOpen, popupCtrl = UIManager:IsOpen(PanelId.SpaceShipFriendHelpList)
        if not isOpen or not popupCtrl or popupCtrl.m_roomId ~= popupState.roomId then
            UIManager:AutoOpen(PanelId.SpaceShipFriendHelpList, {
                roomId = popupState.roomId,
            })
            isOpen, popupCtrl = UIManager:IsOpen(PanelId.SpaceShipFriendHelpList)
        elseif not popupCtrl:IsShow() then
            UIManager:AutoOpen(PanelId.SpaceShipFriendHelpList, {
                roomId = popupState.roomId,
            })
        end
        if isOpen and popupCtrl then
            UIManager:SetTopOrder(PanelId.SpaceShipFriendHelpList)
        end
    end
    recoverPopup()
end

HL.Commit(SpaceshipManufacturingStationCtrl)
