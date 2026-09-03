local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

PageController = HL.Class('PageController', UIWidgetBase)

PageController.curIndex = HL.Field(HL.Int) << 0

PageController.m_pageCount = HL.Field(HL.Int) << 0

PageController.m_onMovePage = HL.Field(HL.Function)

PageController.m_imageIndexCache = HL.Field(HL.Forward("UIListCache"))



PageController._OnFirstTimeInit = HL.Override() << function(self)

    self.view.leftButton.onClick:AddListener(function()
        self:_MovePage(-1)
    end)
    self.view.rightButton.onClick:AddListener(function()
        self:_MovePage(1)
    end)

    self.m_imageIndexCache = UIUtils.genCellCache(self.view.indexToggle)


end

PageController.InitPageController = HL.Method(HL.Number, HL.Function, HL.Opt(HL.Int)) << function(self,pageCount,onMovePage,defaultIndex)
    self:_FirstTimeInit()

    self.m_pageCount = pageCount
    self.m_onMovePage = onMovePage

    self.m_imageIndexCache:Refresh(pageCount)
    if defaultIndex then
        self:MoveToPage(defaultIndex)
    else
        self:MoveToPage(1)
    end

end

PageController._MovePage = HL.Method(HL.Int) << function(self,deltaPage)
    local newPageIndex = self.curIndex + deltaPage
    self:MoveToPage(newPageIndex)
end

PageController.MoveToPage = HL.Method(HL.Int) << function(self,index)
    local pageCount = self.m_pageCount
    if index > pageCount then
        index = pageCount
    elseif index < 1 then
        index = 1
    end
    if index ~= self.curIndex then
        self.curIndex = index

        self.view.leftButton.interactable = index > 1
        self.view.rightButton.interactable = index < pageCount
        local cell = self.m_imageIndexCache:GetItem(index)
        cell.toggle.isOn = true
        if self.m_onMovePage then
            self.m_onMovePage(index)
        end
    end
end

PageController.SetPage = HL.Method(HL.Int) << function(self,index)
    local cell = self.m_imageIndexCache:GetItem(index)
    cell.toggle.isOn = true
    self.curIndex = index
    local isLast = index == self.m_pageCount
    self.view.leftButton.interactable = index > 1
    self.view.rightButton.interactable = not isLast and self.m_pageCount > 1
end

HL.Commit(PageController)
return PageController

