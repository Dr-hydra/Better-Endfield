local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.FacTechTreeUnlockTierPopup
FacTechTreeUnlockTierPopupCtrl = HL.Class('FacTechTreeUnlockTierPopupCtrl', uiCtrl.UICtrl)

FacTechTreeUnlockTierPopupCtrl.m_costItemCellCache = HL.Field(HL.Forward("UIListCache"))

FacTechTreeUnlockTierPopupCtrl.m_pageCellCache = HL.Field(HL.Forward("UIListCache"))

FacTechTreeUnlockTierPopupCtrl.m_curLayerIndex = HL.Field(HL.Number) << -1

FacTechTreeUnlockTierPopupCtrl.m_lockedLayerIds = HL.Field(HL.Table)






FacTechTreeUnlockTierPopupCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.FAC_ON_UNLOCK_TECH_TIER_UI] = 'OnUnlockTier',
}


FacTechTreeUnlockTierPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_lockedLayerIds = arg.lockedLayerIds
    self.m_costItemCellCache = UIUtils.genCellCache(self.view.costItemNode)
    self.m_pageCellCache = UIUtils.genCellCache(self.view.pageCell)

    self.view.clickMask.onClick:AddListener(function()
        self:_OnClickClose()
    end)

    self.view.cancelBtn.onClick:AddListener(function()
        self:_OnClickClose()
    end)

    self.view.confirmBtn.onClick:AddListener(function()
        self:_OnClickConfirm()
    end)

    self.view.nextBtn.onClick:AddListener(function()
        self:_OnClickSwitch(1)
    end)

    self.view.preBtn.onClick:AddListener(function()
        self:_OnClickSwitch(-1)
    end)

    local layerId = arg.layerId
    for index, lockedLayerId in ipairs(self.m_lockedLayerIds) do
        if layerId == lockedLayerId then
            self.m_curLayerIndex = index
            break
        end
    end

    local oneLocked = #self.m_lockedLayerIds == 1
    self.view.preBtn.gameObject:SetActive(not oneLocked)
    self.view.nextBtn.gameObject:SetActive(not oneLocked)
    self.view.pageNode.gameObject:SetActive(not oneLocked)

    self:_InitPageNode()
    self:_UpdateBtnState()
    self:_Refresh()

    self:_InitController()
end

FacTechTreeUnlockTierPopupCtrl.GetCurState = HL.Method().Return(HL.Table) << function(self)
    return {
        layerId = self.m_lockedLayerIds[self.m_curLayerIndex],
        lockedLayerIds = self.m_lockedLayerIds,
    }
end

FacTechTreeUnlockTierPopupCtrl.OnUnlockTier = HL.Method(HL.Table) << function(self, args)
    self:_OnClickClose()
end

FacTechTreeUnlockTierPopupCtrl._Refresh = HL.Method() << function(self)
    local layerId = self.m_lockedLayerIds[self.m_curLayerIndex]
    local layerData = Tables.facSTTLayerTable[layerId]

    local preLayerUnlocked = not GameInstance.player.facTechTreeSystem:LayerIsLocked(layerData.preLayer)
    local txt = preLayerUnlocked and self.view.layerUnlockLightTxt or self.view.layerUnlockDimTxt
    self.view.firstNode:SetState(preLayerUnlocked and "OK" or "NO")

    local unlockTitle = string.format(Language.LUA_FAC_TECH_TREE_UNLOCK_HINT_WITH_PRE, layerData.name)
    self.view.dimTxt.text = unlockTitle
    self.view.lightTxt.text = unlockTitle

    txt:SetAndResolveTextStyle(string.format(Language.LUA_FAC_TECH_TREE_UNLOCK_HINT_WITH_PRE,
                                                      Tables.facSTTLayerTable[layerData.preLayer].name))

    local costItemVOs = {}
    local isEnough = true
    for _, costItem in pairs(layerData.costItems) do
        local ownCount = Utils.getItemCount(costItem.costItemId)
        local costCount = costItem.costItemCount
        local costItemVO = {}
        costItemVO.id = costItem.costItemId
        costItemVO.ownCount = ownCount
        costItemVO.costCount = costCount
        costItemVO.isEnough = ownCount >= costCount
        if ownCount < costCount then
            isEnough = false
        end

        table.insert(costItemVOs, costItemVO)
    end

    self.m_costItemCellCache:Refresh(#costItemVOs, function(cell, index)
        local costItemVO = costItemVOs[index]
        cell.item:InitItem({ id = costItemVO.id, count = costItemVO.costCount }, true)
        cell.item:SetExtraInfo({ isSideTips = DeviceInfo.usingController })
        cell.lightText.gameObject:SetActive(costItemVO.isEnough)
        cell.dimText.gameObject:SetActive(not costItemVO.isEnough)

        local txt = costItemVO.isEnough and cell.lightText or cell.dimText
        txt.text = costItemVO.ownCount
    end)
    if #costItemVOs == 1 then
        self.m_costItemCellCache:Get(1).item.view.button.hideNaviHint = true
    end
    self.view.secondNode:SetState(isEnough and "OK" or "NO")

    local canUnlock = isEnough and preLayerUnlocked
    self.view.btnNode.gameObject:SetActive(canUnlock)
    self.view.closeHintTxt.gameObject:SetActive(not canUnlock)
    self.view.clickMask.gameObject:SetActive(not canUnlock)

    self.view.contentNode:SetState(canUnlock and "CanUnlock" or "CantUnlock")
end

FacTechTreeUnlockTierPopupCtrl._OnClickClose = HL.Method() << function(self)
    self:PlayAnimationOutAndClose()
end

FacTechTreeUnlockTierPopupCtrl._OnClickConfirm = HL.Method() << function(self)
    local layerId = self.m_lockedLayerIds[self.m_curLayerIndex]
    GameInstance.player.facTechTreeSystem:SendUnlockTierMsg(layerId)
end

FacTechTreeUnlockTierPopupCtrl._OnClickSwitch = HL.Method(HL.Number) << function(self, offset)
    local nextIndex = self.m_curLayerIndex + offset
    if nextIndex > #self.m_lockedLayerIds or nextIndex < 1 then
        logger.warn("locked layer index invalid=>", nextIndex)
        return
    end
    self.m_curLayerIndex = nextIndex

    self:_Refresh()
    self:_UpdateBtnState()
    self:_UpdatePageNode()
end

FacTechTreeUnlockTierPopupCtrl._UpdateBtnState = HL.Method() << function(self)
    local canNext = self.m_curLayerIndex < #self.m_lockedLayerIds
    local canPre = self.m_curLayerIndex > 1

    self.view.nextBtn.interactable = canNext
    self.view.preBtn.interactable = canPre
end

FacTechTreeUnlockTierPopupCtrl._InitPageNode = HL.Method() << function(self)
    self.m_pageCellCache:Refresh(#self.m_lockedLayerIds, function(cell, index)
        cell.stateController:SetState(self.m_curLayerIndex == index and "Select" or "Unselect")
    end)
end

FacTechTreeUnlockTierPopupCtrl._UpdatePageNode = HL.Method() << function(self)
    self.m_pageCellCache:Update(function(cell, index)
        cell.stateController:SetState(self.m_curLayerIndex == index and "Select" or "Unselect")
    end)
end

FacTechTreeUnlockTierPopupCtrl._InitController = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})

    self.view.downNode.onIsFocusedChange:AddListener(function(isFocus)
        self:_OnIsFocusedChange(isFocus)
    end)
end

FacTechTreeUnlockTierPopupCtrl._OnIsFocusedChange = HL.Method(HL.Boolean) << function(self, isFocus)
    self.view.preBtn.gameObject:SetActive(not isFocus)
    self.view.nextBtn.gameObject:SetActive(not isFocus)
end

HL.Commit(FacTechTreeUnlockTierPopupCtrl)
