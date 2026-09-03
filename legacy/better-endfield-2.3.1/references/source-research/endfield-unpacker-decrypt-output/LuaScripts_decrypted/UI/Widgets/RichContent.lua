local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

local CONTENT_TYPE_TEXT = 1
local CONTENT_TYPE_IMAGE = 2

RichContent = HL.Class('RichContent', UIWidgetBase)

RichContent.m_contentCellCache = HL.Field(HL.Forward("UIListCache"))

RichContent._OnCreate = HL.Override() << function(self)
    self.m_contentCellCache = UIUtils.genCellCache(self.view.contentCell)
end













RichContent.SetContentById = HL.Method(HL.String) << function(self, contentId)
    local res, contentData = Tables.richContentTable:TryGetValue(contentId)
    if res then
        local contents = {}
        for _, c in pairs(contentData.contentList) do
            local t = c.content
            table.insert(contents, t)
        end
        self:SetContent({title = contentData.title, subTitle = "", contents = contents})
    else
        logger.error("RichContent.SetContentById error: " .. contentId)
    end
end

RichContent.SetContent = HL.Method(HL.Any) << function(self, args)
    self.view.title:SetAndResolveTextStyle(UIUtils.resolveTextCinematic(args.title or ""))
    self.view.subTitle:SetAndResolveTextStyle(UIUtils.resolveTextCinematic(args.subTitle or ""))
    local contents = args.contents or {}
    self.m_contentCellCache:Refresh(#contents, function(contentCell, luaIdx)
        local t = contents[luaIdx]
        local contentType, v = self:_ParseContent(t)

        if contentType == CONTENT_TYPE_TEXT then
            contentCell.txt.gameObject:SetActive(true)
            contentCell.img.gameObject:SetActive(false)
            contentCell.txt:SetAndResolveTextStyle(UIUtils.resolveTextCinematic(t))
        elseif contentType == CONTENT_TYPE_IMAGE then
            contentCell.txt.gameObject:SetActive(false)
            contentCell.img.gameObject:SetActive(true)
            local path = RichContent._TrimWithZWSP(v.path)
            path = Utils.getImgGenderDiffPath(path)
            contentCell.img:LoadSprite(path)
            local layoutElement = contentCell.img.gameObject:GetComponent(typeof(Unity.UI.LayoutElement))

            local contentLayout = self.view.content.gameObject:GetComponent(typeof(Unity.UI.VerticalLayoutGroup))
            local cellWidth = self.view.content.transform.rect.width - contentLayout.padding.left - contentLayout.padding.right

            local sprite = contentCell.img.sprite
            if sprite then
                local spriteWidth, spriteHeight = sprite.rect.width, sprite.rect.height
                if spriteWidth > cellWidth then
                    layoutElement.preferredHeight = spriteHeight * (cellWidth / spriteWidth)
                else
                    layoutElement.preferredHeight = spriteHeight
                end
            else
                layoutElement.preferredHeight = 100
            end
        end
    end)
end

RichContent._ParseContent = HL.Method(HL.String).Return(HL.Number, HL.Any) << function(self, text)
    local imgPatternIndex = string.find(text, "<image")
    if imgPatternIndex ~= nil then
        text = string.sub(text, imgPatternIndex)
        local imageContentParam = CSUtils.ParseImageContent(text)
        if imageContentParam then
            return CONTENT_TYPE_IMAGE, imageContentParam
        end
    end
    return CONTENT_TYPE_TEXT, text
end

RichContent._TrimWithZWSP = HL.StaticMethod(HL.String).Return(HL.String) << function(str)

    str = string.gsub(str, "^[ \t\n\r​]+", "")
    str = string.gsub(str, "[ \t\n\r​]+$", "")
    return str
end

HL.Commit(RichContent)
return RichContent