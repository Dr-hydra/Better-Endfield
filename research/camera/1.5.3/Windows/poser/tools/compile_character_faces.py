"""Compile local PMX facial shapes into per-character bone calibration files.

Requires Python 3, numpy and scipy. No game access; no model meshes/textures are
copied into profiles. Input models and diagnostic reports stay local; reusable
calibration profiles can be maintained under resources/character-faces.
"""
import argparse
import hashlib
import json
import math
import os
import re
import struct
from pathlib import Path

# Small solves are faster and more predictable without a large BLAS thread pool.
os.environ.setdefault('OPENBLAS_NUM_THREADS', '1')
os.environ.setdefault('OMP_NUM_THREADS', '1')
import numpy as np
from scipy.linalg import lstsq
from scipy.spatial.transform import Rotation


class Reader:
    def __init__(self, data):
        self.data, self.offset = data, 0

    def raw(self, n):
        if n < 0 or n > len(self.data) - self.offset:
            raise ValueError('Truncated PMX')
        p = self.offset
        self.offset += n
        return self.data[p:self.offset]

    def get(self, fmt):
        values = struct.unpack('<' + fmt, self.raw(struct.calcsize('<' + fmt)))
        if any(isinstance(v, float) and not math.isfinite(v) for v in values):
            raise ValueError('Non-finite PMX number')
        return values[0] if len(values) == 1 else values

    def count(self, limit=2000000, stride=1):
        n = self.get('i')
        if n < 0 or n > limit or n * stride > len(self.data) - self.offset:
            raise ValueError('Invalid PMX count')
        return n

    def text(self, encoding):
        return self.raw(self.count(1048576)).decode(encoding, errors='strict')

    def index(self, width, unsigned=False):
        return self.get({1: 'B' if unsigned else 'b', 2: 'H' if unsigned else 'h', 4: 'I' if unsigned else 'i'}[width])


def read_pmx(path):
    if path.stat().st_size > 256 * 1024 * 1024:
        raise ValueError('PMX exceeds 256 MiB')
    data = path.read_bytes()
    r = Reader(data)
    magic=r.raw(4);version=r.get('f')
    if magic != b'PMX ' or min(abs(version-2.0),abs(version-2.1))>.001:
        raise ValueError('Expected PMX 2.0/2.1')
    header = r.raw(r.get('B'))
    if len(header) < 8 or header[0] > 1 or header[1] > 4 or any(n not in (1, 2, 4) for n in header[2:8]):
        raise ValueError('Invalid PMX header')
    enc = 'utf-8' if header[0] else 'utf-16-le'
    uv, vi, ti, mi, bi, morphi, rigid = header[1:8]
    name, english, _, _ = (r.text(enc) for _ in range(4))
    n = r.count(stride=33)
    vertices = np.empty((n, 3)); indices = np.full((n, 4), -1, dtype=np.int32); weights = np.zeros((n, 4))
    for i in range(n):
        vertices[i] = r.get('3f'); r.raw(20 + 16 * uv)
        skin = r.get('B')
        if skin == 0:
            indices[i, 0] = r.index(bi); weights[i, 0] = 1
        elif skin in (1, 3):
            indices[i, :2] = [r.index(bi), r.index(bi)]
            w = r.get('f'); weights[i, :2] = [w, 1-w]
            if skin == 3: r.raw(36)
        elif skin in (2, 4):
            indices[i] = [r.index(bi) for _ in range(4)]; weights[i] = r.get('4f')
        else:
            raise ValueError('Unknown PMX skinning mode')
        r.get('f')
    face_count = r.count(20000000, vi)
    if face_count % 3: raise ValueError('Invalid PMX triangle count')
    faces = np.frombuffer(r.raw(face_count * vi), dtype={1:'u1',2:'<u2',4:'<u4'}[vi]).reshape(-1,3)
    if np.any(faces >= len(vertices)): raise ValueError('Invalid PMX triangle vertex')
    textures = [r.text(enc) for _ in range(r.count(100000, 4))]
    materials=[];face_start=0
    for _ in range(r.count(100000, 8)):
        material_name=r.text(enc); r.text(enc); r.raw(65 + 2*ti + 1)
        shared = r.get('B')
        if shared not in (0, 1): raise ValueError('Invalid toon flag')
        r.raw(1 if shared else ti); r.text(enc); material_count=r.get('i')
        if material_count<0 or material_count%3 or face_start+material_count//3>len(faces):
            raise ValueError('Invalid PMX material face count')
        materials.append({'name':material_name,'start':face_start,'count':material_count//3})
        face_start+=material_count//3
    if face_start!=len(faces):raise ValueError('Incomplete PMX material faces')
    bones = []
    for _ in range(r.count(8192, 8)):
        b = {'name': r.text(enc), 'english': r.text(enc), 'rest': r.get('3f'), 'parent': r.index(bi)}
        b['layer']=r.get('i'); flags = b['flags'] = r.get('H')
        b['tail'] = r.index(bi) if flags & 1 else r.get('3f')
        if flags & 0x300: b['append']=(r.index(bi),r.get('f'))
        if flags & 0x400: b['fixed_axis']=r.get('3f')
        # Some references contain unused NaNs in optional axes. Preserve this
        # metadata for diagnostics without rejecting otherwise valid face data.
        if flags & 0x800: b['local_axes']=struct.unpack('<6f',r.raw(24))
        if flags & 0x2000: r.raw(4)
        if flags & 0x20:
            b['ik']={'target':r.index(bi),'iterations':r.get('i'),'angle':r.get('f'),'links':[]}
            for _ in range(r.count(256, bi+1)):
                link={'bone':r.index(bi)}
                if r.get('B'): link['limits']=(r.get('3f'),r.get('3f'))
                b['ik']['links'].append(link)
        bones.append(b)
    if np.any(indices >= len(bones)) or np.any(weights < -1e-5) or np.any(weights > 1.00001):
        raise ValueError('Invalid PMX skinning weights')
    totals=weights.sum(1)
    if np.any(totals<.5) or np.any(totals>1.5): raise ValueError('Invalid PMX weight total')
    weight_fixes=int(np.sum(np.abs(totals-1)>.02))
    weights/=totals[:,None]
    for b in bones:
        if b['parent'] < -1 or b['parent'] >= len(bones): raise ValueError('Invalid PMX parent')
    morphs = []
    for _ in range(r.count(100000, 14)):
        m = {'name': r.text(enc), 'english': r.text(enc), 'category': r.get('B'), 'type': r.get('B')}
        kind = m['type']; strides = {0:morphi+4,1:vi+12,2:bi+28,3:vi+16,4:vi+16,5:vi+16,6:vi+16,7:vi+16,8:mi+113,9:morphi+4,10:rigid+25}
        if kind not in strides: raise ValueError('Invalid morph kind')
        n = r.count(20000000, strides[kind])
        if kind == 1:
            ids = np.empty(n, dtype=np.int32); offsets = np.empty((n,3))
            for i in range(n): ids[i]=r.index(vi,True); offsets[i]=r.get('3f')
            if np.any(ids >= len(vertices)): raise ValueError('Invalid morph vertex')
            m['ids'], m['offsets'] = ids, offsets
        elif kind == 0:
            m['groups'] = [(r.index(morphi),r.get('f')) for _ in range(n)]
        else: r.raw(n*strides[kind])
        morphs.append(m)
    return {'name':name,'english':english,'vertices':vertices,'indices':indices,'weights':weights,
            'bones':bones,'morphs':morphs,'textures':textures,'faces':faces,'materials':materials,
            'weight_fixes':weight_fixes,'hash':hashlib.sha256(data).hexdigest()}


def normalize(name):
    return name.translate(str.maketrans('０１２３４５６７８９ＩＫ','0123456789IK'))


def game_bone(name):
    m = re.fullmatch('([左右])眉毛([1-5])',name)
    if m: return 'brow'+('Lf' if m[1]=='左' else 'Rt')+f'{int(m[2]):02}Joint'
    m = re.fullmatch('([左右])二重([1-3])',name)
    if m: return 'browLine'+('Lf' if m[1]=='左' else 'Rt')+f'{4-int(m[2]):02}Joint'
    m = re.fullmatch('([左右])嘴([上下])([1-4])',name)
    if m: return 'lip'+('L' if m[1]=='左' else 'R')+('up' if m[2]=='上' else 'dn')+str(5-int(m[3]))+'Joint'
    m = re.fullmatch(r'([左右])眼(\d+)',name)
    if m:
        lut={13:'01Joint',2:'02Joint',1:'01EyelashJoint',3:'02EyelashJoint',4:'03Joint',5:'03EyelashJoint',6:'04Joint',7:'04EyelashJoint',8:'05Joint',9:'05EyelashJoint',10:'06Joint',11:'07Joint',12:'08Joint'}
        return 'eye'+('Lf' if m[1]=='左' else 'Rt')+lut.get(int(m[2]),'?')
    return {'嘴上中':'lipMupJoint','嘴下中':'lipMdnJoint','牙下':'faceMdToothDnJoint','牙上':'faceMdToothUpJoint',
            '舌头后':'TongueMd01Joint','舌头前':'TongueMd02Joint','左目':'eyeLfJoint','右目':'eyeRtJoint',
            '瞳左':'faceLfIrisJoint','瞳右':'faceRtIrisJoint'}.get(name,name)


def region(name):
    n=name.lower()
    if n.startswith(('browline','eye')): return 'eyes'
    if n.startswith(('facelfiris','facertiris','facelfpupil','facertpupil','facelfhighlight','facerthighlight')): return 'eyes'
    if n.startswith('brow'): return 'brows'
    if n.startswith(('lip','tongue','facemdtooth')) or n in ('jawjoint','facemdjawdnjoint','line_toothjoint'): return 'mouth'
    if n.startswith(('facelfcheek','facertcheek')): return 'cheeks'
    return None


def recover_teeth_weights(model, selected):
    """Recover detached tooth surfaces only with unambiguous geometric evidence.

    Some references attach the entire mouth interior to the head even though
    matching game tooth controls remain in the skeleton. A zero skinning column
    cannot fit their motion. Use isolated oral surfaces near the tooth anchors;
    never guess weights for the outer face or alter already weighted teeth.
    These weights are only used by the offline fit, never applied to the model.
    """
    vertices=model['vertices'];indices=model['indices'].copy();weights=model['weights'].copy()
    teeth=[i for i in selected if game_bone(model['bones'][i]['name']).lower() in
           ('facemdtoothupjoint','facemdtoothdnjoint','line_toothjoint')]
    missing=[i for i in teeth if not np.any((indices==i)&(weights>1e-7))]
    report={'recovered':[],'unresolved':[game_bone(model['bones'][i]['name']) for i in missing]}
    if not missing or 'faces' not in model:return indices,weights,report
    oral=[]
    for mat in model.get('materials',[]):
        if mat['name'].lower() in ('口内','口腔','mouth','mouth_inside','mouthinside','oral'):
            oral.extend(model['faces'][mat['start']:mat['start']+mat['count']].tolist())
    if not oral:return indices,weights,report
    ids=sorted({int(i) for face in oral for i in face});parent={i:i for i in ids}
    def find(i):
        while parent[i]!=i:parent[i]=parent[parent[i]];i=parent[i]
        return i
    def union(a,b):parent[find(a)]=find(b)
    for a,b,d in oral:union(a,b);union(a,d)
    # Split normals / UV seams may duplicate a single rigid tooth surface.
    anchors=np.array([model['bones'][i]['rest'] for i in teeth])
    span=float(np.max(np.linalg.norm(anchors[:,None]-anchors,axis=2)))
    if span<1e-5:return indices,weights,report
    epsilon=span*1e-5;coincident={}
    for i in ids:
        key=tuple(np.rint(vertices[i]/epsilon).astype(np.int64))
        if key in coincident:union(i,coincident[key])
        else:coincident[key]=i
    groups={}
    for i in ids:groups.setdefault(find(i),[]).append(i)
    components=[]
    for group in groups.values():
        group=np.array(group,dtype=int)
        if len(group)<6:continue
        # Only head-attached components qualify. Do not steal another facial
        # control's surface or combine existing teeth with an inferred surface.
        active=set(indices[group][weights[group]>1e-7].tolist())
        if not active or any(i<0 or model['bones'][i]['name'].lower() not in
            ('頭','head','face_head','bip001_head') for i in active):continue
        points=vertices[group];radius=float(np.linalg.norm(np.ptp(points,axis=0)))
        if radius<=1e-6:continue
        components.append((group,points.mean(0),radius))
    proposed={}
    for i in missing:
        anchor=np.array(model['bones'][i]['rest'])
        ranked=sorted((float(np.linalg.norm(center-anchor)),j) for j,(_,center,_) in enumerate(components))
        if not ranked:continue
        distance,j=ranked[0];group,center,radius=components[j]
        # Both a size-relative bound and a nearest-candidate margin are needed.
        if distance>min(radius*.15,span*.3) or (len(ranked)>1 and ranked[1][0]<max(distance*1.5,span*.025)):continue
        proposed[i]=j
    for i,j in proposed.items():
        if list(proposed.values()).count(j)!=1:continue
        group=components[j][0];indices[group]=-1;weights[group]=0
        indices[group,0]=i;weights[group,0]=1
        name=game_bone(model['bones'][i]['name'])
        report['recovered'].append({'bone':name,'vertices':len(group)})
        report['unresolved'].remove(name)
    return indices,weights,report


def compile_profile(model, label):
    # Exact original names take precedence over additional MMD control bones.
    targets={}
    for i,b in enumerate(model['bones']):
        name=game_bone(b['name']); key=name.lower()
        if region(name) and (key not in targets or name==b['name']): targets[key]=i
    selected=list(targets.values()); names=list(targets)
    if len(selected)>256: raise ValueError('Too many face bones')
    source_ids={b:i for i,b in enumerate(selected)}
    original_indices,original_weights,teeth_report=recover_teeth_weights(model,selected)
    mask=np.zeros(len(model['vertices']),dtype=bool)
    for b in selected: mask |= np.any((original_indices==b)&(original_weights>1e-7),axis=1)
    vertex_ids=np.flatnonzero(mask); vertices=model['vertices'][vertex_ids]
    W=np.zeros((len(vertices),len(selected)))
    for slot in range(4):
        for b,j in source_ids.items(): W[:,j]+=np.where(original_indices[vertex_ids,slot]==b,original_weights[vertex_ids,slot],0)
    positions=np.array([model['bones'][i]['rest'] for i in selected])
    if len(selected)<12 or len(vertices)<12: return None, {'reason':'模型缺少足够的可对应面部控制点'}
    mouth_columns=[i for i,n in enumerate(names) if region(n)=='mouth']
    mouth_vertices=np.any(W[:,mouth_columns]>1e-7,axis=1) if mouth_columns else np.zeros(len(vertices),dtype=bool)
    mouth_points=positions[mouth_columns]
    mouth_span=float(np.max(np.linalg.norm(mouth_points[:,None]-mouth_points,axis=2))) if mouth_columns else 0.
    tooth_names=('facemdtoothupjoint','facemdtoothdnjoint')
    missing_teeth=[name for name in tooth_names if name not in names or np.count_nonzero(W[:,names.index(name)]>1e-7)<6]
    missing_teeth+=teeth_report['unresolved']
    # Solve global translations and infinitesimal rotations using the source
    # skinning Jacobian. All vertices influenced by these bones are included,
    # including unchanged vertices, to prevent unrelated regions drifting.
    blocks=[]
    for j in range(len(selected)):
        x=vertices-positions[j]; z=np.zeros(len(x))
        cross=np.stack([z,x[:,2],-x[:,1],-x[:,2],z,x[:,0],x[:,1],-x[:,0],z],axis=1).reshape(-1,3,3)
        block=np.concatenate([np.broadcast_to(np.eye(3),(len(x),3,3)),cross],axis=2)
        blocks.append(block*W[:,j,None,None])
    A=np.concatenate(blocks,axis=2).reshape(-1,len(selected)*6)
    # Ridge selects a stable solution for shared/degenerate weights. The head
    # bone is excluded: unrepresentable vertex motion is reported, never faked
    # by translating the entire head.
    regularizer=np.tile([.02,.02,.02,.08,.08,.08],len(selected))
    solver=np.vstack([A,np.diag(regularizer)])
    morphs=model['morphs']; chosen=[]; deltas=[]; skipped=[]; panels=[]
    def vertex_delta(index, stack=()):
        if index<0 or index>=len(morphs) or index in stack or len(stack)>32: raise ValueError('Invalid/cyclic group morph')
        m=morphs[index]; d=np.zeros_like(model['vertices'])
        if m['type']==1: np.add.at(d,m['ids'],m['offsets'])
        elif m['type']==0:
            for child,weight in m['groups']: d+=vertex_delta(child,stack+(index,))*weight
        else: raise ValueError('非顶点形状或包含材质/特殊控制')
        return d
    for i,m in enumerate(morphs):
        n=normalize(m['name'])
        if m['category'] not in (1,2,3) or any(x in n for x in ('瞳','恐ろしい','ハイライト','歯消','照れ')):
            skipped.append({'name':n,'reason':'特殊效果不校准'}); continue
        try: d=vertex_delta(i)
        except ValueError as e: skipped.append({'name':n,'reason':str(e)}); continue
        if np.linalg.norm(d)<1e-8: skipped.append({'name':n,'reason':'没有形变'});continue
        chosen.append(n);deltas.append(d);panels.append(m['category'])
    if not chosen:return None,{'reason':'模型没有可校准的普通表情','skipped':skipped}
    rhs=np.zeros((solver.shape[0],len(chosen)))
    for k,d in enumerate(deltas):rhs[:len(vertices)*3,k]=d[vertex_ids].reshape(-1)
    params=lstsq(solver,rhs,cond=1e-6,lapack_driver='gelsy')[0].reshape(len(selected),6,-1)
    # Refine exact rigid transforms by weighted local Procrustes, retaining the
    # stable Jacobian initialization. No approximation of rotations as offsets
    # is used when measuring the exported calibration error.
    expressions=[];quality=[]
    for k,name in enumerate(chosen):
        translation=params[:,:3,k].copy();rotvec=params[:,3:,k].copy()
        lengths=np.linalg.norm(rotvec,axis=1);rotvec*=np.minimum(1,.9/np.maximum(lengths,1e-15))[:,None]
        rotations=Rotation.from_rotvec(rotvec).as_matrix()
        target=deltas[k][vertex_ids]; predicted=np.zeros_like(vertices)
        contributions=[]
        for j in range(len(selected)):
            x=vertices-positions[j];c=W[:,j,None]*(x@rotations[j].T-x+translation[j]);contributions.append(c);predicted+=c
        # Six bounded sweeps improve curved eyelids without unconstrained 180°
        # rotations on tiny one-vertex controls.
        for sweep in range(6):
            for j in range(len(selected)):
                ids=np.flatnonzero(W[:,j]>1e-5)
                if len(ids)<3:continue
                x=vertices[ids]-positions[j];w=W[ids,j];w2=w*w
                goal=x+(target[ids]-predicted[ids]+contributions[j][ids])/w[:,None]
                # Anchor the rigid motion near its current neutral orientation.
                covariance=((x-(x*w2[:,None]).sum(0)/w2.sum())*w2[:,None]).T @ (goal-(goal*w2[:,None]).sum(0)/w2.sum())
                covariance+=np.eye(3)*.002
                u,_,vt=np.linalg.svd(covariance);fix=np.eye(3);fix[2,2]=np.linalg.det(vt.T@u.T)
                R=vt.T@fix@u.T;rv=Rotation.from_matrix(R).as_rotvec()
                if np.linalg.norm(rv)>1.2:continue
                t=((goal-x@R.T)*w2[:,None]).sum(0)/(w2.sum()+.0004)
                if np.linalg.norm(t)>2:continue
                candidate=w[:,None]*(x@R.T-x+t)
                old=contributions[j][ids];res=target[ids]-predicted[ids]
                if np.sum((res+old-candidate)**2)>np.sum(res**2)+1e-12:continue
                predicted[ids]+=candidate-old;contributions[j][ids]=candidate
                rotations[j]=R;rotvec[j]=rv;translation[j]=t
        full=deltas[k];covered=np.zeros(len(full),dtype=bool);covered[vertex_ids]=True
        error=np.sum((target-predicted)**2)+np.sum(full[~covered]**2)
        residual=float(np.sqrt(error/np.sum(full*full)))
        mouth_energy=float(np.sum(target[mouth_vertices]**2))
        mouth_error=float(np.sum((target-predicted)[mouth_vertices]**2))
        # Judge the mouth separately: eye vertices must not hide an inaccurate
        # mouth, and missing tooth weights must not count as a successful fit.
        mouth_residual=float(np.sqrt(mouth_error/mouth_energy)) if mouth_energy>1e-12 else 0.
        mouth_rms=float(np.sqrt(mouth_error/max(1,np.count_nonzero(mouth_vertices))))
        records=[]
        for j in range(len(selected)):
            if np.linalg.norm(translation[j])+np.linalg.norm(rotvec[j])<1e-6:continue
            if np.linalg.norm(translation[j])>5 or np.linalg.norm(rotvec[j])>1.6:raise ValueError('Unstable facial calibration')
            records.append({'bone':j,'position':translation[j].round(8).tolist(),'rotation':rotvec[j].round(8).tolist()})
        supported=bool(records) and residual<.98
        reason='' if residual<.1 else '源顶点形变只能由现有骨骼近似' if supported else '现有面部骨骼无法重建该形状'
        # Tiny closed-mouth changes can have high relative error but negligible
        # visible displacement. Require a substantial absolute error as well.
        bad_mouth=mouth_residual>.5 and mouth_rms>mouth_span*.02
        if panels[k]==3 and (missing_teeth or bad_mouth):
            supported=False
            reason='嘴部缺少可靠的牙齿校准' if missing_teeth else '嘴部形状还原误差过大'
        expressions.append({'name':name,'panel':panels[k],'supported':supported,'residual':round(residual,6),'reason':reason,'deltas':records if supported else []})
        quality.append({'name':name,'residual':residual,'mouth_residual':mouth_residual,'mouth_rms':mouth_rms,'supported':supported,'reason':reason})
    key=model['name'].lower()
    # The male PMX's author suffix is not part of the game's character key.
    if key=='endminm_f':key='endminm'
    if not re.fullmatch(r'[a-z0-9_]{1,128}',key):
        raise ValueError('Model identity must match a game character key')
    profile={'version':1,'model':key,'label':label,'source_hash':model['hash'],
             'bones':[{'name':name,'rest':positions[i].tolist()} for i,name in enumerate(names)],'morphs':expressions}
    return profile,{'model':key,'label':label,'bones':len(selected),'morphs':quality,'skipped':skipped,'teeth':teeth_report,'missing_teeth':missing_teeth,'normalized_weight_vertices':model.get('weight_fixes',0),
                   'note':'Source PMX reconstruction error, not game visual acceptance.'}


def main():
    ap=argparse.ArgumentParser(description=__doc__)
    ap.add_argument('source',type=Path);ap.add_argument('--output',type=Path,required=True)
    ap.add_argument('--report',type=Path);ap.add_argument('--only',default='')
    args=ap.parse_args();args.output.mkdir(parents=True,exist_ok=True)
    paths=sorted(args.source.rglob('*.pmx')) if args.source.is_dir() else [args.source]
    reports=[];seen=set()
    for path in paths:
        if args.only and args.only not in path.stem:continue
        print('Calibrating:',path.stem,flush=True)
        try:
            model=read_pmx(path);profile,report=compile_profile(model,path.stem)
            report['source_name']=path.name;reports.append(report)
            if profile:
                identity=profile['model']+'-'+profile['source_hash'][:12]
                if identity in seen:continue
                seen.add(identity);dest=args.output/(identity+'.face.json');tmp=dest.with_suffix('.tmp')
                tmp.write_text(json.dumps(profile,ensure_ascii=False,separators=(',',':')),encoding='utf-8');tmp.replace(dest)
                usable=sum(m['supported'] for m in profile['morphs'])
                print(' ',profile['model'],usable,'/',len(profile['morphs']),'shapes',flush=True)
            else:print(' ',report['reason'],flush=True)
        except Exception as e:
            reports.append({'source_name':path.name,'error':str(e)});print(' ERROR:',e,flush=True)
    if args.report:
        args.report.parent.mkdir(parents=True,exist_ok=True)
        args.report.write_text(json.dumps(reports,ensure_ascii=False,indent=2),encoding='utf-8')
    if any('error' in r for r in reports):raise SystemExit(1)


if __name__=='__main__':main()
