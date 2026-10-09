"""Move the reviewed documentation set, rewrite local references, and check it offline.

No build, game access, resource refresh, deployment, or edits outside the declared
documentation outputs are performed. The inventory is read once, not discovered
by walking an external drive. Existing tracked consumers are read only.
"""
from __future__ import annotations

import argparse
import csv
import json
import os
import re
import subprocess
from collections import Counter
from pathlib import Path, PurePosixPath, PureWindowsPath
from urllib.parse import unquote

from workspace_config import load_workspace, write_if_changed


BATCH = "workspace-reorganization-20261005"
REWRITE_FILE = f"artifacts/{BATCH}/doc_reference_rewrites.json"
PROTECTED = {
    "docs/workspace/WORKSPACE_POLICY.md",
    "docs/workspace/TESTING.md",
    "docs/workspace/CONFIGURATION.md",
}
OUTPUTS = {"config/documents.json", "config/document-catalog.json", REWRITE_FILE}
MODULES = {
    "custom_model": "自定义模型与 BEM",
    "camera": "相机、第一人称与 MMD",
    "actions": "动作与特殊冲刺",
    "combat_stats": "战斗数据",
    "host": "Host 与第三方模块",
    "voice": "语音",
    "music": "音乐输入与 OmniMix",
    "android": "Android 平台",
    "ui": "界面与显示",
    "model": "登录展示模型",
    "web": "网页与后端",
    "workspace": "工作区与发布记录",
}
CURRENT = {
    "custom_model": """
        BEM_CREATOR_GUIDE.md BEM_CREATOR_GUIDE.en.md
        BEM_FORMAT_SPEC.md BEM_FORMAT_SPEC.en.md
        BEM_RUNTIME_COMPATIBILITY.md BEM_RUNTIME_COMPATIBILITY.en.md
        BEM_SOURCE_MOD_CONVERSION.md BEM_SOURCE_MOD_CONVERSION.en.md
        CUSTOM_MODEL_KNOWN_ISSUES.md CUSTOM_MODEL_NATIVE_PARSER_20260917.md
    """,
    "camera": "FIRST_PERSON_PROFILES_20261003.md",
    "actions": "SPECIAL_DASH_CONTINUOUS_ANIMATION.md",
    "combat_stats": "BUFF_TABLE_EXPORT.md COMBAT_RUNTIME_CONTRACTS.md",
    "host": "GAME_INTERFACES.md THIRD_PARTY_MODULE_CREATOR_GUIDE.md CREATOR_MODULE_API_DESIGN_20261002.md",
    "voice": "VOICE_CUSTOM_LANGUAGE_SYSTEM.md",
    "music": "MUSIC_INTEGRATION_RESEARCH.md OMNIMIX_INTEGRATION_HANDOFF.md",
    "ui": "DISPLAY_PIPELINE.md MOBILE_UI_REVERSING.md",
    "web": "WEB_BACKEND_ARCHITECTURE.md",
}
# Each row is reviewed by purpose, not inferred from its date or software version.
# The explicit 1.5.3 assignments below have document-local client evidence.
RESEARCH = """
android runtime-rebuild unknown ANDROID_REBUILD_20260927.md
android runtime-refactor unknown ANDROID_REFACTOR_PLAN_20260920.md
android ui-import unknown ANDROID_UI_IMPORT_20261001.md
android resource-sync 1.5.3 ANDROID_RESOURCE_SYNC_20261001.md
custom_model android-reuse unknown ANDROID_CUSTOM_MODEL_REUSE_AUDIT_20260920.md
custom_model android-mesh unknown ANDROID_CUSTOM_MODEL_STATIC_AUDIT_20260920.md ANDROID_MESH_WRITE_METHOD_ENTRYPOINTS_20260920.md
custom_model android-mesh 1.5.3 ANDROID_MESH_SUBMISSION_20260921.md
custom_model android-lighting-lod unknown ANDROID_LIGHTING_SHADOW_LOD_20260921.md
ui android-overlay unknown ANDROID_OVERLAY_SETTINGS_20261003.md
ui android-input-layout unknown ANDROID_PC_UI_REVIEW_20261001.md
ui sponsor unknown SPONSOR_UI_20261001.md
ui webview2-toy unknown webview2-toy-probe.md
camera android-mmd unknown ANDROID_CAMERA_MMD_20261001.md
camera first-person-geometry unknown BEM_HAIR_BONE_ANALYSIS_20261002.md BEM_HAIR_SHADOW_IMPLEMENTATION_20261002.md
camera eiem-port unknown CAMERA_EIEM_PORT_PLAN_20260926.md
camera first-person-cinemachine unknown CAMERA_FIRST_PERSON_CINEMACHINE_20260915.md
camera fov-follow unknown CAMERA_FOV_FOLLOW_IMPLEMENTATION_20261002.md CAMERA_HAIR_FOV_FOLLOW_DESIGN_20261002.md
camera relink-port unknown CAMERA_RELINK_PORTING_ANALYSIS_20260926.md
camera first-person-facing unknown FIRST_PERSON_ACTION_FACING_20261003.md
camera first-person-lifecycle unknown FIRST_PERSON_FIX_20261002.md FIRST_PERSON_LIFECYCLE_DIAGNOSIS_20261002.md
camera first-person-accessories unknown FIRST_PERSON_HEAD_ACCESSORIES_20261003.md
camera first-person-typhoea-campus unknown FIRST_PERSON_TYPHOEA_CAMPUS_DIAGNOSIS_20261003.md
camera pc-vmd-stages unknown PC_CAMERA_PROGRESS_20260927.md PC_CHARACTER_PREVIEW_20260927.md
camera renodx-hiding unknown RENODX_FIRST_PERSON_HIDING_RESEARCH_20261003.md
camera windows-input unknown WINDOWS_CAMERA_INPUT_FIX_20261001.md
actions eiem-playback unknown EIEM_PLAYBACK_RESEARCH.md
actions liino-dash unknown LIINO_CLEAN_DASH.md LIINO_DASH_VISUAL_FIX.md LIINO_LOW_GLIDE.md
host hook-update unknown GAME_UPDATE_20260903_HOOK_DIFF.md
voice duration unknown VOICE_DURATION_INVESTIGATION_20260903.md
model purrche-title unknown TITLE_MODEL_PURRCHE_FIX_20261002.md
custom_model architecture unknown CUSTOM_MODEL_ARCHITECTURE.md
custom_model mesh-skinning unknown CUSTOM_MODEL_BONES_PER_VERTEX_ANDROID_20260920.md CUSTOM_MODEL_MESH_NATIVE_FINDINGS_20260914.md
custom_model capability-v25 unknown CUSTOM_MODEL_CAPABILITY_COMPATIBILITY_20260918.md CUSTOM_MODEL_V25_BINDING_IMPLEMENTATION_20260917.md
custom_model early-delivery unknown CUSTOM_MODEL_EARLY_DELIVERY_CONSTRAINTS_20260914.md
custom_model endmin-casualwear unknown CUSTOM_MODEL_EFMI_SAMPLE_ENDMIN_CASUALWEAR.md CUSTOM_MODEL_POC1_C9_PLAN.md CUSTOM_MODEL_RUNTIME_VALIDATION_ENDMIN_20260910.md
custom_model gilberta-outfits unknown CUSTOM_MODEL_GILBERTA_OUTFIT_B_20260918.md CUSTOM_MODEL_GILBERTA_REASSESSMENT_20260917.md CUSTOM_MODEL_GILBERTA_VALIDATION_20260917.md
custom_model hash-lod-conversion unknown CUSTOM_MODEL_HASH_LOD_CONVERTER_20260917.md
custom_model lod-bones-vfx unknown CUSTOM_MODEL_LOD_LOCK_AND_VFX_FINDINGS_20260914.md CUSTOM_MODEL_MULTI_LOD_BONE_ELEVATION_20260915.md
custom_model mod-7b260 unknown CUSTOM_MODEL_MOD_7B260_INSPECTION_20260915.md
custom_model replacement-window unknown CUSTOM_MODEL_MULTI_MOD_REPLACEMENT_WINDOW_20260915.md
custom_model mesh-raw-poc unknown CUSTOM_MODEL_POC21_RAW_CHANNEL.md CUSTOM_MODEL_POC22_RAW_STREAM.md
custom_model purrche-catalog unknown CUSTOM_MODEL_PURRCHE_20261001.md
custom_model resource-delivery unknown CUSTOM_MODEL_REBUILD_BEHAVIOR_COMPARISON_20260916.md CUSTOM_MODEL_RESOURCE_AB_VALIDATION_20260916.md CUSTOM_MODEL_RESOURCE_REBUILD_PROGRESS_20260916.md CUSTOM_MODEL_RESOURCE_RELEASE_CALL_CHAIN_20260916.md
custom_model runtime-probes unknown CUSTOM_MODEL_RUNTIME_PROBE.md CUSTOM_MODEL_RUNTIME_PROBE_20260917.md CUSTOM_MODEL_RUNTIME_SWEEP_20260919.md
custom_model zhuangfangyi unknown CUSTOM_MODEL_ZHUANGFANGYI_VALIDATION_20260917.md
custom_model matching-identity unknown GENERIC_MODEL_MATCHING_DESIGN_20261003.md WORLD_MODEL_BINDING_20261003.md
custom_model hot-switch unknown HOT_SWITCH_LEGACY_PATH_RESEARCH_20261003.md MODEL_HOT_SWITCH_REVIEW_20261001.md
custom_model model-camera-integration unknown MODEL_CAMERA_IMPLEMENTATION_20261003.md PARALLEL_MODEL_CAMERA_REVIEW_20261003.md MODEL_MATCHING_LOADING_DECISIONS_20261003.md
custom_model loading-memory unknown MODEL_MEMORY_REVIEW_20261001.md MODEL_UPLOAD_PEAK_REVIEW_20261003.md NATIVE_MODEL_LOADING_RESEARCH_20261003.md TEAM_MODEL_LOADING_DESIGN_20261003.md
custom_model ui-model-cache unknown UI_MODEL_CACHE_DESIGN_20261003.md
"""
ARCHIVE_RESEARCH = """
android bem-open-with ANDROID_BEM_OPEN_WITH_20260927.md
custom_model bem-body-sliders BEM_1_3_BODY_SLIDER_DESIGN_20261002.md EFMI_BODY_SLIDER_RESEARCH_20261002.md
custom_model bem-character-catalog BEM_CHARACTER_CATALOG_20260919.md BEM_COMPONENTN_AUTOMATION_20260919.md BEM_EFMI_IDENTITIES_20260920.md
custom_model bem-workflow BEM_CREATOR_WORKFLOW_REVIEW_20261001.md BEM_TOOLCHAIN_20260919.md
custom_model bem-textures BEM_DDS_READING_20260920.md BEM_PER_DRAW_COMPATIBILITY_20260920.md BEM_RABBITFX_COMPATIBILITY_20260920.md
custom_model bem-matching BEM_MATCHING_REVIEW_20261001.md
custom_model bem-management BEM_MODEL_MANAGEMENT_20261003.md
custom_model model-replacement CHARACTER_MODEL_REPLACEMENT.md
"""
FORMAT_HISTORY = {
    "bem-1.0": "BEM_V1_DESIGN_DRAFT.md BEM_V1_SPEC.md BEM_CREATOR_GUIDE_DRAFT.md",
    "bem-1.1": "BEM_V1_1_DESIGN.md BEM_V1_1_SPEC.md",
    "bem-1.2": "BEM_V1_2_SPEC.md",
    "bem-1.3": "BEM_V1_3_SPEC.md BEM_V1_3_CREATOR_GUIDE.md",
}
TYPE_STATUS = {
    "现行规范": ("specification", "current"),
    "维护接口": ("maintenance_reference", "reference"),
    "证据与来源": ("source_evidence", "evidence"),
    "实施记录": ("implementation_record", "implemented_record"),
    "历史过程": ("historical_record", "historical"),
    "已替代历史": ("historical_record", "partially_superseded"),
    "研究设计": ("research_design", "proposal"),
    "发布入口": ("release_record", "release_record"),
    "BEM既有归档": ("archived_record", "archived"),
    "分发参考": ("distributed_reference", "distributed_copy"),
    "工作约束": ("policy", "policy"),
    "生成配套报告": ("generated_report", "generated"),
}
STATUS_LABEL = {
    "current": "现行规范", "reference": "维护参考", "evidence": "来源/证据",
    "implemented_record": "阶段实施记录", "historical": "历史过程",
    "partially_superseded": "部分结论已替代", "proposal": "研究/提案",
    "release_record": "发布记录", "archived": "既有归档",
    "distributed_copy": "分发参考副本", "policy": "工作约束",
    "generated": "生成报告", "navigation": "导航",
}
LINK = re.compile(r"(?P<prefix>!?\[[^\]\n]*\]\(\s*<?)(?P<target>[^\s<>\)]+)")
REF_LINK = re.compile(r"(?m)(?P<prefix>^\s*\[[^\]\n]+\]:\s*<?)(?P<target>[^\s<>]+)")
SCHEME = re.compile(r"^[A-Za-z][A-Za-z0-9+.-]*:")
URL = re.compile(r"(?:https?|file)://[^\s<>`)\]]+")
LINK_REPAIRS = {
    "native/modules/custom_model/module_poc2_part_02.inc": "research/custom-model/legacy-runtime/modules/custom_model/module_poc2_part_02.inc",
    "native/modules/custom_model/module_poc2_part_03.inc": "research/custom-model/legacy-runtime/modules/custom_model/module_poc2_part_03.inc",
}


def no_date(filename: str) -> str:
    return re.sub(r"_20\d{6}(?=\.md$)", "", filename)


def move_plan(current_version: str) -> dict[str, str]:
    result = {}
    for module, names in CURRENT.items():
        for name in names.split():
            result[f"docs/{name}"] = f"docs/{module}/{no_date(name)}"
    for row in RESEARCH.strip().splitlines():
        module, topic, version, *names = row.split()
        version = current_version if version == "unknown" else version
        for name in names:
            target_name = no_date(name)
            if name == "CUSTOM_MODEL_RUNTIME_PROBE_20260917.md":
                target_name = "CUSTOM_MODEL_RUNTIME_PROBE_ZHUANGFANGYI.md"
            result[f"docs/{name}"] = f"docs/{module}/research/{version}/{topic}/{target_name}"
    for row in ARCHIVE_RESEARCH.strip().splitlines():
        module, topic, *names = row.split()
        for name in names:
            result[f"docs/archive/bem/{name}"] = f"docs/{module}/research/{current_version}/{topic}/{no_date(name)}"
    for version, names in FORMAT_HISTORY.items():
        for name in names.split():
            result[f"docs/archive/bem/{name}"] = f"docs/custom_model/history/{version}/{name}"
    result["docs/archive/bem/README.md"] = "docs/custom_model/history/INDEX.md"
    result["docs/webview2-toy-probe.md"] = "docs/ui/webview2-toy-probe.md"
    # Format proposals, upstream tool studies and synthetic/tool-only checks
    # are independent of the game's client version.
    generic_history = {
        "BEM_1_3_BODY_SLIDER_DESIGN_20261002.md": "bem-1.3",
        "EFMI_BODY_SLIDER_RESEARCH_20261002.md": "tooling/body-sliders",
        "BEM_CREATOR_WORKFLOW_REVIEW_20261001.md": "tooling/creator-workflow",
        "BEM_TOOLCHAIN_20260919.md": "tooling/toolchain",
        "BEM_DDS_READING_20260920.md": "tooling/dds",
        "BEM_PER_DRAW_COMPATIBILITY_20260920.md": "tooling/efmi-compatibility",
        "BEM_RABBITFX_COMPATIBILITY_20260920.md": "tooling/efmi-compatibility",
    }
    for name, topic in generic_history.items():
        result[f"docs/archive/bem/{name}"] = f"docs/custom_model/history/{topic}/{no_date(name)}"
    for version, date in (("3_4_1", "20261001"), ("3_4_2", "20261002"), ("3_5_0", "20261004")):
        result[f"docs/RELEASE_{version}_{date}.md"] = f"docs/workspace/releases/{version.replace('_', '.')}/RELEASE_{version}.md"
    if len(result) != 126 or len(set(result.values())) != len(result):
        raise ValueError("Reviewed plan is incomplete or has a target collision")
    return dict(sorted(result.items()))


MOVES: dict[str, str] = {}
# This stale title is cited by the action overview; the existing paper is the
# EIEM playback study. It is an alias repair, not an additional migrated file.
ALIASES: dict[str, str] = {}
REWRITES: dict[str, str] = {}
BASENAMES: dict[str, str] = {}
OLD_TOKEN = re.compile(r"(?!)")
REPO_TOKEN = re.compile(r"(?!)")


def configure_plan(workspace) -> None:
    global MOVES, ALIASES, REWRITES, BASENAMES, OLD_TOKEN, REPO_TOKEN
    version = workspace.get("game.version")
    if not isinstance(version, str) or version in ("", "unknown") or not re.fullmatch(r"[A-Za-z0-9.+_-]+", version):
        raise ValueError("This reviewed migration requires the configured current game version")
    MOVES = move_plan(version)
    ALIASES = {"docs/AGLINA_EIEM_PLAYBACK_RESEARCH.md": MOVES["docs/EIEM_PLAYBACK_RESEARCH.md"]}
    REWRITES = MOVES | ALIASES
    BASENAMES = {PurePosixPath(old).name: new for old, new in REWRITES.items() if not old.endswith("/README.md")}
    OLD_TOKEN = re.compile(r"(?<![\w/\\.-])(" + "|".join(re.escape(name) for name in sorted(BASENAMES, key=len, reverse=True)) + r")(?![\w.-])")
    REPO_TOKEN = re.compile(r"(?<![\w/\\.-])(" + "|".join(re.escape(old) for old in sorted(REWRITES, key=len, reverse=True)) + r")(?![\w/\\.-])")


def checked(root: Path, relative: str, *, writable: bool = False) -> Path:
    posix, windows = PurePosixPath(relative), PureWindowsPath(relative)
    if posix.is_absolute() or windows.is_absolute() or windows.drive or ".." in posix.parts or "\\" in relative:
        raise ValueError(f"Unsafe repo-relative path: {relative}")
    path = (root / relative).resolve()
    if path == root or root not in path.parents:
        raise ValueError(f"Path escapes the active repository: {relative}")
    if writable:
        resolved_relative = path.relative_to(root).as_posix()
        for candidate in (relative, resolved_relative):
            if candidate in PROTECTED or not (candidate.startswith("docs/") or candidate in OUTPUTS):
                raise ValueError(f"Write outside documentation ownership: {relative}")
    return path


def json_text(value) -> str:
    return json.dumps(value, ensure_ascii=False, indent=2) + "\n"


def tracked(root: Path) -> set[str]:
    data = subprocess.run(["git", "-C", str(root), "ls-files", "-z"], check=True, capture_output=True).stdout
    return set(data.decode("utf-8").rstrip("\0").split("\0"))


def relative_to(document: str, target: str) -> str:
    return os.path.relpath(target, str(PurePosixPath(document).parent)).replace("\\", "/")


def normalize_target(document: str, target: str) -> tuple[str | None, str]:
    target = unquote(target)
    if target.startswith("file:") and "/docs/" in target:
        target = "docs/" + target.split("/docs/", 1)[1]
        document = "ROOT.md"
    elif SCHEME.match(target) or target.startswith(("#", "//")):
        return None, ""
    path = re.split(r"[?#]", target, maxsplit=1)[0]
    suffix = target[len(path):]
    if not path:
        return None, suffix
    if path.startswith("/"):
        normalized = path.lstrip("/")
    else:
        normalized = os.path.normpath(str(PurePosixPath(document).parent / path)).replace("\\", "/")
    return normalized, suffix


def resolve_target(document: str, target: str, known: set[str]) -> tuple[str | None, str]:
    normalized, suffix = normalize_target(document, target)
    if normalized is None:
        return None, suffix
    if normalized in known or normalized in REWRITES:
        return normalized, suffix
    raw = re.split(r"[?#]", unquote(target), maxsplit=1)[0]
    # Repair old archive links incorrectly written as ../tools/... . Only a
    # known tracked root path or a reviewed document alias can override them.
    root_candidate = re.sub(r"^(?:\.\./)+", "", raw)
    if root_candidate in known or root_candidate in REWRITES:
        return root_candidate, suffix
    if "/" not in raw and raw in BASENAMES:
        candidates = [old for old in REWRITES if PurePosixPath(old).name == raw]
        if len(candidates) == 1:
            return candidates[0], suffix
    return normalized, suffix


def replace_repo_references(content: str) -> str:
    # Tagged GitHub source links keep the original path of that immutable tag.
    parts = []
    end = 0
    for match in URL.finditer(content):
        parts.append(REPO_TOKEN.sub(lambda m: REWRITES[m[1]], content[end:match.start()]))
        parts.append(match[0])
        end = match.end()
    parts.append(REPO_TOKEN.sub(lambda m: REWRITES[m[1]], content[end:]))
    return "".join(parts)


def rewrite(content: str, old: str, new: str, known: set[str]) -> str:
    def link(match):
        target, suffix = resolve_target(old, match["target"], known)
        if target is None:
            return match[0]
        target = REWRITES.get(target, target)
        if target in LINK_REPAIRS and LINK_REPAIRS[target] in known:
            target = LINK_REPAIRS[target]
        return match["prefix"] + relative_to(new, target) + suffix

    content = REF_LINK.sub(link, LINK.sub(link, content))
    content = replace_repo_references(content)
    # Bare paper names (including backtick citations) follow their document's
    # new location. Path-qualified targets were already handled above.
    return OLD_TOKEN.sub(lambda m: relative_to(new, BASENAMES[m[1]]), content)


def doc_title(content: str, fallback: str) -> tuple[str, int]:
    for line_number, line in enumerate(content.splitlines(), 1):
        if line.startswith("# "):
            return line[2:].strip(), line_number
    return fallback, 1


def module_of(path: str) -> str:
    if path.startswith("docs/") and path.split("/")[1] in MODULES:
        return path.split("/")[1]
    tests = (
        ("custom_model", ("CustomModel", "custom-model", "custom_model", "bem-creator", "astcenc", "bcdec")),
        ("camera", ("/camera/", "android_mmd")),
        ("combat_stats", ("combat", "CombatData")),
        ("actions", ("/actions/",)),
        ("voice", ("/voice/",)),
        ("ui", ("BetterEndfieldNext.UI",)),
        ("web", ("web/",)),
        ("android", ("android/",)),
        ("host", ("HookInline",)),
    )
    for module, markers in tests:
        if any(marker in path for marker in markers):
            return module
    return "workspace"


def platforms(old: str, content: str) -> list[str]:
    if old.startswith("android/") or "/ANDROID_" in old or "_ANDROID_" in old:
        return ["Android"]
    if any(word in old for word in ("/PC_", "/WINDOWS_", "DISPLAY_PIPELINE", "webview2", "MOBILE_UI_REVERSING", "VOICE_DURATION")):
        return ["Windows"]
    if old.startswith("web/") or "WEB_BACKEND" in old:
        return ["Web"]
    if "Android" in content and any(word in content for word in ("Windows", "PC", "双端")):
        return ["Windows", "Android"]
    if "Android" in content or "安卓" in content:
        return ["Android", "Windows"] if "PC" in content else ["Android"]
    if any(word in content for word in ("Windows", "PC", "UnityPlayer.dll", "GameAssembly.dll")):
        return ["Windows"]
    return ["unspecified"]


def make_catalog(workspace, inventory: Path, originals: dict[str, bytes], known: set[str]) -> dict:
    root = workspace.root
    with inventory.open(encoding="utf-8-sig", newline="") as stream:
        rows = list(csv.DictReader(stream))
    missing = set(MOVES) - {row["路径"] for row in rows}
    if missing:
        raise ValueError(f"Inventory is missing reviewed papers: {sorted(missing)}")
    entries = []
    for number, row in enumerate(rows, 2):
        old = row["路径"]
        path = MOVES.get(old, old)
        data = originals.get(old)
        if data is None:
            source = checked(root, path)
            data = source.read_bytes() if source.is_file() else b""
        content = data.decode("utf-8-sig")
        title, title_line = doc_title(content, PurePosixPath(path).name)
        doc_type, status = TYPE_STATUS[row["分组"]]
        if old == "docs/archive/bem/README.md":
            doc_type, status = "navigation", "navigation"
        version = workspace.get("game.version")
        version_evidence = [{"kind": "user_clarification", "date": "2026-10-05",
            "excerpt": "用户明确：九月初更新后一直是 1.5.3；此前资料标 pre-1.5.3；1.5.3 内部热更新以资源快照区分。",
            "config_path": "config/workspace.defaults.json", "config_key": "game.version", "value": version}]
        unbound = ("/history/" in path or "/releases/" in path or
                   PurePosixPath(old).name.startswith(("BEM_CREATOR_GUIDE", "BEM_FORMAT_SPEC", "BEM_SOURCE_MOD_CONVERSION")) or
                   old in ("docs/THIRD_PARTY_MODULE_CREATOR_GUIDE.md", "docs/CREATOR_MODULE_API_DESIGN_20261002.md",
                           "docs/OMNIMIX_INTEGRATION_HANDOFF.md", "docs/WEB_BACKEND_ARCHITECTURE.md", "docs/DISPLAY_PIPELINE.md", "docs/webview2-toy-probe.md") or
                   row["分组"] in ("分发参考", "工作约束") or
                   old.startswith(("tools/HookInlineScan/", "web/", "ui/", "native/shared/third_party/")) or
                   "/tests/" in old)
        if unbound:
            version, version_evidence = "not_applicable", []
        comparison_evidence = []
        for line, text in enumerate(content.splitlines(), 1):
            if old in ("docs/BUFF_TABLE_EXPORT.md", "docs/COMBAT_RUNTIME_CONTRACTS.md") and "游戏版本 1.4.4" in text:
                version_evidence.append({"kind": "original_version_claim", "path": path, "line": line, "excerpt": text,
                    "note": "原文的 1.4.4 标记保留；本次按用户对九月初后客户端的明确说明归类，不据此改写研究结论。"})
            if old in ("docs/ANDROID_MESH_SUBMISSION_20260921.md", "docs/ANDROID_RESOURCE_SYNC_20261001.md") and ("游戏 Android 1.5.3" in text or "versionName 1.5.3" in text):
                version = "1.5.3"
                version_evidence.append({"path": path, "line": line, "excerpt": text})
            if re.search(r"GameAssembly[-_. ]*old|OLD\(2026|更新前.*dump|2026-08-03 dump", text, re.I):
                comparison_evidence.append({"path": path, "line": line, "excerpt": text,
                    "game_version": "pre-1.5.3", "note": "旧客户端资料，准确旧版本号未由用户确认。"})
        evidence = [{"kind": "inventory", "report": inventory.as_posix(), "row": number,
                     "excerpt": replace_repo_references(row["正文依据"])},
                    {"kind": "document", "path": path, "line": title_line, "excerpt": title}]
        entry = {
            "module": module_of(path), "type": doc_type, "status": status,
            "game_version": version, "platform": platforms(old, content),
            "legacy_path": old, "path": path, "title": title, "evidence": evidence,
            "purpose": row["用途"], "status_detail": row["状态"],
            "classification_source": row["分组"],
            "dates": sorted(set(re.findall(r"20\d{2}-\d{2}-\d{2}", content))),
            "game_version_evidence": version_evidence,
            "replacement": replace_repo_references(row["替代或校正"]),
            "ownership": "documentation" if old in MOVES else "external_read_only",
            "reference_evidence": {
                key: replace_repo_references(row[key]) for key in
                ("机器消费及生成证据", "证据定位及代码注释", "文档导航及正文引用")
            },
        }
        if version == "not_applicable":
            entry["game_version_note"] = "通用格式、工具接口/替身验证或软件说明不绑定游戏客户端版本。"
        else:
            entry["game_version_note"] = "按用户明确的九月初后 1.5.3 时间线与当前配置归类；原始来源与验证边界保留，热更新差异由资源快照记录。"
            entry["resource_snapshot_evidence"] = [
                {"path": path, "line": line, "excerpt": text}
                for line, text in enumerate(content.splitlines(), 1)
                if re.search(r"(?:manifest\s+(?:Version|Hash)|manifestHash|input-snapshot|pck-snapshot|/\.sources/|2954fa80-23c1-1579-2b22-4ecfd6d70418)", text, re.I)
            ]
        if comparison_evidence:
            entry["game_versions"] = ["pre-1.5.3", workspace.get("game.version")]
            entry["version_comparison_evidence"] = comparison_evidence
        if old in ("docs/BUFF_TABLE_EXPORT.md", "docs/COMBAT_RUNTIME_CONTRACTS.md"):
            entry["reported_game_versions"] = ["1.4.4"]
            entry["version_claim_note"] = "原文标记与用户最新版本时间线不同；保留原文，只在 catalog 明确归类依据，交 parent 校正说明。"
        if old.startswith("docs/archive/bem/"):
            entry["previously_archived"] = True
        release = re.search(r"/releases/([^/]+)/", path)
        if release:
            entry["software_version"] = release[1]
        format_version = re.search(r"/history/bem-([^/]+)/", path)
        if format_version:
            entry["bem_format_version"] = format_version[1]
        if old.endswith("/CHARACTER_MODEL_REPLACEMENT.md"):
            entry["version_scopes"] = [{"platform": "Android", "game_version": "1.5.3",
                "evidence": "正文引用 android/research/device-1.5.3/character-catalog/models.csv；PC 范围版本未知，不将整篇提升到 1.5.3。"}]
        entries.append(entry)
    for path in sorted(PROTECTED):
        source = checked(root, path)
        if source.is_file():
            content = source.read_text(encoding="utf-8-sig")
            title, line = doc_title(content, PurePosixPath(path).name)
            entries.append({"module": "workspace", "type": "policy" if "POLICY" in path else "maintenance_reference",
                "status": "policy" if "POLICY" in path else "reference", "game_version": "not_applicable",
                "platform": ["unspecified"], "legacy_path": path, "path": path, "title": title,
                "evidence": [{"kind": "document", "path": path, "line": line, "excerpt": title}],
                "ownership": "external_read_only", "purpose": "parent/test 维护的工作区说明"})
    return {
        "schema_version": 1, "migration_batch": BATCH,
        "inventory": {"path": inventory.as_posix(), "rows": len(rows), "review_date": "2026-10-05"},
        "scope": "Reviewed project papers plus module navigation; adjacent README/source notes and skill references remain in place.",
        "status_notes": {"partially_superseded": "仅正文指出的结论已替代，不代表全篇失效。",
                         "implemented_record": "记录原有实施范围，迁移不新增构建或实机验证。",
                         "reference": "用途调查所列维护入口；适用版本和验证范围以正文为准。"},
        "protected_paths": sorted(PROTECTED), "documents": entries,
        "current_game_version": workspace.get("game.version"),
        "workspace_snapshot": workspace.get("game.snapshot"),
        "version_policy": "用户确认九月初更新后为 1.5.3；旧客户端标 pre-1.5.3；通用工具/格式标 not_applicable；同版热更新用资源快照区分。",
    }


def table(document: str, entries: list[dict], *, research: bool = False) -> list[str]:
    lines = ["| 文档 | 用途 | 状态 | 游戏版本 |", "| --- | --- | --- | --- |"]
    for entry in sorted(entries, key=lambda e: (e["path"], e["title"])):
        title = entry["title"].replace("|", "\\|")
        purpose = entry.get("purpose", "").replace("|", "\\|")
        lines.append(f"| [{title}]({relative_to(document, entry['path'])}) | {purpose} | {STATUS_LABEL[entry['status']]} | {entry['game_version']} |")
    return lines if entries else ["本模块暂无独立现行说明；可从下列研究或邻接维护入口进入。"]


def navigation(catalog: dict) -> dict[str, str]:
    output = {}
    papers = [e for e in catalog["documents"] if e["legacy_path"] in MOVES]
    for module, title in MODULES.items():
        index = f"docs/{module}/INDEX.md"
        group = [e for e in papers if e["module"] == module]
        current = [e for e in group if "/research/" not in e["path"] and "/history/" not in e["path"] and "/releases/" not in e["path"]]
        research = [e for e in group if "/research/" in e["path"]]
        lines = [f"# {title}", "", "[全部文档](../INDEX.md)", "", "## 现行说明与维护入口", ""]
        if module == "workspace":
            # Parent/test owns these pages; the index does not edit their body.
            lines += ["- [工作区政策](WORKSPACE_POLICY.md)"]
        lines += table(index, current)
        lines += ["", "## 研究与阶段记录", "", "九月初更新后资料按用户确认与当前配置归为 1.5.3；旧客户端为 pre-1.5.3，跨版本比较在清单单独登记。同版热更新以资源快照区分，日期只作元数据。阶段实施、来源证据和提案保留验证边界；部分结论已替代不代表整篇无用。", ""]
        for version in sorted({e["game_version"] for e in research}):
            version_index = f"docs/{module}/research/{version}/INDEX.md"
            subset = [e for e in research if e["game_version"] == version]
            lines += [f"- [{version} 研究入口]({relative_to(index, version_index)})：{len(subset)} 篇。"]
            version_lines = [f"# {title}：游戏版本 {version}", "", f"[模块入口]({relative_to(version_index, index)})", "",
                             "这里保留研究、来源、实施阶段和历史结论。条目状态来自用途调查，研究正文中的来源、勘误、未验证项和适用边界保持原样。", ""]
            topics = sorted({e["path"].split("/")[5] for e in subset})
            for topic in topics:
                version_lines += [f"## {topic}", ""]
                version_lines += table(version_index, [e for e in subset if e["path"].split("/")[5] == topic]) + [""]
            output[version_index] = "\n".join(version_lines).rstrip() + "\n"
        if not research:
            lines += ["本模块没有独立迁入的版本研究；上述维护说明保留自身来源与适用范围。"]
        if module == "custom_model":
            lines += ["", "## 历史 BEM 格式与通用工具研究", "", "[格式版本、工具研究与既有归档入口](history/INDEX.md)。通用格式和工具资料不绑定游戏版本；当前中英文四组规范是本模块根目录的唯一维护来源。"]
        if module == "workspace":
            lines += ["", "## 软件发布记录", "", "以下目录号是 Better Endfield 软件版本，游戏版本未据此推断。", ""]
            lines += table(index, [e for e in group if "/releases/" in e["path"]])
        adjacent = [e for e in catalog["documents"] if e["module"] == module and e["ownership"] == "external_read_only"]
        if adjacent:
            lines += ["", "## 邻接文档与分发来源", "", "这些文件保留在原模块或工具旁。Skill reference 不迁移、不改正文；副本与维护来源的同步由打包流程负责。", ""]
            lines += table(index, adjacent)
        output[index] = "\n".join(lines).rstrip() + "\n"
    history_index = "docs/custom_model/history/INDEX.md"
    # Original archive introduction and its four current-document backlinks are
    # retained by the migration; append the newly organized history navigation.
    history_lines = ["", "## 按 BEM 格式版本追溯", "", "这些是原有归档，格式版本不是游戏版本；现行合并规范见上方入口。", ""]
    history_lines += table(history_index, [e for e in papers if "/history/" in e["path"] and not e["path"].endswith("INDEX.md")])
    history_lines += ["", "## 原归档中的研究与实施记录", "", "原 archive/bem 的版本研究材料按模块、配置中的游戏版本和专题保存，仍维持既有归档状态。", ""]
    history_lines += table(history_index, [e for e in papers if e.get("previously_archived") and "/research/" in e["path"]])
    output[history_index] = "\n".join(history_lines).rstrip() + "\n"
    lines = ["# 项目文档导航", "", "现行说明按模块维护；版本研究放在 `docs/<module>/research/<game-version>/<topic>/`。用户已确认九月初更新后为 1.5.3，此前资料使用 pre-1.5.3；同版热更新由资源快照区分。通用 BEM 格式、工具与替身测试不绑定游戏版本。研究、已实施阶段、部分取代结论与既有归档保留原文的验证范围和来源。", "",
             "| 模块 | 入口 |", "| --- | --- |"]
    lines += [f"| {title} | [{module}]({module}/INDEX.md) |" for module, title in MODULES.items()]
    lines += ["", "## 清单与引用", "", "- [文档用途、状态、版本与来源清单](../config/document-catalog.json)",
              "- [公开文件名到源码位置](../config/documents.json)：安装目录文档名保持原 basename。",
              f"- [全量旧路径到新路径映射](../{REWRITE_FILE})：供外部消费者、代码注释和证据引用集成。",
              "- [迁移方法与离线检查](workspace/DOCUMENT_MIGRATION.md)", ""]
    output["docs/INDEX.md"] = "\n".join(lines)
    output["docs/workspace/DOCUMENT_MIGRATION.md"] = f"""# 文档迁移与检查

本次只整理文档、登记来源和改写引用，不改变研究结论。用途调查为 `workspace_inventory_20261005/项目用途_文档.csv`（165 行）；源报告位置保存在清单元数据中。

## 组织与消费

- 现行说明：`docs/<module>/`；版本相关材料：`docs/<module>/research/<actual-game-version>/<topic>/`。首次迁移从最新工作区配置读取当前版本；用户已确认九月初后为 1.5.3，旧客户端使用 pre-1.5.3。
- BEM 历史格式按 `docs/custom_model/history/bem-<format>/` 组织；BE 发布记录按 `docs/workspace/releases/<software-version>/` 组织。两者不是游戏版本。
- 日期保留在原文标题、正文和 catalog 元数据中，不作为目录主分类。1.5.3 内部热更新由资源快照与来源清单区分。通用工具/格式的 `game_version` 为 `not_applicable`；跨版本对照列出两个版本，原文中未校正的 1.4.4 标记作为原始版本声明保留并注明与用户时间线差异。
- `config/documents.json` 的 `documents` 字典保存原公开 filename 到唯一源码 path。源码移动后，安装目录公开名仍为原 basename。
- 四组中英文 BEM 规范位于 `docs/custom_model/`，第三方模块作者指南位于 `docs/host/`。Skill reference 保留原位与原内容，parent 负责打包同步。
- `{REWRITE_FILE}` 是旧 repo-relative path 到新 path 的普通 JSON 字典，包含 126 个迁移路径及 1 个已确认的过时引用别名。根 README、模块邻接 README、代码注释和其他外部消费者由 parent 集成，不由迁移脚本写入。

## 运行方式

从当前仓库运行配置选择的 Python；`--workspace-config` 或 `BE_WORKSPACE_CONFIG` 沿用共同工作区 API。首次计划、离线模拟和迁移需要传入用途调查 CSV：

```powershell
python scripts/migrate_documents.py plan --inventory '<用途调查 CSV 的绝对路径>'
python scripts/migrate_documents.py simulate --inventory '<用途调查 CSV 的绝对路径>'
python scripts/migrate_documents.py apply --inventory '<用途调查 CSV 的绝对路径>'
python scripts/migrate_documents.py check
```

脚本只读 Git 文件清单和已登记的小型文本，不遍历外置盘历史资料、不计算大文件哈希。移动使用同一仓库内 `Path.rename`；每次读写与移动前验证解析后的绝对路径仍在当前根目录内，拒绝目标冲突、越界路径和受其他代理管理的三个 workspace 页面。

`simulate` 在内存中验证引用重定位、标题与正文行数保留、目标唯一性和重复运行无变化。`check` 检查清单、源码路径、研究版本目录、导航覆盖、相对文档引用和工作区文档查找 API。现有缺失的历史证据文件单独报告，不伪造资源、不视为本轮实机验证。带 Git 标签或 commit 的外部来源 URL 保留其历史路径。

外部文档消费者的待集成命中仅作只读报告；脚本不改根 README、邻接 README、代码、已有脚本或 skill。清单保留各文件的原用途、状态细节、勘误与证据定位，方便 parent 重写迁移范围以外的引用。
"""
    return output


def link_issues(state: dict[str, bytes], known: set[str]) -> list[dict]:
    issues = []
    for document, data in state.items():
        if not document.endswith(".md"):
            continue
        content = data.decode("utf-8-sig")
        for pattern in (LINK, REF_LINK):
            for match in pattern.finditer(content):
                path, _ = normalize_target(document, match["target"])
                if path is not None and path not in known:
                    issues.append({"path": document, "line": content.count("\n", 0, match.start()) + 1,
                                   "target": match["target"], "resolved": path,
                                   "kind": "missing_document" if path.startswith("docs/") and path.endswith(".md") else "missing_evidence"})
    return issues


def external_consumers(root: Path, known: set[str]) -> list[dict]:
    findings = []
    names = {PurePosixPath(old).name for old in REWRITES}
    for path in sorted(known):
        if path.startswith(("docs/", "legacy/", "native/shared/third_party/", "tools/CustomModel/skills/")) or path in OUTPUTS or path == "scripts/migrate_documents.py":
            continue
        if PurePosixPath(path).suffix.lower() not in {".md", ".markdown", ".rst", ".mdx", ".adoc"}:
            continue
        source = checked(root, path)
        if not source.is_file():
            continue
        content = source.read_text(encoding="utf-8-sig")
        for line, text in enumerate(content.splitlines(), 1):
            old_paths = [old for old in REWRITES if old in text]
            if not old_paths and not any(name in text for name in names):
                continue
            changed = rewrite(text, path, path, known)
            if changed != text:
                findings.append({"path": path, "line": line, "original": text, "suggested": changed})
    return findings


def prepare(workspace, inventory: Path, known: set[str]) -> tuple[dict[str, bytes], dict, dict[str, bytes]]:
    root = workspace.root
    originals, locations = {}, {}
    for old, new in MOVES.items():
        source, target = checked(root, old, writable=True), checked(root, new, writable=True)
        if source.is_file() and target.exists():
            raise ValueError(f"Both source and target exist; refusing overwrite: {old} -> {new}")
        current = old if source.is_file() else new
        selected = checked(root, current, writable=True)
        if not selected.is_file():
            raise FileNotFoundError(f"Neither source nor target exists: {old} -> {new}")
        originals[old], locations[old] = selected.read_bytes(), current
    catalog = make_catalog(workspace, inventory, originals, known)
    known |= {e["path"] for e in catalog["documents"] if checked(root, e["path"]).is_file()}
    final_known = (known - set(MOVES)) | set(MOVES.values()) | OUTPUTS
    state = {}
    for old, new in MOVES.items():
        before = originals[old].decode("utf-8-sig")
        after = rewrite(before, locations[old], new, known | set(MOVES.values()))
        if doc_title(before, "")[0] != doc_title(after, "")[0] or before.count("\n") != after.count("\n"):
            raise ValueError(f"Migration changed a title or body line count: {old}")
        if old == "docs/archive/bem/README.md":
            # Strip only our appended navigation on a repeat run.
            after = after.split("\n## 按 BEM 格式版本追溯", 1)[0].rstrip() + "\n"
        state[new] = after.encode("utf-8")
    nav = navigation(catalog)
    history = "docs/custom_model/history/INDEX.md"
    nav[history] = state[history].decode("utf-8").rstrip() + "\n" + nav[history]
    state.update({path: content.encode("utf-8") for path, content in nav.items()})
    catalog["documents"] = [e for e in catalog["documents"] if not e.get("generated_navigation")]
    for path, data in sorted(state.items()):
        if path in MOVES.values():
            continue
        title, line = doc_title(data.decode("utf-8"), PurePosixPath(path).name)
        catalog["documents"].append({"module": module_of(path), "type": "navigation" if path.endswith("INDEX.md") else "maintenance_reference",
            "status": "navigation" if path.endswith("INDEX.md") else "reference",
            "game_version": path.split("/")[3] if "/research/" in path else "not_applicable",
            "platform": ["unspecified"], "legacy_path": None, "path": path, "title": title,
            "evidence": [{"kind": "document", "path": path, "line": line, "excerpt": title}],
            "ownership": "documentation", "generated_navigation": True,
            "game_version_evidence": [{"kind": "navigation", "excerpt": "版本研究索引沿用所属目录与所索引文档的版本归类。"}] if "/research/" in path else []})
    final_known |= set(state)
    issues = link_issues(state, final_known)
    fatal = [issue for issue in issues if issue["kind"] == "missing_document"]
    if fatal:
        raise ValueError("Unresolved document links: " + json_text(fatal))
    catalog["checks"] = {"level": "static_and_in_memory_only", "missing_historical_evidence_links": issues,
                         "external_document_consumers": external_consumers(root, final_known | set(MOVES)),
                         "external_integration_owner": "parent", "game_or_build_checks_performed": False}
    public = {PurePosixPath(old).name: new for old, new in MOVES.items() if not old.endswith("/README.md")}
    state["config/documents.json"] = json_text({"schema_version": 1, "documents": dict(sorted(public.items()))}).encode("utf-8")
    state["config/document-catalog.json"] = json_text(catalog).encode("utf-8")
    state[REWRITE_FILE] = json_text(REWRITES).encode("utf-8")
    return originals, catalog, state


def check(workspace, known: set[str]) -> dict:
    root = workspace.root
    catalog = json.loads(checked(root, "config/document-catalog.json").read_text(encoding="utf-8-sig"))
    rewrites = json.loads(checked(root, REWRITE_FILE).read_text(encoding="utf-8-sig"))
    public = json.loads(checked(root, "config/documents.json").read_text(encoding="utf-8-sig"))["documents"]
    errors, states = [], {}
    for old, new in MOVES.items():
        if checked(root, old).exists() or not checked(root, new).is_file():
            errors.append(f"Unfinished move: {old} -> {new}")
        if rewrites.get(old) != new:
            errors.append(f"Missing rewrite: {old}")
    seen = set()
    for entry in catalog["documents"]:
        path = entry["path"]
        if path in seen:
            errors.append(f"Duplicate catalog entry: {path}")
        seen.add(path)
        required = {"module", "type", "status", "game_version", "platform", "legacy_path", "path", "title", "evidence"}
        if not required <= entry.keys():
            errors.append(f"Incomplete catalog metadata: {path}")
        source = checked(root, path)
        if not source.is_file():
            errors.append(f"Catalog source does not exist: {path}")
            continue
        if entry["ownership"] == "documentation":
            data = source.read_bytes()
            if doc_title(data.decode("utf-8-sig"), "")[0] != entry["title"]:
                errors.append(f"Title changed: {path}")
            states[path] = data
            if "/research/" in path and path.split("/")[3] != entry["game_version"]:
                errors.append(f"Research version mismatch: {path}")
        if entry["game_version"] not in ("unknown", "not_applicable") and not entry.get("game_version_evidence"):
            errors.append(f"Version has no explicit game evidence: {path}")
    for filename, path in public.items():
        if workspace.document(filename) != checked(root, path) or not checked(root, path).is_file():
            errors.append(f"workspace.document lookup failed: {filename}")
    known |= {e["path"] for e in catalog["documents"] if checked(root, e["path"]).is_file()}
    final_known = (known - set(MOVES)) | set(states) | OUTPUTS
    issues = link_issues(states, final_known)
    errors += [str(issue) for issue in issues if issue["kind"] == "missing_document"]
    for entry in catalog["documents"]:
        if entry["legacy_path"] not in MOVES or entry["path"].endswith("INDEX.md"):
            continue
        module_index = states[f"docs/{entry['module']}/INDEX.md"].decode("utf-8")
        if "/research/" in entry["path"]:
            version_index = f"docs/{entry['module']}/research/{entry['game_version']}/INDEX.md"
            index_content = states[version_index].decode("utf-8")
            owner_index = version_index
        elif "/history/" in entry["path"]:
            owner_index = "docs/custom_model/history/INDEX.md"
            index_content = states[owner_index].decode("utf-8")
        else:
            owner_index = f"docs/{entry['module']}/INDEX.md"
            index_content = module_index
        if relative_to(owner_index, entry["path"]) not in index_content:
            errors.append(f"Document is absent from navigation: {entry['path']}")
    if errors:
        raise ValueError(json_text(errors))
    return {"result": "passed", "moved_documents": len(MOVES), "rewrite_entries": len(rewrites),
            "public_filename_entries": len(public), "catalog_entries": len(catalog["documents"]),
            "modules": len(MODULES), "relative_document_links": "passed",
            "missing_historical_evidence_links": issues,
            "external_document_integration_hits": len(catalog["checks"]["external_document_consumers"]),
            "validation_level": "static_and_offline_only"}


def main() -> None:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("mode", choices=("plan", "simulate", "apply", "check"), nargs="?", default="plan")
    parser.add_argument("--inventory", type=Path)
    parser.add_argument("--workspace-config", type=Path)
    args = parser.parse_args()
    workspace = load_workspace(config_path=args.workspace_config)
    configure_plan(workspace)
    root = workspace.root
    if root.name.lower().endswith("_legacy"):
        parser.error("The legacy rollback directory is read only")
    legacy = workspace.path("legacy.root", required=False)
    if legacy and (root == legacy or legacy in root.parents):
        parser.error("The active repository overlaps legacy rollback data")
    known = tracked(root)
    if args.mode == "check":
        print(json_text(check(workspace, known)))
        return
    if not args.inventory:
        parser.error("plan/simulate/apply require --inventory (the reviewed purpose CSV)")
    originals, catalog, state = prepare(workspace, args.inventory.resolve(), known)
    before_outputs = {}
    for path in state:
        destination = checked(root, path, writable=True)
        before_outputs[path] = destination.read_bytes() if destination.is_file() else None
    changed = [path for path, data in state.items() if before_outputs[path] != data]
    pending = [old for old in MOVES if checked(root, old, writable=True).is_file()]
    result = {"mode": args.mode, "active_repo": root.as_posix(), "moves_pending": len(pending),
              "moved_documents": len(MOVES), "rewrite_entries": len(REWRITES),
              "files_to_write": len(changed), "catalog_entries": len(catalog["documents"]),
              "module_counts": dict(sorted(Counter(e["module"] for e in catalog["documents"] if e["legacy_path"] in MOVES).items())),
              "missing_historical_evidence_links": catalog["checks"]["missing_historical_evidence_links"],
              "external_document_integration_hits": len(catalog["checks"]["external_document_consumers"])}
    if args.mode == "simulate":
        final_known = (known - set(MOVES)) | set(state)
        for path, data in state.items():
            if not path.endswith(".md") or path.endswith("INDEX.md") or path == "docs/workspace/DOCUMENT_MIGRATION.md":
                continue
            if rewrite(data.decode("utf-8"), path, path, final_known) != data.decode("utf-8"):
                raise ValueError(f"Reference rewriting is not idempotent: {path}")
        # Exercise boundary rejection without touching a file.
        for relative in ("../Better Endfield_legacy/docs/INDEX.md", "docs/../../escape.md", "C:/escape.md", "scripts/workspace_config.py", *PROTECTED):
            try:
                checked(root, relative, writable=True)
            except ValueError:
                pass
            else:
                raise ValueError(f"Unsafe target was accepted: {relative}")
        result["in_memory_simulation"] = "passed"
    elif args.mode == "apply":
        # Validate every output before the first mutation; source bytes are
        # compared immediately before each rename to detect concurrent edits.
        for path in state:
            checked(root, path, writable=True)
        for old in pending:
            new = MOVES[old]
            source, destination = checked(root, old, writable=True), checked(root, new, writable=True)
            if source.read_bytes() != originals[old] or destination.exists():
                raise ValueError(f"Concurrent source edit or target creation: {old}")
            destination.parent.mkdir(parents=True, exist_ok=True)
            checked(root, old, writable=True).rename(checked(root, new, writable=True))
        for path, data in state.items():
            original_key = next((old for old, new in MOVES.items() if new == path), None)
            if original_key is not None and checked(root, path).read_bytes() != originals[original_key]:
                raise ValueError(f"Concurrent edit after preflight: {path}")
            if original_key is None:
                destination = checked(root, path, writable=True)
                current = destination.read_bytes() if destination.is_file() else None
                if current != before_outputs[path]:
                    raise ValueError(f"Concurrent output edit after preflight: {path}")
            write_if_changed(checked(root, path, writable=True), data)
        result["post_migration_check"] = check(workspace, tracked(root))
    print(json_text(result))


if __name__ == "__main__":
    main()
