local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WeaponUpgradeAttributeNode = HL.Class('WeaponUpgradeAttributeNode', UIWidgetBase)

WeaponUpgradeAttributeNode.m_mainAttributeCellCache = HL.Field(HL.Forward("UIListCache"))

WeaponUpgradeAttributeNode.m_subAttributeCellCache = HL.Field(HL.Forward("UIListCache"))

WeaponUpgradeAttributeNode.InitWeaponUpgradeAttributeNode = HL.Method(HL.Table) << function(self, arg)
    self:_FirstTimeInit()

    local fromLv = arg.fromLv
    local fromBreakthroughLv = arg.fromBreakthroughLv
    local toLv = arg.toLv
    local toBreakthroughLv = arg.toBreakthroughLv
    local weaponInstId = arg.weaponInstId
    local gemInstId = arg.gemInstId or 0

    local mainAttributeList, subAttributeList = CharInfoUtils.getWeaponShowAttributes(weaponInstId, fromLv)
    local targetMainAttributeList, targetSubAttributeList = CharInfoUtils.getWeaponShowAttributes(weaponInstId, toLv)

    self.m_mainAttributeCellCache:Refresh(#mainAttributeList, function(cell, index)
        local attributeInfo = mainAttributeList[index]
        local targetAttributeInfo = targetMainAttributeList[index]
        self:_RefreshAttributeCell(cell, attributeInfo, targetAttributeInfo)
    end)
    self.m_subAttributeCellCache:Refresh(#subAttributeList, function(cell, index)
        local attributeInfo = subAttributeList[index]
        local targetAttributeInfo = targetSubAttributeList[index]
        self:_RefreshAttributeCell(cell, attributeInfo, targetAttributeInfo)
    end)
end

WeaponUpgradeAttributeNode._RefreshAttributeCell = HL.Method(HL.Table, HL.Table, HL.Table) << function(self, cell, attributeInfo, targetAttributeInfo)
    local attributeKey = Const.ATTRIBUTE_TYPE_2_ATTRIBUTE_DATA_KEY[attributeInfo.attributeType]

    cell.mainText.text = attributeInfo.showName
    cell.attributeIcon:LoadSprite(UIConst.UI_SPRITE_ATTRIBUTE_ICON, attributeInfo.iconName)
    cell.fromValue.text = "+" .. attributeInfo.showValue
    cell.toValue.text = "+" .. targetAttributeInfo.showValue
end

WeaponUpgradeAttributeNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_mainAttributeCellCache = UIUtils.genCellCache(self.view.mainAttributeCell)
    self.m_subAttributeCellCache = UIUtils.genCellCache(self.view.subAttributeCell)
end


HL.Commit(WeaponUpgradeAttributeNode)
return WeaponUpgradeAttributeNode

