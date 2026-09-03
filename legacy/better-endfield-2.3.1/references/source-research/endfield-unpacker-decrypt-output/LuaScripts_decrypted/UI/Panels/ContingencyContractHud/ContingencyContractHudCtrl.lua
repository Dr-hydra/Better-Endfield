
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ContingencyContractHud

local STAGE_COUNT = 4
local STAGE_SPRITE_PREFIX = "icon_contract_text_0"
local STAGE_CELL_STATE = {
    Normal = "Normal",
    Current = "Current",
    Complete = "Complete",
}


local COMMON_TASK_END_TOAST_TYPE = "TrackEndToast"

ContingencyContractHudCtrl = HL.Class('ContingencyContractHudCtrl', uiCtrl.UICtrl)

ContingencyContractHudCtrl.m_stageCellCache = HL.Field(HL.Forward("UIListCache"))

ContingencyContractHudCtrl.m_switchStageTimerTickId = HL.Field(HL.Number) << -1

ContingencyContractHudCtrl.m_countDownAudioPlayingId = HL.Field(HL.Number) << -1





ContingencyContractHudCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_SUB_GAME_STAGE_FINISH] = "OnSubGameStageFinish",
    [MessageConst.ON_SUB_GAME_STAGE_CHANGE] = "OnSubGameStageChange",
    [MessageConst.ON_SUB_GAME_FINISH_STATE_CHANGE] = "OnSubGameFinishStateChange",

    [MessageConst.CONTINGENCY_CONTRACT_START_SWITCH_STAGE_TIMER] = "StartSwitchStageTimer",

    [MessageConst.ON_HUD_BTN_VISIBLE_CHANGE] = "OnHudBtnVisibleChange",
    [MessageConst.ON_ONE_COMMON_TASK_PANEL_START] = "OnEndToastStart",
}

ContingencyContractHudCtrl.TryCreateHud = HL.StaticMethod() << function()
    local dungeonId = GameInstance.dungeonManager.curDungeonId
    if not DungeonUtils.isDungeonContract(dungeonId) then
        return
    end


    local ctrl = ContingencyContractHudCtrl.AutoOpen(PANEL_ID)


    AudioManager.SetRtpc("au_music_rtpc_cc_v1d3_ingame_current_stars", ContingencyContractUtils.GetTotalScoreByGamaId(dungeonId));
end

ContingencyContractHudCtrl.OnDungeonGameInit = HL.StaticMethod(HL.Table) << function(args)
    ContingencyContractHudCtrl.TryCreateHud()
end

ContingencyContractHudCtrl.OnSubGameReset = HL.StaticMethod() << function()
    ContingencyContractHudCtrl.TryCreateHud()
end

ContingencyContractHudCtrl.OnOpenSubGameTrackings = HL.StaticMethod(HL.Table) << function(args)
    local dungeonId = unpack(args)
    if not DungeonUtils.isDungeonContract(dungeonId) then
        return
    end


    local ctrl = ContingencyContractHudCtrl.AutoOpen(PANEL_ID)
    ctrl:OnSubGameStageChange()
end

ContingencyContractHudCtrl.OnCloseSubGameTrackings = HL.StaticMethod() << function()
    local succ, ctrl = UIManager:IsOpen(PANEL_ID)
    if succ then



        ctrl:Close()
    end
end


ContingencyContractHudCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    self:_RefreshView()
    self:_ToggleTopMainHud(true)
end





ContingencyContractHudCtrl.OnClose = HL.Override() << function(self)
    self:_TryClearSwitchTimer()
    AudioAdapter.StopByPlayingId(self.m_countDownAudioPlayingId)
end

ContingencyContractHudCtrl.OnSubGameStageFinish = HL.Method() << function(self)
    if not GameInstance.dungeonManager.curDungeonLikeSubGame then
        return
    end

    local curStage = GameInstance.dungeonManager.curDungeonLikeSubGame.stage
    local finishStageIndex = curStage - 1
    local stageCell = self.m_stageCellCache:Get(finishStageIndex)
    stageCell.stateController:SetState(STAGE_CELL_STATE.Complete)
    stageCell.animationWrapper:Play("contingencycontract_stage_done")
    AudioAdapter.PostEvent("Au_UI_Mission_CCReigniteState_Completed")
end

ContingencyContractHudCtrl.OnSubGameStageChange = HL.Method() << function(self)
    self:_TryClearSwitchTimer()

    if not GameInstance.dungeonManager.curDungeonLikeSubGame then
        return
    end

    local curStage = GameInstance.dungeonManager.curDungeonLikeSubGame.stage
    local stageCell = self.m_stageCellCache:Get(curStage)
    stageCell.stateController:SetState(STAGE_CELL_STATE.Current)
    stageCell.animationWrapper:Play("contingencycontract_stage_slc")
    AudioAdapter.PostEvent("Au_UI_Mission_CCReigniteState_start")

    self.view.titleAdaptNode:PlayOutAnimation(function()
        self.view.titleAdaptNode.gameObject:SetActive(false)
    end)
end

ContingencyContractHudCtrl.OnSubGameFinishStateChange = HL.Method(HL.Table) << function(self, args)
    local gameId, phase = unpack(args)
end

ContingencyContractHudCtrl.StartSwitchStageTimer = HL.Method(HL.Table) << function(self, args)
    LuaSystemManager.commonTaskTrackSystem:AddRequest("ContingencyContractHudTimer", function()
        self.view.titleAdaptNode.gameObject:SetActive(true)
        AudioAdapter.PostEvent("Au_UI_Event_CCReignite_CountDownAppear")
    end, function()
        self.view.titleAdaptNode.gameObject:SetActive(true)
        AudioAdapter.PostEvent("Au_UI_Event_CCReignite_CountDownAppear")
    end)

    local _, durationMs = unpack(args)
    self.m_countDownAudioPlayingId = AudioAdapter.PostEvent("Au_UI_Event_CCReignite_CountDown")
    local leftMs = durationMs
    local prevSec = math.ceil(durationMs / 1000)
    self.view.countdownTxt.text = prevSec
    self.m_switchStageTimerTickId = LuaUpdate:Remove(self.m_switchStageTimerTickId)
    self.m_switchStageTimerTickId = LuaUpdate:Add("Tick", function(deltaTime)

        if TimeManagerInst.timeScale == 0 then
            return
        end
        leftMs = leftMs - deltaTime * 1000
        leftMs = lume.clamp(leftMs, 0, durationMs)
        local curSec = math.ceil(leftMs / 1000)
        if curSec ~= prevSec and not self.m_isClosed then
            self.view.titleAdaptNode:Play("contingencycontract_numb_change")
            prevSec = curSec
        end
        self.view.countdownTxt.text = curSec
    end)
end

ContingencyContractHudCtrl.OnHudBtnVisibleChange = HL.Method(HL.Table) << function(self, args)
end

ContingencyContractHudCtrl.OnEndToastStart = HL.Method(HL.String) << function(self, type)
    if type ~= COMMON_TASK_END_TOAST_TYPE then
        return
    end
    self:_ToggleTopMainHud(false)
    self:Close()
end

ContingencyContractHudCtrl._InitUI = HL.Method() << function(self)
    self.view.btnReset.onClick:AddListener(function()
        self:_OnClickResetBtn()
    end)

    self.m_stageCellCache = UIUtils.genCellCache(self.view.contingencyContractStageCell)
end

ContingencyContractHudCtrl._RefreshView = HL.Method() << function(self)
    self.view.stageAdaptNode.gameObject:SetActive(true)
    self.view.titleAdaptNode.gameObject:SetActive(false)

    local maxStage = GameInstance.dungeonManager.curDungeonLikeSubGame.maxStage
    self.m_stageCellCache:Refresh(maxStage, function(cell, luaIndex)
        self:_InitCellCache(cell, luaIndex)
    end)
end

ContingencyContractHudCtrl._OnClickResetBtn = HL.Method() << function(self)
    if not string.isEmpty(GameInstance.player.systemActionConflictManager.curProcessingSystemAction) then
        logger.warn("ContingencyContractHudCtrl._OnClickResetBtn systemConflict:", GameInstance.player.systemActionConflictManager:GetCurProcessingSystemActionInfo(), "SubGameId:", self.m_subGameId)
        return
    end


    if LuaSystemManager.commonTaskTrackSystem:HasRequest() then
        return
    end

    if not GameInstance.dungeonManager.curDungeonLikeSubGame then
        logger.error("ContingencyContractHudCtrl._OnClickResetBtn subGame is nil")
        return
    end

    local dungeonCfg = Tables.dungeonTable[GameInstance.dungeonManager.curDungeonId]
    local dungeonTypeCfg = Tables.dungeonTypeTable[dungeonCfg.dungeonCategory]
    local curStatus = GameInstance.dungeonManager.curDungeonLikeSubGame:GetCurrentCompletionStatus()
    self:Notify(MessageConst.SHOW_POP_UP, {
        content = dungeonTypeCfg.resetConfirmText,
        onConfirm = function()
            GameInstance.dungeonManager.curDungeonLikeSubGame:UserSendReStartAtStatus(curStatus)
        end,
        freezeWorld = true,
        pauseGame = true,
        interrupt = {
            interruptMessage = { MessageConst.ON_SUB_GAME_QUIT,
                                 MessageConst.ON_CONTINGENCY_CONTRACT_SETTLEMENT,
                                 MessageConst.SHOW_CONTINGENCY_CONTRACT_SETTLEMENT,
                                 MessageConst.ON_DUNGEON_COMPLETE,
                                 MessageConst.SHOW_DEATH_INFO },
        }
    })
end

ContingencyContractHudCtrl._InitCellCache = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    cell.textImg:LoadSprite(UIConst.UI_SPRITE_CONTINGENCY_CONTRACT_HUD, STAGE_SPRITE_PREFIX..luaIndex)
    cell.stateController:SetState(STAGE_CELL_STATE.Normal)
    cell.animationWrapper:SampleClipAtPercent("contingencycontract_stage_slc", 0)
    cell.animationWrapper:SampleClipAtPercent("contingencycontract_stage_done", 0)
end

ContingencyContractHudCtrl._TryClearSwitchTimer = HL.Method() << function(self)
    if self.m_switchStageTimerTickId > 0 then
        self.m_switchStageTimerTickId = LuaUpdate:Remove(self.m_switchStageTimerTickId)
    end
end

ContingencyContractHudCtrl._ToggleTopMainHud = HL.Method(HL.Boolean) << function(self, isOn)
    GameInstance.player.forbidSystem:SetForbid(ForbidType.ForbidMainHudTopBtns, "ContingencyContractSettlement", not isOn)
end

HL.Commit(ContingencyContractHudCtrl)
