


local stateBehaviour = require_ex('Phase/Core/StateBehaviour')
local luaLoader = require_ex('Common/Utils/LuaResourceLoader')

PhaseBase = HL.Class("PhaseBase", stateBehaviour.StateBehaviour)




PhaseBase.phaseId = HL.Field(HL.Number) << -1

PhaseBase.cfg = HL.Field(HL.Table)

PhaseBase.systemId = HL.Field(HL.String) << ''

PhaseBase.unlockSystemType = HL.Field(GEnums.UnlockSystemType)

PhaseBase.redDotName = HL.Field(HL.String) << ''


PhaseBase.m_phaseItems = HL.Field(HL.Table)

PhaseBase.m_panel2Item = HL.Field(HL.Table)

PhaseBase.m_gameObject2Item = HL.Field(HL.Table)

PhaseBase.m_charInstId2LoadRequest = HL.Field(HL.Table)

PhaseBase.m_charInstId2LoadCoroutine = HL.Field(HL.Table)

PhaseBase.m_charInstId2Item = HL.Field(HL.Table)




PhaseBase.m_inTransition = HL.Field(HL.Boolean) << false

PhaseBase.m_resourceLoader = HL.Field(HL.Forward("LuaResourceLoader"))

PhaseBase.panels = HL.Field(HL.Table)

PhaseBase.modelLoader = HL.Field(CS.Beyond.UI.UIModelLoader)

PhaseBase.m_destroyed = HL.Field(HL.Boolean) << false






PhaseBase._OnInit = HL.Override() << function(self)
    PhaseBase.Super._OnInit(self)
    self.m_phaseItems = {}
    self.panels = {}
    self.m_panel2Item = {}
    self.m_gameObject2Item = {}
    self.m_charInstId2LoadRequest = {}
    self.m_charInstId2LoadCoroutine = {}
    self.m_charInstId2Item = {}
    self.cfg = {}
    self.m_resourceLoader = luaLoader.LuaResourceLoader()
    self.m_destroyed = false
end

PhaseBase.InitWithCfg = HL.Method(HL.Table) << function(self, cfg)
    self.cfg = cfg
    if string.isEmpty(cfg.systemId) then
        self.unlockSystemType = cfg.unlockSystemType and cfg.unlockSystemType or GEnums.UnlockSystemType.None
        if cfg.redDotName then
            self.redDotName = cfg.redDotName
        end
    else
        self.systemId = cfg.systemId
        local sysData = Tables.gameSystemConfigTable[self.systemId]
        self.unlockSystemType = sysData.unlockSystemType
        self.redDotName = sysData.redDotName
    end
    self.modelLoader = CS.Beyond.UI.UIModelLoader()
end






PhaseBase._OnActivated = HL.Override() << function(self)
end

PhaseBase._OnDeActivated = HL.Override() << function(self)
end

PhaseBase._OnDestroy = HL.Override() << function(self)
end

PhaseBase._InnerDestroy = HL.Override() << function(self)
    if self.m_destroyed then
        logger.critical("PhaseBase._InnerDestroy 失败，Phase 已经销毁了", self.cfg.name)
        return
    end

    for phaseItem, _ in pairs(self.m_phaseItems) do
        phaseItem:Destroy()
    end

    self:RemoveAllPhaseCharItems()

    self.m_resourceLoader:DisposeAllHandles()
    self.m_phaseItems = nil
    self.m_panel2Item = nil
    self.m_gameObject2Item = nil
    self.modelLoader:Dispose()

    self.m_destroyed = true
end

PhaseBase._OnRefresh = HL.Virtual() << function(self)
    if self.m_destroyed then
        logger.critical("PhaseBase._OnRefresh 失败，Phase 已经销毁了", self.cfg.name)
        return
    end
    for phaseItem, _ in pairs(self.m_phaseItems) do
        phaseItem:OnPhaseRefresh(self.arg)
    end
end

PhaseBase.PrepareTransition = HL.Virtual(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseBase._DoTransitionInCoroutine = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if self.m_destroyed then
        logger.critical("PhaseBase._DoTransitionInCoroutine 失败，Phase 已经销毁了", self.cfg.name)
        return
    end

    self:_InitAllPhaseItems()
    self:_DoPhaseTransitionIn(fastMode, args)
    for phaseItem, _ in pairs(self.m_phaseItems) do
        phaseItem:TransitionIn(fastMode)
    end
end

PhaseBase._DoPhaseTransitionIn = HL.Virtual(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end


PhaseBase._DoTransitionOutCoroutine = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if self.m_destroyed then
        logger.critical("PhaseBase._DoTransitionOutCoroutine 失败，Phase 已经销毁了", self.cfg.name)
        return
    end

    self:_DoPhaseTransitionOut(fastMode, args)
    for phaseItem, _ in pairs(self.m_phaseItems) do
        phaseItem:TransitionOut(fastMode)
    end
    self:_DoPhaseTransitionOutAfterItems(fastMode, args)
end

PhaseBase._DoPhaseTransitionOut = HL.Virtual(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseBase._DoPhaseTransitionOutAfterItems = HL.Virtual(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseBase._DoTransitionBehindCoroutine = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if self.m_destroyed then
        logger.critical("PhaseBase._DoTransitionBehindCoroutine 失败，Phase 已经销毁了", self.cfg.name)
        return
    end

    self:_DoPhaseTransitionBehind(fastMode, args)
    for phaseItem, _ in pairs(self.m_phaseItems) do
        phaseItem:TransitionBehind(fastMode)
    end
end

PhaseBase._DoPhaseTransitionBehind = HL.Virtual(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end


PhaseBase._DoTransitionBackToTopCoroutine = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if self.m_destroyed then
        logger.critical("PhaseBase._DoTransitionBackToTopCoroutine 失败，Phase 已经销毁了", self.cfg.name)
        return
    end

    self:_DoPhaseTransitionBackToTop(fastMode, args)
    for phaseItem, _ in pairs(self.m_phaseItems) do
        phaseItem:TransitionBackToTop(fastMode)
    end
end

PhaseBase._DoPhaseTransitionBackToTop = HL.Virtual(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseBase._CheckAllTransitionDone = HL.Override().Return(HL.Boolean) << function(self)
    if self.m_destroyed then
        logger.critical("PhaseBase._CheckAllTransitionDone 失败，Phase 已经销毁了", self.cfg.name)
        return true
    end


    if self.m_inTransition then
        return false
    end

    local checkTypes = self:_GetTransitionCheckTypes()
    for phaseItem, _ in pairs(self.m_phaseItems) do
        if checkTypes[phaseItem.state] then
            return false
        end
    end
    return true
end

PhaseBase._GetTransitionCheckTypes = HL.Virtual().Return(HL.Table) << function(self)
    return {
        [PhaseConst.EPhaseState.TransitionIn] = true,
        [PhaseConst.EPhaseState.TransitionBehind] = true,
        [PhaseConst.EPhaseState.TransitionOut] = true,
        [PhaseConst.EPhaseState.TransitionBackToTop] = true,
    }
end

PhaseBase.GetCurStateArg = HL.Virtual().Return(HL.Opt(HL.Any)) << function(self)
    if self.cfg.isSimpleUIPhase then
        for _, item in pairs(self.m_panel2Item) do
            local overrideArg = item.uiCtrl:GetCurPhaseStateArg()
            if overrideArg ~= nil then
                return lume.deepCopy(overrideArg)
            end
        end
    end
    if self.arg ~= nil then
        return lume.deepCopy(self.arg)
    end
end







PhaseBase.CloseSelf = HL.Virtual() << function(self)
    if self.state == PhaseConst.EPhaseState.WaitRelease then
        return
    end

    if self.state == PhaseConst.EPhaseState.TransitionOut then
        return
    end

    if PhaseManager.curPhase ~= self then
        self:ExitSelfFast()
    else
        PhaseManager:PopPhase(self.phaseId)
    end

end

PhaseBase.ExitSelfFast = HL.Virtual() << function(self)
    PhaseManager:ExitPhaseFast(self.phaseId)
end

PhaseBase.LoadSprite = HL.Method(HL.String, HL.Opt(HL.String)).Return(Unity.Sprite) << function(self, path, name)
    local sprite = self.m_resourceLoader:LoadSprite(UIUtils.getSpritePath(path, name))
    if sprite == nil then
        logger.error("LoadSprite failed", path, name)
    end
    return sprite
end

PhaseBase.AutoOpen = HL.StaticMethod(HL.Number, HL.Table) << function(phaseId, arg)
    if arg.fast then
        PhaseManager:OpenPhaseFast(phaseId, arg)
    else
        PhaseManager:OpenPhase(phaseId, arg, nil)
    end
end

PhaseBase.Refresh = HL.Method(HL.Any) << function(self, arg)
    if self.state ~= PhaseConst.EPhaseState.Activated then
        return
    end

    self.arg = arg
    self:_OnRefresh()
end






PhaseBase._InitAllPhaseItems = HL.Virtual() << function(self)


    if self.cfg.panels then
        for _, panelId in pairs(self.cfg.panels) do
            self:CreatePhasePanelItem(panelId, self.arg)
        end
    end

    if self.cfg.gameObjects then
        for _, name in pairs(self.cfg.gameObjects) do
            self:CreatePhaseGOItem(name, nil, self.arg)
        end
    end
end

PhaseBase._GetPanelPhaseItem = HL.Method(HL.Number).Return(HL.Forward("PhasePanelItem")) << function(self, panelId)
    return self.m_panel2Item[panelId]
end

PhaseBase._GetGOPhaseItem = HL.Method(HL.String).Return(HL.Forward("PhaseGameObjectItem")) << function(self, name)
    return self.m_gameObject2Item[name]
end

PhaseBase._GetCharPhaseItem = HL.Method(HL.Number, HL.Opt(HL.Number)).Return(HL.Forward("PhaseCharItem")) << function(self, charInstId, index)
    local items = self.m_charInstId2Item[charInstId]
    local item = nil
    index = index or 1
    if items then
        item = items[index]
    end
    return item
end






PhaseBase.CreatePhaseCharItem = HL.Method(HL.Table, HL.Any, HL.Function, HL.Opt(HL.Boolean, HL.Number)) << function(
    self, data, parent, callback, prepareEffect, layer)
    local charId = data.charId
    local charInstId = data.charInstId ~= nil and data.charInstId or 0
    local modelPath = string.format(PhaseConst.PHASE_CHAR_MODEL_FILE_PATH, charId)

    self.m_charInstId2LoadCoroutine[charInstId] = self:_StartCoroutine(function()
        local modelGo
        coroutine.waitAsyncRequest(function(onComplete)
            self.m_charInstId2LoadRequest[charInstId] = self.modelLoader:LoadModelAsync(modelPath, parent, function(go)
                self.m_charInstId2LoadRequest[charInstId] = nil
                modelGo = go
                onComplete()
            end)
        end)

        if not modelGo then
            if callback then
                callback(nil)
            end
            return
        end

        modelGo.transform.localPosition = Vector3.zero
        local phaseItem = require_ex("Phase/PhaseItem/PhaseCharItem").PhaseCharItem(data)
        phaseItem:BindBasicInfos(self, self.phaseId)
        phaseItem:BindGameObject(modelGo)
        if data.charInstId then
            if not self.m_charInstId2Item[data.charInstId] then
                self.m_charInstId2Item[data.charInstId] = {}
            end
            table.insert(self.m_charInstId2Item[data.charInstId], phaseItem)
        end
        local modelMono = modelGo:GetComponent("CharUIModelMono")
        if NotNull(modelMono) then
            if layer then
                modelMono:ChangeLayer(layer)
            end
            modelMono:LoadDecoItems()
        end
        self.m_phaseItems[phaseItem] = true


        if prepareEffect then
            local prepareEffectCompleted = false
            modelMono:PrepareEffects(function()
                prepareEffectCompleted = true
            end)
            phaseItem:SetVisible(false)
            while not prepareEffectCompleted do
                coroutine.step()
            end
            phaseItem:SetVisible(true)
        end

        if data.charInstId then
            local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(data.charInstId)
            if charInfo and charInfo.potentialLevel >= UIConst.CHAR_MAX_POTENTIAL and
                GameInstance.player.charBag:IsCharMaxPotentialEffectEnabled(charId) then
                phaseItem:LoadPotentialEffects()
            end
        end

        if callback then
            callback(phaseItem)
        end
    end)
end

PhaseBase.CreatePhaseGOItem = HL.Method(HL.String, HL.Opt(HL.Userdata, HL.Any, HL.String)).Return(HL.Forward("PhaseGameObjectItem")) << function(self, name, parent, arg, folderName)
    local goObj = PhaseManager:GetCachedGameObject(self.phaseId, name)
    local cacheName = ""
    if not goObj then
        folderName = folderName or self.cfg.name
        local goAsset = self.m_resourceLoader:LoadGameObject(string.format(PhaseConst.PHASE_GAME_OBJECT_FILE_PATH, folderName, name))
        goObj = CSUtils.CreateObject(goAsset, parent)
    else
        goObj:SetActive(true)
        cacheName = name
    end

    local phaseItem = require_ex("Phase/PhaseItem/PhaseGameObjectItem").PhaseGameObjectItem(arg)
    phaseItem:BindBasicInfos(self, self.phaseId)
    phaseItem:BindGameObject(goObj)
    phaseItem.cacheName = cacheName
    self.m_phaseItems[phaseItem] = true
    self.m_gameObject2Item[name] = phaseItem
    return phaseItem
end

PhaseBase.CreatePhasePanelItem = HL.Method(HL.Number, HL.Opt(HL.Any)).Return(HL.Forward("PhasePanelItem")) << function(self, panelId, arg)
    if self.m_panel2Item[panelId] then
        logger.critical("尝试重复 CreatePhasePanelItem", UIManager.m_names[panelId])
        return self.m_panel2Item[panelId]
    end
    local phaseItem = require_ex("Phase/PhaseItem/PhasePanelItem").PhasePanelItem(arg)
    phaseItem:BindBasicInfos(self, self.phaseId)
    UIManager:AutoOpenWithPhaseItem(panelId, phaseItem, arg)
    phaseItem.hideOnDestroy = self.cfg.hideOnDestroy == true
    self.m_phaseItems[phaseItem] = true
    self.m_panel2Item[panelId] = phaseItem
    table.insert(self.panels, panelId)
    return phaseItem
end

PhaseBase.CreateOrShowPhasePanelItem = HL.Method(HL.Number, HL.Opt(HL.Any)).Return(HL.Forward("PhasePanelItem")) << function(self, panelId, arg)
    local phaseItem = self:_GetPanelPhaseItem(panelId)
    if phaseItem then
        UIManager:Show(panelId)
        return phaseItem
    else
        return self:CreatePhasePanelItem(panelId, arg)
    end
end

PhaseBase.RemovePhasePanelItem = HL.Method(HL.Forward("PhasePanelItem"), HL.Opt(HL.Any)) << function(self, phasePanelItem, arg)
    if phasePanelItem then
        local panelId = phasePanelItem.uiCtrl.panelId
        phasePanelItem:Destroy()
        self.m_phaseItems[phasePanelItem] = nil
        self.m_panel2Item[panelId] = nil

        local index = nil
        for k, v in pairs(self.panels) do
            if v == panelId then
                index = k
                break
            end
        end
        if index then
            table.remove(self.panels, index)
        end
    end

end

PhaseBase.RemovePhasePanelItemById = HL.Method(HL.Number, HL.Opt(HL.Any)) << function(self, panelId, arg)
    local phaseItem = self:_GetPanelPhaseItem(panelId)
    if phaseItem then
        self:RemovePhasePanelItem(phaseItem, arg)
    end
end

PhaseBase.RemovePhaseCharItemByInstId = HL.Method(HL.Number, HL.Opt(HL.Number)) << function(self, charInstId, index)
    local phaseItems = self.m_charInstId2Item[charInstId]
    local requestId = self.m_charInstId2LoadRequest[charInstId]
    if requestId then
        self.modelLoader:Cancel(requestId)
        self.m_charInstId2LoadRequest[charInstId] = nil
    end
    local loadCoroutine = self.m_charInstId2LoadCoroutine[charInstId]
    if loadCoroutine then
        self:_ClearCoroutine(loadCoroutine)
        self.m_charInstId2LoadCoroutine[charInstId] = nil
    end
    if phaseItems then
        if index == -1 then

            index = #phaseItems
        elseif not index then

            index = 1
        end
        if phaseItems and phaseItems[index] then
            local item = phaseItems[index]
            item:Destroy()
            self.m_phaseItems[item] = nil
            self.m_charInstId2Item[charInstId][index] = nil
        end
    end
end

PhaseBase.RemoveAllPhaseCharItems = HL.Method() << function(self)
    for _, phaseItems in pairs(self.m_charInstId2Item) do
        if phaseItems then
            for _, item in pairs(phaseItems) do
                item:Destroy()
                self.m_phaseItems[item] = nil
            end
        end
    end
    for _, requestId in pairs(self.m_charInstId2LoadRequest) do
        if requestId then
            self.modelLoader:Cancel(requestId)
        end
    end
    for _, loadCoroutine in pairs(self.m_charInstId2LoadCoroutine) do
        if loadCoroutine then
            self:_ClearCoroutine(loadCoroutine)
        end
    end
    self.m_charInstId2LoadCoroutine = {}
    self.m_charInstId2LoadRequest = {}
    self.m_charInstId2Item = {}
end



HL.Commit(PhaseBase)
