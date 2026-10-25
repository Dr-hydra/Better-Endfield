#include <chrono>
#include <cstdio>
#include <cstdlib>
#include <string>
#include <windows.h>
static double Ms(){using namespace std::chrono;return duration<double,std::milli>(steady_clock::now().time_since_epoch()).count();}
static const char* kLine="2026-10-25 19:00:00 [custom_model] Applied BEM 1.5 material overrides: floats=1 colors=0\n";
int main(int argc,char**argv){
    const char* p=argc>1?argv[1]:"bench_log_target.log";
    const int N=20;
    // 造一个 ~120KB 的文件
    { FILE* f=fopen(p,"wb"); for(int i=0;i<2000;++i) fputs(kLine,f); fclose(f); }
    double s,e;
    s=Ms(); for(int i=0;i<N;++i){FILE* f=fopen(p,"ab"); if(f){fputs(kLine,f);fclose(f);} } e=Ms();
    std::printf("fopen(ab)+fputs+fclose : %.1f ms / %d  (%.1f us/line)\n",e-s,N,(e-s)*1000.0/N);
    s=Ms(); for(int i=0;i<N;++i){FILE* f=fopen(p,"rb"); if(f)fclose(f);} e=Ms();
    std::printf("fopen(rb)+fclose      : %.1f ms / %d  (%.1f us/line)\n",e-s,N,(e-s)*1000.0/N);
    s=Ms(); for(int i=0;i<N;++i){
        HANDLE h=CreateFileA(p,GENERIC_WRITE,FILE_SHARE_READ|FILE_SHARE_DELETE,nullptr,OPEN_ALWAYS,FILE_ATTRIBUTE_NORMAL,nullptr);
        if(h!=INVALID_HANDLE_VALUE){SetFilePointer(h,0,nullptr,FILE_END);DWORD w;WriteFile(h,kLine,(DWORD)strlen(kLine),&w,nullptr);CloseHandle(h);} } e=Ms();
    std::printf("CreateFile+Write+Close: %.1f ms / %d  (%.1f us/line)\n",e-s,N,(e-s)*1000.0/N);
    s=Ms(); { HANDLE h=CreateFileA(p,GENERIC_WRITE,FILE_SHARE_READ,nullptr,OPEN_ALWAYS,FILE_ATTRIBUTE_NORMAL,nullptr);
        if(h!=INVALID_HANDLE_VALUE){SetFilePointer(h,0,nullptr,FILE_END);DWORD w;for(int i=0;i<N;++i)WriteFile(h,kLine,(DWORD)strlen(kLine),&w,nullptr);CloseHandle(h);} } e=Ms();
    std::printf("CreateFile 一次 + %d 次 Write : %.2f ms total\n",N,e-s);
    remove(p);
    return 0;
}
