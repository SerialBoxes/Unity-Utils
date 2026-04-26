using Markdig.Renderers.Html;
using UnityEngine;

using Markdig.Syntax;
using UnityEngine.TextCore.Text;

namespace Markdig.Renderers.Uxml {


public class UxmlListRenderer : UxmlObjectRenderer<ListBlock> {
    
    private TextStyleSheet renderStyle;
    
    public UxmlListRenderer(TextStyleSheet renderStyle) {
        this.renderStyle = renderStyle;
    }
    
    protected override void Write(UxmlRenderer renderer, ListBlock listBlock) {
        TextStyle unorderedStyle = renderStyle.GetStyle("unorderedList");
        TextStyle orderedStyle = renderStyle.GetStyle("orderedList");
        TextStyle itemStyle = renderStyle.GetStyle("listItem");
        
        renderer.EnsureLine();
        renderer.WriteLinesBefore(listBlock);

        int delimiterNumber = 1;
        if (listBlock.OrderedStart is not null) {
            delimiterNumber = int.Parse(listBlock.OrderedStart);
        }

        foreach (var item in listBlock) {
            var listItem = (ListItemBlock)item;
            var previousImplicit = renderer.ImplicitParagraph;
            renderer.ImplicitParagraph = !listBlock.IsLoose;

            if (listBlock.IsOrdered) {
                if (orderedStyle is not null) {
                    renderer.WriteRaw(orderedStyle.styleOpeningDefinition);
                }

                if (listBlock.BulletType != '1') {
                    //apparently this isnt supported?
                    //its just treats them as a big paragraph
                    //put its in the libraries own list renderer?
                    //who knows anymore dood
                    char delimiterLetter = (char)(listBlock.BulletType + (delimiterNumber - 1));//least cursed math
                    renderer.Write("" + delimiterLetter + listBlock.OrderedDelimiter);
                }
                else {
                    renderer.WriteRaw(""+ delimiterNumber + listBlock.OrderedDelimiter);
                    //renderer.Write("" + delimiterNumber + listBlock.OrderedDelimiter);
                }
                if (orderedStyle is not null) {
                    renderer.WriteRaw(orderedStyle.styleClosingDefinition);
                }
            }
            else {
                if (unorderedStyle is not null) {
                    renderer.WriteRaw(unorderedStyle.styleOpeningDefinition);
                    renderer.WriteRaw(unorderedStyle.styleClosingDefinition);
                }
            }

            delimiterNumber++;

            if (itemStyle is not null) {
                renderer.WriteRaw(itemStyle.styleOpeningDefinition);
            }
            renderer.WriteChildren(listItem);
            if (itemStyle is not null) {
                renderer.WriteRaw(itemStyle.styleClosingDefinition);
            }
            
            renderer.ImplicitParagraph = previousImplicit;
            renderer.EnsureLine();
        }
        
        renderer.WriteLine();//lists are always followed by a blank line
    }
}
}