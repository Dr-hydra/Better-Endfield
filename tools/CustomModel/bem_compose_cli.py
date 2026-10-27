"""Thin authoring CLI adapter; all format logic belongs to CustomModelCore."""
import json
import os
from pathlib import Path
import subprocess
import sys

def main(argv):
    executable = 'BetterEndfieldNext.BemCompose.exe'
    root = Path(__file__).resolve().parents[2]
    candidates = ([Path(os.environ['BEM_COMPOSER'])] if os.environ.get('BEM_COMPOSER') else [])
    candidates.extend([
        Path(sys.executable).resolve().parent.parent / 'BemCompose' / executable,
        root / 'tools' / 'BemCompose' / executable,
        root / 'build' / 'bem-compose' / 'stage' / 'Release' / 'tools' / 'BemCompose' / executable,
    ])
    tool = next((p for p in candidates if p.is_file()), None)
    if tool is None:
        print(json.dumps(dict(success=False, error='BEM composer is missing. Build BetterEndfieldNext.BemCompose or set BEM_COMPOSER.'), ensure_ascii=False))
        return 2
    return subprocess.call([str(tool), *argv])
