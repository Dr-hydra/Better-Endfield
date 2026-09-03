


local FormatUtils = {}



function FormatUtils.replaceVars(str, vars)
    if not vars then
        vars = str
        str = vars[1]
    end
    return (string.gsub(str, "({([^}]+)})",
        function(whole,i)
            return vars[i] or whole
        end))
end


_G.FormatUtils = FormatUtils
return FormatUtils