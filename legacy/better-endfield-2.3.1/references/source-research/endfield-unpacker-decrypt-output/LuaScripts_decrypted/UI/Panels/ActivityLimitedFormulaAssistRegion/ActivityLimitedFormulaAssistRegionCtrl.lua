
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityLimitedFormulaAssistRegion

ActivityLimitedFormulaAssistRegionCtrl = HL.Class('ActivityLimitedFormulaAssistRegionCtrl', uiCtrl.UICtrl)

local ACHIEVEMENT_ID = "achv_event_formula"
local START_STAGE_INDEX = 1
local FINAL_STAGE_INDEX = 3
local STAGE_2_COMPLETE_TARGET_NUMBER = 800000
local STAGE_STATE_TO_POINT_STATE_MAP = {
    [GEnums.ActivityConditionalStageState.Locked] = "None",
    [GEnums.ActivityConditionalStageState.Unlocked] = "Doing",
    [GEnums.ActivityConditionalStageState.Completed] = "Done",
    [GEnums.ActivityConditionalStageState.Rewarded] = "Done",
}

ActivityLimitedFormulaAssistRegionCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ACTIVITY_UPDATED] = '_OnActivityUpdated',
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE] = '_OnActivityUpdated',
    [MessageConst.ON_ACHIEVEMENT_UPDATE] = '_OnAchievementUpdated',
    [MessageConst.ON_WALLET_CHANGED] = '_OnWalletChanged',
}

ActivityLimitedFormulaAssistRegionCtrl.m_activityId = HL.Field(HL.String) << ''

ActivityLimitedFormulaAssistRegionCtrl.m_activityCfgData = HL.Field(HL.Any)

ActivityLimitedFormulaAssistRegionCtrl.m_achievementCfgData = HL.Field(HL.Any)

ActivityLimitedFormulaAssistRegionCtrl.m_achievementState = HL.Field(HL.String) << ""

ActivityLimitedFormulaAssistRegionCtrl.m_activityData = HL.Field(CS.Beyond.Gameplay.ActivityLimitedFormula)

ActivityLimitedFormulaAssistRegionCtrl.m_stageList = HL.Field(HL.Table)

ActivityLimitedFormulaAssistRegionCtrl.m_curStageIndex = HL.Field(HL.Number) << 0

ActivityLimitedFormulaAssistRegionCtrl.m_curStageState = HL.Field(GEnums.ActivityConditionalStageState)

ActivityLimitedFormulaAssistRegionCtrl.m_isEndStage = HL.Field(HL.Boolean) << false

ActivityLimitedFormulaAssistRegionCtrl.m_completeTitleCor = HL.Field(HL.Thread)

ActivityLimitedFormulaAssistRegionCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_activityId = args.activityId
    if not Tables.activityLimitedFormulaTable:ContainsKey(self.m_activityId) then
        logger.error("限时配方活动数据不存在：", self.m_activityId)
    end
    self.m_activityCfgData = Tables.activityLimitedFormulaTable:GetValue(self.m_activityId)
    self.m_achievementCfgData = Tables.achievementTable[ACHIEVEMENT_ID]
    self.m_activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)

    local stageData = Tables.activityConditionalMultiStageTable:GetValue(self.m_activityId)
    self.m_stageList = {}
    for id, data in pairs(stageData.stageList) do
        local _, stageInfo = self.m_activityData.stageDataDict:TryGetValue(id)
        table.insert(self.m_stageList, {
            id = id,
            sort = data.sortId,
            startTime = stageInfo.OpenTimeTs
        })
    end
    table.sort(self.m_stageList, Utils.genSortFunction({ "sort" }, true))

    self:_UpdateActivityData()

    args.skipTimeCountDown = true
    self.view.activityCommonInfo:InitActivityCommonInfo(args)
    self.view.activityCommonInfo.view.gotoNode.btnDetail.onClick:RemoveAllListeners()
    self.view.activityCommonInfo.view.gotoNode.btnDetail.onClick:AddListener(function()
        if self.m_isEndStage then
            return
        end
        local jumpId = ""
        if self.m_curStageState == GEnums.ActivityConditionalStageState.Unlocked then
            jumpId = self.m_activityCfgData.activityStageList[self.m_stageList[self.m_curStageIndex].id].incompleteJumpId
        elseif self.m_curStageState == GEnums.ActivityConditionalStageState.Completed then
            jumpId = self.m_activityCfgData.activityStageList[self.m_stageList[self.m_curStageIndex].id].completeJumpId
        end
        if not string.isEmpty(jumpId) then
            Utils.jumpToSystem(jumpId)
            ActivityUtils.setFalseNewActivityConditionalStage(self.m_stageList[self.m_curStageIndex].id)
        end
    end)
    local timeFormatFunc = function(leftTime)
        local prefix
        if self.m_curStageIndex > 0 then
            prefix = self.m_isEndStage and Language.LUA_LIMITED_FORMULA_SHOP_LEFT_TIME_PREFIX or Language.LUA_LIMITED_FORMULA_ACTIVITY_LEFT_TIME_PREFIX
        else
            prefix = self.m_isEndStage and "" or Language.LUA_LIMITED_FORMULA_ACTIVITY_LEFT_TIME_PREFIX
        end
        return prefix .. UIUtils.getLeftTime(leftTime)
    end
    local endStageTime = self.m_activityData.endTime





    self.view.activityCommonInfo.view.infoNode.countDownWidget:InitCountDownText(endStageTime, args.timeOnComplete, timeFormatFunc)
    self.view.activityCommonInfo.view.gotoNode.btnDetailRedDot:InitRedDot("ActivityLimitedFormula", self.m_activityId)

    self.view.shopEntryNode.shopBtn.onClick:AddListener(function()
        local groupUnlock = GameInstance.player.shopSystem:CheckShopGroupUnlocked(self.m_activityCfgData.shopGroupId)
        if not groupUnlock then
            if self.m_isEndStage then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_LIMITED_FORMULA_SHOP_ENDLOCK_CLICK_TOAST)
            else
                Notify(MessageConst.SHOW_TOAST, Language.LUA_LIMITED_FORMULA_SHOP_LOCK_CLICK_TOAST)
            end
            return
        end
        PhaseManager:OpenPhase(PhaseId.Shop, {
            shopGroupId = self.m_activityCfgData.shopGroupId,
            activityMoneyId = self.m_activityCfgData.moneyId,
            unlockToastFunc = function(shopId)
                local succ, data = self.m_activityCfgData.activityShopLockList:TryGetValue(shopId)
                if succ then
                    local targetTime = self.m_activityData.startTime + data.unlockTimeOffset * Const.SEC_PER_HOUR
                    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
                    local leftTime = targetTime - curTime
                    local toast = string.format(data.lockToast, UIUtils.getLeftTime(leftTime))
                    Notify(MessageConst.SHOW_TOAST, toast)
                end
            end,

            activityEndTime = self.m_activityData.endTime,
            activityBannerImage = "Shop/ShopCommon/shop_banner_02"
        })
    end)

    self.view.missionNode.medalBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_ACHIEVEMENT, ACHIEVEMENT_ID)
    end)

    self:_UpdateActivityNode()
end

ActivityLimitedFormulaAssistRegionCtrl.OnClose = HL.Override() << function(self)
    self.m_completeTitleCor = self:_ClearCoroutine(self.m_completeTitleCor)
end

ActivityLimitedFormulaAssistRegionCtrl.OnShow = HL.Override() << function(self)

end

ActivityLimitedFormulaAssistRegionCtrl._OnActivityUpdated = HL.Method(HL.Any) << function(self, arg)
    local id = unpack(arg)
    if id ~= self.m_activityId then
        return
    end
    self:PlayAnimationIn()
    self:_UpdateActivityData()
    self:_UpdateActivityNode()
end

ActivityLimitedFormulaAssistRegionCtrl._OnAchievementUpdated = HL.Method(HL.Any) << function(self, arg)
    if arg == nil then
        return
    end
    local bundles = unpack(arg)
    for _, bundle in pairs(bundles) do
        if bundle ~= nil and bundle.achievementId == ACHIEVEMENT_ID then

            local achievementSystem = GameInstance.player.achievementSystem
            local succ, playerInfo = achievementSystem.achievementData.achievementInfos:TryGetValue(ACHIEVEMENT_ID)
            local level = succ and playerInfo.level or 0
            local obtained = succ and playerInfo.level >= self.m_achievementCfgData.initLevel
            local plated = succ and playerInfo.isPlated
            local achievementState
            if plated then
                achievementState = "Rare"
            elseif obtained then
                achievementState = "Normal"
            else
                achievementState = "Empty"
            end
            self.view.missionNode.medalNode:SetState(achievementState)
            if achievementState ~= "Empty" then
                self.view.missionNode.mainMedal:InitMedal({
                    achievementId = ACHIEVEMENT_ID,
                    level = level,
                    isPlated = plated,
                    isRare = self.m_achievementCfgData.applyRareEffect,
                })
            end
            break
        end
    end
end

ActivityLimitedFormulaAssistRegionCtrl._OnWalletChanged = HL.Method(HL.Opt(HL.Any)) << function(self, _)

    self.view.shopEntryNode.shopNumTxt.text = Utils.getItemCount(self.m_activityCfgData.moneyId)
end

ActivityLimitedFormulaAssistRegionCtrl._UpdateActivityData = HL.Method() << function(self)
    for i = #self.m_stageList, 1, -1 do
        if self.m_stageList[i].id ~= self.m_activityCfgData.endStageId then
            local csStageInfo = self.m_activityData:GetStageData(self.m_stageList[i].id)
            if csStageInfo then
                local status = GEnums.ActivityConditionalStageState.__CastFrom(csStageInfo.Status)
                if status == GEnums.ActivityConditionalStageState.Unlocked or status == GEnums.ActivityConditionalStageState.Completed then
                    self.m_curStageIndex = i
                    self.m_curStageState = status
                    break
                end
            end
        end
    end
    local endStageInfo = self.m_activityData:GetStageData(self.m_activityCfgData.endStageId)
    if endStageInfo then
        local engStageStatus = GEnums.ActivityConditionalStageState.__CastFrom(endStageInfo.Status)
        if engStageStatus ~= GEnums.ActivityConditionalStageState.Locked then
            self.m_isEndStage = true
        end
    else
        for i = #self.m_stageList, 1, -1 do
            if self.m_stageList[i].id == self.m_activityCfgData.endStageId then
                self.m_isEndStage = self.m_stageList[i].startTime <= DateTimeUtils.GetCurrentTimestampBySeconds()
                break
            end
        end
    end
end

ActivityLimitedFormulaAssistRegionCtrl._UpdateActivityNode = HL.Method() << function(self)

    if self.m_curStageIndex == 0 then
        self.view.shopEntryNode.gameObject:SetActiveIfNecessary(false)
        self.view.missionNode.gameObject:SetActiveIfNecessary(false)

        if self.m_isEndStage then
            self.view.commonPrompt.gameObject:SetActiveIfNecessary(true)
            self.view.activityCommonInfo.view.gotoNode.stateController:SetState("None")
        end
        return
    end


    if self.m_isEndStage then
        self.view.commonPrompt.gameObject:SetActiveIfNecessary(true)
        self.view.missionNode.grayImage.gameObject:SetActiveIfNecessary(true)
        self.view.activityCommonInfo.view.gotoNode.stateController:SetState("None")
    end


    for i = START_STAGE_INDEX, FINAL_STAGE_INDEX do
        local pointController = self.view.missionNode["pointCell" .. i]
        if i < self.m_curStageIndex then
            pointController:SetState("Done")
        elseif i == self.m_curStageIndex then
            pointController:SetState(STAGE_STATE_TO_POINT_STATE_MAP[self.m_curStageState])
        elseif i > self.m_curStageIndex then
            pointController:SetState("None")
        end
    end


    self.view.missionNode.progBar.fillAmount = (self.m_curStageIndex - START_STAGE_INDEX) / (FINAL_STAGE_INDEX - START_STAGE_INDEX)


    local curStageId = self.m_stageList[self.m_curStageIndex].id
    local completeTitle = self.m_activityCfgData.activityStageList[curStageId].completeTitle
    if string.isEmpty(completeTitle) or self.m_curStageState ~= GEnums.ActivityConditionalStageState.Completed then
        self.view.missionNode.completeTipsNode.gameObject:SetActiveIfNecessary(false)
        self.m_completeTitleCor = self:_ClearCoroutine(self.m_completeTitleCor)
    else
        self.view.missionNode.completeTipsNode.gameObject:SetActiveIfNecessary(true)
        self:_UpdateCompleteTitleThread(completeTitle)
    end
    if self.m_curStageState == GEnums.ActivityConditionalStageState.Unlocked then
        self.view.missionNode.missionDesc.text = self.m_activityCfgData.activityStageList[curStageId].incompleteDesc
        self.view.missionNode.missionTips.text = self.m_activityCfgData.activityStageList[curStageId].incompleteTips
    elseif self.m_curStageState == GEnums.ActivityConditionalStageState.Completed then
        self.view.missionNode.missionDesc.text = self.m_activityCfgData.activityStageList[curStageId].completeDesc
        self.view.missionNode.missionTips.text = self.m_activityCfgData.activityStageList[curStageId].completeTips
    end


    local achievementSystem = GameInstance.player.achievementSystem
    local succ, playerInfo = achievementSystem.achievementData.achievementInfos:TryGetValue(ACHIEVEMENT_ID)
    local level = succ and playerInfo.level or 0
    local obtained = succ and playerInfo.level >= self.m_achievementCfgData.initLevel
    local plated = succ and playerInfo.isPlated
    local achievementState
    if plated then
        achievementState = "Rare"
    elseif obtained then
        achievementState = "Normal"
    else
        achievementState = "Empty"
    end
    self.view.missionNode.medalNode:SetState(achievementState)
    if achievementState ~= "Empty" then
        self.view.missionNode.mainMedal:InitMedal({
            achievementId = ACHIEVEMENT_ID,
            level = level,
            isPlated = plated,
            isRare = self.m_achievementCfgData.applyRareEffect,
        })
    end


    self.view.shopEntryNode.shopNumTxt.text = Utils.getItemCount(self.m_activityCfgData.moneyId)
end

ActivityLimitedFormulaAssistRegionCtrl._UpdateCompleteTitleThread = HL.Method(HL.String) << function(self, rawText)
    self.m_completeTitleCor = self:_ClearCoroutine(self.m_completeTitleCor)
    if self.m_curStageIndex == 1 then
        local targetTime = self.m_stageList[self.m_curStageIndex + 1].startTime - DateTimeUtils.GetCurrentTimestampBySeconds()
        self.view.missionNode.completeTips.text = string.format(rawText, UIUtils.getLeftTime(targetTime))
        self.m_completeTitleCor = self:_StartCoroutine(function()
            while true do
                coroutine.wait(1)
                targetTime = self.m_stageList[self.m_curStageIndex + 1].startTime - DateTimeUtils.GetCurrentTimestampBySeconds()
                self.view.missionNode.completeTips.text = string.format(rawText, UIUtils.getLeftTime(targetTime))
            end
        end)
    else
        local count = self.m_activityData:GetActivityMoneyAccumulateAmount(self.m_activityCfgData.moneyId)
        self.view.missionNode.completeTips.text = string.format(rawText, count)
        self.m_completeTitleCor = self:_StartCoroutine(function()
            while true do
                coroutine.wait(1)
                count = self.m_activityData:GetActivityMoneyAccumulateAmount(self.m_activityCfgData.moneyId)
                self.view.missionNode.completeTips.text = string.format(rawText, count)
            end
        end)
    end
end


HL.Commit(ActivityLimitedFormulaAssistRegionCtrl)
