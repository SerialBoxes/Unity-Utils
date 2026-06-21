
using System;
using Unity.Entities.UI;
using Unity.Mathematics;
using UnityEngine.UIElements;
using UnityUtils.VisualElements;

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
        
        public bool up => state == ButtonState.Up || state == ButtonState.Released;
        public bool pressed => state == ButtonState.Pressed;
        public bool down => state == ButtonState.Down || state == ButtonState.Pressed;
        public bool released => state == ButtonState.Released;
    }
    
    #if UNITY_EDITOR
    //https://discussions.unity.com/t/custom-property-drawer-for-components-and-buffers/935018/7
    class ButtonInspector : PropertyInspector<button> {
        private TextField text;
        public override VisualElement Build() {
            VisualElement container = new VisualElement();
            text = new TextField(Name.SplitPascalCase());
            text.value = Target.state.ToString();
            container.Add(text);
            //register callbacks here if you want to be able to change values
            return container;
        }

        public override void Update() {
            text.value = Target.state.ToString();
        }
    }
#endif
}
