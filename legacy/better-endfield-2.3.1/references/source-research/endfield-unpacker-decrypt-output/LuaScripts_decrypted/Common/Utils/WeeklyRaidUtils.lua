local WeeklyRaidUtils = {}

WeeklyRaidUtils.MAX_WEEKLY_ENTRUST_COUNT = Tables.weekRaidConst.weekDelegateCount

WeeklyRaidUtils.DungeonWeeklyRaidType = {
    Normal = 1,
    Week = 2,
}

WeeklyRaidUtils.DelegateObjectiveConfig = {
    [typeof(CS.Beyond.Gameplay.CheckMoney)] = {
        GetItemId = function(condition)

            return condition._moneyId.constValue
        end,
        GetObjectiveDesc = function(condition, isHud)

            local moneyId = condition._moneyId.constValue
            return string.format(isHud and Language.LUA_WEEKLY_RAID_DELEGATE_MONEY_HUD or Language.LUA_WEEKLY_RAID_DELEGATE_MONEY, Tables.itemTable[moneyId].iconId, Tables.itemTable[moneyId].name)
        end
    },
    [typeof(CS.Beyond.Gameplay.WeekRaidPlayerHasItem)] = {
        GetItemId = function(condition)

            return condition._itemId.constValue
        end,
        GetObjectiveDesc = function(condition, isHud)

            local itemId = condition._itemId.constValue
            local success, textId = Tables.weekraidItemDomainTable:TryGetValue(itemId)
            if not success then
                logger.error("WeeklyRaidUtils.DelegateObjectiveConfig: Invalid itemId in WeekRaidPlayerHasItem: " .. tostring(itemId))
                return ""
            end
            return string.format(isHud and Language.LUA_WEEKLY_RAID_DELEGATE_ITEM_HUD or Language.LUA_WEEKLY_RAID_DELEGATE_ITEM, Language[textId],  Tables.itemTable[itemId].iconId, Tables.itemTable[itemId].name)
        end
    },
    [typeof(CS.Beyond.Gameplay.CheckWeeklyRaidStat)] = {
        GetItemId = function(condition)

            return condition._itemId.constValue
        end,
        GetObjectiveDesc = function(condition, isHud)

            if condition._statType == GEnums.WeekRaidStatType.SettlementInsideItemCount then
                return isHud and string.format(condition._isIncr and Language.LUA_WEEKLY_RAID_DELEGATE_ITEM_HUD or Language.LUA_WEEKLY_RAID_DELEGATE_ITEM_HUD_ALL, Language[Tables.weekraidItemDomainTable[condition._itemId.constValue]], Tables.itemTable[condition._itemId.constValue].iconId,Tables.itemTable[condition._itemId.constValue].name)
                    or string.format(condition._isIncr and Language.LUA_WEEKLY_RAID_DELEGATE_ITEM or Language.LUA_WEEKLY_RAID_DELEGATE_ITEM_ALL, Language[Tables.weekraidItemDomainTable[condition._itemId.constValue]], Tables.itemTable[condition._itemId.constValue].iconId,Tables.itemTable[condition._itemId.constValue].name)
            elseif condition._statType == GEnums.WeekRaidStatType.EnemyIdDeadCount then
                return isHud and string.format(condition._isIncr and Language.LUA_WEEKLY_RAID_DELEGATE_ENEMY_HUD or Language.LUA_WEEKLY_RAID_DELEGATE_ENEMY_HUD_ALL, Tables.enemyDisplayInfoTable[condition._enemyId.constValue].name)
                    or string.format(condition._isIncr and Language.LUA_WEEKLY_RAID_DELEGATE_ENEMY or Language.LUA_WEEKLY_RAID_DELEGATE_ENEMY_ALL, Tables.enemyDisplayInfoTable[condition._enemyId.constValue].name)
            elseif condition._statType == GEnums.WeekRaidStatType.EnemyTemplateDeadCount then
                return isHud and string.format(condition._isIncr and Language.LUA_WEEKLY_RAID_DELEGATE_ENEMY_HUD or Language.LUA_WEEKLY_RAID_DELEGATE_ENEMY_HUD_ALL, Tables.enemyTemplateDisplayInfoTable[condition._enemyTemplateId.constValue].name)
                    or string.format(condition._isIncr and Language.LUA_WEEKLY_RAID_DELEGATE_ENEMY or Language.LUA_WEEKLY_RAID_DELEGATE_ENEMY_ALL, Tables.enemyTemplateDisplayInfoTable[condition._enemyTemplateId.constValue].name)
            else
                return ""
            end
        end
    },
}

WeeklyRaidUtils.GetWeekRaidItemCount = function(itemId)
    local count = Utils.getItemCount(itemId, false, true)
    local success,cfg = Tables.weekRaidItemTable:TryGetValue(itemId)
    if success and not string.isEmpty(cfg.convertItemId) then
        count = count + Utils.getItemCount(cfg.convertItemId, false, true)
    end
    return count
end

WeeklyRaidUtils.TabConfig = {
    [WeeklyRaidUtils.DungeonWeeklyRaidType.Normal] = {
        color = 'e14754',
        title = 'ui_weekraid_mission_page_1',
        icon = 'dwr_tab_icon01',
        type = {
            [1] = GEnums.WeekRaidMissionType.MainMission,
            [2] = GEnums.WeekRaidMissionType.SideMission,
            [3] = GEnums.WeekRaidMissionType.ResearchMission,
        },
        getDelegate = function()
            return GameInstance.player.weekRaidSystem.scheduledMission
        end
    },
    [WeeklyRaidUtils.DungeonWeeklyRaidType.Week] = {
        color = '26bbfd',
        title = 'ui_weekraid_mission_page_3',
        icon = 'dwr_tab_icon03',
        type = {
            [1] = GEnums.WeekRaidMissionType.RandomMission,
        },
        getDelegate = function()
            return GameInstance.player.weekRaidSystem.weeklyMission
        end
    },
}

WeeklyRaidUtils.GetWeeklyRaidMissionText = function(missionId)
    local cfg = Tables.weekRaidDelegateTable:GetValue(missionId)
    if cfg.weekRaidMissionType == GEnums.WeekRaidMissionType.MainMission then
        local missionInfo = GameInstance.player.mission:GetMissionInfo(missionId)
        return {
            name = missionInfo.missionName:GetText(),
            desc = missionInfo:GetMissionDesc():GetText(),
        }
    else
        return {
            name = cfg.name,
            desc = cfg.desc,
        }
    end
end

WeeklyRaidUtils.GetTechShowString = function(cfg, value)
    if WeeklyRaidUtils.TechUseStrValue(cfg.techType) then
        return GameInstance.player.weekRaidSystem:GetBuffNameByWeekRaidBuffTechId(value.techId)
    else
        return string.format(cfg.formatText, GameInstance.player.weekRaidSystem:TechBaseValue(cfg.techType) + value)
    end
end

WeeklyRaidUtils.TechUseStrValue = function(param)

    if type(param) == 'string' then
        local _, cfg = Tables.weekRaidTechTable:TryGetValue(param)
        if cfg then
            return GameInstance.player.weekRaidSystem:TechUseStrValue(cfg.techType)
        else
            logger.error("WeeklyRaidUtils.TechUseStrValue: Invalid tech param: " .. tostring(param))
            return false
        end
    elseif param.techType then
        return GameInstance.player.weekRaidSystem:TechUseStrValue(param.techType)
    elseif param:GetType() == typeof(GEnums.WeekRaidTechType) then
        return GameInstance.player.weekRaidSystem:TechUseStrValue(param)
    else
        logger.error("WeeklyRaidUtils.TechUseStrValue: Invalid tech param type: " .. type(param))
        return false
    end
end

WeeklyRaidUtils.IsInWeeklyRaid = function()
    return GameInstance.mode.modeType == GEnums.GameModeType.WeekRaid
end

WeeklyRaidUtils.IsInWeeklyRaidIntro = function()
    return GameInstance.mode.modeType == GEnums.GameModeType.WeekRaidIntro
end

WeeklyRaidUtils.IsDelegateCompeted = function(missionId)
    local questId = Tables.weekRaidDelegateTable[missionId].questId
    local allCompleted = true
    local questInfo = GameInstance.player.mission:GetQuestInfo(questId)
    for i = 0, questInfo.objectiveList.Count - 1 do
        local objective = questInfo.objectiveList[i]
        allCompleted = allCompleted and objective.isCompleted
    end
    return allCompleted
end


WeeklyRaidUtils.ShowTechCount = 6


_G.WeeklyRaidUtils = WeeklyRaidUtils
return WeeklyRaidUtils
