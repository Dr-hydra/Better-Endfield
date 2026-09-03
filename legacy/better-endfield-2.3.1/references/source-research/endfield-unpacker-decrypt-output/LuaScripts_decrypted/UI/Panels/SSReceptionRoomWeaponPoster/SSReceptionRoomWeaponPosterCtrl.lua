
local SSReceptionRoomPosterCtrl = require_ex('UI/Panels/SSReceptionRoomPoster/SSReceptionRoomPosterCtrl')
local PANEL_ID = PanelId.SSReceptionRoomWeaponPoster
local PHASE_ID = PhaseId.SSReceptionRoomWeaponPoster


SSReceptionRoomWeaponPosterCtrl = HL.Class('SSReceptionRoomWeaponPosterCtrl', SSReceptionRoomPosterCtrl.SSReceptionRoomPosterCtrl)

SSReceptionRoomWeaponPosterCtrl.OpenReceptionRoomPosterPanel = HL.StaticMethod(HL.Opt(HL.Any)) << function(arg)
    local weaponType
    if arg then
        weaponType = unpack(arg)
    end
    PhaseManager:OpenPhase(PHASE_ID, weaponType)
end

HL.Commit(SSReceptionRoomWeaponPosterCtrl)
