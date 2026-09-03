local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

DungeonCommonSelectionGroupSubCell = HL.Class('DungeonCommonSelectionGroupSubCell', UIWidgetBase)

DungeonCommonSelectionGroupSubCell.m_dungeonId = HL.Field(HL.String) << ""

DungeonCommonSelectionGroupSubCell.m_clickFunc = HL.Field(HL.Function)

local UIState = {
    Lock = "Lock",
    Unlock = "UnLock",
    Complete = "Complete",

    Select = "Select",
    Unselect = "UnSelect",
}


DungeonCommonSelectionGroupSubCell._OnFirstTimeInit = HL.Override() << function(self)
    self.view.clickBtn.onClick:AddListener(function()
        if self.m_clickFunc then
            self.m_clickFunc(self, self.m_dungeonId)
        end
    end)
end

DungeonCommonSelectionGroupSubCell.InitDungeonCommonSelectionGroupSubCell = HL.Method(HL.String, HL.Function)
    << function(self, dungeonId, clickFunc)
    self:_FirstTimeInit()

    self.m_dungeonId = dungeonId
    self.m_clickFunc = clickFunc

    local dungeonCfg = Tables.dungeonTable[dungeonId]
    self.view.numTxt.text = dungeonCfg.dungeonLevelDesc

    self:SetSelected(false)
    self:_UpdateState()

    self.view.redDot:InitRedDot("DungeonReadNormal", {dungeonId}, nil, self:GetUICtrl().view.redDotScrollRect)
end

DungeonCommonSelectionGroupSubCell.SetSelected = HL.Method(HL.Boolean) << function(self, isOn)
    self.view.stateController:SetState(isOn and UIState.Select or UIState.Unselect)



    if not isOn then
        self:_UpdateState()
    end


    if isOn then
        GameInstance.player.subGameSys:SendSubGameListRead({ self.m_dungeonId })
    end
end

DungeonCommonSelectionGroupSubCell._UpdateState = HL.Method() << function(self)
    local dungeonId = self.m_dungeonId

    local dungeonCfg = Tables.dungeonTable[dungeonId]
    local isUnlock = DungeonUtils.isDungeonUnlock(dungeonId)
    local isComplete = DungeonUtils.isDungeonPassed(dungeonId)

    local state1
    if isComplete then
        state1 = UIState.Complete
    elseif isUnlock then
        state1 = UIState.Unlock
    else
        state1 = UIState.Lock
    end
    self.view.stateController:SetState(state1)
end

HL.Commit(DungeonCommonSelectionGroupSubCell)
return DungeonCommonSelectionGroupSubCell

