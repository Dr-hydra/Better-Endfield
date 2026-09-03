
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CommonTitleTips

CommonTitleTipsCtrl = HL.Class('CommonTitleTipsCtrl', uiCtrl.UICtrl)






CommonTitleTipsCtrl.s_messages = HL.StaticField(HL.Table) << {

}

CommonTitleTipsCtrl.ShowTitleTips = HL.StaticMethod(HL.Any) << function(args)
    local ctrl = UIManager:AutoOpen(PANEL_ID)
    ctrl:ShowTips(args)
end

CommonTitleTipsCtrl.HideTitleTips = HL.StaticMethod() << function()
    UIManager:Close(PANEL_ID)
end


CommonTitleTipsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self:_InitViews()
end








CommonTitleTipsCtrl.ShowTips = HL.Method(HL.Any) << function(self, args)










    self.view.titleTxt.text = args.title
    self.view.contentTxt.text = args.desc
    self.view.backButton.gameObject:SetActive(not args.isSideTips)
    self:_RefreshPos(args)
end

CommonTitleTipsCtrl._InitViews = HL.Method() << function(self)
    self.view.backButton.onClick:RemoveAllListeners()
    self.view.backButton.onClick:AddListener(function()
        self:PlayAnimationOut()
    end)
end

CommonTitleTipsCtrl._RefreshPos = HL.Method(HL.Any) << function(self, args)
    local finalXPosType = UIConst.UI_TIPS_X_POS_TYPE.Right
    local finalYPosType = UIConst.UI_TIPS_Y_POS_TYPE.Bottom

    if args.targetTransform ~= nil then
        local targetScreenRect = UIUtils.getTransformScreenRect(args.targetTransform, self.uiCamera)
        finalXPosType, finalYPosType = UIUtils.updateTipsPositionWithScreenRect(
            self.view.content,
            targetScreenRect,
            self.view.rectTransform,
            self.uiCamera,
            args.posType
        )
    else
        local mousePos = InputManager.mousePosition
        local offsetX = self.view.config.OFFSET_X
        if args.offsetX ~= nil then
            offsetX = args.offsetX
        end
        local offsetY = self.view.config.OFFSET_Y
        if args.offsetY ~= nil then
            offsetY = args.offsetY
        end
        local targetPos = Unity.Rect(
            mousePos.x - offsetX, Screen.height - mousePos.y - offsetY,
            2 * offsetX, 2 * offsetY
        )
        finalXPosType, finalYPosType = UIUtils.updateTipsPositionWithScreenRect(
            self.view.content,
            targetPos,
            self.view.rectTransform,
            self.uiCamera,
            args.posType
        )
    end
    if finalXPosType == UIConst.UI_TIPS_X_POS_TYPE.Right then
        if finalYPosType == UIConst.UI_TIPS_Y_POS_TYPE.Bottom then
            self.view.arrowIndicator:SetState("LeftTopState")
        else
            self.view.arrowIndicator:SetState("LeftDownState")
        end
    else
        if finalYPosType == UIConst.UI_TIPS_Y_POS_TYPE.Bottom then
            self.view.arrowIndicator:SetState("RightTopState")
        else
            self.view.arrowIndicator:SetState("RightDownState")
        end
    end
end

HL.Commit(CommonTitleTipsCtrl)
