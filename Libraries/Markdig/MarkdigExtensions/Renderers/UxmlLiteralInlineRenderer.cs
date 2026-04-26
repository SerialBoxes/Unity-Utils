// Copyright (c) Alexandre Mutel. All rights reserved.
// This file is licensed under the BSD-Clause 2 license. 
// See the license.txt file in the project root for more information.

using Markdig.Renderers.Uxml;
using Markdig.Syntax.Inlines;
using UnityEngine;

namespace Markdig.Renderers.Uxml {

/// <summary>
/// A HTML renderer for a <see cref="LiteralInline"/>.
/// </summary>
/// <seealso cref="HtmlObjectRenderer{LiteralInline}" />
public class UxmlLiteralInlineRenderer : UxmlObjectRenderer<LiteralInline> {
    protected override void Write(UxmlRenderer renderer, LiteralInline obj) {
        // if (renderer.EnableHtmlEscape) {
        //     renderer.WriteEscape(ref obj.Content);
        // }
        // else {
        //     renderer.Write(ref obj.Content);
        // }
        
        
        renderer.Write(ref obj.Content);
        //Debug.Log("<noparse>"+obj.Content.AsSpan().ToString()+"</noparse>");
    }
}
}