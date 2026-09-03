local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharInfo

CharInfoCtrl = HL.Class('CharInfoCtrl', uiCtrl.UICtrl)

local CONTROL_TAB_FUNC_DICT = {
    [UIConst.CHAR_INFO_TAB_TYPE.OVERVIEW] = {
        pageType = UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW,

        name = "tab_char_overview",
        audioEvent = "au_ui_btn_menu_overview",
        isUnlocked = true,
        gyroscopeEffect = Types.EPanelGyroscopeEffect.Enable,
        hintText = Language.LUA_CHAR_INFO_TITLE_OVERVIEW,
    },
    [UIConst.CHAR_INFO_TAB_TYPE.WEAPON] = {
        pageType = UIConst.CHAR_INFO_PAGE_TYPE.WEAPON,

        name = "tab_char_weapon",
        audioEvent = "au_ui_btn_menu_weapon",
        systemUnlockType = GEnums.UnlockSystemType.Weapon,
        getLockTip = function()
            return Language.LUA_SYSTEM_WEAPON_LOCKED
        end,
        gyroscopeEffect = Types.EPanelGyroscopeEffect.Enable,
        hintText = Language.LUA_CHAR_INFO_TITLE_WEAPON,
    },
    [UIConst.CHAR_INFO_TAB_TYPE.EQUIP] = {
        pageType = UIConst.CHAR_INFO_PAGE_TYPE.EQUIP,

        name = "tab_char_equip",
        audioEvent = "au_ui_btn_menu_equip",
        systemUnlockType = GEnums.UnlockSystemType.Equip,
        gyroscopeEffect = Types.EPanelGyroscopeEffect.Enable,
        getLockTip = function()
            return Language.LUA_SYSTEM_EQUIP_LOCKED
        end,
        redDot = "EquipTab",
        hintText = Language.LUA_CHAR_INFO_TITLE_EQUIP,
    },
    [UIConst.CHAR_INFO_TAB_TYPE.POTENTIAL] = {
        pageType = UIConst.CHAR_INFO_PAGE_TYPE.POTENTIAL,
        name = "tab_char_potential",
        audioEvent = "au_ui_btn_menu_potential",
        gyroscopeEffect = Types.EPanelGyroscopeEffect.Enable,
        isUnlocked = true,
        getLockTip = function()
            return Language.LUA_SYSTEM_POTENTIAL_LOCKED
        end,
        redDot = "CharInfoPotential",
        hintText = Language.LUA_CHAR_INFO_TITLE_POTENTIAL,
    },
}
CharInfoCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CHAR_INFO_SELECT_CHAR_CHANGE] = 'OnSelectCharChange',
    [MessageConst.CHAR_INFO_EMPTY_BUTTON_CLICK] = 'OnCommonEmptyButtonClick',
    [MessageConst.TOGGLE_CHAR_INFO_FOCUS_MODE] = "OnToggleFocusMode",
    [MessageConst.TOGGLE_CHAR_INFO_TOGGLE_MENU_AND_TOP_BTN] = "OnToggleMenuListAndTopBtn",
    [MessageConst.GUIDE_CHAR_INFO_CHANGE_CHAR] = "GuideChangeChar",
    [MessageConst.ON_SCREEN_SIZE_CHANGED] = '_UpdateGyroEffectParams',
}

do

    CharInfoCtrl.m_getCharHeadCell = HL.Field(HL.Function)

    CharInfoCtrl.m_charInfoList = HL.Field(HL.Table)

    CharInfoCtrl.m_charInfo = HL.Field(HL.Table)

    CharInfoCtrl.m_professionCache = HL.Field(HL.Forward("UIListCache"))

    CharInfoCtrl.m_elementalCache = HL.Field(HL.Forward("UIListCache"))

    CharInfoCtrl.m_equipIsOn = HL.Field(HL.Boolean) << false

    CharInfoCtrl.m_effectCor = HL.Field(HL.Thread)

    CharInfoCtrl.m_tabCellCache = HL.Field(HL.Forward("UIListCache"))

    CharInfoCtrl.m_equipSlotMap = HL.Field(HL.Table)

    CharInfoCtrl.m_curPageType = HL.Field(HL.Number) << UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW

    CharInfoCtrl.m_focusMode = HL.Field(HL.Boolean) << false

    CharInfoCtrl.m_isCharListInited = HL.Field(HL.Boolean) << false

    CharInfoCtrl.m_arg = HL.Field(HL.Table)
end

CharInfoCtrl.OnCreate = HL.Override(HL.Opt(HL.Any)) << function(self, arg)
    self.m_arg = arg
    local initCharInfo = arg.initCharInfo or CharInfoUtils.getLeaderCharInfo()
    local pageType = arg.pageType or UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW

    self.m_curPageType = pageType
    self.m_charInfo = initCharInfo
    self.m_charInfoList = arg.phase.m_charInfoList

    self:_InitActionEvent()
    self:_InitCharInfoController(arg.forceSkipIn)
    self:OnPageChange(arg)


    local scrollToIndex = -1
    for k, info in ipairs(self.m_charInfoList) do
        if info.instId == initCharInfo.instId then
            scrollToIndex = CSIndex(k)
            break
        end
    end
    if scrollToIndex >= Const.BATTLE_SQUAD_MAX_CHAR_NUM then
        self.view.charListNode.charList:ScrollToIndex(scrollToIndex, true)
    end
    if #self.m_charInfoList <= 1 and self.view.charListNode.keyHintContent then
        self.view.charListNode.keyHintContent.gameObject:SetActive(false)
    end

    self:_UpdateGyroEffectParams()
end

CharInfoCtrl.PhaseCharInfoPanelShowFinal = HL.Method(HL.Table) << function(self, arg)
    local initCharInfo = arg.initCharInfo or CharInfoUtils.getLeaderCharInfo()
    local pageType = arg.pageType or UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW

    self.m_phase = arg.phase
    self.m_curPageType = pageType
    self.m_charInfo = initCharInfo
    self.m_charInfoList = arg.phase.m_charInfoList

    self:Show()
end

CharInfoCtrl.OnShow = HL.Override() << function(self)
    self:OnPageChange(self.m_curPageType)
end

CharInfoCtrl.OnHide = HL.Override() << function (self)
    self.view.bottomMenuCover.gameObject:SetActive(false)
    self.view.gyroscopeRoot.gameObject:SetActive(false)
end

CharInfoCtrl.OnSelectCharChange = HL.Method(HL.Table) << function(self, charInfo)
    Notify(MessageConst.ON_STOP_HOVER_LINK)
    local aimWrapper= self.view.charInfoBasicNodeRight.view.gameObject.activeSelf and
                        self.view.charInfoBasicNodeRight.view.animationWrapper or self.view.gyroscopeRoot
    aimWrapper:ClearTween()
    aimWrapper:PlayOutAnimation(function()
        self.m_charInfo = charInfo

        self:_RefreshCharInfo(charInfo, self.m_charInfoList)

        if self.view.charInfoBasicNodeRight.gameObject.activeSelf then
            self.view.charInfoBasicNodeRight.view.animationWrapper:PlayInAnimation()
        end

        self.view.textTitle.text = CharInfoUtils.getCharInfoTitle(self.m_charInfo.templateId, self.m_curPageType)

        if DeviceInfo.usingController then
            self:_RefreshCharInfoSideMenu()
        end
        aimWrapper:PlayInAnimation()
    end)
end

CharInfoCtrl.OnCommonEmptyButtonClick = HL.Method(HL.Opt(HL.Userdata)) << function(self, _)
    self:_ToggleExpandNode(false)
    local scrollToIndex = -1
    if self.m_charInfo then
        for k, info in ipairs(self.m_charInfoList) do
            if info.instId == self.m_charInfo.instId then
                scrollToIndex = CSIndex(k)
            end
        end
    end
    if scrollToIndex >= 0 then
        self.view.charListNode.charList:ScrollToIndex(scrollToIndex, true)
    end
end

CharInfoCtrl.OnToggleFocusMode = HL.Method(HL.Boolean) << function(self, isOn)
    if self.m_focusMode == isOn then
        return
    end
    self.m_focusMode = isOn

    self.animationWrapper:ClearTween(true)
    if isOn then
        self:PlayAnimationOut(UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Hide)
        if DeviceInfo.usingTouch then
            self.view.charListNodeAnim:PlayOutAnimation()
        end
    else
        self:Show()
        if DeviceInfo.usingTouch then
            self.view.charListNodeAnim:PlayInAnimation()
        end
    end
end

CharInfoCtrl.OnToggleMenuListAndTopBtn = HL.Method(HL.Boolean) << function(self, isOn)
    self.view.menuListNode.gameObject:SetActive(isOn)
    self.view.closeButton.gameObject:SetActive(isOn)
end

CharInfoCtrl._InitActionEvent = HL.Method() << function(self)
    self.m_getCharHeadCell = UIUtils.genCachedCellFunction(self.view.charListNode.charList)
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(self.m_charInfo.instId)
    local isNormalChar = charInst and charInst.charType ~= GEnums.CharType.Trial

    self.view.bottomMenuNode.gameObject:SetActive(isNormalChar)
    self.view.bottomMenuNode.previewBtn.onClick:AddListener(function()
        self:Notify(MessageConst.CHAR_INFO_PAGE_CHANGE, {
            pageType = UIConst.CHAR_INFO_PAGE_TYPE.PROFILE_SHOW,
            extraArg = {
                onlyShow = true
            }
        })
    end)
    self.view.bottomMenuNode.fashionBtn.onClick:AddListener(function()
        self:Notify(MessageConst.SHOW_TOAST, "切时装(还没做)")
    end)

    self.view.rightBottomNode.profileBtn.gameObject:SetActive(isNormalChar)
    self.view.rightBottomNode.profileBtn.onClick:AddListener(function()
        self:Notify(MessageConst.CHAR_INFO_PAGE_CHANGE, {
            pageType = UIConst.CHAR_INFO_PAGE_TYPE.PROFILE
        })
    end)
    self.view.closeButton.onClick:AddListener(function()
        self.m_phase:OnCommonBackClicked()
    end)

    self.view.expandListButton.onClick:AddListener(function()
        self:_ToggleExpandNode(true)
    end)

    self.view.charListNode.charList.onUpdateCell:AddListener(function(object, csIndex)
        self:_RefreshSmallCharHeadCell(object, csIndex)
    end)
    self.view.charListNode.charList.getCurSelectedIndex = function()
        if self.m_charInfo then
            for k, info in ipairs(self.m_charInfoList) do
                if info.instId == self.m_charInfo.instId then
                    return CSIndex(k)
                end
            end
        end
        return -1
    end

    self.m_tabCellCache = UIUtils.genCellCache(self.view.menuListCell)

    self.view.upgradeBtn.onClick:AddListener(function()
        if not CharInfoUtils.isCharDevAvailable(self.m_charInfo.instId) then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_INFO_UPGRADE_FORBID)
            return
        end

        self:Notify(MessageConst.CHAR_INFO_PAGE_CHANGE, {
            pageType = UIConst.CHAR_INFO_PAGE_TYPE.UPGRADE
        })
    end)
    self.view.skillBtn.onClick:AddListener(function()
        self:Notify(MessageConst.CHAR_INFO_PAGE_CHANGE, {
            pageType = UIConst.CHAR_INFO_PAGE_TYPE.TALENT
        })
    end)

    self.m_equipSlotMap = {}
    for _, equipType in pairs(UIConst.CHAR_INFO_EQUIP_SLOT_MAP) do
        local cellConfig = UIConst.EQUIP_PART_TYPE_2_CELL_CONFIG[equipType]

        if cellConfig then
            local equipCellName = "equip_" .. cellConfig.equipPostfix
            local equipCell = self.view[equipCellName]
            if equipCell ~= nil then
                self.m_equipSlotMap[equipType] = equipCell
            else
                logger.error("equipCell is nil, equipType: " .. equipType)
            end
        end
    end

    local isPreview = self.m_charInfo.isShowPreview
    self.view.commonToggle.gameObject:SetActive(isPreview)
    if isPreview then
        self.view.commonToggle:InitCommonToggle(function(isOn)
            Notify(MessageConst.CHAR_INFO_CLOSE_SKILL_TIP)
            Notify(MessageConst.CHAR_INFO_CLOSE_ATTR_TIP)
            self.m_phase:ToggleInitMaxState(isOn)
            self.m_charInfo = self.m_phase.m_charInfo
            self.m_charInfoList = self.m_phase.m_charInfoList
            self:_RefreshCharInfo(self.m_charInfo, self.m_charInfoList)
        end, false, true)
        if self.m_arg.stateArg and self.m_arg.stateArg.isInitPreview then
            self.view.commonToggle:SetValue(true)
        end
    end
    if self.m_charInfo.charInstIdList ~= nil and #self.m_charInfo.charInstIdList == 1 then
        self.view.expandListButton.gameObject:SetActive(false)
    end

    self:BindInputPlayerAction("common_open_char_panel", function()
        PhaseManager:PopPhase(PhaseId.CharInfo)
    end)
end

CharInfoCtrl._RefreshCharInfo = HL.Method(HL.Table, HL.Table) << function(self, initCharInfo, charInfoList)
    self:_RefreshCharList(initCharInfo, charInfoList)
    self:_RefreshCharInfoBasic(initCharInfo.instId)
    if self.view.detailNode.gameObject.activeSelf then
        self:_RefreshDetailNode(initCharInfo)
    end
    self:_RefreshRedDot()
    self:_ChangeDungeonNode(initCharInfo.templateId)
end

CharInfoCtrl._RefreshCharInfoBasic = HL.Method(HL.Number) << function(self, charInstId)
    self.view.charInfoBasicNodeLeft:InitCharInfoBasicNode(charInstId)
    self.view.charInfoBasicNodeRight:InitCharInfoBasicNode(charInstId, true)
    self.view.friendshipNode:InitFriendshipNode(charInstId)
end

CharInfoCtrl._RefreshCharList = HL.Method(HL.Table, HL.Table) << function(self, initCharInfo, charInfoList)
    if self.m_phase then
        self.m_phase:RefreshCharExpandList(initCharInfo, charInfoList)
    end
    self.view.charListNode.charList:UpdateCount(#charInfoList)
    self.m_isCharListInited = true
end

CharInfoCtrl._RefreshSmallCharHeadCell = HL.Method(HL.Userdata, HL.Number) << function(self, object, csIndex)
    local charInfo = self.m_charInfoList[LuaIndex(csIndex)]
    local templateId = charInfo.templateId
    local instId = charInfo.instId
    local charHeadCell = self.m_getCharHeadCell(object)
    local isSameInstId = charInfo.instId == self.m_charInfo.instId
    local isInSlot = CharInfoUtils.checkIsCardInSlot(instId)

    charHeadCell.tryoutTips.gameObject:SetActive(charInfo.isShowTrail)
    charHeadCell.fixedTips.gameObject:SetActive(charInfo.isShowFixed)
    charHeadCell.previewTips.gameObject:SetActive(charInfo.isShowPreview)

    charHeadCell.redDot:InitRedDot("CharInfo", instId)
    charHeadCell.charInfo = charInfo
    charHeadCell.bgSelected.gameObject:SetActive(isSameInstId)
    charHeadCell.charImage:LoadSprite(CharInfoUtils.getCharHeadSpriteName(templateId))
    charHeadCell.formationMark.gameObject:SetActive(isInSlot)
    charHeadCell.button.onClick:RemoveAllListeners()
    charHeadCell.button.onClick:AddListener(function()
        self:_OnClickCharHeadCell(charHeadCell.charInfo, true)
    end)
end

CharInfoCtrl._ToggleExpandNode = HL.Method(HL.Boolean) << function(self, isOn)
    if self.view.charListNode.animation then
        if isOn then
            self.view.charListNode.animation:Play("charinfo_top_all_out")
        else
            self.view.charListNode.animation:Play("charinfo_top_all_in")
        end
    end

    if self.m_curPageType ~= UIConst.CHAR_INFO_TAB_TYPE.OVERVIEW then
        if isOn then

            local args = {
                onClickCell = function(charInfo)
                    self:_OnClickCharHeadCell(charInfo, true)
                end,
                charInfo = self.m_charInfo,
                charInfoList = self.m_charInfoList,
                refreshAddon = function(cell, charInfo)
                    cell.view.selectedBG.gameObject:SetActive(charInfo.instId == self.m_charInfo.instId)
                end,
                stateArg = self.m_arg.stateArg,
            }
            self.m_phase:ShowCharExpandList(args)
        else
            self.m_phase:HideCharExpandList()
        end
        return
    end
    if isOn then
        self.view.topNode:Play("charinfo_top_all_out")
        if DeviceInfo.usingTouch then
            self.view.charListNodeAnim:PlayOutAnimation()
        else
            self.view.expandListBtnAnim:PlayOutAnimation()
        end
        self.view.menuListNodeAnim:PlayOutAnimation()
        self.view.bottomMenuCover:PlayOutAnimation()
        self.view.rightBottomNode.animationWrapper:PlayOutAnimation()
        self.view.gyroscopeRoot:ClearTween(false)
        self.view.luaPanel:BlockAllInput()
        self.view.gyroscopeRoot:PlayOutAnimation(function()
            self.view.luaPanel:RecoverAllInput()
            self.view.menuListNode.gameObject:SetActive(false)
            UIUtils.PlayAnimationAndToggleActive(self.view.charInfoBasicNodeRight.view.animationWrapper, true)


            local args = {
                onClickCell = function(charInfo)
                    self:_OnClickCharHeadCell(charInfo, true)
                end,
                charInfo = self.m_charInfo,
                charInfoList = self.m_charInfoList,
                refreshAddon = function(cell, charInfo)
                    local showSelectedBG = charInfo.instId == self.m_charInfo.instId and not DeviceInfo.usingController
                    cell.view.selectedBG.gameObject:SetActive(showSelectedBG)
                end,
                stateArg = self.m_arg.stateArg,
            }
            self.m_phase:ShowCharExpandList(args)
        end)
    else
        self.view.topNode:Play("charinfo_top_all_in")
        if DeviceInfo.usingTouch then
            self.view.charListNodeAnim:PlayInAnimation()
        else
            self.view.expandListBtnAnim:PlayInAnimation()
        end
        self.view.menuListNodeAnim:PlayInAnimation()
        self.view.bottomMenuCover:PlayInAnimation()
        self.view.rightBottomNode.animationWrapper:PlayInAnimation()
        self.m_phase:HideCharExpandList()
        UIUtils.PlayAnimationAndToggleActive(self.view.charInfoBasicNodeRight.view.animationWrapper, false, function()
            self.view.gyroscopeRoot:ClearTween(false)
            self.view.gyroscopeRoot:PlayInAnimation()
            self.view.menuListNode.gameObject:SetActive(true)
        end)
    end

    self.view.menuListNode.blocksRaycasts = not isOn
    self.view.charGradeNode.blocksRaycasts = not isOn
end

CharInfoCtrl._OnClickCharHeadCell = HL.Method(HL.Table, HL.Boolean) << function(self, charInfo, realClick)
    self:_ChangeSelectIndex(charInfo, realClick)
end

CharInfoCtrl._ChangeSelectIndex = HL.Method(HL.Table, HL.Boolean) << function(self, charInfo, realClick)
    local curTemplateId = self.m_charInfo.templateId
    local curInstId = self.m_charInfo.instId
    local templateId = charInfo.templateId
    if CharInfoUtils.getPlayerCharInfoByTemplateId(templateId, GEnums.CharType.Default)
        and RedDotManager:GetRedDotState("CharNew", templateId) then
        GameInstance.player.charBag:Send_RemoveCharNewTag(templateId)
    end

    if curTemplateId == charInfo.templateId and curInstId == charInfo.instId then
        return
    end
    Notify(MessageConst.CHAR_INFO_SELECT_CHAR_CHANGE, charInfo)


end

CharInfoCtrl._ChangeDungeonNode = HL.Method(HL.String) << function(self, templateId)
    local virtualId = CS.Beyond.Gameplay.CharUtils.GetVirtualCharTemplateId(templateId)
    local success, dungeonId = Tables.CharId2DungeonIdTable:TryGetValue(virtualId)
    if success then
        self.view.dungeonNode.gameObject:SetActive(RedDotManager:GetRedDotState("DungeonReadNormal", { dungeonId }))
    else
        self.view.dungeonNode.gameObject:SetActive(false)
    end
end

CharInfoCtrl._RefreshRedDot = HL.Method() << function(self)
    self.view.rightBottomNode.profileBtnRedDot:InitRedDot("CharInfoProfile", self.m_charInfo.templateId)
    self.view.skillBtnRedDot:InitRedDot("CharBreak", self.m_charInfo.instId)
    if self.m_tabCellCache then
        local count = self.m_tabCellCache:GetCount()
        for i = 1, count do
            local cell = self.m_tabCellCache:GetItem(i)
            local config = CONTROL_TAB_FUNC_DICT[i]
            local isUnlocked = self:_CheckIfTabUnlock(i)
            local redDot = config.redDot
            if isUnlocked and redDot then
                cell.redDot:InitRedDot(redDot, self.m_charInfo.instId)
            else
                cell.redDot:Stop()
            end
        end
    end
end

CharInfoCtrl._RefreshMenuNode = HL.Method(HL.Number) << function(self, curPageType)
    self.m_tabCellCache:Refresh(lume.count(CONTROL_TAB_FUNC_DICT), function(cell, index)
        local pageType = CONTROL_TAB_FUNC_DICT[index].pageType
        self:_RefreshTabCell(cell, index, pageType == curPageType, curPageType)

        cell.gameObject.name = CONTROL_TAB_FUNC_DICT[index].name
    end)
    self:_RefreshMenuNodeNavi()
end

CharInfoCtrl._RefreshDetailNode = HL.Method(HL.Table) << function(self, charInfo)
    self.view.charLevelNode:InitCharLevelNode(charInfo.instId)
    self.view.potentialRankNode:InitPotentialRankNode(charInfo.instId)
    self:_RefreshWeaponNode(charInfo)
    self:_RefreshEquipNode(charInfo)
end

CharInfoCtrl._RefreshWeaponNode = HL.Method(HL.Table) << function(self, charInfo)
    local weaponInfo = CharInfoUtils.getCharCurWeapon(charInfo.instId)

    local weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponInfo.weaponTemplateId, weaponInfo.weaponInstId)
    self.view.weaponIcon:LoadSprite(UIConst.UI_SPRITE_ITEM_BIG, weaponExhibitInfo.itemCfg.iconId)
    UIUtils.setItemRarityImage(self.view.weaponRarityMarker.mainColor, weaponExhibitInfo.itemCfg.rarity)
    self.view.weaponLvText.text = weaponExhibitInfo.curLv
end

CharInfoCtrl._RefreshEquipNode = HL.Method(HL.Table) << function(self, charInfo)
    local instId = self.m_charInfo.instId
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(instId)
    local equips = charInst.equipCol

    for slotIndex, config in pairs(UIConst.EQUIP_PART_TYPE_2_CELL_CONFIG) do
        local cell = self.m_equipSlotMap[slotIndex]
        local itemId


        if config.isTacticalItem then
            local equippedTacticalId = charInst.tacticalItemId
            if equippedTacticalId and not string.isEmpty(equippedTacticalId) then
                itemId = charInst.tacticalItemId
            end
        else
            local equipIndex = UIConst.EQUIP_PART_TYPE_2_CELL_CONFIG[slotIndex].equipIndex
            local hasValue, equipInstId = equips:TryGetValue(equipIndex)
            if hasValue then
                local equipInst = CharInfoUtils.getEquipByInstId(equipInstId)
                itemId = equipInst.templateId
            end
        end

        local hasValue, itemCfg = Tables.itemTable:TryGetValue(itemId or "")
        cell.iconNode.gameObject:SetActive(hasValue)
        cell.emptyNode.gameObject:SetActive(not hasValue)
        cell.equipmentColorCell.gameObject:SetActive(hasValue)
        if hasValue then
            cell.icon:LoadSprite(UIConst.UI_SPRITE_ITEM, itemCfg.iconId)
            UIUtils.setItemRarityImage(cell.equipmentColorCell.mainColor, itemCfg.rarity)
        end
    end
end

CharInfoCtrl._RefreshTabCell = HL.Method(HL.Any, HL.Number, HL.Boolean, HL.Number) << function(self, cell, index, isCurTab, curPageType)
    local tabIcon = UIConst.CHAR_INFO_TAB_ICON_PREFIX .. index
    local config = CONTROL_TAB_FUNC_DICT[index]

    if cell.isCurTabBefore == false and isCurTab == true then
        UIUtils.PlayAnimationAndToggleActive(cell.cellSelected, true)
    elseif cell.isCurTabBefore == true and isCurTab == false then
        UIUtils.PlayAnimationAndToggleActive(cell.cellSelected, false)
    else
        cell.cellSelected.gameObject:SetActive(isCurTab)
    end

    cell.defalutBg.gameObject:SetActive(not isCurTab)
    cell.hotAreaBig.gameObject:SetActive(not isCurTab and curPageType == UIConst.CHAR_INFO_TAB_TYPE.OVERVIEW)
    cell.hotAreaSmall.gameObject:SetActive(isCurTab)
    cell.hintRectStateCtrl:SetState(isCurTab and "Select" or "Unselect")

    cell.isCurTabBefore = isCurTab


    local isUnlocked = self:_CheckIfTabUnlock(index)
    cell.icon:LoadSprite(UIConst.UI_SPRITE_CHAR_INFO, tabIcon)
    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        self:_OnClickTab(index)
    end)
    cell.button.customBindingViewLabelText = config.hintText

    if DeviceInfo.usingController then
        cell.button.onIsNaviTargetChanged = function(isTarget)
            InputManagerInst:ToggleBinding(cell.button.hoverConfirmBindingId, isTarget and not cell.isCurTabBefore)
            InputManagerInst:ToggleBinding(self.m_menuTabConfirmBindingId, isTarget and cell.isCurTabBefore)
        end
        if not self.m_hasControllerSetTarget and isCurTab and
            InputManagerInst.controllerNaviManager.curTarget ~= cell.button then
            self:SetNaviTarget(cell.button)
            self.m_hasControllerSetTarget = true
        end
        if isCurTab then
            InputManagerInst:ToggleBinding(cell.button.hoverConfirmBindingId, false)
        end
        if isCurTab and InputManagerInst.controllerNaviManager.curTarget == cell.button then
            InputManagerInst:ToggleBinding(self.m_menuTabConfirmBindingId, true)
        end
    end
end

CharInfoCtrl._OnClickTab = HL.Method(HL.Number) << function(self, index)
    local config = CONTROL_TAB_FUNC_DICT[index]
    local isUnlocked = self:_CheckIfTabUnlock(index)

    if not isUnlocked then
        local lockTip = config.getLockTip and config.getLockTip() or Language.LUA_FEATURE_NOT_AVAILABLE
        self:Notify(MessageConst.SHOW_TOAST, lockTip)
        AudioAdapter.PostEvent("au_ui_btn_menu_inactive")
        return
    end

    if self.m_curPageType == config.pageType then
        return
    end

    AudioAdapter.PostEvent(config.audioEvent)
    self:ChangePanelCfg("gyroscopeEffect", config.gyroscopeEffect)
    self:Notify(MessageConst.CHAR_INFO_PAGE_CHANGE, {
        pageType = config.pageType
    })
end

CharInfoCtrl.OnPageChange = HL.Method(HL.Any) << function(self, arg)
    local pageType = arg
    local extraArg
    local forceSkipIn = false
    if type(arg) == "table" then
        pageType = arg.pageType
        extraArg = arg.extraArg
        forceSkipIn = arg.forceSkipIn or false
    end

    self:_TryToggleDetailNode(true, pageType)
    self:_RefreshMenuNode(pageType)

    local isBeforePageOverview = self.m_curPageType == UIConst.CHAR_INFO_TAB_TYPE.OVERVIEW
    local isPageOverview = pageType == UIConst.CHAR_INFO_TAB_TYPE.OVERVIEW
    self.m_curPageType = pageType

    UIUtils.PlayAnimationAndToggleActive(self.view.bottomMenuCover, isPageOverview)
    UIUtils.PlayAnimationAndToggleActive(self.view.gyroscopeRoot, isPageOverview)
    if isBeforePageOverview and not forceSkipIn and (pageType == UIConst.CHAR_INFO_TAB_TYPE.WEAPON
        or pageType == UIConst.CHAR_INFO_TAB_TYPE.EQUIP or pageType == UIConst.CHAR_INFO_TAB_TYPE.POTENTIAL) then
        self.view.controllerHintPlaceholder:PlayAnimationOut()
    end
    self.view.controllerSideMenuBtn.gameObject:SetActive(isPageOverview)
    local isTabType = lume.find(UIConst.CHAR_INFO_TAB_TYPE, pageType) ~= nil
    if DeviceInfo.usingTouch then
        UIUtils.PlayAnimationAndToggleActive(self.view.charListNodeAnim, isTabType)
    end
    if isTabType then
        self.view.expandListBtnAnim.gameObject:SetActive(true)
        if self.m_charInfo.charInstIdList ~= nil and #self.m_charInfo.charInstIdList == 1 then
            self.view.expandListButton.gameObject:SetActive(false)
        end
    else
        UIUtils.PlayAnimationAndToggleActive(self.view.expandListBtnAnim, false)
    end

    if isPageOverview then
        self:_RefreshCharInfo(self.m_charInfo, self.m_charInfoList)
        self:_RefreshDetailNode(self.m_charInfo)
        self.view.rightBottomNode.animationWrapper.gameObject:SetActive(true)
        self.view.topNode:Play("charinfo_top_btn_in")
    else
        UIUtils.PlayAnimationAndToggleActive(self.view.rightBottomNode.animationWrapper, false)
        if isBeforePageOverview then
            self.view.topNode:Play("charinfo_top_btn_out")
        end
    end


    if not self.m_isCharListInited then
        self:_RefreshCharList(self.m_charInfo, self.m_charInfoList)
    end

    self.view.textTitle.text = CharInfoUtils.getCharInfoTitle(self.m_charInfo.templateId, pageType)

    if self.m_charInfo.isShowPreview then
        self.view.commonToggle.gameObject:SetActive(isPageOverview)
    end
end

CharInfoCtrl._TryToggleDetailNode = HL.Method(HL.Boolean, HL.Any) << function(self, isOn, pageType)
    local shouldActive = isOn and pageType == UIConst.CHAR_INFO_TAB_TYPE.OVERVIEW
    if shouldActive then
        self.view.gyroscopeRoot:ClearTween()
        self.view.gyroscopeRoot:PlayInAnimation()
    else
        self.view.gyroscopeRoot:ClearTween()
        self.view.gyroscopeRoot:PlayOutAnimation()
    end
end

CharInfoCtrl._CheckIfTabUnlock = HL.Method(HL.Number).Return(HL.Boolean) << function(self, tabIndex)
    local config = CONTROL_TAB_FUNC_DICT[tabIndex]
    if config.isUnlocked ~= nil then
        return config.isUnlocked
    end

    if config.systemUnlockType then
        return Utils.isSystemUnlocked(config.systemUnlockType)
    end

    return false
end

CharInfoCtrl.GuideChangeChar = HL.Method(HL.Table) << function(self, args)
    local charId = unpack(args)
    if string.isEmpty(charId) then
        logger.error("GuideChangeChar failed, charId is empty")
        return
    end
    charId = CSCharUtils.GetCharTemplateId(charId)
    local charInfo
    local scrollToIndex = -1
    for k, info in ipairs(self.m_charInfoList) do
        if info.templateId == charId then
            scrollToIndex = CSIndex(k)
            charInfo = info
            break
        end
    end
    if not charInfo then
        logger.error("GuideChangeChar failed, charId: " .. charId)
        return
    end
    self.view.charListNode.charList:ScrollToIndex(scrollToIndex, true)
    self:_ChangeSelectIndex(charInfo, false)
end

CharInfoCtrl._UpdateGyroEffectParams = HL.Method() << function(self)
    local curAspectRatio = Screen.width / Screen.height
    local maxAngel = self.view.config.GYRO_EFFECT_MAX_ANGLE_CURVE:Evaluate(curAspectRatio)
    self.view.gyroscopeEffect.y.maxAngle = maxAngel
end



CharInfoCtrl.m_hasControllerSetTarget = HL.Field(HL.Boolean) << false

CharInfoCtrl.m_menuTabConfirmBindingId = HL.Field(HL.Number) << -1

CharInfoCtrl._InitCharInfoController = HL.Method(HL.Opt(HL.Boolean)) << function(self, forceSkipIn)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    self.m_menuTabConfirmBindingId = self:BindInputPlayerAction("char_menu_tab_confirm", function()
        if self.m_curPageType == UIConst.CHAR_INFO_PAGE_TYPE.WEAPON then
            local weaponPanelItem = self.m_phase:_GetPanelPhaseItem(PanelId.CharInfoWeapon)
            if not weaponPanelItem or not weaponPanelItem.uiCtrl then
                return
            end
            self:SetNaviTarget(weaponPanelItem.uiCtrl.view.weaponInfoRight.view.charWeaponBasicNode.btnUpgrade)
        end
        if self.m_curPageType == UIConst.CHAR_INFO_PAGE_TYPE.EQUIP then
            local equipPanelItem = self.m_phase:_GetPanelPhaseItem(PanelId.CharInfoEquipSlot)
            if not equipPanelItem or not equipPanelItem.uiCtrl then
                return
            end
            self:SetNaviTarget(equipPanelItem.uiCtrl.view.equipBody.button)
        end
    end)
    InputManagerInst:ToggleBinding(self.m_menuTabConfirmBindingId, false)

    self.view.charListNode.charList.onSelectedCell:AddListener(function(object, csIndex)
        local charHeadCell = self.m_getCharHeadCell(object)
        if charHeadCell == nil then
            return
        end
        self:_OnClickCharHeadCell(charHeadCell.charInfo, true)
        Notify(MessageConst.CHAR_INFO_CLOSE_ATTR_TIP)
        Notify(MessageConst.CHAR_INFO_CLOSE_SKILL_TIP)

        AudioAdapter.PostEvent("Au_UI_Button_CharHeadCell")
    end)

    self.view.menuListNodeNaviGroup.onIsTopLayerChanged:AddListener(function(isTopLayer)
        self.view.controllerLightDeco.gameObject:SetActive(isTopLayer)
        self.view.controllerShadowMenuDeco.gameObject:SetActive(isTopLayer)
        if not isTopLayer or self.m_curPageType == UIConst.CHAR_INFO_PAGE_TYPE.OVERVIEW then
            return
        end
        self:_StartCoroutine(function()
            coroutine.step()

            if forceSkipIn then
                forceSkipIn = false
                return
            end
            if UIManager:IsShow(PanelId.CharInfoFullAttribute) or
                not self.view.menuListNodeNaviGroup.IsTopLayer then
                return
            end
            local selectedTab = self.m_tabCellCache:Get(self.m_curPageType)
            if selectedTab then
                if InputManagerInst.controllerNaviManager.curTarget ~= selectedTab.button then
                    self:SetNaviTarget(selectedTab.button)
                end
            end
        end)
    end)

    self.view.detailNodeNaviGroup.onIsTopLayerChanged:AddListener(function(isTopLayer)
        self.view.controllerShadowDetailDeco.gameObject:SetActive(isTopLayer)
    end)

    self:_RefreshCharInfoSideMenu()
end

CharInfoCtrl._RefreshMenuNodeNavi = HL.Method() << function(self)
    local secondTab = self.m_tabCellCache:Get(2)
    if not secondTab then
        return
    end
    self.view.upgradeBtn.useExplicitNaviSelect = true
    self.view.upgradeBtn.banExplicitOnUp = true
    self.view.upgradeBtn.banExplicitOnLeft = true
    self.view.upgradeBtn.banExplicitOnRight = true
    self.view.upgradeBtn:SetExplicitSelectOnDown(secondTab.button)

    self.view.skillBtn.useExplicitNaviSelect = true
    self.view.skillBtn.banExplicitOnUp = true
    self.view.skillBtn.banExplicitOnLeft = true
    self.view.skillBtn.banExplicitOnRight = true
    self.view.skillBtn:SetExplicitSelectOnDown(secondTab.button)

    local lastTab = self.m_tabCellCache:Get(self.m_tabCellCache:GetCount())
    if not lastTab then
        return
    end
    self.view.rightBottomNode.profileBtn.useExplicitNaviSelect = true
    self.view.rightBottomNode.profileBtn.banExplicitOnDown = true
    self.view.rightBottomNode.profileBtn.banExplicitOnLeft = true
    self.view.rightBottomNode.profileBtn.banExplicitOnRight = true
    self.view.rightBottomNode.profileBtn:SetExplicitSelectOnUp(lastTab.button)
end

CharInfoCtrl._RefreshCharInfoSideMenu = HL.Method() << function(self)
    self.view.controllerSideMenuBtn:InitControllerSideMenuBtn()
end




HL.Commit(CharInfoCtrl)
