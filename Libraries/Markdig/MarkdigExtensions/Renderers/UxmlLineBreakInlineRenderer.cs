using Markdig.Renderers.Html;
using UnityEngine;

using Markdig.Syntax.Inlines;

namespace Markdig.Renderers.Uxml {

/// <summary>
/// A HTML renderer for a <see cref="LineBreakInline"/>.
/// </summary>
/// <seealso cref="HtmlObjectRenderer{TObject}" />
public class UxmlLineBreakInlineRenderer : UxmlObjectRenderer<LineBreakInline> {
    

    
    public UxmlLineBreakInlineRenderer() {

    }
    
    /// <summary>
    /// Gets or sets a value indicating whether to render this softline break as a HTML hardline break tag (&lt;br /&gt;)
    /// </summary>
    public bool RenderAsHardlineBreak { get; set; }

    protected override void Write(UxmlRenderer renderer, LineBreakInline obj) {
        if (renderer.IsLastInContainer) return;

        renderer.EnsureLine();
    }
}
}