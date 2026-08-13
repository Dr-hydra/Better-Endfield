#ifndef VOICE_LANGUAGE_ROUTER_H
#define VOICE_LANGUAGE_ROUTER_H

#include <string>

namespace VoiceLanguageRouter {

    bool LoadConfiguration( const std::string & configPath );
    bool IsEnabled( );
    bool Initialize( );
    void PollHealth( );
    bool Shutdown( );

} // namespace VoiceLanguageRouter

#endif
