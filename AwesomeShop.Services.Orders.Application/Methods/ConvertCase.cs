using System.Text;

namespace AwesomeShop.Services.Orders.Application.Methods;

public static class ConvertCase
{
    public static string ToDashCase(this string text)
    {
        if (text == null)
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (text.Length < 2)
        {
            return text;
        }

        var stringBuilder = new StringBuilder();
        stringBuilder.Append(char.ToLowerInvariant(text[0]));
        for (int i = 1; i < text.Length; ++i)
        {
            char c = text[i];
            if (char.IsUpper(c))
            {
                stringBuilder.Append('-');
                stringBuilder.Append(char.ToLowerInvariant(c));
            }
            else
            {
                stringBuilder.Append(c);
            }
        }

        Console.WriteLine($"ToDashCase: {stringBuilder.ToString()}");
        return stringBuilder.ToString();
    }
}