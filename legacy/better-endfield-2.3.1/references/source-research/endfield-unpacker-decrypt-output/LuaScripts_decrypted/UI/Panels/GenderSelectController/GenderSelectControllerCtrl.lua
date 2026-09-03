
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GenderSelectController

local FIRST_IN_ANIM_DELAY = 1

GenderSelectControllerCtrl = HL.Class('GenderSelectControllerCtrl', uiCtrl.UICtrl)






GenderSelectControllerCtrl.s_messages = HL.StaticField(HL.Table) << {

}

GenderSelectControllerCtrl.m_hasInited = HL.Field(HL.Boolean) << false

GenderSelectControllerCtrl.m_animTimer = HL.Field(HL.Number) << 0

GenderSelectControllerCtrl.m_updateKey = HL.Field(HL.Number) << 0

GenderSelectControllerCtrl.m_hadHovering = HL.Field(HL.Boolean) << false
GenderSelectControllerCtrl.m_hoveringMale = HL.Field(HL.Boolean) << false


GenderSelectControllerCtrl.m_canHoverAgain = HL.Field(HL.Boolean) << true




GenderSelectControllerCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitActionEvent()
    if not DeviceInfo.usingController then
        return
    end

    self.m_updateKey = LuaUpdate:Add("LateTick", function()
        local stickValue = InputManagerInst:GetGamepadStickValue(true)

        if InputManager.CheckGamepadStickInDeadZone(stickValue) then
            self.m_canHoverAgain = true
            return
        end

        if not self.m_canHoverAgain then
            return
        end

        if self:IsHide() then
            return
        end


        if self.m_hadHovering then
            if self.m_hoveringMale then
                self:HoverBtn(false)
            else
                self:HoverBtn(true)
            end
        else
            if stickValue.x > 0 then
                self:HoverBtn(true)
            else
                self:HoverBtn(false)
            end
        end
        self.m_canHoverAgain = false
    end)
end


GenderSelectControllerCtrl.HoverBtn = HL.Method(HL.Boolean) << function(self, hoverMale)
    if hoverMale then
        self.view.btnFemale.enabled = false
        self.view.btnMale.enabled = true
        self:SetNaviTarget(self.view.btnMale)
    else
        self.view.btnFemale.enabled = true
        self.view.btnMale.enabled = false
        self:SetNaviTarget(self.view.btnFemale)
    end
end

GenderSelectControllerCtrl.OnClose = HL.Override() << function(self)
    self.m_updateKey = LuaUpdate:Remove(self.m_updateKey)
end

GenderSelectControllerCtrl._InitActionEvent = HL.Method() << function(self)
    self.view.btnFemale.onClick:AddListener(function()

        if DeviceInfo.usingController then
            if not self.m_hadHovering then
                return
            end
        end

        self.m_phase:ChooseFemale()
    end)

    self.view.btnFemale.onHoverChange:AddListener(function(isHover)
        self.m_hadHovering = isHover
        self.m_hoveringMale = false
        self:Notify(MessageConst.ON_GENDER_HOVER_CHANGE, {false, isHover})
    end)

    self.view.btnMale.onClick:AddListener(function()

        if DeviceInfo.usingController then
            if not self.m_hadHovering then
                return
            end
        end


        self.m_phase:ChooseMale()
    end)

    self.view.btnMale.onHoverChange:AddListener(function(isHover)
        self.m_hadHovering = isHover
        self.m_hoveringMale = true
        self:Notify(MessageConst.ON_GENDER_HOVER_CHANGE, {true, isHover})
    end)
end

GenderSelectControllerCtrl._DoPlayInAnim = HL.Method() << function(self)
    if self.m_animTimer > 0 then
        self:_ClearTimer(self.m_animTimer)
        self.m_animTimer = 0
    end

    if self.m_hasInited then
        self:PlayAnimationIn()
    else
        local wrapper = self.animationWrapper
        wrapper:SampleClipAtPercent("genderselectcontroller_in", 0)
        self.m_animTimer = self:_StartTimer(FIRST_IN_ANIM_DELAY, function()
            self:PlayAnimationIn()
            self:_ClearTimer(self.m_animTimer)
            self.m_animTimer = 0
        end)
    end
end

GenderSelectControllerCtrl.OnShow = HL.Override() << function(self)
    self:_DoPlayInAnim()
    self:Notify(MessageConst.ON_GENDER_HOVER_ANIM, {true, not self.m_hasInited})
    self.m_hasInited = true
end

GenderSelectControllerCtrl.OnHide = HL.Override() << function(self)
    self:Notify(MessageConst.ON_GENDER_HOVER_ANIM, {false, false})
end

HL.Commit(GenderSelectControllerCtrl)
