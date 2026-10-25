import json,struct,sys
p='temp/bemcheck/pkg.bem'
with open(p,'rb') as f:
    head=f.read(40); hsize=struct.unpack_from('<I',head,12)[0]
    msize=struct.unpack_from('<Q',head,24)[0]
    f.seek(hsize); m=json.loads(f.read(msize).decode('utf-8'))
print("minor",struct.unpack_from('<HH',head,8))
print("params",json.dumps(m.get('parameters'),ensure_ascii=False))
tot=0
for i,mesh in enumerate(m.get('meshes',[])):
    print(f"mesh{i}: vc={mesh.get('vertex_count')} idx={mesh.get('index_size')} draws={len(mesh.get('draws',[]))} stride={[s['stride'] for s in mesh.get('streams',[])]}")
for ch in m.get('mesh_deformations',[]):
    fr=ch.get('frames',[])
    tot+=sum(x.get('count',0) for x in fr)
    print("chan mesh",ch['mesh'],"param",ch['parameter'],"frames",[(x.get('value'),x.get('count')) for x in fr])
print("total deltas",tot)
for d in m.get('drivers',[]):
    print("driver",d.get('id'),d.get('waveform'),d.get('playback'),"bindings",len(d.get('bindings',[])),[ (b['type'],b.get('target') or b.get('property') or b.get('keyword')) for b in d.get('bindings',[])])
for mesh in m.get('meshes',[]):
    for j,dr in enumerate(mesh.get('draws',[])):
        o=dr.get('material_overrides')
        if o: print("draw override:",json.dumps(o,ensure_ascii=False)[:400])
