local phaseBase = require_ex('Phase/Core/PhaseBase')

PhaseShopStar = HL.Class('PhaseShopStar', phaseBase.PhaseBase)

PhaseShopStar.s_messages = HL.StaticField(HL.Table) << {

}

PhaseShopStar.GetCurStateArg = HL.Override().Return(HL.Opt(HL.Any)) << function(self)
    local panelItem = self.m_panel2Item[PanelId.ShopStar]
    if panelItem then
        local overrideArg = panelItem.uiCtrl:GetCurPhaseStateArg()
        if overrideArg ~= nil then
            return lume.deepCopy(overrideArg)
        end
    end
end

HL.Commit(PhaseShopStar)
