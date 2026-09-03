local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

EquipCell = HL.Class('EquipCell', UIWidgetBase)

EquipCell.equipId = HL.Field(HL.Int) << -1

EquipCell.equipData = HL.Field(HL.Userdata)




EquipCell._OnFirstTimeInit = HL.Override() << function(self)
end

EquipCell.InitEquipCell = HL.Method(HL.Any, HL.Opt(HL.Function)) << function(self, equip, onClick)
    self:_FirstTimeInit()

    self.equipId = equip.equipId
    local charInstId = equip.charInstId
    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local _, equipData = Tables.equipTable:TryGetValue(self.equipId)

    self.equipData = equipData

    local partType = equipData.partType
    local spriteName = UIConst.UI_EQUIP_PART_ICON_PREFIX .. string.format("%d", partType)
    self.view.midLeft.imagePart:LoadSprite(UIConst.UI_SPRITE_EQUIP_PART_ICON, spriteName)

    if charInfo then
        local charId = charInfo.charId


        local charSpriteName = UIConst.UI_CHAR_HEAD_PREFIX .. charId
        self.view.imageChar:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD, charSpriteName)
        self.view.imageCharMask.gameObject:SetActive(true)
    else
        self.view.imageCharMask.gameObject:SetActive(false)
    end

    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        if onClick then
            onClick()
        end
    end)
end

HL.Commit(EquipCell)
return EquipCell
