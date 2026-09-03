local UIWidgetBase = require_ex('Common/Core/UIWidgetBase')

DomainTagNode = HL.Class('DomainTagNode', UIWidgetBase)


DomainTagNode._OnFirstTimeInit = HL.Override() << function(self)
end

DomainTagNode.InitDomainTagNode = HL.Method(HL.Any) << function(self, domainId)
    self:_FirstTimeInit()
    local isEmpty = string.isEmpty(domainId)
    self.view.gameObject:SetActive(not isEmpty)
    if isEmpty then
        return
    end
    self.view.nodeState:SetState(domainId)
end

HL.Commit(DomainTagNode)
return DomainTagNode

