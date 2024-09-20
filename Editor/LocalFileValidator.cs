using System.IO;
using System.Text.RegularExpressions;

public class LocalFileValidator
{
    private const string FILE_DOES_NOT_EXIST = "The given string could not be resolved to a file";
    private const string ACCESS_VIOLATION = "The given string was a file, but could not be opened. See IO Exception:\n";
    private const string WRONG_FILETYPE = "The given string does not end on '.xml'";

    /// <summary>
    /// Occured error message. Can only be one at a time.
    /// </summary>
    private string message;

    /// <summary>
    /// Returns true if the given string is allowed to be passed into the GeneratorController
    /// </summary>
    public bool IsValid(string value)
    {
        return !FileExists(value) || !IsXmlFile(value) || !CanBeOpened(value);
    }

    /// <summary>
    /// Checks if the file exists.
    /// </summary>
    /// <param name="filePath">The file path to check.</param>
    /// <returns>True if the file exists, otherwise false.</returns>
    private bool FileExists(string filePath)
    {
        if (!File.Exists(filePath))
        {
            message = FILE_DOES_NOT_EXIST;
            return false;
        }

        return true;
    }

    /// <summary>
    /// Checks if the file is an XML file.
    /// </summary>
    private bool IsXmlFile(string filePath)
    {
        if (Regex.IsMatch(filePath, ".xml$")) return true;

        message = WRONG_FILETYPE;
        return false;

    }

    /// <summary>
    /// Checks if the file can be opened.
    /// </summary>
    private bool CanBeOpened(string filePath)
    {
        try
        {
            using (new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
            }
        }
        catch (IOException e)
        {
            message = ACCESS_VIOLATION + e.Message;
            return false;
        }

        return true;
    }

    /// <summary>
    /// Returns the encountered error
    /// </summary>
    /// <returns></returns>
    public string GetMessage()
    {
        return message ?? "Validator Error: No error message produced";
    }
}