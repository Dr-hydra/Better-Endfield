local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.EquipBatchFormulaChainSelect

EquipBatchFormulaChainSelectCtrl = HL.Class('EquipBatchFormulaChainSelectCtrl', uiCtrl.UICtrl)



EquipBatchFormulaChainSelectCtrl.m_getFormulaChainCell = HL.Field(HL.Function)

EquipBatchFormulaChainSelectCtrl.m_selectedMaterialInfo = HL.Field(HL.Any)








EquipBatchFormulaChainSelectCtrl.s_messages = HL.StaticField(HL.Table) << {

}


EquipBatchFormulaChainSelectCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local preserve = true
    if arg and type(arg) == "table" and arg.preserve ~= nil then
        preserve = arg.preserve
    end

    self.view.toggle.isOn = preserve

    self:_InitBtnBinding()
    self:_InitList(arg and arg.chainSelectState)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end





EquipBatchFormulaChainSelectCtrl.OnClose = HL.Override() << function(self)
    EquipTechUtils.MarkChainsAsRead()
end




EquipBatchFormulaChainSelectCtrl.GetRecoverStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    return {
        preserve = self.view.toggle.isOn,
        chainSelectState = {
            chainId = self.m_selectedMaterialInfo and self.m_selectedMaterialInfo.chainId or nil,
            empty = self.m_selectedMaterialInfo == nil,
        }
    }
end

EquipBatchFormulaChainSelectCtrl._InitBtnBinding = HL.Method() << function(self)
    self.view.btnBack.onClick:AddListener(function()
        self:ClosePanel()
    end)

    self.view.btnHelp.onClick:AddListener(function()
        UIManager:AutoOpen(PanelId.CommonIntro, {
            introId = "equip_formula_switch",
        })
    end)

    self.view.btnConfirm.onClick:AddListener(function()
        if self.m_selectedMaterialInfo then
            local preserve = self.view.toggle.isOn
            local materialName = self.m_selectedMaterialInfo.name
            local warningText = preserve and Language.LUA_EQUIP_BATCH_FORMULA_CONFIRM_PRESERVE or Language.LUA_EQUIP_BATCH_FORMULA_CONFIRM_OVERWRITE
            Notify(MessageConst.SHOW_POP_UP, {
                content = string.format(Language.LUA_EQUIP_BATCH_FORMULA_CONFIRM, materialName),
                warningContent = warningText,
                onConfirm = function()
                    local equipTechSystem = GameInstance.player.equipTechSystem
                    equipTechSystem:BatchSetFormulaChain(self.m_selectedMaterialInfo.chainId, preserve)
                    self:ClosePanel()
                end,
            })
        end
    end)

    self.view.btnReset.onClick:AddListener(function()
        local preserve = self.view.toggle.isOn
        local warningText = preserve and Language.LUA_EQUIP_BATCH_FORMULA_CONFIRM_PRESERVE or Language.LUA_EQUIP_BATCH_FORMULA_CONFIRM_OVERWRITE
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_EQUIP_BATCH_FORMULA_RESET,
            warningContent = warningText,
            onConfirm = function()
                local equipTechSystem = GameInstance.player.equipTechSystem
                equipTechSystem:ResetFormulaChain(preserve)
                self:ClosePanel()
            end,
        })
    end)

    self.view.toggle.onValueChanged:AddListener(function(isOn)

    end)
end

EquipBatchFormulaChainSelectCtrl.ClosePanel = HL.Method() << function(self)
    self:PlayAnimationOutAndClose()
end

EquipBatchFormulaChainSelectCtrl._InitList = HL.Method(HL.Opt(HL.Any)) << function(self, chainSelectState)
    local materialList = EquipTechUtils.GetCostMaterialList()
    if not self.m_getFormulaChainCell then
        self.m_getFormulaChainCell = UIUtils.genCachedCellFunction(self.view.formulaScrollList)
        self.view.formulaScrollList.onUpdateCell:AddListener(function(object, csIdx)
            local cell = self.m_getFormulaChainCell(object)
            local luaIdx = LuaIndex(csIdx)
            local materialInfo = materialList[luaIdx]
            self:_UpdateFormulaCell(cell, materialInfo, csIdx)
        end)

        self.view.formulaScrollList.onCellSelectedChanged:AddListener(function(object, csIdx, isSelected)
            local cell = self.m_getFormulaChainCell(object)
            if cell then
                cell.formulaSelectToggle:SetIsOnWithoutNotify(isSelected)
            end
        end)

        self.view.formulaScrollList.onSelectedCell:AddListener(function(object, csIdx)
            local material = materialList[LuaIndex(csIdx)]
            if material and EquipTechUtils.IsFormulaChainUnlock(material.chainId) then
                self.m_selectedMaterialInfo = material
            else
                self.m_selectedMaterialInfo = nil
            end

            self:_UpdateConfirmInteractable()
        end)
    end

    self.m_selectedMaterialInfo = nil
    local defaultLuaIdx = nil
    local batchChainId = GameInstance.player.equipTechSystem:GetBatchChainId()
    if chainSelectState then
        if chainSelectState.chainId then
            batchChainId = chainSelectState.chainId
        elseif chainSelectState.empty then
            batchChainId = nil
        end
    end

    for luaIdx, materialInfo in ipairs(materialList) do
        if defaultLuaIdx == nil then
            defaultLuaIdx = luaIdx
        end

        if batchChainId == materialInfo.chainId then
            defaultLuaIdx = luaIdx
            break
        end
    end

    local focusLuaIdx = defaultLuaIdx or 1
    self:_RefreshList(focusLuaIdx)


    if defaultLuaIdx then
        if chainSelectState and chainSelectState.empty then
            self.view.formulaScrollList:SetSelectedIndex(-1, true, true, false)
        else
            self.view.formulaScrollList:SetSelectedIndex(CSIndex(defaultLuaIdx), true, true, false)
        end
    end

    if DeviceInfo.usingController then
        self.view.formulaScrollList.onGraduallyShowFinish:RemoveAllListeners()
        self.view.formulaScrollList.onGraduallyShowFinish:AddListener(function()
            self:_NaviToCell(focusLuaIdx)
        end)
    end

    self:_UpdateConfirmInteractable()
end

EquipBatchFormulaChainSelectCtrl._UpdateConfirmInteractable = HL.Method() << function(self)
    local showConfirm = self.m_selectedMaterialInfo ~= nil

    self.view.confirmBtnState:SetState(showConfirm and "NormalState" or "DisableState")
    self.view.btnConfirm.gameObject:SetActive(showConfirm)
    self.view.btnDisable.gameObject:SetActive(not showConfirm)
    self.view.btnConfirm.customBindingViewLabelText = showConfirm and Language.ui_set_player_name_confirm or Language.ui_equip_formula_group_switch_select_hint
end

EquipBatchFormulaChainSelectCtrl._RefreshList = HL.Method(HL.Opt(HL.Number)) << function(self, focusLuaIdx)
    local materialList = EquipTechUtils.GetCostMaterialList()
    local fastScrollToIndex = focusLuaIdx and CSIndex(focusLuaIdx) or -1
    self.view.formulaScrollList:UpdateCount(#materialList, fastScrollToIndex, true)
end

EquipBatchFormulaChainSelectCtrl._NaviToCell = HL.Method(HL.Number) << function(self, luaIdx)
    local obj = self.view.formulaScrollList:Get(CSIndex(luaIdx))
    if not obj then
        return
    end
    local cell = self.m_getFormulaChainCell(obj)
    if cell then
        self:SetNaviTarget(cell.selectable)
    end
end

EquipBatchFormulaChainSelectCtrl._UpdateFormulaCell = HL.Method(HL.Table, HL.Any, HL.Number) << function(self, cell, materialInfo, csIdx)
    local itemId = materialInfo.itemId
    local name = materialInfo.name
    local isRecommended = materialInfo.isRecommended
    local chainId = materialInfo.chainId
    local unlocked = EquipTechUtils.IsFormulaChainUnlock(chainId)

    cell.gameObject.name = itemId


    cell.formulaSelectToggle.interactable = unlocked
    cell.formulaSelectToggle.onValueChanged:RemoveAllListeners()
    cell.formulaSelectToggle.onValueChanged:AddListener(function(isOn)

        self.view.formulaScrollList:SetSelectedIndex(isOn and csIdx or -1)
    end)

    local isSelected = self.m_selectedMaterialInfo ~= nil and self.m_selectedMaterialInfo.chainId == chainId
    cell.formulaSelectToggle:SetIsOnWithoutNotify(isSelected and unlocked)
    cell.formulaSelectToggle.gameObject:SetActive(unlocked)


    local isNew = EquipTechUtils.IsChainNew(chainId)
    cell.redDot.gameObject:SetActive(isNew)
    cell.recommendLayout.gameObject:SetActive(isRecommended)
    cell.cellTitleTxt.text = name


    local compatibleList, incompatibleList = EquipTechUtils.GetMaterialCompatibility(itemId)

    cell.compatibleCache = cell.compatibleCache or UIUtils.genCellCache(cell.replaceableFormulaCell)
    cell.compatibleCache:Refresh(#compatibleList, function(subCell, luaIdx)
        local compatibleMatInfo = compatibleList[luaIdx]
        subCell.itemIcon:InitItemIcon(compatibleMatInfo.itemId)
        subCell.formulaTxt.text = compatibleMatInfo.name
    end)

    cell.incompatibleCache = cell.incompatibleCache or UIUtils.genCellCache(cell.incompatibleFormulaCell)
    cell.incompatibleCache:Refresh(#incompatibleList, function(subCell, luaIdx)
        local incompatibleMatInfo = incompatibleList[luaIdx]
        subCell.itemIcon:InitItemIcon(incompatibleMatInfo.itemId)
        subCell.incompatibleTxt.text = incompatibleMatInfo.name
    end)

    cell.stateController:SetState(self:_GetFormulaChainCellState(compatibleList, incompatibleList))




    local costInfo = EquipTechUtils.GetChainCostInfo(chainId)
    local costItemId = costInfo and costInfo.costItemId or nil
    local costGoldId = costInfo and costInfo.costGoldId or nil
    local discount = costInfo and costInfo.discount or nil

    cell.costItemCellCache = cell.costItemCellCache or UIUtils.genCellCache(cell.costItemCell)
    cell.costItemCellCache:Refresh((costItemId and 1 or 0) + (costGoldId and 1 or 0), function(subCell, luaIdx)
        if costGoldId and luaIdx == 1 then
            subCell.item:InitItem({ id = costGoldId }, true)
            subCell.item:SetExtraInfo({
                isSideTips = DeviceInfo.usingController,
            })
            local discountPercent = math.floor((1 - (discount or 1)) * 100 + 0.5)
            local showDiscountTxt = string.format("-%d", discountPercent)

            subCell.discountInfoNode.gameObject:SetActive(discountPercent > 0)
            subCell.discountNumTxt.text = showDiscountTxt
            subCell.discountNumShadownTxt.text = showDiscountTxt
        elseif costItemId and ((costGoldId and luaIdx == 2) or (not costGoldId and luaIdx == 1)) then
            subCell.item:InitItem({ id = costItemId }, true)
            subCell.item:SetExtraInfo({
                isSideTips = DeviceInfo.usingController,
            })
            subCell.discountInfoNode.gameObject:SetActive(false)
        end
    end)


    cell.lockBtnNode.gameObject:SetActive(not unlocked)
    cell.lockBtn.onClick:RemoveAllListeners()
    cell.lockBtn.onClick:AddListener(function()
        if not unlocked then
            UIManager:Open(PanelId.EquipFormulaChainAccessGuide, { chainId = chainId })
        end
    end)

    cell.selectKeyHint:SetBindingId(cell.formulaSelectToggle.toggleBindingId)
end

EquipBatchFormulaChainSelectCtrl._GetFormulaChainCellState = HL.Method(HL.Table, HL.Table).Return(HL.String) << function(self, compatibleList, incompatibleList)
    local hasCompatible = compatibleList and #compatibleList > 0
    local hasIncompatible = incompatibleList and #incompatibleList > 0

    if hasCompatible and hasIncompatible then
        return "ReplaceableAndIncompatible"
    elseif hasCompatible then
        return "NoIncompatible"
    elseif hasIncompatible then
        return "NoReplaceable"
    else
        return "ReplaceableAndIncompatible"
    end
end

HL.Commit(EquipBatchFormulaChainSelectCtrl)
