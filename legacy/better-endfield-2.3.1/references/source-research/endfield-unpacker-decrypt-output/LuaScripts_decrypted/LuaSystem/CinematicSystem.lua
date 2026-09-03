
local AdventureLevelUpCtrl = require_ex('UI/Panels/AdventureLevelUp/AdventureLevelUpCtrl').AdventureLevelUpCtrl
local LuaSystemBase = require_ex('LuaSystem/LuaSystemBase')
CinematicSystem = HL.Class('CinematicSystem', LuaSystemBase.LuaSystemBase)

CinematicSystem.m_queueItems = HL.Field(HL.Table)

CinematicSystem.m_waitPlayQueueItems = HL.Field(HL.Table)

CinematicSystem.CinematicSystem = HL.Constructor() << function(self)
    self:RegisterMessage(MessageConst.ADD_CINEMATIC_ITEM_TO_QUEUE, function(arg)
        local data = unpack(arg)
        self:AddCinematic2Queue(data)
    end)
    self:RegisterMessage(MessageConst.END_CINEMATIC_QUEUE_ITEM, function(arg)
        local data = unpack(arg)
        self:EndCinematicQueueItem(data)
    end)
    self:RegisterMessage(MessageConst.TOGGLE_IGNORE_CINEMATIC_QUEUE, function(arg)
        local toggle = unpack(arg)
        self:ToggleIgnoreCinematicQueue(toggle)
    end)
end

CinematicSystem.AddCinematic2Queue = HL.Method(HL.Any) << function(self, handle)
    if handle.data.playImmediately then
        self:AddCinematicBlocker()
        self:_DoAction(handle)
        return
    end

    local data = handle.data

    if data.queueItemType == Const.CinematicQueueItemTypeEnum.ForceSNS then
        local needAfterLevelUp = SNSUtils.isSNSDialogLevelUpRelated(data.dialogId) and AdventureLevelUpCtrl.HaveAdventureLevelUpInQueue()
        data.isAfterLevelUpSNS = needAfterLevelUp
        if needAfterLevelUp then

            LuaSystemManager.mainHudActionQueue:AddRequest("AfterLevelUpSNS", function(_)



                FactoryUtils.exitFactoryRelatedMode()

                self:_DoAction(handle)
            end)
            Notify(MessageConst.ON_CINEMATIC_TO_QUEUE)
            return
        end
        data.isAfterLevelUpSNS = false
    end

    LuaSystemManager.mainHudActionQueue:AddRequest(Const.CinematicQueueType, function(_)
        self:_DoAction(handle)
    end, function()
        local _, order = CinematicUtils.TryGetCinematicPriority(data)
        return order
    end, true)
    Notify(MessageConst.ON_CINEMATIC_TO_QUEUE)
    logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "CinematicSystem.AddCinematic2Queue", data.id)
end

CinematicSystem._DoAction = HL.Method(HL.Any) << function(self, handle)
    local data = handle.data
    local id = handle.id
    local queueItemType = data.queueItemType
    logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "CinematicSystem._DoAction", queueItemType, id)

    local res
    if not self.m_waitPlayQueueItems[id] then
        self.m_waitPlayQueueItems[id] = handle
    end
    if queueItemType == Const.CinematicQueueItemTypeEnum.Dialog then
        res = GameAction.DoPlayDialogByHandle(handle)
    elseif queueItemType == Const.CinematicQueueItemTypeEnum.Cutscene then
        res = GameAction.DoPlayCutsceneByHandle(handle)
    elseif queueItemType == Const.CinematicQueueItemTypeEnum.FMV then
        res = GameAction.PlayCGByHandle(handle)
    elseif queueItemType == Const.CinematicQueueItemTypeEnum.RemoteComm then
        res = GameAction.StartRemoteCommByHandle(handle)
    elseif queueItemType == Const.CinematicQueueItemTypeEnum.NarrativeBlackScreen then
        res = GameAction.ShowNarrativeBlackScreenByHandle(handle)
    elseif queueItemType == Const.CinematicQueueItemTypeEnum.ReadingPop then
        res = GameAction.ShowUIReadingPopPanelByHandle(handle)
    elseif queueItemType == Const.CinematicQueueItemTypeEnum.ForceSNS then
        res = GameAction.DoPlayForceSNSByHandle(handle)
    end
    if res and self.m_waitPlayQueueItems[id] then
        self.m_queueItems[id] = handle
        self.m_waitPlayQueueItems[id] = nil
    end
end

CinematicSystem.EndCinematicQueueItem = HL.Method(HL.Any) << function(self, handle)
    local id = handle.id
    local needNotify = false

    if self.m_queueItems[id] then
        local data = self.m_queueItems[id].data
        if data and data.playImmediately then
            needNotify = false
        else
            needNotify = true
        end
        self.m_queueItems[id] = nil
    end
    if self.m_waitPlayQueueItems[id] then
        local data = self.m_waitPlayQueueItems[id].data
        if data and data.playImmediately then
            needNotify = false
        else
            needNotify = true
        end
        self.m_waitPlayQueueItems[id] = nil
    end

    if needNotify then
        if handle.data.queueItemType == Const.CinematicQueueItemTypeEnum.ForceSNS and handle.data.isAfterLevelUpSNS then
            if LuaSystemManager.mainHudActionQueue:HasRequest("AfterLevelUpSNS") then
                Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, "AfterLevelUpSNS")
            end
        else
            if LuaSystemManager.mainHudActionQueue:HasRequest(Const.CinematicQueueType) then
                Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, Const.CinematicQueueType)
            end
        end
    end


    self:RemoveCinematicBlocker()
    logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "CinematicSystem.EndCinematicQueueItem", needNotify, id)
end

CinematicSystem.ToggleIgnoreCinematicQueue = HL.Method(HL.Boolean) << function(self, toggle)
    LuaSystemManager.mainHudActionQueue:ToggleActionPlayIgnoreMainHud("Cinematic", toggle)
    logger.important(CS.Beyond.EnableLogType.MainHudActionQueue, "CinematicSystem.ToggleIgnoreCinematicQueue", toggle)
end

CinematicSystem.OnInit = HL.Override() << function(self)
    self.m_queueItems = {}
    self.m_waitPlayQueueItems = {}
end

CinematicSystem.OnRelease = HL.Override() << function(self)
    self.m_queueItems = {}
    self.m_waitPlayQueueItems = {}
end


CinematicSystem.AddCinematicBlocker = HL.Method() << function(self)

    if LuaSystemManager.mainHudActionQueue:HasRequest("CinematicBlocker") then
        logger.error("CinematicBlocker 已存在！应该同时只有一个，多个则说明有问题！")
        return
    end
    LuaSystemManager.mainHudActionQueue:AddRequest("CinematicBlocker", function()
    end, nil, true)
end

CinematicSystem.RemoveCinematicBlocker = HL.Method() << function(self)
    if not LuaSystemManager.mainHudActionQueue:HasRequest("CinematicBlocker") then
        return
    end

    LuaSystemManager.mainHudActionQueue:RemoveActionsOfType("CinematicBlocker")
end


HL.Commit(CinematicSystem)
return CinematicSystem
