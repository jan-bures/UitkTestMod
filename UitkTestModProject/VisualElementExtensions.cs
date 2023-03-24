using SpaceWarp.API.Assets;
using UnityEngine;
using UnityEngine.UIElements;

namespace UitkTestMod;

public static class VisualElementExtensions
{
    public static T Set<T>(this T v,
        string name = null,
        string _class = null,
        FlexDirection? flexDirection = null,
        Justify? justifyContent = null,
        Align? alignItems = null,
        string background_image = null,
        float? flexGrow = null,
        float? maxHeight = null,
        float? maxWidth = null,
        float? height = null,
        float? width = null,
        Color? color = null,
        ScaleMode? unityBackgroundScaleMode = null,
        DisplayStyle? display = null) where T : VisualElement
    {
        if (name != null)
            v.name = name;
        if (_class != null)
            v.AddToClassList(_class);
        if (flexDirection.HasValue)
            v.style.flexDirection = new StyleEnum<FlexDirection>(flexDirection.Value);
        if (alignItems.HasValue)
            v.style.alignItems = new StyleEnum<Align>(alignItems.Value);
        if (flexGrow.HasValue)
            v.style.flexGrow = new StyleFloat(flexGrow.Value);
        if (background_image != null)
            v.style.backgroundImage = Background.FromTexture2D(
                AssetManager.GetAsset<Texture2D>(
                    $"{UitkTestModPlugin.Instance.SpaceWarpMetadata.ModID}/images/{background_image}"));
        if (maxHeight.HasValue)
            v.style.maxHeight = maxHeight.Value;
        if (maxWidth.HasValue)
            v.style.maxWidth = maxWidth.Value;
        if (height.HasValue)
            v.style.height = height.Value;
        if (width.HasValue)
            v.style.width = width.Value;
        if (justifyContent.HasValue)
            v.style.justifyContent = new StyleEnum<Justify>(justifyContent.Value);
        if (color.HasValue)
            v.style.color = new StyleColor(color.Value);
        if (unityBackgroundScaleMode.HasValue)
            v.style.unityBackgroundScaleMode = new StyleEnum<ScaleMode>(unityBackgroundScaleMode.Value);
        if (display.HasValue)
            v.style.display = display.Value;
        return v;
    }

    public static T Set<T>(this T v, string text) where T : TextElement
    {
        if (text != null)
            v.text = text;
        return v;
    }

    public static T AssignTo<T>(this T v, out T reference) where T : VisualElement
    {
        reference = v;
        return v;
    }

    public static VisualElement AddRange(this VisualElement v, params VisualElement[] elements)
    {
        foreach (var el in elements)
            v.Add(el);
        return v;
    }
}