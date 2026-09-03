local redDotConfigFile = require_ex("UI/RedDot/RedDotConfig")
local RegisterEventToCS = CS.Beyond.EventManager.AddLuaListenGlobal;
local RED_DOT_TYPE = UIConst.RED_DOT_TYPE
local RED_DOT_TYPE_MAX = UIConst.RED_DOT_TYPE_MAX

RedDotManager = HL.Class('RedDotManager')




RedDotManager.m_activeMsgs = HL.Field(HL.Table)

RedDotManager.m_activeRedDots = HL.Field(HL.Table)

RedDotManager.m_redDotInstanceMap = HL.Field(HL.Table)

RedDotManager.m_nexActiveRedDotId = HL.Field(HL.Number) << 1

RedDotManager.configs = HL.Field(HL.Table)




RedDotManager.RedDotManager = HL.Constructor() << function(self)
    self.m_activeMsgs = {}
    self.m_activeRedDots = {}
    self.m_redDotInstanceMap = {}

    self:UpdateConfigs()
end

RedDotManager.UpdateConfigs = HL.Method() << function(self)
    logger.info(ELogChannel.RedDot, "RedDotManager.UpdateConfigs")



    local redDotConfig = {}
    for k, v in pairs(redDotConfigFile) do
        if v.Check and v.needArg == nil then
            logger.error("红点配置不合法，没有标注 needArg", k, inspect(v))
        end
        if v.readLike == nil then
            logger.error("红点配置不合法，没有标注 readLike", k, inspect(v))
        end
        local redDot = {
            name = k,
            config = v,
        }
        setmetatable(redDot, { __index = v })
        redDotConfig[k] = redDot
    end


    for k, v in pairs(redDotConfig) do
        if v.sons then
            for sonName, resultDependency in pairs(v.sons) do
                local son = redDotConfig[sonName]
                if son then
                    if not son.fathers then
                        son.fathers = {}
                    end
                    son.fathers[k] = resultDependency
                else
                    logger.error("No son", k, sonName)
                end
            end
        end
    end

    self.configs = redDotConfig


    local activeMsgs = {}
    for name, _ in pairs(self.m_activeRedDots) do
        local redDot = redDotConfig[name]
        if redDot.msgs then
            for _, v in ipairs(redDot.msgs) do
                local redDots = activeMsgs[v]
                if not redDots then
                    redDots = {}
                    activeMsgs[v] = redDots

                    RegisterEventToCS(MessageConst.getMsgName(v))
                end
                redDots[name] = true
            end
        end
    end
    self.m_activeMsgs = activeMsgs
end


RedDotManager.AddFatherSon = HL.Method(HL.String, HL.String) << function(self, fatherName, sonName)
    logger.info(ELogChannel.RedDot, "RedDotManager.AddFatherSon", fatherName, sonName)

    local redDot = self.configs[fatherName]
    if not redDot then
        logger.error("No RedDot", fatherName)
        return
    end

    local sonRedDot = self.configs[sonName]
    if not sonRedDot then
        logger.error("No RedDot", sonName)
        return
    end

    if not redDot.sons then
        redDot.sons = {}
    end
    redDot.sons[sonName] = true

    if not sonRedDot.fathers then
        sonRedDot.fathers = {}
    end
    sonRedDot.fathers[fatherName] = true
end

RedDotManager.OnMessage = HL.Method(HL.Number) << function(self, msgId)
    local redDots = self.m_activeMsgs[msgId]
    if not redDots then
        return
    end

    logger.info(ELogChannel.RedDot, "RedDotManager.OnMessage", MessageConst.getMsgName(msgId))

    for name, _ in pairs(redDots) do
        self:TriggerUpdate(name)
    end
end

RedDotManager.GetRedDotState = HL.Method(HL.String, HL.Opt(HL.Any, HL.Number)).Return(HL.Boolean, HL.Opt(HL.Number, HL.Number)) <<
function(self, name, arg, penetrateLevel)
    logger.info(ELogChannel.RedDot, "RedDotManager.GetRedDotState", name, inspect(arg), penetrateLevel)


    local redDot = self.configs[name]
    if not redDot then
        logger.error("No RedDot", name)
        return false
    end


    local result, redDotType = false
    local expireTs = 0
    if redDot.Check then
        local succ
        succ, result, redDotType, expireTs = xpcall(redDot.Check, debug.traceback, arg)
        if not succ then
            logger.error("redDot.Check() error", name, result)
            return false
        end
        if result and not redDotType then
            redDotType = RED_DOT_TYPE.Normal
        end
        if redDotType == RED_DOT_TYPE_MAX then

            return result, redDotType, expireTs
        end
    end


    if redDot.sons then
        penetrateLevel = penetrateLevel or redDot.penetrateLevel or UIConst.RED_DOT_DEFAULT_PENETRATE
        local sonResult, sonRedDotType
        for sonName, v in pairs(redDot.sons) do
            if v then
                sonResult, sonRedDotType, expireTs = self:GetRedDotState(sonName, arg, penetrateLevel)
                if sonResult and sonRedDotType >= penetrateLevel then
                    if sonRedDotType == RED_DOT_TYPE_MAX then

                        return sonResult, sonRedDotType, expireTs
                    end
                    if not result or redDotType < sonRedDotType then
                        result, redDotType = sonResult, sonRedDotType
                    end
                end
            end
        end
    end

    return result, redDotType, expireTs
end

RedDotManager.TriggerUpdate = HL.Method(HL.String) << function(self, name)
    local redDot = self.configs and self.configs[name]
    if not redDot then
        logger.error("No RedDot", name)
        return
    end

    local redDotInstanceList = self.m_activeRedDots[name]
    if not redDotInstanceList then

        return
    end

    logger.info(ELogChannel.RedDot, "RedDotManager.TriggerUpdate", name)

    local count = #redDotInstanceList
    if count > 0 then
        local result, redDotType, expireTs

        local list = {}
        for k, v in ipairs(redDotInstanceList) do
            list[k] = v
        end
        for _, v in ipairs(list) do
            local isActive = true
            if v.uiWidget then
                isActive = v.uiWidget:GetActiveState()
            end
            if isActive then
                if redDot.needArg then
                    v.onUpdate()
                else
                    if result == nil then
                        result, redDotType, expireTs = self:GetRedDotState(name)
                    end
                    v.onUpdate(result, redDotType, expireTs)
                end
            else


                v.uiWidget.needUpdateOnActive = true
            end
        end
    end

    if redDot.fathers then
        for k, _ in pairs(redDot.fathers) do
            self:TriggerUpdate(k)
        end
    end
end





RedDotManager.AddRedDotInstance = HL.Method(HL.String, HL.Function, HL.Forward('RedDot')).Return(HL.Opt(HL.Number)) <<
function(self, name, onUpdate, uiWidget)
    logger.info(ELogChannel.RedDot, "RedDotManager.AddRedDotInstance", name)

    local redDot = self.configs[name]
    if not redDot then
        logger.error("No RedDot", name)
        return
    end

    local key = self.m_nexActiveRedDotId
    self.m_nexActiveRedDotId = self.m_nexActiveRedDotId + 1

    local redDotInstance = {
        redDotName = name,
        onUpdate = onUpdate,
        key = key,
        uiWidget = uiWidget,
    }
    self.m_redDotInstanceMap[key] = redDotInstance

    local redDotInstanceList = self:_ActiveRedDot(name)
    table.insert(redDotInstanceList, redDotInstance)

    return key
end

RedDotManager.RemoveRedDotInstance = HL.Method(HL.Number).Return(HL.Number) << function(self, key)
    logger.info(ELogChannel.RedDot, "RedDotManager.RemoveRedDotInstance", key)

    local redDotInstance = self.m_redDotInstanceMap[key]
    if not redDotInstance then
        return -1
    end

    self.m_redDotInstanceMap[key] = nil

    local name = redDotInstance.redDotName
    local redDotInstanceList = self.m_activeRedDots[name]
    lume.remove(redDotInstanceList, redDotInstance)
    self:_TryDeactiveRedDot(name)
    return -1
end

RedDotManager._ActiveRedDot = HL.Method(HL.String).Return(HL.Opt(HL.Table)) << function(self, name)
    local redDot = self.configs[name]
    if not redDot then
        logger.error("No RedDot", name)
        return
    end

    local activeRedDots = self.m_activeRedDots
    if activeRedDots[name] then
        return activeRedDots[name]
    end

    logger.info(ELogChannel.RedDot, "RedDotManager._ActiveRedDot", name)

    activeRedDots[name] = {}


    if redDot.msgs then
        local activeMsgs = self.m_activeMsgs
        for _, v in ipairs(redDot.msgs) do
            local redDots = activeMsgs[v]
            if not redDots then
                redDots = {}
                activeMsgs[v] = redDots

                RegisterEventToCS(MessageConst.getMsgName(v))
            end
            redDots[name] = true
        end
    end


    if redDot.sons then
        for k, _ in pairs(redDot.sons) do
            self:_ActiveRedDot(k)
        end
    end

    return activeRedDots[name]
end

RedDotManager._TryDeactiveRedDot = HL.Method(HL.String) << function(self, name)
    local activeRedDots = self.m_activeRedDots
    if not activeRedDots[name] then
        return
    end

    if #activeRedDots[name] > 0 then
        return
    end

    local redDot = self.configs[name]
    if not redDot then
        logger.error("No RedDot", name)
        return
    end

    if redDot.fathers then
        for k, _ in pairs(redDot.fathers) do
            if activeRedDots[k] then
                return
            end
        end
    end

    logger.info(ELogChannel.RedDot, "RedDotManager DeactiveRedDot ", name)


    if redDot.msgs then
        local activeMsgs = self.m_activeMsgs
        for _, msgId in ipairs(redDot.msgs) do
            activeMsgs[msgId][name] = nil
            if not next(activeMsgs[msgId]) then

                activeMsgs[msgId] = nil
            end
        end
    end


    activeRedDots[name] = nil


    if redDot.sons then
        for k, _ in pairs(redDot.sons) do
            self:_TryDeactiveRedDot(k)
        end
    end
end

HL.Commit(RedDotManager)
return RedDotManager
