"""Classify declarations, not filenames or author labels; never claim conversion readiness."""
import re
from efmi_lod_source import ENTRY

COMPONENT = re.compile(r'TextureOverride_(?:EntryPoint_)?Component\d+(?:_LOD\d+)?$')


def entry_family(sec):
    component = any(COMPONENT.fullmatch(s) for s in sec)
    lod = any(ENTRY.fullmatch(s) for s in sec)
    if component and lod: return 'mixed'
    if component: return 'component-n'
    if lod: return 'hash-lod'
    return None


def classify(family, documents):
    """documents are active source INIs, including companion definitions.

    A custom shader definition/call requires review even if it may be unused;
    resolving reachability for arbitrary INI programs is outside this classifier.
    Engine-provided EFMI merged-skeleton shaders are not author custom shaders.
    """
    features, reasons, evidence = set(), [], []
    for filename, sec in documents:
        text = '\n'.join(line.split(';', 1)[0].strip() for body in sec.values() for line in body.splitlines())
        shaders = [s for s in sec if s.lower().startswith('customshader')]
        calls = re.findall(r'^\s*run\s*=\s*(CustomShader\S+)', text, re.M | re.I)
        custom_calls = [c for c in calls if not re.match(r'CustomShader\\EFMIv1\\', c, re.I)]
        if shaders or custom_calls:
            features.add('custom_shader')
            evidence.append(dict(file=filename, sections=shaders[:16], calls=custom_calls[:16]))
        if re.search(r'\\RabbitFX\\', text, re.I): features.add('external_material_framework')
        if 'MergedSkeleton' in text or any('MergedSkeleton' in s for s in sec): features.add('merged_skeleton')
        if re.search(r'ElementFormat\(BLENDINDICES,\s*0\)\s*=\s*R16G16B16A16_UINT', text): features.add('bone_indices_u16')
        if re.search(r'^\s*ps-t\d+\s*=', text, re.M | re.I): features.add('per_draw_texture_slots')
        if re.search(r'^\s*global\s+persist\s+\$', text, re.M): features.add('static_appearance_switches')
        if re.search(r'^\s*run\s*=\s*\S*ShapeKeys?\S*', text, re.M | re.I): features.add('shape_keys')
    if 'custom_shader' in features:
        reasons.append('包含作者自定义 Shader 定义或调用，不能按标准 EFMI 规则自动转换；需要专门审阅。')
    if 'external_material_framework' in features:
        reasons.append('依赖外部材质框架，不能自动假定与游戏原生材质等价。')
    if 'shape_keys' in features:
        reasons.append('包含形态键处理，超出当前 BEM 固定外观自动转换范围。')
    if family == 'mixed': reasons.append('同一入口混合 ComponentN 与 Hash/LOD 声明，需要审阅。')
    if reasons:
        status, label = 'manual_only', '不能自动转换：需要专门适配'
    elif family == 'component-n' and not features.intersection({'per_draw_texture_slots','merged_skeleton','bone_indices_u16'}):
        status, label = 'standard_candidate', '标准 ComponentN：自动转换优先支持类别'
    elif family in ('component-n', 'hash-lod'):
        status, label = 'requires_mapping', '可解析：需核对骨架与材质映射'
        if 'per_draw_texture_slots' in features:
            reasons.append('源 INI 按绘制段直接设置 ps-t* 贴图槽；需要对应到游戏原生材质属性，当前尚未自动建立此类映射。')
        if features.intersection({'merged_skeleton','bone_indices_u16'}):
            reasons.append('来源使用合并骨架或 16 位骨骼索引，需要核对骨骼分组和原生骨骼顺序。')
        if not reasons:reasons.append('需要可复用的资源/骨骼对应及材质槽语义；存在歧义时不得自动猜测。')
    else:
        status, label = 'unrecognized', '未识别来源格式'
    return dict(status=status, label=label, source_family=family, features=sorted(features),
                reasons=reasons, evidence=evidence, conversion_ready=False,
                note='格式识别不等于可立即转换；还须匹配角色资料并通过默认外观结构校验。')
