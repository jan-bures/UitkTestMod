using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Ksp2Uitk.API;
using SpaceWarp;
using SpaceWarp.API.Assets;
using SpaceWarp.API.Mods;
using UnityEngine.UIElements;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable UnusedMember.Global

namespace UitkTestMod;

[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
[BepInDependency(SpaceWarpPlugin.ModGuid, SpaceWarpPlugin.ModVer)]
public class UitkTestModPlugin : BaseSpaceWarpPlugin
{
    // These are useful in case some other mod wants to add a dependency to this one
    public const string ModGuid = MyPluginInfo.PLUGIN_GUID;
    public const string ModName = MyPluginInfo.PLUGIN_NAME;
    public const string ModVer = MyPluginInfo.PLUGIN_VERSION;

    public static UitkTestModPlugin Instance { get; set; }
    public new static ManualLogSource Logger { get; set; }

    private void Awake()
    {
        Harmony.CreateAndPatchAll(typeof(UitkTestModPlugin));
    }

    public override void OnInitialized()
    {
        base.OnInitialized();

        Instance = this;
        Logger = base.Logger;

        CreateDynamicGUI();
        // CreateUxmlGUI();

        Logger.LogInfo($"{ModName} is initialized.");
    }

    private static void CreateDynamicGUI()
    {
        var root = Element.Root();
        root.style.flexGrow = 1;
        root.AddChildren(new VisualElement[]
        {
            Element.Label(text: "This is a Label"),
            Element.Button("button", "This is a button"),
            Element.Toggle("toggle", "Displaying the counter?"),
            Element.TextField("input-message", "This is a text field").Set("label", "Text Field")
        });

        var window = Window.CreateFromElement(root);
    }

    private void CreateUxmlGUI()
    {
        var uxml = AssetManager.GetAsset<VisualTreeAsset>($"{SpaceWarpMetadata.ModID}/uxml/testingdocument.uxml");
        var window = Window.CreateFromUxml(uxml);
    }
}