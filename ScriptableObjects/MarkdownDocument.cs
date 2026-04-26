using Markdig;
using NoodleKit;
using Unity.Properties;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif

namespace Noodlekit{

[CreateAssetMenu(fileName = "Markdown", menuName = "Noodle Kit/Markdown")]
public class MarkdownDocument : ScriptableObject {
    
    public TextStyleSheet textStyle;

    [Button("RenderUXML", "Render Document!", true, true)] [SerializeField]
    private float forceButtonRender = 0f;
    
    public string rawText = "";
    
    [field: SerializeField][CreateProperty]
    public string uxmlRichText{ get; private set; }
    
    [SerializeField] public bool showEditor = false;
    
    public void RenderUXML() {
        uxmlRichText = MarkdownExtension.RenderUxml(rawText, textStyle);
    }

    public void DisposeRenderer() {
        MarkdownExtension.Dispose();
    }

}
#if UNITY_EDITOR
[CustomEditor(typeof(MarkdownDocument))]
public class MarkdownDocumentDrawer : Editor {
    
    public override VisualElement CreateInspectorGUI() {
        MarkdownDocument script = target as MarkdownDocument;
        VisualElement container = new VisualElement();
        
        VisualElement editor = new VisualElement();
        editor.style.display = script.showEditor?DisplayStyle.Flex:DisplayStyle.None;
            
        VisualElement properties = new VisualElement();
            
        PropertyField styleField = new PropertyField(serializedObject.FindProperty("textStyle"));
        styleField.style.marginRight = 40;
        properties.Add(styleField);
        PropertyField renderField = new PropertyField(serializedObject.FindProperty("forceButtonRender"));
        properties.Add(renderField);
            
        editor.Add(properties);
        
        TextField textBox = new TextField();
        textBox.BindProperty(serializedObject.FindProperty("rawText"));
        textBox.label = "";
        textBox.style.flexGrow = 1;
        textBox.style.maxHeight = new Length(100, LengthUnit.Percent);
        textBox.multiline = true;
        textBox.style.whiteSpace = WhiteSpace.Normal;
        editor.Add(textBox);
            
        container.Add(editor);
        
        VisualElement renderer = new VisualElement();
        renderer.style.display = script.showEditor?DisplayStyle.None:DisplayStyle.Flex;
        
        Label display = new Label();
        display.text = script.uxmlRichText;
        display.style.fontSize = 12;
        display.style.whiteSpace = WhiteSpace.Normal;
        display.style.flexGrow = 1;
        renderer.Add(display);
        
        container.Add(renderer);
        
        Button flipFlop = new Button();
        flipFlop.text = script.showEditor?"📖":"📝";
        flipFlop.style.position = Position.Absolute;
        flipFlop.style.right = 0;
        flipFlop.clickable = new Clickable(() => {
            script.showEditor = !script.showEditor;
            if (script.showEditor) {
                editor.style.display = DisplayStyle.Flex;
                renderer.style.display = DisplayStyle.None;
                flipFlop.text = "📖";
            }
            else {
                editor.style.display = DisplayStyle.None;
                renderer.style.display = DisplayStyle.Flex;
                flipFlop.text = "📝";
                script.RenderUXML();
                display.text = script.uxmlRichText;
            }
        });
        container.Add(flipFlop);

        return container;
    }
}
#endif
}