local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CinematicExUI = HL.Class('CinematicExUI', UIWidgetBase)

CinematicExUI.m_exNodeExistTime = HL.Field(HL.Number) << -1

CinematicExUI.m_pause = HL.Field(HL.Boolean) << false

CinematicExUI.m_tickKey = HL.Field(HL.Number) << -1

CinematicExUI.m_exNodeTween = HL.Field(HL.Any)


CinematicExUI._OnFirstTimeInit = HL.Override() << function(self)
    self.view.touch.onClick:RemoveAllListeners()
    self.view.touch.onClick:AddListener(function()
        if self.m_exNodeExistTime < 0 then
            self:_TrySwitchExNode(true)
        else
            self:RefreshExistTime(Tables.cinematicConst.cinematicUIShowTime + UIConst.CINEMATIC_TWEEN_TIME)
        end
    end)
end

CinematicExUI.InitCinematicExUI = HL.Method() << function(self)
    self:_FirstTimeInit()
    self:Clear()
    self:_EnableControllerKeyCheckTick()
end

CinematicExUI.Clear = HL.Method() << function(self)
    self.m_pause = false
    self.view.exUINode.gameObject:SetActive(false)
    self:RefreshExistTime(-1)
    self:_ClearExNodeTween()
    self:_ClearTickUpdate()
    self:_DisableControllerKeyCheckTick()
end

CinematicExUI.RefreshExistTime = HL.Method(HL.Number) << function(self, existTime)
    self.m_exNodeExistTime = existTime
end

CinematicExUI.SetPause = HL.Method(HL.Boolean) << function(self, pause)
    self.m_pause = pause
end

CinematicExUI._TrySwitchExNode = HL.Method(HL.Boolean) << function(self, show)
    if self.m_exNodeTween then
        return
    end

    local exUINode = self.view.exUINode
    local active = exUINode.gameObject.activeSelf
    if show and not active then
        self:_ClearExNodeTween()
        exUINode.gameObject:SetActive(true)
        exUINode.alpha = 0
        self.m_exNodeTween = exUINode:DOFade(1, UIConst.CINEMATIC_TWEEN_TIME)
        self:RefreshExistTime(Tables.cinematicConst.cinematicUIShowTime + UIConst.CINEMATIC_TWEEN_TIME)
        self.m_exNodeTween:OnComplete(function()
            self:_ClearExNodeTween()
        end)

        self:_ClearTickUpdate()
        self.m_tickKey = LuaUpdate:Add("Tick", function(deltaTime)
            if not self.m_pause then
                if self.m_exNodeExistTime > 0 then
                    self.m_exNodeExistTime = self.m_exNodeExistTime - deltaTime
                else
                    self:_TrySwitchExNode(false)
                end
            end
        end)

    elseif not show and active then
        self:_ClearExNodeTween()
        exUINode.gameObject:SetActive(true)
        exUINode.alpha = 1
        self.m_exNodeTween = exUINode:DOFade(0, UIConst.CINEMATIC_TWEEN_TIME)
        self.m_exNodeTween:OnComplete(function()
            self:_ClearExNodeTween()
            exUINode.gameObject:SetActive(false)
            self:_ClearTickUpdate()
        end)
    end
end

CinematicExUI._ClearExNodeTween = HL.Method() << function(self)
    if self.m_exNodeTween then
        self.m_exNodeTween:Kill()
    end
    self.m_exNodeTween = nil
end

CinematicExUI._ClearTickUpdate = HL.Method() << function(self)
    if self.m_tickKey > 0 then
        LuaUpdate:Remove(self.m_tickKey)
    end
    self.m_tickKey = -1
end



CinematicExUI.m_controllerKeyCheckTick = HL.Field(HL.Number) << -1

CinematicExUI._EnableControllerKeyCheckTick = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    self.m_controllerKeyCheckTick = LuaUpdate:Add("Tick", function(deltaTime)
        if InputManagerInst:AnyGamepadKey() then
            if self.m_exNodeExistTime < 0 then
                self:_TrySwitchExNode(true)
            else
                self:RefreshExistTime(Tables.cinematicConst.cinematicUIShowTime + UIConst.CINEMATIC_TWEEN_TIME)
            end
        end
    end)
end

CinematicExUI._DisableControllerKeyCheckTick = HL.Method() << function(self)
    if self.m_controllerKeyCheckTick > 0 then
        LuaUpdate:Remove(self.m_controllerKeyCheckTick)
        self.m_controllerKeyCheckTick = -1
    end
end



HL.Commit(CinematicExUI)
return CinematicExUI

