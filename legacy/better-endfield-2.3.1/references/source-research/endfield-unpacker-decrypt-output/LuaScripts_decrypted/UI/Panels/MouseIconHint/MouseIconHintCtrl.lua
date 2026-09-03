
local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.MouseIconHint
MouseIconHintCtrl = HL.Class('MouseIconHintCtrl', uiCtrl.UICtrl)







MouseIconHintCtrl.s_messages = HL.StaticField(HL.Table) << {
    [MessageConst.CHANGE_MOUSE_ICON_HINT] = 'ChangeMouseIconHint',
    [MessageConst.ON_START_HOVER_LINK] = 'OnStartHoverLink',
    [MessageConst.ON_STOP_HOVER_LINK] = 'OnStopHoverLink',
}

MouseIconHintCtrl.m_iconInfos = HL.Field(HL.Table)

MouseIconHintCtrl.m_nextPriority = HL.Field(HL.Number) << 0



MouseIconHintCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.m_iconInfos = {}
    self:_UpdateIcon()
end

MouseIconHintCtrl.OnClose = HL.Override() << function(self)
end

MouseIconHintCtrl.ChangeMouseIconHint = HL.Method(HL.Table) << function(self, args)
    local name = args.name
    local type = args.type
    if type == UIConst.MOUSE_ICON_HINT.Default then
        self.m_iconInfos[name] = nil
    else
        local oldInfo = self.m_iconInfos[name]
        if oldInfo and oldInfo.type == type then
            return
        end
        self.m_iconInfos[name] = {
            name = name,
            type = type,
            hotspotPos = args.hotspotPos,
            priority = self.m_nextPriority,
        }
        self.m_nextPriority = self.m_nextPriority + 1
    end
    self:_UpdateIcon()
end

MouseIconHintCtrl._UpdateIcon = HL.Method() << function(self)
    local maxPriority = -1
    local maxPriorityInfo
    for _, v in pairs(self.m_iconInfos) do
        if v.priority > maxPriority then
            maxPriority = v.priority
            maxPriorityInfo = v
        end
    end
    local texName
    local hotspotPos
    if maxPriorityInfo then
        texName = maxPriorityInfo.type
        hotspotPos = maxPriorityInfo.hotspotPos or Vector2.zero
    else
        texName = UIConst.MOUSE_ICON_HINT.Default
        hotspotPos = Vector2.zero
    end
    local tex = self.loader:LoadTexture(string.format("Assets/Beyond/DynamicAssets/Gameplay/UI/Textures/Mouse/%s.png", texName))
    Unity.Cursor.SetCursor(tex, hotspotPos, Unity.CursorMode.Auto)
end


MouseIconHintCtrl.OnStartHoverLink = HL.Method(HL.Table) << function(self, arg)
    local linkId = unpack(arg)
    local linkType = UIUtils.resolveLinkTypeFromId(linkId)
    local linkMouseIconType = UIConst.UI_TEXT_LINK_MOUSE_ICON[linkType]

    local hotspotPos = nil
    if linkMouseIconType == UIConst.MOUSE_ICON_HINT.Magnifier then
        hotspotPos = Vector2(16, 16)
    end

    self:ChangeMouseIconHint({
        name = "LinkHover",
        type = linkMouseIconType,
        hotspotPos = hotspotPos,
    })
end

MouseIconHintCtrl.OnStopHoverLink = HL.Method(HL.Opt(HL.Table)) << function(self, arg)
    self:ChangeMouseIconHint({
        name = "LinkHover",
        type = UIConst.MOUSE_ICON_HINT.Default,
    })
end


HL.Commit(MouseIconHintCtrl)
