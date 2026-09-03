
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.TacticalItem
TacticalItemCtrl = HL.Class('TacticalItemCtrl', uiCtrl.UICtrl)

local CLOSE_WAIT_FX_DURATION = 0.5

local TacticalItemUtil = CS.Beyond.Gameplay.TacticalItemUtil






TacticalItemCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_USE_ITEM] = 'OnUseItem',
}

TacticalItemCtrl.m_getCharCell = HL.Field(HL.Function)

TacticalItemCtrl.m_selectCharInstIdDict = HL.Field(HL.Table)

TacticalItemCtrl.m_curItemId = HL.Field(HL.String) << ""


local USE_ITEM_CFG = {
    [GEnums.ItemUseUiType.SingleHeal] = {
        getMemberFunc = "_GetAllAliveMember",
        selectDefaultFunc = "_SelectLowestHpRate",
        refreshCellFunc = "_RefreshCharCellWithHp",
        onClick = "_OnClickSingleSelect",
        onConfirm = "UseItemOnTarget",
        afterUseCheckFunc = "_AfterUseCheckSingleHeal",
        stateName = "hp",
    },
    [GEnums.ItemUseUiType.Revive] = {
        getMemberFunc = "_GetAllDeadMember",
        selectDefaultFunc = "_SelectFirstOne",
        refreshCellFunc = "_RefreshCharCellDefault",
        onClick = "_OnClickSingleSelect",
        onConfirm = "UseItemOnTarget",
        afterUseCheckFunc = "_AfterUseCheckRevive",
        stateName = "revive",
    },
    [GEnums.ItemUseUiType.AllHeal] = {
        getMemberFunc = "_GetAllAliveMember",
        selectDefaultFunc = "_SelectAll",
        refreshCellFunc = "_RefreshCharCellWithHp",
        onConfirm = "UseItem",
        afterUseCheckFunc = "_AfterUseCheckDefault",
        stateName = "hp",
    },
    [GEnums.ItemUseUiType.Alive] = {
        getMemberFunc = "_GetAllAliveMember",
        selectDefaultFunc = "_SelectAliveDependOnTargetNumType",
        refreshCellFunc = "_RefreshCharCellWithBuff",
        onClick = "_OnClickSingleSelect",
        onConfirm = "UseItemOnTarget",
        afterUseCheckFunc = "_AfterUseCheckDefault",
        stateName = "buff",
    },
    [GEnums.ItemUseUiType.SingleUsp] = {
        getMemberFunc = "_GetAllAliveMember",
        selectDefaultFunc = "_SelectLowestUspRate",
        refreshCellFunc = "_RefreshCharCellWithUsp",
        onClick = "_OnClickSingleSelect",
        onConfirm = "UseItemOnTarget",
        afterUseCheckFunc = "_AfterUseCheckSingleUsp",
        stateName = "usp",
    },
    [GEnums.ItemUseUiType.AllUsp] = {
        getMemberFunc = "_GetAllAliveMember",
        selectDefaultFunc = "_SelectAll",
        refreshCellFunc = "_RefreshCharCellWithUsp",
        onConfirm = "UseItem",
        afterUseCheckFunc = "_AfterUseCheckDefault",
        stateName = "usp",
    },
}


TacticalItemCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local itemId = arg.itemId
    self.m_curItemId = itemId
    self:_InitActionEvent()

    self:_RefreshTacticalPanel(itemId)
end

TacticalItemCtrl.OnUseItem = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    local itemId, _ = unpack(arg)
    if itemId ~= self.m_curItemId then
        return
    end
    self:_StartCoroutine(function()
        coroutine.step()
        coroutine.step()
        local useItemCfg = Tables.useItemTable:GetValue(itemId)
        local cfg = USE_ITEM_CFG[useItemCfg.uiType]

        if cfg.afterUseCheckFunc then
            self[cfg.afterUseCheckFunc](self, itemId, true, CLOSE_WAIT_FX_DURATION)
        end
    end)
end



TacticalItemCtrl._AfterUseCheckDefault = HL.Method(HL.String, HL.Opt(HL.Boolean, HL.Number)) << function(
    self, itemId, inUseItemTransition, delayCloseTime)
    AudioAdapter.PostEvent("au_int_cure_one")
    local storageCount = Utils.getBagItemCount(itemId)
    self:_RefreshTacticalPanel(itemId, inUseItemTransition)
    if storageCount <= 0 then
        if delayCloseTime then
            self:_StartCoroutine(function()
                Notify(MessageConst.BLOCK_LUA_UI_INPUT, { true, "TacticalItem" })
                coroutine.wait(delayCloseTime)
                Notify(MessageConst.BLOCK_LUA_UI_INPUT, { false, "TacticalItem" })

                self.view.anim:PlayOutAnimation(function()
                    self:Close()
                end)
            end)
        else
            self.view.anim:PlayOutAnimation(function()
                self:Close()
            end)
        end
    end
end

TacticalItemCtrl._AfterUseCheckSingleUsp = HL.Method(HL.String, HL.Opt(HL.Boolean, HL.Number)) << function(
    self, itemId, inUseItemTransition, delayCloseTime)
    self:_PlaySelectedCharUspUpFx()


    local isSelectCharUspMax = false
    local useItemCfg = Tables.useItemTable:GetValue(itemId)
    local cfg = USE_ITEM_CFG[useItemCfg.uiType]
    local squadMembers = self[cfg.getMemberFunc](self)
    for i = 1, #squadMembers do
        local squadMember = squadMembers[i]
        if squadMember.slot then
            if squadMember.slot.character ~= nil and squadMember.slot.character.abilityCom.alive then
                local abilityCom = squadMember.slot.character.abilityCom
                local usp = abilityCom.ultimateSp
                local _, skill = abilityCom.activeSkillMap:TryGetValue(abilityCom.curUltimateSkill)
                local maxUsp = skill.data.castData.costData.costValue
                if self.m_selectCharInstIdDict and self.m_selectCharInstIdDict[squadMember.slot.charInstId] and
                    math.abs(usp - maxUsp) < 0.001 then
                    isSelectCharUspMax = true
                    break
                end
            end
        end
    end
    if isSelectCharUspMax then
        local selectCharInstIdDict, isUspMax = self[cfg.selectDefaultFunc](self, squadMembers, useItemCfg)
        if not isUspMax then
            self.m_selectCharInstIdDict = selectCharInstIdDict
        end
    end
    self:_AfterUseCheckDefault(itemId, nil, delayCloseTime)
end

TacticalItemCtrl._AfterUseCheckRevive = HL.Method(HL.String, HL.Opt(HL.Boolean, HL.Number)) << function(
    self, itemId, inUseItemTransition, delayCloseTime)
    self:_StartCoroutine(function()
        self:_PlaySelectedCharHpRecoverFx()
        self:_RefreshItemNode(itemId)
        if delayCloseTime then
            Notify(MessageConst.BLOCK_LUA_UI_INPUT, {true, "TacticalItem"})
            coroutine.wait(CLOSE_WAIT_FX_DURATION)
            Notify(MessageConst.BLOCK_LUA_UI_INPUT, {false, "TacticalItem"})
        end

        self.m_selectCharInstIdDict = nil

        local storageCount = Utils.getBagItemCount(itemId)
        self:_RefreshTacticalPanel(itemId, inUseItemTransition)
        if storageCount <= 0 or #(self:_GetAllDeadMember()) <= 0 then
            self.view.anim:PlayOutAnimation(function()
                self:Close()
            end)
        end
    end)
end

TacticalItemCtrl._AfterUseCheckSingleHeal = HL.Method(HL.String, HL.Opt(HL.Boolean, HL.Number)) << function(
    self, itemId, inUseItemTransition, delayCloseTime)
    self:_PlaySelectedCharHpRecoverFx()


    local isSelectCharHpMax = false
    local useItemCfg = Tables.useItemTable:GetValue(itemId)
    local cfg = USE_ITEM_CFG[useItemCfg.uiType]
    local squadMembers = self[cfg.getMemberFunc](self)
    for i = 1, #squadMembers do
        local squadMember = squadMembers[i]
        if squadMember.slot then
            if squadMember.slot.character ~= nil and squadMember.slot.character.abilityCom.alive then
                local abilityCom = squadMember.slot.character.abilityCom
                if abilityCom.alive and self.m_selectCharInstIdDict and self.m_selectCharInstIdDict[squadMember.slot.charInstId] and
                    math.abs(abilityCom.hp - abilityCom.maxHp) < 0.001 then
                    isSelectCharHpMax = true
                    break
                end
            end
        end
    end
    if isSelectCharHpMax then
        local selectCharInstIdDict, isHpMax = self[cfg.selectDefaultFunc](self, squadMembers, useItemCfg)
        if not isHpMax then
            self.m_selectCharInstIdDict = selectCharInstIdDict
        end
    end
    self:_AfterUseCheckDefault(itemId, nil, delayCloseTime)
end

TacticalItemCtrl._PlaySelectedCharHpRecoverFx = HL.Method() << function(self)
    local selectedCharInstId = next(self.m_selectCharInstIdDict)
    if selectedCharInstId then
        local cell = self.m_getCharCell(self.view.scrollList:Get(CSIndex(self.m_selectCharInstIdDict[selectedCharInstId])))
        cell.charHeadCellLongHpBar.view.hpRecoverAnim:ClearTween(false)
        cell.charHeadCellLongHpBar.view.hpRecoverAnim:PlayInAnimation()
        cell.charHeadCellLongHpBar.view.disableMask.gameObject:SetActive(false)
    end
end

TacticalItemCtrl._PlaySelectedCharUspUpFx = HL.Method() << function(self)
    local selectedCharInstId = next(self.m_selectCharInstIdDict)
    if selectedCharInstId then
        local cell = self.m_getCharCell(self.view.scrollList:Get(CSIndex(self.m_selectCharInstIdDict[selectedCharInstId])))
        cell.charHeadCellLongHpBar.view.atbAnim:ClearTween(false)
        cell.charHeadCellLongHpBar.view.atbAnim:PlayInAnimation()
    end
end



TacticalItemCtrl.UseItem = HL.Method(HL.String, HL.Table) << function(self, itemId, selectCharInstIdDict)
    GameInstance.player.inventory:UseItem(Utils.getCurrentScope(), itemId)
end

TacticalItemCtrl.UseItemOnTarget = HL.Method(HL.String, HL.Table) << function(self, itemId, selectCharInstIdDict)
    local charInstId

    if selectCharInstIdDict == nil or next(selectCharInstIdDict) == nil then
        return
    end

    for instId, v in pairs(selectCharInstIdDict) do
        charInstId = instId
    end

    GameInstance.player.inventory:UseItemOnTarget(Utils.getCurrentScope(), itemId, charInstId)
end

TacticalItemCtrl._InitActionEvent = HL.Method() << function(self)
    self.view.cancelBtn.onClick:AddListener(function()
        self.view.anim:PlayOutAnimation(function()
            self:Close()
        end)
    end)
    self.view.emptyButton.onClick:AddListener(function()
        self.view.anim:PlayOutAnimation(function()
            self:Close()
        end)
    end)

    self.view.confirmBtn.onClick:AddListener(function()
        if Utils.isCurSquadAllDead() then

            Notify(MessageConst.SHOW_TOAST, Language.LUA_GAME_MODE_FORBID_FACTORY_WATCH)
            return
        end

        local useItemCfg = Tables.useItemTable:GetValue(self.m_curItemId)
        local cfg = USE_ITEM_CFG[useItemCfg.uiType]
        if cfg.onConfirm then
            self[cfg.onConfirm](self, self.m_curItemId, self.m_selectCharInstIdDict)
        end
    end)

    self.view.btnEquipItem.gameObject:SetActive(Utils.isSystemUnlocked(GEnums.UnlockSystemType.Equip) and
        Tables.equipItemTable:ContainsKey(self.m_curItemId) and not Utils.isInBlackbox())
    self.view.btnEquipItem.onClick:AddListener(function()
        UIManager:Open(PanelId.QuickEquipTacticalItem, {
            tacticalItemId = self.m_curItemId
        })
    end)

    self.m_getCharCell = UIUtils.genCachedCellFunction(self.view.scrollList)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    UIUtils.bindHyperlinkPopup(self, "TacticalItem", self.view.inputGroup.groupId)
end

TacticalItemCtrl._RefreshTacticalPanel = HL.Method(HL.String, HL.Opt(HL.Boolean)) << function(self, itemId, inUseItemTransition)
    local useItemCfg = Tables.useItemTable:GetValue(itemId)

    local cfg = USE_ITEM_CFG[useItemCfg.uiType]
    local squadMembers = self[cfg.getMemberFunc](self)
    self:_RefreshItemNode(itemId)
    self:_RefreshSquadNode(squadMembers, cfg, useItemCfg, inUseItemTransition)
end

TacticalItemCtrl._RefreshItemNode = HL.Method(HL.String) << function(self, itemId)
    local itemCfg = Tables.itemTable:GetValue(itemId)
    self.view.itemBlack:InitItem({
        id = itemId
    })
    self.view.storageCount.text = Utils.getBagItemCount(itemId)
    self.view.desc:SetAndResolveTextStyle(UIUtils.getItemUseDesc(itemId))
    self.view.name.text = itemCfg.name
end

TacticalItemCtrl._RefreshSquadNode = HL.Method(HL.Table, HL.Table, HL.Userdata, HL.Opt(HL.Boolean)) << function(self, squadMembers, cfg, useItemCfg, inUseItemTransition)
    local squadMemberCount = #squadMembers
    self.view.scrollList.gameObject:SetActive(squadMemberCount > 0)
    self.view.emptyNode.gameObject:SetActive(squadMemberCount <= 0)
    if squadMemberCount > 0 then
        if self.m_selectCharInstIdDict == nil then
            self.m_selectCharInstIdDict = self[cfg.selectDefaultFunc](self, squadMembers, useItemCfg)
        end

        self.view.scrollList.onUpdateCell:RemoveAllListeners()
        self.view.scrollList.onUpdateCell:AddListener(function(object, csIndex)
            local cell = self.m_getCharCell(object)
            local index = LuaIndex(csIndex)
            local memberInfo = squadMembers[index]
            self[cfg.refreshCellFunc](self, cell, memberInfo, useItemCfg, cfg, inUseItemTransition == true)

            cell.charHeadCellLongHpBar.view.button.onClick:RemoveAllListeners()
            cell.charHeadCellLongHpBar.view.button.onClick:AddListener(function()
                if cfg.onClick then
                    self[cfg.onClick](self, memberInfo, index, useItemCfg)
                end
            end)
        end)

        self.view.scrollList:UpdateCount(#squadMembers)
    end
end


TacticalItemCtrl._GetAllAliveMember = HL.Method().Return(HL.Table) << function(self)
    local singleHealSquadMembers = {}
    local squadSlots = GameInstance.player.squadManager.curSquad.slots

    for i = 1, Const.BATTLE_SQUAD_MAX_CHAR_NUM do
        if i <= squadSlots.Count then
            local slot = squadSlots[CSIndex(i)]
            table.insert(singleHealSquadMembers, {
                isEmpty = false,
                slot = slot,
            })
        else
            table.insert(singleHealSquadMembers, {
                isEmpty = true,
            })
        end
    end

    return singleHealSquadMembers
end

TacticalItemCtrl._GetAllDeadMember = HL.Method().Return(HL.Table) << function(self)
    local squadSlots = GameInstance.player.squadManager.curSquad.slots
    local deadMember = {}
    for i, slot in pairs(squadSlots) do
        local isAlive = slot.character ~= nil and slot.character.abilityCom.alive
        if not isAlive then
            table.insert(deadMember, {
                isEmpty = false,
                slot = slot,
            })
        end
    end

    return deadMember
end



TacticalItemCtrl._RefreshCharCellDefault = HL.Method(HL.Table, HL.Table, HL.Userdata, HL.Table, HL.Opt(HL.Boolean)) << function(
    self, cell, memberInfo, useItemCfg, cfg, inUseItemTransition)
    cell.stateController:SetState(memberInfo.isEmpty and 'empty' or 'normal')
    cell.charHeadCellLongHpBar.view.button.enabled = not memberInfo.isEmpty
    cell.charHeadCellLongHpBar.view.stateCtrl:SetState(cfg.stateName)
    if not memberInfo.isEmpty then
        local slot = memberInfo.slot
        self:_RefreshHeadCellBasic(cell.charHeadCellLongHpBar, slot, useItemCfg, cfg)


        if useItemCfg.effectType == GEnums.ItemUseEffectType.Buff then
            if inUseItemTransition and self.m_selectCharInstIdDict[slot.charInstId] ~= nil then
                cell.charHeadCellLongHpBar.view.buffAnim:PlayInAnimation()

                local cellCache = cell.charHeadCellLongHpBar.view.buffNode.selectedBuffCellCache
                if cellCache then
                    for i = 1, cellCache:GetCount() do
                        local buffCell = cellCache:Get(i)
                        buffCell.animationWrapper:PlayInAnimation()
                    end
                end
            end
        end

    end
end

TacticalItemCtrl._RefreshCharCellWithBuff = HL.Method(HL.Table, HL.Table, HL.Userdata, HL.Table, HL.Opt(HL.Boolean)) << function(
    self, cell, memberInfo, useItemCfg, cfg, inUseItemTransition)
    self:_RefreshCharCellDefault(cell, memberInfo, useItemCfg, cfg, inUseItemTransition)
    local buffNodeView = cell.charHeadCellLongHpBar.view.buffNode
    if memberInfo.isEmpty then
        buffNodeView.stateController:SetState('empty')
        return
    end
    local slot = memberInfo.slot
    local isSelected = self.m_selectCharInstIdDict[slot.charInstId] ~= nil
    buffNodeView.stateController:SetState(isSelected and 'selected' or 'normal')

    local buffDetailList = TacticalItemUtil.GetInUseItemBuffDetail(slot.charInstId)
    if not buffNodeView.normalBuffCellCache then
        buffNodeView.normalBuffCellCache = UIUtils.genCellCache(buffNodeView.normalBuffCell)
    end
    buffNodeView.normalBuffCellCache:Refresh(buffDetailList.Count, function(cell, index)
        local buffDetail = buffDetailList[CSIndex(index)]
        cell.imgIcon:LoadSpriteWithOutFormat(buffDetail.buffIconPath)
        cell.imgFill.fillAmount = buffDetail.buffLifetimeProcess
    end)
    buffNodeView.bgImage.gameObject:SetActive(buffDetailList.Count > 0)

    if isSelected then
        if not buffNodeView.selectedBuffCellCache then
            buffNodeView.selectedBuffCellCache = UIUtils.genCellCache(buffNodeView.selectedBuffCell)
        end
        local buffIconList = TacticalItemUtil.GetItemBuffIconPath(self.m_curItemId)
        buffNodeView.selectedBuffCellCache:Refresh(buffIconList.Count, function(cell, index)
            cell.imgIcon:LoadSpriteWithOutFormat(buffIconList[CSIndex(index)])
        end)
    end
end

TacticalItemCtrl._RefreshCharCellWithUsp = HL.Method(HL.Table, HL.Table, HL.Userdata, HL.Table, HL.Opt(HL.Boolean)) << function(
    self, cell, memberInfo, useItemCfg, cfg, inUseItemTransition)
    self:_RefreshCharCellDefault(cell, memberInfo, useItemCfg, cfg, inUseItemTransition)
    local skillNodeView = cell.charHeadCellLongHpBar.view.skillNode
    if memberInfo.isEmpty then
        skillNodeView.stateController:SetState('empty')
        return
    end
    local slot = memberInfo.slot
    local isSelected = self.m_selectCharInstIdDict[slot.charInstId] ~= nil
    local isAlive = slot.character ~= nil and slot.character.abilityCom.alive
    skillNodeView.stateController:SetState(isAlive and (isSelected and 'selected' or 'normal') or 'dead')
    local skillGroupData = CharInfoUtils.getCharSkillGroupCfgByType(slot.charId, GEnums.SkillGroupType.UltimateSkill)

    skillNodeView.skillIcon:LoadSprite(UIConst.UI_SPRITE_SKILL_ICON, skillGroupData.icon)
    local skillColor = CharInfoUtils.getCharInfoSkillGroupBgColor(skillGroupData, true)
    skillNodeView.hpImage.color = skillColor
    skillNodeView.bgSkillColor2.color = skillColor
    if not isAlive then
        return
    end

    local abilityCom = slot.character.abilityCom
    local _, skill = abilityCom.activeSkillMap:TryGetValue(abilityCom.curUltimateSkill)
    local skillData = skill.data
    local usp = abilityCom.ultimateSp
    local maxUsp = skillData.castData.costData.costValue
    local uspRate = usp / maxUsp
    skillNodeView.hpImage.fillAmount = uspRate
    if isSelected then
        local addUsp = TacticalItemUtil.GetItemUspValue(useItemCfg.itemId, abilityCom)
        if addUsp + usp > maxUsp then
            addUsp = maxUsp - usp
        end
        skillNodeView.addHpImage.fillAmount = addUsp / maxUsp
        skillNodeView.addHpImage.transform.localRotation = Quaternion.Euler(0, 0, -360 * uspRate)
    end
    if inUseItemTransition then
        cell.charHeadCellLongHpBar.view.atbAnim:PlayInAnimation()
    end
end

TacticalItemCtrl._RefreshCharCellWithHp = HL.Method(HL.Table, HL.Table, HL.Userdata, HL.Table, HL.Opt(HL.Boolean)) << function(
    self, cell, memberInfo, useItemCfg, cfg, inUseItemTransition)
    self:_RefreshCharCellDefault(cell, memberInfo, useItemCfg, cfg, inUseItemTransition)
    if memberInfo.isEmpty then
        cell.charHeadCellLongHpBar.view.hpStateCtrl:SetState('empty')
    else
        cell.charHeadCellLongHpBar.view.hpStateCtrl:SetState('normal')
        local slot = memberInfo.slot
        self:_RefreshHeadCellWithHp(cell.charHeadCellLongHpBar, slot, useItemCfg, inUseItemTransition)
    end
end

TacticalItemCtrl._RefreshHeadCellBasic = HL.Method(HL.Userdata, HL.Userdata, HL.Userdata, HL.Table) << function(
    self, cell, slot, useItemCfg, cfg)
    local charInstId = slot.charInstId
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local charCfg = Tables.characterTable:GetValue(charInst.templateId)

    cell:InitCharFormationHeadCell({
        instId = charInst.instId,
        level = charInst.level,
        ownTime = charInst.ownTime,
        rarity = charCfg.rarity,
        templateId = charInst.templateId,
    })

    cell.view.stateCtrl:SetState(cfg.stateName)

    local isSelected = self.m_selectCharInstIdDict[slot.charInstId] ~= nil
    cell.view.selectedBG.gameObject:SetActive(isSelected)
    if DeviceInfo.usingController then
        if cfg.onClick and isSelected then
            cell.view.selectedBG.gameObject:SetActive(false)
            self:SetNaviTarget(cell.view.button)
        end
    end

    local isAlive = slot.character ~= nil and slot.character.abilityCom.alive
    cell.view.disableMask.gameObject:SetActive(not isAlive)
end

TacticalItemCtrl._RefreshHeadCellWithHp = HL.Method(HL.Userdata, HL.Userdata, HL.Userdata, HL.Opt(HL.Boolean)) << function(self, cell, slot, useItemCfg, inUseItemTransition)
    local isAlive = slot.character ~= nil and slot.character.abilityCom.alive
    if not isAlive then
        cell.view.curHpFill.fillAmount = 0
        cell.view.addHpFill.gameObject:SetActive(false)
        cell.view.totalAddHpFill.gameObject:SetActive(false)
        return
    end
    local abilityCom = slot.character.abilityCom

    local isSelected = self.m_selectCharInstIdDict[slot.charInstId] ~= nil
    local currentHpPct = abilityCom.hp / abilityCom.maxHp
    if isSelected and inUseItemTransition and currentHpPct - cell.view.curHpFill.fillAmount > 0.01 then
        cell.view.hpRecoverAnim:PlayInAnimation()
    end
    cell.view.curHpFill.fillAmount = currentHpPct
    cell.view.disableMask.gameObject:SetActive(not abilityCom.alive)

    local showAddHp = isSelected and abilityCom.hp < abilityCom.maxHp
    cell.view.addHpFill.gameObject:SetActive(showAddHp)
    cell.view.totalAddHpFill.gameObject:SetActive(showAddHp)
    if showAddHp then

        local value = TacticalItemUtil.GetItemHealValue(useItemCfg.itemId, abilityCom) * (1 + abilityCom.healTakenIncrease)
        local addHpChildRect = cell.view.addHpFill.transform:GetChild(0)
        local addHpRect = cell.view.addHpFill.transform
        addHpChildRect.offsetMin = Vector2(addHpRect.rect.width * currentHpPct, addHpChildRect.offsetMin.y)
        addHpChildRect.offsetMax = Vector2(addHpRect.rect.width * math.min(1, (abilityCom.hp + value) / abilityCom.maxHp), addHpChildRect.offsetMax.y)

        local totalValue = TacticalItemUtil.GetItemTotalHealValue(useItemCfg.itemId, abilityCom) * (1 + abilityCom.healTakenIncrease)
        cell.view.totalAddHpFill.fillAmount = (totalValue + abilityCom.hp) / abilityCom.maxHp
    end
end



TacticalItemCtrl._OnClickSingleSelect = HL.Method(HL.Table, HL.Number, HL.Any) << function(self, memberInfo, index, useItemCfg)
    if useItemCfg.targetNumType == GEnums.ItemUseTargetNumType.All then
        return
    end

    local slot = memberInfo.slot
    local charInstId = slot.charInstId

    self.m_selectCharInstIdDict = {
        [charInstId] = index
    }

    self:_RefreshTacticalPanel(self.m_curItemId)
end



TacticalItemCtrl._SelectLowestUspRate = HL.Method(HL.Table, HL.Opt(HL.Any)).Return(HL.Table, HL.Boolean) << function(self, squadMembers)
    local defaultSelectInstId = -1
    local defaultIndex = -1
    local minUspRate = 100
    for i = 1, #squadMembers do
        local squadMember = squadMembers[i]
        if squadMember.slot then
            if squadMember.slot.character ~= nil and squadMember.slot.character.abilityCom.alive then
                local abilityCom = squadMember.slot.character.abilityCom
                local usp = math.floor(abilityCom.ultimateSp)
                local _, skill = abilityCom.activeSkillMap:TryGetValue(abilityCom.curUltimateSkill)
                local maxUsp = math.floor(skill.data.castData.costData.costValue)
                if defaultSelectInstId < 0 then
                    defaultSelectInstId = squadMember.slot.charInstId
                    defaultIndex = i
                    minUspRate = usp / maxUsp
                else
                    local uspRate = usp / maxUsp
                    if uspRate < minUspRate then
                        defaultSelectInstId = squadMember.slot.charInstId
                        defaultIndex = i
                        minUspRate = uspRate
                    end
                end
            end
        end
    end

    return {
        [defaultSelectInstId] = defaultIndex
    }, minUspRate >= 1
end

TacticalItemCtrl._SelectLowestHpRate = HL.Method(HL.Table, HL.Opt(HL.Any)).Return(HL.Table, HL.Boolean) << function(self, squadMembers)

    local defaultSelectInstId = -1
    local defaultIndex = -1
    local minHpRate = 100
    for i = 1, #squadMembers do
        local squadMember = squadMembers[i]
        if squadMember.slot then
            if squadMember.slot.character ~= nil and squadMember.slot.character.abilityCom.alive then
                local abilityCom = squadMember.slot.character.abilityCom
                if abilityCom.alive then
                    local hp = math.floor(abilityCom.hp)
                    local maxHp = math.floor(abilityCom.maxHp)
                    if defaultSelectInstId < 0 then
                        defaultSelectInstId = squadMember.slot.charInstId
                        defaultIndex = i
                        minHpRate = hp / maxHp
                    else
                        local hpRate = hp / maxHp
                        if hpRate < minHpRate then
                            defaultSelectInstId = squadMember.slot.charInstId
                            defaultIndex = i
                            minHpRate = hpRate
                        end
                    end
                end
            end
        end
    end

    return {
        [defaultSelectInstId] = defaultIndex
    }, minHpRate >= 1
end
TacticalItemCtrl._SelectAll = HL.Method(HL.Table, HL.Opt(HL.Any)).Return(HL.Table) << function(self, squadMembers)

    local selectDict = {}
    for i = 1, #squadMembers do
        local squadMember = squadMembers[i]
        if squadMember.slot then
            if squadMember.slot.character ~= nil then
                local abilityCom = squadMember.slot.character.abilityCom
                if abilityCom.alive then
                    selectDict[squadMember.slot.charInstId] = i
                end
            end
        end
    end

    return selectDict
end

TacticalItemCtrl._SelectFirstOne = HL.Method(HL.Table, HL.Opt(HL.Any)).Return(HL.Table) << function(self, squadMembers)

    for i = 1, #squadMembers do
        local squadMember = squadMembers[i]
        if squadMember.slot then
            return {
                [squadMember.slot.charInstId] = i
            }
        end
    end
end

TacticalItemCtrl._SelectFirstOneAlive = HL.Method(HL.Table, HL.Opt(HL.Any)).Return(HL.Table) << function(self, squadMembers)
    for i = 1, #squadMembers do
        local squadMember = squadMembers[i]
        if squadMember.slot then
            if squadMember.slot.character ~= nil and squadMember.slot.character.abilityCom.alive then
                return {
                    [squadMember.slot.charInstId] = i
                }
            end
        end
    end
end

TacticalItemCtrl._SelectAliveDependOnTargetNumType = HL.Method(HL.Table, HL.Opt(HL.Any)).Return(HL.Table) << function(self, squadMembers, useItemCfg)
    if useItemCfg.targetNumType == GEnums.ItemUseTargetNumType.Single then
        return self:_SelectFirstOneAlive(squadMembers)
    end

    return self:_SelectAll(squadMembers)
end




HL.Commit(TacticalItemCtrl)
