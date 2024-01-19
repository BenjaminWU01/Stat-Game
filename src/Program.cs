class Program
{

    static void Main(string[] args)
    {
        // Get mod data from xml file
        string filePath = "./src/mods.xml";
        string xml = File.ReadAllText(filePath);
        List<Mod> mods = XmlParser.ParseXml(xml);

        // Debugging - print all mods
        foreach (Mod entry in mods)
        {
            Console.WriteLine($"Entry Id: {entry.Id}");
            if (entry.Negative == null)
            {
                Console.WriteLine($"Translation: {entry.Positive?.Text}");
            }
            else
            {
                Console.WriteLine($"Positive Translation: {entry.Positive?.Text}");
                Console.WriteLine($"Negative Translation: {entry.Negative?.Text}");
            }
            Console.WriteLine($"Stats: {string.Join(", ", entry.Stats)}");
            entry.PrintText(42);
            Range range = new Range { Min = -20, Max = -1 };
            entry.PrintText(range);
            Console.WriteLine();
        }
    }
}