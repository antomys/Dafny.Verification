namespace Bee.Prediction.Models;

public sealed class BaseBee
{
    public float MaxFlightKm { get; init; }

    public double NectarCapacityKg { get; init; } = 40/1000000d;
}