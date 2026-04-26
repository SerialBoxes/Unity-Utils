using Markdig.Syntax.Inlines;
using System.Diagnostics;
using Markdig.Renderers.Uxml;
using UnityEngine.TextCore.Text;

namespace Markdig.Renderers.Uxml {

public class UxmlEmphasisInlineRenderer : UxmlObjectRenderer<EmphasisInline> {

    private TextStyleSheet renderStyle;
    private string boldOpen = "";
    private string boldClose = "";
    private string italicsOpen = "";
    private string italicsClose = "";
    private string strikethroughOpen = "";
    private string strikethroughClose = "";
    private string subscriptOpen = "";
    private string subscriptClose = "";
    private string superscriptOpen = "";
    private string superscriptClose = "";
    private string insertedOpen = "";
    private string insertedClose = "";
    private string markedOpen = "";
    private string markedClose = "";

    public UxmlEmphasisInlineRenderer(TextStyleSheet renderStyle) {
        this.renderStyle = renderStyle;
    }

    protected override void Write(UxmlRenderer renderer, EmphasisInline obj) {
        GetStrings();
        
        char delimiter = obj.DelimiterChar;
        int count = obj.DelimiterCount;

        string opener = "", closer = "";
        
        //special rules for OG emphasis chars
        if (delimiter == '*' || delimiter == '_') {
            opener = count switch {
                1 => boldOpen,
                2 => italicsOpen,
                3 => boldOpen+italicsOpen,
                _ => ""
            };
            closer = count switch {
                1 => boldClose,
                2 => italicsClose,
                3 => boldClose+italicsClose,
                _ => ""
            };
        }
        else {
            if (count == 1) {
                opener = delimiter switch {
                    '~' => subscriptOpen,
                    '^' => superscriptOpen,
                    _ => ""
                };
                closer = delimiter switch {
                    '~' => subscriptClose,
                    '^' => superscriptClose,
                    _ => ""
                };
            }
            else {
                opener = delimiter switch {
                    '~' => strikethroughOpen,
                    '+' => insertedOpen,
                    '=' => markedOpen,
                    _ => ""
                };
                closer = delimiter switch {
                    '~' => strikethroughClose,
                    '+' => insertedClose,
                    '=' => markedClose,
                    _ => ""
                };
            }
        }


        renderer.WriteRaw(opener);

        renderer.WriteChildren(obj);
        
        renderer.WriteRaw(closer);

    }

    void GetStrings() {
        TextStyle bold = renderStyle.GetStyle("bold");
        if (bold is not null) {
            boldOpen = bold.styleOpeningDefinition;
            boldClose = bold.styleClosingDefinition;
        }
        TextStyle italics = renderStyle.GetStyle("italics");
        if (italics is not null) {
            italicsOpen = italics.styleOpeningDefinition;
            italicsClose = italics.styleClosingDefinition;
        }
        TextStyle strikethrough = renderStyle.GetStyle("strikethrough");
        if (strikethrough is not null) {
            strikethroughOpen = strikethrough.styleOpeningDefinition;
            strikethroughClose = strikethrough.styleClosingDefinition;
        }
        TextStyle subscript = renderStyle.GetStyle("subscript");
        if (subscript is not null) {
            subscriptOpen = subscript.styleOpeningDefinition;
            subscriptClose = subscript.styleClosingDefinition;
        }
        TextStyle superscript = renderStyle.GetStyle("superscript");
        if (superscript is not null) {
            superscriptOpen = superscript.styleOpeningDefinition;
            superscriptClose = superscript.styleClosingDefinition;
        }
        TextStyle inserted = renderStyle.GetStyle("inserted");
        if (inserted is not null) {
            insertedOpen = inserted.styleOpeningDefinition;
            insertedClose = inserted.styleClosingDefinition;
        }
        TextStyle marked = renderStyle.GetStyle("marked");
        if (marked is not null) {
            markedOpen = marked.styleOpeningDefinition;
            markedClose = marked.styleClosingDefinition;
        }
    }
}
}