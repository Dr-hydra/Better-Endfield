local AchievementUtils = {}

function AchievementUtils.loadAchievementData(loadNoObtain)
    loadNoObtain = loadNoObtain == true

    local dataSource = {}
    local dataMap = {}
    local groupId2CategoryIndexMap = {}
    local groupId2GroupIndexMap = {}
    local categoryTable = Tables.achievementTypeTable
    for id, categoryData in pairs(categoryTable) do
        local categoryInfo = {
            categoryData = categoryData,
            groupInfos = {},
        }
        for i, groupData in pairs(categoryData.achievementGroupData) do
            local groupInfo = {
                groupData = groupData,
                achievements = {}
            }
            table.insert(categoryInfo.groupInfos, groupInfo)
        end
        table.insert(dataSource, categoryInfo)
    end
    table.sort(dataSource, function(lhs, rhs)
        return lhs.categoryData.categoryPriority < rhs.categoryData.categoryPriority
    end)
    for i = 1, #dataSource do
        local categoryInfo = dataSource[i]
        categoryInfo.haveSub = #categoryInfo.groupInfos > 1
        for j, groupInfo in ipairs(categoryInfo.groupInfos) do
            groupId2CategoryIndexMap[groupInfo.groupData.groupId] = i
            groupId2GroupIndexMap[groupInfo.groupData.groupId] = j
        end
    end


    local achievementTable = Tables.achievementTable
    local achievementPlayerData = GameInstance.player.achievementSystem.achievementData;
    for achievementId, achievementData in pairs(achievementTable) do
        local groupId = achievementData.groupId
        local categoryIndex = groupId2CategoryIndexMap[groupId]
        local categoryInfo = dataSource[categoryIndex]
        if categoryInfo ~= nil then
            local groupIndex = groupId2GroupIndexMap[groupId]
            local groupInfo = categoryInfo.groupInfos[groupIndex]
            if groupInfo ~= nil then
                local suc, achievementPlayerInfo = achievementPlayerData.achievementInfos:TryGetValue(achievementId)
                local ok, achievementTimeInfo = achievementPlayerData.achievementTimeInfos:TryGetValue(achievementId)
                if loadNoObtain or (suc and achievementPlayerInfo.level >= achievementData.initLevel) then
                    local achievementInfo = {
                        achievementData = achievementData,
                        achievementPlayerInfo = achievementPlayerInfo,
                        achievementTimeInfo = achievementTimeInfo,
                        sortId = achievementData.order,
                    }
                    table.insert(groupInfo.achievements, achievementInfo)
                    dataMap[achievementId] = achievementInfo
                end
            end
        end
    end
    return dataSource, dataMap
end


























function AchievementUtils.filterAchievementData(dataSource, filterFunc)
    local categoryFilteredData = {}
    local filteredAchievementMap = {}
    for i, categoryInfo in ipairs(dataSource) do
        local filteredGroups = {}
        local showNoObtain = categoryInfo.categoryData.noObtainCanView
        for j, groupInfo in ipairs(categoryInfo.groupInfos) do
            local filteredInfos = {}
            for k, achievementInfo in ipairs(groupInfo.achievements) do
                local include = filterFunc(achievementInfo, filteredInfos, showNoObtain)
                if include then
                    filteredAchievementMap[achievementInfo.achievementData.achieveId] = {
                        categoryIndex = #categoryFilteredData + 1,
                        groupIndex = #filteredGroups + 1,
                    }
                end
            end
            if #filteredInfos ~= 0 then
                table.sort(filteredInfos, Utils.genSortFunction({"sortId"}, true))
                for filteredInfoIndex, filteredInfo in ipairs(filteredInfos) do
                    filteredAchievementMap[filteredInfo.achievementData.achieveId].achievementIndex = filteredInfoIndex
                end
                local filteredGroup = {
                    data = groupInfo.groupData,
                    filteredInfos = filteredInfos,
                }
                table.insert(filteredGroups, filteredGroup)
            end
        end
        if #filteredGroups ~= 0 then
            local filteredCategory = {
                data = categoryInfo.categoryData,
                haveSub = categoryInfo.haveSub,
                filteredGroups = filteredGroups,
            }
            table.insert(categoryFilteredData, filteredCategory)
        end
    end
    return categoryFilteredData, filteredAchievementMap
end


_G.AchievementUtils = AchievementUtils
return AchievementUtils