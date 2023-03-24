﻿using UnityEngine;
using UnityEngine.UIElements;

namespace UitkTestMod;

public class SimpleRuntimeUI : MonoBehaviour
{
    private Button _button;
    private Toggle _toggle;

    private int _clickCount;

    //Add logic that interacts with the UI controls in the `OnEnable` methods
    private void OnEnable()
    {
        // The UXML is already instantiated by the UIDocument component
        var uiDocument = GetComponent<UIDocument>();

        _button = uiDocument.rootVisualElement.Q("button") as Button;
        _toggle = uiDocument.rootVisualElement.Q("toggle") as Toggle;

        _button?.RegisterCallback<ClickEvent>(PrintClickMessage);

        var inputFields = uiDocument.rootVisualElement.Q("input-message");
        inputFields.RegisterCallback<ChangeEvent<string>>(InputMessage);
    }

    private void OnDisable()
    {
        _button.UnregisterCallback<ClickEvent>(PrintClickMessage);
    }

    private void PrintClickMessage(ClickEvent evt)
    {
        ++_clickCount;

        UitkTestModPlugin.Logger.LogInfo($"{"button"} was clicked!" +
                  (_toggle.value ? " Count: " + _clickCount : ""));
    }

    public static void InputMessage(ChangeEvent<string> evt)
    {
        UitkTestModPlugin.Logger.LogInfo($"{evt.newValue} -> {evt.target}");
    }
}