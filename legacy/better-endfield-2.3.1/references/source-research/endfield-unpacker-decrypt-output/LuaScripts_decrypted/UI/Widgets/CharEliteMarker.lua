local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

CharEliteMarker = HL.Class('CharEliteMarker', UIWidgetBase)



CharEliteMarker.m_markerQueue = HL.Field(HL.Table)

CharEliteMarker._OnFirstTimeInit = HL.Override() << function(self)
    self.m_markerQueue = {}

    local maxBreakStage = Tables.characterConst.maxBreak

    for i = 0, maxBreakStage do
        local markerName = 'marker' .. i
        local marker = self.view[markerName]
        if marker ~= nil then
            self.m_markerQueue[i] = marker
        end
    end


end

CharEliteMarker.InitCharEliteMarker = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, charInstId, hideMarker)
    self:_FirstTimeInit()

    local charInfo = CharInfoUtils.getPlayerCharInfoByInstId(charInstId)
    local breakStage = charInfo.breakStage

    self:InitCharEliteMarkerByBreakStage(breakStage, hideMarker)
end

CharEliteMarker.InitCharEliteMarkerByBreakStage = HL.Method(HL.Number, HL.Opt(HL.Boolean)) << function(self, breakStage, hideMarker)
    self:_FirstTimeInit()

    local breakStateCount = Tables.characterConst.maxBreak
    for i = 0, breakStateCount do
        local marker = self.m_markerQueue[i]
        marker.gameObject:SetActive(i == breakStage and (not hideMarker))
    end

    self.view.eliteCellGroup:InitEliteCellGroup(breakStage)
end

HL.Commit(CharEliteMarker)
return CharEliteMarker

