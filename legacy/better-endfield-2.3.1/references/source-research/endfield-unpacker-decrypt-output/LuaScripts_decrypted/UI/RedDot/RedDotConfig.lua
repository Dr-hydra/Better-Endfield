

local Config = {
    NormalItem = {
        msgs = {
            MessageConst.ON_READ_NEW_ITEM,
        },
        readLike = true,
        needArg = true,
        Check = function(id)
            return GameInstance.player.inventory:IsNewItem(id), UIConst.RED_DOT_TYPE.New
        end,
    },
    InstItem = {
        msgs = {
            MessageConst.ON_READ_NEW_INST_ITEM,
        },
        readLike = true,
        needArg = true,
        Check = function(info)
            return GameInstance.player.inventory:IsNewItem(info.id, info.instId), UIConst.RED_DOT_TYPE.New
        end,
    },
    ValuableDepotItem = {
        msgs = {
            MessageConst.ON_READ_NEW_ITEM,
            MessageConst.ON_READ_NEW_INST_ITEM,
            MessageConst.ON_ITEM_COUNT_CHANGED,
            MessageConst.ON_VALUABLE_DEPOT_IMPORT_ITEM_CHANGED,
        },
        readLike = false,
        needArg = true,
        Check = function(arg)
            local id = arg
            local instId
            if type(arg) == "table" then
                id = arg.id
                instId = arg.instId
            end
            if RedDotUtils.isNewObtainedImportantValuableDepotItem(id) then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            if instId then
                return GameInstance.player.inventory:IsNewItem(id, instId), UIConst.RED_DOT_TYPE.New
            end
            return GameInstance.player.inventory:IsNewItem(id), UIConst.RED_DOT_TYPE.New
        end,
    },
    BlocShopDiscountShopItem = {
        msgs = {
            MessageConst.ON_BUY_ITEM_SUCC,
            MessageConst.ON_SYNC_ALL_BLOC,
        },
        readLike = false,
        needArg = true,
        Check = function(blocId)
            return RedDotUtils.hasBlocShopDiscountShopItem(blocId)
        end
    },
    WeaponEmptyGem = {
        msgs = {
            MessageConst.ON_GEM_DETACH,
            MessageConst.ON_GEM_ATTACH,
        },
        readLike = true,
        needArg = true,
        Check = function(weaponInstId)
            local weaponInst = CharInfoUtils.getWeaponByInstId(weaponInstId)
            if weaponInst.attachedGemInstId > 0 then
                return false
            end
            return RedDotUtils.hasGemNotEquipped()
        end
    },
    WeaponCanUpgrade = {
        msgs = {
            MessageConst.ON_WEAPON_GAIN_EXP,
            MessageConst.ON_WEAPON_BREAKTHROUGH,
        },
        readLike = true,
        needArg = true,
        Check = function(weaponInstId)
            return WeaponUtils.canWeaponBreakthrough(weaponInstId) or WeaponUtils.canWeaponUpgrade(weaponInstId)
        end
    },
    Formula = {
        msgs = {
            MessageConst.ON_ADD_NEW_UNREAD_FORMULA,
            MessageConst.ON_READ_FORMULA,
        },
        readLike = true,
        needArg = true,
        Check = function(formulaId)
            return GameInstance.player.remoteFactory.core:IsFormulaUnread(formulaId), UIConst.RED_DOT_TYPE.New
        end,
    },
    BuildingFormula = {
        sons = {
            Formula = false,
        },
        readLike = false,
        needArg = true,
        Check = function(arg)
            local buildingId = arg.buildingId
            local modeName = arg.modeName
            local core = GameInstance.player.remoteFactory.core
            local bData = Tables.factoryBuildingTable:GetValue(buildingId)
            local bType = bData.type
            if bType == GEnums.FacBuildingType.MachineCrafter or bType == GEnums.FacBuildingType.FluidReaction then

                local machineCrafterData = FactoryUtils.getMachineCraftGroupData(buildingId, modeName)
                for _, craftId in pairs(machineCrafterData.craftList) do
                    if core:IsFormulaUnread(craftId) and not FactoryUtils.isExpiredTimeLimitedFormula(craftId) then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end
            return false
        end,
    },

    TechTree = {
        msgs = {
            MessageConst.ON_UNLOCK_FAC_TECH_PACKAGE,
            MessageConst.ON_UNHIDDEN_FAC_TECH_PACKAGE,
            MessageConst.ON_CHANGE_SPACESHIP_DOMAIN_ID,
        },
        readLike = false,
        needArg = false,
        Check = function()
            local levelId = GameWorld.worldInfo.curLevelId
            if string.isEmpty(levelId) then
                logger.warn("RedDotConfig->TechTree Check->levelId is None")
                return false
            end
            local _, levelBasicInfo = DataManager.levelBasicInfoTable:TryGetValue(levelId)
            local isInSpaceShip = Utils.isInSpaceShip()
            local domainId = isInSpaceShip and GameInstance.player.inventory.spaceshipDomainId or levelBasicInfo.domainName
            local hasDomain, domainCfg = Tables.domainDataTable:TryGetValue(domainId)
            if not hasDomain then
                return false
            end
            local facTechPackageId = domainCfg.facTechPackageId
            local techTreeSystem = GameInstance.player.facTechTreeSystem

            if techTreeSystem:PackageIsHidden(facTechPackageId) then
                return false
            end

            if techTreeSystem:PackageIsLocked(facTechPackageId) then
                return false
            end

            local packageCfg = Tables.facSTTGroupTable[facTechPackageId]

            for _, layerId in pairs(packageCfg.layerIds) do
                local groupState = RedDotManager:GetRedDotState("TechTreeLayer", layerId)
                if groupState then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end

            for _, techId in pairs(packageCfg.techIds) do
                local groupState = RedDotManager:GetRedDotState("TechTreeNode", techId)
                if groupState then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end

            local blackboxEntryState = RedDotManager:GetRedDotState("BlackboxEntry", facTechPackageId)
            if blackboxEntryState then
                return true, UIConst.RED_DOT_TYPE.Normal
            end

            return false
        end,
        sons = {
            TechTreeNode = false,
            TechTreeLayer = false,
            BlackboxEntry = false,
        }
    },
    TechTreeLayer = {
        msgs = {
            MessageConst.FAC_ON_REFRESH_TECH_TREE_UI,
            MessageConst.ON_ITEM_COUNT_CHANGED,
        },
        readLike = false,
        needArg = true,
        Check = function(layerId)
            local techTreeSystem = GameInstance.player.facTechTreeSystem
            if not techTreeSystem:LayerIsLocked(layerId) then
                return false
            end

            local layerCfg = Tables.facSTTLayerTable[layerId]
            local isEnough = true
            for _, costItem in pairs(layerCfg.costItems) do
                local ownCount = Utils.getItemCount(costItem.costItemId)
                local costCount = costItem.costItemCount
                if ownCount < costCount then
                    isEnough = false
                    break
                end
            end

            return isEnough, UIConst.RED_DOT_TYPE.Normal
        end
    },
    TechTreeNode = {
        msgs = {
            MessageConst.FAC_ON_REFRESH_TECH_TREE_UI,
            MessageConst.ON_ITEM_COUNT_CHANGED,
        },
        readLike = false,
        needArg = true,
        Check = function(techId)
            local techTreeSystem = GameInstance.player.facTechTreeSystem
            local nodeData = Tables.facSTTNodeTable:GetValue(techId)

            if not techTreeSystem:NodeIsLocked(techId) then
                return false
            end

            if techTreeSystem:PreNodeIsLocked(techId) then
                return false
            end

            if techTreeSystem:LayerIsLocked(nodeData.layer) then
                return false
            end

            if techTreeSystem:NodeIsHidden(techId) then
                return false
            end

            local isMatchCondition = true
            if nodeData.conditions.Count > 0 then
                for i = 1, nodeData.conditions.Count do
                    if not techTreeSystem:GetConditionIsCompleted(techId,
                        nodeData.conditions[CSIndex(i)].conditionId) then
                        isMatchCondition = false
                        break
                    end
                end
            end

            if not isMatchCondition then
                return false
            end

            local isEnough = Utils.getItemCount(Tables.facSTTGroupTable[nodeData.groupId].costPointType)
                >= nodeData.costPointCount
            if not isEnough then
                return false
            end

            return true, UIConst.RED_DOT_TYPE.Normal
        end
    },

    BlackboxPreDependencies = {
        readLike = true,
        needArg = true,
        Check = function(blackboxIds)
            for _, blackboxId in pairs(blackboxIds) do
                if not GameInstance.dungeonManager:IsDungeonPassed(blackboxId) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end,
        sons = {
            BlackboxSelectionCellPassed = false,
        }
    },
    BlackboxSelectionCellPassed = {
        msgs = {
        },
        readLike = true,
        needArg = true,
        Check = function(blackboxId)
            if not GameInstance.dungeonManager:IsDungeonPassed(blackboxId) then
                return true, UIConst.RED_DOT_TYPE.Normal
            end

            return false
        end
    },
    BlackboxEntry = {
        readLike = false,
        needArg = true,
        Check = function(packageId)
            local packageCfg = Tables.facSTTGroupTable[packageId]
            for _, blackboxId in pairs(packageCfg.blackboxIds) do
                local hasReadDotState = RedDotManager:GetRedDotState("BlackboxSelectionCellRead", blackboxId)
                if hasReadDotState then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end,
        sons = {
            BlackboxSelectionCellRead = false,
        },
    },
    BlackboxSelectionCellRead = {
        msgs = {
            MessageConst.ON_SUB_GAME_ACTIVE,
            MessageConst.ON_BLACKBOX_READ,
        },
        readLike = true,
        needArg = true,
        Check = function(blackboxId)
            local dungeonMgr = GameInstance.dungeonManager
            if not dungeonMgr:IsDungeonActive(blackboxId) then
                return false
            end

            if dungeonMgr:IsBlackboxRead(blackboxId) then
                return false
            end

            return true, UIConst.RED_DOT_TYPE.New
        end
    },

    SingleBlueprint = {
        msgs = {
            MessageConst.FAC_ON_READ_BLUEPRINT,
        },
        readLike = true,
        needArg = true,
        Check = function(id)
            local isNew
            local t = type(id)
            if t == "number" then
                isNew = GameInstance.player.remoteFactory.blueprint:IsNewMyBlueprint(id)
            elseif t == "string" then
                isNew = GameInstance.player.remoteFactory.blueprint:IsNewSysBlueprint(id)
            else
                isNew = GameInstance.player.remoteFactory.blueprint:IsNewGiftBlueprint(id)
            end
            if isNew then
                return true, UIConst.RED_DOT_TYPE.New
            else
                return false
            end
        end,
    },

    SingleMail = {
        msgs = {
            MessageConst.ON_ALL_MAIL_INITED,
            MessageConst.ON_READ_MAIL,
            MessageConst.ON_GET_MAIL_ATTACHMENT,
            MessageConst.ON_GET_NEW_MAILS,
        },
        readLike = true,
        needArg = true,
        Check = function(mailId)
            local mailSys = GameInstance.player.mail
            if not mailSys:IsAllMailInited() then
                return false
            end
            local mail = mailSys.mails[mailId]
            if mail.isExpired then
                return false
            end
            if not mail.isRead then
                return true, UIConst.RED_DOT_TYPE.New
            end
            if not mail.collected then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end,
    },
    MailTab = {
        sons = {
            SingleMail = false,
        },
        readLike = false,
        needArg = false,
        Check = function()
            local mailSys = GameInstance.player.mail
            if not mailSys:IsAllMailInited() then
                return false
            end
            for _, mail in pairs(mailSys.mails) do
                if (not mail.isExpired) and (not mail.collected or not mail.isRead) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end,
    },
    MailTabGetAllBtn = {
        sons = {
            SingleMail = false,
        },
        readLike = false,
        needArg = false,
        Check = function()
            local mailSys = GameInstance.player.mail
            if not mailSys:IsAllMailInited() then
                return false
            end
            for _, mail in pairs(mailSys.mails) do
                if (not mail.isExpired) and (not mail.collected) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end,
    },
    Mail = {
        sons = {
            SingleMail = false,
            LostAndFoundBtn = false,
        },
        readLike = false,
        needArg = false,
        Check = function()
            local hasRedDot = GameInstance.player.mail:HasNewMail() or not GameInstance.player.inventory.lostAndFound:IsEmpty()
            return hasRedDot, UIConst.RED_DOT_TYPE.Normal
        end,
    },
    LostAndFoundBtn = {
        msgs = {
            MessageConst.ON_GET_LOST_AND_FOUND,
            MessageConst.ON_ADD_LOST_AND_FOUND,
        },
        Check = function()
            return not GameInstance.player.inventory.lostAndFound:IsEmpty(), UIConst.RED_DOT_TYPE.Normal
        end,
        needArg = false,
        readLike = false,
    },


    Gacha = {
        msgs = {
            MessageConst.ON_GACHA_POOL_INFO_CHANGED,
            MessageConst.ON_GACHA_POOL_ROLE_DATA_CHANGED,
            MessageConst.ON_ACTIVITY_GACHA_BEGINNER_STAGE_MODIFY,
            MessageConst.ON_GACHA_POOL_NEW_OPENED_READ,
            MessageConst.ON_VALUABLE_DEPOT_CHANGED,
        },
        needArg = false,
        Check = function()
            return RedDotUtils.hasGachaRedDot()
        end,
        readLike = false,
        sons = {
            GachaSinglePool = false,
        }
    },

    GachaSinglePool = {
        msgs = {
            MessageConst.ON_GACHA_POOL_INFO_CHANGED,
            MessageConst.ON_GACHA_POOL_ROLE_DATA_CHANGED,
            MessageConst.ON_ACTIVITY_GACHA_BEGINNER_STAGE_MODIFY,
            MessageConst.ON_GACHA_POOL_NEW_OPENED_READ,
            MessageConst.ON_VALUABLE_DEPOT_CHANGED,
        },
        needArg = true,
        Check = function(poolId)
            return RedDotUtils.hasGachaSinglePoolRedDot(poolId)
        end,
        readLike = false,
    },

    GachaCharTenLtTicket = {
        msgs = {
            MessageConst.ON_VALUABLE_DEPOT_CHANGED,
        },
        needArg = true,
        Check = function(poolId)
            return RedDotUtils.hasGachaTenLtTicketRedDot(poolId)
        end,
        readLike = false,
    },



    GachaWeaponCharTenLtTicket = {
        msgs = {
            MessageConst.ON_VALUABLE_DEPOT_CHANGED,
        },
        needArg = true,
        Check = function(poolId)
            return RedDotUtils.hasGachaWeaponTenLtTicketRedDot(poolId)
        end,
        readLike = false,
    },



    PRTSReading = {
        msgs = {
            MessageConst.ON_PRTS_TERMINAL_READ,
        },
        readLike = true,
        needArg = true,
        Check = function(uniqId)
            return not GameInstance.player.prts.prtsTerminalContentSet:Contains(uniqId), UIConst.RED_DOT_TYPE.Normal
        end
    },

    PRTSWatch = {
        readLike = false,
        needArg = false,
        Check = function()

            local prtsSys = GameInstance.player.prts
            for id, _ in pairs(Tables.prtsInvestigate) do
                if not prtsSys:IsInvestigateFinished(id) and prtsSys:IsInvestigateCanFinish(id) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end,
        sons = {
            PRTSDocument = false,
            PRTSText = false,
            PRTSMultimedia = false,
            PRTSInvestigateTab = false,
        },
    },

    PRTSDocument = {
        readLike = false,
        needArg = false,
        Check = function()
            local unlockSet = GameInstance.player.prts.prtsUnlockSet
            local unreadSet = GameInstance.player.prts.prtsUnReadSet
            for _, unreadId in pairs(unreadSet) do
                if unlockSet:Contains(unreadId) and Tables.prtsDocument:ContainsKey(unreadId) then
                    return true, UIConst.RED_DOT_TYPE.New
                end
            end
            return false
        end,
        sons = {
            PRTSItem = false
        },
    },
    PRTSText = {
        readLike = false,
        needArg = false,
        Check = function()
            local unlockSet = GameInstance.player.prts.prtsUnlockSet
            local unreadSet = GameInstance.player.prts.prtsUnReadSet
            for _, unreadId in pairs(unreadSet) do
                if unlockSet:Contains(unreadId) and Tables.prtsRecord:ContainsKey(unreadId) then
                    return true, UIConst.RED_DOT_TYPE.New
                end
            end
            return false
        end,
        sons = {
            PRTSItem = false
        },
    },
    PRTSStoryCollCategory = {
        readLike = false,
        needArg = true,
        Check = function(categoryId)
            local unreadSet = GameInstance.player.prts.prtsUnReadSet
            local prtsSystem = GameInstance.player.prts
            for _, unreadId in pairs(unreadSet) do
                local id = prtsSystem:GetCategoryIdByPrtsId(unreadId)
                if categoryId == id then
                    return true, UIConst.RED_DOT_TYPE.New
                end
            end
            return false
        end,
        sons = {
            PRTSItem = false
        },
    },
    PRTSMultimedia = {
        readLike = false,
        needArg = false,
        Check = function()
            local unlockSet = GameInstance.player.prts.prtsUnlockSet
            local unreadSet = GameInstance.player.prts.prtsUnReadSet
            for _, unreadId in pairs(unreadSet) do
                if unlockSet:Contains(unreadId) and Tables.prtsMultimedia:ContainsKey(unreadId) then
                    return true, UIConst.RED_DOT_TYPE.New
                end
            end
            return false
        end,
        sons = {
            PRTSItem = false
        },
    },
    PRTSFirstLv = {
        readLike = false,
        needArg = true,
        Check = function(firstLvId)
            local unreadSet = GameInstance.player.prts.prtsUnReadSet
            for _, unreadId in pairs(unreadSet) do
                local _, prtsData = Tables.prtsAllItem:TryGetValue(unreadId)
                if prtsData and prtsData.firstLvId == firstLvId then
                    return true, UIConst.RED_DOT_TYPE.New
                end
            end
            return false
        end,
        sons = {
            PRTSItem = false
        },
    },
    PRTSItem = {
        msgs = {
            MessageConst.ON_UNREAD_PRTS,
            MessageConst.ON_READ_PRTS,
        },
        readLike = false,
        needArg = true,
        Check = function(prtsId)
            local unlockSet = GameInstance.player.prts.prtsUnlockSet
            local unreadSet = GameInstance.player.prts.prtsUnReadSet
            if unlockSet:Contains(prtsId) and unreadSet:Contains(prtsId) then
                return true, UIConst.RED_DOT_TYPE.New
            end
            return false
        end,
    },
    PRTSInvestigateTab = {
        readLike = true,
        needArg = false,
        Check = function()

            local prtsSys = GameInstance.player.prts
            local hasUnread = false
            for investId, _ in pairs(Tables.prtsInvestigate) do
                local cfg = Utils.tryGetTableCfg(Tables.prtsInvestigate, investId)
                if cfg then
                    local isFinished = prtsSys:IsInvestigateFinished(investId)
                    if isFinished then
                        if not hasUnread then
                            for _, collId in pairs(cfg.collectionIdList) do
                                if prtsSys:IsPrtsUnread(collId) then
                                    hasUnread = true
                                    break ;
                                end
                            end
                            if not hasUnread and RedDotUtils.hasPrtsNoteRedDot(cfg) then
                                hasUnread = true
                            end
                        end
                    else
                        local unlockCount = 0
                        for _, collId in pairs(cfg.collectionIdList) do
                            unlockCount = prtsSys:IsPrtsUnlocked(collId) and unlockCount + 1 or unlockCount
                            if prtsSys:IsPrtsUnread(collId) then
                                hasUnread = true
                            end
                        end

                        if unlockCount >= cfg.collectionIdList.Count then
                            return true, UIConst.RED_DOT_TYPE.Normal
                        end

                        if not hasUnread and RedDotUtils.hasPrtsNoteRedDot(cfg) then
                            hasUnread = true
                        end
                    end
                end
            end

            if hasUnread then
                return true, UIConst.RED_DOT_TYPE.New
            end
            return false
        end,
        sons = {
            PRTSInvestigate = false,
        },
    },
    PRTSInvestigate = {
        msgs = {
            MessageConst.ON_INVESTIGATE_FINISHED,
            MessageConst.ON_READ_PRTS_NOTE_BATCH,
            MessageConst.ON_READ_PRTS,
        },
        readLike = true,
        needArg = true,
        Check = function(investId)
            local cfg = Utils.tryGetTableCfg(Tables.prtsInvestigate, investId)
            if not cfg then
                return false
            end

            local prtsSys = GameInstance.player.prts
            local isFinished = prtsSys:IsInvestigateFinished(investId)
            if isFinished then
                for _, collId in pairs(cfg.collectionIdList) do
                    if prtsSys:IsPrtsUnread(collId) then
                        return true, UIConst.RED_DOT_TYPE.New
                    end
                end
                if RedDotUtils.hasPrtsNoteRedDot(cfg) then
                    return true, UIConst.RED_DOT_TYPE.New
                end
            else
                local unlockCount = 0
                local hasUnread = false
                for _, collId in pairs(cfg.collectionIdList) do
                    unlockCount = prtsSys:IsPrtsUnlocked(collId) and unlockCount + 1 or unlockCount
                    if prtsSys:IsPrtsUnread(collId) then
                        hasUnread = true
                    end
                end

                if unlockCount >= cfg.collectionIdList.Count then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end

                if hasUnread then
                    return true, UIConst.RED_DOT_TYPE.New
                end

                if RedDotUtils.hasPrtsNoteRedDot(cfg) then
                    return true, UIConst.RED_DOT_TYPE.New
                end
            end
            return false
        end,
        sons = {
            PRTSItem = false,
        },
    },
    PRTSNote = {
        msgs = {
            MessageConst.ON_READ_PRTS_NOTE_BATCH,
            MessageConst.ON_UNREAD_PRTS_NOTE_BATCH,
        },
        readLike = true,
        needArg = true,
        Check = function(noteId)
            local prtsSys = GameInstance.player.prts
            if prtsSys:IsNoteUnread(noteId) then
                return true, UIConst.RED_DOT_TYPE.New
            end
            return false
        end,
    },


    AllCharInfo = {
        msgs = {
            MessageConst.SHOW_FOCUS_MODE_TOAST,
            MessageConst.GAME_MODE_ENABLE,
            MessageConst.ENTER_FOCUS_MODE_READY,
        },
        readLike = false,
        needArg = false,
        Check = function()
            local isFullLockedTeam = CharInfoUtils.IsFullLockedTeam()
            if isFullLockedTeam then
                return false
            end
            if not Utils.isInMainScope() then
                return false
            end

            local charBag = GameInstance.player.charBag
            if charBag.newCharListSet.Count > 0 then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            for charInstId, charInfo in pairs(charBag.charInfos) do
                if charInfo.charType == GEnums.CharType.Default and
                    RedDotManager:GetRedDotState("CharInfoPotential", charInstId) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end,
        sons = {
            CharNew = false,
            CharInfoPotential = false,
        },
    },
    CharInfo = {
        msgs = {
            MessageConst.ON_SYSTEM_UNLOCK_CHANGED,
        },
        readLike = false,
        needArg = true,
        Check = function(charInstId)
            if not CharInfoUtils.isCharDevAvailable(charInstId) then
                return false
            end

            local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)

            if RedDotManager:GetRedDotState("CharNew", charInst.templateId) then
                return true, UIConst.RED_DOT_TYPE.New
            end
            if RedDotManager:GetRedDotState("CharInfoPotential", charInstId) then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end,
        sons = {
            CharNew = false,
            CharInfoPotential = false,
        },
    },
    CharNew = {
        msgs = {
            MessageConst.ON_CHAR_NEW_TAG_CHANGED,
        },
        readLike = true,
        needArg = true,
        Check = function(templateId)
            local res = GameInstance.player.charBag:CheckCharIsNew(templateId)
            return res, UIConst.RED_DOT_TYPE.New
        end,
    },
    CharBreak = {
        readLike = false,
        needArg = true,
        Check = function(charInstId)
            if not CharInfoUtils.isCharDevAvailable(charInstId) then
                return false
            end

            local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)


            for nodeId, nodeData in pairs(Tables.charBreakNodeTable) do
                if nodeData.talentNodeType == GEnums.TalentNodeType.CharBreak then
                    if nodeData.breakStage == charInst.breakStage + 1 and
                        nodeData.equipTierLimit <= charInst.equipTierLimit then
                        local _, breakStageData = Tables.charBreakStageTable:TryGetValue(nodeData.breakStage)
                        if breakStageData and breakStageData.minCharLevel <= charInst.level and
                            CharInfoUtils.isCharBreakCostEnough(charInst.templateId, nodeId) then
                            return true, UIConst.RED_DOT_TYPE.Normal
                        end
                    end
                elseif nodeData.talentNodeType == GEnums.TalentNodeType.EquipBreak then
                    if nodeData.breakStage == charInst.breakStage and
                        nodeId ~= charInst.talentInfo.latestBreakNode and
                        CharInfoUtils.isCharBreakCostEnough(charInst.templateId, nodeId) then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end


            local talentState = RedDotManager:GetRedDotState("CharTalent", charInstId)
            if talentState then
                return talentState, UIConst.RED_DOT_TYPE.Normal
            end


            local skillState = RedDotManager:GetRedDotState("CharSkill", charInstId)
            if skillState then
                return skillState, UIConst.RED_DOT_TYPE.Normal
            end

            return false
        end,
        sons = {
            CharBreakNode = false,
            EquipBreakNode = false,
            CharAttrNode = false,
            PassiveSkillNode = false,
            ShipSkillNode = false,
            CharSkillNode = false,
        },
    },
    CharTalent = {
        readLike = false,
        needArg = true,
        Check = function(charInstId)
            if not CharInfoUtils.isCharDevAvailable(charInstId) then
                return false
            end

            local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
            local charGrowthData = CharInfoUtils.getCharGrowthData(charInst.templateId)
            if charGrowthData then

                local passiveSkillNodeTable = {}

                local shipSkillNodeIndexTable = {}
                for nodeId, nodeData in pairs(charGrowthData.talentNodeMap) do
                    if nodeData.nodeType == GEnums.TalentNodeType.Attr then
                        if not charInst.talentInfo.attributeNodes:Contains(nodeId) and
                            CSPlayerDataUtil.GetCharFriendship(charInstId) >= nodeData.attributeNodeInfo.favorability and
                            charInst.breakStage >= nodeData.attributeNodeInfo.breakStage and
                            CharInfoUtils.isCharTalentCostEnough(charInst.templateId, nodeId) then
                            return true, UIConst.RED_DOT_TYPE.Normal
                        end
                    elseif nodeData.nodeType == GEnums.TalentNodeType.PassiveSkill then
                        local index = nodeData.passiveSkillNodeInfo.index
                        local data = passiveSkillNodeTable[index]
                        if not data then
                            data = {}
                            passiveSkillNodeTable[index] = data
                        end
                        data[nodeData.passiveSkillNodeInfo.level] = nodeData
                    elseif nodeData.nodeType == GEnums.TalentNodeType.FactorySkill then
                        local index = nodeData.factorySkillNodeInfo.index
                        local data = shipSkillNodeIndexTable[index]
                        if not data then
                            data = {}
                            shipSkillNodeIndexTable[index] = data
                        end
                        data[nodeData.factorySkillNodeInfo.level] = nodeData
                    end
                end
                for _, nodeDataTable in pairs(passiveSkillNodeTable) do
                    local maxLv = #nodeDataTable
                    for i = maxLv, 1, -1 do
                        local nodeData = nodeDataTable[i]
                        if charInst.talentInfo.latestPassiveSkillNodes:Contains(nodeData.nodeId) then
                            break
                        end
                        local preNodeData = nodeDataTable[i - 1]
                        if charInst.breakStage >= nodeData.passiveSkillNodeInfo.breakStage and
                            (preNodeData == nil or charInst.talentInfo.latestPassiveSkillNodes:Contains(preNodeData.nodeId)) and
                            CharInfoUtils.isCharTalentCostEnough(charInst.templateId, nodeData.nodeId) then
                            return true, UIConst.RED_DOT_TYPE.Normal
                        end
                    end
                end
                for _, nodeDataTable in pairs(shipSkillNodeIndexTable) do
                    local maxLv = #nodeDataTable
                    for i = maxLv, 1, -1 do
                        local nodeData = nodeDataTable[i]
                        if charInst.talentInfo.latestFactorySkillNodes:Contains(nodeData.nodeId) then
                            break
                        end
                        local preNodeData = nodeDataTable[i - 1]
                        if charInst.breakStage >= nodeData.factorySkillNodeInfo.breakStage and
                            (preNodeData == nil or charInst.talentInfo.latestFactorySkillNodes:Contains(preNodeData.nodeId)) and
                            CharInfoUtils.isCharTalentCostEnough(charInst.templateId, nodeData.nodeId) then
                            return true, UIConst.RED_DOT_TYPE.Normal
                        end
                    end
                end
            end
            return false
        end,
        sons = {
            CharAttrNode = false,
            PassiveSkillNode = false,
            ShipSkillNode = false,
        },
    },
    CharSkill = {
        readLike = false,
        needArg = true,
        Check = function(charInstId)
            if not CharInfoUtils.isCharDevAvailable(charInstId) then
                return false
            end

            local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
            for _, skillGroupLevelInfo in pairs(charInst.skillGroupLevelInfoList) do
                if skillGroupLevelInfo.level < skillGroupLevelInfo.maxLevel then
                    if CharInfoUtils.isSkillGroupLevelUpCostEnough(charInst.templateId, skillGroupLevelInfo.skillGroupId, skillGroupLevelInfo.level + 1) then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end
            return false
        end,
        sons = {
            CharSkillNode = false,
        },
    },
    CharBreakNode = {
        msgs = {
            MessageConst.ON_ITEM_COUNT_CHANGED,
            MessageConst.ON_WALLET_CHANGED,
            MessageConst.ON_CHAR_LEVEL_UP,
            MessageConst.ON_CHAR_TALENT_UPGRADE,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local charInstId, nodeId = unpack(args)
            if not CharInfoUtils.isCharDevAvailable(charInstId) then
                return false
            end
            local isActive, isLock = CharInfoUtils.getCharBreakNodeStatus(charInstId, nodeId)
            if isActive or isLock then
                return false
            end
            local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
            if charInst.charType == GEnums.CharType.Trial then
                return false
            end
            if not CharInfoUtils.isCharBreakCostEnough(charInst.templateId, nodeId) then
                return false
            end
            return true, UIConst.RED_DOT_TYPE.Normal
        end
    },
    EquipBreakNode = {
        msgs = {
            MessageConst.ON_ITEM_COUNT_CHANGED,
            MessageConst.ON_WALLET_CHANGED,
            MessageConst.ON_CHAR_LEVEL_UP,
            MessageConst.ON_CHAR_TALENT_UPGRADE,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local charInstId, nodeId = unpack(args)
            if not CharInfoUtils.isCharDevAvailable(charInstId) then
                return false
            end
            local isActive, isLock = CharInfoUtils.getEquipBreakNodeStatus(charInstId, nodeId)
            if isActive or isLock then
                return false
            end
            local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
            if charInst.charType == GEnums.CharType.Trial then
                return false
            end
            if not CharInfoUtils.isCharBreakCostEnough(charInst.templateId, nodeId) then
                return false
            end
            return true, UIConst.RED_DOT_TYPE.Normal
        end
    },
    CharAttrNode = {
        msgs = {
            MessageConst.ON_ITEM_COUNT_CHANGED,
            MessageConst.ON_WALLET_CHANGED,
            MessageConst.ON_CHAR_TALENT_UPGRADE,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local charInstId, nodeId = unpack(args)
            if not CharInfoUtils.isCharDevAvailable(charInstId) then
                return false
            end
            local isActive, isLock = CharInfoUtils.getAttributeNodeStatus(charInstId, nodeId)
            if isActive or isLock then
                return false
            end
            local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
            if charInst.charType == GEnums.CharType.Trial then
                return false
            end
            if not CharInfoUtils.isCharTalentCostEnough(charInst.templateId, nodeId) then
                return false
            end
            return true, UIConst.RED_DOT_TYPE.Normal
        end
    },
    PassiveSkillNode = {
        msgs = {
            MessageConst.ON_ITEM_COUNT_CHANGED,
            MessageConst.ON_WALLET_CHANGED,
            MessageConst.ON_CHAR_TALENT_UPGRADE,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local charInstId, nodeId = unpack(args)
            if not CharInfoUtils.isCharDevAvailable(charInstId) then
                return false
            end
            local isActive, isLock = CharInfoUtils.getPassiveSkillNodeStatus(charInstId, nodeId)
            if isActive or isLock then
                return false
            end
            local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
            if charInst.charType == GEnums.CharType.Trial then
                return false
            end
            if not CharInfoUtils.isCharTalentCostEnough(charInst.templateId, nodeId) then
                return false
            end
            return true, UIConst.RED_DOT_TYPE.Normal
        end
    },
    ShipSkillNode = {
        msgs = {
            MessageConst.ON_ITEM_COUNT_CHANGED,
            MessageConst.ON_WALLET_CHANGED,
            MessageConst.ON_CHAR_TALENT_UPGRADE,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local charInstId, nodeId = unpack(args)
            if not CharInfoUtils.isCharDevAvailable(charInstId) then
                return false
            end
            local isActive, isLock = CharInfoUtils.getShipSkillNodeStatus(charInstId, nodeId)
            if isActive or isLock then
                return false
            end
            local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
            if charInst.charType == GEnums.CharType.Trial then
                return false
            end
            if not CharInfoUtils.isCharTalentCostEnough(charInst.templateId, nodeId) then
                return false
            end
            return true, UIConst.RED_DOT_TYPE.Normal
        end
    },
    CharSkillNode = {
        msgs = {
            MessageConst.ON_ITEM_COUNT_CHANGED,
            MessageConst.ON_WALLET_CHANGED,
            MessageConst.ON_SKILL_UPGRADE_SUCCESS,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local charInstId, skillGroupId = unpack(args)
            if not CharInfoUtils.isCharDevAvailable(charInstId) then
                return false
            end

            local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)


            local skillGroupLevelInfo = CharInfoUtils.getCharSkillLevelInfo(charInst, skillGroupId)
            if skillGroupLevelInfo.level < skillGroupLevelInfo.maxLevel then
                if CharInfoUtils.isSkillGroupLevelUpCostEnough(charInst.templateId, skillGroupId, skillGroupLevelInfo.level + 1) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end

            return false
        end
    },
    EquipTab = {
        readLike = false,
        needArg = true,
        Check = function(charInstId)
            if not CharInfoUtils.isCharDevAvailable(charInstId) then
                return false
            end
            for slotIndex, _ in pairs(UIConst.EQUIP_PART_TYPE_2_CELL_CONFIG) do
                local groupState = RedDotManager:GetRedDotState("Equip", { charInstId, slotIndex })
                if groupState then
                    return groupState, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end,
        sons = {
            Equip = false,
        },
    },
    Equip = {
        msgs = {
            MessageConst.ON_EQUIP_DEPOT_CHANGED,
            MessageConst.ON_PUT_ON_EQUIP,
            MessageConst.ON_PUT_OFF_EQUIP,
            MessageConst.ON_ITEM_COUNT_CHANGED,
            MessageConst.ON_TACTICAL_ITEM_CHANGE,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local charInstId, equipSlotIndex = unpack(args)
            if not CharInfoUtils.isCharDevAvailable(charInstId) then
                return false
            end
            local equipCellCfg = UIConst.EQUIP_PART_TYPE_2_CELL_CONFIG[equipSlotIndex]
            if not equipCellCfg then
                return false
            end

            local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)

            if equipCellCfg.isTacticalItem then
                if not string.isEmpty(charInst.tacticalItemId) then
                    return false
                end
                for _, itemEquipData in pairs(Tables.equipItemTable) do
                    if GameInstance.player.inventory:IsItemFound(itemEquipData.itemId) and
                        Utils.getBagItemCount(itemEquipData.itemId) > 0 then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            else
                local isEquipped, equipInstId = charInst.equipCol:TryGetValue(equipCellCfg.equipIndex)
                if isEquipped and equipInstId > 0 then
                    return false
                end
                local equipDepot = GameInstance.player.inventory.valuableDepots[GEnums.ItemValuableDepotType.Equip]:GetOrFallback(Utils.getCurrentScope())
                if not equipDepot then
                    return false
                end
                for _, itemBundle in pairs(equipDepot.instItems) do
                    if itemBundle.instData.equippedCharServerId == 0 then
                        local templateId = itemBundle.instData.templateId
                        local _, equipCfg = Tables.equipTable:TryGetValue(templateId)
                        if equipCfg and equipCfg.partType == equipCellCfg.slotPartType then
                            local _, itemData = Tables.itemTable:TryGetValue(templateId)
                            if itemData and itemData.rarity <= charInst.equipTierLimit then
                                return true, UIConst.RED_DOT_TYPE.Normal
                            end
                        end
                    end
                end
            end
            return false
        end,
    },
    WatchBtn = {
        readLike = false,
        needArg = false,
        sons = {
        },
    },
    WatchBtnList = {
        readLike = false,
        needArg = true,
        Check = function(redDots)
            for i = 1, #redDots do
                if RedDotManager:GetRedDotState(redDots[i]) then
                    return true
                end
            end
            return false
        end,
    },

    Questionnaire = {
        msgs = {
            MessageConst.ON_SURVEY_STATE_CHANGE,
        },
        readLike = false,
        needArg = false,
        Check = function()
            return CS.Beyond.SDK.SDKAccountUtils.surveyRedDot
        end,
    },
    InventoryBtn = {
        readLike = false,
        needArg = false,
        sons = {

            ManualCraftBtn = true,
        },
    },

    ShopSeeGoodsInfo = {
        msgs = {
            MessageConst.ON_SHOP_GOODS_SEE_GOODS_INFO_CHANGE,
        },
        readLike = false,
        needArg = true,
        Check = function(info)
            return GameInstance.player.shopSystem:IsNewGoodsId(info.goodsId), UIConst.RED_DOT_TYPE.New
        end,
    },

    CommonShopSeeGoodsInfo = {
        msgs = {
            MessageConst.ON_SHOP_GOODS_SEE_GOODS_INFO_CHANGE,
            MessageConst.ON_SHOP_GOODS_LOCK_CHANGE,
        },
        readLike = false,
        needArg = true,
        Check = function(info)
            local shopSystem = GameInstance.player.shopSystem
            if not shopSystem:CheckGoodsUnlocked(info.goodsId) then
                return false
            end
            return shopSystem:IsNewGoodsId(info.goodsId), UIConst.RED_DOT_TYPE.New
        end,
    },

    ManualCraftBtn = {
        readLike = false,
        needArg = false,
        Check = function()
            if Utils.isInBlackbox() or Utils.isInWeekRaid() then
                return false
            end
            local isUnlocked = Utils.isSystemUnlocked(GEnums.UnlockSystemType.ProductManual)
            if isUnlocked and GameInstance.player.facManualCraft:CheckRewardRedDot() then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end,
        sons = {
            ManualCraftRewardEntry = true,
        },
    },
    ManualCraftType = {
        msgs = {
            MessageConst.ON_CRAFT_SWITCH_STAMINA,
        },
        readLike = false,
        needArg = true,
        Check = function(formulaType)
            local blackboxData = GameWorld.worldInfo.curLevel.levelData.blackbox
            if blackboxData then
                return false, UIConst.RED_DOT_TYPE.Normal
            end
            if GameInstance.player.facManualCraft:ExistUnreadFormulaByType(formulaType) then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end,
        sons = {
            ManualCraftItem = false,
        },
    },
    ManualCraftConvert = {
        msgs = {
            MessageConst.ON_UNREAD_MANUAL_CRAFT,
            MessageConst.ON_READ_MANUAL_CRAFT,
        },
        readLike = false,
        needArg = false,
        Check = function()
            local blackboxData = GameWorld.worldInfo.curLevel.levelData.blackbox
            if blackboxData then
                return false
            end
            local useStamina = GameInstance.player.facManualCraft.useStamina
            if GameInstance.player.facManualCraft:ExistUnreadFormulaByTypeFilteredByStamina(
                CS.Beyond.GEnums.CraftShowingType.ManualCraftConvert, not useStamina) then
                return true, UIConst.RED_DOT_TYPE.New
            end
            return false
        end,
    },
    ManualCraftItem = {
        msgs = {
            MessageConst.ON_UNREAD_MANUAL_CRAFT,
            MessageConst.ON_READ_MANUAL_CRAFT,
        },
        readLike = false,
        needArg = true,
        Check = function(formulaId)
            local blackboxData = GameWorld.worldInfo.curLevel.levelData.blackbox
            if blackboxData then
                return false, UIConst.RED_DOT_TYPE.Normal
            end
            if not GameInstance.player.facManualCraft:IsCraftRead(formulaId) then
                return true, UIConst.RED_DOT_TYPE.New
            end
            return false
        end,
    },

    ManualCraftRewardItem = {
        msgs = {
            MessageConst.ON_UNREAD_MANUAL_CRAFT_REWARD,
            MessageConst.ON_READ_MANUAL_CRAFT_REWARD,
        },
        readLike = false,
        needArg = true,
        Check = function(arg)
            local blackboxData = GameWorld.worldInfo.curLevel.levelData.blackbox
            if blackboxData then
                return false, UIConst.RED_DOT_TYPE.Normal
            end
            if arg.rewardId then
                if GameInstance.player.facManualCraft:CheckHaveReadReward(arg.rewardId) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
                return false
            else
                if GameInstance.player.facManualCraft:CheckHaveReadRewardByItem(arg.itemId) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
                return false
            end
        end,
    },

    ManualCraftReward = {
        readLike = false,
        needArg = true,
        penetrateLevel = UIConst.RED_DOT_TYPE.Normal,
        Check = function(arg)
            local blackboxData = GameWorld.worldInfo.curLevel.levelData.blackbox
            if blackboxData then
                return false, UIConst.RED_DOT_TYPE.Normal
            end
            if GameInstance.player.facManualCraft:CheckHaveRewardByItemNoGet(arg.itemId) > 0 then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end,
        sons = {
            ManualCraftRewardItem = false,
        },
    },
    ManualCraftRewardEntry = {
        msgs = {
            MessageConst.ON_SYSTEM_UNLOCK_CHANGED,
        },
        readLike = false,
        needArg = false,
        penetrateLevel = UIConst.RED_DOT_TYPE.Normal,
        Check = function()
            if Utils.isInBlackbox() or Utils.isInWeekRaid() then
                return false
            end
            if Utils.isSystemUnlocked(GEnums.UnlockSystemType.ProductManual) and GameInstance.player.facManualCraft:CheckRewardRedDot() then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end,
        sons = {
            ManualCraftReward = false,
        },
    },

    Announcement = {
        msgs = {
            MessageConst.ON_ANNOUNCEMENT_RED_DOT_CHANGED,
        },
        readLike = false,
        needArg = false,
        Check = function()
            return GameInstance.player.announcement:HasNewAnnouncement()
        end,
    },

    FacBuildModeMenuItem = {
        readLike = true,
        needArg = true,
        Check = function(id)
            local _, hasSaved = ClientDataManagerInst:GetBool(FacConst.FAC_BUILD_LIST_REDDOT_DATA_CATEGORY .. id, false, false, FacConst.FAC_BUILD_LIST_REDDOT_DATA_CATEGORY)
            if not hasSaved then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false, UIConst.RED_DOT_TYPE.Normal
        end,
    },
    FacBuildMode = {
        sons = {
            FacBuildModeIndustry = true,
            FacBuildModeDecorate = true,
        },
        readLike = false,
        needArg = false,
    },
    FacBuildModeIndustry = {
        sons = {
            FacBuildModeMenuLogisticTab = true
        },
        readLike = false,
        needArg = false,
    },
    FacBuildModeMenuLogisticTab = {
        msgs = {
            MessageConst.ON_SYSTEM_UNLOCK_CHANGED,
            MessageConst.ON_IN_FAC_MAIN_REGION_CHANGE,
        },
        sons = {
            FacBuildModeMenuItem = false,
        },
        readLike = false,
        needArg = false,
        Check = function()
            if not Utils.isInFacMainRegion() then
                return false
            end
            for id, _ in pairs(Tables.factoryGridConnecterTable) do
                if Utils.isSystemUnlocked(FacConst.LOGISTIC_UNLOCK_SYSTEM_MAP[id]) then
                    if RedDotManager:GetRedDotState("FacBuildModeMenuItem", id) then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end
            for id, _ in pairs(Tables.factoryGridRouterTable) do
                if Utils.isSystemUnlocked(FacConst.LOGISTIC_UNLOCK_SYSTEM_MAP[id]) then
                    if RedDotManager:GetRedDotState("FacBuildModeMenuItem", id) then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end
            return false
        end,
    },
    FacBuildModeDecorate = {
        sons = {
            DecoBuilding = false
        },
        readLike = false,
        needArg = false,
        Check = function()
            if Utils.isInBlackbox() then
                return false
            end
            for id, _ in pairs(Tables.FactoryDecoBuildingTable) do
                if FactoryUtils.isDecoBuildingVisible(id) then
                    local hasRedDot = RedDotManager:GetRedDotState("DecoBuilding", id)
                    if hasRedDot then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end
            return false
        end,
    },
    DecoBuildingAllObtainWay = {
        sons = {
            DecoBuildingObtainWay = false
        },
        needArg = true,
        readLike = false,
        Check = function(id)
            if Utils.isInBlackbox() then
                return false
            end
            local success, decoInfo = Tables.FactoryDecoBuildingTable:TryGetValue(id)
            if success and #decoInfo.obtainIds > 0 then
                for i = 1, #decoInfo.obtainIds do
                    local obtainRedDot, _ = RedDotManager:GetRedDotState("DecoBuildingObtainWay", {itemId = id, obtainId = decoInfo.obtainIds[CSIndex(i)]})
                    if obtainRedDot then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end
            return false
        end
    },
    DecoBuildingObtainWay = {
        Check = function(args)
            if Utils.isInBlackbox() then
                return false
            end
            local success, decoInfo = Tables.FactoryDecoBuildingTable:TryGetValue(args.itemId)
            if success then
                local obtainId = args.obtainId
                local successObtainId, obtainInfo = Tables.DecoBuildingObtainWaysTable:TryGetValue(obtainId)
                if successObtainId and obtainInfo.showGetReward then
                    if not GameInstance.player.remoteFactory.core:CheckDecorateObtained(obtainId) then
                        for i = 1, #obtainInfo.conditionIds do
                            local conditionId = obtainInfo.conditionIds[CSIndex(i)]
                            local conditionInfo = Tables.DecoBuildingObtainConditionTable[conditionId]
                            local _, value = LuaGameConditionUtils.getConditionValueByParameters(
                                    conditionInfo.conditionType,
                                    conditionInfo.parameters)
                            if value > 0 then
                                return true, UIConst.RED_DOT_TYPE.Normal
                            end
                        end
                    else
                        return false
                    end
                end
            end
            return false
        end,
        needArg = true,
        readLike = false,
    },
    DecoBuilding = {
        Check = function(id)
            if Utils.isInBlackbox() then
                return false
            end
            local remoteFactoryCore = GameInstance.player.remoteFactory.core
            local buildingId = Tables.factoryDecoBuildingTable[id].buildingId
            local result, gotCnt, placedCnt = remoteFactoryCore:GetDecoBuildingCount(buildingId)
            local totalNum = gotCnt + placedCnt
            if totalNum > 0 then
                if RedDotManager:GetRedDotState("DecoBuildingAllObtainWay", id) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
                return not RedDotUtils.checkDecoBuildingFirstGetIsRead(id), UIConst.RED_DOT_TYPE.Normal
            else
                if FactoryUtils.isDecoBuildingVisible(id) then
                    return not RedDotUtils.checkDecoBuildingNewIsRead(id), UIConst.RED_DOT_TYPE.New
                end
            end
            return false
        end,
        needArg = true,
        readLike = false,
        msgs = {
            MessageConst.ON_FAC_DECO_BUILDING_RED_DOT_REFRESH,
            MessageConst.ON_QUEST_STATE_CHANGE,
        },
    },
    SNSNormalDialogSubCell = {
        msgs = {
            MessageConst.ON_READ_SNS_DIALOG,
        },
        needArg = true,
        readLike = true,
        Check = function(dialogId)
            local dialogHasRead = GameInstance.player.sns:DialogHasRead(dialogId)
            if not dialogHasRead then
                return true, UIConst.RED_DOT_TYPE.Normal
            end

            return false
        end,
    },
    SNSMissionDialogCell = {
        msgs = {
            MessageConst.ON_READ_SNS_DIALOG,
        },
        needArg = true,
        readLike = true,
        Check = function(dialogId)
            local dialogHasRead = GameInstance.player.sns:DialogHasRead(dialogId)
            if not dialogHasRead then
                return true, UIConst.RED_DOT_TYPE.Normal
            end

            return false
        end,
    },
    SNSContactNpcCellTopic = {
        msgs = {
            MessageConst.ON_READ_SNS_DIALOG,
        },
        needArg = true,
        readLike = true,
        Check = function(chatId)
            local showingTopicDialogInfos = SNSUtils.getShowingTopicDialogInfos(chatId)
            for _, info in ipairs(showingTopicDialogInfos) do
                local topicDialogId = info.dialogId
                local dialogHasRead = GameInstance.player.sns:DialogHasRead(topicDialogId)
                if not dialogHasRead then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end

            return false
        end,
    },
    SNSContactNpcCell = {
        sons = {
            SNSNormalDialogSubCell = false,
        },
        needArg = true,
        readLike = true,
        Check = function(chatId)

            local succ, chatInfo = GameInstance.player.sns.chatInfoDic:TryGetValue(chatId)
            if not succ then
                return false
            end

            for _, dialogId in pairs(chatInfo.dialogIds) do
                local dialogCfg = Tables.sNSDialogTable[dialogId]
                if string.isEmpty(dialogCfg.topicId) then
                    local redDotState = RedDotManager:GetRedDotState("SNSNormalDialogSubCell", dialogId)
                    if redDotState then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end

            return false
        end,
    },
    SNSBarkerTabCell = {
        sons = {
            SNSContactNpcCellTopic = false,
            SNSContactNpcCell = false,
        },
        msgs = {
            MessageConst.ON_SNS_BARKER_TAB_READ_STATE_CHANGE,
        },
        needArg = false,
        readLike = true,
        Check = function()
            local succ, read = ClientDataManagerInst:GetBool(SNSUtils.NORMAL_TAB_READ, false, false, SNSUtils.SNS_CATEGORY)
            if succ and read then
                return false
            end

            local chatInfoDic = GameInstance.player.sns.chatInfoDic
            for chatId, chatInfo in pairs(chatInfoDic) do
                local topicRedDotState = RedDotManager:GetRedDotState("SNSContactNpcCellTopic", chatId)
                if topicRedDotState then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end

                local hasRedDot = RedDotManager:GetRedDotState("SNSContactNpcCell", chatId)
                if hasRedDot then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end

            return false
        end
    },
    SNSMissionTabCell = {
        sons = {
            SNSMissionDialogCell = false,
        },
        msgs = {
            MessageConst.ON_SNS_MISSION_TAB_READ_STATE_CHANGE,
        },
        needArg = false,
        readLike = true,
        Check = function()
            local succ, read = ClientDataManagerInst:GetBool(SNSUtils.MISSION_TAB_READ, false, false, SNSUtils.SNS_CATEGORY)
            if succ and read then
                return false
            end

            local missionRelatedSNSDialogIds = GameInstance.player.sns.missionRelatedSNSDialogIds
            for _, dialogId in pairs(missionRelatedSNSDialogIds) do
                local hasRedDot = RedDotManager:GetRedDotState("SNSMissionDialogCell", dialogId)
                if hasRedDot then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end
    },
    SNSHudEntry = {
        sons = {
            SNSBarkerTabCell = false,
            SNSMissionTabCell = false,
            FriendChatUnRead = false,
        },
        needArg = false,
        readLike = false,
        Check = function()
            local hasRedDot = RedDotManager:GetRedDotState("FriendChatUnRead")
            if hasRedDot then
                return true, UIConst.RED_DOT_TYPE.Normal
            end

            hasRedDot = RedDotManager:GetRedDotState("SNSMissionTabCell")
            if hasRedDot then
                return true, UIConst.RED_DOT_TYPE.Normal
            end

            hasRedDot = RedDotManager:GetRedDotState("SNSBarkerTabCell")
            if hasRedDot then
                return true, UIConst.RED_DOT_TYPE.Normal
            end

            return false
        end
    },
    SNSWatchEntry = {
        sons = {
            SNSBarkerTabCell = false,
            SNSMissionTabCell = false,
            FriendChatUnRead = false,
        },
        msgs = {
            MessageConst.ON_SNS_NORMAL_DIALOG_ADD,
            MessageConst.RECV_FRIEND_SEND_CHAT_NOTIFY,
        },
        needArg = false,
        readLike = false,
        Check = function()
            local hasRedDot = RedDotManager:GetRedDotState("FriendChatUnRead")
            if hasRedDot then
                return true, UIConst.RED_DOT_TYPE.Normal
            end

            hasRedDot = RedDotManager:GetRedDotState("SNSMissionTabCell")
            if hasRedDot then
                return true, UIConst.RED_DOT_TYPE.Normal
            end

            hasRedDot = RedDotManager:GetRedDotState("SNSBarkerTabCell")
            if hasRedDot then
                return true, UIConst.RED_DOT_TYPE.Normal
            end

            return false
        end
    },

    CharInfoPotential = {
        msgs = {
            MessageConst.ON_ITEM_COUNT_CHANGED,
        },
        readLike = false,
        needArg = true,
        Check = function(charInstId)
            if not CharInfoUtils.isCharDevAvailable(charInstId) then
                return false
            end

            local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
            local hasValue

            local characterPotentialList
            hasValue, characterPotentialList = Tables.characterPotentialTable:TryGetValue(charInfo.templateId)
            if not hasValue or charInfo.potentialLevel >= #characterPotentialList.potentialUnlockBundle then
                return false
            end
            local potentialData = characterPotentialList.potentialUnlockBundle[charInfo.potentialLevel]
            local itemId = potentialData.itemIds[0]
            local itemCount = Utils.getItemCount(itemId)
            local needCount = potentialData.itemCnts[0]
            if itemCount >= needCount then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end
    },

    CharInfoPotentialSkill = {
        msgs = {
            MessageConst.ON_ITEM_COUNT_CHANGED,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local charInstId = args.charInstId
            if not CharInfoUtils.isCharDevAvailable(charInstId) then
                return false
            end
            local potentialLevel = args.potentialLevel

            local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
            if not charInfo or charInfo.charType == GEnums.CharType.Trial or potentialLevel ~= charInfo.potentialLevel + 1 then
                return false
            end
            local _, characterPotentialList = Tables.characterPotentialTable:TryGetValue(charInfo.templateId)
            if not characterPotentialList or potentialLevel > #characterPotentialList.potentialUnlockBundle then
                return false
            end
            local potentialData = characterPotentialList.potentialUnlockBundle[CSIndex(potentialLevel)]
            local itemId = potentialData.itemIds[0]
            local itemCount = Utils.getItemCount(itemId)
            local needCount = potentialData.itemCnts[0]
            if itemCount >= needCount then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end
    },

    CharInfoPotentialPicture = {
        msgs = {
            MessageConst.ON_CHAR_POTENTIAL_PICTURE_READ,
            MessageConst.ON_CHAR_POTENTIAL_PICTURE_UNREAD,
        },
        readLike = true,
        needArg = true,
        Check = function(args)
            local charInstId = args.charInstId
            if not CharInfoUtils.isCharDevAvailable(charInstId) then
                return false
            end
            local potentialLevel = args.potentialLevel

            local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
            if not charInfo or charInfo.charType == GEnums.CharType.Trial or potentialLevel > charInfo.potentialLevel then
                return false
            end
            local _, characterPotentialList = Tables.characterPotentialTable:TryGetValue(charInfo.templateId)
            if not characterPotentialList or potentialLevel > #characterPotentialList.potentialUnlockBundle then
                return false
            end
            local pictureId = args.pictureId
            if string.isEmpty(pictureId) then
                local potentialData = characterPotentialList.potentialUnlockBundle[CSIndex(potentialLevel)]
                for _, itemId in pairs(potentialData.unlockCharPictureItemList) do
                    local _, pictureId = Tables.pictureItemTable:TryGetValue(itemId)
                    if pictureId and not GameInstance.player.charBag:IsCharPotentialPictureRead(pictureId) then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            else
                if not GameInstance.player.charBag:IsCharPotentialPictureRead(pictureId) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end

            return false
        end,
    },
    ToolBox = {
        readLike = false,
        sons = {
            FacBuildMode = true,
        }
    },
    EquipTech = {
        msgs = {
            MessageConst.ON_SYSTEM_UNLOCK_CHANGED,
        },
        readLike = false,
        needArg = false,
        Check = function()
            if Utils.isInBlackbox() then
                return false
            end
            if RedDotManager:GetRedDotState("EquipProducer", nil) then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end,
        sons = {
            EquipFormula = false,
        }
    },

    EquipProducer = {
        msgs = {
            MessageConst.ON_SYSTEM_UNLOCK_CHANGED,
        },
        readLike = false,
        needArg = true,
        Check = function(isSuit)
            if Utils.isInBlackbox() then
                return false
            end

            for packId, _ in pairs(Tables.equipPackFormulaTable) do
                local _, packData = Tables.equipPackTable:TryGetValue(packId)
                if packData and (isSuit == nil or packData.isSuit == isSuit) then
                    if RedDotManager:GetRedDotState("EquipPack", packId) then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end
            return false
        end,
        sons = {
            EquipFormula = false,
        }
    },

    EquipPack = {
        readLike = false,
        needArg = true,
        Check = function(packId)
            if Utils.isInBlackbox() then
                return false
            end

            local equipTechSystem = GameInstance.player.equipTechSystem
            local _, packDataList = Tables.equipPackFormulaTable:TryGetValue(packId)
            if not packDataList then
                return false
            end
            local isPackNew = true
            local hasNew = false
            local isUnread = false
            for _, packFormulaData in pairs(packDataList.itemList) do
                local formulaId = packFormulaData.formulaId
                if EquipTechUtils.isEquipFormulaVisible(formulaId) then
                    if not isUnread and equipTechSystem:IsFormulaUnread(formulaId) then
                        isUnread = true
                    end
                    local _, formulaData = Tables.equipFormulaTable:TryGetValue(formulaId)
                    if formulaData and formulaData.isNew then
                        local isVersionNewRead = equipTechSystem:IsNewVersionFormulaRead(formulaId)
                        if not isVersionNewRead then
                            hasNew = true
                        end
                    else
                        isPackNew = false
                    end
                end
            end
            if hasNew then
                if isPackNew then
                    return true, EquipTechConst.EQUIP_PRODUCE_PACK_RED_DOT_TYPE.AllNew
                else
                    return true, EquipTechConst.EQUIP_PRODUCE_PACK_RED_DOT_TYPE.PartialNew
                end
            elseif isUnread then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end,
        sons = {
            EquipFormula = false,
        }
    },

    EquipFormula = {
        msgs = {
            MessageConst.ON_EQUIP_FORMULA_UNREAD,
            MessageConst.ON_EQUIP_FORMULA_READ,
            MessageConst.ON_NEW_VERSION_EQUIP_FORMULA_READ,
            MessageConst.ON_NEW_VERSION_EQUIP_FORMULA_UNREAD,
        },
        readLike = true,
        needArg = true,
        Check = function(formulaId)
            if Utils.isInBlackbox() then
                return false
            end
            local _, formulaData = Tables.equipFormulaTable:TryGetValue(formulaId)
            if formulaData and formulaData.isNew and not GameInstance.player.equipTechSystem:IsNewVersionFormulaRead(formulaId) then
                return true, UIConst.RED_DOT_TYPE.New
            end
            return GameInstance.player.equipTechSystem:IsFormulaUnread(formulaId), UIConst.RED_DOT_TYPE.Normal
        end
    },

    CharInfoProfile = {
        readLike = false,
        needArg = true,
        sons = {
            CharVoice = true,
            CharDoc = true,
        }
    },
    CharInfoDungeon = {
        msgs = {
            MessageConst.ON_SUB_GAME_READ,
        },
        readLike = true,
        Check = function(charTemplateId)
            local dungeonId
            local success, _ = pcall(function()
                dungeonId = Tables.CharId2DungeonIdTable[charTemplateId]
            end)
            if success then
                return RedDotManager:GetRedDotState("DungeonReadNormal", { dungeonId })
            else
                return false
            end
        end,
        needArg = true,
    },

    CharVoice = {
        readLike = false,
        needArg = true,
        Check = function(charTemplateId)
            local hasValue

            local charData
            hasValue, charData = Tables.characterTable:TryGetValue(charTemplateId)
            if hasValue then
                for _, voiceData in pairs(charData.profileVoice) do
                    if GameInstance.player.charBag:IsCharVoiceUnread(voiceData.id) then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end
            return false
        end,
        sons = {
            CharVoiceEntry = false
        }
    },

    CharVoiceEntry = {
        msgs = {
            MessageConst.ON_CHAR_VOICE_READ,
            MessageConst.ON_CHAR_VOICE_UNREAD,
            MessageConst.ON_CHAR_VOICE_LOCKED,
            MessageConst.ON_CHAR_VOICE_UNLOCKED,
        },
        readLike = true,
        needArg = true,
        Check = function(charVoiceId)
            return GameInstance.player.charBag:IsCharVoiceUnread(charVoiceId), UIConst.RED_DOT_TYPE.New
        end
    },

    CharDoc = {
        readLike = false,
        needArg = true,
        Check = function(charTemplateId)
            local hasValue

            local charData
            hasValue, charData = Tables.characterTable:TryGetValue(charTemplateId)
            if hasValue then
                for _, recordData in pairs(charData.profileRecord) do
                    if GameInstance.player.charBag:IsCharDocUnread(recordData.id) then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end
            return false
        end,
        sons = {
            CharDocEntry = false
        }
    },

    CharDocEntry = {
        msgs = {
            MessageConst.ON_CHAR_DOC_READ,
            MessageConst.ON_CHAR_DOC_UNREAD,
            MessageConst.ON_CHAR_DOC_LOCKED,
            MessageConst.ON_CHAR_DOC_UNLOCKED,
        },
        readLike = true,
        needArg = true,
        Check = function(charDocId)
            return GameInstance.player.charBag:IsCharDocUnread(charDocId), UIConst.RED_DOT_TYPE.New
        end
    },



    AdventureBook = {
        readLike = false,
        needArg = false,
        sons = {
            AdventureBookTabDaily = true,
            AdventureBookTabStage = true,
            AdventureBookTabDungeon = true,
            AdventureBookTabActivity = true,
        },
    },

    AdventureBookTabStage = {
        msgs = {
            MessageConst.ON_ADVENTURE_TASK_MODIFY,
            MessageConst.ON_ADVENTURE_BOOK_STAGE_MODIFY,
        },
        readLike = false,
        needArg = false,
        Check = AdventureBookUtils.CheckRedDotAdventureBookTabStage
    },

    AdventureBookTabStageTaskCell = {
        msgs = {
            MessageConst.ON_ADVENTURE_TASK_MODIFY,
            MessageConst.ON_ADVENTURE_BOOK_STAGE_MODIFY,
        },
        readLike = true,
        needArg = true,
        Check = function(arg)
            local hasCfg, stageTaskCfg = Tables.adventureBookStageRewardTable:TryGetValue(arg.stage)
            if not hasCfg then
                return false
            end
            local hasCompleteTask = false
            for _, taskId in pairs(stageTaskCfg.taskIds) do
                local taskData = Tables.adventureTaskTable[taskId]
                if taskData ~= nil then
                    local slotId = taskData.stageSlotId
                    if slotId == arg.slotId then
                        if GameInstance.player.adventure:IsTaskRewarded(taskId) then
                            return false
                        end
                        local taskComplete = GameInstance.player.adventure:IsTaskComplete(taskId)
                        if not taskComplete then
                            local taskValue = AdventureBookUtils.GetTaskCurrProgress(taskData)
                            local maxProgress = AdventureBookUtils.GetTaskMaxProgress(taskData)
                            taskComplete = taskValue >= maxProgress
                        end
                        if taskComplete then
                            hasCompleteTask = true
                        end
                    end
                end
            end
            return hasCompleteTask
        end
    },

    AdventureBookTabDaily = {
        msgs = {
            MessageConst.ON_ADVENTURE_TASK_MODIFY,
            MessageConst.ON_DAILY_ACTIVATION_MODIFY,
        },
        readLike = false,
        needArg = false,
        Check = AdventureBookUtils.CheckRedDotAdventureBookTabDaily
    },

    AdventureBookTabDailyTaskCell = {
        msgs = {
            MessageConst.ON_ADVENTURE_TASK_MODIFY,
            MessageConst.ON_DAILY_ACTIVATION_MODIFY,
        },
        readLike = false,
        needArg = true,
        Check = function(taskId)

            local curActivation = GameInstance.player.adventure.adventureBookData.dailyActivation
            local maxActivation = 0
            for _, cfg in pairs(Tables.dailyActivationRewardTable) do
                if cfg.activation > maxActivation then
                    maxActivation = cfg.activation
                end
            end
            if curActivation >= maxActivation then
                return false
            end

            local taskDic = GameInstance.player.adventure.adventureBookData.adventureTasks
            local csTask = taskDic:get_Item(taskId)
            return csTask.isComplete
        end
    },

    AdventureBookTabDungeon = {
        msgs = {
            MessageConst.ON_SUB_GAME_READ,
        },
        readLike = false,
        needArg = false,
        Check = AdventureBookUtils.CheckRedDotAdventureBookTabDungeon
    },

    AdventureDungeonTab = {
        msgs = {
            MessageConst.ON_SUB_GAME_READ,
        },
        readLike = false,
        needArg = true,
        Check = function(ids)
            for _, id in pairs(ids) do
                if GameInstance.player.subGameSys:IsGameUnread(id) then
                    return true, UIConst.RED_DOT_TYPE.New
                end
            end
            return false
        end
    },

    AdventureDungeonTabMonsterSpawnPoint = {
        msgs = {
            MessageConst.ON_SUB_GAME_READ,
        },
        readLike = false,
        needArg = true,
        Check = function(groupIds)
            for _, groupId in ipairs(groupIds) do
                local succ, wepGroupCfg = Tables.worldEnergyPointGroupTable:TryGetValue(groupId)
                if succ then
                    local exist = false
                    for level = 1, GameInstance.player.adventure.currentWorldLevel do
                        local subGameId = wepGroupCfg.worldLevel2GameMechanicsIdMap[level]
                        if GameInstance.player.subGameSys:IsGameUnlocked(subGameId) and
                            not GameInstance.player.subGameSys:IsGameUnread(subGameId) then
                            exist = true
                        end
                    end
                    if not exist then
                        return true, UIConst.RED_DOT_TYPE.New
                    end
                end
            end

            return false
        end
    },

    AdventureDungeonCell = {
        msgs = {
            MessageConst.ON_SUB_GAME_READ,
        },
        readLike = false,
        needArg = true,
        Check = function(ids)
            for _, id in pairs(ids) do
                if GameInstance.player.subGameSys:IsGameUnlocked(id) and
                    GameInstance.player.subGameSys:IsGameUnread(id) then
                    return true, UIConst.RED_DOT_TYPE.New
                end
            end
            return false
        end
    },


    AdventureDungeonCellMonsterSpawnPoint = {
        msgs = {
            MessageConst.ON_SUB_GAME_READ,
        },
        readLike = false,
        needArg = true,
        Check = function(groupId)
            local succ, wepGroupCfg = Tables.worldEnergyPointGroupTable:TryGetValue(groupId)
            if not succ then
                return false
            end


            local exist = false
            for level = 1, GameInstance.player.adventure.currentMaxWorldLevel do
                local subGameId = wepGroupCfg.worldLevel2GameMechanicsIdMap[level]
                if GameInstance.player.subGameSys:IsGameUnlocked(subGameId) and
                    not GameInstance.player.subGameSys:IsGameUnread(subGameId) then
                    exist = true
                end
            end

            if not exist then
                return true, UIConst.RED_DOT_TYPE.New
            else
                return false
            end
        end
    },

    AdventureBookTabTrain = {
        msgs = {
            MessageConst.ON_SUB_GAME_READ,
        },
        readLike = false,
        needArg = false,
        Check = AdventureBookUtils.CheckRedDotAdventureBookTabTrain
    },

    AdventureBookTabBlackbox = {
        msgs = {
            MessageConst.ON_SUB_GAME_ACTIVE,
            MessageConst.ON_BLACKBOX_READ,
        },
        readLike = false,
        needArg = false,
        Check = AdventureBookUtils.CheckRedDotAdventureBookTabBlackbox
    },

    AdventureBookHighDifficulty = {
        msgs = {
            MessageConst.ON_HIGH_DIFFICULTY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
        },
        readLike = false,
        needArg = true,
        Check = function()

            if GameInstance.player.highDifficultySystem:IsHighDifficultyUnlocked() then
                local seriesIds = GameInstance.player.highDifficultySystem:GetAllUnlockSeriesIds()
                for i = 1,seriesIds.Count do
                    local seriesId = seriesIds[CSIndex(i)]
                    if HighDifficultyUtils.isNewHighDifficultySeries(seriesId) then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end
            return false
        end
    },

    DungeonRead = {
        msgs = {
            MessageConst.ON_SUB_GAME_READ,
        },
        readLike = true,
        needArg = true,
        Check = function(dungeonIds)
            for _, id in pairs(dungeonIds) do
                if DungeonUtils.isDungeonUnlock(id) and
                    GameInstance.player.subGameSys:IsGameUnread(id) then
                    return true, UIConst.RED_DOT_TYPE.New
                end
            end
            return false
        end
    },


    DungeonReadNormal = {
        msgs = {
            MessageConst.ON_SUB_GAME_READ,
        },
        readLike = true,
        needArg = true,
        Check = function(dungeonIds)
            for _, id in pairs(dungeonIds) do
                if DungeonUtils.isDungeonUnlock(id) and
                    GameInstance.player.subGameSys:IsGameUnread(id) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end
    },

    AdventureBlackboxCell = {
        msgs = {
            MessageConst.ON_SUB_GAME_ACTIVE,
            MessageConst.ON_BLACKBOX_READ,
        },
        readLike = false,
        needArg = true,
        Check = function(blackboxIds)
            local dungeonMgr = GameInstance.dungeonManager

            for _, blackboxId in ipairs(blackboxIds) do
                if dungeonMgr:IsDungeonUnlocked(blackboxId) and not dungeonMgr:IsBlackboxRead(blackboxId) then
                    return true
                end
            end

            return false
        end
    },

    AdventureBookTabActivity = {
        msgs = {
            MessageConst.ON_HIGH_DIFFICULTY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_SEASON_TOWER_NEW,
            MessageConst.ON_SEASON_TOWER_ACHIEVE_UPDATE,
        },
        sons = {
            WeekRaid = false,
        },
        readLike = false,
        needArg = false,
        Check = function()
            return AdventureBookUtils.CheckRedDotAdventureBookTabActivity()
        end
    },

    AdventureBookTabWeekRaid = {
        sons = {
            WeekRaid = true,
        },
        readLike = false,
    },



    RecycleBinCanPickUp = {
        msgs = {
            MessageConst.ON_RECYCLE_BIN_CAN_PICK_UP,
        },
        readLike = false,
        needArg = false,
        Check = function()
            local pickUpList = GameInstance.player.recycleBinSystem.canPickUpInstIds
            return pickUpList.Count > 0, UIConst.RED_DOT_TYPE.Normal
        end
    },


    CheckIn = {
        readLike = false,
        needArg = false,
        Check = RedDotUtils.hasCheckInRewardsNotCollected,
        msgs = {
            MessageConst.ON_ACTIVITY_UPDATED
        }
    },


    CheckInTab = {
        readLike = false,
        needArg = true,
        Check = RedDotUtils.hasCheckInRewardsNotCollectedInRange,
        msgs = {
            MessageConst.ON_CHECK_IN_UPDATED
        },
    },

    ActivityCalendarCheckin = {
        readLike = false,
        needArg = true,
        Check = function(id)
            local _, haveGotReward, allGetReward = ActivityUtils.CalendarCheckInGetCurDayNumber(id)
            return not haveGotReward and not allGetReward
        end,
        msgs = {
            MessageConst.ON_READ_ACTIVITY_CONDITION_STAGE,
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_ACTIVITY_CALENDAR_CHECK_IN_UPDATE,
        }
    },

    MapUnreadLevel = {
        msgs = {
            MessageConst.ON_READ_LEVEL,
        },
        readLike = true,
        needArg = false,
        Check = function(levelId)
            return not GameInstance.player.mapManager:IsLevelRead(levelId), UIConst.RED_DOT_TYPE.Normal
        end
    },


    Wiki = {
        readLike = false,
        needArg = false,
        Check = function()
            if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.Wiki) then
                return false
            end
            for _, categoryData in pairs(Tables.wikiCategoryTable) do
                local isUnread = RedDotManager:GetRedDotState("WikiCategory", categoryData.categoryId)
                if isUnread then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end,
        sons = {
            WikiCategory = false,
        }
    },

    WikiCategory = {
        readLike = false,
        needArg = true,
        Check = function(categoryId)
            if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.Wiki) then
                return false
            end
            local _, wikiGroupData = Tables.wikiGroupTable:TryGetValue(categoryId)
            if not wikiGroupData then
                return false
            end
            for _, groupData in pairs(wikiGroupData.list) do
                local isUnread = RedDotManager:GetRedDotState("WikiGroup", groupData.groupId)
                if isUnread then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end,
        sons = {
            WikiGroup = false
        }
    },

    WikiGroup = {
        readLike = false,
        needArg = true,
        Check = function(groupId)
            if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.Wiki) then
                return false
            end
            local _, wikiEntryList = Tables.wikiEntryTable:TryGetValue(groupId)
            if not wikiEntryList then
                return false
            end
            for _, wikiEntryId in pairs(wikiEntryList.list) do
                if WikiUtils.isWikiEntryUnread(wikiEntryId) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end,
        sons = {
            WikiEntry = false,
        },
    },
    WikiEntry = {
        msgs = {
            MessageConst.ON_WIKI_ENTRY_UNLOCKED,
            MessageConst.ON_WIKI_ENTRY_LOCKED,
            MessageConst.ON_WIKI_ENTRY_READ,
        },
        readLike = true,
        needArg = true,
        Check = function(entryId)
            if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.Wiki) then
                return false
            end
            return WikiUtils.isWikiEntryUnread(entryId), UIConst.RED_DOT_TYPE.New
        end
    },
    WikiGuideEntry = {
        msgs = {
            MessageConst.ON_WIKI_ENTRY_UNLOCKED,
            MessageConst.ON_WIKI_ENTRY_LOCKED,
            MessageConst.ON_WIKI_ENTRY_READ,
        },
        readLike = true,
        needArg = true,
        Check = function(entryId)
            if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.Wiki) then
                return false
            end
            return WikiUtils.isWikiEntryUnread(entryId), UIConst.RED_DOT_TYPE.New
        end
    },
    WikiLimitedGuide = {
        msgs = {
            MessageConst.ON_LIMITED_GUIDE_WIKI_ENTRY_READ_STATE_CHANGE,
        },
        readLike = false,
        needArg = false,
        Check = function()
            local guideLimitedCtrl = require_ex('UI/Panels/GuideLimited/GuideLimitedCtrl')
            return not string.isEmpty(guideLimitedCtrl.GuideLimitedCtrl.s_waitReadGuideWikiEntry), UIConst.RED_DOT_TYPE.Normal
        end
    },



    Friend = {
        sons = {
            NewFriendRequest = true,
        },
        readLike = false,
    },

    NewFriendRequest = {
        msgs = {
            MessageConst.ON_FRIEND_REQUEST_CHANGE,
        },
        readLike = false,
        needArg = false,
        Check = function()
            return GameInstance.player.friendSystem:NeedFriendRequestRedDot(), UIConst.RED_DOT_TYPE.Normal
        end
    },

    FriendChatUnRead = {
        Check = function()
            local chatPanelShow = false
            local res, ctrl = UIManager:IsOpen(PanelId.SNSFriend)
            if res then
                chatPanelShow = ctrl:IsShow()
            end

            for index, roleId in pairs(GameInstance.player.friendChatSystem.luaShowValidRoleIds) do
                if GameInstance.player.friendChatSystem.luaShowRoleId ~= roleId or not chatPanelShow then
                    local chatInfo = GameInstance.player.friendChatSystem:GetChatInfo(roleId)
                    if chatInfo then
                        if chatInfo.unReadNum > 0 then
                            return true, UIConst.RED_DOT_TYPE.Normal
                        end
                    end
                end
            end
            return false
        end,
        needArg = false,
        readLike = false,
        msgs = {
            MessageConst.FRIEND_CHAT_MSG_READ,
        },
    },

    NewBusinessCard = {
        msgs = {
            MessageConst.ON_BUSINESS_CARD_UNLOCK,
            MessageConst.ON_BUSINESS_CARD_READ,
        },
        readLike = false,
        needArg = true,
        Check = function(id)
            return (GameInstance.player.friendSystem:IsBusinessCardUnlock(CS.Beyond.Gameplay.FriendBusinessCardUnlockType.BusinessCardTopic, id) and
                not GameInstance.player.friendSystem:IsBusinessCardReadRedDot(CS.Beyond.Gameplay.FriendBusinessCardUnlockType.BusinessCardTopic, id))
                 , UIConst.RED_DOT_TYPE.Normal
        end
    },

    NewAvatar = {
        msgs = {
            MessageConst.ON_AVATAR_UNLOCK,
            MessageConst.ON_AVATAR_READ,
        },
        readLike = false,
        needArg = true,
        Check = function(id)
            return GameInstance.player.friendSystem:IsBusinessCardUnlock(CS.Beyond.Gameplay.FriendBusinessCardUnlockType.Avatar, id) and
                not GameInstance.player.friendSystem:IsBusinessCardReadRedDot(CS.Beyond.Gameplay.FriendBusinessCardUnlockType.Avatar, id), UIConst.RED_DOT_TYPE.Normal
        end
    },

    NewAvatarFrame = {
        msgs = {
            MessageConst.ON_AVATAR_FRAME_UNLOCK,
            MessageConst.ON_AVATAR_FRAME_READ,
            MessageConst.ON_BIRTHDAY_SET,
        },
        readLike = false,
        needArg = true,
        Check = function(id)
            return GameInstance.player.friendSystem:IsBusinessCardUnlock(CS.Beyond.Gameplay.FriendBusinessCardUnlockType.AvatarFrame, id) and
                not GameInstance.player.friendSystem:IsBusinessCardReadRedDot(CS.Beyond.Gameplay.FriendBusinessCardUnlockType.AvatarFrame, id), UIConst.RED_DOT_TYPE.Normal
        end
    },

    BusinessCard = {
        sons = {
            NewBusinessCard = true,
            NewAvatar = true,
            NewAvatarFrame = true,
            SetBirthday = true,
        },
        readLike = false,
    },

    NewAvatarInfo = {
        sons = {
            NewAvatar = true,
            NewAvatarFrame = true,
        },
        readLike = false,
    },

    SelfBusinessCard = {
        sons = {
            SetBirthday = true,
        },
        readLike = false,
    },

    PlayInfoCell = {
        sons = {
            NewBusinessCard = true,
            SelfBusinessCard = true,
        },
        readLike = false,
    },

    SetBirthday = {
        msgs = {
            MessageConst.ON_BIRTHDAY_SET,
        },
        readLike = false,
        needArg = false,
        Check = function()
            return Utils.isSystemUnlocked(GEnums.UnlockSystemType.Birthday) and not GameInstance.player.friendSystem:IsBirthdayAlreadySet()
        end
    },






    SettlementMainTab = {
        msgs = {
            MessageConst.ON_SETTLEMENT_MODIFY,
        },
        readLike = false,
        needArg = true,
        Check = function(arg)
            local stlId
            if type(arg) == "string" then
                stlId = arg
            else
                stlId = unpack(arg)
            end
            return RedDotUtils.hasSettlementCanUpgradeRedDot(stlId), UIConst.RED_DOT_TYPE.Normal
        end,
    },




    DomainGradeReward = {
        msgs = {
            MessageConst.ON_DOMAIN_DEVELOPMENT_EXP_CHANGE,
            MessageConst.ON_DOMAIN_DEVELOPMENT_LEVEL_REWARD_GET,
        },
        readLike = false,
        needArg = true,
        Check = function(domainId)
            return RedDotUtils.hasSingleDomainGradeRedDot(domainId)
        end,
    },

    DomainSingleMap = {
        msgs = {
            MessageConst.ON_SETTLEMENT_MODIFY,
            MessageConst.ON_PACK_ITEM_END,
            MessageConst.ON_SELECT_BUYER_END,
            MessageConst.ON_DOMAIN_DEVELOPMENT_EXP_CHANGE,
            MessageConst.ON_DOMAIN_DEVELOPMENT_LEVEL_REWARD_GET,
            MessageConst.ON_WALLET_CHANGED,
        },
        readLike = false,
        needArg = true,
        Check = function(domainId)
            return RedDotUtils.hasSingleDomainDevelopmentRedDot(domainId)
        end,
    },

    DomainOtherMap = {
        msgs = {
            MessageConst.ON_SETTLEMENT_MODIFY,
            MessageConst.ON_PACK_ITEM_END,
            MessageConst.ON_SELECT_BUYER_END,
            MessageConst.ON_DOMAIN_DEVELOPMENT_EXP_CHANGE,
            MessageConst.ON_DOMAIN_DEVELOPMENT_LEVEL_REWARD_GET,
            MessageConst.ON_DOMAIN_DEPOT_DELIVERY_REWARD,
            MessageConst.ON_COLLECT_DELEGATE_REWARD,
            MessageConst.ON_DOMAIN_DEPOT_DAILY_REFRESHED,
            MessageConst.ON_WALLET_CHANGED,
        },
        readLike = false,
        needArg = true,
        Check = function(domainId)
            for checkDomainId, _ in cs_pairs(GameInstance.player.domainDevelopmentSystem.domainDevDataDic) do
                if checkDomainId ~= domainId and RedDotUtils.hasSingleDomainDevelopmentRedDot(checkDomainId) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end,
    },

    DomainEntry = {
        msgs = {
            MessageConst.ON_SETTLEMENT_MODIFY,
            MessageConst.ON_PACK_ITEM_END,
            MessageConst.ON_SELECT_BUYER_END,
            MessageConst.ON_DOMAIN_DEVELOPMENT_EXP_CHANGE,
            MessageConst.ON_DOMAIN_DEVELOPMENT_LEVEL_REWARD_GET,
            MessageConst.ON_DOMAIN_DEPOT_DELIVERY_REWARD,
            MessageConst.ON_COLLECT_DELEGATE_REWARD,
            MessageConst.ON_DOMAIN_DEPOT_DAILY_REFRESHED,
            MessageConst.ON_KITE_STATION_COLLECTION_REWARD,
        },
        sons = {
        },
        readLike = false,
        needArg = false,
        Check = function()
            for checkDomainId, _ in cs_pairs(GameInstance.player.domainDevelopmentSystem.domainDevDataDic) do
                if RedDotUtils.hasSingleDomainDevelopmentRedDot(checkDomainId) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end,
    },



    DomainPOIOverview = {
        msgs = {
            MessageConst.ON_SETTLEMENT_MODIFY,
            MessageConst.ON_WALLET_CHANGED,
        },
        readLike = false,
        needArg = true,
        Check = function(domainId)
            return DomainPOIUtils.HasDomainPOIPreviewRedDot(domainId)
        end,
    },

    DomainPOIOverviewSingle = {
        msgs = {
            MessageConst.ON_SETTLEMENT_MODIFY,
        },
        readLike = false,
        needArg = true,
        Check = function(arg)
            return DomainPOIUtils.HasSinglePOIPreviewRedDot(arg.domainId, arg.poiType)
        end,
    },


    KiteStationCollectionReward = {
        msgs = {
            MessageConst.ON_KITE_STATION_COLLECTION_REWARD
        },
        readLike = false,
        needArg = true,
        Check = function(stationId)
            return GameInstance.player.kiteStationSystem:CheckKiteStationCollectionReward(stationId), UIConst.RED_DOT_TYPE.Normal
        end,
    },




    AchievementMain = {
        readLike = false,
        needArg = false,
        Check = function()
            for _, achievementData in pairs(Tables.achievementTable) do
                if achievementData ~= nil and GameInstance.player.achievementSystem:IsAchievementUnread(achievementData.achieveId) then
                    return true, UIConst.RED_DOT_TYPE.New
                end
            end
            return false
        end,
        sons = {
            AchievementGroup = false,
        },
    },
    AchievementCategory = {
        readLike = false,
        needArg = true,
        Check = function(categoryId)
            local suc, categoryData = Tables.achievementTypeTable:TryGetValue(categoryId)
            if not suc then
                return false
            end
            for _, achievementData in pairs(Tables.achievementTable) do
                if achievementData ~= nil then
                    for i, groupData in pairs(categoryData.achievementGroupData) do
                        if achievementData.groupId == groupData.groupId
                            and GameInstance.player.achievementSystem:IsAchievementUnread(achievementData.achieveId) then
                            return true, UIConst.RED_DOT_TYPE.New
                        end
                    end
                end
            end
            return false
        end,
        sons = {
            AchievementItem = false,
        },
    },
    AchievementGroup = {
        readLike = false,
        needArg = true,
        Check = function(groupId)
            for _, achievementData in pairs(Tables.achievementTable) do
                if achievementData ~= nil then
                    if achievementData.groupId == groupId
                        and GameInstance.player.achievementSystem:IsAchievementUnread(achievementData.achieveId) then
                        return true, UIConst.RED_DOT_TYPE.New
                    end
                end
            end
            return false
        end,
        sons = {
            AchievementItem = false,
        },
    },
    AchievementItem = {
        msgs = {
            MessageConst.ON_UNREAD_ACHIEVEMENT,
            MessageConst.ON_READ_ACHIEVEMENT,
        },
        readLike = true,
        needArg = true,
        Check = function(achievementId)
            return GameInstance.player.achievementSystem:IsAchievementUnread(achievementId), UIConst.RED_DOT_TYPE.New
        end,
    },



    WeekRaid = {
        sons = {
            WeekRaidBattlePass = false,
            WeekRaidDelegate = false,
        },
        readLike = false,
        needArg = false,
        Check = function()
            local battlePassRedDotState = RedDotManager:GetRedDotState("WeekRaidBattlePass")
            if battlePassRedDotState then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end
    },
    WeekRaidBattlePass = {
        msgs = {
            MessageConst.ON_WEEK_RAID_BATTLE_PASS_UPDATE,
            MessageConst.ON_WEEK_RAID_BATTLE_PASS_VERSION_READ,
        },
        readLike = false,
        needArg = false,
        Check = function()
            local isVersionRead = GameInstance.player.weekRaidSystem:IsBattlePassVersionRead()
            if not isVersionRead then
                return true, UIConst.RED_DOT_TYPE.New
            end
            local isAnyReceivable = GameInstance.player.weekRaidSystem:IsAnyBattlePassRewardReceivable()
            return isAnyReceivable, UIConst.RED_DOT_TYPE.Normal
        end,
    },
    WeekRaidBattlePassRefresh = {
        msgs = {
            MessageConst.ON_WEEK_RAID_BATTLE_PASS_UPDATE,
        },
        readLike = false,
        needArg = false,
        Check = function()
            local mapMarkInstIds = GameInstance.player.weekRaidSystem:GetWeekRaidEntryMapMarkInstIds()
            if mapMarkInstIds.Count == 0 then
                return false
            end
            local isAnyReceivable = GameInstance.player.weekRaidSystem:IsAnyBattlePassRewardReceivable()
            if not isAnyReceivable then
                return false
            end
            local nextRefreshTime = Utils.getNextWeeklyServerRefreshTime()
            local currentTime = DateTimeUtils.GetCurrentTimestampBySeconds()
            local deltaTime = nextRefreshTime - currentTime
            local notify = deltaTime <= Tables.weekRaidConst.weekRaidBattlePassRefreshMapRemindTime
            return notify, UIConst.RED_DOT_TYPE.Normal
        end,
    },
    WeekRaidDelegate = {
        msgs = {
            MessageConst.ON_QUEST_STATE_CHANGE,
            MessageConst.ON_QUEST_OBJECTIVE_UPDATE,
            MessageConst.ON_WEEK_RAID_MISSION_REFRESH,
        },
        readLike = false,
        needArg = true,
        Check = function(args)

            if args == nil then
                local isAnyCompleted = GameInstance.player.weekRaidSystem:IsAnyMissionCompleted()
                return isAnyCompleted, UIConst.RED_DOT_TYPE.Normal
            end



            if args and args.missionId ~= nil then
                local isCompleted = GameInstance.player.weekRaidSystem:IsMissionCompleted(args.missionId)
                if isCompleted then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end


                return false
            end


            if args and args.missionType ~= nil then
                local delegateList = WeeklyRaidUtils.TabConfig[args.missionType].getDelegate()
                local isAnyCompleted = GameInstance.player.weekRaidSystem:IsAnyMissionCompleted(delegateList)
                return isAnyCompleted, UIConst.RED_DOT_TYPE.Normal
            end

            return false, UIConst.RED_DOT_TYPE.Normal
        end,
    },



    ActivityCenter = {
        msgs = {
            MessageConst.ON_ACTIVITY_CENTER_CLOSE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = false,
        Check = function()
            local activities = GameInstance.player.activitySystem:GetAllActivities()
            for _, activity in cs_pairs(activities) do

                local redDotName = ActivityUtils.getActivityRedDotName(activity.id)
                if not activity.isCompleted and redDotName and RedDotManager:GetRedDotState(redDotName, activity.id) then
                    return true
                end
            end
            return false
        end,
        sons = ActivityConst.ACTIVITY_COMMON_SONS,
    },
    ActivityTableMore = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(moreActivityData)
            for i = 1, #moreActivityData do
                local activityData = moreActivityData[i]
                if not activityData then
                    return false
                end
                local redDotName = ActivityUtils.getActivityRedDotName(activityData.id)
                if redDotName and RedDotManager:GetRedDotState(redDotName, activityData.id) then
                    return true
                end
            end
            return false
        end,
        sons = ActivityConst.ACTIVITY_COMMON_SONS,
    },
    ActivityIntroMission = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = true,
        needArg = true,
        Check = function(id)
            return ActivityUtils.isNewIntroMissionActivity(id)
        end,
    },
    ActivityBasic = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(id)
            return ActivityUtils.checkActivityRedDot(id)
        end
    },
    ActivityBaseMultiStage = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_LEVEL_REWARD_UPDATE,
        },
        readLike = false,
        needArg = true,
        Check = function(id)
            return RedDotUtils.hasActivityBaseMultiStageRedDot(id)
        end
    },
    ActivityGachaBeginner = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_GACHA_POOL_ROLE_DATA_CHANGED,
            MessageConst.ON_LEVEL_REWARD_UPDATE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(id)
            return RedDotUtils.hasActivityGachaBeginnerRedDot(id)
        end
    },
    ActivityGachaBeginnerJumpPoolBtn = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_GACHA_POOL_ROLE_DATA_CHANGED,
            MessageConst.ON_LEVEL_REWARD_UPDATE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = false,
        Check = function()
            if RedDotUtils.hasGachaBeginnerTicketNotUesRedDot() then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return RedDotUtils.hasGachaStarterCumulateRedDot(Tables.charGachaConst.beginnerGachaActivityPoolId)
        end
    },
    ActivityBaseMultiStageReward = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_LEVEL_REWARD_UPDATE,
        },
        readLike = true,
        needArg = true,
        Check = function(canReceive)
            return canReceive
        end
    },
    ActivityGuideWulingStage = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_READ_ACTIVITY_CONDITION_STAGE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = true,
        needArg = true,
        Check = function(args)
            local activityId, stageId = unpack(args)

            local newConditionalStageText = "new_activity_conditional_stage_key_"
            local success, isNew = ClientDataManagerInst:GetBool(newConditionalStageText .. stageId, false)
            if not success or isNew then
                ClientDataManagerInst:SetBool(newConditionalStageText .. stageId, false, false, EClientDataTimeValidType.Permanent)
                return true, UIConst.RED_DOT_TYPE.New
            end


            local activityData = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activityData then
                return false
            end
            local suc, stageData = activityData.stageDataDict:TryGetValue(stageId)



            if suc and stageData.Status == 2 then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end
    },
    ActivityCheckIn = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_CHECK_IN,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(id)
            local activity = GameInstance.player.activitySystem:GetActivity(id)
            if activity and activity.loginDays ~= activity.rewardDays.Count then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return ActivityUtils.checkActivityRedDot(id)
        end
    },
    ActivityCheckInReward = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_CHECK_IN,
        },
        readLike = true,
        needArg = true,
        Check = function(canReceive)
            return canReceive
        end
    },
    ActivityGlobalEffect = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_ACTIVITY_NEW_DAY,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
        },
        readLike = false,
        needArg = true,
        Check = function(id)
            local activity = GameInstance.player.activitySystem:GetActivity(id)
            if not activity then
                return false
            end
            if activity.stageDataDict then
                for stageId, stageData in pairs(activity.stageDataDict) do
                    local unlocked = stageData.Status ~= GEnums.ActivityConditionalStageState.Locked:GetHashCode()
                    if unlocked and RedDotManager:GetRedDotState("ActivityStaminaDiscountTask", {id, stageId}) then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end
            local suc, type = ActivityUtils.checkActivityRedDot(id)
            if suc then
                return suc, type
            elseif ActivityUtils.isNewActivityDayUnread(id) then


                if activity.stageDataDict then
                    for _, stageData in pairs(activity.stageDataDict) do
                        if stageData.Status < GEnums.ActivityConditionalStageState.Rewarded:GetHashCode() then
                            return true, UIConst.RED_DOT_TYPE.Normal
                        end
                    end
                end
            end
            return false
        end
    },
    ActivityNormalChallenge = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_READ_GAME_ENTRANCE_SERIES,
            MessageConst.ON_SUB_GAME_READ,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        sons = {
            ActivityNormalChallengeSeries = false,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)


            local activityData = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activityData then
                return false
            end

            if ActivityUtils.hasIntroMissionAndComplete(activityId) then
                local _, activitySeriesCfg = Tables.activityGameEntranceSeriesTable:TryGetValue(activityId)
                for seriesId, seriesCfg in pairs(activitySeriesCfg.seriesMap) do
                    local hasRedDot = RedDotUtils.hasActivityNormalChallengeSeriesRedDot(seriesId)
                    if hasRedDot then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end
            if ActivityUtils.isNewIntroMissionActivity(activityId) then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            if ActivityUtils.isNewActivity(activityId) then
                return true, UIConst.RED_DOT_TYPE.New
            end
            return false
        end
    },
    ActivityNormalChallengeSeries = {
        msgs = {
            MessageConst.ON_READ_GAME_ENTRANCE_SERIES,
            MessageConst.ON_SUB_GAME_READ,
        },
        readLike = false,
        needArg = true,
        Check = function(seriesId)
            return RedDotUtils.hasActivityNormalChallengeSeriesRedDot(seriesId)
        end
    },
    ActivityNormalChallengeGotoDetailBtn = {
        msgs = {
            MessageConst.ON_READ_GAME_ENTRANCE_SERIES,
            MessageConst.ON_SUB_GAME_READ,
        },
        sons = {
            ActivityNormalChallengeSeries = false,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)


            local activityData = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activityData or not activityData.isUnlocked then
                return false
            end

            local _, activitySeriesCfg = Tables.activityGameEntranceSeriesTable:TryGetValue(activityId)
            for seriesId, seriesCfg in pairs(activitySeriesCfg.seriesMap) do
                local hasRedDot = RedDotUtils.hasActivityNormalChallengeSeriesRedDot(seriesId)
                if hasRedDot then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end

            return false
        end
    },
    ActivityCharacterGuideLine = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        sons = {
            ActivityCharacterGuideLineBtnDetail = true,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)
            return ActivityUtils.checkActivityRedDot(activityId)
        end
    },
    ActivityCharacterGuideLineBtnDetail = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)
            return ActivityUtils.isNewUnlockCharacterGuideLine(activityId)
        end
    },
    ActivitySimulationTrainingTask = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_SIMULATION_TRAINING_GOTO,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_TASK_PROGRESS_CHANGE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)
            return ActivityUtils.checkActivityRedDot(activityId)
        end,
        sons = {
            ActivitySimulationTrainingGotoDetailBtn = true,
            ActivitySimulationTrainingLimitTaskRedDot = true,
        },
    },
    ActivitySimulationTrainingGotoDetailBtn = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_SIMULATION_TRAINING_GOTO,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_TASK_PROGRESS_CHANGE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)
            if ActivityUtils.IsSimulationTrainingGotoDetailRead() then
                return false
            end
            local activityData = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activityData then
                return false
            end

            local _, activityExcelData = Tables.activityTable:TryGetValue(activityId)
            if activityExcelData and not string.isEmpty(activityExcelData.introMissionQuestId) then
                local questState = GameInstance.player.mission:GetQuestState(activityExcelData.introMissionQuestId)
                if questState ~= CS.Beyond.Gameplay.MissionSystem.QuestState.Completed then
                    return false
                end
            end
            local hasCfg, taskConfig = Tables.activityConditionalMultiStageTaskConfigTable:TryGetValue(activityId)
            if not hasCfg then
                return false
            end
            for id, _ in pairs(taskConfig.TaskConfigMap) do
                local taskStatusData = activityData:GetTaskData(id)
                if taskStatusData then
                    local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskStatusData.Status)
                    if status ~= GEnums.ActivityConditionalTaskState.Rewarded then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end
            return false
        end
    },
    ActivitySimulationTrainingLimitTaskRedDot = {
        msgs = {
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_TASK_PROGRESS_CHANGE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        sons = {
            ActivitySimulationTrainingSingleTask = false,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)
            local activityData = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activityData then
                return false
            end
            local hasCfg, taskConfig = Tables.activityConditionalMultiStageTaskConfigTable:TryGetValue(activityId)
            if not hasCfg then
                return false
            end
            local hasNew = false
            for id, _ in pairs(taskConfig.TaskConfigMap) do
                local taskStatusData = activityData:GetTaskData(id)
                if taskStatusData then
                    local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskStatusData.Status)
                    if status == GEnums.ActivityConditionalTaskState.Completed then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                    if not hasNew and status ~= GEnums.ActivityConditionalTaskState.Locked and ActivityUtils.isSimulationTrainingNewTask(activityId, id) then
                        hasNew = true
                    end
                end
            end
            if hasNew then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end
    },
    ActivitySimulationTrainingSingleTask = {
        msgs = {
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_TASK_PROGRESS_CHANGE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_SIMULATION_TRAINING_TASK_READ,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local activityId = args.activityId
            local taskId = args.taskId
            local activityData = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activityData then
                return false
            end
            local taskData = activityData:GetTaskData(taskId)
            if taskData then
                local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskData.Status)
                if status == GEnums.ActivityConditionalTaskState.Completed then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
                if status ~= GEnums.ActivityConditionalTaskState.Locked and ActivityUtils.isSimulationTrainingNewTask(activityId, taskId) then
                    return true, UIConst.RED_DOT_TYPE.New
                end
            end
            return false
        end
    },
    ActivityCharTrial = {
        msgs = {
            MessageConst.ON_CHARACTER_TRIAL_INFO_CHANGE,
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = true,
        needArg = true,
        Check = function(activityId)
            local activityData = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activityData or not activityData.isUnlocked then
                return false
            end

            for dungeonId, trialData in pairs(Tables.activityCharTrial) do
                if trialData.activityId == activityId then
                    local trialStatus = GameInstance.player.activitySystem:CheckCharacterTrial(activityId, dungeonId)
                    if trialStatus == CS.Beyond.Gameplay.ActivitySystem.CharacterTrialStatus.CanGetReward then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end
            return ActivityUtils.checkActivityRedDot(activityId)
        end
    },
    ActivityItemSubmission = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(id)
            if GameInstance.player.activitySystem:GetActivityStatus(id) == GEnums.ActivityStatus.Locked then
                local record = GameInstance.player.activitySystem:GetFoodSubmitTabMissionRedDotRecord(id)
                if not record then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end

            for stageId, value in pairs(Tables.FoodSubmitStageIdTable) do
                if value.activityId == id then
                    local stageState = ActivityUtils.GetFoodSubmitStageState(id, stageId)
                    if stageState ~= GEnums.ActivityConditionalStageState.Locked then
                        local stageRecord = GameInstance.player.activitySystem:GetFoodSubmitTabStageRedDotRecord(id, stageId)
                        if not stageRecord then
                            return true, UIConst.RED_DOT_TYPE.Normal
                        end
                    end
                end
            end

            return ActivityUtils.checkActivityRedDot(id)
        end,
        sons = {
            ActivityGotoFoodSubmitMap = true,
        },
    },
    ActivityGotoFoodSubmitMap = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(id)
            if GameInstance.player.activitySystem:GetActivityStatus(id) == GEnums.ActivityStatus.Completed then
                return false
            end

            if GameInstance.player.activitySystem:GetActivityStatus(id) == GEnums.ActivityStatus.Locked then
                return false
            end

            if GameInstance.player.activitySystem:GetActivityStatus(id) == GEnums.ActivityStatus.IntroMission then
                return false
            end

            local checkGoTo = false
            for stageId, value in pairs(Tables.FoodSubmitStageIdTable) do
                if value.activityId == id then
                    local stageState = ActivityUtils.GetFoodSubmitStageState(id, stageId)
                    if stageState == GEnums.ActivityConditionalStageState.Unlocked then
                        checkGoTo = true
                        break
                    end
                end
            end

            if checkGoTo then
                local record = GameInstance.player.activitySystem:GetFoodSubmitGoToRedDotRecord(id)
                local timeVal = ActivityUtils.GetFoodSubmitCurGoToRedDot()
                if record < timeVal then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end

            return false
        end
    },
    ActivityCharTrialGetReward = {
        Check = function(args)
            local trialStatus = GameInstance.player.activitySystem:CheckCharacterTrial(args.activityId, args.dungeonId)
            if trialStatus == CS.Beyond.Gameplay.ActivitySystem.CharacterTrialStatus.CanGetReward then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end,

        readLike = true,
        needArg = true,

        msgs = {
            MessageConst.ON_CHARACTER_TRIAL_INFO_CHANGE,
        },
    },
    ActivityHighDifficulty = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_HIGH_DIFFICULTY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
            MessageConst.ACTIVITY_HIGH_DIFFICULTY_NEW_TASK_SET_FALSE,
        },
        readLike = false,
        needArg = true,
        Check = function(id)
            local activity = GameInstance.player.activitySystem:GetActivity(id)
            if not activity then
                return false
            end

            if GameInstance.player.highDifficultySystem:IsHighDifficultyUnlocked() and ActivityUtils.hasIntroMissionAndComplete(id) then
                local seriesIds = GameInstance.player.highDifficultySystem:GetAllUnlockSeriesIds()
                for i = 1,seriesIds.Count do
                    local seriesId = seriesIds[CSIndex(i)]
                    if HighDifficultyUtils.isNewHighDifficultySeries(seriesId) then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end

            for stageId, stageData in pairs(activity.stageDataDict) do
                local unlocked = stageData.Status ~= GEnums.ActivityConditionalStageState.Locked:GetHashCode()
                if unlocked and RedDotManager:GetRedDotState("ActivityHighDifficultyTask", {id, stageId}) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return ActivityUtils.checkActivityRedDot(id)
        end
    },
    ActivityHighDifficultyDetailBtn = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_HIGH_DIFFICULTY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
        },
        readLike = false,
        needArg = true,
        Check = function(id)

            if GameInstance.player.highDifficultySystem:IsHighDifficultyUnlocked() and ActivityUtils.hasIntroMissionAndComplete(id) then
                local seriesIds = GameInstance.player.highDifficultySystem:GetAllUnlockSeriesIds()
                for i = 1,seriesIds.Count do
                    local seriesId = seriesIds[CSIndex(i)]
                    if HighDifficultyUtils.isNewHighDifficultySeries(seriesId) then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end
            return false
        end
    },
    ActivityHighDifficultyTask = {
        msgs = {
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local activityId, stageId = unpack(args)

            local activity = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activity then
                return false
            end
            for id, stageData in cs_pairs(activity.stageDataDict) do
                if id == stageId and stageData.Status == GEnums.ActivityConditionalStageState.Completed:GetHashCode() then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end


            if activity.isCompleted then
                return false
            end
            if HighDifficultyUtils.isNewHighDifficultyTask(activityId, stageId) then
                return true, UIConst.RED_DOT_TYPE.New
            end
            return false
        end
    },
    ActivityHighDifficultyTaskSeries = {
        msgs = {
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
            MessageConst.ACTIVITY_HIGH_DIFFICULTY_NEW_TASK_SET_FALSE,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local activityId = args[1]
            local tasks = args[2]
            for _,stageData in ipairs(tasks) do
                if RedDotManager:GetRedDotState("ActivityHighDifficultyTask", {activityId, stageData.stageId}) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end
    },
    ActivityCharGrowthRebate = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
        },
        readLike = false,
        needArg = true,
        Check = function(id)
            local activity = GameInstance.player.activitySystem:GetActivity(id)
            if not activity then
                return false
            end

            for stageId, stageData in pairs(activity.stageDataDict) do
                local unlocked = stageData.Status ~= GEnums.ActivityConditionalStageState.Locked:GetHashCode()
                if unlocked and RedDotManager:GetRedDotState("ActivityDevelopReturnTask", {id, stageId}) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return ActivityUtils.checkActivityRedDot(id)
        end
    },
    ActivityDevelopReturnTask = {
        msgs = {
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local activityId, curStageId = unpack(args)
            local activity = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activity then
                return false
            end

            local suc, stageData = activity.stageDataDict:TryGetValue(curStageId)
            local isComplete = stageData.Status >= GEnums.ActivityConditionalStageState.Completed:GetHashCode()
            local isReceived = stageData.Status >= GEnums.ActivityConditionalStageState.Rewarded:GetHashCode()


            if isComplete and not isReceived then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end
    },
    ActivityStaminaDiscountTask = {
        msgs = {
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local activityId, curStageId = unpack(args)
            local activity = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activity then
                return false
            end

            local suc, stageData = activity.stageDataDict:TryGetValue(curStageId)
            if not suc then
                return false
            end
            local isComplete = stageData.Status >= GEnums.ActivityConditionalStageState.Completed:GetHashCode()
            local isReceived = stageData.Status >= GEnums.ActivityConditionalStageState.Rewarded:GetHashCode()

            if isComplete and not isReceived then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            if not isReceived and ActivityUtils.isNewActivityDayUnread(activityId) then
                return true, UIConst.RED_DOT_TYPE.New
            end
            return false
        end
    },
    ActivityDevelopReturnTaskSeries = {
        msgs = {
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
            MessageConst.ACTIVITY_HIGH_DIFFICULTY_NEW_TASK_SET_FALSE,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local activityId = args[1]
            local tasks = args[2]
            for _, stageData in ipairs(tasks) do
                if RedDotManager:GetRedDotState("ActivityDevelopReturnTask", {activityId, stageData.stageId}) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end
    },
    HighDifficultyMainHudGotoBtn = {
        msgs = {
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
        },
        readLike = false,
        needArg = false,
        Check = function()
            local activityId = HighDifficultyUtils.getHighDifficultyActivityId()
            if not activityId then
                return false
            end

            local activity = GameInstance.player.activitySystem:GetActivity(activityId)
            for stageId,_ in pairs(activity.stageDataDict) do
                local value, typ = RedDotManager:GetRedDotState("ActivityHighDifficultyTask", {activityId, stageId})
                if value and typ == UIConst.RED_DOT_TYPE.Normal then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end
    },
    HighDifficultyMainHudCell = {
        msgs = {
            MessageConst.ON_HIGH_DIFFICULTY_NEW_RED_DOT_SET_FALSE,
        },
        readLike = true,
        needArg = true,
        Check = function(seriesId)
            if HighDifficultyUtils.isNewHighDifficultySeries(seriesId) then
                return true, UIConst.RED_DOT_TYPE.New
            end
            return false
        end
    },
    ActivityConditionalMultiStage = {
        msgs = {
            MessageConst.ON_READ_ACTIVITY_CONDITION_STAGE,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(id)

            local activityData = GameInstance.player.activitySystem:GetActivity(id)
            if not activityData then
                return false
            end

            if activityData.status == GEnums.ActivityStatus.InProgress then

                for stageId, stageData in cs_pairs(activityData.stageDataDict) do
                    local status = GEnums.ActivityConditionalStageState.__CastFrom(stageData.Status)
                    if status == GEnums.ActivityConditionalStageState.Completed then

                        return true, UIConst.RED_DOT_TYPE.Normal
                    elseif status == GEnums.ActivityConditionalStageState.Unlocked
                        and ActivityUtils.isNewActivityConditionalStage(stageId) then

                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end
            return ActivityUtils.checkActivityRedDot(id)
        end
    },
    ActivityDaily = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_ACTIVITY_NEW_DAY,
        },
        readLike = false,
        needArg = true,
        Check = function(id)
            local activity = GameInstance.player.activitySystem:GetActivity(id)
            if not activity then
                return false
            end
            local suc,type = ActivityUtils.checkActivityRedDot(id)
            if suc then
                return suc, type
            elseif ActivityUtils.isNewActivityDayUnread(activity.id, Tables.activityConst.WebReflowRedDotDays) then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end
    },
    ActivitySpringFest = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_ACTIVITY_NEW_DAY,
        },
        readLike = false,
        needArg = true,
        Check = function(id)
            local suc,type = ActivityUtils.checkActivityRedDot(id)
            if suc then
                return suc, type
            elseif ActivityUtils.isNewActivityDayUnread(id,Tables.activityConst.SpringFestivalRedDotDays) then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end
    },
    ActivityWEB = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_ACTIVITY_NEW_DAY,
            MessageConst.ON_ACTIVITY_WEB_PORTAL_STATE_CHANGE,
        },
        readLike = false,
        needArg = true,
        penetrateLevel = UIConst.RED_DOT_TYPE.Normal,
        Check = function(id)
            if ActivityUtils.isWebActivityFirstVisitUnread(id) then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            local suc, type = ActivityUtils.checkActivityRedDot(id)
            if suc then
                return suc, type
            end
            local webSuc, webInfo = Tables.activityWebTable:TryGetValue(id)
            if webSuc and webInfo then
                if CS.Beyond.SDK.SDKAccountUtils.GetActivityWebPortalRedDot(webInfo.jumpId) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end
    },
    ActivityVersionGuide = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
        },
        readLike = false,
        needArg = true,
        Check = function(id)

            local activityData = Tables.ActivityVersionGuideStageTable[id]
            if not activityData then
                return false
            end

            local hasNormal = false
            local hasNew = false


            local stageListCount = activityData.stageList.Count
            for i = 1, stageListCount do
                local stageData = activityData.stageList[CSIndex(i)]
                local stageId = stageData.stageId
                local hasRedDot, redDotType = RedDotManager:GetRedDotState("ActivityGuideWulingStage", { id, stageId })

                if hasRedDot then
                    if redDotType == UIConst.RED_DOT_TYPE.Normal then
                        hasNormal = true
                    elseif redDotType == UIConst.RED_DOT_TYPE.New then
                        hasNew = true
                    end
                end
            end

            local defHas, defType = ActivityUtils.checkActivityRedDot(id)
            if defHas then
                if defType == UIConst.RED_DOT_TYPE.Normal then
                    hasNormal = true
                elseif defType == UIConst.RED_DOT_TYPE.New then
                    hasNew = true
                end
            end


            if hasNormal then
                return true, UIConst.RED_DOT_TYPE.Normal
            elseif hasNew then
                return true, UIConst.RED_DOT_TYPE.New
            end

            return false
        end
    },
    ActivityScratchOffLottery = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(id)
            local activity = GameInstance.player.activitySystem:GetActivity(id)
            if activity == nil or activity.isCompleted then
                return false
            end


            local state, type = ActivityUtils.checkActivityRedDot(id)
            if state then
                return state, type
            end

            if ActivityUtils.isNewActivityDayUnread(id) then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end
    },
    ActivityRewardOverview = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)

            return ActivityUtils.checkActivityRedDot(activityId)
        end
    },
    ActivityWeeklyTask = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)
            local activity = GameInstance.player.activitySystem:GetActivity(activityId)
            if activity == nil or activity.isCompleted then
                return false
            end

            local maxScore = 0
            for mileStoneId, received in pairs(activity.mileStoneInfo) do
                local success, info = Tables.activityWeeklyTaskMileStoneTable[activityId].mileStones:TryGetValue(mileStoneId)
                if success and activity.score >= info.score and not received then
                    return true
                end
                maxScore = math.max(maxScore, info.score)
            end

            if activity.score < maxScore then
                for _, task in pairs(activity.taskInfo) do
                    if task.Item1 == GEnums.ActivityConditionalStageState.Completed:GetHashCode() then
                        return true
                    end
                end
            end

            return ActivityUtils.checkActivityRedDot(activityId)
        end
    },
    ActivityGraffitiCleaning = {
        msgs = {
            MessageConst.ON_READ_ACTIVITY_CONDITION_STAGE,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(id)

            local activityData = GameInstance.player.activitySystem:GetActivity(id)
            if not activityData then
                return false
            end

            if activityData.status == GEnums.ActivityStatus.InProgress then

                for stageId, stageData in cs_pairs(activityData.stageDataDict) do
                    local status = GEnums.ActivityConditionalStageState.__CastFrom(stageData.Status)
                    if status == GEnums.ActivityConditionalStageState.Completed then

                        return true, UIConst.RED_DOT_TYPE.Normal
                    elseif ActivityUtils.isNewActivityConditionalStageByTime(id, stageId) then

                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end
            return ActivityUtils.checkActivityRedDot(id)
        end
    },
    ActivityCleaningCheckStageIsNewOrCompleteStatus = {
        readLike = false,
        needArg = true,
        sons = {
            ActivityCheckStageIsCompleteStatus = true,
            ActivityCheckStageIsNewStatusByTime = true,
        },
    },

    ActivityCheckStageIsCompleteStatus = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
        },
        readLike = false,
        needArg = true,
        Check = function(arg)
            local activityId = arg.activityId
            local activityData = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activityData then
                return false
            end
            local ids = arg.stageIds or {}
            if arg.stageId then
                table.insert(ids, arg.stageId)
            end
            for _, stageId in ipairs(ids) do
                local csConditionalStageInfo = activityData:GetStageData(stageId)
                if csConditionalStageInfo ~= nil then
                    local status = GEnums.ActivityConditionalStageState.__CastFrom(csConditionalStageInfo.Status)
                    if status == GEnums.ActivityConditionalStageState.Completed then
                        return true
                    end
                end
            end
            return false
        end
    },

    ActivityCheckStageIsNewStatus = {
        msgs = {
            MessageConst.ON_READ_ACTIVITY_CONDITION_STAGE,
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
        },
        readLike = true,
        needArg = true,
        Check = function(arg)
            local activityId = arg.activityId
            local activityData = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activityData then
                return false
            end
            local ids = arg.stageIds or {}
            if arg.stageId then
                table.insert(ids, arg.stageId)
            end
            for _, stageId in ipairs(ids) do
                local csConditionalStageInfo = activityData:GetStageData(stageId)
                if csConditionalStageInfo ~= nil then
                    local status = GEnums.ActivityConditionalStageState.__CastFrom(csConditionalStageInfo.Status)
                    if status == GEnums.ActivityConditionalStageState.Unlocked then

                        local isUnRead = ActivityUtils.isNewActivityConditionalStage(stageId)
                        if isUnRead then
                            return true
                        end
                    end
                end
            end
            return false
        end
    },

    ActivityCheckStageIsNewStatusByTime = {
        msgs = {
            MessageConst.ON_READ_ACTIVITY_CONDITION_STAGE,
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
        },
        readLike = true,
        needArg = true,
        Check = function(arg)
            local activityId = arg.activityId
            local activityData = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activityData then
                return false
            end
            local ids = arg.stageIds or {}
            if arg.stageId then
                table.insert(ids, arg.stageId)
            end
            for _, stageId in ipairs(ids) do
                if ActivityUtils.isNewActivityConditionalStageByTime(activityId, stageId) then
                    return true
                end
            end
            return false
        end
    },
    ActivityCheckStageIsNewOrCompleteStatus = {
        readLike = false,
        needArg = true,
        sons = {
            ActivityCheckStageIsCompleteStatus = true,
            ActivityCheckStageIsNewStatus = true,
        },
    },
    ActivityDungeon = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
            MessageConst.ON_READ_ACTIVITY_CONDITION_STAGE,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)


            local activityData = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activityData then
                return false
            end
            local stageCfgSuc, stageCfg = Tables.activityConditionalMultiStageTable:TryGetValue(activityId)

            if activityData.status == GEnums.ActivityStatus.InProgress then

                for stageId, stageData in cs_pairs(activityData.stageDataDict) do
                    local status = GEnums.ActivityConditionalStageState.__CastFrom(stageData.Status)
                    if status == GEnums.ActivityConditionalStageState.Completed then

                        local hasReward = true
                        if stageCfgSuc then
                            local stageInfoSuc, stageInfo = stageCfg.stageList:TryGetValue(stageId)
                            if stageInfoSuc and string.isEmpty(stageInfo.rewardId) then
                                hasReward = false
                            end
                        end
                        if hasReward then

                            return true, UIConst.RED_DOT_TYPE.Normal
                        end
                    elseif status == GEnums.ActivityConditionalStageState.Unlocked
                        and ActivityUtils.isNewActivityConditionalStage(stageId) then

                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end

            return ActivityUtils.checkActivityRedDot(activityId)
        end
    },
    ActivityLimitedFormula = {
        msgs = {
            MessageConst.ON_READ_ACTIVITY_CONDITION_STAGE,
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)


            local activityData = GameInstance.player.activitySystem:GetActivity(activityId)
            local activityCfgData = Tables.activityLimitedFormulaTable:GetValue(activityId)
            if not activityData or not activityCfgData then
                return false
            end

            if activityData.status == GEnums.ActivityStatus.InProgress then

                local endStageInfo = activityData:GetStageData(activityCfgData.endStageId)
                if endStageInfo then
                    local engStageStatus = GEnums.ActivityConditionalStageState.__CastFrom(endStageInfo.Status)
                    if engStageStatus ~= GEnums.ActivityConditionalStageState.Locked then
                        return false
                    end
                end

                for stageId, stageData in cs_pairs(activityData.stageDataDict) do
                    local status = GEnums.ActivityConditionalStageState.__CastFrom(stageData.Status)
                    if status == GEnums.ActivityConditionalStageState.Unlocked
                        and ActivityUtils.isNewActivityConditionalStage(stageId) then

                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                end
            end

            return ActivityUtils.checkActivityRedDot(activityId)
        end
    },
    ActivityArknightsBirthStage1 = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
            MessageConst.ON_READ_ACTIVITY_CONDITION_STAGE,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local ok, clicked, removed = ClientDataManagerInst:GetBool(args.activityId .. "_stage_1", false)
            return not clicked
        end
    },
    ActivityArknightsBirthStage2 = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
            MessageConst.ON_READ_ACTIVITY_CONDITION_STAGE,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local ok, clicked, removed = ClientDataManagerInst:GetBool(args.activityId .. "_stage_2", false)
            return not clicked
        end
    },
    ActivityArknightsBirthStageButton = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
            MessageConst.ON_READ_ACTIVITY_CONDITION_STAGE,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local ok, clicked, removed = ClientDataManagerInst:GetBool(args.activityId .. "_stage_" .. args.curStagePage, false)
            return not clicked
        end
    },
    ActivityArknightsBirth = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
            MessageConst.ON_READ_ACTIVITY_CONDITION_STAGE,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)
            local curStage = 0
            local suc, multiStageCfg = Tables.activityConditionalMultiStageTable:TryGetValue(activityId)
            local activityData = GameInstance.player.activitySystem:GetActivity(activityId)
            if activityData == nil then
                return false
            end
            if suc then
                for stageId, stageCfg in pairs(multiStageCfg.stageList) do

                    if not Tables.activityArknightsBirthMultiStageTable[stageId].isVisible then
                        goto continue
                    end
                    local csConditionalStageInfo = activityData:GetStageData(stageId)
                    if csConditionalStageInfo ~= nil then
                        local status = GEnums.ActivityConditionalStageState.__CastFrom(csConditionalStageInfo.Status)
                        if status ~= GEnums.ActivityConditionalStageState.Locked then
                            curStage = curStage + 1
                        end
                    end
                    :: continue ::
                end
            end
            if curStage == 1 then
                local ok, clicked, removed = ClientDataManagerInst:GetBool(activityId .. "_stage_1", false)
                return not clicked
            else
                local okStage1, clickedStage1, removedStage1 = ClientDataManagerInst:GetBool(activityId .. "_stage_1", false)
                local okStage2, clickedStage2, removedStage2 = ClientDataManagerInst:GetBool(activityId .. "_stage_2", false)
                return not clickedStage1 or not clickedStage2
            end
        end
    },
    ActivityContingencyContract = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)



            return ActivityUtils.checkActivityRedDot(activityId)
        end,
        sons = {
            ActivityContingencyContractDetail = true,
            ActivityContingencyContractTaskDetail = true,
            ActivityContingencyContractShopDetail = true,
        }
    },
    ActivityContingencyContractDetail = {
        msgs = {
            MessageConst.ON_CC_FIRST_ENTER_SELECT_AFTER_UPDATE
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)

            local activityData = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activityData or activityData.status ~= GEnums.ActivityStatus.InProgress then
                return false
            else

                local currentTime = DateTimeUtils.GetCurrentTimestampBySeconds()
                local isGameplayEnd = currentTime - activityData.gameplayEndTime > 0
                if isGameplayEnd then
                    return false
                end
            end

            local stage2TagMap = ContingencyContractUtils.CreateStageToTagMap()
            for stageId, _ in pairs(stage2TagMap) do
                local stageData = activityData:GetStageData(stageId)
                if stageData and GEnums.ActivityConditionalStageState.__CastFrom(stageData.Status) == GEnums.ActivityConditionalStageState.Unlocked and
                    not ActivityUtils.isCcFirstEnterSelectAfterUpdate(stageId) then
                        return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end,
    },
    ActivityContingencyContractTaskDetail = {
        msgs = {

        },
        readLike = false,
        needArg = true,
        Check = function(activityId)
            local activity = GameInstance.player.activitySystem:GetActivity(activityId)

            if not activity or activity.status ~= GEnums.ActivityStatus.InProgress then
                return false
            else

                local currentTime = DateTimeUtils.GetCurrentTimestampBySeconds()
                local isGameplayEnd = currentTime - activity.gameplayEndTime > 0
                if isGameplayEnd then
                    return false
                end
            end

            local Group2TaskMap = ContingencyContractUtils.CreateGroupToTaskMap(activityId)
            for _,taskList in pairs(Group2TaskMap) do
                for _,taskId in ipairs(taskList) do
                    local taskData = activity:GetTaskData(taskId)
                    if taskData then
                        local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskData.Status)
                        if status == GEnums.ActivityConditionalTaskState.Completed then
                            return true,UIConst.RED_DOT_TYPE.Normal
                        elseif status == GEnums.ActivityConditionalTaskState.Unlocked and ActivityUtils.isCcNewTask(activityId, taskId) then
                            return true,UIConst.RED_DOT_TYPE.Normal
                        end
                    end
                end
            end
            return false

        end,
        sons = {
            ActivityContingencyContractTaskTab = false,
        }
    },
    ActivityContingencyContractTaskTab = {
        msgs = {

        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local activityId = args.activityId
            local groupId = args.groupId
            local Group2TaskMap = ContingencyContractUtils.CreateGroupToTaskMap(activityId)
            local activity = GameInstance.player.activitySystem:GetActivity(args.activityId)

            if not activity then
                return false
            end
            local hasNewTask = false

            for _,taskId in ipairs(Group2TaskMap[groupId]) do
                local taskData = activity:GetTaskData(taskId)
                if taskData then
                    local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskData.Status)
                    if status == GEnums.ActivityConditionalTaskState.Completed then
                        return true,UIConst.RED_DOT_TYPE.Normal
                    elseif status == GEnums.ActivityConditionalTaskState.Unlocked and ActivityUtils.isCcNewTask(activityId, taskId) then
                        hasNewTask = true
                    end
                end
            end
            if hasNewTask then
                return true,UIConst.RED_DOT_TYPE.New
            end

            return false
        end,
        sons = {
            ActivityContingencyContractSingleTask = false,
        }
    },
    ActivityContingencyContractSingleTask = {
        msgs = {
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_TASK_PROGRESS_CHANGE,
            MessageConst.ON_CC_NEW_TASK_READ,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local activityId = args.activityId
            local taskId = args.taskId
            local activity = GameInstance.player.activitySystem:GetActivity(activityId)

            if not activity then
                return false
            end
            local taskData = activity:GetTaskData(taskId)
            if taskData then
                local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskData.Status)
                if status == GEnums.ActivityConditionalTaskState.Completed then
                    return true,UIConst.RED_DOT_TYPE.Normal
                elseif status == GEnums.ActivityConditionalTaskState.Unlocked and ActivityUtils.isCcNewTask(activityId, taskId) then
                    return true,UIConst.RED_DOT_TYPE.New
                end
            end
            return false
        end
    },
    ActivityContingencyContractShopDetail = {
        msgs = {
            MessageConst.ON_BUY_ITEM_SUCC,
            MessageConst.ON_WALLET_CHANGED,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)

            local activity = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activity or activity.status ~= GEnums.ActivityStatus.InProgress then
                return false
            end

            local info = Tables.activityContingencyContractTable[activityId]
            local shopId = info.shopGroupId
            local moneyId = Tables.activityShopAdditionalTable[shopId].activityMoneyId
            local shopGroupId = info.shopGroupId
            local compareToMoneyCount = info.compareToMoneyCount
            local shopSystem = GameInstance.player.shopSystem


            local currentMoneyCount = Utils.getItemCount(moneyId,true,true)
            if currentMoneyCount < compareToMoneyCount then
                return false
            end


            local shopGroupData = shopSystem:GetShopGroupData(shopGroupId)
            for _, shopId in pairs(shopGroupData.shopIdList) do
                local isUnlocked = shopSystem:CheckShopUnlocked(shopId)
                local shopTableData = Tables.shopTable[shopId]
                if isUnlocked or shopTableData.isShowWhenLock then
                    local shopData = shopSystem:GetShopData(shopId)
                    for goodsId, goodsData in pairs(shopData.goodsDic) do
                        local isUnlocked = shopSystem:CheckGoodsUnlocked(goodsId)
                        local goodsTableData = Tables.shopGoodsTable[goodsData.goodsTemplateId]
                        if isUnlocked or goodsTableData.isShowWhenLock then
                            if shopSystem:GetRemainCountByGoodsId(shopId, goodsId) > 0 then
                                return true, UIConst.RED_DOT_TYPE.Normal
                            end
                        end
                    end
                end
            end
            return false
        end
    },
    ActivitySeasonTower = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)

            return ActivityUtils.checkActivityRedDot(activityId)
        end,
        sons = {
            SeasonTowerMain = true,
        }
    },
    ActivitySkipChapter = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)

            local activity = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activity or not activity.isUnlocked then
                return ActivityUtils.checkActivityRedDot(activityId)
            end


            if RedDotManager:GetRedDotState("ActivitySkipChapterDetailBtn", activityId) then
                return true, UIConst.RED_DOT_TYPE.Normal
            end

            return ActivityUtils.checkActivityRedDot(activityId)
        end
    },
    ActivitySkipChapterDetailBtn = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)
            local _, clicked = ClientDataManagerInst:GetBool(activityId .. "_skip_chapter_detail_btn_clicked", false)
            if not clicked then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end
    },
    ActivityRacingDungeon = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)
            return ActivityUtils.checkActivityRedDot(activityId)
        end,
        sons = {
            ActivityRacingDungeonTaskDetail = true,
            ActivityRacingDungeonMilestoneDetail = true,
        }
    },
    ActivityRacingDungeonTaskDetail = {
        msgs = {
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_TASK_PROGRESS_CHANGE,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_NEW_TASK_READ,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)
            local activity = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activity then
                return false
            end
            if activity.status ~= GEnums.ActivityStatus.InProgress
            and activity.status ~= GEnums.ActivityStatus.Completed then
                return false
            end
            local taskDataDict = activity.taskDataDict
            for _, taskData in pairs(taskDataDict) do
                local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskData.Status)
                if status == GEnums.ActivityConditionalTaskState.Completed then
                    return true, UIConst.RED_DOT_TYPE.Normal
                elseif status == GEnums.ActivityConditionalTaskState.Unlocked
                    and ActivityUtils.RacingDungeonIsStageTwoTask(activityId, taskData.Id)
                    and ActivityUtils.isNewTask(activityId, taskData.Id) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end,
        sons = {
            ActivityRacingDungeonSingleTask = false,
        }
    },
    ActivityRacingDungeonSingleTask = {
        msgs = {
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_TASK_PROGRESS_CHANGE,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local activityId = args.activityId
            local taskId = args.taskId
            local activity = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activity then
                return false
            end
            local taskData = activity:GetTaskData(taskId)
            if taskData then
                local status = GEnums.ActivityConditionalTaskState.__CastFrom(taskData.Status)
                if status == GEnums.ActivityConditionalTaskState.Completed then
                    return true, UIConst.RED_DOT_TYPE.Normal
                elseif status == GEnums.ActivityConditionalTaskState.Unlocked
                    and ActivityUtils.RacingDungeonIsStageTwoTask(activityId, taskId)
                    and ActivityUtils.isNewTask(activityId, taskId) then
                    return true, UIConst.RED_DOT_TYPE.New
                end
            end
            return false
        end
    },
    ActivityRacingDungeonMilestoneDetail = {
        msgs = {
            MessageConst.ON_RACING_DUNGEON_GET_MILESTONE_REWARD,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
            MessageConst.ON_RACING_DUNGEON_MILESTONE_NODE_READ,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)
            local activity = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activity then
                return false
            end
            if activity.status ~= GEnums.ActivityStatus.InProgress
            and activity.status ~= GEnums.ActivityStatus.Completed then
                return false
            end
            local haveCfg, milestoneCfg = Tables.activityRacingDungeonMilestoneTable:TryGetValue(activityId)
            if not haveCfg then
                return false
            end
            local milestoneScore = activity.milestoneScore
            local receivedNodes = activity.receivedMilestoneNodes
            local hasNew = false
            for nodeId, nodeData in pairs(milestoneCfg.milestoneMap) do
                local _, stageData = activity.stageDataDict:TryGetValue(nodeData.unlockStageId)
                local status = stageData and GEnums.ActivityConditionalStageState.__CastFrom(stageData.Status)
                if status and status ~= GEnums.ActivityConditionalStageState.Locked then
                    if not receivedNodes:Contains(nodeId) and milestoneScore >= nodeData.completeScore then
                        return true, UIConst.RED_DOT_TYPE.Normal
                    end
                    if ActivityUtils.isNewMilestoneNode(activityId, nodeId) then
                        hasNew = true
                    end
                end
            end
            if hasNew then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end,
        sons = {
            ActivityRacingDungeonSingleMilestone = false,
        }
    },
    ActivityRacingDungeonSingleMilestone = {
        msgs = {
            MessageConst.ON_RACING_DUNGEON_GET_MILESTONE_REWARD,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
            MessageConst.ON_ACTIVITY_RACING_DUNGEON_MILESTONE_CHANGED,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            local activityId = args.activityId
            local nodeId = args.nodeId
            local activity = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activity then
                return false
            end
            local haveCfg, milestoneCfg = Tables.activityRacingDungeonMilestoneTable:TryGetValue(activityId)
            if not haveCfg then
                return false
            end
            if not nodeId then
                return false
            end
            local nodeData = milestoneCfg.milestoneMap[nodeId]
            if not nodeData then
                return false
            end
            local _, stageData = activity.stageDataDict:TryGetValue(nodeData.unlockStageId)
            local status = stageData and GEnums.ActivityConditionalStageState.__CastFrom(stageData.Status)
            if not status or status == GEnums.ActivityConditionalStageState.Locked then
                return false
            end
            if not activity.receivedMilestoneNodes:Contains(nodeId) and activity.milestoneScore >= nodeData.completeScore then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            if ActivityUtils.isNewMilestoneNode(activityId, nodeId) then
                return true, UIConst.RED_DOT_TYPE.New
            end
            return false
        end
    },
    ActivityReflowNew = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)



            return ActivityUtils.checkActivityRedDot(activityId)
        end,
        sons = {
            ActivityReflowSignin = true,
            ActivityReflowTask = true,
            ActivityReflowBP = true,
            ActivityReflowReconnectReward = true,
            ActivityReflowQuestionnaire = true,
        }
    },
    ActivityReflowSignin = {
        msgs = {
            MessageConst.ON_ACTIVITY_UPDATED,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)
            local activity = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activity then
                return false
            end
            return activity.loginDays ~= activity.rewardDays.Count
        end,
    },
    ActivityReflowTask = {
        msgs = {

        },
        readLike = false,
        needArg = true,
        Check = function(activityId)
            local activity = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activity then
                return false
            end
            local _, reflowCfg = Tables.activityReflowTable:TryGetValue(activityId)
            local milestoneId = reflowCfg.activityMilestoneId
            local _, milestone = activity.milestones:TryGetValue(milestoneId)

            for _, taskData in pairs(milestone.tasks) do
                if taskData and GEnums.ActivityConditionalTaskState.__CastFrom(taskData.status) == GEnums.ActivityConditionalTaskState.Completed then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end

            for _, stageData in pairs(milestone.stages) do
                if stageData  and stageData.reached and not stageData.rewarded then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end,
        sons = {
            ActivityReflowSingleTaskGroup = false,
            ActivityReflowSingleMilestoneStage = false,
        }
    },
    ActivityReflowSingleMilestoneStage = {
        msgs = {
            MessageConst.ON_REFLOW_MILESTONE_UPDATE,
        },
        readLike = false,
        needArg = true,
        Check = function(arg)
            local activityId = arg.activityId
            local milestoneId = arg.milestoneId
            local stageId = arg.stageId
            local activity = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activity then
                return false
            end
            local _, milestone = activity.milestones:TryGetValue(milestoneId)
            if milestone then
                local _, stageData = milestone.stages:TryGetValue(stageId)
                return stageData and stageData.reached and not stageData.rewarded
            end
            return false
        end,
        sons = {

        }
    },
    ActivityReflowSingleTaskGroup = {
        msgs = {

        },
        readLike = false,
        needArg = true,
        Check = function(arg)
            local activityId = arg.activityId
            local milestoneId = arg.milestoneId
            local taskGroupId = arg.taskGroupId
            local activity = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activity then
                return false
            end
            local _, activityCfg = Tables.activityTaskGroupTable:TryGetValue(activityId)
            local _, groupCfg = activityCfg.TaskGroupConfig:TryGetValue(taskGroupId)
            local _, milestone = activity.milestones:TryGetValue(milestoneId)
            for _, taskId in pairs(groupCfg.tasks) do
                local _, taskData = milestone.tasks:TryGetValue(taskId)
                if taskData and GEnums.ActivityConditionalTaskState.__CastFrom(taskData.status) == GEnums.ActivityConditionalTaskState.Completed then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end,
        sons = {
            ActivityReflowSingleTask = false,
        }
    },
    ActivityReflowSingleTask = {
        msgs = {
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_TASK_PROGRESS_CHANGE,
            MessageConst.ON_REFLOW_MILESTONE_UPDATE,
        },
        readLike = false,
        needArg = true,
        Check = function(arg)
            local activityId = arg.activityId
            local milestoneId = arg.milestoneId
            local taskId = arg.taskId
            local activity = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activity then
                return false
            end
            local _, milestone = activity.milestones:TryGetValue(milestoneId)
            if milestone then
                local _, taskData = milestone.tasks:TryGetValue(taskId)
                return taskData and GEnums.ActivityConditionalTaskState.__CastFrom(taskData.status) == GEnums.ActivityConditionalTaskState.Completed
            end
            return false
        end,
    },
    ActivityReflowBP = {
        msgs = {
            MessageConst.ON_REFLOW_BP_READ,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)
            local _, activityCfg = Tables.activityReflowTable:TryGetValue(activityId)
            local taskGroupId = activityCfg.reflowCfg.bpTaskGroupId
            local hasRedDot, type = BattlePassUtils.CheckGroupRedDot(taskGroupId)
            if hasRedDot then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end,
        sons = {
            BattlePassTaskItem = false,
        }
    },
    ActivityReflowQuestionnaire = {
        msgs = {
            MessageConst.ON_REFLOW_QUESTIONNAIRE_UPDATE,
            MessageConst.ON_REFLOW_QUESTIONNAIRE_READ,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)
            return not ActivityUtils.hasReflowQuestionnaireRead(activityId)
        end,
    },
    ActivityReflowReconnectReward = {
        msgs = {
            MessageConst.ON_REFLOW_REWARD_GAIN
        },
        readLike = true,
        needArg = true,
        Check = function(activityId)
            local activity = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activity then
                return false
            end
            return not activity.oneTimeRewardReceived
        end
    },



    ActivityDungeonState = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
            MessageConst.ON_READ_ACTIVITY_CONDITION_STAGE,
        },
        readLike = false,
        needArg = true,
        Check = function(args)


            local activityData = GameInstance.player.activitySystem:GetActivity(args.activityId)
            if not activityData then
                return false
            end

            if activityData.status == GEnums.ActivityStatus.InProgress then

                local success, stageData = activityData.stageDataDict:TryGetValue(args.stageId)
                if not success then
                    return false
                end
                local status = GEnums.ActivityConditionalStageState.__CastFrom(stageData.Status)
                if status == GEnums.ActivityConditionalStageState.Completed then

                    local stageCfgSuc, stageCfg = Tables.activityConditionalMultiStageTable:TryGetValue(args.activityId)
                    if stageCfgSuc then
                        local stageInfoSuc, stageInfo = stageCfg.stageList:TryGetValue(args.stageId)
                        if stageInfoSuc and string.isEmpty(stageInfo.rewardId) then
                            return false
                        end
                    end

                    return true, UIConst.RED_DOT_TYPE.Normal
                elseif status == GEnums.ActivityConditionalStageState.Unlocked
                    and ActivityUtils.isNewActivityConditionalStage(args.stageId) then

                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end
    },










    ActivityDoubleAssaultDungeon = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
        },
        readLike = false,
        needArg = true,
        Check = function(args)
            if type(args) ~= "table" then
                return false
            end
            local activityData = GameInstance.player.activitySystem:GetActivity(args.activityId)
            if not activityData or activityData.status ~= GEnums.ActivityStatus.InProgress then
                return false
            end

            local function checkSingleDungeon(dungeonId)
                local hasStateCfg, dungeonStateCfg = Tables.activityDungeonState:TryGetValue(dungeonId)
                if not hasStateCfg then
                    return false
                end
                local hasStageData, stageData = activityData.stageDataDict:TryGetValue(dungeonStateCfg.activityStage)
                if not hasStageData then
                    return false
                end
                local status = GEnums.ActivityConditionalStageState.__CastFrom(stageData.Status)
                if status == GEnums.ActivityConditionalStageState.Locked then
                    return false
                end
                return ActivityUtils.isNewDoubleAssaultDungeon(dungeonId)
            end

            if args.dungeonId then
                if checkSingleDungeon(args.dungeonId) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
                return false
            end

            local hasCfg, activityDungeonCfg = Tables.activityDungeonTable:TryGetValue(args.activityId)
            if not hasCfg or not activityDungeonCfg then
                return false
            end


            for dungeonId, _ in pairs(activityDungeonCfg.gameMap) do
                local id = tostring(dungeonId)
                if Tables.dungeonNormal2RaidTable:ContainsKey(id) and checkSingleDungeon(id) then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end
    },






    ActivityDoubleBattle = {
        msgs = {
            MessageConst.ON_ACTIVITY_NEW_RED_DOT_SET_FALSE,
            MessageConst.ON_ACTIVITY_UPDATED,
            MessageConst.ON_CONDITIONAL_MULTI_STAGE_UPDATE,
            MessageConst.ON_READ_ACTIVITY_CONDITION_STAGE,

            MessageConst.ON_SC_MULTI_STAGE_ACTIVITY_GAIN_TASK_REWARD,
            MessageConst.ON_SC_MULTI_STAGE_ACTIVITY_GAIN_REWARD,
        },
        readLike = false,
        needArg = true,
        Check = function(activityId)

            local hasActivityRedDot, activityRedDotType = ActivityUtils.checkActivityRedDot(activityId)
            if hasActivityRedDot then
                return hasActivityRedDot, activityRedDotType
            end

            local activityData = GameInstance.player.activitySystem:GetActivity(activityId)
            if not activityData or activityData.status ~= GEnums.ActivityStatus.InProgress then
                return false
            end


            local hasNewDungeon = RedDotManager:GetRedDotState("ActivityDoubleAssaultDungeon", { activityId = activityId })
            if hasNewDungeon then
                return true, UIConst.RED_DOT_TYPE.Normal
            end


            local hasTaskCfg, taskCfg = Tables.activityConditionalMultiStageTaskConfigTable:TryGetValue(activityId)
            if hasTaskCfg and taskCfg then
                local taskMap = ActivityUtils.CreateGroupToTaskMap(activityId)
                local completedHash = GEnums.ActivityConditionalTaskState.Completed:GetHashCode()
                for _, taskList in pairs(taskMap) do
                    for _, taskId in ipairs(taskList) do
                        local hasTaskConfig, taskGroupCfg = taskCfg.TaskConfigMap:TryGetValue(taskId)
                        if hasTaskConfig and taskGroupCfg and #taskGroupCfg.completeConditionId == 1 then
                            local taskData = activityData:GetTaskData(taskId)
                            if taskData and taskData.Status == completedHash then
                                return true, UIConst.RED_DOT_TYPE.Normal
                            end
                        end
                    end
                end
            end
            return false
        end,
        sons = {
        }
    },




    DomainDepot = {
        readLike = false,
        needArg = true,
        Check = function(domainId)
            if RedDotManager:GetRedDotState("DomainDepotInstList", domainId) then
                return true
            end
            if RedDotManager:GetRedDotState("DomainDepotMyOrder") then
                return true
            end
            return false
        end,
        sons = {
            DomainDepotInstList = false,
            DomainDepotMyOrder = false,
        }
    },
    DomainDepotInstList = {
        readLike = false,
        needArg = true,
        Check = function(domainId)
            local allDepotIdList = GameInstance.player.domainDepotSystem:GetDomainDepotIdListByDomainId(domainId)
            if allDepotIdList == nil then
                return false
            end
            for index = 0, allDepotIdList.Count - 1 do
                local depotId = allDepotIdList[index]
                if DomainDepotUtils.IsDomainDepotDeliverInTradingState(depotId) then
                    return true
                end
            end
            return false
        end,
        sons = {
            DomainDepotInstCell = false,
        }
    },
    DomainDepotInstCell = {
        readLike = false,
        needArg = true,
        msgs = {
            MessageConst.ON_PACK_ITEM_END,
            MessageConst.ON_SELECT_BUYER_END,
        },
        Check = function(depotId)
            return DomainDepotUtils.IsDomainDepotDeliverInTradingState(depotId)
        end
    },
    DomainDepotMyOrder = {
        msgs = {
            MessageConst.ON_DOMAIN_DEPOT_DELIVERY_REWARD,
            MessageConst.ON_COLLECT_DELEGATE_REWARD,
            MessageConst.ON_DOMAIN_DEPOT_DAILY_REFRESHED,
        },
        readLike = false,
        needArg = false,
        Check = function()
            for _, info in cs_pairs(GameInstance.player.domainDepotSystem.myDelegateDeliverList) do
                if info.packageProgress == GEnums.DomainDepotPackageProgress.SendPackageTimeout or
                    info.packageProgress == GEnums.DomainDepotPackageProgress.WaitingRecvFinalPayment then
                    return true
                end
            end
            return false
        end
    },



    BattlePass = {
        readLike = false,
        msgs = {
            MessageConst.ON_DOMAIN_DEPOT_DELIVERY_REWARD
        },
        needArg = false,
        Check = function()
            if not BattlePassUtils.CheckBattlePassSeasonValid() then
                return false
            end
            local bpSystem = GameInstance.player.battlePassSystem
            local seasonRedDot = bpSystem:IsSeasonUnread(bpSystem.seasonData.seasonId)
            if seasonRedDot then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            local planRedDot = BattlePassUtils.CheckHasAvailBpPlanReward()
            local taskRedDot = false
            for labelId, labelInfo in pairs(bpSystem.taskData.taskLabels) do
                local hasRedDot, redDotType = BattlePassUtils.CheckLabelRedDot(labelId)
                if hasRedDot and redDotType == UIConst.RED_DOT_TYPE.Normal then
                    taskRedDot = true
                    break
                end
            end
            if planRedDot or taskRedDot then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end,
        sons = {
            BattlePassPlan = false,
            BattlePassTask = false,
        }
    },
    BattlePassPlan = {
        readLike = false,
        msgs = {
            MessageConst.ON_BATTLE_PASS_LEVEL_UPDATE,
            MessageConst.ON_BATTLE_PASS_TRACK_UPDATE,
        },
        needArg = false,
        Check = function()
            if BattlePassUtils.CheckHasAvailBpPlanReward() then
                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end,
    },
    BattlePassTask = {
        readLike = false,
        msgs = {
        },
        needArg = false,
        Check = function()
            local bpSystem = GameInstance.player.battlePassSystem
            for labelId, labelInfo in pairs(bpSystem.taskData.taskLabels) do
                local hasLabelRedDot, labelRedDotType = BattlePassUtils.CheckLabelRedDot(labelId)
                if hasLabelRedDot then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end,
        sons = {
            BattlePassTaskLabel = false
        }
    },
    BattlePassTaskLabel = {
        readLike = false,
        msgs = {
        },
        needArg = true,
        Check = function(labelId)
            return BattlePassUtils.CheckLabelRedDot(labelId)
        end,
        sons = {
            BattlePassTaskItem = false,
        }
    },
    BattlePassTaskItem = {
        readLike = false,
        msgs = {
            MessageConst.ON_BATTLE_PASS_TASK_UPDATE,
            MessageConst.ON_BATTLE_PASS_TASK_BASIC_INFO_UPDATE,
            MessageConst.ON_BATTLE_PASS_TASK_READ_UPDATE,
        },
        needArg = true,
        Check = function(taskId)
            if BattlePassUtils.CheckTaskUnread(taskId) then
                return true, UIConst.RED_DOT_TYPE.New
            end
            return false
        end
    },




    CashShop = {
        readLike = false,
        needArg = false,
        sons = {
            CashShopCreditShopGetCredit = true,
            CashShopHaveCharPotentialExchange = true,
        },
    },

    CashShopNewCashGoods = {
        readLike = false,
        msgs = {
            MessageConst.ON_READ_CASH_SHOP_GOODS,
            MessageConst.ON_CASH_SHOP_PLATFORM_DATA_REFRESH,
        },
        needArg = true,
        Check = function(goodsIds)
            return CashShopUtils.CheckCashShopNewCashGoodsRedDot(goodsIds)
        end
    },

    CashShopToken = {
        readLike = false,
        msgs = {
            MessageConst.ON_SHOP_GOODS_SEE_GOODS_INFO_CHANGE,
        },
        needArg = true,
        sons = {
            CashShopHaveCharPotentialExchange = false,
        },
        Check = function(arg)

            if arg.checkPotentialExchange then
                if CashShopUtils.HaveCharPotentialExchange() then
                    return true
                end
            end
            local goodsIds = arg.goodsIds
            for _, goodsId in ipairs(goodsIds) do
                local isNew = GameInstance.player.shopSystem:IsNewGoodsId(goodsId)
                if isNew then
                    return true, UIConst.RED_DOT_TYPE.New
                end
            end
            return false
        end
    },

    CashShopTokenNormal = {
        readLike = false,
        msgs = {
            MessageConst.ON_SHOP_GOODS_SEE_GOODS_INFO_CHANGE,
        },
        needArg = true,
        sons = {
            CashShopHaveCharPotentialExchange = true,
        },
        Check = function(goodsIds)
            for _, goodsId in ipairs(goodsIds) do
                local isNew = GameInstance.player.shopSystem:IsNewGoodsId(goodsId)
                if isNew then
                    return true
                end
            end
            return false
        end
    },

    CashShopCreditShopGetCredit = {
        readLike = false,
        msgs = {
            MessageConst.ON_SPACESHIP_GUEST_ROOM_RECV_VISIT_LIST_REWARD,
            MessageConst.ON_SPACESHIP_RECV_QUERY_VISIT_INFO,
            MessageConst.ON_ITEM_COUNT_CHANGED,
        },
        needArg = false,
        Check = function()
            return CashShopUtils.CheckSpaceshipCreditShopGetCreditRedDot()
        end
    },

    CashShopHaveCharPotentialExchange = {
        readLike = false,
        msgs = {
            MessageConst.ON_SC_SHOP_SWAP_CHAR_POTENTIAL_UP,
        },
        needArg = false,
        Check = function()
            local ret = CashShopUtils.HaveCharPotentialExchange()
            return ret
        end
    },





    StarShop = {
        readLike = false,
        msgs = {
            MessageConst.ON_SHOP_GOODS_SEE_GOODS_INFO_CHANGE,
            MessageConst.ON_SHOP_REFRESH,
        },
        needArg = false,
        Check = function()
            local shopSystem = GameInstance.player.shopSystem
            local shopGroupData = shopSystem:GetShopGroupData("shop_trstar")
            if not shopGroupData then
                return false
            end
            for i = 0, shopGroupData.shopIdList.Count - 1 do
                local shopId = shopGroupData.shopIdList[i]
                local shopData = shopSystem:GetShopData(shopId)
                if shopData then
                    for goodsId, _ in pairs(shopData.goodsDic) do
                        if shopSystem:IsNewGoodsId(goodsId) then
                            return true, UIConst.RED_DOT_TYPE.Normal
                        end
                    end
                end
            end
            return false
        end,
    },




    SSControlCenter = {
        readLike = false,
        msgs = {
            MessageConst.ON_SPACESHIP_ASSIST_DATA_MODIFY,
            MessageConst.ON_SPACESHIP_GROW_CABIN_MODIFY,
            MessageConst.ON_SPACESHIP_GROW_CABIN_BREED,
            MessageConst.ON_SPACESHIP_MANUFACTURING_STATION_SYNC,
            MessageConst.ON_SPACESHIP_MANUFACTURING_STATION_COLLECT,
            MessageConst.SPACESHIP_ON_SYNC_ROOM_STATION,
            MessageConst.ON_SPACESHIP_CLUE_INFO_CHANGE,
        },
        needArg = true,
        Check = function()
            local rooms = GameInstance.player.spaceship.rooms
            local beHelpedCreditLeft, _ = GameInstance.player.spaceship:GetCabinAssistedTime(Tables.spaceshipConst.controlCenterRoomId)
            for id, roomInfo in pairs(rooms) do
                if roomInfo.type == GEnums.SpaceshipRoomType.GrowCabin then
                    if RedDotManager:GetRedDotState("SSGrowCabin", id) then
                        return true
                    end
                elseif roomInfo.type == GEnums.SpaceshipRoomType.ManufacturingStation then
                    if RedDotManager:GetRedDotState("SSManufacturingStation", id) then
                        return true
                    end
                elseif roomInfo.type == GEnums.SpaceshipRoomType.GuestRoomClueExtension then
                    if RedDotManager:GetRedDotState("SSGuestRoomClue") then
                        return true
                    end
                elseif beHelpedCreditLeft > 0 then
                    return true
                end
            end
            return false
        end,
        sons = {
            SSGrowCabin = false,
            SSManufacturingStation = false,
            SSGuestRoomClue = false,
        }
    },


    SSControlCenterRoot = {
        readLike = false,
        msgs = {
            MessageConst.ON_SPACESHIP_ASSIST_DATA_MODIFY,
            MessageConst.ON_SPACESHIP_GROW_CABIN_MODIFY,
            MessageConst.ON_SPACESHIP_GROW_CABIN_BREED,
            MessageConst.ON_SPACESHIP_MANUFACTURING_STATION_SYNC,
            MessageConst.ON_SPACESHIP_MANUFACTURING_STATION_COLLECT,
            MessageConst.SPACESHIP_ON_SYNC_ROOM_STATION,
            MessageConst.ON_SPACESHIP_CLUE_INFO_CHANGE,
        },
        needArg = true,
        Check = function(roomIndex)
            if GameInstance.player.spaceship.isViewingFriend then
                return false
            end

            for id, index in pairs(roomIndex) do
                local succ, roomInfo = GameInstance.player.spaceship:TryGetRoom(id)
                if succ then
                    if roomInfo.type == GEnums.SpaceshipRoomType.GrowCabin then
                        if RedDotManager:GetRedDotState("SSGrowCabin", id) then
                            return true
                        end
                    end
                end
            end
            return false
        end,
        sons = {
            SSGrowCabin = false,
            SSManufacturingStation = false,
            SSGuestRoomClue = false,
        }
    },

    SSGrowCabin = {
        readLike = false,
        needArg = true,
        Check = function(roomId)
            return GameInstance.player.spaceship:HasGrowCabinProduct(roomId)
        end,
        msgs = {
            MessageConst.ON_SPACESHIP_GROW_CABIN_MODIFY,
            MessageConst.ON_SPACESHIP_GROW_CABIN_BREED,
        },
    },

    SSManufacturingStation = {
        readLike = false,
        needArg = true,
        Check = function(roomId)
            local beHelpedCreditLeft, _ = GameInstance.player.spaceship:GetCabinAssistedTime(roomId)
            local hasProduct = GameInstance.player.spaceship:HasProductToCollect(roomId)
            return hasProduct or beHelpedCreditLeft > 0
        end,
        msgs = {
            MessageConst.ON_SPACESHIP_ASSIST_DATA_MODIFY,
            MessageConst.ON_SPACESHIP_MANUFACTURING_STATION_SYNC,
            MessageConst.ON_SPACESHIP_MANUFACTURING_STATION_COLLECT,
        },
    },

    SSGuestRoomClue = {
        readLike = false,
        needArg = true,
        Check = function()
            local spaceship = GameInstance.player.spaceship
            local clueData = GameInstance.player.spaceship:GetClueData()
            return spaceship:IsGuestRoomClueCannotAutoRecv()
                or spaceship:HasGuestRoomClueCanCollect()
                or spaceship:IsGuestRoomClueWaitForExchange()
                or (clueData and clueData.dailyClueIndex ~= 0)
        end,
        msgs = {
            MessageConst.SPACESHIP_ON_SYNC_ROOM_STATION,
            MessageConst.ON_SPACESHIP_CLUE_INFO_CHANGE,
        },
    },

    SSMusicPlayerAlbum = {
        readLike = true,
        needArg = true,
        Check = function(albumId)
            local success, albumInfo = Tables.spaceshipAlbumMusicTable:TryGetValue(albumId)
            if not success then
                return false
            end
            for _, musicId in pairs(albumInfo.musicList) do
                if GameInstance.player.spaceship:CheckMusicUnlock(musicId) and not RedDotUtils.checkMusicIsRead(musicId) then
                    return true, UIConst.RED_DOT_TYPE.New
                end
            end
            return false
        end,
        sons = {
            SSMusicPlayerSingleMusic = false,
        }
    },

    SSMusicPlayerSingleMusic = {
        msgs = {
            MessageConst.ON_SET_MUSIC_READ,
        },
        readLike = false,
        needArg = true,
        Check = function(musicId)
            return GameInstance.player.spaceship:CheckMusicUnlock(musicId) and not RedDotUtils.checkMusicIsRead(musicId), UIConst.RED_DOT_TYPE.New
        end
    },



    ValuableDepotInMainHud = {
        msgs = {
            MessageConst.ON_VALUABLE_DEPOT_CHANGED,
            MessageConst.ON_ITEM_LOCKED_STATE_CHANGED,
            MessageConst.ON_READ_NEW_INST_ITEM,
            MessageConst.ON_VALUABLE_DEPOT_IMPORT_ITEM_CHANGED,
        },
        readLike = false,
        needArg = false,
        Check = function()
            local hasRedDot, redDotType = RedDotManager:GetRedDotState("ValuableDepot")
            if hasRedDot and redDotType == UIConst.RED_DOT_TYPE.Normal then

                return true, UIConst.RED_DOT_TYPE.Normal
            end
            return false
        end,
    },

    ValuableDepot = {
        msgs = {},
        readLike = false,
        needArg = false,
        sons = {
            ValuableDepotTabCommercialItem = true,
        },
        Check = function()
            local types = GameInstance.player.inventory:GetValuableDepotTypes()
            for i = 1, types.Count do
                local suc, value = RedDotManager:GetRedDotState("ValuableDepotTabCommon", types[CSIndex(i)])
                if suc and value then
                    return true, UIConst.RED_DOT_TYPE.Normal
                end
            end
            return false
        end
    },

    ValuableDepotTabCommon = {
        msgs = {
            MessageConst.ON_READ_NEW_INST_ITEM,
            MessageConst.ON_ITEM_COUNT_CHANGED,
            MessageConst.ON_VALUABLE_DEPOT_IMPORT_ITEM_CHANGED,
        },
        readLike = false,
        needArg = true,
        Check = function(type)
            return RedDotUtils.isValuableDepotTabHasNewObtainedImportantItem(type)
        end,
    },

    ValuableDepotTabCommercialItem = {
        msgs = {
            MessageConst.ON_VALUABLE_DEPOT_CHANGED,
            MessageConst.ON_ITEM_LOCKED_STATE_CHANGED,
            MessageConst.ON_READ_NEW_INST_ITEM,
            MessageConst.ON_ITEM_COUNT_CHANGED,
            MessageConst.ON_VALUABLE_DEPOT_IMPORT_ITEM_CHANGED,
        },
        readLike = false,
        needArg = false,
        Check = function()
            return RedDotUtils.hasValuableDepotTabCommercialItemRedDot()
        end,
    },




    DungeonActivity = {
      msgs = {
          MessageConst.ON_ACTIVITY_UPDATED
      },
      readLike = false,
      needArg = true,
      Check = function(activityId)
          return DungeonActivityUtils.checkDungeonActivityRedDot(activityId)
      end
    },







    SeasonTowerMain = {
        msgs = {
            MessageConst.ON_SEASON_TOWER_NEW,
        },
        readLike = false,
        needArg = false,
        Check = function()
            return SeasonTowerUtils.checkMainRedDot()
        end,
        sons = {
            SeasonTowerAchieve = true,
        }
    },
    SeasonTowerNew = {
        msgs = {
            MessageConst.ON_SEASON_TOWER_NEW,
        },
        readLike = true,
        needArg = false,
        Check = function()
            return SeasonTowerUtils.checkMainRedDot(), UIConst.RED_DOT_TYPE.New
        end,
    },
    SeasonTowerNewSeason = {
        msgs = {
            MessageConst.ON_SEASON_TOWER_NEW,
        },
        readLike = true,
        needArg = false,
        Check = function()
            return SeasonTowerUtils.isNewSeason(), UIConst.RED_DOT_TYPE.New
        end,
    },
    SeasonTowerAchieve = {
        msgs = {
            MessageConst.ON_SEASON_TOWER_ACHIEVE_UPDATE,
        },
        readLike = false,
        needArg = false,
        Check = function()
            return SeasonTowerUtils.hasAnyAchieveReward()
        end,
    },
    SeasonTowerAchieveSingle = {
        msgs = {
            MessageConst.ON_SEASON_TOWER_ACHIEVE_UPDATE,
        },
        readLike = false,
        needArg = true,
        Check = function(gameId)
            return SeasonTowerUtils.hasGameAchieveReward(gameId)
        end,
    },





    GameSetting = {
        sons = {
            GameSettingKeyHint = true,
        },
        readLike = false,
    },

    GameSettingKeyHint = {
        msgs = {
            MessageConst.ON_INPUT_DEVICE_TYPE_CHANGED,
            MessageConst.GAME_SETTING_KEY_SETTING_CHANGED,
        },
        readLike = false,
        needArg = false,
        Check = function()
            if not CS.Beyond.GameSetting.IsSettingTabVisible(GameSettingConst.TAB_ID_KEY_HINT) then
                return false
            end
            local gameSettingSystem = GameInstance.player.gameSettingSystem
            local settingItems = Tables.settingTabTable[GameSettingConst.TAB_ID_KEY_HINT].tabItems
            for settingId, settingItemData in pairs(settingItems) do
                if not gameSettingSystem:IsKeySettingValid(settingId) then
                    return true
                end
            end
            return false
        end
    },

    GameSettingKeyHintItem = {
        msgs = {
            MessageConst.GAME_SETTING_KEY_SETTING_CHANGED,
        },
        readLike = false,
        needArg = true,
        Check = function(settingId)
            if not GameInstance.player.gameSettingSystem:IsKeySettingValid(settingId) then
                return true
            end
            return false
        end
    },

    GameSettingAccount = {
        sons = {
            GameSettingAccountCustomerService = true,
        },
        readLike = false,
    },

    GameSettingAccountCustomerService = {
        msgs = {
            MessageConst.ON_QUERY_UNREAD_MSG,
        },
        readLike = false,
        needArg = false,
        Check = function()
            return CS.Beyond.Gameplay.AnnouncementSystem.HasUnreadMsg()
        end
    },



    ContingencyContractTag = {
        msgs = {
            MessageConst.ON_CC_NEW_TAG_READ
        },
        readLike = true,
        needArg = true,
        Check = function(arg)
            return RedDotUtils.hasContingencyContractTagHasRedDot(arg.activityId, arg.stageId, arg.tagId)
        end
    },




    SnapshotActionItem = {
        msgs = {
            MessageConst.ON_READ_NEW_ITEM,
            MessageConst.ON_FIRST_GOT_ITEM,
        },
        readLike = true,
        needArg = true,
        Check = function(itemId)
            if string.isEmpty(itemId) then return false end
            return GameInstance.player.inventory:IsNewItem(itemId), UIConst.RED_DOT_TYPE.New
        end,
    },

    SnapshotActionTab = {
        msgs = {
            MessageConst.ON_READ_NEW_ITEM,
            MessageConst.ON_FIRST_GOT_ITEM,
            MessageConst.ON_BATTLE_SQUAD_CHANGED,
        },
        readLike = false,
        needArg = false,
        Check = function()
            local inventory = GameInstance.player.inventory
            local squadManager = GameInstance.player.squadManager
            local snapshotSystem = GameInstance.player.snapshotSystem
            local curSquad = squadManager and squadManager.curSquad
            local slots = curSquad and curSquad.slots
            if not slots or not snapshotSystem then
                return false
            end

            for i = 0, slots.Count - 1 do
                local slot = slots[i]
                if slot then
                    local entries = snapshotSystem:GetActionsForCharacter(slot.charId)
                    if entries then
                        for j = 0, entries.Count - 1 do
                            local entry = entries[j]
                            local _, cfg = Tables.snapshotActionTable:TryGetValue(entry.templateActionId)
                            local itemId = cfg and cfg.itemId
                            if not string.isEmpty(itemId) and inventory:IsNewItem(itemId) then
                                return true, UIConst.RED_DOT_TYPE.New
                            end
                        end
                    end
                end
            end
            return false
        end,
        sons = {
            SnapshotActionItem = false,
        },
    },


    FacBackupPower = {
        msgs = {
            MessageConst.FAC_BACKUP_POWER_VIEWED,
        },
        readLike = true,
        needArg = true,
        Check = function(arg)
            local chapterId = arg.chapterId
            local lastStartTs = FactoryUtils.getRegionPowerInfoByChapterId(chapterId).backupLastStartTs


            local USE_DURATION = Tables.factoryConst.facBackUpPowerDuration
            local RECOVERY_DURATION = Tables.factoryConst.facBackUpPowerCooldownTime
            local backupUseEndTs = lastStartTs + USE_DURATION
            local backupRecoveryEndTs = backupUseEndTs + RECOVERY_DURATION
            local currentTs = DateTimeUtils.GetCurrentTimestampBySeconds()
            local isUse = currentTs < backupUseEndTs
            local isRecovery = currentTs >= backupUseEndTs and currentTs < backupRecoveryEndTs
            if isUse or isRecovery then
                return false
            end

            return not LuaSystemManager.factory:IsBackupPowerViewed(chapterId)
        end
    }
}

return Config
