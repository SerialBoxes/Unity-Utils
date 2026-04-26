using System;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif

namespace UnityUtils {

//These classes add a simple divider attribute that adds a lil spacer in the inspector! 
[AttributeUsage(AttributeTargets.Field)]
public class UnitsAttribute : PropertyAttribute {
    public string unitsText;
    public string hexTransparency;

    public UnitsAttribute(string unitsText, string hexTransparency = "#88") {
        this.unitsText = unitsText;
        this.hexTransparency = hexTransparency;
    }
}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(UnitsAttribute))]
public class UnitsDrawer : PropertyDrawer {

    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
        VisualElement container = new VisualElement();
        UnitsAttribute unitsAttribute = attribute as UnitsAttribute;
        container.style.flexDirection = FlexDirection.Row;
        container.style.alignItems = Align.FlexEnd;
        
        PropertyField field = new PropertyField(property);
        field.style.flexGrow = 1;
        
        Label unitsLabel = new Label();
        unitsLabel.text = $"<alpha={unitsAttribute!.hexTransparency}> {unitsAttribute.unitsText}<alpha=#FF>";
        unitsLabel.style.marginBottom = 4;
        
        container.Add(field);
        container.Add(unitsLabel);

        return container;
    }
}
#endif
}