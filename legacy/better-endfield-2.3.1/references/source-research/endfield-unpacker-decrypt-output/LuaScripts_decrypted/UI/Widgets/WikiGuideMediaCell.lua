local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

WikiGuideMediaCell = HL.Class('WikiGuideMediaCell', UIWidgetBase)

local GUIDE_VIDEO_PATH_FORMAT = "Guide/%s"


WikiGuideMediaCell._OnFirstTimeInit = HL.Override() << function(self)
end

WikiGuideMediaCell.InitWikiGuideMediaCell = HL.Method(HL.String) << function(self, wikiTutorialPageId)
    self:_FirstTimeInit()

    local _, pageData = Tables.wikiTutorialPageTable:TryGetValue(wikiTutorialPageId)
    self.view.gameObject:SetActive(pageData ~= nil)
    if not pageData then
        return
    end
    local isImg = pageData.image and not string.isEmpty(pageData.image)
    self.view.image.gameObject:SetActive(isImg)

    local videoGo = self.view.video.gameObject
    if videoGo.activeSelf then
        self.view.video:Stop()
    end
    videoGo:SetActive(not isImg)
    self.view.coroutine = self:_ClearCoroutine(self.view.coroutine)
    if isImg then
        self.view.image:LoadSprite(UIConst.UI_SPRITE_GUIDE, pageData.image)
    else
        local success, file = CS.Beyond.Gameplay.GuideSystem.TryGetGuideMediaVideoFullPathByVideoNameAndDeviceType(
            pageData.video, pageData.videoDeviceType
        )
        if success then
            self.view.video.player:SetFile(nil, file)
            self.view.video.player.applyTargetAlpha = true
            self.view.coroutine = self:_StartCoroutine(function()

                while true do
                    local status = self.view.video.player.status
                    if status == CS.CriWare.CriMana.Player.Status.Stop or status == CS.CriWare.CriMana.Player.Status.Ready then
                        break
                    end
                    coroutine.step()
                end
                self.view.video:Play()
            end)
        else
            self.view.video.gameObject:SetActive(false)
        end
    end
end

HL.Commit(WikiGuideMediaCell)
return WikiGuideMediaCell

