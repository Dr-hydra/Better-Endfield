local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

TrainingEntryTab = HL.Class('TrainingEntryTab', UIWidgetBase)

TrainingEntryTab.m_seriesId = HL.Field(HL.String) << ""

TrainingEntryTab.m_tableCfg = HL.Field(HL.Any)

TrainingEntryTab.m_info = HL.Field(HL.Table)

TrainingEntryTab.m_index = HL.Field(HL.Number) << 0

TrainingEntryTab.m_genRewardCells = HL.Field(HL.Forward("UIListCache"))

TrainingEntryTab.m_onClickGotoBtnFunc = HL.Field(HL.Function)


TrainingEntryTab._OnFirstTimeInit = HL.Override() << function(self)
    self.m_genRewardCells = UIUtils.genCellCache(self.view.rewardCell)

    self.view.goToBtn.onClick:RemoveAllListeners()
    self.view.goToBtn.onClick:AddListener(function()
        self:_OnClickGoToBtn()
    end)
    self.view.rewardListNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
end

TrainingEntryTab.InitTrainingEntryTab = HL.Method(HL.Boolean, HL.Opt(HL.String, HL.Number, HL.Function))
        << function(self, isEmptyState, dungeonSeriesId, index, onClickGotoBtnFunc)
    self:_FirstTimeInit()

    if isEmptyState then
        self.view.cellState:SetState("Empty")
        return
    end

    local hasCfg = false
    if dungeonSeriesId then
        hasCfg, self.m_tableCfg = Tables.dungeonSeriesTable:TryGetValue(dungeonSeriesId)
    end
    if not hasCfg then
        logger.error("[Dungeon Series Table] missing, id = " .. dungeonSeriesId)
        self.view.cellState:SetState("Empty")
        return
    end

    self.m_seriesId = dungeonSeriesId
    self.m_index = index
    self.m_onClickGotoBtnFunc = onClickGotoBtnFunc
    self:_UpdateData()
    self:_RefreshAllUI()

    self.view.redDot:InitRedDot("DungeonReadNormal", self.m_tableCfg.includeDungeonIds)
end

TrainingEntryTab._UpdateData = HL.Method() << function(self)
    local curPassNum = 0
    for _, id in pairs(self.m_tableCfg.includeDungeonIds) do
        if GameInstance.dungeonManager:IsDungeonPassed(id) then
            curPassNum = curPassNum + 1
        end
    end
    self.m_info = {
        seriesId = self.m_tableCfg.id,
        titleName = self.m_tableCfg.name,
        curNum = curPassNum,
        maxNum = #self.m_tableCfg.includeDungeonIds,
        rewardInfos = self:_GetDungeonSeriesRewardInfosOverride(self.m_tableCfg),
    }
end

TrainingEntryTab._GetDungeonSeriesRewardInfos = HL.StaticMethod(HL.Any).Return(HL.Table) << function(seriesCfg)
    local rewards = {}
    for _, v in pairs(seriesCfg.includeDungeonIds) do
        TrainingEntryTab._ProcessDungeonRewards(v, rewards)
    end
    local rewardList = {}
    for _, v in pairs(rewards) do
        table.insert(rewardList, v)
    end
    table.sort(rewardList, Utils.genSortFunction({ "rarity", "type" }))
    return rewardList
end


TrainingEntryTab._GetDungeonSeriesRewardInfosOverride = HL.Method(HL.Any).Return(HL.Table) << function(self, seriesCfg)
    local rewardId
    if self.m_index == 1 then
        rewardId = Tables.GlobalConst.adventureBookTrainingCellRewardId_dung_group_chartrial01
    else
        rewardId = Tables.GlobalConst.adventureBookTrainingCellRewardId_dung_group_train01
    end

    if rewardId == nil then
        return TrainingEntryTab._GetDungeonSeriesRewardInfos(seriesCfg)
    end
    local rewards = {}
    TrainingEntryTab._MergeRewards(rewardId, rewards)
    local rewardList = {}
    for _, v in pairs(rewards) do
        table.insert(rewardList, v)
    end
    table.sort(rewardList, Utils.genSortFunction({ "rarity", "type" }))
    return rewardList
end

TrainingEntryTab._ProcessDungeonRewards = HL.StaticMethod(HL.String, HL.Table) << function(dungeonId, rewards)
    local hasCfg, gameMechanicCfg = Tables.gameMechanicTable:TryGetValue(dungeonId)
    if not hasCfg then
        logger.error("[Game Mechanic Table] missing, id = " .. dungeonId)
        return
    end


    local hasFirstReward = not string.isEmpty(gameMechanicCfg.firstPassRewardId)
    if hasFirstReward then
        TrainingEntryTab._MergeRewards(gameMechanicCfg.firstPassRewardId, rewards)
    end


    local hasRecycleReward = not string.isEmpty(gameMechanicCfg.rewardId)
    if hasRecycleReward then
        TrainingEntryTab._MergeRewards(gameMechanicCfg.rewardId, rewards)
    end


    local hasExtraReward = not string.isEmpty(gameMechanicCfg.extraRewardId)
    if hasExtraReward then
        TrainingEntryTab._MergeRewards(gameMechanicCfg.extraRewardId, rewards)
    end
end

TrainingEntryTab._MergeRewards = HL.StaticMethod(HL.String, HL.Table) << function(rewardId, rewards)
    local hasCfg, rewardsCfg = Tables.rewardTable:TryGetValue(rewardId)
    if not hasCfg then
        logger.error("[Reward Table] missing, id = " .. rewardId)
        return
    end

    for _, itemBundle in pairs(rewardsCfg.itemBundles) do
        local reward = rewards[itemBundle.id]
        if not reward then
            local itemCfg
            hasCfg, itemCfg = Tables.itemTable:TryGetValue(itemBundle.id)
            if hasCfg then
                reward = {
                    id = itemBundle.id,
                    rarity = itemCfg.rarity,
                    type = itemCfg.type:ToInt(),
                }
                rewards[itemBundle.id] = reward
            else
                logger.error("[Item Table] missing, id = " .. itemBundle.id)
            end
        end
    end
end

TrainingEntryTab._RefreshAllUI = HL.Method() << function(self)
    local info = self.m_info
    local isAllComplete = info.curNum >= info.maxNum
    local prog = info.curNum / info.maxNum

    if self.m_index < 10 then
        self.view.levelNumTxt.text = "0"..self.m_index
        self.view.shadowText.text = "0"..self.m_index
    else
        self.view.levelNumTxt.text = self.m_index
        self.view.shadowText.text = self.m_index
    end
    self.view.levelTitleTxt.text = info.titleName
    if isAllComplete then
        self.view.cellState:SetState("AllComplete")
    else
        self.view.cellState:SetState("Normal")
    end

    self.view.progressBar.fillAmount = prog
    self.view.curProgTxt.text = info.curNum
    self.view.maxProgTxt.text = info.maxNum
    if self.m_index == 1 then
        self.view.levelNode:SetState("Green")
    elseif self.m_index == 2 then
        self.view.levelNode:SetState("Blue")
    end

    self.m_genRewardCells:Refresh(#info.rewardInfos, function(rewardCell, rewardLuaIndex)
        rewardCell:InitItem(info.rewardInfos[rewardLuaIndex], function()
            UIUtils.showItemSideTips(rewardCell)
        end)
        rewardCell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })

    end)
end

TrainingEntryTab._OnClickGoToBtn = HL.Method() << function(self)
    local enterDungeonCallback
    local panelId = self:GetPanelId()
    if panelId == PanelId.AdventureTraining then
        enterDungeonCallback = function(enterDungeonId)
            LuaSystemManager.uiRestoreSystem:AddRequest(enterDungeonId)
        end
    elseif panelId == PanelId.DungeonTrainOverview then

        enterDungeonCallback = function(enterDungeonId)
            LuaSystemManager.uiRestoreSystem:AddRequest(enterDungeonId, function()
                return GameInstance.mode.modeType == GEnums.GameModeType.SpaceShip
            end)
        end
    end
    Notify(MessageConst.ON_OPEN_DUNGEON_ENTRY_PANEL, { self.m_seriesId, enterDungeonCallback })

    if self.m_onClickGotoBtnFunc then
        self.m_onClickGotoBtnFunc()
    end
end

HL.Commit(TrainingEntryTab)
return TrainingEntryTab
