
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SeasonTowerDungeonInfoPopup

SeasonTowerDungeonInfoPopupCtrl = HL.Class('SeasonTowerDungeonInfoPopupCtrl', uiCtrl.UICtrl)






SeasonTowerDungeonInfoPopupCtrl.s_messages = HL.StaticField(HL.Table) << {
}

SeasonTowerDungeonInfoPopupCtrl.m_dungeonId = HL.Field(HL.String) << ""

SeasonTowerDungeonInfoPopupCtrl.m_closeCb = HL.Field(HL.Function)

SeasonTowerDungeonInfoPopupCtrl.m_paramBlackboard = HL.Field(CS.Beyond.Blackboard)
SeasonTowerDungeonInfoPopupCtrl.m_paramBlackboardFormatData = HL.Field(CS.Beyond.Gameplay.BlackboardFormatData)


SeasonTowerDungeonInfoPopupCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:_OnBtnCloseClick()
    end)

    self.view.mask.onClick:AddListener(function()
        self:_OnBtnCloseClick()
    end)

    self.m_dungeonId = arg.dungeonId
    self.m_closeCb = arg.closeCb

    self.m_paramBlackboard = CS.Beyond.Blackboard()
    self.m_paramBlackboardFormatData = CS.Beyond.Gameplay.BlackboardFormatData(self.m_paramBlackboard)

    self:_Refresh()

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

SeasonTowerDungeonInfoPopupCtrl._Refresh = HL.Method() << function(self)
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


    local hasFeature = DungeonUtils.isDungeonHasFeatureInfo(self.m_dungeonId)
    if hasFeature then
        self.view.feature.infoText:SetAndResolveTextStyle(
            CS.Beyond.Gameplay.FormatUtils.FormatBattleText(dungeonCfg.featureDesc, self.m_paramBlackboardFormatData))
    end
    self.view.feature.gameObject:SetActiveIfNecessary(hasFeature)


    local seasonTowerDungeonCfg = Tables.seasonTowerDungeonTable[self.m_dungeonId]
    if not string.isEmpty(seasonTowerDungeonCfg.specialBuffDesc) then
        self.view.specialBuff.infoText:SetAndResolveTextStyle(
            CS.Beyond.Gameplay.FormatUtils.FormatBattleText(seasonTowerDungeonCfg.specialBuffDesc, self.m_paramBlackboardFormatData))
        self.view.specialBuff.gameObject:SetActive(true)
    else
        self.view.specialBuff.gameObject:SetActive(false)
    end


    local _, _, extraCount = GameWorld.subGameManager:TryGetSubGameTaskCount(self.m_dungeonId)
    if extraCount and extraCount > 0 then

        local _, result = GameWorld.subGameManager:TryGetExtraTaskExtraInfo(self.m_dungeonId, 0)
        if result then
            self.view.extraTask.gameObject:SetActive(true)
            if result.useSingleDescription then
                self.view.extraTask.infoText.text = result.singleDescription:GetText()
            else
                for _, aim in cs_pairs(result.trackingInfoDict) do
                    self.view.extraTask.infoText.text = aim.description:GetText()
                    break
                end
            end
        else
            self.view.extraTask.gameObject:SetActive(false)
        end
    else
        self.view.extraTask.gameObject:SetActive(false)
    end
end

SeasonTowerDungeonInfoPopupCtrl._OnBtnCloseClick = HL.Method() << function(self)
    self:PlayAnimationOut(UIConst.PANEL_PLAY_ANIMATION_OUT_COMPLETE_ACTION_TYPE.Close)
    if self.m_closeCb then
        self.m_closeCb()
    end
end

HL.Commit(SeasonTowerDungeonInfoPopupCtrl)
