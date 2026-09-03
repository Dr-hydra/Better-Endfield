UIWidgetManager = HL.Class('UIWidgetManager')

UIWidgetManager.m_widgetMap = HL.Field(HL.Table)

UIWidgetManager.UIWidgetManager = HL.Constructor() << function(self)
    self.m_widgetMap = {}
end

UIWidgetManager._Register = HL.Method(HL.String, HL.Any) << function(self, id, widgetFile)
    if self.m_widgetMap[id] then
        logger.error("Already exist wrap file", id)
        return
    end
    self.m_widgetMap[id] = widgetFile
end


UIWidgetManager.Wrap = HL.Method(HL.Any).Return(HL.Opt(HL.Any)) << function(self, component)
    if not component then
        return
    end

    if typeof(component) ~= "LuaUIWidget" then
        local luaWidget = component.transform:GetComponent("LuaUIWidget")
        if not luaWidget then
            logger.error("No LuaUIWidget", component.name, inspect(component))
            return
        end
        component = luaWidget
    end

    local id = component.id

    local widgetFile = self.m_widgetMap[id]
    if not widgetFile then
        local file = require_ex("UI/Widgets/" .. id)
        if not file then
            logger.error("No LuaUIWidget File:", id)
            return
        end
        self:_Register(id, file)
        widgetFile = file
    end

    local result = widgetFile(component)
    component.table = { result }
    return result
end

HL.Commit(UIWidgetManager)
return UIWidgetManager
