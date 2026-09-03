
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WeaponExhibitPotential
WeaponExhibitPotentialCtrl = HL.Class('WeaponExhibitPotentialCtrl', uiCtrl.UICtrl)







WeaponExhibitPotentialCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_WEAPON_REFINE] = 'OnWeaponRefine',
    [MessageConst.ON_GEM_DETACH] = 'OnGemDetach',
    [MessageConst.ON_ITEM_LOCKED_STATE_CHANGED] = 'OnItemLockedStateChanged',


}

WeaponExhibitPotentialCtrl.m_arg = HL.Field(HL.Table)

WeaponExhibitPotentialCtrl.m_weaponInfo = HL.Field(HL.Table)

WeaponExhibitPotentialCtrl.m_bottomWeaponCellCache =HL.Field(HL.Forward("UIListCache"))

WeaponExhibitPotentialCtrl.m_lastClickItemId = HL.Field(HL.Any)

WeaponExhibitPotentialCtrl.m_lastClickItemInfo = HL.Field(HL.Table)

WeaponExhibitPotentialCtrl.m_costItemInfoDict = HL.Field(HL.Table)

WeaponExhibitPotentialCtrl.m_isFocusJump = HL.Field(HL.Boolean) << false

WeaponExhibitPotentialCtrl.m_effectCor = HL.Field(HL.Thread)

WeaponExhibitPotentialCtrl.m_selectMaterialsInputBindingId = HL.Field(HL.Number) << -1

WeaponExhibitPotentialCtrl.m_materialsDecreaseCountInputBindingId = HL.Field(HL.Number) << -1

WeaponExhibitPotentialCtrl.m_naviMaterialCell = HL.Field(HL.Any)

WeaponExhibitPotentialCtrl.m_naviMaterialItemInfo = HL.Field(HL.Table)

WeaponExhibitPotentialCtrl.OnItemLockedStateChanged = HL.Method(HL.Table) << function(self, arg)
    local itemId, instId, isLock = unpack(arg)
    if not isLock then
        return
    end

    if not instId or instId <= 0 then
        return
    end

    local costItemInfoDict = self.m_costItemInfoDict
    if costItemInfoDict[instId] ~= nil then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_ITEM_LOCK_TOAST)
        self:_RemoveFromCostItemDict(costItemInfoDict[instId])
    end
end

WeaponExhibitPotentialCtrl.OnGemDetach = HL.Method(HL.Table) << function(self, arg)
    self.view.commonWeaponList:RefreshAllCells()
end

WeaponExhibitPotentialCtrl.OnWeaponRefine = HL.Method(HL.Table) << function(self, arg)
    local weaponInfo = self.m_weaponInfo
    local weaponInst = CharInfoUtils.getWeaponByInstId(weaponInfo.weaponInstId)
    local maxRefineLv = CS.Beyond.Gameplay.WeaponUtil.GetWeaponMaxRefineLv(weaponInfo.weaponTemplateId)
    local weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponInfo.weaponTemplateId, weaponInfo.weaponInstId)

    self.m_costItemInfoDict = {}

    self:_ToggleExpandNode(false, true)
    if weaponInst.refineLv >= maxRefineLv then
        AudioAdapter.PostEvent("Au_UI_Event_WeaponPotentialLevelMax")
        self.m_effectCor = self:_ClearCoroutine(self.m_effectCor)
        self.m_effectCor = self:_StartCoroutine(function()
            self:_RefreshPotentialPanelBasic(weaponExhibitInfo)
            self.view.weaponSkillNodeBefore.gameObject:SetActive(false)
            self.view.potentialNode:Play("weaponpotential_fullfinsh_in")
            coroutine.wait(0.3)
            self:_RefreshWeaponPotentialStar(weaponExhibitInfo.weaponInst.templateId, weaponExhibitInfo.weaponInst.instId)
        end)
    else
        AudioAdapter.PostEvent("Au_UI_Event_WeaponPotentialLevelUp")
        self.m_effectCor = self:_StartCoroutine(function()
            self.view.potentialAfter.view.animation:Play("weaponexhibitpotential_finishglow_break")
            self:_RefreshPotentialPanelBasic(weaponExhibitInfo)
            self.view.potentialAfter:InitWeaponPotentialStar(weaponInst.refineLv, {
                showMaxPotentialHint = false,
                fromLv = weaponInst.refineLv
            })
            coroutine.wait(0.2)
            self.view.potentialBefore:InitWeaponPotentialStar(weaponInst.refineLv, {
                showCurLevelTransition = true,
                showMaxPotentialHint = false,
                fromLv = weaponInst.refineLv
            })
        end)
    end
end

WeaponExhibitPotentialCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg
    self:_InitActionEvent()

    self.m_weaponInfo = arg.weaponInfo
    self.m_isFocusJump = arg.isFocusJump == true
end

WeaponExhibitPotentialCtrl._InitActionEvent = HL.Method() << function(self)
    self.view.m_bottomWeaponCellCache = UIUtils.genCellCache(self.view.listCellWeaponUpgrade)
    self.view.btnBack.onClick:AddListener(function()
        if UIManager:IsShow(PanelId.ItemTips) then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end

        if self.m_isFocusJump then
            Notify(MessageConst.WEAPON_EXHIBIT_BLEND_EXIT, {
                finishCallback = function()
                    PhaseManager:ExitPhaseFast(PhaseId.WeaponInfo)
                end
            })
            self:PlayAnimationOut()
            return
        end

        self:Notify(MessageConst.WEAPON_EXHIBIT_PAGE_CHANGE, {
            pageType = UIConst.WEAPON_EXHIBIT_PAGE_TYPE.OVERVIEW,
        })
    end)
    self.view.btnFullSkill.onClick:AddListener(function()
        Notify(MessageConst.HIDE_ITEM_TIPS)
        UIManager:Open(PanelId.WeaponSkillDetail, {
            weaponInstId = self.m_weaponInfo.weaponInstId,
        })
    end)
    self.view.btnEmpty.onClick:AddListener(function()
        self:_ToggleExpandNode(false, DeviceInfo.usingController)
    end)
    self.view.btnLevelUp.onClick:AddListener(function()
        if not self.m_costItemInfoDict or lume.count(self.m_costItemInfoDict) == 0 then
            self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_POTENTIAL_EMPTY_INGREDIENT)
            return
        end

        local costWeapons = {}
        local costItem2Count = {}
        for i, costItemInfo in pairs(self.m_costItemInfoDict) do
            if costItemInfo.isWeapon then
                table.insert(costWeapons, costItemInfo.itemInst.instId)
            else
                local itemId = costItemInfo.id
                costItem2Count[itemId] = (costItem2Count[itemId] or 0) + costItemInfo.count
            end
        end
        local addRefineLv = self:_GetCurAddRefineLv()
        local weaponInst = CharInfoUtils.getWeaponByInstId(self.m_weaponInfo.weaponInstId)
        GameInstance.player.charBag:WeaponRefine(self.m_weaponInfo.weaponInstId, weaponInst.refineLv + addRefineLv, costItem2Count, costWeapons)
    end)
    self.m_selectMaterialsInputBindingId = self:BindInputPlayerAction("char_info_weapon_select_materials", function()
        self:_ToggleExpandNode(true)
    end, self.view.leftNodeInputGroup.groupId)
    self.m_materialsDecreaseCountInputBindingId = self:BindInputPlayerAction("char_info_weapon_materials_decrease_count", function()
        self:_OnItemBtnMinusClicked(self.m_naviMaterialCell, self.m_naviMaterialItemInfo)
    end)
    InputManagerInst:ToggleBinding(self.m_materialsDecreaseCountInputBindingId, false)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    UIUtils.bindHyperlinkPopup(self, "WeaponSkill", self.view.leftNodeInputGroup.groupId)
end

WeaponExhibitPotentialCtrl.OnShow =  HL.Override() << function(self)
    local weaponInfo = self.m_weaponInfo
    local weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponInfo.weaponTemplateId, weaponInfo.weaponInstId)

    local costItemInfoDict
    local isExpand = false
    if self.m_arg.stateArg and self.m_arg.stateArg.costItemInfoDict then
        costItemInfoDict = self.m_arg.stateArg.costItemInfoDict
        isExpand = self.m_arg.stateArg.isExpand
    end
    self.m_costItemInfoDict = costItemInfoDict or {}

    self:_RefreshPotentialPanelBasic(weaponExhibitInfo, true)
    self:_RefreshWeaponPotentialStar(weaponExhibitInfo.weaponInst.templateId, weaponExhibitInfo.weaponInst.instId)
    if costItemInfoDict then
        local itemInfoList = self.view.commonWeaponList.m_itemInfoList
        for indexId, costItemInfo in pairs(costItemInfoDict) do
            for _, upgradeItemInfo in pairs(itemInfoList) do
                if upgradeItemInfo.indexId == indexId then
                    upgradeItemInfo.count = costItemInfo.count
                    costItemInfoDict[indexId] = upgradeItemInfo
                    break
                end
            end
        end
        self.view.commonWeaponList:RefreshAllCells()
        if isExpand then
            self:_ToggleExpandNode(true, true)
            if self.m_arg.stateArg and self.m_arg.stateArg.curSelectedIndex and DeviceInfo.usingController then
                self.view.commonWeaponList:SetSelectedIndex(self.m_arg.stateArg.curSelectedIndex, false)
            end
        end
    end
    if self.m_arg.stateArg and self.m_arg.stateArg.isBtnFullSkillClicked then
        self.view.btnFullSkill.onClick:Invoke()
    end
    self.m_arg.stateArg = nil
end

WeaponExhibitPotentialCtrl._RefreshPotentialPanelBasic = HL.Method(HL.Table, HL.Opt(HL.Boolean)) << function(self,  weaponExhibitInfo, isInit)
    self.view.title.text = string.format(Language.LUA_WEAPON_EXHIBIT_POTENTIAL_TITLE, weaponExhibitInfo.itemCfg.name)
    self.view.expandNode.gameObject:SetActive(false)
    self:_RefreshSkillNode(weaponExhibitInfo.weaponInst.templateId, weaponExhibitInfo.weaponInst.instId, isInit == true)
    self:_RefreshButtonNode(weaponExhibitInfo)
    self:_RefreshCommonItemList(weaponExhibitInfo)
end


WeaponExhibitPotentialCtrl._RefreshButtonNode = HL.Method(HL.Table) << function(self, weaponExhibitInfo)
    local isMaxPotential = weaponExhibitInfo.weaponInst.refineLv >= UIConst.CHAR_MAX_POTENTIAL
    if isMaxPotential then
        self.view.btnLevelUp.gameObject:SetActive(false)
        self.view.upgradeNode.gameObject:SetActive(false)
        return
    end
end


WeaponExhibitPotentialCtrl._RefreshCommonItemList = HL.Method(HL.Table) << function(self, weaponExhibitInfo)
    self.view.commonWeaponList:InitCommonItemList({
        listType = UIConst.COMMON_ITEM_LIST_TYPE.WEAPON_EXHIBIT_POTENTIAL,
        filter_templateId = weaponExhibitInfo.weaponInst.templateId,
        filter_not_instId = weaponExhibitInfo.weaponInst.instId,
        filter_not_equipped = true,
        filter_not_maxPotential = true,

        onClickItem = function(args)
            local itemInfo = args.itemInfo
            local curCell = args.curCell
            local nextCell = args.nextCell

            self:_OnClickExpandCostItemCell(itemInfo, args.realClick)

            if curCell then
            end

            if nextCell then
                self:_RefreshWeaponCellAddOn(nextCell, itemInfo)
            end
        end,
        refreshItemAddOn = function(cell, itemInfo)
            self:_RefreshWeaponCellAddOn(cell, itemInfo)
        end,
        onItemIsNaviTargetChanged = function(cell, itemInfo, isTarget)
            self:_OnItemIsNaviTargetChanged(cell, itemInfo, isTarget)
        end,
        clickItemControllerHintText = Language.ui_weapon_controller_Increase_materials,
    })
end


WeaponExhibitPotentialCtrl._RefreshWeaponCellAddOn = HL.Method(HL.Any, HL.Opt(HL.Table)) << function(self, cell, itemInfo)
    cell.emptyNode.onClick:RemoveAllListeners()
    cell.emptyNode.onClick:AddListener(function()
        self:_ToggleExpandNode(true)
    end)


    if itemInfo == nil then
        cell.selectNode.gameObject:SetActive(false)
        cell.multiSelectNode.gameObject:SetActive(false)
        cell.emptyNode.gameObject:SetActive(true)
        return
    end

    cell.btnMinus.onClick:RemoveAllListeners()
    cell.btnMinus.onClick:AddListener(function()
        self:_OnItemBtnMinusClicked(cell, itemInfo)
    end)

    cell.emptyNode.gameObject:SetActive(false)

    cell.selectNode.gameObject:SetActive(itemInfo.count ~= nil and itemInfo.count > 0)
    cell.multiSelectNode.gameObject:SetActive(not itemInfo.isWeapon)
    cell.selectCount.text = itemInfo.count

    if cell == self.m_naviMaterialCell then
        InputManagerInst:ToggleBinding(self.m_materialsDecreaseCountInputBindingId, itemInfo.count and itemInfo.count > 0)
    end
end

WeaponExhibitPotentialCtrl._OnItemBtnMinusClicked = HL.Method(HL.Any, HL.Table) << function(self, cell, itemInfo)
    self:_RefreshWeaponCellAddOn(cell, itemInfo)
    self:_RemoveFromCostItemDict(itemInfo, 1)
end

WeaponExhibitPotentialCtrl._OnItemIsNaviTargetChanged = HL.Method(HL.Any, HL.Table, HL.Boolean) << function(self, cell, itemInfo, isTarget)
    if self.m_arg.stateArg and not self.m_arg.stateArg.isShow then
        return
    end
    if isTarget and cell.gameObject.activeSelf then
        InputManagerInst:ToggleBinding(self.m_materialsDecreaseCountInputBindingId, itemInfo.count and itemInfo.count > 0)
        self.m_naviMaterialCell = cell
        self.m_naviMaterialItemInfo = itemInfo
        local instId
        if itemInfo.itemInst then
            instId = itemInfo.itemInst.instId
        end
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            transform = self.view.expandNode.tipPos,
            itemId = itemInfo.itemCfg.id,
            instId = instId,
            noJump = true,
            autoClose = false,
            isSideTips = true,
            posType = UIConst.UI_TIPS_POS_TYPE.RightMid,
        })
    end
end

WeaponExhibitPotentialCtrl._OnClickExpandCostItemCell = HL.Method(HL.Table, HL.Boolean) << function(self, costItemInfo, realClick)
    if not realClick then
        return
    end

    if self.m_lastClickItemId ~= costItemInfo.indexId and not DeviceInfo.usingController then
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            transform = self.view.expandNode.tipPos,
            itemId = costItemInfo.itemCfg.id,
            instId = costItemInfo.instId,
            noJump = true,
            autoClose = false,
            isSideTips = true,
            posType = UIConst.UI_TIPS_POS_TYPE.RightMid,
        })
    end

    self.m_lastClickItemId = costItemInfo.indexId
    self.m_lastClickItemInfo = costItemInfo

    self:_AddIntoCostItemDict(costItemInfo, 1)
end

WeaponExhibitPotentialCtrl._AddIntoCostItemDict = HL.Method(HL.Table, HL.Number) << function(self, itemInfo, count)
    local weaponInfo = self.m_weaponInfo

    self:_TryAddIntoCostItemDict(itemInfo, count)

    self:_RefreshBottomCostItem(true)
    self:_RefreshSkillNode(weaponInfo.weaponTemplateId, weaponInfo.weaponInstId)
    self:_RefreshWeaponPotentialStar(weaponInfo.weaponTemplateId, weaponInfo.weaponInstId)
end

WeaponExhibitPotentialCtrl._RemoveFromCostItemDict = HL.Method(HL.Table, HL.Opt(HL.Number)) << function(self, itemInfo, count)
    local costItemInfoDict = self.m_costItemInfoDict
    local weaponInfo = self.m_weaponInfo

    count = count or 1
    local id = itemInfo.indexId
    itemInfo.count = math.max(0, itemInfo.count - count)

    if costItemInfoDict[id] and itemInfo.count <= 0 then
        costItemInfoDict[id] = nil
    end

    self:_RefreshBottomCostItem(true)
    self:_RefreshSkillNode(weaponInfo.weaponTemplateId, weaponInfo.weaponInstId)
    self:_RefreshWeaponPotentialStar(weaponInfo.weaponTemplateId, weaponInfo.weaponInstId)
    self.view.commonWeaponList:RefreshCellById(itemInfo.indexId)
end

WeaponExhibitPotentialCtrl._TryAddIntoCostItemDict = HL.Method(HL.Table, HL.Number) << function(self, itemInfo, count)
    local curCount = itemInfo.count or 0
    local nextCount = curCount + count

    if count <= 0 then
        return nextCount
    end

    local isLock = itemInfo.isWeapon and
        GameInstance.player.inventory:IsItemLocked(Utils.getCurrentScope(), itemInfo.itemInst.templateId, itemInfo.itemInst.instId)
    if isLock then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_UPGRADE_WEAPON_LOCKED)
        return
    end

    local addRefineLv
    if itemInfo.isWeapon then
        local addRefineWeaponInst = CharInfoUtils.getWeaponByInstId(itemInfo.instId)
        addRefineLv = addRefineWeaponInst.refineLv + 1
    else
        addRefineLv = 1
    end


    local refineWeaponInst = CharInfoUtils.getWeaponByInstId(self.m_weaponInfo.weaponInstId)
    local maxRefineLv = CS.Beyond.Gameplay.WeaponUtil.GetWeaponMaxRefineLv(refineWeaponInst.templateId)

    local costItemInfoDict = self.m_costItemInfoDict
    local alreadyAddRefineLv = self:_GetCurAddRefineLv()

    if alreadyAddRefineLv == maxRefineLv - refineWeaponInst.refineLv then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_POTENTIAL_MAX_TOAST)
        return
    end

    local newRefineLv = refineWeaponInst.refineLv + addRefineLv + alreadyAddRefineLv

    if newRefineLv > maxRefineLv then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_REFINE_OVERFLOW)
        return
    end

    local hasGemAttached = itemInfo.isWeapon and itemInfo.itemInst.attachedGemInstId > 0
    if hasGemAttached then
        local gemInst = CharInfoUtils.getGemByInstId(itemInfo.itemInst.attachedGemInstId)
        local gemItemCfg = Tables.itemTable:GetValue(gemInst.templateId)
        Notify(MessageConst.SHOW_POP_UP, {
            content = string.format(Language.LUA_WEAPON_EXHIBIT_UPGRADE_GEM_EQUIPPED, gemItemCfg.name),
            onConfirm = function()
                GameInstance.player.charBag:DetachGem(itemInfo.itemInst.instId)
            end,
            onCancel = function()
            end
        })
        return
    end

    if itemInfo.inventoryCount ~= 0 and nextCount > itemInfo.inventoryCount then
        if not itemInfo.isWeapon then
            self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_UPGRADE_MAX_ITEM_COUNT)
        end

        nextCount = itemInfo.inventoryCount
    elseif itemInfo.inventoryCount == 0 then
        nextCount = 0
    end

    itemInfo.count = nextCount

    if itemInfo.count > 0 then
        local indexId = itemInfo.indexId
        costItemInfoDict[indexId] = itemInfo
    end
end

WeaponExhibitPotentialCtrl._GetCurAddRefineLv = HL.Method().Return(HL.Number) << function(self)
    local addRefineLv = 0
    for i, itemInfo in pairs(self.m_costItemInfoDict) do
        if itemInfo.isWeapon then
            local weaponInst = CharInfoUtils.getWeaponByInstId(itemInfo.indexId)
            addRefineLv = addRefineLv + weaponInst.refineLv + 1
        else
            addRefineLv = addRefineLv + 1 * itemInfo.count
        end
    end

    return addRefineLv
end

WeaponExhibitPotentialCtrl._RefreshBottomCostItem = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipGraduallyShow)
    local costItemInfoList = self:_CollectCostItemInfoList()
    local costCellCount = math.max(#costItemInfoList, UIConst.WEAPON_EXHIBIT_UPGRADE_MIN_SLOT_NUM)

    self.view.m_bottomWeaponCellCache:Refresh(costCellCount, function(cell, index)
        local costItemInfo = costItemInfoList[index]

        cell.emptyNode.gameObject:SetActive(costItemInfo == nil)
        cell.item.gameObject:SetActive(costItemInfo ~= nil)
        self:_RefreshWeaponCellAddOn(cell, costItemInfo)

        cell.btnMinus.onClick:AddListener(function()
            self:_RemoveFromCostItemDict(costItemInfo, 1)
        end)

        if costItemInfo then
            cell.item:InitItem({
                id = costItemInfo.itemCfg.id,
                instId = costItemInfo.isWeapon and costItemInfo.indexId or 0,
            }, function()
                self:_ToggleExpandNode(true)
            end)
        end
    end)
end

WeaponExhibitPotentialCtrl._ToggleExpandNode = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, isOn, closeAll)
    self.m_lastClickItemId = nil
    self.m_lastClickItemInfo = nil

    if UIManager:IsShow(PanelId.ItemTips) then
        Notify(MessageConst.HIDE_ITEM_TIPS)
        if not closeAll then
            return
        end
    end

    if isOn then
        UIUtils.PlayAnimationAndToggleActive(self.view.commonWeaponList.view.animationWrapper, true)
        self.view.expandNode.gameObject:SetActive(true)
        if DeviceInfo.usingController then
            self.view.commonWeaponList:PlayGraduallyShow(1, false)
        else
            self.view.commonWeaponList:PlayGraduallyShow()
        end
    else
        UIUtils.PlayAnimationAndToggleActive(self.view.commonWeaponList.view.animationWrapper, false, function()
            self.view.expandNode.gameObject:SetActive(false)
        end)
        InputManagerInst:ToggleBinding(self.m_materialsDecreaseCountInputBindingId, false)
    end

    InputManagerInst:ToggleGroup(self.view.leftNodeInputGroup.groupId, not isOn)
    if DeviceInfo.usingController then
        InputManagerInst:ToggleGroup(self.view.btnLevelUp.groupId, not isOn)
        InputManagerInst:ToggleGroup(self.view.btnFullSkill.groupId, not isOn)
    else
        InputManagerInst:ToggleGroup(self.view.btnLevelUp.groupId, true)
        InputManagerInst:ToggleGroup(self.view.btnFullSkill.groupId, true)
    end
end

WeaponExhibitPotentialCtrl._RefreshWeaponPotentialStar = HL.Method(HL.String, HL.Number, HL.Opt(HL.Boolean)) << function(self, templateId, instId)
    local weaponInst = CharInfoUtils.getWeaponByInstId(instId)
    local addRefineLv = self:_GetCurAddRefineLv()

    local isMaxPotential = weaponInst.refineLv >= UIConst.CHAR_MAX_POTENTIAL
    if isMaxPotential then
        self.view.arrowNode.gameObject:SetActive(false)
        self.view.maxPotentialNode.gameObject:SetActive(true)
        self.view.potentialBefore.gameObject:SetActive(false)
        self.view.potentialAfter.gameObject:SetActive(false)
    else
        self.view.potentialBefore:InitWeaponPotentialStar(weaponInst.refineLv)
        self.view.potentialAfter:InitWeaponPotentialStar(weaponInst.refineLv + addRefineLv, {
            showMaxPotentialHint = false,
            fromLv = weaponInst.refineLv
        })
    end
end

WeaponExhibitPotentialCtrl._RefreshSkillNode = HL.Method(HL.String, HL.Number, HL.Opt(HL.Boolean)) << function(self, templateId, instId, isInit)
    local weaponInst = CharInfoUtils.getWeaponByInstId(instId)

    local addRefineLv = self:_GetCurAddRefineLv()

    local maxRefineLv = CS.Beyond.Gameplay.WeaponUtil.GetWeaponMaxRefineLv(templateId)
    local isMaxRefineLv = weaponInst.refineLv >= maxRefineLv
    self.view.weaponSkillNodeBefore.gameObject:SetActive(not isMaxRefineLv)


    self:_RefreshBottomCostItem()

    if not isMaxRefineLv then
        self.view.weaponSkillNodeBefore:InitWeaponSkillNode(instId, {
            tryGemInstId = weaponInst.attachedGemInstId,
            tryBreakthroughLv = weaponInst.breakthroughLv,
            tryRefineLv = weaponInst.refineLv,
            onlyPotentialSkill = true,
        })
    end
    self.view.weaponSkillNodeAfter:InitWeaponSkillNode(instId, {
        tryGemInstId = weaponInst.attachedGemInstId,
        tryBreakthroughLv = weaponInst.breakthroughLv,
        tryRefineLv = weaponInst.refineLv + addRefineLv,
        onlyPotentialSkill = true,
    })
    self.view.weaponIntroduction:InitWeaponIntroduction(templateId, instId)
end

WeaponExhibitPotentialCtrl._CollectCostItemInfoList = HL.Method().Return(HL.Table) << function(self)
    local costItemList = {}
    for i, itemInfo in pairs(self.m_costItemInfoDict) do
        table.insert(costItemList, itemInfo)
    end

    return costItemList
end

WeaponExhibitPotentialCtrl.GetCurStateArg = HL.Method().Return(HL.Table) << function(self)
    local arg = {
        isBtnFullSkillClicked = UIManager:IsShow(PanelId.WeaponSkillDetail),
        costItemInfoDict = self.m_costItemInfoDict,
        isExpand = self.view.expandNode.gameObject.activeSelf,
        curSelectedIndex = self.view.commonWeaponList:IsAnyItemSelecting() and self.view.commonWeaponList:GetCurSelectIndex(),
        isShow = self:IsShow()
    }
    return arg
end

HL.Commit(WeaponExhibitPotentialCtrl)
