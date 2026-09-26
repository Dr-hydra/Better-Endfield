"""BEM 1.1/1.2 finite option rules and selected-resource planning.

No source INI or shader program is executed. Creator validation uses an exact
multi-valued decision diagram; runtime evaluates one selected setting.

BEM 1.2 keeps the 1.1 manifest and adds larger limits, option-selected texture
slots and per-resource bone name aliases. Writers use 1.2 only when content
needs it, so packages within 1.1 limits stay readable by 1.1 runtimes.
"""
from __future__ import annotations

import itertools
import struct
from pathlib import Path

import zstandard as zstd

import bem_v1 as v1

MAX_GROUPS = 64
MAX_CHOICES = 16
MAX_RULES = 512
MAX_SELECTED_TEXTURES = 32
MAX_TEXTURE_BYTES = 64 * 1024**2
MAX_SELECTED_RESIDENT = 768 * 1024**2
MAX_SELECTED_DECODED = 768 * 1024**2
MAX_DIRECTORY = 4096
MAX_CHOICES_V12 = 64
MAX_RULES_V12 = 4096
MAX_SELECTED_TEXTURES_V12 = 64
MAX_TEXTURE_BYTES_V12 = 256 * 1024**2
MAX_SELECTED_RESIDENT_V12 = 1536 * 1024**2
MAX_SELECTED_DECODED_V12 = 1536 * 1024**2
MAX_DIRECTORY_V12 = 16384
MAX_TEXTURE_SLOTS = 1024
MAX_SYMBOLIC_NODES = 250000
MAX_OPTIMIZATION_STATES = 250000
V12_CAPABILITIES = ('texture-slots', 'resource-bone-aliases')
# Skin stream declarations by stride. 32 (float weights, UInt32 indices) is the
# uncompressed native layout some game meshes use; it needs BEM 1.2.
SKIN_LAYOUTS = {4: [[13, 6, 4, 2, 0]], 12: [[12, 4, 4, 2, 0], [13, 6, 4, 2, 8]],
                32: [[12, 0, 4, 2, 0], [13, 10, 4, 2, 16]]}
BONE_ALIAS_RESOURCES = ('world', 'ui')


def limits(minor=None):
    """Format limits for a header minor; None means the newest (creator default)."""
    if minor == 1:
        return dict(minor=1, choices=MAX_CHOICES, rules=MAX_RULES, textures=MAX_SELECTED_TEXTURES,
                    texture_bytes=MAX_TEXTURE_BYTES, resident=MAX_SELECTED_RESIDENT,
                    decoded=MAX_SELECTED_DECODED, directory=MAX_DIRECTORY)
    v1.require(minor in (None, 2), 'Unsupported BEM minor version')
    return dict(minor=2, choices=MAX_CHOICES_V12, rules=MAX_RULES_V12, textures=MAX_SELECTED_TEXTURES_V12,
                texture_bytes=MAX_TEXTURE_BYTES_V12, resident=MAX_SELECTED_RESIDENT_V12,
                decoded=MAX_SELECTED_DECODED_V12, directory=MAX_DIRECTORY_V12)


def texture_bytes(texture):
    """Exact mip-chain byte count implied by a texture descriptor."""
    width, height, mips, fmt = texture['width'], texture['height'], texture['mips'], texture['format']
    pixel = {4: 4, 63: 1}.get(fmt, 0)
    if pixel:
        return sum(max(width >> level, 1) * max(height >> level, 1) * pixel for level in range(mips))
    astc = fmt in (48, 49, 50)
    block = 8 if fmt in (10, 26) else 16
    block_width = fmt - 44 if astc else 4
    return sum(((max(width >> level, 1) + block_width - 1) // block_width) *
               ((max(height >> level, 1) + block_width - 1) // block_width) * block
               for level in range(mips))


def candidate_count(m):
    return (sum(len(rule['candidates']) for rule in m['component_rules']) +
            sum(len(mesh['draws']) for mesh in m['meshes']) +
            sum(len(slot['candidates']) for slot in m.get('texture_slots', [])))


def required_minor(m, summary, payload_count=0):
    """Smallest header minor able to carry this composable manifest."""
    old = limits(1)
    needs = (bool(m.get('texture_slots')) or
             any(component.get('bone_name_aliases') for component in m['target']['components']) or
             any(len(group['choices']) > old['choices'] for group in m['option_groups']) or
             candidate_count(m) > old['rules'] or payload_count > old['directory'] or
             any(texture_bytes(texture) > old['texture_bytes'] for texture in m['textures']) or
             any(mesh['streams'][2]['stride'] == 32 for mesh in m['meshes']) or
             summary['max_selected_textures'] > old['textures'] or
             summary.get('max_selected_decoded_bytes', 0) > old['decoded'] or
             summary.get('max_resident_bytes', 0) > old['resident'])
    return 2 if needs else 1


def _texture_refs(refs, slots, label):
    """Draw/keep texture list: fixed texture indices or {"slot": id} references."""
    fixed, named = set(), set()
    for ref in refs:
        if isinstance(ref, dict):
            v1.require(set(ref) == {'slot'} and ref['slot'] in slots and ref['slot'] not in named,
                       f'Invalid {label} texture slot reference')
            named.add(ref['slot'])
        else:
            v1.require(type(ref) is int and ref not in fixed, f'Invalid {label} texture')
            fixed.add(ref)
    return fixed, named


def _name(value, label):
    v1.require(isinstance(value, str) and 0 < len(value.encode('utf-8')) <= 256,
               f'Invalid {label}')


def _condition(node, groups, *, available_before=None, depth=0):
    v1.require(depth <= 16, 'Condition exceeds depth 16')
    if type(node) is bool:
        return
    v1.require(isinstance(node, dict) and len(node) == 1, 'Invalid condition node')
    kind, value = next(iter(node.items()))
    if kind == 'eq':
        v1.require(isinstance(value, list) and len(value) == 2, 'eq needs group and choice')
        group, choice = value
        v1.require(group in groups and choice in groups[group], 'Condition references missing choice')
        if available_before is not None:
            v1.require(group in available_before, 'available_when must reference an earlier group')
    elif kind in ('all', 'any'):
        v1.require(isinstance(value, list) and 0 < len(value) <= 32, 'Invalid condition operands')
        for child in value:
            _condition(child, groups, available_before=available_before, depth=depth+1)
    elif kind == 'not':
        _condition(value, groups, available_before=available_before, depth=depth+1)
    else:
        raise ValueError(f'Unsupported condition: {kind}')


def evaluate(node, effective):
    if type(node) is bool:
        return node
    kind, value = next(iter(node.items()))
    if kind == 'eq':
        return effective.get(value[0]) == value[1]
    if kind == 'all':
        return all(evaluate(child, effective) for child in value)
    if kind == 'any':
        return any(evaluate(child, effective) for child in value)
    return not evaluate(value, effective)


class _Conditions:
    """Canonical multi-valued diagram over saved group choices (0=false, 1=true)."""

    def __init__(self, option_groups):
        self.groups = option_groups
        self.levels = {group['id']: i for i, group in enumerate(option_groups)}
        self.values = {group['id']: {choice['id']: j for j, choice in
                                    enumerate(group['choices'])} for group in option_groups}
        self.nodes = [None, None]
        self.unique = {}
        self.binary_cache = {}
        self.not_cache = {0: 1, 1: 0}
        self.available = {}
        self.atoms = {}
        for group in option_groups:
            gid = group['id']
            self.available[gid] = self.condition(group.get('available_when', True))
            level = self.levels[gid]
            for choice, value in self.values[gid].items():
                children = [0] * len(group['choices'])
                children[value] = 1
                self.atoms[gid, choice] = self.both(self.available[gid],
                                                    self.make(level, children))

    def make(self, level, children):
        children = tuple(children)
        if all(child == children[0] for child in children):
            return children[0]
        key = level, children
        if key not in self.unique:
            v1.require(len(self.nodes) - 2 < MAX_SYMBOLIC_NODES,
                       'Symbolic condition graph exceeds node limit')
            self.unique[key] = len(self.nodes)
            self.nodes.append(key)
        return self.unique[key]

    def top(self, node):
        return self.nodes[node][0] if node > 1 else len(self.groups)

    def child(self, node, level, choice):
        return self.nodes[node][1][choice] if self.top(node) == level else node

    def negate(self, node):
        if node not in self.not_cache:
            level, children = self.nodes[node]
            self.not_cache[node] = self.make(level, [self.negate(c) for c in children])
        return self.not_cache[node]

    def combine(self, op, a, b):
        if op == 'and':
            if a == 0 or b == 0: return 0
            if a == 1: return b
            if b == 1: return a
            if a == b: return a
        else:
            if a == 1 or b == 1: return 1
            if a == 0: return b
            if b == 0: return a
            if a == b: return a
        if a > b: a, b = b, a
        key = op, a, b
        if key not in self.binary_cache:
            level = min(self.top(a), self.top(b))
            children = [self.combine(op, self.child(a, level, j), self.child(b, level, j))
                        for j in range(len(self.groups[level]['choices']))]
            self.binary_cache[key] = self.make(level, children)
        return self.binary_cache[key]

    def both(self, a, b):
        return self.combine('and', a, b)

    def either(self, a, b):
        return self.combine('or', a, b)

    def condition(self, node):
        if type(node) is bool:
            return int(node)
        kind, value = next(iter(node.items()))
        if kind == 'eq':
            return self.atoms[value[0], value[1]]
        if kind == 'not':
            return self.negate(self.condition(value))
        result = 1 if kind == 'all' else 0
        for child in value:
            result = self.combine('and' if kind == 'all' else 'or',
                                  result, self.condition(child))
        return result

    def maximum(self, reachable, weighted):
        """Maximum of sum(weight * predicate) on the reachable domain.

        Exact when the search fits MAX_OPTIMIZATION_STATES; otherwise returns the
        sound upper bound sum(weights) and sets `self.bounded` (many independent
        options sharing terms can make the exact search exponential).
        """
        weighted = tuple((node, weight) for node, weight in weighted if node and weight)
        memo = {}

        class Overflow(Exception):
            pass

        def solve(domain, terms):
            if domain == 0:
                return -1
            base = sum(weight for node, weight in terms if node == 1)
            terms = tuple(sorted((node, weight) for node, weight in terms if node > 1))
            if not terms:
                return base
            key = domain, terms
            if key in memo:
                return base + memo[key]
            if len(memo) >= MAX_OPTIMIZATION_STATES:
                raise Overflow
            level = min(self.top(domain), *(self.top(node) for node, _ in terms))
            best = max(solve(self.child(domain, level, j),
                             tuple((self.child(node, level, j), weight) for node, weight in terms))
                       for j in range(len(self.groups[level]['choices'])))
            memo[key] = best
            return base + best

        try:
            return solve(reachable, weighted)
        except Overflow:
            self.bounded = True
            return sum(weight for _, weight in weighted)

    def count(self, domain):
        """Exact saved-assignment model count, including skipped group levels."""
        memo = {}

        def solve(node, level):
            if node == 0: return 0
            if level == len(self.groups): return 1
            key = node, level
            if key in memo: return memo[key]
            v1.require(len(memo) < MAX_OPTIMIZATION_STATES,
                       'Symbolic model count exceeds state limit')
            result = sum(solve(self.child(node, level, j), level + 1)
                         for j in range(len(self.groups[level]['choices'])))
            memo[key] = result
            return result

        return solve(domain, 0)


def analyze_selection_space(m, payloads=None, minor=None):
    """Exact selection proof summary; call after structural manifest validation.

    `payloads` may hold bytes or anything with len(); only sizes are used.
    """
    limit = limits(minor)
    logic = _Conditions(m['option_groups'])
    reachable = 1
    for rule in m.get('selection_constraints', []):
        reachable = logic.both(reachable, logic.condition(rule))
    v1.require(reachable != 0, 'No reachable option combination')
    defaults = {group['id']: group['default'] for group in m['option_groups']}
    selection_plan(m, defaults, minor=minor)
    canonical = reachable
    for group in m['option_groups']:
        gid = group['id']
        raw_default = [0] * len(group['choices'])
        raw_default[logic.values[gid][group['default']]] = 1
        raw_default = logic.make(logic.levels[gid], raw_default)
        canonical = logic.both(canonical, logic.either(logic.available[gid], raw_default))
    summary = {'saved_assignments': logic.count(reachable),
               'effective_selections': logic.count(canonical)}

    activations = [0] * len(m['meshes'])
    selected = []
    kept = []
    for component in m['component_rules']:
        covered = 0
        for candidate in component['candidates']:
            predicate = logic.condition(candidate.get('when', True))
            v1.require(logic.both(reachable, logic.both(covered, predicate)) == 0,
                       'Target needs exactly one selected operation')
            covered = logic.either(covered, predicate)
            if candidate['operation'] == 'replace':
                mesh = candidate['mesh']
                active = logic.both(reachable, predicate)
                activations[mesh] = logic.either(activations[mesh], active)
                selected.append((mesh, active))
            elif candidate['operation'] == 'keep':
                kept.append((candidate, logic.both(reachable, predicate)))
        v1.require(logic.both(reachable, logic.negate(covered)) == 0,
                   'Target needs exactly one selected operation')

    slots = m.get('texture_slots', [])
    slot_ids = {slot['id']: index for index, slot in enumerate(slots)}
    slot_predicates = []
    for slot in slots:
        covered, predicates = 0, []
        for candidate in slot['candidates']:
            predicate = logic.condition(candidate.get('when', True))
            v1.require(logic.both(reachable, logic.both(covered, predicate)) == 0,
                       'Texture slot needs exactly one selected candidate')
            covered = logic.either(covered, predicate)
            predicates.append(predicate)
        v1.require(logic.both(reachable, logic.negate(covered)) == 0,
                   'Texture slot needs exactly one selected candidate')
        slot_predicates.append(predicates)
    slot_used = [0] * len(slots)

    draw_predicates = [[logic.condition(draw.get('when', True)) for draw in mesh['draws']]
                       for mesh in m['meshes']]
    texture_predicates = [0] * len(m['textures'])

    def use_textures(refs, predicate):
        for ref in refs:
            if isinstance(ref, dict):
                index = slot_ids[ref['slot']]
                slot_used[index] = logic.either(slot_used[index], predicate)
            else:
                texture_predicates[ref] = logic.either(texture_predicates[ref], predicate)

    for candidate, active in kept:
        for material in candidate.get('material_overrides', []):
            use_textures(material['textures'], active)
    payload_predicates = [0] * len(payloads) if payloads is not None else None
    resident_terms = []
    summary['max_selected_draws'] = 0
    summary['max_selected_indices'] = 0
    for mesh_id, active in selected:
        if not active: continue
        mesh = m['meshes'][mesh_id]
        draws = draw_predicates[mesh_id]
        present = 0
        for predicate in draws:
            present = logic.either(present, predicate)
        v1.require(logic.both(active, logic.negate(present)) == 0,
                   'Selected replacement mesh has no draw')
        selected_draws = [(logic.both(active, predicate), draw) for predicate, draw in
                          zip(draws, mesh['draws'])]
        max_draws = logic.maximum(active, [(predicate, 1) for predicate, _ in selected_draws])
        summary['max_selected_draws'] = max(summary['max_selected_draws'], max_draws)
        v1.require(max_draws <= 256,
                   'Selected draw limit exceeded')
        max_indices = logic.maximum(active, [(predicate, draw['count']) for predicate, draw in
                                     selected_draws])
        summary['max_selected_indices'] = max(summary['max_selected_indices'], max_indices)
        v1.require(max_indices <= 16777216,
                   'Selected index limit exceeded')
        for predicate, draw in selected_draws:
            if payload_predicates is not None:
                payload_id = draw['indices']
                payload_predicates[payload_id] = logic.either(payload_predicates[payload_id], predicate)
            use_textures(draw['textures'], predicate)
            if payloads is not None:
                resident_terms.append((predicate, draw['count'] * mesh['index_size']))
        if payloads is not None:
            stream_bytes = sum(len(payloads[stream['payload']]) for stream in mesh['streams'])
            resident_terms.append((active, stream_bytes))
            for stream in mesh['streams']:
                payload_id = stream['payload']
                payload_predicates[payload_id] = logic.either(payload_predicates[payload_id], active)
    for used, slot, predicates in zip(slot_used, slots, slot_predicates):
        for candidate, predicate in zip(slot['candidates'], predicates):
            if candidate['texture'] is not None:
                texture = candidate['texture']
                texture_predicates[texture] = logic.either(texture_predicates[texture],
                                                           logic.both(used, predicate))
    summary['max_selected_textures'] = logic.maximum(
        reachable, [(predicate, 1) for predicate in texture_predicates])
    v1.require(summary['max_selected_textures'] <= limit['textures'],
               'Selected texture binding limit exceeded')
    if payloads is not None:
        resident_terms.extend((predicate, len(payloads[texture['payload']]))
                              for predicate, texture in zip(texture_predicates, m['textures']))
        for predicate, texture in zip(texture_predicates, m['textures']):
            payload_id = texture['payload']
            payload_predicates[payload_id] = logic.either(payload_predicates[payload_id], predicate)
        summary['max_selected_decoded_bytes'] = logic.maximum(
            reachable, [(predicate, len(payload)) for predicate, payload in
                        zip(payload_predicates, payloads)])
        budget = limit['decoded'] // 1024**2
        v1.require(summary['max_selected_decoded_bytes'] <= limit['decoded'],
                   f'Selected decoded payloads exceed {budget} MiB budget')
        summary['max_resident_bytes'] = logic.maximum(reachable, resident_terms)
        v1.require(summary['max_resident_bytes'] <= limit['resident'],
                   f'Selected state exceeds {limit["resident"] // 1024**2} MiB budget')
    summary['symbolic_nodes'] = len(logic.nodes) - 2
    if getattr(logic, 'bounded', False):
        summary['maxima'] = 'upper-bound'
    return summary


def _symbolic_check(m, *, payloads=None, minor=None):
    return analyze_selection_space(m, payloads, minor)


def _effective(manifest, saved):
    effective = {}
    for group in manifest['option_groups']:
        gid = group['id']
        effective[gid] = (saved[gid] if evaluate(group.get('available_when', True), effective)
                          else None)
    return effective


def resolve_texture_slots(manifest, effective):
    """Selected texture index (or None: keep the native texture) for each slot."""
    resolved = {}
    for slot in manifest.get('texture_slots', []):
        matching = [candidate for candidate in slot['candidates']
                    if evaluate(candidate.get('when', True), effective)]
        v1.require(len(matching) == 1, 'Texture slot needs exactly one selected candidate')
        resolved[slot['id']] = matching[0]['texture']
    return resolved


def _resolve_refs(refs, resolved):
    textures = []
    for ref in refs:
        texture = resolved[ref['slot']] if isinstance(ref, dict) else ref
        if texture is not None:
            textures.append(texture)
    return textures


def selection_plan(manifest, options=None, minor=None):
    """Pure, repeatable plan. Payload IDs are returned without reading bytes.

    Texture slot references are returned already resolved to texture indices.
    """
    limit = limits(minor)
    options = options or {}
    groups = {group['id']: group for group in manifest['option_groups']}
    v1.require(set(options) <= set(groups), 'Unknown option group')
    saved = {}
    for gid, group in groups.items():
        choice = options.get(gid, group['default'])
        v1.require(choice in {item['id'] for item in group['choices']}, 'Unknown option choice')
        saved[gid] = choice
    effective = _effective(manifest, saved)
    v1.require(all(evaluate(rule, effective) for rule in manifest.get('selection_constraints', [])),
               'Unreachable option combination')
    resolved = resolve_texture_slots(manifest, effective)
    operations, textures, payloads = [], set(), set()
    for component in manifest['component_rules']:
        matching = [rule for rule in component['candidates'] if evaluate(rule.get('when', True), effective)]
        v1.require(len(matching) == 1, 'Target needs exactly one selected operation')
        operation = matching[0]
        planned = {'target': component['target'], 'operation': operation['operation']}
        if operation['operation'] == 'keep':
            planned['material_overrides'] = [
                {**material, 'textures': _resolve_refs(material['textures'], resolved)}
                for material in operation.get('material_overrides', [])]
            for material in planned['material_overrides']:
                textures.update(material['textures'])
        if operation['operation'] == 'replace':
            mesh_id = operation['mesh']
            mesh = manifest['meshes'][mesh_id]
            selected = []
            start = 0
            for draw in mesh['draws']:
                if not evaluate(draw.get('when', True), effective):
                    continue
                draw = {**draw, 'textures': _resolve_refs(draw['textures'], resolved)}
                selected.append({**draw, 'start': start})
                start += draw['count']
                textures.update(draw['textures'])
                payloads.add(draw['indices'])
            v1.require(0 < len(selected) <= 256 and start <= 16777216,
                       'Selected draw/index limit exceeded')
            payloads.update(stream['payload'] for stream in mesh['streams'])
            planned.update(mesh=mesh_id, index_count=start, draws=selected)
        operations.append(planned)
    v1.require(len(textures) <= limit['textures'], 'Selected texture binding limit exceeded')
    payloads.update(manifest['textures'][index]['payload'] for index in textures)
    return dict(saved=saved, effective=effective, components=operations, texture_slots=resolved,
                textures=sorted(textures), payloads=sorted(payloads))


def reachable_plans(manifest):
    groups = manifest['option_groups']
    for values in itertools.product(*([choice['id'] for choice in group['choices']] for group in groups)):
        options = dict(zip((group['id'] for group in groups), values))
        try:
            yield selection_plan(manifest, options)
        except ValueError as error:
            if str(error) != 'Unreachable option combination':
                raise


def validate_manifest(m, payload_count, minor=None):
    """Structural and symbolic checks under the limits of header `minor` (None: newest)."""
    limit = limits(minor)
    v1.require(m['schema'] == 1, 'Unsupported manifest schema')
    v1.require('appearances' not in m and 'default_appearance_id' not in m,
               'BEM 1.1 must not enumerate fixed appearances')
    v1.require(payload_count <= limit['directory'], 'Payload directory exceeds limit')
    v1.identity(m['package_id'])
    for key in ('name', 'author', 'version'):
        _name(m[key], key)
    caps = m['required_capabilities']
    allowed = set(v1.SUPPORTED_CAPABILITIES) | {'composable-options', 'keep-material-textures'}
    if limit['minor'] >= 2:
        allowed |= set(V12_CAPABILITIES)
    v1.require(isinstance(caps, list) and 'composable-options' in caps and
               'fixed-appearances' not in caps and len(caps) == len(set(caps)) and set(caps) <= allowed,
               'Invalid BEM 1.1/1.2 capabilities')
    target = m['target']
    for key in ('character_id', 'profile_id', 'revision', 'world_resource', 'ui_resource'):
        v1.identity(target[key])
    v1.require(target['platform'] == 'windows-x64' and target['world_resource'] != target['ui_resource'],
               'Invalid target platform/resources')
    targets = target['components']
    v1.require(isinstance(targets, list) and 0 < len(targets) <= 64, 'Invalid target components')
    names = set()
    for index, component in enumerate(targets):
        v1.require(component['id'] == index and component['original_index_count'] > 0 and
                   component['original_index_count'] % 3 == 0, 'Invalid target identity')
        _name(component['mesh_name'], 'mesh name')
        v1.require(component['mesh_name'] not in names, 'Duplicate mesh identity')
        names.add(component['mesh_name'])
        v1.require(len(component['bone_names']) <= 65536 and len(component['materials']) <= 256,
                   'Target donor table exceeds limit')
        seen_aliases = set()
        for alias in component.get('bone_name_aliases', []):
            # The canonical name stays in bone_names; an alias only lets the named
            # resource's renderer carry a different name for the same palette bone.
            v1.require(isinstance(alias, dict) and set(alias) == {'index', 'resource', 'name'} and
                       type(alias['index']) is int and 0 <= alias['index'] < len(component['bone_names']) and
                       alias['resource'] in BONE_ALIAS_RESOURCES and
                       (alias['index'], alias['resource']) not in seen_aliases,
                       'Invalid bone name alias')
            _name(alias['name'], 'bone name alias')
            v1.require(alias['name'] != component['bone_names'][alias['index']],
                       'Bone name alias repeats the canonical name')
            seen_aliases.add((alias['index'], alias['resource']))
    has_aliases = any(component.get('bone_name_aliases') for component in targets)
    v1.require(('resource-bone-aliases' in caps) == has_aliases, 'Bone name alias capability mismatch')

    option_groups = m['option_groups']
    v1.require(isinstance(option_groups, list) and 0 < len(option_groups) <= MAX_GROUPS,
               'Invalid option groups')
    groups, earlier = {}, set()
    for group in option_groups:
        gid = v1.identity(group['id'])
        v1.require(gid not in groups, 'Duplicate option group')
        _name(group['name'], 'option group name')
        choices = group['choices']
        v1.require(isinstance(choices, list) and 0 < len(choices) <= limit['choices'],
                   'Invalid group choices')
        ids = set()
        for choice in choices:
            cid = v1.identity(choice['id'])
            v1.require(cid not in ids, 'Duplicate choice ID')
            ids.add(cid)
            _name(choice['name'], 'choice name')
        v1.require(group['default'] in ids, 'Group default choice missing')
        groups[gid] = ids
        _condition(group.get('available_when', True), groups, available_before=earlier)
        earlier.add(gid)
    for rule in m.get('selection_constraints', []):
        _condition(rule, groups)

    meshes, textures = m['meshes'], m['textures']
    v1.require(isinstance(meshes, list) and len(meshes) <= 4096 and
               isinstance(textures, list) and len(textures) <= 4096,
               'Resource table exceeds limit')
    def payload(ref):
        v1.require(type(ref) is int and 0 <= ref < payload_count, 'Invalid payload reference')
    counted = 0
    slots = m.get('texture_slots', [])
    v1.require(isinstance(slots, list) and len(slots) <= MAX_TEXTURE_SLOTS, 'Invalid texture slots')
    v1.require(('texture-slots' in caps) == bool(slots), 'Texture slot capability mismatch')
    slot_names = {}
    for slot in slots:
        v1.require(isinstance(slot, dict) and set(slot) == {'id', 'candidates'}, 'Invalid texture slot')
        sid = v1.identity(slot['id'])
        v1.require(sid not in slot_names, 'Duplicate texture slot')
        candidates = slot['candidates']
        v1.require(isinstance(candidates, list) and 0 < len(candidates) <= limit['rules'],
                   'Invalid texture slot candidates')
        names = set()
        for candidate in candidates:
            counted += 1
            v1.require(isinstance(candidate, dict) and set(candidate) <= {'when', 'texture'} and
                       'texture' in candidate, 'Invalid texture slot candidate')
            texture = candidate['texture']
            if texture is not None:
                v1.require(type(texture) is int and 0 <= texture < len(textures),
                           'Invalid texture slot candidate')
                names.add(textures[texture]['original_name'])
            _condition(candidate.get('when', True), groups)
        # A slot stands for one native texture property: every choice replaces
        # the same original texture, or keeps it (null).
        v1.require(len(names) == 1, 'Texture slot candidates must replace one original texture')
        slot_names[sid] = names.pop()

    def texture_list(refs, label):
        v1.require(isinstance(refs, list), f'Invalid {label} texture')
        fixed, named = _texture_refs(refs, slot_names, label)
        v1.require(all(0 <= index < len(textures) for index in fixed), f'Invalid {label} texture')
        if named:
            originals = [textures[index]['original_name'] for index in fixed] + [slot_names[s] for s in named]
            v1.require(len(originals) == len(set(originals)), f'{label} textures replace one original twice')
    for mesh in meshes:
        v1.require(0 < mesh['vertex_count'] <= 1048576 and mesh['index_size'] in (2, 4),
                   'Invalid mesh counts/index size')
        streams = mesh['streams']
        v1.require(len(streams) == 3, 'Three streams required')
        for stream in streams:
            payload(stream['payload'])
            v1.require(0 < stream['stride'] <= 64, 'Invalid stream stride')
        attrs, offsets, seen = mesh['attributes'], [0, 0, 0], set()
        sizes = [4, 2, 1, 1, 2, 2, 1, 1, 2, 2, 4, 4]
        v1.require(0 < len(attrs) <= 16, 'Invalid attributes')
        for attr in attrs:
            v1.require(len(attr) == 5, 'Invalid attribute')
            sem, fmt, dim, stream, off = attr
            v1.require(0 <= sem <= 13 and sem not in seen and 0 <= fmt < len(sizes)
                       and 1 <= dim <= 4 and 0 <= stream < 3 and off == offsets[stream],
                       'Invalid attribute declaration')
            seen.add(sem)
            offsets[stream] += sizes[fmt] * dim
        skin_strides = (4, 12, 32) if limit['minor'] >= 2 else (4, 12)
        v1.require(offsets == [stream['stride'] for stream in streams] and
                   attrs[-1][:2] == [13, 10 if offsets[2] == 32 else 6] and attrs[-1][2:4] == [4, 2] and
                   offsets[2] in skin_strides, 'Unsupported native declaration')
        v1.require([attr for attr in attrs if attr[3] == 2] == SKIN_LAYOUTS[offsets[2]],
                   'Unsupported skin declaration')
        v1.require(0 < len(mesh['bones']) <= 256 and 0 < len(mesh['draws']) <= limit['rules'],
                   'Palette/draw candidate limit exceeded')
        for bone in mesh['bones']:
            v1.require(targets[bone['component']]['bone_names'][bone['index']] == bone['name'],
                       'Bone identity differs from target')
        for draw in mesh['draws']:
            counted += 1
            payload(draw['indices'])
            v1.require(0 < draw['count'] <= 16777216 and draw['count'] % 3 == 0,
                       'Invalid draw index count')
            v1.require(targets[draw['material_component']]['materials'][draw['material_slot']] == draw['material_name'],
                       'Material identity differs from target')
            texture_list(draw['textures'], 'draw')
            _condition(draw.get('when', True), groups)
    for texture in textures:
        payload(texture['payload'])
        v1.require(texture['format'] in (4, 10, 12, 25, 26, 27, 48, 49, 50, 63) and
                   (texture['format'] != 63 or texture['srgb'] is False) and type(texture['srgb']) is bool,
                   'Unsupported texture format')
        _name(texture['original_name'], 'original texture name')

    rules = m['component_rules']
    v1.require(isinstance(rules, list) and len(rules) == len(targets), 'Incomplete component rules')
    has_keep_textures = False
    for target_id, rule in enumerate(rules):
        v1.require(rule['target'] == target_id and 0 < len(rule['candidates']) <= limit['rules'],
                   'Invalid component rule')
        for candidate in rule['candidates']:
            counted += 1
            operation = candidate['operation']
            v1.require(operation in ('keep', 'hide', 'replace'), 'Invalid component operation')
            v1.require(operation == 'keep' or 'material_overrides' not in candidate,
                       'Only keep may override original materials')
            if operation == 'replace':
                v1.require(type(candidate['mesh']) is int and 0 <= candidate['mesh'] < len(meshes),
                           'Missing mesh')
            if 'material_overrides' in candidate:
                overrides = candidate['material_overrides']
                v1.require(isinstance(overrides, list) and 0 < len(overrides) <= 256,
                           'Invalid keep material overrides')
                has_keep_textures = True
                seen_slots = set()
                for override in overrides:
                    slot = override['material_slot']
                    v1.require(type(slot) is int and 0 <= slot < len(targets[target_id]['materials'])
                               and slot not in seen_slots and
                               targets[target_id]['materials'][slot] == override['material_name'],
                               'Keep material identity differs from target')
                    seen_slots.add(slot)
                    refs = override['textures']
                    v1.require(isinstance(refs, list) and 0 < len(refs) <= limit['textures'],
                               'Invalid keep material textures')
                    texture_list(refs, 'keep material')
            _condition(candidate.get('when', True), groups)
    v1.require(counted <= limit['rules'], f'More than {limit["rules"]} candidate rules/draws')
    v1.require(('keep-material-textures' in caps) == has_keep_textures,
               'Keep material override capability mismatch')
    _symbolic_check(m, minor=minor)


def check_geometry(m, payloads, minor=None):
    """Creator-side byte and per-selected-state budget checks.

    The summary's `required_minor` is the header minor a writer must use.
    """
    limit = limits(minor)
    for mesh in m['meshes']:
        for stream in mesh['streams']:
            v1.require(len(payloads[stream['payload']]) == mesh['vertex_count'] * stream['stride'],
                       'Vertex stream length mismatch')
        index_size = mesh['index_size']
        for draw in mesh['draws']:
            raw = payloads[draw['indices']]
            v1.require(len(raw) == draw['count'] * index_size, 'Draw index payload length mismatch')
            v1.require(all(index[0] < mesh['vertex_count'] for index in
                           struct.iter_unpack('<H' if index_size == 2 else '<I', raw)),
                       'Index outside vertices')
        skin = mesh['streams'][2]
        stride = skin['stride']
        buf = payloads[skin['payload']]
        if stride == 32:
            v1.require(all(b < len(mesh['bones']) for offset in range(16, len(buf), stride)
                           for b in struct.unpack_from('<4I', buf, offset)), 'Bone index outside palette')
            v1.require(all(abs(sum(struct.unpack_from('<4f', buf, offset)) - 1) <= 0.01
                           for offset in range(0, len(buf), stride)), 'Skin weights must sum to 1')
        else:
            v1.require(all(b < len(mesh['bones']) for offset in range(stride-4, len(buf), stride)
                           for b in buf[offset:offset+4]), 'Bone index outside palette')
        if stride == 12:
            v1.require(all(abs(sum(struct.unpack_from('<4H', buf, offset))-65535) <= 655
                           for offset in range(0, len(buf), stride)), 'Skin weights must sum to 1')
    for texture in m['textures']:
        width, height, mips = texture['width'], texture['height'], texture['mips']
        pixel = {4: 4, 63: 1}.get(texture['format'], 0)
        astc = texture['format'] in (48, 49, 50)
        v1.require(0 < width <= 32768 and 0 < height <= 32768 and
                   (pixel or astc or (width % 4 == 0 and height % 4 == 0)) and 0 < mips <= 16,
                   'Invalid texture dimensions')
        size = texture_bytes(texture)
        v1.require(size <= limit['texture_bytes'],
                   f'Texture exceeds {limit["texture_bytes"] // 1024**2} MiB')
        v1.require(len(payloads[texture['payload']]) == size, 'Texture mip payload mismatch')
    summary = _symbolic_check(m, payloads=payloads, minor=minor)
    summary['required_minor'] = required_minor(m, summary, len(payloads))
    return summary


def read_selected_payloads(path, options=None, on_payload=None):
    """Reference lazy reader for A→B→A and exact payload-access tests."""
    m, _ = v1.read_package(path, decode=False)
    v1.require('option_groups' in m, 'Expected BEM 1.1 package')
    plan = selection_plan(m, options)
    with Path(path).open('rb') as source:
        header = v1.HEADER.unpack(source.read(v1.HEADER.size))
        source.seek(v1.HEADER.size + header[5])
        directory = list(v1.ENTRY.iter_unpack(source.read(header[6] * v1.ENTRY.size)))
        payloads = {}
        for index in plan['payloads']:
            codec, _, offset, stored, decoded = directory[index]
            source.seek(offset)
            raw = source.read(stored)
            v1.require(len(raw) == stored, 'Truncated selected payload')
            if codec:
                v1.require(zstd.frame_content_size(raw) == decoded, 'Zstd content size mismatch')
                raw = zstd.ZstdDecompressor().decompress(raw, max_output_size=decoded,
                                                        allow_extra_data=False)
            v1.require(len(raw) == decoded, 'Decoded selected payload length mismatch')
            payloads[index] = raw
            if on_payload is not None:
                on_payload(index)
    return m, plan, payloads
