#include "../modules/custom_model/cloth_contact_counter.h"
#include <cstring>
#include <iostream>
#include <stdexcept>
using namespace BetterEndfieldNext::CustomModel::ClothContact;
int main() {
    auto check=[](bool ok){if(!ok)throw std::runtime_error("contact counter regression");};
    // The Android fault had a null first pointer. Scheduling must bind the
    // live list length, not a snapshot of its pre-dependency length.
    alignas(8) struct {uintptr_t buffer;int32_t length,capacity;} list{0,0,10};
    Job job{};job.next={0x1000,77};job.old={0x2000,88};job.list={reinterpret_cast<uintptr_t>(&list),99};
    const Job original=job;Job copy{};
    check(BindCounter(job,job.list.data,offsetof(decltype(list),length),copy)==Repair::Bound);
    list.length=7;check(*reinterpret_cast<int32_t*>(copy.count.data)==7);
    check(std::memcmp(&job,&original,sizeof(job))==0);
    check(std::memcmp(&copy.next,&job.next,sizeof(Container)*3)==0);
    Job valid=job;valid.count.data=0x4000;
    check(BindCounter(valid,valid.list.data,8,copy)==Repair::Unchanged);
    for(int n=0;n<6;++n) {
        Job bad=job;uintptr_t lease=job.list.data;size_t offset=8;
        if(n==0)lease=0;if(n==1)lease+=8;if(n==2)offset=4;
        if(n==3)bad.next.data=0;if(n==4)bad.old.data=0;if(n==5)bad.count.tail=1;
        Job untouched{};untouched.count.data=0xdead;copy=untouched;
        check(BindCounter(bad,lease,offset,copy)==Repair::Refused);
        check(std::memcmp(&copy,&untouched,sizeof(copy))==0);
    }
    std::cout<<"PASS contact counter: live length alias, source/arrays preserved, native count retained, six invalid bindings refused\n";
}
