using Markdig.Renderers.Uxml;
using Markdig.Syntax;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Markdig.Renderers.Uxml {


public class UxmlParagraphRenderer : UxmlObjectRenderer<ParagraphBlock> {

    private TextStyleSheet renderStyle;

    public UxmlParagraphRenderer(TextStyleSheet renderStyle) {
        this.renderStyle = renderStyle;
    }
    
    protected override void Write(UxmlRenderer renderer, ParagraphBlock obj) {
        
        string opener = "";
        string closer = "";
        
        TextStyle style = null;
        if (!renderer.ImplicitParagraph) {
            style = renderStyle.GetStyle("paragraph");
        }
        else {
            style = renderStyle.GetStyle("implicitText");
        }

        if (style is not null) {
            opener = style.styleOpeningDefinition;
            closer = style.styleClosingDefinition;
        }

        //implicit paragraph means that this is an embedded paragraph in a list or something
        //and it should not get paragraph formatting. how elegant -_-
        if (!renderer.ImplicitParagraph) {
            renderer.WriteLinesBefore(obj);
        }

        renderer.WriteRaw(opener);
        renderer.WriteLeafInline(obj);
        renderer.WriteRaw(closer);

        if (!renderer.ImplicitParagraph) {
            renderer.EnsureLine();
            renderer.WriteLine();//paragraphs are always followed by a blank space
        }
        
    }
}
}
