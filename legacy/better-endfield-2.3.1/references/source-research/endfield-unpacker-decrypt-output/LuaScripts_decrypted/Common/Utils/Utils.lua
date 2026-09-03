local localIndex = {}
local locals = {}

setmetatable(locals, {
    ["__index"] = localIndex,

    ["__newindex"] = function(t, k, v)
        localIndex[k] = v
    end,
    ["__mode"] = "kv"
})

setmetatable(localIndex, {
    ["__index"] = setmetatable({}, { ["__index"] = _G }),
    ["__mode"] = "kv"
})

localIndex["__locals"] = locals

local Utils = {}

function Utils.printDoString(str, noPrintOriCode)
    local logs = {}
    local localPrint = function(...)
        print(...)
        logs = lume.concat(logs, { ..., "\n" })
    end
    local localError = function(...)
        logger.error(...)
        logs = lume.concat(logs, { ..., "\n" })
    end

    if not lume.find({ "\r\n", "\n", "\r" }, str) then
        if not noPrintOriCode then
            localPrint("[Run Script]>>>>---- " .. str .. "---- <<<<")
        end


        local hasPrefix = (#str > 1 and str:sub(1, 1) == "=")
        if hasPrefix then

            str = string.sub(str, 2)
        end


        local retStr = "return " .. str
        local retFunc, errMsg = loadstring(retStr)

        if not retFunc and not hasPrefix then

            retFunc, errMsg = loadstring(str)
        end

        if not retFunc then

            localError(errMsg)
        else
            retFunc = setfenv(retFunc, locals)


            local function collectLocals()
                if debug.getinfo(2, "f").func ~= retFunc then

                    return
                end

                local __debug_idx = 1
                while true do
                    local name, value = debug.getlocal(2, __debug_idx)
                    if not name then
                        break
                    end
                    rawset(locals, name, value)
                    __debug_idx = __debug_idx + 1
                end
            end


            local function traceback(msg)
                msg = debug.traceback(msg, 2)
                localError(msg)
                return msg
            end


            local function getReturnValue(status, ...)
                local retNum = select("#", ...)
                return status, retNum, { ... }
            end





            local status, retNum, retVals = getReturnValue(xpcall(retFunc, traceback))




            if status and retNum > 0 then
                local outputStr = ""
                for i = 1, retNum do
                    outputStr = outputStr .. inspect(retVals[i], { ['depth'] = 3 })
                    if i < retNum then
                        outputStr = outputStr .. ", "
                    end
                end
                localPrint(outputStr)
                return logs
            end
        end

        localPrint("> ")
    else

        localPrint("> ")
    end

    return logs
end

function Utils.bindLuaRef(item)
    if type(item) ~= "table" then
        item = {
            gameObject = item.gameObject,
            transform = item.transform,
        }
    end
    local luaRef = item.gameObject:GetComponent("LuaReference")
    if luaRef then
        luaRef:BindToLua(item)
    end
    return item
end

function Utils.wrapLuaNode(item)
    local luaWidget = item.transform:GetComponent("LuaUIWidget")
    local wrapResult
    if luaWidget then
        wrapResult = UIWidgetManager:Wrap(item)
    else
        wrapResult = Utils.bindLuaRef(item)
        if wrapResult then
            UIUtils.initLuaCustomConfig(wrapResult)
        end
    end

    return wrapResult
end

function Utils.genSortFunction(keyList, isIncremental)

    if isIncremental then
        return function(a, b)
            for _, key in ipairs(keyList) do
                local valueA = a[key]
                local valueB = b[key]
                if valueA ~= valueB then
                    if valueA == nil or valueB == nil then
                        return valueA == nil
                    end
                    return valueA < valueB
                end
            end
            return false
        end
    else
        return function(a, b)
            for _, key in ipairs(keyList) do
                local valueA = a[key]
                local valueB = b[key]
                if valueA ~= valueB then
                    if valueA == nil or valueB == nil then
                        return valueA ~= nil
                    end
                    return valueA > valueB
                end
            end
            return false
        end
    end
end

function Utils.genSortFunctionWithIgnore(keyList, isIncremental, ignoreKeyList)

    return function(a, b)
        for _, key in ipairs(keyList) do
            local valueA = a[key]
            local valueB = b[key]
            if type(valueA) == "function" then
                valueA = valueA(a)
            end
            if type(valueB) == "function" then
                valueB = valueB(b)
            end
            if valueA ~= valueB then
                if valueA == nil or valueB == nil then
                    if isIncremental or lume.find(ignoreKeyList, key) then
                        return valueA == nil
                    else
                        return valueA ~= nil
                    end
                end

                if isIncremental or lume.find(ignoreKeyList, key) then
                    return valueA < valueB
                else
                    return valueA > valueB
                end
            end
        end

        return false
    end
end

function Utils.isInclude(table, value)
    for index, v in pairs(table) do
        if v == value then
            return index
        end
    end
    return nil
end

function Utils.transferToCameraCoordinate(v)
    local camera = CameraManager.mainCamera
    local forward = camera.transform.forward
    forward.y = 0
    forward = forward.normalized
    local left = camera.transform.right
    left.y = 0
    left = left.normalized
    return v.x * left + v.y * Vector3.up + v.z * forward
end

function Utils.tobool(v)
    if type(v) == "number" then
        return not (v == 0)
    elseif type(v) == "string" then
        return not (string.lower(v) == "false")
    end

    return false
end

function Utils.syncFreeLookCamWithMain(ctrl, setPitch)
    local angles = GameInstance.cameraManager.mainCamera.transform.rotation.eulerAngles;
    local pitch = angles.x;
    if pitch > 180 then
        pitch = pitch - 360
    end
    local horizontalValue = angles.y;
    if setPitch then
        ctrl:SetCameraVerticalDegrees(pitch, false)
    end
    ctrl:SetCameraHorizontalAngle(horizontalValue, false)
    ctrl:ForceFlush()
end

function Utils.getUnlockedCustomObtainWay(itemId)
    local unlockedObtainWayList = {}
    local hasUnlockedObtainWay = false
    local itemCfg = Tables.itemTable:GetValue(itemId)
    for _, obtainWayId in pairs(itemCfg.obtainWayIds) do
        local _, obtainWayCfg = Tables.systemJumpTable:TryGetValue(obtainWayId)
        if obtainWayCfg then
            local isUnlock = (not obtainWayCfg.bindSystem) or Utils.isSystemUnlocked(obtainWayCfg.bindSystem)
            if isUnlock then
                hasUnlockedObtainWay = true
                table.insert(unlockedObtainWayList, obtainWayCfg)
            end
        end
    end

    return hasUnlockedObtainWay, unlockedObtainWayList
end

function Utils.getItemValuableDepotType(itemId)
    local itemData = Tables.itemTable[itemId]
    return itemData.valuableTabType
end

function Utils.isItemInstType(itemId)
    return GameInstance.player.inventory:IsInstItem(itemId)
end


function Utils.getItemCount(itemId, forceIncludeCurDepot, allDepot)
    if string.isEmpty(itemId) then
        return 0, 0, 0
    end

    local inventory = GameInstance.player.inventory
    local _, itemData = Tables.itemTable:TryGetValue(itemId)
    if not itemData then
        return 0, 0, 0
    end
    if inventory:IsMoneyType(itemData.type) then
        return inventory:GetItemCountInWallet(itemId)
    end

    if itemData.type == CS.Beyond.GEnums.ItemType.APGameplay then
        local count = inventory.curStamina
        return count, count, count
    end

    local valuableDepotType = itemData.valuableTabType
    local isValuableItem = valuableDepotType ~= GEnums.ItemValuableDepotType.Factory

    local bagCount, depotCount, walletCount = 0, 0, 0
    if isValuableItem then

        local scope = CS.Beyond.Gameplay.Scope.Create(GEnums.ScopeName.Main)
        depotCount = inventory:GetItemCountInDepot(scope, 0, itemId)
    else
        local isMoney = inventory:IsMoneyType(itemData.type)
        if isMoney then
            walletCount = inventory:GetItemCountInWallet(itemId)
        else
            bagCount = inventory:GetItemCountInBag(Utils.getCurrentScope(), itemId)
            if forceIncludeCurDepot or Utils.isInSafeZone() then
                if allDepot or itemData.showAllDepotCount then
                    depotCount = inventory:GetItemCountInAllFacDepot(Utils.getCurrentScope(), itemId)
                else
                    depotCount = inventory:GetItemCountInDepot(Utils.getCurrentScope(), Utils.getCurrentChapterId(), itemId)
                end
            end
        end
    end

    local count = depotCount + bagCount + walletCount
    return count, bagCount, depotCount
end

function Utils.getDepotItemStackLimitCountInCurrentDomain()
    return Utils.getDepotItemStackLimitCount(Utils.getCurDomainId())
end

function Utils.getDepotItemStackLimitCount(domainId)
    local domainSuccess, domainCfg = Tables.domainDataTable:TryGetValue(domainId)
    if not domainSuccess then
        return Tables.domainDepotConst.nonConfigDepotBaseItemLimit or 0
    end

    local stackLimitCount = domainCfg.baseItemStackCount
    for domainDepotId, domainDepotCfg in pairs(Tables.domainDepotTable) do
        if domainDepotCfg.domainId == domainId then
            local domainDepotData = GameInstance.player.domainDepotSystem:GetDomainDepotDataById(domainDepotId)
            if domainDepotData ~= nil and domainDepotData.level > 0 then
                stackLimitCount = stackLimitCount + domainDepotData.extraItemStackLimitCount
            end
        end
    end

    return stackLimitCount
end

function Utils.getDepotItemCount(itemId, scope, domainId)
    if string.isEmpty(itemId) then
        return 0
    end
    scope = scope or Utils.getCurrentScope()
    local chapterId = domainId and ScopeUtil.ChapterIdStr2Int(domainId) or Utils.getCurrentChapterId()
    return GameInstance.player.inventory:GetItemCountInDepot(scope, chapterId, itemId)
end

function Utils.getAllFacDepotItemCount(itemId, scope)
    if string.isEmpty(itemId) then
        return 0
    end
    scope = scope or Utils.getCurrentScope()
    return GameInstance.player.inventory:GetItemCountInAllFacDepot(scope, itemId)
end

function Utils.getBagItemCount(itemId)
    if string.isEmpty(itemId) then
        return 0
    end
    return GameInstance.player.inventory:GetItemCountInBag(Utils.getCurrentScope(), itemId)
end

function Utils.isInFactoryMode()
    return GameWorld.worldInfo.inFactoryMode
end

function Utils.isInSafeZone()
    if Utils.isInFacMainRegion() then
        return true
    end
    return GameInstance.playerController.isInSaveZone and not Utils.isInFight()
end


function Utils.isInFacMainRegionAndGetIndex()
    local inMainRegion, panel = GameInstance.remoteFactoryManager:IsPlayerPositionInMainRegionAndGetIndex()
    local panelIndex = -1
    if inMainRegion and panel then
        panelIndex = panel.index
    end
    return inMainRegion, panelIndex
end

function Utils.isInFacMainRegion()
    return GameInstance.remoteFactoryManager:IsPlayerPositionInMainRegion()
end

function Utils.stringJsonToTable(jsonString)
    local value = Json.decode(jsonString)
    return value
end

function Utils.enableCameraDOF(data)
    CS.Beyond.Gameplay.View.CameraUtils.EnableDOF(data)
end

function Utils.disableCameraDOF()
    CS.Beyond.Gameplay.View.CameraUtils.DisableDOF()
end

function Utils.isSystemUnlocked(t)
    if not t or t == GEnums.UnlockSystemType.None then
        return true
    end
    return GameInstance.player.systemUnlockManager:IsSystemUnlockByType(t)
end

function Utils.round(num, numDecimalPlaces)
    local mult = 10 ^ (numDecimalPlaces or 0)
    return math.floor(num * mult + 0.5) / mult
end

function Utils.timestampToDate(timestamp)
    local date = os.date("!*t", timestamp + Utils.getServerTimeZoneOffsetSeconds())
    return string.format(Language.DATE_FORMAT_MONTH_DAY, date.month, date.day)
end

function Utils.timestampToDateYMD(timestamp)
    local date = os.date("!*t", timestamp + Utils.getServerTimeZoneOffsetSeconds())
    return string.format(Language.DATE_FORMAT_YEAR_MONTH_DAY, date.year, date.month, date.day)
end

function Utils.timestampToDateMDHM(timestamp)
    local date = os.date("!*t", timestamp + Utils.getServerTimeZoneOffsetSeconds())
    return string.format(Language.DATE_FORMAT_MONTH_DAY_HOUR_MIN, date.month, date.day, date.hour, date.min)
end

function Utils.timestampToDateYMDHM(timestamp)
    local date = os.date("!*t", timestamp + Utils.getServerTimeZoneOffsetSeconds())
    return string.format(Language.DATE_FORMAT_YEAR_MONTH_DAY_HOUR_MIN, date.year, date.month, date.day, date.hour, date.min)
end


function Utils.getTimestampToday0AM()
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local curDate = os.date("!*t", curTime)
    local today0AM = {
        year = curDate.year,
        month = curDate.month,
        day = curDate.day,
        hour = 0,
    }
    return os.time(today0AM)
end

function Utils.getTimestampNowYear1M1Day()
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local curDate = os.date("!*t", curTime)
    local today0AM = {
        year = curDate.year,
        month = 1,
        day = 1,
        hour = 0,
    }
    return os.time(today0AM)
end



function Utils.triggerVoice(triggerKey, speakerId)
    if not speakerId then

        speakerId = GameInstance.player.squadManager:GetLeaderId()
    end

    return VoiceManager:Response(triggerKey, nil, speakerId, GEnums.VoSpeakerType.Characters)
end
function Utils.stopDefaultChannelVoice()
    VoiceManager:StopDefaultChannel()
end




function Utils.checkCGCanSkip(cgId)
    local res, data = DataManager.cgConfig.data:TryGetValue(cgId)
    if not res then
        local rawId = NarrativeUtils.GetRawNarrativeId(cgId)
        res, data = DataManager.cgConfig.data:TryGetValue(rawId)
        if not res then
            return false
        end
    end

    local skipType = data.skipType
    local skipTypeInt = skipType:ToInt()

    if skipTypeInt == CS.Beyond.Gameplay.CutsceneSkipType.NoneSkip:ToInt() then
        return false
    elseif skipTypeInt == CS.Beyond.Gameplay.CutsceneSkipType.CanSkip:ToInt() then
        return true
    else
        return GameInstance.player.cinematic:CheckFMVWatched(cgId)
    end

end

function Utils.checkCinematicCanSkip(data)
    local skipType = data.skipType
    local key = data.cutsceneName

    local skipTypeInt = skipType:ToInt()
    if skipTypeInt == CS.Beyond.Gameplay.CutsceneSkipType.NoneSkip:ToInt() then
        return false
    elseif skipTypeInt == CS.Beyond.Gameplay.CutsceneSkipType.CanSkip:ToInt() then
        return true
    else
        return GameInstance.player.cinematic:CheckTimelineWatched(key)
    end
end

Utils.SkillUtil = CS.Beyond.Gameplay.SkillUtil



function Utils.isGameSystemUnlocked(systemId)
    if string.isEmpty(systemId) then
        return true
    end
    local success, sysData = Tables.gameSystemConfigTable:TryGetValue(systemId)
    if success and sysData.unlockSystemType ~= GEnums.UnlockSystemType.None then
        return Utils.isSystemUnlocked(sysData.unlockSystemType)
    end
    return true
end

function Utils.isInFight()
    return GlobalTagUtils.IsInFight()
end

function Utils.isInThrowMode()
    return GameWorld.battle.inThrowMode
end

function Utils.isInCustomAbility()
    local com = GameInstance.playerController.mainCharacter.customAbilityCom
    if com ~= nil then
        return com:IsInCustomAbility()
    else
        return false
    end
end

function Utils.isInNarrative()
    return GameWorld.narrativeManager.inNarrative
end

function Utils.isNarrativeTopPhase()
    local topPhaseId = PhaseManager:GetTopPhaseId()
    return topPhaseId == PhaseId.Cinematic or topPhaseId == PhaseId.Dialog or topPhaseId == PhaseId.DialogTimeline
end

function Utils.isRadioPlaying()
    local show, _ = UIManager:IsShow(PanelId.Radio)
    return show
end

function Utils.getCurrentScope()
    if UNITY_EDITOR then
        local callerInfo = debug.getinfo(2, "Sl")
        return CS.Beyond.Gameplay.Scope.Create(
            ScopeUtil.GetCurrentScope(),
            CS.Beyond.Gameplay.Scope.CreateReason.Query,
            callerInfo.name,
            callerInfo.source,
            callerInfo.currentline
        )
    else
        return CS.Beyond.Gameplay.Scope.Create(ScopeUtil.GetCurrentScope())
    end
end

function Utils.getCurrentChapterId()
    return ScopeUtil.GetCurrentChapterId()
end



function Utils.isInMainScope()
    return ScopeUtil.IsMainScope()
end

function Utils.isInRpgDungeon()
    return ScopeUtil.IsPlayerInRpgDungeon()
end

function Utils.isInBlackbox()
    return ScopeUtil.IsPlayerInBlackbox()
end

function Utils.isInDungeonTrain()
    local dungeonId = GameInstance.dungeonManager.curDungeonId
    if not dungeonId then
        return false
    end
    return DungeonUtils.isDungeonTrain(dungeonId)
end

function Utils.isInDungeon()
    return GameInstance.dungeonManager.inDungeon
end

function Utils.isInWeekRaid()
    return GameInstance.mode.modeType == GEnums.GameModeType.WeekRaid or
        GameInstance.mode.modeType == GEnums.GameModeType.WeekRaidIntro
end

function Utils.isInDungeonFactory()
    local dungeonId = GameInstance.dungeonManager.curDungeonId
    local success, dungeonInfo = Tables.gameMechanicTable:TryGetValue(dungeonId or "")
    if success then
        return dungeonInfo.gameCategory == Tables.dungeonConst.dungeonFactoryCategory
    end

    return false
end

function Utils.isDepotManualInOutLocked()
    local bData = GameWorld.worldInfo.curLevel.levelData.blackbox
    if not bData then
        return false
    end
    return bData.inventory.depotManualInOutLocked
end

function Utils.isCurrentMapHasFactoryGrid()
    local mapId = GameWorld.worldInfo.curMapIdStr
    local regionMap = GameInstance.remoteFactoryManager:GetVoxelSpaceQuery(mapId)
    return regionMap ~= nil
end

function Utils.isForbidden(forbidType)
    return GameInstance.player.forbidSystem:IsForbidden(forbidType)
end

function Utils.getForbiddenReason(forbidType)
    return GameInstance.player.forbidSystem:GetForbidParams(forbidType)
end

function Utils.isForbiddenWithReason(forbidType)
    if not Utils.isForbidden(forbidType) then
        return false
    end
    return true, Utils.getForbiddenReason(forbidType)
end

function Utils.isForbiddenMapTeleport()
    return Utils.isForbidden(ForbidType.ForbidMapTeleport) or Utils.isForbidden(ForbidType.ForbidMapTeleportButCanGetUnstuck)
end

function Utils.isSwitchModeDisabled()
    if GameInstance.player.forbidSystem:IsForbidden(ForbidType.ForbidFactoryMode) then
        return true
    end
    if GameInstance.player.forbidSystem:IsForbidden(ForbidType.DisableSwitchMode) then
        return true
    end
    if not Utils.isCurrentMapHasFactoryGrid() then
        return true
    end
    return false
end

function Utils.shouldShowSwitchModeBtn()
    if GameInstance.player.forbidSystem:IsForbidden(ForbidType.DisableSwitchMode) then
        return false
    end
    if not Utils.isCurrentMapHasFactoryGrid() then
        return false
    end
    return true
end

function Utils.getPlayerName()
    local playerInfoSystem = GameInstance.player.playerInfoSystem
    return playerInfoSystem.playerName
end

function Utils.getPlayerGender()
    local playerInfoSystem = GameInstance.player.playerInfoSystem
    return playerInfoSystem.gender
end


function Utils.getServerAreaType()
    local playerInfoSystem = GameInstance.player.playerInfoSystem
    local serverIdType = GEnums.ServerAreaType.__CastFrom(playerInfoSystem.serverAreaType)
    return serverIdType
end

function Utils.csList2Table(list)
    local t = {}
    for _, v in pairs(list) do
        t[v] = true
    end
    return t
end

function Utils.checkNeedShowTpWarningPopup(targetSceneId, targetPosition)
    local checkResult = {
        needShow = false,
        warningText = ""
    }

    local linkBrain = GameWorld.gameMechManager.linkWireBrain
    if linkBrain.isLinking and linkBrain.linkType == CS.Beyond.Gameplay.Core.GameMech.LinkWireBrain.LinkType.Udpipe then
        local success, levelConfig = Utils.getLevelConfig(targetSceneId)
        if success then
            local targetMapId = levelConfig.mapIdStr
            local curMapId = GameWorld.worldInfo.curMapIdStr
            if targetMapId ~= curMapId then
                checkResult.needShow = true
                checkResult.warningText = Language.LUA_MAP_TP_WARNING_OTHER_MAP
            else
                local willBreak, distance = linkBrain:CheckBreakableBeforeTeleport(targetMapId, targetPosition)
                if willBreak then
                    checkResult.needShow = true
                    checkResult.warningText = string.format(
                        Language.LUA_MAP_TP_WARNING_CURRENT_MAP,
                        math.ceil(distance)
                    )
                end
            end
        end
    end

    return checkResult
end

function Utils.teleportToPositionByValidationId(teleportValidationId, callback)
    if string.isEmpty(teleportValidationId) then
        logger.error("teleportToPosition failed, invalid teleportValidationId")
        return
    end

    local success, validationData = GameInstance.dataManager.teleportValidationDatas:TryGetValue(teleportValidationId)
    if not success then
        logger.error("teleportToPositionByValidationId failed, validation data not found", teleportValidationId)
        return
    end

    local function doTeleport()
        GameAction.TeleportToPositionByValidationId(teleportValidationId, callback)
        logger.important(CS.Beyond.EnableLogType.DevOnly, "[LuaTeleport] teleportToPositionByValidationId", teleportValidationId)
    end

    local checkResult = Utils.checkNeedShowTpWarningPopup(validationData.sceneId, validationData.position)
    if checkResult.needShow then
        Notify(MessageConst.SHOW_POP_UP, {
            content = checkResult.warningText,
            onConfirm = doTeleport,
        })
    else
        doTeleport()
    end
end

function Utils.teleportToPosition(sceneId, position, rotation, teleportReason, callback, uiType, hubNodeId)
    if string.isEmpty(sceneId) or position == nil then
        logger.error("teleportToPosition failed, invalid sceneId or position")
        return
    end

    if Utils.isCurSquadAllDead() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_GAME_MODE_FORBID_FACTORY_WATCH)
        return
    end

    teleportReason = teleportReason or GEnums.C2STeleportReason.ServerTpGM
    uiType = uiType or CS.Beyond.Gameplay.TeleportUIType.Default
    hubNodeId = hubNodeId or 0

    local function doTeleport()
        local tpOptions = CS.Beyond.Gameplay.TeleportOptions.None
        if rotation ~= nil then
            GameAction.TeleportToPosition(teleportReason, sceneId, position, rotation, uiType, tpOptions, callback, hubNodeId)
        else
            GameAction.TeleportToPosition(teleportReason, sceneId, position, Vector3.zero, uiType, tpOptions, callback, hubNodeId)
        end
        logger.important(CS.Beyond.EnableLogType.DevOnly, "[LuaTeleport] teleportToPosition", sceneId, position, teleportReason, uiType)
    end

    local checkResult = Utils.checkNeedShowTpWarningPopup(sceneId, position)
    if checkResult.needShow then
        Notify(MessageConst.SHOW_POP_UP, {
            content = checkResult.warningText,
            onConfirm = doTeleport,
        })
    else
        doTeleport()
    end
end

function Utils.teleportToEntity(teleportValidationId, callback)
    if string.isEmpty(teleportValidationId) then
        logger.error("teleportToEntity failed, invalid teleportValidationId")
        return
    end

    if Utils.isCurSquadAllDead() then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_GAME_MODE_FORBID_FACTORY_WATCH)
        return
    end

    local success, validationData = GameInstance.dataManager.teleportValidationDatas:TryGetValue(teleportValidationId)
    if not success then
        logger.error("teleportToEntity failed, validation data not found", teleportValidationId)
        return
    end

    local function doTeleport()
        GameAction.TeleportToPositionByValidationId(teleportValidationId, callback)
        logger.important(CS.Beyond.EnableLogType.DevOnly, "[LuaTeleport] teleportToEntity", teleportValidationId)
    end

    local checkResult = Utils.checkNeedShowTpWarningPopup(validationData.sceneId, validationData.position)
    if checkResult.needShow then
        Notify(MessageConst.SHOW_POP_UP, {
            content = checkResult.warningText,
            onConfirm = doTeleport,
        })
    else
        doTeleport()
    end
end

function Utils.getCurDomainId()
    return ScopeUtil.GetCurrentChapterIdAsStr()
end

function Utils.getCurDomainName()
    return Utils.getDomainName(Utils.getCurDomainId())
end

function Utils.getDomainName(domainId)
    local succ, data = Tables.domainDataTable:TryGetValue(domainId)
    if succ then
        return data.domainName
    else
        logger.error("No Domain Data", domainId)
        return ""
    end
end

function Utils.isInFocusMode()
    return FocusModeUtils.isInFocusMode and not string.isEmpty(GameInstance.mode.instId)
end

function Utils.isInSettlementDefenseDefending()
    local towerDefenseGame = GameInstance.player.towerDefenseSystem.towerDefenseGame
    return towerDefenseGame ~= nil and
        towerDefenseGame.phase == CS.Beyond.Gameplay.Core.TowerDefenseGame.Phase.Defending
end


function Utils.isInSettlementDefense()
    return GameInstance.player.towerDefenseSystem.systemInDefense
end

function Utils.isInSpaceShip()
    return GameUtil.SpaceshipUtils.IsInSpaceShip()
end

function Utils.getServerTimeZoneOffsetHours()
    return CS.Beyond.DateTimeUtils.SERVER_TIME_ZONE.BaseUtcOffset.TotalHours
end

function Utils.getServerTimeZoneOffsetSeconds()
    return CS.Beyond.DateTimeUtils.SERVER_TIME_ZONE.BaseUtcOffset.TotalSeconds
end



function Utils.getClientTimeZoneOffsetSeconds()
    return CS.System.TimeZoneInfo.Local.BaseUtcOffset.TotalSeconds
end


function Utils.getCurrentCommonServerRefreshTime()
    return Utils._getCommonServerRefreshTime(0)
end


function Utils.getNextCommonServerRefreshTime()
    return Utils._getCommonServerRefreshTime(1)
end


function Utils._getCommonServerRefreshTime(offsetDays)
    local timePerDay = 24 * 60 * 60



    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds() + Utils.getServerTimeZoneOffsetSeconds()
    local curDate = os.date("!*t", curTime)
    local today4AM = {
        year = curDate.year,
        month = curDate.month,
        day = curDate.day,
        hour = UIConst.COMMON_SERVER_UPDATE_TIME,
    }
    offsetDays = offsetDays or 0
    if curDate.hour < UIConst.COMMON_SERVER_UPDATE_TIME then
        offsetDays = offsetDays - 1
    end
    return os.time(today4AM) + (timePerDay * offsetDays) + Utils._getTimeZoneDiffOfClientAndServer()
end


function Utils.getNextWeeklyServerRefreshTime()
    local timePerDay = 24 * 60 * 60
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds() + Utils.getServerTimeZoneOffsetSeconds()
    local curDate = os.date("!*t", curTime)
    local today4AM = {
        year = curDate.year,
        month = curDate.month,
        day = curDate.day,
        hour = UIConst.COMMON_SERVER_UPDATE_TIME,
    }

    local weekDay = curDate.wday - 1
    if weekDay == 0 then
        weekDay = 7
    end


    if weekDay == 1 and curDate.hour < UIConst.COMMON_SERVER_UPDATE_TIME then
        return os.time(today4AM) + Utils._getTimeZoneDiffOfClientAndServer()
    end

    local deltaDays = 8 - weekDay
    return os.time(today4AM) + timePerDay * deltaDays + Utils._getTimeZoneDiffOfClientAndServer()
end






function Utils.getServerWeekdayISOAt4AM()
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds() + Utils.getServerTimeZoneOffsetSeconds()
    local curDate = os.date("!*t", curTime)

    local weekDay = curDate.wday - 1
    if weekDay == 0 then
        weekDay = 7
    end

    if curDate.hour < UIConst.COMMON_SERVER_UPDATE_TIME then
        weekDay = weekDay - 1
        if weekDay == 0 then
            weekDay = 7
        end
    end
    return weekDay
end

function Utils._getTimeZoneDiffOfClientAndServer()
    return CS.System.TimeZoneInfo.Local:GetUtcOffset(CS.System.DateTime.Now).TotalSeconds - Utils.getServerTimeZoneOffsetSeconds()
end


function Utils.getNextMonthlyServerRefreshTime()
    local timePerDay = 24 * 60 * 60
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds() + Utils.getServerTimeZoneOffsetSeconds()
    local curDate = os.date("!*t", curTime)
    local today4AM = {
        year = curDate.year,
        month = curDate.month,
        day = curDate.day,
        hour = UIConst.COMMON_SERVER_UPDATE_TIME,
    }

    local monthDay = curDate.day

    if monthDay == 1 and curDate.hour < UIConst.COMMON_SERVER_UPDATE_TIME then
        return os.time(today4AM) + Utils._getTimeZoneDiffOfClientAndServer()
    end

    local monthTotalDays = os.date("%d", os.time({
        year = curDate.year,
        month = curDate.month + 1,
        day = 0,
    }))
    local deltaDays = monthTotalDays + 1 - monthDay
    return os.time(today4AM) + timePerDay * deltaDays + Utils._getTimeZoneDiffOfClientAndServer()
end

function Utils.appendUTC(timeStr)
    local hour = Utils.getServerTimeZoneOffsetHours()
    if hour >= 0 then
        return string.format("%s (UTC+%d)", timeStr, math.abs(hour))
    else
        return string.format("%s (UTC-%d)", timeStr, math.abs(hour))
    end
end




function Utils.timeStr2TimeStamp(timeStr, timeZoneSeconds)

    local pattern = "(%d+)/(%d+)/(%d+) (%d+):(%d+):(%d+)"
    local year, month, day, hour, min, sec = timeStr:match(pattern)


    local timeTable = {
        year = tonumber(year),
        month = tonumber(month),
        day = tonumber(day),
        hour = tonumber(hour),
        min = tonumber(min),
        sec = tonumber(sec)
    }

    local localTimestamp = os.time(timeTable)



    local utcTable = os.date("!*t")
    utcTable.isdst = nil
    local localTimeZoneOffset = os.difftime(os.time(), os.time(utcTable))
    local timestamp = localTimestamp - timeZoneSeconds + localTimeZoneOffset

    return math.floor(timestamp)
end


function Utils.isCurTimeInTimeIdRange(timeId, ignoreCloseTime)
    if string.isEmpty(timeId) then
        return true
    end

    local hasCfg, timeCfg = Tables.timeRangeTable:TryGetValue(timeId)
    if not hasCfg then
        logger.error("时间区间表不存在该timeId：" .. timeId)
        return false
    end

    local serverAreaTypeIndex = CSIndex(Utils.getServerAreaType():GetHashCode())
    if serverAreaTypeIndex < 0 or serverAreaTypeIndex >= timeCfg.timeRangeList.Count then
        logger.info("serverAreaType超出timeRangeList范围，serverAreaType={0}, timeId={1}", serverAreaTypeIndex, timeId)
        return false
    end
    local timeRange = timeCfg.timeRangeList[serverAreaTypeIndex]
    local timeZoneSeconds = Utils.getServerTimeZoneOffsetSeconds()
    local openTs = Utils.timeStr2TimeStamp(timeRange.openTime, timeZoneSeconds)
    local closeTs = nil
    if not ignoreCloseTime and not string.isEmpty(timeRange.closeTime) then
        closeTs = Utils.timeStr2TimeStamp(timeRange.closeTime, timeZoneSeconds)
    end

    local curTs = DateTimeUtils.GetCurrentTimestampBySeconds()
    if closeTs == nil then
        return curTs >= openTs
    else
        return curTs >= openTs and curTs < closeTs
    end
end



function Utils.getTimeIdOpenTimeStamp(timeId)
    if string.isEmpty(timeId) then
        return nil
    end
    local hasCfg, timeCfg = Tables.timeRangeTable:TryGetValue(timeId)
    if not hasCfg then
        logger.error("时间区间表不存在该timeId：" .. timeId)
        return nil
    end
    local serverAreaTypeInt = Utils.getServerAreaType():GetHashCode()
    local timeRange = timeCfg.timeRangeList[CSIndex(serverAreaTypeInt)]
    local timeZoneSeconds = Utils.getServerTimeZoneOffsetSeconds()
    return Utils.timeStr2TimeStamp(timeRange.openTime, timeZoneSeconds)
end



function Utils.isNotObtainCanShow(isNotObtainShow, notObtainShowTimeId)
    if isNotObtainShow then
        return true
    end
    if string.isEmpty(notObtainShowTimeId) then
        return false
    end
    return Utils.isCurTimeInTimeIdRange(notObtainShowTimeId)
end

function Utils.checkSettlementOrderCanSubmit(settlementId, domainId, context)
    local orderId = GameInstance.player.settlementSystem:GetSettlementOrderId(settlementId)
    if orderId == nil then
        return CS.Beyond.Gameplay.SettlementSystem.EOrderSubmitState.None
    end

    local itemConsumeDic = {}
    local orderData = Tables.settlementOrderDataTable[orderId]
    for _, costItem in pairs(orderData.costItems) do
        if context[costItem.id] == nil then
            context[costItem.id] = Utils.getDepotItemCount(costItem.id, nil, domainId)
        end
        itemConsumeDic[costItem.id] = itemConsumeDic[costItem.id] and itemConsumeDic[costItem.id] + costItem.count or costItem.count
    end

    local canSubmit = true
    for id, count in pairs(itemConsumeDic) do
        if context[id] < count then
            canSubmit = false
            break
        end
    end

    if canSubmit then
        for id, count in pairs(itemConsumeDic) do
            context[id] = context[id] - count
        end
    end
    return canSubmit
end

function Utils.getOrderSubmitStateBySettlementId(domainId, settlementId, itemContext)
    local oneCanSubmit = false
    local oneCantSubmit = false
    local orderId = GameInstance.player.settlementSystem:GetSettlementOrderId(settlementId)
    if orderId ~= nil then
        local canSubmit = Utils.checkSettlementOrderCanSubmit(settlementId, domainId, itemContext)
        if canSubmit then
            oneCanSubmit = true
        else
            oneCantSubmit = true
        end
    end

    if oneCanSubmit and not oneCantSubmit then
        return CS.Beyond.Gameplay.SettlementSystem.EOrderSubmitState.All
    elseif oneCanSubmit and oneCantSubmit then
        return CS.Beyond.Gameplay.SettlementSystem.EOrderSubmitState.Part
    elseif oneCantSubmit then
        return CS.Beyond.Gameplay.SettlementSystem.EOrderSubmitState.Zero
    else
        return CS.Beyond.Gameplay.SettlementSystem.EOrderSubmitState.None
    end
end

function Utils.getOrderSubmitStateByDomainId(domainId, itemContext)
    local oneCanSubmit = false
    local oneCantSubmit = false
    for i, settlementId in pairs(Tables.domainDataTable[domainId].settlementGroup) do
        local orderId = GameInstance.player.settlementSystem:GetSettlementOrderId(settlementId)
        if orderId ~= nil then
            local canSubmit = Utils.checkSettlementOrderCanSubmit(settlementId, domainId, itemContext)
            if canSubmit then
                oneCanSubmit = true
            else
                oneCantSubmit = true
            end
        end
    end
    if oneCanSubmit and not oneCantSubmit then
        return CS.Beyond.Gameplay.SettlementSystem.EOrderSubmitState.All
    elseif oneCanSubmit and oneCantSubmit then
        return CS.Beyond.Gameplay.SettlementSystem.EOrderSubmitState.Part
    elseif oneCantSubmit then
        return CS.Beyond.Gameplay.SettlementSystem.EOrderSubmitState.Zero
    else
        return CS.Beyond.Gameplay.SettlementSystem.EOrderSubmitState.None
    end
end

function Utils.intToEnum(enumType, value)
    return CS.System.Enum.ToObject(enumType, value)
end

function Utils.needMissionHud()
    if GameInstance.mode.hideMissionHud then
        return false
    end
    if GameInstance.player.towerDefenseSystem.hudState == CS.Beyond.Gameplay.TowerDefenseSystem.HUDState.WaitingFinished then
        return false
    end
    return true
end

function Utils.canJumpToSystem(jumpId)
    local cfg = Tables.systemJumpTable[jumpId]
    local isUnlock = Utils.isSystemUnlocked(cfg.bindSystem)
    if isUnlock then
        local phaseId = PhaseId[cfg.phaseId]
        local phaseArgs
        if not string.isEmpty(cfg.phaseArgs) then
            phaseArgs = Json.decode(cfg.phaseArgs)
        end
        if not phaseId or PhaseManager:CheckCanOpenPhase(phaseId, phaseArgs) then
            return true
        end
    end
    return false
end

function Utils.jumpToSystem(jumpId, callback)
    Utils.jumpToSystemWithItem(jumpId, callback, nil)
end

function Utils.jumpToSystemWithItem(jumpId, callback, itemId)
    local cfg = Tables.systemJumpTable[jumpId]
    local isUnlock = Utils.isSystemUnlocked(cfg.bindSystem)
    if not isUnlock then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SYSTEM_LOCK)
        return
    end

    local jumpChain = {}
    local curId = cfg.preJumpId
    local visited = {}
    while not string.isEmpty(curId) do
        if visited[curId] then
            break
        end
        visited[curId] = true
        local _, preCfg = Tables.systemJumpTable:TryGetValue(curId)
        if not preCfg then
            break
        end
        table.insert(jumpChain, preCfg)
        curId = preCfg.preJumpId
    end

    for i = #jumpChain, 1, -1 do
        local preCfg = jumpChain[i]
        local prePhaseId = PhaseId[preCfg.phaseId]
        if prePhaseId then
            local preArgs
            preArgs = Utils.buildSystemJumpPhaseArgsWithItem(preCfg, itemId)
            PhaseManager:OpenPhaseFast(prePhaseId, preArgs)
        end
    end

    local phaseId = PhaseId[cfg.phaseId]
    local phaseArgs
    phaseArgs = Utils.buildSystemJumpPhaseArgsWithItem(cfg, itemId)
    if phaseId == PhaseId.CharInfo then
        CharInfoUtils.openCharInfoBestWay(phaseArgs, callback)
    else
        PhaseManager:GoToPhase(phaseId, phaseArgs, callback)
    end
end

function Utils.buildSystemJumpPhaseArgsWithItem(cfg, itemId)
    local phaseArgs
    if not string.isEmpty(cfg.phaseArgs) then
        phaseArgs = Json.decode(cfg.phaseArgs)
    end
    if not string.isEmpty(itemId) then
        if cfg.autoUseItemArg then
            phaseArgs = phaseArgs or {}
            phaseArgs.itemId = itemId
        end
    end
    return phaseArgs
end

function Utils.unlockCraft(itemId)
    local hasCraft, craftIds = Tables.factoryItemAsManualCraftOutcomeTable:TryGetValue(itemId)
    local unlock = true
    if hasCraft then
        unlock = false
        for _, craftId in pairs(craftIds.list) do
            unlock = GameInstance.player.facManualCraft:IsCraftUnlocked(craftId)
            if unlock then
                break
            end
        end
    end
    return hasCraft, unlock
end


function Utils.nextStaminaRecoverLeftTime()
    local nextRecoverTime = GameInstance.player.inventory.staminaNextRecoverTime
    local curTime = DateTimeUtils.GetCurrentTimestampBySeconds()
    local nextLeftTime = nextRecoverTime - curTime
    if (nextLeftTime <= 0) then
        return 0
    else
        return nextLeftTime
    end
end

function Utils.fullStaminaRecoverLeftTime()
    local nextLeftTime = Utils.nextStaminaRecoverLeftTime()
    local curStamina = GameInstance.player.inventory.curStamina
    local maxStamina = GameInstance.player.inventory.maxStamina
    local fullLeftTime = (maxStamina - curStamina - 1) * Tables.dungeonConst.staminaRecoverDuration + nextLeftTime
    if (fullLeftTime <= 0) then
        return 0
    else
        return fullLeftTime
    end
end

function Utils.tryGetTableCfg(table, id)
    local hasCfg, cfg = table:TryGetValue(id)
    if not hasCfg then
        logger.error(ELogChannel.Cfg, "[Utils.tryGetTableCfg] missing cfg, id = "..id)
        return nil
    end
    return cfg
end

function Utils.getImgGenderDiffPath(imgText)
    local path = string.match(imgText, UIConst.UI_RICH_CONTENT_IMG_GENDER_DIFF_MATCH)
    if not path then
        path = imgText
    else
        local isMale = Utils.getPlayerGender() == CS.Proto.GENDER.GenMale
        if isMale then
            path = string.format(UIConst.UI_RICH_CONTENT_IMG_GENDER_DIFF_FORMAT_MALE, path)
        else
            path = string.format(UIConst.UI_RICH_CONTENT_IMG_GENDER_DIFF_FORMAT_FEMALE, path)
        end
    end
    return path
end

function Utils.isCurSquadAllDead()
    return GameInstance.player.squadManager:IsAllDead()
end

function Utils.zoomCamera(delta)
    if LuaSystemManager.factory.inTopView then
        LuaSystemManager.factory.m_topViewCamCtrl:ZoomCamera(delta, true, function(pos)
            return FactoryUtils.clampTopViewCamTargetPosition(pos)
        end)
    else
        CameraManager:Zoom(delta)
    end
end

function Utils.getClientVar(key, default_value)
    local globalVar = GameInstance.player.globalVar
    if globalVar then
        local res, data = globalVar:TryGetClientVar(key)
        if res then
            return data
        end
    end

    return default_value
end

function Utils.getGuideText(key)
    local find, textInfo = Tables.textTable:TryGetValue(key)
    if find then
        return textInfo
    end
    return ""
end

function Utils.compareInt(a,b,compareOperator)
    if compareOperator ==  GEnums.CompareOperator.Equal then
        return a == b;
    end
    if compareOperator ==  GEnums.CompareOperator.NotEqual then
        return a ~= b;
    end
    if compareOperator ==  GEnums.CompareOperator.GreaterThan then
        return a > b;
    end
    if compareOperator ==  GEnums.CompareOperator.GreaterEqual then
        return a >= b;
    end
    if compareOperator ==  GEnums.CompareOperator.LessThan then
        return a < b;
    end
    if compareOperator ==  GEnums.CompareOperator.LessEqual then
        return a <= b;
    end
    return false
end

function Utils.openURL(url)
    CS.Beyond.UI.WebApplication.Start(url)
end








function Utils.getLTItemExpireInfo(itemId, instId)
    local info = {
        almostExpireTime = 0,
        isLTItem = false,
        expireTime = 0,
        isExpire = true,
    }

    local inventory = GameInstance.player.inventory
    local itemCfg = Tables.itemTable[itemId]
    local hasCfg, ltItemCfg = Tables.lTItemTypeTable:TryGetValue(itemCfg.type)
    if hasCfg then
        info.almostExpireTime = ltItemCfg.daysBeforeExpireToNotify * Const.SEC_PER_DAY
    end
    local valuableDepotType = itemCfg.valuableTabType;
    local contains = inventory.valuableDepots:ContainsKey(valuableDepotType)
    if contains then

        local depot = inventory.valuableDepots[valuableDepotType]:GetOrFallback(CS.Beyond.Gameplay.Scope.Create(GEnums.ScopeName.Main))
        if depot then

            local hasData, itemBundle = depot.instItems:TryGetValue(instId)
            if hasData and itemBundle.isLimitTimeItem then
                info.isLTItem = true
                info.expireTime = itemBundle.instData.expireTs
                info.isExpire = itemBundle.isExpire
            end
        end
    end
    return info
end

function Utils.isSettlementDefenseGuideCompleted(mapId)
    local missionIds = Tables.settlementConst.defenseGuideMissionIds
    if mapId and mapId == Tables.settlementConst.specialDefenseGuideMapId then
        missionIds = Tables.settlementConst.specialDefenseGuideMissionIds
    end
    if #missionIds == 0 then
        return true
    end
    for _, missionId in pairs(missionIds) do
        if GameInstance.player.mission:GetMissionState(missionId) ==
            CS.Beyond.Gameplay.MissionSystem.MissionState.Completed then
            return true
        end
    end
    return false
end

function Utils.getCurMissionIdAndDesc(descType)
    local missionSystem = GameInstance.player.mission
    local curMissionId = missionSystem:GetLatestMainMissionId()
    local curMissionDesc = Language.LUA_GACHA_STARTER_ALL_MISSION_COMPLETE
    if not string.isEmpty(curMissionId) then
        local curMissionState = missionSystem:GetMissionState(curMissionId)
        if curMissionState == CS.Beyond.Gameplay.MissionSystem.MissionState.Processing then
            local chapterId = missionSystem:GetChapterIdByMissionId(curMissionId)
            local chapterInfo = missionSystem:GetChapterInfo(chapterId)
            if descType == "gacha" then
                curMissionDesc = string.format(Language.LUA_GACHA_STARTER_CUR_MISSION_DESC, chapterInfo.chapterNum:GetText(), chapterInfo.episodeNum:GetText())
            elseif descType == "activity" then
                curMissionDesc = string.format(Language.LUA_ACTIVITY_UNLOCK_CUR_MISSION_DESC, chapterInfo.chapterNum:GetText(), chapterInfo.episodeNum:GetText(), missionSystem:GetMissionInfo(curMissionId).missionName:GetText())
            end
        end
    end
    return curMissionId, curMissionDesc
end

function Utils.checkIsPSDevice()
    return UNITY_PS4 or UNITY_PS5
end

function Utils.tryGetItemFirstObtainWay(itemId)
    local itemCfg = Tables.itemTable:GetValue(itemId)
    if itemCfg.obtainWayIds then
        for k, obtainWayId in pairs(itemCfg.obtainWayIds) do
            local _, obtainWayCfg = Tables.systemJumpTable:TryGetValue(obtainWayId)
            if obtainWayCfg then
                local isUnlock = Utils.isSystemUnlocked(obtainWayCfg.bindSystem)
                if isUnlock then
                    local phaseId = PhaseId[obtainWayCfg.phaseId]
                    local phaseArgs
                    if not string.isEmpty(obtainWayCfg.phaseArgs) then
                        phaseArgs = Json.decode(obtainWayCfg.phaseArgs)
                    end
                    if not phaseId or PhaseManager:CheckCanOpenPhase(phaseId, phaseArgs) then
                        return {
                            name = obtainWayCfg.desc,
                            iconFolder = UIConst.UI_SPRITE_ITEM_TIPS,
                            iconId = obtainWayCfg.iconId,
                            phaseId = phaseId,
                            phaseArgs = phaseArgs,
                        }
                    end
                end
            end
        end
    end
    return nil
end


function Utils.reportPlacementEvent(eventType)
    if GameInstance.player.gameDataReportSystem:IsPlacementEventReported(eventType) then
        return
    end
    GameInstance.player.gameDataReportSystem:ReportPlacementEvent(eventType)
end

function Utils.isPortableDevice(itemId)
    return not string.isEmpty(itemId) and Tables.itemPortableDeviceTable:ContainsKey(itemId)
end

function Utils.getLevelConfig(levelId)
    return DataManager:TryGetLevelConfig(levelId)
end

function Utils.FreezeWorldByUI()
    local handle = TimeManagerInst:StartChangeTimeScale(0, CS.Beyond.TimeManager.ChangeTimeScaleReason.UIPanel)
    if GameWorld.isInited then
        GameWorld.cutsceneManager:PauseTimelineByUI(true)
        GameWorld.levelSeqManager:PauseTimelineByUI(true)
        GameWorld.dialogTimelineManager:PauseTimelineByUI(true)
    end
    return handle
end

function Utils.ResumeWorldByUI(handle)
    if handle <= 0 then
        return
    end

    TimeManagerInst:StopChangeTimeScale(handle)
    if GameWorld.isInited then
        GameWorld.cutsceneManager:PauseTimelineByUI(false)
        GameWorld.levelSeqManager:PauseTimelineByUI(false)
        GameWorld.dialogTimelineManager:PauseTimelineByUI(false)
    end
end


_G.Utils = Utils
return Utils