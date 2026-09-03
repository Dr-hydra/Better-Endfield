local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

EquipTechEquipInfo = HL.Class('EquipTechEquipInfo', UIWidgetBase)


EquipTechEquipInfo._OnFirstTimeInit = HL.Override() << function(self)
end

EquipTechEquipInfo.InitEquipTechEquipInfo = HL.Method(HL.String) << function(self, templateId)
    self:_FirstTimeInit()

    local _, itemData = Tables.itemTable:TryGetValue(templateId)
    local _, equipData = Tables.equipTable:TryGetValue(templateId)
    if not equipData or not itemData then
        self.view.gameObject:SetActive(false)
        return
    end
    self.view.nameTxt.text = itemData.name
    self.view.levelTxt.text = tostring(equipData.minWearLv)
    local equipTypeName = Language[UIConst.CHAR_INFO_EQUIP_TYPE_TILE_PREFIX .. LuaIndex(equipData.partType:ToInt())]
    self.view.partTypeTxt.text = equipTypeName
    self.view.rarityLine.color = UIUtils.getItemRarityColor(itemData.rarity)
    self.view.iconImage:LoadSprite(UIConst.UI_SPRITE_EQUIP, UIConst.EQUIP_TYPE_TO_ICON_NAME[equipData.partType])
end

HL.Commit(EquipTechEquipInfo)
return EquipTechEquipInfo

