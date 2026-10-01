"""Portable, repeatable export tasks; JSON contains data, never shell commands."""
import json
import uuid
from pathlib import Path
import os
import bem_v1 as bem

KIND = 'bem-export-task'


def resolve(root, name):
    bem.require(isinstance(name, str) and bool(name.strip()), 'Missing project path')
    path = Path(name)
    return (path if path.is_absolute() else root / path).resolve()


def relative(root, path):
    try: return Path(os.path.relpath(path, root)).as_posix()
    except ValueError: return str(path)  # Different Windows volumes.


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
    for key in ('recipe', 'report'):
        task[key] = resolve(root, data[key]) if data.get(key) else None
    bem.require(task['source'].exists(), 'Project source does not exist: ' + str(task['source']))
    if task['recipe']:
        bem.require(task['mode'] == 'convert' and task['recipe'].is_file(), 'Invalid project conversion recipe')
    bem.require(task['output'].suffix.lower() == '.bem', 'Project output must end in .bem')
    inputs = {path, task['source']}
    if task['recipe']: inputs.add(task['recipe'])
    if task['mode'] == 'pack':
        source = bem.load_json(task['source'])
        inputs.update(resolve(task['source'].parent, n) for n in source['payload_files'])
    bem.require(task['output'] not in inputs, 'Project output cannot overwrite an input')
    bem.require(task['report'] is None or task['report'] not in inputs | {task['output']},
                'Project report cannot overwrite an input/output')
    task['input_paths'] = inputs
    return task


def new_project(source, project_path, mode='convert', recipe=None, package=None, export_output=None):
    source, project_path = Path(source).resolve(), Path(project_path).resolve()
    bem.require(mode in ('convert', 'pack') and source.exists(), 'Invalid project source/mode')
    bem.require(project_path != source, 'Project cannot overwrite source')
    root = project_path.parent
    metadata = {}
    if recipe:
        recipe = Path(recipe).resolve()
        bem.require(recipe != project_path, 'Project cannot overwrite recipe')
        metadata = dict(bem.load_json(recipe)['package'])
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
    bem.atomic_write(project_path, json.dumps(data, ensure_ascii=False, indent=2).encode('utf-8'))
    return dict(project=data, output=str(project_path), conversion_ready=False)


def build_task(task):
    if task['mode'] == 'pack':
        from bem_projects import pack_project
        result = pack_project(task['source'], task['output'], task['package'])
    else:
        from bem_tool import convert, convert_automatic
        result = (convert(task['source'], task['recipe'], task['output'], task['package']) if task['recipe'] else
                  convert_automatic(task['source'], task['output'], package=task['package']))
    return dict(result, output=str(task['output']), project_file=str(task['project_file']))
