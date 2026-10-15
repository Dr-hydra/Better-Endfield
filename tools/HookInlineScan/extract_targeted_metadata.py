"""Read only selected IL2CPP classes from the installed Endfield PE/metadata.

No game launch, full dump, database, or binary rewriting is needed. The local
v29 metadata has 92-byte type records (one extra word before member counts).
"""
from __future__ import annotations
import argparse
import json
import re
import struct
from pathlib import Path


class Pe:
    def __init__(self, path):
        self.data = Path(path).read_bytes()
        pe = struct.unpack_from('<I', self.data, 0x3c)[0]
        opt = pe + 24
        self.base = struct.unpack_from('<Q', self.data, opt + 24)[0]
        count = struct.unpack_from('<H', self.data, pe + 6)[0]
        start = opt + struct.unpack_from('<H', self.data, pe + 20)[0]
        self.sections = [struct.unpack_from('<IIII', self.data, start+i*40+8) for i in range(count)]

    def offset(self, va):
        rva = va-self.base if va >= self.base else va
        for vs, address, size, raw in self.sections:
            if address <= rva < address+size:
                return raw+rva-address
        raise ValueError(f'Unmapped address {va:#x}')

    def va(self, offset):
        for vs, address, size, raw in self.sections:
            if raw <= offset < raw+size:
                return self.base+address+offset-raw
        raise ValueError('Unmapped file offset')

    def unpack(self, fmt, va):
        return struct.unpack_from(fmt, self.data, self.offset(va))

    def references(self, va):
        marker = struct.pack('<Q', va)
        start = 0
        while True:
            pos = self.data.find(marker, start)
            if pos < 0:
                return
            start = pos+1
            if pos % 8 == 0:
                yield pos


class Metadata:
    def __init__(self, path, pe):
        self.data = Path(path).read_bytes()
        self.pe = pe
        if struct.unpack_from('<II', self.data) != (0xfab11baf, 29):
            raise ValueError('Expected Endfield metadata version 29')
        self.sections = [struct.unpack_from('<II', self.data, 8+i*8) for i in range(31)]
        self.type_start, type_size = self.sections[19]
        if type_size % 92:
            raise ValueError('Unsupported type-definition layout')
        self.type_count = type_size//92
        self.types = [struct.unpack_from('<17i8H2I', self.data, self.type_start+i*92) for i in range(self.type_count)]
        self.names = {}
        self.images = []
        off, size = self.sections[20]
        for p in range(off, off+size, 40):
            row = struct.unpack_from('<10i', self.data, p)
            self.images.append((row[2], row[2]+row[3], self.string(row[0])))
        candidates = []
        for pos in pe.references(self.type_count):
            if pos < 80 or pos+32 > len(pe.data):
                continue
            if struct.unpack_from('<Q', pe.data, pos+16)[0] != self.type_count:
                continue
            values = struct.unpack_from('<16Q', pe.data, pos-80)
            try:
                pe.offset(values[7]); pe.offset(values[11]); pe.offset(values[13])
            except ValueError:
                continue
            candidates.append(values)
        if len(candidates) != 1:
            raise ValueError(f'Expected one metadata registration, got {len(candidates)}')
        self.registration = candidates[0]
        self.modules = {}
        off, size = self.sections[7]
        self.defaults = {f: (t, d) for f, t, d in struct.iter_unpack('<iii', self.data[off:off+size])}

    def constant(self, field_index):
        item = self.defaults.get(field_index)
        if not item or item[1] < 0:
            return None
        typ, index = item
        p = self.sections[8][0]+index
        kind = (self.pe.unpack('<QI', self.typeptr(typ))[1] >> 16)&255
        if kind in (8, 9):
            b = self.data[p]
            if b < 0x80:
                value = b
            elif b < 0xc0:
                value = ((b&0x3f)<<8)|self.data[p+1]
            elif b < 0xe0:
                value = ((b&0x1f)<<24)|int.from_bytes(self.data[p+1:p+4], 'big')
            elif b == 0xf0:
                value = struct.unpack_from('<I', self.data, p+1)[0]
            elif b == 0xfe:
                value = 0xfffffffe
            elif b == 0xff:
                value = 0xffffffff
            else:
                raise ValueError(f'Invalid compressed default {b:#x}')
            return (value >> 1) ^ -(value & 1) if kind == 8 else value
        formats = {2:'?',3:'H',4:'b',5:'B',6:'h',7:'H',10:'q',11:'Q',12:'f',13:'d'}
        return struct.unpack_from('<'+formats[kind], self.data, p)[0] if kind in formats else None

    def string(self, index):
        start = self.sections[2][0]+index
        end = self.data.index(b'\0', start)
        return self.data[start:end].decode('utf-8')

    def typename(self, index):
        if index in self.names:
            return self.names[index]
        row = self.types[index]
        name, ns = self.string(row[0]), self.string(row[1])
        # declaringType is an Il2CppType index, not a type-definition index.
        if row[3] >= 0:
            parent = self.resolve(row[3])
            full = parent+'.'+name
        else:
            full = (ns+'.' if ns else '')+name
        self.names[index] = full
        return full

    def typeptr(self, index):
        if not 0 <= index < self.registration[6]:
            raise ValueError(f'Invalid type index {index}')
        return self.pe.unpack('<Q', self.registration[7]+index*8)[0]

    def resolve(self, index):
        return self.resolve_ptr(self.typeptr(index))

    def resolve_ptr(self, ptr, depth=0):
        if depth > 12:
            raise ValueError('Recursive IL2CPP type')
        data, bits = self.pe.unpack('<QI', ptr)
        kind = (bits >> 16)&255
        primitive = {1:'Void',2:'Boolean',3:'Char',4:'SByte',5:'Byte',6:'Int16',7:'UInt16',8:'Int32',9:'UInt32',10:'Int64',11:'UInt64',12:'Single',13:'Double',14:'String',24:'IntPtr',25:'UIntPtr',28:'Object',22:'TypedReference'}
        if kind in primitive:
            name = 'System.'+primitive[kind]
        elif kind in (17,18):
            name = self.typename(data)
        elif kind in (15,16,29):
            name = self.resolve_ptr(data,depth+1)+{15:'*',16:'&',29:'[]'}[kind]
        elif kind == 20:
            elem, rank = self.pe.unpack('<QB', data)
            name = self.resolve_ptr(elem,depth+1)+'['+','*(rank-1)+']'
        elif kind == 21:
            typ, inst = self.pe.unpack('<QQ', data)
            count, args = self.pe.unpack('<QQ', inst)
            names = [self.resolve_ptr(self.pe.unpack('<Q',args+i*8)[0],depth+1) for i in range(count)]
            name = self.resolve_ptr(typ,depth+1)+'<'+','.join(names)+'>'
        elif kind in (19,30):
            off = self.sections[12][0]+data*16
            name = self.string(struct.unpack_from('<i',self.data,off+4)[0])
        else:
            name = f'<type:{kind:#x}>'
        # Unity 2021 v29: num_mods:5, byref:1, pinned:1, valuetype:1.
        if bits & (1 << 29) and not name.endswith('&'):
            name += '&'
        return name

    def module(self, assembly):
        if assembly in self.modules:
            return self.modules[assembly]
        matches = []
        start = 0
        marker = assembly.encode()+b'\0'
        while True:
            pos = self.pe.data.find(marker,start)
            if pos < 0:
                break
            start = pos+1
            try:
                va = self.pe.va(pos)
            except ValueError:
                continue
            for ref in self.pe.references(va):
                _, count, methods = struct.unpack_from('<3Q',self.pe.data,ref)
                if not 0 < count < 1000000:
                    continue
                try:
                    self.pe.offset(methods); self.pe.offset(methods+(count-1)*8)
                except ValueError:
                    continue
                matches.append((count,methods))
        matches = list(set(matches))
        if len(matches) != 1:
            raise ValueError(f'{assembly}: expected one codegen module, got {matches}')
        self.modules[assembly] = matches[0]
        return matches[0]

    def extract(self, selected, include_ifix=True):
        methods, classes = [], []
        for index,row in enumerate(self.types):
            short = self.string(row[0])
            if short not in selected and not (include_ifix and 'WrappersManager' in short):
                continue
            full = self.typename(index)
            asm = next(name for lo,hi,name in self.images if lo <= index < hi)
            ns = self.string(row[1])
            count, pointers = self.module(asm)
            fields = []
            offsets = self.pe.unpack('<Q',self.registration[11]+index*8)[0]
            for j in range(row[19]):
                name, typ, token = struct.unpack_from('<iiI',self.data,self.sections[11][0]+(row[8]+j)*12)
                offset = self.pe.unpack('<i',offsets+j*4)[0] if offsets else None
                field = dict(name=self.string(name),type=self.resolve(typ),offset=offset,token=token)
                value = self.constant(row[8]+j)
                if value is not None:
                    field['value'] = value
                fields.append(field)
            classes.append(dict(asm=asm,cls=full,ns=ns,short=short,type_index=index,fields=fields))
            for j in range(row[17]):
                p=self.sections[5][0]+(row[9]+j)*32
                name, owner, ret, params, generic, token, flags, iflags, slot, argc=struct.unpack_from('<5iI4H',self.data,p)
                types=[]
                for k in range(argc):
                    typ=struct.unpack_from('<i',self.data,self.sections[10][0]+(params+k)*12+8)[0]
                    types.append(self.resolve(typ))
                rid=token&0xffffff
                if not 1 <= rid <= count:
                    raise ValueError(f'{full}: invalid method token {token:#x}')
                va=self.pe.unpack('<Q',pointers+(rid-1)*8)[0]
                mods=[m for bit,m in [(0x10,'static'),(0x40,'virtual'),(0x400,'abstract')] if flags&bit]
                methods.append(dict(asm=asm,cls=full,ns=ns,short=short,name=self.string(name),ret=self.resolve(ret),types=types,count=argc,rva=va-self.pe.base if va else 0,token=token,modifiers=mods))
        return methods,classes


def main():
    p=argparse.ArgumentParser(description=__doc__)
    p.add_argument('--game',required=True,type=Path)
    p.add_argument('--repo',type=Path,default=Path(__file__).resolve().parents[2])
    p.add_argument('--output',required=True,type=Path)
    args=p.parse_args()
    literals=set()
    for base in ['native/modules','native/shared']:
        for path in (args.repo/base).rglob('*'):
            if path.suffix in ('.h','.cpp','.hpp','.inc'):
                literals.update(re.findall(r'"([A-Za-z_][A-Za-z0-9_`.<>+/]*)"',path.read_text(encoding='utf-8',errors='replace')))
    selected=set(literals)
    selected.update(re.split(r'[./+]', x)[-1] for x in literals)
    selected.update(['Modifier','ModifierType','DamagePackData','Attributes','BuffStackingSettings','AttributeType','AttributeModifier','ItemInstData','WeaponInstData','EquipInstData','FindFloorResult','RepeatedField`1','MapField`2'])
    pe=Pe(args.game/'GameAssembly.dll')
    metadata=Metadata(args.game/'Endfield_Data/il2cpp_data/Metadata/global-metadata.dat',pe)
    methods,classes=metadata.extract(selected)
    files = {name:dict(size=(args.game/name).stat().st_size,mtime_ns=(args.game/name).stat().st_mtime_ns) for name in ['GameAssembly.dll','Endfield_Data/il2cpp_data/Metadata/global-metadata.dat']}
    result=dict(source=dict(game=str(args.game),metadata_version=29,files=files,scope='source-referenced class names and IFix wrappers; no full dump',type_count=metadata.type_count),methods=methods,classes=classes)
    args.output.parent.mkdir(parents=True,exist_ok=True)
    args.output.write_text(json.dumps(result,ensure_ascii=False,indent=2)+'\n',encoding='utf-8')
    print(json.dumps(dict(output=str(args.output),classes=len(classes),methods=len(methods),codegen_modules=len(metadata.modules))))


if __name__=='__main__':
    main()
