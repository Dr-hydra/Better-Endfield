#pragma once
#include "cloth_asset_cache.h"
namespace eiem_cloth_asset {
struct Job {
  std::atomic<bool> cancel{false},done{false};
  uint64_t session=0,generation=0;unsigned command=0;
  uint32_t backend=0;uintptr_t character=0;
  std::wstring dataRoot;Query query;
  std::shared_ptr<Generated> result;
  std::string error,cacheKey;
  uint64_t elapsedMs=0,indexMs=0,generationMs=0;
  bool cacheHit=false;
  bool foreground=false;
};
struct InstalledSource {
  std::unique_ptr<Vfs> vfs;
  std::unique_ptr<Manifest> manifest;
  void Ready(const std::wstring &root,const std::atomic<bool> *cancel=nullptr){
    if(vfs){vfs->cancel=cancel;if(vfs->game==root&&vfs->StillCurrent()&&manifest&&Digest(vfs->Read(manifest->sourceName))==manifest->sourceHash)return;}
    auto next=std::make_unique<Vfs>();next->cancel=cancel;next->Open(root);auto names=std::make_unique<Manifest>();names->Load(*next);Need(next->StillCurrent(),"auto-index-changed-during-warmup");
    vfs=std::move(next);manifest=std::move(names);
  }
};
inline InstalledSource &Installed(){static InstalledSource value;return value;}
inline void WarmInstalled(const std::wstring &root){Installed().Ready(root);}
inline bool JobMatches(const Job &j,uint64_t session,uint64_t generation,unsigned command,uint32_t backend=0,uintptr_t character=0){return j.session==session&&j.generation==generation&&j.command==command&&j.backend==backend&&j.character==character&&!j.cancel.load(std::memory_order_acquire);}
inline void RunJob(Job &job){const auto start=GetTickCount64();try{
    static ContentCache cache;auto &source=Installed();source.Ready(job.dataRoot,&job.cancel);job.indexMs=GetTickCount64()-start;auto &v=*source.vfs;const auto key=QueryKey(job.query,job.dataRoot,v.indices);job.cacheKey=key;
    job.result=cache.Find(key,[&](const std::string &path){return Digest(v.Read(path));});
    if(job.result){Need(v.StillCurrent(),"auto-source-updated-during-cache-validation");job.cacheHit=true;}
    else {CheckCancel(&job.cancel);job.result=std::make_shared<Generated>(Generate(v,*source.manifest,job.query));cache.Put(key,job.result);}
    CheckCancel(&job.cancel);job.generationMs=GetTickCount64()-start-job.indexMs;
  }
  catch(const std::exception &e){job.error=e.what();job.result.reset();}catch(...){job.error="auto-worker-native-failure";job.result.reset();}
  if(Installed().vfs)Installed().vfs->cancel=nullptr;
  job.elapsedMs=GetTickCount64()-start;job.done.store(true,std::memory_order_release);
}
struct ThreadInput {std::shared_ptr<Job> job;HMODULE module=nullptr;};
static DWORD WINAPI AssetWorker(void *raw){HMODULE module=nullptr;{
    std::unique_ptr<ThreadInput> input(static_cast<ThreadInput*>(raw));
    SetThreadPriority(GetCurrentThread(),input->job->foreground?THREAD_PRIORITY_NORMAL:THREAD_PRIORITY_BELOW_NORMAL);
    module=input->module;RunJob(*input->job);
  }
  FreeLibraryAndExitThread(module,0);
}
inline bool StartJob(const std::shared_ptr<Job> &job){auto input=std::make_unique<ThreadInput>();input->job=job;
  if(!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS,reinterpret_cast<LPCWSTR>(&AssetWorker),&input->module))return false;
  const auto thread=CreateThread(nullptr,0,&AssetWorker,input.get(),0,nullptr);if(!thread){FreeLibrary(input->module);return false;}input.release();CloseHandle(thread);return true;
}
}
