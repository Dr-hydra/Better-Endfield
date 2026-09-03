
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacDepotSwitching
local PHASE_ID = PhaseId.FacDepotSwitching
FacDepotSwitchingCtrl = HL.Class('FacDepotSwitchingCtrl', uiCtrl.UICtrl)






FacDepotSwitchingCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CHANGE_SPACESHIP_DOMAIN_ID] = 'OnChangeSpaceshipDomainId',
}


FacDepotSwitchingCtrl.m_getCell = HL.Field(HL.Function)

FacDepotSwitchingCtrl.m_depotInfos = HL.Field(HL.Table)

FacDepotSwitchingCtrl.m_selectedIndex = HL.Field(HL.Number) << -1



FacDepotSwitchingCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeBtn.onClick:AddListener(function()
        self:_Exit()
    end)
    self.view.bottomMaskBtn.onClick:AddListener(function()
        self:_Exit()
    end)
    self.view.confirmBtn.onClick:AddListener(function()
        self:_OnClickConfirm()
    end)

    self.m_getCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateCell(self.m_getCell(obj), LuaIndex(csIndex))
    end)
    self.view.scrollList.onScrollOrDragStart:AddListener(function()
        self.view.selectedLine.gameObject:SetActive(false)
    end)
    self.view.scrollList.onScrollOrDragEnd:AddListener(function()
        self.view.selectedLine.gameObject:SetActive(true)
    end)

    self.view.main:SampleClipAtPercent("depot_switching", 1)

    self:_InitInfos()
    self.view.scrollList:UpdateCount(#self.m_depotInfos)
    self.view.scrollList:ScrollToIndex(CSIndex(self.m_selectedIndex), true)

    self.view.scrollList.onCenterIndexChanged:AddListener(function(oldIndex, newIndex)
        self:_OnUpdateSelectIndex(LuaIndex(newIndex))
    end)
    if self.m_selectedIndex > 0 then
        local cell = self.m_getCell(self.m_selectedIndex)
        self:SetNaviTarget(cell.button)
    end
    self:_UpdateConfirmButtonState(true)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end


FacDepotSwitchingCtrl._InitInfos = HL.Method() << function(self)
    self.m_depotInfos = {}
    local depotsInChapter = GameInstance.player.inventory.factoryDepot:GetOrFallback(Utils.getCurrentScope())
    for intId, _ in pairs(depotsInChapter) do
        local strId = ScopeUtil.ChapterIdInt2Str(intId)
        local success, data = Tables.domainDataTable:TryGetValue(strId)
        if success then
            table.insert(self.m_depotInfos, {
                intId = intId,
                strId = strId,
                data = data,
                sortId = data.sortId,
            })
        else
            logger.error("[DepotSwitching] Domain not found, domainId: " .. tostring(strId))
        end
    end
    table.sort(self.m_depotInfos, Utils.genSortFunction({ "sortId" }, false))
    local curDomainId = GameInstance.player.inventory.spaceshipDomainId
    for k, v in ipairs(self.m_depotInfos) do
        if v.strId == curDomainId then
            self.m_selectedIndex = k
            break
        end
    end
end

FacDepotSwitchingCtrl._Exit = HL.Method() << function(self)
    PhaseManager:PopPhase(PHASE_ID)
end

FacDepotSwitchingCtrl.OnChangeSpaceshipDomainId = HL.Method(HL.Table) << function(self, args)
    self.view.scrollList:UpdateCount(#self.m_depotInfos)
    local cell = self.m_getCell(self.m_selectedIndex)
    cell.animation:Play("depot_cell_switching")

    self:_UpdateConfirmButtonState(false)
    InputManagerInst:ToggleGroup(self.view.inputGroup.groupId, false)
    self.view.main:Play("depot_switching", function()
        cell.animation:Play("depot_cell_is_current_selected")
        self:_UpdateConfirmButtonState(true)
        InputManagerInst:ToggleGroup(self.view.inputGroup.groupId, true)
    end)
    AudioAdapter.PostEvent("Au_UI_Toast_SwitchWareLoading")
end

FacDepotSwitchingCtrl._OnClickConfirm = HL.Method() << function(self)
    local info = self.m_depotInfos[self.m_selectedIndex]
    if info.strId == GameInstance.player.inventory.spaceshipDomainId then
        return
    end
    GameInstance.player.inventory:ChangeSpaceshipDomainId(info.strId)
end

FacDepotSwitchingCtrl._OnUpdateCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local info = self.m_depotInfos[index]
    cell.nameTxt.text = info.data.domainName
    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        self.view.scrollList:ScrollToIndex(CSIndex(index))
    end)
    cell.icon:LoadSprite(UIConst.UI_SPRITE_INVENTORY, "icon_" .. info.strId)
    self:_UpdateCellSelected(index)
end

FacDepotSwitchingCtrl._UpdateCellSelected = HL.Method(HL.Number) << function(self, index)
    local cell = self.m_getCell(index)
    if not cell then
        return
    end
    local info = self.m_depotInfos[index]
    local isCurrent = info.strId == GameInstance.player.inventory.spaceshipDomainId
    local isSelected = index == self.m_selectedIndex
    if isSelected then
        cell.animation:Play(isCurrent and "depot_cell_is_current_selected" or "depot_cell_not_current_selected")
    else
        cell.animation:Play(isCurrent and "depot_cell_is_current" or "depot_cell_not_current")
    end
end

FacDepotSwitchingCtrl._OnUpdateSelectIndex = HL.Method(HL.Number) << function(self, index)
    if index == self.m_selectedIndex then
        return
    end
    local oldIndex = self.m_selectedIndex
    self.m_selectedIndex = index
    self:_UpdateCellSelected(oldIndex)
    self:_UpdateCellSelected(index)

    self:_UpdateConfirmButtonState(true)
end

FacDepotSwitchingCtrl._UpdateConfirmButtonState = HL.Method(HL.Boolean) << function(self, active)
    local index = self.m_selectedIndex
    local info = self.m_depotInfos[index]
    local curDomainId = GameInstance.player.inventory.spaceshipDomainId
    local isCur = info.strId == curDomainId
    self.view.confirmBtn.gameObject:SetActive(active)
    self.view.confirmBtn.interactable = not isCur
    self.view.confirmBtn.text = isCur and Language["ui_common_inventory_link_depot"] or Language["ui_char_list_new_ok"]
end


HL.Commit(FacDepotSwitchingCtrl)
