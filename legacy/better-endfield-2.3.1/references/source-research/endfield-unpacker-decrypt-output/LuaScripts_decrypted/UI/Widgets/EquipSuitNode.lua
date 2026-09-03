local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

EquipSuitNode = HL.Class('EquipSuitNode', UIWidgetBase)

EquipSuitNode.m_suitDescCellCache = HL.Field(HL.Forward("UIListCache"))


EquipSuitNode._OnFirstTimeInit = HL.Override() << function(self)
    self.m_suitDescCellCache = UIUtils.genCellCache(self.view.suitDescCell)
end


EquipSuitNode.InitEmptyEquipSuitNode = HL.Method(HL.String, HL.Opt(HL.Number, HL.Number, HL.Number)) << function(self, equipTemplateId, tryCharInstId, tryEquipInstId, trySlotIndex)
    self:_FirstTimeInit()

    self.view.charEmptySuitNode.gameObject:SetActive(false)

    local equipCfg = Tables.equipTable:GetValue(equipTemplateId)
    local hasSuit = equipCfg.suitID and not string.isEmpty(equipCfg.suitID)

    self.view.equipEmptySuitNode.gameObject:SetActive(not hasSuit)
    self.view.suitGroupNode.gameObject:SetActive(hasSuit)
    self.view.suitTitle.gameObject:SetActive(hasSuit)

    if not hasSuit then
        return
    end

    local hasCharInst = tryCharInstId and tryCharInstId > 0
    local suitInfoDict = {}
    if hasCharInst then
        suitInfoDict = self:_CollectSuitInfoDictFromCharInst(tryCharInstId, tryEquipInstId, trySlotIndex)
    end

    local totalSuitCfg = Tables.equipSuitTable:GetValue(equipCfg.suitID)
    local firstSuitCfg = totalSuitCfg.list[0]
    local firstSuitEnable = suitInfoDict and suitInfoDict[firstSuitCfg.suitID] and suitInfoDict[firstSuitCfg.suitID] >= firstSuitCfg.equipCnt or (not hasCharInst)
    local firstSuitDesc = firstSuitEnable and Language.LUA_EQUIP_SKILL_ENABLE_FORMAT or Language.LUA_EQUIP_SKILL_DISABLE_FORMAT
    self.view.suitName.text = string.format(firstSuitDesc, firstSuitCfg.suitName)
    self.view.suitLogo:LoadSprite(self:_GetLogoFolder(), firstSuitCfg.suitLogoName)

    local curEquipCount = 0
    local curEquipSuitInfoDict = self:_CollectSuitInfoDictFromCharInst(tryCharInstId)
    for suitId, equipCount in pairs(curEquipSuitInfoDict) do
        if suitId == firstSuitCfg.suitID then
            curEquipCount = equipCount
            break
        end
    end
    self.view.suitNum.text = string.format(Language.LUA_CHAR_INFO_SUIT_EQUIP_NUM_FORMAT, curEquipCount, firstSuitCfg.equipCnt)


    self.m_suitDescCellCache:Refresh(1, function(cell, index)
        if not suitInfoDict then
            cell.desc.color = self.view.config.TEXT_COLOR_ENABLE
            return
        end

        local curEquipCount = suitInfoDict[firstSuitCfg.suitID] or 0
        local suitEnable = curEquipCount >= firstSuitCfg.equipCnt or (not hasCharInst)
        local skillId = firstSuitCfg.skillID
        local skillLv = firstSuitCfg.skillLv
        local skillDesc = CharInfoUtils.getSkillDesc(skillId, skillLv)
        local descFormat = suitEnable and Language.LUA_EQUIP_SKILL_ENABLE_FORMAT or Language.LUA_EQUIP_SKILL_DISABLE_FORMAT

        cell.desc:SetAndResolveTextStyle(string.format(descFormat, skillDesc))
        cell.desc.color = suitEnable and self.view.config.TEXT_COLOR_ENABLE or self.view.config.TEXT_COLOR_DISABLE
    end)
end

EquipSuitNode.InitEquipSuitNode = HL.Method(HL.String, HL.Opt(HL.Number, HL.Number, HL.Number)) << function(self, equipTemplateId, tryCharInstId, tryEquipInstId, trySlotIndex)
    self:_FirstTimeInit()

    self.view.charEmptySuitNode.gameObject:SetActive(false)

    local equipCfg = Tables.equipTable:GetValue(equipTemplateId)
    local hasSuit = equipCfg.suitID and not string.isEmpty(equipCfg.suitID)

    self.view.equipEmptySuitNode.gameObject:SetActive(not hasSuit)
    self.view.suitGroupNode.gameObject:SetActive(hasSuit)
    self.view.suitTitle.gameObject:SetActive(hasSuit)

    if not hasSuit then
        return
    end

    local hasCharInst = tryCharInstId and tryCharInstId > 0
    local suitInfoDict = {}
    if hasCharInst then
        suitInfoDict = self:_CollectSuitInfoDictFromCharInst(tryCharInstId, tryEquipInstId, trySlotIndex)
    end

    local totalSuitCfg = Tables.equipSuitTable:GetValue(equipCfg.suitID)
    local firstSuitCfg = totalSuitCfg.list[0]
    local firstSuitEnable = suitInfoDict and suitInfoDict[firstSuitCfg.suitID] and suitInfoDict[firstSuitCfg.suitID] >= firstSuitCfg.equipCnt or (not hasCharInst)
    local firstSuitDesc = firstSuitEnable and Language.LUA_EQUIP_SKILL_ENABLE_FORMAT or Language.LUA_EQUIP_SKILL_DISABLE_FORMAT
    self.view.suitName.text = hasCharInst and string.format(firstSuitDesc, firstSuitCfg.suitName) or firstSuitCfg.suitName
    self.view.suitLogo:LoadSprite(self:_GetLogoFolder(), firstSuitCfg.suitLogoName)

    self.view.suitNum.gameObject:SetActive(tryCharInstId ~= nil)
    if tryCharInstId then
        local curEquipCount = 0
        local curEquipSuitInfoDict = self:_CollectSuitInfoDictFromCharInst(tryCharInstId)
        for suitId, equipCount in pairs(curEquipSuitInfoDict) do
            if suitId == firstSuitCfg.suitID then
                curEquipCount = equipCount
                break
            end
        end
        self.view.suitNum.text = string.format(Language.LUA_CHAR_INFO_SUIT_EQUIP_NUM_FORMAT, curEquipCount, firstSuitCfg.equipCnt)
    end



    self.m_suitDescCellCache:Refresh(1, function(cell, index)
        if not suitInfoDict then
            cell.desc.color = self.view.config.TEXT_COLOR_ENABLE
            return
        end

        local curEquipCount = suitInfoDict[firstSuitCfg.suitID] or 0
        local suitEnable = curEquipCount >= firstSuitCfg.equipCnt or (not hasCharInst)
        local skillId = firstSuitCfg.skillID
        local skillLv = firstSuitCfg.skillLv
        local skillDesc = CharInfoUtils.getSkillDesc(skillId, skillLv)
        local descFormat = suitEnable and Language.LUA_EQUIP_SKILL_ENABLE_FORMAT or Language.LUA_EQUIP_SKILL_DISABLE_FORMAT

        cell.desc:SetAndResolveTextStyle(hasCharInst and string.format(descFormat, skillDesc) or skillDesc)
        cell.desc.color = suitEnable and self.view.config.TEXT_COLOR_ENABLE or self.view.config.TEXT_COLOR_DISABLE
    end)
end

EquipSuitNode.InitEquipSuitNodeByCharInstId = HL.Method(HL.Number) << function(self, charInstId)
    self:_FirstTimeInit()

    self.view.equipEmptySuitNode.gameObject:SetActive(false)

    local suitInfoDict = self:_CollectSuitInfoDictFromCharInst(charInstId)
    local enabledSuitId
    local curEquipCount = 0
    for suitId, equipCount in pairs(suitInfoDict) do
        local totalSuitCfg = Tables.equipSuitTable:GetValue(suitId)
        local firstSuitCfg = totalSuitCfg.list[0]
        if equipCount >= firstSuitCfg.equipCnt then
            curEquipCount = equipCount
            enabledSuitId = suitId
            break
        end
    end
    local hasEnabledSuit = enabledSuitId ~= nil and not string.isEmpty(enabledSuitId)

    self.view.charEmptySuitNode.gameObject:SetActive(not hasEnabledSuit)
    self.view.suitGroupNode.gameObject:SetActive(hasEnabledSuit)
    self.view.suitTitle.gameObject:SetActive(hasEnabledSuit)
    if not hasEnabledSuit then
        return
    end

    local totalSuitCfg = Tables.equipSuitTable:GetValue(enabledSuitId)
    local firstSuitCfg = totalSuitCfg.list[0]

    local firstSuitDesc = hasEnabledSuit and Language.LUA_EQUIP_SKILL_ENABLE_FORMAT or Language.LUA_EQUIP_SKILL_DISABLE_FORMAT
    self.view.suitName.text = string.format(firstSuitDesc, firstSuitCfg.suitName) or firstSuitCfg.suitName



    self.view.suitLogo:LoadSprite(self:_GetLogoFolder(), firstSuitCfg.suitLogoName)

    self.m_suitDescCellCache:Refresh(1, function(cell, index)
        local skillId = firstSuitCfg.skillID
        local skillLv = firstSuitCfg.skillLv
        local skillDesc = CharInfoUtils.getSkillDesc(skillId, skillLv)

        cell.desc:SetAndResolveTextStyle(string.format(Language.LUA_EQUIP_SKILL_ENABLE_FORMAT, skillDesc))
        cell.desc.color = self.view.config.TEXT_COLOR_ENABLE
    end)

    self.view.suitNum.text = string.format(Language.LUA_CHAR_INFO_SUIT_EQUIP_NUM_FORMAT, curEquipCount, firstSuitCfg.equipCnt)
end

EquipSuitNode._CollectSuitInfoDictFromCharInst = HL.Method(HL.Number, HL.Opt(HL.Number, HL.Number)).Return(HL.Table) << function(self, charInstId, tryEquipInstId, trySlotIndex)
    local suitInfoDict = {}
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local equips = charInst.equipCol

    for _, slotIndex in pairs(UIConst.CHAR_INFO_EQUIP_SLOT_MAP) do
        if slotIndex ~= UIConst.CHAR_INFO_EQUIP_SLOT_MAP.TACTICAL then
            local equipIndex = UIConst.EQUIP_PART_TYPE_2_CELL_CONFIG[slotIndex].equipIndex
            local hasValue, equipInstId = equips:TryGetValue(equipIndex)
            if hasValue then
                local equipInst = CharInfoUtils.getEquipByInstId(equipInstId)
                local equipCfg = Tables.equipTable:GetValue(equipInst.templateId)
                local hasSuit = equipCfg.suitID and not string.isEmpty(equipCfg.suitID)
                if hasSuit then
                    if not suitInfoDict[equipCfg.suitID] then
                        suitInfoDict[equipCfg.suitID] = {}
                    end
                    suitInfoDict[equipCfg.suitID][slotIndex] = equipInstId
                end
            end
        end
    end


    if tryEquipInstId then
        local equipInst = CharInfoUtils.getEquipByInstId(tryEquipInstId)
        local equipCfg = Tables.equipTable:GetValue(equipInst.templateId)
        local hasSuit = equipCfg.suitID and not string.isEmpty(equipCfg.suitID)
        if hasSuit then
            if not suitInfoDict[equipCfg.suitID] then
                suitInfoDict[equipCfg.suitID] = {}
            end

            local alreadyInDict = false
            for slotIndex, equipInstId in pairs(suitInfoDict[equipCfg.suitID]) do
                if equipInstId == tryEquipInstId then
                    alreadyInDict = true
                    break
                end
            end

            if not alreadyInDict then
                suitInfoDict[equipCfg.suitID][trySlotIndex] = tryEquipInstId
            end
        end
    end

    local countDict = {}
    for suitId, dict in pairs(suitInfoDict) do
        local count = 0
        for i, v in pairs(dict) do
            count = count + 1
        end
        countDict[suitId] = count
    end

    return countDict
end

EquipSuitNode._GetLogoFolder = HL.Method().Return(HL.String) << function(self)
    return self.config.WHITE_LOGO and UIConst.UI_SPRITE_EQUIPMENT_LOGO_BIG_WHITE or UIConst.UI_SPRITE_EQUIPMENT_LOGO_BIG
end


HL.Commit(EquipSuitNode)
return EquipSuitNode

