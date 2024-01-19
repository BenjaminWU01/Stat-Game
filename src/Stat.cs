using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

public class Stat
{

    public int Id;
    public string Name;
    public string Text;

    public virtual int Value
    {
        get
        {
            if (isDirty)
            {
                _value = CalculateFinalValue();
            }
            return _value;
        }
    }

    protected bool isDirty = true;
    protected int _value;
    
    protected readonly List<StatModifier> statModifiers;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;

    public Stat(int id, string name, string text)
    {
        Id = id;
        Name = name;
        Text = text;
    
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }

    public virtual void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }


    public virtual bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }

    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
        {
            return -1;
        }
        else if (a.Order > b.Order)
        {
            return 1;
        }
        return 0;
    }

    protected virtual int CalculateFinalValue()
    {
        int finalValue = 0;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if (mod is ScalingStatModifier) 
            {
                finalValue += ((ScalingStatModifier)mod).ScalingStat.Value / ((ScalingStatModifier)mod).Threshold * ((ScalingStatModifier)mod).Value;
            }
            else if (mod is not ScalingStatModifier)
            {
                finalValue += mod.Value;
            }
            

            if (mod is ScalingStatModifier)
            {
                isDirty = true;
            } else {
                isDirty = false;
            }
        }
        return finalValue;
    }
}