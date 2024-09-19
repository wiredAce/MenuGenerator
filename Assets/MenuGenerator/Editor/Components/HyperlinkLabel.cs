using UnityEngine;
using UnityEngine.UIElements;

public class HyperlinkLabel : Label
{
    public HyperlinkLabel(string text, string url) : base(text)
    {
        AddToClassList("hyperlink");
        RegisterCallback<ClickEvent>(evt => Application.OpenURL(url));
    }
}