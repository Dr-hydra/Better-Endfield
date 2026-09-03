local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.KiteStation
local INSTRUCTION_BOOK_ID = "kite_station"

KiteStationCtrl = HL.Class('KiteStationCtrl', uiCtrl.UICtrl)

KiteStationCtrl.m_getCellFunc = HL.Field(HL.Function)





KiteStationCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_KITE_STATION_MODIFY] = '_OnKiteStationClick',
    [MessageConst.ON_KITE_STATION_WEEKLY_RESET] = 'ShowRefreshToast',
}

KiteStationCtrl.m_updateFuncTable = HL.Field(HL.Table)

KiteStationCtrl.m_id = HL.Field(HL.String) << ""

KiteStationCtrl.m_selectKiteStationIndex = HL.Field(HL.Number) << 1

KiteStationCtrl.m_kiteStationEntrustIds = HL.Field(HL.Userdata)


KiteStationCtrl.m_selectCellIndex = HL.Field(HL.Number) << 1

KiteStationCtrl.m_nextRefreshTimeStamp = HL.Field(HL.Number) << 0

KiteStationCtrl.ShowKiteStation = HL.StaticMethod(HL.Table) << function(args)
    PhaseManager:OpenPhase(PhaseId.KiteStation, {
        kiteStationId = args[1],
    })
end


KiteStationCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.domainTopMoneyTitle.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.KiteStation)
    end)

    self.m_id = arg.kiteStationId or arg.poiId or ""
    local sort = math.maxinteger
    if string.isEmpty(self.m_id) then
        if arg.domainId ~= nil then
            for kiteStationId, kiteStationData in pairs(Tables.kiteStationLevelTable) do
                local data = GameInstance.player.kiteStationSystem:GetKiteStationDataByInstId(kiteStationId)
                if kiteStationData.domainId == arg.domainId then
                    if data.level > 0 and kiteStationData.sort < sort then
                        self.m_id = kiteStationId
                        sort = kiteStationData.sort
                        self.m_nextRefreshTimeStamp = data.nextRefreshTimeStamp
                    end
                end
            end
        end

        if string.isEmpty(self.m_id) then
            logger.error("KiteStationCtrl.OnCreate: Kite station ID is empty!")
            return
        end
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    local kiteStationMapList = GameInstance.player.kiteStationSystem:GetKiteStationMapList()


    for i = 0,kiteStationMapList.Count-1 do
        local item = kiteStationMapList[i]
        if item.Item1 == self.m_id then
            self.m_selectKiteStationIndex = LuaIndex(i)
            break
        end
    end

    self.view.helpBtn.onClick:RemoveAllListeners()
    self.view.helpBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, INSTRUCTION_BOOK_ID)
    end)

    self.view.rewardBtn.onClick:RemoveAllListeners()
    self.view.rewardBtn.onClick:AddListener(function()
        self.m_phase:OpenCollectionReward(self.m_id)
    end)

    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.settlementList)
    self.view.settlementList.onUpdateCell:RemoveAllListeners()
    self.view.settlementList.onUpdateCell:AddListener(function(object, index)
        local cell = self.m_getCellFunc(object)
        cell.button.onClick:RemoveAllListeners()
        cell.button.onClick:AddListener(function()
            self.m_selectKiteStationIndex = LuaIndex(index)
            self.view.settlementList:UpdateCount(kiteStationMapList.Count)
            self.m_id = kiteStationMapList[index].Item1
            self:_OnKiteStationClick()

            self.view.redDot:InitRedDot("KiteStationCollectionReward", self.m_id)
        end)
        cell.redDot:InitRedDot("KiteStationCollectionReward", kiteStationMapList[index].Item1)
        local levelCfg = Tables.kiteStationLevelTable:GetValue(kiteStationMapList[index].Item1).list[kiteStationMapList[index].Item2]
        cell.nameText.text = levelCfg.name
        cell.lvNumberTxt.text = string.format(Language.LUA_KITE_STATION_TITLE_LEVEL, levelCfg.level, #Tables.kiteStationLevelTable:GetValue(kiteStationMapList[index].Item1).list)
        cell.stateController:SetState(self.m_selectKiteStationIndex == LuaIndex(index) and "Select" or "Unselect")
        if self.m_selectKiteStationIndex == LuaIndex(index) then
            self.view.domainTopMoneyTitle:InitDomainTopMoneyTitle(levelCfg.domainId)
        end
    end)


    self:BindInputPlayerAction("kiteStation_next", function()
        self.m_selectKiteStationIndex = self.m_selectKiteStationIndex + 1
        if self.m_selectKiteStationIndex > kiteStationMapList.Count then
            self.m_selectKiteStationIndex = 1
        end
        self.view.settlementList:UpdateCount(kiteStationMapList.Count)
        self.m_id = kiteStationMapList[CSIndex(self.m_selectKiteStationIndex)].Item1
        self:_OnKiteStationClick()
        self.view.redDot:InitRedDot("KiteStationCollectionReward", self.m_id)
    end, self.view.inputBindingGroupMonoTarget.groupId)

    self:BindInputPlayerAction("kiteStation_preview", function()
        self.m_selectKiteStationIndex = self.m_selectKiteStationIndex - 1
        if self.m_selectKiteStationIndex < 1 then
            self.m_selectKiteStationIndex = kiteStationMapList.Count
        end
        self.view.settlementList:UpdateCount(kiteStationMapList.Count)
        self.m_id = kiteStationMapList[CSIndex(self.m_selectKiteStationIndex)].Item1
        self:_OnKiteStationClick()
        self.view.redDot:InitRedDot("KiteStationCollectionReward", self.m_id)
    end, self.view.inputBindingGroupMonoTarget.groupId)

    self.view.settlementList:UpdateCount(kiteStationMapList.Count)
    self.view.hintRoot.gameObject:SetActiveIfNecessary(kiteStationMapList.Count > 1)
    self:_OnKiteStationClick()

    self.view.redDot:InitRedDot("KiteStationCollectionReward", self.m_id)


    for i = 1, 3 do
        local cell = self.view["kiteStationDelegation" .. i]
        if cell ~= nil and cell.inputBindingGroupNaviDecorator ~= nil and cell.inputBindingGroupNaviDecorator.enabled then
            self:SetNaviTarget(cell.inputBindingGroupNaviDecorator)
            break
        end
    end

    self:_TryRecoverInstructionBook(arg)

end

KiteStationCtrl.OnPhaseRefresh = HL.Override(HL.Opt(HL.Any)) << function(self, arg)
    local kiteStationMapList = GameInstance.player.kiteStationSystem:GetKiteStationMapList()
    self.view.settlementList:UpdateCount(kiteStationMapList.Count)
    self:_OnKiteStationClick()
    self.view.redDot:InitRedDot("KiteStationCollectionReward", self.m_id)
end

KiteStationCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local recoverState = {
        kiteStationId = self.m_id,
    }

    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and instructionCtrl:IsShow() and instructionCtrl.id == INSTRUCTION_BOOK_ID and PhaseManager:GetTopPhaseId() == PhaseId.KiteStation then
        recoverState.instructionBookArg = {
            id = instructionCtrl.id,
        }
    end


    return recoverState
end

KiteStationCtrl.ShowRefreshToast = HL.Method() << function()
    Notify(MessageConst.SHOW_TOAST, Language.LUA_KITE_STATION_WEEKLY_RESET)
end

KiteStationCtrl._TryRecoverInstructionBook = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil or recoverState.instructionBookArg == nil then
        return
    end
    if PhaseManager:GetTopPhaseId() ~= PhaseId.KiteStation then
        return
    end
    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and instructionCtrl:IsShow() then
        return
    end
    UIManager:Open(PanelId.InstructionBook, recoverState.instructionBookArg)
    recoverState.instructionBookArg = nil
end



KiteStationCtrl._OnKiteStationClick = HL.Method() << function(self)


    logger.info("KiteStationCtrl._OnKiteStationClick: Kite station clicked." .. self.m_id)
    self.m_nextRefreshTimeStamp = GameInstance.player.kiteStationSystem:GetKiteStationDataByInstId(self.m_id).nextRefreshTimeStamp


    local allNotCompleted = true

    local entrustIds = GameInstance.player.kiteStationSystem:GetEntrustIdx(self.m_id)
    local data = GameInstance.player.kiteStationSystem:GetKiteStationDataByInstId(self.m_id)


    if entrustIds.Count < Tables.kiteStationLevelTable:GetValue(self.m_id).list[data.level].entrustSlotCnt then
        allNotCompleted = false
    end
    if allNotCompleted then
        for i = 0, entrustIds.Count - 1 do
            local state = GameInstance.player.kiteStationSystem:GetEntrustState(self.m_id, entrustIds[i])
            if state == CS.Proto.KITE_STATION_ENTRUST_TASK_STATUS.Completed then
                allNotCompleted = false
                break
            end
        end
    end

    if allNotCompleted then
        self.view.countDownText:InitCountDownText(self.m_nextRefreshTimeStamp,nil, function(leftTime)
            return Language.LUA_KITE_STATION_ALL_MISSION_COMPLETED
        end)
    else
        self.view.countDownText:InitCountDownText(self.m_nextRefreshTimeStamp)
    end

    self.m_kiteStationEntrustIds = GameInstance.player.kiteStationSystem:GetEntrustIdx(self.m_id)

    for i = 1, 3 do
        self:_UpdateKiteStationDelegationCell(i)
    end
end

KiteStationCtrl._UpdateKiteStationDelegationCell = HL.Method(HL.Number) << function(self,index)
    local cell = self.view["kiteStationDelegation" .. index]
    if CSIndex(index) < self.m_kiteStationEntrustIds.Count then
        cell.inputBindingGroupNaviDecorator.enabled = true
        local cfg = Tables.kiteStationEntrustTasksTable:GetValue(self.m_id).list[self.m_kiteStationEntrustIds[CSIndex(index)]]
        cell.acceptMissionBtn.onClick:RemoveAllListeners()
        cell.acceptMissionBtn.onClick:AddListener(function()
            Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_KITE_ACCEPT_MISSION, cfg.name))
            GameInstance.player.kiteStationSystem:SendAcceptKiteStationMission(self.m_id, self.m_kiteStationEntrustIds[CSIndex(index)])
            self:_UpdateKiteStationDelegationCell(index)
        end)
        cell.jumpToMissionBtn.onClick:RemoveAllListeners()
        cell.jumpToMissionBtn.onClick:AddListener(function()
            PhaseManager:OpenPhase(PhaseId.Mission, { autoSelect = cfg.missionId })
        end)


        local missionState = GameInstance.player.kiteStationSystem:GetEntrustState(self.m_id, self.m_kiteStationEntrustIds[CSIndex(index)])
        local missionLevel = GameInstance.player.kiteStationSystem:GetEntrustLevel(self.m_id, self.m_kiteStationEntrustIds[CSIndex(index)])
        if missionState ~= -1 and missionLevel ~= -1 then
            cell.mainUIState:SetState(missionState:ToString())
        else

            cell.mainUIState:SetState(GEnums.MissionState.None:ToString())
            logger.error("KiteStationCtrl._UpdateKiteStationDelegationCell: Mission state is Failed, missionId: " .. cfg.missionId)
            return
        end
        cell.titleTxt.text = cfg.name
        cell.nameTxt.text = cfg.shotTargetName
        cell.descTxt.text = missionState == CS.Proto.KITE_STATION_ENTRUST_TASK_STATUS.Completed and cfg.completeDesc or cfg.desc
        cell.iconImg:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT_KITE_STATION, cfg.shotTargetIcon)
        cell.timeConsumeNode:SetState(cfg.timeConsumeType:ToString())

        local isCollection = GameInstance.player.kiteStationSystem:IsKiteStationHasCollection(self.m_id, self.m_kiteStationEntrustIds[CSIndex(index)])
        cell.newMissionNode:SetState(isCollection and 'Normal' or 'New')
        if missionLevel <= #cfg.rewardIdLv then
            local rewardID = cfg.rewardIdLv[missionLevel - 1]
            local rewardValid, rewardCfg = Tables.rewardTable:TryGetValue(rewardID)
            if rewardValid and rewardCfg then
                cell.numberTxt.text = rewardCfg.itemBundles[0].count
            end
        end
    else

        cell.inputBindingGroupNaviDecorator.enabled = false
        cell.mainUIState:SetState(GEnums.MissionState.None:ToString())
        local minLevel = math.maxinteger

        for level, data in pairs(Tables.kiteStationLevelTable:GetValue(self.m_id).list) do
            if data.entrustSlotCnt >= index then
                minLevel = math.min(minLevel, level)
            end
        end
        local data = GameInstance.player.kiteStationSystem:GetKiteStationDataByInstId(self.m_id)

        if data.level >= minLevel then
            cell.lockTxt.text = string.format(Language.LUA_KITE_STATION_EMPTY_UNLOCK)
        else
            cell.lockTxt.text = string.format(Language.LUA_KITE_STATION_NOT_UNLOCK, minLevel)
        end
    end
end











HL.Commit(KiteStationCtrl)
