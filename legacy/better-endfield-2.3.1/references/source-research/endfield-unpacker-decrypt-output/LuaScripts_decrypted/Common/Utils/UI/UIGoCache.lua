UIGoCache = HL.Class('UIGoCache')

UIGoCache.m_freeList = HL.Field(HL.Table)

UIGoCache.m_usedList = HL.Field(HL.Table)

UIGoCache.m_parent = HL.Field(HL.Userdata)

UIGoCache.m_wrapFunction = HL.Field(HL.Function)

UIGoCache.m_goTemplate = HL.Field(HL.Any)

UIGoCache.UIGoCache = HL.Constructor(HL.Any, HL.Opt(HL.Function, HL.Any)) << function(self, goTemplate, wrapFunction, parent)
    self.m_freeList = {}
    self.m_usedList = {}
    self.m_goTemplate = goTemplate
    self.m_wrapFunction = wrapFunction or Utils.wrapLuaNode
    self.m_parent = parent and parent.transform or goTemplate.transform.parent
    self.m_goTemplate.gameObject:SetActive(false)
end

UIGoCache.Get = HL.Method().Return(HL.Any) << function(self)
    if #self.m_freeList > 0 then
        local cell = table.remove(self.m_freeList)
        cell.gameObject:SetActive(true)
        table.insert(self.m_usedList, cell)
        return cell
    end
    local go = GameObject.Instantiate(self.m_goTemplate.gameObject, self.m_parent)
    go:SetActive(true)
    local cell = self.m_wrapFunction(go)
    table.insert(self.m_usedList, cell)
    return cell
end

UIGoCache.Recycle = HL.Method(HL.Any) << function(self, cell)
    local index = lume.find(self.m_usedList, cell)
    if index then
        table.remove(self.m_usedList, index)
        cell.gameObject:SetActive(false)
        table.insert(self.m_freeList, cell)
    end
end

UIGoCache.RecycleAll = HL.Method() << function(self)
    for i, cell in ipairs(self.m_usedList) do
        cell.gameObject:SetActive(false)
        table.insert(self.m_freeList, cell)
    end
    lume.clear(self.m_usedList)
end

HL.Commit(UIGoCache)