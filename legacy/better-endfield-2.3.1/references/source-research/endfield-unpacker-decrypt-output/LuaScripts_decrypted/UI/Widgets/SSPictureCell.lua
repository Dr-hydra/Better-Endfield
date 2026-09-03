local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SSPictureCell = HL.Class('SSPictureCell', UIWidgetBase)

SSPictureCell.m_pictureId = HL.Field(HL.String) << ""

SSPictureCell.m_pictureList = HL.Field(HL.Table)

SSPictureCell.m_isNew = HL.Field(HL.Boolean) << true

SSPictureCell.m_onClickFunc = HL.Field(HL.Function)



SSPictureCell._OnFirstTimeInit = HL.Override() << function(self)
    self.view.previewBtn.onClick:AddListener(function()
        self:OpenPicturePanel()
    end)
    self.view.pictureBtn.onClick:AddListener(function()
        if self.m_onClickFunc then
            self.m_onClickFunc()
        end
    end)
    self.view.pictureBtn.onIsNaviTargetChanged = function(isTarget)
        if isTarget then
            Notify(MessageConst.ON_SPACESHIP_HEAD_NAVI_TARGET_CHANGE, self)
        end
    end
end

SSPictureCell.InitSSPictureCell = HL.Method(HL.String, HL.Table, HL.Function) << function(self, pictureId, pictureList, onClick)
    self.m_isNew = true
    self.gameObject.name = pictureId
    self.m_pictureId = pictureId
    self.m_onClickFunc = onClick
    self.m_pictureList = pictureList
    if not GameInstance.player.spaceship:GetPictureRedDotReadState(self.m_pictureId) then
        self:RegisterMessage(MessageConst.ON_READ_NEW_SS_PICTURE, function(arg)
            local picId = arg[1]
            if picId and picId == self.m_pictureId then
                self:UpdateRedDotState(false)
            end
        end)
    else
        self:UpdateRedDotState(false)
    end
    self:_FirstTimeInit()
    local _, posterData = Tables.pictureTable:TryGetValue(self.m_pictureId)
    self.view.picture.texture = self.loader:LoadTexture(string.format(UIConst.POSTER_TEXTURE_SUB_SIZE_PATH, posterData.imgId))
end

SSPictureCell.OpenPicturePanel = HL.Method() << function(self)
    UIManager:AutoOpen(PanelId.ReceptionDisplayPicture, {
        pictureId = self.m_pictureId,
        pictureList = self.m_pictureList
    })
end

SSPictureCell.SelectIndex = HL.Method(HL.Opt(HL.Number)) << function(self, index)
    if index then
        self.view.pictureSelectNode.gameObject:SetActive(true)
        self.view.pictureNumberTxt.text = index
    else
        self.view.pictureSelectNode.gameObject:SetActive(false)
    end
end

SSPictureCell.UpdateRedDotState = HL.Method(HL.Boolean) << function(self, isNew)
    if not self.m_isNew then
        return
    end
    self.m_isNew = isNew
    self.view.redDot.gameObject:SetActive(isNew)
end


HL.Commit(SSPictureCell)
return SSPictureCell

