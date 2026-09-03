local metatable = {}
            local rawget = rawget
            local setmetatable = setmetatable
            local import_type = xlua.import_type
            local import_generic_type = xlua.import_generic_type
            local load_assembly = xlua.load_assembly

            function metatable:__index(key)
                local fqn = rawget(self,'.fqn')
                fqn = ((fqn and fqn .. '.') or '') .. key

                local obj = import_type(fqn)

                if obj == nil then

                    obj = { ['.fqn'] = fqn }
                    setmetatable(obj, metatable)
                elseif obj == true then
                    return rawget(self, key)
                end


                rawset(self, key, obj)
                return obj
            end

            function metatable:__newindex()
                error('No such type: ' .. rawget(self,'.fqn'), 2)
            end


            function metatable:__call(...)
                local n = select('#', ...)
                local fqn = rawget(self,'.fqn')
                if n > 0 then
                    local gt = import_generic_type(fqn, ...)
                    if gt then
                        return rawget(CS, gt)
                    end
                end
                error('No such type: ' .. fqn, 2)
            end

            CS = CS or {}
            setmetatable(CS, metatable)

            typeof = function(t) return t.UnderlyingSystemType end
            cast = xlua.cast
            if not setfenv or not getfenv then
                local function getfunction(level)
                    local info = debug.getinfo(level + 1, 'f')
                    return info and info.func
                end

                function setfenv(fn, env)
                  if type(fn) == 'number' then fn = getfunction(fn + 1) end
                  local i = 1
                  while true do
                    local name = debug.getupvalue(fn, i)
                    if name == '_ENV' then
                      debug.upvaluejoin(fn, i, (function()
                        return env
                      end), 1)
                      break
                    elseif not name then
                      break
                    end

                    i = i + 1
                  end

                  return fn
                end

                function getfenv(fn)
                  if type(fn) == 'number' then fn = getfunction(fn + 1) end
                  local i = 1
                  while true do
                    local name, val = debug.getupvalue(fn, i)
                    if name == '_ENV' then
                      return val
                    elseif not name then
                      break
                    end
                    i = i + 1
                  end
                end
            end

            xlua.hotfix = function(cs, field, func)
                if func == nil then func = false end
                local tbl = (type(field) == 'table') and field or {[field] = func}
                for k, v in pairs(tbl) do
                    local cflag = ''
                    if k == '.ctor' then
                        cflag = '_c'
                        k = 'ctor'
                    end
                    local f = type(v) == 'function' and v or nil
                    xlua.access(cs, cflag .. '__Hotfix0_'..k, f)
                    pcall(function()
                        for i = 1, 99 do
                            xlua.access(cs, cflag .. '__Hotfix'..i..'_'..k, f)
                        end
                    end)
                end
                xlua.private_accessible(cs)
            end
            xlua.getmetatable = function(cs)
                return xlua.metatable_operation(cs)
            end
            xlua.setmetatable = function(cs, mt)
                return xlua.metatable_operation(cs, mt)
            end
            xlua.setclass = function(parent, name, impl)
                impl.UnderlyingSystemType = parent[name].UnderlyingSystemType
                rawset(parent, name, impl)
            end

            local base_mt = {
                __index = function(t, k)
                    local csobj = t['__csobj']
                    local func = csobj['<>xLuaBaseProxy_'..k]
                    return function(_, ...)
                         return func(csobj, ...)
                    end
                end
            }
            base = function(csobj)
                return setmetatable({__csobj = csobj}, base_mt)
            end