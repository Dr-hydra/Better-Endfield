local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DramaticPerformanceBag
local PHASE_ID = PhaseId.DramaticPerformanceBag

DramaticPerformanceBagCtrl = HL.Class('DramaticPerformanceBagCtrl', uiCtrl.UICtrl)






DramaticPerformanceBagCtrl.s_messages = HL.StaticField(HL.Table) << {

}


local actionMenuRemoveText = {
    [Language.LUA_CONTROLLER_ITEM_ACTION_MOVE_HALF] = true,
    [Language.LUA_CONTROLLER_ITEM_ACTION_MOVE_ALL] = true,
}




DramaticPerformanceBagCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitUI()
    local text = InputManager.ParseTextActionId(self.view.tipsText.text)
    self.view.tipsText:SetAndResolveTextStyle(text)
    AudioManager.PostEvent("Au_UI_Menu_PureWhiteHub_Open")
    self:PlayAnimation("dramaticperformancebag_in_part_0", function()
        self:PlayAnimation("dramaticperformancebag_in_part_1", function()
            self:SetNaviTarget(self.view.bagItemSlot.view.itemSlot.view.item.view.button)
        end)
    end)
end

DramaticPerformanceBagCtrl.ShowBag = HL.StaticMethod(HL.Any) << function(arg)
    local isOpen = PhaseManager:OpenPhase(PHASE_ID)
    local callback = unpack(arg)
    if callback then
        callback(isOpen)
    end
end



DramaticPerformanceBagCtrl._InitUI = HL.Method() << function(self)
    local itemId = Tables.globalConst.dramaticPerformanceBagItemId
    local data = Tables.itemTable:GetValue(itemId)

    self.view.bagItemSlot:InitDramaticPerformanceBagItemSlot({
        id = itemId,
        count = 1,
        allowDrag = true,
        sourceType = UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot,
        acceptType = UIConst.UI_DRAG_DROP_SOURCE_TYPE.ItemBag,
        type = data.type,
        customChangeActionMenuFunc = function(actionMenuInfos)
            for i = #actionMenuInfos, 1, -1 do
                if actionMenuRemoveText[actionMenuInfos[i].text] then
                    table.remove(actionMenuInfos, i)
                end
            end
        end,
        cacheArea = {
            hasNormalCacheIn = true,
            NaviTargetMoveToInCacheSlot = function(_, _, dragHelper, _)
                self.view.facItemSlot:_OnDropItem(dragHelper)
                self:ClearNaviTarget()
            end,
        }
    })
    self.view.facItemSlot:InitDramaticPerformanceBagItemSlot({
        id = "",
        count = 0,
        allowDrag = false,
        sourceType = UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot,
        acceptType = UIConst.UI_DRAG_DROP_SOURCE_TYPE.FactoryDepot,
        type = data.type,
        onDropItem = function()
            self:_CompletePerformance()
        end,
    })

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

DramaticPerformanceBagCtrl._CompletePerformance = HL.Method() << function(self)
    AudioManager.PostEvent("Au_UI_Menu_PureWhiteHub_Close")
    self.view.bagItemSlot:ForbidDrag()
    self.view.bagItemSlot.view.itemSlot:InitItemSlot()
    self.view.animationWrapper:Play("dramaticperformancebag_out", function()
        PhaseManager:ExitPhaseFast(PHASE_ID)
        logger.info("[DramaticPerformanceBagCtrl] UI表演完成")
        GameAction.NotifyDramaticPerformanceBagFinish()
    end)
end

DramaticPerformanceBagCtrl.SetScreenCaptureImg = HL.Method(HL.Userdata) << function(self, renderTexture)
    self.view.screenCaptureImg.texture = renderTexture
end


HL.Commit(DramaticPerformanceBagCtrl)
