local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharInfoCharCell = HL.Class('CharInfoCharCell', UIWidgetBase)

CharInfoCharCell.info = HL.Field(HL.Table)

CharInfoCharCell.data = HL.Field(HL.Userdata)




CharInfoCharCell._OnFirstTimeInit = HL.Override() << function(self)
end

CharInfoCharCell.InitCharInfoCharCell = HL.Method(HL.Any, HL.Opt(HL.Function)) << function(self, info, onClick)
    self:_FirstTimeInit()

    self.info = info
    local hasData = self.info and self.info.charId
    if hasData then
        local characterTable = Tables.characterTable
        local data = characterTable:GetValue(self.info.charId)
        local charInfo = nil
        local instId = info.instId
        if instId and instId > 0 then
            charInfo = CharInfoUtils.getPlayerCharInfoByInstId(instId)
        end
        self:RefreshCharInfo(charInfo)
        self.data = data
        local spriteName = UIConst.UI_CHAR_HEAD_PREFIX .. self.info.charId
        self.view.imageChar:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD, spriteName)
        self.view.button.onClick:RemoveAllListeners()
        self.view.button.onClick:AddListener(function()
            if onClick then
                onClick()
            end
        end)
    end

    self.view.selectNode.gameObject:SetActive(hasData)
    self.view.imageBlank.gameObject:SetActive(not hasData)
end

CharInfoCharCell.RefreshCharInfo = HL.Method(HL.Userdata) << function(self, charInfo)
    if charInfo then
        self.view.textLevel.text = string.format("%d", charInfo.level)
        self.view.textLevel.gameObject:SetActive(true)
    else
        self.view.textLevel.gameObject:SetActive(false)
    end
end

HL.Commit(CharInfoCharCell)
return CharInfoCharCell
