local SocialBuildingSource = CS.Beyond.Gameplay.Factory.SocialBuildingSource

local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

BuildingCommon = HL.Class('BuildingCommon', UIWidgetBase)

local BUILDING_BOTTOM_BG_NAME = "bg_machine_base_%d"



BuildingCommon.nodeId = HL.Field(HL.Number) << -1

BuildingCommon.buildingId = HL.Field(HL.String) << ""

BuildingCommon.buildingUiInfo = HL.Field(CS.Beyond.Gameplay.RemoteFactory.NodeUIInfo)

BuildingCommon.buildingItemId = HL.Field(HL.String) << ""

BuildingCommon.lastState = HL.Field(GEnums.FacBuildingState)

BuildingCommon.m_stateGoNodeMap = HL.Field(HL.Table)

BuildingCommon.bgRatio = HL.Field(HL.Number) << -1

BuildingCommon.m_arg = HL.Field(HL.Table)

BuildingCommon.m_social = HL.Field(CS.Beyond.Gameplay.RemoteFactory.ServerChapterInfo.ComponentHandler.Payload_Social)

BuildingCommon.m_socialBuildingSource = HL.Field(CS.Beyond.Gameplay.Factory.SocialBuildingSource)

BuildingCommon.m_showPower = HL.Field(HL.Boolean) << false

BuildingCommon.smartAlertDynamicNode = HL.Field(HL.Any)

BuildingCommon.m_smartAlertUpdate = HL.Field(HL.Number) << -1

BuildingCommon.m_smartAlertTypeIndex = HL.Field(HL.Number) << 1

BuildingCommon.m_smartAlertCheckedInPeriod = HL.Field(HL.Boolean) << false

BuildingCommon.smartAlertChangeCachePauseUpdate = HL.Field(HL.Boolean) << false

BuildingCommon.isModeEnvRelated = HL.Field(HL.Boolean) << false

BuildingCommon.currEnvState = HL.Field(HL.Number) << -1





BuildingCommon._OnFirstTimeInit = HL.Override() << function(self)
    self.view.closeButton.onClick:AddListener(function()
        self:Close()
    end)
    self.view.wikiButton.onClick:AddListener(function()

        local args = {}
        if not string.isEmpty(self.buildingItemId) then
            args.itemId = self.buildingItemId
        else
            args.buildingId = self.buildingId
        end
        Notify(MessageConst.SHOW_WIKI_ENTRY, args)
    end)
    self.view.moveButton.onClick:AddListener(function()
        self:_MoveBuilding()
    end)
    self.view.delButton.onClick:AddListener(function()
        self:_DelBuilding()
    end)
    self.view.forbiddenMoveButton.onClick:AddListener(function()
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_FORBID_MOVE_OR_DELETE)
    end)
    self.view.forbiddenDelButton.onClick:AddListener(function()
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_FORBID_MOVE_OR_DELETE)
    end)
    self.view.shareButton.onClick:AddListener(function()
        self:_ShareBuilding()
    end)
    self.view.delSocialButton.onClick:AddListener(function()
        self:_DelBuilding()
    end)
    self.view.sourceButton.onClick:AddListener(function()
        self:_ShowBuildingSource()
    end)
    self.view.reportButton.onClick:AddListener(function()
        self:_ReportBuilding()
    end)
    self.view.likeButton.onClick:AddListener(function()
        self:_LikeBuilding()
    end)

    self:RegisterMessage(MessageConst.FAC_ON_MOVE_SHOW_CONTROLLER_MODE_HINT, function(text)
        if text ~= nil then
            self.view.controllerModeHint.gameObject:SetActive(true)
            self.view.controllerModeHint.hintTxt.text = text
        end
    end)
    self:RegisterMessage(MessageConst.FAC_ON_MOVE_HIDE_CONTROLLER_MODE_HINT, function()
        self.view.controllerModeHint.gameObject:SetActive(false)
    end)
end

BuildingCommon.InitBuildingCommon = HL.Method(HL.Opt(CS.Beyond.Gameplay.RemoteFactory.NodeUIInfo, HL.Table)) <<
function(self, uiInfo, arg)
    self:_FirstTimeInit()

    local hasUiInfo = uiInfo ~= nil
    arg = arg or {}
    self.m_arg = arg

    local data
    if hasUiInfo then
        self.buildingUiInfo = uiInfo
        self.nodeId = uiInfo.nodeId
        self.buildingId = uiInfo.buildingId
        data = Tables.factoryBuildingTable:GetValue(self.buildingId)

        self.view.powerToggle:InitCommonToggle(function(isOn)
            self:_OnToggleBuildingPower(isOn)
        end, self.buildingUiInfo.isActive, true)

        self:_UpdateBuildingState(true)
        self:_StartCoroutine(function()
            while true do
                coroutine.step()
                self:_UpdateBuildingState()
                self:_UpdateCurrentEnvironmentState()
            end
        end)
        if self.m_arg.smartAlertFuncNameList and
            self.m_arg.targetCtrlInstance and
            CS.Beyond.GameSetting.otherShowSmartAlert and
            not Utils.isInBlackbox() then
            if self.smartAlertDynamicNode == nil then
                local saPrefab = self:LoadGameObject(FacConst.FAC_SMARTALERT_PREFAB_PATH)
                local saObj = CSUtils.CreateObject(saPrefab, self.transform)
                saObj:SetActive(false)
                self.smartAlertDynamicNode = Utils.wrapLuaNode(saObj)
            end
            self.smartAlertDynamicNode:InitMachineSmartAlertNode(uiInfo.buildingId, uiInfo.nodeId)
            self.m_smartAlertUpdate = LuaUpdate:Add("Tick", function(deltaTime)
                self:_UpdateSmartAlert(deltaTime)
            end)
        end

        self.m_social, self.m_socialBuildingSource = FactoryUtils.getSocialBuildingDetails(uiInfo.nodeId)

        self.m_showPower = data.powerConsume > 0
            and self.m_socialBuildingSource ~= SocialBuildingSource.Others
            and not GameInstance.remoteFactoryManager.system.core.progressStatus:IsBuildingPowerCostNoNeed(uiInfo.buildingId)

        self:_InitBuildingBG(uiInfo.nodeId, data.bgOnPanel)
        self:_InitBuildingOperateButtonState(uiInfo.nodeId)
    else
        data = arg.data
        self.buildingItemId = data.itemId
        self.view.moveButton.gameObject:SetActive(false)
        self.view.delButton.gameObject:SetActive(false)
        self.view.leftButtonDecoLine.gameObject:SetActive(false)
        self.view.shareButton.gameObject:SetActive(false)
        self.view.delSocialButton.gameObject:SetActive(false)
        self.view.leftButtonDecoLine2.gameObject:SetActive(false)
        self.view.powerToggle.gameObject:SetActive(false)
        self.m_showPower = false
        self:_InitBuildingCustomButtons()
        if data.nodeId ~= nil then
            self.m_social, self.m_socialBuildingSource = FactoryUtils.getSocialBuildingDetails(data.nodeId)

            self:_InitBuildingOperateButtonState(data.nodeId)
        end
    end
    if self.view.earlyAccessNode ~= nil then
        if Utils.isInBlackbox() or self.m_socialBuildingSource == SocialBuildingSource.Others then
            self.view.earlyAccessNode.gameObject:SetActiveIfNecessary(false)
        else
            local targetBuildingId = self.buildingId
            if string.isEmpty(targetBuildingId) and data.nodeId ~= nil then
                local hander = FactoryUtils.getBuildingNodeHandler(data.nodeId)
                targetBuildingId = hander and hander.templateId or ""
            end
            self.view.earlyAccessNode.gameObject:SetActiveIfNecessary(
                not string.isEmpty(targetBuildingId) and FactoryUtils.isSkipUnlockedBuilding(targetBuildingId))
        end
    end
    self:_InitBuildingDescription()

    if self.m_showPower then
        self.view.powerNode.gameObject:SetActive(true)
        self.view.decoLine.gameObject:SetActive(true)
        local powerCost = FactoryUtils.getCurBuildingConsumePower(self.nodeId)
        self.view.powerText.text = powerCost
    else
        self.view.powerNode.gameObject:SetActive(false)
        self.view.decoLine.gameObject:SetActive(false)
    end

    self:UpdateBasicInfo(data)

    self:_UpdateSocialInfo()

    local ctrl = self:GetUICtrl()
    ctrl.view.controllerHintPlaceholder = self.view.controllerHintPlaceholder
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ctrl.view.inputGroup.groupId})

    local extraBtnInfos = {}










    if ctrl.view.formulaNode then
        table.insert(extraBtnInfos, {
            button = ctrl.view.formulaNode.view.openBtn,
            sprite = ctrl.view.formulaNode.view.openBtnIcon.sprite,
            textId = "key_hint_fac_machine_toggle_formula",
            priority = 2.1,
        })
    end
    self.view.controllerSideMenuBtn:InitControllerSideMenuBtn({
        extraBtnInfos = extraBtnInfos,
    })

    self:_InitBuildingSwitchAreaKeyHintState()

    self:_UpdateCurrentEnvironmentState()
end

BuildingCommon.UpdateBasicInfo = HL.Method(HL.Any) << function(self, data)
    local machineName = data.name
    if self.m_arg.nameIndex ~= nil then
        self.view.machineName:CombineStringWithLanguageSpilt(machineName, string.format("%d", self.m_arg.nameIndex))
    else
        self.view.machineName.text = machineName
    end
    self.view.machineIcon:LoadSprite(UIConst.UI_SPRITE_FAC_BUILDING_PANEL_ICON, data.iconOnPanel)

    if self.m_showPower then
        self.view.powerNode.gameObject:SetActive(data.needPower)
        self.view.powerNodeNeedPower.gameObject:SetActive(not data.needPower)
    end
end

BuildingCommon._UpdateSocialInfo = HL.Method() << function(self)
    local source = self.m_socialBuildingSource
    if source == SocialBuildingSource.Invalid then
        self.view.socialNode.gameObject:SetActive(false)
    else
        self.view.socialNode.gameObject:SetActive(true)
        local isMine = source == SocialBuildingSource.Mine
        if isMine then

            self.view.mineSocialNode.gameObject:SetActive(true)
            self.view.othersSocialNode.gameObject:SetActive(false)

            self.view.likeText.text = self.m_social.like
        else

            self.view.mineSocialNode.gameObject:SetActive(false)
            self.view.othersSocialNode.gameObject:SetActive(true)

            local canReport = FactoryUtils.canReportSocialBuilding(self.nodeId)
            self.view.reportButton.gameObject:SetActive(canReport)

            local isLiked = FactoryUtils.isLikedSocialBuilding(self.nodeId)
            self.view.likeButtonStateCtrl:SetState(isLiked and "Liked" or "NotLiked")

            local stability = FactoryUtils.getSocialBuildingStability(self.nodeId)
            self.view.stabilityImage.fillAmount = stability
        end
    end
end

BuildingCommon._OnToggleBuildingPower = HL.Method(HL.Boolean) << function(self, isOn)
    if isOn == self.buildingUiInfo.isActive then
        return
    end
    GameInstance.mobileMotionManager:PostEventCommonShort()
    self.buildingUiInfo.sender:Message_OpEnableNode(Utils.getCurrentChapterId(), self.nodeId, isOn, function()
        if IsNull(self.view.gameObject) then
            return
        end
        if self.m_showPower then
            self.view.powerText.text = FactoryUtils.getCurBuildingConsumePower(self.nodeId)
        end
        if self.m_arg.onPowerChanged then
            self.m_arg.onPowerChanged(isOn)
        end
    end)
end

BuildingCommon._UpdateBuildingState = HL.Method(HL.Opt(HL.Boolean)) << function(self, forceUpdate)
    local state = FactoryUtils.getBuildingStateType(self.nodeId)
    if not forceUpdate and state == self.lastState then
        return
    end

    if self.m_stateGoNodeMap == nil then
        self.m_stateGoNodeMap = {}
    end

    self:_RefreshBuildingStateDisplay(state)
    self.lastState = state
end

BuildingCommon._UpdateSmartAlert = HL.Method(HL.Number) << function(self, detlaTime)


    if self.smartAlertChangeCachePauseUpdate or not self.gameObject.activeInHierarchy then
        return
    end


    local periodCount = #self.m_arg.smartAlertFuncNameList + 1
    if self.m_smartAlertCheckedInPeriod then

        self.smartAlertDynamicNode:UpdateSmartAlertState(detlaTime)
        if self.m_smartAlertTypeIndex >= periodCount then

            self.m_smartAlertCheckedInPeriod = false
        end
    else
        if self.m_smartAlertTypeIndex >= periodCount then

            local alertInfo = { condition = GEnums.FacSmartAlertType.DoNotShow }
            self.smartAlertDynamicNode:UpdateSmartAlertState(detlaTime, alertInfo)
        else

            local funcName = self.m_arg.smartAlertFuncNameList[self.m_smartAlertTypeIndex]
            local targetCtrl = self.m_arg.targetCtrlInstance
            local success, alertInfo = targetCtrl[funcName](targetCtrl, self.lastState)
            self.m_smartAlertCheckedInPeriod = success
            self.smartAlertDynamicNode:UpdateSmartAlertState(detlaTime, alertInfo)
        end
    end
    self.m_smartAlertTypeIndex = (self.m_smartAlertTypeIndex % periodCount) + 1
end

BuildingCommon._RefreshBuildingStateDisplay = HL.Method(GEnums.FacBuildingState) << function(self, state)
    if self.m_stateGoNodeMap[self.lastState] ~= nil then
        self.m_stateGoNodeMap[self.lastState]:SetActive(false)
    end

    local showState = state

    if not self.config.HIDE_STATE_NODE then
        if self.m_stateGoNodeMap[showState] == nil then
            if FacConst.FAC_BUILDING_STATE_TO_PREFAB_PATH[showState] ~= nil then
                local statePrefab = self:LoadGameObject(FacConst.FAC_BUILDING_STATE_TO_PREFAB_PATH[showState])
                local stateObj = CSUtils.CreateObject(statePrefab, self.view.stateNode)
                self.m_stateGoNodeMap[showState] = stateObj

                if showState == GEnums.FacBuildingState.Normal then
                    local _, data, customTextKey
                    if string.isEmpty(self.buildingId) then
                        local buildingId = FactoryUtils.getItemBuildingId(self.buildingItemId)
                        if buildingId ~= nil then
                            _, data = Tables.factoryBuildingTable:TryGetValue(buildingId)
                        end
                    else
                        _, data = Tables.factoryBuildingTable:TryGetValue(self.buildingId)
                    end
                    if data ~= nil then
                        customTextKey = FacConst.FAC_BUILDING_NORMAL_STATE_CUSTOM_TEXT_ID[data.type]
                    else
                        customTextKey = FacConst.FAC_NON_BUILDING_NORMAL_STATE_CUSTOM_TEXT_ID[self.buildingItemId]
                    end
                    if customTextKey ~= nil then
                        local text = stateObj.transform:Find("Info/Text"):GetComponent("UIText")
                        text.text = Language[customTextKey]
                    end
                end
            end
        else
            self.m_stateGoNodeMap[showState]:SetActive(true)
        end
    end

    if self.m_arg.onStateChanged then
        self.m_arg.onStateChanged(state)
    end
end

BuildingCommon._MoveBuilding = HL.Method() << function(self)
    local nodeId = self.nodeId
    if not FactoryUtils.canMoveBuilding(nodeId) then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FACTORY_BUILDING_MOVE_NOT_ALLOWED)
        return
    end
    self:Close(true)
    Notify(MessageConst.FAC_ENTER_BUILDING_MODE, {
        nodeId = nodeId
    })
end

BuildingCommon._DelBuilding = HL.Method() << function(self)
    if not FactoryUtils.canDelBuilding(self.nodeId, true) then
        return
    end
    self:Close(true)
    local hintTxt
    local isOthersSocialBuilding = FactoryUtils.isOthersSocialBuilding(self.nodeId)
    if isOthersSocialBuilding then

        hintTxt = Language.LUA_FAC_DEL_SOCIAL_BUILDING_CONFIRM
    else
        local data = Tables.factoryBuildingTable:GetValue(self.buildingId)
        if data ~= nil then
            hintTxt = data.delConfirmText
        end
    end
    FactoryUtils.delBuilding(self.nodeId, nil, false, hintTxt)
end

BuildingCommon._ShareBuilding = HL.Method() << function(self)
    if not FriendUtils.canShareBuilding() then
        return
    end
    if not FactoryUtils.canShareBuilding(self.nodeId) then
        return
    end
    local state = FactoryUtils.getBuildingStateType(self.nodeId)
    if state ~= GEnums.FacBuildingState.Normal then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_SHARE_SOCIAL_BUILDING_STATE_NOT_NORMAL)
        return
    end

    local chapterId = Utils.getCurrentChapterId()
    local nodeId = self.nodeId

    local customCheckFriend
    local fromOtherPlayer = self.m_socialBuildingSource == SocialBuildingSource.Others and not self.m_social.preset
    if fromOtherPlayer then
        local ownerId = self.m_social.ownerId
        customCheckFriend = function(friendInfo)
            return friendInfo.roleId ~= ownerId
        end
    end
    UIManager:Open(PanelId.FriendRequest, {
        customCheckFriend = customCheckFriend,
        onShareClick = function(roleId)
            GameInstance.player.friendChatSystem:SendChatSocialBuilding(roleId, chapterId, nodeId, function()
                Notify(MessageConst.SHOW_TOAST, Language.LUA_FRIEND_SHARE_SOCIAL_BUILDING_SUCCESS)
                UIManager:Close(PanelId.FriendRequest)
                self:Close()
                PhaseManager:OpenPhase(PhaseId.SNS, { roleId = roleId }, nil, true)
            end)
        end,
    })
end

BuildingCommon._ShowBuildingSource = HL.Method() << function(self)
    if self.m_socialBuildingSource ~= SocialBuildingSource.Others then
        return
    end

    local ownerId = self.m_social.ownerId
    if self.m_social.preset then

        local success, npcData = Tables.factorySocialBuildingNpcTable:TryGetValue(ownerId)
        if not success then
            logger.error("[SocialBuilding] ShowSource: Npc data not found, npcId: " .. tostring(ownerId))
            return
        end
        UIManager:Open(PanelId.FacSourceDetails, { nodeId = self.nodeId, npcData = npcData })
    else

        GameInstance.player.friendSystem:SyncSocialFriendInfo({ ownerId }, function()
            if self.m_isDestroyed then
                return
            end
            local success, ownerInfo = GameInstance.player.friendSystem:TryGetFriendInfo(ownerId)
            if not success then

                logger.info("[SocialBuilding] ShowSource: Owner info not found, roleId: " .. tostring(ownerId))
            end
            UIManager:Open(PanelId.FacSourceDetails, { nodeId = self.nodeId, ownerInfo = ownerInfo })
        end)
    end
end

BuildingCommon._ReportBuilding = HL.Method() << function(self)
    FactoryUtils.reportSocialBuilding(self.nodeId, function()
        return not self.m_isDestroyed
    end)
end

BuildingCommon._LikeBuilding = HL.Method() << function(self)
    FactoryUtils.likeSocialBuilding(self.nodeId, function()
        if self.m_isDestroyed then
            return
        end
        AudioAdapter.PostEvent("Au_UI_Button_SocialRepair")
        self.view.socialNodeAnimationWrapper:PlayInAnimation(function()
            self:_UpdateSocialInfo()
        end)
    end)
end

BuildingCommon._InitBuildingBG = HL.Method(HL.Number, HL.String) << function(self, buildingNodeId, buildingBgId)
    local inPortInfoList, outPortInfoList =  FactoryUtils.getBuildingPortState(buildingNodeId, false)
    if inPortInfoList == nil or outPortInfoList == nil then
        return
    end

    if not string.isEmpty(buildingBgId) and not self.view.config.USE_CUSTOM_BG then
        local inPortCount, outPortCount = #inPortInfoList, #outPortInfoList
        if not GameInstance.remoteFactoryManager.unlockSystem.systemUnlockedBelt or
            not GameInstance.remoteFactoryManager:IsWorldPositionInMainRegion(self.buildingUiInfo.nodeHandler.transform.worldPosition) then
            inPortCount, outPortCount = 0, 0
        end
        local inBottomBGName = string.format(BUILDING_BOTTOM_BG_NAME, inPortCount)
        local outBottomBGName = string.format(BUILDING_BOTTOM_BG_NAME, outPortCount)

        self.view.leftPart:LoadSprite(UIConst.UI_SPRITE_FAC_MACHINE_BG, inBottomBGName)
        self.view.rightPart:LoadSprite(UIConst.UI_SPRITE_FAC_MACHINE_BG, outBottomBGName)

        local buildingBg = self:LoadSprite(UIConst.UI_SPRITE_FAC_MACHINE_BG, buildingBgId)
        if buildingBg ~= nil then
            self.view.machineBg.sprite = buildingBg

            if self.view.config.NEED_RESIZE_BOTTOM_BG then
                local originalWidth = self.view.machineBgRect.rect.width
                self.view.machineBg:SetNativeSize()
                local currentWidth = self.view.machineBgRect.rect.width
                if currentWidth ~= originalWidth then

                    local ratio = currentWidth / originalWidth * self.view.config.MACHINE_TO_BOTTOM_RATIO
                    local originalBottomSize = self.view.bottomBgRect.sizeDelta
                    self.view.bottomBgRect.sizeDelta = Vector2(originalBottomSize.x * ratio, originalBottomSize.y)
                    local originalPartSize = self.view.leftPartRect.sizeDelta
                    self.view.leftPartRect.sizeDelta = Vector2(originalPartSize.x * ratio, originalPartSize.y)
                    self.view.rightPartRect.sizeDelta = Vector2(originalPartSize.x * ratio, originalPartSize.y)
                    self.bgRatio = ratio
                end
            end
        end
    end
end

BuildingCommon._InitBuildingDescription = HL.Method() << function(self)
    if self.config.SHOW_BUILDING_DESCRIPTION then
        self.view.machineDescNode.gameObject:SetActiveIfNecessary(true)
        if string.isEmpty(self.buildingId) then
            local itemData = Tables.itemTable:GetValue(self.buildingItemId)
            if itemData ~= nil then
                self.view.descText.text = itemData.desc
            end
        else
            local buildingData = Tables.factoryBuildingTable[self.buildingId]
            self.view.descText.text = buildingData.desc
        end
    else
        self.view.machineDescNode.gameObject:SetActiveIfNecessary(false)
    end
end

BuildingCommon._InitBuildingOperateButtonState = HL.Method(HL.Number) << function(self, nodeId)
    local isOthersSocialBuilding = self.m_socialBuildingSource == SocialBuildingSource.Others

    local canMove, canDel = FactoryUtils.canMoveBuilding(nodeId), FactoryUtils.canDelBuilding(nodeId)
    local needMoveBtn = self.view.moveButton.gameObject.activeSelf
        and not isOthersSocialBuilding
    local needDelBtn = self.view.delButton.gameObject.activeSelf
        and not isOthersSocialBuilding
    self.view.moveButton.gameObject:SetActive(needMoveBtn and canMove)
    self.view.forbiddenMoveButton.gameObject:SetActive(needMoveBtn and not canMove)
    self.view.delButton.gameObject:SetActive(needDelBtn and canDel)
    self.view.forbiddenDelButton.gameObject:SetActive(needDelBtn and not canDel)
    self.view.leftButtonDecoLine.gameObject:SetActive(needDelBtn and needMoveBtn)


    local showShareBtn = FactoryUtils.canShareBuilding(nodeId)
    local needDelSocialBtn = isOthersSocialBuilding
    self.view.shareButton.gameObject:SetActive(showShareBtn)
    self.view.delSocialButton.gameObject:SetActive(needDelSocialBtn and canDel)

    local showDecoLine2 = showShareBtn
    self.view.leftButtonDecoLine2.gameObject:SetActive(showDecoLine2)
end

BuildingCommon._InitBuildingCustomButtons = HL.Method() << function(self)
    local leftButtonValid, rightButtonValid = false, false
    if self.m_arg.customLeftButtonOnClicked ~= nil then
        self.view.moveButton.onClick:RemoveAllListeners()
        self.view.moveButton.onClick:AddListener(function()
            self.m_arg.customLeftButtonOnClicked()
        end)
        self.view.moveButton.gameObject:SetActive(true)
        leftButtonValid = true
    end
    if self.m_arg.customRightButtonOnClicked ~= nil then
        self.view.delButton.onClick:RemoveAllListeners()
        self.view.delButton.onClick:AddListener(function()
            self.m_arg.customRightButtonOnClicked()
        end)
        self.view.delButton.gameObject:SetActive(true)
        rightButtonValid = true
    end
    self.view.leftButtonDecoLine.gameObject:SetActive(leftButtonValid and rightButtonValid)
end

BuildingCommon._InitBuildingSwitchAreaKeyHintState = HL.Method() << function(self)
    local needShowHint = self.config.SHOW_SWITCH_AREA_KEY_HINT
    if not needShowHint then
        self.view.switchAreaKeyHint.overrideValidState = CS.Beyond.UI.CustomUIStyle.OverrideValidState.ForceNotValid
    end
end

BuildingCommon._UpdateCurrentEnvironmentState = HL.Method() << function(self)
    if self.buildingUiInfo == nil or not self.isModeEnvRelated then
        self.view.envStateController.gameObject:SetActive(false)
        return
    end

    local currEnv, envCompValid = FactoryUtils.getBuildingCurrentEnvironment(self.buildingUiInfo.nodeHandler)
    if not envCompValid then
        self.view.envStateController.gameObject:SetActive(false)
        return
    end
    if currEnv == self.currEnvState then
        return
    end

    local envEnum = Utils.intToEnum(typeof(GEnums.FacEnvGenEnvType), currEnv)
    self.view.envStateController.gameObject:SetActive(true)
    self.view.envStateController:SetState(envEnum:ToString())

    if self.m_arg.onEnvChanged then
        self.m_arg.onEnvChanged(currEnv)
    end

    self.currEnvState = currEnv
end

BuildingCommon.Close = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipAnim)
    if not skipAnim then
        if PhaseManager:GetTopPhaseId() == PhaseId.FacMachine then
            PhaseManager:PopPhase(PhaseId.FacMachine)
        end
    else
        PhaseManager:ExitPhaseFast(PhaseId.FacMachine)
    end
end

BuildingCommon.ClearSmartAlertUpdate = HL.Method() << function(self)
    if self.m_arg.smartAlertFuncNameList and
        self.m_arg.targetCtrlInstance and
        CS.Beyond.GameSetting.otherShowSmartAlert and
        not Utils.isInBlackbox() then
        self.m_smartAlertUpdate = LuaUpdate:Remove(self.m_smartAlertUpdate)
        if self.smartAlertDynamicNode ~= nil then
            self.smartAlertDynamicNode:RestoreAlertState()
        end
    end
end

BuildingCommon.ChangeBuildingStateDisplay = HL.Method(GEnums.FacBuildingState) << function(self, state)
    if state == nil then
        return
    end

    if state == self.lastState then
        return
    end

    if self.m_stateGoNodeMap == nil then
        self.m_stateGoNodeMap = {}
    end


    self:_RefreshBuildingStateDisplay(state)

    self.lastState = state
end

BuildingCommon.ShareBuilding = HL.Method() << function(self)
    self:_ShareBuilding()
end

BuildingCommon.ShowBuildingSource = HL.Method() << function(self)
    self:_ShowBuildingSource()
end

HL.Commit(BuildingCommon)
return BuildingCommon
