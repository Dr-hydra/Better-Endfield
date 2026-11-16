#include "platform_macro.h"
#if defined(TARGET_ARCH_X64)

#include "dobby_internal.h"

#include "core/modules/assembler/assembler-x64.h"
#include "core/modules/codegen/codegen-x64.h"

#include "InstructionRelocation/x64/X64InstructionRelocation.h"

#include "MemoryAllocator/NearMemoryArena.h"
#include "InterceptRouting/RoutingPlugin/RoutingPlugin.h"

using namespace zz::x64;

struct IndirectStub {
  addr_t branch_address;
  void *stub_address;
};

LiteMutableArray *existing_indirect_stubs;

static void **AllocIndirectStub(addr_t branch_address) {
  if (!existing_indirect_stubs) {
    existing_indirect_stubs = new LiteMutableArray(10);
  }

  LiteCollectionIterator iter(existing_indirect_stubs);
  IndirectStub *stub = nullptr;
  while ((stub = reinterpret_cast<IndirectStub *>(iter.getNextObject())) != nullptr) {
    if (stub->branch_address == branch_address) {
      return (void **)stub->stub_address;
    }
  }

  WritableDataChunk *forwardStub = NULL;

  forwardStub =
      NearMemoryArena::AllocateDataChunk((addr_t)branch_address, (size_t)2 * 1024 * 1024 * 1024, (int)sizeof(void *));
  if (forwardStub == nullptr) {
    ERROR_LOG("Not found near forward stub");
    return NULL;
  }

  stub = new IndirectStub;
  stub->branch_address = branch_address;
  stub->stub_address = forwardStub->address;
  existing_indirect_stubs->pushObject(reinterpret_cast<LiteObject *>(stub));
  return (void **)forwardStub->address;
}

CodeBufferBase *GenerateNormalTrampolineBuffer(addr_t from, addr_t to) {
  TurboAssembler turbo_assembler_((void *)from);
#define _ turbo_assembler_.

  // branch
  void **branch_stub = AllocIndirectStub(from);
  *branch_stub = (void *)to;

  CodeGen codegen(&turbo_assembler_);
  codegen.JmpNearIndirect((uint64_t)branch_stub);

  CodeBufferBase *result = NULL;
  result = turbo_assembler_.GetCodeBuffer()->Copy();
  return result;
}

CodeBufferBase *GenerateNearTrampolineBuffer(InterceptRouting *routing, addr_t src, addr_t dst) {
  DLOG(0, "x64 near branch trampoline enable default");
  return NULL;
}

#endif