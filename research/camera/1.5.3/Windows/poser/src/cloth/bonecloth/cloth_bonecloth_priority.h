#pragma once
static bool ClothBoneAcceptedConnections(const ClothBoneProfile &p) {
  struct Accepted {const char *signature,*prefab;};
  static constexpr Accepted accepted[]{
    {"e7c36ae1e1bb05d4a47e476f92f995ef1be6351df71e5f482de1a57744cb0bb2","041f11d46ab17fa722a388441aa120e95bada4623d546b65b77c8a847c368b68"},
    {"e678d07ec5e393f9c7367f5734ef2a7a9045f99867b2c60d2e7ae8a476528214","6cf26f15cca59ebcb951a5daa1dd5ca61efc3455aa5e6a94f885c55961e63459"},
    {"7363fffb0e2875eba5569df28941752ffaa31a33ee20216823e5c1dfaf09b1b2","6cf26f15cca59ebcb951a5daa1dd5ca61efc3455aa5e6a94f885c55961e63459"},
    {"152870c4b712f08b23652a5037ee4a9b3a19968507c13cac0e61a25d12586b67","0451a679bbc968fabe8b8a8acec985488afbe641ab04b6c4ac3d86d1e99db876"},
  };
  if(!p.signature||!p.prefabSha)return false;
  for(const auto &a:accepted)if(!strcmp(p.signature,a.signature)&&!strcmp(p.prefabSha,a.prefab))return true;
  return false;
}
