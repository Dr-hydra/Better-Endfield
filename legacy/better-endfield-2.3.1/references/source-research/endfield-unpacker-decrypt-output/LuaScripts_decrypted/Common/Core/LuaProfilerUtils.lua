local luaProfilerUtils = {}



if ENABLE_PROFILER and unity_sample and unity_sample.begin_unity_sample and unity_sample.end_unity_sample then
    local beginSample = unity_sample.begin_unity_sample
    local endSample = unity_sample.end_unity_sample
    luaProfilerUtils.BeginSample = beginSample
    luaProfilerUtils.EndSample = endSample











else
    luaProfilerUtils.BeginSample = function()  end
    luaProfilerUtils.EndSample = function()  end









end

return luaProfilerUtils

