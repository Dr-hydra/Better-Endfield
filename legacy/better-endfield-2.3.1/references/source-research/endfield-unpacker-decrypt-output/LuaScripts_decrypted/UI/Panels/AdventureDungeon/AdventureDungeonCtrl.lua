local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.AdventureDungeon

local SeriesTableFilterItem = {
    [GEnums.DungeonCategoryType.BasicResource] = {},
    [GEnums.DungeonCategoryType.CharResource] = {},
    [GEnums.DungeonCategoryType.BossRush] = {},
    [GEnums.DungeonCategoryType.MiniBossRush] = {},
    [GEnums.DungeonCategoryType.SpecialResource] = {},
}

local IsSeriesTableFiltered = false

local TabDataList = {
    {
        type = GEnums.DungeonCategoryType.CharResource,
        tabName = "ui_AdventureDungeonPanel_title_charmaterial",
        imgPath = "icon_adventure_dg_tab_char_resource",
    },
    {
        type = GEnums.DungeonCategoryType.BasicResource,
        tabName = "ui_AdventureDungeonPanel_title_basematerial",
        imgPath = "icon_adventure_dg_tab_basic_resource",
    },
    {
        type = GEnums.DungeonCategoryType.BossRush,
        tabName = "ui_AdventureDungeonPanel_title_boss",
        imgPath = "icon_adventure_dg_tab_boss_rush",
    },
    {
        type = GEnums.DungeonCategoryType.SpecialResource,
        tabName = "ui_AdventureDungeonPanel_title_specialResource",
        imgPath = "icon_adventure_dg_tab_special_resource",
    },



    {
        type = GEnums.DungeonCategoryType.MiniBossRush,
        tabName = "ui_AdventureDungeonPanel_title_miniBoss",
        imgPath = "icon_adventure_dg_tab_mini_boss",
    },
}

AdventureDungeonCtrl = HL.Class('AdventureDungeonCtrl', uiCtrl.UICtrl)





AdventureDungeonCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_SCENE_GRADE_CHANGE_NOTIFY] = '_OnSceneGradeChangeNotify',
    [MessageConst.ON_CHANGE_ADVENTURE_DUNGEON_TAB] = '_OnChangeTab',
    [MessageConst.ON_PHASE_ADVENTURE_BOOK_BEHIND] = '_OnPhaseBehind',
    [MessageConst.ON_BLOCK_KEYBOARD_EVENT_PANEL_ORDER_CHANGED] = '_OnBlockKeyboardEventPanelOrderChanged',
}



AdventureDungeonCtrl.m_genTabCells = HL.Field(HL.Forward("UIListCache"))

AdventureDungeonCtrl.m_genCategoryCells = HL.Field(HL.Function)

AdventureDungeonCtrl.m_curTabIndex = HL.Field(HL.Number) << 1

AdventureDungeonCtrl.m_dungeonCategoryInfos = HL.Field(HL.Table)

AdventureDungeonCtrl.m_displayDungeonInfosList = HL.Field(HL.Table)

AdventureDungeonCtrl.m_forbidResetTabIndex = HL.Field(HL.Boolean) << false

AdventureDungeonCtrl.m_onGotoDungeon = HL.Field(HL.Function)

AdventureDungeonCtrl.m_naviOnLeft = HL.Field(HL.Boolean) << true


AdventureDungeonCtrl.m_dropdownDomainIds = HL.Field(HL.Table)


AdventureDungeonCtrl.m_currSelectDropdownIndex = HL.Field(HL.Number) << 0

AdventureDungeonCtrl.m_reliefNode = HL.Field(HL.Any)

AdventureDungeonCtrl.m_reliefNodeTimerId = HL.Field(HL.Number) << -1

AdventureDungeonCtrl.m_pendingReliefNodeActive = HL.Field(HL.Boolean) << false




AdventureDungeonCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitShortCut()
    self:_InitData()
    self:_UpdateData()
    self:_InitUI()
    self:_RefreshAllUI()
end

AdventureDungeonCtrl.OnClose = HL.Override() << function(self)
    self.m_reliefNodeTimerId = self:_ClearTimer(self.m_reliefNodeTimerId)

    if self.m_curTabIndex > 0 and self.m_curTabIndex <= self.m_genTabCells:GetCount() then
        self:_ReadTabRedDot(self.m_curTabIndex)
    end
end

AdventureDungeonCtrl._OnChangeTab = HL.Method(HL.Any) << function(self, arg)
    local dungeonTab = arg and arg.dungeonTab or nil
    if string.isEmpty(dungeonTab) then
        logger.error(ELogChannel.UI, "[AdventureDungeonCtrl._OnChangeTab] 跳转到指定dungeonTab失败，参数为空")
        return
    end
    self.m_currSelectDropdownIndex = (arg.filterIndex ~= nil and arg.filterIndex > 0) and arg.filterIndex or 0
    local index = 1
    for _, categoryInfo in pairs(self.m_dungeonCategoryInfos) do
        if categoryInfo.tabJumpName == dungeonTab then
            break
        end
        index = index + 1
    end
    if index > #self.m_dungeonCategoryInfos then
        logger.error(ELogChannel.UI, "[AdventureDungeonCtrl._OnChangeTab] 跳转到指定dungeonTab失败，可能是没解锁或配置错，tabName:" .. tostring(dungeonTab))
        index = 1
    end
    self.m_curTabIndex = math.min(index, #self.m_dungeonCategoryInfos)
    local cell = self.m_genTabCells:Get(self.m_curTabIndex)
    cell.toggle:SetIsOnWithoutNotify(true)
    self:SetNaviTarget(cell.toggle)
    if DeviceInfo.usingController then
        self.m_naviOnLeft = true
        InputManagerInst:ToggleGroup(self.view.slideNodeMonoTarget.groupId, not self.m_naviOnLeft)
        InputManagerInst:ToggleGroup(self.view.tabTogMonoTarget.groupId, self.m_naviOnLeft)
    end
    self:_OnClickTabToggle(self.m_curTabIndex, true)
    if arg.reopenGemTermOverviewGameGroupId ~= nil and not string.isEmpty(tostring(arg.reopenGemTermOverviewGameGroupId)) then
        local gid = arg.reopenGemTermOverviewGameGroupId
        arg.reopenGemTermOverviewGameGroupId = nil
        UIManager:Open(PanelId.GemTermOverviewPopup, gid)
    end
end

AdventureDungeonCtrl.GetCurTabName = HL.Method().Return(HL.String) << function(self)
    local curInfo = self.m_dungeonCategoryInfos[self.m_curTabIndex]
    return curInfo.tabJumpName
end

AdventureDungeonCtrl._OnBlockKeyboardEventPanelOrderChanged = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end


    local _, bookCtrl = UIManager:IsOpen(PanelId.AdventureBook)
    if bookCtrl then
        local isEnabled = InputManagerInst:IsGroupEnabled(bookCtrl.view.inputGroup.groupId)
        if isEnabled then
            InputManagerInst:ToggleGroup(self.view.slideNodeMonoTarget.groupId, not self.m_naviOnLeft)
            InputManagerInst:ToggleGroup(self.view.tabTogMonoTarget.groupId, self.m_naviOnLeft)
        else
            InputManagerInst:ToggleGroup(self.view.slideNodeMonoTarget.groupId, false)
            InputManagerInst:ToggleGroup(self.view.tabTogMonoTarget.groupId, false)
        end
    end
end


AdventureDungeonCtrl._InitUI = HL.Method() << function(self)

    self.m_genTabCells = UIUtils.genCellCache(self.view.tabTogCell)

    self.m_genCategoryCells = UIUtils.genCachedCellFunction(self.view.dungeonCategoryList)
    self.view.dungeonCategoryList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_genCategoryCells(obj)
        self:_UpdateDungeonCategory(cell, LuaIndex(csIndex))
    end)
    self.view.dungeonCategoryListNaviGroup.getDefaultSelectableFunc = function()
        local firstCell = self.m_genCategoryCells(1)
        if firstCell ~= nil then
            return firstCell.view.naviDecorator
        end
    end
end

AdventureDungeonCtrl._InitData = HL.Method() << function(self)

    if not IsSeriesTableFiltered then
        IsSeriesTableFiltered = true
        for _, item in pairs(Tables.dungeonSeriesTable) do
            local category = item.dungeonCategory
            if SeriesTableFilterItem[category] then
                table.insert(SeriesTableFilterItem[category], item)
            end
        end
    end

    self.m_onGotoDungeon = function()
        self.m_forbidResetTabIndex = true
    end

    self:_HandleStaminaDiscount()
end

AdventureDungeonCtrl.OnShow = HL.Override() << function(self)
    self.m_forbidResetTabIndex = false

    if not string.isEmpty(self.m_phase.m_dungeonTab) then
        local dungeonTab = self.m_phase.m_dungeonTab
        self.m_phase.m_dungeonTab = ""
        self:_OnChangeTab({dungeonTab = dungeonTab})
    end

    if not string.isEmpty(self.m_phase.m_reopenGemTermOverviewGameGroupId) then
        local gemGid = self.m_phase.m_reopenGemTermOverviewGameGroupId
        self.m_phase.m_reopenGemTermOverviewGameGroupId = ""
        UIManager:Open(PanelId.GemTermOverviewPopup, gemGid)
    end

    if DeviceInfo.usingController then
        local cell = self.m_genTabCells:Get(self.m_curTabIndex)
        if cell then
            self.m_naviOnLeft = true
            InputManagerInst:ToggleGroup(self.view.slideNodeMonoTarget.groupId, not self.m_naviOnLeft)
            InputManagerInst:ToggleGroup(self.view.tabTogMonoTarget.groupId, self.m_naviOnLeft)
            self:SetNaviTarget(cell.toggle)
        end
    end

    Notify(MessageConst.HIDE_ITEM_TIPS)
end

AdventureDungeonCtrl.OnHide = HL.Override() << function(self)
    if self.m_forbidResetTabIndex then
        return
    end
    local count = self.m_genTabCells:GetCount()
    if count > 0 then
        local cell = self.m_genTabCells:Get(1)
        cell.toggle.isOn = true
    end
end

AdventureDungeonCtrl._OnPhaseBehind = HL.Method() << function(self)
    if self.view.gameObject.activeSelf then
        self.m_forbidResetTabIndex = true
    end
end

AdventureDungeonCtrl._UpdateData = HL.Method() << function(self)
    self.m_dungeonCategoryInfos = {}
    for _, v in pairs(TabDataList) do
        self:_InitDataCommonDungeon(v.type, v.tabName, v.imgPath)
    end
    self:_InitDataMonsterSpawnPoint()

    self.m_curTabIndex = math.min(self.m_curTabIndex, #self.m_dungeonCategoryInfos)
end

AdventureDungeonCtrl._RefreshAllUI = HL.Method() << function(self)
    self.m_genTabCells:Refresh(#self.m_dungeonCategoryInfos, function(cell, luaIndex)
        self:_RefreshUITabCell(cell, luaIndex)
    end)
    self:_OnClickTabToggle(self.m_curTabIndex, true)
    local cell = self.m_genTabCells:Get(1)
    if cell then
        self:SetNaviTarget(cell.toggle)
    end
end

AdventureDungeonCtrl._OnSceneGradeChangeNotify = HL.Method(HL.Table) << function(self, args)
    self:_UpdateData()
    self:_RefreshAllUI()
end

AdventureDungeonCtrl._InitShortCut = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self:BindInputPlayerAction("adventure_dungeon_right", function()
        logger.info("adventure_dungeon_right")
        self.m_naviOnLeft = false
        InputManagerInst:ToggleGroup(self.view.slideNodeMonoTarget.groupId, true)
        InputManagerInst:ToggleGroup(self.view.tabTogMonoTarget.groupId, false)
        self.view.dungeonCategoryListNaviGroup:NaviToThisGroup(true)




        if not self.view.dungeonCategoryListNaviGroup.IsTopLayer then
            self.m_naviOnLeft = true
            InputManagerInst:ToggleGroup(self.view.slideNodeMonoTarget.groupId, false)
            InputManagerInst:ToggleGroup(self.view.tabTogMonoTarget.groupId, true)
        end
    end, self.view.tabTogMonoTarget.groupId)
    self:BindInputPlayerAction("adventure_dungeon_right_hint", function()
        logger.info("adventure_dungeon_right")
        self.m_naviOnLeft = false
        InputManagerInst:ToggleGroup(self.view.slideNodeMonoTarget.groupId, true)
        InputManagerInst:ToggleGroup(self.view.tabTogMonoTarget.groupId, false)
        self.view.dungeonCategoryListNaviGroup:NaviToThisGroup(true)
        if not self.view.dungeonCategoryListNaviGroup.IsTopLayer then
            self.m_naviOnLeft = true
            InputManagerInst:ToggleGroup(self.view.slideNodeMonoTarget.groupId, false)
            InputManagerInst:ToggleGroup(self.view.tabTogMonoTarget.groupId, true)
        end
    end, self.view.tabTogMonoTarget.groupId)

    self:BindInputPlayerAction("adventure_dungeon_left", function()
        logger.info("adventure_dungeon_left")
        self.m_naviOnLeft = true
        InputManagerInst:ToggleGroup(self.view.slideNodeMonoTarget.groupId, false)
        InputManagerInst:ToggleGroup(self.view.tabTogMonoTarget.groupId, true)
        self.view.tabNaviGroup:NaviToThisGroup()
    end, self.view.slideNodeMonoTarget.groupId)
    self:BindInputPlayerAction("adventure_dungeon_left_hint", function()
        logger.info("adventure_dungeon_left")
        self.m_naviOnLeft = true
        InputManagerInst:ToggleGroup(self.view.slideNodeMonoTarget.groupId, false)
        InputManagerInst:ToggleGroup(self.view.tabTogMonoTarget.groupId, true)
        self.view.tabNaviGroup:NaviToThisGroup()
    end, self.view.slideNodeMonoTarget.groupId)

    InputManagerInst:ToggleGroup(self.view.slideNodeMonoTarget.groupId, false)
end



AdventureDungeonCtrl._InitDataCommonDungeon = HL.Method(HL.Any, HL.String, HL.String)
    << function(self, categoryType, tabName, tabImg)

    local isCategoryUnlocked = GameInstance.player.adventure:IsAdventureDungeonFirCategoryUnlock(categoryType)

    if not isCategoryUnlocked then
        return
    end

    local tableDataList = SeriesTableFilterItem[categoryType]
    if (#tableDataList <= 0) then
        return
    end

    local newCategoryInfo = {
        tabName = tabName,
        tabImgPath = tabImg,
        tabJumpName = categoryType:ToString(),
        dungeonInfosList = {},
        subGameIds = {},
        showRelief = false,
    }

    local tempTable = {}
    for _, tableData in pairs(tableDataList) do
        local isUnlocked = GameInstance.player.adventure:IsAdventureDungeonCategoryTypeUnlocked(tableData.id, tableData.dungeonCategory)

        if isUnlocked then
            local category2ndType = tableData.dungeonCategory2nd
            local infosBundle = tempTable[category2ndType]
            if not infosBundle then
                local cfgExist, category2ndTypeCfg = Tables.DungeonCategory2ndTable:TryGetValue(category2ndType)
                infosBundle = {
                    category2ndType = category2ndType:GetHashCode(),
                    name = cfgExist and category2ndTypeCfg.name or "",
                    infos = {},
                }
                tempTable[tableData.dungeonCategory2nd] = infosBundle
            end

            local info = self:_HandleAndCreateSeriesInfo(tableData, newCategoryInfo)
            if info then

                table.insert(infosBundle.infos, info)
            end
            if info.costStamina then
                newCategoryInfo.showRelief = true
            end
        end
    end

    for _, v in pairs(tempTable) do
        table.sort(v.infos, Utils.genSortFunction({ "sortId" }, true))
        table.insert(newCategoryInfo.dungeonInfosList, v)
    end
    table.sort(newCategoryInfo.dungeonInfosList, Utils.genSortFunction({ "category2ndType" }, true))

    if categoryType == GEnums.DungeonCategoryType.MiniBossRush then
        for _, infosBundle in pairs(newCategoryInfo.dungeonInfosList) do
            for _, info in pairs(infosBundle.infos) do
                local allGained = true
                for _, subGameId in pairs(info.subGameIds) do
                    if GameInstance.dungeonManager:IsDungeonUnlocked(subGameId) and
                       not GameInstance.dungeonManager:IsDungeonFirstPassRewardGained(subGameId) then
                        allGained = false
                        break
                    end
                end
                info.allFirstPassGained = allGained and 1 or 0
            end
            table.sort(infosBundle.infos, Utils.genSortFunction({ "allFirstPassGained", "sortId" }, true))
        end
    end

    table.insert(self.m_dungeonCategoryInfos, newCategoryInfo)
end

AdventureDungeonCtrl._HandleAndCreateSeriesInfo = HL.Method(HL.Any,  HL.Table).Return(HL.Table)
    << function(self, seriesCfg, categoryInfo)

    local hasCfg, dungeonTypeCfg = Tables.dungeonTypeTable:TryGetValue(seriesCfg.gameCategory)
    if hasCfg then
        local dungeonCategory = seriesCfg.dungeonCategory
        local seriesId = seriesCfg.id
        local isActive = (
            dungeonCategory == GEnums.DungeonCategoryType.BossRush or
            dungeonCategory == GEnums.DungeonCategoryType.MiniBossRush or
            GameInstance.dungeonManager:IsDungeonInteractiveActive(seriesId)
        )
        local info = {
            seriesId = seriesId,
            sortId = seriesCfg.sortId,
            mapMarkType = dungeonTypeCfg.mapMarkType,
            dungeonImg = self:_GetDungeonImg(seriesCfg.includeDungeonIds),
            dungeonRoleImg = seriesCfg.dungeonRoleImg,
            dungeonName = seriesCfg.name,
            dungeonCategory = dungeonCategory,
            staminaTxt = "",
            isActive = isActive,
            rewardInfos = self:_ProcessDungeonSeriesRewards(seriesId,
                (dungeonCategory == GEnums.DungeonCategoryType.CharResource or
                    dungeonCategory == GEnums.DungeonCategoryType.BasicResource)),
            subGameIds = {},
            onGotoDungeon = self.m_onGotoDungeon,
            costStamina = false,
        }

        local minStaminaCost = math.maxinteger
        local maxStaminaCost = math.mininteger
        for _, subGameId in pairs(seriesCfg.includeDungeonIds) do
            table.insert(info.subGameIds, subGameId)
            table.insert(categoryInfo.subGameIds, subGameId)
            local cfg = Utils.tryGetTableCfg(Tables.gameMechanicTable, subGameId)
            local isUnlocked = GameInstance.dungeonManager:IsDungeonUnlocked(subGameId)
            if cfg and isUnlocked then
                minStaminaCost = ActivityUtils.getRealStaminaCost(math.min(minStaminaCost, cfg.costStamina))
                maxStaminaCost = ActivityUtils.getRealStaminaCost(math.max(maxStaminaCost, cfg.costStamina))
                if cfg.costStamina > 0 then
                    info.costStamina = true
                end
            end
        end

        local count = #info.subGameIds
        if count <= 0 or maxStaminaCost <= 0 then
            if dungeonCategory ~= GEnums.DungeonCategoryType.MiniBossRush then
                info.staminaTxt = "0"
            end
        elseif count == 1 then
            info.staminaTxt = tostring(maxStaminaCost)
            info.staminaMin = maxStaminaCost
            info.staminaMax = maxStaminaCost
        else
            info.staminaMin = minStaminaCost
            info.staminaMax = maxStaminaCost
            if info.staminaMin ~= info.staminaMax then
                info.staminaTxt = info.staminaMin .. "~" .. info.staminaMax
            else
                info.staminaTxt = info.staminaMin
            end
        end

        if dungeonCategory == GEnums.DungeonCategoryType.BossRush then
            self:_ProcessDungeonBossInfo(seriesId, info)
        end
        if dungeonCategory == GEnums.DungeonCategoryType.SpecialResource then
            self:_ProcessDungeonBossInfo(seriesId, info)
            info.dungeonImg = seriesCfg.dungeonImg
        end

        return info
    end

    return nil
end

AdventureDungeonCtrl._InitDataMonsterSpawnPoint = HL.Method() << function(self)

    local newCategoryInfo = {
        tabName = "ui_AdventureDungeonPanel_title_enemyspawner",
        tabImgPath = "icon_adventure_dg_tab_monster_spawn",
        tabJumpName = "EnemySpawner",
        dungeonInfosList = {},
        subGameIds = {},
        redDotArg = {},
        showRelief = GameInstance.player.worldEnergyPointSystem.isFull,
        redDotName = "AdventureDungeonTabMonsterSpawnPoint",
    }

    local infosBundle = {
        category2ndType = GEnums.DungeonCategory2ndType.None:GetHashCode(),
        name = "",
        infos = {},
    }
    table.insert(newCategoryInfo.dungeonInfosList, infosBundle)

    self.m_dropdownDomainIds = { "All" }

    for groupId, tableData in pairs(Tables.worldEnergyPointGroupTable) do
        local id = GameInstance.player.worldEnergyPointSystem:GetCurSubGameId(groupId)
        local canShow = false
        if GameInstance.player.subGameSys:IsGameMapMarkUnlock(groupId, GEnums.MarkType.EnemySpawner) and
            GameInstance.player.subGameSys:IsGameUnlocked(id) and
            AdventureBookUtils.CheckEnemySpawnerCanOpenMap(groupId) then
            canShow = true
        end
        if canShow and id ~= nil then
            local hasCfg, gameCfg = Tables.gameMechanicTable:TryGetValue(id)
            if hasCfg then
                local info = {
                    seriesId = groupId,
                    gameGroupId = groupId,
                    sortId = groupId,
                    mapMarkType = GEnums.MarkType.EnemySpawner,
                    dungeonRoleImg = tableData.icon,
                    dungeonName = gameCfg.gameName,
                    staminaTxt = gameCfg.costStamina == 0 and
                        "" or tostring(ActivityUtils.getRealStaminaCost(gameCfg.costStamina)),
                    isActive = true,
                    rewardInfos = AdventureDungeonCtrl._ProcessMonsterSpawnRewards(groupId, id),
                    subGameIds = { id },
                    redDotArg = groupId,
                    redDotName = "AdventureDungeonCellMonsterSpawnPoint",
                    onGotoDungeon = self.m_onGotoDungeon,
                }
                local gemRandId = tableData.gemRandId
                local _, domainId = Tables.GemItemDomainTable:TryGetValue(gemRandId)
                info.domainId = domainId
                table.insert(infosBundle.infos, info)
                table.insert(newCategoryInfo.subGameIds, id)
                table.insert(newCategoryInfo.redDotArg, groupId)

                if (lume.find(self.m_dropdownDomainIds, domainId)) == nil then
                    table.insert(self.m_dropdownDomainIds, domainId)
                end
            else
                logger.error("[Game Mechanic Table] missing, id = " .. id)
            end
        end
    end

    if #newCategoryInfo.subGameIds <= 0 then
        return
    end
    table.sort(infosBundle.infos, Utils.genSortFunction({ "sortId" }, true))

    local insertIndex = #self.m_dungeonCategoryInfos + 1
    local miniBossRushTabName = GEnums.DungeonCategoryType.MiniBossRush:ToString()
    for index, categoryInfo in ipairs(self.m_dungeonCategoryInfos) do
        if categoryInfo.tabJumpName == miniBossRushTabName then
            insertIndex = index
            break
        end
    end
    table.insert(self.m_dungeonCategoryInfos, insertIndex, newCategoryInfo)
end

AdventureDungeonCtrl._GetDungeonCustomRewardId = HL.StaticMethod(HL.String).Return(HL.String) << function(dungeonId)
    local hasCfg, dungeonCfg = Tables.dungeonTable:TryGetValue(dungeonId)
    if hasCfg and not string.isEmpty(dungeonCfg.customRewardId) then
        return dungeonCfg.customRewardId
    end
    return ""
end

AdventureDungeonCtrl._AddRewardItemsMerged = HL.StaticMethod(HL.String, HL.Table, HL.Number)
    << function(rewardId, rewards, rewardTypeSortId)
    if string.isEmpty(rewardId) then
        return
    end
    local hasCfg, rewardsCfg = Tables.rewardTable:TryGetValue(rewardId)
    if hasCfg then
        for _, itemBundle in pairs(rewardsCfg.itemBundles) do
            local itemId = itemBundle.id
            local reward = rewards[itemId]
            if not reward then
                local itemCfg = Tables.itemTable[itemId]
                reward = {
                    id = itemId,
                    rarity = itemCfg.rarity,
                    type = itemCfg.type:ToInt(),

                    gainedSortId = 1,
                    rewardTypeSortId = rewardTypeSortId,
                    gained = false,
                }
                rewards[itemId] = reward
            end
        end
    end
end

AdventureDungeonCtrl._AddRewardItemsNoMerge = HL.StaticMethod(HL.String, HL.Table, HL.Boolean, HL.Boolean, HL.Number, HL.Number, HL.Boolean)
    << function(rewardId, rewards, isFirst, isExtra, gainedSortId, rewardTypeSortId, gained)
    if string.isEmpty(rewardId) then
        return
    end
    local _, rewardsCfg = Tables.rewardTable:TryGetValue(rewardId)
    if rewardsCfg then
        for _, itemBundle in pairs(rewardsCfg.itemBundles) do
            local itemCfg = Tables.itemTable[itemBundle.id]
            local reward = {
                id = itemBundle.id,
                rarity = itemCfg.rarity,
                type = itemCfg.type:ToInt(),

                isFirst = isFirst,
                isExtra = isExtra,
                gainedSortId = gainedSortId,
                rewardTypeSortId = rewardTypeSortId,
                gained = gained,
            }
            table.insert(rewards, reward)
        end
    end
end




AdventureDungeonCtrl._RefreshUITabCell = HL.Method(HL.Table, HL.Number) << function(self, cell, luaIndex)
    local tabInfo = self.m_dungeonCategoryInfos[luaIndex]

    cell.gameObject.name = luaIndex

    cell.tabNameTxt.text = Language[tabInfo.tabName]
    cell.tabImg:LoadSprite(UIConst.UI_SPRITE_ADVENTURE, tabInfo.tabImgPath)
    cell.toggle.isOn = luaIndex == self.m_curTabIndex

    cell.toggle.onValueChanged:RemoveAllListeners()
    cell.toggle.onValueChanged:AddListener(function(isOn)
        if isOn and self.m_curTabIndex ~= luaIndex then
            self:_OnClickTabToggle(luaIndex)
        end
    end)

    cell.toggle.onHoverChange:RemoveAllListeners()
    cell.toggle.onHoverChange:AddListener(function(isHover)
        if cell.toggle.isOn then
            return
        end
        if isHover then
            cell.aniWrap:Play("adventuredungeontabtogcell_hover")
        else
            cell.aniWrap:Play("adventuredungeontabtogcell_normal")
        end
    end)

    cell.toggle.onIsNaviTargetChanged = function(isTarget, isGroupChanged)
        if isTarget then
            cell.toggle.isOn = true
        end
    end

    if not string.isEmpty(tabInfo.redDotName) then
        cell.redDot:InitRedDot(tabInfo.redDotName, tabInfo.redDotArg)
    else
        cell.redDot:InitRedDot("AdventureDungeonTab", tabInfo.subGameIds)
    end

    cell.reliefTab.gameObject:SetActive(ActivityUtils.hasStaminaReduceCount() and tabInfo.showRelief)
end

AdventureDungeonCtrl._OnClickTabToggle = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, luaIndex, isInit)
    local count = #self.m_dungeonCategoryInfos
    if count < luaIndex or luaIndex < 1 then
        return
    end
    self:_ToggleReliefNode(ActivityUtils.hasStaminaReduceCount() and self.m_dungeonCategoryInfos[luaIndex].showRelief)
    local preIndex = self.m_curTabIndex
    if (preIndex ~= luaIndex) then

        self:_ReadTabRedDot(preIndex)
    end

    if DeviceInfo.usingController and self.m_curTabIndex ~= luaIndex then
        AudioAdapter.PostEvent("Au_UI_Toggle_Common_On")
    end
    self.m_curTabIndex = luaIndex
    local categoryInfo = self.m_dungeonCategoryInfos[luaIndex]
    local dungeonInfosList = categoryInfo.dungeonInfosList
    self.m_displayDungeonInfosList = self:_ResolveDisplayDungeonInfosList(categoryInfo, dungeonInfosList)
    local listCount = #self.m_displayDungeonInfosList
    self.view.dungeonCategoryList.gameObject:SetActiveIfNecessary(listCount > 0)
    self.view.dungeonCategoryList:UpdateCount(listCount, true)

    if listCount > 0 then
        local infoBundle = dungeonInfosList[1]
        if string.isEmpty(infoBundle.name) then
            self.view.titleBgMask.gameObject:SetActiveIfNecessary(true)
        else
            self.view.titleBgMask.gameObject:SetActiveIfNecessary(false)
        end
    else
        self.view.titleBgMask.gameObject:SetActiveIfNecessary(true)
    end

    if categoryInfo.tabJumpName == "EnemySpawner" then
        self.view.siltationPoint.gameObject:SetActive(true)
        self.view.dropDown:ClearComponent()
        self.view.dropDown:Init(

            function(csIndex, option, _)
                local domainId = self.m_dropdownDomainIds[csIndex + 1]
                local textId = "LUA_ADVENTURE_DUNGEON_ENEMY_SPAWNER_DROPNDOWN_" .. tostring(domainId)
                option:SetText(Language[textId])
            end,

            function(csIndex)
                self:_OnSelectFilter(LuaIndex(csIndex))
            end
        )
        local dropdownCsIdx = self.m_currSelectDropdownIndex > 0 and CSIndex(self.m_currSelectDropdownIndex) or 0
        self.view.dropDown:Refresh(#self.m_dropdownDomainIds, dropdownCsIdx, false)
    else
        self.view.siltationPoint.gameObject:SetActive(false)
    end

    if not isInit then
        local aniWrapper = self.animationWrapper
        aniWrapper:Play("adventuredungeonnode_change")
    end
end

AdventureDungeonCtrl._ResolveDisplayDungeonInfosList = HL.Method(HL.Table, HL.Table).Return(HL.Table) << function(self, categoryInfo, dungeonInfosList)
    dungeonInfosList = dungeonInfosList or {}
    if categoryInfo.tabJumpName == "EnemySpawner" then
        local filterIndex = self.m_currSelectDropdownIndex > 0 and self.m_currSelectDropdownIndex or 1
        return self:_BuildEnemySpawnerDisplayList(dungeonInfosList[1], self.m_dropdownDomainIds[filterIndex])
    end

    if #dungeonInfosList == 1 and #dungeonInfosList[1].infos > 1 then
        return self:_BuildSingleInfoDisplayList(dungeonInfosList[1])
    end
    return dungeonInfosList
end

AdventureDungeonCtrl._BuildSingleInfoDisplayList = HL.Method(HL.Table).Return(HL.Table) << function(self, infosBundle)
    local displayList = {}
    infosBundle.hasRead = true
    for _, info in ipairs(infosBundle.infos) do
        table.insert(displayList, {
            category2ndType = infosBundle.category2ndType,
            name = infosBundle.name,
            infos = { info },
            hasRead = true,
        })
    end
    return displayList
end

AdventureDungeonCtrl._BuildEnemySpawnerDisplayList = HL.Method(HL.Table, HL.Opt(HL.String)).Return(HL.Table) << function(self, infosBundle, selectDomainId)
    local displayList = {}
    infosBundle.hasRead = true
    for _, info in ipairs(infosBundle.infos) do
        if selectDomainId == "All" or info.domainId == selectDomainId then
            table.insert(displayList, {
                category2ndType = infosBundle.category2ndType,
                name = infosBundle.name,
                infos = { info },
                hasRead = true,
            })
        end
    end
    return displayList
end

AdventureDungeonCtrl._OnSelectFilter = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, index, forceSelect)
    logger.info("[adventure] dropdown index: " .. tostring(index))
    if index == self.m_currSelectDropdownIndex and not forceSelect then
        return
    end
    self.m_currSelectDropdownIndex = index
    self.view.dungeonCategoryList:ScrollToIndex(0, true)
    local infosBundle = self.m_dungeonCategoryInfos[self.m_curTabIndex].dungeonInfosList[1]
    local selectDomainId = self.m_dropdownDomainIds[index]
    self.m_displayDungeonInfosList = self:_BuildEnemySpawnerDisplayList(infosBundle, selectDomainId)
    self.view.dungeonCategoryList:UpdateCount(#self.m_displayDungeonInfosList, true)
end

AdventureDungeonCtrl._ReadTabRedDot = HL.Method(HL.Number) << function(self, luaIndex)
    if luaIndex <= 0 or luaIndex > #self.m_dungeonCategoryInfos then
        return
    end

    local subGameIds = {}
    local categoryInfo = self.m_dungeonCategoryInfos[luaIndex]
    for _, infosBundle in pairs(categoryInfo.dungeonInfosList) do
        if infosBundle.hasRead then
            for _, info in pairs(infosBundle.infos) do
                if info.hasRead then
                    for _, id in pairs(info.subGameIds) do
                        table.insert(subGameIds, id)
                    end
                end
            end
        end
    end
    if (#subGameIds) > 0 then
        GameInstance.player.subGameSys:SendSubGameListRead(subGameIds)
    end
end

AdventureDungeonCtrl.GetCurEnemySpawnerFilterIndex = HL.Method().Return(HL.Number) << function(self)
    return self.m_currSelectDropdownIndex
end




AdventureDungeonCtrl._UpdateDungeonCategory = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local infosBundle = self.m_displayDungeonInfosList and self.m_displayDungeonInfosList[luaIndex] or nil
    if infosBundle == nil then
        return
    end
    cell:InitDungeonCategoryCell(infosBundle)
end

AdventureDungeonCtrl._ProcessDungeonSeriesRewards = HL.Method(HL.String, HL.Boolean).Return(HL.Table) << function(self, seriesId, checkUnlocked)
    local dungeonSeriesCfg = Tables.dungeonSeriesTable[seriesId]
    if not dungeonSeriesCfg then
        return {}
    end

    local rewardList = {}
    if dungeonSeriesCfg.dungeonCategory == GEnums.DungeonCategoryType.Challenge then
        for _, v in pairs(dungeonSeriesCfg.includeDungeonIds) do
            self:_ProcessDungeonRewardsNoMerge(v, rewardList)
        end
    elseif dungeonSeriesCfg.dungeonCategory == GEnums.DungeonCategoryType.BossRush or
        dungeonSeriesCfg.dungeonCategory == GEnums.DungeonCategoryType.SpecialResource then
        local dungeonIds = {}
        for _, v in pairs(dungeonSeriesCfg.includeDungeonIds) do
            table.insert(dungeonIds, v)
        end
        self:_ProcessDungeonRewardsBoss(dungeonIds, rewardList)
    elseif dungeonSeriesCfg.dungeonCategory == GEnums.DungeonCategoryType.MiniBossRush then
        local dungeonIds = {}
        for _, v in pairs(dungeonSeriesCfg.includeDungeonIds) do
            table.insert(dungeonIds, v)
        end
        self:_ProcessDungeonRewardsMiniBoss(dungeonIds, rewardList)
    else
        local rewards = {}
        for _, v in pairs(dungeonSeriesCfg.includeDungeonIds) do
            self:_ProcessDungeonRewards(v, rewards, checkUnlocked)
        end
        for _, v in pairs(rewards) do
            table.insert(rewardList, v)
        end
    end
    table.sort(rewardList, Utils.genSortFunction({ "gainedSortId", "rewardTypeSortId", "rarity", "type" }))
    return rewardList
end

AdventureDungeonCtrl._ProcessDungeonRewards = HL.Method(HL.String, HL.Table, HL.Boolean) << function(self, dungeonId, rewards, checkUnlocked)
    if checkUnlocked and not GameInstance.dungeonManager:IsDungeonUnlocked(dungeonId) then
        logger.info("[dungeon] 跳过了" .. tostring(dungeonId))
        return
    end

    local gameMechanicCfg = Tables.gameMechanicTable[dungeonId]

    local hasFirstReward = not string.isEmpty(gameMechanicCfg.firstPassRewardId)
    local customRewardId = AdventureDungeonCtrl._GetDungeonCustomRewardId(dungeonId)
    local hasRecycleReward = not string.isEmpty(gameMechanicCfg.rewardId) or not string.isEmpty(customRewardId)
    local hasExtraReward = not string.isEmpty(gameMechanicCfg.extraRewardId)


    if hasFirstReward then
        local succ, rewardsCfg = Tables.rewardTable:TryGetValue(gameMechanicCfg.firstPassRewardId)
        if succ then
            for _, itemBundle in pairs(rewardsCfg.itemBundles) do
                local itemId = itemBundle.id
                local reward = rewards[itemId]
                if not reward then
                    local itemCfg = Tables.itemTable[itemId]
                    reward = {
                        id = itemId,
                        rarity = itemCfg.rarity,
                        type = itemCfg.type:ToInt(),

                        gainedSortId = 1,
                        rewardTypeSortId = 3,
                        gained = false,
                    }
                    rewards[itemId] = reward
                end
            end
        end
    end


    if hasRecycleReward then
        AdventureDungeonCtrl._AddRewardItemsMerged(gameMechanicCfg.rewardId, rewards, 1)
    end
    AdventureDungeonCtrl._AddRewardItemsMerged(customRewardId, rewards, 1)


    if hasExtraReward then
        local _, rewardsCfg = Tables.rewardTable:TryGetValue(gameMechanicCfg.extraRewardId)
        if rewardsCfg then
            for _, itemBundle in pairs(rewardsCfg.itemBundles) do
                local itemId = itemBundle.id
                local reward = rewards[itemId]
                if not reward then
                    local itemCfg = Tables.itemTable[itemId]
                    reward = {
                        id = itemId,
                        rarity = itemCfg.rarity,
                        type = itemCfg.type:ToInt(),

                        gainedSortId = 1,
                        rewardTypeSortId = 2,
                        gained = false,
                    }
                    rewards[itemId] = reward
                end
            end
        end
    end
end

AdventureDungeonCtrl._ProcessDungeonRewardsNoMerge = HL.Method(HL.String, HL.Table) << function(self, dungeonId, rewards)
    local dungeonMgr = GameInstance.dungeonManager
    local gameMechanicCfg = Tables.gameMechanicTable[dungeonId]

    local hasFirstReward = not string.isEmpty(gameMechanicCfg.firstPassRewardId)
    local customRewardId = AdventureDungeonCtrl._GetDungeonCustomRewardId(dungeonId)
    local hasRecycleReward = not string.isEmpty(gameMechanicCfg.rewardId) or not string.isEmpty(customRewardId)
    local hasExtraReward = not string.isEmpty(gameMechanicCfg.extraRewardId)


    if hasFirstReward then
        local firstRewardGained = dungeonMgr:IsDungeonFirstPassRewardGained(dungeonId)
        local hideFirstReward = firstRewardGained and hasRecycleReward
        if not hideFirstReward then
            local _, rewardsCfg = Tables.rewardTable:TryGetValue(gameMechanicCfg.firstPassRewardId)
            if rewardsCfg then
                for _, itemBundle in pairs(rewardsCfg.itemBundles) do
                    local itemCfg = Tables.itemTable[itemBundle.id]
                    local reward = {
                        id = itemBundle.id,
                        rarity = itemCfg.rarity,
                        type = itemCfg.type:ToInt(),

                        isFirst = true,
                        isExtra = false,
                        gainedSortId = firstRewardGained and 1 or 2,
                        rewardTypeSortId = 3,
                        gained = firstRewardGained,
                    }
                    table.insert(rewards, reward)
                end
            end
        end
    end


    AdventureDungeonCtrl._AddRewardItemsNoMerge(gameMechanicCfg.rewardId, rewards, false, false, 1, 1, false)
    AdventureDungeonCtrl._AddRewardItemsNoMerge(customRewardId, rewards, false, false, 1, 1, false)


    if hasExtraReward then
        local extraRewardGained = dungeonMgr:IsDungeonExtraRewardGained(dungeonId)
        local hideExtraReward = extraRewardGained and hasRecycleReward
        if not hideExtraReward then
            local _, rewardsCfg = Tables.rewardTable:TryGetValue(gameMechanicCfg.extraRewardId)
            if rewardsCfg then
                for _, itemBundle in pairs(rewardsCfg.itemBundles) do
                    local itemCfg = Tables.itemTable[itemBundle.id]
                    local reward = {
                        id = itemBundle.id,
                        rarity = itemCfg.rarity,
                        type = itemCfg.type:ToInt(),

                        isFirst = false,
                        isExtra = true,
                        gainedSortId = extraRewardGained and 1 or 2,
                        rewardTypeSortId = 2,
                        gained = extraRewardGained,
                    }
                    table.insert(rewards, reward)
                end
            end
        end
    end
end


AdventureDungeonCtrl._ProcessDungeonRewardsBoss = HL.Method(HL.Table, HL.Table) << function(self, dungeonIds, rewards)
    local dungeonMgr = GameInstance.dungeonManager

    local lastUnlockedDungeonId = ""
    for _, dungeonId in ipairs(dungeonIds) do
        if dungeonMgr:IsDungeonUnlocked(dungeonId) then
            lastUnlockedDungeonId = dungeonId
        end
    end
    if (lastUnlockedDungeonId == "") then
        return
    end

    local gameMechanicCfg = Tables.gameMechanicTable[lastUnlockedDungeonId]
    local dungeonCfg = Tables.dungeonTable[lastUnlockedDungeonId]

    local hasFirstReward = not string.isEmpty(gameMechanicCfg.firstPassRewardId)
    local hasHunterReward = not string.isEmpty(gameMechanicCfg.hunterModeRewardId)
    local isHunterModeUnlocked = DungeonUtils.isHunterModeUnlocked()


    if hasFirstReward then
        local firstRewardGained = dungeonMgr:IsDungeonFirstPassRewardGained(lastUnlockedDungeonId)

        local notShow = isHunterModeUnlocked and hasHunterReward and firstRewardGained
        if not notShow then
            local _, rewardsCfg = Tables.rewardTable:TryGetValue(gameMechanicCfg.firstPassRewardId)
            if rewardsCfg then
                for _, itemBundle in pairs(rewardsCfg.itemBundles) do
                    local itemCfg = Tables.itemTable[itemBundle.id]
                    local reward = {
                        id = itemBundle.id,
                        rarity = itemCfg.rarity,
                        type = itemCfg.type:ToInt(),

                        isFirst = true,
                        isExtra = false,
                        gainedSortId = firstRewardGained and 1 or 2,
                        rewardTypeSortId = 3,
                        gained = firstRewardGained,
                    }
                    table.insert(rewards, reward)
                end
            end
        end
    end


    if hasHunterReward then
        local _, rewardCfg = Tables.rewardTable:TryGetValue(dungeonCfg.hunterModeRewardId)
        if rewardCfg then

            for _, itemBundle in pairs(rewardCfg.itemBundles) do
                local itemCfg = Tables.itemTable[itemBundle.id]
                local reward = {
                    id = itemCfg.id,
                    rarity = itemCfg.rarity,
                    type = itemCfg.type:ToInt(),

                    isFirst = false,
                    isExtra = false,
                    gainedSortId = 1,
                    rewardTypeSortId = 2,
                    gained = false,
                }
                table.insert(rewards, reward)
            end


            for _, itemBundle in pairs(rewardCfg.probItemBundles) do
                local itemId = itemBundle.id
                local succ, itemCfg = Tables.itemTable:TryGetValue(itemId)
                if succ then
                    local reward = {
                        id = itemCfg.id,
                        rarity = itemCfg.rarity,
                        type = itemCfg.type:ToInt(),

                        isFirst = false,
                        isExtra = false,
                        gainedSortId = 1,
                        rewardTypeSortId = 1,
                        gained = false,
                    }
                    table.insert(rewards, reward)
                end
            end
        end
    end
end

AdventureDungeonCtrl._ProcessDungeonRewardsMiniBoss = HL.Method(HL.Table, HL.Table) << function(self, dungeonIds, rewards)
    local dungeonMgr = GameInstance.dungeonManager
    local lastUnlockedDungeonId = ""
    for _, dungeonId in ipairs(dungeonIds) do
        if dungeonMgr:IsDungeonUnlocked(dungeonId) then
            lastUnlockedDungeonId = dungeonId
        end
    end
    if lastUnlockedDungeonId == "" then
        return
    end

    local gameMechanicCfg = Tables.gameMechanicTable[lastUnlockedDungeonId]
    local hasFirstReward = not string.isEmpty(gameMechanicCfg.firstPassRewardId)
    if hasFirstReward then
        local firstRewardGained = dungeonMgr:IsDungeonFirstPassRewardGained(lastUnlockedDungeonId)
        local _, rewardsCfg = Tables.rewardTable:TryGetValue(gameMechanicCfg.firstPassRewardId)
        if rewardsCfg then
            for _, itemBundle in pairs(rewardsCfg.itemBundles) do
                local itemCfg = Tables.itemTable[itemBundle.id]
                table.insert(rewards, {
                    id = itemBundle.id,
                    rarity = itemCfg.rarity,
                    type = itemCfg.type:ToInt(),
                    isFirst = true,
                    isExtra = false,
                    gainedSortId = firstRewardGained and 1 or 2,
                    rewardTypeSortId = 3,
                    gained = firstRewardGained,
                })
            end
        end
    end
end

AdventureDungeonCtrl._ProcessDungeonBossInfo = HL.Method(HL.String, HL.Table) << function(self, seriesId, info)
    local dungeonSeriesData = Tables.DungeonSeriesTable[seriesId]

    local lastUnlockedDungeonId = ""
    for _, dungeonId in pairs(dungeonSeriesData.includeDungeonIds) do
        if GameInstance.dungeonManager:IsDungeonUnlocked(dungeonId) then
            lastUnlockedDungeonId = dungeonId
        end
    end
    if lastUnlockedDungeonId == "" then
        return
    end

    info.isHunterMode = DungeonUtils.isDungeonHasHunterMode(lastUnlockedDungeonId) and
        DungeonUtils.isHunterModeUnlocked()
    local dungeonData = Tables.DungeonTable[lastUnlockedDungeonId]
    if info.isHunterMode then
        info.staminaTxt = tostring(ActivityUtils.getRealStaminaCost(dungeonData.hunterModeCostStamina))
        if dungeonData.hunterModeCostStamina > 0 then
            info.costStamina = true
        end
    end
end


AdventureDungeonCtrl._GetDungeonImg = HL.Method(HL.Any).Return(HL.Opt(HL.String)) << function(self, dungeonIds)
    local ret = nil
    for _, dungeonId in pairs(dungeonIds) do
        local unlocked = GameInstance.dungeonManager:IsDungeonUnlocked(dungeonId)
        if unlocked then
            local _, dungeonCfg = Tables.DungeonTable:TryGetValue(dungeonId)
            if dungeonCfg then
                ret = dungeonCfg.dungeonImg
            end
        end
    end
    return ret
end

AdventureDungeonCtrl._ProcessMonsterSpawnRewards = HL.StaticMethod(HL.String, HL.String).Return(HL.Table) << function(groupId, gameId)
    local rewards = {}
    local rewardList = {}

    local isFull = GameInstance.player.worldEnergyPointSystem.isFull

    local wepGroupCfg = Tables.worldEnergyPointGroupTable[groupId]
    local wepCfg = Tables.worldEnergyPointTable[gameId]
    local firstRewardGained = GameInstance.player.worldEnergyPointSystem:IsGameGroupFirstPassRewardGained(groupId)
    if not firstRewardGained or not isFull then
        local firstPartRewards = {}
        local _, rewardCfg = Tables.rewardTable:TryGetValue(wepGroupCfg.firstPassRewardId)
        if rewardCfg then
            for _, itemBundle in pairs(rewardCfg.itemBundles) do
                local reward = AdventureDungeonCtrl._GenRewardInfo(DungeonConst.DUNGEON_REWARD_TAG_STATE.First, -1, firstRewardGained,
                    itemBundle.id, itemBundle.count)
                table.insert(firstPartRewards, reward)
            end
            table.sort(firstPartRewards, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))
            for _, item in pairs(firstPartRewards) do
                item.isFirst = true,
                table.insert(rewardList, item)
            end
        end
    end

    if isFull then
        local secondPartRewards = {}
        for i = 0, wepCfg.probGemItemIds.Count - 1 do
            local itemId = wepCfg.probGemItemIds[i]
            local reward = AdventureDungeonCtrl._GenRewardInfo(DungeonConst.DUNGEON_REWARD_TAG_STATE.Random, -3, false, itemId)
            table.insert(secondPartRewards, reward)
        end
        table.sort(secondPartRewards, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))

        for _, item in pairs(secondPartRewards) do
            item.isFirst = false,
            table.insert(rewardList, item)
        end
    end

    return rewardList
end

AdventureDungeonCtrl._GenRewardInfo = HL.StaticMethod(HL.String, HL.Number, HL.Boolean, HL.String, HL.Opt(HL.Number)).Return(HL.Table)
    << function(tagState, rewardTypeSortId, gained, itemId, itemCount)
    local itemCfg = Tables.itemTable[itemId]
    return {
        id = itemId,
        count = itemCount,
        gained = gained,
        tagState = tagState,

        gainedSortId = gained and 0 or 1,
        rewardTypeSortId = rewardTypeSortId,
        sortId1 = itemCfg.sortId1,
        sortId2 = itemCfg.sortId2,
    }
end




AdventureDungeonCtrl.m_discount = HL.Field(HL.Number) << 0

AdventureDungeonCtrl._HandleStaminaDiscount = HL.Method() << function(self)
    self.m_discount = GameInstance.player.activitySystem.staminaDiscount
    self:_RefreshReliefNode()
end

AdventureDungeonCtrl._EnsureReliefNode = HL.Method().Return(HL.Any) << function(self)
    if not self.view or not self.view.dungeonCategoryList then return end
    if self.m_reliefNode then
        return self.m_reliefNode
    end
    local prefab = self:LoadGameObject("Assets/Beyond/DynamicAssets/Gameplay/UI/Prefabs/Adventure/Widgets/AdventureDungeonReliefNode.prefab")
    local obj = CSUtils.CreateObject(prefab, self.view.dungeonCategoryList.transform.parent)
    obj.name = "ReliefNode"
    local transform = obj.transform
    transform:SetSiblingIndex(0)
    transform.localScale = Vector3.one

    self.m_reliefNode = Utils.wrapLuaNode(obj)
    self.m_reliefNode.gameObject:SetActive(false)
    return self.m_reliefNode
end

AdventureDungeonCtrl._ToggleReliefNode = HL.Method(HL.Boolean) << function(self, active)
    self.m_pendingReliefNodeActive = active
    if not active then
        self.m_reliefNodeTimerId = self:_ClearTimer(self.m_reliefNodeTimerId)
        if self.m_reliefNode then
            self.m_reliefNode.gameObject:SetActiveIfNecessary(false)
        end
        return
    end
    if not self.m_reliefNode then
        self.m_reliefNodeTimerId = TimerManager:StartFrameTimer(1, function()
            self.m_reliefNodeTimerId = -1
            if not self.m_pendingReliefNodeActive then
                return
            end
            local reliefNode = self:_EnsureReliefNode()
            self:_RefreshReliefNode()
            reliefNode.gameObject:SetActiveIfNecessary(true)
        end, self)
    else
        self.m_reliefNode.gameObject:SetActiveIfNecessary(true)
    end
end

AdventureDungeonCtrl._RefreshReliefNode = HL.Method() << function(self)
    if not self.m_reliefNode then
        return
    end
    local totalCount = GameInstance.player.activitySystem.staminaTotalCount
    local useCount = GameInstance.player.activitySystem.staminaReduceUsedCount
    local remainCount = totalCount - useCount

    self.m_reliefNode.blackNum.gameObject:SetActive(ActivityUtils.hasStaminaReduceCount())
    self.m_reliefNode.blackNum.text = remainCount
    self.m_reliefNode.redNum.gameObject:SetActive(not ActivityUtils.hasStaminaReduceCount())
    self.m_reliefNode.redNum.text = remainCount
    self.m_reliefNode.numTxt.text = string.format("/%d", totalCount)
end



HL.Commit(AdventureDungeonCtrl)
