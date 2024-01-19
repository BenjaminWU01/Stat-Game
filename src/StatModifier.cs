public enum StatModType {
    Flat = 100,
    PercentAdd = 200,
    PercentMult = 300,
}

public class StatModifier {
    public readonly float Value;
    public readonly StatModType Type;
    public readonly int Order;
    public readonly object Source;

    public StatModifier(float value, StatModType type, int order, object source) {
        Value = value;
        Type = type;
        Order = order;
        Source = source;
    }

    public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }
    public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }
    public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }
}

public class ScalingStatModifier : StatModifier {

    public readonly Stat ScalingStat;
    public readonly float Threshold;

    public ScalingStatModifier(float value, Stat scalingStat, float threshold, StatModType type, int order, object source) : base(value, type, order, source){
        ScalingStat = scalingStat;
        Threshold = threshold;
    }
}