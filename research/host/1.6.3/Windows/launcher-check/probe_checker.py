"""Call the installed launch checker in an isolated process and log directory.

The config is an explicit test fixture, not a replacement for game configuration.
This calls only exports 1 (check) and 2 (result); it never calls export 3 (launch).
"""
import argparse
import ctypes
import json
import os
from pathlib import Path


def main():
    parser = argparse.ArgumentParser()
    parser.add_argument("--dll", type=Path, required=True)
    parser.add_argument("--config", type=Path, required=True)
    parser.add_argument("--output", type=Path, required=True)
    args = parser.parse_args()
    output = args.output.resolve()
    output.parent.mkdir(parents=True, exist_ok=True)
    user_data = output.parent / (output.stem + "-userdata")
    (user_data / "sdklogs").mkdir(parents=True, exist_ok=True)
    fixture = json.loads(args.config.read_text(encoding="utf-8-sig"))
    items = fixture.get("checkItems")
    if not isinstance(items, list) or not items:
        raise ValueError("Explicit nonempty checkItems fixture required")
    for item in items:
        if (item.get("name") != "Launcher" or not item.get("parentNames")
                or not item.get("fingerprints")):
            raise ValueError("Fixture must include Launcher, parentNames, fingerprints")

    dll_path = args.dll.resolve(strict=True)
    with os.add_dll_directory(str(dll_path.parent)):
        library = ctypes.WinDLL(str(dll_path))
        check = library[1]
        check.argtypes = [ctypes.c_char_p, ctypes.c_char_p]
        check.restype = ctypes.c_uint32
        get_result = library[2]
        get_result.argtypes = []
        get_result.restype = ctypes.c_char_p
        code = check(str(user_data).encode("utf-8"),
                     json.dumps(fixture, ensure_ascii=False).encode("utf-8"))
        raw = (get_result() or b"").decode("utf-8", errors="replace")
        try:
            result = json.loads(raw)
        except ValueError:
            result = raw
        record = {"pid": os.getpid(), "parent_pid": os.getppid(),
                  "checker_return": code, "result": result,
                  "fixture": str(args.config.resolve()),
                  "user_data": str(user_data)}
        output.write_text(json.dumps(record, ensure_ascii=False, indent=2) + "\n",
                          encoding="utf-8")
        print(json.dumps(record, ensure_ascii=False))
    return 0 if code == 0 else 1


if __name__ == "__main__":
    raise SystemExit(main())
