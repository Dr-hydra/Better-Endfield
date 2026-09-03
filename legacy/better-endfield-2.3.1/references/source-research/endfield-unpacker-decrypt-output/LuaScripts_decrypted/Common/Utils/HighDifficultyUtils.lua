local HighDifficultyUtils = {}


local newActivityHighDifficultyText = "new_activity_high_difficulty_"
function HighDifficultyUtils.isNewHighDifficultySeries(seriesId)
    if not GameInstance.player.highDifficultySystem:IsHighDiffilcultySeriesUnlock(seriesId) then
        return false
    end
    return not ClientDataManagerInst:GetBool(newActivityHighDifficultyText .. seriesId,false)
end
function HighDifficultyUtils.setFalseNewHighDifficultySeries(seriesId)
    ClientDataManagerInst:SetBool(newActivityHighDifficultyText .. seriesId, true, false, EClientDataTimeValidType.Permanent)
    Notify(MessageConst.ON_HIGH_DIFFICULTY_NEW_RED_DOT_SET_FALSE)
end


local newActivityHighDifficultyTaskText = "new_high_difficulty_task_"
function HighDifficultyUtils.isNewHighDifficultyTask(activityId, taskId)
    return not ClientDataManagerInst:GetBool(newActivityHighDifficultyTaskText .. activityId .. taskId, false)
end
function HighDifficultyUtils.setFalseNewHighDifficultyTask(activityId, taskIds)
    for index, taskId in ipairs(taskIds) do
        ClientDataManagerInst:SetBool(newActivityHighDifficultyTaskText .. activityId .. taskId, true, false, "Default", EClientDataTimeValidType.Permanent)
    end
    Notify(MessageConst.ACTIVITY_HIGH_DIFFICULTY_NEW_TASK_SET_FALSE)
end


function HighDifficultyUtils.getHighDifficultyActivityId()
    local ids = GameInstance.player.activitySystem:GetActivityOfCertainType(GEnums.ActivityType.HighDifficultyChallenge)
    if ids.Count > 0 then
        return ids[0]
    end
    return nil
end


function HighDifficultyUtils.GetLatestSeriesIds()
    local latestSeriesIds = {};
    local latestSortId = 0;
    local allSeriesIds = GameInstance.player.highDifficultySystem:GetAllUnlockSeriesIds()
    for i = 1,allSeriesIds.Count do
        local seriesId = allSeriesIds[CSIndex(i)]
        local sortId = Tables.highDifficultySeriesTable[seriesId].newSeriesSortId;
        if (sortId >= latestSortId) then
            if (sortId > latestSortId) then
                latestSeriesIds = {}
            end
            table.insert(latestSeriesIds, seriesId)
            latestSortId = sortId
        end
    end
    return latestSeriesIds
end


function HighDifficultyUtils.GetSeriesInfo(seriesId)
    local seriesCfg = Tables.highDifficultyGameTable[seriesId]
    local seriesInfo = {}
    for _, dungeonInfo in pairs(seriesCfg.gameList) do
        if not Tables.dungeonRaidTable[dungeonInfo.gameId].isRaid then
            local normalId = dungeonInfo.gameId
            local raidId = Tables.dungeonRaidTable[normalId].RelatedLevel
            table.insert(seriesInfo, {
                normalId = normalId,
                raidId = raidId,
                raidUnlocked = GameInstance.dungeonManager:IsDungeonUnlocked(raidId),
                raidPassed = GameInstance.dungeonManager:IsDungeonPassed(raidId),
                sortId = dungeonInfo.sortId,
            })
        end
    end
    table.sort(seriesInfo,Utils.genSortFunction({ "sortId" }, true))
    return seriesInfo
end


_G.HighDifficultyUtils = HighDifficultyUtils
return HighDifficultyUtils