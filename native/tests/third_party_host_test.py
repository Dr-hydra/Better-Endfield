import argparse
import json
import pathlib
import shutil
import socket
import subprocess
import tempfile
import time
import urllib.error
import urllib.request

def main():
    parser = argparse.ArgumentParser()
    parser.add_argument('--host', required=True)
    parser.add_argument('--library', required=True)
    args = parser.parse_args()
    with tempfile.TemporaryDirectory(prefix='betterendfield-third-party-') as tmp:
        root = pathlib.Path(tmp); package = root / 'package'; package.mkdir()
        shutil.copyfile(args.library, package / 'example.echo.dll')
        manifest = dict(format=1, id='example.echo', name='Echo', author='Tests', version='1', abi=1,
                        libraries={'windows-x64': 'example.echo.dll'}, dependencies=[])
        (package / 'module.json').write_text(json.dumps(manifest), encoding='utf-8')
        with socket.socket() as port_socket:
            port_socket.bind(('127.0.0.1', 0)); port = port_socket.getsockname()[1]
        token = 'test_local_only_private_token_12345678'
        index = dict(schema=1, port=port, token=token, modules=[dict(id='example.echo', enabled=True,
                    directory=str(package), configuration={'label': 'first'}, generation='1')])
        path = root / 'index.json'
        def write():
            pending = path.with_suffix('.tmp'); pending.write_text(json.dumps(index), encoding='utf-8'); pending.replace(path)
        write()
        process = subprocess.Popen([args.host, str(path)], stdin=subprocess.PIPE, stdout=subprocess.PIPE, stderr=subprocess.PIPE, text=True)
        try:
            assert process.stdout.readline().strip() == 'ready', 'server did not start'
            def http(endpoint, body=None, credential=token):
                data = None if body is None else json.dumps(body).encode()
                request = urllib.request.Request(f'http://127.0.0.1:{port}{endpoint}', data=data,
                           headers={'Authorization': 'Bearer ' + credential, 'Content-Type': 'application/json'})
                with urllib.request.urlopen(request, timeout=4) as response: return json.load(response)
            def wait(predicate):
                deadline = time.monotonic() + 8
                while time.monotonic() < deadline:
                    status = http('/status')['modules']
                    if status and predicate(status[0]): return status[0]
                    time.sleep(0.05)
                raise AssertionError('module state timeout: ' + repr(status))
            wait(lambda m: m['status'] == 'ready')
            assert any('Echo sample initialized' in line for line in http('/status')['modules'][0]['logs']), 'module logs missing from status bridge'
            try: http('/status', credential='incorrect')
            except urllib.error.HTTPError as error: assert error.code == 401
            else: raise AssertionError('unauthenticated request accepted')
            assert http('/send', dict(module_id='example.echo', request_id='r1', body={'hello': 'world'}))['accepted']
            messages = http('/poll', dict(module_id='example.echo'))['messages']
            reply = next(m for m in messages if m['kind'] == 'reply')
            assert reply['request_id'] == 'r1' and reply['body']['echo'] == {'hello': 'world'}
            assert reply['body']['counter'] == 1 and reply['body']['configuration']['label'] == 'first'
            assert reply['body']['runtime_helper_ready'] is False, 'sample unexpectedly requires game runtime'
            assert any(m['kind'] == 'event' for m in messages)
            index['modules'][0]['configuration'] = {'label': 'changed'}; write(); time.sleep(1.3)
            assert http('/send', dict(module_id='example.echo', request_id='r2', body=['opaque', 123]))['accepted']
            reply = next(m for m in http('/poll', dict(module_id='example.echo'))['messages'] if m['kind'] == 'reply')
            assert reply['body']['configuration']['label'] == 'changed' and reply['body']['counter'] == 2
            index['modules'][0]['enabled'] = False; write(); wait(lambda m: m['status'] == 'disabled')
            try: http('/send', dict(module_id='example.echo', request_id='disabled', body={}))
            except urllib.error.HTTPError as error: assert error.code == 400
            else: raise AssertionError('disabled module received message')
            index['modules'][0]['enabled'] = True; write(); wait(lambda m: m['status'] == 'ready')
            index['modules'][0]['generation'] = '2'; write(); wait(lambda m: m['restart_required'])
            assert http('/status')['connected'] is True
            ui_package = root / 'ui-package'; ui_package.mkdir()
            (ui_package / 'index.html').write_text('<!doctype html><title>UI only</title>', encoding='utf-8')
            (ui_package / 'module.json').write_text(json.dumps(dict(format=1, abi=1, id='example.ui', libraries={}, ui='index.html')), encoding='utf-8')
            index['modules'].append(dict(id='example.ui', enabled=True, directory=str(ui_package), generation='ui1', configuration={}))
            write(); deadline = time.monotonic() + 8
            while time.monotonic() < deadline:
                ui = next((m for m in http('/status')['modules'] if m['id'] == 'example.ui'), None)
                if ui and ui['status'] == 'ui_only': break
                time.sleep(0.05)
            assert ui and ui['status'] == 'ui_only' and ui['requested_enabled'] and not ui['enabled']
            assert http('/configure', dict(module_id='example.ui', configuration={'local': True}))['accepted']
            try: http('/send', dict(module_id='example.ui', request_id='ui', body={}))
            except urllib.error.HTTPError as error: assert error.code == 400
            else: raise AssertionError('UI-only module received native message')
        finally:
            if process.poll() is None:
                process.stdin.write('stop\n'); process.stdin.flush()
            output, errors = process.communicate(timeout=8)
            assert process.returncode == 0, (output, errors)
        restarted = subprocess.Popen([args.host, str(path)], stdin=subprocess.PIPE, stdout=subprocess.PIPE, stderr=subprocess.PIPE, text=True)
        assert restarted.stdout.readline().strip() == 'ready', 'stable localhost endpoint could not restart'
        restarted.stdin.write('stop\n'); restarted.stdin.flush()
        output, errors = restarted.communicate(timeout=8)
        assert restarted.returncode == 0, (output, errors)
    print('PASS: native library loader + authenticated loopback messages/events + opaque config + disable/enable + generation pinning + UI-only module')

if __name__ == '__main__': main()
