local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')
local BLACKBOX_SELECT_IN_ANIM = "blackboxselection_in"
local BLACKBOX_SELECT_OUT_ANIM = "blackboxselection_out"

BlackboxSelectionCell = HL.Class('BlackboxSelectionCell', UIWidgetBase)


BlackboxSelectionCell._OnFirstTimeInit = HL.Override() << function(self)
end

BlackboxSelectionCell.InitBlackboxSelectionCell = HL.Method(HL.String, HL.Function, HL.String)
        << function(self, blackboxId, onClickFunc, redDotName)
    self:_FirstTimeInit()

    FactoryUtils.updateBlackboxCell(self.view, blackboxId, onClickFunc)
    self.view.redDot:InitRedDot(redDotName, blackboxId, nil, self:GetUICtrl().view.redDotScrollRect)
end

BlackboxSelectionCell.SetSelected = HL.Method(HL.Boolean, HL.Opt(HL.Boolean)) << function(self, selected, ignoreEffect)
    if ignoreEffect == true then
        self.view.selected.gameObject:SetActiveIfNecessary(selected)
        self.view.normal.gameObject:SetActiveIfNecessary(not selected)
        local anim = selected and BLACKBOX_SELECT_IN_ANIM or BLACKBOX_SELECT_OUT_ANIM
        self.view.animationWrapper:SampleClipAtPercent(anim, 1)
    else
        if selected then
            self.view.animationWrapper:Play(BLACKBOX_SELECT_IN_ANIM)
        else
            self.view.animationWrapper:Play(BLACKBOX_SELECT_OUT_ANIM)
        end
    end
end

BlackboxSelectionCell.PlayAnimationIn = HL.Method() << function(self)
    self.view.animationWrapper:PlayInAnimation()
end

HL.Commit(BlackboxSelectionCell)
return BlackboxSelectionCell

