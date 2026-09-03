
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonProgress

local ICON_PATH = "CommonProgress"

CommonProgressCtrl = HL.Class('CommonProgressCtrl', uiCtrl.UICtrl)

CommonProgressCtrl.m_doTweenAnim = HL.Field(HL.Any)

CommonProgressCtrl.m_totalTime = HL.Field(HL.Any) << 0

CommonProgressCtrl.m_progressId = HL.Field(HL.Any) << 0

CommonProgressCtrl.m_system = HL.Field(CS.Beyond.Gameplay.ProgressManager)





CommonProgressCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ON_INTERRUPT_PROGRESS] = '_OnInterruptProgress',
    [MessageConst.ON_CLOSE_PROGRESS] = '_OnCloseProgress',
    [MessageConst.ON_REFRESH_PROGRESS] = '_OnRefreshProgress'
}


CommonProgressCtrl.OnCreate = HL.Override(HL.Any) << function(self, id)
    self.m_system = GameWorld.progressManager
    self.view.itemContent.onClick:AddListener(function()
        if self.m_doTweenAnim then
            self.m_doTweenAnim:Kill()
        end
        self.m_system:InterruptProgress(self.m_progressId)
    end)
    if self.m_system == nil then
        self:_ClosePanel()
    end
    self:_RefreshUI(id)
end

CommonProgressCtrl._RefreshUI = HL.Method(HL.Any) << function(self, id)
    if id == self.m_progressId then
        return
    end

    if self.m_doTweenAnim then
        self.m_doTweenAnim:Kill()
    end

    self.m_progressId = id;
    local uiInfo = self.m_system:GetUIInfo(self.m_progressId)

    if string.isEmpty(uiInfo.icon) then
        self.view.iconBG.gameObject:SetActiveIfNecessary(false)
    else
        self.view.iconBG.gameObject:SetActiveIfNecessary(true)
        self.view.iconBG:LoadSprite(ICON_PATH, uiInfo.icon)
    end

    if string.isEmpty(uiInfo.txt) then
        self.view.text.gameObject:SetActiveIfNecessary(false)
    else
        self.view.text.gameObject:SetActiveIfNecessary(true)
        self.view.text:SetAndResolveTextStyle(uiInfo.txt)
    end

    self.m_totalTime = self.m_system:GetDuration(self.m_progressId)
    self.view.itemContent.gameObject:SetActiveIfNecessary(uiInfo.showCancel)


    self:_PlayDoTweenAnim()
end

CommonProgressCtrl._OnInterruptProgress = HL.Method(HL.Any) << function(self, args)
    local id = unpack(args)
    if id ~= self.m_progressId then
        return
    end
    self.m_system:InterruptProgress(self.m_progressId)
end

CommonProgressCtrl._OnCloseProgress = HL.Method(HL.Any) << function(self, args)
    local id = unpack(args)
    if id ~= self.m_progressId then
        return
    end
    self:_ClosePanel()
end

CommonProgressCtrl._OnRefreshProgress = HL.Method(HL.Any) << function(self, id)
    if id == self.m_progressId then
        return
    end
    self:_RefreshUI(id)
end

CommonProgressCtrl._PlayDoTweenAnim = HL.Method() << function(self)
    local progress = self.m_system:GetProgress(self.m_progressId)
    self.view.barMask.sizeDelta = Vector2((progress - 1) * self.view.bar.rect.size.x,self.view.barMask.sizeDelta.y)

    self.m_doTweenAnim = self.view.barMask:DOSizeDelta(
        Vector2(0, self.view.barMask.rect.size.y),
        (1 - self.m_system:GetProgress(self.m_progressId)) * self.m_totalTime
    ):SetEase(1)
end

CommonProgressCtrl._ClosePanel = HL.Method() << function(self)
    self.view.anim:PlayOutAnimation(function()
        UIManager:Close(PANEL_ID)
    end)
end











CommonProgressCtrl._OnProgressOpen = HL.StaticMethod(HL.Any) << function(args)
    local id, force = unpack(args)
    if UIManager:IsOpen(PANEL_ID) then
        if not force then
            return
        else
            Notify(MessageConst.ON_REFRESH_PROGRESS, id)
            return
        end
    end

    UIManager:Open(PANEL_ID, id)
end

HL.Commit(CommonProgressCtrl)
