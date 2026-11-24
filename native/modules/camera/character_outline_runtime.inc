// Independent squad outlines. Reuse the native additive-material controller,
// with a private Material per slot and a private removal handle. Never change
// Renderer property blocks, character materials, or photo-mode VFX ownership.
namespace CharacterOutline {
struct Settings {
    bool enabled=false;
    int key=0xDC;
    CharacterOutlineStyle slots[4];
};
std::shared_ptr<const Settings> settings=std::make_shared<const Settings>();
std::atomic_bool stopping{false}, cleaned{true};
std::recursive_mutex gate;
thread_local bool pumping=false;
enum Id { Alive,Player,Count,Member,Loading,Helper,Load,Get,Dispose,Initialized,Initing,
    HashCtor,MaterialCtor,SetFloat,SetColor,Add,Remove,Destroy,Done,Last };
struct Method { BE_MethodDescriptorV1 spec; BE_ResolvedMethodV1 value{}; };
Method methods[]{
    {{"UnityEngine.CoreModule.dll","UnityEngine","Object","op_Implicit","UnityEngine.Object","System.Boolean",1}},
    {{"Gameplay.Beyond.dll","Beyond.Gameplay","GameInstance","get_player",nullptr,"Beyond.Gameplay.GamePlayer",0}},
    {{"Gameplay.Beyond.dll","Beyond.Gameplay.Core","SquadManager","get_slotCount",nullptr,"System.Int32",0}},
    {{"Gameplay.Beyond.dll","Beyond.Gameplay.Core","SquadManager","GetMemberBySlot","System.Int32","Beyond.Gameplay.Core.Entity",1}},
    {{"Gameplay.Beyond.dll","Beyond.Gameplay.Core","SquadManager","get_isSquadLoading",nullptr,"System.Boolean",0}},
    {{"Gameplay.Beyond.dll","Beyond.Gameplay.View","BaseModelComponent","get_entityRenderHelper",nullptr,"Beyond.Gameplay.View.EntityRenderHelper",0}},
    {{"Common.Beyond.dll","Beyond.Resource","I18NAssetLoader","Load","Beyond.Resource.StringPathHash|System.Type|Beyond.Resource.RootCategory","Beyond.Resource.FAssetProxyHandle",3}},
    {{"Common.Beyond.dll","Beyond.Resource","FAssetProxyHandle","Get",nullptr,"UnityEngine.Object",0}},
    {{"Common.Beyond.dll","Beyond.Resource","FAssetProxyHandle","Dispose",nullptr,"System.Void",0}},
    {{"Common.Beyond.dll","Beyond.Resource","ResourceManager","IsInitialized",nullptr,"System.Boolean",0}},
    {{"Common.Beyond.dll","Beyond.Resource","ResourceManager","IsAsyncIniting",nullptr,"System.Boolean",0}},
    {{"Common.Beyond.dll","Beyond.Resource","StringPathHash",".ctor","System.String","System.Void",1}},
    {{"UnityEngine.CoreModule.dll","UnityEngine","Material",".ctor","UnityEngine.Material","System.Void",1}},
    {{"UnityEngine.CoreModule.dll","UnityEngine","Material","SetFloat","System.String|System.Single","System.Void",2}},
    {{"UnityEngine.CoreModule.dll","UnityEngine","Material","SetColor","System.String|UnityEngine.Color","System.Void",2}},
    {{"Rendering.Beyond.dll","Beyond.Rendering","EntityRenderHelperMaterialController","AddMaterialToAllRenderer","UnityEngine.Material|Beyond.Rendering.EntityVFXRendererMask|Beyond.Rendering.AddMaterialConfig","System.Int32",3}},
    {{"Rendering.Beyond.dll","Beyond.Rendering","EntityRenderHelperMaterialController","RemoveMaterialFromAllRenderer","System.Int32","System.Void",1}},
    {{"UnityEngine.CoreModule.dll","UnityEngine","Object","Destroy","UnityEngine.Object","System.Void",1}},
    {{"Common.Beyond.dll","Beyond.Resource","FAssetProxyHandle","get_isDone",nullptr,"System.Boolean",0}},
};
void* Call(Id id,void* self=nullptr,void** args=nullptr,bool* ok=nullptr) {
    if(ok)*ok=false;
    if(!g_host||!g_host->runtime_invoke||!methods[id].value.method_info)return nullptr;
    void* exception=nullptr;
    void* result=g_host->runtime_invoke(g_host->context,methods[id].value.method_info,self,args,&exception);
    if(ok)*ok=exception==nullptr;
    return exception?nullptr:result;
}
template<class T> bool Value(Id id,void* self,T& out,void** args=nullptr) {
    void* boxed=Call(id,self,args);
    void* value=boxed?g_host->object_unbox(g_host->context,boxed):nullptr;
    if(!value)return false;
    std::memcpy(&out,value,sizeof(T));return true;
}
bool Live(void* obj) { bool live=false;void* args[]{obj};return obj&&Value(Alive,nullptr,live,args)&&live; }
struct Slot {
    void* controller=nullptr;
    void* helper=nullptr;
    void* material=nullptr;
    uint32_t controller_pin=0,helper_pin=0,material_pin=0;
    int handle=0;
    bool attached=false;
    CharacterOutlineStyle style;
};
struct Runtime {
    std::atomic<DWORD> thread{0};
    bool resolved=false,attempted=false,visible=true,was_enabled=false,was_down=false;
    uint64_t next_scan=0,next_load=0;
    BE_ResolvedClassV1 hash_class{},material_class{},add_config_class{},effect_class{};
    BE_ResolvedFieldV1 squad_field{},controller_field{},effect_data_field{},effect_material_field{};
    void* asset_handle=nullptr;
    uint32_t asset_pin=0;
    Slot slots[4];
    void* Field(const BE_ResolvedFieldV1& field,void* instance) {
        return instance&&field.field_info?g_host->field_get_value_object(g_host->context,field.field_info,instance):nullptr;
    }
    bool Resolve() {
        if(attempted)return resolved;
        attempted=true;
        if(!g_host||!g_host->resolve_method||!g_host->resolve_class||!g_host->resolve_field||
            !g_host->field_get_value_object||!g_host->object_new||!g_host->object_unbox||
            !g_host->gchandle_new||!g_host->gchandle_free||!g_host->string_new)return false;
        resolved=true;
        for(auto& method:methods)if(g_host->resolve_method(g_host->context,&method.spec,&method.value)!=BE_Result_Ok) {
            resolved=false;Log(std::string("Character outline contract missing: ")+method.spec.class_name+"."+method.spec.method_name);
        }
        auto cls=[&](const char* assembly,const char* ns,const char* name,BE_ResolvedClassV1& value) {
            resolved=(g_host->resolve_class(g_host->context,assembly,ns,name,&value)==BE_Result_Ok&&value.class_info)&&resolved;
        };
        cls("Common.Beyond.dll","Beyond.Resource","StringPathHash",hash_class);
        cls("UnityEngine.CoreModule.dll","UnityEngine","Material",material_class);
        cls("Rendering.Beyond.dll","Beyond.Rendering","AddMaterialConfig",add_config_class);
        cls("Rendering.Beyond.dll","Beyond.Rendering","EntityVFXAddictiveMaterialAsset",effect_class);
        const BE_FieldDescriptorV1 effect_data{"Rendering.Beyond.dll","Beyond.Rendering","EntityVFXAddictiveMaterialAsset","data","Beyond.Rendering.EntityVFXAddictiveMaterialData"};
        const BE_FieldDescriptorV1 effect_material{"Rendering.Beyond.dll","Beyond.Rendering","EntityVFXAddictiveMaterialData","material","UnityEngine.Material"};
        resolved=(g_host->resolve_field(g_host->context,&effect_data,&effect_data_field)==BE_Result_Ok&&effect_data_field.field_info)&&resolved;
        resolved=(g_host->resolve_field(g_host->context,&effect_material,&effect_material_field)==BE_Result_Ok&&effect_material_field.field_info)&&resolved;
        const BE_FieldDescriptorV1 squad{"Gameplay.Beyond.dll","Beyond.Gameplay","GamePlayer","squadManager","Beyond.Gameplay.Core.SquadManager"};
        const BE_FieldDescriptorV1 controller{"Gameplay.Beyond.dll","Beyond.Gameplay.View","EntityRenderHelper","m_rendererAndMaterialController","Beyond.Rendering.EntityRenderHelperMaterialController"};
        resolved=(g_host->resolve_field(g_host->context,&squad,&squad_field)==BE_Result_Ok)&&resolved;
        resolved=(g_host->resolve_field(g_host->context,&controller,&controller_field)==BE_Result_Ok)&&resolved;
        resolved=resolved&&effect_class.type_object&&squad_field.field_info&&controller_field.field_info;
        if(!resolved)Log("Character outline unavailable: native additive-material contracts did not resolve.");
        else Log("Character outline: native additive-material contracts ready.");
        return resolved;
    }
    void DestroyMaterial(Slot& slot) {
        if(Live(slot.material)){void* args[]{slot.material};Call(Destroy,nullptr,args);}
        if(slot.material_pin)g_host->gchandle_free(g_host->context,slot.material_pin);
        slot.material=nullptr;slot.material_pin=0;
    }
    bool Detach(Slot& slot) {
        // A failed removal retains ownership and the material for a later retry.
        if(slot.attached) {
            if(!Live(slot.helper))slot.attached=false;
            else {bool ok=false;void* args[]{&slot.handle};Call(Remove,slot.controller,args,&ok);if(!ok)return false;slot.attached=false;}
        }
        if(slot.controller_pin)g_host->gchandle_free(g_host->context,slot.controller_pin);
        if(slot.helper_pin)g_host->gchandle_free(g_host->context,slot.helper_pin);
        slot.controller=nullptr;slot.controller_pin=0;slot.helper=nullptr;slot.helper_pin=0;
        DestroyMaterial(slot);return true;
    }
    bool Clear() {
        bool ok=true;
        for(auto& slot:slots)ok=Detach(slot)&&ok;
        if(!ok)return false;
        if(asset_handle) {
            bool disposed=false;Call(Dispose,g_host->object_unbox(g_host->context,asset_handle),nullptr,&disposed);
            if(!disposed)return false;
        }
        if(asset_pin)g_host->gchandle_free(g_host->context,asset_pin);
        asset_handle=nullptr;asset_pin=0;return true;
    }
    void* LoadedMaterial() {
        void* effect=Call(Get,g_host->object_unbox(g_host->context,asset_handle));
        if(!Live(effect))return nullptr;
        return Field(effect_material_field,Field(effect_data_field,effect));
    }
    void* Source(uint64_t now) {
        if(asset_handle) {
            void* source=LoadedMaterial();
            if(Live(source))return source;
            bool done=false;
            if(Value(Done,g_host->object_unbox(g_host->context,asset_handle),done)&&!done)return nullptr;
            if(!Clear())return nullptr;
        }
        if(now<next_load)return nullptr;
        next_load=now+2000;
        bool initialized=false,initing=true;
        if(!Value(Initialized,nullptr,initialized)||!initialized||!Value(Initing,nullptr,initing)||initing)return nullptr;
        void* hash=g_host->object_new(g_host->context,hash_class.class_info);
        void* path=g_host->string_new(g_host->context,"assets/beyond/dynamicassets/gameplay/effects/vfx/p_fxgp_common_char_outline_01_nofade_asset.asset");
        if(!hash||!path)return nullptr;
        void* hash_data=g_host->object_unbox(g_host->context,hash);
        bool ok=false;void* ctor[]{path};Call(HashCtor,hash_data,ctor,&ok);if(!ok)return nullptr;
        // The material is a dependency, absent from the runtime loadable-asset manifest.
        // Load its public effect asset and follow data.material instead.
        uint8_t category=0;void* args[]{hash_data,effect_class.type_object,&category};
        asset_handle=Call(Load,nullptr,args);
        if(!asset_handle)return nullptr;
        asset_pin=g_host->gchandle_new(g_host->context,asset_handle,1);
        if(!asset_pin){Call(Dispose,g_host->object_unbox(g_host->context,asset_handle));asset_handle=nullptr;return nullptr;}
        void* source=LoadedMaterial();
        if(!Live(source)){
            bool done=false;
            if(Value(Done,g_host->object_unbox(g_host->context,asset_handle),done)&&!done)return nullptr;
            Clear();Log("Character outline: outline effect or its referenced material was not loaded; retrying.");return nullptr;
        }
        Log("Character outline: loaded material through native outline effect asset.");
        return source;
    }
    bool Appearance(void* material,const CharacterOutlineStyle& style) {
        auto scalar=[&](const char* property,float value) {
            void* name=g_host->string_new(g_host->context,property);if(!name)return false;
            bool ok=false;void* args[]{name,&value};Call(SetFloat,material,args,&ok);return ok;
        };
        const auto rgb=std::strtoul(style.color.c_str(),nullptr,16);
        // Keep the stock tint alpha: opacity is its separate shader multiplier.
        Color color{float((rgb>>16)&255)/255,float((rgb>>8)&255)/255,float(rgb&255)/255,0.5019608f};
        void* name=g_host->string_new(g_host->context,"_TintColor");bool ok=false;
        if(!name)return false;
        void* args[]{name,&color};Call(SetColor,material,args,&ok);
        return ok&&scalar("_OutlineWidth",style.width)&&scalar("_TintColorIntensity",style.intensity)&&scalar("_TintColorAlpha",style.opacity);
    }
    void UpdateSlot(int index,void* helper,void* controller,void* source,const CharacterOutlineStyle& style) {
        auto& slot=slots[index];
        // The native controller may generate renderer-specific material copies.
        // Reattach on appearance changes so those copies receive the new style.
        if((slot.controller!=controller||slot.helper!=helper||!(slot.style==style))&&!Detach(slot))return;
        if(!controller)return;
        if(slot.attached)return;
        if(!source)return;
        slot.material=g_host->object_new(g_host->context,material_class.class_info);
        if(!slot.material)return;
        slot.material_pin=g_host->gchandle_new(g_host->context,slot.material,1);
        if(!slot.material_pin){slot.material=nullptr;return;}
        bool ok=false;void* ctor[]{source};Call(MaterialCtor,slot.material,ctor,&ok);
        if(!ok||!Appearance(slot.material,style)){DestroyMaterial(slot);return;}
        void* config=g_host->object_new(g_host->context,add_config_class.class_info);
        if(!config){DestroyMaterial(slot);return;}
        slot.controller_pin=g_host->gchandle_new(g_host->context,controller,1);
        if(!slot.controller_pin){DestroyMaterial(slot);return;}
        slot.controller=controller;slot.helper=helper;
        slot.helper_pin=g_host->gchandle_new(g_host->context,helper,1);
        if(!slot.helper_pin){Detach(slot);return;}
        int mask=-67108865; // EntityVFXRendererMask.AllWithoutAlphaQuad, as in stock asset.
        void* args[]{slot.material,&mask,g_host->object_unbox(g_host->context,config)};
        if(!Value(Add,controller,slot.handle,args)||slot.handle<0){Detach(slot);Log("Character outline: failed to attach slot "+std::to_string(index+1));return;}
        slot.attached=true;slot.style=style;
        Log("Character outline: attached private material to squad slot "+std::to_string(index+1));
    }
    void Tick(const Settings& config) {
        const bool focused=GameWindowHasFocus();
        const bool down=KeyDown(config.key);
        if(config.enabled&&!was_enabled){visible=true;next_scan=0;}
        if(config.enabled&&focused&&down&&!was_down){visible=!visible;next_scan=0;Log(visible?"Character outline: visible.":"Character outline: hidden.");}
        was_down=down;was_enabled=config.enabled;
        if(stopping.load()||!config.enabled||!visible) {if(Clear())cleaned=true;return;}
        cleaned=false;
        if(!Resolve())return;
        const auto now=GetTickCount64();if(now<next_scan)return;next_scan=now+250;
        void* player=Call(Player);
        void* squad=Field(squad_field,player);
        bool loading=true;int count=0;
        if(!squad||!Value(Loading,squad,loading)||loading||!Value(Count,squad,count)){Clear();return;}
        void* source=Source(now);
        for(int i=0;i<4;++i) {
            void* controller=nullptr;void* helper=nullptr;
            if(config.slots[i].enabled&&i<count) {
                void* args[]{&i};void* entity=Call(Member,squad,args);
                void* model=entity?Invoke(Contract("entity.get_model_com"),entity,nullptr):nullptr;
                helper=model?Call(Helper,model):nullptr;
                controller=Field(controller_field,helper);
            }
            UpdateSlot(i,helper,controller,source,config.slots[i]);
        }
    }
};
Runtime runtime;
void Publish(const CameraConfiguration& value) {
    auto next=std::make_shared<Settings>();next->enabled=value.enabled&&value.character_outline_enabled;next->key=value.character_outline_key;
    std::copy(std::begin(value.character_outline_slots),std::end(value.character_outline_slots),std::begin(next->slots));
    std::atomic_store(&settings,std::shared_ptr<const Settings>(next));
}
void Pump(bool capture=true) {
    if(pumping)return;
    std::lock_guard lock(gate);
    const auto thread=GetCurrentThreadId();
    if(!runtime.thread){if(!capture)return;runtime.thread=thread;}
    if(runtime.thread!=thread)return;
    struct Guard{Guard(){pumping=true;}~Guard(){pumping=false;}} guard;
    try{runtime.Tick(*std::atomic_load(&settings));}
    catch(...){Log("Character outline: runtime error; cleaning owned materials.");runtime.Clear();}
}
void Shutdown() {
    stopping=true;
    if(runtime.thread==GetCurrentThreadId())Pump(false);
    else for(int i=0;i<50&&!cleaned.load();++i)std::this_thread::sleep_for(std::chrono::milliseconds(10));
    if(!cleaned.load())Log("Character outline: Unity cleanup unavailable during shutdown; remaining materials live until process exit.");
}
} // namespace CharacterOutline
