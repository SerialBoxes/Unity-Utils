using Markdig.Syntax;
using Markdig.Syntax.Inlines;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Markdig.Renderers.Uxml {

#nullable enable
public class UxmlLinkInlineRenderer : UxmlObjectRenderer<LinkInline> {

    private TextStyleSheet renderStyle;

    public UxmlLinkInlineRenderer(TextStyleSheet renderStyle) {
        this.renderStyle = renderStyle;
    }

    protected override void Write(UxmlRenderer renderer, LinkInline obj) {
        TextStyle link = renderStyle.GetStyle("link");
        string opener = "", closer = "";
        if (link is not null) {
            opener = link.styleOpeningDefinition;
            closer = link.styleClosingDefinition;
        }

        renderer.WriteRaw(opener);
        renderer.Write("<a href=\"");
        renderer.WriteRaw(obj.GetDynamicUrl != null ? obj.GetDynamicUrl() ?? obj.Url : obj.Url);
        renderer.WriteRaw("\">");
        renderer.WriteChildren(obj);
        renderer.WriteRaw("</a>");
        renderer.WriteRaw(closer);
    }

}
}