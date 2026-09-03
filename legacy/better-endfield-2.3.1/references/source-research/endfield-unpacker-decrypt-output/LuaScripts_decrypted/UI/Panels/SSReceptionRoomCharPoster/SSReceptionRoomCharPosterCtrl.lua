
local SSReceptionRoomPosterCtrl = require_ex('UI/Panels/SSReceptionRoomPoster/SSReceptionRoomPosterCtrl')
local PANEL_ID = PanelId.SSReceptionRoomCharPoster
local PHASE_ID = PhaseId.SSReceptionRoomCharPoster

SSReceptionRoomCharPosterCtrl = HL.Class('SSReceptionRoomCharPosterCtrl', SSReceptionRoomPosterCtrl.SSReceptionRoomPosterCtrl)
SSReceptionRoomCharPosterCtrl.OpenReceptionRoomPosterPanel = HL.StaticMethod(HL.Opt(HL.Any)) << function(arg)
    local weaponType
    if arg then
        weaponType = unpack(arg)
    end
    PhaseManager:OpenPhase(PHASE_ID, weaponType)
end

HL.Commit(SSReceptionRoomCharPosterCtrl)
