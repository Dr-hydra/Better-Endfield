local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ReflowFormalQuestionnairePopup

ReflowFormalQuestionnairePopupCtrl = HL.Class('ReflowFormalQuestionnairePopupCtrl', uiCtrl.UICtrl)






ReflowFormalQuestionnairePopupCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_REFLOW_QUESTIONNAIRE_UPDATE] = '_OnQuestionnaireUpdate',
    [MessageConst.ON_SPECIFIED_SURVEY_STATE_CHANGE] = '_OnQuestionnaireStateChange',
}

ReflowFormalQuestionnairePopupCtrl.m_activityId = HL.Field(HL.String) << ''

ReflowFormalQuestionnairePopupCtrl.m_questionnaireDataDict = HL.Field(HL.Any)

ReflowFormalQuestionnairePopupCtrl.m_questionnaireCfg = HL.Field(HL.Any)

ReflowFormalQuestionnairePopupCtrl.m_genCellFunction = HL.Field(HL.Function)

ReflowFormalQuestionnairePopupCtrl.m_rewardCacheTable = HL.Field(HL.Table)


ReflowFormalQuestionnairePopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitData(arg)
    self:_InitUI()
    self:_RefreshAllUIs(true)
    ActivityUtils.GameEventLogReflowQuestionnaire(arg.activityId, "visit_questionnaire_page")
end

ReflowFormalQuestionnairePopupCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    self.m_activityId = arg.activityId
    local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    self.m_questionnaireDataDict = activity.questionnaires
    self.m_questionnaireCfg = {}
    local _, reflowCfg = Tables.activityReflowTable:TryGetValue(self.m_activityId)
    for _, questionnaire in pairs(reflowCfg.questionnaires) do
        table.insert(self.m_questionnaireCfg, questionnaire)
    end
    table.sort(self.m_questionnaireCfg, Utils.genSortFunction({ "sortId" }, true))
    self.m_rewardCacheTable = {}
end

ReflowFormalQuestionnairePopupCtrl._InitUI = HL.Method() << function(self)
    self.view.closeButton.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.listScrollList.onUpdateCell:AddListener(function(go, csIndex)
        self:_OnUpdateCell(go, csIndex)
    end)
    self.m_genCellFunction = UIUtils.genCachedCellFunction(self.view.listScrollList)
end

ReflowFormalQuestionnairePopupCtrl._RefreshAllUIs = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    if isInit then
        self.view.listScrollList:UpdateCount(#self.m_questionnaireCfg)
        if DeviceInfo.usingController then
            self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
            self:SetNaviTarget(self.m_genCellFunction(1).naviDecorator)
        end
    else
        self.view.listScrollList:UpdateShowingCells(function(csIndex, go)
            self:_OnUpdateCell(go, csIndex)
        end)
    end
end

ReflowFormalQuestionnairePopupCtrl._OnUpdateCell = HL.Method(HL.Any, HL.Number) << function(self, go, csIndex)
    local cell = self.m_genCellFunction(go)
    local index = LuaIndex(csIndex)
    local cfg = self.m_questionnaireCfg[index]
    local triggerId = cfg.questionnaireTriggerId
    local _, data = self.m_questionnaireDataDict:TryGetValue(triggerId)
    local questionnaireId = data.hashId


    cell.titleTxt.text = cfg.title
    local isLocked = GEnums.ActivityConditionalTaskState.__CastFrom(data.status) ~= GEnums.ActivityConditionalTaskState.Completed
    local isCompleted = false
    if questionnaireId and not string.isEmpty(questionnaireId) then
        local _, isAvailable = CS.Beyond.SDK.SDKAccountUtils.TryGetSpecifiedSurveyState(questionnaireId)
        isCompleted = not isLocked and not isAvailable

        if isCompleted and not data.isCompleted then
            GameInstance.player.activitySystem:SendGainReflowMilestoneReward(self.m_activityId, questionnaireId, triggerId)
            ActivityUtils.GameEventLogReflowQuestionnaire(self.m_activityId, "check_questionnaire_status", questionnaireId, cfg.title, index, "finished")
        end
        cell.goBtn.onClick:RemoveAllListeners()
        cell.goBtn.onClick:AddListener(function()

            local startTime = DateTimeUtils.GetCurrentTimestampBySeconds()
            ActivityUtils.GameEventLogReflowQuestionnaire(self.m_activityId, "start_questionnaire", questionnaireId, cfg.title, index)
            CS.Beyond.Gameplay.Audio.Utils.AudioControlUtil.Webview.SetMute(true)

            local url = self:_GetQuestionnaireFullUrl(questionnaireId)
            CS.Beyond.SDK.HGBrowserBridge.OpenBrowser(url, nil, function(res)
                CS.Beyond.Gameplay.Audio.Utils.AudioControlUtil.Webview.SetMute(false)
                CS.Beyond.SDK.SDKAccountUtils.QuerySpecifiedSurveyState(string.format("[%q]",questionnaireId))
                local costTime = DateTimeUtils.GetCurrentTimestampBySeconds() - startTime
                ActivityUtils.GameEventLogReflowQuestionnaire(self.m_activityId, "finish_questionnaire", questionnaireId, cfg.title, index, "unknown", costTime)
            end)
        end)
    end
    if isLocked then
        local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
        if activity then

            cell.countDownText:InitCountDownText(activity.endTime - cfg.unlockTimeOffset)
        end
    end
    cell.nodeState:SetState(isCompleted and "Finish" or (isLocked and "Lock" or "Go"))


    local rewardId = cfg.rewardId
    local rewardBundles = UIUtils.getRewardItems(rewardId)
    local rewardCellCache = self.m_rewardCacheTable[index] or UIUtils.genCellCache(cell.itemReward)
    self.m_rewardCacheTable[index] = rewardCellCache
    rewardCellCache:Refresh(#rewardBundles, function(innerCell, innerIndex)
        innerCell.view.rewardedCover.gameObject:SetActive(isCompleted)
        local reward = {
            id = rewardBundles[innerIndex].id,
            count = rewardBundles[innerIndex].count,
            forceHidePotentialStar = true,
        }
        innerCell:InitItem(reward, function()
            innerCell:ShowTips()
        end)
        innerCell:SetExtraInfo({
            tipsPosTransform = innerCell.view.content,
            isSideTips = true,
        })
    end)

    if DeviceInfo.usingController then
        cell.rewardsKeyHint.gameObject:SetActive(false)
        cell.naviDecorator.onIsNaviTargetChanged = function(isTarget)
            cell.rewardsKeyHint.gameObject:SetActive(isTarget)
        end
    end
end

ReflowFormalQuestionnairePopupCtrl._OnQuestionnaireUpdate = HL.Method(HL.Any) << function(self, arg)
    local activityId = unpack(arg)
    if activityId ~= self.m_activityId then
        return
    end
    self:_RefreshAllUIs()
end

ReflowFormalQuestionnairePopupCtrl._OnQuestionnaireStateChange = HL.Method() << function(self)
    self:_RefreshAllUIs()
end

ReflowFormalQuestionnairePopupCtrl._GetQuestionnaireFullUrl = HL.Method(HL.String).Return(HL.String) << function(self, hashId)
    local serverType = Utils.getServerAreaType()
    if serverType == GEnums.ServerAreaType.None or serverType == GEnums.ServerAreaType.Max then
        logger.error("Reflow Questionnaire Open Error: Invalid Area Type")
        return ""
    end
    if serverType == GEnums.ServerAreaType.China then
        return Tables.activityConst.ReflowQuestionnaireUrlPremixChina .. hashId
    else
        return Tables.activityConst.ReflowQuestionnaireUrlPremixOverseas .. hashId
    end
end

HL.Commit(ReflowFormalQuestionnairePopupCtrl)
