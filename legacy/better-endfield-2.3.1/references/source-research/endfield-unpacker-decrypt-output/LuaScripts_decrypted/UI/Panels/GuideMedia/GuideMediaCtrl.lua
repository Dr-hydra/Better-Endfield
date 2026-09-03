local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.GuideMedia
GuideMediaCtrl = HL.Class('GuideMediaCtrl', uiCtrl.UICtrl)







GuideMediaCtrl.s_messages = HL.StaticField(HL.Table) << {
     [MessageConst.HIDE_GUIDE_MEDIA] = 'HideGuideMedia',
}


GuideMediaCtrl.m_mediaInfos = HL.Field(HL.Userdata)

GuideMediaCtrl.m_getMediaCell = HL.Field(HL.Function)

GuideMediaCtrl.m_onComplete = HL.Field(HL.Function)

GuideMediaCtrl.m_loadedImgKeys = HL.Field(HL.Table)

GuideMediaCtrl.m_isHideMissionHud = HL.Field(HL.Boolean) << false



GuideMediaCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_loadedImgKeys = {}
    local mediaScrollList = self.view.mediaNode.mediaList
    self.view.mediaNode.closeButton.onClick:AddListener(function()
        self:_OnClickClose()
    end)
    self.view.mediaNode.leftButton.onClick:AddListener(function()
        mediaScrollList:ScrollToIndex(mediaScrollList.centerIndex - 1)
    end)
    self.view.mediaNode.rightButton.onClick:AddListener(function()
        mediaScrollList:ScrollToIndex(mediaScrollList.centerIndex + 1)
    end)
    mediaScrollList.onUpdateCell:AddListener(function(obj, csIndex)
        self:_OnUpdateImageCell(obj, csIndex)
    end)
    mediaScrollList.onCenterIndexChanged:AddListener(function(oldIndex, newIndex)
        self:_OnUpdateSelectImageIndex(newIndex)
    end)
    self.m_getMediaCell = UIUtils.genCachedCellFunction(mediaScrollList)

    if DeviceInfo.usingController then
        UIUtils.bindHyperlinkPopup(self, "guideMediaContent", self.view.inputGroup.groupId)
        self.view.controllerHintPlaceholder:InitControllerHintPlaceholder({ self.view.inputGroup.groupId })
    end
end

GuideMediaCtrl.OnShow = HL.Override() << function(self)
    Notify(MessageConst.FORCE_ENABLE_UI_SCENE_BLUR, { key = self.panelId, enabled = true})
    UIManager:HideWithKey(PanelId.MissionHud, "GuideMedia")
    CameraManager:StopTick("GuideMediaCtrl")
end

GuideMediaCtrl.OnHide = HL.Override() << function(self)
    self:_DisposeLoadedGuideImages()
    UIManager:ShowWithKey(PanelId.MissionHud, "GuideMedia")
    Notify(MessageConst.FORCE_ENABLE_UI_SCENE_BLUR, { key = self.panelId, enabled = false})
    CameraManager:ResumeTick("GuideMediaCtrl")
end

GuideMediaCtrl.OnClose = HL.Override() << function(self)
    Notify(MessageConst.FORCE_ENABLE_UI_SCENE_BLUR, { key = self.panelId, enabled = false})
    UIManager:HideWithKey(PanelId.MissionHud, "GuideMedia")
    CameraManager:ResumeTick("GuideMediaCtrl")
end

GuideMediaCtrl.ShowGuideMedia = HL.StaticMethod(HL.Table) << function(args)

    local self = UIManager:AutoOpen(PANEL_ID)
    self.m_mediaInfos = args.mediaInfos
    self.m_onComplete = args.onComplete
    self:_RefreshMedia(self.m_mediaInfos)
    self.view.luaPanel:RecoverAllInput()


    VoiceManager:SetPause(true)
end

GuideMediaCtrl.HideGuideMedia = HL.Method() << function(self)
    if not self:IsShow() then
        return
    end

    if self:IsPlayingAnimationOut() then
        return
    end

    self:PlayAnimationOutWithCallback(function()
        self.m_mediaInfos = nil
        self.m_onComplete = nil
        self:Hide()
    end)


    VoiceManager:SetPause(false)
end


GuideMediaCtrl._OnClickClose = HL.Method() << function(self)
    self.m_onComplete()
end

GuideMediaCtrl._RefreshMedia = HL.Method(HL.Userdata) << function(self, mediaInfos)
    local node = self.view.mediaNode
    local count = mediaInfos.Count
    node.closeButton.gameObject:SetActive(false)
    node.hint.gameObject:SetActive(true)
    node.mediaList:UpdateCount(count, true)
    node.animationWrapper:PlayInAnimation()
end

GuideMediaCtrl._OnUpdateImageCell = HL.Method(GameObject, HL.Number) << function(self, obj, csIndex)
    local info = self.m_mediaInfos[csIndex]
    local cell = self.m_getMediaCell(obj)

    local isImg = info.type == CS.Beyond.Gameplay.GuideMediaInfo.Type.Image
    local imgPath, videoFile
    if isImg then
        imgPath = UIUtils.getSpritePath(UIConst.UI_SPRITE_GUIDE, info.imgPath)
        if not ResourceManager.CheckExists(imgPath) then
            imgPath = UIUtils.getSpritePath(UIConst.UI_SPRITE_GUIDE, "guide_pic_default")
        end
    else
        local success, file = CS.Beyond.Gameplay.GuideSystem.TryGetGuideMediaVideoFullPathByMediaInfo(info)
        if success then
            videoFile = file
        else
            imgPath = UIUtils.getSpritePath(UIConst.UI_SPRITE_GUIDE, "guide_pic_default")
            isImg = true
        end
    end

    cell.image.gameObject:SetActive(isImg)
    cell.video.gameObject:SetActive(not isImg)
    if cell.video.player then
        cell.video:Stop()
    end
    cell.coroutine = self:_ClearCoroutine(cell.coroutine)
    if isImg then
        local sprite, key = self.loader:LoadSprite(imgPath)
        cell.image.sprite = sprite
        table.insert(self.m_loadedImgKeys, key)
        if cell.video.player then
            cell.video.player:SetFile(nil, "")
        end
    else
        cell.video.player:SetFile(nil, videoFile)
        cell.video.player.applyTargetAlpha = true
        cell.coroutine = self:_StartCoroutine(function()

            while true do
                local status = cell.video.player.status
                if status == CS.CriWare.CriMana.Player.Status.Stop or status == CS.CriWare.CriMana.Player.Status.Ready then
                    break
                end
                coroutine.step()
            end
            cell.video:Play()
        end)
    end
end

GuideMediaCtrl._OnUpdateSelectImageIndex = HL.Method(HL.Number) << function(self, newIndex)
    local node = self.view.mediaNode
    local infos = self.m_mediaInfos
    local info = infos[newIndex]
    local titleTxt = Utils.getGuideText(info.titleTxtId)
    local descTxt = Utils.getGuideText(info.descTxtId)
    node.titleTxt:SetAndResolveTextStyle(InputManager.ParseTextActionId(titleTxt))
    node.contentTxt:SetAndResolveTextStyle(InputManager.ParseTextActionId(descTxt))
    node.indexTxt.text = string.format("%d/%d", LuaIndex(newIndex), infos.Count)

    local isLast = newIndex == infos.Count - 1
    node.leftButton.interactable = newIndex > 0
    node.rightButton.interactable = not isLast and infos.Count > 1
    if isLast then
        node.closeButton.gameObject:SetActive(true)
        node.hint.gameObject:SetActive(false)
    end

    Notify(MessageConst.ON_STOP_HOVER_LINK)
end

GuideMediaCtrl._DisposeLoadedGuideImages = HL.Method() << function(self)
    for _, key in ipairs(self.m_loadedImgKeys) do
        self.loader:DisposeHandleByKey(key)
    end
    self.m_loadedImgKeys = {}
end

HL.Commit(GuideMediaCtrl)
