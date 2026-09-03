local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharFormationHeadCellLight = HL.Class('CharFormationHeadCellLight', UIWidgetBase)

CharFormationHeadCellLight.info = HL.Field(HL.Table)

CharFormationHeadCellLight.exInfo = HL.Field(HL.Table)

CharFormationHeadCellLight.charInfo = HL.Field(HL.Userdata)

CharFormationHeadCellLight.characterData = HL.Field(HL.Userdata)

CharFormationHeadCellLight.m_charPresetData = HL.Field(HL.Userdata)


CharFormationHeadCellLight._OnFirstTimeInit = HL.Override() << function(self)

end

CharFormationHeadCellLight.InitCharFormationHeadCell = HL.Method(HL.Table, HL.Opt(HL.Function, HL.Boolean, HL.Boolean)) << function(self, info, onClick, ignoreDead, needHover)
    self:_FirstTimeInit()

    self:_InitData(info)
    self:_InitCharBaseInfo()
    self:RefreshCharInfo()

    self.view.button.onClick:RemoveAllListeners()
    self.view.button.onClick:AddListener(function()
        if onClick then
            onClick()
        end
    end)

    self.view.button.onHoverChange:RemoveAllListeners()
    if needHover == true then
        self.view.button.onHoverChange:AddListener(function(isHover)
            if isHover then
                Notify(MessageConst.SHOW_COMMON_HOVER_TIP, {
                    mainText = self.characterData.name,
                    subText = Tables.charProfessionTable[self.characterData.profession].name,
                    delay = self.view.config.HOVER_DELAY,
                    targetRect = self.view.transform,
                    rarity = self.characterData.rarity,
                })
            else



                if not DeviceInfo.usingController or InputManagerInst.controllerNaviManager:IsNaviTarget(self.view.button) then
                    Notify(MessageConst.HIDE_COMMON_HOVER_TIP)
                end
            end
        end)
    end
end

CharFormationHeadCellLight.RefreshExInfo = HL.Method(HL.Table) << function(self, exInfo)
    self.exInfo = exInfo
end

CharFormationHeadCellLight._InitData = HL.Method(HL.Table) << function(self, info)
    self.info = info
    if not string.isEmpty(info.charPresetId) then
        self.m_charPresetData = Tables.charPresetTable:GetValue(info.charPresetId)
        self.info.templateId = CS.Beyond.Gameplay.CharUtils.GetCharTemplateId(self.m_charPresetData.charId)
    end
    self.characterData = Tables.characterTable:GetValue(self.info.templateId)
    local instId = info.instId
    if instId and instId > 0 then
        self.charInfo = CharInfoUtils.getPlayerCharInfoByInstId(instId)
    end
end

CharFormationHeadCellLight._InitCharBaseInfo = HL.Method() << function(self)
    local spriteName = UIConst.UI_CHAR_HEAD_SQUARE_PREFIX .. self.info.templateId
    self.view.imageChar:LoadSprite(UIConst.UI_SPRITE_CHAR_HEAD_RECTANGLE, spriteName)

    self.view.charElementIcon:InitCharTypeIcon(self.characterData.charTypeId)

    local proSpriteName = CharInfoUtils.getCharProfessionIconName(self.characterData.profession, true)
    self.view.imagePro:LoadSprite(UIConst.UI_SPRITE_CHAR_PROFESSION, proSpriteName)

    local rarityColor = UIUtils.getCharRarityColor(self.characterData.rarity)
    if rarityColor then
        self.view.rarityColor.color = rarityColor
    end

    self.gameObject.name = "CharHeadCell_" .. self.info.templateId
end

CharFormationHeadCellLight.RefreshCharInfo = HL.Method() << function(self)
    if self.charInfo then
        self.view.textLv.text = string.format("%02d", self.charInfo.level)
        self.view.textLv.gameObject:SetActive(true)
        if self.charInfo.potentialLevel then
            self.view.simplePotentialStar:InitWeaponSimplePotentialStar(self.charInfo.potentialLevel)
        else
            self.view.simplePotentialStar:InitCharSimplePotentialStar(self.charInfo.instId)
        end
    elseif self.m_charPresetData then
        self.view.textLv.text = string.format("%02d", self.m_charPresetData.charLv)
        self.view.textLv.gameObject:SetActive(true)
        self.view.simplePotentialStar:InitWeaponSimplePotentialStar(self.m_charPresetData.potentialLevel)
    elseif self.info then
        self.view.textLv.text = string.format("%02d", self.info.level)
        self.view.textLv.gameObject:SetActive(true)
        if self.info.potentialLevel then
            self.view.simplePotentialStar:InitWeaponSimplePotentialStar(self.info.potentialLevel)
        end
    else
        self.view.textLv.gameObject:SetActive(false)
    end
end

HL.Commit(CharFormationHeadCellLight)
return CharFormationHeadCellLight