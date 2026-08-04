#ifndef MODEL_REPLACER_H
#define MODEL_REPLACER_H

#include <windows.h>
#include <string>

namespace ModelReplacer {

    bool LoadConfiguration( const std::string & configPath );
    bool Initialize( HMODULE module );
    void QueueReplace( );
    void QueueRestore( );
    void Shutdown( );

} // namespace ModelReplacer

#endif
