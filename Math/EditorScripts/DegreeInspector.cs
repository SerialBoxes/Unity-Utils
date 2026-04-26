#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NoodleKit.Math {

[CustomPropertyDrawer(typeof(Degree))]
public class DegreeDrawer : PropertyDrawer {
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
        VisualElement container = new VisualElement();

        PropertyField field = new PropertyField(property.FindPropertyRelative("value"));
        field.RegisterValueChangeCallback((evt) => {
            evt.changedProperty.floatValue = Degree.Mod360Signed(evt.changedProperty.floatValue);
            evt.changedProperty.serializedObject.ApplyModifiedProperties();
        });
        field.label = property.displayName;

        container.Add(field);

        return container;
    }
}
}
#endif
