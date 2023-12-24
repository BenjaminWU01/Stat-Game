using System;

public class Character
{
    public Stat Strength;
    public Stat Agility;
    public Stat Intelligence;
    public Stat Health;
    public Stat Mana;
    public Stat Armor;
    public Stat AttackPower;
    public Stat SpellPower;
    public Stat CritChance;
    public Stat CritDamage;
    public Stat Haste;
    public Stat AttackSpeed;
    public Stat BlockChance;
    public Stat BlockAmount;
    public Stat DodgeChance;
    public Stat ParryChance;
    public Stat MovementSpeed;

    public List<Item> EquippedItems;

    public Character()
    {
        Strength = new Stat(10);
        Agility = new Stat(10);
        Intelligence = new Stat(10);
        Health = new Stat(100);
        Mana = new Stat(100);
        Armor = new Stat(0);
        AttackPower = new Stat(0);
        SpellPower = new Stat(0);
        CritChance = new Stat(0);
        CritDamage = new Stat(0);
        Haste = new Stat(0);
        AttackSpeed = new Stat(0);
        BlockChance = new Stat(0);
        BlockAmount = new Stat(0);
        DodgeChance = new Stat(0);
        ParryChance = new Stat(0);
        MovementSpeed = new Stat(0);

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
        }
    }

    public void PrintAllEquippedItems() {
        Console.WriteLine($"Equipped items:");
        foreach(Item item in EquippedItems) {
            Console.WriteLine($"    {item.Name}");
            foreach((string statName, StatModifier modifier) in item.Stats) {
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

}