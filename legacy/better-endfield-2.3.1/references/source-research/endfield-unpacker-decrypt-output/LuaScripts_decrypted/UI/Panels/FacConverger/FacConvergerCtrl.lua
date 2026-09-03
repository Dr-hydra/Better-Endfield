local routerCtrl = require_ex('UI/Panels/FacRouter/FacRouterCtrl')
local PANEL_ID = PanelId.FacConverger
FacConvergerCtrl = HL.Class('FacConvergerCtrl', routerCtrl.FacRouterCtrl)







FacConvergerCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacConvergerCtrl._InitRouterPortData = HL.Override() << function(self)
    self.m_isSinglePortIn = false

    self.m_inBindingAnimMap = {
        {
            decoAnimWrapper = self.view.arrowDecoAnimationIn1,
            decoAnimName = "facconnector_arrow",
            conveyorAnimWrapper = self.view.conveyorAnimationIn1,
            conveyorAnimName = "facconvergerarrow_loop",
        },
        {
            decoAnimWrapper = self.view.arrowDecoAnimationIn2,
            decoAnimName = "facconnector_arrow",
            conveyorAnimWrapper = self.view.conveyorAnimationIn2,
            conveyorAnimName = "facconvergerarrow_loop",
        },
        {
            decoAnimWrapper = self.view.arrowDecoAnimationIn3,
            decoAnimName = "facconnector_arrow",
            conveyorAnimWrapper = self.view.conveyorAnimationIn3,
            conveyorAnimName = "facconvergerarrow_loop",
        }
    }
    self.m_outBindingAnimMap = {
        {
            decoAnimWrapper = self.view.arrowDecoAnimationOut1,
            decoAnimName = "facconnector_arrow",
            conveyorAnimWrapper = self.view.conveyorAnimationOut1,
            conveyorAnimName = "facconvergerarrow_rightloop",
        },
    }

    self.m_inItemAnimMap = {
        {
            animationNode = self.view.itemAnimationIn1,
            animationName = "converger_itemleft_changed",
        },
        {
            animationNode = self.view.itemAnimationIn2,
            animationName = "connector_item_changed",
        },
        {
            animationNode = self.view.itemAnimationIn3,
            animationName = "connector_itemleft_changed",
        }
    }
    self.m_outItemAnimMap = {
        {
            animationNode = self.view.itemAnimationOut1,
            animationName = "connector_itemright_changed",
        }
    }


    local naviBridgeBtn
    if self.view.buildingCommon.view.delButton.gameObject.activeSelf then
        naviBridgeBtn = self.view.buildingCommon.view.delButton
    else
        naviBridgeBtn = self.view.buildingCommon.view.forbiddenDelButton
    end
    self.view.itemLogistics4.view.button:SetExplicitSelectOnRight(naviBridgeBtn)
    self.view.itemLogistics1.view.button:SetExplicitSelectOnLeft(naviBridgeBtn)
    self.m_initialNaviTarget = self.view.itemLogistics4
end

HL.Commit(FacConvergerCtrl)
