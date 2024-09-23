using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EditInputWindow : EditorWindow
{
    /// <summary>
    /// Welcome message
    /// </summary>
    private const string GREETING =
        "Hello, below this statement you can provide your .xml config. " +
        "If you are confused by this Statement please visit:";

    /// <summary>
    /// Link to the README.md file
    /// </summary>
    private const string README_MD = "https://github.com/wiredAce/README.md";

    /// <summary>
    /// Checks the given path for validity
    /// </summary>
    private readonly LocalFileValidator validator = new();

    /// <summary>
    /// Initialize the window and set the min and max size
    /// </summary>
    [MenuItem("Window/GenerateMenu")]
    public static void Init()
    {
        var window = GetWindow<EditInputWindow>("GenerateMenu");
        window.minSize = new Vector2(400, 300);
        window.maxSize = new Vector2(800, 600);
    }

    /// <summary>
    /// Build the GUI from the elements
    /// </summary>
    private void CreateGUI()
    {
        LoadStyle();

        rootVisualElement.Add(GenerateGreeting());
        rootVisualElement.Add(new HyperlinkLabel("ReadMe", README_MD));
        rootVisualElement.Add(GenerateFilePathInput());
        rootVisualElement.Add(GenerateGenerateButton());
    }

    /// <summary>
    /// Generate the first label to be displayed in editor window
    /// </summary>
    /// <returns></returns>
    private static Label GenerateGreeting()
    {
        var greetingLabel = new Label(GREETING);
        greetingLabel.AddToClassList("greeting");

        return greetingLabel;
    }

    /// <summary>
    /// Generate the input field for the xml file path
    /// </summary>
    /// <returns></returns>
    private static TextField GenerateFilePathInput()
    {
        var filePathInput = new TextField();
        filePathInput.AddToClassList("path-input");

        return filePathInput;
    }

    /// <summary>
    /// Generate the button to trigger the generation of the menu
    /// </summary>
    /// <returns></returns>
    private Button GenerateGenerateButton()
    {
        var buttonLabel = new Label("GENERATE");
        var button = new Button(GenerateAction);
        button.AddToClassList("generate-button");
        button.Add(buttonLabel);

        return button;
    }

    /// <summary>
    /// Attach the style sheet to the window
    /// </summary>
    private void LoadStyle()
    {
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/MenuGenerator/Editor/EditInputWindow.uss");

        rootVisualElement.styleSheets.Add(styleSheet);
    }

    /// <summary>
    /// Triggers validation and the controller to generate the menu
    /// </summary>
    private void GenerateAction()
    {
        var textfield = rootVisualElement.Q<TextField>();

        if (!validator.IsValid(textfield.value))
        {
            throw new ValidationException(validator.GetMessage());
        }

        Debug.Log("success");
    }
}