# CyberSnake

Made this in school as a C# assignment to create a console based snake game.

## Build and run

Use `dotnet run` to compile and start the game directly from the repository root:

```bash
dotnet run --project CyberSnake/CyberSnake
```

## Running the tests

Unit tests are located in the `CyberSnakeTests` project. Execute them with:

```bash
dotnet test CyberSnake/CyberSnake.sln
```

## Controls

Move the snake with **WASD**. Press **Q** at any time to end the game. Colliding with walls or your own tail results in game over.

## Gameplay

The game is rendered entirely in the console. Collect food to grow longer and aim for a high score without hitting any obstacles.
