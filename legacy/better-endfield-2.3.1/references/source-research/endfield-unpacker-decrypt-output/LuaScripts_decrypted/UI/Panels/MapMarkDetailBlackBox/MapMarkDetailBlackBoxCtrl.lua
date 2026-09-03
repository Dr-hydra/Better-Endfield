
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapMarkDetailBlackBox

MapMarkDetailBlackBoxCtrl = HL.Class('MapMarkDetailBlackBoxCtrl', uiCtrl.UICtrl)






MapMarkDetailBlackBoxCtrl.s_messages = HL.StaticField(HL.Table) << {

}


MapMarkDetailBlackBoxCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
    local markInstId = args.markInstId
    local getRuntimeDataSuccess, markRuntimeData = GameInstance.player.mapManager:GetMarkInstRuntimeData(markInstId)
    if getRuntimeDataSuccess == false then
        logger.error("地图详情页获取实例数据失败" .. markInstId)
        return
    end
    local domainId = markRuntimeData.domainId
    if string.isEmpty(domainId) then
        logger.error("黑盒详情页找不到domainId，无法正常显示" .. markInstId)

        self.view.detailCommon:InitMapMarkDetailCommon({})
        return
    end
    local _, domainInfo = Tables.domainDataTable:TryGetValue(domainId)
    local facPackId = domainInfo.facTechPackageId
    local _, packageData = Tables.facSTTGroupTable:TryGetValue(facPackId)
    local itemId = packageData.costPointType
    local _, itemInfo = Tables.itemTable:TryGetValue(itemId)
    local techPointName = itemInfo.name

    local commonArgs = {}
    commonArgs.bigBtnActive = true
    commonArgs.markInstId = markInstId
    commonArgs.descText = ""
    self.view.techPointTips.text = string.format(Language.LUA_BLACKBOX_MAP_DETAIL_TECH_PACK, techPointName)
    self.view.detailCommon:InitMapMarkDetailCommon(commonArgs)
end

HL.Commit(MapMarkDetailBlackBoxCtrl)
