
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.AchievementDetailPopup

AchievementDetailPopupCtrl = HL.Class('AchievementDetailPopupCtrl', uiCtrl.UICtrl)






AchievementDetailPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}

AchievementDetailPopupCtrl.m_viewModel = HL.Field(HL.Any) << nil

AchievementDetailPopupCtrl.m_levelCellCache = HL.Field(HL.Any) << nil

local LEVEL_CONFIGS = {
    [2] = {
        title = "ui_achv_list_evolute_method_2",
    },
    [3] = {
        title = "ui_achv_list_evolute_method_3",
    }
}


AchievementDetailPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitViews()
    if arg == nil then
        return
    end
    local achievementId = arg.achievementId
    if string.isEmpty(achievementId) then
        return
    end
    self:_LoadData(achievementId)
    self:_RenderViews()
end








AchievementDetailPopupCtrl._InitViews = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.view.closeBtn.onClick:RemoveAllListeners()
    self.view.closeBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.m_levelCellCache = UIUtils.genCellCache(self.view.levelCell)
end

AchievementDetailPopupCtrl._LoadData = HL.Method(HL.String) << function(self, achievementId)
    self.m_viewModel = nil
    local succ, achievementData = Tables.achievementTable:TryGetValue(achievementId)
    if not succ then
        return
    end
    local achievementSystem = GameInstance.player.achievementSystem
    local ok, playerAchievement = achievementSystem.achievementData.achievementInfos:TryGetValue(achievementId)
    local achievementLevel = 0
    local achievementPlated = false
    local achievementObtainTs = 0
    if ok then
        achievementLevel = playerAchievement.level
        achievementPlated = playerAchievement.isPlated
        achievementObtainTs = playerAchievement.obtainTs
    end
    local _, achievementTimeInfo = achievementSystem.achievementData.achievementTimeInfos:TryGetValue(achievementId)
    local isGained = achievementLevel >= achievementData.initLevel
    local desc = ''
    local hasInitLevelInfo, initLevelInfo = achievementData.levelInfos:TryGetValue(achievementData.initLevel)
    if isGained then
        local hasLevelInfo, levelInfo = achievementData.levelInfos:TryGetValue(achievementLevel)
        if hasLevelInfo then
            desc = UIUtils.resolveTextCinematic(levelInfo.completeDesc)
        end
    else
        if hasInitLevelInfo then
            desc = UIUtils.resolveTextCinematic(initLevelInfo.completeDesc)
        end
    end
    local maxLevel = achievementData.initLevel
    for i, levelInfo in pairs(achievementData.levelInfos) do
        maxLevel = math.max(maxLevel, levelInfo.achieveLevel)
    end
    self.m_viewModel = {
        achievementId = achievementId,
        name = achievementData.name,
        desc = desc,
        timeInfo = achievementTimeInfo,
        isGained = isGained,
        canPlate = achievementData.canBePlated and not achievementPlated,
        canUpgrade = achievementData.canBeUpgraded and achievementLevel < maxLevel,
        isRare = achievementData.applyRareEffect,
        isRareObtain = achievementLevel >= Tables.achievementConst.levelDisplayEffect,
        obtainTs = achievementObtainTs,
        levelInfos = {},
        level = achievementLevel,
        initLevel = achievementData.initLevel,
        maxLevel = maxLevel,
        isPlated = achievementPlated,
        isSpecialProgress = achievementData.specialProgress,
    }
    if hasInitLevelInfo then
        self:_LoadInitLevel(playerAchievement, initLevelInfo)
    end
    if achievementData.canBeUpgraded then
        self:_LoadUpgradedInfo(playerAchievement, achievementData)
    end
    if achievementData.canBePlated then
        self:_LoadPlatedInfo(playerAchievement, achievementData)
    end
end

AchievementDetailPopupCtrl._LoadInitLevel = HL.Method(HL.Any, HL.Any) << function(self, playerAchievement, initLevelInfo)
    local isFinished = self.m_viewModel.level >= initLevelInfo.achieveLevel
    local levelViewModel = {
        isInit = true,
        isPlate = false,
        isFinished = isFinished,
        title = I18nUtils.GetText("ui_achv_list_obtained_method"),
        conditions = self:_GetConditionDesc(initLevelInfo.conditions, playerAchievement, not isFinished)
    }
    table.insert(self.m_viewModel.levelInfos, levelViewModel)
end

AchievementDetailPopupCtrl._LoadUpgradedInfo = HL.Method(HL.Any, HL.Any) << function(self, playerAchievement, achievementData)
    local overrideProgress = -1
    if self.m_viewModel.level < self.m_viewModel.maxLevel then
        overrideProgress = 0
        local currFinishingLevel = self.m_viewModel.initLevel
        if self.m_viewModel.isGained then
            currFinishingLevel = self.m_viewModel.level + 1
        end
        local succ, levelInfo = achievementData.levelInfos:TryGetValue(currFinishingLevel)
        if succ then
            for _, condition in pairs(levelInfo.conditions) do
                if playerAchievement ~= nil and playerAchievement.condition ~= nil then
                    local suc, playerConditionVal = playerAchievement.condition.conditionVals:TryGetValue(condition.conditionId)
                    if suc then
                        overrideProgress = overrideProgress + playerConditionVal
                    end
                end
            end
        end
    end
    local upgradeModels = {}
    for _, levelInfo in pairs(achievementData.levelInfos) do
        local isInit = levelInfo.achieveLevel == achievementData.initLevel
        if not isInit then
            local isFinished = self.m_viewModel.level >= levelInfo.achieveLevel
            local config = LEVEL_CONFIGS[levelInfo.achieveLevel]
            local conditionDesc = ''
            if not isFinished and overrideProgress > 0 then
                conditionDesc = self:_GetConditionDesc(levelInfo.conditions, playerAchievement, not isFinished, overrideProgress)
            else
                conditionDesc = self:_GetConditionDesc(levelInfo.conditions, playerAchievement, not isFinished)
            end
            local levelViewModel = {
                level = levelInfo.achieveLevel,
                isInit = isInit,
                isPlate = false,
                isFinished = isFinished,
                title = config == nil and '' or I18nUtils.GetText(config.title),
                conditions = conditionDesc
            }
            table.insert(upgradeModels, levelViewModel)
        end
    end
    table.sort(upgradeModels, Utils.genSortFunction({"level"}, true))
    for _, model in ipairs(upgradeModels) do
        table.insert(self.m_viewModel.levelInfos, model)
    end
end

AchievementDetailPopupCtrl._LoadPlatedInfo = HL.Method(HL.Any, HL.Any) << function(self, playerAchievement, achievementData)
    local isObtained = self.m_viewModel.level >= achievementData.initLevel
    local showProgress = isObtained and not self.m_viewModel.isPlated
    local plateViewModel = {
        isInit = false,
        isPlate = true,
        isFinished = self.m_viewModel.isPlated,
        title = I18nUtils.GetText("ui_achv_list_plated_method"),
        conditions = self:_GetConditionDesc(achievementData.plateConditions, playerAchievement, showProgress)
    }
    table.insert(self.m_viewModel.levelInfos, plateViewModel)
end

AchievementDetailPopupCtrl._GetConditionDesc = HL.Method(HL.Any, HL.Any, HL.Boolean, HL.Opt(HL.Number)).Return(HL.String)
    << function(self, conditions, playerAchievement, showProgress, overrideProgress)
    if conditions == nil or conditions[0] == nil then
        return ''
    end
    local conditionDesc = conditions[0].desc
    if not showProgress then
        return conditionDesc
    end
    local progress = 0
    local target = 0
    if self.m_viewModel.isSpecialProgress then
        progress = 0
        target = 1
    else
        for _, condition in pairs(conditions) do
            if playerAchievement ~= nil and playerAchievement.condition ~= nil then
                local suc, playerConditionVal = playerAchievement.condition.conditionVals:TryGetValue(condition.conditionId)
                if suc then
                    progress = progress + playerConditionVal
                end
            end
            target = target + condition.progressToCompare
        end
    end
    if overrideProgress ~= nil and not self.m_viewModel.isSpecialProgress then
        progress = overrideProgress
    end
    local resultDesc = conditionDesc .. string.format(Language.LUA_ACHIEVEMENT_CONDITION_TARGET,
        progress, target)
    return resultDesc
end

AchievementDetailPopupCtrl._RenderViews = HL.Method() << function(self)
    if self.m_viewModel == nil then
        return
    end
    self.view.name.text = self.m_viewModel.name
    self.view.desc.text = self.m_viewModel.desc
    self.view.stateCtrl:SetState(self.m_viewModel.isGained and "Acquired" or "Unattained")
    if self.m_viewModel.isGained then
        self.view.obtainTxt.text = Utils.timestampToDateYMD(self.m_viewModel.obtainTs)
    end
    if self.m_viewModel.isRare then
        if self.m_viewModel.isRareObtain then
            self.view.stateCtrl:SetState("QualifyPossess")
        else
            self.view.stateCtrl:SetState("QualifyNotPossess")
        end
    else
        self.view.stateCtrl:SetState("QualifyNull")
    end
    local isLimit = self.m_viewModel.timeInfo ~= nil
    local hasValidDesc = false
    if isLimit then
        local currTs = DateTimeUtils.GetCurrentTimestampBySeconds()
        local openTime = self.m_viewModel.timeInfo.openTime
        local closeTime = self.m_viewModel.timeInfo.closeTime
        if currTs < openTime then
            hasValidDesc = true
            self.view.timeLimitTxt.text = I18nUtils.GetText("ui_achv_list_can_not_obtain")
        else
            if closeTime > 0 then
                hasValidDesc = true
                if currTs < closeTime then
                    self.view.timeLimitTxt.text = string.format(I18nUtils.GetText("ui_achv_list_obtain_close"), UIUtils.getShortLeftTime(closeTime - currTs))
                else
                    self.view.timeLimitTxt.text = I18nUtils.GetText("ui_achv_list_can_not_obtain")
                end
            end
        end
    end
    self.view.timeLimit.gameObject:SetActive(isLimit and hasValidDesc)
    self.m_levelCellCache:Refresh(#self.m_viewModel.levelInfos, function(cell, luaIndex)
        self:_RenderLevel(cell, luaIndex)
    end)
    if self.m_viewModel.isGained then
        local medalBundle = {
            achievementId = self.m_viewModel.achievementId,
            level = self.m_viewModel.level,
            isPlated = self.m_viewModel.isPlated,
            isRare = self.m_viewModel.isRare
        }
        self.view.medal:InitMedal(medalBundle)
    end
end

AchievementDetailPopupCtrl._RenderLevel = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local levelInfo = self.m_viewModel.levelInfos[luaIndex]
    if levelInfo == nil then
        return
    end
    cell.conditionCell.desc.text = levelInfo.conditions
    if levelInfo.isInit then
        cell.stateCtrl:SetState("GetCondition")
    else
        if levelInfo.isPlate then
            cell.stateCtrl:SetState("Cladding")
        else
            cell.stateCtrl:SetState("Reforge")
        end
    end
    cell.stateCtrl:SetState(self.m_viewModel.isGained and (levelInfo.isFinished and "Finish" or "UnFinished") or "Unattained")
    cell.title.text = levelInfo.title
end

AchievementDetailPopupCtrl.GetRecoverStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    if self.m_viewModel == nil or string.isEmpty(self.m_viewModel.achievementId) then
        return
    end
    return {
        achievementId = self.m_viewModel.achievementId,
    }
end

HL.Commit(AchievementDetailPopupCtrl)
