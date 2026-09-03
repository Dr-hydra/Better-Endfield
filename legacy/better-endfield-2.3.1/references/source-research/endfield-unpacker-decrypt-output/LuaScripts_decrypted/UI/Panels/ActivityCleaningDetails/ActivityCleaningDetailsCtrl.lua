
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ActivityCleaningDetails
local PHASE_ID = PhaseId.ActivityCleaningDetails


local activitySystem = GameInstance.player.activitySystem

ActivityCleaningDetailsCtrl = HL.Class('ActivityCleaningDetailsCtrl', uiCtrl.UICtrl)

ActivityCleaningDetailsCtrl.m_activityId = HL.Field(HL.String) << ""

ActivityCleaningDetailsCtrl.m_activityData = HL.Field(CS.Beyond.Gameplay.ActivityGraffitiCleaning)

ActivityCleaningDetailsCtrl.m_stageInfoList = HL.Field(HL.Table)

ActivityCleaningDetailsCtrl.m_getCellFunc = HL.Field(HL.Function)

ActivityCleaningDetailsCtrl.m_currIndex = HL.Field(HL.Number) << 1

ActivityCleaningDetailsCtrl.m_rewardCells = HL.Field(HL.Forward('UIListCache'))

ActivityCleaningDetailsCtrl.m_haveSetTabNavi = HL.Field(HL.Boolean) << false





ActivityCleaningDetailsCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE] = '_OnMultiStageUpdate',
    [MessageConst.ON_CONDITIONAL_MULTI_STAGE_PROGRESS_CHANGE] = '_OnMultiStageUpdate',
    [MessageConst.ON_ACTIVITY_UPDATED] = '_OnActivityUpdated',
}


ActivityCleaningDetailsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    if arg.activityId == nil then
        logger.error("ActivityCleaningDetailsCtrl: 没有传入activityId, 请检查配置")
        return
    end
    self.m_activityId = arg.activityId
    self.m_activityData = activitySystem:GetActivity(self.m_activityId)

    self:_BindUI()
    self:_InitData()
    self:_SetDefaultTabIndex()
    self:_RefreshUI(true)
end











ActivityCleaningDetailsCtrl._BindUI = HL.Method() << function(self)
    self.view.commonTopTitleNode.btnClose.onClick:RemoveAllListeners()
    self.view.commonTopTitleNode.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)

    self.view.commonTopTitleNode.helpBtn.onClick:RemoveAllListeners()
    self.view.commonTopTitleNode.helpBtn.onClick:AddListener(function()
        local _, activityData = Tables.activityTable:TryGetValue(self.m_activityId)
        UIManager:Open(PanelId.InstructionBook, activityData.instructionId)
    end)


    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.leftNode.scrollChallenge)
    self.view.leftNode.scrollChallenge.onUpdateCell:AddListener(function(obj, index)
        local cell = self.m_getCellFunc(obj)
        self:_SetupViewStageCell(cell, LuaIndex(index), true)

        if LuaIndex(index) == self.m_currIndex and self.m_haveSetTabNavi == false then
            self.m_haveSetTabNavi = true
            self:SetNaviTarget(cell.button)
        end
    end)

    self.view.rightNode.btnDetail.onClick:RemoveAllListeners()
    self.view.rightNode.btnDetail.onClick:AddListener(function()
        local stageInfo = self.m_stageInfoList[self.m_currIndex]
        self:_OnGotoBtnClick(stageInfo)
    end)

    self.view.rightNode.btnReceive.onClick:RemoveAllListeners()
    self.view.rightNode.btnReceive.onClick:AddListener(function()
        local stageInfo = self.m_stageInfoList[self.m_currIndex]
        activitySystem:SendReceiveRewardConditionMultiStage(self.m_activityId, stageInfo.stageId)
    end)

    self.view.rightNode.activityStartReminder.reminderJumpBtn.onClick:RemoveAllListeners()
    self.view.rightNode.activityStartReminder.reminderJumpBtn.onClick:AddListener(function()
        self:_OnLockBtnClick()
    end)

    self.m_rewardCells = UIUtils.genCellCache(self.view.rightNode.itemBlack)

    if self.view.leftNode.redDotScrollRect then
        self.view.leftNode.redDotScrollRect.getRedDotStateAt = function(csIndex)
            return self:GetRedDotStateAt(csIndex)
        end
    end

    local _, achievementData = Tables.activityAchievementDataTable:TryGetValue(self.m_activityId)
    if achievementData ~= nil then
        self.view.leftNode.dungeonMedalCell:InitCommonMedalNode(achievementData.achievementId)
    end

    self.view.rightNode.scrollViewRewards.onIsFocusedChange:AddListener(function(focus)
        if not focus then
            self.view.rightNode.scrollViewRewardsScrollRect.horizontalNormalizedPosition = 0
        end
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

ActivityCleaningDetailsCtrl._InitData = HL.Method() << function(self)
    self.m_stageInfoList = {}
    local currTs = DateTimeUtils.GetCurrentTimestampBySeconds()

    local _, multiStageCfg = Tables.activityConditionalMultiStageTable:TryGetValue(self.m_activityId)
    for stageId, stageCfg in pairs(multiStageCfg.stageList) do

        local csConditionalStageInfo = self.m_activityData:GetStageData(stageId)
        if csConditionalStageInfo ~= nil then
            local status = GEnums.ActivityConditionalStageState.__CastFrom(csConditionalStageInfo.Status)
            local canShowStage, lockDesc = self.m_activityData:CanShowStage(stageId)
            local _, cfg = Tables.activityCleaningStageDataTable:TryGetValue(stageId)

            local stageInfo = {
                stageId = stageId,
                name = stageCfg.name,
                sortId = stageCfg.sortId,
                missionId = stageCfg.missionId,
                mapJumpId = stageCfg.jumpId,
                rewardId = stageCfg.rewardId,
                rewardItems = UIUtils.getRewardItems(stageCfg.rewardId),
                desc = stageCfg.desc,
                rankRelatedId = stageCfg.rankRelatedId,
                status = status,
                isRepeat = not string.isEmpty(stageCfg.rankRelatedId),
                canShowStage = canShowStage,
                lockDesc = lockDesc,
                img = cfg and cfg.img or "",
                startTime = Utils.getTimeIdOpenTimeStamp(stageCfg.timeId),
                timeLock = not Utils.isCurTimeInTimeIdRange(stageCfg.timeId),
            }
            if canShowStage then
                table.insert(self.m_stageInfoList, stageInfo)
            end
        end
    end

    table.sort(self.m_stageInfoList, Utils.genSortFunction({"sortId"}, true))
end

ActivityCleaningDetailsCtrl._RefreshUI = HL.Method(HL.Opt(HL.Boolean))
    << function(self, onCreate)
    if onCreate then
        self.view.leftNode.scrollChallenge:UpdateCount(#self.m_stageInfoList, CSIndex(self.m_currIndex))
    else
        self.view.leftNode.scrollChallenge:UpdateShowingCells(function(index, obj)
            local cell = self.m_getCellFunc(obj)
            self:_SetupViewStageCell(cell, LuaIndex(index), false)
        end)
    end

    self:_SetupViewRightNode()
end

ActivityCleaningDetailsCtrl._SetDefaultTabIndex = HL.Method() << function(self)

    for index, stageInfo in ipairs(self.m_stageInfoList) do
        if stageInfo.status == GEnums.ActivityConditionalStageState.Unlocked then
            self.m_currIndex = index
            return
        end
    end

    for index, stageInfo in ipairs(self.m_stageInfoList) do
        if stageInfo.status == GEnums.ActivityConditionalStageState.Completed then
            self.m_currIndex = index
            return
        end
    end

    local tmpIndex = 0
    for index, stageInfo in ipairs(self.m_stageInfoList) do
        if stageInfo.status == GEnums.ActivityConditionalStageState.Rewarded then
            tmpIndex = index
        end
    end
    if tmpIndex ~= 0 then
        self.m_currIndex = tmpIndex
        return
    end

    self.m_currIndex = 1
end

ActivityCleaningDetailsCtrl._SetupViewStageCell = HL.Method(HL.Any, HL.Number, HL.Boolean, HL.Opt(HL.Boolean, HL.Boolean))
    << function(self, cell, luaIndex, onCreate, playSelectAnim, playNoSelectAnim)
    local stageInfo = self.m_stageInfoList[luaIndex]
    cell.numTxt.text = tostring(luaIndex)
    cell.numSelTxt.text = tostring(luaIndex)
    cell.nameTxt.text = stageInfo.name
    cell.nameSelTxt.text = stageInfo.name
    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        self:_ClickTab(luaIndex)
    end)

    local stateCtrl = cell.stateController


    local key1
    if stageInfo.status == GEnums.ActivityConditionalStageState.Rewarded then
        key1 = "End"
    elseif stageInfo.status == GEnums.ActivityConditionalStageState.Locked and
        stageInfo.timeLock then
        key1 = "Lock"
    else
        key1 = "Nrl"
    end
    local key2 = nil
    if onCreate then
        key2 = (self.m_currIndex == luaIndex) and "Sel" or "NoSel"
    elseif playSelectAnim then

        key2 = "NoSel"
    elseif playNoSelectAnim then

        key2 = "Sel"
    else
        key2 = (self.m_currIndex == luaIndex) and "Sel" or "NoSel"
    end
    local key3 = stageInfo.isRepeat and "repeat" or "normal"
    local key = key1 .. "_" .. key2 .. "_" .. key3
    stateCtrl:SetState(key)

    if playSelectAnim then
        cell.animationWrapper:Play("activitycleaningdetailselect_in")
    elseif playNoSelectAnim then
        cell.animationWrapper:Play("activitycleaningdetailselect_out")
    end

    cell.redDot:InitRedDot("ActivityCleaningCheckStageIsNewOrCompleteStatus", {
        activityId = self.m_activityId,
        stageId = stageInfo.stageId,
    }, nil, self.view.leftNode.redDotScrollRect)
end

ActivityCleaningDetailsCtrl._SetupViewRightNode = HL.Method(HL.Opt(HL.Boolean)) << function(self, playAnim)
    if playAnim then
        self.view.rightNode.ani:ClearTween(false)
        self.view.rightNode.ani:PlayInAnimation()
    end

    local stageInfo = self.m_stageInfoList[self.m_currIndex]
    local rightNode = self.view.rightNode
    local timeLock = stageInfo.timeLock

    if timeLock and stageInfo.status == GEnums.ActivityConditionalStageState.Locked then
        rightNode.simpleStateController:SetState("Empty")
        local remainSeconds = stageInfo.startTime - DateTimeUtils.GetCurrentTimestampBySeconds()
        if remainSeconds < 0 then
            remainSeconds = 0
        end
        rightNode.lockTxt.text = string.format(Language.LUA_ACTIVITY_CLEANING_STAGE_REMAIN_TIME_TEXT, UIUtils.getLeftTime(remainSeconds))
        return
    end

    ActivityUtils.setFalseNewActivityConditionalStage(stageInfo.stageId)

    rightNode.simpleStateController:SetState("Nrl")

    rightNode.infoNode.txtName.text = stageInfo.name
    rightNode.infoNode.detailsTxt.text = stageInfo.desc
    rightNode.photoImg:LoadSprite(UIConst.UI_SPRITE_ACTIVITY, stageInfo.img)
    if stageInfo.status == GEnums.ActivityConditionalStageState.Locked then
        rightNode.titleStateNode:SetState("Lock")
        rightNode.photoLockImage:SampleToInAnimationBegin()
    else
        rightNode.titleStateNode:SetState("Nrl")
    end

    rightNode.activityCommonRecord.gameObject:SetActive(stageInfo.isRepeat)
    rightNode.activityCommonRecord:InitActivityCommonRecord(self.m_activityId, stageInfo.rankRelatedId)

    local rewardItems = stageInfo.rewardItems
    self.m_rewardCells:Refresh(#rewardItems, function(cell, index)
        local rewardItem = rewardItems[index]
        local reward = {
            id = rewardItem.id,
            count = rewardItem.count > 1 and rewardItem.count or nil,
        }
        cell:InitItem(reward, true)
        cell:SetExtraInfo({
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
            tipsPosTransform = cell.transform,
            isSideTips = DeviceInfo.usingController,
        })
        cell.view.rewardedCover.gameObject:SetActive(stageInfo.status == GEnums.ActivityConditionalStageState.Rewarded)
    end)

    rightNode.btnDetail.gameObject:SetActive(stageInfo.status == GEnums.ActivityConditionalStageState.Unlocked
        or (stageInfo.status == GEnums.ActivityConditionalStageState.Rewarded and stageInfo.isRepeat))
    rightNode.btnReceive.gameObject:SetActive(stageInfo.status == GEnums.ActivityConditionalStageState.Completed)
    rightNode.endNode.gameObject:SetActive(stageInfo.status == GEnums.ActivityConditionalStageState.Rewarded
        and not stageInfo.isRepeat)

    rightNode.activityStartReminder.gameObject:SetActive(stageInfo.status == GEnums.ActivityConditionalStageState.Locked)
    rightNode.activityStartReminder.decoText.text = stageInfo.lockDesc
end

ActivityCleaningDetailsCtrl._ClickTab = HL.Method(HL.Number) << function(self, newIndex)
    local prevIndex = self.m_currIndex
    if prevIndex == newIndex then
        return
    end
    self.view.leftNode.scrollChallenge:UpdateShowingCells(function(index, obj)
        if prevIndex == LuaIndex(index) then
            local cell = self.m_getCellFunc(obj)
            self:_SetupViewStageCell(cell, LuaIndex(index), false, false, true)
        elseif newIndex == LuaIndex(index) then
            local cell = self.m_getCellFunc(obj)
            self:_SetupViewStageCell(cell, LuaIndex(index), false, true, false)
        end
    end)

    self.m_currIndex = newIndex

    self:_SetupViewRightNode(true)
end

ActivityCleaningDetailsCtrl._OnGotoBtnClick = HL.Method(HL.Any) << function(self, stageInfo)
    local taskId = stageInfo.missionId
    local taskState = GameInstance.player.mission:GetMissionState(taskId)
    local isTaskComplete = taskState == CS.Beyond.Gameplay.MissionSystem.MissionState.Completed

    if not isTaskComplete then
        PhaseManager:OpenPhase(PhaseId.Mission, {
            autoSelect = taskId
        })
    else
        Utils.jumpToSystem(stageInfo.mapJumpId)
    end
end

ActivityCleaningDetailsCtrl._OnLockBtnClick = HL.Method() << function(self)
    if self.m_currIndex <= 1 then
        logger.error("ActivityCleaningDetailsCtrl: 需要检查代码逻辑")
        return
    end

    for i = self.m_currIndex, 1, -1 do
        local stageInfo = self.m_stageInfoList[i]
        if stageInfo.status == GEnums.ActivityConditionalStageState.Unlocked then
            self:_OnGotoBtnClick(stageInfo)
            return
        end
    end
end

ActivityCleaningDetailsCtrl._OnMultiStageUpdate = HL.Method(HL.Any) << function(self, arg)
    local activityId = unpack(arg)
    if self.m_activityId ~= activityId then
        return
    end





    local timeLockStageInfos = {}
    for i = 1, #self.m_stageInfoList do
        local stageInfo = self.m_stageInfoList[i]
        if stageInfo.status == GEnums.ActivityConditionalStageState.Locked and stageInfo.timeLock then
            table.insert(timeLockStageInfos, stageInfo)
        end
    end

    self:_InitData()


    local flag = false
    for _, stageInfo in ipairs(timeLockStageInfos) do
        local found = lume.match(self.m_stageInfoList, function(x)
            return x.stageId == stageInfo.stageId
        end)
        if found then
            if found.timeLock == false then
                flag = true
            end
        end
    end

    if flag then
        logger.info("ActivityCleaningDetailsCtrl: 准备播放进入动效和toast")
        self.animationWrapper:PlayInAnimation()
        Notify(MessageConst.SHOW_TOAST, Language.LUA_ACTIVITY_STAGE_NEW_UNLOCK_TOAST)

        self:_SetDefaultTabIndex()
        self:_RefreshUI(true)
    else
        self:_RefreshUI()
    end
end

ActivityCleaningDetailsCtrl._OnActivityUpdated = HL.Method(HL.Any) << function(self, arg)
    local id = unpack(arg)
    if id ~= self.m_activityId then
        return
    end
    local activity = GameInstance.player.activitySystem:GetActivity(id)
    if not activity then
        ActivityUtils.backToMainHud()
    end
end

ActivityCleaningDetailsCtrl._OnProgressUpdate = HL.Method() << function(self)
    self:_InitData()
end

ActivityCleaningDetailsCtrl.GetRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
    local luaIndex = LuaIndex(index)
    if luaIndex < 1 or luaIndex > #self.m_stageInfoList then
        return 0
    end

    local stageInfo = self.m_stageInfoList[luaIndex]
    local hasRedDot, redDotType, expireTs = RedDotManager:GetRedDotState(
        "ActivityCheckStageIsCompleteStatus", {
            activityId = self.m_activityId,
            stageId = stageInfo.stageId,
        })
    if hasRedDot then
        return redDotType or UIConst.RED_DOT_TYPE.Normal
    else
        return 0
    end
end

HL.Commit(ActivityCleaningDetailsCtrl)
