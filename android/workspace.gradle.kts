import java.io.File

@Suppress("UNCHECKED_CAST")
val beWorkspace = gradle.extra["beWorkspace"] as Map<String, Any?>
@Suppress("UNCHECKED_CAST")
val workspacePaths = beWorkspace["paths"] as Map<String, Any?>
@Suppress("UNCHECKED_CAST")
val workspaceTools = beWorkspace["tools"] as Map<String, Any?>
val workspaceBuild = File(workspacePaths["build"] as String)
val workspaceTemp = workspacePaths["temp"] as String
rootProject.extra["beWorkspace"] = beWorkspace

allprojects {
    val projectSegment = if (path == ":") "root" else path.removePrefix(":").replace(':', '/')
    layout.buildDirectory.set(File(workspaceBuild, "android/gradle/$projectSegment"))
    tasks.withType<Exec>().configureEach {
        environment("TEMP", workspaceTemp)
        environment("TMP", workspaceTemp)
        environment("TMPDIR", workspaceTemp)
        environment("ANDROID_HOME", workspaceTools["android_sdk"] as String)
        environment("ANDROID_SDK_ROOT", workspaceTools["android_sdk"] as String)
        environment("BE_WORKSPACE_DOBBY_ROOT", workspaceTools["android_dobby"] as String)
    }
}
