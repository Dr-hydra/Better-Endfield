local InteractOptionIdentifier = CS.Beyond.Gameplay.Core.InteractOptionIdentifier
local InteractOptionType = CS.Beyond.Gameplay.Core.InteractOptionType

local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.InteractOption
InteractOptionCtrl = HL.Class('InteractOptionCtrl', uiCtrl.UICtrl)





InteractOptionCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ADD_INTERACT_OPTION] = 'AddInteractOption',
    [MessageConst.UPDATE_INTERACT_OPTION] = 'UpdateInteractOption',
    [MessageConst.REMOVE_INTERACT_OPTION] = 'RemoveInteractOption',

    [MessageConst.ADD_INTERACT_OPTIONS] = 'AddInteractOptions',
    [MessageConst.UPDATE_INTERACT_OPTIONS] = 'UpdateInteractOptions',
    [MessageConst.REMOVE_INTERACT_OPTIONS] = 'RemoveInteractOptions',

    [MessageConst.ON_SCENE_LOAD_START] = 'ClearUIOptions',
    [MessageConst.ON_MAINCHAR_ENTER_MUD] = 'RefreshActiveState',
    [MessageConst.ON_MAINCHAR_LEAVE_MUD] = 'RefreshActiveState',
    [MessageConst.ON_SQUAD_INFIGHT_CHANGED] = 'RefreshActiveState',

    [MessageConst.CAMERA_HIDE_HUD] = 'CameraHideHud',
    [MessageConst.CAMERA_END_HIDE_HUD] = 'CameraEndHideHud',

    [MessageConst.ON_FAC_DESTROY_MODE_CHANGE] = 'OnFacDestroyModeChange',

    [MessageConst.TOGGLE_HIDE_INTERACT_OPTION_LIST] = 'OnToggleHideInteractOptionList',

    [MessageConst.FOCUS_ON_INTERACT_OPTION] = 'FocusOnInteractOption',
}

local MAX_DISPLAY_COUNT = 999



InteractOptionCtrl.m_optionInfoMap = HL.Field(HL.Table)




InteractOptionCtrl.m_curShowingOptInfoList = HL.Field(HL.Table)

InteractOptionCtrl.m_curShowingOptCount = HL.Field(HL.Number) << 0

InteractOptionCtrl.m_curSelectedOptIdentifier = HL.Field(HL.String) << ""

InteractOptionCtrl.m_cellObjCache = HL.Field(HL.Forward("CommonCache"))

InteractOptionCtrl.m_obj2CellMap = HL.Field(HL.Table)

InteractOptionCtrl.m_playingOutInfoTimers = HL.Field(HL.Table)

InteractOptionCtrl.m_viewGroupConfig = HL.Field(HL.Table)

InteractOptionCtrl.m_sortFunc = HL.Field(HL.Function)





InteractOptionCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    UIUtils.bindInputPlayerAction("common_interact", function()
        if self.m_curSelectedOptIdentifier ~= self.m_pressStartIdentifier and not InputManager.instance.guideUseActionIds:Contains("common_interact") then

            return
        end
        self.m_pressStartIdentifier = nil
        local info = self.m_optionInfoMap[self.m_curSelectedOptIdentifier]
        if info and self.view.listNode.gameObject.activeSelf then
            if not info.identifier.isPickable and info.longPressAction == nil then
                self:_OnClickOption(self.m_curSelectedOptIdentifier)
                return
            end
        end
    end, self.view.listNodeInputBindingGroupMonoTarget.groupId)

    self.m_optionInfoMap = {}
    self.m_curShowingOptInfoList = {}
    self.m_hideListKeys = {}
    self.m_sortFunc = Utils.genSortFunction({
        "isItemOrder",
        "typeOrder",
        "sortId",
        "seqNum",
        "sourceId",
        "subIndex"
    }, true)

    local CommonCache = require_ex("Common/Utils/CommonCache")
    self.m_cellObjCache = CommonCache(function()
        local obj = CSUtils.CreateObject(self.view.optionItem.gameObject, self.view.listContainer.transform)
        return obj
    end)
    self.view.optionItem.gameObject:SetActive(false)
    self.m_obj2CellMap = {}
    self.m_playingOutInfoTimers = {}

    self.m_viewGroupConfig = InteractOptionConst.INTERACT_OPTION_VIEW_GROUP_CONFIG

    self:BindInputPlayerAction("interact_option_next", function()
        self:_OnScroll(-1)
    end)
    self:BindInputPlayerAction("interact_option_before", function()
        self:_OnScroll(1)
    end)
    self:BindInputPlayerAction("common_interact_pickup_all_start", function()
        self:_OnPressOptionStart(true)
    end)
    self:BindInputPlayerAction("common_interact_pickup_all_end", function()
        self:_OnPressOptionEnd(true)
    end)

    self:_RefreshScrollHint()
    self:RefreshActiveState()
    self.view.btnHint.gameObject:SetActive(false)
end


InteractOptionCtrl.m_updateCor = HL.Field(HL.Thread)

InteractOptionCtrl.m_needUpdateList = HL.Field(HL.Boolean) << false

InteractOptionCtrl.m_nextUpdateNeedToTop = HL.Field(HL.Boolean) << true

InteractOptionCtrl._TryUpdateListTick = HL.Method() << function(self)
    if self.m_needUpdateList then
        self:_UpdateCurShowingList(self.m_nextUpdateNeedToTop)
        self:_ScrollTo(self.m_curShowingOptCount)
    end
    self:_UpdateBtnHint()
end




InteractOptionCtrl.OnShow = HL.Override() << function(self)
    self:_RefreshList()
    self:_Register()
end

InteractOptionCtrl.OnHide = HL.Override() << function(self)
    Notify(MessageConst.ON_TOGGLE_INTERACT_OPTION_SCROLL, false)
    self:_ClearRegister()
    self:_InterruptPickupAll()
end

InteractOptionCtrl.OnClose = HL.Override() << function(self)
    Notify(MessageConst.ON_TOGGLE_INTERACT_OPTION_SCROLL, false)
    self:_ClearRegister()
    self:_InterruptPickupAll()
end

InteractOptionCtrl.m_onScroll = HL.Field(HL.Function)

InteractOptionCtrl._Register = HL.Method() << function(self)
    local touchPanel = UIManager.commonTouchPanel
    if not self.m_onScroll then
        self.m_onScroll = function(delta)
            self:_OnScroll(delta)
        end
    end
    touchPanel.onScroll:AddListener(self.m_onScroll)

    self.m_optionClickedThisTick = false
    self.m_updateCor = self:_StartCoroutine(function()
        while true do
            coroutine.step()
            self:_TryUpdateListTick()
            self.m_optionClickedThisTick = false
        end
    end)
end

InteractOptionCtrl._ClearRegister = HL.Method() << function(self)
    local touchPanel = UIManager.commonTouchPanel
    touchPanel.onScroll:RemoveListener(self.m_onScroll)
    self.m_updateCor = self:_ClearCoroutine(self.m_updateCor)
end






InteractOptionCtrl.OnFacDestroyModeChange = HL.Method(HL.Boolean) << function(self, inDestroyMode)
    self:_ClearShowingUIOptions()
    self:_UpdateCurShowingList(true)
end






InteractOptionCtrl.m_hideListKeys = HL.Field(HL.Table)

InteractOptionCtrl.OnToggleHideInteractOptionList = HL.Method(HL.Table) << function(self, args)
    local key, hide = unpack(args)
    self:ToggleHideInteractOptionList(key, hide)
end

InteractOptionCtrl.ToggleHideInteractOptionList = HL.Method(HL.String, HL.Boolean) << function(self, key, hide)
    if hide then
        self.m_hideListKeys[key] = true
    else
        self.m_hideListKeys[key] = nil
    end
    self:RefreshActiveState()
end

InteractOptionCtrl.RefreshActiveState = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    local shouldHide = next(self.m_hideListKeys)

    if shouldHide then
        UIManager:HideWithKey(PANEL_ID, "ToggleHideInteractOptionList")
    else
        UIManager:ShowWithKey(PANEL_ID, "ToggleHideInteractOptionList")

        for _, info in pairs(self.m_optionInfoMap) do
            self:_UpdateOptionSelected(info)
        end
    end
end

InteractOptionCtrl.CameraHideHud = HL.Method() << function(self)
    self:ToggleHideInteractOptionList("CameraHideHud", true)
end

InteractOptionCtrl.CameraEndHideHud = HL.Method() << function(self)
    self:ToggleHideInteractOptionList("CameraHideHud", false)
end




InteractOptionCtrl.AddInteractOption = HL.Method(HL.Table) << function(self, args)
    local info = self:_GetSingleInteractOptionInfo(args)
    local key = info.identifier.value
    if self.m_optionInfoMap[key] then
        self:UpdateInteractOption(args)
        return
    end


    local oldInfo = self.m_optionInfoMap[key]
    if oldInfo and oldInfo.identifier then

        self.m_optionInfoMap[key].identifier:Recycle()
    end
    self.m_optionInfoMap[key] = info
    self.m_needUpdateList = true
    self.m_nextUpdateNeedToTop = true
end

InteractOptionCtrl.UpdateInteractOption = HL.Method(HL.Table) << function(self, args)
    local newInfo = self:_GetSingleInteractOptionInfo(args)



    if newInfo.identifier.subIndex < 0 then
        logger.error("identifier.subIndex < 0", inspect(args))
        newInfo.identifier:Recycle()
        return
    end
    local key = newInfo.identifier.value
    if not self.m_optionInfoMap[key] then
        logger.error("没有该选项", inspect(args))
        newInfo.identifier:Recycle()
        return
    end
    local oldInfo = self.m_optionInfoMap[key]
    if self.m_playingOutInfoTimers[oldInfo] then
        self:_ClearTimer(self.m_playingOutInfoTimers[oldInfo])
        self.m_playingOutInfoTimers[oldInfo] = nil

        self:_PlayOptionAnimation(oldInfo.cell, true)
        AudioAdapter.PostEvent("au_ui_btn_f_menubar_appear")
    end
    newInfo.cell = oldInfo.cell
    newInfo.time = oldInfo.time
    newInfo.overrideText = oldInfo.overrideText

    if not newInfo.icon then
        newInfo.icon = oldInfo.icon
        newInfo.iconFolder = oldInfo.iconFolder
    end
    if not newInfo.action then
        newInfo.action = oldInfo.action
    end

    self.m_optionInfoMap[key] = newInfo
    if not newInfo.cell then
        oldInfo.identifier:Recycle()
        return
    end

    local index = lume.find(self.m_curShowingOptInfoList, oldInfo)
    self.m_curShowingOptInfoList[index] = newInfo
    self:_OnUpdateCell(newInfo)

    if args.needReSort then
        self:_SortInteractOptionList()
        local newIndex = lume.find(self.m_curShowingOptInfoList, newInfo)
        if newIndex == index then
            oldInfo.identifier:Recycle()
            return
        end
        newInfo.cell.gameObject.transform:SetSiblingIndex(CSIndex(newIndex))
        self:_RefreshListViewGroup()
        if args.setTopAsSelectedWhenSort then
            self:_SetSelected(self.m_curShowingOptInfoList[1].identifier.value)
        end
    end
    oldInfo.identifier:Recycle()

    CS.Beyond.Gameplay.CheckHasInteractOption.Update()
end

InteractOptionCtrl.RemoveInteractOption = HL.Method(HL.Table) << function(self, args)

    local isList = lume.isarray(args)
    local identifier
    if isList then
        identifier = InteractOptionIdentifier.CreateInstance(unpack(args))
    elseif args.type ~= nil then
        identifier = InteractOptionIdentifier.CreateInstance(args.type, args.sourceId or "0", args.subIndex or 0)
    end

    if not identifier then
        logger.error(ELogChannel.UI, "[InteractOption] 移除交互物选项时 Identifier 为空，移除失败。")
        return
    end

    local needDel = {}
    for key, info in pairs(self.m_optionInfoMap) do
        if identifier:Includes(info.identifier) then
            local cell = info.cell
            if cell then
                self:_DeleteCell(cell, info)
            else

                info.identifier:Recycle()
                table.insert(needDel, key)
            end
        end
    end

    identifier:Recycle()

    for _, key in pairs(needDel) do
        self.m_optionInfoMap[key] = nil
    end
end

InteractOptionCtrl._OnOptOutAnimFinished = HL.Method(HL.Table) << function(self, info)
    local cell = info.cell
    self.m_playingOutInfoTimers[info] = nil
    self:_CacheCell(cell)
    self.m_optionInfoMap[info.identifier.value] = nil
    self:_UpdateCurShowingList()
    info.identifier:Recycle()
    CS.Beyond.Gameplay.CheckHasInteractOption.Update()
end

InteractOptionCtrl.m_nextOptSeqNum = HL.Field(HL.Number) << 1

InteractOptionCtrl._ParseOptionData = HL.Method(HL.Any).Return(HL.Table) << function(self, optionData)
    local optionInfo = {
        data = optionData
    }
    local identifier
    local argsType = type(optionData)



    if argsType == "table" then
        if lume.isarray(optionData) then

            identifier = optionData[1]
            if identifier then
                identifier = InteractOptionIdentifier.CreateInstance(identifier)
            end

            optionInfo.text = optionData[2]
            optionInfo.action = optionData[3]
            optionInfo.icon = optionData[4]
            optionInfo.iconFolder = optionData[5]
            optionInfo.sortId = optionData[6]
        else
            if optionData.type ~= nil then
                identifier = InteractOptionIdentifier.CreateInstance(optionData.type, optionData.sourceId or "0", optionData.subIndex or 0)
                setmetatable(optionInfo, { __index = optionData })
            end
        end
    elseif argsType == "userdata" then
        identifier = optionData.identifier
        if identifier then
            identifier = InteractOptionIdentifier.CreateInstance(identifier)
        end
    end
    if identifier == nil then
        logger.error("InteractOptionCtrl._ParseOptionData: No valid identifier")
        return optionInfo
    end
    optionInfo.identifier = identifier
    optionInfo.typeOrder = identifier.type:GetHashCode()

    optionInfo.isItem = false

    local optionType = identifier.type
    local parseFunction = InteractOptionConst.INTERACT_OPTION_PARSE_OPTION_DATA_CONFIG[optionType]
    if parseFunction ~= nil then
        parseFunction(optionInfo, optionData)
    end

    optionInfo.isItemOrder = optionInfo.isItem and 0 or 1
    optionInfo.sourceId = optionInfo.sourceId or optionInfo.identifier.sourceId
    optionInfo.subIndex = optionInfo.identifier.subIndex
    optionInfo.sortId = optionInfo.sortId or 0

    return optionInfo
end

InteractOptionCtrl._GetSingleInteractOptionInfo = HL.Method(HL.Any).Return(HL.Table) << function(self, optionData)
    local optionInfo = self:_ParseOptionData(optionData)
    if optionInfo == nil then
        return optionInfo
    end
    if optionInfo.identifier.ignoreSeqNum then




        optionInfo.seqNum = 0
    else
        optionInfo.seqNum = self.m_nextOptSeqNum
        self.m_nextOptSeqNum = self.m_nextOptSeqNum + 1
    end
    return optionInfo
end

InteractOptionCtrl._GetGroupInteractOptions = HL.Method(HL.Any).Return(HL.Table) << function(self, optionDataList)
    local optionInfoList = {}
    if optionDataList == nil then
        return optionInfoList
    end

    local argsType = type(optionDataList)
    if argsType == "table" then
        for _, optionArg in pairs(optionDataList) do
            local optionInfo = self:_ParseOptionData(optionArg)
            if optionInfo ~= nil then
                table.insert(optionInfoList, optionInfo)
            end
        end
    elseif argsType == "userdata" then
        for i = 0, optionDataList.Count - 1 do
            local optionInfo = self:_ParseOptionData(optionDataList[i])
            if optionInfo ~= nil then
                table.insert(optionInfoList, optionInfo)
            end
        end
    end

    if #optionInfoList > 0 then
        for _, optionInfo in pairs(optionInfoList) do
            if optionInfo.identifier.ignoreSeqNum then




                optionInfo.seqNum = 0
            else
                optionInfo.seqNum = self.m_nextOptSeqNum
            end
        end
    end
    self.m_nextOptSeqNum = self.m_nextOptSeqNum + 1

    return optionInfoList
end

InteractOptionCtrl._OnScroll = HL.Method(HL.Number) << function(self, delta)
    if self.m_curShowingOptCount <= 1 or self.m_isFocusingForGuide then
        return
    end
    local oldInfo = self.m_optionInfoMap[self.m_curSelectedOptIdentifier]
    local oldIndex = lume.find(self.m_curShowingOptInfoList, oldInfo)
    local newIndex = lume.clamp(oldIndex + (delta < 0 and 1 or -1), 1, self.m_curShowingOptCount)
    if newIndex == oldIndex then
        return
    end
    self:_SelectOption(newIndex)
end

InteractOptionCtrl._SelectOption = HL.Method(HL.Number) << function(self, newIndex)
    local newInfo = self.m_curShowingOptInfoList[newIndex]
    self:_SetSelected(newInfo.identifier.value)
    self:_ScrollTo(newIndex)
    AudioAdapter.PostEvent("au_ui_btn_f_menubar_highlight")
end

InteractOptionCtrl._OnUpdateCell = HL.Method(HL.Table) << function(self, info)
    local cell = info.cell
    local key = info.identifier.value

    local isItem = info.isItem
    local isDel = info.isDel == true
    cell.normalNode.gameObject:SetActiveIfNecessary(not isDel)
    cell.delNode.gameObject:SetActiveIfNecessary(isDel)
    local node = isDel and cell.delNode or cell.normalNode

    if node.itemNode then
        node.itemNode.gameObject:SetActive(isItem)
    end
    node.simpleNode.gameObject:SetActive(not isItem)
    if isItem then
        if not string.isEmpty(info.itemId) then
            node.itemNode.numTxt.text = info.count <= MAX_DISPLAY_COUNT and UIUtils.getNumString(info.count) or Language.LUA_INTERACT_OPTION_MAX_TEXT
            local itemData = Tables.itemTable:GetValue(info.itemId)
            if info.useOverrideName then
                node.nameTxt.text = info.text
            else
                node.nameTxt.text = itemData.name
            end
            node.itemNode.iconImg:InitItemIcon(info.itemId)

            local isPickUp, _ = Tables.useItemTable:TryGetValue(info.itemId)
            node.itemNode.pickUpNode.gameObject:SetActive(isPickUp)
            node.itemNode.portableDeviceNode.gameObject:SetActive(Utils.isPortableDevice(info.itemId))
            UIUtils.setItemRarityImage(node.itemNode.rarityImg, itemData.rarity)
        else
            node.nameTxt.text = "ERROR: No ItemId"
            logger.error("InteractOptionCtrl._OnUpdateCell: No itemId")
        end
    else
        local icon = info.icon
        if not info.icon then
            icon = "btn_common_exchange_icon"
        end
        local iconFolder = info.iconFolder or UIConst.UI_SPRITE_INTERACT_OPTION_ICON
        node.simpleIcon:LoadSprite(iconFolder, icon)
        node.nameTxt.text = info.overrideText or info.text
    end

    cell.button.enabled = true
    cell.button.onClick:RemoveAllListeners()
    cell.button.onHoverChange:RemoveAllListeners()
    cell.button.onPressStart:RemoveAllListeners()
    cell.button.onPressEnd:RemoveAllListeners()
    local isPickable = info.identifier.isPickable
    local canLongPress = isPickable or info.longPressAction ~= nil
    if canLongPress then
        cell.button.onPressStart:AddListener(function(_)
            self:_OnPressOptionStart()
        end)

    else
        cell.button.onClick:AddListener(function()
            info.clicked = true
            self:_OnClickOption(key)
        end)
    end
    cell.button.onHoverChange:AddListener(function(isHover)
        if isHover then
            self:_SetSelected(key)
        end
    end)
    cell.button.onPressEnd:AddListener(function(eventData)
        local newInfo = self.m_optionInfoMap[key]
        self:_UpdateOptionSelected(newInfo)
        if canLongPress then

            if not eventData or (eventData.position - eventData.pressPosition).sqrMagnitude < 1 then
                self:_OnPressOptionEnd()
            else
                self:_InterruptPickupAll()
            end
        end
    end)
    self:_UpdateOptionSelected(info)
    self:_UpdateCellObjectName(info)
end

InteractOptionCtrl._UpdateCellObjectName = HL.Method(HL.Table) << function(self, info)
    local cell = info.cell
    if info.gameObjectName then
        if info.sourceId == "SubBuilding" and InteractOptionCtrl.s_subBuildingOptUseIndexNameForGuide then
            cell.gameObject.name = info.gameObjectName .. "-" .. info.subBuildingIndex
        else
            cell.gameObject.name = info.gameObjectName
        end
    else
        cell.gameObject.name = "OptionItem-" .. info.identifier.value
    end
    cell.button.gameObject.name = "Content-" .. cell.gameObject.name
    info.conditionTriggerName = cell.gameObject.name
    CS.Beyond.Gameplay.Conditions.OnInteractOptionShow.Trigger(cell.gameObject.name)
end

InteractOptionCtrl._SetSelected = HL.Method(HL.String) << function(self, newIdentifier)
    if self.m_isFocusingForGuide then
        return
    end
    local oldInfo = self.m_optionInfoMap[self.m_curSelectedOptIdentifier]
    local newInfo = self.m_optionInfoMap[newIdentifier]
    self.m_curSelectedOptIdentifier = newIdentifier
    self:_InterruptPickupAll()
    self:_UpdateOptionSelected(oldInfo)
    self:_UpdateOptionSelected(newInfo)
end

InteractOptionCtrl._RefreshScrollHint = HL.Method() << function(self)
    local showScroll = self:IsShow() and self.m_curShowingOptCount > 1
    self.view.scrollHint.gameObject:SetActive(showScroll)
    Notify(MessageConst.ON_TOGGLE_INTERACT_OPTION_SCROLL, showScroll)
end

InteractOptionCtrl._UpdateOptionSelected = HL.Method(HL.Table) << function(self, info)
    if info == nil then
        return
    end

    local cell = info.cell
    if not cell then
        return
    end

    local isSelected
    if DeviceInfo.usingTouch then
        isSelected = not LuaSystemManager.factory.inDestroyMode
    else
        isSelected = info.identifier.value == self.m_curSelectedOptIdentifier
    end

    if isSelected then
        cell.animator:ResetTrigger("Normal")
        if cell.gameObject.activeInHierarchy then
            cell.animator:Play("Highlighted")
        end
    else
        cell.animator:ResetTrigger("Highlighted")
        cell.animator:SetTrigger("Normal")
    end
end

InteractOptionCtrl._UpdateBtnHint = HL.Method() << function(self)
    local cell
    if not string.isEmpty(self.m_curSelectedOptIdentifier) then
        local info = self.m_optionInfoMap[self.m_curSelectedOptIdentifier]
        if info then
            cell = info.cell
        else
            logger.error("InteractOptionCtrl._UpdateBtnHint No m_curSelectedOptIdentifier", self.m_curSelectedOptIdentifier, self.m_optionInfoMap)
            if self.m_curShowingOptCount > 0 then
                self:_SelectOption(1)
            else
                self.m_curSelectedOptIdentifier = ""
            end
            self:_UpdateBtnHint()
            return
        end
    end
    local btnHint = self.view.btnHint
    if cell then
        if not btnHint.gameObject.activeSelf then
            btnHint.gameObject:SetActive(true)
            btnHint.animation:Play()
        end
        btnHint.transform.anchoredPosition = Vector2(27, cell.transform.anchoredPosition.y + cell.button.transform.anchoredPosition.y)
    else
        btnHint.gameObject:SetActive(false)
    end
end

InteractOptionCtrl._SortInteractOptionList = HL.Method() << function(self)
    if self.m_curShowingOptInfoList == nil or #self.m_curShowingOptInfoList <= 1 then
        return
    end
    table.sort(self.m_curShowingOptInfoList, self.m_sortFunc)
end

InteractOptionCtrl.m_optionClickedThisTick = HL.Field(HL.Boolean) << false

InteractOptionCtrl._OnClickOption = HL.Method(HL.String) << function(self, identifier)
    if self.m_optionClickedThisTick then
        logger.warn("InteractOptionCtrl._OnClickOption FAIL: self.m_optionClickedThisTick==true")
        return
    end

    local info = self.m_optionInfoMap[identifier]
    if not info then
        return
    end

    if self.m_isFocusingForGuide and self.m_focusingForGuideOptName ~= info.conditionTriggerName then

        return
    end

    if self.m_playingOutInfoTimers[info] then

        return
    end

    local t = info.identifier.type

    local audio = InteractOptionConst.INTERACT_OPTION_CLICK_AUDIO_CONFIG[t]
    AudioManager.PostEvent(audio or "au_ui_g_click")

    if not GameInstance.playerController.mainCharacter then
        return
    end

    if GameWorld.battle.isSquadInFight then
        if not UIConst.INT_BATTLE_ENABLED_OPT_TYPES[t] then
            GameAction.ShowUIToast(Language.LUA_INTERACT_OPTION_IN_FIGHT)
            return
        end
    end


    if t == InteractOptionType.Factory or t == InteractOptionType.Crop then
        local isForbid, forbidReason = Utils.isForbiddenWithReason(ForbidType.ForbidInteractFacBuilding)
        if isForbid then
            Notify(MessageConst.SHOW_RADIO, { GameAction.RadioRuntimeData(forbidReason.radioId, true, 0) })
            return
        end
    end

    info.action()
    self.m_optionClickedThisTick = true
end

InteractOptionCtrl._ClearShowingUIOptions = HL.Method() << function(self)
    for info, timer in pairs(self.m_playingOutInfoTimers) do
        self:_ClearTimer(timer)
        self:_CacheCell(info.cell)
        self.m_optionInfoMap[info.identifier.value] = nil
    end
    self.m_playingOutInfoTimers = {}
    CS.Beyond.Gameplay.CheckHasInteractOption.Update()
end


InteractOptionCtrl.ClearUIOptions = HL.Method(HL.Opt(HL.Any)) << function(self, arg)
    self:_ClearShowingUIOptions()
    self.m_curShowingOptInfoList = {}
    self:_UpdateCurShowingList(true)
end




InteractOptionCtrl._GetNewCell = HL.Method().Return(HL.Table) << function(self)
    local obj = self.m_cellObjCache:Get()
    local cell = self.m_obj2CellMap[obj]
    if not cell then
        obj.gameObject:SetActive(true)
        cell = Utils.wrapLuaNode(obj)
        self.m_obj2CellMap[obj] = cell
    end
    self:_PlayOptionAnimation(cell, true)
    cell.layoutElement.ignoreLayout = false
    cell.transform.localScale = Vector3.one
    AudioAdapter.PostEvent("au_ui_btn_f_menubar_appear")
    return cell
end

InteractOptionCtrl._CacheCell = HL.Method(HL.Table) << function(self, cell)
    if cell == nil then
        return
    end

    cell.layoutElement.ignoreLayout = true
    cell.transform.localScale = Vector3.zero
    cell.transform.anchoredPosition = Vector2(99999, 99999)
    cell.gameObject.name = "CachedCell"
    cell.button.gameObject.name = "Content"

    cell.button.onClick:RemoveAllListeners()
    cell.button.onHoverChange:RemoveAllListeners()
    cell.button.onPressStart:RemoveAllListeners()
    cell.button.onPressEnd:RemoveAllListeners()

    self.m_cellObjCache:Cache(cell.gameObject)
end

InteractOptionCtrl._DeleteCell = HL.Method(HL.Table, HL.Table) << function(self, cell, optionInfo)
    if cell == nil or optionInfo == nil then
        return
    end

    if not self.m_playingOutInfoTimers[optionInfo] then


        if self:IsShow() then
            cell.button.enabled = false
            self:_PlayOptionAnimation(cell, false)
            AudioAdapter.PostEvent("au_ui_btn_f_menubar_dissappear")
            cell.animator:Update(0)
            self.m_playingOutInfoTimers[optionInfo] = self:_StartTimer(self.view.config.OPTION_CELL_OUT_ANIM_TIME, function()
                self:_OnOptOutAnimFinished(optionInfo)
            end)
        else

            self:_OnOptOutAnimFinished(optionInfo)
        end
    end
end

InteractOptionCtrl._UpdateCurShowingList = HL.Method(HL.Opt(HL.Boolean)) << function(self, toTop)
    self.m_needUpdateList = false

    local oldSelectedInfo, oldSelectedIndex
    if not toTop and not string.isEmpty(self.m_curSelectedOptIdentifier) then
        oldSelectedInfo = self.m_optionInfoMap[self.m_curSelectedOptIdentifier]
        oldSelectedIndex = lume.find(self.m_curShowingOptInfoList, oldSelectedInfo)
    end

    self.m_curShowingOptInfoList = {}
    self.m_curShowingOptCount = 0
    for _, v in pairs(self.m_optionInfoMap) do
        if not LuaSystemManager.factory.inDestroyMode or v.isDel then
            table.insert(self.m_curShowingOptInfoList, v)
            self.m_curShowingOptCount = self.m_curShowingOptCount + 1
        else

            if v.cell then
                self:_CacheCell(v.cell)
                v.cell = nil
            end
        end
    end

    self:_SortInteractOptionList()

    local newSelectedInfo
    if oldSelectedInfo then
        local newSelectIndex = lume.find(self.m_curShowingOptInfoList, oldSelectedInfo)
        if newSelectIndex then

            newSelectedInfo = oldSelectedInfo
        else

            newSelectedInfo = self.m_curShowingOptInfoList[math.min(oldSelectedIndex, self.m_curShowingOptCount)]
        end
    else

        newSelectedInfo = self.m_curShowingOptInfoList[1]
    end
    self.m_curSelectedOptIdentifier = newSelectedInfo and newSelectedInfo.identifier.value or ""
    self:_InterruptPickupAll()

    self:_RefreshList()
end

InteractOptionCtrl._RefreshList = HL.Method() << function(self)
    self:_RefreshScrollHint()
    for index, info in ipairs(self.m_curShowingOptInfoList) do
        local cell = info.cell
        if not cell then

            cell = self:_GetNewCell()
            info.cell = cell
        end

        self:_OnUpdateCell(info)
        cell.gameObject.transform:SetSiblingIndex(CSIndex(index))
    end

    self:_RefreshListViewGroup()




    local tooMuchCount = #self.m_curShowingOptInfoList > 1 and self.view.listContainer.rect.height > (self.view.optionList.transform.rect.height + 1)
    self.view.optionListImage.raycastTarget = tooMuchCount

    CS.Beyond.Gameplay.CheckHasInteractOption.Update()
end

InteractOptionCtrl._ScrollTo = HL.Method(HL.Number) << function(self, index)
    if index <= 0 or self.m_isFocusingForGuide then
        return
    end
    local cell = self.m_curShowingOptInfoList[index].cell
    self.view.optionList:ScrollToNaviTarget(cell.button)
end









InteractOptionCtrl._AddGroupInteractOptions = HL.Method(HL.Any) << function(self, optionDataList)
    if optionDataList == nil then
        return
    end

    local optionInfoList = self:_GetGroupInteractOptions(optionDataList)
    if optionInfoList == nil or #optionInfoList == 0 then
        return
    end



    local groupSourceId
    for _, optionInfo in pairs(optionInfoList) do
        local identifier = optionInfo.identifier
        if groupSourceId == nil then
            groupSourceId = identifier.sourceId
        else
            if groupSourceId ~= identifier.sourceId then
                logger.error("InteractOptionCtrl.AddInteractOptions: Group options inconsistent source id")


                for _, optionInfo in pairs(optionInfoList) do
                    if optionInfo and optionInfo.identifier then

                        optionInfo.identifier:Recycle()
                    end
                end
                return
            end
        end
    end

    for _, optionInfo in pairs(optionInfoList) do
        local key = optionInfo.identifier.value
        if self.m_optionInfoMap[key] and self.m_optionInfoMap[key].identifier then

            self.m_optionInfoMap[key].identifier:Recycle()
        end
        self.m_optionInfoMap[key] = optionInfo
    end
end

InteractOptionCtrl._RemoveGroupInteractOptions = HL.Method(HL.String) << function(self, groupSourceId)
    if string.isEmpty(groupSourceId) then
        return
    end

    local deleteList = {}
    for key, optionInfo in pairs(self.m_optionInfoMap) do
        local identifier = optionInfo.identifier
        if identifier ~= nil and identifier.sourceId == groupSourceId then
            local cell = optionInfo.cell
            if cell then
                self:_DeleteCell(cell, optionInfo)
            else

                identifier:Recycle()
            end
            table.insert(deleteList, key)
        end
    end

    for _, deleteKey in ipairs(deleteList) do
        self.m_optionInfoMap[deleteKey] = nil
    end
end

InteractOptionCtrl.AddInteractOptions = HL.Method(HL.Any) << function(self, args)
    if args == nil then
        return
    end

    local optionDataList = args[1] or args.optionDataList
    if optionDataList == nil then
        return
    end

    self:_AddGroupInteractOptions(optionDataList)

    self.m_needUpdateList = true
    self.m_nextUpdateNeedToTop = true
end

InteractOptionCtrl.UpdateInteractOptions = HL.Method(HL.Any) << function(self, args)
    if args == nil then
        return
    end

    local groupSourceId, optionDataList
    if lume.isarray(args) then
        groupSourceId, optionDataList = unpack(args)
    else
        groupSourceId, optionDataList = args.groupSourceId, args.optionDataList
    end

    if string.isEmpty(groupSourceId) or optionDataList == nil then
        return
    end

    self:_RemoveGroupInteractOptions(groupSourceId)
    self:_AddGroupInteractOptions(optionDataList)

    self:_UpdateCurShowingList(false)
end

InteractOptionCtrl.RemoveInteractOptions = HL.Method(HL.Any) << function(self, args)
    if args == nil then
        return
    end

    local groupSourceId = args[1] or args.groupSourceId
    if string.isEmpty(groupSourceId) then
        return
    end

    self:_RemoveGroupInteractOptions(groupSourceId)

    self:_UpdateCurShowingList(false)
end











InteractOptionCtrl._RefreshListViewGroup = HL.Method() << function(self)
    if self.m_curShowingOptInfoList == nil then
        return
    end

    local findGroup = false
    local groupDataList = {}
    if self.m_curShowingOptCount > 0 then

        local optIndex = 1
        repeat
            local optInfo = self.m_curShowingOptInfoList[optIndex]
            local targetGroupValue = optInfo.viewGroupValue
            local targetGroupType = optInfo.viewGroupType
            local isValidGroup
            if targetGroupValue ~= nil then
                local i = optIndex
                repeat
                    i = i + 1
                until(i > self.m_curShowingOptCount or
                        self.m_curShowingOptInfoList[i].viewGroupValue == nil or
                        self.m_curShowingOptInfoList[i].viewGroupValue ~= targetGroupValue or
                        self.m_curShowingOptInfoList[i].viewGroupType ~= targetGroupType
                )
                if i - optIndex >= 2 then
                    local groupStartIndex, groupEndIndex = optIndex, i - 1
                    table.insert(groupDataList, {
                        groupStartIndex = groupStartIndex,
                        groupEndIndex = groupEndIndex,
                        groupType = optInfo.viewGroupType,
                    })
                    isValidGroup = true
                    if not findGroup then
                        findGroup = true
                    end
                end
                optIndex = i - 1
            end
            if not isValidGroup then

                local cell = optInfo.cell
                cell.groupTitle.gameObject:SetActive(false)
                cell.groupEndSpace.gameObject:SetActive(false)
                cell.normalNode.groupMask.gameObject:SetActive(false)
                if not string.isEmpty(optInfo.overrideText) then
                    optInfo.overrideText = nil
                    self:_OnUpdateCell(optInfo)
                end
            end
            optIndex = optIndex + 1
        until(optIndex > self.m_curShowingOptCount)
    end

    if not findGroup then
        return
    end


    for _, groupData in pairs(groupDataList) do
        local groupStartIndex, groupEndIndex = groupData.groupStartIndex, groupData.groupEndIndex
        local startOptInfo = self.m_curShowingOptInfoList[groupStartIndex]
        local endOptInfo = self.m_curShowingOptInfoList[groupEndIndex]
        local startOptCell, endOptCell = startOptInfo.cell, endOptInfo.cell

        local groupConfigInfo = self.m_viewGroupConfig[groupData.groupType]
        local mainOptionCheckFunction = groupConfigInfo.mainOptionCheckFunction
        local groupTitle
        if mainOptionCheckFunction ~= nil then

            local mainOptInfo
            for optInfoIndex = groupStartIndex, groupEndIndex do
                local optInfo = self.m_curShowingOptInfoList[optInfoIndex]
                if mainOptionCheckFunction(optInfo) == true then
                    mainOptInfo = optInfo
                end
            end
            if mainOptInfo ~= nil then
                local configGroupTitle = groupConfigInfo.groupTitle
                if type(configGroupTitle) == "function" then
                    groupTitle = configGroupTitle(mainOptInfo)
                    self:_OnUpdateCell(mainOptInfo)
                end
            end
        else
            local getGroupTitle = groupConfigInfo.groupTitle
            if type(getGroupTitle) == "function" then
                groupTitle = getGroupTitle()
            else


                logger.error("[InteractOptionCtrl] groupTitle must be function")
            end
        end

        local groupTitleNode = startOptCell.groupTitle
        local groupEndSpaceNode = endOptCell.groupEndSpace
        local size = groupTitleNode.bg.sizeDelta
        size.y = self.view.config.BASE_HEIGHT + self.view.config.CELL_HEIGHT * (groupEndIndex - groupStartIndex)
        groupTitleNode.bg.sizeDelta = size
        endOptCell.line.transform.sizeDelta = Vector2(endOptCell.line.transform.sizeDelta.x, self.view.config.CELL_HEIGHT * (groupEndIndex - groupStartIndex + 1))
        groupTitleNode.titleText.text = groupTitle

        if not groupTitleNode.gameObject.activeSelf then
            groupTitleNode.gameObject:SetActive(true)
            groupTitleNode.animationWrapper:PlayInAnimation()
        else
            if groupTitleNode.animationWrapper.curState == CS.Beyond.UI.UIConst.AnimationState.Out then
                groupTitleNode.animationWrapper:PlayInAnimation()
            end
        end
        if not groupEndSpaceNode.gameObject.activeSelf then
            groupEndSpaceNode.gameObject:SetActive(true)
            groupEndSpaceNode:PlayInAnimation()
        else
            if groupEndSpaceNode.curState == CS.Beyond.UI.UIConst.AnimationState.Out then
                groupEndSpaceNode:PlayInAnimation()
            end
        end

        for optInfoIndex = groupStartIndex, groupEndIndex do
            local optInfo = self.m_curShowingOptInfoList[optInfoIndex]
            local cell = optInfo.cell
            cell.normalNode.groupMask.gameObject:SetActive(true)
            cell.gameObject.transform:SetSiblingIndex(CSIndex(optInfoIndex))
            if optInfoIndex ~= groupStartIndex then
                cell.groupTitle.gameObject:SetActive(false)
            end
            if optInfoIndex ~= groupEndIndex then
                cell.groupEndSpace.gameObject:SetActive(false)
            end
        end
    end
end






InteractOptionCtrl.m_pickupAllCoroutine = HL.Field(HL.Thread)

InteractOptionCtrl.m_pressStartIdentifier = HL.Field(HL.Any)

InteractOptionCtrl._OnPressOptionStart = HL.Method(HL.Opt(HL.Boolean)) << function(self, fromInputAction)
    self.m_pressStartIdentifier = self.m_curSelectedOptIdentifier
    local info = self.m_optionInfoMap[self.m_curSelectedOptIdentifier]
    if not info then
        return
    end
    if fromInputAction then

        info.cell.animator:Play("Pressed")
    end
    if not info.identifier.isPickable and info.longPressAction == nil then
        return
    end
    local pressedIdentifier = self.m_curSelectedOptIdentifier
    self.m_pickupAllCoroutine = self:_ClearCoroutine(self.m_pickupAllCoroutine)
    local node = self.view.btnHint
    self.m_pickupAllCoroutine = self:_StartCoroutine(function()
        local startTime = Time.unscaledTime
        coroutine.wait(0.2)
        node.pressHintNode.gameObject:SetActive(true)
        node.pressArrow.gameObject:SetActive(not DeviceInfo.usingTouch)
        while true do
            local percent = (Time.unscaledTime - startTime) / 0.5
            node.progressImg.fillAmount = percent
            if percent >= 1 then
                break
            else
                coroutine.step()
            end
        end
        local pressedInfo = self.m_optionInfoMap[pressedIdentifier]
        if pressedInfo and pressedInfo.longPressAction ~= nil then
            pressedInfo.longPressAction()
        else
            self:_OnTriggerPickupAll()
        end
        self:_InterruptPickupAll()
    end)
end

InteractOptionCtrl._OnPressOptionEnd = HL.Method(HL.Opt(HL.Boolean)) << function(self, fromInputAction)
    local info = self.m_optionInfoMap[self.m_curSelectedOptIdentifier]
    if info and fromInputAction then

        info.cell.animator:SetTrigger("Highlighted")
    end
    if self.m_pickupAllCoroutine then
        self:_InterruptPickupAll()

        if not info or (not info.identifier.isPickable and info.longPressAction == nil) then
            return
        end
        self:_OnClickOption(self.m_curSelectedOptIdentifier)
    end
end

InteractOptionCtrl._InterruptPickupAll = HL.Method() << function(self)
    self.m_pickupAllCoroutine = self:_ClearCoroutine(self.m_pickupAllCoroutine)
    self.view.btnHint.pressHintNode.gameObject:SetActive(false)
    self.view.btnHint.pressArrow.gameObject:SetActive(false)
end

InteractOptionCtrl._OnTriggerPickupAll = HL.Method() << function(self)
    local list = {}
    local curInfo = self.m_optionInfoMap[self.m_curSelectedOptIdentifier]
    local startIndex = lume.find(self.m_curShowingOptInfoList, curInfo)
    local count = #self.m_curShowingOptInfoList
    for k = 0, count - 1 do
        local index = (startIndex + k - 1) % count + 1
        local info = self.m_curShowingOptInfoList[index]
        if info.identifier.isPickable then
            table.insert(list, info.identifier)
        end
    end
    InteractOptionIdentifier.OnTriggerPickupAll(list)
end






InteractOptionCtrl.s_subBuildingOptUseIndexNameForGuide = HL.StaticField(HL.Boolean) << true

InteractOptionCtrl.FacToggleSubBuildingOptUseIndexNameForGuide = HL.StaticMethod(HL.Table) << function(arg)
    local useIndexName = unpack(arg)
    InteractOptionCtrl.s_subBuildingOptUseIndexNameForGuide = useIndexName

    local _, self = UIManager:IsOpen(PANEL_ID)
    if self then
        for _, info in ipairs(self.m_curShowingOptInfoList) do
            self:_UpdateCellObjectName(info)
        end
    end
end

InteractOptionCtrl.m_isFocusingForGuide = HL.Field(HL.Boolean) << false

InteractOptionCtrl.m_focusingForGuideOptName = HL.Field(HL.String) << ""

InteractOptionCtrl.FocusOnInteractOption = HL.Method(HL.Table) << function(self, arg)
    logger.info("InteractOptionCtrl.FocusOnInteractOption", arg)
    local optName = unpack(arg)
    self.m_isFocusingForGuide = false
    self.m_focusingForGuideOptName = ""
    if string.isEmpty(optName) then

        if not string.isEmpty(self.m_curSelectedOptIdentifier) and self.m_curShowingOptCount > 0 then
            local info = self.m_optionInfoMap[self.m_curSelectedOptIdentifier]
            if not info then

                self:_SelectOption(1)
            end
        end
        return
    end
    for k, info in ipairs(self.m_curShowingOptInfoList) do
        if info.cell.gameObject.name == optName then
            self:_SelectOption(k)
            self.m_isFocusingForGuide = true
            self.m_focusingForGuideOptName = optName
            return
        end
    end
    logger.error("InteractOptionCtrl.FocusOnInteractOption: No Target OptName", optName)
end




InteractOptionCtrl._PlayOptionAnimation = HL.Method(HL.Table, HL.Boolean) << function(self, cell, isIn)
    if isIn then
        cell.animator:Play("In")
        if cell.groupEndSpace.gameObject.activeInHierarchy then
            cell.groupEndSpace:PlayInAnimation()
        end
        if cell.groupTitle.gameObject.activeInHierarchy then
            cell.groupTitle.animationWrapper:PlayInAnimation()
        end
    else
        cell.animator:Play("Out")
        if cell.groupEndSpace.gameObject.activeInHierarchy then
            cell.groupEndSpace:PlayOutAnimation()
        end
        if cell.groupTitle.gameObject.activeInHierarchy then
            cell.groupTitle.animationWrapper:PlayOutAnimation()
        end
    end
end


HL.Commit(InteractOptionCtrl)
