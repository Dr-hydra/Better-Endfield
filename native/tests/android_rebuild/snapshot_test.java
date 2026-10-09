package dev.betterendfield.next;
final class SnapshotTest {
 public static void main(String[]args){
  assert !RuntimeSnapshot.parse(null).connected;
  assert !RuntimeSnapshot.parse("BE_STATUS_V1\n").connected;
  RuntimeSnapshot s=RuntimeSnapshot.parse("BE_RUNTIME_V1\nruntime=startup_complete\nbetterendfieldnext.camera=ready\ncamera.capabilities=9\ncamera.active=8\nui.hud_hidden=1\n");
  assert s.connected && s.cameraAvailable(1) && !s.cameraAvailable(2) && s.cameraActive(8) && s.hudHidden();
  assert s.summary().contains("冻结");
  assert RuntimeSnapshot.parse("BE_RUNTIME_V1\nx=failed_contract\n").failed();
  assert RuntimeSnapshot.parse("BE_RUNTIME_V1\ncamera.active=invalid\n").number("camera.active")==0;
  System.out.println("PASS runtime snapshot parser and configured-versus-ready semantics");
 }
}
