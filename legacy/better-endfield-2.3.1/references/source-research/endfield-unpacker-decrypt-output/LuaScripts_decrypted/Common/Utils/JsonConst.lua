local const = {}
local tmpConst = {
    INVENTORY_MONEY_IDS = '["item_gold", "item_ap"]',
}

setmetatable(const, {
    __index = function(_, k)
        local succ, data = Tables.jsonConst:TryGetValue(k)
        if succ then
            return Utils.stringJsonToTable(data.value)
        end
        local value = tmpConst[k]
        if value ~= nil then
            return value
        end
        logger.error("No Json Const:", k)
        return nil
    end
})

return const
