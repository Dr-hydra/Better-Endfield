local LuaSystemBase = require_ex('LuaSystem/LuaSystemBase')

GachaSystem = HL.Class('GachaSystem', LuaSystemBase.LuaSystemBase)


GachaSystem.m_camCullingEnabled = HL.Field(HL.Boolean) << false

GachaSystem.m_camEnableSettingKeys = HL.Field(HL.Table)

GachaSystem.m_curIVPath = HL.Field(HL.Any)


GachaSystem.GachaSystem = HL.Constructor() << function(self)
    self.m_camEnableSettingKeys = {}
end


GachaSystem.UpdateGachaSettingState = HL.Method() << function(self)
    if self:_IsInNeedGachaCamPhase() then
        self:ToggleGachaCamSetting("CalcByPhase", true)
    else
        self:ToggleGachaCamSetting("CalcByPhase", false)
    end

    if self:_IsInCharGacha() then
        self:_UpdateGachaCharIV(true)
    else
        self:_UpdateGachaCharIV(false)
    end
    self:UpdateGachaMusicState()
    self:_UpdateIsInGacha()
end


GachaSystem.UpdateGachaWeaponSettingState = HL.Method() << function(self)
    if self:_IsInNeedGachaCamPhase() then
        self:ToggleGachaCamSetting("CalcByPhase", true)
    else
        self:ToggleGachaCamSetting("CalcByPhase", false)
    end

    if self:_IsInWeaponGacha() then
        self:_UpdateGachaWeaponIV(true)
    else
        self:_UpdateGachaWeaponIV(false)
    end
    self:UpdateGachaMusicState()
    self:_UpdateIsInGacha()
end


local GachaPhaseIds = {
    PhaseId.GachaPool,
    PhaseId.GachaLauncher,
    PhaseId.GachaDropBin,
    PhaseId.GachaChar,

    PhaseId.GachaWeaponPool,
    PhaseId.GachaWeaponPreheat,
    PhaseId.GachaWeapon,
    PhaseId.GachaWeaponResult,
}

GachaSystem._UpdateIsInGacha = HL.Method() << function(self)
    local inGacha = false
    for _, phaseId in pairs(GachaPhaseIds) do
        local isOpen, _ = PhaseManager:IsOpenAndValid(phaseId)
        if isOpen then
            inGacha = true
            break
        end
    end
    GameWorld.worldInfo.inGacha = inGacha
end

local GachaCharPhaseIds = {
    [PhaseId.GachaPool] = true,
    [PhaseId.GachaLauncher] = true,
    [PhaseId.GachaDropBin] = true,
    [PhaseId.GachaChar] = true,
}

local GachaWeaponPhaseIds = {
    [PhaseId.GachaWeaponPool] = true,
    [PhaseId.GachaWeaponPreheat] = true,
    [PhaseId.GachaWeapon] = true,
    [PhaseId.GachaWeaponResult] = true,
}

local NeedGachaCamPhaseIds = {
    [PhaseId.GachaLauncher] = true,
    [PhaseId.GachaDropBin] = true,
    [PhaseId.GachaChar] = true,
    [PhaseId.GachaWeaponPreheat] = true,
    [PhaseId.GachaWeapon] = true,
}

GachaSystem.UpdateGachaMusicState = HL.Method() << function(self)
    local topPhaseId = PhaseManager:GetTopOpenAndValidPhaseId()
    if topPhaseId == PhaseId.GachaDropBin then
        GameInstance.audioManager.music:SetGachaState(true)
        AudioManager.PostEvent(UIConst.GACHA_MUSIC_DROP_BIN)
    elseif GachaCharPhaseIds[topPhaseId] or GachaWeaponPhaseIds[topPhaseId] then
        GameInstance.audioManager.music:SetGachaState(true)
        AudioManager.PostEvent(UIConst.GACHA_MUSIC_UI)
    else

        if PhaseManager:IsOpenAndValid(PhaseId.GachaPool) or
            PhaseManager:IsOpenAndValid(PhaseId.GachaChar) or
            PhaseManager:IsOpenAndValid(PhaseId.GachaWeaponPool) or
            PhaseManager:IsOpenAndValid(PhaseId.GachaWeaponResult) or
            PhaseManager:IsOpenAndValid(PhaseId.GachaLauncher)
        then
            GameInstance.audioManager.music:SetGachaState(true)
            AudioManager.PostEvent(UIConst.GACHA_MUSIC_UI)
        else
            GameInstance.audioManager.music:SetGachaState(false)
        end
    end
end

GachaSystem._IsInCharGacha = HL.Method(HL.Opt(HL.Number)).Return(HL.Boolean) << function(self, topPhaseId)
    topPhaseId = topPhaseId or PhaseManager:GetTopOpenAndValidPhaseId()
    return GachaCharPhaseIds[topPhaseId] == true
end

GachaSystem._IsInWeaponGacha = HL.Method(HL.Opt(HL.Number)).Return(HL.Boolean) << function(self, topPhaseId)
    topPhaseId = topPhaseId or PhaseManager:GetTopOpenAndValidPhaseId()
    return GachaWeaponPhaseIds[topPhaseId] == true
end

GachaSystem._IsInNeedGachaCamPhase = HL.Method().Return(HL.Boolean) << function(self)
    local topPhaseId = PhaseManager:GetTopOpenAndValidPhaseId()
    return NeedGachaCamPhaseIds[topPhaseId] == true
end

GachaSystem._UpdateGachaCharIV = HL.Method(HL.Boolean) << function(self, active)
    if active == (self.m_curIVPath ~= nil) then
        return
    end
    local ivPath
    if active then
        local platformPathName =  CS.Beyond.Resource.PathConsts.GetCurrentDataSceneAssetFolderName()
        ivPath = "Data/IrradianceVolume/" .. platformPathName .. "/gacha/character"
    end
    self:SetGachaIV(ivPath)
end

GachaSystem._UpdateGachaWeaponIV = HL.Method(HL.Boolean) << function(self, active)
    if active == (self.m_curIVPath ~= nil) then
        return
    end
    local ivPath
    if active then
        local platformPathName =  CS.Beyond.Resource.PathConsts.GetCurrentDataSceneAssetFolderName()
        ivPath = "Data/IrradianceVolume/" .. platformPathName .. "/gacha/weapon"
    end
    self:SetGachaIV(ivPath)
end

GachaSystem.SetGachaIV = HL.Method(HL.Opt(HL.String)) << function(self, ivPath)
    if self.m_curIVPath == ivPath then
        return
    end
    local oldPath = self.m_curIVPath
    self.m_curIVPath = ivPath
    if oldPath ~= nil then
        CS.HG.Rendering.Runtime.HGManagerContext.currentManagerContext.ivManager:DestroyGachaIV()
        logger.info("GachaSystem.SetGachaIV DestroyGachaIV")
    end
    if ivPath then
        CS.HG.Rendering.Runtime.HGManagerContext.currentManagerContext.ivManager:CreateGachaIV(ivPath)
        logger.info("GachaSystem.SetGachaIV", ivPath)
    end
end

GachaSystem.ToggleGachaCamSetting = HL.Method(HL.String, HL.Boolean) << function(self, key, active)
    logger.info("ToggleGachaCamSetting", key, active)
    if active then
        self.m_camEnableSettingKeys[key] = true
    else
        self.m_camEnableSettingKeys[key] = nil
    end
    self:_UpdateGachaCamSettingEnabled()
end

GachaSystem._UpdateGachaCamSettingEnabled = HL.Method() << function(self)
    local active = next(self.m_camEnableSettingKeys) ~= nil
    if active == self.m_camCullingEnabled then
        return
    end

    CSFactoryUtil.SetBuildingNameInvalid(active)
    if active then
        self.m_camCullingEnabled = true
        CameraManager:EnableGachaCullingMask(true, "Gacha")
    else
        self.m_camCullingEnabled = false
        CameraManager:EnableGachaCullingMask(false, "Gacha")
    end
end


GachaSystem.PreloadDropBin = HL.Method() << function(self)



end

GachaSystem.GetDropBin = HL.Method() << function(self)
end

GachaSystem.DesDropBin = HL.Method() << function(self)
end

GachaSystem.OnRelease = HL.Override() << function(self)

    self:SetGachaIV()
    self.m_camEnableSettingKeys = {}
    self:_UpdateGachaCamSettingEnabled()
end

HL.Commit(GachaSystem)
return GachaSystem
