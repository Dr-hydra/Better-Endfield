local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.FacRegionUpgrade
PhaseFacRegionUpgrade = HL.Class('PhaseFacRegionUpgrade', phaseBase.PhaseBase)





PhaseFacRegionUpgrade.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.OPEN_FAC_REGION_UPGRADE_PANEL] = { '_OnOpenFacRegionUpgradePanel', false },
}

PhaseFacRegionUpgrade.m_loadAsyncActionHelper = HL.Field(HL.Forward("AsyncActionHelper"))

PhaseFacRegionUpgrade.m_cameraData = HL.Field(HL.Userdata)

PhaseFacRegionUpgrade.m_inBlendCamera = HL.Field(HL.Boolean) << false

PhaseFacRegionUpgrade.m_waitLoadRegionEffectCount = HL.Field(HL.Number) << -1

PhaseFacRegionUpgrade.m_loadedRegionEffectList = HL.Field(HL.Table)

PhaseFacRegionUpgrade.m_waitLoadBusEffectCount = HL.Field(HL.Number) << -1

PhaseFacRegionUpgrade.m_loadedBusEffectList = HL.Field(HL.Table)

PhaseFacRegionUpgrade.m_panelCtrl = HL.Field(HL.Forward("FacRegionUpgradeCtrl"))

PhaseFacRegionUpgrade.s_glitchCoroutine = HL.StaticField(HL.Thread)


PhaseFacRegionUpgrade._OnInit = HL.Override() << function(self)
    PhaseFacRegionUpgrade.Super._OnInit(self)
end

























PhaseFacRegionUpgrade.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
    if not fastMode then
        UIManager:PreloadPanelAsset(PanelId.FacRegionUpgrade, PHASE_ID)
    end
end

PhaseFacRegionUpgrade._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if self.m_loadAsyncActionHelper == nil then
        self.m_loadAsyncActionHelper = require_ex("Common/Utils/AsyncActionHelper")(true)
    end
    self.m_loadAsyncActionHelper:Clear()
    self.m_loadAsyncActionHelper:SetOnFinished(function()
        self.m_panelCtrl:OnLoadFinished()
    end)

    self:_EnterFacRegionUpgradeState(fastMode)
end

PhaseFacRegionUpgrade._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self:_LeaveFacRegionUpgradeState(fastMode)
end

PhaseFacRegionUpgrade._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseFacRegionUpgrade._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args) end

PhaseFacRegionUpgrade._EnterFacRegionUpgradeState = HL.Method(HL.Boolean) << function(self, fastMode)
    local levelId = self.arg and self.arg.levelId
    local regionIndex = self.arg and self.arg.regionIndex
    if levelId == nil or regionIndex == nil then
        levelId, regionIndex = unpack(self.arg)
    end
    self.arg = {
        levelId = levelId,
        regionIndex = regionIndex,
        recoverState = self.arg and self.arg.recoverState,
    }


    self.m_panelCtrl = UIManager:AutoOpen(PanelId.FacRegionUpgrade, self.arg)


    self.m_loadAsyncActionHelper:AddAction(function(onComplete)
        self:_AsyncLoadRegionEffects(onComplete)
    end)
    self.m_loadAsyncActionHelper:AddAction(function(onComplete)
        self:_AsyncLoadBusEffects(onComplete)
    end)

    local internalEnter = function()

        local cameraConfig = DataManager.facRegionUpgradeCameraConfig
        local regionSuccess, regionData = cameraConfig.levelData:TryGetValue(levelId)
        if regionSuccess then
            local configSuccess, configData = regionData.regionData:TryGetValue(regionIndex)
            if configSuccess then
                local cameraController = GameAction.SwitchToCamera(
                    "TrackedCamera",
                    configData.enterBlendData.blendTime,
                    configData.enterBlendData.blendStyle,
                    configData.enterBlendData.blendCurve,
                    false
                )
                if cameraController ~= nil then
                    cameraController:StartCameraTrackByName(
                        configData.trackName,
                        configData.trackTweenTime,
                        nil, nil, 0,
                        configData.trackCurve, false
                    )
                    cameraController:SetFinalRotation(Quaternion.Euler(configData.enterRotation))
                end
                self.m_cameraData = configData
            end
        end


        CSFactoryUtil.HideAllFactoryUnitVisibilityRequest()


        GameInstance.remoteFactoryManager.visual:HideFence()


        GameWorld.gameMechManager.linkWireBrain:ToggleLinkWireTrailVisibleState(false)
    end

    if fastMode then
        internalEnter()
    else

        Notify(MessageConst.PREPARE_BLOCK_GLITCH_TRANSITION)
        self.m_loadAsyncActionHelper:AddAction(function(onComplete)
            PhaseFacRegionUpgrade.s_glitchCoroutine = CoroutineManager:StartCoroutine(function()
                coroutine.waitForRenderDone()
                Notify(MessageConst.SHOW_BLOCK_GLITCH_TRANSITION)

                internalEnter()

                CoroutineManager:ClearCoroutine(PhaseFacRegionUpgrade.s_glitchCoroutine)

                onComplete()
            end)
        end)
    end

    self.m_loadAsyncActionHelper:Start()
end

PhaseFacRegionUpgrade._LeaveFacRegionUpgradeState = HL.Method(HL.Boolean) << function(self, fastMode)
    local internalLeave = function()

        if string.isEmpty(self.m_panelCtrl:GetSelectItemId()) then
            if self.m_cameraData ~= nil then

                GameAction.UnloadCamera(
                    "TrackedCamera",
                    self.m_cameraData.exitBlendData.blendTime,
                    self.m_cameraData.exitBlendData.blendStyle,
                    self.m_cameraData.exitBlendData.blendCurve,
                    true
                )
            end
        else

            GameAction.UnloadCamera("TrackedCamera", true)
            self.m_panelCtrl:BlendOutCameraFromSelectItemTarget(true)
        end


        CSFactoryUtil.ShowAllFactoryUnitVisibilityRequest()


        GameInstance.remoteFactoryManager.visual:ShowFence()


        GameWorld.gameMechManager.linkWireBrain:ToggleLinkWireTrailVisibleState(true)


        self:_DisposeRegionEffects()
        self:_DisposeBusEffects()


        if fastMode then
            self.m_panelCtrl:Close()
        else
            self.m_panelCtrl:PlayAnimationOutAndClose()
        end
        self.m_panelCtrl = nil

        AudioAdapter.PostEvent("Au_UI_Menu_FacLevelUp_Close")
    end

    if fastMode then
        internalLeave()
    else

        Notify(MessageConst.PREPARE_BLOCK_GLITCH_TRANSITION)
        PhaseFacRegionUpgrade.s_glitchCoroutine = CoroutineManager:StartCoroutine(function()
            coroutine.waitForRenderDone()
            Notify(MessageConst.SHOW_BLOCK_GLITCH_TRANSITION)

            internalLeave()

            CoroutineManager:ClearCoroutine(PhaseFacRegionUpgrade.s_glitchCoroutine)
        end)
    end
end






PhaseFacRegionUpgrade._OnActivated = HL.Override() << function(self)
end

PhaseFacRegionUpgrade._OnDeActivated = HL.Override() << function(self)
end

PhaseFacRegionUpgrade._OnDestroy = HL.Override() << function(self)
    PhaseFacRegionUpgrade.Super._OnDestroy(self)
    self.m_loadAsyncActionHelper:Clear()
    self.m_loadAsyncActionHelper = nil
end

PhaseFacRegionUpgrade.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}
    if self.m_panelCtrl then
        arg.recoverState = self.m_panelCtrl:GetRecoverStateArg()
    end
    return arg
end






PhaseFacRegionUpgrade._GetFullEffectPath = HL.Method(HL.String).Return(HL.String) << function(self, relativePath)
    return string.format("%s%s.prefab", CS.Beyond.Gameplay.View.FacRegionUpgradeEffectConfig.EFFECT_FOLDER_PATH, relativePath)
end

PhaseFacRegionUpgrade._AsyncLoadRegionEffects = HL.Method(HL.Function) << function(self, onComplete)
    self.m_loadedRegionEffectList = {}
    local levelId = self.arg.levelId
    local regionIndex = self.arg.regionIndex
    local effectConfig = DataManager.facRegionUpgradeEffectConfig
    local regionSuccess, regionData = effectConfig.levelData:TryGetValue(levelId)
    if regionSuccess then
        local listSuccess, effectList = regionData.regionData:TryGetValue(regionIndex)
        if listSuccess then
            self.m_waitLoadRegionEffectCount = effectList.effectList.Count
            for csIndex, effectPath in cs_pairs(effectList.effectList) do
                local level = LuaIndex(csIndex)
                local key = self.m_resourceLoader:LoadGameObjectAsync(self:_GetFullEffectPath(effectPath.effectPath), function(asset)
                    local effect = CSUtils.CreateObject(asset, UIManager.worldObjectRoot.gameObject.transform)
                    self.m_loadedRegionEffectList[level].effectObject = effect
                    self.m_waitLoadRegionEffectCount = self.m_waitLoadRegionEffectCount - 1
                    if self.m_waitLoadRegionEffectCount == 0 then
                        self.m_panelCtrl:InitRegionEffects(self.m_loadedRegionEffectList)
                        onComplete()
                    end
                end)
                self.m_loadedRegionEffectList[level] = {
                    handlerKey = key,
                }
            end
        end
    else
        onComplete()
    end
end

PhaseFacRegionUpgrade._DisposeRegionEffects = HL.Method() << function(self)
    for _, effect in pairs(self.m_loadedRegionEffectList) do
        GameObject.Destroy(effect.effectObject)
    end
    self.m_loadedRegionEffectList = nil
end

PhaseFacRegionUpgrade._AsyncLoadBusEffects = HL.Method(HL.Function) << function(self, onComplete)
    local instKeyList = self.m_panelCtrl:GetBusEffectInstKeyList()
    local needLoadAssetList = {}
    self.m_loadedBusEffectList = {}
    self.m_waitLoadBusEffectCount = 0
    for _, instKey in ipairs(instKeyList) do
        local success, effectPath = DataManager.facRegionUpgradeEffectConfig.busInstData:TryGetValue(instKey)
        if success then
            local path = effectPath.effectPath
            if needLoadAssetList[path] == nil then
                needLoadAssetList[path] = {}
                self.m_waitLoadBusEffectCount = self.m_waitLoadBusEffectCount + 1
            end
            table.insert(needLoadAssetList[path], instKey)
        end
    end

    if next(needLoadAssetList) then
        for assetPath, waitLoadInstKeyList in pairs(needLoadAssetList) do
            local key = self.m_resourceLoader:LoadGameObjectAsync(self:_GetFullEffectPath(assetPath), function(asset)
                for _, instKey in ipairs(waitLoadInstKeyList) do
                    local effect = CSUtils.CreateObject(asset, UIManager.worldObjectRoot.gameObject.transform)
                    self.m_loadedBusEffectList[instKey].effectObject = effect
                end
                self.m_waitLoadBusEffectCount = self.m_waitLoadBusEffectCount - 1
                if self.m_waitLoadBusEffectCount == 0 then
                    self.m_panelCtrl:InitBusEffects(self.m_loadedBusEffectList)
                    onComplete()
                end
            end)
            for _, instKey in ipairs(waitLoadInstKeyList) do
                self.m_loadedBusEffectList[instKey] = {
                    handlerKey = key,
                }
            end
        end
    else
        onComplete()
    end
end

PhaseFacRegionUpgrade._DisposeBusEffects = HL.Method() << function(self)
    local disposedHandlerKeyList = {}
    for _, effect in pairs(self.m_loadedBusEffectList) do
        local handlerKey = effect.handlerKey
        if not disposedHandlerKeyList[handlerKey] then
            disposedHandlerKeyList[handlerKey] = true
        end
        GameObject.Destroy(effect.effectObject)
    end
    self.m_loadedRegionEffectList = nil
end




PhaseFacRegionUpgrade._OnOpenFacRegionUpgradePanel = HL.StaticMethod(HL.Any) << function(args)
    PhaseManager:OpenPhase(PHASE_ID, args)
end

HL.Commit(PhaseFacRegionUpgrade)
