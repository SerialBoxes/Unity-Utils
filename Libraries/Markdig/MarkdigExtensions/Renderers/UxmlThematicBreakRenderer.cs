
using Markdig.Syntax;
using UnityEngine.TextCore.Text;

namespace Markdig.Renderers.Uxml {


public class UxmlThematicBreakRenderer : UxmlObjectRenderer<ThematicBreakBlock> {
    
    private TextStyleSheet renderStyle;
    
    public UxmlThematicBreakRenderer(TextStyleSheet renderStyle) {
        this.renderStyle = renderStyle;
    }
    
    protected override void Write(UxmlRenderer renderer, ThematicBreakBlock obj) {
        string opener = "", closer = "";
        TextStyle thematicBreak = renderStyle.GetStyle("thematicBreak");
        if (thematicBreak is not null) {
            opener = thematicBreak.styleOpeningDefinition;
            closer = thematicBreak.styleClosingDefinition;
        }

        renderer.WriteLinesBefore(obj);
        renderer.WriteRaw(opener);
        renderer.WriteRaw(closer);
        renderer.WriteLine("");
        renderer.EnsureLine();
    }
}
}