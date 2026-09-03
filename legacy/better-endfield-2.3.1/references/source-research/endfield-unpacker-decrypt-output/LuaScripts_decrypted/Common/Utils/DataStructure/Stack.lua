
Stack = HL.Class('Stack')

Stack.m_bottomIndex = HL.Field(HL.Number) << 1

Stack.m_topIndex = HL.Field(HL.Number) << 1

Stack.m_count = HL.Field(HL.Number) << 1

Stack.m_array = HL.Field(HL.Table)

Stack.Stack = HL.Constructor() << function(self)
    self.m_bottomIndex = -1
    self.m_topIndex = -1
    self.m_count = 0
    self.m_array = {}
end

Stack.Push = HL.Method(HL.Any) << function(self, item)
    if self.m_count < 0 then
        logger.error("stack 内个数小于 0", inspect2(), inspect2(item))
        return
    end

    if self.m_count == 0 then
        self.m_topIndex = 1
        self.m_bottomIndex = 1
        self.m_array[self.m_bottomIndex] = item
        self.m_count = 1
        return
    else
        self.m_topIndex = self.m_topIndex + 1
        self.m_array[self.m_topIndex] = item
        self.m_count = self.m_count + 1
        return
    end
end

Stack.Pop = HL.Method().Return(HL.Any) << function(self)
    if self.m_count > 0 then
        local item = self.m_array[self.m_topIndex]
        self.m_topIndex = self.m_topIndex - 1
        self.m_count = self.m_count - 1
        if self.m_count == 0 then
            self.m_bottomIndex = -1
            self.m_topIndex = -1
        end
        return item
    else
        return nil
    end
end

Stack.Peek = HL.Method().Return(HL.Any) << function(self)
    if self.m_count > 0 then
        return self.m_array[self.m_topIndex]
    else
        return nil
    end
end

Stack.PeekBottom = HL.Method().Return(HL.Any) << function(self)
    if self.m_count > 0 then
        return self.m_array[self.m_bottomIndex]
    else
        return nil
    end
end

Stack.Clear = HL.Method() << function(self)
    if self.m_count > 0 then
        for i = self.m_bottomIndex, self.m_topIndex do
            self.m_array[i] = nil
        end
        self.m_topIndex = -1
        self.m_bottomIndex = -1
        self.m_count = 0
    end
end

Stack.Contains = HL.Method(HL.Any).Return(HL.Boolean) << function(self, item)
    return self:IndexOf(item) ~= nil
end

Stack.IndexOf = HL.Method(HL.Any).Return(HL.Opt(HL.Number)) << function(self, item)
    if self.m_count > 0 then
        for i = self.m_bottomIndex, self.m_topIndex do
            if self.m_array[i] == item then
                return i
            end
        end
    end
end

Stack.Delete = HL.Method(HL.Any) << function(self, item)
    local index = self:IndexOf(item)
    if index then
        for i = index + 1, self.m_topIndex do
            self.m_array[i - 1] = self.m_array[i]
        end
        self.m_array[self.m_topIndex] = nil
        self.m_count = self.m_count - 1
        if self.m_count == 0 then
            self.m_topIndex = -1
            self.m_bottomIndex = -1
        else
            self.m_topIndex = self.m_topIndex - 1
        end
    end
end

Stack.Count = HL.Method().Return(HL.Number) << function(self)
    return self.m_count
end

Stack.Empty = HL.Method().Return(HL.Boolean) << function(self)
    return self.m_count <= 0
end

Stack.TopIndex = HL.Method().Return(HL.Number) << function(self)
    return self.m_topIndex
end

Stack.BottomIndex = HL.Method().Return(HL.Number) << function(self)
    return self.m_bottomIndex
end

Stack.Get = HL.Method(HL.Number).Return(HL.Any) << function(self, index)
    return self.m_array[index]
end

HL.Commit(Stack)
return Stack
