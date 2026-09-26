"""Assemble the self-contained CLI distribution and optional AI skill."""
import argparse
from pathlib import Path
import shutil
import zipfile


def main():
    p = argparse.ArgumentParser(); p.add_argument('directory', type=Path); args = p.parse_args()
    repo = Path(__file__).resolve().parents[2]
    target = args.directory.resolve()
    if not (target/'BetterEndfield.BemConverter.exe').is_file(): raise ValueError('Build CLI first')
    docs = ['BEM_V1_SPEC.md', 'BEM_V1_1_SPEC.md', 'BEM_CREATOR_GUIDE.md', 'BEM_CHARACTER_CATALOG_20260919.md', 'BEM_EFMI_IDENTITIES_20260920.md', 'BEM_PER_DRAW_COMPATIBILITY_20260920.md', 'BEM_RABBITFX_COMPATIBILITY_20260920.md']
    (target/'docs').mkdir(exist_ok=True)
    for name in docs: shutil.copyfile(repo/'docs'/name, target/'docs'/name)
    shutil.copytree(repo/'tools/CustomModel/examples', target/'examples', dirs_exist_ok=True)
    shutil.copytree(repo/'tools/CustomModel/catalog', target/'catalog', dirs_exist_ok=True)
    shutil.copytree(repo/'artifacts/bem-archive-backend/7zip', target/'7zip', dirs_exist_ok=True)
    (target/'7zip/NOTICE.txt').write_text(
        'This tool uses unmodified 7-Zip 26.03 by Igor Pavlov, licensed under GNU LGPL '
        'with additional license terms including the unRAR restriction. See License.txt.\n'
        'Source code and releases: https://www.7-zip.org/ and https://github.com/ip7z/7zip/tree/26.03\n', encoding='utf-8')
    shutil.copytree(repo/'tools/CustomModel/skills', target/'skills', dirs_exist_ok=True)
    refs = target/'skills/bem-creator/references'; refs.mkdir(exist_ok=True)
    for name in docs: shutil.copyfile(repo/'docs'/name, refs/name)
    shutil.copyfile(repo/'LICENSE', target/'LICENSE.txt')
    output = target.parent/'BEM-Tools-win-x64.zip'
    with zipfile.ZipFile(output, 'w', compression=zipfile.ZIP_DEFLATED, compresslevel=6) as z:
        for file in sorted(target.rglob('*')):
            if file.is_file(): z.write(file, str(Path('BEM-Tools')/file.relative_to(target)))
    print(f'Toolchain ZIP: {output} ({output.stat().st_size} bytes)')


if __name__ == '__main__': main()
