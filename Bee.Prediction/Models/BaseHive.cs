namespace Bee.Prediction.Models;

public sealed class BaseHive
{
    public BaseHive(
        BaseBee[] bees, 
        HoneyComb[] combs)
    {
        Bees = bees;
        Combs = combs;
        MaxHoneyCapacityKg = CalculateMaxCapacity();
        HoneyByFieldKg = new Dictionary<string, double>(0);
    }

    public BaseBee[] Bees { get; init; }
    
    public HoneyComb[] Combs { get; init; }
    
    public Dictionary<string, double> HoneyByFieldKg { get; set; }
    
    public double HoneyStoredKg => HoneyByFieldKg.Values.Sum();
    
    public double MaxHoneyCapacityKg { get; init; }
    
    public bool IsFull => HoneyStoredKg >= MaxHoneyCapacityKg;

    private double CalculateMaxCapacity()
    {
        return Combs.Sum(comb => comb.CapacityKg);
    }
}