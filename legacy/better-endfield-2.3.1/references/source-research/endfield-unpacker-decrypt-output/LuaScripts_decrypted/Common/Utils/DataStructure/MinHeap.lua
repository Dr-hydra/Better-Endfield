


MinHeapNode = HL.Class('MinHeapNode')

MinHeapNode.key = HL.Field(HL.Any)

MinHeapNode.value = HL.Field(HL.Number) << -1

MinHeapNode.index = HL.Field(HL.Number) << -1


MinHeapNode.Init = HL.Method(HL.Any, HL.Number, HL.Number) << function(self, key, value, index)
    self.key = key
    self.value = value
    self.index = index
end

MinHeapNode.Clear = HL.Method() << function(self)
    self.key = nil
    self.value = -1
    self.index = -1
end

HL.Commit(MinHeapNode)




MinHeap = HL.Class('MinHeap')

MinHeap.m_nodeList = HL.Field(HL.Table)

MinHeap.m_nodeMap = HL.Field(HL.Table)

MinHeap.m_count = HL.Field(HL.Number) << 0

MinHeap.m_nodeCache = HL.Field(HL.Forward("Stack"))


MinHeap.MinHeap = HL.Constructor() << function(self)
    self.m_nodeList = {}
    self.m_nodeMap = {}
    self.m_nodeCache = require_ex("Common/Utils/DataStructure/Stack")()
end

MinHeap._Swap = HL.Method(HL.Number, HL.Number) << function(self, index1, index2)
    if index1 == index2 then
        return
    end
    local tmp = self.m_nodeList[index1]
    self.m_nodeList[index1] = self.m_nodeList[index2]
    self.m_nodeList[index1].index = index1
    self.m_nodeMap[self.m_nodeList[index1].key] = self.m_nodeList[index1]
    self.m_nodeList[index2] = tmp
    self.m_nodeList[index2].index = index2
    self.m_nodeMap[self.m_nodeList[index2].key] = self.m_nodeList[index2]
end

MinHeap._AdjustUp = HL.Method(HL.Number) << function(self, index)
    local parent = math.floor(index / 2)
    if parent == 0 then
        return
    end
    if self.m_nodeList[index].value < self.m_nodeList[parent].value then
        self:_Swap(index, parent)
        self:_AdjustUp(parent)
    end
end

MinHeap._AdjustDown = HL.Method(HL.Number) << function(self, index)
    local size = self:Size()
    local minIndex = index
    if index * 2 <= size and self.m_nodeList[minIndex].value > self.m_nodeList[index * 2].value then
        minIndex = index * 2
    end
    if index * 2 + 1 <= size and self.m_nodeList[minIndex].value > self.m_nodeList[index * 2 + 1].value then
        minIndex = index * 2 + 1
    end
    if index ~= minIndex then
        self:_Swap(index, minIndex)
        self:_AdjustDown(minIndex)
    end
end

MinHeap.Size = HL.Method().Return(HL.Number) << function(self)
    return self.m_count
end

MinHeap.Add = HL.Method(HL.Any, HL.Number) << function(self, key, value)
    if self.m_nodeMap[key] then
        logger.error("MinHeap:Add same key", key, value)
        return
    end
    local index = self:Size() + 1
    local minHeapNode = self:_GetNode()
    minHeapNode:Init(key, value, index)
    minHeapNode.index = index
    self.m_nodeList[index] = minHeapNode
    self.m_nodeMap[minHeapNode.key] = minHeapNode
    self.m_count = self.m_count + 1
    self:_AdjustUp(self:Size())
end

MinHeap.Remove = HL.Method(HL.Any) << function(self, key)
    local node = self.m_nodeMap[key]
    if node then
        local removeIndex = node.index
        local size = self:Size()

        local oldValue = node.value
        self:_Swap(removeIndex, size)
        self.m_nodeMap[key] = nil
        self.m_nodeList[size] = nil
        self.m_count = self.m_count - 1
        self:_CacheNode(node)

        if size ~= removeIndex then
            if oldValue < self.m_nodeList[removeIndex].value then
                self:_AdjustDown(removeIndex)
            elseif oldValue > self.m_nodeList[removeIndex].value then
                self:_AdjustUp(removeIndex)
            end
        end
    end
end

MinHeap.UpdateValue = HL.Method(HL.Any, HL.Number) << function(self, key, newValue)
    if not self.m_nodeMap[key] then
        logger.error("MinHeap:UpdateValue obj not exist " .. tostring(key))
        return
    end
    local node = self.m_nodeMap[key]
    local oldValue = node.value
    node.value = newValue
    if newValue > oldValue then
        self:_AdjustDown(node.index)
    elseif newValue < oldValue then
        self:_AdjustUp(node.index)
    end
end

MinHeap.Min = HL.Method().Return(HL.Opt(HL.Any, HL.Number)) << function(self)
    if self:Size() > 0 then
        return self.m_nodeList[1].key, self.m_nodeList[1].value
    end
    return nil
end

MinHeap.Pop = HL.Method().Return(HL.Opt(HL.Any, HL.Number)) << function(self)
    local size = self:Size()
    if size > 0 then
        local minKey = self.m_nodeList[1].key
        local minValue = self.m_nodeList[1].value
        if size > 1 then
            self:_Swap(1, size)
            local node = self.m_nodeList[size]
            self.m_nodeMap[node.key] = nil
            self.m_nodeList[size] = nil
            self.m_count = self.m_count - 1
            self:_CacheNode(node)
            self:_AdjustDown(1)
        else
            local node = self.m_nodeList[size]
            self.m_nodeMap[node.key] = nil
            self.m_nodeList[size] = nil
            self.m_count = self.m_count - 1
            self:_CacheNode(node)
        end
        return minKey, minValue
    end
    logger.error("MinHeap:Pop empty")
    return nil
end

MinHeap.Peek = HL.Method().Return(HL.Opt(HL.Any, HL.Number)) << function(self)
    local size = self:Size()
    if size > 0 then
        local min = self.m_nodeList[1]
        return min.key, min.value
    end
    logger.error("MinHeap:Pop empty")
    return nil
end

MinHeap.GetValue = HL.Method(HL.Any).Return(HL.Opt(HL.Any)) << function(self, key)
    return self.m_nodeMap[key] and self.m_nodeMap[key].value or nil
end

MinHeap.Find = HL.Method(HL.Any).Return(HL.Boolean) << function(self, key)
    return self.m_nodeMap[key] and true or false
end

MinHeap.NodeIter = HL.Method().Return(HL.Any) << function(self)
    local i = 0
    local keys = {}
    for k, _ in pairs(self.m_nodeMap) do
        table.insert(keys, k)
    end
    return function()
        i = i + 1
        if self.m_nodeMap[keys[i]] then
            return self.m_nodeMap[keys[i]]
        end
    end
end

MinHeap.Clear = HL.Method() << function(self)
    for _, node in pairs(self.m_nodeMap) do
        self:_CacheNode(node)
    end
    self.m_nodeList = {}
    self.m_nodeMap = {}
    self.m_count = 0
end

MinHeap._GetNode = HL.Method().Return(MinHeapNode) << function(self)
    if self.m_nodeCache:Empty() then
        return MinHeapNode()
    else
        return self.m_nodeCache:Pop()
    end
end

MinHeap._CacheNode = HL.Method(MinHeapNode) << function(self, node)
    node:Clear()
    self.m_nodeCache:Push(node)
end


HL.Commit(MinHeap)
return MinHeap
