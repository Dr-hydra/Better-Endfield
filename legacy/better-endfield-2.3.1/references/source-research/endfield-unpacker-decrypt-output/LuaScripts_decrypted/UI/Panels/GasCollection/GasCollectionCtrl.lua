
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GasCollection

GasCollectionCtrl = HL.Class('GasCollectionCtrl', uiCtrl.UICtrl)

local ItemAnimType = {
    None = 1,
    Init = 2,
    Update = 3,
}

GasCollectionCtrl.m_bagNodeItemInfos = HL.Field(HL.Table)





GasCollectionCtrl.s_messages = HL.StaticField(HL.Table) << {

}

GasCollectionCtrl.m_gasId = HL.Field(HL.String) << ""

GasCollectionCtrl.m_gasNodeId = HL.Field(HL.Number) << 0

GasCollectionCtrl.m_gasCapacity = HL.Field(HL.Number) << 0

GasCollectionCtrl.m_selectedItemMap = HL.Field(HL.Table)

GasCollectionCtrl.m_selectedItemList = HL.Field(HL.Table)


GasCollectionCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_gasNodeId, self.m_gasId = unpack(arg)
    self.m_selectedItemMap = {}
    self.m_selectedItemList = {}
    self:_InitBtn()
    self:_InitBag()
    self:_InitGas(self.m_gasId)
    self:_RefreshContainerInfo(ItemAnimType.Init)
    self:_InitController()
end

GasCollectionCtrl.OnClose = HL.Override() << function(self)
    self.view.containerBagNode:OnPanelClose()
end

GasCollectionCtrl.ShowGasCollection = HL.StaticMethod(HL.Table) << function(info)
    PhaseManager:OpenPhase(PhaseId.GasCollection, {info.gasCoreId, info.gasItemId})
end

GasCollectionCtrl._InitBtn = HL.Method() << function(self)
    self.view.closeBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.GasCollection)
    end)
    self.view.confirmBtn.onClick:AddListener(function()
        self:_OnConfirmAction()
    end)
end

GasCollectionCtrl._RefreshBagData = HL.Method() << function(self)
    self:_PrepareBagData()
    self.view.containerBagNode:ResetData(self.m_bagNodeItemInfos)
end

GasCollectionCtrl._InitBag = HL.Method() << function(self)
    self:_PrepareBagData()

    local tryChangeBagItemNumFunc = function(index, newNum)
        local bottleCount = 0
        local capacity = 0
        for k, v in ipairs(self.m_bagNodeItemInfos) do
            if k ~= index then
                bottleCount = bottleCount + v.selectedCount
                capacity = capacity + v.selectedCount * v.liquidCapacity
            end
        end

        if bottleCount + newNum > Tables.factoryConst.maxFillingBottleCount then
            Notify(MessageConst.SHOW_TOAST, Language.LUA_LIQUID_FILL_TOO_MUCH_BOTTLE)
            return true, math.max(0, Tables.factoryConst.maxFillingBottleCount - bottleCount)
        end
        return true, newNum
    end

    local onBagItemNumChangedFunc = function(index, newNum)
        self.m_bagNodeItemInfos[index].selectedCount = newNum
    end

    local onBagNodeConfirmFunc = function()
        local prevCount = #self.m_selectedItemList
        local prevIsEmpty = prevCount == 0
        self.m_selectedItemList = {}
        self.m_selectedItemMap = {}
        self.m_gasCapacity = 0
        for _, info in ipairs(self.m_bagNodeItemInfos) do
            if info.selectedCount > 0 then
                table.insert(self.m_selectedItemList, { id = info.id, count = info.selectedCount })
                self.m_selectedItemMap[info.id] = info.selectedCount
                self.m_gasCapacity = self.m_gasCapacity + info.liquidCapacity * info.selectedCount
            end
        end

        local currCount = #self.m_selectedItemList
        local currIsEmpty = currCount == 0
        local animType = prevIsEmpty == currIsEmpty and ItemAnimType.None or ItemAnimType.Update
        self:_RefreshContainerInfo(animType)
    end

    self.view.containerBagNode:Init(PANEL_ID, self.m_bagNodeItemInfos, self.view.controllerHintPlaceholder,
        tryChangeBagItemNumFunc, onBagItemNumChangedFunc, onBagNodeConfirmFunc)
end

GasCollectionCtrl._InitGas = HL.Method(HL.String) << function(self, gasItemId)
    local itemData = Tables.itemTable[gasItemId]
    local node = self.view.fillContent.poolNode
    node.nameTxt = itemData.name
    node.item:InitItem({id = gasItemId}, true)
    node.storageTxt.text = Language.LUA_LIQUID_POOL_INFINITE_COUNT

    self.view.gasNameTxt.text = itemData.name
    UIUtils.setItemRarityImage(self.view.gasRarity, itemData.rarity)
end

GasCollectionCtrl._RefreshContainerInfo = HL.Method(HL.Number) << function(self, animType)
    local node = self.view.fillContent.itemNode
    node.content.gameObject:SetActiveIfNecessary(true)
    node.forbidContent.gameObject:SetActiveIfNecessary(false)

    if not node.m_getCell then
        node.m_getCell = UIUtils.genCachedCellFunction(node.scrollList)
        node.scrollList.onUpdateCell:AddListener(function(obj, csIndex)
            local info = self.m_selectedItemList[LuaIndex(csIndex)]
            node.m_getCell(obj):InitItem(info, true)
        end)
        node.content.onClick:AddListener(function()
            self:_RefreshBagData()
            self.view.containerBagNode:SetActive(true)
        end)
        node.emptyAddBtn.onClick:AddListener(function()
            self:_RefreshBagData()
            self.view.containerBagNode:SetActive(true)
        end)
    end
    local count = #self.m_selectedItemList
    local isEmpty = count == 0
    self.view.emptyHint.gameObject:SetActive(isEmpty)
    self.view.confirmBtn.gameObject:SetActive(not isEmpty)
    node.deco1.gameObject:SetActive(isEmpty)
    node.deco2.gameObject:SetActive(isEmpty)
    node.emptyAddBtn.gameObject:SetActive(isEmpty)
    node.scrollList.gameObject:SetActive(not isEmpty)
    node.scrollList:UpdateCount(count)

    self.view.gasNumTxt.text = self.m_gasCapacity
    node.txtName.text = string.format(Language.LUA_FAC_GAS_CAPACITY_TEXT, self.m_gasCapacity, Tables.factoryConst.maxFillingBottleCount)

    local animName = isEmpty and "gascollection_arrowempty" or "gascollection_arrowloop"
    if animType == ItemAnimType.Init then
        self.view.fillContent.arrowNode:SampleClipAtPercent(animName, 1)
    elseif animType == ItemAnimType.Update then
        self.view.fillContent.arrowNode:Play(animName)
    end
end

GasCollectionCtrl._CleanContainerInfo = HL.Method() << function(self)
    local node = self.view.fillContent.itemNode
    self.m_gasCapacity = 0
    self.m_selectedItemList = {}
    self.m_selectedItemMap = {}
    self.view.emptyHint.gameObject:SetActive(true)
    self.view.confirmBtn.gameObject:SetActive(false)
    node.deco1.gameObject:SetActive(true)
    node.deco2.gameObject:SetActive(true)
    node.emptyAddBtn.gameObject:SetActive(true)self:_RefreshBagData()
    node.scrollList.gameObject:SetActive(false)
    node.scrollList:UpdateCount(0)

    self.view.gasNumTxt.text = self.m_gasCapacity
    node.txtName.text = string.format(Language.LUA_FAC_GAS_CAPACITY_TEXT, self.m_gasCapacity, Tables.factoryConst.maxFillingBottleCount)
end

GasCollectionCtrl._PrepareBagData = HL.Method() << function(self)
    self.m_bagNodeItemInfos = {}

    local bottleTable = Tables.emptyGasJarTable
    for id, bottleData in pairs(bottleTable) do
        local needShow
        needShow = lume.find(bottleData.gasItems, self.m_gasId)
        if needShow then
            local count = Utils.getItemCount(id)
            if count > 0 then
                local itemData = Tables.itemTable[id]
                local info = {
                    id = id,
                    count = count,
                    sortId1 = itemData.sortId1,
                    sortId2 = itemData.sortId2,
                    rarity = itemData.rarity,
                    liquidCapacity = bottleData.gasCapacity,
                    selectedCount = self.m_selectedItemMap[id] and self.m_selectedItemMap[id] or 0
                }
                info.isValid = true
                info.validSortId = info.isValid and 1 or 0
                info.countSortId = info.count > 0 and 1 or 0
                table.insert(self.m_bagNodeItemInfos, info)
            end
        end
    end

    table.sort(self.m_bagNodeItemInfos, Utils.genSortFunction({ "validSortId", "countSortId", "rarity", "sortId1", "sortId2", "id" }, false))
end

GasCollectionCtrl._OnConfirmAction = HL.Method() << function(self)
    self:_SendActionMsg()
end

GasCollectionCtrl._SendActionMsg = HL.Method() << function(self)
    local idList = {}
    local countList = {}
    for k, v in ipairs(self.m_selectedItemList) do
        idList[k] = v.id
        countList[k] = v.count
    end
    AudioManager.PostEvent("Au_UI_Event_GasUp")
    GameInstance.player.remoteFactory.core:Message_TakeOutGasFromGasMine(Utils.getCurrentChapterId(), self.m_gasNodeId, idList, countList, function(op, opRet)
        self:_OnActionReturn(op, opRet)
    end)
end

GasCollectionCtrl._OnActionReturn = HL.Method(CS.Proto.CS_FACTORY_OP, CS.Proto.SC_FACTORY_OP_RET) << function(self, op, opRet)
    if opRet.RetCode ~= CS.Proto.FACTORY_OP_RET_CODE.Ok then
        return
    end

    local retType = opRet.TakeOutFluidFromGasMine.Ret
    if retType == CS.Proto.RET_FLUID_WITH_GAS_BODY.None then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_GAS_ACTION_FAIL_FILL_NONE)
        self:_CleanContainerInfo()
        return
    end

    local animationWrapper = self.view.fillContent.animationWrapper
    local anim = "gascollection_filling"
    Notify(MessageConst.SHOW_BLOCK_INPUT_PANEL, animationWrapper:GetClipLength(anim))
    animationWrapper:PlayWithTween(anim, function()
        self:_StartTimer(0, function()
            animationWrapper:PlayWithTween("gascollection_normal")
            self:_RefreshContainerInfo(ItemAnimType.Update)
        end)
        self:_ShowReward(opRet, retType)
    end)
end

GasCollectionCtrl._ShowReward = HL.Method(CS.Proto.SC_FACTORY_OP_RET, CS.Proto.RET_FLUID_WITH_GAS_BODY) << function(self, opRet, retType)
    if retType == CS.Proto.RET_FLUID_WITH_GAS_BODY.PartialByBag then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_GAS_ACTION_FILL_PARTIAL_BY_BAG)
    end

    local protoItems = opRet.TakeOutFluidFromGasMine.GainJarGasItems
    local itemList = {}
    local count = protoItems.Count
    local amount = 0
    for k = 0, count - 1 do
        local bundle = protoItems[k]
        table.insert(itemList, { id = bundle.Id, count = bundle.Count })
        amount = amount + bundle.Count
    end
    local itemData = Tables.itemTable[self.m_gasId]
    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        title = Language.LUA_LIQUID_FILL_SUCCESS,
        subTitle = string.format(Language.LUA_LIQUID_FILL_RESULT, amount, itemData.name),
        icon = "icon_fluid_filling",
        items = itemList,
    })
    self:_CleanContainerInfo()
end

GasCollectionCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

HL.Commit(GasCollectionCtrl)
