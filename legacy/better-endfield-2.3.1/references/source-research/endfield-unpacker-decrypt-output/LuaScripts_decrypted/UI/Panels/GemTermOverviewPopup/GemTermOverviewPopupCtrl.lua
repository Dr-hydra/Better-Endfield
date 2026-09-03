local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GemTermOverviewPopup

GemTermOverviewPopupCtrl = HL.Class('GemTermOverviewPopupCtrl', uiCtrl.UICtrl)






GemTermOverviewPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}


GemTermOverviewPopupCtrl.m_info = HL.Field(HL.Table)

GemTermOverviewPopupCtrl.m_weaponItemCellListCache = HL.Field(HL.Forward("UIListCache"))

GemTermOverviewPopupCtrl.m_termGroupCellListCache = HL.Field(HL.Forward("UIListCache"))




GemTermOverviewPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self:_InitData(arg)
    self:_RefreshAllUI()
end



GemTermOverviewPopupCtrl._InitData = HL.Method(HL.Any) << function(self, arg)
    local gameGroupId = arg

    self.m_info = {
        gameGroupId = gameGroupId,
        weaponInfos = {},
        weaponInfoMap = {},
        termGroupInfos = {},
    }
    for i = 1, 3 do
        table.insert(self.m_info.termGroupInfos, {
            titleName = Language["LUA_GEMCUSTOMIZATIONBOX_TAB_GROUP_ATTR_GROUP_NAME" .. i],
            termInfos = {},
            weaponTagMap = {},

            termCellListCache = nil,
        })
    end

    local cfg = Tables.worldEnergyPointGroupTable:GetValue(gameGroupId)
    local termIdsCfgList = {
        cfg.primAttrTermIds,
        cfg.secAttrTermIds,
        cfg.skillTermIds,
    }
    for i = 1, 3 do
        local ids = termIdsCfgList[i]
        for _, termId in pairs(ids) do
            local hasCfg, termCfg = Tables.gemTable:TryGetValue(termId)
            if not hasCfg then
                logger.error("词条id配置不存在，id: " .. termId)
            else
                table.insert(self.m_info.termGroupInfos[i].termInfos, {
                    termId = termId,
                    termName = termCfg.tagName,
                })
                self.m_info.termGroupInfos[i].weaponTagMap[termCfg.tagId] = true
            end
        end
    end

    for weaponId, weaponCfg in pairs(Tables.weaponBasicTable) do

        local allSkillMatch = true
        for _, skillId in pairs(weaponCfg.weaponSkillList) do
            local _, skillCfg = Tables.skillPatchTable:TryGetValue(skillId)
            local skillPatchData = skillCfg.SkillPatchDataBundle[0]
            local checkTagId = skillPatchData.tagId
            local notMatchTag = true
            for _, groupInfo in pairs(self.m_info.termGroupInfos) do
                if groupInfo.weaponTagMap[checkTagId] then
                    notMatchTag = false
                    break
                end
            end
            if notMatchTag then
                allSkillMatch = false
                break
            end
        end
        if allSkillMatch then
            local _, itemCfg = Tables.itemTable:TryGetValue(weaponId)

            local weaponItemInfo = {
                id = weaponId,
                count = 0,
                forceHidePotentialStar = true,

                isEquipped = false,
                isOwned = false,

                equippedSort = 0,
                ownedSort = 0,
                rarity = itemCfg.rarity,
                sortId1 = itemCfg.sortId1,
                sortId2 = itemCfg.sortId2,
            }
            table.insert(self.m_info.weaponInfos, weaponItemInfo)
            self.m_info.weaponInfoMap[weaponId] = weaponItemInfo
        end
    end

    local weaponDepot = GameInstance.player.inventory.valuableDepots[GEnums.ItemValuableDepotType.Weapon]:GetOrFallback(Utils.getCurrentScope())
    local weaponInstDict = weaponDepot.instItems
    for instId, instItemBundle in cs_pairs(weaponInstDict) do
        local weaponItemInfo = self.m_info.weaponInfoMap[instItemBundle.id]
        if weaponItemInfo then
            weaponItemInfo.isOwned = true
            weaponItemInfo.ownedSort = 1
            local equippedCardInstId = instItemBundle.instData.equippedCharServerId
            local isEquipped = equippedCardInstId and equippedCardInstId > 0
            if isEquipped then
                weaponItemInfo.isEquipped = true
                weaponItemInfo.equippedSort = 1
            end
        end
    end

    table.sort(self.m_info.weaponInfos, Utils.genSortFunction({ "equippedSort", "ownedSort", "rarity", "sortId1", "sortId2", "id" }))
end



GemTermOverviewPopupCtrl._InitUI = HL.Method() << function(self)
    self.view.closeBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.fullScreenCloseBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)

    self.m_weaponItemCellListCache = UIUtils.genCellCache(self.view.weaponItemCell)
    self.m_termGroupCellListCache = UIUtils.genCellCache(self.view.termGroupCell)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.view.weaponList.onIsFocusedChange:AddListener(function(isTarget)
        if isTarget then
            self.view.contentScrollView:AutoScrollToRectTransform(self.view.weaponList:GetComponent("RectTransform"),
                                                                  true)
        end
    end)
end

GemTermOverviewPopupCtrl._RefreshAllUI = HL.Method() << function(self)
    self.m_termGroupCellListCache:Refresh(#self.m_info.termGroupInfos, function(cell, luaIndex)
        self:_RefreshTermGroupCell(cell, luaIndex)
    end)
    self.m_weaponItemCellListCache:Refresh(#self.m_info.weaponInfos, function(cell, luaIndex)
        self:_RefreshWeaponItemCell(cell, luaIndex)
    end)
end

GemTermOverviewPopupCtrl._RefreshTermGroupCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)

    local groupCell = cell
    local groupInfo = self.m_info.termGroupInfos[luaIndex]
    groupCell.titleTxt.text = groupInfo.titleName
    if not groupInfo.termCellListCache then
        groupInfo.termCellListCache = UIUtils.genCellCache(groupCell.termCell)
    end
    groupInfo.termCellListCache:Refresh(#groupInfo.termInfos, function(termCell, termCellIndex)
        termCell.nameTxt.text = groupInfo.termInfos[termCellIndex].termName
    end)
end

GemTermOverviewPopupCtrl._RefreshWeaponItemCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local weaponInfo = self.m_info.weaponInfos[luaIndex]
    cell.itemCell:InitItem(weaponInfo, function()
        UIUtils.showItemSideTips(cell.itemCell)
    end)
    cell.itemCell:SetExtraInfo({ isSideTips = DeviceInfo.usingController })

    if weaponInfo.isEquipped then
        cell.extraTagStateCtrl:SetState("Equipped")
    elseif weaponInfo.isOwned then
        cell.extraTagStateCtrl:SetState("Owned")
    else
        cell.extraTagStateCtrl:SetState("Empty")
    end
end





HL.Commit(GemTermOverviewPopupCtrl)
