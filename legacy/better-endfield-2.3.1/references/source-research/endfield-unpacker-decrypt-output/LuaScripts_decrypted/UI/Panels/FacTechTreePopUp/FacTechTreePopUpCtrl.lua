
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacTechTreePopUp
FacTechTreePopUpCtrl = HL.Class('FacTechTreePopUpCtrl', uiCtrl.UICtrl)







FacTechTreePopUpCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.HIDE_TECH_TREE_POP_UP] = 'HideUI',
    [MessageConst.CLOSE_TECH_TREE_POP_UP] = 'CloseUI',
}

FacTechTreePopUpCtrl.ShowPopUp = HL.StaticMethod(HL.Table) << function(args)
    local ctrl = FacTechTreePopUpCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl:_ShowPopUp(args)
end

FacTechTreePopUpCtrl.m_getItemCell = HL.Field(HL.Function)

FacTechTreePopUpCtrl.m_unlockItems = HL.Field(HL.Forward('UIListCache'))

FacTechTreePopUpCtrl.m_args = HL.Field(HL.Table)

FacTechTreePopUpCtrl.m_onStageFinishCb = HL.Field(HL.Function)

FacTechTreePopUpCtrl.m_disableClick = HL.Field(HL.Boolean) << false



FacTechTreePopUpCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.mask.onClick:AddListener(function()
        self:_OnClickUI()
    end)

    self.m_getItemCell = UIUtils.genCachedCellFunction(self.view.rewardNode.rewardsList)
    self.view.rewardNode.rewardsList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateItemCell(self.m_getItemCell(obj), LuaIndex(csIndex))
    end)

    self.m_unlockItems = UIUtils.genCellCache(self.view.unlockNode.item)

    self:_InitController()
end

FacTechTreePopUpCtrl.OnHide = HL.Override() << function(self)
    self.m_args = nil
end

FacTechTreePopUpCtrl._OnClickUI = HL.Method() << function(self)
    if self.m_disableClick then
        return
    end
    if self.m_onStageFinishCb then
        self.m_onStageFinishCb()
    end
end

FacTechTreePopUpCtrl.HideUI = HL.Method(HL.Table) << function(self, args)
    local onHide = args.onHide
    self:PlayAnimationOutWithCallback(function()
        self:Hide()
        if onHide then
            onHide()
        end
    end)
end

FacTechTreePopUpCtrl.CloseUI = HL.Method() << function(self)
    self:Close()
end

FacTechTreePopUpCtrl._ShowPopUp = HL.Method(HL.Table) << function(self, args)
    self.m_args = args
    self.m_onStageFinishCb = args.onStageFinishCb
    local state = args.state
    self.m_disableClick = true
    if state == 1 then
        self.view.animationWrapper:PlayWithTween("factechtreepopup_in", function()
            self.m_disableClick = false
        end)
    else
        self.view.animationWrapper:SampleClipAtPercent("factechtreepopup_in", 1)
        self.view.animationWrapper:PlayWithTween("factechtreepopup_change", function()
            self.m_disableClick = false
        end)
    end

    local unlockItems = args.unlockItems
    local buildingInfo = args.buildingInfo
    local rewardsItems = args.rewardsItems
    local nodeData = Tables.facSTTNodeTable:GetValue(args.techId)
    self.view.name.text = nodeData.name
    local sprite = self:LoadSprite(UIConst.UI_SPRITE_FAC_TECH_ICON, nodeData.icon.."_big")
    self.view.icon.sprite = sprite
    self.view.iconS.sprite = sprite


    if unlockItems and #unlockItems > 0 then
        self:_ShowUnlock()
    else
        self.view.unlockNode.gameObject:SetActiveIfNecessary(false)
    end


    if buildingInfo and buildingInfo.buildingId and not string.isEmpty(buildingInfo.buildingId) then
        self:_ShowLevelUp()
    else
        self.view.upgradeNode.gameObject:SetActiveIfNecessary(false)
    end


    if rewardsItems and #rewardsItems > 0 then
        self:_ShowRewards()
    else
        self.view.rewardNode.gameObject:SetActiveIfNecessary(false)
    end
end

FacTechTreePopUpCtrl._ShowUnlock = HL.Method() << function(self)
    local node = self.view.unlockNode
    local unlockItems = self.m_args.unlockItems
    node.gameObject:SetActiveIfNecessary(true)
    self.m_unlockItems:Refresh(#unlockItems, function(cell, index)
        cell:InitItem({id = unlockItems[index]}, true)

        if DeviceInfo.usingController then
            cell:SetExtraInfo({
                                  tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
                                  tipsPosTransform = self.view.unlockNode.unlockContent,
                                  isSideTips = true,
                              })
        end
    end)
    local techId = self.m_args.techId
    local techData = Tables.facSTTNodeTable:GetValue(techId)
    self.view.desc:SetAndResolveTextStyle(techData.unlockDesc)
end

FacTechTreePopUpCtrl._ShowLevelUp = HL.Method() << function(self)
    local node = self.view.upgradeNode
    local buildingInfo = self.m_args.buildingInfo

    node.gameObject:SetActiveIfNecessary(true)
    local buildingId = buildingInfo.buildingId
    local level = buildingInfo.level

    local buildingData = Tables.factoryBuildingTable:GetValue(buildingId)
    node.buildingName.text = string.format(Language.LUA_FAC_TECHTREE_POPUP_BUILDING_NAME, buildingData.name)
    node.level.gameObject:SetActiveIfNecessary(true)

    node.craft.gameObject:SetActiveIfNecessary(false)


    node.level.lText.text = buildingInfo.level - 1
    node.level.rText.text = buildingInfo.level


    node.char.gameObject:SetActiveIfNecessary(false)


    local showContract = false
    local curCount = 0
    local lastCount = 0
    node.contract.gameObject:SetActiveIfNecessary(showContract)
    if showContract then
        node.contract.lText.text = lastCount
        node.contract.rText.text = curCount
    end


    local showCraft = false
    local curCraftCount = 0
    local lastCraftCount = 0
    if buildingData.type == GEnums.FacBuildingType.Processor then
        for _,craftData in pairs(Tables.factoryProcessorCraftTable) do
            if craftData.usableLevel <= level and FactoryUtils.isSpMachineFormulaUnlocked(craftData.id) then
                curCraftCount = curCraftCount + 1
            end
            if craftData.usableLevel <= level - 1 and FactoryUtils.isSpMachineFormulaUnlocked(craftData.id) then
                lastCraftCount = lastCraftCount + 1
            end
        end
        if curCraftCount ~= lastCraftCount then
            showCraft = true
        end
    end
    node.craft.gameObject:SetActiveIfNecessary(showCraft)
    if showCraft then
        node.craft.lText.text = lastCraftCount
        node.craft.rText.text = curCraftCount
    end


    local techId = self.m_args.techId
    local techData = Tables.facSTTNodeTable:GetValue(techId)
    self.view.desc:SetAndResolveTextStyle(techData.unlockDesc)
end

FacTechTreePopUpCtrl._ShowRewards = HL.Method() << function(self)
    local node = self.view.rewardNode
    local rewardsItems = self.m_args.rewardsItems
    node.gameObject:SetActiveIfNecessary(true)
    node.rewardsList:UpdateCount(#rewardsItems)
    self.view.desc.text = Language.LUA_FAC_TECHTREE_REWARDS_TIPS
end

FacTechTreePopUpCtrl._OnUpdateItemCell = HL.Method(HL.Userdata, HL.Number) << function(self, cell, index)
    local rewardsItems = self.m_args.rewardsItems
    local info = rewardsItems[index]
    cell:InitItem(info, true)

    if DeviceInfo.usingController then
        cell:SetExtraInfo({
                              tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftMid,
                              tipsPosTransform = self.view.rewardNode.rewardsListRectTransform,
                              isSideTips = true,
                          })
    end
end


FacTechTreePopUpCtrl._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    self.view.unlockNode.selectableNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)

    self.view.rewardNode.selectableNaviGroup.onIsFocusedChange:AddListener(function(isFocused)
        if not isFocused then
            Notify(MessageConst.HIDE_ITEM_TIPS)
        end
    end)
end


HL.Commit(FacTechTreePopUpCtrl)
