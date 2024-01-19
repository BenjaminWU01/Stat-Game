public class StatModifier {
    public readonly int Value;
    public readonly int Order;
    public readonly object Source;

    public StatModifier(int value, int order, object source) {
        Value = value;
        Order = order;
        Source = source;
    }

    public StatModifier(int value) : this(value, 100, null) { }
    public StatModifier(int value, int order) : this(value, order, null) { }
    public StatModifier(int value, object source) : this(value, 100, source) { }
}

public class ScalingStatModifier : StatModifier {

    public readonly Stat ScalingStat;
    public readonly int Threshold;

    public ScalingStatModifier(int value, Stat scalingStat, int threshold, int order, object source) : base(value, order, source){
        ScalingStat = scalingStat;
        Threshold = threshold;
    }
}