using Markdig.Syntax.Inlines;
using UnityEngine;

namespace Markdig.Renderers.Uxml {

public class UxmlDelimiterInlineRenderer : UxmlObjectRenderer<DelimiterInline> {
    protected override void Write(UxmlRenderer renderer, DelimiterInline obj) {
        //idk if i need this one but it can't hurt to have it i guess???
        renderer.WriteEscape(obj.ToLiteral());
        renderer.WriteChildren(obj);
    }
}
}