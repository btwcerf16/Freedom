//пока версия C# у тебя нельзя record struct, не забывай что из за этого могут быть ошибки в сравнениях, если что добавь IEquatable<StatModifier>
public readonly struct StatModifier
{
    public ModifierType Type { get; }
    public float Value { get; }
    public object Source { get; }
    public StatModifier(ModifierType type, float value, object source)
    {
        Type = type;
        Value = value;
        Source = source;
    }
}
