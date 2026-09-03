local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

MiasmaIndicator = HL.Class('MiasmaIndicator', UIWidgetBase)

local LEVEL_TO_STATE_NAME = {
    [1] = 'Low',
    [2] = 'Medium',
    [3] = 'High',
}

MiasmaIndicator.m_toleranceUpdateKey = HL.Field(HL.Number) << -1

MiasmaIndicator.m_maxTolerance = HL.Field(HL.Number) << -1

MiasmaIndicator.m_curTolerance = HL.Field(HL.Number) << -1

MiasmaIndicator.m_preTolerance = HL.Field(HL.Number) << -1

MiasmaIndicator.m_lastEnv = HL.Field(HL.Any)

MiasmaIndicator.m_considerEnv = HL.Field(HL.Boolean) << false


MiasmaIndicator._OnFirstTimeInit = HL.Override() << function(self)
    self:RegisterMessage(MessageConst.ON_BLIGHT_MIASMA_LEVEL_CHANGED, function()
        self:_RefreshMiasmaLevel()
    end, true)
    self:RegisterMessage(MessageConst.ON_BLIGHT_MIASMA_AREA_ENTER, function()
        local disableMiasma = GameInstance.player.forbidSystem:IsForbidden(ForbidType.HideMiasmaIndicator)
        if not disableMiasma then
            self.gameObject:SetActive(true)
        end
    end, true)
    self:RegisterMessage(MessageConst.ON_BLIGHT_MIASMA_AREA_EXIT, function()
        local disableMiasma = GameInstance.player.forbidSystem:IsForbidden(ForbidType.HideMiasmaIndicator)
        if not disableMiasma then
            self.gameObject:SetActive(false)
        end
    end, true)
end

MiasmaIndicator.InitMiasmaIndicator = HL.Method(HL.Opt(HL.Boolean)) << function(self, considerEnv)
    self.m_considerEnv = considerEnv == true
    self:_FirstTimeInit()
    local disableMiasma = GameInstance.player.forbidSystem:IsForbidden(ForbidType.HideMiasmaIndicator)
    local inBlightMiasmaArea = GameWorld.gameMechManager.blightMiasmaBrain.inBlightMiasmaArea
    local show = not disableMiasma and inBlightMiasmaArea
    self.gameObject:SetActive(show)
end

MiasmaIndicator._OnEnable = HL.Override() << function(self)
    self:_RefreshAll()
    self.m_toleranceUpdateKey = LuaUpdate:Add('LateTick', function(deltaTime)
        self:_ToleranceTick()
    end)
end

MiasmaIndicator._OnDisable = HL.Override() << function(self)
    self:_RemoveTick()
end

MiasmaIndicator._OnDestroy = HL.Override() << function(self)
    self:_RemoveTick()
end

MiasmaIndicator._RefreshAll = HL.Method() << function(self)
    self:_RefreshMiasmaLevel()
    self:_RefreshTolerance()
end

MiasmaIndicator._RefreshMiasmaLevel = HL.Method() << function(self)
    local level = GameWorld.gameMechManager.blightMiasmaBrain.currentLevel
    local showLevel = level > 0
    self.view.levelNode.gameObject:SetActive(showLevel)
    if not showLevel then
        return
    end
    local stateName = LEVEL_TO_STATE_NAME[level]
    if stateName == nil then
        logger.error('获取瘴气等级对应的状态失败 ', level, LEVEL_TO_STATE_NAME)
        stateName = LEVEL_TO_STATE_NAME[1]
    end
    self.view.stateController:SetState(stateName)
end

MiasmaIndicator._RefreshTolerance = HL.Method() << function(self)
    local blightMiasmaBrain = GameWorld.gameMechManager.blightMiasmaBrain
    self.m_maxTolerance = blightMiasmaBrain.maxTolerance
    self.m_curTolerance = blightMiasmaBrain.currentTolerance
    self.m_preTolerance = blightMiasmaBrain.preTolerance
    self.view.numberTxt.text = string.format('%d/%d', self.m_curTolerance, self.m_maxTolerance)
    local curRate = 1
    if self.m_maxTolerance > 0 then
        curRate = self.m_curTolerance / self.m_maxTolerance
    end
    local stateName
    local audioEventName
    if curRate >= self.config.NORMAL_FLOOR then
        stateName = 'Normal'
    elseif curRate >= self.config.WARNING_FLOOR then
        stateName = 'Warning'
        audioEventName = 'Au_UI_Event_XiranitenexusWarn_Yellow'
    else
        stateName = 'Danger'
        audioEventName = 'Au_UI_Event_XiranitenexusWarn_Red'
    end
    if self.view.stateController.currentStateName ~= stateName and audioEventName then
        AudioAdapter.PostEvent(audioEventName)
    end
    self.view.stateController:SetState(stateName)
    if self.m_preTolerance == 0 then
        self.view.pre1Img.fillAmount = 0
        self.view.pre2Img.fillAmount = 0
        self.view.sliderImg.fillAmount = curRate
    else
        local preRate = 1
        if self.m_maxTolerance > 0 then
            preRate = (self.m_preTolerance + self.m_curTolerance) / self.m_maxTolerance
        end
        self.view.pre1Img.fillAmount = curRate
        self.view.pre2Img.fillAmount = curRate
        self.view.sliderImg.fillAmount = preRate
    end
end

MiasmaIndicator._ToleranceTick = HL.Method() << function(self)
    local blightMiasmaBrain = GameWorld.gameMechManager.blightMiasmaBrain
    if blightMiasmaBrain.maxTolerance ~= self.m_maxTolerance or
        blightMiasmaBrain.currentTolerance ~= self.m_curTolerance or
        blightMiasmaBrain.preTolerance ~= self.m_preTolerance then
        if blightMiasmaBrain.currentTolerance == blightMiasmaBrain.maxTolerance
            and self.m_curTolerance < blightMiasmaBrain.maxTolerance then
            self.view.animationWrapper:PlayInAnimation()
        end
        self:_RefreshTolerance()
    end
    if not self.m_considerEnv then
        return
    end
    local env = GameInstance.remoteFactoryManager.playerCurrentGridInfoProvider:GetEnvInfo()
    if env ~= self.m_lastEnv then
        self.m_lastEnv = env
        local hasEnv = env ~= GEnums.FacEnvGenEnvType.None:GetHashCode()
        self.view.stateController:SetState(hasEnv and 'WithEnv' or 'WithoutEnv')
    end
end

MiasmaIndicator._RemoveTick = HL.Method() << function(self)
    if self.m_toleranceUpdateKey > 0 then
        LuaUpdate:Remove(self.m_toleranceUpdateKey)
        self.m_toleranceUpdateKey = -1
    end
end

HL.Commit(MiasmaIndicator)
return MiasmaIndicator

