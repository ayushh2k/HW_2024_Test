# Doofus Adventure Game

## Overview

**Doofus Adventure Game** is a Unity-based platformer where players guide a character named Doofus across disappearing platforms, collecting points and avoiding obstacles. The game features both normal and hard difficulty modes, with varying levels of challenge and gameplay dynamics.

## Features

- **Platforming Mechanics**: Move Doofus across platforms that disappear after a set time.
- **Difficulty Modes**: Normal mode with standard platforms and Hard mode with additional obstacles.
- **Dynamic Obstacles**: In Hard mode, obstacles are placed on platforms, adding an extra layer of challenge.
- **UI Elements**: Includes start screen, difficulty selection, score display, and game over screen with restart functionality.
- **Music**: Background music to enhance the gaming experience.

## Getting Started

### Prerequisites

- Unity 2022 or later
- Basic knowledge of Unity and C#
- JSON data for game configuration

### Setup

1. **Clone the Repository**:

   ```bash
   git clone <repository_url>
   cd HW_2024_Test
   ```

2. **Open the Project**:
   - Open Unity Hub and add the project folder.

3. **Import Assets**:
   - Ensure that all required assets, including prefabs and audio files, are imported into the Unity project.

4. **Configure the Game**:
   - Set up the `ScoreManager` and `PulpitManager` scripts according to the game requirements.
   - Ensure the `Doofus` character and `Pulpit` prefabs are correctly configured.

5. **Build and Run**:
   - Click on `File` > `Build Settings`, select your target platform, and click `Build`.

## How to Play

- **Start the Game**: Click on the start button on the main menu.
- **Movement**: Use the `W`, `A`, `S`, `D` keys to move Doofus.
- **Difficulty**: Select the desired difficulty before starting the game.
- **Score**: Collect points by successfully moving Doofus across platforms.
- **Game Over**: Avoid falling off the platforms or hitting obstacles. Restart the game from the game over screen if needed.

## Files and Scripts

- **`DoofusMovement.cs`**: Controls the movement of the Doofus character.
- **`PulpitManager.cs`**: Manages the spawning and despawning of platforms and obstacles.
- **`ScoreManager.cs`**: Handles the scoring system.
