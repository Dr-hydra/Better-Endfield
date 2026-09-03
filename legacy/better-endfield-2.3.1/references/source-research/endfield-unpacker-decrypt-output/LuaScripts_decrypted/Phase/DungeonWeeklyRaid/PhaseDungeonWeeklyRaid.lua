
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.DungeonWeeklyRaid
PhaseDungeonWeeklyRaid = HL.Class('PhaseDungeonWeeklyRaid', phaseBase.PhaseBase)

PhaseDungeonWeeklyRaid.m_panelStack = HL.Field(HL.Forward("Stack"))





PhaseDungeonWeeklyRaid.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_INTERACT_WEEK_RAID_ENTRY] = { "_OnInteractWeekRaidEntry", false },
    [MessageConst.ON_WEEKLY_RAID_ENTER] = { "_OnWeekRaidGameEntry", false },
    [MessageConst.ON_WEEKLY_RAID_QUIT] = { "_OnWeekRaidGameQuit", false },
}

PhaseDungeonWeeklyRaid._OnInteractWeekRaidEntry = HL.StaticMethod() << function()
    PhaseManager:OpenPhase(PhaseId.CharFormation, { weekRaidArg = {} })
end

PhaseDungeonWeeklyRaid._OnWeekRaidGameEntry = HL.StaticMethod(HL.Any) << function(arg)
    UIManager:ToggleBlockObtainWaysJump("WeeklyRaidGame", true, {})
end

PhaseDungeonWeeklyRaid._OnWeekRaidGameQuit = HL.StaticMethod() << function()
    UIManager:ToggleBlockObtainWaysJump("WeeklyRaidGame", false)
end


PhaseDungeonWeeklyRaid._OnInit = HL.Override() << function(self)
    self.m_panelStack = require_ex("Common/Utils/DataStructure/Stack")()
    PhaseDungeonWeeklyRaid.Super._OnInit(self)
end




PhaseDungeonWeeklyRaid.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseDungeonWeeklyRaid._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    local topPanelId = PanelId.RaidMain
    local needRecoverPanelStack = false
    if self.arg then
        if self.arg.strPanelId then
            topPanelId = PanelId[self.arg.strPanelId]
        elseif self.arg.panelId then
            topPanelId = self.arg.panelId
        end
        needRecoverPanelStack = self.arg.needRecoverPanelStack == true
    end

    if needRecoverPanelStack then
        self:TryOpenPanel(PanelId.RaidMain, self.arg)
    else

        self:TryOpenPanel(topPanelId, self.arg)
    end
    if needRecoverPanelStack and topPanelId ~= PanelId.RaidMain then
        self:TryOpenPanel(topPanelId, self.arg)
    end
    self:_TryRecoverRaidTechPopup()
    self:_TryRecoverCommonIntro()
    self:_TryRecoverInstructionBook()
end

PhaseDungeonWeeklyRaid._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseDungeonWeeklyRaid._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseDungeonWeeklyRaid._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end



PhaseDungeonWeeklyRaid.TryOpenPanel = HL.Method(HL.Number, HL.Opt(HL.Table)) << function(self, panelId, args)
    if self.m_panelStack:Contains(panelId) then
        logger.warning("PhaseDungeonWeeklyRaid.TryOpenPanel: 面板已打开，无需重复打开，panelId: " .. tostring(panelId))
        return
    end
    self:CreateOrShowPhasePanelItem(panelId, args)
    self.m_panelStack:Push(panelId)
end

PhaseDungeonWeeklyRaid.TryCloseTopPanel = HL.Method() << function(self)
    if self.m_panelStack:Count() == 1 then
        PhaseManager:PopPhase(PHASE_ID)
        return
    end

    local panelId = self.m_panelStack:Pop()
    local phaseItem = self:_GetPanelPhaseItem(panelId)
    if phaseItem then
        phaseItem.uiCtrl:PlayAnimationOutWithCallback(function()
            self:RemovePhasePanelItem(phaseItem)
        end)
    end
end

PhaseDungeonWeeklyRaid.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or {}
    local panelId = self.m_panelStack and self.m_panelStack:Peek() or nil
    if panelId == nil then
        return arg
    end

    arg.panelId = panelId
    arg.strPanelId = nil
    arg.recoverState = nil
    arg.raidTechPopupOpen = nil
    arg.commonIntroState = nil

    if self.m_panel2Item[PanelId.RaidMain] ~= nil then
        arg.needRecoverPanelStack = true
    end

    if panelId == PanelId.DungeonWeeklyRaid then
        local phaseItem = self:_GetPanelPhaseItem(panelId)
        if phaseItem and phaseItem.uiCtrl then
            arg.recoverState = phaseItem.uiCtrl:GetRecoverStateArg()
        end

        local isOpen, raidTechPopupCtrl = UIManager:IsOpen(PanelId.RaidTechPopup)
        if isOpen and raidTechPopupCtrl:IsShow() and PhaseManager:GetTopPhaseId() == PHASE_ID then
            arg.raidTechPopupOpen = true
        end
    end

    if panelId == PanelId.RaidMain then
        local isOpen, introCtrl = UIManager:IsOpen(PanelId.CommonIntro)
        if isOpen and introCtrl:IsShow() and PhaseManager:GetTopPhaseId() == PHASE_ID then
            local introState = introCtrl:GetRecoverStateArg()
            if introState ~= nil and introState.introId == "week_raid" then
                arg.commonIntroState = introState
            end
        end
    end

    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and instructionCtrl:IsShow() and instructionCtrl.id == "week_raid_battle_pass" and PhaseManager:GetTopPhaseId() == PHASE_ID then
        arg.instructionBookArg = "week_raid_battle_pass"
    else
        arg.instructionBookArg = nil
    end
    return arg
end

PhaseDungeonWeeklyRaid._TryRecoverRaidTechPopup = HL.Method() << function(self)
    if self.arg == nil or self.arg.raidTechPopupOpen ~= true then
        return
    end
    if self.m_panelStack == nil or self.m_panelStack:Peek() ~= PanelId.DungeonWeeklyRaid then
        return
    end
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end

    local isOpen, raidTechPopupCtrl = UIManager:IsOpen(PanelId.RaidTechPopup)
    if isOpen and raidTechPopupCtrl:IsShow() then
        return
    end

    UIManager:Open(PanelId.RaidTechPopup)
    self.arg.raidTechPopupOpen = nil
end

PhaseDungeonWeeklyRaid._TryRecoverCommonIntro = HL.Method() << function(self)
    if self.arg == nil or self.arg.commonIntroState == nil then
        return
    end
    if self.m_panelStack == nil or self.m_panelStack:Peek() ~= PanelId.RaidMain then
        return
    end
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end

    local isOpen = UIManager:IsOpen(PanelId.CommonIntro)
    if isOpen then
        return
    end

    UIManager:Open(PanelId.CommonIntro, self.arg.commonIntroState)
    self.arg.commonIntroState = nil
end

PhaseDungeonWeeklyRaid._TryRecoverInstructionBook = HL.Method() << function(self)
    if self.arg == nil or self.arg.instructionBookArg == nil then
        return
    end
    if self.m_panelStack == nil or self.m_panelStack:Peek() ~= PanelId.RaidReward then
        return
    end
    if PhaseManager:GetTopPhaseId() ~= PHASE_ID then
        return
    end

    local isOpen, instructionCtrl = UIManager:IsOpen(PanelId.InstructionBook)
    if isOpen and instructionCtrl:IsShow() then
        return
    end

    UIManager:Open(PanelId.InstructionBook, self.arg.instructionBookArg)
    self.arg.instructionBookArg = nil
end



PhaseDungeonWeeklyRaid._OnActivated = HL.Override() << function(self)
end

PhaseDungeonWeeklyRaid._OnDeActivated = HL.Override() << function(self)
end

PhaseDungeonWeeklyRaid._OnDestroy = HL.Override() << function(self)
    PhaseDungeonWeeklyRaid.Super._OnDestroy(self)
end




HL.Commit(PhaseDungeonWeeklyRaid)

