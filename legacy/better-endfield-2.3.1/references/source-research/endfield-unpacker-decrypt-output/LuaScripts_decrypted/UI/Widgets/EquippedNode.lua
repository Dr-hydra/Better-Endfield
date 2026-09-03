local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

EquippedNode = HL.Class('EquippedNode', UIWidgetBase)


EquippedNode._OnFirstTimeInit = HL.Override() << function(self)

end

EquippedNode.InitEquippedNode = HL.Method() << function(self)
    self:_FirstTimeInit()
end

EquippedNode.InitEquipNodeByWeaponInstId = HL.Method(HL.Number) << function(self, weaponInstId)
    self:_FirstTimeInit()


    local weaponInstData = CharInfoUtils.getWeaponByInstId(weaponInstId)
    local isEquipped = weaponInstData and weaponInstData.equippedCharServerId > 0
    self.view.gameObject:SetActive(isEquipped)
    if isEquipped then
        self:_SetCharIcon(weaponInstData.equippedCharServerId)
    end
end

EquippedNode.InitEquippedNodeByEquipInstId = HL.Method(HL.Number) << function(self, equipInstId)
    self:_FirstTimeInit()


    local equipInstData = CharInfoUtils.getEquipByInstId(equipInstId)
    local isEquipped = equipInstData and equipInstData.equippedCharServerId > 0
    self.view.gameObject:SetActive(isEquipped)
    if isEquipped then
        self:_SetCharIcon(equipInstData.equippedCharServerId)
    end
end

EquippedNode.InitEquippedNodeByGemInstId = HL.Method(HL.Number) << function(self, gemInstId)
    self:_FirstTimeInit()


    local gemInstData = CharInfoUtils.getGemByInstId(gemInstId)
    local isEquipped = gemInstData and gemInstData.weaponInstId > 0
    self.view.gameObject:SetActive(isEquipped)
    if isEquipped then
        self:_SetWeaponIcon(gemInstData.weaponInstId)
    end
end

EquippedNode._SetCharIcon = HL.Method(HL.Number) << function(self, charInstId)

    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local spriteName = UIConst.UI_CHAR_HEAD_PREFIX .. charInfo.templateId
    self.view.iconImg:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD, spriteName)
    self.view.nameTxt.text = Tables.characterTable[charInfo.templateId].name
end

EquippedNode._SetWeaponIcon = HL.Method(HL.Number) << function(self, weaponInstId)

    local weaponInstData = CharInfoUtils.getWeaponByInstId(weaponInstId)
    local itemData = Tables.itemTable[weaponInstData.templateId]
    self.view.iconImg:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)
    self.view.nameTxt.text = itemData.name
end

HL.Commit(EquippedNode)
return EquippedNode

