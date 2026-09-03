local Item = require_ex('UI/Widgets/Item')

ItemAdventureReward = HL.Class('ItemAdventureReward', Item)


ItemAdventureReward._OnFirstTimeInit = HL.Override() << function(self)

end

ItemAdventureReward.m_rewardInfo = HL.Field(HL.Table)

ItemAdventureReward.InitItemAdventureReward = HL.Method(HL.Table) << function(self, rewardInfo)
    self:_FirstTimeInit()

    self.m_rewardInfo = rewardInfo
    self.view.icon:SetAlpha(UIConst.ITEM_EXIST_TRANSPARENCY)
    self.view.rewardedCover.gameObject:SetActiveIfNecessary(rewardInfo.gained)
    self:_UpdateBaseItem()
    self:_UpdateCornerMark()
end

ItemAdventureReward._UpdateBaseItem = HL.Method() << function(self)
    local rewardInfo = self.m_rewardInfo
    self.extraInfo = {}

    if self.view.config.SHOW_ITEM_TIPS_ON_R3 then
        self:AddHoverBinding("show_item_tips", function()
            self:ShowTips()
        end)
    end

    if self.id ~= rewardInfo.id then
        self.id = rewardInfo.id
        self:SetSelected(false)
    end
    if self.instId ~= rewardInfo.instId then
        if rewardInfo.instId and rewardInfo.instId > 0 then
            self.instId = rewardInfo.instId
        else
            self.instId = nil
        end
        self:SetSelected(false)
    end
    local data = Tables.itemTable:GetValue(rewardInfo.id)

    self:_UpdateRewardIcon(data)
    self:_UpdateRewardWeaponAddon(rewardInfo)

    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        local posInfo
        if DeviceInfo.usingController then
            posInfo = {
                tipsPosType = UIConst.UI_TIPS_POS_TYPE.RightDown,
                tipsPosTransform = self.transform,
                isSideTips = true,
            }
        end
        self:ShowTips(posInfo)
    end)
    self:SetExtraInfo(({ isSideTips = DeviceInfo.usingController }))
    self.view.button.clickHintTextId = "virtual_mouse_hint_item_tips"

    self.view.button.onHoverChange:RemoveAllListeners()
    if self.view.config.SHOW_HOVER_TIP then
        self.view.button.onHoverChange:AddListener(function(isHover)
            if isHover and not self.m_isSelected then
                Notify(MessageConst.SHOW_COMMON_HOVER_TIP, {
                    itemId = rewardInfo.id,
                    delay = self.view.config.HOVER_TIP_DELAY,
                })
                self.m_showingHover = true
            else
                Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
            end
        end)
    end
end

ItemAdventureReward._UpdateCornerMark = HL.Method() << function(self)
    local rewardInfo = self.m_rewardInfo

    if rewardInfo.gained then
        self.view.rewardCornerMarkState:SetState("NoMark")
    else
        if rewardInfo.isFirst then
            self.view.rewardCornerMarkState:SetState("FirstMark")
        elseif rewardInfo.isExtra then
            self.view.rewardCornerMarkState:SetState("ChallengeMark")
        else
            self.view.rewardCornerMarkState:SetState("NoMark")
        end
    end
end

ItemAdventureReward._UpdateRewardIcon = HL.Method(HL.Any) << function(self, data)
    self.view.icon:InitItemIcon(data.id, self.view.config.USE_BIG_ICON)
    if self.view.compositeIconBG then
        self.view.compositeIconBG.gameObject:SetActive(not self.view.icon.showRarity)
    end

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

ItemAdventureReward._UpdateRewardWeaponAddon = HL.Method(HL.Any) << function(self, data)
    local itemCfg = Tables.itemTable:GetValue(data.id)
    local itemType = itemCfg.type
    local isWeapon = itemType == GEnums.ItemType.Weapon

    self.view.potentialStar.gameObject:SetActive(isWeapon)
    if not isWeapon then
        return
    end

    local weaponInstData = data.instId and CharInfoUtils.getWeaponByInstId(data.instId) or nil
    self.view.potentialStar:InitWeaponPotentialStar(weaponInstData and weaponInstData.refineLv or 0)
end

HL.Commit(ItemAdventureReward)
return ItemAdventureReward

