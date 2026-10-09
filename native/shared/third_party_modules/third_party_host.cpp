#if defined(_WIN32)
#include <winsock2.h>
#include <ws2tcpip.h>
#include <Windows.h>
#else
#include <arpa/inet.h>
#include <sys/socket.h>
#include <sys/select.h>
#include <unistd.h>
#include <dlfcn.h>
#endif
#include "third_party_host.h"
#include "../third_party/nlohmann/json.hpp"
#include <algorithm>
#include <atomic>
#include <chrono>
#include <deque>
#include <fstream>
#include <map>
#include <mutex>
#include <set>
#include <thread>
#include <stdexcept>
#include <cctype>
#include <cstring>
#include <climits>
namespace BetterEndfieldNext::ThirdParty {
namespace {
using J=nlohmann::json;
constexpr size_t kLimit=1024*1024;
#ifdef _WIN32
using Socket=SOCKET;constexpr Socket kInvalid=INVALID_SOCKET;
void Close(Socket fd){closesocket(fd);}
#else
using Socket=int;constexpr Socket kInvalid=-1;
void Close(Socket fd){close(fd);}
#endif
void Require(bool condition,const char* reason){if(!condition)throw std::runtime_error(reason);}
std::string Read(const std::filesystem::path& path){std::ifstream in(path,std::ios::binary|std::ios::ate);Require(bool(in),"Module metadata cannot be opened");auto size=in.tellg();Require(size>0 && static_cast<uint64_t>(size)<=kLimit,"Module metadata exceeds limit");std::string data(static_cast<size_t>(size),'\0');in.seekg(0);Require(bool(in.read(data.data(),data.size())),"Module metadata read failed");return data;}
std::filesystem::path UtfPath(const std::string& value){return std::filesystem::path(std::u8string_view(reinterpret_cast<const char8_t*>(value.data()),value.size()));}
bool Id(const std::string& id){if(id.empty() || id.size()>96 || !std::isalnum(static_cast<unsigned char>(id[0])))return false;for(unsigned char c:id)if(!std::isalnum(c)&&c!='_'&&c!='.'&&c!='-')return false;std::string lower=id;std::transform(lower.begin(),lower.end(),lower.begin(),[](unsigned char c){return static_cast<char>(std::tolower(c));});return !lower.starts_with("betterendfieldnext.")&&lower!="voice.character";}
std::filesystem::path Child(const std::filesystem::path& root,const std::string& relative){Require(!relative.empty() && relative.find('\\')==relative.npos && relative.find(':')==relative.npos && relative.find('\0')==relative.npos,"Invalid package-relative path");auto path=UtfPath(relative);Require(!path.is_absolute(),"Absolute package library path");for(const auto& part:path)Require(part!="."&&part!=".."&&!part.empty(),"Invalid package path segment");auto base=std::filesystem::weakly_canonical(root);auto candidate=std::filesystem::weakly_canonical(base/path);auto rel=candidate.lexically_relative(base);Require(!rel.empty()&&!rel.is_absolute()&&*rel.begin()!="..","Library escapes module generation");Require(std::filesystem::is_regular_file(candidate),"Native module library is missing");return candidate;}
J Parse(const std::string& data){Require(data.size()<=kLimit,"JSON exceeds limit");try{return J::parse(data);}catch(const J::parse_error&){throw std::runtime_error("Invalid module JSON");}}
bool ConstantEqual(const std::string& a,const std::string& b){size_t diff=a.size()^b.size();for(size_t i=0;i<std::max(a.size(),b.size());++i)diff|=(i<a.size()?a[i]:0)^(i<b.size()?b[i]:0);return diff==0;}
}
struct ThirdPartyHost::Impl {
    struct Module {
        Impl* owner=nullptr;std::string id,directory,configuration="{}",generation,status="disabled",error;
        bool requested=false,active=false,pinned=false,restart=false;
        void* library=nullptr;const BE_ThirdPartyModuleV1* api=nullptr;
        BE_ThirdPartyHostV1 host{};
        std::vector<std::string> dependencies;
        std::mutex eventsMutex;std::deque<J> events;size_t eventBytes=0;
        std::mutex logsMutex;std::deque<std::string> logs;
    };
    std::filesystem::path indexPath;std::string platform,token;uint16_t port=0;
    Log log;std::atomic<const BE_HostApiV1*> runtime{nullptr};const BE_HookChainApiV1* hooks=nullptr;
    std::atomic_bool stopping{false};std::thread worker;Socket listener=kInvalid;
    std::map<std::string,std::unique_ptr<Module>> modules;std::vector<std::string> order,loadedOrder;
    struct Message {std::string module,request,body;};std::deque<Message> inbox;size_t inboxBytes=0;
    std::mutex updateMutex;std::string pending;
    std::filesystem::file_time_type stamp{};
    static void BE_CALL LogMessage(void* context,const char* message){
        auto* m=static_cast<Module*>(context);if(!m||!message)return;size_t size=0;while(size<4096&&message[size])++size;std::string text(message,size);
        {std::lock_guard lock(m->logsMutex);if(m->logs.size()>=64)m->logs.pop_front();m->logs.push_back(text);}
        if(m->owner->log)m->owner->log(m->id,text);
    }
    static const BE_HostApiV1* BE_CALL GetRuntime(void* context){auto* m=static_cast<Module*>(context);return m?m->owner->runtime.load():nullptr;}
    static void AddEvent(Module& module,J event){auto size=event.dump().size();if(size>kLimit)return;std::lock_guard lock(module.eventsMutex);while(!module.events.empty()&&(module.events.size()>=256||module.eventBytes+size>2*kLimit)){module.eventBytes-=module.events.front().dump().size();module.events.pop_front();}module.eventBytes+=size;module.events.push_back(std::move(event));}
    static BE_Result BE_CALL Reply(void* context,const char* request,BE_Result result,const char* json){try{auto* m=static_cast<Module*>(context);if(!m||!request||std::strlen(request)>128||!json)return BE_Result_InvalidArgument;AddEvent(*m,{{"kind","reply"},{"request_id",request},{"result",result},{"body",Parse(json)}});return BE_Result_Ok;}catch(...){return BE_Result_InvalidArgument;}}
    static BE_Result BE_CALL Emit(void* context,const char* json){try{auto* m=static_cast<Module*>(context);if(!m||!json)return BE_Result_InvalidArgument;AddEvent(*m,{{"kind","event"},{"body",Parse(json)}});return BE_Result_Ok;}catch(...){return BE_Result_InvalidArgument;}}
    void Disable(Module& module){if(module.active){module.active=false;try{if(module.api->shutdown)module.api->shutdown();}catch(...){module.error="Module shutdown threw an exception";}if(hooks&&hooks->disable_module)hooks->disable_module(hooks->context,module.id.c_str());}module.status="disabled";}
    void Configure(const J& index,bool initial){
        Require(index.is_object()&&index.value("schema",0)==1&&index.at("modules").is_array()&&index.at("modules").size()<=128,"Invalid third-party index");
        if(!initial)Require(index.at("port")==port && index.at("token")==token,"Changing connection credentials requires restart");
        std::set<std::string> present;order.clear();
        for(const auto& record:index.at("modules")){
            const auto id=record.at("id").get<std::string>();Require(Id(id)&&present.insert(id).second,"Invalid or duplicate third-party ID");
            auto& entry=modules[id];if(!entry){entry=std::make_unique<Module>();entry->owner=this;entry->id=id;}
            auto& m=*entry;order.push_back(id);
            const auto directory=record.at("directory").get<std::string>();const auto generation=record.value("generation",directory);
            const auto config=record.value("configuration",J::object());Require(config.is_object(),"Module configuration must be an object");const auto encoded=config.dump();
            const bool enabled=record.value("enabled",false);
            if(record.contains("preparation_error") && m.active){m.restart=true;m.error=record.at("preparation_error").get<std::string>()+"; previous module retained";continue;}
            if(m.pinned && (m.directory!=directory || m.generation!=generation)){m.restart=true;m.error="Installed module generation changed; restart required";m.requested=enabled;continue;}
            if(m.directory!=directory)m.directory=directory;if(m.generation!=generation)m.generation=generation;m.requested=enabled;
            if(record.contains("preparation_error")){m.status="failed";m.error=record.at("preparation_error").get<std::string>();}
            if(m.configuration!=encoded){m.configuration=encoded;if(m.active&&enabled&&m.api->configuration_changed){auto result=m.api->configuration_changed(encoded.c_str());if(result!=BE_Result_Ok){m.error="Configuration callback rejected update";}}}
        }
        for(auto& [id,m]:modules)if(!present.contains(id)){m->requested=false;m->restart=m->pinned;}
        std::set<std::string> stop;
        for(const auto& [id,m]:modules)if(m->active&&!m->requested)stop.insert(id);
        for(size_t pass=0;pass<modules.size();++pass)for(const auto& [id,m]:modules)if(m->active)
            for(const auto& dep:m->dependencies){const auto found=modules.find(dep);if(found==modules.end()||(!found->second->active&&!(found->second->status=="ui_only"&&found->second->requested))||stop.contains(dep))stop.insert(id);}
        for(auto it=loadedOrder.rbegin();it!=loadedOrder.rend();++it)if(stop.contains(*it)){auto& m=*modules.at(*it);Disable(m);if(m.requested){m.status="waiting_dependencies";m.error="Module dependency is inactive";}}
        for(auto& [id,m]:modules)if(!m->active&&!m->requested&&m->status!="failed")m->status="disabled";
        // Read manifests before ordering; no native code runs before dependency
        // and package identity validation. Untrusted binaries are never unloaded.
        for(const auto& id:order){auto& m=*modules.at(id);if(!m.requested||m.active||m.restart)continue;try{
            const auto manifest=Parse(Read(UtfPath(m.directory)/"module.json"));
            Require(manifest.value("format",0)==1&&manifest.value("abi",0)==1&&manifest.at("id")==id,"Module identity/ABI differs");
            m.dependencies=manifest.value("dependencies",std::vector<std::string>{});Require(m.dependencies.size()<=128,"Too many module dependencies");
            for(const auto& dependency:m.dependencies)Require(Id(dependency)&&dependency!=id,"Invalid module dependency");
            if(!manifest.at("libraries").contains(platform)){
                Require(manifest.contains("ui"),"Module has no runtime or UI for this platform");Child(UtfPath(m.directory),manifest.at("ui").get<std::string>());
                m.status="ui_only";m.error.clear();continue;
            }
            m.status="waiting_dependencies";m.error.clear();
        }catch(const std::exception& error){m.status="failed";m.error=error.what();}}
        for(size_t pass=0;pass<order.size();++pass){bool progressed=false;for(const auto& id:order){auto& m=*modules.at(id);if(!m.requested||m.active||m.restart||m.status!="waiting_dependencies")continue;
            if(!std::all_of(m.dependencies.begin(),m.dependencies.end(),[&](const auto& dep){auto it=modules.find(dep);return it!=modules.end()&&(it->second->active||(it->second->status=="ui_only"&&it->second->requested));}))continue;
            try{Load(m);progressed=true;}catch(const std::exception& error){m.status="failed";m.error=error.what();}catch(...){m.status="failed";m.error="Module native callback failed";}
        }if(!progressed)break;}
        for(const auto& id:order){auto& m=*modules.at(id);if(m.status=="waiting_dependencies")m.error="Missing, disabled, failed, or cyclic module dependency";}
    }
    void Load(Module& module){
        if(!module.library){const auto manifest=Parse(Read(UtfPath(module.directory)/"module.json"));const auto library=Child(UtfPath(module.directory),manifest.at("libraries").at(platform).get<std::string>());
#ifdef _WIN32
            Require(library.extension()==L".dll","Windows module must be a DLL");module.library=LoadLibraryExW(library.c_str(),nullptr,LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR|LOAD_LIBRARY_SEARCH_DEFAULT_DIRS);
            Require(module.library,"Native module library failed to load");auto entry=reinterpret_cast<BE_GetThirdPartyModuleV1Fn>(GetProcAddress(static_cast<HMODULE>(module.library),"BetterEndfieldNext_GetThirdPartyModuleV1"));
#else
            Require(library.extension()==".so","Android module must be a shared library");module.library=dlopen(library.c_str(),RTLD_NOW|RTLD_LOCAL);Require(module.library,"Native module library failed to load");auto entry=reinterpret_cast<BE_GetThirdPartyModuleV1Fn>(dlsym(module.library,"BetterEndfieldNext_GetThirdPartyModuleV1"));
#endif
            module.pinned=true;Require(entry,"Third-party module entry point is missing");const auto candidate=entry();
            Require(candidate && candidate->struct_size>=sizeof(BE_ThirdPartyModuleV1)&&candidate->version==1&&candidate->id&&module.id==candidate->id&&candidate->initialize&&candidate->on_message,"Third-party module ABI differs");module.api=candidate;
            module.host={sizeof(BE_ThirdPartyHostV1),1,&module,module.id.c_str(),platform.c_str(),module.directory.c_str(),runtime.load(),hooks,LogMessage,Reply,Emit,GetRuntime};
        }
        Require(module.api,"Rejected module library requires restart");
        BE_Result result=BE_Result_Failed;try{result=module.api->initialize(&module.host,module.configuration.c_str());}catch(...){
            try{if(module.api->shutdown)module.api->shutdown();}catch(...){}if(hooks&&hooks->disable_module)hooks->disable_module(hooks->context,module.id.c_str());throw;
        }
        if(result!=BE_Result_Ok){try{if(module.api->shutdown)module.api->shutdown();}catch(...){}if(hooks&&hooks->disable_module)hooks->disable_module(hooks->context,module.id.c_str());}
        Require(result==BE_Result_Ok,"Third-party initialization rejected");module.active=true;module.status="ready";module.error.clear();std::erase(loadedOrder,module.id);loadedOrder.push_back(module.id);LogMessage(&module,"Third-party module started (worker callbacks)");
    }
    J Handle(const std::string& method,const std::string& path,const std::string& body){
        if(method=="GET"&&path=="/status"){J result={{"connected",true},{"protocol",1},{"callback_thread","worker"},{"modules",J::array()}};for(const auto& [id,m]:modules){J logs=J::array();{std::lock_guard lock(m->logsMutex);for(const auto& line:m->logs)logs.push_back(line);}result["modules"].push_back({{"id",id},{"status",m->status},{"error",m->error},{"enabled",m->active},{"requested_enabled",m->requested},{"restart_required",m->restart},{"logs",logs}});}return result;}
        Require(method=="POST","Unsupported request method");const auto request=Parse(body);const auto id=request.at("module_id").get<std::string>();auto found=modules.find(id);Require(found!=modules.end(),"Unknown module ID");auto& m=*found->second;
        if(path=="/poll"){J messages=J::array();std::lock_guard lock(m.eventsMutex);while(!m.events.empty()){messages.push_back(std::move(m.events.front()));m.events.pop_front();}m.eventBytes=0;return {{"messages",messages}};}
        if(path=="/send"){Require(m.active,"Module is not active");const auto requestId=request.at("request_id").get<std::string>();Require(!requestId.empty()&&requestId.size()<=128&&requestId.find('\0')==requestId.npos,"Invalid request ID");const auto payload=request.at("body").dump();Require(inbox.size()<256&&inboxBytes+payload.size()<=4*kLimit,"Module message queue is full");inboxBytes+=payload.size();inbox.push_back({id,requestId,payload});return {{"accepted",true},{"result",BE_Result_Ok}};}
        if(path=="/configure"){const auto config=request.at("configuration");Require(config.is_object(),"Configuration must be an object");const auto encoded=config.dump();m.configuration=encoded;BE_Result result=BE_Result_Ok;if(m.active&&m.api->configuration_changed)result=m.api->configuration_changed(encoded.c_str());return {{"accepted",result==BE_Result_Ok},{"result",result}};}
        throw std::runtime_error("Unknown endpoint");
    }
    void Client(Socket fd){
#ifdef _WIN32
        DWORD timeout=2000;setsockopt(fd,SOL_SOCKET,SO_RCVTIMEO,reinterpret_cast<const char*>(&timeout),sizeof(timeout));setsockopt(fd,SOL_SOCKET,SO_SNDTIMEO,reinterpret_cast<const char*>(&timeout),sizeof(timeout));
#else
        timeval timeout{2,0};setsockopt(fd,SOL_SOCKET,SO_RCVTIMEO,&timeout,sizeof(timeout));setsockopt(fd,SOL_SOCKET,SO_SNDTIMEO,&timeout,sizeof(timeout));
#endif
        int code=200;J response;try{
            std::string data;char buffer[4096];size_t boundary=std::string::npos;
            while((boundary=data.find("\r\n\r\n"))==std::string::npos){const auto count=recv(fd,buffer,sizeof(buffer),0);Require(count>0,"Incomplete HTTP headers");data.append(buffer,count);Require(data.size()<=16384,"HTTP headers exceed limit");}
            const auto first=data.find("\r\n");const auto space=data.find(' '),second=data.find(' ',space+1);Require(first!=data.npos&&space<first&&second<first,"Malformed HTTP request");auto method=data.substr(0,space),path=data.substr(space+1,second-space-1);
            std::map<std::string,std::string> headers;size_t start=first+2;while(start<boundary){auto end=data.find("\r\n",start),colon=data.find(':',start);Require(colon<end,"Malformed HTTP header");auto key=data.substr(start,colon-start);std::transform(key.begin(),key.end(),key.begin(),[](unsigned char c){return static_cast<char>(std::tolower(c));});auto value=data.substr(colon+1,end-colon-1);while(!value.empty()&&value.front()==' ')value.erase(value.begin());Require(headers.emplace(key,value).second,"Duplicate HTTP header");start=end+2;}
            if(!ConstantEqual(headers["authorization"],"Bearer "+token)){code=401;response={{"error","Unauthorized"}};}
            else{Require(!headers.contains("transfer-encoding"),"Chunked requests unsupported");size_t length=0;if(headers.contains("content-length")){const auto& value=headers.at("content-length");Require(!value.empty()&&std::all_of(value.begin(),value.end(),[](unsigned char c){return std::isdigit(c);}),"Invalid content length");length=std::stoull(value);}Require(length<=kLimit,"Request body exceeds limit");const auto offset=boundary+4;while(data.size()-offset<length){const auto count=recv(fd,buffer,sizeof(buffer),0);Require(count>0,"Incomplete request body");data.append(buffer,count);Require(data.size()-offset<=kLimit,"Request body exceeds limit");}response=Handle(method,path,data.substr(offset,length));}
        }catch(const std::exception& error){code=400;response={{"error",error.what()}};}catch(...){code=500;response={{"error","Module callback failed"}};}
        const auto body=response.dump();const auto output="HTTP/1.1 "+std::to_string(code)+" "+(code==200?"OK":"Error")+"\r\nContent-Type: application/json; charset=utf-8\r\nCache-Control: no-store\r\nConnection: close\r\nContent-Length: "+std::to_string(body.size())+"\r\n\r\n"+body;size_t sent=0;int flags=0;
#if defined(MSG_NOSIGNAL)
        flags=MSG_NOSIGNAL;
#endif
        while(sent<output.size()){const auto count=send(fd,output.data()+sent,static_cast<int>(std::min<size_t>(output.size()-sent,INT_MAX)),flags);if(count<=0)break;sent+=count;}
    }
    void Loop(J initial){try{Configure(initial,true);}catch(const std::exception& error){if(log)log("third-party",error.what());}
        auto next=std::chrono::steady_clock::now();while(!stopping.load()){
            std::string update;{std::lock_guard lock(updateMutex);update.swap(pending);}if(!update.empty())try{Configure(Parse(update),false);}catch(const std::exception& error){if(log)log("third-party",error.what());}
            if(std::chrono::steady_clock::now()>=next){next=std::chrono::steady_clock::now()+std::chrono::seconds(1);try{auto current=std::filesystem::last_write_time(indexPath);if(current!=stamp){stamp=current;Configure(Parse(Read(indexPath)),false);}}catch(const std::exception& error){if(log)log("third-party",error.what());}}
            while(!inbox.empty()&&!stopping.load()){
                auto message=std::move(inbox.front());inbox.pop_front();inboxBytes-=message.body.size();auto found=modules.find(message.module);
                if(found==modules.end())continue;auto& module=*found->second;BE_Result result=BE_Result_NotReady;
                try{if(module.active)result=module.api->on_message(message.request.c_str(),message.body.c_str());}catch(...){result=BE_Result_Failed;}
                if(result!=BE_Result_Ok)Reply(&module,message.request.c_str(),result,"{\"error\":\"Module message was rejected\"}");
            }
            fd_set set;FD_ZERO(&set);FD_SET(listener,&set);timeval timeout{0,200000};if(select(static_cast<int>(listener+1),&set,nullptr,nullptr,&timeout)>0){auto client=accept(listener,nullptr,nullptr);if(client!=kInvalid){Client(client);Close(client);}}
        }
        for(auto it=loadedOrder.rbegin();it!=loadedOrder.rend();++it)Disable(*modules.at(*it));for(auto& [id,m]:modules)Disable(*m);
    }
};
ThirdPartyHost::ThirdPartyHost():impl_(std::make_unique<Impl>()){}
ThirdPartyHost::~ThirdPartyHost(){Stop();}
bool ThirdPartyHost::Start(const std::filesystem::path& index,const std::string& platform,Log log,const BE_HostApiV1* runtime,const BE_HookChainApiV1* hooks){
    auto& s=*impl_;if(s.worker.joinable())return false;s.indexPath=index;s.platform=platform;s.log=std::move(log);s.runtime=runtime;s.hooks=hooks;
    if(!std::filesystem::is_regular_file(index))return false;try{auto initial=Parse(Read(index));Require(initial.value("schema",0)==1,"Unknown runtime index schema");const auto port=initial.at("port").get<uint32_t>();Require(port>=1024&&port<=65535,"Invalid localhost port");s.port=static_cast<uint16_t>(port);s.token=initial.at("token").get<std::string>();Require(s.token.size()>=32&&s.token.size()<=128&&std::all_of(s.token.begin(),s.token.end(),[](unsigned char c){return std::isalnum(c)||c=='_'||c=='-';}),"Invalid localhost credential");
#ifdef _WIN32
        WSADATA ws{};Require(WSAStartup(MAKEWORD(2,2),&ws)==0,"Socket initialization failed");
#endif
        s.listener=socket(AF_INET,SOCK_STREAM,IPPROTO_TCP);Require(s.listener!=kInvalid,"Socket creation failed");
#if !defined(_WIN32)
        int reuse=1;setsockopt(s.listener,SOL_SOCKET,SO_REUSEADDR,&reuse,sizeof(reuse));
#endif
        sockaddr_in address{};address.sin_family=AF_INET;address.sin_addr.s_addr=htonl(INADDR_LOOPBACK);address.sin_port=htons(s.port);Require(bind(s.listener,reinterpret_cast<sockaddr*>(&address),sizeof(address))==0&&listen(s.listener,8)==0,"Localhost module port unavailable");s.stamp=std::filesystem::last_write_time(index);s.stopping=false;s.worker=std::thread([&s,initial=std::move(initial)]{s.Loop(initial);});return true;
    }catch(const std::exception& error){if(s.listener!=kInvalid){Close(s.listener);s.listener=kInvalid;}if(s.log)s.log("third-party",error.what());return false;}
}
bool ThirdPartyHost::UpdateIndex(const std::string& json){if(json.size()>kLimit||!impl_->worker.joinable())return false;try{Parse(json);}catch(...){return false;}std::lock_guard lock(impl_->updateMutex);impl_->pending=json;return true;}
void ThirdPartyHost::SetRuntime(const BE_HostApiV1* runtime){impl_->runtime.store(runtime);}
void ThirdPartyHost::Stop(){auto& s=*impl_;s.stopping=true;if(s.worker.joinable())s.worker.join();if(s.listener!=kInvalid){Close(s.listener);s.listener=kInvalid;}}
}
