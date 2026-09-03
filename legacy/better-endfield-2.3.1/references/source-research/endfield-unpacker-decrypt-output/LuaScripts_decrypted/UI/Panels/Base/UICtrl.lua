UICtrl = HL.Class("UICtrl")



UICtrl.panelId = HL.Field(HL.Number) << -1

UICtrl.model = HL.Field(HL.Forward("UIModel"))

UICtrl.view = HL.Field(HL.Table)

UICtrl.panelCfg = HL.Field(HL.Table)

UICtrl.loader = HL.Field(HL.Forward("LuaResourceLoader"))

UICtrl.animationWrapper = HL.Field(CS.Beyond.UI.UIAnimationWrapper)

UICtrl.naviGroup = HL.Field(CS.Beyond.UI.UISelectableNaviGroup)

UICtrl.uiCamera = HL.Field(HL.Userdata)

UICtrl.planeDistance = HL.Field(HL.Number) << -1

UICtrl.m_phase = HL.Field(HL.Forward("PhaseBase"))

UICtrl.m_phaseItem = HL.Field(HL.Forward("PhasePanelItem"))

UICtrl.m_worldAutoRoot = HL.Field(HL.Userdata)

UICtrl.m_worldRoot = HL.Field(HL.Userdata)

UICtrl.m_outAnimAsyncActionHelper = HL.Field(HL.Forward("AsyncActionHelper"))

UICtrl.isControllerPanel = HL.Field(HL.Boolean) << false

UICtrl.isPCPanel = HL.Field(HL.Boolean) << false

UICtrl.isDefaultPanel = HL.Field(HL.Boolean) << false

UICtrl.isFinishedCreation = HL.Field(HL.Boolean) << false

UICtrl.m_updateKeys = HL.Field(HL.Table)

UICtrl.m_isClosed = HL.Field(HL.Boolean) << false

UICtrl.m_inputBindingIds = HL.Field(HL.Table)

UICtrl.m_inputGroupIds = HL.Field(HL.Table)





UICtrl.OnCreate = HL.Virtual(HL.Opt(HL.Any)) << function(self, arg)
end

UICtrl.OnPhaseRefresh = HL.Virtual(HL.Opt(HL.Any)) << function(self, arg)
end

UICtrl.OnCustomUIStyleRefChanged = HL.Virtual() << function(self)
end

UICtrl.GetCurPhaseStateArg = HL.Virtual().Return(HL.Opt(HL.Any)) << function(self)
end


UICtrl.OnClose = HL.Virtual() << function(self)
end

UICtrl.OnShow = HL.Virtual() << function(self)
end

UICtrl.OnHide = HL.Virtual() << function(self)
end

UICtrl.OnAnimationInFinished = HL.Virtual() << function(self)
end

UICtrl.ExtractHotSwitchRuntimeState = HL.Virtual().Return(HL.Opt(HL.Any)) << function(self)
    return nil
end

UICtrl.RestoreHotSwitchRuntimeState = HL.Virtual(HL.Opt(HL.Any)) << function(self, state)
end


UICtrl.Clear = HL.Method() << function(self)
    self.m_isClosed = true

    TimerManager:ClearAllTimer(self)
    CoroutineManager:ClearAllCoroutine(self)

    for k, _ in pairs(self.m_updateKeys) do
        LuaUpdate:Remove(k)
    end
    self.m_updateKeys = nil

    self:_DestroyAllWorldGameObject()

    if self.animationWrapper then
        self.animationWrapper:ClearTween()
    end
    if self.m_outAnimAsyncActionHelper then
        self.m_outAnimAsyncActionHelper:ForceClear()
    end
end



UICtrl.AutoOpen = HL.StaticMethod(HL.Number, HL.Opt(HL.Any, HL.Boolean, HL.Function)).Return(HL.Opt(HL.Forward("UICtrl"))) << function(panelId, arg, forceShow, onShowCallback)
    return UIManager:AutoOpen(panelId, arg, forceShow, onShowCallback)
end

UICtrl.Close = HL.Method() << function(self)
    UIManager:Close(self.panelId)
end

UICtrl.Show = HL.Method() << function(self)
    UIManager:Show(self.panelId)
end

UICtrl.Hide = HL.Method() << function(self)
    UIManager:Hide(self.panelId)
end

UICtrl.IsShow = HL.Method(HL.Opt(HL.Boolean)).Return(HL.Boolean) << function(self, ignoreClear)
    if self.m_isClosed then
        logger.error("面板已关闭，但是仍然持有 UICtrl 并在访问", self.panelCfg.name)
    end
    return UIManager:IsShow(self.panelId, ignoreClear)
end

UICtrl.IsHide = HL.Method().Return(HL.Boolean) << function(self)
    return UIManager:IsHide(self.panelId)
end


UICtrl.PlayAnimation = HL.Virtual(HL.String, HL.Opt(HL.Function)) << function(self, aniName, onComplete)
    local wrapper = self.animationWrapper
    if wrapper then
        wrapper:Play(aniName, onComplete)
    end
end

UICtrl.PlayAnimationIn = HL.Virtual() << function(self)
    local wrapper = self.animationWrapper
    if wrapper then
        wrapper:PlayInAnimation()
    end
end

UICtrl.PlayAnimationOut = HL.Virtual(HL.Opt(HL.Number)) << function(self, outCompleteActionType)
    outCompleteActionType = outCompleteActionType or UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Close
    self:PlayAnimationOutWithCallback(function()
        if outCompleteActionType == UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Close then
            self:Close()
        elseif outCompleteActionType == UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Hide then
            self:Hide()
        end
    end)
end

UICtrl.GetAnimationOutDuration = HL.Method().Return(HL.Number) << function(self)
    local wrapper = self.animationWrapper
    if wrapper then
        return wrapper:GetOutClipLength()
    end
    return 0
end

UICtrl.GetAnimationInDuration = HL.Method().Return(HL.Number) << function(self)
    local wrapper = self.animationWrapper
    if wrapper then
        return wrapper:GetInClipLength()
    end
    return 0
end

UICtrl.PlayAnimationOutAndClose = HL.Method() << function(self)
    self:PlayAnimationOut(UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Close)
end

UICtrl.PlayAnimationOutAndHide = HL.Method() << function(self)
    self:PlayAnimationOut(UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Hide)
end

UICtrl.IsPlayingAnimationOut = HL.Virtual().Return(HL.Boolean) << function(self)
    local wrapper = self.animationWrapper
    if wrapper then
        return wrapper.curState == CS.Beyond.UI.UIConst.AnimationState.Out
    end
    return false
end

UICtrl.IsPlayingAnimationIn = HL.Virtual().Return(HL.Boolean) << function(self)
    local wrapper = self.animationWrapper
    if wrapper then
        return wrapper.curState == CS.Beyond.UI.UIConst.AnimationState.In
    end
    return false
end

UICtrl.PlayAnimationOutWithCallback = HL.Virtual(HL.Opt(HL.Function)) << function(self, outCompleteAction)
    if self.m_isClosed then
        logger.critical("在已关闭面板上调用了 PlayAnimationOutWithCallback, PanelId:", self.panelCfg.name)
        if outCompleteAction then
            outCompleteAction()
        end
        return
    end

    if not self.m_outAnimAsyncActionHelper then
        self.m_outAnimAsyncActionHelper = require_ex("Common/Utils/AsyncActionHelper")(true)
    end

    if self.m_outAnimAsyncActionHelper:IsExecuting() then
        logger.critical("Call PlayAnimationOutWithCallback When Executing, PanelId:", self.panelCfg.name)
        if outCompleteAction then
            outCompleteAction()
        end
        return
    end

    self.m_outAnimAsyncActionHelper:Clear()



    if self.m_needPlayOutAnimWidgets then
        for widget, _ in pairs(self.m_needPlayOutAnimWidgets) do
            widget:PlayAnimationOut()
        end
    end

    if UICtrl.s_useBlackOutPanelIds[self.panelId] then

        UICtrl.TogglePanelUseBlackOut(self.panelId, false)

        self.m_outAnimAsyncActionHelper:AddAction(function(onComplete)
            if self.animationWrapper then
                self.animationWrapper.curState = CS.Beyond.UI.UIConst.AnimationState.Out
            end
            Notify(MessageConst.START_COMMON_BLACK_OUT, {
                fadeInTime = UIConst.INPUT_DEVICE_CHANGE_MASK_TIME,
                onFadeInComplete = function()
                    if self.animationWrapper then
                        self.animationWrapper.curState = CS.Beyond.UI.UIConst.AnimationState.Stop
                    end
                    onComplete()
                end,
            })
        end)
    else
        self.m_outAnimAsyncActionHelper:AddAction(function(onComplete)
            local wrapper = self.animationWrapper
            if wrapper then
                wrapper:PlayOutAnimation(function()
                    onComplete()
                end)
            else
                onComplete()
            end
        end)
    end

    self.view.luaPanel:BlockAllInput()
    self.m_outAnimAsyncActionHelper:SetOnFinished(function()
        self.view.luaPanel:RecoverAllInput()
        if outCompleteAction then
            outCompleteAction()
        end
        if self.panelCfg.hideCamera then


            UIManager:_UpdateMainCamera()
        end
    end)




    self:_OnPlayAnimationOut()
    self.m_outAnimAsyncActionHelper:Start()
    if self.panelCfg.hideCamera then


        UIManager:_UpdateMainCamera()
    end

end

UICtrl._OnPlayAnimationOut = HL.Virtual() << function(self)
end


UICtrl.m_needPlayOutAnimWidgets = HL.Field(HL.Table)
UICtrl.RegisterPlayOutAnimWidget = HL.Virtual(HL.Forward('UIWidgetBase')) << function(self, widget)
    if not self.m_needPlayOutAnimWidgets then
        self.m_needPlayOutAnimWidgets = {}
    end
    self.m_needPlayOutAnimWidgets[widget] = true
end




UICtrl._StartTimer = HL.Method(HL.Number, HL.Function, HL.Opt(HL.Boolean)).Return(HL.Number)
        << function(self, duration, func, unscaled)
    if self.m_isClosed then
        logger.error("面板已关闭，不能调用 _StartTimer", self.panelCfg.name)
        return -1
    end
    return TimerManager:StartTimer(duration, func, unscaled, self)
end

UICtrl._ClearTimer = HL.Method(HL.Number).Return(HL.Number) << function(self, timer)
    TimerManager:ClearTimer(timer)
    return -1
end

UICtrl._StartCoroutine = HL.Method(HL.Function).Return(HL.Opt(HL.Thread)) << function(self, func)
    if self.m_isClosed then
        logger.error("面板已关闭，不能调用 _StartCoroutine", self.panelCfg.name)
        return nil
    end
    return CoroutineManager:StartCoroutine(func, self)
end

UICtrl._ClearCoroutine = HL.Method(HL.Thread).Return(HL.Any) << function(self, coroutine)
    CoroutineManager:ClearCoroutine(coroutine)
    return nil
end


UICtrl._StartUpdate = HL.Method(HL.Function, HL.Opt(HL.String)).Return(HL.Number) << function(self, func, updateName)
    if self.m_isClosed then
        logger.error("面板已关闭，不能调用 _StartUpdate", self.panelCfg.name)
        return -1
    end
    updateName = updateName or "Tick"
    local key = LuaUpdate:Add(updateName, func)
    self.m_updateKeys[key] = true
    return key
end

UICtrl._RemoveUpdate = HL.Method(HL.Number).Return(HL.Number) << function(self, key)
    LuaUpdate:Remove(key)
    self.m_updateKeys[key] = nil
    return -1
end





UICtrl.Notify = HL.Method(HL.Number, HL.Opt(HL.Any)) << function(self, msg, arg)
    Notify(msg, arg)
end








UICtrl.SetSortingOrder = HL.Method(HL.Number, HL.Boolean) << function(self, order, isInit)


    self.view.panelCanvas.sortingOrder = order



    self:OnSortingOrderChange(order, isInit)
end

UICtrl.OnSortingOrderChange = HL.Virtual(HL.Number, HL.Boolean) << function(self, order, isInit)
    local luaPanel = self.view.luaPanel
    local sortingOrderComps = luaPanel.sortingOrderComps
    if sortingOrderComps then
        for v in cs_pairs(sortingOrderComps) do
            local comp = v
            comp:SetOrder(order)
        end
    end
end

UICtrl.ToTop = HL.Method() << function(self)
    UIManager:SetTopOrder(self.panelId)
end

UICtrl.GetSortingOrder = HL.Method().Return(HL.Number) << function(self)
    return self.view.panelCanvas.sortingOrder
end




UICtrl.BindInputPlayerAction = HL.Method(HL.String, HL.Function, HL.Opt(HL.Number)).Return(HL.Number) << function(self, actionId, callback, groupId)
    groupId = groupId or self.view.inputGroup.groupId
    local bindingId = UIUtils.bindInputPlayerAction(actionId, callback, groupId)
    if bindingId and bindingId > 0 then
        if not self.m_inputBindingIds then
            self.m_inputBindingIds = {}
        end
        self.m_inputBindingIds[bindingId] = true
    end
    return bindingId
end


UICtrl.CreateInputGroup = HL.Method(HL.Opt(HL.Number)).Return(HL.Number) << function(self, parentGroupId)
    parentGroupId = parentGroupId or self.view.inputGroup.groupId
    local groupId = InputManagerInst:CreateGroup(parentGroupId)
    if groupId and groupId > 0 then
        if not self.m_inputGroupIds then
            self.m_inputGroupIds = {}
        end
        self.m_inputGroupIds[groupId] = true
    end
    return groupId
end


UICtrl._CleanupInputForHotSwitch = HL.Method() << function(self)

    if self.m_inputGroupIds then
        for groupId, _ in pairs(self.m_inputGroupIds) do
            InputManagerInst:DeleteGroup(groupId)
        end
        self.m_inputGroupIds = nil
    end
    if self.m_inputBindingIds then
        for bindingId, _ in pairs(self.m_inputBindingIds) do
            InputManagerInst:DeleteBinding(bindingId)
        end
        self.m_inputBindingIds = nil
    end
end





UICtrl.BindInputEvent = HL.Method(HL.Userdata, HL.Function, HL.Opt(HL.String, HL.Any)).Return(HL.Number)
        << function(self, key, action, modifyKeys, timing)
    local groupId = self.view.inputGroup.groupId
    local bindingId = UIUtils.bindInputEvent(key, action, modifyKeys, timing, groupId)
    if bindingId and bindingId > 0 then
        if not self.m_inputBindingIds then
            self.m_inputBindingIds = {}
        end
        self.m_inputBindingIds[bindingId] = true
    end
    return bindingId
end

UICtrl.UpdateInputGroupState = HL.Method().Return(HL.Boolean) << function(self)
    local active = false
    if self:IsShow() then


        if self:GetSortingOrder() >= UIManager:CurBlockKeyboardEventPanelOrder() then

            active = true
        end
    end

    local inputGroup = self.view.inputGroup

    if inputGroup.internalEnabled == active then
        return active
    end

    inputGroup.internalEnabled = active
    if self.isFinishedCreation then

        self:_OnPanelInputBlocked(active)
    end


    return active
end

UICtrl._OnPanelInputBlocked = HL.Virtual(HL.Boolean) << function(self, active)
end

UICtrl.DeleteInputBinding = HL.Method(HL.Number).Return(HL.Number) << function(self, keyId)
    if keyId then
        InputManagerInst:DeleteBinding(keyId)
    end
    return -1
end



UICtrl.GetCurPanelCfg = HL.Method(HL.String).Return(HL.Any) << function(self, name)
    if not self.view then
        return self.panelCfg[name]
    end

    local curValue = self.view.curPanelCfg[name]
    if curValue ~= nil then
        return curValue
    else
        return self.panelCfg[name]
    end
end

UICtrl.ChangeCurPanelBlockSetting = HL.Method(HL.Opt(HL.Boolean, HL.Number))
        << function(self, blockKeyboardEvent, multiTouchType)
    if not self.view then
        return
    end

    if blockKeyboardEvent ~= nil then
        self.view.curPanelCfg.blockKeyboardEvent = blockKeyboardEvent
    end
    if multiTouchType ~= nil then
        self.view.curPanelCfg.multiTouchType = multiTouchType
    end
    UIManager:CalcOtherSystemPropertyByPanelOrder()
end

UICtrl.ChangePanelCfg = HL.Method(HL.String, HL.Any) << function(self, key, value)
    self.view.curPanelCfg[key] = value
    UIManager:CalcOtherSystemPropertyByPanelOrder()
end

UICtrl.GetBlockKeyboardEvent = HL.Method().Return(HL.Boolean) << function(self)
    return self:GetCurPanelCfg("blockKeyboardEvent")
end

UICtrl.GetMultiTouchType = HL.Method().Return(HL.Number) << function(self)
    return self:GetCurPanelCfg("multiTouchType")
end



UICtrl.SetPhaseItem = HL.Method(HL.Forward("PhasePanelItem")) << function(self, phaseItem)
    if phaseItem then
        self.m_phaseItem = phaseItem
        self.m_phase = phaseItem.phase
    else
        self.m_phaseItem = nil
        self.m_phase = nil
    end
end




UICtrl.LoadSprite = HL.Method(HL.String, HL.Opt(HL.String)).Return(Unity.Sprite) << function(self, path, name)
    return UIUtils.loadSprite(self.loader, path, name)
end

UICtrl.LoadGameObject = HL.Method(HL.String).Return(CS.UnityEngine.GameObject) << function(self, path)
    local obj = self.loader:LoadGameObject(path)
    return obj
end

UICtrl.LoadGameObjectAsync = HL.Method(HL.String, HL.Function).Return(HL.Number) << function(self, path, callback)
    local key = self.loader:LoadGameObjectAsync(path, callback)
    return key
end



UICtrl._CreateWorldGameObject = HL.Method(GameObject, HL.Opt(HL.Boolean)).Return(GameObject) << function(self, prefab, notAuto)
    if notAuto and not self.m_worldRoot then
        self:_CreateWorldObjectRoot(false)
    elseif not notAuto and not self.m_worldAutoRoot then
        self:_CreateWorldObjectRoot(true)
    end
    local obj
    if notAuto then
        obj = CSUtils.CreateObject(prefab, self.m_worldRoot)
    else
        obj = CSUtils.CreateObject(prefab, self.m_worldAutoRoot)
    end
    return obj
end

UICtrl._CreateEmptyWorldGameObject = HL.Method(HL.String, HL.Opt(HL.Boolean)).Return(GameObject) << function(self, name, notAuto)
    if notAuto and not self.m_worldRoot then
        self:_CreateWorldObjectRoot(false)
    elseif not notAuto and not self.m_worldAutoRoot then
        self:_CreateWorldObjectRoot(true)
    end
    local obj = GameObject(name)
    if notAuto then
        obj.transform:SetParent(self.m_worldRoot)
    else
        obj.transform:SetParent(self.m_worldAutoRoot)
    end
    return obj
end

UICtrl._CreateWorldObjectRoot = HL.Method(HL.Boolean) << function(self, isAuto)
    if isAuto then
        if not self.m_worldAutoRoot then
            self.m_worldAutoRoot = GameObject(self.panelCfg.name .. "AutoRoot").transform
            self.m_worldAutoRoot:SetParent(UIManager.worldObjectRoot)
        end
        if self:IsHide() then
            self:SetGameObjectVisible(false)
        end
    else
        if not self.m_worldRoot then
            self.m_worldRoot = GameObject(self.panelCfg.name .. "Root").transform
            self.m_worldRoot:SetParent(UIManager.worldObjectRoot)
        end
    end
end

UICtrl.SetGameObjectVisible = HL.Method(HL.Boolean) << function(self, visible)
    if self.m_worldAutoRoot then
        self.m_worldAutoRoot.gameObject:SetActive(visible)
    end
end

UICtrl._DestroyAllWorldGameObject = HL.Method() << function(self)
    if self.m_worldRoot then
        GameObject.Destroy(self.m_worldRoot.gameObject)
    end
    if self.m_worldAutoRoot then
        GameObject.Destroy(self.m_worldAutoRoot.gameObject)
    end

    self.m_worldRoot = nil
    self.m_worldAutoRoot = nil
end





UICtrl.s_useBlackOutPanelIds = HL.StaticField(HL.Table)

UICtrl.TogglePanelUseBlackOut = HL.StaticMethod(HL.Number, HL.Boolean) << function(panelId, useBlackOut)

    logger.info("UICtrl.TogglePanelUseBlackOut", panelId, useBlackOut)
    if useBlackOut then
        UICtrl.s_useBlackOutPanelIds[panelId] = true
    else
        UICtrl.s_useBlackOutPanelIds[panelId] = nil
    end
end






UICtrl._GetDummyNaviLayerKey = HL.Method().Return(HL.String) << function(self)
    if self.panelCfg.needNaviDummyLayer then
        return self.panelCfg.name
    end
    local phase = self.m_phase
    if phase ~= nil then
        if not phase.cfg.notCreateDummyNaviLayer then
            local key = PhaseManager:GetPhaseDummyNaviLayerName(phase.phaseId)
            if string.isEmpty(key) then
                logger.error("[UICtrl] _GetDummyNaviLayerKey", self.panelCfg.name, "无法找到当前面板所属Phase的DummyNaviLayer")
            end
            return key
        end
    end
    return ""
end

UICtrl.SetNaviTarget = HL.Method(HL.Userdata) << function(self, selectable)
    local needTopDummyLayerKey = self:_GetDummyNaviLayerKey()
    if string.isEmpty(needTopDummyLayerKey) then
        InputManagerInst.controllerNaviManager:SetTargetInSilentModeIfNecessary(selectable)
        return
    end
    local needTopDummyLayer = LuaSystemManager.dummyNaviLayerSystem:GetDummyNaviLayerByKey(needTopDummyLayerKey)
    if needTopDummyLayer ~= nil then
        InputManagerInst.controllerNaviManager:SetTargetInSilentModeIfNecessary(selectable, needTopDummyLayer)
    end
end

UICtrl.ClearNaviTarget = HL.Method() << function(self)
    local needTopDummyLayerKey = self:_GetDummyNaviLayerKey()
    if string.isEmpty(needTopDummyLayerKey) then
        InputManagerInst.controllerNaviManager:ClearTargetIfNecessary()
        return
    end
    local needTopDummyLayer = LuaSystemManager.dummyNaviLayerSystem:GetDummyNaviLayerByKey(needTopDummyLayerKey)
    if needTopDummyLayer ~= nil then
        InputManagerInst.controllerNaviManager:ClearTargetIfNecessary(needTopDummyLayer)
    end
end


UICtrl.SetAsNaviTargetInSilentModeIfNecessary = HL.Method(HL.Userdata, HL.Userdata, HL.Opt(HL.String)) << function(self, targetNaviGroup, targetSelectable, needTopDummyLayerKey)
    logger.error("[UICtrl] SetAsNaviTargetInSilentModeIfNecessary 已废弃，请使用 self:SetNaviTarget(selectable)", self.panelCfg.name)
    self:SetNaviTarget(targetSelectable)
end




HL.Commit(UICtrl)
