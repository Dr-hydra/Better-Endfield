local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

FriendThemeChangeThemeCell = HL.Class('FriendThemeChangeThemeCell', UIWidgetBase)

FriendThemeChangeThemeCell.m_onClick = HL.Field(HL.Function)

FriendThemeChangeThemeCell.m_id = HL.Field(HL.String) << ""


FriendThemeChangeThemeCell._OnFirstTimeInit = HL.Override() << function(self)
    self.view.themeBtn.onClick:RemoveAllListeners()
    self.view.themeBtn.onClick:AddListener(function()
        GameInstance.player.friendSystem:ReadBusinessCardUnlockRedDot(CS.Beyond.Gameplay.FriendBusinessCardUnlockType.BusinessCardTopic, self.m_id)
        if self.m_onClick then
            self.m_onClick()
        end
    end)
end

FriendThemeChangeThemeCell.InitFriendThemeChangeThemeCell = HL.Method(Cfg.Types.BusinessCardTopicData, HL.Function, HL.Boolean, HL.Boolean) << function(self, cfg, onClick, selected, unlocked)
    self:_FirstTimeInit()

    self.m_onClick = onClick

    self.m_id = cfg.id

    self.view.themeBtnStateController:SetState(unlocked and 'Unlock' or 'Lock')
    self.view.themeBtnStateController:SetState(selected and 'Select' or 'UnSelect')

    self.view.themeIconImg:LoadSprite(string.format('%s/%s', UIConst.UI_SPRITE_THEME_BG, cfg.icon))
    self.view.themeBgImg:LoadSprite(string.format('%s/%s', UIConst.UI_SPRITE_THEME_BG, cfg.id))

    self.view.redDot:InitRedDot("NewBusinessCard", cfg.id, nil, self:GetUICtrl().view.redDotScrollRect)
end

HL.Commit(FriendThemeChangeThemeCell)
return FriendThemeChangeThemeCell

