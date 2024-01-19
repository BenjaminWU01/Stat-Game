using System;

public class Character
{

    //experimental
    public Stat stat1436;
    public Stat stat1438;


    public List<Item> EquippedItems;

    public Character()
    {

        //experimental
        stat1436 = new Stat(1436, "base_maximum_life", "{0} to maximum Life");
        stat1438 = new Stat(1438, "maximum_life_+%", "{0}% increased maximum Life");


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
                Stat stat = (Stat)GetType().GetField(statName).GetValue(this);
                // switch (modifier)
                // {
                    // case StatModType.Flat:
                        output = $"{stat.Text.Replace("{0}", Math.Abs(modifier.Value).ToString())}";
                //         output = modifier.Value > 0 ? $"+{Math.Abs(modifier.Value)} to {statName}" : $"-{Math.Abs(modifier.Value)} to {statName}";
                //         break;

                //     case StatModType.PercentAdd:
                //         output = modifier.Value > 0 ? $"{Math.Abs(modifier.Value * 100)}% increased {statName}" : $"{Math.Abs(modifier.Value * 100)}% reduced {statName}";
                //         break;

                //     case StatModType.PercentMult:
                //         output = modifier.Value > 0 ? $"{Math.Abs(modifier.Value * 100)}% more {statName}" : $"{Math.Abs(modifier.Value * 100)}% less {statName}";
                //         break;

                //     default:
                //         output = "Unknown option";
                //         break;
                // }

                System.Console.WriteLine($"        {output}");
            }
        }
        System.Console.WriteLine($"Character has {stat1436.Value * (100 + stat1438.Value) / 100} life");
    }

    public void PrintOffensiveStats() {
        System.Console.WriteLine("Offensive Stats:");
        // System.Console.WriteLine($"    Melee Physical Damage: {MeleePhysDmg.Value}");
        // System.Console.WriteLine($"    Increased Melee Physical Damage: {MeleePhysDmgPercentAdd.Value}%");
    }

    public void PrintDefensiveStats() {
        System.Console.WriteLine("Defensive Stats:");
        // System.Console.WriteLine($"    Life: {Life.Value}");
        // System.Console.WriteLine($"    Mana: {Mana.Value}");
        // System.Console.WriteLine($"    Armour: {Armor.Value}");
        // System.Console.WriteLine($"    Block Chance: {BlockChance.Value}%");
        // System.Console.WriteLine($"    Block Amount: {BlockAmount.Value}");
    }

    public void PrintMiscStats() {

    }

}