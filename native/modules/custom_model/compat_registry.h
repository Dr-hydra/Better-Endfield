#pragma once
// Compatibility repair layer.
//
// This layer is an entry point, not a specification. It answers two questions
// only: when a repair gets a chance to run, and what context it can read. How a
// case is actually repaired is entirely up to the registered repair.
//
// Three rules apply to every repair, because they are about safety and
// observability rather than about method:
//   1. Repairs are consulted after an ordinary match came back Missing.
//      Ambiguous is never repaired: picking one of several candidates is the
//      historical source of corrupted screens.
//   2. A repair is applied completely or not at all. SelectRepaired restores the
//      original request unless a retry came back Matched, so a half-applied
//      repair can never reach binding.
//   3. An applied repair names itself in the caller's diagnostic sink.
//
// Everything else - single value, exact equality, runtime proof - is a
// self-imposed constraint of the data-record form below. A hand written
// function is free to ignore all of it.
#include <cstddef>
#include <cstdint>
#include <span>
#include <string_view>

#include "generic_model_matcher.h"
#include "mod_registry.h"

namespace BetterEndfieldNext::CustomModel::GenericMatching {

// Read-only view plus the failed match that triggered the consult. The request
// is mutable so a handler may rewrite it in place when that is simpler than
// returning one; SelectRepaired restores it if the case does not stick.
struct RepairContext {
    const CharacterAdapter& adapter;
    void* asset;
    std::span<const Candidate> candidates;
    uint32_t component_id;
    Request& request;
    Match failed;
};

struct RepairResult {
    enum class Kind {
        NotApplied,     // this repair does not apply; try the next one
        RewriteRequest, // a retry with `request` must match exactly once
        PickCandidate,  // `candidate_index` already is the answer
    };
    Kind kind = Kind::NotApplied;
    Request request{};
    size_t candidate_index = 0;
    const char* id = "";
    const char* note = "";
};

// `user` is the entry's payload; data records pass their own address.
using RepairFn = RepairResult (*)(RepairContext&, const void* user);

// ---- data-record form -------------------------------------------------------
// A record states what a package declares (expected) and what the client
// actually has now (repaired). Both sides are single values: the rewrite stays
// an exact comparison, and two surviving candidates are still Ambiguous.
//
// The proof is measured against the live candidate list every time; the record
// only says which checks must hold. A record that cannot prove itself does
// nothing, it never falls back to a guess.
struct DataRepairRecord {
    const char* id;
    const char* character;          // nullptr = any character
    const char* resource;           // nullptr = either resource
    int32_t component;              // -1 = any component

    const char* expected_mesh;      // request.mesh_name must equal this
    uint64_t expected_indices;      // request.indices must equal this when known
    bool expected_indices_known;

    const char* repaired_mesh;      // the mesh the same slot holds now
    uint64_t repaired_indices;      // only written when repaired_indices_known
    bool repaired_indices_known;

    // BundleManifest.Version this was observed under. A mismatch is reported
    // but never changes the outcome; a stale record simply stops proving.
    const char* observed_at;
};

// A hand written repair. Kept separate from the data table so neither form has
// to pretend to be the other.
struct FunctionRepair {
    const char* character; // nullptr = any character
    const char* resource;  // nullptr = either resource
    int32_t component;     // -1 = any component
    RepairFn fn;
    const void* user;
    // Consulted even when the ordinary match already succeeded, because the
    // package asked for an unchecked match (check_indices == false) and a
    // satisfied name therefore proves nothing about geometry. Declared per
    // repair on purpose: it is a choice, not a default.
    bool also_when_unchecked;
};

// Defined once per program: the data table lives in compat_records.inc, the
// hand written repairs in compat_handlers.inc.
std::span<const DataRepairRecord> CompatDataRepairRecords();
std::span<const FunctionRepair> CompatFunctionRepairs();

namespace detail {
inline bool RepairScopeMatches(const DataRepairRecord& r,const RepairContext& ctx) {
    if (r.character && std::string_view(r.character) != std::string_view(ctx.adapter.id)) return false;
    if (r.resource && std::string_view(r.resource) != ctx.request.resource) return false;
    if (r.component >= 0 && uint32_t(r.component) != ctx.component_id) return false;
    return true;
}
inline std::string_view PathLeaf(std::string_view path) {
    const auto slash=path.rfind('/');
    return slash==std::string_view::npos?path:path.substr(slash+1);
}
inline RepairResult ApplyDataRecord(RepairContext& ctx,const DataRepairRecord& r) {
    RepairResult out;
    if (!RepairScopeMatches(r,ctx)) return out;
    if (std::string_view(r.expected_mesh) != ctx.request.mesh_name) return out;
    if (r.expected_indices_known && ctx.request.indices != r.expected_indices) return out;

    // Proof: exactly one live candidate sits in the slot the package names
    // while already carrying the repaired mesh, with the same geometry count.
    // Anything other than exactly one hit is refused.
    size_t hits=0,hit=0;
    for (size_t i=0;i<ctx.candidates.size();++i) {
        const auto& c=ctx.candidates[i];
        if (!c.renderer || !c.pristine.mesh) continue;
        if (c.pristine.origin!=DonorOrigin::Pristine && c.pristine.origin!=DonorOrigin::SavedOriginal) continue;
        if (c.key.character!=ctx.request.character || c.key.resource!=ctx.request.resource) continue;
        if (c.key.region!=ctx.request.region) continue;
        if (std::string_view(c.pristine.name) != std::string_view(r.repaired_mesh)) continue;
        if (r.expected_indices_known &&
            (!c.pristine.indices_known || c.pristine.indices != r.expected_indices)) continue;
        if (PathLeaf(c.key.path) != std::string_view(r.expected_mesh)) continue;
        ++hits; hit=i;
    }
    if (hits!=1) return out;

    const auto& receiver=ctx.candidates[hit];
    if (!ctx.request.verified_receiver_path.empty() &&
        ctx.request.verified_receiver_path != receiver.key.path) return out;

    out.kind=RepairResult::Kind::RewriteRequest;
    out.request=ctx.request;
    out.request.mesh_name=r.repaired_mesh;
    if (r.repaired_indices_known) {
        out.request.indices=r.repaired_indices;
        out.request.check_indices=true;
    }
    // The slot is the identity here. Pinning the receiver path keeps the retry
    // unambiguous: the repaired mesh is usually shared with another renderer.
    out.request.verified_receiver_path=receiver.key.path;
    out.id=r.id;
    out.note="expected->repaired";
    return out;
}
}

// The interpreter for the data-record form. Registered with a record's own
// address as payload.
inline RepairResult RepairFromDataRecord(RepairContext& ctx,const void* user) {
    if (!user) return {};
    return detail::ApplyDataRecord(ctx,*static_cast<const DataRepairRecord*>(user));
}

// Consult the registry given an already computed ordinary match.
// Returns the original result unless a repair produced a verified Matched one.
//
// `applied` receives the id of the repair that produced the result, or nullptr.
// That is the only channel a caller needs to know it did not get an ordinary
// match, so no repair has to leak its own state into the caller.
template<class Validate,class Report>
Match SelectRepairedFrom(RepairContext ctx,Match match,Validate&& validate,Report&& report,
    const char** applied=nullptr) {
    if (applied) *applied=nullptr;
    if (match.status==MatchStatus::Ambiguous || match.status==MatchStatus::InvalidIndex) return match;

    // Only an unchecked satisfied match is re-opened; a checked one is already
    // the exact answer the package asked for.
    const bool consult_matched=match.status==MatchStatus::Matched && !ctx.request.check_indices;
    const Request original=ctx.request;

    if (match.status==MatchStatus::Missing) {
        for (const auto& r:CompatDataRepairRecords()) {
            auto result=RepairFromDataRecord(ctx,&r);
            if (result.kind==RepairResult::Kind::NotApplied) { ctx.request=original; continue; }
            auto retry=SelectUnique(ctx.candidates,result.request,validate);
            if (retry.status!=MatchStatus::Matched) { ctx.request=original; continue; }
            ctx.request=result.request;
            report(result.id,result.note);
            if (applied) *applied=result.id;
            return retry;
        }
    }
    for (const auto& e:CompatFunctionRepairs()) {
        if (!e.fn) continue;
        if (match.status!=MatchStatus::Missing && !(consult_matched && e.also_when_unchecked)) continue;
        if (e.character && !ctx.adapter.id) continue;
        if (e.character && std::string_view(e.character) != std::string_view(ctx.adapter.id)) continue;
        if (e.resource && std::string_view(e.resource) != ctx.request.resource) continue;
        if (e.component >= 0 && uint32_t(e.component) != ctx.component_id) continue;
        auto result=e.fn(ctx,e.user);
        if (result.kind==RepairResult::Kind::NotApplied) { ctx.request=original; continue; }
        if (result.kind==RepairResult::Kind::PickCandidate) {
            if (result.candidate_index>=ctx.candidates.size() ||
                !ctx.candidates[result.candidate_index].renderer ||
                !validate(ctx.candidates[result.candidate_index])) { ctx.request=original; continue; }
            // A picked candidate answers the match; the request is left as the
            // package declared it so a failed repair cannot leave a rewrite.
            ctx.request=original;
            report(result.id,result.note);
            if (applied) *applied=result.id;
            return {MatchStatus::Matched,result.candidate_index,1};
        }
        auto retry=SelectUnique(ctx.candidates,result.request,validate);
        if (retry.status!=MatchStatus::Matched) { ctx.request=original; continue; }
        ctx.request=result.request;
        report(result.id,result.note);
        if (applied) *applied=result.id;
        return retry;
    }
    ctx.request=original;
    return match;
}

template<class Validate,class Report>
Match SelectRepaired(RepairContext ctx,Validate&& validate,Report&& report,const char** applied=nullptr) {
    auto match=SelectUnique(ctx.candidates,ctx.request,validate);
    return SelectRepairedFrom(ctx,match,validate,report,applied);
}
}
