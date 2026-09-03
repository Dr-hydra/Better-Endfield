
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.Cinematic
PhaseCinematic = HL.Class('PhaseCinematic', phaseBase.PhaseBase)





PhaseCinematic.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_PLAY_CUTSCENE] = { 'OnCinematicStart', false},
    [MessageConst.ON_FINISH_CUTSCENE] = { 'OnCinematicEnd', true},
}

PhaseCinematic.OnCinematicStart = HL.StaticMethod(HL.Table) << function (arg)
    arg.fast = true
    if not PhaseManager:IsOpen(PHASE_ID) then
        PhaseCinematic.AutoOpen(PHASE_ID, arg)
    end

    Notify(MessageConst.ON_LOAD_NEW_CUTSCENE, arg)
end

PhaseCinematic.OnCinematicEnd = HL.Method(HL.Opt(HL.Table)) << function (self, args)

    logger.info("OnCutsceneEnd, exit CinematicPhase")
    self:ExitSelfFast()
end





































HL.Commit(PhaseCinematic)
