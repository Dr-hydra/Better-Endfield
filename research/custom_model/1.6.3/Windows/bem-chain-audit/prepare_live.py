"""Stage production binaries and isolated settings for a login-screen BEM check."""
from pathlib import Path
import json
import shutil

ROOT = Path(__file__).resolve().parents[5]
LIVE = ROOT / 'build/bem-chain-audit-4.0.3/live'
PUBLISH = ROOT / 'build/release-4.0.3/next/windows/win-x64/Release/publish'


def main():
    runtime = LIVE / 'runtime'
    files = [
        'loaders/BetterEndfieldNext.Injector.exe',
        'loaders/BetterEndfieldNext.Bootstrap.dll',
        'runtime/BetterEndfieldNext.Host.dll',
    ]
    for module in ['Model', 'CustomModel']:
        files.append(f'modules/BetterEndfieldNext.{module}.dll')
        module_id = 'custom_model' if module == 'CustomModel' else 'model'
        files.append(f'modules/betterendfieldnext.{module_id}.module.ini')
    for relative in files:
        source = PUBLISH / relative
        destination = runtime / relative
        destination.parent.mkdir(parents=True, exist_ok=True)
        shutil.copy2(source, destination)
    presets = json.loads((ROOT / 'resources/generated/model/character-presets.json').read_text(encoding='utf-8-sig'))
    character = next(c for c in presets['characters'] if c['id'] == 'chr_0013_aglina')
    action = next(a for a in character['actions'] if a['id'] == character['defaultActionId'])
    model = {'enabled': 'true', 'model_replacement_enabled': 'true',
             'character': character['id'], 'model_path': character['model']['path'],
             'model_path_hash': character['model']['pathHash'],
             'final_action': action['id'],
             'final_native_loop': str(action.get('nativeLoop', False)).lower(),
             'logo_theme_enabled': 'false'}
    for prefix, clip in [('sit_loop', character['sitLoop']), ('sit_special', character['sitSpecial']),
                         ('sit_to_walk', character['sitToWalk']), ('final', action)]:
        model[prefix + '_path'] = clip['path']
        model[prefix + '_path_hash'] = clip['pathHash']
        model[prefix + '_label'] = clip.get('displayName', clip.get('id', ''))
    sections = {'Loader': {'install_root': str(runtime), 'load_host': 'true'},
                'Host': {'modules_root': str(runtime / 'modules')},
                'betterendfieldnext.model': model,
                'betterendfieldnext.custom_model': {'enabled': 'true'}}
    settings = LIVE / 'local/BetterEndfieldNext/BetterEndfieldNext.ini'
    settings.parent.mkdir(parents=True, exist_ok=True)
    text = ''.join(f'[{section}]\r\n' + ''.join(f'{k}={v}\r\n' for k, v in values.items()) + '\r\n'
                   for section, values in sections.items())
    settings.write_bytes(text.encode('utf-16'))
    result = {'game_version': '1.6.3', 'product_version': '4.0.3', 'runtime': str(runtime),
              'settings': str(settings), 'character': character['id'], 'production_binaries': files,
              'xinput_deployed': False, 'user_settings_modified': False}
    (LIVE / 'setup.json').write_text(json.dumps(result, indent=2) + '\n', encoding='utf-8')
    print(json.dumps(result, ensure_ascii=False))


if __name__ == '__main__':
    main()
