local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.LostAndFound
LostAndFoundCtrl = HL.Class('LostAndFoundCtrl', uiCtrl.UICtrl)







LostAndFoundCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_GET_LOST_AND_FOUND] = 'OnGetLostAndFound',
    [MessageConst.ON_ADD_LOST_AND_FOUND] = 'OnAddLostAndFound',
    [MessageConst.ON_LOST_AND_FOUND_NOTHING_GET] = 'OnLostAndFoundNothingGet'
}

LostAndFoundCtrl.m_getCell = HL.Field(HL.Function)

LostAndFoundCtrl.m_allItemInfoList = HL.Field(HL.Table)


LostAndFoundCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.maskBtn.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.LostAndFound)
    end)
    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PhaseId.LostAndFound)
    end)
    self.view.btnGetAll.onClick:AddListener(function()
        self:_OnClickGetAll()
    end)

    self.m_getCell = UIUtils.genCachedCellFunction(self.view.scrollList)
    self.view.scrollList.onUpdateCell:AddListener(function(object, csIndex)
        self:_OnUpdateCell(object, csIndex)
    end)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

LostAndFoundCtrl.OnShow = HL.Override() << function(self)
    self:_Refresh()
end

LostAndFoundCtrl._OnUpdateCell = HL.Method(GameObject, HL.Number) << function(self, object, csIndex)
    local cell = self.m_getCell(object)
    local bundle = self.m_allItemInfoList[LuaIndex(csIndex)]
    cell:InitItem(bundle, true)
    if DeviceInfo.usingController then
        cell:SetExtraInfo({
            tipsPosType = UIConst.UI_TIPS_POS_TYPE.LeftTop,
            isSideTips = true,
        })
    end
end

LostAndFoundCtrl._OnClickGetAll = HL.Method() << function(self)
    GameInstance.player.inventory:TakeLostAndFound(true)
end

LostAndFoundCtrl._OnClickGetItem = HL.Method(HL.Table) << function(self, bundle)
    local itemsDic = bundle.isInst and {} or { [bundle.id] = bundle.count }
    local instIdsList = bundle.isInst and { bundle.instId } or {}

    GameInstance.player.inventorySystem:TakeLostAndFound(false, itemsDic, instIdsList)
end

LostAndFoundCtrl._ProcessItem = HL.Method(HL.String, HL.Number, HL.Opt(HL.Number)).Return(HL.Table)
        << function(self, id, count, instId)
    local data = Tables.itemTable:GetValue(id)
    local info = {
        id = id,
        instId = instId or 0,
        isInst = instId ~= nil,
        count = count,
        maxStackCount = data.maxBackpackStackCount,
        data = data,
        showingType = data.showingType,
        rarity = data.rarity,
        sortId1 = data.sortId1,
        sortId2 = data.sortId2,
    }
    return info
end

LostAndFoundCtrl._Refresh = HL.Method() << function(self)
    local lostAndFound = GameInstance.player.inventory.lostAndFound
    local gridCount = lostAndFound:GetUsedGridCount()
    local empty = lostAndFound:IsEmpty()

    self.view.scrollList.gameObject:SetActive(not empty)
    self.view.btnGetAll.gameObject:SetActive(not empty)
    self.view.noGridLabel.gameObject:SetActive(empty)
    if not empty then

        local allItemInfoList = {}

        local normalItems = lostAndFound.normalItems
        local instItems = lostAndFound.instItems

        for id, bundle in pairs(normalItems) do
            table.insert(allItemInfoList, self:_ProcessItem(id, bundle.count))
        end
        for instId, bundle in pairs(instItems) do
            table.insert(allItemInfoList, self:_ProcessItem(bundle.id, bundle.count, instId))
        end

        table.sort(allItemInfoList, Utils.genSortFunction({ "sortId1", "sortId2" }))
        self.m_allItemInfoList = allItemInfoList
        self.view.scrollList:UpdateCount(gridCount)

        if DeviceInfo.usingController then
            local maxWidth = self.view.scrollList.transform.rect.width
            local spaceWidth = self.view.scrollList.space.x
            local itemWidth = self.view.item.transform.rect.width
            local posx = math.min((itemWidth + spaceWidth) * gridCount, maxWidth) / 2
            self.view.showTipsKeyHint.anchoredPosition = Vector2(-posx, 0)
        end
    end

end

LostAndFoundCtrl.OnGetLostAndFound = HL.Method(HL.Table) << function(self, args)

    local removedNormalItems, removedInstItems = unpack(args)

    local getItems = {}
    for k, v in pairs(removedNormalItems) do
        table.insert(getItems, self:_ProcessItem(k, v))
    end

    for k, v in pairs(removedInstItems) do
        table.insert(getItems, self:_ProcessItem(v, 1, k))
    end
    table.sort(getItems, Utils.genSortFunction({ "sortId1", "sortId2" }))

    Notify(MessageConst.SHOW_SYSTEM_REWARDS, {
        title = Language.ui_common_have_item,
        icon = "icon_mail_obtain",
        items = getItems,
    })

    local lostAndFound = GameInstance.player.inventory.lostAndFound
    local empty = lostAndFound:IsEmpty()
    if empty then
        PhaseManager:PopPhase(PhaseId.LostAndFound)
    else
        self:_Refresh()
    end
end

LostAndFoundCtrl.OnAddLostAndFound = HL.Method() << function(self)
    self:_Refresh()
end

LostAndFoundCtrl.OnLostAndFoundNothingGet = HL.Method() << function(self)
    self:_ProcessOverflowToast()
end

LostAndFoundCtrl._ProcessOverflowToast = HL.Method() << function(self)
    local lostAndFound = GameInstance.player.inventory.lostAndFound
    local valueDepotOverflow = false
    local bagOverflow = false

    for k, v in pairs(lostAndFound.normalItems) do
        if GameInstance.player.inventory:IsPlaceInBag(k) then
            bagOverflow = true
        else
            valueDepotOverflow = true
        end

        if bagOverflow and valueDepotOverflow then
            break
        end
    end

    if not bagOverflow or not valueDepotOverflow then
        for k, v in pairs(lostAndFound.instItems) do
            if GameInstance.player.inventory:IsPlaceInBag(v.id) then
                bagOverflow = true
            else
                valueDepotOverflow = true
            end

            if bagOverflow and valueDepotOverflow then
                break
            end
        end
    end

    if bagOverflow then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_LOST_AND_FOUND_BAG_OVERFLOW)
    end

    if valueDepotOverflow then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_LOST_AND_FOUND_VALUABLE_DEPOT_OVERFLOW)
    end
end











HL.Commit(LostAndFoundCtrl)
