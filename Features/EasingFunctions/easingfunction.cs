using Unity.Entities.UI;
using Unity.Mathematics;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using UnityUtils.Mathematics;
using UnityUtils.VisualElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityUtils
{
    public enum EasingFunctionType {
        Linear,
        EaseInSine,
        EaseInQuad,
        EaseInCubic,
        EaseInQuart,
        EaseInQuint,
        EaseInExpo,
        EaseInCirc,
        EaseInBack,
        EaseOutSine,
        EaseOutQuad,
        EaseOutCubic,
        EaseOutQuart,
        EaseOutQuint,
        EaseOutExpo,
        EaseOutCirc,
        EaseOutBack,
        EaseInOutSine,
        EaseInOutQuad,
        EaseInOutCubic,
        EaseInOutQuart,
        EaseInOutQuint,
        EaseInOutExpo,
        EaseInOutCirc,
        EaseInOutBack,
    }
    [System.Serializable]
    public struct easingfunction
    {

        public EasingFunctionType type;
        
        public easingfunction(EasingFunctionType type) {
            this.type = type;
        }
        
        
        public float Evaluate(float t, float minI, float maxI, float minO, float maxO, bool clampInput = true) {
            float input = roxmath.MapFromRange(t, minI, maxI);
            if (clampInput) input = math.clamp(input, 0, 1);
            float fitToCurve = Evaluate(input);
            return roxmath.MapToRange(fitToCurve, minO, maxO);
        }

        public float Evaluate(float x) => type switch {
            EasingFunctionType.Linear      => EasingFunctions.Linear(x),
            EasingFunctionType.EaseInSine  => EasingFunctions.EaseInSine(x),
            EasingFunctionType.EaseInQuad  => EasingFunctions.EaseInQuad(x),
            EasingFunctionType.EaseInCubic => EasingFunctions.EaseInCubic(x),
            EasingFunctionType.EaseInQuart => EasingFunctions.EaseInQuart(x),
            EasingFunctionType.EaseInQuint => EasingFunctions.EaseInQuint(x),
            EasingFunctionType.EaseInExpo  => EasingFunctions.EaseInExpo(x),
            EasingFunctionType.EaseInCirc  => EasingFunctions.EaseInCirc(x),
            EasingFunctionType.EaseInBack  => EasingFunctions.EaseInBack(x),
            EasingFunctionType.EaseOutSine  => EasingFunctions.EaseOutSine(x),
            EasingFunctionType.EaseOutQuad  => EasingFunctions.EaseOutQuad(x),
            EasingFunctionType.EaseOutCubic => EasingFunctions.EaseOutCubic(x),
            EasingFunctionType.EaseOutQuart => EasingFunctions.EaseOutQuart(x),
            EasingFunctionType.EaseOutQuint => EasingFunctions.EaseOutQuint(x),
            EasingFunctionType.EaseOutExpo  => EasingFunctions.EaseOutExpo(x),
            EasingFunctionType.EaseOutCirc  => EasingFunctions.EaseOutCirc(x),
            EasingFunctionType.EaseOutBack  => EasingFunctions.EaseOutBack(x),
            EasingFunctionType.EaseInOutSine  => EasingFunctions.EaseInOutSine(x),
            EasingFunctionType.EaseInOutQuad  => EasingFunctions.EaseInOutQuad(x),
            EasingFunctionType.EaseInOutCubic => EasingFunctions.EaseInOutCubic(x),
            EasingFunctionType.EaseInOutQuart => EasingFunctions.EaseInOutQuart(x),
            EasingFunctionType.EaseInOutQuint => EasingFunctions.EaseInOutQuint(x),
            EasingFunctionType.EaseInOutExpo  => EasingFunctions.EaseInOutExpo(x),
            EasingFunctionType.EaseInOutCirc  => EasingFunctions.EaseInOutCirc(x),
            EasingFunctionType.EaseInOutBack  => EasingFunctions.EaseInOutBack(x),
            _ => x,
        };
        
        public float2[] GenerateGraphPoints(int pointCount) {
            float2[] data = new float2[pointCount];
            for (int i = 0; i < pointCount; i++) {
                float x = i / (pointCount - 1f);
                data[i] = new float2(x, Evaluate(x));
            }
            return data;
        }
    }
    
#if UNITY_EDITOR
    //https://discussions.unity.com/t/custom-property-drawer-for-components-and-buffers/935018/7
    class EasingFunctionInspectors {
        class EasingFunctionInspectorECS : PropertyInspector<easingfunction> {
            private EnumField typeLabel;
            private LineChart graph;
            private EasingFunctionType lastTypeValue;

            public override VisualElement Build() {
                typeLabel = new EnumField(EasingFunctionType.Linear);
                graph = new LineChart();
                lastTypeValue = Target.type;
                //register callbacks here if you want to be able to change values
                return buildInspector(graph, typeLabel, Target, Name);
            }

            public override void Update() {
                if (Target.type != lastTypeValue) {
                    typeLabel.value = Target.type;
                    graph.data = Target.GenerateGraphPoints(100);
                    lastTypeValue = Target.type;
                }
            }
        }

        [CustomPropertyDrawer(typeof(easingfunction))]
        class EasingFunctionInspectorMono : PropertyDrawer {
            public override VisualElement CreatePropertyGUI(SerializedProperty property) {
                LineChart graph = new LineChart();
                EnumField typeLabel = new EnumField(EasingFunctionType.Linear);
                typeLabel.BindProperty(property.FindPropertyRelative("type"));
                typeLabel.RegisterValueChangedCallback(evt => graph.data = new easingfunction((EasingFunctionType)evt.newValue).GenerateGraphPoints(40));
                easingfunction target = new easingfunction((EasingFunctionType)property.FindPropertyRelative("type").enumValueIndex);
                return buildInspector(graph, typeLabel, target, preferredLabel);
            }
        }

        private static VisualElement buildInspector(LineChart graph, EnumField typeField, easingfunction Target, string Name) {
            VisualElement container = new VisualElement();
            container.style.display = DisplayStyle.Flex;
            container.style.flexDirection = FlexDirection.Row;
            container.style.marginLeft = 3;
            container.style.marginRight = -2;
            container.style.marginTop = 1;
            container.style.marginBottom = 1;

            var nameLabel = new Label(Name.SplitPascalCase());
            nameLabel.style.flexShrink = 0f;
            nameLabel.style.flexGrow = 0f;
            nameLabel.style.minWidth = 76;
            container.Add(nameLabel);
            
            //container.RegisterCallback((GeometryChangedEvent evt) => { nameLabel.style.width = ((VisualElement)evt.currentTarget).panel.visualTree.contentRect.width * 0.45f - 100;});
            container.RegisterCallback((GeometryChangedEvent evt) => { nameLabel.style.width = FindContainer((VisualElement)evt.currentTarget).contentRect.width * 0.45f - 50;});

            VisualElement rightSide = new VisualElement();
            rightSide.style.display = DisplayStyle.Flex;
            rightSide.style.flexDirection = FlexDirection.Row;
            rightSide.style.flexShrink = 0f;
            rightSide.style.flexGrow = 1f;
            container.Add(rightSide);

            VisualElement typeContainer = new VisualElement();
            typeContainer.style.flexGrow = 1f;
            typeContainer.style.flexBasis = 1f;
            rightSide.Add(typeContainer);
            
            typeField.value = Target.type;
            typeContainer.Add(typeField);
            
            graph.style.height = 40;
            graph.style.flexGrow = 1f;
            graph.style.flexBasis = 1f;
            graph.data = Target.GenerateGraphPoints(40);
            rightSide.Add(graph);
            //register callbacks here if you want to be able to change values
            return container;
        }

        private static VisualElement FindContainer(VisualElement element) {
            if (element.parent is null || element.ClassListContains("unity-inspector-main-container")) return element;
            return FindContainer(element.parent);
        }
    }
#endif
}
