

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
#endif

namespace NoodleKit {

//these classes create a button to execute a UnityEvent. Only works for UnityEvents :(
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
public class ButtonAttribute : PropertyAttribute {
    public string function;
    public string label;
    public bool inEditor;
    public bool hideField;

    public ButtonAttribute(string function, string label = "", bool inEditor = false, bool hideField = false) {
        this.label = label;
        this.function = function;
        this.inEditor = inEditor;
        this.hideField = hideField;
    }
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(ButtonAttribute))]
public class ButtonDrawer : PropertyDrawer { //inherit from property drawer to change how a property is drawn

    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
        VisualElement container = new VisualElement();
        ButtonAttribute buttonAttribute = attribute as ButtonAttribute;

        if (!buttonAttribute.hideField) {
            PropertyField defaultInspector = new PropertyField(property);
            container.Add(defaultInspector);
        }

        //hooooooooooooly reflection batman
        //hooooooooooooly reflection batman
        Type type = property.serializedObject.targetObject.GetType();
        MethodInfo[] methods = type.GetMethods();
        MethodInfo method = null;
        foreach (var m in methods) {
            if (m.Name == buttonAttribute.function && m.GetParameters().Length == 0) {
                method = m;
                break;
            }
        }

        if (method != null) {
            Button button = new Button();

            string label = buttonAttribute.label;
            if (label.Length == 0) label = NoodleUtils.FormatCamelCase(buttonAttribute.function);
            button.text = label;

            int margins = 2;
            button.style.marginTop = margins;
            button.style.marginBottom = margins;

            button.clickable = new Clickable(() => {
                if (Application.isPlaying || buttonAttribute.inEditor) {
                    method.Invoke(property.serializedObject.targetObject, null);
                }
            });

            container.Add(button);
        }
        else {
            Label errorLabel = new Label();

            errorLabel.text = "Error: Function " + buttonAttribute.function + " doesn't exist or isn't public!";
            errorLabel.style.color = Color.red;

            int errorMargin = 4;
            errorLabel.style.marginTop = errorMargin;
            errorLabel.style.marginBottom = errorMargin;
            errorLabel.style.marginLeft = errorMargin;
            errorLabel.style.marginRight = errorMargin;

            container.Add(errorLabel);
        }

        return container;
    }
}
#endif
}


