#ifndef IL2CPP_IMAGE_H
#define IL2CPP_IMAGE_H

#include <cstddef>
#include "il2cpp_class.hxx"

class Il2CppImage {

public:
    void * image = nullptr;

    explicit Il2CppImage( void * img = nullptr ) : image( img ) { }

    const char * GetName( ) const;
    size_t GetClassCount( ) const;
    Il2CppClass GetClassByIndex( size_t index ) const;

};

#endif // IL2CPP_IMAGE_H