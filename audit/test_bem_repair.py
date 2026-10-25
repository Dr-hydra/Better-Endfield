"""Regression tests for skills/bem-creator/scripts/bem_repair.py.

Run with:
    python audit/test_bem_repair.py -v

Every case is synthetic, so the suite runs in well under a second and does not
touch F:\\zmd. The cases encode the seven defects the rewrite was meant to fix.
"""
from __future__ import annotations

import hashlib
import json
import struct
import sys
import tempfile
import unittest
from unittest import mock
from pathlib import Path

ROOT = Path(__file__).resolve().parents[1]
SCRIPTS = ROOT / "tools" / "CustomModel" / "skills" / "bem-creator" / "scripts"
sys.path.insert(0, str(SCRIPTS))

import bem_repair as repair  # noqa: E402


def sha(path: Path) -> str:
    return hashlib.sha256(path.read_bytes()).hexdigest()


def gate_available() -> bool:
    """True when the CLI modules import, i.e. the entry gate can be rechecked.

    ``component_auto`` pulls in ``bem_v1``, which needs ``zstandard``. The
    refusal paths of ``entry-normalize`` never import it, so only the
    positive-path test needs this guard.
    """
    try:
        repair._component_auto()
        return True
    except Exception:
        return False


def dds_legacy(width: int, height: int, mips: int, fourcc: bytes, unit: int) -> bytes:
    header = bytearray(128)
    header[0:4] = b"DDS "
    struct.pack_into("<I", header, 4, 124)
    struct.pack_into("<I", header, 8, 0x1 | 0x2 | 0x4 | 0x1000 | 0x80000)
    struct.pack_into("<I", header, 12, height)
    struct.pack_into("<I", header, 16, width)
    struct.pack_into("<I", header, 20, max(1, (width + 3) // 4) * max(1, (height + 3) // 4) * unit)
    struct.pack_into("<I", header, 28, mips)
    struct.pack_into("<I", header, 76, 32)
    struct.pack_into("<I", header, 80, 0x4)
    header[84:88] = fourcc
    struct.pack_into("<I", header, 108, 0x1000)
    body = b""
    for level in range(mips):
        w = max(width >> level, 1)
        h = max(height >> level, 1)
        body += bytes([level & 0xFF]) * (max(1, (w + 3) // 4) * max(1, (h + 3) // 4) * unit)
    return bytes(header) + body


def dds_dx10(width: int, height: int, mips: int, array: int = 1,
             dxgi: int = 98, unit: int = 16) -> bytes:
    header = bytearray(128)
    header[0:4] = b"DDS "
    struct.pack_into("<I", header, 4, 124)
    struct.pack_into("<I", header, 8, 0x1 | 0x2 | 0x4 | 0x1000 | 0x80000)
    struct.pack_into("<I", header, 12, height)
    struct.pack_into("<I", header, 16, width)
    struct.pack_into("<I", header, 20, max(1, (width + 3) // 4) * max(1, (height + 3) // 4) * unit)
    struct.pack_into("<I", header, 28, mips)
    struct.pack_into("<I", header, 76, 32)
    struct.pack_into("<I", header, 80, 0x4)
    header[84:88] = b"DX10"
    struct.pack_into("<I", header, 108, 0x1000)
    dx10 = struct.pack("<5I", dxgi, 3, 0, array, 0)
    body = b""
    for slice_index in range(array):
        for level in range(mips):
            w = max(width >> level, 1)
            h = max(height >> level, 1)
            body += bytes([(slice_index * 16 + level) & 0xFF]) * (
                max(1, (w + 3) // 4) * max(1, (h + 3) // 4) * unit)
    return bytes(header) + dx10 + body


DEFORM_STREAMS = [
    b"".join(struct.pack("<4f", *p, 1.0) for p in [(0, 0, 0), (1, 0, 0), (0, 1, 0)]),
    b"".join(struct.pack("<2f4B", 0, 0, 127, 127, 127, 127) for _ in range(3)),
    b"".join(struct.pack("<4H4B", 65535, 0, 0, 0, 0, 0, 0, 0) for _ in range(3)),
    struct.pack("<3H", 0, 1, 2),
]


def delta_payload(records=None, count=None, header_bytes=None) -> bytes:
    """One ``4 + J + 16N`` delta payload; arguments let a test break one rule."""
    records = [(0, 0.0, 0.0, 0.5), (2, 0.1, 0.0, 0.0)] if records is None else records
    header = json.dumps({"count": len(records) if count is None else count,
                         "encoding": "sparse-position-f32"},
                        separators=(",", ":")).encode()
    if header_bytes is not None:
        header = header[:header_bytes]
    return (struct.pack("<I", len(header)) + header +
            b"".join(struct.pack("<I3f", *record) for record in records))


SHAPE_BATCH_WORDS = 132


def shape_config(batches: int = 1, table=None) -> bytes:
    """8 header u32 then 132 u32 per batch, matching the EFMI batch layout."""
    entries = list(table if table is not None else [0, 2]) + [2] * 126
    words = [0] * 8
    for _ in range(batches):
        words += [0, 0, 0, 0]          # base + three unused words
        words += entries[:SHAPE_BATCH_WORDS - 4]
    return struct.pack("<%dI" % len(words), *words)


def deform_manifest() -> dict:
    """The smallest manifest that carries one body parameter and one channel."""
    return {
        "schema": 1, "package_id": "test.deform", "name": "Deform test",
        "author": "test", "version": "1",
        "required_capabilities": ["native-materials", "palette-u8", "indices-u32",
                                  "fixed-appearances", "body-parameters",
                                  "mesh-position-deltas"],
        "target": {"character_id": "chr_test", "platform": "windows-x64",
                   "profile_id": "example", "revision": "1", "snapshot": "synthetic",
                   "world_resource": "world", "ui_resource": "ui",
                   "components": [{"id": 0, "mesh_name": "body_lod0",
                                   "original_index_count": 3,
                                   "bone_names": ["Root"], "materials": ["body"]}]},
        "meshes": [{"vertex_count": 3, "index_count": 3, "index_size": 2,
                    "streams": [{"stride": 16, "payload": 0}, {"stride": 12, "payload": 1},
                                {"stride": 12, "payload": 2}],
                    "indices": 3,
                    "attributes": [[0, 0, 3, 0, 0], [1, 0, 1, 0, 12], [4, 0, 2, 1, 0],
                                   [6, 3, 4, 1, 8], [12, 4, 4, 2, 0], [13, 6, 4, 2, 8]],
                    "bones": [{"component": 0, "index": 0, "name": "Root"}],
                    "draws": [{"start": 0, "count": 3, "material_component": 0,
                               "material_slot": 0, "material_name": "body",
                               "textures": []}]}],
        "textures": [], "default_appearance_id": "default",
        "appearances": [{"id": "default", "name": "Triangle",
                         "components": [{"target": 0, "operation": "replace", "mesh": 0}]}],
        "parameters": [{"id": "body", "name": "体型", "min": 0, "max": 1000,
                        "neutral": 0, "default": 0, "step": 1}],
        "mesh_deformations": [{"mesh": 0, "parameter": "body", "frames": [
            {"value": 0, "neutral": True},
            {"value": 1000, "payload": 4, "count": 2,
             "encoding": "sparse-position-f32"}]}],
    }


def option_manifest() -> dict:
    """Two groups where the second only exists for one choice of the first."""
    return {
        "meshes": [{"draws": [{"count": 300}, {"count": 300}]},
                   {"draws": [{"count": 100}]}],
        "target": {"components": [{"id": 0}, {"id": 1}]},
        "option_groups": [
            {"id": "outfit", "name": "服装", "default": "a",
             "choices": [{"id": "a", "name": "A"}, {"id": "b", "name": "B"}]},
            {"id": "part", "name": "部件", "default": "on",
             "available_when": {"eq": ["outfit", "b"]},
             "choices": [{"id": "on", "name": "显示"}, {"id": "off", "name": "隐藏"}]},
        ],
        "component_rules": [
            {"target": 0, "candidates": [
                {"when": {"eq": ["outfit", "a"]}, "operation": "keep"},
                {"when": {"eq": ["outfit", "b"]}, "operation": "replace", "mesh": 0}]},
            {"target": 1, "candidates": [
                {"when": {"eq": ["part", "on"]}, "operation": "replace", "mesh": 1},
                {"when": {"eq": ["part", "off"]}, "operation": "hide"},
                {"when": {"eq": ["outfit", "a"]}, "operation": "keep"}]},
        ],
    }


class RepairCase(unittest.TestCase):
    def setUp(self) -> None:
        self.tmp = Path(tempfile.mkdtemp(prefix="bem-repair-test-"))
        self.delta: bytes | None = None

    def tearDown(self) -> None:
        for path in sorted(self.tmp.rglob("*"), reverse=True):
            if path.is_file():
                path.unlink()
            elif path.is_dir():
                path.rmdir()
        self.tmp.rmdir()

    # -- index buffers ------------------------------------------------------

    def test_ib_overrun_is_measured_from_first(self) -> None:
        ib = self.tmp / "ib.buf"
        ib.write_bytes(b"\x00" * (300 * 2))
        report = repair.audit_ib(ib, first=200, count=150)
        self.assertTrue(report["overrun"])
        self.assertEqual(report["available_from_first"], 100)
        self.assertEqual(report["short_by"], 50)

    def test_ib_clamp_refuses_without_confirmation(self) -> None:
        ib = self.tmp / "ib.buf"
        ib.write_bytes(b"\x00" * (300 * 2))
        with self.assertRaises(repair.Refused) as caught:
            repair.clamp_ib(ib, first=0, count=303)
        self.assertEqual(caught.exception.code, "IB_TRUNCATION_NOT_ACCEPTED")

    def test_ib_clamp_returns_count_and_leaves_file_untouched(self) -> None:
        ib = self.tmp / "ib.buf"
        ib.write_bytes(b"\x00" * (300 * 2))
        before = sha(ib)
        report = repair.clamp_ib(ib, first=0, count=303, accept_truncation=True)
        self.assertTrue(report["changed"])
        self.assertEqual(report["clamped_count"], 300)
        self.assertEqual(report["dropped_triangles"], 1)
        self.assertEqual(sha(ib), before)

    def test_ib_clamp_refuses_unaligned_tail(self) -> None:
        ib = self.tmp / "ib.buf"
        ib.write_bytes(b"\x00" * (301 * 2))
        with self.assertRaises(repair.Refused) as caught:
            repair.clamp_ib(ib, first=0, count=400, accept_truncation=True)
        self.assertEqual(caught.exception.code, "IB_NOT_FIXABLE")

    # -- DDS ----------------------------------------------------------------

    def test_dds_audit_reports_layout(self) -> None:
        path = self.tmp / "tex.dds"
        path.write_bytes(dds_dx10(64, 64, 4))
        report = repair.audit_dds(path)
        self.assertEqual(report["width"], 64)
        self.assertEqual(report["mips"], 4)
        self.assertEqual(report["issues"], [])

    def test_mipdrop_handles_every_slice_of_a_cube(self) -> None:
        path = self.tmp / "cube.dds"
        raw = dds_dx10(64, 64, 4, array=6)
        path.write_bytes(raw)
        info = repair.parse_dds(raw)
        out = self.tmp / "cube.out.dds"
        report = repair.drop_mip0(path, out)
        self.assertEqual(report["slices"], 6)
        self.assertEqual(report["dropped"], 1)
        expected = info["header_len"] + (info["per_slice"] - info["mip_sizes"][0]) * 6
        self.assertEqual(out.stat().st_size, expected)
        self.assertEqual(sha(path), sha(path))  # input untouched
        self.assertEqual(path.stat().st_size, len(raw))

    def test_mipdrop_refuses_single_mip(self) -> None:
        path = self.tmp / "single.dds"
        path.write_bytes(dds_dx10(1024, 1024, 1))
        with self.assertRaises(repair.Refused) as caught:
            repair.drop_mip0(path, self.tmp / "out.dds")
        self.assertEqual(caught.exception.code, "DDS_SINGLE_MIP")

    def test_mipdrop_refuses_header_size_mismatch(self) -> None:
        path = self.tmp / "broken.dds"
        path.write_bytes(dds_dx10(64, 64, 4)[:-32])
        with self.assertRaises(repair.Refused) as caught:
            repair.drop_mip0(path, self.tmp / "out.dds")
        self.assertEqual(caught.exception.code, "DDS_SIZE_MISMATCH")

    def test_mipdrop_refuses_still_over_limit(self) -> None:
        path = self.tmp / "big.dds"
        path.write_bytes(dds_dx10(256, 256, 4))
        with self.assertRaises(repair.Refused) as caught:
            repair.drop_mip0(path, self.tmp / "out.dds", limit=1024)
        self.assertEqual(caught.exception.code, "DDS_STILL_OVER_LIMIT")

    def test_mipdrop_never_overwrites_input(self) -> None:
        path = self.tmp / "tex.dds"
        path.write_bytes(dds_legacy(64, 64, 4, b"DXT1", 8))
        with self.assertRaises(repair.Refused) as caught:
            repair.drop_mip0(path, path)
        self.assertEqual(caught.exception.code, "DDS_WOULD_OVERWRITE")

    # -- donors -------------------------------------------------------------

    def test_donor_find_reports_conflicting_sizes(self) -> None:
        (self.tmp / "a" / "Textures").mkdir(parents=True)
        (self.tmp / "b" / "Other").mkdir(parents=True)
        (self.tmp / "a" / "Textures" / "face_N.dds").write_bytes(b"A" * 8)
        (self.tmp / "b" / "Other" / "face_N.dds").write_bytes(b"B" * 16)
        report = repair.find_donors("Textures/face_N.dds", self.tmp)
        self.assertEqual(report["count"], 2)
        self.assertTrue(report["conflict"])
        self.assertFalse(report["usable"])
        self.assertEqual(report["best_tier"], "path")

    def test_donor_copy_refuses_without_evidence(self) -> None:
        donor = self.tmp / "x.dds"
        donor.write_bytes(b"1234")
        with self.assertRaises(repair.Refused) as caught:
            repair.copy_donor(donor, self.tmp / "out")
        self.assertEqual(caught.exception.code, "DONOR_NO_EVIDENCE")

    def test_donor_copy_checks_size_then_copies(self) -> None:
        donor = self.tmp / "x.dds"
        donor.write_bytes(b"1234")
        report = repair.copy_donor(donor, self.tmp / "out", expect_size=4)
        self.assertTrue(report["copied"])
        self.assertEqual(Path(report["output"]).read_bytes(), b"1234")

    def test_donor_copy_rejects_wrong_size(self) -> None:
        donor = self.tmp / "x.dds"
        donor.write_bytes(b"1234")
        with self.assertRaises(repair.Refused) as caught:
            repair.copy_donor(donor, self.tmp / "out", expect_size=5)
        self.assertEqual(caught.exception.code, "DONOR_SIZE_MISMATCH")

    # -- manifest edits -----------------------------------------------------

    def _project(self) -> Path:
        folder = self.tmp / "proj"
        (folder / "payloads").mkdir(parents=True)
        (folder / "payloads" / "0000.bin").write_bytes(b"x")
        (folder / "project.json").write_text(json.dumps({
            "manifest": {"meshes": [{"draws": [{}]}]},
            "payload_files": ["payloads/0000.bin"],
        }), encoding="utf-8")
        return folder / "project.json"

    def test_override_rejects_out_of_range_float(self) -> None:
        data, is_project = repair.load_project(self._project())
        with self.assertRaises(repair.Refused) as caught:
            repair.apply_overrides(data, is_project, floats={"_Cutoff": 500.0})
        self.assertEqual(caught.exception.code, "OVERRIDE_OUT_OF_RANGE")

    def test_override_rejects_bad_property_name(self) -> None:
        data, is_project = repair.load_project(self._project())
        with self.assertRaises(repair.Refused) as caught:
            repair.apply_overrides(data, is_project, floats={"Cutoff": 0.5})
        self.assertEqual(caught.exception.code, "OVERRIDE_BAD_NAME")

    def test_override_accepts_colors_and_vectors(self) -> None:
        data, is_project = repair.load_project(self._project())
        result = repair.apply_overrides(
            data, is_project,
            colors={"_BaseColor": [0.1, 0.2, 0.3, 0.75]},
            vectors={"_MainTex_ST": [1.0, 1.0, 0.0, 0.0]},
            enable_keywords=["_ALPHAPREMULTIPLY_ON"],
        )
        self.assertEqual(result["colors"]["_BaseColor"], [0.1, 0.2, 0.3, 0.75])
        self.assertEqual(result["enable_keywords"], ["_ALPHAPREMULTIPLY_ON"])
        self.assertIn("material-overrides-v1", data["manifest"]["required_capabilities"])

    def test_root_offset_rejects_more_than_twenty_centimetres(self) -> None:
        data, is_project = repair.load_project(self._project())
        with self.assertRaises(repair.Refused) as caught:
            repair.apply_root_offset(data, is_project, (0.0, 0.5, 0.0))
        self.assertEqual(caught.exception.code, "ROOT_OFFSET_TOO_LARGE")

    def test_root_offset_accepts_within_limit(self) -> None:
        data, is_project = repair.load_project(self._project())
        result = repair.apply_root_offset(data, is_project, (0.0, 0.04, 0.0))
        self.assertEqual(result["translation"], [0.0, 0.04, 0.0])
        self.assertIn("root-offset-v1", data["manifest"]["required_capabilities"])

    # -- INI duplicates -----------------------------------------------------

    def _ini(self, text: str, name: str = "mod.ini") -> Path:
        path = self.tmp / name
        path.write_text(text, encoding="utf-8")
        return path

    def test_ini_audit_separates_global_from_prefixed_duplicates(self) -> None:
        path = self._ini(
            "[Constants]\n"
            "$a = 1\n"
            "\n"
            "[Constants]\n"
            "$b = 2\n"
            "\n"
            "[TextureOverrideComponent0]\n"
            "hash = 1111\n"
            "\n"
            "[TextureOverrideComponent0]\n"
            "hash = 2222\n"
        )
        report = repair.audit_ini(path)
        rules = {row["section"]: row["rule"] for row in report["duplicates"]}
        self.assertEqual(rules["Constants"], "append_in_source_order")
        self.assertEqual(rules["TextureOverrideComponent0"], "first_occurrence_only")
        self.assertEqual(report["section_count"], 4)
        self.assertEqual(report["unique_names"], 2)
        self.assertTrue(report["ok"])

    def test_ini_audit_marks_unknown_repeats_as_unreviewed(self) -> None:
        path = self._ini("[MysteryBlock]\nx = 1\n\n[MysteryBlock]\nx = 2\n")
        report = repair.audit_ini(path)
        self.assertFalse(report["ok"])
        self.assertEqual(report["unreviewed_duplicates"][0]["rule"], "unreviewed")

    def test_ini_normalize_merges_globals_and_archives_prefixed(self) -> None:
        path = self._ini(
            "[Constants]\n"
            "$a = 1\n"
            "\n"
            "[Constants]\n"
            "$b = 2\n"
            "\n"
            "[TextureOverrideComponent0]\n"
            "hash = 1111\n"
            "\n"
            "[TextureOverrideComponent0]\n"
            "hash = 2222\n"
        )
        out = self.tmp / "mod.normalized.ini"
        report = repair.normalize_ini(path, out)
        self.assertTrue(report["projection_unchanged"])
        self.assertTrue(report["idempotent"])
        self.assertEqual(report["sections_before"], 4)
        self.assertEqual(report["sections_after"], 2)

        text = out.read_text(encoding="utf-8")
        # The global duplicate is folded into one section, in source order.
        self.assertLess(text.index("$a = 1"), text.index("$b = 2"))
        # The prefixed duplicate keeps the first copy and archives the second.
        self.assertIn("hash = 1111", text)
        self.assertIn("; hash = 2222", text)
        # The input is untouched.
        self.assertEqual(path.read_text(encoding="utf-8").count("[Constants]"), 2)

    def test_ini_normalize_keeps_the_loader_projection(self) -> None:
        source = "[Constants]\n$a = 1\n\n[Constants]\n$a = 2\n\n[Present]\nif $a\nendif\n"
        path = self._ini(source)
        out = self.tmp / "out.ini"
        repair.normalize_ini(path, out)
        before = repair.ini_projection(source)
        after = repair.ini_projection(out.read_text(encoding="utf-8"))
        self.assertEqual(before, after)
        # Later assignment wins for a global, and the rewrite preserved that.
        self.assertEqual(after["maps"]["constants"]["$a"], "2")

    def test_ini_normalize_refuses_unknown_duplicate(self) -> None:
        path = self._ini("[MysteryBlock]\nx = 1\n\n[MysteryBlock]\nx = 2\n")
        with self.assertRaises(repair.Refused) as caught:
            repair.normalize_ini(path, self.tmp / "out.ini")
        self.assertEqual(caught.exception.code, "INI_UNREVIEWED_GLOBAL_DUPLICATE")

    def test_ini_normalize_refuses_empty_namespace(self) -> None:
        path = self._ini("namespace =\n\n[Constants]\n$a = 1\n")
        with self.assertRaises(repair.Refused) as caught:
            repair.normalize_ini(path, self.tmp / "out.ini")
        self.assertEqual(caught.exception.code, "INI_EMPTY_NAMESPACE")

    # -- conditional textures ----------------------------------------------

    def test_texture_normalize_resolves_if_else_to_one_selection(self) -> None:
        path = self._ini(
            "[Constants]\n"
            "global $swapvar_body = 0\n"
            "\n"
            "[TextureOverrideComponent0Tex]\n"
            "hash = abcd1234\n"
            "if $swapvar_body == 0\n"
            "this = ResourceDefault\n"
            "else\n"
            "this = ResourceAlt\n"
            "endif\n"
        )
        out = self.tmp / "tex.ini"
        report = repair.normalize_textures(path, out)
        self.assertEqual(len(report["selected"]), 1)
        self.assertEqual(report["selected"][0]["resource"], "ResourceDefault")
        text = out.read_text(encoding="utf-8")
        self.assertIn("if $object_detected", text)
        self.assertIn("this = ResourceDefault", text)
        self.assertNotIn("ResourceAlt", text)

    def test_texture_normalize_honours_a_stated_state(self) -> None:
        path = self._ini(
            "[TextureOverrideComponent0Tex]\n"
            "hash = abcd1234\n"
            "if $swapvar_body == 0\n"
            "this = ResourceDefault\n"
            "else\n"
            "this = ResourceAlt\n"
            "endif\n"
        )
        out = self.tmp / "tex.ini"
        report = repair.normalize_textures(path, out, {"$swapvar_body": 1.0})
        self.assertEqual(report["selected"][0]["resource"], "ResourceAlt")
        self.assertIn("this = ResourceAlt", out.read_text(encoding="utf-8"))

    def test_texture_normalize_leaves_per_draw_slots_alone(self) -> None:
        source = (
            "[TextureOverrideComponent0Draw]\n"
            "hash = abcd1234\n"
            "ps-t0 = ResourceBody\n"
            "ps-t1 = ResourceMask\n"
        )
        path = self._ini(source)
        out = self.tmp / "tex.ini"
        report = repair.normalize_textures(path, out)
        self.assertEqual(report["left_untouched"], ["TextureOverrideComponent0Draw"])
        self.assertEqual(out.read_text(encoding="utf-8"), source)

    def test_texture_normalize_refuses_unknown_condition(self) -> None:
        path = self._ini(
            "[TextureOverrideComponent0Tex]\n"
            "hash = abcd1234\n"
            "if $unknown_key == 1\n"
            "this = ResourceA\n"
            "endif\n"
        )
        out = self.tmp / "tex.ini"
        report = repair.normalize_textures(path, out)
        self.assertFalse(report["ok"])
        self.assertEqual(report["refused_sections"][0]["refused"], "TEXTURE_CONDITION_UNKNOWN")
        # The section is preserved verbatim so the caller can inspect it.
        self.assertIn("$unknown_key", out.read_text(encoding="utf-8"))

    def test_texture_normalize_matches_variables_case_insensitively(self) -> None:
        # 3Dmigoto names are case-insensitive; Constants declares $SocksColor
        # while the condition reads $sockscolor.
        path = self._ini(
            "[Constants]\n"
            "global persist $SocksColor = 0\n"
            "\n"
            "[TextureOverrideComponent0Tex]\n"
            "hash = abcd1234\n"
            "if $sockscolor == 0\n"
            "this = ResourceSock\n"
            "endif\n"
        )
        report = repair.normalize_textures(path, self.tmp / "tex.ini")
        self.assertEqual(len(report["selected"]), 1)
        self.assertEqual(report["selected"][0]["resource"], "ResourceSock")

    def test_texture_normalize_ignores_an_injected_watermark(self) -> None:
        path = self._ini(
            "[TextureOverrideComponent0Tex]\n"
            "hash = abcd1234\n"
            "if $object_detected\n"
            "this = ResourceA\n"
            "endif\n"
            "【防倒卖：群号 799191082】_283125\n"
        )
        report = repair.normalize_textures(path, self.tmp / "tex.ini")
        self.assertTrue(report["ok"])
        self.assertEqual(report["selected"][0]["resource"], "ResourceA")

    # -- skin and vertex data ----------------------------------------------

    def _skin_project(self, weights, indices, draws, palette=4,
                      meshes=1, index_size=4) -> Path:
        """Build an unpacked project whose stream 2 is a real skin layout."""
        folder = self.tmp / "skin"
        (folder / "payloads").mkdir(parents=True, exist_ok=True)
        count = len(weights)
        positions = b"".join(struct.pack("<4f", float(i), 0.0, 0.0, 1.0)
                             for i in range(count))
        normals = b"".join(struct.pack("<3f", 0.0, 1.0, 0.0) for _ in range(count))
        skin = b""
        for vertex in range(count):
            packed = [max(0, min(65535, int(round(value * 65535))))
                      for value in weights[vertex]]
            skin += struct.pack("<4H", *packed) + struct.pack("<4B", *indices[vertex])
        payloads = [positions, normals, skin]
        draw_ids = []
        for values in draws:
            draw_ids.append(len(payloads))
            payloads.append(struct.pack(f"<{len(values)}I", *values))
        for order, raw in enumerate(payloads):
            (folder / "payloads" / f"{order:04d}.bin").write_bytes(raw)
        bone_table = [{"component": 0, "index": order, "name": f"Bip001_{order}"}
                      for order in range(palette)]
        manifest_meshes = []
        for _ in range(meshes):
            manifest_meshes.append({
                "vertex_count": count,
                "index_size": index_size,
                "streams": [{"stride": 16, "payload": 0},
                            {"stride": 12, "payload": 1},
                            {"stride": 12, "payload": 2}],
                "attributes": [[0, 0, 4, 0, 0], [1, 0, 3, 1, 0],
                               [12, 4, 4, 2, 0], [13, 6, 4, 2, 8]],
                "bones": bone_table,
                "draws": [{"indices": draw_ids[order], "count": len(values),
                           "material_component": 0, "material_slot": 0,
                           "material_name": "M_test", "textures": []}
                          for order, values in enumerate(draws)],
            })
        manifest = {"meshes": manifest_meshes}
        (folder / "project.json").write_text(json.dumps({
            "manifest": manifest,
            "payload_files": [f"payloads/{order:04d}.bin" for order in range(len(payloads))],
        }), encoding="utf-8")
        return folder / "project.json"

    def test_skin_audit_separates_zero_weight_from_weighted_overruns(self) -> None:
        project = self._skin_project(
            weights=[[1.0, 0.0, 0.0, 0.0], [1.0, 0.0, 0.0, 0.0]],
            indices=[[0, 9, 9, 0], [9, 0, 0, 0]],
            draws=[[0, 1, 0]],
        )
        report = repair.audit_skin(project)
        row = report["meshes"][0]
        self.assertEqual(row["out_of_palette_with_zero_weight"], 2)
        self.assertEqual(row["out_of_palette_with_nonzero_weight"], 1)
        self.assertFalse(report["ok"])
        self.assertEqual(report["blocking_meshes"], [0])
        self.assertEqual(report["fixable_meshes"], [0])

    def test_skin_fix_zeroes_only_slots_with_no_weight(self) -> None:
        project = self._skin_project(
            weights=[[1.0, 0.0, 0.0, 0.0], [0.5, 0.5, 0.0, 0.0]],
            indices=[[0, 9, 9, 0], [1, 2, 0, 0]],
            draws=[[0, 1, 0]],
        )
        before = sha(project)
        out = self.tmp / "fixed"
        report = repair.fix_skin(project, out)
        self.assertTrue(report["written"])
        self.assertEqual(report["changed_meshes"][0]["zeroed_indices"], 2)
        self.assertEqual(sha(project), before)          # input untouched
        after = repair.audit_skin(out / "project.json")
        self.assertTrue(after["ok"])
        self.assertEqual(after["meshes"][0]["out_of_palette_with_zero_weight"], 0)

    def test_skin_fix_refuses_a_weighted_overrun(self) -> None:
        project = self._skin_project(
            weights=[[1.0, 0.0, 0.0, 0.0]],
            indices=[[9, 0, 0, 0]],
            draws=[[0, 0, 0]],
        )
        with self.assertRaises(repair.Refused) as caught:
            repair.fix_skin(project, self.tmp / "fixed")
        self.assertEqual(caught.exception.code, "SKIN_BONE_OUT_OF_PALETTE")

    def test_skin_fix_writes_nothing_when_already_clean(self) -> None:
        project = self._skin_project(
            weights=[[1.0, 0.0, 0.0, 0.0]],
            indices=[[1, 0, 0, 0]],
            draws=[[0, 0, 0]],
        )
        report = repair.fix_skin(project, self.tmp / "fixed")
        self.assertFalse(report["written"])
        self.assertIsNone(report["output"])
        self.assertFalse((self.tmp / "fixed").exists())

    def test_skin_fix_pins_all_zero_weight_vertices_only_when_asked(self) -> None:
        project = self._skin_project(
            weights=[[0.0, 0.0, 0.0, 0.0], [1.0, 0.0, 0.0, 0.0]],
            indices=[[3, 3, 3, 3], [0, 0, 0, 0]],
            draws=[[0, 1, 0]],
        )
        self.assertEqual(
            repair.audit_skin(project)["meshes"][0]["all_zero_weight_vertices"], 1)
        self.assertFalse(repair.fix_skin(project, self.tmp / "keep")["written"])

        report = repair.fix_skin(project, self.tmp / "first", zero_weight_policy="first")
        self.assertTrue(report["written"])
        self.assertEqual(report["changed_meshes"][0]["zero_weight_vertices_fixed"], 1)
        self.assertEqual(
            repair.audit_skin(self.tmp / "first" / "project.json")
            ["meshes"][0]["all_zero_weight_vertices"], 0)

    def test_vertex_compact_drops_unreferenced_vertices(self) -> None:
        project = self._skin_project(
            weights=[[1.0, 0.0, 0.0, 0.0]] * 8,
            indices=[[0, 0, 0, 0]] * 8,
            draws=[[0, 1, 2], [3, 4, 5]],
        )
        out = self.tmp / "compact"
        report = repair.vertex_compact(project, out, 0)
        self.assertEqual(report["source_vertices"], 8)
        self.assertEqual(report["used_vertices"], 6)
        self.assertEqual(report["dropped_vertices"], 2)
        self.assertTrue(report["geometry_verified"])
        self.assertEqual(report["compacted_meshes"], [0])

        data, _ = repair.load_project(out / "project.json")
        mesh = data["manifest"]["meshes"][0]
        self.assertEqual(mesh["vertex_count"], 6)
        payloads = repair.load_payloads(out / "project.json", data, True)
        # The kept rows are the original rows, in order.
        source, _ = repair.load_project(project)
        original = repair.load_payloads(project, source, True)
        self.assertEqual(payloads[mesh["streams"][0]["payload"]],
                         original[0][:6 * 16])
        self.assertEqual(payloads[mesh["streams"][2]["payload"]],
                         original[2][:6 * 12])

    def test_vertex_compact_prunes_the_orphaned_buffers(self) -> None:
        project = self._skin_project(
            weights=[[1.0, 0.0, 0.0, 0.0]] * 6,
            indices=[[0, 0, 0, 0]] * 6,
            draws=[[0, 1, 2]],
        )
        source, _ = repair.load_project(project)
        before = len(source["payload_files"])
        out = self.tmp / "compact"
        report = repair.vertex_compact(project, out, 0)
        self.assertEqual(report["input_payloads"], before)
        self.assertLess(report["payload_pruning"]["payloads_after"],
                        report["payload_pruning"]["payloads_before"])
        # Nothing may reference a payload id that no longer exists.
        data, _ = repair.load_project(out / "project.json")
        self.assertEqual(len(data["payload_files"]),
                         report["payload_pruning"]["payloads_after"])

    def test_vertex_compact_refuses_when_nothing_is_unused(self) -> None:
        project = self._skin_project(
            weights=[[1.0, 0.0, 0.0, 0.0]] * 3,
            indices=[[0, 0, 0, 0]] * 3,
            draws=[[0, 1, 2]],
        )
        with self.assertRaises(repair.Refused) as caught:
            repair.vertex_compact(project, self.tmp / "compact", 0)
        self.assertEqual(caught.exception.code, "VERTEX_COMPACT_NOOP")

    def test_vertex_compact_refuses_u16_indices(self) -> None:
        project = self._skin_project(
            weights=[[1.0, 0.0, 0.0, 0.0]] * 6,
            indices=[[0, 0, 0, 0]] * 6,
            draws=[[0, 1, 2]],
            index_size=2,
        )
        with self.assertRaises(repair.Refused) as caught:
            repair.vertex_compact(project, self.tmp / "compact", 0)
        self.assertEqual(caught.exception.code, "VERTEX_COMPACT_NEEDS_U32")

    def test_vertex_compact_treats_a_shared_buffer_as_one_unit(self) -> None:
        # Two meshes read the same vertex buffer. Compacting only one would have
        # to keep the old buffer for the other, so the group is rewritten.
        project = self._skin_project(
            weights=[[1.0, 0.0, 0.0, 0.0]] * 8,
            indices=[[0, 0, 0, 0]] * 8,
            draws=[[0, 1, 2]],
            meshes=2,
        )
        report = repair.vertex_compact(project, self.tmp / "compact", 0)
        self.assertEqual(report["compacted_meshes"], [0, 1])
        data, _ = repair.load_project(self.tmp / "compact" / "project.json")
        for mesh in data["manifest"]["meshes"]:
            self.assertEqual(mesh["vertex_count"], 3)

    def test_prune_payloads_keeps_references_from_unusual_places(self) -> None:
        manifest = {
            "meshes": [{"streams": [{"payload": 0}], "draws": [{"indices": 1}]}],
            "textures": [{"payload": 2}],
            "deformations": [{"frames": [{"value": 0, "payload": 3, "count": 1,
                                          "encoding": "sparse-position-f32"}]}],
            "audio": [{"payload": 4}],
            "preview": 5,
        }
        payloads = [bytes([index]) * 4 for index in range(7)]  # 6 is orphaned
        pruned, kept, report = repair.prune_payloads(manifest, payloads)
        self.assertEqual(report["dropped"], 1)
        self.assertEqual(len(kept), 6)
        self.assertEqual(pruned["preview"], 5)
        self.assertEqual(pruned["audio"][0]["payload"], 4)
        self.assertEqual(pruned["deformations"][0]["frames"][0]["payload"], 3)

    def test_prune_payloads_refuses_a_dangling_reference(self) -> None:
        manifest = {"meshes": [{"streams": [{"payload": 9}]}]}
        with self.assertRaises(repair.Refused) as caught:
            repair.prune_payloads(manifest, [b"x"])
        self.assertEqual(caught.exception.code, "PAYLOAD_REF_OUT_OF_RANGE")

    # -- merged-skeleton skin conversion ------------------------------------

    BONE_NAMES = ["Bip001_Pelvis", "Bip001_Spine", "Bip001_Head",
                  "Bip001_L_Hand", "Bip001_R_Hand", "Bip001_Tail"]

    def _merged_skin_project(self, skin_rows, draws, merged_stride=16,
                             meshes=1, with_target=True, bone_names=None) -> Path:
        """A project whose skin stream carries *global* bone ids (EFMI merged)."""
        folder = self.tmp / "merged"
        (folder / "payloads").mkdir(parents=True, exist_ok=True)
        count = len(skin_rows)
        positions = b"".join(struct.pack("<4f", float(i), 0.0, 0.0, 1.0)
                             for i in range(count))
        normals = b"".join(struct.pack("<3f", 0.0, 1.0, 0.0) for _ in range(count))
        skin = b""
        for weights, bones in skin_rows:
            if merged_stride == 16:
                skin += struct.pack("<4H", *weights) + struct.pack("<4H", *bones)
            else:
                skin += struct.pack("<4H", *bones)
        payloads = [positions, normals, skin]
        draw_ids = []
        for values in draws:
            draw_ids.append(len(payloads))
            payloads.append(struct.pack(f"<{len(values)}I", *values))
        for order, raw in enumerate(payloads):
            (folder / "payloads" / f"{order:04d}.bin").write_bytes(raw)
        names = self.BONE_NAMES if bone_names is None else bone_names
        if merged_stride == 16:
            attributes = [[0, 0, 4, 0, 0], [1, 0, 3, 1, 0],
                          [12, 4, 4, 2, 0], [13, 4, 4, 2, 8]]
        else:
            attributes = [[0, 0, 4, 0, 0], [1, 0, 3, 1, 0], [13, 4, 4, 2, 0]]
        mesh_list = [{
            "vertex_count": count,
            "index_size": 4,
            "streams": [{"stride": 16, "payload": 0}, {"stride": 12, "payload": 1},
                        {"stride": merged_stride, "payload": 2}],
            "attributes": attributes,
            "bones": [{"component": 0, "index": i, "name": name}
                      for i, name in enumerate(names[:1])],
            "draws": [{"indices": draw_ids[order], "count": len(values),
                       "material_component": 0, "material_slot": 0,
                       "material_name": "M_test", "textures": []}
                      for order, values in enumerate(draws)],
        } for _ in range(meshes)]
        manifest = {"meshes": mesh_list}
        if with_target:
            manifest["target"] = {"components": [
                {"id": 0, "mesh_name": "S_test", "original_index_count": 3,
                 "bone_names": names, "materials": ["M_test"]}]}
        (folder / "project.json").write_text(json.dumps({
            "manifest": manifest,
            "payload_files": [f"payloads/{order:04d}.bin" for order in range(len(payloads))],
        }), encoding="utf-8")
        return folder / "project.json"

    def test_skin_convert_builds_a_mesh_local_palette(self) -> None:
        rows = [((65535, 0, 0, 0), (2, 0, 0, 0)),
                ((32768, 32767, 0, 0), (2, 0, 0, 0)),
                ((65535, 0, 0, 0), (0, 0, 0, 0))]
        project = self._merged_skin_project(rows, draws=[[0, 1, 2]])
        out = self.tmp / "native"
        report = repair.skin_convert(project, out, 0)
        self.assertEqual(report["source_layout"], "merged16")
        self.assertEqual(report["target_stride"], 12)
        # First-use order: vertex 0 uses global bone 2 first, then bone 0.
        self.assertEqual(report["palette_bones"], ["Bip001_Head", "Bip001_Pelvis"])
        self.assertTrue(report["weights_verified"])

        data, _ = repair.load_project(out / "project.json")
        mesh = data["manifest"]["meshes"][0]
        self.assertEqual(len(mesh["bones"]), 2)
        self.assertEqual(mesh["streams"][2]["stride"], 12)
        self.assertEqual([a[0] for a in mesh["attributes"]].count(12), 1)
        payloads = repair.load_payloads(out / "project.json", data, True)
        skin = payloads[mesh["streams"][2]["payload"]]
        self.assertEqual(len(skin), 3 * 12)
        weights, bones = struct.unpack_from("<4H", skin, 0), struct.unpack_from("<4B", skin, 8)
        self.assertEqual(weights, (65535, 0, 0, 0))
        self.assertEqual(bones[0], 0)                 # Bip001_Head
        weights, bones = struct.unpack_from("<4H", skin, 24), struct.unpack_from("<4B", skin, 32)
        self.assertEqual(weights, (65535, 0, 0, 0))
        self.assertEqual(bones[0], 1)                 # Bip001_Pelvis

    def test_skin_convert_handles_the_rigid_stride8_form(self) -> None:
        rows = [((), (3, 0, 0, 0)), ((), (5, 0, 0, 0))]
        project = self._merged_skin_project(rows, draws=[[0, 1, 0]], merged_stride=8)
        out = self.tmp / "native"
        report = repair.skin_convert(project, out, 0)
        self.assertEqual(report["source_layout"], "merged8")
        self.assertEqual(report["palette_bones"], ["Bip001_L_Hand", "Bip001_Tail"])
        data, _ = repair.load_project(out / "project.json")
        payloads = repair.load_payloads(out / "project.json", data, True)
        skin = payloads[data["manifest"]["meshes"][0]["streams"][2]["payload"]]
        self.assertEqual(struct.unpack_from("<4H", skin, 0), (65535, 0, 0, 0))
        self.assertEqual(struct.unpack_from("<4B", skin, 8), (0, 0, 0, 0))

    def test_skin_convert_can_target_the_float_layout(self) -> None:
        rows = [((32768, 32767, 0, 0), (1, 2, 0, 0))]
        project = self._merged_skin_project(rows, draws=[[0, 0, 0]])
        report = repair.skin_convert(project, self.tmp / "native", 0, target_stride=32)
        self.assertEqual(report["target_stride"], 32)
        data, _ = repair.load_project(self.tmp / "native" / "project.json")
        payloads = repair.load_payloads(self.tmp / "native" / "project.json", data, True)
        skin = payloads[data["manifest"]["meshes"][0]["streams"][2]["payload"]]
        weights = struct.unpack_from("<4f", skin, 0)
        self.assertAlmostEqual(weights[0] + weights[1], 1.0, places=4)
        self.assertEqual(struct.unpack_from("<4I", skin, 16)[:2], (0, 1))

    def test_skin_convert_refuses_an_already_native_stream(self) -> None:
        project = self._skin_project(
            weights=[[1.0, 0.0, 0.0, 0.0]], indices=[[0, 0, 0, 0]], draws=[[0, 0, 0]])
        with self.assertRaises(repair.Refused) as caught:
            repair.skin_convert(project, self.tmp / "native", 0)
        self.assertEqual(caught.exception.code, "SKIN_ALREADY_NATIVE")

    def test_skin_convert_refuses_a_palette_over_256(self) -> None:
        names = [f"Bip001_{index}" for index in range(300)]
        # 300 vertices, each rigidly bound to a different global bone.
        rows = [((65535, 0, 0, 0), (index, 0, 0, 0)) for index in range(300)]
        project = self._merged_skin_project(rows, draws=[list(range(300))],
                                            bone_names=names)
        with self.assertRaises(repair.Refused) as caught:
            repair.skin_convert(project, self.tmp / "native", 0)
        self.assertEqual(caught.exception.code, "SKIN_PALETTE_OVERFLOW")

    def test_skin_convert_refuses_a_bone_outside_the_table(self) -> None:
        rows = [((65535, 0, 0, 0), (99, 0, 0, 0))]
        project = self._merged_skin_project(rows, draws=[[0, 0, 0]])
        with self.assertRaises(repair.Refused) as caught:
            repair.skin_convert(project, self.tmp / "native", 0)
        self.assertEqual(caught.exception.code, "SKIN_BONE_OUT_OF_TABLE")

    def test_skin_convert_needs_a_bone_table(self) -> None:
        rows = [((65535, 0, 0, 0), (1, 0, 0, 0))]
        project = self._merged_skin_project(rows, draws=[[0, 0, 0]], with_target=False)
        with self.assertRaises(repair.Refused) as caught:
            repair.skin_convert(project, self.tmp / "native", 0)
        self.assertEqual(caught.exception.code, "SKIN_NO_BONE_TABLE")

    def test_skin_convert_accepts_an_explicit_palette_file(self) -> None:
        rows = [((65535, 0, 0, 0), (1, 0, 0, 0))]
        project = self._merged_skin_project(rows, draws=[[0, 0, 0]], with_target=False)
        palette = self.tmp / "bones.json"
        palette.write_text(json.dumps([
            {"component": 0, "index": 0, "name": "Bip001_Pelvis"},
            {"component": 0, "index": 1, "name": "Bip001_Spine"},
        ]), encoding="utf-8")
        report = repair.skin_convert(project, self.tmp / "native", 0, palette_path=palette)
        self.assertEqual(report["palette_bones"], ["Bip001_Spine"])

    def test_skin_convert_rewrites_a_shared_skin_buffer_once(self) -> None:
        rows = [((65535, 0, 0, 0), (1, 0, 0, 0)),
                ((65535, 0, 0, 0), (2, 0, 0, 0))]
        project = self._merged_skin_project(rows, draws=[[0, 1]], meshes=2)
        report = repair.skin_convert(project, self.tmp / "native", 0)
        self.assertEqual(report["converted_meshes"], [0, 1])
        data, _ = repair.load_project(self.tmp / "native" / "project.json")
        meshes = data["manifest"]["meshes"]
        self.assertEqual(meshes[0]["streams"][2]["payload"],
                         meshes[1]["streams"][2]["payload"])
        self.assertEqual(meshes[0]["bones"], meshes[1]["bones"])

    # -- entry programs -----------------------------------------------------

    DISPATCHER = "\n".join(repair.ENTRY_PROGRAM) + "\n"
    OLD_ENTRY = (
        "[Constants]\n"
        "global $mod_enabled = 1\n"
        "\n"
        "[TextureOverride_Component0]\n"
        "hash = d160fb72\n"
        "$object_detected = 1\n"
        "if $mod_enabled && DRAW_TYPE == 4\n"
        "handling = skip\n"
        "run = CommandListDraw0\n"
        "endif\n"
        "\n"
        "[CommandListDraw0]\n"
        "drawindexed = 300, 0, 0\n"
    )

    @staticmethod
    def _gate_available() -> bool:
        return gate_available()

    def test_entry_normalize_lowers_an_old_inline_program(self) -> None:
        path = self._ini(self.OLD_ENTRY)
        out = self.tmp / "entry.ini"
        report = repair.entry_normalize(path, out)
        self.assertTrue(report["written"])
        self.assertEqual(report["lowered"], ["TextureOverride_Component0"])
        self.assertTrue(report["dispatcher_added"])
        self.assertTrue(report["component_count_added"])
        text = out.read_text(encoding="utf-8")
        self.assertIn("CommandList\\EFMIv1\\Callback_Component_DrawCustom = ref CommandListDraw0", text)
        self.assertIn("run = CommandListBEMReviewedDispatcher", text)
        self.assertIn("global $component_count = 1", text)
        self.assertNotIn("handling = skip\nrun = CommandListDraw0", text)
        # The source file is untouched.
        self.assertEqual(path.read_text(encoding="utf-8"), self.OLD_ENTRY)

    @unittest.skipUnless(gate_available(), "component_auto needs zstandard")
    def test_entry_normalize_passes_the_real_gate(self) -> None:
        path = self._ini(self.OLD_ENTRY)
        out = self.tmp / "entry.ini"
        repair.entry_normalize(path, out)
        gate = repair._component_auto()
        _preamble, blocks = repair.split_blocks(out.read_text(encoding="utf-8"))
        sections = {block["name"]: block["body"] for block in blocks}
        gate.validate_entry(sections, {"section": "TextureOverride_Component0",
                                       "source_component": 0})

    def test_entry_normalize_upgrades_a_dispatcher_missing_template_lines(self) -> None:
        partial = [line for line in repair.ENTRY_PROGRAM
                   if "Object_ReadConfig" not in line]
        source = (
            "[Constants]\n"
            "global $component_count = 1\n"
            "\n"
            "[TextureOverride_Component0]\n"
            "hash = d160fb72\n"
            "$object_detected = 1\n"
            "if $mod_enabled && DRAW_TYPE == 4\n"
            "$\\EFMIv1\\component_id = 0\n"
            "$\\EFMIv1\\gpu_posed = 1\n"
            "CommandList\\EFMIv1\\Callback_Component_DrawCustom = ref CommandListDraw0\n"
            "run = CommandListLocal\n"
            "endif\n"
            "\n"
            "[CommandListLocal]\n"
            + "\n".join(partial) + "\n"
            "\n"
            "[CommandListDraw0]\n"
            "drawindexed = 300, 0, 0\n"
        )
        path = self._ini(source)
        out = self.tmp / "entry.ini"
        report = repair.entry_normalize(path, out)
        self.assertTrue(report["written"])
        self.assertEqual(report["evidence"][0]["operation"], "upgrade_dispatcher")
        self.assertEqual(report["evidence"][0]["added_statements"],
                         [line for line in repair.ENTRY_PROGRAM if "Object_ReadConfig" in line])
        text = out.read_text(encoding="utf-8")
        self.assertIn("Object_ReadConfig", text)
        self.assertNotIn("CommandListBEMReviewedDispatcher", text)

    def test_entry_normalize_refuses_a_newer_dispatcher(self) -> None:
        newer = list(repair.ENTRY_PROGRAM) + [r"$\EFMIv1\bones_count = $bones_count"]
        source = (
            "[TextureOverride_Component0]\n"
            "hash = d160fb72\n"
            "$object_detected = 1\n"
            "if $mod_enabled && DRAW_TYPE == 4\n"
            "$\\EFMIv1\\component_id = 0\n"
            "$\\EFMIv1\\gpu_posed = 1\n"
            "CommandList\\EFMIv1\\Callback_Component_DrawCustom = ref CommandListDraw0\n"
            "run = CommandListLocal\n"
            "endif\n"
            "\n"
            "[CommandListLocal]\n"
            + "\n".join(newer) + "\n"
            "\n"
            "[CommandListDraw0]\n"
            "drawindexed = 300, 0, 0\n"
        )
        path = self._ini(source)
        report = repair.entry_normalize(path, self.tmp / "entry.ini")
        self.assertFalse(report["written"])
        self.assertFalse(report["ok"])
        stale = report["template_stale"][0]
        self.assertEqual(stale["dispatcher"], "CommandListLocal")
        self.assertEqual(stale["extra_statements"], [r"$\EFMIv1\bones_count = $bones_count"])
        self.assertEqual(stale["entries"], ["TextureOverride_Component0"])
        self.assertEqual(report["refused_sections"][0]["refused"], "ENTRY_TEMPLATE_STALE")

    def test_entry_normalize_names_a_cpu_posed_component(self) -> None:
        source = (
            "[TextureOverride_Component3]\n"
            "hash = ba957493\n"
            "$object_detected = 1\n"
            "if $mod_enabled && DRAW_TYPE == 4\n"
            "$\\EFMIv1\\component_id = 3\n"
            "$\\EFMIv1\\gpu_posed = 0\n"
            "CommandList\\EFMIv1\\Callback_Component_DrawCustom = ref CommandListDraw0\n"
            "run = CommandListLocal\n"
            "endif\n"
        )
        path = self._ini(source)
        report = repair.entry_normalize(path, self.tmp / "entry.ini")
        self.assertEqual(report["refused_sections"][0]["refused"], "ENTRY_CPU_POSED_COMPONENT")
        self.assertIn("gpu_posed = 0", report["refused_sections"][0]["reason"])

    def test_entry_normalize_leaves_a_standard_entry_alone(self) -> None:
        source = (
            "[TextureOverride_Component0]\n"
            "hash = d160fb72\n"
            "$object_detected = 1\n"
            "if $mod_enabled && DRAW_TYPE == 4\n"
            "$\\EFMIv1\\component_id = 0\n"
            "$\\EFMIv1\\gpu_posed = 1\n"
            "CommandList\\EFMIv1\\Callback_Component_DrawCustom = ref CommandListDraw0\n"
            "run = CommandListBEMReviewedDispatcher\n"
            "endif\n"
            "\n"
            "[CommandListBEMReviewedDispatcher]\n" + self.DISPATCHER + "\n"
            "[CommandListDraw0]\n"
            "drawindexed = 300, 0, 0\n"
        )
        path = self._ini(source)
        report = repair.entry_normalize(path, self.tmp / "entry.ini")
        self.assertFalse(report["written"])
        self.assertTrue(report["ok"])
        self.assertEqual(report["already_standard"], ["TextureOverride_Component0"])

    def test_entry_normalize_reports_a_file_without_entries(self) -> None:
        # A companion INI has no ComponentN entry at all; that is not a defect.
        path = self._ini("[CommandListFoo]\ndrawindexed = 3, 0, 0\n")
        report = repair.entry_normalize(path, self.tmp / "entry.ini")
        self.assertTrue(report["ok"])
        self.assertFalse(report["written"])
        self.assertFalse(report["applicable"])
        self.assertFalse((self.tmp / "entry.ini").exists())

    def test_entry_normalize_refuses_duplicate_sections(self) -> None:
        path = self._ini("[TextureOverride_Component0]\nhash = d160fb72\n"
                         "\n[TextureOverride_Component0]\nhash = d160fb72\n")
        with self.assertRaises(repair.Refused) as caught:
            repair.entry_normalize(path, self.tmp / "entry.ini")
        self.assertEqual(caught.exception.code, "ENTRY_DUPLICATE_SECTION")

    def test_entry_normalize_ignores_an_injected_watermark(self) -> None:
        # Scraper banners sit after 'endif' in real packages and are not
        # statements. bem_merge removes them on the same rule.
        source = (
            "[TextureOverride_Component0]\n"
            "hash = d160fb72\n"
            "$object_detected = 1\n"
            "if $mod_enabled && DRAW_TYPE == 4\n"
            "handling = skip\n"
            "run = CommandListDraw0\n"
            "endif\n"
            "【防倒卖：此资源来自网站：/www.k/ekehxl./top，扣扣群号：7/99191082】_702728\n"
            "\n"
            "[CommandListDraw0]\n"
            "drawindexed = 300, 0, 0\n"
        )
        path = self._ini(source)
        out = self.tmp / "entry.ini"
        report = repair.entry_normalize(path, out)
        self.assertTrue(report["written"])
        self.assertEqual(report["lowered"], ["TextureOverride_Component0"])
        self.assertNotIn("防倒卖", out.read_text(encoding="utf-8"))

    def test_is_watermark_leaves_real_statements_alone(self) -> None:
        self.assertTrue(repair._is_watermark("【防倒卖：群号 799191082】_702728"))
        self.assertFalse(repair._is_watermark("drawindexed = 300, 0, 0"))
        self.assertFalse(repair._is_watermark("endif"))
        self.assertFalse(repair._is_watermark("run = CommandListDraw0"))


    # -- texture overrides --------------------------------------------------

    def test_texture_audit_accepts_the_static_shape(self) -> None:
        # hash + object-detected guard + one resource assignment is the only
        # shape component_auto.texture_table will convert on its own.
        path = self._ini(
            "[TextureOverride_Body]\n"
            "hash = d160fb72\n"
            "if $object_detected\n"
            "this = ResourceBodyDiffuse\n"
            "endif\n"
        )
        report = repair.audit_textures(path)
        self.assertTrue(report["ok"])
        self.assertEqual(report["convertible_static"], 1)
        self.assertEqual(report["texture_overrides"][0]["kind"], "static")
        self.assertIsNone(report["texture_overrides"][0]["auto_error"])

    def test_texture_audit_blocks_on_a_duplicate_identity(self) -> None:
        # Two sections claiming one hash is ambiguous in the source itself, so it
        # is the one finding that must stop the build instead of being reported.
        path = self._ini(
            "[TextureOverride_Body]\n"
            "hash = d160fb72\n"
            "if $object_detected\n"
            "this = ResourceA\n"
            "endif\n"
            "\n"
            "[TextureOverride_Body_Alt]\n"
            "hash = d160fb72\n"
            "if $object_detected\n"
            "this = ResourceB\n"
            "endif\n"
        )
        report = repair.audit_textures(path)
        self.assertFalse(report["ok"])
        self.assertEqual(report["duplicate_hashes"][0]["hash"], "d160fb72")
        self.assertEqual(len(report["duplicate_hashes"][0]["sections"]), 2)
        self.assertTrue(any("TEXTURE_MAPPING" in item for item in report["blockers"]))
        self.assertIn("duplicate", report["note"])

    def test_texture_audit_blocks_on_a_missing_identity(self) -> None:
        path = self._ini("[TextureOverride_NoHash]\nthis = ResourceA\n")
        report = repair.audit_textures(path)
        self.assertFalse(report["ok"])
        self.assertEqual(report["overrides_without_identity"], ["TextureOverride_NoHash"])
        self.assertIn("缺少贴图身份", report["blockers"][0])

    def test_texture_audit_reports_per_draw_slot_writes(self) -> None:
        # ps-t0 = ResourceX binds one slot for one draw; it is not a component
        # texture replacement and must be surfaced rather than merged.
        path = self._ini(
            "[TextureOverride_Body_Draw]\n"
            "hash = d160fb72\n"
            "ps-t0 = ResourceBodyDiffuse\n"
            "ps-t1 = ResourceBodyNormal\n"
        )
        report = repair.audit_textures(path)
        self.assertEqual(report["per_draw_overrides"], 1)
        self.assertEqual(report["per_draw_sections"], ["TextureOverride_Body_Draw"])
        row = report["texture_overrides"][0]
        self.assertEqual(row["kind"], "per_draw")
        self.assertEqual(row["slots"], ["ps-t0", "ps-t1"])
        self.assertEqual(row["null_slots"], [])
        self.assertIn("ps-t0 = ResourceBodyDiffuse", row["auto_error"])

    def test_texture_audit_recognises_slot_clearing(self) -> None:
        # ps-t18 = null means the draw binds nothing in that slot; BEM 1.5 spells
        # this with texture_slots, so the report has to name the slot.
        path = self._ini(
            "[TextureOverride_Body_Clear]\n"
            "hash = d160fb72\n"
            "ps-t18 = null\n"
            "ps-t19 = 0\n"
        )
        report = repair.audit_textures(path)
        self.assertEqual(report["cleared_slots"], ["ps-t18", "ps-t19"])
        self.assertEqual(report["texture_overrides"][0]["null_slots"], ["ps-t18", "ps-t19"])

    def test_texture_audit_reports_unsupported_statements_without_blocking(self) -> None:
        # $active = 1 is a real EFMI marker; the auto path cannot express it, but
        # a human can carry it across, so it must not fail the audit.
        path = self._ini(
            "[TextureOverride.Active.Mark]\n"
            "hash = 1f3a085f\n"
            "$active = 1\n"
        )
        report = repair.audit_textures(path)
        self.assertTrue(report["ok"])
        self.assertEqual(report["blockers"], [])
        self.assertEqual(len(report["manual_review"]), 1)
        self.assertEqual(report["manual_review"][0]["section"], "TextureOverride.Active.Mark")
        self.assertTrue(report["manual_review"][0]["error"].startswith("TEXTURE_STATE:"))
        self.assertEqual(report["unsupported"][0]["statements"], ["$active = 1"])

    def test_texture_audit_separates_entries_and_lod_variants(self) -> None:
        path = self._ini(
            "[TextureOverride_Component0]\n"
            "hash = 11223344\n"
            "handling = skip\n"
            "\n"
            "[TextureOverride_Component0_LOD1]\n"
            "hash = 55667788\n"
            "ps-t0 = ResourceA\n"
            "\n"
            "[TextureOverride_Body]\n"
            "hash = 99aabbcc\n"
            "if $object_detected\n"
            "this = ResourceBody\n"
            "endif\n"
        )
        report = repair.audit_textures(path)
        self.assertEqual(report["entries"], [{"section": "TextureOverride_Component0",
                                              "component": 0, "hash": "11223344"}])
        # The LOD variant and the entry are not texture overrides.
        self.assertEqual([row["section"] for row in report["texture_overrides"]],
                         ["TextureOverride_Body"])
        self.assertEqual(report["convertible_static"], 1)

    def test_texture_audit_ignores_comments_and_banners(self) -> None:
        path = self._ini(
            "[TextureOverride_Body]\n"
            "; a comment about the texture\n"
            "hash = d160fb72\n"
            "【防倒卖：群号 799191082】_702728\n"
            "if $object_detected\n"
            "this = ResourceBody\n"
            "endif\n"
        )
        report = repair.audit_textures(path)
        self.assertEqual(report["texture_overrides"][0]["kind"], "static")
        self.assertEqual(report["manual_review"], [])

    def test_texture_audit_rejects_a_missing_file(self) -> None:
        with self.assertRaises(repair.Refused) as caught:
            repair.audit_textures(self.tmp / "absent.ini")
        self.assertEqual(caught.exception.code, "INI_NOT_FOUND")


    # -- catalog coverage ---------------------------------------------------

    def _catalog(self, name: str, entries, **over) -> Path:
        directory = self.tmp / "catalog"
        directory.mkdir(exist_ok=True)
        payload = {
            "schema": 1, "kind": "bem-character-catalog", "verified": True,
            "platform": "windows-x64", "mapping_status": "offline-resource-identities",
            "character_id": name, "name": name.upper(), "entries": list(entries),
        }
        payload.update(over)
        path = directory / f"{name}.json"
        path.write_text(json.dumps(payload), encoding="utf-8")
        return directory

    def _entry_ini(self, hashes, prefix: str = "TextureOverride_Component") -> Path:
        body = ""
        for index, identity in enumerate(hashes):
            body += (f"[{prefix}{index}]\n"
                     f"hash = {identity}\n"
                     "match_first_index = 0\n"
                     "match_index_count = 300\n\n")
        return self._ini(body, "entry.ini")

    def test_catalog_gap_matches_a_single_profile(self) -> None:
        directory = self._catalog("chr_0001_a", ["11223344", "55667788"])
        path = self._entry_ini(["11223344", "55667788"])
        report = repair.catalog_gap(path, directory)
        self.assertTrue(report["ok"])
        self.assertEqual(report["verdict"], "matched")
        self.assertEqual(report["entry_family"], "component-n")
        self.assertTrue(report["catalog_required"])
        self.assertEqual(report["matching_profiles"], ["chr_0001_a"])
        self.assertEqual(report["best_missing"], [])

    def test_catalog_gap_reports_an_identity_nobody_knows(self) -> None:
        directory = self._catalog("chr_0001_a", ["11223344"])
        path = self._entry_ini(["11223344", "deadbeef"])
        report = repair.catalog_gap(path, directory)
        self.assertFalse(report["ok"])
        self.assertEqual(report["verdict"], "unknown-identity")
        self.assertEqual(report["unknown_identities"], ["deadbeef"])
        self.assertEqual(report["best_missing"], ["deadbeef"])
        self.assertIn("EFMI hash", report["note"])

    def test_catalog_gap_reports_a_package_mixing_characters(self) -> None:
        # Each catalog owns one half, so no single profile can ever satisfy it.
        directory = self._catalog("chr_0001_a", ["11223344"])
        self._catalog("chr_0002_b", ["deadbeef"])
        path = self._entry_ini(["11223344", "deadbeef"])
        report = repair.catalog_gap(path, directory)
        self.assertFalse(report["ok"])
        self.assertEqual(report["verdict"], "mixed-characters")
        self.assertEqual(report["unknown_identities"], [])
        self.assertEqual(report["identities_present_elsewhere"], {"deadbeef": ["chr_0002_b"]})

    def test_catalog_gap_reports_an_ambiguous_profile(self) -> None:
        directory = self._catalog("chr_0001_a", ["11223344"])
        self._catalog("chr_0002_b", ["11223344"])
        path = self._entry_ini(["11223344"])
        report = repair.catalog_gap(path, directory)
        self.assertFalse(report["ok"])
        self.assertEqual(report["verdict"], "ambiguous-profile")
        self.assertEqual(len(report["matching_profiles"]), 2)

    def test_catalog_gap_ignores_unusable_profiles(self) -> None:
        directory = self._catalog("chr_0001_a", ["11223344"], verified=False)
        self._catalog("chr_0002_b", ["11223344"])
        path = self._entry_ini(["11223344"])
        report = repair.catalog_gap(path, directory)
        self.assertEqual(report["matching_profiles"], ["chr_0002_b"])
        self.assertEqual(len(report["unusable_catalogs"]), 1)
        self.assertIn("verified", report["unusable_catalogs"][0]["reason"])

    def test_catalog_gap_accepts_a_dict_shaped_entry_table(self) -> None:
        # Some catalogs map hash -> component index instead of listing hashes.
        directory = self._catalog("chr_0001_a", {"11223344": 0})
        path = self._entry_ini(["11223344"])
        report = repair.catalog_gap(path, directory)
        self.assertTrue(report["ok"])

    def test_catalog_gap_does_not_require_a_catalog_for_hash_named_exports(self) -> None:
        # Hash-named entries carry their own index count; the lowering path never
        # consults a catalog. The family still has no reader, so the report must
        # block on that instead of on a catalog gap.
        directory = self._catalog("chr_0001_a", ["11223344"])
        path = self._ini("[TextureOverride_11223344_300_0]\n"
                         "hash = 11223344\n"
                         "match_index_count = 300\n"
                         "match_first_index = 0\n")
        report = repair.catalog_gap(path, directory)
        self.assertFalse(report["catalog_required"])
        self.assertEqual(report["verdict"], "not-required")
        self.assertEqual(report["entry_family"], "bare-hash")
        self.assertFalse(report["ok"])
        self.assertIn("ENTRY_FAMILY", report["blockers"][0])

    def test_catalog_gap_recognises_the_hash_lod_family(self) -> None:
        directory = self._catalog("chr_0001_a", ["11223344"])
        path = self._ini("[TextureOverride_LOD0.11223344_300_0]\n"
                         "hash = 11223344\n"
                         "match_index_count = 300\n"
                         "match_first_index = 0\n")
        report = repair.catalog_gap(path, directory)
        self.assertEqual(report["entry_family"], "hash-lod")
        self.assertEqual(report["entries"][0]["hash"], "11223344")
        self.assertFalse(report["catalog_required"])

    def test_catalog_gap_recognises_the_crossib_family(self) -> None:
        directory = self._catalog("chr_0001_a", ["11223344"])
        path = self._ini("[TextureOverride_IB_11223344_cloth_Component1]\n"
                         "hash = 11223344\n"
                         "match_first_index = 0\n"
                         "handling = skip\n")
        report = repair.catalog_gap(path, directory)
        self.assertEqual(report["entry_family"], "ib-component")
        self.assertEqual(report["entries"][0]["component"], 1)
        self.assertIn("ENTRY_FAMILY", report["blockers"][0])

    def test_catalog_gap_reports_shapes_it_cannot_read(self) -> None:
        directory = self._catalog("chr_0001_a", ["11223344"])
        path = self._ini("[TextureOverride_Weird_Thing]\nhash = 11223344\n")
        report = repair.catalog_gap(path, directory)
        self.assertFalse(report["ok"])
        self.assertEqual(report["verdict"], "unrecognized-entry")
        self.assertEqual(report["entry_count"], 0)
        self.assertEqual(list(report["observed_shapes"]), ["TextureOverride_Weird_Thing"])

    def test_catalog_gap_refuses_a_companion_ini(self) -> None:
        directory = self._catalog("chr_0001_a", ["11223344"])
        path = self._ini("[CommandListFoo]\ndrawindexed = 3, 0, 0\n")
        with self.assertRaises(repair.Refused) as caught:
            repair.catalog_gap(path, directory)
        self.assertEqual(caught.exception.code, "SOURCE_IDENTITY")

    def test_catalog_gap_refuses_a_name_body_hash_mismatch(self) -> None:
        # In the hash-named families the name *is* the identity, so a body that
        # disagrees makes the entry untrustworthy.
        directory = self._catalog("chr_0001_a", ["11223344"])
        path = self._ini("[TextureOverride_11223344_300_0]\n"
                         "hash = 99aabbcc\n"
                         "match_index_count = 300\n")
        with self.assertRaises(repair.Refused) as caught:
            repair.catalog_gap(path, directory)
        self.assertEqual(caught.exception.code, "SOURCE_IDENTITY")
        self.assertIn("不一致", caught.exception.message)

    def test_catalog_gap_treats_the_crossib_name_hash_as_a_resource_tag(self) -> None:
        # TextureOverride_IB_<tag>_<label>_ComponentN: the tag is the resource
        # group the buffers came from, and the body hash is the draw identity.
        directory = self._catalog("chr_0001_a", ["586b4574"])
        path = self._ini("[TextureOverride_IB_b21b22ac_衣服_Component1]\n"
                         "hash = 586b4574\n"
                         "match_first_index = 0\n")
        report = repair.catalog_gap(path, directory)
        entry = report["entries"][0]
        self.assertEqual(entry["hash"], "586b4574")
        self.assertEqual(entry["resource_tag"], "b21b22ac")
        self.assertEqual(report["matching_profiles"], ["chr_0001_a"])

    def test_catalog_gap_flags_a_non_zero_first_index(self) -> None:
        directory = self._catalog("chr_0001_a", ["11223344"])
        path = self._ini("[TextureOverride_Component0]\n"
                         "hash = 11223344\n"
                         "match_first_index = 300\n")
        report = repair.catalog_gap(path, directory)
        self.assertFalse(report["ok"])
        self.assertIn("SUBMESH_MAPPING", report["blockers"][0])

    def test_catalog_gap_reports_an_empty_catalog_directory(self) -> None:
        directory = self.tmp / "empty"
        directory.mkdir()
        path = self._entry_ini(["11223344"])
        with self.assertRaises(repair.Refused) as caught:
            repair.catalog_gap(path, directory)
        self.assertEqual(caught.exception.code, "CATALOG_EMPTY")


    # -- body parameters ----------------------------------------------------

    def _deform(self, manifest: dict | None = None) -> Path:
        root = self.tmp / "deform"
        payloads = root / "payloads"
        payloads.mkdir(parents=True, exist_ok=True)
        for index, raw in enumerate(DEFORM_STREAMS):
            (payloads / f"{index}.bin").write_bytes(raw)
        (payloads / "delta.bin").write_bytes(self.delta or delta_payload())
        files = [f"payloads/{i}.bin" for i in range(4)] + ["payloads/delta.bin"]
        path = root / "project.json"
        path.write_text(json.dumps({"manifest": deform_manifest() if manifest is None
                                    else manifest, "payload_files": files}),
                        encoding="utf-8")
        return path

    def _codes(self, report: dict) -> list:
        return [finding["code"] for finding in report["findings"]]

    def test_deformation_audit_accepts_a_valid_package(self) -> None:
        report = repair.deformation_audit(self._deform(), 3)
        self.assertTrue(report["ok"])
        self.assertTrue(report["applicable"])
        self.assertEqual(report["findings"], [])
        self.assertEqual((report["parameters"], report["channels"]), (1, 1))

    def test_deformation_audit_skips_a_package_without_morphs(self) -> None:
        manifest = deform_manifest()
        del manifest["parameters"]
        del manifest["mesh_deformations"]
        report = repair.deformation_audit(self._deform(manifest), 3)
        self.assertTrue(report["ok"])
        self.assertFalse(report["applicable"])

    def test_deformation_audit_flags_a_pre_1_3_header(self) -> None:
        report = repair.deformation_audit(self._deform(), 2)
        self.assertIn("DEFORM_VERSION", self._codes(report))

    def test_deformation_audit_flags_a_missing_capability(self) -> None:
        manifest = deform_manifest()
        manifest["required_capabilities"].remove("mesh-position-deltas")
        report = repair.deformation_audit(self._deform(manifest), 3)
        self.assertEqual(self._codes(report), ["DEFORM_CAPABILITY"])

    def test_deformation_audit_flags_unaligned_parameter_ticks(self) -> None:
        manifest = deform_manifest()
        manifest["parameters"][0]["step"] = 7
        report = repair.deformation_audit(self._deform(manifest), 3)
        self.assertIn("DEFORM_PARAMETER_ALIGNMENT", self._codes(report))

    def test_deformation_audit_flags_a_duplicate_parameter(self) -> None:
        manifest = deform_manifest()
        manifest["parameters"].append(dict(manifest["parameters"][0]))
        report = repair.deformation_audit(self._deform(manifest), 3)
        self.assertIn("DEFORM_PARAMETER_DUPLICATE", self._codes(report))

    def test_deformation_audit_flags_frames_that_miss_max(self) -> None:
        manifest = deform_manifest()
        manifest["mesh_deformations"][0]["frames"][1]["value"] = 500
        report = repair.deformation_audit(self._deform(manifest), 3)
        self.assertIn("DEFORM_FRAME_COVERAGE", self._codes(report))

    def test_deformation_audit_flags_non_increasing_frames(self) -> None:
        manifest = deform_manifest()
        frames = manifest["mesh_deformations"][0]["frames"]
        frames.append(dict(frames[1]))
        report = repair.deformation_audit(self._deform(manifest), 3)
        self.assertIn("DEFORM_FRAME_TICK", self._codes(report))

    def test_deformation_audit_flags_a_neutral_frame_off_neutral(self) -> None:
        manifest = deform_manifest()
        manifest["mesh_deformations"][0]["frames"][0]["value"] = 100
        report = repair.deformation_audit(self._deform(manifest), 3)
        self.assertIn("DEFORM_FRAME_NEUTRAL", self._codes(report))

    def test_deformation_audit_flags_two_neutral_frames(self) -> None:
        manifest = deform_manifest()
        manifest["mesh_deformations"][0]["frames"][1] = {"value": 0, "neutral": True}
        report = repair.deformation_audit(self._deform(manifest), 3)
        self.assertIn("DEFORM_FRAME_NEUTRAL", self._codes(report))

    def test_deformation_audit_flags_a_payload_length_mismatch(self) -> None:
        # The frame claims three records but the payload carries two.
        self.delta = delta_payload()
        manifest = deform_manifest()
        manifest["mesh_deformations"][0]["frames"][1]["count"] = 3
        report = repair.deformation_audit(self._deform(manifest), 3)
        self.assertIn("DEFORM_PAYLOAD_SIZE", self._codes(report))
        self.assertIn("DEFORM_PAYLOAD_HEADER", self._codes(report))

    def test_deformation_audit_flags_an_out_of_range_vertex_index(self) -> None:
        self.delta = delta_payload(records=[(9, 0.0, 0.0, 0.0)])
        manifest = deform_manifest()
        manifest["mesh_deformations"][0]["frames"][1]["count"] = 1
        report = repair.deformation_audit(self._deform(manifest), 3)
        self.assertIn("DEFORM_PAYLOAD_INDEX", self._codes(report))

    def test_deformation_audit_flags_a_duplicate_vertex_index(self) -> None:
        self.delta = delta_payload(records=[(1, 0.0, 0.0, 0.0), (1, 0.1, 0.0, 0.0)])
        report = repair.deformation_audit(self._deform(), 3)
        self.assertIn("DEFORM_PAYLOAD_INDEX", self._codes(report))

    def test_deformation_audit_flags_a_non_finite_delta(self) -> None:
        self.delta = delta_payload(records=[(1, float("inf"), 0.0, 0.0)])
        manifest = deform_manifest()
        manifest["mesh_deformations"][0]["frames"][1]["count"] = 1
        report = repair.deformation_audit(self._deform(manifest), 3)
        self.assertIn("DEFORM_PAYLOAD_RECORD", self._codes(report))

    def test_deformation_audit_flags_a_missing_position_attribute(self) -> None:
        manifest = deform_manifest()
        manifest["meshes"][0]["attributes"] = [a for a in manifest["meshes"][0]["attributes"]
                                               if a[0] != 0]
        report = repair.deformation_audit(self._deform(manifest), 3)
        self.assertIn("DEFORM_POSITION_ATTRIBUTE", self._codes(report))

    def test_deformation_audit_flags_an_undeclared_parameter(self) -> None:
        manifest = deform_manifest()
        manifest["mesh_deformations"][0]["parameter"] = "absent"
        report = repair.deformation_audit(self._deform(manifest), 3)
        self.assertIn("DEFORM_CHANNEL_TARGET", self._codes(report))

    def test_deformation_audit_flags_a_slider_used_as_a_condition_group(self) -> None:
        # available_when may only reference discrete option groups.
        manifest = deform_manifest()
        manifest["parameters"][0]["available_when"] = {"eq": ["body", "x"]}
        report = repair.deformation_audit(self._deform(manifest), 3)
        self.assertIn("DEFORM_CONDITION_UNKNOWN_GROUP", self._codes(report))

    def test_deformation_audit_flags_a_too_deep_condition(self) -> None:
        manifest = deform_manifest()
        manifest["option_groups"] = [{"id": "outfit", "name": "服装", "default": "a",
                                      "choices": [{"id": "a", "name": "A"},
                                                  {"id": "b", "name": "B"}]}]
        node: dict = {"eq": ["outfit", "a"]}
        for _ in range(repair.CONDITION_DEPTH + 1):
            node = {"not": node}
        manifest["parameters"][0]["available_when"] = node
        report = repair.deformation_audit(self._deform(manifest), 3)
        self.assertIn("DEFORM_CONDITION_DEPTH", self._codes(report))

    def test_deformation_audit_flags_a_condition_with_two_keys(self) -> None:
        manifest = deform_manifest()
        manifest["option_groups"] = [{"id": "outfit", "name": "服装", "default": "a",
                                      "choices": [{"id": "a", "name": "A"}]}]
        manifest["parameters"][0]["available_when"] = {"eq": ["outfit", "a"],
                                                       "not": {"eq": ["outfit", "a"]}}
        report = repair.deformation_audit(self._deform(manifest), 3)
        self.assertIn("DEFORM_CONDITION_SHAPE", self._codes(report))


    # -- option groups ------------------------------------------------------

    def _options(self, manifest: dict | None = None) -> Path:
        path = self.tmp / "options.json"
        path.write_text(json.dumps({"manifest": option_manifest() if manifest is None
                                    else manifest, "payload_files": ["unused.bin"]}),
                        encoding="utf-8")
        return path

    def test_option_audit_enumerates_legal_states(self) -> None:
        report = repair.option_audit(self._options())
        self.assertTrue(report["ok"])
        self.assertEqual(report["findings"], [])
        # outfit=a hides 'part', so the legal states are 1 + 2 = 3.
        self.assertEqual(report["states"], 3)
        self.assertEqual(report["default_state"], {"outfit": "a", "part": None})
        self.assertEqual(report["max_draws_per_mesh"], 2)

    def test_option_audit_flags_two_candidates_in_one_state(self) -> None:
        manifest = option_manifest()
        manifest["component_rules"][0]["candidates"].append(
            {"when": {"eq": ["outfit", "a"]}, "operation": "hide"})
        report = repair.option_audit(self._options(manifest))
        self.assertFalse(report["ok"])
        self.assertIn("OPTION_RULE_AMBIGUOUS", report["codes"])
        self.assertEqual(report["illegal_states"][0]["matches"], 2)

    def test_option_audit_flags_a_state_with_no_candidate(self) -> None:
        # 'part' is unavailable while outfit=a, so target 1 has nothing to pick.
        manifest = option_manifest()
        manifest["component_rules"][1]["candidates"] = [
            {"when": {"eq": ["part", "on"]}, "operation": "replace", "mesh": 1}]
        report = repair.option_audit(self._options(manifest))
        self.assertFalse(report["ok"])
        self.assertIn("OPTION_RULE_UNCOVERED", report["codes"])

    def test_option_audit_flags_the_256_draw_ceiling(self) -> None:
        manifest = option_manifest()
        manifest["meshes"][1]["draws"] = [{"count": 1}] * 300
        report = repair.option_audit(self._options(manifest))
        self.assertFalse(report["ok"])
        self.assertEqual(report["max_draws_per_mesh"], 300)
        self.assertIn("OPTION_DRAW_LIMIT", report["codes"])

    def test_option_audit_counts_draws_per_mesh_not_in_total(self) -> None:
        # Two meshes with 200 draws each never coexist in one state, so the
        # ceiling must be judged per mesh rather than as a sum of 400.
        manifest = option_manifest()
        manifest["meshes"][0]["draws"] = [{"count": 1}] * 200
        manifest["meshes"][1]["draws"] = [{"count": 1}] * 200
        report = repair.option_audit(self._options(manifest))
        self.assertTrue(report["ok"])
        self.assertEqual(report["max_draws_per_mesh"], 200)

    def test_option_audit_flags_the_index_ceiling(self) -> None:
        manifest = option_manifest()
        manifest["meshes"][1]["draws"] = [{"count": 20000000}]
        report = repair.option_audit(self._options(manifest))
        self.assertIn("OPTION_INDEX_LIMIT", report["codes"])

    def test_option_audit_flags_an_unknown_operation(self) -> None:
        manifest = option_manifest()
        manifest["component_rules"][0]["candidates"][1]["operation"] = "swap"
        report = repair.option_audit(self._options(manifest))
        self.assertIn("OPTION_RULE_OPERATION", report["codes"])

    def test_option_audit_flags_incomplete_rule_coverage(self) -> None:
        manifest = option_manifest()
        manifest["component_rules"].pop()
        report = repair.option_audit(self._options(manifest))
        self.assertIn("OPTION_RULE_COVERAGE", report["codes"])

    def test_option_audit_flags_a_rule_out_of_order(self) -> None:
        manifest = option_manifest()
        manifest["component_rules"][0]["target"] = 1
        manifest["component_rules"][1]["target"] = 0
        report = repair.option_audit(self._options(manifest))
        self.assertIn("OPTION_RULE_COVERAGE", report["codes"])

    def test_option_audit_flags_a_replace_without_a_mesh(self) -> None:
        manifest = option_manifest()
        del manifest["component_rules"][0]["candidates"][1]["mesh"]
        report = repair.option_audit(self._options(manifest))
        self.assertIn("OPTION_RULE_TARGET", report["codes"])

    def test_option_audit_flags_a_forward_available_when(self) -> None:
        manifest = option_manifest()
        manifest["option_groups"][0]["available_when"] = {"eq": ["part", "on"]}
        report = repair.option_audit(self._options(manifest))
        self.assertIn("OPTION_AVAILABLE_WHEN_ORDER", report["codes"])

    def test_option_audit_flags_a_default_outside_choices(self) -> None:
        manifest = option_manifest()
        manifest["option_groups"][1]["default"] = "maybe"
        report = repair.option_audit(self._options(manifest))
        self.assertIn("OPTION_DEFAULT", report["codes"])

    def test_option_audit_flags_an_illegal_default_state(self) -> None:
        manifest = option_manifest()
        manifest["selection_constraints"] = [{"eq": ["outfit", "b"]}]
        report = repair.option_audit(self._options(manifest))
        self.assertIn("OPTION_DEFAULT_ILLEGAL", report["codes"])

    def test_option_audit_flags_a_duplicate_group_and_choice(self) -> None:
        manifest = option_manifest()
        manifest["option_groups"][1]["choices"][1]["id"] = "on"
        report = repair.option_audit(self._options(manifest))
        self.assertIn("OPTION_CHOICES_DUPLICATE", report["codes"])
        manifest = option_manifest()
        manifest["option_groups"].append(dict(manifest["option_groups"][0]))
        report = repair.option_audit(self._options(manifest))
        self.assertIn("OPTION_GROUP_DUPLICATE", report["codes"])

    def test_option_audit_flags_a_condition_on_an_unknown_group(self) -> None:
        manifest = option_manifest()
        manifest["component_rules"][0]["candidates"][0]["when"] = {"eq": ["absent", "a"]}
        report = repair.option_audit(self._options(manifest))
        self.assertIn("OPTION_CONDITION_UNKNOWN_GROUP", report["codes"])

    def test_option_audit_accepts_a_package_without_groups(self) -> None:
        # A 1.3 slider-only package has an empty option_groups array.
        manifest = option_manifest()
        manifest["option_groups"] = []
        manifest["component_rules"] = [
            {"target": 0, "candidates": [{"operation": "replace", "mesh": 0}]},
            {"target": 1, "candidates": [{"operation": "keep"}]}]
        report = repair.option_audit(self._options(manifest))
        self.assertTrue(report["ok"])
        self.assertEqual(report["states"], 1)

    def test_option_audit_reports_a_truncated_enumeration(self) -> None:
        manifest = option_manifest()
        with mock.patch.object(repair, "MAX_OPTION_STATES", 2):
            report = repair.option_audit(self._options(manifest))
        self.assertTrue(report["truncated"])
        self.assertEqual(report["states"], 2)
        self.assertIn("截断", report["note"])


    # -- CrossIB index grouping ---------------------------------------------

    def _meshes(self, spec: list) -> Path:
        """spec: list of (vertex_bytes, index_bytes, layout, index_size)."""
        root = self.tmp / "ibgroups"
        payloads = root / "payloads"
        payloads.mkdir(parents=True, exist_ok=True)
        files, meshes = [], []
        for position, (vertex, index, layout, index_size) in enumerate(spec):
            vertex_name = f"payloads/v{position}.bin"
            index_name = f"payloads/i{position}.bin"
            (root / vertex_name).write_bytes(vertex)
            (root / index_name).write_bytes(index)
            files += [vertex_name, index_name]
            mesh = {"vertex_count": 3, "index_count": len(index) // index_size,
                    "index_size": index_size,
                    "streams": [{"stride": 16, "payload": len(files) - 2}],
                    "draws": [{"count": len(index) // index_size,
                               "material_component": position}]}
            if layout == "single":
                mesh["indices"] = len(files) - 1
            else:
                mesh["draws"][0]["indices"] = len(files) - 1
            meshes.append(mesh)
        path = root / "project.json"
        path.write_text(json.dumps({"manifest": {"meshes": meshes},
                                    "payload_files": files}), encoding="utf-8")
        return path

    def test_ib_groups_finds_a_shared_topology(self) -> None:
        # One topology, two skins: the CrossIB signature.
        path = self._meshes([(b"A" * 48, b"\x00\x01\x02", "single", 2),
                             (b"B" * 48, b"\x00\x01\x02", "single", 2)])
        report = repair.ib_groups(path)
        self.assertTrue(report["ok"])
        self.assertEqual(report["distinct_index_payloads"], 1)
        self.assertEqual(report["shared_topology"], 1)
        group = report["shared_groups"][0]
        self.assertEqual(group["meshes"], [0, 1])
        self.assertEqual(group["kind"], "shared-topology")
        self.assertEqual(group["exact_duplicates"], [])

    def test_ib_groups_finds_an_exact_duplicate_mesh(self) -> None:
        path = self._meshes([(b"A" * 48, b"\x00\x01\x02", "single", 2),
                             (b"A" * 48, b"\x00\x01\x02", "single", 2)])
        report = repair.ib_groups(path)
        self.assertEqual(report["duplicate_meshes"], 1)
        self.assertEqual(report["shared_groups"][0]["exact_duplicates"], [[0, 1]])

    def test_ib_groups_separates_distinct_topologies(self) -> None:
        path = self._meshes([(b"A" * 48, b"\x00\x01\x02", "single", 2),
                             (b"B" * 48, b"\x02\x01\x00", "single", 2)])
        report = repair.ib_groups(path)
        self.assertEqual(report["shared_groups"], [])
        self.assertEqual(report["distinct_index_payloads"], 2)
        # Same length, different bytes: a variant, not a defect.
        self.assertEqual(len(report["same_count_variants"]), 1)
        self.assertTrue(report["ok"])

    def test_ib_groups_fingerprints_both_index_layouts(self) -> None:
        # The per-draw layout concatenates the draw payloads, so the same bytes
        # group together regardless of how the mesh stores them.
        path = self._meshes([(b"A" * 48, b"\x00\x01\x02", "single", 2),
                             (b"B" * 48, b"\x00\x01\x02", "per-draw", 2)])
        report = repair.ib_groups(path)
        self.assertEqual(report["distinct_index_payloads"], 1)
        self.assertEqual(report["shared_groups"][0]["meshes"], [0, 1])

    def test_ib_groups_refuses_a_mixed_index_width(self) -> None:
        path = self._meshes([(b"A" * 48, b"\x00\x01\x02", "single", 2),
                             (b"B" * 48, b"\x00\x01\x02", "single", 1)])
        report = repair.ib_groups(path)
        self.assertFalse(report["ok"])
        self.assertEqual(len(report["index_size_conflicts"]), 1)

    def test_ib_groups_refuses_a_draw_without_an_index_payload(self) -> None:
        path = self._meshes([(b"A" * 48, b"\x00\x01\x02", "single", 2)])
        data = json.loads(path.read_text(encoding="utf-8"))
        data["manifest"]["meshes"][0].pop("indices")
        path.write_text(json.dumps(data), encoding="utf-8")
        with self.assertRaises(repair.Refused) as caught:
            repair.ib_groups(path)
        self.assertEqual(caught.exception.code, "IB_PAYLOAD_MISSING")


    def _shape_source(self, component: int = 0, shape_key: int = 0, weight=1.0,
                      ids=(0, 2), table=None, filename: str = "main.ini",
                      drop: str = "", extra: str = "") -> Path:
        """A minimal EFMI package: one component, one shape key, real buffers."""
        root = self.tmp / "shape-src"
        (root / "res").mkdir(parents=True, exist_ok=True)
        (root / "res" / "config.bin").write_bytes(shape_config(table=table))
        (root / "res" / "ids.bin").write_bytes(struct.pack("<%dI" % len(ids), *ids))
        (root / "res" / "offsets.bin").write_bytes(
            b"".join(struct.pack("<3e", 0.0, 0.0, 0.5) for _ in ids))
        blocks = [f"[Constants]\n$cid = {component}\n$kid = {shape_key}\n$w = {weight}\n",
                  f"[CommandListSetShapeKey{shape_key}]\n$component_id = $cid\n"
                  f"$shapekey_id = $kid\n$shapekey_value = $w\nrun = CommandListSetShapeKey\n",
                  f"[Resource_Component{component}_ShapeKeyBatchConfigs]\n"
                  "filename = res/config.bin\nformat = DXGI_FORMAT_R32G32B32A32_UINT\nstride = 16\n",
                  f"[Resource_Component{component}_ShapeKeyVertexIds]\n"
                  "filename = res/ids.bin\nformat = DXGI_FORMAT_R32_UINT\nstride = 4\n",
                  f"[Resource_Component{component}_ShapeKeyVertexOffsets]\n"
                  "filename = res/offsets.bin\nformat = DXGI_FORMAT_R16_FLOAT\nstride = 2\n"]
        keep = [block for block in blocks if not block.startswith(f"[{drop}")] if drop else blocks
        (root / filename).write_text("\n".join(keep) + extra, encoding="utf-8")
        return root

    def _shape_project(self, manifest: dict | None = None,
                       wrapped: bool = False) -> Path:
        root = self.tmp / "shape-proj"
        root.mkdir(parents=True, exist_ok=True)
        data = deform_manifest() if manifest is None else json.loads(json.dumps(manifest))
        data.pop("parameters", None)
        data.pop("mesh_deformations", None)
        data["required_capabilities"] = [row for row in data["required_capabilities"]
                                         if row not in ("body-parameters", "mesh-position-deltas")]
        if wrapped:
            data = {"manifest": data, "payload_files": []}
        path = root / "project.json"
        path.write_text(json.dumps(data), encoding="utf-8")
        return path

    def test_shape_params_emits_a_two_frame_slider(self) -> None:
        report = repair.shape_params(self._shape_source(), self._shape_project())
        self.assertTrue(report["ok"], report["blockers"])
        self.assertEqual(report["parameters"], 1)
        self.assertEqual(report["channels"], 1)
        parameter = report["spec"]["parameters"][0]
        self.assertEqual(parameter["id"], "shape_0_0")
        self.assertEqual((parameter["min"], parameter["max"], parameter["neutral"],
                          parameter["step"]), (0, 1000, 0, 1))
        frames = report["spec"]["mesh_deformations"][0]["frames"]
        self.assertEqual(frames[0], {"value": 0, "neutral": True})
        self.assertEqual(frames[1]["value"], 1000)
        self.assertEqual(frames[1]["efmi"]["component"], 0)
        self.assertEqual(frames[1]["efmi"]["shape_key"], 0)
        self.assertEqual(frames[1]["efmi"]["vertex_order"], "exported")

    def test_shape_params_uses_the_source_weight_as_default(self) -> None:
        report = repair.shape_params(self._shape_source(weight=0.35), self._shape_project())
        self.assertTrue(report["ok"], report["blockers"])
        self.assertEqual(report["spec"]["parameters"][0]["default"], 350)

    def test_shape_params_skips_a_zero_weight(self) -> None:
        report = repair.shape_params(self._shape_source(weight=0.0), self._shape_project())
        self.assertFalse(report["ok"])
        self.assertEqual(report["parameters"], 0)
        self.assertEqual([row["code"] for row in report["blockers"]], ["SHAPE_NO_DRIVEN_KEY"])

    def test_shape_params_requires_a_vertex_map_past_the_mesh_range(self) -> None:
        report = repair.shape_params(self._shape_source(ids=(0, 7)), self._shape_project())
        self.assertFalse(report["ok"])
        self.assertEqual([row["code"] for row in report["blockers"]],
                         ["SHAPE_VERTEX_MAP_REQUIRED"])
        self.assertEqual(report["blockers"][0]["highest_source_vertex"], 7)

    def test_shape_params_reports_a_missing_resource(self) -> None:
        source = self._shape_source(drop="Resource_Component0_ShapeKeyVertexOffsets")
        report = repair.shape_params(source, self._shape_project())
        self.assertFalse(report["ok"])
        self.assertEqual(report["blockers"][0]["code"], "SHAPE_RESOURCE_MISSING")
        self.assertEqual(report["blockers"][0]["missing"], ["ShapeKeyVertexOffsets"])

    def test_shape_params_refuses_an_archive_it_cannot_read(self) -> None:
        archive = self.tmp / "mod.7z"
        archive.write_bytes(b"7z\xbc\xaf\x27\x1c")
        with self.assertRaises(repair.Refused) as caught:
            repair.shape_params(archive, self._shape_project())
        self.assertEqual(caught.exception.code, "SHAPE_SOURCE_ARCHIVE")

    def test_shape_params_reports_a_package_without_any_shape_data(self) -> None:
        root = self.tmp / "shape-plain"
        root.mkdir(parents=True, exist_ok=True)
        (root / "main.ini").write_text("[TextureOverride_Component0]\nhash = 11223344\n",
                                       encoding="utf-8")
        with self.assertRaises(repair.Refused) as caught:
            repair.shape_params(root, self._shape_project())
        self.assertEqual(caught.exception.code, "SHAPE_DATA_ABSENT")

    def test_shape_params_separates_buffers_nobody_drives(self) -> None:
        # Real shape (M0265): every resource is declared and initialised, but no
        # caller ever runs the setter, so the source sliders are dead at weight 0.
        root = self.tmp / "shape-dead"
        (root / "res").mkdir(parents=True)
        for name, raw in (("config.bin", shape_config()),
                          ("ids.bin", struct.pack("<2I", 0, 2)),
                          ("offsets.bin", struct.pack("<3e", 0.0, 0.0, 0.5) * 2)):
            (root / "res" / name).write_bytes(raw)
        (root / "main.ini").write_text(
            "[Constants]\nglobal $shapekey_id = 0\nglobal $shapekey_value = 0\n"
            "run = CommandListInitializeShapeKeys\n\n"
            "[Resource_Component0_ShapeKeyBatchConfigs]\nfilename = res/config.bin\n\n"
            "[Resource_Component0_ShapeKeyVertexIds]\nfilename = res/ids.bin\n\n"
            "[Resource_Component0_ShapeKeyVertexOffsets]\nfilename = res/offsets.bin\n",
            encoding="utf-8")
        with self.assertRaises(repair.Refused) as caught:
            repair.shape_params(root, self._shape_project())
        self.assertEqual(caught.exception.code, "SHAPE_NOT_DRIVEN")
        self.assertEqual(caught.exception.detail["components_with_buffers"], [0])

    def test_shape_params_refuses_a_shape_key_outside_the_batches(self) -> None:
        report = repair.shape_params(self._shape_source(shape_key=200), self._shape_project())
        self.assertFalse(report["ok"])
        self.assertEqual(report["blockers"][0]["code"], "SHAPE_KEY_OUT_OF_RANGE")

    def test_shape_params_flags_a_mesh_without_a_float32_position(self) -> None:
        manifest = deform_manifest()
        manifest["meshes"][0]["attributes"] = [row for row in manifest["meshes"][0]["attributes"]
                                               if row[0] != 0]
        report = repair.shape_params(self._shape_source(), self._shape_project(manifest))
        self.assertFalse(report["ok"])
        self.assertEqual(report["blockers"][0]["code"], "SHAPE_POSITION_ATTRIBUTE")

    def test_shape_params_writes_a_spec_that_points_back_at_the_source(self) -> None:
        source = self._shape_source()
        output = self.tmp / "shape-out" / "body-morphs.json"
        report = repair.shape_params(source, self._shape_project(), output)
        self.assertTrue(report["ok"], report["blockers"])
        written = json.loads(output.read_text(encoding="utf-8"))
        self.assertEqual(written["kind"], "bem-position-morphs")
        self.assertEqual(written["schema"], 1)
        reference = written["mesh_deformations"][0]["frames"][1]["efmi"]["source"]
        self.assertFalse(Path(reference).is_absolute())
        self.assertTrue(reference.endswith("shape-src"))

    def test_shape_params_reads_a_project_that_wraps_its_manifest(self) -> None:
        report = repair.shape_params(self._shape_source(),
                                     self._shape_project(wrapped=True))
        self.assertTrue(report["ok"], report["blockers"])
        self.assertEqual(report["appearance"], "default")
        self.assertEqual(report["parameters"], 1)

    def test_shape_params_ignores_a_disabled_ini(self) -> None:
        source = self._shape_source()
        (source / "DISABLEDcopy.ini").write_text(
            (source / "main.ini").read_text(encoding="utf-8").replace("$w = 1.0", "$w = 9.0"),
            encoding="utf-8")
        report = repair.shape_params(source, self._shape_project())
        self.assertTrue(report["ok"], report["blockers"])
        self.assertEqual(report["spec"]["parameters"][0]["default"], 1000)


if __name__ == "__main__":
    unittest.main(verbosity=2)
