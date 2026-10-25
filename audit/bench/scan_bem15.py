import json, struct, sys, glob, os

def scan(path):
    try:
        with open(path,'rb') as f:
            head=f.read(40)
            if len(head)<40 or head[:8]!=b'BEM\x00PKG\x00': return None
            major,minor=struct.unpack_from('<HH',head,8)
            hsize=struct.unpack_from('<I',head,12)[0]
            fsize=struct.unpack_from('<Q',head,16)[0]
            msize=struct.unpack_from('<Q',head,24)[0]
            pcount=struct.unpack_from('<I',head,32)[0]
            f.seek(hsize); man=f.read(msize)
            m=json.loads(man.decode('utf-8'))
            return dict(minor=minor,fsize=fsize,pcount=pcount,man=m)
    except Exception as e:
        return None

files=sorted(glob.glob('**/*.bem',recursive=True))
files=[f for f in files if not f.startswith('build')]
rows=[]
for p in files:
    r=scan(p)
    if not r: continue
    m=r['man']
    drv=m.get('drivers') or []
    md=m.get('mesh_deformations') or []
    params=m.get('parameters') or []
    ndelta=0
    for ch in md:
        for fr in ch.get('frames',[]):
            if 'count' in fr: ndelta+=fr.get('count',0)
    mo=0
    for mesh in m.get('meshes',[]):
        for d in mesh.get('draws',[]):
            o=d.get('material_overrides')
            if o: mo+=1
    rows.append((p,r['minor'],r['fsize'],len(m.get('meshes',[])),len(m.get('textures',[])),len(md),ndelta,len(params),len(drv),mo))

rows.sort(key=lambda x:(-x[1],-x[3]))
print(f"{'minor':>5} {'sizeMB':>8} {'mesh':>5} {'tex':>5} {'chan':>5} {'deltas':>7} {'param':>5} {'drv':>4} {'mo':>4}  path")
for p,mi,fs,nm,nt,nc,nd,npar,ndrv,mo in rows:
    if mi>=3:
        print(f"{mi:>5} {fs/1048576:>8.1f} {nm:>5} {nt:>5} {nc:>5} {nd:>7} {npar:>5} {ndrv:>4} {mo:>4}  {p}")
print("total", len(rows), "max minor", max(r[1] for r in rows) if rows else None)
