using Bee.Prediction;
using Bee.Prediction.Models;
using Bee.Prediction.Models.Fields;
using Bogus;

var bees = new Faker<BaseBee>()
    .RuleFor(x => x.MaxFlightKm, _ => Random.Shared.Next(1, 10))
    .Generate(Random.Shared.Next(20000, 80000))
    .ToArray();

var combs = new Faker<HoneyComb>()
    .RuleFor(x => x.CapacityKg, _ => 2)
    .Generate(Random.Shared.Next(8, 12))
    .ToArray();

var hive = new BaseHive(bees, combs);

var hive2 = new BaseHive(bees, combs);

var fields = new IField[]
{
    new CerasusField
    {
        AreaHectares = 4,
        MinDistanceFromHiveKm = 0.3,
        VisibleToHiveHectares = 0.2,
    },
    new SinapisField
    {
        AreaHectares = 1,
        MinDistanceFromHiveKm = 6,
        VisibleToHiveHectares = .7,
    },
    new RobiniaAcaciaField
    {
        AreaHectares = 15,
        MinDistanceFromHiveKm = 2,
        VisibleToHiveHectares = .1,
    }
};

var results = new Results[fields.Length];

PredictionAlgorithmV2.AlgoV2(hive, ref results, fields);

hive.HoneyByFieldKg = results.GroupBy(x => x.Name).ToDictionary(x => x.Key, x => x.Sum(res=> res.HoneyAmount));

Console.WriteLine($"Max Honey from the fields: {fields.Sum(x=> x.MaxVisibleHoney)} Kg");
Console.WriteLine($"Collected Honey: {hive.HoneyStoredKg} Kg");
Console.WriteLine($"Max honey capacity: {hive.MaxHoneyCapacityKg} Kg");
foreach (var kvp in hive.HoneyByFieldKg)
{
    Console.WriteLine($"Field {kvp.Key} and honey stored {kvp.Value} kg, which is {(kvp.Value/hive.HoneyStoredKg)*100}% of all");
}

Console.WriteLine("2");
PredictionAlgorithm.Algo(hive2, fields);
Console.WriteLine($"Max Honey from the fields: {fields.Sum(x=> x.MaxVisibleHoney)} Kg");
Console.WriteLine($"Collected Honey: {hive2.HoneyStoredKg} Kg");
Console.WriteLine($"Max honey capacity: {hive2.MaxHoneyCapacityKg} Kg");
foreach (var kvp in hive2.HoneyByFieldKg)
{
    Console.WriteLine($"Field {kvp.Key} and honey stored {kvp.Value} kg, which is {(kvp.Value/hive2.HoneyStoredKg)*100}% of all");
}