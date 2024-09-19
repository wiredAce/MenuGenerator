using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EditInput : EditorWindow
{
    private const string GREETING =
        "Hello, below this statement you can provide your .xml config. " +
        "If you are confused by this Statement please visit:"
        ;

    private const string README_MD = "https://github.com/wiredAce/README.md";

    [MenuItem("Window/GenerateMenu")]
    public static void Init()
    {
        var window = GetWindow<EditInput>("GenerateMenu");
        window.minSize = new Vector2(400, 300);
        window.maxSize = new Vector2(800, 600);
    }

    private void CreateGUI()
    {
        LoadStyle();

        var readMeMd = new HyperlinkLabel("Documentation", README_MD);

        var greetingLabel = new Label(GREETING);
        greetingLabel.AddToClassList("greeting");

        var filePathInput = new TextField();
        filePathInput.AddToClassList("path-input");

        var buttonLabel = new Label("GENERATE");
        var button = new Button(GenerateAction);
        button.AddToClassList("generate-button");
        button.Add(buttonLabel);

        rootVisualElement.Add(greetingLabel);
        rootVisualElement.Add(readMeMd);
        rootVisualElement.Add(filePathInput);
        rootVisualElement.Add(button);
    }

    private void LoadStyle()
    {
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/MenuGenerator/Editor/EditInput.uss");
        rootVisualElement.styleSheets.Add(styleSheet);
    }

    private void GenerateAction()
    {
        var textfield = rootVisualElement.Q<TextField>();
        Debug.Log(textfield.value);
    }
}