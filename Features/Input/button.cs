
using System;
using Unity.Entities.UI;
using UnityEditor.Compilation;
using UnityEngine.UIElements;

namespace UnityUtils.Input {
    public enum ButtonState { 
        Up,
        Pressed,
        Down,
        Released
    }

    [Serializable]
    public struct button {
        public ButtonState state;
        public bool value;

        public button(bool oldValue, bool newValue) {
            value = newValue;
            if (!oldValue && !newValue) state = ButtonState.Up;
            else if (!oldValue && newValue) state = ButtonState.Pressed;
            else if (oldValue && newValue) state = ButtonState.Down;
            else state = ButtonState.Released;
        }
        
        public bool up => state == ButtonState.Up;
        public bool pressed => state == ButtonState.Pressed;
        public bool down => state == ButtonState.Down;
        public bool released => state == ButtonState.Released;
    }
    
    #if UNITY_EDITOR
    //https://discussions.unity.com/t/custom-property-drawer-for-components-and-buffers/935018/7
    // class ButtonInspector : PropertyInspector<button> {
    //     public override VisualElement Build() {
    //         button target = Target;
    //         var field = new EnumField("State");
    //         field.value = target.state;
    //         return field;
    //     }
    // }
#endif
}
