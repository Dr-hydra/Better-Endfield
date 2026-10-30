#!/usr/bin/env python3
"""Verify the published distribution index matches the latest software release.

PublishRelease.py refreshes the index after publishing, but a release created
another way (for example the GitHub CLI) leaves the index pointing at the
previous version and installed clients never see the update. Run this after
any software release; a non-zero exit means the index is stale or inconsistent.
"""
import argparse
import json
import sys
from pathlib import Path
import urllib.request

sys.path.insert(0, str(Path(__file__).resolve().parent))
from PublishRelease import credentials  # noqa: E402

REPOSITORY = "Dr-hydra/Better-Endfield"
RESOURCE_TAG = "game-resources"
INDEX_URL = f"https://github.com/{REPOSITORY}/releases/download/{RESOURCE_TAG}/distribution-index.json"


def fetch_json(url, token):
    request = urllib.request.Request(url, headers={
        "Authorization": "Bearer " + token,
        "User-Agent": "Better-Endfield-release-tool",
        "Accept": "application/vnd.github+json",
    })
    with urllib.request.urlopen(request, timeout=60) as response:
        return json.load(response)


def fetch_bytes(url):
    request = urllib.request.Request(url, headers={"User-Agent": "Better-Endfield-release-tool"})
    with urllib.request.urlopen(request, timeout=60) as response:
        return response.read()


def main():
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--tag", help="Release tag to compare against; defaults to the latest stable release")
    args = parser.parse_args()

    token = credentials()
    if args.tag:
        release = fetch_json(f"https://api.github.com/repos/{REPOSITORY}/releases/tags/{args.tag}", token)
    else:
        release = fetch_json(f"https://api.github.com/repos/{REPOSITORY}/releases/latest", token)
    if release["tag_name"] == RESOURCE_TAG or release["draft"] or release["prerelease"]:
        raise SystemExit("Expected a stable software release, got " + release["tag_name"])

    index = json.loads(fetch_bytes(INDEX_URL))
    if index.get("schemaVersion") != 1 or index.get("kind") != "betterendfield-distribution-index":
        raise SystemExit("Published index has an unsupported schema")
    problems = []
    software = index.get("software", {})
    expected_version = release["tag_name"].removeprefix("v")
    if software.get("version") != expected_version:
        problems.append(f"software.version is {software.get('version')!r}, release tag is {release['tag_name']!r}")
    if software.get("releaseUrl") != release["html_url"]:
        problems.append("software.releaseUrl does not point at " + release["html_url"])
    released = {asset["name"]: (asset["size"], asset["browser_download_url"]) for asset in release["assets"]}
    indexed = {asset["name"]: (asset["bytes"], asset["url"]) for asset in software.get("assets", [])}
    for name in sorted(set(released) ^ set(indexed)):
        problems.append("asset list differs: " + name)
    for name in sorted(set(released) & set(indexed)):
        if released[name][0] != indexed[name][0]:
            problems.append(f"{name}: {indexed[name][0]} bytes in the index, {released[name][0]} on the release")
        if released[name][1] != indexed[name][1]:
            problems.append(f"{name}: index url does not point at the release asset")
    for tool, entry in index.get("tools", {}).items():
        for asset in entry.get("assets", []):
            if f"/releases/download/{release['tag_name']}/" not in asset["url"]:
                problems.append(f"tools.{tool}: {asset['name']} url does not point at {release['tag_name']}")

    print(json.dumps({
        "release": release["tag_name"],
        "indexSoftware": software.get("version"),
        "resources": len(index.get("resources", [])),
        "assets": len(indexed),
        "consistent": not problems,
        "problems": problems,
    }, ensure_ascii=False, indent=2))
    if problems:
        raise SystemExit(1)


if __name__ == "__main__":
    main()
