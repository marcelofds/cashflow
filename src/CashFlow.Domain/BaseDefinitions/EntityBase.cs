using System.Diagnostics.CodeAnalysis;

namespace CashFlow.Domain.BaseDefinitions;

[ExcludeFromCodeCoverage]
public class Entity
{
    public int Id { get; set; }

    public override bool Equals(object? obj)
    {
        return obj is Entity tmp && tmp.Id == Id;
    }

    public static bool operator ==(Entity? a, Entity? b)
    {
        if (ReferenceEquals(a, b))
            return true;
        if (ReferenceEquals(a, null))
            return false;
        return !ReferenceEquals(b, null) && a.Equals(b);
    }

    public static bool operator !=(Entity? a, Entity? b)
    {
        return !(a == b);
    }

    public override string ToString()
    {
        return GetType().Name + "[Id = " + Id + "]";
    }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}