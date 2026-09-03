local wikiDetailBaseCtrl = require_ex('UI/Panels/WikiDetailBase/WikiDetailBaseCtrl')
local PANEL_ID = PanelId.WikiBuilding
WikiBuildingCtrl = HL.Class('WikiBuildingCtrl', wikiDetailBaseCtrl.WikiDetailBaseCtrl)



local DEFAULT_TITLE_KEY = "ui_wiki_building_extra"
local DETAIL_TITLE_TEXT_KEY =
{
    ["wiki_group_building_source"] = "ui_wiki_building_mine",
    ["wiki_group_building_basic"] = "ui_wiki_building_usable_formula",
    ["wiki_group_building_assemble"] = "ui_wiki_building_usable_formula",
    ["wiki_group_building_battle"] = "ui_wiki_building_battle",
    ["wiki_group_building_soil"] = "ui_wiki_building_soil",
}

local HIDE_CRAFT_TREE_GROUP_TABLE =
{
    wiki_group_building_logistic = true,
}



WikiBuildingCtrl.s_overrideMessages = HL.StaticField(HL.Table) << {
    [MessageConst.FAC_ON_UNLOCK_TECH_TREE_UI] = '_FacOnUnlockTechTreeUI',
}



WikiBuildingCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    WikiBuildingCtrl.Super.OnCreate(self, arg)

    GameAction.FacToggleSystemEnabled(true, CS.Beyond.Gameplay.Factory.FacSystemTickSkipSource.UI)

    self:_RefreshModel(true)
    self:_PlayBgDecoAnim()
end

WikiBuildingCtrl.OnShow = HL.Override() << function(self)
    WikiBuildingCtrl.Super.OnShow(self)
    self:_RefreshModel()
    self:_PlayBgDecoAnim()
end

WikiBuildingCtrl.OnClose = HL.Override() << function(self)
    WikiBuildingCtrl.Super.OnClose(self)
    if self.m_phase then
        self.m_phase:ActiveModelRotateRoot(false)
    end





    GameAction.FacScheduleUISystemSkipAfterGraceFrames()
end

WikiBuildingCtrl._FacOnUnlockTechTreeUI = HL.Method(HL.Table) << function(self, args)
    self:_RefreshRight()
end

WikiBuildingCtrl._OnPlayAnimationOut = HL.Override() << function(self)
    WikiBuildingCtrl.Super._OnPlayAnimationOut(self)
    self.m_phase:PlayBgAnim("wiki_plane_tobuilding_out")
end

WikiBuildingCtrl.GetPanelId = HL.Override().Return(HL.Number) << function(self)
    return PANEL_ID
end

WikiBuildingCtrl._RefreshCenter = HL.Override() << function(self)
    WikiBuildingCtrl.Super._RefreshCenter(self)
    self:_RefreshModel()
end

WikiBuildingCtrl.m_isBtnInited = HL.Field(HL.Boolean) << false

WikiBuildingCtrl._RefreshRight = HL.Override() << function(self)
    local view = self.view.right
    local itemId = self.m_wikiEntryShowData.wikiEntryData.refItemId

    if not self.m_isBtnInited then
        self.m_isBtnInited = true
        view.viewBtn.onClick:AddListener(function()
            self:_StartCoroutine(function()
                coroutine.step()
                self.view.right.naviGroup:ManuallyStopFocus()
                self.view.right.controllerFocusHintNode.gameObject:SetActive(true)
            end)
            self.m_phase:CreatePhasePanelItem(PanelId.WikiCraftingTree, {
                wikiEntryShowData = self.m_wikiEntryShowData,
                forceShowBackBtn = true,
            })
        end)
    end
    view.viewBtn.gameObject:SetActive(HIDE_CRAFT_TREE_GROUP_TABLE[self.m_wikiEntryShowData.wikiGroupData.groupId] ~= true)

    self:_RefreshDetail(itemId)

    local isFocusEnabled = self.m_obtainCellCache:GetCount() > 0 or
        ((view.itemObtainWaysForWiki.m_obtainCells:GetCount() > 0 or view.itemObtainWaysForWiki.view.emptyNode.gameObject.activeSelf) and
        view.itemObtainWaysForWiki.view.gameObject.activeSelf)
    view.naviGroup.enabled = isFocusEnabled
    self.view.right.controllerFocusHintNode.gameObject:SetActive(isFocusEnabled)
end

WikiBuildingCtrl.m_craftCellCache = HL.Field(HL.Forward("UIListCache"))

WikiBuildingCtrl.m_obtainCellCache = HL.Field(HL.Forward("UIListCache"))



WikiBuildingCtrl._RefreshDetail = HL.Method(HL.String) << function(self, itemId)
    self.view.right.itemObtainWaysForWiki:InitItemObtainWays(itemId, nil, self.m_itemTipsPosInfo, function(cell, craftCellView)
        self:_OnClickRightItemCell(cell, craftCellView)
    end)
    local lastCraftFirstSelectable, lastCraftNaviGroup = self:_InitItemObtainWaysController(self.view.right.itemObtainWaysForWiki)

    local view = self.view.right.itemDetail






    local modeTypeTable = {}
    for modeType, modeTypeData in pairs(Tables.factoryMachineCraftModeTable) do
        modeTypeTable[modeTypeData.sortId] = modeType
    end


    local modeInfos = {}

    local buildingData = FactoryUtils.getItemBuildingData(itemId)
    if buildingData then
        local machineModeList = FactoryUtils.getBuildingModeListCovered(buildingData.id)
        if next(machineModeList) ~= nil then
            for _, modeData in ipairs(machineModeList) do
                local craftInfos = FactoryUtils.getBuildingCrafts(buildingData.id, nil, nil, modeData.modeName)
                local _, modeTypeData = Tables.factoryMachineCraftModeTable:TryGetValue(modeData.modeName)
                if modeTypeData and craftInfos and next(craftInfos) then

                    local tempTL = {}
                    local temp = {}
                    for _, craftInfo in ipairs(craftInfos) do
                        if FactoryUtils.isTimeLimitedFormula(craftInfo.craftId) then
                            table.insert(tempTL, craftInfo)
                        else
                            table.insert(temp, craftInfo)
                        end
                    end
                    for _, craftInfo in ipairs(temp) do
                        table.insert(tempTL, craftInfo)
                    end
                    table.insert(modeInfos, { modeType = modeData.modeName, craftInfos = tempTL, sortId = modeTypeData.sortId })
                end
            end
            table.sort(modeInfos, Utils.genSortFunction({ "sortId" }, true))
        else
            local craftInfos = FactoryUtils.getBuildingCrafts(buildingData.id)
            if craftInfos and next(craftInfos) then
                table.insert(modeInfos, { modeType = FacConst.FAC_FORMULA_MODE_MAP.NORMAL, craftInfos = craftInfos })
            end
        end
    end
    local modeInfoCount = #modeInfos

    if not self.m_obtainCellCache then
        self.m_obtainCellCache = UIUtils.genCellCache(view.obtainCell)
    end
    self.m_obtainCellCache:Refresh(modeInfoCount, function(obtainCell, index)
        local modeInfo = modeInfos[index]
        obtainCell.titleNode.gameObject:SetActive(modeInfoCount > 1)
        obtainCell.titleNodeToggle:SetIsOnWithoutNotify(true)
        obtainCell.content.gameObject:SetActive(true)

        if modeInfoCount > 1 then
            local _, modeData = Tables.factoryMachineCraftModeTable:TryGetValue(modeInfo.modeType)
            if modeData then
                obtainCell.nameTxt.text = modeData.machineModeTypeName
                local hasIcon = not string.isEmpty(modeData.iconId)
                obtainCell.titleImg.gameObject:SetActive(hasIcon)
                if hasIcon then
                    obtainCell.titleImg:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_COMMON, modeData.iconId)
                end
            end
        end
        obtainCell.titleNodeToggle.onIsNaviTargetChanged = function(isTarget)
            if isTarget then
                self.view.right.scrollRect:ScrollToNaviTarget(obtainCell.titleNodeToggle)
                Notify(MessageConst.HIDE_ITEM_TIPS)
            end
        end
        obtainCell.titleNodeToggle.useExplicitNaviSelect = true
        if lastCraftFirstSelectable and obtainCell.titleNodeToggle.isActiveAndEnabled then
            obtainCell.titleNodeToggle.banExplicitOnUp = false
            obtainCell.titleNodeToggle:SetExplicitSelectOnUp(lastCraftFirstSelectable)
            lastCraftFirstSelectable.useExplicitNaviSelect = true
            lastCraftFirstSelectable.banExplicitOnLeft = true
            lastCraftFirstSelectable.banExplicitOnRight = true
            lastCraftFirstSelectable.banExplicitOnUp = true
            lastCraftFirstSelectable.banExplicitOnDown = false
            lastCraftFirstSelectable:SetExplicitSelectOnDown(obtainCell.titleNodeToggle)
        else
            obtainCell.titleNodeToggle.banExplicitOnUp = true
        end
        if obtainCell.titleNodeToggle.isActiveAndEnabled and lastCraftNaviGroup then
            obtainCell.titleNaviGroup.naviPartnerOnUp:Clear()
            obtainCell.titleNaviGroup.naviPartnerOnDown:Clear()
            lastCraftNaviGroup.naviPartnerOnDown:Add(obtainCell.titleNaviGroup)
            obtainCell.titleNaviGroup.naviPartnerOnUp:Add(lastCraftNaviGroup)
            lastCraftNaviGroup = obtainCell.titleNaviGroup
        end
        obtainCell.titleNodeToggle.onValueChanged:RemoveAllListeners()
        obtainCell.titleNodeToggle.onValueChanged:AddListener(function(isOn)
            if DeviceInfo.usingController then
                for i = 1, obtainCell.m_craftCellCache:GetCount() do
                    local craftCell = obtainCell.m_craftCellCache:GetItem(i)
                    InputManagerInst:ToggleBinding(craftCell.view.pinBtn.view.pinToggle.toggleBindingId, false)
                end
            end

            local preToggleIsOn = false
            local preObtainCell = self.m_obtainCellCache:Get(index - 1)
            if preObtainCell then
                preToggleIsOn = preObtainCell.titleNodeToggle.isOn
            end
            obtainCell.titleNodeToggle.banExplicitOnUp = not preToggleIsOn

            local nextObtainCell = self.m_obtainCellCache:Get(index + 1)
            if nextObtainCell then
                nextObtainCell.titleNodeToggle.banExplicitOnUp = not isOn
            end
            obtainCell.titleNodeToggle.banExplicitOnDown = not isOn
        end)

        if not obtainCell.m_craftCellCache then
            obtainCell.m_craftCellCache = UIUtils.genCellCache(obtainCell.craftCell)
        end
        local firstItemSelectable
        obtainCell.m_craftCellCache:Refresh(#modeInfo.craftInfos, function(craftCell, index)
            local craftInfo = modeInfo.craftInfos[index]
            craftInfo.buildingId = nil
            craftCell.view.outcomeItem = craftCell.view.outcomeItemBlack
            craftCell.view.incomeItem = craftCell.view.incomeItemBlack
            craftCell:InitCraftCell(craftInfo, self.m_itemTipsPosInfo, function(cell)
                self:_OnClickRightItemCell(cell, craftCell.view)
            end)

            local useBGNode = false


            if craftCell.view.stateController then
                if craftInfo.craftId ~= nil then
                    local timeLimited = FactoryUtils.isTimeLimitedFormula(craftInfo.craftId)
                    craftCell.view.stateController:SetState(timeLimited and "LimitedTimeEvent" or "Normal")
                    FactoryUtils.setTimeLimitedFormulaTagColor(craftCell.view.timeLimitedColorTag1, craftInfo.craftId)
                    FactoryUtils.setTimeLimitedFormulaTagColor(craftCell.view.timeLimitedColorTag2, craftInfo.craftId)
                    FactoryUtils.setTimeLimitedFormulaTagColor(craftCell.view.timeLimitedColorTag3, craftInfo.craftId)
                    if timeLimited then
                        useBGNode = true
                    end
                else
                    craftCell.view.stateController:SetState("Normal")
                end
            end


            if craftInfo.env ~= nil and craftInfo.env ~= GEnums.FacEnvGenEnvType.None then
                useBGNode = true
            end

            craftCell.view.bg.gameObject:SetActive(useBGNode)

            if DeviceInfo.usingController then
                InputManagerInst:ToggleBinding(craftCell.view.pinBtn.view.pinToggle.toggleBindingId, false)

                craftCell.view.selectableNaviGroup.naviPartnerOnUp:Clear()
                craftCell.view.selectableNaviGroup.naviPartnerOnDown:Clear()
                if lastCraftNaviGroup then
                    if index == 1 then
                        if not obtainCell.titleNodeToggle.isActiveAndEnabled then
                            lastCraftNaviGroup.naviPartnerOnDown:Add(craftCell.view.selectableNaviGroup)
                            craftCell.view.selectableNaviGroup.naviPartnerOnUp:Add(lastCraftNaviGroup)
                        end
                    else
                        lastCraftNaviGroup.naviPartnerOnDown:Add(craftCell.view.selectableNaviGroup)
                        craftCell.view.selectableNaviGroup.naviPartnerOnUp:Add(lastCraftNaviGroup)
                    end
                end
                lastCraftNaviGroup = craftCell.view.selectableNaviGroup
            end
            craftCell.view.pinKeyHint.gameObject:SetActive(false)
            for i = 1, craftCell.incomeCache:GetCount() do
                local selectable = craftCell.incomeCache:GetItem(i).view.button
                selectable.useExplicitNaviSelect = false
                selectable.onIsNaviTargetChanged = function(isTarget)
                    self:_OnRightItemIsNaviTargetChanged(isTarget, selectable, craftCell.view)
                end
                if firstItemSelectable == nil then
                    firstItemSelectable = selectable
                end
                if i == 1 then
                    lastCraftFirstSelectable = selectable
                end
            end
            for i = 1, craftCell.outcomeItemsCache:GetCount() do
                local selectable = craftCell.outcomeItemsCache:GetItem(i).view.button
                selectable.useExplicitNaviSelect = false
                selectable.onIsNaviTargetChanged = function(isTarget)
                    self:_OnRightItemIsNaviTargetChanged(isTarget, selectable, craftCell.view)
                end






            end
        end)
        if firstItemSelectable and obtainCell.titleNodeToggle.isActiveAndEnabled then
            obtainCell.titleNodeToggle.banExplicitOnDown = false
            obtainCell.titleNodeToggle:SetExplicitSelectOnDown(firstItemSelectable)
            firstItemSelectable.useExplicitNaviSelect = true
            firstItemSelectable.banExplicitOnLeft = true
            firstItemSelectable.banExplicitOnRight = true
            firstItemSelectable.banExplicitOnDown = true
            firstItemSelectable.banExplicitOnUp = false
            firstItemSelectable:SetExplicitSelectOnUp(obtainCell.titleNodeToggle)
        else
            obtainCell.titleNodeToggle.banExplicitOnDown = true
        end
    end)

    local titleTextKey = DETAIL_TITLE_TEXT_KEY[self.m_wikiEntryShowData.wikiGroupData.groupId]
    if not titleTextKey then
        titleTextKey = DEFAULT_TITLE_KEY
    end
    view.obtainTitle.text = Language[titleTextKey]
    local desc = self.m_wikiEntryShowData.wikiEntryData.desc
    view.emptyText.gameObject:SetActive(modeInfoCount == 0 and string.isEmpty(desc))
    view.descTxt.gameObject:SetActive(not string.isEmpty(desc))
    view.descTxt:SetAndResolveTextStyle(desc)
end

WikiBuildingCtrl._RefreshModel = HL.Method(HL.Opt(HL.Boolean)) << function(self, playInAnim)
    if self.m_phase then
        local isShowImg = lume.find(WikiConst.BUILDING_SHOW_IMG_GROUP_ID_LIST, self.m_wikiEntryShowData.wikiGroupData.groupId) ~= nil
        self.view.wikiItemImg.gameObject:SetActive(isShowImg)
        if isShowImg then
            self.m_phase:DestroyModel()
            self.m_phase:ActiveCategorySceneItem(self.m_wikiEntryShowData.wikiCategoryType)
            local cameraDistance = DataManager.wikiModelConfig.buildingDefaultCameraDistance
            self.m_phase:_SetCameraParams(self.m_phase.m_currentCamera.vcam_entry, cameraDistance)
            local sceneScale = 1
            self.m_phase:SetSceneScale(1)
            local factor = DataManager.wikiModelConfig.buildingSceneScaleOffsetFactor
            local sceneOffset = -factor * sceneScale + factor + DataManager.wikiModelConfig.buildingSceneOffsetY * sceneScale
            self.m_phase:SetSceneOffset(sceneOffset)
            self.m_phase.m_sceneRoot.view.ground:SetParent(self.m_phase.m_currentCamera.vcam_entry.transform, true)
            self.m_phase.m_sceneRoot.view.ground.localPosition = self.m_phase.m_sceneRoot.view.config.BUILDING_GROUND_OFFSET
            self.m_phase.m_sceneRoot.view.ground.localScale = self.m_phase.m_sceneRoot.view.config.BUILDING_GROUND_SCALE

            local _, itemData = Tables.itemTable:TryGetValue(self.m_wikiEntryShowData.wikiEntryData.refItemId)
            if itemData then
                self.view.wikiItemImg:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, itemData.iconId)
            end
        else
            self.m_phase:ShowModel(self.m_wikiEntryShowData, { playInAnim = playInAnim })
        end
        self.m_phase:ActiveEntryVirtualCamera(true)
    end
end



WikiBuildingCtrl._PlayBgDecoAnim = HL.Method() << function(self)
    if self.m_phase then
        self.m_phase:PlayBgAnim("wiki_plane_tobuilding_in")
        self.m_phase:PlayDecoAnim("wiki_uideco_grouptobuildingpanel")
    end
end

HL.Commit(WikiBuildingCtrl)