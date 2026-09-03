local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SuitDescCell = HL.Class('SuitDescCell', UIWidgetBase)

SuitDescCell.m_descCellCache = HL.Field(HL.Forward("UIListCache"))

SuitDescCell._OnFirstTimeInit = HL.Override() << function(self)
    self.m_descCellCache = UIUtils.genCellCache(self.view.descCell)
end


SuitDescCell.InitSuitDescCellByEquipTemplateId = HL.Method(HL.String) << function(self, equipTemplateId)
    self:_FirstTimeInit()

    local _, equipTemplate = Tables.equipTable:TryGetValue(equipTemplateId)
    local suitId = equipTemplate.suitID
    local hasValue, EquipSuitClientDataList = Tables.equipSuitTable:TryGetValue(suitId)
    self.view.gameObject:SetActive(hasValue)
    self.view.suitName.gameObject:SetActive(hasValue)
    if not hasValue then
        self.m_descCellCache:Refresh(0)
        return
    end

    local suitDescList = EquipSuitClientDataList.list
    local count = suitDescList.Count
    self.m_descCellCache:Refresh(count, function(cell, index)
        local descInfo = suitDescList[CSIndex(index)]

        cell.descText.text = descInfo.desc
        self.view.suitName.text = descInfo.suitName
    end)
    if hasValue then
        self.view.equipmentLogo.gameObject:SetActive(true)
        local suitData = EquipSuitClientDataList.list[0]
        self.view.equipmentLogo:LoadSprite(UIUtils.getSpritePath(
            UIConst.UI_SPRITE_EQUIPMENT_LOGO_BIG,
            suitData.suitIcon))
    else
        self.view.equipmentLogo.gameObject:SetActive(false)
    end

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.transform)
end
SuitDescCell.InitSuitDescCell = HL.Method(HL.String, HL.Number, HL.Function, HL.Opt(HL.Boolean)) << function(self,
                                                                                                           suitId,
                                                                                                 suitCount,
                                                                                         callback, onlyEnable)
    self:_FirstTimeInit()

    local hasValue, EquipSuitClientDataList = Tables.equipSuitTable:TryGetValue(suitId)
    self.view.gameObject:SetActive(hasValue)
    if not hasValue then
        logger.error("SuitDescCell->Can't find suitId: " .. suitId .. " in equipSuitTable")
        return
    end
    local suitDescList = EquipSuitClientDataList.list
    local count = onlyEnable and 1 or suitDescList.Count
    local offsetCSIndex = 0
    local reachIndex = -1
    for index, descInfo in pairs(suitDescList) do
        local isEnable = suitCount >= descInfo.equipCnt
        if isEnable then
            reachIndex = index
        end

        if onlyEnable then
            if isEnable then
                offsetCSIndex = index
            end
        end
    end

    local isSuit, suitList = Tables.equipSuitTable:TryGetValue(suitId)
    if isSuit then
        local suitData = suitList.list[0]
        self.view.equipmentLogo:LoadSprite(UIUtils.getSpritePath(
            UIConst.UI_SPRITE_EQUIPMENT_LOGO_BIG,
            suitData.suitIcon))
        self.view.equipmentLogo.gameObject:SetActive(true)
    else
        self.view.equipmentLogo.gameObject:SetActive(false)
    end

    local suitName = ""
    local suitNameColor = self.view.config.SUIT_DISABLE_COLOR

    self.m_descCellCache:Refresh(count, function(cell, index)
        local realIndex = CSIndex(index) + offsetCSIndex
        local descInfo = suitDescList[CSIndex(index) + offsetCSIndex]
        local isEnable = reachIndex == realIndex
        local descColor = isEnable and self.view.config.SUIT_ENABLE_COLOR or self.view.config.SUIT_DISABLE_COLOR
        local reachIconName = isEnable and self.view.config.SUIT_ENABLE_ICON or self.view.config.SUIT_DISABLE_ICON

        cell.reachIcon:LoadSprite(UIConst.UI_SPRITE_ITEM_TIPS, reachIconName)
        cell.descText.text = descInfo.desc

        cell.reachIcon.color = descColor
        cell.descText.color = descColor
        suitName = descInfo.suitName
        if isEnable then
            suitNameColor = descColor
        end
    end)

    self.view.suitName.text = suitName
    self.view.suitName.color = suitNameColor

    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        callback({
            cell = self,
            suitId = suitId,
            transform = self.view.button.transform,
        })
    end)

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.transform)
end

HL.Commit(SuitDescCell)
return SuitDescCell

