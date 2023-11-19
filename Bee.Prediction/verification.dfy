class Field {
    var MaxVisibleHoney: int
    var Name: string
}

class Results {
    var Name: string
    var HoneyAmount: int

  predicate Valid()
  reads this
  {
    HoneyAmount >= 0
  }

  constructor (name: string, honeyAmount: int)
  requires honeyAmount >= 0
  ensures Valid()
  {
    Name := name;
    HoneyAmount := honeyAmount;
  }
}


class Program {
   method AlgoV2(isFull: bool, results: array<Results>, maxHoneyCapacityKg: int, fields: array<Field>)
        requires results.Length == fields.Length
        modifies results, fields
    {
        var maxStored := 0;

        var i := 0;
        while i < fields.Length && !isFull
            invariant 0 <= i <= fields.Length
            invariant fields.Length == results.Length
            invariant maxStored >= 0
        {
            var num1 := 0;
            if maxHoneyCapacityKg < fields[i].MaxVisibleHoney {
                num1 := fields[i].MaxVisibleHoney - maxHoneyCapacityKg;
            } else {
                num1 := fields[i].MaxVisibleHoney;
            }

            var maxHoneyAmount1 := num1;

            var num2 := 0;
            if maxStored < maxHoneyAmount1 {
                num2 := maxHoneyAmount1 - maxStored;
            } else {
                num2 := 0;
            }

            var maxHoneyAmount2 := num2;

            var maxHoneyAmount3 := 0;
            if maxHoneyAmount2 >= maxHoneyCapacityKg && maxHoneyAmount2 >= maxStored {
                maxHoneyAmount3 := maxHoneyCapacityKg - maxStored;
            } else {
                maxHoneyAmount3 := maxHoneyAmount2;
            }

            if maxHoneyAmount3 >= 0 
            {
                maxStored := maxStored + maxHoneyAmount3;

                // Update array element with the new Results
                results[i] := new Results(fields[i].Name, maxHoneyAmount3);
            }

            i := i + 1;
        }
    }
}