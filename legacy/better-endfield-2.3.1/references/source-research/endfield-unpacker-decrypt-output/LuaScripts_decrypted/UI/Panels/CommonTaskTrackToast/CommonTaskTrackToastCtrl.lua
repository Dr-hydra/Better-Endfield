
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonTaskTrackToast

local ToastType = {
    Start = "StartToast",
    Fail = "FailToast",
    Finish = "FinishToast",
    Countdown = "CountdownToast",
}
local CountdownToast = "CountdownToast"

local showTimeRecordCategoryList = {
    DungeonConst.DUNGEON_CATEGORY.BossRush,
}


local WorldChallengeStartToast = "WorldChallengeStartToast"
local DEFAULT_ICON = "challenge_icon"
CommonTaskTrackToastCtrl = HL.Class('CommonTaskTrackToastCtrl', uiCtrl.UICtrl)

CommonTaskTrackToastCtrl.m_countDownTickId = HL.Field(HL.Number) << -1

CommonTaskTrackToastCtrl.m_showingToastCor = HL.Field(HL.Thread)




CommonTaskTrackToastCtrl.s_cachedCompleteResult = HL.StaticField(HL.Table)



CommonTaskTrackToastCtrl.OnDungeonComplete = HL.StaticMethod(HL.Any) << function(args)
    local isNewTimeRecord, curGameTimeRecord = unpack(args)
    if CommonTaskTrackToastCtrl.s_cachedCompleteResult == nil then
        CommonTaskTrackToastCtrl.s_cachedCompleteResult = {}
    end

    CommonTaskTrackToastCtrl.s_cachedCompleteResult.isNewTimeRecord = isNewTimeRecord
    CommonTaskTrackToastCtrl.s_cachedCompleteResult.curGameTimeRecord = curGameTimeRecord
end







CommonTaskTrackToastCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SUB_GAME_RESET] = "OnSubGameReset",
}

CommonTaskTrackToastCtrl.OnShowCommonTaskCountdownToast = HL.StaticMethod(HL.Any) << function(args)
    LuaSystemManager.commonTaskTrackSystem:AddRequest("TrackStartCountdown", function()

        local ctrl = CommonTaskTrackToastCtrl.AutoOpen(PANEL_ID, nil, true)
        if ctrl == nil then
            return
        end

        ctrl:ShowCountdownToast(args)
    end)
end

CommonTaskTrackToastCtrl.OnShowCommonTaskStartToast = HL.StaticMethod(HL.Any) << function(args)
    LuaSystemManager.commonTaskTrackSystem:AddRequest("TrackStartToast", function()

        local ctrl = CommonTaskTrackToastCtrl.AutoOpen(PANEL_ID, nil, true)
        if ctrl == nil then
            return
        end

        ctrl:ShowTaskStartToast(args, function()
            Notify(MessageConst.ON_ONE_COMMON_TASK_PANEL_FINISH, "TrackStartToast")
            Notify(MessageConst.SHOW_DUNGEON_TOAST, args)
        end)
    end, function()
        UIManager:Close(PANEL_ID)
        Notify(MessageConst.ON_HUD_BTN_VISIBLE_CHANGE, {true})
    end)
end

CommonTaskTrackToastCtrl.OnShowCommonTaskFinishToast = HL.StaticMethod(HL.Any) << function(args)
    LuaSystemManager.commonTaskTrackSystem:AddRequest("TrackEndToast", function()

        local ctrl = CommonTaskTrackToastCtrl.AutoOpen(PANEL_ID, nil, true)
        if ctrl == nil then
            return
        end

        ctrl:ShowTaskFinishToast(args, function()
            Notify(MessageConst.ON_ONE_COMMON_TASK_PANEL_FINISH, "TrackEndToast")
        end)
    end, function()
        UIManager:Close(PANEL_ID)
    end)
end

CommonTaskTrackToastCtrl.OnShowCommonTaskFailToast = HL.StaticMethod(HL.Any) << function(args)
    LuaSystemManager.commonTaskTrackSystem:AddRequest("TrackEndToast", function()

        local ctrl = CommonTaskTrackToastCtrl.AutoOpen(PANEL_ID, nil, true)
        if ctrl == nil then
            return
        end

        ctrl:ShowTaskFailToast(args, function()
            Notify(MessageConst.ON_ONE_COMMON_TASK_PANEL_FINISH, "TrackEndToast")
        end)
    end, function()
        UIManager:Close(PANEL_ID)
    end)
end

CommonTaskTrackToastCtrl.OnShowCommonTaskStartToastWithoutId = HL.StaticMethod(HL.Any) << function(args)
    LuaSystemManager.commonTaskTrackSystem:AddRequest("TrackStartToast", function()

        local ctrl = CommonTaskTrackToastCtrl.AutoOpen(PANEL_ID, nil, true)
        if ctrl == nil then
            return
        end

        ctrl:ShowTaskStartToastWithoutId(args, function()
            Notify(MessageConst.ON_ONE_COMMON_TASK_PANEL_FINISH, "TrackStartToast")
        end)
    end, function()
        UIManager:Close(PANEL_ID)
        Notify(MessageConst.ON_HUD_BTN_VISIBLE_CHANGE, {true})
    end)
end

CommonTaskTrackToastCtrl.OnShowCommonTaskFinishToastWithoutId = HL.StaticMethod(HL.Any) << function(args)
    LuaSystemManager.commonTaskTrackSystem:AddRequest("TrackEndToast", function()

        local ctrl = CommonTaskTrackToastCtrl.AutoOpen(PANEL_ID, nil, true)
        if ctrl == nil then
            return
        end

        ctrl:ShowTaskFinishToastWithoutId(args, function()
            Notify(MessageConst.ON_ONE_COMMON_TASK_PANEL_FINISH, "TrackEndToast")
        end)
    end, function()
        UIManager:Close(PANEL_ID)
    end)
end

CommonTaskTrackToastCtrl.OnShowCommonTaskFailToastWithoutId = HL.StaticMethod(HL.Any) << function(args)
    LuaSystemManager.commonTaskTrackSystem:AddRequest("TrackEndToast", function()

        local ctrl = CommonTaskTrackToastCtrl.AutoOpen(PANEL_ID, nil, true)
        if ctrl == nil then
            return
        end

        ctrl:ShowTaskFailToastWithoutId(args, function()
            Notify(MessageConst.ON_ONE_COMMON_TASK_PANEL_FINISH, "TrackEndToast")
        end)
    end, function()
        UIManager:Close(PANEL_ID)
    end)
end


CommonTaskTrackToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end

CommonTaskTrackToastCtrl.OnClose = HL.Override() << function(self)
    if self.m_countDownTickId > 0 then
        self.m_countDownTickId = LuaUpdate:Remove(self.m_countDownTickId)
    end

    if self.m_showingToastCor then
        self.m_showingToastCor = self:_ClearCoroutine(self.m_showingToastCor)
    end

    CommonTaskTrackToastCtrl.s_cachedCompleteResult = nil
end

CommonTaskTrackToastCtrl._IsWorldFreeze = HL.Method().Return(HL.Boolean) << function(self)
    local isOpen, ctrl = UIManager:IsOpen(PanelId.CommonPopUp)
    return UIWorldFreezeManager:IsUIWorldFreeze() or isOpen and ctrl.m_timeScaleHandler > 0
end

CommonTaskTrackToastCtrl.ShowCountdownToast = HL.Method(HL.Any, HL.Opt(HL.Function)) << function(self, args, endFunc)
    self:Notify(MessageConst.ON_HUD_BTN_VISIBLE_CHANGE, {false})

    local countdownDuration, cb = unpack(args)
    local toast = Utils.wrapLuaNode(self:_CreateToastGO(CountdownToast))
    toast.contentTimeStart.gameObject:SetActiveIfNecessary(false)
    toast.contentTimeNumber.gameObject:SetActiveIfNecessary(true)

    local tickTimes = math.ceil(countdownDuration)
    local animFrequency = 1
    local timer = 1
    local endTimes = 0
    self.m_countDownTickId = LuaUpdate:Add("Tick", function(deltaTime)
        if self:_IsWorldFreeze() then
            toast.gameObject:SetActiveIfNecessary(false)
            return
        end

        local game = GameWorld.worldInfo.subGame
        if game == nil or game.waitingSrvResume then
            toast.gameObject:SetActiveIfNecessary(false)
            return
        end

        toast.gameObject:SetActiveIfNecessary(true)
        timer = timer + deltaTime
        if timer > animFrequency and tickTimes > endTimes then
            toast.animationWrapper:SampleToInAnimationEnd()
            toast.animationWrapper:PlayInAnimation()

            toast.startNumberTxt.text = math.ceil(tickTimes)
            AudioAdapter.PostEvent("Au_UI_Toast_TaskTrack_CountdownToast_Number")
        end

        if timer > animFrequency then
            timer = 0
            tickTimes = tickTimes - 1
        end

        if tickTimes < endTimes then
            self:Notify(MessageConst.ON_HUD_BTN_VISIBLE_CHANGE, {true})
            toast.animationWrapper:PlayOutAnimation(function()
                self:Close()

                if not string.isEmpty(GameWorld.worldInfo.curSubGameId) and cb ~= nil then
                    cb()
                end

                if endFunc then
                    endFunc()
                end
            end)
            self.m_countDownTickId = LuaUpdate:Remove(self.m_countDownTickId)
        end
    end)
end

CommonTaskTrackToastCtrl.ShowTaskStartToast = HL.Method(HL.Any, HL.Opt(HL.Function)) << function(self, args, endFunc)
    self:_RefreshToast(ToastType.Start, args, endFunc)
    self:_TryPostSubGameVo(args, CS.Beyond.Gameplay.Core.ESubGameVoType.Start)
    AudioAdapter.PostEvent("Au_UI_Toast_TaskTrack_TaskStartToast_Open")
end

CommonTaskTrackToastCtrl.ShowTaskFinishToast = HL.Method(HL.Any, HL.Opt(HL.Function)) << function(self, args, endFunc)
    self:_RefreshToast(ToastType.Finish, args, endFunc)
    self:_TryPostSubGameVo(args, CS.Beyond.Gameplay.Core.ESubGameVoType.Complete)
    AudioAdapter.PostEvent("Au_UI_Toast_TaskTrack_TaskFinishToast_Open")
end

CommonTaskTrackToastCtrl.ShowTaskFailToast = HL.Method(HL.Any, HL.Opt(HL.Function)) << function(self, args, endFunc)
    self:_RefreshToast(ToastType.Fail, args, endFunc)
    self:_TryPostSubGameVo(args, CS.Beyond.Gameplay.Core.ESubGameVoType.Fail)
    AudioAdapter.PostEvent("Au_UI_Toast_TaskTrack_TaskFailToast_Open")
end



CommonTaskTrackToastCtrl._TryPostSubGameVo = HL.Method(HL.Any, CS.Beyond.Gameplay.Core.ESubGameVoType)
        << function(self, args, voType)
    local id = unpack(args)
    if string.isEmpty(id) then
        return
    end
    local hasSubGame, subGame = GameWorld.subGameManager:TryGetSubGameById(id)
    if not hasSubGame then
        return
    end
    subGame:PostCategoryVo(voType)
end

CommonTaskTrackToastCtrl.ShowTaskStartToastWithoutId = HL.Method(HL.Any, HL.Opt(HL.Function)) << function(self, args, endFunc)
    local toastGOName, iconName, title, desc = unpack(args)
    self:_RefreshToastWithoutId(toastGOName, iconName, title, desc, endFunc)
    AudioAdapter.PostEvent("Au_UI_Toast_TaskTrack_TaskStartToast_Open")
end

CommonTaskTrackToastCtrl.ShowTaskFinishToastWithoutId = HL.Method(HL.Any, HL.Opt(HL.Function)) << function(self, args, endFunc)
    local iconName, title, desc = unpack(args)
    self:_RefreshToastWithoutId(ToastType.Finish, iconName, title, desc, endFunc)
    AudioAdapter.PostEvent("Au_UI_Toast_TaskTrack_TaskFinishToast_Open")
end

CommonTaskTrackToastCtrl.ShowTaskFailToastWithoutId = HL.Method(HL.Any, HL.Opt(HL.Function)) << function(self, args, endFunc)
    local iconName, title, desc = unpack(args)
    self:_RefreshToastWithoutId(ToastType.Fail, iconName, title, desc, endFunc)
    AudioAdapter.PostEvent("Au_UI_Toast_TaskTrack_TaskFailToast_Open")
end

CommonTaskTrackToastCtrl.OnSubGameReset = HL.Method() << function(self)
    self:Close()
end

CommonTaskTrackToastCtrl._RefreshToast = HL.Method(HL.String, HL.Any, HL.Opt(HL.Function))
        << function(self, toastType, args, endFunc)
    local instId, isNewRecord, passTime = unpack(args)
    local taskTitle = ""
    local taskDesc = ""
    local hasTableData, gameMechanicData = Tables.gameMechanicTable:TryGetValue(instId)
    local hasSubGameData, subGameData = DataManager.subGameInstDataTable:TryGetValue(instId)
    if not hasTableData then
        logger.error("未找到%s玩法配置数据,使用默认值", instId)
    end
    if not hasSubGameData then
        logger.error("未找到%s玩法实例数据,使用默认值", instId)
    end

    if toastType == ToastType.Start then
        taskTitle = hasTableData and gameMechanicData.gameName or ""
        taskDesc = hasTableData and gameMechanicData.desc or ""
        if hasSubGameData and not subGameData.showDesc then
            taskDesc = ""
        end
    elseif toastType == ToastType.Finish then
        if hasSubGameData then
            local success, successInfoText = subGameData.successInfo:TryGetText()
            taskTitle = success and successInfoText or Language.LUA_COMMON_TASK_TRACK_TOAST_SUCC_DESC
        else
            taskTitle = Language.LUA_COMMON_TASK_TRACK_TOAST_SUCC_DESC
        end
    elseif toastType == ToastType.Fail then
        if hasSubGameData then
            local success, failInfoText = subGameData.failInfo:TryGetText()
            taskTitle = success and failInfoText or Language.LUA_COMMON_TASK_TRACK_TOAST_FAIL_DESC
        else
            taskTitle = Language.LUA_COMMON_TASK_TRACK_TOAST_FAIL_DESC
        end
    end

    local hasGameCategory = false
    local gameCategoryCfg
    if hasTableData then
        hasGameCategory, gameCategoryCfg = Tables.gameMechanicCategoryTable:TryGetValue(gameMechanicData.gameCategory)
    end
    local toastGOName
    if toastType == ToastType.Start then
        local hasConfig = hasGameCategory and not string.isEmpty(gameCategoryCfg.startToastType)
        toastGOName = hasConfig and gameCategoryCfg.startToastType or WorldChallengeStartToast
    else
        toastGOName = toastType
    end
    local toastIconValid = (hasGameCategory and not string.isEmpty(gameCategoryCfg.toastIcon))
    local toastIcon = toastIconValid and gameCategoryCfg.toastIcon or DEFAULT_ICON


    if hasTableData and lume.find(showTimeRecordCategoryList, gameMechanicData.gameCategory) ~= nil and
            CommonTaskTrackToastCtrl.s_cachedCompleteResult then
        passTime = CommonTaskTrackToastCtrl.s_cachedCompleteResult.curGameTimeRecord
    end


    self:_RefreshToastWithoutId(toastGOName, toastIcon, taskTitle, taskDesc, endFunc, isNewRecord, passTime)
end

CommonTaskTrackToastCtrl._RefreshToastWithoutId = HL.Method(HL.String, HL.String, HL.String, HL.String, HL.Opt(HL.Function, HL.Boolean, HL.Number))
        << function(self, toastGOName, iconName, title, desc, endFunc, isNewRecord, passTime)
    self:Notify(MessageConst.ON_HUD_BTN_VISIBLE_CHANGE, {false})
    local toastGO = self:_CreateToastGO(toastGOName)
    local toastNode = Utils.wrapLuaNode(toastGO)
    toastNode.middleIcon:LoadSprite(UIConst.UI_SPRITE_COMMON_TASK_TRACK, iconName)
    toastNode.titleTxt.text = title
    toastNode.descTxt.text = desc or ""

    if toastNode.newRecordNode then
        toastNode.newRecordNode.gameObject:SetActiveIfNecessary(isNewRecord == true)
    end

    if toastNode.passTimeTxtNode then
        toastNode.passTimeTxtNode.gameObject:SetActiveIfNecessary(passTime ~= nil)
        toastNode.passTimeTxt.text = UIUtils.getLeftTimeToSecond(math.floor((passTime or 0) / 1000))
    end

    self.m_showingToastCor = self:_StartCoroutine(function()
        local inAnimLength = toastNode.animationWrapper:GetInClipLength()
        toastNode.animationWrapper:PlayInAnimation()
        coroutine.wait(inAnimLength)

        local outAnimLength = toastNode.animationWrapper:GetOutClipLength()
        toastNode.animationWrapper:PlayOutAnimation()
        coroutine.wait(outAnimLength)

        self:Close()
        self:Notify(MessageConst.ON_HUD_BTN_VISIBLE_CHANGE, {true})
        if endFunc then
            endFunc()
        end
    end)
end

CommonTaskTrackToastCtrl._CreateToastGO = HL.Method(HL.String).Return(GameObject) << function(self, name)
    local path = string.format(UIConst.UI_COMMON_TASK_TRACK_TOAST_WIDGETS_PATH, name)
    local goAsset = self:LoadGameObject(path)
    local go = CSUtils.CreateObject(goAsset, self.view.main)
    go.transform.anchoredPosition = Vector2.zero
    go.transform.localScale = Vector3.one
    go.transform.localRotation = Quaternion.identity
    go.name = name
    return go
end

CommonTaskTrackToastCtrl._CreateToastWidget = HL.Method(HL.String, HL.String).Return(HL.Any)
        << function(self, toastType, instId)


    local toastGOName = toastType
    local gameMechCfg = Tables.gameMechanicTable[instId]
    local gameCategoryCfg = Tables.gameMechanicCategoryTable[gameMechCfg.gameCategory]
    if toastType == ToastType.Start then
        toastGOName = gameCategoryCfg.startToastType
    end

    if string.isEmpty(toastGOName) then
        toastGOName = WorldChallengeStartToast
    end

    local toastGO = self:_CreateToastGO(toastGOName)
    return Utils.wrapLuaNode(toastGO)
end

HL.Commit(CommonTaskTrackToastCtrl)
