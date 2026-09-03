local RegisterEventToCS = CS.Beyond.EventManager.AddLuaListenGlobal;

MessageManager = HL.Class('MessageManager')

MessageManager.m_registerMap = HL.Field(HL.Table)

MessageManager.m_registerMsgs = HL.Field(HL.Table)

MessageManager.m_registerGroups = HL.Field(HL.Table)

MessageManager.m_nextRegisterKey = HL.Field(HL.Number) << 1

MessageManager.m_sendingMsgPendingActions = HL.Field(HL.Table)

MessageManager.MessageManager = HL.Constructor() << function(self)
    self.m_registerMap = {}
    self.m_registerMsgs = {}
    self.m_registerGroups = {}
    self.m_sendingMsgPendingActions = {}
    self.m_nextRegisterKey = 1
end

MessageManager.Register = HL.Method(HL.Number, HL.Function, HL.Opt(HL.Any)).Return(HL.Opt(HL.Number)) << function(self, msg, action, groupKey)
    if not msg or type(msg) ~= "number" then
        logger.error("MessageManager:Register error, Invalid msg:" .. msg)
        return
    end

    if self.m_sendingMsgPendingActions[msg] then
        if self.m_sendingMsgPendingActions[msg] == true then
            self.m_sendingMsgPendingActions[msg] = {}
        end
        logger.info("发送消息中新增监听 被缓存", msg)
        table.insert(self.m_sendingMsgPendingActions[msg], {"Register", 3, msg, action, groupKey})
        return
    end

    local registerKey = self.m_nextRegisterKey
    self.m_nextRegisterKey = self.m_nextRegisterKey + 1
    self.m_registerMap[registerKey] = action

    local keys = self.m_registerMsgs[msg]
    if not keys then
        keys = {}
        self.m_registerMsgs[msg] = keys
        RegisterEventToCS(MessageConst.getMsgName(msg))
    end
    keys[registerKey] = true

    if groupKey ~= nil then
        keys = self.m_registerGroups[groupKey]
        if not keys then
            keys = {}
            self.m_registerGroups[groupKey] = keys
        end
        keys[registerKey] = true
    end

    return registerKey
end

MessageManager.Unregister = HL.Method(HL.Number) << function(self, registerKey)
    if self.m_registerMap[registerKey] then
        self.m_registerMap[registerKey] = nil
    end
end

MessageManager.UnregisterAll = HL.Method(HL.Any) << function(self, groupKey)
    local keys = self.m_registerGroups[groupKey]
    if not keys then
        return
    end

    for k, _ in pairs(keys) do
        self:Unregister(k)
    end
    self.m_registerGroups[groupKey] = nil
end

MessageManager.Send = HL.Method(HL.Number, HL.Opt(HL.Any)) << function(self, msg, arg)
    if not msg or type(msg) ~= "number" then
        logger.error("MessageManager:Send error, Invalid msg:" .. msg)
        return
    end

    local keys = self.m_registerMsgs[msg]
    if keys and next(keys) then
        if self.m_sendingMsgPendingActions[msg] then

            logger.info("重复发送消息 被缓存", msg)
            if self.m_sendingMsgPendingActions[msg] == true then
                self.m_sendingMsgPendingActions[msg] = {}
            end
            table.insert(self.m_sendingMsgPendingActions[msg], {"Send", 2, msg, arg})
            return
        end
        self.m_sendingMsgPendingActions[msg] = true

        for k, _ in pairs(keys) do
            local action = self.m_registerMap[k]
            if action then

                local succ, log
                if arg == nil then
                    succ, log = xpcall(action, debug.traceback)
                else
                    succ, log = xpcall(action, debug.traceback, arg)
                end
                if not succ then
                    logger.critical("Lua执行消息时出现了错误", MessageConst.getMsgName(msg), "\n" .. tostring(log), "\n\nArg: " .. tostring((arg ~= nil and inspect2(arg) or "nil")))
                end
            else

                keys[k] = nil
            end
        end

        local actions = self.m_sendingMsgPendingActions[msg]
        self.m_sendingMsgPendingActions[msg] = nil
        if actions ~= true then
            self:_TryDealPendingActions(actions)
        end
    end


    if RedDotManager then
        RedDotManager:OnMessage(msg)
    end
end

MessageManager._TryDealPendingActions = HL.Method(HL.Table) << function(self, actions)
    if not next(actions) then
        return
    end
    for _, v in ipairs(actions) do
        local funcName = v[1]
        local argNum = v[2]

        if argNum == 0 then
            self[funcName](self)
        elseif argNum == 1 then
            self[funcName](self, v[3])
        elseif argNum == 2 then
            self[funcName](self, v[3], v[4])
        elseif argNum == 3 then
            self[funcName](self, v[3], v[4], v[5])
        else
            logger.error("Args Num Not Support", inspect(v))
        end
    end
end

HL.Commit(MessageManager)
return MessageManager
