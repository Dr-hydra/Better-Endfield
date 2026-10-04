"""Portable, repeatable export tasks; JSON contains data, never shell commands."""
import json
import uuid
from pathlib import Path
import os
import shutil
import tempfile
import bem_v1 as bem

KIND = 'bem-export-task'


def resolve(root, name):
    bem.require(isinstance(name, str) and bool(name.strip()), 'Missing project path')
    path = Path(name)
    return (path if path.is_absolute() else root / path).resolve()


def relative(root, path):
    try: return Path(os.path.relpath(path, root)).as_posix()
    except ValueError: return str(path)  # Different Windows volumes.


def _copy_path(source, destination):
    """Copy a file or directory without following an output path through it."""
    source, destination = Path(source), Path(destination)
    bem.require(source.exists(), 'Workspace input does not exist: ' + str(source))
    bem.require(not destination.exists(), 'Workspace destination already exists: ' + str(destination))
    destination.parent.mkdir(parents=True, exist_ok=True)
    if source.is_dir():
        shutil.copytree(source, destination)
    else:
        shutil.copy2(source, destination)


def _safe_relative_path(name):
    path = Path(str(name).replace('\\', '/'))
    bem.require(not path.is_absolute() and '..' not in path.parts and ':' not in str(name),
                'Workspace reference must stay inside its source directory: ' + str(name))
    return path


def _copy_pack_project(source, destination):
    """Stage a low-level project and its payloads under workspace/project."""
    source, destination = Path(source).resolve(), Path(destination)
    bem.require(source.is_file(), 'Pack workspace source must be project.json')
    data = bem.load_json(source)
    payloads = data.get('payload_files')
    bem.require(isinstance(payloads, list), 'Pack project has no payload_files list')
    target = destination / source.name
    _copy_path(source, target)
    for name in payloads:
        rel = _safe_relative_path(name)
        _copy_path(source.parent / rel, destination / rel)
    return target


def _copy_reference(value, original_root, destination_root):
    """Copy one recipe/deformation reference while retaining its relative spelling."""
    if not isinstance(value, str) or not value.strip():
        return value
    raw = Path(value)
    if raw.is_absolute():
        source = raw.resolve()
        relative_name = Path(source.name)
    else:
        _safe_relative_path(value)
        source = (Path(original_root) / raw).resolve()
        relative_name = Path(str(value).replace('\\', '/'))
    if not source.exists():
        return value
    target = Path(destination_root) / relative_name
    if target.exists():
        bem.require(target.is_dir() == source.is_dir(), 'Workspace reference collision: ' + str(target))
    else:
        _copy_path(source, target)
    return relative_name.as_posix()


def _copy_deformations(source, destination_root):
    """Copy a deformation author file and its relative target/EFMI inputs."""
    source = Path(source).resolve()
    target = Path(destination_root) / source.name
    _copy_path(source, target)
    try:
        data = bem.load_json(source)
    except (OSError, ValueError, TypeError):
        return target
    changed = False
    for channel in data.get('mesh_deformations', []):
        for frame in channel.get('frames', []):
            for key in ('deltas', 'target_positions'):
                if key in frame:
                    frame[key] = _copy_reference(frame[key], source.parent, Path(destination_root))
                    changed = True
            binding = frame.get('efmi')
            if isinstance(binding, dict) and isinstance(binding.get('source'), str):
                binding['source'] = _copy_reference(binding['source'], source.parent, Path(destination_root))
                changed = True
    if changed:
        target.write_text(json.dumps(data, ensure_ascii=False, indent=2), encoding='utf-8')
    return target


def _copy_recipe(source, destination_root, source_origin, source_target, deformation_origin=None,
                 deformation_target=None):
    """Copy a recipe and the referenced profile/input files into recipe/."""
    source = Path(source).resolve()
    target = Path(destination_root) / source.name
    data = bem.load_json(source)
    source_origin, source_target = Path(source_origin).resolve(), Path(source_target)

    def copy_value(value):
        if not isinstance(value, str) or not value.strip():
            return value
        candidate = (source.parent / Path(value)).resolve() if not Path(value).is_absolute() else Path(value).resolve()
        # Recipes commonly refer to the separately selected source by its old basename.
        if candidate == source_origin or (not candidate.exists() and Path(value).name == source_origin.name):
            return Path(os.path.relpath(source_target, target.parent)).as_posix()
        return _copy_reference(value, source.parent, target.parent)

    for appearance in data.get('appearances', []):
        if 'source' in appearance:
            appearance['source'] = copy_value(appearance['source'])
        for key in ('profile', 'preview'):
            if key in appearance:
                appearance[key] = copy_value(appearance[key])
        reviewed = appearance.get('reviewed')
        if isinstance(reviewed, dict):
            for key in ('recipe', 'database', 'observations', 'native_textures', 'texture_dir'):
                if key in reviewed:
                    reviewed[key] = copy_value(reviewed[key])
    if deformation_origin is not None:
        if data.get('deformations'):
            data['deformations'] = Path(os.path.relpath(deformation_target, target.parent)).as_posix()
    else:
        deformation_ref = data.get('deformations')
        if deformation_ref:
            old = (source.parent / Path(deformation_ref)).resolve()
            if old.exists():
                copied = _copy_deformations(old, target.parent)
                data['deformations'] = Path(os.path.relpath(copied, target.parent)).as_posix()
    target.parent.mkdir(parents=True, exist_ok=True)
    target.write_text(json.dumps(data, ensure_ascii=False, indent=2), encoding='utf-8')
    return target


def load_task(path):
    path = Path(path).resolve()
    data = bem.load_json(path)
    bem.require(data.get('schema') == 1 and data.get('kind') == KIND, 'Not a BEM export task project')
    bem.require(data.get('mode') in ('convert', 'pack'), 'Unsupported export task mode')
    bem.require(isinstance(data.get('package'), dict) and data['package'].get('id'),
                'Export project needs a stable package.id; create or save the project first')
    from bem_export import package_overrides
    package_overrides({'package_id': data['package']['id']}, data['package'])
    root = path.parent
    task = dict(data, project_file=path)
    for key in ('source', 'output'):
        task[key] = resolve(root, data[key])
    for key in ('recipe', 'report', 'deformations'):
        task[key] = resolve(root, data[key]) if data.get(key) else None
    bem.require(task['source'].exists(), 'Project source does not exist: ' + str(task['source']))
    if task['recipe']:
        bem.require(task['mode'] == 'convert' and task['recipe'].is_file(), 'Invalid project conversion recipe')
        if task['deformations'] is None:
            recipe_data = bem.load_json(task['recipe'])
            if recipe_data.get('deformations'):
                task['deformations'] = resolve(task['recipe'].parent, recipe_data['deformations'])
    bem.require(task['output'].suffix.lower() == '.bem', 'Project output must end in .bem')
    inputs = {path, task['source']}
    if task['recipe']: inputs.add(task['recipe'])
    if task['deformations']:
        from bem_v13 import author_input_paths
        bem.require(task['deformations'].is_file(), 'Missing position morph inputs')
        inputs.update(author_input_paths(task['deformations']))
    if task['mode'] == 'pack':
        source = bem.load_json(task['source'])
        inputs.update(resolve(task['source'].parent, n) for n in source['payload_files'])
    bem.require(task['output'] not in inputs, 'Project output cannot overwrite an input')
    bem.require(task['report'] is None or task['report'] not in inputs | {task['output']},
                'Project report cannot overwrite an input/output')
    task['input_paths'] = inputs
    return task


def new_project(source, project_path, mode='convert', recipe=None, package=None, export_output=None, deformations=None):
    source, project_path = Path(source).resolve(), Path(project_path).resolve()
    bem.require(mode in ('convert', 'pack') and source.exists(), 'Invalid project source/mode')
    bem.require(project_path != source, 'Project cannot overwrite source')
    root = project_path.parent
    metadata = {}
    if recipe:
        recipe = Path(recipe).resolve()
        bem.require(recipe != project_path, 'Project cannot overwrite recipe')
        recipe_data = bem.load_json(recipe)
        metadata = dict(recipe_data['package'])
        if deformations is None and recipe_data.get('deformations'):
            deformations = resolve(recipe.parent, recipe_data['deformations'])
    elif mode == 'pack':
        manifest = bem.load_json(source)['manifest']
        metadata = dict(id=manifest['package_id'], name=manifest['name'], author=manifest['author'], version=manifest['version'])
    metadata.update(package or {})
    metadata.setdefault('id', 'creator.' + uuid.uuid4().hex)
    metadata.setdefault('name', source.stem or '角色外观')
    metadata.setdefault('author', '未填写')
    metadata.setdefault('version', '1.0.0')
    data = dict(schema=1, kind=KIND, mode=mode, source=relative(root, source),
                output=relative(root, Path(export_output).resolve()) if export_output else 'dist/' + source.stem + '.bem',
                report='reports/build.json', package=metadata)
    if recipe: data['recipe'] = relative(root, recipe)
    if deformations:
        from bem_v13 import author_input_paths
        deformations = Path(deformations).resolve()
        bem.require(project_path not in author_input_paths(deformations), 'Project cannot overwrite shape input')
        data['deformations'] = relative(root, deformations)
    bem.atomic_write(project_path, json.dumps(data, ensure_ascii=False, indent=2).encode('utf-8'))
    return dict(project=data, output=str(project_path), conversion_ready=False)


def init_workspace(workspace, source, mode='convert', recipe=None, package=None,
                   deformations=None, export_output=None):
    """Create a self-contained standard creator workspace.

    The generated file is the existing ``bem-export-task`` format.  Inputs are
    copied into the workspace so the resulting task can be moved and built
    without depending on the original source locations.
    """
    workspace = Path(workspace).resolve()
    source = Path(source).resolve()
    bem.require(mode in ('convert', 'pack') and source.exists(), 'Invalid workspace source/mode')
    bem.require(workspace != source and not workspace.is_relative_to(source),
                'Workspace cannot contain or overwrite its source')
    if workspace.exists():
        bem.require(workspace.is_dir() and not any(workspace.iterdir()),
                    'Workspace directory must be new or empty: ' + str(workspace))
    workspace.parent.mkdir(parents=True, exist_ok=True)

    recipe_origin = Path(recipe).resolve() if recipe else None
    deformation_origin = Path(deformations).resolve() if deformations else None
    if recipe_origin:
        bem.require(recipe_origin.is_file(), 'Workspace recipe does not exist: ' + str(recipe_origin))
    if deformation_origin:
        bem.require(deformation_origin.is_file(), 'Workspace deformations do not exist: ' + str(deformation_origin))
    if mode == 'pack':
        bem.require(source.is_file(), 'Pack workspace source must be project.json')

    staging = Path(tempfile.mkdtemp(prefix='.bem-workspace-', dir=str(workspace.parent)))
    try:
        for directory in ('source', 'recipe', 'project', 'textures', 'dist', 'reports'):
            (staging / directory).mkdir()

        if mode == 'pack':
            staged_source = _copy_pack_project(source, staging / 'project')
            source_for_task = staged_source
            manifest = bem.load_json(source)['manifest']
            metadata = dict(id=manifest['package_id'], name=manifest['name'],
                            author=manifest['author'], version=manifest['version'])
        else:
            staged_source = staging / 'source' / source.name
            _copy_path(source, staged_source)
            source_for_task = staged_source
            metadata = {}

        recipe_for_task = None
        staged_deformations = None
        if deformation_origin:
            staged_deformations = _copy_deformations(deformation_origin, staging / 'recipe')
        if recipe_origin:
            recipe_data = bem.load_json(recipe_origin)
            metadata.update(recipe_data.get('package') or {})
            if staged_deformations is None and recipe_data.get('deformations'):
                referenced = (recipe_origin.parent / Path(recipe_data['deformations'])).resolve()
                if referenced.is_file():
                    staged_deformations = _copy_deformations(referenced, staging / 'recipe')
                    deformation_origin = referenced
            recipe_for_task = _copy_recipe(recipe_origin, staging / 'recipe', source,
                                           staged_source, deformation_origin, staged_deformations)

        metadata.update(package or {})
        metadata.setdefault('id', 'creator.' + uuid.uuid4().hex)
        metadata.setdefault('name', source.stem or '角色外观')
        metadata.setdefault('author', '未填写')
        metadata.setdefault('version', '1.0.0')
        output = Path(export_output) if export_output else Path('dist') / (source.stem + '.bem')
        if output.is_absolute():
            output = Path(os.path.relpath(output, staging))
        _safe_relative_path(output.as_posix())
        output_path = (staging / output).resolve()
        bem.require(output_path.is_relative_to(staging), 'Workspace output must stay inside workspace')
        bem.require(output_path.suffix.lower() == '.bem', 'Workspace output must end in .bem')
        data = dict(schema=1, kind=KIND, mode=mode,
                    source=relative(staging, source_for_task),
                    output=output.as_posix(), report='reports/build.json', package=metadata)
        if recipe_for_task:
            data['recipe'] = relative(staging, recipe_for_task)
        if staged_deformations:
            data['deformations'] = relative(staging, staged_deformations)
        task_path = staging / 'export.bemproj.json'
        bem.atomic_write(task_path, json.dumps(data, ensure_ascii=False, indent=2).encode('utf-8'))

        if workspace.exists():
            for child in staging.iterdir():
                os.replace(child, workspace / child.name)
            staging.rmdir()
        else:
            os.replace(staging, workspace)
        return dict(project=data, output=str(workspace / 'export.bemproj.json'),
                    workspace=str(workspace), conversion_ready=False)
    except Exception:
        shutil.rmtree(staging, ignore_errors=True)
        raise


def build_task(task):
    if task['mode'] == 'pack':
        from bem_projects import pack_project
        result = pack_project(task['source'], task['output'], task['package'], task['deformations'])
    else:
        from bem_tool import convert, convert_automatic
        result = (convert(task['source'], task['recipe'], task['output'], task['package'], task['deformations']) if task['recipe'] else
                  convert_automatic(task['source'], task['output'], package=task['package'], deformations=task['deformations']))
    return dict(result, output=str(task['output']), project_file=str(task['project_file']))
