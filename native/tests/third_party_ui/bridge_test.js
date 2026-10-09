'use strict';
const assert=require('node:assert/strict'),fs=require('node:fs'),vm=require('node:vm'),path=require('node:path');
const root=path.resolve(__dirname,'../../..');
const android=fs.readFileSync(path.join(root,'android/app/src/main/assets/third-party-bridge.js'),'utf8').replace(/\r\n/g,'\n').trim();
const cs=fs.readFileSync(path.join(root,'ui/BetterEndfieldNext.UI/Services/ThirdPartyWebBridge.cs'),'utf8');
const windows=cs.match(/Script = """\r?\n([\s\S]*?)\r?\n    """;/)[1].split(/\r?\n/).map(line=>line.slice(4)).join('\n').trim();
assert.equal(windows,android,'Both platforms must expose the same creator bridge');
async function test(platform){
  const sent=[],timers=new Map();let listener,id=0;
  const window={};window.top=window;
  if(platform==='windows')window.chrome={webview:{postMessage:message=>sent.push(message),addEventListener:(_,callback)=>listener=callback}};
  else window.BetterEndfieldNextModuleHost={postMessage:message=>sent.push(JSON.parse(message))};
  vm.runInNewContext(android,{window,console,setTimeout:(callback,millis)=>{assert.equal(millis,15000);timers.set(++id,callback);return id;},clearTimeout:key=>timers.delete(key)});
  const deliver=data=>platform==='windows'?listener({data}):window.__beModuleDeliver(data);
  const host=window.betterEndfieldNext;assert.equal(Object.isFrozen(host),true);
  let promise=host.readConfig(),request=sent.pop();assert.equal(request.operation,'readConfig');assert.equal(request.protocol,'better-endfield-next.module-ui.v1');
  deliver({kind:'bridge_reply',request_id:request.request_id,value:{label:'author',future:[1,2]}});assert.deepEqual(await promise,{label:'author',future:[1,2]});
  promise=host.saveConfig({label:'changed',nested:{yes:true}});request=sent.pop();assert.equal(request.operation,'saveConfig');assert.equal(request.payload.nested.yes,true);
  deliver({kind:'bridge_reply',request_id:request.request_id,value:{saved:true}});assert.equal((await promise).saved,true);
  promise=host.send([1,{module_id:'other.module'}]);request=sent.pop();assert.equal(request.operation,'send');assert.equal(request.module_id,undefined);assert.equal(request.payload[1].module_id,'other.module');
  deliver({kind:'bridge_reply',request_id:request.request_id,value:{accepted:true}});assert.equal((await promise).accepted,true);
  const messages=[],unsubscribe=host.onmessage(message=>messages.push(message));deliver({kind:'runtime_message',message:{kind:'reply',request_id:request.request_id,body:{counter:1}}});
  deliver({kind:'runtime_message',message:{kind:'event',body:{type:'counter',value:1}}});assert.equal(messages.length,2);unsubscribe();deliver({kind:'runtime_message',message:{kind:'event'}});assert.equal(messages.length,2);
  promise=host.status();request=sent.pop();deliver({kind:'bridge_reply',request_id:request.request_id,error:'offline'});await assert.rejects(promise,/offline/);
  promise=host.status();sent.pop();const timedOut=timers.values().next().value;timedOut();await assert.rejects(promise,/timed out/);assert.equal(timers.size,1); // Virtual scheduler retains its fired timer.
}
(async()=>{await test('windows');await test('android');console.log('PASS shared Windows/Android creator bridge: config, opaque messages, reply/event, unsubscribe, errors and timeout');})().catch(error=>{console.error(error);process.exit(1);});
