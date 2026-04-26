using System.IO;
using Markdig.Extensions.EmphasisExtras;
using Markdig.Renderers;
using Markdig.Syntax;
using UnityEngine.TextCore.Text;

namespace Markdig {

public static class MarkdownExtension {

    private static TextStyleSheet styleSheet;
    private static UxmlRenderer renderer;
    private static StringWriter writer;
    
    public static string RenderUxml(string rawText, TextStyleSheet renderStyle) {
        if (writer is null) {
            writer = new();
        }
        if (styleSheet is null || styleSheet != renderStyle) {
            styleSheet = renderStyle;
            renderer = new UxmlRenderer(writer, styleSheet);
        }
        if (renderer is null) {
            renderer = new UxmlRenderer(writer, styleSheet);
        }
        
        var pipeline = new MarkdownPipelineBuilder()
            .UseEmphasisExtras(EmphasisExtraOptions.Default)
            .EnableTrackTrivia()
            .Build();
        
        MarkdownDocument doc = Markdown.Parse(rawText, pipeline);
        
        renderer.Render(doc);
        renderer.Writer.Flush();

        string document =  renderer.Writer.ToString() ?? string.Empty;

        StringWriter w = renderer.Writer as StringWriter;
        w.GetStringBuilder().Clear();
        return document;
    }

    public static void Dispose() {
        styleSheet = null;
        renderer = null;
        writer.Dispose();
        writer = null;
    }
}
}
