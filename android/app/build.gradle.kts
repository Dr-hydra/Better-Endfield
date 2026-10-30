import groovy.json.JsonSlurper
import java.io.File
import java.security.KeyStore
import java.security.MessageDigest
import java.util.Properties

plugins {
    id("com.android.application")
}

@Suppress("UNCHECKED_CAST")
val beWorkspace = rootProject.extra["beWorkspace"] as Map<String, Any?>
@Suppress("UNCHECKED_CAST")
val workspacePaths = beWorkspace["paths"] as Map<String, Any?>
@Suppress("UNCHECKED_CAST")
val workspaceTools = beWorkspace["tools"] as Map<String, Any?>
@Suppress("UNCHECKED_CAST")
val resourceUpdate = beWorkspace["resource_update"] as Map<String, Any?>
@Suppress("UNCHECKED_CAST")
val sharedOutputs = resourceUpdate["outputs"] as Map<String, Any?>
val nativeStaging = File(workspacePaths["build"] as String, "next/android/native/app")
val generatedAssets = layout.buildDirectory.dir("generatedAssets")

@Suppress("UNCHECKED_CAST")
val releaseSigningPolicy = beWorkspace["android_signing"] as Map<String, Any?>
val signingPropertiesPath = File(releaseSigningPolicy["properties_file"] as String).let {
    if (it.isAbsolute) it else File(rootProject.projectDir.parentFile, it.path)
}
val releaseSigningProperties = Properties()
if (signingPropertiesPath.isFile) signingPropertiesPath.inputStream().use { releaseSigningProperties.load(it) }
val configuredStore = releaseSigningProperties.getProperty("storeFile")?.takeIf { it.isNotBlank() }?.let {
    File(it).let { path -> if (path.isAbsolute) path else File(rootProject.projectDir.parentFile, it) }
}
val configuredAlias = releaseSigningProperties.getProperty("keyAlias")?.takeIf { it.isNotBlank() }
val configuredStorePassword = providers.environmentVariable("BE_ANDROID_STORE_PASSWORD").orNull
    ?: releaseSigningProperties.getProperty("storePassword")?.takeIf { it.isNotEmpty() }
val configuredKeyPassword = providers.environmentVariable("BE_ANDROID_KEY_PASSWORD").orNull
    ?: releaseSigningProperties.getProperty("keyPassword")?.takeIf { it.isNotEmpty() }

val verifyReleaseSigningKey by tasks.registering {
    group = "verification"
    doLast {
        val store = configuredStore
        val alias = configuredAlias
        val storePassword = configuredStorePassword
        val keyPassword = configuredKeyPassword
        check(store != null && store.isFile && alias != null &&
            storePassword != null && keyPassword != null) {
            "Configure the Next signing key in ${signingPropertiesPath.path}; Release never uses the machine debug key."
        }
        val keyStore = KeyStore.getInstance(store, storePassword.toCharArray())
        check(keyStore.isKeyEntry(alias)) { "Configured signing alias is not a private key." }
        val certificate = keyStore.getCertificate(alias)
            ?: error("Configured signing certificate is missing.")
        val digest = MessageDigest.getInstance("SHA-256").digest(certificate.encoded)
            .joinToString("") { "%02x".format(it.toInt() and 255) }
        check(digest == releaseSigningPolicy["certificate_sha256"]) {
            "Signing certificate does not match the Next release identity. Expected ${releaseSigningPolicy["certificate_sha256"]}, found $digest."
        }
        check(keyStore.getKey(alias, keyPassword.toCharArray()) is java.security.PrivateKey) {
            "Configured signing alias has no usable private key."
        }
    }
}

android {
    namespace = "dev.betterendfield.next"
    compileSdk = 37
    ndkVersion = "27.2.12479018"

    buildFeatures {
        buildConfig = true
    }

    defaultConfig {
        applicationId = "dev.betterendfield.next"
        // The libxposed API 102 service is the only framework entry point, and it
        // needs Android 10. The legacy API 82 build was dropped in 3.3.0.
        minSdk = 29
        targetSdk = 35
        versionCode = 40106
        versionName = "4.1.6"
        testInstrumentationRunner = "dev.betterendfield.next.BemInstallerTest"

        ndk {
            abiFilters += "arm64-v8a"
        }

        externalNativeBuild {
            cmake {
                cppFlags += listOf("-std=c++20")
                arguments += listOf("-DANDROID_STL=c++_static",
                    "-DDOBBY_ROOT=${(workspaceTools["android_dobby"] as String).replace('\\', '/')}")
            }
        }
    }

    signingConfigs {
        create("persistentRelease") {
            storeFile = configuredStore
            storePassword = configuredStorePassword
            keyAlias = configuredAlias
            keyPassword = configuredKeyPassword
        }
    }

    buildTypes {
        debug {
            isJniDebuggable = true
        }
        release {
            isMinifyEnabled = true
            isShrinkResources = true
            signingConfig = signingConfigs.getByName("persistentRelease")
            proguardFiles(
                getDefaultProguardFile("proguard-android-optimize.txt"),
                "proguard-rules.pro"
            )
        }
    }

    externalNativeBuild {
        cmake {
            path = file("src/main/cpp/CMakeLists.txt")
            version = "3.22.1"
            // AGP forbids native staging inside this project's Gradle buildDir.
            buildStagingDirectory = nativeStaging
        }
    }

    packaging {
        jniLibs {
            useLegacyPackaging = true
        }
    }

    androidResources {
        // The sustained-dash bone-pose banks are copied out to the game's files
        // directory once and then skipped on a size match. Storing them
        // uncompressed is what makes the asset's reported length the real one.
        noCompress += "bin"
    }

    compileOptions {
        sourceCompatibility = JavaVersion.VERSION_17
        targetCompatibility = JavaVersion.VERSION_17
    }

    sourceSets {
        getByName("main").assets.directories.add(generatedAssets.get().asFile.absolutePath)
    }
}

val androidCoreSource = JsonSlurper().parse(rootProject.file("resources/resource-source.json")) as Map<*, *>
val androidCoreGame = (androidCoreSource["provenance"] as Map<*, *>)["gameVersion"] as String
val resourcePackRegistry = JsonSlurper().parse(rootProject.file("../config/resource-packs.json")) as Map<*, *>
val androidCoreRevisions = (((resourcePackRegistry["revisions"] as Map<*, *>)["core"] as Map<*, *>)["android"] as Map<*, *>)
val bundledAndroidCoreArchive = File(workspacePaths["build"] as String,
    "resource-pack-archives/BE-Core-Android-$androidCoreGame.${androidCoreRevisions[androidCoreGame] ?: 1}.zip")

val prepareAndroidResourceAssets by tasks.registering(Sync::class) {
    dependsOn("stageAndroidCoreResources")
    // The baseline pack is data, extracted into the same external resource store.
    // Optional preview geometry/textures are still downloaded separately.
    from(bundledAndroidCoreArchive) { into("bundled-resources"); rename { "core.zip" } }
    from(rootProject.file("../resources/bem-preview")) { exclude("*.zip"); into("bem-preview") }
    into(generatedAssets)
}

// Build the external companion and the identical offline baseline for the APK.
val stageAndroidCoreResources by tasks.registering(Exec::class) {
    workingDir(rootProject.projectDir.parentFile)
    commandLine(workspaceTools["python"] as String, "scripts/BuildResourcePacks.py",
        "--platform", "android", "--type", "core",
        "--output", File(workspacePaths["build"] as String, "resource-packs").absolutePath,
        "--archives", File(workspacePaths["build"] as String, "resource-pack-archives").absolutePath)
}

val archiveAndroidRelease by tasks.registering(Copy::class) {
    doNotTrackState("Release archives share their directory with other platform installers.")
    from(layout.buildDirectory.dir("outputs/apk/release")) {
        include("*.apk")
        rename { "BetterEndfieldNext-${android.defaultConfig.versionName}-Android-arm64.apk" }
    }
    into(File(workspacePaths["releases"] as String,
        "${android.defaultConfig.versionName}"))
    onlyIf { tasks.named("assembleRelease").get().state.failure == null }
}
tasks.matching { it.name == "assembleRelease" }.configureEach {
    finalizedBy(archiveAndroidRelease)
}

val archiveAndroidBundleRelease by tasks.registering(Copy::class) {
    from(layout.buildDirectory.dir("outputs/bundle/release")) {
        include("*.aab")
        rename { "BetterEndfieldNext-${android.defaultConfig.versionName}-Android-arm64.aab" }
    }
    into(File(workspacePaths["releases"] as String,
        "${android.defaultConfig.versionName}"))
    onlyIf { tasks.named("bundleRelease").get().state.failure == null }
}
tasks.matching { it.name == "bundleRelease" }.configureEach {
    finalizedBy(archiveAndroidBundleRelease)
}

val verifyDesktopModelHookParity by tasks.registering {
    val modelSource = rootProject.file("../native/modules/model/module.cpp")
    inputs.file(modelSource)
    doLast {
        // CustomModel also hooks Internal_CloneSingleWithParent; the Host
        // create_hook chains both modules on that target (2026-10-03).
        val source = modelSource.readText()
        val expected = linkedMapOf(
            "login_bind" to "LoginBindHook",
            "init_main_hash" to "InitMainHashHook",
            "init_initial_hash" to "InitInitialHashHook",
            "anim_tick" to "AnimationTickHook",
            "anim_release" to "AnimationReleaseHook",
            "anim_change_state" to "AnimationChangeStateHook",
            "anim_reset_a1" to "AnimationResetA1Hook",
            "anim_play_special" to "AnimationSpecialHook",
            "anim_play_transition" to "AnimationTransitionHook",
            "clone_with_parent" to "CloneWithParentHook",
            "login_decorate_tick" to "LoginDecorateTickHook",
            "login_decorate_release" to "LoginDecorateReleaseHook",
            "login_enter_value_changed" to "LoginEnterGamePanelValueChangedHook",
            "login_material_animation_late_tick" to "LoginMaterialAnimationLateTickHook",
            "canvas_update_perform" to "CanvasUpdatePerformHook"
        )
        val missing = expected.filter { (field, detour) ->
            !Regex(
                "Hook\\s*\\(\\s*g_methods\\.${Regex.escape(field)}\\s*," +
                    "\\s*reinterpret_cast<void\\*>\\s*\\(&${Regex.escape(detour)}\\)",
                setOf(RegexOption.DOT_MATCHES_ALL)
            ).containsMatchIn(source)
        }
        check(missing.isEmpty()) {
            "Android model Hook parity failed; missing desktop entries: " +
                missing.entries.joinToString { "${it.key}->${it.value}" }
        }
        logger.lifecycle(
            "Verified Android model parity against ${expected.size} desktop Hook entries")
    }
}

tasks.named("preBuild").configure {
    dependsOn(prepareAndroidResourceAssets)
    dependsOn(stageAndroidCoreResources)
    dependsOn(verifyDesktopModelHookParity)
}

tasks.matching { it.name == "preReleaseBuild" || it.name == "validateSigningRelease" ||
    it.name == "packageRelease" || it.name == "signReleaseBundle" }.configureEach {
    dependsOn(verifyReleaseSigningKey)
}

dependencies {
    implementation("org.apache.commons:commons-compress:1.28.0")
    implementation("org.tukaani:xz:1.10")
    compileOnly("io.github.libxposed:api:102.0.0")
    implementation("io.github.libxposed:service:102.0.0")
}

