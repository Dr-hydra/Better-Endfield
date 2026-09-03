
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceshipRoomUpgrade
local PHASE_ID = PhaseId.SpaceshipRoomUpgrade
SpaceshipRoomUpgradeCtrl = HL.Class('SpaceshipRoomUpgradeCtrl', uiCtrl.UICtrl)


local States = {
    Upgrade = "Upgrade",
    Max = "Max",
    Build = "Build",
}






SpaceshipRoomUpgradeCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.SPACESHIP_ON_ROOM_LEVEL_UP] = 'OnRoomLevelUp',
    [MessageConst.SPACESHIP_ON_ROOM_ADDED] = 'OnRoomAdded',
}


SpaceshipRoomUpgradeCtrl.m_roomId = HL.Field(HL.String) << ''

SpaceshipRoomUpgradeCtrl.m_roomType = HL.Field(GEnums.SpaceshipRoomType)

SpaceshipRoomUpgradeCtrl.m_allowBuildType = HL.Field(GEnums.SpaceshipRoomType)

SpaceshipRoomUpgradeCtrl.m_buildTypeIndex = HL.Field(HL.Number) << -1

SpaceshipRoomUpgradeCtrl.m_buildTypeData = HL.Field(HL.Table)

SpaceshipRoomUpgradeCtrl.m_moveCam = HL.Field(HL.Boolean) << false

SpaceshipRoomUpgradeCtrl.m_clearScreenKey = HL.Field(HL.Number) << -1

SpaceshipRoomUpgradeCtrl.m_isRemoteCamera = HL.Field(HL.Boolean) << false

SpaceshipRoomUpgradeCtrl.m_roomInfo = HL.Field(CS.Beyond.Gameplay.SpaceshipSystem.Room)

SpaceshipRoomUpgradeCtrl.m_roomTypeData = HL.Field(Cfg.Types.SpaceshipRoomTypeData)

SpaceshipRoomUpgradeCtrl.m_roomLvTable = HL.Field(HL.Userdata)

SpaceshipRoomUpgradeCtrl.m_isEnough = HL.Field(HL.Boolean) << false

SpaceshipRoomUpgradeCtrl.m_curSelectedLv = HL.Field(HL.Number) << -1

SpaceshipRoomUpgradeCtrl.m_state = HL.Field(HL.String) << ''

SpaceshipRoomUpgradeCtrl.m_upgradeEffectCells = HL.Field(HL.Forward('UIListCache'))

SpaceshipRoomUpgradeCtrl.m_portNodeCells = HL.Field(HL.Forward('UIListCache'))

SpaceshipRoomUpgradeCtrl.m_tempCancelBindingId = HL.Field(HL.Number) << -1

SpaceshipRoomUpgradeCtrl.OnIntSSRoom = HL.StaticMethod(HL.Any) << function(args)
    local roomId, type, cameraBlend = unpack(args)
    local _, room = GameInstance.player.spaceship:TryGetRoom(roomId)

    if type == CS.Beyond.Gameplay.SpaceshipIntType.BuildOrUpgrade then
        if not PhaseManager:CheckCanOpenPhaseAndToast(PhaseId.SpaceshipRoomUpgrade, nil) or PhaseManager:CheckIsInTransition() then
            return
        end
        local clearScreenKey
        if cameraBlend then
            clearScreenKey = UIManager:ClearScreen()
        end
        PhaseManager:OpenPhase(PhaseId.SpaceshipRoomUpgrade, {
            roomId = roomId,
            moveCam = cameraBlend ~= nil,
            clearScreenKey = clearScreenKey,
        })
    elseif type == CS.Beyond.Gameplay.SpaceshipIntType.Room then
        local roomType = room.type
        local phaseId = PhaseId[SpaceshipConst.ROOM_PHASE_ID_NAME_MAP_BY_TYPE[roomType]]
        local phaseArgs =
        {
            roomId = roomId,
            moveCam = true,
        }
        if not PhaseManager:CheckCanOpenPhaseAndToast(phaseId, phaseArgs) or PhaseManager:CheckIsInTransition() then
            return
        end

        local startPhaseId = PhaseManager:GetTopPhaseId()
        GameInstance.player.spaceship:MoveCamToSpaceshipRoom(roomId)
        local clearScreenKey
        if cameraBlend then
            clearScreenKey = UIManager:ClearScreen()
        end
        phaseArgs.clearScreenKey = clearScreenKey
        TimerManager:StartTimer(0.5, function()
            local canOpenPhase = PhaseManager:GetTopPhaseId() == startPhaseId and not PhaseManager:CheckIsInTransition()
                and PhaseManager:CheckCanOpenPhase(phaseId, phaseArgs, true)
            if not canOpenPhase then
                GameInstance.player.spaceship:UndoMoveCamToSpaceshipRoom(roomId)
                return
            end
            PhaseManager:OpenPhase(phaseId, phaseArgs)
        end)
    elseif type == CS.Beyond.Gameplay.SpaceshipIntType.Deconstruct then
        local roomType = room.type
        local content, subContent = SpaceshipUtils.getDeconstructDescByType(roomType)
        Notify(MessageConst.SHOW_POP_UP, {
            content = content,
            subContent = subContent,
            onConfirm = function()
                GameInstance.player.spaceship:DeconstructRoom(roomId)
            end
        })
    end
end

SpaceshipRoomUpgradeCtrl.OnRoomDeconstruct = HL.StaticMethod(HL.Any) << function(args)
    local roomId = unpack(args)
    local emptyRoomData = Tables.spaceshipEmptyRoomTable[roomId]
    local dialogId = emptyRoomData.demolitionDialogId
    SpaceshipUtils.playSSDialog(roomId, dialogId)
end


SpaceshipRoomUpgradeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeBtn.onClick:AddListener(function()
        self:_Exit()
    end)
    self.view.upgradeBtn.onClick:AddListener(function()
        self:_OnClickConfirm()
    end)
    self.view.buildBtn.onClick:AddListener(function()
        self:_OnClickConfirm()
    end)

    self.view.roomInfoTipNode.button.onClick:AddListener(function()
        if self.view.roomInfoTipNode.tipNode.gameObject.activeSelf then
            self.view.roomInfoTipNode.tipNodeAnimationWrapper:PlayOutAnimation(function()
                self.view.roomInfoTipNode.tipNode.gameObject:SetActive(false)
            end)
        else
            self.view.roomInfoTipNode.tipNode.gameObject:SetActive(true)
            AudioManager.PostEvent("Au_UI_Button_Detail")
            self.view.roomInfoTipNode.tipNodeAnimationWrapper:PlayInAnimation()
        end
        if DeviceInfo.usingController then
            Notify(MessageConst.SHOW_AS_CONTROLLER_SMALL_MENU, {
                panelId = PANEL_ID,
                isGroup = true,
                id = self.view.roomInfoTipNode.tipNodeInputBindingGroupMonoTarget.groupId,
                rectTransform = self.view.roomInfoTipNode.gameObject.transform,
                noHighlight = true,
            })

            self.m_tempCancelBindingId = self:BindInputPlayerAction("common_cancel", function()
                self.view.roomInfoTipNode.tipNode.gameObject:SetActive(false)
                self:_DeleteDetailNaviBinding()
            end, self.view.roomInfoTipNode.tipNodeInputBindingGroupMonoTarget.groupId)
        end
    end)

    self.view.changeLvNode.reduceBtn.onClick:AddListener(function()
        self:_ChangeCurSelectedLv(self.m_curSelectedLv - 1)
    end)
    self.view.changeLvNode.addBtn.onClick:AddListener(function()
        self:_ChangeCurSelectedLv(self.m_curSelectedLv + 1)
    end)
    self.view.buildNode.leftBtn.onClick:AddListener(function()
        self:_OnClickBuildSwitchBtn(true)
    end)
    self.view.buildNode.rightBtn.onClick:AddListener(function()
        self:_OnClickBuildSwitchBtn(false)
    end)

    self.m_upgradeEffectCells = UIUtils.genCellCache(self.view.upgradeEffectCell)
    self.m_portNodeCells = UIUtils.genCellCache(self.view.buildNode.portImage)

    local roomId, moveCam, clearScreenKey, isRemoteCamera
    if type(arg) == "string" then
        roomId = arg
        moveCam = false
        isRemoteCamera = false
    else
        roomId = arg.roomId
        moveCam = arg.moveCam
        clearScreenKey = arg.clearScreenKey
        isRemoteCamera = arg.isRemoteCamera
    end
    self.m_roomId = roomId
    self.m_moveCam = moveCam == true
    self.m_clearScreenKey = clearScreenKey or -1
    self.m_isRemoteCamera = isRemoteCamera == true

    local unlocked, roomData = GameInstance.player.spaceship:TryGetRoom(self.m_roomId)
    if unlocked then
        self.m_roomInfo = roomData
        self.m_roomType = roomData.roomType
        self.m_roomLvTable = SpaceshipUtils.getRoomLvTableByType(self.m_roomType)
        self.m_roomTypeData = Tables.spaceshipRoomTypeTable[self.m_roomType]
        local lv = self.m_roomInfo.lv
        local maxLv = self.m_roomInfo.maxLv
        local isMax = lv >= maxLv
        self.m_state = isMax and States.Max or States.Upgrade
        self.view.content:SetState(self.m_state)
        if isMax then
            self.m_curSelectedLv = lv
            self:_RefreshMaxInfo()
        else
            self.m_curSelectedLv = lv + 1
            self:_RefreshUpgradeInfo()
        end
        self:_SetViewByRoomTypeData()
    else
        self:_InitBuildInfo()
    end

    if self.m_moveCam or self.m_isRemoteCamera then
        local succ, config = GameInstance.player.spaceship:GetAndSetCurSpaceshipRoomCamConfig(roomId, "upgrade")
        if succ then
            GameInstance.player.spaceship:MoveCamToSpaceshipRoom(roomId, self.m_clearScreenKey, self.m_isRemoteCamera)
        end
    end
    if self.m_isRemoteCamera then
        local _, level = GameUtil.SpaceshipUtils.TryGetSpaceshipLevel()
        local succ, cabinPos = GameInstance.player.spaceship:TryGetCabinEntityTransform(self.m_roomId)
        level:HideCharacters(cabinPos)
        local maskData = CS.Beyond.Gameplay.UICommonMaskData()
        maskData.maskType = UIConst.UI_COMMON_MASK_TYPE.BlackScreen
        maskData.fadeInTime = 0
        maskData.waitHide = true
        if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
            maskData.extraData = CS.Beyond.Gameplay.CommonMaskExtraData()
            maskData.extraData.desc = "SpaceshipRoomUpgrade Create"
        end
        GameAction.ShowBlackScreen(maskData)
        level:LoadExtraAtPosWithCallback(cabinPos, function()
            GameAction.BlackScreenFadeOut(0.2, true, false)
        end)
    end
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

SpaceshipRoomUpgradeCtrl.OnShow = HL.Override() << function(self)
    if self.m_state == States.Build then
        self:_RefreshBuildInfo(self.m_roomType)
        return
    end
    local lv = self.m_roomInfo.lv
    local maxLv = self.m_roomInfo.maxLv
    local isMax = lv >= maxLv
    self.m_state = isMax and States.Max or States.Upgrade
    self.view.content:SetState(self.m_state)
    if isMax then
        self.m_curSelectedLv = lv
        self:_RefreshMaxInfo()
    else
        self.m_curSelectedLv = lv + 1
        self:_RefreshUpgradeInfo()
    end
end

SpaceshipRoomUpgradeCtrl.OnClose = HL.Override() << function(self)

    if self.m_moveCam or self.m_isRemoteCamera then
        local clearScreenKey = GameInstance.player.spaceship:UndoMoveCamToSpaceshipRoom(self.m_roomId, self.m_isRemoteCamera)
        if clearScreenKey and clearScreenKey ~= -1 then
            UIManager:RecoverScreen(clearScreenKey)
        end
        if self.m_clearScreenKey ~= -1 then
            if self.m_clearScreenKey ~= clearScreenKey then
                UIManager:RecoverScreen(self.m_clearScreenKey)
            end
            self.m_clearScreenKey = -1
        end
        self.m_moveCam = false
    end
    if self.m_isRemoteCamera then
        local _, level = GameUtil.SpaceshipUtils.TryGetSpaceshipLevel()
        level:ShowCharacters()
        local maskData = CS.Beyond.Gameplay.UICommonMaskData()
        maskData.maskType = UIConst.UI_COMMON_MASK_TYPE.BlackScreen
        maskData.fadeInTime = 0
        maskData.waitHide = true
        if BEYOND_DEBUG or BEYOND_DEBUG_COMMAND then
            maskData.extraData = CS.Beyond.Gameplay.CommonMaskExtraData()
            maskData.extraData.desc = "SpaceshipRoomUpgrade Close"
        end
        GameAction.ShowBlackScreen(maskData)
        level:ClearExtraLoadWithCallback(function()
            GameAction.BlackScreenFadeOut(0.2, true, false)
        end)
    end
end


SpaceshipRoomUpgradeCtrl._DeleteDetailNaviBinding = HL.Method() << function(self)
    InputManagerInst:DeleteBinding(self.m_tempCancelBindingId)
    Notify(MessageConst.CLOSE_CONTROLLER_SMALL_MENU, self.view.roomInfoTipNode.tipNodeInputBindingGroupMonoTarget.groupId)
end

SpaceshipRoomUpgradeCtrl._SetViewByRoomTypeData = HL.Method() << function(self)
    if not self.m_roomTypeData then
        return
    end
    local count = 0

    if self.m_roomInfo then
        count = self.m_roomInfo.serialNum or 0
    else
        local rooms = GameInstance.player.spaceship.rooms
        local roomCount = 0
        for id, roomInfo in pairs(rooms) do
            if roomInfo.type == self.m_roomType then
                roomCount = roomCount + 1
            end
        end
        count = roomCount + 1
    end
    local name = self.m_roomInfo and SpaceshipUtils.getFormatCabinSerialNum(self.m_roomId, count) or SpaceshipUtils.getFormatCabinSerialNumByName(self.m_roomTypeData.name, count)
    self.view.name.text = name
    self.view.icon:LoadSprite(UIConst.UI_SPRITE_SPACESHIP_ROOM, self.m_roomTypeData.icon)
    self.view.colorfulBg.color = UIUtils.getColorByString(self.m_roomTypeData.color)
    self.view.previewItemNode.text.text = self.m_roomTypeData.newFormulaTitle
end



SpaceshipRoomUpgradeCtrl.OnRoomAdded = HL.Method(HL.Table) << function(self, arg)
    local roomId = unpack(arg)
    if roomId ~= self.m_roomId then
        return
    end

    self:_Exit(true, true, self.m_state == States.Build)
end

SpaceshipRoomUpgradeCtrl._InitBuildInfo = HL.Method() << function(self)
    self.m_curSelectedLv = 1
    self.m_state = States.Build
    self.view.content:SetState(self.m_state)
    self.m_allowBuildType = Tables.spaceshipEmptyRoomTable:GetValue(self.m_roomId).roomType
    self.m_buildTypeData = {}

    for i = 0, CSIndex(Tables.SpaceshipBuildTypeTable[self.m_allowBuildType].typeList.Count) do
        local roomType = Tables.SpaceshipBuildTypeTable[self.m_allowBuildType].typeList[i].type
        local roomCount, maxCount = SpaceshipUtils.getSpaceshipNowAndMaxRoom(roomType)
        table.insert(self.m_buildTypeData, Tables.spaceshipRoomTypeTable[roomType])
        if roomCount ~= maxCount and self.m_buildTypeIndex ~= -1 then
            self.m_buildTypeIndex = LuaIndex(i)
        end
    end

    if self.m_buildTypeIndex == -1 then
        self.m_buildTypeIndex = 1
    end
    self:_RefreshBuildInfo(self.m_buildTypeData[self.m_buildTypeIndex].type)
end

SpaceshipRoomUpgradeCtrl._OnClickBuildSwitchBtn = HL.Method(HL.Boolean) <<function(self, isLeft)
    if isLeft then
        self.m_buildTypeIndex = self.m_buildTypeIndex - 1
        if self.m_buildTypeIndex <= 1 then
            self.m_buildTypeIndex = 1
        end
    else
        self.m_buildTypeIndex = self.m_buildTypeIndex + 1
        if self.m_buildTypeIndex >= #self.m_buildTypeData then
            self.m_buildTypeIndex = #self.m_buildTypeData
        end
    end
    self:_RefreshBuildInfo(self.m_buildTypeData[self.m_buildTypeIndex].type)
end
SpaceshipRoomUpgradeCtrl._RefreshRedDot = HL.Method() <<function(self)
    local data = self.m_buildTypeData
    local index = self.m_buildTypeIndex
    local playerSpaceship = GameInstance.player.spaceship

    local function shouldShowRedDot(roomType)
        local _, curMaxCount = SpaceshipUtils.getSpaceshipNowAndMaxRoom(roomType)
        local isLock = curMaxCount == 0
        return not playerSpaceship:GetBuildRoomReadStateByType(roomType) and not isLock
    end

    self.view.buildNode.redDotRight.gameObject:SetActive(
        index < #data and shouldShowRedDot(data[index + 1].type)
    )

    self.view.buildNode.redDotLeft.gameObject:SetActive(
        index > 1 and shouldShowRedDot(data[index - 1].type)
    )

    local midType = data[index].type
    local shouldShowMid = shouldShowRedDot(midType)
    self.view.newTagImg.gameObject:SetActive(shouldShowMid)

    local _, curMaxCount = SpaceshipUtils.getSpaceshipNowAndMaxRoom(midType)
    if shouldShowMid and curMaxCount > 0 then
        playerSpaceship:ReadBuildRoomRedDot(midType)
    end
end

SpaceshipRoomUpgradeCtrl._RefreshBuildInfo = HL.Method(GEnums.SpaceshipRoomType) << function(self, roomType)
    self.m_roomType = roomType
    self.m_roomLvTable = SpaceshipUtils.getRoomLvTableByType(self.m_roomType)
    self.m_roomTypeData = Tables.spaceshipRoomTypeTable[self.m_roomType]
    local typeLvData = self.m_roomLvTable[self.m_curSelectedLv]
    local commonLvData = Tables.spaceshipRoomLvTable[typeLvData.id]
    self.view.buildNode.descTxt.text = self.m_roomTypeData.desc
    self:_UpdateCommonItemList(self.view.buildNode.previewItemNode, self.m_roomTypeData.previewProductItemIds)
    self:_UpdateCostItemList()
    self:_SetViewByRoomTypeData()
    self.view.buildNode.leftBtn.gameObject:SetActive(#self.m_buildTypeData > 1)
    self.view.buildNode.rightBtn.gameObject:SetActive(#self.m_buildTypeData > 1)
    self.view.buildNode.leftBtn.interactable = self.m_buildTypeIndex > 1
    self.view.buildNode.leftBtnStateController:SetState(self.m_buildTypeIndex > 1 and "Nrl" or "Dis")
    self.view.buildNode.rightBtn.interactable = self.m_buildTypeIndex < #self.m_buildTypeData
    self.view.buildNode.rightBtnStateController:SetState(self.m_buildTypeIndex < #self.m_buildTypeData and "Nrl" or "Dis")

    local curCount, curMaxCount = SpaceshipUtils.getSpaceshipNowAndMaxRoom(roomType)
    self.view.buildNode.countTxt.text = string.format("%d/%d", curCount, curMaxCount)

    self.m_portNodeCells:Refresh(#self.m_buildTypeData, function(cell, index)
        cell.stateController:SetState(index == self.m_buildTypeIndex and "On" or "Off")
    end)

    if curCount == curMaxCount then
        if curMaxCount == 0 then
            self.view.buildNode.stateController:SetState("Lock")
            self.view.buildNode.emptyTxt.text = commonLvData.conditionDesc
        else
            self.view.buildNode.stateController:SetState("Tips")
            self.view.cantConfirmHintNode.text.text = Language.LUA_SPACESHIP_ROOM_COUNT_LIMIT
        end
    elseif not self.m_isEnough then
        self.view.buildNode.stateController:SetState("Tips")
        self.view.cantConfirmHintNode.text.text = Language.LUA_ITEM_NOT_ENOUGH
    else
        self.view.buildNode.stateController:SetState("Normal")
    end
    self:_RefreshRedDot()
end






SpaceshipRoomUpgradeCtrl.OnRoomLevelUp = HL.Method(HL.Table) << function(self, arg)
    local roomId = unpack(arg)
    if roomId ~= self.m_roomId then
        return
    end
    self:_Exit(true)
end

SpaceshipRoomUpgradeCtrl._RefreshUpgradeInfo = HL.Method() << function(self)
    local roomInfo = self.m_roomInfo
    local isControlCenter = roomInfo.type == GEnums.SpaceshipRoomType.ControlCenter
    if not self.m_roomLvTable then
        logger.error("没有找到舱室对应的m_roomLvTable", self.m_roomId)
        return
    end
    local typeLvData = self.m_roomLvTable[self.m_curSelectedLv]
    local commonLvData = Tables.spaceshipRoomLvTable[typeLvData.id]

    self.view.lvDotNode:InitLvDotNode(roomInfo.lv, roomInfo.maxLv, UIUtils.getColorByString(self.m_roomTypeData.color))
    self:_UpdateChangeLvNodeState()


    local effectInfos = SpaceshipUtils.getUpgradeEffectInfos(self.m_roomId, self.m_curSelectedLv)
    self.m_upgradeEffectCells:Refresh(#effectInfos, function(cell, index)
        local info = effectInfos[index]
        cell.nameTxt.text = info.name
        cell.newTxt.gameObject:SetActive(info.newText ~= nil)
        cell.beforeTxt.gameObject:SetActive(not info.newText)
        cell.afterTxt.gameObject:SetActive(not info.newText)
        cell.addedTxt.gameObject:SetActive(not info.newText)
        cell.arrow.gameObject:SetActive(not info.newText)

        if info.newText then
            cell.newTxt.text = info.newText
        else
            cell.beforeTxt.text = info.oldValueShow or info.oldValue
            cell.afterTxt.text = info.newValueShow or info.newValue
            cell.addedTxt.text = string.format("+%d", info.newValue - info.oldValue)
        end
        cell.icon:LoadSprite(UIConst.UI_SPRITE_SPACESHIP_ROOM, info.icon)
    end)


    if isControlCenter then
        self.view.previewItemNode.gameObject:SetActive(false)
    else
        local newOutcomeItemIds = SpaceshipUtils.getRoomRecipeOutcomesByLv(self.m_roomId, self.m_curSelectedLv, true)
        self:_UpdateCommonItemList(self.view.previewItemNode, newOutcomeItemIds)
    end

    if self.m_curSelectedLv > roomInfo.lv then
        self:_UpdateCostItemList()
        self.view.bottomNode.gameObject:SetActive(true)
        if self.m_curSelectedLv == roomInfo.lv + 1 then

            if not self.m_roomInfo:CanLevelUp() then
                self.view.upgradeBtn.gameObject:SetActive(false)
                self.view.cantConfirmHintNode.gameObject:SetActive(true)
                self.view.cantConfirmHintNode.text.text = commonLvData.conditionDesc
            end
        else

            self.view.upgradeBtn.gameObject:SetActive(false)
            self.view.cantConfirmHintNode.gameObject:SetActive(true)
            self.view.cantConfirmHintNode.text.text = Language.LUA_SPACESHIP_ROOM_NEED_UNLOCK_PRE_LV
        end
    else

        self.view.costNode.gameObject:SetActive(false)
        self.view.bottomNode.gameObject:SetActive(false)
    end

    self.view.roomInfoTipNode.tipNode.gameObject:SetActive(false)
    self.view.roomInfoTipNode.tipTxt.text = self.m_roomTypeData.desc
    self.view.newTagImg.gameObject:SetActive(false)
end

SpaceshipRoomUpgradeCtrl._ChangeCurSelectedLv = HL.Method(HL.Number) << function(self, newLv)
    local roomInfo = self.m_roomInfo
    newLv = lume.clamp(newLv, 2, roomInfo.maxLv)
    self.m_curSelectedLv = newLv
    self:_RefreshUpgradeInfo()
end

SpaceshipRoomUpgradeCtrl._UpdateChangeLvNodeState = HL.Method() << function(self)
    local node = self.view.changeLvNode
    local roomInfo = self.m_roomInfo
    node.addBtn.interactable = self.m_curSelectedLv < roomInfo.maxLv
    node.addBtnStateController:SetState(self.m_curSelectedLv < roomInfo.maxLv and "Nrl" or "Dis")

    node.reduceBtn.interactable = self.m_curSelectedLv > (roomInfo.lv + 1) and self.m_curSelectedLv > 2
    node.reduceBtnStateController:SetState((self.m_curSelectedLv > (roomInfo.lv + 1) and self.m_curSelectedLv > 2) and "Nrl" or "Dis")

    node.title.gameObject:SetActive(self.m_curSelectedLv == (roomInfo.lv + 1))
    node.titleLvPreviewImg.gameObject:SetActive(self.m_curSelectedLv ~= (roomInfo.lv + 1))

    self:_UpdateChangeLvCell(node.leftLvCell, self.m_curSelectedLv - 1)
    self:_UpdateChangeLvCell(node.rightLvCell, self.m_curSelectedLv)
end

SpaceshipRoomUpgradeCtrl._UpdateChangeLvCell = HL.Method(HL.Table, HL.Number) << function(self, cell, lv)
    cell.text.text = lv
    cell.isCurHintNode.gameObject:SetActive(lv == self.m_roomInfo.lv)
    cell.image.enabled = lv > self.m_roomInfo.lv
end






SpaceshipRoomUpgradeCtrl._RefreshMaxInfo = HL.Method() << function(self)
    local node = self.view.maxNode
    local roomInfo = self.m_roomInfo
    node.lvDotNode:InitLvDotNode(roomInfo.lv, roomInfo.maxLv, UIUtils.getColorByString(self.m_roomTypeData.color))
    node.lvTxt.text = roomInfo.lv
    node.descTxt.text = self.m_roomTypeData.desc


    local effectInfos = SpaceshipUtils.getMaxUpgradeEffectInfos(self.m_roomId)
    if not node.m_finalEffectCells then
        node.m_finalEffectCells = UIUtils.genCellCache(node.finalEffectCell)
    end
    node.m_finalEffectCells:Refresh(#effectInfos, function(cell, index)
        local info = effectInfos[index]
        cell.nameTxt.text = info.name
        cell.icon:LoadSprite(UIConst.UI_SPRITE_SPACESHIP_ROOM, info.icon)
        cell.finalTxt.text = info.value
    end)

    local outcomeItemIds = SpaceshipUtils.getRoomRecipeOutcomesByLv(self.m_roomId, roomInfo.lv, false)
    self:_UpdateCommonItemList(node.formulaItemNode, outcomeItemIds)
    self.view.roomInfoTipNode.tipNode.gameObject:SetActive(false)
    self.view.roomInfoTipNode.tipTxt.text = self.m_roomTypeData.desc
    self.view.newTagImg.gameObject:SetActive(false)
    node.formulaItemNode.text.text = self.m_roomTypeData.newFormulaTitle
end







SpaceshipRoomUpgradeCtrl._OnPanelInputBlocked = HL.Override(HL.Boolean) << function(self, active)
    if DeviceInfo.usingController then
        self.view.changeLvNode.keyHintAdd.gameObject:SetActive(active)
        self.view.changeLvNode.keyHintReduce.gameObject:SetActive(active)
    end
end

SpaceshipRoomUpgradeCtrl._UpdateCommonItemList = HL.Method(HL.Table, HL.Opt(HL.Any)) << function(self, listNode, itemInfos)
    local count = itemInfos and #itemInfos or 0
    if count == 0 then
        listNode.gameObject:SetActive(false)
        return
    end
    listNode.gameObject:SetActive(true)
    if not listNode.itemCells then
        listNode.itemCells = UIUtils.genCellCache(listNode.item)
    end
    local isTable = type(itemInfos) == "table"
    if DeviceInfo.usingController then
        listNode.listSelectableNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
            if not isFocused then
                Notify(MessageConst.HIDE_ITEM_TIPS)
                listNode.list:ScrollTo(Vector2(0, 0), true)
            end
        end)
        local bindingText = self.m_state == States.Build and Language.ui_spaceship_roomupgrade_previewitemnode_text or self.m_roomTypeData.newFormulaTitle
        InputManagerInst:SetBindingText(listNode.listSelectableNaviGroup.FocusBindingId, bindingText)
    end

    listNode.itemCells:Refresh(#itemInfos, function(cell, index)
        local info = itemInfos[isTable and index or CSIndex(index)]
        local itemId

        if type(info) == "string" then
            itemId = info
            cell:InitItem({ id = itemId }, true)
        else
            cell:InitItem(info, true)
        end
        if DeviceInfo.usingController then
            cell:SetExtraInfo({
                isSideTips = true,
            })
        end
    end)
end

SpaceshipRoomUpgradeCtrl._UpdateCostItemList = HL.Method() << function(self)
    if not self.m_roomLvTable then
        return
    end
    local typeLvData = self.m_roomLvTable[self.m_curSelectedLv]
    local commonLvData = Tables.spaceshipRoomLvTable[typeLvData.id]
    local costItemInfos = commonLvData.costItems
    local node = self.view.costNode
    if DeviceInfo.usingController then
        node.costListSelectableNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
            if not isFocused then
                Notify(MessageConst.HIDE_ITEM_TIPS)
                node.costList:ScrollTo(Vector2(0, 0), true)
            end
        end)
    end
    local count = costItemInfos and #costItemInfos or 0
    self.m_isEnough = true
    if count == 0 then
        node.gameObject:SetActive(false)
    else
        node.gameObject:SetActive(true)
        if not node.itemCells then
            node.itemCells = UIUtils.genCellCache(node.item)
        end

        node.itemCells:Refresh(#costItemInfos, function(cell, index)
            local itemBundle = costItemInfos[CSIndex(index)]
            cell:InitItem(itemBundle, true)
            local ownCount = Utils.getItemCount(itemBundle.id, true, true)
            local isLack = ownCount < itemBundle.count
            local str = string.format("%s %s", Language.LUA_SAFE_AREA_ITEM_COUNT_LABEL, UIUtils.getNumString(ownCount))
            cell.view.ownCountTxt.text = UIUtils.setCountColor(str, isLack)
            cell:UpdateCountSimple(itemBundle.count, isLack)
            if isLack then
                self.m_isEnough = false
            end
            if DeviceInfo.usingController then
                cell:SetExtraInfo({
                    isSideTips = true,
                })
            end
        end)
    end

    if self.m_state == States.Build then
        self.view.buildBtn.gameObject:SetActive(self.m_isEnough)
    else
        self.view.upgradeBtn.gameObject:SetActive(self.m_isEnough)
    end

    self.view.cantConfirmHintNode.gameObject:SetActive(not self.m_isEnough)
    if not self.m_isEnough then
        self.view.cantConfirmHintNode.text.text = Language.LUA_ITEM_NOT_ENOUGH
    end
end

SpaceshipRoomUpgradeCtrl._OnClickConfirm = HL.Method() << function(self)
    if self:IsPlayingAnimationIn() then

        return
    end
    if self.m_state == States.Build then
        GameInstance.player.spaceship:BuildRoom(self.m_roomId, self.m_roomType)
    elseif self.m_state == States.Upgrade then
        GameInstance.player.spaceship:LevelUpRoom(self.m_roomId)
    end
end

SpaceshipRoomUpgradeCtrl._Exit = HL.Method(HL.Opt(HL.Boolean, HL.Boolean, HL.Boolean)) << function(self, needDialog, needClosePanel, forceClose)

    if not PhaseManager:CanPopPhase(PHASE_ID) then
        return
    end

    local dialogId
    local roomId = self.m_roomId
    if needDialog then
        local lv = self.m_state == States.Upgrade and self.m_roomInfo.lv or 1
        local typeLvData = self.m_roomLvTable[lv]
        local commonLvData = Tables.spaceshipRoomLvTable[typeLvData.id]
        dialogId = commonLvData.upgradeDialogId
    end
    if string.isEmpty(dialogId) then
        if self.m_moveCam and not self.m_isRemoteCamera then
            local clearScreenKey = GameInstance.player.spaceship:UndoMoveCamToSpaceshipRoom(roomId, self.m_isRemoteCamera)
            if clearScreenKey and clearScreenKey ~= -1 then
                UIManager:RecoverScreen(clearScreenKey)
            end
            if self.m_clearScreenKey ~= -1 then
                if self.m_clearScreenKey ~= clearScreenKey then
                    UIManager:RecoverScreen(self.m_clearScreenKey)
                end
                self.m_clearScreenKey = -1
            end
            self.m_moveCam = false
        end
        PhaseManager:PopPhase(PHASE_ID)
        return
    end
    if (self.m_moveCam and needClosePanel) or forceClose then
        local clearScreenKey = GameInstance.player.spaceship:UndoMoveCamToSpaceshipRoom(roomId, self.m_isRemoteCamera)
        if clearScreenKey and clearScreenKey ~= -1 then
            UIManager:RecoverScreen(clearScreenKey)
        end
        if self.m_clearScreenKey ~= -1 then
            if self.m_clearScreenKey ~= clearScreenKey then
                UIManager:RecoverScreen(self.m_clearScreenKey)
            end
            self.m_clearScreenKey = -1
        end
        self.m_moveCam = false
        PhaseManager:ExitPhaseFast(PHASE_ID)
    end
    SpaceshipUtils.playSSDialog(self.m_roomId, dialogId)
end




HL.Commit(SpaceshipRoomUpgradeCtrl)
