local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WikiCraftingTreeBuilding = HL.Class('WikiCraftingTreeBuilding', UIWidgetBase)

local ActionOnSetNaviTarget = CS.Beyond.Input.ActionOnSetNaviTarget

WikiCraftingTreeBuilding.m_args = HL.Field(HL.Table)

WikiCraftingTreeBuilding.m_isDefaultCraft = HL.Field(HL.Boolean) << false

WikiCraftingTreeBuilding.m_isPinnedCraft = HL.Field(HL.Boolean) << false

WikiCraftingTreeBuilding.m_hasWiki = HL.Field(HL.Boolean) << false

WikiCraftingTreeBuilding.m_isTimeLimitedFormula = HL.Field(HL.Boolean) << false


WikiCraftingTreeBuilding._OnFirstTimeInit = HL.Override() << function(self)
    if self.m_args.onClicked then
        self.view.button.onClick:AddListener(function()
            if not self.m_hasWiki then
                return
            end
            self.m_args.onClicked(self.m_args.craftInfo.buildingId, self)
        end)
    end
    self:RegisterMessage(MessageConst.CHANGE_WIKI_DEFAULT_CRAFT, function()
        if self.m_args.isShowDefaultNode then
            self:RefreshDefaultNode()
        end
    end)
    self.view.selectNode.cutBtn.onClick:AddListener(function()
        if not self.m_hasWiki then
            return
        end
        Notify(MessageConst.CHANGE_WIKI_CRAFTING_TREE, FactoryUtils.getBuildingItemId(self.m_args.craftInfo.buildingId))
    end)
end














WikiCraftingTreeBuilding.InitWikiCraftingTreeBuilding = HL.Method(HL.Table) << function(self, args)
    self.m_args = args
    self:_FirstTimeInit()

    local hasTime = args.craftInfo.time and args.craftInfo.time > 0
    self.view.timeNode.gameObject:SetActive(hasTime)
    if hasTime then
        self.view.timeTxt.text = string.format(Language["LUA_CRAFT_CELL_STANDARD_TIME"], FactoryUtils.getCraftTimeStr(args.craftInfo.time))
    end

    local name, iconFolder, iconId
    local buildingId = args.craftInfo.buildingId
    if not buildingId then
        name = Language.LUA_OBTAIN_WAYS_MANUAL_CRAFT_NAME
        iconFolder = UIConst.UI_SPRITE_ITEM_TIPS
        iconId = UIConst.UI_MANUALCRAFT_ICON_ID
    else
        local _, buildingData = Tables.factoryBuildingTable:TryGetValue(buildingId)
        if buildingData then
            name = buildingData.name
            iconFolder = UIConst.UI_SPRITE_FAC_BUILDING_PANEL_ICON
            iconId = buildingData.iconOnPanel
        end
    end
    local itemId = buildingId and FactoryUtils.getBuildingItemId(buildingId)
    if self.view.earlyAccessNode then
        self.view.earlyAccessNode.gameObject:SetActiveIfNecessary(
            not string.isEmpty(itemId) and FactoryUtils.isSkipUnlockedBuildingByItemId(itemId))
    end
    self.m_hasWiki = WikiUtils.getWikiEntryIdFromItemId(itemId) ~= nil
    self.view.stateController:SetState(self.m_hasWiki and "Selectable" or "Unselectable")
    self.view.button:ChangeActionOnSetNaviTarget(self.m_hasWiki and ActionOnSetNaviTarget.PressConfirmTriggerOnClick or ActionOnSetNaviTarget.None)
    if name then
        self.view.iconImg:LoadSprite(iconFolder, iconId)
        self.view.titleTxt.text = name
    end
    self.view.extraItemNode.gameObject:SetActive(args.isShowExtraItemIcon)
    self:SetSelected(false)

    local modeStateName = "normal"
    if not string.isEmpty(args.craftInfo.formulaMode) then
        modeStateName = args.craftInfo.formulaMode
    end
    self.view.modeStateCtrl:SetState(modeStateName)


    local pinnedCraftId
    local chapterInfo = FactoryUtils.getCurChapterInfo()
    if chapterInfo then
        pinnedCraftId = CS.Beyond.Gameplay.RemoteFactory.RemoteFactoryUtil.GetPinBoardStrId(chapterInfo.pinBoard, GEnums.FCPinPosition.Formula:GetHashCode())
    end
    self.m_isPinnedCraft = pinnedCraftId ~= nil and pinnedCraftId == self.m_args.craftInfo.craftId
    local isShowDefaultNode = (args.isShowDefaultNode == true or (self.m_isPinnedCraft and not args.ignorePinCraft)) and buildingId ~= nil
    self.view.setDefaultNode.gameObject:SetActive(isShowDefaultNode)
    if isShowDefaultNode then
        self:RefreshDefaultNode()
        self.view.setDefaultNode.button.onClick:RemoveAllListeners()
        self.view.setDefaultNode.button.onClick:AddListener(function()
            if self.m_isPinnedCraft or self.m_isDefaultCraft then
                return
            end
            WikiUtils.setUserItemDefaultCraftId(args.itemId, args.craftInfo.craftId)
            Notify(MessageConst.CHANGE_WIKI_DEFAULT_CRAFT)
        end)
    end


    if self.view.limitedTimeEventNode then
        if self.m_args.craftInfo.craftId ~= nil then
            self.m_isTimeLimitedFormula = FactoryUtils.isTimeLimitedFormula(self.m_args.craftInfo.craftId)
            self.view.stateController:SetState(self.m_isTimeLimitedFormula and "LimitedTime" or "Normal")
            FactoryUtils.setTimeLimitedFormulaTagColor(self.view.timeLimitedColorTag1, self.m_args.craftInfo.craftId)
            FactoryUtils.setTimeLimitedFormulaTagColor(self.view.timeLimitedColorTag2, self.m_args.craftInfo.craftId)
            self.view.selectBgImage.gameObject:SetActiveIfNecessary(self.m_isTimeLimitedFormula)
        else
            self.view.stateController:SetState("Normal")
        end
    end


    FactoryUtils.refreshEnvIcon(args.craftInfo.env, self.view.envTitleController)

    self:_InitController()

    LayoutRebuilder.ForceRebuildLayoutImmediate(self.view.transform)
end

WikiCraftingTreeBuilding.RefreshDefaultNode = HL.Method() << function(self)
    local defaultCraftId = WikiUtils.getItemDefaultCraftId(self.m_args.itemId)
    if string.isEmpty(defaultCraftId) then
        self.m_isDefaultCraft = self.m_args.craftIndex == self.m_args.defaultCraftIndex
    else
        self.m_isDefaultCraft = defaultCraftId == self.m_args.craftInfo.craftId
    end
    local stateName = "Normal"
    if self.m_isPinnedCraft then
        stateName = "Pinned"
    elseif self.m_isDefaultCraft then
        stateName = "Default"
    end
    self.view.setDefaultNode.stateController:SetState(stateName)

    if DeviceInfo.usingController then
        self.view.setDefaultNode.root.gameObject:SetActive(self.m_isDefaultCraft or self.m_isPinnedCraft)
        self.view.setDefaultNode.defaultNode.gameObject:SetActive(false)
        self.view.setDefaultNode.lineImage.gameObject:SetActive(false)
    end
end

WikiCraftingTreeBuilding.SetSelected = HL.Method(HL.Boolean) << function(self, isSelected)
    self.view.selectNode.gameObject:SetActive(isSelected)
    if not self.m_isTimeLimitedFormula then
        self.view.selectBgImage.gameObject:SetActiveIfNecessary(isSelected)
    end
end

WikiCraftingTreeBuilding.GetButton = HL.Method().Return(HL.Userdata) << function(self)
    return self.view.button
end

WikiCraftingTreeBuilding.GetLeftMountPoint = HL.Method(Transform).Return(Vector2) << function(self, relativeTo)
    local pos = relativeTo:InverseTransformPoint(self.view.leftMountPoint.transform.position)
    return Vector2(pos.x, pos.y)
end

WikiCraftingTreeBuilding.GetRightMountPoint = HL.Method(Transform).Return(Vector2) << function(self, relativeTo)
    local pos = relativeTo:InverseTransformPoint(self.view.rightMountPoint.transform.position)
    return Vector2(pos.x, pos.y)
end

WikiCraftingTreeBuilding._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end
    self.view.setDefaultNode.inputBindingGroup.enabled = false
    self.view.setDefaultNode.defaultNode.gameObject:SetActive(false)
    local moreCraftCellParent
    if self.m_args.moreCraftCell then
        self.m_args.moreCraftCell.inputBindingGroup.enabled = self.view.button.isNaviTarget
        moreCraftCellParent = self.m_args.moreCraftCell.transform.parent
    end
    self.view.setDefaultNode.lineImage.gameObject:SetActive(self.view.button.isNaviTarget)
    self.view.button.onIsNaviTargetChanged = function(isNaviTarget)
        local canSetDefault = not self.m_isDefaultCraft and not self.m_isPinnedCraft
        if isNaviTarget then
            self.view.setDefaultNode.inputBindingGroup.enabled = true
            self.view.setDefaultNode.defaultNode.gameObject:SetActive(false)
            if self.m_args.moreCraftCell then
                self.m_args.moreCraftCell.inputBindingGroup.enabled = true
                self.view.setDefaultNode.defaultNode.gameObject:SetActive(canSetDefault)
                if self.m_args.moreCraftCell.btnLess.gameObject.activeSelf then
                    self.view.setDefaultNode.lineImage.gameObject:SetActive(canSetDefault)
                    self.m_args.moreCraftCell.gameObject:SetActive(true)
                    self.m_args.moreCraftCell.transform.parent = self.view.setDefaultNode.lessNode.transform
                    self.m_args.moreCraftCell.transform.localPosition = Vector3.zero
                end
            end
        else
            self.view.setDefaultNode.inputBindingGroup.enabled = false
            self.view.setDefaultNode.defaultNode.gameObject:SetActive(false)
            if self.m_args.moreCraftCell then
                self.m_args.moreCraftCell.inputBindingGroup.enabled = false
                if self.m_args.moreCraftCell.btnLess.gameObject.activeSelf then
                    self.view.setDefaultNode.lineImage.gameObject:SetActive(false)
                    self.m_args.moreCraftCell.gameObject:SetActive(false)
                    self.m_args.moreCraftCell.transform:SetParent(moreCraftCellParent, false)
                end
            end
        end
    end
end

HL.Commit(WikiCraftingTreeBuilding)
return WikiCraftingTreeBuilding

