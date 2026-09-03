local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DomainDepotPackBackGround
local DOMAIN_DEPOT_BACKGROUND_STAGES = UIConst.DOMAIN_DEPOT_BACKGROUND_STAGES

DomainDepotPackBackGroundCtrl = HL.Class('DomainDepotPackBackGroundCtrl', uiCtrl.UICtrl)

local STAGE_TO_ANIMATION_IN = {
    [DOMAIN_DEPOT_BACKGROUND_STAGES.Pack] = "domainDepot_type_in",
    [DOMAIN_DEPOT_BACKGROUND_STAGES.WaitSelectBuyer] = "domainDepot_background_in",
    [DOMAIN_DEPOT_BACKGROUND_STAGES.SelectBuyer] = "domainDepot_packagesellbg_in",
    [DOMAIN_DEPOT_BACKGROUND_STAGES.FinishSelectBuyer] = "domainDepot_goodsettle_in",
}

local STAGE_TO_ANIMATION_OUT = {
    [DOMAIN_DEPOT_BACKGROUND_STAGES.Pack] = "domainDepot_type_out",
    [DOMAIN_DEPOT_BACKGROUND_STAGES.WaitSelectBuyer] = "domainDepot_background_out",
    [DOMAIN_DEPOT_BACKGROUND_STAGES.SelectBuyer] = "domainDepot_packagesellbg_out",
    [DOMAIN_DEPOT_BACKGROUND_STAGES.FinishSelectBuyer] = "domainDepot_goodsettle_out",
}





DomainDepotPackBackGroundCtrl.s_messages = HL.StaticField(HL.Table) << {

}

DomainDepotPackBackGroundCtrl.m_count = HL.Field(HL.Number) << 0


DomainDepotPackBackGroundCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.domainDepotPack:InitDomainDepotPack()
end

DomainDepotPackBackGroundCtrl.ChangePackItemType = HL.Method(GEnums.DomainDepotDeliverItemType) << function(self, itemType)
    self.view.decoStateNode:SetState(itemType:ToString())
end

DomainDepotPackBackGroundCtrl.OnGoodsPack = HL.Method() << function(self)
    self.view.domainDepotGoodsPackBgNode.gameObject:SetActiveIfNecessary(true)
    self.view.domainDepotGoodsSettleBgBode.gameObject:SetActiveIfNecessary(false)
    self.view.domainDepotPackBackGroundBgNode.gameObject:SetActiveIfNecessary(false)
    self.view.domainDepotPackageSellBgNode.gameObject:SetActiveIfNecessary(false)

    self:PlayAnimationByStage(DOMAIN_DEPOT_BACKGROUND_STAGES.Pack, true)
end

DomainDepotPackBackGroundCtrl.OnPackBackGround = HL.Method() << function(self)
    self.view.domainDepotGoodsPackBgNode.gameObject:SetActiveIfNecessary(false)
    self.view.domainDepotGoodsSettleBgBode.gameObject:SetActiveIfNecessary(false)
    self.view.domainDepotPackBackGroundBgNode.gameObject:SetActiveIfNecessary(true)
    self.view.domainDepotPackageSellBgNode.gameObject:SetActiveIfNecessary(false)

    self:PlayAnimationByStage(DOMAIN_DEPOT_BACKGROUND_STAGES.WaitSelectBuyer, true)
end

DomainDepotPackBackGroundCtrl.OnPackageSell = HL.Method() << function(self)
    self.view.domainDepotGoodsPackBgNode.gameObject:SetActiveIfNecessary(false)
    self.view.domainDepotGoodsSettleBgBode.gameObject:SetActiveIfNecessary(false)
    self.view.domainDepotPackBackGroundBgNode.gameObject:SetActiveIfNecessary(false)
    self.view.domainDepotPackageSellBgNode.gameObject:SetActiveIfNecessary(true)

    self:PlayAnimationByStage(DOMAIN_DEPOT_BACKGROUND_STAGES.SelectBuyer, true)
end

DomainDepotPackBackGroundCtrl.InitPackageSellBgNode = HL.Method(HL.Any) << function(self, deliverInfo)
    self.view.domainDepotPack:InitPackageSellBgNode(deliverInfo)
end

DomainDepotPackBackGroundCtrl.OnGoodsSettle = HL.Method(HL.Number) << function(self, insId)
    self.view.domainDepotGoodsPackBgNode.gameObject:SetActiveIfNecessary(false)
    self.view.domainDepotGoodsSettleBgBode.gameObject:SetActiveIfNecessary(true)
    self.view.domainDepotPackBackGroundBgNode.gameObject:SetActiveIfNecessary(false)
    self.view.domainDepotPackageSellBgNode.gameObject:SetActiveIfNecessary(false)

    self.view.domainDepotPack:CloseBoxCover(insId)

    self:PlayAnimationByStage(DOMAIN_DEPOT_BACKGROUND_STAGES.FinishSelectBuyer, true)
end

DomainDepotPackBackGroundCtrl.PlayAnimationByStage = HL.Method(HL.Number, HL.Boolean, HL.Opt(HL.Function)) << function(self, stage, isIn, callback)
    local animGetter = isIn and STAGE_TO_ANIMATION_IN or STAGE_TO_ANIMATION_OUT
    local animName = animGetter[stage]
    self.animationWrapper:PlayWithTween(animName, function()
        if callback ~= nil then
            callback()
        end
    end)

    if not isIn then
        AudioAdapter.PostEvent("Au_UI_Event_RegionWareBox_Out")
    end
end

HL.Commit(DomainDepotPackBackGroundCtrl)
