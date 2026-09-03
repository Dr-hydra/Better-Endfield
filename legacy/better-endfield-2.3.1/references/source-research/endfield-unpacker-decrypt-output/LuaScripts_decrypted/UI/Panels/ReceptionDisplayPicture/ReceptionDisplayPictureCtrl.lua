
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.ReceptionDisplayPicture
ReceptionDisplayPictureCtrl = HL.Class('ReceptionDisplayPictureCtrl', uiCtrl.UICtrl)

ReceptionDisplayPictureCtrl.m_charPotentialIndex2Infos = HL.Field(HL.Table)

ReceptionDisplayPictureCtrl.m_charPotentialPicId2Index = HL.Field(HL.Table)

ReceptionDisplayPictureCtrl.m_curIndex = HL.Field(HL.Number) << 1

ReceptionDisplayPictureCtrl.m_getPictureCell = HL.Field(HL.Function)





ReceptionDisplayPictureCtrl.s_messages = HL.StaticField(HL.Table) << {

}


ReceptionDisplayPictureCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.btnClose.onClick:AddListener(function()
        self:PlayAnimationOutWithCallback(function()
            self:Close()
        end)
    end)

    self.view.leftBtn.onClick:AddListener(function()
        self:_OnClickSwitchBtn(true)
    end)

    self.view.rightBtn.onClick:AddListener(function()
        self:_OnClickSwitchBtn(false)
    end)

    if arg then
        self.m_charPotentialIndex2Infos = arg.pictureList
        self.m_charPotentialPicId2Index = {}
        for index, info in ipairs(self.m_charPotentialIndex2Infos) do
            self.m_charPotentialPicId2Index[info.posterData.pictureId] = index
        end
        if #self.m_charPotentialIndex2Infos == 0 then
            logger.error("ReceptionDisplayPicture: 没有已解锁的图片")
        end
        self.m_curIndex = self.m_charPotentialPicId2Index[arg.pictureId]
        GameInstance.player.spaceship:ReadPictureRedDot(arg.pictureId)
    else
        self.m_curIndex = 1
    end
    self.m_getPictureCell = UIUtils.genCachedCellFunction(self.view.pictureLayout)
    self:_RefreshCurPictureView()
    self:_RefreshArrowState()
    self.view.pictureLayout.onUpdateCell:AddListener(function(obj, csIndex)
        self:UpdatePicture(obj, csIndex)
    end)

    self.view.pictureLayout:UpdateCount(#self.m_charPotentialIndex2Infos, true)
    self.view.pictureLayout.onCenterIndexChanged:AddListener(function(oldIndex, newIndex)
        self.m_curIndex = LuaIndex(newIndex)
        self:_RefreshCurPictureView()
        self:_RefreshArrowState()
    end)

    self.view.pictureLayout:ScrollToIndex(CSIndex(self.m_charPotentialPicId2Index[arg.pictureId] or 1), true)

    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
end

ReceptionDisplayPictureCtrl.OnShow = HL.Override() << function(self)

end

ReceptionDisplayPictureCtrl.OnHide = HL.Override() << function(self)

end

ReceptionDisplayPictureCtrl.OnClose = HL.Override() << function(self)

end

ReceptionDisplayPictureCtrl.GetRecoverStateArg = HL.Method().Return(HL.Opt(HL.Any)) << function(self)
    local info = self.m_charPotentialIndex2Infos and self.m_charPotentialIndex2Infos[self.m_curIndex]
    if not info or not info.posterData then
        return nil
    end
    return {
        currentIndex = self.m_curIndex,
        pictureId = info.posterData.pictureId,
    }
end

ReceptionDisplayPictureCtrl._OnClickSwitchBtn = HL.Method(HL.Boolean) <<function(self, isLeft)
    if isLeft then
        self.m_curIndex = self.m_curIndex - 1
        if self.m_curIndex <= 1 then
            self.m_curIndex = 1
        end
    else
        self.m_curIndex = self.m_curIndex + 1
        if self.m_curIndex >= #self.m_charPotentialIndex2Infos then
            self.m_curIndex = #self.m_charPotentialIndex2Infos
        end
    end
    self.view.pictureLayout:ScrollToIndex(CSIndex(self.m_curIndex))
    self:_RefreshArrowState()
end

ReceptionDisplayPictureCtrl._RefreshCurPictureView = HL.Method() << function(self)
    local info = self.m_charPotentialIndex2Infos[self.m_curIndex]
    local charInfo = info.charInfo
    if not info then
        return
    end
    local posterData = info.posterData
    self.view.pictureNameTxt.text = posterData.name
    self.view.outhorNameTxt.gameObject:SetActive(not string.isEmpty(posterData.author))
    self.view.outhorNameTxt.text = posterData.author
    local charData = Tables.characterTable[charInfo.templateId]
    self.view.sourceCharTxt.text = charData.name
    self.view.sourceTxt.text = string.format(Language.LUA_SPACESHIP_PICTURE_POTENTIAL_FORMAT,
        Language["ui_weapon_exhibit_overview_potential"],
        info.photoLevel)
    GameInstance.player.spaceship:ReadPictureRedDot(info.posterData.pictureId)
end


ReceptionDisplayPictureCtrl._RefreshArrowState = HL.Method() << function(self)
    self.view.rightBtnNode.gameObject:SetActive(self.m_curIndex < #self.m_charPotentialIndex2Infos)
    self.view.leftBtnNode.gameObject:SetActive(self.m_curIndex > 1)
end

ReceptionDisplayPictureCtrl.UpdatePicture = HL.Method(GameObject, HL.Number) << function(self, obj, csIndex)
    local info = self.m_charPotentialIndex2Infos[LuaIndex(csIndex)]
    if not info then
        return
    end
    local posterData = info.posterData
    local cell = self.m_getPictureCell(obj)

    local texture = self.loader:LoadTexture(string.format(UIConst.POSTER_TEXTURE_PATH, posterData.imgId))
    cell.picture.texture = texture
end

HL.Commit(ReceptionDisplayPictureCtrl)
