"""Check developer paths and local dependency availability without running tools."""
import argparse
import json
import sys
from pathlib import Path
from workspace_config import load_workspace


def main():
    parser=argparse.ArgumentParser(description=__doc__)
    parser.add_argument('--workspace-config',type=Path)
    parser.add_argument('--json',action='store_true')
    args=parser.parse_args();ws=load_workspace(args.workspace_config)
    required=['resource_update.input_root','resource_update.catalog_root','resource_update.walk_clip_metadata','resource_update.table_root','resource_update.json_root','resource_update.bank_root','resource_update.legacy_lod_catalog','tools.android_sdk','tools.android_dobby','tools.archive_backend','tools.native_asset_reader']
    paths=[{'key':key,'path':str(ws.path(key)),'exists':ws.path(key).exists()}for key in required]
    optional=['tools.resconv','game.install_dir','test.be_install_dir']
    paths += [{'key':key,'path':str(ws.path(key,required=False))if ws.path(key,required=False)else None,'exists':bool(ws.path(key,required=False)and ws.path(key,required=False).exists())}for key in optional]
    docs=['BEM_CREATOR_GUIDE.md','BEM_FORMAT_SPEC.md','BEM_RUNTIME_COMPATIBILITY.md','BEM_SOURCE_MOD_CONVERSION.md','THIRD_PARTY_MODULE_CREATOR_GUIDE.md']
    documents=[{'name':name,'path':str(ws.document(name)),'exists':ws.document(name).is_file()}for name in docs]
    result={'configuration_valid':True,'game_version':ws.get('game.version'),'game_discovery_source':ws.get('game.discovery_source'),'cleanup_targets':[str(p)for p in ws.clean_targets()],'paths':paths,'documents':documents,'no_tools_executed':True,'build_and_game_validation':'not_run'}
    print(json.dumps(result,ensure_ascii=False,indent=2))
    return int(any(not row['exists']for row in paths if row['key']in required)or any(not d['exists']for d in documents))


if __name__=='__main__':raise SystemExit(main())
