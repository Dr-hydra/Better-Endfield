local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.HyperlinkTips
local PHASE_ID = PhaseId.HyperlinkTips
HyperlinkTipsCtrl = HL.Class('HyperlinkTipsCtrl', uiCtrl.UICtrl)






HyperlinkTipsCtrl.s_messages = HL.StaticField(HL.Table) << {

}



HyperlinkTipsCtrl.WaitShowCoroutine = HL.StaticField(HL.Thread)

HyperlinkTipsCtrl.WaitShowCoroutineKey = HL.StaticField(HL.String) << "HyperlinkTipsCtrl"

HyperlinkTipsCtrl.m_curLinkId = HL.Field(HL.String) << ""

HyperlinkTipsCtrl.m_targetPos = HL.Field(HL.Any)

HyperlinkTipsCtrl.m_args = HL.Field(HL.Any)




HyperlinkTipsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
end









HyperlinkTipsCtrl.ShowHyperlinkTips = HL.StaticMethod(HL.Any) << function(args)
    local linkId = unpack(args)
    local linkType = UIUtils.resolveLinkTypeFromId(linkId)
    if linkType ~= UIConst.UI_TEXT_LINK_TYPE.Hyperlink then
        return
    end

    logger.info("[HyperlinkTipsCtrl] Show Event")
    CoroutineManager:ClearCoroutine(HyperlinkTipsCtrl.WaitShowCoroutine)
    local isOpen, self = UIManager:IsOpen(PANEL_ID)
    if isOpen then
        self:_HideTips()
    end
    HyperlinkTipsCtrl.WaitShowCoroutine = CoroutineManager:StartCoroutine(function()
        coroutine.wait(Tables.globalConst.showHyperlinkTipsNeedHoverTime)

        local self = UIManager:AutoOpen(PANEL_ID)
        self:_ShowTips(args)
    end, HyperlinkTipsCtrl.WaitShowCoroutineKey)
end

HyperlinkTipsCtrl.HideHyperlinkTips = HL.StaticMethod(HL.Opt(HL.Any)) << function(args)
    logger.info("[HyperlinkTipsCtrl] Hide Event")
    CoroutineManager:ClearCoroutine(HyperlinkTipsCtrl.WaitShowCoroutine)
    local isOpen, self = UIManager:IsOpen(PANEL_ID)
    if isOpen then
        self:_HideTips()
    end
end





HyperlinkTipsCtrl._ShowTips = HL.Method(HL.Any) << function(self, args)
    logger.info("[HyperlinkTipsCtrl] Show")
    UIManager:SetTopOrder(PANEL_ID)
    self.m_args = args
    self:_UpdateData()
    self:_RefreshUI()
    local finalXPosType, finalYPosType = UIUtils.updateTipsPositionWithScreenRect(
        self.view.content,
        self.m_targetPos,
        self.view.rectTransform,
        self.uiCamera,
        UIConst.UI_TIPS_POS_TYPE.RightDown
    )
    if finalXPosType == UIConst.UI_TIPS_X_POS_TYPE.Right then
        if finalYPosType == UIConst.UI_TIPS_Y_POS_TYPE.Bottom then
            self.view.arrowStateCtrl:SetState("LeftTopState")
        else
            self.view.arrowStateCtrl:SetState("LeftDownState")
        end
    else
        if finalYPosType == UIConst.UI_TIPS_Y_POS_TYPE.Bottom then
            self.view.arrowStateCtrl:SetState("RightTopState")
        else
            self.view.arrowStateCtrl:SetState("RightDownState")
        end
    end
end

HyperlinkTipsCtrl._HideTips = HL.Method() << function(self)
    logger.info("[HyperlinkTipsCtrl] Hide")
    self:_ClearData()
    self:Hide()
end

HyperlinkTipsCtrl._ClearData = HL.Method() << function(self)
    self.m_args = nil
    self.m_curLinkId = ""
    self.m_targetPos = nil
end

HyperlinkTipsCtrl._UpdateData = HL.Method() << function(self)
    self.m_curLinkId = unpack(self.m_args)
    self.m_curLinkId = string.gsub(self.m_curLinkId, CS.Beyond.UI.UIText.HYPERLINK_ID_PREFIX, "")
    local mousePos = InputManager.mousePosition

    self.m_targetPos = Unity.Rect(
        mousePos.x - self.view.config.OFFSET_X, Screen.height - mousePos.y - self.view.config.OFFSET_Y,
        2 * self.view.config.OFFSET_X, 2 * self.view.config.OFFSET_Y
    )
end

HyperlinkTipsCtrl._RefreshUI = HL.Method() << function(self)
    local cfgExist, hyperlinkCfg = Tables.hyperlinkTextTable:TryGetValue(self.m_curLinkId)
    if not cfgExist then
        self:_HideTips()
    end
    self.view.titleTxt.text = hyperlinkCfg.name
    self.view.contentTxt:SetAndResolveTextStyle(hyperlinkCfg.desc)
end



HL.Commit(HyperlinkTipsCtrl)
