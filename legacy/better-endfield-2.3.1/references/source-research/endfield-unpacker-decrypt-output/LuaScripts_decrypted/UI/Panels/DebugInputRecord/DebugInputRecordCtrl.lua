
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DebugInputRecord

DebugInputRecordCtrl = HL.Class('DebugInputRecordCtrl', uiCtrl.UICtrl)






DebugInputRecordCtrl.s_messages = HL.StaticField(HL.Table) << {

}

DebugInputRecordCtrl.m_lateTickKey = HL.Field(HL.Number) << -1

DebugInputRecordCtrl.m_items = HL.Field(HL.Table)


DebugInputRecordCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_items = {}
    local obj = CSUtils.CreateObject(self.view.item.gameObject, self.view.listContainer)
    local item = Utils.wrapLuaNode(obj)
    self.m_items[1] = item
    self.view.item.gameObject:SetActive(false)

    self.m_lateTickKey = LuaUpdate:Add("LateTick", function(deltaTime)
        self:_Update()
    end)
end





DebugInputRecordCtrl.OnClose = HL.Override() << function(self)
    if self.m_lateTickKey ~= -1 then
        LuaUpdate:Remove(self.m_lateTickKey)
        self.m_lateTickKey = -1
    end
end

DebugInputRecordCtrl._Update = HL.Method() << function(self)
    local current = InputManagerInst.currentPressHistory
    self.m_items[1].numTxt.text = tostring(current.frameCount)
    self.m_items[1].nameTxt.text = current:GetShowingString()

    local j = 2
    for i = InputManagerInst.inputRecordHistory.Count, 1, -1 do
        local item = self.m_items[j]
        if not item then
            local obj = CSUtils.CreateObject(self.view.item.gameObject, self.view.listContainer)
            item = Utils.wrapLuaNode(obj)
            item.gameObject:SetActive(true)
            self.m_items[j] = item
        end

        local record = InputManagerInst.inputRecordHistory[CSIndex(i)]
        if record then
            item.numTxt.text = tostring(record.frameCount)
            item.nameTxt.text = record:GetShowingString()
        end
        j = j + 1
        if j > InputManager.INPUT_RECORD_NUM then
            break
        end
    end
end

DebugInputRecordCtrl.OnToggleDebugInputRecord = HL.StaticMethod(HL.Table) << function(arg)
    local show = arg and arg[1]
    local isShowing = UIManager:IsShow(PANEL_ID)
    if isShowing and not show then
        UIManager:Close(PANEL_ID)
    elseif not isShowing and show then
        UIManager:AutoOpen(PANEL_ID, arg)
    end
end

HL.Commit(DebugInputRecordCtrl)
