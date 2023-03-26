# UITK Test Mod

A project with some examples on using UITK in a KSP 2 SpaceWarp mod.

## Building
1. Copy all .dll files from `<KSP2 Root>/KSP2_x64_Data/Managed` into `external_dlls`.
2. Download a [release of KSP 2 UITK](https://github.com/jan-bures/Ksp2Uitk/releases/latest) or compile it from source.
3. Copy all replace all .dll files from `<KSP 2 UITK build folder>/BepInEx/plugins/ksp2_uitk/lib` into `external_dlls`.
4. Copy the `ksp2_uitk.dll` file from `<KSP 2 UITK build folder>/BepInEx/plugins/ksp2_uitk` into `external_dlls`.
5. Open the `UitkTestMod.sln` solution and rebuild.