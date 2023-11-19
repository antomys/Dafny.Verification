class BaseHive {
    var HoneyStoredKg: real
    var MaxHoneyCapacityKg: real
    var IsFull: bool

    constructor(honeyStoredKg: real, maxHoneyCapacityKg: real, isFull: bool)
        requires honeyStoredKg >= 0.0 && maxHoneyCapacityKg >= 0.0
    {
        HoneyStoredKg := honeyStoredKg;
        MaxHoneyCapacityKg := maxHoneyCapacityKg;
        IsFull := isFull;
    }
}

class IField {
    var MaxVisibleHoney: real
    var Name: string
}

class Results {
    var Name: string
    var HoneyAmount: real

  predicate Valid()
  reads this
  {
    HoneyAmount >= 0.0
  }

  constructor (name: string, honeyAmount: real)
  requires honeyAmount >= 0.0
  ensures Valid()
  { 
    Name := name;
    HoneyAmount := honeyAmount;
  }
}

method AlgoV2(hive: BaseHive, results: array<Results>, fields: array<IField>)
requires results.Length == fields.Length
modifies results, fields
{
    var maxStored := hive.HoneyStoredKg;

    var i := 0;
    while i < fields.Length && !hive.IsFull
        invariant 0 <= i <= fields.Length
        invariant fields.Length == results.Length
        invariant maxStored >= hive.HoneyStoredKg
    {
        var num1 := 0.0;
        if hive.MaxHoneyCapacityKg < fields[i].MaxVisibleHoney {
            num1 := fields[i].MaxVisibleHoney - hive.MaxHoneyCapacityKg;
        } else {
            num1 := fields[i].MaxVisibleHoney;
        }

        var maxHoneyAmount1 := num1;

        var num2 := 0.0;
        if maxStored < maxHoneyAmount1 {
            num2 := maxHoneyAmount1 - maxStored;
        } else {
            num2 := 0.0;
        }

        var maxHoneyAmount2 := num2;

        var maxHoneyAmount3 := 0.0;
        if maxHoneyAmount2 >= hive.MaxHoneyCapacityKg && maxHoneyAmount2 >= maxStored {
            maxHoneyAmount3 := hive.MaxHoneyCapacityKg - maxStored;
        } else {
            maxHoneyAmount3 := maxHoneyAmount2;
        }

        if maxHoneyAmount3 >= 0.0 
        {
            maxStored := maxStored + maxHoneyAmount3;

            // Update array element with the new Results
            results[i] := new Results(fields[i].Name, maxHoneyAmount3);
        }

        i := i + 1;
    }
}
