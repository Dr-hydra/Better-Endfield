from pathlib import Path
import json, traceback
import ida_auto, ida_funcs, ida_hexrays, ida_nalt, ida_ua, idc

root=Path(r'E:\Dr.Hydra\Better Endfield\.worktrees\dev-custom-model-design\research\postmodel-focused')
ida_auto.enable_auto(False)
base=ida_nalt.get_imagebase()
selected=[(0x19bd80,'Mesh_UploadMeshData'),(0xd4d60,'Mesh_GPUUpdate'),
          (0xe1ed20,'HG_SkinnedResource_Create'),(0x309ac0,'HG_SkinnedResourceBuffer_Ctor')]
out=[]
try:
    ready=ida_hexrays.init_hexrays_plugin()
    for rva,label in selected:
        ea=base+rva
        candidates=[]
        for file in root.glob('mesh-native-*.json'):
            try:
                entries=json.loads(file.read_text(encoding='utf-8'))
                candidates.extend(f for f in entries if f.get('rva')==hex(rva) and 'asm' in f)
            except Exception:pass
        if candidates:
            obj=max(candidates,key=lambda f:len(f['asm']))
            spans=[]
            for line in obj['asm']:
                addr=base+int(line.split()[0],16)
                ins=ida_ua.insn_t()
                size=ida_ua.decode_insn(ins,addr)
                if not size:continue
                ida_ua.create_insn(addr)
                if spans and spans[-1][1]==addr:spans[-1][1]=addr+size
                else:spans.append([addr,addr+size])
            ida_funcs.del_func(ea)
            ida_funcs.add_func(ea,spans[0][1])
            fn=ida_funcs.get_func(ea)
            for lo,hi in spans[1:]:ida_funcs.append_func_tail(fn,lo,hi)
        else:ida_funcs.add_func(ea)
        idc.set_name(ea,label)
        try:
            f=ida_hexrays.decompile(ea) if ready else None
            out.append({'rva':hex(rva),'name':label,'code':str(f) if f else 'decompile unavailable'})
        except Exception as e:out.append({'rva':hex(rva),'name':label,'error':str(e)})
    (root/'mesh-native-decompiled.json').write_text(json.dumps(out,ensure_ascii=False,indent=2),encoding='utf-8')
except Exception:
    (root/'mesh-native-decompile-error.txt').write_text(traceback.format_exc(),encoding='utf-8')
ida_auto.enable_auto(False)
idc.qexit(0)
