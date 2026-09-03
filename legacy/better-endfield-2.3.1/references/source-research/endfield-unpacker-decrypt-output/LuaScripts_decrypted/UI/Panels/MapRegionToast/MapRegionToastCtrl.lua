local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MapRegionToast
local ELevelAreaPriority = CS.Beyond.Gameplay.ELevelAreaPriority

MapRegionToastCtrl = HL.Class('MapRegionToastCtrl', uiCtrl.UICtrl)








local MAP_REGION_MAIN_HUD_TOAST_TYPE = "MapRegionToast"





MapRegionToastCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.INTERRUPT_MAIN_HUD_ACTION_QUEUE] = "OnToastInterrupted",
}

MapRegionToastCtrl.RequestShowMapRegionToast = HL.StaticMethod(HL.Table) << function(args)
    if GameWorld.worldInfo.curLevelId == Tables.spaceshipConst.visitSceneName then
        return
    end
    local priority, isFirst, mainTitle, subTitle, depthText = unpack(args)

    local toastInfo = {
        priority = priority,
        isFirst = isFirst,
        mainTitle = mainTitle,
        subTitle = subTitle,
        depthText = depthText
    }
    LuaSystemManager.mainHudActionQueue:AddRequest(MAP_REGION_MAIN_HUD_TOAST_TYPE, function()
        MapRegionToastCtrl._OnShowMapRegionToast(toastInfo)
    end)
end

MapRegionToastCtrl.ClearAllMapRegionToast = HL.StaticMethod() << function()

    if LuaSystemManager.mainHudActionQueue then
        LuaSystemManager.mainHudActionQueue:RemoveActionsOfType(MAP_REGION_MAIN_HUD_TOAST_TYPE)
    end
end

MapRegionToastCtrl._OnShowMapRegionToast = HL.StaticMethod(HL.Table) << function(toastInfo)

    local self = UIManager:AutoOpen(PANEL_ID)
    self:DisplayToast(toastInfo)
end

MapRegionToastCtrl.OnToastInterrupted = HL.Method() << function(self)

    self.view.animationWrapper:ClearTween(false)
    self:Hide()
end


MapRegionToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, args)
end

MapRegionToastCtrl.OnClose = HL.Override() << function(self)

end


MapRegionToastCtrl.DisplayToast = HL.Method(HL.Table) << function(self, toastInfo)

    local toastView = self.view.bigMapNode
    local toastAnimName = "map_toast_inout"
    local toastAudKey = "Au_UI_Banner_Region_big_Open"
    if toastInfo.priority == ELevelAreaPriority.Override then

        toastAnimName = "map_region_toast_normal_inout"
        toastView = self.view.smallMapNode
        toastAudKey = "Au_UI_Banner_Region_Side_Open"
        if toastInfo.isFirst then
            toastAnimName = "map_region_toast_first_inout"
            toastView = self.view.firstSmallMapNode
            toastAudKey = "Au_UI_Banner_Region_Main_Open"
        end

        toastView.numberTxt.text = toastInfo.depthText
    else
        local success, activeIds, releaseIds = GameInstance.player.remoteFactory:TryGetCacheChangedChapterIds()
        if success then
            toastAnimName = "map_toast_inout_with_chapter_changed"
            self.view.systemStateNode.activeTxt.text = Tables.domainDataTable[activeIds].domainName
            self.view.systemStateNode.releaseTxt.text = Tables.domainDataTable[releaseIds].domainName
            self.view.systemStateNode.activeIcon:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT, Tables.domainDataTable[activeIds].domainIcon)
            self.view.systemStateNode.releaseIcon:LoadSprite(UIConst.UI_SPRITE_SETTLEMENT, Tables.domainDataTable[releaseIds].domainIcon)
            toastAudKey = "Au_UI_Toast_FactoryStartStopWork_Open"
        end
    end

    toastView.nameTxt.text = toastInfo.mainTitle
    toastView.descTxt.text = toastInfo.subTitle
    self.view.animationWrapper:Play(toastAnimName, function()
        self:Hide()
        Notify(MessageConst.ON_ONE_MAIN_HUD_ACTION_FINISHED, MAP_REGION_MAIN_HUD_TOAST_TYPE)
    end)
    AudioManager.PostEvent(toastAudKey)
end

HL.Commit(MapRegionToastCtrl)
