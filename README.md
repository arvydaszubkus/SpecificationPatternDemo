# Specification Pattern Demo (C#)

A simple C# console app showing the **Specification Pattern** in action.

## Features
- Encapsulates business rules in reusable specifications
- Combines specs with **AND / OR / NOT**
- Works with LINQ expressions (`Expression<Func<T,bool>>`)
- Example: filtering orders by `Paid`, `Expensive`, `Recent`, etc.

## Run
```bash
dotnet build
dotnet run
