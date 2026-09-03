
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailEnemySpawner

MapMarkDetailEnemySpawnerCtrl = HL.Class('MapMarkDetailEnemySpawnerCtrl', uiCtrl.UICtrl)





MapMarkDetailEnemySpawnerCtrl.s_messages = HL.StaticField(HL.Table) << {

}

MapMarkDetailEnemySpawnerCtrl.m_firstRewardItemCache = HL.Field(HL.Forward('UIListCache'))

MapMarkDetailEnemySpawnerCtrl.m_commonRewardItemCache = HL.Field(HL.Forward('UIListCache'))

MapMarkDetailEnemySpawnerCtrl.m_gameGroupId = HL.Field(HL.String) << ""

MapMarkDetailEnemySpawnerCtrl.m_gameId = HL.Field(HL.String) << ""

MapMarkDetailEnemySpawnerCtrl.m_markInstId = HL.Field(HL.String) << ""


MapMarkDetailEnemySpawnerCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    self.m_firstRewardItemCache = UIUtils.genCellCache(self.view.firstPassRewardItem)
    self.m_commonRewardItemCache = UIUtils.genCellCache(self.view.commonRewardItem)

    self.view.enemyDetailBtn.onClick:AddListener(function()
        self:_OnClickEnemyDetailBtn()
    end)

    local markInstId = args.markInstId
    local getRuntimeDataSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    if getRuntimeDataSuccess == false then
        logger.error("地图详情页获取实例数据失败" .. markInstId)
        return
    end
    local detail = markRuntimeData.detail
    if detail == nil then
        logger.error("能量淤积点详情数据中没有detailData   " .. markInstId)
    end

    local gameGroupId = detail.systemInstId
    self.m_gameGroupId = gameGroupId
    self.m_markInstId = markInstId

    self:_InitData()
    self:_InitView()
end

MapMarkDetailEnemySpawnerCtrl._OnClickEnemyDetailBtn = HL.Method() << function(self)
    self:OpenEnemyDetailPopup()
end

MapMarkDetailEnemySpawnerCtrl.OpenEnemyDetailPopup = HL.Method(HL.Opt(HL.Number)) << function(self, initSelectEnemyLuaIndex)
    local worldEnergyPointCfg = Tables.worldEnergyPointTable[self.m_gameId]
    local enemyPopupArg = {
        title = Language.LUA_WEP_ENEMY_INFO_TITLE,
        enemyListTitle = Language["ui_dungeon_enemy_popup_info_list"],
        enemyInfoTitle = Language["ui_dungeon_enemy_popup_info_desc"],
        enemyIds = worldEnergyPointCfg.enemyIds,
        enemyLevels = worldEnergyPointCfg.enemyLevels,
    }
    if initSelectEnemyLuaIndex ~= nil then
        enemyPopupArg.initSelectEnemyLuaIndex = initSelectEnemyLuaIndex
    end
    UIManager:AutoOpen(PanelId.CommonEnemyPopup, enemyPopupArg)
end

MapMarkDetailEnemySpawnerCtrl.GetRecoverPopupStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local isOpen, enemyPopupCtrl = UIManager:IsOpen(PanelId.CommonEnemyPopup)
    if not isOpen then
        return
    end
    local popupState = {
        popupType = "EnemyDetails",
    }
    if HL.TryGet(enemyPopupCtrl, "GetCurSelectEnemyLuaIndex") then
        local selectedEnemyLuaIndex = enemyPopupCtrl:GetCurSelectEnemyLuaIndex()
        if selectedEnemyLuaIndex > 0 then
            popupState.selectedEnemyLuaIndex = selectedEnemyLuaIndex
        end
    end
    return popupState
end

MapMarkDetailEnemySpawnerCtrl.TryRecoverPopupState = HL.Method(HL.Any) << function(self, popupState)
    if popupState == nil or string.isEmpty(popupState.popupType) then
        return
    end
    if popupState.popupType ~= "EnemyDetails" then
        return
    end
    local isOpen = UIManager:IsOpen(PanelId.CommonEnemyPopup)
    if isOpen then
        return
    end
    self:OpenEnemyDetailPopup(popupState.selectedEnemyLuaIndex)
end

MapMarkDetailEnemySpawnerCtrl._InitData = HL.Method() << function(self)
    self.m_gameId = GameInstance.player.worldEnergyPointSystem:GetCurSubGameId(self.m_gameGroupId)
end

MapMarkDetailEnemySpawnerCtrl._InitView = HL.Method() << function(self)
    local wepGroupCfg = Tables.worldEnergyPointGroupTable[self.m_gameGroupId]
    local wepCfg = Tables.worldEnergyPointTable[self.m_gameId]

    local commonArgs = {}
    commonArgs.bigBtnActive = true
    commonArgs.markInstId = self.m_markInstId
    commonArgs.titleText = wepCfg.gameName
    commonArgs.descText = wepCfg.desc
    self.view.mapMarkDetailCommon:InitMapMarkDetailCommon(commonArgs)

    local firstPartRewards = {}
    local firstRewardGained = GameInstance.player.worldEnergyPointSystem:IsGameGroupFirstPassRewardGained(self.m_gameGroupId)
    local rewardCfg = Tables.rewardTable[wepGroupCfg.firstPassRewardId]
    for _, itemBundle in pairs(rewardCfg.itemBundles) do
        local reward = self:_GenRewardInfo(DungeonConst.DUNGEON_REWARD_TAG_STATE.First, -1, firstRewardGained,
                                           itemBundle.id, itemBundle.count)
        table.insert(firstPartRewards, reward)
    end
    table.sort(firstPartRewards, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))
    self.m_firstRewardItemCache:Refresh(#firstPartRewards, function(cell, luaIndex)
        local reward = firstPartRewards[luaIndex]
        cell.view.rewardedCover.gameObject:SetActive(reward.gained)
        self.view.mapMarkDetailCommon:InitDetailItem(cell, reward, {
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
            tipsPosTransform = self.view.enemySpawner,
        })
    end)

    local isFull = GameInstance.player.worldEnergyPointSystem.isFull
    if isFull then
        local secondPartRewards = {}
        for i = 0, wepCfg.probGemItemIds.Count - 1 do
            local itemId = wepCfg.probGemItemIds[i]
            local reward = self:_GenRewardInfo(DungeonConst.DUNGEON_REWARD_TAG_STATE.Random, -3, false, itemId)
            table.insert(secondPartRewards, reward)
        end
        table.sort(secondPartRewards, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))

        self.m_commonRewardItemCache:Refresh(#secondPartRewards, function(cell, luaIndex)
            local reward = secondPartRewards[luaIndex]
            cell.view.rewardedCover.gameObject:SetActive(reward.gained == true)
            cell:InitItem(reward, true)
            self.view.mapMarkDetailCommon:InitDetailItem(cell, reward, {
                tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
                tipsPosTransform = self.view.enemySpawner,
            })
        end)
    end
    self.view.commonRewardNode.gameObject:SetActive(isFull)
end

MapMarkDetailEnemySpawnerCtrl._GenRewardInfo = HL.Method(HL.String, HL.Number, HL.Boolean, HL.String, HL.Opt(HL.Number)).Return(HL.Table)
        << function(self, typeTag, rewardTypeSortId, gained, itemId, itemCount)
    local itemCfg = Tables.itemTable[itemId]
    return {
        id = itemId,
        count = itemCount,
        gained = gained,


        gainedSortId = gained and 0 or 1,
        rewardTypeSortId = rewardTypeSortId,
        sortId1 = itemCfg.sortId1,
        sortId2 = itemCfg.sortId2,
    }
end

HL.Commit(MapMarkDetailEnemySpawnerCtrl)
