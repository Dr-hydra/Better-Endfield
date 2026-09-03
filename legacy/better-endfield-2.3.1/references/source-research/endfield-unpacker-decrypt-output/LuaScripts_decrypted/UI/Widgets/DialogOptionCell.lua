local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

DialogOptionCell = HL.Class('DialogOptionCell', UIWidgetBase)

DialogOptionCell.info = HL.Field(HL.Table)

DialogOptionCell.optionOnClickFunc = HL.Field(HL.Function)


DialogOptionCell._OnFirstTimeInit = HL.Override() << function(self)
end

DialogOptionCell.InitDialogOptionCell = HL.Method(HL.Any, HL.Opt(HL.Function)) << function(self, info, onClick)
    self:_FirstTimeInit()
    self.info = info
    self.view.textDes:SetAndResolveTextStyle(UIUtils.resolveTextCinematic(info.text))
    self.view.imageIcon:LoadSprite(UIConst.UI_SPRITE_DIALOG_OPTION_ICON, info.icon)

    local selectedOptions = GameWorld.dialogManager.selectedOptions
    local optionId = info.optionId
    local setGreyed = info.setGreyed
    local iconTypeLower = self.info.iconType and self.info.iconType:lower()
    if setGreyed or not string.isEmpty(optionId) and selectedOptions:ContainsKey(optionId) and selectedOptions:get_Item(optionId).selectedFlag then
        self.view.imageIcon.color = self.view.config.SELECTED_COLOR
        self.view.textDes.color = self.view.config.SELECTED_COLOR
    elseif self.info.color then
        self.view.imageIcon.color = self.info.color
        self.view.textDes.color = self.info.color
    elseif iconTypeLower == "main" then
        self.view.imageIcon.color = self.view.config.MAINLINE_COLOR
        self.view.textDes.color = self.view.config.MAINLINE_COLOR
    elseif Utils.isInclude(UIConst.DIALOG_OPTION_ENHANCE_COLOR_ICON_TYPE, iconTypeLower) then
        self.view.imageIcon.color = self.view.config.ENHANCE_COLOR
        self.view.textDes.color = self.view.config.ENHANCE_COLOR
    else
        self.view.imageIcon.color = self.view.config.NORMAL_COLOR
        self.view.textDes.color = self.view.config.NORMAL_COLOR
    end


    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        if onClick then
            onClick()
        end
    end)

    if DeviceInfo.usingKeyboard then
        self.view.button.onClick:ChangeBindingPlayerAction("dialog_option_" .. info.index)
    end






    self.optionOnClickFunc = onClick
end

HL.Commit(DialogOptionCell)
return DialogOptionCell
