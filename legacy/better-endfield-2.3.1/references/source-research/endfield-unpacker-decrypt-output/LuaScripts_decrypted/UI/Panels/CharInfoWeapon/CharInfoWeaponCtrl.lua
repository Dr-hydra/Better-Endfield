local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharInfoWeapon
CharInfoWeaponCtrl = HL.Class('CharInfoWeaponCtrl', uiCtrl.UICtrl)







CharInfoWeaponCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CHAR_INFO_SELECT_CHAR_CHANGE] = 'OnSelectCharChange',
    [MessageConst.ON_PUT_ON_WEAPON] = 'OnPutOnWeapon',
    [MessageConst.GUIDE_CHAR_INFO_WEAPON_SCROLL_TO_TOP] = 'OnGuideScrollToTop',
    [MessageConst.CHAR_INFO_WEAPON_SELECT_WEAPON] = 'OnNotifySelectWeapon',
}

CharInfoWeaponCtrl.m_arg = HL.Field(HL.Table)

CharInfoWeaponCtrl.m_charInfo = HL.Field(HL.Table)

CharInfoWeaponCtrl.m_curSelectInstId = HL.Field(HL.Int) << 0

CharInfoWeaponCtrl.state = HL.Field(HL.Number) << UIConst.CHAR_INFO_WEAPON_STATE.Normal

CharInfoWeaponCtrl.m_inCompare = HL.Field(HL.Boolean) << false


CharInfoWeaponCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg
    self:_InitActionEvent()

    self.view.bgMask.gameObject:SetActive(false)
    self.view.backButton.gameObject:SetActive(false)
    self.view.commonItemList.gameObject:SetActive(false)
    self.view.weaponInfoLeft.gameObject:SetActive(false)
    self.view.compareButton.gameObject:SetActive(false)
    self.view.shrinkButton.gameObject:SetActive(false)
    self.view.btnFullSkill.gameObject:SetActive(false)

    local initCharInfo = arg.initCharInfo or CharInfoUtils.getLeaderCharInfo()
    local phase = arg.phase

    self.m_charInfo = initCharInfo

    local curWeaponInstId = CharInfoUtils.getCharCurWeapon(self.m_charInfo.instId).weaponInstId
    self.m_curSelectInstId = curWeaponInstId

    self:_InitController()
    if DeviceInfo.usingController then
        self.m_phase:_ActiveWeaponPageNavi(self, true)
    end
    self:_ProcessStateArg(self.m_arg.stateArg)
    self.m_arg.stateArg = nil
end

CharInfoWeaponCtrl.OnClose = HL.Override() << function(self)
    if DeviceInfo.usingController then
        self.m_phase:_ActiveWeaponPageNavi(self, false)
    end
end

CharInfoWeaponCtrl._InitActionEvent = HL.Method() << function(self)
    self.view.backButton.onClick:RemoveAllListeners()
    self.view.backButton.onClick:AddListener(function()
        if self.state == UIConst.CHAR_INFO_WEAPON_STATE.Detail then
            local curWeaponInstId = CharInfoUtils.getCharCurWeapon(self.m_charInfo.instId).weaponInstId
            self.m_curSelectInstId = curWeaponInstId

            self:OnCommonEmptyButtonClick()
            self:SwitchState(UIConst.CHAR_INFO_WEAPON_STATE.Normal)
        end
    end)

    self.view.compareButton.onClick:RemoveAllListeners()
    self.view.compareButton.onClick:AddListener(function()
        self:_SwitchCompare(true)
    end)

    self.view.shrinkButton.onClick:RemoveAllListeners()
    self.view.shrinkButton.onClick:AddListener(function()
        self:_SwitchCompare(false)
    end)

    self.view.commonEmptyButton.gameObject:SetActive(false)
    self.view.commonEmptyButton.onClick:RemoveAllListeners()
    self.view.commonEmptyButton.onClick:AddListener(function()

        self:OnCommonEmptyButtonClick()
    end)

    self.view.btnFullSkill.onClick:AddListener(function()
        UIManager:Open(PanelId.WeaponSkillDetail, {
            weaponInstId = self.m_curSelectInstId,
        })
    end)

    self.view.weaponInfoRight.view.tagBtn.onClick:RemoveAllListeners()
    self.view.weaponInfoRight.view.tagBtn.onClick:AddListener(function()
        UIManager:Open(PanelId.RecommendedTips, {
            charId = self.m_charInfo.templateId,
            charInstId = self.m_charInfo.instId,
        })

        EventLogManagerInst:GameEvent_WeaponRecView(
            self.m_charInfo.templateId
        )
    end)
end

CharInfoWeaponCtrl._SwitchCompare = HL.Method(HL.Opt(HL.Boolean)) << function(self, compare)
    if self.state == UIConst.CHAR_INFO_WEAPON_STATE.Detail then
        self.m_inCompare = compare

        self:_RefreshWeaponInfo()
        self:_RefreshCompareButton()
        self:_RefreshRecommendButton()
        self.view.commonEmptyButton.gameObject:SetActive(compare)
        if DeviceInfo.usingController then
            self.view.scrollKeyHint.gameObject:SetActive(not compare)
        end

        Notify(MessageConst.REFRESH_CONTROLLER_HINT)
    end
end

CharInfoWeaponCtrl.OnSelectCharChange = HL.Method(HL.Table) << function(self, charInfo)
    self.m_charInfo = charInfo
    local curWeaponInstId = CharInfoUtils.getCharCurWeapon(self.m_charInfo.instId).weaponInstId
    self.m_curSelectInstId = curWeaponInstId
    self:_RefreshWeaponInfo()
end

CharInfoWeaponCtrl.SwitchState = HL.Method(HL.Number) << function(self, state)
    if self.state == state then
        return
    end
    self.state = state

    local inDetail = self.state == UIConst.CHAR_INFO_WEAPON_STATE.Detail
    self:_RefreshState()
    self:_ToggleWeaponItemList(inDetail)
    self:_RefreshWeaponInfo()
    self.view.focusMasteryHint.gameObject:SetActive(inDetail)
    InputManagerInst:ToggleBinding(self.m_focusMasteryBindingId, inDetail)
    self.view.focusMasteryNaviGroup.removeLayerOnDisable = inDetail
end

CharInfoWeaponCtrl._RefreshState = HL.Method() << function(self)
    local inDetail = self.state == UIConst.CHAR_INFO_WEAPON_STATE.Detail
    self.view.backButton.gameObject:SetActive(inDetail)
    self.view.btnFullSkill.gameObject:SetActive(inDetail)

    if not inDetail then
        UIUtils.PlayAnimationAndToggleActive(self.view.bgMask, false)
        UIUtils.PlayAnimationAndToggleActive(self.view.weaponInfoLeft.view.animationWrapper, false)

        local curWeaponInstId = CharInfoUtils.getCharCurWeapon(self.m_charInfo.instId).weaponInstId
        self:Notify(MessageConst.CHAR_INFO_PREVIEW_WEAPON, curWeaponInstId)
    end
    self:Notify(MessageConst.TOGGLE_CHAR_INFO_FOCUS_MODE, inDetail)

    self:_RefreshCompareButton()
    self:_RefreshRecommendButton()
end

CharInfoWeaponCtrl._RefreshCompareButton = HL.Method() << function(self)
    local inDetail = self.state == UIConst.CHAR_INFO_WEAPON_STATE.Detail
    local curWeaponInstId = CharInfoUtils.getCharCurWeapon(self.m_charInfo.instId).weaponInstId
    local canCompare = inDetail and self.m_curSelectInstId > 0 and self.m_curSelectInstId ~= curWeaponInstId
    local inCompare = self.m_inCompare
    self.view.compareButton.gameObject:SetActive(canCompare and not inCompare)
    self.view.shrinkButton.gameObject:SetActive(canCompare and inCompare)
    self:_SetFocusMasteryNaviGroupActive(not inCompare or not canCompare)
end

CharInfoWeaponCtrl._RefreshRecommendButton = HL.Method() << function(self)
    local inDetail = self.state == UIConst.CHAR_INFO_WEAPON_STATE.Detail
    local inCompare = self.m_inCompare
    self.view.weaponInfoRight.view.recommendTagLayout.gameObject:SetActive(inDetail and not inCompare)
end

CharInfoWeaponCtrl._ToggleWeaponItemList = HL.Method(HL.Boolean) << function(self, inDetail)
    self:Notify(MessageConst.TOGGLE_CHAR_INFO_FOCUS_MODE, inDetail)

    UIUtils.PlayAnimationAndToggleActive(self.view.commonItemList.view.animationWrapper, inDetail)

    if inDetail then
        local curWeaponInstId = CharInfoUtils.getCharCurWeapon(self.m_charInfo.instId).weaponInstId
        self:_RefreshWeaponList(curWeaponInstId)
        self:Notify(MessageConst.CHAR_INFO_WEAPON_SECOND_OPEN)
    else
        InputManagerInst.controllerNaviManager:TryRemoveLayer(self.view.commonItemList.view.scrollRect.naviGroup)
        self:Notify(MessageConst.CHAR_INFO_WEAPON_SECOND_CLOSE)
    end
end

CharInfoWeaponCtrl.OnShow = HL.Override() << function(self)
    local curEquipWeaponInstId = CharInfoUtils.getCharCurWeapon(self.m_charInfo.instId).weaponInstId
    local curWeaponInstId = self.m_phase.m_curPreviewWeaponInstId
    if curWeaponInstId == nil or curWeaponInstId <= 0 then
        curWeaponInstId = curEquipWeaponInstId
    end

    self.m_curSelectInstId = curWeaponInstId

    if self.view.commonItemList.view.gameObject.activeSelf then
        self:_RefreshWeaponList(curWeaponInstId)
    end

    self:_RefreshWeaponInfo()
end

CharInfoWeaponCtrl.OnHide = HL.Override() << function(self)
    self.m_curSelectInstId = 0
end

CharInfoWeaponCtrl._RefreshWeaponList = HL.Method(HL.Opt(HL.Any)) << function(self, selectedIndexId)
    local charTable = CharInfoUtils.getCharTableData(self.m_charInfo.templateId)
    local weaponType = charTable.weaponType

    local curSortCSIndex, curSortIsIncremental, selectedTags, skipGraduallyShow
    if self.m_arg.stateArg then
        curSortCSIndex = self.m_arg.stateArg.curSortCSIndex
        curSortIsIncremental = self.m_arg.stateArg.curSortIsIncremental
        selectedTags = self.m_arg.stateArg.selectedTags
        skipGraduallyShow = true
        selectedIndexId = nil
    end

    self.view.commonItemList:InitCommonItemList({
        listType = UIConst.COMMON_ITEM_LIST_TYPE.CHAR_INFO_WEAPON,
        defaultSelectedIndex = 1,
        selectedIndexId = selectedIndexId,
        refreshItemAddOn = function(cell, itemInfo)
            WeaponUtils.refreshListCellWeaponAddOn(cell, itemInfo)
        end,
        onClickItem = function(args)
            local itemInfo = args.itemInfo
            local curCell = args.curCell
            local nextCell = args.nextCell
            if itemInfo.instId == self.m_curSelectInstId then
                return
            end

            self:_OnWeaponClick(itemInfo)
        end,
        filter_weaponType = weaponType,
        enableKeyboardNavi = true,
        afterSetItemSelected = function(cell, selected)
            if self.m_arg.stateArg and self.m_arg.stateArg.isTagBtnClicked then
                return
            end
            Notify(MessageConst.CHAR_INFO_WEAPON_LIST_SELECT_ITEM, cell)
        end,
        curSortCSIndex = curSortCSIndex,
        curSortIsIncremental = curSortIsIncremental,
        selectedTags = selectedTags,
        skipGraduallyShow = skipGraduallyShow,
    })
end

CharInfoWeaponCtrl._GetCurSelectedCellHint = HL.Method(HL.Table).Return(HL.String) << function(self, itemInfo)
    local weaponInstId = itemInfo.instId
    local curEquipWeaponInstId = CharInfoUtils.getCharCurWeapon(self.m_charInfo.instId).weaponInstId

    if self.m_curSelectInstId == weaponInstId and weaponInstId ~= curEquipWeaponInstId then
        return "ui_weapon_info_change_weapon"
    end

    if self.m_curSelectInstId == weaponInstId then
        return ""
    end

    return ""
end

CharInfoWeaponCtrl._RefreshWeaponInfo = HL.Method() << function(self)
    local curWeaponInfo = CharInfoUtils.getCharCurWeapon(self.m_charInfo.instId)
    local curWeaponInstId = curWeaponInfo.weaponInstId
    local curSelectWeaponInfo = CharInfoUtils.getWeaponInstInfo(self.m_curSelectInstId)

    local rightWeaponInfo = curSelectWeaponInfo
    local leftWeaponInfo = nil
    local isShowCompare = self.m_inCompare and self.m_curSelectInstId > 0 and self.m_curSelectInstId ~= curWeaponInstId
    local isRightWeaponChange = self.view.weaponInfoRight.m_weaponInfo ~= nil and self.view.weaponInfoRight.m_weaponInfo.weaponInst.instId ~= curSelectWeaponInfo.weaponInst.instId
    if isShowCompare then
        leftWeaponInfo = curWeaponInfo
    end

    rightWeaponInfo.expandCallback = function()
        self:_OnNormalEquipClick()
    end
    rightWeaponInfo.equipCallback = function(weaponInfo)
        self:_OnEquipClick(weaponInfo)
    end

    local isSelectingEquipped = self.m_curSelectInstId > 0 and self.m_curSelectInstId == curWeaponInstId
    local rightWeaponExpInfo = CharInfoUtils.getWeaponExpInfo(curSelectWeaponInfo.weaponInst.instId)
    rightWeaponInfo.canEditGem = true
    rightWeaponInfo.canUpgrade = true
    rightWeaponInfo.isWeaponMaxLevel = true
    rightWeaponInfo.canUpgradePotential = true
    rightWeaponInfo.showExpand = self.state == UIConst.CHAR_INFO_WEAPON_STATE.Normal
    rightWeaponInfo.showEquip = self.state ~= UIConst.CHAR_INFO_WEAPON_STATE.Normal and not isSelectingEquipped
    rightWeaponInfo.showEquipped = self.state ~= UIConst.CHAR_INFO_WEAPON_STATE.Normal and isSelectingEquipped
    rightWeaponInfo.showEnhance = true
    rightWeaponInfo.isInDetail = self.state == UIConst.CHAR_INFO_WEAPON_STATE.Detail
    rightWeaponInfo.charId = self.m_charInfo.templateId


    if leftWeaponInfo ~= nil then
        leftWeaponInfo.canEditGem = false
        leftWeaponInfo.canUpgrade = false
        leftWeaponInfo.canUpgradePotential = false
        leftWeaponInfo.showEquip = false
        leftWeaponInfo.showEquipped = true
        leftWeaponInfo.showEnhance = false
        leftWeaponInfo.charId = self.m_charInfo.templateId
        self.view.weaponInfoLeft:InitInCharInfo(leftWeaponInfo)
    end
    self.view.weaponInfoRight:InitInCharInfo(rightWeaponInfo)

    UIUtils.PlayAnimationAndToggleActive(self.view.bgMask, leftWeaponInfo ~= nil)

    UIUtils.PlayAnimationAndToggleActive(self.view.weaponInfoLeft.view.animationWrapper, leftWeaponInfo ~= nil)
    if isRightWeaponChange then
        self.view.weaponInfoRight.view.animationWrapper:PlayInAnimation()
    end
end

CharInfoWeaponCtrl._OnEquipClick = HL.Method(HL.Table) << function(self, weaponInfo)
    local weaponInstId = self.m_curSelectInstId
    local isInFight = Utils.isInFight()
    if isInFight then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_IN_FIGHT_FORBID_INTERACT_TOAST)
        return
    end

    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(self.m_charInfo.instId)
    local isTrail = charInst.charType == GEnums.CharType.Trial
    if isTrail then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_WEAPON_TRAIL_FORBID)
        return
    end

    local tryWeapon = CharInfoUtils.getWeaponByInstId(weaponInstId)
    if tryWeapon.equippedCharServerId > 0 then

        if tryWeapon.equippedCharServerId == self.m_charInfo.instId then
            return
        end

        local charInst = CharInfoUtils.getPlayerCharInfoByInstId(tryWeapon.equippedCharServerId)
        local charCfg = Tables.characterTable[charInst.templateId]
        Notify(MessageConst.SHOW_POP_UP, {
            content = string.format(Language.LUA_CHAR_INFO_WEAPON_EQUIPPED_POP_UP_FORMAT, charCfg.name),
            onConfirm = function()
                GameInstance.player.charBag:PutOnWeapon(self.m_charInfo.instId, weaponInstId)
            end,
        })
        return
    end



    GameInstance.player.charBag:PutOnWeapon(self.m_charInfo.instId, weaponInstId)
end

CharInfoWeaponCtrl._OnNormalEquipClick = HL.Method() << function(self)
    local isInFight = Utils.isInFight()
    if isInFight then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_IN_FIGHT_FORBID_INTERACT_TOAST)
        return
    end

    local isTrailCard = not CharInfoUtils.isCharDevAvailable(self.m_charInfo.instId)
    if isTrailCard then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_WEAPON_TRAIL_FORBID)
        return
    end

    self:SwitchState(UIConst.CHAR_INFO_WEAPON_STATE.Detail)
end

CharInfoWeaponCtrl.OnCommonEmptyButtonClick = HL.Method(HL.Opt(HL.Userdata)) << function(self)
    self:_SwitchCompare(false)
end

CharInfoWeaponCtrl.OnPutOnWeapon = HL.Method(HL.Table) << function(self, arg)
    self:_RefreshState()
    self:_RefreshWeaponInfo()
    self.view.commonItemList:RefreshAllCells()
    Utils.triggerVoice("chrbark_weap", self.m_charInfo.templateId)
end

CharInfoWeaponCtrl._OnWeaponClick = HL.Method(HL.Table) << function(self, itemInfo)
    local weaponInstId = itemInfo.instId
    if self.m_curSelectInstId == weaponInstId then
        return
    end

    self.m_curSelectInstId = weaponInstId
    self:_RefreshState()
    self:_RefreshWeaponInfo()

    self:Notify(MessageConst.CHAR_INFO_PREVIEW_WEAPON, weaponInstId)
end

CharInfoWeaponCtrl.OnNotifySelectWeapon = HL.Method(HL.Any) << function(self, arg)
    if not arg.instId and not arg.id then
        logger.error("MessageConst.CHAR_INFO_WEAPON_SELECT_WEAPON 参数不对")
        return
    end
    local targetInstId = arg.instId
    local targetId = arg.id
    local currSelectedIndex = self.view.commonItemList:GetCurSelectIndex()
    local count = self.view.commonItemList:GetFilteredItemDepotCount()
    for i = 1, count do
        local itemInfo = self.view.commonItemList:GetItemInfoByIndex(i)
        if targetId ~= nil then
            if itemInfo.id == targetId and i ~= currSelectedIndex then
                self.view.commonItemList:SetSelectedIndex(i, false)
                if arg.audioEventName then
                    AudioAdapter.PostEvent(arg.audioEventName)
                end
                return
            end
        elseif targetInstId ~= nil then
            if itemInfo.instId == targetInstId and i ~= currSelectedIndex then
                self.view.commonItemList:SetSelectedIndex(i, false)
                if arg.audioEventName then
                    AudioAdapter.PostEvent(arg.audioEventName)
                end
                return
            end
        end
    end
end

CharInfoWeaponCtrl.OnGuideScrollToTop = HL.Method(HL.Table) << function(self, arg)
    local itemId = unpack(arg)
    local filterInfoList = self.view.commonItemList.m_filteredInfoList
    if filterInfoList then
        for index, itemInfo in ipairs(filterInfoList) do
            if itemInfo.id == itemId and itemInfo.itemInst.equippedCharServerId == 0 then
                self.view.commonItemList.view.itemList:ScrollToIndex(CSIndex(index), true)
                local obj = self.view.commonItemList.view.itemList:Get(CSIndex(index))
                if obj then
                    obj.name = itemId
                end
                return
            end
        end
    end
end



CharInfoWeaponCtrl.m_focusMasteryBindingId = HL.Field(HL.Number) << -1

CharInfoWeaponCtrl._InitController = HL.Method() << function(self)
    local extraBtnInfos = {}
    self.view.focusMasteryNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        self.view.focusMasteryHint.gameObject:SetActive(not isFocused and self.state == UIConst.CHAR_INFO_WEAPON_STATE.Detail)
        local showEquip  = self.view.weaponInfoRight.m_weaponInfo.showEquip
        self.view.weaponInfoRight.view.buttonNode.controllerConfirmBtn.gameObject:SetActive(showEquip == true and not isFocused)
    end)
    self.view.focusMasteryHint.gameObject:SetActive(false)
    self.m_focusMasteryBindingId = self:BindInputPlayerAction("char_info_weapon_focus_mastery", function()
        if self.state == UIConst.CHAR_INFO_WEAPON_STATE.Detail then
            self.view.commonItemList.view.itemList:SkipGraduallyShow()
            self.view.focusMasteryNaviGroup:ManuallyFocus()
        end
    end)
    InputManagerInst:ToggleBinding(self.m_focusMasteryBindingId, false)
    self.view.focusMasteryNaviGroup.removeLayerOnDisable = false
    local charInfoPanelPhaseItem = self.m_phase:_GetPanelPhaseItem(PanelId.CharInfo)
    if charInfoPanelPhaseItem then
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder(
            {self.view.inputGroup.groupId, charInfoPanelPhaseItem.uiCtrl.view.inputGroup.groupId})
    else
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
    end
    self.view.controllerSideMenuBtn:InitControllerSideMenuBtn({
        extraBtnInfos = extraBtnInfos,
    })
    UIUtils.bindHyperlinkPopup(self, "WeaponSkill", self.view.inputGroup.groupId)
end

CharInfoWeaponCtrl._SetFocusMasteryNaviGroupActive = HL.Method(HL.Boolean) << function(self, enabled)
    self.view.focusMasteryNaviGroup.enabled = enabled
    self.view.focusMasteryHint.gameObject:SetActive(enabled)
    self.view.focusMasteryNaviGroup.removeLayerOnDisable = enabled
    InputManagerInst:ToggleBinding(self.m_focusMasteryBindingId, enabled)
end

CharInfoWeaponCtrl.GetCurStateArg = HL.Method().Return(HL.Table) << function(self)
    local arg = {
        inCompare = self.m_inCompare,
        isDetail = self.state == UIConst.CHAR_INFO_WEAPON_STATE.Detail,
        curSortCSIndex = self.view.commonItemList.view.sortNode:GetCurSelectedIndex() - 1,
        curSortIsIncremental = self.view.commonItemList.view.sortNode.isIncremental,
        selectedTags = self.view.commonItemList.m_selectedTags,
        curSelectedIndex = self.view.commonItemList:IsAnyItemSelecting() and self.view.commonItemList:GetCurSelectIndex() or nil,
        isBtnFullSkillClicked = UIManager:IsShow(PanelId.WeaponSkillDetail),
        isTagBtnClicked = UIManager:IsShow(PanelId.RecommendedTips),
    }
    return arg
end

CharInfoWeaponCtrl._ProcessStateArg = HL.Method(HL.Table) << function(self, arg)
    if arg == nil or not arg.isDetail then
        return
    end
    self:SwitchState(UIConst.CHAR_INFO_WEAPON_STATE.Detail)
    self.view.commonItemList:SetSelectedIndex(arg.curSelectedIndex, false)
    if arg.inCompare then
        self:_SwitchCompare(true)
    end
    if arg.isBtnFullSkillClicked then
        self.view.btnFullSkill.onClick:Invoke()
    end
    if arg.isTagBtnClicked then
        self.view.weaponInfoRight.view.tagBtn.onClick:Invoke()
    end
end



HL.Commit(CharInfoWeaponCtrl)
