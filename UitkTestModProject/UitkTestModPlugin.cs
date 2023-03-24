using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using KSP2UITK;
using SpaceWarp;
using SpaceWarp.API.Assets;
using SpaceWarp.API.Mods;
using UnityEngine;
using UnityEngine.UIElements;

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
        
        // CreateDynamicGUI();
        CreateUxmlGUI();
        // CreateBuilderPrefabGUI();
        // CreatePrefabGUI();

        Logger.LogInfo($"{ModName} is initialized.");
    }

    private void CreateDynamicGUI()
    {
        /*
         The UXML we're trying to recreate (from https://docs.unity3d.com/Manual/UIE-get-started-with-runtime-ui.html):
         
<ui:UXML xmlns:ui="UnityEngine.UIElements" xmlns:uie="UnityEditor.UIElements"
        xsi="http://www.w3.org/2001/XMLSchema-instance" engine="UnityEngine.UIElements" editor="UnityEditor.UIElements"
        noNamespaceSchemaLocation="../UIElementsSchema/UIElements.xsd" editor-extension-mode="False">
    <ui:VisualElement style="flex-grow: 1;">
        <ui:Label text="This is a Label" display-tooltip-when-elided="true"/>
        <ui:Button text="This is a Button" display-tooltip-when-elided="true" name="button"/>
        <ui:Toggle label="Display the counter?" name="toggle"/>
        <ui:TextField picking-mode="Ignore" label="Text Field" text="filler text" name="input-message" />
    </ui:VisualElement>
</ui:UXML>
         */

        // Create an object with a UIDocument component
        var go = new GameObject("My UI object");
        var doc = go.AddComponent<UIDocument>();

        // Add VisualElements to the UIDocument
        doc.rootVisualElement.Set(flexGrow: 1);
        var ve = new VisualElement[]
        {
            new Label().Set(text: "This is a Label"),
            new Button().Set(text: "This is a button").Set(name: "button"),
            new Toggle("Displaying the counter?").Set(name: "toggle"),
            new TextField("Text Field").Set(name: "input-message").AssignTo(out var tf)
        };
        tf.pickingMode = PickingMode.Ignore;
        tf.value = "filler text";
        foreach (var element in ve)
        {
            doc.rootVisualElement.Add(element);
        }

        // Add PanelSettings to the UIDocument component
        doc.panelSettings = KSP2UITKPlugin.PanelSettings;

        // Enable the UIDocument
        doc.AddRootVisualElementToTree();
        doc.enabled = true;

        // Place and activate the object
        go.transform.parent = transform;
        go.SetActive(true);
    }

    private void CreateUxmlGUI()
    {
        var go = new GameObject("My UXML object");
        var doc = go.AddComponent<UIDocument>();

        var uxml = AssetManager.GetAsset<VisualTreeAsset>($"{SpaceWarpMetadata.ModID}/uxml/mydoctemplate.uxml");
        doc.sourceAsset = uxml;

        doc.panelSettings = KSP2UITKPlugin.PanelSettings;

        doc.RecreateUI();
        doc.enabled = true;

        go.transform.parent = transform;
        go.SetActive(true);
    }

    private void CreateBuilderPrefabGUI()
    {
        var prefab = AssetManager.GetAsset<GameObject>($"{SpaceWarpMetadata.ModID}/builder_prefab/builderdoc.prefab");
        var doc = prefab.GetComponent<UIDocument>();

        doc.RecreateUI();
        doc.enabled = true;

        prefab.transform.parent = transform;
        prefab.SetActive(true);
    }

    private void CreatePrefabGUI()
    {
        var prefab = AssetManager.GetAsset<GameObject>($"{SpaceWarpMetadata.ModID}/prefab/mydocgo.prefab");
        var doc = prefab.GetComponent<UIDocument>();

        doc.RecreateUI();
        doc.enabled = true;

        prefab.transform.parent = transform;
        prefab.SetActive(true);
    }
}