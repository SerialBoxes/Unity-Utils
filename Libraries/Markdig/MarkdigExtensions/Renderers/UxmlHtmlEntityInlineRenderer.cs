using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Uxml {


public class UxmlHtmlEntityInlineRenderer : UxmlObjectRenderer<HtmlEntityInline> {
    protected override void Write(UxmlRenderer renderer, HtmlEntityInline obj) {
        renderer.Write(obj.Transcoded);
    }
}
}