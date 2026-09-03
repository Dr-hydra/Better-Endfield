local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SettlementDefenseMiniMap

SettlementDefenseMiniMapCtrl = HL.Class('SettlementDefenseMiniMapCtrl', uiCtrl.UICtrl)






SettlementDefenseMiniMapCtrl.s_messages = HL.StaticField(HL.Table) << {
}


SettlementDefenseMiniMapCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.settlementDefenseMapRoot.gameObject:SetActive(false)
    self.view.settlementDefenseMapRoot.gameObject:SetActive(true)
    self.view.settlementDefenseMapRoot:InitSettlementDefenseMapRoot()
    self.view.openMapBtn.onClick:AddListener(function()
        PhaseManager:OpenPhase(PhaseId.SettlementDefenseMainMap)
    end)
end

HL.Commit(SettlementDefenseMiniMapCtrl)
