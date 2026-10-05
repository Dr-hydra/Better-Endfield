#pragma once
struct ClothLayerCode { size_t bytes;uint64_t hash; };
static constexpr ClothLayerCode ClothLayerBurstBodies[]{
  {2644,0x4f2ad03553b99707ULL},{1708,0xf3f1612e8c642b02ULL},
  {2643,0xb38412ac0230789bULL},{1733,0xa173eee7d5b1041bULL},
  {93,0xfdda4ec58ab9a8a9ULL},{93,0x789ba6fdb88d7cb5ULL},
  {57,0xac552a2a30760959ULL},{57,0x63e0f3de8146176dULL}};
static bool ClothLayerSpan(HMODULE module,const void *address,size_t bytes) {
  MEMORY_BASIC_INFORMATION m{};
  return module && address && bytes && VirtualQuery(address,&m,sizeof(m)) && m.AllocationBase==module &&
      m.State==MEM_COMMIT && !(m.Protect&(PAGE_NOACCESS|PAGE_GUARD)) && uintptr_t(address)>=uintptr_t(m.BaseAddress) &&
      bytes<=m.RegionSize-(uintptr_t(address)-uintptr_t(m.BaseAddress));
}
static bool ClothLayerBody(HMODULE module,const void *code,ClothLayerCode expected) {
  return ClothLayerSpan(module,code,expected.bytes) &&
      eiem_cloth_input::Fingerprint((const unsigned char*)code,expected.bytes)==expected.hash;
}
static bool ClothLayerCalls(unsigned char *code,size_t bytes,const void *target) {
  unsigned matches=0;
  for(size_t n=0;n+5<=bytes;++n)if(code[n]==0xe8) {
    int32_t rel=0;memcpy(&rel,code+n+1,4);
    if(uintptr_t(code+n+5)+intptr_t(rel)==uintptr_t(target))++matches;
  }
  return matches==1;
}
static bool ClothLayerBurstLocate(HMODULE module,unsigned char *(&found)[8]) {
  std::fill(std::begin(found),std::end(found),nullptr);
  auto base=(unsigned char*)module;
  if(!ClothLayerSpan(module,base,sizeof(IMAGE_DOS_HEADER)))return false;
  auto dos=(const IMAGE_DOS_HEADER*)base;
  if(dos->e_magic!=IMAGE_DOS_SIGNATURE || dos->e_lfanew<0 || dos->e_lfanew>4096)return false;
  auto nt=(const IMAGE_NT_HEADERS64*)(base+dos->e_lfanew);
  if(!ClothLayerSpan(module,nt,sizeof(*nt)) || nt->Signature!=IMAGE_NT_SIGNATURE ||
      nt->FileHeader.Machine!=IMAGE_FILE_MACHINE_AMD64 || nt->OptionalHeader.Magic!=IMAGE_NT_OPTIONAL_HDR64_MAGIC ||
      nt->OptionalHeader.NumberOfRvaAndSizes<=IMAGE_DIRECTORY_ENTRY_EXCEPTION)return false;
  const size_t size=nt->OptionalHeader.SizeOfImage;
  const auto &dir=nt->OptionalHeader.DataDirectory[IMAGE_DIRECTORY_ENTRY_EXCEPTION];
  if(size<4096 || size>64*1024*1024 || !dir.VirtualAddress || dir.VirtualAddress>=size ||
      dir.Size>size-dir.VirtualAddress || dir.Size<sizeof(RUNTIME_FUNCTION) || dir.Size%sizeof(RUNTIME_FUNCTION) ||
      dir.Size/sizeof(RUNTIME_FUNCTION)>65536 || !ClothLayerSpan(module,base+dir.VirtualAddress,dir.Size))return false;
  auto entries=(const RUNTIME_FUNCTION*)(base+dir.VirtualAddress);
  for(unsigned pass=0;pass<2;++pass)for(size_t n=0;n<dir.Size/sizeof(RUNTIME_FUNCTION);++n) {
    const auto &e=entries[n];
    if(e.BeginAddress>=e.EndAddress || e.EndAddress>size)return false;
    for(unsigned k=pass*4;k<pass*4+4;++k)if(e.EndAddress-e.BeginAddress==ClothLayerBurstBodies[k].bytes &&
        ClothLayerBody(module,base+e.BeginAddress,ClothLayerBurstBodies[k])) {
      if(pass && (!found[k-4] || !ClothLayerCalls(base+e.BeginAddress,ClothLayerBurstBodies[k].bytes,found[k-4])))continue;
      if(found[k])return false;found[k]=base+e.BeginAddress;
    }
  }
  for(auto p:found)if(!p)return false;
  return true;
}
