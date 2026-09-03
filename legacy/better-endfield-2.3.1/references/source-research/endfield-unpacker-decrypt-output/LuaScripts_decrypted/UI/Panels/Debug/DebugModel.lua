
local uiModel = require_ex('UI/Panels/Base/UIModel')
local GmCommandManager = CS.Beyond.GmCommandManager.instance
local LuaGmCommand = CS.Beyond.LuaGmCommand
DebugModel = HL.Class('DebugModel', uiModel.UIModel)

DebugModel.m_cmds = HL.Field(HL.Table)


DebugModel.InitModel = HL.Override() << function(self)
    self.m_cmds = {}
    self:_RegisterDebugCommands()
end

DebugModel._RegisterCommand = HL.Method(HL.String, HL.String, HL.String, HL.Function, HL.Table, HL.Table, HL.String, HL.String, HL.String)
<< function(self, category, name, alias, func, args, defaultValues, friendlyName, tip, params)
    local command = LuaGmCommand(category, name, alias, func, args, defaultValues, friendlyName, tip, params)
    table.insert(self.m_cmds, command)
    GmCommandManager:RegisterCommand(command)
end

DebugModel._RegisterDebugCommands = HL.Method() << function(self)
    local debugCmdConfig
    if BEYOND_DEBUG_COMMAND then
        debugCmdConfig = require_ex('Debug/DebugCommandConfigs')
    end
    if not debugCmdConfig then
        return
    end

    local cmdCfgs = debugCmdConfig
    for category, cmdList in pairs(cmdCfgs) do
        for _, cfg in ipairs(cmdList) do
            local cmdName = cfg.cmdName or ""
            local cmdNameAlias = cfg.cmdNameAlias or ""
            local friendlyName = cfg.btnName or ""
            local tip = cfg.tip or ""
            local args = {}
            local defaultValues = {}
            local methodParams
            if cfg.args then
                for i, arg in ipairs(cfg.args) do
                    table.insert(args, arg.name)
                    table.insert(defaultValues, arg.default or "")
                    if arg.params ~= nil then
                        if methodParams == nil then
                            methodParams = {}
                        end
                        methodParams[CSIndex(i)] = arg.params
                    end
                end
            end
            local params = ""
            if methodParams ~= nil then
                local json = require("Common/Tools/json")
                params = json.encode(methodParams)
            end
            self:_RegisterCommand(category, cmdName, cmdNameAlias, cfg.cmdFunc, args, defaultValues, friendlyName, tip, params)
        end
    end
end


 DebugModel.OnClose = HL.Override() << function(self)
     for _, v in ipairs(self.m_cmds) do
        GmCommandManager:UnRegisterCommand(v)
         v:Release()
     end
     self.m_cmds = {}
 end

HL.Commit(DebugModel)
