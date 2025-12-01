# Backend-MazeRunner
A console-based maze game where the player collects coins while avoiding enemies. The game runs **only in the terminal** and is designed for easy integration with a frontend UI.

## 🚀 How to Run

1. **Clone the repository:**
   ```bash
   git clone <your-repo-url>
   cd <your-repo-folder>
````

2.  **Run the game:**
    ```bash
    dotnet run
    ```

> **Note:** This game runs only in the terminal. Ensure you have .NET SDK installed.
>**Personal Note**: I can't work on this project with full capacity as I was facing issues in my system. Because of this, there is no commit history too.

## ✅ Features Added to Support Frontend

*   **Structured Game Logic:** Modular design for easy integration with UI.
*   **Clear Models:** Player, Enemy, Coin classes for clean data handling.
*   **Timer-based Enemy Movement:** Supports real-time updates for UI.

## 🔥 Improvements in This Version

*   **Bigger Mazes per Level:**
    *   Level 1: 10×10
    *   Level 2: 12×12
    *   Level 3: 15×15
*   **Faster Enemy Movement:**
    *   Level 1: 1000ms
    *   Level 2: 750ms
    *   Level 3: 500ms
*   **Strategic Enemy Placement:** Enemies near coins and key paths.
*   **Coins Spread Across Maze:** 5–8 coins per level.
*   **Game Over More Likely:** Maze paths force player to cross enemy zones.

## 🛠 Technologies

*   C# (.NET Core)
*   Console-based rendering

## 🎮 Gameplay

*   Use arrow keys or WASD to move the player.
*   Collect all coins while avoiding enemies.

***

Enjoy the challenge and feel free to extend the game with a graphical frontend!

