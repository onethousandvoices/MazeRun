# MazeRun

MazeRun is a Unity 6 first-person maze prototype where the player collects all diamonds, avoids patrolling enemies, and reaches the exit. The scene is assembled at runtime from prefabs and ScriptableObject configs, with the maze, diamonds, enemies, UI, and exit driven by a simple grid model.

## What Is Implemented

  * First-person player movement through `CharacterController`
  * Mouse camera rotation with pitch clamp and locked cursor during gameplay
  * Sprint movement while holding `Shift`
  * Grid-authored maze layout with runtime floor and wall spawning
  * Wall blocking through solid wall colliders and the player `CharacterController`
  * Runtime spawning of player, enemies, diamonds, exit and UI prefabs
  * Randomized diamond placement from configured free grid cells
  * Diamond pickup on trigger contact with HUD counter refresh
  * Rotating diamond visuals
  * Two enemy spawns with configured patrol routes
  * Enemy chase based on the player's recorded grid trail
  * Defeat when an enemy reaches the player
  * Victory only after all diamonds are collected and the player enters the exit grid cell
  * Restart button on the result screen

## Controls

  * `WASD` - move
  * `Mouse` - look around
  * `Left Shift` - sprint
  * `Restart` - reload the scene after victory or defeat

## Architecture

    GameSession (entry point and game flow coordinator)
        |
        +-- MazeGrid          (grid coordinates, world conversion, wall lookup)
        +-- MazeBuilder       (runtime floor and wall spawning)
        +-- DiamondCounter    (collected / total diamond state)
        |
        +-- PlayerMovement    (WASD movement, sprint, CharacterController move)
        +-- PlayerLook        (mouse look and camera pitch)
        |
        +-- Diamond           (rotation, trigger pickup, hide after collection)
        +-- Enemy             (patrol, trail-based chase, catch detection)
        |
        +-- GameHud           (diamond counter UI)
        +-- GameResultView    (victory, defeat and restart UI)
        +-- UiRoot            (UGUI input module setup)

## Game Flow

  1. `GameSession` builds `MazeGrid` from `MazeConfig`.
  2. `MazeBuilder` instantiates floor and wall prefabs under a runtime `Maze` root.
  3. `GameSession` instantiates UI prefabs under one `UiRootPrefab` canvas.
  4. Player, exit, diamonds and enemies are instantiated from `PrefabConfig`.
  5. `Diamond` notifies `GameSession.Collect()` on trigger contact.
  6. `DiamondCounter` stores collection progress, and `GameHud` displays it.
  7. `GameSession.Update()` checks victory by comparing the player's grid cell with `MazeConfig.ExitCell`.
  8. `Enemy.Update()` patrols configured cells, chases the player trail inside detect radius, and calls `GameSession.Lose()` inside catch radius.
  9. `GameResultView` shows `Victory` or `Defeat` and reloads the active scene on restart.

## Configs And Prefabs

  * `MazeConfig` - cell size, rows, player cell, exit cell, diamond cells and enemy patrol data
  * `PlayerConfig` - walk speed, sprint speed and look sensitivity
  * `EnemyConfig` - patrol speed, chase speed, detect radius and catch radius
  * `PrefabConfig` - floor, wall, exit, player, enemy, diamond and UI prefab references
  * `Assets/Prefabs/UI` - editable UGUI prefabs for root canvas, HUD and result screen

## Running

  1. Open the project in Unity 6.
  2. Open `Assets/Scenes/SampleScene.unity`.
  3. Enter Play Mode.
  4. Collect all diamonds, avoid enemies, then step onto the exit cell.
