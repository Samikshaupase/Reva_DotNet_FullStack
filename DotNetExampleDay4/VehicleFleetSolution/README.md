# Vehicle Fleet Management System

## Overview
A console-based application demonstrating OOP principles using C#.

## Key Concepts Used
- Abstraction (Vehicle base class)
- Inheritance (Car, Truck)
- Polymorphism (List<Vehicle>)
- Interfaces (IMaintainable, ITrackable)
- Explicit Interface Implementation
- Encapsulation (Fuel validation)
- Thread-safe static ID generation
- Unit Testing (xUnit)

## Design Decisions
- Domain logic isolated from UI
- Vehicle IDs generated centrally
- Interfaces used to avoid tight coupling

## Known Limitations
- No persistence (in-memory only)
- Console-based UI
- Location is mocked

## How to Run
1. Open solution in Visual Studio
2. Restore NuGet packages
3. Run ConsoleApp
4. Run Tests via Test Explorer
