local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DeathInfoFake
local DisplayMode = CS.Beyond.Gameplay.Core.DeathPerformance.CommonDeathPanelDisplayMode

DeathInfoFakeCtrl = HL.Class('DeathInfoFakeCtrl', uiCtrl.UICtrl)


DeathInfoFakeCtrl.s_messages = HL.StaticField(HL.Table) << {
}

DeathInfoFakeCtrl._TryGetRandomTrainingTip = HL.Method(HL.String).Return(HL.Opt(HL.String)) << function(self, trainingType)
    local trainingTipGroupWrapper = Tables.trainingDeathTips[trainingType]
    if not trainingTipGroupWrapper then
        return nil
    end
    local tipGroup = trainingTipGroupWrapper.tipContents
    if not tipGroup or #tipGroup == 0 then
        return nil
    end
    local tipIndex = CSIndex(math.random(#tipGroup))
    return tipGroup[tipIndex]
end

DeathInfoFakeCtrl._TryShowTrainingTip = HL.Method(HL.Userdata, HL.Userdata, HL.Table).Return(HL.Boolean) << function(self, trainingStd, trainingTypeInfo, deathInfo)
    local trainingType = trainingTypeInfo.trainingType
    if not trainingStd[trainingType] or trainingStd[trainingType] <= 0 then
        return false
    end
    local trainingStdOfType = trainingStd[trainingType]
    if not trainingStdOfType or trainingStdOfType <= 0 then
        return false
    end
    local degree = deathInfo[trainingType] / trainingStdOfType
    if degree >= trainingTypeInfo.trainingThresholdFactor then
        return false
    end
    local candidateTip = self:_TryGetRandomTrainingTip(trainingType)
    if not candidateTip then
        return false
    end
    self.view.trainingTips.gameObject:SetActive(true)
    self.view.trainingTipText:SetAndResolveTextStyle(candidateTip)
    self.view.trainingProgressBarLabel.text = trainingTypeInfo.progressBarLabel
    self.view.trainingProgress.fillAmount = degree
    return true
end

DeathInfoFakeCtrl._ShowTips = HL.Method(HL.Userdata, HL.Number, HL.Opt(HL.Number), HL.Opt(HL.Number)).Return(HL.Boolean) << function(self, tipGroup, indexOffset, index1, index2)
    if not tipGroup or #tipGroup == 0 then
        return false
    end

    local tipIndex1 = index1 or math.random(#tipGroup)
    self.view.tipText01:SetAndResolveTextStyle(tipGroup[tipIndex1 + indexOffset])

    if #tipGroup == 1 then
        return true
    end

    self.view.tipNode02.gameObject:SetActive(true)
    local tipIndex2
    if index2 then
        tipIndex2 = index2
    else
        tipIndex2 = math.random(#tipGroup - 1)
        if tipIndex2 >= tipIndex1 then
            tipIndex2 = tipIndex2 + 1
        end
    end
    self.view.tipText02:SetAndResolveTextStyle(tipGroup[tipIndex2 + indexOffset])
    return true
end

DeathInfoFakeCtrl._TryShowInDungeonMode = HL.Method(HL.Table, HL.Opt(HL.Number), HL.Opt(HL.Number)).Return(HL.Boolean) << function(self, deathInfo, index1, index2)
    local dungeonId = deathInfo.dungeonId
    if not dungeonId then
        return false
    end
    local _, tipGroupBean = Tables.dungeonDeathTips:TryGetValue(dungeonId)
    if not tipGroupBean then
        return false
    end
    if not self:_ShowTips(tipGroupBean.tipContents, -1, index1, index2) then
        return false
    end
    self.view.enemyTipsHeader.gameObject:SetActive(false)
    self.view.commonTipsHeader.gameObject:SetActive(true)
    return true
end

DeathInfoFakeCtrl._TryShowInEnemyMode = HL.Method(HL.Table, HL.Opt(HL.Number), HL.Opt(HL.Number)).Return(HL.Boolean) << function(self, deathInfo, index1, index2)
    if not deathInfo.enemyId or deathInfo.enemyLv < 0 then
        return false
    end

    local _, tipGroupBean = Tables.enemyRelatedDeathTips:TryGetValue(deathInfo.enemyId)
    if not tipGroupBean then
        return false
    end
    if not self:_ShowTips(tipGroupBean.tipContents, -1, index1, index2) then
        return false
    end

    local enemyInfo = UIUtils.getEnemyInfoByIdAndLevel(deathInfo.enemyId, deathInfo.enemyLv)
    self.view.enemyTipsHeader.gameObject:SetActive(true)
    self.view.commonTipsHeader.gameObject:SetActive(false)
    self.view.enemyAvatar:LoadSprite(UIConst.UI_SPRITE_MONSTER_ICON_BIG, enemyInfo.templateId)
    self.view.enemyNameText.text = enemyInfo.name
    return true
end

DeathInfoFakeCtrl._ShowCommonFallback = HL.Method(HL.Opt(HL.Number, HL.Number)) << function(self, index1, index2)
    self:_ShowTips(Tables.commonDeathTips, 0, index1, index2)
    self.view.enemyTipsHeader.gameObject:SetActive(false)
    self.view.commonTipsHeader.gameObject:SetActive(true)
end


DeathInfoFakeCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    local deathInfo = arg.deathInfo
    local index1 = arg.index1
    local index2 = arg.index2
    local displayMode = arg.displayMode or DisplayMode.WorldDeath

    self.view.tipNode02.gameObject:SetActive(false)
    self.view.trainingTips.gameObject:SetActive(false)
    self.view.exitDungeonBtn.gameObject:SetActive(false)
    self.view.retryBattleBtn.gameObject:SetActive(true)

    self.view.retryBattleBtn.onClick:AddListener(function()
        UIManager:Close(PANEL_ID)
    end)


    local _, trainingStd = Tables.recommendTraining:TryGetValue(deathInfo.enemyLv)
    if trainingStd then
        local checkTypeInOrder = {}
        for _, trainingTypeInfo in pairs(Cfg.Tables.trainingTypeInfoTable) do
            checkTypeInOrder[trainingTypeInfo.priority] = trainingTypeInfo
        end
        for priority = 1, #checkTypeInOrder do
            if self:_TryShowTrainingTip(trainingStd, checkTypeInOrder[priority], deathInfo) then
                break
            end
        end
    end


    if displayMode == DisplayMode.Miasma then
        if not self:_ShowTips(Tables.miasmaDeathTips, 0, index1, index2) then
            self:_ShowTips(Tables.commonDeathTips, 0, index1, index2)
        end
        self.view.enemyTipsHeader.gameObject:SetActive(false)
        self.view.commonTipsHeader.gameObject:SetActive(true)
        return
    end

    if displayMode == DisplayMode.DungeonFail then
        if self:_TryShowInDungeonMode(deathInfo, index1, index2) then
            return
        end
        if self:_TryShowInEnemyMode(deathInfo, index1, index2) then
            return
        end
        self:_ShowCommonFallback(index1, index2)
        return
    end


    if self:_TryShowInEnemyMode(deathInfo, index1, index2) then
        return
    end
    self:_ShowCommonFallback(index1, index2)
end

HL.Commit(DeathInfoFakeCtrl)
