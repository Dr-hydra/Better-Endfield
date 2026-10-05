using BetterEndfield.UI.Services;
static void Check(bool value){if(!value)throw new Exception("INI preservation regression");}
foreach(var newline in new[]{"\n","\r\n"}) foreach(var flag in new[]{"true","false"}) {
    var existing=$"[unrelated]{newline}external_loop=wrong{newline}[betterendfield.actions]{newline}enabled=true{newline}external_loop={flag}{newline}[Host]{newline}test=1{newline}";
    var visible=$"[betterendfield.actions]{newline}schema_version=2{newline}enabled=false{newline}diagnostics=true{newline}";
    var result=ActionIniPreservation.PreserveExternalLoop(existing,visible);
    Check(result.Contains($"external_loop={flag}{newline}"));Check(result.Contains($"enabled=false{newline}"));Check(!result.Contains("wrong"));
    Check(ActionIniPreservation.PreserveExternalLoop(existing,result)==result);
    var full="[Host]"+newline+"modules_root=test"+newline+visible+"[Loader]"+newline+"load_host=true"+newline;
    var merged=ActionIniPreservation.PreserveExternalLoop(existing,full);
    Check(merged.Contains($"external_loop={flag}{newline}")&&merged.StartsWith("[Host]"+newline)&&merged.EndsWith("load_host=true"+newline));
    Check(ActionIniPreservation.PreserveExternalLoop("[other]\nexternal_loop=true",visible)==visible);
    Check(ActionIniPreservation.PreserveExternalLoop(existing,visible+"external_loop=explicit"+newline).EndsWith("external_loop=explicit"+newline));
}
Console.WriteLine("Action INI preservation: opt-in, opt-out, full save, toggle save, unrelated section and newline checks passed.");
