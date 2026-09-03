local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FacGasBg = HL.Class('FacGasBg', UIWidgetBase)

local GAS_HEIGHT_TWEEN_DURATION = 0.2

FacGasBg.m_heightTween = HL.Field(HL.Userdata)

FacGasBg.m_lastHeight = HL.Field(HL.Number) << -1

FacGasBg.m_tweenH = HL.Field(HL.Number) << 0

FacGasBg.RefreshGasHeight = HL.Method(HL.Number) << function(self, height)
    if height == self.m_lastHeight then
        return
    end
    if height <= 0 then
        self.view.gasNode.gameObject:SetActiveIfNecessary(false)
        self:_CleanTween()
    else
        self.m_heightTween = DOTween.To(function()
            return self.m_tweenH
        end, function(currentHeight)
            if self.m_heightTween == nil then
                return
            end
            self.m_tweenH = currentHeight
            self.view.animationWrapper:SampleClipAtPercent("facgasminer_filling", currentHeight)
        end, height, GAS_HEIGHT_TWEEN_DURATION):OnComplete(
            function()
                if self.m_heightTween == nil then
                    return
                end
                if height == 0 then
                    self.view.gasNode.gameObject:SetActiveIfNecessary(false)
                end
            end
        )
    end

    if height > 0 then
        self.view.gasNode.gameObject:SetActiveIfNecessary(true)
    end

    self.m_lastHeight = height
end

FacGasBg._OnDestroy = HL.Override() << function(self)
    self:_CleanTween()
end

FacGasBg._CleanTween = HL.Method() << function(self)
    if self.m_heightTween ~= nil then
        self.m_heightTween:Kill(false)
        self.m_heightTween = nil
    end

    self.m_tweenH = 0
end

HL.Commit(FacGasBg)
return FacGasBg

