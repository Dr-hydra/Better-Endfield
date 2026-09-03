
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SeasonTowerMainHud
local PHASE_ID = PhaseId.SeasonTowerMainHud
local COMMON_INTRO_ID = "season_tower"
SeasonTowerMainHudCtrl = HL.Class('SeasonTowerMainHudCtrl', uiCtrl.UICtrl)

SeasonTowerMainHudCtrl.m_system = HL.Field(CS.Beyond.Gameplay.SeasonTowerSystem)

SeasonTowerMainHudCtrl.m_listCells = HL.Field(HL.Any)

SeasonTowerMainHudCtrl.m_weekCfg = HL.Field(HL.Any)

SeasonTowerMainHudCtrl.m_countdownCor = HL.Field(HL.Any)





SeasonTowerMainHudCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SEASON_TOWER_NEW] = '_OnRefreshWeek',

}


SeasonTowerMainHudCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_system = GameInstance.player.seasonTowerSystem

    self.view.achieveRedDot:InitRedDot("SeasonTowerAchieve")
    self.view.newSeasonRedDot:InitRedDot("SeasonTowerNewSeason")

    self.view.btnClose.onClick:AddListener(function()
        PhaseManager:PopPhase(PHASE_ID)
    end)
    self.view.helpBtn.onClick:AddListener(function()
        Notify(MessageConst.SHOW_INTRO, COMMON_INTRO_ID)
    end)

    self.view.achieveBtn.onClick:AddListener(function()
        if self.m_system.levelAchieve.Count > 0 then
            PhaseManager:OpenPhase(PhaseId.SeasonTowerAchieve)
        else
            Notify(MessageConst.SHOW_TOAST, Language.LUA_SEASON_TOWER_ACHIEVE_NOT_OPEN)
        end
    end)

    self.view.historyBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.SeasonTowerScoreReview)
    end)

    self.m_listCells = UIUtils.genCellCache(self.view.cellNode)


    if DeviceInfo.usingController then
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    end


    self:_TryRecoverCommonIntro(arg and arg.commonIntroState or nil)
end

SeasonTowerMainHudCtrl.OnShow = HL.Override() << function(self)
    self:_OnRefreshWeek()
    if DeviceInfo.usingController then
        self.view.levelsSelectNavi:NaviToThisGroup(false)
    end
end




SeasonTowerMainHudCtrl._OnRefreshWeek = HL.Method() << function(self)
    if self.m_system.currentSeasonId <= 0 or self.m_system.currentWeekId <= 0 then
        logger.error('没有有效数据!当前赛季id：%d，当前周id：%d', self.m_system.currentSeasonId, self.m_system.currentWeekId)
        return
    end
    if not UIManager:IsShow(PANEL_ID) then
        return
    end
    local seasonCfg = Tables.seasonTowerTable[self.m_system.currentSeasonId]
    self.m_weekCfg = seasonCfg.weeks[self.m_system.currentWeekId]
    self.m_listCells:Refresh(#self.m_weekCfg.includeGameIdList, function(cell, index)
        self:_OnRefreshCell(cell, index)
    end)

    self:_StopCountdownCor()

    self.view.weekNameTxt.text = self.m_weekCfg.weekShowName
    self.view.weekRankTag:SetState(SeasonTowerUtils.getRankStateName(self.m_system.weekRecord.rank))
    if SeasonTowerUtils.isFinalWeek() then

        self.view.seasonInfo:InitSeasonTowerSeasonInfo(self.m_system.seasonRecord, true)
        self.view.weekTimeTxt.text = Language.LUA_SEASON_TOWER_FINAL_WEEK
    else

        self.view.seasonInfo:InitSeasonTowerSeasonInfo(self.m_system.seasonRecord, false)
        self.view.weekTimeTxt.text = string.format(Language.LUA_SEASON_TOWER_REMAIN_WEEK_TIME, SeasonTowerUtils.getRemainTimeText())
        self.m_countdownCor = self:_StartCoroutine(function()
            while true do
                coroutine.wait(1)
                local remainText = SeasonTowerUtils.getRemainTimeText()
                self.view.weekTimeTxt.text = string.format(Language.LUA_SEASON_TOWER_REMAIN_WEEK_TIME, remainText)
            end
        end)
    end

    if self.m_system.levelAchieve.Count > 0 then
        self.view.achieveBtnState:SetState("Nrl")
    else
        self.view.achieveBtnState:SetState("Lock")
    end

    self:_StartCoroutine(function()
        coroutine.step()
        SeasonTowerUtils.setMainRedDotChecked()
        self.m_system:SetRefreshed()
    end)
end

SeasonTowerMainHudCtrl._StopCountdownCor = HL.Method() << function(self)
    if self.m_countdownCor then
        self:_ClearCoroutine(self.m_countdownCor)
        self.m_countdownCor = nil
    end
end

SeasonTowerMainHudCtrl._OnRefreshCell = HL.Method(HL.Any, HL.Int) << function(self, cell, luaIndex)
    local levelId = self.m_weekCfg.includeGameIdList[CSIndex(luaIndex)]

    local res, levelData = self.m_system.weekRecord.levelRecords:TryGetValue(levelId)
    local levelCfg = Tables.seasonTowerGameGroupTable[levelId]
    local gameGroupCfg = Tables.gameMechanicGroupTable[levelId]

    local levelCell = cell
    levelCell.levelsNameTxt.text = gameGroupCfg.gameGroupName
    levelCell.levelsIcon:LoadSprite(UIConst.UI_SPRITE_SEASONTOWER, levelCfg.icon)
    levelCell.starList:InitStarGroupWithState(3, levelData and levelData.starNum or 0,
        levelData.completeTask and "03" or "02", "01")
    levelCell.buttonNode.onClick:RemoveAllListeners()
    levelCell.buttonNode.onClick:AddListener(function()
        self:_OnSelectLevel(levelId)
    end)
    levelCell.buttonNode.onIsNaviTargetChanged = nil
    levelCell.buttonNode.onIsNaviTargetChanged = function(isNaviTarget)
        levelCell.keyHint.gameObject:SetActive(isNaviTarget)
    end
    levelCell.keyHint.gameObject:SetActive(false)
    levelCell.redDot:InitRedDot("SeasonTowerNew")
end

SeasonTowerMainHudCtrl._OnSelectLevel = HL.Method(HL.String) << function(self, gameGroupId)
    PhaseManager:OpenPhase(PhaseId.SeasonTowerDungeonEntry, {levelId = gameGroupId})
end


SeasonTowerMainHudCtrl.GetCurPhaseStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    return {
        commonIntroState = self:_GetCommonIntroRecoverState(),
    }
end


SeasonTowerMainHudCtrl._GetCommonIntroRecoverState = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end
    local isOpen, introCtrl = UIManager:IsOpen(PanelId.CommonIntro)
    if not isOpen or not introCtrl:IsShow() then
        return
    end
    local recoverState = introCtrl:GetRecoverStateArg()
    if recoverState == nil or recoverState.introId ~= COMMON_INTRO_ID then
        return
    end
    return recoverState
end

SeasonTowerMainHudCtrl._TryRecoverCommonIntro = HL.Method(HL.Opt(HL.Any)) << function(self, recoverState)
    if recoverState == nil then
        return
    end
    if UIManager:IsOpen(PanelId.CommonIntro) then
        return
    end
    UIManager:Open(PanelId.CommonIntro, recoverState)
end




HL.Commit(SeasonTowerMainHudCtrl)
