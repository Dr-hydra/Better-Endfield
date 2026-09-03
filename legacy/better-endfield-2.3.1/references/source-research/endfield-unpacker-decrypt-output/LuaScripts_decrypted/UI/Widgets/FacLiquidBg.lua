local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FacLiquidBg = HL.Class('FacLiquidBg', UIWidgetBase)

local LIQUID_HEIGHT_TWEEN_DURATION = 0.2

FacLiquidBg.m_liquidHeightTween = HL.Field(HL.Userdata)

FacLiquidBg.m_lastLiquidHeight = HL.Field(HL.Number) << -1

FacLiquidBg.m_runtimeMaterials = HL.Field(HL.Table)


FacLiquidBg._OnFirstTimeInit = HL.Override() << function(self)
    self:_InitLiquidBgMaterial()
end

FacLiquidBg._OnDestroy = HL.Override() << function(self)
    if self.m_liquidHeightTween ~= nil then
        self.m_liquidHeightTween:Kill(false)
        self.m_liquidHeightTween = nil
    end

    if self.m_runtimeMaterials ~= nil and next(self.m_runtimeMaterials) then
        for _, material in pairs(self.m_runtimeMaterials) do
            Unity.Object.Destroy(material)
        end
    end
    self.m_runtimeMaterials = nil
end

FacLiquidBg.InitFacLiquidBg = HL.Method() << function(self)
    self:_FirstTimeInit()
end

FacLiquidBg._InitLiquidBgMaterial = HL.Method() << function(self)
    local liquid1 = self.view.liquid1
    local liquid2 = self.view.liquid2

    local material1 = Unity.Material(liquid1.material)
    local material2 = Unity.Material(liquid2.material)

    liquid1.material = material1
    liquid2.material = material2

    self.m_runtimeMaterials = { material1, material2 }
end

FacLiquidBg._RefreshLiquidHeight = HL.Method(HL.Number) << function(self, height)
    if self.m_runtimeMaterials == nil then
        return
    end

    if height == self.m_lastLiquidHeight then
        return
    end

    local liquid1 = self.view.liquid1
    local liquid2 = self.view.liquid2

    height = height * self.view.config.MAX_MATERIAL_LIQUID_HEIGHT

    if self.m_lastLiquidHeight < 0 then
        liquid1.material:SetFloat("_LiquidHeight", height)
        liquid2.material:SetFloat("_LiquidHeight", height)
    else
        self.m_liquidHeightTween = DOTween.To(function()
            return liquid1.material:GetFloat("_LiquidHeight")
        end, function(currHeight)
            if self.m_liquidHeightTween == nil then
                return
            end
            liquid1.material:SetFloat("_LiquidHeight", currHeight)
            liquid2.material:SetFloat("_LiquidHeight", currHeight)
        end, height, LIQUID_HEIGHT_TWEEN_DURATION):OnComplete(
            function()
                if self.m_liquidHeightTween == nil or self.m_runtimeMaterials == nil then
                    return
                end
                liquid1.gameObject:SetActiveIfNecessary(height > 0)
                liquid2.gameObject:SetActiveIfNecessary(height > 0)
            end
        )
    end

    liquid1.gameObject:SetActiveIfNecessary(height > 0)
    liquid2.gameObject:SetActiveIfNecessary(height > 0)

    self.m_lastLiquidHeight = height
end

FacLiquidBg.RefreshLiquidHeight = HL.Method(HL.Number) << function(self, height)
    self:_RefreshLiquidHeight(height)
end

HL.Commit(FacLiquidBg)
return FacLiquidBg

