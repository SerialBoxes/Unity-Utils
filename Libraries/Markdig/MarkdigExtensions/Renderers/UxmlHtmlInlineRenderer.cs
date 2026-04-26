using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Uxml {


public class UxmlHtmlInlineRenderer : UxmlObjectRenderer<HtmlInline> {
    protected override void Write(UxmlRenderer renderer, HtmlInline obj) {
        renderer.Write(obj.Tag);
    }
}
}