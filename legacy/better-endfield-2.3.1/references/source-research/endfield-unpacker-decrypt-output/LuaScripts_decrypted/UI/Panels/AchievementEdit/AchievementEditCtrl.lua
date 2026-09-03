
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.AchievementEdit

AchievementEditCtrl = HL.Class('AchievementEditCtrl', uiCtrl.UICtrl)






AchievementEditCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_ACHIEVEMENT_DISPLAY_UPDATE] = '_OnDisplayUpdate',
}

AchievementEditCtrl.m_onClose = HL.Field(HL.Any) << nil

AchievementEditCtrl.m_depotListCellFunc = HL.Field(HL.Function)

AchievementEditCtrl.m_playerDisplay = HL.Field(HL.Any) << {}

AchievementEditCtrl.m_playerDepot = HL.Field(HL.Any) << {}

AchievementEditCtrl.m_editDisplay = HL.Field(HL.Any) << {}

AchievementEditCtrl.m_editDepot = HL.Field(HL.Any) << {}

AchievementEditCtrl.m_medalGroupDragOptions = HL.Field(HL.Table)

AchievementEditCtrl.m_depotDragOptions = HL.Field(HL.Table)

AchievementEditCtrl.m_isSaving = HL.Field(HL.Boolean) << false

AchievementEditCtrl.m_dragMedalId = HL.Field(HL.String) << ''

AchievementEditCtrl.m_naviDragBeginSlot = HL.Field(HL.Number) << -1

AchievementEditCtrl.m_naviDragMedal = HL.Field(HL.Any)

AchievementEditCtrl.m_naviCancelInputGroupId = HL.Field(HL.Number) << 1

AchievementEditCtrl.m_naviRetractInputGroupId = HL.Field(HL.Number) << 1

AchievementEditCtrl.m_naviFocusMedal = HL.Field(HL.Any)

AchievementEditCtrl.m_lateTickKey = HL.Field(HL.Number) << -1

AchievementEditCtrl.m_tipsSwitch = HL.Field(HL.Any) << nil

local DRAG_MEDAL_CONFIGS = {
    [UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDisplay] = {
        [UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDisplay] = {
            action = function(self, dragInfo, dropInfo)
                if dragInfo.achievementId ~= dropInfo.achievementId then
                    if string.isEmpty(dropInfo.achievementId) then
                        self:_SetTips(true, "Display2EmptyDisplay")
                    else
                        self:_SetTips(true, "Display2Display")
                    end
                else
                    self:_SetTips(false)
                end
            end
        },
        [UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDepot] = {
            action = function(self, dragInfo, dropInfo)
                self:_SetTips(true, "Display2Depot")
            end
        }
    },
    [UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDepot] = {
        [UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDisplay] = {
            action = function(self, dragInfo, dropInfo)
                if not string.isEmpty(dropInfo.achievementId) then
                    self:_SetTips(true, "Depot2Display")
                else
                    self:_SetTips(true, "Display2EmptyDisplay")
                end
            end
        },
    },
}

local DROP_MEDAL_CONFIGS = {
    [UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDisplay] = {
        [UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDisplay] = {
            action = function(self, dragInfo, dropInfo)
                local dragSlotIndex = dragInfo.slotIndex
                local dropSlotIndex = dropInfo.slotIndex
                if dragSlotIndex <= 0 or dropSlotIndex <= 0 then
                    return
                end
                local dragMedal = self.m_editDisplay[dragSlotIndex]
                if dragMedal == nil then
                    return
                end
                self.m_editDisplay[dragSlotIndex] = nil
                local dropMedal = self.m_editDisplay[dropSlotIndex]
                if dropMedal ~= nil then
                    self.m_editDisplay[dragSlotIndex] = dropMedal
                end
                self.m_editDisplay[dropSlotIndex] = dragMedal
                self:_OnEditDisplayChanged()
                self:_RenderViews(false)
            end
        },
        [UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDepot] = {
            action = function(self, dragInfo, dropInfo)
                local dragSlotIndex = dragInfo.slotIndex
                if dragSlotIndex <= 0 then
                    return
                end
                local dragMedal = self.m_editDisplay[dragSlotIndex]
                if dragMedal == nil then
                    return
                end
                self.m_editDisplay[dragSlotIndex] = nil
                table.insert(self.m_editDepot, dragMedal)
                self:_OnEditDisplayChanged()
                self:_RenderViews(false)
            end
        }
    },
    [UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDepot] = {
        [UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDisplay] = {
            action = function(self, dragInfo, dropInfo)
                local dragAchievementId = dragInfo.achievementId
                local dropSlotIndex = dropInfo.slotIndex
                if dropSlotIndex <= 0 then
                    return
                end
                local depotIndex, depotMedal = self:_FindEditDepot(dragAchievementId)
                if depotIndex <= 0 then
                    return
                end
                table.remove(self.m_editDepot, depotIndex)
                local dropMedal = self.m_editDisplay[dropSlotIndex]
                if dropMedal ~= nil then
                    table.insert(self.m_editDepot, dropMedal)
                end
                self.m_editDisplay[dropSlotIndex] = depotMedal
                self:_OnEditDisplayChanged()
                self:_RenderViews(false)
            end
        },
    },
}


AchievementEditCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local recoverState = arg and arg.recoverState or nil
    self.m_onClose = arg and arg.onClose
    self:_InitViews()
    self:_LoadData()
    self:_TryRecoverState(recoverState)
    self:_RenderViews(true)
    self:_TryRecoverAchievementDepot(recoverState and recoverState.achievementDepotState)
end

AchievementEditCtrl.OnShow = HL.Override() << function(self)
    self:_StartTick()
end

AchievementEditCtrl.OnHide = HL.Override() << function(self)
    self:_StopTick()
end

AchievementEditCtrl.OnClose = HL.Override() << function(self)
    self:_StopTick()
end

AchievementEditCtrl._StartTick = HL.Method() << function(self)
    self.m_lateTickKey = LuaUpdate:Add("TailTick", function(deltaTime)
        local usingController = DeviceInfo.usingController
        if self.view.naviDragNode and self.view.naviDragNode.gameObject.activeInHierarchy ~= usingController then
            self.view.naviDragNode.gameObject:SetActive(usingController)
            if not usingController then
                self:_OnNaviDragCancel()
            end
        end
        if usingController and self.view.naviDragNode.gameObject.activeInHierarchy then
            local target = InputManagerInst.controllerNaviManager.curTarget
            self:_UpdateNaviDragPos(target)
        end
    end)
end

AchievementEditCtrl._StopTick = HL.Method() << function(self)
    self.m_lateTickKey = LuaUpdate:Remove(self.m_lateTickKey)
end

AchievementEditCtrl._InitViews = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId}, nil, function(infoList)

        if self.m_naviFocusMedal ~= nil or self.m_naviDragMedal ~= nil then
            return
        end
        local medalSlotIndex = -1
        for i, info in ipairs(infoList) do
            if info ~= nil and info.hintView ~= nil then
                local currWidget = info.hintView.transform:GetComponent("LuaUIWidget")
                if currWidget ~= nil and currWidget.table ~= nil then
                    local medalSlot = currWidget.table[1]
                    if medalSlot ~= nil then
                        medalSlotIndex = i
                    end
                end
            end
        end
        if medalSlotIndex > 0 then
            table.remove(infoList, medalSlotIndex)
        end
    end)
    self.view.btnBack.onClick:RemoveAllListeners()
    self.view.btnBack.onClick:AddListener(function()
        self:_OnBackClick()
    end)
    self.view.resetBtn.onClick:RemoveAllListeners()
    self.view.resetBtn.onClick:AddListener(function()
        self:_OnResetClick()
    end)
    self.view.saveBtn.onClick:RemoveAllListeners()
    self.view.saveBtn.onClick:AddListener(function()
        self:_SaveEditData()
    end)

    self.view.depotBtn.onClick:RemoveAllListeners()
    self.view.depotBtn.onClick:AddListener(function()
        self:_OpenDepot()
    end)
    self.m_depotListCellFunc = UIUtils.genCachedCellFunction(self.view.depotList)
    self.view.depotList.onUpdateCell:RemoveAllListeners()
    self.view.depotList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_RenderDepotCell(self.m_depotListCellFunc(obj), LuaIndex(csIndex))
    end)
    self.view.depotDropArea:ClearEvents()
    self.view.depotDropArea.luaTable = {
        {
            slotType = UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDepot,
            slotIndex = -1,
            dropItem = self.view.depotDropArea,
        }
    }
    self.view.depotDropArea.onDropEvent:AddListener(function(eventData)
        self:_OnDropToDepot(eventData)
    end)
    self.view.depotDropArea.gameObject:SetActive(false)

    self.m_medalGroupDragOptions = {
        slotType = UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDisplay,
        onBeginDrag = function(dragInfo)
            self:_OnBeginDrag(dragInfo)
        end,
        onEndDrag = function(dragInfo)
            self:_OnEndDrag(dragInfo)
        end,
        onDragMedal = function(dragInfo, dropInfo)
            self:_OnDragMedal(dragInfo, dropInfo)
        end,
        onDropMedal = function(dragInfo, dropInfo)
            self:_OnDropMedal(dragInfo, dropInfo)
        end,
        onClick = function(slotIndex, achievementId)
            self:_OnClick(slotIndex, achievementId)
        end,
    }

    self.m_depotDragOptions = {
        slotType = UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDepot,
        slotIndex = -1,
        onBeginDrag = function(dragInfo)
            self:_OnBeginDrag(dragInfo)
        end,
        onEndDrag = function(dragInfo)
            self:_OnEndDrag(dragInfo)
        end,
        onDragMedal = function(dragInfo, dropInfo)
            self:_OnDragMedal(dragInfo, dropInfo)
        end,
        onDropMedal = function(dragInfo, dropInfo)
            self:_OnDropMedal(dragInfo, dropInfo)
        end,
        onClick = function(slotIndex, achievementId)
            self:_OnClick(slotIndex, achievementId)
        end,
    }
    self.view.naviMedalGroup.onIsTopLayerChanged:RemoveAllListeners()
    self.view.naviMedalGroup.onIsTopLayerChanged:AddListener(function(isTop)
        self.view.selectFrame.gameObject:SetActive(isTop)
        self:_UpdateNaviDragMedal()
    end)
    self.view.naviMedalGroup.onSetLayerSelectedTarget:AddListener(function(target)
        local currWidget = target.transform:GetComponent("LuaUIWidget")
        if currWidget == nil or currWidget.table == nil then
            return
        end
        local medalSlot = currWidget.table[1]
        if medalSlot ~= nil then
            self:_UpdateNaviFocusMedal(self.m_editDisplay[medalSlot.slotIndex])
            self:_UpdateNaviDragFocus(medalSlot.slotIndex)
        end
    end)
    self.view.naviMedalDepot.onSetLayerSelectedTarget:AddListener(function(target)
        local currWidget = target.transform:GetComponent("LuaUIWidget")
        if currWidget == nil or currWidget.table == nil then
            return
        end
        local medalSlot = currWidget.table[1]
        if medalSlot ~= nil then
            local _, naviFocusMedal = self:_FindEditDepot(medalSlot.m_achievementId)
            self:_UpdateNaviFocusMedal(naviFocusMedal)
        end
    end)

    local switchBuilder = CS.Beyond.UI.UIAnimationSwitchTween.Builder()
    switchBuilder.animWrapper = self.view.etchTips
    switchBuilder.dontDisableGameObject = true
    self.m_tipsSwitch = switchBuilder:Build()
    self.m_tipsSwitch:Reset(false)

    self:_InitNaviBind()
end

AchievementEditCtrl._InitNaviBind = HL.Method() << function(self)
    self.m_naviCancelInputGroupId = InputManagerInst:CreateGroup(self.view.inputGroup.groupId)
    UIUtils.bindInputPlayerAction("achievement_navi_edit_cancel", function()
        self:_OnNaviDragCancel()
    end, self.m_naviCancelInputGroupId)

    self.m_naviRetractInputGroupId = InputManagerInst:CreateGroup(self.view.inputGroup.groupId)
    UIUtils.bindInputPlayerAction("achievement_navi_edit_retract", function()
        self:_OnNaviDragRetract()
    end, self.m_naviRetractInputGroupId)

    self:BindInputPlayerAction("achievement_edit_select_up", function()
        self:_OnNavigate(CS.UnityEngine.UI.NaviDirection.Up)
    end)

    self:BindInputPlayerAction("achievement_edit_select_down", function()
        self:_OnNavigate(CS.UnityEngine.UI.NaviDirection.Down)
    end)

    self:BindInputPlayerAction("achievement_edit_select_left", function()
        self:_OnNavigate(CS.UnityEngine.UI.NaviDirection.Left)
    end)

    self:BindInputPlayerAction("achievement_edit_select_right", function()
        self:_OnNavigate(CS.UnityEngine.UI.NaviDirection.Right)
    end)

    self:_UpdateInputGroupStatus()
end

AchievementEditCtrl._OnNavigate = HL.Method(CS.UnityEngine.UI.NaviDirection) << function(self, direction)
    local curr = InputManagerInst.controllerNaviManager.curTarget
    if curr == nil then
        return
    end
    local currWidget = curr.transform:GetComponent("LuaUIWidget")
    if currWidget == nil or currWidget.table == nil then
        return
    end
    local medalSlot = currWidget.table[1]
    if medalSlot == nil then
        return
    end
    local slotIndex = medalSlot.slotIndex
    if slotIndex == nil or slotIndex < 0 then
        self:_OnNavigateDepot(direction)
        return
    end
    self.view.medalGroup:OnNavigate(direction, slotIndex)
end

AchievementEditCtrl._OnNavigateDepot = HL.Method(CS.UnityEngine.UI.NaviDirection) << function(self, direction)
    InputManagerInst.controllerNaviManager:Navigate(direction)
end

AchievementEditCtrl._LoadData = HL.Method() << function(self)
    self.m_playerDisplay = {}
    self.m_playerDepot = {}
    local achievementSystem = GameInstance.player.achievementSystem
    for slotIndex, achievementId in pairs(achievementSystem.achievementData.displayInfo) do
        local medalBundle = self:_GenMedalBundle(achievementId)
        if medalBundle ~= nil then
            self.m_playerDisplay[slotIndex] = medalBundle
        end
    end
    for _, achievementId in pairs(achievementSystem.achievementData.displayDepot) do
        local medalBundle = self:_GenMedalBundle(achievementId)
        if medalBundle ~= nil then
            table.insert(self.m_playerDepot, medalBundle)
        end
    end
    self:_ResetEditData()
end

AchievementEditCtrl._RefreshData = HL.Method(HL.Any) << function(self, editDepotMap)
    for slotIndex, medalBundle in pairs(self.m_editDisplay) do
        local achievementId = medalBundle.achievementId
        if editDepotMap[achievementId] == nil then
            self.m_editDisplay[slotIndex] = nil
        else
            editDepotMap[achievementId] = nil
        end
    end
    local editDepot = {}
    for _, medalBundle in ipairs(self.m_editDepot) do
        local achievementId = medalBundle.achievementId
        if editDepotMap[achievementId] ~= nil then
            table.insert(editDepot, medalBundle)
            editDepotMap[achievementId] = nil
        end
    end
    for achievementId, _ in pairs(editDepotMap) do
        local medalBundle = self:_GenMedalBundle(achievementId)
        if medalBundle ~= nil then
            table.insert(editDepot, medalBundle)
        end
    end
    self.m_editDepot = editDepot
end

AchievementEditCtrl.GetRecoverStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    return {
        displayInfo = self:_GetDisplayRecoverState(),
        depotInfo = self:_GetDepotRecoverState(),
        achievementDepotState = self:_GetAchievementDepotRecoverState(),
    }
end

AchievementEditCtrl._GetDisplayRecoverState = HL.Method().Return(HL.Table) << function(self)
    local displayInfo = {}
    for slotIndex, medalBundle in pairs(self.m_editDisplay) do
        if slotIndex > 0 and medalBundle ~= nil and not string.isEmpty(medalBundle.achievementId) then
            displayInfo[slotIndex] = medalBundle.achievementId
        end
    end
    return displayInfo
end

AchievementEditCtrl._GetDepotRecoverState = HL.Method().Return(HL.Table) << function(self)
    local depotInfo = {}
    for _, medalBundle in ipairs(self.m_editDepot) do
        if medalBundle ~= nil and not string.isEmpty(medalBundle.achievementId) then
            table.insert(depotInfo, medalBundle.achievementId)
        end
    end
    return depotInfo
end

AchievementEditCtrl._GetAchievementDepotRecoverState = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local isOpen, ctrl = UIManager:IsOpen(PanelId.AchievementDepot)
    if not isOpen or not ctrl:IsShow() then
        return
    end
    return ctrl:GetRecoverStateArg()
end

AchievementEditCtrl._TryRecoverState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    self.m_editDisplay = {}
    self.m_editDepot = {}
    local displayInfo = recoverState.displayInfo or {}
    local depotInfo = recoverState.depotInfo or {}
    for slotIndex, achievementId in pairs(displayInfo) do
        if slotIndex > 0 and not string.isEmpty(achievementId) then
            local medalBundle = self:_GenMedalBundle(achievementId)
            if medalBundle ~= nil then
                self.m_editDisplay[slotIndex] = medalBundle
            end
        end
    end
    for _, achievementId in ipairs(depotInfo) do
        if not string.isEmpty(achievementId) then
            local medalBundle = self:_GenMedalBundle(achievementId)
            if medalBundle ~= nil then
                table.insert(self.m_editDepot, medalBundle)
            end
        end
    end
    self:_OnEditDisplayChanged()
end

AchievementEditCtrl._TryRecoverAchievementDepot = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    local isOpen = UIManager:IsOpen(PanelId.AchievementDepot)
    if isOpen then
        return
    end
    self:_OpenDepot(recoverState)
end

AchievementEditCtrl._GenMedalBundle = HL.Method(HL.String).Return(HL.Opt(HL.Any)) << function(self, achievementId)
    if string.isEmpty(achievementId) then
        return
    end
    local achievementSystem = GameInstance.player.achievementSystem
    local achievementTable = Tables.achievementTable
    local hasPlayer, playerAchievement = achievementSystem.achievementData.achievementInfos:TryGetValue(achievementId)
    local hasData, achievementData = achievementTable:TryGetValue(achievementId)
    if not hasPlayer and not hasData then
        return
    end
    return {
        achievementId = achievementId,
        level = hasPlayer and playerAchievement.level or 0,
        isPlated = hasPlayer and playerAchievement.isPlated,
        isRare = hasData and achievementData.applyRareEffect,
    }
end

AchievementEditCtrl._ResetEditData = HL.Method() << function(self)
    self.m_editDisplay = lume.copy(self.m_playerDisplay)
    self.m_editDepot = lume.copy(self.m_playerDepot)
    self:_OnEditDisplayChanged()
end

AchievementEditCtrl._ClearMedalGroup = HL.Method() << function(self)
    for slotIndex, displayInfo in pairs(self.m_editDisplay) do
        table.insert(self.m_editDepot, displayInfo)
        self.m_editDisplay[slotIndex] = nil
    end
    self:_OnEditDisplayChanged()
end

AchievementEditCtrl._RenderViews = HL.Method(HL.Boolean) << function(self, isInit)
    local haveDepot = #self.m_editDepot > 0
    self.view.bottomStateCtrl:SetState(haveDepot and "EtchHave" or "EtchNull")
    self.view.depotList:UpdateCount(#self.m_editDepot, isInit, false, false, not isInit)
    self.view.medalGroup:InitMedalGroup(self.m_editDisplay, Tables.achievementConst.maxDisplayPosition, self.m_medalGroupDragOptions)
end

AchievementEditCtrl._RenderDepotCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local medalBundle = self.m_editDepot[luaIndex]
    cell:InitMedalSlot(medalBundle, self.m_depotDragOptions, -1)
    cell:SetDragState(medalBundle ~= nil and medalBundle.achievementId == self.m_dragMedalId)
    cell.view.button.customBindingViewLabelText = Language["key_hint_achievement_edit_take"]
end

AchievementEditCtrl._SetTips = HL.Method(HL.Boolean, HL.Opt(HL.String)) << function(self, isShow, stateName)
    self.m_tipsSwitch.isShow = isShow
    if stateName ~= nil then
        self.view.tipStateCtrl:SetState(stateName)
    end
end

AchievementEditCtrl._OnBeginDrag = HL.Method(HL.Any) << function(self, dragInfo)
    self.view.depotDropArea.gameObject:SetActive(true)
    self:_SetTips(false)
    if dragInfo ~= nil then
        self.m_dragMedalId = dragInfo.achievementId
        if dragInfo.slotType == UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDisplay then
            self.view.medalGroup:OnDragMedal(dragInfo.slotIndex)
        elseif dragInfo.slotType == UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDepot then
            self.view.depotList:UpdateShowingCells(function(csIndex, obj)
                self:_RenderDepotCell(self.m_depotListCellFunc(obj), LuaIndex(csIndex))
            end)
        end
    end
    AudioAdapter.PostEvent("Au_UI_Event_AchieveMedal_hold")
end

AchievementEditCtrl._OnEndDrag = HL.Method(HL.Any) << function(self, dragInfo)
    self.m_dragMedalId = ''
    self.view.depotDropArea.gameObject:SetActive(false)
    self:_SetTips(false)
    if dragInfo ~= nil then
        if dragInfo.slotType == UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDisplay then
            self.view.medalGroup:CancelDragMedal()
        elseif dragInfo.slotType == UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDepot then
            self.view.depotList:UpdateShowingCells(function(csIndex, obj)
                self:_RenderDepotCell(self.m_depotListCellFunc(obj), LuaIndex(csIndex))
            end)
        end
    end
end

AchievementEditCtrl._OnDragMedal = HL.Method(HL.Any, HL.Any) << function(self, dragInfo, dropInfo)
    if not DeviceInfo.usingController and dragInfo ~= nil and dropInfo ~= nil then
        local dragConfig = DRAG_MEDAL_CONFIGS[dragInfo.slotType]
        if dragConfig ~= nil then
            local dragDropConfig = dragConfig[dropInfo.slotType]
            if dragDropConfig ~= nil then
                AudioAdapter.PostEvent("Au_UI_Hover_Common")
                dragDropConfig.action(self, dragInfo, dropInfo)
                return
            end
        end
    end
    self:_SetTips(false)
end

AchievementEditCtrl._OnDropMedal = HL.Method(HL.Any, HL.Any) << function(self, dragInfo, dropInfo)
    self.m_dragMedalId = ''
    if dragInfo == nil or dropInfo == nil or string.isEmpty(dragInfo.achievementId) then
        return
    end
    local dragConfig = DROP_MEDAL_CONFIGS[dragInfo.slotType]
    if dragConfig == nil then
        return
    end
    local dragDropConfig = dragConfig[dropInfo.slotType]
    if dragDropConfig == nil then
        return
    end
    if dropInfo.slotType == UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDisplay then
        AudioAdapter.PostEvent("Au_UI_Event_AchieveMedal_inlay")
    elseif dropInfo.slotType == UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDepot then
        AudioAdapter.PostEvent("Au_UI_Event_AchieveMedal_putback")
    end
    dragDropConfig.action(self, dragInfo, dropInfo)
end

AchievementEditCtrl._OnDropToDepot = HL.Method(CS.UnityEngine.EventSystems.PointerEventData) << function(self, eventData)
    if IsNull(eventData.pointerDrag) then
        return
    end
    local dragItem = eventData.pointerDrag:GetComponent(typeof(CS.Beyond.UI.UIDragItem))
    if dragItem and dragItem.inDragging and dragItem.luaTable then
        local dragInfo = dragItem.luaTable[1]
        local dropInfo = self.view.depotDropArea.luaTable[1]
        if dragInfo and dropInfo then
            self:_OnDropMedal(dragInfo, dropInfo)
        end
    end
end

AchievementEditCtrl._FindEditDepot = HL.Method(HL.String).Return(HL.Number, HL.Any) << function(self, achievementId)
    local depotIndex = -1
    local depotMedal = nil
    for i, depot in ipairs(self.m_editDepot) do
        if depot.achievementId == achievementId then
            depotIndex = i
            depotMedal = depot
        end
    end
    return depotIndex, depotMedal
end

AchievementEditCtrl._FindEditDisplay = HL.Method(HL.String).Return(HL.Number, HL.Any) << function(self, achievementId)
    local slotIndex = -1
    local displayMedal = nil
    for i, medal in pairs(self.m_editDisplay) do
        if medal.achievementId == achievementId then
            slotIndex = i
            displayMedal = medal
        end
    end
    return slotIndex, displayMedal
end

AchievementEditCtrl._GetCurrentNaviAchievementId = HL.Method().Return(HL.String) << function(self)
    if not DeviceInfo.usingController then
        return ''
    end
    if self.m_naviFocusMedal ~= nil and not string.isEmpty(self.m_naviFocusMedal.achievementId) then
        return self.m_naviFocusMedal.achievementId
    end
    local curr = InputManagerInst.controllerNaviManager.curTarget
    if curr == nil then
        return ''
    end
    local currWidget = curr.transform:GetComponent("LuaUIWidget")
    if currWidget == nil or currWidget.table == nil then
        return ''
    end
    local medalSlot = currWidget.table[1]
    if medalSlot == nil or string.isEmpty(medalSlot.m_achievementId) then
        return ''
    end
    return medalSlot.m_achievementId
end

AchievementEditCtrl._SetNaviTarget = HL.Method(HL.Userdata, HL.Userdata, HL.Any) << function(self, naviGroup, target, focusMedal)
    if target == nil then
        return
    end
    self:SetNaviTarget(target)
    self:_UpdateNaviFocusMedal(focusMedal)
end

AchievementEditCtrl._FocusDisplaySlot = HL.Method(HL.Number) << function(self, slotIndex)
    if slotIndex <= 0 then
        return
    end
    local cellIndex = self.view.medalGroup:_GetMedalIndexBySlot(slotIndex)
    if cellIndex < 0 then
        return
    end
    self.view.medalGroup.view.slotList:ScrollToIndex(cellIndex, true)
    self.view.medalGroup.view.slotList:UpdateShowingCells(function(csIndex, obj)
        if csIndex ~= cellIndex then
            return
        end
        local cell = self.view.medalGroup.m_getMedalSlotCell(obj)
        self.view.medalGroup:_OnUpdateCell(cell, csIndex)
        self:_SetNaviTarget(self.view.naviMedalGroup, cell.view.medalSlot.view.button, self.m_editDisplay[slotIndex])
    end)
end

AchievementEditCtrl._FocusDepotIndex = HL.Method(HL.Number) << function(self, depotIndex)
    if depotIndex <= 0 then
        return
    end
    self.view.depotList:ScrollToIndex(depotIndex, true)
    self.view.depotList:UpdateShowingCells(function(csIndex, obj)
        local luaIndex = LuaIndex(csIndex)
        if luaIndex ~= depotIndex then
            return
        end
        local cell = self.m_depotListCellFunc(obj)
        self:_RenderDepotCell(cell, luaIndex)
        self:_SetNaviTarget(self.view.naviMedalDepot, cell.view.button, self.m_editDepot[depotIndex])
    end)
end

AchievementEditCtrl._FocusFirstItem = HL.Method() << function(self)
    for slotIndex = 1, Tables.achievementConst.maxDisplayPosition do
        if self.m_editDisplay[slotIndex] ~= nil then
            self:_FocusDisplaySlot(slotIndex)
            return
        end
    end
    if #self.m_editDepot > 0 then
        self:_FocusDepotIndex(1)
        return
    end
    self:_FocusDisplaySlot(1)
end

AchievementEditCtrl._RestoreNaviAfterDepotEdit = HL.Method(HL.String) << function(self, achievementId)
    if not DeviceInfo.usingController then
        return
    end
    if not string.isEmpty(achievementId) then
        local slotIndex = self:_FindEditDisplay(achievementId)
        if slotIndex > 0 then
            self:_FocusDisplaySlot(slotIndex)
            return
        end
        local depotIndex = self:_FindEditDepot(achievementId)
        if depotIndex > 0 then
            self:_FocusDepotIndex(depotIndex)
            return
        end
    end
    self:_FocusFirstItem()
end

AchievementEditCtrl._OpenDepot = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    local recoverNaviAchievementId = self:_GetCurrentNaviAchievementId()
    local currDepot = {}
    for slotIndex, medalBundle in pairs(self.m_editDisplay) do
        table.insert(currDepot, medalBundle.achievementId)
    end
    for _, medalBundle in ipairs(self.m_editDepot) do
        table.insert(currDepot, medalBundle.achievementId)
    end
    local args = {
        depot = currDepot,
        recoverState = recoverState,
        onConfirm = function(editDepot)
            self:_RefreshData(editDepot)
            self:_RenderViews(true)
            self:_RestoreNaviAfterDepotEdit(recoverNaviAchievementId)
        end
    }
    UIManager:Open(PanelId.AchievementDepot, args)
end

AchievementEditCtrl._SaveEditData = HL.Method() << function(self)
    if not self:_CheckDisplayDepotChanged() then
        return
    end
    self.m_isSaving = true
    local achievementSystem = GameInstance.player.achievementSystem
    local displayInfo = {}
    for slotIndex, medalBundle in pairs(self.m_editDisplay) do
        if slotIndex > 0 and medalBundle ~= nil then
            displayInfo[slotIndex] = medalBundle.achievementId
        end
    end
    local depotInfo = {}
    for _, depot in ipairs(self.m_editDepot) do
        if depot ~= nil then
            table.insert(depotInfo, depot.achievementId)
        end
    end

    local guestRoomId = Tables.spaceshipConst.guestRoomId
    local guestRoomTypeStr = tostring(GEnums.SpaceshipRoomType.GuestRoom)
    local beforeIds = {}
    local afterIds = {}
    local places = {}
    for i = 1, Tables.achievementConst.maxDisplayPosition do
        local medalPlayer = self.m_playerDisplay[i]
        local medalEdit = self.m_editDisplay[i]
        local achievementIdPlayer = medalPlayer ~= nil and medalPlayer.achievementId or nil
        local achievementIdEdit = medalEdit ~= nil and medalEdit.achievementId or nil
        if achievementIdPlayer ~= achievementIdEdit then
            table.insert(beforeIds, achievementIdPlayer == nil and '' or achievementIdPlayer)
            table.insert(afterIds, achievementIdEdit == nil and '' or achievementIdEdit)
            table.insert(places, tostring(i))
        end
    end
    if #places > 0 then
        EventLogManagerInst:GameEvent_PersonalDecoration(beforeIds, afterIds, places, "achievement_wall", guestRoomTypeStr, guestRoomId)
    end

    achievementSystem:SaveDisplayDepot(displayInfo, depotInfo)
end

AchievementEditCtrl._OnEditDisplayChanged = HL.Method() << function(self)
    local displayInfo = {}
    for slotIndex, medalBundle in pairs(self.m_editDisplay) do
        if slotIndex > 0 and medalBundle ~= nil then
            displayInfo[slotIndex] = medalBundle.achievementId
        end
    end
    GameInstance.player.achievementSystem:OnDisplayClientChanged(displayInfo)
end

AchievementEditCtrl._CheckDisplayDepotChanged = HL.Method().Return(HL.Boolean) << function(self)
    local depotChanged = self:_CheckDepotChanged()
    local displayChanged = self:_CheckDisplayChanged()
    return depotChanged or displayChanged
end

AchievementEditCtrl._CheckDepotChanged = HL.Method().Return(HL.Boolean) << function(self)
    if #self.m_editDepot ~= #self.m_playerDepot then
        return true
    end
    local depotCompareMap = {}
    for i, medalBundle in ipairs(self.m_playerDepot) do
        depotCompareMap[medalBundle.achievementId] = true
    end
    for i, medalBundle in ipairs(self.m_editDepot) do
        if depotCompareMap[medalBundle.achievementId] == true then
            depotCompareMap[medalBundle.achievementId] = nil
        end
    end
    for id, flag in pairs(depotCompareMap) do
        if flag == true then
            return true
        end
    end
    return false
end

AchievementEditCtrl._CheckDisplayChanged = HL.Method().Return(HL.Boolean) << function(self)
    local editCount = 0
    for slotIndex, medalBundle in pairs(self.m_editDisplay) do
        editCount = editCount + 1
    end
    local playerCount = 0
    for slotIndex, medalBundle in pairs(self.m_playerDisplay) do
        local editMedalBundle = self.m_editDisplay[slotIndex]
        if editMedalBundle == nil or editMedalBundle.achievementId ~= medalBundle.achievementId then
            return true
        end
        playerCount = playerCount + 1
    end
    if editCount ~= playerCount then
        return true
    end
    return false
end

AchievementEditCtrl._OnBackClick = HL.Method() << function(self)
    if self:_CheckDisplayChanged() then
        Notify(MessageConst.SHOW_POP_UP, {
            content = I18nUtils.GetText("ui_achv_edit_save_confirm"),
            onConfirm = function()
                self:_OnBackImpl()
            end,
        })
        return
    end
    self:_OnBackImpl()
end

AchievementEditCtrl._OnBackImpl = HL.Method() << function(self)
    if self.m_onClose ~= nil then
        self.m_onClose()
    end
    self:PlayAnimationOutAndClose()
end

AchievementEditCtrl._OnResetClick = HL.Method() << function(self)
    Notify(MessageConst.SHOW_POP_UP, {
        content = I18nUtils.GetText("ui_achv_edit_reset_confirm"),
        onConfirm = function()
            self:_ClearMedalGroup()
            self:_RenderViews(true)
        end,
    })
end

AchievementEditCtrl._OnDisplayUpdate = HL.Method() << function(self)
    self:_LoadData()
    self:_RenderViews(true)
    if self.m_isSaving then
        self.m_isSaving = false
        Notify(MessageConst.SHOW_TOAST, I18nUtils.GetText("ui_achv_edit_save_toast"))
    end
end

AchievementEditCtrl._OnClick = HL.Method(HL.Number, HL.String) << function(self, slotIndex, achievementId)
    if DeviceInfo.usingController then
        self:_OnNaviClick(slotIndex, achievementId)
    elseif slotIndex < 0 and not string.isEmpty(achievementId) then
        self:_OnDepotMedalClick(slotIndex, achievementId)
    end
end

AchievementEditCtrl._OnDepotMedalClick = HL.Method(HL.Number, HL.String) << function(self, slotIndex, achievementId)
    local depotIndex, depotMedal = self:_FindEditDepot(achievementId)
    if depotMedal == nil then
        return
    end
    local emptyIndex, emptyCellIndex = self.view.medalGroup:GetFirstEmptySlot()
    if emptyIndex < 0 then
        Notify(MessageConst.SHOW_TOAST, I18nUtils.GetText("ui_achv_edit_display_full_toast"))
        return
    end
    local dragInfo = {
        slotType = UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDepot,
        slotIndex = -1,
        achievementId = achievementId,
    }
    local dropInfo = {
        slotType = UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDisplay,
        slotIndex = emptyIndex,
    }
    self:_OnDropMedal(dragInfo, dropInfo)
end

AchievementEditCtrl._OnNaviClick = HL.Method(HL.Number, HL.String) << function(self, slotIndex, achievementId)
    if self.m_naviDragMedal == nil then

        self:_OnNaviDrag(slotIndex, achievementId)
    else

        self:_OnNaviDrop(slotIndex, achievementId)
    end
    self:_UpdateNaviDragMedal()
    self:_UpdateInputGroupStatus()
end

AchievementEditCtrl._OnNaviDrag = HL.Method(HL.Number, HL.String) << function(self, slotIndex, achievementId)
    if slotIndex > 0 then
        local displayMedal = self.m_editDisplay[slotIndex]
        if displayMedal == nil then
            return
        end
        self.m_naviDragMedal = displayMedal
        self.m_naviDragBeginSlot = slotIndex
        self.m_dragMedalId = achievementId
        self:_OnBeginDrag({
            slotType = UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDisplay,
            slotIndex = slotIndex,
            achievementId = achievementId,
        })
        self.view.medalGroup:InitNaviDragMedal(slotIndex)
    else
        local depotIndex, depotMedal = self:_FindEditDepot(achievementId)
        if depotMedal == nil then
            return
        end
        self.m_naviDragMedal = depotMedal
        self.m_naviDragBeginSlot = -1
        self.m_dragMedalId = achievementId
        self:_OnBeginDrag({
            slotType = UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDepot,
            slotIndex = -1,
            achievementId = achievementId,
        })
        self.view.depotList:UpdateShowingCells()
        self.view.medalGroup:InitNaviDragMedal()
    end
end

AchievementEditCtrl._OnNaviDrop = HL.Method(HL.Number, HL.String) << function(self, slotIndex, achievementId)
    local dropInfo = {
        slotType = slotIndex > 0 and UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDisplay or UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDepot,
        slotIndex = slotIndex,
        achievementId = achievementId,
    }
    local dragInfo = {
        slotType = self.m_naviDragBeginSlot > 0 and UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDisplay or UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDepot,
        slotIndex = self.m_naviDragBeginSlot > 0 and self.m_naviDragBeginSlot or -1,
        achievementId = self.m_dragMedalId,
    }
    self:_OnDropMedal(dragInfo, dropInfo)
    self:_OnEndDrag(dragInfo)
    self.m_naviDragMedal = nil
    self.m_naviDragBeginSlot = -1
    self.m_dragMedalId = ''
    self.view.medalGroup:ClearNaviDragMedal()
    if slotIndex > 0 then
        self:_UpdateNaviFocusMedal(self.m_editDisplay[slotIndex])
    end
end

AchievementEditCtrl._OnNaviDragCancel = HL.Method() << function(self)
    if self.m_naviDragBeginSlot > 0 then
        self.m_dragMedalId = ''
        self.view.medalGroup:ClearNaviDragMedal(self.m_naviDragBeginSlot)
        self:_OnEndDrag({
            slotType = UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDisplay,
            slotIndex = self.m_naviDragBeginSlot,
            achievementId = self.m_dragMedalId,
        })
        self:_UpdateNaviFocusMedal(self.m_editDisplay[self.m_naviDragBeginSlot])
    else
        self.view.medalGroup:ClearNaviDragMedal()
        local depotIndex, depotMedal = self:_FindEditDepot(self.m_dragMedalId)
        self.m_dragMedalId = ''
        if depotIndex > 0 then
            self.view.depotList:ScrollToIndex(depotIndex, true)
            self.view.depotList:UpdateShowingCells(function(csIndex, obj)
                local cell = self.m_depotListCellFunc(obj)
                local luaIndex = LuaIndex(csIndex)
                self:_RenderDepotCell(cell, luaIndex)
                if luaIndex == depotIndex then
                    self:SetNaviTarget(cell.view.button)
                    self:_UpdateNaviFocusMedal(self.m_editDepot[luaIndex])
                end
            end)
        end
        self:_OnEndDrag({
            slotType = UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDepot,
            slotIndex = -1,
            achievementId = self.m_dragMedalId,
        })
    end
    self.m_naviDragMedal = nil
    self.m_naviDragBeginSlot = -1
    self:_UpdateNaviDragMedal()
    self:_UpdateInputGroupStatus()
end

AchievementEditCtrl._OnNaviDragRetract = HL.Method() << function(self)
    if self.m_naviDragBeginSlot <= 0 then
        return
    end
    local dragInfo = {
        slotType = UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDisplay,
        slotIndex = self.m_naviDragBeginSlot,
        achievementId = self.m_dragMedalId,
    }
    local dropInfo = {
        slotType = UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDepot,
        slotIndex = -1,
    }
    self:_OnDropMedal(dragInfo, dropInfo)
    self:_OnEndDrag(dragInfo)

    local focusMedal = self.m_editDisplay[self.m_naviDragBeginSlot]
    self.view.medalGroup:ClearNaviDragMedal(self.m_naviDragBeginSlot)
    self.m_naviDragMedal = nil
    self.m_naviDragBeginSlot = -1
    self:_UpdateNaviFocusMedal(focusMedal)
    self:_UpdateInputGroupStatus()
end

AchievementEditCtrl._UpdateNaviFocusMedal = HL.Method(HL.Any) << function(self, focusMedal)
    self.m_naviFocusMedal = focusMedal
    self:_UpdateNaviDragMedal()
end

AchievementEditCtrl._UpdateNaviDragMedal = HL.Method() << function(self)
    self.view.naviDragMedal.medal.gameObject:SetActive(self.m_naviDragMedal ~= nil)
    self.view.naviDragMedal.medal:InitMedal(self.m_naviDragMedal)
    self.view.etchKeyHint.gameObject:SetActive(self.m_naviDragMedal == nil and self.m_naviFocusMedal ~= nil)
end

AchievementEditCtrl._UpdateNaviDragFocus = HL.Method(HL.Number) << function(self, slotIndex)
    local dragInfo = nil
    local dropInfo = nil
    if self.m_naviDragMedal ~= nil then
        dragInfo = {
            slotType = self.m_naviDragBeginSlot > 0 and UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDisplay or UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDepot,
            slotIndex = self.m_naviDragBeginSlot,
            achievementId = self.m_naviDragMedal.achievementId,
        }
    end
    dropInfo = {
        slotType = UIConst.ACHIEVEMENT_MEDAL_SLOT_TYPE.MedalDisplay,
        slotIndex = slotIndex,
        achievementId = self.m_naviFocusMedal ~= nil and self.m_naviFocusMedal.achievementId or '',
    }
    self:_OnDragMedal(dragInfo, dropInfo)
end

AchievementEditCtrl._UpdateNaviDragPos = HL.Method(HL.Any) << function(self, cell)
    if cell == nil or not self.view.inputGroup.groupEnabled then
        return
    end
    local cellRectTrans = cell.transform
    local bound = CSUtils.CalcBoundOfRectTransform(cellRectTrans, self.view.naviDragNode)
    self.view.naviDragMedal.transform.anchoredPosition = Vector2(bound.center.x, bound.center.y)
end

AchievementEditCtrl._UpdateInputGroupStatus = HL.Method() << function(self)
    InputManagerInst:ToggleGroup(self.view.buttonGroup.groupId, self.m_naviDragMedal == nil)
    InputManagerInst:ToggleGroup(self.m_naviCancelInputGroupId, self.m_naviDragMedal ~= nil)
    InputManagerInst:ToggleGroup(self.m_naviRetractInputGroupId, self.m_naviDragMedal ~= nil and self.m_naviDragBeginSlot > 0)
end

HL.Commit(AchievementEditCtrl)
