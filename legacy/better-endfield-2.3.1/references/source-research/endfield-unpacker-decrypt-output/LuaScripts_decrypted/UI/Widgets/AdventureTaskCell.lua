local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

AdventureTaskCell = HL.Class('AdventureTaskCell', UIWidgetBase)

AdventureTaskCell.m_itemRewardCellCache = HL.Field(HL.Forward("UIListCache"))

AdventureTaskCell.m_taskId = HL.Field(HL.String) << ""

AdventureTaskCell.m_delayShowCoroutine = HL.Field(HL.Thread)


AdventureTaskCell._OnFirstTimeInit = HL.Override() << function(self)
    self.m_itemRewardCellCache = UIUtils.genCellCache(self.view.itemSmallReward)

    self.view.contentNode.getTaskReward.onClick:AddListener(function()
        GameInstance.player.adventure:TakeAdventureTaskReward(self.m_taskId)
    end)

    self.view.rewardListNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
end

AdventureTaskCell._OnDisable = HL.Override() << function(self)
    self:_ClearCoroutine(self.m_delayShowCoroutine)
end

AdventureTaskCell.InitAdventureTaskCell = HL.Method(HL.Table, HL.Opt(HL.Number)) << function(self, info, delayShowTime)
    self:_FirstTimeInit()

    if info == nil then
        if delayShowTime then
            self:_ClearCoroutine(self.m_delayShowCoroutine)
            self:PlayInAniAndDelayTime(delayShowTime)
        end
        self.view.aniWrapper:Play("adventuretaskcell_empty")
        return
    end

    self.m_taskId = info.taskId
    local state = info.state or AdventureBookUtils.StageTaskDisplayState.InProgress


    local isNormal = state == AdventureBookUtils.StageTaskDisplayState.InProgress
    local isFinish = state == AdventureBookUtils.StageTaskDisplayState.Complete
    local isRewarded = state == AdventureBookUtils.StageTaskDisplayState.Rewarded
        or state == AdventureBookUtils.StageTaskDisplayState.OtherDomainRewarded

    local taskCfg = Tables.AdventureTaskTable[info.taskId]
    local rewardId = taskCfg.rewardId
    local rewardData = Tables.rewardTable[rewardId]
    local adventure = GameInstance.player.adventure

    local rewards = {}
    for _, itemBundle in pairs(rewardData.itemBundles) do
        local cfg = Utils.tryGetTableCfg(Tables.itemTable, itemBundle.id)
        if cfg then
            table.insert(rewards, {
                id = itemBundle.id,
                count = itemBundle.count,

                rarity = -cfg.rarity,
                sortId1 = cfg.sortId1,
                sortId2 = cfg.sortId2,
            })
        end
    end

    table.sort(rewards, Utils.genSortFunction({"rarity", "sortId1", "sortId2", "id"}, true))

    self.m_itemRewardCellCache:Refresh(#rewards, function(cell, luaIndex)
        cell.view.rewardedCover.gameObject:SetActiveIfNecessary(isRewarded)
        cell:InitItem(rewards[luaIndex], function()
            UIUtils.showItemSideTips(cell)
        end)
        cell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
    end)
    local taskData = Tables.adventureTaskTable[info.taskId]
    local isCompleted = adventure:IsTaskComplete(info.taskId)
    local taskProgress = AdventureBookUtils.GetTaskCurrProgress(taskData)
    local maxProgress = AdventureBookUtils.GetTaskMaxProgress(taskData)
    local curProgress = (isRewarded or isCompleted) and maxProgress or taskProgress


    local stateAniName
    if isNormal then
        stateAniName = "adventuretaskcell_normal"
    elseif isFinish then
        stateAniName = "adventuretaskcell_finish"
    else
        stateAniName = "adventuretaskcell_rewarded"
    end
    if delayShowTime then
        self:_ClearCoroutine(self.m_delayShowCoroutine)
        self:PlayInAniAndDelayTime(delayShowTime)
    end
    self.view.aniWrapper:Play(stateAniName)

    if state == AdventureBookUtils.StageTaskDisplayState.OtherDomainRewarded then
        self.view.contentNode.text.text = "-/-"
    else
        self.view.contentNode.text.text = string.format("%d/%d", curProgress, maxProgress)
    end
    self.view.contentNode.taskDesc.text = taskData.taskDesc
    self.view.contentNode.otherDomainCompleteNode.gameObject:SetActiveIfNecessary(
        state == AdventureBookUtils.StageTaskDisplayState.OtherDomainRewarded)
    if state == AdventureBookUtils.StageTaskDisplayState.OtherDomainRewarded then
        self.view.contentNode.otherDomainCompleteText.text =
            string.format(Language.LUA_ADVENTURE_BOOK_OTHER_DOMAIN_COMPLETE, info.otherDomainName or "")
    end

    local showDomainBadge = info.showDomainBadge
    self.view.domainBadge.gameObject:SetActiveIfNecessary(showDomainBadge)
    if showDomainBadge and not string.isEmpty(info.badgeIcon) then
        self.view.domainBadgeIcon:LoadSprite(UIConst.UI_SPRITE_ADVENTURE, info.badgeIcon)
    end


    if isNormal then
        if string.isEmpty(taskCfg.jumpSystemId) then
            self.view.contentNode.jumpBtn.gameObject:SetActiveIfNecessary(false)
            self.view.contentNode.ongoing.gameObject:SetActiveIfNecessary(true)
        else
            self.view.contentNode.jumpBtn.gameObject:SetActiveIfNecessary(true)
            self.view.contentNode.ongoing.gameObject:SetActiveIfNecessary(false)
            local jumpId = taskCfg.jumpSystemId
            self.view.contentNode.jumpBtn.onClick:RemoveAllListeners()
            self.view.contentNode.jumpBtn.onClick:AddListener(function()
                Utils.jumpToSystem(jumpId)
            end)
        end
    end


    self.view.redDot:InitRedDot("AdventureBookTabStageTaskCell", info.redDotArg)
end

AdventureTaskCell.PlayInAniAndDelayTime = HL.Method(HL.Number) << function(self, delayTime)
    self.view.aniWrapper:ClearTween(false)
    self.view.canvasGroup.alpha = 0
    self.m_delayShowCoroutine = self:_StartCoroutine(function()
        coroutine.wait(delayTime)
        self.view.aniWrapper:Play("adventuretaskcell_in")
        self.m_delayShowCoroutine = nil
    end)
end

HL.Commit(AdventureTaskCell)
return AdventureTaskCell

