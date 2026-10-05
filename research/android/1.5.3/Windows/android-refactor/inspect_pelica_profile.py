from pathlib import Path
import json,pprint
for p in ['tools/CustomModel/profiles/pelica-mod_7b260.source.json','tools/CustomModel/profiles/pelica-mod_7b260.mapping.json']:
 d=json.loads(Path(p).read_text(encoding='utf-8-sig')); print('\n',p); pprint.pp(d['components'])
