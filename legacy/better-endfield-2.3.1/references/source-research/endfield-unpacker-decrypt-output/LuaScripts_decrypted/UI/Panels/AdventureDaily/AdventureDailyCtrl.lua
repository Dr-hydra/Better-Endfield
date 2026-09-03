
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.AdventureDaily

AdventureDailyCtrl = HL.Class('AdventureDailyCtrl', uiCtrl.UICtrl)






AdventureDailyCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ADVENTURE_TASK_MODIFY] = 'OnAdventureTaskModify',
    [MessageConst.ON_DAILY_ACTIVATION_MODIFY] = 'OnDailyActivationModify',
    [MessageConst.ON_RESET_DAILY_ADVENTURE_TASK] = 'Refresh',
    [MessageConst.P_ON_ADVENTURE_DAILY_CLOSE_REWARD_TIPS] = '_TryHideProgressRewardTips',
    [MessageConst.ON_DAILY_ACTIVATION_REWARD] = 'OnDailyActivationReward',
    [MessageConst.ON_BATTLE_PASS_SEASON_UPDATE] = 'Refresh',
    [MessageConst.ON_BATTLE_PASS_TRACK_UPDATE] = 'Refresh',
    [MessageConst.ON_ADVENTURE_BOOK_SWITCH_SAME_TAB] = 'OnAdventureTabChangedSame',
}


AdventureDailyCtrl.m_getTaskCell = HL.Field(HL.Function)

AdventureDailyCtrl.m_taskInfos = HL.Field(HL.Table)

AdventureDailyCtrl.m_isCurActivationMax = HL.Field(HL.Boolean) << false

AdventureDailyCtrl.m_isFirstShow = HL.Field(HL.Boolean) << true

AdventureDailyCtrl.m_isBPActive = HL.Field(HL.Boolean) << false

AdventureDailyCtrl.m_getAllActionGroupId = HL.Field(HL.Number) << 0

AdventureDailyCtrl.m_taskRefreshPendingTimerId = HL.Field(HL.Number) << -1

AdventureDailyCtrl.m_rewardTipsNode = HL.Field(HL.Any)


AdventureDailyCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.getAllBtn.onClick:AddListener(function()
        self:_OnClickGetAllBtn()
    end)

    self.m_getTaskCell = UIUtils.genCachedCellFunction(self.view.taskList)
    self.view.taskList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getTaskCell(obj), LuaIndex(csIndex))
    end)

    self.view.taskListNaviGroup.onIsTopLayerChanged:AddListener(function(isTop)
        if isTop then
            self:_TryHideProgressRewardTips()
        end
    end)

    self.view.progressNode.noteBtn.onClick:RemoveAllListeners()
    self.view.progressNode.noteBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_COMMON_TITLE_TIPS, {
            title = Language.LUA_ADVENTUREBOOK_DAILY_BP_ABSENT_TITLE,
            desc = Language.LUA_ADVENTUREBOOK_DAILY_BP_ABSENT_DESC,
            posType = UIConst.UI_TIPS_POS_TYPE.DailyAbsentRightTop,
            targetTransform = self.view.progressNode.noteBtn.transform,
            isSideTips = DeviceInfo.usingController,
        })
    end)
    self.view.progressNode.noteNode.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_COMMON_TITLE_TIPS)
        end
    end)

    self.view.gotoBpTaskBtn.onClick:RemoveAllListeners()
    self.view.gotoBpTaskBtn.onClick:AddListener(function()
        PhaseManager:GoToPhase(PhaseId.BattlePass, {
            panelId = 'BattlePassTask',
        })
    end)

    self.view.gotoBpRewardBtn.onClick:RemoveAllListeners()
    self.view.gotoBpRewardBtn.onClick:AddListener(function()
        PhaseManager:GoToPhase(PhaseId.BattlePass, {
            panelId = 'BattlePassPlan',
        })
    end)


    self.m_getAllActionGroupId = InputManagerInst:CreateGroup(self.view.inputGroup.groupId)
    self:BindInputPlayerAction("adventure_daily_get_all_reward", function()
        self:_GetAllReward()
    end, self.m_getAllActionGroupId)

    self:_InitProgressNode()
    self:_RefreshTaskNode()

    self.view.countDownText:InitCountDownText(Utils.getNextCommonServerRefreshTime())
end

AdventureDailyCtrl.OnClose = HL.Override() << function(self)
    self.m_taskRefreshPendingTimerId = self:_ClearTimer(self.m_taskRefreshPendingTimerId)
    self.m_rewardTipsNode = nil
    self.view.progressNode.progressBarImg:DOKill()
    self:_TryHideProgressRewardTips()
end

AdventureDailyCtrl.OnShow = HL.Override() << function(self)
    if not self.m_isFirstShow then
        self.view.taskList:UpdateCount(#self.m_taskInfos, true)
    else
        self.m_isFirstShow = false
    end

    local firstCell = self.m_getTaskCell(1)
    if firstCell then
        self:SetNaviTarget(firstCell.naviDecorator)
    end
end

AdventureDailyCtrl.OnAdventureTaskModify = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    self:_ScheduleRefreshTaskNode()
end

AdventureDailyCtrl.OnDailyActivationModify = HL.Method() << function(self)
    self:_RefreshProgressNode()
    self:_ScheduleRefreshTaskNode()
end

AdventureDailyCtrl.OnDailyActivationReward = HL.Method(HL.Any) << function(self, args)

    local argBundles = unpack(args)
    local itemBundles = {}
    local hasDouble = BattlePassUtils.CheckBattlePassSeasonValid()
    local boostPercent = 1
    if hasDouble then
        boostPercent = (Tables.battlePassConst.absentFlagBpExpRate / 1000)
    end
    for _, argBundle in pairs(argBundles) do
        local isDouble = hasDouble and (argBundle.id == Tables.battlePassConst.bpExpItem)
        local count = argBundle.count
        if isDouble then
            count = argBundle.count * boostPercent
        end
        table.insert(itemBundles, {
            id = argBundle.id,
            count = count,
            instId = argBundle.instId,
            isDouble = isDouble,
        })
    end
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        items = itemBundles,
    })
end

AdventureDailyCtrl.Refresh = HL.Method() << function(self)
    self:_RefreshProgressNode()
    self:_ScheduleRefreshTaskNode()
end



AdventureDailyCtrl.m_progressInfos = HL.Field(HL.Table)

AdventureDailyCtrl.m_maxActivation = HL.Field(HL.Number) << -1


AdventureDailyCtrl._InitProgressNode = HL.Method() << function(self)
    local node = self.view.progressNode
    local infos = {}
    for k, v in pairs(Tables.dailyActivationRewardTable) do
        local info = {
            id = k,
            activation = v.activation,
            rewardId = v.rewardId,
        }
        table.insert(infos, info)
    end
    table.sort(infos, Utils.genSortFunction({ "id"}, true))
    self.m_progressInfos = infos
    self.m_maxActivation = infos[#infos].activation
    self.m_isBPActive = BattlePassUtils.CheckBattlePassSeasonValid()

    node.m_progressCells = UIUtils.genCellCache(node.progressCell)
    node.m_progressCells:Refresh(#self.m_progressInfos, function(cell, index)
        cell.hintBtn.onClick:AddListener(function()
            self:_OnHintBtnClick(index)
        end)
    end)

    local absentMax = Tables.battlePassConst.maxAbsenceCount
    local bpSystem = GameInstance.player.battlePassSystem
    node.m_absentCells = UIUtils.genCellCache(node.absentCell)
    node.m_absentCells:Refresh(absentMax, function(cell, index)
        cell.stateController:SetState(index <= bpSystem.seasonData.absentCount and "AbsentAvail" or "AbsentDisable")
    end)

    self:_RefreshProgressNode(true)
end

AdventureDailyCtrl._RefreshProgressNode = HL.Method(HL.Opt(HL.Boolean)) << function(self, isInit)
    local node = self.view.progressNode
    local canGetAllReward = false
    node.m_progressCells:Update(function(cell, index)
        local canGetReward = self:_RefreshProgressCell(cell, index)
        if canGetReward then
            canGetAllReward = true
        end
    end)
    InputManagerInst:ToggleGroup(self.m_getAllActionGroupId, canGetAllReward)
    local curDailyActivation = GameInstance.player.adventure.adventureBookData.dailyActivation
    local percent = curDailyActivation / self.m_maxActivation
    if isInit then
        node.progressBarImg.fillAmount = percent
    else
        node.progressBarImg:DOFillAmount(percent, 0.3)
    end
    node.progressTxt.text = curDailyActivation
    if self.m_isBPActive then
        self:_RefreshProgressBPPart()
    else
        node.stateController:SetState("NoBP")
        self.view.taskNodeStateController:SetState("NoBP")
    end
end

AdventureDailyCtrl._RefreshProgressBPPart = HL.Method() << function(self)
    local bpSystem = GameInstance.player.battlePassSystem
    local bpAbsentCount = bpSystem.seasonData.absentCount
    local absentMax = Tables.battlePassConst.maxAbsenceCount
    local curDailyRewardActivation = GameInstance.player.adventure.adventureBookData.dailyRewardedActivation
    local hasAbsent = bpAbsentCount > 0
    local bpItemData = Tables.itemTable[Tables.battlePassConst.bpExpItem]
    local bpItemName = bpItemData ~= nil and bpItemData.name or ''
    local rewarId = self.m_progressInfos[#self.m_progressInfos].rewardId
    local bpExpBaseCount = BattlePassUtils.GetAdventureDailyBpExpBaseCount(rewarId)
    local bpExpBoostCount = (Tables.battlePassConst.absentFlagBpExpRate / 1000) * bpExpBaseCount
    local activationMax = curDailyRewardActivation >= self.m_maxActivation
    local hasBpRewardAvail = BattlePassUtils.CheckHasAvailBpPlanReward()
    local node = self.view.progressNode
    node.stateController:SetState(hasAbsent and "BPAbscent" or "BPNoAbscent")
    if activationMax then
        node.experienceTipsNodeAnimationWrapper:PlayOutAnimation(function()
            node.stateController:SetState("BPComplete")
        end)
    else
        node.stateController:SetState("BPAvail")
    end

    node.experienceTipsNode:SetState(hasAbsent and "Double" or "Nrl")
    node.experienceTxt.text = hasAbsent and string.format(Language.LUA_ADVENTUREBOOK_DAILY_BP_EXP_DOUBLE_FORMAT, bpItemName, bpExpBoostCount) or
        string.format(Language.LUA_ADVENTUREBOOK_DAILY_BP_EXP_FORMAT, bpItemName, bpExpBaseCount)
    node.m_absentCells:Refresh(absentMax, function(cell, index)
        cell.stateController:SetState(index <= bpSystem.seasonData.absentCount and "AbsentAvail" or "AbsentDisable")
    end)
    self.view.taskNodeStateController:SetState((not activationMax) and "NoBP" or (hasBpRewardAvail and "BpReward" or "BpTask"))
end

AdventureDailyCtrl._RefreshProgressCell = HL.Method(HL.Any, HL.Number).Return(HL.Boolean)
    << function(self, cell, index)
    local info = self.m_progressInfos[index]
    local curDailyActivation = GameInstance.player.adventure.adventureBookData.dailyActivation
    local curDailyRewardedActivation = GameInstance.player.adventure.adventureBookData.dailyRewardedActivation
    local isMax = info.activation == self.m_maxActivation
    local isDouble = isMax and BattlePassUtils.CheckBattlePassSeasonValid()
        and GameInstance.player.battlePassSystem.seasonData.absentCount > 0
    local canGetReward = false
    cell.txt.text = info.activation
    cell.icon:LoadSprite(UIConst.UI_SPRITE_ADVENTURE, isDouble and UIConst.ADVENTURE_DAILY_PROGRESS_DOUBLE_ICON or UIConst.ADVENTURE_DAILY_PROGRESS_ICON)
    if curDailyRewardedActivation >= info.activation then
        cell.simpleStateController:SetState("Rewarded")
        cell.redDot.gameObject:SetActive(false)
        cell.gameObject:GetComponent("UIAnimationWrapper"):SampleToInAnimationBegin()
    elseif curDailyActivation >= info.activation then
        cell.simpleStateController:SetState("LightUp")
        cell.redDot.gameObject:SetActive(true)
        cell.gameObject:GetComponent("UIAnimationWrapper"):PlayLoopAnimation()
        canGetReward = true
    else
        cell.simpleStateController:SetState("Normal")
        cell.redDot.gameObject:SetActive(false)
        cell.gameObject:GetComponent("UIAnimationWrapper"):SampleToInAnimationBegin()
    end


    local nextActivation = (index < #self.m_progressInfos) and (self.m_progressInfos[index + 1].activation) or (99999999)
    if curDailyRewardedActivation < info.activation and
        curDailyActivation >= info.activation and
        curDailyActivation < nextActivation then
        cell.keyHint.overrideValidState = CS.Beyond.UI.CustomUIStyle.OverrideValidState.None
    else
        cell.keyHint.overrideValidState = CS.Beyond.UI.CustomUIStyle.OverrideValidState.ForceNotValid
    end
    cell.maxImg.gameObject:SetActive(isMax)
    return canGetReward
end

AdventureDailyCtrl.m_curShowingRewardHintIndex = HL.Field(HL.Number) << -1


AdventureDailyCtrl._OnHintBtnClick = HL.Method(HL.Number) << function(self, index)
    logger.info("AdventureDailyCtrl._OnHintBtnClick  " .. index)
    if DeviceInfo.usingController then
        self:_OnHintBtnClickWhenController(index)
    else
        self:_ShowProgressRewardTips(index)
    end
end


AdventureDailyCtrl._ShowProgressRewardTips = HL.Method(HL.Number) << function(self, index)
    local node = self:_EnsureRewardTips()
    local preCell = self.view.progressNode.m_progressCells:Get(self.m_curShowingRewardHintIndex)
    if preCell then
        preCell.lightCircle.gameObject:SetActive(false)
    end
    if node.gameObject.activeSelf and self.m_curShowingRewardHintIndex == index then

        node.animationWrapper:PlayOutAnimation(function()
            node.gameObject:SetActive(false)
        end)
        return
    end

    local info = self.m_progressInfos[index]
    local curDailyActivation = GameInstance.player.adventure.adventureBookData.dailyActivation
    local curDailyRewardedActivation = GameInstance.player.adventure.adventureBookData.dailyRewardedActivation

    if info.activation > curDailyRewardedActivation and curDailyActivation >= info.activation then
        GameInstance.player.adventure:TakeAdventureAllActivationReward()
        self.m_curShowingRewardHintIndex = -1

        AudioAdapter.PostEvent("Au_UI_Button_Common")
        return
    end


    self.m_curShowingRewardHintIndex = index

    local cell = self.view.progressNode.m_progressCells:Get(index)

    cell.lightCircle.gameObject:SetActive(true)
    node.rewardItems:InitRewardItems(info.rewardId,
        info.activation <= curDailyRewardedActivation,
        {
            onPostInitItem = function(cell, bundle)
                local isMax = info.activation == self.m_maxActivation
                self:_PostInitProgressTipsRewardItem(cell, bundle, isMax)
            end,
        })
    node.txt.text = string.format(Language.LUA_ADV_DAILY_REWARD_HINT, info.activation)
    node.autoCloseArea.tmpSafeArea = cell.hintBtn.transform
    node.autoCloseArea.onTriggerAutoClose:RemoveAllListeners()
    node.autoCloseArea.onTriggerAutoClose:AddListener(function()
        cell.lightCircle.gameObject:SetActive(false)
    end)

    node.gameObject:SetActive(true)
    UIUtils.updateTipsPosition(node.transform, cell.hintBtn.transform, self.view.rectTransform, self.uiCamera, UIConst.UI_TIPS_POS_TYPE.RightMid)
    node.animationWrapper:ClearTween(false)
    node.animationWrapper:PlayInAnimation()

    AudioAdapter.PostEvent("Au_UI_Button_Common")
end

AdventureDailyCtrl._OnHintBtnClickWhenController = HL.Method(HL.Number) << function(self, index)
    local node = self:_EnsureRewardTips()
    local preCell = self.view.progressNode.m_progressCells:Get(self.m_curShowingRewardHintIndex)
    if preCell then
        preCell.lightCircle.gameObject:SetActive(false)
    end
    if node.gameObject.activeSelf and self.m_curShowingRewardHintIndex == index then
        return
    end

    local info = self.m_progressInfos[index]
    local curDailyActivation = GameInstance.player.adventure.adventureBookData.dailyActivation
    local curDailyRewardedActivation = GameInstance.player.adventure.adventureBookData.dailyRewardedActivation


    self.m_curShowingRewardHintIndex = index

    local cell = self.view.progressNode.m_progressCells:Get(index)

    cell.lightCircle.gameObject:SetActive(true)
    node.rewardItems:InitRewardItems(info.rewardId,
        info.activation <= curDailyRewardedActivation,
        {
            onPostInitItem = function(cell, bundle)
                local isMax = info.activation == self.m_maxActivation
                self:_PostInitProgressTipsRewardItem(cell, bundle, isMax)
            end,
        })
    node.txt.text = string.format(Language.LUA_ADV_DAILY_REWARD_HINT, info.activation)
    node.autoCloseArea.tmpSafeArea = cell.hintBtn.transform
    node.autoCloseArea.onTriggerAutoClose:RemoveAllListeners()
    node.autoCloseArea.onTriggerAutoClose:AddListener(function()
        cell.lightCircle.gameObject:SetActive(false)
    end)

    node.gameObject:SetActive(true)
    UIUtils.updateTipsPosition(node.transform, cell.hintBtn.transform, self.view.rectTransform, self.uiCamera, UIConst.UI_TIPS_POS_TYPE.RightMid)
    node.animationWrapper:ClearTween(false)
    node.animationWrapper:PlayInAnimation()
end

AdventureDailyCtrl._GetAllReward = HL.Method() << function(self)
    GameInstance.player.adventure:TakeAdventureAllActivationReward()
end

AdventureDailyCtrl._PostInitProgressTipsRewardItem = HL.Method(HL.Any, HL.Any, HL.Boolean) << function(self, cell, bundle, isMax)
    if not self.m_isBPActive or bundle.id ~= Tables.battlePassConst.bpExpItem or not isMax then
        cell.view.bgDouble.gameObject:SetActive(false)
        return
    end
    local isDouble = GameInstance.player.battlePassSystem.seasonData.absentCount > 0
    cell.view.bgDouble.gameObject:SetActive(isDouble)
    if isDouble then
        cell:UpdateCount(bundle.count * (Tables.battlePassConst.absentFlagBpExpRate / 1000))
    end
end

AdventureDailyCtrl._TryHideProgressRewardTips = HL.Method() << function(self)
    local node = self.m_rewardTipsNode
    if not node or not node.gameObject.activeSelf then
        return
    end
    node.animationWrapper:PlayOutAnimation(function()
        node.gameObject:SetActive(false)
    end)
    local cell = self.view.progressNode.m_progressCells:Get(self.m_curShowingRewardHintIndex)
    if cell then
        cell.lightCircle.gameObject:SetActive(false)
    end
    self.m_curShowingRewardHintIndex = -1
end


AdventureDailyCtrl._EnsureRewardTips = HL.Method().Return(HL.Any) << function(self)
    if self.m_rewardTipsNode then
        return self.m_rewardTipsNode
    end
    local prefab = self:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Adventure/Widgets/AdventureDailyRewardTips.prefab")
    local obj = CSUtils.CreateObject(prefab, self.view.transform)
    obj.name = "RewardTips"
    obj.transform.localScale = Vector3.one
    obj:SetActive(false)
    self.m_rewardTipsNode = Utils.wrapLuaNode(obj)
    self.m_rewardTipsNode.rewardItems.view.rewardListNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
    return self.m_rewardTipsNode
end






AdventureDailyCtrl._ScheduleRefreshTaskNode = HL.Method() << function(self)
    if self.m_taskRefreshPendingTimerId ~= -1 then
        return
    end
    self.m_taskRefreshPendingTimerId = TimerManager:StartFrameTimer(0, function()
        self.m_taskRefreshPendingTimerId = -1
        self:_RefreshTaskNode()
    end, self)
end

AdventureDailyCtrl._RefreshTaskNode = HL.Method() << function(self)
    local taskDic = GameInstance.player.adventure.adventureBookData.adventureTasks
    self.m_taskInfos = {}
    local completeCount = 0
    for k, v in pairs(Tables.adventureTaskTable) do
        if v.taskType == GEnums.AdventureTaskType.Daily then
            local _, csTask = taskDic:TryGetValue(k)
            if csTask ~= nil then
                local info = {
                    id = k,
                    data = v,
                    sortId = v.sortId,
                    csTask = csTask,
                }
                if info.csTask.isRewarded then
                    info.stateOrder = -1
                elseif info.csTask.isComplete then
                    info.stateOrder = 1
                    completeCount = completeCount + 1
                else
                    info.stateOrder = 0
                end
                table.insert(self.m_taskInfos, info)
            end
        end
    end
    table.sort(self.m_taskInfos, Utils.genSortFunction({"stateOrder", "sortId"}))

    local curActivation = GameInstance.player.adventure.adventureBookData.dailyActivation
    self.m_isCurActivationMax = curActivation >= self.m_maxActivation

    self.view.taskList:UpdateCount(#self.m_taskInfos)
    local showGetAll = completeCount > 0 and not self.m_isCurActivationMax
    self.view.getAllNode.gameObject:SetActive(showGetAll)





end

AdventureDailyCtrl._OnUpdateCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local info = self.m_taskInfos[index]
    cell.desc.text = info.data.taskDesc
    local itemBundle = UIUtils.getRewardFirstItem(info.data.rewardId)
    cell.rewardTxt.text = "×" .. itemBundle.count
    cell.progressTxt.text = string.format("%d/%d", info.csTask.progress, info.csTask.targetProgress)

    cell.progressImg.fillAmount = info.csTask.progress / info.csTask.targetProgress

    cell.getBtn.onClick:RemoveAllListeners()
    cell.gotoBtn.onClick:RemoveAllListeners()

    if self.m_isCurActivationMax then
        if info.csTask.isRewarded then
            cell.stateNode:PlayWithTween("adv_daily_task_max_rewarded")
        else
            cell.stateNode:PlayWithTween("adv_daily_task_max")
        end
    elseif info.csTask.isRewarded then
        cell.stateNode:PlayWithTween("adv_daily_task_rewarded")
    elseif info.csTask.isComplete then
        cell.stateNode:PlayWithTween("adv_daily_task_can_getloop")
        cell.getBtn.onClick:AddListener(function()

            self:_OnClickGetAllBtn()
        end)
    else
        cell.stateNode:PlayWithTween("adv_daily_task_normal")
        if not string.isEmpty(info.data.jumpSystemId) then
            cell.gotoBtn.gameObject:SetActive(true)
            cell.unfinishHint.gameObject:SetActive(false)
            cell.gotoBtn.onClick:AddListener(function()
                Utils.jumpToSystem(info.data.jumpSystemId)
            end)
        else
            cell.gotoBtn.gameObject:SetActive(false)
            cell.unfinishHint.gameObject:SetActive(true)
        end
    end

    cell.cellAniWrapper:PlayWithTween("adventuredailytaskcell_in")

    cell.redDot:InitRedDot("AdventureBookTabDailyTaskCell", info.id)
end

AdventureDailyCtrl._OnClickGetBtn = HL.Method(HL.Number) << function(self, index)
    local info = self.m_taskInfos[index]
    GameInstance.player.adventure:TakeAdventureTaskReward(info.id)
end

AdventureDailyCtrl._OnClickGetAllBtn = HL.Method() << function(self)
    GameInstance.player.adventure:TakeAdventureAllTaskRewardOfType(GEnums.AdventureTaskType.Daily)
end



AdventureDailyCtrl.OnAdventureTabChangedSame = HL.Method(HL.Number) << function(self, panelId)
    if panelId == PANEL_ID then
        local firstCell = self.m_getTaskCell(1)
        if firstCell then
            self:SetNaviTarget(firstCell.naviDecorator)
        end
    end
end

HL.Commit(AdventureDailyCtrl)
