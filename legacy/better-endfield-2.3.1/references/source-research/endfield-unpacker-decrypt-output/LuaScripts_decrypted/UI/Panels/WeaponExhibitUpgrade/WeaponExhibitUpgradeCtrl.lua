
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.WeaponExhibitUpgrade
WeaponExhibitUpgradeCtrl = HL.Class('WeaponExhibitUpgradeCtrl', uiCtrl.UICtrl)







local SELECTION_OPTIONS = {
    {
        nameKey = "LUA_WEAPON_EXHIBIT_SELECTION_LEVEL_TWO",
        maxFillWeaponQuality = 2,
    },
    {
        nameKey = "LUA_WEAPON_EXHIBIT_SELECTION_LEVEL_THREE",
        maxFillWeaponQuality = 3,
    },
    {
        nameKey = "LUA_WEAPON_EXHIBIT_SELECTION_LEVEL_FOUR",
        maxFillWeaponQuality = 4,
    },
}

WeaponExhibitUpgradeCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_WEAPON_GAIN_EXP] = 'OnWeaponGainExp',
    [MessageConst.ON_WEAPON_BREAKTHROUGH] = 'OnWeaponBreakthrough',
    [MessageConst.CACHE_REWARDS_POPUP] = 'CacheRewardsPopup',
    [MessageConst.SHOW_CACHED_REFUND_POPUP] = 'ShowRewardsPopup',
    [MessageConst.ON_ITEM_LOCKED_STATE_CHANGED] = '_OnItemLockedStateChanged',
    [MessageConst.ON_GEM_DETACH] = 'OnGemDetach',
    [MessageConst.ON_ITEM_COUNT_CHANGED] = '_OnItemCountChanged',
}

WeaponExhibitUpgradeCtrl.m_arg = HL.Field(HL.Table)

WeaponExhibitUpgradeCtrl.m_level2RequireExpDict = HL.Field(HL.Table)

WeaponExhibitUpgradeCtrl.m_level2RequireGoldDict = HL.Field(HL.Table)

WeaponExhibitUpgradeCtrl.m_costItemInfoList = HL.Field(HL.Table)

WeaponExhibitUpgradeCtrl.m_costItemInfoDict = HL.Field(HL.Table)

WeaponExhibitUpgradeCtrl.m_upgradeItemInfoList = HL.Field(HL.Table)

WeaponExhibitUpgradeCtrl.m_weaponInfo = HL.Field(HL.Table)

WeaponExhibitUpgradeCtrl.m_isFocusJump = HL.Field(HL.Boolean) << false

WeaponExhibitUpgradeCtrl.m_getBasicCostItemCell = HL.Field(HL.Function)

WeaponExhibitUpgradeCtrl.m_getExpandCostItemCell = HL.Field(HL.Function)

WeaponExhibitUpgradeCtrl.m_getBreakCostItemCell = HL.Field(HL.Function)

WeaponExhibitUpgradeCtrl.m_isBreakthrough = HL.Field(HL.Boolean) << false

WeaponExhibitUpgradeCtrl.m_selectionOptions = HL.Field(HL.Table)

WeaponExhibitUpgradeCtrl.m_maxFillWeaponQuality = HL.Field(HL.Number) << 4

WeaponExhibitUpgradeCtrl.m_weaponExhibitInfo = HL.Field(HL.Table)

WeaponExhibitUpgradeCtrl.m_curGenerateExp = HL.Field(HL.Number) << 0

WeaponExhibitUpgradeCtrl.m_curTargetLv = HL.Field(HL.Number) << 0

WeaponExhibitUpgradeCtrl.m_isBreakItemEnough = HL.Field(HL.Boolean) << false

WeaponExhibitUpgradeCtrl.m_rewardCache = HL.Field(HL.Any)

WeaponExhibitUpgradeCtrl.m_lastClickItemId = HL.Field(HL.Any)

WeaponExhibitUpgradeCtrl.m_lastClickItemInfo = HL.Field(HL.Table)

WeaponExhibitUpgradeCtrl.m_forceFocusListIndex = HL.Field(HL.Number) << -1

WeaponExhibitUpgradeCtrl.m_upgradeEffectCor = HL.Field(HL.Thread)

WeaponExhibitUpgradeCtrl.m_itemFillCor = HL.Field(HL.Thread)

WeaponExhibitUpgradeCtrl.m_breakItemList = HL.Field(HL.Userdata)

WeaponExhibitUpgradeCtrl.m_naviMaterialCell = HL.Field(HL.Any)

WeaponExhibitUpgradeCtrl.m_naviMaterialItemInfo = HL.Field(HL.Table)

WeaponExhibitUpgradeCtrl.s_materialFilterSelectionIndex = HL.StaticField(HL.Number) << -1

WeaponExhibitUpgradeCtrl.CacheRewardsPopup = HL.Method(HL.Any) << function(self, args)
    self.m_rewardCache = args
end

WeaponExhibitUpgradeCtrl.ShowRewardsPopup = HL.Method() << function(self)
    if self.m_rewardCache then
        Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
            title = Language.LUA_WEAPON_EXHIBIT_UPGRADE_REFUND,
            items = unpack(self.m_rewardCache),
            icon = UIConst.WEAPON_EXHIBIT_REFUND_ICON,
        })
    end
    self.m_rewardCache = nil
end

WeaponExhibitUpgradeCtrl._OnItemLockedStateChanged = HL.Method(HL.Table) << function(self, arg)
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
        self:_RemoveFromCostItemDict(costItemInfoDict[instId], 1)
    end
end

WeaponExhibitUpgradeCtrl.OnGemDetach = HL.Method(HL.Table) << function(self, arg)
    self.view.expandNode.commonItemList:RefreshAllCells()
end

WeaponExhibitUpgradeCtrl.OnWeaponGainExp = HL.Method(HL.Table) << function(self, arg)
    local weaponInstId, newLv, newExp = unpack(arg)
    local expInfoBefore = self.m_weaponExhibitInfo
    local expInfo = CharInfoUtils.getWeaponExpInfo(weaponInstId)
    local weaponInfo = self.m_weaponInfo

    self:_InitUpgradeCache(weaponInfo.weaponInstId, weaponInfo.weaponTemplateId)
    self:_RefreshTitle(weaponInfo.weaponTemplateId, false)
    self:_ToggleExpandNode(false, true)
    self:_ResetCostItem()
    self:_RefreshBottomCostItem()
    self:_RefreshSelectionDropdown()
    self:_UpdateAddAndReduceButtonState()

    self.m_upgradeEffectCor = self:_ClearCoroutine(self.m_upgradeEffectCor)
    self.m_upgradeEffectCor = self:_StartCoroutine(function()
        LuaSystemManager.inputDeviceChangeSystem:SetForbidInputDeviceChange("WeaponExhibitUpgrade.Anim", true)
        local hadLevelUp = expInfoBefore.curLv < newLv
        if hadLevelUp then
            AudioAdapter.PostEvent("Au_UI_Event_WeaponLevelUp")
            self.view.animation:Play("weaponexhibitupgrade_scroll_in")
        end

        self.view.luaPanel:BlockAllInput()
        coroutine.wait(0.3)
        self:_RefreshUpgradeInformation(weaponInfo.weaponInstId, weaponInfo.weaponTemplateId, hadLevelUp)
        coroutine.wait(1.2)
        self.view.luaPanel:RecoverAllInput()
        self:ShowRewardsPopup()

        if newLv >= expInfo.stageLv then
            if newLv >= expInfo.maxLv then
                self:_LeaveWeaponUpgrade()
                return
            end



            local aniWrapper = self.animationWrapper
            aniWrapper:Play("weaponexhibitupgrade_out", function()
                self:PlayAnimationIn()
                self:OnShow()
                LuaSystemManager.inputDeviceChangeSystem:SetForbidInputDeviceChange("WeaponExhibitUpgrade.Anim", false)
            end)
        end
    end)
end

WeaponExhibitUpgradeCtrl._LeaveWeaponUpgrade = HL.Method() << function(self, arg)
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
end

WeaponExhibitUpgradeCtrl.OnWeaponBreakthrough = HL.Method(HL.Table) << function(self, arg)
    AudioAdapter.PostEvent("Au_UI_Event_WeaponLevelLimit")
    local weaponInstId, newBreakLv = unpack(arg)
    local weaponExpInfo = CharInfoUtils.getWeaponExpInfo(weaponInstId)
    if weaponExpInfo.curBreakthroughLv >= weaponExpInfo.maxBreakthroughLv and weaponExpInfo.curLv >= weaponExpInfo.maxLv then
        self:_LeaveWeaponUpgrade()
        return
    end

    self.m_upgradeEffectCor = self:_ClearCoroutine(self.m_upgradeEffectCor)
    self.m_upgradeEffectCor = self:_StartCoroutine(function()
        LuaSystemManager.inputDeviceChangeSystem:SetForbidInputDeviceChange("WeaponExhibitUpgrade.Anim", true)
        self.view.breakWeaponInfo.view.animation:Play("weaponexhibitupgrade_breakweaponinfo_break_in")
        self.view.luaPanel:BlockAllInput()
        coroutine.wait(0.5)
        self.view.luaPanel:RecoverAllInput()



        local aniWrapper = self.animationWrapper
        self.view.luaPanel:BlockAllInput()
        aniWrapper:Play("weaponexhibitupgrade_out", function()
            self:PlayAnimationIn()
            self:OnShow()
            self.view.luaPanel:RecoverAllInput()
            LuaSystemManager.inputDeviceChangeSystem:SetForbidInputDeviceChange("WeaponExhibitUpgrade.Anim", false)
        end)
    end)
end


WeaponExhibitUpgradeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg
    self:_InitController()
    self:_InitActionEvent()

    self.view.expandNode.commonItemList.view.gameObject:SetActive(false)
    self.m_weaponInfo = arg.weaponInfo
    self.m_isFocusJump = arg.isFocusJump == true
end

WeaponExhibitUpgradeCtrl.OnHide = HL.Override() << function(self)
    LuaSystemManager.inputDeviceChangeSystem:SetForbidInputDeviceChange("WeaponExhibitUpgrade.Anim", false)
end

WeaponExhibitUpgradeCtrl.OnClose = HL.Override() << function()
    LuaSystemManager.inputDeviceChangeSystem:SetForbidInputDeviceChange("WeaponExhibitUpgrade.Anim", false)
    CS.Beyond.Lua.UtilsForLua.ToggleWeaponInUpgradePanelOption(false)
end

WeaponExhibitUpgradeCtrl.OnShow = HL.Override() << function(self)
    local weaponInfo = self.m_weaponInfo
    local weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponInfo.weaponTemplateId, weaponInfo.weaponInstId)
    local weaponTemplateId = weaponExhibitInfo.weaponInst.templateId
    local weaponInstId = weaponExhibitInfo.weaponInst.instId

    local isBreakthrough = weaponExhibitInfo.curLv >= weaponExhibitInfo.stageLv
    self.m_isBreakthrough = isBreakthrough
    self.m_weaponExhibitInfo = weaponExhibitInfo

    self.view.expandNode.gameObject:SetActive(false)

    self:_InitUpgradeCache(weaponInstId, weaponTemplateId)
    self:_RefreshTitle(weaponTemplateId, isBreakthrough)
    self:_ToggleExpandNode(false)
    self:_RefreshSelectionDropdown()

    self.view.breakWeaponInfo.gameObject:SetActive(isBreakthrough)
    self.view.upgradeWeaponInfo.gameObject:SetActive(not isBreakthrough)
    self.view.weaponIntroduction:InitWeaponIntroduction(weaponTemplateId, weaponInstId)

    self.view.upgradeNode.gameObject:SetActive(not isBreakthrough)
    self.view.breakNode.gameObject:SetActive(isBreakthrough)

    CS.Beyond.Lua.UtilsForLua.ToggleWeaponInUpgradePanelOption(not isBreakthrough)
    if isBreakthrough then
        self:_RefreshBreakPanel(weaponInstId, weaponTemplateId)
    else
        local costItemInfoDict
        local isExpand = false
        if self.m_arg.stateArg and self.m_arg.stateArg.costItemInfoDict then
            costItemInfoDict = self.m_arg.stateArg.costItemInfoDict
            isExpand = self.m_arg.stateArg.isExpand
        end
        self:_RefreshUpgradePanel(weaponInstId, weaponTemplateId, costItemInfoDict)
        if costItemInfoDict then
            for indexId, costItemInfo in pairs(costItemInfoDict) do
                for _, upgradeItemInfo in pairs(self.m_upgradeItemInfoList) do
                    if upgradeItemInfo.indexId == indexId then
                        upgradeItemInfo.count = costItemInfo.count
                        upgradeItemInfo.addCount = costItemInfo.addCount
                        costItemInfoDict[indexId] = upgradeItemInfo
                        break
                    end
                end
            end
            self.view.expandNode.commonItemList:RefreshAllCells()
            if isExpand then
                self:_ToggleExpandNode(true)
            end
        end
    end
    self.m_arg.stateArg = nil
end

WeaponExhibitUpgradeCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    self.view.addKeyHint.gameObject:SetActive(active)
    self.view.reduceKeyHint.gameObject:SetActive(active)
end

WeaponExhibitUpgradeCtrl._InitActionEvent = HL.Method() << function(self)
    self.m_getBasicCostItemCell = UIUtils.genCachedCellFunction(self.view.upgradeNode.costItemList)
    self.m_getBreakCostItemCell = UIUtils.genCachedCellFunction(self.view.breakNode.costItemList)

    self.view.btnLevelUp.onClick:AddListener(function()
        local isBreakthrough = self.m_isBreakthrough

        Notify(MessageConst.HIDE_ITEM_TIPS)
        if isBreakthrough then
            self:_ClickWeaponBreakButton()
        else
            self:_ClickWeaponUpgradeButton()
        end
    end)

    self.view.btnBack.onClick:AddListener(function()
        Notify(MessageConst.HIDE_ITEM_TIPS)
        if self.m_isFocusJump then
            Notify(MessageConst.WEAPON_EXHIBIT_BLEND_EXIT, {
                finishCallback = function()
                    PhaseManager:ExitPhaseFast(PhaseId.WeaponInfo)
                end
            })
            self:PlayAnimationOut()
            return
        end

        UIUtils.PlayAnimationAndToggleActive(self.view.expandNode.commonItemList.view.animationWrapper, false)
        self:Notify(MessageConst.WEAPON_EXHIBIT_PAGE_CHANGE, {
            pageType = UIConst.WEAPON_EXHIBIT_PAGE_TYPE.OVERVIEW,
        })
    end)
    self.view.upgradeNode.costItemList.onUpdateCell:AddListener(function(object, csIndex)
        self:_RefreshBasicCostItemCell(object, csIndex)
    end)

    self.view.breakNode.costItemList.onUpdateCell:AddListener(function(object, csIndex)
        self:_RefreshBreakCostItemCell(object, csIndex)
    end)

    self.view.upgradeNode.btnAutoFill.onClick:AddListener(function()
        self:_ResetItemInfoCount(self.m_upgradeItemInfoList)
        self:_AutoFillCostItem()
    end)
    self.view.upgradeNode.btnReset.onClick:AddListener(function()
        self:_ResetCostItem()
        self:_RefreshBottomCostItem()
        self:_RefreshUpgradeInformation(self.m_weaponInfo.weaponInstId, self.m_weaponInfo.weaponTemplateId)
        self.view.upgradeNode.costItemList:ScrollToIndex(0, true)
    end)
    self.view.addBtn.onClick:AddListener(function()
        self:_AddOneLevel()
    end)
    self.view.reduceBtn.onClick:AddListener(function()
        self:_SubOneLevel()
    end)
    self.view.expandNode.emptyButton.onClick:AddListener(function()
        self:_ToggleExpandNode(false)
    end)

    local onUpdateSelectionCell = function(index, option, isSelected)
        option:SetText(Language[self.m_selectionOptions[LuaIndex(index)].nameKey])
    end

    local onSelectCell = function(index)
        local selectionOption = self.m_selectionOptions[LuaIndex(index)]
        self.m_maxFillWeaponQuality = selectionOption.maxFillWeaponQuality

        WeaponExhibitUpgradeCtrl.s_materialFilterSelectionIndex = index
    end
    self.view.upgradeNode.selectionDropDown.dropdown:Init(onUpdateSelectionCell, onSelectCell)
    self.view.walletBarPlaceholder:InitWalletBarPlaceholder(UIConst.INVENTORY_MONEY_IDS)
end

WeaponExhibitUpgradeCtrl.m_materialsDecreaseInputGroupId = HL.Field(HL.Number) << -1

WeaponExhibitUpgradeCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    self:BindInputPlayerAction("char_info_weapon_select_materials", function()
        self.m_forceFocusListIndex = -1
        self:_ToggleExpandNode(true)
    end, self.view.upgradeNode.inputGroup.groupId)
    self.m_materialsDecreaseInputGroupId = InputManagerInst:CreateGroup(self.view.expandNodeInputGroup.groupId)
    self:BindInputPlayerAction("char_info_weapon_materials_decrease_count", function()
        self:_OnItemBtnMinusClicked(self.m_naviMaterialCell, self.m_naviMaterialItemInfo)
    end, self.m_materialsDecreaseInputGroupId)
    self:BindInputPlayerAction("char_info_weapon_materials_decrease_count_press", function()
        if self.m_naviMaterialItemInfo == nil then
            return
        end
        self:_StartFillTimer(self.m_naviMaterialItemInfo, -10)
    end, self.m_materialsDecreaseInputGroupId)
    self:BindInputPlayerAction("char_info_weapon_materials_decrease_count_release", function()
        if self.m_naviMaterialItemInfo == nil then
            return
        end
        self:_StopFillTimer(self.m_naviMaterialItemInfo)
    end, self.m_materialsDecreaseInputGroupId)
    self:BindInputPlayerAction("char_info_weapon_materials_increase_count_press", function()
        if self.m_naviMaterialItemInfo == nil then
            return
        end
        self:_StartFillTimer(self.m_naviMaterialItemInfo, 10)
    end, self.view.expandNodeInputGroup.groupId)
    self:BindInputPlayerAction("char_info_weapon_materials_increase_count_release", function()
        if self.m_naviMaterialItemInfo == nil then
            return
        end
        self:_StopFillTimer(self.m_naviMaterialItemInfo)
    end, self.view.expandNodeInputGroup.groupId)
    self.view.breakNode.naviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
end

WeaponExhibitUpgradeCtrl._InitUpgradeCache = HL.Method(HL.Number, HL.String) << function(self, weaponInstId, weaponTemplateId)
    local _, weaponCfg = Tables.weaponBasicTable:TryGetValue(weaponTemplateId)
    if not weaponCfg then
        logger.error("CharInfoUtils->Can't get weapon basic info, templateId: " .. weaponTemplateId)
        return
    end

    local levelUpTemplateId = weaponCfg.levelTemplateId
    local _, levelUpCfg = Tables.weaponUpgradeTemplateTable:TryGetValue(levelUpTemplateId)
    if not levelUpCfg then
        logger.error("CharInfoUtils->Can't get weapon level up info, levelUpTemplateId: " .. levelUpTemplateId)
        return
    end
    local curLevel = CharInfoUtils.getWeaponByInstId(weaponInstId).weaponLv
    local maxLevel = weaponCfg.maxLv

    local startExp = levelUpCfg.list[CSIndex(curLevel)].lvUpExp
    local startGold = levelUpCfg.list[CSIndex(curLevel)].lvUpGold

    local level2RequireExpDict = {}
    local level2RequireGoldDict = {}
    for levelIndex = curLevel + 1, maxLevel do
        level2RequireExpDict[levelIndex] = startExp
        level2RequireGoldDict[levelIndex] = startGold
        startExp = startExp + levelUpCfg.list[CSIndex(levelIndex)].lvUpExp
        startGold = startGold + levelUpCfg.list[CSIndex(levelIndex)].lvUpGold
    end

    self.m_selectionOptions = SELECTION_OPTIONS
    self.m_level2RequireExpDict = level2RequireExpDict
    self.m_level2RequireGoldDict = level2RequireGoldDict

    self.m_costItemInfoList = {}
    self.m_costItemInfoDict = {}
end

WeaponExhibitUpgradeCtrl._RefreshBreakPanel = HL.Method(HL.Number, HL.String) << function(self, weaponInstId, weaponTemplateId)
    local weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponTemplateId, weaponInstId)

    self.view.breakWeaponInfo:InitBreakWeaponInfo({
        weaponExhibitInfo = weaponExhibitInfo,
    })

    local weaponInst = weaponExhibitInfo.weaponInst
    local fromBreakthroughCfg = CharInfoUtils.getWeaponBreakthroughInfo(weaponInstId, weaponInst.breakthroughLv + 1)
    if fromBreakthroughCfg == nil then
        return
    end
    self:_RefreshBreakItemList(fromBreakthroughCfg)
    self:_RefreshBreakthroughButton(fromBreakthroughCfg.breakthroughGold)
end

WeaponExhibitUpgradeCtrl._RefreshUpgradePanel = HL.Method(HL.Number, HL.String, HL.Opt(HL.Table)) << function(self, weaponInstId, weaponTemplateId, costItemInfoDict)
    self.m_costItemInfoDict = costItemInfoDict or {}
    self:_RefreshBottomCostItem()
    self:_ResetCostItem(costItemInfoDict)
    self:_RefreshUpgradeInformation(weaponInstId, weaponTemplateId)
end

WeaponExhibitUpgradeCtrl._RefreshSelectionDropdown = HL.Method() << function(self)
    local optionCount = #self.m_selectionOptions
    local lastSelectKey = WeaponExhibitUpgradeCtrl.s_materialFilterSelectionIndex
    if lastSelectKey < 0 or lastSelectKey >= optionCount then
        lastSelectKey = optionCount - 1
    end
    self.view.upgradeNode.selectionDropDown.dropdown:Refresh(optionCount, lastSelectKey)
end

WeaponExhibitUpgradeCtrl._RefreshTitle = HL.Method(HL.String, HL.Boolean) << function(self, weaponTemplateId, isBreakthrough)
    local _, itemCfg = Tables.itemTable:TryGetValue(weaponTemplateId)
    if not itemCfg then
        logger.error("WeaponExhibitUpgradeCtrl->Can't find weaponItem ID: " .. weaponTemplateId)
        return
    end

    local title = isBreakthrough and Language.LUA_WEAPON_EXHIBIT_BREAKTHROUGH_TITLE or Language.LUA_WEAPON_EXHIBIT_UPGRADE_TITLE
    self.view.title.text = string.format(title, itemCfg.name)
end


WeaponExhibitUpgradeCtrl._RefreshBottomCostItem = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipGraduallyShow)
    local costItemInfoList = self:_CollectCostItemInfoList()
    local costCellCount = math.max(#costItemInfoList, UIConst.WEAPON_EXHIBIT_UPGRADE_MIN_SLOT_NUM)

    self.m_costItemInfoList = costItemInfoList
    self.view.upgradeNode.costItemList:UpdateCount(costCellCount)
    self.view.upgradeNode.consumeNumber.text = #costItemInfoList
    self.view.upgradeNode.maxNumber.text = "/" .. UIConst.WEAPON_EXHIBIT_UPGRADE_ITEM_MAX_COUNT
    if DeviceInfo.usingController then
        self.view.upgradeNode.controllerScrollHint.gameObject:SetActive(#costItemInfoList > 4)
    end
end

WeaponExhibitUpgradeCtrl._RefreshBreakItemList = HL.Method(HL.Userdata) << function(self, breakthroughCfg)
    local breakItemList = breakthroughCfg.breakItemList

    self.m_breakItemList = breakthroughCfg.breakItemList
    self.view.breakNode.costItemList:UpdateCount(#breakItemList)
end

WeaponExhibitUpgradeCtrl._RefreshUpgradeInformation = HL.Method(HL.Number, HL.String, HL.Opt(HL.Boolean)) << function(
    self, weaponInstId, weaponTemplateId, inUpgradeTransition)
    local weaponExhibitInfo = CharInfoUtils.getWeaponExhibitBasicInfo(weaponTemplateId, weaponInstId)

    self.m_weaponExhibitInfo = weaponExhibitInfo

    local weaponInst = CharInfoUtils.getWeaponByInstId(weaponInstId)
    local curExp = weaponInst.exp
    local curLevel = weaponInst.weaponLv

    local curGenerateExp = self:_CalcGenerateExp()
    local curExpSum = curGenerateExp + curExp
    local targetLevel, expLeft = self:_CalcLevelByExp(curExpSum, curLevel, weaponExhibitInfo.stageLv)
    local _, weaponCfg = Tables.weaponBasicTable:TryGetValue(weaponTemplateId)
    local levelUpTemplateId = weaponCfg.levelTemplateId
    local _, levelUpCfg = Tables.weaponUpgradeTemplateTable:TryGetValue(levelUpTemplateId)
    local nextLvExp = levelUpCfg.list[CSIndex(curLevel)].lvUpExp

    self.m_curTargetLv = targetLevel
    self.m_curGenerateExp = curGenerateExp

    self.view.upgradeWeaponInfo:InitUpgradeWeaponInfo({
        weaponExhibitInfo = weaponExhibitInfo,
        curLevel = curLevel,
        targetLevel = targetLevel,
        curExp = curExp,
        nextLvExp = nextLvExp,
        addExp = curGenerateExp,
        inUpgradeTransition = inUpgradeTransition,
    })

    local requireGold = targetLevel == curLevel and 0 or self.m_level2RequireGoldDict[targetLevel]
    self:_RefreshUpgradeBtn(curGenerateExp, requireGold)
    self:_UpdateAddAndReduceButtonState()
end

WeaponExhibitUpgradeCtrl._RefreshUpgradeBtn = HL.Method(HL.Number, HL.Number) << function(self, generateExp, requireGold)
    local curGold = Utils.getItemCount(UIConst.INVENTORY_MONEY_IDS[1], true)
    local goldEnough = curGold >= requireGold

    self.view.goldCostNumber.text = UIUtils.setCountColor(requireGold, not goldEnough)
    self.view.btnLevelUp.text = Language.LUA_WEAPON_EXHIBIT_UPGRADE
end

WeaponExhibitUpgradeCtrl._RefreshBreakthroughButton = HL.Method(HL.Number) << function(self, requireGold)
    local curGold = Utils.getItemCount(UIConst.INVENTORY_MONEY_IDS[1], true)
    local goldEnough = curGold >= requireGold

    self.view.goldCostNumber.text = UIUtils.setCountColor(requireGold, not goldEnough)
    self.view.btnLevelUp.text = Language.LUA_WEAPON_EXHIBIT_BREAKTHROUGH

end

WeaponExhibitUpgradeCtrl._RefreshBasicCostItemCell = HL.Method(HL.Userdata, HL.Number) << function(self, object, index)
    local cell = self.m_getBasicCostItemCell(object)
    local costItemInfo = self.m_costItemInfoList[LuaIndex(index)]

    self:_RefreshCostItemCell(cell, costItemInfo, function()
        self.m_forceFocusListIndex = costItemInfo and costItemInfo.listShowIndex or -1

        self:_ToggleExpandNode(true)
    end)
end

WeaponExhibitUpgradeCtrl._RefreshBreakCostItemCell = HL.Method(HL.Userdata, HL.Number) << function(self, object, index)
    local cell = self.m_getBreakCostItemCell(object)

    local itemInfo = self.m_breakItemList[index]
    local itemId = itemInfo.id
    local inventoryCount = Utils.getItemCount(itemId, true)

    cell.itemBlack:InitItem({
        id = itemId,
        count = itemInfo.count,
    }, true)
    if DeviceInfo.usingController then
        cell.itemBlack:SetExtraInfo({
            isSideTips = true
        })
    end
    if inventoryCount < itemInfo.count then
        cell.itemBlack:SetItemTipsJumpExtraArgs({
            expectedRewardItemId = itemId,
            expectedRewardItemCount = itemInfo.count - inventoryCount,
        })
    end

    cell.storageNode:InitStorageNode(inventoryCount, itemInfo.count, true)
end

WeaponExhibitUpgradeCtrl._OnClickExpandCostItemCell = HL.Method(HL.Table, HL.Boolean) << function(self, costItemInfo, realClick)
    if not realClick then
        return
    end

    if self.m_lastClickItemId ~= costItemInfo.indexId and realClick and not DeviceInfo.usingController then
        Notify(MessageConst.SHOW_ITEM_TIPS, {
            transform = self.view.expandNode.tipPos,
            itemId = costItemInfo.itemCfg.id,
            instId = costItemInfo.isWeapon and costItemInfo.indexId or 0,
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

WeaponExhibitUpgradeCtrl._RefreshCostItemCell = HL.Method(HL.Any, HL.Table, HL.Any) << function(self, cell, costItemInfo, onClickAction)
    local isEmpty = not costItemInfo

    cell.itemBlack.gameObject:SetActive(not isEmpty)
    cell.emptyNode.gameObject:SetActive(isEmpty)
    cell.addNode.gameObject:SetActive(isEmpty)
    cell.selectNode.gameObject:SetActive(false)
    cell.multiSelectNode.gameObject:SetActive(false)
    cell.button.onClick:RemoveAllListeners()
    cell.button.onClick:AddListener(function()
        onClickAction()
    end)

    cell.btnMinus.onClick:RemoveAllListeners()
    cell.btnMinus.onClick:AddListener(function()
        self:_RemoveFromCostItemDict(costItemInfo, 1)
    end)
    cell.btnMinus.onPressStart:RemoveAllListeners()
    cell.btnMinus.onPressStart:AddListener(function()
        self:_StartFillTimer(costItemInfo, -10)
    end)
    cell.btnMinus.onPressEnd:RemoveAllListeners()
    cell.btnMinus.onPressEnd:AddListener(function()
        self:_StopFillTimer(costItemInfo)
    end)


    if isEmpty then
        return
    end

    local id = costItemInfo.indexId
    local isMarkCost = self.m_costItemInfoDict and self.m_costItemInfoDict[id] ~= nil
    local isWeapon = costItemInfo.isWeapon
    cell.btnMinus.gameObject:SetActive(isMarkCost)

    cell.itemBlack:InitItem({
        id = costItemInfo.itemCfg.id,
        instId = isWeapon and costItemInfo.indexId or 0,

    }, true)
    cell.itemBlack:UpdateRedDot()

    cell.itemBlack.view.button.onClick:RemoveAllListeners()
    cell.itemBlack.view.button.onClick:AddListener(function()
        onClickAction()
    end)


    if costItemInfo.isWeapon then
        cell.selectNode.gameObject:SetActive(isMarkCost)
        cell.multiSelectNode.gameObject:SetActive(false)
    else
        cell.selectNode.gameObject:SetActive(isMarkCost)
        cell.multiSelectNode.gameObject:SetActive(true)
        cell.selectCount.text = costItemInfo.count
    end
end

WeaponExhibitUpgradeCtrl._ToggleExpandNode = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, isExpand, closeAll)
    self.m_itemFillCor = self:_ClearCoroutine(self.m_itemFillCor)

    if self.view.expandNode.commonItemList:IsAnyItemSelecting() then
        self.view.expandNode.commonItemList:SetSelectedIndex(0, false, true)
    end

    self.m_lastClickItemId = nil
    self.m_lastClickItemInfo = nil

    if not isExpand and UIManager:IsShow(PanelId.ItemTips) then
        Notify(MessageConst.HIDE_ITEM_TIPS)
        if not closeAll and not DeviceInfo.usingController then
            return
        end
    end

    if isExpand then
        self.view.expandNode.gameObject:SetActive(true)
        UIUtils.PlayAnimationAndToggleActive(self.view.expandNode.commonItemList.view.animationWrapper, true)
        if DeviceInfo.usingController then
            local selectedIndex = 1
            if self.m_arg.stateArg and self.m_arg.stateArg.curSelectedIndex then
                selectedIndex = self.m_arg.stateArg.curSelectedIndex
            end
            self.view.expandNode.commonItemList:PlayGraduallyShow(selectedIndex, false)
        else
            self.view.expandNode.commonItemList:PlayGraduallyShow()
        end
    else
        UIUtils.PlayAnimationAndToggleActive(self.view.expandNode.commonItemList.view.animationWrapper, false, function()
            self.view.expandNode.gameObject:SetActive(false)
        end)
    end

    InputManagerInst:ToggleGroup(self.view.upgradeNode.inputGroup.groupId, not isExpand)
    if DeviceInfo.usingController then
        InputManagerInst:ToggleGroup(self.view.btnLevelUp.groupId, not isExpand)
    else
        InputManagerInst:ToggleGroup(self.view.btnLevelUp.groupId, true)
    end

    local audioEventKey = isExpand and "au_ui_menu_side_open" or "au_ui_menu_side_close"
    AudioManager.PostEvent(audioEventKey)
end

WeaponExhibitUpgradeCtrl._CalcGenerateExp = HL.Method().Return(HL.Number) << function(self)
    local expSum = 0
    for _, costItemInfo in pairs(self.m_costItemInfoDict) do
        local itemExp = costItemInfo.generateExp
        expSum = expSum + itemExp * costItemInfo.count
    end
    return expSum
end

WeaponExhibitUpgradeCtrl._TryModifyCostItemDict = HL.Method(HL.Table, HL.Number) << function(self, itemInfo, count)
    local costItemInfoDict = self.m_costItemInfoDict
    local expInfo = self.m_weaponExhibitInfo
    local targetLv = self.m_curTargetLv
    if targetLv >= expInfo.stageLv then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_UPGRADE_MAX_LEVEL)
        return
    end

    if lume.count(costItemInfoDict) >= UIConst.WEAPON_EXHIBIT_UPGRADE_ITEM_MAX_COUNT then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_UPGRADE_MAX_ITEM_COUNT)
        return
    end
    if itemInfo.isWeapon then
        local isLock = GameInstance.player.inventory:IsItemLocked(Utils.getCurrentScope(), itemInfo.itemInst.templateId, itemInfo.itemInst.instId)
        if isLock then
            self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_UPGRADE_WEAPON_LOCKED)
            return
        end
    end

    if itemInfo.isWeapon then
        itemInfo.count = 1
    else

        itemInfo.count = math.min(itemInfo.count + count, itemInfo.inventoryCount)
    end

    local indexId = itemInfo.indexId
    costItemInfoDict[indexId] = itemInfo
end

WeaponExhibitUpgradeCtrl._AddIntoCostItemDict = HL.Method(HL.Table, HL.Number) << function(self, itemInfo, count)
    local weaponInfo = self.m_weaponInfo

    self:_TryAddIntoCostItemDict(itemInfo, count)

    self:_RefreshBottomCostItem(true)
    self.view.expandNode.commonItemList:RefreshCellById(itemInfo.indexId)
    self:_RefreshUpgradeInformation(weaponInfo.weaponInstId, weaponInfo.weaponTemplateId)
end

WeaponExhibitUpgradeCtrl._TryAddIntoCostItemDict = HL.Method(HL.Table, HL.Number) << function(self, itemInfo, count)
    local curCount = itemInfo.count or 0
    local nextCount = curCount + count
    local costItemInfoDict = self.m_costItemInfoDict


    if count <= 0 then
        return nextCount
    end

    if itemInfo.isWeapon then
        local isLock = GameInstance.player.inventory:IsItemLocked(Utils.getCurrentScope(), itemInfo.itemInst.templateId, itemInfo.itemInst.instId)
        if isLock then
            self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_UPGRADE_WEAPON_LOCKED)
            return
        end

        local weaponInst = itemInfo.itemInst
        local hasGemEquipped = weaponInst.attachedGemInstId > 0
        if hasGemEquipped then
            local gemInst = CharInfoUtils.getGemByInstId(weaponInst.attachedGemInstId)
            local gemItemCfg = Tables.itemTable[gemInst.templateId]
            Notify(MessageConst.SHOW_POP_UP, {
                content = string.format(Language.LUA_WEAPON_EXHIBIT_UPGRADE_GEM_EQUIPPED, gemItemCfg.name),
                onConfirm = function()
                    GameInstance.player.charBag:DetachGem(weaponInst.instId)
                end,
                onCancel = function()
                end
            })
            return
        end

        if lume.count(costItemInfoDict) >= UIConst.WEAPON_EXHIBIT_UPGRADE_ITEM_MAX_COUNT then
            self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_UPGRADE_MAX_INGREDIENT)
            return
        end
    else
        if lume.count(costItemInfoDict) >= UIConst.WEAPON_EXHIBIT_UPGRADE_ITEM_MAX_COUNT and costItemInfoDict[itemInfo.indexId] == nil then
            self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_UPGRADE_MAX_INGREDIENT)
            return
        end
    end


    local weaponInstId = self.m_weaponInfo.weaponInstId
    local weaponInst = CharInfoUtils.getWeaponByInstId(weaponInstId)
    local curExp = weaponInst.exp
    local expInfo = self.m_weaponExhibitInfo

    local curGenerateExp = self:_CalcGenerateExp()
    local stageExp = self.m_level2RequireExpDict[expInfo.stageLv] or 0
    local expRequire = stageExp - curExp
    local itemExp = itemInfo.generateExp

    if (curGenerateExp + (itemExp * count)) > expRequire then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_UPGRADE_MAX_LEVEL)

        local needExp = math.max(0, expRequire - curGenerateExp)
        nextCount = curCount + math.ceil(needExp / itemExp)
    end

    if itemInfo.inventoryCount ~= 0 and nextCount >= itemInfo.inventoryCount then
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


WeaponExhibitUpgradeCtrl._RemoveFromCostItemDict = HL.Method(HL.Table, HL.Number) << function(self, itemInfo, count)
    local costItemInfoDict = self.m_costItemInfoDict
    local weaponInfo = self.m_weaponInfo

    local id = itemInfo.indexId
    itemInfo.count  = lume.clamp(itemInfo.count - count, 0, itemInfo.inventoryCount)

    if costItemInfoDict[id] and itemInfo.count <= 0 then
        costItemInfoDict[id].count = 0
        costItemInfoDict[id] = nil
    end


    self:_RefreshBottomCostItem(true)
    self:_RefreshUpgradeInformation(weaponInfo.weaponInstId, weaponInfo.weaponTemplateId)
    self.view.expandNode.commonItemList:RefreshCellById(itemInfo.indexId)
end

WeaponExhibitUpgradeCtrl._CalcLevelByExp = HL.Method(HL.Number, HL.Number, HL.Number).Return(HL.Number, HL.Number) << function(self, addExp, curLevel, maxLevel)
    local level2RequireExpDict = self.m_level2RequireExpDict

    if level2RequireExpDict[maxLevel] and addExp >= level2RequireExpDict[maxLevel] then
        return maxLevel, addExp - level2RequireExpDict[maxLevel]
    end

    for level = maxLevel, curLevel + 1, -1 do
        local requireExp = level2RequireExpDict[level]
        if addExp >= requireExp then
            return level, addExp - requireExp
        end
    end

    return curLevel, addExp
end

WeaponExhibitUpgradeCtrl._AutoFillCostItem = HL.Method() << function(self)
    local weaponInfo = self.m_weaponInfo

    self:_TryFillCostItem()

    self:_RefreshBottomCostItem()
    self.view.expandNode.commonItemList:RefreshAllCells()
    self:_RefreshUpgradeInformation(weaponInfo.weaponInstId, weaponInfo.weaponTemplateId)
end

WeaponExhibitUpgradeCtrl._TryFillCostItem = HL.Method() << function(self)
    local upgradeItemInfoList = self.m_upgradeItemInfoList
    local weaponExhibitInfo = self.m_weaponExhibitInfo
    local stageExp = self.m_level2RequireExpDict[weaponExhibitInfo.stageLv] or 0
    local targetExp = stageExp - weaponExhibitInfo.weaponInst.exp
    local maxFillWeaponQuality = self.m_maxFillWeaponQuality
    local costItemInfoDict = self:_AutoFillCostItemDict(targetExp, maxFillWeaponQuality, upgradeItemInfoList)

    for key, costItemInfo in pairs(costItemInfoDict) do
        if costItemInfo.count <= 0 then
            costItemInfoDict[key] = nil
        end
    end

    self.m_costItemInfoDict = costItemInfoDict

    if lume.count(costItemInfoDict) == 0 and targetLv ~= weaponExhibitInfo.curLv then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_UPGRADE_NO_ITEM)
    end
end

WeaponExhibitUpgradeCtrl._AutoFillCostItemDict = HL.Method(HL.Number, HL.Number, HL.Table).Return(HL.Table)
    << function(self, targetExp, maxQuality, upgradeItemInfoList)
    local costItemInfoDict = {}
    local expLeft = targetExp
    local calcAssistInfoList = lume.sort(upgradeItemInfoList, WeaponUtils.upgradeItemSortComp)

    for _, itemInfo in ipairs(calcAssistInfoList) do
        itemInfo.addCount = 0
    end

    local ingredientList = {}

    for _, itemInfo in ipairs(calcAssistInfoList) do
        local singleExp = itemInfo.generateExp
        local curCount = itemInfo.count or 0
        local maxAddCount = itemInfo.inventoryCount - curCount
        local generateExpTotal = singleExp * maxAddCount

        if expLeft <= 0 then
            break
        end

        if lume.count(costItemInfoDict) >= UIConst.WEAPON_EXHIBIT_UPGRADE_ITEM_MAX_COUNT then

            return costItemInfoDict
        end

        local isWeapon = itemInfo.isWeapon
        local isLockWeapon = itemInfo.itemInst and itemInfo.itemInst.isLocked
        local weaponEquippedWithGem = false
        if isWeapon then
            local weaponInst = itemInfo.itemInst
            weaponEquippedWithGem = weaponInst.attachedGemInstId > 0
        end

        if (not isLockWeapon) and (not weaponEquippedWithGem) then
            if itemInfo.itemCfg.rarity <= maxQuality then
                if generateExpTotal > expLeft then
                    local addCount = math.min(math.ceil(expLeft / singleExp), maxAddCount)
                    itemInfo.addCount = addCount
                    itemInfo.count = addCount + curCount
                    expLeft = expLeft - singleExp * addCount
                else
                    itemInfo.addCount = maxAddCount
                    itemInfo.count = maxAddCount + curCount
                    expLeft = expLeft - singleExp * maxAddCount
                end

                if itemInfo.addCount and itemInfo.addCount > 0 then
                    costItemInfoDict[itemInfo.indexId] = itemInfo
                    table.insert(ingredientList, itemInfo)
                end
            end
        end
    end


    if expLeft == 0 then
        return costItemInfoDict
    end


    ingredientList = lume.sort(ingredientList, WeaponUtils.ingredientItemSortComp)

    for _, itemInfo in ipairs(ingredientList) do
        local singleExp = itemInfo.generateExp
        local addCount = itemInfo.addCount
        local addedExpTotal = singleExp * addCount


        if expLeft + singleExp < 0 then

            if expLeft + addedExpTotal > 0 then

                if itemInfo.isWeapon then
                    break
                else
                    if addCount > 1 then
                        local canPopCount = math.floor(math.abs(expLeft) / singleExp)
                        itemInfo.addCount = itemInfo.addCount - canPopCount
                        itemInfo.count = itemInfo.count - canPopCount
                        expLeft = expLeft + singleExp * canPopCount
                    end
                end
            else
                expLeft = expLeft + addedExpTotal
                itemInfo.addCount = 0
                itemInfo.count = itemInfo.count - addCount
                costItemInfoDict[itemInfo.indexId] = nil
            end
        end
    end

    return costItemInfoDict
end

WeaponExhibitUpgradeCtrl._RefreshWeaponCellAddOn = HL.Method(HL.Any, HL.Table) << function(self, cell, itemInfo)
    cell.selectNode.gameObject:SetActive(itemInfo.count ~= nil and itemInfo.count > 0)
    cell.multiSelectNode.gameObject:SetActive(false)
    cell.selectCount.text = itemInfo.count
end

WeaponExhibitUpgradeCtrl._RefreshItemCellAddOn = HL.Method(HL.Any, HL.Table) << function(self, cell, itemInfo)
    cell.selectNode.gameObject:SetActive(itemInfo.count ~= nil and itemInfo.count > 0)

    cell.multiSelectNode.gameObject:SetActive(true)
    cell.selectCount.text = itemInfo.count
end

WeaponExhibitUpgradeCtrl._RefreshCellAddOn = HL.Method(HL.Any, HL.Table) << function(self, cell, itemInfo)
    if itemInfo.itemInst ~= nil then
        self:_RefreshWeaponCellAddOn(cell, itemInfo)
    else
        self:_RefreshItemCellAddOn(cell, itemInfo)
    end

    cell.btnMinus.onClick:RemoveAllListeners()
    cell.btnMinus.onClick:AddListener(function()
        self:_OnItemBtnMinusClicked(cell, itemInfo)
    end)

    cell.btnMinus.onPressStart:AddListener(function()
        self:_StartFillTimer(itemInfo, -10)
    end)
    cell.btnMinus.onPressEnd:AddListener(function()
        self:_StopFillTimer(itemInfo)
    end)

    if cell == self.m_naviMaterialCell then
        local enableGroup = itemInfo.count and itemInfo.count > 0
        InputManagerInst:ToggleGroup(self.m_materialsDecreaseInputGroupId, enableGroup)
        if not enableGroup then
            self:_StopFillTimer(itemInfo)
        end
    end
end

WeaponExhibitUpgradeCtrl._OnItemBtnMinusClicked = HL.Method(HL.Any, HL.Table) << function(self, cell, itemInfo)
    self:_RemoveFromCostItemDict(itemInfo, 1)
    self:_RefreshCellAddOn(cell, itemInfo)
end

WeaponExhibitUpgradeCtrl._OnItemIsNaviTargetChanged = HL.Method(HL.Any, HL.Table, HL.Boolean) << function(self, cell, itemInfo, isTarget)
    if isTarget and self.view.expandNode.gameObject.activeInHierarchy then
        InputManagerInst:ToggleGroup(self.m_materialsDecreaseInputGroupId, itemInfo.count and itemInfo.count > 0)
        self.m_naviMaterialCell = cell
        self.m_naviMaterialItemInfo = itemInfo

        Notify(MessageConst.SHOW_ITEM_TIPS, {
            transform = self.view.expandNode.tipPos,
            itemId = itemInfo.itemCfg.id,
            instId = itemInfo.isWeapon and itemInfo.indexId or 0,
            noJump = true,
            autoClose = false,
            isSideTips = true,
            posType = UIConst.UI_TIPS_POS_TYPE.RightMid,
        })
    end
end

WeaponExhibitUpgradeCtrl._ResetCostItem = HL.Method(HL.Opt(HL.Table)) << function(self, costItemInfoDict)
    self.m_costItemInfoDict = costItemInfoDict or {}
    self.view.expandNode.commonItemList:InitCommonItemList({
        listType = UIConst.COMMON_ITEM_LIST_TYPE.WEAPON_EXHIBIT_UPGRADE,
        onlyGenerateData = self.view.expandNode.commonItemList.view.activeInHierarchy,
        refreshItemAddOn = function(cell, itemInfo)
            self:_RefreshCellAddOn(cell, itemInfo)
        end,
        onClickItem = function(args)
            local itemInfo = args.itemInfo
            local curCell = args.curCell
            local nextCell = args.nextCell

            self:_OnClickExpandCostItemCell(itemInfo, args.realClick)

            if curCell then
            end

            if nextCell then
                self:_RefreshCellAddOn(nextCell, itemInfo)
            end
        end,
        onPressItem = function(itemInfo)
            self:_StartFillTimer(itemInfo, 10)
        end,
        onReleaseItem = function(itemInfo)
            self:_StopFillTimer(itemInfo)
        end,
        filter_not_equipped = true,
        filter_not_instId = self.m_weaponInfo.weaponInstId,
        onItemIsNaviTargetChanged = function(cell, itemInfo, isTarget)
            self:_OnItemIsNaviTargetChanged(cell, itemInfo, isTarget)
        end,
        clickItemControllerHintText = Language.ui_weapon_controller_Increase_materials,
        skipGraduallyShow = true,
        curSortCSIndex = self.m_arg.stateArg and self.m_arg.stateArg.curSortCSIndex,
        curSortIsIncremental = self.m_arg.stateArg and self.m_arg.stateArg.curSortIsIncremental,
        selectedTags = self.m_arg.stateArg and self.m_arg.stateArg.selectedTags,
    })
    if self.m_arg.stateArg and self.m_arg.stateArg.curSelectedIndex and DeviceInfo.usingController then
        self.view.expandNode.commonItemList:SetSelectedIndex(self.m_arg.stateArg.curSelectedIndex, false)
    end

    self.m_upgradeItemInfoList = self.view.expandNode.commonItemList.m_itemInfoList


    for _, itemInfo in pairs(self.m_upgradeItemInfoList) do
        if itemInfo.generateExp == nil then
            itemInfo.generateExp = WeaponUtils.CalcItemExp(itemInfo.itemCfg, itemInfo.itemInst)
        end
    end
end

WeaponExhibitUpgradeCtrl._StartFillTimer = HL.Method(HL.Table, HL.Number) << function(self, itemInfo, count)
    self.m_itemFillCor = self:_ClearCoroutine(self.m_itemFillCor)
    self.m_itemFillCor = self:_StartCoroutine(function()
        while true do
            coroutine.wait(0.3)
            if count > 0 then
                self:_AddIntoCostItemDict(itemInfo, count)
            else
                self:_RemoveFromCostItemDict(itemInfo, -count)
            end
        end
    end)
end

WeaponExhibitUpgradeCtrl._StopFillTimer = HL.Method(HL.Table) << function(self, itemInfo)
    self.m_itemFillCor = self:_ClearCoroutine(self.m_itemFillCor)
end

WeaponExhibitUpgradeCtrl._CollectCostItemInfoList = HL.Method().Return(HL.Table) << function(self)
    local costItemInfoList = {}
    for _, costItemInfo in pairs(self.m_costItemInfoDict) do
        table.insert(costItemInfoList, costItemInfo)
    end

    costItemInfoList = lume.sort(costItemInfoList, function(a,b)
        if a.isWeapon ~= b.isWeapon then
            return a.isWeapon
        end

        if a.isWeapon and b.isWeapon then
            local aWeaponLv = a.itemInst.weaponLv
            local bWeaponLv = b.itemInst.weaponLv
            if a.isLocked ~= b.isLocked then
                return a.isLocked
            end

            if aWeaponLv ~= bWeaponLv then
                return aWeaponLv > bWeaponLv
            end
        end


        if a.itemCfg.rarity ~= b.itemCfg.rarity then
            return a.itemCfg.rarity > b.itemCfg.rarity
        end

        return a.itemCfg.id > b.itemCfg.id
    end)

    return costItemInfoList
end

WeaponExhibitUpgradeCtrl._ClickWeaponUpgradeButton = HL.Method() << function(self)
    if self.m_curGenerateExp <= 0 then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_UPGRADE_NONE_ITEM)
        return
    end

    local weaponExhibitInfo = self.m_weaponExhibitInfo
    local weaponInst = weaponExhibitInfo.weaponInst

    local curLv = weaponInst.weaponLv
    local targetLv = self.m_curTargetLv

    if curLv >= weaponExhibitInfo.stageLv then
        return
    end

    local requireGold = (targetLv == curLv) and 0 or self.m_level2RequireGoldDict[targetLv]
    local curGold = Utils.getItemCount(UIConst.INVENTORY_MONEY_IDS[1], true)
    if curGold < requireGold then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_GOLD_NOT_ENOUGH)
        return
    end

    local weaponInfo = self.m_weaponInfo
    local costItemInfoDict = self.m_costItemInfoDict
    local costWeaponInstIds = {}
    local costItemId2Count = {}
    for _, costItemInfo in pairs(costItemInfoDict) do
        if costItemInfo.isWeapon then
            local isLocked = GameInstance.player.inventory:IsItemLocked(Utils.getCurrentScope(), costItemInfo.itemInst.templateId, costItemInfo.itemInst.instId)
            if isLocked then
                self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_UPGRADE_CONTAIN_WEAPON_LOCKED)
                return
            end
            table.insert(costWeaponInstIds, costItemInfo.itemInst.instId)
        else
            costItemId2Count[costItemInfo.itemCfg.id] = costItemInfo.count
        end
    end
    GameInstance.player.charBag:AddWeaponExp(weaponInfo.weaponInstId, costItemId2Count, costWeaponInstIds)
end

WeaponExhibitUpgradeCtrl._ClickWeaponBreakButton = HL.Method() << function(self)
    local exhibitInfo = self.m_weaponExhibitInfo
    local expInfo = CharInfoUtils.getWeaponExpInfo(exhibitInfo.weaponInst.instId)

    if expInfo.curLv < expInfo.stageLv then
        return
    end

    local breakItemList = self.m_breakItemList
    if breakItemList == nil then
        return
    end



    for i, itemInfo in pairs(breakItemList) do
        local itemId = itemInfo.id
        local itemCount = Utils.getItemCount(itemId, true)
        if itemCount < itemInfo.count then
            self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_BREAK_NOT_ENOUGH_ITEM)
            return
        end
    end

    local weaponInst = exhibitInfo.weaponInst
    local fromBreakthroughCfg = CharInfoUtils.getWeaponBreakthroughInfo(weaponInst.instId, weaponInst.breakthroughLv)
    local curGold = Utils.getItemCount(UIConst.INVENTORY_MONEY_IDS[1], true)
    if curGold < fromBreakthroughCfg.breakthroughGold then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_GOLD_NOT_ENOUGH)
        return
    end


    local weaponInfo = self.m_weaponInfo
    GameInstance.player.charBag:BreakthroughWeapon(weaponInfo.weaponInstId)
end

WeaponExhibitUpgradeCtrl.GetCurStateArg = HL.Method().Return(HL.Table) << function(self)
    local arg = {}
    arg.costItemInfoDict = self.m_costItemInfoDict
    arg.isExpand = self.view.expandNode.gameObject.activeSelf
    arg.selectedTags = self.view.expandNode.commonItemList.m_selectedTags
    arg.curSortCSIndex = self.view.expandNode.commonItemList.view.sortNode:GetCurSelectedIndex() - 1
    arg.curSortIsIncremental = self.view.expandNode.commonItemList.view.sortNode.isIncremental
    arg.curSelectedIndex = self.view.expandNode.commonItemList:IsAnyItemSelecting() and self.view.expandNode.commonItemList:GetCurSelectIndex() or nil
    return arg
end



WeaponExhibitUpgradeCtrl._UpdateAddAndReduceButtonState = HL.Method() << function(self)
    self.view.addBtnStateCtrl:SetState(self.m_curTargetLv < self.m_weaponExhibitInfo.stageLv
        and "normal" or "gray")
    self.view.reduceBtnStateCtrl:SetState(self.m_curTargetLv == self.m_weaponExhibitInfo.curLv and "gray" or "normal")
end

WeaponExhibitUpgradeCtrl._AddOneLevel = HL.Method() << function(self)
    local targetLv = self.m_curTargetLv + 1
    if targetLv > self.m_weaponExhibitInfo.stageLv then
        return
    end
    local exp = self.m_level2RequireExpDict[targetLv] or 0
    local targetExp = exp - (self.m_curGenerateExp + self.m_weaponExhibitInfo.weaponInst.exp)

    local costItemInfoDict = self:_AutoFillCostItemDict(targetExp, self.m_maxFillWeaponQuality, self.m_upgradeItemInfoList)

    for key, costItemInfo in pairs(costItemInfoDict) do
        if not self.m_costItemInfoDict[key] then
            self.m_costItemInfoDict[key] = costItemInfo
        end
    end

    if lume.count(costItemInfoDict) == 0 then
        self:Notify(MessageConst.SHOW_TOAST, Language.LUA_WEAPON_EXHIBIT_UPGRADE_NO_ITEM)
    end

    self:_RefreshBottomCostItem()
    self.view.expandNode.commonItemList:RefreshAllCells()
    self:_RefreshUpgradeInformation(self.m_weaponInfo.weaponInstId, self.m_weaponInfo.weaponTemplateId)
end

WeaponExhibitUpgradeCtrl._SubOneLevel = HL.Method() << function(self)
    if self.m_curTargetLv - self.m_weaponExhibitInfo.curLv <= 1 then
        self:_ResetCostItem()
        self:_RefreshBottomCostItem()
        self:_RefreshUpgradeInformation(self.m_weaponInfo.weaponInstId, self.m_weaponInfo.weaponTemplateId)
        return
    end
    local totalExp = self.m_curGenerateExp + self.m_weaponExhibitInfo.weaponInst.exp
    local curLv = self.m_curTargetLv
    local targetLv = curLv - 1
    local maxSubExp = totalExp - self.m_level2RequireExpDict[targetLv]
    local minSubExp = totalExp - self.m_level2RequireExpDict[self.m_curTargetLv]

    local ingredientList = {}
    for _, itemInfo in pairs(self.m_costItemInfoDict) do
        table.insert(ingredientList, itemInfo)
    end
    table.sort(ingredientList, WeaponUtils.expItemSortComp)


    local leftExpMax = maxSubExp
    local leftExpMin = minSubExp
    for _, itemInfo in ipairs(ingredientList) do
        local singleExp = itemInfo.generateExp
        if singleExp < leftExpMax then
            local subCount = math.floor(leftExpMax / singleExp)
            if subCount > itemInfo.count then
                subCount = itemInfo.count
                self.m_costItemInfoDict[itemInfo.indexId] = nil
            end
            itemInfo.count = itemInfo.count - subCount

            leftExpMax = leftExpMax - singleExp * subCount
            leftExpMin = leftExpMin - singleExp * subCount
        end

        if leftExpMin < 0 then
            break
        end
    end


    if leftExpMin > 0 then
        for i = #ingredientList, 1, -1 do
            local itemInfo = ingredientList[i]
            local singleExp = itemInfo.generateExp
            local subCount = math.ceil(leftExpMax / singleExp)
            if subCount > itemInfo.count then
                subCount = itemInfo.count
                self.m_costItemInfoDict[itemInfo.indexId] = nil
            end
            itemInfo.count = itemInfo.count - subCount
            leftExpMin = leftExpMin - singleExp * subCount

            if leftExpMin < 0 then
                break
            end
        end
    end


    local leftExp = 0
    for _, itemInfo in pairs(self.m_costItemInfoDict) do
        leftExp = leftExp + itemInfo.generateExp * itemInfo.count
    end
    if leftExp + self.m_weaponExhibitInfo.weaponInst.exp < self.m_level2RequireExpDict[self.m_weaponExhibitInfo.curLv + 1] then
        self.m_costItemInfoDict = {}
    end

    self:_RefreshBottomCostItem()
    self.view.expandNode.commonItemList:RefreshAllCells()
    self:_RefreshUpgradeInformation(self.m_weaponInfo.weaponInstId, self.m_weaponInfo.weaponTemplateId)
end

WeaponExhibitUpgradeCtrl._ResetItemInfoCount = HL.Method(HL.Table) << function(self, itemInfoList)
    if itemInfoList == nil then
        return
    end
    for _, itemInfo in ipairs(itemInfoList) do
        itemInfo.count = 0
        itemInfo.addCount = 0
    end
end

WeaponExhibitUpgradeCtrl._OnItemCountChanged = HL.Method(HL.Table) << function(self, args)
    if self.m_isBreakthrough and
        (self.m_upgradeEffectCor == nil or coroutine.status(self.m_upgradeEffectCor) == "dead") then
        self:_RefreshBreakPanel(self.m_weaponInfo.weaponInstId, self.m_weaponInfo.weaponTemplateId)
    end
end



HL.Commit(WeaponExhibitUpgradeCtrl)
