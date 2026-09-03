


local phaseConfig = require_ex("Phase/PhaseConfig")
local luaLoader = require_ex('Common/Utils/LuaResourceLoader')

PhaseManager = HL.Class("PhaseManager")

local PHASE_DUMMY_NAVI_LAYER_KEY_FORMAT = "Phase%s"



PhaseManager.curPhase = HL.Field(HL.Forward("PhaseBase"))

PhaseManager.phaseIds = HL.Field(HL.Table)

PhaseManager.phaseId2Names = HL.Field(HL.Table)

PhaseManager.m_openedPhaseSet = HL.Field(HL.Table)

PhaseManager.m_cfgs = HL.Field(HL.Table)

PhaseManager.m_transCor = HL.Field(HL.Thread)

PhaseManager.m_curState = HL.Field(HL.Number) << 1

PhaseManager.m_phaseStack = HL.Field(HL.Forward("Stack"))

PhaseManager.m_waitingQueue = HL.Field(HL.Forward("Queue"))

PhaseManager.m_phaseOpenStartTime = HL.Field(HL.Table)

PhaseManager.m_waitingDestroyList = HL.Field(HL.Table)

PhaseManager.m_defaultFOV = HL.Field(HL.Number) << -1

PhaseManager.m_cacheTable = HL.Field(HL.Table)

PhaseManager.m_resourceLoader = HL.Field(HL.Forward("LuaResourceLoader"))

PhaseManager.m_cacheRoot = HL.Field(Transform)

PhaseManager.m_isExitingAll = HL.Field(HL.Boolean) << false


PhaseManager.m_effectLodControlBlockers = HL.Field(HL.Table)

PhaseManager.isRecovering = HL.Field(HL.Boolean) << false






PhaseManager.PhaseManager = HL.Constructor() << function(self)
    self.m_curState = Const.PhaseState.Idle
    self.curPhase = nil
    self:_OnTopPhaseChanged()
    self.m_isExitingAll = false
    self.phaseIds = {}
    self.phaseId2Names = {}
    self.m_openedPhaseSet = {}
    self.m_cfgs = {}
    self.m_phaseOpenStartTime = {}
    self.m_waitingDestroyList = {}
    self.m_phaseStack = require_ex("Common/Utils/DataStructure/Stack")()
    self.m_waitingQueue = require_ex("Common/Utils/DataStructure/Queue")()
    self.m_cacheTable = {}
    self.m_effectLodControlBlockers = {}
    self.m_resourceLoader = luaLoader.LuaResourceLoader()
    local cacheRoot = GameObject("PhaseCacheRoot")
    GameObject.DontDestroyOnLoad(cacheRoot)
    self.m_cacheRoot = cacheRoot.transform

    Register(MessageConst.OPEN_PHASE_FOR_CS, function(arg)
        local phaseIdName, phaseArgJson = unpack(arg)
        local phaseArg
        if phaseArgJson then
            phaseArg = Utils.stringJsonToTable(phaseArgJson)
        end
        self:OpenPhase(PhaseId[phaseIdName], phaseArg)
    end, self)

    Register(MessageConst.EXIT_ALL_PHASE, function(arg)
        local reason = unpack(arg)
        if reason == CS.Beyond.EventSystemConst.ExitAllPhaseReason.SEAMLESS_LOADING then
            self:_ExitOnSeamlessLoading()
        else
            self:_ExitAndCloseAll()
        end
    end, self)

    Register(MessageConst.ON_CLEAN_CACHE_POOL, function(arg)
        UIManager:ReleaseCachedPanelAsset()
    end, self)

end

PhaseManager.InitPhaseConfigs = HL.Method() << function(self)
    self:_InitConfig()
    self:_InitBackMsgs()
    self.m_defaultFOV = UIManager:GetUICameraFOV()
end

PhaseManager._OnTopPhaseChanged = HL.Method() << function(self)
    if self.curPhase then
        EventLogManagerInst.curTopUIPhaseName = self.curPhase.cfg.name
    else
        EventLogManagerInst.curTopUIPhaseName = ""
    end
    Notify(MessageConst.UPDATE_BLOCK_INPUT_WHEN_PHASE_LEVEL)
end

PhaseManager._InitConfig = HL.Method() << function(self)
    local nextId = 1
    for name, data in pairs(phaseConfig.config) do
        local id = nextId
        nextId = nextId + 1
        local filePath
        if data.isSimpleUIPhase then
            filePath = 'Phase/Core/PhaseBase'
        else
            filePath = string.format(PhaseConst.PHASE_FILE_PATH, name, name)
        end
        local check = require_check(filePath)
        if check then
            local cfg = {
                name = name,
                id = id,
                data = data
            }
            data.name = name
            setmetatable(cfg, { __index = data })
            self.m_cfgs[id] = cfg
            self.phaseIds[name] = id
            self.phaseId2Names[id] = name
        else
            logger.error("No Phase class but in PhaseConst: " .. name)
        end
    end
end

PhaseManager._InitBackMsgs = HL.Method() << function(self)
    for _, cfg in pairs(self.m_cfgs) do
        if not cfg.isSimpleUIPhase then
            local name = cfg.name
            local phase = require_ex(string.format(PhaseConst.PHASE_FILE_PATH, name, name))["Phase" .. name]
            local messages = phase.s_messages or {}
            for msg, infos in pairs(messages) do
                local funcName, isForeground = unpack(infos)
                if not isForeground and funcName then
                    Register(msg, function(msgArg)
                        if msgArg == nil then
                            phase[funcName]()
                        else
                            phase[funcName](msgArg)
                        end
                    end, self)
                end
            end
        end
    end
end






PhaseManager.GoToPhase = HL.Method(HL.Number, HL.Opt(HL.Any, HL.Function, HL.Boolean)) << function(self, phaseId, arg, callback, couldWait)
    logger.info("PhaseManager.GoToPhase", phaseId, self:GetPhaseName(phaseId), arg)

    if self.m_isExitingAll then
        logger.info("Phase isExitingAll, GotoPhase Fail", phaseId, self:GetPhaseName(phaseId))
        return
    end

    if self:GetTopPhaseId() == phaseId then

        self:ForceRefreshPhase(phaseId, arg)
        if callback then callback() end
    elseif self:IsOpen(phaseId) then

        self:ExitPhaseFastTo(phaseId)
        self:ForceRefreshPhase(phaseId, arg)
        if callback then callback() end
    else

        self:OpenPhase(phaseId, arg, callback, couldWait)
    end
end

PhaseManager.OpenPhase = HL.Method(HL.Number, HL.Opt(HL.Any, HL.Function, HL.Boolean)).Return(HL.Boolean) << function(self, phaseId, arg, callback, couldWait)
    logger.info("PhaseManager.OpenPhase", phaseId, self:GetPhaseName(phaseId), arg, couldWait)

    if self.m_isExitingAll then
        logger.info("Phase isExitingAll, OpenPhase Fail", phaseId, self:GetPhaseName(phaseId))
        return false
    end

    if not self:CheckCanOpenPhaseAndToast(phaseId, arg) then
        return false
    end

    if not couldWait and self.m_transCor then
        if UNITY_EDITOR then

            logger.error("正在进行其他Phase操作，如需等待操作完成后进行，couldWait需为true", phaseId, self:GetPhaseName(phaseId), arg)
        else
            logger.warn("正在进行其他Phase操作，如需等待操作完成后进行，couldWait需为true", phaseId, self:GetPhaseName(phaseId), arg)
        end
        return false
    end

    self.m_phaseOpenStartTime[phaseId] = Time.realtimeSinceStartupAsDouble
    self.m_waitingQueue:Push({ phaseId, arg, false, callback })


    self:_TryOpenPhase()

    return true
end

PhaseManager.OpenPhaseFast = HL.Method(HL.Number, HL.Opt(HL.Any)).Return(HL.Boolean) << function(self, phaseId, arg)
    logger.info("PhaseManager.OpenPhaseFast", phaseId, self:GetPhaseName(phaseId), arg)

    if self.m_isExitingAll then
        logger.info("Phase isExitingAll, OpenPhaseFast Fail", phaseId, self:GetPhaseName(phaseId))
        return false
    end

    if not self:CheckCanOpenPhaseAndToast(phaseId, arg) then
        return false
    end

    self.m_phaseOpenStartTime[phaseId] = Time.realtimeSinceStartupAsDouble
    self.m_waitingQueue:Push({ phaseId, arg, true, nil })


    self:_TryOpenPhase()

    return true
end

PhaseManager.PopPhase = HL.Method(HL.Number, HL.Opt(HL.Function)).Return(HL.Boolean) << function(self, phaseId, callback)
    logger.info("PhaseManager.PopPhase", phaseId, self:GetPhaseName(phaseId))

    if not self:CanPopPhase(phaseId) then
        return false
    end

    self:_DoPopPhase(callback)
    return true
end

PhaseManager.CanPopPhase = HL.Method(HL.Number).Return(HL.Boolean) << function(self, phaseId)
    if self.m_isExitingAll then
        logger.info("Phase isExitingAll, PopPhase Fail", phaseId, self:GetPhaseName(phaseId))
        return false
    end

    local top = self.m_phaseStack:Peek()
    if not top or top.phaseId ~= phaseId then
        logger.error("PopPhase Error, No Top or PhaseId Mismatch", phaseId, top and top.phaseId, self.phaseId2Names[phaseId], top and self.phaseId2Names[top.phaseId])
        return false
    end

    if self.m_curState == Const.PhaseState.Pop then
        if UNITY_EDITOR then

            logger.error("正在退出 Phase，不能再次调用退出 Phase", phaseId, self.phaseId2Names[phaseId])
        else
            logger.warn("正在退出 Phase，不能再次调用退出 Phase", phaseId, self.phaseId2Names[phaseId])
        end
        return false
    elseif self.m_curState == Const.PhaseState.Push then
        if UNITY_EDITOR then

            logger.error("正在进入 Phase，不能调用退出 Phase", phaseId, self.phaseId2Names[phaseId])
        else
            logger.warn("正在进入 Phase，不能调用退出 Phase", phaseId, self.phaseId2Names[phaseId])
        end
        return false
    end

    return true
end










PhaseManager._IsPhaseInTransitionRef = HL.Method(HL.Forward("PhaseBase")).Return(HL.Boolean) << function(self, phase)
    if self.m_curState == Const.PhaseState.Idle then
        return false
    end

    if self.m_waitingDestroyList[phase] then
        return true
    end


    if self.m_curState == Const.PhaseState.Push then
        local topIdx = self.m_phaseStack:TopIndex()
        if topIdx and topIdx >= 2 and phase == self.m_phaseStack:Get(topIdx - 1) then
            return true
        end
    end

    if phase == self.curPhase then
        return phase.state ~= PhaseConst.EPhaseState.Activated
    end
    return false
end

PhaseManager.ExitPhaseFast = HL.Method(HL.Number) << function(self, phaseId)
    logger.info("PhaseManager.ExitPhaseFast", phaseId, self:GetPhaseName(phaseId))

    if self.m_isExitingAll then
        logger.info("Phase isExitingAll, ExitPhaseFast Fail", phaseId, self:GetPhaseName(phaseId))
        return
    end

    if self.m_phaseStack:Empty() then
        logger.error("Phase Stack Is Empty", phaseId, self.phaseId2Names[phaseId])
        return
    end

    local startTime = Time.realtimeSinceStartupAsDouble
    local phase = self.m_openedPhaseSet[phaseId]
    if phase then
        if self:_IsPhaseInTransitionRef(phase) then
            logger.critical("PhaseManager.ExitPhaseFast 跳过：phase 正被当前 transition 协程引用",
                phaseId, self:GetPhaseName(phaseId),
                "m_curState=", self.m_curState, "phase.state=", phase.state)
            return
        end
        self.m_curState = Const.PhaseState.Pop
        local isExitTopPhase = phase == self.m_phaseStack:Peek()
        self.m_openedPhaseSet[phaseId] = nil
        self:_DoFinishPhase(phase, true)
        if isExitTopPhase then
            local newPhase = self.m_phaseStack:Peek()
            self.curPhase = newPhase
            self:_OnTopPhaseChanged()
            self:_DoPopTransition(nil, newPhase, true)
            local costSecond = Time.realtimeSinceStartupAsDouble - startTime
            EventLogManagerInst:GameEvent_UISwitch(phase.cfg.name, newPhase and newPhase.cfg.name or "", false, costSecond)
        end
        self.m_curState = Const.PhaseState.Idle
    else
        logger.error("Phase Not Found", phaseId, self.phaseId2Names[phaseId])
    end

    self:_UpdateIsUsingSystemSceneCamera()
end

PhaseManager.ExitPhaseFastTo = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, targetPhaseId, forceExit)
    logger.info("PhaseManager.ExitPhaseFastTo", targetPhaseId, self:GetPhaseName(targetPhaseId), forceExit)

    if self.m_isExitingAll then
        logger.info("Phase isExitingAll, ExitPhaseFastTo Fail", targetPhaseId, self:GetPhaseName(targetPhaseId))
        return
    end

    if self.m_phaseStack:Empty() then
        logger.error("Phase Stack Is Empty", targetPhaseId, self.phaseId2Names[targetPhaseId])
        return
    end
    if self.m_transCor and not forceExit then
        logger.error("PhaseManager.ExitPhaseFast self.m_transCor not nil", targetPhaseId, self.phaseId2Names[targetPhaseId])
        return
    end

    local startTime = Time.realtimeSinceStartupAsDouble
    self.m_curState = Const.PhaseState.Pop
    local isTop = true
    local oldTopPhaseName = self.curPhase.cfg.name
    for _ = self.m_phaseStack:Count(), 1, -1 do
        local phase = self.m_phaseStack:Peek()
        self.curPhase = phase
        self:_OnTopPhaseChanged()
        local phaseId = phase.phaseId
        if phaseId == targetPhaseId then
            if not isTop then
                self:_DoPopTransition(nil, phase, true)
                local costSecond = Time.realtimeSinceStartupAsDouble - startTime
                EventLogManagerInst:GameEvent_UISwitch(oldTopPhaseName, phase.cfg.name, false, costSecond)
            end
            self.m_curState = Const.PhaseState.Idle
            self:_UpdateIsUsingSystemSceneCamera()
            return
        end
        self.m_openedPhaseSet[phaseId] = nil
        self:_DoFinishPhase(phase, true)
        isTop = false
    end

    logger.error("ExitPhaseFastTo: No Target Phase", targetPhaseId)
    self.curPhase = nil
    self:_OnTopPhaseChanged()
    local costSecond = Time.realtimeSinceStartupAsDouble - startTime
    EventLogManagerInst:GameEvent_UISwitch(oldTopPhaseName, "", false, costSecond)
    self.m_curState = Const.PhaseState.Idle
    self:_UpdateIsUsingSystemSceneCamera()
end

PhaseManager.IsPhaseRepeated = HL.Method(HL.Number).Return(HL.Boolean) << function(self, phaseId)
    if self:IsInWaitingQueue(phaseId) then
        return true
    end

    local isOpen, phase = self:IsOpen(phaseId)
    if isOpen and phase ~= nil then
        local peekPhase = self.m_phaseStack:Peek()
        local isPeek = phase == peekPhase
        return isPeek and phase.state ~= PhaseConst.EPhaseState.TransitionOut and phase.state ~= PhaseConst.EPhaseState.WaitRelease
    end

    return false
end

PhaseManager.ForceRefreshPhase = HL.Method(HL.Number, HL.Any) << function(self, phaseId, arg)
    logger.info("PhaseManager.ForceRefreshPhase", phaseId, self:GetPhaseName(phaseId), arg)

    if phaseId == nil then
        return
    end

    local phase = self.m_openedPhaseSet[phaseId]
    if phase == nil then
        return
    end

    if not self:CheckCanRefreshPhaseAndToast(phaseId, arg) then
        return
    end

    phase:Refresh(arg)
end

PhaseManager.GetTopPhaseId = HL.Method().Return(HL.Opt(HL.Number)) << function(self)
    local topPhase = self.m_phaseStack:Peek()
    if not topPhase then
        return
    end
    return topPhase.phaseId
end

PhaseManager.GetTopPhaseName = HL.Method().Return(HL.String) << function(self)
    local topPhaseId = self:GetTopPhaseId()
    if topPhaseId == nil then
        return ""
    end
    local cfg = self.m_cfgs[topPhaseId]
    return cfg.name
end

PhaseManager.GetTopOpenAndValidPhaseId = HL.Method().Return(HL.Opt(HL.Number)) << function(self)

    local stack = self.m_phaseStack
    for i = stack.m_topIndex, stack.m_bottomIndex, -1 do
        local topPhase = stack:Get(i)
        local isValid = self:IsOpenAndValid(topPhase.phaseId)
        if isValid then
            return topPhase.phaseId
        end
    end
end






PhaseManager.TryCacheGOByName = HL.Method(HL.Number, HL.String, HL.Opt(HL.Function)) << function(self, phaseId, name, callback)
    if not self.m_cfgs[phaseId] then
        logger.error("PhaseManager.TryCacheGOByName Error: wrong phaseId ", phaseId, self.phaseId2Names[phaseId])
        return
    end
    local key = phaseId .. name
    if not self.m_cacheTable[key] then
        local phaseName = self.m_cfgs[phaseId].name
        local path = string.format(PhaseConst.PHASE_GAME_OBJECT_FILE_PATH, phaseName, name)
        self.m_resourceLoader:LoadGameObjectAsync(path, function(goAsset)
            if goAsset then
                local goObj = CSUtils.CreateObject(goAsset)
                self.m_cacheTable[key] = goObj
                goObj.transform:SetParent(self.m_cacheRoot)
                goObj:SetActive(false)
                if callback then
                    callback(goObj)
                end
            end
        end)
    elseif callback then
        if callback then
            callback(self.m_cacheTable[key])
        end
    end
end

PhaseManager.TryCacheGo = HL.Method(HL.Number, HL.String, CS.UnityEngine.GameObject) << function(self, phaseId, name, go)
    local key = phaseId .. name
    if self.m_cacheTable[key] or not go then
        logger.error("PhaseManager TryCacheGo error: already cached, " .. name)
    else
        self.m_cacheTable[key] = go
        self.m_cacheTable[key]:SetActive(false)
    end
end

PhaseManager.GetCachedGameObject = HL.Method(HL.Number, HL.String).Return(CS.UnityEngine.GameObject) << function(self, phaseId, name)
    local key = phaseId .. name
    local res
    if self.m_cacheTable[key] then
        res = self.m_cacheTable[key]
        self.m_cacheTable[key] = nil
    end
    return res
end

PhaseManager.ReleaseCache = HL.Method(HL.Number, HL.String) << function(self, phaseId, name)
    local key = phaseId .. name
    local obj = self.m_cacheTable[key]
    if obj then
        CSUtils.ClearUIComponents(obj)
        GameObject.Destroy(obj)
        self.m_cacheTable[key] = nil
    end
end

PhaseManager.ReleaseAllCache = HL.Method() << function(self)
    for k, obj in pairs(self.m_cacheTable) do
        CSUtils.ClearUIComponents(obj)
        GameObject.Destroy(obj)
    end
    self.m_cacheTable = {}
end






PhaseManager.IsOpen = HL.Method(HL.Number).Return(HL.Boolean, HL.Opt(HL.Forward("PhaseBase"))) << function(self, phaseId)
    local phase = self.m_openedPhaseSet[phaseId]
    return phase ~= nil, phase
end

PhaseManager.IsOpenAndValid = HL.Method(HL.Number).Return(HL.Boolean, HL.Opt(HL.Forward("PhaseBase"))) << function(self, phaseId)
    local phase = self.m_openedPhaseSet[phaseId]
    if phase and not self.m_waitingDestroyList[phase] then
        return true, phase
    end
    return false
end

PhaseManager.IsEmptyPhase = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_phaseStack:Empty()
end

PhaseManager.IsInWaitingQueue = HL.Method(HL.Number).Return(HL.Boolean) << function(self, phaseId)
    local queue = self.m_waitingQueue
    if not queue:Empty() then
        for k = 1, queue:Count() do
            local item = queue:AtIndex(k)
            local id = unpack(item)
            if id == phaseId then
                return true
            end
        end
    end
    return false
end

PhaseManager.IsPhaseUnlocked = HL.Method(HL.Number).Return(HL.Boolean) << function(self, phaseId)
    if CS.Beyond.GlobalOptions.instance.auditing then

        if phaseId == PhaseId.CashShop then
            return true
        end
    end
    local cfg = self.m_cfgs[phaseId]
    if cfg.isUnlocked then
        return cfg.isUnlocked()
    end
    if cfg.unlockSystemType and cfg.unlockSystemType ~= GEnums.UnlockSystemType.None then
        return Utils.isSystemUnlocked(cfg.unlockSystemType)
    end
    if not string.isEmpty(cfg.systemId) then
        return Utils.isGameSystemUnlocked(cfg.systemId)
    end
    return true
end

PhaseManager.IsPhaseForbidden = HL.Method(HL.Number).Return(HL.Boolean, HL.Any, HL.Any) << function(self, phaseId)
    local cfg = self.m_cfgs[phaseId]
    if cfg.cannotForbid then
        return false, nil, nil
    end
    local isForbidden, forbidStyle = GameInstance.player.forbidSystem:IsPhaseForbidden(cfg.name)
    local forbidParams
    if isForbidden then
        forbidParams = GameInstance.player.forbidSystem:GetPhaseForbidParams(cfg.name)
    end
    return isForbidden, forbidStyle, forbidParams
end

PhaseManager.GetPhaseRedDotName = HL.Method(HL.Number).Return(HL.String) << function(self, phaseId)
    local cfg = self.m_cfgs[phaseId]
    local invalidName = ""
    if cfg == nil then
        return invalidName
    end

    if not string.isEmpty(cfg.redDotName) then
        return cfg.redDotName
    end

    if not cfg.systemId then
        return invalidName
    end
    local success, systemConfigData = Tables.gameSystemConfigTable:TryGetValue(cfg.systemId)
    if not success then
        return invalidName
    end

    return systemConfigData.redDotName
end

PhaseManager.GetPhaseSystemViewConfig = HL.Method(HL.Number).Return(HL.Table) << function(self, phaseId)
    local cfg = self.m_cfgs[phaseId]
    if cfg == nil or cfg.systemId == nil then
        return nil
    end

    local success, systemConfigData = Tables.gameSystemConfigTable:TryGetValue(cfg.systemId)
    if not success then
        return nil
    end

    return {
        ["systemName"] = systemConfigData.systemName,
        ["systemIcon"] = systemConfigData.systemIcon,
        ["systemDesc"] = systemConfigData.systemDesc,
    }
end






PhaseManager.CheckCanOpenPhaseAndToast = HL.Method(HL.Number, HL.Opt(HL.Any)).Return(HL.Boolean) << function(self, phaseId, arg)
    local rst, toast, errorInfo = self:_RealCheckCanOpenPhase(phaseId, arg, true, true)
    if rst then
        return true
    end
    if toast then
        Notify(MessageConst.SHOW_TOAST, toast)
    end
    if errorInfo then
        logger.error("CheckCanOpenPhase errorInfo: " .. errorInfo)
    end
    return false
end


PhaseManager.CheckIsInTransition = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_transCor ~= nil
end

PhaseManager.CheckCanOpenPhase = HL.Method(HL.Number, HL.Opt(HL.Any, HL.Boolean)).Return(HL.Boolean, HL.Opt(HL.String, HL.String)) << function(self, phaseId, arg, considerIsOpen)
    return self:_RealCheckCanOpenPhase(phaseId, arg, considerIsOpen)
end

PhaseManager.CheckCanRefreshPhaseAndToast = HL.Method(HL.Number, HL.Opt(HL.Any)).Return(HL.Boolean) << function(self, phaseId, arg)

    local rst, toast, errorInfo = self:_RealCheckCanOpenPhase(phaseId, arg)
    if rst then
        return true
    end
    if toast then
        Notify(MessageConst.SHOW_TOAST, toast)
    end
    if errorInfo then
        logger.error("CheckCanRefreshPhaseAndToast errorInfo: " .. errorInfo)
    end
    return false
end

PhaseManager._RealCheckCanOpenPhase = HL.Method(HL.Number, HL.Opt(HL.Any, HL.Boolean, HL.Boolean)).Return(HL.Boolean, HL.Opt(HL.String, HL.String)) << function(self, phaseId, arg, considerIsOpen, checkConflict)
    if not self:IsPhaseUnlocked(phaseId) then
        return false, Language.LUA_SYSTEM_LOCK
    end
    local isForbidden, forbidStyle, forbidParams = self:IsPhaseForbidden(phaseId)
    if isForbidden then
        local toast = Language.LUA_SYSTEM_FORBIDDEN
        if forbidParams and not string.isEmpty(forbidParams.toastTextId) then
            toast = Language[forbidParams.toastTextId]
        end
        return false, toast
    end
    local cfg = self.m_cfgs[phaseId]

    if checkConflict then
        local curAct = GameInstance.player.systemActionConflictManager.curProcessingSystemAction
        if curAct and curAct ~= Const.PhasePushSystemActionConflictName then

            local errStr = "正在进行其他互斥的行为 " .. GameInstance.player.systemActionConflictManager:GetCurProcessingSystemActionInfo()
            if UNITY_EDITOR then
                return false, nil, errStr
            else

                logger.warn(errStr)
                return false, nil
            end
        end
    end

    if cfg.checkCanOpen then
        local rst, toast = cfg.checkCanOpen(arg)
        if not rst then
            return false, toast
        end
    end

    local rst = true

    local errorInfo = ""
    if self.m_cfgs[phaseId] == nil then

        errorInfo = "PhaseManager OpenPhase error, not in config, phase: " .. phaseId .. " " .. tostring(self.phaseId2Names[phaseId])
        rst = false
    end

    if considerIsOpen then
        if self:IsOpen(phaseId) then
            local phase = self.m_openedPhaseSet[phaseId]
            if phase and (phase.state == PhaseConst.EPhaseState.TransitionOut or phase.state == PhaseConst.EPhaseState.WaitRelease) then
                rst = true
            else
                errorInfo = "PhaseManager already opened phase: " .. phaseId .. " " .. tostring(self.phaseId2Names[phaseId])
                rst = false
            end
        elseif self:IsInWaitingQueue(phaseId) then
            errorInfo = "PhaseManager OpenPhase error, Phase Already IsInWaitingQueue, phase: " .. phaseId .. " " .. tostring(self.phaseId2Names[phaseId])
            rst = false
        end
    end
    return rst, nil, errorInfo
end

PhaseManager._TryOpenPhase = HL.Method() << function(self)
    if self.m_transCor or self.m_waitingQueue:Empty() then
        return
    end
    local infos = self.m_waitingQueue:Front()
    local phaseId = infos[1]

    if not GameInstance.player.systemActionConflictManager:TryStartSystemAction(Const.PhasePushSystemActionConflictName, self.phaseId2Names[phaseId]) then
        logger.warn("PhaseManager._TryOpenPhase FAIL 正在进行其他互斥的行为 ", GameInstance.player.systemActionConflictManager:GetCurProcessingSystemActionInfo())
        return
    end


    GameInstance.playerController.commandController:ConsumeAllCommand()

    self.m_curState = Const.PhaseState.Push

    self.m_waitingQueue:Pop()
    local arg = infos[2]
    local fastMode = infos[3]
    local callback = infos[4]


    local newPhase = self:_CreatePhase(phaseId, arg)
    self.m_openedPhaseSet[phaseId] = newPhase
    self.m_phaseStack:Push(newPhase)

    Notify(MessageConst.UPDATE_DEBUG_PHASE_TEXT)
    Notify(MessageConst.ON_UI_PHASE_OPENED, newPhase.cfg.name)


    local oldPhase = self.curPhase
    self.curPhase = newPhase
    self:_OnTopPhaseChanged()
    if fastMode then
        self:_DoPushTransition(oldPhase, newPhase, true)
        self.m_curState = Const.PhaseState.Idle
        GameInstance.player.systemActionConflictManager:OnSystemActionEnd(Const.PhasePushSystemActionConflictName)
        if callback then
            callback()
        end
        self:_TryOpenPhase()
    else
        self.m_transCor = self:_StartCoroutine(function()
            self:_DoPushTransition(oldPhase, newPhase, false)
            self.m_curState = Const.PhaseState.Idle
            GameInstance.player.systemActionConflictManager:OnSystemActionEnd(Const.PhasePushSystemActionConflictName)
            if callback then
                callback()
            end
            self:_TryOpenPhase()
        end)
    end
end

PhaseManager._DoFinishPhase = HL.Method(HL.Forward("PhaseBase"), HL.Boolean) << function(self, phase, fastMode)
    Notify(MessageConst.ON_UI_PHASE_EXITED, phase.cfg.name)

    phase:TransitionOut(fastMode)
    self:_ReleasePhase(phase)

    Notify(MessageConst.DETACH_DUMMY_NAVI_LAYER, "Phase" .. phase.cfg.name)

    Notify(MessageConst.UPDATE_DEBUG_PHASE_TEXT)
end

PhaseManager._DoPopPhase = HL.Method(HL.Opt(HL.Function)) << function(self, callback)
    local startTime = Time.realtimeSinceStartupAsDouble

    self:_ClearTransCor()

    self.m_curState = Const.PhaseState.Pop


    local oldPhase = self.m_phaseStack:Pop()
    if oldPhase then
        self.m_waitingDestroyList[oldPhase] = true
    end
    Notify(MessageConst.UPDATE_DEBUG_PHASE_TEXT)
    Notify(MessageConst.ON_UI_PHASE_EXITED, oldPhase.cfg.name)

    local newTopPhase = self.m_phaseStack:Peek()
    self.curPhase = newTopPhase
    self:_OnTopPhaseChanged()
    self.m_transCor = self:_StartCoroutine(function()
        self:_DoPopTransition(oldPhase, newTopPhase, false)
        local costSecond = Time.realtimeSinceStartupAsDouble - startTime
        EventLogManagerInst:GameEvent_UISwitch(oldPhase.cfg.name, newTopPhase and newTopPhase.cfg.name or "", false, costSecond)
        self.m_curState = Const.PhaseState.Idle
        if callback then
            callback()
        end
        self:_TryOpenPhase()
    end)
end

PhaseManager._ClearTransCor = HL.Method() << function(self)
    if self.m_transCor ~= nil then
        self:_ClearCoroutine(self.m_transCor)
        self.m_transCor = nil
    end
    if GameInstance.IsAfter(CS.Beyond.GameState.Preload)  then

        GameInstance.player.systemActionConflictManager:OnSystemActionEnd(Const.PhasePushSystemActionConflictName)
    end
end

PhaseManager._CreatePhase = HL.Method(HL.Number, HL.Opt(HL.Any)).Return(HL.Forward("PhaseBase")) << function(self, phaseId, arg)
    local cfg = self.m_cfgs[phaseId]

    local name = cfg.name
    local phase
    if not cfg.isSimpleUIPhase then
        phase = require_ex(string.format(PhaseConst.PHASE_FILE_PATH, name, name))["Phase" .. name](arg)
    else
        phase = require_ex('Phase/Core/PhaseBase').PhaseBase(arg)
    end

    phase.phaseId = phaseId


    local messages = cfg.isSimpleUIPhase and {} or HL.GetType(phase).s_messages
    for msg, infos in pairs(messages) do
        local funcName, isForeground = unpack(infos)
        if isForeground then
            Register(msg, function(msgArg)
                if msgArg == nil then
                    phase[funcName](phase)
                else
                    phase[funcName](phase, msgArg)
                end
            end, phase)
        end
    end

    phase:InitWithCfg(cfg.data)

    return phase
end

PhaseManager._ReleasePhase = HL.Method(HL.Forward("PhaseBase")) << function(self, phase)
    local cfg = self.m_cfgs[phase.phaseId]

    phase:Destroy()
    UIManager:ClearPreloadAsset(phase.phaseId)
    if cfg.disableEffectLodControl then
        self:ResumeEffectLodByPhase(phase.phaseId)
    end
    self.m_waitingDestroyList[phase] = nil
    MessageManager:UnregisterAll(phase)
    if self.m_phaseStack:Contains(phase) then
        self.m_phaseStack:Delete(phase)
    end
    self.m_openedPhaseSet[phase.phaseId] = nil
end

PhaseManager._RefreshUIScaler = HL.Method() << function(self)
    local comp = UIManager.worldUICanvas:GetComponent("UICanvasScaleHelper")
    if comp then
        comp:UpdateCanvas()
    end
end






PhaseManager._StartCoroutine = HL.Method(HL.Function).Return(HL.Thread) << function(self, func)
    return CoroutineManager:StartCoroutine(func, self)
end

PhaseManager._ClearCoroutine = HL.Method(HL.Thread) << function(self, coroutine)
    CoroutineManager:ClearCoroutine(coroutine)
end

PhaseManager._Dispose = HL.Method() << function(self)




    UIManager:StartCacheRecoverScreen()
    self:_PopAllPhases()


    self:ReleaseAllCache()
    if self.m_cacheRoot then
        GameObject.DestroyImmediate(self.m_cacheRoot.gameObject)
        self.m_cacheRoot = nil
    end


    UIManager:Dispose()


    UIManager:EndCacheRecoverScreen()


    if self.m_resourceLoader ~= nil then
        self.m_resourceLoader:DisposeAllHandles()
    end

end






PhaseManager._PopAllPhases = HL.Method() << function(self)
    self.m_isExitingAll = true
    self:_ClearTransCor()

    local count = self.m_phaseStack:Count()
    for _ = 1, count do
        local phase = self.m_phaseStack:Pop()
        self.m_openedPhaseSet[phase.phaseId] = nil
        self:_DoFinishPhase(phase, true)
    end

    for phase, active in pairs(self.m_waitingDestroyList) do
        if active then
            self:_DoFinishPhase(phase, true)
        end
    end

    self.curPhase = nil
    self:_OnTopPhaseChanged()
    self.m_waitingDestroyList = {}
    self.m_openedPhaseSet = {}
    self.m_phaseStack:Clear()
    self.m_waitingQueue:Clear()
    self.m_isExitingAll = false
end

PhaseManager.ExitAllPhase = HL.Method() << function(self)
    logger.info("PhaseManager.ExitAllPhase")
    UIManager:StartCacheRecoverScreen()
    self:_PopAllPhases()
    UIManager:EndCacheRecoverScreen()
end

PhaseManager.ExitAllPhaseExceptLevel = HL.Method() << function(self)
    logger.info("ExitAllPhaseExceptLevel")
    self.m_isExitingAll = true
    self:_ClearTransCor()
    local levelPhaseId = PhaseId.Level
    local levelPhase = nil

    local count = self.m_phaseStack:Count()
    for _ = 1, count do
        local phase = self.m_phaseStack:Pop()
        if phase.phaseId == levelPhaseId then
            levelPhase = phase
        else
            self.m_openedPhaseSet[phase.phaseId] = nil
            self:_DoFinishPhase(phase, true)
        end
    end

    for phase, active in pairs(self.m_waitingDestroyList) do
        if active and phase.phaseId ~= levelPhaseId then
            self:_DoFinishPhase(phase, true)
        end
    end

    self.m_waitingDestroyList = {}
    self.m_openedPhaseSet = {}
    self.m_phaseStack:Clear()
    self.m_waitingQueue:Clear()

    if levelPhase then
        self.m_openedPhaseSet[levelPhaseId] = levelPhase
        self.m_phaseStack:Push(levelPhase)
        self.curPhase = levelPhase
    else
        self.curPhase = nil
    end
    self:_OnTopPhaseChanged()
    self.m_isExitingAll = false
end

PhaseManager._ExitAndCloseAll = HL.Method(HL.Opt(HL.Table)) << function(self, closeExceptPanelIds)
    self:ExitAllPhase()
    UIManager:CloseAllUI(true, closeExceptPanelIds)
end

PhaseManager._ExitOnSeamlessLoading = HL.Method(HL.Opt(HL.Table)) << function(self)
    self:ExitAllPhaseExceptLevel()

    local closeExceptPanelIds = {}
    for _, panelId in pairs(PhaseConst.DONT_DESTROY_ON_SEAMLESS_LOADING) do
        table.insert(closeExceptPanelIds, PanelId[panelId])
    end
    UIManager:CloseAllUI(true, closeExceptPanelIds)
end

PhaseManager._UpdateIsUsingSystemSceneCamera = HL.Method() << function(self)
    for _ = self.m_phaseStack:Count(), 1, -1 do
        local phase = self.m_phaseStack:Peek()
        if phase.cfg.haveSceneCamera then
            CameraManager.isUsingSystemSceneCamera = true
            logger.info("CameraManager.isUsingSystemSceneCamera = true")
            return
        end
    end
    CameraManager.isUsingSystemSceneCamera = false
    logger.info("CameraManager.isUsingSystemSceneCamera = false")
end






PhaseManager._DoPopTransition = HL.Method(HL.Forward("PhaseBase"), HL.Forward("PhaseBase"), HL.Boolean) << function(self, popPhase, newTopPhase, fastMode)
    logger.info("PhaseManager._DoPopTransition", popPhase and popPhase.cfg.name, newTopPhase and newTopPhase.cfg.name, fastMode)

    local newTopPhaseId = newTopPhase and newTopPhase.phaseId or nil
    local popPhaseId = popPhase and popPhase.phaseId or nil
    if popPhase then
        popPhase:PrepareTransition(PhaseConst.EPhaseState.TransitionOut, fastMode, newTopPhaseId)
    end
    if newTopPhase then
        newTopPhase:PrepareTransition(PhaseConst.EPhaseState.TransitionBackToTop, fastMode, popPhaseId)
    end

    UIManager:PauseMainCameraUpdate()


    if self:_IsSceneVisibleAfterTransition() then
        UIManager:AddMainCameraTempRequest("PhaseTransition")
    end

    if popPhase then
        popPhase:TransitionOut(fastMode, {
            anotherPhaseId = newTopPhaseId
        })
        if not fastMode and popPhase.state == PhaseConst.EPhaseState.TransitionOut then
            coroutine.waitCondition(function()
                return popPhase.state ~= PhaseConst.EPhaseState.TransitionOut
            end)
        end
        self:_ReleasePhase(popPhase)
        Notify(MessageConst.DETACH_DUMMY_NAVI_LAYER, "Phase" .. popPhase.cfg.name)
    end

    self:_UpdateIsUsingSystemSceneCamera()

    if newTopPhase then
        local cfg = newTopPhase.cfg
        local fov = cfg.fov
        if fov and fov > 0 then
            UIManager:SetUICameraFOV(fov)
        else
            local topIndex = self.m_phaseStack:TopIndex()
            local bottomIndex = self.m_phaseStack:BottomIndex()
            local hasFov = false
            for i = topIndex, bottomIndex, -1 do
                local oldCfg = self.m_phaseStack:Get(i).cfg
                local oldFov = oldCfg.fov
                if oldFov and oldFov > 0 then
                    UIManager:SetUICameraFOV(oldFov)
                    hasFov = true
                    break
                end
            end
            if not hasFov then
                logger.error("[PhaseManager._DoPopTransition] 当前Phase栈里没有Phase配置FOV！")
            end
        end
        newTopPhase:TransitionBackToTop(fastMode, {
            anotherPhaseId = popPhaseId
        })

        UIManager:ResumeMainCameraUpdate()

        if not fastMode and newTopPhase.state == PhaseConst.EPhaseState.TransitionBackToTop then
            coroutine.waitCondition(function()
                return newTopPhase.state ~= PhaseConst.EPhaseState.TransitionBackToTop
            end)
        end
    else
        UIManager:ResumeMainCameraUpdate()
    end



    UIManager:RemoveMainCameraTempRequest("PhaseTransition")

    if not fastMode then
        self.m_transCor = nil
    end
end

PhaseManager._DoPushTransition = HL.Method(HL.Forward("PhaseBase"), HL.Forward("PhaseBase"), HL.Boolean) << function(self, oldTopPhase, pushPhase, fastMode)
    logger.info("PhaseManager._DoPushTransition", oldTopPhase and oldTopPhase.cfg.name, pushPhase and pushPhase.cfg.name, fastMode)

    local oldTopPhaseId = oldTopPhase and oldTopPhase.phaseId or nil
    local pushPhaseId = pushPhase and pushPhase.phaseId or nil
    if oldTopPhase then
        oldTopPhase:PrepareTransition(PhaseConst.EPhaseState.TransitionBehind, fastMode, pushPhaseId)
    end
    if pushPhase then
        pushPhase:PrepareTransition(PhaseConst.EPhaseState.TransitionIn, fastMode, oldTopPhaseId)

        if not fastMode and pushPhase.cfg.panels then
            for _, panelId in ipairs(pushPhase.cfg.panels) do
                UIManager:PreloadPanelAsset(panelId, pushPhaseId)
            end
        end
    end

    UIManager:PauseMainCameraUpdate()



    if self:_IsSceneVisibleAfterTransition() then
        UIManager:AddMainCameraTempRequest("PhaseTransition")
    end

    if oldTopPhase then
        oldTopPhase:TransitionBehind(fastMode, {
            anotherPhaseId = pushPhaseId
        })
        if not fastMode and oldTopPhase.state == PhaseConst.EPhaseState.TransitionBehind then
            coroutine.waitCondition(function()
                return oldTopPhase.state ~= PhaseConst.EPhaseState.TransitionBehind
            end)
        end
    end

    self:_UpdateIsUsingSystemSceneCamera()

    if pushPhase then
        if pushPhase.cfg.notCreateDummyNaviLayer ~= true then
            Notify(MessageConst.ATTACH_DUMMY_NAVI_LAYER, string.format(PHASE_DUMMY_NAVI_LAYER_KEY_FORMAT, pushPhase.cfg.name))
        end
        local cfg = pushPhase.cfg
        local fov = cfg.fov
        if fov and fov > 0 then
            UIManager:SetUICameraFOV(fov)
        end
        self:_RefreshUIScaler()
        if cfg.disableEffectLodControl then
            self:DisableEffectLodByPhase(pushPhaseId)
        end
        pushPhase:TransitionIn(fastMode, {
            anotherPhaseId = oldTopPhaseId
        })


        local openCostSecond = 0
        if self.m_phaseOpenStartTime[pushPhase.phaseId] ~= nil then
            openCostSecond = Time.realtimeSinceStartupAsDouble - self.m_phaseOpenStartTime[pushPhase.phaseId]
            self.m_phaseOpenStartTime[pushPhase.phaseId] = nil
        end
        EventLogManagerInst:GameEvent_UISwitch(oldTopPhase and oldTopPhase.cfg.name or "", pushPhase.cfg.name, true, openCostSecond)


        UIManager:ResumeMainCameraUpdate()

        if not fastMode and pushPhase.state == PhaseConst.EPhaseState.TransitionIn then
            coroutine.waitCondition(function()
                return pushPhase.state ~= PhaseConst.EPhaseState.TransitionIn
            end)
        end
    else
        UIManager:ResumeMainCameraUpdate()
    end


    UIManager:RemoveMainCameraTempRequest("PhaseTransition")

    if not fastMode then
        self.m_transCor = nil
    end
end





PhaseManager.DisableEffectLodByPhase = HL.Method(HL.Number) << function(self, phaseId)
    if next(self.m_effectLodControlBlockers) == nil then
        GameInstance.effectManager:SetIsCheckDistanceLod(false)
    end
    self.m_effectLodControlBlockers[phaseId] = true
end

PhaseManager.ResumeEffectLodByPhase = HL.Method(HL.Number) << function(self, phaseId)
    self.m_effectLodControlBlockers[phaseId] = nil
    if next(self.m_effectLodControlBlockers) == nil then
        GameInstance.effectManager:SetIsCheckDistanceLod(true)
    end
end





PhaseManager.GetPhaseStack = HL.Method().Return(HL.Forward("Stack")) << function(self)
    return self.m_phaseStack
end

PhaseManager.GetPhaseCfgs = HL.Method().Return(HL.Table) << function(self)
    return self.m_cfgs
end

PhaseManager.GetPhaseName = HL.Method(HL.Number).Return(HL.String) << function(self, phaseId)
    local cfg = self.m_cfgs[phaseId]
    return cfg and cfg.name or ""
end

PhaseManager.IsPhaseHaveSceneCamera = HL.Method(HL.Opt(HL.Number)).Return(HL.Boolean) << function(self, phaseId)
    if not phaseId then
        return false
    end
    local cfg = self.m_cfgs[phaseId]
    return cfg ~= nil and cfg.haveSceneCamera == true
end













PhaseManager._IsSceneVisibleAfterTransition = HL.Method().Return(HL.Boolean) << function(self)
    local topPhase = self.m_phaseStack:Peek()
    if topPhase == nil then
        return false
    end
    return topPhase.cfg.sceneVisible == true or topPhase.cfg.haveSceneCamera == true
end

PhaseManager.GetPhaseDummyNaviLayerName = HL.Method(HL.Number).Return(HL.String) << function(self, phaseId)
    return string.format(PHASE_DUMMY_NAVI_LAYER_KEY_FORMAT, self:GetPhaseName(phaseId))
end

PhaseManager.GetPhaseIdByName = HL.Method(HL.String).Return(HL.Number) << function(self, phaseName)
    if self.phaseIds[phaseName] ~= nil then
        return self.phaseIds[phaseName]
    end
    return -1
end







PhaseManager.CollectCurPhaseArgs = HL.Method().Return(HL.Table) << function(self)
    local args = {}
    for k = 1, self.m_phaseStack:Count() do
        local phase = self.m_phaseStack:Get(k)
        local arg = phase:GetCurStateArg()
        if arg ~= nil and arg == phase.arg and type(arg) == "table" then
            logger.error("PhaseBase.GetCurStateArg Override后返回的arg是该Phase本身持有的arg 需要深拷贝返回一份独立的arg", phase.cfg.name)
        end
        args[k] = {
            id = phase.phaseId,
            name = phase.cfg.name,
            arg = arg,
        }
    end
    logger.info("PhaseManager.CollectCurPhaseArgs", args)
    return args
end

PhaseManager.RecoverPhaseByArgs = HL.Method(HL.Table) << function(self, args)
    logger.info("PhaseManager.RecoverPhaseByArgs", args)
    self.isRecovering = true
    for _, v in ipairs(args) do
        self:OpenPhaseFast(v.id, v.arg)
    end
    self.isRecovering = false
end




HL.Commit(PhaseManager)
return PhaseManager
