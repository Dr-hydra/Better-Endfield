#pragma once
#include "../core/cloth_state.h"

namespace eiem_cloth_rebuild {
using eiem_cloth::Owner;
enum class Phase { Idle, Prepared, RetireOriginal, InstallCandidate, BuildCandidate,
  ActivateCandidate, Active, RetireCandidate, InstallOriginal, BuildOriginal, RestoreEnabled,
  Complete, Retained };
enum class Slot { Candidate, Original };
enum class Verdict { Waiting, Ready, Failed };
struct Identity {
  Owner owner{};
  uint64_t bbc=0, process=0, data=0, data2=0;
  bool Valid() const {
    return owner.session && owner.generation && owner.backend && owner.character &&
        bbc && process && data && data2;
  }
  bool operator==(const Identity &r) const {
    return owner==r.owner && bbc==r.bbc && process==r.process && data==r.data && data2==r.data2;
  }
};
struct Preconditions {
  bool exactMainBoundary=false, exclusiveOwner=false, snapshotComplete=false;
  bool configurationIndependent=false, selectionPreserved=false;
  bool constructionReferenceVerified=false, restoreRecipeVerified=false;
  bool pendingTeleportKnown=false, pendingTeleport=false;
  bool sourceReady=false, sourceBuildingKnown=false, sourceBuilding=true;
  bool Ready() const {
    return exactMainBoundary && exclusiveOwner && snapshotComplete &&
        configurationIndependent && selectionPreserved && constructionReferenceVerified &&
        restoreRecipeVerified && pendingTeleportKnown && !pendingTeleport && sourceReady &&
        sourceBuildingKnown && !sourceBuilding;
  }
};
struct Retirement {
  uint64_t process=0;
  int frame=-1;
  bool exactMainBoundary=false, readable=false, buildEnded=false, disposed=false;
  bool teamAbsent=false, monitoringAbsent=false, dirtyQueuesAbsent=false;
  bool colliderTeamsAbsent=false, teleportReleased=false, pendingTeleportCleared=false;
  bool Complete() const {
    return process && frame>=0 && exactMainBoundary && readable && buildEnded && disposed &&
        teamAbsent && monitoringAbsent && dirtyQueuesAbsent && colliderTeamsAbsent &&
        teleportReleased && pendingTeleportCleared;
  }
};
inline bool DisabledCandidateQueueRetirable(bool exactPrivate,bool disabled,bool idle,
    unsigned known,unsigned present,int parameterEntries,int skipEntries) {
  return exactPrivate&&disabled&&idle&&known==15&&!(present&12)&&
      parameterEntries>=0&&parameterEntries<=1&&skipEntries>=0&&skipEntries<=1&&
      bool(present&1)==bool(parameterEntries)&&bool(present&2)==bool(skipEntries);
}
struct RetirementBarrier {
  uint64_t process=0;
  int frame=-1;
  unsigned consecutive=0;
  bool Observe(const Retirement &r, uint64_t expected) {
    if (!expected || r.process!=expected || !r.Complete()) {
      process=0; frame=-1; consecutive=0; return false;
    }
    if (process!=expected || r.frame<frame) { process=expected; frame=-1; consecutive=0; }
    if (r.frame==frame) return consecutive>=2;
    frame=r.frame;
    if (consecutive<2) ++consecutive;
    return consecutive>=2;
  }
};
struct BuildObservation {
  Identity identity{};
  int frame=-1;
  bool exactMainBoundary=false, readable=false, building=true;
  bool valid=false, running=false, registered=false, error=false;
  bool graphVerified=false, selectionVerified=false, referenceVerified=false;
};
struct BuildAttempt {
  Identity identity{};
  bool issued=false, resultReported=false, returnKnown=false, accepted=false;
  uint64_t deadline=0;
  int issueFrame=-1, lastFrame=-1;
  unsigned readyReads=0;
  bool Issue(const Identity &id, uint64_t now, int frame) {
    if (issued || !id.Valid() || frame<0 || now>UINT64_MAX-8000) return false;
    issued=true; identity=id; deadline=now+8000; issueFrame=lastFrame=frame;
    return true;
  }
  void Result(bool known, bool value) {
    if (!issued || resultReported) return;
    resultReported=true; returnKnown=known; accepted=known && value;
  }
  Verdict Readiness(const BuildObservation &o) {
    if (!o.exactMainBoundary || !o.readable || o.frame<=issueFrame || o.frame<lastFrame) {
      readyReads=0; return Verdict::Waiting;
    }
    const bool ready=!o.building && o.valid && o.running && o.registered &&
        o.graphVerified && o.selectionVerified && o.referenceVerified;
    if (!ready) { readyReads=0; lastFrame=o.frame; return Verdict::Waiting; }
    if (o.frame!=lastFrame) { ++readyReads; lastFrame=o.frame; }
    return readyReads>=2 ? Verdict::Ready : Verdict::Waiting;
  }
  Verdict Observe(const BuildObservation &o, uint64_t now) {
    if (!issued) return Verdict::Waiting;
    if (!(o.identity==identity)) return Verdict::Failed;
    if (o.error || (returnKnown && !accepted) || now>=deadline) return Verdict::Failed;
    return Readiness(o);
  }
  Verdict ObserveLateRestore(const BuildObservation &o) {
    if(!issued || !(o.identity==identity) || o.error || (returnKnown && !accepted)) {
      readyReads=0; return Verdict::Waiting;
    }
    return Readiness(o);
  }
};

struct Transaction {
  Phase phase=Phase::Idle;
  Phase retainedFrom=Phase::Idle;
  Identity original{}, installed{}, planned{};
  uint64_t restoreData2=0;
  BuildAttempt candidate{}, restoration{};
  RetirementBarrier retirement{};
  bool cancelled=false, originalRetired=false, lease=false;
  bool disposeOriginalIssued=false, disposeCandidateIssued=false;
  bool installIssued=false;
  uint64_t retireDeadline=0;
  void Retain() { if(phase!=Phase::Retained) { retainedFrom=phase; phase=Phase::Retained; } }
  static bool Terminal(Phase p) { return p==Phase::Idle || p==Phase::Complete; }
  bool Begin(const Identity &source, const Preconditions &p, uint64_t constructionData2=0) {
    if (!Terminal(phase) || !source.Valid() || !p.Ready()) return false;
    *this={}; original=installed=source; restoreData2=constructionData2?constructionData2:source.data2;
    phase=Phase::Prepared; return true;
  }
  bool Disabled(const Identity &source, bool exactMainBoundary, bool readbackDisabled, uint64_t now) {
    if (phase!=Phase::Prepared || !(source==original) || !exactMainBoundary ||
        !readbackDisabled || now>UINT64_MAX-8000) return false;
    lease=true; phase=Phase::RetireOriginal; retireDeadline=now+8000; return true;
  }
  bool ReserveDispose(const Identity &current, bool exactMainBoundary, bool teleportReleased, bool noPendingTeleport) {
    if (!lease || !(current==installed) || !exactMainBoundary || !teleportReleased || !noPendingTeleport) return false;
    if (phase==Phase::RetireOriginal && !cancelled && !disposeOriginalIssued) {
      disposeOriginalIssued=true; return true;
    }
    if (phase==Phase::RetireCandidate && !disposeCandidateIssued) {
      disposeCandidateIssued=true; return true;
    }
    return false;
  }
  bool ObserveRetirement(const Retirement &r, uint64_t now) {
    if (phase==Phase::Retained && (retainedFrom==Phase::RetireOriginal || retainedFrom==Phase::RetireCandidate)) {
      if (!retirement.Observe(r,installed.process)) return false;
      phase=retainedFrom;
    }
    const bool old=phase==Phase::RetireOriginal;
    if (!old && phase!=Phase::RetireCandidate) return false;
    if (!(old ? disposeOriginalIssued : disposeCandidateIssued)) return false;
    if (!retirement.Observe(r,installed.process)) {
      if (now>=retireDeadline) Retain();
      return false;
    }
    originalRetired=true;
    retirement={}; installIssued=false;
    phase=old && !cancelled ? Phase::InstallCandidate : Phase::InstallOriginal;
    return true;
  }
  bool ReserveInstall(const Identity &expected, const Identity &current, bool exactMainBoundary) {
    if (!(current==installed) || !exactMainBoundary || installIssued || !originalRetired ||
        (phase!=Phase::InstallCandidate && phase!=Phase::InstallOriginal)) return false;
    if (!expected.Valid() || expected.bbc!=original.bbc || !(expected.owner==original.owner) ||
        expected.process==original.process || expected.process==installed.process ||
        (candidate.issued && expected.process==candidate.identity.process)) return false;
    if (phase==Phase::InstallOriginal ?
        (expected.data!=original.data || expected.data2!=restoreData2) :
        (cancelled || expected.data==original.data || expected.data2==original.data2)) return false;
    planned=expected; installIssued=true; return true;
  }
  bool AllowsInstallRepair(const Identity &current) const {
    const auto stage=phase==Phase::Retained?retainedFrom:phase;
    return installIssued && (stage==Phase::InstallCandidate || stage==Phase::InstallOriginal) &&
        current.owner==planned.owner && current.bbc==planned.bbc &&
        (current.process==installed.process || current.process==planned.process) &&
        (current.data==installed.data || current.data==planned.data) &&
        (current.data2==installed.data2 || current.data2==planned.data2);
  }
  bool Installed(const Identity &id, bool exactMainBoundary) {
    if (!installIssued || !exactMainBoundary || !(id==planned)) return false;
    const auto stage=phase==Phase::Retained ? retainedFrom : phase;
    if (stage==Phase::InstallOriginal) {
      if (id.data!=original.data || id.data2!=restoreData2) return false;
      installed=id; phase=Phase::BuildOriginal; return true;
    }
    if (stage!=Phase::InstallCandidate || id.data==original.data || id.data2==original.data2)
      return false;
    installed=id; phase=Phase::BuildCandidate;
    if (cancelled) {
      phase=Phase::RetireCandidate; retirement={};
    }
    return true;
  }
  bool ReserveBuild(bool exactMainBoundary, uint64_t now, int frame) {
    if (!exactMainBoundary) return false;
    if (phase==Phase::BuildCandidate && !cancelled) return candidate.Issue(installed,now,frame);
    if (phase==Phase::BuildOriginal) return restoration.Issue(installed,now,frame);
    return false;
  }
  void BuildResult(bool known, bool accepted) {
    if (phase==Phase::BuildCandidate) candidate.Result(known,accepted);
    if (phase==Phase::BuildOriginal) restoration.Result(known,accepted);
  }
  void Cancel(uint64_t now) {
    cancelled=true;
    if(phase==Phase::Retained && (retainedFrom==Phase::BuildCandidate ||
        retainedFrom==Phase::ActivateCandidate || retainedFrom==Phase::Active)) phase=retainedFrom;
    if (phase==Phase::Prepared) { phase=Phase::Complete; return; }
    if (phase==Phase::InstallCandidate && !installIssued) { phase=Phase::InstallOriginal; return; }
    if (phase==Phase::InstallCandidate && installIssued) { Retain(); return; }
    if (phase==Phase::BuildCandidate || phase==Phase::ActivateCandidate || phase==Phase::Active) {
      phase=Phase::RetireCandidate; retirement={};
      retireDeadline=now>UINT64_MAX-8000 ? UINT64_MAX : now+8000;
    }
  }
  Verdict ObserveBuild(const BuildObservation &o, uint64_t now) {
    if(phase==Phase::Retained && retainedFrom==Phase::BuildOriginal) {
      const auto result=restoration.ObserveLateRestore(o);
      if(result==Verdict::Ready) phase=Phase::RestoreEnabled;
      return result;
    }
    if (phase!=Phase::BuildCandidate && phase!=Phase::BuildOriginal) return Verdict::Waiting;
    const bool restore=phase==Phase::BuildOriginal;
    const auto result=(restore ? restoration : candidate).Observe(o,now);
    if (result==Verdict::Ready) phase=restore ? Phase::RestoreEnabled : Phase::ActivateCandidate;
    if (result==Verdict::Failed) {
      if (restore) Retain();
      else Cancel(now);
    }
    return result;
  }
  bool Activated(const Identity &id, bool exactMainBoundary, bool componentEnabled,
                 bool processEnabled, bool inputAndGraphStillVerified) {
    if (phase!=Phase::ActivateCandidate || cancelled || !(id==installed) || !exactMainBoundary ||
        !componentEnabled || !processEnabled || !inputAndGraphStillVerified) return false;
    phase=Phase::Active; return true;
  }
  bool CanReturnData2(const Identity &id) const {
    const auto stage=phase==Phase::Retained?retainedFrom:phase;
    return stage==Phase::RestoreEnabled && restoration.readyReads>=2 &&
        id.owner==installed.owner && id.bbc==installed.bbc && id.process==installed.process &&
        id.data==original.data && (id.data2==restoreData2 || id.data2==original.data2);
  }
  bool ReturnedData2(const Identity &id,bool boundary) {
    if(!boundary || !CanReturnData2(id) || id.data2!=original.data2) return false;
    installed=id; return true;
  }
  bool FinishRestore(const Identity &id, bool boundary, bool enabledMatchesSnapshot) {
    const bool untouched=phase==Phase::RetireOriginal && cancelled && !disposeOriginalIssued;
    const bool late=phase==Phase::Retained && retainedFrom==Phase::RestoreEnabled;
    if ((!untouched && phase!=Phase::RestoreEnabled && !late) || !boundary || !enabledMatchesSnapshot ||
        !(id==(untouched ? original : installed)) || id.data2!=original.data2) return false;
    lease=false; phase=Phase::Complete; return true;
  }
};
}
