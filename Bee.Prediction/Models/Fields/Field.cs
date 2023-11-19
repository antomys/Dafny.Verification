namespace Bee.Prediction.Models.Fields;

public interface IField
{
    string Name { get; init; }
    
    float AreaHectares { get; init; }
    
    double MinDistanceFromHiveKm { get; init; }
    
    double VisibleToHiveHectares { get; init; }

    int[] MaxHoneyCapacityForHectare { get; init; }
    
    double MaxHoney => AreaHectares * MaxHoneyCapacityForHectare.Average();

    double MaxVisibleHoney => VisibleToHiveHectares * MaxHoneyCapacityForHectare.Average();
}