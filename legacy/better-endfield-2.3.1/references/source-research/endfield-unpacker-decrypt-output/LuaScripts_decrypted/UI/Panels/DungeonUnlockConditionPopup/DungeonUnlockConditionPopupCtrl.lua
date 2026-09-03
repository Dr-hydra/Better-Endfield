
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DungeonUnlockConditionPopup

DungeonUnlockConditionPopupCtrl = HL.Class('DungeonUnlockConditionPopupCtrl', uiCtrl.UICtrl)

DungeonUnlockConditionPopupCtrl.m_unlockConditionCellCache = HL.Field(HL.Forward("UIListCache"))

DungeonUnlockConditionPopupCtrl.m_dungeonId = HL.Field(HL.String) << ""





DungeonUnlockConditionPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}


DungeonUnlockConditionPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:_OnBtnCloseClick()
    end)

    self.view.mask.onClick:AddListener(function()
        self:_OnBtnCloseClick()
    end)

    self.m_dungeonId = arg.dungeonId
    self.m_unlockConditionCellCache = UIUtils.genCellCache(self.view.unlockConditionCell)

    self:_Refresh()


    if self.m_unlockConditionCellCache:GetCount() >= 1 then
        local cell = self.m_unlockConditionCellCache:Get(1)
        local decorator = cell.gameObject:GetComponent("InputBindingGroupNaviDecorator")
        self:SetNaviTarget(decorator)
    end
end











DungeonUnlockConditionPopupCtrl._OnBtnCloseClick = HL.Method() << function(self)
    self:PlayAnimationOut(UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Close)
end

DungeonUnlockConditionPopupCtrl._Refresh = HL.Method() << function(self)
    local uncompletedConditionIds = DungeonUtils.getUncompletedConditionIds(self.m_dungeonId)
    local uncompletedConditionInfo = {}
    for _, conditionId in ipairs(uncompletedConditionIds) do
        local gameMechanicConditionCfg = Tables.gameMechanicConditionTable[conditionId]

        local isKnown = true
        table.insert(uncompletedConditionInfo,{
            conditionId = conditionId,
            desc = isKnown and gameMechanicConditionCfg.desc,
            type = gameMechanicConditionCfg.conditionType:GetHashCode(),
            isKnown = isKnown,
            customSortId1 = isKnown and 1 or 0,
        })
    end
    table.sort(uncompletedConditionInfo, Utils.genSortFunction({ "customSortId1", "type" }))

    self.m_unlockConditionCellCache:Refresh(#uncompletedConditionInfo, function(cell, luaIndex)
        self:_UpdateCell(cell, uncompletedConditionInfo[luaIndex])
    end)
end

DungeonUnlockConditionPopupCtrl._UpdateCell = HL.Method(HL.Any, HL.Table) << function(self, cell, info)
    cell.normalNode.gameObject:SetActiveIfNecessary(info.isKnown)
    cell.disableNode.gameObject:SetActiveIfNecessary(not info.isKnown)

    local node = info.isKnown and cell.normalNode or cell.disableNode
    node.unlockDescTxt.text = info.desc
    local canJump = DungeonUtils.getConditionCanJump(self.m_dungeonId, info.conditionId)
    cell.gotoNode.gameObject:SetActiveIfNecessary(canJump)

    cell.button.onClick:RemoveAllListeners()
    cell.button.interactable = canJump
    if canJump then
        cell.button.onClick:AddListener(function()
            if info.isKnown then
                DungeonUtils.diffActionByConditionId(info.conditionId)
            end
        end)
    end
end

HL.Commit(DungeonUnlockConditionPopupCtrl)
