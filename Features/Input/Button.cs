namespace UnityUtils.Input {
    public enum ButtonState {
        Up,
        Pressed,
        Down,
        Released
    }

    public struct Button {
        public ButtonState state;
        public bool value;

        public Button(bool oldValue, bool newValue) {
            value = newValue;
            if (!oldValue && !newValue) state = ButtonState.Up;
            else if (!oldValue && newValue) state = ButtonState.Pressed;
            else if (oldValue && newValue) state = ButtonState.Down;
            else state = ButtonState.Released;
        }
    }
}
