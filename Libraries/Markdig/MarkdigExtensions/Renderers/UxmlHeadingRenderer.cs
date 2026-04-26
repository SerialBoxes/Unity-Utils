using Markdig.Renderers.Html;
using Markdig.Syntax;
using UnityEngine.TextCore.Text;

namespace Markdig.Renderers.Uxml {

public class UxmlHeadingRenderer : UxmlObjectRenderer<HeadingBlock> {
    
    private TextStyleSheet renderStyle;
    private string[] headerNames;

    public UxmlHeadingRenderer(TextStyleSheet renderStyle) {
        this.renderStyle = renderStyle;
        headerNames = new string[]{
            "heading1",
            "heading2",
            "heading3",
            "heading4",
            "heading5",
            "heading6"
        };
    }
    

    protected override void Write(UxmlRenderer renderer, HeadingBlock obj) {

        string opener = "";
        string closer = "";

        TextStyle style = renderStyle.GetStyle(headerNames[obj.Level - 1]);
        if (style is not null) {
            opener = style.styleOpeningDefinition;
            closer = style.styleClosingDefinition;
        }

        renderer.WriteLinesBefore(obj);

        renderer.WriteRaw(opener);
        
        renderer.WriteLeafInline(obj);

        renderer.WriteRaw(closer);

        renderer.EnsureLine();
        //renderer.WriteLine();
    }
}
}