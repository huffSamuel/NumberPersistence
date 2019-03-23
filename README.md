# Number Persistence

Small .NET Core project to calculate the multiplicative persistence of numbers.

# Motivation

This project was inspired by [this](https://youtu.be/Wim9WJeDTHQ) Numberphile video about the multiplicitave persistence of numbers and what is special about the number **277777788888899**.

# Use

1. Build
2. `dotnet persistenceNumbers.dll`
3. Wait for your results

This project will count up to ulong.MaxValue (18,446,744,073,709,551,615) to find the most persistent number. There are some minor optimizations included that will help the counting process.

If a number contains '0' it's multiplicative persistence from that point is only one more level since n x 0 = 0 so numbers that contain 0 are shorted out. If a number contains '5' there is a good chance it also contains an even number which will be 10 x n and the subsequent persistence will contain a 0.
