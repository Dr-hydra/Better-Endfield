local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

SnapshotSwitchWidget = HL.Class('SnapshotSwitchWidget', UIWidgetBase)


SnapshotSwitchWidget._OnFirstTimeInit = HL.Override() << function(self)
    self.view.nextBtn.onClick:AddListener(function()
        self:_OnSwitchNext()
    end)
    self.view.preBtn.onClick:AddListener(function()
        self:_OnSwitchPre()
    end)
end



SnapshotSwitchWidget.m_curIndex = HL.Field(HL.Number) << -1

SnapshotSwitchWidget.m_nameList = HL.Field(HL.Table)

SnapshotSwitchWidget.m_nameListCount = HL.Field(HL.Number) << 0

SnapshotSwitchWidget.m_selectChangedCallback = HL.Field(HL.Function)



SnapshotSwitchWidget.InitSnapshotSwitchWidget = HL.Method(HL.Table, HL.Number, HL.Function) << function(self, nameList, defaultIndex, onSelectChanged)
    self:_FirstTimeInit()


    self.m_nameList = nameList
    self.m_nameListCount = #nameList
    self.m_selectChangedCallback = onSelectChanged
    self.m_curIndex = lume.clamp(defaultIndex, 1, self.m_nameListCount)

    if self.m_nameListCount <= 0 then
        self.view.switchTxt.text = "ERROR DATA"
        return
    end
    self.view.switchTxt.text = nameList[self.m_curIndex]
end



SnapshotSwitchWidget._OnSwitchNext = HL.Method() << function(self)
    if self.m_nameListCount <= 0 then
        return
    end

    self.m_curIndex = self.m_curIndex % self.m_nameListCount + 1
    self.view.switchTxt.text = self.m_nameList[self.m_curIndex]
    if self.m_selectChangedCallback then
        self.m_selectChangedCallback(self.m_curIndex)
    end
end

SnapshotSwitchWidget._OnSwitchPre = HL.Method() << function(self)
    if self.m_nameListCount <= 0 then
        return
    end

    self.m_curIndex = (self.m_curIndex - 2 + self.m_nameListCount) % self.m_nameListCount + 1
    self.view.switchTxt.text = self.m_nameList[self.m_curIndex]
    if self.m_selectChangedCallback then
        self.m_selectChangedCallback(self.m_curIndex)
    end
end



HL.Commit(SnapshotSwitchWidget)
return SnapshotSwitchWidget

