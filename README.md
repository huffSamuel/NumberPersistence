# Number Persistence

Small .NET Core project to calculate the multiplicative persistence of numbers.

# Motivation

This project was inspired by [this](https://youtu.be/Wim9WJeDTHQ) Numberphile video about the multiplicitave persistence of numbers and what is special about the number **277777788888899**.

# Use

1. Build
2. `dotnet persistenceNumbers.dll`
3. Wait for your results

# Optimizations

This project will count up to ulong.MaxValue (18,446,744,073,709,551,615) to find the most persistent number. There are some minor optimizations included that will help the counting process.

## Regarding 0

If a number contains '0' it's multiplicative persistence from that point is only one more level since n x 0 = 0 so numbers that contain 0 are shorted out.

## Regarding 5

If a number contains '5' and an even number then the resulting multiplication will result in a multiple of 10 *(n x EVEN x 5 = m * p * 10)*. This then gets us to the case where the number will contain a zero and it can trigger our **Regarding 0** optimization so it is short circuited to return the current level + 2.

If a number contains '5' and there is no even number then the resulting multiplication will contain '5' and be even, triggering the above case so it is short circuited to return the current level + 3.
