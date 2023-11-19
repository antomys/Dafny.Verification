using Bee.Prediction.Models;
using Bee.Prediction.Models.Fields;

namespace Bee.Prediction;

public static class PredictionAlgorithmV2
{
    public static void AlgoV2(
        BaseHive hive,
        ref Results[] results,
        IField[] fields)
    {
        if (results.Length > fields.Length || hive.IsFull)
        {
            return;
        }

        var probabilities = fields.CalculateSoftmax().OrderByDescending(p => p.Value);

        var maxStored = hive.HoneyStoredKg;

        var i = 0;
        foreach (var (field, probability) in probabilities)
        {
            Console.WriteLine($"Probability of collection honey from the field {field.Name} is {probability}");

            if (hive.IsFull)
            {
                return;
            }

            var maxHoneyAmount = hive.MaxHoneyCapacityKg >= field.MaxVisibleHoney
                ? field.MaxVisibleHoney
                : field.MaxVisibleHoney - hive.MaxHoneyCapacityKg;
            
            maxHoneyAmount = maxStored >= maxHoneyAmount
                ? 0f
                : maxHoneyAmount - maxStored;

            if (maxHoneyAmount >= hive.MaxHoneyCapacityKg && maxHoneyAmount >= maxStored)
            {
                maxHoneyAmount = hive.MaxHoneyCapacityKg - maxStored;
            }

            if (maxHoneyAmount <= 0f)
            {
                continue;
            }
            
            maxStored += maxHoneyAmount;

            results[i++] = new Results
            {
                Name = field.Name,
                HoneyAmount = maxHoneyAmount,
            };
        }
    }
}
