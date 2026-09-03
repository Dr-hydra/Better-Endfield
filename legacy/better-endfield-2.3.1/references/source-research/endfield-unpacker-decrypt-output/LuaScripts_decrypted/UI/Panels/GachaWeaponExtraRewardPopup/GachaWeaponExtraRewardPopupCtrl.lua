
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GachaWeaponExtraRewardPopup

GachaWeaponExtraRewardPopupCtrl = HL.Class('GachaWeaponExtraRewardPopupCtrl', uiCtrl.UICtrl)






GachaWeaponExtraRewardPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}


GachaWeaponExtraRewardPopupCtrl.m_info = HL.Field(HL.Table)

GachaWeaponExtraRewardPopupCtrl.m_itemInfo = HL.Field(HL.Table)













GachaWeaponExtraRewardPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self.m_info = arg
    self:_InitData()

    self:_RefreshAllUI()
end

GachaWeaponExtraRewardPopupCtrl.OnClose = HL.Override() << function(self)
    local onComplete = self.m_info.onComplete
    self.m_info = nil
    if onComplete then
        onComplete()
    end
end



GachaWeaponExtraRewardPopupCtrl._InitData = HL.Method() << function(self)
    self.m_itemInfo = {
        weaponInfo = nil,
        boxInfo = nil,
    }

    local itemId = self.m_info.itemId
    local poolId = self.m_info.poolId
    local hasCfg, itemCfg = Tables.itemTable:TryGetValue(itemId)
    if not hasCfg then
        logger.error("【武器抽卡额外赠礼】 itemId不存在！" .. itemId)
        return
    end
    local _, poolCfg = Tables.gachaWeaponPoolTable:TryGetValue(poolId)
    if not poolCfg then
        logger.error("【武器抽卡额外赠礼】 poolId不存在！" .. poolId)
        return
    end

    if itemCfg.type == GEnums.ItemType.Weapon then
        local weaponCfg = Tables.weaponBasicTable[itemId]
        local weaponTypeIconName = UIConst.WEAPON_EXHIBIT_WEAPON_TYPE_ICON_PREFIX .. weaponCfg.weaponType:ToInt()

        self.m_itemInfo.weaponInfo = {
            title = poolCfg.loopRewardShowTitle,
            name = itemCfg.name,
            icon = itemCfg.iconId,
            typeIcon = weaponTypeIconName,
            typeName = UIUtils.getItemTypeName(itemId)
        }
    elseif itemCfg.type == GEnums.ItemType.ItemCase then
        self.m_itemInfo.boxInfo = {
            title = Language.LUA_GACHA_WEAPON_LOOP_REWARD_BOX_TITLE,
            name = itemCfg.name,
            icon = itemCfg.iconId,
        }
    else
        logger.error("【武器抽卡额外赠礼】 当前赠礼展示界面不支持这种itemType：" .. tostring(itemCfg.type))
    end
end



GachaWeaponExtraRewardPopupCtrl._InitUI = HL.Method() << function(self)
    self.view.fullScreenCloseBtn.onClick:AddListener(function()
        self:PlayAnimationOutAndClose()
    end)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

GachaWeaponExtraRewardPopupCtrl._RefreshAllUI = HL.Method() << function(self)
    if self.m_itemInfo.weaponInfo then

        local weaponInfo = self.m_itemInfo.weaponInfo
        self.view.mainStateCtrl:SetState("Weapon")
        self.view.titleTxt.text = weaponInfo.title
        local weaponNode = self.view.weaponNode
        weaponNode.nameTxt.text = weaponInfo.name
        weaponNode.nameShadowTxt.text = weaponInfo.name
        weaponNode.weaponIcon:LoadSprite(UIConst.UI_SPRITE_GACHA_WEAPON, weaponInfo.icon)
        weaponNode.typeIconImg:LoadSprite(UIConst.UI_SPRITE_WEAPON_EXHIBIT, weaponInfo.typeIcon)
        weaponNode.weaponTypeTxt.text = weaponInfo.typeName
    elseif self.m_itemInfo.boxInfo then

        local boxInfo = self.m_itemInfo.boxInfo
        self.view.mainStateCtrl:SetState("Box")
        self.view.titleTxt.text = boxInfo.title
        local boxNode = self.view.boxNode
        boxNode.nameTxt.text = boxInfo.name
        boxNode.iconImg:LoadSprite(UIConst.UI_SPRITE_SHOP_WEAPON_BOX, boxInfo.icon)
    end
end


HL.Commit(GachaWeaponExtraRewardPopupCtrl)
