local MailUtils = {}








function MailUtils.AnalyzeMailContent(mail)
    local rawLanguageContent = mail.content
    local newContent, specialParamTable = MailUtils._ResolveSpecialParam(mail, rawLanguageContent)
    newContent = MailUtils._ResolveCommonParam(mail, newContent)
    local resultInfo = {
        content = newContent,
        specialParamTable = specialParamTable
    }
    return resultInfo
end




function MailUtils._ResolveCommonParam(mail, content)
    if mail.paramDic == nil then
        return content
    end


    local escape_pattern = function(text)
        return text:gsub("([%^%$%(%)%%%.%[%]%*%+%-%?])", "%%%1")
    end

    for key, value in cs_pairs(mail.paramDic) do
        local escapedKey = escape_pattern(key)
        content = content:gsub(escapedKey, value)
    end
    return content
end







MailUtils.specialParamKey = {
    Time = "#__time__#",
    Testimonial = "#__testimonial__#",
    GachaLTTicket = "#__gachaLTTicket__#",
    GachaWeaponLTTicket = "#__gachaWeaponLTTicket__#",
    LTItems = "#__ltItems__#",
}


MailUtils._specialParamAnalyzeFunc = {
    ["#__time__#"] = "_GetSpecialParamInfo_Time",
    ["#__testimonial__#"] = "_GetSpecialParamInfo_Testimonial",
    ["#__gachaLTTicket__#"] = "_GetSpecialParamInfo_GachaLTTicket",
    ["#__gachaWeaponLTTicket__#"] = "_GetSpecialParamInfo_GachaWeaponLTTicket",
    ["#__ltItems__#"] = "_GetSpecialParamInfo_LTItems",
}






function MailUtils._ResolveSpecialParam(mail, content)
    if mail.paramDic == nil then
        return content, nil
    end
    local newContent
    local specialParamTable = {}
    content = content:gsub("#__.-__#", function(key)
        local funcName = MailUtils._specialParamAnalyzeFunc[key]
        if not string.isEmpty(funcName) then
            logger.info("Resolve Mail SpecialParamKey: " .. key)
            local analyzedText, paramValue = MailUtils[funcName](mail, newContent)
            specialParamTable[key] = paramValue
            return analyzedText or string.format("<color=red>[Error!: %s]</color>", key)
        end
    end)
    return content, specialParamTable
end





function MailUtils._GetSpecialParamInfo_Time(mail)
    local realKey = MailUtils.specialParamKey.Time:sub(2, -2)
    local hasValue, timestamp = mail.paramDic:TryGetValue(realKey)
    if hasValue then
        return Utils.appendUTC(Utils.timestampToDateYMDHM(timestamp)), nil
    end
    logger.error(string.format("[邮件解析] key 【%s】 对应param不存在，可能是服务端没下发", MailUtils.specialParamKey.Time))
    return nil, nil
end




function MailUtils._GetSpecialParamInfo_Testimonial(mail)
    for key, value in pairs(mail.paramDic) do
        local hasCfg, itemCfg = Tables.itemTable:TryGetValue(key)
        if hasCfg and itemCfg.type == GEnums.ItemType.GachaRecruitmentLetter then
            local info = {
                itemId = key,
                count = value,
            }
            return itemCfg.name, info
        end
    end
    logger.error(string.format("[邮件解析] key 【%s】 对应param不存在，可能是服务端没下发", MailUtils.specialParamKey.Testimonial))
    return nil, nil
end




function MailUtils._GetSpecialParamInfo_GachaLTTicket(mail)
    local resultStr = ""
    local resultInfos = {}
    for key, value in pairs(mail.paramDic) do
        local hasCfg, itemCfg = Tables.itemTable:TryGetValue(key)
        if hasCfg and itemCfg.type == GEnums.ItemType.TicketGachaLTItem then
            local info = {
                itemId = key,
                count = value,
            }
            if string.isEmpty(resultStr) then
                resultStr = string.format(Language.LUA_COMMON_NAME_X_COUNT, itemCfg.name, value)
            else
                resultStr = resultStr .. "\n" .. string.format(Language.LUA_COMMON_NAME_X_COUNT, itemCfg.name, value)
            end
            table.insert(resultInfos, info)
        end
    end
    if not string.isEmpty(resultStr) then
        return resultStr, resultInfos
    end
    logger.error(string.format("[邮件解析] key 【%s】 对应param不存在，可能是服务端没下发", MailUtils.specialParamKey.GachaLTTicket))
    return nil, nil
end




function MailUtils._GetSpecialParamInfo_LTItems(mail)
    local resultStr = ""
    local resultInfos = {}
    for key, value in pairs(mail.paramDic) do
        local hasCfg, itemCfg = Tables.itemTable:TryGetValue(key)
        if hasCfg then
            local hasCfg2, ltItemCfg = Tables.lTItemTypeTable:TryGetValue(itemCfg.type)
            if hasCfg2 then
                local info = {
                    itemId = key,
                    count = value,
                }
                resultStr = resultStr .. string.format(Language.LUA_COMMON_NAME_X_COUNT, itemCfg.name, value) .. "\n"
                table.insert(resultInfos, info)
            end
        end
    end
    if not string.isEmpty(resultStr) then
        return resultStr, resultInfos
    end
    logger.error(string.format("[邮件解析] key 【%s】 对应param不存在，可能是服务端没下发", MailUtils.specialParamKey.LTItems))
    return nil, nil
end





function MailUtils._GetSpecialParamInfo_GachaWeaponLTTicket(mail)
    local resultStr = ""
    local resultInfos = {}
    for key, value in pairs(mail.paramDic) do
        local hasCfg, itemCfg = Tables.itemTable:TryGetValue(key)
        if hasCfg and itemCfg.type == GEnums.ItemType.TicketWeaponBoxLTItem then
            local info = {
                itemId = key,
                count = value,
            }
            if string.isEmpty(resultStr) then
                resultStr = itemCfg.name
            else
                resultStr = resultStr .. "\n" .. itemCfg.name
            end
            table.insert(resultInfos, info)
        end
    end
    if not string.isEmpty(resultStr) then
        return resultStr, resultInfos
    end
    logger.error(string.format("[邮件解析] key 【%s】 对应param不存在，可能是服务端没下发", MailUtils.specialParamKey.GachaWeaponLTTicket))
    return nil, nil
end





_G.MailUtils = MailUtils
return MailUtils