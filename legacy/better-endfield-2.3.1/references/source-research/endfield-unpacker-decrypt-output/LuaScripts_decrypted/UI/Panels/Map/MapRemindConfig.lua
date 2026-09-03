


















local Config = {
    [GEnums.MapRemindType.MaxRecycleBin] = {
        redDotName = "",
        Check = function(levelId)
            return GameInstance.player.recycleBinSystem:GetAllCanPickUpInstIdsByGroup(levelId)
        end,

        mapMarkType = GEnums.MarkType.Recycler,
    },
    [GEnums.MapRemindType.NewMine] = {
        redDotName = "",
        Check = function(levelId)
            return GameInstance.player.doodadSystem:GetNewMineIds(levelId)
        end,
    },
    [GEnums.MapRemindType.MaxRareCollection] = {
        redDotName = "",
        Check = function(levelId)
            return GameInstance.player.doodadSystem:GetRareDoodadIds(levelId)
        end,
    },
    [GEnums.MapRemindType.MaxRareMine] = {
        redDotName = "",
        Check = function(levelId)
            return GameInstance.player.doodadSystem:GetRareMineIds(levelId)
        end,
    },
    [GEnums.MapRemindType.SettlementDefenseTerminal] = {
        redDotName = "",
        Check = function(levelId)
            local mapId = nil
            local success, levelConfig = Utils.getLevelConfig(levelId)
            if success then
                mapId = levelConfig.mapIdStr
            end
            if not Utils.isSettlementDefenseGuideCompleted(mapId) then
                return {}
            end
            return GameInstance.player.towerDefenseSystem.dangerSettlementIds
        end,
        mapMarkType = GEnums.MarkType.SettlementDefenseTerminal,
    },
    [GEnums.MapRemindType.SSGrowCabinCanCollect] = {
        Check = function(levelId)
            local ids = {}
            if levelId ~= Tables.spaceshipConst.baseSceneName then
                return ids
            end
            local rooms = GameInstance.player.spaceship:GetRoomIdsByType(GEnums.SpaceshipRoomType.GrowCabin)
            for i = CSIndex(1), CSIndex(rooms.Count) do
                local roomId = rooms[i]
                if GameInstance.player.spaceship:HasGrowCabinProduct(roomId) then
                    local instId = GameInstance.player.mapManager:GetMapInstIdBySpaceshipRoomId(roomId)
                    if instId and instId ~= "" then
                        table.insert(ids, instId)
                    end
                end
            end
            return ids
        end,
        useMarkIcon = true,
    },
    [GEnums.MapRemindType.SSManufacturingStationDone] = {
        Check = function(levelId)
            local ids = {}
            if levelId ~= Tables.spaceshipConst.baseSceneName then
                return ids
            end
            local rooms = GameInstance.player.spaceship:GetRoomIdsByType(GEnums.SpaceshipRoomType.ManufacturingStation)
            for i = CSIndex(1), CSIndex(rooms.Count) do
                local roomId = rooms[i]
                if GameInstance.player.spaceship:HasProductToCollect(roomId) then
                    local instId = GameInstance.player.mapManager:GetMapInstIdBySpaceshipRoomId(roomId)
                    if instId and instId ~= "" then
                        table.insert(ids, instId)
                    end
                end
            end
            return ids
        end,
        useMarkIcon = true,
    },
    [GEnums.MapRemindType.SSRoomCanBuildOrLevelUp] = {
        Check = function(levelId)
            local ids = {}
            if levelId ~= Tables.spaceshipConst.baseSceneName then
                return ids
            end
            local spaceship = GameInstance.player.spaceship
            local mapManager = GameInstance.player.mapManager

            for roomId, _ in pairs(Tables.spaceshipEmptyRoomTable) do
                if roomId == Tables.spaceshipConst.guestRoomId then
                    goto continue
                end
                if spaceship:IsRoomCanLevelUp(roomId) then
                    local instId = mapManager:GetMapInstIdBySpaceshipRoomId(roomId)
                    if instId and instId ~= "" then
                        if roomId == Tables.spaceshipConst.controlCenterRoomId then
                            local succ, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(instId)
                            if not succ or not markRuntimeData.isActive then
                                goto continue
                            end
                        end
                        table.insert(ids, instId)
                    end
                end
                :: continue ::
            end
            return ids
        end,
        useMarkIcon = true,
    },
    [GEnums.MapRemindType.SSRoomCanSetCharacter] = {
        Check = function(levelId)
            local ids = {}
            if levelId ~= Tables.spaceshipConst.baseSceneName then
                return ids
            end
            local rooms = GameInstance.player.spaceship.rooms
            for id, roomInfo in pairs(rooms) do
                local stationCharNum = roomInfo.stationedCharList.Count
                local maxStationCharNum = roomInfo.maxStationCharNum
                if stationCharNum < maxStationCharNum then
                    local instId = GameInstance.player.mapManager:GetMapInstIdBySpaceshipRoomId(id)
                    if instId and instId ~= "" then
                        if id == Tables.spaceshipConst.controlCenterRoomId then
                            local succ, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(instId)
                            if succ and markRuntimeData.isActive then
                                table.insert(ids, instId)
                            end
                        else
                            table.insert(ids, instId)
                        end
                    end
                end
            end
            return ids
        end,
        useMarkIcon = true,
    },
    [GEnums.MapRemindType.AnyPOICanUnlock] = {
        Check = function(levelId)
            local ids = nil
            local insId = nil
            local finalPOIType = nil
            local values = CS.System.Enum.GetValues(typeof(GEnums.DomainPoiType))
            for i = 0, values.Length - 1 do
                local poiType = values[i]
                if DomainPOIUtils.POICanUnlock[poiType] ~= nil then
                    ids = DomainPOIUtils.POICanUnlock[poiType](levelId)
                    if ids then
                        if type(ids) == "table" then
                            if #ids > 0 then
                                insId = ids[1]
                                finalPOIType = poiType
                                break
                            end
                        elseif type(ids) == "userdata" then
                            if ids.Count ~= 0 then
                                insId = ids[0]
                                finalPOIType = poiType
                                break
                            end
                        end
                    end
                end
            end
            if insId == nil then
                return {}
            end
            if finalPOIType == nil then
                logger.error(string.format("MapRemindConfig AnyPOICanUnlock finalPOIType为空, levelId=%s, insId=%s",
                    tostring(levelId), tostring(insId)))
            end
            local markType = DomainPOIUtils.MarkTypeMap[finalPOIType]
            if markType == nil then
                logger.error(string.format("MapRemindConfig AnyPOICanUnlock MarkTypeMap缺失, levelId=%s, poiType=%s, insId=%s",
                    tostring(levelId), tostring(finalPOIType), tostring(insId)))
            end

            local _,markId = GameInstance.player.mapManager:GetMapMarkInstId(markType, insId)
            return { markId }
        end,
        useMarkIcon = true,
    },
    [GEnums.MapRemindType.AnyPOICanUpgrade] = {
        Check = function(levelId)
            local ids = nil
            local insId = nil
            local finalPOIType = nil
            local values = CS.System.Enum.GetValues(typeof(GEnums.DomainPoiType))
            for i = 0, values.Length - 1 do
                local poiType = values[i]
                if DomainPOIUtils.POICanUpgrade[poiType] ~= nil then
                    ids = DomainPOIUtils.POICanUpgrade[poiType](levelId)
                    if ids then
                        if type(ids) == "table" then
                            if #ids > 0 then
                                insId = ids[1]
                                finalPOIType = poiType
                                break
                            end
                        elseif type(ids) == "userdata" then
                            if ids.Count ~= 0 then
                                insId = ids[0]
                                finalPOIType = poiType
                                break
                            end
                        end
                    end
                end
            end
            if insId == nil then
                return {}
            end

            local _,markId = GameInstance.player.mapManager:GetMapMarkInstId(DomainPOIUtils.MarkTypeMap[finalPOIType], insId)
            return { markId }
        end,
        useMarkIcon = true,
    },
    [GEnums.MapRemindType.SSGuestRoomBaseClueCanRecv] = {
        Check = function(levelId)
            local ids = {}
            if levelId ~= Tables.spaceshipConst.baseSceneName then
                return ids
            end
            local clueData = GameInstance.player.spaceship:GetClueData()
            if not clueData then
                return ids
            end
            if clueData.dailyClueIndex ~= 0 then
                local instId = GameInstance.player.mapManager:GetMapInstIdBySpaceshipRoomId(Tables.spaceshipConst.guestRoomClueExtensionId)
                table.insert(ids, instId)
            end
            return ids
        end,
    },
    [GEnums.MapRemindType.SSGuestRoomSelfClueIsFull] = {
        Check = function(levelId)
            local ids = {}
            if levelId ~= Tables.spaceshipConst.baseSceneName then
                return ids
            end
            if GameInstance.player.spaceship:IsGuestRoomClueCannotAutoRecv() then
                local instId = GameInstance.player.mapManager:GetMapInstIdBySpaceshipRoomId(Tables.spaceshipConst.guestRoomClueExtensionId)
                table.insert(ids, instId)
            end
            return ids
        end,
    },
    [GEnums.MapRemindType.NewGasMine] = {
        redDotName = "",
        Check = function(levelId)
            return GameInstance.player.doodadSystem:GetNewGasMineIds(levelId)
        end,
    },
    [GEnums.MapRemindType.InvalidBuilding] = {
        redDotName = "",
        state = "Hint",
        Check = function(levelId)
            local ids = {}
            if Utils.isInSpaceShip() then
                return ids
            end

            local chapterId = ScopeUtil.GetChapterId(levelId)
            if chapterId ~= Utils.getCurrentChapterId() then
                return ids
            end

            local invalidPlacedBuildings = GameInstance.player.remoteFactory:GetInvalidPlacedBuildings(chapterId)
            if invalidPlacedBuildings ~= nil and invalidPlacedBuildings.Count > 0 then
                local instId = GameInstance.player.mapManager:GetFacMarkInstId(chapterId, invalidPlacedBuildings[0].nodeId)
                table.insert(ids, instId)
            end
            return ids
        end,
        onClick = function(levelId)
            local chapterId = ScopeUtil.GetChapterId(levelId)
            UIManager:Open(PanelId.MapWaitDeleteBuildingList, chapterId)
        end,
    }
}

return Config