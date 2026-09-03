local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CommonToggle = HL.Class('CommonToggle', UIWidgetBase)

local TO_LEFT_ANIMATION_NAME = "common_toggle_to_left"
local TO_RIGHT_ANIMATION_NAME = "common_toggle_to_right"
local TO_LEFT_LIGHT_ANIMATION_NAME = "common_toggle_to_left01"
local TO_RIGHT_LIGHT_ANIMATION_NAME = "common_toggle_to_right01"

CommonToggle.toggle = HL.Field(CS.Beyond.UI.UIToggle)

CommonToggle.m_action = HL.Field(HL.Function)

CommonToggle.m_toLeftAnimName = HL.Field(HL.String) << ""

CommonToggle.m_toRightAnimName = HL.Field(HL.String) << ""


CommonToggle._OnFirstTimeInit = HL.Override() << function(self)
    self.toggle = self.view.toggle
end

CommonToggle.Toggle = HL.Method() << function(self)
    self.toggle.isOn = not self.toggle.isOn
end


CommonToggle.InitCommonToggle = HL.Method(HL.Function, HL.Boolean, HL.Opt(HL.Boolean, HL.Table)) <<
function(self, action, value, notCall, labels)
    self:_FirstTimeInit()

    self.m_action = action
    self.m_toLeftAnimName = self.view.config.IS_COLOR_LIGHT and TO_LEFT_LIGHT_ANIMATION_NAME or TO_LEFT_ANIMATION_NAME
    self.m_toRightAnimName = self.view.config.IS_COLOR_LIGHT and TO_RIGHT_LIGHT_ANIMATION_NAME or TO_RIGHT_ANIMATION_NAME

    self.toggle.onValueChanged:RemoveAllListeners()
    self.toggle.isOn = value
    self.toggle.onValueChanged:AddListener(function(isOn)
        self:_OnValueChanged(isOn)
    end)

    if notCall then
        self:_UpdateAnimation(value, true)
    else
        self:_OnValueChanged(value)
    end

    self:_SetLabels(labels)
end

CommonToggle._OnValueChanged = HL.Method(HL.Boolean) << function(self, isOn)
    self:_UpdateAnimation(isOn)
    self.m_action(isOn)
end

CommonToggle._UpdateAnimation = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, isOn, jumpToEnd)
    local name = isOn and self.m_toLeftAnimName or self.m_toRightAnimName
    self.view.animation:PlayWithTween(name)
    if jumpToEnd then
        self.view.animation:SampleClipAtPercent(name, 1)
    end
end

CommonToggle._SetLabels = HL.Method(HL.Opt(HL.Table)) << function(self, labels)
    if not labels then
        return
    end
    self.view.left.text.text = labels[1]
    self.view.right.text.text = labels[2]
end

CommonToggle.SetValue = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, isOn, withoutNotify)
    if withoutNotify then
        self.view.toggle:SetIsOnWithoutNotify(isOn)
        self:_UpdateAnimation(isOn, true)
    else
        self.view.toggle.isOn = isOn
    end
end

CommonToggle.ToggleInteractable = HL.Method(HL.Boolean) << function(self, interactable)
    self.view.toggle.interactable = interactable
end


CommonToggle.SetCustomAnimation = HL.Method(HL.String, HL.String) << function(self, leftAnimName, rightAnimName)
    self.m_toLeftAnimName = leftAnimName
    self.m_toRightAnimName = rightAnimName
    self:_UpdateAnimation(self.toggle.isOn, true)
end

HL.Commit(CommonToggle)
return CommonToggle
