class Program
{

    static void Main(string[] args)
    {
        Character character = new Character();
        Item sword = new Item("Sword", "10% increased strength " + Environment.NewLine + "+10 to strength", 1, 1, 0, 0, 0);
        sword.AddStat("Strength", new StatModifier(10, StatModType.Flat, sword));
        sword.AddStat("Intelligence", new StatModifier(-5, StatModType.Flat, sword));
        sword.AddStat("Strength", new StatModifier(0.1f, StatModType.PercentAdd, sword));
        Item multiPotion = new Item("Strength Potion", "10% more strength", 1, 1, 0, 0, 0);
        multiPotion.AddStat("Strength", new StatModifier(0.2f, StatModType.PercentMult, multiPotion));
        multiPotion.AddStat("Intelligence", new StatModifier(-0.1f, StatModType.PercentMult, multiPotion));

        Console.WriteLine("Your character has " + character.Strength.Value + " Strength");
        character.Equip(sword);
        Console.WriteLine("Your character has " + character.Strength.Value + " Strength");
        character.Equip(multiPotion);
        character.PrintAllEquippedItems();
        Console.WriteLine("Your character has " + character.Strength.Value + " Strength");
        Console.WriteLine("Your character has " + character.Intelligence.Value + " Intelligence");
        character.Unequip(sword);
        character.Unequip(multiPotion);
        Console.WriteLine("Your character has " + character.Strength.Value + " Strength");
    }
}