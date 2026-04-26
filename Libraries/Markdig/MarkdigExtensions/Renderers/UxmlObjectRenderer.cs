using Markdig.Syntax;

namespace Markdig.Renderers.Uxml {

/// <summary>
/// A base class for HTML rendering <see cref="Block"/> and <see cref="Syntax.Inlines.Inline"/> Markdown objects.
/// </summary>
/// <typeparam name="TObject">The type of the object.</typeparam>
/// <seealso cref="IMarkdownObjectRenderer" />
public abstract class UxmlObjectRenderer<TObject> : MarkdownObjectRenderer<UxmlRenderer, TObject>
    where TObject : MarkdownObject {
}
}
