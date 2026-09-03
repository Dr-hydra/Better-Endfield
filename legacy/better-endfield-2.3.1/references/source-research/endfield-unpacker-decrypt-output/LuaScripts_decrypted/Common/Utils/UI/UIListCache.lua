












UIListCache = HL.Class("UIListCache")

do


    UIListCache.m_items = HL.Field(HL.Table)

    UIListCache.m_itemTemplate = HL.Field(HL.Any)

    UIListCache.m_wrapFunction = HL.Field(HL.Function)

    UIListCache.m_parent = HL.Field(HL.Userdata)

    UIListCache.m_count = HL.Field(HL.Number) << 0

    UIListCache.m_isPlayingAllOut = HL.Field(HL.Boolean) << false

    UIListCache._coroutine = HL.Field(HL.Thread)

    UIListCache.m_ctrlBound = HL.Field(HL.Boolean) << false


    UIListCache.m_debugParentPath = HL.Field(HL.String) << ""
    UIListCache.m_debugTemplatePath = HL.Field(HL.String) << ""
end



UIListCache.UIListCache = HL.Constructor(HL.Any, HL.Opt(HL.Function, HL.Any)) << function(self, itemTemplate, wrapFunction, parent)
    wrapFunction = wrapFunction or Utils.wrapLuaNode

    self.m_items = {}
    self.m_itemTemplate = itemTemplate
    self.m_wrapFunction = wrapFunction
    self.m_parent = parent and parent.transform or itemTemplate.transform.parent


    if self.m_itemTemplate then
        self.m_debugTemplatePath = self.m_itemTemplate.transform:PathFromRoot()
    else
        logger.error("UIListCache Init Error #2: itemTemplate is nil in Lua!")
    end

    if self.m_parent then
        self.m_debugParentPath = self.m_parent:PathFromRoot()
    else
        logger.error("UIListCache Init Error #4: parent is nil in Lua!")
    end

    self.m_itemTemplate.gameObject:SetActive(false)
end

UIListCache.Refresh = HL.Method(HL.Number, HL.Opt(HL.Function, HL.Boolean, HL.Function)) << function(self, count, refreshFunction, shouldHide, onDisableFunction)
    self.m_count = count
    self:ClearAllTween(true)
    for index = 1, count do
        self:_GenItem(index, shouldHide)
    end

    for index = count + 1, #self.m_items do
        local item = self.m_items[index]
        item.gameObject:SetActive(false)
        if onDisableFunction then
            onDisableFunction(item, index)
        end
    end





    for index = 1, count do
        local item = self:_GenItem(index, shouldHide)
        if refreshFunction then
            refreshFunction(item, index)
        end
    end
end

UIListCache.GetCount = HL.Method().Return(HL.Number) << function(self)
    return self.m_count or 0
end


UIListCache.Init = HL.Method(HL.Number, HL.Opt(HL.Function)) << function(self, count, initFunc)
    self.m_count = count
    for index = 1, count do
        local item = self:_GenItem(index)
        if initFunc then
            initFunc(item, index)
        end
    end
end


UIListCache._BoundCoroutineToCtrl = HL.Method() << function(self)
    if self.m_ctrlBound then
        return
    end
    local luaPanel = self.m_parent:GetComponentInParent(typeof(CS.Beyond.UI.LuaPanel), true)
    if not luaPanel then
        return
    end
    local _, ctrl = UIManager:IsOpen(luaPanel.panelId)
    if not ctrl then
        return
    end
    CoroutineManager:RegisterParent(self, ctrl)
    self.m_ctrlBound = true
end

UIListCache.RefreshCoroutine = HL.Method(HL.Number, HL.Number, HL.Function) << function(self, count, wait, refreshFunction)
    self:_BoundCoroutineToCtrl()
    self._coroutine = CoroutineManager:ClearCoroutine(self._coroutine)
    self:Refresh(count, refreshFunction, true)
    self._coroutine = CoroutineManager:StartCoroutine(function()
        for index = 1, count do
            local item = self.m_items[index]
            coroutine.wait(wait)
            item.gameObject:SetActive(true)
        end
    end, self)
end

UIListCache.GetRefreshCoroutine = HL.Method(HL.Number, HL.Number, HL.Function).Return(HL.Function) << function(self, count, wait, refreshFunction)
    return function()
        self:Refresh(count, refreshFunction, true)
        for index = 1, count do
            local item = self.m_items[index]
            coroutine.wait(wait)
            item.gameObject:SetActive(true)
        end
    end
end

UIListCache.GraduallyRefresh = HL.Method(HL.Number, HL.Number, HL.Function) << function(self, count, wait, refreshFunction)
    self:_BoundCoroutineToCtrl()
    self._coroutine = CoroutineManager:ClearCoroutine(self._coroutine)
    self:Refresh(0, nil)
    self._coroutine = CoroutineManager:StartCoroutine(function()
        for index = 1, count do
            local item = self:_GenItem(index)
            if refreshFunction then
                refreshFunction(item, index)
            end

            coroutine.wait(wait)
        end
    end, self)
end

UIListCache._GenItem = HL.Method(HL.Number, HL.Opt(HL.Boolean)).Return(HL.Any) << function(self, index, shouldHide)
    local item = self.m_items[index]
    if not item then

        if IsNull(self.m_parent.gameObject) then
            local errorMsg = string.format(
                "[UIListCache Parent NULL] Error#5 during _GenItem at index %d\n" ..
                    "Parent: %s is NULL, (Path: %s)",
                index,
                tostring(self.m_parent), self.m_debugParentPath
            )
            logger.error(errorMsg)
        end
        if IsNull(self.m_itemTemplate.gameObject) then
            local errorMsg = string.format(
                "[UIListCache ItemTemplate NULL] Error#6 during _GenItem at index %d\n" ..
                    "Template: %s is NULL, (Path: %s)",
                index,
                tostring(self.m_itemTemplate), self.m_debugTemplatePath
            )
            logger.error(errorMsg)
        end

        local child = UIUtils.addChild(self.m_parent, self.m_itemTemplate, true)
        item = {
            gameObject = child.gameObject,
            transform = child.transform,
        }
        local luaRef = item.gameObject:GetComponent("LuaReference")
        if luaRef then
            luaRef:BindToLua(item)
        end
        item = self.m_wrapFunction(item)
        if not item.gameObject then
            item.gameObject = child.gameObject
        end
        if not item.transform then
            item.transform = child.transform
        end
        self.m_items[index] = item
    end
    item.gameObject:SetActive(not shouldHide)
    return item
end

UIListCache.GetItem = HL.Method(HL.Number).Return(HL.Any) << function(self, index)
    return self.m_items[index]
end

UIListCache.Get = HL.Method(HL.Number).Return(HL.Any) << function(self, index)
    return self.m_items[index]
end

UIListCache.GetItems = HL.Method().Return(HL.Any) << function(self)
    return self.m_items
end

UIListCache.Hide = HL.Method() << function(self)
    self.m_parent.gameObject:SetActive(false)
end

UIListCache.Show = HL.Method() << function(self)
    self.m_parent.gameObject:SetActive(true)
end

UIListCache.PlayAllOut = HL.Method(HL.Opt(HL.Function)) << function(self, callback)
    local count = self:GetCount()
    for i = 1, count do
        local cell = self:GetItem(i)
        if cell and cell.view and cell.view.animationWrapper then
            self.m_isPlayingAllOut = true
            cell.view.animationWrapper:PlayOutAnimation(function()

                if i == count then
                    self.m_isPlayingAllOut = false
                    self:Refresh(0)
                    if callback then
                        callback()
                    end
                end
            end)
        end
    end
end

UIListCache.ClearAllTween = HL.Method(HL.Boolean) << function(self, executeCallback)
    if self.m_isPlayingAllOut then
        local count = self:GetCount()
        for i = 1, count do
            local cell = self:GetItem(i)
            if cell and cell.view and cell.view.animationWrapper then
                cell.view.animationWrapper:ClearTween(executeCallback)
            end
        end
    end
    self.m_isPlayingAllOut = false
end

UIListCache.ClearAll = HL.Method() << function(self)
    self._coroutine = CoroutineManager:ClearCoroutine(self._coroutine)
    self:ClearAllTween(true)
    for _, item in pairs(self.m_items) do
        if item and item.gameObject and not IsNull(item.gameObject) then
            GameObject.Destroy(item.gameObject)
        end
    end
    self.m_items = {}
    self.m_count = 0
end

UIListCache.Update = HL.Method(HL.Function) << function(self, updateFunc)
    for k = 1, self.m_count do
        local item = self.m_items[k]
        updateFunc(item, k)
    end
end

UIListCache.OnClose = HL.Method() << function(self)
    self._coroutine = CoroutineManager:ClearCoroutine(self._coroutine)
end

HL.Commit(UIListCache)
