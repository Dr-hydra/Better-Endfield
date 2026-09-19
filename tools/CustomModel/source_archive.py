"""Read RAR/7z members through bundled 7-Zip, without extracting source paths."""
from collections import OrderedDict
from pathlib import Path, PurePosixPath
import re
import subprocess
import sys
import threading

MAX_MEMBER = 512 * 1024**2
MAX_TOTAL = 4 * 1024**3
MAX_FILES = 8192
CACHE_LIMIT = 128 * 1024**2


def backend():
    root = Path(sys.executable).parent if getattr(sys, 'frozen', False) else Path(__file__).resolve().parents[2]/'artifacts/bem-archive-backend'
    path = root/'7zip/7z.exe'
    if not path.is_file() or not path.with_name('7z.dll').is_file():
        raise ValueError('ARCHIVE_BACKEND: 缺少随工具附带的 7-Zip 解压组件，请重新部署完整工具目录。')
    return path


def run(arguments, limit, timeout=120):
    exe=backend()
    process=subprocess.Popen([str(exe),*arguments],stdin=subprocess.DEVNULL,
        stdout=subprocess.PIPE,stderr=subprocess.DEVNULL,cwd=exe.parent,
        creationflags=getattr(subprocess,'CREATE_NO_WINDOW',0))
    timed_out=threading.Event()
    def expire():
        timed_out.set();process.kill()
    timer=threading.Timer(timeout,expire);timer.daemon=True;timer.start()
    data=bytearray()
    try:
        while True:
            chunk=process.stdout.read(min(1024**2,limit+1-len(data)))
            if not chunk: break
            data.extend(chunk)
            if len(data)>limit: raise ValueError('ARCHIVE_LIMIT: 解压数据超出声明大小或读取上限。')
        code=process.wait()
        if timed_out.is_set(): raise ValueError('ARCHIVE_TIMEOUT: 解压超时，请检查压缩包。')
        if code: raise ValueError('ARCHIVE_READ: 无法读取压缩包；可能损坏、需要密码或缺少分卷。请提供完整的无密码压缩包。')
        return bytes(data)
    finally:
        timer.cancel()
        if process.poll() is None: process.kill();process.wait()
        process.stdout.close()


def parse_listing(text):
    files={}; identities=set();total=0;entries=0
    for block in re.split(r'\r?\n\s*\r?\n',text.strip()):
        if not block.strip(): continue
        row={}
        for line in block.splitlines():
            key,separator,val=line.partition(' =')
            val=val.removeprefix(' ')
            if not separator or key in row: raise ValueError('ARCHIVE_LIST: 无法可靠解析压缩包目录。')
            row[key]=val
        entries+=1
        if entries>MAX_FILES: raise ValueError('ARCHIVE_LIMIT: 压缩包条目过多。')
        name=row.get('Path','').replace('\\','/')
        path=PurePosixPath(name)
        if not name or name.startswith('@') or path.is_absolute() or '..' in path.parts or ':' in name or any(ord(c)<32 for c in name):
            raise ValueError('ARCHIVE_PATH: 压缩包包含不安全的资源路径。')
        identity=str(path).casefold()
        if identity in identities: raise ValueError('ARCHIVE_PATH: 压缩包包含重复或大小写冲突的路径。')
        identities.add(identity)
        if row.get('Encrypted')=='+': raise ValueError('ARCHIVE_PASSWORD: 暂不支持需要密码的源压缩包。')
        if any('link' in k.lower() and v for k,v in row.items()): raise ValueError('ARCHIVE_PATH: 不支持压缩包内的链接。')
        if row.get('Folder')=='+' or row.get('Attributes','').startswith('D'): continue
        if not row.get('Size','').isdigit(): raise ValueError('ARCHIVE_LIST: 缺少资源大小。')
        size=int(row['Size']);total+=size
        if size>MAX_MEMBER or total>MAX_TOTAL: raise ValueError('ARCHIVE_LIMIT: 源包超过单文件 512 MiB / 总计 4 GiB 上限。')
        files[name]=size
    if not files: raise ValueError('ARCHIVE_EMPTY: 压缩包内没有可读取的文件。')
    return files


class PackedSource:
    def __init__(self,path):
        self.path=Path(path).resolve()
        if self.path.stat().st_size>MAX_TOTAL: raise ValueError('ARCHIVE_LIMIT: 源压缩包超过 4 GiB。')
        with self.path.open('rb') as f: magic=f.read(8)
        expected=b'7z\xbc\xaf\x27\x1c' if self.path.suffix.lower()=='.7z' else b'Rar!\x1a\x07'
        if not magic.startswith(expected): raise ValueError('ARCHIVE_FORMAT: 压缩包签名与扩展名不符，或文件已损坏。')
        raw=run(['l','-slt','-ba','-sccUTF-8','-p-','--',str(self.path)],8*1024**2)
        self.files=parse_listing(raw.decode('utf-8-sig'))
        self.names=list(self.files);self.cache=OrderedDict();self.cached_size=0

    def read(self,name):
        if name not in self.files: raise ValueError('ARCHIVE_RESOURCE: 缺少资源 '+name)
        if name in self.cache:
            self.cache.move_to_end(name);return self.cache[name]
        size=self.files[name]
        data=run(['x','-so','-spd','-y','-p-','--',str(self.path),name],size)
        if len(data)!=size: raise ValueError('ARCHIVE_READ: 解压后的资源大小与目录不符。')
        if size<=CACHE_LIMIT:
            while self.cached_size+size>CACHE_LIMIT:
                _,old=self.cache.popitem(last=False);self.cached_size-=len(old)
            self.cache[name]=data;self.cached_size+=size
        return data

    def close(self):
        self.cache.clear();self.cached_size=0
