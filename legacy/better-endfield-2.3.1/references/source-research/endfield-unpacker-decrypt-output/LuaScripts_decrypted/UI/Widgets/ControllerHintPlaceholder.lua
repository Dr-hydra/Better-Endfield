local PlaceholderBaseWidget = require_ex('UI/Widgets/PlaceholderBaseWidget')

ControllerHintPlaceholder = HL.Class('ControllerHintPlaceholder', PlaceholderBaseWidget)


ControllerHintPlaceholder.m_groupIds = HL.Field(HL.Table)

ControllerHintPlaceholder.m_optionalActionIds = HL.Field(HL.Table)

ControllerHintPlaceholder.m_customGetKeyHintInfos = HL.Field(HL.Function)


ControllerHintPlaceholder.InitControllerHintPlaceholder = HL.Method(HL.Table, HL.Opt(HL.Table, HL.Function))
        << function(self, groupIds, optionalActionIds, customGetKeyHintInfos)
    self:_InitPlaceholder({
        groupIds = groupIds,
        optionalActionIds = optionalActionIds,
        customGetKeyHintInfos = customGetKeyHintInfos,
    })
end

ControllerHintPlaceholder._InitPlaceholder = HL.Override(HL.Opt(HL.Table)) << function(self, args)
    self.m_playAnimationOutMsg = MessageConst.PLAY_CONTROLLER_HINT_OUT_ANIM
    self.m_showMsg = MessageConst.SHOW_CONTROLLER_HINT
    self.m_hideMsg = MessageConst.HIDE_CONTROLLER_HINT
    self.m_groupIds = args.groupIds
    self.m_optionalActionIds = args.optionalActionIds
    self.m_customGetKeyHintInfos = args.customGetKeyHintInfos

    ControllerHintPlaceholder.Super._InitPlaceholder(self, args)
end

ControllerHintPlaceholder.GetArgs = HL.Override().Return(HL.Table) << function(self)
    return {
        panelId = self.m_panelId,
        placeHolderObject = self.gameObject,
        groupIds = self.m_groupIds,
        optionalActionIds = self.m_optionalActionIds,
        isMain = self.config.IS_MAIN,
        offset = self.config.PANEL_ORDER_OFFSET,
        transform = self.view.transform,
        rectTransform = self.view.rectTransform,
        posType = self.view.config.POS_TYPE,
        useBG = self.view.config.USE_BG,
        useFullBG = self.view.config.USE_FULL_BG,
        customWidth = self.view.config.CUSTOM_WIDTH,
        customGetKeyHintInfos = self.m_customGetKeyHintInfos,
    }
end

ControllerHintPlaceholder.GetHideArgs = HL.Override().Return(HL.Any) << function(self)
    return {
        panelId = self.m_panelId,
        placeHolderObject = self.gameObject,
    }
end

ControllerHintPlaceholder.PlayAnimationOut = HL.Override() << function(self)
    if not self.config.IS_MAIN then
        return
    end
    ControllerHintPlaceholder.Super.PlayAnimationOut(self)
end

HL.Commit(ControllerHintPlaceholder)
return ControllerHintPlaceholder
