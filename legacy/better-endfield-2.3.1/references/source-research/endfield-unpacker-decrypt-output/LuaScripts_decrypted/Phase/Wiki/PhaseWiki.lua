local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.Wiki
PhaseWiki = HL.Class('PhaseWiki', phaseBase.PhaseBase)



local WIKI_CATEGORY_TO_PANEL_CFG = {
    [WikiConst.EWikiCategoryType.Weapon] = {
        groupPanelId = PanelId.WikiGroup,
        detailPanelId = PanelId.WikiWeapon,
        includeLocked = true,
    },
    [WikiConst.EWikiCategoryType.Equip] = {
        groupPanelId = PanelId.WikiEquipSuit,
        detailPanelId = PanelId.WikiEquip,
    },
    [WikiConst.EWikiCategoryType.Item] = {
        groupPanelId = PanelId.WikiGroup,
        detailPanelId = PanelId.WikiItem,
    },
    [WikiConst.EWikiCategoryType.Monster] = {
        groupPanelId = PanelId.WikiGroup,
        detailPanelId = PanelId.WikiMonster,
    },
    [WikiConst.EWikiCategoryType.Building] = {
        groupPanelId = PanelId.WikiGroup,
        detailPanelId = PanelId.WikiBuilding,
    },
    [WikiConst.EWikiCategoryType.Tutorial] = {
        groupPanelId = nil,
        detailPanelId = PanelId.WikiGuide,
    },
}

local SHOW_MODEL_FUNC = {
    [WikiConst.EWikiCategoryType.Weapon] = "_ShowWeaponModel",
    [WikiConst.EWikiCategoryType.Monster] = "_ShowMonsterModel",
    [WikiConst.EWikiCategoryType.Building] = "_ShowBuildingModel",
}

local MODEL_ROOT_ANIM_NAME = {
    [WikiConst.EWikiCategoryType.Weapon] = "wiki_model_in_weapon",
    [WikiConst.EWikiCategoryType.Monster] = "wiki_model_in_monster",
    [WikiConst.EWikiCategoryType.Building] = "wiki_model_in_building"
}


local SCENE_ITEM_NAME = "WikiModelShow"
local CAMERA_GROUP_NAME = "WikiCameraGroup"
local LIGHT_GROUP_NAME = "WikiLightGroup"
local SCENE_POS = Vector3(0, 600, 0)

local WIKI_ENY_POSE_CONTROLLER_PATH = "Assets/Beyond/DynamicAssets/Gameplay/Prefabs/UIModels/WikiEnyPose/%s.overrideController"









PhaseWiki.s_messages = HL.StaticField(HL.Table) << {


    [MessageConst.SHOW_WIKI_ENTRY] = { 'OnShowWikiEntry', false },



    [MessageConst.SHOW_WIKI_WEAPON_PREVIEW] = { 'OnShowWeaponPreview', false },
}












PhaseWiki.OnShowWikiEntry = HL.StaticMethod(HL.Table) << function(args)
    local categoryId, wikiDetailArgs = PhaseWiki.ProcessArgs(args)
    if not string.isEmpty(categoryId) then
        PhaseManager:GoToPhase(PHASE_ID, { categoryType = categoryId, wikiDetailArgs = wikiDetailArgs })
    else
        local toastTxt = Language.LUA_WIKI_ENTRY_NOT_SUPPORT
        if not string.isEmpty(args.buildingId) then
            toastTxt = Language.LUA_WIKI_BUILDING_NOT_SUPPORT
        elseif not string.isEmpty(args.monsterId) then
            toastTxt = Language.LUA_WIKI_MONSTER_NOT_SUPPORT
        elseif not string.isEmpty(args.itemId) then
            toastTxt = Language.LUA_WIKI_ITEM_NOT_SUPPORT
        end
        Notify(MessageConst.SHOW_TOAST, toastTxt)
    end
end










PhaseWiki.OnShowWeaponPreview = HL.StaticMethod(HL.Table) << function(args)
    PhaseManager:GoToPhase(PHASE_ID, args)
end





PhaseWiki.OnShowItemCraft = HL.Method(HL.Table) << function(self, args)
    PhaseManager:GoToPhase(PHASE_ID, args)
end





PhaseWiki.m_isShowBackBtn = HL.Field(HL.Boolean) << false

PhaseWiki.m_isSceneInit = HL.Field(HL.Boolean) << false





PhaseWiki._OnInit = HL.Override() << function(self)
    PhaseWiki.Super._OnInit(self)
    self.m_activeModelGos = {}
    self.m_modelRequestIdLut = {}
    self.m_animatorControllerCache = {}


    self.m_isShowBackBtn = self.arg == nil
    if self.arg and self.arg.resumeState then
        self.m_isShowBackBtn = self.arg.resumeState.isShowBackBtn
    end


    self.arg = self.arg or {}
    self:_ActivateBlackboxEffect(false)
    CS.HG.Rendering.ScriptBridge.HGRenderBridgeStatics.SetVFXPPPriorityFilterCinematic()
    CS.HG.Rendering.ScriptBridge.HGRenderBridgeStatics.SetSceneDarkEnabled(false)
end

PhaseWiki._OnActivated = HL.Override() << function(self)
    CS.HG.Rendering.ScriptBridge.HGRenderBridgeStatics.SetVFXPPPriorityFilterCinematic()
    CS.HG.Rendering.ScriptBridge.HGRenderBridgeStatics.SetSceneDarkEnabled(false)
end

PhaseWiki._OnDeActivated = HL.Override() << function(self)

end

PhaseWiki._OnDestroy = HL.Override() << function(self)
    PhaseWiki.Super._OnDestroy(self)
    self:DestroyModel()
    self.m_isSceneInit = false
    GameInstance.remoteFactoryManager:ForceCullingExecute()
    self:_ActivateBlackboxEffect(true)
    CS.HG.Rendering.ScriptBridge.HGRenderBridgeStatics.SetVFXPPPriorityFilterNormal()
    CS.HG.Rendering.ScriptBridge.HGRenderBridgeStatics.SetSceneDarkEnabled(true)
end

PhaseWiki._InitAllPhaseItems = HL.Override() << function(self)
    self:_InitSceneRoot()
    self:_OpenByArgs()
end

PhaseWiki._OnRefresh = HL.Override() << function(self)
    PhaseWiki.Super._OnRefresh(self)
    self:_OpenByArgs()
end

PhaseWiki._OpenByArgs = HL.Method() << function(self)
    self:CreateOrShowPhasePanelItem(PanelId.WikiEmpty)
    if self.arg and self.arg.resumeState then
        self:_RestoreByResumeState(self.arg.resumeState)
        self.arg.resumeState = nil
        return
    end
    if self.arg and self.arg.isWeaponPreview then
        self:CreatePhasePanelItem(PanelId.WikiWeaponPreview, self.arg)
    elseif self.arg and self.arg.isItemCraft then
        local wikiEntryShowData = WikiUtils.getWikiEntryShowData(self.arg.itemId, WikiConst.EWikiCategoryType.Item)
        self:CreatePhasePanelItem(PanelId.WikiCraftingTree, { wikiEntryShowData = wikiEntryShowData, craftId = self.arg.craftId })
    else
        self:OpenCategoryByPhaseArgs()
    end
end

PhaseWiki.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = lume.deepCopy(self.arg or {})
    arg.resumeState = self:_CollectResumeState()
    return arg
end

PhaseWiki._CollectResumeState = HL.Method().Return(HL.Table) << function(self)
    return {
        panelStates = self:_CollectVisiblePanelStates(),
        isShowBackBtn = self.m_isShowBackBtn,
    }
end

PhaseWiki._CollectVisiblePanelStates = HL.Method().Return(HL.Table) << function(self)
    local panelStates = {}
    local addedPanelIdMap = {}
    local function tryAddPanel(panelId, arg, needShow)
        if not panelId or addedPanelIdMap[panelId] then
            return
        end
        local panelItem = self:_GetPanelPhaseItem(panelId)
        if not panelItem or not panelItem.uiCtrl then
            return
        end
        if needShow == true and not panelItem.uiCtrl:IsShow(true) then
            return
        end
        table.insert(panelStates, {
            panelId = panelId,
            arg = arg or panelItem.uiCtrl:GetCurPhaseStateArg(),
        })
        addedPanelIdMap[panelId] = true
    end

    local currentDetailArgs = self.m_currentWikiDetailArgs
    local currentGroupArgs = self.m_currentWikiGroupArgs
    local currentCategoryType = currentDetailArgs and currentDetailArgs.categoryType or
        (currentGroupArgs and currentGroupArgs.categoryType or nil)
    local panelCfg = currentCategoryType and WIKI_CATEGORY_TO_PANEL_CFG[currentCategoryType] or nil
    local wikiPanelItem = self:_GetPanelPhaseItem(PanelId.Wiki)
    if wikiPanelItem and wikiPanelItem.uiCtrl then
        tryAddPanel(PanelId.Wiki)
        if panelCfg and panelCfg.groupPanelId then
            local groupPanelItem = self:_GetPanelPhaseItem(panelCfg.groupPanelId)
            if groupPanelItem and groupPanelItem.uiCtrl then
                tryAddPanel(panelCfg.groupPanelId)
            elseif currentDetailArgs then
                local groupArg = lume.deepCopy(currentGroupArgs or {})
                groupArg.categoryType = currentCategoryType
                groupArg.detailPanelId = panelCfg.detailPanelId
                groupArg.includeLocked = panelCfg.includeLocked
                groupArg.wikiEntryShowData = nil
                groupArg.resumeState = groupArg.resumeState or {}
                groupArg.resumeState.selectedGroupId = currentDetailArgs.wikiEntryShowData and
                    currentDetailArgs.wikiEntryShowData.wikiGroupData.groupId or groupArg.resumeState.selectedGroupId
                groupArg.resumeState.focusTab = true
                table.insert(panelStates, {
                    panelId = panelCfg.groupPanelId,
                    arg = groupArg,
                })
                addedPanelIdMap[panelCfg.groupPanelId] = true
            end
        end
        if panelCfg and panelCfg.detailPanelId then
            tryAddPanel(panelCfg.detailPanelId)
        end
    else
        if panelCfg and panelCfg.groupPanelId then
            tryAddPanel(panelCfg.groupPanelId)
        end
        if panelCfg and panelCfg.detailPanelId then
            tryAddPanel(panelCfg.detailPanelId)
        end
    end

    for _, panelId in ipairs(self.panels) do
        if panelId ~= PanelId.WikiEmpty and not addedPanelIdMap[panelId] then
            local panelItem = self:_GetPanelPhaseItem(panelId)
            if panelItem and panelItem.uiCtrl and panelItem.uiCtrl:IsShow(true) then
                tryAddPanel(panelId, nil, true)
            end
        end
    end
    return panelStates
end

PhaseWiki._RestoreByResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    local panelIds = {}
    for _, panelId in ipairs(self.panels) do
        if panelId ~= PanelId.WikiEmpty then
            table.insert(panelIds, panelId)
        end
    end
    for _, panelId in ipairs(panelIds) do
        self:RemovePhasePanelItemById(panelId)
    end

    self.m_currentWikiGroupArgs = nil
    self.m_currentWikiDetailArgs = nil
    self.curSearchKeyword = ""

    local panelStates = resumeState and resumeState.panelStates or nil
    if not panelStates or #panelStates <= 0 then
        self:CreateOrShowPhasePanelItem(PanelId.Wiki)
        return
    end
    for _, panelState in ipairs(panelStates) do
        self:_RestorePanelState(panelState)
    end
end

PhaseWiki._RestorePanelState = HL.Method(HL.Table) << function(self, panelState)
    if not panelState or not panelState.panelId then
        return
    end
    local panelId = panelState.panelId
    local arg = panelState.arg
    arg.phase = self
    self:_SyncWikiStateByPanel(panelId, arg)
    self:CreatePhasePanelItem(panelId, arg)
    if panelId == PanelId.WikiSearch then
        UIManager:SetTopOrder(panelId)
    end
    arg.resumeState = nil
end

PhaseWiki._SyncWikiStateByPanel = HL.Method(HL.Number, HL.Opt(HL.Any)) << function(self, panelId, arg)
    if panelId == PanelId.WikiGroup or panelId == PanelId.WikiEquipSuit then
        self.m_currentWikiGroupArgs = arg
        return
    end
    if self:_IsDetailPanelId(panelId) then
        self.m_currentWikiDetailArgs = arg
    end
end

PhaseWiki._IsDetailPanelId = HL.Method(HL.Number).Return(HL.Boolean) << function(self, panelId)
    if panelId == PanelId.WikiWeaponPreview then
        return false
    end
    for _, panelCfg in pairs(WIKI_CATEGORY_TO_PANEL_CFG) do
        if panelCfg.detailPanelId == panelId then
            return true
        end
    end
    return false
end





PhaseWiki.m_hideCamCor = HL.Field(HL.Thread)

PhaseWiki.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
    self.m_hideCamCor = self:_ClearCoroutine(self.m_hideCamCor)
    if transitionType == PhaseConst.EPhaseState.TransitionBackToTop then
        self.m_cameraGroup:SetActive(true)
        self.m_lightGroup:SetActive(true)
    end
end

PhaseWiki._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)

end

PhaseWiki._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if self.m_restoreHyperlinkPopupCallback ~= nil then
        self.m_restoreHyperlinkPopupCallback()
    end
    self.m_sceneRoot.view.decoAnim:PlayOutAnimation(function()
        if self.m_sceneRoot and not PhaseManager:IsPhaseHaveSceneCamera(args and args.anotherPhaseId) then
            self.m_sceneRoot:SetActive(false)
        end
    end)
end

PhaseWiki._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    local isRecovering = PhaseManager.isRecovering
    self.m_hideCamCor = self:_StartCoroutine(function()
        if not isRecovering then

            coroutine.wait(1)
        end

        local needCameraPhaseIds = {
            [PhaseId.FacBuildListSelect] = true,
            [PhaseId.PRTSStoryCollDetail] = true,
            [PhaseId.ManualCraftPopups] = true,
            [PhaseId.FacDecoObtainWays] = true,
        }


        if args == nil or needCameraPhaseIds[args.anotherPhaseId] == nil then
            self.m_cameraGroup:SetActive(false)
            self.m_lightGroup:SetActive(false)
        end
    end)
end

PhaseWiki._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)

end





PhaseWiki.curSearchKeyword = HL.Field(HL.String) << ""

PhaseWiki.m_currentWikiGroupArgs = HL.Field(HL.Table)

PhaseWiki.m_currentWikiDetailArgs = HL.Field(HL.Table)

PhaseWiki.m_restoreHyperlinkPopupCallback = HL.Field(HL.Function)

PhaseWiki.OpenCategoryByPhaseArgs = HL.Method() << function(self)
    local categoryType = self.arg.categoryType
    local wikiDetailArgs = self.arg.wikiDetailArgs
    if string.isEmpty(categoryType) then
        categoryType, wikiDetailArgs = PhaseWiki.ProcessArgs(self.arg)
        self.m_restoreHyperlinkPopupCallback = self.arg.restoreHyperlinkPopupCallback
    end
    self:OpenCategory(categoryType, wikiDetailArgs)
end

PhaseWiki.OpenCategory = HL.Method(HL.Any, HL.Opt(HL.Table)) << function(self, categoryId, args)
    self.m_currentWikiDetailArgs = args
    local panelCfg = WIKI_CATEGORY_TO_PANEL_CFG[categoryId]
    if panelCfg then

        local wikiGroupArgs = {
            categoryType = categoryId,
            detailPanelId = panelCfg.detailPanelId,
            includeLocked = panelCfg.includeLocked,
        }
        self.m_currentWikiGroupArgs = wikiGroupArgs

        if panelCfg.groupPanelId and not args then
            self:CreateOrShowPhasePanelItem(panelCfg.groupPanelId, wikiGroupArgs)
        else
            self:RemovePhasePanelItemById(PanelId.WikiGroup)
            self:RemovePhasePanelItemById(PanelId.WikiEquipSuit)
            for panelId, panelItem in pairs(self.m_panel2Item) do
                if panelId == PanelId.WikiSearch then
                    if panelItem.uiCtrl:IsShow(true) then
                        panelItem.uiCtrl:Hide()
                    end
                elseif panelId ~= panelCfg.detailPanelId and panelId ~= PanelId.Wiki and panelId ~= PanelId.WikiEmpty then
                    self:RemovePhasePanelItem(panelItem)
                end
            end
            if UIManager:IsShow(panelCfg.detailPanelId) then
                self:_GetPanelPhaseItem(panelCfg.detailPanelId).uiCtrl:Refresh(args)
            else
                self:CreateOrShowPhasePanelItem(panelCfg.detailPanelId, args)
            end
        end
    else
        self.m_currentWikiGroupArgs = nil
        self:CreateOrShowPhasePanelItem(PanelId.Wiki)
    end
end

PhaseWiki.GetCategoryPanelCfg = HL.Method(HL.String).Return(HL.Table) << function(self, categoryId)
    return WIKI_CATEGORY_TO_PANEL_CFG[categoryId]
end

PhaseWiki.ProcessArgs = HL.StaticMethod(HL.Table).Return(HL.String, HL.Table) << function(args)
    if not args then
        return '', nil
    end
    local targetCategoryId, targetGroupId, targetEntryId

    if not string.isEmpty(args.wikiEntryId) then
        targetEntryId = args.wikiEntryId
    elseif not string.isEmpty(args.itemId) then
        targetEntryId = WikiUtils.getWikiEntryIdFromItemId(args.itemId)
    elseif not string.isEmpty(args.buildingId) then
        targetEntryId = WikiUtils.getWikiEntryIdFromItemId(FactoryUtils.getBuildingItemId(args.buildingId))
    elseif not string.isEmpty(args.monsterId) then
        targetEntryId = WikiUtils.getWikiEntryIdFromItemId(args.monsterId)
    end

    if string.isEmpty(targetEntryId) or
        GameInstance.player.wikiSystem:GetWikiEntryState(targetEntryId) == CS.Beyond.Gameplay.WikiSystem.EWikiEntryState.Locked then
        return '', nil
    end

    local _, wikiEntryData = Tables.wikiEntryDataTable:TryGetValue(targetEntryId)
    if wikiEntryData then
        targetEntryId = wikiEntryData.id
        targetGroupId = wikiEntryData.groupId
    end

    if targetGroupId then
        for wikiCategoryType, wikiGroupDataList in pairs(Tables.wikiGroupTable) do
            for _, wikiGroupData in pairs(wikiGroupDataList.list) do
                if wikiGroupData.groupId == targetGroupId then
                    targetCategoryId = wikiCategoryType
                    break
                end
            end
        end
    end

    if targetCategoryId then
        local panelCfg = WIKI_CATEGORY_TO_PANEL_CFG[targetCategoryId]
        if panelCfg then
            local wikiGroupShowDataList, wikiEntryShowData = WikiUtils.getWikiGroupShowDataList(targetCategoryId, targetEntryId, panelCfg.includeLocked)

            local wikiDetailArgs = {
                categoryType = targetCategoryId,
                wikiGroupShowDataList = wikiGroupShowDataList,
                wikiEntryShowData = wikiEntryShowData,
            }
            return targetCategoryId, wikiDetailArgs
        end
    end
    return '', nil
end





PhaseWiki.m_sceneRoot = HL.Field(HL.Forward("PhaseGameObjectItem"))

PhaseWiki.m_cameraGroup = HL.Field(HL.Forward("PhaseGameObjectItem"))

PhaseWiki.m_currentCamera = HL.Field(HL.Table)

PhaseWiki.m_lightGroup = HL.Field(HL.Forward("PhaseGameObjectItem"))





PhaseWiki.m_categorySceneItem = HL.Field(HL.Table)

PhaseWiki.m_currentModelCategory = HL.Field(HL.String) << ''

PhaseWiki._InitSceneRoot = HL.Method() << function(self)
    if self.m_sceneRoot == nil then
        self.m_sceneRoot = self:CreatePhaseGOItem(SCENE_ITEM_NAME)
        self.m_sceneRoot.view.transform.position = SCENE_POS
        self.m_cameraGroup = self:CreatePhaseGOItem(CAMERA_GROUP_NAME, self.m_sceneRoot.view.sceneCamera)
        self.m_lightGroup = self:CreatePhaseGOItem(LIGHT_GROUP_NAME, self.m_sceneRoot.view.sceneLight)
        if UNITY_EDITOR then

            local additionLightGroup = self.m_lightGroup.go:AddComponent(typeof(CS.Beyond.DevTools.AdditionalLightGroup))
            additionLightGroup.savePath = "Assets/Beyond/DynamicAssets/Gameplay/Prefabs/Wiki/"
        end
        self.m_categorySceneItem = {}
        self.m_categorySceneItem[WikiConst.EWikiCategoryType.Weapon] = {
            camera = self.m_cameraGroup.view.weapon,
            light = self.m_lightGroup.view.weapon,
        }
        self.m_categorySceneItem[WikiConst.EWikiCategoryType.Building] = {
            camera = self.m_cameraGroup.view.building,
            light = self.m_lightGroup.view.building,
        }
        self.m_categorySceneItem[WikiConst.EWikiCategoryType.Monster] = {
            camera = self.m_cameraGroup.view.monster,
            light = self.m_lightGroup.view.monster,
        }
        for _, sceneItem in pairs(self.m_categorySceneItem) do
            sceneItem.camera.gameObject:SetActive(false)
            sceneItem.camera.gameObject:SetAllChildrenActiveIfNecessary(false)
            sceneItem.light.gameObject:SetActive(false)
        end
        self.m_isSceneInit = true
    end
end


PhaseWiki.m_modelRequestIdLut = HL.Field(HL.Table)








PhaseWiki.m_activeModelGos = HL.Field(HL.Table)




PhaseWiki.m_animatorControllerCache = HL.Field(HL.Table)

PhaseWiki.m_buildingRenderer = HL.Field(HL.Userdata)

PhaseWiki.m_buildingPhaseItem = HL.Field(HL.Forward("PhaseGameObjectItem"))

PhaseWiki.m_weaponDecoBundleList = HL.Field(HL.Table)

PhaseWiki.m_monsterEffectList = HL.Field(HL.Table)






PhaseWiki.ShowModel = HL.Method(HL.Table, HL.Opt(HL.Table)) << function(self, wikiEntryShowData, extraArgs)
    if not self.m_isSceneInit then
        return
    end
    self.m_currentModelCategory = wikiEntryShowData.wikiCategoryType
    self:ActiveCategorySceneItem(wikiEntryShowData.wikiCategoryType)
    self:ResetModelRotateRoot()
    self:ResetScene()
    local showFunc = SHOW_MODEL_FUNC[wikiEntryShowData.wikiCategoryType]
    if showFunc then
        self[showFunc](self, wikiEntryShowData, extraArgs)
    end
end

PhaseWiki._ShowWeaponModel = HL.Method(HL.Table, HL.Opt(HL.Table)) << function(self, wikiEntryShowData, extraArgs)
    self:DestroyModel()
    local hasValue

    local weaponBasicData
    hasValue, weaponBasicData = Tables.weaponBasicTable:TryGetValue(wikiEntryShowData.wikiEntryData.refItemId)
    if hasValue then
        local isMax = extraArgs.isWeaponRefinedMax == true
        local isGemMax = extraArgs.isWeaponGemMax == true
        local playInAim = extraArgs.playInAnim == true
        local modelPath
        hasValue, modelPath = CS.Beyond.Gameplay.WeaponUtil.TryGetWeaponModelByTemplateId(weaponBasicData.weaponId, isMax)


        local wikiWeaponData
        hasValue, wikiWeaponData = DataManager.wikiModelConfig.weaponDataDict:TryGetValue(weaponBasicData.weaponType)
        if hasValue then
            local spawnDataList = wikiWeaponData.spawnDataList
            for i = 1, spawnDataList.Length do
                local spawnData = spawnDataList[CSIndex(i)]
                self:_LoadModelAsync(modelPath, function(goInfo)

                    local go = goInfo.gameObject
                    go.transform.localPosition = spawnData.position
                    go.transform.localEulerAngles = spawnData.rotation
                    go.transform.localScale = spawnData.scale


                    if isGemMax and #weaponBasicData.weaponSkillList >= 3 then

                        local decoEffectData
                        local hasValue
                        hasValue, decoEffectData = DataManager.weaponDecoEffectConfig.weaponDecoEffectDict:TryGetValue(weaponBasicData.weaponId)
                        if hasValue then
                            local decoDataList = {}
                            table.insert(decoDataList, decoEffectData.gemMaxDeco)

                            self.m_weaponDecoBundleList = self.m_weaponDecoBundleList or {}
                            local weaponDecoBundle = CS.Beyond.Gameplay.WeaponUtil.SetWeaponDecoEffect(go.transform, decoDataList)
                            table.insert(self.m_weaponDecoBundleList, weaponDecoBundle)
                        end
                    end

                    if playInAim then
                        self:PlayModelRootInAnim(WikiConst.EWikiCategoryType.Weapon)
                    end
                end)
            end
        end
    end
end

PhaseWiki.m_curBuildingRendererTemplateId = HL.Field(HL.String) << ''

PhaseWiki._ShowBuildingModel = HL.Method(HL.Table, HL.Opt(HL.Table)) << function(self, wikiEntryShowData, extraArgs)
    local rendererTemplateId
    local _, factoryBuildingItemData = Tables.factoryBuildingItemTable:TryGetValue(wikiEntryShowData.wikiEntryData.refItemId)
    if factoryBuildingItemData then
        rendererTemplateId = factoryBuildingItemData.buildingId
    else
        local _, factoryLogisticItemData = Tables.factoryItem2LogisticIdTable:TryGetValue(wikiEntryShowData.wikiEntryData.refItemId)
        if factoryLogisticItemData then
            rendererTemplateId = factoryLogisticItemData.logisticId
        end
    end
    if rendererTemplateId then
        local model = self.m_sceneRoot.view.buildingECSModel

        local spawnData
        _, spawnData = DataManager.wikiModelConfig.buildingDataDict:TryGetValue(rendererTemplateId)
        if not spawnData then
            spawnData = DataManager.wikiModelConfig.defaultBuildingSpawnData
        end
        model.transform.localPosition = spawnData.position
        model.transform.localEulerAngles = spawnData.rotation
        self:_SetCameraParams(self.m_currentCamera.vcam_entry, spawnData.cameraDistance)
        self:_SetCameraParams(self.m_currentCamera.vcam_show, spawnData.cameraDistance * DataManager.wikiModelConfig.buildingShowCameraDistanceScale)
        local cameraDistance = DataManager.wikiModelConfig.buildingDefaultCameraDistance
        local sceneScale = (spawnData.cameraDistance - cameraDistance) / cameraDistance + 1
        self:SetSceneScale(sceneScale)
        local factor = DataManager.wikiModelConfig.buildingSceneScaleOffsetFactor
        local sceneOffset = -factor * sceneScale + factor + DataManager.wikiModelConfig.buildingSceneOffsetY * sceneScale
        self:SetSceneOffset(sceneOffset)
        self.m_sceneRoot.view.ground:SetParent(self.m_currentCamera.vcam_entry.transform, true)
        self.m_sceneRoot.view.ground.localPosition = self.m_sceneRoot.view.config.BUILDING_GROUND_OFFSET
        self.m_sceneRoot.view.ground.localScale = self.m_sceneRoot.view.config.BUILDING_GROUND_SCALE

        local playInAnim = extraArgs and extraArgs.playInAnim
        if playInAnim then
            self:PlayModelRootInAnim(WikiConst.EWikiCategoryType.Building)
        end

        model.gameObject:SetActiveIfNecessary(true)
        if rendererTemplateId ~= self.m_curBuildingRendererTemplateId then
            model:ChangeTemplate(rendererTemplateId, true, true)
        end
        model:Cutoff(true, 0, 1)

        if playInAnim then
            model:OnEnterWikiBuildingDetailUI()
        end

        self.m_curBuildingRendererTemplateId = rendererTemplateId

        if UNITY_EDITOR then
            DataManager.wikiModelConfig:TryEditModel()
        end
    end
end

PhaseWiki._ShowMonsterModel = HL.Method(HL.Table, HL.Opt(HL.Table)) << function(self, wikiEntryShowData, extraArgs)
    self:DestroyModel()
    local monsterTemplateId = wikiEntryShowData.wikiEntryData.refMonsterTemplateId
    local _, monsterTemplateData = DataManager:TryGetEntityTemplate(Const.ObjectType.Enemy, monsterTemplateId)
    if not monsterTemplateData then
        return
    end

    local modelCompData = monsterTemplateData:FindComponentData(typeof(CS.Beyond.Gameplay.View.ModelComponentData))
    if not modelCompData then
        return
    end
    local modelId = modelCompData.modelId
    if not modelId or string.isEmpty(modelId) then
        return
    end
    local _, modelData = DataManager.modelData:TryGetValue(modelId)
    if not modelData or not modelData.path or string.isEmpty(modelData.path) then
        return
    end

    local playInAim = false
    if extraArgs then
        playInAim = extraArgs.playInAnim == true
    end
    self:_LoadModelAsync(modelData.path, function(enemyModelGoInfo)
        local enemyModelGo = enemyModelGoInfo.gameObject
        local animator = enemyModelGoInfo.animator
        local isSuccess, rigBuilder = enemyModelGo:TryGetComponent(typeof(Unity.Animations.Rigging.RigBuilder))
        if isSuccess then
            rigBuilder.enabled = false
        end
        local clothCpts = enemyModelGo:GetComponentsInChildren(typeof(CS.BeyondDynamicBone.BeyondBoneCloth))
        if clothCpts then
            for i = 0, clothCpts.Length - 1 do
                clothCpts[i].enabled = false
            end
        end
        local _, wikiMonsterSpawnData = DataManager.wikiModelConfig.monsterDataDict:TryGetValue(monsterTemplateId)
        if not wikiMonsterSpawnData then
            wikiMonsterSpawnData = DataManager.wikiModelConfig.defaultMonsterSpawnData
        end
        enemyModelGo.transform.localPosition = wikiMonsterSpawnData.position
        enemyModelGo.transform.localEulerAngles = wikiMonsterSpawnData.rotation
        enemyModelGo.transform.localScale = wikiMonsterSpawnData.scale
        if animator then

            local animatorCtrlAsset = self.m_animatorControllerCache[monsterTemplateId]
            if not animatorCtrlAsset then
                animatorCtrlAsset = self.m_resourceLoader:LoadAnimatorController(string.format(WIKI_ENY_POSE_CONTROLLER_PATH, monsterTemplateId))
                self.m_animatorControllerCache[monsterTemplateId] = animatorCtrlAsset
            end
            if animatorCtrlAsset then
                animator.runtimeAnimatorController = animatorCtrlAsset
                animator:Update(0)
            end
        end

        if wikiMonsterSpawnData.effects and wikiMonsterSpawnData.effects.Length > 0 then
            for i = 0, wikiMonsterSpawnData.effects.Length - 1 do

                local effectData = wikiMonsterSpawnData.effects[i]
                if effectData and not string.isEmpty(effectData.name) then
                    local mountPoint = enemyModelGo.transform
                    if not string.isEmpty(effectData.mountPoint) then
                        mountPoint = enemyModelGo.transform:FindRecursive(effectData.mountPoint)
                    end
                    if mountPoint then

                        local mountPointHelper = Unity.GameObject("mountPointHelper").transform
                        mountPointHelper:SetParent(mountPoint, false)
                        mountPointHelper.localPosition = effectData.offset
                        mountPointHelper.localEulerAngles = effectData.rotation
                        mountPointHelper.localScale = effectData.scale
                        local scaleTransform = nil
                        if effectData.followScale then
                            scaleTransform = mountPointHelper
                        end
                        local effect = GameInstance.effectManager:CreateEffectOnTransform(
                            effectData.name, mountPointHelper, effectData.followRotation, scaleTransform)
                        self.m_monsterEffectList = self.m_monsterEffectList or {}
                        table.insert(self.m_monsterEffectList, effect)
                    end
                end
            end
        end

        if wikiMonsterSpawnData.hideNodeNames and wikiMonsterSpawnData.hideNodeNames.Length > 0 then
            for i = 0, wikiMonsterSpawnData.hideNodeNames.Length - 1 do
                local hideNodeName = wikiMonsterSpawnData.hideNodeNames[i]
                if hideNodeName and not string.isEmpty(hideNodeName) then
                    local t = enemyModelGo.transform:FindRecursiveWithName(hideNodeName)
                    if t then
                        t.gameObject:SetActive(false)
                    end
                end
            end
        end

        if playInAim then
            self:PlayModelRootInAnim(WikiConst.EWikiCategoryType.Monster)
        end
    end)
end

PhaseWiki._LoadModelAsync = HL.Method(HL.String, HL.Function) << function(self, modelPath, callback)
    local m_modelRequestId
    m_modelRequestId = self.modelLoader:LoadModelAsync(modelPath, self.m_sceneRoot.view.modelRoot, function(activeModelGo)
        if m_modelRequestId then
            self.m_modelRequestIdLut[m_modelRequestId] = nil
        end
        if activeModelGo and callback then


            local activeModelInfo = {
                gameObject = activeModelGo
            }

            local success, animator = activeModelGo:TryGetComponent(typeof(CS.UnityEngine.Animator))
            if success then
                activeModelInfo.animator = animator
                activeModelInfo.animatorController = animator.runtimeAnimatorController
            end
            table.insert(self.m_activeModelGos, activeModelInfo)
            callback(activeModelInfo)
            if UNITY_EDITOR then
                self:_StartCoroutine(function()
                    coroutine.step()
                    coroutine.step()
                    DataManager.wikiModelConfig:TryEditModel()
                end)
            end
        end
    end)
    if m_modelRequestId then
        self.m_modelRequestIdLut[m_modelRequestId] = true
    end
end

PhaseWiki._SetCameraParams = HL.Method(HL.Userdata, HL.Number) << function(self, vcam, cameraDistance)
    local vcamTransform = vcam.transform
    local cameraPosition = vcamTransform.localPosition
    cameraPosition.z = cameraDistance
    vcamTransform.localPosition = cameraPosition
end

PhaseWiki.DestroyModel = HL.Method() << function(self)
    if not self.m_isSceneInit then
        return
    end
    self.m_currentModelCategory = ''
    if self.m_weaponDecoBundleList then
        for _, bundle in ipairs(self.m_weaponDecoBundleList) do
            bundle:Dispose(true)
        end
        self.m_weaponDecoBundleList = nil
    end

    if self.m_monsterEffectList then
        for _, effect in ipairs(self.m_monsterEffectList) do
            effect:Lock():Finish(true)
        end
        self.m_monsterEffectList = nil
    end

    for requestId, _ in pairs(self.m_modelRequestIdLut) do
        self.modelLoader:Cancel(requestId)
        self.m_modelRequestIdLut[requestId] = nil
    end

    for index, activeModelInfo in pairs(self.m_activeModelGos) do
        local activeModel = activeModelInfo.gameObject

        local animator = activeModelInfo.animator
        if animator then
            animator.runtimeAnimatorController = activeModelInfo.animatorController
        end
        activeModel.transform.localScale = Vector3.one
        self.modelLoader:UnloadModel(activeModel)
        self.m_activeModelGos[index] = nil
    end

    if IsNull(self.m_sceneRoot.view.gameObject) then
        return
    end


    self.m_sceneRoot.view.buildingECSModel:Cutoff(false)
    self.m_sceneRoot.view.buildingECSModel:OnLeaveWikiBuildingDetailUI()
    self.m_sceneRoot.view.buildingECSModel.gameObject:SetActiveIfNecessary(false)
    self.m_curBuildingRendererTemplateId = ''
end

PhaseWiki.SetSceneScale = HL.Method(HL.Number) << function(self, scale)
    if not self.m_isSceneInit then
        return
    end
    self.m_sceneRoot.view.sceneRoot.transform.localScale = Vector3.one * scale
end

PhaseWiki.SetSceneOffset = HL.Method(HL.Number) << function(self, offsetY)
    if not self.m_isSceneInit then
        return
    end
    local pos = self.m_sceneRoot.view.sceneRoot.transform.localPosition
    pos.y = offsetY
    self.m_sceneRoot.view.sceneRoot.transform.localPosition = pos
end

PhaseWiki.RotateModel = HL.Method(HL.Number) << function(self, deltaX)
    if not self.m_isSceneInit then
        return
    end
    local rotateRoot = self.m_sceneRoot.view.modelRotateRoot.transform
    rotateRoot:Rotate(rotateRoot.up, -deltaX)
    if self.m_currentModelCategory == WikiConst.EWikiCategoryType.Building then
        self.m_sceneRoot.view.buildingECSModel:SyncTransform()
    end
end

PhaseWiki.ResetModelRotateRoot = HL.Method() << function(self)
    if not self.m_isSceneInit then
        return
    end
    self:ActiveModelRotateRoot(true)
    self.m_sceneRoot.view.modelRotateRoot.transform.localPosition = Vector3.zero
    self:ResetModelRotation()
end

PhaseWiki.ResetModelRotation = HL.Method() << function(self)
    if not self.m_isSceneInit then
        return
    end
    self.m_sceneRoot.view.modelRotateRoot.transform.localEulerAngles = Vector3.zero
end

PhaseWiki.ActiveModelRotateRoot = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, active, noResetScene)
    if not self.m_isSceneInit or IsNull(self.m_sceneRoot.go) then
        return
    end
    if self.m_sceneRoot.view.modelRotateRoot.gameObject.activeSelf ~= active then
        self.m_sceneRoot.view.modelRotateRoot.gameObject:SetActive(active)
        local buildingModel = self.m_sceneRoot.view.buildingECSModel
        if buildingModel.gameObject.activeSelf then
            if active then
                buildingModel:OnEnterWikiBuildingDetailUI()
            else
                buildingModel:OnLeaveWikiBuildingDetailUI()
            end
        end
    end

    if not active and noResetScene ~= true then
        self:ResetScene()
    end

    if self.m_monsterEffectList then
        for _, effect in ipairs(self.m_monsterEffectList) do
            effect:Lock():SetVisible(active)
        end
    end

    if self.m_weaponDecoBundleList then
        for _, weaponDecoBundle in pairs(self.m_weaponDecoBundleList) do
            weaponDecoBundle:ToggleDecoEffect(active)
        end
    end
end

PhaseWiki.ResetScene = HL.Method() << function(self)
    if not self.m_isSceneInit or IsNull(self.m_sceneRoot.go) then
        return
    end
    self:SetSceneScale(1)
    self:SetSceneOffset(0)
    self.m_sceneRoot.view.ground:SetParent(self.m_sceneRoot.view.bgAnim.transform, true)
    self.m_sceneRoot.view.ground.localPosition = Vector3.zero
    self.m_sceneRoot.view.ground.localScale = self.m_sceneRoot.view.config.DEFAULT_GROUND_SCALE
end

PhaseWiki.PlayModelRootInAnim = HL.Method(HL.String) << function(self, modelType)
    local animWrapper = modelType == WikiConst.EWikiCategoryType.Building and
        self.m_sceneRoot.view.buildingModelAnimWrapper or
        self.m_sceneRoot.view.modelRootAnimWrapper
    animWrapper:Play(MODEL_ROOT_ANIM_NAME[modelType])
end

PhaseWiki.ActiveEntryVirtualCamera = HL.Method(HL.Boolean) << function(self, active)
    if not IsNull(self.m_currentCamera) and self.m_isSceneInit then
        self.m_currentCamera.vcam_entry.gameObject:SetActive(active)
    end
end

PhaseWiki.ActiveShowVirtualCamera = HL.Method(HL.Boolean) << function(self, active)
    if not IsNull(self.m_currentCamera) and self.m_isSceneInit then
        self.m_currentCamera.vcam_show.gameObject:SetActive(active)

        if self.m_currentModelCategory == WikiConst.EWikiCategoryType.Building then
            local parent = active and self.m_currentCamera.vcam_show.transform or self.m_currentCamera.vcam_entry.transform
            self.m_sceneRoot.view.ground:SetParent(parent, true)
            self.m_sceneRoot.view.ground.localPosition = self.m_sceneRoot.view.config.BUILDING_GROUND_OFFSET
            self.m_sceneRoot.view.ground.localScale = self.m_sceneRoot.view.config.BUILDING_GROUND_SCALE
        end
    end
end

PhaseWiki.ActiveCategorySceneItem = HL.Method(HL.String) << function(self, categoryType)
    if not self.m_isSceneInit then
        return
    end
    self:ActiveMainSceneItem(false)
    self:ActiveCommonSceneItem(false)
    local camera
    for id, sceneItem in pairs(self.m_categorySceneItem) do
        local isActivated = id == categoryType
        sceneItem.camera.gameObject:SetActive(isActivated)
        sceneItem.light.gameObject:SetActive(isActivated)
        if isActivated then
            camera = sceneItem.camera
        end
    end

    self.m_currentCamera = camera
end

PhaseWiki.ActiveMainSceneItem = HL.Method(HL.Boolean) << function(self, active)
    if IsNull(self.m_lightGroup.go) or IsNull(self.m_cameraGroup.go) then
        return
    end
    if active then
        self:ActiveCategorySceneItem('')
        self:ActiveCommonSceneItem(false)
    end
    self.m_lightGroup.view.common.gameObject:SetActive(active)
    self.m_cameraGroup.view.main.gameObject:SetActive(active)
end

PhaseWiki.ActiveCommonSceneItem = HL.Method(HL.Boolean) << function(self, active)
    if IsNull(self.m_lightGroup.go) or IsNull(self.m_cameraGroup.go) then
        return
    end
    if active then
        self:ActiveCategorySceneItem('')
        self:ActiveMainSceneItem(false)
    end
    self.m_lightGroup.view.common.gameObject:SetActive(active)
    self.m_cameraGroup.view.common.gameObject:SetActive(active)
end

PhaseWiki.m_blackBoxEffectActive = HL.Field(HL.Any)

PhaseWiki._ActivateBlackboxEffect = HL.Method(HL.Boolean) << function(self, active)
    if not Utils.isInBlackbox() then
        return
    end
    if not GameInstance.remoteFactoryManager.blackboxIntroEffectController or
        not GameInstance.remoteFactoryManager.blackboxIntroEffectController.effect or
        not GameInstance.remoteFactoryManager.blackboxIntroEffectController.effect.ActivateVFXPPBlackBox then
        return
    end
    if active == self.m_blackBoxEffectActive then
        return
    end
    self.m_blackBoxEffectActive = active
    GameInstance.remoteFactoryManager.blackboxIntroEffectController.effect:ActivateVFXPPBlackBox(active)
end

PhaseWiki.PlayDecoAnim = HL.Method(HL.String) << function(self, animName)
    if not self.m_isSceneInit or not self:_CheckAllTransitionDone() then
        return
    end
    self.m_sceneRoot.view.decoAnim:ClearTween()
    self.m_sceneRoot.view.decoAnim:Play(animName)
end

PhaseWiki.PlayBgAnim = HL.Method(HL.String) << function(self, animName)
    if not self.m_isSceneInit then
        return
    end
    self.m_sceneRoot.view.bgAnim:ClearTween()
    self.m_sceneRoot.view.bgAnim:Play(animName)
end



HL.Commit(PhaseWiki)
