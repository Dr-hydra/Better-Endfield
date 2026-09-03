
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityReflowFormal

ActivityReflowFormalCtrl = HL.Class('ActivityReflowFormalCtrl', uiCtrl.UICtrl)

ActivityReflowFormalCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_REFLOW_MILESTONE_UPDATE] = '_OnTabUpdate',
    [MessageConst.ON_ACTIVITY_UPDATED] = '_OnTabUpdate',
    [MessageConst.ON_BATTLE_PASS_TASK_UPDATE] = '_OnTabUpdate',
    [MessageConst.ON_REFLOW_REWARD_GAIN] = '_OnTabUpdate',
    [MessageConst.ON_SPECIFIED_SURVEY_STATE_CHANGE] = '_OnTabUpdate',
    [MessageConst.SHOW_REFLOW_ONE_TIME_REWARD] = '_ShowReward',
}

ActivityReflowFormalCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityReflowFormalCtrl.m_tabInfos = HL.Field(HL.Table)
ActivityReflowFormalCtrl.m_reflowCfg = HL.Field(HL.Any)
ActivityReflowFormalCtrl.m_milestoneData = HL.Field(HL.Any)

ActivityReflowFormalCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self:_InitData(args)
    self:_InitUI()
end

ActivityReflowFormalCtrl._InitData = HL.Method(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local _, reflowCfg = Tables.activityReflowTable:TryGetValue(self.m_activityId)
    local milestoneId = reflowCfg.activityMilestoneId
    local _, milestoneData = activity.milestones:TryGetValue(milestoneId)
    self.m_reflowCfg = reflowCfg
    self.m_milestoneData = milestoneData

    self.m_tabInfos = {

        {
            node = self.view.entranceNode.signinBtn,
            redDot = "ActivityReflowSignin",
            clickCallback = function()
                PhaseManager:OpenPhase(PhaseId.ReflowFormalReconnect, {
                    activityId = self.m_activityId,
                    panelId = PanelId.ActivityReflowSignin,
                })
                ActivityUtils.GameEventLogActivityVisit(self.m_activityId, "ReflowSigninButton", "visit_reflow_signin")
            end,
            getHighestRarityItemIconFunc = function()

                local itemBundles = {}
                local _, signinRewardCfg = Tables.checkInRewardTable:TryGetValue(self.m_activityId)
                for _, rewardCfg in pairs(signinRewardCfg.stageList) do
                     local dayIndex = rewardCfg.day
                     if not activity.rewardDays:Contains(dayIndex) then
                         local rewardId = rewardCfg.rewardId
                         local _, rewardTableData = Tables.rewardTable:TryGetValue(rewardId)
                            if rewardTableData then
                                for _, v in pairs(rewardTableData.itemBundles) do
                                    table.insert(itemBundles, v)
                                end
                            end
                    end
                end
                itemBundles = UIUtils.convertLuaRewardItemBundlesToDataList(itemBundles, false)
                if itemBundles[1] then
                    local _, itemData = Tables.itemTable:TryGetValue(itemBundles[1].id)
                    return itemData.iconId
                end
                return nil
            end,
            checkCompleteFunc = function()
                local _, checkInCfg = Tables.checkInRewardTable:TryGetValue(self.m_activityId)
                return activity.rewardDays.Count == #checkInCfg.stageList
            end,
        },

        {
            node = self.view.entranceNode.taskBtn,
            redDot = "ActivityReflowTask",
            clickCallback = function()
                PhaseManager:OpenPhase(PhaseId.ReflowFormalReconnect, {
                    activityId = self.m_activityId,
                    panelId = PanelId.ReflowFormalReconnectTask,
                })
                ActivityUtils.GameEventLogActivityVisit(self.m_activityId, "ReflowTaskButton", "visit_reflow_task")
            end,
            getHighestRarityItemIconFunc = function()

                local itemBundles = {}
                local _, cfg = Tables.activityConditionalMultiStageTaskConfigTable:TryGetValue(self.m_activityId)
                for _, stage in pairs(self.m_milestoneData.stages) do

                     if not stage.rewarded then
                         local _, stageCfg = self.m_reflowCfg.stages:TryGetValue(stage.id)
                         local rewardId = stageCfg.rewardId
                         local _, rewardTableData = Tables.rewardTable:TryGetValue(rewardId)
                         if rewardTableData then
                            for _, v in pairs(rewardTableData.itemBundles) do
                                table.insert(itemBundles, v)
                            end
                         end
                     end
                end
                for _, taskCfg in pairs(cfg.TaskConfigMap) do
                     local taskId = taskCfg.taskId
                     local _, taskData = self.m_milestoneData.tasks:TryGetValue(taskId)

                     if GEnums.ActivityConditionalTaskState.__CastFrom(taskData.status) ~= GEnums.ActivityConditionalTaskState.Rewarded then
                         local rewardId = taskCfg.rewardId
                         local _, rewardTableData = Tables.rewardTable:TryGetValue(rewardId)
                         if rewardTableData then
                            for _, v in pairs(rewardTableData.itemBundles) do
                                table.insert(itemBundles, v)
                            end
                         end
                     end
                end
                itemBundles = UIUtils.convertLuaRewardItemBundlesToDataList(itemBundles, false)
                if itemBundles[1] then
                    local _, itemData = Tables.itemTable:TryGetValue(itemBundles[1].id)
                    return itemData.iconId
                end
                return nil
            end,
            checkCompleteFunc = function()
                local milestoneId = self.m_reflowCfg.activityMilestoneId
                local _, milestone = activity.milestones:TryGetValue(milestoneId)

                for _, taskData in pairs(milestone.tasks) do
                    if taskData and GEnums.ActivityConditionalTaskState.__CastFrom(taskData.status) ~= GEnums.ActivityConditionalTaskState.Rewarded then
                        return false
                    end
                end

                for _, stageData in pairs(milestone.stages) do
                    if stageData and not stageData.rewarded then
                        return false
                    end
                end
                return true
            end,
        },

        {
            node = self.view.entranceNode.passBtn,
            redDot = "ActivityReflowBP",
            clickCallback = function()
                if Utils.isSystemUnlocked(GEnums.UnlockSystemType.BPSystem) then
                    PhaseManager:GoToPhase(PhaseId.BattlePass, {
                        panelId = 'BattlePassTask',
                    })
                else
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_REFLOW_BP_LOCKED)
                end
                ActivityUtils.GameEventLogActivityVisit(self.m_activityId, "ReflowBPButton", "visit_reflow_battlePass")
            end,
            checkCompleteFunc = function()
                if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.BPSystem) or not PhaseManager:CheckCanOpenPhase(PhaseId.BattlePass) then
                    return false
                end
                local taskGroupId = self.m_reflowCfg.reflowCfg.bpTaskGroupId
                return GameInstance.player.battlePassSystem:HasGroupAllRewarded(taskGroupId)
            end,
        },

        {
            node = self.view.entranceNode.questionnaireBtn,
            redDot = "ActivityReflowQuestionnaire",
            clickCallback = function()
                ActivityUtils.setReflowQuestionnaireRead(self.m_activityId)
                UIManager:Open(PanelId.ReflowFormalQuestionnairePopup, {activityId = self.m_activityId})
                ActivityUtils.GameEventLogActivityVisit(self.m_activityId, "ReflowQuestionnaire", "visit_reflow_questionnaire")
            end,
            checkCompleteFunc = function()
                for _, questionnaire in pairs(self.m_reflowCfg.questionnaires) do
                    local _, data = activity.questionnaires:TryGetValue(questionnaire.questionnaireTriggerId)
                    local isLocked = GEnums.ActivityConditionalTaskState.__CastFrom(data.status) ~= GEnums.ActivityConditionalTaskState.Completed
                    local questionnaireId = data.hashId
                    if questionnaireId and not string.isEmpty(questionnaireId)then
                        local _, isAvailable = CS.Beyond.SDK.SDKAccountUtils.TryGetSpecifiedSurveyState(questionnaireId)

                        if isLocked or isAvailable then
                            return false
                        end

                        if not isAvailable and not data.isCompleted then
                            GameInstance.player.activitySystem:SendGainReflowMilestoneReward(self.m_activityId, questionnaireId, questionnaire.questionnaireTriggerId)
                        end
                    else
                        return false
                    end
                end
                return true
            end,
        },

        {
            node = self.view.entranceNode.reconnectBtn,
            redDot = "ActivityReflowReconnectReward",
            clickCallback = function()

                if not activity.oneTimeRewardReceived then
                    GameInstance.player.activitySystem:SendGainReflowOneTimeReward(self.m_activityId)
                else
                    UIManager:Open(PanelId.ReflowFormalWelcomeBack, {activityId = self.m_activityId})
                end
                ActivityUtils.GameEventLogActivityVisit(self.m_activityId, "ReflowReward", "visit_reflow_reward")
            end,
            checkCompleteFunc = function()
                return activity.oneTimeRewardReceived
            end,
        }
    }

    self:_InitQuestionnaireData()
end

ActivityReflowFormalCtrl._InitUI = HL.Method() << function(self)
    for _, info in pairs(self.m_tabInfos) do
        local node = info.node
        node.button.onClick:AddListener(info.clickCallback)
        node.redDot:InitRedDot(info.redDot, self.m_activityId)

        local hasCompleted = info.checkCompleteFunc()
        node.state:SetState(hasCompleted and "Finish" or "Normal")
        if hasCompleted then
            node.redDot:Stop()
        end

        if not hasCompleted and info.getHighestRarityItemIconFunc then
            local icon = info.getHighestRarityItemIconFunc()
            if icon then
                node.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, icon)
            end
        end
        if DeviceInfo.usingController and node.button.enableControllerNavi then
            node.keyHint.gameObject:SetActive(false)
            node.button.onIsNaviTargetChanged = function(isTarget)
                node.keyHint.gameObject:SetActive(isTarget)
            end
        end
    end


    if DeviceInfo.usingController then
        local viewBindingId = self:BindInputPlayerAction("common_view_item", function()
            self:OnActivityCenterNaviFailed()
        end)


        self.view.entranceNode.inputGroup.internalEnabled = false
        self.view.rightNaviGroup.onIsTopLayerChanged:AddListener(function(active)
            self.view.entranceNode.inputGroup.internalEnabled = active
            InputManagerInst:ToggleBinding(viewBindingId, not active)
        end)
    end
end

ActivityReflowFormalCtrl._OnTabUpdate = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    if arg then
        local activityId = unpack(arg)
        if activityId ~= self.m_activityId then
            return
        end
    end

    for _, info in pairs(self.m_tabInfos) do
        local node = info.node
        local hasCompleted = info.checkCompleteFunc()
        node.state:SetState(hasCompleted and "Finish" or "Normal")

        if not hasCompleted and info.getHighestRarityItemIconFunc then
            local icon = info.getHighestRarityItemIconFunc()
            if icon then
                node.itemIcon:LoadSprite(UIConst.UI_SPRITE_ITEM, icon)
            end
        end
    end
end

ActivityReflowFormalCtrl._InitQuestionnaireData = HL.Method() << function(self)

    local ids = {}
    local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    for _, questionnaire in pairs(self.m_reflowCfg.questionnaires) do
        local _, data = activity.questionnaires:TryGetValue(questionnaire.questionnaireTriggerId)
        local id = data.hashId
        if not string.isEmpty(id)then
            table.insert(ids, string.format("%q",id))
        end
    end
    local surveyIds = table.concat(ids,",")
    CS.Beyond.SDK.SDKAccountUtils.QuerySpecifiedSurveyState(string.format("[%s]",surveyIds))
end

ActivityReflowFormalCtrl.OnActivityCenterNaviFailed = HL.Method() << function(self)
    self:SetNaviTarget(self.m_tabInfos[1].node.button)
end

ActivityReflowFormalCtrl._ShowReward = HL.Method(HL.Any) << function(self, arg)
    local _, items, chars = unpack(arg)
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        items = items,
        chars = chars,
        showHint = true,
    })
end

ActivityReflowFormalCtrl.OnClose = HL.Override() << function(self)



    local isOpen, ctrl = UIManager:IsOpen(PanelId.ReflowFormalWelcomeBack)
    if isOpen then
        ctrl:Close()
    end

    local isOpen, ctrl = UIManager:IsOpen(PanelId.ReflowFormalQuestionnairePopup)
    if isOpen then
        ctrl:Close()
    end
end

HL.Commit(ActivityReflowFormalCtrl)
