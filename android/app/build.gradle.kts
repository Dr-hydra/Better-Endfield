plugins {
    id("com.android.application")
}

android {
    namespace = "dev.betterendfield.android"
    compileSdk = 35
    ndkVersion = "27.2.12479018"

    buildFeatures {
        buildConfig = true
    }

    defaultConfig {
        applicationId = "dev.betterendfield.android"
        minSdk = 27
        targetSdk = 35
        versionCode = 30101
        versionName = "3.1.1"

        ndk {
            abiFilters += "arm64-v8a"
        }

        externalNativeBuild {
            cmake {
                cppFlags += listOf("-std=c++20")
                arguments += listOf("-DANDROID_STL=c++_static")
            }
        }
    }

    buildTypes {
        debug {
            isJniDebuggable = true
        }
        release {
            isMinifyEnabled = false
            signingConfig = signingConfigs.getByName("debug")
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
        }
    }

    packaging {
        jniLibs {
            useLegacyPackaging = true
        }
    }

    compileOptions {
        sourceCompatibility = JavaVersion.VERSION_17
        targetCompatibility = JavaVersion.VERSION_17
    }

    sourceSets {
        getByName("main").assets.srcDir(
            layout.buildDirectory.dir("generated/androidResourceAssets"))
    }
}

val prepareAndroidResourceAssets by tasks.registering(Copy::class) {
    from(rootProject.file("resources/voice-catalog-index.json"))
    from(rootProject.file("resources/character-names.json"))
    from(rootProject.file("resources/character-presets.json"))
    into(layout.buildDirectory.dir("generated/androidResourceAssets"))
}

val verifyDesktopModelHookParity by tasks.registering {
    val modelSource = rootProject.file("../native/modules/model/module.cpp")
    inputs.file(modelSource)
    doLast {
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

dependencies {
    compileOnly("de.robv.android.xposed:api:82")
}
