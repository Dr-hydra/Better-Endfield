local uiCtrl = require_ex('UI/Panels/Base/UICtrl')
local PANEL_ID = PanelId.LuaConsole
LuaConsoleCtrl = HL.Class('LuaConsoleCtrl', uiCtrl.UICtrl)


local LUA_CONSOLE_LOG_SEP = "---------------------------------------------------------------------------------------------------"

local function luaConsoleFormatLogCellText(inputText, contentText, logIndex)
    local input = inputText or ""
    local content = contentText or ""
    local head = "[Run Script][" .. tostring(logIndex or 0) .. "]>>>>--- "
    local scriptBlock
    if string.find(input, "\n", 1, true) then
        scriptBlock = head .. "\n" .. input .. "\n---- <<<<"
    else
        scriptBlock = head .. input .. "---- <<<<"
    end
    return LUA_CONSOLE_LOG_SEP .. "\n" .. scriptBlock .. "\n" .. LUA_CONSOLE_LOG_SEP .. "\n" .. content
end





LuaConsoleCtrl.s_messages = HL.StaticField(HL.Table) << {
}


LuaConsoleCtrl.s_globalBindingGroupId = HL.StaticField(HL.Number) << -1

LuaConsoleCtrl.m_executeInputStrList = HL.Field(HL.Table)

LuaConsoleCtrl.m_getLogCellFunc = HL.Field(HL.Function)


LuaConsoleCtrl.OnCreate = HL.Override(HL.Any) << function(self, arg)
    self.view.closeButton.onClick:AddListener(function()
        self:Hide()
    end)

    self.view.inputField.onValidateInput = function(input, charIndex, addedChar)
        return self:_ValidateInput(addedChar)
    end

    self.view.confirmButton.onClick:AddListener(function()
        self:_TryExecute()
    end)
    self:BindInputEvent(CS.Beyond.Input.KeyboardKeyCode.Return, function()
        self:_TryExecute()
    end)
    self:BindInputEvent(CS.Beyond.Input.KeyboardKeyCode.KeypadEnter, function()
        self:_TryExecute()
    end)
    self.view.confirmAndCloseButton.onClick:AddListener(function()
        self:_TryExecute()
        self:Hide()
    end)

    self.m_getLogCellFunc = UIUtils.genCachedCellFunction(self.view.radioTextList)
    self.view.logList.onUpdateCell:AddListener(function(obj, csIndex)
        local cell = self.m_getLogCellFunc(obj)
        self:_OnRefreshLogCell(cell, LuaIndex(csIndex))
    end)

    self.view.clearButton.onClick:AddListener(function()
        self:_ClearLog()
    end)
    self:_ClearLog()
end

LuaConsoleCtrl.Init = HL.StaticMethod() << function()
    if not (BEYOND_DEBUG_COMMAND or BEYOND_DEBUG) then
        return
    end

    logger.info("LuaConsoleCtrl.Init")

    LuaConsoleCtrl.s_globalBindingGroupId = InputManagerInst:CreateGroup(-1)

    UIUtils.bindInputEvent(CS.Beyond.Input.KeyboardKeyCode.BackQuote, function()
        LuaConsoleCtrl.ToggleSelf()
    end, "a", InputTimingType.OnClick, LuaConsoleCtrl.s_globalBindingGroupId)

    UIUtils.bindInputEvent(CS.Beyond.Input.KeyboardKeyCode.R, function()
        LuaConsoleCtrl.RefreshScripts()
    end, "a", InputTimingType.OnClick, LuaConsoleCtrl.s_globalBindingGroupId)

    UIUtils.bindInputEvent(CS.Beyond.Input.KeyboardKeyCode.I, function()
        CS.Beyond.UI.UIText.CopyUITextContent()
    end, "a", InputTimingType.OnClick, LuaConsoleCtrl.s_globalBindingGroupId)


    _G.UI = UIManager
    _G.UC = {}
    setmetatable(_G.UC, {
        __index = function(_, k)
            return UIManager.cfgs[k].ctrl
        end
    })
end

LuaConsoleCtrl.OnDisposeLuaEnv = HL.StaticMethod() << function()
    if not (BEYOND_DEBUG_COMMAND or BEYOND_DEBUG) then
        return
    end
    InputManagerInst:DeleteGroup(LuaConsoleCtrl.s_globalBindingGroupId)
end


LuaConsoleCtrl.ToggleSelf = HL.StaticMethod() << function()
    if UIManager:IsShow(PANEL_ID) then
        UIManager:Hide(PANEL_ID)
    else
        LuaConsoleCtrl.AutoOpen(PANEL_ID, nil, true)
        UIManager:SetTopOrder(PANEL_ID)
    end
end

LuaConsoleCtrl.RefreshScripts = HL.StaticMethod() << function()
    Notify(MessageConst.SHOW_TOAST, "刷新Lua脚本……")
    refreshScripts()



    logger.info("开始进行刷新Lua后处理……")

    RedDotManager:UpdateConfigs()

    logger.info("刷新Lua后处理结束")
end

LuaConsoleCtrl.DevOnlyTogglePanel = HL.StaticMethod(HL.Table) << function(args)
    local panelName, isShow = unpack(args)
    if isShow then
        UIManager:Show(PanelId[panelName])
    else
        UIManager:Hide(PanelId[panelName])
    end
end

LuaConsoleCtrl.OnShow = HL.Override() << function(self)
    self.view.inputField:ActivateInputField()
end

LuaConsoleCtrl._ClearLog = HL.Method() << function(self)
    self.m_executeInputStrList = {}
    self.view.logList:UpdateCount(0)
end

LuaConsoleCtrl._ValidateInput = HL.Method(HL.Number).Return(HL.Any) << function(self, addedChar)
    if addedChar == 10 then

        if not Input.GetKey(Unity.KeyCode.LeftShift) and not Input.GetKey(Unity.KeyCode.RightShift) then
            return ""
        end
    end
    return addedChar
end

LuaConsoleCtrl._TryExecute = HL.Method() << function(self)
    if not self:IsShow() or Input.GetKey(Unity.KeyCode.LeftShift) or Input.GetKey(Unity.KeyCode.RightShift) then
        return
    end

    local str = string.trim(self.view.inputField.text)
    if str == "" then
        return
    end

    CS.Beyond.Lua.LuaCypher.DisableDoStringCypher()
    local logs = Utils.printDoString(str, true)
    CS.Beyond.Lua.LuaCypher.EnableDoStringCypher()
    local newContent = table.concat(logs or {})
    newContent = string.gsub(newContent, "\r\n", "\n")
    newContent = string.gsub(newContent, "\r", "\n")

    local logIndex = #self.m_executeInputStrList + 1

    table.insert(self.m_executeInputStrList, 1, {
        inputText = str,
        contentText = newContent,
        logIndex = logIndex,
    })
    self.view.logList:UpdateCount(#self.m_executeInputStrList)

    self.view.inputField:ActivateInputField()
end

LuaConsoleCtrl._OnRefreshLogCell = HL.Method(HL.Any, HL.Number) << function(self, cell, luaIndex)
    local data = self.m_executeInputStrList[luaIndex]
    cell.logTxt.text = luaConsoleFormatLogCellText(data.inputText, data.contentText, data.logIndex)
    cell.reInputBtn.onClick:RemoveAllListeners()
    cell.reInputBtn.onClick:AddListener(function()
        self.view.inputField.text = data.inputText
    end)
    cell.reExecuteBtn.onClick:RemoveAllListeners()
    cell.reExecuteBtn.onClick:AddListener(function()
        self.view.inputField.text = data.inputText
        self:_TryExecute()
    end)
    cell.reExecuteAndCloseBtn.onClick:RemoveAllListeners()
    cell.reExecuteAndCloseBtn.onClick:AddListener(function()
        self.view.inputField.text = data.inputText
        self:_TryExecute()
        self:Hide()
    end)
end

LuaConsoleCtrl.OnLuaDebugSocketMessage = HL.StaticMethod(HL.Table) << function(arg)
    local socketMgr, str = unpack(arg)
    local logs = Utils.printDoString(str, true)
    local newContent = table.concat(logs or {})
    newContent = string.gsub(newContent, "\n", "\r\n")
    socketMgr:SendString(newContent)
end

LuaConsoleCtrl.SyncClientRemoteTask = HL.StaticMethod(HL.Table) << function(arg)
    local content, callback = unpack(arg)
    logger.info("LuaConsoleCtrl.SyncClientRemoteTask:\n", content)
    local logs = Utils.printDoString(content, true)
    local result = table.concat(logs or {})
    logger.info("LuaConsoleCtrl.SyncClientRemoteTask Finish:\n", result)
    callback(result)
end

HL.Commit(LuaConsoleCtrl)
