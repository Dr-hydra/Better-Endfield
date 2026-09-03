
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DungeonCharTutorialStepHud

local StepNodeState = {
    Normal = "Normal",
    Succ = "Succ",
    Fail = "Fail",
}

local StepState = {
    Fail = 0,
    Succ = 1,
}

local Phase = {
    Normal = 1,
    Fail = 2,
    CompleteMainGoal = 3,
    CompleteAllGoal = 4,
}

local StepNodeAnim = {
    Cur = "actionstepnormal_loop",
    Succ = "actionstepnormal_succ",
    Fail = "actionstepnormal_fail",
}

DungeonCharTutorialStepHudCtrl = HL.Class('DungeonCharTutorialStepHudCtrl', uiCtrl.UICtrl)

DungeonCharTutorialStepHudCtrl.m_dungeonId = HL.Field(HL.String) << ""

DungeonCharTutorialStepHudCtrl.m_curStage = HL.Field(HL.Number) << 0

DungeonCharTutorialStepHudCtrl.m_curStep = HL.Field(HL.Number) << 0

DungeonCharTutorialStepHudCtrl.m_curStepIds = HL.Field(HL.Any)

DungeonCharTutorialStepHudCtrl.m_actionStepCellCached = HL.Field(HL.Forward("UIListCache"))

DungeonCharTutorialStepHudCtrl.m_failResetCor = HL.Field(HL.Thread)

DungeonCharTutorialStepHudCtrl.m_inFailCor = HL.Field(HL.Boolean) << false

DungeonCharTutorialStepHudCtrl.m_succScrollCor = HL.Field(HL.Thread)

DungeonCharTutorialStepHudCtrl.OnDungeonGameStart = HL.StaticMethod(HL.Table) << function(args)
    local dungeonId = unpack(args)
    if not DungeonUtils.isDungeonCharTutorial(dungeonId) then
        return
    end
    UIManager:Open(PANEL_ID, dungeonId)
end

DungeonCharTutorialStepHudCtrl.OpenCharTutorialStepHud = HL.StaticMethod(HL.Table) << function(args)
    local dungeonId = unpack(args)
    if string.isEmpty(dungeonId) then
        dungeonId = GameInstance.dungeonManager.curDungeonId
    end


    if not Tables.dungeonCharTutorialTable:ContainsKey(dungeonId) then
        return
    end

    UIManager:Open(PANEL_ID, dungeonId)
end





DungeonCharTutorialStepHudCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CHAR_TUTORIAL_STEP_STATE_CHANGE] = 'OnTutorialStepStateChangeV2',

    [MessageConst.ON_SUB_GAME_FINISH_STATE_CHANGE] = "OnSubGameFinishStateChange",
    [MessageConst.ON_SUB_GAME_STAGE_FINISH] = "OnSubGameStageFinish",
    [MessageConst.ON_SUB_GAME_STAGE_CHANGE_FINISH] = "OnSubGameStageChangeFinish",

    [MessageConst.LEVEL_SCRIPT_PUSH_STAGE] = "OnLevelScriptPushStage",
    [MessageConst.TOGGLE_CHAR_TUTORIAL_STEP_HUD] = "ToggleCharTutorialStepHUD",

    [MessageConst.ON_SUB_GAME_RESET] = "OnSubGameReset",
}


DungeonCharTutorialStepHudCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_actionStepCellCached = UIUtils.genCellCache(self.view.actionStepCell)
    self.m_dungeonId = arg

    self:_RefreshStage()
end








DungeonCharTutorialStepHudCtrl.OnTutorialStepStateChangeV2 = HL.Method(HL.Any) << function(self, arg)
    local step, state, needResetStep = unpack(arg)
    logger.warn("[char tutorial] step:", step, "state:", state, "needResetStep:", needResetStep)
    if state == StepState.Fail then

        local curStepCell = self.m_actionStepCellCached:Get(self.m_curStep)

        curStepCell.animationWrapper:Play(StepNodeAnim.Fail)
        AudioAdapter.PostEvent("Au_UI_Mission_TrainError")


        if needResetStep then
            local waitTime = curStepCell.animationWrapper:GetClipLength(StepNodeAnim.Fail)
            self.m_curStep = 1
            self.m_inFailCor = true
            self.m_failResetCor = self:_StartCoroutine( function()
                coroutine.wait(waitTime)
                self:_RefreshStage()
                self.m_inFailCor = false
            end)
        else
            self:_StartTimer(self.view.config.SHOW_RESET_HINT_TIME_DELAY,function()
                self:_ToggleResetHintNode(true)
            end)
        end
    else







        local totalSteps =  self.m_curStepIds.Count
        for i = 1, totalSteps do
            local stepCell = self.m_actionStepCellCached:Get(i)
            local percent
            if i < step then

                percent = 1
            elseif i > step then

                percent = 0
            end
            stepCell.animationWrapper:SampleClipAtPercent(StepNodeAnim.Succ, percent)
            stepCell.imageSelect.gameObject:SetActive(false)
        end

        local curStepCell = self.m_actionStepCellCached:Get(step)
        curStepCell.animationWrapper:SampleClipAtPercent(StepNodeAnim.Cur, 0)
        curStepCell.animationWrapper:Play(StepNodeAnim.Succ)
        self.view.actionStepScrollView:AutoScrollToRectTransform(curStepCell.transform, true)
        AudioAdapter.PostEvent("Au_UI_Mission_TrainStep_Complete")

        if step < totalSteps then
            local nextStep = step + 1
            local nextCell = self.m_actionStepCellCached:Get(nextStep)
            nextCell.animationWrapper:Play(StepNodeAnim.Cur)
            nextCell.imageSelect.gameObject:SetActive(true)

            local succTime = curStepCell and curStepCell.animationWrapper:GetClipLength(StepNodeAnim.Succ) or 0.1
            self.m_curStep = nextStep


            self.view.actionStepScrollView:AutoScrollToRectTransform(nextCell.transform)
        end
    end

    self:_UpdateStepDesc()
end

DungeonCharTutorialStepHudCtrl.OnSubGameFinishStateChange = HL.Method(HL.Any) << function(self, args)


    local _, phase = unpack(args)
    self.view.finishNode.gameObject:SetActive(phase == Phase.CompleteAllGoal or phase == Phase.CompleteMainGoal)
    self.view.stepDescNode.gameObject:SetActive(phase == Phase.Normal or phase == Phase.Fail)
end

DungeonCharTutorialStepHudCtrl.OnSubGameStageFinish = HL.Method() << function(self)
    self.view.finishNode.gameObject:SetActive(true)
    self.view.stepDescNode.gameObject:SetActive(false)
end

DungeonCharTutorialStepHudCtrl.OnSubGameStageChangeFinish = HL.Method() << function(self)


end

DungeonCharTutorialStepHudCtrl.OnLevelScriptPushStage = HL.Method(HL.Table) << function(self, args)

    local pushStage = unpack(args)
    self:_RefreshStage(pushStage)
end

DungeonCharTutorialStepHudCtrl.OnSubGameReset = HL.Method() << function(self)
    if self.m_failResetCor then
        self.m_failResetCor = self:_ClearCoroutine(self.m_failResetCor)
        self.m_inFailCor = false
    end

    if self.m_succScrollCor then
        self.m_succScrollCor = self:_ClearCoroutine(self.m_succScrollCor)
    end

    self:_RefreshStage()
end

DungeonCharTutorialStepHudCtrl.ToggleCharTutorialStepHUD = HL.Method(HL.Table) << function(self, args)
    local isOn, closeHUD = unpack(args)
    if isOn then
        self:Show()
    elseif not closeHUD then
        self:Hide()
    else
        self:Close()
    end
end

DungeonCharTutorialStepHudCtrl._RefreshStage = HL.Method(HL.Opt(HL.Number)) << function(self, pushStage)
    local charTutorialCfg = Tables.dungeonCharTutorialTable[self.m_dungeonId]

    local game = GameInstance.dungeonManager.curDungeonLikeSubGame
    local stage = game.stage
    if pushStage ~= nil then
        stage = pushStage
    end

    local tutorialStageCfg = charTutorialCfg.tutorialStageData[CSIndex(stage)]
    local stepIds = tutorialStageCfg.stepIds

    self.m_curStepIds = stepIds
    self.m_curStage = stage

    self.m_curStep = 1


    self.m_actionStepCellCached:Refresh(stepIds.Count, function(cell, luaIndex)
        local csIndex = CSIndex(luaIndex)
        local stepId = self.m_curStepIds[csIndex]
        local stepCfg = Tables.dungeonCharTutorialStepTable[stepId]
        local curLuaIndex = LuaIndex(csIndex)
        cell.skillIcon:LoadSprite(UIConst.UI_SPRITE_SKILL_ICON, stepCfg.stepIcon)
        cell.skillDesc.text = stepCfg.iconDesc

        cell.animationWrapper:SampleClipAtPercent(StepNodeAnim.Succ, curLuaIndex < self.m_curStep and 1 or 0)
        cell.imageSelect.gameObject:SetActive(self.m_curStep == LuaIndex(csIndex))
        cell.gameObject.name = stepId

        if curLuaIndex == self.m_curStep then
            cell.animationWrapper:Play(StepNodeAnim.Cur)
        end
    end)
    self.view.actionStepScrollView:AutoScrollToRectTransform(self.m_actionStepCellCached:Get(self.m_curStep).transform)

    self:_ToggleResetHintNode(false)

    self:_UpdateStepDesc()
    self:_ResetSubGameStageState()
end

DungeonCharTutorialStepHudCtrl._UpdateStepDesc = HL.Method() << function(self)
    local curStepId = self.m_curStepIds[CSIndex(self.m_curStep)]
    local curStepCfg = Tables.dungeonCharTutorialStepTable[curStepId]

    self.view.stepDescTxt.text = curStepCfg.stepDesc
end

DungeonCharTutorialStepHudCtrl._ResetSubGameStageState = HL.Method() << function(self)
    self:OnSubGameFinishStateChange({self.m_dungeonId, Phase.Normal})
end

DungeonCharTutorialStepHudCtrl._ToggleResetHintNode = HL.Method(HL.Boolean) << function(self, isOn)
    self.view.resetHintNode.gameObject:SetActive(isOn)

    local succ, ctrl = UIManager:IsOpen(PanelId.CommonTaskTrackHud)
    if not succ then
        return
    end

    local rect = ctrl:GetResetBtnFollowerRect()
    ctrl:ToggleResetBtnLoopHint(isOn)
    self.view.resetHintNode.position = rect.position
end

HL.Commit(DungeonCharTutorialStepHudCtrl)
