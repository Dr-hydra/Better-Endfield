local PlaceholderBaseWidget = require_ex('UI/Widgets/PlaceholderBaseWidget')

WalletBarPlaceholder = HL.Class('WalletBarPlaceholder', PlaceholderBaseWidget)

WalletBarPlaceholder.m_moneyInfos = HL.Field(HL.Table)

WalletBarPlaceholder.m_useItemIcon = HL.Field(HL.Boolean) << false

WalletBarPlaceholder.m_showLimit = HL.Field(HL.Boolean) << false

WalletBarPlaceholder.m_stopFocusAfterCLick = HL.Field(HL.Boolean) << false

WalletBarPlaceholder.m_closeCommonPopupAfterClickStamina = HL.Field(HL.Boolean) << false

WalletBarPlaceholder.m_cellPreferredWidths = HL.Field(HL.Table)

WalletBarPlaceholder.InitWalletBarPlaceholder = HL.Method(HL.Table, HL.Opt(HL.Boolean, HL.Boolean, HL.Boolean, HL.Table, HL.Boolean))
        << function(self, moneyIds, useItemIcon, showLimit, closeCommonPopupAfterClickStamina, cellPreferredWidths)
    local moneyInfos = {}
    showLimit = showLimit == true
    for _, moneyId in pairs(moneyIds) do
        table.insert(moneyInfos, {
            moneyId = moneyId,
            showLimit = showLimit,
            limitNumber = nil,
            clearRuleType = GEnums.MoneyClearRuleType.None,
            clearTipsTextKey = "",
        })
    end
    self:InitWalletBarPlaceholderDetailed(moneyInfos, useItemIcon, closeCommonPopupAfterClickStamina, cellPreferredWidths)
end

WalletBarPlaceholder.InitWalletBarPlaceholderDetailed = HL.Method(HL.Table, HL.Opt(HL.Boolean, HL.Boolean, HL.Table))
        << function(self, moneyInfos, useItemIcon, closeCommonPopupAfterClickStamina, cellPreferredWidths)
    self:_InitPlaceholder({
        moneyInfos = moneyInfos,
        useItemIcon = useItemIcon == true,
        closeCommonPopupAfterClickStamina = closeCommonPopupAfterClickStamina == true,
        cellPreferredWidths = cellPreferredWidths,
    })
end


WalletBarPlaceholder._InitPlaceholder = HL.Override(HL.Opt(HL.Table)) << function(self, args)
    self.m_playAnimationOutMsg = MessageConst.PLAY_WALLET_BAR_OUT_ANIM
    self.m_showMsg = MessageConst.SHOW_WALLET_BAR
    self.m_hideMsg = MessageConst.HIDE_WALLET_BAR
    self.m_moneyInfos = args.moneyInfos
    self.m_useItemIcon = args.useItemIcon == true
    self.m_closeCommonPopupAfterClickStamina = args.closeCommonPopupAfterClickStamina == true
    self.m_cellPreferredWidths = args.cellPreferredWidths

    self.m_stopFocusAfterCLick = false
    for _, moneyInfo in pairs(self.m_moneyInfos) do
        if moneyInfo.moneyId == Tables.globalConst.apItemId then
            self.m_stopFocusAfterCLick = true
        end
    end

    self:_RefreshWalletBarControllerFocus()

    WalletBarPlaceholder.Super._InitPlaceholder(self, args)
end

WalletBarPlaceholder._RefreshWalletBarControllerFocus = HL.Method() << function(self)
    if not DeviceInfo.usingController then
        return
    end

    local isOpen, walletBarCtrl = UIManager:IsOpen(PanelId.WalletBar)
    if not isOpen then
        return
    end

    walletBarCtrl:StopFocus()
end

WalletBarPlaceholder.GetArgs = HL.Override().Return(HL.Table) << function(self)
    local trans = DeviceInfo.usingController and self.view.controllerPosition or self.view.transform
    local uiCam = self.luaPanel.uiCamera
    local targetScreenRect = UIUtils.getTransformScreenRect(trans, uiCam)
    local pos = UIUtils.screenPointToUI(Vector2(targetScreenRect.xMax, targetScreenRect.yMin), uiCam, self.luaPanel.transform)
    return {
        panelId = self.m_panelId,
        moneyInfos = self.m_moneyInfos,

        offset = self.config.PANEL_ORDER_OFFSET,
        paddingRight = self.luaPanel.transform.rect.width / 2 - pos.x,
        paddingTop = self.luaPanel.transform.rect.height / 2 + pos.y,

        useMoneyCellAction = self.config.USE_ACTION,
        useItemIcon = self.m_useItemIcon,
        useLightTextColor = self.config.USE_LIGHT_TEXT_COLOR,
        showLimit = self.m_showLimit,
        focusAfterCLick = self.m_stopFocusAfterCLick,
        closeCommonPopupAfterClickStamina = self.m_closeCommonPopupAfterClickStamina,
        cellPreferredWidths = self.m_cellPreferredWidths,
    }
end

HL.Commit(WalletBarPlaceholder)
return WalletBarPlaceholder
