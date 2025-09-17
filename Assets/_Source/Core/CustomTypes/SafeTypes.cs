using System;
using UnityEngine;

[Serializable]
public struct SDouble
{
    [field:SerializeField]
    private double offset;
    [field:SerializeField]
    private double value;
    public SDouble(double value = 0)
    {
        offset = UnityEngine.Random.Range(-1100, +1100);
        this.value = value + offset;
    }
    public readonly double GetValue()
    {
        return value - offset;
    }
    public void Dispose()
    {
        offset = 0;
        value = 0;
    }
    // holy shit, so much in here...
    public override readonly string ToString() => GetValue().ToString();
    public static SDouble operator +(SDouble f1, SDouble f2) => new(f1.GetValue() + f2.GetValue());
    public static SDouble operator -(SDouble f1, SDouble f2) => new(f1.GetValue() - f2.GetValue());
    public static SDouble operator *(SDouble f1, SDouble f2) => new(f1.GetValue() * f2.GetValue());
    public static SDouble operator /(SDouble f1, SDouble f2) => new(f1.GetValue() / f2.GetValue());
    public static bool operator <(SDouble f1, SDouble f2) => f1.GetValue() < f2.GetValue();
    public static bool operator >(SDouble f1, SDouble f2) => f1.GetValue() > f2.GetValue();
    public static bool operator ==(SDouble f1, SDouble f2) => f1.GetValue() == f2.GetValue();
    public static bool operator !=(SDouble f1, SDouble f2) => f1.GetValue() != f2.GetValue();
    public static bool operator >=(SDouble f1, SDouble f2) => f1.GetValue() >= f2.GetValue();
    public static bool operator <=(SDouble f1, SDouble f2) => f1.GetValue() <= f2.GetValue();
    // for normal double :sob:
    public static SDouble operator +(SDouble f1, double f2) => new(f1.GetValue() + f2);
    public static SDouble operator -(SDouble f1, double f2) => new(f1.GetValue() - f2);
    public static SDouble operator *(SDouble f1, double f2) => new(f1.GetValue() * f2);
    public static SDouble operator /(SDouble f1, double f2) => new(f1.GetValue() / f2);
    public static bool operator <(SDouble f1, double f2) => f1.GetValue() < f2;
    public static bool operator >(SDouble f1, double f2) => f1.GetValue() > f2;
    public static bool operator ==(SDouble f1, double f2) => f1.GetValue() == f2;
    public static bool operator !=(SDouble f1, double f2) => f1.GetValue() != f2;
    public static bool operator >=(SDouble f1, double f2) => f1.GetValue() >= f2;
    public static bool operator <=(SDouble f1, double f2) => f1.GetValue() <= f2;
    public override bool Equals(object obj)
    {
        return GetValue().Equals(obj);
    }
    public override int GetHashCode()
    {
        return GetValue().GetHashCode();
    }
}
[Serializable]
public struct SInt
{
    [field:SerializeField]
    private int offset;
    [field:SerializeField]
    private int value;
    public SInt(int value = 0)
    {
        offset = UnityEngine.Random.Range(-1100, +1100);
        this.value = value + offset;
    }
    public readonly int GetValue()
    {
        return value - offset;
    }
    public void Dispose()
    {
        offset = 0;
        value = 0;
    }
    // holy shit, so much in here...
    public override readonly string ToString() => GetValue().ToString();
    public static SInt operator +(SInt f1, SInt f2) => new(f1.GetValue() + f2.GetValue());
    public static SInt operator -(SInt f1, SInt f2) => new(f1.GetValue() - f2.GetValue());
    public static SInt operator *(SInt f1, SInt f2) => new(f1.GetValue() * f2.GetValue());
    public static SInt operator /(SInt f1, SInt f2) => new(f1.GetValue() / f2.GetValue());
    public static bool operator <(SInt f1, SInt f2) => f1.GetValue() < f2.GetValue();
    public static bool operator >(SInt f1, SInt f2) => f1.GetValue() > f2.GetValue();
    public static bool operator ==(SInt f1, SInt f2) => f1.GetValue() == f2.GetValue();
    public static bool operator !=(SInt f1, SInt f2) => f1.GetValue() != f2.GetValue();
    public static bool operator >=(SInt f1, SInt f2) => f1.GetValue() >= f2.GetValue();
    public static bool operator <=(SInt f1, SInt f2) => f1.GetValue() <= f2.GetValue();
    // for normal int :sob:
    public static SInt operator +(SInt f1, int f2) => new(f1.GetValue() + f2);
    public static SInt operator -(SInt f1, int f2) => new(f1.GetValue() - f2);
    public static SInt operator *(SInt f1, int f2) => new(f1.GetValue() * f2);
    public static SInt operator /(SInt f1, int f2) => new(f1.GetValue() / f2);
    public static bool operator <(SInt f1, int f2) => f1.GetValue() < f2;
    public static bool operator >(SInt f1, int f2) => f1.GetValue() > f2;
    public static bool operator ==(SInt f1, int f2) => f1.GetValue() == f2;
    public static bool operator !=(SInt f1, int f2) => f1.GetValue() != f2;
    public static bool operator >=(SInt f1, int f2) => f1.GetValue() >= f2;
    public static bool operator <=(SInt f1, int f2) => f1.GetValue() <= f2;
    public override bool Equals(object obj)
    {
        return GetValue().Equals(obj);
    }
    public override int GetHashCode()
    {
        return GetValue().GetHashCode();
    }
}