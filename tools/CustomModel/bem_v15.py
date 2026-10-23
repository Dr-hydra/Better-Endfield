"""BEM 1.5 material and shader overrides specification.

Defines material-overrides-v1 capability and material_overrides schema on draws.
"""
from __future__ import annotations

import math
import re
from pathlib import PurePosixPath

import bem_v1 as bem
import bem_v14

CAPABILITIES = ('material-overrides-v1', 'root-offset-v1', 'dynamic-drivers-v1')

PROP_RE = re.compile(r'^_[a-zA-Z0-9_]{1,95}$')
KEYWORD_RE = re.compile(r'^[A-Z0-9_]{2,96}$')
RELAXED_PROP_RE = re.compile(r'^[a-zA-Z0-9_]{1,96}$')

# Section 13 quotas. The native parser applies the same numbers, so a manifest
# that validates here cannot be silently truncated at runtime.
MAX_DRIVERS, MAX_BINDINGS = 16, 32
WAVEFORMS = ('pingpong', 'sine', 'jitter', 'sawtooth', 'step')
BINDING_TYPES = ('morph_parameter', 'blend_shape', 'material_float', 'material_color',
                 'material_keyword')
DRIVER_KEYS = {'id', 'name', 'waveform', 'default_playing', 'base_frequency',
               'speed_multiplier', 'bindings'}



def validate_root_offset(m, skip_validation=False):
    key = 'root_offset' if 'root_offset' in m else ('visual_root_offset' if 'visual_root_offset' in m else None)
    if not key:
        return
    offset = m[key]
    bem.require(isinstance(offset, dict), f'{key} must be an object')
    bem.require('path' in offset, f'{key} missing path')
    path = offset['path']
    bem.require(isinstance(path, str) and 1 <= len(path) <= 256 and '\x00' not in path, f'Invalid {key} path')
    if not skip_validation:
        bem.require(not path.startswith('/') and not path.endswith('/') and '//' not in path and '..' not in path, f'Invalid {key} path separators')

    if 'translation' in offset:
        t = offset['translation']
        bem.require(isinstance(t, list) and len(t) == 3, f'{key} translation must be array of 3 numbers')
        for v in t:
            bem.require(isinstance(v, (int, float)) and math.isfinite(v), f'{key} translation must contain finite numbers')
        squared = sum(x*x for x in t)
        max_dist = 1.0 if skip_validation else 0.2
        bem.require(squared <= max_dist*max_dist + 1e-4, f'{key} translation exceeds maximum distance ({max_dist}m)')
    elif 'translation_bits' in offset:
        t_bits = offset['translation_bits']
        bem.require(isinstance(t_bits, list) and len(t_bits) == 3, f'{key} translation_bits must be array of 3 integers')
        for b in t_bits:
            bem.require(type(b) is int and 0 <= b <= 0xffffffff, f'{key} translation_bits must be UInt32')
    else:
        bem.require(False, f'{key} requires translation or translation_bits')

def used(m):
    caps = m.get('required_capabilities', [])
    if any(c in caps for c in CAPABILITIES):
        return True
    if 'root_offset' in m or 'visual_root_offset' in m:
        return True
    if m.get('drivers'):
        return True
    for mesh in m.get('meshes', []):
        for draw in mesh.get('draws', []):
            if 'material_overrides' in draw:
                return True
    return False


def validate_material_overrides(overrides, skip_validation=False):
    bem.require(isinstance(overrides, dict), 'material_overrides must be an object')
    allowed_keys = {'floats', 'colors', 'vectors', 'disable_keywords', 'enable_keywords'}
    if not skip_validation:
        bem.require(set(overrides.keys()) <= allowed_keys, 'Invalid keys in material_overrides')

    max_floats = 128 if skip_validation else 32
    max_colors = 64 if skip_validation else 16
    max_vectors = 32 if skip_validation else 8
    max_keywords = 64 if skip_validation else 16

    floats = overrides.get('floats', {})
    if floats:
        bem.require(isinstance(floats, dict), 'floats must be an object')
        bem.require(len(floats) <= max_floats, f'Too many float overrides (max {max_floats})')
        for name, val in floats.items():
            bem.require(isinstance(name, str) and 2 <= len(name) <= 96, 'Invalid float property name length')
            if not skip_validation:
                bem.require(PROP_RE.match(name) is not None, f'Float property name must match ^_[a-zA-Z0-9_]{{1,95}}$: {name}')
            bem.require(isinstance(val, (int, float)) and math.isfinite(val), f'Float property {name} must be a finite number')
            if not skip_validation:
                bem.require(-128.0 <= val <= 128.0, f'Float property {name} value out of range [-128, 128]')

    colors = overrides.get('colors', {})
    if colors:
        bem.require(isinstance(colors, dict), 'colors must be an object')
        bem.require(len(colors) <= max_colors, f'Too many color overrides (max {max_colors})')
        for name, val in colors.items():
            bem.require(isinstance(name, str) and 2 <= len(name) <= 96, 'Invalid color property name length')
            if not skip_validation:
                bem.require(PROP_RE.match(name) is not None, f'Color property name must match ^_[a-zA-Z0-9_]{{1,95}}$: {name}')
            bem.require(isinstance(val, list) and len(val) == 4, f'Color {name} must be an RGBA array of length 4')
            for c in val:
                bem.require(isinstance(c, (int, float)) and math.isfinite(c), f'Color {name} channels must be finite numbers')
                if not skip_validation:
                    bem.require(0.0 <= c <= 8.0, f'Color {name} channel out of range [0, 8]')

    vectors = overrides.get('vectors', {})
    if vectors:
        bem.require(isinstance(vectors, dict), 'vectors must be an object')
        bem.require(len(vectors) <= max_vectors, f'Too many vector overrides (max {max_vectors})')
        for name, val in vectors.items():
            bem.require(isinstance(name, str) and 2 <= len(name) <= 96, 'Invalid vector property name length')
            if not skip_validation:
                bem.require(PROP_RE.match(name) is not None, f'Vector property name must match ^_[a-zA-Z0-9_]{{1,95}}$: {name}')
            bem.require(isinstance(val, list) and len(val) == 4, f'Vector {name} must be an array of length 4')
            for v in val:
                bem.require(isinstance(v, (int, float)) and math.isfinite(v), f'Vector {name} values must be finite numbers')
                if not skip_validation:
                    bem.require(-65536.0 <= v <= 65536.0, f'Vector {name} component out of range [-65536, 65536]')

    for kw_key in ('disable_keywords', 'enable_keywords'):
        keywords = overrides.get(kw_key, [])
        if keywords:
            bem.require(isinstance(keywords, list), f'{kw_key} must be a list')
            bem.require(len(keywords) <= max_keywords, f'Too many keywords in {kw_key} (max {max_keywords})')
            for kw in keywords:
                bem.require(isinstance(kw, str) and 2 <= len(kw) <= 96, f'Invalid keyword length in {kw_key}')
                if not skip_validation:
                    bem.require(KEYWORD_RE.match(kw) is not None, f'Keyword must match ^[A-Z0-9_]{{2,96}}$: {kw}')


def _range(node, label, low, high, skip_validation):
    if 'range' not in node:
        return (low, high)
    value = node['range']
    bem.require(isinstance(value, list) and len(value) == 2, f'{label} range must be two numbers')
    for v in value:
        bem.require(isinstance(v, (int, float)) and not isinstance(v, bool) and math.isfinite(v),
                    f'{label} range must contain finite numbers')
    bem.require(value[0] < value[1], f'{label} range must be increasing')
    return (value[0], value[1])


def _rgba(value, label, skip_validation):
    bem.require(isinstance(value, list) and len(value) == 4, f'{label} must be an RGBA array of 4')
    for c in value:
        bem.require(isinstance(c, (int, float)) and not isinstance(c, bool) and math.isfinite(c),
                    f'{label} channels must be finite numbers')
        if not skip_validation:
            bem.require(0.0 <= c <= 8.0, f'{label} channel out of range [0, 8]')


def validate_drivers(m, skip_validation=False):
    """dynamic-drivers-v1: the clock controller and its binding table (section 13)."""
    drivers = m.get('drivers')
    if drivers is None:
        return
    bem.require(isinstance(drivers, list), 'drivers must be an array')
    bem.require(len(drivers) <= (MAX_DRIVERS * 2 if skip_validation else MAX_DRIVERS),
                f'Too many animation drivers (max {MAX_DRIVERS})')
    parameters = {p['id'] for p in m.get('parameters', [])}
    components = m['target']['components']
    ids = set()
    for driver in drivers:
        bem.require(isinstance(driver, dict), 'Each driver must be an object')
        if not skip_validation:
            bem.require(set(driver) <= DRIVER_KEYS, 'Invalid driver fields')
        did = bem.identity(driver.get('id')) if 'id' in driver else None
        bem.require(did is not None, 'Driver is missing an id')
        bem.require(did not in ids, f'Duplicate driver id: {did}')
        ids.add(did)
        if 'name' in driver:
            name = driver['name']
            bem.require(isinstance(name, str) and 0 < len(name) <= 64 and '\x00' not in name,
                        f'Invalid driver name: {did}')
        waveform = driver.get('waveform', 'pingpong')
        bem.require(waveform in WAVEFORMS, f'Unknown driver waveform: {waveform}')
        bem.require(isinstance(driver.get('default_playing', False), bool),
                    f'default_playing must be a boolean: {did}')
        frequency = driver.get('base_frequency', 1.0)
        bem.require(isinstance(frequency, (int, float)) and not isinstance(frequency, bool) and
                    math.isfinite(frequency) and 0.0 <= frequency <= 10.0,
                    f'base_frequency must be a number in [0, 10] Hz: {did}')
        speed = driver.get('speed_multiplier', 1.0)
        bem.require(isinstance(speed, (int, float)) and not isinstance(speed, bool) and
                    math.isfinite(speed) and 0.0 <= speed <= 10.0,
                    f'speed_multiplier must be a number in [0, 10]: {did}')
        bindings = driver.get('bindings')
        bem.require(isinstance(bindings, list) and 1 <= len(bindings) <=
                    (MAX_BINDINGS * 2 if skip_validation else MAX_BINDINGS),
                    f'Driver {did} needs 1..{MAX_BINDINGS} bindings')
        for binding in bindings:
            bem.require(isinstance(binding, dict), f'Driver {did} binding must be an object')
            kind = binding.get('type', 'morph_parameter')
            bem.require(kind in BINDING_TYPES, f'Unknown binding type in {did}: {kind}')
            if kind in ('morph_parameter', 'blend_shape'):
                target = binding.get('target')
                bem.require(isinstance(target, str) and bool(target),
                            f'Morph binding in {did} is missing target')
                if not skip_validation:
                    bem.require(target in parameters,
                                f'Morph binding target is not a declared parameter: {target}')
                low, high = _range(binding, f'Morph binding in {did}', 0.0, 1.0, skip_validation)
                if not skip_validation:
                    bem.require(0.0 <= low and high <= 1.0,
                                f'Morph binding range must stay inside [0, 1]: {did}')
                continue
            component = binding.get('component', 0)
            slot = binding.get('material_slot', 0)
            bem.require(type(component) is int and 0 <= component < len(components),
                        f'Material binding in {did} names an unknown component')
            bem.require(type(slot) is int and 0 <= slot < len(components[component]['materials']),
                        f'Material binding in {did} names an unknown material slot')
            if kind == 'material_float':
                prop = binding.get('property')
                bem.require(isinstance(prop, str), f'material_float binding in {did} is missing property')
                if not skip_validation:
                    bem.require(PROP_RE.match(prop) is not None,
                                f'material_float property must match ^_[a-zA-Z0-9_]{{1,95}}$: {prop}')
                low, high = _range(binding, f'material_float binding in {did}', 0.0, 1.0,
                                   skip_validation)
                if not skip_validation:
                    bem.require(-128.0 <= low and high <= 128.0,
                                f'material_float range out of bounds: {did}')
            elif kind == 'material_color':
                prop = binding.get('property')
                bem.require(isinstance(prop, str), f'material_color binding in {did} is missing property')
                if not skip_validation:
                    bem.require(PROP_RE.match(prop) is not None,
                                f'material_color property must match ^_[a-zA-Z0-9_]{{1,95}}$: {prop}')
                colors = binding.get('colors')
                bem.require(isinstance(colors, list) and len(colors) == 2,
                            f'material_color binding in {did} needs two RGBA endpoints')
                _rgba(colors[0], f'material_color from in {did}', skip_validation)
                _rgba(colors[1], f'material_color to in {did}', skip_validation)
            else:
                keyword = binding.get('keyword')
                bem.require(isinstance(keyword, str), f'material_keyword binding in {did} is missing keyword')
                if not skip_validation:
                    bem.require(KEYWORD_RE.match(keyword) is not None,
                                f'material_keyword must match ^[A-Z0-9_]{{2,96}}$: {keyword}')
                threshold = binding.get('threshold', 0.5)
                bem.require(isinstance(threshold, (int, float)) and not isinstance(threshold, bool) and
                            math.isfinite(threshold) and 0.0 <= threshold <= 1.0,
                            f'material_keyword threshold must be inside [0, 1]: {did}')
    if not skip_validation:
        bem.require('dynamic-drivers-v1' in m['required_capabilities'],
                    'drivers requires dynamic-drivers-v1 capability')


def validate_manifest(m, payload_count, minor=None, skip_validation=False):
    bem.require(minor in (None, 5), 'material-overrides-v1 requires BEM 1.5')
    bem.require('option_groups' in m, 'BEM 1.5 requires a composable project')
    # 1.5 extends 1.3. The 1.4 explicit resource targets stay an optional branch
    # that is selected by the target structure itself, never by the header
    # version, so a 1.3-style composable manifest keeps its own validation and
    # only gains the section 12/13 checks on top.
    if 'resources' in m['target']:
        bem_v14.validate_manifest(m, payload_count, 5)
    else:
        bem.require(not set(m['required_capabilities']) & set(bem_v14.CAPABILITIES),
                    'Resource capability requires explicit resource targets')
        bem.require(not any(key in c for c in m['target']['components']
                            for key in ('resource', 'renderer_kind', 'renderer_path')) and
                    not any('renderer_kind' in mesh for mesh in m['meshes']),
                    'Renderer kind requires explicit resource targets')
        import bem_v13
        bem_v13.validate_manifest(m, payload_count, 5)

    caps = m.get('required_capabilities', [])
    has_overrides = False

    for mesh in m.get('meshes', []):
        for draw in mesh.get('draws', []):
            if 'material_overrides' in draw:
                has_overrides = True
                validate_material_overrides(draw['material_overrides'], skip_validation=skip_validation)

    if has_overrides and not skip_validation:
        bem.require('material-overrides-v1' in caps, 'material_overrides requires material-overrides-v1 capability')

    validate_root_offset(m, skip_validation=skip_validation)
    validate_drivers(m, skip_validation=skip_validation)
    if ('root_offset' in m or 'visual_root_offset' in m) and not skip_validation:
        bem.require('root-offset-v1' in caps or 'material-overrides-v1' in caps,
                    'root_offset requires root-offset-v1 or material-overrides-v1 capability')