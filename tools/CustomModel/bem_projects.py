"""Editable projects and standard ZIP distribution; no archive-provided code runs."""
import json
import os
import shutil
import stat
import tempfile
import zipfile
import zstandard
from collections import Counter
from pathlib import Path, PurePosixPath
import bem_v1 as bem

MAX_BUNDLE = 4 * 1024**3
MAX_PACKAGE = 2 * 1024**3


def validated(path):
    from bem_tool import check_geometry
    m, payloads = bem.read_package(path)
    check_geometry(m, payloads, bem.package_minor(path) or None)
    return m, payloads


def pack_project(source, output):
    source, output = Path(source), Path(output)
    project = bem.load_json(source)
    root = source.parent.resolve()
    payloads, total = [], 0
    bem.require(len(project['payload_files']) <= 16384, 'Too many payload files')
    for name in project['payload_files']:
        rel = PurePosixPath(name.replace('\\', '/'))
        bem.require(not rel.is_absolute() and '..' not in rel.parts and ':' not in name, 'Payload path must stay inside project')
        path = (root / str(rel)).resolve()
        bem.require(path.is_relative_to(root) and path.is_file() and path != output.resolve(), 'Invalid payload/output path')
        size = path.stat().st_size
        total += size
        bem.require(0 < size <= bem.LIMIT and total <= MAX_PACKAGE, 'Project decoded budget exceeded')
        payloads.append(path.read_bytes())
    bem.require(output.resolve() != source.resolve(), 'Output cannot overwrite project manifest')
    from bem_tool import check_geometry
    bem.validate_manifest(project['manifest'], len(payloads))
    check_geometry(project['manifest'], payloads)
    bem.write_package(output, project['manifest'], payloads)
    from bem_tool import FORMAT_VERSIONS
    return dict(package=project['manifest'], conversion_ready=True, size=output.stat().st_size,
                format_version=FORMAT_VERSIONS[bem.package_minor(output)])


def unpack(source, output):
    source, output = Path(source), Path(output)
    bem.require(not output.exists(), 'Output directory already exists; choose a new project directory')
    output.parent.mkdir(parents=True, exist_ok=True)
    with tempfile.TemporaryDirectory(dir=output.parent, prefix='.bem-unpack-') as tmp:
        staging = Path(tmp) / 'project'; staging.mkdir()
        if source.suffix.lower() == '.zip':
            result = inspect_bundle(source, staging)
            bem.require(result['packages'], 'ZIP contains no valid BEM packages: ' + json.dumps(result['issues'], ensure_ascii=False))
        else:
            m, payloads = validated(source)
            folder = staging / 'payloads'; folder.mkdir()
            names = []
            for i, raw in enumerate(payloads):
                name = f'payloads/{i:04d}.bin'
                (staging / name).write_bytes(raw); names.append(name)
            (staging / 'project.json').write_text(json.dumps(dict(manifest=m, payload_files=names), ensure_ascii=False, indent=2), encoding='utf-8')
            from bem_tool import FORMAT_VERSIONS
            result = dict(format='BEM-project', package=m, project='project.json', payload_count=len(names), issues=[],
                          format_version=FORMAT_VERSIONS[bem.package_minor(source)])
        os.replace(staging, output)
    return dict(result, output=str(output))


def bundle(inputs, output):
    output = Path(output)
    bem.require(output.suffix.lower() == '.zip', 'Distribution filename must end in .zip')
    bem.require(0 < len(inputs) <= 256, 'Select 1..256 packages')
    output.parent.mkdir(parents=True, exist_ok=True)
    fd, temp = tempfile.mkstemp(dir=output.parent, prefix='.bem-bundle-', suffix='.zip'); os.close(fd)
    packages, ids, total = [], set(), 0
    try:
        with zipfile.ZipFile(temp, 'w', compression=zipfile.ZIP_STORED, allowZip64=True) as archive:
            for source in inputs:
                source = Path(source)
                bem.require(source.suffix.lower() == '.bem' and source.resolve() != output.resolve(), 'Bundle accepts .bem files only')
                # Validate the exact staged bytes, so later changes to source cannot
                # introduce an unchecked package into a successful output archive.
                with tempfile.TemporaryDirectory() as td:
                    staged = Path(td) / 'package.bem'
                    size = source.stat().st_size; total += size
                    bem.require(size <= MAX_PACKAGE and total <= MAX_BUNDLE, 'Bundle exceeds 4 GiB budget')
                    shutil.copyfile(source, staged)
                    m, _ = validated(staged)
                    key = m['package_id'].casefold()
                    bem.require(key not in ids, 'Duplicate package ID in bundle: ' + m['package_id']); ids.add(key)
                    name = m['package_id'] + '.bem'
                    archive.write(staged, name)
                    packages.append(dict(entry=name, package_id=m['package_id'], name=m['name'], character_id=m['target']['character_id']))
        bem.require(Path(temp).stat().st_size <= MAX_BUNDLE, 'ZIP exceeds 4 GiB budget')
        os.replace(temp, output)
    finally:
        if os.path.exists(temp): os.unlink(temp)
    return dict(format='BEM-ZIP', packages=packages, size=output.stat().st_size, issues=[])


def inspect_bundle(source, destination=None):
    """Validate members independently, optionally stage only valid BEMs.

    Archive paths are never used as filesystem paths. Duplicate package IDs
    make all competing entries invalid; no arbitrary first/last winner.
    """
    bem.require(Path(source).stat().st_size <= MAX_BUNDLE, 'ZIP exceeds 4 GiB budget')
    issues, packages, ids, total, conflicts = [], [], {}, 0, set()
    with tempfile.TemporaryDirectory(prefix='bem-zip-') as td, zipfile.ZipFile(source) as archive:
        entries = archive.infolist()
        bem.require(len(entries) <= 8192, 'ZIP has too many entries')
        candidates = [e for e in entries if e.filename.lower().endswith('.bem') and not e.is_dir()]
        bem.require(len(candidates) <= 256, 'ZIP has more than 256 BEM entries')
        entry_names = Counter(str(PurePosixPath(e.filename.replace('\\', '/'))).casefold() for e in candidates)
        for index, entry in enumerate(candidates):
            staged = Path(td) / f'{index:04d}.bem'
            try:
                rel = PurePosixPath(entry.filename.replace('\\', '/'))
                bem.require(not rel.is_absolute() and '..' not in rel.parts and ':' not in str(rel), 'Unsafe archive member path')
                bem.require(not stat.S_ISLNK(entry.external_attr >> 16), 'Symlink member refused')
                bem.require(not entry.flag_bits & 1 and entry.compress_type in (zipfile.ZIP_STORED, zipfile.ZIP_DEFLATED), 'Encrypted/unsupported ZIP member')
                key = str(rel).casefold()
                bem.require(entry_names[key] == 1, 'Duplicate archive member path')
                bem.require(0 < entry.file_size <= MAX_PACKAGE, 'Package exceeds 2 GiB')
                total += entry.file_size
                bem.require(total <= MAX_BUNDLE, 'Unpacked BEM members exceed 4 GiB budget')
                written = 0
                with archive.open(entry) as src, staged.open('wb') as dst:
                    while chunk := src.read(1024*1024):
                        written += len(chunk)
                        bem.require(written <= entry.file_size, 'ZIP entry size mismatch')
                        dst.write(chunk)
                bem.require(written == entry.file_size, 'Truncated ZIP entry')
                m, _ = validated(staged)
                key = m['package_id'].casefold()
                if key in ids:
                    conflicts.add(key)
                    raise ValueError('Duplicate package ID: ' + m['package_id'])
                ids[key] = entry.filename
                file = f'{index:04d}.bem'
                packages.append(dict(entry=entry.filename, file=file, package=m, size=written))
            except (ValueError, KeyError, IndexError, TypeError, OSError, zipfile.BadZipFile, RuntimeError, zstandard.ZstdError) as exc:
                issues.append(dict(code='ZIP_MEMBER', entry=entry.filename, message=str(exc)))
        accepted = []
        for p in packages:
            if p['package']['package_id'].casefold() in conflicts:
                issues.append(dict(code='DUPLICATE_ID', entry=p['entry'], message='同 ID 的多个包全部跳过，请分别处理。'))
            else:
                if destination: shutil.copyfile(Path(td)/p['file'], Path(destination)/p['file'])
                accepted.append(p)
    return dict(format='BEM-ZIP', packages=accepted, issues=issues,
                ignored_entries=len(entries)-len(candidates), partial=bool(issues), conversion_ready=False)


def has_bem_members(path):
    with zipfile.ZipFile(path) as z:
        return any(e.filename.lower().endswith('.bem') and not e.is_dir() for e in z.infolist())
