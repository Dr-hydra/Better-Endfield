local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.EquipToast

EquipToastCtrl = HL.Class('EquipToastCtrl', uiCtrl.UICtrl)






EquipToastCtrl.s_messages = HL.StaticField(HL.Table) << {
}

EquipToastCtrl.m_showingToasts = HL.Field(HL.Forward("Queue"))

EquipToastCtrl.m_pendingToasts = HL.Field(HL.Forward("Queue"))

EquipToastCtrl.m_cacheToasts = HL.Field(HL.Forward("Stack"))

EquipToastCtrl.m_maxCount = HL.Field(HL.Number) << 5

EquipToastCtrl.OnShowToast = HL.StaticField(HL.Any) << function (arg)
    local ctrl = EquipToastCtrl.AutoOpen(PANEL_ID, nil, false)
    ctrl:ShowToast(arg)
end


EquipToastCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_showingToasts = require_ex("Common/Utils/DataStructure/Queue")()
    self.m_pendingToasts = require_ex("Common/Utils/DataStructure/Queue")()
    self.m_cacheToasts = require_ex("Common/Utils/DataStructure/Stack")()
    self.view.toastCell.gameObject:SetActive(false)
    self:_InitMaxCount()

    self.view.systemToast.gameObject:SetActive(false)
end

EquipToastCtrl.OnShow = HL.Override() << function(self)
    self:_InitMaxCount()
    self:_TryShowPendingToast()
end

EquipToastCtrl._InitMaxCount = HL.Method() << function (self)
    self.m_maxCount = self.view.config.MAX_TOAST_COUNT or 10
end

EquipToastCtrl._GetCurTextToast = HL.Method(HL.String).Return(HL.Table, HL.Number) << function (self, text)
    if self.m_showingToasts:Empty() then
        return nil, -1
    end
    for i = 1, self.m_showingToasts:Size() do
        local toast = self.m_showingToasts:AtIndex(i)
        if toast.originalText == text then
            return toast, i
        end
    end
    return nil, -1
end

EquipToastCtrl._TryShowPendingToast = HL.Method() << function (self)
    while not self.m_pendingToasts:Empty() and self.m_showingToasts:Size() < self.m_maxCount do
        local arg = self.m_pendingToasts:Pop()
        self:ShowToast(arg)
    end
end

EquipToastCtrl.ShowToast = HL.Method(HL.Any) << function (self, arg)
    local text, duration = nil, nil

    if type(arg) == "string" then
        text = arg
    elseif type(arg) == "table" then
        text, duration = unpack(arg)
    end

    if text then
        local showingToasts = self.m_showingToasts
        local oldestToast, index = self:_GetCurTextToast(text)
        if oldestToast then
            self.m_showingToasts:RemoveAt(index)
            self:_ClearTimer(oldestToast.timerId)
            oldestToast.animation:ClearTween(false)
            self:_CacheToast(oldestToast)
        elseif showingToasts:Size() >= self.m_maxCount then
            self.m_pendingToasts:Push(arg)
            return
        end

        local toast = self:_GetToast()
        toast.transform:SetAsLastSibling()
        toast.gameObject:SetActive(true)
        toast.originalText = text

        toast.label:SetAndResolveTextStyle(text)
        showingToasts:Push(toast)
        if duration == nil or duration == 0 then
            toast.timerId = self:_StartTimer(self.view.config.SHOW_DURATION, function()
                self:_HideToast(toast)
            end)
        else
            toast.timerId = self:_StartTimer(duration, function()
                self:_HideToast(toast)
            end)
        end
    end
end

EquipToastCtrl._HideToast = HL.Method(HL.Table) << function(self, toast)
    toast.animation:PlayOutAnimation(function()
        self:_CacheToast(toast)
        local index = self.m_showingToasts:IndexOf(toast)
        if index ~= nil then
            self.m_showingToasts:RemoveAt(index)
        end
        self:_TryShowPendingToast()
    end)
end

EquipToastCtrl._GetToast = HL.Method().Return(HL.Table) << function(self)
    if self.m_cacheToasts:Count() > 0 then
        return self.m_cacheToasts:Pop()
    end

    local obj = CSUtils.CreateObject(self.view.toastCell.gameObject, self.view.list.transform)
    local toast = {}
    local luaRef = obj.transform:GetComponent("LuaReference")
    luaRef:BindToLua(toast)
    return toast
end

EquipToastCtrl._CacheToast = HL.Method(HL.Table) << function (self, toast)
    toast.gameObject:SetActive(false)
    toast.timerId = -1
    self.m_cacheToasts:Push(toast)
end

HL.Commit(EquipToastCtrl)
