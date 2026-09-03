local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharFormationTacticalItem = HL.Class('CharFormationTacticalItem', UIWidgetBase)

local STATE_NAME = {
    NORMAL = "normal",
    EMPTY = "empty",
}

CharFormationTacticalItem.m_args = HL.Field(HL.Table)

CharFormationTacticalItem.m_isNaviTarget = HL.Field(HL.Boolean) << false



CharFormationTacticalItem._OnFirstTimeInit = HL.Override() << function(self)
    self.view.btnItem.onClick:AddListener(function()
        self:_OnBtnItemClicked()
    end)
    self.view.btnItem.onIsNaviTargetChanged = function(active)
        if active then
            Notify(MessageConst.CHAR_INFO_CLOSE_ATTR_TIP)
            Notify(MessageConst.CHAR_INFO_CLOSE_SKILL_TIP)
            local isOpen = UIManager:IsOpen(PanelId.CharInfoTips)
            if not isOpen and self:CanChangeItem() and not string.isEmpty(self.m_args.itemId) then

                local itemTipsArgs = {
                    itemId = self.m_args.itemId,
                    isLocked = self.m_args.isLocked,
                    charTemplateId = self.m_args.charTemplateId,
                    charInstId = self.m_args.charInstId,
                    targetTransform = self.m_args.tipNode or self.view.transform,
                    tipPosType = self.m_args.tipPosType,
                }
                Notify(MessageConst.SHOW_CHAR_TACTICAL_ITEM_TIP, itemTipsArgs)
            end
        else
            Notify(MessageConst.CHAR_INFO_CLOSE_INFO_TIP)
        end
        self.m_isNaviTarget = active
    end
end















CharFormationTacticalItem.InitCharFormationTacticalItem = HL.Method(HL.Table) << function(self, args)
    self.m_args = args

    self:_FirstTimeInit()

    local itemId = self.m_args.itemId

    local isEmpty = string.isEmpty(itemId)
    self.view.icon.gameObject:SetActive(not isEmpty)
    self.view.color.gameObject:SetActive(not isEmpty)
    self.view.plusIcon.gameObject:SetActive(isEmpty)
    self.view.btnItem.enabled = self.m_args.isClickable
    if not isEmpty then

        local itemCfg = Tables.itemTable[itemId]
        self.view.icon:LoadSprite(UIConst.UI_SPRITE_ITEM, itemCfg.iconId)
        UIUtils.setItemRarityImage(self.view.color, itemCfg.rarity)
        local itemCount = GameInstance.player.inventory:GetTacticalItemCount(
            Utils.getCurrentScope(), itemId, self.m_args.charInstId)
        self.view.numText.text = UIUtils.setCountColor(UIUtils.getNumString(itemCount), itemCount <= 0)
        self.view.txtCarry.text = UIUtils.setCountColor(self.m_args.isLocked and
            Language.LUA_TACTICAL_ITEM_CARRY_TRAIL or Language.LUA_TACTICAL_ITEM_CARRY)


        local alpha = itemCount < CS.Beyond.Gameplay.TacticalItemUtil.GetTacticalItemChargeCount(itemId) and 0.5 or 1
        local iconColor = self.view.icon.color
        iconColor.a = alpha
        self.view.icon.color = iconColor
    end
    local stateName = isEmpty and STATE_NAME.EMPTY or STATE_NAME.NORMAL
    self.view.state:SetState(stateName)

    self.view.disable.gameObject:SetActive(self.m_args.isForbidden == true)
    self.view.lock.gameObject:SetActive(self.m_args.isLocked == true)
end

CharFormationTacticalItem.CanChangeItem = HL.Method().Return(HL.Boolean) << function(self)
    if not self.m_args.isClickable or self.m_args.isForbidden or self.m_args.isLocked then
        return false
    end
    return true
end



CharFormationTacticalItem._OnBtnItemClicked = HL.Method() << function(self)
    if self.m_args.isForbidden then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_TACTICAL_ITEM_FORBIDDEN)
    elseif string.isEmpty(self.m_args.itemId) then
        if self.m_args.isLocked then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_TACTICAL_ITEM_TRAIL_LOCKED)
        else
            self:GoToCharInfoEquipPage()
        end
    else
        if self.m_isNaviTarget then
            self:GoToCharInfoEquipPage()
        else
            self:_ShowTips()
        end
    end
end

CharFormationTacticalItem.GoToCharInfoEquipPage = HL.Method() << function(self)
    if not Utils.isSystemUnlocked(GEnums.UnlockSystemType.Equip) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SYSTEM_EQUIP_LOCKED)
        return
    end

    CharInfoUtils.openCharInfoBestWay({
        pageType = UIConst.CHAR_INFO_PAGE_TYPE.EQUIP,
        initCharInfo = {
            instId = self.m_args.charInstId,
            templateId = self.m_args.charTemplateId,
            isSingleChar = true,
        },
        forceSkipIn = true,
        extraArg = {
            slotType = UIConst.CHAR_INFO_EQUIP_SLOT_MAP.TACTICAL
        }
    })
end

CharFormationTacticalItem._ShowTips = HL.Method() << function(self)

    local itemTipsArgs = {
        itemId = self.m_args.itemId,
        isLocked = self.m_args.isLocked,
        charTemplateId = self.m_args.charTemplateId,
        charInstId = self.m_args.charInstId,
        targetTransform = self.m_args.tipNode or self.view.transform,
        tipPosType = self.m_args.tipPosType,
    }
    Notify(MessageConst.SHOW_CHAR_TACTICAL_ITEM_TIP, itemTipsArgs)
end

HL.Commit(CharFormationTacticalItem)
return CharFormationTacticalItem
