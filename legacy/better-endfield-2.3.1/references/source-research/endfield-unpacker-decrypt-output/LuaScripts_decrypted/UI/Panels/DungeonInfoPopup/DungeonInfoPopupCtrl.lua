
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DungeonInfoPopup

DungeonInfoPopupCtrl = HL.Class('DungeonInfoPopupCtrl', uiCtrl.UICtrl)






DungeonInfoPopupCtrl.s_messages = HL.StaticField(HL.Table) << {

}


DungeonInfoPopupCtrl.TryToShow = HL.StaticMethod(HL.Any) << function(args)
    local dungeonId = unpack(args)
    DungeonUtils.tryAutoShowDungeonPopup(dungeonId)
end


DungeonInfoPopupCtrl.OnShowDungeonInfoPopup = HL.StaticMethod() << function()
    DungeonUtils.showDungeonPopupByEvent()
end

DungeonInfoPopupCtrl.m_dungeonId = HL.Field(HL.String) << ""

DungeonInfoPopupCtrl.m_closeCb = HL.Field(HL.Function)

DungeonInfoPopupCtrl.m_dungeonInfoCellCache = HL.Field(HL.Forward("UIListCache"))
DungeonInfoPopupCtrl.m_paramBlackboard = HL.Field(CS.Beyond.Blackboard)
DungeonInfoPopupCtrl.m_paramBlackboardFormatData = HL.Field(CS.Beyond.Gameplay.BlackboardFormatData)


DungeonInfoPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:_OnBtnCloseClick()
    end)

    self.view.mask.onClick:AddListener(function()
        self:_OnBtnCloseClick()
    end)

    self:BindInputPlayerAction("common_dungeon_info", function()
        self:_OnBtnCloseClick()
    end, self.view.btnClose.groupId)

    self.m_dungeonId = arg.dungeonId
    self.m_closeCb = arg.closeCb
    self.m_dungeonInfoCellCache = UIUtils.genCellCache(self.view.dungeonInfoCell)
    self.m_paramBlackboard = CS.Beyond.Blackboard()
    self.m_paramBlackboardFormatData = CS.Beyond.Gameplay.BlackboardFormatData(self.m_paramBlackboard)

    self:_Refresh()

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

DungeonInfoPopupCtrl._Refresh = HL.Method() << function(self)
    local dungeonCfg = Tables.dungeonTable[self.m_dungeonId]
    local gameMechanicCfg = Tables.gameMechanicTable[self.m_dungeonId]
    local dungeonTypeCfg = Tables.dungeonTypeTable[gameMechanicCfg.gameCategory]


    self.m_paramBlackboard:Clear()
    if dungeonCfg.paramList then
        for i = 1, dungeonCfg.paramList.Count do
            local param = dungeonCfg.paramList[CSIndex(i)]
            self.m_paramBlackboard:Assign(param.key, param.value)
        end
    end

    self.view.titleText.text = string.isEmpty(dungeonTypeCfg.dungeonInfoTitle) and "TBD" or dungeonTypeCfg.dungeonInfoTitle

    local positionText = DungeonUtils.getEntryLocation(dungeonCfg.levelId, true)
    self.view.positionTxt.text = positionText
    self.view.positionNode.gameObject:SetActiveIfNecessary(not string.isEmpty(positionText))


    local featureInfos = DungeonUtils.getListByStr(dungeonCfg.featureDesc)
    local hasFeature = #featureInfos > 0
    if hasFeature then
        self.m_dungeonInfoCellCache:Refresh(#featureInfos, function(cell, index)
            cell.txt:SetAndResolveTextStyle(CS.Beyond.Gameplay.FormatUtils.FormatBattleText(featureInfos[index], self.m_paramBlackboardFormatData))
        end)
    end
    self.view.featureNode.gameObject:SetActiveIfNecessary(hasFeature)
end

DungeonInfoPopupCtrl._OnBtnCloseClick = HL.Method() << function(self)
    self:PlayAnimationOut(UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Close)
    if self.m_closeCb then
        self.m_closeCb()
    end
end

HL.Commit(DungeonInfoPopupCtrl)
