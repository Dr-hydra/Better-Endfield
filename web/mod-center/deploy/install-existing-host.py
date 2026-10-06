"""Install onto the supplied Oracle host, preserving its existing HTTPS routes."""
import datetime
import os
from pathlib import Path
import pwd
import shutil
import subprocess
import sys
import urllib.request

stage = Path(sys.argv[1]).resolve()
if not (stage / 'server/server.mjs').is_file() or not (stage / 'dist/index.html').is_file():
    raise SystemExit('Incomplete deployment package')
target = Path('/opt/endfield-resource-center')
data = Path('/var/lib/endfield-resource-center')
proxy = Path('/opt/fh6-heybox/deploy/https-proxy.mjs')
runtime = Path('/opt/fh6-heybox/node/bin/node')
if os.geteuid() != 0 or not proxy.is_file() or not runtime.is_file():
    raise SystemExit('Run as root on the inspected Oracle host')

def run(*args):
    subprocess.run(args, check=True)

try:
    account = pwd.getpwnam('endfield-mods')
except KeyError:
    run('useradd', '--system', '--no-create-home', '--shell', '/sbin/nologin', 'endfield-mods')
    account = pwd.getpwnam('endfield-mods')

target.mkdir(mode=0o755, parents=True, exist_ok=True)
for folder in ['dist', 'server', 'shared']:
    shutil.copytree(stage / folder, target / folder, dirs_exist_ok=True)
shutil.copy2(stage / 'README.md', target / 'README.md')
(target / 'runtime').mkdir(mode=0o755, exist_ok=True)
if not (target / 'runtime/node').is_file():
    shutil.copy2(runtime, target / 'runtime/node')
    (target / 'runtime/node').chmod(0o755)
data.mkdir(mode=0o750, parents=True, exist_ok=True)
os.chown(data, account.pw_uid, account.pw_gid)
environment = Path('/etc/endfield-resource-center.env')
if not environment.exists():
    shutil.copy2(stage / 'deploy/environment.example', environment)
environment.chmod(0o640)
os.chown(environment, 0, account.pw_gid)
shutil.copy2(stage / 'deploy/endfield-resource-center.service', '/etc/systemd/system/endfield-resource-center.service')
run('restorecon', '-RF', str(target), str(data), str(environment), '/etc/systemd/system/endfield-resource-center.service')
run(str(target / 'runtime/node'), '--check', str(target / 'server/server.mjs'))
run('systemctl', 'daemon-reload')
run('systemctl', 'enable', 'endfield-resource-center')
run('systemctl', 'restart', 'endfield-resource-center')

import time
for attempt in range(20):
    try:
        with urllib.request.urlopen('http://127.0.0.1:9017/endfield/api/health', timeout=2) as response:
            if response.status == 200:
                break
    except Exception:
        if attempt == 19:
            raise SystemExit('Backend health check failed; existing proxy was not changed')
        time.sleep(0.25)

original = proxy.read_text()
modified = original
if "import { serveResourceCenter }" not in modified:
    modified = "import { serveResourceCenter } from './proxy-resource-center.mjs';\n" + modified
if 'if (serveResourceCenter(req, res)) return;' not in modified:
    needle = "const server = https.createServer(loadTls(), (req, res) => {\n"
    if needle not in modified:
        raise SystemExit('Existing proxy structure changed; leaving it untouched')
    modified = modified.replace(needle, needle + '  if (serveResourceCenter(req, res)) return;\n', 1)
shutil.copy2(stage / 'deploy/proxy-resource-center.mjs', proxy.parent / 'proxy-resource-center.mjs')
stamp = datetime.datetime.now(datetime.timezone.utc).strftime('%Y%m%dT%H%M%SZ')
backup = proxy.with_name(f'https-proxy.mjs.before-endfield-{stamp}')
shutil.copy2(proxy, backup)
replacement = proxy.with_name('https-proxy.endfield-new.mjs')
replacement.write_text(modified)
replacement.chmod(proxy.stat().st_mode & 0o777)
os.chown(replacement, proxy.stat().st_uid, proxy.stat().st_gid)
run(str(runtime), '--check', str(replacement))
os.replace(replacement, proxy)
run('restorecon', '-F', str(proxy), str(proxy.parent / 'proxy-resource-center.mjs'))
try:
    run('systemctl', 'restart', 'fh6-heybox-tls')
    for attempt in range(20):
        try:
            with urllib.request.urlopen('https://146.235.16.65:8443/endfield/api/health', timeout=3) as response:
                if response.status == 200:
                    break
        except Exception:
            if attempt == 19:
                raise RuntimeError('Public HTTPS health check failed')
            time.sleep(0.25)
except Exception:
    shutil.copy2(backup, proxy)
    run('systemctl', 'restart', 'fh6-heybox-tls')
    raise
print('Deployed: https://146.235.16.65:8443/endfield/')
print(f'Existing HTTPS proxy backup: {backup}')
