#include "../../modules/camera/module.cpp"
#include "eiem_body_fake.h"
#include "test_support.h"
#include <map>
using namespace BetterEndfieldNext::CameraModule;
namespace CO=BetterEndfieldNext::CameraModule::CharacterOutline;
struct Object {
    bool alive=true,unity=true;
    std::map<std::string,float> scalars;
    Color color{};
    std::map<int,Object*> additions;
};
std::vector<std::unique_ptr<Object>> objects;
std::vector<std::unique_ptr<std::string>> strings;
std::map<uint32_t,void*> pins;
uint32_t next_pin=1;
int next_handle=10,removals=0,destroys=0;
bool fail_remove=false;
Object effect_asset,effect_data,source_material,asset_handle;
bool asset_done=true;int asset_loads=0,asset_disposals=0;
void* effect_type=reinterpret_cast<void*>(900);
void* BE_CALL FieldFake(void*,const void* field,void* instance) {
    if(field==reinterpret_cast<void*>(1)&&instance==&effect_asset)return &effect_data;
    if(field==reinterpret_cast<void*>(2)&&instance==&effect_data)return &source_material;
    return nullptr;
}
template<class T> void* Box(T value){static T boxed;boxed=value;return &boxed;}
void* BE_CALL InvokeFake(void*,const void* method,void* instance,void** args,void** exception) {
    *exception=nullptr;
    auto object=static_cast<Object*>(instance);
    switch(CO::Id(reinterpret_cast<uintptr_t>(method)-1)) {
    case CO::Alive: {
        auto target=static_cast<Object*>(args[0]);CHECK(target->unity);return Box(target->alive);
    }
    case CO::Initialized:return Box(true);
    case CO::Initing:return Box(false);
    case CO::HashCtor:
        CHECK(*static_cast<std::string*>(args[0])=="assets/beyond/dynamicassets/gameplay/effects/vfx/p_fxgp_common_char_outline_01_nofade_asset.asset");return nullptr;
    case CO::Load:
        CHECK(args[1]==effect_type);CHECK(*static_cast<uint8_t*>(args[2])==0);++asset_loads;return &asset_handle;
    case CO::Get:return asset_done?&effect_asset:nullptr;
    case CO::Done:return Box(asset_done);
    case CO::Dispose:++asset_disposals;return nullptr;
    case CO::MaterialCtor: {
        auto source=static_cast<Object*>(args[0]);object->scalars=source->scalars;object->color=source->color;return nullptr;
    }
    case CO::SetFloat:object->scalars[*static_cast<std::string*>(args[0])]=*static_cast<float*>(args[1]);return nullptr;
    case CO::SetColor:object->color=*static_cast<Color*>(args[1]);return nullptr;
    case CO::Add: {
        CHECK(*static_cast<int*>(args[1])==-67108865);
        int handle=next_handle++;object->additions[handle]=static_cast<Object*>(args[0]);return Box(handle);
    }
    case CO::Remove:
        if(fail_remove){*exception=reinterpret_cast<void*>(1);return nullptr;}
        object->additions.erase(*static_cast<int*>(args[0]));++removals;return nullptr;
    case CO::Destroy:static_cast<Object*>(args[0])->alive=false;++destroys;return nullptr;
    default:*exception=reinterpret_cast<void*>(1);return nullptr;
    }
}
void* BE_CALL NewFake(void*,const void*){objects.push_back(std::make_unique<Object>());return objects.back().get();}
void* BE_CALL StringFake(void*,const char* text){strings.push_back(std::make_unique<std::string>(text));return strings.back().get();}
void* BE_CALL UnboxFake(void*,void* box){return box;}
uint32_t BE_CALL PinFake(void*,void* object,int){const auto pin=next_pin++;pins[pin]=object;return pin;}
void BE_CALL UnpinFake(void*,uint32_t pin){pins.erase(pin);}
void BE_CALL LogFake(void*,const char*,const char*){}
int main() {
    auto defaults=ParseConfiguration("");
    CHECK(!defaults.character_outline_enabled);CHECK(defaults.character_outline_key==0xDC);
    CHECK(ParseVirtualKey("、",0)==0xDC);CHECK(ParseVirtualKey("BACKSLASH",0)==0xDC);
    CHECK(ParseVirtualKey("CTRL+\\",0)==(BetterEndfieldNext::Input::kCtrl|0xDC));
    auto config=ParseConfiguration("character_outline_enabled=true\ncharacter_outline_slot2_enabled=false\ncharacter_outline_slot3_color=#12A0FF\ncharacter_outline_slot3_width=0.12\ncharacter_outline_slot4_width=nan\ncharacter_outline_slot4_opacity=99\ncharacter_outline_slot4_intensity=-5\n");
    CHECK(config.character_outline_enabled&&!config.character_outline_slots[1].enabled);
    CHECK(config.character_outline_slots[2].color=="12A0FF");CHECK(nearly(config.character_outline_slots[2].width,0.12));
    CHECK(nearly(config.character_outline_slots[3].width,0.06));CHECK(nearly(config.character_outline_slots[3].opacity,1));CHECK(nearly(config.character_outline_slots[3].intensity,1));
    CHECK(config.character_outline_slots[0].color=="FFFFFF");
    config.enabled=false;CO::Publish(config);CHECK(!std::atomic_load(&CO::settings)->enabled);
    config.enabled=true;CO::Publish(config);CHECK(std::atomic_load(&CO::settings)->enabled);

    BE_HostApiV1 host{};host.runtime_invoke=InvokeFake;host.object_new=NewFake;host.string_new=StringFake;
    host.field_get_value_object=FieldFake;host.object_unbox=UnboxFake;host.gchandle_new=PinFake;host.gchandle_free=UnpinFake;host.log=LogFake;g_host=&host;
    for(size_t i=0;i<std::size(CO::methods);++i)CO::methods[i].value.method_info=reinterpret_cast<void*>(i+1);
    CO::Runtime runtime;
    runtime.effect_class.type_object=effect_type;
    runtime.effect_data_field.field_info=reinterpret_cast<void*>(1);
    runtime.effect_material_field.field_info=reinterpret_cast<void*>(2);
    // A loadable effect owns the dependency material; direct material loads fail in-game.
    // Pending loads must survive ticks instead of being disposed and restarted.
    asset_done=false;CHECK(runtime.Source(2000)==nullptr);CHECK(asset_loads==1&&asset_disposals==0);
    CHECK(runtime.Source(2250)==nullptr);CHECK(asset_loads==1&&asset_disposals==0);
    asset_done=true;CHECK(runtime.Source(2500)==&source_material);CHECK(asset_loads==1);
    CHECK(runtime.Source(2750)==&source_material);CHECK(runtime.Clear());
    CHECK(asset_disposals==1&&pins.empty());
    Object source,helper1,helper2,controller1,controller2,photo_material,body_material;
    controller1.unity=false;controller2.unity=false;
    source.scalars["_OutlineWidth"]=0.06f;body_material.scalars["_OutlineWidth"]=0.6f;
    // Existing photo outline belongs to the game and must survive our handles.
    controller1.additions[1]=&photo_material;
    CharacterOutlineStyle red,blue;red.color="FF0000";blue.color="0000FF";blue.width=0.12f;
    runtime.UpdateSlot(0,&helper1,&controller1,&source,red);
    runtime.UpdateSlot(1,&helper2,&controller2,&source,blue);
    CHECK(runtime.slots[0].attached&&runtime.slots[1].attached);
    auto first=static_cast<Object*>(runtime.slots[0].material);auto second=static_cast<Object*>(runtime.slots[1].material);
    CHECK(first!=second&&first!=&source);CHECK(nearly(first->color.r,1)&&nearly(second->color.b,1));
    CHECK(nearly(first->scalars["_OutlineWidth"],0.06)&&nearly(second->scalars["_OutlineWidth"],0.12));
    CHECK(nearly(body_material.scalars["_OutlineWidth"],0.6)&&nearly(source.scalars["_OutlineWidth"],0.06));
    const int first_handle=runtime.slots[0].handle;
    red.opacity=0.3f;runtime.UpdateSlot(0,&helper1,&controller1,&source,red);
    CHECK(runtime.slots[0].handle!=first_handle);CHECK(!first->alive);
    first=static_cast<Object*>(runtime.slots[0].material);
    CHECK(nearly(first->scalars["_TintColorAlpha"],0.3));
    CHECK(nearly(second->scalars["_TintColorAlpha"],1));
    // An unsuccessful removal must retain the pins and live material for retry.
    fail_remove=true;CHECK(!runtime.Detach(runtime.slots[0]));CHECK(first->alive&&runtime.slots[0].attached);
    fail_remove=false;CHECK(runtime.Detach(runtime.slots[0]));CHECK(!first->alive);
    CHECK(controller1.additions.size()==1&&controller1.additions[1]==&photo_material);
    // Changing the model moves the owned draw and releases the old material.
    runtime.UpdateSlot(1,&helper1,&controller1,&source,blue);
    CHECK(controller2.additions.empty()&&!second->alive);CHECK(runtime.slots[1].controller==&controller1);
    CHECK(runtime.Clear());CHECK(pins.empty());CHECK(controller1.additions.size()==1);
    CHECK(photo_material.alive&&body_material.alive);CHECK(removals==4&&destroys==4);
    g_host=nullptr;
    std::cout<<"PASS independent outline configuration/ownership: "<<checks<<" checks\n";
}
