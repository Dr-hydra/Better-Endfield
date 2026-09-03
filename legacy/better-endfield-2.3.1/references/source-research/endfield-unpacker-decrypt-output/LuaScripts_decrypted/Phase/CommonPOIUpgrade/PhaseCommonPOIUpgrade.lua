
local phaseBase = require_ex('Phase/Core/PhaseBase')
local PHASE_ID = PhaseId.CommonPOIUpgrade
PhaseCommonPOIUpgrade = HL.Class('PhaseCommonPOIUpgrade', phaseBase.PhaseBase)

PhaseCommonPOIUpgrade.m_domainPOIType = HL.Field(GEnums.DomainPoiType)

PhaseCommonPOIUpgrade.m_instId = HL.Field(HL.String) << ""

PhaseCommonPOIUpgrade.m_blendCamCfg = HL.Field(CS.Beyond.Gameplay.RelativeCameraBlendConfig)

PhaseCommonPOIUpgrade.m_upgradeTag = HL.Field(HL.Boolean) << false





PhaseCommonPOIUpgrade.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.OPEN_PHASE_COMMON_POI] = { 'OpenPhaseCommonPOI', false },

    [MessageConst.ON_COMMON_POI_UNLOCKED] = {'OnCommonPOIUnlocked', true},
    [MessageConst.ON_COMMON_POI_LEVEL_UP] = {'OnCommonPOILevelUp', true},
}



PhaseCommonPOIUpgrade._OnInit = HL.Override() << function(self)
    PhaseCommonPOIUpgrade.Super._OnInit(self)
end




PhaseCommonPOIUpgrade.PrepareTransition = HL.Override(HL.Number, HL.Boolean, HL.Opt(HL.Number)) << function(self, transitionType, fastMode, anotherPhaseId)
end

PhaseCommonPOIUpgrade._DoPhaseTransitionIn = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    local domainPOIType, instId, blendCamCfg = unpack(self.arg)
    if type(domainPOIType) == "number" or type(domainPOIType) == "string" then
        domainPOIType = GEnums.DomainPoiType.__CastFrom(domainPOIType)
    end

    if blendCamCfg then
        GameAction.BlendToRelativeCamera(blendCamCfg)
    end

    self.m_domainPOIType = domainPOIType
    self.m_instId = instId
    self.m_blendCamCfg = blendCamCfg
end

PhaseCommonPOIUpgrade._DoPhaseTransitionOut = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
    if not self.m_blendCamCfg then
        return
    end



    if self.m_upgradeTag then
        return
    end

    local hasCameraCfg, cameraCfg = DataManager.relativeCameraConfigs:TryGetValue(self.m_blendCamCfg.configId)
    local blendOutDuration = 0.5
    if hasCameraCfg then

        blendOutDuration = cameraCfg.tweenTime
    end

    CS.Beyond.Gameplay.View.CameraUtils.DoCommonTempBlendOut(GameWorld.levelLoader.isLoading and -1 or blendOutDuration)
end

PhaseCommonPOIUpgrade._DoPhaseTransitionBehind = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end

PhaseCommonPOIUpgrade._DoPhaseTransitionBackToTop = HL.Override(HL.Boolean, HL.Opt(HL.Table)) << function(self, fastMode, args)
end






PhaseCommonPOIUpgrade._OnActivated = HL.Override() << function(self)
end

PhaseCommonPOIUpgrade._OnDeActivated = HL.Override() << function(self)
end

PhaseCommonPOIUpgrade._OnDestroy = HL.Override() << function(self)
    PhaseCommonPOIUpgrade.Super._OnDestroy(self)
end



PhaseCommonPOIUpgrade.OpenPhaseCommonPOI = HL.StaticMethod(HL.Table) << function(arg)
    PhaseManager:OpenPhase(PHASE_ID, arg)
end


PhaseCommonPOIUpgrade.OnCommonPOIUnlocked = HL.Method(HL.Opt(HL.Table)) << function(self, arg)

    self.m_upgradeTag = true
    PhaseManager:ExitPhaseFast(PHASE_ID)
end

PhaseCommonPOIUpgrade.OnCommonPOILevelUp = HL.Method(HL.Opt(HL.Table)) << function(self, arg)

    self.m_upgradeTag = true
    PhaseManager:ExitPhaseFast(PHASE_ID)
end

HL.Commit(PhaseCommonPOIUpgrade)

