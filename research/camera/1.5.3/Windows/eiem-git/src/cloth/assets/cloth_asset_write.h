#pragma once
#include "cloth_asset_model.h"
#include "../resources/cloth_resource_limits.h"

namespace eiem_cloth_asset {
struct ByteWriter {
  Bytes bytes;bool big=false;
  explicit ByteWriter(bool be=false):big(be){}
  void U(uint64_t value,size_t n){Need(n&&n<=8,"auto-write-integer-width");for(size_t i=0;i<n;++i)bytes.push_back(uint8_t(value>>(8*(big?n-1-i:i))));}
  void Raw(const uint8_t *p,size_t n){Need(n<=MaxFileBytes-bytes.size(),"auto-write-byte-budget");bytes.insert(bytes.end(),p,p+n);}
  void Raw(const Bytes &b){Raw(b.data(),b.size());}
  void Align(size_t n=4){while(bytes.size()%n)bytes.push_back(0);}
  void Z(const std::string &s){Need(s.find('\0')==std::string::npos,"auto-write-null-name");Raw(reinterpret_cast<const uint8_t*>(s.data()),s.size());U(0,1);}
};
inline Value Integer(int64_t x){Value v;v.kind=Value::Integer;v.integer=x;return v;}
inline Value Real(double x){Need(std::isfinite(x),"auto-write-nonfinite");Value v;v.kind=Value::Real;v.real=x;return v;}
inline Value Text(std::string x){Value v;v.kind=Value::String;v.text=std::move(x);return v;}
inline Value Array(std::vector<Value> x){Value v;v.kind=Value::Array;v.items=std::move(x);return v;}
inline Value Binary(Bytes x){Value v;v.kind=Value::Blob;v.bytes=std::move(x);return v;}
inline Value Object(std::vector<std::pair<std::string,Value>> x){Value v;v.kind=Value::Object;v.fields=std::move(x);return v;}
inline Value &Field(Value &v,const std::string &name){Need(v.kind==Value::Object,"auto-write-object");for(auto &f:v.fields)if(f.first==name)return f.second;throw std::runtime_error("auto-write-field-missing");}
inline Value Pointer(int64_t id){return Object({{"m_FileID",Integer(0)},{"m_PathID",Integer(id)}});}
inline Value MatrixValue(const Matrix &m){Value v=Object({});for(int i=0;i<4;++i)for(int j=0;j<4;++j)v.fields.push_back({"e"+std::to_string(i)+std::to_string(j),Real(float(m[j*4+i]))});return v;}
inline Value VectorValue(Point p){return Object({{"x",Real(float(p[0]))},{"y",Real(float(p[1]))},{"z",Real(float(p[2]))}});}
inline void WriteValue(ByteWriter &out,const SerializedType &tree,size_t index,const Value &v,unsigned depth=0) {
  Need(depth<128&&index<tree.nodes.size(),"auto-write-tree-budget");const auto &n=tree.nodes[index];const auto &t=n.type;bool align=(n.meta&0x4000)!=0;int width=0;bool sign=false;
  if(t=="UInt8"||t=="char"||t=="bool")width=1;else if(t=="SInt8"){width=1;sign=true;}
  else if(t=="UInt16"||t=="unsigned short")width=2;else if(t=="SInt16"||t=="short"){width=2;sign=true;}
  else if(t=="UInt32"||t=="unsigned int"||t=="Type*")width=4;else if(t=="SInt32"||t=="int"){width=4;sign=true;}
  else if(t=="UInt64"||t=="unsigned long long"||t=="FileSize")width=8;else if(t=="SInt64"||t=="long long"){width=8;sign=true;}
  if(width){const auto x=v.Int();if(width<8){const int bits=width*8;Need(sign?(x>=-(int64_t(1)<<(bits-1))&&x<(int64_t(1)<<(bits-1))):(x>=0&&uint64_t(x)<(uint64_t(1)<<bits)),"auto-write-integer-range");}if(t=="bool")Need(x==0||x==1,"auto-write-bool");out.U(uint64_t(x),width);}
  else if(t=="float"){const float f=float(v.Number());Need(std::isfinite(f),"auto-write-float-overflow");uint32_t b=0;memcpy(&b,&f,4);out.U(b,4);}
  else if(t=="double"){const double f=v.Number();uint64_t b=0;memcpy(&b,&f,8);out.U(b,8);}
  else if(t=="string"){const auto &s=v.Text();Need(s.size()<=65536,"auto-write-string-budget");out.U(s.size(),4);out.Raw(reinterpret_cast<const uint8_t*>(s.data()),s.size());out.Align();}
  else if(t=="TypelessData"){const auto raw=Blob(v);out.U(raw.size(),4);out.Raw(raw);}
  else if(!n.children.empty()&&tree.nodes[n.children[0]].type=="Array"){
    const auto &a=tree.nodes[n.children[0]];Need(a.children.size()==2,"auto-write-array-tree");const auto element=a.children[1];align|=(a.meta&0x4000)!=0;
    if(v.kind==Value::Blob){const auto &et=tree.nodes[element];Need((et.type=="UInt8"||et.type=="SInt8"||et.type=="char")&&!(et.meta&0x4000),"auto-write-blob-element");out.U(v.bytes.size(),4);out.Raw(v.bytes);}
    else {const auto &list=v.List();Need(list.size()<=1000000,"auto-write-array-budget");out.U(list.size(),4);for(const auto &e:list)WriteValue(out,tree,element,e,depth+1);}
  } else if(!n.children.empty()){
    Need(v.kind==Value::Object&&v.fields.size()==n.children.size(),"auto-write-field-set");for(auto child:n.children)WriteValue(out,tree,child,v.At(tree.nodes[child].name),depth+1);
  } else {Need(n.bytes>=0&&v.kind==Value::Blob&&v.bytes.size()==size_t(n.bytes),"auto-write-unknown-leaf");out.Raw(v.bytes);}
  if(align)out.Align();
}
inline Bytes Repack(SerializedFile &source,const std::map<int64_t,std::map<std::string,Value>> &edits) {
  Need(!source.big&&!source.scriptCount&&source.externals.empty()&&source.refs.empty()&&!edits.empty()&&edits.size()<=16,"auto-repack-dependent-source");
  Need(source.objectTable>=48&&source.objectTableEnd<=source.metadataEnd&&source.metadataEnd<=source.bytes.size(),"auto-repack-metadata-range");
  ByteWriter meta,data;meta.Raw(source.bytes.data(),source.objectTable);meta.U(edits.size(),4);
  for(const auto &entry:edits){CheckCancel(source.cancel);const auto o=source.objects.at(entry.first);const auto &root=source.Get(entry.first);const auto &tree=source.types[o.type];
    Need(source.Class(entry.first)==43||source.Class(entry.first)==142,"auto-repack-object-class");for(const auto &e:entry.second)Need(root.Has(e.first),"auto-repack-unknown-field");ByteWriter body;
    for(auto child:tree.nodes[0].children){const auto &n=tree.nodes[child];const auto &value=root.At(n.name);auto found=entry.second.find(n.name);
      Need(value.start<=o.size&&value.length<=o.size-value.start&&body.bytes.size()%4==value.start%4,"auto-repack-field-alignment");
      if(found==entry.second.end())body.Raw(source.bytes.data()+o.offset+value.start,value.length);else WriteValue(body,tree,child,found->second);
    }
    Need(body.bytes.size()<=eiem_cloth_resource::RuntimeBytes,"auto-repack-object-budget");meta.Align();data.Align(8);meta.U(uint64_t(o.id),8);meta.U(data.bytes.size(),8);meta.U(body.bytes.size(),4);meta.U(o.type,4);data.Raw(body.bytes);
  }
  meta.Raw(source.bytes.data()+source.objectTableEnd,source.metadataEnd-source.objectTableEnd);const size_t metadata=meta.bytes.size()-48;meta.Align(16);const size_t offset=meta.bytes.size();meta.Raw(data.bytes);
  ByteWriter header(true);header.U(metadata,4);header.U(meta.bytes.size(),8);header.U(offset,8);header.U(0,8);std::copy(header.bytes.begin(),header.bytes.end(),meta.bytes.begin()+20);
  SerializedFile check;check.Open("private",meta.bytes);Need(check.objects.size()==edits.size(),"auto-repack-object-readback");
  for(const auto &entry:edits){const auto &before=source.Get(entry.first),&after=check.Get(entry.first);const auto &old=source.objects.at(entry.first),&now=check.objects.at(entry.first);const auto &tree=source.types[old.type];
    for(auto child:tree.nodes[0].children){const auto &name=tree.nodes[child].name;const auto &v=after.At(name);auto found=entry.second.find(name);ByteWriter wanted;
      if(found==entry.second.end()){const auto &b=before.At(name);wanted.Raw(source.bytes.data()+old.offset+b.start,b.length);}else {wanted.bytes.resize(v.start%4);WriteValue(wanted,tree,child,found->second);wanted.bytes.erase(wanted.bytes.begin(),wanted.bytes.begin()+v.start%4);}
      Need(wanted.bytes.size()==v.length&&std::equal(wanted.bytes.begin(),wanted.bytes.end(),check.bytes.begin()+now.offset+v.start),"auto-repack-field-readback");
    }
  }
  return std::move(meta.bytes);
}
inline Bytes PrivateBundle(const Bytes &cab,const std::string &name) {
  Need(eiem_cloth_resource::Fits(cab.size(),eiem_cloth_resource::RuntimeBytes)&&name.size()==36&&name.rfind("CAB-",0)==0&&name.substr(4).find_first_not_of("0123456789abcdef")==std::string::npos,"auto-private-cab-input");
  auto enc32=[](uint32_t n){const auto x=RotL(n^0xf74324ee,18);const auto lo=uint16_t(x);return std::array<uint16_t,2>{uint16_t((x>>16)^lo^0xa121),lo};};
  auto enc64=[](uint64_t n){const auto x=RotL64(n^0xa4f1a11747816520ull,50);const auto lo=uint32_t(x);return std::array<uint32_t,2>{uint32_t((x>>32)^lo^0xdad76848),lo};};
  auto count=[](uint32_t n,uint32_t key,uint32_t outer){const auto x=RotL(n^key,18);const uint32_t lo=uint16_t(x);return ((((x>>16)^lo)<<16)|lo)^outer;};
  const auto sz=enc32(uint32_t(cab.size()));const uint16_t x=uint16_t(sz[1]^0x523f),rot=uint16_t((x<<2)|(x>>14));const uint16_t flags=uint16_t((((rot>>8)^uint8_t(rot))<<8)|uint8_t(rot))^0x9cd6;
  ByteWriter info(true);info.U(count(1,0x91ce0a4f,0x23f77b8a),4);info.U(sz[0],2);info.U(sz[0],2);info.U(sz[1],2);info.U(flags,2);info.U(sz[1],2);info.U(count(1,0xe4c1d9f2,0x6b0ae55d),4);
  const auto zero=enc64(0),length=enc64(cab.size());const auto n=RotL(4^0xf13927c4^length[0],18),lo=uint32_t(uint16_t(n));
  info.U(((((n>>16)^lo)<<16)|lo)^0x8e06a9f8,4);info.U(length[0],4);info.U(zero[1],4);info.U(zero[0],4);
  for(size_t i=0;i<name.size();++i)info.U(uint8_t(name[i])^uint8_t(i^0x97),1);info.U(0,1);info.U(length[1],4);
  const auto packed=enc32(uint32_t(info.bytes.size()));info.Align(16);const auto total=enc64(48+info.bytes.size()+cab.size());ByteWriter result(true);
  result.U(0x97cca8abcc53934eull,8);result.U(packed[1],2);result.U(0xee156f43,4);result.U(0xee156f43^8,4);result.U(total[1],4);result.U(0xee156f43^0xa7f49310^0x240,4);result.U(packed[0],2);result.U(0x2246fc0d,4);result.U(packed[1],2);result.U(total[0],4);result.U(packed[0],2);result.U(0,1);result.Align(16);result.Raw(info.bytes);result.Raw(cab);
  const auto check=DecodeBundle(result.bytes);Need(check.size()==1&&check[0].name==name&&check[0].bytes==cab,"auto-private-bundle-readback");return std::move(result.bytes);
}
}
