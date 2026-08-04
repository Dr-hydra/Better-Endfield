#ifndef DUMPER_H
#define DUMPER_H

#include "il2cpp_class.hxx"
#include "il2cpp_image.hxx"
#include <iosfwd>
#include <map>
#include <string>
#include <vector>

struct CachedClass {
    uint32_t typeToken;
    uint32_t flags;
    int32_t instanceSize;
    std::string name;
    std::string ns;
    std::string parentName;
    std::vector<std::string> interfaces;
    std::vector<std::string> attributes;
    bool isInterface;
    bool isStruct;
    std::vector<FieldData> fields;
    std::vector<MethodData> methods;
    std::vector<PropertyData> properties;
    std::vector<EventData> events;
};

class Dumper {

public:
    std::vector<Il2CppImage> images;

    Dumper( );

    std::map<std::string, std::vector<CachedClass>>
        CacheImage( const Il2CppImage & img ) const;

    void DumpAssemblyNormal(
        const std::string & asmName, std::size_t classCount,
        const std::map<std::string, std::vector<CachedClass>> & nsClasses );
    void DumpAssemblyAi(
        const std::string & asmName, std::size_t classCount,
        const std::map<std::string, std::vector<CachedClass>> & nsClasses );

    void DumpStringLiterals(
        const std::string & asmName,
        const std::map<std::string, std::vector<CachedClass>> & nsClasses,
        std::ofstream & out, std::size_t & counter );

    void DumpAllToFiles( );
};

#endif // DUMPER_H
