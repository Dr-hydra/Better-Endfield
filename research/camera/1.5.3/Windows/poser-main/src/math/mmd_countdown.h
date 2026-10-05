#pragma once
#include <algorithm>
#include <cmath>

namespace mmd {
// Wall-clock preparation, independent of motion speed and the 30 FPS timeline.
struct PlaybackCountdown {
  bool active=false, prepared=false, running=false;
  double remaining=0, lastNow=0;
  void cancel() { *this={}; }
  void arm(bool enabled,double seconds) {
    cancel();
    if(enabled&&std::isfinite(seconds)) {active=true;remaining=(std::max)(1.,(std::min)(10.,seconds));}
  }
  void pause() {running=false;}
  void update(double now,bool ready,bool playing) {
    if(!active)return;
    if(!ready) {running=false;lastNow=now;return;}
    if(prepared&&running&&playing)remaining-=(std::max)(0.,now-lastNow);
    prepared=true;lastNow=now;running=playing;
    if(remaining<=0)cancel();
  }
  int display() const {return active&&prepared?int(std::ceil(remaining)):0;}
};
}
