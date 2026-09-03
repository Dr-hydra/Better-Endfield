
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacFertilization
local PHASE_ID = PhaseId.FacFertilization
local FertilizeType = GEnums.FertilizeType



FERTILIZATION_SORT_KEYS = { "isNotEnough", "reverseRarity", "sortId1", "sortId2", "id" }

local Fertilize_Cell_Select_In = "facfertilization_cell_select_in"
local Fertilize_Cell_Select_Out = "facfertilization_cell_select_out"





FacFertilizationCtrl = HL.Class('FacFertilizationCtrl', uiCtrl.UICtrl)





FacFertilizationCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacFertilizationCtrl.m_intFacSoilComponent = HL.Field(CS.Beyond.Gameplay.Core.IntFacSoilComponent)


FacFertilizationCtrl.m_inventorySystem = HL.Field(CS.Beyond.Gameplay.InventorySystem)

FacFertilizationCtrl.m_typeStateMap = HL.Field(HL.Table)

FacFertilizationCtrl.m_itemCell = HL.Field(HL.Any)

FacFertilizationCtrl.m_itemList = HL.Field(HL.Any)

FacFertilizationCtrl.m_chosenIdx = HL.Field(HL.Any)


FacFertilizationCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    print("args ", arg)

    self.m_inventorySystem = GameInstance.player.inventory
    local nodeId = arg
    local soilShow = GameInstance.player.facSoilSystem:GetSoilShow(nodeId)
    self.m_intFacSoilComponent = soilShow.facSoilComponent
    self.m_chosenIdx = -1
    self.m_typeStateMap = {
        [FertilizeType.DecreaseTime] = "AccelerateGrowth",
        [FertilizeType.IncreaseProduction] = "Increase",
    }


    self.view.closeBtn.onClick:AddListener(function()
        self:_Exit()
    end)
    self.view.maskBtn.onClick:AddListener(function()
        self:_Exit()
    end)
    self.view.useBtn.onClick:AddListener(function()

        self:_DoFertilize()
    end)


    local itemCount = self:_LoadFertilization()
    self.m_itemCell = UIUtils.genCachedCellFunction(self.view.itemList)
    self.view.itemList.onUpdateCell:AddListener(function(obj, index)
        self:_UpdateItem(self.m_itemCell(obj), LuaIndex(index))
    end)
    self.view.itemList:UpdateCount(itemCount)

    self:_ChooseItem(LuaIndex(0))





    self.view.main:SetState(itemCount <= 0 and "Empty" or "Content")
    if DeviceInfo.usingController then
        self:_InitController()
    end
end

FacFertilizationCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputBindingGroupMonoTarget.groupId })
end


FacFertilizationCtrl._LoadFertilization = HL.Method().Return(HL.Any) << function(self)

    local fertilizeTable = Tables.fertilizeDataTable

    local itemCount = 0
    self.m_itemList = {}
    for id, _ in pairs(fertilizeTable) do

        if self.m_inventorySystem:IsItemFound(id) then
            local _, count, _ = Utils.getItemCount(id)
            local itemCfg = Tables.itemTable[id]
            local _, fertilizeCfg = Tables.fertilizeDataTable:TryGetValue(id)
            local iconId
            if fertilizeCfg ~= nil then
                iconId = fertilizeCfg.detailIconId
            end
            local info = {
                id = id,
                count = count,
                rarity = itemCfg.rarity,
                reverseRarity = -itemCfg.rarity,
                type = itemCfg.type,
                name = itemCfg.name,
                iconId = itemCfg.iconId,
                detailText = itemCfg.desc,
                richText = itemCfg.decoDesc,
                detailIconId = iconId,
                typeState = self.m_typeStateMap[fertilizeCfg.fertilizeType],
                isNotEnough = count <= 0 and 1 or 0,

                sortId1 = itemCfg.sortId1,
                sortId2 = itemCfg.sortId2,
            }
            table.insert(self.m_itemList, info)
            itemCount = itemCount + 1
        end
    end


    table.sort(self.m_itemList, Utils.genSortFunction(FERTILIZATION_SORT_KEYS, true))
    return itemCount
end

FacFertilizationCtrl._UpdateItem = HL.Method(HL.Any, HL.Any) << function(self, item, index)
    local itemInfo = self.m_itemList[index]

    item.button.onClick:AddListener(function()
        self:_ChooseItem(index)
    end)

    item.nodeState:SetState(itemInfo.isNotEnough == 1 and "NotEnough" or "Enough")
    item.nodeState:SetState("NotSelected")
    item.nameTxt.text = itemInfo.name
    item.numTxt.text = itemInfo.count
    item.icon:LoadSprite(UIConst.UI_SPRITE_ITEM, itemInfo.iconId)
    UIUtils.setItemRarityImage(item.colorLine, itemInfo.rarity)
    if DeviceInfo.usingController and index == 1 then
        self:SetNaviTarget(item.button)
    end
end

FacFertilizationCtrl._ChooseItem = HL.Method(HL.Any) << function(self, index)
    if self.m_chosenIdx == index then
        return
    end


    if self.m_chosenIdx ~= -1 then
        local lastItem = self.m_itemCell(self.m_chosenIdx)
        local lastItemInfo = self.m_itemList[self.m_chosenIdx]
        if lastItem then
            lastItem.nodeState:SetState((lastItemInfo.isNotEnough == 1) and "NotEnough" or "Enough")
            lastItem.nodeStateAnim:Play(Fertilize_Cell_Select_Out, function()
                lastItem.nodeState:SetState("NotSelected")
            end)
        end
    end

    local item = self.m_itemCell(index)
    local itemInfo = self.m_itemList[index]
    if item then
        item.nodeState:SetState((itemInfo.isNotEnough == 1) and "NotEnough" or "Enough")
        AudioAdapter.PostEvent("Au_UI_Toggle_Common_On")
        item.nodeStateAnim:Play(Fertilize_Cell_Select_In, function()
            item.nodeState:SetState("Selected")
        end)
    end

    self.m_chosenIdx = index


    self:_UpdateContent()
end

FacFertilizationCtrl._UpdateContent = HL.Method() << function(self)
    if self.m_chosenIdx == -1 then
        return
    end

    local info = self.m_itemList[self.m_chosenIdx]
    if info == nil then
        return
    end


    local canInteract = info.isNotEnough == 0
    self.view.useBtn.interactable = canInteract
    if canInteract then
        self.view.useBtnTxt.text = Language.LUA_FAC_FERTILIZATION_INTERACT
        self.view.useBtnState:SetState("NormalState")
    else
        self.view.useBtnTxt.text = Language.LUA_FAC_FERTILIZATION_CANNOT_INTERACT
        self.view.useBtnState:SetState("ActiveState")
    end


    self.view.nameTxt.text = info.name
    self.view.typeState:SetState(info.typeState)
    if info.detailIconId ~= nil then
        self.view.icon:LoadSprite(UIConst.UI_SPRITE_CROP, info.detailIconId)
    end


    self.view.detailTxt.text = info.detailText
    self.view.richTxt.text = info.richText
end

FacFertilizationCtrl._DoFertilize = HL.Method() << function(self)
    if self.m_chosenIdx == -1 then
        return
    end

    local info = self.m_itemList[self.m_chosenIdx]
    if info == nil then
        return
    end


    Notify(MessageConst.On_FERTILIZE_PANEL_DO_OPERATION)
    self.m_intFacSoilComponent:DoFertilize(info.id)



end

FacFertilizationCtrl._Exit = HL.Method() << function(self)
    self.view.animationWrapper:PlayOutAnimation(function()
        UIManager:Close(PANEL_ID)
    end)
end











FacFertilizationCtrl._OnOpenFertilization = HL.StaticMethod(HL.Any) << function(nodeId)
    local unpackNodeId = unpack(nodeId)


    UIManager:Open(PANEL_ID, unpackNodeId)
end

HL.Commit(FacFertilizationCtrl)
