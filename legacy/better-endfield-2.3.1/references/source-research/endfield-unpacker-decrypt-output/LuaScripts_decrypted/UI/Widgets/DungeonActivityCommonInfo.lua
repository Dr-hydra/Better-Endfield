local DungeonCommonInfo = require_ex('UI/Widgets/DungeonCommonInfo')

DungeonActivityCommonInfo = HL.Class('DungeonActivityCommonInfo', DungeonCommonInfo)

DungeonActivityCommonInfo.m_missionId = HL.Field(HL.String) << ''


DungeonActivityCommonInfo._OnFirstTimeInit = HL.Override() << function(self)
    DungeonActivityCommonInfo.Super._OnFirstTimeInit(self)
    self.view.jumpBtn.onClick:RemoveAllListeners()
    self.view.jumpBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.Mission, { autoSelect = self.m_missionId })
    end)
end

DungeonActivityCommonInfo.m_activityId = HL.Field(HL.String) << ''

DungeonActivityCommonInfo.m_openCharFormationCallback = HL.Field(HL.Function)

DungeonActivityCommonInfo.InitDungeonActivityCommonInfo = HL.Method() << function(self)
    self:_FirstTimeInit()


end

DungeonActivityCommonInfo.SetOpenCharFormationCallback = HL.Method(HL.Function) << function(self, callback)
    self.m_openCharFormationCallback = callback
end

DungeonActivityCommonInfo._OpenCharFormation = HL.Override() << function(self)
    if self.m_openCharFormationCallback then
        self.m_openCharFormationCallback(self.m_dungeonId)
        return
    end
    DungeonActivityCommonInfo.Super._OpenCharFormation(self)
end

DungeonActivityCommonInfo.RefreshDungeonActivityCommonInfo = HL.Method(HL.String, HL.String) << function(self, dungeonId, activityId)
    DungeonActivityCommonInfo.Super.RefreshDungeonCommonInfo(self, dungeonId)

    self.m_activityId = activityId

    local _, dungeonCfg = Tables.dungeonTable:TryGetValue(self.m_dungeonId)
    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local activityDungeonStateCfg = Tables.activityDungeonState:GetValue(self.m_dungeonId)
    local success, stageData = activityData.stageDataDict:TryGetValue(activityDungeonStateCfg.activityStage)
    local stageCfg = Tables.activityConditionalMultiStageTable:GetValue(self.m_activityId)

    local rewardId = stageCfg.stageList[activityDungeonStateCfg.activityStage].rewardId
    local isStageCompleted = stageData.Status == GEnums.ActivityConditionalStageState.Completed:GetHashCode()
    local isStageRewarded = stageData.Status == GEnums.ActivityConditionalStageState.Rewarded:GetHashCode()

    local treatCompletedAsRewarded = isStageCompleted and string.isEmpty(rewardId)

    self.view.claimRewardsBtn.gameObject:SetActive(isStageCompleted and not treatCompletedAsRewarded)
    self.view.goToBattleBtn.gameObject:SetActive(stageData.Status == GEnums.ActivityConditionalStageState.Unlocked:GetHashCode())
    self.view.rechallengeBtn.gameObject:SetActive(isStageRewarded or treatCompletedAsRewarded)


    if not string.isEmpty(stageCfg.stageList[activityDungeonStateCfg.activityStage].rankRelatedId) then
        self.view.activityCommonRecord:InitActivityCommonRecord(self.m_activityId, stageCfg.stageList[activityDungeonStateCfg.activityStage].rankRelatedId)
        self.view.activityCommonRecord.gameObject:SetActive(true)
    else
        self.view.activityCommonRecord.gameObject:SetActive(false)
    end

    local gained = isStageRewarded or treatCompletedAsRewarded

    local rewards = {}

    local findReward, mainRewardData = Tables.rewardTable:TryGetValue(rewardId)
    if findReward then
        for _, itemBundle in pairs(mainRewardData.itemBundles) do
            local itemId = itemBundle.id
            local succ, itemCfg = Tables.itemTable:TryGetValue(itemId)
            if succ then
                table.insert(rewards, { id = itemId,
                                        count = itemBundle.count,
                                        gained = gained,
                                        typeTag = DungeonConst.DUNGEON_REWARD_TAG_STATE.First,
                                        sortId1 = itemCfg.sortId1,
                                        sortId2 = itemCfg.sortId2, })
            else
                logger.error("配置的RewardId中的ItemId在Item表中找不到", itemId, rewardId)
            end
        end
    elseif not string.isEmpty(rewardId) then
        logger.error("配置的首通奖励RewardId在Reward表中找不到：", rewardId)
    end



    if #rewards == 0 then
        rewards = DungeonUtils.genFirstPartRewardsInfo(self.m_dungeonId)
        for _, reward in ipairs(rewards) do
            reward.typeTag = DungeonConst.DUNGEON_REWARD_TAG_STATE.First
        end
    end

    self.view.rewardNode.gameObject:SetActive(#rewards > 0)
    self:SetRewardDetailsData(rewards)
    self.m_rewardCellCache:Refresh(#rewards, function(cell, luaIndex)
        local reward = rewards[luaIndex]
        cell.itemSmall:InitItem(reward, true)
        cell.itemSmall:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        cell.getNode.gameObject:SetActive(reward.gained == true)
        cell.lockNode.gameObject:SetActive(reward.locked == true)
        cell.lineNode.gameObject:SetActiveIfNecessary(false)
    end)
    self.m_missionId = ''
    self.view.lockedSpNode.gameObject:SetActive(false)
    self.view.jumpBtn.gameObject:SetActive(false)
    self.view.lockedNode.gameObject:SetActive(false)

    if stageData.Status == GEnums.ActivityConditionalStageState.Locked:GetHashCode() then
        local tuple = GameInstance.player.activitySystem:GetStageLockDisplayConditionInfo(activityDungeonStateCfg.activityStage)
        local desc, conditionType, questId = tuple.Item1, tuple.Item2, tuple.Item3
        if not string.isEmpty(desc) then
            if conditionType == GEnums.ConditionType.MissionStateEqual:GetHashCode() or conditionType == GEnums.ConditionType.QuestStateEqual:GetHashCode() then
                self.m_missionId = GameInstance.player.mission:GetMissionIdByQuestId(questId or '')
                self.view.jumpTxt.text = desc
            else
                self.view.lockedTxt.text = desc
                self.m_missionId = ''
            end
        end
        self.view.jumpBtn.gameObject:SetActive(string.isEmpty(self.m_missionId) == false)
        self.view.lockedNode.gameObject:SetActive(string.isEmpty(self.m_missionId))
    end
end

HL.Commit(DungeonActivityCommonInfo)
return DungeonActivityCommonInfo

