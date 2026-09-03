
local rewardsPopUpForSystemCtrl = require_ex('UI/Panels/RewardsPopUpForSystem/RewardsPopUpForSystemCtrl')
local PANEL_ID = PanelId.RewardsPopUpForCraft
RewardsPopUpForCraftCtrl = HL.Class('RewardsPopUpForCraftCtrl', rewardsPopUpForSystemCtrl.RewardsPopUpForSystemCtrl)







RewardsPopUpForCraftCtrl.s_overrideMessages = HL.StaticField(HL.Table) << {
}

RewardsPopUpForCraftCtrl.ShowCraftRewards = HL.StaticMethod(HL.Table) << function(args)
    local ctrl = UIManager:AutoOpen(PANEL_ID, nil, false)
    UIManager:SetTopOrder(PANEL_ID)
    ctrl:_ShowRewards(args)
end

HL.Commit(RewardsPopUpForCraftCtrl)
