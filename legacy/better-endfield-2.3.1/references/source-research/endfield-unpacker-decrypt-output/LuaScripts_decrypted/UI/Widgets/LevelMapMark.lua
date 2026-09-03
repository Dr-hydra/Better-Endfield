local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local MARK_GAME_OBJECT_NAME_FORMAT = "MarkInst_%s"

LevelMapMark = HL.Class('LevelMapMark', UIWidgetBase)

LevelMapMark.markRuntimeData = HL.Field(HL.Userdata)

LevelMapMark.rectPosition = HL.Field(Vector2)

LevelMapMark.m_loadArgs = HL.Field(HL.Table)

LevelMapMark.m_needLoadAsync = HL.Field(HL.Boolean) << false

LevelMapMark.m_isTrackingMark = HL.Field(HL.Boolean) << false

LevelMapMark.m_isTracking = HL.Field(HL.Boolean) << false

LevelMapMark.m_waitingPlayTrackingAnim = HL.Field(HL.Boolean) << false

LevelMapMark.m_isCustomMark = HL.Field(HL.Boolean) << false

LevelMapMark.m_isInTierState = HL.Field(HL.Boolean) << false

LevelMapMark.m_extraDynamicNodes = HL.Field(HL.Table)

LevelMapMark.m_extraDynamicNodeHideKeyList = HL.Field(HL.Table)

LevelMapMark.m_hideKeyList = HL.Field(HL.Table)

LevelMapMark.m_forceShowKeyList = HL.Field(HL.Table)

LevelMapMark.m_initialized = HL.Field(HL.Boolean) << false



LevelMapMark.needTryLoadResourceStateNode = HL.Field(HL.Boolean) << false



LevelMapMark._OnFirstTimeInit = HL.Override() << function(self)
end

LevelMapMark._OnEnable = HL.Override() << function(self)
    self:_RecoverTrackingMarkStateOnEnabled()
end

LevelMapMark.InitLevelMapMark = HL.Method(Vector2, HL.Userdata, HL.Opt(HL.Boolean, HL.Boolean)) << function(
    self, initialPos, markRuntimeData, needLoadAsync, ignoreVisible
)
    self:_FirstTimeInit()

    self:ClearLevelMapMark()
    self.m_initialized = true

    self.markRuntimeData = markRuntimeData
    self.m_isTrackingMark = false
    self.m_isTracking = false
    self.m_isCustomMark = false
    self.m_needLoadAsync = needLoadAsync or false
    self.m_extraDynamicNodes = {}
    self.m_extraDynamicNodeHideKeyList = {}

    self.view.rectTransform.anchoredPosition = initialPos
    self.rectPosition = initialPos


    if not ignoreVisible then
        self.m_hideKeyList = {}
        self.m_forceShowKeyList = {}
        self:_RefreshMarkVisibleState()
    end


    self:_RefreshMarkIcon()


    self:_RefreshCustomMarkState()

    if not self.m_isCustomMark then

        self:_RefreshDynamicPrefabs()
    end


    self:_RefreshMarkClick()

    self.view.gameObject.name = string.format(MARK_GAME_OBJECT_NAME_FORMAT, markRuntimeData.instId)
end

LevelMapMark._OnDestroy = HL.Override() << function(self)
    self:ClearLevelMapMark(true)
end

LevelMapMark.ClearLevelMapMark = HL.Method(HL.Opt(HL.Boolean)) << function(self, isDestroy)
    if not self.m_initialized then
        return
    end

    for _, dynamicNode in pairs(self.m_extraDynamicNodes) do
        GameObject.Destroy(dynamicNode)
    end

    if not isDestroy then
        self.loader:DisposeAllHandles(true)
    end

    MessageManager:UnregisterAll(self)
    self:_ClearDynamicPrefabs()
    self.view.gameObject.name = "Cached"

    self.markRuntimeData = nil
    self.m_hideKeyList = {}
    self.m_forceShowKeyList = {}
    self.m_extraDynamicNodeHideKeyList = {}
    self.m_extraDynamicNodes = {}

    self.m_initialized = false
end

LevelMapMark._RefreshDynamicPrefabs = HL.Method() << function(self)
    self:_RefreshDomainDepotHint()
    self:_RefreshSocialBuildingHint()
    self:_RefreshSettlementLevelNode()
    self:_RefreshSettlementDefense()
    self:_RefreshResourceStateNode()
end

LevelMapMark._ClearDynamicPrefabs = HL.Method() << function(self)
    self:_ClearSettlementLevel()
    self:_ClearSettlementDefense()
    self:_ClearResourceStateNode()
end

LevelMapMark._CreatePrefabObj = HL.Method(HL.String, HL.Opt(HL.Boolean, Transform)).Return(HL.Any) << function(self, prefabKey, isBasic, transform)
    local prefab = LuaSystemManager.mapResourceSystem:GetMarkDynamicNodePrefab(prefabKey)
    if prefab == nil then
        return nil
    end

    transform = transform or self.view.transform
    local prefabObj = CSUtils.CreateObject(prefab, transform)
    if not isBasic then
        self.m_extraDynamicNodes[prefabKey] = prefabObj
    end
    return Utils.wrapLuaNode(prefabObj)
end

LevelMapMark._ToggleExtraDynamicNodeActive = HL.Method(HL.String, HL.String, HL.Boolean) << function(self, prefabKey, toggleKey, active)
    if self.m_extraDynamicNodeHideKeyList[prefabKey] == nil then
        self.m_extraDynamicNodeHideKeyList[prefabKey] = {}
    end
    if active then
        self.m_extraDynamicNodeHideKeyList[prefabKey][toggleKey] = nil
    else
        self.m_extraDynamicNodeHideKeyList[prefabKey][toggleKey] = true
    end
    local extraNode = self.m_extraDynamicNodes[prefabKey]
    if extraNode ~= nil then
        extraNode:SetActive(next(self.m_extraDynamicNodeHideKeyList[prefabKey]) == nil)
    end
end

LevelMapMark._GetMarkIconSprite = HL.Method(HL.String, HL.Boolean, HL.Function) << function(self, templateId, active, onComplete)
    local templateCfg = Tables.mapMarkTempTable[templateId]
    local iconName = active and templateCfg.activeIcon or templateCfg.inActiveIcon
    local iconPath = UIUtils.getSpritePath(UIConst.UI_SPRITE_MAP_MARK_ICON, iconName)
    if self.m_needLoadAsync and not self.m_isCustomMark then
        self:ToggleMarkHiddenState("LoadIcon", true)
        self.loader:LoadSpriteAsync(iconPath, function(sprite)
            self:ToggleMarkHiddenState("LoadIcon", false)
            onComplete(sprite)
        end)
    else
        onComplete(self.loader:LoadSprite(iconPath))
    end
end

LevelMapMark._RefreshMarkIcon = HL.Method() << function(self)
    if self.markRuntimeData == nil then
        return
    end
    local templateId = self.markRuntimeData.templateId
    self:_GetMarkIconSprite(templateId, self.markRuntimeData.isActive, function(iconSprite)
        if not NotNull(self.view.iconImg) then
            return
        end
        self.view.iconImg.sprite = iconSprite
        self.view.iconImg:SetNativeSize()
    end)
end

LevelMapMark._RefreshMarkClick = HL.Method() << function(self)
    if self.view.button == nil then
        return
    end
    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        Notify(MessageConst.ON_LEVEL_MAP_MARK_CLICKED, self.markRuntimeData.instId)
    end)


    local extraPadding = 0
    local basicPadding = self.m_isCustomMark and DataManager.uiLevelMapConfig.customClickPadding or 0
    if DeviceInfo.usingTouch then
        extraPadding = DataManager.uiLevelMapConfig.touchClickPadding
    elseif DeviceInfo.usingController then
        extraPadding = DataManager.uiLevelMapConfig.controllerClickPadding
    else
        extraPadding = DataManager.uiLevelMapConfig.mouseClickPadding
    end
    self.view.iconImg.raycastPadding = Vector4.one * -(extraPadding + basicPadding)
end

LevelMapMark._RefreshMarkVisibleState = HL.Method() << function(self)


    local isVisible = (self:GetIsMarkForceShowing() or not self:GetIsMarkHidden()) and self.m_initialized
    self.view.gameObject:SetActive(isVisible)
end

LevelMapMark._DynamicNodeStretchAndCenter = HL.Method(HL.Any) << function(self, node)
    if node == nil or node.rectTransform == nil then
        return
    end
    node.rectTransform.pivot = Vector2(0.5, 0.5)
    node.rectTransform.anchorMin = Vector2.zero
    node.rectTransform.anchorMax = Vector2.one
    node.rectTransform.anchoredPosition = Vector2.zero
end




LevelMapMark._RefreshTrackingMarkCircleAndArrowImage = HL.Method() << function(self)
    local circleName, arrowName = "icon_mission_diffusion_white", "icon_track_arrow_white"
    if self.markRuntimeData.missionInfo ~= nil and self.markRuntimeData.isMissionTracking then
        if self.markRuntimeData.missionInfo.missionImportance == GEnums.MissionImportance.High then
            circleName = "icon_mission_diffusion_yellow"
            arrowName = "icon_track_arrow_yellow"
        elseif self.markRuntimeData.missionInfo.missionImportance == GEnums.MissionImportance.Mid then
            circleName = "icon_mission_diffusion_green"
            arrowName = "icon_track_arrow_green"
        else
            circleName = "icon_mission_diffusion_blue"
            arrowName = "icon_track_arrow_blue"
        end
    end
    self.view.trackingImg:LoadSprite(UIConst.UI_SPRITE_MISSION_ICON, circleName)
    self.view.arrowImg:LoadSprite(UIConst.UI_SPRITE_MISSION_ICON, arrowName)
end

LevelMapMark._OnLimitStateChanged = HL.Method(HL.Boolean) << function(self, isLimited)
    self:_RefreshMarkTierVisibleState()
end

LevelMapMark._RecoverTrackingMarkStateOnEnabled = HL.Method() << function(self)
    if not self.m_isTrackingMark or not self.view.trackingImg.gameObject.activeSelf then
        return
    end
    self.view.trackingImgAnim:PlayLoopAnimation()
end

LevelMapMark._RefreshTrackingMarkLimitOriginalRectPosition = HL.Method(Vector2) << function(self, rectPosition)
    if self.view.levelMapLimitInRect == nil then
        return
    end
    self.view.levelMapLimitInRect.originalRectPosition = rectPosition
end

LevelMapMark._StartLimitMarkInRect = HL.Method() << function(self)
    self.view.levelMapLimitInRect:StartLimitMarkInRect()
    if self.view.config.NEED_HIDE_TIER_NODE_WHEN_LIMIT then
        self.view.levelMapLimitInRect.onIsLimitedInRectChanged:RemoveAllListeners()
        self.view.levelMapLimitInRect.onIsLimitedInRectChanged:AddListener(function(isLimited)
            self:_OnLimitStateChanged(isLimited)
        end)
    end
end

LevelMapMark._StopLimitMarkInRect = HL.Method() << function(self)
    self.view.levelMapLimitInRect:StopLimitMarkInRect()
    if self.view.config.NEED_HIDE_TIER_NODE_WHEN_LIMIT then
        self.view.levelMapLimitInRect.onIsLimitedInRectChanged:RemoveAllListeners()
    end
end

LevelMapMark._ForceRefreshTrackingMarkLimitState = HL.Method() << function(self)
    if IsNull(self.view.levelMapLimitInRect) then
        return
    end
    self.view.levelMapLimitInRect:ForceRefreshLimitState()
end

LevelMapMark.RefreshTrackingMarkState = HL.Method(HL.Boolean) << function(self, isTracking)
    self.view.trackingImg.gameObject:SetActive(isTracking)
    if isTracking and not self.m_isTracking then
        if not self:GetUICtrl():IsHide() then
            self.view.trackingImgAnim:PlayInAnimation()
        else
            self.m_waitingPlayTrackingAnim = true
        end
        self:_StartLimitMarkInRect()
        self:_RefreshTrackingMarkCircleAndArrowImage()
    elseif not isTracking and self.m_isTracking then
        self:_StopLimitMarkInRect()
        self.m_waitingPlayTrackingAnim = false
    end
    self.m_isTracking = isTracking
    self.m_isTrackingMark = true
end

LevelMapMark.TryPlayTrackingMarkAnimation = HL.Method() << function(self)
    if not self.m_isTracking or not self.m_isTrackingMark then
        return
    end
    if self.m_waitingPlayTrackingAnim then
        self.view.trackingImgAnim:PlayInAnimation()
    else
        self.view.trackingImgAnim:PlayLoopAnimation()
    end
    self.m_waitingPlayTrackingAnim = false
end






LevelMapMark._RefreshCustomMarkState = HL.Method() << function(self)
    if self.markRuntimeData.templateIdNum == nil then
        return
    end
    if self.view.deleteImage ~= nil then
        self.view.deleteImage.gameObject:SetActive(false)
    end
    self.m_isCustomMark = true
end

LevelMapMark.RefreshCustomMarkDeleteState = HL.Method(HL.Boolean) << function(self, isSelect)
    UIUtils.PlayAnimationAndToggleActive(self.view.deleteImage, isSelect)
end

LevelMapMark.GetCustomMarkDeleteState = HL.Method().Return(HL.Boolean) << function(self)
    return self.view.deleteImage.gameObject.activeSelf
end






LevelMapMark._RefreshMarkTierVisibleState = HL.Method() << function(self)
    if self.view.tierStateNode == nil then
        return
    end
    local needShowTier = self.m_isInTierState
    if self.m_isTrackingMark then
        local needHideTier = self.view.config.NEED_HIDE_TIER_NODE_WHEN_LIMIT and self.view.levelMapLimitInRect.isLimitedInRect
        needShowTier = needShowTier and not needHideTier
    end
    self.view.tierStateNode.gameObject:SetActive(needShowTier)
end

LevelMapMark.RefreshMarkTierState = HL.Method(HL.Number, HL.Boolean) << function(self, tierIndex, forceHide)
    if self.view.tierStateNode == nil then
        local iconNode = self.view.iconImg.gameObject:GetComponent("RectTransform")
        self.view.tierStateNode = self:_CreatePrefabObj("TierStateNode", true, iconNode)
    end
    if forceHide or self.markRuntimeData == nil then
        self.view.tierStateNode.gameObject:SetActive(false)
        return
    end
    local selfIndex = self.markRuntimeData.tierIndex
    local needShowTierNode = selfIndex ~= tierIndex
    self.m_isInTierState = needShowTierNode
    self:_RefreshMarkTierVisibleState()
end

LevelMapMark.GetMarkTierState = HL.Method().Return(HL.Boolean, HL.Number, HL.Number) << function(self)
    if self.markRuntimeData == nil then
        return false, MapConst.BASE_TIER_ID, MapConst.BASE_TIER_INDEX
    end
    return self.view.tierStateNode.gameObject.activeSelf, self.markRuntimeData.tierId, self.markRuntimeData.tierIndex
end






LevelMapMark.RefreshDetectorNodeState = HL.Method(HL.Boolean) << function(self, active)
    if self.view.detectorNode == nil then
        if not active then
            return
        end
        self.view.detectorNode = self:_CreatePrefabObj("DetectorNode")
        self:_DynamicNodeStretchAndCenter(self.view.detectorNode)
    end
    self:_ToggleExtraDynamicNodeActive("DetectorNode", "RefreshDetector", active)
    if active then
        self.view.animationWrapper:ClearTween()
        self.view.iconImg.raycastTarget = false
        self.view.animationWrapper:PlayWithTween("levelmapmark_detector_in", function()
            self.view.iconImg.raycastTarget = true
        end)
    end
end






LevelMapMark._RefreshDomainDepotHint = HL.Method() << function(self)
    if not GameInstance.player.mapManager.showingDomainDepotDeliverMark then
        return
    end
    if self.markRuntimeData.instId ~= GameInstance.player.mapManager.showingDomainDepotRecvMarkInstId and
        self.markRuntimeData.instId ~= GameInstance.player.mapManager.showingDomainDepotSendMarkInstId then
        return
    end
    local hintTextId = self.markRuntimeData.instId == GameInstance.player.mapManager.showingDomainDepotRecvMarkInstId and
        "LUA_DOMAIN_DEPOT_MAP_MARK_HINT_RECV" or "LUA_DOMAIN_DEPOT_MAP_MARK_HINT_SEND"
    local hintNode = self:_CreatePrefabObj("DomainDepotHint")
    hintNode.hintTxt.text = Language[hintTextId]
end

LevelMapMark._RefreshSocialBuildingHint = HL.Method() << function(self)
    local socialBuildingMarkInstId = GameInstance.player.mapManager.socialBuildingMarkInstId
    if string.isEmpty(socialBuildingMarkInstId) or self.markRuntimeData.instId ~= socialBuildingMarkInstId then
        return
    end
    self:_CreatePrefabObj("SocialBuildingHint")
end

LevelMapMark._RefreshSettlementLevelNode = HL.Method() << function(self)
    if string.isEmpty(self.markRuntimeData.settlementId) then
        return
    end
    local settlementLevelNode = self:_CreatePrefabObj("SettlementLevelNode")
    self:_DynamicNodeStretchAndCenter(settlementLevelNode)
    self.view.settlementLevelNode = settlementLevelNode
    self:_RefreshSettlementLevelText()
    self:RegisterMessage(MessageConst.ON_SETTLEMENT_UPGRADE, function()
        self:_RefreshSettlementLevelText()
    end)
end

LevelMapMark._RefreshSettlementLevelText = HL.Method() << function(self)
    local level = GameInstance.player.settlementSystem:GetSettlementLevel(self.markRuntimeData.settlementId)
    self.view.settlementLevelNode.levelTxt.text = string.format("%d", level)
end

LevelMapMark._ClearSettlementLevel = HL.Method() << function(self)
    self.view.settlementLevelNode = nil
end



LevelMapMark._RefreshSettlementDefense = HL.Method() << function(self)
    if self.markRuntimeData.detail == nil or string.isEmpty(self.markRuntimeData.detail.settlementId) then
        return
    end
    self:RegisterMessage(MessageConst.ON_TOWER_DEFENSE_LEVEL_UNLOCKED, function()
        self:_UpdateSettlementDefense()
    end)
    self:RegisterMessage(MessageConst.ON_TOWER_DEFENSE_LEVEL_COMPLETED, function()
        self:_UpdateSettlementDefense()
    end)
    self:RegisterMessage(MessageConst.ON_SETTLEMENT_UPGRADE, function()
        self:_UpdateSettlementDefense()
    end)
    self:_UpdateSettlementDefense()
end

LevelMapMark._UpdateSettlementDefense = HL.Method() << function(self)
    if self.markRuntimeData.detail == nil or string.isEmpty(self.markRuntimeData.detail.settlementId) then
        return
    end
    local settlementId = self.markRuntimeData.detail.settlementId
    local isDanger = false
    if not string.isEmpty(settlementId) then
        isDanger = GameInstance.player.towerDefenseSystem:GetSettlementDefenseState(settlementId) ==
            CS.Beyond.Gameplay.TowerDefenseSystem.DefenseState.Danger
    end

    if isDanger then
        if not IsNull(self.view.settlementDefenseNode) then
            self:_ToggleExtraDynamicNodeActive("SettlementDefenseHint", "IsDanger", true)
        else
            self.view.settlementDefenseNode = self:_CreatePrefabObj("SettlementDefenseHint")
        end
    else
        if not IsNull(self.view.settlementDefenseNode) then
            self:_ToggleExtraDynamicNodeActive("SettlementDefenseHint", "IsDanger", false)
        end
    end
end

LevelMapMark._ClearSettlementDefense = HL.Method() << function(self)
    self.view.settlementDefenseNode = nil
end





LevelMapMark._RefreshResourceStateNode = HL.Method() << function(self)
    if not self.needTryLoadResourceStateNode or not MapConst.RESOURCE_RELATED_MARK_TYPE[self.markRuntimeData.type] then
        return
    end
    self:RegisterMessage(MessageConst.ON_DOODAD_GROUP_DATA_CHANGE, function(args)
        local instId = unpack(args)
        if instId == self.markRuntimeData.instId then
            self:_RefreshResourceStateNodeContent()
        end
    end)
    self:_RefreshResourceStateNodeContent()
end

LevelMapMark._RefreshResourceStateNodeContent = HL.Method() << function(self)
    if IsNull(self.view.resourceStateNode) then
        self.view.resourceStateNode = self:_CreatePrefabObj("ResourceStateNode")
    end
    local resourceStateNode = self.view.resourceStateNode
    if self.markRuntimeData.type == GEnums.MarkType.DoodadGroup then
        local doodadGroupData = GameInstance.player.doodadSystem:GetDoodadSystemDataByMarkInst(self.markRuntimeData.instId)
        if doodadGroupData ~= nil then
            resourceStateNode.countTxt.text = tostring(doodadGroupData.curCount)
            if doodadGroupData.curCount == 0 then
                resourceStateNode.contentStateController:SetState("Hidden")
            else
                resourceStateNode.contentStateController:SetState(doodadGroupData.curCount == doodadGroupData.maxCount and "Max" or "Normal")
            end
        else
            self.view.iconImg.sprite = nil
        end
    elseif self.markRuntimeData.type == GEnums.MarkType.MinePointTeam then
        if self.markRuntimeData.mapId ~= GameWorld.worldInfo.curMapIdStr then
            resourceStateNode.contentStateController:SetState("Hidden")
        else
            local minePointIdList = {}
            for index = 0, self.markRuntimeData.detail.count - 1 do
                table.insert(minePointIdList, self.markRuntimeData.detail.coreLogicId[index])
            end
            local minerList = GameInstance.player.mapManager:GetMinerList(minePointIdList, self.markRuntimeData.levelId)
            if minerList.Count ~= self.markRuntimeData.detail.count then
                resourceStateNode.contentStateController:SetState("Warning")
            else
                local allMinerNormal = true
                local _, levelBasicInfo = DataManager.levelBasicInfoTable:TryGetValue(self.markRuntimeData.levelId)
                local developmentLv = GameInstance.player.domainDevelopmentSystem:GetDomainDevelopmentLv(levelBasicInfo.domainName)
                for index = 0, minerList.Count - 1 do
                    local miner = minerList[index]
                    if miner ~= nil then
                        local minerState = GameInstance.remoteFactoryManager:QueryBuildingState(miner.belongChapter.chapterId, miner.nodeId, false)
                        local activeState = minerState == GEnums.FacBuildingState.Normal or minerState == GEnums.FacBuildingState.Blocked
                        if not activeState then
                            allMinerNormal = false
                            break
                        end
                    else
                        local lowLevel = self.markRuntimeData.detail.lowPurityLevel[index]
                        if developmentLv >= lowLevel then

                            allMinerNormal = false
                            break
                        end
                    end
                end
                resourceStateNode.contentStateController:SetState(allMinerNormal and "Hidden" or "Warning")
            end
        end
    elseif self.markRuntimeData.type == GEnums.MarkType.GasMinePointTeam then
        if self.markRuntimeData.mapId ~= GameWorld.worldInfo.curMapIdStr then
            resourceStateNode.contentStateController:SetState("Hidden")
        else
            local minePointIdList = {}
            for index = 0, self.markRuntimeData.detail.count - 1 do
                table.insert(minePointIdList, self.markRuntimeData.detail.coreLogicId[index])
            end
            local minerList = GameInstance.player.mapManager:GetGasMinerList(minePointIdList, self.markRuntimeData.levelId)
            if minerList.Count ~= self.markRuntimeData.detail.count then
                resourceStateNode.contentStateController:SetState("Warning")
            else
                local allMinerNormal = true
                for index = 0, minerList.Count - 1 do
                    local miner = minerList[index]
                    if miner ~= nil then
                        local minerState = GameInstance.remoteFactoryManager:QueryBuildingState(miner.belongChapter.chapterId, miner.nodeId, false)
                        local activeState = minerState == GEnums.FacBuildingState.Normal or minerState == GEnums.FacBuildingState.Blocked
                        if not activeState then
                            allMinerNormal = false
                            break
                        end
                    else
                        allMinerNormal = false
                        break
                    end
                end
                resourceStateNode.contentStateController:SetState(allMinerNormal and "Hidden" or "Warning")
            end
        end
    end
end

LevelMapMark._ClearResourceStateNode = HL.Method() << function(self)
    self.view.resourceStateNode = nil
end








LevelMapMark.OverrideMarkRectPosition = HL.Method(Vector2) << function(self, position)
    self.view.rectTransform.anchoredPosition = position
    self:_RefreshTrackingMarkLimitOriginalRectPosition(position)
    self:_ForceRefreshTrackingMarkLimitState()
end

LevelMapMark.ResetMarkRectPosition = HL.Method() << function(self)
    self.view.rectTransform.anchoredPosition = self.rectPosition
    self:_RefreshTrackingMarkLimitOriginalRectPosition(self.rectPosition)
    self:_ForceRefreshTrackingMarkLimitState()
end

LevelMapMark.OverrideMarkIcon = HL.Method(HL.String, HL.Opt(HL.Boolean)) << function(self, iconName, needHideExtraNodes)
    self.view.iconImg.sprite = LuaSystemManager.mapResourceSystem:GetSingleMarkIconSprite(iconName)
    self.view.iconImg:SetNativeSize()

    if needHideExtraNodes then
        for prefabKey, _ in pairs(self.m_extraDynamicNodes) do
            self:_ToggleExtraDynamicNodeActive(prefabKey, "OverrideIcon", false)
        end
    end
end

LevelMapMark.ResetMarkIcon = HL.Method() << function(self)
    self:_RefreshMarkIcon()

    for prefabKey, _ in pairs(self.m_extraDynamicNodes) do
        self:_ToggleExtraDynamicNodeActive(prefabKey, "OverrideIcon", true)
    end
end

LevelMapMark.OverrideMarkOnClickCallback = HL.Method(HL.Function) << function(self, callback)
    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        callback()
    end)
end

LevelMapMark.ResetMarkOnClickCallback = HL.Method() << function(self)
    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        Notify(MessageConst.ON_LEVEL_MAP_MARK_CLICKED, self.markRuntimeData.instId)
    end)
end

LevelMapMark.SetMarkOnHoverCallback = HL.Method(HL.Function) << function(self, callback)
    self.view.button.onHoverChange:RemoveAllListeners()
    self.view.button.onHoverChange:AddListener(function(isHover)
        callback(isHover)
    end)
end

LevelMapMark.ToggleMarkHighlightState = HL.Method(HL.Boolean) << function(self, isHighlight)
    self.view.animationWrapper:ClearTween()
    self.view.animator:SetBool("Highlighted", isHighlight)
    self.view.animator:SetBool("Normal", not isHighlight)
end

LevelMapMark.ToggleMarkHiddenState = HL.Method(HL.String, HL.Boolean) << function(self, hideKey, isHidden)
    if isHidden then
        self.m_hideKeyList[hideKey] = true
    else
        self.m_hideKeyList[hideKey] = nil
    end
    self:_RefreshMarkVisibleState()
end

LevelMapMark.ToggleForceShowMark = HL.Method(HL.String, HL.Boolean) << function(self, forceShowKey, forceShow)
    if forceShow then
        self.m_forceShowKeyList[forceShowKey] = true
    else
        self.m_forceShowKeyList[forceShowKey] = nil
    end
    self:_RefreshMarkVisibleState()
end

LevelMapMark.GetIsMarkHidden = HL.Method().Return(HL.Boolean) << function(self)
    return next(self.m_hideKeyList) ~= nil
end

LevelMapMark.GetIsMarkForceShowing = HL.Method().Return(HL.Boolean) << function(self)
    return next(self.m_forceShowKeyList) ~= nil
end




HL.Commit(LevelMapMark)
return LevelMapMark