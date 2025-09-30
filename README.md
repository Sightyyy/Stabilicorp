<!-- <table>
  <tr>
    <td align="left" width="50%">
      <img width="100%" alt="gif1" src="">
    </td>
    <td align="right" width="50%">
      <img width="100%" alt="gif2" src="">
    </td>
  </tr>
</table> -->

##  📜Scripts and Features

You can make decisions for your company, and witness the cute AI movements of the workers thanks to the following scripts.

|  Script       | Description                                                  |
| ------------------- | ------------------------------------------------------------ |
| `PlayerAndSecretaryBehavior.cs` | Controls CEO and secretary movement using A* pathfinding to navigate between home and work locations |
| `DecisionManager.cs` | Manages random business events with multiple choices that affect company stats like finance, worker happiness, and client trust |
| `GridManager.cs`  | Creates a navigable grid system and implements A* pathfinding algorithm for character movement around obstacles |
| `StorylineScript.cs`  | Handles the introductory narrative sequence with typing effects, player input for CEO/company names, and branching story choices |
| `DayAndTimeManager.cs`  | Manages the game's time progression, daily routines, worker commands, project completion, and stat changes while tracking the calendar system |
| `WorkerSpawner.cs`  | Dynamically spawns and despawns worker characters in grid formations based on the current worker count in GameData |
| `etc`  | |

<br>


## 🔴About
Stabilicorp is a management decision-making game where you need to manage your company's financial situation, client trust, and worker happiness. Your objective none other than to keep your company growing, and not bankruptcy. I handled most of the main mechanics and also arranged the objects in unity. Here's some details about Stabilicorp's development.
<br>

## 🕹️Play Game
Currently in development phase
<br>

## 👤Developer & Contributions
- Frederick Aurelius (Game Designer/Artist)
- Kayla Cynthia Lukman (Game Programmer)
- Ruliyanto Rasyid Huda (Game Artist)
- Michael Wong (Game Designer)
<br>

## 📂Files description

```
├── Stabilicorp                       # Contains everything needed for Stabilicorp to work as a game.
   ├── Assets                         # Contains every assets that is connected with unity to create the game, like the scripts and the art.
      ├── Animation                   # Contains every animation clip and animator controller that is used for the game.
      ├── Audio                       # Contains every audio clip used for the game, like the background music and sound effects.
      ├── Drawing Assets              # Contains all the game art, like the sprites used for character, background, etc.
      ├── Scenes                      # Contains all scenes that exist in the game, used to connect between storyline, gameplay scene, etc.
      ├── Scripts                     # Contains all the scripts needed to make the game functioning.
      ├── Prefabs                     # Contains every reusable game objects that will be instantiated in the game.
```
      

<br>

## 🕹️Game controls

The following controls are bound in-game, for gameplay and testing.

| Key Binding       | Function          |
| Left Mouse Click             | Decision-making, camera shifting, etc              |
| Esc             | Pause              |

<br>
