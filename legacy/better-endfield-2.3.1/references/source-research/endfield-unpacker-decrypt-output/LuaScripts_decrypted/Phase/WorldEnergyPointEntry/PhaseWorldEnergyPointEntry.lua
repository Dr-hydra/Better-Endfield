
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.WorldEnergyPointEntry
PhaseWorldEnergyPointEntry = HL.Class('PhaseWorldEnergyPointEntry', phaseBase.PhaseBase)


PhaseWorldEnergyPointEntry.m_gameGroupId = HL.Field(HL.String) << ""

PhaseWorldEnergyPointEntry.m_blendCamCfg = HL.Field(CS.Beyond.Gameplay.RelativeCameraBlendConfig)





PhaseWorldEnergyPointEntry.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.OPEN_WORLD_ENERGY_POINT_ENTRY] = { 'OpenWorldEnergyPointEntry', false },
}

PhaseWorldEnergyPointEntry.OpenWorldEnergyPointEntry = HL.StaticMethod(HL.Table) << function(args)
    PhaseManager:OpenPhase(PHASE_ID, args)
end


PhaseWorldEnergyPointEntry._OnInit = HL.Override() << function(self)
    PhaseWorldEnergyPointEntry.Super._OnInit(self)
end




PhaseWorldEnergyPointEntry.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseWorldEnergyPointEntry._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    local gameGroupId, blendCamCfg = unpack(self.arg)

    if blendCamCfg then
        GameAction.BlendToRelativeCamera(blendCamCfg)
    end

    self.m_gameGroupId = gameGroupId
    self.m_blendCamCfg = blendCamCfg
end

PhaseWorldEnergyPointEntry._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if not self.m_blendCamCfg then
        return
    end

    local hasCameraCfg, cameraCfg = DataManager.relativeCameraConfigs:TryGetValue(self.m_blendCamCfg.configId)
    local blendOutDuration = 0.5
    if hasCameraCfg then

        blendOutDuration = cameraCfg.tweenTime
    end

    CS.Beyond.Gameplay.View.CameraUtils.DoCommonTempBlendOut(blendOutDuration)
end

PhaseWorldEnergyPointEntry._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseWorldEnergyPointEntry._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseWorldEnergyPointEntry.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local arg = self.arg and lume.deepCopy(self.arg) or nil
    if arg == nil then
        arg = {}
    end

    local panelItem = self.m_panel2Item and self.m_panel2Item[PanelId.WorldEnergyPointEntry]
    local uiCtrl = panelItem and panelItem.uiCtrl
    if uiCtrl and HL.TryGet(uiCtrl, "GetRecoverPopupStateArg") and PhaseManager:GetTopPhaseId() == PHASE_ID then

        arg.popupState = uiCtrl:GetRecoverPopupStateArg()
    else
        arg.popupState = nil
    end
    return arg
end






PhaseWorldEnergyPointEntry._OnActivated = HL.Override() << function(self)
end

PhaseWorldEnergyPointEntry._OnDeActivated = HL.Override() << function(self)
end

PhaseWorldEnergyPointEntry._OnDestroy = HL.Override() << function(self)
    PhaseWorldEnergyPointEntry.Super._OnDestroy(self)
end



HL.Commit(PhaseWorldEnergyPointEntry)

