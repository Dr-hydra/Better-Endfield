"""Read typed string references using the current game's serializer schema.

Only actual String fields are resolved through USPC. Integers elsewhere in a
payload are never interpreted as string IDs. Parsing must consume the file.
"""
import json
import struct
from pathlib import Path
from string_pool import StringPool
from extract_memorypack_schema import type_args


class Reader:
    def __init__(self, data, schema, pool):
        self.data, self.schema, self.pool = data, schema, pool
        self.pos = 0
        self.strings = set()
        self.context = []

    def unpack(self, fmt):
        fmt = '<'+fmt
        size = struct.calcsize(fmt)
        if self.pos+size > len(self.data):
            raise ValueError(f'Truncated {fmt} at {self.pos}')
        value = struct.unpack_from(fmt, self.data, self.pos)
        self.pos += size
        return value[0] if len(value) == 1 else value

    def read(self, name, use_formatter=False):
        if len(self.context) > 100:
            raise ValueError('MemoryPack nesting exceeds 100')
        self.context.append(name)
        try:
            return self._read(name, use_formatter)
        except (ValueError, KeyError, struct.error) as error:
            if not hasattr(error, 'memorypack_context'):
                error.memorypack_context = f"offset={self.pos} path={' > '.join(self.context)}"
            raise
        finally:
            self.context.pop()

    def _read(self, name, use_formatter=False):
        primitives = {'System.Boolean':'B','System.Byte':'B','System.SByte':'b','System.Int16':'h','System.UInt16':'H','System.Int32':'i','System.UInt32':'I','System.Int64':'q','System.UInt64':'Q','System.Single':'f','System.Double':'d'}
        if name in primitives:
            result = self.unpack(primitives[name])
            if name == 'System.Boolean' and result not in (0,1):
                raise ValueError(f'Invalid Boolean {result}')
            return result
        if name == 'System.String':
            value = self.pool.get(self.unpack('i'))
            if value: self.strings.add(value.lower())
            return value
        raw = {'UnityEngine.Vector2':2,'UnityEngine.Vector3':3,'UnityEngine.Quaternion':4,'UnityEngine.Color':4}
        if name in raw:
            return self.unpack('f'*raw[name])
        if name in ('UnityEngine.LayerMask', 'Beyond.Audio.AudioId'):
            return self.unpack('i')
        if name == 'UnityEngine.AnimationCurve':
            count = self.unpack('B')
            if count == 255: return None
            if count != 3: raise ValueError(f'AnimationCurve member count {count}')
            self.unpack('ii')
            keys = self.unpack('i')
            if keys == -1: return None
            if not 0 <= keys <= (len(self.data)-self.pos)//28:
                raise ValueError(f'Invalid AnimationCurve key count {keys}')
            self.pos += keys*28
            return None
        if name.endswith('[]') or name.startswith('System.Collections.Generic.List`1<'):
            element = name[:-2] if name.endswith('[]') else type_args(name)[0]
            count = self.unpack('i')
            if count == -1: return None
            if not 0 <= count <= len(self.data)-self.pos:
                raise ValueError(f'Invalid collection count {count}')
            # Arrays of unmanaged structs are bulk-copied. List<T> calls the
            # registered per-element formatter, including generated wrappers.
            for _ in range(count): self.read(element, not name.endswith('[]'))
            return None
        if name.startswith('Beyond.SerializeFieldDictionary`2<'):
            header = self.unpack('B')
            if header == 255: return None
            if header != 1: raise ValueError(f'SerializeFieldDictionary header {header}')
            count = self.unpack('i')
            if not 0 <= count <= (len(self.data)-self.pos)//2:
                raise ValueError(f'Invalid dictionary count {count}')
            key_type, value_type = type_args(name)
            for _ in range(count):
                self.read(key_type, True)
                self.read(value_type, True)
            return None
        definition = self.schema[name]
        if 'enum' in definition:
            return self.read(definition['enum'])
        if 'raw_size' in definition and not (use_formatter and 'wrapper' in definition):
            size = definition['raw_size']
            if self.pos+size > len(self.data): raise ValueError(f'Truncated {name}')
            self.pos += size
            return None
        if 'union_candidates' in definition:
            tag = self.unpack('B')
            if tag == 255: return None
            if tag == 250: tag = self.unpack('H')
            actual = definition.get('union', {}).get(str(tag))
            if actual is None:
                raise ValueError(f'Unknown union tag {name}: {tag}')
            return self.read(actual)
        if 'wrapper' not in definition:
            raise ValueError(f'No formatter for {name}')
        count = self.unpack('B')
        if count == 255: return None
        fields = definition['fields']
        if count != len(fields):
            raise ValueError(f'{name} member count {count} != {len(fields)}')
        for field, typ in fields:
            self.context.append(field)
            try: self.read(typ)
            finally: self.context.pop()
        return None


class ReferenceReader:
    def __init__(self, schema_path: Path, pool_root: Path):
        self.schema = json.loads(schema_path.read_text(encoding='utf-8'))
        self.pool = StringPool(pool_root)

    def read(self, path: Path):
        typ = 'Beyond.Gameplay.Core.'+path.parent.name
        reader = Reader(path.read_bytes(), self.schema, self.pool)
        try:
            reader.read(typ)
            if reader.pos != len(reader.data):
                raise ValueError(f'Unread bytes: {len(reader.data)-reader.pos}')
        except Exception as error:
            raise ValueError(f'{path.name}: {error}; {getattr(error,"memorypack_context", "")}') from error
        return reader.strings
