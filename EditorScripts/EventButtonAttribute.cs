

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif

namespace UnityUtils {

//these classes create a button to execute a UnityEvent. Only works for UnityEvents :(
[AttributeUsage(AttributeTargets.Field)]
public class EventButtonAttribute : PropertyAttribute {
    public string label;
    public bool showDefaultInspector;

    public EventButtonAttribute(string label = "", bool showDefaultInspector = false) {
        this.label = label;
        this.showDefaultInspector = showDefaultInspector;
    }
}

#if UNITY_EDITOR

[CustomPropertyDrawer(typeof(EventButtonAttribute))]
public class EventButtonDrawer : PropertyDrawer { //inherit from property drawer to change how a property is drawn

    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
        VisualElement container = new VisualElement();
        EventButtonAttribute eventButtonAttribute = attribute as EventButtonAttribute;

        if (eventButtonAttribute.showDefaultInspector) {
            PropertyField defaultInspector = new PropertyField(property);
            container.Add(defaultInspector);
        }

        //Reflection nonsense to actually get the Event this button triggers
        //Type type = property.serializedObject.targetObject.GetType();
        //FieldInfo fieldInfo = type.GetField(property.name);
        var monobehaviour = property.serializedObject.targetObject;
        var field = fieldInfo.GetValue(monobehaviour);

        //only try to invoke event if this attribute was placed on an event!
        if (field is UnityEvent) {
            UnityEvent daEvent = field as UnityEvent;
            Button button = new Button();

            string label = eventButtonAttribute.label;
            if (label.Length == 0) label = UnityUtils.FormatCamelCase(property.name);
            button.text = label;

            int margins = 2;
            button.style.marginTop = margins;
            button.style.marginBottom = margins;

            button.clickable = new Clickable(() => { daEvent.Invoke(); });

            container.Add(button);
        }
        else {
            //display an error message if no event is found
            Label errorLabel = new Label();

            errorLabel.text = "Error: Field " + property.name + " is not of type UnityEvent.";
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


