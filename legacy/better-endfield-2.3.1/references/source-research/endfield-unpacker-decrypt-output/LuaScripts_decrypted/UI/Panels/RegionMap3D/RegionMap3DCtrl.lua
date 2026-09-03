
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.RegionMap3D
RegionMap3DCtrl = HL.Class('RegionMap3DCtrl', uiCtrl.UICtrl)

local SWITCH_ANIMATION_IN_FORMAT = "regionmap3d_%s_in"
local SWITCH_ANIMATION_OUT_FORMAT = "regionmap3d_%s_out"







RegionMap3DCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.SWITCH_DOMAIN_MAP] = '_OnSwitchDomainMap',
    [MessageConst.ON_CLICK_REGIONMAP_LOCK] = '_OnClickRegionMapLevelBtn',
}

RegionMap3DCtrl.m_args = HL.Field(HL.Table)

RegionMap3DCtrl.m_domainId = HL.Field(HL.String) << ""

RegionMap3DCtrl.m_loadedRegionMapSetting = HL.Field(HL.Table)

RegionMap3DCtrl.m_levelDataList = HL.Field(HL.Table)

RegionMap3DCtrl.m_loadedRegionMapTransform = HL.Field(HL.Table)

RegionMap3DCtrl.m_initNaviThread = HL.Field(HL.Thread)

RegionMap3DCtrl.m_lastSwitchDomainId = HL.Field(HL.String) << ""

RegionMap3DCtrl.m_nextSwitchDomainId = HL.Field(HL.String) << ""



RegionMap3DCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)

    local args = arg
    self.m_args = args
    self.m_loadedRegionMapSetting = {}
    self.m_loadedRegionMapTransform = {}

    self.m_domainId = args.domainId
    self:_RefreshAll()
    self:_InitRegionMap3DController()
end

RegionMap3DCtrl.OnClose = HL.Override() << function(self)
    if self.m_controllerNaviThread ~= nil then
        self:_ClearCoroutine(self.m_controllerNaviThread)
    end
    if self.m_controllerNaviWaitTimer > 0 then
        self:_ClearTimer(self.m_controllerNaviWaitTimer)
    end
end






RegionMap3DCtrl._OnSwitchDomainMap = HL.Method(HL.Table) << function(self, args)
    self.m_domainId = args.domainId
    self:_PlayMapSwitchAnimation(args.lastDomainId, args.domainId, function()
        self:_RefreshAll()
    end)
end

RegionMap3DCtrl._PlayMapSwitchAnimation = HL.Method(HL.String, HL.String, HL.Function) << function(self, lastDomainId, nextDomainId, onComplete)
    local getAnimationData = function(domainId)
        local success, domainData = Tables.domainDataTable:TryGetValue(domainId)
        if not success then
            return {}
        end
        local regionName = string.lower(domainData.domainMap)
        return {
            animationWrapper = self.view.domainRoot[regionName].animationWrapper,
            animationIn = string.format(SWITCH_ANIMATION_IN_FORMAT, regionName),
            animationOut = string.format(SWITCH_ANIMATION_OUT_FORMAT, regionName)
        }
    end

    local lastAnimationData = getAnimationData(lastDomainId)
    self.m_lastSwitchDomainId = lastDomainId
    self.m_nextSwitchDomainId = nextDomainId
    lastAnimationData.animationWrapper:ClearTween(false)
    lastAnimationData.animationWrapper:PlayWithTween(lastAnimationData.animationOut, function()
        onComplete()
        local currAnimationData = getAnimationData(self.m_nextSwitchDomainId)
        currAnimationData.animationWrapper:ClearTween(false)
        currAnimationData.animationWrapper:PlayWithTween(currAnimationData.animationIn)
    end)
end

RegionMap3DCtrl._OnClickRegionMapLevelBtn = HL.Method() << function(self)
    if DeviceInfo.usingController and not string.isEmpty(self.m_controllerNaviLevelId) then
        self.view.regionMap3DPanel:OnLevelHoverChanged(self.m_controllerNaviLevelId, false)
    end
end




RegionMap3DCtrl._RefreshAll = HL.Method() << function(self)

    local regionMapSetting  = self:_GetDomainRegionMapSetting(self.m_domainId)
    if not regionMapSetting then
        return
    end
    for domainId, loadedRegionMapSetting in pairs(self.m_loadedRegionMapSetting) do
        loadedRegionMapSetting.gameObject:SetActive(domainId == self.m_domainId)
    end

    local regionTransform = self.m_loadedRegionMapTransform[self.m_domainId]
    regionMapSetting:SetLoadedRegionTransform(regionTransform)
    self.m_levelDataList = {}
    self.view.regionMap3DPanel:InitPanel(regionMapSetting)
    regionMapSetting:InitData(CS.Beyond.UI.RegionMapShowType.Map)
    for levelId, cfg in cs_pairs(regionMapSetting.cfg) do
        if cfg.isLoaded then

            local sceneBasicInfo = Utils.wrapLuaNode(cfg.ui)
            if sceneBasicInfo then

                local sceneBasicInfoArgs = {
                    levelId = levelId,
                    onClick = function(clickLevelId)
                        if DeviceInfo.usingController then
                            return
                        end
                        self.view.regionMap3DPanel:OnClickLevelBtn(clickLevelId, "")
                    end,
                    onHoverChanged = function(hoverLevelId, isHover)
                        if DeviceInfo.usingController then
                            return
                        end
                        self.view.regionMap3DPanel:OnLevelHoverChanged(hoverLevelId, isHover)
                    end,
                }
                sceneBasicInfo:InitSceneBasicInfo(sceneBasicInfoArgs)
                self.m_levelDataList[levelId] = sceneBasicInfo
            end
        end
    end

    if DeviceInfo.usingController then
        self:_DelayInitNaviTarget()
    end
end

RegionMap3DCtrl._GetDomainRegionMapSetting = HL.Method(HL.String).Return(CS.Beyond.UI.RegionMapSetting) << function(self, domainId)
    local regionMapSetting = self.m_loadedRegionMapSetting[domainId]
    if regionMapSetting then
        return regionMapSetting
    end
    local _, domainData = Tables.domainDataTable:TryGetValue(domainId)
    if domainData == nil then
        return nil
    end

    local domainPrefab = self:LoadGameObject(string.format(MapConst.UI_DOMAIN_MAP_PATH, domainData.domainMap))
    local domainGo = CSUtils.CreateObject(domainPrefab, self.view.domainRoot[string.lower(domainData.domainMap)].transform)
    local _, regionMapSetting = domainGo:TryGetComponent(typeof(CS.Beyond.UI.RegionMapSetting))
    self.m_loadedRegionMapSetting[domainId] = regionMapSetting
    local domainTransform = domainGo:GetComponent("Transform")
    self.m_loadedRegionMapTransform[domainId] = domainTransform
    return regionMapSetting
end




RegionMap3DCtrl.m_controllerNaviThread = HL.Field(HL.Thread)

RegionMap3DCtrl.m_regionMapGroupId = HL.Field(HL.Number) << -1

RegionMap3DCtrl.m_controllerNaviWaitTimer = HL.Field(HL.Number) << -1

RegionMap3DCtrl.m_controllerNaviLevelId = HL.Field(HL.String) << ""

RegionMap3DCtrl.m_controllerNaviRect = HL.Field(RectTransform)

RegionMap3DCtrl._InitRegionMap3DController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    local _, regionMapCtrl = UIManager:IsOpen(PanelId.RegionMap)
    self.m_regionMapGroupId = regionMapCtrl.view.inputGroup.groupId
    self:BindInputPlayerAction("map_region_confirm", function()
        self:_OnConfirmNaviTargetLevel()
    end, self.m_regionMapGroupId)

    self.m_controllerNaviThread = self:_StartCoroutine(function()
        while true do
            self:_TickNavigateLevelRect()
            coroutine.step()
        end
    end)
end

RegionMap3DCtrl._OnConfirmNaviTargetLevel = HL.Method() << function(self)
    if string.isEmpty(self.m_controllerNaviLevelId) then
        return
    end
    self.view.regionMap3DPanel:OnClickLevelBtn(self.m_controllerNaviLevelId, "")
    AudioAdapter.PostEvent("Au_UI_Button_SceenBasicInfo")
end

RegionMap3DCtrl._DelayInitNaviTarget = HL.Method() << function(self)
    if self.m_initNaviThread ~= nil then
        self.m_initNaviThread = self:_ClearCoroutine(self.m_initNaviThread)
    end
    self.m_initNaviThread = self:_StartCoroutine(function()
        coroutine.step()
        self:_InitNaviTarget()
        self.m_initNaviThread = self:_ClearCoroutine(self.m_initNaviThread)
    end)
end

RegionMap3DCtrl._InitNaviTarget = HL.Method() << function(self)
    local _, initialLevelId = DataManager.uiLevelMapConfig.controllerInitialSelectLevel:TryGetValue(self.m_domainId)
    local currDomainId = Utils.getCurDomainId()
    if currDomainId == self.m_domainId then

        if not string.isEmpty(self.m_args.levelId) then

            if self.m_levelDataList[self.m_args.levelId] ~= nil then
                initialLevelId = self.m_args.levelId
            end
        else

            local currLevelId = GameWorld.worldInfo.curLevelId
            if self.m_levelDataList[currLevelId] then
                initialLevelId = currLevelId
            end
        end
    end

    self:_SetNaviTarget(initialLevelId, true)
end

RegionMap3DCtrl._TickNavigateLevelRect = HL.Method() << function(self)
    if not InputManagerInst:IsGroupEnabled(self.m_regionMapGroupId) then
        return
    end

    local hitLevelId, hitDistance
    local stickValue = InputManagerInst:GetGamepadStickValue(true)
    if stickValue == Vector2.zero then
        return
    end

    if self.m_controllerNaviWaitTimer > 0 then
        return
    end

    if string.isEmpty(self.m_controllerNaviLevelId) then
        return
    end

    local currPosition = self.m_controllerNaviRect.anchoredPosition
    for levelId, basicInfo in pairs(self.m_levelDataList) do
        if levelId ~= self.m_controllerNaviLevelId then
            local rectTransform = basicInfo.rectTransform
            if CS.Beyond.Gameplay.UILevelMapUtils.IsRayHitRect(currPosition, stickValue, rectTransform) then
                if string.isEmpty(hitLevelId) then
                    hitLevelId = levelId
                    hitDistance = Vector2.Dot(stickValue.normalized, rectTransform.anchoredPosition - currPosition)
                else
                    local distance = Vector2.Dot(stickValue.normalized, rectTransform.anchoredPosition - currPosition)
                    if distance < hitDistance then
                        hitLevelId = levelId
                        hitDistance = distance
                    end
                end
            end
        end
    end

    if not string.isEmpty(hitLevelId) then
        self:_SetNaviTarget(hitLevelId)
        AudioAdapter.PostEvent("Au_UI_Toggle_MapRegionSelect")
    end
end

RegionMap3DCtrl._SetNaviTarget = HL.Method(HL.String, HL.Opt(HL.Boolean)) << function(self, levelId, noWait)
    local basicInfo = self.m_levelDataList[levelId]
    if basicInfo == nil then
        return
    end

    if not string.isEmpty(self.m_controllerNaviLevelId) then
        self.view.regionMap3DPanel:OnLevelHoverChanged(self.m_controllerNaviLevelId, false)
    end

    self.m_controllerNaviLevelId = levelId
    self.m_controllerNaviRect = basicInfo.rectTransform

    self.view.regionMap3DPanel:OnLevelHoverChanged(levelId, true)

    if not noWait then
        self.m_controllerNaviWaitTimer = self:_StartTimer(MapConst.MAP_3D_NAVI_THREAD_WAIT_TIME, function()
            self.m_controllerNaviWaitTimer = self:_ClearTimer(self.m_controllerNaviWaitTimer)
        end)
    end
end




HL.Commit(RegionMap3DCtrl)
