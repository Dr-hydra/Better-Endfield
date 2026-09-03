local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local SkipChapterBuildingStatus = CS.Beyond.Gameplay.Factory.SkipChapterBuildingStatus

Item = HL.Class('Item', UIWidgetBase)
local LT_ITEM_TICK_TIME_INTERVAL = 30



Item.canPlace = HL.Field(HL.Boolean) << false

Item.canSplit = HL.Field(HL.Boolean) << false

Item.canUse = HL.Field(HL.Boolean) << false

Item.canClear = HL.Field(HL.Boolean) << false

Item.canDestroy = HL.Field(HL.Boolean) << false

Item.canSetQuickBar = HL.Field(HL.Boolean) << false

Item.fromDepot = HL.Field(HL.Boolean) << false

Item.showingTips = HL.Field(HL.Boolean) << false

Item.showingActionMenu = HL.Field(HL.Boolean) << false

Item.hideItemObtainWays = HL.Field(HL.Boolean) << false

Item.hideBottomInfo = HL.Field(HL.Boolean) << false

Item.slotIndex = HL.Field(HL.Any)

Item.id = HL.Field(HL.String) << ''

Item.count = HL.Field(HL.Number) << 0

Item.instId = HL.Field(HL.Any)

Item.extraInfo = HL.Field(HL.Table)

Item.prefixDesc = HL.Field(HL.String) << ''

Item.equipInfo = HL.Field(HL.Any)

Item.redDot = HL.Field(HL.Forward("RedDot"))

Item.m_enableHoverTips = HL.Field(HL.Boolean) << true

Item.m_delayHoverTimer = HL.Field(HL.Number) << -1

Item.m_showCount = HL.Field(HL.Boolean) << true

Item.m_isSelected = HL.Field(HL.Boolean) << false

Item.m_isInfinite = HL.Field(HL.Boolean) << false

Item.m_showingHover = HL.Field(HL.Boolean) << false

Item.m_needShowDeco1 = HL.Field(HL.Boolean) << false

Item.m_limitTimeInfo = HL.Field(HL.Table)

Item.m_itemTipsJumpExtraArgs = HL.Field(HL.Table)

Item.customShowTipsFunc = HL.Field(HL.Function)

Item.customHideTipsFunc = HL.Field(HL.Function)


Item._OnFirstTimeInit = HL.Override() << function(self)
    self:SetSelected(false, true)
end

Item._ResetOnInit = HL.Method() << function(self)
    self.canPlace = false
    self.canSplit = false
    self.canUse = false
    self.canDestroy = false
    self.canSetQuickBar = false

    self:ShowPickUpLogo(false)
    self:ShowGemPerfectIcon(false)
    self:_UpdateLevelNode()
    self.view.button.onIsNaviTargetChanged = nil
    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onHoverChange:RemoveAllListeners()
    self.view.button.onLongPress:RemoveAllListeners()
    if self.redDot then
        self.redDot:Stop()
    end
    self.view.button.clickHintTextId = nil
    self.view.button.longPressHintTextId = nil

    InputManagerInst:DeleteInGroup(self.view.button.hoverBindingGroupId)
    self.m_actionMenuBindingId = -1
    self.actionMenuArgs = nil
    self.customChangeActionMenuFunc = nil
    self.customShowTipsFunc = nil
    self.customHideTipsFunc = nil
    self.m_itemTipsJumpExtraArgs = nil

    self:_ToggleSkipChapterTagNode(false)
    self:_ToggleSkipChapterInvalidNode(false)


    self.m_needShowDeco1 = self.view.deco1.gameObject.activeSelf
end





Item.InitItem = HL.Method(HL.Opt(HL.Any, HL.Any, HL.String, HL.Boolean))
        << function(self, itemBundle, onClick, limitId, clickableEvenEmpty)






    self:_FirstTimeInit()
    self:_ResetOnInit()
    self:SetIconTransparent(UIConst.ITEM_EXIST_TRANSPARENCY)

    if itemBundle then
        if Cfg.GetType(itemBundle) == Cfg.Types.ItemBundle then
            itemBundle = {
                id = itemBundle.id,
                count = itemBundle.count,
            }
        end
    end

    local isEmpty = itemBundle == nil or string.isEmpty(itemBundle.id)
    local isLimited = not string.isEmpty(limitId)
    self.view.content.gameObject:SetActive(not isEmpty or isLimited)
    if itemBundle == nil then
        self.m_isInfinite = false
    else
        self.m_isInfinite = itemBundle.isInfinite or false
    end

    self.extraInfo = {}

    local data
    if isEmpty then
        self:_CloseHoverTips()
        self.id = ""
        self:SetSelected(false)
        if clickableEvenEmpty then
            self.view.button.enabled = true
            self.view.button.onClick:AddListener(function()
                onClick(itemBundle)
            end)
        elseif DeviceInfo.usingController then
            self.view.button.enabled = true
        else
            self.view.button.enabled = false
        end

        if isLimited then
            data = Tables.itemTable[limitId]
            self.view.name.text = data.name
            if self.view.nameScrollText then
                self.view.nameScrollText:ForceUpdate()
            end
            self.view.count.gameObject:SetActive(false)
            self:SetIconTransparent(UIConst.ITEM_MISSING_TRANSPARENCY)
        end
        self:_UpdateIcon(data)

        self.view.content.gameObject:SetActive(isLimited)
        if self.config.USE_EMPTY_BG then
            self.view.emptyBG.gameObject:SetActive(true)
            self.view.normalBG.gameObject:SetActive(false)
        end
        self.m_showCount = false
        self.count = 0
        self:_ToggleEquipEnhanceNode(false)
        self:_TogglePotentialStar(false)
        return
    end

    if self.view.config.SHOW_ITEM_TIPS_ON_R3 or self.view.config.SHOW_ITEM_TIPS_ON_R3_AND_X then
        local actionId = self.view.config.SHOW_ITEM_TIPS_ON_R3_AND_X and "show_item_tips_with_confirm" or "show_item_tips"
        local bId = self:AddHoverBinding(actionId, function()
            self:ShowTips()
        end)
        if self.view.config.SHOW_ITEM_TIPS_ON_R3_AND_X then
            self.view.button:ChangeActionOnSetNaviTarget(CS.Beyond.Input.ActionOnSetNaviTarget.None)
        end
        InputManagerInst:SetBindingText(bId, InputManagerInst:GetActionText("show_item_tips"))
    end

    self.view.content.gameObject:SetActive(true)
    if self.config.USE_EMPTY_BG then
        self.view.emptyBG.gameObject:SetActive(false)
        self.view.normalBG.gameObject:SetActive(true)
    end

    if self.id ~= itemBundle.id then
        self.id = itemBundle.id
        self:SetSelected(false)
        if DeviceInfo.usingController and self.view.button.isNaviTarget and self.m_enableHoverTips and self.view.config.SHOW_HOVER_TIP then
            self:_OnHoverChange(true)
        else
            self:_CloseHoverTips()
        end
    end
    if itemBundle.instId and itemBundle.instId > 0 then
        if self.instId ~= itemBundle.instId then
            self.instId = itemBundle.instId
            self:SetSelected(false)
        end
    else
        if self.instId then
            self.instId = nil
            self:SetSelected(false)
        end
    end
    local _, data = Tables.itemTable:TryGetValue(itemBundle.id)
    if not data then
        logger.error("Item.InitItem error: item id not found: " .. tostring(itemBundle.id))
        return
    end
    local typeData = Tables.itemTypeTable[data.type:ToInt()]
    self.m_showCount = typeData.showCount or self.view.config.FORCE_SHOW_COUNT or (typeData.itemType == GEnums.ItemType.Weapon and self.view.config.IS_REWARD_ITEM)

    self.view.name.text = data.name
    if self.view.nameScrollText then
        self.view.nameScrollText:ForceUpdate()
    end

    self:UpdateCount(itemBundle.count, itemBundle.needCount, nil, nil, nil, nil, itemBundle.isInfinite)
    self:_UpdateIcon(data, itemBundle.instId)
    self:_UpdateInstData(itemBundle)
    self:_UpdateWeaponAddon(itemBundle)
    self:_UpdateEquipAddon(itemBundle)
    self:_UpdateLimitTimeNode(itemBundle)
    self:_UpdateItemRewardTypeTagNode(itemBundle)
    self:_UpdateSkipChapterBuildingNodes(itemBundle)

    if onClick then
        self.view.button.enabled = true
        if onClick == true then

            self.view.button.onClick:AddListener(function()
                self:ShowTips()
            end)
            self.view.button.clickHintTextId = "virtual_mouse_hint_item_tips"
        else
            self.view.button.onClick:AddListener(function()
                onClick(itemBundle)
            end)
        end
    else
        self.view.button.enabled = false
    end

    self:_InitLockNode()

    self.view.button.onHoverChange:RemoveAllListeners()
    if self.m_enableHoverTips and self.view.config.SHOW_HOVER_TIP and not isEmpty then
        self.view.button.onHoverChange:AddListener(function(isHover)
            self:_OnHoverChange(isHover)
        end)
    end
end

Item._OnDestroy = HL.Override() << function(self)
    self:_CloseHoverTips()
    if self.view.ltMarkNode then
        self.view.ltMarkNode:EndTickLimitTime()
    end
end

Item._UpdateIcon = HL.Method(HL.Opt(HL.Any, HL.Number)) << function(self, data, instId)
    if not data then
        self.view.simpleStateController:SetState(self.view.config.FORCE_NO_RARITY and "NoRarity" or "Normal")
        return
    end

    local useBig = self.view.config.USE_BIG_ICON or (DeviceInfo.isPCorConsole and self.view.config.FORCE_USE_BIG_ICON_ON_PC)
    self.view.icon:InitItemIcon(data.id, useBig, instId)
    self:_UpdateCompositeIconBG()

    local showRarity = self.view.icon.showRarity and not self.view.config.FORCE_NO_RARITY
    if showRarity then
        local isMaxRarity = data.rarity == UIConst.ITEM_MAX_RARITY
        self.view.simpleStateController:SetState(isMaxRarity and "6Star" or "Normal")
        if self.view.rarityLight then
            local rarityColor = UIUtils.getItemRarityColor(data.rarity)
            self.view.rarityLine.color = rarityColor
            if not isMaxRarity then
                self.view.rarityLight.color = rarityColor
            end
        end
    else
        self.view.simpleStateController:SetState("NoRarity")
    end
end

Item._UpdateCompositeIconBG = HL.Method() << function(self)
    local active = not self.view.icon.showRarity
    local obj = self:tryToggleDynamicNode(self.view.animationNode.transform, "CompositeIconBG",
        LuaSystemManager.itemPrefabSystem.compositeIconBGPrefab, active,
        function(o)
            o.transform:SetSiblingIndex(0)
            o.transform.localScale = Vector3.one
            o.transform.pivot = Vector2.one / 2
            o.transform.anchorMin = Vector2.zero
            o.transform.anchorMax = Vector2.one
            o.transform.offsetMin = Vector2.zero
            o.transform.offsetMax = Vector2.zero
            o.transform.anchoredPosition3D = Vector3.zero
        end)
    if obj then
        self.view.compositeIconBG = obj:GetComponent("UIImage")
    end
end

Item._InitLockNode = HL.Method() << function(self)
    local hasLock = self.instId and self.instId > 0
    local obj = self:tryToggleDynamicNode(self.view.content.transform, "LockNode",
        LuaSystemManager.itemPrefabSystem.lockNodePrefab, hasLock,
        function(o)
            o.transform.localScale = Vector3.one
            o.transform.pivot = Vector2.one / 2
            o.transform.anchorMin = Vector2.zero
            o.transform.anchorMax = Vector2.zero

            o.transform.anchoredPosition = Vector2(15, 20)
            o.transform.sizeDelta = Vector2(20, 26)
        end)
    if obj and not self.view.lockNode then
        self.view.lockNode = Utils.wrapLuaNode(obj)
    end
    if hasLock and self.view.lockNode then
        self.view.lockNode:InitItemLock(self.id, self.instId)
    end
end

Item._UpdateWeaponAddon = HL.Method(HL.Opt(HL.Any)) << function(self, data)
    local itemCfg = Tables.itemTable:GetValue(data.id)
    local itemType = itemCfg.type
    local isWeapon = itemType == GEnums.ItemType.Weapon
    local weaponInstData
    if isWeapon then
        weaponInstData = CharInfoUtils.getWeaponByInstId(data.instId)
    end

    local showPotentialStar = not data.forceHidePotentialStar and not self.view.config.IS_REWARD_ITEM and isWeapon
    self.view.deco1.gameObject:SetActive(not showPotentialStar and self.m_needShowDeco1)
    self:_TogglePotentialStar(showPotentialStar)
    if showPotentialStar then
        self.view.potentialStar:InitWeaponSimplePotentialStar(weaponInstData and weaponInstData.refineLv or 0)
    end

    self:_ToggleGemEquipped(weaponInstData and weaponInstData.attachedGemInstId > 0)
end

Item._TogglePotentialStar = HL.Method(HL.Boolean) << function(self, active)
    local obj = self:tryToggleDynamicNode(self.view.animationNode.transform, "PotentialStar",
        LuaSystemManager.itemPrefabSystem.potentialStarPrefab, active,
        function(o)
            o.transform:SetSiblingIndex(self.view.content:GetSiblingIndex() + 1)
            o.transform.localScale = Vector3.one
            local center = Vector2(0, 1)
            o.transform.pivot = center
            o.transform.anchorMin = center
            o.transform.anchorMax = center

            o.transform.anchoredPosition = Vector2(0, 0)
            o.transform.sizeDelta = Vector2(40, 40)
        end)
    if obj and not self.view.potentialStar then
        self.view.potentialStar = Utils.wrapLuaNode(obj)
    end
end

Item._ToggleGemEquipped = HL.Method(HL.Opt(HL.Boolean)) << function(self, active)
    local obj = self:tryToggleDynamicNode(self.view.animationNode.transform, "GemEquipped",
        LuaSystemManager.itemPrefabSystem.gemEquippedNodePrefab, active,
        function(o)
            o.transform:SetSiblingIndex(self.view.content:GetSiblingIndex() + 1)

        end)
    if obj and not self.view.gemEquipped then
        self.view.gemEquipped = Utils.wrapLuaNode(obj)
    end
end

Item._ToggleEquipEnhanceNode = HL.Method(HL.Boolean) << function(self, active)
    local obj = self:tryToggleDynamicNode(self.view.animationNode.transform, "EquipEnhanceNode",
        LuaSystemManager.itemPrefabSystem.equipEnhanceNodePrefab, active,
        function(o)
            o.transform:SetSiblingIndex(self.view.content:GetSiblingIndex() + 1)
            o.transform.localScale = self.view.config.EQUIP_ENHANCE_NODE_SCALE
            local leftUp = Vector2(0, 1)
            o.transform.pivot = leftUp
            o.transform.anchorMin = leftUp
            o.transform.anchorMax = leftUp
            o.transform.anchoredPosition = Vector2(0, 0)
        end)
    if obj and not self.view.equipEnhanceNode then
        self.view.equipEnhanceNode = Utils.wrapLuaNode(obj)
        self.view.equipEnhanceNode.customNormalBgColor = self.view.config.EQUIP_ENHANCE_NODE_BG_COLOR
    end
end

Item._ToggleItemLimitTimeMarkNode = HL.Method(HL.Boolean) << function(self, active)
    local obj = self:tryToggleDynamicNode(self.view.animationNode.transform, "LimitTimeMarkNode",
        LuaSystemManager.itemPrefabSystem.itemLimitTimeMarkNodePrefab, active,
        function(o)

            local transform = o.transform
            transform:SetSiblingIndex(self.view.content:GetSiblingIndex() + 1)
            transform.localScale = Vector3.one
            local leftUp = Vector2(0, 1)
            transform.pivot = leftUp
            transform.anchorMin = leftUp
            transform.anchorMax = leftUp
            transform.anchoredPosition = Vector2(0, 0)
            transform:SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, self.view.transform.rect.width * 0.65)
        end)
    if obj and not self.view.ltMarkNode then

        self.view.ltMarkNode = Utils.wrapLuaNode(obj)
    end
end

Item._ToggleItemRewardTypeTagNode = HL.Method(HL.Boolean) << function(self, active)
    local obj = self:tryToggleDynamicNode(self.view.animationNode.transform, "ItemRewardTypeTag",
        LuaSystemManager.itemPrefabSystem.itemRewardTypeTagPrefab, active,
        function(o)

            local transform = o.transform
            transform:SetSiblingIndex(self.view.content:GetSiblingIndex() + 1)
            transform.localScale = Vector3.one
            local leftUp = Vector2(0, 1)
            transform.pivot = leftUp
            transform.anchorMin = leftUp
            transform.anchorMax = leftUp
            transform.anchoredPosition = Vector2(-5, 3)
        end)
    if obj and not self.view.rewardTypeTagNode then
        self.view.rewardTypeTagNode = Utils.wrapLuaNode(obj)
    end
end

Item.HideSkipChapterMarks = HL.Method() << function(self)
    self:_ToggleSkipChapterTagNode(false)
    self:_ToggleSkipChapterInvalidNode(false)
end

Item._ToggleSkipChapterTagNode = HL.Method(HL.Boolean) << function(self, active)
    local obj = self:tryToggleDynamicNode(self.view.animationNode.transform, "SkipChapterTagNode",
        LuaSystemManager.itemPrefabSystem.skipChapterTagNodePrefab, active,
        function(o) o.transform:SetSiblingIndex(self.view.content:GetSiblingIndex() + 1) end)
    if obj then
        self.view.skipChapterTagNode = obj.transform
    end
end

Item._ToggleSkipChapterInvalidNode = HL.Method(HL.Opt(HL.Boolean)) << function(self, active)
    local obj = self:tryToggleDynamicNode(self.view.animationNode.transform, "SkipChapterInvalidNode",
        LuaSystemManager.itemPrefabSystem.skipChapterInvalidNodePrefab, active,
        function(o) o.transform:SetSiblingIndex(self.view.content:GetSiblingIndex() + 1) end)
    if obj then
        self.view.skipChapterInvalidNode = obj.transform
    end
end

Item._UpdateSkipChapterBuildingNodes = HL.Method(HL.Opt(HL.Any)) << function(self, itemBundle)
    local enableSkipChapterMarks = self.view.config.SHOW_SKIP_CHAPTER_BUILDING_MARKS == true
    if not enableSkipChapterMarks or Utils.isInBlackbox() then
        self:HideSkipChapterMarks()
        return
    end
    local itemId = itemBundle and itemBundle.id or self.id
    if string.isEmpty(itemId) then
        self:HideSkipChapterMarks()
        return
    end
    local status = FactoryUtils.getSkipChapterBuildingStatusByItemId(itemId)
    local showTag = status == SkipChapterBuildingStatus.SkipUnlocked
        or status == SkipChapterBuildingStatus.SkipUnlockedButInvalid
    self:_ToggleSkipChapterTagNode(showTag)
    local hideSkipChapterInvalidNode = self.view.config.HIDE_SKIP_CHAPTER_INVALID_MARK == true
        or Utils.isInSpaceShip()
        or Utils.isInDungeon()
    local domainNotSupport = FactoryUtils.checkBuildingDomainSupportByItemId(itemId)
    self:_ToggleSkipChapterInvalidNode(not hideSkipChapterInvalidNode and (status == SkipChapterBuildingStatus.SkipUnlockedButInvalid or domainNotSupport))
end

Item._UpdateEquipAddon = HL.Method(HL.Opt(HL.Any)) << function(self, data)
    local itemCfg = Tables.itemTable:GetValue(data.id)
    local itemType = itemCfg.type
    local isEquip = itemType == GEnums.ItemType.Equip
    if not isEquip then
        self:_ToggleEquipEnhanceNode(false)
        return
    end
    local _, equipCfg = Tables.equipTable:TryGetValue(data.id)
    if not equipCfg then
        logger.error("Item._UpdateEquipAddon error: 【装备基础信息】中不存在: " .. tostring(data.id))
        return
    end
    self:_UpdateLevelNode(equipCfg.minWearLv)

    local showEquipEnhance = EquipTechUtils.canShowEquipEnhanceNode(data.instId)
    self:_ToggleEquipEnhanceNode(showEquipEnhance)
    if showEquipEnhance then
        self.view.equipEnhanceNode:InitEquipEnhanceNode({
            equipInstId = data.instId,
        })
    end
end

Item._UpdateLevelNode = HL.Method(HL.Opt(HL.Number)) << function(self, lv)
    if not self.view.levelNode then
        local obj = self:tryToggleDynamicNode(self.view.countNode.transform.parent, "LevelNode",
            LuaSystemManager.itemPrefabSystem.levelNodePrefab, lv ~= nil,
            function(o)

                self.view.bottomTxtBgDecoLineActiveHelper.checkTargets:Add(o)
            end)
        if obj then
            self.view.levelNode = obj.transform

            local lvTxt = obj.transform:Find("LvTxt"):GetComponent("UIText")
            lvTxt.color = self.view.config.LEVEL_TEXT_COLOR
            self.view.lvNumTxt = obj.transform:Find("LvNumTxt"):GetComponent("UIText")
            self.view.lvNumTxt.color = self.view.config.LEVEL_TEXT_COLOR
        end
    end
    if lv then
        self.view.levelNode.gameObject:SetActive(true)
        self.view.lvNumTxt.text = lv
    elseif self.view.levelNode then
        self.view.levelNode.gameObject:SetActive(false)
    end
end

Item._UpdateLimitTimeNode = HL.Method(HL.Opt(HL.Any)) << function(self, itemBundle)
    self.m_limitTimeInfo = Utils.getLTItemExpireInfo(itemBundle.id, itemBundle.instId)
    if self.m_limitTimeInfo.isLTItem then
        self:_ToggleItemLimitTimeMarkNode(true)
        self.view.ltMarkNode:StartTickLimitTime(self.m_limitTimeInfo.expireTime, self.m_limitTimeInfo.almostExpireTime)
    else
        self:_ToggleItemLimitTimeMarkNode(false)
        if self.view.ltMarkNode then
            self.view.ltMarkNode:EndTickLimitTime()
        end
    end
end

Item._UpdateItemRewardTypeTagNode = HL.Method(HL.Opt(HL.Any)) << function(self, itemBundle)
    if not string.isEmpty(itemBundle.typeTag) then
        self:_ToggleItemRewardTypeTagNode(true)
        self.view.rewardTypeTagNode.stateController:SetState(itemBundle.typeTag)
        self.view.rewardTypeTagNode.text.fontSize = self.view.config.ITEM_REWARD_TYPE_TAG_FONT_SIZE
    else
        self:_ToggleItemRewardTypeTagNode(false)
    end
end

Item.SetIconTransparent = HL.Method(HL.Number) << function(self, a)
    self.view.icon:SetAlpha(a)
end

Item.SetExtraInfo = HL.Method(HL.Table) << function(self, extraInfo)
    self.extraInfo = extraInfo
end

Item.ShowTips = HL.Method(HL.Opt(HL.Table, HL.Function)) << function(self, posInfo, onClose)
    posInfo = posInfo or self.extraInfo

    if self.showingTips then
        if not (DeviceInfo.usingController and posInfo.isSideTips) then
            Notify(MessageConst.HIDE_ITEM_TIPS)
            self:_OnTipsClosed(onClose)
        end
        return
    end

    self.showingTips = true

    self:SetSelected(true)

    if DeviceInfo.usingController and posInfo.isSideTips and self.m_enableHoverTips then


        if self.extraInfo == nil or not self.extraInfo.isSideTips then
            logger.error("It's illegal to use sideTips but haven't setting a extra info with sideTips!")
        end
    end

    if self.customShowTipsFunc then
        self.customShowTipsFunc()
    end
    Notify(MessageConst.SHOW_ITEM_TIPS, {
        transform = posInfo.tipsPosTransform or self.transform,
        posType = posInfo.tipsPosType,
        safeArea = posInfo.safeArea,
        padding = posInfo.padding,
        isSideTips = posInfo.isSideTips,
        moveVirtualMouse = posInfo.moveVirtualMouse,
        onBeforeJump = posInfo.onBeforeJump,

        keyHintGroupIds = posInfo.keyHintGroupIds,

        notPenetrate = self.config.NOT_PENETRATE_ITEM_TIPS_PANEL,
        forceShowOwnCount = self.config.ITEM_TIPS_FORCE_SHOW_OWN_COUNT,

        hideItemObtainWays = self.hideItemObtainWays,
        hideBottomInfo = self.hideBottomInfo,
        prefixDesc = self.prefixDesc,

        itemId = self.id,
        itemCount = self.count,
        instId = self.instId,
        slotIndex = self.slotIndex,
        fromDepot = self.fromDepot,

        canPlace = self.canPlace,
        canSplit = self.canSplit,
        canUse = self.canUse,
        canClear = self.canClear,

        jumpExtraArgs = self.m_itemTipsJumpExtraArgs,

        onClose = function()
            self:_OnTipsClosed(onClose)
        end
    })
end

Item._OnTipsClosed = HL.Method(HL.Opt(HL.Function)) << function(self, onClose)
    if not self.showingTips then
        return
    end
    if NotNull(self.view.gameObject) then
        self:SetSelected(false)
        self.showingTips = false
        if DeviceInfo.usingController then
            if self.m_enableHoverTips and self.view.config.SHOW_HOVER_TIP and self.view.button.isNaviTarget then
                self:_OnHoverChange(true)
            end
        end
    end
    if self.customHideTipsFunc then
        self.customHideTipsFunc()
    end
    if onClose then
        onClose()
    end
end

Item.UpdateCountSimple = HL.Method(HL.Opt(HL.Number, HL.Boolean))
        << function(self, count, isLack)
    self:UpdateCount(count, nil, false, false, nil, isLack, self.m_isInfinite)
end

Item.UpdateCountWithColor = HL.Method(HL.Number, HL.String) << function(self, count, colorFormatter)
    if not self.m_showCount then
        self.view.count.gameObject:SetActive(false)
        return
    end

    self.count = count
    self.view.count.text = string.format(colorFormatter, UIUtils.getNumString(count))

    if count > 0 then
        self:SetIconTransparent(UIConst.ITEM_EXIST_TRANSPARENCY)
    else
        self:SetIconTransparent(UIConst.ITEM_MISSING_TRANSPARENCY)
    end

    self.view.count.gameObject:SetActive(true)
end





Item.UpdateCount = HL.Method(HL.Opt(HL.Number, HL.Number, HL.Boolean, HL.Boolean, HL.String, HL.Boolean, HL.Boolean))
        << function(self, count, needCount, keepColor, needCountFirst, formatter, isLack, isInfinite)
    if not self.m_showCount then
        self.view.count.gameObject:SetActive(false)
        return
    end

    if count then
        self.count = count
        isInfinite = isInfinite or self.m_isInfinite
        local countText = isInfinite and Language.LUA_ITEM_INFINITE_COUNT  or UIUtils.getNumString(count)
        if not needCount then
            self.view.count.text = UIUtils.setCountColor(countText, isLack)
            self.view.count.gameObject:SetActive(true)
            if count > 0 then
                self:SetIconTransparent(UIConst.ITEM_EXIST_TRANSPARENCY)
            else
                self:SetIconTransparent(UIConst.ITEM_MISSING_TRANSPARENCY)
            end
        else
            self.view.count.gameObject:SetActive(true)
            self:SetIconTransparent(UIConst.ITEM_EXIST_TRANSPARENCY)
            local text
            formatter = formatter or "%s/%s"
            if needCountFirst then
                text = string.format(formatter, UIUtils.getNumString(needCount), countText)
            else
                text = string.format(formatter, countText, UIUtils.getNumString(needCount))
            end
            if not keepColor then
                self.view.count.text = UIUtils.setCountColor(text, count < needCount)
            else
                self.view.count.text = text
            end
        end
    else
        self.count = 0
        self:SetIconTransparent(UIConst.ITEM_EXIST_TRANSPARENCY)
        self.view.count.gameObject:SetActive(false)
    end
end

Item.SetSelected = HL.Method(HL.Opt(HL.Boolean, HL.Boolean)) << function(self, isSelected, forceUpdate)
    if self.m_isDestroyed then
        return
    end
    isSelected = isSelected == true
    if not forceUpdate and self.m_isSelected == isSelected then
        return
    end
    self.m_isSelected = isSelected == true
    self:_CloseHoverTips()
    self.view.selectedBG.gameObject:SetActive(isSelected)
end

Item.OpenLongPressTips = HL.Method() << function(self)
    self.view.button.onLongPress:AddListener(function()
        self:ShowTips()
    end)
    self.view.button.longPressHintTextId = "virtual_mouse_hint_item_tips"
end

Item.UpdateRedDot = HL.Method(HL.Opt(HL.String, HL.Any)) << function(self, name, arg)
    if not self.redDot then
        local obj = self:tryToggleDynamicNode(self.view.animationNode.transform, "RedDot",
            LuaSystemManager.itemPrefabSystem.redDotPrefab, true,
            function(o)

                o.transform:SetAsLastSibling()

                o.transform.localScale = Vector3.one
                o.transform.pivot = Vector2.one / 2
                o.transform.anchorMin = Vector2.one
                o.transform.anchorMax = Vector2.one
                local cfg = self.view.config.RED_DOT_TRANS_INFO
                o.transform.anchoredPosition = Vector2(cfg.x, cfg.y)
                o.transform.sizeDelta = Vector2(cfg.z, cfg.w)
            end)
        if obj then
            self.view.redDot = Utils.wrapLuaNode(obj)
            self.redDot = self.view.redDot
        end
    end
    if string.isEmpty(self.id) then
        self.redDot:Stop()
    else
        if name then
            self.redDot:InitRedDot(name, arg)
        elseif self.instId then
            self.redDot:InitRedDot("InstItem", self)
        else
            self.redDot:InitRedDot("NormalItem", self.id)
        end
    end
end

Item.Read = HL.Method() << function(self)
    if not self.redDot then
        return
    end
    if not self.redDot.curIsActive then
        return
    end
    if self.instId then
        GameInstance.player.inventory:ReadNewItem(self.id, self.instId)
    else
        GameInstance.player.inventory:ReadNewItem(self.id)
    end
end

Item._UpdateInstData = HL.Method(HL.Opt(HL.Any)) << function(self, itemBundle)
    local hasInstId = itemBundle and itemBundle.instId and itemBundle.instId > 0
    if not hasInstId then
        return
    end

    local instId = itemBundle.instId
    local weaponInst = CharInfoUtils.getWeaponByInstId(instId)
    if weaponInst and not self.view.config.IS_REWARD_ITEM then
        self:_UpdateLevelNode(weaponInst.weaponLv)
        return
    end

    local equipInst = CharInfoUtils.getEquipByInstId(instId)
    if equipInst then
    end
end

Item.ShowPickUpLogo = HL.Method(HL.Boolean) << function(self, isShow)
    local obj = self:tryToggleDynamicNode(self.view.animationNode.transform, "SpTypeIconNode",
        LuaSystemManager.itemPrefabSystem.spTypeIconNodePrefab, isShow,
        function(o)
            o.transform:SetSiblingIndex(self.view.content:GetSiblingIndex() + 1)
            o.transform.localScale = Vector3.one
            o.transform.pivot = Vector2.up
            o.transform.anchorMin = Vector2.up
            o.transform.anchorMax = Vector2.up
            o.transform.anchoredPosition = Vector2(0, 0)
            local size = 156 * self.view.transform.rect.width / 180
            o.transform.sizeDelta = Vector2(size, size)
        end)
    if obj and not self.view.spTypeIconNode then
        self.view.spTypeIconNode = Utils.wrapLuaNode(obj)
    end
    if not self.view.spTypeIconNode then
        return
    end
    if isShow then

        local isPickUp, _ = Tables.useItemTable:TryGetValue(self.id)
        if isPickUp then
            self.view.spTypeIconNode.gameObject:SetActive(true)
            self.view.spTypeIconNode.icon:LoadSprite(UIConst.UI_SPRITE_ITEM, "icon_pickupitems")
            self.view.spTypeIconNode.stateController:SetState("Normal")
        elseif Utils.isPortableDevice(self.id) then
            self.view.spTypeIconNode.gameObject:SetActive(true)
            self.view.spTypeIconNode.icon:LoadSprite(UIConst.UI_SPRITE_ITEM, "icon_portable_device")
            self.view.spTypeIconNode.stateController:SetState("Normal")
        else
            self.view.spTypeIconNode.gameObject:SetActive(false)
        end
    else
        self.view.spTypeIconNode.gameObject:SetActive(false)
    end
end

Item.ShowGemPerfectIcon = HL.Method(HL.Boolean) << function(self, isShow)
    local obj = self:tryToggleDynamicNode(self.view.animationNode.transform, "GemPerfectIcon",
        LuaSystemManager.itemPrefabSystem.gemPerfectIconPrefab, isShow,
        function(o)
            o.transform:SetSiblingIndex(self.view.content:GetSiblingIndex() + 1)
            o.transform.localScale = Vector3.one
            local leftUp = Vector2(0, 1)
            o.transform.pivot = leftUp
            o.transform.anchorMin = leftUp
            o.transform.anchorMax = leftUp
            o.transform.anchoredPosition = Vector2(0, 0)
        end)
    if obj then
        self.view.gemPerfectIcon = obj
    elseif not isShow then
        return
    end


    self.view.deco1.gameObject:SetActive(not isShow and self.m_needShowDeco1)
end

Item.SetAsNaviTarget = HL.Method() << function(self)
    self:SetNaviTarget(self.view.button)
end

Item.SetItemTipsJumpExtraArgs = HL.Method(HL.Table) << function(self, args)
    self.m_itemTipsJumpExtraArgs = args
end



Item.m_actionMenuBindingId = HL.Field(HL.Number) << -1









Item.actionMenuArgs = HL.Field(HL.Table)

Item.customChangeActionMenuFunc = HL.Field(HL.Function)


Item.InitActionMenu = HL.Method() << function(self)
    if self.m_actionMenuBindingId > 0 then
        return
    end
    self.m_actionMenuBindingId = InputManagerInst:CreateBindingByActionId("item_open_action_menu", function()
        self:ShowActionMenu()
    end, self.view.button.hoverBindingGroupId)
end

Item.ToggleActionMenu = HL.Method(HL.Boolean) << function(self, active)
    if self.m_actionMenuBindingId <= 0 then
        return
    end
    InputManagerInst:ToggleBinding(self.m_actionMenuBindingId, active)
end

Item.ShowActionMenu = HL.Method(HL.Opt(HL.Boolean, HL.Number)) << function(self, noMask, posType)
    self:SetSelected(true)
    self.showingActionMenu = true
    Notify(MessageConst.HIDE_ITEM_TIPS)
    local initId = self.id
    local checkActionValid = function()
        return initId == self.id
    end
    Notify(MessageConst.SHOW_NAVI_TARGET_ACTION_MENU, {
        transform = self.transform,
        actions = self:_GenActionMenuInfos(),
        onClose = function()
            self:SetSelected(false)
            self.showingActionMenu = false
        end,
        noMask = noMask,
        posType = posType,
        checkActionValid = checkActionValid,
    })
end

Item._GenActionMenuInfos = HL.Method().Return(HL.Table) << function(self)
    local id = self.id
    local count = self.count
    local args = self.actionMenuArgs

    local isItemBag = args.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag
    local isFacDepot = args.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot
    local isRepository = args.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.Repository
    local isStorage = args.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.Storage
    local isQuickBar = args.source == UIConst.UI_DRAG_DROP_SOURCE_TYPE.QuickBar

    local inventory = GameInstance.player.inventory
    local core = GameInstance.player.remoteFactory.core
    local scope = Utils.getCurrentScope()
    local chapterId = Utils.getCurrentChapterId()
    local isSafeArea = args.ignoreInSafeZone or Utils.isInSafeZone()
    local isBuilding, buildingId = FactoryUtils.isBuilding(id)
    local isLogistic, logisticId = FactoryUtils.isLogistic(id)
    local isEmptyBottle = FactoryUtils.isEmptyBottleOrJarItem(id, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
    local isFullBottle = FactoryUtils.isFullBottleOrJarItem(id, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
    local isEmptyGasJar = FactoryUtils.isEmptyBottleOrJarItem(id, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)
    local isFullGasJar = FactoryUtils.isFullBottleOrJarItem(id, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)
    local isLiquid = Tables.liquidTable:ContainsKey(id)
    local isGas = Tables.gasTable:ContainsKey(id)

    local itemMoveCheckFunc = args.itemMoveCheckFunc
    local itemMoveValid = itemMoveCheckFunc == nil or itemMoveCheckFunc()

    local depotMoveNotLocked = not Utils.isDepotManualInOutLocked()

    local actionMenuInfos = {}


    if itemMoveValid and count > 0 and (args.cacheArea or args.cacheRepo) and (isItemBag or (isFacDepot and depotMoveNotLocked)) then
        local cacheHasNormal = (args.cacheArea and args.cacheArea.hasNormalCacheIn) or
                               (args.cacheRepo and args.cacheRepo:GetRepoCacheType() == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal)
        local cacheHasFluid = (args.cacheArea and args.cacheArea.hasFluidCacheIn) or
                              (args.cacheRepo and args.cacheRepo:GetRepoCacheType() == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
        local cacheHasGas = (args.cacheArea and args.cacheArea.hasGasCacheIn) or
                            (args.cacheRepo and args.cacheRepo:GetRepoCacheType() == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)

        if isEmptyGasJar and cacheHasGas then
            table.insert(actionMenuInfos, {
                objName = "FillGas",
                text = Language.LUA_ITEM_ACTION_FILL_GAS,
                action = function()
                    if args.cacheArea then
                        args.cacheArea:NaviTargetMoveToInCacheSlot(self, args.dragHelper, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)
                    elseif args.cacheRepo then
                        args.cacheRepo:TryDropItemToRepository(args.dragHelper)
                    end
                end,
            })
        end

        if isFullGasJar and cacheHasGas then
            table.insert(actionMenuInfos, {
                objName = "DumpGas",
                text = Language.LUA_ITEM_ACTION_DUMP_GAS,
                action = function()
                    if args.cacheArea then
                        args.cacheArea:NaviTargetMoveToInCacheSlot(self, args.dragHelper, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)
                    elseif args.cacheRepo then
                        args.cacheRepo:TryDropItemToRepository(args.dragHelper)
                    end
                end,
            })
        end

        if isEmptyBottle and cacheHasFluid then
            table.insert(actionMenuInfos, {
                objName = "FillLiquid",
                text = Language.LUA_ITEM_ACTION_FILL_LIQUID,
                action = function()
                    if args.cacheArea then
                        args.cacheArea:NaviTargetMoveToInCacheSlot(self, args.dragHelper, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
                    elseif args.cacheRepo then
                        args.cacheRepo:TryDropItemToRepository(args.dragHelper)
                    end
                end,
            })
        end

        if isFullBottle and cacheHasFluid then
            table.insert(actionMenuInfos, {
                objName = "DumpLiquid",
                text = Language.LUA_ITEM_ACTION_DUMP_LIQUID,
                action = function()
                    if args.cacheArea then
                        args.cacheArea:NaviTargetMoveToInCacheSlot(self, args.dragHelper, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)
                    elseif args.cacheRepo then
                        args.cacheRepo:TryDropItemToRepository(args.dragHelper)
                    end
                end,
            })
        end

        if cacheHasNormal then
            table.insert(actionMenuInfos, {
                objName = "MoveToMachine",
                text = Language.LUA_ITEM_ACTION_MOVE_TO_MACHINE,
                action = function()
                    if args.cacheArea then
                        args.cacheArea:NaviTargetMoveToInCacheSlot(self, args.dragHelper, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal)
                    elseif args.cacheRepo then
                        args.cacheRepo:TryDropItemToRepository(args.dragHelper)
                    end
                end,
                onHoverAction = function(isHover)
                    local msg = isHover and
                        MessageConst.FAC_ON_MOVE_TO_IN_CACHE_SLOT_HINT_START or
                        MessageConst.FAC_ON_MOVE_TO_IN_CACHE_SLOT_HINT_END
                    Notify(msg)
                end,
            })
        end

        if cacheHasNormal then
            table.insert(actionMenuInfos, {
                objName = "MoveHalf",
                text = Language.LUA_CONTROLLER_ITEM_ACTION_MOVE_HALF,
                action = function()
                    if args.cacheArea then
                        args.cacheArea:DropItemToArea(args.dragHelper, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal, CS.Proto.ITEM_MOVE_MODE.HalfGrid)
                    elseif args.cacheRepo then
                        args.cacheRepo:TryDropItemToRepository(args.dragHelper, CS.Proto.ITEM_MOVE_MODE.HalfGrid)
                    end
                end,
            })
            table.insert(actionMenuInfos, {
                objName = "MoveAll",
                text = Language.LUA_CONTROLLER_ITEM_ACTION_MOVE_ALL,
                action = function()
                    if args.cacheArea then
                        args.cacheArea:DropItemToArea(args.dragHelper, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal, CS.Proto.ITEM_MOVE_MODE.BatchItemId)
                    elseif args.cacheRepo then
                        args.cacheRepo:TryDropItemToRepository(args.dragHelper, CS.Proto.ITEM_MOVE_MODE.BatchItemId)
                    end
                end,
            })
        end
    end



    if itemMoveValid and count > 0 and (args.storage) and (isItemBag or (isFacDepot and depotMoveNotLocked)) then
        table.insert(actionMenuInfos, {
            objName = "MoveToMachine",
            text = Language.LUA_ITEM_ACTION_MOVE_TO_MACHINE,
            action = function()
                args.storage:_OnDropItem(-1, args.dragHelper)
            end,
        })
        local componentId = args.storage.m_storage.componentId
        table.insert(actionMenuInfos, {
            objName = "MoveHalf",
            text = Language.LUA_CONTROLLER_ITEM_ACTION_MOVE_HALF,
            action = function()
                if isItemBag then
                    core:Message_OpMoveItemBagToGridBox(chapterId, args.dragHelper.info.csIndex, componentId, 0, CS.Proto.ITEM_MOVE_MODE.HalfGrid)
                elseif isFacDepot then
                    core:Message_OpMoveItemDepotToGridBox(chapterId, args.dragHelper.info.itemId, componentId, 0, CS.Proto.ITEM_MOVE_MODE.HalfGrid)
                end
            end
        })
        table.insert(actionMenuInfos, {
            objName = "MoveAll",
            text = Language.LUA_CONTROLLER_ITEM_ACTION_MOVE_ALL,
            action = function()
                if isItemBag then
                    core:Message_OpMoveItemBagToGridBox(chapterId, args.dragHelper.info.csIndex, componentId, 0, CS.Proto.ITEM_MOVE_MODE.BatchItemId)
                elseif isFacDepot then
                    core:Message_OpMoveItemDepotToGridBox(chapterId, args.dragHelper.info.itemId, componentId, 0, CS.Proto.ITEM_MOVE_MODE.BatchItemId)
                end
            end
        })
    end




    if itemMoveValid and count > 0 and ((isFacDepot and isSafeArea and depotMoveNotLocked) or isRepository or isStorage) and (args.cacheType == nil or args.cacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal) then
        table.insert(actionMenuInfos, {
            objName = "MoveToBag",
            text = Language.LUA_ITEM_ACTION_MOVE_TO_ITEM_BAG,
            action = function()
                local itemBag = inventory.itemBag[scope]
                local toIndex = itemBag:GetFirstValidSlotIndex(id)
                if toIndex < 0 then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_ITEM_BAG_NO_EMPTY_SLOT)
                    return
                end
                if isFacDepot then
                    inventory:FactoryDepotMoveToItemBag(scope, chapterId, id, args.moveCount or count, toIndex)
                elseif isRepository then
                    core:Message_OpMoveItemCacheToBag(chapterId, args.componentId, toIndex, args.cacheGridIndex, CS.Proto.ITEM_MOVE_MODE.Normal)
                elseif isStorage then
                    core:Message_OpMoveItemGridBoxToBag(chapterId, args.componentId, args.csIndex, toIndex, CS.Proto.ITEM_MOVE_MODE.Normal)
                end
            end
        })

        if isFacDepot or isRepository or isStorage then
            table.insert(actionMenuInfos, {
                objName = "MoveHalf",
                text = Language.LUA_CONTROLLER_ITEM_ACTION_MOVE_HALF_TO_BAG,
                action = function()
                    if isFacDepot then
                        inventory:FactoryDepotMoveToItemBag(scope, chapterId, id, CS.Proto.ITEM_MOVE_MODE.HalfGrid)
                    elseif isRepository then
                        core:Message_OpMoveItemCacheToBag(chapterId, args.componentId, 0, args.cacheGridIndex, CS.Proto.ITEM_MOVE_MODE.HalfGrid)
                    elseif isStorage then
                        core:Message_OpMoveItemGridBoxToBag(chapterId, args.componentId, args.csIndex, 0, CS.Proto.ITEM_MOVE_MODE.HalfGrid)
                    end
                end
            })
            if isFacDepot or isStorage then
                table.insert(actionMenuInfos, {
                    objName = "MoveAll",
                    text = Language.LUA_CONTROLLER_ITEM_ACTION_MOVE_ALL_TO_BAG,
                    action = function()
                        if isFacDepot then
                            inventory:FactoryDepotMoveToItemBag(scope, chapterId, id, CS.Proto.ITEM_MOVE_MODE.BatchItemId)
                        elseif isStorage then
                            core:Message_OpMoveItemGridBoxToBag(chapterId, args.componentId, args.csIndex, 0, CS.Proto.ITEM_MOVE_MODE.BatchItemId)
                        end
                    end
                })
            end
        end

























    end



    if itemMoveValid and depotMoveNotLocked and count > 0 and (isSafeArea and (isItemBag or isRepository or isStorage)) and (args.cacheType == nil or args.cacheType == FacConst.FAC_CACHE_SLOT_TYPE_STATE.Normal) then
        table.insert(actionMenuInfos, {
            objName = "MoveToDepot",
            text = Language.LUA_ITEM_ACTION_MOVE_TO_DEPOT,
            action = function()
                if isItemBag then
                    inventory:ItemBagMoveToFactoryDepot(scope, chapterId, self.slotIndex)
                elseif isRepository then
                    core:Message_OpMoveItemCacheToDepot(chapterId, args.componentId, args.cacheGridIndex)
                elseif isStorage then
                    core:Message_OpMoveItemGridBoxToDepot(chapterId, args.componentId, args.csIndex, CS.Proto.ITEM_MOVE_MODE.Normal)
                end
            end
        })

        if isItemBag or isRepository or isStorage then
            table.insert(actionMenuInfos, {
                objName = "MoveHalf",
                text = Language.LUA_CONTROLLER_ITEM_ACTION_MOVE_HALF_TO_DEPOT,
                action = function()
                    if isItemBag then
                        inventory:ItemBagMoveToFactoryDepot(scope, chapterId, self.slotIndex, CS.Proto.ITEM_MOVE_MODE.HalfGrid)
                    elseif isRepository then
                        core:Message_OpMoveItemCacheToDepot(chapterId, args.componentId, args.cacheGridIndex, CS.Proto.ITEM_MOVE_MODE.HalfGrid)
                    elseif isStorage then
                        core:Message_OpMoveItemGridBoxToDepot(chapterId, args.componentId, args.csIndex, CS.Proto.ITEM_MOVE_MODE.HalfGrid)
                    end
                end
            })
            if isItemBag or isStorage then
                table.insert(actionMenuInfos, {
                    objName = "MoveAll",
                    text = Language.LUA_CONTROLLER_ITEM_ACTION_MOVE_ALL_TO_DEPOT,
                    action = function()
                        if isItemBag then
                            inventory:ItemBagMoveToFactoryDepot(scope, chapterId, self.slotIndex, CS.Proto.ITEM_MOVE_MODE.BatchItemId)
                        elseif isStorage then
                            core:Message_OpMoveItemGridBoxToDepot(chapterId, args.componentId, args.csIndex, CS.Proto.ITEM_MOVE_MODE.BatchItemId)
                        end
                    end
                })
            end
        end
    end



    if count > 0 and isItemBag and args.startMoveAct then
        table.insert(actionMenuInfos, {
            objName = "MoveInItemBag",
            text = Language.LUA_ITEM_ACTION_MOVE_IN_ITEM_BAG,
            action = args.startMoveAct
        })
    end



    if itemMoveValid and isRepository and isLiquid and (args.cacheType ~= nil and FactoryUtils.isCacheTypeAcceptItemType(args.cacheType, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid)) then

        table.insert(actionMenuInfos, {
            objName = "SelectFillLiquid",
            text = Language.LUA_ITEM_ACTION_CACHE_SELECT_FILL_LIQUID,
            action = function()
                Notify(MessageConst.ON_NAVI_INVENTORY_SELECT_FLUID, {
                    componentId = args.componentId,
                    slotIndex = args.cacheGridIndex,
                    fluidId = "",
                    sourceItem = self,
                    cacheType = FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid
                })
            end
        })
        if args.isInCacheSlot then
            table.insert(actionMenuInfos, {
                objName = "SelectDumpLiquid",
                text = Language.LUA_ITEM_ACTION_CACHE_SELECT_DUMP_LIQUID,
                action = function()
                    Notify(MessageConst.ON_NAVI_INVENTORY_SELECT_FLUID, {
                        componentId = args.componentId,
                        slotIndex = args.cacheGridIndex,
                        fluidId = id,
                        sourceItem = self,
                        cacheType = FacConst.FAC_CACHE_SLOT_TYPE_STATE.Liquid
                    })
                end
            })
        end
    end



    if itemMoveValid and isRepository and isGas and (args.cacheType ~= nil and FactoryUtils.isCacheTypeAcceptItemType(args.cacheType, FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas)) then

        table.insert(actionMenuInfos, {
            objName = "SelectFillGas",
            text = Language.LUA_ITEM_ACTION_CACHE_SELECT_FILL_GAS,
            action = function()
                Notify(MessageConst.ON_NAVI_INVENTORY_SELECT_FLUID, {
                    componentId = args.componentId,
                    slotIndex = args.cacheGridIndex,
                    fluidId = "",
                    sourceItem = self,
                    cacheType = FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas
                })
            end
        })
        if args.isInCacheSlot and not args.disableDump then
            table.insert(actionMenuInfos, {
                objName = "SelectDumpGas",
                text = Language.LUA_ITEM_ACTION_CACHE_SELECT_DUMP_GAS,
                action = function()
                    Notify(MessageConst.ON_NAVI_INVENTORY_SELECT_FLUID, {
                        componentId = args.componentId,
                        slotIndex = args.cacheGridIndex,
                        fluidId = id,
                        sourceItem = self,
                        cacheType = FacConst.FAC_CACHE_SLOT_TYPE_STATE.Gas
                    })
                end
            })
        end
    end



    if count > 0 and self.canUse then
        local isUseItem, _ = Tables.useItemTable:TryGetValue(id)
        if isUseItem and Utils.isSystemUnlocked(GEnums.UnlockSystemType.ItemUse) then
            table.insert(actionMenuInfos, {
                objName = "Use",
                text = Language.LUA_ITEM_ACTION_USE,
                action = function()
                    UIUtils.useItemOnTip(id)
                end
            })
        end
    end



    if self.canPlace and ((isBuilding and count > 0) or isLogistic) then
        table.insert(actionMenuInfos, {
            objName = "Place",
            text = Language.LUA_ITEM_ACTION_PLACE,
            action = function()
                if isBuilding then
                    Notify(MessageConst.FAC_ENTER_BUILDING_MODE, {
                        itemId = id,
                        slotIndex = self.slotIndex,
                        fromDepot = isFacDepot,
                    })
                elseif isLogistic then
                    Notify(MessageConst.FAC_ENTER_LOGISTIC_MODE, { itemId = id })
                end
            end
        })
    end




    if isBuilding and self.canSetQuickBar then
        if UIManager:IsShow(PanelId.FacQuickBar) then
            table.insert(actionMenuInfos, {
                objName = "MoveToQuickBar",
                text = Language.LUA_ITEM_ACTION_MOVE_TO_FAC_QUICK_BAR,
                action = function()
                    Notify(MessageConst.START_SET_BUILDING_ON_FAC_QUICK_BAR, {
                        itemId = id,
                    })
                end
            })
        end
    end

    if isQuickBar and args.canSwitch then
        table.insert(actionMenuInfos, {
            objName = "QuickBarSwitch",
            text = Language.LUA_CONTROLLER_INV_DEPOT_QUICK_BAR_SWITCH,
            action = function()
                Notify(MessageConst.START_SWITCH_SLOT_ON_FAC_QUICK_BAR, {
                    fromIndex = args.fromIndex,
                })
            end
        })
    end



    if isItemBag and self.canSplit and count > 1 then
        table.insert(actionMenuInfos, {
            objName = "Split",
            text = Language.LUA_ITEM_ACTION_SPLIT,
            action = function()
                UIUtils.splitItem(self.slotIndex)
            end
        })
    end



    if count > 0 and self.canDestroy and inventory:CanDestroyItem(scope, id) then

        if isItemBag then
            table.insert(actionMenuInfos, {
                objName = "Drop",
                text = Language.LUA_ITEM_ACTION_DROP,
                action = function()
                    inventory:AbandonItemInItemBag(scope, { self.slotIndex })
                end
            })
        end


        if isFacDepot then
            table.insert(actionMenuInfos, {
                objName = "Destroy",
                text = Language.LUA_ITEM_ACTION_DESTROY,
                action = function()
                    local items = { { id = id, count = count } }
                    Notify(MessageConst.SHOW_POP_UP, {
                        content = Language.LUA_DESTROY_ITEM_CONFIRM_TEXT,
                        warningContent = Language.LUA_DESTROY_ITEM_CONFIRM_WARNING_TEXT,
                        items = items,
                        onConfirm = function()
                            inventory:DestroyInFactoryDepot(scope, chapterId, { [id] = count })
                        end,
                    })
                end
            })
        end
    end



    if isFullBottle and itemMoveValid and count > 0 and (isItemBag or isFacDepot) then
        table.insert(actionMenuInfos, {
            objName = "ClearLiquid",
            text = Language.LUA_ITEM_ACTION_CLEAR_LIQUID,
            action = function()
                UIManager:Open(PanelId.ClearBottlePopUp, {
                    slotIndex = self.slotIndex,
                    fromDepot = isFacDepot,
                    itemId = id,
                    itemCount = count,
                })
            end,
        })
    end



    if isFullGasJar and itemMoveValid and count > 0 and (isItemBag or isFacDepot) then
        table.insert(actionMenuInfos, {
            objName = "ClearGas",
            text = Language.LUA_ITEM_ACTION_CLEAR_GAS,
            action = function()
                UIManager:Open(PanelId.ClearBottlePopUp, {
                    slotIndex = self.slotIndex,
                    fromDepot = isFacDepot,
                    itemId = id,
                    itemCount = count,
                })
            end,
        })
    end



    table.insert(actionMenuInfos, {
        objName = "ShowTips",
        text = Language.LUA_ITEM_ACTION_SHOW_TIPS,
        action = function()
            self:ShowTips()
        end
    })

    if args.extraButtons then
        table.insert(actionMenuInfos, {
            text = Language.LUA_ITEM_ACTION_EXTRA_TITLE,
        })
        for _, btn in ipairs(args.extraButtons) do
            if btn.gameObject.activeInHierarchy then
                table.insert(actionMenuInfos, {
                    objName = btn.name,
                    text = btn.hintText,
                    action = function()
                        btn.onClick:Invoke()
                    end
                })
            end
        end
    end



    if isQuickBar and args.canSwitch then
        table.insert(actionMenuInfos, {
            objName = "QuickBarClear",
            text = Language.LUA_CONTROLLER_QUICK_BAR_CLEAR,
            action = function()
                FactoryUtils.clearQuickBarSlot(CSIndex(args.fromIndex))
            end
        })
    end

    if self.customChangeActionMenuFunc then
        self.customChangeActionMenuFunc(actionMenuInfos)
    end

    return actionMenuInfos
end






Item._OnHoverChange = HL.Method(HL.Boolean) << function(self, isHover)
    if self.m_delayHoverTimer >= 0 then
        self.m_delayHoverTimer = self:_ClearTimer(self.m_delayHoverTimer)
    end

    if not self.m_enableHoverTips or string.isEmpty(self.id) then
        return
    end

    local showHover, delay, targetRect
    if DeviceInfo.usingController then
        showHover = isHover and self.view.button.groupEnabled and not self.extraInfo.isSideTips
        if isHover and not self.view.button.groupEnabled then


            self.m_delayHoverTimer = self:_StartTimer(0.2, function()
                if self.view.button.isNaviTarget then
                    self:_OnHoverChange(true)
                end
            end)
            return
        end
        delay = 0
        targetRect = self.view.transform
    elseif DeviceInfo.usingKeyboard then
        showHover = isHover and not self.m_isSelected
        delay = self.view.config.HOVER_TIP_DELAY
    else
        showHover = false
    end
    if showHover then
        local posType
        if DeviceInfo.usingController then
            if self.view.config.HOVER_TIPS_POS_RIGHT_TOP then
                posType = UIConst.UI_TIPS_POS_TYPE.RightTop
            else
                posType = UIConst.UI_TIPS_POS_TYPE.RightDown
            end
        end
        Notify(MessageConst.SHOW_COMMON_HOVER_TIP, {
            itemId = self.id,
            delay = delay,
            targetRect = targetRect,
            posType = posType,
        })
        self.m_showingHover = true
    else
        self:_CloseHoverTips()
    end
end

Item._CloseHoverTips = HL.Method(HL.Opt(HL.Boolean)) << function(self, noAnimation)
    if self.m_showingHover then
        Notify(MessageConst.HIDE_COMMON_HOVER_TIP, { noAnimation = noAnimation })
        self.m_showingHover = false
    end
end

Item.AddHoverBinding = HL.Method(HL.String, HL.Function).Return(HL.Number) << function(self, actionId, action)
    local groupId = self.view.button.hoverBindingGroupId
    if groupId <= 0 then



        return -1
    end
    return InputManagerInst:CreateBindingByActionId(actionId, action, groupId)
end

Item.SetEnableHoverTips = HL.Method(HL.Boolean) << function(self, enabled)
    self.m_enableHoverTips = enabled

    if DeviceInfo.usingController and self.view.button.isNaviTarget then
        if enabled and not self.m_showingHover then
            self:_OnHoverChange(true)
        elseif not enabled and self.m_showingHover then
            self:_CloseHoverTips()
        end
    end
end



HL.Commit(Item)
return Item
