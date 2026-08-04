#ifndef IL2CPP_CLASS_H
#define IL2CPP_CLASS_H

#include "il2cpp_api.hxx"
#include <string>
#include <vector>

struct FieldData {
	uint32_t flags;
	std::string type;
	std::string name;
	int32_t offset;
	uint32_t token; // 0 if il2cpp_field_get_token unavailable
	void * raw;      // FieldInfo*
	std::vector<std::string> attributes;
};

struct PropertyData {
	std::string name;
	uint32_t flags;
	std::string getterName; // empty if no getter
	std::string setterName;
};

struct EventData {
	std::string name;
	std::string addName;
	std::string removeName;
	std::string raiseName;
};

struct ParamData {
	std::string type;
	std::string name;
};

struct MethodData {
	uint32_t flags;
	uint32_t iflags; // method-impl flags (icall/native/runtime/)
	uint32_t token;  // 0 if il2cpp_method_get_token unavailable
	bool isGeneric;  // generic method definition (no native code)
	std::string returnType;
	std::string name;
	std::vector<ParamData> params;
	uintptr_t address;
	uint64_t rva;
	std::vector<std::string> attributes;
};

class Il2CppClass {

public:
	void * klass = nullptr;

	explicit Il2CppClass( void * k = nullptr ) : klass( k ) { }

	const char * GetName( ) const;
	const char * GetNamespace( ) const;

	bool IsValueType( ) const;
	bool IsInterface( ) const;

	uint32_t GetFlags( ) const;
	uint32_t GetTypeToken( ) const;

	Il2CppClass GetParent( ) const;
	std::vector<Il2CppClass> GetInterfaces( ) const;

	std::vector<FieldData> GetFields( ) const;
	std::vector<MethodData> GetMethods( ) const;
	std::vector<PropertyData> GetProperties( ) const;
	std::vector<EventData> GetEvents( ) const;

	int32_t InstanceSize( ) const; // 0 if unknown

	std::vector<std::string> GetAttributes( ) const;
};

#endif // IL2CPP_CLASS_H
