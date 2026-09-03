
local GEM_RECAST_TAB_TYPE = {
    FORGE = 1,
    RECAST = 2,
}

local GEM_RECAST_CTRL_TAB = {
    [GEM_RECAST_TAB_TYPE.FORGE] = {
        tabName = "tabForge",
        nodeName = "forgeNode",
    },
    [GEM_RECAST_TAB_TYPE.RECAST] = {
        tabName = "tabRecast",
        nodeName = "recastNode",
    }
}

local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GemRecast
GemRecastCtrl = HL.Class('GemRecastCtrl', uiCtrl.UICtrl)






GemRecastCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_GEM_RECAST] = "OnGemRecast",
    [MessageConst.ON_ITEM_LOCKED_STATE_CHANGED] = '_OnItemLockedStateChanged',
    [MessageConst.ON_GEM_DETACH] = 'OnGemDetach',


}

GemRecastCtrl.m_curTab = HL.Field(HL.Number) << GEM_RECAST_TAB_TYPE.FORGE

GemRecastCtrl.m_curSelectFormulaIndex = HL.Field(HL.Number) << -1

GemRecastCtrl.m_sortedFormulas = HL.Field(HL.Table)


GemRecastCtrl.m_gemIngredientDict = HL.Field(HL.Table)

GemRecastCtrl.m_gemIngredientList = HL.Field(HL.Table)

GemRecastCtrl.m_starCellCache = HL.Field(HL.Forward("UIListCache"))

GemRecastCtrl.m_rankCellCache = HL.Field(HL.Forward("UIListCache"))

GemRecastCtrl.m_gemCellCache = HL.Field(HL.Forward("UIListCache"))



GemRecastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_curTab = GEM_RECAST_TAB_TYPE.FORGE


    self.view.commonGemList.view.gameObject:SetActive(true)
    self.view.commonGemList.view.gameObject:SetActive(false)


    self:_InitActionEvent()

end

GemRecastCtrl._InitActionEvent = HL.Method() << function(self)
    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.GemRecast)
    end)

    self.view.btnEmpty.onClick:AddListener(function()
        self:_ToggleGemList(false)
    end)

    self.view.tabForge.button.onClick:AddListener(function()
        self.m_curSelectFormulaIndex = -1

        self:_RefreshMainPanel(GEM_RECAST_TAB_TYPE.FORGE)
    end)
    self.view.tabRecast.button.onClick:AddListener(function()
        self.m_curSelectFormulaIndex = -1

        self:_RefreshMainPanel(GEM_RECAST_TAB_TYPE.RECAST)
    end)
    self.view.btnAutoFill.onClick:AddListener(function()
        local curFormula = self.m_sortedFormulas[self.m_curSelectFormulaIndex]

        self:_AutoFill(curFormula)
    end)
    self.view.btnLevelUp.onClick:AddListener(function()
        local curFormula = self.m_sortedFormulas[self.m_curSelectFormulaIndex]

        local ingredients = {}
        for instId, _ in pairs(self.m_gemIngredientDict) do
            table.insert(ingredients, instId)
        end

        if #ingredients <= 0 then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_GEM_RECAST_GEM_EMPTY_TOAST)
            return
        end

        if #ingredients < curFormula.costGemNum then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_GEM_RECAST_GEM_EMPTY_TOAST)
            return
        end

        if curFormula.costItemNum > 0 then
            local inventoryCount = Utils.getItemCount(curFormula.costItemId, true)
            if inventoryCount < curFormula.costItemNum then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_GEM_RECAST_COST_EMPTY_TOAST)
                return
            end
        end

        GameInstance.player.inventory:RecastGem(curFormula.formulaId, 1, ingredients)
    end)

    self.m_starCellCache = UIUtils.genCellCache(self.view.starCell)
    self.m_rankCellCache = UIUtils.genCellCache(self.view.forgeNode.rankCell)
    self.m_gemCellCache = UIUtils.genCellCache(self.view.listCellGemRecastSmaller)
end

GemRecastCtrl.OnShow = HL.Override() << function(self)
    self:_RefreshMainPanel(self.m_curTab)
end

GemRecastCtrl._RefreshMainPanel = HL.Method(HL.Number) << function(self, curSelectTab)
    self.view.btnEmpty.gameObject:SetActive(false)

    self.m_curTab = curSelectTab

    for tabType, cfg in pairs(GEM_RECAST_CTRL_TAB) do
        self.view[cfg.tabName].selected.gameObject:SetActive(tabType == curSelectTab)
        self.view[cfg.tabName].default.gameObject:SetActive(tabType ~= curSelectTab)

        self.view[cfg.nodeName].gameObject:SetActive(tabType == curSelectTab)
    end

    self:_InitCommonNode(curSelectTab)
    self:_ToggleGemList(false)
end

GemRecastCtrl._InitCommonNode = HL.Method(HL.Number) << function(self, curSelectTab)
    local firstUnlockIndex = nil
    local sortedFormulas = {}
    for i, cfg in pairs(Tables.gemRecastTable) do
        if curSelectTab == GEM_RECAST_TAB_TYPE.FORGE then
            if cfg.formulaType == GEnums.GemForgeFormulaType.Forge then
                table.insert(sortedFormulas, cfg)
            end
        elseif curSelectTab == GEM_RECAST_TAB_TYPE.RECAST then
            if cfg.formulaType == GEnums.GemForgeFormulaType.Recast then
                table.insert(sortedFormulas, cfg)
            end
        end
    end
    table.sort(sortedFormulas, Utils.genSortFunction({"order"}, true))


    for index, cfg in ipairs(sortedFormulas) do
        local isLocked = not UIUtils.checkIfReachAdventureLv(cfg.adventureLevel)

        if not isLocked and firstUnlockIndex == nil then
            firstUnlockIndex = index
        end
    end

    if firstUnlockIndex <= 0 then
        logger.error("GemRecast->No recast formula unlocked")
        return
    end

    self.m_sortedFormulas = sortedFormulas
    if self.m_curSelectFormulaIndex < 0 then
        self.m_curSelectFormulaIndex = firstUnlockIndex
    end
    self.m_gemIngredientDict = {}
    self.m_gemIngredientList = {}
    self:_RefreshForgeRankCellCache(sortedFormulas)
    self:_RefreshForgeNode(sortedFormulas[self.m_curSelectFormulaIndex])
end

GemRecastCtrl._RefreshForgeRankCellCache = HL.Method(HL.Table) << function(self, sortedFormulas)
    self.m_rankCellCache:Refresh(#sortedFormulas, function(cell, index)
        local formula = sortedFormulas[index]
        local isLocked = not UIUtils.checkIfReachAdventureLv(formula.adventureLevel)

        local showProductItemId = formula.showProductItemId
        local outputGemItemCfg = Tables.itemTable[showProductItemId]

        cell.locked.gameObject:SetActive(isLocked)
        local isSelected = index == self.m_curSelectFormulaIndex
        cell.default.gameObject:SetActive(not isLocked and not isSelected)
        if not isLocked then
            if isSelected then
                cell.selected.gameObject:SetActive(true)
            else
                cell.selectedAniWrp:PlayOutAnimation(function()
                    cell.selected.gameObject:SetActive(false)
                end)
            end
        else
            cell.selected.gameObject:SetActive(false)
        end

        cell.rank.text = outputGemItemCfg.rarity
        cell.colorLine.color = UIUtils.getItemRarityColor(outputGemItemCfg.rarity)


        cell.button.onClick:RemoveAllListeners()
        cell.button.onClick:AddListener(function()
            if isLocked then
                Notify(MessageConst.SHOW_TOAST, string.format(Language.LUA_GEM_FORMULA_LOCKED_WITH_LV_TOAST, formula.adventureLevel))
                return
            end

            self.m_curSelectFormulaIndex = index

            self.m_gemIngredientDict = {}
            self.m_gemIngredientList = {}
            self:_RefreshForgeNode(formula)
            self:_RefreshForgeRankCellCache(self.m_sortedFormulas)
        end)
    end)
end

GemRecastCtrl._RefreshForgeNode = HL.Method(HL.Userdata) << function(self, formula)
    self.view.rightBarAniWrp:PlayInAnimation()
    local outputGemId = formula.showProductItemId
    local outputGemItemCfg = Tables.itemTable[outputGemId]

    self.m_starCellCache:Refresh(outputGemItemCfg.rarity)
    self.view.gemName.text = outputGemItemCfg.name
    self.view.gemIconColor.color = UIUtils.getItemRarityColor(outputGemItemCfg.rarity)

    local hasCostItem = formula.costItemId ~= nil and formula.costItemNum > 0

    self.view.itemCostNode.gameObject:SetActive(hasCostItem)
    self.view.itemCostNodeEmpty.gameObject:SetActive(not hasCostItem)

    self.view.formulaHint:SetAndResolveTextStyle(formula.formulaHint)

    self.view.costItem:InitItem({
        id = formula.costItemId,
        count = formula.costItemNum,
    }, true)
    local inventoryCount = Utils.getItemCount(formula.costItemId, true)
    self.view.storageNode:InitStorageNode(inventoryCount, formula.costItemNum, true)
    self.view.commonGemList:InitCommonItemList({
        listType = UIConst.COMMON_ITEM_LIST_TYPE.GEM_RECAST,
        refreshItemAddOn = function(cell, itemInfo)
            self:_RefreshGemRecastCellAddOn(cell, itemInfo)
        end,
        onClickItem = function(args)
            local itemInfo = args.itemInfo

            args.nextCell.item:ShowTips()

            self:_AddIngredient(itemInfo)
        end,
        filter_rarity = formula.costGemQuality
    })
    self:_RefreshGemCellCache(formula)
end

GemRecastCtrl._RefreshCommonNode = HL.Method(HL.Userdata) << function(self, formula)

end



GemRecastCtrl._RefreshGemCellCache = HL.Method(HL.Userdata) << function(self, formula)
    self.m_gemCellCache:Refresh(formula.costGemNum, function(cell, index)
        local itemInfo = self.m_gemIngredientList[index]

        if itemInfo then
            local gemInst = CharInfoUtils.getGemByInstId(itemInfo.instId)
            cell.item:InitItem({
                id = gemInst.templateId,
                instId = gemInst.instId,
            }, true)
            cell.item.view.button.onClick:RemoveAllListeners()
            cell.item.view.button.onClick:AddListener(function()
                self:_ToggleGemList(true)
            end)
        end

        self:_RefreshGemRecastCellAddOn(cell, itemInfo)
    end)
end

GemRecastCtrl._ToggleGemList = HL.Method(HL.Boolean) << function(self, isOn)
    if isOn then
        self.view.btnEmpty.gameObject:SetActive(true)
        self.view.commonGemList.gameObject:SetActive(true)
    else
        self.view.commonGemList.view.animationWrapper:PlayOutAnimation(function()
            self.view.btnEmpty.gameObject:SetActive(false)
            self.view.commonGemList.gameObject:SetActive(false)
        end)
    end
end

GemRecastCtrl._RefreshGemRecastCellAddOn = HL.Method(HL.Table, HL.Opt(HL.Table, HL.Function)) << function(self, cell, itemInfo, onClick)
    local isEmpty = itemInfo == nil
    cell.btnEmpty.gameObject:SetActive(isEmpty)
    cell.item.gameObject:SetActive(not isEmpty)
    cell.currentSelected.gameObject:SetActive(not isEmpty)
    cell.equipped.gameObject:SetActive(not isEmpty)
    cell.btnDelete.gameObject:SetActive(not isEmpty)
    cell.disableMask.gameObject:SetActive(false)

    cell.btnEmpty.onClick:RemoveAllListeners()
    cell.btnEmpty.onClick:AddListener(function()
        self:_ToggleGemList(true)
    end)

    if isEmpty then
        return
    end

    local instId = itemInfo.instId
    local gemInst = CharInfoUtils.getGemByInstId(instId)
    local isEquipped = gemInst.weaponInstId and gemInst.weaponInstId > 0

    cell.equipped.gameObject:SetActive(isEquipped)
    cell.btnDelete.onClick:RemoveAllListeners()
    cell.btnDelete.onClick:AddListener(function()
        self:_RemoveIngredient(itemInfo)
    end)

    local isSelected = self.m_gemIngredientDict[instId] ~= nil
    cell.btnDelete.gameObject:SetActive(isSelected)
    cell.currentSelected.gameObject:SetActive(isSelected)
end

GemRecastCtrl._AddIngredient = HL.Method(HL.Table) << function(self, itemInfo)
    local instId = itemInfo.instId
    local curFormula = self.m_sortedFormulas[self.m_curSelectFormulaIndex]
    if #self.m_gemIngredientList >= curFormula.costGemNum then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_GEM_FORMULA_ITEM_FULL_TOAST)
        return
    end

    local isLock = GameInstance.player.inventory:IsItemLocked(Utils.getCurrentScope(), itemInfo.itemInst.templateId, itemInfo.itemInst.instId)
    if isLock then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_UPGRADE_WEAPON_LOCKED)
        return
    end

    local gemInst = CharInfoUtils.getGemByInstId(instId)
    local isEquipped = gemInst.weaponInstId > 0

    if isEquipped then
        local weaponInst = CharInfoUtils.getWeaponByInstId(gemInst.weaponInstId)
        local weaponItemCfg = Tables.itemTable[weaponInst.templateId]
        Notify(MessageConst.SHOW_POP_UP, {
            content = string.format(Language.LUA_GEM_RECAST_GEM_EQUIPPED, weaponItemCfg.name),
            onConfirm = function()
                GameInstance.player.charBag:DetachGem(weaponInst.instId)
            end,
            onCancel = function()
            end
        })
        return
    end


    self.m_gemIngredientDict[itemInfo.instId] = itemInfo
    self.m_gemIngredientList = self:_GenerateIngredientList(self.m_gemIngredientDict)

    self.view.commonGemList:RefreshCellById(instId)
    self:_RefreshGemCellCache(curFormula)
end

GemRecastCtrl._RemoveIngredient = HL.Method(HL.Table) << function(self, itemInfo)
    local instId = itemInfo.instId
    local curFormula = self.m_sortedFormulas[self.m_curSelectFormulaIndex]
    local newGemList = {}
    self.m_gemIngredientDict[instId] = nil

    for instId, itemInfo in ipairs(self.m_gemIngredientDict) do
        table.insert(newGemList, itemInfo)
    end

    self.m_gemIngredientList = self:_GenerateIngredientList(self.m_gemIngredientDict)
    self.view.commonGemList:RefreshCellById(instId)
    self:_RefreshGemCellCache(curFormula)
end

GemRecastCtrl._GenerateIngredientList = HL.Method(HL.Table).Return(HL.Table) << function(self, ingredientDict)
    local ingredientList = {}
    for instId, itemInfo in pairs(ingredientDict) do
        table.insert(ingredientList, itemInfo)
    end

    local defaultSortKeys = UIConst.WEAPON_GEM_SORT_OPTION[1].keys
    table.sort(ingredientList, Utils.genSortFunction(defaultSortKeys))

    return ingredientList
end

GemRecastCtrl._AutoFill = HL.Method(HL.Userdata) << function(self, formula)
    local newDict = {}
    local newList = {}

    local depotList = self.view.commonGemList.m_filteredInfoList
    local needCount = formula.costGemNum
    local collectCount = 0
    for _, itemInfo in ipairs(depotList) do
        local gemInst = CharInfoUtils.getGemByInstId(itemInfo.instId)
        local hadEquipped = gemInst.weaponInstId and gemInst.weaponInstId > 0
        local isLock = GameInstance.player.inventory:IsItemLocked(Utils.getCurrentScope(), itemInfo.itemInst.templateId, itemInfo.itemInst.instId)

        if (not isLock) and (not hadEquipped) then
            newDict[itemInfo.instId] = itemInfo
            table.insert(newList, itemInfo)
            collectCount = collectCount + 1
        end
        if collectCount >= needCount then
            break
        end
    end

    self.m_gemIngredientDict = newDict
    self.m_gemIngredientList = newList

    self:_RefreshGemCellCache(formula)
    self.view.commonGemList:RefreshAllCells()
end

GemRecastCtrl._OnItemLockedStateChanged = HL.Method(HL.Table) << function(self, arg)
    local itemId, instId, isLock = unpack(arg)
    if not isLock then
        return
    end

    if not instId or instId <= 0 then
        return
    end

    if self.m_gemIngredientDict[instId] then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_ITEM_LOCK_TOAST)
        self:_RemoveIngredient(self.m_gemIngredientDict[instId])
    end
end

GemRecastCtrl.OnGemDetach = HL.Method(HL.Table) << function(self, arg)
    self.view.commonGemList:RefreshAllCells()
end

GemRecastCtrl.OnGemRecast = HL.Method(HL.Table) << function(self, arg)
    local title
    if self.m_curTab == GEM_RECAST_TAB_TYPE.FORGE then
        title = Language.LUA_GEM_FORGE_REWARD_TITLE
    else
        title = Language.LUA_GEM_RECAST_REWARD_TITLE
    end

    local gemInstIds = unpack(arg)
    local items = {}
    for _, v in pairs(gemInstIds) do
        local gemInst = CharInfoUtils.getGemByInstId(v)
        if gemInst ~= nil then
            table.insert(items, {
                id = gemInst.templateId,
                instId = v,
                count = 1,
            })
        end
    end

    if #items <= 0 then
        return
    end

    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        title = title,
        items = items,
    })

    self:_RefreshMainPanel(self.m_curTab)
end


HL.Commit(GemRecastCtrl)
