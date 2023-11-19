namespace Bee.Prediction.Models.Fields;

public sealed class SinapisField : IField
{
    public string Name { get; init; } = "Гірчиця біла";
    
    public float AreaHectares { get; init; }
   
    public double MinDistanceFromHiveKm { get; init; }
   
    public double VisibleToHiveHectares { get; init; }

    public int[] MaxHoneyCapacityForHectare { get; init; } = { 30, 150 };
}