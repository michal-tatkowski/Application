using Ardalis.SmartEnum;

namespace Core.Enums;

/// <summary>
/// Enum, który ma pomóc w wyeliminowaniu magicznych liczb zastępując je stałą wartością.
/// </summary>
public sealed class Quantity : SmartEnum<Quantity>
{
    public static readonly Quantity Zero = new(nameof(Zero), 0);
    public static readonly Quantity One = new(nameof(One), 1);
    public static readonly Quantity Two = new(nameof(Two), 2);
    public static readonly Quantity Three = new(nameof(Three), 3);
    public static readonly Quantity Four = new(nameof(Four), 4);
    public static readonly Quantity Five = new(nameof(Five), 5);
    public static readonly Quantity Six = new(nameof(Six), 6);
    public static readonly Quantity Seven = new(nameof(Seven), 7);
    public static readonly Quantity Eight = new(nameof(Eight), 8);
    public static readonly Quantity Nine = new(nameof(Nine), 9);
    public static readonly Quantity Ten = new(nameof(Ten), 10);
    public static readonly Quantity TwentyFive = new(nameof(TwentyFive), 25);

    public Quantity(string name, int value) : base(name, value)
    {
    }
}