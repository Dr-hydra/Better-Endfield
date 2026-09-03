local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacSaveBlueprint

FacSaveBlueprintCtrl = HL.Class('FacSaveBlueprintCtrl', uiCtrl.UICtrl)






FacSaveBlueprintCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.FAC_ON_SAVE_BLUEPRINT] = 'FacOnSaveBlueprint',
    [MessageConst.FAC_ON_MODIFY_BLUEPRINT] = 'FacOnModifyBlueprint',
    [MessageConst.FAC_ON_FETCH_BLUEPRINT] = 'FacOnFetchBlueprint',
    [MessageConst.FAC_ON_GET_GIFT_BLUEPRINT] = 'FacOnGetGiftBlueprint',
    [MessageConst.FAC_ON_UNLOCK_TECH_TREE_UI] = 'OnRefreshTechTree',
    [MessageConst.ON_UNLOCK_FAC_TECH_PACKAGE] = 'OnRefreshTechTree',
    [MessageConst.FAC_ON_UNLOCK_TECH_TIER_UI] = 'OnRefreshTechTree',
}


FacSaveBlueprintCtrl.m_bpInst = HL.Field(CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintInstance)

FacSaveBlueprintCtrl.m_csBP = HL.Field(CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprint)

FacSaveBlueprintCtrl.m_isCreate = HL.Field(HL.Boolean) << false

FacSaveBlueprintCtrl.m_isEditing = HL.Field(HL.Boolean) << false


FacSaveBlueprintCtrl.m_isSharing = HL.Field(HL.Boolean) << false

FacSaveBlueprintCtrl.m_isImporting = HL.Field(HL.Boolean) << false


FacSaveBlueprintCtrl.m_blueprintID = HL.Field(HL.Any) << 0

FacSaveBlueprintCtrl.m_shareCode = HL.Field(HL.Any)

FacSaveBlueprintCtrl.m_fromFriend = HL.Field(HL.Boolean) << false


FacSaveBlueprintCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local recoverState = arg and arg.recoverState or nil
    self.view.cancelBtn.onClick:AddListener(function()
        self:_OnClickClose()
    end)
    self.view.closeBtn.onClick:AddListener(function()
        self:_OnClickClose()
    end)
    self.view.saveBtn.onClick:AddListener(function()
        self:_OnClickSave()
    end)

    local bpAbnormalIconHelper
    if arg then
        self.m_blueprintID = arg.id
        self.m_bpInst = arg.bpInst
        self.m_csBP = self.m_bpInst.info.bp
        self.m_isCreate = false
        self.m_isSharing = arg.isSharing or false
        self.m_isImporting = arg.isImporting or false
        self.m_isEditing = arg.bpInst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Mine and not self.m_isSharing
        self.m_fromFriend = arg.fromFriend or false
        if arg.shareCode then
            self.m_shareCode = arg.shareCode
        end
    else

        self.m_csBP = LuaSystemManager.factory:GetBlueprintFromBatchSelectTargets()
        self.m_isCreate = true
        self.m_isEditing = true
    end
    if not self.m_isSharing then
        bpAbnormalIconHelper = FactoryUtils.createBPAbnormalIconHelper()
    end

    if self.m_isSharing then
        self.view.titleTxt.text = Language.LUA_FAC_BLUEPRINT_SHARE_TITLE
    elseif self.m_isCreate then
        self.view.titleTxt.text = Language.LUA_FAC_BLUEPRINT_CREATE_TITLE
    elseif self.m_isEditing then
        self.view.titleTxt.text = Language.LUA_FAC_BLUEPRINT_EDIT_TITLE
    else
        self.view.titleTxt.text = Language.LUA_FAC_BLUEPRINT_VIEW_TITLE
    end
    self.view.blueprintContent.view.deviceCell.moreDeviceCell.gameObject:SetActive(false)
    if self.m_isSharing then
        self.view.closeBtn.gameObject:SetActive(false)
    else
        self.view.closeBtn.gameObject:SetActive(true)
    end

    self:_RefreshBlueprintContent()
    self.view.blueprintPreview:InitBlueprintPreview(self.m_csBP, self.m_isEditing, bpAbnormalIconHelper)

    self.view.main:SetState(self.m_isImporting and "Importing" or (self.m_isSharing and "Photo" or (self.m_isEditing and "Editing" or "ViewOnly")))
    if self.m_isSharing then
        self.animationWrapper:SampleToInAnimationEnd()
    end

    if DeviceInfo.usingController then
        self:_RefreshController()
    end

    self:_TryRecoverState(recoverState)
end

FacSaveBlueprintCtrl.GetRecoverStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    if not self.m_bpInst then
        return
    end
    local content = self.view.blueprintContent
    return {
        bpInst = self.m_bpInst,
        id = self.m_blueprintID,
        isSharing = self.m_isSharing,
        isImporting = self.m_isImporting,
        shareCode = self.m_shareCode,
        fromFriend = self.m_fromFriend,
        recoverState = content:GetRecoverStateArg(),
    }
end

FacSaveBlueprintCtrl.GetIsCreating = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_isCreate
end

FacSaveBlueprintCtrl._TryRecoverState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if not recoverState then
        return
    end
    self.view.blueprintContent:RecoverState(recoverState)
end

FacSaveBlueprintCtrl._RefreshController = HL.Method() << function(self)

    if self.m_isSharing then
        self:_SetActiveControllerMouse(false)
        return
    end


    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.view.blueprintContent:SetActiveControllerNode(self.m_isEditing and FacConst.FocusStateTable.UnFocused or FacConst.FocusStateTable.None)
    self.view.blueprintContent.view.topContainer.onIsFocusedChange:RemoveAllListeners()
    self.view.blueprintContent.view.topContainer.onIsFocusedChange:AddListener(function(isFocused)
        self:_SetActiveControllerMouse(not isFocused)
        self.view.blueprintContent:SetActiveControllerNode(isFocused and FacConst.FocusStateTable.Focused or FacConst.FocusStateTable.UnFocused)
        if isFocused then
            self:SetNaviTarget(self.view.blueprintContent.view.changeIconBtn)
        end
    end)
    self.view.blueprintContent.view.content.onIsFocusedChange:RemoveAllListeners()
    self.view.blueprintContent.view.content.onIsFocusedChange:AddListener(function(isFocused)
        self:_SetActiveControllerMouse(not isFocused)
    end)


    self.view.blueprintPreview.view.leftBottomNode.gameObject:SetActive(true)
    self:BindInputPlayerAction("fac_blueprint_move_mouse", function() end,self.view.inputGroup.groupId)


    if not self.m_bpInst or (self.m_bpInst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Mine) then
        self.view.blueprintContent.view.tagNode.button.interactable = true
        self.view.blueprintPreview.view.controllerEditBtn.gameObject:SetActive(true)
        self.view.blueprintPreview.view.controllerEditBtn.onClick:RemoveAllListeners()
        self.view.blueprintPreview.view.controllerEditBtn.onClick:AddListener(function()
            self.view.blueprintPreview:_OnClick()
        end)
    end

    if not self.m_bpInst then
        return
    end


    if (self.m_bpInst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Sys or self.m_bpInst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Preset)  then
        InputManagerInst:ToggleGroup(self.view.blueprintContent.view.topBinding.groupId,false)
    end


    if self.m_bpInst.sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Gift then
        local content = self.view.blueprintContent
        content.view.tagNode.button.interactable = false
        InputManagerInst:ToggleGroup(self.view.blueprintContent.view.topBinding.groupId,false)
        if FactoryUtils.isOtherPeopleGiftBlueprint(self.m_bpInst) then
            content.view.rightActions.moreBtn.button.onClick:RemoveAllListeners()
            content.view.rightActions.moreBtn.button.onClick:AddListener(function()
                self:_SetActiveControllerMouse(false)
                self.view.blueprintContent:_SeeMoreInfo({
                    onClose = function()
                        self:_SetActiveControllerMouse(true)
                    end
                })
            end)
        end
    end
end

FacSaveBlueprintCtrl._SetActiveControllerMouse = HL.Method(HL.Boolean) << function(self, active)
    self.view.blueprintPreview.mouseShow = active
    self.view.blueprintPreview.view.leftBottomNode.gameObject:SetActive(active)
    if not active then
        self.view.blueprintPreview:_CancelHover()
    end
end




FacSaveBlueprintCtrl._OnClickSave = HL.Method() << function(self)
    self:_CheckIsChangedAndDo(function(name, desc, icon, colorId, tagIds, timeLimitedFormulas)
        if self.m_isCreate then
            local csBPSys = GameInstance.player.remoteFactory.blueprint
            if csBPSys.myBlueprints.Count >= Tables.facBlueprintConst.MyBluePrintNumMax then
                Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BLUEPRINT_SAVE_FAIL_FOR_MAX_COUNT)
                self:PlayAnimationOutAndClose()
                return
            end
            self.view.blueprintPreview:ApplyIconChanges()
            GameInstance.player.remoteFactory.blueprint:SendSaveBlueprint(self.m_csBP, name, desc, icon, colorId, tagIds, timeLimitedFormulas)
        elseif self.m_isEditing then
            Notify(MessageConst.SHOW_POP_UP, {
                content = Language.LUA_FAC_BLUEPRINT_SAVE_CHANGE_HINT,
                warningContent = Language.LUA_FAC_BLUEPRINT_SAVE_CHANGE_WARNING_HINT,
                onConfirm = function()
                    local changedProdIconDic = self.view.blueprintPreview:GetChangedIcons()
                    GameInstance.player.remoteFactory.blueprint:SendModifyBlueprint(self.m_bpInst.param.myBpUid, name, desc, icon, colorId, tagIds, changedProdIconDic, timeLimitedFormulas)
                end
            })
        end
    end,function()
        self:PlayAnimationOutAndClose()
    end)
end

FacSaveBlueprintCtrl._OnClickClose = HL.Method() << function(self)
    self:_CheckIsChangedAndDo(function()
        Notify(MessageConst.SHOW_POP_UP, {
            content = Language.LUA_FAC_BLUEPRINT_CHANGE_NOT_SAVE_HINT,
            onConfirm = function()
                self:PlayAnimationOutAndClose()
            end
        })
    end, function()
        self:PlayAnimationOutAndClose()
    end)
end

FacSaveBlueprintCtrl._CheckIsChangedAndDo = HL.Method(HL.Function, HL.Opt(HL.Function)) << function(self, actionOnChange, actionOnNotChange)
    local contentView = self.view.blueprintContent.view
    local rawName = contentView.nameInputField.text


    local name = string.isEmpty(rawName) and Language.LUA_FAC_BLUEPRINT_DEFAULT_NAME or rawName
    local desc = contentView.descInputField.text
    local icon = self.view.blueprintContent.curIcon
    local colorId = self.view.blueprintContent.curColorId
    local tagIds = self.view.blueprintContent:GetSortedTagIds()
    local timeLimitedFormulas = self:_GetTimeLimitedFormulas()
    local isChanged
    if self.m_isCreate then
        isChanged = true
    elseif self.m_isEditing then





        if rawName ~= self.m_bpInst.info.name then
            isChanged = true
        elseif desc ~= self.m_bpInst.info.desc then
            isChanged = true
        elseif icon ~= self.m_bpInst.info.icon.icon then
            isChanged = true
        elseif colorId ~= self.m_bpInst.info.icon.baseColor then
            isChanged = true
        elseif self.view.blueprintPreview:HasIconChanged() then
            isChanged = true
        else
            local oldTagIds = self.m_bpInst.info.tags
            if #tagIds ~= oldTagIds.Count then
                isChanged = true
            else

                for k, id in ipairs(tagIds) do
                    if oldTagIds[CSIndex(k)] ~= id then
                        isChanged = true
                        break
                    end
                end
            end
        end
    end
    if isChanged then
        actionOnChange(name, desc, icon, colorId, tagIds, timeLimitedFormulas)
    elseif actionOnNotChange then
        actionOnNotChange()
    end
end

FacSaveBlueprintCtrl.FacOnModifyBlueprint = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BLUEPRINT_CHANGE_SAVED)
    self:PlayAnimationOutAndClose()
end

FacSaveBlueprintCtrl.FacOnSaveBlueprint = HL.Method() << function(self)
    Notify(MessageConst.FAC_EXIT_DESTROY_MODE, true)
    PhaseManager:OpenPhaseFast(PhaseId.FacBlueprint, { blueprintType = "Mine" })
    self:PlayAnimationOutAndClose()
    Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BLUEPRINT_SAVED)
end

FacSaveBlueprintCtrl.FacOnFetchBlueprint = HL.Method(HL.Any) << function(self, arg)
    Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BLUEPRINT_GIFT_SAVE_SUCCESS)
    self:PlayAnimationOutAndClose()
end





FacSaveBlueprintCtrl._UpdateSceneView = HL.Method() << function(self)



    local range = GameInstance.remoteFactoryManager.batchSelect.selectedRange
    local gridPos = range.center
    local width = range.width
    local height = range.height
    local rectInt = CS.UnityEngine.RectInt(gridPos.x - math.floor(width / 2), gridPos.y - math.floor(height / 2), width, height)
    CSFactoryUtil.SetSelectGrids(rectInt, CS.Beyond.Gameplay.Factory.GlobalSharedData.MapGridRendererData.MapGridInfo.SelectType.BLUEPRINT)

    local camTarns = CameraManager.mainCamera.transform
    local needReverse = lume.round(camTarns.localEulerAngles.y) % 180 ~= 0
    local horSize = needReverse and height or width
    local verSize = needReverse and width or height
    local horExtraSize = horSize * 0.3


    local camTarget = LuaSystemManager.factory.topViewCamTarget
    local targetPos = Vector3(range.center.x, camTarget.position.y, range.center.y)
    targetPos = targetPos + camTarns.right * horExtraSize
    camTarget:DOKill()
    camTarget:DOMove(targetPos, 0.5)


    local camCtrl = LuaSystemManager.factory.m_topViewCamCtrl
    camCtrl:AdjustCameraForRange(horSize + horExtraSize, verSize, function(pos)
        return FactoryUtils.clampTopViewCamTargetPosition(pos)
    end)
end

FacSaveBlueprintCtrl._ResetSceneView = HL.Method() << function(self)
    local camCtrl = LuaSystemManager.factory.m_topViewCamCtrl
    camCtrl:ResetCameraAdjustForRange()
    CSFactoryUtil.ClearSelectGrids()
end


FacSaveBlueprintCtrl.FacOnGetGiftBlueprint = HL.Method() << function(self)
    if not self:IsShow() then
        return
    end
    self:_RefreshImportState()
end

FacSaveBlueprintCtrl._RefreshImportState = HL.Method() << function(self)
    local found = false
    for _, v in pairs(GameInstance.player.remoteFactory.blueprint.giftBlueprintHandles) do
        if not v.loadedBlueprintInstance then

            self.view.importBtn.gameObject:SetActive(false)
            GameInstance.player.remoteFactory.blueprint:GetGiftBlueprint()
            return
        end
        local old = v.loadedBlueprintInstance.param
        local new = self.m_bpInst.param
        if old.bpGiftUid == new.bpGiftUid and old.shareIdx == new.shareIdx and old.targetRoleId == new.targetRoleId then
            found = true
            break
        end
    end

    if found then
        self.view.importBtn.gameObject:SetActive(false)
        self.view.savedNode.gameObject:SetActive(true)
        self.view.savedNode.onClick:AddListener(function()
            Notify(MessageConst.SHOW_TOAST, Language.LUA_WIKI_CRAFTING_BLUEPRINT_ALREADY_SAVED)
        end)
    else
        self.view.importBtn.onClick:RemoveAllListeners()
        self.view.importBtn.onClick:AddListener(function()
            GameInstance.player.remoteFactory.blueprint:SendFetchGiftBlueprint(self.m_shareCode)
        end)
        self.view.blueprintPreview.view.controllerEditBtn.interactable = false
        self.view.importBtn.gameObject:SetActive(true)
    end
end

FacSaveBlueprintCtrl._RefreshBlueprintContent = HL.Method() << function(self)
    local bpAbnormalIconHelper
    if not self.m_isEditing then
        bpAbnormalIconHelper = FactoryUtils.createBPAbnormalIconHelper()
    end
    self.view.blueprintContent:InitBlueprintContent(self.m_bpInst, self.m_blueprintID,self.m_csBP, self.m_isEditing, self.m_isSharing, bpAbnormalIconHelper)
    if self.m_isImporting then
        self:_RefreshImportState()
    end

    if self.m_isCreate then
        FactoryUtils.SetCreatorName(self.view.blueprintContent, true)
    end
end

FacSaveBlueprintCtrl.OnRefreshTechTree = HL.Method(HL.Opt(HL.Any)) << function(self, _)
    if self:IsShow() then
        self:_RefreshBlueprintContent()
        if DeviceInfo.usingController then
            self:_RefreshController()
        end
    end
end

FacSaveBlueprintCtrl._GetTimeLimitedFormulas = HL.Method().Return(HL.Table) << function(self)
    local helper = self.view.blueprintPreview.m_bpAbnormalIconHelper
    local timeLimitedFormulas = {}
    local changedProdIconDic = self.view.blueprintPreview:GetChangedIcons() or {}
    for _, entry in pairs(self.m_csBP.buildingNodes) do
        local buildingId = entry.templateId
        if Tables.factoryBuildingTable:ContainsKey(buildingId) then
            local itemId = changedProdIconDic[entry.nodeId] or entry.productIcon
            if not string.isEmpty(itemId) and not FactoryUtils.isBlueprintProductIconGasEnv(itemId) then
                local _, craftId = helper.GetAbnormalType(buildingId, itemId, true)
                if craftId and FactoryUtils.isTimeLimitedFormula(craftId) then
                    timeLimitedFormulas[craftId] = true
                end
            end
        end
    end
    local intIds = {}
    for craftId, _ in pairs(timeLimitedFormulas) do
        table.insert(intIds, CS.Beyond.Cfg.Tables.formulaIdToNum:GetValue(craftId))
    end
    logger.info("FacSaveBlueprintCtrl._GetTimeLimitedFormulas", timeLimitedFormulas, intIds)
    return intIds
end

HL.Commit(FacSaveBlueprintCtrl)
