local CommonCache = require_ex("Common/Utils/CommonCache")
GameObjectCache = HL.Class('GameObjectCache', CommonCache)

GameObjectCache.GameObjectCache = HL.Constructor(HL.Any, HL.Any, HL.Opt(HL.Function, HL.Function, HL.Function))
<< function(self, template, root, onCreate, onUse, onCache)
    local createFunc = function()
        local obj = CSUtils.CreateObject(template.gameObject, root.transform)
        if onCreate then
            onCreate(obj)
        end
        return obj
    end
    local wrappedOnUse = function(obj)
        obj.gameObject:SetActive(true)
        if onUse then
            onUse(obj)
        end
    end
    local wrappedOnCache = function(obj)
        obj.gameObject:SetActive(false)
        if onCache then
            onCache(obj)
        end
    end

    GameObjectCache.SuperConstructor(self, createFunc, wrappedOnUse, wrappedOnCache)
end

HL.Commit(GameObjectCache)
return GameObjectCache
