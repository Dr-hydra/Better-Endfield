
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.BattlePassWeaponCase

BattlePassWeaponCaseCtrl = HL.Class('BattlePassWeaponCaseCtrl', uiCtrl.UICtrl)

local CellStateName = {
    Normal = 'Normal',
    Selected = 'Selected',
}

local CellAnimeName = {
    Normal = 'battlepassweaponcase_cell_slcout',
    Selected = 'battlepassweaponcase_cell_slc',
}





BattlePassWeaponCaseCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SC_OPEN_USABLE_ITEM_CHEST] = '_OnOpenChest',
}







BattlePassWeaponCaseCtrl.m_arg = HL.Field(HL.Table)

BattlePassWeaponCaseCtrl.m_selectedRewardId = HL.Field(HL.Any)

BattlePassWeaponCaseCtrl.m_selectedCell = HL.Field(HL.Table)

BattlePassWeaponCaseCtrl.m_selectedListCell = HL.Field(HL.Table)

BattlePassWeaponCaseCtrl.m_chestData = HL.Field(HL.Userdata)

BattlePassWeaponCaseCtrl.isAutoOpenWhenGet = HL.Field(HL.Boolean) << false

BattlePassWeaponCaseCtrl.m_isPreview = HL.Field(HL.Boolean) << false

BattlePassWeaponCaseCtrl.m_itemCellCache = HL.Field(HL.Forward("UIListCache"))

BattlePassWeaponCaseCtrl.m_getListCell = HL.Field(HL.Function)

BattlePassWeaponCaseCtrl.m_selectedIndexTiled = HL.Field(HL.Number) << -1

BattlePassWeaponCaseCtrl.m_selectedIndexList = HL.Field(HL.Number) << -1

BattlePassWeaponCaseCtrl.m_leftToggleBindingId = HL.Field(HL.Number) << -1

BattlePassWeaponCaseCtrl.m_rightToggleBindingId = HL.Field(HL.Number) << -1

BattlePassWeaponCaseCtrl.OpenBPWeaponCase = HL.StaticMethod(HL.Any) << function(arg)
    local itemId, isPreview, isAutoOpenWhenGet, subTitle = unpack(arg)
    local luaArg = {
        itemId = itemId,
        isPreview = isPreview,
        isAutoOpenWhenGet = isAutoOpenWhenGet,
        subTitle = subTitle
    }
    UIManager:Open(PanelId.BattlePassWeaponCase, luaArg)
end


BattlePassWeaponCaseCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg
    self.isAutoOpenWhenGet = self.m_arg.isAutoOpenWhenGet == true
    self.m_isPreview = self.m_arg.isPreview == true
    self.view.txtDesc.text = Language.LUA_BATTLEPASS_WEAPON_CHEST_TITLE
    local hint = arg.subTitle
    if string.isEmpty(hint) then
        hint = self.isAutoOpenWhenGet and Language.LUA_BATTLEPASS_WEAPON_CHEST_SUBTITLE or ""
    end
    self.view.txtHint.text = hint
    self.view.txtHint.gameObject:SetActive(not string.isEmpty(hint))
    self.view.titleTxt.text = self.m_isPreview and
        Language.LUA_BATTLEPASS_WEAPON_CHEST_TITLE_LEFT_PREVIEW or Language.LUA_BATTLEPASS_WEAPON_CHEST_TITLE_LEFT
    self.view.desc3Text.gameObject:SetActive(not self.m_isPreview)
    self.m_itemCellCache = UIUtils.genCellCache(self.view.weaponCell)
    self.m_getListCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self:_InitAction()
    self:_InitController()
    self:_Refresh()
    self.view.scrollRect.normalizedPosition = 0
    self.view.showToggle.onValueChanged:AddListener(function(isOn)
        if isOn then
            self.m_selectedIndexList = -1
        else
            self.m_selectedIndexTiled = -1
        end
        self:_RefreshToggleMode(isOn)
    end)
    local leftActionId = self.view.leftKeyHint.actionId
    local rightActionId = self.view.rightKeyHint.actionId
    self.m_leftToggleBindingId = self:BindInputPlayerAction(leftActionId, function()
        self.view.showToggle.isOn = false
    end)
    self.m_rightToggleBindingId = self:BindInputPlayerAction(rightActionId, function()
        self.view.showToggle.isOn = true
    end)
    local recoverState = arg and arg.recoverState
    if recoverState then
        self:_TryRecoverState(recoverState)
        self.m_arg.recoverState = nil
    end
end

BattlePassWeaponCaseCtrl.OnShow = HL.Override() << function(self)
    self:_RefreshToggleMode(self.view.showToggle.isOn)
end

BattlePassWeaponCaseCtrl._CanPutWeaponCount = HL.Method(HL.String, HL.Number).Return(HL.Boolean) << function(self, weaponId, count)
    local depots = GameInstance.player.inventory.valuableDepots
    if not depots:ContainsKey(GEnums.ItemValuableDepotType.Weapon) then
        return true
    end
    local weaponDepot = depots[GEnums.ItemValuableDepotType.Weapon]:GetOrFallback(Utils.getCurrentScope())
    if not weaponDepot then
        return true
    end
    return weaponDepot:GetUsedGridCount() + count <= weaponDepot.gridLimit
end

BattlePassWeaponCaseCtrl._TryJumpToValuableDepotWhenWeaponDepotFull = HL.Method(HL.String, HL.Number).Return(HL.Boolean) << function(self, weaponId, count)
    if self:_CanPutWeaponCount(weaponId, count) then
        return false
    end
    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_GACHA_WEAPON_EXTRA_POP_UP_TEXT,
        onConfirm = function()
            local isOpen, valuableDepotCtrl = UIManager:IsOpen(PanelId.ValuableDepot)
            if isOpen and valuableDepotCtrl then
                valuableDepotCtrl:EnterWeaponDestroyMode()
                self:PlayAnimationOutAndClose()
                return
            end
            PhaseManager:OpenPhase(PhaseId.ValuableDepot, {
                depotType = GEnums.ItemValuableDepotType.Weapon,
                inDestroyMode = true,
                shouldClearScreenOnOpen = true,
            })
        end,
    })
    return true
end

BattlePassWeaponCaseCtrl._InitAction = HL.Method() << function(self)
    self.view.btnBack.onClick:AddListener(function()
        self:_CloseWithConfirm()
    end)
    self.view.btnMore.gameObject:SetActive(self.m_isPreview)
    self.view.btnMore.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.btnCancel.gameObject:SetActive(not self.m_isPreview)
    self.view.btnCancel.onClick:AddListener(function()
        self:_CloseWithConfirm()
    end)
    self.view.btnConfirm.onClick:AddListener(function()
        if self.m_selectedRewardId then
            local itemId = UIUtils.getRewardFirstItem(self.m_selectedRewardId).id
            Notify(MessageConst.SHOW_POP_UP, {
                content = Language.LUA_BATTLEPASS_WEAPON_CHEST_CONFIRM_TITLE,
                subContent = string.format(Language.LUA_BATTLEPASS_WEAPON_CHEST_CONFIRM_CONTENT_FORMAT, Tables.itemTable[itemId].name),
                onConfirm = function()
                    if Tables.itemTable[itemId].type == GEnums.ItemType.Weapon and
                        self:_TryJumpToValuableDepotWhenWeaponDepotFull(itemId, 1) then
                        return
                    end
                    GameInstance.player.inventory:OpenUsableItemChest(self.m_chestData.id, 1, {self.m_selectedRewardId })
                end,
            })
        end
    end)
    self:_UpdateBtns()
end

BattlePassWeaponCaseCtrl._CloseWithConfirm = HL.Method() << function(self)
    if self.isAutoOpenWhenGet then
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_BATTLEPASS_WEAPON_CHEST_CANCEL_TITLE,
            subContent = Language.LUA_BATTLEPASS_WEAPON_CHEST_CANCEL_CONTENT,
            onConfirm = function()
                self:PlayAnimationOutAndClose()
            end,
        })
    else
        self:PlayAnimationOutAndClose()
    end
end

BattlePassWeaponCaseCtrl._UpdateBtns = HL.Method() << function(self)
    local isSelected = self.m_selectedRewardId ~= nil
    self.view.btnConfirm.gameObject:SetActive(not self.m_isPreview and isSelected)
    self.view.emptyNode.gameObject:SetActive(not self.m_isPreview and not isSelected)
end

BattlePassWeaponCaseCtrl._RefreshToggleMode = HL.Method(HL.Boolean) << function(self, isOn)
    self.view.tileViewNode.gameObject:SetActive(isOn)
    self.view.centerNode.gameObject:SetActive(not isOn)
    InputManagerInst:ForceBindingKeyhintToGray(self.m_leftToggleBindingId, not isOn)
    InputManagerInst:ForceBindingKeyhintToGray(self.m_rightToggleBindingId, isOn)
    if self.m_arg.recoverState then
        return
    end
    local cell
    if isOn then
        local effectiveIndex = self:_GetSelectedIndexByMode(true)
        if effectiveIndex ~= -1 then
            self.view.scrollList:ScrollToIndex(CSIndex(effectiveIndex), true)
            cell = self.view.scrollList:Get(CSIndex(effectiveIndex))
        end
        if cell then
            cell = Utils.wrapLuaNode(cell)
        end
        if self.m_selectedIndexList ~= effectiveIndex then
            self:_OnWeaponListCellClicked(cell, self.m_selectedRewardId, effectiveIndex)
        end
        if not cell and DeviceInfo.usingController then
            self.view.scrollList:ScrollToIndex(CSIndex(1), true)
            cell = self.view.scrollList:Get(CSIndex(1))
            cell = Utils.wrapLuaNode(cell)
        end
        if cell then
            self:_SetModeNaviTarget(true, cell)
        end
    else
        local effectiveIndex = self:_GetSelectedIndexByMode(false)
        if effectiveIndex ~= -1 then
            cell = self.m_itemCellCache:GetItem((effectiveIndex))
            self.view.scrollRect:AutoScrollToRectTransform(cell.gameObject.transform, true)
        end
        if self.m_selectedIndexTiled ~= effectiveIndex then
            self:_OnWeaponCellClicked(cell, self.m_selectedRewardId, effectiveIndex)
        end
        if not cell and DeviceInfo.usingController then
            cell = self.m_itemCellCache:GetItem(1)
            self.view.scrollRect:AutoScrollToRectTransform(cell.gameObject.transform, true)
            local _, itemChestData = Tables.usableItemChestTable:TryGetValue(self.m_arg.itemId)
            local rewardId = itemChestData.rewardIdList[CSIndex(1)]
            self:_OnWeaponCellClicked(cell, rewardId, 1)
        end
        if cell then
            self:_SetModeNaviTarget(false, cell)
        end
    end
end

BattlePassWeaponCaseCtrl._GetSelectedIndexByMode = HL.Method(HL.Boolean).Return(HL.Number) << function(self, isOn)
    if isOn then
        return self.m_selectedIndexList ~= -1 and self.m_selectedIndexList or self.m_selectedIndexTiled
    end
    return self.m_selectedIndexTiled ~= -1 and self.m_selectedIndexTiled or self.m_selectedIndexList
end

BattlePassWeaponCaseCtrl._SetModeNaviTarget = HL.Method(HL.Boolean, HL.Opt(HL.Table)) << function(self, isOn, cell)
    if not cell then
        return
    end
    self:SetNaviTarget(isOn and cell.button or cell.btnConfirm)
end

BattlePassWeaponCaseCtrl._Refresh = HL.Method() << function(self)
    local _, itemChestData = Tables.usableItemChestTable:TryGetValue(self.m_arg.itemId)
    self.m_chestData = itemChestData
    if not itemChestData then
        self.m_itemCellCache:Refresh(0)
        self.view.scrollList:UpdateCount(0)
        return
    end
    local itemCount = #itemChestData.rewardIdList

    self.m_itemCellCache:Refresh(itemCount, function(cell, index)
        local rewardId = itemChestData.rewardIdList[CSIndex(index)]
        local weaponId = UIUtils.getRewardFirstItem(rewardId).id
        cell.nodeState:SetState(CellStateName.Normal)
        cell.btnConfirm.onClick:RemoveAllListeners()
        cell.btnConfirm.onClick:AddListener(function()
            if self.m_isPreview and not DeviceInfo.usingController then
                self:_PreviewWeapon(weaponId)
                return
            end
            self:_OnWeaponCellClicked(cell, rewardId, index)
        end)
        cell.btnDetail.onClick:RemoveAllListeners()
        cell.btnDetail.onClick:AddListener(function()
            self:_PreviewWeapon(weaponId)
        end)
        local _, itemData = Tables.itemTable:TryGetValue(weaponId)
        if itemData then
            cell.weaponImg:LoadSprite(UIConst.UI_SPRITE_GACHA_WEAPON, itemData.iconId)
            cell.nameTxt.text = itemData.name
            local stateName = self:_GetCellRarityStateName(itemData.rarity)
            cell.selectedNode:SetState(stateName)
            cell.unselectedNode:SetState(stateName)
            cell.numTxt.text = tostring(Utils.getItemCount(weaponId))
            cell.starGroup:InitStarGroup(itemData.rarity)
        end

        if DeviceInfo.usingController then
            cell.btnDetailInputGroup.enabled = false
            if not self.m_isPreview then
                InputManagerInst:SetBindingText(cell.btnConfirm.hoverConfirmBindingId, Language.LUA_BATTLEPASS_WEAPON_SELECT)
            else
                InputManagerInst:SetBindingText(cell.btnConfirm.hoverConfirmBindingId, "")
            end

            cell.btnConfirm.onIsNaviTargetChanged = function(isTarget)
                cell.btnDetailInputGroup.enabled = isTarget
            end

        end
    end)

    if itemCount > 7 then
        LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.scrollRectContent.transform)
        self:_StartTimer(0, function()
            self.view.scrollRect.normalizedPosition = Vector2.zero
        end)
    end

    self.view.scrollList.onUpdateCell:AddListener(function(object, csIndex)
        local rewardId = itemChestData.rewardIdList[csIndex]
        local weaponId = UIUtils.getRewardFirstItem(rewardId).id
        local cell = self.m_getListCell(object)
        cell.itemBlack:InitItem({
            id = weaponId,
            forceHidePotentialStar = true,
            count = 1,
        }, true)

        local _, itemData = Tables.itemTable:TryGetValue(weaponId)
        if itemData then
            cell.nameTxt.text = itemData.name
            cell.numberTxt.text = Utils.getItemCount(weaponId)
        end
        if DeviceInfo.usingController then
            InputManagerInst:DeleteInGroup(cell.button.hoverBindingGroupId)
            InputManagerInst:CreateBindingByActionId("bp_weapon_case_detail", function()
                cell.itemBlack:ShowTips()
            end, cell.button.hoverBindingGroupId)
            cell.button.onIsNaviTargetChanged = function(isTarget)
                cell.keyHintParent.gameObject:SetActive(isTarget)
            end
            cell.keyHintParent.gameObject:SetActive(false)
        end
        cell.button.onClick:RemoveAllListeners()
        cell.button.onClick:AddListener(function()
            if self.m_isPreview and not DeviceInfo.usingController then
                return
            end
            self:_OnWeaponListCellClicked(cell, rewardId, LuaIndex(csIndex))
        end)
    end)
    self.view.scrollList:UpdateCount(itemCount)
end

BattlePassWeaponCaseCtrl._OnWeaponCellClicked = HL.Method(HL.Table, HL.Any, HL.Number) << function(self, cell, rewardId, index)
    if self.m_selectedIndexTiled == index or not cell then
        if DeviceInfo.usingController then
            return
        end
        if self.m_selectedCell then
            self.m_selectedCell.nodeState:SetState(CellStateName.Normal)
        end
        if self.m_selectedListCell then
            self.m_selectedListCell.animationWrapper:Play(CellAnimeName.Normal)
        end
        self.m_selectedCell = nil
        self.m_selectedListCell = nil
        self.m_selectedRewardId = nil
        self.m_selectedIndexTiled = -1
        self.m_selectedIndexList = -1
        self:_UpdateBtns()
        return
    end
    if self.m_selectedCell then
        self.m_selectedCell.nodeState:SetState(CellStateName.Normal)
    end
    cell.nodeState:SetState(CellStateName.Selected)
    self.m_selectedCell = cell
    self.m_selectedRewardId = rewardId
    self.m_selectedIndexTiled = index
    self:_UpdateBtns()
end

BattlePassWeaponCaseCtrl._OnWeaponListCellClicked = HL.Method(HL.Table, HL.Any, HL.Number) << function(self, cell, rewardId, index)
    if self.m_selectedIndexList == index or not cell then
        if DeviceInfo.usingController then
            return
        end
        if self.m_selectedListCell then
            self.m_selectedListCell.animationWrapper:Play(CellAnimeName.Normal)
        end
        if self.m_selectedCell then
            self.m_selectedCell.nodeState:SetState(CellStateName.Normal)
        end
        self.m_selectedListCell = nil
        self.m_selectedCell = nil
        self.m_selectedRewardId = nil
        self.m_selectedIndexList = -1
        self.m_selectedIndexTiled = -1
        self:_UpdateBtns()
        return
    end
    if self.m_selectedListCell then
        self.m_selectedListCell.animationWrapper:Play(CellAnimeName.Normal)
    end
    cell.animationWrapper:Play(CellAnimeName.Selected)
    self.m_selectedListCell = cell
    self.m_selectedRewardId = rewardId
    self.m_selectedIndexList = index
    self:_UpdateBtns()
end


BattlePassWeaponCaseCtrl._PreviewWeapon = HL.Method(HL.String) << function(self, weaponId)
    local weaponGroupData = {
        title = Language.LUA_BATTLEPASS_WEAPON_CHEST_TITLE,
        weaponIds = {},
    }
    for _, rewardId in pairs(self.m_chestData.rewardIdList) do
        local weaponId = UIUtils.getRewardFirstItem(rewardId).id
        table.insert(weaponGroupData.weaponIds, weaponId)
    end


    local showWeaponPreviewArgs = {
        weaponId = weaponId,
        weaponGroups = { weaponGroupData },
    }
    WikiUtils.showWeaponPreview(showWeaponPreviewArgs)
end

BattlePassWeaponCaseCtrl._GetCellRarityStateName = HL.Method(HL.Number).Return(HL.String) << function(self, rarity)
    local stateName = ''
    if rarity <= 4 then
        stateName = 'Low'
    elseif rarity == 5 then
        stateName = 'Mid'
    else
        stateName = 'High'
    end
    return stateName
end

BattlePassWeaponCaseCtrl._OnOpenChest = HL.Method(HL.Table) << function(self, args)
    local openCount = args[1]
    if openCount == 0 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_USABLE_ITEM_CHEST_OPEN_FAILED)
        return
    end
    self:Close()
    local rewardPack = GameInstance.player.inventory:ConsumeLatestRewardPackOfType(CS.Beyond.GEnums.RewardSourceType.ItemCase)
    local items = {}
    local chars = nil
    if rewardPack and rewardPack.rewardSourceType == CS.Beyond.GEnums.RewardSourceType.ItemCase then
        for _, itemBundle in pairs(rewardPack.itemBundleList) do
            local _, itemData = Tables.itemTable:TryGetValue(itemBundle.id)
            if itemData then
                local putInside = false
                for i = 1, #items do
                    if items[i].id == itemData.id and itemBundle.instId == 0 then
                        items[i].count = items[i].count + itemBundle.count
                        putInside = true
                        break
                    end
                end

                if not putInside then
                    table.insert(items, {id = itemBundle.id,
                                         count = itemBundle.count,
                                         instData = itemBundle.instData,
                                         instId = itemBundle.instId,
                                         rarity = itemData.rarity,
                                         type = itemData.type:ToInt()})
                end
            end
        end
        table.sort(items, Utils.genSortFunction({"rarity", "type", "id"}, false))

        chars = rewardPack.chars
    end
    local rewardPanelArgs = {}
    rewardPanelArgs.items = items
    rewardPanelArgs.chars = chars
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, rewardPanelArgs)
end

BattlePassWeaponCaseCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

BattlePassWeaponCaseCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = lume.deepCopy(self.m_arg)
    arg.recoverState = {
        showToggleIsOn = self.view.showToggle.isOn,
        selectedIndexTiled = self.m_selectedIndexTiled,
        selectedIndexList = self.m_selectedIndexList,
        selectedRewardId = self.m_selectedRewardId,
    }
    return arg
end

BattlePassWeaponCaseCtrl._TryRecoverState = HL.Method(HL.Table) << function(self, recoverState)
    local selectedRewardId = recoverState.selectedRewardId
    local selectedIndexTiled = recoverState.selectedIndexTiled or -1
    local selectedIndexList = recoverState.selectedIndexList or -1
    local showToggleIsOn = recoverState.showToggleIsOn == true
    local itemCount = self.m_chestData and #self.m_chestData.rewardIdList or 0
    if itemCount == 0 then
        return
    end
    if showToggleIsOn then
        self.view.showToggle.isOn = true
        local effectiveIndex = selectedIndexList ~= -1 and selectedIndexList or selectedIndexTiled
        local cell
        if effectiveIndex ~= -1 and effectiveIndex <= itemCount and selectedRewardId ~= nil then
            cell = self.view.scrollList:Get(CSIndex(effectiveIndex))
        else
            effectiveIndex = 1
            local _, itemChestData = Tables.usableItemChestTable:TryGetValue(self.m_arg.itemId)
            selectedRewardId = itemChestData.rewardIdList[CSIndex(effectiveIndex)]
            cell = self.view.scrollList:Get(CSIndex(effectiveIndex))
        end
        if cell then
            cell = Utils.wrapLuaNode(cell)
            self.view.scrollList:ScrollToIndex(CSIndex(effectiveIndex), true)
            self:_OnWeaponListCellClicked(cell, selectedRewardId, effectiveIndex)
            self:_SetModeNaviTarget(true, cell)
        end
    else
        local cell
        if selectedIndexTiled ~= -1 and selectedIndexTiled <= itemCount and selectedRewardId ~= nil then
            cell = self.m_itemCellCache:GetItem(selectedIndexTiled)
        else
            selectedIndexTiled = 1
            cell = self.m_itemCellCache:GetItem(selectedIndexTiled)
            local _, itemChestData = Tables.usableItemChestTable:TryGetValue(self.m_arg.itemId)
            selectedRewardId = itemChestData.rewardIdList[CSIndex(selectedIndexTiled)]
        end
        if cell then
            self:_OnWeaponCellClicked(cell, selectedRewardId, selectedIndexTiled)
            self.view.scrollRect:AutoScrollToRectTransform(cell.gameObject.transform, true)
            self:_SetModeNaviTarget(false, cell)
        end
    end
end

BattlePassWeaponCaseCtrl._GetRewardIdByIndex = HL.Method(HL.Number).Return(HL.Opt(HL.Any)) << function(self, index)
    if not self.m_chestData or index <= 0 or index > #self.m_chestData.rewardIdList then
        return
    end
    return self.m_chestData.rewardIdList[CSIndex(index)]
end

HL.Commit(BattlePassWeaponCaseCtrl)
