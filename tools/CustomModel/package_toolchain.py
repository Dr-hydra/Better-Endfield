"""Assemble the self-contained CLI distribution and optional AI skill."""
import argparse
from pathlib import Path
import shutil
import zipfile
import sys


def main():
    p = argparse.ArgumentParser(); p.add_argument('directory', type=Path)
    p.add_argument('--workspace-config', type=Path)
    p.add_argument('--archive-backend', type=Path)
    args = p.parse_args()
    repo = Path(__file__).resolve().parents[2]
    sys.path.insert(0, str(repo/'scripts'))
    from workspace_config import load_workspace
    workspace = load_workspace(args.workspace_config, repo)
    target = args.directory.resolve()
    if not (target/'BetterEndfield.BemConverter.exe').is_file(): raise ValueError('Build CLI first')
    docs = ['BEM_CREATOR_GUIDE.md', 'BEM_FORMAT_SPEC.md', 'BEM_RUNTIME_COMPATIBILITY.md', 'BEM_SOURCE_MOD_CONVERSION.md',
            'BEM_CREATOR_GUIDE.en.md', 'BEM_FORMAT_SPEC.en.md', 'BEM_RUNTIME_COMPATIBILITY.en.md', 'BEM_SOURCE_MOD_CONVERSION.en.md']
    (target/'docs').mkdir(exist_ok=True)
    for name in docs: shutil.copyfile(workspace.document(name), target/'docs'/name)
    source_ignore = shutil.ignore_patterns('__pycache__', '*.pyc')
    shutil.copytree(repo/'tools/CustomModel/examples', target/'examples', dirs_exist_ok=True, ignore=source_ignore)
    shutil.copytree(repo/'tools/CustomModel/blender_addon', target/'blender_addon', dirs_exist_ok=True, ignore=source_ignore)
    shutil.copytree(repo/'tools/CustomModel/catalog', target/'catalog', dirs_exist_ok=True)
    archive_backend = args.archive_backend or workspace.path('tools.archive_backend')
    shutil.copytree(archive_backend, target/'7zip', dirs_exist_ok=True)
    (target/'7zip/NOTICE.txt').write_text(
        'This tool uses unmodified 7-Zip 26.03 by Igor Pavlov, licensed under GNU LGPL '
        'with additional license terms including the unRAR restriction. See License.txt.\n'
        'Source code and releases: https://www.7-zip.org/ and https://github.com/ip7z/7zip/tree/26.03\n', encoding='utf-8')
    shutil.copytree(repo/'tools/CustomModel/skills', target/'skills', dirs_exist_ok=True, ignore=source_ignore)
    refs = target/'skills/bem-creator/references'; refs.mkdir(exist_ok=True)
    for name in docs: shutil.copyfile(workspace.document(name), refs/name)
    shutil.copyfile(repo/'LICENSE', target/'LICENSE.txt')
    output = target.parent/'BEM-Tools-win-x64.zip'
    with zipfile.ZipFile(output, 'w', compression=zipfile.ZIP_DEFLATED, compresslevel=6) as z:
        for file in sorted(target.rglob('*')):
            if file.is_file(): z.write(file, str(Path('BEM-Tools')/file.relative_to(target)))
    print(f'Toolchain ZIP: {output} ({output.stat().st_size} bytes)')


if __name__ == '__main__': main()
