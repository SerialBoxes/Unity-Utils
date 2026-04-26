#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Unity.Mathematics {

[CustomPropertyDrawer(typeof(dangle))]
public class dangleDrawer : PropertyDrawer {
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
        VisualElement container = new VisualElement();

        PropertyField field = new PropertyField(property.FindPropertyRelative("value"));
        field.RegisterValueChangeCallback((evt) => {
            evt.changedProperty.floatValue = dangle.Mod360Signed(evt.changedProperty.floatValue);
            evt.changedProperty.serializedObject.ApplyModifiedProperties();
        });
        field.label = property.displayName;

        container.Add(field);

        return container;
    }
}
}
#endif
