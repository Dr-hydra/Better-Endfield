local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FriendThemeChange
local PHASE_ID = PhaseId.FriendThemeChange

FriendThemeChangeCtrl = HL.Class('FriendThemeChangeCtrl', uiCtrl.UICtrl)






FriendThemeChangeCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_BUSINESS_CARD_UNLOCK] = 'OnBusinessCardUnlock',

    [MessageConst.ON_SYSTEM_UNLOCK_CHANGED] = 'OnSystemUnlockChanged',
    [MessageConst.ON_ALL_SYSTEM_UNLOCK_SYNC] = 'OnSystemUnlockChanged',

    [MessageConst.ON_ACTIVITY_UPDATED] = 'OnActivityUpdated',
}

FriendThemeChangeCtrl.m_getCellFunc = HL.Field(HL.Function)

FriendThemeChangeCtrl.m_panel = HL.Field(HL.Userdata)

FriendThemeChangeCtrl.m_businessCard = HL.Field(HL.Forward('FriendBusinessCard'))

FriendThemeChangeCtrl.m_infos = HL.Field(HL.Table)

FriendThemeChangeCtrl.m_selectId = HL.Field(HL.String) << ""

FriendThemeChangeCtrl.m_inCreate = HL.Field(HL.Boolean) << false

FriendThemeChangeCtrl.m_arg = HL.Field(HL.Table)

FriendThemeChangeCtrl.m_closeCallback = HL.Field(HL.Function)


FriendThemeChangeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg or {}
    self.m_inCreate = true
    self.m_closeCallback = arg and arg.onClose or nil
    self.view.btnBack.onClick:RemoveAllListeners()
    self.view.btnBack.onClick:AddListener(function()
        self:_Close()
    end)

    self.view.confirmBtn.onClick:RemoveAllListeners()
    self.view.confirmBtn.onClick:AddListener(function()
        GameInstance.player.friendSystem:BusinessCardTopicModify(self.m_selectId)
        self:_Close()
    end)

    self.view.cancelBtn.onClick:RemoveAllListeners()
    self.view.cancelBtn.onClick:AddListener(function()
        self:_Close()
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    self.m_selectId = self.m_arg.selectId or GameInstance.player.friendSystem.SelfInfo.businessCardTopicId
    GameInstance.player.friendSystem:ReadBusinessCardUnlockRedDot(CS.Beyond.Gameplay.FriendBusinessCardUnlockType.BusinessCardTopic, self.m_selectId)
    self:_RebuildInfos()

    self.m_getCellFunc = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.themeList.onUpdateCell:RemoveAllListeners()
    self.view.themeList.onUpdateCell:AddListener(function(object, csIndex)
        self:_UpdateCell(object, csIndex)
    end)


    if self.view.redDotScrollRect then
        self.view.redDotScrollRect.getRedDotStateAt = function(csIndex)
            return self:GetRedDotStateAt(csIndex)
        end
    end

    self:_OnSelectIdChange(true)
    self.view.m_inCreate = false
end

FriendThemeChangeCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.m_arg and lume.deepCopy(self.m_arg) or {}
    arg.selectId = self.m_selectId
    return arg
end


FriendThemeChangeCtrl.GetRedDotStateAt = HL.Method(HL.Number).Return(HL.Number) << function(self, index)
    local luaIndex = LuaIndex(index)
    if luaIndex < 1 or luaIndex > #self.m_infos then
        return 0
    end

    local info = self.m_infos[luaIndex]
    if not info then
        return 0
    end

    local hasRedDot, redDotType, expireTs = RedDotManager:GetRedDotState("NewBusinessCard", info.cfg.id)
    if hasRedDot then
        return redDotType or UIConst.RED_DOT_TYPE.Normal
    else
        return 0
    end
end

FriendThemeChangeCtrl._UpdateCell = HL.Method(HL.Userdata, HL.Number) << function(self, object, csIndex)
    local cell = self.m_getCellFunc(object)
    local id = self.m_infos[LuaIndex(csIndex)].cfg.id

    cell:InitFriendThemeChangeThemeCell(self.m_infos[LuaIndex(csIndex)].cfg, function()
        if self.m_selectId == id then
            return
        end
        self.m_selectId = id
        self:_OnSelectIdChange(false)

    end, self.m_selectId == id, GameInstance.player.friendSystem:IsBusinessCardUnlock(CS.Beyond.Gameplay.FriendBusinessCardUnlockType.BusinessCardTopic, id))


    if self.m_selectId == id and self.m_inCreate then
        self:SetNaviTarget(cell.view.themeBtn)
    end
end

FriendThemeChangeCtrl._OnSelectIdChange = HL.Method(HL.Boolean) << function(self, init)


    logger.info("Selected theme ID changed to: " .. self.m_selectId)

    if self.m_panel then
        CSUtils.ClearUIComponents(self.m_panel)
        GameObject.DestroyImmediate(self.m_panel)
    end

    local cfg = Tables.businessCardTopicTable[self.m_selectId]

    local path = string.format(UIConst.UI_BUSINESS_CARD_PREFAB_PATH, cfg.panelPrefab)
    local prefab = self:LoadGameObject(path)

    self.m_panel = CSUtils.CreateObject(prefab, self.view.businessCardScale)

    self.m_businessCard = Utils.wrapLuaNode(self.m_panel)
    self.m_businessCard:InitFriendBusinessCard(GameInstance.player.roleId, true, false, true, self.m_selectId)

    if GameInstance.player.friendSystem:IsBusinessCardUnlock(CS.Beyond.Gameplay.FriendBusinessCardUnlockType.BusinessCardTopic, self.m_selectId) then
        self.view.lockState:SetState("Unlock")
        self.view.confirmBtn.interactable = true
        self.view.root:SetState("NormalState")
    else
        self.view.lockState:SetState("Lock")
        self:_UpdateObtainWay(cfg.itemId)
        self.view.confirmBtn.interactable = false
        self.view.root:SetState("DisableState")
    end

    if init then

        local index = 1
        for i, info in ipairs(self.m_infos) do
            if info.cfg.id == self.m_selectId then
                index = i
                break
            end
        end

        self.view.themeList:UpdateCount(#self.m_infos, index)
    else
        self.view.themeList:UpdateShowingCells(function(csIndex, object)
            self:_UpdateCell(object, csIndex)
        end)
    end
    self.view.nameTxt.text = Tables.itemTable[cfg.itemId].name
    self.view.detailsTxt.text = Tables.itemTable[cfg.itemId].decoDesc
end

FriendThemeChangeCtrl._UpdateObtainWay = HL.Method(HL.String) << function(self, itemId)
    local obtainWay = Utils.tryGetItemFirstObtainWay(itemId)
    local cell = self.view.obtainCell
    cell.gameObject:SetActive(obtainWay ~= nil)
    if obtainWay ~= nil then
        cell.normalNode.nameTxt.text = obtainWay.name
        local iconId = obtainWay.iconId
        local iconFolder = obtainWay.iconFolder
        cell.normalNode.icon.gameObject:SetActive(iconId ~= nil and iconFolder ~= nil)
        if iconId ~= nil and iconFolder ~= nil then
            cell.normalNode.icon:LoadSprite(obtainWay.iconFolder, obtainWay.iconId)
        end

        cell.normalNode.button.onClick:RemoveAllListeners()
        cell.normalNode.button.gameObject:SetActive(obtainWay.phaseId ~= nil)
        if obtainWay.phaseId then
            cell.normalNode.animationNode:PlayInAnimation()
            cell.normalNode.button.onClick:AddListener(function()
                local isBlocked = UIManager:ShouldBlockObtainWaysPhaseJump(obtainWay.phaseId)
                if isBlocked then
                    Notify(MessageConst.SHOW_TOAST, Language.LUA_OBTAIN_WAYS_JUMP_BLOCKED)
                    return
                end
                if obtainWay.phaseId then

                    PhaseManager:GoToPhase(obtainWay.phaseId, obtainWay.phaseArgs)
                end
            end)
        else
            cell.normalNode.animationNode:PlayOutAnimation()
        end
    end
end

FriendThemeChangeCtrl._RebuildInfos = HL.Method() << function(self)
    self.m_infos = {}
    for _, cfg in pairs(Tables.businessCardTopicTable) do
        local info = {
            sort = cfg.sort,
            cfg = cfg,
        }
        local success, itemCfg = Tables.itemTable:TryGetValue(cfg.itemId)
        if success then
            local canShow = GameInstance.player.friendSystem:IsBusinessCardUnlock(CS.Beyond.Gameplay.FriendBusinessCardUnlockType.BusinessCardTopic, cfg.id) or Utils.isNotObtainCanShow(itemCfg.notObtainShow, itemCfg.notObtainShowTimeId)
            if canShow then
                table.insert(self.m_infos, info)
            end
        else
            logger.error("FriendThemeChangeCtrl:_RebuildInfos itemCfg not found for id:", cfg.itemId)
        end
    end

    table.sort(self.m_infos, function(a, b)

        local aUnlock = GameInstance.player.friendSystem:IsBusinessCardUnlock(CS.Beyond.Gameplay.FriendBusinessCardUnlockType.BusinessCardTopic, a.cfg.id)
        local bUnlock = GameInstance.player.friendSystem:IsBusinessCardUnlock(CS.Beyond.Gameplay.FriendBusinessCardUnlockType.BusinessCardTopic, b.cfg.id)
        if aUnlock ~= bUnlock then
            return aUnlock
        end

        return a.sort < b.sort
    end)


    if not string.isEmpty(self.m_selectId) then
        local stillExist = false
        for _, info in ipairs(self.m_infos) do
            if info.cfg.id == self.m_selectId then
                stillExist = true
                break
            end
        end
        if not stillExist then
            self.m_selectId = GameInstance.player.friendSystem.SelfInfo.businessCardTopicId or ""
            if string.isEmpty(self.m_selectId) and #self.m_infos > 0 then
                self.m_selectId = self.m_infos[1].cfg.id
            end
        end
    end
end

FriendThemeChangeCtrl._Refresh = HL.Method() << function(self)
    if self.m_infos == nil then
        return
    end

    self:_RebuildInfos()

    self.view.themeList:UpdateCount(#self.m_infos)
    self:_OnSelectIdChange(false)
end

FriendThemeChangeCtrl.OnBusinessCardUnlock = HL.Method() << function(self)
    self:_Refresh()
end

FriendThemeChangeCtrl.OnSystemUnlockChanged = HL.Method() << function(self)


    self:_Refresh()
end

FriendThemeChangeCtrl.OnActivityUpdated = HL.Method() << function(self)

    self:_Refresh()
end

FriendThemeChangeCtrl._TryInvokeCloseCallback = HL.Method() << function(self)
    if self.m_closeCallback then
        self.m_closeCallback()
    end
end

FriendThemeChangeCtrl._Close = HL.Method() << function(self)
    self:_TryInvokeCloseCallback()
    if PhaseManager:IsOpen(PHASE_ID) then
        PhaseManager:PopPhase(PHASE_ID)
    else

        logger.error("FriendThemeChangeCtrl:Close - Phase not found, fallback to PlayAnimationOutAndClose")
        self:PlayAnimationOutAndClose()
        if PhaseManager:IsOpen(PhaseId.Friend) then
            PhaseManager:GoToPhase(PhaseId.Friend)
        end
    end
end



FriendThemeChangeCtrl.OnShow = HL.Override() << function(self)
    if self.m_infos == nil then
        return
    end
    self:_Refresh()
end









HL.Commit(FriendThemeChangeCtrl)
