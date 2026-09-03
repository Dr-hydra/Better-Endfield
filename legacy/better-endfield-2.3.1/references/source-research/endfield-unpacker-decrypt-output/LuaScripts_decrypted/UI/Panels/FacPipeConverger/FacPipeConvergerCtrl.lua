local pipeRouterCtrl = require_ex('UI/Panels/FacPipeRouter/FacPipeRouterCtrl')
local PANEL_ID = PanelId.FacPipeConverger
FacPipeConvergerCtrl = HL.Class('FacPipeConvergerCtrl', pipeRouterCtrl.FacPipeRouterCtrl)







FacPipeConvergerCtrl.s_messages = HL.StaticField(HL.Table) << {

}

FacPipeConvergerCtrl._InitRouterPortData = HL.Override() << function(self)
    self.m_isSinglePortIn = false

    self.m_inBindingAnimMap = {
        {
            decoAnimWrapper = self.view.arrowDecoAnimationIn1,
            decoAnimName = "facconnector_arrow03",
            conveyorAnimWrapper = self.view.conveyorAnimationIn1,
            conveyorAnimName = "facpipeconnector_arrow03",
        },
        {
            decoAnimWrapper = self.view.arrowDecoAnimationIn2,
            decoAnimName = "facconnector_arrow",
            conveyorAnimWrapper = self.view.conveyorAnimationIn2,
            conveyorAnimName = "facpipeconnector_arrow",
        },
        {
            decoAnimWrapper = self.view.arrowDecoAnimationIn3,
            decoAnimName = "facconnector_arrow02",
            conveyorAnimWrapper = self.view.conveyorAnimationIn3,
            conveyorAnimName = "facpipeconnector_arrow",
        }
    }
    self.m_outBindingAnimMap = {
        {
            decoAnimWrapper = self.view.arrowDecoAnimationOut1,
            decoAnimName = "facconnector_arrow01",
            conveyorAnimWrapper = self.view.conveyorAnimationOut1,
            conveyorAnimName = "facpipeconnector_arrow02",
        },
    }

    self.m_inItemAnimMap = {
        {
            animationNode = self.view.itemAnimationIn1,
            animationName = "connector_facpipe_changed_4",
        },
        {
            animationNode = self.view.itemAnimationIn2,
            animationName = "connector_facpipe_changed_3",
        },
        {
            animationNode = self.view.itemAnimationIn3,
            animationName = "connector_facpipe_changed",
        }
    }
    self.m_outItemAnimMap = {
        {
            animationNode = self.view.itemAnimationOut1,
            animationName = "connector_facpipe_changed_2",
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

HL.Commit(FacPipeConvergerCtrl)
