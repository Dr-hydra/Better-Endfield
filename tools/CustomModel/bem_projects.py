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


def project_manifest(source):
    """Resolve a project manifest from either project.json or the directory holding it.

    Unpacking writes a project directory, so packing is naturally handed that directory
    back; opening a directory as JSON would surface a bare permission error instead of
    anything the caller can act on.
    """
    source = Path(source)
    if source.is_dir():
        manifest = source / 'project.json'
        bem.require(manifest.is_file(), f'Project directory contains no project.json: {source}')
        return manifest
    bem.require(source.exists(), f'Project manifest does not exist: {source}')
    return source


def load_project(source):
    """Parse a resolved project manifest, naming the file when it is not JSON."""
    try:
        return bem.load_json(source)
    except ValueError as error:
        # JSONDecodeError and UnicodeDecodeError both land here; neither names the file
        # on its own, and a binary file handed in by mistake reads as a decode failure.
        raise ValueError(f'Project manifest is not readable JSON ({source}): {error}') from None


def pack_project(source, output, package=None, deformations=None):
    source, output = project_manifest(source), Path(output)
    project = load_project(source)
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
    from bem_export import prepare_export, package_overrides
    project['manifest'], payloads = prepare_export(project['manifest'], payloads)
    from bem_v13 import apply_author_spec, author_input_paths
    if deformations:
        bem.require(output.resolve() not in author_input_paths(deformations), 'Output cannot overwrite shape input')
    project['manifest'], payloads, deformation_report = apply_author_spec(project['manifest'], payloads, deformations)
    package_overrides(project['manifest'], package)
    from bem_tool import check_geometry
    bem.validate_manifest(project['manifest'], len(payloads))
    check_geometry(project['manifest'], payloads)
    bem.write_package(output, project['manifest'], payloads)
    from bem_tool import FORMAT_VERSIONS
    return dict(package=project['manifest'], conversion_ready=True, size=output.stat().st_size,
                deformations=deformation_report,
                format_version=FORMAT_VERSIONS[bem.package_minor(output)])


def _stage_project(manifest, payloads, staging):
    """Materialise an editable project: one payload file per index plus project.json."""
    folder = staging / 'payloads'; folder.mkdir()
    names = []
    for i, raw in enumerate(payloads):
        name = f'payloads/{i:04d}.bin'
        (staging / name).write_bytes(raw); names.append(name)
    (staging / 'project.json').write_text(
        json.dumps(dict(manifest=manifest, payload_files=names), ensure_ascii=False, indent=2), encoding='utf-8')
    return names


def unpack(source, output):
    source, output = Path(source), Path(output)
    bem.require(not output.exists(), 'Output directory already exists; choose a new project directory')
    output.parent.mkdir(parents=True, exist_ok=True)
    with tempfile.TemporaryDirectory(dir=output.parent, prefix='.bem-unpack-') as tmp:
        staging = Path(tmp) / 'project'; staging.mkdir()
        if source.suffix.lower() == '.zip':
            if has_package_manifest(source):
                package = read_zip_package(source)
                m, payloads = package['manifest'], package['payloads']
                # format_version is a container-only hint for readers that do not see a
                # binary header; the binary form records the same thing in its header.
                declared = str(m.get('format_version', 'unknown'))
                m = {key: value for key, value in m.items() if key != 'format_version'}
                names = _stage_project(m, payloads, staging)
                result = dict(format='BEM-ZIP-PACKAGE', package=m, project='project.json',
                              payload_count=len(names), issues=[], conversion_ready=True,
                              format_version=declared)
            else:
                result = inspect_bundle(source, staging)
                bem.require(result['packages'], 'ZIP contains no valid BEM packages: ' + json.dumps(result['issues'], ensure_ascii=False))
        else:
            m, payloads = validated(source)
            names = _stage_project(m, payloads, staging)
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
                    target = m['target']
                    target_kind = target.get('kind', 'character')
                    target_id = target['id'] if 'kind' in target else target['character_id']
                    summary = dict(entry=name, package_id=m['package_id'], name=m['name'],
                                   target_kind=target_kind, target_id=target_id)
                    if target_kind == 'character':
                        summary['character_id'] = target_id
                    packages.append(summary)
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


ZIP_FIXED_DATE = (1980, 1, 1, 0, 0, 0)
ZIP_MAX_ENTRIES = 8192
ZIP_MANIFEST_LIMIT = 4 * 1024**2


def _zip_member(archive, name, data, compress=zipfile.ZIP_DEFLATED):
    """Write one reproducible member: fixed timestamp and fixed file mode."""
    info = zipfile.ZipInfo(name, date_time=ZIP_FIXED_DATE)
    info.compress_type = compress
    info.external_attr = 0o600 << 16
    archive.writestr(info, data)


def _package_layout(archive):
    """Locate manifest.json in a single model package, tolerating one wrapper folder.

    Returns (prefix, manifest_entry, members) or (None, None, members).
    """
    members = {}
    for entry in archive.infolist():
        if entry.is_dir():
            continue
        rel = PurePosixPath(entry.filename.replace('\\', '/'))
        bem.require(not rel.is_absolute() and '..' not in rel.parts and ':' not in str(rel),
                    'Unsafe archive member path')
        key = str(rel)
        bem.require(key not in members, 'Duplicate archive member path')
        members[key] = entry
    if 'manifest.json' in members:
        return '', members['manifest.json'], members
    tops = sorted({PurePosixPath(name).parts[0] for name in members if len(PurePosixPath(name).parts) > 1})
    wrapped = f'{tops[0]}/manifest.json' if len(tops) == 1 else None
    if wrapped and wrapped in members:
        return f'{tops[0]}/', members[wrapped], members
    return None, None, members


def has_package_manifest(path):
    """True when the archive is a single model package rather than a collection bundle."""
    with zipfile.ZipFile(path) as archive:
        prefix, _, _ = _package_layout(archive)
        return prefix is not None


def payload_count(manifest):
    """Number of payload slots, matching how the native reader sizes its directory."""
    highest = -1

    def take(ref):
        nonlocal highest
        if isinstance(ref, int) and not isinstance(ref, bool):
            highest = max(highest, ref)

    for mesh in manifest.get('meshes') or []:
        for stream in mesh.get('streams') or []:
            if isinstance(stream, dict):
                take(stream.get('payload'))
        if isinstance(mesh, dict):
            take(mesh.get('indices'))
            for draw in mesh.get('draws') or []:
                if isinstance(draw, dict):
                    take(draw.get('indices'))
    for texture in manifest.get('textures') or []:
        if isinstance(texture, dict):
            take(texture.get('payload'))
    for channel in manifest.get('mesh_deformations') or []:
        for frame in channel.get('frames') or []:
            if isinstance(frame, dict):
                take(frame.get('payload'))
    for appearance in manifest.get('appearances') or []:
        if isinstance(appearance, dict):
            take(appearance.get('preview'))
    return highest + 1


def read_zip_package(source):
    """Read the single model ZIP package of format specification section 14.

    Manifest payload references are indices, so each payload lives at
    payloads/<id>.bin; the shorter spellings are accepted for hand-made archives.
    """
    with zipfile.ZipFile(source) as archive:
        entries = archive.infolist()
        bem.require(len(entries) <= ZIP_MAX_ENTRIES, 'ZIP has too many entries')
        prefix, manifest_entry, members = _package_layout(archive)
        bem.require(prefix is not None, 'ZIP package is missing manifest.json')
        # A manifest at the root wins even when stray .bem members are present, which is
        # exactly how the runtime resolves the same archive.
        raw = archive.read(manifest_entry)
        bem.require(0 < len(raw) <= ZIP_MANIFEST_LIMIT, 'Manifest exceeds 4 MiB')
        try:
            manifest = json.loads(raw.decode('utf-8'))
        except (UnicodeDecodeError, ValueError) as exc:
            raise ValueError('Manifest is not valid JSON: ' + str(exc)) from exc
        bem.require(isinstance(manifest, dict) and 'package_id' in manifest and 'target' in manifest,
                    'ZIP manifest is not a BEM package manifest')
        payloads, total = [], 0
        for index in range(payload_count(manifest)):
            data = None
            for name in (f'payloads/{index:04d}.bin', f'payloads/{index}.bin', f'{index}.bin'):
                if prefix + name in members:
                    data = archive.read(members[prefix + name])
                    break
            bem.require(data is not None, 'ZIP package is missing payload %d' % index)
            total += len(data)
            bem.require(total <= MAX_PACKAGE, 'ZIP payloads exceed 2 GiB')
            payloads.append(data)
    return dict(manifest=manifest, payloads=payloads, prefix=prefix, entries=len(entries))


def pack_zip(source, output, preview=None):
    """Wrap a validated .bem into the single model ZIP package of section 14.

    The manifest is copied unchanged except for a format_version hint, which lets a
    reader pick the right limits when the container carries no binary header.
    """
    from bem_tool import FORMAT_VERSIONS
    source, output = Path(source), Path(output)
    bem.require(source.suffix.lower() == '.bem', 'ZIP package input must be a .bem file')
    bem.require(output.suffix.lower() == '.zip', 'ZIP package filename must end in .zip')
    bem.require(output.resolve() != source.resolve(), 'Output cannot overwrite the input package')
    if preview is not None:
        preview = Path(preview)
        bem.require(preview.is_file(), 'Preview image not found')
        bem.require(preview.stat().st_size <= bem.LIMIT, 'Preview image exceeds the payload budget')
    m, payloads = validated(source)
    minor = bem.package_minor(source)
    bem.require(len(payloads) <= 16384, 'Too many payload files')
    decoded = sum(len(p) for p in payloads)
    bem.require(decoded <= MAX_PACKAGE, 'Package decoded budget exceeded')
    manifest = dict(m)
    manifest.setdefault('format_version', f'1.{minor}')

    output.parent.mkdir(parents=True, exist_ok=True)
    fd, temp = tempfile.mkstemp(dir=output.parent, prefix='.bem-package-', suffix='.zip'); os.close(fd)
    try:
        with zipfile.ZipFile(temp, 'w', compression=zipfile.ZIP_DEFLATED, allowZip64=True) as archive:
            _zip_member(archive, 'manifest.json', json.dumps(manifest, ensure_ascii=False).encode('utf-8'))
            if preview is not None:
                _zip_member(archive, 'preview.png', preview.read_bytes(), zipfile.ZIP_STORED)
            for index, raw in enumerate(payloads):
                _zip_member(archive, f'payloads/{index:04d}.bin', raw)
        bem.require(Path(temp).stat().st_size <= MAX_PACKAGE, 'ZIP package exceeds 2 GiB')
        os.replace(temp, output)
    finally:
        if os.path.exists(temp): os.unlink(temp)

    # Read the written file back: a package that cannot be re-read is not a deliverable.
    readback = read_zip_package(output)
    bem.require(readback['manifest']['package_id'] == m['package_id'], 'ZIP package ID changed on rewrite')
    bem.require(len(readback['payloads']) == len(payloads), 'ZIP package payload count changed on rewrite')
    return dict(format='BEM-ZIP-PACKAGE', package=m, payload_count=len(payloads),
                entry_count=readback['entries'], size=output.stat().st_size,
                format_version=FORMAT_VERSIONS[minor], issues=[], conversion_ready=True)
