#include "BetterEndfieldNext/ThirdPartyModule.h"
#include <string>
namespace {
const BE_ThirdPartyHostV1* host=nullptr;
std::string configuration="{}";
uint64_t count=0;
BE_Result BE_CALL Initialize(const BE_ThirdPartyHostV1* provided,const char* json){
    if(!provided||provided->version!=1||provided->struct_size<sizeof(*provided)||!provided->reply||!provided->emit)return BE_Result_ContractMismatch;
    host=provided;configuration=json?json:"{}";count=0;
    host->log(host->context,"Echo sample initialized without game addresses");
    return host->emit(host->context,"{\"type\":\"ready\",\"callback_thread\":\"worker\"}");
}
BE_Result BE_CALL Configure(const char* json){configuration=json?json:"{}";return BE_Result_Ok;}
BE_Result BE_CALL Message(const char* request,const char* body){
    if(!host||!request||!body)return BE_Result_NotReady;++count;
    const bool helper=host->get_runtime&&host->get_runtime(host->context);
    const std::string reply="{\"counter\":"+std::to_string(count)+",\"echo\":"+body+",\"configuration\":"+configuration+",\"runtime_helper_ready\":"+(helper?"true":"false")+"}";
    const auto result=host->reply(host->context,request,BE_Result_Ok,reply.c_str());
    const auto event="{\"type\":\"counter\",\"value\":"+std::to_string(count)+"}";
    host->emit(host->context,event.c_str());return result;
}
void BE_CALL Shutdown(){host=nullptr;}
const BE_ThirdPartyModuleV1 api{sizeof(BE_ThirdPartyModuleV1),1,"example.echo",Initialize,Configure,Message,Shutdown};
}
#if defined(__ANDROID__)
extern "C" __attribute__((visibility("default"))) const BE_ThirdPartyModuleV1* BetterEndfieldNext_GetThirdPartyModuleV1(){return &api;}
#else
BE_EXPORT const BE_ThirdPartyModuleV1* BE_CALL BetterEndfieldNext_GetThirdPartyModuleV1(){return &api;}
#endif
