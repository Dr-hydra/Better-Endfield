
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.WeaponInfo
local PHASE_WEAPON_INFO_GAME_OBJECT = "WeaponInfo"

local WEAPON_EXHIBIT_PAGE_TYPE_2_PANEL_ID = {
    [UIConst.WEAPON_EXHIBIT_PAGE_TYPE.OVERVIEW] = {
        PanelId.WeaponExhibitOverview
    },
    [UIConst.WEAPON_EXHIBIT_PAGE_TYPE.UPGRADE] = {
        PanelId.WeaponExhibitUpgrade
    },
    [UIConst.WEAPON_EXHIBIT_PAGE_TYPE.GEM] = {
        PanelId.WeaponExhibitGemCard,
        PanelId.WeaponExhibitGem,
    },
    [UIConst.WEAPON_EXHIBIT_PAGE_TYPE.DOCUMENT] = {
        PanelId.WeaponExhibitDocument
    },
    [UIConst.WEAPON_EXHIBIT_PAGE_TYPE.POTENTIAL] = {
        PanelId.WeaponExhibitPotential
    },
}
local WEAPON_DESC_SHOW_CFG = {
    [UIConst.WEAPON_EXHIBIT_PAGE_TYPE.UPGRADE] = false,
    [UIConst.WEAPON_EXHIBIT_PAGE_TYPE.GEM] = false,
    [UIConst.WEAPON_EXHIBIT_PAGE_TYPE.POTENTIAL] = false,
}

local HIDE_GRID_PAGE_TYPE = {
    [UIConst.WEAPON_EXHIBIT_PAGE_TYPE.UPGRADE] = true,
}

local PHASE_ITEMS = {
    PHASE_WEAPON_INFO_GAME_OBJECT,
}





PhaseWeaponInfo = HL.Class('PhaseWeaponInfo', phaseBase.PhaseBase)
PhaseWeaponInfo.m_weaponExhibitInfo = HL.Field(HL.Table)

PhaseWeaponInfo.m_curPageType = HL.Field(HL.Number) << -1

PhaseWeaponInfo.m_weaponDecoBundleList = HL.Field(HL.Table)

PhaseWeaponInfo.m_effectCor = HL.Field(HL.Thread)

PhaseWeaponInfo.m_cameraGroup = HL.Field(HL.Userdata)

PhaseWeaponInfo.m_blendTransitionCor = HL.Field(HL.Thread)

PhaseWeaponInfo.m_hideCamCor = HL.Field(HL.Thread)

PhaseWeaponInfo.m_isBlendExit = HL.Field(HL.Boolean) << false

PhaseWeaponInfo.m_isPanelsInit = HL.Field(HL.Boolean) << false

PhaseWeaponInfo.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.WEAPON_EXHIBIT_PAGE_CHANGE] = { 'OnSelectPageChange', true },
    [MessageConst.ON_GEM_ATTACH] = { "OnGemAttach", true },
    [MessageConst.ON_GEM_DETACH] = { 'OnGemDetach', true },
    [MessageConst.ON_WEAPON_REFINE] = { 'OnWeaponRefine', true },
    [MessageConst.WEAPON_EXHIBIT_BLEND_EXIT] = { '_BlendExitPhase', true },
    [MessageConst.ON_WEAPON_ATTACH_GEM_ENHANCE_MAX] = { 'OnWeaponAttachGemEnhanceMax', true },
}

PhaseWeaponInfo.RotateWeapon = HL.Method(HL.Number) << function(self, deltaX)
    local sceneObject = self.m_gameObject2Item[PHASE_WEAPON_INFO_GAME_OBJECT]
    local sensitivity = 0.1
    local weaponRotateRoot = sceneObject.view.weaponRotateRoot
    weaponRotateRoot.transform:Rotate(weaponRotateRoot.transform.up, - deltaX * sensitivity)
end

PhaseWeaponInfo.ResetWeaponRotation = HL.Method() << function(self)
    local sceneObject = self.m_gameObject2Item[PHASE_WEAPON_INFO_GAME_OBJECT]
    sceneObject.view.weaponRotateRoot.transform.rotation = Quaternion.identity
end

PhaseWeaponInfo._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    CameraManager:EnableUIModelCullingMask(true, "weaponInfo")
end

PhaseWeaponInfo._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if PhaseManager:IsPhaseHaveSceneCamera(args and args.anotherPhaseId) then
        CameraManager:EnableUIModelCullingMask(false, "weaponInfo")
    end

    self.m_hideCamCor = self:_ClearCoroutine(self.m_hideCamCor)
    local isRecovering = PhaseManager.isRecovering
    self.m_hideCamCor = self:_StartCoroutine(function()
        if not isRecovering then
            coroutine.wait(1)
        end
        CameraManager:EnableUIModelCullingMask(false, "weaponInfo")
        self:_ResetVCam()
        self:_ToggleSceneLight(false)
    end)
end

PhaseWeaponInfo.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
    if transitionType == PhaseConst.EPhaseState.TransitionBackToTop then
        if not PhaseManager:IsPhaseHaveSceneCamera(anotherPhaseId) then
            CameraManager:EnableUIModelCullingMask(true, "weaponInfo")
        end
    end

    if transitionType == PhaseConst.EPhaseState.TransitionBackToTop then
        self.m_hideCamCor = PhaseManager:_ClearCoroutine(self.m_hideCamCor)
        self:_ToggleSceneLight(true)
        self:_RefreshVCam(self.m_curPageType)
    end

    if transitionType == PhaseConst.EPhaseState.TransitionIn then
        self.m_hideCamCor = PhaseManager:_ClearCoroutine(self.m_hideCamCor)
        self.m_isBlendExit = true
        UIManager:PreloadPanelAsset(PanelId.WeaponExhibitEmpty, PHASE_ID)
    end
end

PhaseWeaponInfo._InitPanels = HL.Method(HL.Table) << function(self, arg)
    if self.m_isPanelsInit then
        return
    end
    local pageType = arg.pageType
    local pageBefore = self.m_curPageType
    local isFast = arg.isFocusJump == true
    self.m_curPageType = pageType
    self:ResetWeaponRotation()
    self:_RefreshVCam(pageType)
    self:_ToggleWeaponUpgradeDeco(pageType == UIConst.WEAPON_EXHIBIT_PAGE_TYPE.UPGRADE)
    if pageType == UIConst.WEAPON_EXHIBIT_PAGE_TYPE.POTENTIAL then
        self:_ToggleWeaponPotential(true, isFast)
    elseif pageBefore == UIConst.WEAPON_EXHIBIT_PAGE_TYPE.POTENTIAL then
        self:_ToggleWeaponPotential(false, isFast)
    end
    local neededPanels = WEAPON_EXHIBIT_PAGE_TYPE_2_PANEL_ID[pageType]
    for _, panelId in pairs(neededPanels) do
        if not self.m_panel2Item[panelId] then
            self:CreatePhasePanelItem(panelId, {
                pageType = pageType,
                phase = self,
                weaponInfo = {
                    weaponTemplateId = self.m_weaponExhibitInfo.weaponInst.templateId,
                    weaponInstId = self.m_weaponExhibitInfo.weaponInst.instId,
                },
                isFocusJump = arg.isFocusJump,
                stateArg = arg.stateArg,
            })
        else
            UIManager:Show(panelId)
        end
    end
    self:_RefreshGridDeco(pageType)
    self.m_isPanelsInit = true
end

PhaseWeaponInfo.OnSelectPageChange = HL.Method(HL.Table) << function(self, arg)
    local pageType = arg.pageType
    local pageBefore = self.m_curPageType
    local isFast = arg.isFocusJump == true

    self.m_curPageType = pageType

    self:ResetWeaponRotation()
    self:_RefreshVCam(pageType)

    self.m_effectCor = self:_ClearCoroutine(self.m_effectCor)
    self.m_effectCor = self:_StartCoroutine(function()
        local waitOutDuration = 0
        self:_ToggleWeaponUpgradeDeco(pageType == UIConst.WEAPON_EXHIBIT_PAGE_TYPE.UPGRADE)
        local neededPanels = WEAPON_EXHIBIT_PAGE_TYPE_2_PANEL_ID[pageType]

        for panelId, panelItem in pairs(self.m_panel2Item) do
            if panelItem.uiCtrl:IsShow() and (not lume.find(neededPanels, panelId)) then
                local outDuration = panelItem.uiCtrl:GetAnimationOutDuration()

                panelItem.uiCtrl:PlayAnimationOutWithCallback(function()
                    self:CloseCharInfoPanel(panelId)
                end)
                waitOutDuration = math.max(waitOutDuration, outDuration)
            end
        end

        if pageType == UIConst.WEAPON_EXHIBIT_PAGE_TYPE.POTENTIAL then
            self:_ToggleWeaponPotential(true, isFast)
        elseif pageBefore == UIConst.WEAPON_EXHIBIT_PAGE_TYPE.POTENTIAL then
            self:_ToggleWeaponPotential(false, isFast)
        end

        coroutine.wait(waitOutDuration)

        for _, panelId in pairs(neededPanels) do
            if not self.m_panel2Item[panelId] then
                self:CreatePhasePanelItem(panelId, {
                    pageType = pageType,
                    phase = self,
                    weaponInfo = {
                        weaponTemplateId = self.m_weaponExhibitInfo.weaponInst.templateId,
                        weaponInstId = self.m_weaponExhibitInfo.weaponInst.instId,
                    },
                    isFocusJump = arg.isFocusJump
                })
            else
                UIManager:Show(panelId)
            end
        end

        self:_RefreshGridDeco(pageType)
    end)
end

PhaseWeaponInfo._BlendExitPhase = HL.Method(HL.Table) << function(self, arg)
    local curActiveCam = CameraManager.curVirtualCam
    local sceneObject = self.m_gameObject2Item[PHASE_WEAPON_INFO_GAME_OBJECT]
    local blendCamera = sceneObject.view.weaponExhibitBlendCamera

    self.m_isBlendExit = true

    blendCamera.transform.position = curActiveCam.State.RawPosition + sceneObject.view.config.BLEND_CAM_DELTA_POS
    blendCamera.transform.rotation = curActiveCam.State.RawOrientation
    self.m_blendTransitionCor = self:_ClearCoroutine(self.m_blendTransitionCor)
    self.m_blendTransitionCor = self:_StartCoroutine(function()
        blendCamera.gameObject:SetActive(true)

        coroutine.wait(sceneObject.view.config.BLEND_BLACK_SCREEN_WAIT_TIME)

        local maskData = CS.Beyond.Gameplay.UICommonMaskData()
        maskData.notHideCursor = true
        maskData.fadeInTime = sceneObject.view.config.BLEND_BLACK_SCREEN_TIME
        maskData.fadeBeforeTime = 0
        maskData.fadeOutTime = sceneObject.view.config.BLEND_BLACK_SCREEN_TIME
        maskData.fadeInCallback = function()
            if arg.finishCallback then
                arg.finishCallback()
            end
        end
        if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
            maskData.extraData = CS.Beyond.Gameplay.CommonMaskExtraData()
            maskData.extraData.desc = "WeaponInfo"
        end
        GameAction.ShowBlackScreen(maskData)
    end)
end

PhaseWeaponInfo._BlendEnterPhase = HL.Method(HL.Number) << function(self, pageType)
    local sceneObject = self.m_gameObject2Item[PHASE_WEAPON_INFO_GAME_OBJECT]
    local blendCamera = sceneObject.view.weaponExhibitBlendCamera
    local camName = UIConst.WEAPON_EXHIBIT_PAGE_TYPE_2_CAM_NAME[pageType]
    local cameraGroup = self.m_cameraGroup
    if not cameraGroup then
        return
    end

    local targetCam = cameraGroup.view[camName]

    blendCamera.transform.position = targetCam.transform.position + sceneObject.view.config.BLEND_CAM_DELTA_POS
    blendCamera.transform.rotation = targetCam.transform.rotation

    blendCamera.gameObject:SetActive(true)
    self.m_blendTransitionCor = self:_ClearCoroutine(self.m_blendTransitionCor)
    self.m_blendTransitionCor = self:_StartCoroutine(function()
        coroutine.step()
        blendCamera.gameObject:SetActive(false)
    end)
end

PhaseWeaponInfo.CloseCharInfoPanel = HL.Method(HL.Number) << function(self, panelId)
    if not self.m_panel2Item[panelId] then
        return
    end
    self:RemovePhasePanelItemById(panelId)
end

PhaseWeaponInfo._ToggleWeaponPotential = HL.Method(HL.Boolean, HL.Boolean) << function(self, isOn, isFast)
    local sceneObject = self.m_gameObject2Item[PHASE_WEAPON_INFO_GAME_OBJECT]
    local weaponContainer = sceneObject.view.weaponContainer

    local weaponTemplateId = self.m_weaponExhibitInfo.weaponInst.templateId
    local weaponCfg = Tables.weaponBasicTable[weaponTemplateId]

    local potentialAnim
    if isOn then
        potentialAnim = "weapon_scene_potential_in"
        local replaceKey = "POTENTIAL_REPLACE_ANIM_IN_" .. weaponCfg.weaponType:ToString()
        if weaponContainer.config:HasValue(replaceKey) then
            potentialAnim = weaponContainer.config[replaceKey]
        end
    else
        potentialAnim = "weapon_scene_potential_out"
        local replaceKey = "POTENTIAL_REPLACE_ANIM_OUT_" .. weaponCfg.weaponType:ToString()
        if weaponContainer.config:HasValue(replaceKey) then
            potentialAnim = weaponContainer.config[replaceKey]
        end
    end

    if isFast then
        weaponContainer.animation:SeekToPercent(potentialAnim, 1)
    else
        weaponContainer.animation:Play(potentialAnim)
    end
end

PhaseWeaponInfo._RefreshGridDeco = HL.Method(HL.Number) << function(self, pageType)
    local isOn = not HIDE_GRID_PAGE_TYPE[pageType]
    local sceneObject = self.m_gameObject2Item[PHASE_WEAPON_INFO_GAME_OBJECT]
    UIUtils.PlayAnimationAndToggleActive(sceneObject.view.weaponGridDeco, isOn)
end

PhaseWeaponInfo._ToggleWeaponUpgradeDeco = HL.Method(HL.Boolean) << function(self, isOn)
    local sceneObject = self.m_gameObject2Item[PHASE_WEAPON_INFO_GAME_OBJECT]

    UIUtils.PlayAnimationAndToggleActive(sceneObject.view.weaponUpgradeDeco, isOn)
end

PhaseWeaponInfo.OnGemAttach = HL.Method(HL.Table) << function(self, arg)
    self:_ReloadWeaponEffect()
end

PhaseWeaponInfo.OnGemDetach = HL.Method(HL.Table) << function(self, arg)
    self:_ReloadWeaponEffect()
end

PhaseWeaponInfo.OnWeaponAttachGemEnhanceMax = HL.Method(HL.Table) << function(self, arg)
    local weaponInstId = unpack(arg)
    if weaponInstId == self.m_weaponExhibitInfo.weaponInst.instId then
        self:_ReloadWeaponEffect()
    end
end

PhaseWeaponInfo._ReloadWeaponEffect = HL.Method() << function(self)
    local weaponExhibitInfo = self.m_weaponExhibitInfo
    self:_CleanUpWeaponEffect()
    local sceneObject = self.m_gameObject2Item[PHASE_WEAPON_INFO_GAME_OBJECT]
    local spawnRoot = sceneObject.view.weaponRoot
    for i = 1, spawnRoot.childCount do
        local child = spawnRoot:GetChild(CSIndex(i))
        self:_RefreshWeaponDecoEffect(child.gameObject, weaponExhibitInfo.weaponInst.instId)
    end
end

PhaseWeaponInfo.OnWeaponRefine = HL.Method(HL.Table) << function(self, arg)
    local weaponExhibitInfoBefore = self.m_weaponExhibitInfo
    local weaponTemplateId = weaponExhibitInfoBefore.weaponInst.templateId
    local weaponInstId = weaponExhibitInfoBefore.weaponInst.instId

    self.m_weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponTemplateId, weaponInstId)

    self:_InitWeaponModel(self.m_weaponExhibitInfo)
end

PhaseWeaponInfo._OnInit = HL.Override() << function(self)
    PhaseWeaponInfo.Super._OnInit(self)


    UIManager:Open(PanelId.WeaponExhibitEmpty)
end

PhaseWeaponInfo.OnPreLevelStart = HL.StaticMethod() << function()
    PhaseManager:TryCacheGOByName(PHASE_ID, PHASE_WEAPON_INFO_GAME_OBJECT)
end

PhaseWeaponInfo._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)

    local arg = self.arg
    local weaponTemplateId = arg.weaponTemplateId
    local weaponInstId = arg.weaponInstId
    local pageType = arg.pageType or UIConst.WEAPON_EXHIBIT_PAGE_TYPE.OVERVIEW
    local weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponTemplateId, weaponInstId)

    self:_InitGameObject(pageType, arg)
    self:_InitWeaponModel(weaponExhibitInfo)
    self:_InitVCamController(weaponTemplateId)
end

PhaseWeaponInfo._OnActivated = HL.Override() << function(self)
    CameraManager:EnableUIModelCullingMask(true, "weaponInfo")
    local arg = self.arg
    local weaponTemplateId = arg.weaponTemplateId
    local weaponInstId = arg.weaponInstId
    local weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponTemplateId, weaponInstId)

    local lastPageType = self.m_curPageType
    local pageType
    if not self.m_isPanelsInit and arg.pageType and arg.pageType > 0 then
        pageType = arg.pageType
    elseif self.m_curPageType > 0 then
        pageType = self.m_curPageType
    else
        pageType = UIConst.WEAPON_EXHIBIT_PAGE_TYPE.OVERVIEW
    end

    self.m_weaponExhibitInfo = weaponExhibitInfo
    self.m_curPageType = pageType

    UIManager:Hide(PanelId.Touch)

    self:_RefreshVCam(pageType)

    self:_ToggleSceneLight(true)
    self:_SetListCameraDOF()



    if not arg.isFocusJump then
        self:_RefreshWeaponEquipped(weaponExhibitInfo)
    end

    if pageType == lastPageType then
        return
    end
    if self.m_isPanelsInit then
        self:OnSelectPageChange({
            pageType = pageType,
            isFocusJump = arg.isFocusJump
        })
    else
        self:_InitPanels({
            pageType = pageType,
            isFocusJump = arg.isFocusJump,
            stateArg = arg.stateArg,
        })
    end


    if self.m_isBlendExit then
        self.m_isBlendExit = false
        self:_BlendEnterPhase(pageType)
    end
end

PhaseWeaponInfo._OnDeActivated = HL.Override() << function(self)
    Utils.disableCameraDOF()

    UIManager:Hide(PanelId.WeaponExhibitEmpty)
end

PhaseWeaponInfo._OnDestroy = HL.Override() << function(self)
    CameraManager:EnableUIModelCullingMask(false, "weaponInfo")
    self:_CleanUpWeapon()
    self:_RemoveCameraController()

    UIManager:Close(PanelId.WeaponExhibitEmpty)
    UIManager:Show(PanelId.Touch)
end

PhaseWeaponInfo._ToggleSceneLight = HL.Method(HL.Boolean) << function(self, isOn)
    local sceneObject = self.m_gameObject2Item[PHASE_WEAPON_INFO_GAME_OBJECT]
    sceneObject.view.light.gameObject:SetActive(isOn)
end

PhaseWeaponInfo._SetListCameraDOF = HL.Method() << function(self)
    local sceneObject = self.m_gameObject2Item[PHASE_WEAPON_INFO_GAME_OBJECT]
    local listDOFParams = Utils.stringJsonToTable(sceneObject.view.config.LIST_DOF_PARAM)
    local data = CS.HG.Rendering.Runtime.HGDepthOfFieldData(
        listDOFParams.type,
        listDOFParams.nearFocusStart,
        listDOFParams.nearFocusEnd,
        listDOFParams.nearRadius,
        listDOFParams.farFocusStart,
        listDOFParams.farFocusEnd,
        listDOFParams.farRadius
    )
    Utils.enableCameraDOF(data)
end

PhaseWeaponInfo._InitWeaponModel = HL.Method(HL.Table) << function(self, weaponExhibitInfo)
    self:_CleanUpWeapon()
    local weaponTemplateId = weaponExhibitInfo.weaponInst.templateId
    local weaponInstId = weaponExhibitInfo.weaponInst.instId
    local _, weaponConfig = Tables.weaponBasicTable:TryGetValue(weaponTemplateId)
    if not weaponConfig then
        logger.error(string.format("找不到武器[%s]", weaponTemplateId))
        return
    end

    local suc, modelPath = CS.Beyond.Gameplay.WeaponUtil.TryGetWeaponModelByInstId(Utils.getCurrentScope(), weaponInstId)
    if not suc then
        logger.error(string.format("找不到武器modelPath[%s]", weaponTemplateId))
        return
    end
    local weaponPrefab = self.m_resourceLoader:LoadGameObject(modelPath)
    if not weaponPrefab then
        logger.error(string.format("找不到武器Prefab[%s]", weaponTemplateId))
        return
    end

    local res, exhibitData = DataManager.weaponExhibitConfig.weaponExhibitDataDict:TryGetValue(weaponConfig.weaponType)
    if not exhibitData then
        logger.error(string.format("找不到武器[%s]", weaponTemplateId))
        return
    end

    local spawnDataList = exhibitData.spawnDataList
    local sceneObject = self.m_gameObject2Item[PHASE_WEAPON_INFO_GAME_OBJECT]
    local spawnRoot = sceneObject.view.weaponRoot

    for i = 1, spawnRoot.childCount do
        local child = spawnRoot:GetChild(CSIndex(i))
        GameObject.Destroy(child.gameObject)
    end

    self:_CleanUpWeaponEffect()
    for i = 1, spawnDataList.Length do
        local spawnData = spawnDataList[CSIndex(i)]
        local weaponGo = CSUtils.CreateObject(weaponPrefab, sceneObject.view.weaponRoot)
        local entityRenderHelper = weaponGo:GetComponent("EntityRenderHelper")
        if not entityRenderHelper then
            weaponGo:AddComponent(typeof(CS.Beyond.Gameplay.View.EntityRenderHelper))
        end
        weaponGo.transform.localRotation = Quaternion.Euler(spawnData.generateRotationEuler)
        weaponGo.transform.localPosition = spawnData.generateOffset
        weaponGo.transform.localScale = spawnData.generateScale
        weaponGo:SetLayerRecursive(UIConst.UI_MODEL_LAYER)

        self:_RefreshWeaponDecoEffect(weaponGo, weaponInstId)
    end
end

PhaseWeaponInfo._InitVCamController = HL.Method(HL.String) << function(self, weaponTemplateId)
    local _, weaponConfig = Tables.weaponBasicTable:TryGetValue(weaponTemplateId)
    if not weaponConfig then
        logger.error(string.format("找不到武器[%s]", weaponTemplateId))
        return
    end

    local sceneObject = self.m_gameObject2Item[PHASE_WEAPON_INFO_GAME_OBJECT]
    local weaponType = weaponConfig.weaponType
    local res, exhibitData = DataManager.weaponExhibitConfig.weaponExhibitDataDict:TryGetValue(weaponType)
    if not exhibitData then
        logger.error(string.format("找不到武器 ExhibitConfig [%s]", weaponType))
        return
    end

    local cameraGroup = self:CreatePhaseGOItem(exhibitData.cameraGroup, sceneObject.view.sceneCamera.transform)
    if not cameraGroup then
        logger.error(string.format("找不到武器CameraGroup[%s]", weaponType))
        return
    end

    cameraGroup.go:SetLayerRecursive(UIConst.UI_MODEL_LAYER)
    self.m_cameraGroup = cameraGroup
end

PhaseWeaponInfo._ResetVCam = HL.Method() << function(self)
    local camGroup = self.m_cameraGroup
    if not camGroup then
        return
    end
    for _, camName in pairs(UIConst.WEAPON_EXHIBIT_PAGE_TYPE_2_CAM_NAME) do
        camGroup.view[camName].gameObject:SetActive(false)
    end
end

PhaseWeaponInfo._RefreshWeaponEquipped = HL.Method(HL.Table) << function(self, weaponExhibitInfo)
    local sceneObject = self.m_gameObject2Item[PHASE_WEAPON_INFO_GAME_OBJECT]
    local isWeaponEquipped = weaponExhibitInfo.weaponInst.equippedCharServerId and weaponExhibitInfo.weaponInst.equippedCharServerId > 0

    sceneObject.view.weaponEquipMarker.gameObject:SetActive(isWeaponEquipped)
    if isWeaponEquipped then
        local charServerId = weaponExhibitInfo.weaponInst.equippedCharServerId
        local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charServerId)
        local charImage = sceneObject.view.weaponEquipMarker.charImage

        charImage:LoadSprite(UIConst.UI_SPRITE_CHAR_INFO, UIConst.UI_CHAR_INFO_CHAR_BG_PREFIX .. charInfo.templateId)
    end
end

PhaseWeaponInfo._RefreshVCam = HL.Method(HL.Number) << function(self, targetPageType)
    local camName = UIConst.WEAPON_EXHIBIT_PAGE_TYPE_2_CAM_NAME[targetPageType]
    local cameraGroup = self.m_cameraGroup
    if not cameraGroup then
        return
    end

    local isShowDesc = WEAPON_DESC_SHOW_CFG[targetPageType] ~= false
    self:_ToggleWeaponEquippedMarker(isShowDesc)

    self:_ResetVCam()
    cameraGroup.view[camName].gameObject:SetActive(true)
end

PhaseWeaponInfo._ToggleWeaponEquippedMarker = HL.Method(HL.Boolean) << function(self, isOn)
    local sceneObject = self.m_gameObject2Item[PHASE_WEAPON_INFO_GAME_OBJECT]
    local weaponEquipMarker = sceneObject.view.weaponEquipMarker

    UIUtils.PlayAnimationAndToggleActive(weaponEquipMarker.weaponEquipped, isOn)
end

PhaseWeaponInfo._InitGameObject = HL.Method(HL.Number, HL.Table) << function(self, pageType, arg)
    for _, name in ipairs(PHASE_ITEMS) do
        self:CreatePhaseGOItem(name)
    end
end

PhaseWeaponInfo._CleanUpWeapon = HL.Method() << function(self)
    self:_CleanUpWeaponEffect()
end

PhaseWeaponInfo._CleanUpWeaponEffect = HL.Method() << function(self)
    if self.m_weaponDecoBundleList then
        for _, bundle in ipairs(self.m_weaponDecoBundleList) do
            bundle:Dispose(true)
        end
        self.m_weaponDecoBundleList = nil
    end
end

PhaseWeaponInfo._RemoveCameraController = HL.Method() << function(self)
end

PhaseWeaponInfo._RefreshWeaponDecoEffect = HL.Method(HL.Userdata, HL.Number) << function(self, weaponGo, weaponInstId)

    local _, decoDataList = CS.Beyond.Gameplay.WeaponUtil.TryGetWeaponGemDecoEffect(Utils.getCurrentScope(), weaponInstId)
    self.m_weaponDecoBundleList = self.m_weaponDecoBundleList or {}
    local weaponDecoBundle = CS.Beyond.Gameplay.WeaponUtil.SetWeaponDecoEffect(weaponGo.transform, decoDataList)
    table.insert(self.m_weaponDecoBundleList, weaponDecoBundle)
end

PhaseWeaponInfo.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}
    local stateArg = {}
    arg.stateArg = stateArg
    for _, panelItem in pairs(self.m_panel2Item) do
        if HL.TryGet(panelItem.uiCtrl, "GetCurStateArg") then
            local arg = panelItem.uiCtrl:GetCurStateArg()
            lume.extend(stateArg, arg)
        end
    end
    return arg
end

HL.Commit(PhaseWeaponInfo)
