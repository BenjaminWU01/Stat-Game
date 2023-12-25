public class Item {

    public string? Name;
    public string? Description;
    public int? ItemLevel;
    public int? RequiredLevel;
    public int? RequiredStrength;
    public int? RequiredAgility;
    public int? RequiredIntelligence;

    public List<(string statName, StatModifier modifier)> Stats;

    public Item(string name, string description, int itemLevel, int requiredLevel, int requiredStrength, int requiredAgility, int requiredIntelligence) {
        Name = name;
        Description = description;
        ItemLevel = itemLevel;
        RequiredLevel = requiredLevel;
        RequiredStrength = requiredStrength;
        RequiredAgility = requiredAgility;
        RequiredIntelligence = requiredIntelligence;
        Stats = new List<(string statName, StatModifier modifier)>();
    }
    
    public Item() {
        Stats = new List<(string statName, StatModifier modifier)>();
    }

    public void AddStat(string statName, StatModifier modifier) {
        Stats.Add((statName, modifier));
    }
}