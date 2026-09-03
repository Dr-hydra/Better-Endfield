local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailBossRush

MapMarkDetailBossRushCtrl = HL.Class('MapMarkDetailBossRushCtrl', uiCtrl.UICtrl)






MapMarkDetailBossRushCtrl.s_messages = HL.StaticField(HL.Table) << {

}

MapMarkDetailBossRushCtrl.m_difficultyList = HL.Field(HL.Forward('UIListCache'))

MapMarkDetailBossRushCtrl.m_type = HL.Field(HL.Any)

MapMarkDetailBossRushCtrl.m_defaultLookDungeonId = HL.Field(HL.Any)

MapMarkDetailBossRushCtrl.m_markInstId = HL.Field(HL.String) << ""

MapMarkDetailBossRushCtrl.m_dungeonSeriesId = HL.Field(HL.String) << ""

MapMarkDetailBossRushCtrl.m_difficulty2rewardList = HL.Field(HL.Table)

MapMarkDetailBossRushCtrl.m_dungeonSeriesData = HL.Field(HL.Any)


local FIRST_TEXT = "ui_dung_entry_new_tag_once"
local FIRST_REWARD_LIST_TEXT = "ui_map_mark_dung_new_once"
local hunter_REWARD_LIST_TEXT = "ui_map_mark_dung_new_regular"


MapMarkDetailBossRushCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_difficulty2rewardList = {}
    self.m_difficultyList = UIUtils.genCellCache(self.view.singleDifficulty)
    args = args or {}
    local markInstId = args.markInstId
    self.m_markInstId = markInstId
    local getRuntimeDataSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)

    if getRuntimeDataSuccess == false then
        logger.error("地图详情页获取实例数据失败" .. markInstId)
        return
    end
    local templateId = markRuntimeData.templateId
    local getSuccess, templateInfo = Tables.mapMarkTempTable:TryGetValue(templateId)
    self.m_type = templateInfo.markType
    local detailData = markRuntimeData.detail
    local dungeonSeriesId = detailData.systemInstId
    self.m_dungeonSeriesId = dungeonSeriesId
    local getDungeonSeriesSuccess, dungeonSeriesData = Tables.dungeonSeriesTable:TryGetValue(dungeonSeriesId or "")
    if not getDungeonSeriesSuccess then
        logger.error("地图详情页获取DungeonSeries信息失败" .. dungeonSeriesId)
        return
    end
    self.m_dungeonSeriesData = dungeonSeriesData

    local includeDungeonIds = dungeonSeriesData.includeDungeonIds
    local dungeonDifficultyCount = includeDungeonIds.Count

    for i = 1, dungeonDifficultyCount do
        local dungeonId = includeDungeonIds[CSIndex(i)]
        if not GameInstance.dungeonManager:IsDungeonPassed(dungeonId) then
            self.m_defaultLookDungeonId = dungeonId
            break
        end
    end


    if self.m_defaultLookDungeonId == nil then
        self.m_defaultLookDungeonId = includeDungeonIds[CSIndex(dungeonDifficultyCount)]
    end

    self.m_difficultyList:Refresh(dungeonDifficultyCount, function(difficulty, luaIndex)
        local dungeonId = includeDungeonIds[CSIndex(luaIndex)]
        self:_UpdateSingleDifficulty(luaIndex, difficulty, dungeonId)
    end)

    self:_SetCommonWidget(markRuntimeData.isActive)
end

MapMarkDetailBossRushCtrl._SetCommonWidget = HL.Method(HL.Boolean) << function(self, isActive)
    self.view.detailCommon.gameObject:SetActive(true)
    local commonArgs = {}
    commonArgs.markInstId = self.m_markInstId
    commonArgs.titleText = self.m_dungeonSeriesData.name
    commonArgs.descText = self.m_dungeonSeriesData.desc
    if not isActive or self.m_type ~= GEnums.MarkType.BossRush then
        commonArgs.bigBtnActive = true
    else
        commonArgs.leftBtnActive = true
        commonArgs.leftBtnText = Language["ui_mapmarkdetail_button_enter"]
        commonArgs.leftBtnCallback = function()
            PhaseManager:GoToPhase(PhaseId.DungeonEntry, {dungeonSeriesId = self.m_dungeonSeriesId})
        end

        commonArgs.leftBtnIconName = UIConst.MAP_DETAIL_BTN_ICON_NAME.FAST_ENTER

        commonArgs.rightBtnActive = true
    end
    self.view.detailCommon:InitMapMarkDetailCommon(commonArgs)
end

MapMarkDetailBossRushCtrl._UpdateSingleDifficulty = HL.Method(HL.Number, HL.Any, HL.Any) << function(self, luaIndex, difficulty, dungeonId)
    if DeviceInfo.usingController then
        difficulty.m_rewardActive = true
    else
        difficulty.m_rewardActive = (dungeonId == self.m_defaultLookDungeonId)
    end


    local getDungeonInfoSuccess, dungeonCfg = Tables.dungeonTable:TryGetValue(dungeonId)
    if not getDungeonInfoSuccess then
        logger.error("无法获取到副本数据，dungeonId为" .. dungeonId)
        return
    end

    if not difficulty.m_rewardList then
        difficulty.m_rewardList = UIUtils.genCellCache(difficulty.rewardCell)
    end

    difficulty.difficultyText.text = dungeonCfg.dungeonLevelDesc
    local gameMechanicData = Tables.gameMechanicTable[dungeonId]
    local rewardInfoTable = {}
    local rewardListNum = 0

    local hasFirstReward = not string.isEmpty(gameMechanicData.firstPassRewardId)
    local firstRewardsTable = {}
    if hasFirstReward then
        local showInfoTable = {}
        rewardListNum = rewardListNum + 1
        local rewardId = gameMechanicData.firstPassRewardId
        local firstRewardGained = GameInstance.dungeonManager:IsDungeonFirstPassRewardGained(dungeonId)
        firstRewardsTable = self:_ProcessNormalRewards(rewardId, firstRewardGained)
        local isGet, textVal = CS.Beyond.I18n.I18nUtils.TryGetText(FIRST_REWARD_LIST_TEXT)
        showInfoTable.titleText = textVal
        showInfoTable.rewardsTable = firstRewardsTable
        showInfoTable.isFirstRewards = true
        rewardInfoTable[rewardListNum] = showInfoTable
    end

    local hasHunterModeRewardId = not string.isEmpty(gameMechanicData.hunterModeRewardId)
    local hunterRewardTable = {}
    if hasHunterModeRewardId then
        local showInfoTable = {}
        rewardListNum = rewardListNum + 1
        local rewardId = gameMechanicData.hunterModeRewardId
        hunterRewardTable = self:_ProcessHunterRewards(dungeonId, false)
        local isGet, textVal = CS.Beyond.I18n.I18nUtils.TryGetText(hunter_REWARD_LIST_TEXT)
        showInfoTable.titleText = textVal
        showInfoTable.rewardsTable = hunterRewardTable
        showInfoTable.isFirstRewards = false
        rewardInfoTable[rewardListNum] = showInfoTable
    end

    if self.m_difficulty2rewardList[difficulty] == nil then
        self.m_difficulty2rewardList[difficulty] = {}
    end

    difficulty.m_rewardList:Refresh(rewardListNum, function(rewardListCell, rewardListLuaIndex)
        local showInfoTable = rewardInfoTable[rewardListLuaIndex]
        if showInfoTable == nil then
            logger.error("m_rewardList：showInfoTable 获取失败 ".. rewardListLuaIndex)
            return
        end

        local rewardList = UIUtils.genCellCache(rewardListCell.itemReward)
        table.insert(self.m_difficulty2rewardList[difficulty], rewardListCell)
        rewardListCell.titleTxt.text = showInfoTable.titleText
        rewardList:Refresh(#showInfoTable.rewardsTable, function(rewardCell, rewardLuaIndex)
            self:_UpdateRewardCell(luaIndex, rewardLuaIndex, rewardCell, showInfoTable.rewardsTable[rewardLuaIndex], showInfoTable.isFirstRewards)
        end)
    end)

    difficulty.switchBtn.onClick:AddListener(function()
        difficulty.m_rewardActive = not difficulty.m_rewardActive
        self:_RefreshDifficultyFold(difficulty)
    end)

    self:_RefreshDifficultyFold(difficulty)
end

MapMarkDetailBossRushCtrl._RefreshDifficultyFold = HL.Method(HL.Any) << function(self, difficulty)
    if self.m_difficulty2rewardList[difficulty] ~= nil then
        for key, value in pairs(self.m_difficulty2rewardList[difficulty]) do
            value.gameObject:SetActive(difficulty.m_rewardActive)
        end
    end
    difficulty.switchIconUp.gameObject:SetActive(difficulty.m_rewardActive)
    difficulty.switchIconDown.gameObject:SetActive(not difficulty.m_rewardActive)
end


MapMarkDetailBossRushCtrl._ProcessHunterRewards = HL.Method(HL.String, HL.Boolean).Return(HL.Table)
    << function(self, dungeonId, gained)
    local rewardsTable = {}
    local dungeonCfg = Tables.dungeonTable[dungeonId]
    local rewardCfg = Tables.rewardTable[dungeonCfg.hunterModeRewardId]
    for _, itemBundle in pairs(rewardCfg.itemBundles) do
        local itemCfg = Tables.itemTable[itemBundle.id]
        table.insert(rewardsTable, {
            id = itemCfg.id,
            typeSortId = 0,
            typeTag = DungeonConst.DUNGEON_REWARD_TAG_STATE.Regular,
            sortId1 = itemCfg.sortId1,
            sortId2 = itemCfg.sortId2,
        })
    end

    for _, itemBundle in pairs(rewardCfg.probItemBundles) do
        local itemCfg = Tables.itemTable[itemBundle.id]
        table.insert(rewardsTable, {
            id = itemCfg.id,
            typeSortId = -1,
            typeTag = DungeonConst.DUNGEON_REWARD_TAG_STATE.Random,
            sortId1 = itemCfg.sortId1,
            sortId2 = itemCfg.sortId2,
        })
    end

    local sortKeys = UIConst.COMMON_ITEM_SORT_KEYS
    table.insert(sortKeys, 1, "typeSortId")
    table.sort(rewardsTable, Utils.genSortFunction(sortKeys))
    return rewardsTable
end

MapMarkDetailBossRushCtrl._ProcessNormalRewards = HL.Method(HL.String, HL.Boolean).Return(HL.Table)
        << function(self, rewardId, gained)
    local rewardsTable = {}
    local rewardsCfg = Tables.rewardTable[rewardId]
    for _, itemBundle in pairs(rewardsCfg.itemBundles) do
        local itemCfg = Tables.itemTable[itemBundle.id]
        table.insert(rewardsTable, {
            id = itemBundle.id,
            count = itemBundle.count,
            rarity = itemCfg.rarity,
            sortId1 = itemCfg.sortId1,
            sortId2 = itemCfg.sortId2,
            type = itemCfg.type:ToInt(),
            gained = gained,
        })
    end
    table.sort(rewardsTable, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))
    return rewardsTable
end

MapMarkDetailBossRushCtrl._UpdateRewardCell = HL.Method(HL.Number, HL.Number, HL.Any, HL.Table, HL.Boolean) << function(self, luaIndex, rewardLuaIndex, cell, info, isFirstRewards)
    self.view.detailCommon:InitDetailItem(cell, info, {
        tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
        tipsPosTransform = self.view.scrollView,
    })

    cell.view.rewardedCover.gameObject:SetActiveIfNecessary(info.gained)
end

HL.Commit(MapMarkDetailBossRushCtrl)
