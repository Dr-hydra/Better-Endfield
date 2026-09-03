local AttributeUtils = {}

local NEED_SUB_HINT = {
    [GEnums.AttributeType.Str] = "_generateStrHint",
    [GEnums.AttributeType.Agi] = "_generateAgiHint",
    [GEnums.AttributeType.Wisd] = "_generateWisdHint",
    [GEnums.AttributeType.Will] = "_generateWillHint"
}


local FormatUtils = CS.Beyond.Gameplay.FormatUtils




function AttributeUtils.extractAttrData(attr)
    local attrValue
    local attrModifier
    local enhancedAttrIndex
    local enhanceGuaranteeTimesRuleId
    if type(attr) == "table" then
        attrValue = attr.attrValue
        attrModifier = attr.modifierType
        enhancedAttrIndex = attr.enhancedAttrIndex
        enhanceGuaranteeTimesRuleId = attr.enhanceGuaranteeTimesRuleId
    else
        attrValue = attr
    end

    return attrValue, attrModifier, enhancedAttrIndex, enhanceGuaranteeTimesRuleId
end


function AttributeUtils.generateAttributeShowInfoListByModifierGroup(attrModifierDict)
    local showAttributeList = {}

    for attrType, modifierDict in pairs(attrModifierDict) do
        for i, attrInfo in pairs(modifierDict) do
            local attrValue = attrInfo.attrValue
            local modifierType = attrInfo.modifierType
            local extraCfg = {
                attrModifier = modifierType
            }
            local attributeShowInfo = AttributeUtils.generateAttributeShowInfo(attrType, attrValue, extraCfg)
            if attrInfo.attrValueMin then
                local attributeShowInfoMin = AttributeUtils.generateAttributeShowInfo(attrType, attrInfo.attrValueMin, extraCfg)
                attributeShowInfo.showValueMin = attributeShowInfoMin.showValue
            end
            if attrInfo.attrValueMax then
                local attributeShowInfoMin = AttributeUtils.generateAttributeShowInfo(attrType, attrInfo.attrValueMax, extraCfg)
                attributeShowInfo.showValueMax = attributeShowInfoMin.showValue
            end
            if attributeShowInfo then
                attributeShowInfo.extraInfo = attrInfo.extraInfo
                table.insert(showAttributeList, attributeShowInfo)
            end
        end
    end

    return showAttributeList
end

function AttributeUtils.generateAttributeShowInfoList(allAttributes)
    local showAttributeList = {}
    for attrType, attrInfo in pairs(allAttributes) do
        local attrValue = attrInfo.attrValue
        local modifierType = attrInfo.modifierType
        local extraCfg = {
            attrModifier = modifierType
        }
        local attributeShowInfo = AttributeUtils.generateAttributeShowInfo(attrType, attrValue, extraCfg)
        if attrInfo.attrValueMin then
            local attributeShowInfoMin = AttributeUtils.generateAttributeShowInfo(attrType, attrInfo.attrValueMin, extraCfg)
            attributeShowInfo.showValueMin = attributeShowInfoMin.showValue
        end
        if attrInfo.attrValueMax then
            local attributeShowInfoMin = AttributeUtils.generateAttributeShowInfo(attrType, attrInfo.attrValueMax, extraCfg)
            attributeShowInfo.showValueMax = attributeShowInfoMin.showValue
        end
        if attributeShowInfo then
            attributeShowInfo.extraInfo = attrInfo.extraInfo
            table.insert(showAttributeList, attributeShowInfo)
        end
    end

    showAttributeList = lume.sort(showAttributeList, function(a, b)
        return a.attributeType:ToInt() < b.attributeType:ToInt()
    end)

    return showAttributeList
end































function AttributeUtils.generateAttributeShowInfo(attrType, attributeValue, extraCfg)
    if not attributeValue then
        logger.error("CharInfoUtils->Can't find attributeValue for attrType: " .. attrType:ToString())
        return
    end

    local attrShowCfg
    local isCompositeAttr = type(attrType) == "string"
    if isCompositeAttr then
        attrShowCfg = AttributeUtils.getCompositeAttributeShowCfg(attrType, extraCfg)
    else
        attrShowCfg = AttributeUtils.getAttributeShowCfg(attrType, extraCfg)
    end

    local shouldShow = AttributeUtils._checkShouldShow(attrType, attributeValue, attrShowCfg, extraCfg)
    if not shouldShow then
        return
    end

    local showDiffFromDefault = AttributeUtils._checkIfShowDiffFromDefault(attrShowCfg, extraCfg)
    local showPercent, isForceByExtraCfg = AttributeUtils._checkIfShowPercent(attrShowCfg, extraCfg)
    local modifiedValue = AttributeUtils.modifyAttributeValue(attrType, attributeValue, showPercent, showDiffFromDefault, extraCfg)
    local showValue
    if isForceByExtraCfg or string.isEmpty(attrShowCfg.valueFormat) then
        showValue = AttributeUtils.generateShowValue(modifiedValue, showPercent, extraCfg)
    else
        showValue = AttributeUtils.generateShowValueByValueFormat(attributeValue, attrShowCfg.valueFormat, showPercent)
    end
    local attributeKey = isCompositeAttr and string.lowerFirst(attrType) or
                         (Const.ATTRIBUTE_TYPE_2_ATTRIBUTE_DATA_KEY[attrType] or attrType:ToString())

    local iconName, bigIconName
    if isCompositeAttr then

    else
        local hasCfg, attrCfg = Tables.AttributeMetaTable:TryGetValue(attrType)
        if hasCfg then
            iconName = attrCfg.iconName
            bigIconName = attrCfg.bigIconName
        else
            logger.error("CharInfoUtils->找不到属性的元数据，attrType: %s", attrType:ToString())
        end
    end


    return {
        attributeType = attrType,
        attributeKey = attributeKey,
        showName = attrShowCfg.name,
        showValue = showValue,
        attributeValue = attributeValue,
        modifiedValue = modifiedValue,
        sortOrder = attrShowCfg.index,
        hasHint = not string.isEmpty(attrShowCfg.attributeHint),
        enhancedAttrIndex = extraCfg and extraCfg.enhancedAttrIndex,
        enhanceGuaranteeTimesRuleId = extraCfg and extraCfg.enhanceGuaranteeTimesRuleId,
        attrShowCfg = attrShowCfg,
        attrModifier = extraCfg and extraCfg.attrModifier,
        isCompositeAttr = isCompositeAttr,
        iconName = iconName,
        bigIconName = bigIconName
    }
end

function AttributeUtils.generateEmptyAttributeShowInfo(attrType, modifier)
    local attrMetaCfg = Tables.AttributeMetaTable:GetValue(attrType)
    return AttributeUtils.generateAttributeShowInfo(attrType, attrMetaCfg.defaultValue, {attrModifier = modifier, forceShow = true})
end

function AttributeUtils.getAttributesDiff(attributesFrom, attributesTo)
    local attributesDiff = {}
    for attributeType, toValue in pairs(attributesTo) do
        local fromValue = attributesFrom[attributeType]
        if not fromValue then
            logger.error("计算属性差值时不存在 [%s] 这个属性", attributeType)
        else
            attributesDiff[attributeType] = toValue - fromValue
        end
    end

    return attributesDiff
end

function AttributeUtils.getAttributeModifier(attributeType, charInstId)
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    if not charInst then
        return
    end

    local attributeModifier = charInst:GetAttributeModifier(attributeType)
    return attributeModifier
end





function AttributeUtils.getCompositeAttributeShowCfg(compositeAttrType, extraCfg)
    local attrModifier = GEnums.ModifierType.None
    if extraCfg and extraCfg.attrModifier ~= nil then
        attrModifier = extraCfg.attrModifier
    end
    local ignoreAttrModifier = false
    if extraCfg and extraCfg.ignoreAttrModifier ~= nil then
        ignoreAttrModifier = extraCfg.ignoreAttrModifier
    end

    local showCfgTable = Tables.CompositeAttributeShowConfigTable
    local _, attributeShowData = showCfgTable:TryGetValue(compositeAttrType)
    if not attributeShowData then
        return
    end

    local modifier2AttributeShowConfig = attributeShowData.list
    local attrCfg
    for _, config in pairs(modifier2AttributeShowConfig) do
        if config.attributeModifier == attrModifier or ignoreAttrModifier then
            attrCfg = config
        end
    end
    return attrCfg
end





function AttributeUtils.getAttributeShowCfg(attributeType, extraCfg)
    local attributeShowConfigTable = Tables.AttributeShowConfigTable
    local _, attributeShowData = attributeShowConfigTable:TryGetValue(attributeType)
    if not attributeShowData then
        return
    end

    local attrModifier = GEnums.ModifierType.None
    if extraCfg and extraCfg.attrModifier ~= nil then
        attrModifier = extraCfg.attrModifier
    end
    local ignoreAttrModifier = false
    if extraCfg and extraCfg.ignoreAttrModifier ~= nil then
        ignoreAttrModifier = extraCfg.ignoreAttrModifier
    end

    local modifier2AttributeShowConfig = attributeShowData.list
    local attributeCfg
    for _, config in pairs(modifier2AttributeShowConfig) do
        if config.attributeModifier == attrModifier or ignoreAttrModifier then
            attributeCfg = config
        end
    end

    if attributeCfg == nil then
        logger.error(string.format("CharInfoUtils->找不到属性的显示规则，attrType: %s ,modifierType: %s", attributeType:ToString(), attrModifier:ToString()))
        return
    end

    return attributeCfg
end

function AttributeUtils.generateShowValue(attributeValue, showPercent, extraCfg)
    if showPercent then
        if extraCfg and extraCfg.forceShowRoundPercent then
            return string.format("%.0f%%", attributeValue)
        else
            return string.format("%.1f%%", attributeValue)
        end
    end

    return string.format("%d", attributeValue)
end

function AttributeUtils.generateShowValueByValueFormat(attrValue, valueFormat, isPercent)
    local attrValue = lume.round(attrValue * 100000) / 100000
    if not isPercent then
        attrValue = math.floor(attrValue)
    end
    return FormatUtils.GetShowAttrValueString(valueFormat, attrValue)
end

local AttributeType = GEnums.AttributeType
function AttributeUtils.modifyAttributeValue(attributeType, attributeValue, showPercent, showDiffFromDefault, extraCfg)
    if showDiffFromDefault then
        local finalDefaultValue = 0
        local isCompositeAttr = type(attributeType) == "string"

        if isCompositeAttr then
            local _, compositeCfg = Tables.CompositeAttributeTable:TryGetValue(attributeType)
            if compositeCfg and compositeCfg.list.Count > 0 then

                local attrTypeString = compositeCfg.list[0]
                local isAttributeType, attrType = CS.System.Enum.TryParse(typeof(AttributeType), attrTypeString)
                if isAttributeType then
                    local _, attributeCfg = Tables.AttributeMetaTable:TryGetValue(attrType)
                    local defaultValue = attributeCfg.defaultValue
                    if defaultValue then
                        finalDefaultValue = defaultValue
                    end
                else

                    finalDefaultValue = 0
                end
            end
        else
            local _, attributeCfg = Tables.AttributeMetaTable:TryGetValue(attributeType)
            if attributeCfg then
                local defaultValue = attributeCfg.defaultValue
                if defaultValue then
                    finalDefaultValue = defaultValue
                end
            end
        end

        attributeValue = finalDefaultValue - attributeValue
    end



    attributeValue = lume.round(attributeValue * 100000) / 100000

    if showPercent then

        if extraCfg and extraCfg.forceShowRoundPercent then
            attributeValue = attributeValue * 10000
            return math.floor(attributeValue) / 100
        else
            attributeValue = attributeValue * 1000
            return math.floor(attributeValue) / 10
        end
    end


    return math.floor(attributeValue)
end

function AttributeUtils._checkShouldShow(attrType, attributeValue, attrShowCfg, extraCfg)

    if not attrShowCfg then
        return false
    end


    if extraCfg and extraCfg.forceShow then
        return true
    end


    local displayType = attrShowCfg.defaultDisplayType
    if extraCfg and extraCfg.fromSpecificSystem == UIConst.CHAR_INFO_ATTRIBUTE_SPECIFIC_SYSTEM.CHAR_FULL_ATTR then
        displayType = attrShowCfg.charFullAttrDisplayType
    elseif extraCfg and extraCfg.fromSpecificSystem == UIConst.CHAR_INFO_ATTRIBUTE_SPECIFIC_SYSTEM.EQUIP_FULL_ATTR then
        displayType = attrShowCfg.equipFullAttrDisplayType
    end

    return AttributeUtils._checkAttrDisplayType(attrType, attributeValue, attrShowCfg, extraCfg, displayType)
end

function AttributeUtils._checkAttrDisplayType(attrType, attributeValue, attrShowCfg, extraCfg, attrDisplayType)

    if attrDisplayType == GEnums.AttributeDisplayType.None then
        return false
    end


    if attrDisplayType == GEnums.AttributeDisplayType.OnlyDiff then
        local attrMetaCfg = Tables.AttributeMetaTable:GetValue(attrType)
        if attrMetaCfg then
            local defaultValue = attrMetaCfg.defaultValue
            if defaultValue then
                return math.abs(defaultValue - attributeValue) > 0.0001
            end
        end
    end

    return true
end

function AttributeUtils._checkIfShowPercent(attributeCfg, extraCfg)
    local showPercent = attributeCfg.showPercent
    local isForce = false
    if extraCfg and extraCfg.forceShowPercent ~= nil and extraCfg.forceShowPercent ~= UIConst.ATTRIBUTE_GENERATE_FORCE_PERCENT.DO_NOT_CARE then
        showPercent = extraCfg.forceShowPercent == UIConst.ATTRIBUTE_GENERATE_FORCE_PERCENT.HAS_PERCENT
        isForce = true
    end
    return showPercent, isForce
end

function AttributeUtils._checkIfShowDiffFromDefault(attributeCfg, extraCfg)

    if extraCfg and extraCfg.isAttributeDiff then
        return false
    end


    local showDiffFromDefault = attributeCfg.showDiffFromDefault
    if extraCfg and extraCfg.forceShowDiffFromDefault ~= nil and extraCfg.forceShowDiffFromDefault ~= UIConst.ATTRIBUTE_GENERATE_FORCE_DIFF_FROM_DEFAULT.DO_NOT_CARE then
        showDiffFromDefault = extraCfg.forceShowDiffFromDefault == UIConst.ATTRIBUTE_GENERATE_FORCE_DIFF_FROM_DEFAULT.SHOW_DIFF
    end
    return showDiffFromDefault
end


function AttributeUtils.getAttributeHint(attributeInfo, extraCfg)
    local attrCfg = AttributeUtils.getAttributeShowCfg(attributeInfo.attributeType, extraCfg)
    local attributeType = attributeInfo.attributeType
    local hintInfo = {}

    hintInfo.mainHint = attrCfg.attributeHint

    if extraCfg then
        local subHintFuncName = NEED_SUB_HINT[attributeType]
        if subHintFuncName then
            local subHintFunc = AttributeUtils[subHintFuncName]
            local subHintList = subHintFunc(attrCfg, extraCfg)

            hintInfo.subHintList = subHintList
        end

        local extraHintList = {}
        if AttributeUtils.CheckIsMainAttr(attributeInfo.attributeType, extraCfg.charTemplateId) then
            local charInst = CharInfoUtils.getPlayerCharInfoByInstId(extraCfg.charInstId)
            local attributes = charInst.attributes
            local mainAtkScalar = attributes:GetAtkFinalScalarFromMainAttribute()
            local mainAttrScalarShowInfo = AttributeUtils.generateAttributeShowInfo(
                GEnums.AttributeType.Atk,
                mainAtkScalar,
                {
                    forceShowPercent = UIConst.ATTRIBUTE_GENERATE_FORCE_PERCENT.HAS_PERCENT,
                    fromSpecificSystem = UIConst.CHAR_INFO_ATTRIBUTE_SPECIFIC_SYSTEM.CHAR_FULL_ATTR
                }
            )

            local mainHint = string.format(Language.LUA_CHAR_INFO_ATTRIBUTE_HINT_MAIN_FORMAT, mainAttrScalarShowInfo.showValue)
            table.insert(extraHintList, mainHint)
        end

        if AttributeUtils.CheckIsSubAttr(attributeInfo.attributeType, extraCfg.charTemplateId) then
            local charInst = CharInfoUtils.getPlayerCharInfoByInstId(extraCfg.charInstId)
            local attributes = charInst.attributes
            local subAtkScalar = attributes:GetAtkFinalScalarFromSubAttribute()
            local subAttrScalarShowInfo = AttributeUtils.generateAttributeShowInfo(
                GEnums.AttributeType.Atk,
                subAtkScalar,
                {
                    forceShowPercent = UIConst.ATTRIBUTE_GENERATE_FORCE_PERCENT.HAS_PERCENT,
                    fromSpecificSystem = UIConst.CHAR_INFO_ATTRIBUTE_SPECIFIC_SYSTEM.CHAR_FULL_ATTR
                }
            )

            local subHint = string.format(Language.LUA_CHAR_INFO_ATTRIBUTE_HINT_SUB_FORMAT, subAttrScalarShowInfo.showValue)
            table.insert(extraHintList, subHint)
        end

        if #extraHintList > 0 then
            hintInfo.extraHintList = extraHintList
        end
    end


    return hintInfo
end

function AttributeUtils._generateStrHint(attributeCfg, extraCfg)
    local charInstId = extraCfg.charInstId
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)

    if not charInst then
        return
    end

    local attributes = charInst.attributes
    local hpByStr = attributes:GetHpBaseAddFromStrAttribute()
    local hpByStrShowValue = AttributeUtils.generateAttributeShowInfo(GEnums.AttributeType.MaxHp, hpByStr,{
        fromSpecificSystem = UIConst.CHAR_INFO_ATTRIBUTE_SPECIFIC_SYSTEM.CHAR_FULL_ATTR
    }).showValue

    return AttributeUtils._formatAndResolveHint({hpByStrShowValue}, attributeCfg)
end

function AttributeUtils._generateAgiHint(attributeCfg, extraCfg)
    local charInstId = extraCfg.charInstId
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)

    if not charInst then
        return
    end

    local attributes = charInst.attributes
    local resistanceFromAgi = attributes:GetValue(GEnums.AttributeType.PhysicalResistance)
    local resistanceShowValue = AttributeUtils.generateAttributeShowInfo(GEnums.AttributeType.PhysicalResistance, resistanceFromAgi, {
        fromSpecificSystem = UIConst.CHAR_INFO_ATTRIBUTE_SPECIFIC_SYSTEM.CHAR_FULL_ATTR
    }).showValue

    return AttributeUtils._formatAndResolveHint({resistanceShowValue}, attributeCfg)
end

function AttributeUtils._generateWisdHint(attributeCfg, extraCfg)
    local charInstId = extraCfg.charInstId
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)

    if not charInst then
        return
    end

    local attributes = charInst.attributes
    local resistanceFromWisd = attributes:GetValue(GEnums.AttributeType.FireResistance)
    local resistanceShowValue = AttributeUtils.generateAttributeShowInfo(GEnums.AttributeType.FireResistance, resistanceFromWisd, {
        fromSpecificSystem = UIConst.CHAR_INFO_ATTRIBUTE_SPECIFIC_SYSTEM.CHAR_FULL_ATTR
    }).showValue

    return AttributeUtils._formatAndResolveHint({resistanceShowValue}, attributeCfg)
end

function AttributeUtils._generateWillHint(attributeCfg, extraCfg)
    local charInstId = extraCfg.charInstId
    local charInst = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)

    if not charInst then
        return
    end

    local attributes = charInst.attributes



    local healTakenScalarFromWill = attributes:GetHealTakenScalarFromWill()
    local healTakenScalarShowValue = AttributeUtils.generateAttributeShowInfo(GEnums.AttributeType.HealTakenIncrease, healTakenScalarFromWill, {
        forceShow = true
    }).showValue

    return AttributeUtils._formatAndResolveHint({healTakenScalarShowValue}, attributeCfg)
end

function AttributeUtils._formatAndResolveHint(formatValues, attributeCfg)
    local resolvedHints = {}
    for i = 1, #attributeCfg.attributeHintSubs do
        local format = attributeCfg.attributeHintSubs[CSIndex(i)]
        local formatValue = formatValues[i]
        local formatHint = string.format(format, formatValue)

        resolvedHints[i] = formatHint
    end

    return resolvedHints
end

function AttributeUtils.CheckIsMainAttr(attributeType, charTemplateId)
    local charCfg = Tables.characterTable[charTemplateId]

    return charCfg.mainAttrType == attributeType
end

function AttributeUtils.CheckIsSubAttr(attributeType, charTemplateId)
    local charCfg = Tables.characterTable[charTemplateId]

    return charCfg.subAttrType == attributeType
end

function AttributeUtils._tryGenerateMainAttrHint()

end

function AttributeUtils._tryGenerateSubAttrHint()

end




_G.AttributeUtils = AttributeUtils
return AttributeUtils
