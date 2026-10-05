"""Read back actual extension bytes, run native checks, and stage review samples."""
import json
from pathlib import Path
import shutil
import struct
import subprocess
from audit_inputs import REPO, ROOT, SOURCES, sections, value
import bem_v1 as bem
from bem_tool import check_geometry


def validate(task):
    report_path=ROOT/f'{task}-conversion-report.json'
    report=json.loads(report_path.read_text(encoding='utf-8'))
    package=Path(report['output']); manifest,payloads=bem.read_package(package)
    check_geometry(manifest,payloads,4)
    ini=sections((SOURCES[task]/'mod.ini').read_text(encoding='utf-8-sig'))
    records=[]
    for proof in report['geometry']:
        source=proof['source_component']; expected=[]
        for stream in range(3):
            filename=value(ini[f'Resource_Component{source}_VB{stream}'],'filename')
            expected.append((SOURCES[task]/filename).read_bytes())
        raw_ib=(SOURCES[task]/value(ini[f'Resource_Component{source}_IB'],'filename')).read_bytes()
        matched=[]
        for mesh in manifest['meshes']:
            component=manifest['target']['components'][mesh['bones'][0]['component']]
            if component['mesh_name']!=proof['native_mesh']: continue
            assert [payloads[s['payload']] for s in mesh['streams']]==expected
            assert [b['name'] for b in mesh['bones']]==component['bone_names']
            assert all(b['component']==component['id'] and b['index']==i for i,b in enumerate(mesh['bones']))
            slots=len(component['materials']); assert len(mesh['draws'])==slots*len(proof['source_draws'])
            for index,draw in enumerate(mesh['draws']):
                source_draw=proof['source_draws'][index//slots]; start=source_draw['start']; count=source_draw['count']
                assert draw['material_component']==component['id'] and draw['material_slot']==index%slots
                assert payloads[draw['indices']]==raw_ib[start*mesh['index_size']:(start+count)*mesh['index_size']]
                assert draw['when']==source_draw['when']
            matched.append(component['resource'])
        assert sorted(matched)==['ability','ultimate']
        records.append(dict(source_component=source,resources=matched,streams_byte_identical=True,
                            all_draw_indices_byte_identical=True,all_native_material_slots_preserved=True,
                            donor_indices_names_order_preserved=True))
    validator=REPO/'build/bem14/native/modules/custom_model/Release/BetterEndfield.BemValidate.exe'
    command=[str(validator),str(package),'--compare-loading']
    groups=manifest['option_groups']
    if groups:
        command+=['--options','&'.join(g['id']+':'+g['default'] for g in groups)]
        command+=['--options','&'.join(g['id']+':'+g['choices'][-1]['id'] for g in groups)]
    log=ROOT/f'{task}-native-validation.log'
    with log.open('wb') as output:
        result=subprocess.run(command,stdout=output,stderr=subprocess.STDOUT,check=False)
    assert result.returncode==0,log.read_text(encoding='utf-8',errors='replace')[-2000:]
    summary=dict(task=task,native_returncode=result.returncode,native_log=str(log),
                 native_options_sampled=2 if groups else 1,full_python_option_combinations=report['all_option_combinations_checked'],
                 native_loading_modes_equal=True,actual_written_geometry=records,
                 source_coverage_complete=report['source_coverage_complete'],runtime_verified=False,render_verified=False)
    (ROOT/f'{task}-readback-validation.json').write_text(json.dumps(summary,ensure_ascii=False,indent=2),encoding='utf-8')
    sample_dir=REPO/'build/bem14/samples';sample_dir.mkdir(exist_ok=True,parents=True)
    sample=sample_dir/f'{task}-zhuangfy-ultimate-extension-Windows-LOD0.bem'
    shutil.copyfile(package,sample)
    sidecar=dict(package=sample.name,package_id=manifest['package_id'],source=str(SOURCES[task]),platform='windows-x64',lod=0,
                 resources=[r['name'] for r in manifest['target']['resources']],mirror='unchanged',
                 original_package='unchanged; separate ID, compatible ordinary-form resource scope',
                 option_groups=report['option_groups'],option_combinations=report['all_option_combinations_checked'],
                 source_coverage_complete=report['source_coverage_complete'],unapplied_active_textures=report['unmapped_active_textures'],
                 retained_original_base_textures=report['retained_original_base_textures'],
                 native_validation='passed; default/last options and both loading modes',
                 runtime_verified=False,render_verified=False,artifact_hashes_computed=False)
    sample.with_suffix('.report.json').write_text(json.dumps(sidecar,ensure_ascii=False,indent=2),encoding='utf-8')
    print(json.dumps(dict(task=task,sample=str(sample),bytes=sample.stat().st_size,native_returncode=0,readback_components=len(records))))
    return summary


if __name__=='__main__':
    for task in SOURCES:validate(task)
