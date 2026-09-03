
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SubmitItemInteractive
SubmitItemInteractiveCtrl = HL.Class('SubmitItemInteractiveCtrl', uiCtrl.UICtrl)







SubmitItemInteractiveCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SUBMIT_ITEM] = 'OnSubmitItem',
}

SubmitItemInteractiveCtrl.m_submitItemsNormal = HL.Field(HL.Table)

SubmitItemInteractiveCtrl.m_submitItemsSelect = HL.Field(HL.Table)

SubmitItemInteractiveCtrl.m_selectItemBundle = HL.Field(HL.Table)

SubmitItemInteractiveCtrl.m_info = HL.Field(HL.Table)

SubmitItemInteractiveCtrl.m_normalItemListCache = HL.Field(HL.Forward("UIListCache"))

SubmitItemInteractiveCtrl.m_selectItemListCache = HL.Field(HL.Forward("UIListCache"))


SubmitItemInteractiveCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnCancel.onClick:AddListener(function()
        self:Close()
    end)
    self.view.btnSubmit.onClick:AddListener(function()
        self:_OnClickSubmit()
    end)

    local submitId = arg.submitId
    local questId = arg.questId
    local objId = arg.objId
    if not questId then
        questId = ""
    end
    if not objId then
        objId = 0
    end
    self.m_info = {submitId = submitId, questId = questId, objId = objId}

    local data = Tables.submitItem[submitId]
    self.view.title.text = data.name
    self.view.icon:LoadSprite(data.icon)

    self.m_submitItemsNormal = {}
    self.m_submitItemsSelect = {}
    self.m_selectItemBundle = {
        id = "",
        count = 0,
        instId = 0
    }
    for _, v in pairs(data.paramData) do
        if v.type == GEnums.SubmitTermType.Common then
            local itemId = v.paramList[0].valueStringList[0]
            local needCount = v.paramList[1].valueIntList[0]
            local itemBundle = Tables.itemTable[itemId]
            if itemBundle ~= nil and Utils.isItemInstType(itemId) then
                if self.m_selectItemBundle.id == itemId then
                    self.m_selectItemBundle.count = self.m_selectItemBundle.count + needCount
                else
                    if self.m_selectItemBundle.id == "" then
                        self.m_selectItemBundle.id = itemId
                        self.m_selectItemBundle.count = needCount
                    end
                end
            else
                table.insert(self.m_submitItemsNormal, {
                    id = itemId,
                    count = v.paramList[1].valueIntList[0],
                })
            end
        end

    end

    self.m_normalItemListCache = UIUtils.genCellCache(self.view.itemCellNormal)
    self.m_normalItemListCache:Refresh(#self.m_submitItemsNormal, function(cell, index)
        cell.item:InitItem(self.m_submitItemsNormal[index], true)
    end)
    self.m_selectItemListCache = UIUtils.genCellCache(self.view.itemCellSelect)
    self.m_selectItemListCache:Refresh(self.m_selectItemBundle.count, function(cell, _)
        cell.item:InitItem({ })
        cell.add.onClick:RemoveAllListeners()
        cell.add.onClick:AddListener(function()
            self:_OnClickSelectItem()
        end)
    end)
    self.view.bar.gameObject:SetActiveIfNecessary(#self.m_submitItemsNormal ~= 0 and self.m_selectItemBundle.count ~= 0)

    self:_UpdateSelectText()
    self:_UpdateCount()

    self:_StartCoroutine(function()
        while true do
            coroutine.wait(UIConst.COMMON_UI_TIME_UPDATE_INTERVAL)
            self:_UpdateCount()
        end
    end)
end

SubmitItemInteractiveCtrl._OnClickSubmit = HL.Method() << function(self)
    if self.m_selectItemBundle.count ~= 0 then
        local selectInstIds = {}
        for _, v in pairs(self.m_submitItemsSelect) do
            table.insert(selectInstIds, v.instId)
        end
        GameInstance.player.inventory:SubmitItem(Utils.getCurrentScope(), Utils.getCurrentChapterId(), self.m_info.submitId, self.m_info.questId, self.m_info.objId, selectInstIds)
    else
        GameInstance.player.inventory:SubmitItem(Utils.getCurrentScope(), Utils.getCurrentChapterId(), self.m_info.submitId, self.m_info.questId, self.m_info.objId)
    end
end

SubmitItemInteractiveCtrl._OnClickSelectItem = HL.Method() << function(self)
    self.view.itemSelectList.gameObject:SetActiveIfNecessary(true)
    self.view.itemSelectList:InitItemSelectList({self.m_selectItemBundle.id}, self.m_submitItemsSelect, function(itemBundle, cell)
        self:_OnSelectItem(itemBundle, cell)
    end)
end

SubmitItemInteractiveCtrl._OnSelectItem = HL.Method(HL.Table, HL.Any) << function(self, itemBundle, cell)

    for i = #self.m_submitItemsSelect, 1, -1 do
        local bundle = self.m_submitItemsSelect[i]
        if bundle.instId == itemBundle.instId then
            table.remove(self.m_submitItemsSelect, i)
            cell.view.toggle.gameObject:SetActiveIfNecessary(false)
            self:_UpdateCount()
            return
        end
    end

    if #self.m_submitItemsSelect >= self.m_selectItemBundle.count then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_SUBMIT_ITEM_SELECT_MAX_COUNT_TIP)
        return
    end

    cell.view.toggle.gameObject:SetActiveIfNecessary(true)

    table.insert(self.m_submitItemsSelect, {
        id = itemBundle.id,
        count = 1,
        instId = itemBundle.instId
    })
    self:_UpdateCount()
end

SubmitItemInteractiveCtrl._UpdateSelectText = HL.Method() << function(self)
    local hasSelectItem = #self.m_submitItemsSelect > 0

    self.view.textSelect.gameObject:SetActiveIfNecessary(hasSelectItem)
    if (hasSelectItem) then
        local submitItemData = Tables.itemTable:GetValue(self.m_submitItemsSelect[1].id)
        local rarityColorStr = Tables.rarityColorTable[submitItemData.rarity].color
        self.view.textSelect:SetAndResolveTextStyle(
            string.format(
                Language.LUA_SUBMIT_ITEM_SELECT_TIP,
                #self.m_submitItemsSelect,
                submitItemData.name,
                rarityColorStr))
    end
end

SubmitItemInteractiveCtrl._UpdateCount = HL.Method() << function(self)
    local enoughNormal = true
    self.m_normalItemListCache:Update(function(cell, index)
        local bundle = self.m_submitItemsNormal[index]
        local count = Utils.getItemCount(bundle.id)
        local isLack = count < bundle.count
        cell.item:UpdateCountSimple(bundle.count, isLack)
        UIUtils.setItemStorageCountText(cell.storageNode, bundle.id, bundle.count)
        if isLack then
            enoughNormal = false
        end
    end)


    local enoughSelect = true
    local enoughSelectInBag = (self.m_selectItemBundle.id == "") or (GameInstance.player.inventory:GetItemCount(Utils.getCurrentScope(), Utils.getCurrentChapterId(), self.m_selectItemBundle.id) >= self.m_selectItemBundle.count)
    self.m_selectItemListCache:Update(function(cell, index)
        local bundle = self.m_submitItemsSelect[index]
        if bundle == nil or bundle.instId == 0 then
            enoughSelect = false
            cell.item:InitItem({ })
            cell.add.gameObject:SetActiveIfNecessary(true)
        else
            cell.item:InitItem(bundle, function()
                self:_OnClickSelectItem()
            end, "", true)
            cell.add.gameObject:SetActiveIfNecessary(false)
        end
    end)

    self.view.notEnoughHint.gameObject:SetActiveIfNecessary(not enoughNormal or not enoughSelectInBag)
    self.view.btnSubmit.gameObject:SetActiveIfNecessary(enoughNormal and enoughSelectInBag)
    self.view.btnSubmit.interactable = enoughNormal and enoughSelect
end

SubmitItemInteractiveCtrl.OnSubmitItem = HL.Method(HL.Any) << function(self, submitId)
    if (submitId[1] == self.m_info.submitId) then
        self:Close()
    else
        print("SubmitItemInteractiveCtrl.OnSubmitItem: submitId not match", submitId, self.m_info.submitId)
    end
end

SubmitItemInteractiveCtrl.ShowPanel = HL.StaticMethod(HL.Any) << function(arg)
    arg = unpack(arg) or arg
    local self = UIManager:AutoOpen(PANEL_ID, arg)
end

HL.Commit(SubmitItemInteractiveCtrl)
