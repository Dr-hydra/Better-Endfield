#!/usr/bin/env python3
"""Validate resource archives and build/publish the unified distribution index."""
import argparse
import json
import re
import subprocess
import tempfile
import zipfile
from datetime import datetime, timezone
from pathlib import Path
from BuildResourcePacks import relative, verify

REPOSITORY = 'Dr-hydra/Better-Endfield'
TAG = 'game-resources'

class GitHub:
    def __init__(self):
        from PublishRelease import credentials
        self.token=credentials()
    def request(self,path,data=None,method=None):
        import urllib.request
        headers={'Authorization':'Bearer '+self.token,'User-Agent':'Better-Endfield-resource-release','Accept':'application/vnd.github+json','Content-Type':'application/json'}
        raw=None if data is None else json.dumps(data).encode()
        request=urllib.request.Request('https://api.github.com/repos/'+REPOSITORY+'/'+path,raw,headers,method=method)
        with urllib.request.urlopen(request,timeout=60) as response:return None if response.status==204 else json.load(response)
    def download(self,url):
        import urllib.request
        request=urllib.request.Request(url,headers={'User-Agent':'Better-Endfield-resource-release'})
        with urllib.request.urlopen(request,timeout=180) as response:return response.read()
    def upload(self,release,path):
        import http.client,urllib.parse
        url=urllib.parse.urlsplit(release['upload_url'].split('{')[0]+'?name='+urllib.parse.quote(path.name))
        connection=http.client.HTTPSConnection(url.hostname,timeout=300)
        print('Uploading: '+path.name+' ('+str(path.stat().st_size)+' bytes)',flush=True)
        try:
            connection.putrequest('POST',url.path+'?'+url.query)
            for key,value in {'Authorization':'Bearer '+self.token,'User-Agent':'Better-Endfield-resource-release','Content-Type':'application/octet-stream','Content-Length':str(path.stat().st_size)}.items():connection.putheader(key,value)
            connection.endheaders()
            with path.open('rb') as stream:
                while data:=stream.read(1024*1024):connection.send(data)
            response=connection.getresponse();body=response.read()
            if response.status!=201:raise RuntimeError('GitHub upload HTTP '+str(response.status))
            result=json.loads(body)
            if result['size']!=path.stat().st_size or result.get('state')!='uploaded':raise ValueError('Uploaded resource verification failed')
            return result
        finally:connection.close()

def build_index(archives, software):
    entries = []
    seen = set()
    for archive in sorted(archives):
        with tempfile.TemporaryDirectory(prefix='be-resource-verify-') as temporary:
            root = Path(temporary)
            with zipfile.ZipFile(archive) as z:
                names = set()
                for item in z.infolist():
                    if item.is_dir():
                        continue
                    relative(item.filename)
                    if item.filename in names or (item.external_attr >> 16) & 0o170000 == 0o120000:
                        raise ValueError('Duplicate entry or symbolic link')
                    names.add(item.filename)
                z.extractall(root)
            manifest = verify(root)
            if manifest['packageType']=='preview':
                source=manifest.get('source',{})
                characters=json.loads((root/'characters.json').read_text(encoding='utf-8'))['characters']
                if source.get('coverage')!='all playable characters in current source manifest' or source.get('characterCount')!=len(characters) or not characters:
                    raise ValueError('Release previews require a complete --preview-report; diagnostic single-character archives cannot be published')
            key = (manifest['packageType'], manifest['platform'], manifest['version'])
            if key in seen:
                raise ValueError('Duplicate resource version: ' + str(key))
            seen.add(key)
            if manifest['gameVersion'] != '.'.join(manifest['version'].split('.')[:3]):
                raise ValueError('Resource/game version mismatch')
            expected = 'BE-%s-%s-%s.zip' % (key[0].title(), key[1].title(), key[2])
            if archive.name != expected:
                raise ValueError('Expected archive name: ' + expected)
            entries.append(dict(packageType=key[0], platform=key[1], version=key[2],
                gameVersion=manifest['gameVersion'], resourceApi=1, asset=archive.name,
                url=f'https://github.com/{REPOSITORY}/releases/download/{TAG}/{archive.name}',
                bytes=archive.stat().st_size, expandedBytes=sum(manifest['files'].values()),
                optional=key[0]=='preview'))
    tools={}
    for asset in software.get('assets',[]):
        match=re.match(r'BEM-Tools-([0-9]+[.][0-9]+[.][0-9]+)-',asset['name'])
        name='bem'
        if not match:
            match=re.match(r'dsh-bem-creator-([0-9]+[.][0-9]+[.][0-9]+)[.]tgz$',asset['name']);name='dsh'
        if match:
            tool=tools.setdefault(name,dict(version=match[1],assets=[]))
            if tool['version']!=match[1]:raise ValueError('Mixed versions for one tool')
            tool['assets'].append(asset)
    return dict(schemaVersion=1, kind='betterendfield-distribution-index',
        generatedAtUtc=datetime.now(timezone.utc).isoformat(), software=software, tools=tools, resources=entries)

def main():
    ap=argparse.ArgumentParser(description=__doc__)
    ap.add_argument('--archives',type=Path)
    ap.add_argument('--refresh-software',action='store_true',help='Preserve published resources and update software metadata only')
    ap.add_argument('--output',type=Path,required=True)
    ap.add_argument('--software-index',type=Path)
    ap.add_argument('--publish',action='store_true')
    args=ap.parse_args()
    api=None
    if args.software_index:
        software=json.loads(args.software_index.read_text(encoding='utf-8-sig'))
    else:
        api=GitHub();release=api.request('releases/latest')
        if release['tag_name']==TAG or release.get('draft') or release.get('prerelease'):raise ValueError('Latest must be a stable software release')
        software=dict(version=release['tag_name'].removeprefix('v'),releaseUrl=release['html_url'],assets=[dict(name=a['name'],url=a['browser_download_url'],bytes=a['size']) for a in release['assets']])
    archives=list(args.archives.glob('BE-*.zip')) if args.archives else []
    if not archives and not args.refresh_software:raise ValueError('No resource archives found')
    if args.refresh_software and not args.publish:raise ValueError('--refresh-software requires --publish')
    index=build_index(archives,software)
    if args.publish:
        if args.output.name!='distribution-index.json':raise ValueError('Published index must be named distribution-index.json')
        api=api or GitHub()
        import urllib.error
        try:release=api.request('releases/tags/'+TAG)
        except urllib.error.HTTPError as error:
            if error.code!=404:raise
            if args.refresh_software:raise ValueError('Resource release is not initialized') from error
            release=api.request('releases',dict(tag_name=TAG,name='Game resources',make_latest='false',draft=True,body='Versioned core and optional BC7 / ASTC preview resources. Import ZIPs through the application or use automatic resource updates. Preview geometry is generated once from the Windows game; Android uses offline ASTC transcoding.'))
        existing={}
        for page in range(1,101):
            batch=api.request(f'releases/{release["id"]}/assets?per_page=100&page={page}')
            existing.update({a['name']:a for a in batch})
            if len(batch)<100:break
        source=existing.get('distribution-index.json') or existing.get('distribution-index.previous.json')
        if source:
            previous_bytes=api.download(source['browser_download_url'])
            previous=json.loads(previous_bytes)
            if previous.get('schemaVersion')!=1 or previous.get('kind')!=index['kind']:raise ValueError('Existing index schema is incompatible')
            old={(e['packageType'],e['platform'],e['version']):e for e in previous['resources']}
            for entry in index['resources']:
                key=(entry['packageType'],entry['platform'],entry['version'])
                if key in old and old[key]!=entry:raise ValueError('Published metadata differs; increment revision')
                old[key]=entry
            index['resources']=list(old.values())
        elif args.refresh_software:raise ValueError('Published distribution index is unavailable')
        required={(kind,platform) for kind in ('core','preview') for platform in ('windows','android')}
        if not required<={(e['packageType'],e['platform']) for e in index['resources']}:raise ValueError('Initial distribution requires all four resource classes')
        for archive in archives:
            if archive.name in existing:
                if existing[archive.name]['size']!=archive.stat().st_size or api.download(existing[archive.name]['browser_download_url'])!=archive.read_bytes():raise ValueError('Published archive differs; increment revision: '+archive.name)
            else:api.upload(release,archive)
    args.output.parent.mkdir(parents=True,exist_ok=True)
    args.output.write_text(json.dumps(index,ensure_ascii=False,indent=2)+chr(10),encoding='utf-8')
    if args.publish:
        if 'distribution-index.json' in existing:
            # Keep the last usable entry recoverable if replacing the main
            # GitHub asset is interrupted between deletion and upload.
            with tempfile.TemporaryDirectory(prefix='be-index-backup-') as temporary:
                backup=Path(temporary)/'distribution-index.previous.json';backup.write_bytes(previous_bytes)
                if backup.name in existing:api.request('releases/assets/'+str(existing[backup.name]['id']),method='DELETE')
                api.upload(release,backup)
        # All immutable payloads are available before switching the small index.
        if 'distribution-index.json' in existing:api.request('releases/assets/'+str(existing['distribution-index.json']['id']),method='DELETE')
        api.upload(release,args.output)
        api.request('releases/'+str(release['id']),dict(draft=False,make_latest='false'),method='PATCH')
    print(json.dumps(dict(index=str(args.output),resources=len(index['resources']),published=args.publish)))

if __name__=='__main__':main()
