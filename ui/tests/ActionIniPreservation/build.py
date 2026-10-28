"""Build the standalone Actions INI regression and emit its workspace receipt.

Run on Windows with the workspace .NET 9 SDK configured. The suite runner
only executes receipted builds; it never builds this project implicitly.
"""
from datetime import datetime, timezone
import json
from pathlib import Path
import subprocess
import sys

ROOT = Path(__file__).resolve().parents[3]
sys.path.insert(0, str(ROOT / "scripts"))
from workspace_config import load_workspace
from run_workspace_tests import safe_output, source_record


def main():
    ws = load_workspace()
    suite = next(item for item in json.loads((ROOT / "config/test-suites.json").read_text(encoding="utf-8"))["suites"]
                 if item["id"] == "actions.ini_preservation")
    entry = suite["entry"]
    output = safe_output(ws, ws.path("paths.build", "next"))
    source = source_record(ROOT)
    command = [ws.command("tools.dotnet"), "build", str(Path(__file__).with_name("ActionIniPreservation.csproj")),
               "-c", "Release", "-p:Platform=AnyCPU", f"-p:BEWorkspaceBuildRoot={output}"]
    subprocess.run(command, cwd=ROOT, check=True)
    if source_record(ROOT)["code_revision"] != source["code_revision"]:
        raise RuntimeError("Source changed during build; no receipt written")
    artifacts = []
    for name in entry["artifacts"]:
        stat = safe_output(ws, ws.path("paths.build", name)).stat()
        artifacts.append({"path": name, "size": stat.st_size, "mtime_ns": stat.st_mtime_ns})
    receipt = dict(schema_version=1, suite_id=suite["id"], source_commit=source["commit"],
                   code_revision=source["code_revision"], build_succeeded=True, configuration="Release",
                   tool_revision=subprocess.check_output([command[0], "--version"], text=True).strip(),
                   build_command=command, built_at=datetime.now(timezone.utc).isoformat(), artifacts=artifacts)
    target = safe_output(ws, ws.path("paths.build", entry["receipt"]))
    target.parent.mkdir(parents=True, exist_ok=True)
    target.write_text(json.dumps(receipt, indent=2) + "\n", encoding="utf-8")


if __name__ == "__main__":
    main()
