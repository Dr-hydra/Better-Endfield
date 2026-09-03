local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WeaponGemSlimNode = HL.Class('WeaponGemSlimNode', UIWidgetBase)


WeaponGemSlimNode._OnFirstTimeInit = HL.Override() << function(self)

end

WeaponGemSlimNode.InitWeaponGemSlimNode = HL.Method(HL.Number) << function(self, gemInstId)
    self:_FirstTimeInit()

    local gemInst = CharInfoUtils.getGemByInstId(gemInstId)
    self:InitWeaponGemSlimeNodeByInst(gemInst)
end

WeaponGemSlimNode.InitWeaponGemSlimeNodeByInst = HL.Method(HL.Userdata) << function(self, gemInst)
    self:_FirstTimeInit()

    local hasGem = gemInst ~= nil
    self.view.emptyNode.gameObject:SetActive(not hasGem)
    self.view.existNode.gameObject:SetActive(hasGem)
    if not hasGem then
        return
    end

    if gemInst then
        local templateId = gemInst.templateId
        local _, itemData = Tables.itemTable:TryGetValue(templateId)
        if itemData then
            self.view.gemNameTxt.text = itemData.name
            UIUtils.setItemRarityImage(self.view.qualityImg, itemData.rarity)
            self.view.icon:LoadSprite(UIConst.UI_SPRITE_ITEM, itemData.iconId)
        end
    end
end

HL.Commit(WeaponGemSlimNode)
return WeaponGemSlimNode

