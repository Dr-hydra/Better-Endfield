local CommonCache = require_ex("Common/Utils/CommonCache")
LuaNodeCache = HL.Class('LuaNodeCache', CommonCache)

LuaNodeCache.LuaNodeCache = HL.Constructor(HL.Any, HL.Any, HL.Opt(HL.Function, HL.Function, HL.Function))
<< function(self, template, uiRoot, onCreate, onUse, onCache)
    template.gameObject.gameObject:SetActive(false)
    local createFunc = function()
        local obj = CSUtils.CreateObject(template.gameObject, uiRoot.transform)
        local cell = Utils.wrapLuaNode(obj)
        if onCreate then
            onCreate(cell)
        end
        return cell
    end
    local wrappedOnUse = function(cell)
        cell.gameObject:SetActive(true)
        if onUse then
            onUse(cell)
        end
    end
    local wrappedOnCache = function(cell)
        cell.gameObject:SetActive(false)
        if onCache then
            onCache(cell)
        end
    end

    LuaNodeCache.SuperConstructor(self, createFunc, wrappedOnUse, wrappedOnCache)
end

HL.Commit(LuaNodeCache)
return LuaNodeCache
