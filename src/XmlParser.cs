using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text.RegularExpressions;

public class Range
{
    public int Min { get; set; }
    public int Max { get; set; }
}

public class Limit
{
    public Range Range { get; set; }
}

public class Translation
{
    public Limit Limit { get; set; }
    public string Text { get; set; }
}

public class Mod
{
    public int Id { get; set; }
    public Translation Positive { get; set; }
    public Translation Negative { get; set; }
    public List<string> Stats { get; set; }

    public void PrintText(int value)
    {
        string formattedText = Positive?.Text ?? Negative?.Text;
        if (value < 0 && Negative?.Text != null)
        {
            formattedText = Negative.Text;
        }

        if (formattedText != null)
        {
            // Handle the {0} and {0:+d} formats
            formattedText = Regex.Replace(formattedText, @"\{0(?::\+d)?\}", match =>
            {
                if (match.Value.Contains(":+d"))
                {
                    return (value >= 0 ? "+" : "") + value.ToString("0");
                }
                else
                {
                    return Math.Abs(value).ToString();
                }
            });

            // Apply other formatting if needed
            // ...

            Console.WriteLine(formattedText);
        }
    }

    public void PrintText(Range range)
    {
        int rolledValue = RollValueInRange(range.Min, range.Max);
        PrintText(rolledValue);
    }

    private int RollValueInRange(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max + 1); // "+1" to include the upper bound
    }
}

public static class XmlParser
{
    public static List<Mod> ParseXml(string xml)
    {
        XDocument doc = XDocument.Parse(xml);

        return doc.Root
            .Elements("entry")
            .Select(entryElement => new Mod
            {
                Id = int.Parse(entryElement.Attribute("id").Value),
                Positive = ParseTranslation(entryElement.Element("positive")),
                Negative = ParseTranslation(entryElement.Element("negative")),
                Stats = entryElement.Element("stats").Elements("stat").Select(statElement => statElement.Value).ToList()
            })
            .ToList();
    }

    private static Translation ParseTranslation(XElement translationElement)
    {
        if (translationElement == null)
        {
            return null;
        }

        return new Translation
        {
            Limit = ParseLimit(translationElement.Element("limit")),
            Text = translationElement.Element("text").Value
        };
    }

    private static Limit ParseLimit(XElement limitElement)
    {
        if (limitElement == null)
        {
            return null;
        }

        return new Limit
        {
            Range = ParseRange(limitElement.Element("range"))
        };
    }

    private static Range ParseRange(XElement rangeElement)
    {
        if (rangeElement == null)
        {
            return null;
        }

        int min;
        int max;

        XAttribute minAttribute = rangeElement.Attribute("min");
        XAttribute maxAttribute = rangeElement.Attribute("max");

        if (minAttribute != null && minAttribute.Value == "#")
        {
            min = int.MinValue;
        }
        else
        {
            min = minAttribute != null ? int.Parse(minAttribute.Value) : 0; // You can choose a default value if needed
        }

        if (maxAttribute != null && maxAttribute.Value == "#")
        {
            max = int.MaxValue;
        }
        else
        {
            max = maxAttribute != null ? int.Parse(maxAttribute.Value) : 0; // You can choose a default value if needed
        }

        return new Range
        {
            Min = min,
            Max = max
        };
    }
}