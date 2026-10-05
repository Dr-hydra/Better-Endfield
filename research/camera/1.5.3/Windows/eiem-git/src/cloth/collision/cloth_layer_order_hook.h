#pragma once
#include "cloth_contact_job_state.h"
#include "../generated/cloth_layer_order_generated.h"
static eiem_cloth_layer::Gate s_clothLayerGate;
static std::atomic<bool> s_clothLayerInstalled{false};
static bool (*s_clothLayerInstaller)()=nullptr;
static char s_clothLayerInstallIssue[128]{};
using ClothLayerKernelFn=void(__fastcall *)(void *,const eiem_cloth_layer::PointContact *,int *,int *,int);
struct ClothLayerSolverJob { eiem_cloth_contact_job::Container next,contacts,count,sum; };
static_assert(sizeof(ClothLayerSolverJob)==64 && offsetof(ClothLayerSolverJob,contacts)==16,"native solver job POD");
using ClothLayerJobFn=void(__fastcall *)(const ClothLayerSolverJob *,int);
using ClothLayerManagedKernelFn=void(__fastcall *)(void *,const eiem_cloth_layer::PointContact *,int *,int *,int,void *);
using ClothLayerManagedJobFn=void(__fastcall *)(const ClothLayerSolverJob *,int,void *);
static void *s_clothLayerOriginal[6]{};
template<unsigned Path> static void __fastcall ClothLayerKernelHook(void *next,const eiem_cloth_layer::PointContact *buffer,int *counts,int *sums,int index) {
  alignas(16) eiem_cloth_layer::PointContact copy{};
  if(s_clothLayerInstalled.load(std::memory_order_acquire) &&
      s_clothLayerGate.Solve(buffer,index,copy,Path)==eiem_cloth_layer::Result::Changed) {
    buffer=&copy;index=0;
  }
  ((ClothLayerKernelFn)s_clothLayerOriginal[Path])(next,buffer,counts,sums,index);
}
template<unsigned Path> static void __fastcall ClothLayerJobHook(const ClothLayerSolverJob *job,int index) {
  alignas(16) eiem_cloth_layer::PointContact contact{};ClothLayerSolverJob copy{};
  if(s_clothLayerInstalled.load(std::memory_order_acquire) && job &&
      s_clothLayerGate.Solve((const eiem_cloth_layer::PointContact*)job->contacts.data,index,contact,Path)==eiem_cloth_layer::Result::Changed) {
    copy=*job;copy.contacts.data=uintptr_t(&contact);job=&copy;index=0;
  }
  ((ClothLayerJobFn)s_clothLayerOriginal[Path])(job,index);
}
static void __fastcall ClothLayerManagedKernelHook(void *next,const eiem_cloth_layer::PointContact *buffer,int *counts,int *sums,int index,void *method) {
  alignas(16) eiem_cloth_layer::PointContact copy{};
  if(s_clothLayerInstalled.load(std::memory_order_acquire) &&
      s_clothLayerGate.Solve(buffer,index,copy,4)==eiem_cloth_layer::Result::Changed) {buffer=&copy;index=0;}
  ((ClothLayerManagedKernelFn)s_clothLayerOriginal[4])(next,buffer,counts,sums,index,method);
}
static void __fastcall ClothLayerManagedJobHook(const ClothLayerSolverJob *job,int index,void *method) {
  alignas(16) eiem_cloth_layer::PointContact contact{};ClothLayerSolverJob copy{};
  if(s_clothLayerInstalled.load(std::memory_order_acquire) && job &&
      s_clothLayerGate.Solve((const eiem_cloth_layer::PointContact*)job->contacts.data,index,contact,5)==eiem_cloth_layer::Result::Changed) {
    copy=*job;copy.contacts.data=uintptr_t(&contact);job=&copy;index=0;
  }
  ((ClothLayerManagedJobFn)s_clothLayerOriginal[5])(job,index,method);
}
