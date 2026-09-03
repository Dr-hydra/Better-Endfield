local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local BannerWidget = require_ex('UI/Panels/Watch/BannerWidget')
local PANEL_ID = PanelId.Watch
WatchCtrl = HL.Class('WatchCtrl', uiCtrl.UICtrl)







WatchCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_GOLD_CHANGE] = 'RefreshCurrency',
    [MessageConst.ON_BLOC_TOKEN_CHANGE] = 'RefreshCurrency',
    [MessageConst.ON_WORLD_LEVEL_CHANGED] = 'RefreshWorldLevel',
    [MessageConst.ON_MISSION_STATE_CHANGE] = 'RefreshWorldLevel',
    [MessageConst.ON_WALLET_CHANGED] = 'OnWalletChanged',
    [MessageConst.ON_SYSTEM_UNLOCK_CHANGED] = '_OnSystemUnlock',
    [MessageConst.ON_FRIEND_BUSINESS_INFO_CHANGE] = 'OnFriendBusinessInfoChange',
}


local BTN_CONST = {
    LEFT = {
        CHAR_INFO = 101,
        ACTIVITY = 102,
        GACHA = 103,
        SHOP = 104,
    },
    RIGHT = {
        ADVENTURE_BOOK = 11,
        BATTLE_PASS = 12,
        DOMAIN = 21,
        FRIEND = 22,
        EQUIP_PRODUCER = 31,
        CHAR_FORMATION = 32,
        WIKI = 41,
        VALUABLE_INVENTORY = 42,
        STAR_SHOP = 51,
        NARRATE = 52,
        INVENTORY = 61,
        ACHIEVEMENT = 62,
        GEM_ENHANCE = 71,
        MISSION = 72,
        MAP = 81,
        SNS = 82,
        QUESTIONNAIRE = 91,
        GAME_TOOL = 92,
    },
    CENTER = {
        SETTING = 201,
        MAIL = 202,
        ANNOUNCEMENT = 203,
        FAC_TECH_TREE = 204,
        FAC_HUB_DATA = 205,
        FAC_CHAR_SET = 206,
    }
}

local RIGHT_BTN_ORDER = {}
for _, key in pairs(BTN_CONST.RIGHT) do
    table.insert(RIGHT_BTN_ORDER, key)
end
table.sort(RIGHT_BTN_ORDER)

WatchCtrl.m_btnData = HL.Field(HL.Table)

WatchCtrl.m_banner = HL.Field(HL.Forward("BannerWidget"))

WatchCtrl.BuildData = HL.Method() << function(self)
    if self.m_btnData == nil then
        self.m_btnData = {}
    end
    self.m_btnData = {
        [BTN_CONST.LEFT.CHAR_INFO] = {
            view = self.view.buttonCharInfo,
            phaseId = PhaseId.CharInfo,
            needRefreshUnlock = true,
            needShowRedDot = true,
        },
        [BTN_CONST.LEFT.ACTIVITY] = {
            view = self.view.activityBtnShadow,
            phaseId = PhaseId.ActivityCenter,
            openPhaseArg = {
                openFrom = "Watch"
            },
            needRefreshUnlock = true,
            needShowRedDot = true,
        },
        [BTN_CONST.LEFT.GACHA] = {
            view = self.view.gachaBtnShadow,
            phaseId = PhaseId.GachaPool,
            needShowRedDot = true,
            openPhaseArg = "",
            needRefreshUnlock = true,
        },
        [BTN_CONST.LEFT.SHOP] = {
            view = self.view.purchaseBtnNode,
            phaseId = PhaseId.CashShop,
            needRefreshUnlock = true,
            needShowRedDot = true,
        },
        [BTN_CONST.RIGHT.ADVENTURE_BOOK] = {
            view = self.view.adventureBookNode,
            phaseId = PhaseId.AdventureBook,
            needRefreshUnlock = true,
            needShowRedDot = true,
            column = 1,
        },
        [BTN_CONST.RIGHT.BATTLE_PASS] = {
            view = self.view.battlePassBtn,
            phaseId = PhaseId.BattlePass,
            needRefreshUnlock = true,
            needShowRedDot = true,
            column = 1,
        },
        [BTN_CONST.RIGHT.DOMAIN] = {
            view = self.view.DomainBtn,
            phaseId = PhaseId.DomainMain,
            needRefreshUnlock = true,
            needShowRedDot = true,
            column = 2,
        },
        [BTN_CONST.RIGHT.FRIEND] = {
            view = self.view.friendBtn,
            phaseId = PhaseId.Friend,
            needRefreshUnlock = true,
            needShowRedDot = true,
            openPhaseArg = {
                panelId = PanelId.FriendList,
            },
            column = 2,
        },
        [BTN_CONST.RIGHT.EQUIP_PRODUCER] = {
            view = self.view.equipBtn,
            phaseId = PhaseId.EquipTech,
            needRefreshUnlock = true,
            needShowRedDot = true,
            column = 3,
        },
        [BTN_CONST.RIGHT.CHAR_FORMATION] = {
            view = self.view.buttonCharFormation,
            phaseId = PhaseId.CharFormation,
            needRefreshUnlock = true,
            onClick = function()
                if Utils.isForbidden(ForbidType.ForbidSetSquad) then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_SYSTEM_FORBIDDEN)
                    return
                end
                PhaseManager:OpenPhase(PhaseId.CharFormation)
            end,
            column = 3,
        },
        [BTN_CONST.RIGHT.WIKI] = {
            view = self.view.wikiBtnShadow,
            phaseId = PhaseId.Wiki,
            needRefreshUnlock = true,
            needShowRedDot = false,
            column = 4,
        },
        [BTN_CONST.RIGHT.VALUABLE_INVENTORY] = {
            view = self.view.valuableButtonInvntory,
            phaseId = PhaseId.ValuableDepot,
            needRefreshUnlock = true,
            needShowRedDot = true,
            column = 4,
        },
        [BTN_CONST.RIGHT.STAR_SHOP] = {
            view = self.view.starShopBtnShadow,
            phaseId = PhaseId.ShopStar,
            needRefreshUnlock = true,
            needShowRedDot = true,
            column = 5,
        },
        [BTN_CONST.RIGHT.NARRATE] = {
            view = self.view.narrateNode,
            phaseId = PhaseId.PRTS,
            needShowRedDot = true,
            needRefreshUnlock = true,
            column = 5,
        },
        [BTN_CONST.RIGHT.INVENTORY] = {
            view = self.view.buttonInventory,
            phaseId = PhaseId.Inventory,
            needCloseWatch = true,
            afterCloseWatch = function()
                self.cacheNaviTarget = nil
            end,
            needRefreshUnlock = true,
            column = 6,
        },
        [BTN_CONST.RIGHT.MISSION] = {
            view = self.view.buttonMission,
            phaseId = PhaseId.Mission,
            needRefreshUnlock = true,
            openPhaseArg = {
                useBlackMask = true,
            },
            column = 7,
        },
        [BTN_CONST.RIGHT.ACHIEVEMENT] = {
            view = self.view.achievementBtn,
            phaseId = PhaseId.AchievementMain,
            needRefreshUnlock = true,
            column = 6,
        },
        [BTN_CONST.RIGHT.GEM_ENHANCE] = {
            view = self.view.gemEnhanceBtn,
            phaseId = PhaseId.GemEnhance,
            needRefreshUnlock = true,
            column = 7,
        },
        [BTN_CONST.RIGHT.SNS] = {
            view = self.view.snsBtn,
            phaseId = PhaseId.SNS,
            needRefreshUnlock = true,
            needShowRedDot = true,
            column = 8,
        },
        [BTN_CONST.RIGHT.MAP] = {
            view = self.view.buttonMap,
            phaseId = PhaseId.Map,
            needRefreshUnlock = true,
            needShowRedDot = true,
            column = 8,
        },

        [BTN_CONST.RIGHT.GAME_TOOL] = {
            view = self.view.gameToolBtn,
            onClick = function()
                CS.Beyond.SDK.SDKUtils.OpenHGWebPortalSDK("sk_toolkit", "", nil)
                EventLogManagerInst:GameEvent_GameToolClick()
            end,
            column = 9,
            needHide = GameInstance.player.gameSettingSystem.forbiddenGameTool or CS.Beyond.SDK.SDKConsts.IsBilibiliVersion()
        },
        [BTN_CONST.RIGHT.QUESTIONNAIRE] = {
            view = self.view.questionnaireBtn,
            onClick = function()
                CS.Beyond.SDK.SDKAccountUtils.OpenSurvey()
            end,
            needShowRedDot = true,
            redDotName = "Questionnaire",
            column = 9,
            needHide = GameInstance.player.gameSettingSystem.forbiddenWebView,
        },
        [BTN_CONST.CENTER.MAIL] = {
            view = self.view.mailNode,
            phaseId = PhaseId.Mail,
            needShowRedDot = true,
        },
        [BTN_CONST.CENTER.FAC_HUB_DATA] = {
            view = self.view.reportNode,
            phaseId = PhaseId.FacHUBData,
            needRefreshUnlock = true,
            needRefreshForbidden = true,
        },

        [BTN_CONST.CENTER.ANNOUNCEMENT] = {
            view = self.view.announcementBtn,
        },
        [BTN_CONST.CENTER.SETTING] = {
            view = self.view.settingNode,
            phaseId = PhaseId.GameSetting,
            needShowRedDot = true,
        },
        [BTN_CONST.CENTER.FAC_TECH_TREE] = {
            view = self.view.techtreeNode,
            phaseId = PhaseId.FacTechTree,
            needRefreshUnlock = true,
            needRefreshForbidden = true,
            needShowRedDot = true,
        },
    }
end

WatchCtrl.GenClickCallBack = HL.Method(HL.Int).Return(HL.Function) << function(self, key)
    if self.m_btnData == nil then
        return nil
    end

    local data = self.m_btnData[key]
    if data == nil then
        return nil
    end

    if data.onClick then
        return data.onClick
    end

    return function()
        if data.needCloseWatch then
            PhaseManager:ExitPhaseFast(PhaseId.Watch)
            if data.afterCloseWatch ~= nil then
                data.afterCloseWatch()
            end
        end
        if not string.isEmpty(data.phaseId) then
            PhaseManager:OpenPhase(data.phaseId, data.openPhaseArg)
        end
    end
end

WatchCtrl.InitWatchNodes = HL.Method() << function(self)
    self:BuildData()

    self:_SnapshotRightList()
    self:_RelayoutRightList()
    self:_RefreshBtnLockState()

    self.view.mapBtn.onClick:AddListener(function()
        self:_OpenMap()
    end)


    self:_InitAvatarTheme()


    self:_InitController()
end

WatchCtrl.m_rightGroups = HL.Field(HL.Table)

WatchCtrl.m_rightGroupHeight = HL.Field(HL.Number) << 0

WatchCtrl.m_rightGroupSpacing = HL.Field(HL.Number) << 0

WatchCtrl.m_rightContentOriginalHeight = HL.Field(HL.Number) << 0

WatchCtrl.m_rightListVisibleLength = HL.Field(HL.Number) << 1

WatchCtrl._SnapshotRightList = HL.Method() << function(self)
    local rightList = self.view.rightList
    local groups = {}

    for i = 0, rightList.childCount - 1 do
        local child = rightList:GetChild(i)
        if string.startWith(child.name, "Group") then
            table.insert(groups, child)
        end
    end

    local rightListScale = rightList.localScale.y
    if #groups >= 1 then
        self.m_rightGroupHeight = groups[1].sizeDelta.y * rightListScale
    end
    if #groups >= 2 then
        self.m_rightGroupSpacing = math.abs(groups[1].anchoredPosition.y - groups[2].anchoredPosition.y) * rightListScale
    end

    self.m_rightGroups = groups
    self.m_rightContentOriginalHeight = self.view.scrollViewContent.sizeDelta.y

    self.m_rightListVisibleLength = self.view.scrollViewRectTransform.rect.height / self.m_rightGroupSpacing
end

WatchCtrl._RelayoutRightList = HL.Method() << function(self)
    local groups = self.m_rightGroups
    if not groups then
        return
    end

    local activeBtns = {}
    for _, key in ipairs(RIGHT_BTN_ORDER) do
        local data = self.m_btnData[key]
        if data then
            data.column = nil
            if data.needHide then
                data.view.gameObject:SetActive(false)
            else
                table.insert(activeBtns, key)
            end
        end
    end

    local activeGroupCount = math.ceil(#activeBtns / 2)
    local btnGrid = {}
    local btnIndex = 1

    for i, group in ipairs(groups) do
        if i <= activeGroupCount then
            group.gameObject:SetActive(true)
            btnGrid[i] = {}
            for slot = 0, 1 do
                if btnIndex <= #activeBtns then
                    local key = activeBtns[btnIndex]
                    local data = self.m_btnData[key]
                    data.view.transform:SetParent(group, false)
                    data.view.transform:SetSiblingIndex(slot)
                    data.view.gameObject:SetActive(true)
                    data.column = i
                    table.insert(btnGrid[i], data.view.btn)
                    btnIndex = btnIndex + 1
                end
            end
        else
            group.gameObject:SetActive(false)
        end
    end

    local inactiveGroupCount = #groups - activeGroupCount
    local inactiveGroupHeight = inactiveGroupCount * self.m_rightGroupSpacing

    local contentSizeDelta = self.view.scrollViewContent.sizeDelta
    contentSizeDelta.y = self.m_rightContentOriginalHeight - inactiveGroupHeight
    self.view.scrollViewContent.sizeDelta = contentSizeDelta

    self.m_rightListLength = activeGroupCount

    self:_RebuildRightListNavigation(btnGrid)
end

WatchCtrl._RebuildRightListNavigation = HL.Method(HL.Table) << function(self, btnGrid)
    local rowCount = self.m_rightListLength
    for row = 1, rowCount do
        local btnRow = btnGrid[row]
        for column, btn in ipairs(btnRow) do
            btn.useExplicitNaviSelect = true

            local upBtnRow = btnGrid[row - 1]
            if upBtnRow then
                btn:SetExplicitSelectOnUp(upBtnRow[math.min(column, #upBtnRow)])
                btn.banExplicitOnUp = false
            else
                btn:SetExplicitSelectOnUp(nil)
                btn.banExplicitOnUp = true
            end

            local downBtnRow = btnGrid[row + 1]
            if downBtnRow then
                btn:SetExplicitSelectOnDown(downBtnRow[math.min(column, #downBtnRow)])
                btn.banExplicitOnDown = false
            else
                btn:SetExplicitSelectOnDown(nil)
                btn.banExplicitOnDown = true
            end

            if column > 1 then
                btn:SetExplicitSelectOnLeft(btnRow[column - 1])
                btn.banExplicitOnLeft = false
            else
                btn:SetExplicitSelectOnLeft(nil)
                btn.banExplicitOnLeft = true
            end

            if column < #btnRow then
                btn:SetExplicitSelectOnRight(btnRow[column + 1])
                btn.banExplicitOnRight = false
            else
                btn:SetExplicitSelectOnRight(nil)
                btn.banExplicitOnRight = true
            end
        end
    end
end

WatchCtrl._RefreshBtnLockState = HL.Method() << function(self)
    local inSafeZone = Utils.isInSafeZone()
    for key, data in pairs(self.m_btnData or {}) do
        local view, phaseId, needHide = data.view, data.phaseId, data.needHide
        if view ~= nil and needHide then
            view.gameObject:SetActive(false)
        elseif view ~= nil then
            local needRefreshUnlock = data.needRefreshUnlock == true
            local unlocked = (not needRefreshUnlock) or self:_CheckUnlock(phaseId, true)
            local needCheckSafeZone = data.view.safeZoneIcon ~= nil
            local showSafeIcon = needCheckSafeZone and inSafeZone

            if needRefreshUnlock then
                if view.icon ~= nil then
                    view.icon.gameObject:SetActiveIfNecessary(unlocked and (not showSafeIcon))
                end
                if view.lockIcon ~= nil then
                    view.lockIcon.gameObject:SetActiveIfNecessary(not unlocked)
                end
                if view.text ~= nil then
                    view.text.gameObject:SetActiveIfNecessary(unlocked)
                end
            end

            if needCheckSafeZone and view.safeZoneIcon ~= nil then
                view.safeZoneIcon.gameObject:SetActiveIfNecessary(unlocked and showSafeIcon)
            end

            local needShowRedDot = data.needShowRedDot
            if view.redDot ~= nil then
                if needShowRedDot and unlocked then
                    local redDotName
                    if data.redDotName then
                        redDotName = data.redDotName
                    elseif phaseId then
                        redDotName = PhaseManager:GetPhaseRedDotName(phaseId)
                    end
                    local showRedDot = not string.isEmpty(redDotName)
                    view.redDot.gameObject:SetActiveIfNecessary(showRedDot)
                    if showRedDot then
                        view.redDot:InitRedDot(redDotName)
                    end
                else
                    view.redDot:InitRedDot("")
                end
            end

            local callback = self:GenClickCallBack(key)
            if view.btn ~= nil then
                view.btn.onClick:RemoveAllListeners()
                view.btn.onClick:AddListener(callback)
            end
        end
    end
end

WatchCtrl.OnFriendBusinessInfoChange = HL.Method() << function(self)
    self:_InitAvatarTheme()
end

WatchCtrl.m_playerInfo = HL.Field(HL.Any)

WatchCtrl.m_playerInfoNode = HL.Field(HL.Any)

WatchCtrl._InitAvatarTheme = HL.Method() << function(self)
    local success , friendInfo = GameInstance.player.friendSystem:TryGetFriendInfo(GameInstance.player.roleId)
    if success then
        local businessCardId = friendInfo.businessCardTopicId
        if businessCardId then
            local successAgain, cfg = Tables.businessCardTopicTable:TryGetValue(businessCardId)
            if successAgain then
                local path = string.format(UIConst.UI_WATCH_BUSINESS_CARD_PREFAB_PATH , cfg.watchPrefab)
                local prefab = self:LoadGameObject(path)
                if prefab == nil then
                    logger.critical("无法加载表盘上的名片预制体，路径：%s, businessCardId: %s", path, businessCardId)
                    return
                end
                if self.m_playerInfoNode then
                    CSUtils.ClearUIComponents(self.m_playerInfoNode)
                    GameObject.DestroyImmediate(self.m_playerInfoNode)
                end
                self.m_playerInfoNode = CSUtils.CreateObject(prefab, self.view.playInfoPosNode)
                self.m_playerInfo = Utils.wrapLuaNode(self.m_playerInfoNode)
                self.m_playerInfo:InitPlayInfoCell()
            end
        end
    end
end

WatchCtrl._InitController = HL.Method() << function(self)
    if DeviceInfo.usingController then
        self:_InitTopLayerListeners()

        self:BindInputPlayerAction("watch_open_map", function()
            self:_OpenMap()
        end, self.view.inputGroup.groupId)


        self:BindInputPlayerAction("watch_change_left", function()
            self:SetNaviTarget(self.view.buttonCharInfo.btn)
        end, self.view.inputGroup.groupId)
        self:BindInputPlayerAction("watch_change_right", function()
            self:SetNaviTarget(self.view.adventureBookNode.btn)
        end, self.view.inputGroup.groupId)


        InputManagerInst:ToggleBinding(self.view.oriTipsButton.onClick.bindingId, false)
        self.view.addStoneBtn.onIsNaviTargetChanged = function(active)
            InputManagerInst:ToggleBinding(self.view.oriTipsButton.onClick.bindingId, active)
        end


        InputManagerInst:ToggleBinding(self.view.diamondTipsButton.onClick.bindingId,false)
        self.view.addDiamondFocusRect.gameObject:SetActive(false)
        self.view.addDiamondBtn.onIsNaviTargetChanged = function(active)
            InputManagerInst:ToggleBinding(self.view.diamondTipsButton.onClick.bindingId,active)
            if not active or self.view.inputGroup.groupEnabled then

                self.view.addDiamondFocusRect.gameObject:SetActive(active)
            end
        end


        local bannerLeftId = self:BindInputPlayerAction("watch_banner_change_left", function()
            self.m_banner:PageUpOrDown(false)
        end, self.view.inputGroup.groupId)
        local bannerRightId = self:BindInputPlayerAction("watch_banner_change_right", function()
            self.m_banner:PageUpOrDown(true)
        end, self.view.inputGroup.groupId)
        InputManagerInst:ToggleBinding(bannerLeftId,false)
        InputManagerInst:ToggleBinding(bannerRightId,false)
        self.view.bannerList.onIsNaviTargetChanged = function(active)
            InputManagerInst:ToggleBinding(bannerLeftId,active)
            InputManagerInst:ToggleBinding(bannerRightId,active)
        end


        self.view.homePageBtn.onClick:AddListener(function()
            PhaseManager:OpenPhase(PhaseId.Friend)
        end)
        local levelId = self:BindInputPlayerAction("watch_level", function()
            PhaseManager:OpenPhase(PhaseId.AdventureReward)
        end, self.view.inputGroup.groupId)
        InputManagerInst:ToggleBinding(levelId,false)
        self.view.homePageBtn.onIsNaviTargetChanged = function(active)
            InputManagerInst:ToggleBinding(levelId,active)
        end


        self.view.bannerList.onClick:AddListener(function()
            self.m_banner:JumpOut()
        end)


        self.view.reportNode.virtualReportBtn.onClick:AddListener(function()
            PhaseManager:OpenPhase(PhaseId.FacHUBData)
        end)


        self:_SetActiveControllerPanel(true)


        self:SetNaviTarget(self.cacheNaviTarget and self.cacheNaviTarget or self.view.buttonCharInfo.btn)
    end
end

WatchCtrl.m_rightListLength = HL.Field(HL.Number) << 0

WatchCtrl.m_tween = HL.Field(HL.Any)

WatchCtrl._InitSpecialRoll = HL.Method() << function(self)
    self.view.scrollViewScrollRect.verticalNormalizedPosition = 1
    for _,index in pairs(BTN_CONST.RIGHT) do
        local config = self.m_btnData[index]
        if config.column then
            config.view.btn.onIsNaviTargetChanged = function(active)
                local viewArea = self.view.scrollViewRectTransform.rect.height / self.view.scrollViewContent.rect.height
                local now = 1 - self.view.scrollViewScrollRect.verticalNormalizedPosition
                local minView = (1 - viewArea) * now
                local maxView = minView + viewArea
                local target = CSIndex(config.column) / CSIndex(self.m_rightListLength)
                local targetInView = target > minView and target < maxView
                if active and not targetInView then
                    self:_RollTo(config.column)
                end
            end
        end
    end
end

WatchCtrl._GetRollUpPosition = HL.Method(HL.Number).Return(HL.Number) << function(self, column)
    local position = (column - 1) / (self.m_rightListLength - self.m_rightListVisibleLength)

    local normalizedPosition = position - self.view.config.CONTROLLER_SCROLL_FINE_TUNE_VALUE
    normalizedPosition = lume.clamp(normalizedPosition , 0, 1)
    return normalizedPosition
end

WatchCtrl._RollTo = HL.Method(HL.Number) << function(self, column)
    if self.m_isClosed then
        return
    end
    local targetPosition
    if column <= self.m_rightListLength / 2 then
        targetPosition = 1 - self:_GetRollUpPosition(column)
    else
        targetPosition = self:_GetRollUpPosition(self.m_rightListLength + 1 - column)
    end
    self.m_tween = DOTween.To(
        function()
            return self.view.scrollViewScrollRect.verticalNormalizedPosition
        end,
        function(value)
            self.view.scrollViewScrollRect.verticalNormalizedPosition = value
        end,
        targetPosition,
        self.view.config.CONTROLLER_RIGHT_LIST_ROLL_TIME
    )
    self.m_tween:SetEase(CS.DG.Tweening.Ease.OutSine)
    self.m_tween:OnComplete(function()
        self:_KillTween()
    end)
end



WatchCtrl.m_regionMapSetting = HL.Field(HL.Userdata)

WatchCtrl.m_domainMapNode = HL.Field(HL.Any)

WatchCtrl._OpenMap = HL.Method() << function(self)
    if Utils.isInSpaceShip() then
        MapUtils.openMap(nil, Tables.spaceshipConst.baseSceneName)
        return
    end
    PhaseManager:OpenPhase(PhaseId.RegionMap)
end

WatchCtrl._InitDomain = HL.Method() << function(self)
    if self.m_domainMapNode ~= nil then
        return
    end

    if Utils.isInSpaceShip() then
        local spaceshipPrefab = self:LoadGameObject(string.format(MapConst.UI_DOMAIN_MAP_PATH, MapConst.UI_SPACESHIP_MAP))
        local spaceshipGo = CSUtils.CreateObject(spaceshipPrefab, self.view.domainRoot[string.lower(MapConst.UI_SPACESHIP_MAP)])
        self.m_domainMapNode = spaceshipGo
        local spaceship = Utils.wrapLuaNode(spaceshipGo)
        local _, roomInfo = GameInstance.player.spaceship:TryGetRoom(Tables.spaceshipConst.controlCenterRoomId)
        spaceship.spaceshipInfo.lvTxt.text = roomInfo.lv

        spaceship.meshRenderer.sharedMaterial:SetInt("_RegionMapEditor",0)
        return
    end

    local _, domainData = Tables.domainDataTable:TryGetValue(Utils.getCurDomainId())
    if domainData == nil then
        return
    end

    local domainPrefab = self:LoadGameObject(string.format(MapConst.UI_DOMAIN_MAP_PATH, domainData.domainMap))
    local domainGo = CSUtils.CreateObject(domainPrefab, self.view.domainRoot[string.lower(domainData.domainMap)])
    self.m_domainMapNode = domainGo

    local _, regionMapSetting = domainGo:TryGetComponent(typeof(CS.Beyond.UI.RegionMapSetting))
    if regionMapSetting == nil then
        return
    end

    self.m_regionMapSetting = regionMapSetting
end

WatchCtrl._RefreshDomain = HL.Method() << function(self)
    if self.m_regionMapSetting == nil then
        return
    end

    self.m_regionMapSetting:InitData(CS.Beyond.UI.RegionMapShowType.Watch, self.view.center, self.view.domainRoot.transform,
        self.view.config.RADIUS)
    for levelId, cfg in cs_pairs(self.m_regionMapSetting.cfg) do
        if cfg.isLoaded then

            local sceneBasicInfo = Utils.wrapLuaNode(cfg.ui)
            if sceneBasicInfo then

                local sceneBasicInfoArgs = {
                    levelId = levelId,
                }
                sceneBasicInfo:InitSceneBasicInfo(sceneBasicInfoArgs)
            end
        end
    end
end




local RIGHT_LIFT_RED_DOT_CHECK_TIME = 0.3


WatchCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:InitWatchNodes()

    self.view.buttonBack.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.Watch)
    end)
    self.view.fullScreenCloseBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.Watch)
    end)
    if CS.Beyond.CloudGame.enabled then

        self.view.quitBtn.gameObject:SetActive(false)
    else
        self.view.quitBtn.gameObject:SetActive(true)
        self.view.quitBtn.onClick:AddListener(function()
            self:Notify(MessageConst.SHOW_POP_UP, {
                content = Language.LUA_EXIT_GAME_CONFIRM,
                hideBlur = true,
                onConfirm = function()
                    logger.info("click quit btn on watch")
                    GameInstance.instance:ReturnToLogin()
                end,
            })
        end)
    end

    self.view.announcementBtn.onClick:AddListener(function()
        GameInstance.player.announcement:OpenAnnouncement()
    end)
    self.view.announcementRedDot:InitRedDot("Announcement")

    self.view.staminaCell.gameObject:SetActive(Utils.isSystemUnlocked(GEnums.UnlockSystemType.Dungeon))
    self.view.staminaCell:InitMoneyCell(Tables.globalConst.apItemId)

    self:RefreshWorldLevel()

    self.m_banner = BannerWidget(self.view.bannerNode)
    self.m_banner:InitBannerWidget()

    self:_InitShowInfo()
    self:_InitDomain()

    self:RefreshCurrency()

    self.view.addDiamondBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.CommonMoneyExchange, {sourceId = Tables.globalConst.originiumItemId, targetId = Tables.globalConst.diamondItemId})
    end)
    self.view.addStoneBtn.onClick:AddListener(function()
        CashShopUtils.GotoCashShopRechargeTab()
    end)

    local oriTipsOpen = false
    local diamondTipsOpen = false
    self.view.oriTipsButton.onClick:AddListener(function()
        if oriTipsOpen then
            Notify(MessageConst.HIDE_ITEM_TIPS)
            return
        end
        oriTipsOpen = true
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            itemId = "item_originium_recharge",
            transform = self.view.stoneIcon,
            posType = UIConst.UI_TIPS_POS_TYPE.MidBottom,
            onClose = function()
                oriTipsOpen = false
            end
        })
    end)

    self.view.diamondTipsButton.onClick:AddListener(function()
        if diamondTipsOpen then
            Notify(MessageConst.HIDE_ITEM_TIPS)
            return
        end
        diamondTipsOpen = true
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            itemId = "item_diamond",
            transform = self.view.jadeIcon,
            posType = UIConst.UI_TIPS_POS_TYPE.MidBottom,
            onClose = function()
                diamondTipsOpen = false
            end
        })
    end)


    self:_InitSpecialRoll()


    self:_StartCoroutine(function()
        while true do
            if self:IsShow() then
                self:_RefreshRightListRedDots()
            end
            coroutine.wait(RIGHT_LIFT_RED_DOT_CHECK_TIME)
        end
    end)


    if not GameInstance.player.gameSettingSystem.forbiddenWebView then
        CS.Beyond.SDK.SDKAccountUtils.QuerySurveyState()
    end
end

WatchCtrl._RefreshRightListRedDots = HL.Method() << function(self)
    local upRedDots = {}
    local downRedDots = {}
    local viewport = self.view.scrollViewRectTransform
    local viewRect = viewport.rect

    for _,index in pairs(BTN_CONST.RIGHT) do
        local config = self.m_btnData[index]
        local unlocked = (not config.needRefreshUnlock) or self:_CheckUnlock(config.phaseId, true)
        if unlocked and config.column and config.needShowRedDot and config.view.redDot then
            local redDotName
            if config.redDotName then
                redDotName = config.redDotName
            elseif config.phaseId then
                redDotName = PhaseManager:GetPhaseRedDotName(config.phaseId)
            end
            if not string.isEmpty(redDotName) then

                local localPos = viewport:InverseTransformPoint(config.view.redDot.transform.position)
                if localPos.y > viewRect.yMax then
                    table.insert(upRedDots, redDotName)
                elseif localPos.y < viewRect.yMin then
                    table.insert(downRedDots, redDotName)
                end
            end
        end
    end
    self.view.rightMoreDownRedDot:InitRedDot("WatchBtnList",downRedDots)
    self.view.rightMoreUpRedDot:InitRedDot("WatchBtnList",upRedDots)
end

WatchCtrl.OnClose = HL.Override() << function(self)
    self:_KillTween()
    self:_SetActiveControllerPanel(false)
    self:_ClearCameraCfg()
    if self.m_banner then
        self.m_banner:OnDestroy()
    end
    if self.m_playerInfo ~= nil then
        self.m_playerInfo:OnDestroy()
    end
end

WatchCtrl.ExtractHotSwitchRuntimeState = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    if self.m_domainMapNode == nil and self.m_regionMapSetting == nil then
        return nil
    end

    local state = {
        domainMapNode = self.m_domainMapNode,
        regionMapSetting = self.m_regionMapSetting,
    }
    self.m_domainMapNode = nil
    self.m_regionMapSetting = nil
    return state
end

WatchCtrl.RestoreHotSwitchRuntimeState = HL.Override(HL.Opt(HL.Any)) << function(self, state)
    if not state then
        return
    end

    self.m_domainMapNode = state.domainMapNode
    self.m_regionMapSetting = state.regionMapSetting
end

WatchCtrl._SetCameraCfg = HL.Method() << function(self)
    CameraManager:SetUICameraPostProcess(true)
    CameraManager:AddUICamCullingMaskConfig("Watch", UIConst.LAYERS.UIPP)
end

WatchCtrl._ClearCameraCfg = HL.Method() << function(self)
    CameraManager:SetUICameraPostProcess(false)
    CameraManager:RemoveUICamCullingMaskConfig("Watch")
    if self.m_phase then
        self.m_phase:_ChangeBlurSetting(false)
    end
end

WatchCtrl._CheckUnlock = HL.Method(HL.Number, HL.Opt(HL.Any)).Return(HL.Boolean) << function(self, phaseId, silent)
    local unlock = PhaseManager:IsPhaseUnlocked(phaseId)
    if (not unlock) and (silent ~= true) then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_SYSTEM_LOCK)
    end
    return unlock
end

WatchCtrl._InitShowInfo = HL.Method() << function(self)

    self.view.facMiniPowerContent:InitFacMiniPowerContent()
    local needShowFacMiniPower = Utils.isSystemUnlocked(GEnums.UnlockSystemType.FacSystem) and not Utils.isInSpaceShip()
    self.view.facMiniPower.gameObject:SetActiveIfNecessary(needShowFacMiniPower)

    local isInFacMainRegion = Utils.isInFacMainRegion()
    self.view.techtreeNode.forbidIcon.gameObject:SetActiveIfNecessary(false)
    self.view.employeeNode.forbidIcon.gameObject:SetActiveIfNecessary(not isInFacMainRegion)
    self.view.reportNode.forbidIcon.gameObject:SetActiveIfNecessary(false)
end

WatchCtrl.OnWalletChanged = HL.Method(HL.Table) << function(self, args)
    self:RefreshCurrency()
end

WatchCtrl._OnSystemUnlock = HL.Method(HL.Table) << function(self, arg)
    local systemIndex = unpack(arg)
    if systemIndex == GEnums.UnlockSystemType.Dungeon:GetHashCode() then
        self.view.staminaCell.gameObject:SetActive(true)
    end
end

WatchCtrl.RefreshCurrency = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    local originiumId = Tables.globalConst.originiumItemId
    local diamondId = Tables.globalConst.diamondItemId
    self.view.textMoney1.text = tonumber(GameInstance.player.inventory:GetItemCount(Utils.getCurrentScope(), Utils.getCurrentChapterId(), originiumId))
    self.view.textMoney2.text = tonumber(GameInstance.player.inventory:GetItemCount(Utils.getCurrentScope(), Utils.getCurrentChapterId(), diamondId))
end

WatchCtrl.RefreshWorldLevel = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    self.view.exploreCell:InitWorldLevelCell()
end

WatchCtrl.cacheNaviTarget = HL.Field(HL.Any)

WatchCtrl.m_topLayer = HL.Field(HL.Any)

WatchCtrl._InitTopLayerListeners = HL.Method() << function(self)
    self.m_topLayer = self.view.selectableNaviGroup
    self.view.selectableNaviGroup.onIsTopLayerChanged:AddListener(function(isTop)
        if isTop then
            self.m_topLayer = self.view.selectableNaviGroup
        end
    end)
    self.view.leftBottomNode.onIsTopLayerChanged:AddListener(function(isTop)
        if isTop then
            self.m_topLayer = self.view.leftBottomNode
        end
    end)
    self.view.scrollView.onIsTopLayerChanged:AddListener(function(isTop)
        if isTop then
            self.m_topLayer = self.view.scrollView
        end
    end)
end

WatchCtrl._KillTween = HL.Method() << function(self)
    if self.m_tween then
        self.m_tween:Kill()
    end
    self.m_tween = nil
end

WatchCtrl.OnHide = HL.Override() << function(self)
    self:_KillTween()
    self:_ClearCameraCfg()
    self.m_banner:SetPause(true)
    self:_SetVisibilityControllerPanel(false)

    self.view.animationWrapper.autoPlay = false

    if self.m_playerInfo ~= nil then
        self.m_playerInfo:OnHide()
    end
end

WatchCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    if active then
        if DeviceInfo.usingController then
            self:SetNaviTarget(self.cacheNaviTarget and self.cacheNaviTarget or self.view.buttonCharInfo.btn)
            self.view.scrollViewScrollRect.verticalNormalizedPosition = self.cacheNaviTarget and self.view.scrollViewScrollRect.verticalNormalizedPosition or 1
        end
    else
        self:SetNaviTarget(nil)
        if DeviceInfo.usingController and self.m_topLayer then
            self.cacheNaviTarget = self.m_topLayer.LayerSelectedTarget
        end
    end
end


WatchCtrl.OnShow = HL.Override() << function(self)
    self.m_banner:SetPause(false)
    self:_SetVisibilityControllerPanel(true)
    self:_InitAvatarTheme()
    self:_RefreshDomain()
    self:_RefreshBtnLockState()

    self.view.animationWrapper.autoPlay = true
end

WatchCtrl.m_controllerHintPanel = HL.Field(HL.Any)

WatchCtrl._SetActiveControllerPanel = HL.Method(HL.Boolean) << function(self, active)
    if not DeviceInfo.usingController then
        return
    end
    local isOpen = UIManager:IsOpen(PanelId.WatchController)
    if active and not isOpen then
        self.m_controllerHintPanel = UIManager:Open(PanelId.WatchController,{ groupId = { self.view.inputGroup.groupId } })
    elseif not active and isOpen then
        UIManager:Close(PanelId.WatchController)
    end
end

WatchCtrl._SetVisibilityControllerPanel = HL.Method(HL.Boolean) << function(self, active)
    if not DeviceInfo.usingController or self.m_controllerHintPanel == nil then
        return
    end
    if active then
        UIManager:Show(PanelId.WatchController)
    else
        UIManager:Hide(PanelId.WatchController)
    end
end

WatchCtrl._OnPlayAnimationOut = HL.Override() << function(self)
end

WatchCtrl.OnAnimationInFinished = HL.Override() << function(self)
end

HL.Commit(WatchCtrl)
