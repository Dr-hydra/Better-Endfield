#include <chrono>
#include <cstdio>
#include <fstream>
#include <string>
static double Ms(){using namespace std::chrono;return duration<double,std::milli>(steady_clock::now().time_since_epoch()).count();}
static void line(std::ofstream& st){st<<"2026-10-25 19:00:00 [custom_model] Applied BEM 1.5 material overrides: floats=1 colors=0\n";}
int main(int argc,char**argv){
    const char* p=argc>1?argv[1]:"bench_log_target.log";
    const int N=argc>2?atoi(argv[2]):20;
    std::remove(p);
    double s=Ms();
    for(int i=0;i<N;++i){std::ofstream st(p,std::ios::out|std::ios::app);line(st);}
    double e=Ms();
    std::printf("fresh file, %d open/append/close: %.1f ms (%.1f us/line)\n",N,e-s,(e-s)*1000.0/N);
    // 已存在且更大的文件
    {std::ofstream st(p,std::ios::out|std::ios::app);for(int i=0;i<2000;++i)line(st);}
    s=Ms();
    for(int i=0;i<N;++i){std::ofstream st(p,std::ios::out|std::ios::app);line(st);}
    e=Ms();
    std::printf("existing ~120KB file, %d open/append/close: %.1f ms (%.1f us/line)\n",N,e-s,(e-s)*1000.0/N);
    std::remove(p);
    return 0;
}
