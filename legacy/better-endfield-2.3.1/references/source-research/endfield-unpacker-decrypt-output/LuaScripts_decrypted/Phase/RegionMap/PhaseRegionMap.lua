
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.RegionMap
PhaseRegionMap = HL.Class('PhaseRegionMap', phaseBase.PhaseBase)
local Panels = { PanelId.RegionMap, PanelId.RegionMap3D }




PhaseRegionMap.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_COMMON_BACK_CLICKED] = { 'OnCommonBackClicked', true },
    [MessageConst.ON_CLICK_REGIONMAP_LEVEL_BTN] = { 'OnClickRegionMapLevelBtn' ,true},
    [MessageConst.ON_CLICK_REGIONMAP_LOCK] = { 'OnClickRegionMapLock' ,true},
}







PhaseRegionMap._OnInit = HL.Override() << function(self)
    PhaseRegionMap.Super._OnInit(self)
    if not self.arg then
        self.arg = {
            domainId = Utils.getCurDomainId()
        }
    end
    if not self.arg.domainId then
        local hasValue

        local levelBasicInfo
        hasValue, levelBasicInfo = DataManager.levelBasicInfoTable:TryGetValue(self.arg.levelId)
        if hasValue and not string.isEmpty(levelBasicInfo.domainName) then
            self.arg.domainId = levelBasicInfo.domainName
        else
            self.arg.domainId = Utils.getCurDomainId()
        end
    end
end

PhaseRegionMap.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
    if transitionType == PhaseConst.EPhaseState.TransitionIn then
        UIManager:PreloadPanelAsset(PanelId.RegionMap3D, PHASE_ID)
        local assetPath
            local _, domainData = Tables.domainDataTable:TryGetValue(self.arg.domainId)
            if domainData then
                assetPath = string.format(MapConst.UI_DOMAIN_MAP_PATH, domainData.domainMap)
            end
        if assetPath then
            self.m_resourceLoader:LoadGameObjectAsync(assetPath, function(go)
                logger.info(assetPath, "预载完成")
            end)
        end
    end
    if not fastMode and (transitionType == PhaseConst.EPhaseState.TransitionIn or transitionType == PhaseConst.EPhaseState.TransitionOut) then
        Notify(MessageConst.PREPARE_BLOCK_GLITCH_TRANSITION)
        coroutine.waitForRenderDone()
    end
end



PhaseRegionMap._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if not fastMode then
        Notify(MessageConst.SHOW_BLOCK_GLITCH_TRANSITION)
    end
end


PhaseRegionMap._OnActivated = HL.Override() << function(self)
    UIManager:Hide(PanelId.Touch)
    self:_InitPhaseItems()
end


PhaseRegionMap._OnDeActivated = HL.Override() << function(self)
    if not self.m_behindPopupPhase then
        self:_ClearCameraCfg()
    end
end


PhaseRegionMap._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self.m_behindPopupPhase = false
    if not fastMode then
        if self.m_RegionMap3DPanel and self.m_RegionMap3DPanel.uiCtrl then
            local wrapper = self.m_RegionMap3DPanel.uiCtrl.animationWrapper
            if wrapper and wrapper.curState == CS.Beyond.UI.UIConst.AnimationState.In then












                wrapper:ClearTween(false)
            end
        end



        CameraManager:RemoveMainCamCullingMaskConfig("RegionMap")
        Notify(MessageConst.SHOW_BLOCK_GLITCH_TRANSITION)
    end
    Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
end


PhaseRegionMap._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    local nextPhaseId = args.anotherPhaseId
    self.m_behindPopupPhase = (nextPhaseId == PhaseId.CommonMoneyExchange or nextPhaseId == PhaseId.StaminaPopUp)
end


PhaseRegionMap._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    self.m_behindPopupPhase = false
    self:_InitCameraCfg()
end


PhaseRegionMap._OnDestroy = HL.Override() << function(self)
    UIManager:Show(PanelId.Touch)



    CameraManager:RemoveMainCamCullingMaskConfig("RegionMap")
end





PhaseRegionMap.OnCommonBackClicked = HL.Method() << function (self)
    if self.m_closeLock then
        return
    end
    MapUtils.closeMapRelatedPhase()
end

PhaseRegionMap.m_closeLock = HL.Field(HL.Boolean) << false

PhaseRegionMap.OnClickRegionMapLock = HL.Method() << function (self)
    self.m_closeLock = true
end

PhaseRegionMap.OnClickRegionMapLevelBtn = HL.Method(HL.Table) << function (self, data)
    self.m_RegionMap3DPanel.uiCtrl:OnClickLevelBtn(data.levelId, data.insId)
end



PhaseRegionMap.m_RegionMap3DPanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseRegionMap.m_RegionMapPanel = HL.Field(HL.Forward("PhasePanelItem"))

PhaseRegionMap.m_inited = HL.Field(HL.Boolean) << false

PhaseRegionMap.m_behindPopupPhase = HL.Field(HL.Boolean) << false

PhaseRegionMap._InitPhaseItems = HL.Method() << function(self)
    if not self.m_inited then
        for _, panelId in pairs(Panels) do
            self:CreatePhasePanelItem(panelId, self.arg)
        end
        self.m_RegionMap3DPanel = self:_GetPanelPhaseItem(PanelId.RegionMap3D)
        self.m_RegionMapPanel = self:_GetPanelPhaseItem(PanelId.RegionMap)
        UIManager:Close(PanelId.Map)
        self:_InitCameraCfg()
        Notify(MessageConst.ON_INITIALIZE_REGION_MAP_CONTROLLER, {
            regionMap3dPanelGroupId = self.m_RegionMap3DPanel.uiCtrl.view.inputGroup.groupId
        })
    end
    self.m_inited = true
    self.m_closeLock = false
end

PhaseRegionMap._InitCameraCfg = HL.Method() << function(self)
    CameraManager:SetUICameraPostProcess(true)
    CameraManager:AddUICamCullingMaskConfig("RegionMap", UIConst.LAYERS.UIPP)



    CameraManager:AddMainCamCullingMaskConfig("RegionMap", UIConst.LAYERS.Nothing)
end

PhaseRegionMap._ClearCameraCfg = HL.Method() << function(self)
    CameraManager:SetUICameraPostProcess(false)
    CameraManager:RemoveUICamCullingMaskConfig("RegionMap")
end

PhaseRegionMap.GetCurStateArg = HL.Override().Return(HL.Any) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}
    arg.domainId = self.m_RegionMapPanel.uiCtrl.domainId
    return arg
end


HL.Commit(PhaseRegionMap)
