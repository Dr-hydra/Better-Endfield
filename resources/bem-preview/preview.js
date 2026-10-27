/* Offline installed-package preview. Draft edits persist only after explicit save. */
'use strict';
const composition=new URLSearchParams(location.search).get('mode')==='composition';
const english=new URLSearchParams(location.search).get('lang')==='en';
const t=(zh,en)=>english?en:zh;
const $=id=>document.getElementById(id);
$('reload').textContent=t('还原已保存','Restore saved');$('save').textContent=t('保存设置','Save settings');$('partsLabel').textContent=t('临时显隐 / 单独查看','Temporary visibility / isolate');$('reset').textContent=t('重置视角','Reset camera');
$('textureLabel').textContent=t('贴图','Textures');$('resourceLabel').textContent=t('预览资源','Resource');
$('notice').textContent=t('仅显示包内替换网格。这里的显隐仅用于查看，不会保存；包内选项在上方切换。点击名称可单独查看。','Replacement meshes only. Visibility here is temporary; edit package options above to save. Click a name to isolate.');
if(composition){$('reload').textContent=t('还原组合默认值','Restore composition defaults');$('save').textContent=t('应用到组合','Apply to composition')}
$('detailsLabel').textContent=t('配置与诊断','Settings and diagnostics');
let stage,decoder,awaitingHost=false,disposed=false,loading=false,saving=false,valid=false,resource='',sceneVersion=0,sceneId='',draft=null,saved='',controls=null,loadSaved=true,refreshPending=false,refreshPreserveCamera=true;
const decode=value=>new Map((value||'').split('&').filter(Boolean).map(pair=>{const split=pair.indexOf(':');return [pair.slice(0,split),pair.slice(split+1)]}));
const encode=values=>Array.from(values,([id,value])=>id+':'+value).join('&');
const signature=()=>JSON.stringify(draft);
const gpu=BemRenderer.capabilities();let gpuFallbackPending=false;
let highlightedSegment=null,highlightedSegments=new Set(),inspection=false,bodyEditDraws=new Set();
window.bemPreviewBodyEdit=draws=>{
 bodyEditDraws=new Set(draws||[]);if(stage)stage.bodyEditDraws=new Set(bodyEditDraws);
 if(!bodyEditDraws.size&&inspection)return;
 $('notice').textContent=bodyEditDraws.size?(window.matchMedia?.('(pointer: coarse)')?.matches?t('点按修正 · 拖动旋转 · 双指平移/缩放','Tap to assign · Drag to orbit · Two fingers: pan/pinch'):t('身体区域修正：按住 Ctrl 点击三角形，归入左侧选定区域；普通拖动可旋转。','Body region editing: Ctrl-click a triangle to assign it to the region selected on the left. Drag normally to orbit.')):normalNotice;
};
const normalNotice=$('notice').textContent;
window.bemPreviewHighlightSegments=(ids,inspect=false)=>{
 highlightedSegments=new Set(ids||[]);highlightedSegment=null;inspection=!!inspect;
 if(stage){stage.highlightSegment=null;stage.highlightSegments=new Set(highlightedSegments)}
 $('notice').textContent=inspection?t('临时高亮查看：当前部件只显示所选段，未启用的段也可查看；不改变组合配置，退出高亮后恢复。源包条件仍然有效。','Temporary inspection: the current component shows selected segments, including disabled ones, without changing the composition. Exit highlight to restore. Source conditions still apply.'):normalNotice;
 if(inspection)$('saveStatus').textContent=t('高亮仅用于查看，不会保存','Highlight is temporary and cannot be saved');
 updateControls();
};
window.bemPreviewHighlightSegment=id=>{
 highlightedSegment=highlightedSegment===id?null:id;
 if(stage){stage.highlightSegment=highlightedSegment;if(highlightedSegment&&!stage.meshes.some(m=>m.draws.some(d=>d.segment===highlightedSegment)))status(t('该绘制段在当前来源或选项下未显示。','This draw segment is not visible with the current source/options.'));else stage.reportStatus()}
};
function busy(){return loading||saving}
function updateControls(){
 $('reload').disabled=busy();$('resource').disabled=busy();
 $('save').disabled=busy()||inspection||!valid||!draft||signature()===saved;
 for(const input of $('options').querySelectorAll('select,input'))input.disabled=busy()||inspection;
}
function evaluate(condition,active){
 if(typeof condition==='boolean')return condition;
 if(condition.eq)return active.get(condition.eq[0])===condition.eq[1];
 if(condition.all)return condition.all.every(c=>evaluate(c,active));
 if(condition.any)return condition.any.some(c=>evaluate(c,active));
 if(condition.not!==undefined)return !evaluate(condition.not,active);
 return false;
}
function renderControls(){
 if(!controls||!draft)return;
 const panel=$('options');panel.replaceChildren();const selected=decode(draft.selection),parameters=decode(draft.parameters),active=new Map();
 function selector(title,items,value,change){
  const label=document.createElement('label');label.className='setting';const text=document.createElement('span');text.textContent=title;text.title=title;
  const select=document.createElement('select');select.setAttribute('aria-label',title);for(const item of items)select.add(new Option(item.name||item.id,item.id));select.value=value;
  select.onchange=()=>change(select.value);label.append(text,select);panel.append(label);
 }
 function edit(){valid=false;$('saveStatus').textContent=t('未保存 · 正在更新预览','Unsaved · Updating preview');renderControls();request(resource)}
 if(controls.composable){
  for(const group of controls.optionGroups){
   if(group.available_when!==undefined&&!evaluate(group.available_when,active))continue;
   active.set(group.id,selected.get(group.id));
   selector(group.name||group.id,group.choices,selected.get(group.id),value=>{selected.set(group.id,value);draft.selection=encode(selected);edit()});
  }
 }else if(controls.appearances.length){
  selector(t('外观','Appearance'),controls.appearances,draft.selection,value=>{draft.selection=value;edit()});
 }
 for(const parameter of controls.parameters){
  if(parameter.available_when!==undefined&&!evaluate(parameter.available_when,active))continue;
  const label=document.createElement('label');label.className='setting';const title=document.createElement('span');title.textContent=parameter.name||parameter.id;title.title=title.textContent;
  const row=document.createElement('div');row.className='slider';const input=document.createElement('input');input.type='range';input.min=parameter.min;input.max=parameter.max;input.step=parameter.step;input.value=parameters.get(parameter.id)??parameter.default;input.setAttribute('aria-label',title.textContent);
  const value=document.createElement('output');const display=()=>value.textContent=(Number(input.value)/10).toFixed(1)+'%';display();input.oninput=display;
  input.onchange=()=>{parameters.set(parameter.id,input.value);draft.parameters=encode(parameters);edit()};row.append(input,value);label.append(title,row);panel.append(label);
 }
 updateControls();
}
window.bemPreviewSaved=function(message){saving=false;saved=signature();$('saveStatus').textContent=message||t('已保存','Saved');updateControls()};
window.bemPreviewSaveError=function(message){saving=false;$('saveStatus').textContent=t('保存失败：','Save failed: ')+message;updateControls()};

function status(message){$('status').textContent=message}
function notify(message){if(window.chrome?.webview)window.chrome.webview.postMessage(message);else if(window.BemHost)window.BemHost.request(JSON.stringify(message))}
function request(next='',restore=false){if(busy())return;loading=true;awaitingHost=true;valid=false;loadSaved=restore||!draft;updateControls();status(t('正在读取模型…','Loading model…'));notify({type:'load',resource:next,gpu,...(!loadSaved?{selection:draft.selection,parameters:draft.parameters}:{})})}
window.bemPreviewError=function(message){awaitingHost=false;loading=false;valid=false;updateControls();$('saveStatus').textContent=t('配置未应用，请调整选项或还原已保存','Settings not applied. Adjust options or restore saved settings');status(t('预览更新失败：','Preview update failed: ')+message)};
window.bemPreviewLoad=async function(id){
 awaitingHost=false;
 const version=++sceneVersion;
 try{
  if(disposed)return;
  const response=await fetch('/data/'+encodeURIComponent(id)+'/scene.json');if(!response.ok)throw Error('scene '+response.status);
  const scene=await response.json();if(disposed||version!==sceneVersion)return;
  const camera=stage&&resource===scene.resource?{...stage.camera,target:[...stage.camera.target]}:null;
  if(!stage){
   decoder=new BemRenderer.Decoder();stage=new BemRenderer.Stage($('canvas'),decoder);
   $('canvas').addEventListener('webglcontextlost',event=>{event.preventDefault();status(t('图形上下文已丢失，请刷新预览。','Graphics context lost. Refresh preview.'))});
   $('canvas').addEventListener('webglcontextrestored',()=>{stage?.dispose(false);decoder?.dispose();stage=null;decoder=null;loading=false;awaitingHost=false;request(resource)});
  }
  stage.onCompressedFailure=()=>{if(disposed||version!==sceneVersion)return;gpu.bc7=false;gpu.astc=false;gpuFallbackPending=true;if(!loading){gpuFallbackPending=false;request(resource)}};
  stage.onStatus=state=>{if(disposed||version!==sceneVersion||!valid)return;status(state.pending?t('正在加载贴图：','Loading textures: ')+state.pending:state.errors.size?t('部分贴图不可用，已显示素模','Some textures unavailable; showing untextured meshes'):((window.matchMedia?.('(pointer: coarse)')?.matches) ? t('单指旋转 · 双指平移/缩放','One finger: orbit · Two fingers: pan/pinch') : t('左键旋转 · 右键/中键平移 · 滚轮缩放','Left drag: orbit · Right/middle drag: pan · Wheel: zoom')))};
  sceneId=id;controls=scene.controls;draft={selection:scene.selection,parameters:scene.parameters};if(loadSaved)saved=signature();renderControls();
  $('name').textContent=scene.package.name;$('name').title=scene.package.name;resource=scene.resource;
  $('resource').replaceChildren();for(const r of scene.resources){const option=new Option(r.name||r.id,r.id);option.title=r.path;$('resource').add(option)}
  $('resource').value=resource;$('resource').hidden=scene.resources.length===0;
  await stage.load(scene,id);if(disposed||version!==sceneVersion)return;
  stage.highlightSegment=highlightedSegment;stage.highlightSegments=new Set(highlightedSegments);
  stage.bodyEditDraws=new Set(bodyEditDraws);
  stage.onBodyTrianglePicked=hit=>{if(valid&&!loading&&!inspection&&bodyEditDraws.has(hit.bodyDraw))notify({type:'bodyPick',id:sceneId,...hit})};
  if(camera)stage.camera=camera;
  stage.setTextureMode($('texture').checked);valid=true;$('saveStatus').textContent=signature()===saved?(composition?t('当前组合默认值','Current composition defaults'):t('已保存的设置','Saved settings')):(composition?t('未应用 · 点击应用到组合','Not applied · Click Apply to composition'):t('未保存 · 点击保存设置应用','Unsaved · Click Save settings to apply'));
  if(inspection){const shown=new Set(stage.meshes.flatMap(mesh=>mesh.draws.map(draw=>draw.segment)));const count=[...highlightedSegments].filter(id=>shown.has(id)).length;$('saveStatus').textContent=t('临时高亮：','Temporary highlight: ')+count+'/'+highlightedSegments.size+t(' 段可见；其余受源包选项控制。不会保存。',' segments visible; others are controlled by source options. Cannot be saved.')}
  $('parts').replaceChildren();const visible=new Set(scene.meshes.map(m=>m.index));let isolated=null;
  function apply(){stage.setVisibility(isolated===null?new Set(visible):new Set([isolated]),{})}
  for(const mesh of scene.meshes){const row=document.createElement('label');row.className='part';const check=document.createElement('input');check.type='checkbox';check.checked=true;check.onchange=()=>{check.checked?visible.add(mesh.index):visible.delete(mesh.index);isolated=null;apply()};const name=document.createElement('span');name.textContent=mesh.name;name.title=mesh.name;name.onclick=event=>{event.preventDefault();isolated=isolated===mesh.index?null:mesh.index;apply()};row.append(check,name);$('parts').append(row)}
  $('details').textContent=JSON.stringify({format:scene.package.formatVersion,selection:scene.selection,parameters:scene.parameters,textures:scene.textures.filter(texture=>texture.blob).map(texture=>({name:texture.name,source:[texture.sourceWidth,texture.sourceHeight],preview:[texture.width,texture.height],mode:texture.gpuCompressed?'GPU '+texture.family.toUpperCase():'RGBA'})),stats:scene.stats,nativeCompletion:scene.nativeCompletion,warnings:scene.warnings},null,2);
  if(!scene.meshes.length)status(t('当前资源/配置没有可预览的网格。','No geometry available for this resource/selection.'));
  else stage.reportStatus();
 }catch(error){window.bemPreviewError(String(error.message||error))}
 finally{if(!disposed){loading=false;updateControls();notify({type:'loaded',id});if(refreshPending){refreshPending=false;window.bemPreviewRefreshDraft(refreshPreserveCamera)}else if(gpuFallbackPending){gpuFallbackPending=false;request(resource)}}}
};
$('save').onclick=()=>{if(busy()||inspection||!valid||!draft)return;saving=true;updateControls();$('saveStatus').textContent=t('正在保存…','Saving…');notify({type:'save',id:sceneId})};
$('reload').onclick=()=>request(resource,true);$('resource').onchange=()=>request($('resource').value);$('reset').onclick=()=>stage?.resetView();$('texture').onchange=()=>stage?.setTextureMode($('texture').checked);
window.bemPreviewCancelDraftLoad=()=>{if(!composition||!awaitingHost)return;awaitingHost=false;loading=false;valid=false;updateControls()};
window.bemPreviewRefreshDraft=(preserveCamera=true)=>{if(busy()){refreshPending=true;refreshPreserveCamera=preserveCamera;return}if(!preserveCamera)resource='';draft=null;saved='';request('',true)};
window.bemPreviewPause=paused=>{if(stage)stage.paused=paused};
window.addEventListener('pagehide',()=>{disposed=true;++sceneVersion;stage?.dispose();decoder?.dispose()});
request();
