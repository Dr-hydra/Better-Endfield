import groovy.json.JsonSlurper
import java.io.File
import java.util.Properties

pluginManagement {
    repositories {
        google()
        mavenCentral()
        gradlePluginPortal()
    }
}

// -PworkspaceConfig=... (or BE_WORKSPACE_CONFIG) uses the common resolver.
// workspacePython is only the bootstrap interpreter, like Workspace.ps1's python.
val workspaceRepo = settingsDir.parentFile.canonicalFile
val workspaceConfig = providers.gradleProperty("workspaceConfig").orNull
    ?: providers.gradleProperty("workspace-config").orNull
    ?: providers.environmentVariable("BE_WORKSPACE_CONFIG").orNull
val bootstrapPython = providers.gradleProperty("workspacePython").orNull
    ?: providers.environmentVariable("BE_WORKSPACE_PYTHON").orNull
    ?: "python"
fun resolveWorkspace(python: String): Map<String, Any?> {
    val command = mutableListOf(python,
        File(workspaceRepo, "scripts/workspace_config.py").path, "resolve")
    workspaceConfig?.takeIf { it.isNotBlank() }?.let { command += listOf("--config", it) }
    val builder = ProcessBuilder(command).directory(workspaceRepo).redirectErrorStream(true)
    builder.environment()["PYTHONDONTWRITEBYTECODE"] = "1"
    val process = builder.start()
    val output = process.inputStream.bufferedReader(Charsets.UTF_8).use { it.readText() }
    check(process.waitFor() == 0) { "Workspace configuration failed:\n$output" }
    @Suppress("UNCHECKED_CAST")
    return JsonSlurper().parseText(output) as Map<String, Any?>
}
val bootstrapWorkspace = resolveWorkspace(bootstrapPython)
@Suppress("UNCHECKED_CAST")
val bootstrapTools = bootstrapWorkspace["tools"] as Map<String, Any?>
val configuredPython = bootstrapTools["python"] as String
val workspace = if (configuredPython == bootstrapPython) bootstrapWorkspace
    else resolveWorkspace(configuredPython)
@Suppress("UNCHECKED_CAST")
val workspacePaths = workspace["paths"] as Map<String, Any?>
@Suppress("UNCHECKED_CAST")
val workspaceTools = workspace["tools"] as Map<String, Any?>
val workspaceSdk = File(workspaceTools["android_sdk"] as String).canonicalFile
val workspaceTemp = File(workspacePaths["temp"] as String)
check(workspaceTemp.isDirectory || workspaceTemp.mkdirs()) {
    "Cannot create workspace temp directory: $workspaceTemp"
}
System.setProperty("java.io.tmpdir", workspaceTemp.path)
// AGP reads android.home. Reject competing SDK locations rather than silently
// selecting an old SDK through local.properties or a daemon environment.
for (name in listOf("ANDROID_HOME", "ANDROID_SDK_ROOT")) {
    val value = System.getenv(name)
    check(value.isNullOrBlank() || File(value).canonicalFile == workspaceSdk) {
        "$name differs from tools.android_sdk; set it to $workspaceSdk or unset it."
    }
}
val localProperties = Properties()
File(settingsDir, "local.properties").takeIf { it.isFile }?.inputStream()?.use {
    localProperties.load(it)
}
localProperties.getProperty("sdk.dir")?.let {
    val localSdk = File(it).let { path -> if (path.isAbsolute) path else File(settingsDir, it) }
    check(localSdk.canonicalFile == workspaceSdk) {
        "android/local.properties sdk.dir differs from tools.android_sdk ($workspaceSdk)."
    }
}
System.setProperty("android.home", workspaceSdk.path)
gradle.startParameter.projectCacheDir = File(workspacePaths["build"] as String, "next/android/gradle-cache")
gradle.extra["beWorkspace"] = workspace

dependencyResolutionManagement {
    repositoriesMode.set(RepositoriesMode.FAIL_ON_PROJECT_REPOS)
    repositories {
        google()
        mavenCentral()
        maven("https://api.xposed.info/")
    }
}

rootProject.name = "BetterEndfieldNext.Android"
include(":app")
