local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

DungeonSelectionCell = HL.Class('DungeonSelectionCell', UIWidgetBase)

DungeonSelectionCell.m_styleNode = HL.Field(HL.Any)


DungeonSelectionCell._OnFirstTimeInit = HL.Override() << function(self)

end

DungeonSelectionCell.InitDungeonSelectionCell = HL.Method(HL.Any, HL.Function) << function(self, info, clickFunc)
    self:_FirstTimeInit()

    local dungeonMgr = GameInstance.dungeonManager
    local dungeonId = info
    local dungeonCfg = Tables.dungeonTable[dungeonId]
    local isUnlock = DungeonUtils.isDungeonUnlock(dungeonId)
    local isComplete = dungeonMgr:IsDungeonPassed(dungeonId)
    local text = dungeonCfg.dungeonLevelDesc

    local isTrain = DungeonUtils.isDungeonTrain(dungeonId)
    self.view.dungeonNormalSelectionNode.gameObject:SetActiveIfNecessary(not isTrain)
    self.view.dungeonTrainSelectionNode.gameObject:SetActiveIfNecessary(isTrain)

    self.m_styleNode = isTrain and self.view.dungeonTrainSelectionNode or self.view.dungeonNormalSelectionNode
    local node = self.m_styleNode
    node.txtN.text = text
    node.txtS.text = text

    node.lockedIconN.gameObject:SetActiveIfNecessary(not isUnlock)
    node.lockedIconS.gameObject:SetActiveIfNecessary(not isUnlock)

    node.finishedIconN.gameObject:SetActiveIfNecessary(isUnlock and isComplete)
    node.finishedIconS.gameObject:SetActiveIfNecessary(isUnlock and isComplete)

    node.button.onClick:RemoveAllListeners()
    node.button.onClick:AddListener(function()
        if clickFunc then
            clickFunc(self)
        end
    end)
end

DungeonSelectionCell.SetSelected = HL.Method(HL.Boolean) << function(self, selected)
    if selected then
        self.m_styleNode.animationWrapper:PlayInAnimation()
    else
        self.m_styleNode.animationWrapper:PlayOutAnimation()
    end
end

HL.Commit(DungeonSelectionCell)
return DungeonSelectionCell

