using UnityEngine;
using UnityEngine.UIElements;

public class HyperlinkLabel : Label
{
    /// <summary>
    /// Generate a clickable label that opens a URL
    /// </summary>
    public HyperlinkLabel(string text, string url) : base(text)
    {
        AddToClassList("hyperlink");
        RegisterCallback<ClickEvent>(evt => Application.OpenURL(url));
    }
}