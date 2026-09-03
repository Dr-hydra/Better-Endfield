local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CoinActivitySettlement
local PHASE_ID = PhaseId.CoinActivitySettlement
CoinActivitySettlementCtrl = HL.Class('CoinActivitySettlementCtrl', uiCtrl.UICtrl)









CoinActivitySettlementCtrl.m_resultData = HL.Field(HL.Any)

CoinActivitySettlementCtrl.m_scoreAnimLuaUpdateKey = HL.Field(HL.Number) << -1

CoinActivitySettlementCtrl.m_getCellFunc = HL.Field(HL.Function)





CoinActivitySettlementCtrl.s_messages = HL.StaticField(HL.Table) << {

}





CoinActivitySettlementCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_BindUI()
    self:_InitData(arg)
    self:_RefreshUI()
    self:_PlayAudio()
end






CoinActivitySettlementCtrl.OnClose = HL.Override() << function(self)
    if self.m_scoreAnimLuaUpdateKey > 0 then
        self.m_scoreAnimLuaUpdateKey = LuaUpdate:Remove(self.m_scoreAnimLuaUpdateKey)
    end
end




CoinActivitySettlementCtrl.ShowSettlement = HL.StaticMethod(HL.Any) << function(args)
    LuaSystemManager.commonTaskTrackSystem:AddRequest("DungeonSettlement", function()
        if not Utils.isInDungeon() then


            logger.error(ELogChannel.Dungeon, "error, try to open CoinActivitySettlementCtrl out of dungeon")
            return
        end


        AudioAdapter.PostEvent("au_ui_travel_pole_stop")


        PhaseManager:ExitPhaseFastTo(PhaseId.Level)

        local dungeonId, isPassed, addScore, milestoneScore, lastMilestoneScore = unpack(args)

        local ctrl = UIManager:AutoOpen(PANEL_ID, {
            dungeonId = dungeonId,
            isPassed = isPassed,
            addScore = addScore,
            milestoneScore = milestoneScore,
            lastMilestoneScore = lastMilestoneScore,
        })
    end, function()
        UIManager:Close(PANEL_ID)
    end)
end





CoinActivitySettlementCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    if arg.isFake then
        self:_InitFakeData(arg)
        return
    end

    self.m_resultData = {}
    self.m_resultData.isPassed = arg.isPassed
    self.m_resultData.score = arg.addScore
    self.m_resultData.originScore = arg.lastMilestoneScore
    self.m_resultData.finalScore = arg.milestoneScore
    self.m_resultData.maxScore = 0
    self.m_resultData.isNewRecord = false

    local activity = GameInstance.player.racingDungeonSystem:GetActivityInfo()
    if activity then
        self.m_resultData.maxScore = ActivityUtils.GetRacingDungeonMilestoneMaxScore(activity.id)
        local succ, rankValue = GameInstance.player.activitySystem:TryGetActivityOwnRankValue(
            activity.id,
            ActivityUtils.RacingDungeonGetGameId(activity.id))
        self.m_resultData.isNewRecord = succ and self.m_resultData.score > rankValue
    end

    self.m_resultData.charInstIdList = {}
    local slots = GameInstance.player.squadManager.curSquad.slots
    for _, slot in pairs(slots) do
        local id = slot.charInstId
        table.insert(self.m_resultData.charInstIdList, id)
    end
end

CoinActivitySettlementCtrl._InitFakeData = HL.Method(HL.Any) << function(self, arg)
    local data = {}
    data.isFake = true
    data.score = 7000
    data.originScore = 3000
    data.finalScore = 10000
    data.maxScore = 10000
    data.isNewRecord = true

    local charIdList = { "chr_0030_zhuangfy", "chr_0003_endminf", "chr_0013_aglina" }
    data.charInstIdList = {}
    for _, charId in ipairs(charIdList) do
        local charInfo = CharInfoUtils.getPlayerCharInfoByTemplateId(charId, GEnums.CharType.Default)
        if charInfo then
            table.insert(data.charInstIdList, charInfo.instId)
        end
    end

    self.m_resultData = data
end

CoinActivitySettlementCtrl._BindUI = HL.Method() << function(self)
    self.view.confirmBtnLayout.onClick:AddListener(function()
        self:_OnConfirmBtnClick()
    end)


    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.teamList)
    self.view.teamList.onUpdateCell:AddListener(function(obj, index)
        local cell = self.m_getCellFunc(obj)
        local charInstId = self.m_resultData.charInstIdList[index + 1]
        self:_RefreshCharCell(cell, charInstId)
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

CoinActivitySettlementCtrl._OnConfirmBtnClick = HL.Method() << function(self)
    if self.m_resultData and self.m_resultData.isFake then
        self:PlayAnimationOut()
    else
        GameInstance.dungeonManager:LeaveDungeon()
    end
end


CoinActivitySettlementCtrl._RefreshUI = HL.Method() << function(self)
    self.view.mainState:SetState(self.m_resultData.isPassed and "Finish" or "BreakOff")

    self.view.leftContent.integralNumTxt.text = 0
    self.view.leftContent.integralNeedNumTxt.text = self:_GetScoreString(self.m_resultData.originScore, self.m_resultData.maxScore)
    local originNormalizedValue = self.m_resultData.originScore / self.m_resultData.maxScore
    self.view.leftContent.integralBar.fillAmount = originNormalizedValue
    if self.view.leftContent.newRecordNode then
        self.view.leftContent.newRecordNode.gameObject:SetActive(self.m_resultData.isNewRecord)
    end

    self.view.teamList:UpdateCount(4)

    self:_RefreshUnlockCountdown()


    self:_StartScoreAnim()
end

CoinActivitySettlementCtrl._PlayAudio = HL.Method() << function(self)
    if self.m_resultData.isPassed then
        AudioAdapter.PostEvent("Au_UI_Popup_Getcoin_Completed")
    else
        AudioAdapter.PostEvent("Au_UI_Popup_Getcoin_Failed")
    end
end

CoinActivitySettlementCtrl._GetScoreString = HL.Method(HL.Any, HL.Any).Return(HL.Any) << function(self, score, maxScore)
    local formatStr = Language.LUA_CoinActivitySettlementScore
    local str = string.format(formatStr, score, maxScore)
    return str
end

CoinActivitySettlementCtrl._StartScoreAnim = HL.Method() << function(self)
    local curve = self.view.config.SCORE_CURVE
    local start_time = self.view.config.SCORE_START_TIME
    local time = self.view.config.SCORE_TIME
    local end_time = time + start_time

    local start_value = self.m_resultData.originScore / self.m_resultData.maxScore
    local end_value = self.m_resultData.finalScore / self.m_resultData.maxScore

    local show_max = self.m_resultData.finalScore >= self.m_resultData.maxScore
    local timeCnt = 0

    self.m_scoreAnimLuaUpdateKey = LuaUpdate:Add("Tick", function(deltaTime)
        timeCnt = timeCnt + deltaTime
        if timeCnt < start_time then
            self.view.leftContent.integralNumTxt.text = 0
            self.view.leftContent.integralNeedNumTxt.text = self:_GetScoreString(self.m_resultData.originScore, self.m_resultData.maxScore)
            self.view.leftContent.integralBar.fillAmount = start_value
            self.view.leftContent.upperLimitLayout.gameObject:SetActive(false)
        elseif start_time <= timeCnt and timeCnt < end_time then
            local normalizedX = (timeCnt - start_time) / time
            local normalizedY = curve:Evaluate(normalizedX)
            local value = start_value + (end_value - start_value) * normalizedY
            self.view.leftContent.integralBar.fillAmount = value
            local integralNum = self.m_resultData.score * normalizedY
            self.view.leftContent.integralNumTxt.text = lume.round(integralNum)
            local score = self.m_resultData.originScore + (self.m_resultData.finalScore - self.m_resultData.originScore) * normalizedY
            score = lume.round(score)
            self.view.leftContent.integralNeedNumTxt.text = self:_GetScoreString(score, self.m_resultData.maxScore)
            self.view.leftContent.upperLimitLayout.gameObject:SetActive(false)
        else
            self.view.leftContent.integralBar.fillAmount = end_value
            self.view.leftContent.upperLimitLayout.gameObject:SetActive(show_max)
            self.view.leftContent.integralNumTxt.text = self.m_resultData.score
            self.view.leftContent.integralNeedNumTxt.text = self:_GetScoreString(self.m_resultData.finalScore, self.m_resultData.maxScore)
        end
    end)
end


CoinActivitySettlementCtrl._RefreshUnlockCountdown = HL.Method() << function(self)
    self.view.unlockNode.gameObject:SetActive(false)

    local activity = GameInstance.player.racingDungeonSystem:GetActivityInfo()
    if not activity then
        return
    end

    local haveCfg, multiStageCfg = Tables.activityConditionalMultiStageTable:TryGetValue(activity.id)
    if not haveCfg then
        return
    end

    local stages = {}
    for stageId, stageCfg in pairs(multiStageCfg.stageList) do
        table.insert(stages, { stageId = stageId, cfg = stageCfg })
    end
    table.sort(stages, function(a, b) return a.cfg.sortId < b.cfg.sortId end)

    local lockedStageData = nil
    for _, stage in ipairs(stages) do
        local _, stageData = activity.stageDataDict:TryGetValue(stage.stageId)
        local status = stageData and GEnums.ActivityConditionalStageState.__CastFrom(stageData.Status)
        if status == GEnums.ActivityConditionalStageState.Locked then
            lockedStageData = stageData
            break
        end
    end

    if not lockedStageData then
        return
    end

    self.view.unlockNode.gameObject:SetActive(true)
    local openTime = lockedStageData.OpenTimeTs

    self:_StartCoroutine(function()
        while true do
            local remaining = openTime - DateTimeUtils.GetCurrentTimestampBySeconds()
            if remaining <= 0 then
                self.view.unlockNode.gameObject:SetActive(false)
                break
            end
            self.view.unlockNode.unlockTimeTxt.text = UIUtils.getLeftTime(remaining)
            coroutine.wait(1)
        end
    end)
end

CoinActivitySettlementCtrl._RefreshCharCell = HL.Method(HL.Any, HL.Any) << function(self, cell, charInstId)

    local stateController = cell.stateController
    if charInstId == nil then
        stateController:SetState("empty")
        return
    end
    stateController:SetState("normal")
    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local templateId = charInfo.templateId
    local charWidget = cell.charHeadCellLongHpBar
    charWidget:InitCharFormationHeadCell({
        instId = charInstId,
        templateId = templateId,
        noHpBar = false,
    })
end






HL.Commit(CoinActivitySettlementCtrl)
