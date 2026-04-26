using Markdig.Renderers.Uxml;
using Markdig.Syntax;
using UnityEngine;

namespace Markdig.Renderers.Uxml {

public class UxmlHtmlBlockRenderer : UxmlObjectRenderer<HtmlBlock> {
    protected override void Write(UxmlRenderer renderer, HtmlBlock obj) {
        Debug.Log("Block");
        renderer.WriteLeafRawLines(obj, true, false);
    }
}
}