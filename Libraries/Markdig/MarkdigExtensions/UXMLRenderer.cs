// Copyright (c) Alexandre Mutel. All rights reserved.
// This file is licensed under the BSD-Clause 2 license. 
// See the license.txt file in the project root for more information.

using System.Buffers;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime;
using System.Text;
using System;

using Markdig.Helpers;
using Markdig.Renderers.Html;
using Markdig.Renderers.Uxml;
using Markdig.Syntax;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Markdig.Renderers {

/// <summary>
/// Default HTML renderer for a Markdown <see cref="MarkdownDocument"/> object.
/// </summary>
/// <seealso cref="TextRendererBase{HtmlRenderer}" />
#nullable enable
public class UxmlRenderer : TextRendererBase<UxmlRenderer> {

    /// <summary>
    /// Initializes a new instance of the <see cref="UxmlRenderer"/> class.
    /// </summary>
    /// <param name="writer">The writer.</param>
    public UxmlRenderer(TextWriter writer, TextStyleSheet renderStyle) : base(writer) {
        // Default block renderers
        // ObjectRenderers.Add(new CodeBlockRenderer());
        // ObjectRenderers.Add(new ListRenderer());
        // ObjectRenderers.Add(new HeadingRenderer());
        // ObjectRenderers.Add(new HtmlBlockRenderer());
        // ObjectRenderers.Add(new ParagraphRenderer());
        // ObjectRenderers.Add(new QuoteBlockRenderer());
        // ObjectRenderers.Add(new ThematicBreakRenderer());
        //
        // // Default inline renderers
        // ObjectRenderers.Add(new AutolinkInlineRenderer());
        // ObjectRenderers.Add(new CodeInlineRenderer());
        // ObjectRenderers.Add(new DelimiterInlineRenderer());
        // ObjectRenderers.Add(new EmphasisInlineRenderer());
        // ObjectRenderers.Add(new LineBreakInlineRenderer());
        // ObjectRenderers.Add(new HtmlInlineRenderer());
        // ObjectRenderers.Add(new HtmlEntityInlineRenderer());
        // ObjectRenderers.Add(new LinkInlineRenderer());
        // ObjectRenderers.Add(new LiteralInlineRenderer());
        
        //stuff I want:
        //quote block?
        //code block & inline???
        ObjectRenderers.Add(new UxmlHeadingRenderer(renderStyle));
        ObjectRenderers.Add(new UxmlParagraphRenderer(renderStyle));
        ObjectRenderers.Add(new UxmlListRenderer(renderStyle));
        ObjectRenderers.Add(new UxmlThematicBreakRenderer(renderStyle));
        
        ObjectRenderers.Add(new UxmlLiteralInlineRenderer());
        ObjectRenderers.Add(new UxmlLineBreakInlineRenderer());
        ObjectRenderers.Add(new UxmlDelimiterInlineRenderer());
        ObjectRenderers.Add(new UxmlEmphasisInlineRenderer(renderStyle));
        ObjectRenderers.Add(new UxmlLinkInlineRenderer(renderStyle));
        
        //this just makes rich text tags already in the document work
        ObjectRenderers.Add(new UxmlHtmlBlockRenderer());
        ObjectRenderers.Add(new UxmlHtmlInlineRenderer());
        ObjectRenderers.Add(new UxmlHtmlEntityInlineRenderer());

        EnableHtmlForBlock = true;
        EnableHtmlForInline = true;
        EnableHtmlEscape = false;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to output HTML tags when rendering. See remarks.
    /// </summary>
    /// <remarks>
    /// This is used by some renderers to disable HTML tags when rendering some inline elements (for image links).
    /// </remarks>
    public bool EnableHtmlForInline { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to output HTML tags when rendering. See remarks.
    /// </summary>
    /// <remarks>
    /// This is used by some renderers to disable HTML tags when rendering some block elements (for image links).
    /// </remarks>
    public bool EnableHtmlForBlock { get; set; }

    public bool EnableHtmlEscape { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to use implicit paragraph (optional &lt;p&gt;)
    /// </summary>
    public bool ImplicitParagraph { get; set; }

    public bool UseNonAsciiNoEscape { get; set; }

    /// <summary>
    /// Gets a value to use as the base url for all relative links
    /// </summary>
    public Uri? BaseUrl { get; set; }

    /// <summary>
    /// Allows links to be rewritten
    /// </summary>
    public Func<string, string>? LinkRewriter { get; set; }

    /// <summary>
    /// Writes the content escaped for HTML.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <returns>This instance</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UxmlRenderer WriteEscape(string? content) {
        WriteEscape(content.AsSpan());
        return this;
    }

    /// <summary>
    /// Writes the content escaped for HTML.
    /// </summary>
    /// <param name="slice">The slice.</param>
    /// <param name="softEscape">Only escape &lt; and &amp;</param>
    /// <returns>This instance</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UxmlRenderer WriteEscape(ref StringSlice slice, bool softEscape = false) {
        WriteEscape(slice.AsSpan(), softEscape);
        return this;
    }

    /// <summary>
    /// Writes the content escaped for HTML.
    /// </summary>
    /// <param name="slice">The slice.</param>
    /// <param name="softEscape">Only escape &lt; and &amp;</param>
    /// <returns>This instance</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UxmlRenderer WriteEscape(StringSlice slice, bool softEscape = false) {
        WriteEscape(slice.AsSpan(), softEscape);
        return this;
    }

    /// <summary>
    /// Writes the content escaped for HTML.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="offset">The offset.</param>
    /// <param name="length">The length.</param>
    /// <param name="softEscape">Only escape &lt; and &amp;</param>
    /// <returns>This instance</returns>
    public UxmlRenderer WriteEscape(string content, int offset, int length, bool softEscape = false) {
        WriteEscape(content.AsSpan(offset, length), softEscape);
        return this;
    }


    //private static readonly SearchValues<char> s_escapedChars = SearchValues.Create("<>&\"");

    /// <summary>
    /// Writes the content escaped for HTML.
    /// </summary>
    /// <param name="content">The content.</param>
    /// <param name="softEscape">Only escape &lt; and &amp;</param>
    public void WriteEscape(ReadOnlySpan<char> content, bool softEscape = false) {
        if (!content.IsEmpty) {
            //WriteIndent();

            while (true) {
                int indexOfCharToEscape = softEscape
                    ? content.IndexOfAny('<', '&')
                    : content.IndexOfAny('<','>','&') | content.IndexOfAny('/','\\','\"');

                if ((uint)indexOfCharToEscape >= (uint)content.Length) {
                    WriteRaw(content);
                    return;
                }

                WriteRaw(content.Slice(0, indexOfCharToEscape));

                WriteRaw(content[indexOfCharToEscape] switch {
                    '<' => "<",
                    '>' => ">",
                    '&' => "&",
                    '/' => "\\/",
                    '\\' => "\\"+"\\",
                    '\"' => "\\"+"\"",
                    _ => "",
                });

                content = content.Slice(indexOfCharToEscape + 1);
            }
        }
    }

    /// <summary>
    /// Writes the attached <see cref="HtmlAttributes"/> on the specified <see cref="MarkdownObject"/>.
    /// </summary>
    /// <param name="markdownObject">The object.</param>
    /// <returns></returns>
    public UxmlRenderer WriteAttributes(MarkdownObject markdownObject) {
        //if (markdownObject is null) ThrowHelper.ArgumentNullException_markdownObject();
        return WriteAttributes(markdownObject.TryGetAttributes());
    }

    /// <summary>
    /// Writes the specified <see cref="HtmlAttributes"/>.
    /// </summary>
    /// <param name="attributes">The attributes to render.</param>
    /// <param name="classFilter">A class filter used to transform a class into another class at writing time</param>
    /// <returns>This instance</returns>
    public UxmlRenderer WriteAttributes(HtmlAttributes? attributes, Func<string, string>? classFilter = null) {
        if (attributes is null) {
            return this;
        }

        if (attributes.Id != null) {
            Write(" id=\"");
            WriteEscape(attributes.Id);
            WriteRaw('"');
        }

        if (attributes.Classes is { Count: > 0 }) {
            Write(" class=\"");
            for (int i = 0; i < attributes.Classes.Count; i++) {
                var cssClass = attributes.Classes[i];
                if (i > 0) {
                    WriteRaw(' ');
                }

                WriteEscape(classFilter != null ? classFilter(cssClass) : cssClass);
            }

            WriteRaw('"');
        }

        if (attributes.Properties is { Count: > 0 }) {
            foreach (var property in attributes.Properties) {
                Write(' ');
                WriteRaw(property.Key);
                WriteRaw("=\"");
                WriteEscape(property.Value ?? "");
                WriteRaw('"');
            }
        }

        return this;
    }

    public UxmlRenderer WriteLinesBefore(Block obj) {
        if (obj.LinesBefore is not null) {
            int lines = obj.LinesBefore.Count;
            for (int i = 0; i < lines; i++) {
                WriteLine();
            }
        }

        return this;
    }

    /// <summary>
    /// Writes the lines of a <see cref="LeafBlock"/>
    /// </summary>
    /// <param name="leafBlock">The leaf block.</param>
    /// <param name="writeEndOfLines">if set to <c>true</c> write end of lines.</param>
    /// <param name="escape">if set to <c>true</c> escape the content for HTML</param>
    /// <param name="softEscape">Only escape &lt; and &amp;</param>
    /// <returns>This instance</returns>
    public UxmlRenderer WriteLeafRawLines(LeafBlock leafBlock, bool writeEndOfLines, bool escape,
        bool softEscape = false) {
        //if (leafBlock is null) ThrowHelper.ArgumentNullException_leafBlock();

        var slices = leafBlock.Lines.Lines;
        if (slices is not null) {
            for (int i = 0; i < slices.Length; i++) {
                ref StringSlice slice = ref slices[i].Slice;
                if (slice.Text is null) {
                    break;
                }

                if (!writeEndOfLines && i > 0) {
                    WriteLine();
                }

                ReadOnlySpan<char> span = slice.AsSpan();
                if (escape) {
                    WriteEscape(span, softEscape);
                }
                else {
                    Write(span);
                }

                if (writeEndOfLines) {
                    WriteLine();
                }
            }
        }

        return this;
    }
    //internal methods I yoinked from the source code on github so stuff would work
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteRaw(char content) => Writer.Write(content);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteRaw(string? content) => Writer.Write(content);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void WriteRaw(ReadOnlySpan<char> content) {
        Writer.Write(content);
    }
}
}