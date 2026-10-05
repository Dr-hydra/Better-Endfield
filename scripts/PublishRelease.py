"""Publish an explicitly requested release after uploading and verifying its assets."""
import argparse
import hashlib
import http.client
import json
import mimetypes
import os
from pathlib import Path
import subprocess
import urllib.error
import urllib.parse
import urllib.request


def credentials():
    token = os.environ.get("GH_TOKEN") or os.environ.get("GITHUB_TOKEN")
    if token:
        return token
    env = os.environ.copy()
    env.update(GIT_TERMINAL_PROMPT="0", GCM_INTERACTIVE="Never")
    result = subprocess.run(["git", "credential", "fill"], input="protocol=https\nhost=github.com\n\n",
                            text=True, capture_output=True, env=env, check=True)
    values = dict(line.split("=", 1) for line in result.stdout.splitlines() if "=" in line)
    if not values.get("password"):
        raise RuntimeError("GitHub credentials are unavailable")
    return values["password"]


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--repo", required=True)
    parser.add_argument("--tag", required=True)
    parser.add_argument("--target", required=True)
    parser.add_argument("--name", required=True)
    parser.add_argument("--notes-file", type=Path, required=True)
    parser.add_argument("--asset", nargs=2, action="append", metavar=("NAME", "PATH"), required=True)
    parser.add_argument("--publish", action="store_true")
    args = parser.parse_args()
    token = credentials()
    base = "https://api.github.com/repos/" + args.repo
    headers = {"Authorization": "Bearer " + token, "Accept": "application/vnd.github+json",
               "X-GitHub-Api-Version": "2022-11-28", "User-Agent": "Better-Endfield-release-tool"}

    def api(url, method="GET", body=None):
        payload = None if body is None else json.dumps(body).encode()
        request = urllib.request.Request(url, payload, {**headers, "Content-Type": "application/json"}, method=method)
        with urllib.request.urlopen(request, timeout=60) as response:
            return json.load(response) if response.status != 204 else None

    assets = [(name, Path(path).resolve()) for name, path in args.asset]
    if len({name for name, _ in assets}) != len(assets) or any(not path.is_file() for _, path in assets):
        raise ValueError("Release asset names must be unique and all files must exist")
    body = args.notes_file.read_text(encoding="utf-8-sig")
    try:
        release = api(base + "/releases/tags/" + urllib.parse.quote(args.tag, safe=""))
    except urllib.error.HTTPError as error:
        if error.code != 404:
            raise
        release = api(base + "/releases", "POST", {"tag_name": args.tag, "target_commitish": args.target,
                      "name": args.name, "body": body, "draft": True, "prerelease": False})
    if not release["draft"]:
        raise RuntimeError("Refusing to replace assets of an already published release")
    release = api(release["url"], "PATCH", {"name": args.name, "body": body, "target_commitish": args.target})
    upload = urllib.parse.urlsplit(release["upload_url"].split("{", 1)[0])
    if upload.scheme != "https" or upload.hostname != "uploads.github.com":
        raise RuntimeError("Unexpected GitHub upload endpoint")
    expected = {}
    for name, path in assets:
        size = path.stat().st_size
        digest = hashlib.file_digest(path.open("rb"), "sha256").hexdigest()
        expected[name] = (size, "sha256:" + digest)
        existing = next((asset for asset in release["assets"] if asset["name"] == name), None)
        if existing and (existing["size"], existing.get("digest")) == expected[name]:
            print("Verified existing draft asset:", name, flush=True)
            continue
        if existing:
            api(existing["url"], "DELETE")
        connection = http.client.HTTPSConnection(upload.hostname, timeout=120)
        try:
            connection.putrequest("POST", upload.path + "?" + urllib.parse.urlencode({"name": name}))
            for key, value in {**headers, "Content-Type": mimetypes.guess_type(name)[0] or "application/octet-stream",
                               "Content-Length": str(size)}.items():
                connection.putheader(key, value)
            connection.endheaders()
            with path.open("rb") as stream:
                while chunk := stream.read(1024 * 1024):
                    connection.send(chunk)
            response = connection.getresponse()
            result = json.loads(response.read())
            if response.status != 201 or result.get("state") != "uploaded":
                raise RuntimeError(f"GitHub asset upload failed: HTTP {response.status}")
        finally:
            connection.close()
        print("Uploaded:", name, size, flush=True)
    release = api(release["url"])
    for name, signature in expected.items():
        asset = next((a for a in release["assets"] if a["name"] == name), None)
        if not asset or asset["state"] != "uploaded" or (asset["size"], asset.get("digest")) != signature:
            raise RuntimeError("Uploaded asset verification failed: " + name)
    if args.publish:
        release = api(release["url"], "PATCH", {"draft": False, "prerelease": False, "make_latest": "true"})
    print(json.dumps({"url": release["html_url"], "tag": release["tag_name"], "draft": release["draft"],
                      "assets": [{"name": a["name"], "bytes": a["size"], "digest": a.get("digest")}
                                 for a in release["assets"]]}, ensure_ascii=False, indent=2))


if __name__ == "__main__":
    main()
