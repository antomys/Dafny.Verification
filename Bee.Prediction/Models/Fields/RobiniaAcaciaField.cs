namespace Bee.Prediction.Models.Fields;

public sealed class RobiniaAcaciaField : IField
{
    public string Name { get; init; } = "Акація біла";
    
    public float AreaHectares { get; init; }
   
    public double MinDistanceFromHiveKm { get; init; }
   
    public double VisibleToHiveHectares { get; init; }

    public int[] MaxHoneyCapacityForHectare { get; init; } = { 200, 500 };
}