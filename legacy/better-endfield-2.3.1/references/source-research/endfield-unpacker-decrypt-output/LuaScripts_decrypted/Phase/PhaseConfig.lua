


























config = {

    CharInfo = {

        panels = {},
        systemId = "system_character",
        fov = 15.3818,
        redDotName = "AllCharInfo",
        disableEffectLodControl = true,
        haveSceneCamera = true,
        checkCanOpen = function(arg)
            if GameInstance.player.generalAbilitySystem.isRPressed then
                return false
            end
            return true
        end,
    },

    CharFormation = {

        panels = {},
        fov = 15.3818,
        systemId = "system_char_formation",
        disableEffectLodControl = true,
        checkCanOpen = function(arg)
            if Utils.isCurSquadAllDead() then

                return false, Language.LUA_GAME_MODE_FORBID_FACTORY_WATCH
            end
            return not Utils.isInFight(), Language.LUA_CHAR_FORMATION_IN_FIGHT
        end,
        haveSceneCamera = true,
    },

    Level = {

        panels = {},
        fov = 15.3818,
        cannotForbid = true,
        notCreateDummyNaviLayer = true,
        sceneVisible = true,
    },

    Dialog = {

        panels = {
            PanelId.DialogMask,
            PanelId.Dialog,
            PanelId.HeadLabelInDialog,
        },
        cannotForbid = true,
        sceneVisible = true,
    },

    DialogTimeline = {

        panels = {
            PanelId.DialogTimelineMask,
            PanelId.BigLogo,
            PanelId.DialogTimeline,
        },
        isSimpleUIPhase = false,
        cannotForbid = true,
        sceneVisible = true,
    },

    Watch = {

        panels = {
            PanelId.Watch,
        },
        hideOnDestroy = true,
        fov = 40,
        unlockSystemType = GEnums.UnlockSystemType.Watch,
        checkCanOpen = function(arg)
            if GameInstance.player.simulationTrainingSystem.banAllAction then
                return false, Language.LUA_GAME_MODE_FORBID_FACTORY_WATCH
            end
            return not Utils.isInThrowMode()
        end,
    },

    SimpleSystem = {

        panels = {
            PanelId.SimpleSystem
        },
        isSimpleUIPhase = true,
    },

    ManualCraft = {

        panels = {
            PanelId.ManualCraft
        },
        unlockSystemType = GEnums.UnlockSystemType.ManualCraft,
        isSimpleUIPhase = false,
        checkCanOpen = function(arg)
            if arg and arg.showPopup and arg.itemId and GameInstance.player.facManualCraft:GetItemAccumulateCount(arg.itemId) <= 0 then
                return false, Language.LUA_MANUAL_CRAFT_JUMP_FAIL_ITEM_LOCKED
            end
            return true
        end
    },

    ManualCraftPopups = {

        panels = {
            PanelId.ManualCraftPopups
        },
        unlockSystemType = GEnums.UnlockSystemType.ManualCraft,
        isSimpleUIPhase = true,
    },

    ManualcraftUpgradePopup = {

        panels = {
            PanelId.ManualcraftUpgradePopup,
        },
        isSimpleUIPhase = true,
    },

    RiftDetailInfo = {

        panels = {
            PanelId.RiftDetailInfo,
        },
        isSimpleUIPhase = true,
    },

    Mission = {

        panels = {
            PanelId.Mission,
        },
        systemId = "system_mission",
        isSimpleUIPhase = true,
    },

    Cinematic = {

        panels = {
            PanelId.Cinematic,
            PanelId.BigLogo,
        },
        cannotForbid = true,
        sceneVisible = true,
    },

    WeaponInfo = {

        panels = {},
        fov = 15.3818,
        unlockSystemType = GEnums.UnlockSystemType.Weapon,
        disableEffectLodControl = true,
        haveSceneCamera = true,
    },

    Map = {

        panels = {},
        unlockSystemType = GEnums.UnlockSystemType.Map,
        checkCanOpen = function(arg)
            if GameInstance.player.simulationTrainingSystem.banAllAction then
                return false, Language.LUA_GAME_MODE_FORBID_FACTORY_WATCH
            end
            return MapUtils.checkCanOpenMapAndParseArgs(arg)
        end,
    },

    RegionMap = {

        panels = {},
        fov = 40,
        unlockSystemType = GEnums.UnlockSystemType.Map,
        checkCanOpen = function(arg)
            if GameInstance.player.simulationTrainingSystem.banAllAction then
                return false, Language.LUA_GAME_MODE_FORBID_FACTORY_WATCH
            end
            if Utils.isCurSquadAllDead() then

                return false, Language.LUA_GAME_MODE_FORBID_FACTORY_WATCH
            end
            return true
        end,
    },

    ValuableDepot = {

        panels = {
            PanelId.ValuableDepot,
        },
        systemId = "system_valuable_depot",
        isSimpleUIPhase = true,
        redDotName = "ValuableDepot",
    },

    Mail = {

        panels = {
            PanelId.Mail,
        },
        systemId = "system_mail",
        isSimpleUIPhase = true,
    },

    RepairInteractive = {

        panels = {
            PanelId.RepairInteractive,
        },
        isSimpleUIPhase = true,
    },

    Puzzle = {

        panels = {
            PanelId.Puzzle,
        },
        isSimpleUIPhase = true,
    },

    PuzzleTrackPopup = {

        panels = {
            PanelId.PuzzleTrackPopup,
        },
        isSimpleUIPhase = true,
    },

    Inventory = {

        panels = {},
        systemId = "system_inventory",
        checkCanOpen = function(arg)
            if Utils.isInFight() then
                return false, Language.LUA_CANT_OPEN_INVENTORY_IN_FIGHT
            end
            if Utils.isInThrowMode() then
                return false, Language.LUA_CANT_OPEN_INVENTORY_IN_THROW_MODE
            end
            return true
        end
    },

    ChangePortableDevice = {
        panels = {
            PanelId.ChangePortableDevice,
        },
        isSimpleUIPhase = true,
    },

    Wiki = {

        panels = {},
        systemId = "system_wiki",
        disableEffectLodControl = true,
        haveSceneCamera = true,
        checkCanOpen = function(arg)
            if GameInstance.player.spaceship.isViewingFriend then
                return false, Language.LUA_FRIEND_SOCIAL_BUILDING_JUMP_SPACESHIP_TIP
            end
            return true
        end
    },

    Shop = {

        panels = {
            PanelId.Shop,
        },
        isSimpleUIPhase = false,
    },

    ShopTrade = {

        panels = {
            PanelId.ShopTrade,
        },
        unlockSystemType = GEnums.UnlockSystemType.DomainShop,
        checkCanOpen = function(arg)

            if Utils.isInBlackbox() then
                return false
            end

            if string.isEmpty(arg.domainId) then
                return arg.friendRoleId ~= nil
            end

            local domainId = arg.domainId
            return DomainPOIUtils.checkCanOpenDomainShop(domainId), Language.LUA_DOMAIN_DEVELOPMENT_POI_UNLOCK_CLICK_TOAST
        end,
        isSimpleUIPhase = false,
    },

    ShopCreditPointsPopUp = {

        panels = {
            PanelId.ShopCreditPointsPopUp,
        },
        isSimpleUIPhase = true,
    },

    GameSetting = {

        panels = {
            PanelId.GameSetting,
        },
        isSimpleUIPhase = false,
        redDotName = "GameSetting",
    },

    FacMachine = {

        panels = {},
    },

    FacHUBData = {

        panels = {
            PanelId.FacHUBData,
        },
        systemId = "system_hub_data",
        isSimpleUIPhase = true,
    },

    FacDepotSwitching = {

        panels = {
            PanelId.FacDepotSwitching,
        },
        isSimpleUIPhase = true,
    },

    FacRegionUpgrade = {

        panels = {},
        isSimpleUIPhase = false,
        sceneVisible = true,
    },

    FacBuildListSelect = {

        panels = {
            PanelId.FacBuildListSelect,
        },
        isSimpleUIPhase = false,
        checkCanOpen = function(arg)

            if arg ~= nil and (arg.onlyCraftNode ~= nil or arg.bluePrintData ~= nil) then
                if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacHub) then
                    return false, Language.LUA_BUILDLIST_HUB_UNLOCK_TIPS
                end
                if Utils.isInBlackbox() then
                    local blackboxEnableBuildingCraft = GameWorld.worldInfo.curLevel.levelData.blackbox.buildingCraft
                    if not blackboxEnableBuildingCraft then
                        return false, Language.LUA_BLACKBOX_PENDING_CANNOT_CRAFT
                    end
                end
            end
            if GameInstance.player.spaceship.isViewingFriend then
                return false, Language.LUA_FRIEND_SOCIAL_BUILDING_JUMP_SPACESHIP_TIP
            end
            return true
        end
    },
    FacDecoObtainWays = {

        checkCanOpen = function(arg)
            local result, text = PhaseManager:CheckCanOpenPhase(PhaseId.FacBuildListSelect)
            return result, text
        end,
        panels = {
            PanelId.FacDecoObtainWays,
        },
        isSimpleUIPhase = true,
    },

    FacTechTree = {

        checkCanOpen = function(arg)
            return FactoryUtils.checkCanOpenPhaseFacTechTree(arg)
        end,
        systemId = "system_tech_tree",
        isSimpleUIPhase = false,
    },

    FacFertilization = {

        panels = {
            PanelId.FacFertilization,
        },
        isSimpleUIPhase = true,
    },

    FacBlueprint = {

        panels = {
            PanelId.FacBlueprint,
        },
        isSimpleUIPhase = true,
        checkCanOpen = function()
            local isSystemUnlocked = Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacBlueprint)
            if Utils.isInBlackbox() then
                return isSystemUnlocked and GameInstance.player.remoteFactory.blueprint.presetBlueprints.Count > 0
            else
                return isSystemUnlocked
            end
        end
    },

    RemoteComm = {

        panels = {
            PanelId.RemoteCommBG,
            PanelId.RemoteComm,
            PanelId.RemoteCommHud,
        },
        fov = 15.3818,
    },

    LostAndFound = {

        panels = {
            PanelId.LostAndFound,
        },
        isSimpleUIPhase = true,
    },

    SNS = {

        panels = { },
        redDotName = "SNSWatchEntry",
        isSimpleUIPhase = false,
        unlockSystemType = GEnums.UnlockSystemType.SNS,
        checkCanOpen = function(arg)
            if Utils.isForbidden(ForbidType.HideSNSHud) then
                return false, Language.LUA_OBTAIN_WAYS_JUMP_BLOCKED
            end

            if arg and arg.dialogId and
                    not GameInstance.player.sns.dialogInfoDic:ContainsKey(arg.dialogId) then
                return false, Language.LUA_SNS_OPEN_PANEL_WITH_INVALID_ID_DESC
            end

            return true
        end,
        systemId = "system_sns",
    },

    SNSBarkerSide = {

        panels = {
            PanelId.SNSBarkerSide,
        },
        isSimpleUIPhase = true,
    },

    CharJoinToast = {

        panels = {
            PanelId.CharJoinToast,
        },
        isSimpleUIPhase = true,
    },

    SettlementMain = {

        panels = {
            PanelId.SettlementMain,
        },
        systemId = "system_settlement",
        isSimpleUIPhase = true,
        unlockSystemType = GEnums.UnlockSystemType.Settlement,
        checkCanOpen = function(args)
            local domainId, _ = DomainPOIUtils.resolveOpenSettlementArgs(args)

            local domainDevelopmentSystem = GameInstance.player.domainDevelopmentSystem
            local hasData = domainDevelopmentSystem.domainDevDataDic:TryGetValue(domainId)
            local hasCfg, domainCfg = Tables.domainDataTable:TryGetValue(domainId)
            if not hasCfg or not hasData then
                return false, Language.LUA_DOMAIN_DEVELOPMENT_POI_UNLOCK_CLICK_TOAST
            end

            for _, stlId in pairs(domainCfg.settlementGroup) do
                if GameInstance.player.settlementSystem:IsSettlementUnlocked(stlId) then
                    return true
                end
            end
            return false, Language.LUA_DOMAIN_DEVELOPMENT_POI_UNLOCK_CLICK_TOAST
        end,
    },

    SettlementChar = {

        panels = {
            PanelId.SettlementChar,
        },
        systemId = "system_settlement",
        isSimpleUIPhase = true,
    },

    SettlementCommodity = {

        panels = {
            PanelId.SettlementCommodity,
        },
        systemId = "system_settlement",
        isSimpleUIPhase = true,
    },

    SettlementDefenseRewardsInfo = {

        panels = {
            PanelId.SettlementDefenseRewardsInfo,
        },
        isSimpleUIPhase = true,
    },

    SettlementDefenseTransit = {

        panels = {
            PanelId.SettlementDefenseTransit,
        },
        isSimpleUIPhase = true,
    },

    SettlementDefenseMainMap = {

        panels = {
            PanelId.SettlementDefenseMainMap,
        },
        isSimpleUIPhase = true,
    },

    SettlementDefenseTerminal = {

        panels = {
            PanelId.SettlementDefenseTerminal,
        },
        isSimpleUIPhase = true,
    },

    SettlementDefenseFinish = {

        panels = {
            PanelId.SettlementDefenseFinish,
        },
        isSimpleUIPhase = true,
    },

    SettlementDefenseFinishFail = {

        panels = {
            PanelId.SettlementDefenseFinishFail,
        },
        isSimpleUIPhase = true,
    },

    SpaceshipStation = {

        panels = {
            PanelId.SpaceshipStation,
        },
        isSimpleUIPhase = true,
    },

    SpaceshipManufacturingStation = {

        panels = {
            PanelId.SpaceshipManufacturingStation,
        },
        isSimpleUIPhase = true,
    },

    SpaceshipRoomUpgrade = {

        panels = {
            PanelId.SpaceshipRoomUpgrade,
        },
        isSimpleUIPhase = true,
    },

    SpaceshipControlCenter = {

        panels = {
            PanelId.SpaceshipControlCenterRoom,
            PanelId.SpaceshipControlCenter,
        },
        fov = 15.3818,
        isSimpleUIPhase = false,
        unlockSystemType = GEnums.UnlockSystemType.SpaceshipControlCenter,
        redDotName = "SSControlCenter",
    },

    SpaceshipCollectHintInfo = {

        panels = {
            PanelId.SpaceshipCollectHintInfo,
        },
        isSimpleUIPhase = true,
    },

    SpaceshipGrowCabin = {

        panels = {
            PanelId.SpaceshipGrowCabin,
        },
        isSimpleUIPhase = true,
    },

    SpaceshipShop = {

        panels = {
            PanelId.SpaceshipShop,
        },
        isSimpleUIPhase = true,
    },

    SpaceshipVisitor = {

        panels = {
            PanelId.SpaceshipVisitor,
        },
        isSimpleUIPhase = true,
    },
    SpaceshipRoomClueSchedule = {

        panels = {
            PanelId.SpaceshipRoomClueSchedule,
        },
        isSimpleUIPhase = true,
    },
    SpaceshipRoomClueSettlement = {

        panels = {
            PanelId.SpaceshipRoomClueSettlement,
        },
        isSimpleUIPhase = true,
    },
    SpaceshipRoomClueGift = {

        panels = {
            PanelId.SpaceshipRoomClueGift,
        },
        isSimpleUIPhase = true,
    },
    ActivityFoodSubmit = {

        panels = {
            PanelId.ActivityFoodSubmit,
        },
        isSimpleUIPhase = true,
    },
    ActivityFoodSubmitNotes = {

        panels = {
            PanelId.ActivityFoodSubmitNotes,
        },
        isSimpleUIPhase = true,
    },

    SpaceshipSalesRecords = {

        panels = {
            PanelId.SpaceshipSalesRecords,
        },
        checkCanOpen = function(arg)
            local guestRoomBuild = GameInstance.player.spaceship:IsRoomBuild(Tables.spaceshipConst.guestRoomId)
            return guestRoomBuild, Language.LUA_SPACESHIP_LOCKED_TOAST
        end,
        isSimpleUIPhase = true,
    },

    SpaceshipDailyReport = {

        panels = {
            PanelId.SpaceshipDailyReport,
        },
        isSimpleUIPhase = true,
    },

    SpaceshipCollectionBooth = {

        panels = {
            PanelId.SpaceshipCollectionBooth,
        },
        isSimpleUIPhase = true,
    },

    SpaceshipReceptionDisplay = {

        panels = {
            PanelId.SpaceshipReceptionDisplay,
        },
        isSimpleUIPhase = true,
    },

    SSReceptionRoomCharPoster = {

        panels = {
            PanelId.SSReceptionRoomCharPoster,
        },
        isSimpleUIPhase = true,
    },

    SSReceptionRoomWeaponPoster = {

        panels = {
            PanelId.SSReceptionRoomWeaponPoster,
        },
        isSimpleUIPhase = true,
    },

    LiquidPool = {

        panels = {
            PanelId.LiquidPool,
        },
        isSimpleUIPhase = true,
    },

    PowerPoleFastTravel = {

        panels = {
        },
        isSimpleUIPhase = false,
    },

    SubmitItem = {

        panels = {
            PanelId.SubmitItem,
        },
        isSimpleUIPhase = true,
    },

    FriendShipPresent = {

        panels = {
            PanelId.FriendShipPresent,
        },
        isSimpleUIPhase = true,
    },

    RacingDungeonEntry = {

        panels = {
            PanelId.RacingDungeonEntry,
        },
        isSimpleUIPhase = true,
    },

    RacingDungeonEffect = {

        panels = {
            PanelId.RacingDungeonEffect,
        },
        isSimpleUIPhase = true,
        unlockSystemType = GEnums.UnlockSystemType.RacingDungeon,
    },

    AdventureReward = {

        panels = {
            PanelId.AdventureReward,
        },
        isSimpleUIPhase = true,
        fov = 4,
    },

    AdventureBook = {

        panels = {},
        redDotName = "AdventureBook",
        isSimpleUIPhase = false,
        unlockSystemType = GEnums.UnlockSystemType.AdventureBook,
        systemId = "system_adventure_book",
    },

    Reading = {

        panels = {
            PanelId.Reading,
        },
        isSimpleUIPhase = true,
    },

    ReadingPopUp = {

        panels = {},
        isSimpleUIPhase = false,
    },

    BlackboxEntry = {

        panels = {
            PanelId.BlackboxEntry,
        },
        isSimpleUIPhase = true,
    },

    GenderSelect = {

        isSimpleUIPhase = false,
        fov = 15.3818,
        sceneVisible = true,

    },

    DungeonEntry = {

        panels = {},
        checkCanOpen = function(args)
            return DungeonUtils.checkCanOpenPhase(args)
        end,
        isSimpleUIPhase = false,
    },

    DungeonTrainOverview = {

        panels = {
            PanelId.DungeonTrainOverview,
        },
        isSimpleUIPhase = true,
    },

    GachaPool = {

        panels = {
            PanelId.GachaPool,
        },
        isSimpleUIPhase = false,
        systemId = "system_gacha",
        redDotName = "Gacha",
        checkCanOpen = function(arg)
            return (not GameInstance.player.gameSettingSystem.forbiddenGacha), Language.LUA_SWITCH_TYPE_FORBIDDEN_TOAST
        end
    },

    GachaChar = {

        panels = {},
        isSimpleUIPhase = false,
        disableEffectLodControl = true,
        fov = 15.3818,
        haveSceneCamera = true,
    },

    GachaDropBin = {

        panels = {},
        isSimpleUIPhase = false,
        unlockSystemType = GEnums.UnlockSystemType.Gacha,
        disableEffectLodControl = true,
        haveSceneCamera = true,
    },

    GachaWeaponPreheat = {

        panels = {},
        isSimpleUIPhase = false,
        disableEffectLodControl = true,
        haveSceneCamera = true,
    },

    GachaWeapon = {

        panels = {},
        isSimpleUIPhase = false,
        disableEffectLodControl = true,
        haveSceneCamera = true,
    },

    GachaWeaponResult = {

        panels = {
            PanelId.GachaWeaponResult,
        },
        fov = 30,
        isSimpleUIPhase = false,
    },

    GachaWeaponPool = {

        panels = {
            PanelId.GachaWeaponPool,
        },
        isSimpleUIPhase = false,
        checkCanOpen = function(arg)
            return (not GameInstance.player.gameSettingSystem.forbiddenGacha), Language.LUA_SWITCH_TYPE_FORBIDDEN_TOAST
        end
    },

    DeathInfo = {

        panels = {
           PanelId.DeathInfo,
        },
        isSimpleUIPhase = true,
    },

    PlayerRename = {

        panels = {
           PanelId.PlayerRename,
        },
        isSimpleUIPhase = true,
    },

    PRTS = {

        panels = {
            PanelId.PRTSMain,
        },
        redDotName = "PRTSWatch",
        unlockSystemType = GEnums.UnlockSystemType.PRTS,
        isSimpleUIPhase = true,
    },

    PRTSInvestigateGallery = {

        panels = {
            PanelId.PRTSInvestigateGallery,
        },
        isSimpleUIPhase = true,
    },

    PRTSInvestigateDetail = {

        panels = {
            PanelId.PRTSInvestigateDetail,
        },
        isSimpleUIPhase = true,
        unlockSystemType = GEnums.UnlockSystemType.PRTS,
        checkCanOpen = function(arg)
            if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.PRTS) then
                return false, "PRTS System Not Unlock"
            end
            return (arg ~= nil and not string.isEmpty(arg.id)), "[PRTSInvestigateDetailCtrl] arg or arg.id is nil!"
        end,
    },

    PRTSStoryCollGallery = {

        panels = {
           PanelId.PRTSStoryCollGallery,
        },
        isSimpleUIPhase = true,
        checkCanOpen = function(arg)
            return (arg ~= nil and not string.isEmpty(arg.pageType)), "[PRTSStoryCollGalleryCtrl] arg or arg.pageType is nil!"
        end,
    },

    PRTSStoryCollDetail = {

        panels = {
            PanelId.PRTSStoryCollDetail,
        },
        isSimpleUIPhase = true,
        unlockSystemType = GEnums.UnlockSystemType.PRTS,
        checkCanOpen = function(arg)
            if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.PRTS) then
                return false, "PRTS System Not Unlock"
            end
            local canShow = arg ~= nil and (arg.id or arg.idList and #arg.idList > 0)
            return canShow, "[PRTSStoryCollDetailCtrl] arg is illegal!"
        end,
    },

    PRTSInvestigateReport = {

        panels = {
            PanelId.PRTSInvestigateReport,
        },
        isSimpleUIPhase = true,
        unlockSystemType = GEnums.UnlockSystemType.PRTS,
        checkCanOpen = function(arg)
            local canOpen = arg ~= nil and arg.storyCollId and Tables.prtsAllItem:ContainsKey(arg.storyCollId)
            return canOpen, "[PRTSInvestigateReportCtrl] arg or arg.storyCollId is missing!"
        end,
    },

    SceneGradeDifferenceItemPopUp = {

        panels = {
           PanelId.SceneGradeDifferenceItemPopUp,
        },
        isSimpleUIPhase = true,
    },

    UsableItemChest = {

        panels = {
           PanelId.UsableItemChest,
        },
        isSimpleUIPhase = true,
    },

    GemRecast = {

        panels = {
              PanelId.GemRecast,
        },
        isSimpleUIPhase = true,
    },

    GemEnhance = {

        panels = {
            PanelId.GemEnhance,
        },
        unlockSystemType = GEnums.UnlockSystemType.GemEnhance,
        isSimpleUIPhase = true,
    },

    GemCustomization = {

        panels = {
            PanelId.GemCustomization,
        },
        isSimpleUIPhase = true,
    },

    SubmitCollection = {

        panels = {
            PanelId.SubmitCollection,
        },
        isSimpleUIPhase = false,
    },

    SimulationTraining = {
        panels = {
            PanelId.SimulationTraining,
        },
        isSimpleUIPhase = true,
    },

    SimulationTrainingExplain = {
        panels = {
            PanelId.SimulationTrainingExplain,
        },
        isSimpleUIPhase = true,
    },

    SimulationTrainingTask = {

        panels = {
           PanelId.SimulationTrainingTask,
        },
        isSimpleUIPhase = true,
    },

    CharacterSummon = {

        panels = {
            PanelId.CharacterSummon,
        },
        isSimpleUIPhase = true,
    },

    AreaBuffPopup = {

        panels = {
            PanelId.AreaBuffPopup,
        },
        isSimpleUIPhase = true,
    },

    CommonMoneyExchange = {

        panels = {
            PanelId.CommonMoneyExchange,
        },
        isSimpleUIPhase = true,
    },

    EndingToast = {

        panels = {
           PanelId.EndingToast,
        },
        isSimpleUIPhase = true,
    },

    LeadingCharacter = {

        panels = {
           PanelId.LeadingCharacter,
        },
        isSimpleUIPhase = true,
    },

    FriendlyTips = {

        panels = {
           PanelId.FriendlyTips,
        },
        isSimpleUIPhase = true,
    },

    Snapshot = {

        panels = {},
        isSimpleUIPhase = false,
    },

    Friend = {

        panels = {},
        isSimpleUIPhase = false,
        systemId = 'system_friend',
        redDotName = "Friend",
    },

    FriendBusinessCardPreview = {

        panels = {
            PanelId.FriendBusinessCardPreview,
        },
        isSimpleUIPhase = true,
    },

    FriendThemeChange = {

        panels = {
            PanelId.FriendThemeChange,
        },
        isSimpleUIPhase = true,
    },

    DomainMain = {

        panels = {
            PanelId.DomainMain,
        },
        unlockSystemType = GEnums.UnlockSystemType.DomainDevelopment,
        redDotName = "DomainEntry",
        systemId = "system_domain_development",
        checkCanOpen = function(args)
            if GameInstance.player.simulationTrainingSystem.banAllAction then
                return false, Language.LUA_GAME_MODE_FORBID_FACTORY_WATCH
            end
            local domainId = args and args.domainId or Utils.getCurDomainId()

            local domainDevelopmentSystem = GameInstance.player.domainDevelopmentSystem
            local hasData = domainDevelopmentSystem.domainDevDataDic:TryGetValue(domainId)
            return hasData
        end,
    },

    DomainPOIOverview = {
        panels = {
            PanelId.DomainPOIOverview,
        },
        isSimpleUIPhase = true,
    },

    DomainItemTransfer = {

        panels = {
            PanelId.DomainItemTransfer,
        },
        isSimpleUIPhase = true,
        checkCanOpen = function(arg)
            return GameInstance.player.remoteFactory:IsFacTransEntryUnlocked()
        end,
    },

    DomainGrade = {

        panels = {
           PanelId.DomainGrade,
        },
        isSimpleUIPhase = true,
        checkCanOpen = function(arg)
            local _, domainId = DomainPOIUtils.resolveOpenGradeArgs(arg)
            local hasValue, _ = GameInstance.player.domainDevelopmentSystem.domainDevDataDic:TryGetValue(domainId)
            return hasValue
        end,
    },

    DomainDepotPackage = {

        panels = {},
        isSimpleUIPhase = false,
        unlockSystemType = GEnums.UnlockSystemType.DomainDevelopmentDomainDepot,
        checkCanOpen = function(arg)
            return DomainPOIUtils.checkCanOpenDomainDepot(arg.domainId), Language.LUA_DOMAIN_DEPOT_NOT_UNLOCK_TIPS
        end,
    },

    DramaticPerformanceBag = {

        panels = {},
        isSimpleUIPhase = false,
    },

    GenderChange = {

        panels = {},
        isSimpleUIPhase = false,
    },

    CommonPOIUpgrade = {

        panels = {
           PanelId.CommonPOIUpgrade,
        },
        isSimpleUIPhase = false,
    },

    EquipTech = {

        panels = {
           PanelId.EquipTech,
        },
        unlockSystemType = GEnums.UnlockSystemType.EquipProduce,
        isSimpleUIPhase = true,
        redDotName = "EquipTech",
        checkCanOpen = function(arg)
            if Utils.isInBlackbox() then
                local curSceneInfo = GameInstance.remoteFactoryManager.currentSceneInfo
                return CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryUtil.IsHubEquipCraftEnabledInBlackbox(curSceneInfo)
            else
                return true
            end
        end,
    },

    KiteStation = {

        panels = {
           PanelId.KiteStation,
        },
        isSimpleUIPhase = false,
        unlockSystemType = GEnums.UnlockSystemType.KiteStation,
    },

    WorldLevelPopup = {

        panels = {
           PanelId.WorldLevelTipsPopup,
        },
        isSimpleUIPhase = false,
    },

    ActivityCenter = {

        panels = {
            PanelId.ActivityCenterEmptyBottom,
            PanelId.ActivityCenter,
        },
        fov = 40,
        isSimpleUIPhase = false,
        redDotName = "ActivityCenter",
        systemId = "system_activity_center",
        checkCanOpen = function(arg)

            local activities = GameInstance.player.activitySystem:GetAllActivities()
            if activities.Count == 0 then
                return false, Language.LUA_ACTIVITY_NONE_ACTIVITY_EXIST
            end

            if arg and arg.gotoCenter and arg.activityId then
                if not GameInstance.player.activitySystem:GetActivity(arg.activityId) then
                    return false, Language.LUA_ACTIVITY_FORBIDDEN
                end
            end
            return true
        end
    },

    ActivityPopup = {

        panels = {
        },
        isSimpleUIPhase = false,
        redDotName = 'ActivityCheckIn',
        unlockSystemType = GEnums.UnlockSystemType.Activity,
        isUnlocked = function()
            return Utils.isSystemUnlocked(GEnums.UnlockSystemType.Activity)
        end,
        checkCanOpen = function(args)
            local popupIds = ActivityUtils.getPopUpIds()
            if BEYOND_DEBUG_COMMAND and args and args.manuallyPopup then
                return true
            else
                return #popupIds > 0
            end
        end
    },

    SnapshotChallenge = {

        panels = {
            PanelId.SnapshotChallenge,
        },
        isSimpleUIPhase = false,
        checkCanOpen = function(arg)

            if arg and arg.activityId then
                if not GameInstance.player.activitySystem:GetActivity(arg.activityId) then
                    return false, Language.LUA_ACTIVITY_FORBIDDEN
                end
            end
            return true
        end
    },

    ChallengeDungeon = {

        panels = {
            PanelId.ChallengeDungeon,
        },
        isSimpleUIPhase = true,
        checkCanOpen = function(arg)

            if arg and arg.activityId then
                if not GameInstance.player.activitySystem:GetActivity(arg.activityId) then
                    return false, Language.LUA_ACTIVITY_FORBIDDEN
                end
            end
            return true
        end
    },

    AchievementMain = {

        panels = {
           PanelId.AchievementMain,
        },
        isSimpleUIPhase = true,
        unlockSystemType = GEnums.UnlockSystemType.Achievement,
    },

    DungeonWeeklyRaid = {

        panels = {

        },
        isSimpleUIPhase = false,
        unlockSystemType = GEnums.UnlockSystemType.WeekRaid,
    },

    PresetTeamSwitch = {

        panels = {
           PanelId.PresetTeamSwitch,
        },
        isSimpleUIPhase = false,
    },

    ShopEntry = {

        panels = {
            PanelId.ShopEntry,
        },
        isSimpleUIPhase = true,
    },

    CashShop = {

        panels = {},
        isSimpleUIPhase = false,
        systemId = "system_cash_shop",
        fov = 40,
        checkCanOpen = function(arg)
            if GameInstance.player.gameSettingSystem.forbiddenCashShop then
                return false, Language.LUA_SWITCH_TYPE_FORBIDDEN_TOAST
            end
            local ret, toast = CashShopUtils.CheckCanOpenPhase(arg)
            return ret, toast
        end
    },

    ShopMonthlyPassPopUp = {

        panels = {


        },
        isSimpleUIPhase = false,
    },

    DramaticPerformanceEmpty = {

        panels = {
           PanelId.DramaticPerformanceEmpty,
        },
        isSimpleUIPhase = true,
    },

    WorldEnergyPointEntry = {

        panels = {
            PanelId.WorldEnergyPointEntry,
        },
        isSimpleUIPhase = false,
    },

    WorldEnergyPointCustomReward = {

        panels = {
            PanelId.WorldEnergyPointCustomReward,
        },
        isSimpleUIPhase = true,
    },

    WorldEnergyPointSettlement = {

        panels = {
            PanelId.WorldEnergyPointSettlement,
        },
        isSimpleUIPhase = true,
    },

    DungeonCustomReward = {

        panels = {
           PanelId.DungeonCustomReward,
        },
        isSimpleUIPhase = true,
    },

    BattlePass = {

        panels = {
        },
        isSimpleUIPhase = false,
        redDotName = 'BattlePass',
        unlockSystemType = GEnums.UnlockSystemType.BPSystem,
        checkCanOpen = function()
            if GameInstance.player.gameSettingSystem.forbiddenBp then
                return false, Language.LUA_SWITCH_TYPE_FORBIDDEN_TOAST
            end
            return BattlePassUtils.CheckBattlePassSeasonValid(), Language.LUA_BATTLEPASS_CANNOT_OPEN_TOAST
        end,
    },

    HighDifficultyMainHud = {

        panels = {
            PanelId.HighDifficultyMainHud,
        },
        isSimpleUIPhase = true,
        isUnlocked = function()
            return GameInstance.player.highDifficultySystem:IsHighDifficultyUnlocked()
        end,
    },
    GachaLauncher = {

        panels = {},
        isSimpleUIPhase = false,
        unlockSystemType = GEnums.UnlockSystemType.Gacha,
        disableEffectLodControl = true,
        haveSceneCamera = true,
    },
    SpaceshipGuestRoomClue = {

        panels = {
           PanelId.SpaceshipGuestRoomClue,
        },
        isSimpleUIPhase = true,
    },

    BattlePassBuyLevel = {

        panels = {
            PanelId.BattlePassBuyLevel,
        },
        isSimpleUIPhase = true,
    },

    BattlePassBuyPlan = {

        panels = {
            PanelId.BattlePassBuyPlan,
        },
        isSimpleUIPhase = true,
    },

    BattlePassAdvancedPlanBuy = {

        panels = {
            PanelId.BattlePassAdvancedPlanBuy,
        },
        isSimpleUIPhase = false,
    },
    ActivityCleaningDetails = {

        panels = {
           PanelId.ActivityCleaningDetails,
        },
        isSimpleUIPhase = true,
    },
    ActivityRanking = {

        panels = {
           PanelId.ActivityRanking,
        },
        isSimpleUIPhase = true,
    },
    DialogSign = {

        panels = {
           PanelId.DialogSign,
        },
        isSimpleUIPhase = true,
    },
    ContingencySelectTag = {

        panels = {
           PanelId.ContingencySelectTag,
        },
        isSimpleUIPhase = true,
    },
    ContingencyContractTask = {

        panels = {
           PanelId.ContingencyContractTask,
        },
        isSimpleUIPhase = true,
    },
    ContingencyContractDetailsPopup = {

        panels = {
            PanelId.ContingencyContractDetailsPopup,
        },
        isSimpleUIPhase = false,
    },
    StaminaPopUp = {
        panels = {
            PanelId.StaminaPopUp,
        },
        isSimpleUIPhase = true,
    },
    Dialog2D = {
        panels = {
           PanelId.Dialog2D,
        },
        isSimpleUIPhase = true,
    },
    FacReservePowerPopup = {

        panels = {
            PanelId.FacReservePowerPopup
        },
        isSimpleUIPhase = true,
    },

    FriendHeadSelectedPopUp = {

        panels = {
            PanelId.FriendHeadSelectedPopUp
        },
        isSimpleUIPhase = true,
    },


    GemWishlist = {

        panels = {
           PanelId.GemWishlist,
        },
        isSimpleUIPhase = true,
        unlockSystemType = GEnums.UnlockSystemType.WeaponWishList,
    },




    GasCollection = {
        panels = {
            PanelId.GasCollection,
        },
        isSimpleUIPhase = true,
    },
    BalloonInteractive = {
        panels = {
            PanelId.BalloonInteractive,
        },
        isSimpleUIPhase = true,
    },
    Balloon = {
        panels = {
            PanelId.BalloonMiniGame,
            PanelId.BalloonMiniGameRotatePlate,
        },
        isSimpleUIPhase = false,
        unlockSystemType = GEnums.UnlockSystemType.MinigameBalloon,
        fov = 90,
    },
    MusicPlayer = {
        panels = {
            PanelId.MusicPlayer,
        },
        isSimpleUIPhase = false,
    },
    SeasonTowerMainHud = {
        panels = {
            PanelId.SeasonTowerMainHud,
        },
        isSimpleUIPhase = true,
        checkCanOpen = function(arg)
            if GameInstance.player.gameSettingSystem.forbiddenSeasonTower then
                return false, Language.LUA_SWITCH_TYPE_FORBIDDEN_TOAST
            end
            if GameInstance.player.seasonTowerSystem.currentSeasonId <= 0 then
                return false, Language.LUA_SWITCH_TYPE_FORBIDDEN_TOAST
            end
            return true
        end,
    },
    SeasonTowerSucc = {

        panels = {
            PanelId.SeasonTowerSucc,
        },
        isSimpleUIPhase = true,
    },
    SeasonTowerAchieve = {
        panels = {
            PanelId.SeasonTowerAchieve,
        },
        isSimpleUIPhase = true,
    },
    SeasonTowerDungeonEntry = {
        panels = {
            PanelId.SeasonTowerDungeonEntry,
        },
        isSimpleUIPhase = true,
        checkCanOpen = function(arg)
            if SeasonTowerUtils.getShouldRefresh() then
                return false
            end
            return true
        end,
    },



    ActivityCoinMilestone = {

        panels = {
           PanelId.ActivityCoinMilestone,
        },
        isSimpleUIPhase = true,
    },


    ActivityImportantPopup = {

        panels = {
           PanelId.ActivityImportantPopup,
        },
        isSimpleUIPhase = false,
    },


    ActivityCoinRanking = {

        panels = {
           PanelId.ActivityCoinRanking,
        },
        isSimpleUIPhase = true,
    },


    ActivityCoinReward = {

        panels = {
           PanelId.ActivityCoinReward,
        },
        isSimpleUIPhase = true,
    },







    SeasonTowerAllScore = {

        panels = {
           PanelId.SeasonTowerAllScore,
        },
        isSimpleUIPhase = true,
    },


    SeasonTowerScoreReview = {

        panels = {
           PanelId.SeasonTowerScoreReview,
        },
        isSimpleUIPhase = true,
    },





    ReflowPopup = {

        panels = {

        },
        isSimpleUIPhase = false,
    },

    ReflowFormalReconnect = {

        panels = {

        },
        isSimpleUIPhase = false,
    },





    ShopStar = {

        panels = {
            PanelId.ShopStar,
        },
        isSimpleUIPhase = false,
        unlockSystemType = GEnums.UnlockSystemType.TrstarShop,
        redDotName = "StarShop",
    },





    DungeonCustomEntry = {

        panels = {
           PanelId.DungeonCustomEntry,
        },
        isSimpleUIPhase = true,
    },




}
