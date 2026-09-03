local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DungeonActivityEntry
local PHASE_ID = PhaseId.DungeonEntry

DungeonActivityEntryCtrl = HL.Class('DungeonActivityEntryCtrl', uiCtrl.UICtrl)

DungeonActivityEntryCtrl.m_genCells = HL.Field(HL.Forward("UIListCache"))

DungeonActivityEntryCtrl.m_rewardCellCache = HL.Field(HL.Forward("UIListCache"))

DungeonActivityEntryCtrl.m_curSelectedDungeonId = HL.Field(HL.String) << ""

DungeonActivityEntryCtrl.m_dungeonSeriesId = HL.Field(HL.String) << ""

DungeonActivityEntryCtrl.m_dungeonCount = HL.Field(HL.Number) << 0

DungeonActivityEntryCtrl.m_dungeons = HL.Field(HL.Table)

DungeonActivityEntryCtrl.m_activityId = HL.Field(HL.String) << ""

DungeonActivityEntryCtrl.m_fromDialog = HL.Field(HL.Boolean) << false

DungeonActivityEntryCtrl.m_args = HL.Field(HL.Table)

DungeonActivityEntryCtrl.m_enterDungeonCallback = HL.Field(HL.Function)





DungeonActivityEntryCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SC_MULTI_STAGE_ACTIVITY_GAIN_REWARD] = 'OnGainMultiStageActivityReward',

    [MessageConst.ON_ACTIVITY_UPDATED] = 'OnActivityUpdated',
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE] = 'OnActivityUpdated',
}

DungeonActivityEntryCtrl._IsSameDungeon = HL.Virtual(HL.String).Return(HL.Boolean) << function(self, dungeonId)
    return dungeonId == self.m_curSelectedDungeonId
end

DungeonActivityEntryCtrl._GetCellRefreshContext = HL.Virtual().Return(HL.Table) << function(self)
    return {
        selectedDungeonId = self.m_curSelectedDungeonId,
        activityId = self.m_activityId,
        dungeonSeriesData = Tables.dungeonSeriesTable:GetValue(self.m_dungeonSeriesId),
        arg = self.m_args,
        redDotScrollRect = self.view.redDotScrollRect,


        cellRedDotName = "ActivityDungeonState",
        cellRedDotArgsBuilder = function(dungeonId, activityDungeonStateCfg)
            return {
                activityId = self.m_activityId,
                stageId = activityDungeonStateCfg.gameUnlockStage,
            }
        end,
    }
end


DungeonActivityEntryCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_args = arg

    self.m_fromDialog = arg ~= nil and arg.fromDialog == true

    self.view.btnClose.onClick:RemoveAllListeners()
    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.view.btnEnemyDetails.onClick:RemoveAllListeners()
    self.view.btnEnemyDetails.onClick:AddListener(function()
        Notify(MessageConst.SHOW_INTRO, self.m_curSelectedDungeonId)
    end)

    self.view.helpBtn.onClick:RemoveAllListeners()
    self.view.helpBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.InstructionBook, "activity_dungeon_actmonster")
    end)

    self.view.rechallengeBtn.onClick:RemoveAllListeners()
    self.view.rechallengeBtn.onClick:AddListener(function()
        self.view.dungeonCommonInfo:_OnBtnDungeonEntryClick()
    end)

    self.view.goToBattleBtn.onClick:RemoveAllListeners()
    self.view.goToBattleBtn.onClick:AddListener(function()
        self.view.dungeonCommonInfo:_OnBtnDungeonEntryClick()
    end)

    self.m_dungeonSeriesId = arg.dungeonSeriesId
    self.m_activityId = arg.activityId
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    local activity = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    if not activity then

        self:_StartCoroutine(function()
            coroutine.step()
            coroutine.step()
            coroutine.yield()
            Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_FORBIDDEN)
            Notify(MessageConst.SHOW_POP_UP, {
                content = Language.LUA_ACTIVITY_MODIFY_QUIT_TO_MENU,
                hideCancel = true,
                onConfirm = function()
                    PhaseManager:ExitPhaseFastTo(PhaseId.Level, true)
                end
            })
            Notify(MessageConst.DIALOG_CHANGE_NEXT_INDEX, { phaseId = PHASE_ID, nextIndex = 1 })
            PhaseManager:PopPhase(PHASE_ID)
        end)
        return
    end

    self.m_genCells = UIUtils.genCellCache(self.view.dungeonSelectCell)
    self.m_rewardCellCache = UIUtils.genCellCache(self.view.rewardCell)

    local _, achievementData = Tables.activityAchievementDataTable:TryGetValue(self.m_activityId)
    if achievementData ~= nil then
        self.view.dungeonMedalCell:InitCommonMedalNode(achievementData.achievementId)
    end


    local success, dungeonSeriesData = Tables.dungeonSeriesTable:TryGetValue(self.m_dungeonSeriesId)
    if not success then
        logger.error("DungeonActivityEntryCtrl.OnCreate 找不到副本系列数据，id=", self.m_dungeonSeriesId)
        return
    end

    local function cellRefreshFunc(cellView, cellIndex)
        cellView:RefreshForActivityEntry(cellIndex, self:_GetCellRefreshContext())
    end

    self.m_curSelectedDungeonId = arg.dungeonId or self:GetDefaultSelectedDungeonId()


    self.m_dungeons = {}
    for i = 0, dungeonSeriesData.includeDungeonIds.Count - 1 do
        local dungeonId = dungeonSeriesData.includeDungeonIds[i]
        local isNormalDungeon, cfg = Tables.dungeonNormal2RaidTable:TryGetValue(dungeonId)
        if isNormalDungeon then
            table.insert(self.m_dungeons, dungeonId)
        end
    end
    table.sort(self.m_dungeons, function(a, b)
        local _, cfgA = Tables.dungeonTable:TryGetValue(a)
        local _, cfgB = Tables.dungeonTable:TryGetValue(b)
        return cfgA.sortId < cfgB.sortId
    end)
    self.m_dungeonCount = #self.m_dungeons
    self.m_genCells:Refresh(#self.m_dungeons, cellRefreshFunc)

    local items = self.m_genCells:GetItems()
    for index, item in pairs(items) do
        item.view.clickBtn.onClick:RemoveAllListeners()
        item.view.clickBtn.onClick:AddListener(function()
            local dungeonId = self.m_dungeons[index]
            self:_OnDungeonCellClick(item, index, dungeonId)
        end)
    end
    self.m_enterDungeonCallback = function(enterDungeonId)
        LuaSystemManager.uiRestoreSystem:AddRequest(enterDungeonId)
    end
    self.view.dungeonCommonInfo:InitDungeonCommonInfo({
        enterDungeonCallback = self.m_enterDungeonCallback,
    })
    self:UpdateInfo()

    self.view.claimRewardsBtn.onClick:RemoveAllListeners()
    self.view.claimRewardsBtn.onClick:AddListener(function()
        local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
        local activityDungeonStateCfg = Tables.activityDungeonState:GetValue(self.m_curSelectedDungeonId)
        local _, task = activityData.stageDataDict:TryGetValue(activityDungeonStateCfg.activityStage)
        GameInstance.player.activitySystem:SendReceiveRewardConditionMultiStage(self.m_activityId, task.Id)
    end)

    if DeviceInfo.inputType ~= DeviceInfo.InputType.Controller then
        return
    end
    for index, dungeonId in ipairs(self.m_dungeons) do
        if self:_IsSameDungeon(dungeonId) then
            self:SetNaviTarget(self.m_genCells:Get(index).view.clickBtn)
            break
        end
    end
end

DungeonActivityEntryCtrl.GetDefaultSelectedDungeonId = HL.Virtual().Return(HL.String) << function(self)






    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local dungeonSeriesData = Tables.dungeonSeriesTable:GetValue(self.m_dungeonSeriesId)
    local activityDungeonCfg = Tables.activityDungeonTable:GetValue(self.m_activityId)
    local dungeonIds = {}
    for i = 0, dungeonSeriesData.includeDungeonIds.Count - 1 do
        local dungeonId = dungeonSeriesData.includeDungeonIds[i]
        table.insert(dungeonIds, dungeonId)
    end

    table.sort(dungeonIds, function(a, b)
        local _, cfgA = Tables.dungeonTable:TryGetValue(a)
        local _, cfgB = Tables.dungeonTable:TryGetValue(b)
        return cfgA.sortId > cfgB.sortId
    end)

    for _, dungeonId in ipairs(dungeonIds) do
        local activityDungeonStateCfg = activityDungeonCfg.gameMap:GetValue(dungeonId)
        local success, stageData = activityData.stageDataDict:TryGetValue(activityDungeonStateCfg.gameUnlockStage)
        if success and stageData.Status ~= GEnums.ActivityConditionalStageState.Locked:GetHashCode() then
            local isRaid, normalId = Tables.dungeonRaid2NormalTable:TryGetValue(dungeonId)
            if isRaid and not DungeonUtils.isDungeonPassed(normalId) then

            else
                return dungeonId
            end
        end
    end

    return dungeonIds[1] or ""
end

DungeonActivityEntryCtrl.OnGainMultiStageActivityReward = HL.Method(HL.Any) << function(self, args)
    local activityId = unpack(args)
    if activityId == self.m_activityId then
        self.m_genCells:Refresh(self.m_dungeonCount, function(cellView, cellIndex)
            cellView:RefreshForActivityEntry(cellIndex, self:_GetCellRefreshContext())
        end)
        self:UpdateInfo()
    end
end

DungeonActivityEntryCtrl.OnActivityUpdated = HL.Method(HL.Any) << function(self, args)
    local id = unpack(args)
    if id ~= self.m_activityId then
        return
    end
    local activity = GameInstance.player.activitySystem:GetActivity(id)
    if not activity then
        GameInstance.player.guide:OnActivityDisabled()
        UIManager:Close(PanelId.ActivityStartReminderPopup)
        UIManager:Close(PanelId.InstructionBook)
        UIManager:Close(PanelId.CommonIntro)
        UIManager:Close(PanelId.ItemTips)
        UIManager:Close(PanelId.CommonEnemyPopup)
        UIManager:Close(PanelId.CommonRewardDetailsPopup)
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_FORBIDDEN)
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_ACTIVITY_MODIFY_QUIT_TO_MENU,
            hideCancel = true,
            onConfirm = function()
                PhaseManager:ExitPhaseFastTo(PhaseId.Level, true)
            end
        })
        PhaseManager:PopPhase(PHASE_ID)
    else
        self.m_genCells:Refresh(self.m_dungeonCount, function(cellView, cellIndex)
            cellView:RefreshForActivityEntry(cellIndex, self:_GetCellRefreshContext())
        end)
        self:UpdateInfo()
    end
end

DungeonActivityEntryCtrl.OnNewDay = HL.Method() << function(self)
    self.m_genCells:Refresh(self.m_dungeonCount, function(cellView, cellIndex)
        cellView:RefreshForActivityEntry(cellIndex, self:_GetCellRefreshContext())
    end)
    self:UpdateInfo()
end

DungeonActivityEntryCtrl._OnDungeonCellClick = HL.Virtual(HL.Any, HL.Number, HL.String) << function(self, cell, index, dungeonId)
    if self:_IsSameDungeon(dungeonId) then
        return
    end

    local lastSelectedIndex = 0
    for i = 1, #self.m_dungeons do
        if self:_IsSameDungeon(self.m_dungeons[i]) then
            lastSelectedIndex = i
            break
        end
    end
    local lastSelectedCell = self.m_genCells:Get(lastSelectedIndex)
    self.m_curSelectedDungeonId = dungeonId
    local activityDungeonCfg = Tables.activityDungeonTable:GetValue(self.m_activityId)
    local data = activityDungeonCfg.gameMap:GetValue(dungeonId)
    if data.showState == "SpecialNode" then
        AudioAdapter.PostEvent("Au_UI_Toast_HighDifficultyHint")
    end
    local function refreshLastSelected(cellView, cellIndex)
        cellView:RefreshForActivityEntry(cellIndex, self:_GetCellRefreshContext())
        local currentDungeonId = self.m_dungeons[cellIndex]
        if not self:_IsSameDungeon(currentDungeonId) then
            if cellView.view.selectBg then
                cellView.view.selectBg:ClearTween()
                cellView.view.selectBg:PlayOutAnimation(function()
                    cellView.view.selectBg.gameObject:SetActive(false)
                end)
            end
        end
    end
    if lastSelectedCell then
        refreshLastSelected(lastSelectedCell, lastSelectedIndex)
    end
    refreshLastSelected(cell, index)

    self:UpdateInfo()
end

DungeonActivityEntryCtrl.UpdateInfo = HL.Virtual() << function(self)
    local _, dungeonCfg = Tables.dungeonTable:TryGetValue(self.m_curSelectedDungeonId)
    local activityData = GameInstance.player.activitySystem:GetActivity(self.m_activityId)
    local activityDungeonStateCfg = Tables.activityDungeonState:GetValue(self.m_curSelectedDungeonId)
    local success, stageData = activityData.stageDataDict:TryGetValue(activityDungeonStateCfg.activityStage)
    local stageCfg = Tables.activityConditionalMultiStageTable:GetValue(self.m_activityId)

    if not string.isEmpty(dungeonCfg.dungeonPicPath) then
        self.view.dungeonBGImage:LoadSprite(UIConst.UI_SPRITE_DUNGEON, dungeonCfg.dungeonPicPath)
        self.view.maskImg:LoadSprite(UIConst.UI_SPRITE_DUNGEON, dungeonCfg.dungeonPicPath .. "_bg")
    end

    if success and stageData.Status == GEnums.ActivityConditionalStageState.Locked:GetHashCode() then
        self.view.lockNode.gameObject:SetActiveIfNecessary(true)
        local unlockTime = stageData.OpenTimeTs
        self.view.dungeonCommonInfo.gameObject:SetActive(false)

        local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
        local deltaTime = unlockTime - curTime
        local timeTipsDesc = stageCfg.stageList:GetValue(activityDungeonStateCfg.activityStage).timeTipsDesc
        if deltaTime > 0 then
            if not string.isEmpty(timeTipsDesc) then
                self.view.timeTxt.text = CS.System.String.Format(timeTipsDesc, UIUtils.getLeftTime(deltaTime))
            else
                self.view.timeTxt.text = UIUtils.getLeftTime(deltaTime)
            end
            return
        end
        if not GameInstance.player.activitySystem:HasUncompletedNonTimeConditions(activityDungeonStateCfg.activityStage) then
            if not string.isEmpty(timeTipsDesc) then
                self.view.timeTxt.text = CS.System.String.Format(timeTipsDesc, UIUtils.getLeftTime(0))
            else
                self.view.timeTxt.text = UIUtils.getLeftTime(0)
            end

            return
        end
    end
    self.view.lockNode.gameObject:SetActiveIfNecessary(false)

    local stageInfo = stageCfg.stageList:GetValue(activityDungeonStateCfg.activityStage)
    local isStageCompleted = stageData.Status == GEnums.ActivityConditionalStageState.Completed:GetHashCode()
    local isStageRewarded = stageData.Status == GEnums.ActivityConditionalStageState.Rewarded:GetHashCode()

    local treatCompletedAsRewarded = isStageCompleted and string.isEmpty(stageInfo.rewardId)

    self.view.claimRewardsBtn.gameObject:SetActive(isStageCompleted and not treatCompletedAsRewarded)
    self.view.goToBattleBtn.gameObject:SetActive(stageData.Status == GEnums.ActivityConditionalStageState.Unlocked:GetHashCode())
    self.view.rechallengeBtn.gameObject:SetActive(isStageRewarded or treatCompletedAsRewarded)


    self.view.dungeonCommonInfo:RefreshDungeonActivityCommonInfo(self.m_curSelectedDungeonId, self.m_activityId)
    self.view.dungeonCommonInfo.gameObject:SetActive(true)
    self.view.dungeonCommonInfo.view.btnDungeonEntry.gameObject:SetActive(false)

    self.view.rewardNode.gameObject:SetActive(true)
    self:_MarkCurrentSelectedAsViewed(activityDungeonStateCfg)
end




DungeonActivityEntryCtrl._MarkCurrentSelectedAsViewed = HL.Virtual(HL.Any) << function(self, activityDungeonStateCfg)
    ActivityUtils.setFalseNewActivityConditionalStage(activityDungeonStateCfg.activityStage)
end


DungeonActivityEntryCtrl.GetRedDotStateAt = HL.Virtual(HL.Number).Return(HL.Number) << function(self, index)
    local luaIndex = LuaIndex(index)

    local dungeonId = self.m_dungeons[luaIndex]
    local activityDungeonCfg = Tables.activityDungeonTable:GetValue(self.m_activityId)
    local data = activityDungeonCfg.gameMap[dungeonId]

    local hasRedDot, redDotType, expireTs = RedDotManager:GetRedDotState("ActivityDungeonState", {
        activityId = self.m_activityId,
        stageId = data.gameUnlockStage,
    })
    if hasRedDot then
        return redDotType or UIConst.RED_DOT_TYPE.Normal
    else
        return 0
    end
end

DungeonActivityEntryCtrl.GetCurSelectDungeonId = HL.Method().Return(HL.String) << function(self)
    return self.m_curSelectedDungeonId
end

DungeonActivityEntryCtrl.GetRecoverPopupStateArg = HL.Virtual().Return(HL.Opt(HL.Any)) << function(self)
    local popupState = self.view.dungeonCommonInfo:GetRecoverPopupStateArg()
    if popupState ~= nil then
        return popupState
    end

    local isOpen, introCtrl = UIManager:IsOpen(PanelId.CommonIntro)
    if isOpen then
        return {
            popupType = "ActivityIntro",
            introState = introCtrl:GetRecoverStateArg(),
        }
    end

    local isInstructionBookOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isInstructionBookOpen and instructionCtrl.id == "activity_dungeon_actmonster" then
        return {
            popupType = "ActivityInstructionBook",
        }
    end
end

DungeonActivityEntryCtrl.TryRecoverPopupState = HL.Virtual(HL.Any) << function(self, popupState)
    if popupState == nil or string.isEmpty(popupState.popupType) then
        return
    end

    if popupState.popupType == "ActivityIntro" then
        local isOpen, introCtrl = UIManager:IsOpen(PanelId.CommonIntro)
        if isOpen then
            return
        end
        local introState = popupState.introState
        if introState ~= nil and not string.isEmpty(introState.introId) then
            UIManager:Open(PanelId.CommonIntro, introState)
        else

            Notify(MessageConst.SHOW_INTRO, self.m_curSelectedDungeonId)
        end
        return
    end

    if popupState.popupType == "ActivityInstructionBook" then
        local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
        if isOpen and instructionCtrl.id == "activity_dungeon_actmonster" then
            return
        end
        UIManager:Open(PanelId.InstructionBook, "activity_dungeon_actmonster")
        return
    end

    self.view.dungeonCommonInfo:TryRecoverPopupState(popupState)
end

HL.Commit(DungeonActivityEntryCtrl)
