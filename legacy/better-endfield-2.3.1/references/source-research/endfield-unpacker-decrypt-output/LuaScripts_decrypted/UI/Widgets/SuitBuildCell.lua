local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SuitBuildCell = HL.Class('SuitBuildCell', UIWidgetBase)

SuitBuildCell.m_suitCellCache = HL.Field(HL.Forward("UIListCache"))


SuitBuildCell._OnFirstTimeInit = HL.Override() << function(self)
    self.m_suitCellCache = UIUtils.genCellCache(self.view.suitCell)
end

SuitBuildCell.InitSuitBuildCell = HL.Method(HL.String) << function(self, equipTemplateId)
    self:_FirstTimeInit()

    local _, equipTemplate = Tables.equipTable:TryGetValue(equipTemplateId)
    local suitId = equipTemplate.suitID
    if not suitId then
        self.view.gameObject:SetActive(false)
        return
    end

    local hasValue, equipSuitClientDataList = Tables.equipSuitTable:TryGetValue(suitId)
    self.view.gameObject:SetActive(hasValue)
    if not hasValue then
        self.view.gameObject:SetActive(false)
        return
    end

    self.view.gameObject:SetActive(true)
    local equipList = equipSuitClientDataList.equipList
    local suitName = equipSuitClientDataList.list[1].suitName
    self.view.suitNameText.text = suitName
    self.m_suitCellCache:Refresh(equipList.Count, function(cell, luaIndex)
        local equipId = equipList[CSIndex(luaIndex)]
        local _, data = Tables.equipTable:TryGetValue(equipId)

        cell.cellLight.imageIcon:LoadSprite(UIConst.UI_SPRITE_EQUIP, UIConst.CHAR_INFO_EQUIP_LIST_SPRITE_NAME_PREFIX .. LuaIndex(data.partType:ToInt()))
        cell.cellDim.imageIcon:LoadSprite(UIConst.UI_SPRITE_EQUIP, UIConst.CHAR_INFO_EQUIP_LIST_SPRITE_NAME_PREFIX .. LuaIndex(data.partType:ToInt()))

        cell.cellLight.textName.text = data.name
        cell.cellDim.textName.text = data.name

        local isSameEquip = equipId == equipTemplateId
        cell.cellLight.gameObject:SetActive(isSameEquip)
        cell.cellDim.gameObject:SetActive(not isSameEquip)
    end)
end

HL.Commit(SuitBuildCell)
return SuitBuildCell

