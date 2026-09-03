local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FriendHeadSelectedPopUp
local PHASE_ID = PhaseId.FriendHeadSelectedPopUp

local tabConfig = {
    avatar = {
        GetCurrentId = function()
            return GameInstance.player.friendSystem.SelfInfo.userAvatarId
        end,
        icon = "icon_friend_type_head",
        GetTable = function()
            return Tables.userAvatarTable
        end,
        type = CS.Beyond.Gameplay.FriendBusinessCardUnlockType.Avatar,
        currentSelectId = "",
        successNotify = Language.LUA_FRIEND_HEAD_SELECTED_POPUP_AVATAR,
        title = Language.LUA_FRIEND_HEAD_CHANGE,
        SendMsg = function(id)
            GameInstance.player.friendSystem:AvatarModify(id)
        end,
        redDot = "NewAvatar",
        resPath = "",
    },
    avatarFrame = {
        GetCurrentId = function()
            return GameInstance.player.friendSystem.SelfInfo.userAvatarFrameId
        end,
        icon = "icon_friend_type_headframe",
        GetTable = function()
            return Tables.userAvatarTableFrame
        end,
        type = CS.Beyond.Gameplay.FriendBusinessCardUnlockType.AvatarFrame,
        currentSelectId = "",
        successNotify = Language.LUA_FRIEND_HEAD_SELECTED_POPUP_AVATAR_FRAME,
        title = Language.LUA_FRIEND_HEAD_FRAME_CHANGE,
        SendMsg = function(id)
            GameInstance.player.friendSystem:AvatarFrameModify(id)
        end,
        redDot = "NewAvatarFrame",
        resPath = UIConst.UI_SPRITE_HEAD_FRAME,
    }
}

local tabInfo = {
    [1] = tabConfig.avatar,
    [2] = tabConfig.avatarFrame,
}

FriendHeadSelectedPopUpCtrl = HL.Class('FriendHeadSelectedPopUpCtrl', uiCtrl.UICtrl)

FriendHeadSelectedPopUpCtrl.m_selectId = HL.Field(HL.String) << ""

FriendHeadSelectedPopUpCtrl.m_tabConfig = HL.Field(HL.Table)

FriendHeadSelectedPopUpCtrl.m_getCell = HL.Field(HL.Function)

FriendHeadSelectedPopUpCtrl.m_cfgTable = HL.Field(HL.Table)

FriendHeadSelectedPopUpCtrl.m_inTabChange = HL.Field(HL.Boolean) << false

FriendHeadSelectedPopUpCtrl.m_genTabCells = HL.Field(HL.Forward("UIListCache"))





FriendHeadSelectedPopUpCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_FRIEND_BUSINESS_INFO_CHANGE] = 'OnFriendBusinessInfoChange',

    [MessageConst.ON_AVATAR_UNLOCK] = 'OnAvatarUnlock',
    [MessageConst.ON_AVATAR_FRAME_UNLOCK] = 'OnAvatarFrameUnlock',

    [MessageConst.ON_SYSTEM_UNLOCK_CHANGED] = 'OnSystemUnlockChanged',
    [MessageConst.ON_ALL_SYSTEM_UNLOCK_SYNC] = 'OnSystemUnlockChanged',

    [MessageConst.ON_ACTIVITY_UPDATED] = 'OnActivityUpdated',
}


FriendHeadSelectedPopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeBtn.onClick:RemoveAllListeners()
    self.view.closeBtn.onClick:AddListener(function()
        self:_CancelAndClose()
    end)

    self.view.cancelBtn.onClick:RemoveAllListeners()
    self.view.cancelBtn.onClick:AddListener(function()
        self:_CancelAndClose()
    end)

    self.view.confirmBtn.onClick:RemoveAllListeners()
    self.view.confirmBtn.onClick:AddListener(function()
        if tabConfig.avatar.currentSelectId ~= tabConfig.avatar.GetCurrentId() and not string.isEmpty(tabConfig.avatar.currentSelectId) then
            tabConfig.avatar.SendMsg(tabConfig.avatar.currentSelectId)
        end

        if tabConfig.avatarFrame.currentSelectId ~= tabConfig.avatarFrame.GetCurrentId() and not string.isEmpty(tabConfig.avatarFrame.currentSelectId) then
            tabConfig.avatarFrame.SendMsg(tabConfig.avatarFrame.currentSelectId)
        end
        self:_Close()
    end)

    self.view.commonPlayerHead:UpdateHideLevelTxt(true)
    self.view.commonPlayerHead:InitCommonPlayerHeadByRoleId(GameInstance.player.roleId, false)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })

    if arg and arg.tabIndex then
        self.m_tabConfig = tabInfo[arg.tabIndex]
        tabConfig.avatar.currentSelectId = arg.avatarSelectId or ""
        tabConfig.avatarFrame.currentSelectId = arg.avatarFrameSelectId or ""
    else
        self.m_tabConfig = tabConfig.avatar
    end

    self.m_getCell = UIUtils.genCachedCellFunction(self.view.goodsScrollView)
    self.view.headScrollList.onUpdateCell:RemoveAllListeners();
    self.view.headScrollList.onUpdateCell:AddListener(function(gameObject, index)
        self:_UpdateCell(gameObject, LuaIndex(index))
    end)

    self.m_genTabCells = UIUtils.genCellCache(self.view.tabs.tabCell)
    self.m_genTabCells:Refresh(#tabInfo, function(cell, luaIndex)
        local info = tabInfo[luaIndex]
        cell.gameObject.name = "FriendTab_" .. luaIndex


        cell.selectedIcon:LoadSprite(UIConst.UI_SPRITE_FRIEND, info.icon)
        cell.defaultIcon:LoadSprite(UIConst.UI_SPRITE_FRIEND, info.icon)

        cell.redDot:InitRedDot(info.redDot, "")

        cell.toggle.onValueChanged:RemoveAllListeners()
        cell.toggle.onValueChanged:AddListener(function(isOn)
            if isOn then
                self.m_inTabChange = true


                if not InputManagerInst.inChangingInputDevice
                    and self.m_tabConfig ~= nil
                    and not string.isEmpty(self.m_tabConfig.currentSelectId) then
                    local hasCurrent = false
                    for id, cfg in pairs(self.m_tabConfig.GetTable()) do
                        if cfg.id == self.m_tabConfig.currentSelectId and GameInstance.player.friendSystem:IsBusinessCardUnlock(self.m_tabConfig.type, cfg.id) then
                            hasCurrent = true
                            break
                        end
                    end
                    if not hasCurrent then
                        self.m_tabConfig.currentSelectId = self.m_tabConfig.GetCurrentId()
                    end
                end

                self.m_tabConfig = info
                self:_RebuildCfgTableForCurrentTab()


                local currentIndex = 1
                for index, cfg in ipairs(self.m_cfgTable) do
                    if cfg.id == self.m_tabConfig.currentSelectId then
                        currentIndex = index
                        break
                    end
                end
                self.view.headScrollList:UpdateCount(#self.m_cfgTable, CSIndex(currentIndex))
                self:_OnSelectChange()
            end
        end)
        cell.toggle.isOn = false
        cell.toggle.isOn = self.m_tabConfig == info
    end)
    self.m_inTabChange = true

    local currentIndex = 1
    for index, cfg in ipairs(self.m_cfgTable) do
        if cfg.id == self.m_tabConfig.currentSelectId then
            currentIndex = index
            break
        end
    end
    self.view.headScrollList:UpdateCount(#self.m_cfgTable, CSIndex(currentIndex))

end

FriendHeadSelectedPopUpCtrl._ResetSelectToCurrent = HL.Method() << function(self)
    tabConfig.avatar.currentSelectId = tabConfig.avatar.GetCurrentId()
    tabConfig.avatarFrame.currentSelectId = tabConfig.avatarFrame.GetCurrentId()
end

FriendHeadSelectedPopUpCtrl._CancelAndClose = HL.Method() << function(self)
    if not InputManagerInst.inChangingInputDevice then
        self:_ResetSelectToCurrent()
    end
    self:_Close()
end

FriendHeadSelectedPopUpCtrl._Close = HL.Method() << function(self)
    if PhaseManager:IsOpen(PHASE_ID) then
        PhaseManager:PopPhase(PHASE_ID)
    else
        self:PlayAnimationOutAndClose()
    end
end

FriendHeadSelectedPopUpCtrl._UpdateCell = HL.Method(HL.Userdata, HL.Number) << function(self, gameObject, luaIndex)
    local itemCell = self.m_getCell(gameObject)
    local cfg = self.m_cfgTable[luaIndex]

    itemCell.redDot:InitRedDot(self.m_tabConfig.redDot, cfg.id)
    itemCell.stateCtrl:SetState(self.m_tabConfig.currentSelectId == cfg.id and 'Selected' or 'Unselected')
    itemCell.stateCtrl:SetState(GameInstance.player.friendSystem:IsBusinessCardUnlock(self.m_tabConfig.type, cfg.id) and 'Unlocked' or 'Locked')
    itemCell.itemBig:InitItem({ id = cfg.itemId }, function()
        if self.m_tabConfig.currentSelectId == cfg.id then
            return
        end
        self.m_tabConfig.currentSelectId = cfg.id
        GameInstance.player.friendSystem:ReadBusinessCardUnlockRedDot(self.m_tabConfig.type, cfg.id)
        self:_OnSelectChange()
    end)

    if string.isEmpty(self.m_tabConfig.resPath) then
        itemCell.itemBig.view.icon.view.icon:LoadSprite(cfg.icon)
    else

        itemCell.itemBig.view.icon.view.icon:LoadSprite(self.m_tabConfig.resPath, cfg.icon)
    end






    if DeviceInfo.usingController and self.m_inTabChange and self.m_tabConfig.currentSelectId == cfg.id then
        self.m_inTabChange = false
        self:SetNaviTarget(itemCell.itemBig.view.button)
    end
end

FriendHeadSelectedPopUpCtrl._OnSelectChange = HL.Method() << function(self)
    local currentAvatarIcon = ""
    local currentAvatarFrameIcon = ""
    if not string.isEmpty(tabConfig.avatar.currentSelectId) then
        currentAvatarIcon = Tables.userAvatarTable:GetValue(tabConfig.avatar.currentSelectId).icon
    end
    if not string.isEmpty(tabConfig.avatarFrame.currentSelectId) then
        currentAvatarFrameIcon = Tables.userAvatarTableFrame:GetValue(tabConfig.avatarFrame.currentSelectId).icon
    end

    if not string.isEmpty(self.m_tabConfig.currentSelectId) then
        self.view.commonPlayerHead:InitCommonPlayerHead(currentAvatarIcon, currentAvatarFrameIcon, false)

        local currentSelectItemId = self.m_tabConfig.GetTable():GetValue(self.m_tabConfig.currentSelectId).itemId
        local itemCfg = Tables.itemTable:GetValue(currentSelectItemId)

        self.view.headNameTxt.text = itemCfg.name

        self:_UpdateObtainWay(currentSelectItemId)
    end
    self.view.titleTxt.text = self.m_tabConfig.title
    self.view.headScrollList:UpdateShowingCells(function(csIndex, object)
        self:_UpdateCell(object, LuaIndex(csIndex))
    end)

    local canConfirm = GameInstance.player.friendSystem:IsBusinessCardUnlock(tabConfig.avatar.type, tabConfig.avatar.currentSelectId)
        and GameInstance.player.friendSystem:IsBusinessCardUnlock(tabConfig.avatarFrame.type, tabConfig.avatarFrame.currentSelectId)

    self.view.confirmBtn.interactable = canConfirm
    self.view.confirmState:SetState(canConfirm and 'NormalState' or 'DisableState')
end

FriendHeadSelectedPopUpCtrl._UpdateObtainWay = HL.Method(HL.String) << function(self, itemId)
    local obtainWay = Utils.tryGetItemFirstObtainWay(itemId)
    local cell = self.view.obtainCell
    local showObtainWay = obtainWay ~= nil and not GameInstance.player.friendSystem:IsBusinessCardUnlock(self.m_tabConfig.type, self.m_tabConfig.currentSelectId)
    cell.gameObject:SetActive(showObtainWay)
    if showObtainWay then
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

FriendHeadSelectedPopUpCtrl._RebuildCfgTableForCurrentTab = HL.Method() << function(self)
    if self.m_tabConfig == nil then
        return
    end
    self.m_cfgTable = {}
    for id, cfg in pairs(self.m_tabConfig.GetTable()) do
        local success, itemCfg = Tables.itemTable:TryGetValue(cfg.itemId)
        if success then
            local canShow = GameInstance.player.friendSystem:IsBusinessCardUnlock(self.m_tabConfig.type, cfg.id) or Utils.isNotObtainCanShow(itemCfg.notObtainShow, itemCfg.notObtainShowTimeId)
            if canShow then
                table.insert(self.m_cfgTable, cfg)
            end
        else
            logger.error("FriendHeadSelectedPopUpCtrl:_RebuildCfgTableForCurrentTab itemCfg not found for id:", cfg.itemId)
        end
    end

    table.sort(self.m_cfgTable, function(a, b)
        return a.sort < b.sort
    end)


    if string.isEmpty(self.m_tabConfig.currentSelectId) then
        self.m_tabConfig.currentSelectId = self.m_tabConfig.GetCurrentId()
        if string.isEmpty(self.m_tabConfig.currentSelectId) and #self.m_cfgTable > 0 then
            self.m_tabConfig.currentSelectId = self.m_cfgTable[1].id
        end
    else

        local stillExist = false
        for _, cfg in ipairs(self.m_cfgTable) do
            if cfg.id == self.m_tabConfig.currentSelectId then
                stillExist = true
                break
            end
        end
        if not stillExist then
            self.m_tabConfig.currentSelectId = self.m_tabConfig.GetCurrentId()
            if string.isEmpty(self.m_tabConfig.currentSelectId) and #self.m_cfgTable > 0 then
                self.m_tabConfig.currentSelectId = self.m_cfgTable[1].id
            end
        end
    end
end

FriendHeadSelectedPopUpCtrl._Refresh = HL.Method() << function(self)
    if self.m_tabConfig == nil then
        return
    end

    self:_RebuildCfgTableForCurrentTab()

    self.view.headScrollList:UpdateCount(#self.m_cfgTable)

    self:_OnSelectChange()
end

FriendHeadSelectedPopUpCtrl.OnFriendBusinessInfoChange = HL.Method() << function(self)

    if self.m_tabConfig.currentSelectId == "" then
        return
    end

    Notify(MessageConst.SHOW_TOAST, self.m_tabConfig.successNotify)


    self:_Refresh()
end

FriendHeadSelectedPopUpCtrl.OnAvatarUnlock = HL.Method() << function(self)
    self:_Refresh()
end

FriendHeadSelectedPopUpCtrl.OnAvatarFrameUnlock = HL.Method() << function(self)
    self:_Refresh()
end

FriendHeadSelectedPopUpCtrl.OnSystemUnlockChanged = HL.Method() << function(self)


    self:_Refresh()
end

FriendHeadSelectedPopUpCtrl.OnActivityUpdated = HL.Method() << function(self)

    self:_Refresh()
end



FriendHeadSelectedPopUpCtrl.OnShow = HL.Override() << function(self)
    if self.m_tabConfig == nil then
        return
    end


    self.m_inTabChange = true
    self:_Refresh()
end

FriendHeadSelectedPopUpCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local tabIndex = 1
    for i, info in ipairs(tabInfo) do
        if self.m_tabConfig == info then
            tabIndex = i
            break
        end
    end
    return {
        tabIndex = tabIndex,
        avatarSelectId = tabConfig.avatar.currentSelectId,
        avatarFrameSelectId = tabConfig.avatarFrame.currentSelectId,
    }
end

HL.Commit(FriendHeadSelectedPopUpCtrl)
