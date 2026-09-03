local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

DungeonCommonSelectionCell = HL.Class('DungeonCommonSelectionCell', UIWidgetBase)

DungeonCommonSelectionCell.m_dungeonId = HL.Field(HL.String) << ""

DungeonCommonSelectionCell.m_clickFunc = HL.Field(HL.Function)

DungeonCommonSelectionCell.m_styleNode = HL.Field(HL.Any)

local UIState = {
    Lock = "Lock",
    Unlock = "Unlock",
    Complete = "Complete",
    Raid = "Raid",

    UnlockChar = "UnlockChar",
    UnlockRed = "UnlockRed",
    UnlockYellow = "UnlockYellow",
    UnlockGreen = "UnlockGreen",
    LockRed = "LockRed",
    LockYellow = "LockYellow",
    LockGreen = "LockGreen",

    Select = "Select",
    Unselect = "Unselect",
}



DungeonCommonSelectionCell._OnFirstTimeInit = HL.Override() << function(self)
    self.view.clickBtn.onClick:AddListener(function()
        if self.m_clickFunc then
            self.m_clickFunc()
        end
    end)
end

DungeonCommonSelectionCell.InitDungeonCommonSelectionCell = HL.Method(HL.String, HL.Function)
        << function(self, dungeonId, clickFunc)
    self:_FirstTimeInit()

    self.m_dungeonId = dungeonId
    self.m_clickFunc = clickFunc

    local dungeonCfg = Tables.dungeonTable[dungeonId]
    self.view.levelDescTxt.text = dungeonCfg.dungeonLevelDesc
    if not string.isEmpty(dungeonCfg.relatedCharId) then
        self.view.charNameTxt.text = dungeonCfg.dungeonLevelDesc
        self.view.charImg:LoadSprite(UIConst.UI_SPRITE_SQUARE_CHAR_HEAD,
                                     UIConst.UI_CHAR_HEAD_SQUARE_PREFIX..CSCharUtils.GetCharTemplateId(dungeonCfg.relatedCharId))
    end

    self:_UpdateState()

    self.view.redDot:InitRedDot("DungeonReadNormal", {dungeonId}, nil, self:GetUICtrl().view.redDotScrollRect)


    local inRelief = DungeonUtils.isDungeonCostStamina(dungeonId) and ActivityUtils.hasStaminaReduceCount()
    self.view.reliefTab.gameObject:SetActive(inRelief)
end

DungeonCommonSelectionCell.SetSelected = HL.Method(HL.Boolean) << function(self, isOn)


    self:_UpdateState(isOn)


    if isOn then
        GameInstance.player.subGameSys:SendSubGameListRead({ self.m_dungeonId })
    end
end

DungeonCommonSelectionCell._UpdateState = HL.Method(HL.Opt(HL.Boolean)) << function(self, isOn)

    local dungeonId = self.m_dungeonId
    local isRaid = Tables.dungeonRaidTable:TryGetValue(dungeonId)

    local dungeonCfg = Tables.dungeonTable[dungeonId]
    local isUnlock = DungeonUtils.isDungeonUnlock(dungeonId)
    local isComplete
    local isNormalComplete = isRaid and DungeonUtils.isDungeonPassed(dungeonId)

    if isRaid then
        isComplete = DungeonUtils.isDungeonPassed(Tables.dungeonRaidTable[dungeonId].RelatedLevel) and DungeonUtils.isDungeonPassed(dungeonId)
    else
        isComplete = DungeonUtils.isDungeonPassed(dungeonId)
    end


    local state1
    if isComplete then
        state1 = UIState.Complete
    elseif isNormalComplete then
        state1 = UIState.Raid
    elseif isUnlock then
        state1 = UIState.Unlock
    else
        state1 = UIState.Lock
    end
    self.view.stateController:SetState(state1)

    local hasHunterMode = not string.isEmpty(dungeonCfg.hunterModeRewardId)
    local hasCustomRewardId = not string.isEmpty(dungeonCfg.customRewardId)
    local hasCharInfo = not string.isEmpty(dungeonCfg.relatedCharId)
    local state2
    if isUnlock then
        if hasCharInfo then
            state2 = UIState.UnlockChar
        elseif hasHunterMode then
            state2 = UIState.UnlockRed
        elseif hasCustomRewardId then
            state2 = UIState.UnlockYellow
        else
            state2 = UIState.UnlockGreen
        end
    else
        if hasHunterMode then
            state2 = UIState.LockRed
        elseif hasCustomRewardId then
            state2 = UIState.LockYellow
        else
            state2 = UIState.LockGreen
        end
    end
    self.view.stateController:SetState(state2)

    local state3 = isOn and "Select" or "Unselect"
    self.view.stateController:SetState(state3)
end


DungeonCommonSelectionCell.RefreshForActivityEntry = HL.Method(HL.Number, HL.Table) << function(self, cellIndex, context)
    local csIndex = CSIndex(cellIndex)
    local dungeonId = context.dungeonSeriesData.includeDungeonIds[csIndex]
    local success, dungeonData = Tables.dungeonTable:TryGetValue(dungeonId)
    if success then
        local stateName = "Lock"
        local activityData = GameInstance.player.activitySystem:GetActivity(context.activityId)
        local activityDungeonCfg = Tables.activityDungeonTable:GetValue(context.activityId)
        local activityDungeonStateCfg = activityDungeonCfg.gameMap:GetValue(dungeonId)
        local _, stageData = activityData.stageDataDict:TryGetValue(activityDungeonStateCfg.gameUnlockStage)

        local stageUnlocked = stageData.Status >= GEnums.ActivityConditionalStageState.Unlocked:GetHashCode()
        local stageRewarded = stageData.Status >= GEnums.ActivityConditionalStageState.Rewarded:GetHashCode()

        if context.useDungeonPassedForCompleteState then



            local hasRaid, raidId = Tables.dungeonNormal2RaidTable:TryGetValue(dungeonId)
            local normalPassed = hasRaid and DungeonUtils.isDungeonPassed(dungeonId)
            local raidPassed = hasRaid and (not string.isEmpty(raidId)) and DungeonUtils.isDungeonPassed(raidId)
            if normalPassed and raidPassed then
                if not stageUnlocked then
                    logger.error("DungeonCommonSelectionCell: stage 未解锁但 normal+raid 都已通关, dungeonId=" .. tostring(dungeonId))
                end
                stateName = "Complete"
            elseif normalPassed then
                if not stageUnlocked then
                    logger.error("DungeonCommonSelectionCell: stage 未解锁但 normal 已通关, dungeonId=" .. tostring(dungeonId))
                end
                stateName = "Raid"
            elseif stageUnlocked then
                stateName = "Unlock"
            end
        else
            if stageUnlocked then
                if stageRewarded then
                    stateName = "Complete"
                else
                    stateName = "Unlock"
                end
            end
        end
        self.view.stateController:SetState(stateName, false)

        local isSelected = dungeonId == context.selectedDungeonId or (Tables.dungeonNormal2RaidTable:GetValue(dungeonId) == context.selectedDungeonId)

        self.view.stateController:SetState(
            isSelected and "Select" or "Unselect", false
        )

        if not string.isEmpty(activityDungeonStateCfg.showState) then
            self.view.stateController:SetState(activityDungeonStateCfg.showState, false)
        end
        self.view.levelDescTxt.text = dungeonData.dungeonName
        if self.view.serialNumberTxt then
            self.view.serialNumberTxt.text = activityDungeonStateCfg.lv
        end




        local cellRedDotName = context.cellRedDotName or "ActivityDungeonState"
        local cellRedDotArgs
        if context.cellRedDotArgsBuilder then
            cellRedDotArgs = context.cellRedDotArgsBuilder(dungeonId, activityDungeonStateCfg)
        else
            cellRedDotArgs = {
                activityId = context.arg.activityId,
                stageId = activityDungeonStateCfg.gameUnlockStage,
            }
        end
        self.view.redDot:InitRedDot(cellRedDotName, cellRedDotArgs, nil, context.redDotScrollRect)
    end
end

HL.Commit(DungeonCommonSelectionCell)
return DungeonCommonSelectionCell

