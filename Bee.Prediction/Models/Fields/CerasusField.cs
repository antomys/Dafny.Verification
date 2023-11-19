namespace Bee.Prediction.Models.Fields;

public sealed class CerasusField : IField
{
    public string Name { get; init; } = "Вишня";
    
    public float AreaHectares { get; init; }
   
    public double MinDistanceFromHiveKm { get; init; }
    
    public double VisibleToHiveHectares { get; init; }

    public int[] MaxHoneyCapacityForHectare { get; init; } = { 28, 32 };
}