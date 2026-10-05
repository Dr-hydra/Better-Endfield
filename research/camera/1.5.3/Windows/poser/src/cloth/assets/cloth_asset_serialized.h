#pragma once
#include "cloth_asset_bundle.h"
#include <unordered_map>
#include <cmath>
#include <memory>
namespace eiem_cloth_asset {
struct Value {
  enum Kind{Null,Integer,Real,String,Array,Object,Blob} kind=Null;
  int64_t integer=0;double real=0;std::string text;Bytes bytes;
  std::vector<Value> items;std::vector<std::pair<std::string,Value>> fields;
  size_t start=0,length=0;
  const Value &At(const std::string &name) const {for(const auto &p:fields)if(p.first==name)return p.second;static const Value empty;return empty;}
  bool Has(const std::string &name) const {return At(name).kind!=Null;}
  int64_t Int() const {Need(kind==Integer,"asset-expected-integer");return integer;}
  double Number() const {Need(kind==Real||kind==Integer,"asset-expected-number");const double n=kind==Real?real:double(integer);Need(std::isfinite(n),"asset-nonfinite-number");return n;}
  const std::string &Text() const {Need(kind==String,"asset-expected-string");return text;}
  const std::vector<Value> &List() const {Need(kind==Array,"asset-expected-array");return items;}
  int64_t LocalRef() const {Need(At("m_FileID").Int()==0,"asset-external-reference");return At("m_PathID").Int();}
};
struct TypeNode {std::string type,name;int32_t bytes=0,meta=0;uint8_t level=0;std::vector<size_t> children;};
struct SerializedType {int32_t classId=0;std::vector<TypeNode> nodes;std::array<std::string,3> reference;};
struct SerializedObject {int64_t id=0;size_t offset=0,size=0;int type=0;};
struct SerializedFile {
  Bytes bytes;bool big=false;std::string name,sha,unity;
  std::vector<SerializedType> types,refs;std::vector<std::string> externals;
  std::map<int64_t,SerializedObject> objects;std::unordered_map<int64_t,Value> values;
  size_t objectTable=0,objectTableEnd=0,metadataEnd=0;uint32_t scriptCount=0;
  const std::atomic<bool> *cancel=nullptr;
  std::shared_ptr<size_t> valueBudget=std::make_shared<size_t>(2000000);
  static SerializedType ReadType(Reader &r,bool reference=false) {
    SerializedType type;type.classId=r.I32();Need(r.U8()<=1,"asset-stripped-flag");const auto script=int16_t(r.U16());
    if((reference&&script>=0)||type.classId==114)r.Take(16);r.Take(16);
    const auto count=r.Count(100000),length=r.Count(10000000);Need(count>0,"asset-empty-type-tree");
    struct Names{uint32_t type,name;};std::vector<Names> names;type.nodes.reserve(count);
    for(uint32_t n=0;n<count;++n){r.U16();TypeNode node;node.level=r.U8();r.U8();const auto t=r.U32(),f=r.U32();node.bytes=r.I32();r.I32();node.meta=r.I32();r.U64();names.push_back({t,f});type.nodes.push_back(std::move(node));}
    const auto stringData=r.Take(length);
    auto resolve=[&](uint32_t index){if(index&0x80000000){index&=0x7fffffff;for(const auto &s:UnityCommonStrings)if(s.offset==index)return std::string(s.value);throw std::runtime_error("asset-type-common-string");}
      Need(index<length,"asset-type-string-offset");Reader sr(stringData+index,length-index);return sr.Z();};
    std::vector<size_t> stack;
    for(size_t n=0;n<type.nodes.size();++n){auto &node=type.nodes[n];node.type=resolve(names[n].type);node.name=resolve(names[n].name);
      while(!stack.empty()&&type.nodes[stack.back()].level>=node.level)stack.pop_back();
      if(n==0)Need(node.level==0,"asset-type-root-level");else Need(!stack.empty()&&node.level==type.nodes[stack.back()].level+1,"asset-type-child-level");
      if(!stack.empty())type.nodes[stack.back()].children.push_back(n);stack.push_back(n);
    }
    if(reference){for(auto &s:type.reference)s=r.Z();}else r.Take(size_t(r.Count(100000))*4);return type;
  }
  void Open(std::string source,Bytes content) {
    name=std::move(source);bytes=std::move(content);sha=Digest(bytes);Reader r(bytes,true);r.U32();r.U32();Need(r.U32()==22,"asset-serialized-version");r.U32();const auto endian=r.U8();Need(endian<=1,"asset-serialized-endian");r.Take(3);
    const auto metadata=r.U32();const auto size=r.U64(),offset=r.U64();r.U64();Need(size==bytes.size()&&offset<=size&&uint64_t(metadata)+48<=offset,"asset-serialized-header");r.big=big=endian!=0;
    unity=r.Z();r.I32();Need(r.U8()==1,"asset-type-tree-required");types.clear();refs.clear();objects.clear();values.clear();externals.clear();
    for(uint32_t n=r.Count(10000);n;--n){CheckCancel(cancel);types.push_back(ReadType(r));}
    objectTable=r.pos;
    for(uint32_t n=r.Count(100000);n;--n){r.Align();SerializedObject o;o.id=r.I64();const auto start=r.U64();o.size=r.U32();o.type=r.I32();
      Need(start<=size-offset&&o.size<=size-offset-start&&o.type>=0&&size_t(o.type)<types.size(),"asset-serialized-object-range");o.offset=size_t(offset+start);Need(objects.emplace(o.id,o).second,"asset-serialized-duplicate-object");}
    objectTableEnd=r.pos;scriptCount=r.Count(100000);
    for(uint32_t n=scriptCount;n;--n){r.I32();r.Align();r.U64();}
    for(uint32_t n=r.Count(100000);n;--n){r.Z();r.Take(16);r.I32();externals.push_back(r.Z());}
    for(uint32_t n=r.Count(10000);n;--n)refs.push_back(ReadType(r,true));r.Z();Need(r.pos==uint64_t(metadata)+48,"asset-serialized-metadata-tail");metadataEnd=r.pos;
  }
  Value ReadValue(Reader &r,const SerializedType &tree,size_t ix,size_t &budget,unsigned depth=0) const {
    Need(depth<=128&&ix<tree.nodes.size()&&budget>0&&valueBudget&&*valueBudget>0,"asset-value-budget");--budget;--*valueBudget;if((budget&4095)==0)CheckCancel(cancel);
    const auto &n=tree.nodes[ix];const auto &t=n.type;Value v;v.start=r.pos;bool align=(n.meta&0x4000)!=0;
    int primitive=0;bool sign=false;
    if(t=="UInt8"||t=="char"||t=="bool")primitive=1;
    else if(t=="SInt8"){primitive=1;sign=true;}
    else if(t=="UInt16"||t=="unsigned short")primitive=2;
    else if(t=="SInt16"||t=="short"){primitive=2;sign=true;}
    else if(t=="UInt32"||t=="unsigned int"||t=="Type*")primitive=4;
    else if(t=="SInt32"||t=="int"){primitive=4;sign=true;}
    else if(t=="UInt64"||t=="unsigned long long"||t=="FileSize")primitive=8;
    else if(t=="SInt64"||t=="long long"){primitive=8;sign=true;}
    if(primitive){auto raw=r.U(primitive);v.kind=Value::Integer;if(sign&&primitive<8&&(raw&(uint64_t(1)<<(primitive*8-1))))raw|=~uint64_t(0)<<(primitive*8);v.integer=int64_t(raw);if(t=="bool")Need(v.integer==0||v.integer==1,"asset-bool-value");}
    else if(t=="float"||t=="double"){v.kind=Value::Real;if(t=="float"){uint32_t bits=r.U32();float f;memcpy(&f,&bits,4);v.real=f;}else{uint64_t bits=r.U64();memcpy(&v.real,&bits,8);}}
    else if(t=="string"){v.kind=Value::String;v.text=r.Text(r.Count(16*1024*1024));r.Align();}
    else if(t=="TypelessData"){v.kind=Value::Blob;v.bytes=r.Blob(r.Count(uint32_t(MaxFileBytes)));}
    else if(t=="ReferencedObject") {
      v.kind=Value::Object;for(auto child:n.children){const auto &c=tree.nodes[child];
        if(c.type=="ReferencedObjectData") {
          const auto &descriptor=v.At("type");const std::array<std::string,3> key{descriptor.At("class").Text(),descriptor.At("ns").Text(),descriptor.At("asm").Text()};const SerializedType *target=nullptr;
          for(const auto &ref:refs)if(ref.reference==key){Need(!target,"asset-managed-reference-ambiguous");target=&ref;}Need(target,"asset-managed-reference-missing");v.fields.push_back({c.name,ReadValue(r,*target,0,budget,depth+1)});
        }else v.fields.push_back({c.name,ReadValue(r,tree,child,budget,depth+1)});
      }
    }
    else if(!n.children.empty()&&tree.nodes[n.children[0]].type=="Array") {
      const auto &a=tree.nodes[n.children[0]];Need(a.children.size()==2,"asset-array-type");const auto count=r.Count(10000000);const auto element=a.children[1];const auto &et=tree.nodes[element];
      Need(count<=r.Left()||count==0,"asset-array-count-range");align|=(a.meta&0x4000)!=0;
      if((et.type=="UInt8"||et.type=="SInt8"||et.type=="char")&&count>256){v.kind=Value::Blob;v.bytes=r.Blob(count);}
      else {Need(count<=budget,"asset-array-value-budget");v.kind=Value::Array;v.items.reserve(count);for(uint32_t k=0;k<count;++k)v.items.push_back(ReadValue(r,tree,element,budget,depth+1));}
    }
    else if(!n.children.empty()){v.kind=Value::Object;std::set<std::string> names;for(auto child:n.children){const auto &c=tree.nodes[child];Need(names.insert(c.name).second,"asset-value-duplicate-field");v.fields.push_back({c.name,ReadValue(r,tree,child,budget,depth+1)});}}
    else {Need(n.bytes>=0,"asset-value-unknown-leaf");v.kind=Value::Blob;v.bytes=r.Blob(size_t(n.bytes));}
    if(align)r.Align();v.length=r.pos-v.start;return v;
  }
  const Value &Get(int64_t id) {
    auto old=values.find(id);if(old!=values.end())return old->second;auto i=objects.find(id);Need(i!=objects.end(),"asset-object-not-found");const auto &o=i->second;Reader r(bytes.data()+o.offset,o.size,big);size_t budget=2000000;
    auto value=ReadValue(r,types[o.type],0,budget);Need(!r.Left(),"asset-object-unconsumed");return values.emplace(id,std::move(value)).first->second;
  }
  int Class(int64_t id) const {auto i=objects.find(id);Need(i!=objects.end(),"asset-object-class-missing");return types[i->second.type].classId;}
};
}
