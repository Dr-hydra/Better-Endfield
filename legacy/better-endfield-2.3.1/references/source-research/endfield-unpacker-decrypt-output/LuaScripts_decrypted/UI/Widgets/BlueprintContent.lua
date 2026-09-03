local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local hiddenTechId = "hidden-tech"

local BLUEPRINT_SHARE_STATEMENT_KEY = "blueprint_share_statement_key"

BlueprintContent = HL.Class('BlueprintContent', UIWidgetBase)


BlueprintContent.m_deviceInfos = HL.Field(HL.Table)

BlueprintContent.m_csBPInst = HL.Field(CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintInstance)

BlueprintContent.m_csBP = HL.Field(CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprint)

BlueprintContent.m_deviceCells = HL.Field(HL.Forward('UIListCache'))

BlueprintContent.isEditing = HL.Field(HL.Boolean) << false

BlueprintContent.isSharing = HL.Field(HL.Boolean) << false

BlueprintContent.curIcon = HL.Field(HL.String) << 'blueprint_default_icon'

BlueprintContent.curColorId = HL.Field(HL.Number) << -1

BlueprintContent.m_bpAbnormalIconHelper = HL.Field(HL.Table)


BlueprintContent.m_deviceCellMaxNumber = HL.Field(HL.Number) << 8

BlueprintContent.m_isNaviDevice = HL.Field(HL.Boolean) << true

BlueprintContent.m_inputFieldNum = HL.Field(HL.Number) << 1

BlueprintContent.m_blueprintID = HL.Field(HL.Any) << 0

BlueprintContent._BuildTechInfoList = HL.Method(HL.Any).Return(HL.Table) << function(self, techIds)
    local infoMap = {}
    local techSys = GameInstance.player.facTechTreeSystem
    for _, techId in pairs(techIds) do
        if techSys:NodeIsHidden(techId) then
            if not infoMap[hiddenTechId] then
                infoMap[hiddenTechId] = {
                    icon = "icon_industrial_plan_unknown_node",
                    name = Language.LUA_FAC_BLUEPRINT_HIDDEN_TECH_NAME,
                    desc = Language.LUA_FAC_BLUEPRINT_HIDDEN_TECH_DESC,
                    sortId1 = 3,
                }
            end
        else
            local techData = Tables.facSTTNodeTable[techId]
            if techData then
                if techSys:PackageIsLocked(techData.groupId) then
                    if not infoMap[techData.groupId] then
                        local data = Tables.facSTTGroupTable[techData.groupId]
                        infoMap[techData.groupId] = {
                            id = techData.groupId,
                            isGroup = true,
                            icon = data.icon,
                            name = data.groupName,
                            desc = data.desc,
                            sortId1 = 2,
                            sortId2 = techData.groupId,
                        }
                    end
                elseif techSys:LayerIsLocked(techData.layer) then
                    if not infoMap[techData.layer] then
                        local data = Tables.facSTTLayerTable[techData.layer]
                        infoMap[techData.layer] = {
                            id = techData.layer,
                            groupId = techData.groupId,
                            layerId = techData.layer,
                            isLayer = true,
                            icon = data.icon,
                            name = data.name,
                            desc = data.desc,
                            sortId1 = 1,
                            sortId2 = data.order,
                        }
                    end
                else
                    if not infoMap[techId] then
                        infoMap[techId] = {
                            id = techId,
                            icon = techData.icon,
                            name = techData.name,
                            desc = techData.desc,
                            sortId1 = 0,
                            sortId2 = techData.sortId,
                        }
                    end
                end
            end
        end
    end
    local infoList = {}
    for _, v in pairs(infoMap) do
        table.insert(infoList, v)
    end
    table.sort(infoList, Utils.genSortFunction({ "sortId1", "sortId2", "id" }, true))
    return infoList
end


BlueprintContent._OnFirstTimeInit = HL.Override() << function(self)
    self.m_deviceCells = UIUtils.genCellCache(self.view.deviceCell)
    self:_InitTag()
    self.view.changeIconBtn.onClick:AddListener(function()
        self:_ToggleChangeIcon(not self.view.changeIconNode.gameObject.activeSelf)
    end)
    self.view.changeIconNode.view.autoCloseArea.onTriggerAutoClose:AddListener(function()
        self:_ToggleChangeIcon(false)
    end)

    self.view.nameInputField.onValidateCharacterLimit = I18nUtils.GetRealTextByLengthLimit
    self.view.nameInputField.onGetTextLength = I18nUtils.GetTextRealLength

    self.view.descInputField.onValidateCharacterLimit = I18nUtils.GetRealTextByLengthLimit
    self.view.descInputField.onGetTextLength = I18nUtils.GetTextRealLength

    if BEYOND_DEBUG_COMMAND and UNITY_EDITOR then
        if GameInstance.player.remoteFactory.blueprint.debugBuiltinBlueprints then
            self.view.nameInputField.characterLimit = 0
            self.view.descInputField.characterLimit = 0
        end
    end

    self:RegisterMessage(MessageConst.FAC_ON_REVIEW_BLUEPRINT, function(arg)
        self:FacOnReviewBlueprint(arg)
    end)
end

BlueprintContent.InitBlueprintContent = HL.Method(HL.Opt(CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintInstance, HL.Any, CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprint, HL.Boolean, HL.Boolean, HL.Table)) << function(self, csBPInst, id, csBP, isEditing, isSharing, bpAbnormalIconHelper)
    self:_FirstTimeInit()

    self:ChangeIsEditing(isEditing)

    self.m_blueprintID = id
    self.m_csBPInst = csBPInst
    self.m_csBP = csBP or csBPInst.info.bp
    self.m_bpAbnormalIconHelper = bpAbnormalIconHelper
    self.isSharing = isSharing

    self:Refresh()
    self:_ToggleChangeIcon(false, true)
    if self.m_csBPInst then
        self:_InitShare()
    end
    self:_DisableAllInputField()
    if DeviceInfo.usingController then
        self:_InitController()
    end
end

BlueprintContent._InitController = HL.Method() << function(self)

    self.view.content.onDefaultNaviFailed:RemoveAllListeners()
    self.view.content.onDefaultNaviFailed:AddListener(function(dir)
        self:_FindTechAndDevice(dir)
    end)


    InputManagerInst:ChangeParent(true, self.view.techTipsNode.inputBindingGroupMonoTarget.groupId, self.view.contentInputBindingGroupMonoTarget.groupId)


    self.view.content.onIsFocusedChange:RemoveAllListeners()
    self.view.content.onIsFocusedChange:AddListener(function(isFocused)
        if isFocused then
            if #self.m_deviceInfos == 0 and #self.m_lackTechIdInfos == 0 and #self.m_previewTechIdInfos == 0 then
                Notify(MessageConst.SHOW_TOAST,Language.LUA_FAC_BLUEPRINT_NO_DETAIL_INFO)
                self.view.content:ManuallyStopFocus()
            elseif #self.m_lackTechIdInfos > 0 then
                self.m_techCells:Refresh(#self.m_lackTechIdInfos, function(cell, index)
                    if index == 1 then
                        self:SetNaviTarget(cell.button)
                    end
                end)
            elseif #self.m_previewTechIdInfos > 0 then
                self.m_previewTechCells:Refresh(#self.m_previewTechIdInfos, function(cell, index)
                    if index == 1 then
                        self:SetNaviTarget(cell.button)
                    end
                end)
            elseif #self.m_deviceInfos > 0 then
                self.m_deviceCells:Refresh(#self.m_deviceInfos, function(cell, index)
                    if index == 1 then
                        self:SetNaviTarget(cell.item.view.button)
                    end
                end)
            end
        else
            self:_CloseTechTips()
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)


    self.view.topContainer.onIsFocusedChange:RemoveAllListeners()
    self.view.topContainer.onIsFocusedChange:AddListener(function(isFocused)
        self:_DisableAllInputField()
    end)


    self:_InitControllerTopContainer()
end

BlueprintContent.SetActiveControllerNode = HL.Method(HL.Number) << function(self, state)
    if self.isEditing then
        self.view.saveBlueControllerNode.gameObject:SetActive(state == FacConst.FocusStateTable.Focused)
        self.view.saveBlueControllerNodeNotFocused.gameObject:SetActive(state == FacConst.FocusStateTable.UnFocused)
    end
end

BlueprintContent._DisableAllInputField = HL.Method() << function(self)
    self.view.nameInputFieldEditBgImage.gameObject:SetActive(false)
    self.view.descEditBgImage.gameObject:SetActive(false)
    InputManagerInst:ToggleBinding(self.view.nameInputField.activeInputBindingId, false)
    InputManagerInst:ToggleBinding(self.view.descInputField.activeInputBindingId, false)
end

BlueprintContent._InitControllerTopContainer = HL.Method() << function(self)
    self:_DisableAllInputField()

    self.view.nameInputField.onIsNaviTargetChanged = function(isTarget)
        InputManagerInst:ToggleBinding(self.view.nameInputField.activeInputBindingId, isTarget)
        InputManagerInst:ToggleBinding(self.view.descInputField.activeInputBindingId, not isTarget)
    end
    self.view.nameInputField.onFocused:AddListener(function(_)
        self.view.nameInputFieldEditBgImage.gameObject:SetActive(true)
    end)
    self.view.nameInputField.onEndEdit:AddListener(function(_)
        self.view.nameInputFieldEditBgImage.gameObject:SetActive(false)
    end)


    self.view.descInputFieldLightImage.gameObject:SetActive(false)
    self.view.descInputField.onIsNaviTargetChanged = function(isTarget)
        InputManagerInst:ToggleBinding(self.view.descInputField.activeInputBindingId, isTarget)
        InputManagerInst:ToggleBinding(self.view.nameInputField.activeInputBindingId, not isTarget)
    end
    self.view.descInputField.onFocused:AddListener(function(_)
        self.view.descEditBgImage.gameObject:SetActive(true)
    end)
    self.view.descInputField.onEndEdit:AddListener(function(_)
        self.view.descEditBgImage.gameObject:SetActive(false)
    end)


    self.view.tagNode.button.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self:_DisableAllInputField()
        else
            self.view.tagNode.inEditHint.gameObject:SetActive(false)
        end
    end


    self.view.changeIconBtn.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            self:_DisableAllInputField()
        end
    end
end

BlueprintContent._FindTechAndDevice = HL.Method(HL.Any) << function(self, dir)

    if not (dir == Unity.UI.NaviDirection.Left or dir == Unity.UI.NaviDirection.Right) then
        return
    end


    local currentTarget = self.view.content.LayerSelectedTarget
    local currentTargetIndex = -1
    local currentTargetType = -1
    self.m_techCells:Refresh(#self.m_lackTechIdInfos, function(cell, index)
        if currentTarget == cell.button then
            currentTargetIndex = index
            currentTargetType = 1
        end
    end)
    self.m_previewTechCells:Refresh(#self.m_previewTechIdInfos, function(cell, index)
        if currentTarget == cell.button then
            currentTargetIndex = index
            currentTargetType = 2
        end
    end)
    self.m_deviceCells:Refresh(#self.m_deviceInfos, function(cell, index)
        if currentTarget == cell.item.view.button then
            currentTargetIndex = index
            currentTargetType = 3
        end
    end)

    if currentTargetType < 0 then
        return
    end

    local groups = {
        { type = 1, count = #self.m_lackTechIdInfos },
        { type = 2, count = #self.m_previewTechIdInfos },
        { type = 3, count = #self.m_deviceInfos },
    }
    local currentGroupIndex = -1
    for i, group in ipairs(groups) do
        if group.type == currentTargetType then
            currentGroupIndex = i
            break
        end
    end


    local futureTargetIndex = currentTargetIndex + ((dir == Unity.UI.NaviDirection.Right) and 1 or -1)
    local futureTargetType = currentTargetType

    local function moveToNextAvailableGroup(step)
        local groupIndex = currentGroupIndex
        while true do
            groupIndex = groupIndex + step
            if groupIndex < 1 or groupIndex > #groups then
                return nil
            end
            if groups[groupIndex].count > 0 then
                return groups[groupIndex]
            end
        end
    end

    if futureTargetIndex > groups[currentGroupIndex].count then
        local nextGroup = moveToNextAvailableGroup(1)
        if not nextGroup then
            return
        end
        futureTargetIndex = 1
        futureTargetType = nextGroup.type
    elseif futureTargetIndex <= 0 then
        local prevGroup = moveToNextAvailableGroup(-1)
        if not prevGroup then
            return
        end
        futureTargetIndex = prevGroup.count
        futureTargetType = prevGroup.type
    end


    if futureTargetType == 1 then
        self.m_techCells:Refresh(#self.m_lackTechIdInfos, function(cell, index)
            if index == futureTargetIndex then
                self:SetNaviTarget(cell.button)
            end
        end)
    elseif futureTargetType == 2 then
        self.m_previewTechCells:Refresh(#self.m_previewTechIdInfos, function(cell, index)
            if index == futureTargetIndex then
                self:SetNaviTarget(cell.button)
            end
        end)
    elseif futureTargetType == 3 then
        self.m_deviceCells:Refresh(#self.m_deviceInfos, function(cell, index)
            if index == futureTargetIndex then
                self:SetNaviTarget(cell.item.view.button)
            end
        end)
    end
end

BlueprintContent.ChangeIsEditing = HL.Method(HL.Boolean) << function(self, isEditing)
    self.isEditing = isEditing
    self.view.stateController:SetState(isEditing and "Edit" or "View")
end

BlueprintContent.Refresh = HL.Method() << function(self)
    local deviceMap = {}

    if self.m_csBPInst then

        local bpInfo = self.m_csBPInst.info


        local range = self.m_csBP.sourceRect
        self.view.sizeTxt.text = string.format(Language.LUA_FAC_BLUEPRINT_RANGE_FORMAT_NUM_ONLY, range.width, range.height)



        local hasUnknowItem = false
        local hasAbnormal = false
        local hasExpired = false
        local inBlackbox = Utils.isInBlackbox()
        for _, entry in pairs(self.m_csBP.buildingNodes) do
            local buildingId = entry.templateId
            if Tables.factoryBuildingTable:ContainsKey(buildingId) then
                if deviceMap[buildingId] then
                    deviceMap[buildingId] = deviceMap[buildingId] + 1
                else
                    deviceMap[buildingId] = 1
                end
            else

                deviceMap[buildingId] = 0
            end
            if not self.isSharing and not self.isEditing and not inBlackbox and not string.isEmpty(entry.productIcon)
                    and not FactoryUtils.isBlueprintProductIconGasEnv(entry.productIcon) then
                if not GameInstance.player.inventory:IsItemFound(entry.productIcon) then
                    hasUnknowItem = true
                    hasAbnormal = true
                else
                    if self.m_bpAbnormalIconHelper then
                        local iconAbnormalType = self.m_bpAbnormalIconHelper.GetAbnormalType(buildingId, entry.productIcon)
                        if iconAbnormalType == FacConst.FAC_BP_ABNORMAL_ICON_TYPE.TimeLimitedExpired then
                            hasExpired = true
                        end
                        if iconAbnormalType == FacConst.FAC_BP_ABNORMAL_ICON_TYPE.Locked then
                            hasAbnormal = true
                        end
                    end
                end
            end
        end


        for _, entry in pairs(self.m_csBP.conveyorNodes) do
            deviceMap[entry.templateId] = 0
        end


        self.curSelectedTags = {}
        for _, tagId in pairs(bpInfo.tags) do
            self.curSelectedTags[tagId] = true
        end
        self:_RefreshContentTagCells()


        local t = self.m_csBPInst.sourceType

        self.view.nameTxt.text = bpInfo.name
        self.view.nameInputField.text = bpInfo.name

        local hideDesc = (t == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Gift and GameInstance.player.friendSystem.isCommunicationRestricted) or
            GameInstance.player.friendSystem:PlayerInBlackList(self.m_csBPInst.creatorRoleId)
        local desc = hideDesc and "" or bpInfo.desc
        if FactoryUtils.isPlayerBP(self.m_csBPInst) then
            self.view.descTxt.richText = false
            self.view.descTxt.text = desc
            self.view.descInputField.text = desc
        else
            self.view.descTxt.richText = true
            self.view.descTxt:SetAndResolveTextStyle(desc)
        end
        self:_OnChangeIcon(bpInfo.icon.icon, bpInfo.icon.baseColor)

        FactoryUtils.SetCreatorName(self, false, self.m_csBPInst)
        self:_RefreshLackTech()

        self.view.expiredNode.gameObject:SetActive(hasExpired)

        self.view.lowerTechWarnNode.gameObject:SetActive(self.haveLackTechs and self.canUseBP)

        if hasAbnormal then
            self.view.lackWarnNode.gameObject:SetActive(true)
            self.view.lackWarnTxt.text = hasUnknowItem and Language.LUA_FAC_BLUEPRINT_LACK_ITEM or Language.LUA_FAC_BLUEPRINT_LACK_FORMULA
        else
            self.view.lackWarnNode.gameObject:SetActive(false)
        end
    else

        local batchSelect = GameInstance.remoteFactoryManager.batchSelect
        local range = batchSelect.selectedRange


        local needReverse = lume.round(LuaSystemManager.factory.topViewCamTarget.eulerAngles.y) % 180 ~= 0
        self.view.sizeTxt.text = string.format(Language.LUA_FAC_BLUEPRINT_RANGE_FORMAT_NUM_ONLY, needReverse and range.height or range.width, needReverse and range.width or range.height)


        self:_OnChangeIcon(FacConst.FAC_BLUEPRINT_DEFAULT_ICON, FacConst.BLUEPRINT_DEFAULT_ICON_BG_COLOR_ID)


        local targets = LuaSystemManager.factory.batchSelectTargets
        for nodeId, _ in pairs(targets) do
            local node = FactoryUtils.getBuildingNodeHandler(nodeId)
            local buildingId = node.templateId
            if Tables.factoryBuildingTable:ContainsKey(buildingId) then
                if deviceMap[buildingId] then
                    deviceMap[buildingId] = deviceMap[buildingId] + 1
                else
                    deviceMap[buildingId] = 1
                end
            else
                deviceMap[buildingId] = 0
            end
        end


        self.curSelectedTags = {}
        self:_RefreshContentTagCells()


        self.view.creatorNode.gameObject:SetActive(false)

        self.view.lackInfoNode.gameObject:SetActive(false)
        self.view.previewTechNode.gameObject:SetActive(false)
        self.m_previewTechIdInfos = {}
        self.haveLackTechs = false
        self.canUseBP = false

        self.view.lackWarnNode.gameObject:SetActive(false)
    end


    self.m_deviceInfos = {}
    local sortByIsEnough = self.view.config.SORT_BY_IS_ENOUGH
    for templateId, count in pairs(deviceMap) do
        local itemId
        if count == 0 then
            if templateId == FacConst.BELT_ID then
                itemId = FacConst.BELT_ITEM_ID
            elseif templateId == FacConst.PIPE_ID then
                itemId = FacConst.PIPE_ITEM_ID
            else
                local data = FactoryUtils.getLogisticData(templateId)
                itemId = data.itemId
            end
        else
            itemId = FactoryUtils.getBuildingItemId(templateId)
        end
        if itemId then
            local itemData = Tables.itemTable[itemId]
            local info = {
                buildingId = templateId,
                id = itemData.id,
                sortId1 = itemData.sortId1,
                sortId2 = itemData.sortId2,
            }
            if count > 0 then
                info.count = count
            end
            if sortByIsEnough then
                local isEnough = count <= 0 or Utils.getItemCount(info.id, true, true) >= count
                info.isEnoughOrder = isEnough and 0 or 1
            end
            table.insert(self.m_deviceInfos, info)
        end
    end
    if sortByIsEnough then
        table.sort(self.m_deviceInfos, Utils.genSortFunction({ "isEnoughOrder", "sortId1", "sortId2", "id", "customSortId" }))
    else
        table.sort(self.m_deviceInfos, Utils.genSortFunction(UIConst.COMMON_ITEM_SORT_KEYS))
    end

    self.m_deviceCells:Refresh(#self.m_deviceInfos, function(cell, index)
        self:_OnUpdateCell(cell, index)
    end)
end

BlueprintContent._OnUpdateCell = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    if self.isSharing then
        if index == self.m_deviceCellMaxNumber and #self.m_deviceInfos - self.m_deviceCellMaxNumber > 0 then
            cell.countNode.gameObject:SetActive(false)
            cell.item.gameObject:SetActive(false)
            cell.deviceCountTxt.text = #self.m_deviceInfos - self.m_deviceCellMaxNumber + 1
            cell.moreDeviceCell.gameObject:SetActive(true)
            return
        elseif index > self.m_deviceCellMaxNumber then
            cell.gameObject:SetActive(false)
            return
        end
    end

    local info = self.m_deviceInfos[index]
    cell.item:InitItem(info, true)
    if self.isSharing then
        cell.item:HideSkipChapterMarks()
    end
    self:_UpdateCellCount(cell, index)

    if DeviceInfo.usingController then
        cell.item:SetExtraInfo({
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
            tipsPosTransform = self.view.content.transform,
            isSideTips = true,
        })
    end
end

BlueprintContent._UpdateCellCount = HL.Method(HL.Table, HL.Number) << function(self, cell, index)
    local info = self.m_deviceInfos[index]
    if self.view.config.SHOW_OWN_COUNT and info.count then
        local count = Utils.getItemCount(info.id, true, true)
        local isEnough = count >= info.count
        cell.countTxt.text = UIUtils.setCountColor(count, not isEnough)
        cell.countNode.gameObject:SetActive(true)
    else
        cell.countNode.gameObject:SetActive(false)
    end
end

BlueprintContent.UpdateCount = HL.Method() << function(self)
    if not self.m_deviceCells then
        return
    end
    self.m_deviceCells:Update(function(cell, index)
        self:_UpdateCellCount(cell, index)
    end)
end


BlueprintContent.GetFirstLackItemIdAndCount = HL.Method().Return(HL.Opt(HL.String, HL.Number)) << function(self)
    for _, info in ipairs(self.m_deviceInfos) do
        if info.count then
            local count = Utils.getItemCount(info.id, true, true)
            if count < info.count then
                return info.id, info.count - count
            end
        end
    end
end

BlueprintContent.GetAllDeviceIdAndCount = HL.Method().Return(HL.Table) << function(self)
    local ret = {}
    for _, info in ipairs(self.m_deviceInfos) do
        if info.count then
            local isEnough = info.count <= 0 or Utils.getItemCount(info.id, true, true) >= info.count
            if not isEnough then
                table.insert(ret, {
                    id = info.buildingId,
                    count = info.count
                })
            end
        end
    end
    return ret
end




BlueprintContent.m_tagGroupCells = HL.Field(HL.Forward('UIListCache'))

BlueprintContent.m_tagCells = HL.Field(HL.Forward('UIListCache'))

BlueprintContent.curSelectedTags = HL.Field(HL.Table)

BlueprintContent.m_tagGroupInfos = HL.Field(HL.Table)


BlueprintContent._InitTag = HL.Method() << function(self)
    self.view.tagNode.editBtn.onClick:AddListener(function()
        self:_OnClickEditTag()
    end)
    if DeviceInfo.usingController then
        self.view.tagNode.button.onClick:AddListener(function()
            self:_OnClickEditTag()
        end)
    end
    self.view.selectTagNode.autoCloseArea.onTriggerAutoClose:AddListener(function()
        self:_ToggleSelectTag(false)
    end)
    self:_ToggleSelectTag(false)
    self.m_tagCells = UIUtils.genCellCache(self.view.tagNode.tagCell)
end

BlueprintContent._OnClickEditTag = HL.Method() << function(self)
    if self.view.selectTagNode.gameObject.activeInHierarchy then
        self:_ToggleSelectTag(false)
    else
        self:_ToggleSelectTag(true)
    end
end

BlueprintContent._ToggleSelectTag = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, active, skipAni)
    local node = self.view.selectTagNode
    if not skipAni and not active then
        if node.animationWrapper.curState == CS.Beyond.UI.UIConst.AnimationState.Out then

            return
        end
    end
    if active then
        self:_ToggleChangeIcon(false, true)
        if not self.m_tagGroupCells then
            self.m_tagGroupCells = UIUtils.genCellCache(node.tagGroupCell)
            self.m_tagGroupInfos = FactoryUtils.getBlueprintTagGroupInfos()
        end
        self.m_tagGroupCells:Refresh(#self.m_tagGroupInfos, function(groupCell, index)
            local groupInfo = self.m_tagGroupInfos[index]
            groupCell.nameTxt.text = groupInfo.title
            if not groupCell.m_tagCells then
                groupCell.m_tagCells = UIUtils.genCellCache(groupCell.tagCell)
            end
            groupCell.m_tagCells:Refresh(#groupInfo.tags, function(tagCell, tagIndex)
                local tagInfo = groupInfo.tags[tagIndex]
                tagCell.nameTxt.text = tagInfo.name
                tagCell.button.onClick:RemoveAllListeners()
                tagCell.button.onClick:AddListener(function()
                    self:_OnClickTagCell(tagInfo, tagCell, groupCell)
                end)
                tagCell.stateController:SetState(self.curSelectedTags[tagInfo.id] and "Selected" or "Unselected")
                tagCell.button.hintTextId = self.curSelectedTags[tagInfo.id] and "key_hint_common_unselect" or "key_hint_common_select"
                Notify(MessageConst.REFRESH_CONTROLLER_HINT)
                tagCell.button.onIsNaviTargetChanged = function(isTarget)
                    if isTarget then
                        tagCell.button.hintTextId = self.curSelectedTags[tagInfo.id] and "key_hint_common_unselect" or "key_hint_common_select"
                        Notify(MessageConst.REFRESH_CONTROLLER_HINT)
                    end
                end
            end)
        end)

        local curCount = lume.count(self.curSelectedTags)
        local maxCount = Tables.facBlueprintConst.BluePrintTagNumMax
        node.countTxt.text = string.format("%s/%d", UIUtils.setCountColor(curCount, curCount >= maxCount), maxCount)
    end
    if skipAni then
        node.animationWrapper:ClearTween(false)
        node.gameObject:SetActive(active)
    else
        UIUtils.PlayAnimationAndToggleActive(node.animationWrapper, active)
    end
    self.view.tagNode.inEditHint.gameObject:SetActive(active)
    if active and DeviceInfo.usingController then
        node.contentSelectableNaviGroup:ManuallyFocus()
        node.contentSelectableNaviGroup.onIsFocusedChange:RemoveAllListeners()
        node.contentSelectableNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
            if not isFocused then
                self:_ToggleSelectTag(false)
            end
        end)
    end
end

BlueprintContent._OnClickTagCell = HL.Method(HL.Table, HL.Table, HL.Table) << function(self, tagInfo, tagCell, groupCell)
    local newValue = not self.curSelectedTags[tagInfo.id]
    local curCount
    local maxCount = Tables.facBlueprintConst.BluePrintTagNumMax
    if newValue then

        local typeData = Tables.factoryBlueprintTagTypeTable[tagInfo.type]
        if not typeData.allowMultiSelect then

            local sameTypeTagId
            for otherId, _ in pairs(self.curSelectedTags) do
                local data = Tables.factoryBlueprintTagTable[otherId]
                if data.type == tagInfo.type then
                    sameTypeTagId = otherId
                    break
                end
            end
            if sameTypeTagId then
                self.curSelectedTags[sameTypeTagId] = nil
                groupCell.m_tagCells:Update(function(cell, index)
                    if cell ~= tagCell then
                        cell.stateController:SetState("Unselected")
                        cell.button.hintTextId = "key_hint_common_select"
                        Notify(MessageConst.REFRESH_CONTROLLER_HINT)
                    end
                end)
            end
        end


        curCount = lume.count(self.curSelectedTags)
        if curCount == maxCount then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BLUEPRINT_MAX_TAG)
            return
        end

        self.curSelectedTags[tagInfo.id] = newValue
        curCount = curCount + 1
    else
        self.curSelectedTags[tagInfo.id] = nil
        curCount = lume.count(self.curSelectedTags)
    end
    self.view.selectTagNode.countTxt.text = string.format("%s/%d", UIUtils.setCountColor(curCount, curCount >= maxCount), maxCount)
    tagCell.stateController:SetState(newValue and "Selected" or "Unselected")
    tagCell.button.hintTextId = self.curSelectedTags[tagInfo.id] and "key_hint_common_unselect" or "key_hint_common_select"
    Notify(MessageConst.REFRESH_CONTROLLER_HINT)
    self:_RefreshContentTagCells()
end

BlueprintContent._RefreshContentTagCells = HL.Method() << function(self)
    local tagList = {}
    for id, _ in pairs(self.curSelectedTags) do
        local data = Tables.factoryBlueprintTagTable[id]
        table.insert(tagList, {
            id = id,
            data = data,
            sortId = data.sortId,
        })
    end
    table.sort(tagList, Utils.genSortFunction({ "sortId", "id" }))
    local count = #tagList
    if count == 0 then
        self.m_tagCells:Refresh(0)
        self.view.tagNode.addBtn.gameObject:SetActive(self.isEditing)
        self.view.tagNode.emptyNode.gameObject:SetActive(not self.isEditing)
        self.view.tagNode.changeHint.gameObject:SetActive(false)
    else
        self.m_tagCells:Refresh(count, function(cell, index)
            local info = tagList[index]
            cell.nameTxt.text = info.data.name
        end)
        self.view.tagNode.addBtn.gameObject:SetActive(false)
        self.view.tagNode.emptyNode.gameObject:SetActive(false)
        self.view.tagNode.changeHint.gameObject:SetActive(self.isEditing)
    end
end

BlueprintContent.GetSortedTagIds = HL.Method().Return(HL.Table) << function(self)
    local tagList = {}
    for id, _ in pairs(self.curSelectedTags) do
        table.insert(tagList, id)
    end
    table.sort(tagList)
    return tagList
end

BlueprintContent.GetRecoverStateArg = HL.Method().Return(HL.Table) << function(self)
    return {
        nameInputText = self.view.nameInputField.text,
        descInputText = self.view.descInputField.text,
        icon = self.curIcon,
        colorId = self.curColorId,
        tagIds = self:GetSortedTagIds(),
        isSelectTagOpen = self.view.selectTagNode.gameObject.activeSelf,
        isChangeIconOpen = self.view.changeIconNode.gameObject.activeSelf,
        isChangeIconTab = self.view.changeIconNode:IsIconTabSelected(),
    }
end

BlueprintContent.RecoverState = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if not recoverState then
        return
    end
    if recoverState.nameInputText ~= nil then
        self.view.nameInputField.text = recoverState.nameInputText
    end
    if recoverState.descInputText ~= nil then
        self.view.descInputField.text = recoverState.descInputText
    end
    if not string.isEmpty(recoverState.icon) then
        self:_OnChangeIcon(recoverState.icon, recoverState.colorId or self.curColorId)
    end
    if recoverState.tagIds then
        self.curSelectedTags = {}
        for _, tagId in ipairs(recoverState.tagIds) do
            self.curSelectedTags[tagId] = true
        end
        self:_RefreshContentTagCells()
    end

    local needRecoverController = recoverState.isChangeIconOpen or recoverState.isSelectTagOpen
    if needRecoverController and DeviceInfo.usingController then
        self.view.topContainer:ManuallyFocus()
        self:SetActiveControllerNode(FacConst.FocusStateTable.Focused)
    end

    if recoverState.isChangeIconOpen then
        self:_ToggleChangeIcon(true, true)
        if recoverState.isChangeIconTab == false then
            self.view.changeIconNode:SetTabState(false)
        end
    elseif recoverState.isSelectTagOpen then
        self:_ToggleSelectTag(true, true)
    end
end






BlueprintContent._ToggleChangeIcon = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, active, skipAni)
    local node = self.view.changeIconNode
    if not skipAni and not active then
        if node.view.animationWrapper.curState == CS.Beyond.UI.UIConst.AnimationState.Out then

            return
        end
    end
    if active then
        if not node.m_csBP then
            node:InitBlueprintChangeIconNode(self.m_csBP, self.curIcon, self.curColorId, function(iconId, colorId)
                self:_OnChangeIcon(iconId, colorId)
            end)
        end
    end

    if skipAni then
        node.view.animationWrapper:ClearTween(false)
        node.gameObject:SetActive(active)
    else
        UIUtils.PlayAnimationAndToggleActive(node.view.animationWrapper, active)
    end

    if active then
        self:_ToggleSelectTag(false, true)
    end

    if active and DeviceInfo.usingController then
        node:RefreshController()
        node.view.changeIconNodeMain.onIsFocusedChange:RemoveAllListeners()
        node.view.changeIconNodeMain.onIsFocusedChange:AddListener(function(isFocused)
            if not isFocused then
                self:_ToggleChangeIcon(false)
                self.view.inChangeIconHint.gameObject:SetActive(false)
            end
        end)
    end
    self.view.inChangeIconHint.gameObject:SetActive(active)
end

BlueprintContent._OnChangeIcon = HL.Method(HL.String, HL.Number) << function(self, iconId, colorId)
    self.curIcon = iconId
    self.curColorId = colorId
    self.view.blueprintIcon:InitBlueprintIcon(iconId, colorId)
end






BlueprintContent.canUseBP = HL.Field(HL.Boolean) << false

BlueprintContent.m_lackTechIdInfos = HL.Field(HL.Table)

BlueprintContent.m_techCells = HL.Field(HL.Forward('UIListCache'))

BlueprintContent.m_previewTechIdInfos = HL.Field(HL.Table)

BlueprintContent.m_previewTechCells = HL.Field(HL.Forward('UIListCache'))

BlueprintContent.haveLackTechs = HL.Field(HL.Boolean) << false

BlueprintContent._InitLackTechIdInfos = HL.Method() << function(self)
    local canUseBP, lockedTechIds, previewTechIds
    if Utils.isInBlackbox() then
        canUseBP = true
        lockedTechIds = {}
        previewTechIds = {}
    else
        local domainId = ScopeUtil.GetCurrentChapterIdAsStr()
        canUseBP, lockedTechIds, previewTechIds = GameInstance.player.remoteFactory.blueprint:GetLockedTechIdsInBlueprint(self.m_csBP, domainId)
    end
    self.m_lackTechIdInfos = self:_BuildTechInfoList(lockedTechIds)


    self.m_previewTechIdInfos = {}
    self.canUseBP = canUseBP
end

BlueprintContent._RefreshLackTech = HL.Method() << function(self)
    if self.isSharing then
        self.view.lackInfoNode.gameObject:SetActive(false)
        self.view.previewTechNode.gameObject:SetActive(false)
        return
    end

    self:_InitLackTechIdInfos()
    if not self.m_techCells then
        self:_InitLackTechNode()
    end
    local count = #self.m_lackTechIdInfos
    if count > 0 then
        self.haveLackTechs = true
        self.m_techCells:Refresh(count, function(cell, index)
            cell.button.onIsNaviTargetChanged = function(isTarget)
                if isTarget then
                    Notify(MessageConst.HIDE_ITEM_TIPS)
                    self:_OnClickTechCell(index)
                else
                    cell.selectedBG.gameObject:SetActive(false)
                    self:_CloseTechTips()
                end
            end
            local info = self.m_lackTechIdInfos[index]
            cell.button.onClick:RemoveAllListeners()
            cell.button.onClick:AddListener(function()
                if cell.selectedBG.gameObject.activeSelf then
                    self:_CloseTechTips()
                else
                    self:_OnClickTechCell(index)
                end
            end)
            cell.icon:LoadSprite(UIConst.UI_SPRITE_FAC_TECH_ICON, info.icon)
            cell.selectedBG.gameObject:SetActive(false)
        end)
        self.view.lackInfoNode.gameObject:SetActive(true)
    else
        self.haveLackTechs = false
        self.view.lackInfoNode.gameObject:SetActive(false)
    end
    self:_RefreshPreviewTech()
end

BlueprintContent._RefreshPreviewTech = HL.Method() << function(self)
    if not self.m_previewTechCells then
        self.m_previewTechCells = UIUtils.genCellCache(self.view.previewTechCell)
    end
    local count = #self.m_previewTechIdInfos
    if count > 0 then
        self.m_previewTechCells:Refresh(count, function(cell, index)
            cell.button.onIsNaviTargetChanged = function(isTarget)
                if isTarget then
                    Notify(MessageConst.HIDE_ITEM_TIPS)
                    self:_OnClickPreviewTechCell(index)
                else
                    cell.selectedBG.gameObject:SetActive(false)
                    self:_CloseTechTips()
                end
            end
            local info = self.m_previewTechIdInfos[index]
            cell.button.onClick:RemoveAllListeners()
            cell.button.onClick:AddListener(function()
                if cell.selectedBG.gameObject.activeSelf then
                    self:_CloseTechTips()
                else
                    self:_OnClickPreviewTechCell(index)
                end
            end)
            cell.icon:LoadSprite(UIConst.UI_SPRITE_FAC_TECH_ICON, info.icon)
            cell.selectedBG.gameObject:SetActive(false)
        end)
        self.view.previewTechNode.gameObject:SetActive(true)
    else
        self.view.previewTechNode.gameObject:SetActive(false)
    end
end

BlueprintContent._InitLackTechNode = HL.Method() << function(self)
    self.m_techCells = UIUtils.genCellCache(self.view.lackInfoCell)

    local tipsNode = self.view.techTipsNode
    tipsNode.gameObject:SetActive(false)
    tipsNode.transform:SetParent(self:GetUICtrl().view.transform)
    tipsNode.autoCloseArea.onTriggerAutoClose:AddListener(function()
        self:_CloseTechTips()
    end)
end

BlueprintContent._CloseTechTips = HL.Method(HL.Opt(HL.Boolean)) << function(self, skipAni)
    local tipsNode = self.view.techTipsNode
    if skipAni then
        tipsNode.animationWrapper:ClearTween(false)
        tipsNode.gameObject:SetActive(false)
        tipsNode.previewTagNode.gameObject:SetActive(false)
    else
        UIUtils.PlayAnimationAndToggleActive(tipsNode.animationWrapper, false, function()
            tipsNode.previewTagNode.gameObject:SetActive(false)
        end)
    end
    if self.m_techCells then
        self.m_techCells:Update(function(cell)
            cell.selectedBG.gameObject:SetActive(false)
        end)
    end
    if self.m_previewTechCells then
        self.m_previewTechCells:Update(function(cell)
            cell.selectedBG.gameObject:SetActive(false)
        end)
    end
end

BlueprintContent._ShowTechInfoTips = HL.Method(HL.Table, HL.Number, HL.Boolean) << function(self, info, index, isPreview)
    local cellCache = isPreview and self.m_previewTechCells or self.m_techCells
    local infoList = isPreview and self.m_previewTechIdInfos or self.m_lackTechIdInfos
    if index > #infoList then
        self.view.content:ManuallyStopFocus()
        return
    end
    local tipsNode = self.view.techTipsNode

    tipsNode.titleTxt.text = info.name
    tipsNode.descTxt:SetAndResolveTextStyle(info.desc)
    tipsNode.iconImg:LoadSprite(UIConst.UI_SPRITE_FAC_TECH_ICON, info.icon .. "_big")
    tipsNode.previewTagNode.gameObject:SetActive(isPreview)

    local lockedText
    tipsNode.jumpBtn.onClick:RemoveAllListeners()
    if not info.id then

        lockedText = Language.LUA_FAC_BLUEPRINT_HIDDEN_TECH_LOCKED_HINT
    elseif info.isGroup then

        lockedText = string.format(Language.LUA_FAC_BLUEPRINT_TECH_PACKAGE_LOCKED_HINT, info.name)
    elseif info.isLayer then

        tipsNode.jumpBtn.onClick:AddListener(function()
            PhaseManager:OpenPhase(PhaseId.FacTechTree, { layerId = info.layerId })
        end)
    else

        tipsNode.jumpBtn.onClick:AddListener(function()
            PhaseManager:OpenPhase(PhaseId.FacTechTree, { techId = info.id })
        end)
    end
    if lockedText then
        tipsNode.lockedNode.gameObject:SetActive(true)
        tipsNode.jumpNode.gameObject:SetActive(false)
        tipsNode.lockedTxt.text = lockedText
    else
        tipsNode.lockedNode.gameObject:SetActive(false)
        tipsNode.jumpNode.gameObject:SetActive(true)
    end

    UIUtils.PlayAnimationAndToggleActive(tipsNode.animationWrapper, true)

    local curCell
    cellCache:Update(function(cell, k)
        if k == index then
            cell.selectedBG.gameObject:SetActive(true)
            curCell = cell
        else
            cell.selectedBG.gameObject:SetActive(false)
        end
    end)
    tipsNode.autoCloseArea.tmpSafeArea = curCell.transform

    local panelCtrl = self:GetUICtrl()
    UIUtils.updateTipsPosition(tipsNode.transform, curCell.transform, panelCtrl.view.transform, panelCtrl.uiCamera, UIConst.UI_TIPS_POS_TYPE.LeftTop)
end

BlueprintContent._OnClickTechCell = HL.Method(HL.Number) << function(self, index)
    if index > #self.m_lackTechIdInfos then
        self.view.content:ManuallyStopFocus()
        return
    end
    self:_ShowTechInfoTips(self.m_lackTechIdInfos[index], index, false)
end

BlueprintContent._OnClickPreviewTechCell = HL.Method(HL.Number) << function(self, index)
    if index > #self.m_previewTechIdInfos then
        self.view.content:ManuallyStopFocus()
        return
    end
    self:_ShowTechInfoTips(self.m_previewTechIdInfos[index], index, true)
end

BlueprintContent.LocateFirstLackTech = HL.Method() << function(self)
    local info = self.m_lackTechIdInfos and self.m_lackTechIdInfos[1]
    if not info then
        return
    end
    if info.isLayer then
        PhaseManager:OpenPhase(PhaseId.FacTechTree, { layerId = info.layerId })
    elseif info.id and not info.isGroup then
        PhaseManager:OpenPhase(PhaseId.FacTechTree, { techId = info.id })
    else


        Notify(MessageConst.SHOW_TOAST, Language.LUA_FAC_BLUEPRINT_LACK_TECH_NEED_MAINLINE)
    end
end





BlueprintContent._InitShare = HL.Method() << function(self)
    self.view.deviceCell.moreDeviceCell.gameObject:SetActive(false)

    local isOtherPeopleGift = FactoryUtils.isOtherPeopleGiftBlueprint(self.m_csBPInst)
    self.view.rightActions.moreBtn.gameObject:SetActive(isOtherPeopleGift)
    if isOtherPeopleGift then
        self.view.rightActions.moreBtn.button.onClick:AddListener(function()
            self:_SeeMoreInfo()
        end)
    end
    self:_RefreshShareState()
end

BlueprintContent._SeeMoreInfo = HL.Method(HL.Opt(HL.Any)) << function(self,arg)
    local onClose
    if arg and arg.onClose then
        onClose = arg.onClose
    end
    if not UIManager:IsOpen(PanelId.BlueprintShareBlackScreen) then
        UIManager:Open(PanelId.BlueprintShareBlackScreen,{
            csBPInst = self.m_csBPInst,
            id = self.m_blueprintID,
            tipsTransform = self.view.rightActions.moreBtn.rectTransform,
            isReporting = true,
            onClose = onClose,
            deviceInfo = self.m_deviceInfos,
        })
    end

end

BlueprintContent._RefreshShareState = HL.Method() << function(self)
    if Utils.isInBlackbox() then
        self.view.rightActions.shareNode.gameObject:SetActive(false)
        return
    end

    local reviewStatus = self.m_csBPInst.reviewStatus
    local sourceType = self.m_csBPInst.sourceType
    if sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Gift or sourceType == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintSourceType.Sys then
        reviewStatus = CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintReviewStatus.Approved
    end
    self.view.rightActions.shareNode.shareBtn.onClick:RemoveAllListeners()
    self.view.rightActions.shareNode.shareBtn.gameObject:SetActive(true)

    if reviewStatus == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintReviewStatus.Approved then
        self.view.rightActions.shareNode.stateController:SetState("CanShare")
        self.view.rightActions.shareNode.shareBtn.onClick:AddListener(function()
            UIManager:Open(PanelId.BlueprintShareBlackScreen,{
                csBPInst = self.m_csBPInst,
                id = self.m_blueprintID,
                tipsTransform = self.view.rightActions.shareNode.canShareNode,
                isReporting = false,
                deviceInfo = self.m_deviceInfos,
            })
        end)
    elseif reviewStatus == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintReviewStatus.InProgress then
        self.view.rightActions.shareNode.stateController:SetState("InAudit")
        self.view.rightActions.shareNode.shareBtn.onClick:AddListener(function()
            Notify(MessageConst.SHOW_TOAST,Language.LUA_FAC_BLUEPRINT_PENDING_CANT_SHARE)
        end)
    else
        self.view.rightActions.shareNode.stateController:SetState("NeedSubmit")
        self.view.rightActions.shareNode.shareBtn.onClick:AddListener(function()
            self:_PendingShare()
        end)
    end

    if GameInstance.player.remoteFactory.blueprint.forbidShare or GameInstance.player.friendSystem.isCommunicationRestricted then
        self.view.rightActions.shareNode.shareBtn.onClick:RemoveAllListeners()
        self.view.rightActions.shareNode.shareBtn.onClick:AddListener(function()
            Notify(MessageConst.SHOW_TOAST,Language.LUA_BLUEPRINT_BAN_SHARE)
        end)
        return
    end
end

BlueprintContent._PendingShare = HL.Method() << function(self)
    self.view.topContainer:ManuallyStopFocus()
    Notify(MessageConst.SHOW_POP_UP, {
        content = Language.LUA_FAC_BLUEPRINT_REVIEW_POPUP,
        warningContent = Language.LUA_FAC_BLUEPRINT_REVIEW_POPUP_WARNING,
        onConfirm = function()
            UIManager:Close(PanelId.BlueprintShareBlackScreen)
            UIManager:Close(PanelId.CommonPopUp)
            local alreadyReadStatement = ClientDataManagerInst:GetBool(BLUEPRINT_SHARE_STATEMENT_KEY,false)
            if not alreadyReadStatement then
                UIManager:Open(PanelId.InstructionBook, {
                    id = "fac_blueprint_statement",
                })
                ClientDataManagerInst:SetBool(BLUEPRINT_SHARE_STATEMENT_KEY, true, false, EClientDataTimeValidType.Permanent)
            end
            GameInstance.player.remoteFactory.blueprint:SendReviewBlueprint(self.m_blueprintID)
        end,
        onCancel = function()
            UIManager:Close(PanelId.BlueprintShareBlackScreen)
        end
    })
end

BlueprintContent.FacOnReviewBlueprint = HL.Method(HL.Table) << function(self, arg)
    local _, status = unpack(arg)
    if status == CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryBlueprintReviewStatus.InProgress then
        Notify(MessageConst.SHOW_TOAST,Language.LUA_FAC_BLUEPRINT_SHARE_STATEMENT_TIP)
    end
    self:_RefreshShareState()
end

BlueprintContent.SetFriendShareState = HL.Method() << function(self)
    self.view.rightActions.moreBtn.gameObject:SetActive(false)
    self.view.rightActions.buttonNode.gameObject:SetActive(false)
    self.view.rightActions.shareNode.shareBtn.gameObject:SetActive(false)
    self:SetActiveControllerNode(FacConst.FocusStateTable.None)
    InputManagerInst:ToggleGroup(self.view.topBinding.groupId,false)
end



HL.Commit(BlueprintContent)
return BlueprintContent
