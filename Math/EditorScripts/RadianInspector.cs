#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace NoodleKit.Math {

[CustomPropertyDrawer(typeof(Radian))]
public class RadianDrawer : PropertyDrawer {
    
    public override VisualElement CreatePropertyGUI(SerializedProperty property) {
        VisualElement container = new VisualElement();
        
        PropertyField field = new PropertyField(property.FindPropertyRelative("value"));
        field.RegisterValueChangeCallback((evt) => {
            evt.changedProperty.floatValue = Radian.Mod2PISigned(evt.changedProperty.floatValue);
            evt.changedProperty.serializedObject.ApplyModifiedProperties();
        });
        field.label = property.displayName;
        
        container.Add(field);

        return container;
    }
}
}
#endif