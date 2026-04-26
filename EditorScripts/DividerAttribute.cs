

using System;
using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace NoodleKit {

//These classes add a simple divider attribute that adds a lil spacer in the inspector! 
[AttributeUsage(AttributeTargets.Field)]
public class DividerAttribute : PropertyAttribute {
    public float thickness;
    public float margin;

    public DividerAttribute(float thickness = 1.5f, float margin = 12) {
        this.thickness = thickness;
        this.margin = margin;
    }
}
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(DividerAttribute))]
public class DividerDrawer : DecoratorDrawer { //inherit from property drawer to change how a property is drawn

    public override VisualElement CreatePropertyGUI() {
        VisualElement container = new VisualElement();
        DividerAttribute divider = attribute as DividerAttribute;

        VisualElement bar = new VisualElement();
        bar.style.backgroundColor = NoodleUtils.GetEditorBorderColor();
        bar.style.height = divider!.thickness;
        bar.style.marginLeft = 0;
        bar.style.marginRight = 12; //this centers it with the built-in left margin
        bar.style.marginTop = divider!.margin;
        bar.style.marginBottom = divider!.margin;
        container.Add(bar);
        return container;
    }
}
#endif
}


