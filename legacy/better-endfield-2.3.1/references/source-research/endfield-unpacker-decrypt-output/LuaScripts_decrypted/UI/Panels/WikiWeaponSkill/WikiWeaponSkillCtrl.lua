
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WikiWeaponSkill

WikiWeaponSkillCtrl = HL.Class('WikiWeaponSkillCtrl', uiCtrl.UICtrl)






WikiWeaponSkillCtrl.s_messages = HL.StaticField(HL.Table) << {

}

WikiWeaponSkillCtrl.m_curWikiEntryShowData = HL.Field(HL.Table)

WikiWeaponSkillCtrl.m_arg = HL.Field(HL.Table)

WikiWeaponSkillCtrl.m_curWeaponSkillShowData = HL.Field(HL.Table)




WikiWeaponSkillCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg or {}
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    UIUtils.bindHyperlinkPopup(self, "wiki_weapon_skill", self.view.inputGroup.groupId)

    self.m_curWikiEntryShowData = arg
    self:_RefreshLeft()
    self:_RefreshCenter()
    self:_ApplyResumeState(self.m_arg and self.m_arg.resumeState or nil)
    if self.m_arg then
        self.m_arg.resumeState = nil
    end

    self.view.top:InitWikiTop({
        phase = self.m_phase,
        panelId = PANEL_ID,
        wikiEntryShowData = self.m_curWikiEntryShowData,
        forceShowBackBtn = true,
    })
    self.m_phase:ActiveCommonSceneItem(true)
end



WikiWeaponSkillCtrl.m_getSkillItemCell = HL.Field(HL.Function)

WikiWeaponSkillCtrl.m_weaponSkillShowDataList = HL.Field(HL.Table)

WikiWeaponSkillCtrl.m_selectedIndex = HL.Field(HL.Number) << 0

WikiWeaponSkillCtrl.m_pendingNaviIndex = HL.Field(HL.Number) << 0

WikiWeaponSkillCtrl._RefreshLeft = HL.Method() << function(self)
    self.m_weaponSkillShowDataList = WikiUtils.getWeaponSkillShowDataList(self.m_curWikiEntryShowData.wikiEntryData.refItemId)
    if not self.m_getSkillItemCell then
        self.m_getSkillItemCell = UIUtils.genCachedCellFunction(self.view.left.scrollListLeft)
        self.view.left.scrollListLeft.onUpdateCell:AddListener(function(object, csIndex)
            local cell = self.m_getSkillItemCell(object)
            local showData = self.m_weaponSkillShowDataList[LuaIndex(csIndex)]
            cell.lockNode.gameObject:SetActive(not showData.isUnlocked)
            if showData.isUnlocked then
                local isSelected = self.m_selectedIndex == LuaIndex(csIndex)
                self:_SetCellSelected(cell, isSelected)
            else
                cell.selectNode.gameObject:SetActive(false)
                cell.normalNode.gameObject:SetActive(false)
            end
            cell.currentWeaponNode.gameObject:SetActive(showData.isUnlocked and showData.isCurrentWeaponSkill)
            if showData.isUnlocked then

                local skillPathData= Tables.skillPatchTable[showData.skillId].SkillPatchDataBundle[0]
                cell.txtTitle.text = skillPathData.skillName
                cell.txtSkillName.text = skillPathData.skillName
            end
            cell.btn.onClick:RemoveAllListeners()
            cell.btn.onClick:AddListener(function()
                if showData.isUnlocked then
                    self:_SetSelectedIndex(LuaIndex(csIndex))
                else
                    Notify(MessageConst.SHOW_TOAST, Language.WIKI_WEAPON_SKILL_LOCKED)
                end
            end)
        end)
        self.view.left.scrollListLeft.onGraduallyShowFinish:AddListener(function()
            if self.m_pendingNaviIndex <= 0 or self.m_pendingNaviIndex ~= self.m_selectedIndex then
                self.m_pendingNaviIndex = 0
                return
            end
            self:_TrySetSelectedCellNaviTarget(self.m_pendingNaviIndex)
            self.m_pendingNaviIndex = 0
        end)
    end
    local selectedIndex = 1
    local resumeState = self.m_arg and self.m_arg.resumeState or nil
    if resumeState and type(resumeState.selectedIndex) == "number" then
        selectedIndex = lume.clamp(resumeState.selectedIndex, 1, math.max(#self.m_weaponSkillShowDataList, 1))
    end
    self.view.left.scrollListLeft:UpdateCount(#self.m_weaponSkillShowDataList, CSIndex(selectedIndex))
    self:_SetSelectedIndex(selectedIndex)
end

WikiWeaponSkillCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.m_arg and lume.deepCopy(self.m_arg) or {}
    arg.resumeState = {
        selectedIndex = self.m_selectedIndex,
    }
    return arg
end

WikiWeaponSkillCtrl._ApplyResumeState = HL.Method(HL.Opt(HL.Any)) << function(self, resumeState)
    if not resumeState or not resumeState.selectedIndex or resumeState.selectedIndex <= 1 then
        return
    end
    local targetIndex = lume.clamp(resumeState.selectedIndex, 1, math.max(#self.m_weaponSkillShowDataList, 1))
    if self:_TrySetSelectedCellNaviTarget(targetIndex) then
        self.m_pendingNaviIndex = 0
    else
        self.m_pendingNaviIndex = targetIndex
    end
end

WikiWeaponSkillCtrl._TrySetSelectedCellNaviTarget = HL.Method(HL.Number).Return(HL.Boolean) << function(self, targetIndex)
    local selectedCellObj = self.view.left.scrollListLeft:Get(CSIndex(targetIndex))
    local selectedCell = selectedCellObj and self.m_getSkillItemCell(selectedCellObj) or nil
    if not selectedCell then
        return false
    end
    self:_SetCellSelected(selectedCell, true, false)
    self:SetNaviTarget(selectedCell.btn)
    return true
end

WikiWeaponSkillCtrl._SetSelectedIndex = HL.Method(HL.Number) << function(self, selectedIndex)
    if selectedIndex == self.m_selectedIndex then
        return
    end
    local lastSelectedCell = self.m_getSkillItemCell(self.view.left.scrollListLeft:Get(CSIndex(self.m_selectedIndex)))
    self:_SetCellSelected(lastSelectedCell, false, true)
    self.m_selectedIndex = selectedIndex
    local selectedCell = self.m_getSkillItemCell(self.view.left.scrollListLeft:Get(CSIndex(selectedIndex)))
    self:_SetCellSelected(selectedCell, true, true)
    self.m_curWeaponSkillShowData = self.m_weaponSkillShowDataList[selectedIndex]
    self:_RefreshCenter()
    self:_TrySetSelectedCellNaviTarget(selectedIndex)
end

WikiWeaponSkillCtrl._SetCellSelected = HL.Method(HL.Table, HL.Boolean, HL.Opt(HL.Boolean)) << function(self, cell, isSelected, playAnim)
    if not cell then
        return
    end
    if playAnim then
        UIUtils.PlayAnimationAndToggleActive(cell.selectAnimWrapper, isSelected)
    else
        cell.selectNode.gameObject:SetActive(isSelected)
    end
    cell.normalNode.gameObject:SetActive(not isSelected)
end

WikiWeaponSkillCtrl.m_getEffectCell = HL.Field(HL.Function)

WikiWeaponSkillCtrl.m_getWeaponCell = HL.Field(HL.Function)

WikiWeaponSkillCtrl.m_weaponList = HL.Field(HL.Table)

WikiWeaponSkillCtrl._RefreshCenter = HL.Method() << function(self)
    local hasValue

    local skillPatchDataBundleList
    hasValue, skillPatchDataBundleList = Tables.skillPatchTable:TryGetValue(self.m_curWeaponSkillShowData.skillId)
    local skillPatchDataCount = 0
    if hasValue then
        skillPatchDataCount = skillPatchDataBundleList.SkillPatchDataBundle.Count
    end

    if not self.m_getEffectCell then
        self.m_getEffectCell = UIUtils.genCachedCellFunction(self.view.center.scrollListSkillEffect)
        self.view.center.scrollListSkillEffect.onUpdateCell:AddListener(function(object, csIndex)
            local cell = self.m_getEffectCell(object)
            local skillLv = LuaIndex(csIndex)
            local _, _, desc = CS.Beyond.Gameplay.WeaponUtil.GetSkillNameAndDescriptionFromSkillId(self.m_curWeaponSkillShowData.skillId, skillLv)
            cell.txtNumber.text = string.format("%02d", skillLv)
            cell.txtDec:SetAndResolveTextStyle(desc)
            cell.animationWrapper:PlayInAnimation()
        end)
    end
    self.view.center.scrollListSkillEffect:UpdateCount(skillPatchDataCount, true)

    self.m_weaponList = {}
    for _, weaponData in ipairs(self.m_curWeaponSkillShowData.weaponDataList) do
        local weaponId = weaponData.id
        local _, itemData = Tables.itemTable:TryGetValue(weaponId)
        local _, weaponBasicData = Tables.weaponBasicTable:TryGetValue(weaponId)
        if itemData and weaponBasicData then
            local weapon = {
                id = weaponId,
                isUnlocked = weaponData.isUnlocked,
                weaponType = weaponBasicData.weaponType:ToInt(),
                rarity = itemData.rarity,
                unlockedState = weaponData.isUnlocked and 1 or 0
            }
            table.insert(self.m_weaponList, weapon)
        end
    end

    table.sort(self.m_weaponList, Utils.genSortFunction({"unlockedState", "weaponType", "rarity"}))

    if not self.m_getWeaponCell then
        self.m_getWeaponCell = UIUtils.genCachedCellFunction(self.view.center.scrollListWeapon)
        self.view.center.scrollListWeapon.onUpdateCell:AddListener(function(object, csIndex)
            local cell = self.m_getWeaponCell(object)
            local weapon = self.m_weaponList[LuaIndex(csIndex)]
            cell:InitItem({ id = weapon.id }, true)
            cell.view.lockedNode.gameObject:SetActive(not weapon.isUnlocked)
            if cell.view.potentialStar then
                cell.view.potentialStar.gameObject:SetActive(false)
            end
        end)
    end
    self.view.center.scrollListWeapon:UpdateCount(#self.m_weaponList)
end


HL.Commit(WikiWeaponSkillCtrl)
