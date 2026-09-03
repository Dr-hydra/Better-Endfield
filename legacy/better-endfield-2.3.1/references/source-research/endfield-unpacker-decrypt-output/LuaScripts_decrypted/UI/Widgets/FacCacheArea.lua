local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FacCacheArea = HL.Class('FacCacheArea', UIWidgetBase)

FacCacheArea.m_onInitializeFinished = HL.Field(HL.Function)

FacCacheArea.hasNormalCacheIn = HL.Field(HL.Boolean) << false

FacCacheArea.hasFluidCacheIn = HL.Field(HL.Boolean) << false

FacCacheArea.hasGasCacheIn = HL.Field(HL.Boolean) << false

FacCacheArea.m_isActivator = HL.Field(HL.Boolean) << false


FacCacheArea._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_START_UI_DRAG, function(dragHelper)
        self:OnStartUiDrag(dragHelper)
    end)
    self:RegisterMessage(MessageConst.ON_END_UI_DRAG, function(dragHelper)
        self:OnEndUiDrag(dragHelper)
    end)
end

FacCacheArea._OnDestroy = HL.Override() << function(self)
    if self.m_currDragHelper then
        self:_ClearMoveToInCacheSlotBinding(true)
    end
end

FacCacheArea.InitFacCacheArea = HL.Method(HL.Table) << function(self, areaData)
    self:_FirstTimeInit()
    if areaData == nil then
        return
    end

    self.m_buildingInfo = areaData.buildingInfo
    self.m_inRepositoryChangedCallback = areaData.inChangedCallback or function()end
    self.m_outRepositoryChangedCallback = areaData.outChangedCallback or function()end
    self.m_onInitializeFinished = areaData.onInitializeFinished or function()end
    self.m_onIsInCacheAreaNaviGroup = areaData.onIsInCacheAreaNaviGroup or function(isIn)end
    self.m_inRepositoryList = {}
    self.m_outRepositoryList = {}

    self.m_isActivator = FactoryUtils.getBuildingTypeByBuildingId(self.m_buildingInfo.buildingId) == GEnums.FacBuildingType.MachineWithActivator
    self.view.decoActivatorAnimation.gameObject:SetActive(self.m_isActivator)
    self.view.decoArrowAnimation.gameObject:SetActive(not self.m_isActivator)

    self:_InitAreaRepositoryList(true)
    self:_InitCacheAreaController()
    if self.m_onInitializeFinished ~= nil then
        self.m_onInitializeFinished()
    end
end




FacCacheArea.m_buildingInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.BuildingUIInfo_Producer)






FacCacheArea.m_inRepositoryList = HL.Field(HL.Table)

FacCacheArea.m_outRepositoryList = HL.Field(HL.Table)

FacCacheArea.m_inRepositoryChangedCallback = HL.Field(HL.Function)

FacCacheArea.m_outRepositoryChangedCallback = HL.Field(HL.Function)

FacCacheArea._InitAreaRepositoryList = HL.Method(HL.Opt(HL.Boolean)) << function(self, needDelayInit)
    local layoutData = FactoryUtils.getMachineCraftCacheLayoutData(self.m_buildingInfo.nodeId)

    if layoutData == nil then
        return
    end

    local viewRepoList = {
        self.view.inRepositoryList.repository1,
        self.view.inRepositoryList.repository2,
        self.view.outRepositoryList.repository1,
        self.view.outRepositoryList.repository2,
    }
    for _, repo in ipairs(viewRepoList) do
        repo:ClearFluidSlotShaderOnChangeMode()
        repo.gameObject:SetActive(false)
    end

    self.m_inRepositoryList = {}
    self.m_outRepositoryList = {}

    if layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal] > 0 then
        self.hasNormalCacheIn = true
    end
    if layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid] > 0 then
        self.hasFluidCacheIn = true
        self.hasGasCacheIn = true
    else
        self.hasFluidCacheIn = layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid] > 0
        self.hasGasCacheIn = layoutData.inSlotCountByType[FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas] > 0
    end

    if needDelayInit then
        self:_StartCoroutine(function()
            coroutine.step()
        end)
    end
    self:_InitAreaRepositoryListByCaches(layoutData.normalIncomeCaches, true, false)
    self:_InitAreaRepositoryListByCaches(layoutData.normalOutcomeCaches, false, false)
    self:_InitAreaRepositoryListByCaches(layoutData.liquidIncomeCaches, true, true)
    self:_InitAreaRepositoryListByCaches(layoutData.liquidOutcomeCaches, false, true)
end

FacCacheArea._InitAreaRepositoryListByCaches = HL.Method(HL.Table, HL.Boolean, HL.Boolean) << function(self, caches, isIn, isFluid)
    local repoList = isIn and self.m_inRepositoryList or self.m_outRepositoryList
    local viewRepoList = isIn and self.view.inRepositoryList or self.view.outRepositoryList
    local lockFormulaId = FactoryUtils.getMachineCraftLockFormulaId(self.m_buildingInfo.nodeId)

    for cacheIndex, cache in ipairs(caches) do
        local viewRepoName = string.format("repository%d", #repoList + 1)
        local repo = viewRepoList[viewRepoName]
        if repo ~= nil then
            repo:InitFacCacheRepository({
                cache = self.m_buildingInfo:GetCache(cacheIndex, isIn, isFluid),
                isInCache = isIn,
                cacheType = cache.cacheType,
                cacheIndex = cacheIndex,
                slotCount = cache.slotCount,
                formulaId = self.m_buildingInfo.formulaId,
                lastFormulaId = self.m_buildingInfo.lastFormulaId,
                lockFormulaId = lockFormulaId,
                cacheChangedCallback = isIn and self.m_inRepositoryChangedCallback or self.m_outRepositoryChangedCallback,
                producerInfo = self.m_buildingInfo,
                forceUpdateOutRepoWithFormula = true,
            })
            repo.gameObject:SetActive(true)

            table.insert(repoList, repo)
        end
    end
end

FacCacheArea._GetAreaRepositoryItemCount = HL.Method(HL.Table, HL.Boolean).Return(HL.Number) << function(self, crafts, isIn)
    if crafts == nil then
        return 0
    end


    local result = 0
    for _, craftInfo in pairs(crafts) do
        local itemInfoList = isIn and craftInfo.incomes or craftInfo.outcomes
        if itemInfoList ~= nil then
            local count = 0
            for _, itemInfo in pairs(itemInfoList) do
                if itemInfo ~= nil and not string.isEmpty(itemInfo.id) then
                    count = count + 1
                end
            end
            if count > result then
                result = count
            end
            if result > 0 and result ~= count then
                logger.error("FacCacheArea: 当前机器配方格式不一致")
                break
            end
        end
    end

    return result
end

FacCacheArea._GetAreaRepositorySlotGroup = HL.Method(HL.Number, HL.Table).Return(HL.Table) << function(self, cacheType, repoList)
    local slotGroup = {}
    if repoList == nil then
        return slotGroup
    end

    for _, repo in ipairs(repoList) do
        if FactoryUtils.isCacheTypeAcceptItemType(cacheType, repo:GetRepoCacheType()) then
            table.insert(slotGroup, repo:GetRepositorySlotList())
        end
    end

    return slotGroup
end






FacCacheArea.ChangedFormula = HL.Method(HL.String, HL.String) << function(self, formulaId, lastFormulaId)
    for _, repo in pairs(self.m_inRepositoryList) do
        repo:UpdateRepositoryFormula(formulaId, lastFormulaId)
    end
    for _, repo in pairs(self.m_outRepositoryList) do
        repo:UpdateRepositoryFormula(formulaId, lastFormulaId)
    end
end






FacCacheArea.m_moveConfirmBindingId = HL.Field(HL.Number) << -1

FacCacheArea.m_moveCancelBindingId = HL.Field(HL.Number) << -1

FacCacheArea.m_currDragHelper = HL.Field(HL.Any)

FacCacheArea.m_currMoveSourceItem = HL.Field(HL.Userdata)

FacCacheArea.m_onIsInCacheAreaNaviGroup = HL.Field(HL.Function)

FacCacheArea._InitCacheAreaController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.m_moveConfirmBindingId = UIUtils.bindInputPlayerAction("fac_move_to_in_cache_slot_confirm", function()
        self:_OnNaviTargetMoveToSelectedSlotConfirm()
        self:_ClearMoveToInCacheSlotBinding()
    end, self.view.inputGroup.groupId)
    self.m_moveCancelBindingId = UIUtils.bindInputPlayerAction("common_cancel", function()
        self:_ClearMoveToInCacheSlotBinding()
    end, self.view.inputGroup.groupId)

    InputManagerInst:ToggleBinding(self.m_moveConfirmBindingId, false)
    InputManagerInst:ToggleBinding(self.m_moveCancelBindingId, false)

    self.view.inRepositoryList.inRepoNaviGroup.onIsTopLayerChanged:RemoveAllListeners()
    self.view.inRepositoryList.inRepoNaviGroup.onIsTopLayerChanged:AddListener(function(isTop)
        if self.m_onIsInCacheAreaNaviGroup ~= nil then
            self.m_onIsInCacheAreaNaviGroup(isTop or self.view.outRepositoryList.outRepoNaviGroup.IsTopLayer)
        end
    end)
    self.view.inRepositoryList.inRepoNaviGroup.getDefaultSelectableFunc = function()
        local repo = self.m_inRepositoryList[#self.m_inRepositoryList]
        return repo:GetFirstSlotNaviTarget()
    end
    self.view.outRepositoryList.outRepoNaviGroup.onIsTopLayerChanged:RemoveAllListeners()
    self.view.outRepositoryList.outRepoNaviGroup.onIsTopLayerChanged:AddListener(function(isTop)
        if self.m_onIsInCacheAreaNaviGroup ~= nil then
            self.m_onIsInCacheAreaNaviGroup(isTop or self.view.inRepositoryList.inRepoNaviGroup.IsTopLayer)
        end
    end)
end

FacCacheArea._ClearMoveToInCacheSlotBinding = HL.Method(HL.Opt(HL.Boolean)) << function(self, destroy)
    self.m_currDragHelper = nil
    self:_RefreshSlotButtonState(true)
    if self.m_currMoveSourceItem ~= nil then
        self.m_currMoveSourceItem:SetSelected(false)
        if not destroy then
            self:SetNaviTarget(self.m_currMoveSourceItem.view.button)
        end
    end
    for _, repo in ipairs(self.m_inRepositoryList) do
        repo:SetSlotListBtnEnabled(true)
    end
    for _, repo in ipairs(self.m_outRepositoryList) do
        repo:SetSlotListBtnEnabled(true)
    end
    self.view.inRepositoryList.inRepoNaviGroup.enablePartner = true
    self.view.outRepositoryList.outRepoNaviGroup.enablePartner = true
    self.view.inRepositoryList.inRepoNaviGroup.onDefaultNaviFailed:RemoveAllListeners()
    self.view.outRepositoryList.outRepoNaviGroup.onDefaultNaviFailed:RemoveAllListeners()
    InputManagerInst:ToggleBinding(self.m_moveConfirmBindingId, false)
    InputManagerInst:ToggleBinding(self.m_moveCancelBindingId, false)
    Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.inputGroup.groupId)
    Notify(MessageConst.FAC_ON_MOVE_HIDE_CONTROLLER_MODE_HINT)
end

FacCacheArea._OnNaviTargetMoveToSelectedSlotConfirm = HL.Method() << function(self)
    for _, repo in ipairs(self.m_inRepositoryList) do
        local slotList = repo:GetRepositorySlotList()
        for _, slot in ipairs(slotList) do
            if slot:IsNaviTarget() then
                slot:TryDropItem(self.m_currDragHelper, false)
                return
            end
        end
    end
    for _, repo in ipairs(self.m_outRepositoryList) do
        local slotList = repo:GetRepositorySlotList()
        for _, slot in ipairs(slotList) do
            if slot:IsNaviTarget() then
                slot:TryDropItem(self.m_currDragHelper, false)
                return
            end
        end
    end
end

FacCacheArea._RefreshSlotButtonState = HL.Method(HL.Boolean) << function(self, active)
    for _, repo in ipairs(self.m_inRepositoryList) do
        local slotList = repo:GetRepositorySlotList()
        for _, slot in ipairs(slotList) do
            slot:SetSlotBtnHoverBindingEnabled(active)
        end
    end
    for _, repo in ipairs(self.m_outRepositoryList) do
        local slotList = repo:GetRepositorySlotList()
        for _, slot in ipairs(slotList) do
            slot:SetSlotBtnHoverBindingEnabled(active)
        end
    end
end






FacCacheArea.RefreshCacheArea = HL.Method() << function(self)

    self:_InitAreaRepositoryList()
end

FacCacheArea.RefreshAreaBlockState = HL.Method(HL.Boolean) << function(self, isBlocked)
    self.view.decoActivatorAnimation.transform.localScale = isBlocked and Vector3.zero or Vector3.one
    self.view.decoArrowAnimation.transform.localScale = isBlocked and Vector3.zero or Vector3.one
    self.view.blockNode.gameObject:SetActive(isBlocked)
end

FacCacheArea.GainAreaOutItems = HL.Method() << function(self)
    local core = GameInstance.player.remoteFactory.core
    core:Message_OpMoveAllCacheOutItemToBag(Utils.getCurrentChapterId(), self.m_buildingInfo.nodeId)
end

FacCacheArea.GetAreaInRepositoryNormalSlotGroup = HL.Method().Return(HL.Table) << function(self)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.inRepositoryList.rectTransform)
    return self:_GetAreaRepositorySlotGroup(FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal, self.m_inRepositoryList)
end

FacCacheArea.GetAreaOutRepositoryNormalSlotGroup = HL.Method().Return(HL.Table) << function(self)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.outRepositoryList.rectTransform)
    return self:_GetAreaRepositorySlotGroup(FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal, self.m_outRepositoryList)
end

FacCacheArea.GetAreaInRepositoryFluidSlotGroup = HL.Method().Return(HL.Table) << function(self)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.inRepositoryList.rectTransform)
    return self:_GetAreaRepositorySlotGroup(FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid, self.m_inRepositoryList)
end

FacCacheArea.GetAreaOutRepositoryFluidSlotGroup = HL.Method().Return(HL.Table) << function(self)
    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.inRepositoryList.rectTransform)
    return self:_GetAreaRepositorySlotGroup(FacConst.FAC_CACHE_SLOT_TYPE_STATE.GasLiquid, self.m_outRepositoryList)
end

FacCacheArea.PlayArrowAnimation = HL.Method(HL.Opt(HL.Function)) << function(self, callback)
    if self.m_isActivator then
        self.view.decoActivatorAnimation:PlayWithTween("facconnector_arrow04", callback)
    else
        self.view.decoArrowAnimation:PlayWithTween("facmac_decoarrow_loop", callback)
    end
end

FacCacheArea.DropItemToArea = HL.Method(HL.Forward('UIDragHelper'), HL.Number, HL.Opt(CS.Proto.ITEM_MOVE_MODE)) << function(self, dragHelper, cacheType, mode)
    for _, repo in ipairs(self.m_inRepositoryList) do
        if FactoryUtils.isCacheTypeAcceptItemType(repo:GetRepoCacheType(), cacheType) then
            if repo:TryDropItemToRepository(dragHelper, mode) == true then
                return
            end
        end
    end
    local bottleOrJarFill = FactoryUtils.isEmptyBottleOrJarItem(dragHelper.info.itemId, cacheType)
    if bottleOrJarFill then
        for _, repo in ipairs(self.m_outRepositoryList) do
            if FactoryUtils.isCacheTypeAcceptItemType(repo:GetRepoCacheType(), cacheType) then
                if repo:TryDropItemToRepository(dragHelper, mode) == true then
                    return
                end
            end
        end
    end
end

FacCacheArea.GetDropToComponentId = HL.Method(HL.Forward('UIDragHelper')).Return(HL.Opt(HL.Number)) << function(self, dragHelper)
    for _, repo in ipairs(self.m_inRepositoryList) do
        for index = 1, repo.m_slotList:GetCount() do
            local cacheSlot = repo.m_slotList:GetItem(index)
            if cacheSlot:CanDrop(dragHelper) then
                return cacheSlot.m_cache.componentId
            end
        end
    end
    return nil
end

FacCacheArea.InitAreaNaviTarget = HL.Method() << function(self)
    if #self.m_inRepositoryList > 0 then
        self.m_inRepositoryList[#self.m_inRepositoryList]:SetFirstSlotToNaviTarget()
    end
end

FacCacheArea.CheckRepoNaviTargetTopLayer = HL.Method(HL.Boolean).Return(HL.Boolean) << function(self, isIn)
    if isIn then
        return self.view.inRepositoryList.inRepoNaviGroup.IsTopLayer
    else
        return self.view.outRepositoryList.outRepoNaviGroup.IsTopLayer
    end
    return false
end

FacCacheArea.AddNaviGroupSwitchInfo = HL.Method(HL.Table) << function(self, naviGroupInfos)
    local groupData = {
        naviGroup = self.view.inRepositoryList.inRepoNaviGroup,
        subGroups = { self.view.outRepositoryList.outRepoNaviGroup },
        text = Language.LUA_INV_NAVI_SWITCH_TO_MACHINE,
        forceDefault = true,
    }
    if self.m_isActivator then
        local ctrl = self:GetUICtrl()
        table.insert(groupData.subGroups, ctrl.view.activatorNodes.naviGroup)
    end
    table.insert(naviGroupInfos, groupData)
end

FacCacheArea.NaviTargetMoveToInCacheSlot = HL.Method(HL.Forward('Item'), HL.Forward('UIDragHelper'), HL.Number) << function(self, sourceItem, dragHelper, cacheType)
    local slotCount = 0
    for _, repo in ipairs(self.m_inRepositoryList) do
        if FactoryUtils.isCacheTypeAcceptItemType(repo:GetRepoCacheType(), cacheType) then
            local slotList = repo:GetRepositorySlotList()
            slotCount = slotCount + #slotList
        end
    end
    local bottleOrJarFill = FactoryUtils.isEmptyBottleOrJarItem(dragHelper.info.itemId, cacheType)
    if bottleOrJarFill then
        for _, repo in ipairs(self.m_outRepositoryList) do
            if FactoryUtils.isCacheTypeAcceptItemType(repo:GetRepoCacheType(), cacheType) then
                local slotList = repo:GetRepositorySlotList()
                slotCount = slotCount + #slotList
            end
        end
    end

    if slotCount > 1 then

        self.m_currDragHelper = dragHelper
        self.m_currMoveSourceItem = sourceItem
        InputManagerInst:ToggleBinding(self.m_moveConfirmBindingId, true)
        InputManagerInst:ToggleBinding(self.m_moveCancelBindingId, true)
        local ctrl = self:GetUICtrl()
        Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
            panelId = ctrl.panelId,
            isGroup = true,
            id = self.view.inputGroup.groupId,
            hintPlaceholder = ctrl.view.controllerHintPlaceholder,
            noHighlight = true,
            rectTransform = self.view.rectTransform,
        })

        for _, repo in ipairs(self.m_inRepositoryList) do
            if not FactoryUtils.isCacheTypeAcceptItemType(repo:GetRepoCacheType(), cacheType) then
                repo:SetSlotListBtnEnabled(false)
            end
        end
        for _, repo in ipairs(self.m_outRepositoryList) do
            if not FactoryUtils.isCacheTypeAcceptItemType(repo:GetRepoCacheType(), cacheType) then
                repo:SetSlotListBtnEnabled(false)
            end
        end
        self.view.inRepositoryList.inRepoNaviGroup:NaviToThisGroup(true)
        self.view.inRepositoryList.inRepoNaviGroup.enablePartner = false
        if bottleOrJarFill then
            self.view.outRepositoryList.outRepoNaviGroup.enablePartner = false
            self.view.inRepositoryList.inRepoNaviGroup.onDefaultNaviFailed:AddListener(function(dir)
                if dir == CS.UnityEngine.UI.NaviDirection.Right then
                    self.view.outRepositoryList.outRepoNaviGroup:NaviToThisGroup(true)
                end
            end)
            self.view.outRepositoryList.outRepoNaviGroup.onDefaultNaviFailed:AddListener(function(dir)
                if dir == CS.UnityEngine.UI.NaviDirection.Left then
                    self.view.inRepositoryList.inRepoNaviGroup:NaviToThisGroup(true)
                end
            end)
        end
        self.m_currMoveSourceItem:SetSelected(true)
        self:_RefreshSlotButtonState(false)

        local textId
        if cacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal then
            textId = "LUA_ITEM_ACTION_CACHE_AREA_SELECT_NORMAL_SLOT"
        elseif cacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid then
            textId = bottleOrJarFill and "LUA_ITEM_ACTION_CACHE_AREA_SELECT_LIQUID_SLOT_FILL" or "LUA_ITEM_ACTION_CACHE_AREA_SELECT_LIQUID_SLOT"
        elseif cacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas then
            textId = bottleOrJarFill and "LUA_ITEM_ACTION_CACHE_AREA_SELECT_GAS_SLOT_FILL" or "LUA_ITEM_ACTION_CACHE_AREA_SELECT_GAS_SLOT"
        end
        Notify(MessageConst.FAC_ON_MOVE_SHOW_CONTROLLER_MODE_HINT, Language[textId])
    else

        self:DropItemToArea(dragHelper, cacheType)
    end
end






FacCacheArea.OnStartUiDrag = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if not DeviceInfo.usingTouch then
        return
    end

    local fromBag = dragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag
    local fromDepot = dragHelper.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot
    if not fromBag and not fromDepot then
        return
    end

    local args = {
        isLeft = false,
        actions = {}
    }

    local realIndex = 0
    for _, repo in ipairs(self.m_inRepositoryList) do
        for k = 1, repo.m_slotList:GetCount() do
            local cacheSlot = repo.m_slotList:GetItem(k)
            realIndex = realIndex + 1
            if cacheSlot:CanDrop(dragHelper) then
                table.insert(args.actions, {
                    text = Language["LUA_MOBILE_ITEM_DRAG_GRID_TO_FAC_SLOT_" .. realIndex],
                    icon = "icon_common_move_to_machine",
                    action = function()
                        if self.m_isDestroyed then
                            return
                        end
                        cacheSlot:_OnDropItem(dragHelper)
                        dragHelper.uiDragItem:OnEndDrag(nil)
                    end
                })
            end
        end
    end
    if #args.actions == 0 then
        return
    end
    Notify(MessageConst.SHOW_ITEM_DRAG_HELPER, args)
end

FacCacheArea.OnEndUiDrag = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    Notify(MessageConst.HIDE_ITEM_DRAG_HELPER)
end




HL.Commit(FacCacheArea)
return FacCacheArea
