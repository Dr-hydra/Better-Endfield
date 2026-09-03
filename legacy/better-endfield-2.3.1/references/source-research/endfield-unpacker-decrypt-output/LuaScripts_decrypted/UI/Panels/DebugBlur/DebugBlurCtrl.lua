local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.DebugBlur
DebugBlurCtrl = HL.Class('DebugBlurCtrl', uiCtrl.UICtrl)







DebugBlurCtrl.s_messages = HL.StaticField(HL.Table) << {

    [MessageConst.ADD_UI_BLUR] = "AddUIBlur",
    [MessageConst.ADD_UI_BLUR_RANDOM] = "AddUIBlurRandom"
}

DebugBlurCtrl.m_orderLayerOffset = HL.Field(HL.Number) << 1

DebugBlurCtrl.m_rootSortingOrder = HL.Field(HL.Number) << -1


DebugBlurCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.blurWithUICanvas.gameObject:SetActive(false)
    self.view.blurSceneCanvas.gameObject:SetActive(false)

    self.m_rootSortingOrder = self.view.gameObject:GetComponent("Canvas").sortingOrder
end











DebugBlurCtrl.ShowDebugBlur = HL.StaticMethod() << function()
    DebugBlurCtrl.AutoOpen(PANEL_ID, nil, false)
end

DebugBlurCtrl.AddUIBlur = HL.Method(HL.Table) << function(self, args)
    local w, h, x, y = unpack(args)

    self:_ProcessUIBlur(tonumber(w), tonumber(h), tonumber(x), tonumber(y))
    logger.warn(string.format("raqinyuan===> width:%s,height:%s; x:%s, y:%s", w, h, x, y))
    logger.warn(string.format("屏幕宽高：%d * %d", Screen.width, Screen.height))
end

DebugBlurCtrl.AddUIBlurRandom = HL.Method() << function(self)
    local halfScreenWidth = Screen.width / 2
    local halfScreenHeight = Screen.height / 2

    local w = lume.random(200, 500)
    local h = lume.random(200, 500)
    local x = lume.random(-(halfScreenWidth - w), halfScreenWidth - w)
    local y = lume.random(-(halfScreenHeight - h), halfScreenHeight - h)

    self:_ProcessUIBlur(w, h, x, y)
    logger.warn(string.format("raqinyuan===> width:%s,height:%s; x:%s, y:%s", w, h, x, y))
    logger.warn(string.format("屏幕宽高：%d * %d", Screen.width, Screen.height))
end

DebugBlurCtrl._ProcessUIBlur = HL.Method(HL.Number, HL.Number, HL.Number, HL.Number) << function(self, w, h, x, y)
    local go = CSUtils.CreateObject(self.view.blurWithUICanvas.gameObject, self.view.transform)

    go:GetComponent("Canvas").sortingOrder = self.m_rootSortingOrder + self.m_orderLayerOffset
    go.transform.sizeDelta = Vector2(tonumber(w), tonumber(h))
    go.transform.anchoredPosition = Vector2(tonumber(x), tonumber(y))
    go:SetActive(true)

    self.m_orderLayerOffset = self.m_orderLayerOffset + 1
end

HL.Commit(DebugBlurCtrl)
