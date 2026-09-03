
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.SpaceshipCharTips
SpaceshipCharTipsCtrl = HL.Class('SpaceshipCharTipsCtrl', uiCtrl.UICtrl)






SpaceshipCharTipsCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.HIDE_SPACESHIP_CHAR_TIPS] = 'HideSpaceshipCharTips',
}

SpaceshipCharTipsCtrl.m_charId = HL.Field(HL.String) << ''

SpaceshipCharTipsCtrl.m_args = HL.Field(HL.Table)

SpaceshipCharTipsCtrl.m_onClose = HL.Field(HL.Function)


SpaceshipCharTipsCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.content.onTriggerAutoClose:AddListener(function()
        self:HideTips()
    end)
end

SpaceshipCharTipsCtrl.ShowTips = HL.StaticMethod(HL.Table) << function(args)

    local self = UIManager:AutoOpen(PANEL_ID)
    if self.m_onClose then
        self.m_onClose()
    end
    if args.isHover then

        if self.m_args and not self.m_args.isHover then
            return
        end
    end
    if args.onClose then
        self.m_onClose = args.onClose
    end
    if args.blockOtherInput then
        self:ChangeCurPanelBlockSetting(true)
    end

    self:UpdateContent(args)
end

SpaceshipCharTipsCtrl.UpdateContent = HL.Method(HL.Table) << function(self, args)
    if self.m_args and args.key == self.m_args.key then

        self:HideTips()
        return
    end
    self.m_args = args
    local charId = args.charId
    local transform = args.transform
    if args.isHover or args.isSideTips then
        self.view.content.enabled = false
    else
        self.view.content.enabled = true
        self.view.content.tmpSafeArea = args.tmpSafeArea or transform
    end
    self.m_charId = charId

    SpaceshipUtils.updateSSCharInfos(self.view, charId,nil,true)

    if args.ignoreWorkNode then
        self.view.workStateNode.gameObject:SetActive(false)
    end
    self.view.charSkillNode:InitSSCharSkillNode(charId)
    self.view.showKeyHint.gameObject:SetActive(self.view.charSkillNode:GetCanLevelUpState())

    local posType = args.posType or UIConst.UI_TIPS_POS_TYPE.LeftTop
    UIUtils.updateTipsPosition(self.view.content.transform, transform, self.view.rectTransform, self.uiCamera, posType, args.padding)
end

SpaceshipCharTipsCtrl.HideTips = HL.Method() << function(self)
    if self.m_args.blockOtherInput then
        self:ChangeCurPanelBlockSetting(false)
    end
    self:PlayAnimationOutWithCallback(function()
        Notify(MessageConst.HIDE_SPACESHIP_CHAR_TIPS_DONE)
        if self.m_onClose then
            self.m_onClose()
        end
        self.m_args = nil
        self.m_charId = ""
        self.m_onClose = nil
        self:Hide()
    end)
end

SpaceshipCharTipsCtrl.HideSpaceshipCharTips = HL.Method(HL.Opt(HL.Table)) << function(self, arg)
    if not self.m_args then
        return
    end
    arg = arg or {}
    if arg.isHover and not self.m_args.isHover then

        return
    end
    if arg.key and arg.key ~= self.m_args.key then

        return
    end
    self:HideTips()
end


HL.Commit(SpaceshipCharTipsCtrl)
