#include "../shared/third_party_modules/third_party_host.h"
#include <iostream>
#ifdef _WIN32
int wmain(int argc,wchar_t** argv){
#else
int main(int argc,char** argv){
#endif
    if(argc!=2)return 2;
    BetterEndfield::ThirdParty::ThirdPartyHost host;
#ifdef _WIN32
    const char* platform="windows-x64";
#else
    const char* platform="android-arm64";
#endif
    if(!host.Start(argv[1],platform,[](const std::string& id,const std::string& message){std::cerr<<id<<": "<<message<<'\n';}))return 1;
    std::cout<<"ready\n"<<std::flush;std::string line;std::getline(std::cin,line);host.Stop();return 0;
}
