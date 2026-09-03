local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

ComboUseBg = HL.Class('ComboUseBg', UIWidgetBase)

ComboUseBg.m_lights = HL.Field(HL.Table)


ComboUseBg._OnFirstTimeInit = HL.Override() << function(self)
    self.m_lights = {}
    table.insert(self.m_lights, self.view.colorLight2)
    table.insert(self.m_lights, self.view.colorLight3)
    table.insert(self.m_lights, self.view.colorLight4)
end

ComboUseBg.InitComboUseBg = HL.Method() << function(self)
    self:_FirstTimeInit()
end

ComboUseBg.PlayInAnimation = HL.Method() << function(self)
    self.view.animationWrapper:PlayInAnimation()
end

ComboUseBg.SampleToInAnimationBegin = HL.Method() << function(self)
    self.view.animationWrapper:SampleToInAnimationBegin()
end

ComboUseBg.PlayOutAnimation = HL.Method() << function(self)
    self.view.animationWrapper:PlayOutAnimation()
end

ComboUseBg.PlayLightAnim = HL.Method(HL.Number) << function(self, totalNum)
    local light = self.m_lights[totalNum - 1]
    if light then
        light:PlayInAnimation()
    end
end

HL.Commit(ComboUseBg)
return ComboUseBg

