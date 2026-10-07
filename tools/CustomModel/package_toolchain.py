"""Assemble the self-contained creator GUI, CLI and AI skill distribution."""
import argparse
from pathlib import Path
import shutil
import os
import stat
import zipfile
import sys
import tempfile


def prepare_bem14_example(repo, target):
    """Ship a runnable project; end users need no Python to generate fixtures."""
    import importlib.util
    path = repo/'tools/CustomModel/examples/multi-resource/create_project.py'
    spec = importlib.util.spec_from_file_location('_bem14_packaged_example', path)
    module = importlib.util.module_from_spec(spec); spec.loader.exec_module(module)
    return module.create(target/'examples/multi-resource/project')


def copy_documents(workspace, names, destination, draft_link):
    for name in names:
        source = workspace.document(name)
        if name == 'BEM_V1_4_SPEC.md':
            text = source.read_text(encoding='utf-8-sig').replace(
                '../../tools/CustomModel/profiles/bem14-drafts/README.md', draft_link)
            (destination/name).write_text(text, encoding='utf-8')
        else:
            shutil.copyfile(source, destination/name)


def main():
    p = argparse.ArgumentParser(); p.add_argument('directory', type=Path)
    p.add_argument('--workspace-config', type=Path)
    p.add_argument('--archive-backend', type=Path)
    p.add_argument('--gui-directory', type=Path, required=True)
    args = p.parse_args()
    repo = Path(__file__).resolve().parents[2]
    sys.path.insert(0, str(repo/'scripts'))
    from workspace_config import load_workspace
    workspace = load_workspace(args.workspace_config, repo)
    target = args.directory.resolve()
    if not (target/'BetterEndfield.BemConverter.exe').is_file(): raise ValueError('Build CLI first')
    gui = args.gui_directory.resolve()
    if not (gui/'BetterEndfield.BemTools.exe').is_file(): raise ValueError('Build creator GUI first')
    docs = ['BEM_CREATOR_GUIDE.md', 'BEM_FORMAT_SPEC.md', 'BEM_V1_4_SPEC.md', 'BEM_RUNTIME_COMPATIBILITY.md', 'BEM_SOURCE_MOD_CONVERSION.md',
            'BEM_CREATOR_GUIDE.en.md', 'BEM_FORMAT_SPEC.en.md', 'BEM_RUNTIME_COMPATIBILITY.en.md', 'BEM_SOURCE_MOD_CONVERSION.en.md']
    (target/'docs').mkdir(exist_ok=True)
    copy_documents(workspace, docs, target/'docs', '../profiles/bem14-drafts/README.md')
    source_ignore = shutil.ignore_patterns('__pycache__', '*.pyc')
    shutil.copytree(repo/'tools/CustomModel/examples', target/'examples', dirs_exist_ok=True, ignore=source_ignore)
    prepare_bem14_example(repo, target)
    shutil.copytree(repo/'tools/CustomModel/blender_addon', target/'blender_addon', dirs_exist_ok=True, ignore=source_ignore)
    shutil.copytree(repo/'tools/CustomModel/catalog', target/'catalog', dirs_exist_ok=True)
    shutil.copytree(repo/'tools/CustomModel/profiles/bem14-drafts', target/'profiles/bem14-drafts', dirs_exist_ok=True)
    draft_readme = target/'profiles/bem14-drafts/README.md'
    draft_readme.write_text(draft_readme.read_text(encoding='utf-8').replace(
        '../../../../docs/custom_model/BEM_V1_4_SPEC.md', '../../docs/BEM_V1_4_SPEC.md'), encoding='utf-8')
    archive_backend = args.archive_backend or workspace.path('tools.archive_backend')
    shutil.copytree(archive_backend, target/'7zip', dirs_exist_ok=True)
    if os.name == 'nt':
        # Administrative MSI extraction can mark the backend directory read-only.
        # Keep the generated distribution rebuildable without changing its source.
        copied_backend = target/'7zip'
        for entry in [copied_backend, *copied_backend.rglob('*')]:
            entry.chmod(entry.stat().st_mode | stat.S_IWRITE)
    (target/'7zip/NOTICE.txt').write_text(
        'This tool uses unmodified 7-Zip 26.03 by Igor Pavlov, licensed under GNU LGPL '
        'with additional license terms including the unRAR restriction. See License.txt.\n'
        'Source code and releases: https://www.7-zip.org/ and https://github.com/ip7z/7zip/tree/26.03\n', encoding='utf-8')
    shutil.copytree(repo/'tools/CustomModel/skills', target/'skills', dirs_exist_ok=True, ignore=source_ignore)
    refs = target/'skills/bem-creator/references'; refs.mkdir(exist_ok=True)
    copy_documents(workspace, docs, refs, '../../../profiles/bem14-drafts/README.md')
    shutil.copyfile(repo/'LICENSE', target/'LICENSE.txt')
    output = target.parent/'BEM-Tools-win-x64.zip'
    # Keep the CLI staging directory usable by the main application without
    # adding another self-contained WinUI runtime to its embedded tools folder.
    # Only the independent ZIP combines the GUI and backend beside each other.
    with tempfile.TemporaryDirectory(prefix='bem-tools-package-', dir=target.parent) as temporary:
        staged_archive = Path(temporary)/output.name
        with zipfile.ZipFile(staged_archive, 'w', compression=zipfile.ZIP_DEFLATED, compresslevel=6) as z:
            names = set()
            for root in (target, gui):
                for file in sorted(root.rglob('*')):
                    if not file.is_file(): continue
                    name = (Path('BEM-Tools')/file.relative_to(root)).as_posix()
                    if name.casefold() in names:
                        raise ValueError(f'Duplicate toolchain archive entry: {name}')
                    names.add(name.casefold())
                    z.write(file, name)
            z.write(repo/'tools/CustomModel/TOOLCHAIN_README.md', 'BEM-Tools/README.md')
        staged_archive.replace(output)
    print(f'Toolchain ZIP: {output} ({output.stat().st_size} bytes)')


if __name__ == '__main__': main()
