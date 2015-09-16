using System.Collections.Generic;

namespace Dev4s.WebClient
{
    public static class Extensions
    {
        public static void AddMany(this HashSet<string> existingHashSet, IEnumerable<string> hashSetToAdd)
        {
            foreach (var item in hashSetToAdd)
            {
                existingHashSet.Add(item);
            }
        }
    }
}
/// <summary>
/// Replacing the number place (ex. {0}) with string, char, int, uint.
/// </summary>
/// <param name="text">Text we want to change</param>
/// <param name="position">Number to replace</param>
/// <param name="newObj">String, Char, Int, UInt</param>
/// <returns>Replaced text</returns>
public static string Replace(this string text, int position, object newObj)
{
    ReplaceCheckings(text, newObj);

    var tempPosition = position.ToString(CultureInfo.InvariantCulture);
    var allPlaces = new bool[tempPosition.Length];

    // ReSharper disable TooWideLocalVariableScope
    int i, j, k;
    // ReSharper restore TooWideLocalVariableScope

    // Searching for first occurence of '{' char and looping through all characters
    for (i = text.IndexOf('{'); i < text.Length; i++)
    {
        // Searching for number in text
        for (j = 0; j < tempPosition.Length; j++)
        {
            // i + j = for first search it will allways be '{'
            // that's why it has + 1 in it ;)
            k = i + j + 1;

            // IndexOutOfException repaired
            if (k >= text.Length)
            {
                break;
            }

            // Checks if all the numbers in position are the same as in text
            if (text[k] == tempPosition[j])
            {
                allPlaces[j] = true;
            }
        }

        // Position of last '}' char
        // Without + 1 it will show last number of position
        k = i + tempPosition.Length + 1;

        // IndexOutOfException repaired
        if (k >= text.Length)
        {
            break;
        }

        // .All(x => x) checks if every bool in array is true
        if (allPlaces.All(x => x) && text[k] == '}')
        {
            // It takes all chars from string without the last '}'
            k = i + tempPosition.Length + 2;
            return text.Substring(0, i) + newObj + text.Substring(k);
        }
        allPlaces = new bool[tempPosition.Length];
    }

    return text;
}

// ReSharper disable UnusedParameter.Local
private static void ReplaceCheckings(string text, object newObj)
// ReSharper restore UnusedParameter.Local
{
    if (string.IsNullOrEmpty(text))
    {
        throw new ArgumentException("The 'text' argument should not be empty.");
    }

    if (newObj == null)
    {
        // ReSharper disable NotResolvedInText
        throw new ArgumentNullException("The 'newObj' argument should not be null.");
        // ReSharper restore NotResolvedInText
    }

    var typeOfNewObj = newObj.GetType().ToString();
    if (typeOfNewObj != "System.String" &&
        typeOfNewObj != "System.Char" &&
        typeOfNewObj != "System.Int32" &&
        typeOfNewObj != "System.UInt32")
    {
        throw new ArgumentException("The 'newObj' argument should be (u)int, char, string, etc.");
    }

    if (text.IndexOf('{') == -1 || text.IndexOf('}') == -1)
    {
        throw new ArgumentException("The 'text' argument should have at least 1 number place (ex. 'text/{0}').");
    }
}