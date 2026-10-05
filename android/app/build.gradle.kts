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
val nativeStaging = File(workspacePaths["build"] as String, "android/native/app")
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
            "Configure the original 3.5.0 signing key in ${signingPropertiesPath.path}; Release never uses the machine debug key."
        }
        val keyStore = KeyStore.getInstance(store, storePassword.toCharArray())
        check(keyStore.isKeyEntry(alias)) { "Configured signing alias is not a private key." }
        val certificate = keyStore.getCertificate(alias)
            ?: error("Configured signing certificate is missing.")
        val digest = MessageDigest.getInstance("SHA-256").digest(certificate.encoded)
            .joinToString("") { "%02x".format(it.toInt() and 255) }
        check(digest == releaseSigningPolicy["certificate_sha256"]) {
            "Signing certificate does not match published 3.5.0. Expected ${releaseSigningPolicy["certificate_sha256"]}, found $digest."
        }
        check(keyStore.getKey(alias, keyPassword.toCharArray()) is java.security.PrivateKey) {
            "Configured signing alias has no usable private key."
        }
    }
}

android {
    namespace = "dev.betterendfield.android"
    compileSdk = 37
    ndkVersion = "27.2.12479018"

    buildFeatures {
        buildConfig = true
    }

    defaultConfig {
        applicationId = "dev.betterendfield.android"
        // The libxposed API 102 service is the only framework entry point, and it
        // needs Android 10. The legacy API 82 build was dropped in 3.3.0.
        minSdk = 29
        targetSdk = 35
        versionCode = 30501
        versionName = "3.5.1"
        testInstrumentationRunner = "dev.betterendfield.android.BemInstallerTest"

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
            isMinifyEnabled = false
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
        getByName("main").assets.srcDir(generatedAssets.get().asFile)
    }
}

val prepareAndroidResourceAssets by tasks.registering(Sync::class) {
    // Android voice descriptors and model increments remain maintained inputs.
    // Share the canonical file only when the complete JSON content agrees;
    // neither source is rewritten during packaging.
    for ((name, key) in listOf("voice-catalog-index.json" to "voice_index",
                              "character-presets.json" to "character_presets")) {
        val shared = File(sharedOutputs[key] as String)
        val platform = rootProject.file("resources/$name")
        inputs.files(shared, platform)
        from(providers.provider {
            check(shared.isFile) { "Shared resource is missing: $shared" }
            if (!platform.isFile || JsonSlurper().parse(shared) == JsonSlurper().parse(platform)) {
                shared
            } else {
                platform
            }
        }) {
            rename { name }
        }
    }
    from(rootProject.file("resources/character-names.json"))
    // Same layout the desktop module reads from beside its DLL, so one set of
    // bone-pose banks serves both platforms.
    from(rootProject.file("../native/modules/actions/assets")) {
        include("pose_*.bin")
        into("actions")
    }
    into(generatedAssets)
}

val archiveAndroidRelease by tasks.registering(Copy::class) {
    from(layout.buildDirectory.dir("outputs/apk/release")) {
        include("*.apk")
        rename { "BetterEndfield-${android.defaultConfig.versionName}-Android-arm64.apk" }
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
        rename { "BetterEndfield-${android.defaultConfig.versionName}-Android-arm64.aab" }
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
