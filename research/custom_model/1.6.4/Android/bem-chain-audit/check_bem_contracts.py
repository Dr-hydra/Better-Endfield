"""Match selected current Android IL2CPP contracts to the production BEM source."""
from pathlib import Path
import json
import re

root = Path(__file__).resolve().parent
repo = root.parents[4]
data = json.loads((root/'contracts/targeted-contracts.json').read_text(encoding='utf-8'))
methods = data['methods']
classes = {(c['asm'], c['cls']): c for c in data['classes']}
builder = (repo/'android/app/src/main/cpp/modules/custom_model/android_mesh_builder.cpp').read_text(encoding='utf-8')
shared = (repo/'native/modules/custom_model/module.cpp').read_text(encoding='utf-8')
required = []
for _, typ, name, args, ret, count in re.findall(r'resolve\(api\.(\w+),"([^"]+)","([^"]+)","([^"]*)","([^"]+)",(\d+)\)',builder):
    required.append(('mesh_builder','UnityEngine.CoreModule.dll','UnityEngine.'+typ,name,args,ret,int(count)))
donor_builder = builder[builder.index('void* AndroidLoadUiDonor('):]
for typ, name, args, ret, count in re.findall(r'=resolve\("([^"]+)","([^"]+)","([^"]*)","([^"]+)",(\d+)\)',donor_builder):
    required.append(('ui_donor','Common.Beyond.dll','Beyond.Resource.'+typ,name,args,ret,int(count)))
required.append(('delivery','Common.Beyond.dll','Beyond.Resource.Runtime.BundleLoader.AssetProxy',
                 '_FinishWithAsset','UnityEngine.Object','System.Void',1))
pattern = r'\{\s*"([^"]+)"\s*,\s*\{\s*"([^"]+)"\s*,\s*"([^"]*)"\s*,\s*"([^"]+)"\s*,\s*"([^"]+)"\s*,\s*(nullptr|"[^"]*")\s*,\s*"([^"]+)"\s*,\s*(\d+)\s*\}\s*,\s*(true|false)'
for key, assembly, namespace, typ, name, args, ret, count, must in re.findall(pattern,shared):
    full = namespace+'.'+typ if namespace else typ
    if (assembly,full) not in classes:
        continue
    required.append((key,assembly,full,name,'' if args=='nullptr' else args[1:-1],ret,int(count)))

results = []
for key, assembly, typ, name, args, ret, count in required:
    signature = args.split('|') if args else []
    found = [m for m in methods if m['asm']==assembly and m['cls']==typ and m['name']==name and
             m['types']==signature and m['ret']==ret and m['count']==count]
    results.append({'source':key,'assembly':assembly,'class':typ,'method':name,'params':signature,'return':ret,
                    'match':len(found)==1,'entry':hex(found[0]['rva']) if len(found)==1 else None})

field_requirements = [
    ('UnityEngine.Object','m_CachedPtr','System.IntPtr',16),
    ('UnityEngine.Mesh.MeshData','m_Ptr','System.IntPtr',16),
    ('UnityEngine.Mesh.MeshDataArray','m_Ptrs','System.IntPtr*',16),
    ('UnityEngine.Mesh.MeshDataArray','m_Length','System.Int32',24),
    ('UnityEngine.Rendering.VertexAttributeDescriptor','<attribute>k__BackingField','UnityEngine.Rendering.VertexAttribute',16),
    ('UnityEngine.Rendering.VertexAttributeDescriptor','<format>k__BackingField','UnityEngine.Rendering.VertexAttributeFormat',20),
    ('UnityEngine.Rendering.VertexAttributeDescriptor','<dimension>k__BackingField','System.Int32',24),
    ('UnityEngine.Rendering.VertexAttributeDescriptor','<stream>k__BackingField','System.Int32',28),
    ('UnityEngine.Rendering.SubMeshDescriptor','<bounds>k__BackingField','UnityEngine.Bounds',16),
    ('UnityEngine.Rendering.SubMeshDescriptor','<vertexCount>k__BackingField','System.Int32',60),
]
fields = []
for typ, name, field_type, offset in field_requirements:
    klass = classes['UnityEngine.CoreModule.dll',typ]
    found = [f for f in klass['fields'] if f['name']==name and f['type']==field_type]
    fields.append({'class':typ,'name':name,'type':field_type,'expected_offset':offset,
                   'actual_offset':found[0]['offset'] if len(found)==1 else None,
                   'match':len(found)==1 and found[0]['offset']==offset})
result = {'game_version':'1.6.4','architecture':'arm64-v8a','method_count':len(results),
          'matched_methods':sum(x['match'] for x in results),'field_count':len(fields),
          'matched_fields':sum(x['match'] for x in fields),'methods':results,'fields':fields,
          'resource_reference':'production path uses FAssetProxyHandle.Get/Dispose; no literal ResourceRef class dependency',
          'scope':'selected BEM production contracts; no Android game launch or UI/resource scene capture'}
(root/'contract-check.json').write_text(json.dumps(result,ensure_ascii=False,indent=2)+'\n',encoding='utf-8')
print(json.dumps({k:v for k,v in result.items() if k not in ('methods','fields')}))
print('unmatched_methods', [x for x in results if not x['match']])
print('unmatched_fields', [x for x in fields if not x['match']])
