"""The two compiled-in baseline tables must stay identical to the declared map.

Windows reads the C# copy and the Android module reads the C++ copy, so a batch
added to only one of them would silently stop backfilling on that platform.
"""
import json
import re
import unittest
from pathlib import Path

ROOT = Path(__file__).resolve().parents[2]
SOURCE = ROOT / 'config' / 'game-baseline-map.json'
CSHARP = ROOT / 'ui' / 'BetterEndfieldNext.UI' / 'Services' / 'BemBaselineTable.Generated.cs'
CPP = ROOT / 'native' / 'modules' / 'custom_model' / 'bem_baseline_table.generated.h'


def declared():
    data = json.loads(SOURCE.read_text(encoding='utf-8-sig'))
    return [(batch['revision'], batch['windows-x64'], batch['android-arm64'], int(batch['hotfix']))
            for batch in data['batches']]


class BaselineTableTests(unittest.TestCase):
    def test_generated_tables_match_the_declared_batches(self):
        expected = declared()
        self.assertTrue(expected, 'the batch map must declare at least one revision')
        csharp = re.findall(r'new\("([^"]*)", "([^"]*)", "([^"]*)", (-?\d+)\)', CSHARP.read_text(encoding='utf-8'))
        cpp = re.findall(r'\{"([^"]*)", "([^"]*)", "([^"]*)", (-?\d+)\}', CPP.read_text(encoding='utf-8'))
        for name, rows in (('C#', csharp), ('C++', cpp)):
            with self.subTest(generated=name):
                self.assertEqual([(r, w, a, int(h)) for r, w, a, h in rows], expected)

    def test_every_batch_declares_both_platforms_and_a_counter(self):
        for revision, windows, android, hotfix in declared():
            with self.subTest(revision=revision):
                self.assertTrue(windows and android)
                self.assertGreaterEqual(hotfix, 0)


if __name__ == '__main__':
    unittest.main()
