local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

MapSpaceshipNode = HL.Class('MapSpaceshipNode', UIWidgetBase)

MapSpaceshipNode.m_isSpaceshipMap = HL.Field(HL.Boolean) << false

MapSpaceshipNode.m_domainId = HL.Field(HL.String) << ""

MapSpaceshipNode.m_levelId = HL.Field(HL.String) << ""

MapSpaceshipNode.s_fromLevelId = HL.StaticField(HL.String) << ""

MapSpaceshipNode.s_fromDomainId = HL.StaticField(HL.String) << ""


MapSpaceshipNode._OnFirstTimeInit = HL.Override() << function(self)
    self.view.btn.onClick:AddListener(function()
        self:_RecordStaticFromData()
        if self.m_isSpaceshipMap then
            if string.isEmpty(MapSpaceshipNode.s_fromLevelId) then

                self:_ReturnToRegionMap(MapSpaceshipNode.s_fromDomainId)
            else
                self:_ReturnToLevelMap(MapSpaceshipNode.s_fromLevelId)
            end
        else
            GameInstance.player.mapManager:SendLevelReadMessage(Tables.spaceshipConst.baseSceneName)
            self:_ReturnToLevelMap(Tables.spaceshipConst.baseSceneName)
        end
    end)
end





MapSpaceshipNode.InitMapSpaceshipNode = HL.Method(HL.Table) << function(self, args)
    self:_FirstTimeInit()


    local mapManager = GameInstance.player.mapManager
    local spaceshipLevelId = Tables.spaceshipConst.baseSceneName

    self.m_isSpaceshipMap = args.levelId == spaceshipLevelId
    self.m_levelId = args.levelId == nil and "" or args.levelId
    self.m_domainId = args.domainId == nil and "" or args.domainId

    local isUnlocked = (not self.m_isSpaceshipMap and mapManager:IsLevelUnlocked(spaceshipLevelId)) or
        (self.m_isSpaceshipMap)
    self:_ToggleVisibleState(isUnlocked)
    if not isUnlocked then
        return
    end
    if self.m_isSpaceshipMap then
        self.view.redDot:Stop()
    else
        self.view.redDot:InitRedDot("MapUnreadLevel", spaceshipLevelId)
    end

    local isShowPlayer = (self.m_isSpaceshipMap and not Utils.isInSpaceShip()) or
        (not self.m_isSpaceshipMap and Utils.isInSpaceShip())
    if isShowPlayer then
        self.view.mainPlayerNode.gameObject:SetActive(self.m_isSpaceshipMap)
        self.view.shipPlayerNode.gameObject:SetActive(not self.m_isSpaceshipMap)
    else
        self.view.mainPlayerNode.gameObject:SetActive(false)
        self.view.shipPlayerNode.gameObject:SetActive(false)
    end

    local isShowTracking = false
    local trackingMissionId = ""
    for index = 0, mapManager.trackingMissionMarkList.Count - 1 do
        local markInstId = mapManager.trackingMissionMarkList[index]
        local _, runtimeData = mapManager:GetMarkInstRuntimeData(markInstId)
        if ((self.m_isSpaceshipMap and runtimeData.levelId ~= spaceshipLevelId) or
                (not self.m_isSpaceshipMap and runtimeData.levelId == spaceshipLevelId)) then
            isShowTracking = true
            trackingMissionId = runtimeData.missionInfo.missionId
            break
        end
    end
    if isShowTracking then
        local missionColor = GameInstance.player.mission:GetMissionColor(trackingMissionId)
        self.view.mainTrackingIcon.color = missionColor
        self.view.shipTrackingIcon.color = missionColor
        self.view.mainTrackingIcon.gameObject:SetActive(self.m_isSpaceshipMap)
        self.view.shipTrackingIcon.gameObject:SetActive(not self.m_isSpaceshipMap)
    else
        self.view.mainTrackingIcon.gameObject:SetActive(false)
        self.view.shipTrackingIcon.gameObject:SetActive(false)
    end

    self.view.imgBgSpaceship.gameObject:SetActive(not self.m_isSpaceshipMap)
    self.view.imgBgMain.gameObject:SetActive(self.m_isSpaceshipMap)

    if self.m_isSpaceshipMap then
        local levelId = MapSpaceshipNode.s_fromLevelId
        local domainId = MapSpaceshipNode.s_fromDomainId
        if string.isEmpty(levelId) then
            if string.isEmpty(domainId) then
                MapSpaceshipNode.s_fromDomainId = Utils.getCurDomainId()
                domainId = MapSpaceshipNode.s_fromDomainId
            end
            local success, domainData = Tables.domainDataTable:TryGetValue(domainId)
            if success then
                self.view.txtName.text = domainData.domainName
            end
        else
            local success, levelDescData = Tables.levelDescTable:TryGetValue(levelId)
            if success then
                self.view.txtName.text = levelDescData.showName
            end
        end
    end

    self.view.btn.customBindingViewLabelText = self.m_isSpaceshipMap and Language.LUA_MAP_BACK_FROM_SPACESHIP or Language.LUA_MAP_JUMP_TO_SPACESHIP
end

MapSpaceshipNode._ToggleVisibleState = HL.Method(HL.Boolean) << function(self, isVisible)
    self.view.content.gameObject:SetActive(isVisible)
    self.view.btn.enabled = isVisible
end

MapSpaceshipNode._RecordStaticFromData = HL.Method() << function(self)
    if self.m_isSpaceshipMap then
        return
    end
    MapSpaceshipNode.s_fromLevelId = self.m_levelId
    MapSpaceshipNode.s_fromDomainId = self.m_domainId
end

MapSpaceshipNode._ReturnToRegionMap = HL.Method(HL.String) << function(self, targetDomainId)
    MapUtils.switchFromLevelMapToRegionMap(nil, targetDomainId)
end

MapSpaceshipNode._ReturnToLevelMap = HL.Method(HL.String) << function(self, targetLevelId)
    if string.isEmpty(self.m_domainId) then

        PhaseManager:GoToPhase(PhaseId.Map, {
            levelId = targetLevelId
        })
    else
        MapUtils.switchFromRegionMapToLevelMap(nil, targetLevelId)
    end
end

MapSpaceshipNode.ClearStaticFromData = HL.StaticMethod() << function()
    MapSpaceshipNode.s_fromLevelId = ""
    MapSpaceshipNode.s_fromDomainId = ""
end

HL.Commit(MapSpaceshipNode)
return MapSpaceshipNode

