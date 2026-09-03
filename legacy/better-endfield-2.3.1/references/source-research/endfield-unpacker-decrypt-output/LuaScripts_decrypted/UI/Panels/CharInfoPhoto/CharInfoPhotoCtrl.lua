
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.CharInfoPhoto

CharInfoPhotoCtrl = HL.Class('CharInfoPhotoCtrl', uiCtrl.UICtrl)






CharInfoPhotoCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.ON_CHAR_SET_POTENTIAL_CG] = '_OnCharSetPotentialCg',
}









CharInfoPhotoCtrl.m_arg = HL.Field(HL.Table)

CharInfoPhotoCtrl.m_isFlipped = HL.Field(HL.Boolean) << false

CharInfoPhotoCtrl.m_pictureIds = HL.Field(HL.Table)

CharInfoPhotoCtrl.m_curPicIndex = HL.Field(HL.Number) << 0

CharInfoPhotoCtrl.m_curPicId = HL.Field(HL.String) << ""

CharInfoPhotoCtrl.m_charInfo = HL.Field(CS.Beyond.Gameplay.CharInfo)



CharInfoPhotoCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_arg = arg
    self:_InitAction()

    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(self.m_arg.charInstId)
    if charInfo == nil then
        logger.error("角色实例不存在:"..tostring(self.m_arg.charInstId))
        return
    end
    self.m_charInfo = charInfo

    local _, potentialList = Tables.characterPotentialTable:TryGetValue(charInfo.templateId)
    if not potentialList then
        logger.error("角色潜能数据不存在:"..charInfo.templateId)
        return
    end
    local potentialLevel = self.m_arg.potentialLevel
    if potentialLevel > #potentialList.potentialUnlockBundle then
        logger.error("角色潜能等级不存在:"..charInfo.templateId..","..tostring(potentialLevel))
        return
    end
    local potentialData = potentialList.potentialUnlockBundle[CSIndex(potentialLevel)]
    self.m_pictureIds = {}
    local _, showPictureId = charInfo.potentialCgIds:TryGetValue(self.m_arg.potentialLevel)
    if showPictureId then
        table.insert(self.m_pictureIds, showPictureId)
    end
    for _, itemId in pairs(potentialData.unlockCharPictureItemList) do
        local _, pictureId = Tables.pictureItemTable:TryGetValue(itemId)
        local isShowPicture = showPictureId and pictureId == showPictureId
        if pictureId and not isShowPicture then
            table.insert(self.m_pictureIds, pictureId)
        end
    end

    local pictureId = self.m_arg.pictureId
    if string.isEmpty(pictureId) then
        pictureId = showPictureId
        if string.isEmpty(pictureId) then
            pictureId = self.m_pictureIds[1]
        end
    end

    local pictureIndex = 0
    for index, id in pairs(self.m_pictureIds) do
        if id == pictureId then
            pictureIndex = index
            break
        end
    end
    local hasMultiPhoto = #self.m_pictureIds > 1
    self.view.leftArrow.gameObject:SetActive(hasMultiPhoto)
    self.view.rightArrow.gameObject:SetActive(hasMultiPhoto)
    self.view.touchPanel.enabled = hasMultiPhoto
    self.view.photoFlipNode.gameObject:SetActive(false)
    self:_RefreshPicture(pictureIndex)

    if self.m_arg.isFlipped then
        self.view.animWrapper:SkipInAnimation()
        self:Flip(true)
        self.view.animWrapper:ClearTween(true)
    end
end

CharInfoPhotoCtrl.OnShow = HL.Override() << function(self)
    UIManager:Hide(PanelId.UIDPanel)
end

CharInfoPhotoCtrl.OnHide = HL.Override() << function (self)
    UIManager:Show(PanelId.UIDPanel)
end

CharInfoPhotoCtrl.OnClose = HL.Override() << function (self)
    UIManager:Show(PanelId.UIDPanel)
end

CharInfoPhotoCtrl._InitAction = HL.Method() << function(self)
    self.view.btnClose.onClick:AddListener(function()
        local onClose = self.m_arg.onClose
        self:PlayAnimationOutAndClose()
        if onClose then
            onClose()
        end
    end)
    self.view.btnClose.gameObject:SetActive(true)
    self.view.btnBack.onClick:AddListener(function()
        self:Flip(false)
    end)
    self.view.btnBack.gameObject:SetActive(false)
    self.view.flipBtn.onClick:AddListener(function()
        self:Flip(true)
    end)

    self.view.leftArrow.button.onClick:AddListener(function()
        self:_PreviousPicture()
    end)
    self.view.rightArrow.button.onClick:AddListener(function()
        self:_NextPicture()
    end)
    self.view.touchPanel.onDragToLeft:AddListener(function()
        self:_NextPicture()
    end)
    self.view.touchPanel.onDragToRight:AddListener(function()
        self:_PreviousPicture()
    end)

    self.view.btnSet.onClick:AddListener(function()
        local pictureId = self.m_pictureIds[self.m_curPicIndex]
        GameInstance.player.charBag:SetCharPotentialPicture(self.m_arg.charInstId, self.m_arg.potentialLevel, pictureId)
    end)
    self:_InitController()
end

CharInfoPhotoCtrl._PreviousPicture = HL.Method() << function(self)
    local newIndex = self.m_curPicIndex - 1
    if newIndex < 1 then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_FIRST_PHOTO)
        return
    end
    self:_RefreshPicture(newIndex)
end

CharInfoPhotoCtrl._NextPicture = HL.Method() << function(self)
    local newIndex = self.m_curPicIndex + 1
    if newIndex > #self.m_pictureIds then
        Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_LAST_PHOTO)
        return
    end
    self:_RefreshPicture(newIndex)
end

CharInfoPhotoCtrl._RefreshPicture = HL.Method(HL.Number) << function(self, pictureIndex)
    if pictureIndex < 1 or pictureIndex > #self.m_pictureIds then
        logger.error("立绘索引越界:"..tostring(pictureIndex))
        return
    end
    self.m_curPicIndex = pictureIndex
    local pictureId = self.m_pictureIds[pictureIndex]
    local _, posterData = Tables.pictureTable:TryGetValue(pictureId)
    if not posterData then
        logger.error("立绘数据不存在:"..pictureId)
        return
    end
    self.m_curPicId = pictureId

    if not GameInstance.player.charBag:IsCharPotentialPictureRead(pictureId) then
        GameInstance.player.charBag:SetCharPotentialPictureRead({ pictureId })
    end

    self.view.leftArrow.stateController:SetState(pictureIndex == 1 and 'Grey' or 'Normal')
    self.view.rightArrow.stateController:SetState(pictureIndex == #self.m_pictureIds and 'Grey' or 'Normal')
    InputManagerInst:ForceBindingKeyhintToGray(self.view.leftArrow.button.onClick.bindingId, pictureIndex == 1)
    InputManagerInst:ForceBindingKeyhintToGray(self.view.rightArrow.button.onClick.bindingId, pictureIndex == #self.m_pictureIds)

    local leftUnRead = false
    for i = 1, pictureIndex - 1 do
        local picId = self.m_pictureIds[i]
        if not GameInstance.player.charBag:IsCharPotentialPictureRead(picId) then
            leftUnRead = true
            break
        end
    end
    self.view.leftArrow.redDot.gameObject:SetActive(leftUnRead)
    local rightUnRead = false
    for i = pictureIndex + 1, #self.m_pictureIds do
        local picId = self.m_pictureIds[i]
        if not GameInstance.player.charBag:IsCharPotentialPictureRead(picId) then
            rightUnRead = true
            break
        end
    end
    self.view.rightArrow.redDot.gameObject:SetActive(rightUnRead)

    local _, defaultPictureId = self.m_charInfo.potentialCgIds:TryGetValue(self.m_arg.potentialLevel)
    if not defaultPictureId then
        defaultPictureId = self.m_pictureIds[1]
    end
    local hasMultiPicture = #self.m_pictureIds > 1
    self.view.setDisplayNode.gameObject:SetActive(pictureId ~= defaultPictureId and hasMultiPicture)
    self.view.currentDisplayNode.gameObject:SetActive(pictureId == defaultPictureId and hasMultiPicture)

    self.view.picture.texture = self.loader:LoadTexture(string.format(UIConst.POSTER_TEXTURE_PATH, posterData.imgId))
    self.view.txtPhotoName.text = posterData.name
    self.view.txtAuthorName.text = posterData.author
    self.view.authorNameNode.gameObject:SetActive(not string.isEmpty(posterData.author))
    local msg = ''
    local _, pictureData = Tables.pictureTable:TryGetValue(pictureId)
    if pictureData then
        local _, itemData = Tables.itemTable:TryGetValue(pictureData.unlockCharPictureItem)
        if itemData then
            msg = itemData.decoDesc
        end
    end
    self.view.txtMsg.text = msg
end

CharInfoPhotoCtrl.Flip = HL.Method(HL.Boolean) << function(self, isFlipped)
    self.view.luaPanel:BlockAllInput()
    local animName = isFlipped and 'charinfo_photo_flip_in' or 'charinfo_photo_flip_out'
    self.view.arrowNode.gameObject:SetActive(false)
    self.view.animWrapper:Play(animName, function()
        self.view.luaPanel:RecoverAllInput()
        self.m_isFlipped = isFlipped
        self.view.btnBack.gameObject:SetActive(isFlipped)
        self.view.btnClose.gameObject:SetActive(not isFlipped)
        self.view.arrowNode.gameObject:SetActive(not isFlipped)
        self.view.bottomBgNode.gameObject:SetActive(not isFlipped)
    end)
    AudioAdapter.PostEvent("Au_UI_Event_PhotoFlip")
end

CharInfoPhotoCtrl._OnCharSetPotentialCg = HL.Method(HL.Table) << function(self, args)
    local charInstId, potentialLevel, pictureId = unpack(args)
    if charInstId ~= self.m_arg.charInstId or potentialLevel ~= self.m_arg.potentialLevel then
        return
    end
    Notify(MessageConst.SHOW_TOAST, Language.LUA_CHAR_PHOTO_SET)
    self:_RefreshPicture(self.m_curPicIndex)
end



CharInfoPhotoCtrl._InitController = HL.Method() << function(self)
    self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({self.view.inputGroup.groupId})
end

CharInfoPhotoCtrl.GetCurStateArg = HL.Method().Return(HL.Table) << function(self)

    local arg = lume.deepCopy(self.m_arg)
    arg.pictureId = self.m_curPicId
    arg.isFlipped = self.m_isFlipped
    arg.onClose = nil
    return arg
end



HL.Commit(CharInfoPhotoCtrl)
