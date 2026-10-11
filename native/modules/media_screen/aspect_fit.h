#pragma once
#include <algorithm>
#include <cmath>
#include <stdexcept>
#include <string_view>

namespace BetterEndfieldNext::MediaScreen {
struct FitVector2 { float x=1, y=1; };
struct DisplayContent { FitVector2 scale{}, offset{0,0}; };
struct DisplayLayout {
    int width, height, image_width, image_height, x=0, y=0;
    FitVector2 source_scale{}, source_offset{0,0};
    FitVector2 canvas_scale{}, canvas_offset{0,0};
};
// Fit one captured/decoded canvas to each physical monitor. The output stays
// within the user's resolution budget; no second capture or decode is needed.
inline DisplayLayout LayoutDisplay(int width, int height, double aspect, std::string_view mode, DisplayContent content={}) {
    if (width<=0 || height<=0 || !std::isfinite(aspect) || aspect<.25 || aspect>8)
        throw std::runtime_error("Invalid monitor aspect ratio");
    DisplayLayout result{width,height,width,height};
    if (!std::isfinite(content.scale.x) || !std::isfinite(content.scale.y) || !std::isfinite(content.offset.x) || !std::isfinite(content.offset.y) ||
        content.scale.x<=0 || content.scale.y<=0 || content.offset.x<0 || content.offset.y<0 ||
        content.scale.x+content.offset.x>1.00001f || content.scale.y+content.offset.y>1.00001f)
        throw std::runtime_error("Invalid source content rectangle");
    const double source=double(width)*content.scale.x/(height*content.scale.y);
    if (aspect>double(width)/height) result.height=std::max(1,int(std::round(width/aspect)));
    else result.width=std::max(1,int(std::round(height*aspect)));
    result.image_width=result.width; result.image_height=result.height;
    const double destination=double(result.width)/result.height;
    if (mode=="fit") {
        if (source>destination) result.image_height=std::clamp(int(std::round(result.width/source)),1,result.height);
        else result.image_width=std::clamp(int(std::round(result.height*source)),1,result.width);
        result.x=(result.width-result.image_width)/2; result.y=(result.height-result.image_height)/2;
        result.canvas_scale={float(result.width)/result.image_width,float(result.height)/result.image_height};
        result.canvas_offset={-float(result.x)/result.image_width,-float(result.y)/result.image_height};
    } else if (mode=="crop") {
        if (source>destination) result.source_scale.x=float(destination/source);
        else result.source_scale.y=float(source/destination);
        result.source_offset={(1-result.source_scale.x)/2,(1-result.source_scale.y)/2};
        result.canvas_scale=result.source_scale; result.canvas_offset=result.source_offset;
    } else if (mode!="stretch") throw std::runtime_error("Invalid monitor fit mode");
    result.source_offset={content.offset.x+result.source_offset.x*content.scale.x,content.offset.y+result.source_offset.y*content.scale.y};
    result.source_scale={result.source_scale.x*content.scale.x,result.source_scale.y*content.scale.y};
    result.canvas_offset={content.offset.x+result.canvas_offset.x*content.scale.x,content.offset.y+result.canvas_offset.y*content.scale.y};
    result.canvas_scale={result.canvas_scale.x*content.scale.x,result.canvas_scale.y*content.scale.y};
    return result;
}
}
