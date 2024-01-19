using System;

public class Character
{
    public Stat Strength;
    public Stat Dexterity;
    public Stat Intelligence;
    public Stat Life;
    public Stat Mana;
    public Stat Armor;
    public Stat BlockChance;
    public Stat BlockAmount;
    public Stat MeleePhysDmg;
    public Stat MeleePhysDmgPercentAdd;
    public Stat AccuracyRating;
    public Stat EvasionRating;

    public List<Item> EquippedItems;

    public Character()
    {
        Strength = new Stat(10f);
        Dexterity = new Stat(10f);
        Intelligence = new Stat(10f);
        Life = new Stat(100f);
        Mana = new Stat(100f);
        Armor = new Stat(0f);
        BlockChance = new Stat(0);
        BlockAmount = new Stat(0);
        MeleePhysDmg = new Stat(0f);
        MeleePhysDmgPercentAdd = new Stat(100f);
        AccuracyRating = new Stat(0f);
        EvasionRating = new Stat(0f);

        EquippedItems = new List<Item>();
    }

    public void Equip(Item item)
    {
        if (!EquippedItems.Contains(item))
        {
            EquippedItems.Add(item);
            foreach ((string statName, StatModifier modifier) in item.Stats)
            {
                var statField = this.GetType().GetField(statName);
                if (statField != null)
                {
                    Stat stat = (Stat)statField.GetValue(this);
                    stat.AddModifier(modifier);
                }
            }
            System.Console.WriteLine($"Your character equipped {item.Name}");
        }
    }

    public void Unequip(Item item)
    {
        if (EquippedItems.Contains(item))
        {
            EquippedItems.Remove(item);
            foreach ((string statName, StatModifier modifier) in item.Stats)
            {
                var statField = this.GetType().GetField(statName);
                if (statField != null)
                {
                    Stat stat = (Stat)statField.GetValue(this);
                    stat.RemoveAllModifiersFromSource(item);
                }
            }
            System.Console.WriteLine($"Your character unequipped {item.Name}");
        }
    }

    public void PrintAllEquippedItems()
    {
        Console.WriteLine($"Equipped items:");
        foreach (Item item in EquippedItems)
        {
            Console.WriteLine($"    {item.Name}");
            foreach ((string statName, StatModifier modifier) in item.Stats)
            {
                string output;

                switch (modifier.Type)
                {
                    case StatModType.Flat:
                        output = modifier.Value > 0 ? $"+{Math.Abs(modifier.Value)} to {statName}" : $"-{Math.Abs(modifier.Value)} to {statName}";
                        break;

                    case StatModType.PercentAdd:
                        output = modifier.Value > 0 ? $"{Math.Abs(modifier.Value * 100)}% increased {statName}" : $"{Math.Abs(modifier.Value * 100)}% reduced {statName}";
                        break;

                    case StatModType.PercentMult:
                        output = modifier.Value > 0 ? $"{Math.Abs(modifier.Value * 100)}% more {statName}" : $"{Math.Abs(modifier.Value * 100)}% less {statName}";
                        break;

                    default:
                        output = "Unknown option";
                        break;
                }

                System.Console.WriteLine($"        {output}");
            }
        }
    }

    public void PrintOffensiveStats() {
        System.Console.WriteLine("Offensive Stats:");
        System.Console.WriteLine($"    Melee Physical Damage: {MeleePhysDmg.Value}");
        System.Console.WriteLine($"    Increased Melee Physical Damage: {MeleePhysDmgPercentAdd.Value}%");
    }

    public void PrintDefensiveStats() {
        System.Console.WriteLine("Defensive Stats:");
        System.Console.WriteLine($"    Life: {Life.Value}");
        System.Console.WriteLine($"    Mana: {Mana.Value}");
        System.Console.WriteLine($"    Armour: {Armor.Value}");
        System.Console.WriteLine($"    Block Chance: {BlockChance.Value}%");
        System.Console.WriteLine($"    Block Amount: {BlockAmount.Value}");
    }

    public void PrintMiscStats() {

    }

}