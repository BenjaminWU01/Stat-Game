class Program
{

    static void Main(string[] args)
    {
        // Create a new character
        Character character = new Character();

        // Create a new item and add stats to it
        Item sword = new Item("Sword", "10% increased strength " + Environment.NewLine + "+10 to strength", 1, 1, 0, 0, 0);
        sword.AddStat("Strength", new StatModifier(10, StatModType.Flat, sword));
        sword.AddStat("Intelligence", new StatModifier(-5, StatModType.Flat, sword));
        sword.AddStat("Strength", new StatModifier(0.1f, StatModType.PercentAdd, sword));

        // Create another item and add stats to it
        Item multiPotion = new Item("Strength Potion", "10% more strength", 1, 1, 0, 0, 0);
        multiPotion.AddStat("Strength", new StatModifier(0.2f, StatModType.PercentMult, multiPotion));
        multiPotion.AddStat("Intelligence", new StatModifier(-0.1f, StatModType.PercentMult, multiPotion));

        // Add basic attribute scaling
        ScalingStatModifier lifePer10Strength = new ScalingStatModifier(5f, character.Strength, 10, StatModType.Flat, 100, character.Strength);
        character.Life.AddModifier(lifePer10Strength);
        ScalingStatModifier meleePhysDmgPer10Strength = new ScalingStatModifier(0.02f, character.Strength, 10, StatModType.PercentAdd, 200, character.Strength);
        character.MeleePhysDmgPercentAdd.AddModifier(meleePhysDmgPer10Strength);

        ScalingStatModifier accuracyRatingPerDexterity = new ScalingStatModifier(2f, character.Dexterity, 1, StatModType.Flat, 100, character.Dexterity);
        character.AccuracyRating.AddModifier(accuracyRatingPerDexterity);
        ScalingStatModifier evasionRatingPer5Dexterity = new ScalingStatModifier(0.01f, character.Dexterity, 5, StatModType.Flat, 100, character.Intelligence);
        character.EvasionRating.AddModifier(evasionRatingPer5Dexterity);

        ScalingStatModifier manaPer10Intelligence = new ScalingStatModifier(5f, character.Intelligence, 10, StatModType.Flat, 100, character.Intelligence);
        character.Mana.AddModifier(manaPer10Intelligence);
        ScalingStatModifier manaPer10Intelligence = new ScalingStatModifier(5f, character.Intelligence, 10, StatModType.Flat, 100, character.Intelligence);
        character.Mana.AddModifier(manaPer10Intelligence);

        character.PrintOffensiveStats();
        character.PrintDefensiveStats();
        character.Strength.AddModifier(new StatModifier(10, StatModType.Flat, character.Strength));
        character.PrintOffensiveStats();
        character.PrintDefensiveStats();
    }
}