local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WeaponCompatibleNode = HL.Class('WeaponCompatibleNode', UIWidgetBase)

WeaponCompatibleNode.m_cellCache = HL.Field(HL.Forward("UIListCache"))

WeaponCompatibleNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_cellCache = UIUtils.genCellCache(self.view.content)
end

WeaponCompatibleNode.InitWeaponCompatibleNode = HL.Method(HL.Number) << function(self, gemInstId)
    self:_FirstTimeInit()

    local isPerfectMatch, matchList = UIUtils.getGemWishListPerfectMatch(gemInstId)
    self.view.gameObject:SetActive(isPerfectMatch)
    if not isPerfectMatch then
        return
    end

    self.m_cellCache:Refresh(#matchList, function(cell, index)
        local weaponTemplateId = matchList[index]
        local _, itemCfg = Tables.itemTable:TryGetValue(weaponTemplateId)
        if itemCfg then
            cell.contentNode.iconImg:LoadSprite(UIConst.UI_SPRITE_ITEM, itemCfg.iconId)
            cell.contentNode.nameTxt.text = itemCfg.name
        end
    end)
end

HL.Commit(WeaponCompatibleNode)
return WeaponCompatibleNode

