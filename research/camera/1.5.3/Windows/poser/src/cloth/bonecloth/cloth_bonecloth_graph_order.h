#pragma once
#include <algorithm>
#include <array>
#include <map>
#include <set>
#include <vector>

namespace eiem_cloth_graph {
using Face=std::array<int,3>;
using Edge=std::array<int,2>;
using Quad=std::array<int,4>;
struct Choice { Edge diagonal; std::array<int,2> faces; };
struct Rule { Quad vertices; std::vector<Choice> choices; };
struct OrderContract {
  std::vector<Face> source;
  std::vector<Edge> adjacent;
  std::vector<Rule> rules;
};
inline bool Valid(const OrderContract &c) {
  if(c.source.empty()||c.source.size()>512||c.adjacent.empty()||c.adjacent.size()>512||c.rules.size()>256)return false;
  auto unique=[](const auto &xs){return std::is_sorted(xs.begin(),xs.end())&&std::adjacent_find(xs.begin(),xs.end())==xs.end();};
  if(!unique(c.source)||!unique(c.adjacent))return false;
  for(auto f:c.source)if(f[0]<0||f[2]>=128||f[0]>=f[1]||f[1]>=f[2])return false;
  for(auto e:c.adjacent)if(e[0]<0||e[1]>=128||e[0]>=e[1])return false;
  std::set<Quad> quads;
  for(const auto &r:c.rules) {
    if(r.choices.size()<2||r.choices.size()>3||!unique(r.vertices)||!quads.insert(r.vertices).second)return false;
    std::set<Edge> diagonals;
    for(auto choice:r.choices) {
      const auto a=choice.faces[0],b=choice.faces[1];
      if(a<0||b<0||a==b||size_t(a)>=c.source.size()||size_t(b)>=c.source.size()||
          choice.diagonal[0]>=choice.diagonal[1]||!diagonals.insert(choice.diagonal).second)return false;
      std::set<int> vertices(c.source[a].begin(),c.source[a].end());vertices.insert(c.source[b].begin(),c.source[b].end());
      if(vertices.size()!=4||!std::equal(vertices.begin(),vertices.end(),r.vertices.begin()))return false;
      for(auto f:{c.source[a],c.source[b]})for(int n:choice.diagonal)if(std::find(f.begin(),f.end(),n)==f.end())return false;
    }
  }
  return true;
}
inline bool Match(const OrderContract &c,std::vector<Face> actual,std::vector<Edge> lines) {
  if(!Valid(c)||actual.empty()||actual.size()>256||lines.size()>128)return false;
  for(auto &f:actual)std::sort(f.begin(),f.end());std::sort(actual.begin(),actual.end());
  for(auto &e:lines)std::sort(e.begin(),e.end());std::sort(lines.begin(),lines.end());
  if(std::adjacent_find(actual.begin(),actual.end())!=actual.end()||std::adjacent_find(lines.begin(),lines.end())!=lines.end()||
      !std::includes(c.source.begin(),c.source.end(),actual.begin(),actual.end()))return false;
  std::vector<bool> present(c.source.size());
  for(size_t n=0;n<c.source.size();++n)present[n]=std::binary_search(actual.begin(),actual.end(),c.source[n]);
  std::map<Edge,int> indices;
  for(const auto &r:c.rules)for(auto choice:r.choices)indices.emplace(choice.diagonal,0);
  int next=0;for(auto &entry:indices)entry.second=next++;
  std::vector<std::set<int>> after(indices.size());std::vector<int> degree(indices.size());
  std::vector<std::vector<std::array<int,2>>> events(indices.size());
  for(size_t n=0;n<c.rules.size();++n) {
    const auto &r=c.rules[n];int first=-1;
    for(size_t k=0;k<r.choices.size();++k) {
      const auto &choice=r.choices[k];const int edge=indices.at(choice.diagonal);
      events[edge].push_back({int(n),int(k)});
      if(present[choice.faces[0]]||present[choice.faces[1]]){if(first>=0)return false;first=edge;}
    }
    if(first>=0)for(auto choice:r.choices) {
      const int edge=indices.at(choice.diagonal);
      if(edge!=first&&after[first].insert(edge).second)++degree[edge];
    }
  }
  for(bool reverse:{false,true}) {
    auto remaining=degree;std::set<int> ready;
    for(int n=0;n<int(remaining.size());++n)if(!remaining[n])ready.insert(n);
    std::vector<bool> used(c.rules.size()),removed(c.source.size());int visited=0;
    while(!ready.empty()) {
      const int edge=reverse?*ready.rbegin():*ready.begin();ready.erase(edge);++visited;
      for(auto event:events[edge]) {
        const auto &choice=c.rules[event[0]].choices[event[1]];
        if(used[event[0]])for(int f:choice.faces)removed[f]=true;else used[event[0]]=true;
      }
      for(int edgeAfter:after[edge])if(!--remaining[edgeAfter])ready.insert(edgeAfter);
    }
    if(visited!=int(indices.size()))return false;
    bool exact=true;for(size_t n=0;n<present.size();++n)if(present[n]==removed[n]){exact=false;break;}
    if(!exact)continue;
    std::set<Edge> covered;
    for(auto f:actual)for(int a=0;a<3;++a)for(int b=a+1;b<3;++b)covered.insert({f[a],f[b]});
    std::vector<Edge> expectedLines;for(auto e:c.adjacent)if(!covered.count(e))expectedLines.push_back(e);
    return lines==expectedLines;
  }
  return false;
}
}
