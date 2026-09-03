
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local commonToastCtrl = require_ex('UI/Panels/CommonToast/CommonToastCtrl')
local PANEL_ID = PanelId.DungeonCommonToast

DungeonCommonToastCtrl = HL.Class('DungeonCommonToastCtrl', commonToastCtrl.CommonToastCtrl)

DungeonCommonToastCtrl.TryShow = HL.StaticMethod(HL.Any) << function(arg)
    local dungeonId
    if type(arg) == "string" then
        dungeonId = arg
    else
        dungeonId = unpack(arg)
    end
    if not DungeonUtils.isDungeonChallenge(dungeonId) then
        return
    end

    local hasCfg, dungeonCfg = Tables.dungeonTable:TryGetValue(dungeonId)
    if not hasCfg then
        return
    end

    local sceneId = dungeonCfg.sceneId
    local gainedRewardChestNum, maxRewardChestNum = DungeonUtils.getDungeonChestCount(sceneId)
    if maxRewardChestNum <= 0 or gainedRewardChestNum >= maxRewardChestNum then
        return
    end
    local str = string.format(Language.LUA_DUNGEON_CHALLENGE_CHEST_COLLECTION_PROGRESS, gainedRewardChestNum, maxRewardChestNum)
    local ctrl = DungeonCommonToastCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl:ShowToast(str)
end

DungeonCommonToastCtrl.OnSceneCollectionModify = HL.StaticMethod(HL.Any) << function(arg)
    local prefabId, sceneId = unpack(arg)

    local dungeonId = GameInstance.dungeonManager.curDungeonId
    if not DungeonUtils.isDungeonChallenge(dungeonId) then
        return
    end

    local chestTag = Tables.dungeonConst.dungeonChestCollectionTag
    local _, chestIdList = Tables.collectionLabelTable:TryGetValue(chestTag)
    local isChest = false
    for _, idCfg in pairs(chestIdList.list) do
        if idCfg.prefabId == prefabId then
            isChest = true
            break
        end
    end
    if not isChest then
        return
    end

    local gainedRewardChestNum, maxRewardChestNum = DungeonUtils.getDungeonChestCount(sceneId)
    if maxRewardChestNum <= 0 then
        return
    end
    local str = string.format(Language.LUA_DUNGEON_CHALLENGE_CHEST_COLLECTION_PROGRESS, gainedRewardChestNum, maxRewardChestNum)
    local ctrl = DungeonCommonToastCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl:ShowToast(str)
end

HL.Commit(DungeonCommonToastCtrl)
