"""Read D3D11 resource reflection from offline Shader blobs; never rewrite code."""
import argparse
import json
import struct
import re
from pathlib import Path


class Reader:
    def __init__(self, data, offset=0): self.data, self.pos = data, offset
    def take(self, count):
        if count < 0 or self.pos+count > len(self.data): raise ValueError('truncated shader data')
        result=self.data[self.pos:self.pos+count]; self.pos+=count; return result
    def u32(self): return struct.unpack('<I',self.take(4))[0]
    def align(self): self.take((-self.pos)%4)
    def vector(self):
        count=self.u32()
        if count>1_000_000: raise ValueError('excessive shader vector')
        return list(struct.unpack('<'+str(count)+'I',self.take(count*4)))
    def nested(self):
        count=self.u32()
        if count>128: raise ValueError('excessive shader platforms')
        return [self.vector() for _ in range(count)]


def lz4_block(data, expected):
    if not 0<=expected<=128*1024*1024: raise ValueError('excessive shader block')
    reader=Reader(data); out=bytearray()
    def length(n):
        if n==15:
            while True:
                extra=reader.take(1)[0]; n+=extra
                if extra!=255: break
        return n
    while reader.pos<len(data):
        token=reader.take(1)[0]; literal=length(token>>4)
        if len(out)+literal>expected: raise ValueError('LZ4 literals exceed output')
        out.extend(reader.take(literal))
        if reader.pos==len(data): break
        distance=struct.unpack('<H',reader.take(2))[0]; count=length(token&15)+4
        if not 0<distance<=len(out) or len(out)+count>expected: raise ValueError('invalid LZ4 match')
        pattern=out[-distance:]
        out.extend(pattern*(count//distance)+pattern[:count%distance])
    if len(out)!=expected: raise ValueError('LZ4 output length differs')
    return bytes(out)


def shader_blobs(metadata, raw):
    expected=['m_EnableShaderLODStreaming','subShaderBlobs','platforms','offsets',
              'compressedLengths','decompressedLengths','compressedBlob','stageCounts','m_CompressionType']
    roots=[n['name'] for n in metadata['suffix_schema'] if n['level']==1]
    if roots[:len(expected)]!=expected: raise ValueError('unsupported native Shader suffix')
    r=Reader(raw, metadata['metadata_bytes']); r.take(1); r.align()
    count=r.u32()
    if count>32: raise ValueError('excessive subshader LODs')
    blobs=[]
    for _ in range(count):
        lod=r.u32(); blob=r.take(r.u32()); r.align()
        offsets, compressed, decompressed=r.nested(),r.nested(),r.nested()
        blobs.append((lod,blob,offsets,compressed,decompressed))
    platforms=r.vector(); offsets,compressed,decompressed=r.nested(),r.nested(),r.nested()
    blob=r.take(r.u32()); r.align(); r.vector(); compression=r.u32()
    if compression!=3: raise ValueError(f'unsupported shader compression {compression}')
    if blob: blobs.append((None,blob,offsets,compressed,decompressed))
    return platforms,blobs


def reflect_dxbc(data):
    """Read RDEF entries only when the complete DXBC container is bounded/valid."""
    results=[]; start=0
    while True:
        pos=data.find(b'DXBC',start)
        if pos<0: break
        start=pos+4
        if pos+32>len(data): continue
        _,size,count=struct.unpack_from('<3I',data,pos+20)
        if count>64 or size<32+4*count or pos+size>len(data): continue
        chunks={}
        for offset in struct.unpack_from('<'+str(count)+'I',data,pos+32):
            if offset+8>size: raise ValueError('DXBC chunk outside container')
            tag,length=struct.unpack_from('<4sI',data,pos+offset)
            if offset+8+length>size: raise ValueError('DXBC chunk data outside container')
            chunks[tag]=data[pos+offset+8:pos+offset+8+length]
        program=chunks.get(b'SHEX',chunks.get(b'SHDR',b''))
        stage=struct.unpack_from('<I',program)[0]>>16 if len(program)>=4 else None
        resources=[]; rdef=chunks.get(b'RDEF')
        if rdef:
            if len(rdef)<28: raise ValueError('truncated RDEF')
            count,offset=struct.unpack_from('<2I',rdef,8)
            if count>4096 or offset+32*count>len(rdef): raise ValueError('RDEF bindings outside chunk')
            for i in range(count):
                name,kind,slot,slots,flags,return_type,dimension,samples=struct.unpack_from('<8I',rdef,offset+i*32)
                end=rdef.find(b'\0',name)
                if name>=len(rdef) or end<name: raise ValueError('RDEF name outside chunk')
                resources.append({'name':rdef[name:end].decode('utf-8'),'kind':kind,'slot':slot,'count':slots,
                                  'dimension':dimension})
        results.append({'offset':pos,'size':size,'stage':stage,'reflection_present':rdef is not None,'resources':resources})
        start=pos+size
    return results


def descriptor_groups(data):
    """Recognize HG's bounded, trailing named descriptor groups (202506210).

    Preserve packed binding words as evidence; never treat them as Unity property IDs.
    """
    if len(data)<24 or struct.unpack_from('<2I',data)!=(202506210,9): return None
    candidates=[]; marker=struct.pack('<I',6)+b'Global\0\0'
    start=0
    while True:
        pos=data.find(marker,start)
        if pos<4: break
        start=pos+1
        try:
            r=Reader(data,pos-4); count=r.u32()
            if not 1<=count<=8: continue
            groups=[]
            def string():
                size=r.u32()
                if not 0<size<=256: raise ValueError('invalid descriptor name')
                value=r.take(size).decode('ascii'); r.align(); return value
            for _ in range(count):
                name=string(); set_index,n,variable_index=r.u32(),r.u32(),r.u32()
                if name not in ('Global','PerMaterial','PerObject') or n>512: raise ValueError('unknown descriptor group')
                bindings=[]
                for _ in range(n):
                    prop=string(); binding,kind,packed,flags=struct.unpack('<4I',r.take(16))
                    bindings.append({'name':prop,'binding':binding,'kind':kind,'packed_binding':packed,'flags':flags,
                                     'pixel_slot':(packed>>16)&255})
                groups.append({'name':name,'set':set_index,'variable_index':variable_index,'bindings':bindings})
            if r.pos==len(data): candidates.append(groups)
        except (ValueError,UnicodeDecodeError): pass
    if len(candidates)>1: raise ValueError('ambiguous descriptor tail')
    return candidates[0] if candidates else None


def d3d11_texture_slots(container):
    # Offline disassembly only, through the Windows SDK runtime; no GPU/device/game.
    import ctypes
    dll=ctypes.WinDLL('d3dcompiler_47.dll')
    disassemble=dll.D3DDisassemble
    disassemble.argtypes=[ctypes.c_void_p,ctypes.c_size_t,ctypes.c_uint,ctypes.c_char_p,ctypes.POINTER(ctypes.c_void_p)]
    disassemble.restype=ctypes.c_long
    buffer=ctypes.create_string_buffer(container); blob=ctypes.c_void_p()
    status=disassemble(buffer,len(container),0,None,ctypes.byref(blob))
    if status<0 or not blob.value: raise ValueError(f'D3DDisassemble failed: {status}')
    table=ctypes.cast(blob,ctypes.POINTER(ctypes.POINTER(ctypes.c_void_p))).contents
    try:
        pointer=ctypes.WINFUNCTYPE(ctypes.c_void_p,ctypes.c_void_p)(table[3])(blob)
        size=ctypes.WINFUNCTYPE(ctypes.c_size_t,ctypes.c_void_p)(table[4])(blob)
        text=ctypes.string_at(pointer,size).decode('ascii')
        return sorted({int(m[1]) for m in re.finditer(r'^dcl_resource_.*?\bt(\d+)\b',text,re.M)})
    finally:
        ctypes.WINFUNCTYPE(ctypes.c_ulong,ctypes.c_void_p)(table[2])(blob)


def inspect(metadata, raw):
    platforms,blobs=shader_blobs(metadata,raw); records=[]; parameters=[]
    for lod,blob,offsets,compressed,decompressed in blobs:
        for platform_index,platform in enumerate(platforms):
            if platform!=4: continue  # Unity ShaderCompilerPlatform.D3D11
            if len(offsets[platform_index])!=len(compressed[platform_index]) or len(offsets[platform_index])!=len(decompressed[platform_index]):
                raise ValueError('shader segment tables differ')
            segments=[]
            for offset,size,decoded in zip(offsets[platform_index],compressed[platform_index],decompressed[platform_index]):
                if offset+size>len(blob): raise ValueError('shader compressed segment outside blob')
                segments.append(lz4_block(blob[offset:offset+size],decoded))
            if not segments: continue
            r=Reader(segments[0]); count=r.u32()
            if count>100_000: raise ValueError('excessive shader subprograms')
            entries=[struct.unpack('<3I',r.take(12)) for _ in range(count)]
            for i,(offset,size,segment) in enumerate(entries):
                if segment>=len(segments) or offset+size>len(segments[segment]): raise ValueError('shader program outside segment')
                data=segments[segment][offset:offset+size]
                containers=reflect_dxbc(data)
                groups=descriptor_groups(data)
                if groups is not None: parameters.append({'lod':lod,'program':i,'groups':groups})
                if containers: records.append({'lod':lod,'program':i,'segment':segment,'containers':containers})
    return {'schema':1,'shader_id':metadata['id'],'shader_name':metadata['parsed_form']['m_Name'],
            'bytecode_rewritten':False,'records':records,'parameters':parameters}


def main():
    parser=argparse.ArgumentParser(description=__doc__)
    parser.add_argument('metadata',type=Path); parser.add_argument('--output',type=Path,required=True)
    args=parser.parse_args(); metadata=json.loads(args.metadata.read_text(encoding='utf-8'))
    raw_path=(args.metadata.parent/metadata['raw_file']).resolve()
    if raw_path.parent!=args.metadata.parent.resolve(): raise ValueError('raw shader path outside metadata directory')
    result=inspect(metadata,raw_path.read_bytes())
    args.output.parent.mkdir(parents=True,exist_ok=True)
    args.output.write_text(json.dumps(result,ensure_ascii=False,indent=2),encoding='utf-8')
    print(json.dumps({'shader':result['shader_name'],'programs':len(result['records']),
        'containers':sum(len(r['containers']) for r in result['records']),
        'reflected':sum(c['reflection_present'] for r in result['records'] for c in r['containers'])}))


if __name__=='__main__': main()
