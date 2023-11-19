using Bee.Prediction.Models;
using Bee.Prediction.Models.Fields;

namespace Bee.Prediction;

public static class PredictionAlgorithm
{
    public static void Algo(BaseHive hive, IField[] fields)
    {
        if (hive.IsFull)
        {
            return;
        }
        
        var probabilities = CalculateSoftmax(fields).OrderByDescending(p => p.Value);

        var maxStored = hive.HoneyStoredKg;
        
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
                ? 0
                : maxHoneyAmount - maxStored;

            if (maxHoneyAmount >= hive.MaxHoneyCapacityKg && maxHoneyAmount >= maxStored)
            {
                maxHoneyAmount = hive.MaxHoneyCapacityKg - maxStored;
            }

            maxStored += maxHoneyAmount;

            hive.HoneyByFieldKg.TryGetValue(field.Name, out var storedByField);

            hive.HoneyByFieldKg[field.Name] = maxHoneyAmount + storedByField;
        }
    }
    
    public static Dictionary<IField, double> CalculateSoftmax(this IField[] fields)
    {
        var sortedFields = fields.OrderBy(x=> x.Name).ToArray();
    
        var distances = sortedFields.Select(x => (double)x.MinDistanceFromHiveKm);
        
        var z_exp = distances.Select(Math.Exp);
        // [2.72, 7.39, 20.09, 54.6, 2.72, 7.39, 20.09]

        var sum_z_exp = z_exp.Sum();
        // 114.98

        var softmax = z_exp.Select(i => i / sum_z_exp).ToArray();
    
        var dictionary = new Dictionary<IField, double>(fields.Length);
        var idx = 0;
        foreach (var field in sortedFields)
        {
            dictionary.Add(field, 1d - softmax[idx++]);
        }
    
        return dictionary;
    }
}