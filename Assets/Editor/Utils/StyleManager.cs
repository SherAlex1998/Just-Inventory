using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using TMPro;

public class StyleManager : EditorWindow
{
    [MenuItem("Window/UI Toolkit/StyleManager")]
    public static void ShowExample()
    {
        StyleManager wnd = GetWindow<StyleManager>();
        wnd.titleContent = new GUIContent("StyleManager");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/Utils/StyleManager.uxml");
        VisualElement UXMLVisualElements = visualTree.Instantiate();
        root.Add(UXMLVisualElements);

        Button setStyleButton = rootVisualElement.Q<Button>("setStyleButton");
        setStyleButton.RegisterCallback<ClickEvent>(ChangeStyle);
    }

    private void ChangeStyle(ClickEvent evt)
    {
        ColorField colorField = rootVisualElement.Q<ColorField>("textColorSelector");

        TextMeshProUGUI[] textMeshes = UnityEngine.Object.FindObjectsOfType<TextMeshProUGUI>();
        foreach (var textMesh in textMeshes) 
        {
            textMesh.color = colorField.value;
        }
    }
}