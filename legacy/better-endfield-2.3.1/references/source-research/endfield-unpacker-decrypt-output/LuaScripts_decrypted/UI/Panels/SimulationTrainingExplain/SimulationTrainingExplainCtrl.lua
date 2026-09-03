local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SimulationTrainingExplain
local PHASE_ID = PhaseId.SimulationTrainingExplain
SimulationTrainingExplainCtrl = HL.Class('SimulationTrainingExplainCtrl', uiCtrl.UICtrl)






SimulationTrainingExplainCtrl.s_messages = HL.StaticField(HL.Table) << {

}

SimulationTrainingExplainCtrl.m_enemyCells = HL.Field(HL.Forward("UIListCache"))

SimulationTrainingExplainCtrl.m_system = HL.Field(HL.Any)

SimulationTrainingExplainCtrl.m_gameId = HL.Field(HL.Any) << nil

SimulationTrainingExplainCtrl.m_close = HL.Field(HL.Boolean) << false


local EnemyCellPointImg = {
    [1] = "icon_simulation_training_big_1",
    [2] = "icon_simulation_training_big_2",
    [3] = "icon_simulation_training_big_3",
    [4] = "icon_simulation_training_big_4",
    [5] = "icon_simulation_training_big_5",
}

local PointNumberImg = {
    [1] = "icon_simulation_training_number_1",
    [2] = "icon_simulation_training_number_2",
    [3] = "icon_simulation_training_number_3",
    [4] = "icon_simulation_training_number_4",
    [5] = "icon_simulation_training_number_5",
}

local IMG_FOLDER = "SimulationTraining"

SimulationTrainingExplainCtrl.ShowSimulationTrainingExplain = HL.StaticMethod(HL.Opt(HL.Table)) << function(args)

    UIManager:Open(PANEL_ID, args)
end


SimulationTrainingExplainCtrl.OnClose = HL.Override() << function(self)
    if self.m_gameId ~= nil then
        GameInstance.player.subGameSys:SendPrepareFinished(self.m_gameId)
        self.m_system:HideInteractive()
    end
end


SimulationTrainingExplainCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    local maskData = CS.Beyond.Gameplay.UICommonMaskData()
    maskData.fadeType = UIConst.UI_COMMON_MASK_FADE_TYPE.FadeOut
    maskData.fadeOutTime = 0.2
    Notify(MessageConst.ON_COMMON_MASK_END, {maskData})

    local gameId, time = unpack(args)
    self.m_gameId = gameId
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    self.view.closeMaskBtn.onClick:RemoveAllListeners()
    self.view.closeMaskBtn.onClick:AddListener(function()

        if self.m_close then
            return
        end
        self.m_close = true
        self.animationWrapper:PlayOutAnimation(function()
            UIManager:Close(PANEL_ID)
        end)
    end)

    self.view.closeButton.onClick:RemoveAllListeners()
    self.view.closeButton.onClick:AddListener(function()

        if self.m_close then
            return
        end
        self.m_close = true
        self.animationWrapper:PlayOutAnimation(function()
            UIManager:Close(PANEL_ID)
        end)
    end)

    self.m_system = GameInstance.player.simulationTrainingSystem

    if self.m_system.curHandCards.Count == 0 then
        logger.error("敌人相关信息有效的数量为0，请检查配置")
        return
    end

    local monsterShowLevel = 0
    self.m_enemyCells = UIUtils.genCellCache(self.view.simulationTrainingListCell)
    self.m_enemyCells:Refresh(self.m_system.curHandCards.Count, function(cell, luaIndex)
        local csIndex = CSIndex(luaIndex)
        local cardName = self.m_system.curHandCards[csIndex]
        local hasCard, cardData = Tables.SimulationTrainingCardTable:TryGetValue(cardName)
        if hasCard then
            local cardPoint = cardData.cardPoint
            if cardData.enemyIdList.Count > 0 then
                local enemyId = cardData.enemyIdList[0]
                local enemyCount = cardData.enemyCountList[0]
                local enemyInfo = UIUtils.getEnemyInfoByIdAndLevel(enemyId, cardData.enemyLevel[0])
                cell.nameTxt.text = string.format(Language.LUA_SIMULATION_TRAINING_ENEMY_SHOW_NAME_AND_COUNT, enemyInfo.name, enemyCount)
                local level = enemyInfo.level + self.m_system.deBuffMonsterLevel
                if level > monsterShowLevel then
                    monsterShowLevel = level
                end
                if cardData.isBonusCard then
                    cell.stateController:SetState("Special")
                else
                    cell.stateController:SetState("Normal")
                end
                cell.lvTxt.text = level
                cell.iconImg:LoadSprite(UIConst.UI_SPRITE_MONSTER_ICON_BIG, enemyInfo.templateId)
                cell.numberImg.gameObject:SetActive(true)
                cell.numberImgVx.gameObject:SetActive(false)
                cell.numberImg:LoadSprite(IMG_FOLDER, PointNumberImg[cardPoint])
                cell.pointsImg:LoadSprite(IMG_FOLDER, EnemyCellPointImg[cardPoint])
            end
        end
    end)

    if self.m_system.deBuffTime > 0 or self.m_system.deBuffMonsterLevel > 0 then
        self.view.tipsLayoutNode.gameObject:SetActive(true)
        self.view.tipsTxt.gameObject:SetActive(true)
        time = time - self.m_system.deBuffTime
        self.view.tipsTxt.text = string.format(Language.LUA_SIMULATION_TRAINING_PRE_ATTACK_TIP, monsterShowLevel, math.floor(time/60), time%60)
    else
        self.view.tipsLayoutNode.gameObject:SetActive(false)
    end
    local savedRewardScore = self.m_system.rewardScoreNumber
    self.m_system:ClearCardAllInfo()
    self.m_system.rewardScoreNumber = savedRewardScore
end

HL.Commit(SimulationTrainingExplainCtrl)
