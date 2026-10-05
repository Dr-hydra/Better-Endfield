from pathlib import Path
import json, traceback
import ida_auto, ida_funcs, ida_hexrays, ida_nalt, idautils, idc
root=Path(r'E:/Dr.Hydra/Better Endfield/tmp_analysis/enhancer-port')
try:
    ida_auto.auto_wait()
    targets={}
    needles=['Missing native buffer descriptor','Cannot root read alias','Required mesh API absent','No head components or neck caps','Unsupported skin encoding','Invalid float skin weight','Cannot clone mesh independently','Uploaded vertex content mismatch','Bind poses do not match bones','CameraFillNeckHole','Cannot retain original shadow mesh']
    for s in idautils.Strings():
        label=str(s)
        if any(n in label for n in needles):
            for x in idautils.XrefsTo(s.ea):
                f=ida_funcs.get_func(x.frm)
                if f:targets.setdefault(f.start_ea,[]).append(label)
    for ea in list(targets):
        for x in idautils.XrefsTo(ea):
            f=ida_funcs.get_func(x.frm)
            if f:targets.setdefault(f.start_ea,[]).append('caller '+hex(ea))
    ready=ida_hexrays.init_hexrays_plugin()
    out=[]
    for ea,labels in targets.items():
        try:code=str(ida_hexrays.decompile(ea)) if ready else 'no decompiler'
        except Exception as e:code=str(e)
        name=hex(ea)
        (root/(name+'.c')).write_text(code,encoding='utf-8')
        out.append({'ea':name,'labels':labels,'size':len(code)})
    (root/'functions.json').write_text(json.dumps(out,indent=2),encoding='utf-8')
except Exception:
    (root/'error.txt').write_text(traceback.format_exc(),encoding='utf-8')
idc.qexit(0)
