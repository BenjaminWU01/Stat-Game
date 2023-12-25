# Stat Game

 > the foundation for a console stat game

A C# program that contains user defined stats, and modifiers to edit them

```sh
Equipped items:
    Sword
        +10 to Strength
        -5 to Intelligence
        10% increased Strength
```

## Stat

stats are defined on the character, and can be modified with StatModifiers.

### Add modifier

```cs
public virtual void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }
```

### Remove modifer

```cs
public virtual bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }
```

## StatModifier
