local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FacQuickBarClearDropZone = HL.Class('FacQuickBarClearDropZone', UIWidgetBase)


FacQuickBarClearDropZone.m_clearAreaDropHelper = HL.Field(HL.Forward('UIDropHelper'))



FacQuickBarClearDropZone._OnFirstTimeInit = HL.Override() << function(self)
    self.m_clearAreaDropHelper = UIUtils.initUIDropHelper(self.view.dropItem, {
        isQuickBarClearDropZone = true,
        acceptTypes = UIConst.FACTORY_QUICK_BAR_CLEAR_AREA_DROP_ACCEPT_INFO,
        onDropItem = function(eventData, dragHelper)
            self:_OnDropItemToClearArea(dragHelper)
        end,
    })
    self:RegisterMessage(MessageConst.ON_START_UI_DRAG, function(dragHelper)
        self:OnStartDrag(dragHelper)
    end, true)
    self:RegisterMessage(MessageConst.ON_END_UI_DRAG, function(dragHelper)
        self:OnEndDrag(dragHelper)
    end, true)
    self.view.dropItem.onToggleHighlight:AddListener(function(active)
        if active then
            self.view.animationWrapper:Play("facquickbarcleardrop_highlight")
            AudioAdapter.PostEvent("Au_UI_Toast_RemoveFromToolbar_On")
        else
            self.view.animationWrapper:SampleClip("facquickbarcleardrop_highlight", 0, true)
        end
    end)
end

FacQuickBarClearDropZone.InitFacQuickBarClearDropZone = HL.Method() << function(self)
    self:_FirstTimeInit()
    self.gameObject:SetActive(false)
end

FacQuickBarClearDropZone.OnStartDrag = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if DeviceInfo.usingController then
        return
    end
    if UIUtils.isTypeDropValid(dragHelper, UIConst.FACTORY_QUICK_BAR_CLEAR_AREA_DROP_ACCEPT_INFO) then
        UIUtils.PlayAnimationAndToggleActive(self.view.animationWrapper, true)
    end
end

FacQuickBarClearDropZone.OnEndDrag = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if DeviceInfo.usingController then
        return
    end
    UIUtils.PlayAnimationAndToggleActive(self.view.animationWrapper, false)
end

FacQuickBarClearDropZone._OnDropItemToClearArea = HL.Method(HL.Forward('UIDragHelper')) << function(self, dragHelper)
    if dragHelper.source ~= UIConst.UI_DRAG_DROP_SOURCE_TYPE.QuickBar then
        return
    end
    FactoryUtils.clearQuickBarSlot(dragHelper.info.csIndex)
    AudioAdapter.PostEvent("Au_UI_Button_Delete")
end


HL.Commit(FacQuickBarClearDropZone)
return FacQuickBarClearDropZone
