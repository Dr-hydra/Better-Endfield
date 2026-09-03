local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.EnvironmentalPrompt

EnvironmentalPromptCtrl = HL.Class('EnvironmentalPromptCtrl', uiCtrl.UICtrl)






EnvironmentalPromptCtrl.s_messages = HL.StaticField(HL.Table) << {

}


EnvironmentalPromptCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.miasmaIndicator:InitMiasmaIndicator(true)
    self.view.facEnvironmental:InitFacEnvironmental()
end



EnvironmentalPromptCtrl.ShowMiasmaIndicator = HL.StaticMethod() << function()
    UIManager:AutoOpen(PANEL_ID)
end

EnvironmentalPromptCtrl.ShowFacEnvironmental = HL.StaticMethod(HL.Table) << function(args)
    local env = unpack(args)
    if env ~= GEnums.FacEnvGenEnvType.None:GetHashCode() then
        UIManager:AutoOpen(PANEL_ID)
    end
end

EnvironmentalPromptCtrl.TryShowEnvironmentalPrompt = HL.StaticMethod() << function()
    if GameWorld.gameMechManager == nil then
        return
    end
    local disableMiasma = GameInstance.player.forbidSystem:IsForbidden(ForbidType.HideMiasmaIndicator)
    local inBlightMiasmaArea = GameWorld.gameMechManager.blightMiasmaBrain.inBlightMiasmaArea
    local env = GameInstance.remoteFactoryManager.playerCurrentGridInfoProvider:GetEnvInfo()
    if (not disableMiasma and inBlightMiasmaArea) or env ~= GEnums.FacEnvGenEnvType.None:GetHashCode() then
        UIManager:AutoOpen(PANEL_ID)
    end
end



HL.Commit(EnvironmentalPromptCtrl)
