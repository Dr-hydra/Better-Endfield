#ifndef SCENE_DUMPER_H
#define SCENE_DUMPER_H

namespace SceneDumper {

	bool Initialize( );
	bool Ready( );

	void Dump( );

	void SetDeepFieldDump( bool on );

	void DumpCamera( );
	void DumpMeshColliders( );
	void DumpClassesByList( );
	void DumpAll( );

} // namespace SceneDumper

#endif
