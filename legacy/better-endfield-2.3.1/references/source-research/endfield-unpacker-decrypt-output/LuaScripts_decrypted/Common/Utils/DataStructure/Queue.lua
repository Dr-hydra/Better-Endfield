
Queue = HL.Class('Queue')

Queue.m_data = HL.Field(HL.Table)

Queue.m_head = HL.Field(HL.Number) << -1

Queue.m_tail = HL.Field(HL.Number) << -1

Queue.m_length = HL.Field(HL.Number) << 0

Queue.Queue = HL.Constructor() << function(self)
    self.m_data = {}
    self.m_head = 1
    self.m_tail = 0
    self.m_length = 0
end

Queue.Push = HL.Method(HL.Any) << function(self, val)
    self.m_tail = self.m_tail + 1
    self.m_data[self.m_tail] = val
    self.m_length = self.m_length + 1
end

Queue.Empty = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_head > self.m_tail
end

Queue.Pop = HL.Method().Return(HL.Any) << function(self)
    assert(not self:Empty(), string.format("pop error %d %d", self.m_head, self.m_tail))
    local val = self.m_data[self.m_head]
    self.m_data[self.m_head] = nil
    self.m_head = self.m_head + 1
    self.m_length = self.m_length - 1
    return val
end

Queue.Front = HL.Method().Return(HL.Any) << function(self)
    assert(not self:Empty(), string.format("front error %d %d", self.m_head, self.m_tail))
    return self.m_data[self.m_head]
end

Queue.GetTail = HL.Method().Return(HL.Any) << function(self)
    assert(not self:Empty(), string.format("tail error %d %d", self.m_head, self.m_tail))
    return self.m_data[self.m_tail]
end

Queue.PopTail = HL.Method().Return(HL.Any) << function(self)
    assert(not self:Empty(), string.format("pop tail error %d %d", self.m_head, self.m_tail))
    local val = self.m_data[self.m_tail]
    self.m_data[self.m_tail] = nil
    self.m_tail = self.m_tail - 1
    self.m_length = self.m_length - 1
    return val
end

Queue.Size = HL.Method().Return(HL.Number) << function(self)
    return self.m_length
end

Queue.Count = HL.Method().Return(HL.Number) << function(self)
    return self:Size()
end

Queue.Clear = HL.Method() << function(self)
    self.m_data = {}
    self.m_head = 1
    self.m_tail = 0
    self.m_length = 0
end

Queue.AtIndex = HL.Method(HL.Number).Return(HL.Any) << function(self, index)
    if self.m_head + index - 1 > self.m_tail then
        logger.error("index is larger than length", index, self.m_length)
        return
    end
    return self.m_data[self.m_head + index - 1]
end

Queue.Sort = HL.Method(HL.Function) << function(self, functionObject)
    if self:Empty() then
        return
    end

    self:_QuickSort(self.m_data, self.m_head, self.m_tail, functionObject or function(left, right)
        return left < right
    end)
end

Queue._QuickSort = HL.Method(HL.Table, HL.Any, HL.Any, HL.Function) << function(self, array, left, right, functionObject)
    if left < right then
        local i = left
        local j = right
        local x = array[left]
        while i < j do
            while i < j and(not functionObject(array[j], x)) do
                j = j - 1
            end
            if i < j then
                array[i] = array[j]
                i = i + 1
            end

            while i < j and functionObject(array[i], x) do
                i = i + 1
            end
            if i < j then
                array[j] = array[i]
                j = j - 1
            end
        end
        array[i] = x

        self:_QuickSort(array, left, i - 1, functionObject)
        self:_QuickSort(array, i + 1, right, functionObject)
    end
end

Queue.Contains = HL.Method(HL.Any).Return(HL.Boolean) << function(self, val)
    if self:Empty() then
        return false
    end

    for i = self.m_head, self.m_tail do
        if self.m_data[i] == val then
            return true
        end
    end

    return false
end

Queue.IndexOf = HL.Method(HL.Any).Return(HL.Opt(HL.Number)) << function(self, val)
    if self:Empty() then
        return nil
    end

    for i = self.m_head, self.m_tail do
        if self.m_data[i] == val then
            return i - self.m_head + 1
        end
    end

    return nil
end

Queue.Move2Tail = HL.Method(HL.Any) << function(self, index)
    if self:Empty() or index < self.m_head or index > self.m_tail then
        return
    end

    local targetValue = self.m_data[index]
    for i = index, self.m_tail - 1 do
        self.m_data[i] = self.m_data[i + 1]
    end
    self.m_data[self.m_tail] = targetValue
end

Queue.Move2Front = HL.Method(HL.Number) << function(self, index)
    if self:Empty() or index < self.m_head or index > self.m_tail then
        return
    end

    local targetValue = self.m_data[index]
    for i = index, self.m_head + 1, -1 do
        self.m_data[i] = self.m_data[i - 1]
    end
    self.m_data[self.m_head] = targetValue
end

Queue.RemoveAt = HL.Method(HL.Number) << function(self, index)
    if self:Empty() or index < self.m_head or index > self.m_tail then
        return
    end

    if index == self.m_tail then
        self:PopTail()
        return
    end

    for i = index, self.m_tail - 1 do
        self.m_data[i] = self.m_data[i + 1]
    end
    self.m_data[self.m_tail] = nil
    self.m_tail = self.m_tail - 1
    self.m_length = self.m_length - 1
end

Queue.RemoveIf = HL.Method(HL.Function) << function(self, functionObject)
    if self:Empty() then
        return
    end

    local cur = self.m_head
    for idx = self.m_head, self.m_tail do
        if not functionObject(self.m_data[idx]) then
            self.m_data[cur] = self.m_data[idx]
            cur = cur + 1
        end
    end

    for idx = cur, self.m_tail do
        self.m_data[idx] = nil
        self.m_length = self.m_length - 1
    end

    self.m_tail = cur - 1
end

HL.Commit(Queue)
return Queue
